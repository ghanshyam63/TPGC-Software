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

public partial class Sales_SalesOrder : System.Web.UI.Page
{
    #region defind Class Object
    Common cmn = new Common();
    Inv_SalesOrderHeader objSOrderHeader = new Inv_SalesOrderHeader();
    Inv_SalesOrderDetail ObjSOrderDetail = new Inv_SalesOrderDetail();
    Inv_SalesQuotationHeader objSQuoteHeader = new Inv_SalesQuotationHeader();
    Inv_SalesQuotationDetail ObjSQuoteDetail = new Inv_SalesQuotationDetail();
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
            FillPaymentMode();
            txtSONo.Text = GetDocumentNumber(); //updated by jitendra on 28-9-2013
            //txtSONo.Text = objSOrderHeader.GetAutoID(StrCompId, StrBrandId, StrLocationId);
            txtSODate.Text = DateTime.Now.ToString(ObjSysParam.GetSysParameterByParamName("Date_Format").Rows[0]["Param_Value"].ToString());
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
                btnSOrderSave.Visible = true;
                foreach (GridViewRow Row in GvSalesOrder.Rows)
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
                        btnSOrderSave.Visible = true;
                    }
                    foreach (GridViewRow Row in GvSalesOrder.Rows)
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

        DataTable Dt = objDocNo.GetDocumentNumberAll(StrCompId, "13", "67");

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
                DocumentNo += "-" + (Convert.ToInt32(objSOrderHeader.GetSOHeaderAll(StrCompId.ToString(), StrBrandId, StrLocationId).Rows.Count) + 1).ToString();
            }
            else
            {
                DocumentNo += (Convert.ToInt32(objSOrderHeader.GetSOHeaderAll(StrCompId.ToString(), StrBrandId, StrLocationId).Rows.Count) + 1).ToString();

            }
        }
        else
        {
            DocumentNo += (Convert.ToInt32(objSOrderHeader.GetSOHeaderAll(StrCompId.ToString(), StrBrandId, StrLocationId).Rows.Count) + 1).ToString();
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

        DataTable dtOrderEdit = objSOrderHeader.GetSOHeaderAllByTransId(StrCompId, StrBrandId, StrLocationId, editid.Value);

        btnNew.Text = Resources.Attendance.Edit;
        if (dtOrderEdit.Rows.Count > 0)
        {
            txtSONo.Text = dtOrderEdit.Rows[0]["SalesOrderNo"].ToString();
            txtSONo.ReadOnly = true;
            txtSODate.Text = Convert.ToDateTime(dtOrderEdit.Rows[0]["SalesOrderDate"].ToString()).ToString(ObjSysParam.GetSysParameterByParamName("Date_Format").Rows[0]["Param_Value"].ToString());
            string strSOFromTransType = dtOrderEdit.Rows[0]["SOfromTransType"].ToString();

            if (strSOFromTransType == "S")
            {
                ddlOrderType.SelectedValue = "Q";
                ddlOrderType_SelectedIndexChanged(sender, e);
                ddlQuotationNo.Visible = false;
                txtQuotationNo.Visible = true;
                hdnSalesQuotationId.Value = dtOrderEdit.Rows[0]["SOfromTransNo"].ToString();

                DataTable dtQuotationHeader = objSQuoteHeader.GetQuotationHeaderAllBySQuotationId(StrCompId, StrBrandId, StrLocationId, hdnSalesQuotationId.Value);
                if (dtQuotationHeader.Rows.Count > 0)
                {
                    txtQuotationNo.Text = dtQuotationHeader.Rows[0]["SQuotation_No"].ToString();

                    DataTable dtQuoteChild = ObjSQuoteDetail.GetQuotationDetailBySQuotation_Id(StrCompId, StrBrandId, StrLocationId, hdnSalesQuotationId.Value);
                    if (dtQuoteChild.Rows.Count > 0)
                    {
                        GvQuotationDetail.DataSource = dtQuoteChild;
                        GvQuotationDetail.DataBind();
                        FillUnit();

                        float fGrossTotal = 0.00f;
                        foreach (GridViewRow gvr in GvQuotationDetail.Rows)
                        {
                            HiddenField hdngvProductId = (HiddenField)gvr.FindControl("hdngvProductId");
                            DropDownList ddlgvUnit = (DropDownList)gvr.FindControl("ddlgvUnit");
                            TextBox txtgvFreeQuantity = (TextBox)gvr.FindControl("txtgvFreeQuantity");
                            TextBox txtgvRemainQuantity = (TextBox)gvr.FindControl("txtgvRemainQuantity");
                            TextBox txtgvUnitPrice = (TextBox)gvr.FindControl("txtgvUnitPrice");
                            TextBox txtgvTaxP = (TextBox)gvr.FindControl("txtgvTaxP");
                            TextBox txtgvTaxV = (TextBox)gvr.FindControl("txtgvTaxV");
                            TextBox txtgvPriceAfterTax = (TextBox)gvr.FindControl("txtgvPriceAfterTax");
                            TextBox txtgvDiscountP = (TextBox)gvr.FindControl("txtgvDiscountP");
                            TextBox txtgvDiscountV = (TextBox)gvr.FindControl("txtgvDiscountV");
                            TextBox txtgvPriceAfterDiscount = (TextBox)gvr.FindControl("txtgvPriceAfterDiscount");
                            TextBox txtgvTotal = (TextBox)gvr.FindControl("txtgvTotal");

                            DataTable dtOrderDetailByProductId = ObjSOrderDetail.GetSODetailBySOrderNo(StrCompId, StrBrandId, StrLocationId, editid.Value);

                            dtOrderDetailByProductId = new DataView(dtOrderDetailByProductId, "Product_Id='" + hdngvProductId.Value + "'", "", DataViewRowState.CurrentRows).ToTable();

                            if (dtOrderDetailByProductId.Rows.Count > 0)
                            {
                                txtgvFreeQuantity.Text = dtOrderDetailByProductId.Rows[0]["FreeQty"].ToString();

                                ddlgvUnit.SelectedValue = dtOrderDetailByProductId.Rows[0]["UnitId"].ToString();
                                txtgvRemainQuantity.Text = dtOrderDetailByProductId.Rows[0]["RemainQty"].ToString();
                                txtgvUnitPrice.Text = dtOrderDetailByProductId.Rows[0]["UnitPrice"].ToString();
                                txtgvTaxP.Text = dtOrderDetailByProductId.Rows[0]["TaxP"].ToString();
                                txtgvTaxV.Text = dtOrderDetailByProductId.Rows[0]["TaxV"].ToString();
                                txtgvPriceAfterTax.Text = (float.Parse(txtgvUnitPrice.Text) + float.Parse(txtgvTaxV.Text)).ToString();
                                txtgvDiscountP.Text = dtOrderDetailByProductId.Rows[0]["DiscountP"].ToString();
                                txtgvDiscountV.Text = dtOrderDetailByProductId.Rows[0]["DiscountV"].ToString();
                                txtgvPriceAfterDiscount.Text = (float.Parse(txtgvPriceAfterTax.Text) - float.Parse(txtgvDiscountV.Text)).ToString();
                                txtgvTotal.Text = (float.Parse(txtgvPriceAfterTax.Text) - float.Parse(txtgvDiscountV.Text)).ToString();
                            }
                            else
                            {
                                txtgvFreeQuantity.Text = "0";
                                txtgvRemainQuantity.Text = "0";
                                txtgvUnitPrice.Text = "0";
                                txtgvTaxP.Text = "0";
                                txtgvTaxV.Text = "0";
                                txtgvPriceAfterTax.Text = (float.Parse(txtgvUnitPrice.Text) + float.Parse(txtgvTaxV.Text)).ToString();
                                txtgvDiscountP.Text = "0";
                                txtgvDiscountV.Text = "0";
                                txtgvPriceAfterDiscount.Text = (float.Parse(txtgvPriceAfterTax.Text) - float.Parse(txtgvDiscountV.Text)).ToString();
                                txtgvTotal.Text = (float.Parse(txtgvPriceAfterTax.Text) - float.Parse(txtgvDiscountV.Text)).ToString();
                            }
                            if (txtgvTotal.Text != "")
                            {
                                fGrossTotal = fGrossTotal + float.Parse(txtgvTotal.Text);
                            }
                        }
                        txtAmount.Text = fGrossTotal.ToString();
                    }
                }
            }
            else
            {
                ddlOrderType.SelectedValue = "D";
                ddlOrderType_SelectedIndexChanged(sender, e);

                DataTable dtOrderDetail = ObjSOrderDetail.GetSODetailBySOrderNo(StrCompId, StrBrandId, StrLocationId, editid.Value);
                if (dtOrderDetail.Rows.Count > 0)
                {
                    GvProductDetail.DataSource = dtOrderDetail;
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

            txtTransFrom.Text = GetTransType(dtOrderEdit.Rows[0]["SOfromTransType"].ToString());

            string strCustomerId = dtOrderEdit.Rows[0]["CustomerId"].ToString();
            txtCustomer.Text = GetCustomerName(dtOrderEdit.Rows[0]["CustomerId"].ToString()) + "/" + strCustomerId;

            ddlPaymentMode.SelectedValue = dtOrderEdit.Rows[0]["PaymentModeId"].ToString();
            txtEstimateDeliveryDate.Text = Convert.ToDateTime(dtOrderEdit.Rows[0]["EstimateDeliveryDate"].ToString()).ToString(ObjSysParam.GetSysParameterByParamName("Date_Format").Rows[0]["Param_Value"].ToString());
            string strAddressId = dtOrderEdit.Rows[0]["ShipToAddressID"].ToString();

            DataTable dtAddName = objAddMaster.GetAddressDataByTransId(strAddressId, StrCompId);
            if (dtAddName.Rows.Count > 0)
            {
                txtShipingAddress.Text = dtAddName.Rows[0]["Address_Name"].ToString();
            }
            else
            {
                txtShipingAddress.Text = "";
            }

            txtTaxP.Text = dtOrderEdit.Rows[0]["TaxP"].ToString();
            txtTaxV.Text = dtOrderEdit.Rows[0]["TaxV"].ToString();
            txtDiscountP.Text = dtOrderEdit.Rows[0]["DiscountP"].ToString();
            txtDiscountV.Text = dtOrderEdit.Rows[0]["DiscountV"].ToString();
            txtAmount_TextChanged(sender, e);

            txtShippingCharge.Text = dtOrderEdit.Rows[0]["ShippingCharge"].ToString();
            txtRemark.Text = dtOrderEdit.Rows[0]["Remark"].ToString();
            txtNetAmount.Text = dtOrderEdit.Rows[0]["NetAmount"].ToString();
            string strPost = dtOrderEdit.Rows[0]["Post"].ToString();
        }
        btnNew_Click(null, null);
        System.Web.UI.ScriptManager.GetCurrent(this).SetFocus(txtSONo);
    }
    protected void GvSalesOrder_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        GvSalesOrder.PageIndex = e.NewPageIndex;
        DataTable dt = (DataTable)Session["dtFilter"];
        GvSalesOrder.DataSource = dt;
        GvSalesOrder.DataBind();
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
            GvSalesOrder.DataSource = view.ToTable();
            Session["dtFilter"] = view.ToTable();
            lblTotalRecords.Text = Resources.Attendance.Total_Records + " : " + view.ToTable().Rows.Count + "";
            GvSalesOrder.DataBind();
        }
    }
    protected void GvSalesOrder_Sorting(object sender, GridViewSortEventArgs e)
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
        GvSalesOrder.DataSource = dt;
        GvSalesOrder.DataBind();
    }
    protected void IbtnDelete_Command(object sender, CommandEventArgs e)
    {
        editid.Value = e.CommandArgument.ToString();
        int b = 0;
        b = objSOrderHeader.DeleteSOHeader(StrCompId, StrBrandId, StrLocationId, editid.Value, "false", StrUserId, DateTime.Now.ToString());
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
    protected void btnSOrderCancel_Click(object sender, EventArgs e)
    {
        Reset();
        btnList_Click(null, null);
        FillGridBin();
        FillGrid();
    }
    protected void BtnReset_Click(object sender, EventArgs e)
    {
        Reset();
        System.Web.UI.ScriptManager.GetCurrent(this).SetFocus(txtSONo);
    }
    protected void btnSOrderSave_Click(object sender, EventArgs e)
    {
        if (txtSODate.Text == "")
        {
            DisplayMessage("Enter Sales Order Date");
            System.Web.UI.ScriptManager.GetCurrent(this).SetFocus(txtSODate);
            return;
        }
        if (txtSONo.Text == "")
        {
            DisplayMessage("Enter Sales Order No.");
            System.Web.UI.ScriptManager.GetCurrent(this).SetFocus(txtSONo);
            return;
        }
        if (ddlOrderType.SelectedIndex == 0)
        {
            DisplayMessage("Select Order Type");
            ddlOrderType.Focus();
            return;
            
        }
        if (ddlOrderType.SelectedValue == "Q")
        {
            if (ddlQuotationNo.SelectedIndex == 0)
            {
                DisplayMessage("Select Quotation");
                ddlQuotationNo.Focus();
                return;
            }
        }
        else
        {
            if (editid.Value == "")
            {
                DataTable dtSQNo = objSOrderHeader.GetSOHeaderAllBySalesOrderNo(StrCompId, StrBrandId, StrLocationId, txtSONo.Text);
                if (dtSQNo.Rows.Count > 0)
                {
                    DisplayMessage("Sales Quotation No. Already Exits");
                    txtSONo.Text = "";
                    System.Web.UI.ScriptManager.GetCurrent(this).SetFocus(txtSONo);
                    return;
                }
            }
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
        if (ddlPaymentMode.SelectedValue == "--Select--")
        {
            DisplayMessage("Select PaymentMode");
            ddlPaymentMode.Focus();
            return;
        }
        if (txtEstimateDeliveryDate.Text == "")
        {
            DisplayMessage("Enter Estimated Delievery Date");
            txtEstimateDeliveryDate.Focus();
            return;
        }
        if (txtShippingCharge.Text == "")
        {
            txtShippingCharge.Text = "0";
        }
        if (txtNetAmount.Text == "")
        {
            txtNetAmount.Text = "0";
        }
        if (txtAmount.Text == "")
        {
            txtAmount.Text="0";
        }


        string strAddressId = string.Empty;
        if (txtShipingAddress.Text != "")
        {
            DataTable dtAddId = objAddMaster.GetAddressDataByAddressName(StrCompId, txtShipingAddress.Text);
            if (dtAddId.Rows.Count > 0)
            {
                strAddressId = dtAddId.Rows[0]["Trans_Id"].ToString();
            }
        }
        else
        {
            DisplayMessage("Enter Shipping Address");
            System.Web.UI.ScriptManager.GetCurrent(this).SetFocus(txtShipingAddress);
            return;
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
                if (GvQuotationDetail.Rows.Count > 0)
                {

                }
                else
                {
                    DisplayMessage("No Product available For Generate Sales Order");
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
        if (txtTotalAmount.Text == "")
        {
            txtTotalAmount.Text = "0";
        }


        int b = 0;
        if (editid.Value != "")
        {
            b = objSOrderHeader.UpdateSOHeader(StrCompId, StrBrandId, StrLocationId, editid.Value, txtSONo.Text, txtSODate.Text, ddlPaymentMode.SelectedValue, txtEstimateDeliveryDate.Text, txtTransFrom.Text, hdnSalesQuotationId.Value, GetCustomerId(), strAddressId, txtTaxP.Text, txtTaxV.Text, txtDiscountP.Text, txtDiscountV.Text, txtShippingCharge.Text, txtRemark.Text, txtNetAmount.Text, "False", "", "", "", "", "", "True", DateTime.Now.ToString(), "True", Session["UserId"].ToString(), DateTime.Now.ToString());

            if (b != 0)
            {
                ObjSOrderDetail.DeleteSODetail(StrCompId, StrBrandId, StrLocationId, editid.Value);
                if (ddlOrderType.SelectedValue == "D")
                {
                    foreach (GridViewRow gvr in GvProductDetail.Rows)
                    {
                        Label lblgvSerialNo = (Label)gvr.FindControl("lblgvSerialNo");
                        HiddenField hdngvProductId = (HiddenField)gvr.FindControl("hdngvProductId");
                        HiddenField hdngvUnitId = (HiddenField)gvr.FindControl("hdngvUnitId");
                        HiddenField hdngvCurrencyId = (HiddenField)gvr.FindControl("hdnCurrencyId");
                        Label lblgvQuantity = (Label)gvr.FindControl("lblgvQuantity");
                        Label lblgvFreeQuantity = (Label)gvr.FindControl("lblgvFreeQuantity");
                        Label lblgvRemainQuantity = (Label)gvr.FindControl("lblgvRemainQuantity");
                        Label lblgvUnitPrice = (Label)gvr.FindControl("lblgvUnitPrice");
                        Label lblgvTaxP = (Label)gvr.FindControl("lblgvTaxP");
                        Label lblgvTaxV = (Label)gvr.FindControl("lblgvTaxV");
                        Label lblgvDiscountP = (Label)gvr.FindControl("lblgvDiscountP");
                        Label lblgvDiscountV = (Label)gvr.FindControl("lblgvDiscountV");

                        if (lblgvUnitPrice.Text == "")
                        {
                            lblgvUnitPrice.Text = "0";
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

                        ObjSOrderDetail.InsertSODetail(StrCompId, StrBrandId, StrLocationId, editid.Value, lblgvSerialNo.Text, hdngvProductId.Value, lblgvQuantity.Text, lblgvFreeQuantity.Text, lblgvRemainQuantity.Text, hdngvUnitId.Value, lblgvUnitPrice.Text, lblgvTaxP.Text, lblgvTaxV.Text, lblgvDiscountP.Text, lblgvDiscountV.Text, "", "", "", "", "", "True", DateTime.Now.ToString(), "True", Session["UserId"].ToString(), DateTime.Now.ToString(), Session["UserId"].ToString(), DateTime.Now.ToString());
                    }
                }
                else if (ddlOrderType.SelectedValue == "Q")
                {
                    foreach (GridViewRow gvrQ in GvQuotationDetail.Rows)
                    {
                        Label lblgvSerialNo = (Label)gvrQ.FindControl("lblgvSerialNo");
                        HiddenField hdngvProductId = (HiddenField)gvrQ.FindControl("hdngvProductId");
                        DropDownList ddlgvUnit = (DropDownList)gvrQ.FindControl("ddlgvUnit");
                        HiddenField hdngvCurrencyId = (HiddenField)gvrQ.FindControl("hdnCurrencyId");
                        Label lblgvQuantity = (Label)gvrQ.FindControl("lblgvQuantity");
                        TextBox txtgvFreeQuantity = (TextBox)gvrQ.FindControl("txtgvFreeQuantity");
                        TextBox txtgvRemainQuantity = (TextBox)gvrQ.FindControl("txtgvRemainQuantity");
                        TextBox txtgvUnitPrice = (TextBox)gvrQ.FindControl("txtgvUnitPrice");
                        TextBox txtgvTaxP = (TextBox)gvrQ.FindControl("txtgvTaxP");
                        TextBox txtgvTaxV = (TextBox)gvrQ.FindControl("txtgvTaxV");
                        TextBox txtgvDiscountP = (TextBox)gvrQ.FindControl("txtgvDiscountP");
                        TextBox txtgvDiscountV = (TextBox)gvrQ.FindControl("txtgvDiscountV");

                        if (txtgvFreeQuantity.Text == "")
                        {
                            txtgvFreeQuantity.Text = "0";
                        }
                        if (txtgvRemainQuantity.Text == "")
                        {
                            txtgvRemainQuantity.Text = "0";
                        }
                        if (txtgvUnitPrice.Text == "")
                        {
                            txtgvUnitPrice.Text = "0";
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

                        string strUnitId = string.Empty;
                        if (ddlgvUnit.SelectedValue == "--Select--")
                        {
                            strUnitId = "0";
                        }
                        else
                        {
                            strUnitId = ddlgvUnit.SelectedValue;
                        }

                        ObjSOrderDetail.InsertSODetail(StrCompId, StrBrandId, StrLocationId, editid.Value, lblgvSerialNo.Text, hdngvProductId.Value, lblgvQuantity.Text, txtgvFreeQuantity.Text, txtgvRemainQuantity.Text, strUnitId, txtgvUnitPrice.Text, txtgvTaxP.Text, txtgvTaxV.Text, txtgvDiscountP.Text, txtgvDiscountV.Text, "", "", "", "", "", "True", DateTime.Now.ToString(), "True", Session["UserId"].ToString(), DateTime.Now.ToString(), Session["UserId"].ToString(), DateTime.Now.ToString());
                    }
                }

                DisplayMessage("Record Updated");
                Reset();
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
            b = objSOrderHeader.InsertSOHeader(StrCompId, StrBrandId, StrLocationId, txtSONo.Text, txtSODate.Text, ddlPaymentMode.SelectedValue, txtEstimateDeliveryDate.Text, txtTransFrom.Text, hdnSalesQuotationId.Value, GetCustomerId(), strAddressId, txtTaxP.Text, txtTaxV.Text, txtDiscountP.Text, txtDiscountV.Text, txtShippingCharge.Text, txtRemark.Text, txtNetAmount.Text, "False", "", "", "", "", "", "True", DateTime.Now.ToString(), "True", Session["UserId"].ToString(), DateTime.Now.ToString(), Session["UserId"].ToString(), DateTime.Now.ToString());
            if (b != 0)
            {
                string strMaxId = string.Empty;
                DataTable dtMaxId = objSOrderHeader.GetMaxSalesOrderId(StrCompId, StrBrandId, StrLocationId);
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
                            HiddenField hdngvUnitId = (HiddenField)gvr.FindControl("hdngvUnitId");
                            HiddenField hdngvCurrencyId = (HiddenField)gvr.FindControl("hdnCurrencyId");
                            Label lblgvQuantity = (Label)gvr.FindControl("lblgvQuantity");
                            Label lblgvFreeQuantity = (Label)gvr.FindControl("lblgvFreeQuantity");
                            Label lblgvRemainQuantity = (Label)gvr.FindControl("lblgvRemainQuantity");
                            Label lblgvUnitPrice = (Label)gvr.FindControl("lblgvUnitPrice");
                            Label lblgvTaxP = (Label)gvr.FindControl("lblgvTaxP");
                            Label lblgvTaxV = (Label)gvr.FindControl("lblgvTaxV");
                            Label lblgvDiscountP = (Label)gvr.FindControl("lblgvDiscountP");
                            Label lblgvDiscountV = (Label)gvr.FindControl("lblgvDiscountV");

                            if (lblgvUnitPrice.Text == "")
                            {
                                lblgvUnitPrice.Text = "0";
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


                            ObjSOrderDetail.InsertSODetail(StrCompId, StrBrandId, StrLocationId, strMaxId, lblgvSerialNo.Text, hdngvProductId.Value, lblgvQuantity.Text, lblgvFreeQuantity.Text, lblgvRemainQuantity.Text, hdngvUnitId.Value, lblgvUnitPrice.Text, lblgvTaxP.Text, lblgvTaxV.Text, lblgvDiscountP.Text, lblgvDiscountV.Text, "", "", "", "", "", "True", DateTime.Now.ToString(), "True", Session["UserId"].ToString(), DateTime.Now.ToString(), Session["UserId"].ToString(), DateTime.Now.ToString());
                        }
                    }
                    else if (ddlOrderType.SelectedValue == "Q")
                    {
                        foreach (GridViewRow gvrQ in GvQuotationDetail.Rows)
                        {
                            Label lblgvSerialNo = (Label)gvrQ.FindControl("lblgvSerialNo");
                            HiddenField hdngvProductId = (HiddenField)gvrQ.FindControl("hdngvProductId");
                            DropDownList ddlgvUnit = (DropDownList)gvrQ.FindControl("ddlgvUnit");
                            HiddenField hdngvCurrencyId = (HiddenField)gvrQ.FindControl("hdnCurrencyId");
                            Label lblgvQuantity = (Label)gvrQ.FindControl("lblgvQuantity");
                            TextBox txtgvFreeQuantity = (TextBox)gvrQ.FindControl("txtgvFreeQuantity");
                            TextBox txtgvRemainQuantity = (TextBox)gvrQ.FindControl("txtgvRemainQuantity");
                            TextBox txtgvUnitPrice = (TextBox)gvrQ.FindControl("txtgvUnitPrice");
                            TextBox txtgvTaxP = (TextBox)gvrQ.FindControl("txtgvTaxP");
                            TextBox txtgvTaxV = (TextBox)gvrQ.FindControl("txtgvTaxV");
                            TextBox txtgvDiscountP = (TextBox)gvrQ.FindControl("txtgvDiscountP");
                            TextBox txtgvDiscountV = (TextBox)gvrQ.FindControl("txtgvDiscountV");

                            if (txtgvFreeQuantity.Text == "")
                            {
                                txtgvFreeQuantity.Text = "0";
                            }
                            if (txtgvRemainQuantity.Text == "")
                            {
                                txtgvRemainQuantity.Text = "0";
                            }
                            if (txtgvUnitPrice.Text == "")
                            {
                                txtgvUnitPrice.Text = "0";
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

                            string strUnitId = string.Empty;
                            if (ddlgvUnit.SelectedValue == "--Select--")
                            {
                                strUnitId = "0";
                            }
                            else
                            {
                                strUnitId = ddlgvUnit.SelectedValue;
                            }

                            ObjSOrderDetail.InsertSODetail(StrCompId, StrBrandId, StrLocationId, strMaxId, lblgvSerialNo.Text, hdngvProductId.Value, lblgvQuantity.Text, txtgvFreeQuantity.Text, txtgvRemainQuantity.Text, strUnitId, txtgvUnitPrice.Text, txtgvTaxP.Text, txtgvTaxV.Text, txtgvDiscountP.Text, txtgvDiscountV.Text, "", "", "", "", "", "True", DateTime.Now.ToString(), "True", Session["UserId"].ToString(), DateTime.Now.ToString(), Session["UserId"].ToString(), DateTime.Now.ToString());
                        }
                    }
                }
                DisplayMessage("Record Saved");
               
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
        b = objSOrderHeader.DeleteSOHeader(StrCompId, StrBrandId, StrLocationId, editid.Value, true.ToString(), StrUserId, DateTime.Now.ToString());
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
        FillGridBin();
    }
    protected void GvSalesOrderBin_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        GvSalesOrderBin.PageIndex = e.NewPageIndex;

        DataTable dt = (DataTable)Session["dtInactive"];
        GvSalesOrderBin.DataSource = dt;
        GvSalesOrderBin.DataBind();

        string temp = string.Empty;

        for (int i = 0; i < GvSalesOrderBin.Rows.Count; i++)
        {
            HiddenField lblconid = (HiddenField)GvSalesOrderBin.Rows[i].FindControl("hdnTransId");
            string[] split = lblSelectedRecord.Text.Split(',');

            for (int j = 0; j < lblSelectedRecord.Text.Split(',').Length; j++)
            {
                if (lblSelectedRecord.Text.Split(',')[j] != "")
                {
                    if (lblconid.Value.Trim().ToString() == lblSelectedRecord.Text.Split(',')[j].Trim().ToString())
                    {
                        ((CheckBox)GvSalesOrderBin.Rows[i].FindControl("chkSelect")).Checked = true;
                    }
                }
            }
        }
    }
    protected void GvSalesOrderBin_OnSorting(object sender, GridViewSortEventArgs e)
    {
        HDFSortbin.Value = HDFSortbin.Value == "ASC" ? "DESC" : "ASC";
        DataTable dt = new DataTable();
        dt = objSOrderHeader.GetSOHeaderAllFalse(StrCompId, StrBrandId, StrLocationId);
        DataView dv = new DataView(dt);
        string Query = "" + e.SortExpression + " " + HDFSortbin.Value + "";
        dv.Sort = Query;
        dt = dv.ToTable();
        Session["dtInactive"] = dt;
        GvSalesOrderBin.DataSource = dt;
        GvSalesOrderBin.DataBind();
        lblSelectedRecord.Text = "";
    }
    public void FillGridBin()
    {
        DataTable dt = new DataTable();
        dt = objSOrderHeader.GetSOHeaderAllFalse(StrCompId, StrBrandId, StrLocationId);
        GvSalesOrderBin.DataSource = dt;
        GvSalesOrderBin.DataBind();
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
            GvSalesOrderBin.DataSource = view.ToTable();
            GvSalesOrderBin.DataBind();
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

        if (GvSalesOrderBin.Rows.Count != 0)
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
                foreach (GridViewRow Gvr in GvSalesOrderBin.Rows)
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
        CheckBox chkSelAll = ((CheckBox)GvSalesOrderBin.HeaderRow.FindControl("chkCurrent"));
        for (int i = 0; i < GvSalesOrderBin.Rows.Count; i++)
        {
            ((CheckBox)GvSalesOrderBin.Rows[i].FindControl("chkSelect")).Checked = chkSelAll.Checked;
            if (chkSelAll.Checked)
            {
                if (!lblSelectedRecord.Text.Split(',').Contains(((HiddenField)(GvSalesOrderBin.Rows[i].FindControl("hdnTransId"))).Value.Trim().ToString()))
                {
                    lblSelectedRecord.Text += ((HiddenField)(GvSalesOrderBin.Rows[i].FindControl("hdnTransId"))).Value.Trim().ToString() + ",";
                }
            }
            else
            {
                string temp = string.Empty;
                string[] split = lblSelectedRecord.Text.Split(',');
                foreach (string item in split)
                {
                    if (item != ((HiddenField)(GvSalesOrderBin.Rows[i].FindControl("hdnTransId"))).Value.Trim().ToString())
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
        HiddenField lb = (HiddenField)GvSalesOrderBin.Rows[index].FindControl("hdnTransId");
        if (((CheckBox)GvSalesOrderBin.Rows[index].FindControl("chkSelect")).Checked)
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
            for (int i = 0; i < GvSalesOrderBin.Rows.Count; i++)
            {
                string[] split = lblSelectedRecord.Text.Split(',');
                HiddenField lblconid = (HiddenField)GvSalesOrderBin.Rows[i].FindControl("hdnTransId");
                for (int j = 0; j < lblSelectedRecord.Text.Split(',').Length; j++)
                {
                    if (lblSelectedRecord.Text.Split(',')[j] != "")
                    {
                        if (lblconid.Value.Trim().ToString() == lblSelectedRecord.Text.Split(',')[j].Trim().ToString())
                        {
                            ((CheckBox)GvSalesOrderBin.Rows[i].FindControl("chkSelect")).Checked = true;
                        }
                    }
                }
            }
        }
        else
        {
            lblSelectedRecord.Text = "";
            DataTable dtUnit1 = (DataTable)Session["dtInactive"];
            GvSalesOrderBin.DataSource = dtUnit1;
            GvSalesOrderBin.DataBind();
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
                    b = objSOrderHeader.DeleteSOHeader(StrCompId, StrBrandId, StrLocationId, lblSelectedRecord.Text.Split(',')[j].Trim(), true.ToString(), Session["UserId"].ToString(), DateTime.Now.ToString());
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
            foreach (GridViewRow Gvr in GvSalesOrderBin.Rows)
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

    #region User defind Funcation
    private void FillGrid()
    {
        DataTable dtBrand = objSOrderHeader.GetSOHeaderAllTrue(StrCompId, StrBrandId, StrLocationId);
        lblTotalRecords.Text = Resources.Attendance.Total_Records + " : " + dtBrand.Rows.Count + "";
        Session["dtCurrency"] = dtBrand;
        Session["dtFilter"] = dtBrand;
        if (dtBrand != null && dtBrand.Rows.Count > 0)
        {
            GvSalesOrder.DataSource = dtBrand;
            GvSalesOrder.DataBind();
        }
        else
        {
            GvSalesOrder.DataSource = null;
            GvSalesOrder.DataBind();
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
        txtSONo.Text = objSOrderHeader.GetAutoID(StrCompId, StrBrandId, StrLocationId);
        txtSODate.Text = DateTime.Now.ToString(ObjSysParam.GetSysParameterByParamName("Date_Format").Rows[0]["Param_Value"].ToString());
        txtSONo.ReadOnly = false;
        txtCustomer.Text = "";
        ddlOrderType.SelectedValue = "--Select--";
        txtTransFrom.Text = "";
        FillPaymentMode();
        txtEstimateDeliveryDate.Text = "";
        txtShipingAddress.Text = "";
        txtAmount.Text = "";
        txtTaxP.Text = "";
        txtTaxV.Text = "";
        txtPriceAfterTax.Text = "";
        txtDiscountP.Text = "";
        txtDiscountV.Text = "";
        txtTotalAmount.Text = "";
        txtShippingCharge.Text = "";
        txtNetAmount.Text = "";
        txtRemark.Text = "";

        GvQuotationDetail.DataSource = null;
        GvQuotationDetail.DataBind();

        GvProductDetail.DataSource = null;
        GvProductDetail.DataBind();

        FillRequestGrid();
        FillGrid();

        hdnSalesQuotationId.Value = "0";


        editid.Value = "";
        btnNew.Text = Resources.Attendance.New;
        ddlFieldName.SelectedIndex = 1;
        ddlOption.SelectedIndex = 2;
        txtValue.Text = "";
        txtValueBin.Text = "";
        lblSelectedRecord.Text = "";
        System.Web.UI.ScriptManager.GetCurrent(this).SetFocus(txtValue);
        txtSONo.Text = GetDocumentNumber();
    }
    #endregion

    #region Add Request Section
    private void FillUnit()
    {
        foreach (GridViewRow gvr in GvQuotationDetail.Rows)
        {
            DropDownList ddlUnit = (DropDownList)gvr.FindControl("ddlgvUnit");

            DataTable dsUnit = null;
            dsUnit = UM.GetUnitMaster(StrCompId);
            if (dsUnit.Rows.Count > 0)
            {
                ddlUnit.DataSource = dsUnit;
                ddlUnit.DataTextField = "Unit_Code";
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
    }
    private void FillQuotationNo()
    {
        DataTable dsQuotationNo = null;
        dsQuotationNo = objSQuoteHeader.GetDataForSalesOrder(StrCompId, StrBrandId, StrLocationId);
        if (dsQuotationNo.Rows.Count > 0)
        {
            ddlQuotationNo.DataSource = dsQuotationNo;
            ddlQuotationNo.DataTextField = "SQuotation_No";
            ddlQuotationNo.DataValueField = "SQuotation_Id";
            ddlQuotationNo.DataBind();

            ddlQuotationNo.Items.Add("--Select--");
            ddlQuotationNo.SelectedValue = "--Select--";
        }
        else
        {
            ddlQuotationNo.Items.Add("--Select--");
            ddlQuotationNo.SelectedValue = "--Select--";
        }
    }
    protected void GvSalesQuotation_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        GvSalesQuotation.PageIndex = e.NewPageIndex;
        DataTable dt = (DataTable)Session["dtPRequest"];
        GvSalesQuotation.DataSource = dt;
        GvSalesQuotation.DataBind();
    }
    protected void GvSalesQuotation_Sorting(object sender, GridViewSortEventArgs e)
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
        GvSalesQuotation.DataSource = dt;
        GvSalesQuotation.DataBind();
    }
    private void FillRequestGrid()
    {
        DataTable dtPRequest = objSQuoteHeader.GetDataForSalesOrder(StrCompId, StrBrandId, StrLocationId);
        lblTotalRecords.Text = Resources.Attendance.Total_Records + " : " + dtPRequest.Rows.Count + "";
        Session["dtPRequest"] = dtPRequest;
        if (dtPRequest != null && dtPRequest.Rows.Count > 0)
        {
            GvSalesQuotation.DataSource = dtPRequest;
            GvSalesQuotation.DataBind();
        }
        else
        {
            GvSalesQuotation.DataSource = null;
            GvSalesQuotation.DataBind();
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
    protected string GetTransType(string strTransType)
    {
        string strTransName = string.Empty;
        if (strTransType == "S")
        {
            strTransName = "Sales Quotation";
        }
        else if (strTransType == "")
        {
            strTransName = "Direct";
        }
        return strTransName;
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
        string strRequestId = e.CommandArgument.ToString();

        DataTable dtPRequest = objSQuoteHeader.GetQuotationHeaderAllBySQuotationId(StrCompId, StrBrandId, StrLocationId, strRequestId);

        if (dtPRequest.Rows.Count > 0)
        {
            trTransfer.Visible = true;
            ddlOrderType.SelectedValue = "Q";
            txtTransFrom.Text = "Sales Quotation";
            txtQuotationNo.Visible = true;
            ddlQuotationNo.Visible = false;
            txtQuotationNo.Text = dtPRequest.Rows[0]["SQuotation_No"].ToString();
            hdnSalesQuotationId.Value = dtPRequest.Rows[0]["SQuotation_Id"].ToString();

            string strInquiryNo = dtPRequest.Rows[0]["SInquiry_No"].ToString();
            if (strInquiryNo != "0" && strInquiryNo != "")
            {
                DataTable dtSInquiryData = objSInquiryHeader.GetSIHeaderAllBySInquiryId(StrCompId, StrBrandId, StrLocationId, strInquiryNo);
                if (dtSInquiryData.Rows.Count > 0)
                {
                    string strCustomerId = dtSInquiryData.Rows[0]["Customer_Id"].ToString();
                    if (strCustomerId != "0" && strCustomerId != "")
                    {
                        txtCustomer.Text = GetCustomerName(dtSInquiryData.Rows[0]["Customer_Id"].ToString()) + "/" + strCustomerId;
                    }
                }
            }

            //Add Detail Grid
            DataTable dtDetail = ObjSQuoteDetail.GetQuotationDetailBySQuotation_Id(StrCompId, StrBrandId, StrLocationId, strRequestId);
            if (dtDetail.Rows.Count > 0)
            {
                GvQuotationDetail.DataSource = dtDetail;
                GvQuotationDetail.DataBind();
                FillUnit();

                foreach (GridViewRow gvr in GvQuotationDetail.Rows)
                {
                    HiddenField hdngvProductId = (HiddenField)gvr.FindControl("hdngvProductId");
                    TextBox txtgvUnitPrice = (TextBox)gvr.FindControl("txtgvUnitPrice");
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
                                txtgvUnitPrice.Text = dtDetail.Rows[i]["UnitPrice"].ToString();
                                if (txtgvUnitPrice.Text != "0.000" && txtgvUnitPrice.Text != "")
                                {
                                    txtgvUnitPrice_TextChanged(sender, e);
                                }
                                txtgvTaxP.Text = dtDetail.Rows[i]["TaxPercent"].ToString();
                                if (txtgvTaxP.Text != "0.000" && txtgvTaxP.Text != "")
                                {
                                    txtgvTaxP_TextChanged(sender, e);
                                }
                                txtgvTaxV.Text = dtDetail.Rows[i]["TaxValue"].ToString();
                                if (txtgvTaxV.Text != "0.000" && txtgvTaxV.Text != "")
                                {
                                    txtgvTaxV_TextChanged(sender, e);
                                }

                                txtgvPriceAfterTax.Text = dtDetail.Rows[i]["PriceAfterTax"].ToString();
                                txtgvDiscountP.Text = dtDetail.Rows[i]["DiscountPercent"].ToString();
                                if (txtgvDiscountP.Text != "0.000" && txtgvDiscountP.Text != "")
                                {
                                    txtgvDiscountP_TextChanged(sender, e);
                                }

                                txtgvDiscountV.Text = dtDetail.Rows[i]["DiscountValue"].ToString();
                                if (txtgvDiscountV.Text != "0.000" && txtgvDiscountV.Text != "")
                                {
                                    txtgvDiscountV_TextChanged(sender, e);
                                }

                                txtgvPriceAfterDiscount.Text = dtDetail.Rows[i]["PriceAfterDiscount"].ToString();
                            }
                        }
                    }
                }
            }

            txtTaxP.Text = dtPRequest.Rows[0]["TaxPercent"].ToString();
            if (txtTaxP.Text != "0.000" && txtTaxP.Text != "")
            {
                txtTaxP_TextChanged(sender, e);
            }
            txtTaxV.Text = dtPRequest.Rows[0]["TaxValue"].ToString();
            if (txtTaxV.Text != "0.000" && txtTaxV.Text != "")
            {
                txtTaxV_TextChanged(sender, e);
            }
            txtDiscountP.Text = dtPRequest.Rows[0]["DiscountPercent"].ToString();
            if (txtDiscountP.Text != "0.000" && txtDiscountP.Text != "")
            {
                txtDiscountP_TextChanged(sender, e);
            }
            txtDiscountV.Text = dtPRequest.Rows[0]["DiscountValue"].ToString();
            if (txtDiscountV.Text != "0.000" && txtDiscountV.Text != "")
            {
                txtDiscountV_TextChanged(sender, e);
            }
        }
        btnNew_Click(null, null);
        System.Web.UI.ScriptManager.GetCurrent(this).SetFocus(txtSONo);
    }
    protected void GvQuotationDetail_RowCreated(object sender, GridViewRowEventArgs e)
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
            cell.Text = Resources.Attendance.Quantity;
            row.Controls.Add(cell);

            cell = new TableHeaderCell();
            cell.ColumnSpan = 1;
            cell.Text = Resources.Attendance.Unit;
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
                            txtPriceAfterTax.Text = (float.Parse(txtAmount.Text) + float.Parse(txtTaxV.Text)).ToString();
                        }
                        if (txtDiscountP.Text != "")
                        {
                            txtDiscountP_TextChanged(sender, e);
                        }
                        else
                        {
                            txtTotalAmount.Text = txtPriceAfterTax.Text;
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
                            txtPriceAfterTax.Text = (float.Parse(txtAmount.Text) + float.Parse(txtTaxV.Text)).ToString();
                            txtAmount.Text = (float.Parse(txtAmount.Text) + float.Parse(txtPriceAfterTax.Text)).ToString();
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
                        if (txtPriceAfterTax.Text != "")
                        {
                            if (txtPriceAfterTax.Text == "0")
                            {
                                txtDiscountV.Text = ((float.Parse(txtAmount.Text) * float.Parse(txtDiscountP.Text)) / 100).ToString();
                                txtTotalAmount.Text = (float.Parse(txtAmount.Text) - float.Parse(txtDiscountV.Text)).ToString();
                            }
                            else
                            {
                                txtDiscountV.Text = ((float.Parse(txtPriceAfterTax.Text) * float.Parse(txtDiscountP.Text)) / 100).ToString();
                                txtTotalAmount.Text = (float.Parse(txtPriceAfterTax.Text) - float.Parse(txtDiscountV.Text)).ToString();
                            }
                            System.Web.UI.ScriptManager.GetCurrent(this).SetFocus(txtShippingCharge);
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
                        if (txtPriceAfterTax.Text != "")
                        {
                            if (txtPriceAfterTax.Text == "0")
                            {
                                txtDiscountP.Text = ((100 * float.Parse(txtDiscountV.Text)) / float.Parse(txtAmount.Text)).ToString();
                                txtTotalAmount.Text = (float.Parse(txtAmount.Text) - float.Parse(txtDiscountV.Text)).ToString();
                            }
                            else
                            {
                                txtDiscountP.Text = ((100 * float.Parse(txtDiscountV.Text)) / float.Parse(txtPriceAfterTax.Text)).ToString();
                                txtTotalAmount.Text = (float.Parse(txtPriceAfterTax.Text) - float.Parse(txtDiscountV.Text)).ToString();
                            }
                            System.Web.UI.ScriptManager.GetCurrent(this).SetFocus(txtShippingCharge);
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
        foreach (GridViewRow gvr in GvQuotationDetail.Rows)
        {
            TextBox txtgvUnitPrice = (TextBox)gvr.FindControl("txtgvUnitPrice");
            TextBox txtgvTaxP = (TextBox)gvr.FindControl("txtgvTaxP");
            TextBox txtgvTaxV = (TextBox)gvr.FindControl("txtgvTaxV");
            TextBox txtgvPriceAfterTax = (TextBox)gvr.FindControl("txtgvPriceAfterTax");
            TextBox txtgvDiscountP = (TextBox)gvr.FindControl("txtgvDiscountP");
            TextBox txtgvDiscountV = (TextBox)gvr.FindControl("txtgvDiscountV");
            TextBox txtgvPriceAfterDiscount = (TextBox)gvr.FindControl("txtgvPriceAfterDiscount");
            TextBox txtgvTotal = (TextBox)gvr.FindControl("txtgvTotal");

            if (txtgvUnitPrice.Text != "")
            {
                float flTemp = 0;
                if (float.TryParse(txtgvUnitPrice.Text, out flTemp))
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
                        txtgvTotal.Text = (float.Parse(txtgvUnitPrice.Text) + float.Parse(txtgvPriceAfterTax.Text)).ToString();
                    }
                    System.Web.UI.ScriptManager.GetCurrent(this).SetFocus(txtTaxP);
                }
                else
                {
                    txtgvUnitPrice.Text = "";
                    ScriptManager.RegisterStartupScript(this, typeof(string), "OPEN_WINDOW", "window.alert('Enter Numeric Value Only');", true);
                    System.Web.UI.ScriptManager.GetCurrent(this).SetFocus(txtgvUnitPrice);
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
        foreach (GridViewRow gvr in GvQuotationDetail.Rows)
        {
            TextBox txtgvUnitPrice = (TextBox)gvr.FindControl("txtgvUnitPrice");
            TextBox txtgvTaxP = (TextBox)gvr.FindControl("txtgvTaxP");
            TextBox txtgvTaxV = (TextBox)gvr.FindControl("txtgvTaxV");
            TextBox txtgvPriceAfterTax = (TextBox)gvr.FindControl("txtgvPriceAfterTax");
            TextBox txtgvDiscountP = (TextBox)gvr.FindControl("txtgvDiscountP");
            TextBox txtgvDiscountV = (TextBox)gvr.FindControl("txtgvDiscountV");
            TextBox txtgvPriceAfterDiscount = (TextBox)gvr.FindControl("txtgvPriceAfterDiscount");
            TextBox txtgvTotal = (TextBox)gvr.FindControl("txtgvTotal");

            if (txtgvUnitPrice.Text != "")
            {
                float flTemp = 0;
                if (float.TryParse(txtgvUnitPrice.Text, out flTemp))
                {
                    if (txtgvTaxP.Text != "")
                    {
                        if (float.TryParse(txtgvTaxP.Text, out flTemp))
                        {
                            txtgvTaxV.Text = ((float.Parse(txtgvUnitPrice.Text) * float.Parse(txtgvTaxP.Text)) / 100).ToString();
                            if (txtgvUnitPrice.Text != "")
                            {
                                txtgvPriceAfterTax.Text = (float.Parse(txtgvUnitPrice.Text) + float.Parse(txtgvTaxV.Text)).ToString();
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
                    txtgvUnitPrice.Text = "";
                    ScriptManager.RegisterStartupScript(this, typeof(string), "OPEN_WINDOW", "window.alert('Enter Numeric Value Only');", true);
                    System.Web.UI.ScriptManager.GetCurrent(this).SetFocus(txtgvUnitPrice);
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
        foreach (GridViewRow gvr in GvQuotationDetail.Rows)
        {
            TextBox txtgvUnitPrice = (TextBox)gvr.FindControl("txtgvUnitPrice");
            TextBox txtgvTaxP = (TextBox)gvr.FindControl("txtgvTaxP");
            TextBox txtgvTaxV = (TextBox)gvr.FindControl("txtgvTaxV");
            TextBox txtgvPriceAfterTax = (TextBox)gvr.FindControl("txtgvPriceAfterTax");
            TextBox txtgvDiscountP = (TextBox)gvr.FindControl("txtgvDiscountP");
            TextBox txtgvDiscountV = (TextBox)gvr.FindControl("txtgvDiscountV");
            TextBox txtgvPriceAfterDiscount = (TextBox)gvr.FindControl("txtgvPriceAfterDiscount");
            TextBox txtgvTotal = (TextBox)gvr.FindControl("txtgvTotal");

            if (txtgvUnitPrice.Text != "")
            {
                float flTemp = 0;
                if (float.TryParse(txtgvUnitPrice.Text, out flTemp))
                {
                    if (txtgvTaxV.Text != "")
                    {
                        if (float.TryParse(txtgvTaxV.Text, out flTemp))
                        {
                            txtgvTaxP.Text = ((100 * float.Parse(txtgvTaxV.Text)) / float.Parse(txtgvUnitPrice.Text)).ToString();
                            if (txtgvUnitPrice.Text != "")
                            {
                                txtgvPriceAfterTax.Text = (float.Parse(txtgvUnitPrice.Text) + float.Parse(txtgvTaxV.Text)).ToString();
                                txtgvTotal.Text = (float.Parse(txtgvUnitPrice.Text) + float.Parse(txtgvPriceAfterTax.Text)).ToString();
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
                    txtgvUnitPrice.Text = "";
                    ScriptManager.RegisterStartupScript(this, typeof(string), "OPEN_WINDOW", "window.alert('Enter Numeric Value Only');", true);
                    System.Web.UI.ScriptManager.GetCurrent(this).SetFocus(txtgvUnitPrice);
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
        foreach (GridViewRow gvr in GvQuotationDetail.Rows)
        {
            TextBox txtgvUnitPrice = (TextBox)gvr.FindControl("txtgvUnitPrice");
            TextBox txtgvTaxP = (TextBox)gvr.FindControl("txtgvTaxP");
            TextBox txtgvTaxV = (TextBox)gvr.FindControl("txtgvTaxV");
            TextBox txtgvPriceAfterTax = (TextBox)gvr.FindControl("txtgvPriceAfterTax");
            TextBox txtgvDiscountP = (TextBox)gvr.FindControl("txtgvDiscountP");
            TextBox txtgvDiscountV = (TextBox)gvr.FindControl("txtgvDiscountV");
            TextBox txtgvPriceAfterDiscount = (TextBox)gvr.FindControl("txtgvPriceAfterDiscount");
            TextBox txtgvTotal = (TextBox)gvr.FindControl("txtgvTotal");

            if (txtgvUnitPrice.Text != "")
            {
                float flTemp = 0;
                if (float.TryParse(txtgvUnitPrice.Text, out flTemp))
                {
                    if (txtgvDiscountP.Text != "")
                    {
                        if (float.TryParse(txtgvDiscountP.Text, out flTemp))
                        {
                            if (txtgvPriceAfterTax.Text != "")
                            {
                                if (txtgvPriceAfterTax.Text == "0")
                                {
                                    txtgvDiscountV.Text = ((float.Parse(txtgvUnitPrice.Text) * float.Parse(txtgvDiscountP.Text)) / 100).ToString();
                                    txtgvPriceAfterDiscount.Text = (float.Parse(txtgvUnitPrice.Text) - float.Parse(txtgvDiscountV.Text)).ToString();
                                    txtgvTotal.Text = (float.Parse(txtgvUnitPrice.Text) - float.Parse(txtgvDiscountV.Text)).ToString();
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
                    txtgvUnitPrice.Text = "";
                    ScriptManager.RegisterStartupScript(this, typeof(string), "OPEN_WINDOW", "window.alert('Enter Numeric Value Only');", true);
                    System.Web.UI.ScriptManager.GetCurrent(this).SetFocus(txtgvUnitPrice);
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
        foreach (GridViewRow gvr in GvQuotationDetail.Rows)
        {
            TextBox txtgvUnitPrice = (TextBox)gvr.FindControl("txtgvUnitPrice");
            TextBox txtgvTaxP = (TextBox)gvr.FindControl("txtgvTaxP");
            TextBox txtgvTaxV = (TextBox)gvr.FindControl("txtgvTaxV");
            TextBox txtgvPriceAfterTax = (TextBox)gvr.FindControl("txtgvPriceAfterTax");
            TextBox txtgvDiscountP = (TextBox)gvr.FindControl("txtgvDiscountP");
            TextBox txtgvDiscountV = (TextBox)gvr.FindControl("txtgvDiscountV");
            TextBox txtgvPriceAfterDiscount = (TextBox)gvr.FindControl("txtgvPriceAfterDiscount");
            TextBox txtgvTotal = (TextBox)gvr.FindControl("txtgvTotal");

            if (txtgvUnitPrice.Text != "")
            {
                float flTemp = 0;
                if (float.TryParse(txtgvUnitPrice.Text, out flTemp))
                {
                    if (txtgvDiscountV.Text != "")
                    {
                        if (float.TryParse(txtgvDiscountV.Text, out flTemp))
                        {
                            if (txtgvPriceAfterTax.Text != "")
                            {
                                if (txtgvPriceAfterTax.Text == "0")
                                {
                                    txtgvDiscountP.Text = ((100 * float.Parse(txtgvDiscountV.Text)) / float.Parse(txtgvUnitPrice.Text)).ToString();
                                    txtgvPriceAfterDiscount.Text = (float.Parse(txtgvUnitPrice.Text) - float.Parse(txtgvDiscountV.Text)).ToString();
                                    txtgvTotal.Text = (float.Parse(txtgvUnitPrice.Text) - float.Parse(txtgvDiscountV.Text)).ToString();
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
                    txtgvUnitPrice.Text = "";
                    ScriptManager.RegisterStartupScript(this, typeof(string), "OPEN_WINDOW", "window.alert('Enter Numeric Value Only');", true);
                    System.Web.UI.ScriptManager.GetCurrent(this).SetFocus(txtgvUnitPrice);
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
                    txtPriceAfterTax.Text = "0";
                }
                if (txtDiscountP.Text != "")
                {
                    txtDiscountP_TextChanged(sender, e);
                }
                else
                {
                    txtTotalAmount.Text = (float.Parse(txtAmount.Text) + float.Parse(txtPriceAfterTax.Text)).ToString();
                }

                if (txtShippingCharge.Text != "")
                {
                    txtShippingCharge_TextChanged(sender, e);
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
            GvQuotationDetail.DataSource = null;
            GvQuotationDetail.DataBind();
            txtAmount.Text = "";
            txtTaxP.Text = "";
            txtTaxV.Text = "";
            txtPriceAfterTax.Text = "";
            txtDiscountP.Text = "";
            txtDiscountV.Text = "";
            txtTotalAmount.Text = "";
            btnAddNewProduct.Visible = false;
        }
        else if (ddlOrderType.SelectedValue == "D")
        {
            trTransfer.Visible = false;
            txtTransFrom.Text = "";
            GvQuotationDetail.DataSource = null;
            GvQuotationDetail.DataBind();
            txtAmount.Text = "";
            txtTaxP.Text = "";
            txtTaxV.Text = "";
            txtPriceAfterTax.Text = "";
            txtDiscountP.Text = "";
            txtDiscountV.Text = "";
            txtTotalAmount.Text = "";
            btnAddNewProduct.Visible = true;
        }
        else if (ddlOrderType.SelectedValue == "Q")
        {
            trTransfer.Visible = true;
            txtTransFrom.Text = "Sales Quotation";
            ddlQuotationNo.Visible = true;
            txtQuotationNo.Visible = false;
            FillQuotationNo();
            btnAddNewProduct.Visible = false;
        }
    }
    protected void ddlQuotationNo_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (ddlQuotationNo.SelectedValue != "--Select--")
        {
            DataTable dtPRequest = objSQuoteHeader.GetQuotationHeaderAllBySQuotationId(StrCompId, StrBrandId, StrLocationId, ddlQuotationNo.SelectedValue);

            if (dtPRequest.Rows.Count > 0)
            {
                //txtQuotationNo.Text = dtPRequest.Rows[0]["SQuotation_No"].ToString();
                hdnSalesQuotationId.Value = dtPRequest.Rows[0]["SQuotation_Id"].ToString();

                string strInquiryNo = dtPRequest.Rows[0]["SInquiry_No"].ToString();
                if (strInquiryNo != "0" && strInquiryNo != "")
                {
                    DataTable dtSInquiryData = objSInquiryHeader.GetSIHeaderAllBySInquiryId(StrCompId, StrBrandId, StrLocationId, strInquiryNo);
                    if (dtSInquiryData.Rows.Count > 0)
                    {
                        string strCustomerId = dtSInquiryData.Rows[0]["Customer_Id"].ToString();
                        if (strCustomerId != "0" && strCustomerId != "")
                        {
                            txtCustomer.Text = GetCustomerName(dtSInquiryData.Rows[0]["Customer_Id"].ToString()) + "/" + strCustomerId;
                        }
                    }
                }

                //Add Detail Grid
                DataTable dtDetail = ObjSQuoteDetail.GetQuotationDetailBySQuotation_Id(StrCompId, StrBrandId, StrLocationId, hdnSalesQuotationId.Value);
                if (dtDetail.Rows.Count > 0)
                {
                    GvQuotationDetail.DataSource = dtDetail;
                    GvQuotationDetail.DataBind();
                    FillUnit();

                    foreach (GridViewRow gvr in GvQuotationDetail.Rows)
                    {
                        HiddenField hdngvProductId = (HiddenField)gvr.FindControl("hdngvProductId");
                        TextBox txtgvUnitPrice = (TextBox)gvr.FindControl("txtgvUnitPrice");
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
                                    txtgvUnitPrice.Text = dtDetail.Rows[i]["UnitPrice"].ToString();
                                    if (txtgvUnitPrice.Text != "0.000" && txtgvUnitPrice.Text != "")
                                    {
                                        txtgvUnitPrice_TextChanged(sender, e);
                                    }
                                    txtgvTaxP.Text = dtDetail.Rows[i]["TaxPercent"].ToString();
                                    if (txtgvTaxP.Text != "0.000" && txtgvTaxP.Text != "")
                                    {
                                        txtgvTaxP_TextChanged(sender, e);
                                    }
                                    txtgvTaxV.Text = dtDetail.Rows[i]["TaxValue"].ToString();
                                    if (txtgvTaxV.Text != "0.000" && txtgvTaxV.Text != "")
                                    {
                                        txtgvTaxV_TextChanged(sender, e);
                                    }

                                    txtgvPriceAfterTax.Text = dtDetail.Rows[i]["PriceAfterTax"].ToString();
                                    txtgvDiscountP.Text = dtDetail.Rows[i]["DiscountPercent"].ToString();
                                    if (txtgvDiscountP.Text != "0.000" && txtgvDiscountP.Text != "")
                                    {
                                        txtgvDiscountP_TextChanged(sender, e);
                                    }

                                    txtgvDiscountV.Text = dtDetail.Rows[i]["DiscountValue"].ToString();
                                    if (txtgvDiscountV.Text != "0.000" && txtgvDiscountV.Text != "")
                                    {
                                        txtgvDiscountV_TextChanged(sender, e);
                                    }

                                    txtgvPriceAfterDiscount.Text = dtDetail.Rows[i]["PriceAfterDiscount"].ToString();
                                }
                            }
                        }
                    }
                }

                txtTaxP.Text = dtPRequest.Rows[0]["TaxPercent"].ToString();
                if (txtTaxP.Text != "0.000" && txtTaxP.Text != "")
                {
                    txtTaxP_TextChanged(sender, e);
                }
                txtTaxV.Text = dtPRequest.Rows[0]["TaxValue"].ToString();
                if (txtTaxV.Text != "0.000" && txtTaxV.Text != "")
                {
                    txtTaxV_TextChanged(sender, e);
                }
                txtDiscountP.Text = dtPRequest.Rows[0]["DiscountPercent"].ToString();
                if (txtDiscountP.Text != "0.000" && txtDiscountP.Text != "")
                {
                    txtDiscountP_TextChanged(sender, e);
                }
                txtDiscountV.Text = dtPRequest.Rows[0]["DiscountValue"].ToString();
                if (txtDiscountV.Text != "0.000" && txtDiscountV.Text != "")
                {
                    txtDiscountV_TextChanged(sender, e);
                }
            }
            btnNew_Click(null, null);
            System.Web.UI.ScriptManager.GetCurrent(this).SetFocus(txtSONo);
        }
        else
        {
            GvQuotationDetail.DataSource = null;
            GvQuotationDetail.DataBind();
            txtAmount.Text = "";
            txtTaxP.Text = "";
            txtTaxV.Text = "";
            txtPriceAfterTax.Text = "";
            txtDiscountP.Text = "";
            txtDiscountV.Text = "";
            txtTotalAmount.Text = "";
        }
    }
    protected void txtShippingCharge_TextChanged(object sender, EventArgs e)
    {
        if (txtShippingCharge.Text != "")
        {
            float flTemp = 0;
            if (float.TryParse(txtShippingCharge.Text, out flTemp))
            {
                if (txtTotalAmount.Text != "")
                {
                    txtNetAmount.Text = (float.Parse(txtTotalAmount.Text) + float.Parse(txtShippingCharge.Text)).ToString();
                }
                else
                {
                    txtNetAmount.Text = txtShippingCharge.Text;
                }
            }
            else
            {
                txtShippingCharge.Text = "";
                ScriptManager.RegisterStartupScript(this, typeof(string), "OPEN_WINDOW", "window.alert('Enter Numeric Value Only');", true);
                System.Web.UI.ScriptManager.GetCurrent(this).SetFocus(txtShippingCharge);
            }
        }
        else
        {

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
            cell.Text = Resources.Attendance.Quantity;
            row.Controls.Add(cell);

            cell = new TableHeaderCell();
            cell.ColumnSpan = 1;
            cell.Text = Resources.Attendance.Unit;
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
    private void FillProductCurrency()
    {
        DataTable dsPCurrency = null;
        dsPCurrency = objCurrency.GetCurrencyMaster();
        if (dsPCurrency.Rows.Count > 0)
        {
            ddlPCurrency.DataSource = dsPCurrency;
            ddlPCurrency.DataTextField = "Currency_Name";
            ddlPCurrency.DataValueField = "Currency_ID";
            ddlPCurrency.DataBind();

            ddlPCurrency.Items.Add("--Select--");
            ddlPCurrency.SelectedValue = "--Select--";
        }
        else
        {
            ddlPCurrency.Items.Add("--Select--");
            ddlPCurrency.SelectedValue = "--Select--";
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
                    FillUnit();
                }
                txtPDescription.Text = dt.Rows[0]["Description"].ToString();
                hdnNewProductId.Value = dt.Rows[0]["ProductId"].ToString();
                System.Web.UI.ScriptManager.GetCurrent(this).SetFocus(ddlPCurrency);
            }
            else
            {
                FillProductUnit();
                txtPDescription.Text = "";
                hdnNewProductId.Value = "0";
            }
            txtPFreeQuantity.Text = "0";
            txtPRemainQuantity.Text = "0";
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

            if (txtPQuantity.Text != "")
            {

            }
            else
            {
                DisplayMessage("Enter Required Quantity");
                System.Web.UI.ScriptManager.GetCurrent(this).SetFocus(txtPQuantity);
                return;
            }

            if (ddlPCurrency.SelectedValue != "--Select--")
            {
                hdnCurrencyId.Value = ddlPCurrency.SelectedValue;
            }
            else
            {
                DisplayMessage("Select Currency Name");
                System.Web.UI.ScriptManager.GetCurrent(this).SetFocus(ddlPCurrency);
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
            if (txtPFreeQuantity.Text == "")
            {
                txtPFreeQuantity.Text = "0";
            }
            if (txtPRemainQuantity.Text == "")
            {
                txtPRemainQuantity.Text = "0";
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
        foreach (GridViewRow gvr in GvProductDetail.Rows)
        {
            Label lblgvTotal = (Label)gvr.FindControl("lblgvTotal");

            if (lblgvTotal.Text != "")
            {
                fGrossTotal = fGrossTotal + float.Parse(lblgvTotal.Text);
            }
        }
        txtAmount.Text = fGrossTotal.ToString();
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
        txtPDescription.Text = "";
        FillProductCurrency();
        txtPQuantity.Text = "1";
        txtPFreeQuantity.Text = "";
        txtPRemainQuantity.Text = "";
        txtPUnitPrice.Text = "";
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
        dt.Columns.Add("UnitId");
        dt.Columns.Add("Quantity");
        dt.Columns.Add("FreeQty");
        dt.Columns.Add("RemainQty");
        dt.Columns.Add("UnitPrice");
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
                    HiddenField hdngvUnitId = (HiddenField)GvProductDetail.Rows[i].FindControl("hdngvUnitId");
                    Label lblgvQuantity = (Label)GvProductDetail.Rows[i].FindControl("lblgvQuantity");
                    Label lblgvFreeQuantity = (Label)GvProductDetail.Rows[i].FindControl("lblgvFreeQuantity");
                    Label lblgvRemainQuantity = (Label)GvProductDetail.Rows[i].FindControl("lblgvRemainQuantity");
                    Label lblgvUnitPrice = (Label)GvProductDetail.Rows[i].FindControl("lblgvUnitPrice");
                    Label lblgvTaxP = (Label)GvProductDetail.Rows[i].FindControl("lblgvTaxP");
                    Label lblgvTaxV = (Label)GvProductDetail.Rows[i].FindControl("lblgvTaxV");
                    Label lblgvDiscountP = (Label)GvProductDetail.Rows[i].FindControl("lblgvDiscountP");
                    Label lblgvDiscountV = (Label)GvProductDetail.Rows[i].FindControl("lblgvDiscountV");

                    dt.Rows[i]["Serial_No"] = lblgvSNo.Text;
                    strNewSNo = lblgvSNo.Text;
                    dt.Rows[i]["Product_Id"] = hdngvProductId.Value;
                    dt.Rows[i]["UnitId"] = hdngvUnitId.Value;
                    dt.Rows[i]["Quantity"] = lblgvQuantity.Text;
                    dt.Rows[i]["FreeQty"] = lblgvFreeQuantity.Text;
                    dt.Rows[i]["RemainQty"] = lblgvRemainQuantity.Text;
                    dt.Rows[i]["UnitPrice"] = lblgvUnitPrice.Text;
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
                    dt.Rows[i]["UnitId"] = hdnUnitId.Value;
                    dt.Rows[i]["Quantity"] = txtPQuantity.Text;
                    dt.Rows[i]["FreeQty"] = txtPFreeQuantity.Text;
                    dt.Rows[i]["RemainQty"] = txtPRemainQuantity.Text;
                    dt.Rows[i]["UnitPrice"] = txtPUnitPrice.Text;
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
            dt.Rows[0]["UnitId"] = hdnUnitId.Value;
            dt.Rows[0]["Quantity"] = txtPQuantity.Text;
            dt.Rows[0]["FreeQty"] = txtPFreeQuantity.Text;
            dt.Rows[0]["RemainQty"] = txtPRemainQuantity.Text;
            dt.Rows[0]["UnitPrice"] = txtPUnitPrice.Text;
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
            HiddenField hdngvUnitId = (HiddenField)GvProductDetail.Rows[i].FindControl("hdngvUnitId");
            Label lblgvQuantity = (Label)GvProductDetail.Rows[i].FindControl("lblgvQuantity");
            Label lblgvFreeQuantity = (Label)GvProductDetail.Rows[i].FindControl("lblgvFreeQuantity");
            Label lblgvRemainQuantity = (Label)GvProductDetail.Rows[i].FindControl("lblgvRemainQuantity");
            Label lblgvUnitPrice = (Label)GvProductDetail.Rows[i].FindControl("lblgvUnitPrice");
            Label lblgvTaxP = (Label)GvProductDetail.Rows[i].FindControl("lblgvTaxP");
            Label lblgvTaxV = (Label)GvProductDetail.Rows[i].FindControl("lblgvTaxV");
            Label lblgvDiscountP = (Label)GvProductDetail.Rows[i].FindControl("lblgvDiscountP");
            Label lblgvDiscountV = (Label)GvProductDetail.Rows[i].FindControl("lblgvDiscountV");

            dt.Rows[0]["Serial_No"] = lblgvSNo.Text;
            dt.Rows[0]["Product_Id"] = hdngvProductId.Value;
            dt.Rows[0]["UnitId"] = hdngvUnitId.Value;
            dt.Rows[0]["Quantity"] = lblgvQuantity.Text;
            dt.Rows[0]["FreeQty"] = lblgvFreeQuantity.Text;
            dt.Rows[0]["RemainQty"] = lblgvRemainQuantity.Text;
            dt.Rows[0]["UnitPrice"] = lblgvUnitPrice.Text;
            dt.Rows[0]["TaxP"] = lblgvTaxP.Text;
            dt.Rows[0]["TaxV"] = lblgvTaxV.Text;
            dt.Rows[0]["DiscountP"] = lblgvDiscountP.Text;
            dt.Rows[0]["DiscountV"] = lblgvDiscountV.Text;

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
            HiddenField hdngvUnitId = (HiddenField)GvProductDetail.Rows[i].FindControl("hdngvUnitId");
            Label lblgvQuantity = (Label)GvProductDetail.Rows[i].FindControl("lblgvQuantity");
            Label lblgvFreeQuantity = (Label)GvProductDetail.Rows[i].FindControl("lblgvFreeQuantity");
            Label lblgvRemainQuantity = (Label)GvProductDetail.Rows[i].FindControl("lblgvRemainQuantity");
            Label lblgvUnitPrice = (Label)GvProductDetail.Rows[i].FindControl("lblgvUnitPrice");
            Label lblgvTaxP = (Label)GvProductDetail.Rows[i].FindControl("lblgvTaxP");
            Label lblgvTaxV = (Label)GvProductDetail.Rows[i].FindControl("lblgvTaxV");
            Label lblgvDiscountP = (Label)GvProductDetail.Rows[i].FindControl("lblgvDiscountP");
            Label lblgvDiscountV = (Label)GvProductDetail.Rows[i].FindControl("lblgvDiscountV");

            dt.Rows[i]["Serial_No"] = lblgvSNo.Text;
            dt.Rows[i]["Product_Id"] = hdngvProductId.Value;
            dt.Rows[i]["UnitId"] = hdngvUnitId.Value;
            dt.Rows[i]["Quantity"] = lblgvQuantity.Text;
            dt.Rows[i]["FreeQty"] = lblgvFreeQuantity.Text;
            dt.Rows[i]["RemainQty"] = lblgvRemainQuantity.Text;
            dt.Rows[i]["UnitPrice"] = lblgvUnitPrice.Text;
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
            ddlUnit.SelectedValue = dt.Rows[0]["UnitId"].ToString();
            txtPQuantity.Text = dt.Rows[0]["Quantity"].ToString();
            txtPFreeQuantity.Text = dt.Rows[0]["FreeQty"].ToString();
            txtPRemainQuantity.Text = dt.Rows[0]["RemainQty"].ToString();
            txtPUnitPrice.Text = dt.Rows[0]["UnitPrice"].ToString();
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
        ResetProduct();
    }
    public DataTable FillProductDataTableUpdate()
    {
        DataTable dt = CreateProductDataTable();
        for (int i = 0; i < GvProductDetail.Rows.Count; i++)
        {
            dt.Rows.Add(i);
            Label lblSNo = (Label)GvProductDetail.Rows[i].FindControl("lblgvSerialNo");
            HiddenField hdngvProductId = (HiddenField)GvProductDetail.Rows[i].FindControl("hdngvProductId");
            HiddenField hdngvUnitId = (HiddenField)GvProductDetail.Rows[i].FindControl("hdngvUnitId");
            Label lblgvQuantity = (Label)GvProductDetail.Rows[i].FindControl("lblgvQuantity");
            Label lblgvFreeQuantity = (Label)GvProductDetail.Rows[i].FindControl("lblgvFreeQuantity");
            Label lblgvRemainQuantity = (Label)GvProductDetail.Rows[i].FindControl("lblgvRemainQuantity");
            Label lblgvUnitPrice = (Label)GvProductDetail.Rows[i].FindControl("lblgvUnitPrice");
            Label lblgvTaxP = (Label)GvProductDetail.Rows[i].FindControl("lblgvTaxP");
            Label lblgvTaxV = (Label)GvProductDetail.Rows[i].FindControl("lblgvTaxV");
            Label lblgvDiscountP = (Label)GvProductDetail.Rows[i].FindControl("lblgvDiscountP");
            Label lblgvDiscountV = (Label)GvProductDetail.Rows[i].FindControl("lblgvDiscountV");

            dt.Rows[i]["Serial_No"] = lblSNo.Text;
            dt.Rows[i]["Product_Id"] = hdngvProductId.Value;
            dt.Rows[i]["UnitId"] = hdngvUnitId.Value;
            dt.Rows[i]["Quantity"] = lblgvQuantity.Text;
            dt.Rows[i]["FreeQty"] = lblgvFreeQuantity.Text;
            dt.Rows[i]["RemainQty"] = lblgvRemainQuantity.Text;
            dt.Rows[i]["UnitPrice"] = lblgvUnitPrice.Text;
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
                dt.Rows[i]["UnitId"] = hdnUnitId.Value;
                dt.Rows[i]["Quantity"] = txtPQuantity.Text;
                dt.Rows[i]["FreeQty"] = txtPFreeQuantity.Text;
                dt.Rows[i]["RemainQty"] = txtPRemainQuantity.Text;
                dt.Rows[i]["UnitPrice"] = txtPUnitPrice.Text;
                dt.Rows[i]["TaxP"] = txtPTaxP.Text;
                dt.Rows[i]["TaxV"] = txtPTaxV.Text;
                dt.Rows[i]["DiscountP"] = txtPDiscountP.Text;
                dt.Rows[i]["DiscountV"] = txtPDiscountV.Text;
            }
        }
        return dt;
    }
    protected void txtPTaxP_TextChanged(object sender, EventArgs e)
    {
        if (txtPUnitPrice.Text != "")
        {
            float flTemp = 0;
            if (float.TryParse(txtPUnitPrice.Text, out flTemp))
            {
                if (txtPTaxP.Text != "")
                {
                    if (float.TryParse(txtPTaxP.Text, out flTemp))
                    {
                        txtPTaxV.Text = ((float.Parse(txtPUnitPrice.Text) * float.Parse(txtPTaxP.Text)) / 100).ToString();
                        if (txtPUnitPrice.Text != "")
                        {
                            txtPPriceAfterTax.Text = (float.Parse(txtPUnitPrice.Text) + float.Parse(txtPTaxV.Text)).ToString();
                            txtPPriceAfterDiscount.Text = (float.Parse(txtPUnitPrice.Text) + float.Parse(txtPTaxV.Text)).ToString();
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
                if (txtPTaxV.Text != "")
                {
                    if (float.TryParse(txtPTaxV.Text, out flTemp))
                    {
                        txtPTaxP.Text = ((100 * float.Parse(txtPTaxV.Text)) / float.Parse(txtPUnitPrice.Text)).ToString();
                        if (txtPUnitPrice.Text != "")
                        {
                            txtPPriceAfterTax.Text = (float.Parse(txtPUnitPrice.Text) + float.Parse(txtPTaxV.Text)).ToString();
                            txtPPriceAfterDiscount.Text = (float.Parse(txtPUnitPrice.Text) + float.Parse(txtPTaxV.Text)).ToString();
                            txtPTotalAmount.Text = (float.Parse(txtPUnitPrice.Text) + float.Parse(txtPPriceAfterTax.Text)).ToString();
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
                if (txtPDiscountP.Text != "")
                {
                    if (float.TryParse(txtPDiscountP.Text, out flTemp))
                    {
                        if (txtPPriceAfterTax.Text != "")
                        {
                            if (txtPPriceAfterTax.Text == "0")
                            {
                                txtPDiscountV.Text = ((float.Parse(txtPUnitPrice.Text) * float.Parse(txtPDiscountP.Text)) / 100).ToString();
                                txtPTotalAmount.Text = (float.Parse(txtPUnitPrice.Text) - float.Parse(txtPDiscountV.Text)).ToString();
                                txtPPriceAfterDiscount.Text = (float.Parse(txtPUnitPrice.Text) - float.Parse(txtPDiscountV.Text)).ToString();
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
                if (txtPDiscountV.Text != "")
                {
                    if (float.TryParse(txtPDiscountV.Text, out flTemp))
                    {
                        if (txtPPriceAfterTax.Text != "")
                        {
                            if (txtPPriceAfterTax.Text == "0")
                            {
                                txtPDiscountP.Text = ((100 * float.Parse(txtPDiscountV.Text)) / float.Parse(txtPUnitPrice.Text)).ToString();
                                txtPTotalAmount.Text = (float.Parse(txtPUnitPrice.Text) - float.Parse(txtPDiscountV.Text)).ToString();
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
                        txtPTaxV.Text = "";
                        ScriptManager.RegisterStartupScript(this, typeof(string), "OPEN_WINDOW", "window.alert('Enter Numeric Value Only');", true);
                        System.Web.UI.ScriptManager.GetCurrent(this).SetFocus(txtPTaxV);
                    }
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
    protected void txtShipingAddress_TextChanged(object sender, EventArgs e)
    {
        if (txtShipingAddress.Text != "")
        {
            DataTable dtAM = objAddMaster.GetAddressDataByAddressName(StrCompId, txtShipingAddress.Text);
            if (dtAM.Rows.Count > 0)
            {

            }
            else
            {
                txtShipingAddress.Text = "";
                DisplayMessage("Choose In Suggestions Only");
                System.Web.UI.ScriptManager.GetCurrent(this).SetFocus(txtShipingAddress);
                return;
            }
        }
        else
        {
            DisplayMessage("Enter Address Name");
            System.Web.UI.ScriptManager.GetCurrent(this).SetFocus(txtShipingAddress);
        }
    }
}
