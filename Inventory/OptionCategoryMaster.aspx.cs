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

public partial class Inventory_OptionCategoryMaster : BasePage
{
    #region Object Defined
    Common cmn = new Common();
    Inv_OptionCategoryMaster objOptionCategory = new Inv_OptionCategoryMaster();
    string StrCompId = string.Empty;
    string StrUserId = string.Empty;
    SystemParameter ObjSysPeram = new SystemParameter();
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
            FillGridBin();
            FillGrid();
            btnNew_Click(null, null);

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
        DataTable dtAllPageCode = cmn.GetAllPagePermission(StrUserId.ToString(), "11", "29");
        if (dtAllPageCode.Rows.Count != 0)
        {
            if (dtAllPageCode.Rows[0][0].ToString() == "SuperAdmin")
            {
                btnCSave.Visible = true;
                foreach (GridViewRow Row in GvOptionCategory.Rows)
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
                    foreach (GridViewRow Row in GvOptionCategory.Rows)
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


    #region System Defind Funcation:-Events
    protected void btnNew_Click(object sender, EventArgs e)
    {
        pnlMenuNew.BackColor = System.Drawing.ColorTranslator.FromHtml("#ccddee");


        pnlMenuBin.BackColor = System.Drawing.ColorTranslator.FromHtml("#90bde9");

        pnlList.Visible = true;

        pnlBin.Visible = false;
        txtOpCateName.Focus();
    }
    protected void btnBin_Click(object sender, EventArgs e)
    {
        pnlMenuBin.BackColor = System.Drawing.ColorTranslator.FromHtml("#ccddee");

        pnlMenuNew.BackColor = System.Drawing.ColorTranslator.FromHtml("#90bde9");

        pnlList.Visible = false;
        pnlBin.Visible = true;
        FillGridBin();
        txtValueBin.Focus();
    }
    protected void btnCSave_Click(object sender, EventArgs e)
    {
        if (txtOpCateName.Text == "")
        {
            DisplayMessage("Enter Option Category Name");
            Focus();
            return;
        }
        int b = 0;
        if (editid.Value == "")
        {
            b = objOptionCategory.InsertOptionCategory(StrCompId.ToString(), txtOpCateName.Text.Trim(), txtlblOpCateNameLocal.Text.Trim(), "", "", "", "", "", true.ToString(), DateTime.Now.ToString(), true.ToString(), StrUserId, DateTime.Now.ToString(), StrUserId.ToString(), DateTime.Now.ToString());
            if (b != 0)
            {
                DisplayMessage("Record Saved");
                FillGrid();
                Reset();
            }
        }
        else
        {
            b = objOptionCategory.UpdateOptionCategory(StrCompId.ToString(), editid.Value.ToString(), txtOpCateName.Text.Trim(), txtlblOpCateNameLocal.Text.Trim(), "", "", "", "", "", true.ToString(), DateTime.Now.ToString(), true.ToString(), StrUserId, DateTime.Now.ToString());
            if (b != 0)
            {
                DisplayMessage("Record Updated");
                FillGrid();
                Reset();
            }

        }
        txtOpCateName.Focus();
    }
    protected void GvOptionCategory_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        GvOptionCategory.PageIndex = e.NewPageIndex;
        DataTable dt = (DataTable)Session["dtFilter"];
        GvOptionCategory.DataSource = dt;
        GvOptionCategory.DataBind();
        AllPageCode();
        GvOptionCategory.BottomPagerRow.Focus();
    }
    protected void GvOptionCategory_Sorting(object sender, GridViewSortEventArgs e)
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
        GvOptionCategory.DataSource = dt;
        GvOptionCategory.DataBind();

        AllPageCode();
        GvOptionCategory.HeaderRow.Focus();
    }
    protected void btnEdit_Command(object sender, CommandEventArgs e)
    {
        editid.Value = e.CommandArgument.ToString();
        DataTable dt = objOptionCategory.GetOptionCategoryTruebyId(StrCompId.ToString(), editid.Value);
        if (dt.Rows.Count != 0)
        {
            txtOpCateName.Text = dt.Rows[0]["EName"].ToString();
            txtlblOpCateNameLocal.Text = dt.Rows[0]["LName"].ToString();

            txtOpCateName.Focus();
        }
        btnNew.Text = Resources.Attendance.Edit;
    
    }
    protected void IbtnDelete_Command(object sender, CommandEventArgs e)
    {
        int b = 0;
        b = objOptionCategory.DeleteOptionCategory(StrCompId.ToString(), e.CommandArgument.ToString(), false.ToString(), StrUserId.ToString(), DateTime.Now.ToString());
        if (b != 0)
        {
            DisplayMessage("Record Deleted");
            FillGridBin();
            BtnReset_Click(null, null);


        }
        try
        {
            int i = ((GridViewRow)((ImageButton)sender).Parent.Parent).RowIndex;
            ((ImageButton)GvOptionCategory.Rows[i].FindControl("IbtnDelete")).Focus();
        }
        catch
        {
            txtValue.Focus();
        
        }
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
            DataTable dtCurrency = (DataTable)Session["dtoptionCate"];
            DataView view = new DataView(dtCurrency, condition, "", DataViewRowState.CurrentRows);
            GvOptionCategory.DataSource = view.ToTable();
            Session["dtFilter"] = view.ToTable();
            lblTotalRecords.Text = Resources.Attendance.Total_Records + " : " + view.ToTable().Rows.Count + "";
            GvOptionCategory.DataBind();
            AllPageCode();
            btnbind.Focus();
        }
    }
    protected void btnRefreshReport_Click(object sender, ImageClickEventArgs e)
    {
        FillGrid();
        ddlFieldName.SelectedIndex = 1;
        ddlOption.SelectedIndex = 2;
        txtValue.Text = "";
        txtValue.Focus();
    }
    protected void BtnReset_Click(object sender, EventArgs e)
    {
        Reset();
        FillGrid();
        txtValue.Text = "";
        ddlFieldName.SelectedIndex = 1;
        ddlOption.SelectedIndex = 2;
        txtOpCateName.Focus();
    }

    protected void txtOpCateName_TextChanged(object sender, EventArgs e)
    {
        if (txtOpCateName.Text != "")
        {
            DataTable dt = new DataView(objOptionCategory.GetOptionCategoryAll(StrCompId.ToString()), "EName='" + txtOpCateName.Text.Trim() + "'", "", DataViewRowState.CurrentRows).ToTable();
            if (dt.Rows.Count != 0)
            {
                if (dt.Rows[0]["OptionCategoryId"].ToString() != editid.Value.Trim())
                {
                    if (Convert.ToBoolean(dt.Rows[0]["IsActive"].ToString()))
                    {
                        DisplayMessage("Option Category Already Exists");
                        txtOpCateName.Text = "";
                        txtOpCateName.Focus();
                    }
                    else
                    {
                        DisplayMessage("Option Category Already Exists :- Go to Bin Tab");
                        txtOpCateName.Text = "";
                        txtOpCateName.Focus();

                    }
                }

            }

            else
            {
                txtlblOpCateNameLocal.Focus();
            }


        }
    }


    #region Bin

    protected void btnbindBin_Click(object sender, ImageClickEventArgs e)
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

            DataTable dtCust = (DataTable)Session["dtBinOpCate"];
            DataView view = new DataView(dtCust, condition, "", DataViewRowState.CurrentRows);
            Session["dtBinFilter"] = view.ToTable();
            lblTotalRecordsBin.Text = Resources.Attendance.Total_Records + " : " + view.ToTable().Rows.Count.ToString() + "";
            GvOptionCategoryBin.DataSource = view.ToTable();
            GvOptionCategoryBin.DataBind();

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
    protected void btnRefreshBin_Click(object sender, ImageClickEventArgs e)
    {

        FillGridBin();
        txtValueBin.Text = "";
        ddlOptionBin.SelectedIndex = 2;
        ddlFieldNameBin.SelectedIndex = 1;
        lblSelectedRecord.Text = "";
        ViewState["Select"] = null;
        txtValueBin.Focus();

    }
    protected void btnRestoreSelected_Click(object sender, ImageClickEventArgs e)
    {
        int Msg = 0;
        DataTable dt = objOptionCategory.GetAddressCategoryFalseAll(StrCompId.ToString());

        if (GvOptionCategoryBin.Rows.Count != 0)
        {
            if (lblSelectedRecord.Text != "")
            {
                for (int j = 0; j < lblSelectedRecord.Text.Split(',').Length; j++)
                {
                    if (lblSelectedRecord.Text.Split(',')[j] != "")
                    {

                        Msg = objOptionCategory.DeleteOptionCategory(StrCompId.ToString(), lblSelectedRecord.Text.Split(',')[j].Trim().ToString(), true.ToString(), Session["UserId"].ToString(), DateTime.Now.ToString());
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
                btnRefreshBin_Click(null, null);

            }
            else
            {
                int fleg = 0;
                foreach (GridViewRow Gvr in GvOptionCategoryBin.Rows)
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
        txtValueBin.Focus();

    }
    protected void ImgbtnSelectAll_Click(object sender, ImageClickEventArgs e)
    {
        DataTable dtOptionCategory = (DataTable)Session["dtbinFilter"];

        if (ViewState["Select"] == null)
        {
            ViewState["Select"] = 1;
            foreach (DataRow dr in dtOptionCategory.Rows)
            {
                if (!lblSelectedRecord.Text.Split(',').Contains(dr["OptionCategoryID"]))
                {
                    lblSelectedRecord.Text += dr["OptionCategoryID"] + ",";
                }
            }
            for (int i = 0; i < GvOptionCategoryBin.Rows.Count; i++)
            {
                string[] split = lblSelectedRecord.Text.Split(',');
                Label lblconid = (Label)GvOptionCategoryBin.Rows[i].FindControl("lblgvOptionCategoryId");
                for (int j = 0; j < lblSelectedRecord.Text.Split(',').Length; j++)
                {
                    if (lblSelectedRecord.Text.Split(',')[j] != "")
                    {
                        if (lblconid.Text.Trim().ToString() == lblSelectedRecord.Text.Split(',')[j].Trim().ToString())
                        {
                            ((CheckBox)GvOptionCategoryBin.Rows[i].FindControl("chkgvSelect")).Checked = true;
                        }
                    }
                }
            }
        }
        else
        {
            lblSelectedRecord.Text = "";
            DataTable dtOptionCategory1 = (DataTable)Session["dtBinFilter"];
            GvOptionCategoryBin.DataSource = dtOptionCategory1;
            GvOptionCategoryBin.DataBind();
            AllPageCode();
            ViewState["Select"] = null;
        }

        ImgbtnSelectAll.Focus();


    }
    protected void chkgvSelect_CheckedChanged(object sender, EventArgs e)
    {
        string OptCateidlist = string.Empty;
        string temp = string.Empty;
        int index = ((GridViewRow)((CheckBox)sender).Parent.Parent).RowIndex;
        Label lb = (Label)GvOptionCategoryBin.Rows[index].FindControl("lblgvOptionCategoryId");
        if (((CheckBox)GvOptionCategoryBin.Rows[index].FindControl("chkgvSelect")).Checked)
        {
            OptCateidlist += lb.Text.Trim().ToString() + ",";
            lblSelectedRecord.Text += OptCateidlist;
        }
        else
        {
            OptCateidlist += lb.Text.ToString().Trim();
            lblSelectedRecord.Text += OptCateidlist;
            string[] split = lblSelectedRecord.Text.Split(',');
            foreach (string item in split)
            {
                if (item != OptCateidlist)
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
        ((CheckBox)GvOptionCategoryBin.Rows[index].FindControl("chkgvSelect")).Focus();
    }
    protected void chkgvSelectAll_CheckedChanged(object sender, EventArgs e)
    {
        CheckBox chkSelAll = ((CheckBox)GvOptionCategoryBin.HeaderRow.FindControl("chkgvSelectAll"));
        for (int i = 0; i < GvOptionCategoryBin.Rows.Count; i++)
        {
            ((CheckBox)GvOptionCategoryBin.Rows[i].FindControl("chkgvSelect")).Checked = chkSelAll.Checked;
            if (chkSelAll.Checked)
            {
                if (!lblSelectedRecord.Text.Split(',').Contains(((Label)(GvOptionCategoryBin.Rows[i].FindControl("lblgvOptionCategoryId"))).Text.Trim().ToString()))
                {
                    lblSelectedRecord.Text += ((Label)(GvOptionCategoryBin.Rows[i].FindControl("lblgvOptionCategoryId"))).Text.Trim().ToString() + ",";
                }
            }
            else
            {
                string temp = string.Empty;
                string[] split = lblSelectedRecord.Text.Split(',');
                foreach (string item in split)
                {
                    if (item != ((Label)(GvOptionCategoryBin.Rows[i].FindControl("lblgvOptionCategoryId"))).Text.Trim().ToString())
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
    protected void GvOptionCategoryBin_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        GvOptionCategoryBin.PageIndex = e.NewPageIndex;

        DataTable dt = (DataTable)Session["dtBinFilter"];
        GvOptionCategoryBin.DataSource = dt;
        GvOptionCategoryBin.DataBind();
        AllPageCode();
        string temp = string.Empty;


        for (int i = 0; i < GvOptionCategoryBin.Rows.Count; i++)
        {
            Label lblconid = (Label)GvOptionCategoryBin.Rows[i].FindControl("lblgvOptionCategoryId");
            string[] split = lblSelectedRecord.Text.Split(',');

            for (int j = 0; j < lblSelectedRecord.Text.Split(',').Length; j++)
            {
                if (lblSelectedRecord.Text.Split(',')[j] != "")
                {
                    if (lblconid.Text.Trim().ToString() == lblSelectedRecord.Text.Split(',')[j].Trim().ToString())
                    {
                        ((CheckBox)GvOptionCategoryBin.Rows[i].FindControl("chkgvSelect")).Checked = true;
                    }
                }
            }
        }
        GvOptionCategoryBin.BottomPagerRow.Focus();
    }
    protected void GvOptionCategoryBin_OnSorting(object sender, GridViewSortEventArgs e)
    {

        HDFSortbin.Value = HDFSortbin.Value == "ASC" ? "DESC" : "ASC";
        DataTable dt = new DataTable();
        dt = (DataTable)Session["dtBinFilter"];
        DataView dv = new DataView(dt);
        string Query = "" + e.SortExpression + " " + HDFSortbin.Value + "";
        dv.Sort = Query;
        dt = dv.ToTable();
        Session["dtBinFilter"] = dt;
        GvOptionCategoryBin.DataSource = dt;
        GvOptionCategoryBin.DataBind();
        lblSelectedRecord.Text = "";
        AllPageCode();
        GvOptionCategoryBin.HeaderRow.Focus();
    }
    #endregion
    #region Auto Complete Method

    [System.Web.Services.WebMethodAttribute(), System.Web.Script.Services.ScriptMethodAttribute()]
    public static string[] GetCompletionList(string prefixText, int count, string contextKey)
    {
        Inv_OptionCategoryMaster ObjOptCat = new Inv_OptionCategoryMaster();
        DataTable dt = new DataView(ObjOptCat.GetOptionCategoryTrueAll(HttpContext.Current.Session["CompId"].ToString()), "EName='" + prefixText.ToString() + "'", "", DataViewRowState.CurrentRows).ToTable();
        string[] text = new string[dt.Rows.Count];
        for (int i = 0; i < dt.Rows.Count; i++)
        {
            text[i] = dt.Rows[i]["EName"].ToString();

        }
        return text;

    }

    #endregion

    #endregion


    #region User Defind Funcation

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
    public void FillGrid()
    {
        DataTable dt = objOptionCategory.GetOptionCategoryTrueAll(StrCompId.ToString());
        Session["dtoptionCate"] = dt;
        Session["dtFilter"] = dt;
        GvOptionCategory.DataSource = dt;
        GvOptionCategory.DataBind();
        lblTotalRecords.Text = Resources.Attendance.Total_Records + " : " + dt.Rows.Count.ToString();
        AllPageCode();
    }
    public void Reset()
    {
        txtOpCateName.Text = "";
        txtlblOpCateNameLocal.Text = "";
        editid.Value = "";
        btnRefreshReport_Click(null, null);
        btnNew.Text = Resources.Attendance.New;

    }

    #region//Bin
    public void FillGridBin()
    {

        DataTable dt = objOptionCategory.GetAddressCategoryFalseAll(StrCompId.ToString());

        GvOptionCategoryBin.DataSource = dt;
        GvOptionCategoryBin.DataBind();
        Session["dtBinOpCate"] = dt;
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
    #endregion
    #endregion



}
