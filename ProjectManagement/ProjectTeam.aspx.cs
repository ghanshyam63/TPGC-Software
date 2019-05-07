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


public partial class ProjectManagement_ProjectTeam : System.Web.UI.Page
{
    Common cmn = new Common();
    Prj_ProjectMaster objProjctMaster = new Prj_ProjectMaster();
    SystemParameter ObjSysParam = new SystemParameter();
    Set_CustomerMaster objCustomermaster = new Set_CustomerMaster();
    Ems_ContactMaster objContactmaster = new Ems_ContactMaster();
    Arc_Directory_Master objDirectorymaster = new Arc_Directory_Master();
    Arc_FileTransaction objFiletransection = new Arc_FileTransaction();
    EmployeeMaster objEmpmaster = new EmployeeMaster();
    Prj_ProjectTeam objProjectteam = new Prj_ProjectTeam();
    Prj_ProjectTask objProjecttask = new Prj_ProjectTask();

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

            getprojectteam();
            ddlOption.SelectedIndex = 1;
            pnlgrid.Visible = false;

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
                foreach (GridViewRow Row in GvrProjectteam.Rows)
                {
                   ((ImageButton)Row.FindControl("btnEdit")).Visible = true;
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
                    foreach (GridViewRow Row in GvrProjectteam.Rows)
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

    public void Fillddlprojectname()
    {
        DataTable dt = objProjctMaster.GetAllProjectMasteer();
        if (dt.Rows.Count > 0)
        {
            ddlprojectname.DataSource = dt;
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

        DataTable dtempname = objEmpmaster.GetEmployeeMaster(Session["CompId"].ToString());
        if (dtempname.Rows.Count > 0)
        {
            ddlempname.DataSource = dtempname;
            ddlempname.DataTextField = "Emp_Name";
            ddlempname.DataValueField = "Emp_Id";
            ddlempname.DataBind();

            ddlempname.Items.Add("--Select--");
            ddlempname.SelectedValue = "--Select--";
        }
        else
        {
            ddlempname.Items.Add("--Select--");
            ddlempname.SelectedValue = "--Select--";
        }
        pnlgrid.Visible = true;

    }
   
    protected void btnnew_Click(object sender, EventArgs e)
    {
        PanelList.BackColor = System.Drawing.ColorTranslator.FromHtml(" #90bde9");
        pnlnew.BackColor = System.Drawing.ColorTranslator.FromHtml("#ccddee");
        
        pnllist.Visible = false;
        pnlgrid.Visible = false;
        pnlteamdetials.Visible = true;
        Fillddlprojectname();
    }


    public void getprojectteam()
    {
        DataTable dtRecord = new DataTable();
        dtRecord.Columns.Add("Project_Id");
        dtRecord.Columns.Add("Project_Name");
        dtRecord.Columns.Add("counters");
       
        int counters = 0;
        
      
        DataTable dtProjectrecord = new DataTable();
        dtProjectrecord.Columns.Add("counters");
       
       
        
       
        dtProjectrecord=objProjectteam.GetAllProjectTeam();
       
        if (dtProjectrecord.Rows.Count > 0)
        {
            
           DataTable dt=new DataTable();
                dtProjectrecord = new DataView(dtProjectrecord, "", "", DataViewRowState.CurrentRows).ToTable(true,"Project_Id");
                    for (int i = 0; i < dtProjectrecord.Rows.Count; i++)
                {
                    DataRow dr = dtRecord.NewRow();
                    dt = objProjectteam.GetAllProjectTeam();
                    counters = 0;
                    dt = new DataView(dt, "Project_Id=" + dtProjectrecord.Rows[i]["Project_Id"].ToString() + "", "", DataViewRowState.CurrentRows).ToTable();
                    for (int j = 0; j < dt.Rows.Count; j++)
                    {
                        counters += 1;
                    }
                    
                        dr[0] = dt.Rows[0]["Project_Id"].ToString();
                        dr[1] = dt.Rows[0]["Project_Name"].ToString();
                        dr[2] = counters.ToString();
                       
                       
                        dtRecord.Rows.Add(dr);
                    }
        }
        else
        { 
        
        }



        if (dtRecord.Rows.Count > 0)
        {

            GvrProjectteam.DataSource = dtRecord;
            GvrProjectteam.DataBind();
            Session["dtprojectteamrecord"] = dtRecord;
            Session["dtFilter"] = dtRecord;
        }
        else
        {
            DataTable Dtclear = new DataTable();
            GvrProjectteam.DataSource = Dtclear;
            GvrProjectteam.DataBind();
        }
        lblTotalRecords.Text = Resources.Attendance.Total_Records + ": " + dtRecord.Rows.Count + "";
        AllPageCode();
    
    }

    
    public void bindgriddetials()
    {
        pnlgrid.Visible = true;
        DataTable DtBindGrid = new DataTable();
        DtBindGrid = objProjectteam.GetRecordByProjectId(HiddeniD.Value);

        if (DtBindGrid.Rows.Count > 0)
        {
            grvteamlistDetailrecord.DataSource = DtBindGrid;
            grvteamlistDetailrecord.DataBind();


        }
        else
        {
            DataTable Dtclear = new DataTable();
            grvteamlistDetailrecord.DataSource = Dtclear;
            grvteamlistDetailrecord.DataBind();

        }

        pnlgrid.Visible = true;
    
    
    }

    protected void grvteamlistDetailrecord_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        grvteamlistDetailrecord.PageIndex = e.NewPageIndex;
        //DataTable dt = (DataTable)Session["dtFilterRecord"];
        //grvteamlistDetailrecord.DataSource = dt;
        //grvteamlistDetailrecord.DataBind();
        AllPageCode();
    }
    
    //protected void GvrProjectteam_Sorting(object sender, GridViewSortEventArgs e)
    //{
    //    HDFSort.Value = HDFSort.Value == "ASC" ? "DESC" : "ASC";
    //    DataTable dt = new DataTable();
    //    dt = (DataTable)Session["dtFilter"];
    //    DataView dv = new DataView(dt);
    //    string Query = "" + e.SortExpression + " " + HDFSort.Value + "";
    //    dv.Sort = Query;
    //    dt = dv.ToTable();
    //    Session["dtFilter"] = dt;

    //    GvrProjectteam.DataSource = dt;
    //    GvrProjectteam.DataBind();
    //}


    protected void GvrProjectteam_Sorting(object sender, GridViewSortEventArgs e)
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
        GvrProjectteam.DataSource = dt;
        GvrProjectteam.DataBind();

     AllPageCode();
    }

    protected void btList_Click(object sender, EventArgs e)
    {
        PanelList.BackColor = System.Drawing.ColorTranslator.FromHtml("#ccddee");
        pnlnew.BackColor = System.Drawing.ColorTranslator.FromHtml("#90bde9");
        pnllist.Visible = true;
        pnlgrid.Visible = false;
        pnlteamdetials.Visible = false;
        getprojectteam();
    }

   
    protected void btnEdit_Command(object sender, CommandEventArgs e)
    {
        string pid = "";
        PanelList.BackColor = System.Drawing.ColorTranslator.FromHtml(" #90bde9");
        pnlnew.BackColor = System.Drawing.ColorTranslator.FromHtml("#ccddee");
        pnllist.Visible = false;
        pnlteamdetials.Visible = true;
        pnlgrid.Visible = true;

        HiddeniD.Value = e.CommandArgument.ToString();
        DataTable Dt = new DataTable();
        Dt = objProjectteam.GetRecordByProjectId(HiddeniD.Value);

        if (Dt.Rows.Count > 0)
        {
            grvteamlistDetailrecord.DataSource = Dt;
            grvteamlistDetailrecord.DataBind();
            Session["dtFilterRecord"] = Dt;

        }
        else
        {
            DataTable Dtclear = new DataTable();
            grvteamlistDetailrecord.DataSource = Dtclear;
            grvteamlistDetailrecord.DataBind();

        }
        Fillddlprojectname();
     
    }


    protected void btnsubmit_Click(object sender, EventArgs e)
    {
        int i = 0;
        if (ddlprojectname.SelectedItem.Text == "--Select--" && ddlprojectname.SelectedItem.Text != "")
        {
            DisplayMessage("Select Project Name");

        }
        else if (ddlempname.SelectedItem.Text == "--Select--" && ddlempname.SelectedItem.Text != "")
        {
            DisplayMessage("Select Employee Name");

        }

        else if (hidTransId.Value != "")
        {
            if (chktaskvisibility.Checked)
            {
                objProjectteam.UpdateProjcetTeam(HiddeniD.Value.ToString(), ddlprojectname.SelectedValue, ddlempname.SelectedValue, "True", Session["UserId"].ToString(), DateTime.Now.ToString());
                DisplayMessage("Record Updated");
                chktaskvisibility.Checked = false;

            }
            else
            {
                objProjectteam.UpdateProjcetTeam(HiddeniD.Value.ToString(), ddlprojectname.SelectedValue, ddlempname.SelectedValue, "False", Session["UserId"].ToString(), DateTime.Now.ToString());
                DisplayMessage("Record Updated");
                chktaskvisibility.Checked = false;
            }
            hidTransId.Value = "";
            DataTable DtBindGrid1 = new DataTable();
            DtBindGrid1 = objProjectteam.GetRecordByProjectId(hidProId.Value.ToString());

            if (DtBindGrid1.Rows.Count > 0)
            {
                grvteamlistDetailrecord.DataSource = DtBindGrid1;
                grvteamlistDetailrecord.DataBind();
            }
            ddlprojectname.SelectedIndex = 0;
            ddlempname.SelectedIndex = 0;
           
        }
            else
            {
                i = 0;
                i = Convert.ToInt32(ddlprojectname.SelectedValue);
                if (chktaskvisibility.Checked)
                {

                    objProjectteam.InsertProjectTeam(ddlprojectname.SelectedValue.ToString(), ddlempname.SelectedValue.ToString(), "True", "", "", "", "", "", "", "", true.ToString(), Session["UserId"].ToString(), DateTime.Now.ToString(), Session["UserId"].ToString(), DateTime.Now.ToString());
                    ddlprojectname.SelectedIndex = 0;
                    ddlempname.SelectedIndex = 0;
                    chktaskvisibility.Checked = false;
                  
                  
                 
                }
                else
                {
                    objProjectteam.InsertProjectTeam(ddlprojectname.SelectedValue.ToString(), ddlempname.SelectedValue.ToString(), "False", "", "", "", "", "", "", "", true.ToString(), Session["UserId"].ToString(), DateTime.Now.ToString(), Session["UserId"].ToString(), DateTime.Now.ToString());
                    ddlprojectname.SelectedIndex = 0;
                    ddlempname.SelectedIndex = 0;
                    chktaskvisibility.Checked = false;
                    DisplayMessage("Record Successfully Save");
                   

                }

                DataTable DtBindGrid2 = new DataTable();
                DtBindGrid2 = objProjectteam.GetRecordByProjectId(i.ToString());

                if (DtBindGrid2.Rows.Count > 0)
                {
                    grvteamlistDetailrecord.DataSource = DtBindGrid2;
                    grvteamlistDetailrecord.DataBind();
                }
                ddlprojectname.SelectedIndex = 0;
                ddlempname.SelectedIndex = 0;
            }
 
        }
    
    protected void btndelete_Click(object sender, EventArgs e)
    {
        int j = 0;
        if (hidTransId.Value != "")
        {
            j = Convert.ToInt32(ddlprojectname.SelectedValue);
            objProjectteam.DeleteProjectTeam(hidTransId.Value.ToString());
            hidTransId.Value = "";
            pnlgrid.Visible = true;
            
            DataTable DtBindGrid2 = new DataTable();
            DtBindGrid2 = objProjectteam.GetRecordByProjectId(j.ToString());

            if (DtBindGrid2.Rows.Count > 0)
            {
                grvteamlistDetailrecord.DataSource = DtBindGrid2;
                grvteamlistDetailrecord.DataBind();
            }
            ddlprojectname.SelectedIndex = 0;
            ddlempname.SelectedIndex = 0;
            
            DisplayMessage("Record Successfully Deleted");
           
        }
        else
       {
           DisplayMessage("Record Select First");
       }


    }

    protected void GvrProjectteam_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        GvrProjectteam.PageIndex = e.NewPageIndex;
        DataTable dt = (DataTable)Session["dtFilter"];
        GvrProjectteam.DataSource = dt;
        GvrProjectteam.DataBind();
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
            DataTable dtProjectteam = (DataTable)Session["dtprojectteamrecord"];

            DataView view = new DataView(dtProjectteam, condition, "", DataViewRowState.CurrentRows);
            GvrProjectteam.DataSource = view.ToTable();
            Session["dtFilter"] = view.ToTable();
            lblTotalRecords.Text = Resources.Attendance.Total_Records + " : " + view.ToTable().Rows.Count + "";
            GvrProjectteam.DataBind();
           AllPageCode();
        }
    }


    protected void btnRefresh_Click(object sender, ImageClickEventArgs e)
    {
        getprojectteam();
        ddlFieldName.SelectedIndex = 1;
        ddlOption.SelectedIndex = 1;
        txtValue.Text = "";
    }
   
   
   
    protected void ddlprojectname_SelectedIndexChanged(object sender, EventArgs e)
    {
        pnlgrid.Visible = true;

        if (ddlprojectname.SelectedItem.Text == "--Select--")
        {
            DisplayMessage("Select Project Name");

        }
        else
        {
            DataTable dtBind = new DataTable();


            dtBind = objProjectteam.GetRecordByProjectId(ddlprojectname.SelectedValue);

            if (dtBind.Rows.Count > 0)
            {
                grvteamlistDetailrecord.DataSource = dtBind;
                grvteamlistDetailrecord.DataBind();


            }
            else
            {
                DataTable Dtclear = new DataTable();
                grvteamlistDetailrecord.DataSource = Dtclear;
                grvteamlistDetailrecord.DataBind();

            }
        }

    }
  

  protected void btnEditGrid_Command(object sender, CommandEventArgs e)
    {
      hidTransId.Value = "";
      int projectid = 0;
      int empid = 0;
      hidProId.Value = "";
      HiddeniD.Value = e.CommandArgument.ToString();
      hidTransId.Value = HiddeniD.Value;
      DataTable DtFill = new DataTable();
        string strtransid = HiddeniD.Value;
        Session["TeamTransId"] = strtransid;
        DtFill = objProjectteam.GetRecordByTransId(strtransid);
       

        DtFill = new DataView(DtFill, "Trans_Id=" + HiddeniD.Value+ "", "", DataViewRowState.CurrentRows).ToTable();
        
      if (DtFill.Rows.Count > 0)
      {
          hidProId.Value = DtFill.Rows[0]["Project_Id"].ToString();
            ddlempname.SelectedValue = DtFill.Rows[0]["Emp_Id"].ToString();
            ddlprojectname.SelectedValue = DtFill.Rows[0]["Project_Id"].ToString();
            if (DtFill.Rows[0]["Task_Visibility"].ToString() == "True")
            {
                chktaskvisibility.Checked = true;
            }
        }



      foreach (GridViewRow gvr in grvteamlistDetailrecord.Rows)
      {
          HiddenField hdntransid = (HiddenField)gvr.FindControl("hdntrans");
          Label lblprojectname = (Label)gvr.FindControl("lblpojectname");
          Label lblempname = (Label)gvr.FindControl("lblEmpIdList");
          HiddenField hdnprojectid = (HiddenField)gvr.FindControl("hdnprojectid");

          HiddenField hdnempid = (HiddenField)gvr.FindControl("hdnempid");

          projectid = Convert.ToInt32(hdnprojectid.Value);
          empid = Convert.ToInt32(hdnempid.Value);


          Session["TransId"] = hdntransid.Value;


      }
      
   
  }



  protected void btnEdit_Click(object sender, ImageClickEventArgs e)
  {


  }

  protected void btnEditGrid_Click(object sender, ImageClickEventArgs e)
  {

  }

     }


