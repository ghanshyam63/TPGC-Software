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

public partial class ProjectManagement_ProjectTask : System.Web.UI.Page
{   
   
    Common cmn = new Common();
    Prj_ProjectMaster objProjctMaster = new Prj_ProjectMaster();
    SystemParameter ObjSysParam = new SystemParameter();
    Set_CustomerMaster objCustomermaster = new Set_CustomerMaster();
    Ems_ContactMaster objContactmaster = new Ems_ContactMaster();
    Arc_Directory_Master objDirectorymaster = new Arc_Directory_Master();
    Arc_FileTransaction objFiletransection = new Arc_FileTransaction();
    Prj_ProjectTask objProjectTask = new Prj_ProjectTask();
    EmployeeMaster objEmpMaster = new EmployeeMaster();
    string StrCompId = string.Empty;
    string StrUserId = string.Empty;
    
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

            gridbind();
            ddlOption.SelectedIndex = 2;
           
            pnlpopup.Visible = false;

        }
       AllPageCode();
      

    }


    public void AllPageCode()
    {
        Page.Title = ObjSysParam.GetSysTitle();
        StrUserId = Session["UserId"].ToString();
        StrCompId = Session["CompId"].ToString();
       // Session["AccordianId"] = "19";
        //Session["HeaderText"] = "HR";
        DataTable dtAllPageCode = cmn.GetAllPagePermission(StrUserId.ToString(), "19", "65");
        if (dtAllPageCode.Rows.Count != 0)
        {
            if (dtAllPageCode.Rows[0][0].ToString() == "SuperAdmin")
            {
                //btnCSave.Visible = true;
                foreach (GridViewRow Row in GvrProjecttask.Rows)
                {
                   // ((ImageButton)Row.FindControl("btnEdit")).Visible = true;
                    //((ImageButton)Row.FindControl("IbtnDelete")).Visible = true;
                }
               // imgBtnRestore.Visible = true;
               // ImgbtnSelectAll.Visible = true;
            }
            else
            {
                foreach (DataRow DtRow in dtAllPageCode.Rows)
                {
                    if (Convert.ToBoolean(DtRow["Op_Add"].ToString()))
                    {
                       // btnCSave.Visible = true;
                    }
                    foreach (GridViewRow Row in GvrProjecttask.Rows)
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
                       // imgBtnRestore.Visible = true;
                        //ImgbtnSelectAll.Visible = true;
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

        ddlprojectname.SelectedIndex = 0;
        txtEmplname.Text = "";
        txtassigndate.Text = "";
        txtassigntime.Text = "";
        txtempenddate.Text = "";
        txtempendtime.Text = "";
        txttlenddate.Text = "";
        txttlendtime.Text = "";
        txtsubject.Text = "";
        Editor1.Content = ""; 
        txtfilename.Text = ""; 
        System.Web.UI.ScriptManager.GetCurrent(this).SetFocus(txtEmplname);

    }
  
   public string FormatTime(object input)
    {
        return Convert.ToDateTime(input).ToString("HH:mm");
    }

    public void gridbind()
    {
        DataTable dtProjecttask = new DataTable();
        dtProjecttask = objProjectTask.GetAllRecord();
       
      
      
        if (dtProjecttask.Rows.Count > 0)
        {
            GvrProjecttask.DataSource = dtProjecttask;
            GvrProjecttask.DataBind();
            Session["dtFilter"] = dtProjecttask;
            Session["dtProjecttask"] = dtProjecttask;
        }
        else
        {
            DataTable Dtclear = new DataTable();
            GvrProjecttask.DataSource = Dtclear;
            GvrProjecttask.DataBind();
        
        
        }
        lblTotalRecords.Text = Resources.Attendance.Total_Records + ": " + dtProjecttask.Rows.Count + "";
        AllPageCode();
    
    
    }


    public void projectname()
    {
        DataTable dtprojectname = objProjctMaster.GetAllProjectMasteer();

        if (dtprojectname.Rows.Count > 0)
        {

            ddlprojectname.DataSource = dtprojectname;
            ddlprojectname.DataTextField = "Project_Name";
            ddlprojectname.DataValueField = "Project_Id";
            ddlprojectname.DataBind();

            ddlprojectname.Items.Add("--Select--");
            ddlprojectname.SelectedValue = "--Select--";
        }
        else
        {
            ddlprojectname.Items.Add("--Select--");
            ddlprojectname.SelectedValue = "--Select--";
        }
 
    }
   
    protected void btnRefresh_Click(object sender, ImageClickEventArgs e)
    {
        ddlFieldName.SelectedIndex = 1;
        ddlOption.SelectedIndex = 1;
        txtValue.Text = "";
        gridbind();
    }

    protected void GvrProjecttask_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        GvrProjecttask.PageIndex = e.NewPageIndex;
        DataTable dt = (DataTable)Session["dtFilter"];
        GvrProjecttask.DataSource = dt;
        GvrProjecttask.DataBind();
        AllPageCode();
    }

    protected void GvrProjecttask_Sorting(object sender, GridViewSortEventArgs e)
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
        GvrProjecttask.DataSource = dt;
        GvrProjecttask.DataBind();

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

            DataTable dtPrjecttask = (DataTable)Session["dtProjecttask"];

            DataView view = new DataView(dtPrjecttask, condition, "", DataViewRowState.CurrentRows);
            GvrProjecttask.DataSource = view.ToTable();
            Session["dtFilter"] = view.ToTable();
            lblTotalRecords.Text = Resources.Attendance.Total_Records + " : " + view.ToTable().Rows.Count + "";
            GvrProjecttask.DataBind();
            AllPageCode();
        }
    }
    
    protected void btnfiledwnload_Click(object sender, EventArgs e)
    {
        pnlpopup.Visible = true;
        pnl2.Visible = true;
        pnl3.Visible = true;
        pnlteamdetials.Visible = false;

        DataTable dtDirectry = objDirectorymaster.getDirectoryMaster(Session["CompId"].ToString(), "0");

        if (dtDirectry.Rows.Count > 0)
        {

            ddlDirctoryname.DataSource = dtDirectry;
            ddlDirctoryname.DataTextField = "Directory_Name";
            ddlDirctoryname.DataValueField = "Id";
            ddlDirctoryname.DataBind();

            ddlDirctoryname.Items.Add("--Select--");
            ddlDirctoryname.SelectedValue = "--Select--";
        }
        else
        {
            ddlDirctoryname.Items.Add("--Select--");
            ddlDirctoryname.SelectedValue = "--Select--";
        }




    }
    protected void ddlDirctoryname_SelectedIndexChanged(object sender, EventArgs e)
    {

        if (ddlDirctoryname.SelectedValue == ddlDirctoryname.SelectedValue)
        {


            DataTable dtFilename = objFiletransection.Get_FileTransaction_By_Documentid(Session["CompId"].ToString(), "0", ddlDirctoryname.SelectedValue.ToString());

            if (dtFilename.Rows.Count > 0)
            {

                ddlFilename.DataSource = dtFilename;
                ddlFilename.DataTextField = "File_Name";
                ddlFilename.DataValueField = "Trans_Id";
                ddlFilename.DataBind();

                ddlFilename.Items.Add("--Select--");
                ddlFilename.SelectedValue = "--Select--";
            }
            else
            {
                ddlFilename.Items.Add("--Select--");
                ddlFilename.SelectedValue = "--Select--";
            }

        }



    }
    
    protected void btnsubmitpopup_Click(object sender, EventArgs e)
    {
        pnlpopup.Visible = false;
        pnlteamdetials.Visible = true;
        txtfilename.Text = ddlFilename.SelectedItem.Text;

    }
    

    protected void btnnew_Click(object sender, EventArgs e)
    {
        projectname();
        pnllist.Visible = false;

        pnlteamdetials.Visible = true;
        PanelList.BackColor = System.Drawing.ColorTranslator.FromHtml(" #90bde9");
        pnlnew.BackColor = System.Drawing.ColorTranslator.FromHtml("#ccddee");

    }
    protected void btList_Click(object sender, EventArgs e)
    {
        gridbind();
        pnllist.Visible = true;
        pnlteamdetials.Visible = false;
        pnlpopup.Visible = false;
        PanelList.BackColor = System.Drawing.ColorTranslator.FromHtml(" #ccddee");
        pnlnew.BackColor = System.Drawing.ColorTranslator.FromHtml("#90bde9");

        gridbind();

    }
   
    protected void btnsave_Click(object sender, EventArgs e)
    {
        int a = 0;
        string time = string.Empty;
        a = objProjectTask.InsertProjectTask(ddlprojectname.SelectedValue.ToString(),HidCustId.Value.ToString(), txtassigndate.Text, txtassigntime.Text, txtempenddate.Text, txtempendtime.Text, txttlenddate.Text, txttlendtime.Text, txtsubject.Text, Editor1.Content, ddlFilename.SelectedValue, "Assigned", "", "", "", "", "", "", "", true.ToString(), Session["UserId"].ToString(), DateTime.Now.ToString(), Session["UserId"].ToString(), DateTime.Now.ToString());
        DisplayMessage("Record Save Successfully");
        ddlprojectname.SelectedIndex = 0;
        System.Web.UI.ScriptManager.GetCurrent(this).SetFocus(txtEmplname);
        Reset();

        
    }
    
    protected void btncencel_Click(object sender, EventArgs e)
    {
        pnllist.Visible = true;
        pnlpopup.Visible = false;
        pnlteamdetials.Visible = false;
    }
   
    protected void btnreset_Click(object sender, EventArgs e)
    {
        ddlprojectname.SelectedIndex = 0;
        System.Web.UI.ScriptManager.GetCurrent(this).SetFocus(txtEmplname);
        Reset();
    }
   
    protected void btncencelpnl_Click(object sender, EventArgs e)
    {
        pnllist.Visible = false;
        pnlpopup.Visible = false;
        pnlteamdetials.Visible = true;

    }
    
    protected void txtEmplname_TextChanged(object sender, EventArgs e)
    {
       string empid = string.Empty;
        if (txtEmplname.Text != "")
        {
            empid = txtEmplname.Text.Split('/')[txtEmplname.Text.Split('/').Length - 1];



          //  DataTable dtContactmaster = objContactmaster.GetAllCustomerName(Session["CompId"].ToString());
            DataTable dtEmpname = objEmpMaster.GetEmployeeMasterAllData(Session["CompId"].ToString());

           dtEmpname = new DataView(dtEmpname, "Emp_Id='" + empid + "'", "", DataViewRowState.CurrentRows).ToTable();
           HidCustId.Value = empid;
            if (dtEmpname.Rows.Count > 0)
            {
                empid = dtEmpname.Rows[0]["Emp_Id"].ToString();
                HidCustId.Value = empid;
            }
            else
            {
                DisplayMessage("Employee Not Exists");
                txtEmplname.Text = "";
                txtEmplname.Focus();
              //  HidCustId.Value = "";
                return;
            }
        }


    }
    [System.Web.Services.WebMethodAttribute(), System.Web.Script.Services.ScriptMethodAttribute()]

    public static string[] GetCompletionListEmployeeName(string prefixText, int count, string contextKey)
    {
        DataTable dt = Common.GetEmployee(prefixText, HttpContext.Current.Session["CompId"].ToString());

        string[] str = new string[dt.Rows.Count];
        for (int i = 0; i < dt.Rows.Count; i++)
        {
            str[i] = "" + dt.Rows[i][1].ToString() + "/(" + dt.Rows[i][2].ToString() + ")/" + dt.Rows[i][0].ToString() + "";
        }
        return str;
    }

    protected void Imgbtn_Click(object sender, ImageClickEventArgs e)
    {
        pnlpopup.Visible = false;
        pnl2.Visible = false;
        pnl3.Visible = false;
        pnlteamdetials.Visible = true;
    }
}
