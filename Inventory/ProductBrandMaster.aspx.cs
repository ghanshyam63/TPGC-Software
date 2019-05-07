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

public partial class Inventory_ProductBrandMaster : BasePage
{
    #region defind Class Object
    Common cmn = new Common();
    Inv_ProductBrandMaster objProB = new Inv_ProductBrandMaster();
    SystemParameter ObjSysPeram = new SystemParameter();
    string StrCompId = string.Empty;
    string StrBrandId = "1";
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
            StrCompId = Session["CompId"].ToString();
            StrUserId = Session["UserId"].ToString();
            ddlOption.SelectedIndex = 2;
            btnList_Click(null, null);
            FillGridBin();
            FillGrid();
        }

        AllPageCode();
    }

    #region AllPageCode
    public void AllPageCode()
    {
        Page.Title = ObjSysPeram.GetSysTitle();
        StrUserId = Session["UserId"].ToString();
        StrCompId = Session["CompId"].ToString();
        Session["AccordianId"] = "11";
        Session["HeaderText"] = "Inventory";
        DataTable dtAllPageCode = cmn.GetAllPagePermission(StrUserId.ToString(), "11", "22");
        if (dtAllPageCode.Rows.Count != 0)
        {
            if (dtAllPageCode.Rows[0][0].ToString() == "SuperAdmin")
            {
                btnCSave.Visible = true;
                foreach (GridViewRow Row in gvProductBrand.Rows)
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
                        btnCSave.Visible = true;
                    }
                    foreach (GridViewRow Row in gvProductBrand.Rows)
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


    #region System defind Funcation
    protected void btnList_Click(object sender, EventArgs e)
    {
        pnlMenuList.BackColor = System.Drawing.ColorTranslator.FromHtml("#ccddee");
        pnlMenuNew.BackColor = System.Drawing.ColorTranslator.FromHtml("#90bde9");
        pnlMenuBin.BackColor = System.Drawing.ColorTranslator.FromHtml("#90bde9");

        PnlList.Visible = true;
        PnlNewEdit.Visible = false;
        PnlBin.Visible = false;
        txtValue.Focus();
    }
    protected void btnNew_Click(object sender, EventArgs e)
    {
        pnlMenuNew.BackColor = System.Drawing.ColorTranslator.FromHtml("#ccddee");

        pnlMenuList.BackColor = System.Drawing.ColorTranslator.FromHtml("#90bde9");
        pnlMenuBin.BackColor = System.Drawing.ColorTranslator.FromHtml("#90bde9");

        PnlList.Visible = false;
        PnlNewEdit.Visible = true;
        PnlBin.Visible = false;
        txtBrandName.Focus();
    }
    protected void btnEdit_Command(object sender, CommandEventArgs e)
    {
        editid.Value = e.CommandArgument.ToString();

        DataTable dt = objProB.GetProductBrandByPBrandId(StrCompId, editid.Value);

        btnNew.Text = Resources.Attendance.Edit;

        txtBrandName.Text = dt.Rows[0]["Brand_Name"].ToString();
        txtLBrandName.Text = dt.Rows[0]["Brand_Name_L"].ToString();
        txtDescription.Text = dt.Rows[0]["Description"].ToString();

        btnNew_Click(null, null);

    }
    protected void gvProductBrand_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        gvProductBrand.PageIndex = e.NewPageIndex;
        DataTable dt = (DataTable)Session["dtFilter"];
        gvProductBrand.DataSource = dt;
        gvProductBrand.DataBind();
        AllPageCode();
        gvProductBrand.BottomPagerRow.Focus();
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
            DataTable dtBrand = (DataTable)Session["dtBrand"];
            DataView view = new DataView(dtBrand, condition, "", DataViewRowState.CurrentRows);
            gvProductBrand.DataSource = view.ToTable();
            Session["dtFilter"] = view.ToTable();
            lblTotalRecords.Text = Resources.Attendance.Total_Records + " : " + view.ToTable().Rows.Count + "";
            gvProductBrand.DataBind();
            AllPageCode();
            btnbind.Focus();

        }
    }
    protected void gvProductBrand_Sorting(object sender, GridViewSortEventArgs e)
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
        gvProductBrand.DataSource = dt;
        gvProductBrand.DataBind();
        AllPageCode();
        gvProductBrand.HeaderRow.Focus();
    }
    protected void IbtnDelete_Command(object sender, CommandEventArgs e)
    {
        editid.Value = e.CommandArgument.ToString();
        int b = 0;
        b = objProB.DeleteProductBrandMaster(StrCompId, editid.Value, "false", StrUserId, DateTime.Now.ToString());
        if (b != 0)
        {
            DisplayMessage("Record Deleted");
        }
        else
        {
            DisplayMessage("Record  Not Deleted");
        }
        FillGridBin();
        FillGrid();
        Reset();
        try
        {
            int i = ((GridViewRow)((ImageButton)sender).Parent.Parent).RowIndex;
            ((ImageButton)gvProductBrand.Rows[i].FindControl("IbtnDelete")).Focus();
        }
        catch
        {
            txtValue.Focus();
        }


    }
    protected void btnRefreshReport_Click(object sender, ImageClickEventArgs e)
    {
        FillGrid();
        FillGridBin();
        ddlFieldName.SelectedIndex = 1;
        ddlOption.SelectedIndex = 2;
        txtValue.Text = "";
        txtValue.Focus();
    }
    protected void BtnCCancel_Click(object sender, EventArgs e)
    {
        Reset();
        btnList_Click(null, null);
        FillGridBin();
        FillGrid();
        txtValue.Focus();
    }
    protected void BtnReset_Click(object sender, EventArgs e)
    {
        Reset();
        txtBrandName.Focus();
    }
    protected void btnCSave_Click(object sender, EventArgs e)
    {
        //Condition added to check brand name entered before save
        if (txtBrandName.Text == "" || txtBrandName.Text == null)
        {
            DisplayMessage("Enter Brand Name");
            System.Web.UI.ScriptManager.GetCurrent(this).SetFocus(txtBrandName);
            return;
        }
        int b = 0;
        if (editid.Value != "")
        {
            //Code to check whether the new name after edit does not exists
            DataTable dtPBrand = objProB.GetProductBrandByBrandName(StrCompId, txtBrandName.Text);
            if (dtPBrand.Rows.Count > 0)
            {
                if (dtPBrand.Rows[0]["PBrandId"].ToString() != editid.Value)
                {
                    txtBrandName.Text = "";
                    DisplayMessage("Brand Name Already Exists");
                    txtBrandName.Focus();
                    return;
                }
            }

            b = objProB.UpdateProductBrandMaster(StrCompId, StrBrandId, editid.Value, txtBrandName.Text, txtLBrandName.Text, txtDescription.Text, "", "", "", "", "", "True", DateTime.Now.ToString(), "True", Session["UserId"].ToString(), DateTime.Now.ToString());

            editid.Value = "";
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
            DataTable dtPro = objProB.GetProductBrandByBrandName(StrCompId, txtBrandName.Text);
            if (dtPro.Rows.Count > 0)
            {
                txtBrandName.Text = "";
                DisplayMessage("Product Brand Name Already Exists");
                System.Web.UI.ScriptManager.GetCurrent(this).SetFocus(txtBrandName);
                return;
            }

            b = objProB.InsertProductBrandMaster(StrCompId, StrBrandId, txtBrandName.Text, txtLBrandName.Text, txtDescription.Text, "", "", "", "", "", "True", DateTime.Now.ToString(), "True", Session["UserId"].ToString(), DateTime.Now.ToString(), Session["UserId"].ToString(), DateTime.Now.ToString());
            if (b != 0)
            {
                DisplayMessage("Record Saved");

                Reset();
                FillGrid();
                txtBrandName.Focus();
            }
            else
            {
                DisplayMessage("Record  Not Saved");
            }
        }
    }

    protected void txtBrandName_TextChanged(object sender, EventArgs e)
    {
        if (editid.Value == "")
        {
            DataTable dt = objProB.GetProductBrandByBrandName(StrCompId, txtBrandName.Text);
            if (dt.Rows.Count > 0)
            {
                txtBrandName.Text = "";
                DisplayMessage("Brand Name Already Exists");
                txtBrandName.Focus();
                return;
            }
            DataTable dt1 = objProB.GetProductBrandFalseAllData(StrCompId);
            dt1 = new DataView(dt1, "Brand_Name='" + txtBrandName.Text + "'", "", DataViewRowState.CurrentRows).ToTable();
            if (dt1.Rows.Count > 0)
            {
                txtBrandName.Text = "";
                DisplayMessage("Brand Name Already Exists - Go to Bin Tab");
                txtBrandName.Focus();
                return;
            }
        }
        else
        {
            DataTable dtTemp = objProB.GetProductBrandByPBrandId(StrCompId, editid.Value);
            if (dtTemp.Rows.Count > 0)
            {
                if (dtTemp.Rows[0]["Brand_Name"].ToString() != txtBrandName.Text)
                {
                    DataTable dt = objProB.GetProductBrandByBrandName(StrCompId, txtBrandName.Text);
                    if (dt.Rows.Count > 0)
                    {
                        txtBrandName.Text = "";
                        DisplayMessage("Brand Name Already Exists");
                        txtBrandName.Focus();
                        return;
                    }
                    DataTable dt1 = objProB.GetProductBrandFalseAllData(StrCompId);
                    dt1 = new DataView(dt1, "Brand_Name='" + txtBrandName.Text + "'", "", DataViewRowState.CurrentRows).ToTable();
                    if (dt1.Rows.Count > 0)
                    {
                        txtBrandName.Text = "";
                        DisplayMessage("Brand Name Already Exists - Go to Bin Tab");
                        txtBrandName.Focus();
                        return;
                    }
                }
            }
        }
        txtLBrandName.Focus();
    }

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
        txtValueBin.Focus();
    }
    protected void gvProductBrandMasterBin_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        gvProductBrandMasterBin.PageIndex = e.NewPageIndex;

        DataTable dt = (DataTable)Session["dtInactive"];
        gvProductBrandMasterBin.DataSource = dt;
        gvProductBrandMasterBin.DataBind();
        AllPageCode();
        string temp = string.Empty;

        for (int i = 0; i < gvProductBrandMasterBin.Rows.Count; i++)
        {
            Label lblconid = (Label)gvProductBrandMasterBin.Rows[i].FindControl("lblProductBrandId");
            string[] split = lblSelectedRecord.Text.Split(',');

            for (int j = 0; j < lblSelectedRecord.Text.Split(',').Length; j++)
            {
                if (lblSelectedRecord.Text.Split(',')[j] != "")
                {
                    if (lblconid.Text.Trim().ToString() == lblSelectedRecord.Text.Split(',')[j].Trim().ToString())
                    {
                        ((CheckBox)gvProductBrandMasterBin.Rows[i].FindControl("chkSelect")).Checked = true;
                    }
                }
            }
        }
        gvProductBrandMasterBin.BottomPagerRow.Focus();
    }
    protected void gvProductBrandMasterBin_OnSorting(object sender, GridViewSortEventArgs e)
    {
        HDFSortbin.Value = HDFSortbin.Value == "ASC" ? "DESC" : "ASC";
        DataTable dt = new DataTable();
        dt = objProB.GetProductBrandFalseAllData(StrCompId);
        DataView dv = new DataView(dt);
        string Query = "" + e.SortExpression + " " + HDFSortbin.Value + "";
        dv.Sort = Query;
        dt = dv.ToTable();
        Session["dtInactive"] = dt;
        gvProductBrandMasterBin.DataSource = dt;
        gvProductBrandMasterBin.DataBind();
        AllPageCode();
        lblSelectedRecord.Text = "";
        gvProductBrandMasterBin.HeaderRow.Focus();
    }
    public void FillGridBin()
    {
        DataTable dt = new DataTable();
        dt = objProB.GetProductBrandFalseAllData(StrCompId);
        gvProductBrandMasterBin.DataSource = dt;
        gvProductBrandMasterBin.DataBind();
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
            gvProductBrandMasterBin.DataSource = view.ToTable();
            gvProductBrandMasterBin.DataBind();
            lblSelectedRecord.Text = "";
            if (view.ToTable().Rows.Count == 0)
            {
                imgBtnRestore.Visible = false;
                ImgbtnSelectAll.Visible = false;
            }
            else
            {
                AllPageCode();
            }
            btnbindBin.Focus();
        }
    }

    protected void chkCurrent_CheckedChanged(object sender, EventArgs e)
    {
        CheckBox chkSelAll = ((CheckBox)gvProductBrandMasterBin.HeaderRow.FindControl("chkCurrent"));
        for (int i = 0; i < gvProductBrandMasterBin.Rows.Count; i++)
        {
            ((CheckBox)gvProductBrandMasterBin.Rows[i].FindControl("chkSelect")).Checked = chkSelAll.Checked;
            if (chkSelAll.Checked)
            {
                if (!lblSelectedRecord.Text.Split(',').Contains(((Label)(gvProductBrandMasterBin.Rows[i].FindControl("lblProductBrandId"))).Text.Trim().ToString()))
                {
                    lblSelectedRecord.Text += ((Label)(gvProductBrandMasterBin.Rows[i].FindControl("lblProductBrandId"))).Text.Trim().ToString() + ",";
                }
            }
            else
            {
                string temp = string.Empty;
                string[] split = lblSelectedRecord.Text.Split(',');
                foreach (string item in split)
                {
                    if (item != ((Label)(gvProductBrandMasterBin.Rows[i].FindControl("lblProductBrandId"))).Text.Trim().ToString())
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
        chkSelAll.Focus();
    }
    protected void chkSelect_CheckedChanged(object sender, EventArgs e)
    {
        string empidlist = string.Empty;
        string temp = string.Empty;
        int index = ((GridViewRow)((CheckBox)sender).Parent.Parent).RowIndex;
        Label lb = (Label)gvProductBrandMasterBin.Rows[index].FindControl("lblProductBrandId");
        if (((CheckBox)gvProductBrandMasterBin.Rows[index].FindControl("chkSelect")).Checked)
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
        ((CheckBox)gvProductBrandMasterBin.Rows[index].FindControl("chkSelect")).Focus();
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
                if (!lblSelectedRecord.Text.Split(',').Contains(dr["PBrandId"]))
                {
                    lblSelectedRecord.Text += dr["PBrandId"] + ",";
                }
            }
            for (int i = 0; i < gvProductBrandMasterBin.Rows.Count; i++)
            {
                string[] split = lblSelectedRecord.Text.Split(',');
                Label lblconid = (Label)gvProductBrandMasterBin.Rows[i].FindControl("lblProductBrandId");
                for (int j = 0; j < lblSelectedRecord.Text.Split(',').Length; j++)
                {
                    if (lblSelectedRecord.Text.Split(',')[j] != "")
                    {
                        if (lblconid.Text.Trim().ToString() == lblSelectedRecord.Text.Split(',')[j].Trim().ToString())
                        {
                            ((CheckBox)gvProductBrandMasterBin.Rows[i].FindControl("chkSelect")).Checked = true;
                        }
                    }
                }
            }
        }
        else
        {
            lblSelectedRecord.Text = "";
            DataTable dtUnit1 = (DataTable)Session["dtInactive"];
            gvProductBrandMasterBin.DataSource = dtUnit1;
            gvProductBrandMasterBin.DataBind();
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
                    b = objProB.DeleteProductBrandMaster(StrCompId, lblSelectedRecord.Text.Split(',')[j].Trim(), true.ToString(), Session["UserId"].ToString(), DateTime.Now.ToString());
                }
            }
        }

        if (b != 0)
        {

            FillGrid();
            FillGridBin();
            lblSelectedRecord.Text = "";
            ViewState["Select"] = null;
            DisplayMessage("Record Activated");
            btnRefreshBin_Click(null, null);
        }
        else
        {
            int fleg = 0;
            foreach (GridViewRow Gvr in gvProductBrandMasterBin.Rows)
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

        txtValueBin.Focus();

    }
    #endregion

    #endregion

    #region User defind Funcation
    private void FillGrid()
    {
        DataTable dtBrand = objProB.GetProductBrandTrueAllData(StrCompId);
        lblTotalRecords.Text = Resources.Attendance.Total_Records + " : " + dtBrand.Rows.Count + "";
        Session["dtBrand"] = dtBrand;
        Session["dtFilter"] = dtBrand;
        if (dtBrand != null && dtBrand.Rows.Count > 0)
        {
            gvProductBrand.DataSource = dtBrand;
            gvProductBrand.DataBind();
        }
        else
        {
            gvProductBrand.DataSource = null;
            gvProductBrand.DataBind();
        }
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
    public void Reset()
    {

        txtBrandName.Text = "";
        txtLBrandName.Text = "";
        txtDescription.Text = "";
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

    #region Auto Complete Funcation
    [System.Web.Services.WebMethodAttribute(), System.Web.Script.Services.ScriptMethodAttribute()]
    public static string[] GetCompletionList(string prefixText, int count, string contextKey)
    {
        Inv_ProductBrandMaster ts = new Inv_ProductBrandMaster();
        DataTable dt = ts.GetDistinctBrandName(HttpContext.Current.Session["CompId"].ToString(), prefixText);

        string[] str = new string[dt.Rows.Count];
        for (int i = 0; i < dt.Rows.Count; i++)
        {
            str[i] = dt.Rows[i]["Brand_Name"].ToString();
        }
        return str;
    }
    #endregion
}
