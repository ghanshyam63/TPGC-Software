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
using System.Web.Services;
using net.webservicex.www;

public partial class Purchase_PurchaseInvoice : BasePage
{
    #region Defind Class Object
    Inv_ProductMaster ObjProductMaster = new Inv_ProductMaster();
    Inv_UnitMaster objUnit = new Inv_UnitMaster();
    PurchaseOrderHeader ObjPurchaseOrder = new PurchaseOrderHeader();
    PurchaseOrderDetail ObjPurchaseOrderDetail = new PurchaseOrderDetail();
    SystemParameter ObjSysParam = new SystemParameter();
    CurrencyMaster ObjCurrencyMaster = new CurrencyMaster();
    Ems_ContactMaster ObjContactMaster = new Ems_ContactMaster();
    PurchaseInvoice ObjPurchaseInvoice = new PurchaseInvoice();
    PurchaseInvoiceDetail ObjPurchaseInvoiceDetail = new PurchaseInvoiceDetail();
    Inv_ShipExpMaster ObjShipExp = new Inv_ShipExpMaster();
    Inv_ShipExpDetail ObjShipExpDetail = new Inv_ShipExpDetail();
    Inv_ShipExpHeader ObjShipExpHeader = new Inv_ShipExpHeader();
    Common cmn = new Common();
    UserMaster ObjUser = new UserMaster();
    Set_DocNumber objDocNo = new Set_DocNumber();
    net.webservicex.www.CurrencyConvertor obj = new net.webservicex.www.CurrencyConvertor();
    net.webservicex.www.Currency Currency = new net.webservicex.www.Currency();

    string StrCompId = string.Empty;
    string StrBrandId = string.Empty;
    string StrLocationId = string.Empty;
    string UserId = string.Empty;
    #endregion
    protected void Page_Load(object sender, EventArgs e)
    {
        StrCompId = Session["CompId"].ToString();
        UserId = Session["UserId"].ToString();

        if (!IsPostBack)
        {
            StrBrandId = Session["BrandId"].ToString();
            StrLocationId = Session["LocId"].ToString();
            txtInvoiceNo.Text = GetDocumentNumber();
            txtInvoicedate.Text = DateTime.Now.ToString(ObjSysParam.GetSysParameterByParamName("Date_Format").Rows[0]["Param_Value"].ToString());
            txtSupInvoiceDate.Text = DateTime.Now.ToString(ObjSysParam.GetSysParameterByParamName("Date_Format").Rows[0]["Param_Value"].ToString());

            fillGrid();
            FillCurrency(ddlCurrency);
            FillCurrency(ddlExpCurrency);
            FillUnit();
            fillExpenses();
            btnList_Click(null, null);
            btnReset_Click(null, null);
        }
        AllPageCode();
    }
    #region System Defind Funcation:-Events

    protected void btnSave_Click(object sender, EventArgs e)
    {
        if (txtInvoicedate.Text == "")
        {

            DisplayMessage("Enter Invoice Date");
            txtInvoicedate.Focus();

            txtInvoicedate.Text = DateTime.Now.ToString(ObjSysParam.GetSysParameterByParamName("Date_Format").Rows[0]["Param_Value"].ToString());
            return;
           

        }
        if (txtSupInvoiceDate.Text == "")
        {
            DisplayMessage("Select Supplier Invoice Date");
            txtSupInvoiceDate.Focus();
            txtSupInvoiceDate.Text = DateTime.Now.ToString(ObjSysParam.GetSysParameterByParamName("Date_Format").Rows[0]["Param_Value"].ToString());
            return;
        }




        if (txtInvoiceNo.Text == "")
        {
            txtInvoiceNo.Text = "";
            txtInvoiceNo.Focus();
            DisplayMessage("Enter Invoice No");
            return;
        }
        if (txtSupplierInvoiceNo.Text == "")
        {
            txtSupplierInvoiceNo.Text = "";
            txtSupplierInvoiceNo.Focus();
            DisplayMessage("Enter Supplier Invoice No");
            return;
        }

        if (txtSupplierName.Text == "")
        {
            txtSupplierName.Focus();
            DisplayMessage("Enter Supplier Name");
            return;

        }
       
        if (txtCostingRate.Text == "")
        {
            txtCostingRate.Text = "0";
        }
        if (txtOtherCharges.Text == "")
        {
            txtOtherCharges.Text = "0";
        }
        if (!RdoPo.Checked && !RdoWithOutPo.Checked)
        {

            DisplayMessage("Select One With Purchase Order Or WithOut Purchase Order");
            RdoPo.Focus();
            return;
        }
        if (RdoWithOutPo.Checked)
        {
            if (gvProduct.Rows.Count == 0)
            {
                DisplayMessage("Select Product");
                btnProductSave.Focus();
                return;
            }
        }
        if (HdnEdit.Value == "")
        {
            if (RdoPo.Checked)
            {
                bool TrFl = false;
                foreach (GridViewRow Row in gvPurchaseOrder.Rows)
                {
                    CheckBox CHk = (CheckBox)Row.FindControl("ChkPoId");
                    if (CHk.Checked)
                    {
                        TrFl = true;
                    }
                }
                if (!TrFl)
                {
                    DisplayMessage("Select Purchase Order No ");
                    gvPurchaseOrder.Focus();
                    return;

                }
            }
        }
        if (ddlCurrency.SelectedIndex == 0)
        {
            ddlCurrency.Focus();
            DisplayMessage("Select Currency");
            return;

        }


        string strTotalQty = string.Empty;
        string strAmount = string.Empty;
        string PoId = string.Empty;
        bool Post = false;

        if (RdoOpen.Checked)
        {
            Post = true;
        }
        if (!RdoOpen.Checked && !RdoClose.Checked)
        {

            DisplayMessage("Select Post");
            return;
        }
        string POst = string.Empty;
        if (RdoPo.Checked)
        {
            POst = "PO";
        }
        if (RdoWithOutPo.Checked)
        {
            POst = "WOutPO";
        }
        strTotalQty = "0";
        strAmount = "0";
        int b = 0;
        string InvoiceId = "0";
        if (HdnEdit.Value == "")
        {

            b = ObjPurchaseInvoice.InsertPurchaseInvoiceHeader(StrCompId.ToString(), StrBrandId.ToString(), StrLocationId.ToString(), txtInvoiceNo.Text.Trim(), txtInvoicedate.Text.Trim(), ddlPaymentMode.SelectedValue.ToString(), txtSupInvoiceDate.Text.Trim(), txtSupplierInvoiceNo.Text.Trim(), ddlInvoiceType.SelectedValue.ToString(), txtSupplierName.Text.Split('/')[1].ToString(), ddlCurrency.SelectedValue.ToString(), txtExchangeRate.Text.Trim(), txtCostingRate.Text.Trim(), txtOtherCharges.Text.Trim(), strAmount.ToString(), strTotalQty.ToString(), txtNetTaxPar.Text.Trim(), txtNetTaxVal.Text.Trim(), txtNetAmount.Text.Trim(), txtNetDisPer.Text.Trim(), txtNetDisVal.Text.Trim(), txtGrandTotal.Text.Trim(), Post.ToString(), txRemark.Text.Trim(), POst, "", "", "", "", true.ToString(), DateTime.Now.ToString(), true.ToString(), UserId.ToString(), DateTime.Now.ToString(), UserId.ToString(), DateTime.Now.ToString());
            InvoiceId = (Convert.ToInt32(ObjPurchaseInvoice.getAutoId()) - 1).ToString();
            try
            {
                ObjShipExpHeader.ShipExpHeader_Insert(StrCompId.ToString(), StrBrandId.ToString(), StrLocationId.ToString(), InvoiceId.ToString(), ddlCurrency.SelectedValue.ToString(), txtExchangeRate.Text.Trim(), txtCostingRate.Text.Trim(), txtGrandTotal.Text.Trim(), ((Label)GridExpenses.FooterRow.FindControl("txttotExp")).Text.Trim(), "", "", "", "", "", true.ToString(), DateTime.Now.ToString(), true.ToString(), UserId.ToString(), DateTime.Now.ToString(), UserId.ToString(), DateTime.Now.ToString());
            }
            catch
            {

            }
        }
        else
        {
            b = ObjPurchaseInvoice.UpdatePurchaseInvoiceHeader(StrCompId.ToString(), StrBrandId.ToString(), StrLocationId.ToString(), HdnEdit.Value.Trim(), txtInvoiceNo.Text.Trim(), txtInvoicedate.Text.Trim(), ddlPaymentMode.SelectedValue.ToString(), txtSupInvoiceDate.Text.Trim(), txtSupplierInvoiceNo.Text.Trim(), ddlInvoiceType.SelectedValue.ToString(), txtSupplierName.Text.Split('/')[1].ToString(), ddlCurrency.SelectedValue.ToString(), txtExchangeRate.Text.Trim(), txtCostingRate.Text.Trim(), txtOtherCharges.Text.Trim(), strAmount.ToString(), strTotalQty.ToString(), txtNetTaxPar.Text.Trim(), txtNetTaxVal.Text.Trim(), txtNetAmount.Text.Trim(), txtNetDisPer.Text.Trim(), txtNetDisVal.Text.Trim(), txtGrandTotal.Text.Trim(), Post.ToString(), txRemark.Text.Trim(), POst, "", "", "", "", true.ToString(), DateTime.Now.ToString(), true.ToString(), UserId.ToString(), DateTime.Now.ToString());
            try
            {
                ObjShipExpHeader.ShipExpHeader_Update(StrCompId.ToString(), StrBrandId.ToString(), StrLocationId.ToString(), HdnEdit.Value.Trim().ToString(), ddlCurrency.SelectedValue.ToString(), txtExchangeRate.Text.Trim(), txtCostingRate.Text.Trim(), txtGrandTotal.Text.Trim(), ((Label)GridExpenses.FooterRow.FindControl("txttotExp")).Text.Trim(), "", "", "", "", "", true.ToString(), DateTime.Now.ToString(), true.ToString(), UserId.ToString(), DateTime.Now.ToString(), UserId.ToString(), DateTime.Now.ToString());
            }
            catch
            {

            }
        }
        if (b != 0)
        {

            if (RdoPo.Checked)
            {
                foreach (GridViewRow gridPORow in gvPurchaseOrder.Rows)
                {
                    if (HdnEdit.Value == "")
                    {
                        if (((CheckBox)gridPORow.FindControl("ChkPoId")).Checked)
                        {
                            PoId = ((Label)gridPORow.FindControl("lblPoId")).Text.Trim().ToString();
                            foreach (GridViewRow GridRow in ((GridView)gridPORow.FindControl("gvProduct")).Rows)
                            {
                                Label LblTransId = (Label)GridRow.FindControl("lblTransId");
                                Label LblProductId = (Label)GridRow.FindControl("lblProductId");
                                Label LblUnitId = (Label)GridRow.FindControl("lblUnitId");
                                Label lblUnitCost = (Label)GridRow.FindControl("lblUnitRate");
                                Label lblOrderQty = (Label)GridRow.FindControl("lblReqQty");
                                Label lblFreeQty = (Label)GridRow.FindControl("lblFreeQty");
                                TextBox txtRecQty = (TextBox)GridRow.FindControl("txtRecQty");
                                TextBox txtTaxvalue = (TextBox)GridRow.FindControl("txtTax");
                                TextBox txtDisvalue = (TextBox)GridRow.FindControl("txtDiscount");

                                if (txtRecQty.Text != "0")
                                {

                                    ObjPurchaseInvoiceDetail.InsertPurchaseInvoiceDetail(StrCompId.ToString(), StrBrandId.ToString(), StrLocationId.ToString(), InvoiceId.ToString(), GridRow.RowIndex + 1.ToString(), LblProductId.Text.Trim(), PoId.ToString(), LblUnitId.Text, lblUnitCost.Text, lblOrderQty.Text, lblFreeQty.Text.Trim(), txtRecQty.Text, "0", txtDisvalue.Text.Trim(), txtTaxvalue.Text.Trim(), "", "", "", "", "", true.ToString(), DateTime.Now.ToString(), true.ToString(), UserId.ToString(), DateTime.Now.ToString(), UserId.ToString(), DateTime.Now.ToString());
                                }

                            }
                        }
                    }

                    else
                    {
                        PoId = ((Label)gridPORow.FindControl("lblPoId")).Text.Trim().ToString();
                        foreach (GridViewRow GridRow in ((GridView)gridPORow.FindControl("gvProduct")).Rows)
                        {
                            Label LblTransId = (Label)GridRow.FindControl("lblTransId");
                            Label LblProductId = (Label)GridRow.FindControl("lblProductId");
                            Label LblUnitId = (Label)GridRow.FindControl("lblUnitId");
                            Label lblUnitCost = (Label)GridRow.FindControl("lblUnitRate");
                            Label lblOrderQty = (Label)GridRow.FindControl("lblReqQty");
                            Label lblFreeQty = (Label)GridRow.FindControl("lblFreeQty");
                            TextBox txtRecQty = (TextBox)GridRow.FindControl("txtRecQty");
                            TextBox txtTaxvalue = (TextBox)GridRow.FindControl("txtTax");
                            TextBox txtDisvalue = (TextBox)GridRow.FindControl("txtDiscount");
                            if (txtRecQty.Text == "0")
                            {
                                ObjPurchaseInvoiceDetail.DeletePurchaseInvoiceDetail(StrCompId.ToString(), StrBrandId.ToString(), StrLocationId.ToString(), LblTransId.Text.Trim());
                            }
                            else
                            {
                                ObjPurchaseInvoiceDetail.UpdatePurchaseInvoiceDetail(StrCompId.ToString(), StrBrandId.ToString(), StrLocationId.ToString(), LblTransId.Text.Trim(), HdnEdit.Value.Trim(), GridRow.RowIndex + 1.ToString(), LblProductId.Text.Trim(), PoId.ToString(), LblUnitId.Text, lblUnitCost.Text, lblOrderQty.Text, lblFreeQty.Text.Trim(), txtRecQty.Text, "0", txtDisvalue.Text.Trim(), txtTaxvalue.Text.Trim(), "", "", "", "", "", true.ToString(), DateTime.Now.ToString(), true.ToString(), UserId.ToString(), DateTime.Now.ToString());
                            }
                        }
                    }
                }

            }
            DisplayMessage("Record Saved");
            Reset();
        }
    }
    protected void btnReset_Click(object sender, EventArgs e)
    {
        if (HdnEdit.Value == "")
        {
            string Id = ObjPurchaseInvoice.getAutoId();
            ObjPurchaseInvoiceDetail.DeletePurchaseInvoiceDetailByInvoiceNo(StrCompId.ToString(), StrBrandId.ToString(), StrLocationId.ToString(), Id.ToString());
            ObjShipExpDetail.ShipExpDetail_Delete(StrCompId.ToString(), StrBrandId.ToString(), StrLocationId.ToString(), Id.ToString());
        }
        Reset();
    }
    protected void btnCancel_Click(object sender, EventArgs e)
    {
        if (HdnEdit.Value == "")
        {
            string Id = ObjPurchaseInvoice.getAutoId();
            ObjPurchaseInvoiceDetail.DeletePurchaseInvoiceDetailByInvoiceNo(StrCompId.ToString(), StrBrandId.ToString(), StrLocationId.ToString(), Id.ToString());
            ObjShipExpDetail.ShipExpDetail_Delete(StrCompId.ToString(), StrBrandId.ToString(), StrLocationId.ToString(), Id.ToString());
        }
        Reset();
        btnRefresh_Click(null, null);
        btnList_Click(null, null);
    }
    protected void txtSupplierName_TextChanged(object sender, EventArgs e)
    {
        if (txtSupplierName.Text != "")
        {
            DataTable dt = ObjContactMaster.GetContactByContactName(StrCompId.ToString(), txtSupplierName.Text.Trim().Split('/')[0].ToString());
            if (dt.Rows.Count == 0)
            {
                DisplayMessage("Select Supplier Name");
                txtSupplierName.Text = "";
                txtSupplierName.Focus();

            }
            else
            {
                RdoPo_CheckedChanged(null, null);
            }

        }
        else
        {
            DisplayMessage("Select Supplier Name");
            txtSupplierName.Focus();

        }

    }
    protected void btnList_Click(object sender, EventArgs e)
    {
        pnlMenuList.BackColor = System.Drawing.ColorTranslator.FromHtml("#ccddee");


        pnlMenuNew.BackColor = System.Drawing.ColorTranslator.FromHtml("#90bde9");


        PnlList.Visible = true;
        PnlNewEdit.Visible = false;
        PnlBin.Visible = false;
        txtValue.Focus();
    }
    protected void btnNew_Click(object sender, EventArgs e)
    {
        pnlMenuNew.BackColor = System.Drawing.ColorTranslator.FromHtml("#ccddee");

        pnlMenuList.BackColor = System.Drawing.ColorTranslator.FromHtml("#90bde9");


        PnlList.Visible = false;
        PnlNewEdit.Visible = true;
        PnlBin.Visible = false;

        txtInvoiceNo.Focus();

    }
    protected void gvProduct_RowCreated(object sender, GridViewRowEventArgs e)
    {
        GridView gvProduct = (GridView)sender;
        if (e.Row.RowType == DataControlRowType.Header)
        {
            GridView HeaderGrid = (GridView)sender;
            GridViewRow row = new GridViewRow(0, 0, DataControlRowType.Header, DataControlRowState.Insert);

            TableHeaderCell cell = new TableHeaderCell();
            cell.Text = Resources.Attendance.Product_Description;
            cell.ColumnSpan = 2;
            row.Controls.Add(cell);

            cell = new TableHeaderCell();
            cell.ColumnSpan = 2;
            cell.Text = Resources.Attendance.Unit;
            row.Controls.Add(cell);


            cell = new TableHeaderCell();
            cell.ColumnSpan = 4;
            cell.Text = Resources.Attendance.Quantity;
            row.Controls.Add(cell);

            cell = new TableHeaderCell();
            cell.ColumnSpan = 3;
            cell.Text = Resources.Attendance.Value;
            row.Controls.Add(cell);
            cell = new TableHeaderCell();

            gvProduct.Controls[0].Controls.Add(row);



        }
    }
    protected void txtRecQty_TextChanged(object sender, EventArgs e)
    {
        GridView gvProduct = (GridView)((TextBox)sender).NamingContainer.NamingContainer;

        try
        {



            ((Label)gvProduct.FooterRow.Cells[7].FindControl("txtTotalQuantity")).Text = "0";
            Total(gvProduct);
        }
        catch
        {
            ((TextBox)sender).Text = "0";

            ((TextBox)sender).Focus();
            Total(gvProduct);
        }
    }

    protected void btnClosePanel_Click(object sender, ImageClickEventArgs e)
    {
        pnlProduct1.Visible = false;
        pnlProduct2.Visible = false;
        pnlAddOrderProduct.Visible = false;
        pnlAddOrderProduct.Visible = false;
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
        if (txtUnitCost.Text == "" )
        {
            DisplayMessage("Ener Unit Cost");
            txtUnitCost.Focus();
            return;
        }
      

        if (txtfreeQty.Text == "")
        {
            txtfreeQty.Text = "0";
        }
        if (HdnEdit.Value == "")
        {
            ReqId = ObjPurchaseInvoice.getAutoId();
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
        int SerialNO = 0;
        if (hidProduct.Value == "")
        {
            DataTable dtProduct = ObjPurchaseInvoiceDetail.GetPurchaseInvoiceDetailByInvoiceNo(StrCompId, StrBrandId, StrLocationId, ReqId.Trim());
            if (dtProduct.Rows.Count > 0)
            {
                dtProduct = new DataView(dtProduct, "", "SerialNo Desc", DataViewRowState.CurrentRows).ToTable();
                SerialNO = Convert.ToInt32(dtProduct.Rows[0]["SerialNo"].ToString());
                SerialNO += 1;
            }
            else
            {
                SerialNO = 1;
            }



        }
        else
        {
            SerialNO = Convert.ToInt32(ViewState["SerialNo"].ToString());
          
        }
        if (hidProduct.Value == "")
        {
            ObjPurchaseInvoiceDetail.InsertPurchaseInvoiceDetail(StrCompId.ToString(), StrBrandId.ToString(), StrLocationId.ToString(), ReqId.ToString(),SerialNO.ToString(), ProductId.ToString(), "0", UnitId.ToString(), txtUnitCost.Text.ToString(), (Convert.ToDouble(txtReceivedQty.Text.Trim()) - Convert.ToDouble(txtfreeQty.Text.Trim())).ToString(), txtfreeQty.Text.ToString(), txtReceivedQty.Text.Trim(), "0", txtPDisValue.Text.Trim(), txtPTaxVal.Text.Trim(), "", "", "", "", "", true.ToString(), DateTime.Now.ToString(), true.ToString(), UserId.ToString(), DateTime.Now.ToString(), UserId.ToString(), DateTime.Now.ToString());
        }
        else
        {
            ObjPurchaseInvoiceDetail.UpdatePurchaseInvoiceDetail(StrCompId.ToString(), StrBrandId.ToString(), StrLocationId.ToString(), hidProduct.Value.ToString(), ReqId.ToString(),SerialNO.ToString(), ProductId.ToString(), "0", UnitId.ToString(), txtUnitCost.Text.ToString(), txtReceivedQty.Text.Trim(), txtfreeQty.Text.ToString(), txtReceivedQty.Text.Trim(), "0", txtPDisValue.Text.Trim(), txtPTaxVal.Text.Trim(), "", "", "", "", "", true.ToString(), DateTime.Now.ToString(), true.ToString(), UserId.ToString(), DateTime.Now.ToString());
            pnlProduct1.Visible = false;
            pnlProduct2.Visible = false;
        }
        fillgridDetail();

        NetTotal();
        txtNetTaxPar_TextChanged(null, null);
        txtNetDisPer_TextChanged(null, null);
        ResetDetail();


    }


    protected void txtNetTaxPar_TextChanged(object sender, EventArgs e)
    {
        if (txtNetTaxPar.Text == "")
        {
            txtNetTaxPar.Text = "0";

        }

        txtNetTaxVal.Text = "0";
        txtNetDisPer_TextChanged(null, null);
    }
    protected void txtNetTaxVal_TextChanged(object sender, EventArgs e)
    {
        if (txtNetTaxVal.Text == "")
        {
            txtNetTaxVal.Text = "0";

        }

        txtNetTaxPar.Text = "0";

        txtNetDisVal_TextChanged(null, null);
    }
    protected void txtNetDisPer_TextChanged(object sender, EventArgs e)
    {
        if (txtNetDisPer.Text == "")
        {
            txtNetDisPer.Text = "0";

        }

        txtNetDisVal.Text = "0";

        NetTotal();
    }
    protected void txtNetDisVal_TextChanged(object sender, EventArgs e)
    {
        if (txtNetDisVal.Text == "")
        {
            txtNetDisVal.Text = "0";

        }

        txtNetDisPer.Text = "0";

        NetTotal();

    }
    protected void txtInvoiceNo_TextChanged(object sender, EventArgs e)
    {
        if (txtInvoiceNo.Text != "")
        {
            DataTable dt = new DataView(ObjPurchaseInvoice.GetPurchaseInvoiceHeader(StrCompId.ToString(), StrBrandId.ToString(), StrLocationId.ToString()), "InvoiceNo='" + txtInvoiceNo.Text.Trim() + "'", "", DataViewRowState.CurrentRows).ToTable();
            if (dt.Rows.Count != 0)
            {
                if (Convert.ToBoolean(dt.Rows[0]["IsActive"].ToString()))
                {
                    DisplayMessage("Purchase Invoice No Already Exist");
                    txtInvoiceNo.Text = "";
                    txtInvoiceNo.Focus();
                }
                else
                {
                    DisplayMessage("Purchase Invoice No Already Exist :- Go To Bin");
                    txtInvoiceNo.Text = "";
                    txtInvoiceNo.Focus();

                }

            }
            else
            {
                ddlInvoiceType.Focus();
            }


        }

    }
    protected void ChkPoId_CheckedChanged(object sender, EventArgs e)
    {
        CheckBox chk = ((CheckBox)sender);
        GridViewRow Row = (GridViewRow)((CheckBox)sender).Parent.Parent;
        if (chk.Checked == true)
        {
            fillPOgridDetail((GridView)Row.FindControl("gvProduct"), ((Label)Row.FindControl("lblPoId")).Text.Trim());
        }
        else
        {

            ((GridView)Row.FindControl("gvProduct")).DataSource = null;
            ((GridView)Row.FindControl("gvProduct")).DataBind();
            ((Label)((GridView)Row.FindControl("gvProduct")).FooterRow.FindControl("txtTotalQuantity")).Text = "0";
            ((Label)((GridView)Row.FindControl("gvProduct")).FooterRow.FindControl("txtTotalAmount")).Text = "0";
        }
        NetTotal();
        bool b = false;

        foreach (GridViewRow GridRow in gvPurchaseOrder.Rows)
        {
            if (((CheckBox)GridRow.FindControl("ChkPoId")).Checked)
            {
                b = true;
            }


        }
        if (!b)
        {
            gvPurchaseOrder.FooterRow.Visible = false;
        }
        else
        {
            gvPurchaseOrder.FooterRow.Visible = true;
            ((Label)gvPurchaseOrder.FooterRow.FindControl("lblTotalQuantity")).Text = Resources.Attendance.Total_Quantity;
            ((Label)gvPurchaseOrder.FooterRow.FindControl("lblTotalAmount")).Text = Resources.Attendance.Total_Amount;
            if (((Label)gvPurchaseOrder.FooterRow.FindControl("txtTotalQuantity")).Text == "")
            {
                ((Label)gvPurchaseOrder.FooterRow.FindControl("txtTotalQuantity")).Text = "0";
            }
            if (((Label)gvPurchaseOrder.FooterRow.FindControl("txtTotalAmount")).Text == "")
            {
                ((Label)gvPurchaseOrder.FooterRow.FindControl("txtTotalAmount")).Text = "0";
            }
        }
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
            DataTable dtCust = (DataTable)Session["DtPurchaseInvocie"];


            DataView view = new DataView(dtCust, condition, "", DataViewRowState.CurrentRows);
            Session["dtFilter"] = view.ToTable();
            lblTotalRecords.Text = Resources.Attendance.Total_Records + ": " + view.ToTable().Rows.Count.ToString() + "";

            GvPurchaseInvocie.DataSource = view.ToTable();
            GvPurchaseInvocie.DataBind();
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
        DataTable dt = ObjPurchaseInvoice.GetPurchaseInvoiceTrueAllByTransId(StrCompId.ToString(), StrBrandId.ToString(), StrLocationId.ToString(), e.CommandArgument.ToString());
        if (dt.Rows.Count != 0)
        {
            btnNew_Click(null, null);
            HdnEdit.Value = e.CommandArgument.ToString();
            txtInvoicedate.Text = GetDateFormat(dt.Rows[0]["InvoiceDate"].ToString());
            txtInvoiceNo.Text = dt.Rows[0]["InvoiceNo"].ToString();
            txtSupInvoiceDate.Text = GetDateFormat(dt.Rows[0]["SupInvoiceDate"].ToString());
            txtSupplierInvoiceNo.Text = dt.Rows[0]["SupInvoiceNo"].ToString();
            txtInvoiceNo.Enabled = false;
            ddlInvoiceType.SelectedValue = dt.Rows[0]["InvoiceType"].ToString();

            if (Convert.ToBoolean(dt.Rows[0]["Post"].ToString()))
            {
                RdoOpen.Checked = true;
                RdoClose.Checked = false;
            }
            else
            {
                RdoOpen.Checked = false;
                RdoClose.Checked = true;
            }
            txtSupplierName.Text = ObjContactMaster.GetContactByContactId(StrCompId.ToString(), dt.Rows[0]["SupplierId"].ToString()).Rows[0]["Contact_Name"].ToString() + "/" + dt.Rows[0]["SupplierId"].ToString();
            txtSupplierName_TextChanged(null, null);
            if (dt.Rows[0]["Field1"].ToString() == "PO")
            {
                RdoPo.Checked = true;
                RdoWithOutPo.Checked = false;
                btnAddOrderProduct.Visible = true;
            }
            if (dt.Rows[0]["Field1"].ToString() == "WOutPO")
            {
                btnAddOrderProduct.Visible = false;
                RdoPo.Checked = false;
                RdoWithOutPo.Checked = true;
            }
            RdoPo_CheckedChanged(null, null);
            txtNetTaxPar.Text = dt.Rows[0]["NetTax"].ToString();
            txtNetTaxVal.Text = dt.Rows[0]["NetTaxValue"].ToString();
            txtNetAmount.Text = dt.Rows[0]["NetAmount"].ToString();
            txtNetDisPer.Text = dt.Rows[0]["NetDiscount"].ToString();
            txtNetDisVal.Text = dt.Rows[0]["NetDiscountValue"].ToString();
            txtGrandTotal.Text = dt.Rows[0]["GrandTotal"].ToString();
            ddlPaymentMode.SelectedValue = dt.Rows[0]["PaymentModeID"].ToString();
            try
            {
                ddlCurrency.SelectedValue = dt.Rows[0]["CurrencyId"].ToString();
            }
            catch
            {

            }

            txtExchangeRate.Text = dt.Rows[0]["ExchangeRate"].ToString();

            txtCostingRate.Text = dt.Rows[0]["CostingRate"].ToString();
            txRemark.Text = dt.Rows[0]["Remark"].ToString();
            txtOtherCharges.Text = dt.Rows[0]["OtherCharges"].ToString();

            fillExpGrid(HdnEdit.Value.Trim());



        }


    }
    //Start :- Code is due some logical reason
    protected void IbtnDelete_Command(object sender, CommandEventArgs e)
    {

    }
    //End
    protected void GvPurchaseInvocie_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        GvPurchaseInvocie.PageIndex = e.NewPageIndex;

        DataTable dt = (DataTable)Session["dtFilter"];
        GvPurchaseInvocie.DataSource = dt;
        GvPurchaseInvocie.DataBind();
        AllPageCode();
        GvPurchaseInvocie.BottomPagerRow.Focus();

    }
    protected void GvPurchaseInvocie_OnSorting(object sender, GridViewSortEventArgs e)
    {

        HDFSort.Value = HDFSort.Value == "ASC" ? "DESC" : "ASC";
        DataTable dt = new DataTable();
        dt = (DataTable)Session["dtFilter"];
        DataView dv = new DataView(dt);
        string Query = "" + e.SortExpression + " " + HDFSort.Value + "";
        dv.Sort = Query;
        dt = dv.ToTable();
        Session["dtFilter"] = dt;
        GvPurchaseInvocie.DataSource = dt;
        GvPurchaseInvocie.DataBind();
        AllPageCode();
        GvPurchaseInvocie.HeaderRow.Focus();

    }


    protected void ddlCurrency_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (ddlCurrency.SelectedIndex != 0)
        {
            try
            {
                txtExchangeRate.Text = Convert.ToDouble(obj.ConversionRate((Currency)System.Enum.Parse(Currency.GetType(), ObjCurrencyMaster.GetCurrencyMasterById(ddlCurrency.SelectedValue.ToString()).Rows[0]["Currency_Code"].ToString()), (Currency)System.Enum.Parse(Currency.GetType(), "KWD"))).ToString();

            }
            catch
            {
                DataTable dt = new DataView(ObjCurrencyMaster.GetCurrencyMaster(), "Currency_Code='" + ddlCurrency.SelectedValue.ToString() + "'", "", DataViewRowState.CurrentRows).ToTable();
                if (dt.Rows.Count != 0)
                {
                    txtExpExchangeRate.Text = dt.Rows[0]["Currency_Value"].ToString();
                }
            }
        }
    }
    protected void ddlExpCurrency_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (ddlExpCurrency.SelectedIndex != 0)
        {

            try
            {
                txtExpExchangeRate.Text = Convert.ToDouble(obj.ConversionRate((Currency)System.Enum.Parse(Currency.GetType(), ObjCurrencyMaster.GetCurrencyMasterById(ddlExpCurrency.SelectedValue.ToString()).Rows[0]["Currency_Code"].ToString()), (Currency)System.Enum.Parse(Currency.GetType(), "KWD"))).ToString();
            }
            catch
            {
                DataTable dt = new DataView(ObjCurrencyMaster.GetCurrencyMaster(), "Currency_Code='" + ddlCurrency.SelectedValue.ToString() + "'", "", DataViewRowState.CurrentRows).ToTable();
                if (dt.Rows.Count != 0)
                {
                    txtExpExchangeRate.Text = dt.Rows[0]["Currency_Value"].ToString();
                }

            }
            if (txtExpExchangeRate.Text != "")
            {
                txtExpCharges.Text = (float.Parse(txtFCExpAmount.Text.Trim()) * float.Parse(txtExpExchangeRate.Text.Trim())).ToString();
            }
        }
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
                    if (hidProduct.Value == "")
                    {
                      string ReqId = ObjPurchaseInvoice.getAutoId();
                        DataTable dtProduct = ObjPurchaseInvoiceDetail.GetPurchaseInvoiceDetailByInvoiceNo(StrCompId, StrBrandId, StrLocationId, ReqId.Trim());
                        if (dtProduct.Rows.Count > 0)
                        {
                            dtProduct = new DataView(dtProduct, "ProductId=" + dt.Rows[0]["ProductId"].ToString() + "", "", DataViewRowState.CurrentRows).ToTable();
                            if (dtProduct.Rows.Count > 0)
                            {
                                DisplayMessage("Product Is already exists!");
                                txtProductName.Focus();
                                txtProductName.Text = "";

                                return;

                            }
                        }
                    }


                    txtPDescription.Text = dt.Rows[0]["Description"].ToString();

                    string strUnitId = dt.Rows[0]["UnitId"].ToString();
                    if (strUnitId != "0" && strUnitId != "")
                    {
                        ddlUnit.SelectedValue = strUnitId;
                    }
                    else
                    {
                        FillUnit();
                    }

                }
                else
                {
                    FillUnit();

                    txtPDescription.Text = "";
                    txtPDescription.Focus();

                }
                txtReceivedQty.Text = "1";

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
    protected void btnPDEdit_Command(object sender, CommandEventArgs e)
    {
        pnlProduct1.Visible = true;
        pnlProduct2.Visible = true;
        hidProduct.Value = e.CommandArgument.ToString();
        DataTable dt = ObjPurchaseInvoiceDetail.GetPurchaseInvoiceDetail(StrCompId.ToString(), StrBrandId.ToString(), StrLocationId.ToString(), e.CommandArgument.ToString());
        if (dt.Rows.Count != 0)
        {
            if (dt.Rows[0]["ProductId"].ToString() != "0")
            {
                txtProductName.Text = ProductName(dt.Rows[0]["ProductId"].ToString());
                txtProductName_TextChanged(null, null);
            }

            ddlUnit.SelectedValue = dt.Rows[0]["UnitId"].ToString();

            txtfreeQty.Text = dt.Rows[0]["FreeQty"].ToString();
            txtReceivedQty.Text = dt.Rows[0]["InvoiceQty"].ToString();

            txtUnitCost.Text = dt.Rows[0]["UnitCost"].ToString();

            ProductTotal();
            txtPTaxVal.Text = dt.Rows[0]["TaxValue"].ToString();
            txtPTaxVal_TextChanged(null, null);
            txtPDisValue.Text = dt.Rows[0]["DiscountValue"].ToString();
            ViewState["SerialNo"] = dt.Rows[0]["SerialNo"].ToString();

            txtPDisValue_TextChanged(null, null);


        }

    }
    protected void IbtnPDDelete_Command(object sender, CommandEventArgs e)
    {
        ObjPurchaseInvoiceDetail.DeletePurchaseInvoiceDetail(StrCompId.ToString(), StrBrandId.ToString(), StrLocationId.ToString(), e.CommandArgument.ToString());
        fillgridDetail();

    }
    protected void RdoPo_CheckedChanged(object sender, EventArgs e)
    {
        if (txtSupplierName.Text != "")
        {
            if (RdoPo.Checked)
            {
                gvProduct.DataSource = null;
                gvProduct.DataBind();
                btnAddProduct.Visible = false;
                gvProduct.Visible = false;
                gvPurchaseOrder.Visible = true;
                DataTable dtPurchaseOrder = new DataView(ObjPurchaseOrder.GetPurchaseOrderTrueAll(StrCompId.ToString(), StrBrandId.ToString(), StrLocationId.ToString()), "SupplierId='" + txtSupplierName.Text.Trim().Split('/')[1].ToString() + "'", "", DataViewRowState.CurrentRows).ToTable();

                if (HdnEdit.Value != "")
                {
                    DataTable dtTemp = dtPurchaseOrder.Copy();
                    dtPurchaseOrder = new DataTable();
                    DataTable dtPIDe = ObjPurchaseInvoiceDetail.GetPurchaseInvoiceDetailByInvoiceNo(StrCompId.ToString(), StrBrandId.ToString(), StrLocationId.ToString(), HdnEdit.Value.ToString()).DefaultView.ToTable(true, "POId"); ;
                    for (int i = 0; i < dtPIDe.Rows.Count; i++)
                    {
                        dtPurchaseOrder.Merge(new DataView(dtTemp, "TransId='" + dtPIDe.Rows[i]["POId"].ToString() + "'", "", DataViewRowState.CurrentRows).ToTable());
                    }

                }

                if (dtPurchaseOrder.Rows.Count != 0)
                {
                    gvPurchaseOrder.DataSource = dtPurchaseOrder;
                    gvPurchaseOrder.DataBind();
                    AllPageCode();
                    gvPurchaseOrder.FooterRow.Visible = false;
                    if (HdnEdit.Value != "")
                    {
                        gvPurchaseOrder.FooterRow.Visible = true;
                        ((Label)gvPurchaseOrder.FooterRow.FindControl("lblTotalQuantity")).Text = Resources.Attendance.Total_Quantity;
                        ((Label)gvPurchaseOrder.FooterRow.FindControl("lblTotalAmount")).Text = Resources.Attendance.Total_Amount;

                        foreach (GridViewRow GridRow in gvPurchaseOrder.Rows)
                        {
                            ((CheckBox)GridRow.FindControl("ChkPoId")).Visible = false;
                            fillPOgridDetail((GridView)GridRow.FindControl("gvProduct"), ((Label)GridRow.FindControl("lblPoId")).Text.Trim());
                        }
                    }
                    else
                    {

                        foreach (GridViewRow GridRow in gvPurchaseOrder.Rows)
                        {

                            fillPOgridDetail((GridView)GridRow.FindControl("gvProduct"), ((Label)GridRow.FindControl("lblPoId")).Text.Trim());
                            if (((GridView)GridRow.FindControl("gvProduct")).Rows.Count != 0)
                            {
                                ((GridView)GridRow.FindControl("gvProduct")).DataSource = null;
                                ((GridView)GridRow.FindControl("gvProduct")).DataBind();
                            }
                            else
                            {
                                GridRow.Visible = false;

                            }
                        }
                    }
                }
                else
                {
                    gvPurchaseOrder.DataSource = null;
                    gvPurchaseOrder.DataBind();
                }


            }
            if (RdoWithOutPo.Checked)
            {
                btnAddProduct.Visible = true;
                gvProduct.Visible = true;
                gvPurchaseOrder.Visible = false;

                fillgridDetail();
                gvPurchaseOrder.DataSource = null;
                gvPurchaseOrder.DataBind();
            }
        }
        else
        {
            DisplayMessage("Select Supplier Name");
            txtSupplierName.Text = "";
            txtSupplierName.Focus();
            RdoPo.Checked = false;
            RdoWithOutPo.Checked = false;
        }
    }

    protected void btnProductCancel_Click(object sender, EventArgs e)
    {
        btnClosePanel_Click(null, null);
        // ResetDetail();

    }
    protected void txtReceivedQty_TextChanged(object sender, EventArgs e)
    {
        if (txtReceivedQty.Text == "")
        {
            txtReceivedQty.Text = "0";
        }
        if (txtReceivedQty.Text == "0")
        {
            txtfreeQty.Text = "0";
        }
        ProductTotal();
    }
    protected void txtPTaxPer_TextChanged(object sender, EventArgs e)
    {
        if (txtPTaxPer.Text != "")
        {
            if (txtAmount.Text.Trim() != "0")
            {

                txtPTaxVal.Text = (Convert.ToDouble(txtAmount.Text.Trim()) * Convert.ToDouble(txtPTaxPer.Text.Trim()) / 100).ToString();
                ProductTotal();
            }
            else
            {
                txtPTaxPer.Text = "0";
                txtPTaxVal.Text = "0";
            }
        }
        else
        {
            txtPTaxPer.Text = "0";
        }

    }
    protected void txtPTaxVal_TextChanged(object sender, EventArgs e)
    {
        if (txtPTaxVal.Text != "")
        {
            if (txtAmount.Text != "0")
            {
                txtPTaxPer.Text = (100 * Convert.ToDouble(txtPTaxVal.Text.Trim()) / Convert.ToDouble(txtAmount.Text.Trim())).ToString();
                ProductTotal();
            }
            else
            {
                txtPTaxPer.Text = "0";
                txtPTaxVal.Text = "0";
            }
        }
        else
        {
            txtPTaxVal.Text = "0";
        }
    }
    protected void txtPDisPer_TextChanged(object sender, EventArgs e)
    {
        if (txtPDisPer.Text != "")
        {
            if (txtPAfterTaxAmount.Text != "0")
            {


                double d = Convert.ToDouble(txtPAfterTaxAmount.Text.Trim());
                txtPDisValue.Text = (d * Convert.ToDouble(txtPDisPer.Text.Trim()) / 100).ToString();
                ProductTotal();
            }
            else
            {
                txtPDisValue.Text = "0";
                txtPDisPer.Text = "0";
            }

        }
        else
        {
            txtPDisPer.Text = "0";
        }

    }
    protected void txtPDisValue_TextChanged(object sender, EventArgs e)
    {
        if (txtPDisValue.Text != "")
        {
            if (txtPAfterTaxAmount.Text != "0")
            {

                double d = Convert.ToDouble(txtPAfterTaxAmount.Text.Trim());
                txtPDisPer.Text = (Convert.ToDouble(txtPDisValue.Text.Trim()) * 100 / d).ToString();
                ProductTotal();
            }
            else
            {
                txtPDisValue.Text = "0";
                txtPDisPer.Text = "0";
            }

        }

        else
        {
            txtPDisValue.Text = "0";
        }
    }
    protected void txtUnitCost_TextChanged(object sender, EventArgs e)
    {
        if (txtUnitCost.Text == "")
        {
            txtUnitCost.Text = "0";

        }
        ProductTotal();
        txtPDisPer.Focus();
    }

    protected void txtfreeQty_TextChanged(object sender, EventArgs e)
    {
        if (txtfreeQty.Text == "")
        {
            txtfreeQty.Text = "0";

        }
        ProductTotal();
        txtPDisPer.Focus();

    }
    protected void GridExpenses_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {

    }
    protected void IbtnAddExpenses_Click(object sender, ImageClickEventArgs e)
    {
        string ExpId = string.Empty;
        string InvoiceId = string.Empty;
        if (ddlExpense.SelectedIndex == 0)
        {
            DisplayMessage("Select Expeneses");
            ddlExpense.Focus();
            return;
        }
        else
        {
            ExpId = ddlExpense.SelectedValue.ToString();
        }
        if (txtExpCharges.Text == "")
        {
            DisplayMessage("Enter Expeneses Charges");
            txtExpCharges.Focus();
            return;
        }
        if (ddlExpCurrency.SelectedIndex == 0)
        {
            DisplayMessage("Select Expeneses Currency");
            ddlExpCurrency.Focus();
            return;
        }
        if (HdnEdit.Value != "")
        {
            InvoiceId = HdnEdit.Value.Trim();
        }
        else
        {
            InvoiceId = (Convert.ToInt32(ObjPurchaseInvoice.getAutoId())).ToString();
        }
        DataTable dt = ObjShipExpDetail.Get_ShipExpDetailByInvoiceIdandExpId(StrCompId.ToString(), StrBrandId.ToString(), StrLocationId.ToString(), InvoiceId.ToString(), ExpId.ToString());
        if (dt.Rows.Count != 0)
        {
            ObjShipExpDetail.ShipExpDetail_Update(StrCompId.ToString(), StrBrandId.ToString(), StrLocationId.ToString(), InvoiceId.ToString(), ExpId.ToString(), "NA", txtExpCharges.Text.Trim(), ddlExpCurrency.SelectedValue.ToString(), txtExpExchangeRate.Text.Trim(), txtFCExpAmount.Text.Trim(), "", "", "", "", "", true.ToString(), DateTime.Now.ToString(), true.ToString(), UserId.ToString(), DateTime.Now.ToString(), UserId.ToString(), DateTime.Now.ToString());
        }
        else
        {
            ObjShipExpDetail.ShipExpDetail_Insert(StrCompId.ToString(), StrBrandId.ToString(), StrLocationId.ToString(), InvoiceId.ToString(), ExpId.ToString(), "NA", txtExpCharges.Text.Trim(), ddlExpCurrency.SelectedValue.ToString(), txtExpExchangeRate.Text.Trim(), txtFCExpAmount.Text.Trim(), "", "", "", "", "", true.ToString(), DateTime.Now.ToString(), true.ToString(), UserId.ToString(), DateTime.Now.ToString(), UserId.ToString(), DateTime.Now.ToString());
        }

        txtExpCharges.Text = "0";
        ddlExpense.SelectedIndex = 0;
        ddlExpCurrency.SelectedIndex = 0;
        txtExpExchangeRate.Text = "0";
        txtFCExpAmount.Text = "0";
        fillExpGrid(InvoiceId.Trim());

    }

    protected void IbtnDeleteExp_Command(object sender, CommandEventArgs e)
    {
        string InvoiceId = string.Empty;
        if (HdnEdit.Value != "")
        {
            InvoiceId = HdnEdit.Value.Trim();
        }
        else
        {
            InvoiceId = ObjPurchaseInvoice.getAutoId();
        }
        ObjShipExpDetail.ShipExpDetail_Delete(StrCompId, StrBrandId, StrLocationId, InvoiceId, e.CommandArgument.ToString());
        fillExpGrid(InvoiceId);

    }

    #region Auto Complete Method
    [System.Web.Services.WebMethodAttribute(), System.Web.Script.Services.ScriptMethodAttribute()]
    public static string[] GetCompletionList(string prefixText, int count, string contextKey)
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
    #endregion
    #region User Defind Funcation
    public string CurrencyName(string CurrencyId)
    {
        string CurrencyName = string.Empty;
        DataTable dt = ObjCurrencyMaster.GetCurrencyMasterById(CurrencyId.ToString());
        if (dt.Rows.Count != 0)
        {
            CurrencyName = dt.Rows[0]["Currency_Name"].ToString();
        }
        else
        {
            CurrencyName = "0";
        }
        return CurrencyName;
    }
    public string GetExpName(string ExpId)
    {
        return (ObjShipExp.GetShipExpMasterById(StrCompId, ExpId)).Rows[0]["Exp_Name"].ToString();
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
        DataTable dt = objUnit.GetUnitMasterById(StrCompId.ToString(), UnitId.ToString());
        if (dt.Rows.Count != 0)
        {
            UnitName = dt.Rows[0]["Unit_Name"].ToString();
        }
        return UnitName;
    }

    public void fillGrid()
    {
        DataTable dt = ObjPurchaseInvoice.GetPurchaseInvoiceTrueAll(StrCompId.ToString(), StrBrandId.ToString(), StrLocationId.ToString());
        GvPurchaseInvocie.DataSource = dt;
        GvPurchaseInvocie.DataBind();
        lblTotalRecords.Text = Resources.Attendance.Total_Records + " : " + dt.Rows.Count;
        Session["DtPurchaseInvocie"] = dt;
        Session["dtFilter"] = dt;
        AllPageCode();

    }
    public void fillPOgridDetail(GridView gvProduct, string ReqId)
    {
        DataTable dt = new DataTable();
        if (HdnEdit.Value == "")
        {
            dt = ObjPurchaseOrderDetail.GetPurchaseOrderDetailbyPOId(StrCompId, StrBrandId, StrLocationId, ReqId.ToString());
            if (dt.Rows.Count != 0)
            {
                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    DataTable dtPuInvDetail = ObjPurchaseInvoiceDetail.GetPurchaseInvoiceDetailbyPOId_ProductId(StrCompId.ToString(), StrBrandId.ToString(), StrLocationId.ToString(), ReqId.ToString(), dt.Rows[i]["Product_Id"].ToString());
                    if (dtPuInvDetail.Rows.Count != 0)
                    {

                        string RecQty = "0";
                        for (int j = 0; j < dtPuInvDetail.Rows.Count; j++)
                        {
                            //   RecQty = (float.Parse(RecQty) + float.Parse((float.Parse(dtPuInvDetail.Rows[j]["InvoiceQty"].ToString()) - float.Parse(dtPuInvDetail.Rows[j]["RecQty"].ToString())).ToString())).ToString();
                            //Update After Got Issue by Akshay
                            RecQty = ((float.Parse(RecQty)) + float.Parse(dtPuInvDetail.Rows[j]["RecQty"].ToString())).ToString();
                        }
                      //  dt.Rows[i]["RemainQty"] = (float.Parse(dtPuInvDetail.Rows[0]["FreeQty"].ToString()) + float.Parse(dtPuInvDetail.Rows[0]["OrderQty"].ToString()) - float.Parse((float.Parse(dtPuInvDetail.Rows[0]["InvoiceQty"].ToString()) - float.Parse(RecQty.ToString())).ToString())).ToString();
                        //Update After Got Issue by Akshay
                        dt.Rows[i]["RemainQty"] = (float.Parse(dtPuInvDetail.Rows[0]["FreeQty"].ToString()) + float.Parse(dtPuInvDetail.Rows[0]["OrderQty"].ToString()) - float.Parse(RecQty.ToString())).ToString();
                    }
                    else
                    {
                        dt.Rows[i]["RemainQty"] = (float.Parse(dt.Rows[i]["FreeQty"].ToString()) + float.Parse(dt.Rows[i]["OrderQty"].ToString())).ToString();
                    }


                }

                foreach (DataRow row in dt.Select())
                {
                    if ((row["RemainQty"].ToString() == "0"))
                    {
                        row.Delete();
                    }

                }
            }
        }
        else
        {
            dt = ObjPurchaseInvoiceDetail.GetPurchaseInvoiceDetailbyInvoiceId_POId(StrCompId.ToString(), StrBrandId.ToString(), StrLocationId.ToString(), HdnEdit.Value.Trim(), ReqId.ToString());
            dt.Columns["ProductId"].ColumnName = "Product_Id";
            dt.Columns["TransId"].ColumnName = "Trans_Id";
            dt.Columns.Add("RemainQty");
            for (int i = 0; i < dt.Rows.Count; i++)
            {
                DataTable dtPuInvDetail = ObjPurchaseInvoiceDetail.GetPurchaseInvoiceDetailbyPOId_ProductId(StrCompId.ToString(), StrBrandId.ToString(), StrLocationId.ToString(), ReqId.ToString(), dt.Rows[i]["Product_Id"].ToString());
                if (dtPuInvDetail.Rows.Count != 0)
                {
                    string RecQty = "0";
                    for (int j = 0; j < dtPuInvDetail.Rows.Count; j++)
                    {
                      //  RecQty = (float.Parse(RecQty) + float.Parse((float.Parse(dtPuInvDetail.Rows[j]["InvoiceQty"].ToString()) - float.Parse(dtPuInvDetail.Rows[j]["RecQty"].ToString())).ToString())).ToString();
                        //Update After Got Issue by Akshay
                        RecQty = ((float.Parse(RecQty)) + float.Parse(dtPuInvDetail.Rows[j]["RecQty"].ToString())).ToString();
                    }
                  //  dt.Rows[i]["RemainQty"] = (float.Parse(dtPuInvDetail.Rows[0]["FreeQty"].ToString()) + float.Parse(dtPuInvDetail.Rows[0]["OrderQty"].ToString()) - float.Parse((float.Parse(dtPuInvDetail.Rows[0]["InvoiceQty"].ToString()) - float.Parse(RecQty.ToString())).ToString())).ToString();
                    //Update After Got Issue by Akshay
                    dt.Rows[i]["RemainQty"] = (float.Parse(dtPuInvDetail.Rows[0]["FreeQty"].ToString()) + float.Parse(dtPuInvDetail.Rows[0]["OrderQty"].ToString()) - float.Parse(RecQty.ToString())).ToString();
                 
                }

            }

        }
        gvProduct.DataSource = dt;
        gvProduct.DataBind();
        AllPageCode();
        if (HdnEdit.Value != "")
        {
            foreach (GridViewRow Row in gvProduct.Rows)
            {


                string Id = ((Label)Row.FindControl("lblTransId")).Text.Trim();

                ((TextBox)Row.FindControl("txtRecQty")).Text = Math.Round(Convert.ToDouble(new DataView(dt, "Trans_Id='" + Id + "'", "", DataViewRowState.CurrentRows).ToTable().Rows[0]["InvoiceQty"].ToString())).ToString();
                ((TextBox)Row.FindControl("txtTax")).Text = Math.Round(Convert.ToDouble(new DataView(dt, "Trans_Id='" + Id + "'", "", DataViewRowState.CurrentRows).ToTable().Rows[0]["TaxValue"].ToString())).ToString();
                ((TextBox)Row.FindControl("txtDiscount")).Text = Math.Round(Convert.ToDouble(new DataView(dt, "Trans_Id='" + Id + "'", "", DataViewRowState.CurrentRows).ToTable().Rows[0]["DiscountValue"].ToString())).ToString();
                if (RdoClose.Checked)
                {
                    ((TextBox)Row.FindControl("txtRecQty")).Enabled = false;
                    ((TextBox)Row.FindControl("txtTax")).Enabled = false;
                    ((TextBox)Row.FindControl("txtDiscount")).Enabled = false;
                    ((TextBox)Row.FindControl("txtAmount")).Enabled = false;

                }
            }
        }
        Total(gvProduct);


    }
    public void FillCurrency(DropDownList ddlCurrency)
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
    public void Total(GridView gvProduct)
    {
        int i = 0;
        float j = 0;

        foreach (GridViewRow Row in gvProduct.Rows)
        {
            TextBox txtQty = (TextBox)Row.FindControl("txtRecQty");

            TextBox txtTotalAmount = (TextBox)Row.FindControl("txtAmount");
            i = i + Convert.ToInt32(txtQty.Text);

            Label lblUnitRate = (Label)Row.FindControl("lblUnitRate");
            TextBox txttaxValue = (TextBox)Row.FindControl("txtTax");
            TextBox txDisValue = (TextBox)Row.FindControl("txtDiscount");
            if (txtQty.Text == "0")
            {
                txttaxValue.Text = "0";
                txDisValue.Text = "0";

            }
            float F = float.Parse(lblUnitRate.Text);
            txtTotalAmount.Text = ((Convert.ToInt32(txtQty.Text) * F) + float.Parse(txttaxValue.Text) - float.Parse(txDisValue.Text)).ToString();

            j = j + float.Parse(txtTotalAmount.Text);

        }
        if (gvProduct.Rows.Count != 0)
        {
            ((Label)gvProduct.FooterRow.FindControl("txtTotalQuantity")).Text = i.ToString();
            ((Label)gvProduct.FooterRow.FindControl("txtTotalAmount")).Text = j.ToString();
            txtNetTaxPar_TextChanged(null, null);
            txtNetDisPer_TextChanged(null, null);
            NetTotal();
        }


    }
    public void NetTotal()
    {
        try
        {

            float Total = 0;
            float Qty = 0;
            float NetVal = 0;
            float NetPer = 0;
            if (RdoPo.Checked)
            {
                try
                {
                    foreach (GridViewRow gvRow in gvPurchaseOrder.Rows)
                    {
                        GridView gvProduct = (GridView)gvRow.FindControl("gvProduct");
                        try
                        {

                            Total += float.Parse(((Label)gvProduct.FooterRow.FindControl("txtTotalAmount")).Text);
                        }
                        catch
                        {

                        }
                        try
                        {
                            Qty += float.Parse(((Label)gvProduct.FooterRow.FindControl("txtTotalQuantity")).Text);

                        }
                        catch
                        {

                        }
                        gvPurchaseOrder.ShowFooter = true;
                    }
                }
                catch
                {

                }

                ((Label)gvPurchaseOrder.FooterRow.FindControl("txtTotalQuantity")).Text = Qty.ToString();
                ((Label)gvPurchaseOrder.FooterRow.FindControl("txtTotalAmount")).Text = Total.ToString();
            }
            if (RdoWithOutPo.Checked)
            {
                try
                {

                    Total += float.Parse(((Label)gvProduct.FooterRow.FindControl("txtTotalAmount")).Text);
                }
                catch
                {

                }
                try
                {
                    Qty += float.Parse(((Label)gvProduct.FooterRow.FindControl("txtTotalQuantity")).Text);

                }
                catch
                {

                }


            }

            if (txtNetDisPer.Text == "0")
            {
                if (txtNetAmount.Text.Trim().ToString() != "0")
                {

                    NetPer = 100 * float.Parse(txtNetDisVal.Text.Trim()) / Total;
                    txtNetDisPer.Text = NetPer.ToString();
                }
            }
            else
            {
                if (txtNetDisVal.Text == "0")
                {
                    NetVal = Total * float.Parse(txtNetDisPer.Text.Trim()) / 100;
                    txtNetDisVal.Text = NetVal.ToString();
                }
            }

            txtNetAmount.Text = (Total - float.Parse(txtNetDisVal.Text.Trim())).ToString();
            NetVal = 0;
            NetPer = 0;

            if (txtNetTaxPar.Text == "0")
            {
                if (Total.ToString() != "0")
                {
                    NetPer = 100 * float.Parse(txtNetTaxVal.Text.Trim()) / float.Parse(txtNetAmount.Text.Trim());
                    txtNetTaxPar.Text = NetPer.ToString();
                }
            }
            else
            {
                if (txtNetTaxVal.Text == "0")
                {
                    if (Total.ToString() != "0")
                    {
                        NetVal = float.Parse(txtNetAmount.Text.Trim()) * float.Parse(txtNetTaxPar.Text.Trim()) / 100;
                        txtNetTaxVal.Text = NetVal.ToString();
                    }
                }

            }
            float TotExp = 0;
            try
            {
                TotExp = float.Parse(((Label)GridExpenses.FooterRow.FindControl("txttotExp")).Text);
            }
            catch
            {
                TotExp = 0;
            }
            txtGrandTotal.Text = (float.Parse(txtNetAmount.Text) + float.Parse(txtNetTaxVal.Text.Trim())).ToString();
            float Cost = 0;
            Cost = (float.Parse(txtGrandTotal.Text.Trim()) * float.Parse(txtExchangeRate.Text.Trim()) + TotExp);
            txtCostingRate.Text = (Cost / float.Parse(txtGrandTotal.Text.Trim())).ToString();
        }
        catch
        {

        }
    }
    public void DisplayMessage(string str)
    {
        ScriptManager.RegisterStartupScript(this, GetType(), "", "alert('" + str + "');", true);
    }
    public void Reset()
    {
        txtInvoicedate.Text = DateTime.Now.ToString(ObjSysParam.GetSysParameterByParamName("Date_Format").Rows[0]["Param_Value"].ToString());
        txtSupInvoiceDate.Text = DateTime.Now.ToString(ObjSysParam.GetSysParameterByParamName("Date_Format").Rows[0]["Param_Value"].ToString());
        FillCurrency(ddlCurrency);
        FillCurrency(ddlExpCurrency);
        txtInvoiceNo.Text = "";
        ddlInvoiceType.SelectedIndex = 0;
        txtSupplierInvoiceNo.Text = "";
        txtSupplierName.Text = "";
        gvPurchaseOrder.DataSource = null;
        gvPurchaseOrder.DataBind();
        txtNetAmount.Text = "";
        txtNetDisPer.Text = "";
        txtNetDisVal.Text = "";
        txtNetTaxPar.Text = "";
        txtNetTaxVal.Text = "";
        txtGrandTotal.Text = "";
        txtOtherCharges.Text = "";
        txtExchangeRate.Text = "";
        txRemark.Text = "";
        RdoClose.Checked = false;
        RdoOpen.Checked = false;
        btnAddOrderProduct.Visible = false;
        ddlPaymentMode.SelectedIndex = 0;
        HdnEdit.Value = "";
        txtCostingRate.Text = "";
        txtSupplierName.Text = "";
        ResetDetail();
        gvProduct.DataSource = null;
        gvProduct.DataBind();
        RdoPo.Checked = false;
        RdoWithOutPo.Checked = false;
        btnAddProduct.Visible = false;
        GridExpenses.DataSource = null;
        GridExpenses.DataBind();
        txtInvoiceNo.Enabled = true;
        txtInvoiceNo.Text = GetDocumentNumber();
    }



    public string GetDateFormat(string Date)
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


    public void fillgridDetail()
    {
        string ReqId = string.Empty;
        if (HdnEdit.Value == "")
        {
            ReqId = ObjPurchaseInvoice.getAutoId();
        }
        else
        {
            ReqId = HdnEdit.Value.ToString();
        }
        DataTable dt = ObjPurchaseInvoiceDetail.GetPurchaseInvoiceDetailByInvoiceNo(StrCompId, StrBrandId, StrLocationId, ReqId.ToString());
        gvProduct.DataSource = dt;
        gvProduct.DataBind();

        try
        {
            double d = 0;
            double q = 0;
            foreach (GridViewRow Row in gvProduct.Rows)
            {
                q += Convert.ToDouble(((Label)Row.FindControl("QtyReceived")).Text.Trim());
                d += Convert.ToDouble(((Label)Row.FindControl("lblAmount")).Text.Trim());
            }
            ((Label)gvProduct.FooterRow.FindControl("txtTotalAmount")).Text = d.ToString();
            ((Label)gvProduct.FooterRow.FindControl("txtTotalQuantity")).Text = q.ToString();

        }
        catch
        {

        }
        AllPageCode();
    }

    public void FillUnit()
    {
        try
        {
            DataTable dt = objUnit.GetUnitMasterAll(StrCompId.ToString());
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


    public void ProductTotal()
    {
        double d = Convert.ToDouble(txtReceivedQty.Text.Trim()) - Convert.ToDouble(txtfreeQty.Text.Trim());
        txtAmount.Text = (d * Convert.ToDouble(txtUnitCost.Text)).ToString();
        txtPDisValue.Text = (Convert.ToDouble(txtAmount.Text.Trim()) * Convert.ToDouble(txtPDisPer.Text.Trim()) / 100).ToString();

        txtPAfterTaxAmount.Text = (Convert.ToDouble(txtAmount.Text.Trim()) - Convert.ToDouble(txtPDisValue.Text.Trim())).ToString();
        txtPTaxVal.Text = (Convert.ToDouble(txtPAfterTaxAmount.Text.Trim()) * Convert.ToDouble(txtPTaxPer.Text.Trim()) / 100).ToString();

        txtTotalAmount.Text = (Convert.ToDouble(txtPAfterTaxAmount.Text.Trim()) - Convert.ToDouble(txtPDisValue.Text.Trim())).ToString();

    }


    public void ResetDetail()
    {
        txtProductName.Text = "";
        txtPDescription.Text = "";
        ddlUnit.SelectedIndex = 0;
        txtfreeQty.Text = "0";
        hidProduct.Value = "";
        txtUnitCost.Text = "0";
        txtReceivedQty.Text = "0";
        txtPDisPer.Text = "0";
        txtPDisValue.Text = "0";
        txtPTaxPer.Text = "0";
        txtPTaxVal.Text = "0";
        txtPAfterTaxAmount.Text = "0";
        txtAmount.Text = "0";
        txtTotalAmount.Text = "0";

    }

    public void fillExpGrid(string InvoiceId)
    {
        DataTable dt = ObjShipExpDetail.Get_ShipExpDetailByInvoiceId(StrCompId, StrBrandId, StrLocationId, InvoiceId.Trim());
        GridExpenses.DataSource = dt;
        GridExpenses.DataBind();
        if (dt.Rows.Count != 0)
        {
            float f = 0;
            foreach (GridViewRow Row in GridExpenses.Rows)
            {
                f += float.Parse(((Label)Row.FindControl("lblgvExp_Charges")).Text.Trim());
            }
            ((Label)GridExpenses.FooterRow.FindControl("txttotExp")).Text = f.ToString();
        }
        NetTotal();
        AllPageCode();
    }

    public void fillExpenses()
    {
        DataTable dt = ObjShipExp.GetShipExpMaster(StrCompId.ToString());
        ddlExpense.DataSource = dt;
        ddlExpense.DataTextField = "Exp_Name";
        ddlExpense.DataValueField = "Expense_Id";
        ddlExpense.DataBind();
        ddlExpense.Items.Insert(0, "--Select--");
        ddlExpense.SelectedIndex = 0;
    }
    #endregion
    #region AllPageCode
    public void AllPageCode()
    {
        Page.Title = ObjSysParam.GetSysTitle();
        UserId = Session["UserId"].ToString();
        StrCompId = Session["CompId"].ToString();
        StrBrandId = Session["BrandId"].ToString();
        StrLocationId = Session["LocId"].ToString();
        Session["AccordianId"] = "12";
        Session["HeaderText"] = "Purchase";
        DataTable dtAllPageCode = cmn.GetAllPagePermission(UserId.ToString(), "12", "48");
        if (dtAllPageCode.Rows.Count != 0)
        {
            if (dtAllPageCode.Rows[0][0].ToString() == "SuperAdmin")
            {
                btnSave.Visible = true;
                btnProductSave.Visible = true;
                IbtnAddExpenses.Visible = true;

                foreach (GridViewRow Row in GvPurchaseInvocie.Rows)
                {
                    ((ImageButton)Row.FindControl("btnEdit")).Visible = true;
                    ((ImageButton)Row.FindControl("IbtnDelete")).Visible = true;
                }
                foreach (GridViewRow Row in gvProduct.Rows)
                {
                    ((ImageButton)Row.FindControl("btnPDEdit")).Visible = true;
                    ((ImageButton)Row.FindControl("IbtnPDDelete")).Visible = true;
                }

                foreach (GridViewRow Row in GridExpenses.Rows)
                {

                    ((ImageButton)Row.FindControl("IbtnDeleteExp")).Visible = true;
                }

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
                    foreach (GridViewRow Row in GvPurchaseInvocie.Rows)
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
                    foreach (GridViewRow Row in GridExpenses.Rows)
                    {
                        if (Convert.ToBoolean(DtRow["Op_Delete"].ToString()))
                        {
                            ((ImageButton)Row.FindControl("IbtnDeleteExp")).Visible = true;
                        }
                    }

                    if (Convert.ToBoolean(DtRow["Op_Restore"].ToString()))
                    {

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

        DataTable Dt = objDocNo.GetDocumentNumberAll(StrCompId, "12", "48");

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
                DocumentNo += "-" + (Convert.ToInt32(ObjPurchaseInvoice.GetPurchaseInvoiceHeader(StrCompId.ToString()).Rows.Count) + 1).ToString();
            }
            else
            {
                DocumentNo += (Convert.ToInt32(ObjPurchaseInvoice.GetPurchaseInvoiceHeader(StrCompId.ToString()).Rows.Count) + 1).ToString();

            }
        }
        else
        {
            DocumentNo += (Convert.ToInt32(ObjPurchaseInvoice.GetPurchaseInvoiceHeader(StrCompId.ToString()).Rows.Count) + 1).ToString();
        }

        return DocumentNo;

    }



    //  Add Product based on Purchase Order  on Edit Mode if Open
    #region

    public void FillgridOfaddOrderProduct()
    {
        int i = 0;
        if (txtSupplierName.Text != "")
        {
            DataTable dtPurchaseOrder = new DataView(ObjPurchaseOrder.GetPurchaseOrderTrueAll(StrCompId.ToString(), StrBrandId.ToString(), StrLocationId.ToString()), "SupplierId='" + txtSupplierName.Text.Trim().Split('/')[1].ToString() + "'", "", DataViewRowState.CurrentRows).ToTable();

            if (dtPurchaseOrder.Rows.Count != 0)
            {
                gvaddOrderProduct.DataSource = dtPurchaseOrder;
                gvaddOrderProduct.DataBind();
                AllPageCode();

                if (HdnEdit.Value != "")
                {
                    foreach (GridViewRow GridRow in gvaddOrderProduct.Rows)
                    {

                        FillgridAddOrderProductDetail((GridView)GridRow.FindControl("gvProduct"), ((Label)GridRow.FindControl("lblPoId")).Text.Trim());
                        if (((GridView)GridRow.FindControl("gvProduct")).Rows.Count != 0)
                        {
                            ((GridView)GridRow.FindControl("gvProduct")).DataSource = null;
                            ((GridView)GridRow.FindControl("gvProduct")).DataBind();
                        }
                        else
                        {
                            GridRow.Visible = false;
                            i++;
                        }
                    }

                }
            }
            else
            {
                gvaddOrderProduct.DataSource = null;
                gvaddOrderProduct.DataBind();
            }

        }
        ViewState["CheckProductCount"] = i;

    }

    public void FillgridAddOrderProductDetail(GridView gvProduct, string ReqId)
    {
        DataTable dt = new DataTable();
        dt = ObjPurchaseOrderDetail.GetPurchaseOrderDetailbyPOId(StrCompId, StrBrandId, StrLocationId, ReqId.ToString());
        if (dt.Rows.Count != 0)
        {
            foreach (DataRow Row in dt.Rows)
            {
                DataTable dtPuInvDetail = ObjPurchaseInvoiceDetail.GetPurchaseInvoiceDetailbyPOId_ProductId(StrCompId.ToString(), StrBrandId.ToString(), StrLocationId.ToString(), ReqId.ToString(), Row["Product_Id"].ToString());

                if (dtPuInvDetail.Rows.Count != 0)
                {

                    string RecQty = "0";
                    for (int j = 0; j < dtPuInvDetail.Rows.Count; j++)
                    {
                        RecQty = (float.Parse(RecQty) + float.Parse((float.Parse(dtPuInvDetail.Rows[j]["InvoiceQty"].ToString()) - float.Parse(dtPuInvDetail.Rows[j]["RecQty"].ToString())).ToString())).ToString();

                    }
                    Row["RemainQty"] = (float.Parse(dtPuInvDetail.Rows[0]["FreeQty"].ToString()) + float.Parse(dtPuInvDetail.Rows[0]["OrderQty"].ToString()) - float.Parse((float.Parse(dtPuInvDetail.Rows[0]["InvoiceQty"].ToString()) - float.Parse(RecQty.ToString())).ToString())).ToString();

                }
                else
                {
                    Row["RemainQty"] = (float.Parse(Row["FreeQty"].ToString()) + float.Parse(Row["OrderQty"].ToString())).ToString();
                }
                if (dtPuInvDetail.Rows.Count != 0)
                {

                    Row.Delete();
                }

            }


        }

        gvProduct.DataSource = dt;
        gvProduct.DataBind();
        AllPageCode();
    }

    protected void btnAddOrderProduct_Click(object sender, EventArgs e)
    {
        FillgridOfaddOrderProduct();
        bool b = false;
        if (Convert.ToInt32(ViewState["CheckProductCount"].ToString()) == gvaddOrderProduct.Rows.Count)
        {
            DisplayMessage("Product Not Found");
        }
        else
        {

            pnlAddOrderProduct.Visible = true;
            pnlAddOrderProduct1.Visible = true;
        }
    }

    protected void ChkgvaddOrderProductPoId_CheckedChanged(object sender, EventArgs e)
    {
        CheckBox chk = ((CheckBox)sender);
        GridViewRow Row = (GridViewRow)((CheckBox)sender).Parent.Parent;
        if (chk.Checked == true)
        {
            FillgridAddOrderProductDetail((GridView)Row.FindControl("gvProduct"), ((Label)Row.FindControl("lblPoId")).Text.Trim());
        }
        else
        {

            ((GridView)Row.FindControl("gvProduct")).DataSource = null;
            ((GridView)Row.FindControl("gvProduct")).DataBind();
        }


    }

    protected void btnAddToInvoice_Click(object sender, EventArgs e)
    {
        string PoId = string.Empty;
        foreach (GridViewRow gridPORow in gvaddOrderProduct.Rows)
        {
            if (((CheckBox)gridPORow.FindControl("ChkgvaddOrderProductPoId")).Checked)
            {
                PoId = ((Label)gridPORow.FindControl("lblPoId")).Text.Trim().ToString();
                foreach (GridViewRow GridRow in ((GridView)gridPORow.FindControl("gvProduct")).Rows)
                {
                    Label LblTransId = (Label)GridRow.FindControl("lblTransId");
                    Label LblProductId = (Label)GridRow.FindControl("lblProductId");
                    Label LblUnitId = (Label)GridRow.FindControl("lblUnitId");
                    Label lblUnitCost = (Label)GridRow.FindControl("lblUnitRate");
                    Label lblOrderQty = (Label)GridRow.FindControl("lblReqQty");
                    Label lblFreeQty = (Label)GridRow.FindControl("lblFreeQty");
                    TextBox txtRecQty = (TextBox)GridRow.FindControl("txtRecQty");
                    TextBox txtTaxvalue = (TextBox)GridRow.FindControl("txtTax");
                    TextBox txtDisvalue = (TextBox)GridRow.FindControl("txtDiscount");

                    if (txtRecQty.Text != "0")
                    {

                        ObjPurchaseInvoiceDetail.InsertPurchaseInvoiceDetail(StrCompId.ToString(), StrBrandId.ToString(), StrLocationId.ToString(), HdnEdit.Value.ToString(), GridRow.RowIndex + 1.ToString(), LblProductId.Text.Trim(), PoId.ToString(), LblUnitId.Text, lblUnitCost.Text, lblOrderQty.Text, lblFreeQty.Text.Trim(), txtRecQty.Text, "0", txtDisvalue.Text.Trim(), txtTaxvalue.Text.Trim(), "", "", "", "", "", true.ToString(), DateTime.Now.ToString(), true.ToString(), UserId.ToString(), DateTime.Now.ToString(), UserId.ToString(), DateTime.Now.ToString());
                    }

                }
            }

        }
        pnlAddOrderProduct.Visible = false;
        pnlAddOrderProduct1.Visible = false;
        gvaddOrderProduct.DataSource = null;
        gvaddOrderProduct.DataBind();
        RdoPo_CheckedChanged(null, null);
    }

    #endregion




}





