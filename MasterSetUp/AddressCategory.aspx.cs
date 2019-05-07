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

public partial class Master_AddressCategory : BasePage
{
    #region Defined Class Object
    Common cmn = new Common();
    Set_AddressCategory objAddCat = new Set_AddressCategory();
    SystemParameter ObjSysParam = new SystemParameter();
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
     
            ddlOption.SelectedIndex = 2;
            btnNew_Click(null, null);
            FillGridBin();
            FillGrid();


        }
        AllPageCode();

    }
    #region AllPageCode

    public void AllPageCode()
    {
        Page.Title = ObjSysParam.GetSysTitle();
        StrUserId = Session["UserId"].ToString();
        StrCompId = Session["CompId"].ToString();
        Session["AccordianId"] = "8";
        Session["HeaderText"] = "Master Setup";
        DataTable dtAllPageCode = cmn.GetAllPagePermission(StrUserId.ToString(), "8", "16");
        if (dtAllPageCode.Rows.Count != 0)
        {
            if (dtAllPageCode.Rows[0][0].ToString() == "SuperAdmin")
            {
                btnCSave.Visible = true;
                foreach (GridViewRow Row in GvAddressCategory.Rows)
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
                    foreach (GridViewRow Row in GvAddressCategory.Rows)
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
    #region User Defined Funcation
    public void FillGridBin()
    {

        DataTable dt = new DataTable();
        dt = objAddCat.GetAddressCategoryFalseAll(StrCompId.ToString());
        GvAddressCategoryBin.DataSource = dt;
        GvAddressCategoryBin.DataBind();
        Session["dtBinAddressCategory"] = dt;
        Session["dtBinFilter"] = dt;
        lblTotalRecordsBin.Text = Resources.Attendance.Total_Records + ": " + dt.Rows.Count.ToString() + "";

        lblSelectedRecord.Text = "";

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
    private void FillGrid()
    {
        DataTable dtBrand = objAddCat.GetAddressCategoryTrueAll(StrCompId.ToString());
        lblTotalRecords.Text = Resources.Attendance.Total_Records + ": " + dtBrand.Rows.Count + "";
        Session["dtAddressCategory"] = dtBrand;
        Session["dtFilter"] = dtBrand;
        if (dtBrand != null && dtBrand.Rows.Count > 0)
        {
            GvAddressCategory.DataSource = dtBrand;
            GvAddressCategory.DataBind();
        }
        else
        {
            GvAddressCategory.DataSource = null;
            GvAddressCategory.DataBind();
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

        txtAddressName.Text = "";
        editid.Value = "";
        btnNew.Text = Resources.Attendance.New;
        ddlFieldName.SelectedIndex = 1;
        ddlOption.SelectedIndex = 2;
        txtValue.Text = "";
        txtValueBin.Text = "";
        lblSelectedRecord.Text = "";
        System.Web.UI.ScriptManager.GetCurrent(this).SetFocus(txtValue);
        txtAddressNameL.Text = "";
    }
    #endregion
    #region Auto Complete Method/Funcation
    [System.Web.Services.WebMethodAttribute(), System.Web.Script.Services.ScriptMethodAttribute()]
    public static string[] GetCompletionList(string prefixText, int count, string contextKey)
    {
        Set_AddressCategory obj = new Set_AddressCategory();
        DataTable dt = obj.GetDistinctAddressName(HttpContext.Current.Session["CompId"].ToString(), prefixText);
        string[] str = new string[dt.Rows.Count];

        if (dt.Rows.Count > 0)
        {
            for (int i = 0; i < dt.Rows.Count; i++)
            {
                str[i] = dt.Rows[i][0].ToString();
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
                dt = obj.GetAddressCategoryTrueAll("1");
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
    #endregion
    #region System Defined Funcation

    protected void btnNew_Click(object sender, EventArgs e)
    {
        pnlMenuNew.BackColor = System.Drawing.ColorTranslator.FromHtml("#ccddee");


        pnlMenuBin.BackColor = System.Drawing.ColorTranslator.FromHtml("#90bde9");

        pnlList.Visible = true;

        pnlBin.Visible = false;

    }
    protected void btnBin_Click(object sender, EventArgs e)
    {
        pnlMenuBin.BackColor = System.Drawing.ColorTranslator.FromHtml("#ccddee");

        pnlMenuNew.BackColor = System.Drawing.ColorTranslator.FromHtml("#90bde9");

        pnlList.Visible = false;
        pnlBin.Visible = true;

        FillGridBin();
    }
    protected void BtnReset_Click(object sender, EventArgs e)
    {
        Reset();
        System.Web.UI.ScriptManager.GetCurrent(this).SetFocus(txtAddressName);
    }
    protected void btnCSave_Click(object sender, EventArgs e)
    {

        if (txtAddressName.Text == "" || txtAddressName.Text == null)
        {
            DisplayMessage("Enter Address Name");
            System.Web.UI.ScriptManager.GetCurrent(this).SetFocus(txtAddressName);
            return;
        }
        int b = 0;
        if (editid.Value != "")
        {

            DataTable dtCate = objAddCat.GetAddressCategoryTrueAll(StrCompId.ToString());
            dtCate = new DataView(dtCate, "Address_Name='" + txtAddressName.Text + "'", "", DataViewRowState.CurrentRows).ToTable();
            if (dtCate.Rows.Count > 0)
            {
                if (dtCate.Rows[0]["Address_Category_ID"].ToString() != editid.Value)
                {
                    txtAddressName.Text = "";
                    DisplayMessage("Address Category Already Exists");
                    System.Web.UI.ScriptManager.GetCurrent(this).SetFocus(txtAddressName);
                    return;
                }
            }


            b = objAddCat.UpdateAddressCategory(StrCompId.ToString(), editid.Value, txtAddressName.Text, txtAddressNameL.Text.Trim().ToString(), "", "", "", "", "", true.ToString(), DateTime.Now.ToString(), "True", Session["UserId"].ToString(), DateTime.Now.ToString());
            editid.Value = "";
            if (b != 0)
            {
                Reset();
                FillGrid();
                DisplayMessage("Record Updated");

            }
            else
            {
                DisplayMessage("Record  Not Updated");
            }
        }
        else
        {
            DataTable dtPro = objAddCat.GetAddressCategoryTrueAll(StrCompId.ToString());
            dtPro = new DataView(dtPro, "Address_Name='" + txtAddressName.Text + "'", "", DataViewRowState.CurrentRows).ToTable();

            if (dtPro.Rows.Count > 0)
            {
                txtAddressName.Text = "";
                DisplayMessage("Product Brand Name Already Exists");
                System.Web.UI.ScriptManager.GetCurrent(this).SetFocus(txtAddressName);
                return;
            }

            b = objAddCat.InsertAddressCategory(StrCompId.ToString(), txtAddressName.Text, txtAddressNameL.Text.Trim(), "", "", "", "", "", true.ToString(), DateTime.Now.ToString(), "True", Session["UserId"].ToString(), DateTime.Now.ToString(), Session["UserId"].ToString(), DateTime.Now.ToString());
            if (b != 0)
            {
                DisplayMessage("Record Saved");
                Reset();
                FillGrid();
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
        String CompanyId = Session["CompId"].ToString();
        String UserId = Session["UserId"].ToString();
        b = objAddCat.DeleteAddressCategory(StrCompId.ToString(), editid.Value, true.ToString(), StrUserId, DateTime.Now.ToString());
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
    protected void GvAddressCategoryBin_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        GvAddressCategoryBin.PageIndex = e.NewPageIndex;

        DataTable dt = (DataTable)Session["dtInactive"];
        GvAddressCategoryBin.DataSource = dt;
        GvAddressCategoryBin.DataBind();
        AllPageCode();
        string temp = string.Empty;


        for (int i = 0; i < GvAddressCategoryBin.Rows.Count; i++)
        {
            Label lblconid = (Label)GvAddressCategoryBin.Rows[i].FindControl("lblProductBrandId");
            string[] split = lblSelectedRecord.Text.Split(',');

            for (int j = 0; j < lblSelectedRecord.Text.Split(',').Length; j++)
            {
                if (lblSelectedRecord.Text.Split(',')[j] != "")
                {
                    if (lblconid.Text.Trim().ToString() == lblSelectedRecord.Text.Split(',')[j].Trim().ToString())
                    {
                        ((CheckBox)GvAddressCategoryBin.Rows[i].FindControl("chkgvSelect")).Checked = true;
                    }
                }
            }
        }

    }
    protected void GvAddressCategoryBin_OnSorting(object sender, GridViewSortEventArgs e)
    {
        HDFSortbin.Value = HDFSortbin.Value == "ASC" ? "DESC" : "ASC";
        DataTable dt = new DataTable();
        dt = objAddCat.GetAddressCategoryFalseAll(StrCompId.ToString());
        DataView dv = new DataView(dt);
        string Query = "" + e.SortExpression + " " + HDFSortbin.Value + "";
        dv.Sort = Query;
        dt = dv.ToTable();
        Session["dtInactive"] = dt;
        GvAddressCategoryBin.DataSource = dt;
        GvAddressCategoryBin.DataBind();
        lblSelectedRecord.Text = "";
        AllPageCode();
    }
    protected void btnEdit_Command(object sender, CommandEventArgs e)
    {
        editid.Value = e.CommandArgument.ToString();

        DataTable dtTax = objAddCat.GetAddressCategoryTruebyId(StrCompId.ToString(), editid.Value);

        btnNew.Text = Resources.Attendance.Edit;

        txtAddressName.Text = dtTax.Rows[0]["Address_Name"].ToString();
        txtAddressNameL.Text = dtTax.Rows[0]["Address_Name_L"].ToString();
        System.Web.UI.ScriptManager.GetCurrent(this).SetFocus(txtAddressName);
    }
    protected void GvAddressCategory_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        GvAddressCategory.PageIndex = e.NewPageIndex;
        DataTable dt = (DataTable)Session["dtFilter"];
        GvAddressCategory.DataSource = dt;
        GvAddressCategory.DataBind();
        AllPageCode();

    }
    protected void btnbindrpt_Click(object sender, ImageClickEventArgs e)
    {

        if (ddlOption.SelectedIndex != 0)
        {
            string condition = string.Empty;
            if (ddlOption.SelectedIndex == 1)
            {
                condition = "convert(" + ddlFieldName.SelectedValue + ",System.String)='" + txtValue.Text + "'";
            }
            else if (ddlOption.SelectedIndex == 2)
            {
                condition = "convert(" + ddlFieldName.SelectedValue + ",System.String) like '%" + txtValue.Text + "%'";
            }
            else
            {
                condition = "convert(" + ddlFieldName.SelectedValue + ",System.String) Like '" + txtValue.Text + "%'";
            }
            DataTable dtCurrency = (DataTable)Session["dtAddressCategory"];
            DataView view = new DataView(dtCurrency, condition, "", DataViewRowState.CurrentRows);
            GvAddressCategory.DataSource = view.ToTable();
            Session["dtFilter"] = view.ToTable();
            lblTotalRecords.Text = Resources.Attendance.Total_Records + " : " + view.ToTable().Rows.Count + "";
            GvAddressCategory.DataBind();
            AllPageCode();
        }
    }
    protected void GvAddressCategory_Sorting(object sender, GridViewSortEventArgs e)
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
        GvAddressCategory.DataSource = dt;
        GvAddressCategory.DataBind();

        AllPageCode();
    }
    protected void IbtnDelete_Command(object sender, CommandEventArgs e)
    {
        editid.Value = e.CommandArgument.ToString();
        int b = 0;
        String CompanyId = StrCompId.ToString();
        String UserId = StrUserId.ToString();
        b = objAddCat.DeleteAddressCategory(StrCompId.ToString(), editid.Value, "false", StrUserId.ToString(), DateTime.Now.ToString());
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
    }
    protected void btnRefreshReport_Click(object sender, ImageClickEventArgs e)
    {
        FillGrid();
        FillGridBin();
        ddlFieldName.SelectedIndex = 1;
        ddlOption.SelectedIndex = 2;
        txtValue.Text = "";
    }

    protected void btnbindBin_Click(object sender, EventArgs e)
    {
        string condition = string.Empty;
        if (ddlOptionBin.SelectedIndex != 0)
        {


            if (ddlOptionBin.SelectedIndex == 1)
            {
                condition = "convert(" + ddlFieldNameBin.SelectedValue + ",System.String)='" + txtValueBin.Text + "'";
            }
            else if (ddlOptionBin.SelectedIndex == 2)
            {
                condition = "convert(" + ddlFieldNameBin.SelectedValue + ",System.String) like '%" + txtValueBin.Text + "%'";
            }
            else
            {
                condition = "convert(" + ddlFieldNameBin.SelectedValue + ",System.String) Like '" + txtValueBin.Text + "%'";
            }

            DataTable dtCust = (DataTable)Session["dtBinAddressCategory"];
            DataView view = new DataView(dtCust, condition, "", DataViewRowState.CurrentRows);
            Session["dtBinFilter"] = view.ToTable();
            lblTotalRecordsBin.Text = Resources.Attendance.Total_Records + " : " + view.ToTable().Rows.Count.ToString() + "";
            GvAddressCategoryBin.DataSource = view.ToTable();
            GvAddressCategoryBin.DataBind();

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
            System.Web.UI.ScriptManager.GetCurrent(this).SetFocus(txtValueBin);
        }
    }
    protected void btnRestoreSelected_Click(object sender, EventArgs e)
    {
        int Msg = 0;
        DataTable dt = objAddCat.GetAddressCategoryFalseAll(StrCompId.ToString());

        if (GvAddressCategoryBin.Rows.Count != 0)
        {
            if (lblSelectedRecord.Text != "")
            {
                for (int j = 0; j < lblSelectedRecord.Text.Split(',').Length; j++)
                {
                    if (lblSelectedRecord.Text.Split(',')[j] != "")
                    {

                        Msg = objAddCat.DeleteAddressCategory(StrCompId.ToString(), lblSelectedRecord.Text.Split(',')[j].Trim().ToString(), true.ToString(), Session["UserId"].ToString(), DateTime.Now.ToString());
                    }
                }
            }

            if (Msg != 0)
            {
                FillGrid();
                FillGridBin();
                ViewState["Select"] = null;
                lblSelectedRecord.Text = "";
                DisplayMessage("Record Activated");

            }
            else
            {
                int fleg = 0;
                foreach (GridViewRow Gvr in GvAddressCategoryBin.Rows)
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
        }
        System.Web.UI.ScriptManager.GetCurrent(this).SetFocus(txtValueBin);
    }
    protected void ImgbtnSelectAll_Click(object sender, ImageClickEventArgs e)
    {
        DataTable dtAddressCategory = (DataTable)Session["dtbinFilter"];

        if (ViewState["Select"] == null)
        {
            ViewState["Select"] = 1;
            foreach (DataRow dr in dtAddressCategory.Rows)
            {
                if (!lblSelectedRecord.Text.Split(',').Contains(dr["Address_Category_ID"]))
                {
                    lblSelectedRecord.Text += dr["Address_Category_ID"] + ",";
                }
            }
            for (int i = 0; i < GvAddressCategoryBin.Rows.Count; i++)
            {
                string[] split = lblSelectedRecord.Text.Split(',');
                Label lblconid = (Label)GvAddressCategoryBin.Rows[i].FindControl("lblgvAddressCategoryId");
                for (int j = 0; j < lblSelectedRecord.Text.Split(',').Length; j++)
                {
                    if (lblSelectedRecord.Text.Split(',')[j] != "")
                    {
                        if (lblconid.Text.Trim().ToString() == lblSelectedRecord.Text.Split(',')[j].Trim().ToString())
                        {
                            ((CheckBox)GvAddressCategoryBin.Rows[i].FindControl("chkgvSelect")).Checked = true;
                        }
                    }
                }
            }
        }
        else
        {
            lblSelectedRecord.Text = "";
            DataTable dtAddressCategory1 = (DataTable)Session["dtBinFilter"];
            GvAddressCategoryBin.DataSource = dtAddressCategory1;
            GvAddressCategoryBin.DataBind();
            ViewState["Select"] = null;
        }



    }
    protected void chkgvSelectAll_CheckedChanged(object sender, EventArgs e)
    {
        CheckBox chkSelAll = ((CheckBox)GvAddressCategoryBin.HeaderRow.FindControl("chkgvSelectAll"));
        for (int i = 0; i < GvAddressCategoryBin.Rows.Count; i++)
        {
            ((CheckBox)GvAddressCategoryBin.Rows[i].FindControl("chkgvSelect")).Checked = chkSelAll.Checked;
            if (chkSelAll.Checked)
            {
                if (!lblSelectedRecord.Text.Split(',').Contains(((Label)(GvAddressCategoryBin.Rows[i].FindControl("lblgvAddressCategoryId"))).Text.Trim().ToString()))
                {
                    lblSelectedRecord.Text += ((Label)(GvAddressCategoryBin.Rows[i].FindControl("lblgvAddressCategoryId"))).Text.Trim().ToString() + ",";
                }
            }
            else
            {
                string temp = string.Empty;
                string[] split = lblSelectedRecord.Text.Split(',');
                foreach (string item in split)
                {
                    if (item != ((Label)(GvAddressCategoryBin.Rows[i].FindControl("lblgvAddressCategoryId"))).Text.Trim().ToString())
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
    protected void chkgvSelect_CheckedChanged(object sender, EventArgs e)
    {
        string empidlist = string.Empty;
        string temp = string.Empty;
        int index = ((GridViewRow)((CheckBox)sender).Parent.Parent).RowIndex;
        Label lb = (Label)GvAddressCategoryBin.Rows[index].FindControl("lblgvAddressCategoryId");
        if (((CheckBox)GvAddressCategoryBin.Rows[index].FindControl("chkgvSelect")).Checked)
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
        ViewState["Select"] = null;
        System.Web.UI.ScriptManager.GetCurrent(this).SetFocus(txtValueBin);
    }
    protected void txtAddressName_TextChanged(object sender, EventArgs e)
    {
        if (editid.Value == "")
        {
            DataTable dt = objAddCat.GetAddressCategoryByAddressName(StrCompId.ToString(), txtAddressName.Text.Trim());
            if (dt.Rows.Count > 0)
            {
                txtAddressName.Text = "";
                DisplayMessage("Address Name Already Exists");
                System.Web.UI.ScriptManager.GetCurrent(this).SetFocus(txtAddressName);
                return;
            }
            DataTable dt1 = objAddCat.GetAddressCategoryFalseAll(StrCompId.ToString());
            dt1 = new DataView(dt1, "Address_Name='" + txtAddressName.Text + "'", "", DataViewRowState.CurrentRows).ToTable();
            if (dt1.Rows.Count > 0)
            {
                txtAddressName.Text = "";
                DisplayMessage("Address Name Already Exists - Go to Bin Tab");
                System.Web.UI.ScriptManager.GetCurrent(this).SetFocus(txtAddressName);
                return;
            }
        }
        else
        {
            DataTable dtTemp = objAddCat.GetAddressCategoryTruebyId(StrCompId.ToString(), editid.Value);
            if (dtTemp.Rows.Count > 0)
            {
                if (dtTemp.Rows[0]["Address_Name"].ToString() != txtAddressName.Text)
                {
                    DataTable dt = objAddCat.GetAddressCategoryTrueAll(StrCompId.ToString());
                    dt = new DataView(dt, "Address_Name='" + txtAddressName.Text + "'", "", DataViewRowState.CurrentRows).ToTable();
                    if (dt.Rows.Count > 0)
                    {
                        txtAddressName.Text = "";
                        DisplayMessage("Address Name Already Exists");
                        System.Web.UI.ScriptManager.GetCurrent(this).SetFocus(txtAddressName);
                        return;
                    }
                    DataTable dt1 = objAddCat.GetAddressCategoryFalseAll(StrCompId.ToString());
                    dt1 = new DataView(dt1, "Address_Name='" + txtAddressName.Text + "'", "", DataViewRowState.CurrentRows).ToTable();
                    if (dt1.Rows.Count > 0)
                    {
                        txtAddressName.Text = "";
                        DisplayMessage("Address Name Already Exists - Go to Bin Tab");
                        System.Web.UI.ScriptManager.GetCurrent(this).SetFocus(txtAddressName);
                        return;
                    }
                }
            }
        }
        System.Web.UI.ScriptManager.GetCurrent(this).SetFocus(btnCSave);
    }
    #endregion



}
