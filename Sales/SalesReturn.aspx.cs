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

public partial class Sales_SalesReturn : System.Web.UI.Page
{
    #region defind Class Object
    Common cmn = new Common();
    Inv_SalesInvoiceHeader objSInvHeader = new Inv_SalesInvoiceHeader();
    Inv_SalesInvoiceDetail objSInvDetail = new Inv_SalesInvoiceDetail();
    Inv_SalesOrderHeader objSOrderHeader = new Inv_SalesOrderHeader();
    Inv_SalesOrderDetail ObjSOrderDetail = new Inv_SalesOrderDetail();
    Inv_SalesInquiryHeader objSInquiryHeader = new Inv_SalesInquiryHeader();
    Set_Payment_Mode_Master objPaymentMode = new Set_Payment_Mode_Master();
    Inv_ProductMaster objProductM = new Inv_ProductMaster();
    Ems_ContactMaster objContact = new Ems_ContactMaster();
    CurrencyMaster objCurrency = new CurrencyMaster();
    Inv_UnitMaster UM = new Inv_UnitMaster();
    EmployeeMaster objEmployee = new EmployeeMaster();
    Set_AddressMaster objAddMaster = new Set_AddressMaster();
    SystemParameter ObjSysParam = new SystemParameter();
    UserMaster ObjUser = new UserMaster();
    Set_DocNumber objDocNo = new Set_DocNumber();

    string StrCompId = string.Empty;
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
        AllPageCode();
        if (!IsPostBack)
        {
            ddlOption.SelectedIndex = 2;
            btnList_Click(null, null);
            FillGridBin();
            FillGrid();
            FillRequestGrid();
        }
    }
    public void AllPageCode()
    {
        Page.Title = ObjSysParam.GetSysTitle();
        StrUserId = Session["UserId"].ToString();
        StrCompId = Session["CompId"].ToString();
        StrBrandId = Session["BrandId"].ToString();
        StrLocationId = Session["LocId"].ToString();
        Session["AccordianId"] = "13";
        Session["HeaderText"] = "Sales";
        DataTable dtAllPageCode = cmn.GetAllPagePermission(StrUserId.ToString(), "13", "67");
        if (dtAllPageCode.Rows.Count != 0)
        {
            if (dtAllPageCode.Rows[0][0].ToString() == "SuperAdmin")
            {
                btnSInvSave.Visible = true;
                foreach (GridViewRow Row in GvSalesReturn.Rows)
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
                        btnSInvSave.Visible = true;
                    }
                    foreach (GridViewRow Row in GvSalesReturn.Rows)
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
        PnlBin.Visible = false;
    }
    protected void btnEdit_Command(object sender, CommandEventArgs e)
    {
        editid.Value = e.CommandArgument.ToString();

        DataTable dtInvEdit = objSInvHeader.GetSInvHeaderAllByTransId(StrCompId, StrBrandId, StrLocationId, editid.Value);

        btnNew.Text = Resources.Attendance.Edit;
        if (dtInvEdit.Rows.Count > 0)
        {

        }
        btnNew_Click(null, null);
        System.Web.UI.ScriptManager.GetCurrent(this).SetFocus(txtSInvNo);
    }
    protected void GvSalesReturn_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        GvSalesReturn.PageIndex = e.NewPageIndex;
        DataTable dt = (DataTable)Session["dtFilter"];
        GvSalesReturn.DataSource = dt;
        GvSalesReturn.DataBind();
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
            GvSalesReturn.DataSource = view.ToTable();
            Session["dtFilter"] = view.ToTable();
            lblTotalRecords.Text = Resources.Attendance.Total_Records + " : " + view.ToTable().Rows.Count + "";
            GvSalesReturn.DataBind();
        }
    }
    protected void GvSalesReturn_Sorting(object sender, GridViewSortEventArgs e)
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
        GvSalesReturn.DataSource = dt;
        GvSalesReturn.DataBind();
    }
    protected void IbtnDelete_Command(object sender, CommandEventArgs e)
    {
        editid.Value = e.CommandArgument.ToString();
        int b = 0;
        b = objSInvHeader.DeleteSInvHeader(StrCompId, StrBrandId, StrLocationId, editid.Value, "false", StrUserId, DateTime.Now.ToString());
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
    protected void btnSInvCancel_Click(object sender, EventArgs e)
    {
        Reset();
        btnList_Click(null, null);
        FillGridBin();
        FillGrid();
    }
    protected void BtnReset_Click(object sender, EventArgs e)
    {
        Reset();
        System.Web.UI.ScriptManager.GetCurrent(this).SetFocus(txtSInvNo);
    }
    protected void btnSInvSave_Click(object sender, EventArgs e)
    {
        //if (txtSInvDate.Text == "")
        //{
        //    DisplayMessage("Enter Sales Invoice Date");
        //    System.Web.UI.ScriptManager.GetCurrent(this).SetFocus(txtSInvDate);
        //    return;
        //}
        //if (txtSInvNo.Text == "")
        //{
        //    DisplayMessage("Enter Sales Invoice No.");
        //    System.Web.UI.ScriptManager.GetCurrent(this).SetFocus(txtSInvNo);
        //    return;
        //}
        //else
        //{
        //    if (editid.Value == "")
        //    {
        //        DataTable dtSQNo = objSInvHeader.GetSInvHeaderAllByInvoiceNo(StrCompId, StrBrandId, StrLocationId, txtSInvNo.Text);
        //        if (dtSQNo.Rows.Count > 0)
        //        {
        //            DisplayMessage("Sales Invoice No. Already Exits");
        //            txtSInvNo.Text = "";
        //            System.Web.UI.ScriptManager.GetCurrent(this).SetFocus(txtSInvNo);
        //            return;
        //        }
        //    }
        //}

        //if (ddlOrderType.SelectedValue != "--Select--")
        //{
        //    if (ddlOrderType.SelectedValue == "D")
        //    {
        //        if (GvProductDetail.Rows.Count > 0)
        //        {

        //        }
        //        else
        //        {
        //            DisplayMessage("You have no Product For Generate Sales Order");
        //            return;
        //        }
        //    }
        //    else if (ddlOrderType.SelectedValue == "Q")
        //    {
        //        if (GvSalesOrderDetail.Rows.Count > 0)
        //        {

        //        }
        //        else
        //        {
        //            DisplayMessage("No Product available For Generate Sales Invoice");
        //            return;
        //        }
        //    }
        //}
        //else
        //{
        //    DisplayMessage("Select Order Type");
        //    System.Web.UI.ScriptManager.GetCurrent(this).SetFocus(ddlOrderType);
        //    return;
        //}

        //string strCustomerId = string.Empty;
        //if (txtCustomer.Text != "")
        //{
        //    if (GetCustomerId() != "")
        //    {

        //    }
        //    else
        //    {
        //        txtCustomer.Text = "";
        //        DisplayMessage("Customer Choose In Suggestion Only");
        //        System.Web.UI.ScriptManager.GetCurrent(this).SetFocus(txtCustomer);
        //        return;
        //    }
        //}
        //else
        //{
        //    DisplayMessage("Enter Customer Name");
        //    System.Web.UI.ScriptManager.GetCurrent(this).SetFocus(txtCustomer);
        //    return;
        //}
        //if (ddlPaymentMode.Text == "--Select--")
        //{
        //    DisplayMessage("Select PaymentMode");
        //    ddlPaymentMode.Focus();
        //    return;
        //}
        //if (ddlCurrency.Text == "--Select--")
        //{
        //    DisplayMessage("Select Currency");
        //    ddlCurrency.Focus();
        //    return;
        //}

        //if (txtAmount.Text == "")
        //{
        //    txtAmount.Text = "0";
        //}
        //if (txtTaxP.Text == "")
        //{
        //    txtTaxP.Text = "0";
        //}
        //if (txtTaxV.Text == "")
        //{
        //    txtTaxV.Text = "0";
        //}
        //if (txtDiscountP.Text == "")
        //{
        //    txtDiscountP.Text = "0";
        //}
        //if (txtDiscountV.Text == "")
        //{
        //    txtDiscountV.Text = "0";
        //}
        //if (txtGrandTotal.Text == "")
        //{
        //    txtGrandTotal.Text = "0";
        //}
        //if (txtInvoiceCosting.Text == "")
        //{
        //    txtInvoiceCosting.Text = "0";
        //}
        //if (txtNetAmount.Text == "")
        //{
        //    txtNetAmount.Text = "0";
        //}
        //if (txtTender.Text == "")
        //{
        //    txtTender.Text = "0";
        //}

        //int b = 0;
        //if (editid.Value != "")
        //{
        //    b = objSInvHeader.UpdateSInvHeader(StrCompId, StrBrandId, StrLocationId, editid.Value, txtSInvNo.Text, txtSInvDate.Text, ddlPaymentMode.SelectedValue, ddlCurrency.SelectedValue, txtTransFrom.Text, hdnSalesOrderId.Value, GetCustomerId(), txtPOSNo.Text, txtRemark.Text, txtAccountNo.Text, txtInvoiceCosting.Text, txtShift.Text, "False", txtTender.Text, txtAmount.Text, txtTotalQuantity.Text, txtAmount.Text, txtTaxP.Text, txtTaxV.Text, txtNetAmount.Text, txtDiscountP.Text, txtDiscountV.Text, txtGrandTotal.Text, GetCustomerId(), txtCondition1.Text, txtCondition2.Text, txtCondition3.Text, txtCondition4.Text, txtCondition5.Text, "", "", "", "", "", "True", DateTime.Now.ToString(), "True", Session["UserId"].ToString(), DateTime.Now.ToString());

        //    if (b != 0)
        //    {
        //        objSInvDetail.DeleteSInvDetail(StrCompId, StrBrandId, StrLocationId, editid.Value);
        //        if (ddlOrderType.SelectedValue == "D")
        //        {
        //            foreach (GridViewRow gvr in GvProductDetail.Rows)
        //            {
        //                Label lblgvSerialNo = (Label)gvr.FindControl("lblgvSerialNo");
        //                HiddenField hdngvProductId = (HiddenField)gvr.FindControl("hdngvProductId");
        //                Label lblgvProductDescription = (Label)gvr.FindControl("lblgvProductDescription");
        //                HiddenField hdngvUnitId = (HiddenField)gvr.FindControl("hdngvUnitId");
        //                Label lblgvUnitPrice = (Label)gvr.FindControl("lblgvUnitPrice");
        //                Label lblgvUnitCost = (Label)gvr.FindControl("lblgvUnitCost");
        //                Label lblgvQuantity = (Label)gvr.FindControl("lblgvQuantity");
        //                Label lblgvTaxP = (Label)gvr.FindControl("lblgvTaxP");
        //                Label lblgvTaxV = (Label)gvr.FindControl("lblgvTaxV");
        //                Label lblgvDiscountP = (Label)gvr.FindControl("lblgvDiscountP");
        //                Label lblgvDiscountV = (Label)gvr.FindControl("lblgvDiscountV");

        //                if (lblgvUnitPrice.Text == "")
        //                {
        //                    lblgvUnitPrice.Text = "0";
        //                }
        //                if (lblgvUnitCost.Text == "")
        //                {
        //                    lblgvUnitCost.Text = "0";
        //                }
        //                if (lblgvQuantity.Text == "")
        //                {
        //                    lblgvQuantity.Text = "0";
        //                }
        //                if (lblgvTaxP.Text == "")
        //                {
        //                    lblgvTaxP.Text = "0";
        //                }
        //                if (lblgvTaxV.Text == "")
        //                {
        //                    lblgvTaxV.Text = "0";
        //                }
        //                if (lblgvDiscountP.Text == "")
        //                {
        //                    lblgvDiscountP.Text = "0";
        //                }
        //                if (lblgvDiscountV.Text == "")
        //                {
        //                    lblgvDiscountV.Text = "0";
        //                }

        //                objSInvDetail.InsertSInvDetail(StrCompId, StrBrandId, StrLocationId, editid.Value, lblgvSerialNo.Text, ddlPaymentMode.SelectedValue, txtPOSNo.Text, txtTransFrom.Text, hdnSalesOrderId.Value, hdngvProductId.Value, lblgvProductDescription.Text, hdngvUnitId.Value, lblgvUnitPrice.Text, lblgvUnitCost.Text, lblgvQuantity.Text, "0", lblgvTaxP.Text, lblgvTaxV.Text, lblgvDiscountP.Text, lblgvDiscountV.Text, "False", "", "", "", "", "", "True", DateTime.Now.ToString(), "True", Session["UserId"].ToString(), DateTime.Now.ToString(), Session["UserId"].ToString(), DateTime.Now.ToString());
        //            }
        //        }
        //        else if (ddlOrderType.SelectedValue == "Q")
        //        {
        //            foreach (GridViewRow gvrQ in GvSalesOrderDetail.Rows)
        //            {
        //                Label lblgvSerialNo = (Label)gvrQ.FindControl("lblgvSerialNo");
        //                HiddenField hdngvProductId = (HiddenField)gvrQ.FindControl("hdngvProductId");
        //                Label lblgvProductDescription = (Label)gvrQ.FindControl("lblgvProductDescription");
        //                HiddenField hdngvUnitId = (HiddenField)gvrQ.FindControl("hdngvUnitId");
        //                Label lblgvUnitPrice = (Label)gvrQ.FindControl("lblgvUnitPrice");
        //                Label lblgvUnitCost = (Label)gvrQ.FindControl("lblgvUnitCost");
        //                Label lblgvQuantity = (Label)gvrQ.FindControl("lblgvQuantity");
        //                TextBox txtgvTaxP = (TextBox)gvrQ.FindControl("txtgvTaxP");
        //                TextBox txtgvTaxV = (TextBox)gvrQ.FindControl("txtgvTaxV");
        //                TextBox txtgvDiscountP = (TextBox)gvrQ.FindControl("txtgvDiscountP");
        //                TextBox txtgvDiscountV = (TextBox)gvrQ.FindControl("txtgvDiscountV");

        //                if (lblgvUnitPrice.Text == "")
        //                {
        //                    lblgvUnitPrice.Text = "0";
        //                }
        //                if (lblgvUnitCost.Text == "")
        //                {
        //                    lblgvUnitCost.Text = "0";
        //                }
        //                if (lblgvQuantity.Text == "")
        //                {
        //                    lblgvQuantity.Text = "0";
        //                }
        //                if (txtgvTaxP.Text == "")
        //                {
        //                    txtgvTaxP.Text = "0";
        //                }
        //                if (txtgvTaxV.Text == "")
        //                {
        //                    txtgvTaxV.Text = "0";
        //                }
        //                if (txtgvDiscountP.Text == "")
        //                {
        //                    txtgvDiscountP.Text = "0";
        //                }
        //                if (txtgvDiscountV.Text == "")
        //                {
        //                    txtgvDiscountV.Text = "0";
        //                }

        //                objSInvDetail.InsertSInvDetail(StrCompId, StrBrandId, StrLocationId, editid.Value, lblgvSerialNo.Text, ddlPaymentMode.SelectedValue, txtPOSNo.Text, txtTransFrom.Text, hdnSalesOrderId.Value, hdngvProductId.Value, lblgvProductDescription.Text, hdngvUnitId.Value, lblgvUnitPrice.Text, lblgvUnitCost.Text, lblgvQuantity.Text, "0", txtgvTaxP.Text, txtgvTaxV.Text, txtgvDiscountP.Text, txtgvDiscountV.Text, "False", "", "", "", "", "", "True", DateTime.Now.ToString(), "True", Session["UserId"].ToString(), DateTime.Now.ToString(), Session["UserId"].ToString(), DateTime.Now.ToString());
        //            }
        //        }

        //        DisplayMessage("Record Updated");

        //        editid.Value = "";
        //        FillGrid();
        //        btnList_Click(null, null);
        //        Reset();
        //    }
        //    else
        //    {
        //        DisplayMessage("Record  Not Updated");
        //    }
        //}
        //else
        //{
        //    b = objSInvHeader.InsertSInvHeader(StrCompId, StrBrandId, StrLocationId, txtSInvNo.Text, txtSInvDate.Text, ddlPaymentMode.SelectedValue, ddlCurrency.SelectedValue, txtTransFrom.Text, hdnSalesOrderId.Value, GetCustomerId(), txtPOSNo.Text, txtRemark.Text, txtAccountNo.Text, txtInvoiceCosting.Text, txtShift.Text, "False", txtTender.Text, txtAmount.Text, txtTotalQuantity.Text, txtAmount.Text, txtTaxP.Text, txtTaxV.Text, txtNetAmount.Text, txtDiscountP.Text, txtDiscountV.Text, txtGrandTotal.Text, GetCustomerId(), txtCondition1.Text, txtCondition2.Text, txtCondition3.Text, txtCondition4.Text, txtCondition5.Text, "", "", "", "", "", "True", DateTime.Now.ToString(), "True", Session["UserId"].ToString(), DateTime.Now.ToString(), Session["UserId"].ToString(), DateTime.Now.ToString());
        //    if (b != 0)
        //    {
        //        string strMaxId = string.Empty;
        //        DataTable dtMaxId = objSInvHeader.GetMaxSalesInvoiceId(StrCompId, StrBrandId, StrLocationId);
        //        if (dtMaxId.Rows.Count > 0)
        //        {
        //            strMaxId = dtMaxId.Rows[0][0].ToString();
        //        }

        //        if (strMaxId != "" && strMaxId != "0")
        //        {
        //            if (ddlOrderType.SelectedValue == "D")
        //            {
        //                foreach (GridViewRow gvr in GvProductDetail.Rows)
        //                {
        //                    Label lblgvSerialNo = (Label)gvr.FindControl("lblgvSerialNo");
        //                    HiddenField hdngvProductId = (HiddenField)gvr.FindControl("hdngvProductId");
        //                    Label lblgvProductDescription = (Label)gvr.FindControl("lblgvProductDescription");
        //                    HiddenField hdngvUnitId = (HiddenField)gvr.FindControl("hdngvUnitId");
        //                    Label lblgvUnitPrice = (Label)gvr.FindControl("lblgvUnitPrice");
        //                    Label lblgvUnitCost = (Label)gvr.FindControl("lblgvUnitCost");
        //                    Label lblgvQuantity = (Label)gvr.FindControl("lblgvQuantity");
        //                    Label lblgvTaxP = (Label)gvr.FindControl("lblgvTaxP");
        //                    Label lblgvTaxV = (Label)gvr.FindControl("lblgvTaxV");
        //                    Label lblgvDiscountP = (Label)gvr.FindControl("lblgvDiscountP");
        //                    Label lblgvDiscountV = (Label)gvr.FindControl("lblgvDiscountV");

        //                    if (lblgvUnitPrice.Text == "")
        //                    {
        //                        lblgvUnitPrice.Text = "0";
        //                    }
        //                    if (lblgvUnitCost.Text == "")
        //                    {
        //                        lblgvUnitCost.Text = "0";
        //                    }
        //                    if (lblgvQuantity.Text == "")
        //                    {
        //                        lblgvQuantity.Text = "0";
        //                    }
        //                    if (lblgvTaxP.Text == "")
        //                    {
        //                        lblgvTaxP.Text = "0";
        //                    }
        //                    if (lblgvTaxV.Text == "")
        //                    {
        //                        lblgvTaxV.Text = "0";
        //                    }
        //                    if (lblgvDiscountP.Text == "")
        //                    {
        //                        lblgvDiscountP.Text = "0";
        //                    }
        //                    if (lblgvDiscountV.Text == "")
        //                    {
        //                        lblgvDiscountV.Text = "0";
        //                    }

        //                    objSInvDetail.InsertSInvDetail(StrCompId, StrBrandId, StrLocationId, strMaxId, lblgvSerialNo.Text, ddlPaymentMode.SelectedValue, txtPOSNo.Text, txtTransFrom.Text, hdnSalesOrderId.Value, hdngvProductId.Value, lblgvProductDescription.Text, hdngvUnitId.Value, lblgvUnitPrice.Text, lblgvUnitCost.Text, lblgvQuantity.Text, "0", lblgvTaxP.Text, lblgvTaxV.Text, lblgvDiscountP.Text, lblgvDiscountV.Text, "False", "", "", "", "", "", "True", DateTime.Now.ToString(), "True", Session["UserId"].ToString(), DateTime.Now.ToString(), Session["UserId"].ToString(), DateTime.Now.ToString());
        //                }
        //            }
        //            else if (ddlOrderType.SelectedValue == "Q")
        //            {
        //                foreach (GridViewRow gvrQ in GvSalesOrderDetail.Rows)
        //                {
        //                    Label lblgvSerialNo = (Label)gvrQ.FindControl("lblgvSerialNo");
        //                    HiddenField hdngvProductId = (HiddenField)gvrQ.FindControl("hdngvProductId");
        //                    Label lblgvProductDescription = (Label)gvrQ.FindControl("lblgvProductDescription");
        //                    HiddenField hdngvUnitId = (HiddenField)gvrQ.FindControl("hdngvUnitId");
        //                    Label lblgvUnitPrice = (Label)gvrQ.FindControl("lblgvUnitPrice");
        //                    TextBox txtgvUnitCost = (TextBox)gvrQ.FindControl("txtgvUnitCost");
        //                    Label lblgvQuantity = (Label)gvrQ.FindControl("lblgvQuantity");
        //                    TextBox txtgvTaxP = (TextBox)gvrQ.FindControl("txtgvTaxP");
        //                    TextBox txtgvTaxV = (TextBox)gvrQ.FindControl("txtgvTaxV");
        //                    TextBox txtgvDiscountP = (TextBox)gvrQ.FindControl("txtgvDiscountP");
        //                    TextBox txtgvDiscountV = (TextBox)gvrQ.FindControl("txtgvDiscountV");

        //                    if (lblgvUnitPrice.Text == "")
        //                    {
        //                        lblgvUnitPrice.Text = "0";
        //                    }
        //                    if (txtgvUnitCost.Text == "")
        //                    {
        //                        txtgvUnitCost.Text = "0";
        //                    }
        //                    if (txtgvTaxP.Text == "")
        //                    {
        //                        txtgvTaxP.Text = "0";
        //                    }
        //                    if (txtgvTaxV.Text == "")
        //                    {
        //                        txtgvTaxV.Text = "0";
        //                    }
        //                    if (txtgvDiscountP.Text == "")
        //                    {
        //                        txtgvDiscountP.Text = "0";
        //                    }
        //                    if (txtgvDiscountV.Text == "")
        //                    {
        //                        txtgvDiscountV.Text = "0";
        //                    }

        //                    objSInvDetail.InsertSInvDetail(StrCompId, StrBrandId, StrLocationId, strMaxId, lblgvSerialNo.Text, ddlPaymentMode.SelectedValue, txtPOSNo.Text, txtTransFrom.Text, hdnSalesOrderId.Value, hdngvProductId.Value, lblgvProductDescription.Text, hdngvUnitId.Value, lblgvUnitPrice.Text, txtgvUnitCost.Text, lblgvQuantity.Text, "0", txtgvTaxP.Text, txtgvTaxV.Text, txtgvDiscountP.Text, txtgvDiscountV.Text, "False", "", "", "", "", "", "True", DateTime.Now.ToString(), "True", Session["UserId"].ToString(), DateTime.Now.ToString(), Session["UserId"].ToString(), DateTime.Now.ToString());
        //                }
        //            }
        //        }
        //        DisplayMessage("Record Saved");

        //        FillGrid();
        //        Reset();
        //    }
        //    else
        //    {
        //        DisplayMessage("Record  Not Saved");
        //    }
        //}
    }
    protected void IbtnRestore_Command(object sender, CommandEventArgs e)
    {
        editid.Value = e.CommandArgument.ToString();
        int b = 0;
        b = objSInvHeader.DeleteSInvHeader(StrCompId, StrBrandId, StrLocationId, editid.Value, true.ToString(), StrUserId, DateTime.Now.ToString());
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
        PnlList.Visible = false;
        FillRequestGrid();
    }
    protected void GvSalesReturnBin_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        GvSalesReturnBin.PageIndex = e.NewPageIndex;

        DataTable dt = (DataTable)Session["dtInactive"];
        GvSalesReturnBin.DataSource = dt;
        GvSalesReturnBin.DataBind();

        string temp = string.Empty;

        for (int i = 0; i < GvSalesReturnBin.Rows.Count; i++)
        {
            HiddenField lblconid = (HiddenField)GvSalesReturnBin.Rows[i].FindControl("hdnTransId");
            string[] split = lblSelectedRecord.Text.Split(',');

            for (int j = 0; j < lblSelectedRecord.Text.Split(',').Length; j++)
            {
                if (lblSelectedRecord.Text.Split(',')[j] != "")
                {
                    if (lblconid.Value.Trim().ToString() == lblSelectedRecord.Text.Split(',')[j].Trim().ToString())
                    {
                        ((CheckBox)GvSalesReturnBin.Rows[i].FindControl("chkSelect")).Checked = true;
                    }
                }
            }
        }
    }
    protected void GvSalesReturnBin_OnSorting(object sender, GridViewSortEventArgs e)
    {
        HDFSortbin.Value = HDFSortbin.Value == "ASC" ? "DESC" : "ASC";
        DataTable dt = new DataTable();
        dt = objSOrderHeader.GetSOHeaderAllFalse(StrCompId, StrBrandId, StrLocationId);
        DataView dv = new DataView(dt);
        string Query = "" + e.SortExpression + " " + HDFSortbin.Value + "";
        dv.Sort = Query;
        dt = dv.ToTable();
        Session["dtInactive"] = dt;
        GvSalesReturnBin.DataSource = dt;
        GvSalesReturnBin.DataBind();
        lblSelectedRecord.Text = "";
    }
    public void FillGridBin()
    {
        DataTable dt = new DataTable();
        dt = objSInvHeader.GetSInvHeaderAllFalse(StrCompId, StrBrandId, StrLocationId);
        GvSalesReturnBin.DataSource = dt;
        GvSalesReturnBin.DataBind();
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
            GvSalesReturnBin.DataSource = view.ToTable();
            GvSalesReturnBin.DataBind();
            lblSelectedRecord.Text = "";
            if (view.ToTable().Rows.Count == 0)
            {
                FillGridBin();
            }
            System.Web.UI.ScriptManager.GetCurrent(this).SetFocus(txtValueBin);
        }
    }
    protected void btnRestoreSelected_Click(object sender, CommandEventArgs e)
    {
        int b = 0;
        DataTable dt = objSOrderHeader.GetSOHeaderAllFalse(StrCompId, StrBrandId, StrLocationId);

        if (GvSalesReturnBin.Rows.Count != 0)
        {
            if (lblSelectedRecord.Text != "")
            {
                for (int j = 0; j < lblSelectedRecord.Text.Split(',').Length; j++)
                {
                    if (lblSelectedRecord.Text.Split(',')[j] != "")
                    {
                        //Msg = objTax.DeleteTaxMaster(lblSelectedRecord.Text.Split(',')[j].Trim().ToString(), true.ToString(), Session["UserId"].ToString(), DateTime.Now.ToString());
                        b = objSOrderHeader.DeleteSOHeader(StrCompId, StrBrandId, StrLocationId, lblSelectedRecord.Text.Split(',')[j].Trim().ToString(), true.ToString(), Session["UserId"].ToString(), DateTime.Now.ToString());
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
                foreach (GridViewRow Gvr in GvSalesReturnBin.Rows)
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
        CheckBox chkSelAll = ((CheckBox)GvSalesReturnBin.HeaderRow.FindControl("chkCurrent"));
        for (int i = 0; i < GvSalesReturnBin.Rows.Count; i++)
        {
            ((CheckBox)GvSalesReturnBin.Rows[i].FindControl("chkSelect")).Checked = chkSelAll.Checked;
            if (chkSelAll.Checked)
            {
                if (!lblSelectedRecord.Text.Split(',').Contains(((HiddenField)(GvSalesReturnBin.Rows[i].FindControl("hdnTransId"))).Value.Trim().ToString()))
                {
                    lblSelectedRecord.Text += ((HiddenField)(GvSalesReturnBin.Rows[i].FindControl("hdnTransId"))).Value.Trim().ToString() + ",";
                }
            }
            else
            {
                string temp = string.Empty;
                string[] split = lblSelectedRecord.Text.Split(',');
                foreach (string item in split)
                {
                    if (item != ((HiddenField)(GvSalesReturnBin.Rows[i].FindControl("hdnTransId"))).Value.Trim().ToString())
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
        HiddenField lb = (HiddenField)GvSalesReturnBin.Rows[index].FindControl("hdnTransId");
        if (((CheckBox)GvSalesReturnBin.Rows[index].FindControl("chkSelect")).Checked)
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
                if (!lblSelectedRecord.Text.Split(',').Contains(dr["SQuotation_Id"]))
                {
                    lblSelectedRecord.Text += dr["SQuotation_Id"] + ",";
                }
            }
            for (int i = 0; i < GvSalesReturnBin.Rows.Count; i++)
            {
                string[] split = lblSelectedRecord.Text.Split(',');
                HiddenField lblconid = (HiddenField)GvSalesReturnBin.Rows[i].FindControl("hdnTransId");
                for (int j = 0; j < lblSelectedRecord.Text.Split(',').Length; j++)
                {
                    if (lblSelectedRecord.Text.Split(',')[j] != "")
                    {
                        if (lblconid.Value.Trim().ToString() == lblSelectedRecord.Text.Split(',')[j].Trim().ToString())
                        {
                            ((CheckBox)GvSalesReturnBin.Rows[i].FindControl("chkSelect")).Checked = true;
                        }
                    }
                }
            }
        }
        else
        {
            lblSelectedRecord.Text = "";
            DataTable dtUnit1 = (DataTable)Session["dtInactive"];
            GvSalesReturnBin.DataSource = dtUnit1;
            GvSalesReturnBin.DataBind();
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
                    b = objSInvHeader.DeleteSInvHeader(StrCompId, StrBrandId, StrLocationId, lblSelectedRecord.Text.Split(',')[j].Trim(), true.ToString(), Session["UserId"].ToString(), DateTime.Now.ToString());
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
            foreach (GridViewRow Gvr in GvSalesReturnBin.Rows)
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
    [System.Web.Services.WebMethodAttribute(), System.Web.Script.Services.ScriptMethodAttribute()]
    public static string[] GetCompletionListCustomer(string prefixText, int count, string contextKey)
    {
        Ems_ContactMaster objCustomer = new Ems_ContactMaster();
        DataTable dtCustomer = objCustomer.GetDistinctCustomerName("1", prefixText);
        string[] txt = new string[dtCustomer.Rows.Count];

        if (dtCustomer.Rows.Count > 0)
        {
            for (int i = 0; i < dtCustomer.Rows.Count; i++)
            {
                txt[i] = dtCustomer.Rows[i]["Contact_Name"].ToString() + "/" + dtCustomer.Rows[i]["Contact_Id"].ToString() + "";
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
            dtCustomer = objCustomer.GetAllCustomerName("1");
            if (dtCustomer.Rows.Count > 0)
            {
                txt = new string[dtCustomer.Rows.Count];
                for (int i = 0; i < dtCustomer.Rows.Count; i++)
                {
                    txt[i] = dtCustomer.Rows[i]["Contact_Name"].ToString() + "/" + dtCustomer.Rows[i]["Contact_Id"].ToString() + "";
                }
            }
            //}
        }
        return txt;
    }

    [System.Web.Services.WebMethodAttribute(), System.Web.Script.Services.ScriptMethodAttribute()]
    public static string[] GetCompletionListEmployeeName(string prefixText, int count, string contextKey)
    {
        EmployeeMaster ObjEmployeeMaster = new EmployeeMaster();
        DataTable dt1 = ObjEmployeeMaster.GetEmployeeMaster(HttpContext.Current.Session["CompId"].ToString());
        dt1 = new DataView(dt1, "Brand_Id='1' and Location_Id='1'", "", DataViewRowState.CurrentRows).ToTable();

        DataTable dt = new DataView(dt1, "Emp_Name like '" + prefixText.ToString() + "%'", "", DataViewRowState.CurrentRows).ToTable();
        string[] txt = new string[dt.Rows.Count];
        if (dt.Rows.Count > 0)
        {
            for (int i = 0; i < dt.Rows.Count; i++)
            {
                //txt[i] = dt.Rows[i]["Emp_Name"].ToString();
                txt[i] = dt.Rows[i]["Emp_Name"].ToString() + "/" + dt.Rows[i]["Emp_Id"].ToString() + "";
            }
        }
        else
        {
            if (dt1.Rows.Count > 0)
            {
                txt = new string[dt1.Rows.Count];
                for (int i = 0; i < dt1.Rows.Count; i++)
                {
                    //txt[i] = dt1.Rows[i]["Emp_Name"].ToString();
                    txt[i] = dt1.Rows[i]["Emp_Name"].ToString() + "/" + dt1.Rows[i]["Emp_Id"].ToString() + "";
                }
            }
        }
        return txt;
    }

    #region User defind Funcation
    private void FillGrid()
    {
        DataTable dtBrand = objSInvHeader.GetSInvHeaderAllTrue(StrCompId, StrBrandId, StrLocationId);
        lblTotalRecords.Text = Resources.Attendance.Total_Records + " : " + dtBrand.Rows.Count + "";
        Session["dtCurrency"] = dtBrand;
        Session["dtFilter"] = dtBrand;
        if (dtBrand != null && dtBrand.Rows.Count > 0)
        {
            GvSalesReturn.DataSource = dtBrand;
            GvSalesReturn.DataBind();
        }
        else
        {
            GvSalesReturn.DataSource = null;
            GvSalesReturn.DataBind();
        }
        lblTotalRecords.Text = Resources.Attendance.Total_Records + " : " + dtBrand.Rows.Count.ToString() + "";
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
        txtSInvNo.Text = "";
        txtSInvDate.Text = "";
        txtSInvNo.ReadOnly = false;
        txtCustomerName.Text = "";
        txtSalesOrderNo.Text = "";
        txtSalesPerson.Text = "";
        txtPOSNo.Text = "";
        txtAccountNo.Text = "";
        txtInvoiceCosting.Text = "";
        txtShift.Text = "";
        txtTender.Text = "";

        txtRemark.Text = "";

        GvSalesOrderDetail.DataSource = null;
        GvSalesOrderDetail.DataBind();

        FillRequestGrid();
        FillGrid();

        hdnSalesOrderId.Value = "0";

        editid.Value = "";
        btnNew.Text = Resources.Attendance.New;
        ddlFieldName.SelectedIndex = 1;
        ddlOption.SelectedIndex = 2;
        txtValue.Text = "";
        txtValueBin.Text = "";
        lblSelectedRecord.Text = "";
        System.Web.UI.ScriptManager.GetCurrent(this).SetFocus(txtValue);
    }
    #endregion

    #region Add Request Section
    //private void FillUnit()
    //{
    //    foreach (GridViewRow gvr in GvSalesOrderDetail.Rows)
    //    {
    //        DropDownList ddlUnit = (DropDownList)gvr.FindControl("ddlgvUnit");

    //        DataTable dsUnit = null;
    //        dsUnit = UM.GetUnitMaster(StrCompId);
    //        if (dsUnit.Rows.Count > 0)
    //        {
    //            ddlUnit.DataSource = dsUnit;
    //            ddlUnit.DataTextField = "Unit_Code";
    //            ddlUnit.DataValueField = "Unit_Id";
    //            ddlUnit.DataBind();

    //            ddlUnit.Items.Add("--Select--");
    //            ddlUnit.SelectedValue = "--Select--";
    //        }
    //        else
    //        {
    //            ddlUnit.Items.Add("--Select--");
    //            ddlUnit.SelectedValue = "--Select--";
    //        }
    //    }
    //}
    protected void GvSalesOrder_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        GvSalesOrder.PageIndex = e.NewPageIndex;
        DataTable dt = (DataTable)Session["dtPRequest"];
        GvSalesOrder.DataSource = dt;
        GvSalesOrder.DataBind();
    }
    protected void GvSalesOrder_Sorting(object sender, GridViewSortEventArgs e)
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
        GvSalesOrder.DataSource = dt;
        GvSalesOrder.DataBind();
    }
    private void FillRequestGrid()
    {
        //Check Query According To Quotation Page For Order Record.
        DataTable dtPRequest = objSOrderHeader.GetDataForSalesInvoice(StrCompId, StrBrandId, StrLocationId);
        lblTotalRecords.Text = Resources.Attendance.Total_Records + " : " + dtPRequest.Rows.Count + "";
        Session["dtPRequest"] = dtPRequest;
        if (dtPRequest != null && dtPRequest.Rows.Count > 0)
        {
            GvSalesOrder.DataSource = dtPRequest;
            GvSalesOrder.DataBind();
        }
        else
        {
            GvSalesOrder.DataSource = null;
            GvSalesOrder.DataBind();
        }
        lblTotalRecords.Text = Resources.Attendance.Total_Records + " : " + dtPRequest.Rows.Count.ToString() + "";
    }
    protected string GetEmployeeName(string strEmployeeId)
    {
        string strEmployeeName = string.Empty;
        if (strEmployeeId != "0" && strEmployeeId != "")
        {
            DataTable dtEName = objEmployee.GetEmployeeMasterById(StrCompId, strEmployeeId);
            if (dtEName.Rows.Count > 0)
            {
                strEmployeeName = dtEName.Rows[0]["Emp_Name"].ToString();
            }
        }
        else
        {
            strEmployeeName = "";
        }
        return strEmployeeName;
    }
    protected string GetCustomerName(string strCustomerId)
    {
        string strCustomerName = string.Empty;
        if (strCustomerId != "0" && strCustomerId != "")
        {
            DataTable dtCName = objContact.GetContactByContactId(StrCompId, strCustomerId);
            if (dtCName.Rows.Count > 0)
            {
                strCustomerName = dtCName.Rows[0]["Contact_Name"].ToString();
            }
        }
        else
        {
            strCustomerName = "";
        }
        return strCustomerName;
    }
    protected string GetUnitCode(string strUnitId)
    {
        string strUnitCode = string.Empty;
        if (strUnitId != "0" && strUnitId != "")
        {
            DataTable dtUCode = UM.GetUnitMasterById(StrCompId, strUnitId);
            if (dtUCode.Rows.Count > 0)
            {
                strUnitCode = dtUCode.Rows[0]["Unit_Code"].ToString();
            }
        }
        else
        {
            strUnitCode = "";
        }
        return strUnitCode;
    }
    private string GetEmployeeId(string strEmployeeName)
    {
        string retval = string.Empty;
        if (strEmployeeName != "")
        {
            DataTable dtEmployee = objEmployee.GetEmployeeMasterByEmpName(StrCompId, strEmployeeName.Split('/')[0].ToString());
            if (dtEmployee.Rows.Count > 0)
            {
                retval = (strEmployeeName.Split('/'))[strEmployeeName.Split('/').Length - 1];

                DataTable dtEmp = objEmployee.GetEmployeeMasterById(StrCompId, retval);
                if (dtEmp.Rows.Count > 0)
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
    protected void btnSIEdit_Command(object sender, CommandEventArgs e)
    {
        //Check Data With Sales Order Like Sales Quotation Page.
        string strRequestId = e.CommandArgument.ToString();

        DataTable dtPRequest = objSOrderHeader.GetSOHeaderAllByTransId(StrCompId, StrBrandId, StrLocationId, strRequestId);

        if (dtPRequest.Rows.Count > 0)
        {
            trTransfer.Visible = true;
            txtSalesOrderNo.Visible = true;
            txtSalesOrderNo.Text = dtPRequest.Rows[0]["SalesOrderNo"].ToString();
            hdnSalesOrderId.Value = dtPRequest.Rows[0]["Trans_Id"].ToString();

            //string strInquiryNo = dtPRequest.Rows[0]["SInquiry_No"].ToString();
            //if (strInquiryNo != "0" && strInquiryNo != "")
            //{
            //    DataTable dtSInquiryData = objSInquiryHeader.GetSIHeaderAllBySInquiryId(StrCompId, StrBrandId, StrLocationId, strInquiryNo);
            //    if (dtSInquiryData.Rows.Count > 0)
            //    {
            //        string strCustomerId = dtSInquiryData.Rows[0]["Customer_Id"].ToString();
            //        if (strCustomerId != "0" && strCustomerId != "")
            //        {
            //            txtCustomer.Text = GetCustomerName(dtSInquiryData.Rows[0]["Customer_Id"].ToString()) + "/" + strCustomerId;
            //        }
            //    }
            //}

            //Add Detail Grid
            DataTable dtDetail = ObjSOrderDetail.GetSODetailBySOrderNo(StrCompId, StrBrandId, StrLocationId, strRequestId);
            if (dtDetail.Rows.Count > 0)
            {
                GvSalesOrderDetail.DataSource = dtDetail;
                GvSalesOrderDetail.DataBind();
                //FillUnit();


                float fGrossTotal = 0.00f;
                foreach (GridViewRow gvr in GvSalesOrderDetail.Rows)
                {
                    HiddenField hdngvProductId = (HiddenField)gvr.FindControl("hdngvProductId");
                    Label lblgvUnitPrice = (Label)gvr.FindControl("lblgvUnitPrice");
                    Label lblgvQuantity = (Label)gvr.FindControl("lblgvQuantity");
                    TextBox txtgvTaxP = (TextBox)gvr.FindControl("txtgvTaxP");
                    TextBox txtgvTaxV = (TextBox)gvr.FindControl("txtgvTaxV");
                    TextBox txtgvPriceAfterTax = (TextBox)gvr.FindControl("txtgvPriceAfterTax");
                    TextBox txtgvDiscountP = (TextBox)gvr.FindControl("txtgvDiscountP");
                    TextBox txtgvDiscountV = (TextBox)gvr.FindControl("txtgvDiscountV");
                    TextBox txtgvPriceAfterDiscount = (TextBox)gvr.FindControl("txtgvPriceAfterDiscount");

                    if (hdngvProductId.Value != "0" && hdngvProductId.Value != "")
                    {
                        for (int i = 0; i < dtDetail.Rows.Count; i++)
                        {
                            string strProductId = dtDetail.Rows[i]["Product_Id"].ToString();
                            if (strProductId == hdngvProductId.Value)
                            {
                                lblgvUnitPrice.Text = dtDetail.Rows[i]["UnitPrice"].ToString();
                                if (lblgvUnitPrice.Text != "0.000" && lblgvUnitPrice.Text != "")
                                {
                                    //txtgvUnitPrice_TextChanged(sender, e);
                                }
                                txtgvTaxP.Text = dtDetail.Rows[i]["TaxP"].ToString();
                                if (txtgvTaxP.Text != "0.000" && txtgvTaxP.Text != "")
                                {

                                }
                                txtgvTaxV.Text = dtDetail.Rows[i]["TaxV"].ToString();
                                if (txtgvTaxV.Text != "0.000" && txtgvTaxV.Text != "")
                                {

                                }

                                //txtgvPriceAfterTax.Text = dtDetail.Rows[i]["PriceAfterTax"].ToString();
                                txtgvDiscountP.Text = dtDetail.Rows[i]["DiscountP"].ToString();
                                if (txtgvDiscountP.Text != "0.000" && txtgvDiscountP.Text != "")
                                {

                                }

                                txtgvDiscountV.Text = dtDetail.Rows[i]["DiscountV"].ToString();
                                if (txtgvDiscountV.Text != "0.000" && txtgvDiscountV.Text != "")
                                {

                                }

                                if (lblgvQuantity.Text != "")
                                {
                                    fGrossTotal = fGrossTotal + float.Parse(lblgvQuantity.Text);
                                }
                                //txtgvPriceAfterDiscount.Text = dtDetail.Rows[i]["PriceAfterDiscount"].ToString();
                            }
                        }
                    }
                }
            }
        }
        btnNew_Click(null, null);
        System.Web.UI.ScriptManager.GetCurrent(this).SetFocus(txtSInvNo);
    }
    protected void GvSalesOrderDetail_RowCreated(object sender, GridViewRowEventArgs e)
    {
        GridView gvProduct = (GridView)sender;
        if (e.Row.RowType == DataControlRowType.Header)
        {
            GridView HeaderGrid = (GridView)sender;
            GridViewRow row = new GridViewRow(0, 0, DataControlRowType.Header, DataControlRowState.Insert);

            TableHeaderCell cell = new TableHeaderCell();
            cell.Text = Resources.Attendance.S_No_;
            cell.ColumnSpan = 1;
            row.Controls.Add(cell);

            cell = new TableHeaderCell();
            cell.ColumnSpan = 2;
            cell.Text = Resources.Attendance.Product_Detail;
            row.Controls.Add(cell);


            cell = new TableHeaderCell();
            cell.ColumnSpan = 3;
            cell.Text = Resources.Attendance.Unit;
            row.Controls.Add(cell);

            cell = new TableHeaderCell();
            cell.ColumnSpan = 2;
            cell.Text = Resources.Attendance.Quantity;
            row.Controls.Add(cell);
            cell = new TableHeaderCell();

            cell = new TableHeaderCell();
            cell.ColumnSpan = 3;
            cell.Text = Resources.Attendance.Tax;
            row.Controls.Add(cell);
            cell = new TableHeaderCell();

            cell = new TableHeaderCell();
            cell.ColumnSpan = 3;
            cell.Text = Resources.Attendance.Discount;
            row.Controls.Add(cell);
            cell = new TableHeaderCell();

            cell = new TableHeaderCell();
            cell.ColumnSpan = 1;
            cell.Text = Resources.Attendance.Total;
            row.Controls.Add(cell);
            cell = new TableHeaderCell();

            gvProduct.Controls[0].Controls.Add(row);
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
    protected string GetProductDescription(string strProductId)
    {
        string strProductDescription = string.Empty;
        if (strProductId != "0" && strProductId != "")
        {
            DataTable dtPName = objProductM.GetProductMasterById(StrCompId, strProductId);
            if (dtPName.Rows.Count > 0)
            {
                strProductDescription = dtPName.Rows[0]["Description"].ToString();
            }
            else
            {
                strProductDescription = "";
            }
        }
        else
        {
            strProductDescription = "";
        }
        return strProductDescription;
    }

    [System.Web.Services.WebMethodAttribute(), System.Web.Script.Services.ScriptMethodAttribute()]
    public static string[] GetCompletionListInvoiceNo(string prefixText, int count, string contextKey)
    {

        Inv_SalesInvoiceHeader objSalesInvoice = new Inv_SalesInvoiceHeader();
        DataTable dtSalesInvoice = objSalesInvoice.GetDistinctInvoiceNo(HttpContext.Current.Session["CompId"].ToString(), HttpContext.Current.Session["BrandId"].ToString(), HttpContext.Current.Session["LocId"].ToString(), prefixText);
        string[] txt = new string[dtSalesInvoice.Rows.Count];

        if (dtSalesInvoice.Rows.Count > 0)
        {
            for (int i = 0; i < dtSalesInvoice.Rows.Count; i++)
            {
                txt[i] = dtSalesInvoice.Rows[i]["Invoice_No"].ToString();
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
            dtSalesInvoice = objSalesInvoice.GetSInvHeaderAllTrue(HttpContext.Current.Session["CompId"].ToString(), HttpContext.Current.Session["BrandId"].ToString(), HttpContext.Current.Session["LocId"].ToString());
            if (dtSalesInvoice.Rows.Count > 0)
            {
                txt = new string[dtSalesInvoice.Rows.Count];
                for (int i = 0; i < dtSalesInvoice.Rows.Count; i++)
                {
                    txt[i] = dtSalesInvoice.Rows[i]["Invoice_No"].ToString();
                }
            }
            //}
        }
        return txt;
    }
    #endregion
}
