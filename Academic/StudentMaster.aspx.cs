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
using System.IO;

public partial class Academic_StudentMaster : BasePage
{
    #region Defined Class Object
    Common cmn = new Common();
    SystemParameter objSys = new SystemParameter();
    StudentMaster objDesg = new StudentMaster();

    #endregion
    protected void Page_Load(object sender, EventArgs e)
    {

        if (Session["UserId"] == null)
        {
            Response.Redirect("~/ERPLogin.aspx");
        }
        AllPageCode();
        if (!IsPostBack)
        {
            ddlOption.SelectedIndex = 2;
            btnNew_Click(null, null);
            FillGridBin();
            FillGrid();
            ddlFieldName.SelectedIndex = 0;
            txtStudentName.Focus();
        }


    }
    public void AllPageCode()
    {

        Page.Title = objSys.GetSysTitle();
        Session["AccordianId"] = "7";
        Session["HeaderText"] = "System Setup";
        DataTable dtAllPageCode = cmn.GetAllPagePermission(Session["UserId"].ToString(), "7", "8");
        if (dtAllPageCode.Rows.Count != 0)
        {
            if (dtAllPageCode.Rows[0][0].ToString() == "SuperAdmin")
            {
                btnCSave.Visible = true;
                foreach (GridViewRow Row in GvStudent.Rows)
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
                    foreach (GridViewRow Row in GvStudent.Rows)
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
    #region User Defined Funcation
    public void FillGridBin()
    {

        DataTable dt = new DataTable();
        dt = objDesg.GetStudentMasterInactive(Session["CompId"].ToString().ToString());
        GvStudentBin.DataSource = dt;
        GvStudentBin.DataBind();
        Session["dtBinStudent"] = dt;
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
        DataTable dtBrand = objDesg.GetStudentMaster(Session["CompId"].ToString().ToString());
        lblTotalRecords.Text = Resources.Attendance.Total_Records + ": " + dtBrand.Rows.Count + "";
        Session["dtStudent"] = dtBrand;
        Session["dtFilter"] = dtBrand;
        if (dtBrand != null && dtBrand.Rows.Count > 0)
        {
            GvStudent.DataSource = dtBrand;
            GvStudent.DataBind();
            AllPageCode();
        }
        else
        {
            GvStudent.DataSource = null;
            GvStudent.DataBind();
        }

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

        txtStudentName.Text = "";
        txtStudentNameL.Text = "";
        editid.Value = "";
        btnNew.Text = Resources.Attendance.New;
        ddlFieldName.SelectedIndex = 0;
        ddlOption.SelectedIndex = 2;
        txtValue.Text = "";
        txtValueBin.Text = "";
        lblSelectedRecord.Text = "";
        System.Web.UI.ScriptManager.GetCurrent(this).SetFocus(txtValue);
        txtStudentNameL.Text = "";
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
        System.Web.UI.ScriptManager.GetCurrent(this).SetFocus(txtStudentName);
    }
    protected void btnCSave_Click(object sender, EventArgs e)
    {

        if (txtStudentName.Text == "" || txtStudentName.Text == null)
        {
            DisplayMessage("Enter Student Name");
            System.Web.UI.ScriptManager.GetCurrent(this).SetFocus(txtStudentName);
            return;
        }
        int b = 0;
        if (editid.Value != "")
        {

            DataTable dtCate = objDesg.GetStudentMaster(Session["CompId"].ToString().ToString());
            dtCate = new DataView(dtCate, "Student='" + txtStudentName.Text + "'", "", DataViewRowState.CurrentRows).ToTable();
            if (dtCate.Rows.Count > 0)
            {
                if (dtCate.Rows[0]["Student_ID"].ToString() != editid.Value)
                {
                    txtStudentName.Text = "";
                    DisplayMessage("Student Already Exists");
                    System.Web.UI.ScriptManager.GetCurrent(this).SetFocus(txtStudentName);
                    return;
                }
            }


            b = objDesg.UpdateStudentMaster(Session["CompId"].ToString().ToString(), editid.Value, txtStudentName.Text, txtStudentNameL.Text.Trim().ToString(), "", "", "", "", "", true.ToString(), DateTime.Now.ToString(), "True", Session["UserId"].ToString(), DateTime.Now.ToString());
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
            DataTable dtPro = objDesg.GetStudentMaster(Session["CompId"].ToString().ToString());
            dtPro = new DataView(dtPro, "Student='" + txtStudentName.Text + "'", "", DataViewRowState.CurrentRows).ToTable();

            if (dtPro.Rows.Count > 0)
            {
                txtStudentName.Text = "";
                DisplayMessage("Student Name Already Exists");
                System.Web.UI.ScriptManager.GetCurrent(this).SetFocus(txtStudentName);
                return;
            }

            b = objDesg.InsertStudentMaster(Session["CompId"].ToString().ToString(), txtStudentName.Text, txtStudentNameL.Text.Trim(), "", "", "", "", "", true.ToString(), DateTime.Now.ToString(), "True", Session["UserId"].ToString(), DateTime.Now.ToString(), Session["UserId"].ToString(), DateTime.Now.ToString());
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
        b = objDesg.DeleteStudentMaster(Session["CompId"].ToString().ToString(), editid.Value, true.ToString(), Session["UserId"].ToString(), DateTime.Now.ToString());
        if (b != 0)
        {
            DisplayMessage("Record Restored");
        }
        else
        {
            DisplayMessage("Record  Restore Fail");
        }
        FillGrid();
        FillGridBin();
        Reset();



    }
    protected void GvStudentBin_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        GvStudentBin.PageIndex = e.NewPageIndex;

        DataTable dt = (DataTable)Session["dtBinFilter"];
        GvStudentBin.DataSource = dt;
        GvStudentBin.DataBind();
        AllPageCode();
        string temp = string.Empty;


        for (int i = 0; i < GvStudentBin.Rows.Count; i++)
        {
            Label lblconid = (Label)GvStudentBin.Rows[i].FindControl("lblStudentId");
            string[] split = lblSelectedRecord.Text.Split(',');

            for (int j = 0; j < lblSelectedRecord.Text.Split(',').Length; j++)
            {
                if (lblSelectedRecord.Text.Split(',')[j] != "")
                {
                    if (lblconid.Text.Trim().ToString() == lblSelectedRecord.Text.Split(',')[j].Trim().ToString())
                    {
                        ((CheckBox)GvStudentBin.Rows[i].FindControl("chkgvSelect")).Checked = true;
                    }
                }
            }
        }

    }
    protected void GvStudentBin_OnSorting(object sender, GridViewSortEventArgs e)
    {
        HDFSortbin.Value = HDFSortbin.Value == "ASC" ? "DESC" : "ASC";
        DataTable dt = new DataTable();
        dt = objDesg.GetStudentMasterInactive(Session["CompId"].ToString().ToString());
        DataView dv = new DataView(dt);
        string Query = "" + e.SortExpression + " " + HDFSortbin.Value + "";
        dv.Sort = Query;
        dt = dv.ToTable();
        Session["dtBinFilter"] = dt;
        GvStudentBin.DataSource = dt;
        GvStudentBin.DataBind();
        AllPageCode();
        lblSelectedRecord.Text = "";

    }
    protected void btnEdit_Command(object sender, CommandEventArgs e)
    {
        editid.Value = e.CommandArgument.ToString();

        DataTable dtTax = objDesg.GetStudentMasterById(Session["CompId"].ToString().ToString(), editid.Value);

        btnNew.Text = Resources.Attendance.Edit;

        txtStudentName.Text = dtTax.Rows[0]["Student"].ToString();
        txtStudentNameL.Text = dtTax.Rows[0]["Student_L"].ToString();
        System.Web.UI.ScriptManager.GetCurrent(this).SetFocus(txtStudentName);
    }
    protected void GvStudent_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        GvStudent.PageIndex = e.NewPageIndex;
        DataTable dt = (DataTable)Session["dtFilter"];
        GvStudent.DataSource = dt;
        GvStudent.DataBind();
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
            DataTable dtCurrency = (DataTable)Session["dtStudent"];
            DataView view = new DataView(dtCurrency, condition, "", DataViewRowState.CurrentRows);
            GvStudent.DataSource = view.ToTable();
            Session["dtFilter"] = view.ToTable();
            lblTotalRecords.Text = Resources.Attendance.Total_Records + " : " + view.ToTable().Rows.Count + "";
            GvStudent.DataBind();
            AllPageCode();
        }
    }
    protected void GvStudent_Sorting(object sender, GridViewSortEventArgs e)
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
        GvStudent.DataSource = dt;
        GvStudent.DataBind();
        AllPageCode();

    }
    protected void IbtnDelete_Command(object sender, CommandEventArgs e)
    {
        editid.Value = e.CommandArgument.ToString();
        int b = 0;
        String CompanyId = Session["CompId"].ToString().ToString();
        String UserId = Session["UserId"].ToString().ToString();
        b = objDesg.DeleteStudentMaster(Session["CompId"].ToString().ToString(), editid.Value, "false", Session["UserId"].ToString().ToString(), DateTime.Now.ToString());
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
        ddlFieldName.SelectedIndex = 0;
        ddlOption.SelectedIndex = 2;
        txtValue.Text = "";
    }

    [System.Web.Services.WebMethodAttribute(), System.Web.Script.Services.ScriptMethodAttribute()]
    public static string[] GetCompletionListStudent(string prefixText, int count, string contextKey)
    {
        StudentMaster objStudent = new StudentMaster();
        DataTable dt = new DataView(objStudent.GetStudentMaster(HttpContext.Current.Session["CompId"].ToString()), "Student like '" + prefixText.ToString() + "%'", "", DataViewRowState.CurrentRows).ToTable();
        string[] txt = new string[dt.Rows.Count];

        for (int i = 0; i < dt.Rows.Count; i++)
        {
            txt[i] = dt.Rows[i]["Student"].ToString();
        }
        return txt;
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

            DataTable dtCust = (DataTable)Session["dtBinStudent"];
            DataView view = new DataView(dtCust, condition, "", DataViewRowState.CurrentRows);
            Session["dtBinFilter"] = view.ToTable();
            lblTotalRecordsBin.Text = Resources.Attendance.Total_Records + " : " + view.ToTable().Rows.Count.ToString() + "";
            GvStudentBin.DataSource = view.ToTable();
            GvStudentBin.DataBind();

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
        DataTable dt = objDesg.GetStudentMasterInactive(Session["CompId"].ToString().ToString());

        if (GvStudentBin.Rows.Count != 0)
        {
            if (lblSelectedRecord.Text != "")
            {
                for (int j = 0; j < lblSelectedRecord.Text.Split(',').Length; j++)
                {
                    if (lblSelectedRecord.Text.Split(',')[j] != "")
                    {

                        Msg = objDesg.DeleteStudentMaster(Session["CompId"].ToString().ToString(), lblSelectedRecord.Text.Split(',')[j].Trim().ToString(), true.ToString(), Session["UserId"].ToString(), DateTime.Now.ToString());
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
                foreach (GridViewRow Gvr in GvStudentBin.Rows)
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
        DataTable dtStudent = (DataTable)Session["dtbinFilter"];

        if (ViewState["Select"] == null)
        {
            ViewState["Select"] = 1;
            foreach (DataRow dr in dtStudent.Rows)
            {
                if (!lblSelectedRecord.Text.Split(',').Contains(dr["Student_ID"]))
                {
                    lblSelectedRecord.Text += dr["Student_ID"] + ",";
                }
            }
            for (int i = 0; i < GvStudentBin.Rows.Count; i++)
            {
                string[] split = lblSelectedRecord.Text.Split(',');
                Label lblconid = (Label)GvStudentBin.Rows[i].FindControl("lblStudentId");
                for (int j = 0; j < lblSelectedRecord.Text.Split(',').Length; j++)
                {
                    if (lblSelectedRecord.Text.Split(',')[j] != "")
                    {
                        if (lblconid.Text.Trim().ToString() == lblSelectedRecord.Text.Split(',')[j].Trim().ToString())
                        {
                            ((CheckBox)GvStudentBin.Rows[i].FindControl("chkgvSelect")).Checked = true;
                        }
                    }
                }
            }
        }
        else
        {
            lblSelectedRecord.Text = "";
            DataTable dtStudent1 = (DataTable)Session["dtBinFilter"];
            GvStudentBin.DataSource = dtStudent1;
            GvStudentBin.DataBind();
            ViewState["Select"] = null;
        }



    }
    protected void chkgvSelectAll_CheckedChanged(object sender, EventArgs e)
    {
        CheckBox chkSelAll = ((CheckBox)GvStudentBin.HeaderRow.FindControl("chkgvSelectAll"));
        for (int i = 0; i < GvStudentBin.Rows.Count; i++)
        {
            ((CheckBox)GvStudentBin.Rows[i].FindControl("chkgvSelect")).Checked = chkSelAll.Checked;
            if (chkSelAll.Checked)
            {
                if (!lblSelectedRecord.Text.Split(',').Contains(((Label)(GvStudentBin.Rows[i].FindControl("lblStudentId"))).Text.Trim().ToString()))
                {
                    lblSelectedRecord.Text += ((Label)(GvStudentBin.Rows[i].FindControl("lblStudentId"))).Text.Trim().ToString() + ",";
                }
            }
            else
            {
                string temp = string.Empty;
                string[] split = lblSelectedRecord.Text.Split(',');
                foreach (string item in split)
                {
                    if (item != ((Label)(GvStudentBin.Rows[i].FindControl("lblStudentId"))).Text.Trim().ToString())
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
        Label lb = (Label)GvStudentBin.Rows[index].FindControl("lblStudentId");
        if (((CheckBox)GvStudentBin.Rows[index].FindControl("chkgvSelect")).Checked)
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
        ddlFieldNameBin.SelectedIndex = 0;
        lblSelectedRecord.Text = "";
        ViewState["Select"] = null;
        System.Web.UI.ScriptManager.GetCurrent(this).SetFocus(txtValueBin);
    }
    protected void txtStudentName_TextChanged(object sender, EventArgs e)
    {
        if (editid.Value == "")
        {
            DataTable dt = objDesg.GetStudentMasterByStudentName(Session["CompId"].ToString().ToString(), txtStudentName.Text.Trim());
            if (dt.Rows.Count > 0)
            {
                txtStudentName.Text = "";
                DisplayMessage("Student Name Already Exists");
                System.Web.UI.ScriptManager.GetCurrent(this).SetFocus(txtStudentName);
                return;
            }
            DataTable dt1 = objDesg.GetStudentMasterInactive(Session["CompId"].ToString().ToString());
            dt1 = new DataView(dt1, "Student='" + txtStudentName.Text + "'", "", DataViewRowState.CurrentRows).ToTable();
            if (dt1.Rows.Count > 0)
            {
                txtStudentName.Text = "";
                DisplayMessage("Student Name Already Exists - Go to Bin Tab");
                System.Web.UI.ScriptManager.GetCurrent(this).SetFocus(txtStudentName);
                return;
            }
            txtStudentNameL.Focus();
        }
        else
        {
            DataTable dtTemp = objDesg.GetStudentMasterById(Session["CompId"].ToString().ToString(), editid.Value);
            if (dtTemp.Rows.Count > 0)
            {
                if (dtTemp.Rows[0]["Student"].ToString() != txtStudentName.Text)
                {
                    DataTable dt = objDesg.GetStudentMaster(Session["CompId"].ToString().ToString());
                    dt = new DataView(dt, "Student='" + txtStudentName.Text + "'", "", DataViewRowState.CurrentRows).ToTable();
                    if (dt.Rows.Count > 0)
                    {
                        txtStudentName.Text = "";
                        DisplayMessage("Student Name Already Exists");
                        System.Web.UI.ScriptManager.GetCurrent(this).SetFocus(txtStudentName);
                        return;
                    }
                    DataTable dt1 = objDesg.GetStudentMasterInactive(Session["CompId"].ToString().ToString());
                    dt1 = new DataView(dt1, "Student='" + txtStudentName.Text + "'", "", DataViewRowState.CurrentRows).ToTable();
                    if (dt1.Rows.Count > 0)
                    {
                        txtStudentName.Text = "";
                        DisplayMessage("Student Name Already Exists - Go to Bin Tab");
                        System.Web.UI.ScriptManager.GetCurrent(this).SetFocus(txtStudentName);
                        return;
                    }
                }
            }
            txtStudentNameL.Focus();
        }

    }
    #endregion

}
