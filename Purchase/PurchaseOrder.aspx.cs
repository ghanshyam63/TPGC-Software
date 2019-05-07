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
using net.webservicex.www;

public partial class Purchase_PurchaseOrder : BasePage
{
    #region Class Object
    string StrCompId = string.Empty;
    string UserId = string.Empty;
    string StrBrandId = string.Empty;
    string StrLocationId = string.Empty;
    SystemParameter ObjSysParam = new SystemParameter();
    Inv_UnitMaster ObjUnitMaster = new Inv_UnitMaster();
    Ems_ContactMaster ObjContactMaster = new Ems_ContactMaster();
    CurrencyMaster ObjCurrencyMaster = new CurrencyMaster();
    Set_AddressChild ObjAddChild = new Set_AddressChild();
    Set_AddressMaster ObjAdd = new Set_AddressMaster();
    PurchaseOrderHeader ObjPurchaseOrder = new PurchaseOrderHeader();
    Common cmn = new Common();
    Inv_ProductMaster ObjProductMaster = new Inv_ProductMaster();
    PurchaseOrderDetail ObjPurchaseOrderDetail = new PurchaseOrderDetail();
    
    Inv_PurchaseQuoteHeader objQuoteHeader = new Inv_PurchaseQuoteHeader();
    Inv_PurchaseQuoteDetail ObjQuoteDetail = new Inv_PurchaseQuoteDetail();
    net.webservicex.www.CurrencyConvertor obj = new net.webservicex.www.CurrencyConvertor();
    net.webservicex.www.Currency Currency = new net.webservicex.www.Currency();
    UserMaster ObjUser = new UserMaster();
    Set_DocNumber objDocNo = new Set_DocNumber();

    #endregion
    protected void Page_Load(object sender, EventArgs e)
    {


        if (Session["UserId"] == null)
        {
            Response.Redirect("~/ERPLogin.aspx");
        }

        if (!IsPostBack)
        {
            StrBrandId = Session["BrandId"].ToString();
            StrLocationId = Session["LocId"].ToString();
            StrCompId = Session["CompId"].ToString();
            UserId = Session["UserId"].ToString();
            txtPoNo.Text = GetDocumentNumber();
            txtPOdate.Text = DateTime.Now.ToString(ObjSysParam.GetSysParameterByParamName("Date_Format").Rows[0]["Param_Value"].ToString());
            txtReceivingDate.Text = DateTime.Now.ToString(ObjSysParam.GetSysParameterByParamName("Date_Format").Rows[0]["Param_Value"].ToString());
            txtShippingDate.Text = DateTime.Now.ToString(ObjSysParam.GetSysParameterByParamName("Date_Format").Rows[0]["Param_Value"].ToString());
            txtDeliveryDate.Text = DateTime.Now.ToString(ObjSysParam.GetSysParameterByParamName("Date_Format").Rows[0]["Param_Value"].ToString());
            FillCurrency();
            FillUnit();
            FillUnitdetail();
            btnReset_Click(null, null);

            ddlOrderType_SelectedIndexChanged(null, null);
            fillGrid();
            fillGridBin();
            FillGridQuotation();
            btnList_Click(null, null);
        }
        AllPageCode();

    }

    #region AllPageCode
    public void AllPageCode()
    {
        StrBrandId = Session["BrandId"].ToString();
        StrLocationId = Session["LocId"].ToString();
        Page.Title = ObjSysParam.GetSysTitle();
        UserId = Session["UserId"].ToString();
        StrCompId = Session["CompId"].ToString();
        Session["AccordianId"] = "12";
        Session["HeaderText"] = "Purchase";
        DataTable dtAllPageCode = cmn.GetAllPagePermission(UserId.ToString(), "12", "45");
        if (dtAllPageCode.Rows.Count != 0)
        {
            if (dtAllPageCode.Rows[0][0].ToString() == "SuperAdmin")
            {
                btnSave.Visible = true;
                btnProductSave.Visible = true;
                foreach (GridViewRow Row in gvPurchaseOrder.Rows)
                {
                    ((ImageButton)Row.FindControl("btnEdit")).Visible = true;
                    ((ImageButton)Row.FindControl("IbtnDelete")).Visible = true;
                }
                foreach (GridViewRow Row in gvProduct.Rows)
                {
                    ((ImageButton)Row.FindControl("btnPDEdit")).Visible = true;
                    ((ImageButton)Row.FindControl("IbtnPDDelete")).Visible = true;
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
                        btnSave.Visible = true;
                        btnProductSave.Visible = true;
                    }
                    foreach (GridViewRow Row in gvPurchaseOrder.Rows)
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
                    foreach (GridViewRow Row in gvProduct.Rows)
                    {
                        if (Convert.ToBoolean(DtRow["Op_Edit"].ToString()))
                        {
                            ((ImageButton)Row.FindControl("btnPDEdit")).Visible = true;
                        }
                        if (Convert.ToBoolean(DtRow["Op_Delete"].ToString()))
                        {
                            ((ImageButton)Row.FindControl("IbtnPDDelete")).Visible = true;
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

        DataTable Dt = objDocNo.GetDocumentNumberAll(StrCompId, "12", "45");

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
                DocumentNo += "-" + (Convert.ToInt32(ObjPurchaseOrder.GetPurchaseOrderHeader(StrCompId.ToString()).Rows.Count) + 1).ToString();
            }
            else
            {
                DocumentNo += (Convert.ToInt32(ObjPurchaseOrder.GetPurchaseOrderHeader(StrCompId.ToString()).Rows.Count) + 1).ToString();

            }
        }
        else
        {
            DocumentNo += (Convert.ToInt32(ObjPurchaseOrder.GetPurchaseOrderHeader(StrCompId.ToString()).Rows.Count) + 1).ToString();
        }

        return DocumentNo;

    } 

    #region System Defind Funcation :

    protected void btnSave_Click(object sender, EventArgs e)
    {
        string ReFerVoNo = string.Empty;
        string SupplierId = string.Empty;
        string ShippingLine = string.Empty;
        if (txtPOdate.Text == "")
        {
            DisplayMessage("Enter Purchase Order Date");
            txtPOdate.Focus();
            txtPOdate.Text = "";
            txtPOdate.Text = DateTime.Now.ToString(ObjSysParam.GetSysParameterByParamName("Date_Format").Rows[0]["Param_Value"].ToString());
           
            return;
        }
        if (txtPoNo.Text == "")
        {
            DisplayMessage("Enter Purchase Order No");
            txtPoNo.Focus();
            return;

        }
        if (txtSupplierName.Text != "")
        {
            SupplierId = txtSupplierName.Text.Split('/')[1].ToString();
        }
        else
        {
            DisplayMessage("Select Supplier");
            txtSupplierName.Focus();
            return;
        }
        if (ddlOrderType.SelectedValue == "D")
        {
            if (gvProduct.Rows.Count == 0)
            {
                DisplayMessage("Add at least one product");
                btnAddProduct.Focus();
                return;
            }
        }
       
        if (ddlCurrency.SelectedIndex == 0)
        {
            DisplayMessage("Select Currency");
            ddlCurrency.Focus();
            return;

        }
        if (txtTotalWeight.Text == "")
        {
            txtTotalWeight.Text = "0";
        }
        if (txtUnitRate.Text == "")
        {
            txtUnitRate.Text = "0";
        }
        if (txtShippingLine.Text != "")
        {
            ShippingLine = txtShippingLine.Text.Split('/')[1].ToString();
        }
        if (PnlReference.Visible == true)
        {
            ReFerVoNo = ddlReferenceVoucherType.SelectedValue;
        }
        int b = 0;

           string RpqId = string.Empty;
            RpqId = objQuoteHeader.GetQuoteHeaderAllDataByRPQ_No(StrCompId.ToString(), StrBrandId.ToString(), StrLocationId.ToString(), txtReferenceNo.Text.Trim()).Rows[0]["Trans_Id"].ToString();
           
        if (HdnEdit.Value == "")
        {
            if (ddlShipUnit.SelectedIndex == 0)
            {
                int shipUnit = 0;
            }
          b = ObjPurchaseOrder.InsertPurchaseOderHeader(StrCompId.ToString(), StrBrandId.ToString(), StrLocationId.ToString(), txtPoNo.Text.Trim(), ddlPaymentMode.SelectedValue.ToString(), txtPOdate.Text, ReFerVoNo.ToString(), RpqId.ToString(), txtDeliveryDate.Text, ddlOrderType.SelectedValue.ToString(), SupplierId.ToString(), ddlCurrency.SelectedValue.ToString(), txtCurrencyRate.Text, txRemark.Text, ShippingLine.ToString(), ddlShipBy.SelectedValue.ToString(), ddlShipmentType.SelectedValue.ToString(), ddlFreightStatus.SelectedValue.ToString(), ddlShipUnit.SelectedValue.ToString(), txtTotalWeight.Text.ToString(), txtUnitRate.Text.ToString(), txtShippingDate.Text.ToString(), txtReceivingDate.Text.ToString(), ddlPartialShipment.SelectedValue.ToString(), txtCondition_1.Text.ToString(), txtCondition2.Text.ToString(), txtCondition3.Text.ToString(), txtCondition4.Text.ToString(), txtCondition5.Text.ToString(), "", "", "", "", "", true.ToString(), DateTime.Now.ToString(), true.ToString(), UserId.ToString(), DateTime.Now.ToString(), UserId.ToString(), DateTime.Now.ToString());
            if (b != 0)
            {
                if (GvPurchaseQuote.Rows.Count != 0)
                {
                    
                    string Id = new DataView(ObjPurchaseOrder.GetPurchaseOrderHeader(StrCompId.ToString()), "PONo='" + txtPoNo.Text.ToString() + "'", "", DataViewRowState.CurrentRows).ToTable().Rows[0]["TransId"].ToString();
                    try
                    {
                        RpqId = objQuoteHeader.GetQuoteHeaderAllDataByRPQ_No(StrCompId.ToString(), StrBrandId.ToString(), StrLocationId.ToString(), txtReferenceNo.Text.Trim()).Rows[0]["Trans_Id"].ToString();
                    }
                    catch(Exception ex)
                    {
                        RpqId = "0";
                    }
                    foreach (GridViewRow GridRow in gvQuatationProduct.Rows)
                    {
                        CheckBox CheckBoxId = (CheckBox)GridRow.FindControl("chk");
                        Label lblgvProductId = (Label)GridRow.FindControl("lblgvProductId");
                        Label lblgvProductDes = (Label)GridRow.FindControl("lblgvProductName");
                        Label lblgvUnitId = (Label)GridRow.FindControl("lblgvUnitId");
                        Label lblgvRequiredQty = (Label)GridRow.FindControl("lblgvRequiredQty");
                        Label lblgvUnitCost = (Label)GridRow.FindControl("lblUnitCost");
                        TextBox txtgvFreeQty = (TextBox)GridRow.FindControl("txtFreeQty");
                        TextBox txtgvRemainQty = new TextBox();
                        txtgvRemainQty.Text = "0";

                        if (CheckBoxId.Checked)
                        {
                            ObjPurchaseOrderDetail.InsertPurchaseOrderDetail(StrCompId.ToString(), StrBrandId.ToString(), StrLocationId.ToString(), Id.ToString(), Id.ToString(), lblgvProductId.Text, lblgvProductDes.Text, lblgvUnitId.Text, lblgvUnitCost.Text, lblgvRequiredQty.Text, txtgvRemainQty.Text, txtgvFreeQty.Text, ddlReferenceVoucherType.SelectedValue.ToString(), RpqId.Trim(), "", "", "", "", "", true.ToString(), DateTime.Now.ToString(), true.ToString(), UserId.ToString(), DateTime.Now.ToString(), UserId.ToString(), DateTime.Now.ToString());
                        }
                    }

                }
                
                fillGrid();
                DisplayMessage("Record Saved");
                Reset();
            }
        }
        else
        {
            b = ObjPurchaseOrder.UpdatePurchaseOderHeader(StrCompId.ToString(), StrBrandId.ToString(), StrLocationId.ToString(), HdnEdit.Value, txtPoNo.Text.Trim(), ddlPaymentMode.SelectedValue.ToString(), txtPOdate.Text, ReFerVoNo.ToString(), RpqId.ToString(), txtDeliveryDate.Text, ddlOrderType.SelectedValue.ToString(), SupplierId.ToString(), ddlCurrency.SelectedValue.ToString(), txtCurrencyRate.Text, txRemark.Text, ShippingLine.ToString(), ddlShipBy.SelectedValue.ToString(), ddlShipmentType.SelectedValue.ToString(), ddlFreightStatus.SelectedValue.ToString(), ddlShipUnit.SelectedValue.ToString(), txtTotalWeight.Text.ToString(), txtUnitRate.Text.ToString(), txtShippingDate.Text.ToString(), txtReceivingDate.Text.ToString(), ddlPartialShipment.SelectedValue.ToString(), txtCondition_1.Text.ToString(), txtCondition2.Text.ToString(), txtCondition3.Text.ToString(), txtCondition4.Text.ToString(), txtCondition5.Text.ToString(), "", "", "", "", "", true.ToString(), DateTime.Now.ToString(), true.ToString(), UserId.ToString(), DateTime.Now.ToString(), UserId.ToString(), DateTime.Now.ToString());
            if (b != 0)
            {
               
                fillGrid();
                DisplayMessage("Record Updated");
                btnList_Click(null, null);
                Reset();
            }
        }


    }
    protected void btnList_Click(object sender, EventArgs e)
    {
        pnlMenuList.BackColor = System.Drawing.ColorTranslator.FromHtml("#ccddee");
        PnlMenuQuotation.BackColor = System.Drawing.ColorTranslator.FromHtml("#90bde9");

        pnlMenuNew.BackColor = System.Drawing.ColorTranslator.FromHtml("#90bde9");
        pnlMenuBin.BackColor = System.Drawing.ColorTranslator.FromHtml("#90bde9");

        PnlList.Visible = true;
        PnlNewEdit.Visible = false;
        PnlBin.Visible = false;
        PnlQuotation.Visible = false;
        txtValue.Focus();
    }
    protected void btnNew_Click(object sender, EventArgs e)
    {
        pnlMenuNew.BackColor = System.Drawing.ColorTranslator.FromHtml("#ccddee");

        pnlMenuList.BackColor = System.Drawing.ColorTranslator.FromHtml("#90bde9");
        pnlMenuBin.BackColor = System.Drawing.ColorTranslator.FromHtml("#90bde9");
        PnlMenuQuotation.BackColor = System.Drawing.ColorTranslator.FromHtml("#90bde9");

        PnlList.Visible = false;
        PnlNewEdit.Visible = true;
        PnlBin.Visible = false;
        PnlQuotation.Visible = false;
        txtPOdate.Focus();

    }
    protected void btnBin_Click(object sender, EventArgs e)
    {
        pnlMenuBin.BackColor = System.Drawing.ColorTranslator.FromHtml("#ccddee");
        PnlMenuQuotation.BackColor = System.Drawing.ColorTranslator.FromHtml("#90bde9");

        pnlMenuNew.BackColor = System.Drawing.ColorTranslator.FromHtml("#90bde9");
        pnlMenuList.BackColor = System.Drawing.ColorTranslator.FromHtml("#90bde9");
        PnlNewEdit.Visible = false;
        PnlBin.Visible = true;
        PnlList.Visible = false;
        PnlQuotation.Visible = false;
        fillGridBin();
        txtbinValue.Focus();
    }

    protected void ddlOrderType_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (ddlOrderType.SelectedIndex == 0)
        {
            PnlReference.Visible = false;
            btnCompareQuatation.Visible = false;
            btnAddProduct.Visible = true;
        }
        else
        {
            btnCompareQuatation.Visible = true;
            btnAddProduct.Visible = false;
            PnlReference.Visible = true;

        }
    }
    protected void txtShippingLine_TextChanged(object sender, EventArgs e)
    {
        lblshipingLineMobileNo.Text = "";
        lblShipingEmailId.Text = "";
        if (txtShippingLine.Text != "")
        {
            try
            {
                string ContactId = txtShippingLine.Text.Split('/')[1].ToString();
                if (ContactId.ToString() != "")
                {
                    string RefName = ObjAddChild.GetAddressChildDataByAddTypeAndAddRefId("Contact", ContactId.ToString()).Rows[0]["Address_Name"].ToString();
                    DataTable dt = ObjAdd.GetAddressDataByAddressName(StrCompId.ToString(), RefName.ToString());
                    if (dt.Rows.Count != 0)
                    {
                        string Temp = dt.Rows[0]["MobileNo1"].ToString();
                        Temp = Temp != "" ? dt.Rows[0]["MobileNo1"].ToString() : "No Mobile No";

                        lblshipingLineMobileNo.Text = Resources.Attendance.Mobile_No_1 + " : " + Temp.Trim();

                        Temp = dt.Rows[0]["EmailId1"].ToString();
                        Temp = Temp != "" ? dt.Rows[0]["EmailId1"].ToString() : "No Email Id";

                        lblShipingEmailId.Text = Resources.Attendance.Email_Id + " : " + Temp.Trim();
                        Temp = null;
                    }
                }
            }
            catch
            {

            }
        }
    }
    protected void gvPurchaseOrder_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        gvPurchaseOrder.PageIndex = e.NewPageIndex;

        DataTable dt = (DataTable)Session["dtFilter"];
        gvPurchaseOrder.DataSource = dt;
        gvPurchaseOrder.DataBind();
        AllPageCode();
        gvPurchaseOrder.BottomPagerRow.Focus();

    }
    protected void gvPurchaseOrder_OnSorting(object sender, GridViewSortEventArgs e)
    {

        HDFSort.Value = HDFSort.Value == "ASC" ? "DESC" : "ASC";
        DataTable dt = new DataTable();
        dt = (DataTable)Session["dtFilter"];
        DataView dv = new DataView(dt);
        string Query = "" + e.SortExpression + " " + HDFSort.Value + "";
        dv.Sort = Query;
        dt = dv.ToTable();
        Session["dtFilter"] = dt;
        gvPurchaseOrder.DataSource = dt;
        gvPurchaseOrder.DataBind();
        AllPageCode();
        gvPurchaseOrder.HeaderRow.Focus();

    }
    protected void btnbind_Click(object sender, ImageClickEventArgs e)
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
            DataTable dtCust = (DataTable)Session["DtPurchaseOrder"];


            DataView view = new DataView(dtCust, condition, "", DataViewRowState.CurrentRows);
            Session["dtFilter"] = view.ToTable();
            lblTotalRecords.Text = Resources.Attendance.Total_Records + ": " + view.ToTable().Rows.Count.ToString() + "";

            gvPurchaseOrder.DataSource = view.ToTable();
            gvPurchaseOrder.DataBind();
            AllPageCode();

            btnbind.Focus();

        }

    }
    protected void btnRefresh_Click(object sender, ImageClickEventArgs e)
    {
        txtValue.Text = "";
        ddlFieldName.SelectedIndex = 0;
        ddlOption.SelectedIndex = 3;
        txtValue.Focus();
        fillGrid();
    }

    protected void btnEdit_Command(object sender, CommandEventArgs e)
    {
        DataTable dt = ObjPurchaseOrder.GetPurchaseOrderTrueAllByReqId(StrCompId.ToString(), StrBrandId.ToString(), StrLocationId.ToString(), e.CommandArgument.ToString());
        if (dt.Rows.Count != 0)
        {
            HdnEdit.Value = e.CommandArgument.ToString();
            txtPOdate.Text = Convert.ToDateTime(dt.Rows[0]["PODate"].ToString()).ToString(ObjSysParam.GetSysParameterByParamName("Date_Format").Rows[0]["Param_Value"].ToString());
            txtPoNo.Text = dt.Rows[0]["PONo"].ToString();
            ddlOrderType.SelectedValue = dt.Rows[0]["OrderType"].ToString();
            txtSupplierName.Text = ObjContactMaster.GetContactByContactId(StrCompId.ToString(), dt.Rows[0]["SupplierId"].ToString()).Rows[0]["Contact_Name"].ToString() + "/" + dt.Rows[0]["SupplierId"].ToString();
            txtDeliveryDate.Text = Convert.ToDateTime(dt.Rows[0]["DeliveryDate"].ToString()).ToString(ObjSysParam.GetSysParameterByParamName("Date_Format").Rows[0]["Param_Value"].ToString());
            ddlPaymentMode.SelectedValue = dt.Rows[0]["PaymentModeId"].ToString();
            ddlCurrency.SelectedValue = dt.Rows[0]["CurrencyId"].ToString();
            txtCurrencyRate.Text = dt.Rows[0]["CurrencyRate"].ToString();
            txRemark.Text = dt.Rows[0]["Remark"].ToString();
            try
            {
                txtShippingLine.Text = ObjContactMaster.GetContactByContactId(StrCompId.ToString(), dt.Rows[0]["ShippingLine"].ToString()).Rows[0]["Contact_Name"].ToString() + "/" + dt.Rows[0]["ShippingLine"].ToString();
                txtShippingLine_TextChanged(null, null);
            }
            catch
            {

            }
            ddlShipBy.SelectedValue = dt.Rows[0]["ShipBy"].ToString();
            ddlShipmentType.SelectedValue = dt.Rows[0]["ShipmentType"].ToString();
            ddlFreightStatus.SelectedValue = dt.Rows[0]["Freight_Status"].ToString();
            ddlShipUnit.SelectedValue = dt.Rows[0]["ShipUnit"].ToString();
            txtTotalWeight.Text = dt.Rows[0]["TotalWeight"].ToString();
            txtUnitRate.Text = dt.Rows[0]["UnitRate"].ToString();
            txtReceivingDate.Text = Convert.ToDateTime(dt.Rows[0]["DateReceiving"].ToString()).ToString(ObjSysParam.GetSysParameterByParamName("Date_Format").Rows[0]["Param_Value"].ToString());
            txtShippingDate.Text = Convert.ToDateTime(dt.Rows[0]["DateShipping"].ToString()).ToString(ObjSysParam.GetSysParameterByParamName("Date_Format").Rows[0]["Param_Value"].ToString());
            ddlPartialShipment.SelectedValue = dt.Rows[0]["PartialShipment"].ToString();
            txtCondition_1.Text = dt.Rows[0]["Condition_1"].ToString();
            txtCondition2.Text = dt.Rows[0]["Condition_2"].ToString();
            txtCondition3.Text = dt.Rows[0]["Condition_3"].ToString();
            txtCondition4.Text = dt.Rows[0]["Condition_4"].ToString();
            txtCondition5.Text = dt.Rows[0]["Condition_5"].ToString();
            btnNew.Text = Resources.Attendance.Edit;
            btnNew_Click(null, null);
            txtPoNo.Enabled = false;
            fillgridDetail();
            if (ddlOrderType.SelectedValue == "R")
            {
                ddlOrderType_SelectedIndexChanged(null, null);
                btnAddProduct.Visible = false;
                ddlReferenceVoucherType.Enabled = false;
                txtReferenceNo.Enabled = false;
                ddlOrderType.Enabled = false;

            }
            try
            {
                ddlReferenceVoucherType.SelectedValue = dt.Rows[0]["ReferenceVoucherType"].ToString();
                
            }
            catch
            {

            }
            txtReferenceNo.Text=objQuoteHeader.GetQuoteHeaderAllDataByRPQ_No(StrCompId.ToString(), StrBrandId.ToString(), StrLocationId.ToString(), txtReferenceNo.Text.Trim()).Rows[0]["TransId"].ToString();
        }

    }
    protected void IbtnDelete_Command(object sender, CommandEventArgs e)
    {
        int b = 0;
        b = ObjPurchaseOrder.DeletePurchaseOderHeader(StrCompId.ToString(), StrBrandId.ToString(), StrLocationId.ToString(), e.CommandArgument.ToString(), false.ToString(), UserId.ToString(), DateTime.Now.ToString());
        if (b != 0)
        {

            DisplayMessage("Record Deleted");
            fillGrid();
            fillGridBin();
        }

    }
    protected void btnReset_Click(object sender, EventArgs e)
    {

        if (HdnEdit.Value == "")
        {
            string Id = ObjPurchaseOrder.getAutoId();
            ObjPurchaseOrderDetail.DeletePurchaseOrderDetailByPONo(StrCompId.ToString(), StrBrandId.ToString(), StrLocationId.ToString(), Id.ToString());
        }

        Reset();
    }
    protected void btnCancel_Click(object sender, EventArgs e)
    {
        if (HdnEdit.Value == "")
        {
            string Id = ObjPurchaseOrder.getAutoId();
            ObjPurchaseOrderDetail.DeletePurchaseOrderDetailByPONo(StrCompId.ToString(), StrBrandId.ToString(), StrLocationId.ToString(), Id.ToString());
        }
        Reset();
        btnRefresh_Click(null, null);
        btnList_Click(null, null);
    }
    protected void txtPoNo_TextChanged(object sender, EventArgs e)
    {
        if (txtPoNo.Text != "")
        {
            DataTable dt = new DataView(ObjPurchaseOrder.GetPurchaseOrderHeader(StrCompId.ToString(), StrBrandId.ToString(), StrLocationId.ToString()), "PONo='" + txtPoNo.Text.Trim() + "'", "", DataViewRowState.CurrentRows).ToTable();
            if (dt.Rows.Count != 0)
            {
                if (Convert.ToBoolean(dt.Rows[0]["IsActive"].ToString()))
                {
                    DisplayMessage("Purchase Order No Already Exist");
                    txtPoNo.Text = "";
                    txtPoNo.Focus();
                }
                else
                {
                    DisplayMessage("Purchase Order No Already Exist :- Go To Bin");
                    txtPoNo.Text = "";
                    txtPoNo.Focus();

                }

            }
            else
            {
                ddlOrderType.Focus();
            }


        }
    }

    protected void btnClosePanel_Click(object sender, ImageClickEventArgs e)
    {
        pnlProduct1.Visible = false;
        pnlProduct2.Visible = false;
    }
    protected void btnProductSave_Click(object sender, EventArgs e)
    {
        string ReqId = string.Empty;
        string ProductId = string.Empty;
        string UnitId = string.Empty;
        if (txtProductName.Text == "")
        {
            DisplayMessage("Enter Product Name");
            txtProductName.Focus();
            return;
        }
        if (ddlUnit.SelectedIndex == 0)
        {
            DisplayMessage("Select Unit Name");
            ddlUnit.Focus();
            return;
        }
        if (txtUnitCost.Text == "")
        {
            DisplayMessage("Ener Unit Cost");
            txtUnitCost.Focus();
            return;
        }
        if (txtOrderQty.Text == "")
        {
            DisplayMessage("Enter Order Quantity");
            txtOrderQty.Focus();
            return;
        }

        if (txtfreeQty.Text == "")
        {
            txtfreeQty.Text = "0";
        }
        if (HdnEdit.Value == "")
        {
            ReqId = ObjPurchaseOrder.getAutoId();
        }
        else
        {
            ReqId = HdnEdit.Value.ToString();
        }

      
       
        if (txtProductName.Text != "")
        {
            DataTable dt = new DataView(ObjProductMaster.GetProductMasterAll(StrCompId.ToString()), "EProductName='" + txtProductName.Text.ToString() + "'", "", DataViewRowState.CurrentRows).ToTable();
            if (dt.Rows.Count != 0)
            {
                ProductId = dt.Rows[0]["ProductId"].ToString();
            }
            else
            {
                ProductId = "0";
            }
        }
        if (ddlUnit.SelectedIndex == 0)
        {

        }
        else
        {
            UnitId = ddlUnit.SelectedValue.ToString();
        }
        int SerialNo = 0;
        if (hidProduct.Value == "")
        {
            DataTable dtProduct = ObjPurchaseOrderDetail.GetPurchaseOrderDetailbyPOId(StrCompId, StrBrandId, StrLocationId, ReqId.Trim());
            if (dtProduct.Rows.Count > 0)
            {
                dtProduct = new DataView(dtProduct, "", "Serial_No Desc", DataViewRowState.CurrentRows).ToTable();
                SerialNo = Convert.ToInt32(dtProduct.Rows[0]["Serial_No"].ToString());
                SerialNo += 1;
            }
            else
            {
                SerialNo = 1;
            }



        }
        else
        {
            SerialNo = Convert.ToInt32(ViewState["SerialNo"].ToString());

        }

        if (hidProduct.Value == "")
        {
                ObjPurchaseOrderDetail.InsertPurchaseOrderDetail(StrCompId.ToString(), StrBrandId.ToString(), StrLocationId.ToString(), ReqId.ToString(),SerialNo.ToString(), ProductId.ToString(), txtPDescription.Text, UnitId.ToString(), txtUnitCost.Text.ToString(), txtOrderQty.Text.ToString(), "0", txtfreeQty.Text.ToString(), "", "", "", "", "", "", "", true.ToString(), DateTime.Now.ToString(), true.ToString(), UserId.ToString(), DateTime.Now.ToString(), UserId.ToString(), DateTime.Now.ToString());
        }
        else
        {
            ObjPurchaseOrderDetail.UpdatePurchaseOrderDetail(StrCompId.ToString(), StrBrandId.ToString(), StrLocationId.ToString(), hidProduct.Value.ToString(), ReqId.ToString(), SerialNo.ToString(), ProductId.ToString(), txtPDescription.Text, UnitId.ToString(), txtUnitCost.Text.ToString(), txtOrderQty.Text.ToString(), "0", txtfreeQty.Text.ToString(), "", "", "", "", "", "", "", true.ToString(), DateTime.Now.ToString(), true.ToString(), UserId.ToString(), DateTime.Now.ToString());
            pnlProduct1.Visible = false;
            pnlProduct2.Visible = false;
        }
        fillgridDetail();
        ResetDetail();


    }


    protected void btnPDEdit_Command(object sender, CommandEventArgs e)
    {
        pnlProduct1.Visible = true;
        pnlProduct2.Visible = true;
        hidProduct.Value = e.CommandArgument.ToString();
        DataTable dt = ObjPurchaseOrderDetail.GetPurchaseOrderDetail(StrCompId.ToString(), StrBrandId.ToString(), StrLocationId.ToString(), "0", e.CommandArgument.ToString());
        if (dt.Rows.Count != 0)
        {
            if (dt.Rows[0]["Product_Id"].ToString() != "0")
            {
                txtProductName.Text = ProductName(dt.Rows[0]["Product_Id"].ToString());
            }
            else
            {
                txtProductName.Text = dt.Rows[0]["ProductDescription"].ToString();
            }
            ddlUnit.SelectedValue = dt.Rows[0]["UnitId"].ToString();
            txtPDescription.Text = dt.Rows[0]["ProductDescription"].ToString();
            txtfreeQty.Text = dt.Rows[0]["FreeQty"].ToString();
            txtOrderQty.Text = dt.Rows[0]["OrderQty"].ToString();
            txtUnitCost.Text = dt.Rows[0]["UnitCost"].ToString();
            ViewState["SerialNo"] = dt.Rows[0]["Serial_No"].ToString();
        }
    }
    protected void IbtnPDDelete_Command(object sender, CommandEventArgs e)
    {
        ObjPurchaseOrderDetail.DeletePurchaseOrderDetail(StrCompId.ToString(), StrBrandId.ToString(), StrLocationId.ToString(), e.CommandArgument.ToString(), "False", UserId.ToString(), DateTime.Now.ToString());
        fillgridDetail();

    }
    protected void btnProductCancel_Click(object sender, EventArgs e)
    {
        btnClosePanel_Click(null, null);
        ResetDetail();

    }


    protected void btnAddProduct_Click(object sender, EventArgs e)
    {
        pnlProduct1.Visible = true;
        pnlProduct2.Visible = true;
        txtProductName.Focus();
    }
    protected void txtProductName_TextChanged(object sender, EventArgs e)
    {
        if (txtProductName.Text != "")
        {
            try
            {
                DataTable dt = new DataView(ObjProductMaster.GetProductMasterTrueAll(StrCompId.ToString()), "EProductName='" + txtProductName.Text.ToString().Trim() + "'", "", DataViewRowState.CurrentRows).ToTable();
                if (dt.Rows.Count != 0)
                {
                    string ReqId = ObjPurchaseOrder.getAutoId();
                    DataTable dtProduct = ObjPurchaseOrderDetail.GetPurchaseOrderDetailbyPOId(StrCompId, StrBrandId, StrLocationId, ReqId.Trim());
                    if (dtProduct.Rows.Count > 0)
                    {
                        dtProduct = new DataView(dtProduct, "Product_Id=" + dt.Rows[0]["ProductId"].ToString() + "", "", DataViewRowState.CurrentRows).ToTable();
                        if (dtProduct.Rows.Count > 0)
                        {
                            DisplayMessage("Product Is already exists!");
                            txtProductName.Text = "";
                            txtProductName.Focus();
                            return;

                        }
                    }

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

                    txtOrderQty.Text = "1";
                    txtfreeQty.Text = "0";


                }
                else
                {
                    FillUnit();

                    txtPDescription.Text = "";
                    txtOrderQty.Text = "1";
                    txtfreeQty.Text = "0";

                }
                ddlUnit.Focus();

            }
            catch
            {

            }
        }
        else
        {
            DisplayMessage("Enter Product Name");
            System.Web.UI.ScriptManager.GetCurrent(this).SetFocus(txtProductName);
        }



    }

    protected void txtFreeQty_TextChanged(object sender, EventArgs e)
    {
        TextBox TxtBox = (TextBox)((GridViewRow)((TextBox)sender).Parent.Parent).FindControl("txtFreeQty");

        if (TxtBox.Text == "")
        {
            TxtBox.Text = "0";
            TxtBox.Focus();
        }

    }
    protected void lblRemainQty_TextChanged(object sender, EventArgs e)
    {
        TextBox TxtBox = (TextBox)((GridViewRow)((TextBox)sender).Parent.Parent).FindControl("txtRemainQty");
        if (TxtBox.Text == "")
        {
            TxtBox.Text = "0";
            TxtBox.Focus();
        }

    }
    protected void ddlCurrency_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (ddlCurrency.SelectedIndex != 0)
        {
            try
            {
                txtCurrencyRate.Text = Convert.ToDouble(obj.ConversionRate((Currency)System.Enum.Parse(Currency.GetType(), ObjCurrencyMaster.GetCurrencyMasterById(ddlCurrency.SelectedValue.ToString()).Rows[0]["Currency_Code"].ToString()), (Currency)System.Enum.Parse(Currency.GetType(), "KWD"))).ToString();

            }
            catch
            {
               DataTable dt = new DataView(ObjCurrencyMaster.GetCurrencyMaster(), "Currency_Code='" + ddlCurrency.SelectedValue.ToString() + "'", "", DataViewRowState.CurrentRows).ToTable();
               if(dt.Rows.Count!=0)
               {
                   txtCurrencyRate.Text = dt.Rows[0]["Currency_Value"].ToString();
               }
            }

        }
    }
    protected void txtSupplierName_TextChanged(object sender, EventArgs e)
    {
        
        if (txtSupplierName.Text.Trim() != "")
        {
            if (ddlOrderType.SelectedIndex != 0)
            {
                if (ddlReferenceVoucherType.SelectedValue == "PQ")
                {
                    if (txtReferenceNo.Text != "")
                    {
                        string Id = objQuoteHeader.GetQuoteHeaderAllDataByRPQ_No(StrCompId, StrBrandId, StrLocationId, txtReferenceNo.Text.Trim()).Rows[0]["Trans_Id"].ToString();
                        DataTable dtPQDetail = new DataView(ObjQuoteDetail.GetPurchaseQuationDatilsnotpodetail(StrCompId.ToString(), StrBrandId.ToString(), StrLocationId.ToString(), Id.ToString()), "Supplier_Id='" + txtSupplierName.Text.Split('/')[1].ToString() + "'", "", DataViewRowState.CurrentRows).ToTable();    
                        gvQuatationProduct.DataSource = dtPQDetail;
                        gvQuatationProduct.DataBind();
                    }
                }
            }
        }
    }
    protected void btnCompareQuatation_Click(object sender, ImageClickEventArgs e)
    {
        ScriptManager.RegisterStartupScript(Page, typeof(Page), "New", "window.open('../Purchase/CompareQuatation.aspx?RPQId=" + txtReferenceNo.Text.Trim() + "');", true);



    }
    #region Bin
    protected void btnbinbind_Click(object sender, ImageClickEventArgs e)
    {


        if (ddlbinOption.SelectedIndex != 0)
        {
            string condition = string.Empty;


            if (ddlbinOption.SelectedIndex == 1)
            {
                condition = "convert(" + ddlbinFieldName.SelectedValue + ",System.String)='" + txtbinValue.Text.Trim() + "'";
            }
            else if (ddlOption.SelectedIndex == 2)
            {
                condition = "convert(" + ddlbinFieldName.SelectedValue + ",System.String) like '%" + txtbinValue.Text + "%'";
            }
            else
            {
                condition = "convert(" + ddlbinFieldName.SelectedValue + ",System.String) Like '" + txtbinValue.Text + "%'";
            }
            DataTable dtCust = (DataTable)Session["DtBinPurchaseOrder"];


            DataView view = new DataView(dtCust, condition, "", DataViewRowState.CurrentRows);
            Session["dtBinFilter"] = view.ToTable();
            lblbinTotalRecords.Text = Resources.Attendance.Total_Records + ": " + view.ToTable().Rows.Count.ToString() + "";

            GvBinPurchaseOrder.DataSource = view.ToTable();
            GvBinPurchaseOrder.DataBind();


            if (view.ToTable().Rows.Count == 0)
            {
                imgBtnRestore.Visible = false;
                ImgbtnSelectAll.Visible = false;
            }
            else
            {

                AllPageCode();
            }
            btnbinbind.Focus();

        }

    }
    protected void btnbinRefresh_Click(object sender, ImageClickEventArgs e)
    {
        fillGrid();
        fillGridBin();
        ddlbinOption.SelectedIndex = 3;
        ddlbinFieldName.SelectedIndex = 0;
        txtbinValue.Text = "";
        lblSelectedRecord.Text = "";
        txtbinValue.Focus();
    }
    protected void GvBinPurchaseOrder_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        GvBinPurchaseOrder.PageIndex = e.NewPageIndex;
        if (HDFSortbin.Value == "")
            fillGridBin();
        else
        {
            DataTable dt = (DataTable)Session["dtbinFilter"];
            GvBinPurchaseOrder.DataSource = dt;
            GvBinPurchaseOrder.DataBind();
        }
        AllPageCode();

        string temp = string.Empty;
        bool isselcted;

        for (int i = 0; i < GvBinPurchaseOrder.Rows.Count; i++)
        {
            Label lblconid = (Label)GvBinPurchaseOrder.Rows[i].FindControl("lblId");
            string[] split = lblSelectedRecord.Text.Split(',');

            for (int j = 0; j < lblSelectedRecord.Text.Split(',').Length; j++)
            {
                if (lblSelectedRecord.Text.Split(',')[j] != "")
                {
                    if (lblconid.Text.Trim().ToString() == lblSelectedRecord.Text.Split(',')[j].Trim().ToString())
                    {
                        ((CheckBox)GvBinPurchaseOrder.Rows[i].FindControl("chkgvSelect")).Checked = true;
                    }
                }
            }
        }

        GvBinPurchaseOrder.BottomPagerRow.Focus();

    }
    protected void GvBinPurchaseOrder_Sorting(object sender, GridViewSortEventArgs e)
    {
        lblSelectedRecord.Text = "";
        HDFSortbin.Value = HDFSortbin.Value == "ASC" ? "DESC" : "ASC";
        DataTable dt = new DataTable();
        dt = (DataTable)Session["dtBinFilter"];
        DataView dv = new DataView(dt);
        string Query = "" + e.SortExpression + " " + HDFSortbin.Value + "";
        dv.Sort = Query;
        dt = dv.ToTable();
        Session["dtbinFilter"] = dt;
        GvBinPurchaseOrder.DataSource = dt;
        GvBinPurchaseOrder.DataBind();
        AllPageCode();
        GvBinPurchaseOrder.HeaderRow.Focus();

    }
    protected void chkgvSelectAll_CheckedChanged(object sender, EventArgs e)
    {
        CheckBox chkSelAll = ((CheckBox)GvBinPurchaseOrder.HeaderRow.FindControl("chkgvSelectAll"));
        for (int i = 0; i < GvBinPurchaseOrder.Rows.Count; i++)
        {
            ((CheckBox)GvBinPurchaseOrder.Rows[i].FindControl("chkgvSelect")).Checked = chkSelAll.Checked;
            if (chkSelAll.Checked)
            {
                if (!lblSelectedRecord.Text.Split(',').Contains(((Label)(GvBinPurchaseOrder.Rows[i].FindControl("lblId"))).Text.Trim().ToString()))
                {
                    lblSelectedRecord.Text += ((Label)(GvBinPurchaseOrder.Rows[i].FindControl("lblId"))).Text.Trim().ToString() + ",";
                }
            }
            else
            {
                string temp = string.Empty;
                string[] split = lblSelectedRecord.Text.Split(',');
                foreach (string item in split)
                {
                    if (item != ((Label)(GvBinPurchaseOrder.Rows[i].FindControl("lblId"))).Text.Trim().ToString())
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
        ((CheckBox)GvBinPurchaseOrder.HeaderRow.FindControl("chkgvSelectAll")).Focus();
    }
    protected void chkgvSelect_CheckedChanged(object sender, EventArgs e)
    {
        string empidlist = string.Empty;
        string temp = string.Empty;
        int index = ((GridViewRow)((CheckBox)sender).Parent.Parent).RowIndex;
        Label lb = (Label)GvBinPurchaseOrder.Rows[index].FindControl("lblId");
        if (((CheckBox)GvBinPurchaseOrder.Rows[index].FindControl("chkgvSelect")).Checked)
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
        ((CheckBox)GvBinPurchaseOrder.Rows[index].FindControl("chkgvSelect")).Focus();
    }
    protected void ImgbtnSelectAll_Click(object sender, ImageClickEventArgs e)
    {
        DataTable dtPr = (DataTable)Session["dtBinFilter"];

        if (ViewState["Select"] == null)
        {
            ViewState["Select"] = 1;
            foreach (DataRow dr in dtPr.Rows)
            {
                if (!lblSelectedRecord.Text.Split(',').Contains(dr["TransId"]))
                {
                    lblSelectedRecord.Text += dr["TransId"] + ",";
                }
            }
            for (int i = 0; i < GvBinPurchaseOrder.Rows.Count; i++)
            {
                string[] split = lblSelectedRecord.Text.Split(',');
                Label lblconid = (Label)GvBinPurchaseOrder.Rows[i].FindControl("lblId");
                for (int j = 0; j < lblSelectedRecord.Text.Split(',').Length; j++)
                {
                    if (lblSelectedRecord.Text.Split(',')[j] != "")
                    {
                        if (lblconid.Text.Trim().ToString() == lblSelectedRecord.Text.Split(',')[j].Trim().ToString())
                        {
                            ((CheckBox)GvBinPurchaseOrder.Rows[i].FindControl("chkgvSelect")).Checked = true;
                        }
                    }
                }
            }
        }
        else
        {
            lblSelectedRecord.Text = "";
            DataTable dtPr1 = (DataTable)Session["dtBinFilter"];
            GvBinPurchaseOrder.DataSource = dtPr1;
            GvBinPurchaseOrder.DataBind();
            AllPageCode();
            ViewState["Select"] = null;
        }

        ImgbtnSelectAll.Focus();

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
                    b = ObjPurchaseOrder.DeletePurchaseOderHeader(StrCompId.ToString(), StrBrandId.ToString(), StrLocationId.ToString(), lblSelectedRecord.Text.Split(',')[j].Trim(), true.ToString(), UserId.ToString(), DateTime.Now.ToString());

                }
            }
        }

        if (b != 0)
        {

            fillGrid();
            fillGridBin();
            lblSelectedRecord.Text = "";
            ViewState["Select"] = null;
            DisplayMessage("Record Activated");
        }
        else
        {
            int fleg = 0;
            foreach (GridViewRow Gvr in GvBinPurchaseOrder.Rows)
            {
                CheckBox chk = (CheckBox)Gvr.FindControl("chkgvSelect");
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
        txtbinValue.Focus();
    }

    #endregion
    #region Quotation
    protected void BtnQuotation_Click(object sender, EventArgs e)
    {
        pnlMenuList.BackColor = System.Drawing.ColorTranslator.FromHtml("#90bde9");
        PnlMenuQuotation.BackColor = System.Drawing.ColorTranslator.FromHtml("#ccddee");
        pnlMenuNew.BackColor = System.Drawing.ColorTranslator.FromHtml("#90bde9");
        pnlMenuBin.BackColor = System.Drawing.ColorTranslator.FromHtml("#90bde9");
        PnlNewEdit.Visible = false;
        PnlBin.Visible = false;
        PnlList.Visible = false;
        PnlQuotation.Visible = true;

    }

    protected void GvPurchaseQuote_Sorting(object sender, GridViewSortEventArgs e)
    {
        DataTable dt = (DataTable)Session["dtQFilter"];
        string sortdir = "DESC";
        if (ViewState["QSortDir"] != null)
        {
            sortdir = ViewState["QSortDir"].ToString();
            if (sortdir == "ASC")
            {
                e.SortDirection = SortDirection.Descending;
                ViewState["QSortDir"] = "DESC";
            }
            else
            {
                e.SortDirection = SortDirection.Ascending;
                ViewState["QSortDir"] = "ASC";
            }
        }
        else
        {
            ViewState["QSortDir"] = "DESC";
        }

        dt = (new DataView(dt, "", e.SortExpression + " " + ViewState["@SortDir"].ToString(), DataViewRowState.CurrentRows)).ToTable();
        Session["dtQFilter"] = dt;
        GvPurchaseQuote.DataSource = dt;
        GvPurchaseQuote.DataBind();
    }

    protected void GvPurchaseQuote_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        GvPurchaseQuote.PageIndex = e.NewPageIndex;
        DataTable dt = (DataTable)Session["dtQFilter"];
        GvPurchaseQuote.DataSource = dt;
        GvPurchaseQuote.DataBind();
    }

    protected void ImgBtnQBind_Click(object sender, ImageClickEventArgs e)
    {
        if (ddlQOption.SelectedIndex != 0)
        {
            string condition = string.Empty;
            if (ddlOption.SelectedIndex == 1)
            {
                condition = "convert(" + ddlQSeleclField.SelectedValue + ",System.String)='" + txtQValue.Text.Trim() + "'";
            }
            else if (ddlOption.SelectedIndex == 2)
            {
                condition = "convert(" + ddlQSeleclField.SelectedValue + ",System.String) like '%" + txtQValue.Text.Trim() + "%'";
            }
            else
            {
                condition = "convert(" + ddlQSeleclField.SelectedValue + ",System.String) Like '" + txtQValue.Text.Trim() + "%'";
            }
            DataTable dtAdd = (DataTable)Session["dtQuotation"];
            DataView view = new DataView(dtAdd, condition, "", DataViewRowState.CurrentRows);
            GvPurchaseQuote.DataSource = view.ToTable();
            Session["dtQFilter"] = view.ToTable();
            lblQTotalRecords.Text = Resources.Attendance.Total_Records + " : " + view.ToTable().Rows.Count + "";
            GvPurchaseQuote.DataBind();
        }
    }

    protected void btnPurchaseQuote_Command(object sender, CommandEventArgs e)
    {
        DataTable dt = objQuoteHeader.GetQuoteHeaderAllDataByTransId(StrCompId.ToString(), StrBrandId.ToString(), StrLocationId.ToString(), e.CommandArgument.ToString());
        if (dt.Rows.Count != 0)
        {
            try
            {
                txtSupplierName.Text = ObjContactMaster.GetContactByContactId(StrCompId.ToString(), dt.Rows[0]["FinalSupplierId"].ToString()).Rows[0]["Contact_Name"].ToString() + "/" + dt.Rows[0]["FinalSupplierId"].ToString();
            }
            catch
            {
            }
            ddlOrderType.SelectedValue = "R";
            ddlOrderType_SelectedIndexChanged(null, null);
            ddlReferenceVoucherType.SelectedValue = "PQ";
            txtReferenceNo.Text = dt.Rows[0]["RPQ_No"].ToString();

            btnAddProduct.Visible = false;
            btnNew_Click(null, null);
        }

    }

    protected void ImgBtnQRefresh_Click(object sender, ImageClickEventArgs e)
    {

        ddlQSeleclField.SelectedIndex = 1;
        ddlQOption.SelectedIndex = 3;
        txtQValue.Text = "";
    }


    #endregion
    #endregion

    #region User Defind Funcation

    public void Reset()
    {
        txtPOdate.Text = DateTime.Now.ToString(ObjSysParam.GetSysParameterByParamName("Date_Format").Rows[0]["Param_Value"].ToString());
        txtReceivingDate.Text = DateTime.Now.ToString(ObjSysParam.GetSysParameterByParamName("Date_Format").Rows[0]["Param_Value"].ToString());
        txtShippingDate.Text = DateTime.Now.ToString(ObjSysParam.GetSysParameterByParamName("Date_Format").Rows[0]["Param_Value"].ToString());
        txtDeliveryDate.Text = DateTime.Now.ToString(ObjSysParam.GetSysParameterByParamName("Date_Format").Rows[0]["Param_Value"].ToString());
        FillCurrency();
        FillUnit();
        ddlOrderType_SelectedIndexChanged(null, null);
        txtPoNo.Text = "";
        txtSupplierName.Text = "";
        ddlPaymentMode.SelectedIndex = 0;
        txtCurrencyRate.Text = "0";
        txRemark.Text = "";
        txtShippingLine.Text = "";
        lblshipingLineMobileNo.Text = "";
        lblShipingEmailId.Text = "";
        ddlShipBy.SelectedIndex = 0;
        txtTotalWeight.Text = "";
        ddlShipmentType.SelectedIndex = 0;
        ddlFreightStatus.SelectedIndex = 0;
        txtUnitRate.Text = "";
        ddlPartialShipment.SelectedIndex = 0;
        txtCondition_1.Text = "";
        txtCondition2.Text = "";
        txtCondition3.Text = "";
        txtCondition4.Text = "";
        txtCondition5.Text = "";
        btnNew.Text = Resources.Attendance.New;
        txtPoNo.Enabled = true;
        gvProduct.DataSource = null;
        gvProduct.DataBind();
        gvQuatationProduct.DataSource = null;
        gvQuatationProduct.DataBind();
        ddlOrderType.SelectedValue = "D";
        ddlOrderType_SelectedIndexChanged(null, null);
        txtReferenceNo.Text = "";
        ddlReferenceVoucherType.SelectedIndex = 0;
        btnAddProduct.Visible = true;
        HdnEdit.Value = "";
        ddlReferenceVoucherType.Enabled = true;
        txtReferenceNo.Enabled = true;
        ddlOrderType.Enabled = true;
        txtPoNo.Text = GetDocumentNumber();

    }

    public void fillGrid()
    {
        DataTable dt = ObjPurchaseOrder.GetPurchaseOrderTrueAll(StrCompId.ToString(), StrBrandId.ToString(), StrLocationId.ToString());
        gvPurchaseOrder.DataSource = dt;
        gvPurchaseOrder.DataBind();
        Session["dtFilter"] = dt;
        Session["dtPurchaseOrder"] = dt;
        lblTotalRecords.Text = Resources.Attendance.Total_Records + " : " + dt.Rows.Count.ToString();
        AllPageCode();
    }
    public void fillGridBin()
    {
        DataTable dt = ObjPurchaseOrder.GetPurchaseOrderFalseAll(StrCompId.ToString(), StrBrandId.ToString(), StrLocationId.ToString());
        GvBinPurchaseOrder.DataSource = dt;
        GvBinPurchaseOrder.DataBind();
        Session["dtBinFilter"] = dt;
        Session["dtBinPurchaseOrder"] = dt;
        lblbinTotalRecords.Text = Resources.Attendance.Total_Records + " : " + dt.Rows.Count.ToString();
        if (dt.Rows.Count == 0)
        {
            imgBtnRestore.Visible = false;
            ImgbtnSelectAll.Visible = false;
        }
        else
        {
            AllPageCode();
        }
    }

    public string GetDateFromat(string Date)
    {
        try
        {
            return Convert.ToDateTime(Date).ToString(ObjSysParam.GetSysParameterByParamName("Date_Format").Rows[0]["Param_Value"].ToString()).ToString();
        }
        catch
        {
            return "";
        }

    }
    public string GetOderType(string OrderType)
    {
        string Type = string.Empty;

        if (OrderType == "D")
        {
            Type = "Direct(search as D)";
        }
        else
        {
            Type = "Refference of Quotation(Search as R)";
        }


        return Type;

    }
    public void DisplayMessage(string str)
    {
        ScriptManager.RegisterStartupScript(this, GetType(), "", "alert('" + str + "');", true);
    }

    public void FillUnitdetail()
    {
        try
        {
            DataTable dt = ObjUnitMaster.GetUnitMasterAll(StrCompId.ToString());
            ddlUnit.DataSource = dt;
            ddlUnit.DataTextField = "Unit_Name";
            ddlUnit.DataValueField = "Unit_Id";
            ddlUnit.DataBind();
            ddlUnit.Items.Insert(0, "--Select--");
            ddlUnit.SelectedIndex = 0;
        }
        catch
        {
            ddlUnit.Items.Insert(0, "--Select--");
            ddlUnit.SelectedIndex = 0;

        }


    }
    public void FillCurrency()
    {
        try
        {
            DataTable dt = ObjCurrencyMaster.GetCurrencyMaster();
            ddlCurrency.DataSource = dt;
            ddlCurrency.DataTextField = "Currency_Name";
            ddlCurrency.DataValueField = "Currency_Id";
            ddlCurrency.DataBind();
            ddlCurrency.Items.Insert(0, "--Select--");
            ddlCurrency.SelectedIndex = 0;
        }
        catch
        {
            ddlCurrency.Items.Insert(0, "--Select--");
            ddlCurrency.SelectedIndex = 0;

        }


    }

    public void fillgridDetail()
    {
        string ReqId = ObjPurchaseOrder.getAutoId();
        if (HdnEdit.Value == "")
        {
            ReqId = ObjPurchaseOrder.getAutoId();
        }
        else
        {
            ReqId = HdnEdit.Value.ToString();
        }
        DataTable dt = ObjPurchaseOrderDetail.GetPurchaseOrderDetailbyPOId(StrCompId, StrBrandId, StrLocationId, ReqId.ToString());
        gvProduct.DataSource = dt;
        gvProduct.DataBind();
        AllPageCode();

    }
    public string ProductName(string ProductId)
    {
        string ProductName = string.Empty;
        DataTable dt = ObjProductMaster.GetProductMasterById(StrCompId.ToString(), ProductId.ToString());
        if (dt.Rows.Count != 0)
        {
            ProductName = dt.Rows[0]["EProductName"].ToString();
        }
        else
        {
            ProductName = "0";
        }
        return ProductName;

    }
    public string UnitName(string UnitId)
    {
        string UnitName = string.Empty;
        DataTable dt = ObjUnitMaster.GetUnitMasterById(StrCompId.ToString(), UnitId.ToString());
        if (dt.Rows.Count != 0)
        {
            UnitName = dt.Rows[0]["Unit_Name"].ToString();
        }
        return UnitName;
    }
    public void FillUnit()
    {
        try
        {
            DataTable dt = ObjUnitMaster.GetUnitMasterAll(StrCompId.ToString());
            ddlShipUnit.DataSource = dt;
            ddlShipUnit.DataTextField = "Unit_Name";
            ddlShipUnit.DataValueField = "Unit_Id";
            ddlShipUnit.DataBind();
            ListItem li = new ListItem();
            li.Value = "0";
            li.Text = "--Select--";
            li.Selected = true;
            ddlShipUnit.Items.Add(li);
        }
        catch
        {
            ListItem li = new ListItem();
            li.Value = "0";
            li.Text="--Select--";
            li.Selected = true;
            ddlShipUnit.Items.Add(li);
           
        }


    }
    public void ResetDetail()
    {
        txtProductName.Text = "";
        txtPDescription.Text = "";
        ddlUnit.SelectedIndex = 0;
        txtfreeQty.Text = "";
        hidProduct.Value = "";
        txtOrderQty.Text = "";
        txtUnitCost.Text = "";
    }


    #region Quotation
    private void FillGridQuotation()
    {
        DataTable dtBrand = objQuoteHeader.GetQuoteHeaderAllTrue(StrCompId, StrBrandId, StrLocationId);
        lblQTotalRecords.Text = Resources.Attendance.Total_Records + " : " + dtBrand.Rows.Count + "";
        Session["dtQuotation"] = dtBrand;
        Session["dtQFilter"] = dtBrand;
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
        lblQTotalRecords.Text = Resources.Attendance.Total_Records + " : " + dtBrand.Rows.Count.ToString() + "";
    }
    #endregion
    #endregion

    #region Auto Complete Method

    [System.Web.Services.WebMethodAttribute(), System.Web.Script.Services.ScriptMethodAttribute()]
    public static string[] GetCompletionList(string prefixText, int count, string contextKey)
    {
        Ems_ContactMaster ObjCon = new Ems_ContactMaster();
        DataTable dt = ObjCon.GetContactTrueAllData(HttpContext.Current.Session["CompId"].ToString());
        DataTable dtTemp = new DataTable();
        dtTemp = new DataView(dt, "Contact_Name like'" + prefixText + "%'", "", DataViewRowState.CurrentRows).ToTable();
        if (dtTemp.Rows.Count != 0)
        {
            dt.Rows.Clear();
            dt.Merge(dtTemp);

        }


        string[] str = new string[dt.Rows.Count];
        for (int i = 0; i < dt.Rows.Count; i++)
        {
            str[i] = dt.Rows[i]["Contact_Name"].ToString() + " / " + dt.Rows[i]["Contact_Id"].ToString();
        }
        return str;
    }

    [System.Web.Services.WebMethodAttribute(), System.Web.Script.Services.ScriptMethodAttribute()]
    public static string[] GetCompletionList_Supplier(string prefixText, int count, string contextKey)
    {
        Ems_ContactMaster ObjContMaster = new Ems_ContactMaster();
        Set_Suppliers ObjSupplier = new Set_Suppliers();
        DataTable dtContAll = ObjContMaster.GetContactAllData(HttpContext.Current.Session["CompId"].ToString());
        DataTable dtSupplier = ObjSupplier.GetSupplierAllTrueData(HttpContext.Current.Session["CompId"].ToString(), "1");
        DataTable dtMain = new DataTable();
        for (int i = 0; i < dtSupplier.Rows.Count; i++)
        {
            dtMain.Merge(new DataView(dtContAll, "Contact_Id='" + dtSupplier.Rows[i]["Supplier_Id"].ToString() + "'", "", DataViewRowState.CurrentRows).ToTable());
        }


        string filtertext = "Contact_Name like '" + prefixText + "%'";
        DataTable dtCon = new DataView(dtMain, filtertext, "", DataViewRowState.CurrentRows).ToTable();
        if (dtCon.Rows.Count == 0)
        {
            dtCon = dtMain.Copy();
        }
        string[] filterlist = new string[dtCon.Rows.Count];
        if (dtCon.Rows.Count > 0)
        {
            for (int i = 0; i < dtCon.Rows.Count; i++)
            {
                filterlist[i] = dtCon.Rows[i]["Contact_Name"].ToString() + "/" + dtCon.Rows[i]["Contact_Id"].ToString();
            }
        }
        return filterlist;
    }

    [System.Web.Services.WebMethodAttribute(), System.Web.Script.Services.ScriptMethodAttribute()]
    public static string[] GetCompletionListProductName(string prefixText, int count, string contextKey)
    {
        Inv_ProductMaster ObjInvProductMaster = new Inv_ProductMaster();
        DataTable dt = ObjInvProductMaster.GetProductMasterTrueAll(HttpContext.Current.Session["CompId"].ToString());
        DataTable dtTemp = dt.Copy();

        dt = new DataView(dt, "EProductName like '" + prefixText.ToString() + "%'", "", DataViewRowState.CurrentRows).ToTable();
        if (dt.Rows.Count == 0)
        {
            dt = dtTemp.Copy();

        }

        string[] txt = new string[dt.Rows.Count];


        for (int i = 0; i < dt.Rows.Count; i++)
        {
            txt[i] = dt.Rows[i]["EProductName"].ToString();
        }


        return txt;
    }

    #endregion
    public void txtCommon_Click(object sender, EventArgs e)
    {
        TextBox txt = (TextBox)sender;
        if (txt.Text == "")
        {
            txt.Text = "0";
        }
      
    }


    protected void txtTotalWeight_TextChanged(object sender, EventArgs e)
    {
        txtCommon_Click(sender, null);
        
    }
    protected void txtUnitRate_TextChanged(object sender, EventArgs e)
    {
        txtCommon_Click(sender, null);
    }
}
