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

public partial class Purchase_PurchaseReturn : System.Web.UI.Page
{
    #region defind Class Object
    Common cmn = new Common();
    //Set_Suppliers objSupplier = new Set_Suppliers();
    Inv_PurchaseReturnHeader objPReturnHeader = new Inv_PurchaseReturnHeader();
    Inv_PurchaseReturnDetail objPReturnDetail = new Inv_PurchaseReturnDetail();
    PurchaseInvoice objPInvoice = new PurchaseInvoice();
    PurchaseInvoiceDetail objPInvoiceD = new PurchaseInvoiceDetail();
    Ems_ContactMaster objContact = new Ems_ContactMaster();
    Inv_ProductMaster objProductM = new Inv_ProductMaster();
    Inv_UnitMaster UM = new Inv_UnitMaster();
    Set_Payment_Mode_Master objPaymentMode = new Set_Payment_Mode_Master();
    SystemParameter ObjSysParam = new SystemParameter();
    Inv_StockBatchMaster ObjStockBatchMaster = new Inv_StockBatchMaster();
    Inv_ProductLedger ObjProductLadger = new Inv_ProductLedger();
    UserMaster ObjUser = new UserMaster();
    Set_DocNumber objDocNo = new Set_DocNumber();
    string StrCompId = string.Empty;
    string StrBrandId = string.Empty;
    string strLocationId = string.Empty;
    string StrUserId =string.Empty;
    #endregion

    protected void Page_Load(object sender, EventArgs e)
    {
        //Remove
        StrUserId = Session["UserId"].ToString();
        StrCompId = Session["CompId"].ToString();
        StrBrandId = Session["BrandId"].ToString();
        strLocationId = Session["LocId"].ToString();
        //End Remove
        if (Session["UserId"] == null)
        {
            Response.Redirect("~/Login.aspx");
        }
        if (!IsPostBack)
        {
            ddlOption.SelectedIndex = 2;
            btnList_Click(null, null);
            FillGridBin();
            FillGrid();
            FillPaymentMode();
            txtPInvoiceNo.Visible = true;
            //txtReturnNo.Text = objPReturnHeader.GetAutoID(StrCompId, StrBrandId, strLocationId);
            txtReturnNo.Text = GetDocumentNumber();
            txtReturnDate.Text = DateTime.Now.ToString(ObjSysParam.GetSysParameterByParamName("Date_Format").Rows[0]["Param_Value"].ToString());
        }
        AllPageCode();
    }

    #region System defind Funcation
    protected void btnList_Click(object sender, EventArgs e)
    {
        pnlMenuList.BackColor = System.Drawing.ColorTranslator.FromHtml("#ccddee");
        pnlMenuNew.BackColor = System.Drawing.ColorTranslator.FromHtml("#90bde9");
        pnlMenuBin.BackColor = System.Drawing.ColorTranslator.FromHtml("#90bde9");

        PnlList.Visible = true;
        PnlNewEdit.Visible = false;
        PnlBin.Visible = false;
    }
    protected void btnNew_Click(object sender, EventArgs e)
    {
        pnlMenuNew.BackColor = System.Drawing.ColorTranslator.FromHtml("#ccddee");

        pnlMenuList.BackColor = System.Drawing.ColorTranslator.FromHtml("#90bde9");
        pnlMenuBin.BackColor = System.Drawing.ColorTranslator.FromHtml("#90bde9");

        PnlList.Visible = false;
        PnlNewEdit.Visible = true;
        PnlBin.Visible = false;
        txtPInvoiceNo.Visible = true;
    }
    protected void btnEdit_Command(object sender, CommandEventArgs e)
    {
        hdnPReturnId.Value = e.CommandArgument.ToString();

        DataTable dtPReturnEdit = objPReturnHeader.GetPRHeaderAllDataByTransId(StrCompId, StrBrandId, strLocationId, hdnPReturnId.Value);

        btnNew.Text = Resources.Attendance.Edit;
        if (dtPReturnEdit.Rows.Count > 0)
        {
            txtReturnNo.Text = dtPReturnEdit.Rows[0]["PReturn_No"].ToString();
            txtReturnNo.ReadOnly = true;
            txtReturnDate.Text = Convert.ToDateTime(dtPReturnEdit.Rows[0]["PRDate"].ToString()).ToString(ObjSysParam.GetSysParameterByParamName("Date_Format").Rows[0]["Param_Value"].ToString());
            ddlInvoiceType.SelectedValue = dtPReturnEdit.Rows[0]["InvoiceType"].ToString();
            ddlPaymentMode.SelectedValue = dtPReturnEdit.Rows[0]["PaymentModeID"].ToString();
            txtRemark.Text = dtPReturnEdit.Rows[0]["Remark"].ToString();

            //Add Invoice Detail
            string strInvoiceId = dtPReturnEdit.Rows[0]["Invoice_Id"].ToString();
            if (strInvoiceId != "" && strInvoiceId != "0")
            {
                DataTable dtInvoiceNo = objPInvoice.GetPurchaseInvoiceTrueAllByTransId(StrCompId, StrBrandId, strLocationId, strInvoiceId);
                if (dtInvoiceNo.Rows.Count > 0)
                {
                    hdnInvoiceId.Value = dtInvoiceNo.Rows[0]["TransID"].ToString();
                    txtPInvoiceNo.Text = dtInvoiceNo.Rows[0]["InvoiceNo"].ToString();
                    string strSupplierId = dtInvoiceNo.Rows[0]["SupplierId"].ToString();
                    txtPInvoiceDate.Text = Convert.ToDateTime(dtInvoiceNo.Rows[0]["InvoiceDate"].ToString()).ToString(ObjSysParam.GetSysParameterByParamName("Date_Format").Rows[0]["Param_Value"].ToString());

                    //Add Child Detail
                    DataTable dtDetail = objPInvoiceD.GetPurchaseInvoiceDetailByInvoiceNo(StrCompId, StrBrandId, strLocationId, hdnInvoiceId.Value);
                    if (dtDetail.Rows.Count > 0)
                    {
                        GvInvoiceDetail.DataSource = dtDetail;
                        GvInvoiceDetail.DataBind();
                    }
                    else
                    {
                        GvInvoiceDetail.DataSource = dtDetail;
                        GvInvoiceDetail.DataBind();
                    }

                    //Add Supplier Name
                    if (strSupplierId != "" && strSupplierId != "0")
                    {
                        DataTable dtSupplier = objContact.GetContactByContactId(StrCompId, strSupplierId);
                        if (dtSupplier.Rows.Count > 0)
                        {
                            txtSupplierName.Text = dtSupplier.Rows[0]["Contact_Name"].ToString() + "/" + dtSupplier.Rows[0]["Contact_Id"].ToString();
                        }
                        else
                        {
                            txtSupplierName.Text = "";
                        }
                    }
                    else
                    {
                        txtSupplierName.Text = "";
                    }
                }
                else
                {
                    txtPInvoiceDate.Text = "";
                    txtSupplierName.Text = "";
                }
            }


            //Add Child Concept
            foreach (GridViewRow gvr in GvInvoiceDetail.Rows)
            {
                HiddenField hdnProductId = (HiddenField)gvr.FindControl("hdnIProductId");
                HiddenField hdnQty = (HiddenField)gvr.FindControl("hdnQty");
                TextBox txtReturnQty = (TextBox)gvr.FindControl("txtReturnQty");

                if (hdnProductId.Value != "0" && hdnProductId.Value != "")
                {
                    DataTable dtDetail = objPReturnDetail.GetPRDetailWithPRNoAndProductId(StrCompId, StrBrandId, strLocationId, hdnPReturnId.Value, hdnProductId.Value);
                    if (dtDetail.Rows.Count > 0)
                    {
                        txtReturnQty.Text = dtDetail.Rows[0]["ReturnQty"].ToString();
                        hdnQty.Value = dtDetail.Rows[0]["ReturnQty"].ToString();
                    }
                }
            }
        }
        btnNew_Click(null, null);
        System.Web.UI.ScriptManager.GetCurrent(this).SetFocus(txtSupplierName);
    }
    protected void GvPReturn_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        GvPReturn.PageIndex = e.NewPageIndex;
        DataTable dt = (DataTable)Session["dtFilter"];
        GvPReturn.DataSource = dt;
        GvPReturn.DataBind();
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
            GvPReturn.DataSource = view.ToTable();
            Session["dtFilter"] = view.ToTable();
            lblTotalRecords.Text = Resources.Attendance.Total_Records + " : " + view.ToTable().Rows.Count + "";
            GvPReturn.DataBind();
            AllPageCode();
        }
    }
    protected void GvPReturn_Sorting(object sender, GridViewSortEventArgs e)
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
        GvPReturn.DataSource = dt;
        GvPReturn.DataBind();
        AllPageCode();
    }
    protected void IbtnDelete_Command(object sender, CommandEventArgs e)
    {
        hdnPReturnId.Value = e.CommandArgument.ToString();
        int b = 0;
        b = objPReturnHeader.DeletePRHeader(StrCompId, StrBrandId, strLocationId, hdnPReturnId.Value, "false", StrUserId, DateTime.Now.ToString());
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
    protected void btnPReturnCancel_Click(object sender, EventArgs e)
    {
        Reset();
        btnList_Click(null, null);
        FillGridBin();
        FillGrid();

    }
    protected void BtnReset_Click(object sender, EventArgs e)
    {
        Reset();
        System.Web.UI.ScriptManager.GetCurrent(this).SetFocus(txtReturnDate);
    }
    protected void btnPReturnSave_Click(object sender, EventArgs e)
    {
        if (txtReturnNo.Text == "")
        {
            DisplayMessage("Enter Return No.");
            System.Web.UI.ScriptManager.GetCurrent(this).SetFocus(txtReturnNo);
            return;
        }
        else
        {
            if (hdnPReturnId.Value == "0")
            {
                DataTable dtReturnNo = objPReturnHeader.GetPRHeaderAllDataByPReturn_No(StrCompId, StrBrandId, strLocationId, txtReturnNo.Text);
                if (dtReturnNo.Rows.Count > 0)
                {
                    DisplayMessage("Purchase Return No. Already Exits");
                    txtReturnNo.Text = "";
                    System.Web.UI.ScriptManager.GetCurrent(this).SetFocus(txtReturnNo);
                    return;
                }
            }
        }
        if (txtSupplierName.Text == "")
        {
            DisplayMessage("Fill Supplier Name");
            txtSupplierName.Focus();
            return;
        }

        if (txtPInvoiceNo.Visible == true)
        {
            if (txtPInvoiceNo.Text == "")
            {
                DisplayMessage("Enter Purchase Invoice No.");
                txtPInvoiceNo.Focus();
                return;
            }
        }
        else if (txtPInvoiceNo.Visible == false)
        {
            if (ddlPIncoiceNo.Visible == true)
            {
                if (ddlPIncoiceNo.SelectedValue == "--Select--")
                {
                    DisplayMessage("Select Purchase Invoice No.");
                    ddlPIncoiceNo.Focus();
                    return;
                }
            }
        }

        if (ddlInvoiceType.SelectedValue == "--Select--")
        {
            DisplayMessage("Select Invoice Type");
            ddlInvoiceType.Focus();
            return;
        }
        if (ddlPaymentMode.SelectedValue == "--Select--")
        {
            DisplayMessage("Select Payment Mode");
            ddlPaymentMode.Focus();
            return;
        }
        if (txtRemark.Text == "")
        {
            DisplayMessage("Enter Remark");
            txtRemark.Focus();
            return;
        }

        int b = 0;
        if (hdnPReturnId.Value != "0")
        {
            b = objPReturnHeader.UpdatePRHeader(StrCompId, StrBrandId, strLocationId, hdnPReturnId.Value, txtReturnNo.Text, txtReturnDate.Text, hdnInvoiceId.Value, ddlPaymentMode.SelectedValue, ddlInvoiceType.SelectedValue, txtRemark.Text, "", "", "", "", "", "True", DateTime.Now.ToString(), "True", Session["UserId"].ToString(), DateTime.Now.ToString());

            //Add Detail Section.
            objPReturnDetail.DeletePRDetail(StrCompId, StrBrandId, strLocationId, hdnPReturnId.Value);
            foreach (GridViewRow gvr in GvInvoiceDetail.Rows)
            {
                Label lblSerialNo = (Label)gvr.FindControl("lblSerialNo");
                HiddenField hdnProductId = (HiddenField)gvr.FindControl("hdnIProductId");
                HiddenField hdnIUnitId = (HiddenField)gvr.FindControl("hdnIUnitId");
                TextBox txtReturnQty = (TextBox)gvr.FindControl("txtReturnQty");
                Label lblUnitCost = (Label)gvr.FindControl("lblIUnitCost");

                objPReturnDetail.InsertPRDetail(StrCompId, StrBrandId, strLocationId, hdnPReturnId.Value, hdnProductId.Value, lblSerialNo.Text, txtReturnQty.Text, lblUnitCost.Text, "", "", "", "", "", "True", DateTime.Now.ToString(), "True", Session["UserId"].ToString(), DateTime.Now.ToString(), Session["UserId"].ToString(), DateTime.Now.ToString());
                ObjProductLadger.DeleteProductLedger(StrCompId, StrBrandId, strLocationId, "PR", hdnPReturnId.Value, hdnProductId.Value);
                ObjProductLadger.InsertProductLedger(StrCompId.ToString(), StrBrandId.ToString(), strLocationId.ToString(), "PR", hdnPReturnId.Value, "0", hdnProductId.Value, hdnIUnitId.Value, "O", "0", "0", "0", txtReturnQty.Text, "1/1/1800", "0", "1/1/1800", "0", "1/1/1800", "0", "", "", "", "", "", true.ToString(), "1/1/1800", true.ToString(), Session["UserId"].ToString().ToString(), "1/1/1800", Session["UserId"].ToString().ToString(), "1/1/1800");
   

            }
            //End  

            if (b != 0)
            {
                DisplayMessage("Record Updated");
                Reset();
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
            b = objPReturnHeader.InsertPRHeader(StrCompId, StrBrandId, strLocationId, txtReturnNo.Text, txtReturnDate.Text, hdnInvoiceId.Value, ddlPaymentMode.SelectedValue, ddlInvoiceType.SelectedValue, txtRemark.Text, "", "", "", "", "", "True", DateTime.Now.ToString(), "True", Session["UserId"].ToString(), DateTime.Now.ToString(), Session["UserId"].ToString(), DateTime.Now.ToString());

            string strMaxId = string.Empty;
            DataTable dtMaxId = objPReturnHeader.GetMaxTransId(StrCompId, StrBrandId, strLocationId);
            if (dtMaxId.Rows.Count > 0)
            {
                strMaxId = dtMaxId.Rows[0][0].ToString();
                //Add Detail Section.
                objPReturnDetail.DeletePRDetail(StrCompId, StrBrandId, strLocationId, strMaxId);
                foreach (GridViewRow gvr in GvInvoiceDetail.Rows)
                {
                    Label lblSerialNo = (Label)gvr.FindControl("lblSerialNo");
                    HiddenField hdnProductId = (HiddenField)gvr.FindControl("hdnIProductId");
                    TextBox txtReturnQty = (TextBox)gvr.FindControl("txtReturnQty");
                    Label lblUnitCost = (Label)gvr.FindControl("lblIUnitCost");

                    HiddenField hdnIUnitId = (HiddenField)gvr.FindControl("hdnIUnitId");


                    objPReturnDetail.InsertPRDetail(StrCompId, StrBrandId, strLocationId, strMaxId, hdnProductId.Value, lblSerialNo.Text, txtReturnQty.Text, lblUnitCost.Text, "", "", "", "", "", "True", DateTime.Now.ToString(), "True", Session["UserId"].ToString(), DateTime.Now.ToString(), Session["UserId"].ToString(), DateTime.Now.ToString());
                    ObjProductLadger.InsertProductLedger(StrCompId.ToString(), StrBrandId.ToString(), strLocationId.ToString(), "PR", strMaxId, "0", hdnProductId.Value, hdnIUnitId.Value, "O", "0", "0", "0", txtReturnQty.Text, "1/1/1800", "0", "1/1/1800", "0", "1/1/1800", "0", "", "", "", "", "", true.ToString(), "1/1/1800", true.ToString(), Session["UserId"].ToString().ToString(), "1/1/1800", Session["UserId"].ToString().ToString(), "1/1/1800");
   
                }
                //End  
            }

            if (b != 0)
            {
                DisplayMessage("Record Saved");
                Reset();
            }
            else
            {
                DisplayMessage("Record  Not Saved");
            }
        }
    }
    private string GetSupplierId()
    {
        string retval = string.Empty;
        if (txtSupplierName.Text != "")
        {
            DataTable dtSupp = objContact.GetContactByContactName(StrCompId.ToString(), txtSupplierName.Text.Trim().Split('/')[0].ToString());
            if (dtSupp.Rows.Count > 0)
            {
                retval = (txtSupplierName.Text.Split('/'))[txtSupplierName.Text.Split('/').Length - 1];

                DataTable dtCompany = objContact.GetContactByContactId(StrCompId, retval);
                if (dtCompany.Rows.Count > 0)
                {

                }
                else
                {
                    retval = "";
                }
            }
            else
            {
                retval = "";
            }
        }
        else
        {
            retval = "";
        }
        return retval;
    }
    protected void IbtnRestore_Command(object sender, CommandEventArgs e)
    {
        hdnPReturnId.Value = e.CommandArgument.ToString();
        int b = 0;
        b = objPReturnHeader.DeletePRHeader(StrCompId, StrBrandId, strLocationId, hdnPReturnId.Value, true.ToString(), StrUserId, DateTime.Now.ToString());
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

    #region Auto Complete
    [System.Web.Services.WebMethodAttribute(), System.Web.Script.Services.ScriptMethodAttribute()]
    public static string[] GetCompletionListSupplier(string prefixText, int count, string contextKey)
    {
        Ems_ContactMaster objSupplier = new Ems_ContactMaster();
        DataTable dtSupplier = objSupplier.GetDistinctSupplierName("1", prefixText);
        string[] txt = new string[dtSupplier.Rows.Count];

        if (dtSupplier.Rows.Count > 0)
        {
            for (int i = 0; i < dtSupplier.Rows.Count; i++)
            {
                txt[i] = dtSupplier.Rows[i]["Contact_Name"].ToString() + "/" + dtSupplier.Rows[i]["Contact_Id"].ToString() + "";
            }
        }
        else
        {
            //if (prefixText.Length > 2)
            //{
            //    txt = null;
            //}
            //else
            //{
            dtSupplier = objSupplier.GetAllSupplierName("1");
            if (dtSupplier.Rows.Count > 0)
            {
                txt = new string[dtSupplier.Rows.Count];
                for (int i = 0; i < dtSupplier.Rows.Count; i++)
                {
                    txt[i] = dtSupplier.Rows[i]["Contact_Name"].ToString() + "/" + dtSupplier.Rows[i]["Contact_Id"].ToString() + "";
                }
            }
            //}
        }
        return txt;
    }
    [System.Web.Services.WebMethodAttribute(), System.Web.Script.Services.ScriptMethodAttribute()]
    public static string[] GetCompletionListInvoiceNo(string prefixText, int count, string contextKey)
    {
        PurchaseInvoice objPInvoice = new PurchaseInvoice();
        DataTable dtInvoiceNo = objPInvoice.GetDistinctInvoiceNo("1", "1", "1", prefixText);
        string[] txt = new string[dtInvoiceNo.Rows.Count];

        if (dtInvoiceNo.Rows.Count > 0)
        {
            for (int i = 0; i < dtInvoiceNo.Rows.Count; i++)
            {
                txt[i] = dtInvoiceNo.Rows[i]["InvoiceNo"].ToString();
            }
        }
        else
        {
            if (prefixText.Length > 2)
            {
                txt = null;
            }
            else
            {
                dtInvoiceNo = objPInvoice.GetPurchaseInvoiceTrueAll("1", "1", "1");
                if (dtInvoiceNo.Rows.Count > 0)
                {
                    txt = new string[dtInvoiceNo.Rows.Count];
                    for (int i = 0; i < dtInvoiceNo.Rows.Count; i++)
                    {
                        txt[i] = dtInvoiceNo.Rows[i]["InvoiceNo"].ToString();
                    }
                }
            }
        }
        return txt;
    }
    #endregion

    #region Bin Section
    protected void btnBin_Click(object sender, EventArgs e)
    {
        pnlMenuBin.BackColor = System.Drawing.ColorTranslator.FromHtml("#ccddee");

        pnlMenuNew.BackColor = System.Drawing.ColorTranslator.FromHtml("#90bde9");
        pnlMenuList.BackColor = System.Drawing.ColorTranslator.FromHtml("#90bde9");
        PnlNewEdit.Visible = false;
        PnlBin.Visible = true;
        PnlList.Visible = false;
        FillGridBin();
    }
    protected void GvPReturnBin_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        GvPReturnBin.PageIndex = e.NewPageIndex;

        DataTable dt = (DataTable)Session["dtInactive"];
        GvPReturnBin.DataSource = dt;
        GvPReturnBin.DataBind();

        string temp = string.Empty;

        for (int i = 0; i < GvPReturnBin.Rows.Count; i++)
        {
            Label lblconid = (Label)GvPReturnBin.Rows[i].FindControl("lblgvReturnId");
            string[] split = lblSelectedRecord.Text.Split(',');

            for (int j = 0; j < lblSelectedRecord.Text.Split(',').Length; j++)
            {
                if (lblSelectedRecord.Text.Split(',')[j] != "")
                {
                    if (lblconid.Text.Trim().ToString() == lblSelectedRecord.Text.Split(',')[j].Trim().ToString())
                    {
                        ((CheckBox)GvPReturnBin.Rows[i].FindControl("chkSelect")).Checked = true;
                    }
                }
            }
        }
        AllPageCode();
    }
    protected void GvPReturnBin_OnSorting(object sender, GridViewSortEventArgs e)
    {
        HDFSortbin.Value = HDFSortbin.Value == "ASC" ? "DESC" : "ASC";
        DataTable dt = new DataTable();
        dt = objPReturnHeader.GetPRHeaderAllFalse(StrCompId, StrBrandId, strLocationId);
        DataView dv = new DataView(dt);
        string Query = "" + e.SortExpression + " " + HDFSortbin.Value + "";
        dv.Sort = Query;
        dt = dv.ToTable();
        Session["dtInactive"] = dt;
        GvPReturnBin.DataSource = dt;
        GvPReturnBin.DataBind();
        lblSelectedRecord.Text = "";
        AllPageCode();
    }
    public void FillGridBin()
    {
        DataTable dt = new DataTable();
        dt = objPReturnHeader.GetPRHeaderAllFalse(StrCompId, StrBrandId, strLocationId);
        GvPReturnBin.DataSource = dt;
        GvPReturnBin.DataBind();
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
            GvPReturnBin.DataSource = view.ToTable();
            GvPReturnBin.DataBind();
            lblSelectedRecord.Text = "";
            if (view.ToTable().Rows.Count == 0)
            {
                FillGridBin();
            }
            System.Web.UI.ScriptManager.GetCurrent(this).SetFocus(txtValueBin);
        }
        AllPageCode();
    }
    protected void btnRestoreSelected_Click(object sender, CommandEventArgs e)
    {
        int b = 0;
        DataTable dt = objPReturnHeader.GetPRHeaderAllFalse(StrCompId, StrBrandId, strLocationId);

        if (GvPReturnBin.Rows.Count != 0)
        {
            if (lblSelectedRecord.Text != "")
            {
                for (int j = 0; j < lblSelectedRecord.Text.Split(',').Length; j++)
                {
                    if (lblSelectedRecord.Text.Split(',')[j] != "")
                    {
                        b = objPReturnHeader.DeletePRHeader(StrCompId, StrBrandId, strLocationId, lblSelectedRecord.Text.Split(',')[j].Trim().ToString(), true.ToString(), Session["UserId"].ToString(), DateTime.Now.ToString());
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
                foreach (GridViewRow Gvr in GvPReturnBin.Rows)
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
        CheckBox chkSelAll = ((CheckBox)GvPReturnBin.HeaderRow.FindControl("chkCurrent"));
        for (int i = 0; i < GvPReturnBin.Rows.Count; i++)
        {
            ((CheckBox)GvPReturnBin.Rows[i].FindControl("chkSelect")).Checked = chkSelAll.Checked;
            if (chkSelAll.Checked)
            {
                if (!lblSelectedRecord.Text.Split(',').Contains(((Label)(GvPReturnBin.Rows[i].FindControl("lblgvReturnId"))).Text.Trim().ToString()))
                {
                    lblSelectedRecord.Text += ((Label)(GvPReturnBin.Rows[i].FindControl("lblgvReturnId"))).Text.Trim().ToString() + ",";
                }
            }
            else
            {
                string temp = string.Empty;
                string[] split = lblSelectedRecord.Text.Split(',');
                foreach (string item in split)
                {
                    if (item != ((Label)(GvPReturnBin.Rows[i].FindControl("lblgvReturnId"))).Text.Trim().ToString())
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
        Label lb = (Label)GvPReturnBin.Rows[index].FindControl("lblgvReturnId");
        if (((CheckBox)GvPReturnBin.Rows[index].FindControl("chkSelect")).Checked)
        {
            empidlist += lb.Text.Trim().ToString() + ",";
            lblSelectedRecord.Text += empidlist;
        }
        else
        {
            empidlist += lb.Text.ToString().Trim();
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
            for (int i = 0; i < GvPReturnBin.Rows.Count; i++)
            {
                string[] split = lblSelectedRecord.Text.Split(',');
                Label lblconid = (Label)GvPReturnBin.Rows[i].FindControl("lblgvReturnId");
                for (int j = 0; j < lblSelectedRecord.Text.Split(',').Length; j++)
                {
                    if (lblSelectedRecord.Text.Split(',')[j] != "")
                    {
                        if (lblconid.Text.Trim().ToString() == lblSelectedRecord.Text.Split(',')[j].Trim().ToString())
                        {
                            ((CheckBox)GvPReturnBin.Rows[i].FindControl("chkSelect")).Checked = true;
                        }
                    }
                }
            }
        }
        else
        {
            lblSelectedRecord.Text = "";
            DataTable dtUnit1 = (DataTable)Session["dtInactive"];
            GvPReturnBin.DataSource = dtUnit1;
            GvPReturnBin.DataBind();
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
                    b = objPReturnHeader.DeletePRHeader(StrCompId, StrBrandId, strLocationId, lblSelectedRecord.Text.Split(',')[j].Trim(), true.ToString(), Session["UserId"].ToString(), DateTime.Now.ToString());
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
            foreach (GridViewRow Gvr in GvPReturnBin.Rows)
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
        DataTable dtBrand = objPReturnHeader.GetPRHeaderAllTrue(StrCompId, StrBrandId, strLocationId);
        lblTotalRecords.Text = Resources.Attendance.Total_Records + " : " + dtBrand.Rows.Count + "";
        Session["dtCurrency"] = dtBrand;
        Session["dtFilter"] = dtBrand;
        if (dtBrand != null && dtBrand.Rows.Count > 0)
        {
            GvPReturn.DataSource = dtBrand;
            GvPReturn.DataBind();
        }
        else
        {
            GvPReturn.DataSource = null;
            GvPReturn.DataBind();
        }
        lblTotalRecords.Text = Resources.Attendance.Total_Records + " : " + dtBrand.Rows.Count.ToString() + "";
        AllPageCode();
    }
    private void FillPaymentMode()
    {
        DataTable dsPaymentMode = null;
        dsPaymentMode = objPaymentMode.GetPaymentModeMaster(StrCompId);
        if (dsPaymentMode.Rows.Count > 0)
        {
            ddlPaymentMode.DataSource = dsPaymentMode;
            ddlPaymentMode.DataTextField = "Pay_Mod_Name";
            ddlPaymentMode.DataValueField = "Pay_Mode_Id";
            ddlPaymentMode.DataBind();

            ddlPaymentMode.Items.Add("--Select--");
            ddlPaymentMode.SelectedValue = "--Select--";
        }
        else
        {
            ddlPaymentMode.Items.Add("--Select--");
            ddlPaymentMode.SelectedValue = "--Select--";
        }
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
        FillGrid();
        FillPaymentMode();
        txtPInvoiceNo.Visible = true;
        txtPInvoiceNo.Text = "";
        ddlPIncoiceNo.Visible = false;
        txtSupplierName.Text = "";
        txtReturnNo.Text = objPReturnHeader.GetAutoID(StrCompId, StrBrandId, strLocationId);
        txtReturnNo.ReadOnly = false;
        txtReturnDate.Text = DateTime.Now.ToString(ObjSysParam.GetSysParameterByParamName("Date_Format").Rows[0]["Param_Value"].ToString());
        ddlInvoiceType.SelectedValue = "--Select--";
        txtRemark.Text = "";

        GvInvoiceDetail.DataSource = null;
        GvInvoiceDetail.DataBind();

        hdnPReturnId.Value = "0";
        hdnInvoiceId.Value = "0";

        btnNew.Text = Resources.Attendance.New;
        ddlFieldName.SelectedIndex = 1;
        ddlOption.SelectedIndex = 2;
        txtValue.Text = "";
        txtValueBin.Text = "";
        lblSelectedRecord.Text = "";
        System.Web.UI.ScriptManager.GetCurrent(this).SetFocus(txtValue);
        txtReturnNo.Text = GetDocumentNumber();
    }
    public string getQty(string Qty)
    {
        if (Qty == "")
        {
            Qty = "0";

        }
        return Qty;
    }
    #endregion
    #region Invoice Section
    private void FillInvoiceNo(string strSupplierId)
    {
        DataTable dsInvoiceNo = null;
        dsInvoiceNo = objPInvoice.GetInvoiceNoBySupplierId(StrCompId, StrBrandId, strLocationId, strSupplierId);
        if (dsInvoiceNo.Rows.Count > 0)
        {
            ddlPIncoiceNo.DataSource = dsInvoiceNo;
            ddlPIncoiceNo.DataTextField = "InvoiceNo";
            ddlPIncoiceNo.DataValueField = "TransID";
            ddlPIncoiceNo.DataBind();

            ddlPIncoiceNo.Items.Add("--Select--");
            ddlPIncoiceNo.SelectedValue = "--Select--";
        }
        else
        {
            ddlPIncoiceNo.Items.Add("--Select--");
            ddlPIncoiceNo.SelectedValue = "--Select--";
        }
    }
    protected void txtSupplierName_TextChanged(object sender, EventArgs e)
    {
        string strSupplierId = string.Empty;
        if (txtSupplierName.Text != "")
        {
            strSupplierId = GetSupplierId();
            if (strSupplierId != "" && strSupplierId != "0")
            {
                FillInvoiceNo(strSupplierId);
                ddlPIncoiceNo.Visible = true;
                txtPInvoiceNo.Visible = false;
            }
            else
            {
                DisplayMessage("Select In Suggestions Only");
                txtSupplierName.Text = "";
                System.Web.UI.ScriptManager.GetCurrent(this).SetFocus(txtSupplierName);
            }
        }
    }
    protected void txtPInvoiceNo_TextChanged(object sender, EventArgs e)
    {
        if (txtPInvoiceNo.Text != "")
        {
            DataTable dtInvoiceNo = objPInvoice.GetDataByInvoiceNo(StrCompId, StrBrandId, strLocationId, txtPInvoiceNo.Text);
            if (dtInvoiceNo.Rows.Count > 0)
            {
                hdnInvoiceId.Value = dtInvoiceNo.Rows[0]["TransID"].ToString();
                string strSupplierId = dtInvoiceNo.Rows[0]["SupplierId"].ToString();
                txtPInvoiceDate.Text = Convert.ToDateTime(dtInvoiceNo.Rows[0]["InvoiceDate"].ToString()).ToString(ObjSysParam.GetSysParameterByParamName("Date_Format").Rows[0]["Param_Value"].ToString());

                //Add Child Detail
                DataTable dtDetail = objPInvoiceD.GetPurchaseInvoiceDetailByInvoiceNo(StrCompId, StrBrandId, strLocationId, hdnInvoiceId.Value);
                if (dtDetail.Rows.Count > 0)
                {
                    GvInvoiceDetail.DataSource = dtDetail;
                    GvInvoiceDetail.DataBind();
                }
                else
                {
                    GvInvoiceDetail.DataSource = dtDetail;
                    GvInvoiceDetail.DataBind();
                }

                //Add Supplier Name
                if (strSupplierId != "" && strSupplierId != "0")
                {
                    DataTable dtSupplier = objContact.GetContactByContactId(StrCompId, strSupplierId);
                    if (dtSupplier.Rows.Count > 0)
                    {
                        txtSupplierName.Text = dtSupplier.Rows[0]["Contact_Name"].ToString() + "/" + dtSupplier.Rows[0]["Contact_Id"].ToString();
                    }
                    else
                    {
                        txtSupplierName.Text = "";
                    }
                }
                else
                {
                    txtSupplierName.Text = "";
                }
            }
            else
            {
                txtPInvoiceDate.Text = "";
                txtSupplierName.Text = "";
            }
        }
        else
        {
            txtPInvoiceDate.Text = "";
            txtSupplierName.Text = "";
            DisplayMessage("Select In Suggestions Only");
            txtPInvoiceNo.Text = "";
            System.Web.UI.ScriptManager.GetCurrent(this).SetFocus(txtPInvoiceNo);
        }

        //Add Edit Section
        if (hdnInvoiceId.Value != "" && hdnInvoiceId.Value != "0")
        {
            DataTable dtReturnData = objPReturnHeader.GetPRHeaderAllDataByInvoiceId(StrCompId, StrBrandId, strLocationId, hdnInvoiceId.Value);
            if (dtReturnData.Rows.Count > 0)
            {
                btnNew.Text = Resources.Attendance.Edit;
                hdnPReturnId.Value = dtReturnData.Rows[0]["Trans_Id"].ToString();
                txtReturnNo.Text = dtReturnData.Rows[0]["PReturn_No"].ToString();
                txtReturnNo.ReadOnly = true;
                txtReturnDate.Text = Convert.ToDateTime(dtReturnData.Rows[0]["PRDate"].ToString()).ToString(ObjSysParam.GetSysParameterByParamName("Date_Format").Rows[0]["Param_Value"].ToString());
                ddlInvoiceType.SelectedValue = dtReturnData.Rows[0]["InvoiceType"].ToString();
                ddlPaymentMode.SelectedValue = dtReturnData.Rows[0]["PaymentModeID"].ToString();
                txtRemark.Text = dtReturnData.Rows[0]["Remark"].ToString();
                foreach (GridViewRow gvr in GvInvoiceDetail.Rows)
                {
                    HiddenField hdnProductId = (HiddenField)gvr.FindControl("hdnIProductId");
                    TextBox txtReturnQty = (TextBox)gvr.FindControl("txtReturnQty");

                    if (hdnProductId.Value != "0" && hdnProductId.Value != "")
                    {
                        DataTable dtDetail = objPReturnDetail.GetPRDetailWithPRNoAndProductId(StrCompId, StrBrandId, strLocationId, hdnPReturnId.Value, hdnProductId.Value);
                        if (dtDetail.Rows.Count > 0)
                        {
                            txtReturnQty.Text = dtDetail.Rows[0]["ReturnQty"].ToString();
                        }
                    }
                }
            }
            else
            {
                btnNew.Text = Resources.Attendance.New;
                hdnPReturnId.Value = "0";
                FillPaymentMode();
                txtReturnNo.Text = objPReturnHeader.GetAutoID(StrCompId, StrBrandId, strLocationId);
                txtReturnNo.ReadOnly = false;
                txtReturnDate.Text = DateTime.Now.ToString(ObjSysParam.GetSysParameterByParamName("Date_Format").Rows[0]["Param_Value"].ToString());
                ddlInvoiceType.SelectedValue = "--Select--";
                txtRemark.Text = "";
            }
        }
    }
    protected void ddlPIncoiceNo_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (ddlPIncoiceNo.SelectedValue != "--Select--")
        {
            hdnInvoiceId.Value = ddlPIncoiceNo.SelectedValue;
            DataTable dtData = objPInvoice.GetPurchaseInvoiceTrueAllByTransId(StrCompId, StrBrandId, strLocationId, ddlPIncoiceNo.SelectedValue);
            if (dtData.Rows.Count > 0)
            {
                txtPInvoiceDate.Text = Convert.ToDateTime(dtData.Rows[0]["InvoiceDate"].ToString()).ToString(ObjSysParam.GetSysParameterByParamName("Date_Format").Rows[0]["Param_Value"].ToString());

                //Add Child Detail
                DataTable dtDetail = objPInvoiceD.GetPurchaseInvoiceDetailByInvoiceNo(StrCompId, StrBrandId, strLocationId, hdnInvoiceId.Value);
                if (dtDetail.Rows.Count > 0)
                {
                    GvInvoiceDetail.DataSource = dtDetail;
                    GvInvoiceDetail.DataBind();
                }
                else
                {
                    GvInvoiceDetail.DataSource = dtDetail;
                    GvInvoiceDetail.DataBind();
                }
            }
            else
            {
                txtPInvoiceDate.Text = "";
            }
        }
        else
        {
            txtPInvoiceDate.Text = "";
        }

        //Add Edit Section
        if (hdnInvoiceId.Value != "" && hdnInvoiceId.Value != "0")
        {
            DataTable dtReturnData = objPReturnHeader.GetPRHeaderAllDataByInvoiceId(StrCompId, StrBrandId, strLocationId, hdnInvoiceId.Value);
            if (dtReturnData.Rows.Count > 0)
            {
                btnNew.Text = Resources.Attendance.Edit;
                hdnPReturnId.Value = dtReturnData.Rows[0]["Trans_Id"].ToString();
                txtReturnNo.Text = dtReturnData.Rows[0]["PReturn_No"].ToString();
                txtReturnNo.ReadOnly = true;
                txtReturnDate.Text = Convert.ToDateTime(dtReturnData.Rows[0]["PRDate"].ToString()).ToString(ObjSysParam.GetSysParameterByParamName("Date_Format").Rows[0]["Param_Value"].ToString());
                ddlInvoiceType.SelectedValue = dtReturnData.Rows[0]["InvoiceType"].ToString();
                ddlPaymentMode.SelectedValue = dtReturnData.Rows[0]["PaymentModeID"].ToString();
                txtRemark.Text = dtReturnData.Rows[0]["Remark"].ToString();
                foreach (GridViewRow gvr in GvInvoiceDetail.Rows)
                {
                    HiddenField hdnProductId = (HiddenField)gvr.FindControl("hdnIProductId");
                    TextBox txtReturnQty = (TextBox)gvr.FindControl("txtReturnQty");

                    if (hdnProductId.Value != "0" && hdnProductId.Value != "")
                    {
                        DataTable dtDetail = objPReturnDetail.GetPRDetailWithPRNoAndProductId(StrCompId, StrBrandId, strLocationId, hdnPReturnId.Value, hdnProductId.Value);
                        if (dtDetail.Rows.Count > 0)
                        {
                            txtReturnQty.Text = dtDetail.Rows[0]["ReturnQty"].ToString();
                        }
                    }
                }
            }
            else
            {
                btnNew.Text = Resources.Attendance.New;
                hdnPReturnId.Value = "0";
                FillPaymentMode();
                txtReturnNo.Text = objPReturnHeader.GetAutoID(StrCompId, StrBrandId, strLocationId);
                txtReturnNo.ReadOnly = false;
                txtReturnDate.Text = DateTime.Now.ToString(ObjSysParam.GetSysParameterByParamName("Date_Format").Rows[0]["Param_Value"].ToString());
                ddlInvoiceType.SelectedValue = "--Select--";
                txtRemark.Text = "";
            }
        }
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
    protected void txtReturnQty_TextChanged(object sender, EventArgs e)
    {
        foreach (GridViewRow gvr in GvInvoiceDetail.Rows)
        {
            Label lblReceivedQty = (Label)gvr.FindControl("lblIReceiveQty");
            TextBox txtReturnQty = (TextBox)gvr.FindControl("txtReturnQty");

            if (txtReturnQty.Text != "")
            {
                if (lblReceivedQty.Text != "")
                {
                    if (float.Parse(txtReturnQty.Text) > float.Parse(lblReceivedQty.Text))
                    {
                        DisplayMessage("Entered Quantity Is Greater Then Received Quantity");
                        txtReturnQty.Text = "";
                        System.Web.UI.ScriptManager.GetCurrent(this).SetFocus(txtReturnQty);
                    }
                }
            }
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
        strLocationId = Session["LocId"].ToString();
        Session["AccordianId"] = "12";
        Session["HeaderText"] = "Purchase";
        DataTable dtAllPageCode = cmn.GetAllPagePermission(StrUserId.ToString(), "12", "53");
        if (dtAllPageCode.Rows.Count != 0)
        {
            if (dtAllPageCode.Rows[0][0].ToString() == "SuperAdmin")
            {
                btnPReturnSave.Visible = true;

                foreach (GridViewRow Row in GvPReturn.Rows)
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
                        btnPReturnSave.Visible = true;

                    }
                    foreach (GridViewRow Row in GvPReturn.Rows)
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

        DataTable Dt = objDocNo.GetDocumentNumberAll(StrCompId, "12", "53");

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
                DocumentNo += strLocationId;
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
                DocumentNo += "-" + (Convert.ToInt32(objPReturnHeader.GetPRHeaderAll(StrCompId.ToString(),StrBrandId,strLocationId).Rows.Count) + 1).ToString();
            }
            else
            {
                DocumentNo += (Convert.ToInt32(objPReturnHeader.GetPRHeaderAll(StrCompId.ToString(), StrBrandId, strLocationId).Rows.Count) + 1).ToString();

            }
        }
        else
        {
            DocumentNo += (Convert.ToInt32(objPReturnHeader.GetPRHeaderAll(StrCompId.ToString(), StrBrandId, strLocationId).Rows.Count) + 1).ToString();
        }

        return DocumentNo;

    } 
}
