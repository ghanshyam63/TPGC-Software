using System;
using System.Collections;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Xml.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Collections.Generic;

public partial class Purchase_PurchaseInquiry : BasePage
{
    #region defind Class Object
    Common cmn = new Common();
    Inv_PurchaseInquiryHeader objPIHeader = new Inv_PurchaseInquiryHeader();
    Inv_PurchaseInquiryDetail ObjPIDetail = new Inv_PurchaseInquiryDetail();
    Inv_PurchaseInquiry_Supplier ObjPISupplier = new Inv_PurchaseInquiry_Supplier();
    Ems_GroupMaster objGroupMaster = new Ems_GroupMaster();
    PurchaseRequestHeader objPRHeader = new PurchaseRequestHeader();
    PurchaseRequestDetail ObjPRDetail = new PurchaseRequestDetail();
    CountryMaster ObjCountry = new CountryMaster();
    Inv_ProductMaster objProductM = new Inv_ProductMaster();
    Inv_UnitMaster UM = new Inv_UnitMaster();
    SystemParameter ObjSysParam = new SystemParameter();
    Inv_SalesInquiryDetail ObjSalesInquiryDetail = new Inv_SalesInquiryDetail();
    Inv_SalesInquiryHeader ObjSelesInquiryHeader = new Inv_SalesInquiryHeader();
    UserMaster ObjUser = new UserMaster();
    Set_DocNumber objDocNo = new Set_DocNumber();

    string StrCompId = string.Empty;
    string StrBrandId = string.Empty;
    string StrLocationId = string.Empty;
    string StrUserId = string.Empty;
    #endregion

    protected void Page_Load(object sender, EventArgs e)
    {



        if (Session["UserId"] == null)
        {
            Response.Redirect("~/Login.aspx");
        }
        if (!IsPostBack)
        {
            StrBrandId = Session["BrandId"].ToString();
            StrLocationId = Session["LocId"].ToString();
            StrUserId = Session["UserId"].ToString();
            StrCompId = Session["CompId"].ToString();

            ddlOption.SelectedIndex = 2;
            btnList_Click(null, null);
            FillGridBin();
            FillGrid();
            FillRequestGrid();
            txtPINo.Text = GetDocumentNumber();
            txtPIDate.Text = DateTime.Now.ToString(ObjSysParam.GetSysParameterByParamName("Date_Format").Rows[0]["Param_Value"].ToString());
        }
        AllPageCode();
    }

    #region System defind Funcation
    protected void btnList_Click(object sender, EventArgs e)
    {
        pnlMenuList.BackColor = System.Drawing.ColorTranslator.FromHtml("#ccddee");

        pnlMenuRequest.BackColor = System.Drawing.ColorTranslator.FromHtml("#90bde9");
        pnlMenuNew.BackColor = System.Drawing.ColorTranslator.FromHtml("#90bde9");
        pnlMenuBin.BackColor = System.Drawing.ColorTranslator.FromHtml("#90bde9");

        PnlList.Visible = true;
        PnlNewEdit.Visible = false;
        pnlRequest.Visible = false;
        PnlBin.Visible = false;
        txtValue.Focus();
        txtValue.Text = "";
        pnlSupplier.Visible = false;

    }
    protected void btnNew_Click(object sender, EventArgs e)
    {
        pnlMenuNew.BackColor = System.Drawing.ColorTranslator.FromHtml("#ccddee");

        pnlMenuRequest.BackColor = System.Drawing.ColorTranslator.FromHtml("#90bde9");
        pnlMenuList.BackColor = System.Drawing.ColorTranslator.FromHtml("#90bde9");
        pnlMenuBin.BackColor = System.Drawing.ColorTranslator.FromHtml("#90bde9");

        PnlList.Visible = false;
        PnlNewEdit.Visible = true;
        pnlRequest.Visible = false;
        pnlSupplier.Visible = false;
        PnlBin.Visible = false;
        txtPINo.Focus();
    }
    protected void btnEdit_Command(object sender, CommandEventArgs e)
    {
        editid.Value = e.CommandArgument.ToString();

        DataTable dtPIEdit = objPIHeader.GetPIHeaderAllDataByTransId(StrCompId, StrBrandId, StrLocationId, editid.Value);

        btnNew.Text = Resources.Attendance.Edit;
        if (dtPIEdit.Rows.Count > 0)
        {
            txtPINo.Text = dtPIEdit.Rows[0]["PI_No"].ToString();
            txtPINo.ReadOnly = true;
            txtPIDate.Text = Convert.ToDateTime(dtPIEdit.Rows[0]["PIDate"].ToString()).ToString(ObjSysParam.GetSysParameterByParamName("Date_Format").Rows[0]["Param_Value"].ToString());
            txtDescription.Text = dtPIEdit.Rows[0]["Description"].ToString();

            //Add Trans Panel
            string strTransFrom = dtPIEdit.Rows[0]["TransFrom"].ToString();
            string strTransNo = dtPIEdit.Rows[0]["TransNo"].ToString();
            hdnTransNo.Value = strTransNo;
            if (strTransFrom != "")
            {
                pnlTrans.Visible = true;
                if (strTransFrom == "PR")
                {
                    txtTransFrom.Text = Resources.Attendance.Purchase_Request;
                    DataTable dtRequest = objPRHeader.GetPurchaseRequestTrueAllByReqId(StrCompId, StrBrandId, StrLocationId, strTransNo);
                    if (dtRequest.Rows.Count > 0)
                    {
                        txtTransNo.Text = dtRequest.Rows[0]["RequestNo"].ToString();
                    }
                }
                else
                {
                    txtTransFrom.Text = Resources.Attendance.Sales_Inquiry;
                    DataTable dtRequest = ObjSelesInquiryHeader.GetSIHeaderAllBySInquiryId(StrCompId, StrBrandId, StrLocationId, strTransNo);
                    if (dtRequest.Rows.Count > 0)
                    {
                        txtTransNo.Text = dtRequest.Rows[0]["SInquiryNo"].ToString();
                    }

                }


            }

            //Add Product Section For Edit
            DataTable dtProduct = ObjPIDetail.GetPIDetailByPI_No(StrCompId, StrBrandId, StrLocationId, editid.Value);
            if (dtProduct.Rows.Count > 0)
            {
                GvProduct.DataSource = dtProduct;
                GvProduct.DataBind();

                foreach (GridViewRow gvr in GvProduct.Rows)
                {
                    Label lblProductId = (Label)gvr.FindControl("lblgvProductId");
                    Label lblProductName = (Label)gvr.FindControl("lblgvProductName");
                    Label lblProductDesc = (Label)gvr.FindControl("lblgvProductDescription");

                    if (lblProductId.Text == "0")
                    {
                        if (lblProductDesc.Text != "")
                        {
                            lblProductName.Text = lblProductDesc.Text;
                        }
                    }
                }

            }
        }
        AllPageCode();
        btnNew_Click(null, null);
        System.Web.UI.ScriptManager.GetCurrent(this).SetFocus(txtPINo);
    }
    protected void GvPurchaseInquiry_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        GvPurchaseInquiry.PageIndex = e.NewPageIndex;
        DataTable dt = (DataTable)Session["dtFilter"];
        GvPurchaseInquiry.DataSource = dt;
        GvPurchaseInquiry.DataBind();
        AllPageCode();
    }
    protected void btnbindrpt_Click(object sender, ImageClickEventArgs e)
    {
        if (ddlOption.SelectedIndex != 0)
        {
            string condition = string.Empty;
            if (ddlOption.SelectedIndex == 1)
            {
                condition = "convert(" + ddlFieldName.SelectedValue + ",System.String)='" + txtValue.Text.Trim() + "'";
            }
            else if (ddlOption.SelectedIndex == 2)
            {
                condition = "convert(" + ddlFieldName.SelectedValue + ",System.String) like '%" + txtValue.Text.Trim() + "%'";
            }
            else
            {
                condition = "convert(" + ddlFieldName.SelectedValue + ",System.String) Like '" + txtValue.Text.Trim() + "%'";
            }
            DataTable dtAdd = (DataTable)Session["dtCurrency"];
            DataView view = new DataView(dtAdd, condition, "", DataViewRowState.CurrentRows);
            GvPurchaseInquiry.DataSource = view.ToTable();
            Session["dtFilter"] = view.ToTable();
            lblTotalRecords.Text = Resources.Attendance.Total_Records + " : " + view.ToTable().Rows.Count + "";
            GvPurchaseInquiry.DataBind();
            AllPageCode();
            btnRefresh.Focus();
        }
    }
    protected void GvPurchaseInquiry_Sorting(object sender, GridViewSortEventArgs e)
    {
        DataTable dt = (DataTable)Session["dtFilter"];
        string sortdir = "DESC";
        if (ViewState["SortDir"] != null)
        {
            sortdir = ViewState["SortDir"].ToString();
            if (sortdir == "ASC")
            {
                e.SortDirection = SortDirection.Descending;
                ViewState["SortDir"] = "DESC";
            }
            else
            {
                e.SortDirection = SortDirection.Ascending;
                ViewState["SortDir"] = "ASC";
            }
        }
        else
        {
            ViewState["SortDir"] = "DESC";
        }

        dt = (new DataView(dt, "", e.SortExpression + " " + ViewState["SortDir"].ToString(), DataViewRowState.CurrentRows)).ToTable();
        Session["dtFilter"] = dt;
        GvPurchaseInquiry.DataSource = dt;
        GvPurchaseInquiry.DataBind();
        AllPageCode();
    }
    protected void IbtnDelete_Command(object sender, CommandEventArgs e)
    {
        editid.Value = e.CommandArgument.ToString();
        int b = 0;
        b = objPIHeader.DeletePIHeader(StrCompId, StrBrandId, StrLocationId, editid.Value, "false", StrUserId, DateTime.Now.ToString());
        if (b != 0)
        {
            DisplayMessage("Record Delete");
        }
        else
        {
            DisplayMessage("Record  Not Deleted");
        }
        FillGridBin(); //Update grid view in bin tab
        FillGrid();
        Reset();
    }
    protected void btnRefreshReport_Click(object sender, ImageClickEventArgs e)
    {
        FillGrid();
        FillGridBin();
        ddlFieldName.SelectedIndex = 1;
        ddlOption.SelectedIndex = 3;
        txtValue.Text = "";
        txtValue.Focus();
    }
    protected void btnPICancel_Click(object sender, EventArgs e)
    {
        Reset();
        btnList_Click(null, null);
        FillGridBin();
        FillGrid();
    }
    protected void BtnReset_Click(object sender, EventArgs e)
    {
        Reset();
        System.Web.UI.ScriptManager.GetCurrent(this).SetFocus(txtPINo);
    }
    protected void btnPISave_Click(object sender, EventArgs e)
    {
        if (txtPIDate.Text == "")
        {
            DisplayMessage("Enter Purchase Inquiry Date");
            System.Web.UI.ScriptManager.GetCurrent(this).SetFocus(txtPIDate);
            return;
        }
        if (txtPINo.Text == "")
        {
            DisplayMessage("Enter Purchase Inquiry No.");
            System.Web.UI.ScriptManager.GetCurrent(this).SetFocus(txtPINo);
            return;
        }
        else
        {
            if (editid.Value == "")
            {
                DataTable dtPINo = objPIHeader.GetPIHeaderAllDataByPI_No(StrCompId, StrBrandId, StrLocationId, txtPINo.Text);
                if (dtPINo.Rows.Count > 0)
                {
                    DisplayMessage("Purchase Inquiry No. Already Exits");
                    txtPINo.Text = "";
                    System.Web.UI.ScriptManager.GetCurrent(this).SetFocus(txtPINo);
                    return;
                }
            }
        }

        if (pnlTrans.Visible == true)
        {
            if (txtTransNo.Text == "")
            {
                DisplayMessage("Enter Transfer No.");
                System.Web.UI.ScriptManager.GetCurrent(this).SetFocus(txtTransNo);
                return;
            }
            if (txtTransFrom.Text == "Purchase Request")
            {
                txtTransFrom.Text = "PR";
            }
            if (txtTransFrom.Text == "Sales Inquiry")
            {
                txtTransFrom.Text = "SI";
            }
        }

        if (GvProduct.Rows.Count > 0)
        {

        }
        else
        {
            DisplayMessage("Add Atleast One Product");
            System.Web.UI.ScriptManager.GetCurrent(this).SetFocus(btnAddNewProduct);
            return;
        }


        int b = 0;
        if (editid.Value != "")
        {
            b = objPIHeader.UpdatePIHeader(StrCompId, StrBrandId, StrLocationId, editid.Value, txtPINo.Text, txtPIDate.Text, txtTransFrom.Text, hdnTransNo.Value, txtDescription.Text, "Pending", "", "", "", "", "", "True", DateTime.Now.ToString(), "True", Session["UserId"].ToString(), DateTime.Now.ToString());


            if (b != 0)
            {

                //Add PIDetail Record
                ObjPIDetail.DeletePIDetail(StrCompId, StrBrandId, StrLocationId, editid.Value);

                foreach (GridViewRow gvr in GvProduct.Rows)
                {
                    Label lblSerialNo = (Label)gvr.FindControl("lblSNo");
                    Label lblProductId = (Label)gvr.FindControl("lblgvProductId");
                    Label lblUnitId = (Label)gvr.FindControl("lblgvUnitId");
                    Label lblReqQty = (Label)gvr.FindControl("lblgvRequiredQty");
                    Label lblProductDescription = (Label)gvr.FindControl("lblgvProductDescription");

                    ObjPIDetail.InsertPIDetail(StrCompId, StrBrandId, StrLocationId, editid.Value, lblSerialNo.Text, lblProductId.Text, lblProductDescription.Text, lblUnitId.Text, lblReqQty.Text, "", "", "", "", "", "True", DateTime.Now.ToString(), "True", Session["UserId"].ToString(), DateTime.Now.ToString(), Session["UserId"].ToString(), DateTime.Now.ToString());
                }

                //DisplayMessage("Record Updated");
                txtSInquiryNo.Text = txtPINo.Text;
                txtSInquiryDate.Text = txtPIDate.Text;

                FillGrid();
                Reset();
                //btnList_Click(null, null);
                FillSupplierGroup();
                FillSupplier();
                PnlNewEdit.Visible = false;
                pnlSupplier.Visible = true;

                //Add Supplier

                DataTable dtSup = ObjPISupplier.GetAllPISupplierWithPI_No(StrCompId, StrBrandId, editid.Value);
                if (dtSup.Rows.Count > 0)
                {
                    string strSGroup = string.Empty;
                    strSGroup = dtSup.Rows[0]["Group_Id"].ToString();

                    if (strSGroup == "2")
                    {

                    }
                    else if (strSGroup != "2")
                    {
                        ddlSupplierGroup.SelectedValue = strSGroup;
                        ddlSupplierGroup_SelectedIndexChanged(sender, e);
                    }

                    for (int i = 0; i < ChkSupplier.Items.Count; i++)
                    {
                        for (int j = 0; j < dtSup.Rows.Count; j++)
                        {
                            if (ChkSupplier.Items[i].Value == dtSup.Rows[j]["Supplier_Id"].ToString())
                            {
                                ChkSupplier.Items[i].Selected = true;
                            }
                        }
                    }
                }
            }
            else
            {
                DisplayMessage("Record  Not Updated");
            }
        }
        else
        {
            b = objPIHeader.InsertPIHeader(StrCompId, StrBrandId, StrLocationId, txtPINo.Text, txtPIDate.Text, txtTransFrom.Text, hdnTransNo.Value, txtDescription.Text, "Pending", "", "", "", "", "", "True", DateTime.Now.ToString(), "True", Session["UserId"].ToString(), DateTime.Now.ToString(), Session["UserId"].ToString(), DateTime.Now.ToString());
            if (b != 0)
            {
                string strMaxId = string.Empty;
                DataTable dtMaxId = objPIHeader.GetMaxTransId(StrCompId, StrBrandId, StrLocationId);
                if (dtMaxId.Rows.Count > 0)
                {
                    strMaxId = dtMaxId.Rows[0][0].ToString();
                    editid.Value = strMaxId;
                }
                //Strart Update Status of sales inquiry  
                if (txtTransFrom.Text == "SI")
                {
                    string SIId = string.Empty;

                    ObjSelesInquiryHeader.UpdateSalesInquiryStatusbyPI(StrCompId.ToString(), StrBrandId.ToString(), StrLocationId.ToString(), hdnTransNo.Value.Trim(), "Send Inquiry To Supplier");
                }
                //End
                //Add PIDetail Record
                foreach (GridViewRow gvr in GvProduct.Rows)
                {
                    Label lblSerialNo = (Label)gvr.FindControl("lblSNo");
                    Label lblProductId = (Label)gvr.FindControl("lblgvProductId");
                    Label lblUnitId = (Label)gvr.FindControl("lblgvUnitId");
                    Label lblReqQty = (Label)gvr.FindControl("lblgvRequiredQty");
                    Label lblProductDescription = (Label)gvr.FindControl("lblgvProductDescription");

                    ObjPIDetail.InsertPIDetail(StrCompId, StrBrandId, StrLocationId, strMaxId, lblSerialNo.Text, lblProductId.Text, lblProductDescription.Text, lblUnitId.Text, lblReqQty.Text, "", "", "", "", "", "True", DateTime.Now.ToString(), "True", Session["UserId"].ToString(), DateTime.Now.ToString(), Session["UserId"].ToString(), DateTime.Now.ToString());
                }

                //DisplayMessage("Record Saved");
                txtSInquiryNo.Text = txtPINo.Text;
                txtSInquiryDate.Text = txtPIDate.Text;

                PnlNewEdit.Visible = false;
                pnlSupplier.Visible = true;
                FillSupplierGroup();
                FillSupplier();
                FillGrid();
                FillRequestGrid();
                Reset();
            }
            else
            {
                DisplayMessage("Record  Not Saved");
            }
        }
    }
    protected void IbtnRestore_Command(object sender, CommandEventArgs e)
    {
        editid.Value = e.CommandArgument.ToString();
        int b = 0;
        b = objPIHeader.DeletePIHeader(StrCompId, StrBrandId, StrLocationId, editid.Value, true.ToString(), StrUserId, DateTime.Now.ToString());
        if (b != 0)
        {
            DisplayMessage("Record Activated");
        }
        else
        {
            DisplayMessage("Record Not Activated");
        }
        FillGrid();
        FillGridBin();
        Reset();
    }

    #region Bin Section
    protected void btnBin_Click(object sender, EventArgs e)
    {
        pnlMenuBin.BackColor = System.Drawing.ColorTranslator.FromHtml("#ccddee");

        pnlMenuRequest.BackColor = System.Drawing.ColorTranslator.FromHtml("#90bde9");
        pnlMenuNew.BackColor = System.Drawing.ColorTranslator.FromHtml("#90bde9");
        pnlMenuList.BackColor = System.Drawing.ColorTranslator.FromHtml("#90bde9");
        PnlNewEdit.Visible = false;
        PnlBin.Visible = true;
        pnlRequest.Visible = false;
        PnlList.Visible = false;
        pnlSupplier.Visible = false;
        FillGridBin();
        txtValueBin.Text = "";
        txtValueBin.Focus();
    }
    protected void btnPRequest_Click(object sender, EventArgs e)
    {
        pnlMenuRequest.BackColor = System.Drawing.ColorTranslator.FromHtml("#ccddee");

        pnlMenuBin.BackColor = System.Drawing.ColorTranslator.FromHtml("#90bde9");
        pnlMenuNew.BackColor = System.Drawing.ColorTranslator.FromHtml("#90bde9");
        pnlMenuList.BackColor = System.Drawing.ColorTranslator.FromHtml("#90bde9");
        PnlNewEdit.Visible = false;
        PnlBin.Visible = false;
        pnlRequest.Visible = true;
        PnlList.Visible = false;
        pnlSupplier.Visible = false;
        FillGridBin();
    }
    protected void GvPurchaseInquiryBin_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        GvPurchaseInquiryBin.PageIndex = e.NewPageIndex;

        DataTable dt = (DataTable)Session["dtInactive"];
        GvPurchaseInquiryBin.DataSource = dt;
        GvPurchaseInquiryBin.DataBind();

        string temp = string.Empty;

        for (int i = 0; i < GvPurchaseInquiryBin.Rows.Count; i++)
        {
            HiddenField lblconid = (HiddenField)GvPurchaseInquiryBin.Rows[i].FindControl("hdnTransId");
            string[] split = lblSelectedRecord.Text.Split(',');

            for (int j = 0; j < lblSelectedRecord.Text.Split(',').Length; j++)
            {
                if (lblSelectedRecord.Text.Split(',')[j] != "")
                {
                    if (lblconid.Value.Trim().ToString() == lblSelectedRecord.Text.Split(',')[j].Trim().ToString())
                    {
                        ((CheckBox)GvPurchaseInquiryBin.Rows[i].FindControl("chkSelect")).Checked = true;
                    }
                }
            }
        }
    }
    protected void GvPurchaseInquiryBin_OnSorting(object sender, GridViewSortEventArgs e)
    {
        HDFSortbin.Value = HDFSortbin.Value == "ASC" ? "DESC" : "ASC";
        DataTable dt = new DataTable();
        dt = objPIHeader.GetPIHeaderAllFalse(StrCompId, StrBrandId, StrLocationId);
        DataView dv = new DataView(dt);
        string Query = "" + e.SortExpression + " " + HDFSortbin.Value + "";
        dv.Sort = Query;
        dt = dv.ToTable();
        Session["dtInactive"] = dt;
        GvPurchaseInquiryBin.DataSource = dt;
        GvPurchaseInquiryBin.DataBind();
        lblSelectedRecord.Text = "";
    }
    public void FillGridBin()
    {
        DataTable dt = new DataTable();
        dt = objPIHeader.GetPIHeaderAllFalse(StrCompId, StrBrandId, StrLocationId);
        GvPurchaseInquiryBin.DataSource = dt;
        GvPurchaseInquiryBin.DataBind();
        Session["dtPBrandBin"] = dt;
        Session["dtInactive"] = dt;
        lblTotalRecordsBin.Text = Resources.Attendance.Total_Records + " : " + dt.Rows.Count.ToString() + "";
        lblSelectedRecord.Text = "";
        if (dt.Rows.Count == 0)
        {
            ImgbtnSelectAll.Visible = false;
            imgBtnRestore.Visible = false;
        }
        else
        {
            ImgbtnSelectAll.Visible = true;
            imgBtnRestore.Visible = true;
        }
    }
    protected void btnbindBin_Click(object sender, EventArgs e)
    {
        string condition = string.Empty;
        if (ddlOptionBin.SelectedIndex != 0)
        {
            if (ddlOptionBin.SelectedIndex == 1)
            {
                condition = "convert(" + ddlFieldNameBin.SelectedValue + ",System.String)='" + txtValueBin.Text.Trim() + "'";
            }
            else if (ddlOptionBin.SelectedIndex == 2)
            {
                condition = "convert(" + ddlFieldNameBin.SelectedValue + ",System.String) like '%" + txtValueBin.Text.Trim() + "%'";
            }
            else
            {
                condition = "convert(" + ddlFieldNameBin.SelectedValue + ",System.String) Like '" + txtValueBin.Text.Trim() + "%'";
            }


            DataTable dtCust = (DataTable)Session["dtPBrandBin"];
            DataView view = new DataView(dtCust, condition, "", DataViewRowState.CurrentRows);
            Session["dtInactive"] = view.ToTable();
            lblTotalRecordsBin.Text = Resources.Attendance.Total_Records + " : " + view.ToTable().Rows.Count.ToString() + "";
            GvPurchaseInquiryBin.DataSource = view.ToTable();
            GvPurchaseInquiryBin.DataBind();
            lblSelectedRecord.Text = "";
            if (view.ToTable().Rows.Count == 0)
            {
                FillGridBin();
            }
            System.Web.UI.ScriptManager.GetCurrent(this).SetFocus(txtValueBin);
            btnRefreshBin.Focus();

        }
    }
    protected void btnRestoreSelected_Click(object sender, CommandEventArgs e)
    {
        int b = 0;
        DataTable dt = objPIHeader.GetPIHeaderAllFalse(StrCompId, StrBrandId, StrLocationId);

        if (GvPurchaseInquiryBin.Rows.Count != 0)
        {
            if (lblSelectedRecord.Text != "")
            {
                for (int j = 0; j < lblSelectedRecord.Text.Split(',').Length; j++)
                {
                    if (lblSelectedRecord.Text.Split(',')[j] != "")
                    {
                        //Msg = objTax.DeleteTaxMaster(lblSelectedRecord.Text.Split(',')[j].Trim().ToString(), true.ToString(), Session["UserId"].ToString(), DateTime.Now.ToString());
                        b = objPIHeader.DeletePIHeader(StrCompId, StrBrandId, StrLocationId, lblSelectedRecord.Text.Split(',')[j].Trim().ToString(), true.ToString(), Session["UserId"].ToString(), DateTime.Now.ToString());
                    }
                }
            }

            if (b != 0)
            {
                FillGrid();
                FillGridBin();

                lblSelectedRecord.Text = "";
                DisplayMessage("Record Activate");
            }
            else
            {
                int fleg = 0;
                foreach (GridViewRow Gvr in GvPurchaseInquiryBin.Rows)
                {
                    CheckBox chk = (CheckBox)Gvr.FindControl("chkSelect");
                    if (chk.Checked)
                    {
                        fleg = 1;
                    }
                    else
                    {
                        fleg = 0;
                    }
                }
                if (fleg == 0)
                {
                    DisplayMessage("Please Select Record");
                }
                else
                {
                    DisplayMessage("Record Not Activated");
                }
            }
        }
        System.Web.UI.ScriptManager.GetCurrent(this).SetFocus(txtValueBin);
    }
    protected void chkCurrent_CheckedChanged(object sender, EventArgs e)
    {
        CheckBox chkSelAll = ((CheckBox)GvPurchaseInquiryBin.HeaderRow.FindControl("chkCurrent"));
        for (int i = 0; i < GvPurchaseInquiryBin.Rows.Count; i++)
        {
            ((CheckBox)GvPurchaseInquiryBin.Rows[i].FindControl("chkSelect")).Checked = chkSelAll.Checked;
            if (chkSelAll.Checked)
            {
                if (!lblSelectedRecord.Text.Split(',').Contains(((HiddenField)(GvPurchaseInquiryBin.Rows[i].FindControl("hdnTransId"))).Value.Trim().ToString()))
                {
                    lblSelectedRecord.Text += ((HiddenField)(GvPurchaseInquiryBin.Rows[i].FindControl("hdnTransId"))).Value.Trim().ToString() + ",";
                }
            }
            else
            {
                string temp = string.Empty;
                string[] split = lblSelectedRecord.Text.Split(',');
                foreach (string item in split)
                {
                    if (item != ((HiddenField)(GvPurchaseInquiryBin.Rows[i].FindControl("hdnTransId"))).Value.Trim().ToString())
                    {
                        if (item != "")
                        {
                            temp += item + ",";
                        }
                    }
                }
                lblSelectedRecord.Text = temp;
            }
        }
    }
    protected void chkSelect_CheckedChanged(object sender, EventArgs e)
    {
        string empidlist = string.Empty;
        string temp = string.Empty;
        int index = ((GridViewRow)((CheckBox)sender).Parent.Parent).RowIndex;
        HiddenField lb = (HiddenField)GvPurchaseInquiryBin.Rows[index].FindControl("hdnTransId");
        if (((CheckBox)GvPurchaseInquiryBin.Rows[index].FindControl("chkSelect")).Checked)
        {
            empidlist += lb.Value.Trim().ToString() + ",";
            lblSelectedRecord.Text += empidlist;
        }
        else
        {
            empidlist += lb.Value.ToString().Trim();
            lblSelectedRecord.Text += empidlist;
            string[] split = lblSelectedRecord.Text.Split(',');
            foreach (string item in split)
            {
                if (item != empidlist)
                {
                    if (item != "")
                    {
                        if (item != "")
                        {
                            temp += item + ",";
                        }
                    }
                }
            }
            lblSelectedRecord.Text = temp;
        }
    }
    protected void btnRefreshBin_Click(object sender, EventArgs e)
    {
        FillGrid();
        FillGridBin();
        txtValueBin.Text = "";
        ddlOptionBin.SelectedIndex = 2;
        ddlFieldNameBin.SelectedIndex = 1;
        lblSelectedRecord.Text = "";
        System.Web.UI.ScriptManager.GetCurrent(this).SetFocus(txtValueBin);
    }
    protected void ImgbtnSelectAll_Click(object sender, ImageClickEventArgs e)
    {
        DataTable dtPbrand = (DataTable)Session["dtInactive"];

        if (ViewState["Select"] == null)
        {
            ViewState["Select"] = 1;
            foreach (DataRow dr in dtPbrand.Rows)
            {
                if (!lblSelectedRecord.Text.Split(',').Contains(dr["Trans_Id"]))
                {
                    lblSelectedRecord.Text += dr["Trans_Id"] + ",";
                }
            }
            for (int i = 0; i < GvPurchaseInquiryBin.Rows.Count; i++)
            {
                string[] split = lblSelectedRecord.Text.Split(',');
                HiddenField lblconid = (HiddenField)GvPurchaseInquiryBin.Rows[i].FindControl("hdnTransId");
                for (int j = 0; j < lblSelectedRecord.Text.Split(',').Length; j++)
                {
                    if (lblSelectedRecord.Text.Split(',')[j] != "")
                    {
                        if (lblconid.Value.Trim().ToString() == lblSelectedRecord.Text.Split(',')[j].Trim().ToString())
                        {
                            ((CheckBox)GvPurchaseInquiryBin.Rows[i].FindControl("chkSelect")).Checked = true;
                        }
                    }
                }
            }
        }
        else
        {
            lblSelectedRecord.Text = "";
            DataTable dtUnit1 = (DataTable)Session["dtInactive"];
            GvPurchaseInquiryBin.DataSource = dtUnit1;
            GvPurchaseInquiryBin.DataBind();
            ViewState["Select"] = null;
        }
    }
    protected void imgBtnRestore_Click(object sender, ImageClickEventArgs e)
    {
        int b = 0;
        if (lblSelectedRecord.Text != "")
        {
            for (int j = 0; j < lblSelectedRecord.Text.Split(',').Length; j++)
            {
                if (lblSelectedRecord.Text.Split(',')[j] != "")
                {
                    b = objPIHeader.DeletePIHeader(StrCompId, StrBrandId, StrLocationId, lblSelectedRecord.Text.Split(',')[j].Trim(), true.ToString(), Session["UserId"].ToString(), DateTime.Now.ToString());
                }
            }
        }

        if (b != 0)
        {
            FillGrid();
            FillGridBin();
            lblSelectedRecord.Text = "";
            ViewState["Select"] = null;
            DisplayMessage("Record Activate");
        }
        else
        {
            int fleg = 0;
            foreach (GridViewRow Gvr in GvPurchaseInquiryBin.Rows)
            {
                CheckBox chk = (CheckBox)Gvr.FindControl("chkSelect");
                if (chk.Checked)
                {
                    fleg = 1;
                }
                else
                {
                    fleg = 0;
                }
            }
            if (fleg == 0)
            {
                DisplayMessage("Please Select Record");
            }
            else
            {
                DisplayMessage("Record Not Activated");
            }
        }
    }
    #endregion

    #endregion

    #region User defind Funcation
    private void FillGrid()
    {
        DataTable dtBrand = objPIHeader.GetPIHeaderAllTrue(StrCompId, StrBrandId, StrLocationId);

        lblTotalRecords.Text = Resources.Attendance.Total_Records + " : " + dtBrand.Rows.Count + "";
        Session["dtCurrency"] = dtBrand;
        Session["dtFilter"] = dtBrand;
        if (dtBrand != null && dtBrand.Rows.Count > 0)
        {
            GvPurchaseInquiry.DataSource = dtBrand;
            GvPurchaseInquiry.DataBind();
        }
        else
        {
            GvPurchaseInquiry.DataSource = null;
            GvPurchaseInquiry.DataBind();
        }
        lblTotalRecords.Text = Resources.Attendance.Total_Records + " : " + dtBrand.Rows.Count.ToString() + "";
        AllPageCode();
    }
    protected string GetDate(string strDate)
    {
        string strNewDate = string.Empty;
        if (strDate != "")
        {
            strNewDate = DateTime.Parse(strDate).ToString("dd/MMM/yyyy");
        }
        else
        {
            strNewDate = "";
        }
        return strNewDate;
    }
    public void DisplayMessage(string str)
    {
        if (Session["lang"] == null)
        {
            Session["lang"] = "1";
        }
        if (Session["lang"].ToString() == "1")
        {
            ScriptManager.RegisterStartupScript(this, GetType(), "", "alert('" + str + "');", true);
        }
        else if (Session["lang"].ToString() == "2")
        {
            ScriptManager.RegisterStartupScript(this, GetType(), "", "alert('" + GetArebicMessage(str) + "');", true);
        }
    }
    public string GetArebicMessage(string EnglishMessage)
    {
        string ArebicMessage = string.Empty;
        DataTable dtres = (DataTable)Session["MessageDt"];
        if (dtres.Rows.Count != 0)
        {
            ArebicMessage = (new DataView(dtres, "Key='" + EnglishMessage + "'", "", DataViewRowState.CurrentRows).ToTable()).Rows[0]["Value"].ToString();
        }
        if (ArebicMessage == "")
        {
            ArebicMessage = EnglishMessage;
        }
        return ArebicMessage;
    }
    public void Reset()
    {

        txtPIDate.Text = DateTime.Now.ToString(ObjSysParam.GetSysParameterByParamName("Date_Format").Rows[0]["Param_Value"].ToString());
        txtPINo.ReadOnly = false;
        txtDescription.Text = "";

        GvProduct.DataSource = null;
        GvProduct.DataBind();

        pnlTrans.Visible = false;
        txtTransFrom.Text = "";
        txtTransNo.Text = "";
        hdnTransNo.Value = "0";

        btnNew.Text = Resources.Attendance.New;
        ddlFieldName.SelectedIndex = 1;
        ddlOption.SelectedIndex = 2;
        txtValue.Text = "";
        txtValueBin.Text = "";
        lblSelectedRecord.Text = "";
        System.Web.UI.ScriptManager.GetCurrent(this).SetFocus(txtValue);
        txtPINo.Text = GetDocumentNumber();
    }
    #endregion

    #region Auto Complete Function
    [System.Web.Services.WebMethodAttribute(), System.Web.Script.Services.ScriptMethodAttribute()]
    public static string[] GetCompletionList(string prefixText, int count, string contextKey)
    {
        Set_AddressMaster Address = new Set_AddressMaster();
        DataTable dt = Address.GetDistinctAddressName("1", prefixText);

        string[] str = new string[dt.Rows.Count];
        for (int i = 0; i < dt.Rows.Count; i++)
        {
            str[i] = dt.Rows[i]["Address_Name"].ToString();
        }
        return str;
    }
    #endregion

    #region Add Product Concept
    private void FillUnit()
    {
        DataTable dsUnit = null;
        dsUnit = UM.GetUnitMaster(StrCompId);
        if (dsUnit.Rows.Count > 0)
        {
            ddlUnit.DataSource = dsUnit;
            ddlUnit.DataTextField = "Unit_Name";
            ddlUnit.DataValueField = "Unit_Id";
            ddlUnit.DataBind();

            ddlUnit.Items.Add("--Select--");
            ddlUnit.SelectedValue = "--Select--";
        }
        else
        {
            ddlUnit.Items.Add("--Select--");
            ddlUnit.SelectedValue = "--Select--";
        }
    }
    [System.Web.Services.WebMethodAttribute(), System.Web.Script.Services.ScriptMethodAttribute()]
    public static string[] GetCompletionListProductName(string prefixText, int count, string contextKey)
    {
        Inv_ProductMaster PM = new Inv_ProductMaster();
        DataTable dt = PM.GetDistinctProductName("1", prefixText);
        string[] str = new string[dt.Rows.Count];
        if (dt.Rows.Count > 0)
        {
            for (int i = 0; i < dt.Rows.Count; i++)
            {
                str[i] = dt.Rows[i]["EProductName"].ToString();
            }
        }
        else
        {
            if (prefixText.Length > 2)
            {
                str = null;
            }
            else
            {
                dt = PM.GetProductMasterTrueAll("1");
                if (dt.Rows.Count > 0)
                {
                    str = new string[dt.Rows.Count];
                    for (int i = 0; i < dt.Rows.Count; i++)
                    {
                        str[i] = dt.Rows[i]["EProductName"].ToString();
                    }
                }
            }
        }
        return str;
    }
    protected void btnAddNewProduct_Click(object sender, EventArgs e)
    {
        pnlProduct1.Visible = true;
        pnlProduct2.Visible = true;
        FillUnit();
        txtRequestQty.Text = "1";
    }
    protected void txtProductName_TextChanged(object sender, EventArgs e)
    {
        if (txtProductName.Text != "")
        {
            DataTable dtProduct = objProductM.GetProductMasterTrueAll(StrCompId);
            dtProduct = new DataView(dtProduct, "EProductName ='" + txtProductName.Text + "'", "", DataViewRowState.CurrentRows).ToTable();
            if (dtProduct.Rows.Count > 0)
            {
               DataTable dt = CreateProductDataTable();
              for (int i = 0; i < GvProduct.Rows.Count; i++)
              {
              dt.Rows.Add(i);
            Label lblSNo = (Label)GvProduct.Rows[i].FindControl("lblSNo");
            Label lblgvProductId = (Label)GvProduct.Rows[i].FindControl("lblgvProductId");
            Label lblgvUnitId = (Label)GvProduct.Rows[i].FindControl("lblgvUnitId");
            Label lblgvReqQty = (Label)GvProduct.Rows[i].FindControl("lblgvRequiredQty");
            Label lblgvPDescription = (Label)GvProduct.Rows[i].FindControl("lblgvProductDescription");

            dt.Rows[i]["Serial_No"] = lblSNo.Text;
            dt.Rows[i]["Product_Id"] = lblgvProductId.Text;
            dt.Rows[i]["UnitId"] = lblgvUnitId.Text;
            dt.Rows[i]["ReqQty"] = lblgvReqQty.Text;
            dt.Rows[i]["ProductDescription"] = lblgvPDescription.Text;
        }
        for (int i = 0; i < dt.Rows.Count; i++)
        {
            if (dt.Rows[i]["Product_Id"].ToString() == dtProduct.Rows[0]["ProductId"].ToString())
            {

                DisplayMessage("Product is already exists!");
                txtProductName.Text = "";
                txtProductName.Focus();
                return;
            }
        }
           string strUnitId = dtProduct.Rows[0]["UnitId"].ToString();
                if (strUnitId != "0" && strUnitId != "")
                {
                    ddlUnit.SelectedValue = strUnitId;
                }
                else
                {
                    FillUnit();
                }


                txtPDescription.Text = dtProduct.Rows[0]["Description"].ToString();
                hdnNewProductId.Value = dtProduct.Rows[0]["ProductId"].ToString();
                txtPDesc.Visible = false;
                pnlPDescription.Visible = true;
            }
            else
            {
                FillUnit();
                
                txtPDescription.Text = "";
                hdnNewProductId.Value = "0";
                txtPDesc.Visible = true;
                pnlPDescription.Visible = false;
            }
            txtRequestQty.Text = "1";
        }
        else
        {
            DisplayMessage("Enter Product Name");
            System.Web.UI.ScriptManager.GetCurrent(this).SetFocus(txtProductName);
        }
    }
    protected void btnProductSave_Click(object sender, EventArgs e)
    {
        if (txtRequestQty.Text == "")
        {
            DisplayMessage("Enter Quantity");
            txtRequestQty.Text = "1";
            txtRequestQty.Focus();
            return;
        }

        if (txtProductName.Text != "")
        {
            string strA = "0";
            foreach (GridViewRow gve in GvProduct.Rows)
            {
                Label lblgvProductName = (Label)gve.FindControl("lblgvProductName");
                if (txtProductName.Text == lblgvProductName.Text)
                {
                    strA = "1";
                }
            }

            if (hdnNewProductId.Value == "0")
            {
                if (txtProductName.Text != "")
                {
                    DataTable dt = objProductM.GetProductMasterTrueAll(StrCompId);
                    dt = new DataView(dt, "EProductName ='" + txtProductName.Text + "'", "", DataViewRowState.CurrentRows).ToTable();
                    if (dt.Rows.Count > 0)
                    {
                        hdnNewProductId.Value = dt.Rows[0]["ProductId"].ToString();
                    }
                    else
                    {
                        hdnNewProductId.Value = "0";//here we insert the description
                    }
                }
            }

            if (ddlUnit.Visible == true)
            {
                if (ddlUnit.SelectedValue != "--Select--")
                {
                    hdnNewUnitId.Value = ddlUnit.SelectedValue;
                }
                else
                {
                    DisplayMessage("Select Unit");
                    System.Web.UI.ScriptManager.GetCurrent(this).SetFocus(ddlUnit);
                    return;
                }
            }
            else if (txtUnit.Visible == true)
            {
                if (txtUnit.Text != "")
                {
                    UM.InsertUnitMaster(StrCompId, txtUnit.Text, txtUnit.Text, "", "0", "1", "", "", "", "", "", "True", DateTime.Now.ToString(), "True", Session["UserId"].ToString(), DateTime.Now.ToString(), Session["UserId"].ToString(), DateTime.Now.ToString());
                    DataTable dtMaxId = UM.GetMaxUnitId(StrCompId);
                    if (dtMaxId.Rows.Count > 0)
                    {
                        hdnNewUnitId.Value = dtMaxId.Rows[0][0].ToString();
                    }
                }
                else
                {
                    DisplayMessage("Enter Unit Name");
                    System.Web.UI.ScriptManager.GetCurrent(this).SetFocus(txtUnit);
                    return;
                }
            }

            if (hdnProductId.Value == "")
            {
                //if (strA == "0")
                //{
                  FillProductChidGird("Save");
                    System.Web.UI.ScriptManager.GetCurrent(this).SetFocus(btnAddNewProduct);
                    pnlProduct1.Visible = false;
                    pnlProduct2.Visible = false;
                //}
                //else
                //{
                //    txtProductName.Text = "";
                //    DisplayMessage("Product Name Already Exists");
                //    System.Web.UI.ScriptManager.GetCurrent(this).SetFocus(txtProductName);
                //}
            }
            else
            {
                if (txtProductName.Text == hdnProductName.Value)
                {
                    FillProductChidGird("Edit");
                    System.Web.UI.ScriptManager.GetCurrent(this).SetFocus(btnAddNewProduct);
                    pnlProduct1.Visible = false;
                    pnlProduct2.Visible = false;
                }
                else
                {
                    //if (strA == "0")
                    //{
                     FillProductChidGird("Edit");
                        System.Web.UI.ScriptManager.GetCurrent(this).SetFocus(btnAddNewProduct);
                        pnlProduct1.Visible = false;
                        pnlProduct2.Visible = false;
                    //}
                    //else
                    //{
                    //    txtProductName.Text = "";
                    //    DisplayMessage("Product Name Already Exists");
                    //    System.Web.UI.ScriptManager.GetCurrent(this).SetFocus(txtProductName);
                    //}
                }
            }
        }
        else
        {
            DisplayMessage("Enter Product Name");
            System.Web.UI.ScriptManager.GetCurrent(this).SetFocus(txtProductName);
        }
        txtPDesc.Text = "";
        txtProductName.Focus();
        txtRequestQty.Text = "1";
    }
    protected void btnProductCancel_Click(object sender, EventArgs e)
    {
        ResetProduct();
    }
    protected void btnClosePanel_Click(object sender, EventArgs e)
    {
        pnlProduct1.Visible = false;
        pnlProduct2.Visible = false;
    }
    public void ResetProduct()
    {
        txtProductName.Text = "";
        FillUnit();
        txtUnit.Text = "";
        ddlUnit.Visible = true;
        txtUnit.Visible = false;
        txtRequestQty.Text = "1";
        txtPDescription.Text = "";
        hdnProductId.Value = "";
        hdnProductName.Value = "";
        hdnNewProductId.Value = "0";
    }
    public DataTable CreateProductDataTable()
    {
        DataTable dt = new DataTable();
        dt.Columns.Add("Serial_No");
        dt.Columns.Add("Product_Id");
        dt.Columns.Add("UnitId");
        dt.Columns.Add("ReqQty");
        dt.Columns.Add("ProductDescription");
        return dt;
    }
    public DataTable FillProductDataTabel()
    {
        string strNewSNo = string.Empty;
        DataTable dt = CreateProductDataTable();
        if (GvProduct.Rows.Count > 0)
        {
            for (int i = 0; i < GvProduct.Rows.Count + 1; i++)
            {
                if (dt.Rows.Count != GvProduct.Rows.Count)
                {
                    dt.Rows.Add(i);
                    Label lblgvSNo = (Label)GvProduct.Rows[i].FindControl("lblSNo");
                    Label lblgvProductId = (Label)GvProduct.Rows[i].FindControl("lblgvProductId");
                    Label lblgvUnitId = (Label)GvProduct.Rows[i].FindControl("lblgvUnitId");
                    Label lblgvReqQty = (Label)GvProduct.Rows[i].FindControl("lblgvRequiredQty");
                    Label lblgvPDescription = (Label)GvProduct.Rows[i].FindControl("lblgvProductDescription");

                    dt.Rows[i]["Serial_No"] = lblgvSNo.Text;
                    strNewSNo = lblgvSNo.Text;
                    dt.Rows[i]["Product_Id"] = lblgvProductId.Text;
                    dt.Rows[i]["UnitId"] = lblgvUnitId.Text;
                    dt.Rows[i]["ReqQty"] = lblgvReqQty.Text;
                    dt.Rows[i]["ProductDescription"] = lblgvPDescription.Text;
                }
                else
                {
                    dt.Rows.Add(i);
                    dt.Rows[i]["Serial_No"] = (float.Parse(strNewSNo) + 1).ToString();
                    dt.Rows[i]["Product_Id"] = hdnNewProductId.Value;
                    dt.Rows[i]["UnitId"] = hdnNewUnitId.Value;
                    dt.Rows[i]["ReqQty"] = txtRequestQty.Text;
                    if (hdnNewProductId.Value == "0")
                    {

                        dt.Rows[i]["ProductDescription"] = txtPDesc.Text;
                    }
                    else
                    {
                        dt.Rows[i]["ProductDescription"] = txtPDescription.Text;
                  
                    }//here we check th description
                }
            }
        }
        else
        {
            dt.Rows.Add(0);
            dt.Rows[0]["Serial_No"] = "1";
            dt.Rows[0]["Product_Id"] = hdnNewProductId.Value;
            dt.Rows[0]["UnitId"] = hdnNewUnitId.Value;
            dt.Rows[0]["ReqQty"] = txtRequestQty.Text;
            if (hdnNewProductId.Value == "0")
            {
                dt.Rows[0]["ProductDescription"] = txtPDesc.Text;
            }
            else
            {
                dt.Rows[0]["ProductDescription"] = txtPDescription.Text;
          
            }
        }
        if (dt.Rows.Count > 0)
        {
            GvProduct.DataSource = dt;
            GvProduct.DataBind();

            foreach (GridViewRow gvr in GvProduct.Rows)
            {
                Label lblProductId = (Label)gvr.FindControl("lblgvProductId");
                Label lblProductName = (Label)gvr.FindControl("lblgvProductName");
                Label lblProductDesc = (Label)gvr.FindControl("lblgvProductDescription");

                if (lblProductId.Text == "0")
                {
                    if (lblProductDesc.Text != "")
                    {
                        lblProductName.Text = lblProductDesc.Text;
                    }
                }
            }
        }
        return dt;
    }
    public DataTable FillProductDataTabelDelete()
    {
        DataTable dt = CreateProductDataTable();
        for (int i = 0; i < GvProduct.Rows.Count; i++)
        {
            dt.Rows.Add(i);
            Label lblgvSNo = (Label)GvProduct.Rows[i].FindControl("lblSNo");
            Label lblgvProductId = (Label)GvProduct.Rows[i].FindControl("lblgvProductId");
            Label lblgvUnitId = (Label)GvProduct.Rows[i].FindControl("lblgvUnitId");
            Label lblgvReqQty = (Label)GvProduct.Rows[i].FindControl("lblgvRequiredQty");
            Label lblgvPDescription = (Label)GvProduct.Rows[i].FindControl("lblgvProductDescription");

            dt.Rows[i]["Serial_No"] = lblgvSNo.Text;
            dt.Rows[i]["Product_Id"] = lblgvProductId.Text;
            dt.Rows[i]["UnitId"] = lblgvUnitId.Text;
            dt.Rows[i]["ReqQty"] = lblgvReqQty.Text;
            dt.Rows[i]["ProductDescription"] = lblgvPDescription.Text;
        }

        DataView dv = new DataView(dt);
        dv.RowFilter = "Serial_No<>'" + hdnProductId.Value + "'";
        dt = (DataTable)dv.ToTable();
        return dt;
    }
    protected void imgBtnProductEdit_Command(object sender, CommandEventArgs e)
    {
        hdnProductId.Value = e.CommandArgument.ToString();
        FillProductDataTabelEdit();
        pnlProduct1.Visible = true;
        pnlProduct2.Visible = true;
    }
    public DataTable FillProductDataTabelEdit()
    {
        DataTable dt = CreateProductDataTable();

        for (int i = 0; i < GvProduct.Rows.Count; i++)
        {
            dt.Rows.Add(i);
            Label lblSNo = (Label)GvProduct.Rows[i].FindControl("lblSNo");
            Label lblgvProductId = (Label)GvProduct.Rows[i].FindControl("lblgvProductId");
            Label lblgvUnitId = (Label)GvProduct.Rows[i].FindControl("lblgvUnitId");
            Label lblgvReqQty = (Label)GvProduct.Rows[i].FindControl("lblgvRequiredQty");
            Label lblgvPDescription = (Label)GvProduct.Rows[i].FindControl("lblgvProductDescription");

            dt.Rows[i]["Serial_No"] = lblSNo.Text;
            dt.Rows[i]["Product_Id"] = lblgvProductId.Text;
            dt.Rows[i]["UnitId"] = lblgvUnitId.Text;
            dt.Rows[i]["ReqQty"] = lblgvReqQty.Text;
            dt.Rows[i]["ProductDescription"] = lblgvPDescription.Text;
        }

        DataView dv = new DataView(dt);
        dv.RowFilter = "Serial_No='" + hdnProductId.Value + "'";
        dt = (DataTable)dv.ToTable();
        if (dt.Rows.Count != 0)
        {
            txtProductName.Text = GetProductName(dt.Rows[0]["Product_Id"].ToString());
            FillUnit();
            ddlUnit.SelectedValue = dt.Rows[0]["UnitId"].ToString();
            txtRequestQty.Text = dt.Rows[0]["ReqQty"].ToString();
            if (txtProductName.Text != "")
            {
                txtPDescription.Text = dt.Rows[0]["ProductDescription"].ToString();
                pnlPDescription.Visible = true;
                txtPDesc.Visible = false;
            }
            else
            {
                txtProductName.Text = dt.Rows[0]["ProductDescription"].ToString();
                txtPDesc.Text = dt.Rows[0]["ProductDescription"].ToString();
                pnlPDescription.Visible = false;
                txtPDesc.Visible = true;
            }

            hdnProductName.Value = GetProductName(dt.Rows[0]["Product_Id"].ToString());
        }
        return dt;
    }
    protected void imgBtnProductDelete_Command(object sender, CommandEventArgs e)
    {
        hdnProductId.Value = e.CommandArgument.ToString();
        FillProductChidGird("Del");
    }
    public void FillProductChidGird(string CommandName)
    {
        DataTable dt = new DataTable();
        if (CommandName.ToString() == "Del")
        {
            dt = FillProductDataTabelDelete();
        }
        else if (CommandName.ToString() == "Edit")
        {
            dt = FillProductDataTableUpdate();
        }
        else
        {
            dt = FillProductDataTabel();
        }
        GvProduct.DataSource = dt;
        GvProduct.DataBind();

        foreach (GridViewRow gvr in GvProduct.Rows)
        {
            Label lblProductId = (Label)gvr.FindControl("lblgvProductId");
            Label lblProductName = (Label)gvr.FindControl("lblgvProductName");
            Label lblProductDesc = (Label)gvr.FindControl("lblgvProductDescription");

            if (lblProductId.Text == "0")
            {
                if (lblProductDesc.Text != "")
                {
                    lblProductName.Text = lblProductDesc.Text;
                }
            }
        }

        ResetProduct();
        AllPageCode();
    }
    public DataTable FillProductDataTableUpdate()
    {
        DataTable dt = CreateProductDataTable();
        for (int i = 0; i < GvProduct.Rows.Count; i++)
        {
            dt.Rows.Add(i);
            Label lblSNo = (Label)GvProduct.Rows[i].FindControl("lblSNo");
            Label lblgvProductId = (Label)GvProduct.Rows[i].FindControl("lblgvProductId");
            Label lblgvUnitId = (Label)GvProduct.Rows[i].FindControl("lblgvUnitId");
            Label lblgvReqQty = (Label)GvProduct.Rows[i].FindControl("lblgvRequiredQty");
            Label lblgvPDescription = (Label)GvProduct.Rows[i].FindControl("lblgvProductDescription");

            dt.Rows[i]["Serial_No"] = lblSNo.Text;
            dt.Rows[i]["Product_Id"] = lblgvProductId.Text;
            dt.Rows[i]["UnitId"] = lblgvUnitId.Text;
            dt.Rows[i]["ReqQty"] = lblgvReqQty.Text;
            dt.Rows[i]["ProductDescription"] = lblgvPDescription.Text;
        }
        for (int i = 0; i < dt.Rows.Count; i++)
        {
            if (hdnProductId.Value == dt.Rows[i]["Serial_No"].ToString())
            {
                dt.Rows[i]["Product_Id"] = hdnNewProductId.Value;
                dt.Rows[i]["UnitId"] = hdnNewUnitId.Value;
                dt.Rows[i]["ReqQty"] = txtRequestQty.Text;
                if (hdnNewProductId.Value == "0")
                {
                    dt.Rows[i]["ProductDescription"] = txtPDesc.Text;
              
                }
                else
                {
                    dt.Rows[i]["ProductDescription"] = txtPDescription.Text;
                }
            }
        }
        return dt;
    }
    protected string GetProductName(string strProductId)
    {
        string strProductName = string.Empty;
        if (strProductId != "0" && strProductId != "")
        {
            DataTable dtPName = objProductM.GetProductMasterById(StrCompId, strProductId);
            if (dtPName.Rows.Count > 0)
            {
                strProductName = dtPName.Rows[0]["EProductName"].ToString();
            }
        }
        else
        {
            strProductName = "";
        }
        return strProductName;
    }
    protected string GetUnitName(string strUnitId)
    {
        string strUnitName = string.Empty;
        if (strUnitId != "0" && strUnitId != "")
        {
            DataTable dtUName = UM.GetUnitMasterById(StrCompId, strUnitId);
            if (dtUName.Rows.Count > 0)
            {
                strUnitName = dtUName.Rows[0]["Unit_Name"].ToString();
            }
        }
        else
        {
            strUnitName = "";
        }
        return strUnitName;
    }
    #endregion

    #region Add Request Section
    protected void GvPurchaseRequest_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        GvPurchaseRequest.PageIndex = e.NewPageIndex;
        DataTable dt = (DataTable)Session["dtPRequest"];
        GvPurchaseRequest.DataSource = dt;
        GvPurchaseRequest.DataBind();
        AllPageCode();
    }
    protected void GvPurchaseRequest_Sorting(object sender, GridViewSortEventArgs e)
    {
        DataTable dt = (DataTable)Session["dtPRequest"];
        string sortdir = "DESC";
        if (ViewState["PRSortDir"] != null)
        {
            sortdir = ViewState["PRSortDir"].ToString();
            if (sortdir == "ASC")
            {
                e.SortDirection = SortDirection.Descending;
                ViewState["PRSortDir"] = "DESC";
            }
            else
            {
                e.SortDirection = SortDirection.Ascending;
                ViewState["PRSortDir"] = "ASC";
            }
        }
        else
        {
            ViewState["PRSortDir"] = "DESC";
        }

        dt = (new DataView(dt, "", e.SortExpression + " " + ViewState["PRSortDir"].ToString(), DataViewRowState.CurrentRows)).ToTable();
        Session["dtPRequest"] = dt;
        GvPurchaseRequest.DataSource = dt;
        GvPurchaseRequest.DataBind();
        AllPageCode();
    }
    private void FillRequestGrid()
    {
        DataTable dtPRequest = objPRHeader.GetPurchaseRequestAndInquiryData(StrCompId, StrBrandId, StrLocationId);
        DataTable dtSI = new DataView(ObjSelesInquiryHeader.GetSIHeaderAll(StrCompId.ToString(), StrBrandId.ToString(), StrLocationId), "Field1='FWInPur'", "", DataViewRowState.CurrentRows).ToTable();
        dtPRequest.Columns.Add("RequestType");
        for (int i = 0; i < dtSI.Rows.Count; i++)
        {
            dtPRequest.Rows.Add();
            dtPRequest.Rows[dtPRequest.Rows.Count - 1]["Trans_Id"] = dtSI.Rows[i]["SInquiryID"].ToString();
            dtPRequest.Rows[dtPRequest.Rows.Count - 1]["RequestNo"] = dtSI.Rows[i]["SInquiryNo"].ToString();
            dtPRequest.Rows[dtPRequest.Rows.Count - 1]["RequestDate"] = dtSI.Rows[i]["IDate"].ToString();
            dtPRequest.Rows[dtPRequest.Rows.Count - 1]["RequestType"] = "Sales Inquiry";
        }
        for (int i = 0; i < dtPRequest.Rows.Count; i++)
        {
            if (dtPRequest.Rows[i]["RequestType"].ToString() == "")
            {
                dtPRequest.Rows[i]["RequestType"] = "Purchase Request";
            }
        }

        Session["dtPRequest"] = dtPRequest;
        if (dtPRequest != null && dtPRequest.Rows.Count > 0)
        {
            GvPurchaseRequest.DataSource = dtPRequest;
            GvPurchaseRequest.DataBind();
        }
        else
        {
            GvPurchaseRequest.DataSource = null;
            GvPurchaseRequest.DataBind();
        }


        AllPageCode();
    }
    protected void btnPREdit_Command(object sender, CommandEventArgs e)
    {
        ImageButton Imgbtn = (ImageButton)sender;
        GridViewRow row = (GridViewRow)Imgbtn.NamingContainer;
        Label li = (Label)row.FindControl("lblgvRequestType");
        DataTable dtPRequest = new DataTable();
        string strRequestId = e.CommandArgument.ToString();
        if (li.Text.Trim() == "Sales Inquiry")
        {
            dtPRequest = ObjSelesInquiryHeader.GetSIHeaderAllBySInquiryId(StrCompId.ToString(), StrBrandId.ToString(), StrLocationId.ToString(), strRequestId);
            pnlTrans.Visible = true;
            txtTransFrom.Text = Resources.Attendance.Sales_Inquiry;
            txtTransNo.Text = dtPRequest.Rows[0]["SInquiryNo"].ToString();
            hdnTransNo.Value = dtPRequest.Rows[0]["SInquiryId"].ToString();
            DataTable dtRequestProduct = ObjSalesInquiryDetail.GetSIDetailBySInquiryId(StrCompId, StrBrandId, StrLocationId, strRequestId);
            dtRequestProduct.Columns["Quantity"].ColumnName = "ReqQty";
            if (dtRequestProduct.Rows.Count > 0)
            {
                GvProduct.DataSource = dtRequestProduct;
                GvProduct.DataBind();

                foreach (GridViewRow gvr in GvProduct.Rows)
                {
                    Label lblProductId = (Label)gvr.FindControl("lblgvProductId");
                    Label lblProductName = (Label)gvr.FindControl("lblgvProductName");
                    Label lblProductDesc = (Label)gvr.FindControl("lblgvProductDescription");

                    if (lblProductId.Text == "0")
                    {
                        if (lblProductDesc.Text != "")
                        {
                            lblProductName.Text = lblProductDesc.Text;
                        }
                    }
                }
            }
        }
        else
        {

            dtPRequest = objPRHeader.GetPurchaseRequestTrueAllByReqId(StrCompId, StrBrandId, StrLocationId, strRequestId);

            pnlTrans.Visible = true;
            txtTransFrom.Text = Resources.Attendance.Purchase_Request;
            txtTransNo.Text = dtPRequest.Rows[0]["RequestNo"].ToString();
            hdnTransNo.Value = dtPRequest.Rows[0]["Trans_Id"].ToString();
            //Add Product Section For Edit
            DataTable dtRequestProduct = ObjPRDetail.GetPurchaseRequestDetailbyRequestId(StrCompId, StrBrandId, StrLocationId, strRequestId);
            if (dtRequestProduct.Rows.Count > 0)
            {
                GvProduct.DataSource = dtRequestProduct;
                GvProduct.DataBind();

                foreach (GridViewRow gvr in GvProduct.Rows)
                {
                    Label lblProductId = (Label)gvr.FindControl("lblgvProductId");
                    Label lblProductName = (Label)gvr.FindControl("lblgvProductName");
                    Label lblProductDesc = (Label)gvr.FindControl("lblgvProductDescription");

                    if (lblProductId.Text == "0")
                    {
                        if (lblProductDesc.Text != "")
                        {
                            lblProductName.Text = lblProductDesc.Text;
                        }
                    }
                }
            }
        }

        btnNew_Click(null, null);
        AllPageCode();
        System.Web.UI.ScriptManager.GetCurrent(this).SetFocus(txtPINo);
    }
    #endregion

    #region Add Supplier
    public void FillSupplier()
    {
        DataTable dt = ObjPISupplier.GetAllSupplier(StrCompId, StrBrandId);
        try
        {
            ChkSupplier.DataSource = dt;
            ChkSupplier.DataTextField = "Contact_Name";
            ChkSupplier.DataValueField = "Contact_Id";
            ChkSupplier.DataBind();
        }
        catch
        {

        }
    }
    private void FillSupplierGroup()
    {
        DataTable dsSupplierG = null;
        dsSupplierG = objGroupMaster.GetGroupMasterByParentId(StrCompId, "2");
        if (dsSupplierG.Rows.Count > 0)
        {
            ddlSupplierGroup.DataSource = dsSupplierG;
            ddlSupplierGroup.DataTextField = "Group_Name";
            ddlSupplierGroup.DataValueField = "Group_Id";
            ddlSupplierGroup.DataBind();

            ddlSupplierGroup.Items.Add("All Supplier");
            ddlSupplierGroup.SelectedValue = "All Supplier";
        }
        else
        {
            ddlSupplierGroup.Items.Add("--Select--");
            ddlSupplierGroup.SelectedValue = "--Select--";
        }
    }
    protected void ddlSupplierGroup_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (ddlSupplierGroup.SelectedValue != "All Supplier")
        {
            DataTable dtSG = ObjPISupplier.GetAllSupplierByGroupId(StrCompId, StrBrandId, ddlSupplierGroup.SelectedValue);
            if (dtSG.Rows.Count > 0)
            {
                ChkSupplier.DataSource = dtSG;
                ChkSupplier.DataTextField = "Contact_Name";
                ChkSupplier.DataValueField = "Contact_Id";
                ChkSupplier.DataBind();
            }
            else
            {
                ChkSupplier.DataSource = null;
                ChkSupplier.DataBind();
            }
        }
        else if (ddlSupplierGroup.SelectedValue == "All Supplier")
        {
            FillSupplier();
        }
    }
    protected void btnSaveSupplier_Click(object sender, EventArgs e)
    {
        int b = 0;
        string strSupplierGroup = string.Empty;

        if (ddlSupplierGroup.SelectedValue != "--Select--")
        {
            if (ddlSupplierGroup.SelectedValue != "All Supplier")
            {
                strSupplierGroup = ddlSupplierGroup.SelectedValue;
            }
            else if (ddlSupplierGroup.SelectedValue == "All Supplier")
            {
                strSupplierGroup = "2";
            }
        }

        if (editid.Value != "0" && editid.Value != "")
        {
            ObjPISupplier.DeletePurchaseInquirySupplier(StrCompId, editid.Value);
            foreach (ListItem li in ChkSupplier.Items)
            {
                if (li.Selected == true)
                {
                    b = ObjPISupplier.InsertPurchaseInquirySupplier(StrCompId, StrBrandId, editid.Value, li.Value, strSupplierGroup, "", "", "", "", "", "True", DateTime.Now.ToString(), "True", Session["UserId"].ToString(), DateTime.Now.ToString(), Session["UserId"].ToString(), DateTime.Now.ToString());
                }
            }
            if (b != 0)
            {
                FillSupplier();
                FillSupplierGroup();
                txtSInquiryNo.Text = "";
                txtSInquiryDate.Text = "";
                DisplayMessage("Record Saved");
                btnList_Click(null, null);
                pnlSupplier.Visible = false;
                editid.Value = "";
            }
            else
            {
                FillSupplier();
                FillSupplierGroup();
                txtSInquiryNo.Text = "";
                txtSInquiryDate.Text = "";
                DisplayMessage("Record Saved");
                btnList_Click(null, null);
                pnlSupplier.Visible = false;
                editid.Value = "";
            }
        }
    }
    protected void btnResetSupplier_Click(object sender, EventArgs e)
    {
        FillSupplier();
        FillSupplierGroup();
    }
    protected void btnCancelSupplier_Click(object sender, EventArgs e)
    {
        btnList_Click(null, null);
        editid.Value = "";
        pnlSupplier.Visible = false;
    }
    #endregion

    #region AllPageCode
    public void AllPageCode()
    {
        Page.Title = ObjSysParam.GetSysTitle();
        StrBrandId = Session["BrandId"].ToString();
        StrLocationId = Session["LocId"].ToString();
        StrUserId = Session["UserId"].ToString();
        StrCompId = Session["CompId"].ToString();
        Session["AccordianId"] = "12";
        Session["HeaderText"] = "Purchase";
        DataTable dtAllPageCode = cmn.GetAllPagePermission(StrUserId.ToString(), "12", "49");
        if (dtAllPageCode.Rows.Count != 0)
        {
            if (dtAllPageCode.Rows[0][0].ToString() == "SuperAdmin")
            {
                btnPISave.Visible = true;
                btnAddNewProduct.Visible = true;
                btnSaveSupplier.Visible = true;
                btnProductSave.Visible = true;
                foreach (GridViewRow Row in GvPurchaseInquiry.Rows)
                {
                    ((ImageButton)Row.FindControl("btnEdit")).Visible = true;
                    ((ImageButton)Row.FindControl("IbtnDelete")).Visible = true;
                }
                foreach (GridViewRow Row in GvProduct.Rows)
                {
                    ((ImageButton)Row.FindControl("imgBtnProductEdit")).Visible = true;
                    ((ImageButton)Row.FindControl("imgBtnProductDelete")).Visible = true;
                }
                imgBtnRestore.Visible = true;
                ImgbtnSelectAll.Visible = true;
            }
            else
            {
                foreach (DataRow DtRow in dtAllPageCode.Rows)
                {
                    if (Convert.ToBoolean(DtRow["Op_Add"].ToString()))
                    {
                        btnPISave.Visible = true;
                        btnProductSave.Visible = true;
                        btnAddNewProduct.Visible = true;
                        btnSaveSupplier.Visible = true;
                    }
                    foreach (GridViewRow Row in GvPurchaseInquiry.Rows)
                    {
                        if (Convert.ToBoolean(DtRow["Op_Edit"].ToString()))
                        {
                            ((ImageButton)Row.FindControl("btnEdit")).Visible = true;
                        }
                        if (Convert.ToBoolean(DtRow["Op_Delete"].ToString()))
                        {
                            ((ImageButton)Row.FindControl("IbtnDelete")).Visible = true;
                        }
                    }
                    foreach (GridViewRow Row in GvProduct.Rows)
                    {
                        if (Convert.ToBoolean(DtRow["Op_Edit"].ToString()))
                        {
                            ((ImageButton)Row.FindControl("imgBtnProductEdit")).Visible = true;
                        }
                        if (Convert.ToBoolean(DtRow["Op_Delete"].ToString()))
                        {
                            ((ImageButton)Row.FindControl("imgBtnProductDelete")).Visible = true;
                        }
                    }

                    if (Convert.ToBoolean(DtRow["Op_Restore"].ToString()))
                    {
                        imgBtnRestore.Visible = true;
                        ImgbtnSelectAll.Visible = true;
                    }
                    if (Convert.ToBoolean(DtRow["Op_View"].ToString()))
                    {

                    }
                    if (Convert.ToBoolean(DtRow["Op_Print"].ToString()))
                    {

                    }
                    if (Convert.ToBoolean(DtRow["Op_Download"].ToString()))
                    {

                    }
                    if (Convert.ToBoolean(DtRow["Op_Upload"].ToString()))
                    {

                    }
                }

            }


        }


    }
    #endregion
    public string GetDocumentNumber()
    {
        string DocumentNo = string.Empty;

        DataTable Dt = objDocNo.GetDocumentNumberAll(StrCompId, "12", "49");

        if (Dt.Rows.Count > 0)
        {
            if (Dt.Rows[0]["Prefix"].ToString() != "")
            {
                DocumentNo += Dt.Rows[0]["Prefix"].ToString();
            }



            if (Convert.ToBoolean(Dt.Rows[0]["CompId"].ToString()))
            {
                DocumentNo += StrCompId;
            }

            if (Convert.ToBoolean(Dt.Rows[0]["BrandId"].ToString()))
            {
                DocumentNo += StrBrandId;
            }

            if (Convert.ToBoolean(Dt.Rows[0]["LocationId"].ToString()))
            {
                DocumentNo += StrLocationId;
            }

            if (Convert.ToBoolean(Dt.Rows[0]["DeptId"].ToString()))
            {
                DocumentNo += (string)Session["SessionDepId"];
            }

            if (Convert.ToBoolean(Dt.Rows[0]["EmpId"].ToString()))
            {

                DataTable Dtuser = ObjUser.GetUserMasterByUserId(Session["UserId"].ToString());
                DocumentNo += Dtuser.Rows[0]["Emp_Id"].ToString();

            }

            if (Convert.ToBoolean(Dt.Rows[0]["Year"].ToString()))
            {
                DocumentNo += DateTime.Now.Year.ToString();
            }

            if (Convert.ToBoolean(Dt.Rows[0]["Month"].ToString()))
            {
                DocumentNo += DateTime.Now.Month.ToString();
            }

            if (Convert.ToBoolean(Dt.Rows[0]["Day"].ToString()))
            {
                DocumentNo += DateTime.Now.Day.ToString();
            }

            if (Dt.Rows[0]["Suffix"].ToString() != "")
            {
                DocumentNo += Dt.Rows[0]["Suffix"].ToString();
            }
            if (DocumentNo != "")
            {
                DocumentNo += "-" + (Convert.ToInt32(objPIHeader.GetAllData(StrCompId.ToString(), StrBrandId, StrLocationId).Rows.Count) + 1).ToString();
            }
            else
            {
                DocumentNo += (Convert.ToInt32(objPIHeader.GetAllData(StrCompId.ToString(), StrBrandId, StrLocationId).Rows.Count) + 1).ToString();

            }
        }
        else
        {
            DocumentNo += (Convert.ToInt32(objPIHeader.GetAllData(StrCompId.ToString(), StrBrandId, StrLocationId).Rows.Count) + 1).ToString();
        }

        return DocumentNo;

    }

    protected void GvPurchaseInquiry_SelectedIndexChanged(object sender, EventArgs e)
    {

    }


}
