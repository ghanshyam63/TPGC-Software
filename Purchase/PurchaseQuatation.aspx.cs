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


public partial class Purchase_PurchaseQuatation : System.Web.UI.Page
{
    #region defind Class Object
    Common cmn = new Common();
    Inv_PurchaseQuoteHeader objQuoteHeader = new Inv_PurchaseQuoteHeader();
    Inv_PurchaseQuoteDetail ObjQuoteDetail = new Inv_PurchaseQuoteDetail();
    Inv_PurchaseInquiryHeader objPIHeader = new Inv_PurchaseInquiryHeader();
    Inv_PurchaseInquiryDetail ObjPIDetail = new Inv_PurchaseInquiryDetail();
    Inv_PurchaseInquiry_Supplier objPISupplier = new Inv_PurchaseInquiry_Supplier();
    Inv_ProductMaster objProductM = new Inv_ProductMaster();
    Ems_ContactMaster objContact = new Ems_ContactMaster();
    Inv_UnitMaster UM = new Inv_UnitMaster();
    SystemParameter ObjSysParam = new SystemParameter();
    UserMaster ObjUser = new UserMaster();
    Set_DocNumber objDocNo = new Set_DocNumber();
   

    string StrCompId =string.Empty;
    string StrBrandId = string.Empty;
    string StrLocationId = string.Empty;
    string StrUserId = string.Empty;
    #endregion

    protected void Page_Load(object sender, EventArgs e)
    {
        //Remove
        StrUserId = Session["UserId"].ToString();
        StrCompId = Session["CompId"].ToString();
        StrBrandId = Session["BrandId"].ToString();
        StrLocationId = Session["LocId"].ToString();
           
        //End Remove
        if (Session["UserId"] == null)
        {
            Response.Redirect("~/Login.aspx");
        }
        if (!IsPostBack)
        {
            txtRPQNo.Text = GetDocumentNumber();
           
            ddlOption.SelectedIndex = 2;
            btnList_Click(null, null);
            FillGridBin();
            FillGrid();
            FillRequestGrid();
            //txtRPQNo.Text = objQuoteHeader.GetAutoID(StrCompId, StrBrandId, StrLocationId);
            txtRPQDate.Text = DateTime.Now.ToString(ObjSysParam.GetSysParameterByParamName("Date_Format").Rows[0]["Param_Value"].ToString());
        }
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
        pnlProduct.Visible = false;
        pnlRequest.Visible = false;
        PnlBin.Visible = false;
    }
    protected void btnNew_Click(object sender, EventArgs e)
    {
        pnlMenuNew.BackColor = System.Drawing.ColorTranslator.FromHtml("#ccddee");

        pnlMenuRequest.BackColor = System.Drawing.ColorTranslator.FromHtml("#90bde9");
        pnlMenuList.BackColor = System.Drawing.ColorTranslator.FromHtml("#90bde9");
        pnlMenuBin.BackColor = System.Drawing.ColorTranslator.FromHtml("#90bde9");

        PnlList.Visible = false;
        PnlNewEdit.Visible = true;
        pnlProduct.Visible = false;
        pnlRequest.Visible = false;
        PnlBin.Visible = false;
    }
    protected void btnEdit_Command(object sender, CommandEventArgs e)
    {
        editid.Value = e.CommandArgument.ToString();

        DataTable dtQuoteEdit = objQuoteHeader.GetQuoteHeaderAllDataByTransId(StrCompId, StrBrandId, StrLocationId, editid.Value);

        btnNew.Text = Resources.Attendance.Edit;
        if (dtQuoteEdit.Rows.Count > 0)
        {
            txtRPQNo.Text = dtQuoteEdit.Rows[0]["RPQ_No"].ToString();
            txtRPQNo.ReadOnly = true;
            txtRPQDate.Text = Convert.ToDateTime(dtQuoteEdit.Rows[0]["RPQ_Date"].ToString()).ToString(ObjSysParam.GetSysParameterByParamName("Date_Format").Rows[0]["Param_Value"].ToString());
            txtTotalAmount.Text = dtQuoteEdit.Rows[0]["TotalAmount"].ToString();

            //Add Inquiry Data
            string strPI_Id = dtQuoteEdit.Rows[0]["PI_No"].ToString();
            hdnPIId.Value = strPI_Id;
            if (strPI_Id != "" && strPI_Id != "0")
            {
                DataTable dtInquiry = objPIHeader.GetPIHeaderAllDataByTransId(StrCompId, StrBrandId, StrLocationId, hdnPIId.Value);
                if (dtInquiry.Rows.Count > 0)
                {
                    txtInquiryNo.Text = dtInquiry.Rows[0]["PI_No"].ToString();
                    txtInquiryDate.Text = Convert.ToDateTime(dtInquiry.Rows[0]["PIDate"].ToString()).ToString(ObjSysParam.GetSysParameterByParamName("Date_Format").Rows[0]["Param_Value"].ToString());
                }
            }

            //Add Supplier Section For Edit
            DataTable dtSupplier = objPISupplier.GetAllPISupplierWithPI_No(StrCompId, StrBrandId, strPI_Id);
            if (dtSupplier.Rows.Count > 0)
            {
                GvSupplier.DataSource = dtSupplier;
                GvSupplier.DataBind();

            }
        }
        btnNew_Click(null, null);
        System.Web.UI.ScriptManager.GetCurrent(this).SetFocus(txtRPQNo);
    }
    protected void GvPurchaseQuote_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        GvPurchaseQuote.PageIndex = e.NewPageIndex;
        DataTable dt = (DataTable)Session["dtFilter"];
        GvPurchaseQuote.DataSource = dt;
        GvPurchaseQuote.DataBind();
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
            GvPurchaseQuote.DataSource = view.ToTable();
            Session["dtFilter"] = view.ToTable();
            lblTotalRecords.Text = Resources.Attendance.Total_Records + " : " + view.ToTable().Rows.Count + "";
            GvPurchaseQuote.DataBind();
            AllPageCode();
        }
    }
    protected void GvPurchaseQuote_Sorting(object sender, GridViewSortEventArgs e)
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
        GvPurchaseQuote.DataSource = dt;
        GvPurchaseQuote.DataBind();
        AllPageCode();
    }
    protected void IbtnDelete_Command(object sender, CommandEventArgs e)
    {
        editid.Value = e.CommandArgument.ToString();
        int b = 0;
        b = objQuoteHeader.DeleteQuoteHeader(StrCompId, StrBrandId, StrLocationId, editid.Value, "false", StrUserId, DateTime.Now.ToString());
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
    }
    protected void btnQuoteCancel_Click(object sender, EventArgs e)
    {
        Reset();
        btnList_Click(null, null);
        FillGridBin();
        FillGrid();
    }
    protected void BtnReset_Click(object sender, EventArgs e)
    {
        Reset();
        System.Web.UI.ScriptManager.GetCurrent(this).SetFocus(txtRPQNo);
    }
    protected void btnQuoteSave_Click(object sender, EventArgs e)
    {
        if (txtRPQDate.Text == "")
        {
            DisplayMessage("Enter Purchase Quotation Date");
            System.Web.UI.ScriptManager.GetCurrent(this).SetFocus(txtRPQDate);
            return;
        }
        if (txtRPQNo.Text == "")
        {
            DisplayMessage("Enter Purchase Quotation No.");
            System.Web.UI.ScriptManager.GetCurrent(this).SetFocus(txtRPQNo);
            return;
        }
        else
        {
            if (editid.Value == "")
            {
                DataTable dtPINo = objQuoteHeader.GetQuoteHeaderAllDataByRPQ_No(StrCompId, StrBrandId, StrLocationId, txtRPQNo.Text);
                if (dtPINo.Rows.Count > 0)
                {
                    DisplayMessage("Purchase Quotation No. Already Exits");
                    txtRPQNo.Text = "";
                    System.Web.UI.ScriptManager.GetCurrent(this).SetFocus(txtRPQNo);
                    return;
                }
            }
        }

        //if (txtSupplier.Text == "")
        //{
        //    DisplayMessage("Enter Supplier");
        //    System.Web.UI.ScriptManager.GetCurrent(this).SetFocus(txtSupplier);
        //    return;
        //}
        //else
        //{
        //    if (GetSupplierId() == "")
        //    {
        //        DisplayMessage("Please Choose In Suggestions Only");
        //        txtSupplier.Text = "";
        //        System.Web.UI.ScriptManager.GetCurrent(this).SetFocus(txtSupplier);
        //        return;
        //    }
        //}

        if (hdnPIId.Value != "0")
        {
            if (GvSupplier.Rows.Count > 0)
            {

            }
            else
            {
                DisplayMessage("You have no supplier");
                return;
            }
        }
        else if (hdnPIId.Value == "0")
        {
            DisplayMessage("Choose Record In Inquiry Section For Create Quotation");
            return;
        }

        int b = 0;
        if (editid.Value != "")
        {
            b = objQuoteHeader.UpdateQuoteHeader(StrCompId, StrBrandId, StrLocationId, editid.Value, txtRPQNo.Text, txtRPQDate.Text, hdnPIId.Value, txtTotalAmount.Text, "", "", "", "", "", "True", DateTime.Now.ToString(), "True", Session["UserId"].ToString(), DateTime.Now.ToString());
            objPIHeader.DeletePIHeader(StrCompId, StrBrandId, StrLocationId, hdnPIId.Value, "False", Session["UserId"].ToString(), DateTime.Now.ToString());

            if (b != 0)
            {
                DisplayMessage("Record Updated");
                Reset();
                editid.Value = "";
                FillRequestGrid();
                FillGrid();
                btnList_Click(null, null);
            }
            else
            {
                DisplayMessage("Record  Not Updated");
            }
        }
        else
        {
            b = objQuoteHeader.InsertQuoteHeader(StrCompId, StrBrandId, StrLocationId, txtRPQNo.Text, txtRPQDate.Text,hdnPIId.Value, txtTotalAmount.Text, "", "", "", "", "", "True", DateTime.Now.ToString(), "True", Session["UserId"].ToString(), DateTime.Now.ToString(), Session["UserId"].ToString(), DateTime.Now.ToString());
            objPIHeader.DeletePIHeader(StrCompId, StrBrandId, StrLocationId, hdnPIId.Value, "False", Session["UserId"].ToString(), DateTime.Now.ToString());

            if (b != 0)
            {
                DisplayMessage("Record Saved");
                FillRequestGrid();
                FillGrid();
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
        b = objQuoteHeader.DeleteQuoteHeader(StrCompId, StrBrandId, StrLocationId, editid.Value, true.ToString(), StrUserId, DateTime.Now.ToString());
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
        pnlProduct.Visible = false;
        pnlRequest.Visible = false;
        PnlList.Visible = false;
        FillGridBin();
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
        pnlProduct.Visible = false;
        PnlList.Visible = false;
        FillGridBin();
    }
    protected void GvPurchaseQuoteBin_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        GvPurchaseQuoteBin.PageIndex = e.NewPageIndex;

        DataTable dt = (DataTable)Session["dtInactive"];
        GvPurchaseQuoteBin.DataSource = dt;
        GvPurchaseQuoteBin.DataBind();

        string temp = string.Empty;

        for (int i = 0; i < GvPurchaseQuoteBin.Rows.Count; i++)
        {
            HiddenField lblconid = (HiddenField)GvPurchaseQuoteBin.Rows[i].FindControl("hdnTransId");
            string[] split = lblSelectedRecord.Text.Split(',');

            for (int j = 0; j < lblSelectedRecord.Text.Split(',').Length; j++)
            {
                if (lblSelectedRecord.Text.Split(',')[j] != "")
                {
                    if (lblconid.Value.Trim().ToString() == lblSelectedRecord.Text.Split(',')[j].Trim().ToString())
                    {
                        ((CheckBox)GvPurchaseQuoteBin.Rows[i].FindControl("chkSelect")).Checked = true;
                    }
                }
            }
        }
        AllPageCode();
    }
    protected void GvPurchaseQuoteBin_OnSorting(object sender, GridViewSortEventArgs e)
    {
        HDFSortbin.Value = HDFSortbin.Value == "ASC" ? "DESC" : "ASC";
        DataTable dt = new DataTable();
        dt = objQuoteHeader.GetQuoteHeaderAllFalse(StrCompId, StrBrandId, StrLocationId);
        DataView dv = new DataView(dt);
        string Query = "" + e.SortExpression + " " + HDFSortbin.Value + "";
        dv.Sort = Query;
        dt = dv.ToTable();
        Session["dtInactive"] = dt;
        GvPurchaseQuoteBin.DataSource = dt;
        GvPurchaseQuoteBin.DataBind();
        lblSelectedRecord.Text = "";
        AllPageCode();
    }
    public void FillGridBin()
    {
        DataTable dt = new DataTable();
        dt = objQuoteHeader.GetQuoteHeaderAllFalse(StrCompId, StrBrandId, StrLocationId);
        GvPurchaseQuoteBin.DataSource = dt;
        GvPurchaseQuoteBin.DataBind();
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
            AllPageCode();
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
            GvPurchaseQuoteBin.DataSource = view.ToTable();
            GvPurchaseQuoteBin.DataBind();
            lblSelectedRecord.Text = "";
            if (view.ToTable().Rows.Count == 0)
            {
                FillGridBin();
            }
            AllPageCode();
            System.Web.UI.ScriptManager.GetCurrent(this).SetFocus(txtValueBin);
        }
    }
    protected void btnRestoreSelected_Click(object sender, CommandEventArgs e)
    {
        int b = 0;
        DataTable dt = objQuoteHeader.GetQuoteHeaderAllFalse(StrCompId, StrBrandId, StrLocationId);

        if (GvPurchaseQuoteBin.Rows.Count != 0)
        {
            if (lblSelectedRecord.Text != "")
            {
                for (int j = 0; j < lblSelectedRecord.Text.Split(',').Length; j++)
                {
                    if (lblSelectedRecord.Text.Split(',')[j] != "")
                    {
                        //Msg = objTax.DeleteTaxMaster(lblSelectedRecord.Text.Split(',')[j].Trim().ToString(), true.ToString(), Session["UserId"].ToString(), DateTime.Now.ToString());
                        b = objQuoteHeader.DeleteQuoteHeader(StrCompId, StrBrandId, StrLocationId, lblSelectedRecord.Text.Split(',')[j].Trim().ToString(), true.ToString(), Session["UserId"].ToString(), DateTime.Now.ToString());
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
                foreach (GridViewRow Gvr in GvPurchaseQuoteBin.Rows)
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
        CheckBox chkSelAll = ((CheckBox)GvPurchaseQuoteBin.HeaderRow.FindControl("chkCurrent"));
        for (int i = 0; i < GvPurchaseQuoteBin.Rows.Count; i++)
        {
            ((CheckBox)GvPurchaseQuoteBin.Rows[i].FindControl("chkSelect")).Checked = chkSelAll.Checked;
            if (chkSelAll.Checked)
            {
                if (!lblSelectedRecord.Text.Split(',').Contains(((HiddenField)(GvPurchaseQuoteBin.Rows[i].FindControl("hdnTransId"))).Value.Trim().ToString()))
                {
                    lblSelectedRecord.Text += ((HiddenField)(GvPurchaseQuoteBin.Rows[i].FindControl("hdnTransId"))).Value.Trim().ToString() + ",";
                }
            }
            else
            {
                string temp = string.Empty;
                string[] split = lblSelectedRecord.Text.Split(',');
                foreach (string item in split)
                {
                    if (item != ((HiddenField)(GvPurchaseQuoteBin.Rows[i].FindControl("hdnTransId"))).Value.Trim().ToString())
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
        HiddenField lb = (HiddenField)GvPurchaseQuoteBin.Rows[index].FindControl("hdnTransId");
        if (((CheckBox)GvPurchaseQuoteBin.Rows[index].FindControl("chkSelect")).Checked)
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
            for (int i = 0; i < GvPurchaseQuoteBin.Rows.Count; i++)
            {
                string[] split = lblSelectedRecord.Text.Split(',');
                HiddenField lblconid = (HiddenField)GvPurchaseQuoteBin.Rows[i].FindControl("hdnTransId");
                for (int j = 0; j < lblSelectedRecord.Text.Split(',').Length; j++)
                {
                    if (lblSelectedRecord.Text.Split(',')[j] != "")
                    {
                        if (lblconid.Value.Trim().ToString() == lblSelectedRecord.Text.Split(',')[j].Trim().ToString())
                        {
                            ((CheckBox)GvPurchaseQuoteBin.Rows[i].FindControl("chkSelect")).Checked = true;
                        }
                    }
                }
            }
        }
        else
        {
            lblSelectedRecord.Text = "";
            DataTable dtUnit1 = (DataTable)Session["dtInactive"];
            GvPurchaseQuoteBin.DataSource = dtUnit1;
            GvPurchaseQuoteBin.DataBind();
            ViewState["Select"] = null;
        }
        AllPageCode();
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
                    b = objQuoteHeader.DeleteQuoteHeader(StrCompId, StrBrandId, StrLocationId, lblSelectedRecord.Text.Split(',')[j].Trim(), true.ToString(), Session["UserId"].ToString(), DateTime.Now.ToString());
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
            foreach (GridViewRow Gvr in GvPurchaseQuoteBin.Rows)
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
        DataTable dtBrand = objQuoteHeader.GetQuoteHeaderAllTrue(StrCompId, StrBrandId, StrLocationId);
        lblTotalRecords.Text = Resources.Attendance.Total_Records + " : " + dtBrand.Rows.Count + "";
        Session["dtCurrency"] = dtBrand;
        Session["dtFilter"] = dtBrand;
        if (dtBrand != null && dtBrand.Rows.Count > 0)
        {
            GvPurchaseQuote.DataSource = dtBrand;
            GvPurchaseQuote.DataBind();
        }
        else
        {
            GvPurchaseQuote.DataSource = null;
            GvPurchaseQuote.DataBind();
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
        txtRPQNo.Text = objQuoteHeader.GetAutoID(StrCompId, StrBrandId, StrLocationId);
        txtRPQDate.Text = DateTime.Now.ToString(ObjSysParam.GetSysParameterByParamName("Date_Format").Rows[0]["Param_Value"].ToString());
        txtRPQNo.ReadOnly = false;
        txtInquiryNo.Text = "";
        txtInquiryDate.Text = "";
        txtTotalAmount.Text = "";

        GvSupplier.DataSource = null;
        GvSupplier.DataBind();

        dlProductDetail.DataSource = null;
        dlProductDetail.DataBind();

        hdnPIId.Value = "0";
        hdnSupplierId.Value = "0";

        editid.Value = "";
        btnNew.Text = Resources.Attendance.New;
        ddlFieldName.SelectedIndex = 1;
        ddlOption.SelectedIndex = 2;
        txtValue.Text = "";
        txtValueBin.Text = "";
        lblSelectedRecord.Text = "";
        System.Web.UI.ScriptManager.GetCurrent(this).SetFocus(txtValue);
        txtRPQNo.Text = GetDocumentNumber();
    }
    #endregion

    #region Add Product Concept
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
    protected void GvPurchaseInquiry_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        GvPurchaseInquiry.PageIndex = e.NewPageIndex;
        DataTable dt = (DataTable)Session["dtPRequest"];
        GvPurchaseInquiry.DataSource = dt;
        GvPurchaseInquiry.DataBind();
    }
    protected void GvPurchaseInquiry_Sorting(object sender, GridViewSortEventArgs e)
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
        GvPurchaseInquiry.DataSource = dt;
        GvPurchaseInquiry.DataBind();
    }
    private void FillRequestGrid()
    {
        DataTable dtPRequest = objPIHeader.GetDataForQuotation(StrCompId, StrBrandId, StrLocationId);
    
     
        lblTotalRecords.Text = Resources.Attendance.Total_Records + " : " + dtPRequest.Rows.Count + "";
        Session["dtPRequest"] = dtPRequest;
        if (dtPRequest != null && dtPRequest.Rows.Count > 0)
        {
            GvPurchaseInquiry.DataSource = dtPRequest;
            GvPurchaseInquiry.DataBind();
        }
        else
        {
            GvPurchaseInquiry.DataSource = null;
            GvPurchaseInquiry.DataBind();
        }
        lblTotalRecords.Text = Resources.Attendance.Total_Records + " : " + dtPRequest.Rows.Count.ToString() + "";
    }
    protected void btnPREdit_Command(object sender, CommandEventArgs e)
    {
        string strRequestId = e.CommandArgument.ToString();

        DataTable dtPRequest = objPIHeader.GetPIHeaderAllDataByTransId(StrCompId, StrBrandId, StrLocationId, strRequestId);

        if (dtPRequest.Rows.Count > 0)
        {
            txtInquiryNo.Text = dtPRequest.Rows[0]["PI_No"].ToString();
            txtInquiryDate.Text = Convert.ToDateTime(dtPRequest.Rows[0]["PIDate"].ToString()).ToString(ObjSysParam.GetSysParameterByParamName("Date_Format").Rows[0]["Param_Value"].ToString());

            hdnPIId.Value = dtPRequest.Rows[0]["Trans_Id"].ToString();

            //Add Supplier Grid
            DataTable dtSupplier = objPISupplier.GetAllPISupplierWithPI_No(StrCompId, StrBrandId, strRequestId);
            if (dtSupplier.Rows.Count > 0)
            {
                GvSupplier.DataSource = dtSupplier;
                GvSupplier.DataBind();
            }
        }
        btnNew_Click(null, null);
        System.Web.UI.ScriptManager.GetCurrent(this).SetFocus(txtRPQNo);
    }
    #endregion

    #region Supplier Section
    protected string GetSupplierName(string strSupplierId)
    {
        string strSupplierName = string.Empty;
        if (strSupplierId != "0" && strSupplierId != "")
        {
            DataTable dtSName = objContact.GetContactByContactId(StrCompId, strSupplierId);
            if (dtSName.Rows.Count > 0)
            {
                strSupplierName = dtSName.Rows[0]["Contact_Name"].ToString();
            }
        }
        else
        {
            strSupplierName = "";
        }
        return strSupplierName;
    }
    protected void ImgSupplierDetail_Command(object sender, CommandEventArgs e)
    {
        if (txtRPQDate.Text == "")
        {
            DisplayMessage("Enter Purchase Quotation Date");
            System.Web.UI.ScriptManager.GetCurrent(this).SetFocus(txtRPQDate);
            return;
        }
        if (txtRPQNo.Text == "")
        {
            DisplayMessage("Enter Purchase Quotation No.");
            System.Web.UI.ScriptManager.GetCurrent(this).SetFocus(txtRPQNo);
            return;
        }
        else
        {
            //if (editid.Value == "")
            //{
            //    DataTable dtPINo = objQuoteHeader.GetQuoteHeaderAllDataByRPQ_No(StrCompId, StrBrandId, StrLocationId, txtRPQNo.Text);
            //    if (dtPINo.Rows.Count > 0)
            //    {
            //        DisplayMessage("Purchase Quotation No. Already Exits");
            //        txtRPQNo.Text = "";
            //        System.Web.UI.ScriptManager.GetCurrent(this).SetFocus(txtRPQNo);
            //        return;
            //    }
            //}
        }

        hdnSupplierId.Value = e.CommandArgument.ToString();

        DataTable dtProductDetail = ObjPIDetail.GetPIDetailByPI_No(StrCompId, StrBrandId, StrLocationId, hdnPIId.Value);

        if (dtProductDetail.Rows.Count > 0)
        {
            dlProductDetail.DataSource = dtProductDetail;
            dlProductDetail.DataBind();
            pnlProduct.Visible = true;
            PnlNewEdit.Visible = false;

            foreach (DataListItem dl in dlProductDetail.Items)
            {
                HiddenField hdnProductId = (HiddenField)dl.FindControl("hdngvProductId");
                Label lblProductName = (Label)dl.FindControl("txtProductName");
                Label lblProductDescription = (Label)dl.FindControl("txtProductDescription");

                if (hdnProductId.Value == "0")
                {
                    if (lblProductDescription.Text != "")
                    {
                        lblProductName.Text = lblProductDescription.Text;
                    }
                }
            }
        }
        else
        {
            dlProductDetail.DataSource = null;
            dlProductDetail.DataBind();
        }

        //add edit Detail
        if (editid.Value == "")
        {
            if (txtRPQNo.Text != "")
            {
                DataTable dtData = objQuoteHeader.GetQuoteHeaderAllDataByRPQ_No(StrCompId, StrBrandId, StrLocationId, txtRPQNo.Text);
                if (dtData.Rows.Count > 0)
                {
                    editid.Value = dtData.Rows[0]["Trans_Id"].ToString();

                    foreach (DataListItem dl in dlProductDetail.Items)
                    {
                        HiddenField hdnProductId = (HiddenField)dl.FindControl("hdngvProductId");
                        TextBox txtRefrencedPName = (TextBox)dl.FindControl("txtRefProductName");
                        TextBox txtRefPartNo = (TextBox)dl.FindControl("txtRefPartNo");
                        HiddenField hdnUnitId = (HiddenField)dl.FindControl("hdngvUnitId");
                        Label txtReqQty = (Label)dl.FindControl("txtRequiredQuantity");
                        TextBox txtUnitPrice = (TextBox)dl.FindControl("txtUnitPrice");
                        TextBox txtTaxP = (TextBox)dl.FindControl("txtTaxP");
                        TextBox txtTaxV = (TextBox)dl.FindControl("txtTaxV");
                        TextBox txtAfterTax = (TextBox)dl.FindControl("txtPriceAfterTax");
                        TextBox txtDiscountP = (TextBox)dl.FindControl("txtDiscountP");
                        TextBox txtDiscountV = (TextBox)dl.FindControl("txtDiscountV");
                        TextBox txtAmount = (TextBox)dl.FindControl("txtAmount");
                        TextBox txtTermsCondition = (TextBox)dl.FindControl("txtTermsCondition");

                        DataTable dtChildData = ObjQuoteDetail.GetQuoteDetailByRPQ_No(StrCompId, StrBrandId, StrLocationId, editid.Value);
                        if (dtChildData.Rows.Count > 0)
                        {
                            dtChildData = new DataView(dtChildData, "Supplier_Id='" + hdnSupplierId.Value + "' and Product_Id='" + hdnProductId.Value + "'", "", DataViewRowState.CurrentRows).ToTable();

                            if (dtChildData.Rows.Count > 0)
                            {
                                txtRefrencedPName.Text = dtChildData.Rows[0]["RefrencedProductName"].ToString();
                                txtRefPartNo.Text = dtChildData.Rows[0]["RefrencedPartNo"].ToString();
                                txtUnitPrice.Text = dtChildData.Rows[0]["UnitPrice"].ToString();
                                txtTaxP.Text = dtChildData.Rows[0]["TaxPercentage"].ToString();
                                txtTaxV.Text = dtChildData.Rows[0]["TaxValue"].ToString();
                                txtAfterTax.Text = dtChildData.Rows[0]["PriceAfterTax"].ToString();
                                txtDiscountP.Text = dtChildData.Rows[0]["DisPercentage"].ToString();
                                txtDiscountV.Text = dtChildData.Rows[0]["DiscountValue"].ToString();
                                txtAmount.Text = dtChildData.Rows[0]["Amount"].ToString();
                                txtTermsCondition.Text = dtChildData.Rows[0]["TermsCondition"].ToString();
                            }
                        }
                    }
                }
                else
                {

                }
            }
        }
        else
        {
            foreach (DataListItem dl in dlProductDetail.Items)
            {
                HiddenField hdnProductId = (HiddenField)dl.FindControl("hdngvProductId");
                TextBox txtRefrencedPName = (TextBox)dl.FindControl("txtRefProductName");
                TextBox txtRefPartNo = (TextBox)dl.FindControl("txtRefPartNo");
                HiddenField hdnUnitId = (HiddenField)dl.FindControl("hdngvUnitId");
                Label txtReqQty = (Label)dl.FindControl("txtRequiredQuantity");
                TextBox txtUnitPrice = (TextBox)dl.FindControl("txtUnitPrice");
                TextBox txtTaxP = (TextBox)dl.FindControl("txtTaxP");
                TextBox txtTaxV = (TextBox)dl.FindControl("txtTaxV");
                TextBox txtAfterTax = (TextBox)dl.FindControl("txtPriceAfterTax");
                TextBox txtDiscountP = (TextBox)dl.FindControl("txtDiscountP");
                TextBox txtDiscountV = (TextBox)dl.FindControl("txtDiscountV");
                TextBox txtAmount = (TextBox)dl.FindControl("txtAmount");
                TextBox txtTermsCondition = (TextBox)dl.FindControl("txtTermsCondition");

                DataTable dtChildData = ObjQuoteDetail.GetQuoteDetailByRPQ_No(StrCompId, StrBrandId, StrLocationId, editid.Value);
                if (dtChildData.Rows.Count > 0)
                {
                    dtChildData = new DataView(dtChildData, "Supplier_Id='" + hdnSupplierId.Value + "' and Product_Id='" + hdnProductId.Value + "'", "", DataViewRowState.CurrentRows).ToTable();

                    if (dtChildData.Rows.Count > 0)
                    {
                        txtRefrencedPName.Text = dtChildData.Rows[0]["RefrencedProductName"].ToString();
                        txtRefPartNo.Text = dtChildData.Rows[0]["RefrencedPartNo"].ToString();
                        txtUnitPrice.Text = dtChildData.Rows[0]["UnitPrice"].ToString();
                        txtTaxP.Text = dtChildData.Rows[0]["TaxPercentage"].ToString();
                        txtTaxV.Text = dtChildData.Rows[0]["TaxValue"].ToString();
                        txtAfterTax.Text = dtChildData.Rows[0]["PriceAfterTax"].ToString();
                        txtDiscountP.Text = dtChildData.Rows[0]["DisPercentage"].ToString();
                        txtDiscountV.Text = dtChildData.Rows[0]["DiscountValue"].ToString();
                        txtAmount.Text = dtChildData.Rows[0]["Amount"].ToString();
                        txtTermsCondition.Text = dtChildData.Rows[0]["TermsCondition"].ToString();
                    }
                }
            }
        }
    }

    protected void btnProductSave_Click(object sender, EventArgs e)
    {
        int b = 0;
       
    
        if (editid.Value != "")
        {
            b = objQuoteHeader.UpdateQuoteHeader(StrCompId, StrBrandId, StrLocationId, editid.Value, txtRPQNo.Text, txtRPQDate.Text, hdnPIId.Value, txtTotalAmount.Text, "", "", "", "", "", "True", DateTime.Now.ToString(), "True", Session["UserId"].ToString(), DateTime.Now.ToString());


            if (b != 0)
            {
                //Add QuoteDetail Record
                ObjQuoteDetail.DeleteQuoteDetail(StrCompId, StrBrandId, StrLocationId, editid.Value, hdnSupplierId.Value);

                foreach (DataListItem dlDetail in dlProductDetail.Items)
                {
                    HiddenField hdnProductId = (HiddenField)dlDetail.FindControl("hdngvProductId");
                    Label lblProductDescription = (Label)dlDetail.FindControl("txtProductDescription");
                    TextBox txtRefrencedPName = (TextBox)dlDetail.FindControl("txtRefProductName");
                    TextBox txtRefPartNo = (TextBox)dlDetail.FindControl("txtRefPartNo");
                    HiddenField hdnUnitId = (HiddenField)dlDetail.FindControl("hdngvUnitId");
                    Label txtReqQty = (Label)dlDetail.FindControl("txtRequiredQuantity");
                    TextBox txtUnitPrice = (TextBox)dlDetail.FindControl("txtUnitPrice");
                    TextBox txtTaxP = (TextBox)dlDetail.FindControl("txtTaxP");
                    TextBox txtTaxV = (TextBox)dlDetail.FindControl("txtTaxV");
                    TextBox txtAfterTax = (TextBox)dlDetail.FindControl("txtPriceAfterTax");
                    TextBox txtDiscountP = (TextBox)dlDetail.FindControl("txtDiscountP");
                    TextBox txtDiscountV = (TextBox)dlDetail.FindControl("txtDiscountV");
                    TextBox txtAmount = (TextBox)dlDetail.FindControl("txtAmount");
                    TextBox txtTermsCondition = (TextBox)dlDetail.FindControl("txtTermsCondition");

                    ObjQuoteDetail.InsertQuoteDetail(StrCompId, StrBrandId, StrLocationId, editid.Value, hdnSupplierId.Value, hdnProductId.Value, lblProductDescription.Text, txtRefrencedPName.Text, txtRefPartNo.Text, hdnUnitId.Value, txtReqQty.Text, txtUnitPrice.Text, txtTaxP.Text, txtTaxV.Text, txtAfterTax.Text, txtDiscountP.Text, txtDiscountV.Text, txtAmount.Text, txtTermsCondition.Text, "", "", "", "", "", "True", DateTime.Now.ToString(), "True", Session["UserId"].ToString(), DateTime.Now.ToString(), Session["UserId"].ToString(), DateTime.Now.ToString());
                }

                //Get Total Amount
                txtTotalAmount.Text = "0";
                DataTable dtData = ObjQuoteDetail.GetQuoteDetailByRPQ_No(StrCompId, StrBrandId, StrLocationId, editid.Value);
                if (dtData.Rows.Count > 0)
                {
                    string strAmount = string.Empty;
                    float fGrossTotal = 0.00f;
                    for (int i = 0; i < dtData.Rows.Count; i++)
                    {
                        strAmount = dtData.Rows[i]["Amount"].ToString();
                        if (strAmount != "")
                        {
                            fGrossTotal = fGrossTotal + float.Parse(strAmount);
                        }
                    }
                    txtTotalAmount.Text = fGrossTotal.ToString();
                }

                dlProductDetail.DataSource = null;
                dlProductDetail.DataBind();
                pnlProduct.Visible = false;
                PnlNewEdit.Visible = true;
            }
            else
            {
                DisplayMessage("Record  Not Updated");
            }
        }
        else
        {
            b = objQuoteHeader.InsertQuoteHeader(StrCompId, StrBrandId, StrLocationId, txtRPQNo.Text, txtRPQDate.Text, hdnPIId.Value, txtTotalAmount.Text, "", "", "", "", "", "True", DateTime.Now.ToString(), "True", Session["UserId"].ToString(), DateTime.Now.ToString(), Session["UserId"].ToString(), DateTime.Now.ToString());
            if (b != 0)
            {
                string strMaxId = string.Empty;
                DataTable dtMaxId = objQuoteHeader.GetMaxTransId(StrCompId, StrBrandId, StrLocationId);
                if (dtMaxId.Rows.Count > 0)
                {
                    strMaxId = dtMaxId.Rows[0][0].ToString();
                }

                //Add QuoteDetail Record
                foreach (DataListItem dlDetail in dlProductDetail.Items)
                {
                    HiddenField hdnProductId = (HiddenField)dlDetail.FindControl("hdngvProductId");
                    Label lblProductDescription = (Label)dlDetail.FindControl("txtProductDescription");
                    TextBox txtRefrencedPName = (TextBox)dlDetail.FindControl("txtRefProductName");
                    TextBox txtRefPartNo = (TextBox)dlDetail.FindControl("txtRefPartNo");
                    HiddenField hdnUnitId = (HiddenField)dlDetail.FindControl("hdngvUnitId");
                    Label txtReqQty = (Label)dlDetail.FindControl("txtRequiredQuantity");
                    TextBox txtUnitPrice = (TextBox)dlDetail.FindControl("txtUnitPrice");
                    TextBox txtTaxP = (TextBox)dlDetail.FindControl("txtTaxP");
                    TextBox txtTaxV = (TextBox)dlDetail.FindControl("txtTaxV");
                    TextBox txtAfterTax = (TextBox)dlDetail.FindControl("txtPriceAfterTax");
                    TextBox txtDiscountP = (TextBox)dlDetail.FindControl("txtDiscountP");
                    TextBox txtDiscountV = (TextBox)dlDetail.FindControl("txtDiscountV");
                    TextBox txtAmount = (TextBox)dlDetail.FindControl("txtAmount");
                    TextBox txtTermsCondition = (TextBox)dlDetail.FindControl("txtTermsCondition");

                    ObjQuoteDetail.InsertQuoteDetail(StrCompId, StrBrandId, StrLocationId, strMaxId, hdnSupplierId.Value, hdnProductId.Value, lblProductDescription.Text, txtRefrencedPName.Text, txtRefPartNo.Text, hdnUnitId.Value, txtReqQty.Text, txtUnitPrice.Text, txtTaxP.Text, txtTaxV.Text, txtAfterTax.Text, txtDiscountP.Text, txtDiscountV.Text, txtAmount.Text, txtTermsCondition.Text, "", "", "", "", "", "True", DateTime.Now.ToString(), "True", Session["UserId"].ToString(), DateTime.Now.ToString(), Session["UserId"].ToString(), DateTime.Now.ToString());
                }

                //Get Total Amount
                txtTotalAmount.Text = "0";
                DataTable dtData = ObjQuoteDetail.GetQuoteDetailByRPQ_No(StrCompId, StrBrandId, StrLocationId, strMaxId);
                if (dtData.Rows.Count > 0)
                {
                    string strAmount = string.Empty;
                    float fGrossTotal = 0.00f;
                    for (int i = 0; i < dtData.Rows.Count; i++)
                    {
                        strAmount = dtData.Rows[i]["Amount"].ToString();
                        if (strAmount != "")
                        {
                            fGrossTotal = fGrossTotal + float.Parse(strAmount);
                        }
                    }
                    txtTotalAmount.Text = fGrossTotal.ToString();
                }

                dlProductDetail.DataSource = null;
                dlProductDetail.DataBind();
                pnlProduct.Visible = false;
                PnlNewEdit.Visible = true;
            }
            else
            {
                DisplayMessage("Record  Not Saved");
            }
        }
        AllPageCode();
    }
    protected void btnProductCancel_Click(object sender, EventArgs e)
    {
        dlProductDetail.DataSource = null;
        dlProductDetail.DataBind();
        pnlProduct.Visible = false;
        PnlNewEdit.Visible = true;
    }
    #endregion

    #region Calculations
    protected void txtUnitPrice_TextChanged(object sender, EventArgs e)
    {
        TextBox txt = (TextBox)sender;
        if (txt.Text == "")
        {
            txt.Text = "0";
        }
        foreach (DataListItem dl in dlProductDetail.Items)
        {
            TextBox txtUnitPrice = (TextBox)dl.FindControl("txtUnitPrice");
            TextBox txtTaxP = (TextBox)dl.FindControl("txtTaxP");
            TextBox txtTaxV = (TextBox)dl.FindControl("txtTaxV");
            TextBox txtPriceAfterTax = (TextBox)dl.FindControl("txtPriceAfterTax");
            TextBox txtDiscountP = (TextBox)dl.FindControl("txtDiscountP");
            TextBox txtDiscountV = (TextBox)dl.FindControl("txtDiscountV");
            TextBox txtAmount = (TextBox)dl.FindControl("txtAmount");

            if (txtUnitPrice.Text != "")
            {
                float flTemp = 0;
                if (float.TryParse(txtUnitPrice.Text, out flTemp))
                {
                    if (txtTaxP.Text != "")
                    {
                        txtTaxP_TextChanged(sender, e);
                    }
                    else
                    {
                        txtPriceAfterTax.Text = "0";
                    }
                    if (txtDiscountP.Text != "")
                    {
                        txtDiscountP_TextChanged(sender, e);
                    }
                    else
                    {
                        txtAmount.Text = (float.Parse(txtUnitPrice.Text) + float.Parse(txtPriceAfterTax.Text)).ToString();
                    }
                    System.Web.UI.ScriptManager.GetCurrent(this).SetFocus(txtTaxP);
                }
                else
                {
                    txtUnitPrice.Text = "";
                    ScriptManager.RegisterStartupScript(this, typeof(string), "OPEN_WINDOW", "window.alert('Enter Numeric Value Only');", true);
                    System.Web.UI.ScriptManager.GetCurrent(this).SetFocus(txtUnitPrice);
                }
            }
        }
    }
    protected void txtTaxP_TextChanged(object sender, EventArgs e)
    {
        foreach (DataListItem dl in dlProductDetail.Items)
        {
            TextBox txtUnitPrice = (TextBox)dl.FindControl("txtUnitPrice");
            TextBox txtTaxP = (TextBox)dl.FindControl("txtTaxP");
            TextBox txtTaxV = (TextBox)dl.FindControl("txtTaxV");
            TextBox txtPriceAfterTax = (TextBox)dl.FindControl("txtPriceAfterTax");
            TextBox txtDiscountP = (TextBox)dl.FindControl("txtDiscountP");
            TextBox txtDiscountV = (TextBox)dl.FindControl("txtDiscountV");
            TextBox txtAmount = (TextBox)dl.FindControl("txtAmount");

            if (txtUnitPrice.Text != "")
            {
                float flTemp = 0;
                if (float.TryParse(txtUnitPrice.Text, out flTemp))
                {
                    if (txtTaxP.Text != "")
                    {
                        if (float.TryParse(txtTaxP.Text, out flTemp))
                        {
                            txtTaxV.Text = ((float.Parse(txtUnitPrice.Text) * float.Parse(txtTaxP.Text)) / 100).ToString();
                            if (txtUnitPrice.Text != "")
                            {
                                txtPriceAfterTax.Text = (float.Parse(txtUnitPrice.Text) + float.Parse(txtTaxV.Text)).ToString();
                            }
                            if (txtDiscountP.Text != "")
                            {
                                txtDiscountP_TextChanged(sender, e);
                            }
                            else
                            {
                                txtAmount.Text = txtPriceAfterTax.Text;
                            }
                            System.Web.UI.ScriptManager.GetCurrent(this).SetFocus(txtDiscountP);
                        }
                        else
                        {
                            txtTaxP.Text = "";
                            ScriptManager.RegisterStartupScript(this, typeof(string), "OPEN_WINDOW", "window.alert('Enter Numeric Value Only');", true);
                            System.Web.UI.ScriptManager.GetCurrent(this).SetFocus(txtTaxP);
                        }
                    }
                }
                else
                {
                    txtUnitPrice.Text = "";
                    ScriptManager.RegisterStartupScript(this, typeof(string), "OPEN_WINDOW", "window.alert('Enter Numeric Value Only');", true);
                    System.Web.UI.ScriptManager.GetCurrent(this).SetFocus(txtUnitPrice);
                }
            }
            //else
            //{
            //    ScriptManager.RegisterStartupScript(this, typeof(string), "OPEN_WINDOW", "window.alert('Enter Unit Price');", true);
            //    System.Web.UI.ScriptManager.GetCurrent(this).SetFocus(txtUnitPrice);
            //}
        }
    }
    protected void txtTaxV_TextChanged(object sender, EventArgs e)
    {
        foreach (DataListItem dl in dlProductDetail.Items)
        {
            TextBox txtUnitPrice = (TextBox)dl.FindControl("txtUnitPrice");
            TextBox txtTaxP = (TextBox)dl.FindControl("txtTaxP");
            TextBox txtTaxV = (TextBox)dl.FindControl("txtTaxV");
            TextBox txtPriceAfterTax = (TextBox)dl.FindControl("txtPriceAfterTax");
            TextBox txtDiscountP = (TextBox)dl.FindControl("txtDiscountP");
            TextBox txtDiscountV = (TextBox)dl.FindControl("txtDiscountV");
            TextBox txtAmount = (TextBox)dl.FindControl("txtAmount");

            if (txtUnitPrice.Text != "")
            {
                float flTemp = 0;
                if (float.TryParse(txtUnitPrice.Text, out flTemp))
                {
                    if (txtTaxV.Text != "")
                    {
                        if (float.TryParse(txtTaxV.Text, out flTemp))
                        {
                            txtTaxP.Text = ((100 * float.Parse(txtTaxV.Text)) / float.Parse(txtUnitPrice.Text)).ToString();
                            if (txtUnitPrice.Text != "")
                            {
                                txtPriceAfterTax.Text = (float.Parse(txtUnitPrice.Text) + float.Parse(txtTaxV.Text)).ToString();
                                txtAmount.Text = (float.Parse(txtUnitPrice.Text) + float.Parse(txtPriceAfterTax.Text)).ToString();
                            }
                            if (txtDiscountP.Text != "")
                            {
                                txtDiscountP_TextChanged(sender, e);
                            }
                            System.Web.UI.ScriptManager.GetCurrent(this).SetFocus(txtDiscountP);
                        }
                        else
                        {
                            txtTaxV.Text = "";
                            ScriptManager.RegisterStartupScript(this, typeof(string), "OPEN_WINDOW", "window.alert('Enter Numeric Value Only');", true);
                            System.Web.UI.ScriptManager.GetCurrent(this).SetFocus(txtTaxV);
                        }
                    }
                }
                else
                {
                    txtUnitPrice.Text = "";
                    ScriptManager.RegisterStartupScript(this, typeof(string), "OPEN_WINDOW", "window.alert('Enter Numeric Value Only');", true);
                    System.Web.UI.ScriptManager.GetCurrent(this).SetFocus(txtUnitPrice);
                }
            }
            //else
            //{
            //    ScriptManager.RegisterStartupScript(this, typeof(string), "OPEN_WINDOW", "window.alert('Enter Unit Price');", true);
            //    System.Web.UI.ScriptManager.GetCurrent(this).SetFocus(txtUnitPrice);
            //}
        }
    }
    protected void txtDiscountP_TextChanged(object sender, EventArgs e)
    {
        foreach (DataListItem dl in dlProductDetail.Items)
        {
            TextBox txtUnitPrice = (TextBox)dl.FindControl("txtUnitPrice");
            TextBox txtTaxP = (TextBox)dl.FindControl("txtTaxP");
            TextBox txtTaxV = (TextBox)dl.FindControl("txtTaxV");
            TextBox txtPriceAfterTax = (TextBox)dl.FindControl("txtPriceAfterTax");
            TextBox txtDiscountP = (TextBox)dl.FindControl("txtDiscountP");
            TextBox txtDiscountV = (TextBox)dl.FindControl("txtDiscountV");
            TextBox txtAmount = (TextBox)dl.FindControl("txtAmount");
            TextBox txtTermCondition = (TextBox)dl.FindControl("txtTermsCondition");

            if (txtUnitPrice.Text != "")
            {
                float flTemp = 0;
                if (float.TryParse(txtUnitPrice.Text, out flTemp))
                {
                    if (txtDiscountP.Text != "")
                    {
                        if (float.TryParse(txtDiscountP.Text, out flTemp))
                        {
                            if (txtPriceAfterTax.Text != "")
                            {
                                if (txtPriceAfterTax.Text == "0")
                                {
                                    txtDiscountV.Text = ((float.Parse(txtUnitPrice.Text) * float.Parse(txtDiscountP.Text)) / 100).ToString();
                                    txtAmount.Text = (float.Parse(txtUnitPrice.Text) - float.Parse(txtDiscountV.Text)).ToString();
                                }
                                else
                                {
                                    txtDiscountV.Text = ((float.Parse(txtPriceAfterTax.Text) * float.Parse(txtDiscountP.Text)) / 100).ToString();
                                    txtAmount.Text = (float.Parse(txtPriceAfterTax.Text) - float.Parse(txtDiscountV.Text)).ToString();
                                }
                                System.Web.UI.ScriptManager.GetCurrent(this).SetFocus(txtTermCondition);
                            }
                        }
                        else
                        {
                            txtTaxP.Text = "";
                            ScriptManager.RegisterStartupScript(this, typeof(string), "OPEN_WINDOW", "window.alert('Enter Numeric Value Only');", true);
                            System.Web.UI.ScriptManager.GetCurrent(this).SetFocus(txtDiscountP);
                        }
                    }
                }
                else
                {
                    txtUnitPrice.Text = "";
                    ScriptManager.RegisterStartupScript(this, typeof(string), "OPEN_WINDOW", "window.alert('Enter Numeric Value Only');", true);
                    System.Web.UI.ScriptManager.GetCurrent(this).SetFocus(txtUnitPrice);
                }
            }
            //else
            //{
            //    ScriptManager.RegisterStartupScript(this, typeof(string), "OPEN_WINDOW", "window.alert('Enter Unit Price');", true);
            //    System.Web.UI.ScriptManager.GetCurrent(this).SetFocus(txtUnitPrice);
            //}
        }
    }
    protected void txtDiscountV_TextChanged(object sender, EventArgs e)
    {
        foreach (DataListItem dl in dlProductDetail.Items)
        {
            TextBox txtUnitPrice = (TextBox)dl.FindControl("txtUnitPrice");
            TextBox txtTaxP = (TextBox)dl.FindControl("txtTaxP");
            TextBox txtTaxV = (TextBox)dl.FindControl("txtTaxV");
            TextBox txtPriceAfterTax = (TextBox)dl.FindControl("txtPriceAfterTax");
            TextBox txtDiscountP = (TextBox)dl.FindControl("txtDiscountP");
            TextBox txtDiscountV = (TextBox)dl.FindControl("txtDiscountV");
            TextBox txtAmount = (TextBox)dl.FindControl("txtAmount");
            TextBox txtTermCondition = (TextBox)dl.FindControl("txtTermsCondition");

            if (txtUnitPrice.Text != "")
            {
                float flTemp = 0;
                if (float.TryParse(txtUnitPrice.Text, out flTemp))
                {
                    if (txtDiscountV.Text != "")
                    {
                        if (float.TryParse(txtDiscountV.Text, out flTemp))
                        {
                            if (txtPriceAfterTax.Text != "")
                            {
                                if (txtPriceAfterTax.Text == "0")
                                {
                                    txtDiscountP.Text = ((100 * float.Parse(txtDiscountV.Text)) / float.Parse(txtUnitPrice.Text)).ToString();
                                    txtAmount.Text = (float.Parse(txtUnitPrice.Text) - float.Parse(txtDiscountV.Text)).ToString();
                                }
                                else
                                {
                                    txtDiscountP.Text = ((100 * float.Parse(txtDiscountV.Text)) / float.Parse(txtPriceAfterTax.Text)).ToString();
                                    txtAmount.Text = (float.Parse(txtPriceAfterTax.Text) - float.Parse(txtDiscountV.Text)).ToString();
                                }
                                System.Web.UI.ScriptManager.GetCurrent(this).SetFocus(txtTermCondition);
                            }
                        }
                        else
                        {
                            txtTaxV.Text = "";
                            ScriptManager.RegisterStartupScript(this, typeof(string), "OPEN_WINDOW", "window.alert('Enter Numeric Value Only');", true);
                            System.Web.UI.ScriptManager.GetCurrent(this).SetFocus(txtTaxV);
                        }
                    }
                }
                else
                {
                    txtUnitPrice.Text = "";
                    ScriptManager.RegisterStartupScript(this, typeof(string), "OPEN_WINDOW", "window.alert('Enter Numeric Value Only');", true);
                    System.Web.UI.ScriptManager.GetCurrent(this).SetFocus(txtUnitPrice);
                }
            }
            //else
            //{
            //    ScriptManager.RegisterStartupScript(this, typeof(string), "OPEN_WINDOW", "window.alert('Enter Unit Price');", true);
            //    System.Web.UI.ScriptManager.GetCurrent(this).SetFocus(txtUnitPrice);
            //}
        }
    }
    #endregion

    #region AllPageCode
    public void AllPageCode()
    {
        Page.Title = ObjSysParam.GetSysTitle();
        StrUserId = Session["UserId"].ToString();
        StrCompId = Session["CompId"].ToString();
        StrBrandId = Session["BrandId"].ToString();
        StrLocationId = Session["LocId"].ToString();
        
        Session["AccordianId"] = "12";
        Session["HeaderText"] = "Purchase";
        DataTable dtAllPageCode = cmn.GetAllPagePermission(StrUserId.ToString(), "12", "51");
        if (dtAllPageCode.Rows.Count != 0)
        {
            if (dtAllPageCode.Rows[0][0].ToString() == "SuperAdmin")
            {
                btnQuoteSave.Visible = true;
                btnProductSave.Visible = true;
               
                foreach (GridViewRow Row in GvPurchaseQuote.Rows)
                {
                    ((ImageButton)Row.FindControl("btnEdit")).Visible = true;
                    ((ImageButton)Row.FindControl("IbtnDelete")).Visible = true;
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
                        btnQuoteSave.Visible = true;
                        btnProductSave.Visible = true;
                    }
                    foreach (GridViewRow Row in GvPurchaseQuote.Rows)
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

        DataTable Dt = objDocNo.GetDocumentNumberAll(StrCompId, "12", "51");

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
                DocumentNo += "-" + (Convert.ToInt32(objQuoteHeader.GetQuoteHeaderAll(StrCompId.ToString(),StrBrandId,StrLocationId).Rows.Count) + 1).ToString();
            }
            else
            {
                DocumentNo += (Convert.ToInt32(objQuoteHeader.GetQuoteHeaderAll(StrCompId.ToString(), StrBrandId, StrLocationId).Rows.Count) + 1).ToString();

            }
        }
        else
        {
            DocumentNo += (Convert.ToInt32(objQuoteHeader.GetQuoteHeaderAll(StrCompId.ToString(), StrBrandId, StrLocationId).Rows.Count) + 1).ToString();
        }

        return DocumentNo;

    } 

}
