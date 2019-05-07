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

public partial class Sales_SalesQuotation : System.Web.UI.Page
{
    #region defind Class Object
    Common cmn = new Common();
    Inv_SalesQuotationHeader objSQuoteHeader = new Inv_SalesQuotationHeader();
    Inv_SalesQuotationDetail ObjSQuoteDetail = new Inv_SalesQuotationDetail();
    Inv_SalesInquiryHeader objSIHeader = new Inv_SalesInquiryHeader();
    Inv_SalesInquiryDetail ObjSIDetail = new Inv_SalesInquiryDetail();
    Inv_ProductMaster objProductM = new Inv_ProductMaster();
    Ems_ContactMaster objContact = new Ems_ContactMaster();
    CurrencyMaster objCurrency = new CurrencyMaster();
    Inv_UnitMaster UM = new Inv_UnitMaster();
    EmployeeMaster objEmployee = new EmployeeMaster();
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
            FillCurrency();
            txtSQNo.Text = GetDocumentNumber();//updated by jitendra on 28-9-2013
            //txtSQNo.Text = objSQuoteHeader.GetAutoID(StrCompId, StrBrandId, StrLocationId);
            txtSQDate.Text = DateTime.Now.ToString(ObjSysParam.GetSysParameterByParamName("Date_Format").Rows[0]["Param_Value"].ToString());
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
        DataTable dtAllPageCode = cmn.GetAllPagePermission(StrUserId.ToString(), "13", "57");
        if (dtAllPageCode.Rows.Count != 0)
        {
            if (dtAllPageCode.Rows[0][0].ToString() == "SuperAdmin")
            {
                btnSQuoteSave.Visible = true;
                foreach (GridViewRow Row in GvSalesQuote.Rows)
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
                        btnSQuoteSave.Visible = true;
                    }
                    foreach (GridViewRow Row in GvSalesQuote.Rows)
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

        DataTable Dt = objDocNo.GetDocumentNumberAll(StrCompId, "13", "57");

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
                DocumentNo += "-" + (Convert.ToInt32(objSQuoteHeader.GetQuotationHeaderAll(StrCompId.ToString(), StrBrandId, StrLocationId).Rows.Count) + 1).ToString();
            }
            else
            {
                DocumentNo += (Convert.ToInt32(objSQuoteHeader.GetQuotationHeaderAll(StrCompId.ToString(), StrBrandId, StrLocationId).Rows.Count) + 1).ToString();

            }
        }
        else
        {
            DocumentNo += (Convert.ToInt32(objSQuoteHeader.GetQuotationHeaderAll(StrCompId.ToString(), StrBrandId, StrLocationId).Rows.Count) + 1).ToString();
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

        DataTable dtQuoteEdit = objSQuoteHeader.GetQuotationHeaderAllBySQuotationId(StrCompId, StrBrandId, StrLocationId, editid.Value);

        btnNew.Text = Resources.Attendance.Edit;
        if (dtQuoteEdit.Rows.Count > 0)
        {
            txtSQNo.Text = dtQuoteEdit.Rows[0]["SQuotation_No"].ToString();
            txtSQNo.ReadOnly = true;
            txtSQDate.Text = Convert.ToDateTime(dtQuoteEdit.Rows[0]["Quotation_Date"].ToString()).ToString(ObjSysParam.GetSysParameterByParamName("Date_Format").Rows[0]["Param_Value"].ToString());

            //Add Inquiry Data
            string strSalesInquiryId = dtQuoteEdit.Rows[0]["SInquiry_No"].ToString();
            hdnSalesInquiryId.Value = strSalesInquiryId;
            if (strSalesInquiryId != "" && strSalesInquiryId != "0")
            {
                DataTable dtInquiry = objSIHeader.GetSIHeaderAllBySInquiryId(StrCompId, StrBrandId, StrLocationId, hdnSalesInquiryId.Value);
                if (dtInquiry.Rows.Count > 0)
                {
                    txtInquiryNo.Text = dtInquiry.Rows[0]["SInquiryNo"].ToString();
                    txtInquiryDate.Text = Convert.ToDateTime(dtInquiry.Rows[0]["IDate"].ToString()).ToString(ObjSysParam.GetSysParameterByParamName("Date_Format").Rows[0]["Param_Value"].ToString());
                    ddlCurrency.SelectedValue = dtInquiry.Rows[0]["Currency_Id"].ToString();
                    string strEmployeeId = dtInquiry.Rows[0]["HandledEmpID"].ToString();
                    txtEmployee.Text = GetEmployeeName(strEmployeeId) + "/" + strEmployeeId;
                    txtCustomer.Text = GetCustomerName(dtInquiry.Rows[0]["Customer_Id"].ToString());
                }
            }

            //Add Detail Grid For Edit
            DataTable dtDetail = ObjSIDetail.GetSIDetailBySInquiryId(StrCompId, StrBrandId, StrLocationId, hdnSalesInquiryId.Value);
            if (dtDetail.Rows.Count > 0)
            {
                GvDetail.DataSource = dtDetail;
                GvDetail.DataBind();

                float fGrossTotal = 0.00f;
                foreach (GridViewRow gvr in GvDetail.Rows)
                {
                    HiddenField hdngvProductId = (HiddenField)gvr.FindControl("hdnProductId");
                    Label lblgvProductDescription = (Label)gvr.FindControl("lblgvProductDescription");
                    TextBox txtgvUnitPrice = (TextBox)gvr.FindControl("txtgvUnitPrice");
                    TextBox txtgvTaxP = (TextBox)gvr.FindControl("txtgvTaxP");
                    TextBox txtgvTaxV = (TextBox)gvr.FindControl("txtgvTaxV");
                    TextBox txtgvPriceAfterTax = (TextBox)gvr.FindControl("txtgvPriceAfterTax");
                    TextBox txtgvDiscountP = (TextBox)gvr.FindControl("txtgvDiscountP");
                    TextBox txtgvDiscountV = (TextBox)gvr.FindControl("txtgvDiscountV");
                    TextBox txtgvPriceAfterDiscount = (TextBox)gvr.FindControl("txtgvPriceAfterDiscount");
                    TextBox txtgvTotal = (TextBox)gvr.FindControl("txtgvTotal");

                    DataTable dtQuoteDetailByProductId = ObjSQuoteDetail.GetQuotationDetailBySQuotation_IdAndProductId(StrCompId, StrBrandId, StrLocationId, editid.Value, hdngvProductId.Value);
                    if (dtQuoteDetailByProductId.Rows.Count > 0)
                    {
                        txtgvUnitPrice.Text = dtQuoteDetailByProductId.Rows[0]["UnitPrice"].ToString();
                        txtgvTaxP.Text = dtQuoteDetailByProductId.Rows[0]["TaxPercent"].ToString();
                        txtgvTaxV.Text = dtQuoteDetailByProductId.Rows[0]["TaxValue"].ToString();
                        txtgvPriceAfterTax.Text = dtQuoteDetailByProductId.Rows[0]["PriceAfterTax"].ToString();
                        txtgvDiscountP.Text = dtQuoteDetailByProductId.Rows[0]["DiscountPercent"].ToString();
                        txtgvDiscountV.Text = dtQuoteDetailByProductId.Rows[0]["DiscountValue"].ToString();
                        txtgvPriceAfterDiscount.Text = dtQuoteDetailByProductId.Rows[0]["PriceAfterDiscount"].ToString();
                        string strTaxTotal = (float.Parse(txtgvUnitPrice.Text) + float.Parse(txtgvTaxV.Text)).ToString();
                        txtgvTotal.Text = (float.Parse(strTaxTotal) - float.Parse(txtgvDiscountV.Text)).ToString();
                    }
                    else
                    {
                        DataTable dtQuoteDetailByDescr = ObjSQuoteDetail.GetQuotationDetailBySQuotation_IdAndProductDescription(StrCompId, StrBrandId, StrLocationId, editid.Value, hdngvProductId.Value, lblgvProductDescription.Text);
                        if (dtQuoteDetailByDescr.Rows.Count > 0)
                        {
                            txtgvUnitPrice.Text = dtQuoteDetailByDescr.Rows[0]["UnitPrice"].ToString();
                            txtgvTaxP.Text = dtQuoteDetailByDescr.Rows[0]["TaxPercent"].ToString();
                            txtgvTaxV.Text = dtQuoteDetailByDescr.Rows[0]["TaxValue"].ToString();
                            txtgvPriceAfterTax.Text = dtQuoteDetailByDescr.Rows[0]["PriceAfterTax"].ToString();
                            txtgvDiscountP.Text = dtQuoteDetailByDescr.Rows[0]["DiscountPercent"].ToString();
                            txtgvDiscountV.Text = dtQuoteDetailByDescr.Rows[0]["DiscountValue"].ToString();
                            txtgvPriceAfterDiscount.Text = dtQuoteDetailByDescr.Rows[0]["PriceAfterDiscount"].ToString();
                            string strTaxTotal = (float.Parse(txtgvUnitPrice.Text) + float.Parse(txtgvTaxV.Text)).ToString();
                            txtgvTotal.Text = (float.Parse(strTaxTotal) - float.Parse(txtgvDiscountV.Text)).ToString();
                        }
                        else
                        {
                            txtgvUnitPrice.Text = "0";
                            txtgvTaxP.Text = "0";
                            txtgvTaxV.Text = "0";
                            txtgvPriceAfterTax.Text = "0";
                            txtgvDiscountP.Text = "0";
                            txtgvDiscountV.Text = "0";
                            txtgvPriceAfterDiscount.Text = "0";
                            string strTaxTotal = (float.Parse(txtgvUnitPrice.Text) + float.Parse(txtgvTaxV.Text)).ToString();
                            txtgvTotal.Text = (float.Parse(strTaxTotal) - float.Parse(txtgvDiscountV.Text)).ToString();
                        }
                    }
                    if (txtgvTotal.Text != "")
                    {
                        fGrossTotal = fGrossTotal + float.Parse(txtgvTotal.Text);
                    }
                }
                txtAmount.Text = fGrossTotal.ToString();
            }

            txtTaxP.Text = dtQuoteEdit.Rows[0]["TaxPercent"].ToString();
            txtTaxV.Text = dtQuoteEdit.Rows[0]["TaxValue"].ToString();
            txtDiscountP.Text = dtQuoteEdit.Rows[0]["DiscountPercent"].ToString();
            txtDiscountV.Text = dtQuoteEdit.Rows[0]["DiscountValue"].ToString();
            txtAmount_TextChanged(sender, e);

            txtHeader.Text = dtQuoteEdit.Rows[0]["Header"].ToString();
            txtFooter.Text = dtQuoteEdit.Rows[0]["Footer"].ToString();
            txtCondition1.Text = dtQuoteEdit.Rows[0]["Condition1"].ToString();
            txtCondition2.Text = dtQuoteEdit.Rows[0]["Condition2"].ToString();
            txtCondition3.Text = dtQuoteEdit.Rows[0]["Condition3"].ToString();
            txtCondition4.Text = dtQuoteEdit.Rows[0]["Condition4"].ToString();
            txtCondition5.Text = dtQuoteEdit.Rows[0]["Condition5"].ToString();
        }
        btnNew_Click(null, null);
        System.Web.UI.ScriptManager.GetCurrent(this).SetFocus(txtSQNo);
    }
    protected void GvSalesQuote_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        GvSalesQuote.PageIndex = e.NewPageIndex;
        DataTable dt = (DataTable)Session["dtFilter"];
        GvSalesQuote.DataSource = dt;
        GvSalesQuote.DataBind();
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
            GvSalesQuote.DataSource = view.ToTable();
            Session["dtFilter"] = view.ToTable();
            lblTotalRecords.Text = Resources.Attendance.Total_Records + " : " + view.ToTable().Rows.Count + "";
            GvSalesQuote.DataBind();
        }
    }
    protected void GvSalesQuote_Sorting(object sender, GridViewSortEventArgs e)
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
        GvSalesQuote.DataSource = dt;
        GvSalesQuote.DataBind();
    }
    protected void IbtnDelete_Command(object sender, CommandEventArgs e)
    {
        editid.Value = e.CommandArgument.ToString();
        int b = 0;
        b = objSQuoteHeader.DeleteQuotationHeader(StrCompId, StrBrandId, StrLocationId, editid.Value, "false", StrUserId, DateTime.Now.ToString());
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
    protected void btnSQuoteCancel_Click(object sender, EventArgs e)
    {
        Reset();
        btnList_Click(null, null);
        FillGridBin();
        FillGrid();
    }
    protected void BtnReset_Click(object sender, EventArgs e)
    {
        Reset();
        System.Web.UI.ScriptManager.GetCurrent(this).SetFocus(txtSQNo);
    }
    protected void btnSQuoteSave_Click(object sender, EventArgs e)
    {
        if (txtSQDate.Text == "")
        {
            DisplayMessage("Enter Sales Quotation Date");
            System.Web.UI.ScriptManager.GetCurrent(this).SetFocus(txtSQDate);
            return;
        }
        if (txtSQNo.Text == "")
        {
            DisplayMessage("Enter Sales Quotation No.");
            System.Web.UI.ScriptManager.GetCurrent(this).SetFocus(txtSQNo);
            return;
        }
        else
        {
            if (editid.Value == "")
            {
                DataTable dtSQNo = objSQuoteHeader.GetQuotationHeaderAllBySQuotationNo(StrCompId, StrBrandId, StrLocationId, txtSQNo.Text);
                if (dtSQNo.Rows.Count > 0)
                {
                    DisplayMessage("Sales Quotation No. Already Exits");
                    txtSQNo.Text = "";
                    System.Web.UI.ScriptManager.GetCurrent(this).SetFocus(txtSQNo);
                    return;
                }
            }
        }



        if (hdnSalesInquiryId.Value != "0")
        {
            if (GvDetail.Rows.Count > 0)
            {

            }
            else
            {
                DisplayMessage("You have no Product For Generate Quotation");
                return;
            }
        }
        else if (hdnSalesInquiryId.Value == "0")
        {
            DisplayMessage("Choose Record In Inquiry Section For Create Quotation");
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
            b = objSQuoteHeader.UpdateQuotationHeader(StrCompId, StrBrandId, StrLocationId, editid.Value, txtSQNo.Text, txtSQDate.Text, hdnSalesInquiryId.Value, ddlCurrency.SelectedValue, GetEmployeeId(txtEmployee.Text), txtTaxP.Text, txtTaxV.Text, txtDiscountP.Text, txtDiscountV.Text, txtHeader.Text, txtFooter.Text, txtCondition1.Text, txtCondition2.Text, txtCondition3.Text, txtCondition4.Text, txtCondition5.Text, "False", "", "", "", "", "", "True", DateTime.Now.ToString(), "True", Session["UserId"].ToString(), DateTime.Now.ToString());

            if (b != 0)
            {
                ObjSQuoteDetail.DeleteQuotationDetail(StrCompId, StrBrandId, StrLocationId, editid.Value);
                foreach (GridViewRow gvr in GvDetail.Rows)
                {
                    Label lblgvSerialNo = (Label)gvr.FindControl("lblgvSerialNo");
                    HiddenField hdngvProductId = (HiddenField)gvr.FindControl("hdnProductId");
                    Label lblgvProductDescription = (Label)gvr.FindControl("lblgvProductDescription");
                    HiddenField hdngvCurrencyId = (HiddenField)gvr.FindControl("hdnCurrencyId");
                    TextBox txtgvUnitPrice = (TextBox)gvr.FindControl("txtgvUnitPrice");
                    Label lblgvQuantity = (Label)gvr.FindControl("lblgvQuantity");
                    TextBox txtgvTaxP = (TextBox)gvr.FindControl("txtgvTaxP");
                    TextBox txtgvTaxV = (TextBox)gvr.FindControl("txtgvTaxV");
                    TextBox txtgvPriceAfterTax = (TextBox)gvr.FindControl("txtgvPriceAfterTax");
                    TextBox txtgvDiscountP = (TextBox)gvr.FindControl("txtgvDiscountP");
                    TextBox txtgvDiscountV = (TextBox)gvr.FindControl("txtgvDiscountV");
                    TextBox txtgvPriceAfterDiscount = (TextBox)gvr.FindControl("txtgvPriceAfterDiscount");

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
                    if (txtgvPriceAfterTax.Text == "")
                    {
                        txtgvPriceAfterTax.Text = "0";
                    }
                    if (txtgvDiscountP.Text == "")
                    {
                        txtgvDiscountP.Text = "0";
                    }
                    if (txtgvDiscountV.Text == "")
                    {
                        txtgvDiscountV.Text = "0";
                    }
                    if (txtgvPriceAfterDiscount.Text == "")
                    {
                        txtgvPriceAfterDiscount.Text = "0";
                    }

                    ObjSQuoteDetail.InsertQuotationDetail(StrCompId, StrBrandId, StrLocationId, editid.Value, lblgvSerialNo.Text, hdngvProductId.Value, lblgvProductDescription.Text, hdngvCurrencyId.Value, txtgvUnitPrice.Text, lblgvQuantity.Text, txtgvTaxP.Text, txtgvTaxV.Text, txtgvPriceAfterTax.Text, txtgvDiscountP.Text, txtgvDiscountV.Text, txtgvPriceAfterDiscount.Text, "False", "", "", "", "", "", "True", DateTime.Now.ToString(), "True", Session["UserId"].ToString(), DateTime.Now.ToString(), Session["UserId"].ToString(), DateTime.Now.ToString());
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
            b = objSQuoteHeader.InsertQuotationHeader(StrCompId, StrBrandId, StrLocationId, txtSQNo.Text, txtSQDate.Text, hdnSalesInquiryId.Value, ddlCurrency.SelectedValue, GetEmployeeId(txtEmployee.Text), txtTaxP.Text, txtTaxV.Text, txtDiscountP.Text, txtDiscountV.Text, txtHeader.Text, txtFooter.Text, txtCondition1.Text, txtCondition2.Text, txtCondition3.Text, txtCondition4.Text, txtCondition5.Text, "False", "", "", "", "", "", "True", DateTime.Now.ToString(), "True", Session["UserId"].ToString(), DateTime.Now.ToString(), Session["UserId"].ToString(), DateTime.Now.ToString());
            if (b != 0)
            {
                string strMaxId = string.Empty;
                DataTable dtMaxId = objSQuoteHeader.GetMaxSalesQuotationId(StrCompId, StrBrandId, StrLocationId);
                if (dtMaxId.Rows.Count > 0)
                {
                    strMaxId = dtMaxId.Rows[0][0].ToString();
                }

                if (strMaxId != "" && strMaxId != "0")
                {
                    foreach (GridViewRow gvr in GvDetail.Rows)
                    {
                        Label lblgvSerialNo = (Label)gvr.FindControl("lblgvSerialNo");
                        HiddenField hdngvProductId = (HiddenField)gvr.FindControl("hdnProductId");
                        Label lblgvProductDescription = (Label)gvr.FindControl("lblgvProductDescription");
                        HiddenField hdngvCurrencyId = (HiddenField)gvr.FindControl("hdnCurrencyId");
                        TextBox txtgvUnitPrice = (TextBox)gvr.FindControl("txtgvUnitPrice");
                        Label lblgvQuantity = (Label)gvr.FindControl("lblgvQuantity");
                        TextBox txtgvTaxP = (TextBox)gvr.FindControl("txtgvTaxP");
                        TextBox txtgvTaxV = (TextBox)gvr.FindControl("txtgvTaxV");
                        TextBox txtgvPriceAfterTax = (TextBox)gvr.FindControl("txtgvPriceAfterTax");
                        TextBox txtgvDiscountP = (TextBox)gvr.FindControl("txtgvDiscountP");
                        TextBox txtgvDiscountV = (TextBox)gvr.FindControl("txtgvDiscountV");
                        TextBox txtgvPriceAfterDiscount = (TextBox)gvr.FindControl("txtgvPriceAfterDiscount");

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
                        if (txtgvPriceAfterTax.Text == "")
                        {
                            txtgvPriceAfterTax.Text = "0";
                        }
                        if (txtgvDiscountP.Text == "")
                        {
                            txtgvDiscountP.Text = "0";
                        }
                        if (txtgvDiscountV.Text == "")
                        {
                            txtgvDiscountV.Text = "0";
                        }
                        if (txtgvPriceAfterDiscount.Text == "")
                        {
                            txtgvPriceAfterDiscount.Text = "0";
                        }

                        ObjSQuoteDetail.InsertQuotationDetail(StrCompId, StrBrandId, StrLocationId, strMaxId, lblgvSerialNo.Text, hdngvProductId.Value, lblgvProductDescription.Text, hdngvCurrencyId.Value, txtgvUnitPrice.Text, lblgvQuantity.Text, txtgvTaxP.Text, txtgvTaxV.Text, txtgvPriceAfterTax.Text, txtgvDiscountP.Text, txtgvDiscountV.Text, txtgvPriceAfterDiscount.Text, "False", "", "", "", "", "", "True", DateTime.Now.ToString(), "True", Session["UserId"].ToString(), DateTime.Now.ToString(), Session["UserId"].ToString(), DateTime.Now.ToString());
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
        b = objSQuoteHeader.DeleteQuotationHeader(StrCompId, StrBrandId, StrLocationId, editid.Value, true.ToString(), StrUserId, DateTime.Now.ToString());
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
    protected void GvSalesQuoteBin_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        GvSalesQuoteBin.PageIndex = e.NewPageIndex;

        DataTable dt = (DataTable)Session["dtInactive"];
        GvSalesQuoteBin.DataSource = dt;
        GvSalesQuoteBin.DataBind();

        string temp = string.Empty;

        for (int i = 0; i < GvSalesQuoteBin.Rows.Count; i++)
        {
            HiddenField lblconid = (HiddenField)GvSalesQuoteBin.Rows[i].FindControl("hdnTransId");
            string[] split = lblSelectedRecord.Text.Split(',');

            for (int j = 0; j < lblSelectedRecord.Text.Split(',').Length; j++)
            {
                if (lblSelectedRecord.Text.Split(',')[j] != "")
                {
                    if (lblconid.Value.Trim().ToString() == lblSelectedRecord.Text.Split(',')[j].Trim().ToString())
                    {
                        ((CheckBox)GvSalesQuoteBin.Rows[i].FindControl("chkSelect")).Checked = true;
                    }
                }
            }
        }
    }
    protected void GvSalesQuoteBin_OnSorting(object sender, GridViewSortEventArgs e)
    {
        HDFSortbin.Value = HDFSortbin.Value == "ASC" ? "DESC" : "ASC";
        DataTable dt = new DataTable();
        dt = objSQuoteHeader.GetQuotationHeaderAllFalse(StrCompId, StrBrandId, StrLocationId);
        DataView dv = new DataView(dt);
        string Query = "" + e.SortExpression + " " + HDFSortbin.Value + "";
        dv.Sort = Query;
        dt = dv.ToTable();
        Session["dtInactive"] = dt;
        GvSalesQuoteBin.DataSource = dt;
        GvSalesQuoteBin.DataBind();
        lblSelectedRecord.Text = "";
    }
    public void FillGridBin()
    {
        DataTable dt = new DataTable();
        dt = objSQuoteHeader.GetQuotationHeaderAllFalse(StrCompId, StrBrandId, StrLocationId);
        GvSalesQuoteBin.DataSource = dt;
        GvSalesQuoteBin.DataBind();
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
            GvSalesQuoteBin.DataSource = view.ToTable();
            GvSalesQuoteBin.DataBind();
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
        DataTable dt = objSQuoteHeader.GetQuotationHeaderAllFalse(StrCompId, StrBrandId, StrLocationId);

        if (GvSalesQuoteBin.Rows.Count != 0)
        {
            if (lblSelectedRecord.Text != "")
            {
                for (int j = 0; j < lblSelectedRecord.Text.Split(',').Length; j++)
                {
                    if (lblSelectedRecord.Text.Split(',')[j] != "")
                    {
                        //Msg = objTax.DeleteTaxMaster(lblSelectedRecord.Text.Split(',')[j].Trim().ToString(), true.ToString(), Session["UserId"].ToString(), DateTime.Now.ToString());
                        b = objSQuoteHeader.DeleteQuotationHeader(StrCompId, StrBrandId, StrLocationId, lblSelectedRecord.Text.Split(',')[j].Trim().ToString(), true.ToString(), Session["UserId"].ToString(), DateTime.Now.ToString());
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
                foreach (GridViewRow Gvr in GvSalesQuoteBin.Rows)
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
        CheckBox chkSelAll = ((CheckBox)GvSalesQuoteBin.HeaderRow.FindControl("chkCurrent"));
        for (int i = 0; i < GvSalesQuoteBin.Rows.Count; i++)
        {
            ((CheckBox)GvSalesQuoteBin.Rows[i].FindControl("chkSelect")).Checked = chkSelAll.Checked;
            if (chkSelAll.Checked)
            {
                if (!lblSelectedRecord.Text.Split(',').Contains(((HiddenField)(GvSalesQuoteBin.Rows[i].FindControl("hdnTransId"))).Value.Trim().ToString()))
                {
                    lblSelectedRecord.Text += ((HiddenField)(GvSalesQuoteBin.Rows[i].FindControl("hdnTransId"))).Value.Trim().ToString() + ",";
                }
            }
            else
            {
                string temp = string.Empty;
                string[] split = lblSelectedRecord.Text.Split(',');
                foreach (string item in split)
                {
                    if (item != ((HiddenField)(GvSalesQuoteBin.Rows[i].FindControl("hdnTransId"))).Value.Trim().ToString())
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
        HiddenField lb = (HiddenField)GvSalesQuoteBin.Rows[index].FindControl("hdnTransId");
        if (((CheckBox)GvSalesQuoteBin.Rows[index].FindControl("chkSelect")).Checked)
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
            for (int i = 0; i < GvSalesQuoteBin.Rows.Count; i++)
            {
                string[] split = lblSelectedRecord.Text.Split(',');
                HiddenField lblconid = (HiddenField)GvSalesQuoteBin.Rows[i].FindControl("hdnTransId");
                for (int j = 0; j < lblSelectedRecord.Text.Split(',').Length; j++)
                {
                    if (lblSelectedRecord.Text.Split(',')[j] != "")
                    {
                        if (lblconid.Value.Trim().ToString() == lblSelectedRecord.Text.Split(',')[j].Trim().ToString())
                        {
                            ((CheckBox)GvSalesQuoteBin.Rows[i].FindControl("chkSelect")).Checked = true;
                        }
                    }
                }
            }
        }
        else
        {
            lblSelectedRecord.Text = "";
            DataTable dtUnit1 = (DataTable)Session["dtInactive"];
            GvSalesQuoteBin.DataSource = dtUnit1;
            GvSalesQuoteBin.DataBind();
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
                    b = objSQuoteHeader.DeleteQuotationHeader(StrCompId, StrBrandId, StrLocationId, lblSelectedRecord.Text.Split(',')[j].Trim(), true.ToString(), Session["UserId"].ToString(), DateTime.Now.ToString());
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
            foreach (GridViewRow Gvr in GvSalesQuoteBin.Rows)
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
        DataTable dtBrand = objSQuoteHeader.GetQuotationHeaderAllTrue(StrCompId, StrBrandId, StrLocationId);
        lblTotalRecords.Text = Resources.Attendance.Total_Records + " : " + dtBrand.Rows.Count + "";
        Session["dtCurrency"] = dtBrand;
        Session["dtFilter"] = dtBrand;
        if (dtBrand != null && dtBrand.Rows.Count > 0)
        {
            GvSalesQuote.DataSource = dtBrand;
            GvSalesQuote.DataBind();
        }
        else
        {
            GvSalesQuote.DataSource = null;
            GvSalesQuote.DataBind();
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
        txtSQNo.Text = objSQuoteHeader.GetAutoID(StrCompId, StrBrandId, StrLocationId);
        txtSQDate.Text = DateTime.Now.ToString(ObjSysParam.GetSysParameterByParamName("Date_Format").Rows[0]["Param_Value"].ToString());
        txtSQNo.ReadOnly = false;
        txtCustomer.Text = "";
        txtInquiryNo.Text = "";
        txtInquiryDate.Text = "";
        FillCurrency();
        txtEmployee.Text = "";
        txtAmount.Text = "";
        txtTaxP.Text = "";
        txtTaxV.Text = "";
        txtPriceAfterTax.Text = "";
        txtDiscountP.Text = "";
        txtDiscountV.Text = "";
        txtTotalAmount.Text = "";
        txtHeader.Text = "";
        txtFooter.Text = "";
        txtCondition1.Text = "";
        txtCondition2.Text = "";
        txtCondition3.Text = "";
        txtCondition4.Text = "";
        txtCondition5.Text = "";

        GvDetail.DataSource = null;
        GvDetail.DataBind();

        FillRequestGrid();
        FillGrid();

        hdnSalesInquiryId.Value = "0";


        editid.Value = "";
        btnNew.Text = Resources.Attendance.New;
        ddlFieldName.SelectedIndex = 1;
        ddlOption.SelectedIndex = 2;
        txtValue.Text = "";
        txtValueBin.Text = "";
        lblSelectedRecord.Text = "";
        System.Web.UI.ScriptManager.GetCurrent(this).SetFocus(txtValue);
        txtSQNo.Text = GetDocumentNumber();
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
    #endregion

    #region Add Request Section
    protected void GvSalesInquiry_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        GvSalesInquiry.PageIndex = e.NewPageIndex;
        DataTable dt = (DataTable)Session["dtPRequest"];
        GvSalesInquiry.DataSource = dt;
        GvSalesInquiry.DataBind();
    }
    protected void GvSalesInquiry_Sorting(object sender, GridViewSortEventArgs e)
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
        GvSalesInquiry.DataSource = dt;
        GvSalesInquiry.DataBind();
    }
    private void FillRequestGrid()
    {
        DataTable dtPRequest = objSIHeader.GetDataForSalesQuotation(StrCompId, StrBrandId, StrLocationId);
        lblTotalRecords.Text = Resources.Attendance.Total_Records + " : " + dtPRequest.Rows.Count + "";
        Session["dtPRequest"] = dtPRequest;
        if (dtPRequest != null && dtPRequest.Rows.Count > 0)
        {
            GvSalesInquiry.DataSource = dtPRequest;
            GvSalesInquiry.DataBind();
        }
        else
        {
            GvSalesInquiry.DataSource = null;
            GvSalesInquiry.DataBind();
        }
        lblTotalRecords.Text = Resources.Attendance.Total_Records + " : " + dtPRequest.Rows.Count.ToString() + "";
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

        DataTable dtPRequest = objSIHeader.GetSIHeaderAllBySInquiryId(StrCompId, StrBrandId, StrLocationId, strRequestId);

        if (dtPRequest.Rows.Count > 0)
        {
            txtInquiryNo.Text = dtPRequest.Rows[0]["SInquiryNo"].ToString();
            txtInquiryDate.Text = Convert.ToDateTime(dtPRequest.Rows[0]["IDate"].ToString()).ToString(ObjSysParam.GetSysParameterByParamName("Date_Format").Rows[0]["Param_Value"].ToString());
            ddlCurrency.SelectedValue = dtPRequest.Rows[0]["Currency_Id"].ToString();
            string strEmployeeId = dtPRequest.Rows[0]["HandledEmpID"].ToString();
            txtEmployee.Text = GetEmployeeName(strEmployeeId) + "/" + strEmployeeId;
            txtCustomer.Text = GetCustomerName(dtPRequest.Rows[0]["Customer_Id"].ToString());

            hdnSalesInquiryId.Value = dtPRequest.Rows[0]["SInquiryID"].ToString();

            //Add Detail Grid
            DataTable dtDetail = ObjSIDetail.GetSIDetailBySInquiryId(StrCompId, StrBrandId, StrLocationId, strRequestId);
            if (dtDetail.Rows.Count > 0)
            {
                GvDetail.DataSource = dtDetail;
                GvDetail.DataBind();
            }
        }
        btnNew_Click(null, null);
        System.Web.UI.ScriptManager.GetCurrent(this).SetFocus(txtSQNo);
    }
    protected void GvDetail_RowCreated(object sender, GridViewRowEventArgs e)
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
            cell.ColumnSpan = 5;
            cell.Text = Resources.Attendance.Product_Detail;
            row.Controls.Add(cell);


            cell = new TableHeaderCell();
            cell.ColumnSpan = 2;
            cell.Text = Resources.Attendance.Unit_Price;
            row.Controls.Add(cell);

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
                            System.Web.UI.ScriptManager.GetCurrent(this).SetFocus(txtHeader);
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
                            System.Web.UI.ScriptManager.GetCurrent(this).SetFocus(txtHeader);
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
        foreach (GridViewRow gvr in GvDetail.Rows)
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
        foreach (GridViewRow gvr in GvDetail.Rows)
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
        foreach (GridViewRow gvr in GvDetail.Rows)
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
        foreach (GridViewRow gvr in GvDetail.Rows)
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
        foreach (GridViewRow gvr in GvDetail.Rows)
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
                System.Web.UI.ScriptManager.GetCurrent(this).SetFocus(txtTaxP);
            }
        }
    }
    #endregion
}
