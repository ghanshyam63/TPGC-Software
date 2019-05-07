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

public partial class Inventory_BillOfMaterial : BasePage
{
    #region Defind Class Object
    BillOfMaterial ObjInvBOM = new BillOfMaterial();
    Inv_ProductMaster ObjInvProductMaster = new Inv_ProductMaster();
    Inv_OptionCategoryMaster ObjOpCate = new Inv_OptionCategoryMaster();
    Common cmn = new Common();
    SystemParameter ObjSysPeram = new SystemParameter();
    string StrCompId = string.Empty;
    string StrUserId = string.Empty;


    #endregion

    protected void Page_Load(object sender, EventArgs e)
    {

        if (Session["UserId"] == null)
        {
            Response.Redirect("~/ERPLogin.aspx");
        }
        if (!IsPostBack)
        {
            StrUserId = Session["UserId"].ToString();
            StrCompId = Session["CompId"].ToString();
            Reset_Child();
            FillProductGrid();
            btnList_Click(null, null);
            txtDate.Text = DateTime.Now.ToString("dd/MM/yyyy");
            Session["IT"] = "A";
        }
        AllPageCode();
    }


    #region System Defind Funcation

    protected void btnList_Click(object sender, EventArgs e)
    {
        pnlMenuList.BackColor = System.Drawing.ColorTranslator.FromHtml("#ccddee");

        pnlMenuNew.BackColor = System.Drawing.ColorTranslator.FromHtml("#90bde9");


        pnlList.Visible = true;

        pnlNew.Visible = false;
        txtValue.Focus();
    }
    protected void btnNew_Click(object sender, EventArgs e)
    {
        pnlMenuNew.BackColor = System.Drawing.ColorTranslator.FromHtml("#ccddee");

        pnlMenuList.BackColor = System.Drawing.ColorTranslator.FromHtml("#90bde9");

        pnlNew.Visible = true;

        pnlList.Visible = false;
        txtProductId.Focus();

    }
    protected void btnSave_Click(object sender, EventArgs e)
    {

        DateTime dt = DateTime.ParseExact(txtDate.Text, "dd/MM/yyyy", null);
        string SubProductId = "0";
        string ProductId = new DataView(ObjInvProductMaster.GetProductMasterTrueAll(StrCompId.ToString()), "EProductName like '" + txtProductId.Text.Trim() + "%'", "", DataViewRowState.CurrentRows).ToTable().Rows[0]["ProductId"].ToString();
        if (txtSubProduct.Text != "")
        {
            if (txtSubProduct.Text != "0")
            {
                SubProductId = new DataView(ObjInvProductMaster.GetProductMasterTrueAll(StrCompId.ToString()), "EProductName like '" + txtSubProduct.Text.Trim() + "%'", "", DataViewRowState.CurrentRows).ToTable().Rows[0]["ProductId"].ToString();
            }
        }
        if (ProductId == "")
        {
            ProductId = "0";
        }
        if (SubProductId == "")
        {
            ProductId = "0";
        }

        if (txtOption.Text == "")
        {
            DisplayMessage("Enter Option");

            txtOption.Text = "";
            txtOption.Focus();
            return;
        }
        if (txtOptCatId.Text == "")
        {
            DisplayMessage("Enter Option Category");

            txtOptCatId.Text = "";
            txtOptCatId.Focus();
            return;
        }
        if (txtShortDesc.Text == "")
        {
            DisplayMessage("Enter Option Short Description");

            txtShortDesc.Text = "";
            txtShortDesc.Focus();
            return;
        }
        if (txtUnitPrice.Text == "")
        {
            DisplayMessage("Enter Unit Price");

            txtUnitPrice.Text = "";
            txtUnitPrice.Focus();
            return;
        }
        if (txtQty.Text == "")
        {
            DisplayMessage("Enter Quantity");

            txtQty.Text = "";
            txtQty.Focus();
            return;
        }

        string OptionCateId = string.Empty;
        if (txtOptCatId.Text != "")
        {
            OptionCateId = new DataView(ObjOpCate.GetOptionCategoryTrueAll(StrCompId.ToString()), "EName='" + txtOptCatId.Text.Trim() + "'", "", DataViewRowState.CurrentRows).ToTable().Rows[0]["OptionCategoryId"].ToString();


        }
        if (hdnDetailEdit.Value == "")
        {
            DataTable dtPS = new DataView(ObjInvBOM.BOM_All(StrCompId.ToString()), "OptionId='" + txtOption.Text.Trim() + "' and OptionCategoryId='" + OptionCateId.Trim() + "'", "", DataViewRowState.CurrentRows).ToTable();
            if (dtPS.Rows.Count != 0)
            {
                DisplayMessage("Option Id & Option Category Already Exists");
                txtOptCatId.Text = "";
                txtOption.Text = "";
                txtOption.Focus();

                return;

            }


            ObjInvBOM.Insert_BOM(StrCompId.ToString(), "1", ProductId.Trim(), dt.ToString().Trim(), ddlTransType.SelectedValue.ToString().Trim(), SubProductId.ToString().Trim(), txtModelNo.Text.ToString().Trim(), txtOption.Text.ToString().Trim(), txtOptionDesc.Text.ToString().Trim(), txtShortDesc.Text.ToString().Trim(), OptionCateId.ToString().Trim(), txtUnitPrice.Text.ToString().Trim(), txtQty.Text.ToString().Trim(), "", "", "", "", "", "", true.ToString(), DateTime.Now.ToString(), true.ToString(), StrUserId.ToString(), DateTime.Now.ToString(), StrUserId.ToString(), DateTime.Now.ToString());
        }
        else
        {
            DataTable dtPSID = new DataView(ObjInvBOM.BOM_ById(StrCompId.ToString(), hdnDetailEdit.Value.ToString()), "OptionId='" + txtOption.Text.Trim() + "' and OptionCategoryId='" + OptionCateId.Trim() + "'", "", DataViewRowState.CurrentRows).ToTable();
            if (dtPSID.Rows.Count == 0)
            {
                DataTable dtPS = new DataView(ObjInvBOM.BOM_All(StrCompId), "OptionId='" + txtOption.Text.Trim() + "' and OptionCategoryId='" + OptionCateId.Trim() + "'", "", DataViewRowState.CurrentRows).ToTable();
                if (dtPS.Rows.Count != 0)
                {
                    DisplayMessage("Option Id & Option Category Already Exists");
                    txtOptCatId.Text = "";
                    txtOption.Text = "";
                    txtOption.Focus();

                    return;

                }
            }
            ObjInvBOM.Update_BOM(hdnDetailEdit.Value.ToString(), StrCompId.ToString().Trim(), "1", ProductId.ToString().Trim(), dt.ToString().Trim(), ddlTransType.SelectedValue.ToString().Trim(), SubProductId.ToString().Trim(), txtModelNo.Text.ToString().Trim(), txtOption.Text.ToString().Trim(), txtOptionDesc.Text.ToString().Trim(), txtShortDesc.Text.ToString().Trim(), OptionCateId.ToString().Trim(), txtUnitPrice.Text.ToString().Trim(), txtQty.Text.ToString().Trim(), "", "", "", "", "", "", true.ToString(), DateTime.Now.ToString(), true.ToString(), StrUserId.ToString(), DateTime.Now.ToString());
        }
        ddlTransType.Enabled = false;
        txtDate.Enabled = false;
        txtModelNo.Enabled = false;
        txtProductId.Enabled = false;
        Reset_Child();
        fillGrid();
        pnlChlidGrid.Visible = true;
        rdoOption.Focus();
    }
    protected void btnFinalSave_Click(object sender, EventArgs e)
    {
        ddlTransType.Enabled = true;
        txtDate.Enabled = true;
        txtModelNo.Enabled = true;
        txtProductId.Enabled = true;
        Reset_Child();
        btnList_Click(null, null);
        txtProductId.Text = "";
        ddlTransType.SelectedValue = "A";
        txtDate.Text = DateTime.Now.ToString("dd/MM/yyyy");
        Session["IT"] = "A";
        txtModelNo.Text = "";
        gvProductSpecsChild.DataSource = null;
        gvProductSpecsChild.DataBind();
        pnlChlidGrid.Visible = false;
        txtValue.Focus();
    }
    protected void rdoStockOption_CheckedChanged(object sender, EventArgs e)
    {
        if (txtProductId.Text != "")
        {
            if (rdoOption.Checked)
            {
                trStock.Visible = false;
                txtSubProduct.Text = "";
                txtSubProduct.Focus();
            }
            else
            {
                if (rdoStock.Checked)
                {
                    trStock.Visible = true;

                }

            }
            pnlChlidGrid.Visible = false;
            pnlOptStock.Visible = true;

        }
        else
        {
            DisplayMessage("Enter Product Name");
            txtProductId.Focus();
            rdoOption.Checked = false;
            rdoStock.Checked = false;
        }

    }
    protected void btnDetailEdit_Command(object sender, CommandEventArgs e)
    {
        hdnDetailEdit.Value = e.CommandArgument.ToString();

        DataTable dt = ObjInvBOM.BOM_ById(StrCompId.ToString(), hdnDetailEdit.Value);
        if (dt.Rows.Count != 0)
        {
            if (dt.Rows[0]["SubProductId"].ToString() == "0")
            {
                rdoStock.Checked = false;
                rdoOption.Checked = true;
            }
            else
            {
                rdoOption.Checked = false;
                rdoStock.Checked = true;

            }
            rdoStockOption_CheckedChanged(null, null);
            txtSubProduct.Text =getProductName(dt.Rows[0]["SubProductId"].ToString());
            txtOption.Text = dt.Rows[0]["OptionId"].ToString();
            txtOptCatId.Text = GetOpCateName(dt.Rows[0]["OptionCategoryId"].ToString()).ToString();
            txtOptionDesc.Text = dt.Rows[0]["OptionDescription"].ToString();
            txtShortDesc.Text = dt.Rows[0]["ShortDescription"].ToString();
            txtUnitPrice.Text = dt.Rows[0]["UnitPrice"].ToString();
            txtQty.Text = dt.Rows[0]["Quantity"].ToString();
            rdoOption.Focus();
        }

    }
    protected void btnReset_Click(object sender, EventArgs e)
    {
        Reset_Child();
        pnlChlidGrid.Visible = true;
        ddlTransType.Focus();
    }

    protected void ddlTransType_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (ddlTransType.SelectedValue == "A")
        {
            Session["IT"] = ddlTransType.SelectedValue;
        }
        else
        {
            if (ddlTransType.SelectedValue == "K")
            {
                Session["IT"] = ddlTransType.SelectedValue;
            }


        }
        ddlTransType.Focus();
    }
    protected void IbtnDelete_Command(object sender, CommandEventArgs e)
    {
        ObjInvBOM.DeleteOrRestore_BOM(e.CommandArgument.ToString(), StrCompId.ToString(), false.ToString(), StrUserId.ToString(), DateTime.Now.ToString());
        fillGrid();
        try
        {
            ((ImageButton)((GridViewRow)((ImageButton)sender).Parent.Parent).FindControl("IbtnDelete")).Focus();
        }
        catch
        {
            rdoOption.Focus();
        }
    }
    protected void gvProductSpecsChild_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        gvProductSpecsChild.PageIndex = e.NewPageIndex;
        fillGrid();
        gvProductSpecsChild.BottomPagerRow.Focus();
    }


    protected void GridProduct_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        GridProduct.PageIndex = e.NewPageIndex;
        GridProduct.DataSource = (DataTable)Session["DtFilter"];
        GridProduct.DataBind();
        AllPageCode();
        GridProduct.BottomPagerRow.Focus();
    }
    protected void txtProductId_TextChanged(object sender, EventArgs e)
    {
        if (txtProductId.Text != "")
        {

            DataTable dt = new DataView(ObjInvProductMaster.GetProductMasterTrueAll(StrCompId.ToString()), "EProductName='" + txtProductId.Text.ToString().Trim() + "'", "", DataViewRowState.CurrentRows).ToTable();
            if (dt.Rows.Count != 0)
            {
                txtModelNo.Text = dt.Rows[0]["ModelNo"].ToString();
                fillGrid();
                pnlChlidGrid.Visible = true;
                rdoOption.Focus();
            }
            else
            {
                txtProductId.Text = "";
                DisplayMessage("Select Product Name");
                txtProductId.Focus();

            }
        }


    }
    protected void btnEdit_Command(object sender, CommandEventArgs e)
    {

        GridViewRow Row = (GridViewRow)((ImageButton)sender).Parent.Parent; ;

        txtProductId.Text = ((Label)Row.FindControl("lblProductName")).Text.ToString();
        txtModelNo.Text = ((Label)Row.FindControl("lblModelNo")).Text.ToString();
        if (((Label)Row.FindControl("lblItemType")).Text.ToString() == "Assemble")
        {
            ddlTransType.SelectedValue = "A";
        }
        else
        {
            if (((Label)Row.FindControl("lblItemType")).Text.ToString() == "KIT")
            {
                ddlTransType.SelectedValue = "K";
            }

        }
        txtProductId_TextChanged(null, null);
        btnNew_Click(null, null);
        ddlTransType.Enabled = false;
        txtDate.Enabled = false;
        txtModelNo.Enabled = false;
        txtProductId.Enabled = false;

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
                condition = "convert(" + ddlFieldName.SelectedValue + ",System.String) Like '" + txtValue.Text.Trim() + "%'";
            }
            else
            {
                condition = "convert(" + ddlFieldName.SelectedValue + ",System.String) like '%" + txtValue.Text.Trim() + "%'";
            }
            DataView view = new DataView((DataTable)Session["DtProduct"], condition, "", DataViewRowState.CurrentRows);

            lblTotalRecord.Text = Resources.Attendance.Total_Records + " : " + (view.ToTable()).Rows.Count.ToString() + "";

            GridProduct.DataSource = view.ToTable();
            GridProduct.DataBind();
            Session["DtFilter"] = view.ToTable();
            AllPageCode();
            btnbind.Focus();

        }

    }

    protected void btnRefresh_Click(object sender, ImageClickEventArgs e)
    {

        ddlFieldName.SelectedIndex = 1;
        ddlOption.SelectedIndex = 3;
        txtValue.Text = "";
        FillProductGrid();
        txtValue.Focus();
    }

    protected void txtOptCatId_TextChanged(object sender, EventArgs e)
    {
        if (txtOptCatId.Text != "")
        {
            DataTable dt = new DataView(ObjOpCate.GetOptionCategoryTrueAll(StrCompId.ToString()), "EName='" + txtOptCatId.Text.Trim() + "'", "", DataViewRowState.CurrentRows).ToTable();
            if (dt.Rows.Count == 0)
            {
                DisplayMessage("Select Option Category ");
                txtOptCatId.Text = "";
                txtOptCatId.Focus();


            }

            else
            {
                txtShortDesc.Focus();
            }


        }
    }

    #endregion

    #region AllPageCode
    public void AllPageCode()
    {
        Page.Title = ObjSysPeram.GetSysTitle();
        StrUserId = Session["UserId"].ToString();
        StrCompId = Session["CompId"].ToString();
        Session["AccordianId"] = "11";
        Session["HeaderText"] = "Inventory";
        DataTable dtAllPageCode = cmn.GetAllPagePermission(StrUserId.ToString(), "11", "27");
        if (dtAllPageCode.Rows.Count != 0)
        {
            if (dtAllPageCode.Rows[0][0].ToString() == "SuperAdmin")
            {
                btnSave.Visible = true;
                foreach (GridViewRow Row in GridProduct.Rows)
                {
                    ((ImageButton)Row.FindControl("btnEdit")).Visible = true;

                }
                foreach (GridViewRow Row in gvProductSpecsChild.Rows)
                {
                    ((ImageButton)Row.FindControl("btnDetailEdit")).Visible = true;
                    ((ImageButton)Row.FindControl("IbtnDelete")).Visible = true;
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
                    if (Convert.ToBoolean(DtRow["Op_Edit"].ToString()))
                    {

                        foreach (GridViewRow Row in GridProduct.Rows)
                        {
                            ((ImageButton)Row.FindControl("btnEdit")).Visible = true;

                        }
                        foreach (GridViewRow Row in gvProductSpecsChild.Rows)
                        {
                            ((ImageButton)Row.FindControl("btnDetailEdit")).Visible = true;

                        }
                    }
                    if (Convert.ToBoolean(DtRow["Op_Delete"].ToString()))
                    {
                        foreach (GridViewRow Row in gvProductSpecsChild.Rows)
                        {
                            ((ImageButton)Row.FindControl("IbtnDelete")).Visible = true;

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

    #region Auto Complete Method/Funcation
    [System.Web.Services.WebMethodAttribute(), System.Web.Script.Services.ScriptMethodAttribute()]
    public static string[] GetCompletionListProductName(string prefixText, int count, string contextKey)
    {
        Inv_ProductMaster ObjInvProductMaster = new Inv_ProductMaster();
        DataTable dt = ObjInvProductMaster.GetProductMasterTrueAll(HttpContext.Current.Session["CompId"].ToString());
        DataTable dtTemp = dt.Copy();

        dt = new DataView(dt, "EProductName like '" + prefixText.ToString() + "%' and ItemType='" + HttpContext.Current.Session["IT"].ToString() + "'", "", DataViewRowState.CurrentRows).ToTable();
        if (dt.Rows.Count == 0)
        {
            dt = new DataView(dtTemp, "ItemType='" + HttpContext.Current.Session["IT"].ToString() + "'", "", DataViewRowState.CurrentRows).ToTable();
        }

        string[] txt = new string[dt.Rows.Count];


        for (int i = 0; i < dt.Rows.Count; i++)
        {
            txt[i] = dt.Rows[i]["EProductName"].ToString();
        }


        return txt;
    }
    [System.Web.Services.WebMethodAttribute(), System.Web.Script.Services.ScriptMethodAttribute()]
    public static string[] GetCompletionListSubProductName(string prefixText, int count, string contextKey)
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

    [System.Web.Services.WebMethodAttribute(), System.Web.Script.Services.ScriptMethodAttribute()]
    public static string[] GetCompletionList(string prefixText, int count, string contextKey)
    {
        Inv_OptionCategoryMaster ObjOptCat = new Inv_OptionCategoryMaster();
        DataTable dt = ObjOptCat.GetOptionCategoryTrueAll(HttpContext.Current.Session["CompId"].ToString());
        DataTable dtOp = dt.Copy();
        dt = new DataView(dt, "EName Like'" + prefixText.ToString() + "%'", "", DataViewRowState.CurrentRows).ToTable();
        if (dt.Rows.Count == 0)
        {
            dt = null;
            dt = dtOp.Copy();
        }
        string[] text = new string[dt.Rows.Count];
        for (int i = 0; i < dt.Rows.Count; i++)
        {
            text[i] = dt.Rows[i]["EName"].ToString();

        }
        return text;

    }
    #endregion

    #region User Defind Funcation
    public string GetItemType(string IT)
    {
        string retval = string.Empty;
        if (IT == "A")
        {
            retval = "Assemble  (Search KeyWord as A)";
        }
        if (IT == "K")
        {
            retval = "KIT  (Search KeyWord as K)";
        }

        return retval;

    }

    public void Reset_Child()
    {
        rdoStock.Checked = false;
        rdoOption.Checked = false;
        pnlOptStock.Visible = false;
        txtSubProduct.Text = "";
        txtUnitPrice.Text = "";
        txtQty.Text = "";
        txtShortDesc.Text = "";
        txtOptionDesc.Text = "";
        txtOption.Text = "";
        txtOptCatId.Text = "";
        hdnDetailEdit.Value = "";
    }
    public void fillGrid()
    {
        string ProductId = new DataView(ObjInvProductMaster.GetProductMasterTrueAll(StrCompId.ToString()), "EProductName like '" + txtProductId.Text.Trim() + "%'", "", DataViewRowState.CurrentRows).ToTable().Rows[0]["ProductId"].ToString();
        DataTable dt = ObjInvBOM.BOM_ByProductId(StrCompId.ToString(), ProductId.ToString());
        gvProductSpecsChild.DataSource = dt;
        gvProductSpecsChild.DataBind();
        AllPageCode();
    }
    private void FillProductGrid()
    {
        DataTable dtproduct = new DataView(ObjInvProductMaster.GetProductMasterTrueAll(StrCompId.ToString()), "ItemType='A' or ItemType='K' ", "", DataViewRowState.CurrentRows).ToTable();
        GridProduct.DataSource = dtproduct;
        GridProduct.DataBind();
        Session["DtProduct"] = dtproduct;
        Session["DtFilter"] = dtproduct;
        lblTotalRecord.Text = Resources.Attendance.Total_Records + " : " + dtproduct.Rows.Count;
        AllPageCode();
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

    public string GetOpCateName(string OpCatId)
    {
        string OpCateName = string.Empty;
        try
        {
            OpCateName = ObjOpCate.GetOptionCategoryTruebyId(StrCompId.ToString(), OpCatId.ToString()).Rows[0]["EName"].ToString();
        }
        catch
        {

        }
        return OpCateName;
    }

    public string getProductName(string ProductId)
    {
        string ProductName = string.Empty;
        try
        {
            ProductName = new DataView(ObjInvProductMaster.GetProductMasterTrueAll(StrCompId.ToString()), "ProductId='" + ProductId.ToString() + "'", "", DataViewRowState.CurrentRows).ToTable().Rows[0]["EProductName"].ToString();

        }
        catch
        {
            ProductName = "0";
        }

        return ProductName;
    }
    #endregion
}
