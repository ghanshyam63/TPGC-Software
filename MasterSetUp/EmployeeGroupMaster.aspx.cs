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

public partial class Master_EmployeeGroupMaster : BasePage
{
    #region Defined Class Object
    Common cmn = new Common();
    SystemParameter objSys = new SystemParameter();
    Set_EmployeeGroup_Master objEmpGroup = new Set_EmployeeGroup_Master();
    EmployeeMaster objEmp = new EmployeeMaster();
    Set_Group_Employee objGroupEmp = new Set_Group_Employee();
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
            ddlFieldName.SelectedIndex = 0  ;
            txtGroupName.Focus();
        }


    }


    protected void gvEmp_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        gvEmp.PageIndex = e.NewPageIndex;
        gvEmp.DataSource = (DataTable)Session["dtEmp"];
        gvEmp.DataBind();

       
        string temp = string.Empty;
        bool isselcted;

        for (int i = 0; i < gvEmp.Rows.Count; i++)
        {
            Label lblconid = (Label)gvEmp.Rows[i].FindControl("lblEmpId");
            string[] split = lblEmp.Text.Split(',');

            for (int j = 0; j < lblEmp.Text.Split(',').Length; j++)
            {
                if (lblEmp.Text.Split(',')[j] != "")
                {
                    if (lblconid.Text.Trim().ToString() == lblEmp.Text.Split(',')[j].Trim().ToString())
                    {
                        ((CheckBox)gvEmp.Rows[i].FindControl("chkgvSelect")).Checked = true;
                    }
                }
            }
        }
        AllPageCode();
    }


       

    public void AllPageCode()
    {

        Page.Title = objSys.GetSysTitle();
        Session["AccordianId"] = "8";
        Session["HeaderText"] = "Master Setup";
        DataTable dtAllPageCode = cmn.GetAllPagePermission(Session["UserId"].ToString(), "8", "43");
        if (dtAllPageCode.Rows.Count != 0)
        {
            if (dtAllPageCode.Rows[0][0].ToString() == "SuperAdmin")
            {
                btnCSave.Visible = true;
                foreach (GridViewRow Row in GvGroup.Rows)
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
                    foreach (GridViewRow Row in GvGroup.Rows)
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
        dt = objEmpGroup.GetEmployeeGroup_MasterInactive(Session["CompId"].ToString().ToString());
        dt = new DataView(dt, "Brand_Id='" + Session["BrandId"].ToString() + "'  and Location_Id='" + Session["LocId"].ToString() + "'", "", DataViewRowState.CurrentRows).ToTable();

        
        GvGroupBin.DataSource = dt;
        GvGroupBin.DataBind();
        Session["dtBinGroup"] = dt;
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
        DataTable dtBrand = objEmpGroup.GetEmployeeGroup_Master(Session["CompId"].ToString().ToString());

        dtBrand = new DataView(dtBrand, "Brand_Id='" + Session["BrandId"].ToString() + "'  and Location_Id='" + Session["LocId"].ToString() + "'", "", DataViewRowState.CurrentRows).ToTable();


        lblTotalRecords.Text = Resources.Attendance.Total_Records + ": " + dtBrand.Rows.Count + "";
        Session["dtGroup"] = dtBrand;
        Session["dtFilter"] = dtBrand;
        if (dtBrand != null && dtBrand.Rows.Count > 0)
        {
            GvGroup.DataSource = dtBrand;
            GvGroup.DataBind();
            AllPageCode();
        }
        else
        {
            GvGroup.DataSource = null;
            GvGroup.DataBind();
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

        txtGroupName.Text = "";
        txtGroupNameL.Text = "";
        editid.Value = "";
        btnNew.Text = Resources.Attendance.New;
        ddlFieldName.SelectedIndex = 0;
        ddlOption.SelectedIndex = 2;
        txtValue.Text = "";
        txtValueBin.Text = "";
        lblSelectedRecord.Text = "";
        lblEmp.Text = "";

        System.Web.UI.ScriptManager.GetCurrent(this).SetFocus(txtValue);
        txtGroupNameL.Text = "";
        rbtnAllEmp.Checked = false;
        rbtnEmpInGroup.Checked = false;
        rbtnEmpNotInGroup.Checked = false;
        Session["dtEmp"] = null;
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
        System.Web.UI.ScriptManager.GetCurrent(this).SetFocus(txtGroupName);
    }
    protected void btnCSave_Click(object sender, EventArgs e)
    {

        if (txtGroupName.Text == "" || txtGroupName.Text == null)
        {
            DisplayMessage("Enter Group Name");
            System.Web.UI.ScriptManager.GetCurrent(this).SetFocus(txtGroupName);
            return;
        }
        int b = 0;
        if (editid.Value != "")
        {

            DataTable dtCate = objEmpGroup.GetEmployeeGroup_Master(Session["CompId"].ToString().ToString());
            dtCate = new DataView(dtCate, "Group_Name='" + txtGroupName.Text + "'", "", DataViewRowState.CurrentRows).ToTable();
            if (dtCate.Rows.Count > 0)
            {
                if (dtCate.Rows[0]["Group_ID"].ToString() != editid.Value)
                {
                    txtGroupName.Text = "";
                    DisplayMessage("Group Already Exists");
                    System.Web.UI.ScriptManager.GetCurrent(this).SetFocus(txtGroupName);
                    return;
                }
            }


            b = objEmpGroup.UpdateEmployeeGroup_Master(Session["CompId"].ToString().ToString(), Session["BrandId"].ToString(), Session["LocId"].ToString(), editid.Value, "0", Session["RoleId"].ToString(), txtGroupName.Text, txtGroupNameL.Text.Trim().ToString(), "", "", "", "", "", true.ToString(), DateTime.Now.ToString(), "True", Session["UserId"].ToString(), DateTime.Now.ToString());
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
            DataTable dtPro = objEmpGroup.GetEmployeeGroup_Master(Session["CompId"].ToString().ToString());
            dtPro = new DataView(dtPro, "Group_Name='" + txtGroupName.Text + "'", "", DataViewRowState.CurrentRows).ToTable();

            if (dtPro.Rows.Count > 0)
            {
                txtGroupName.Text = "";
                DisplayMessage("Group Name Already Exists");
                System.Web.UI.ScriptManager.GetCurrent(this).SetFocus(txtGroupName);
                return;
            }

            b = objEmpGroup.InsertEmployeeGroup_Master(Session["CompId"].ToString().ToString(), Session["BrandId"].ToString(), Session["LocId"].ToString(), txtGroupName.Text, txtGroupNameL.Text.Trim(), "0", Session["RoleId"].ToString(), "", "", "", "", "", true.ToString(), DateTime.Now.ToString(), true.ToString(), Session["UserId"].ToString(), DateTime.Now.ToString(), Session["UserId"].ToString(), DateTime.Now.ToString());
            if (b!=0)
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
        int b =0;
        String CompanyId = Session["CompId"].ToString();
        String UserId = Session["UserId"].ToString();
        b = objEmpGroup.DeleteEmployeeGroup_Master(Session["CompId"].ToString().ToString(),editid.Value, true.ToString(), Session["UserId"].ToString(), DateTime.Now.ToString());
        if (b!=0)
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
    protected void GvGroupBin_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        GvGroupBin.PageIndex = e.NewPageIndex;

        DataTable dt = (DataTable)Session["dtBinFilter"];
        GvGroupBin.DataSource = dt;
        GvGroupBin.DataBind();
        AllPageCode();
        string temp = string.Empty;


        for (int i = 0; i < GvGroupBin.Rows.Count; i++)
        {
            Label lblconid = (Label)GvGroupBin.Rows[i].FindControl("lblGroupId");
            string[] split = lblSelectedRecord.Text.Split(',');

            for (int j = 0; j < lblSelectedRecord.Text.Split(',').Length; j++)
            {
                if (lblSelectedRecord.Text.Split(',')[j] != "")
                {
                    if (lblconid.Text.Trim().ToString() == lblSelectedRecord.Text.Split(',')[j].Trim().ToString())
                    {
                        ((CheckBox)GvGroupBin.Rows[i].FindControl("chkgvSelect")).Checked = true;
                    }
                }
            }
        }

    }
    protected void GvGroupBin_OnSorting(object sender, GridViewSortEventArgs e)
    {
        HDFSortbin.Value = HDFSortbin.Value == "ASC" ? "DESC" : "ASC";
        DataTable dt = new DataTable();
        dt = objEmpGroup.GetEmployeeGroup_MasterInactive(Session["CompId"].ToString().ToString());
        DataView dv = new DataView(dt);
        string Query = "" + e.SortExpression + " " + HDFSortbin.Value + "";
        dv.Sort = Query;
        dt = dv.ToTable();
        Session["dtBinFilter"] = dt;
        GvGroupBin.DataSource = dt;
        GvGroupBin.DataBind();
        AllPageCode();
        lblSelectedRecord.Text = "";

    }
    protected void btnEdit_Command(object sender, CommandEventArgs e)
    {
        editid.Value = e.CommandArgument.ToString();

        DataTable dtTax = objEmpGroup.GetEmployeeGroup_MasterById(Session["CompId"].ToString().ToString(),editid.Value);

        btnNew.Text = Resources.Attendance.Edit;

        txtGroupNameG.Text = dtTax.Rows[0]["Group_Name"].ToString();
        txtGroupNameLG.Text = dtTax.Rows[0]["Group_Name_L"].ToString();
        pnl1.Visible = true;
        pnl2.Visible = true;
        rbtnAllEmp.Checked = true;
        EmpGroup_CheckedChanged(null,null);


        //System.Web.UI.ScriptManager.GetCurrent(this).SetFocus(txtGroupName);
    }
    protected void IbtnDelete_Command1(object sender, CommandEventArgs e)
    {
        int b = 0;
        b = objGroupEmp.DeleteGroup_EmployeeByEmpId(e.CommandArgument.ToString(), editid.Value, false.ToString(), Session["UserId"].ToString(), DateTime.Now.ToString());

        if(b!=0)
        {
            DisplayMessage("Record Deleted");
            rbtnEmpInGroup.Checked = true;
            EmpGroup_CheckedChanged(null,null);
            lblEmp.Text = "";
        }

      }
    


    protected void GvGroup_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        GvGroup.PageIndex = e.NewPageIndex;
        DataTable dt = (DataTable)Session["dtFilter"];
        GvGroup.DataSource = dt;
        GvGroup.DataBind();
        AllPageCode();

    }

    protected void chkgvSelect_CheckedChanged1(object sender, EventArgs e)
    {
        string empidlist = string.Empty;
        string temp = string.Empty;
        int index = ((GridViewRow)((CheckBox)sender).Parent.Parent).RowIndex;
        Label lb = (Label)gvEmp.Rows[index].FindControl("lblEmpId");
        if (((CheckBox)gvEmp.Rows[index].FindControl("chkgvSelect")).Checked)
        {
            empidlist += lb.Text.Trim().ToString() + ",";
            
            if(!lblEmp.Text.Split(',').Contains(lb.Text))
            {
            lblEmp.Text += empidlist;
            }
        }

        else
        {

            empidlist += lb.Text.ToString().Trim();
            lblEmp.Text += empidlist;
            string[] split = lblEmp.Text.Split(',');
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
            lblEmp.Text = temp;
        }
    }

    protected void chkgvSelectAll_CheckedChanged1(object sender, EventArgs e)
    {
        CheckBox chkSelAll = ((CheckBox)gvEmp.HeaderRow.FindControl("chkgvSelectAll"));
        for (int i = 0; i < gvEmp.Rows.Count; i++)
        {
            ((CheckBox)gvEmp.Rows[i].FindControl("chkgvSelect")).Checked = chkSelAll.Checked;
            if (chkSelAll.Checked)
            {
                if (!lblEmp.Text.Split(',').Contains(((Label)(gvEmp.Rows[i].FindControl("lblEmpId"))).Text.Trim().ToString()))
                {
                    lblEmp.Text += ((Label)(gvEmp.Rows[i].FindControl("lblEmpId"))).Text.Trim().ToString() + ",";
                }
            }
            else
            {
                string temp = string.Empty;
                string[] split = lblEmp.Text.Split(',');
                foreach (string item in split)
                {
                    if (item != ((Label)(gvEmp.Rows[i].FindControl("lblEmpId"))).Text.Trim().ToString())
                    {
                        if (item != "")
                        {
                            temp += item + ",";
                        }
                    }
                }
                lblEmp.Text = temp;
            }
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
            DataTable dtCurrency = (DataTable)Session["dtGroup"];
            DataView view = new DataView(dtCurrency, condition, "", DataViewRowState.CurrentRows);
            GvGroup.DataSource = view.ToTable();
            Session["dtFilter"] = view.ToTable();
            lblTotalRecords.Text = Resources.Attendance.Total_Records + " : " + view.ToTable().Rows.Count + "";
            GvGroup.DataBind();
            AllPageCode();
        }
    }
    protected void GvGroup_Sorting(object sender, GridViewSortEventArgs e)
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
        GvGroup.DataSource = dt;
        GvGroup.DataBind();
        AllPageCode();

    }
    protected void IbtnDelete_Command(object sender, CommandEventArgs e)
    {
        editid.Value = e.CommandArgument.ToString();
        int b = 0;
        String CompanyId = Session["CompId"].ToString().ToString();
        String UserId =Session["UserId"].ToString().ToString();
        b = objEmpGroup.DeleteEmployeeGroup_Master(Session["CompId"].ToString().ToString(),editid.Value, "false", Session["UserId"].ToString().ToString(), DateTime.Now.ToString());
        if (b!=0)
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
    public static string[] GetCompletionListGroup(string prefixText, int count, string contextKey)
    {
        Set_EmployeeGroup_Master objGroup = new Set_EmployeeGroup_Master();
        DataTable dt = new DataView(objGroup.GetEmployeeGroup_Master(HttpContext.Current.Session["CompId"].ToString()), "Group_Name like '" + prefixText.ToString() + "%'", "", DataViewRowState.CurrentRows).ToTable();
        string[] txt = new string[dt.Rows.Count];

        for (int i = 0; i < dt.Rows.Count; i++)
        {
            txt[i] = dt.Rows[i]["Group_Name"].ToString();
        }
        return txt;
    }

    [System.Web.Services.WebMethodAttribute(), System.Web.Script.Services.ScriptMethodAttribute()]
    public static string[] GetCompletionListGroupG(string prefixText, int count, string contextKey)
    {
        Set_EmployeeGroup_Master objGroup = new Set_EmployeeGroup_Master();
        DataTable dt = new DataView(objGroup.GetEmployeeGroup_Master(HttpContext.Current.Session["CompId"].ToString()), "Group_Name like '" + prefixText.ToString() + "%'", "", DataViewRowState.CurrentRows).ToTable();
        string[] txt = new string[dt.Rows.Count];

        for (int i = 0; i < dt.Rows.Count; i++)
        {
            txt[i] = dt.Rows[i]["Group_Name"].ToString();
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

            DataTable dtCust = (DataTable)Session["dtBinGroup"];
            DataView view = new DataView(dtCust, condition, "", DataViewRowState.CurrentRows);
            Session["dtBinFilter"] = view.ToTable();
            lblTotalRecordsBin.Text = Resources.Attendance.Total_Records + " : " + view.ToTable().Rows.Count.ToString() + "";
            GvGroupBin.DataSource = view.ToTable();
            GvGroupBin.DataBind();

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
        DataTable dt = objEmpGroup.GetEmployeeGroup_MasterInactive(Session["CompId"].ToString().ToString());

        if (GvGroupBin.Rows.Count != 0)
        {
            if (lblSelectedRecord.Text != "")
            {
                for (int j = 0; j < lblSelectedRecord.Text.Split(',').Length; j++)
                {
                    if (lblSelectedRecord.Text.Split(',')[j] != "")
                    {

                        Msg = objEmpGroup.DeleteEmployeeGroup_Master(Session["CompId"].ToString().ToString(), lblSelectedRecord.Text.Split(',')[j].Trim().ToString(), true.ToString(), Session["UserId"].ToString(), DateTime.Now.ToString());
                    }
                }
            }

            if (Msg!=0)
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
                foreach (GridViewRow Gvr in GvGroupBin.Rows)
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
        DataTable dtGroup = (DataTable)Session["dtbinFilter"];

        if (ViewState["Select"] == null)
        {
            ViewState["Select"] = 1;
            foreach (DataRow dr in dtGroup.Rows)
            {
                if (!lblSelectedRecord.Text.Split(',').Contains(dr["Group_ID"]))
                {
                    lblSelectedRecord.Text += dr["Group_ID"] + ",";
                }
            }
            for (int i = 0; i < GvGroupBin.Rows.Count; i++)
            {
                string[] split = lblSelectedRecord.Text.Split(',');
                Label lblconid = (Label)GvGroupBin.Rows[i].FindControl("lblGroupId");
                for (int j = 0; j < lblSelectedRecord.Text.Split(',').Length; j++)
                {
                    if (lblSelectedRecord.Text.Split(',')[j] != "")
                    {
                        if (lblconid.Text.Trim().ToString() == lblSelectedRecord.Text.Split(',')[j].Trim().ToString())
                        {
                            ((CheckBox)GvGroupBin.Rows[i].FindControl("chkgvSelect")).Checked = true;
                        }
                    }
                }
            }
        }
        else
        {
            lblSelectedRecord.Text = "";
            DataTable dtGroup1 = (DataTable)Session["dtBinFilter"];
            GvGroupBin.DataSource = dtGroup1;
            GvGroupBin.DataBind();
            ViewState["Select"] = null;
        }



    }
    protected void chkgvSelectAll_CheckedChanged(object sender, EventArgs e)
    {
        CheckBox chkSelAll = ((CheckBox)GvGroupBin.HeaderRow.FindControl("chkgvSelectAll"));
        for (int i = 0; i < GvGroupBin.Rows.Count; i++)
        {
            ((CheckBox)GvGroupBin.Rows[i].FindControl("chkgvSelect")).Checked = chkSelAll.Checked;
            if (chkSelAll.Checked)
            {
                if (!lblSelectedRecord.Text.Split(',').Contains(((Label)(GvGroupBin.Rows[i].FindControl("lblGroupId"))).Text.Trim().ToString()))
                {
                    lblSelectedRecord.Text += ((Label)(GvGroupBin.Rows[i].FindControl("lblGroupId"))).Text.Trim().ToString() + ",";
                }
            }
            else
            {
                string temp = string.Empty;
                string[] split = lblSelectedRecord.Text.Split(',');
                foreach (string item in split)
                {
                    if (item != ((Label)(GvGroupBin.Rows[i].FindControl("lblGroupId"))).Text.Trim().ToString())
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
        Label lb = (Label)GvGroupBin.Rows[index].FindControl("lblGroupId");
        if (((CheckBox)GvGroupBin.Rows[index].FindControl("chkgvSelect")).Checked)
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
    protected void txtGroupName_TextChanged(object sender, EventArgs e)
    {
        if (editid.Value == "")
        {
            DataTable dt = objEmpGroup.GetEmployeeGroup_MasterByGroupName(Session["CompId"].ToString().ToString(),txtGroupName.Text.Trim());
            if (dt.Rows.Count > 0)
            {
                txtGroupName.Text = "";
                DisplayMessage("Group Name Already Exists");
                System.Web.UI.ScriptManager.GetCurrent(this).SetFocus(txtGroupName);
                return;
            }
            DataTable dt1 = objEmpGroup.GetEmployeeGroup_MasterInactive(Session["CompId"].ToString().ToString());
            dt1 = new DataView(dt1, "Group_Name='" + txtGroupName.Text + "'", "", DataViewRowState.CurrentRows).ToTable();
            if (dt1.Rows.Count > 0)
            {
                txtGroupName.Text = "";
                DisplayMessage("Group Name Already Exists - Go to Bin Tab");
                System.Web.UI.ScriptManager.GetCurrent(this).SetFocus(txtGroupName);
                return;
            }
            txtGroupNameL.Focus();
        }
        else
        {
            DataTable dtTemp = objEmpGroup.GetEmployeeGroup_MasterById(Session["CompId"].ToString().ToString(), editid.Value);
            if (dtTemp.Rows.Count > 0)
            {
                if (dtTemp.Rows[0]["Group_Name"].ToString() != txtGroupName.Text)
                {
                    DataTable dt = objEmpGroup.GetEmployeeGroup_Master(Session["CompId"].ToString().ToString());
                    dt = new DataView(dt, "Group_Name='" + txtGroupName.Text + "'", "", DataViewRowState.CurrentRows).ToTable();
                    if (dt.Rows.Count > 0)
                    {
                        txtGroupName.Text = "";
                        DisplayMessage("Group Name Already Exists");
                        System.Web.UI.ScriptManager.GetCurrent(this).SetFocus(txtGroupName);
                        return;
                    }
                    DataTable dt1 = objEmpGroup.GetEmployeeGroup_MasterInactive(Session["CompId"].ToString().ToString());
                    dt1 = new DataView(dt1, "Group_Name='" + txtGroupName.Text + "'", "", DataViewRowState.CurrentRows).ToTable();
                    if (dt1.Rows.Count > 0)
                    {
                        txtGroupName.Text = "";
                        DisplayMessage("Group Name Already Exists - Go to Bin Tab");
                        System.Web.UI.ScriptManager.GetCurrent(this).SetFocus(txtGroupName);
                        return;
                    }
                }
            }
            txtGroupNameL.Focus();
        }
       
    }

    protected void btnbinbind_Click(object sender, ImageClickEventArgs e)
    {

        if (ddlbinOption.SelectedIndex != 0)
        {
            string condition = string.Empty;
            if (ddlbinOption.SelectedIndex == 1)
            {
                condition = "convert(" + ddlbinFieldName.SelectedValue + ",System.String)='" + txtbinVal.Text.Trim() + "'";
            }
            else if (ddlbinOption.SelectedIndex == 2)
            {
                condition = "convert(" + ddlbinFieldName.SelectedValue + ",System.String) like '%" + txtbinVal.Text.Trim() + "%'";
          
               
            }else
            { condition = "convert(" + ddlbinFieldName.SelectedValue + ",System.String) Like '" + txtbinVal.Text.Trim() + "%'";
            
                 }

            DataTable dt=(DataTable)Session["dtEmp"];

            DataView view = new DataView(dt, condition, "", DataViewRowState.CurrentRows);

            lblbinTotalRecord.Text = Resources.Attendance.Total_Records + " : " + (view.ToTable()).Rows.Count.ToString() + "";

           

            dt = view.ToTable();

            if (dt.Rows.Count > 0)
            {
                gvEmp.DataSource = dt;
                gvEmp.DataBind();

                DataTable dtEmpInGroup = objGroupEmp.GetGroup_Employee(editid.Value, Session["CompId"].ToString());
                if (dtEmpInGroup.Rows.Count > 0)
                {
                    string EmpIds = string.Empty;

                    for (int i = 0; i < dtEmpInGroup.Rows.Count; i++)
                    {
                        EmpIds += dtEmpInGroup.Rows[i]["Emp_Id"].ToString() + ",";

                    }
                    foreach (GridViewRow gvr in gvEmp.Rows)
                    {
                        Label lb = (Label)gvr.FindControl("lblEmpId");
                        if (EmpIds.Split(',').Contains(lb.Text))
                        {
                            if (!lblEmp.Text.Split(',').Contains(lb.Text))
                            {
                                lblEmp.Text += lb.Text + ",";
                            }
                            CheckBox chk = (CheckBox)gvr.FindControl("chkgvSelect");
                            chk.Checked = true;
                        }

                    }
                }
            }
        }


    }

    protected void btnbinRefresh_Click(object sender, ImageClickEventArgs e)
    {
        

        txtbinVal.Text = "";
        ddlbinFieldName.SelectedIndex = 1;
        ddlbinOption.SelectedIndex = 2;


       
        EmpGroup_CheckedChanged(null,null);
    }

    


    protected void ImgbtnSelectAll_Click1(object sender, ImageClickEventArgs e)
    {
        DataTable dtGroup = (DataTable)Session["dtEmp"];

        if (ViewState["Select"] == null)
        {
            ViewState["Select"] = 1;
            foreach (DataRow dr in dtGroup.Rows)
            {
                if (!lblEmp.Text.Split(',').Contains(dr["Emp_Id"]))
                {
                    lblEmp.Text += dr["Emp_Id"] + ",";
                }
            }
            for (int i = 0; i < gvEmp.Rows.Count; i++)
            {
                string[] split = lblEmp.Text.Split(',');
                Label lblconid = (Label)gvEmp.Rows[i].FindControl("lblEmpId");
                for (int j = 0; j < lblEmp.Text.Split(',').Length; j++)
                {
                    if (lblEmp.Text.Split(',')[j] != "")
                    {
                        if (lblconid.Text.Trim().ToString() == lblEmp.Text.Split(',')[j].Trim().ToString())
                        {
                            ((CheckBox)gvEmp.Rows[i].FindControl("chkgvSelect")).Checked = true;
                        }
                    }
                }
            }
        }
        else
        {
            lblEmp.Text = "";
            DataTable dtGroup1 = (DataTable)Session["dtEmp"];
            gvEmp.DataSource = dtGroup1;
            gvEmp.DataBind();
            ViewState["Select"] = null;
        }



    }
    protected void btnSaveGroup_Click(object sender, EventArgs e)
    {

        int b = 0;

       

        DataTable dtCate = objEmpGroup.GetEmployeeGroup_Master(Session["CompId"].ToString().ToString());
        dtCate = new DataView(dtCate, "Group_Name='" + txtGroupNameG.Text + "'", "", DataViewRowState.CurrentRows).ToTable();
        if (dtCate.Rows.Count > 0)
        {
            if (dtCate.Rows[0]["Group_ID"].ToString() != editid.Value)
            {
                txtGroupNameG.Text = "";
                DisplayMessage("Group Already Exists");
                System.Web.UI.ScriptManager.GetCurrent(this).SetFocus(txtGroupNameG);
                return;
            }
        }


        b = objEmpGroup.UpdateEmployeeGroup_Master(Session["CompId"].ToString().ToString(), Session["BrandId"].ToString(), Session["LocId"].ToString(), editid.Value, "0", Session["RoleId"].ToString(), txtGroupNameG.Text, txtGroupNameLG.Text.Trim().ToString(), "", "", "", "", "", true.ToString(), DateTime.Now.ToString(), "True", Session["UserId"].ToString(), DateTime.Now.ToString());
        
        if (b != 0)
        {

            objGroupEmp.DeleteGroup_Employee(Session["CompId"].ToString(),editid.Value,false.ToString(),Session["UserId"].ToString(),DateTime.Now.ToString());

            foreach (string str in lblEmp.Text.Split(','))
            {
                if(str!="")
                {
                                b = objGroupEmp.InsertGroup_Employee(Session["CompId"].ToString(),str,editid.Value,"", "", "", "", "", true.ToString(), DateTime.Now.ToString(), true.ToString(), Session["UserId"].ToString(), DateTime.Now.ToString(), Session["UserId"].ToString(), DateTime.Now.ToString());
                }
            }





          
            DisplayMessage("Record Updated");

        }
        else
        {
            DisplayMessage("Record  Not Updated");
        }

     
    }
    protected void btnCancelGroup_Click(object sender, EventArgs e)
    {



        pnl1.Visible = false;
        pnl2.Visible = false;
        txtGroupNameG.Text = "";
        txtGroupNameLG.Text = "";
        rbtnAllEmp.Checked = false;
        rbtnEmpInGroup.Checked = false;
        rbtnEmpNotInGroup.Checked=false;
        Reset();
        
        FillGrid();
    }
    protected void btnClosePanel_Click(object sender, EventArgs e)
    {
        pnl1.Visible = false;
        pnl2.Visible = false;
        txtGroupNameG.Text = "";
        txtGroupNameLG.Text = "";
    }


    protected void EmpGroup_CheckedChanged(object sender, EventArgs e)
    {
        lblEmp.Text = "";
        if(rbtnAllEmp.Checked)
        {
        DataTable dtEmp = objEmp.GetEmployeeMaster(Session["CompId"].ToString());

        dtEmp = new DataView(dtEmp, "Brand_Id='" + Session["BrandId"].ToString() + "'  and Location_Id='" + Session["LocId"].ToString() + "'", "", DataViewRowState.CurrentRows).ToTable();

        if (dtEmp.Rows.Count > 0)
        {
            Session["dtEmp"] = dtEmp;
            gvEmp.DataSource = dtEmp;
            gvEmp.DataBind();
            lblbinTotalRecord.Text = Resources.Attendance.Total_Records + " : " + dtEmp.Rows.Count.ToString() + "";

            DataTable dtEmpInGroup = objGroupEmp.GetGroup_Employee(editid.Value, Session["CompId"].ToString());
            if (dtEmpInGroup.Rows.Count > 0)
            {
            string EmpIds = string.Empty;

            for (int i = 0; i < dtEmpInGroup.Rows.Count; i++)
            {
                EmpIds += dtEmpInGroup.Rows[i]["Emp_Id"].ToString() + ",";

            }
            lblEmp.Text += EmpIds;
            foreach (GridViewRow gvr in gvEmp.Rows)
            {
                Label lb = (Label)gvr.FindControl("lblEmpId");
                if(EmpIds.Split(',').Contains(lb.Text))
                {
                    if (!lblEmp.Text.Split(',').Contains(lb.Text))
                    {
                    lblEmp.Text += lb.Text+","; 
                    }
                    CheckBox chk = (CheckBox)gvr.FindControl("chkgvSelect");
                    chk.Checked=true;
                }

            }
            }



            gvEmp.Columns[0].Visible = true;
            gvEmp.Columns[1].Visible=false;
            imgbtnSelectAll1.Visible = true;
        }
        }
        else if (rbtnEmpInGroup.Checked)
        {

            DataTable dtEmpInGroup = objGroupEmp.GetGroup_Employee(editid.Value,Session["CompId"].ToString());

            string EmpIds = string.Empty;

            for (int i = 0; i < dtEmpInGroup.Rows.Count;i++)
            {
                EmpIds+=dtEmpInGroup.Rows[i]["Emp_Id"].ToString()+",";

            }
            DataTable dtEmp = objEmp.GetEmployeeMaster(Session["CompId"].ToString());

            dtEmp = new DataView(dtEmp, "Brand_Id='" + Session["BrandId"].ToString() + "'  and Location_Id='" + Session["LocId"].ToString() + "'", "", DataViewRowState.CurrentRows).ToTable();

            if(EmpIds!="")
            {
            dtEmpInGroup = new DataView(dtEmp,"Emp_Id in("+EmpIds.Substring(0,EmpIds.Length-1)+")","",DataViewRowState.CurrentRows).ToTable();
            }
            
           
                Session["dtEmp"] = dtEmpInGroup;
                gvEmp.DataSource = dtEmpInGroup;
                gvEmp.DataBind();
                lblbinTotalRecord.Text = Resources.Attendance.Total_Records + " : " + dtEmpInGroup.Rows.Count.ToString() + "";

                gvEmp.Columns[1].Visible = true;
                gvEmp.Columns[0].Visible = false;
                imgbtnSelectAll1.Visible = false;
        }
        else if(rbtnEmpNotInGroup.Checked)
        {


            DataTable dtEmpInGroup = objGroupEmp.GetGroup_Employee(editid.Value, Session["CompId"].ToString());

            string EmpIds = string.Empty;

            for (int i = 0; i < dtEmpInGroup.Rows.Count; i++)
            {
                EmpIds += dtEmpInGroup.Rows[i]["Emp_Id"].ToString() + ",";

            }
            DataTable dtEmp = objEmp.GetEmployeeMaster(Session["CompId"].ToString());

            dtEmp = new DataView(dtEmp, "Brand_Id='" + Session["BrandId"].ToString() + "'  and Location_Id='" + Session["LocId"].ToString() + "'", "", DataViewRowState.CurrentRows).ToTable();
            if (EmpIds != "")
            {
                dtEmpInGroup = new DataView(dtEmp, "Emp_Id not in(" + EmpIds.Substring(0, EmpIds.Length - 1) + ")", "", DataViewRowState.CurrentRows).ToTable();
            }
            else
            {
                dtEmpInGroup = dtEmp;
            }
           
                Session["dtEmp"] = dtEmpInGroup;
                gvEmp.DataSource = dtEmpInGroup;
                gvEmp.DataBind();
                lblbinTotalRecord.Text = Resources.Attendance.Total_Records + " : " + dtEmpInGroup.Rows.Count.ToString() + "";

                gvEmp.Columns[0].Visible = true;
                gvEmp.Columns[1].Visible = false;
                imgbtnSelectAll1.Visible = true;
        }


    }

    
    #endregion



}
