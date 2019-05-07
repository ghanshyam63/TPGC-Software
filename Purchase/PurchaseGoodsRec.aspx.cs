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

public partial class Purchase_PurchaseGoodsRec : BasePage
{
    #region Defind Class object
    PurchaseInvoice ObjPurchaseInvoice = new PurchaseInvoice();
    SystemParameter ObjSysParam = new SystemParameter();
    PurchaseInvoiceDetail ObjPurchaseInvoiceDetail = new PurchaseInvoiceDetail();
    Inv_UnitMaster objUnit = new Inv_UnitMaster();
    Inv_ProductMaster ObjProductMaster = new Inv_ProductMaster();
    Ems_ContactMaster objContact = new Ems_ContactMaster();
    Common cmn = new Common();
    Inv_StockBatchMaster ObjStockBatchMaster = new Inv_StockBatchMaster();
    Inv_ProductLedger ObjProductLadger = new Inv_ProductLedger();
    string StrCompId = string.Empty;
    string StrBrandId = string.Empty;
    string StrLocationId = string.Empty;
    string UserId = string.Empty;
    #endregion
    protected void Page_Load(object sender, EventArgs e)
    {
        StrCompId = Session["CompId"].ToString();
        StrBrandId = Session["BrandId"].ToString();
        StrLocationId = Session["LocId"].ToString();
        if (!IsPostBack)
        {
            fillGrid();
            PnlProduct.Visible = false;
        }

        AllPageCode();
    }
    #region System Defind Funcation

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
        fillGrid();
        txtValue.Text = "";
        ddlFieldName.SelectedIndex = 0;
        ddlOption.SelectedIndex = 3;

    }


    protected void btnEdit_Command(object sender, CommandEventArgs e)
    {
        DataTable dt = ObjPurchaseInvoice.GetPurchaseInvoiceTrueAllByTransId(StrCompId.ToString(), StrBrandId.ToString(), StrLocationId.ToString(), e.CommandArgument.ToString());
        if (dt.Rows.Count != 0)
        {
            gvInvoice.DataSource = dt;
            gvInvoice.DataBind();
            FillGridDetail(e.CommandArgument.ToString());
            PnlList.Visible = false;
            PnlProduct.Visible = true;
            AllPageCode();
            ViewState["hdnInvoiceId"] = e.CommandArgument.ToString();
        }
    }
    protected void GvPurchaseInvocie_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        gvInvoice.PageIndex = e.NewPageIndex;

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

    protected void btnSave_Click(object sender, EventArgs e)
    {
        int counter = 0;
        string TransId = string.Empty;
        string Qty = string.Empty;
        string InvoiceId = ViewState["hdnInvoiceId"].ToString();
        foreach (GridViewRow Rows in GvProduct.Rows)
        {
            TransId = ((Label)Rows.FindControl("lblTransID")).Text.ToString();
            string ProductId = ((HiddenField)Rows.FindControl("hdnProductId")).Value.ToString();
            string UnitId = ((HiddenField)Rows.FindControl("hdnUnitId")).Value.ToString();
            Qty = (float.Parse(((TextBox)Rows.FindControl("txtRecQty")).Text.Trim().ToString()) + float.Parse(((Label)Rows.FindControl("QtyField1")).Text.ToString())).ToString();



            if (Qty != "0")
            {
                ObjPurchaseInvoiceDetail.UpdatePurchaseInvoiceDetailByGoods(StrCompId.ToString(), StrBrandId.ToString(), StrLocationId.ToString(), TransId.ToString(), Qty);
                //Update By Akshay 
                Qty = ((TextBox)Rows.FindControl("txtRecQty")).Text.Trim();
                ObjProductLadger.InsertProductLedger(StrCompId.ToString(), StrBrandId.ToString(), StrLocationId.ToString(), "PG", InvoiceId, "0", ProductId, UnitId, "I", "0", "0", Qty, "0", "1/1/1800", "0", "1/1/1800", "0", "1/1/1800", "0", "", "", "", "", "", true.ToString(), "1/1/1800", true.ToString(), UserId.ToString(), "1/1/1800", UserId.ToString(), "1/1/1800");
                if (Session["dtSerial"] != null)
                {
                    DataTable dt = (DataTable)Session["dtSerial"];
                    dt = new DataView(dt, "ProductId='" + ProductId + "'", "", DataViewRowState.CurrentRows).ToTable();
                    foreach (DataRow dr in dt.Rows)
                    {
                        ObjStockBatchMaster.InsertStockBatchMaster(StrCompId.ToString(), StrBrandId.ToString(), StrLocationId.ToString(), "PG", InvoiceId, ProductId, UnitId, "I", "0", "0", "1", "1/1/1800", dr["SerialNo"].ToString(), "1/1/1800", "0", "", "", "", "", "", true.ToString(), "1/1/1800", true.ToString(), UserId.ToString(), "1/1/1800", UserId.ToString(), "1/1/1800");
                    }
                }
            }
        }
        DisplayMessage("Record Saved");
        fillGrid();
        PnlProduct.Visible = false;
        PnlList.Visible = true;
        ViewState["hdnInvoiceId"] = null;
        Session["dtSerial"] = null;

    }
    protected void txtRecQty_TextChanged(object sender, EventArgs e)
    {
        GridViewRow Row = (GridViewRow)((TextBox)sender).Parent.Parent;
        TextBox txtqty = (TextBox)sender;
        if (txtqty.Text == "")
        {
            txtqty.Text = "0";

        }
        else
        {
            if (txtqty.Text != "0")
            {
                float Qty = float.Parse(((Label)Row.FindControl("QtyReceived")).Text.ToString()) - float.Parse(((Label)Row.FindControl("QtyField1")).Text.ToString());
                if (Qty < float.Parse(txtqty.Text.Trim()))
                {
                    txtqty.Text = "0";
                    return;

                }
            }
            try
            {
                ((TextBox)(GvProduct.Rows[Row.RowIndex + 1].FindControl("txtRecQty"))).Focus();
            }
            catch
            {
                txtqty.Focus();
            }
            pnlProduct1.Visible = true;
            pnlProduct2.Visible = true;
            DataTable dt = new DataTable();
            dt.Columns.Add("SerialNo");
            for (int i = 0; i < Convert.ToInt32(txtqty.Text); i++)
            {

                dt.Rows.Add(i);
                dt.Rows[i][0] = "";
            }

            gvSerial.DataSource = dt;
            gvSerial.DataBind();
            try
            {
                gvSerial.Rows[0].Cells[1].Focus();
            }
            catch
            {

            }
            ViewState["ProductId"] = ((HiddenField)Row.FindControl("hdnProductId")).Value.ToString();
        }
    }

    protected void btnCancel_Click(object sender, EventArgs e)
    {
        PnlList.Visible = true;
        PnlProduct.Visible = false;
    }
    protected void btnClosePanel_Click(object sender, EventArgs e)
    {
        pnlProduct1.Visible = false;
        pnlProduct2.Visible = false;
    }
    protected void BtnSerialSave_Click(object sender, EventArgs e)
    {
        if (Session["dtSerial"] == null)
        {
            DataTable dt = new DataTable();
            dt.Columns.Add("ProductId");
            dt.Columns.Add("SerialNo");

            for (int i = 0; i < gvSerial.Rows.Count; i++)
            {
                dt.Rows.Add();
                try
                {
                    dt.Rows[i][0] = ViewState["ProductId"].ToString();
                }
                catch
                {
                    dt.Rows[i][0] = "0";
                }
                dt.Rows[i][1] = ((TextBox)gvSerial.Rows[i].FindControl("txtSerialNo")).Text.Trim();
            }
            Session["dtSerial"] = dt;
        }
        else
        {
            DataTable dt = (DataTable)Session["dtSerial"];
            dt = new DataView(dt, "ProductId<>'" + ViewState["ProductId"].ToString() + "'", "", DataViewRowState.CurrentRows).ToTable();
            int j = 0;

            for (int i = 0; i < gvSerial.Rows.Count; i++)
            {
                if (dt.Rows.Count != 0)
                {
                    j = dt.Rows.Count;
                }
                else
                {
                    j = 0;
                }

                dt.Rows.Add();
                try
                {
                    dt.Rows[j][0] = ViewState["ProductId"].ToString();
                }
                catch
                {
                    dt.Rows[j][0] = "0";
                }
                dt.Rows[j][1] = ((TextBox)gvSerial.Rows[i].FindControl("txtSerialNo")).Text.Trim();

            }
            Session["dtSerial"] = dt;
        }

        pnlProduct1.Visible = false;
        pnlProduct2.Visible = false;

    }
    protected void txtSerialNo_TextChanged(object sender, EventArgs e)
    {
        TextBox txt1 = ((TextBox)sender);
      
    
        bool b = false;
        foreach (GridViewRow Row in gvSerial.Rows)
        {
            if (Row.RowIndex != ((GridViewRow)((TextBox)sender).Parent.Parent).RowIndex)
            {
                if (((TextBox)Row.FindControl("txtSerialNo")).Text.ToString() == txt1.Text.ToString())
                {
                    b = true;

                }
            }

        }
        if (b)
        {
            DisplayMessage("Serial No Already Exists");
            txt1.Text = "";
            txt1.Focus();

        }
        else
        {

            pnlSerial.Focus();
            foreach (GridViewRow Row in gvSerial.Rows)
            {
                if (((TextBox)Row.FindControl("txtSerialNo")).Text.ToString() == "")
                {

                    ((TextBox)Row.Cells[1].FindControl("txtSerialNo")).Focus();
                    break;
                }
            }


        }
    }


    #endregion
    #region User Defind Funcation
    public void DisplayMessage(string str)
    {
        ScriptManager.RegisterStartupScript(this, GetType(), "", "alert('" + str + "');", true);
    }
    public void fillGrid()
    {
        DataTable dt = ObjPurchaseInvoice.GetPurchaseInvoiceTrueAll(StrCompId.ToString(), StrBrandId.ToString(), StrLocationId.ToString());
        int i = 0;
        foreach (DataRow dr in dt.Rows)
        {
            DataTable dtInvDetail = ObjPurchaseInvoiceDetail.GetPurchaseInvoiceDetailByInvoiceNo(StrCompId.ToString(), StrBrandId.ToString(), StrLocationId.ToString(), dr["TransID"].ToString());
            bool b = false;
            foreach (DataRow drChild in dtInvDetail.Rows)
            {
                if (drChild["RecQty"].ToString() == "")
                {
                    drChild["RecQty"] = "0";
                }
                if (float.Parse(drChild["RecQty"].ToString()) == float.Parse(drChild["InvoiceQty"].ToString()))
                {
                    b = true;
                }
                else
                {
                    b = false;
                    break;
                }
            }
            if (b)
            {
                dr.Delete();
                i++;
            }

        }
        GvPurchaseInvocie.DataSource = dt;
        GvPurchaseInvocie.DataBind();
        lblTotalRecords.Text = Resources.Attendance.Total_Records + " : " + (dt.Rows.Count - i);
        Session["DtPurchaseInvocie"] = dt;
        Session["dtFilter"] = dt;
        AllPageCode();

    }
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
    public void FillGridDetail(string InvoiceId)
    {
        DataTable dt = ObjPurchaseInvoiceDetail.GetPurchaseInvoiceDetailByInvoiceNo(StrCompId.ToString(), StrBrandId.ToString(), StrLocationId.ToString(), InvoiceId);
        if (dt.Rows.Count != 0)
        {
            GvProduct.DataSource = dt;
            GvProduct.DataBind();

        }
        AllPageCode();

    }
    public string GetTotQty(string Qty)
    {
        if (Qty == "")
        {
            Qty = "0";
        }
        return Qty;
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
        DataTable dtAllPageCode = cmn.GetAllPagePermission(UserId.ToString(), "12", "58");
        if (dtAllPageCode.Rows.Count != 0)
        {
            if (dtAllPageCode.Rows[0][0].ToString() == "SuperAdmin")
            {
                btnSave.Visible = true;

                foreach (GridViewRow Row in GvPurchaseInvocie.Rows)
                {
                    ((ImageButton)Row.FindControl("btnEdit")).Visible = true;

                }



            }
            else
            {
                foreach (DataRow DtRow in dtAllPageCode.Rows)
                {
                    if (Convert.ToBoolean(DtRow["Op_Add"].ToString()))
                    {
                        btnSave.Visible = true;

                    }
                    foreach (GridViewRow Row in GvPurchaseInvocie.Rows)
                    {
                        if (Convert.ToBoolean(DtRow["Op_Edit"].ToString()))
                        {
                            ((ImageButton)Row.FindControl("btnEdit")).Visible = true;
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

}
