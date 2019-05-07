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

public partial class Sales_SalesInvoice : BasePage
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
    Inv_StockBatchMaster ObjStockBatchMaster = new Inv_StockBatchMaster();
    Inv_ProductLedger ObjProductLedger = new Inv_ProductLedger();

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
            FillPaymentMode();
            FillCurrency();
            txtSInvNo.Text = GetDocumentNumber();
            //txtSInvNo.Text = objSInvHeader.GetAutoID(StrCompId, StrBrandId, StrLocationId);
            txtSInvDate.Text = DateTime.Now.ToString(ObjSysParam.GetSysParameterByParamName("Date_Format").Rows[0]["Param_Value"].ToString());
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
                foreach (GridViewRow Row in GvSalesInvoice.Rows)
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
                    foreach (GridViewRow Row in GvSalesInvoice.Rows)
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
    public string GetDocumentNumber()
    {
        string DocumentNo = string.Empty;

        DataTable Dt = objDocNo.GetDocumentNumberAll(StrCompId, "13", "92");

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
                DocumentNo += "-" + (Convert.ToInt32(objSInvHeader.GetSInvHeaderAll(StrCompId.ToString(), StrBrandId, StrLocationId).Rows.Count) + 1).ToString();
            }
            else
            {
                DocumentNo += (Convert.ToInt32(objSInvHeader.GetSInvHeaderAll(StrCompId.ToString(), StrBrandId, StrLocationId).Rows.Count) + 1).ToString();
            }
        }
        else
        {
            DocumentNo += (Convert.ToInt32(objSInvHeader.GetSInvHeaderAll(StrCompId.ToString(), StrBrandId, StrLocationId).Rows.Count) + 1).ToString();
        }
        return DocumentNo;
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
        if (Convert.ToBoolean(dtInvEdit.Rows[0]["Post"]))
        {
            DisplayMessage("Sales Invoice has posted,can not be Update");
            return;
        }
        btnNew.Text = Resources.Attendance.Edit;
        if (dtInvEdit.Rows.Count > 0)
        {
            txtSInvNo.Text = dtInvEdit.Rows[0]["Invoice_No"].ToString();
            txtSInvNo.ReadOnly = true;
            txtSInvDate.Text = Convert.ToDateTime(dtInvEdit.Rows[0]["Invoice_Date"].ToString()).ToString(ObjSysParam.GetSysParameterByParamName("Date_Format").Rows[0]["Param_Value"].ToString());
            string strSOFromTransType = dtInvEdit.Rows[0]["SIFromTransType"].ToString();

            if (strSOFromTransType == "S")
            {
                txtTransFrom.Text = "Sales Order";
                ddlOrderType.SelectedValue = "Q";
                ddlOrderType_SelectedIndexChanged(sender, e);
                ddlSalesOrderNo.Visible = false;
                txtSalesOrderNo.Visible = true;
                hdnSalesOrderId.Value = dtInvEdit.Rows[0]["SIFromTransNo"].ToString();

                DataTable dtSalesOrderHeader = objSOrderHeader.GetSOHeaderAllByTransId(StrCompId, StrBrandId, StrLocationId, hdnSalesOrderId.Value);
                if (dtSalesOrderHeader.Rows.Count > 0)
                {
                    txtSalesOrderNo.Text = dtSalesOrderHeader.Rows[0]["SalesOrderNo"].ToString();

                    DataTable dtSalesOrderChild = ObjSOrderDetail.GetSODetailBySOrderNo(StrCompId, StrBrandId, StrLocationId, hdnSalesOrderId.Value);
                    if (dtSalesOrderChild.Rows.Count > 0)
                    {
                        GvSalesOrderDetail.DataSource = dtSalesOrderChild;
                        GvSalesOrderDetail.DataBind();
                        //FillUnit();

                        //float fGrossTotal = 0.00f;
                        foreach (GridViewRow gvr in GvSalesOrderDetail.Rows)
                        {
                            HiddenField hdngvProductId = (HiddenField)gvr.FindControl("hdngvProductId");
                            Label lblgvUnitPrice = (Label)gvr.FindControl("lblgvUnitPrice");
                            TextBox txtgvUnitCost = (TextBox)gvr.FindControl("txtgvUnitCost");
                            TextBox txtgvTaxP = (TextBox)gvr.FindControl("txtgvTaxP");
                            TextBox txtgvTaxV = (TextBox)gvr.FindControl("txtgvTaxV");
                            TextBox txtgvPriceAfterTax = (TextBox)gvr.FindControl("txtgvPriceAfterTax");
                            TextBox txtgvDiscountP = (TextBox)gvr.FindControl("txtgvDiscountP");
                            TextBox txtgvDiscountV = (TextBox)gvr.FindControl("txtgvDiscountV");
                            TextBox txtgvPriceAfterDiscount = (TextBox)gvr.FindControl("txtgvPriceAfterDiscount");
                            TextBox txtgvTotal = (TextBox)gvr.FindControl("txtgvTotal");

                            DataTable dtInvoiceDetailByInvoiceId = objSInvDetail.GetSInvDetailByInvoiceNo(StrCompId, StrBrandId, StrLocationId, editid.Value);

                            dtInvoiceDetailByInvoiceId = new DataView(dtInvoiceDetailByInvoiceId, "Product_Id='" + hdngvProductId.Value + "'", "", DataViewRowState.CurrentRows).ToTable();

                            if (dtInvoiceDetailByInvoiceId.Rows.Count > 0)
                            {
                                txtgvUnitCost.Text = dtInvoiceDetailByInvoiceId.Rows[0]["UnitCost"].ToString();
                                txtgvTaxP.Text = dtInvoiceDetailByInvoiceId.Rows[0]["TaxP"].ToString();
                                txtgvTaxV.Text = dtInvoiceDetailByInvoiceId.Rows[0]["TaxV"].ToString();
                                txtgvPriceAfterTax.Text = (float.Parse(lblgvUnitPrice.Text) + float.Parse(txtgvTaxV.Text)).ToString();
                                txtgvDiscountP.Text = dtInvoiceDetailByInvoiceId.Rows[0]["DiscountP"].ToString();
                                txtgvDiscountV.Text = dtInvoiceDetailByInvoiceId.Rows[0]["DiscountV"].ToString();
                                txtgvPriceAfterDiscount.Text = (float.Parse(txtgvPriceAfterTax.Text) - float.Parse(txtgvDiscountV.Text)).ToString();
                                txtgvTotal.Text = (float.Parse(txtgvPriceAfterTax.Text) - float.Parse(txtgvDiscountV.Text)).ToString();
                            }
                            else
                            {
                                txtgvUnitCost.Text = "0";
                                txtgvTaxP.Text = "0";
                                txtgvTaxV.Text = "0";
                                txtgvPriceAfterTax.Text = (float.Parse(lblgvUnitPrice.Text) + float.Parse(txtgvTaxV.Text)).ToString();
                                txtgvDiscountP.Text = "0";
                                txtgvDiscountV.Text = "0";
                                txtgvPriceAfterDiscount.Text = (float.Parse(txtgvPriceAfterTax.Text) - float.Parse(txtgvDiscountV.Text)).ToString();
                                txtgvTotal.Text = (float.Parse(txtgvPriceAfterTax.Text) - float.Parse(txtgvDiscountV.Text)).ToString();
                            }
                            //if (txtgvTotal.Text != "")
                            //{
                            //    fGrossTotal = fGrossTotal + float.Parse(txtgvTotal.Text);
                            //}
                        }
                        //txtAmount.Text = fGrossTotal.ToString();
                    }
                }
            }
            else
            {
                ddlOrderType.SelectedValue = "D";
                ddlOrderType_SelectedIndexChanged(sender, e);

                DataTable dtSInvDetail = objSInvDetail.GetSInvDetailByInvoiceNo(StrCompId, StrBrandId, StrLocationId, editid.Value);
                if (dtSInvDetail.Rows.Count > 0)
                {
                    GvProductDetail.DataSource = dtSInvDetail;
                    GvProductDetail.DataBind();

                    foreach (GridViewRow gvr in GvProductDetail.Rows)
                    {
                        Label lblgvUnitPrice = (Label)gvr.FindControl("lblgvUnitPrice");
                        Label lblgvTaxV = (Label)gvr.FindControl("lblgvTaxV");
                        Label lblgvPriceAfterTax = (Label)gvr.FindControl("lblgvPriceAfterTax");
                        Label lblgvDiscountV = (Label)gvr.FindControl("lblgvDiscountV");
                        Label lblgvPriceAfterDiscount = (Label)gvr.FindControl("lblgvPriceAfterDiscount");
                        Label lblgvTotal = (Label)gvr.FindControl("lblgvTotal");

                        if (lblgvUnitPrice.Text != "")
                        {
                            if (lblgvTaxV.Text != "")
                            {
                                lblgvPriceAfterTax.Text = (float.Parse(lblgvUnitPrice.Text) + float.Parse(lblgvTaxV.Text)).ToString();
                                lblgvTotal.Text = (float.Parse(lblgvUnitPrice.Text) + float.Parse(lblgvTaxV.Text)).ToString();
                            }
                            else
                            {
                                lblgvPriceAfterTax.Text = lblgvUnitPrice.Text;
                                lblgvTotal.Text = lblgvPriceAfterTax.Text;
                            }
                            if (lblgvDiscountV.Text != "")
                            {
                                lblgvPriceAfterDiscount.Text = (float.Parse(lblgvPriceAfterTax.Text) - float.Parse(lblgvDiscountV.Text)).ToString();
                                lblgvTotal.Text = (float.Parse(lblgvPriceAfterTax.Text) - float.Parse(lblgvDiscountV.Text)).ToString();
                            }
                            else
                            {
                                lblgvPriceAfterDiscount.Text = lblgvPriceAfterTax.Text;
                                lblgvTotal.Text = lblgvPriceAfterTax.Text;
                            }
                        }
                    }
                    GetGridTotal();
                }
            }

            txtTransFrom.Text = dtInvEdit.Rows[0]["SIFromTransType"].ToString();

            string strCustomerId = dtInvEdit.Rows[0]["Supplier_Id"].ToString();
            txtCustomer.Text = GetCustomerName(strCustomerId) + "/" + strCustomerId;

            ddlPaymentMode.SelectedValue = dtInvEdit.Rows[0]["PaymentModeId"].ToString();
            ddlCurrency.SelectedValue = dtInvEdit.Rows[0]["Currency_Id"].ToString();

            string strEmployeeId = dtInvEdit.Rows[0]["SalesPerson_Id"].ToString();
            txtSalesPerson.Text = GetEmployeeName(strEmployeeId) + "/" + strEmployeeId;

            txtPOSNo.Text = dtInvEdit.Rows[0]["PosNo"].ToString();
            txtAccountNo.Text = dtInvEdit.Rows[0]["Account_No"].ToString();
            txtInvoiceCosting.Text = dtInvEdit.Rows[0]["Invoice_Costing"].ToString();
            txtShift.Text = dtInvEdit.Rows[0]["Shift"].ToString();
            txtTender.Text = dtInvEdit.Rows[0]["Tender"].ToString();
            txtTotalQuantity.Text = dtInvEdit.Rows[0]["TotalQuantity"].ToString();

            txtAmount.Text = dtInvEdit.Rows[0]["TotalAmount"].ToString();
            txtTaxP.Text = dtInvEdit.Rows[0]["NetTaxP"].ToString();
            txtTaxV.Text = dtInvEdit.Rows[0]["NetTaxV"].ToString();
            txtNetAmount.Text = dtInvEdit.Rows[0]["NetAmount"].ToString();
            txtDiscountP.Text = dtInvEdit.Rows[0]["NetDiscountP"].ToString();
            txtDiscountV.Text = dtInvEdit.Rows[0]["NetDiscountV"].ToString();
            txtGrandTotal.Text = dtInvEdit.Rows[0]["GrandTotal"].ToString();

            txtCondition1.Text = dtInvEdit.Rows[0]["Condition1"].ToString();
            txtCondition2.Text = dtInvEdit.Rows[0]["Condition2"].ToString();
            txtCondition3.Text = dtInvEdit.Rows[0]["Condition3"].ToString();
            txtCondition4.Text = dtInvEdit.Rows[0]["Condition4"].ToString();
            txtCondition5.Text = dtInvEdit.Rows[0]["Condition5"].ToString();

            txtAmount_TextChanged(sender, e);
            txtRemark.Text = dtInvEdit.Rows[0]["Remark"].ToString();
            try
            {
                chkPost.Checked = Convert.ToBoolean(dtInvEdit.Rows[0]["Post"].ToString());
            }
            catch
            {
            
            }
        }
        btnNew_Click(null, null);
        System.Web.UI.ScriptManager.GetCurrent(this).SetFocus(txtSInvNo);
    }
    protected void GvSalesInvoice_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        GvSalesInvoice.PageIndex = e.NewPageIndex;
        DataTable dt = (DataTable)Session["dtFilter"];
        GvSalesInvoice.DataSource = dt;
        GvSalesInvoice.DataBind();
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
            GvSalesInvoice.DataSource = view.ToTable();
            Session["dtFilter"] = view.ToTable();
            lblTotalRecords.Text = Resources.Attendance.Total_Records + " : " + view.ToTable().Rows.Count + "";
            GvSalesInvoice.DataBind();
        }
    }
    protected void GvSalesInvoice_Sorting(object sender, GridViewSortEventArgs e)
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
        GvSalesInvoice.DataSource = dt;
        GvSalesInvoice.DataBind();
    }
    protected void IbtnDelete_Command(object sender, CommandEventArgs e)
    {
        editid.Value = e.CommandArgument.ToString();
        DataTable dtInvEdit = objSInvHeader.GetSInvHeaderAllByTransId(StrCompId, StrBrandId, StrLocationId, editid.Value);
        if (Convert.ToBoolean(dtInvEdit.Rows[0]["Post"]))
        {
            DisplayMessage("Sales Invoice has posted,can not be Delete");
            return;
        }
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
        if (txtSInvDate.Text == "")
        {
            DisplayMessage("Enter Sales Invoice Date");
            System.Web.UI.ScriptManager.GetCurrent(this).SetFocus(txtSInvDate);
            return;
        }
        if (txtSInvNo.Text == "")
        {
            DisplayMessage("Enter Sales Invoice No.");
            System.Web.UI.ScriptManager.GetCurrent(this).SetFocus(txtSInvNo);
            return;
        }
        else
        {
            if (editid.Value == "")
            {
                DataTable dtSQNo = objSInvHeader.GetSInvHeaderAllByInvoiceNo(StrCompId, StrBrandId, StrLocationId, txtSInvNo.Text);
                if (dtSQNo.Rows.Count > 0)
                {
                    DisplayMessage("Sales Invoice No. Already Exits");
                    txtSInvNo.Text = "";
                    System.Web.UI.ScriptManager.GetCurrent(this).SetFocus(txtSInvNo);
                    return;
                }
            }
        }

        if (ddlOrderType.SelectedValue != "--Select--")
        {
            if (ddlOrderType.SelectedValue == "D")
            {
                if (GvProductDetail.Rows.Count > 0)
                {

                }
                else
                {
                    DisplayMessage("You have no Product For Generate Sales Order");
                    return;
                }
            }
            else if (ddlOrderType.SelectedValue == "Q")
            {
                if (GvSalesOrderDetail.Rows.Count > 0)
                {

                }
                else
                {
                    DisplayMessage("No Product available For Generate Sales Invoice");
                    return;
                }
            }
        }
        else
        {
            DisplayMessage("Select Order Type");
            System.Web.UI.ScriptManager.GetCurrent(this).SetFocus(ddlOrderType);
            return;
        }

        string strCustomerId = string.Empty;
        if (txtCustomer.Text != "")
        {
            if (GetCustomerId() != "")
            {

            }
            else
            {
                txtCustomer.Text = "";
                DisplayMessage("Customer Choose In Suggestion Only");
                System.Web.UI.ScriptManager.GetCurrent(this).SetFocus(txtCustomer);
                return;
            }
        }
        else
        {
            DisplayMessage("Enter Customer Name");
            System.Web.UI.ScriptManager.GetCurrent(this).SetFocus(txtCustomer);
            return;
        }
        if (ddlPaymentMode.Text == "--Select--")
        {
            DisplayMessage("Select PaymentMode");
            ddlPaymentMode.Focus();
            return;
        }
        if (ddlCurrency.Text == "--Select--")
        {
            DisplayMessage("Select Currency");
            ddlCurrency.Focus();
            return;
        }

        if (txtSalesPerson.Text != "")
        {
            if (GetEmployeeId(txtSalesPerson.Text) != "")
            {

            }
            else
            {
                txtSalesPerson.Text = "";
                DisplayMessage("Sales Person Choose In Suggestion Only");
                System.Web.UI.ScriptManager.GetCurrent(this).SetFocus(txtSalesPerson);
                return;
            }
        }
        else
        {
            DisplayMessage("Enter Sales Person");
            System.Web.UI.ScriptManager.GetCurrent(this).SetFocus(txtSalesPerson);
            return;
        }

        string strPost = string.Empty;
        if (chkPost.Checked == true)
        {
            strPost = "True";
        }
        else if (chkPost.Checked == false)
        {
            strPost = "False";
        }

        if (txtAmount.Text == "")
        {
            txtAmount.Text = "0";
        }
        if (txtTaxP.Text == "")
        {
            txtTaxP.Text = "0";
        }
        if (txtTaxV.Text == "")
        {
            txtTaxV.Text = "0";
        }
        if (txtDiscountP.Text == "")
        {
            txtDiscountP.Text = "0";
        }
        if (txtDiscountV.Text == "")
        {
            txtDiscountV.Text = "0";
        }
        if (txtGrandTotal.Text == "")
        {
            txtGrandTotal.Text = "0";
        }
        if (txtInvoiceCosting.Text == "")
        {
            txtInvoiceCosting.Text = "0";
        }
        if (txtNetAmount.Text == "")
        {
            txtNetAmount.Text = "0";
        }
        if (txtTender.Text == "")
        {
            txtTender.Text = "0";
        }

        int b = 0;
        if (editid.Value != "")
        {
            b = objSInvHeader.UpdateSInvHeader(StrCompId, StrBrandId, StrLocationId, editid.Value, txtSInvNo.Text, txtSInvDate.Text, ddlPaymentMode.SelectedValue, ddlCurrency.SelectedValue, txtTransFrom.Text, hdnSalesOrderId.Value, GetEmployeeId(txtSalesPerson.Text), txtPOSNo.Text, txtRemark.Text, txtAccountNo.Text, txtInvoiceCosting.Text, txtShift.Text, strPost, txtTender.Text, txtAmount.Text, txtTotalQuantity.Text, txtAmount.Text, txtTaxP.Text, txtTaxV.Text, txtNetAmount.Text, txtDiscountP.Text, txtDiscountV.Text, txtGrandTotal.Text, GetCustomerId(), txtCondition1.Text, txtCondition2.Text, txtCondition3.Text, txtCondition4.Text, txtCondition5.Text, "", "", "", "", "", "True", DateTime.Now.ToString(), "True", Session["UserId"].ToString(), DateTime.Now.ToString());

            if (b != 0)
            {
                objSInvDetail.DeleteSInvDetail(StrCompId, StrBrandId, StrLocationId, editid.Value);
                if (ddlOrderType.SelectedValue == "D")
                {
                    foreach (GridViewRow gvr in GvProductDetail.Rows)
                    {
                        Label lblgvSerialNo = (Label)gvr.FindControl("lblgvSerialNo");
                        HiddenField hdngvProductId = (HiddenField)gvr.FindControl("hdngvProductId");
                        Label lblgvProductDescription = (Label)gvr.FindControl("lblgvProductDescription");
                        HiddenField hdngvUnitId = (HiddenField)gvr.FindControl("hdngvUnitId");
                        Label lblgvUnitPrice = (Label)gvr.FindControl("lblgvUnitPrice");
                        Label lblgvUnitCost = (Label)gvr.FindControl("lblgvUnitCost");
                        Label lblgvQuantity = (Label)gvr.FindControl("lblgvQuantity");
                        Label lblgvTaxP = (Label)gvr.FindControl("lblgvTaxP");
                        Label lblgvTaxV = (Label)gvr.FindControl("lblgvTaxV");
                        Label lblgvDiscountP = (Label)gvr.FindControl("lblgvDiscountP");
                        Label lblgvDiscountV = (Label)gvr.FindControl("lblgvDiscountV");

                        if (lblgvUnitPrice.Text == "")
                        {
                            lblgvUnitPrice.Text = "0";
                        }
                        if (lblgvUnitCost.Text == "")
                        {
                            lblgvUnitCost.Text = "0";
                        }
                        if (lblgvQuantity.Text == "")
                        {
                            lblgvQuantity.Text = "0";
                        }
                        if (lblgvTaxP.Text == "")
                        {
                            lblgvTaxP.Text = "0";
                        }
                        if (lblgvTaxV.Text == "")
                        {
                            lblgvTaxV.Text = "0";
                        }
                        if (lblgvDiscountP.Text == "")
                        {
                            lblgvDiscountP.Text = "0";
                        }
                        if (lblgvDiscountV.Text == "")
                        {
                            lblgvDiscountV.Text = "0";
                        }

                        objSInvDetail.InsertSInvDetail(StrCompId, StrBrandId, StrLocationId, editid.Value, lblgvSerialNo.Text, ddlPaymentMode.SelectedValue, txtPOSNo.Text, txtTransFrom.Text, hdnSalesOrderId.Value, hdngvProductId.Value, lblgvProductDescription.Text, hdngvUnitId.Value, lblgvUnitPrice.Text, lblgvUnitCost.Text, lblgvQuantity.Text, "0", lblgvTaxP.Text, lblgvTaxV.Text, lblgvDiscountP.Text, lblgvDiscountV.Text, "False", "", "", "", "", "", "True", DateTime.Now.ToString(), "True", Session["UserId"].ToString(), DateTime.Now.ToString(), Session["UserId"].ToString(), DateTime.Now.ToString());
                    }
                }
                else if (ddlOrderType.SelectedValue == "Q")
                {
                    foreach (GridViewRow gvrQ in GvSalesOrderDetail.Rows)
                    {
                        Label lblgvSerialNo = (Label)gvrQ.FindControl("lblgvSerialNo");
                        HiddenField hdngvProductId = (HiddenField)gvrQ.FindControl("hdngvProductId");
                        Label lblgvProductDescription = (Label)gvrQ.FindControl("lblgvProductDescription");
                        HiddenField hdngvUnitId = (HiddenField)gvrQ.FindControl("hdngvUnitId");
                        Label lblgvUnitPrice = (Label)gvrQ.FindControl("lblgvUnitPrice");
                        TextBox txtgvUnitCost = (TextBox)gvrQ.FindControl("txtgvUnitCost");
                        Label lblgvQuantity = (Label)gvrQ.FindControl("lblgvQuantity");
                        TextBox txtgvTaxP = (TextBox)gvrQ.FindControl("txtgvTaxP");
                        TextBox txtgvTaxV = (TextBox)gvrQ.FindControl("txtgvTaxV");
                        TextBox txtgvDiscountP = (TextBox)gvrQ.FindControl("txtgvDiscountP");
                        TextBox txtgvDiscountV = (TextBox)gvrQ.FindControl("txtgvDiscountV");

                        if (lblgvUnitPrice.Text == "")
                        {
                            lblgvUnitPrice.Text = "0";
                        }
                        if (txtgvUnitCost.Text == "")
                        {
                            txtgvUnitCost.Text = "0";
                        }
                        if (lblgvQuantity.Text == "")
                        {
                            lblgvQuantity.Text = "0";
                        }
                        if (txtgvTaxP.Text == "")
                        {
                            txtgvTaxP.Text = "0";
                        }
                        if (txtgvTaxV.Text == "")
                        {
                            txtgvTaxV.Text = "0";
                        }
                        if (txtgvDiscountP.Text == "")
                        {
                            txtgvDiscountP.Text = "0";
                        }
                        if (txtgvDiscountV.Text == "")
                        {
                            txtgvDiscountV.Text = "0";
                        }
                        objSInvDetail.InsertSInvDetail(StrCompId, StrBrandId, StrLocationId, editid.Value, lblgvSerialNo.Text, ddlPaymentMode.SelectedValue, txtPOSNo.Text, txtTransFrom.Text, hdnSalesOrderId.Value, hdngvProductId.Value, lblgvProductDescription.Text, hdngvUnitId.Value, lblgvUnitPrice.Text, txtgvUnitCost.Text, lblgvQuantity.Text, "0", txtgvTaxP.Text, txtgvTaxV.Text, txtgvDiscountP.Text, txtgvDiscountV.Text, "False", "", "", "", "", "", "True", DateTime.Now.ToString(), "True", Session["UserId"].ToString(), DateTime.Now.ToString(), Session["UserId"].ToString(), DateTime.Now.ToString());
                    }
                }

                DisplayMessage("Record Updated");

                editid.Value = "";
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
            b = objSInvHeader.InsertSInvHeader(StrCompId, StrBrandId, StrLocationId, txtSInvNo.Text, txtSInvDate.Text, ddlPaymentMode.SelectedValue, ddlCurrency.SelectedValue, txtTransFrom.Text, hdnSalesOrderId.Value, GetEmployeeId(txtSalesPerson.Text), txtPOSNo.Text, txtRemark.Text, txtAccountNo.Text, txtInvoiceCosting.Text, txtShift.Text, strPost, txtTender.Text, txtAmount.Text, txtTotalQuantity.Text, txtAmount.Text, txtTaxP.Text, txtTaxV.Text, txtNetAmount.Text, txtDiscountP.Text, txtDiscountV.Text, txtGrandTotal.Text, GetCustomerId(), txtCondition1.Text, txtCondition2.Text, txtCondition3.Text, txtCondition4.Text, txtCondition5.Text, "", "", "", "", "", "True", DateTime.Now.ToString(), "True", Session["UserId"].ToString(), DateTime.Now.ToString(), Session["UserId"].ToString(), DateTime.Now.ToString());
            if (b != 0)
            {
                string strMaxId = string.Empty;
                DataTable dtMaxId = objSInvHeader.GetMaxSalesInvoiceId(StrCompId, StrBrandId, StrLocationId);
                if (dtMaxId.Rows.Count > 0)
                {
                    strMaxId = dtMaxId.Rows[0][0].ToString();
                }

                if (strMaxId != "" && strMaxId != "0")
                {
                    if (ddlOrderType.SelectedValue == "D")
                    {
                        foreach (GridViewRow gvr in GvProductDetail.Rows)
                        {
                            Label lblgvSerialNo = (Label)gvr.FindControl("lblgvSerialNo");
                            HiddenField hdngvProductId = (HiddenField)gvr.FindControl("hdngvProductId");
                            Label lblgvProductDescription = (Label)gvr.FindControl("lblgvProductDescription");
                            HiddenField hdngvUnitId = (HiddenField)gvr.FindControl("hdngvUnitId");
                            Label lblgvUnitPrice = (Label)gvr.FindControl("lblgvUnitPrice");
                            Label lblgvUnitCost = (Label)gvr.FindControl("lblgvUnitCost");
                            Label lblgvQuantity = (Label)gvr.FindControl("lblgvQuantity");
                            Label lblgvTaxP = (Label)gvr.FindControl("lblgvTaxP");
                            Label lblgvTaxV = (Label)gvr.FindControl("lblgvTaxV");
                            Label lblgvDiscountP = (Label)gvr.FindControl("lblgvDiscountP");
                            Label lblgvDiscountV = (Label)gvr.FindControl("lblgvDiscountV");

                            if (lblgvUnitPrice.Text == "")
                            {
                                lblgvUnitPrice.Text = "0";
                            }
                            if (lblgvUnitCost.Text == "")
                            {
                                lblgvUnitCost.Text = "0";
                            }
                            if (lblgvQuantity.Text == "")
                            {
                                lblgvQuantity.Text = "0";
                            }
                            if (lblgvTaxP.Text == "")
                            {
                                lblgvTaxP.Text = "0";
                            }
                            if (lblgvTaxV.Text == "")
                            {
                                lblgvTaxV.Text = "0";
                            }
                            if (lblgvDiscountP.Text == "")
                            {
                                lblgvDiscountP.Text = "0";
                            }
                            if (lblgvDiscountV.Text == "")
                            {
                                lblgvDiscountV.Text = "0";
                            }

                            objSInvDetail.InsertSInvDetail(StrCompId, StrBrandId, StrLocationId, strMaxId, lblgvSerialNo.Text, ddlPaymentMode.SelectedValue, txtPOSNo.Text, txtTransFrom.Text, hdnSalesOrderId.Value, hdngvProductId.Value, lblgvProductDescription.Text, hdngvUnitId.Value, lblgvUnitPrice.Text, lblgvUnitCost.Text, lblgvQuantity.Text, "0", lblgvTaxP.Text, lblgvTaxV.Text, lblgvDiscountP.Text, lblgvDiscountV.Text, "False", "", "", "", "", "", "True", DateTime.Now.ToString(), "True", Session["UserId"].ToString(), DateTime.Now.ToString(), Session["UserId"].ToString(), DateTime.Now.ToString());
                        }
                    }
                    else if (ddlOrderType.SelectedValue == "Q")
                    {
                        foreach (GridViewRow gvrQ in GvSalesOrderDetail.Rows)
                        {
                            Label lblgvSerialNo = (Label)gvrQ.FindControl("lblgvSerialNo");
                            HiddenField hdngvProductId = (HiddenField)gvrQ.FindControl("hdngvProductId");
                            Label lblgvProductDescription = (Label)gvrQ.FindControl("lblgvProductDescription");
                            HiddenField hdngvUnitId = (HiddenField)gvrQ.FindControl("hdngvUnitId");
                            Label lblgvUnitPrice = (Label)gvrQ.FindControl("lblgvUnitPrice");
                            TextBox txtgvUnitCost = (TextBox)gvrQ.FindControl("txtgvUnitCost");
                            Label lblgvQuantity = (Label)gvrQ.FindControl("lblgvQuantity");
                            TextBox txtgvTaxP = (TextBox)gvrQ.FindControl("txtgvTaxP");
                            TextBox txtgvTaxV = (TextBox)gvrQ.FindControl("txtgvTaxV");
                            TextBox txtgvDiscountP = (TextBox)gvrQ.FindControl("txtgvDiscountP");
                            TextBox txtgvDiscountV = (TextBox)gvrQ.FindControl("txtgvDiscountV");

                            if (lblgvUnitPrice.Text == "")
                            {
                                lblgvUnitPrice.Text = "0";
                            }
                            if (txtgvUnitCost.Text == "")
                            {
                                txtgvUnitCost.Text = "0";
                            }
                            if (txtgvTaxP.Text == "")
                            {
                                txtgvTaxP.Text = "0";
                            }
                            if (txtgvTaxV.Text == "")
                            {
                                txtgvTaxV.Text = "0";
                            }
                            if (txtgvDiscountP.Text == "")
                            {
                                txtgvDiscountP.Text = "0";
                            }
                            if (txtgvDiscountV.Text == "")
                            {
                                txtgvDiscountV.Text = "0";
                            }

                            objSInvDetail.InsertSInvDetail(StrCompId, StrBrandId, StrLocationId, strMaxId, lblgvSerialNo.Text, ddlPaymentMode.SelectedValue, txtPOSNo.Text, txtTransFrom.Text, hdnSalesOrderId.Value, hdngvProductId.Value, lblgvProductDescription.Text, hdngvUnitId.Value, lblgvUnitPrice.Text, txtgvUnitCost.Text, lblgvQuantity.Text, "0", txtgvTaxP.Text, txtgvTaxV.Text, txtgvDiscountP.Text, txtgvDiscountV.Text, "False", "", "", "", "", "", "True", DateTime.Now.ToString(), "True", Session["UserId"].ToString(), DateTime.Now.ToString(), Session["UserId"].ToString(), DateTime.Now.ToString());
                        }
                    }
                }
                DisplayMessage("Record Saved");

                FillGrid();

            }
            else
            {
                DisplayMessage("Record  Not Saved");
            }
        }

        if (chkPost.Checked)
        {
            string strMaxId = string.Empty;
            if (editid.Value == "")
            {
                DataTable dtMaxId = objSInvHeader.GetMaxSalesInvoiceId(StrCompId, StrBrandId, StrLocationId);
                if (dtMaxId.Rows.Count > 0)
                {
                    strMaxId = dtMaxId.Rows[0][0].ToString();
                }
            }
            else
            {
                strMaxId = editid.Value.ToString();

            }
            if (ddlOrderType.SelectedValue == "D")
            {
                foreach (GridViewRow gvr in GvProductDetail.Rows)
                {
                    HiddenField hdngvProductId = (HiddenField)gvr.FindControl("hdngvProductId");
                    HiddenField hdngvUnitId = (HiddenField)gvr.FindControl("hdngvUnitId");
                    Label lblgvQuantity = (Label)gvr.FindControl("lblgvQuantity");


                    ObjProductLedger.InsertProductLedger(StrCompId.ToString(), StrBrandId.ToString(), StrLocationId.ToString(), "SI", strMaxId, "0", hdngvProductId.Value, hdngvUnitId.Value, "O", "0", "0", "0", lblgvQuantity.Text, "1/1/1800", "0", "1/1/1800", "0", "1/1/1800", "0", "", "", "", "", "", true.ToString(), "1/1/1800", true.ToString(), Session["UserId"].ToString(), "1/1/1800", Session["UserId"].ToString().ToString(), "1/1/1800");

                }
            }
            else if (ddlOrderType.SelectedValue == "Q")
            {
                foreach (GridViewRow gvrQ in GvSalesOrderDetail.Rows)
                {
                    HiddenField hdngvProductId = (HiddenField)gvrQ.FindControl("hdngvProductId");
                    HiddenField hdngvUnitId = (HiddenField)gvrQ.FindControl("hdngvUnitId");
                    Label lblgvQuantity = (Label)gvrQ.FindControl("lblgvQuantity");

                    ObjProductLedger.InsertProductLedger(StrCompId.ToString(), StrBrandId.ToString(), StrLocationId.ToString(), "SI", strMaxId, "0", hdngvProductId.Value, hdngvUnitId.Value, "O", "0", "0", "0", lblgvQuantity.Text, "1/1/1800", "0", "1/1/1800", "0", "1/1/1800", "0", "", "", "", "", "", true.ToString(), "1/1/1800", true.ToString(), Session["UserId"].ToString(), "1/1/1800", Session["UserId"].ToString().ToString(), "1/1/1800");
                }
            }
        }
        Reset();
    }
    protected void txtSalesPerson_TextChanged(object sender, EventArgs e)
    {
        string strEmployeeId = string.Empty;
        if (txtSalesPerson.Text != "")
        {
            strEmployeeId = GetEmployeeId(txtSalesPerson.Text);
            if (strEmployeeId != "" && strEmployeeId != "0")
            {

            }
            else
            {
                DisplayMessage("Select Employee In Suggestions Only");
                txtSalesPerson.Text = "";
                System.Web.UI.ScriptManager.GetCurrent(this).SetFocus(txtSalesPerson);
            }
        }
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
    protected void GvSalesInvoiceBin_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        GvSalesInvoiceBin.PageIndex = e.NewPageIndex;

        DataTable dt = (DataTable)Session["dtInactive"];
        GvSalesInvoiceBin.DataSource = dt;
        GvSalesInvoiceBin.DataBind();

        string temp = string.Empty;

        for (int i = 0; i < GvSalesInvoiceBin.Rows.Count; i++)
        {
            HiddenField lblconid = (HiddenField)GvSalesInvoiceBin.Rows[i].FindControl("hdnTransId");
            string[] split = lblSelectedRecord.Text.Split(',');

            for (int j = 0; j < lblSelectedRecord.Text.Split(',').Length; j++)
            {
                if (lblSelectedRecord.Text.Split(',')[j] != "")
                {
                    if (lblconid.Value.Trim().ToString() == lblSelectedRecord.Text.Split(',')[j].Trim().ToString())
                    {
                        ((CheckBox)GvSalesInvoiceBin.Rows[i].FindControl("chkSelect")).Checked = true;
                    }
                }
            }
        }
    }
    protected void GvSalesInvoiceBin_OnSorting(object sender, GridViewSortEventArgs e)
    {
        HDFSortbin.Value = HDFSortbin.Value == "ASC" ? "DESC" : "ASC";
        DataTable dt = new DataTable();
        dt = objSOrderHeader.GetSOHeaderAllFalse(StrCompId, StrBrandId, StrLocationId);
        DataView dv = new DataView(dt);
        string Query = "" + e.SortExpression + " " + HDFSortbin.Value + "";
        dv.Sort = Query;
        dt = dv.ToTable();
        Session["dtInactive"] = dt;
        GvSalesInvoiceBin.DataSource = dt;
        GvSalesInvoiceBin.DataBind();
        lblSelectedRecord.Text = "";
    }
    public void FillGridBin()
    {
        DataTable dt = new DataTable();
        dt = objSInvHeader.GetSInvHeaderAllFalse(StrCompId, StrBrandId, StrLocationId);
        GvSalesInvoiceBin.DataSource = dt;
        GvSalesInvoiceBin.DataBind();
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
            GvSalesInvoiceBin.DataSource = view.ToTable();
            GvSalesInvoiceBin.DataBind();
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

        if (GvSalesInvoiceBin.Rows.Count != 0)
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
                foreach (GridViewRow Gvr in GvSalesInvoiceBin.Rows)
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
        CheckBox chkSelAll = ((CheckBox)GvSalesInvoiceBin.HeaderRow.FindControl("chkCurrent"));
        for (int i = 0; i < GvSalesInvoiceBin.Rows.Count; i++)
        {
            ((CheckBox)GvSalesInvoiceBin.Rows[i].FindControl("chkSelect")).Checked = chkSelAll.Checked;
            if (chkSelAll.Checked)
            {
                if (!lblSelectedRecord.Text.Split(',').Contains(((HiddenField)(GvSalesInvoiceBin.Rows[i].FindControl("hdnTransId"))).Value.Trim().ToString()))
                {
                    lblSelectedRecord.Text += ((HiddenField)(GvSalesInvoiceBin.Rows[i].FindControl("hdnTransId"))).Value.Trim().ToString() + ",";
                }
            }
            else
            {
                string temp = string.Empty;
                string[] split = lblSelectedRecord.Text.Split(',');
                foreach (string item in split)
                {
                    if (item != ((HiddenField)(GvSalesInvoiceBin.Rows[i].FindControl("hdnTransId"))).Value.Trim().ToString())
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
        HiddenField lb = (HiddenField)GvSalesInvoiceBin.Rows[index].FindControl("hdnTransId");
        if (((CheckBox)GvSalesInvoiceBin.Rows[index].FindControl("chkSelect")).Checked)
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
            for (int i = 0; i < GvSalesInvoiceBin.Rows.Count; i++)
            {
                string[] split = lblSelectedRecord.Text.Split(',');
                HiddenField lblconid = (HiddenField)GvSalesInvoiceBin.Rows[i].FindControl("hdnTransId");
                for (int j = 0; j < lblSelectedRecord.Text.Split(',').Length; j++)
                {
                    if (lblSelectedRecord.Text.Split(',')[j] != "")
                    {
                        if (lblconid.Value.Trim().ToString() == lblSelectedRecord.Text.Split(',')[j].Trim().ToString())
                        {
                            ((CheckBox)GvSalesInvoiceBin.Rows[i].FindControl("chkSelect")).Checked = true;
                        }
                    }
                }
            }
        }
        else
        {
            lblSelectedRecord.Text = "";
            DataTable dtUnit1 = (DataTable)Session["dtInactive"];
            GvSalesInvoiceBin.DataSource = dtUnit1;
            GvSalesInvoiceBin.DataBind();
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
            foreach (GridViewRow Gvr in GvSalesInvoiceBin.Rows)
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
            GvSalesInvoice.DataSource = dtBrand;
            GvSalesInvoice.DataBind();
        }
        else
        {
            GvSalesInvoice.DataSource = null;
            GvSalesInvoice.DataBind();
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
        txtSInvNo.Text = objSOrderHeader.GetAutoID(StrCompId, StrBrandId, StrLocationId);
        txtSInvDate.Text = DateTime.Now.ToString(ObjSysParam.GetSysParameterByParamName("Date_Format").Rows[0]["Param_Value"].ToString());
        txtSInvNo.ReadOnly = false;
        txtCustomer.Text = "";
        ddlOrderType.SelectedValue = "--Select--";
        txtTransFrom.Text = "";
        txtSalesOrderNo.Text = "";
        FillSalesOrderNo();
        FillPaymentMode();
        FillCurrency();
        txtSalesPerson.Text = "";
        txtPOSNo.Text = "";
        txtAccountNo.Text = "";
        txtInvoiceCosting.Text = "";
        txtShift.Text = "";
        txtTender.Text = "";
        txtTotalQuantity.Text = "";
        txtAmount.Text = "";
        txtTaxP.Text = "";
        txtTaxV.Text = "";
        txtNetAmount.Text = "";
        txtDiscountP.Text = "";
        txtDiscountV.Text = "";
        txtGrandTotal.Text = "";
        txtRemark.Text = "";
        txtCondition1.Text = "";
        txtCondition2.Text = "";
        txtCondition3.Text = "";
        txtCondition4.Text = "";
        txtCondition5.Text = "";

        GvSalesOrderDetail.DataSource = null;
        GvSalesOrderDetail.DataBind();

        GvProductDetail.DataSource = null;
        GvProductDetail.DataBind();

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
        txtSInvNo.Text = GetDocumentNumber();
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
    private void FillSalesOrderNo()
    {
        //Check Query Like Sales Quotation No.
        DataTable dsSalesOrderNo = null;
        dsSalesOrderNo = objSOrderHeader.GetDataForSalesInvoice(StrCompId, StrBrandId, StrLocationId);
        if (dsSalesOrderNo.Rows.Count > 0)
        {
            ddlSalesOrderNo.DataSource = dsSalesOrderNo;
            ddlSalesOrderNo.DataTextField = "SalesOrderNo";
            ddlSalesOrderNo.DataValueField = "Trans_Id";
            ddlSalesOrderNo.DataBind();

            ddlSalesOrderNo.Items.Add("--Select--");
            ddlSalesOrderNo.SelectedValue = "--Select--";
        }
        else
        {
            ddlSalesOrderNo.Items.Add("--Select--");
            ddlSalesOrderNo.SelectedValue = "--Select--";
        }
    }
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
    private void FillCurrency()
    {
        DataTable dsCurrency = null;
        dsCurrency = objCurrency.GetCurrencyMaster();
        if (dsCurrency.Rows.Count > 0)
        {
            ddlCurrency.DataSource = dsCurrency;
            ddlCurrency.DataTextField = "Currency_Name";
            ddlCurrency.DataValueField = "Currency_ID";
            ddlCurrency.DataBind();

            ddlCurrency.Items.Add("--Select--");
            ddlCurrency.SelectedValue = "--Select--";
        }
        else
        {
            ddlCurrency.Items.Add("--Select--");
            ddlCurrency.SelectedValue = "--Select--";
        }
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
            ddlOrderType.SelectedValue = "Q";
            txtTransFrom.Text = "Sales Order";
            txtSalesOrderNo.Visible = true;
            ddlSalesOrderNo.Visible = false;
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
                                    txtgvTaxP_TextChanged(sender, e);
                                }
                                txtgvTaxV.Text = dtDetail.Rows[i]["TaxV"].ToString();
                                if (txtgvTaxV.Text != "0.000" && txtgvTaxV.Text != "")
                                {
                                    txtgvTaxV_TextChanged(sender, e);
                                }

                                //txtgvPriceAfterTax.Text = dtDetail.Rows[i]["PriceAfterTax"].ToString();
                                txtgvDiscountP.Text = dtDetail.Rows[i]["DiscountP"].ToString();
                                if (txtgvDiscountP.Text != "0.000" && txtgvDiscountP.Text != "")
                                {
                                    txtgvDiscountP_TextChanged(sender, e);
                                }

                                txtgvDiscountV.Text = dtDetail.Rows[i]["DiscountV"].ToString();
                                if (txtgvDiscountV.Text != "0.000" && txtgvDiscountV.Text != "")
                                {
                                    txtgvDiscountV_TextChanged(sender, e);
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

                txtTotalQuantity.Text = fGrossTotal.ToString();
            }

            txtTaxP.Text = dtPRequest.Rows[0]["TaxP"].ToString();
            if (txtTaxP.Text != "0.000" && txtTaxP.Text != "")
            {
                txtTaxP_TextChanged(sender, e);
            }
            txtTaxV.Text = dtPRequest.Rows[0]["TaxV"].ToString();
            if (txtTaxV.Text != "0.000" && txtTaxV.Text != "")
            {
                txtTaxV_TextChanged(sender, e);
            }
            txtDiscountP.Text = dtPRequest.Rows[0]["DiscountP"].ToString();
            if (txtDiscountP.Text != "0.000" && txtDiscountP.Text != "")
            {
                txtDiscountP_TextChanged(sender, e);
            }
            txtDiscountV.Text = dtPRequest.Rows[0]["DiscountV"].ToString();
            if (txtDiscountV.Text != "0.000" && txtDiscountV.Text != "")
            {
                txtDiscountV_TextChanged(sender, e);
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
    #endregion

    #region Calculations
    protected void txtTaxP_TextChanged(object sender, EventArgs e)
    {
        if (txtAmount.Text != "")
        {
            float flTemp = 0;
            if (float.TryParse(txtAmount.Text, out flTemp))
            {
                if (txtTaxP.Text != "")
                {
                    if (float.TryParse(txtTaxP.Text, out flTemp))
                    {
                        txtTaxV.Text = ((float.Parse(txtAmount.Text) * float.Parse(txtTaxP.Text)) / 100).ToString();
                        if (txtAmount.Text != "")
                        {
                            txtNetAmount.Text = (float.Parse(txtAmount.Text) + float.Parse(txtTaxV.Text)).ToString();
                        }
                        if (txtDiscountP.Text != "")
                        {
                            txtDiscountP_TextChanged(sender, e);
                        }
                        else
                        {
                            txtGrandTotal.Text = txtNetAmount.Text;
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
                txtAmount.Text = "";
                ScriptManager.RegisterStartupScript(this, typeof(string), "OPEN_WINDOW", "window.alert('Get Numeric Value Only');", true);
                System.Web.UI.ScriptManager.GetCurrent(this).SetFocus(txtAmount);
            }
        }
        else
        {
            txtTaxP.Text = "";
            ScriptManager.RegisterStartupScript(this, typeof(string), "OPEN_WINDOW", "window.alert('First Get Amount');", true);
            System.Web.UI.ScriptManager.GetCurrent(this).SetFocus(txtAmount);
        }
    }
    protected void txtTaxV_TextChanged(object sender, EventArgs e)
    {
        if (txtAmount.Text != "")
        {
            float flTemp = 0;
            if (float.TryParse(txtAmount.Text, out flTemp))
            {
                if (txtTaxV.Text != "")
                {
                    if (float.TryParse(txtTaxV.Text, out flTemp))
                    {
                        txtTaxP.Text = ((100 * float.Parse(txtTaxV.Text)) / float.Parse(txtAmount.Text)).ToString();
                        if (txtAmount.Text != "")
                        {
                            txtNetAmount.Text = (float.Parse(txtAmount.Text) + float.Parse(txtTaxV.Text)).ToString();
                            txtAmount.Text = (float.Parse(txtAmount.Text) + float.Parse(txtNetAmount.Text)).ToString();
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
                txtAmount.Text = "";
                ScriptManager.RegisterStartupScript(this, typeof(string), "OPEN_WINDOW", "window.alert('Get Numeric Value Only');", true);
                System.Web.UI.ScriptManager.GetCurrent(this).SetFocus(txtAmount);
            }
        }
        else
        {
            txtTaxV.Text = "";
            ScriptManager.RegisterStartupScript(this, typeof(string), "OPEN_WINDOW", "window.alert('First Get Amount');", true);
            System.Web.UI.ScriptManager.GetCurrent(this).SetFocus(txtAmount);
        }
    }
    protected void txtDiscountP_TextChanged(object sender, EventArgs e)
    {
        if (txtAmount.Text != "")
        {
            float flTemp = 0;
            if (float.TryParse(txtAmount.Text, out flTemp))
            {
                if (txtDiscountP.Text != "")
                {
                    if (float.TryParse(txtDiscountP.Text, out flTemp))
                    {
                        if (txtNetAmount.Text != "")
                        {
                            if (txtNetAmount.Text == "0")
                            {
                                txtDiscountV.Text = ((float.Parse(txtAmount.Text) * float.Parse(txtDiscountP.Text)) / 100).ToString();
                                txtGrandTotal.Text = (float.Parse(txtAmount.Text) - float.Parse(txtDiscountV.Text)).ToString();
                            }
                            else
                            {
                                txtDiscountV.Text = ((float.Parse(txtNetAmount.Text) * float.Parse(txtDiscountP.Text)) / 100).ToString();
                                txtGrandTotal.Text = (float.Parse(txtNetAmount.Text) - float.Parse(txtDiscountV.Text)).ToString();
                            }
                            System.Web.UI.ScriptManager.GetCurrent(this).SetFocus(txtRemark);
                        }
                    }
                    else
                    {
                        txtDiscountP.Text = "";
                        ScriptManager.RegisterStartupScript(this, typeof(string), "OPEN_WINDOW", "window.alert('Enter Numeric Value Only');", true);
                        System.Web.UI.ScriptManager.GetCurrent(this).SetFocus(txtDiscountP);
                    }
                }
            }
            else
            {
                txtAmount.Text = "";
                ScriptManager.RegisterStartupScript(this, typeof(string), "OPEN_WINDOW", "window.alert('Get Numeric Value Only');", true);
                System.Web.UI.ScriptManager.GetCurrent(this).SetFocus(txtAmount);
            }
        }
        else
        {
            txtDiscountP.Text = "";
            ScriptManager.RegisterStartupScript(this, typeof(string), "OPEN_WINDOW", "window.alert('First Get Amount');", true);
            System.Web.UI.ScriptManager.GetCurrent(this).SetFocus(txtAmount);
        }
    }
    protected void txtDiscountV_TextChanged(object sender, EventArgs e)
    {
        if (txtAmount.Text != "")
        {
            float flTemp = 0;
            if (float.TryParse(txtAmount.Text, out flTemp))
            {
                if (txtDiscountV.Text != "")
                {
                    if (float.TryParse(txtDiscountV.Text, out flTemp))
                    {
                        if (txtNetAmount.Text != "")
                        {
                            if (txtNetAmount.Text == "0")
                            {
                                txtDiscountP.Text = ((100 * float.Parse(txtDiscountV.Text)) / float.Parse(txtAmount.Text)).ToString();
                                txtGrandTotal.Text = (float.Parse(txtAmount.Text) - float.Parse(txtDiscountV.Text)).ToString();
                            }
                            else
                            {
                                txtDiscountP.Text = ((100 * float.Parse(txtDiscountV.Text)) / float.Parse(txtGrandTotal.Text)).ToString();
                                txtGrandTotal.Text = (float.Parse(txtNetAmount.Text) - float.Parse(txtDiscountV.Text)).ToString();
                            }
                            System.Web.UI.ScriptManager.GetCurrent(this).SetFocus(txtRemark);
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
                txtAmount.Text = "";
                ScriptManager.RegisterStartupScript(this, typeof(string), "OPEN_WINDOW", "window.alert('Get Numeric Value Only');", true);
                System.Web.UI.ScriptManager.GetCurrent(this).SetFocus(txtAmount);
            }
        }
        else
        {
            txtDiscountV.Text = "";
            ScriptManager.RegisterStartupScript(this, typeof(string), "OPEN_WINDOW", "window.alert('First Get Amount');", true);
            System.Web.UI.ScriptManager.GetCurrent(this).SetFocus(txtAmount);
        }
    }
    #endregion

    #region Grid Calculations
    protected void txtgvUnitPrice_TextChanged(object sender, EventArgs e)
    {
        float fGrossTotal = 0.00f;
        foreach (GridViewRow gvr in GvSalesOrderDetail.Rows)
        {
            Label lblgvUnitPrice = (Label)gvr.FindControl("lblgvUnitPrice");
            TextBox txtgvTaxP = (TextBox)gvr.FindControl("txtgvTaxP");
            TextBox txtgvTaxV = (TextBox)gvr.FindControl("txtgvTaxV");
            TextBox txtgvPriceAfterTax = (TextBox)gvr.FindControl("txtgvPriceAfterTax");
            TextBox txtgvDiscountP = (TextBox)gvr.FindControl("txtgvDiscountP");
            TextBox txtgvDiscountV = (TextBox)gvr.FindControl("txtgvDiscountV");
            TextBox txtgvPriceAfterDiscount = (TextBox)gvr.FindControl("txtgvPriceAfterDiscount");
            TextBox txtgvTotal = (TextBox)gvr.FindControl("txtgvTotal");

            if (lblgvUnitPrice.Text != "")
            {
                float flTemp = 0;
                if (float.TryParse(lblgvUnitPrice.Text, out flTemp))
                {
                    if (txtgvTaxP.Text != "")
                    {
                        txtgvTaxP_TextChanged(sender, e);
                    }
                    else
                    {
                        txtgvPriceAfterTax.Text = "0";
                    }
                    if (txtgvDiscountP.Text != "")
                    {
                        txtgvDiscountP_TextChanged(sender, e);
                    }
                    else
                    {
                        txtgvPriceAfterDiscount.Text = "0";
                        txtgvTotal.Text = (float.Parse(lblgvUnitPrice.Text) + float.Parse(txtgvPriceAfterTax.Text)).ToString();
                    }
                    System.Web.UI.ScriptManager.GetCurrent(this).SetFocus(txtTaxP);
                }
                else
                {
                    lblgvUnitPrice.Text = "";
                    ScriptManager.RegisterStartupScript(this, typeof(string), "OPEN_WINDOW", "window.alert('Enter Numeric Value Only');", true);
                    System.Web.UI.ScriptManager.GetCurrent(this).SetFocus(lblgvUnitPrice);
                }
            }

            if (txtgvTotal.Text != "")
            {
                fGrossTotal = fGrossTotal + float.Parse(txtgvTotal.Text);
            }
        }
        txtAmount.Text = fGrossTotal.ToString();
        txtAmount_TextChanged(sender, e);
    }
    protected void txtgvTaxP_TextChanged(object sender, EventArgs e)
    {
        float fGrossTotal = 0.00f;
        foreach (GridViewRow gvr in GvSalesOrderDetail.Rows)
        {
            Label lblgvUnitPrice = (Label)gvr.FindControl("lblgvUnitPrice");
            TextBox txtgvTaxP = (TextBox)gvr.FindControl("txtgvTaxP");
            TextBox txtgvTaxV = (TextBox)gvr.FindControl("txtgvTaxV");
            TextBox txtgvPriceAfterTax = (TextBox)gvr.FindControl("txtgvPriceAfterTax");
            TextBox txtgvDiscountP = (TextBox)gvr.FindControl("txtgvDiscountP");
            TextBox txtgvDiscountV = (TextBox)gvr.FindControl("txtgvDiscountV");
            TextBox txtgvPriceAfterDiscount = (TextBox)gvr.FindControl("txtgvPriceAfterDiscount");
            TextBox txtgvTotal = (TextBox)gvr.FindControl("txtgvTotal");

            if (lblgvUnitPrice.Text != "")
            {
                float flTemp = 0;
                if (float.TryParse(lblgvUnitPrice.Text, out flTemp))
                {
                    if (txtgvTaxP.Text != "")
                    {
                        if (float.TryParse(txtgvTaxP.Text, out flTemp))
                        {
                            txtgvTaxV.Text = ((float.Parse(lblgvUnitPrice.Text) * float.Parse(txtgvTaxP.Text)) / 100).ToString();
                            if (lblgvUnitPrice.Text != "")
                            {
                                txtgvPriceAfterTax.Text = (float.Parse(lblgvUnitPrice.Text) + float.Parse(txtgvTaxV.Text)).ToString();
                            }
                            if (txtgvDiscountP.Text != "")
                            {
                                txtgvDiscountP_TextChanged(sender, e);
                            }
                            else
                            {
                                txtgvTotal.Text = txtgvPriceAfterTax.Text;
                            }
                            System.Web.UI.ScriptManager.GetCurrent(this).SetFocus(txtgvDiscountP);
                        }
                        else
                        {
                            txtgvTaxP.Text = "";
                            ScriptManager.RegisterStartupScript(this, typeof(string), "OPEN_WINDOW", "window.alert('Enter Numeric Value Only');", true);
                            System.Web.UI.ScriptManager.GetCurrent(this).SetFocus(txtgvTaxP);
                        }
                    }
                }
                else
                {
                    lblgvUnitPrice.Text = "";
                    ScriptManager.RegisterStartupScript(this, typeof(string), "OPEN_WINDOW", "window.alert('Enter Numeric Value Only');", true);
                    System.Web.UI.ScriptManager.GetCurrent(this).SetFocus(lblgvUnitPrice);
                }
            }
            else
            {
                txtgvTaxP.Text = "";
            }
            if (txtgvTotal.Text != "")
            {
                fGrossTotal = fGrossTotal + float.Parse(txtgvTotal.Text);
            }
        }
        txtAmount.Text = fGrossTotal.ToString();
        txtAmount_TextChanged(sender, e);
    }
    protected void txtgvTaxV_TextChanged(object sender, EventArgs e)
    {
        float fGrossTotal = 0.00f;
        foreach (GridViewRow gvr in GvSalesOrderDetail.Rows)
        {
            Label lblgvUnitPrice = (Label)gvr.FindControl("lblgvUnitPrice");
            TextBox txtgvTaxP = (TextBox)gvr.FindControl("txtgvTaxP");
            TextBox txtgvTaxV = (TextBox)gvr.FindControl("txtgvTaxV");
            TextBox txtgvPriceAfterTax = (TextBox)gvr.FindControl("txtgvPriceAfterTax");
            TextBox txtgvDiscountP = (TextBox)gvr.FindControl("txtgvDiscountP");
            TextBox txtgvDiscountV = (TextBox)gvr.FindControl("txtgvDiscountV");
            TextBox txtgvPriceAfterDiscount = (TextBox)gvr.FindControl("txtgvPriceAfterDiscount");
            TextBox txtgvTotal = (TextBox)gvr.FindControl("txtgvTotal");

            if (lblgvUnitPrice.Text != "")
            {
                float flTemp = 0;
                if (float.TryParse(lblgvUnitPrice.Text, out flTemp))
                {
                    if (txtgvTaxV.Text != "")
                    {
                        if (float.TryParse(txtgvTaxV.Text, out flTemp))
                        {
                            txtgvTaxP.Text = ((100 * float.Parse(txtgvTaxV.Text)) / float.Parse(lblgvUnitPrice.Text)).ToString();
                            if (lblgvUnitPrice.Text != "")
                            {
                                txtgvPriceAfterTax.Text = (float.Parse(lblgvUnitPrice.Text) + float.Parse(txtgvTaxV.Text)).ToString();
                                txtgvTotal.Text = (float.Parse(lblgvUnitPrice.Text) + float.Parse(txtgvPriceAfterTax.Text)).ToString();
                            }
                            if (txtgvDiscountP.Text != "")
                            {
                                txtgvDiscountP_TextChanged(sender, e);
                            }
                            System.Web.UI.ScriptManager.GetCurrent(this).SetFocus(txtgvDiscountP);
                        }
                        else
                        {
                            txtgvTaxV.Text = "";
                            ScriptManager.RegisterStartupScript(this, typeof(string), "OPEN_WINDOW", "window.alert('Enter Numeric Value Only');", true);
                            System.Web.UI.ScriptManager.GetCurrent(this).SetFocus(txtgvTaxV);
                        }
                    }
                }
                else
                {
                    lblgvUnitPrice.Text = "";
                    ScriptManager.RegisterStartupScript(this, typeof(string), "OPEN_WINDOW", "window.alert('Enter Numeric Value Only');", true);
                    System.Web.UI.ScriptManager.GetCurrent(this).SetFocus(lblgvUnitPrice);
                }
            }
            else
            {
                txtgvTaxV.Text = "";
            }
            if (txtgvTotal.Text != "")
            {
                fGrossTotal = fGrossTotal + float.Parse(txtgvTotal.Text);
            }
        }
        txtAmount.Text = fGrossTotal.ToString();
        txtAmount_TextChanged(sender, e);
    }
    protected void txtgvDiscountP_TextChanged(object sender, EventArgs e)
    {
        float fGrossTotal = 0.00f;
        foreach (GridViewRow gvr in GvSalesOrderDetail.Rows)
        {
            Label lblgvUnitPrice = (Label)gvr.FindControl("lblgvUnitPrice");
            TextBox txtgvTaxP = (TextBox)gvr.FindControl("txtgvTaxP");
            TextBox txtgvTaxV = (TextBox)gvr.FindControl("txtgvTaxV");
            TextBox txtgvPriceAfterTax = (TextBox)gvr.FindControl("txtgvPriceAfterTax");
            TextBox txtgvDiscountP = (TextBox)gvr.FindControl("txtgvDiscountP");
            TextBox txtgvDiscountV = (TextBox)gvr.FindControl("txtgvDiscountV");
            TextBox txtgvPriceAfterDiscount = (TextBox)gvr.FindControl("txtgvPriceAfterDiscount");
            TextBox txtgvTotal = (TextBox)gvr.FindControl("txtgvTotal");

            if (lblgvUnitPrice.Text != "")
            {
                float flTemp = 0;
                if (float.TryParse(lblgvUnitPrice.Text, out flTemp))
                {
                    if (txtgvDiscountP.Text != "")
                    {
                        if (float.TryParse(txtgvDiscountP.Text, out flTemp))
                        {
                            if (txtgvPriceAfterTax.Text != "")
                            {
                                if (txtgvPriceAfterTax.Text == "0")
                                {
                                    txtgvDiscountV.Text = ((float.Parse(lblgvUnitPrice.Text) * float.Parse(txtgvDiscountP.Text)) / 100).ToString();
                                    txtgvPriceAfterDiscount.Text = (float.Parse(lblgvUnitPrice.Text) - float.Parse(txtgvDiscountV.Text)).ToString();
                                    txtgvTotal.Text = (float.Parse(lblgvUnitPrice.Text) - float.Parse(txtgvDiscountV.Text)).ToString();
                                }
                                else
                                {
                                    txtgvDiscountV.Text = ((float.Parse(txtgvPriceAfterTax.Text) * float.Parse(txtgvDiscountP.Text)) / 100).ToString();
                                    txtgvPriceAfterDiscount.Text = (float.Parse(txtgvPriceAfterTax.Text) - float.Parse(txtgvDiscountV.Text)).ToString();
                                    txtgvTotal.Text = (float.Parse(txtgvPriceAfterTax.Text) - float.Parse(txtgvDiscountV.Text)).ToString();
                                }
                                System.Web.UI.ScriptManager.GetCurrent(this).SetFocus(txtTaxP);
                            }
                        }
                        else
                        {
                            txtgvDiscountP.Text = "";
                            ScriptManager.RegisterStartupScript(this, typeof(string), "OPEN_WINDOW", "window.alert('Enter Numeric Value Only');", true);
                            System.Web.UI.ScriptManager.GetCurrent(this).SetFocus(txtgvDiscountP);
                        }
                    }
                }
                else
                {
                    lblgvUnitPrice.Text = "";
                    ScriptManager.RegisterStartupScript(this, typeof(string), "OPEN_WINDOW", "window.alert('Enter Numeric Value Only');", true);
                    System.Web.UI.ScriptManager.GetCurrent(this).SetFocus(lblgvUnitPrice);
                }
            }
            else
            {
                txtgvDiscountP.Text = "";
            }
            if (txtgvTotal.Text != "")
            {
                fGrossTotal = fGrossTotal + float.Parse(txtgvTotal.Text);
            }
        }
        txtAmount.Text = fGrossTotal.ToString();
        txtAmount_TextChanged(sender, e);
    }
    protected void txtgvDiscountV_TextChanged(object sender, EventArgs e)
    {
        float fGrossTotal = 0.00f;
        foreach (GridViewRow gvr in GvSalesOrderDetail.Rows)
        {
            Label lblgvUnitPrice = (Label)gvr.FindControl("lblgvUnitPrice");
            TextBox txtgvTaxP = (TextBox)gvr.FindControl("txtgvTaxP");
            TextBox txtgvTaxV = (TextBox)gvr.FindControl("txtgvTaxV");
            TextBox txtgvPriceAfterTax = (TextBox)gvr.FindControl("txtgvPriceAfterTax");
            TextBox txtgvDiscountP = (TextBox)gvr.FindControl("txtgvDiscountP");
            TextBox txtgvDiscountV = (TextBox)gvr.FindControl("txtgvDiscountV");
            TextBox txtgvPriceAfterDiscount = (TextBox)gvr.FindControl("txtgvPriceAfterDiscount");
            TextBox txtgvTotal = (TextBox)gvr.FindControl("txtgvTotal");

            if (lblgvUnitPrice.Text != "")
            {
                float flTemp = 0;
                if (float.TryParse(lblgvUnitPrice.Text, out flTemp))
                {
                    if (txtgvDiscountV.Text != "")
                    {
                        if (float.TryParse(txtgvDiscountV.Text, out flTemp))
                        {
                            if (txtgvPriceAfterTax.Text != "")
                            {
                                if (txtgvPriceAfterTax.Text == "0")
                                {
                                    txtgvDiscountP.Text = ((100 * float.Parse(txtgvDiscountV.Text)) / float.Parse(lblgvUnitPrice.Text)).ToString();
                                    txtgvPriceAfterDiscount.Text = (float.Parse(lblgvUnitPrice.Text) - float.Parse(txtgvDiscountV.Text)).ToString();
                                    txtgvTotal.Text = (float.Parse(lblgvUnitPrice.Text) - float.Parse(txtgvDiscountV.Text)).ToString();
                                }
                                else
                                {
                                    txtgvDiscountP.Text = ((100 * float.Parse(txtgvDiscountV.Text)) / float.Parse(txtgvPriceAfterTax.Text)).ToString();
                                    txtgvPriceAfterDiscount.Text = (float.Parse(txtgvPriceAfterTax.Text) - float.Parse(txtgvDiscountV.Text)).ToString();
                                    txtgvTotal.Text = (float.Parse(txtgvPriceAfterTax.Text) - float.Parse(txtgvDiscountV.Text)).ToString();
                                }
                                System.Web.UI.ScriptManager.GetCurrent(this).SetFocus(txtTaxP);
                            }
                        }
                        else
                        {
                            txtgvTaxV.Text = "";
                            ScriptManager.RegisterStartupScript(this, typeof(string), "OPEN_WINDOW", "window.alert('Enter Numeric Value Only');", true);
                            System.Web.UI.ScriptManager.GetCurrent(this).SetFocus(txtgvTaxV);
                        }
                    }
                }
                else
                {
                    lblgvUnitPrice.Text = "";
                    ScriptManager.RegisterStartupScript(this, typeof(string), "OPEN_WINDOW", "window.alert('Enter Numeric Value Only');", true);
                    System.Web.UI.ScriptManager.GetCurrent(this).SetFocus(lblgvUnitPrice);
                }
            }
            else
            {
                txtgvDiscountV.Text = "";
            }
            if (txtgvTotal.Text != "")
            {
                fGrossTotal = fGrossTotal + float.Parse(txtgvTotal.Text);
            }
        }
        txtAmount.Text = fGrossTotal.ToString();
        txtAmount_TextChanged(sender, e);
    }
    protected void txtAmount_TextChanged(object sender, EventArgs e)
    {
        if (txtAmount.Text != "")
        {
            float flTemp = 0;
            if (float.TryParse(txtAmount.Text, out flTemp))
            {
                if (txtTaxP.Text != "")
                {
                    txtTaxP_TextChanged(sender, e);
                }
                else
                {
                    txtNetAmount.Text = "0";
                }
                if (txtDiscountP.Text != "")
                {
                    txtDiscountP_TextChanged(sender, e);
                }
                else
                {
                    txtGrandTotal.Text = (float.Parse(txtAmount.Text) + float.Parse(txtNetAmount.Text)).ToString();
                }
                System.Web.UI.ScriptManager.GetCurrent(this).SetFocus(txtTaxP);
            }
        }
    }
    #endregion

    private string GetCustomerId()
    {
        string retval = string.Empty;
        if (txtCustomer.Text != "")
        {
            DataTable dtSupp = objContact.GetContactByContactName(StrCompId.ToString(), txtCustomer.Text.Trim().Split('/')[0].ToString());
            if (dtSupp.Rows.Count > 0)
            {
                retval = (txtCustomer.Text.Split('/'))[txtCustomer.Text.Split('/').Length - 1];

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
    protected void ddlOrderType_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (ddlOrderType.SelectedValue == "--Select--")
        {
            trTransfer.Visible = false;
            txtTransFrom.Text = "";
            GvSalesOrderDetail.DataSource = null;
            GvSalesOrderDetail.DataBind();
            GvProductDetail.DataSource = null;
            GvProductDetail.DataBind();
            txtAmount.Text = "";
            txtTaxP.Text = "";
            txtTaxV.Text = "";
            txtNetAmount.Text = "";
            txtDiscountP.Text = "";
            txtDiscountV.Text = "";
            txtGrandTotal.Text = "";
            btnAddNewProduct.Visible = false;
        }
        else if (ddlOrderType.SelectedValue == "D")
        {
            trTransfer.Visible = false;
            txtTransFrom.Text = "";
            GvSalesOrderDetail.DataSource = null;
            GvSalesOrderDetail.DataBind();
            GvProductDetail.DataSource = null;
            GvProductDetail.DataBind();
            txtAmount.Text = "";
            txtTaxP.Text = "";
            txtTaxV.Text = "";
            txtNetAmount.Text = "";
            txtDiscountP.Text = "";
            txtDiscountV.Text = "";
            txtGrandTotal.Text = "";
            btnAddNewProduct.Visible = true;
        }
        else if (ddlOrderType.SelectedValue == "Q")
        {
            trTransfer.Visible = true;
            txtTransFrom.Text = "Sales Order";
            ddlSalesOrderNo.Visible = true;
            txtSalesOrderNo.Visible = false;
            GvProductDetail.DataSource = null;
            GvProductDetail.DataBind();
            GvSalesOrderDetail.DataSource = null;
            GvSalesOrderDetail.DataBind();
            FillSalesOrderNo();
            btnAddNewProduct.Visible = false;
        }
    }
    protected void ddlSalesOrderNo_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (ddlSalesOrderNo.SelectedValue != "--Select--")
        {
            DataTable dtPSalesOrder = objSOrderHeader.GetSOHeaderAllByTransId(StrCompId, StrBrandId, StrLocationId, ddlSalesOrderNo.SelectedValue);

            if (dtPSalesOrder.Rows.Count > 0)
            {
                hdnSalesOrderId.Value = dtPSalesOrder.Rows[0]["Trans_Id"].ToString();

                //string strInquiryNo = dtPSalesOrder.Rows[0]["SInquiry_No"].ToString();
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
                DataTable dtSODetail = ObjSOrderDetail.GetSODetailBySOrderNo(StrCompId, StrBrandId, StrLocationId, hdnSalesOrderId.Value);
                if (dtSODetail.Rows.Count > 0)
                {
                    GvSalesOrderDetail.DataSource = dtSODetail;
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
                            for (int i = 0; i < dtSODetail.Rows.Count; i++)
                            {
                                string strProductId = dtSODetail.Rows[i]["Product_Id"].ToString();
                                if (strProductId == hdngvProductId.Value)
                                {
                                    lblgvUnitPrice.Text = dtSODetail.Rows[i]["UnitPrice"].ToString();
                                    if (lblgvUnitPrice.Text != "0.000" && lblgvUnitPrice.Text != "")
                                    {
                                        //txtgvUnitPrice_TextChanged(sender, e);
                                    }
                                    txtgvTaxP.Text = dtSODetail.Rows[i]["TaxP"].ToString();
                                    if (txtgvTaxP.Text != "0.000" && txtgvTaxP.Text != "")
                                    {
                                        txtgvTaxP_TextChanged(sender, e);
                                    }
                                    txtgvTaxV.Text = dtSODetail.Rows[i]["TaxV"].ToString();
                                    if (txtgvTaxV.Text != "0.000" && txtgvTaxV.Text != "")
                                    {
                                        txtgvTaxV_TextChanged(sender, e);
                                    }

                                    //txtgvPriceAfterTax.Text = dtSODetail.Rows[i]["PriceAfterTax"].ToString();
                                    txtgvDiscountP.Text = dtSODetail.Rows[i]["DiscountP"].ToString();
                                    if (txtgvDiscountP.Text != "0.000" && txtgvDiscountP.Text != "")
                                    {
                                        txtgvDiscountP_TextChanged(sender, e);
                                    }

                                    txtgvDiscountV.Text = dtSODetail.Rows[i]["DiscountV"].ToString();
                                    if (txtgvDiscountV.Text != "0.000" && txtgvDiscountV.Text != "")
                                    {
                                        txtgvDiscountV_TextChanged(sender, e);
                                    }

                                    //txtgvPriceAfterDiscount.Text = dtSODetail.Rows[i]["PriceAfterDiscount"].ToString();

                                    if (lblgvQuantity.Text != "")
                                    {
                                        fGrossTotal = fGrossTotal + float.Parse(lblgvQuantity.Text);
                                    }
                                }
                            }
                        }
                    }
                    txtTotalQuantity.Text = fGrossTotal.ToString();
                }

                txtTaxP.Text = dtPSalesOrder.Rows[0]["TaxP"].ToString();
                if (txtTaxP.Text != "0.000" && txtTaxP.Text != "")
                {
                    txtTaxP_TextChanged(sender, e);
                }
                txtTaxV.Text = dtPSalesOrder.Rows[0]["TaxV"].ToString();
                if (txtTaxV.Text != "0.000" && txtTaxV.Text != "")
                {
                    txtTaxV_TextChanged(sender, e);
                }
                txtDiscountP.Text = dtPSalesOrder.Rows[0]["DiscountP"].ToString();
                if (txtDiscountP.Text != "0.000" && txtDiscountP.Text != "")
                {
                    txtDiscountP_TextChanged(sender, e);
                }
                txtDiscountV.Text = dtPSalesOrder.Rows[0]["DiscountV"].ToString();
                if (txtDiscountV.Text != "0.000" && txtDiscountV.Text != "")
                {
                    txtDiscountV_TextChanged(sender, e);
                }
            }
            btnNew_Click(null, null);
            System.Web.UI.ScriptManager.GetCurrent(this).SetFocus(txtSInvNo);
        }
        else
        {
            GvSalesOrderDetail.DataSource = null;
            GvSalesOrderDetail.DataBind();
            txtAmount.Text = "";
            txtTaxP.Text = "";
            txtTaxV.Text = "";
            txtNetAmount.Text = "";
            txtDiscountP.Text = "";
            txtDiscountV.Text = "";
            txtGrandTotal.Text = "";
        }
    }
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
    protected string GetUnitName(string strUnitId)
    {
        string strUnitName = string.Empty;
        if (strUnitId != "0" && strUnitId != "")
        {
            DataTable dtUName = UM.GetUnitMasterById(StrCompId, strUnitId);
            if (dtUName.Rows.Count > 0)
            {
                strUnitName = dtUName.Rows[0]["Unit_Code"].ToString();
            }
        }
        else
        {
            strUnitName = "";
        }
        return strUnitName;
    }
    protected string GetCurrencyName(string strCurrencyId)
    {
        string strCurrencyName = string.Empty;
        if (strCurrencyId != "0" && strCurrencyId != "")
        {
            DataTable dtCName = objCurrency.GetCurrencyMasterById(strCurrencyId);
            if (dtCName.Rows.Count > 0)
            {
                strCurrencyName = dtCName.Rows[0]["Currency_Code"].ToString();
            }
        }
        else
        {
            strCurrencyName = "";
        }
        return strCurrencyName;
    }
    protected void GvProductDetail_RowCreated(object sender, GridViewRowEventArgs e)
    {
        GridView gvProduct = (GridView)sender;
        if (e.Row.RowType == DataControlRowType.Header)
        {
            GridView HeaderGrid = (GridView)sender;
            GridViewRow row = new GridViewRow(0, 0, DataControlRowType.Header, DataControlRowState.Insert);

            TableHeaderCell cell = new TableHeaderCell();
            cell.Text = Resources.Attendance.Edit;
            cell.ColumnSpan = 1;
            row.Controls.Add(cell);

            cell = new TableHeaderCell();
            cell.ColumnSpan = 1;
            cell.Text = Resources.Attendance.Delete;
            row.Controls.Add(cell);

            cell = new TableHeaderCell();
            cell.ColumnSpan = 1;
            cell.Text = Resources.Attendance.S_No_;
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
            cell.ColumnSpan = 1;
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
    private void FillProductUnit()
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
        ResetProduct();
    }
    protected void txtProductName_TextChanged(object sender, EventArgs e)
    {
        if (txtProductName.Text != "")
        {
            DataTable dt = objProductM.GetProductMasterTrueAll(StrCompId);
            dt = new DataView(dt, "EProductName ='" + txtProductName.Text + "'", "", DataViewRowState.CurrentRows).ToTable();
            if (dt.Rows.Count > 0)
            {
                string strUnitId = dt.Rows[0]["UnitId"].ToString();
                if (strUnitId != "0" && strUnitId != "")
                {
                    ddlUnit.SelectedValue = strUnitId;
                }
                else
                {
                    FillProductUnit();
                }
                txtPDescription.Text = dt.Rows[0]["Description"].ToString();
                hdnNewProductId.Value = dt.Rows[0]["ProductId"].ToString();
                System.Web.UI.ScriptManager.GetCurrent(this).SetFocus(txtPUnitPrice);
            }
            else
            {
                FillProductUnit();
                txtPDescription.Text = "";
                hdnNewProductId.Value = "0";
            }
            txtPUnitPrice.Text = "0";
            txtPUnitCost.Text = "0";
        }
        else
        {
            DisplayMessage("Enter Product Name");
            System.Web.UI.ScriptManager.GetCurrent(this).SetFocus(txtProductName);
        }
    }
    protected void btnProductSave_Click(object sender, EventArgs e)
    {
        if (txtProductName.Text != "")
        {
            string strA = "0";
            foreach (GridViewRow gve in GvProductDetail.Rows)
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
                        hdnNewProductId.Value = "0";
                    }
                }
            }

            if (ddlUnit.SelectedValue != "--Select--")
            {
                hdnUnitId.Value = ddlUnit.SelectedValue;
            }
            else
            {
                DisplayMessage("Select Unit Name");
                System.Web.UI.ScriptManager.GetCurrent(this).SetFocus(ddlUnit);
                return;
            }
            if (txtPUnitPrice.Text == "")
            {
                txtPUnitPrice.Text = "0";
            }
            if (txtPUnitCost.Text == "")
            {
                txtPUnitCost.Text = "0";
            }

            if (txtPQuantity.Text != "")
            {

            }
            else
            {
                DisplayMessage("Enter Required Quantity");
                System.Web.UI.ScriptManager.GetCurrent(this).SetFocus(txtPQuantity);
                return;
            }

            if (txtPQuantity.Text != "")
            {
                float flTemp = 0;
                if (float.TryParse(txtPQuantity.Text, out flTemp))
                {

                }
                else
                {
                    txtPQuantity.Text = "";
                    ScriptManager.RegisterStartupScript(this, typeof(string), "OPEN_WINDOW", "window.alert('Enter Numeric Value Only');", true);
                    System.Web.UI.ScriptManager.GetCurrent(this).SetFocus(txtPQuantity);
                    return;
                }
            }
            else
            {
                txtPQuantity.Text = "0";
            }


            if (hdnProductId.Value == "")
            {
                if (strA == "0")
                {
                    FillProductChidGird("Save");
                    System.Web.UI.ScriptManager.GetCurrent(this).SetFocus(btnAddNewProduct);
                    pnlProduct1.Visible = false;
                    pnlProduct2.Visible = false;
                    GetGridTotal();
                }
                else
                {
                    txtProductName.Text = "";
                    DisplayMessage("Product Name Already Exists");
                    System.Web.UI.ScriptManager.GetCurrent(this).SetFocus(txtProductName);
                }
            }
            else
            {
                if (txtProductName.Text == hdnProductName.Value)
                {
                    FillProductChidGird("Edit");
                    System.Web.UI.ScriptManager.GetCurrent(this).SetFocus(btnAddNewProduct);
                    pnlProduct1.Visible = false;
                    pnlProduct2.Visible = false;
                    GetGridTotal();
                }
                else
                {
                    if (strA == "0")
                    {
                        FillProductChidGird("Edit");
                        System.Web.UI.ScriptManager.GetCurrent(this).SetFocus(btnAddNewProduct);
                        pnlProduct1.Visible = false;
                        pnlProduct2.Visible = false;
                        GetGridTotal();
                    }
                    else
                    {
                        txtProductName.Text = "";
                        DisplayMessage("Product Name Already Exists");
                        System.Web.UI.ScriptManager.GetCurrent(this).SetFocus(txtProductName);
                    }
                }
            }
        }
        else
        {
            DisplayMessage("Enter Product Name");
            System.Web.UI.ScriptManager.GetCurrent(this).SetFocus(txtProductName);
        }
        txtProductName.Focus();
    }
    protected void GetGridTotal()
    {
        float fGrossTotal = 0.00f;
        float fTotalQuantity = 0.00f;
        foreach (GridViewRow gvr in GvProductDetail.Rows)
        {
            Label lblgvTotal = (Label)gvr.FindControl("lblgvTotal");
            Label lblgvQuantity = (Label)gvr.FindControl("lblgvQuantity");

            if (lblgvTotal.Text != "")
            {
                fGrossTotal = fGrossTotal + float.Parse(lblgvTotal.Text);
            }

            if (lblgvQuantity.Text != "")
            {
                fTotalQuantity = fTotalQuantity + float.Parse(lblgvQuantity.Text);
            }
        }
        txtAmount.Text = fGrossTotal.ToString();
        txtTotalQuantity.Text = fTotalQuantity.ToString();
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
        FillProductUnit();
        txtPUnitPrice.Text = "";
        txtPUnitCost.Text = "";
        txtPDescription.Text = "";
        txtPQuantity.Text = "1";
        txtPQuantityPrice.Text = "";
        txtPTaxP.Text = "";
        txtPTaxV.Text = "";
        txtPPriceAfterTax.Text = "";
        txtPDiscountP.Text = "";
        txtPDiscountV.Text = "";
        txtPTotalAmount.Text = "";

        hdnProductId.Value = "";
        hdnProductName.Value = "";
        hdnNewProductId.Value = "0";
    }
    public DataTable CreateProductDataTable()
    {
        DataTable dt = new DataTable();
        dt.Columns.Add("Serial_No");
        dt.Columns.Add("Product_Id");
        dt.Columns.Add("ProductDescription");
        dt.Columns.Add("Unit_Id");
        dt.Columns.Add("UnitPrice");
        dt.Columns.Add("UnitCost");
        dt.Columns.Add("Quantity");
        dt.Columns.Add("TaxP");
        dt.Columns.Add("TaxV");
        dt.Columns.Add("DiscountP");
        dt.Columns.Add("DiscountV");

        return dt;
    }
    public DataTable FillProductDataTabel()
    {
        string strNewSNo = string.Empty;
        DataTable dt = CreateProductDataTable();
        if (GvProductDetail.Rows.Count > 0)
        {
            for (int i = 0; i < GvProductDetail.Rows.Count + 1; i++)
            {
                if (dt.Rows.Count != GvProductDetail.Rows.Count)
                {
                    dt.Rows.Add(i);
                    Label lblgvSNo = (Label)GvProductDetail.Rows[i].FindControl("lblgvSerialNo");
                    HiddenField hdngvProductId = (HiddenField)GvProductDetail.Rows[i].FindControl("hdngvProductId");
                    Label lblgvProductDescription = (Label)GvProductDetail.Rows[i].FindControl("lblgvProductDescription");
                    HiddenField hdngvUnitId = (HiddenField)GvProductDetail.Rows[i].FindControl("hdngvUnitId");
                    Label lblgvUnitPrice = (Label)GvProductDetail.Rows[i].FindControl("lblgvUnitPrice");
                    Label lblgvUnitCost = (Label)GvProductDetail.Rows[i].FindControl("lblgvUnitCost");
                    Label lblgvQuantity = (Label)GvProductDetail.Rows[i].FindControl("lblgvQuantity");
                    Label lblgvTaxP = (Label)GvProductDetail.Rows[i].FindControl("lblgvTaxP");
                    Label lblgvTaxV = (Label)GvProductDetail.Rows[i].FindControl("lblgvTaxV");
                    Label lblgvDiscountP = (Label)GvProductDetail.Rows[i].FindControl("lblgvDiscountP");
                    Label lblgvDiscountV = (Label)GvProductDetail.Rows[i].FindControl("lblgvDiscountV");

                    dt.Rows[i]["Serial_No"] = lblgvSNo.Text;
                    strNewSNo = lblgvSNo.Text;
                    dt.Rows[i]["Product_Id"] = hdngvProductId.Value;
                    dt.Rows[i]["ProductDescription"] = lblgvProductDescription.Text;
                    dt.Rows[i]["Unit_Id"] = hdngvUnitId.Value;
                    dt.Rows[i]["UnitPrice"] = lblgvUnitPrice.Text;
                    dt.Rows[i]["UnitCost"] = lblgvUnitCost.Text;
                    dt.Rows[i]["Quantity"] = lblgvQuantity.Text;
                    dt.Rows[i]["TaxP"] = lblgvTaxP.Text;
                    dt.Rows[i]["TaxV"] = lblgvTaxV.Text;
                    dt.Rows[i]["DiscountP"] = lblgvDiscountP.Text;
                    dt.Rows[i]["DiscountV"] = lblgvDiscountV.Text;
                }
                else
                {
                    dt.Rows.Add(i);
                    dt.Rows[i]["Serial_No"] = (float.Parse(strNewSNo) + 1).ToString();
                    dt.Rows[i]["Product_Id"] = hdnNewProductId.Value;
                    dt.Rows[i]["ProductDescription"] = txtPDescription.Text;
                    dt.Rows[i]["Unit_Id"] = ddlUnit.SelectedValue;
                    dt.Rows[i]["UnitPrice"] = txtPUnitPrice.Text;
                    dt.Rows[i]["UnitCost"] = txtPUnitCost.Text;
                    dt.Rows[i]["Quantity"] = txtPQuantity.Text;
                    dt.Rows[i]["TaxP"] = txtPTaxP.Text;
                    dt.Rows[i]["TaxV"] = txtPTaxV.Text;
                    dt.Rows[i]["DiscountP"] = txtPDiscountP.Text;
                    dt.Rows[i]["DiscountV"] = txtPDiscountV.Text;
                }
            }
        }
        else
        {

            dt.Rows.Add(0);
            dt.Rows[0]["Serial_No"] = "1";
            dt.Rows[0]["Product_Id"] = hdnNewProductId.Value;
            dt.Rows[0]["ProductDescription"] = txtPDescription.Text;
            dt.Rows[0]["Unit_Id"] = ddlUnit.SelectedValue;
            dt.Rows[0]["UnitPrice"] = txtPUnitPrice.Text;
            dt.Rows[0]["UnitCost"] = txtPUnitCost.Text;
            dt.Rows[0]["Quantity"] = txtPQuantity.Text;
            dt.Rows[0]["TaxP"] = txtPTaxP.Text;
            dt.Rows[0]["TaxV"] = txtPTaxV.Text;
            dt.Rows[0]["DiscountP"] = txtPDiscountP.Text;
            dt.Rows[0]["DiscountV"] = txtPDiscountV.Text;
        }
        return dt;
    }
    public DataTable FillProductDataTabelDelete()
    {
        DataTable dt = CreateProductDataTable();
        for (int i = 0; i < GvProductDetail.Rows.Count; i++)
        {
            dt.Rows.Add(i);
            Label lblgvSNo = (Label)GvProductDetail.Rows[i].FindControl("lblgvSerialNo");
            HiddenField hdngvProductId = (HiddenField)GvProductDetail.Rows[i].FindControl("hdngvProductId");
            Label lblgvProductDescription = (Label)GvProductDetail.Rows[i].FindControl("lblgvProductDescription");
            HiddenField hdngvUnitId = (HiddenField)GvProductDetail.Rows[i].FindControl("hdngvUnitId");
            Label lblgvUnitPrice = (Label)GvProductDetail.Rows[i].FindControl("lblgvUnitPrice");
            Label lblgvUnitCost = (Label)GvProductDetail.Rows[i].FindControl("lblgvUnitCost");
            Label lblgvQuantity = (Label)GvProductDetail.Rows[i].FindControl("lblgvQuantity");
            Label lblgvTaxP = (Label)GvProductDetail.Rows[i].FindControl("lblgvTaxP");
            Label lblgvTaxV = (Label)GvProductDetail.Rows[i].FindControl("lblgvTaxV");
            Label lblgvDiscountP = (Label)GvProductDetail.Rows[i].FindControl("lblgvDiscountP");
            Label lblgvDiscountV = (Label)GvProductDetail.Rows[i].FindControl("lblgvDiscountV");

            dt.Rows[i]["Serial_No"] = lblgvSNo.Text;
            dt.Rows[i]["Product_Id"] = hdngvProductId.Value;
            dt.Rows[i]["ProductDescription"] = txtPDescription.Text;
            dt.Rows[i]["Unit_Id"] = hdngvUnitId.Value;
            dt.Rows[i]["UnitPrice"] = txtPUnitPrice.Text;
            dt.Rows[i]["UnitCost"] = txtPUnitCost.Text;
            dt.Rows[i]["Quantity"] = txtPQuantity.Text;
            dt.Rows[i]["TaxP"] = txtPTaxP.Text;
            dt.Rows[i]["TaxV"] = txtPTaxV.Text;
            dt.Rows[i]["DiscountP"] = txtPDiscountP.Text;
            dt.Rows[i]["DiscountV"] = txtPDiscountV.Text;
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

        for (int i = 0; i < GvProductDetail.Rows.Count; i++)
        {
            dt.Rows.Add(i);
            Label lblgvSNo = (Label)GvProductDetail.Rows[i].FindControl("lblgvSerialNo");
            HiddenField hdngvProductId = (HiddenField)GvProductDetail.Rows[i].FindControl("hdngvProductId");
            Label lblgvProductDescription = (Label)GvProductDetail.Rows[i].FindControl("lblgvProductDescription");
            HiddenField hdngvUnitId = (HiddenField)GvProductDetail.Rows[i].FindControl("hdngvUnitId");
            Label lblgvUnitPrice = (Label)GvProductDetail.Rows[i].FindControl("lblgvUnitPrice");
            Label lblgvUnitCost = (Label)GvProductDetail.Rows[i].FindControl("lblgvUnitCost");
            Label lblgvQuantity = (Label)GvProductDetail.Rows[i].FindControl("lblgvQuantity");
            Label lblgvTaxP = (Label)GvProductDetail.Rows[i].FindControl("lblgvTaxP");
            Label lblgvTaxV = (Label)GvProductDetail.Rows[i].FindControl("lblgvTaxV");
            Label lblgvDiscountP = (Label)GvProductDetail.Rows[i].FindControl("lblgvDiscountP");
            Label lblgvDiscountV = (Label)GvProductDetail.Rows[i].FindControl("lblgvDiscountV");

            dt.Rows[i]["Serial_No"] = lblgvSNo.Text;
            dt.Rows[i]["Product_Id"] = hdngvProductId.Value;
            dt.Rows[i]["ProductDescription"] = lblgvProductDescription.Text;
            dt.Rows[i]["Unit_Id"] = hdngvUnitId.Value;
            dt.Rows[i]["UnitPrice"] = lblgvUnitPrice.Text;
            dt.Rows[i]["UnitCost"] = lblgvUnitCost.Text;
            dt.Rows[i]["Quantity"] = lblgvQuantity.Text;
            dt.Rows[i]["TaxP"] = lblgvTaxP.Text;
            dt.Rows[i]["TaxV"] = lblgvTaxV.Text;
            dt.Rows[i]["DiscountP"] = lblgvDiscountP.Text;
            dt.Rows[i]["DiscountV"] = lblgvDiscountV.Text;
        }

        DataView dv = new DataView(dt);
        dv.RowFilter = "Serial_No='" + hdnProductId.Value + "'";
        dt = (DataTable)dv.ToTable();
        if (dt.Rows.Count != 0)
        {
            txtProductName.Text = GetProductName(dt.Rows[0]["Product_Id"].ToString());
            FillProductUnit();
            txtProductName_TextChanged(null, null);
            ddlUnit.SelectedValue = dt.Rows[0]["Unit_Id"].ToString();
            txtPDescription.Text = dt.Rows[0]["ProductDescription"].ToString();
            txtPUnitPrice.Text = dt.Rows[0]["UnitPrice"].ToString();
            txtPUnitCost.Text = dt.Rows[0]["UnitCost"].ToString();
            txtPQuantity.Text = dt.Rows[0]["Quantity"].ToString();
            txtPTaxP.Text = dt.Rows[0]["TaxP"].ToString();
            txtPTaxV.Text = dt.Rows[0]["TaxV"].ToString();
            txtPDiscountP.Text = dt.Rows[0]["DiscountP"].ToString();
            txtPDiscountV.Text = dt.Rows[0]["DiscountV"].ToString();
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
        GvProductDetail.DataSource = dt;
        GvProductDetail.DataBind();

        foreach (GridViewRow gvr in GvProductDetail.Rows)
        {
            Label lblgvUnitPrice = (Label)gvr.FindControl("lblgvUnitPrice");
            Label lblgvQuantity = (Label)gvr.FindControl("lblgvQuantity");
            Label lblgvTaxV = (Label)gvr.FindControl("lblgvTaxV");
            Label lblgvPriceAfterTax = (Label)gvr.FindControl("lblgvPriceAfterTax");
            Label lblgvDiscountV = (Label)gvr.FindControl("lblgvDiscountV");
            Label lblgvPriceAfterDiscount = (Label)gvr.FindControl("lblgvPriceAfterDiscount");
            Label lblgvTotal = (Label)gvr.FindControl("lblgvTotal");
            string strQuantityPrice = string.Empty;

            if (lblgvUnitPrice.Text != "")
            {
                if (lblgvQuantity.Text != "")
                {
                    strQuantityPrice = (float.Parse(lblgvUnitPrice.Text) * float.Parse(lblgvQuantity.Text)).ToString();
                }

                if (lblgvTaxV.Text != "")
                {
                    lblgvPriceAfterTax.Text = (float.Parse(strQuantityPrice) + float.Parse(lblgvTaxV.Text)).ToString();
                    lblgvTotal.Text = (float.Parse(strQuantityPrice) + float.Parse(lblgvTaxV.Text)).ToString();
                }
                else
                {
                    lblgvPriceAfterTax.Text = strQuantityPrice;
                    lblgvTotal.Text = lblgvPriceAfterTax.Text;
                }
                if (lblgvDiscountV.Text != "")
                {
                    lblgvPriceAfterDiscount.Text = (float.Parse(lblgvPriceAfterTax.Text) - float.Parse(lblgvDiscountV.Text)).ToString();
                    lblgvTotal.Text = (float.Parse(lblgvPriceAfterTax.Text) - float.Parse(lblgvDiscountV.Text)).ToString();
                }
                else
                {
                    lblgvPriceAfterDiscount.Text = lblgvPriceAfterTax.Text;
                    lblgvTotal.Text = lblgvPriceAfterTax.Text;
                }
            }
        }
        ResetProduct();
    }
    public DataTable FillProductDataTableUpdate()
    {
        DataTable dt = CreateProductDataTable();
        for (int i = 0; i < GvProductDetail.Rows.Count; i++)
        {
            dt.Rows.Add(i);
            Label lblgvSNo = (Label)GvProductDetail.Rows[i].FindControl("lblgvSerialNo");
            HiddenField hdngvProductId = (HiddenField)GvProductDetail.Rows[i].FindControl("hdngvProductId");
            Label lblgvProductDescription = (Label)GvProductDetail.Rows[i].FindControl("lblgvProductDescription");
            HiddenField hdngvUnitId = (HiddenField)GvProductDetail.Rows[i].FindControl("hdngvUnitId");
            Label lblgvUnitPrice = (Label)GvProductDetail.Rows[i].FindControl("lblgvUnitPrice");
            Label lblgvUnitCost = (Label)GvProductDetail.Rows[i].FindControl("lblgvUnitCost");
            Label lblgvQuantity = (Label)GvProductDetail.Rows[i].FindControl("lblgvQuantity");
            Label lblgvTaxP = (Label)GvProductDetail.Rows[i].FindControl("lblgvTaxP");
            Label lblgvTaxV = (Label)GvProductDetail.Rows[i].FindControl("lblgvTaxV");
            Label lblgvDiscountP = (Label)GvProductDetail.Rows[i].FindControl("lblgvDiscountP");
            Label lblgvDiscountV = (Label)GvProductDetail.Rows[i].FindControl("lblgvDiscountV");

            dt.Rows[i]["Serial_No"] = lblgvSNo.Text;
            dt.Rows[i]["Product_Id"] = hdngvProductId.Value;
            dt.Rows[i]["ProductDescription"] = lblgvProductDescription.Text;
            dt.Rows[i]["Unit_Id"] = hdngvUnitId.Value;
            dt.Rows[i]["UnitPrice"] = lblgvUnitPrice.Text;
            dt.Rows[i]["UnitCost"] = lblgvUnitCost.Text;
            dt.Rows[i]["Quantity"] = lblgvQuantity.Text;
            dt.Rows[i]["TaxP"] = lblgvTaxP.Text;
            dt.Rows[i]["TaxV"] = lblgvTaxV.Text;
            dt.Rows[i]["DiscountP"] = lblgvDiscountP.Text;
            dt.Rows[i]["DiscountV"] = lblgvDiscountV.Text;

        }
        for (int i = 0; i < dt.Rows.Count; i++)
        {
            if (hdnProductId.Value == dt.Rows[i]["Serial_No"].ToString())
            {
                dt.Rows[i]["Product_Id"] = hdnNewProductId.Value;
                dt.Rows[i]["ProductDescription"] = txtPDescription.Text;
                dt.Rows[i]["Unit_Id"] = hdnUnitId.Value;
                dt.Rows[i]["UnitPrice"] = txtPUnitPrice.Text;
                dt.Rows[i]["UnitCost"] = txtPUnitCost.Text;
                dt.Rows[i]["Quantity"] = txtPQuantity.Text;
                dt.Rows[i]["TaxP"] = txtPTaxP.Text;
                dt.Rows[i]["TaxV"] = txtPTaxV.Text;
                dt.Rows[i]["DiscountP"] = txtPDiscountP.Text;
                dt.Rows[i]["DiscountV"] = txtPDiscountV.Text;
            }
        }
        return dt;
    }
    protected void txtPUnitPrice_TextChanged(object sender, EventArgs e)
    {
        if (txtPUnitPrice.Text != "")
        {
            if (txtPQuantity.Text != "")
            {
                txtPQuantityPrice.Text = (float.Parse(txtPUnitPrice.Text) * float.Parse(txtPQuantity.Text)).ToString();
            }

            if (txtPTaxP.Text != "")
            {
                txtPTaxP_TextChanged(sender, e);
            }
            else
            {
                txtPTotalAmount.Text = txtPUnitPrice.Text;
                if (txtPDiscountP.Text != "")
                {
                    txtPDiscountP_TextChanged(sender, e);
                }
            }
        }
    }
    protected void txtPQuantity_TextChanged(object sender, EventArgs e)
    {
        if (txtPUnitPrice.Text != "")
        {
            if (txtPQuantity.Text != "")
            {
                txtPQuantityPrice.Text = (float.Parse(txtPUnitPrice.Text) * float.Parse(txtPQuantity.Text)).ToString();
                txtPTotalAmount.Text = txtPQuantityPrice.Text;
            }
            if (txtPQuantityPrice.Text != "")
            {
                if (txtPTaxP.Text != "")
                {
                    txtPTaxP_TextChanged(sender, e);
                }
                if (txtDiscountP.Text != "")
                {
                    txtPDiscountP_TextChanged(sender, e);
                }
            }
        }
        else
        {
            txtPQuantity.Text = "";
            ScriptManager.RegisterStartupScript(this, typeof(string), "OPEN_WINDOW", "window.alert('First Fill Unit Price');", true);
            System.Web.UI.ScriptManager.GetCurrent(this).SetFocus(txtPUnitPrice);
        }
    }
    protected void txtPUnitCost_TextChanged(object sender, EventArgs e)
    {

    }
    protected void txtPTaxP_TextChanged(object sender, EventArgs e)
    {
        if (txtPUnitPrice.Text != "")
        {
            float flTemp = 0;
            if (float.TryParse(txtPUnitPrice.Text, out flTemp))
            {
                if (txtPQuantityPrice.Text != "")
                {
                    if (txtPTaxP.Text != "")
                    {
                        if (float.TryParse(txtPTaxP.Text, out flTemp))
                        {
                            txtPTaxV.Text = ((float.Parse(txtPQuantityPrice.Text) * float.Parse(txtPTaxP.Text)) / 100).ToString();
                            if (txtPQuantityPrice.Text != "")
                            {
                                txtPPriceAfterTax.Text = (float.Parse(txtPQuantityPrice.Text) + float.Parse(txtPTaxV.Text)).ToString();
                                txtPPriceAfterDiscount.Text = (float.Parse(txtPQuantityPrice.Text) + float.Parse(txtPTaxV.Text)).ToString();
                            }
                            if (txtPDiscountP.Text != "")
                            {
                                txtPDiscountP_TextChanged(sender, e);
                            }
                            else
                            {
                                txtPTotalAmount.Text = txtPPriceAfterTax.Text;
                            }
                            System.Web.UI.ScriptManager.GetCurrent(this).SetFocus(txtPDiscountP);
                        }
                        else
                        {
                            txtPTaxP.Text = "";
                            ScriptManager.RegisterStartupScript(this, typeof(string), "OPEN_WINDOW", "window.alert('Enter Numeric Value Only');", true);
                            System.Web.UI.ScriptManager.GetCurrent(this).SetFocus(txtPTaxP);
                        }
                    }
                }
                else
                {
                    txtPTaxP.Text = "";
                    ScriptManager.RegisterStartupScript(this, typeof(string), "OPEN_WINDOW", "window.alert('Get Quantity Price');", true);
                    System.Web.UI.ScriptManager.GetCurrent(this).SetFocus(txtPQuantity);
                }
            }
            else
            {
                txtPTaxP.Text = "";
                txtPUnitPrice.Text = "";
                ScriptManager.RegisterStartupScript(this, typeof(string), "OPEN_WINDOW", "window.alert('Enter Numeric Value Only');", true);
                System.Web.UI.ScriptManager.GetCurrent(this).SetFocus(txtPUnitPrice);
            }
        }
        else
        {
            txtPTaxP.Text = "";
            ScriptManager.RegisterStartupScript(this, typeof(string), "OPEN_WINDOW", "window.alert('First Fill Unit Price');", true);
            System.Web.UI.ScriptManager.GetCurrent(this).SetFocus(txtPUnitPrice);
        }
    }
    protected void txtPTaxV_TextChanged(object sender, EventArgs e)
    {
        if (txtPUnitPrice.Text != "")
        {
            float flTemp = 0;
            if (float.TryParse(txtPUnitPrice.Text, out flTemp))
            {
                if (txtPQuantityPrice.Text != "")
                {
                    if (txtPTaxV.Text != "")
                    {
                        if (float.TryParse(txtPTaxV.Text, out flTemp))
                        {
                            txtPTaxP.Text = ((100 * float.Parse(txtPTaxV.Text)) / float.Parse(txtPQuantityPrice.Text)).ToString();
                            if (txtPQuantityPrice.Text != "")
                            {
                                txtPPriceAfterTax.Text = (float.Parse(txtPQuantityPrice.Text) + float.Parse(txtPTaxV.Text)).ToString();
                                txtPPriceAfterDiscount.Text = (float.Parse(txtPQuantityPrice.Text) + float.Parse(txtPTaxV.Text)).ToString();
                                txtPTotalAmount.Text = (float.Parse(txtPQuantityPrice.Text) + float.Parse(txtPPriceAfterTax.Text)).ToString();
                            }
                            if (txtPDiscountP.Text != "")
                            {
                                txtPDiscountP_TextChanged(sender, e);
                            }
                            System.Web.UI.ScriptManager.GetCurrent(this).SetFocus(txtPDiscountP);
                        }
                        else
                        {
                            txtPTaxV.Text = "";
                            ScriptManager.RegisterStartupScript(this, typeof(string), "OPEN_WINDOW", "window.alert('Enter Numeric Value Only');", true);
                            System.Web.UI.ScriptManager.GetCurrent(this).SetFocus(txtPTaxV);
                        }
                    }
                }
                else
                {
                    txtPTaxV.Text = "";
                    ScriptManager.RegisterStartupScript(this, typeof(string), "OPEN_WINDOW", "window.alert('Get Quantity Price');", true);
                    System.Web.UI.ScriptManager.GetCurrent(this).SetFocus(txtPQuantity);
                }
            }
            else
            {
                txtPTaxV.Text = "";
                txtPUnitPrice.Text = "";
                ScriptManager.RegisterStartupScript(this, typeof(string), "OPEN_WINDOW", "window.alert('Enter Numeric Value Only');", true);
                System.Web.UI.ScriptManager.GetCurrent(this).SetFocus(txtPUnitPrice);
            }
        }
        else
        {
            txtPTaxV.Text = "";
            ScriptManager.RegisterStartupScript(this, typeof(string), "OPEN_WINDOW", "window.alert('First Enter Unit Price');", true);
            System.Web.UI.ScriptManager.GetCurrent(this).SetFocus(txtPUnitPrice);
        }
    }
    protected void txtPDiscountP_TextChanged(object sender, EventArgs e)
    {
        if (txtPUnitPrice.Text != "")
        {
            float flTemp = 0;
            if (float.TryParse(txtPUnitPrice.Text, out flTemp))
            {
                if (txtPQuantityPrice.Text != "")
                {
                    if (txtPDiscountP.Text != "")
                    {
                        if (float.TryParse(txtPDiscountP.Text, out flTemp))
                        {
                            if (txtPPriceAfterTax.Text != "")
                            {
                                if (txtPPriceAfterTax.Text == "0")
                                {
                                    txtPDiscountV.Text = ((float.Parse(txtPQuantityPrice.Text) * float.Parse(txtPDiscountP.Text)) / 100).ToString();
                                    txtPTotalAmount.Text = (float.Parse(txtPQuantityPrice.Text) - float.Parse(txtPDiscountV.Text)).ToString();
                                    txtPPriceAfterDiscount.Text = (float.Parse(txtPQuantityPrice.Text) - float.Parse(txtPDiscountV.Text)).ToString();
                                }
                                else
                                {
                                    txtPDiscountV.Text = ((float.Parse(txtPPriceAfterTax.Text) * float.Parse(txtPDiscountP.Text)) / 100).ToString();
                                    txtPTotalAmount.Text = (float.Parse(txtPPriceAfterTax.Text) - float.Parse(txtPDiscountV.Text)).ToString();
                                    txtPPriceAfterDiscount.Text = (float.Parse(txtPPriceAfterTax.Text) - float.Parse(txtPDiscountV.Text)).ToString();
                                }
                                //System.Web.UI.ScriptManager.GetCurrent(this).SetFocus(txtShippingCharge);
                            }
                        }
                        else
                        {
                            txtPDiscountP.Text = "";
                            ScriptManager.RegisterStartupScript(this, typeof(string), "OPEN_WINDOW", "window.alert('Enter Numeric Value Only');", true);
                            System.Web.UI.ScriptManager.GetCurrent(this).SetFocus(txtPDiscountP);
                        }
                    }
                }
                else
                {
                    txtPDiscountP.Text = "";
                    ScriptManager.RegisterStartupScript(this, typeof(string), "OPEN_WINDOW", "window.alert('Get Quantity Price');", true);
                    System.Web.UI.ScriptManager.GetCurrent(this).SetFocus(txtPQuantity);
                }
            }
            else
            {
                txtPDiscountP.Text = "";
                txtPUnitPrice.Text = "";
                ScriptManager.RegisterStartupScript(this, typeof(string), "OPEN_WINDOW", "window.alert('Enter Numeric Value Only');", true);
                System.Web.UI.ScriptManager.GetCurrent(this).SetFocus(txtPUnitPrice);
            }
        }
        else
        {
            txtPDiscountP.Text = "";
            ScriptManager.RegisterStartupScript(this, typeof(string), "OPEN_WINDOW", "window.alert('First Enter Unit Price');", true);
            System.Web.UI.ScriptManager.GetCurrent(this).SetFocus(txtPUnitPrice);
        }
    }
    protected void txtPDiscountV_TextChanged(object sender, EventArgs e)
    {
        if (txtPUnitPrice.Text != "")
        {
            float flTemp = 0;
            if (float.TryParse(txtPUnitPrice.Text, out flTemp))
            {
                if (txtPQuantityPrice.Text != "")
                {
                    if (txtPDiscountV.Text != "")
                    {
                        if (float.TryParse(txtPDiscountV.Text, out flTemp))
                        {
                            if (txtPPriceAfterTax.Text != "")
                            {
                                if (txtPPriceAfterTax.Text == "0")
                                {
                                    txtPDiscountP.Text = ((100 * float.Parse(txtPDiscountV.Text)) / float.Parse(txtPQuantityPrice.Text)).ToString();
                                    txtPTotalAmount.Text = (float.Parse(txtPQuantityPrice.Text) - float.Parse(txtPDiscountV.Text)).ToString();
                                }
                                else
                                {
                                    txtPDiscountP.Text = ((100 * float.Parse(txtPDiscountV.Text)) / float.Parse(txtPPriceAfterTax.Text)).ToString();
                                    txtPTotalAmount.Text = (float.Parse(txtPPriceAfterTax.Text) - float.Parse(txtPDiscountV.Text)).ToString();
                                }
                                //System.Web.UI.ScriptManager.GetCurrent(this).SetFocus(txtShippingCharge);
                            }
                        }
                        else
                        {
                            txtPDiscountV.Text = "";
                            ScriptManager.RegisterStartupScript(this, typeof(string), "OPEN_WINDOW", "window.alert('Enter Numeric Value Only');", true);
                            System.Web.UI.ScriptManager.GetCurrent(this).SetFocus(txtPDiscountV);
                        }
                    }
                }
                else
                {
                    txtPDiscountV.Text = "";
                    ScriptManager.RegisterStartupScript(this, typeof(string), "OPEN_WINDOW", "window.alert('Get Quantity Price');", true);
                    System.Web.UI.ScriptManager.GetCurrent(this).SetFocus(txtPQuantity);
                }
            }
            else
            {
                txtPDiscountV.Text = "";
                txtPUnitPrice.Text = "";
                ScriptManager.RegisterStartupScript(this, typeof(string), "OPEN_WINDOW", "window.alert('Enter Numeric Value Only');", true);
                System.Web.UI.ScriptManager.GetCurrent(this).SetFocus(txtPUnitPrice);
            }
        }
        else
        {
            txtPDiscountV.Text = "";
            ScriptManager.RegisterStartupScript(this, typeof(string), "OPEN_WINDOW", "window.alert('First Enter Unit Price');", true);
            System.Web.UI.ScriptManager.GetCurrent(this).SetFocus(txtPUnitPrice);
        }
    }
    #endregion
    [System.Web.Services.WebMethodAttribute(), System.Web.Script.Services.ScriptMethodAttribute()]
    public static string[] GetCompletionListAddressName(string prefixText, int count, string contextKey)
    {
        Set_AddressMaster AddressN = new Set_AddressMaster();
        DataTable dt = AddressN.GetDistinctAddressName(HttpContext.Current.Session["CompId"].ToString(), prefixText);
        string[] str = new string[dt.Rows.Count];
        if (dt.Rows.Count > 0)
        {
            for (int i = 0; i < dt.Rows.Count; i++)
            {
                str[i] = dt.Rows[i]["Address_Name"].ToString();
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
                dt = AddressN.GetAddressAllData(HttpContext.Current.Session["CompId"].ToString());
                if (dt.Rows.Count > 0)
                {
                    str = new string[dt.Rows.Count];
                    for (int i = 0; i < dt.Rows.Count; i++)
                    {
                        str[i] = dt.Rows[i]["Address_Name"].ToString();
                    }
                }
            }
        }
        return str;
    }

}
