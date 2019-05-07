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

public partial class Attendance_TimeTable : BasePage
{
   
    Common cmn = new Common();
    SystemParameter objSys = new SystemParameter();
    Att_TimeTable objTimeTable = new Att_TimeTable();

    Att_ShiftDescription objShiftDesc = new Att_ShiftDescription();

    Att_ScheduleMaster objSch = new Att_ScheduleMaster();
    Set_ApplicationParameter objAppParam = new Set_ApplicationParameter();
    protected void Page_Load(object sender, EventArgs e)
    {
       
       
        if (Session["UserId"] == null)
        {
            Response.Redirect("~/ERPLogin.aspx");
        }
        AllPageCode();
        if (!IsPostBack)
        {
            txtValue.Focus();
           
            FillGridBin();
            FillGrid();
            
            btnList_Click(null, null);
            
        }
       
    }

    public void AllPageCode()
    {
        Page.Title = objSys.GetSysTitle();

        Session["AccordianId"] = "9";
        Session["HeaderText"] = "Attendance Setup";
        DataTable dtAllPageCode = cmn.GetAllPagePermission(Session["UserId"].ToString(), "9", "38");
        if (dtAllPageCode.Rows.Count != 0)
        {
            if (dtAllPageCode.Rows[0][0].ToString() == "SuperAdmin")
            {
                btnSave.Visible = true;
                foreach (GridViewRow Row in gvTimeTable.Rows)
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
                        btnSave.Visible = true;
                    }
                    foreach (GridViewRow Row in gvTimeTable.Rows)
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
    protected void txtTimeTableName_OnTextChanged(object sender, EventArgs e)
    {

        if (editid.Value == "")
        {
            DataTable dt = objTimeTable.GetTimeTableMasterByTimeTableName(Session["CompId"].ToString().ToString(), txtTimeTableName.Text.Trim());
            if (dt.Rows.Count > 0)
            {
                txtTimeTableName.Text = "";
                DisplayMessage("TimeTable Name Already Exists");
                System.Web.UI.ScriptManager.GetCurrent(this).SetFocus(txtTimeTableName);
                return;
            }
            DataTable dt1 = objTimeTable.GetTimeTableMasterInactive(Session["CompId"].ToString().ToString());
            dt1 = new DataView(dt1, "TimeTable_Name='" + txtTimeTableName.Text + "'", "", DataViewRowState.CurrentRows).ToTable();
            if (dt1.Rows.Count > 0)
            {
                txtTimeTableName.Text = "";
                DisplayMessage("TimeTable Name Already Exists - Go to Bin Tab");
                System.Web.UI.ScriptManager.GetCurrent(this).SetFocus(txtTimeTableName);
                return;
            }
            txtTimeTableNameL.Focus();
        }
        else
        {
            DataTable dtTemp = objTimeTable.GetTimeTableMasterById(Session["CompId"].ToString().ToString(), editid.Value);
            if (dtTemp.Rows.Count > 0)
            {
                if (dtTemp.Rows[0]["TimeTable_Name"].ToString() != txtTimeTableName.Text)
                {
                    DataTable dt = objTimeTable.GetTimeTableMaster(Session["CompId"].ToString().ToString());
                    dt = new DataView(dt, "TimeTable_Name='" + txtTimeTableName.Text + "'", "", DataViewRowState.CurrentRows).ToTable();
                    if (dt.Rows.Count > 0)
                    {
                        txtTimeTableName.Text = "";
                        DisplayMessage("TimeTable Name Already Exists");
                        System.Web.UI.ScriptManager.GetCurrent(this).SetFocus(txtTimeTableName);
                        return;
                    }
                    DataTable dt1 = objTimeTable.GetTimeTableMaster(Session["CompId"].ToString().ToString());
                    dt1 = new DataView(dt1, "TimeTable_Name='" + txtTimeTableName.Text + "'", "", DataViewRowState.CurrentRows).ToTable();
                    if (dt1.Rows.Count > 0)
                    {
                        txtTimeTableName.Text = "";
                        DisplayMessage("TimeTable Name Already Exists - Go to Bin Tab");
                        System.Web.UI.ScriptManager.GetCurrent(this).SetFocus(txtTimeTableName);
                        return;
                    }
                }
            }
            txtTimeTableNameL.Focus();
        }
    }
  
    
    protected void txtBreakMin_OnTextChanged(object sender, EventArgs e)
    {
        if (txtOffDutyTime.Text == "" || txtOffDutyTime.Text == "__:__:__")
        {
            txtWorkMinute.Text = "";
            return;

        }

        if(txtBreakMinute.Text=="")
        {

            txtBreakMinute.Text = "0";
            
        }
        if (int.Parse(txtBreakMinute.Text)>=GetMinuteDiff(txtOffDutyTime.Text, txtOnDutyTime.Text))
        {
            DisplayMessage("Break minute cannot be greater than or equal to work minutes");
            txtBreakMinute.Text = "0";
            txtWorkMinute.Text = (GetMinuteDiff(txtOffDutyTime.Text, txtOnDutyTime.Text) - int.Parse(txtBreakMinute.Text)).ToString();
      
            return;
        }
        else
        {
        txtWorkMinute.Text =(GetMinuteDiff(txtOffDutyTime.Text, txtOnDutyTime.Text)-int.Parse(txtBreakMinute.Text)).ToString();
        }
        }

    protected void txtOnDutyTime_OnTextChanged(object sender, EventArgs e)
    {
        if (!CheckValid(txtOnDutyTime, "On Duty Time Required"))
        {
            txtWorkMinute.Text = "";
            return;
        }   
            txtBreakMin_OnTextChanged(null, null);


            txtOffDutyTime.Text = "00:00:00";
            txtOffDutyTime.Focus();
       
    }

    protected void txtOffDutyTime_OnTextChanged(object sender, EventArgs e)
    {
        if (!CheckValid(txtOnDutyTime, "On Duty Time Required"))
        {
            txtWorkMinute.Text = "";
            return;
        }


        if (!CheckValid(txtOffDutyTime, "Off Duty Time Required"))
        {
            txtWorkMinute.Text = "";
            return;
        }


        txtBreakMin_OnTextChanged(null,null);
 }
    protected void btnList_Click(object sender, EventArgs e)
    {
        txtValue.Focus();
        pnlMenuList.BackColor = System.Drawing.ColorTranslator.FromHtml("#ccddee");

        pnlMenuNew.BackColor = System.Drawing.ColorTranslator.FromHtml("#90bde9");
        pnlMenuBin.BackColor = System.Drawing.ColorTranslator.FromHtml("#90bde9");

        PnlList.Visible = true;
        PnlNewEdit.Visible = false;
        PnlBin.Visible = false;
    }
    protected void btnNew_Click(object sender, EventArgs e)
    {
         txtTimeTableName.Focus();
        pnlMenuNew.BackColor = System.Drawing.ColorTranslator.FromHtml("#ccddee");

        pnlMenuList.BackColor = System.Drawing.ColorTranslator.FromHtml("#90bde9");
        pnlMenuBin.BackColor = System.Drawing.ColorTranslator.FromHtml("#90bde9");

        PnlList.Visible = false;
        PnlNewEdit.Visible = true;
        PnlBin.Visible = false;

    }
    protected void btnBin_Click(object sender, EventArgs e)
    {
        txtbinValue.Focus();
        pnlMenuBin.BackColor = System.Drawing.ColorTranslator.FromHtml("#ccddee");

        pnlMenuNew.BackColor = System.Drawing.ColorTranslator.FromHtml("#90bde9");
        pnlMenuList.BackColor = System.Drawing.ColorTranslator.FromHtml("#90bde9");
        PnlNewEdit.Visible = false;
        PnlBin.Visible = true;
        PnlList.Visible = false;
        FillGridBin();
    }

    protected void btnRefresh_Click(object sender, ImageClickEventArgs e)
    {
        FillGrid();
        FillGridBin();

        ddlOption.SelectedIndex = 2;
        ddlFieldName.SelectedIndex = 0;
        txtValue.Text = "";
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
            DataTable dtCust = (DataTable)Session["TimeTable"];


            DataView view = new DataView(dtCust, condition, "", DataViewRowState.CurrentRows);
            Session["dtFilter"] = view.ToTable();
            lblTotalRecords.Text = Resources.Attendance.Total_Records + ": " + view.ToTable().Rows.Count.ToString() + "";

            gvTimeTable.DataSource = view.ToTable();
            gvTimeTable.DataBind();
            AllPageCode();


        }


    }
    protected void btnbinbind_Click(object sender, ImageClickEventArgs e)
    {
        if (ddlbinOption.SelectedIndex != 0)
        {
            string condition = string.Empty;


            if (ddlbinOption.SelectedIndex == 1)
            {
                condition = "convert(" + ddlbinFieldName.SelectedValue + ",System.String)='" + txtbinValue.Text.Trim() + "'";
            }
            else if (ddlbinOption.SelectedIndex == 2)
            {
                condition = "convert(" + ddlbinFieldName.SelectedValue + ",System.String) like '%" + txtbinValue.Text + "%'";
            }
            else
            {
                condition = "convert(" + ddlbinFieldName.SelectedValue + ",System.String) Like '" + txtbinValue.Text + "%'";
            }
            DataTable dtCust = (DataTable)Session["dtbinTimeTable"];


            DataView view = new DataView(dtCust, condition, "", DataViewRowState.CurrentRows);
            Session["dtbinFilter"] = view.ToTable();
            lblbinTotalRecords.Text = Resources.Attendance.Total_Records + ": " + view.ToTable().Rows.Count.ToString() + "";

            gvTimeTableBin.DataSource = view.ToTable();
            gvTimeTableBin.DataBind();


            if (view.ToTable().Rows.Count == 0)
            {
                imgBtnRestore.Visible = false;
                ImgbtnSelectAll.Visible = false;
            }
            else
            {
                AllPageCode();
            }
        }
    }
    private int GetMinuteDiff(string greatertime, string lesstime)
    {

        if (greatertime == "__:__:__" || greatertime=="")
        {
            return 0;
        }
        if (lesstime == "__:__:__" || lesstime=="")
        {
            return 0;
        }
        int retval = 0;
        int actTimeHour = Convert.ToInt32(greatertime.Split(':')[0]);
        int ondutyhour = Convert.ToInt32(lesstime.Split(':')[0]);
        int actTimeMinute = Convert.ToInt32(greatertime.Split(':')[1]);
        int ondutyMinute = Convert.ToInt32(lesstime.Split(':')[1]);
        int totalActTimeMinute = actTimeHour * 60 + actTimeMinute;
        int totalOnDutyTimeMinute = ondutyhour * 60 + ondutyMinute;
        if (totalActTimeMinute - totalOnDutyTimeMinute < 0)
        {
            retval = 1440 + (totalActTimeMinute - totalOnDutyTimeMinute);
        }
        else
        {
            retval = (totalActTimeMinute - totalOnDutyTimeMinute);
        }
        return retval;
    }
    protected void btnbinRefresh_Click(object sender, ImageClickEventArgs e)
    {
        FillGrid();
        FillGridBin();

        ddlbinOption.SelectedIndex = 2;
        ddlbinFieldName.SelectedIndex = 0;
        txtbinValue.Text = "";
        lblSelectedRecord.Text = "";
    }

    protected void gvTimeTableBin_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {

        gvTimeTableBin.PageIndex = e.NewPageIndex;
        if (HDFSortbin.Value == "")
            FillGridBin();
        else
        {
            DataTable dt = (DataTable)Session["dtbinFilter"];
            gvTimeTableBin.DataSource = dt;
            gvTimeTableBin.DataBind();
            AllPageCode();
        }
        string temp = string.Empty;
        bool isselcted;

        for (int i = 0; i < gvTimeTableBin.Rows.Count; i++)
        {
            Label lblconid = (Label)gvTimeTableBin.Rows[i].FindControl("lblTimeTableId");
            string[] split = lblSelectedRecord.Text.Split(',');

            for (int j = 0; j < lblSelectedRecord.Text.Split(',').Length; j++)
            {
                if (lblSelectedRecord.Text.Split(',')[j] != "")
                {
                    if (lblconid.Text.Trim().ToString() == lblSelectedRecord.Text.Split(',')[j].Trim().ToString())
                    {
                        ((CheckBox)gvTimeTableBin.Rows[i].FindControl("chkgvSelect")).Checked = true;
                    }
                }
            }
        }

    }
    protected void gvTimeTableBin_OnSorting(object sender, GridViewSortEventArgs e)
    {
        lblSelectedRecord.Text = "";
        HDFSortbin.Value = HDFSortbin.Value == "ASC" ? "DESC" : "ASC";
        DataTable dt = new DataTable();
        dt = objTimeTable.GetTimeTableMasterInactive(Session["CompId"].ToString());
        DataView dv = new DataView(dt);
        string Query = "" + e.SortExpression + " " + HDFSortbin.Value + "";
        dv.Sort = Query;
        dt = dv.ToTable();
        Session["dtbinFilter"] = dt;
        gvTimeTableBin.DataSource = dt;
        gvTimeTableBin.DataBind();
        AllPageCode();

    }
    

   
    protected void btnSave_Click(object sender, EventArgs e)
    {
        int b = 0;

      

        if (txtTimeTableName.Text == "")
        {
            DisplayMessage("Enter TimeTable Name");
            txtTimeTableName.Focus();
            return;
        }

        if (!CheckValid(txtOnDutyTime, "On Duty Time Required"))
        {
            return;
        }


        if (!CheckValid(txtOffDutyTime, "Off Duty Time Required"))
        {
            return;
        }
        if (!CheckValid(txtBreakMinute, "Break Minute Required"))
        {
            return;
        }

        

        if (!CheckValid(txtBeginingIn, "Beginning In Required"))
        {
            return;
        }

        if (!CheckValid(txtEndingIn, "Ending In Required"))
        {
            return;
        }

        if (!CheckValid(txtBeginingOut, "Beginning Out Required"))
        {
            return;
        }

        if (!CheckValid(txtEndingOut, "Ending Out Required"))
        {
            return;
        }

        if (!Check24Format(txtOnDutyTime, "Invalid Time ! Please Read * Note"))
        {
            return;
        }


        if (!Check24Format(txtOffDutyTime, "Invalid Time ! Please Read * Note"))
        {
            return;
        }

        if (!Check24Format(txtBeginingIn, "Invalid Time ! Please Read * Note"))
        {
            return;
        }

        if (!Check24Format(txtEndingIn, "Invalid Time ! Please Read * Note"))
        {
            return;
        }

        if (!Check24Format(txtBeginingOut, "Invalid Time ! Please Read * Note"))
        {
            return;
        }

        if (!Check24Format(txtEndingOut, "Invalid Time ! Please Read * Note"))
        {
            return;
        }


        
        DateTime beginingin = Convert.ToDateTime(txtBeginingIn.Text);
        DateTime ondutytime = Convert.ToDateTime(txtOnDutyTime.Text);
       

        DateTime endingin = Convert.ToDateTime(txtEndingIn.Text);
        DateTime endingout = Convert.ToDateTime(txtEndingOut.Text);
      
        DateTime beginingout = Convert.ToDateTime(txtBeginingOut.Text);
     
        DateTime offdutytime = Convert.ToDateTime(txtOffDutyTime.Text);
                
        DateTime offduty = Convert.ToDateTime(txtOffDutyTime.Text);



        bool IsLate = Convert.ToBoolean(objAppParam.GetApplicationParameterValueByParamName("Is_Late_Penalty", Session["CompId"].ToString()));
        
        string LatePenaltyMethod=objAppParam.GetApplicationParameterValueByParamName("Is_Late_Penalty_Method", Session["CompId"].ToString());

            if(IsLate)
            {
                if(LatePenaltyMethod.Trim()=="Salary")
                {
                    double relaxlatemin=0;


                    relaxlatemin = double.Parse(objAppParam.GetApplicationParameterValueByParamName("Late_Relaxation_Min", Session["CompId"].ToString()));

                    DateTime EndingIn1=endingin;

                    endingin = ondutytime.AddMinutes(relaxlatemin);


                    if (ondutytime <= EndingIn1 && EndingIn1 < endingin)
                    {
                        DisplayMessage("Set Ending In time after "+relaxlatemin+" minute (Relaxation Late Minute) of On Duty Time") ;
                        return;

                    }
                }



            }

            bool IsEarly = Convert.ToBoolean(objAppParam.GetApplicationParameterValueByParamName("Is_Early_Penalty", Session["CompId"].ToString()));

            string EarlyPenaltyMethod = objAppParam.GetApplicationParameterValueByParamName("Is_Early_Penalty_Method", Session["CompId"].ToString());

            if (IsEarly)
            {
                if (EarlyPenaltyMethod.Trim() == "Salary")
                {
                    double relaxEarlymin = 0;


                    relaxEarlymin = double.Parse(objAppParam.GetApplicationParameterValueByParamName("Early_Relaxation_Min", Session["CompId"].ToString()));

                    DateTime BegningOut1 = beginingout;

                    beginingout = offdutytime.AddMinutes(-relaxEarlymin);

                    if (offdutytime >= BegningOut1 && BegningOut1 > beginingout)
                    {
                        DisplayMessage("Set Begining Out time before " + relaxEarlymin + " minute (Relaxation Early Minute) of Off Duty Time");
                      
                          return;

                    }
                }



            }
          int MinuteDiff=0;
           string mindiff = (GetMinuteDiff(txtOffDutyTime.Text, txtOnDutyTime.Text)-int.Parse(txtBreakMinute.Text)).ToString();
           MinuteDiff = Convert.ToInt32(mindiff);

           string ShortestTime = objAppParam.GetApplicationParameterValueByParamName("Shortest Time Table", Session["CompId"].ToString());
           int shorttime = int.Parse(ShortestTime);

        if(MinuteDiff < shorttime)
        {
            DisplayMessage("Work minute cannot be less than Shortest Time Table Duration ("+shorttime.ToString()+" minutes)");
            return;

        }


        if (editid.Value == "")
        {
            
            DataTable dt1 = objTimeTable.GetTimeTableMaster(Session["CompId"].ToString());

            dt1 = new DataView(dt1, "TimeTable_Name='" + txtTimeTableName.Text + "'", "", DataViewRowState.CurrentRows).ToTable();
            if (dt1.Rows.Count > 0)
            {
                DisplayMessage("TimeTable Name Already Exists");
                txtTimeTableName.Focus();
                return;

            }




         b = objTimeTable.InsertTimeTableMaster(Session["CompId"].ToString(),txtTimeTableName.Text, txtTimeTableNameL.Text,Session["BrandId"].ToString(),Convert.ToDateTime(txtOnDutyTime.Text),Convert.ToDateTime(txtOffDutyTime.Text),"0","0",txtBeginingIn.Text,txtBeginingOut.Text,txtEndingIn.Text,txtEndingOut.Text,mindiff.ToString(),txtBreakMinute.Text,false.ToString(),"", "", "", "", "", true.ToString(), DateTime.Now.ToString(), true.ToString(), Session["UserId"].ToString(), DateTime.Now.ToString(), Session["UserId"].ToString(), DateTime.Now.ToString());
            if (b != 0)
            {
                DisplayMessage("Record Saved");
                FillGrid();
                Reset();
                btnList_Click(null, null);
            }
            else
            {
                DisplayMessage("Record Not Saved");
            }
        }
        else
        {
            string TimeTableTypeName = string.Empty;
            DataTable dt1 = objTimeTable.GetTimeTableMaster(Session["CompId"].ToString());
            try
            {
                TimeTableTypeName = new DataView(dt1, "TimeTable_Id='" + editid.Value + "'", "", DataViewRowState.CurrentRows).ToTable().Rows[0]["TimeTable_Name"].ToString();
            }
            catch
            {
                TimeTableTypeName = "";
            }
            dt1 = new DataView(dt1, "TimeTable_Name='" + txtTimeTableName.Text + "' and TimeTable_Name<>'"+TimeTableTypeName+"'", "", DataViewRowState.CurrentRows).ToTable();
            if (dt1.Rows.Count > 0)
            {
                DisplayMessage("TimeTable Name Already Exists");
                txtTimeTableName.Focus();
                return;

            }
            b = objTimeTable.UpdateTimeTableMaster(editid.Value, Session["CompId"].ToString(), txtTimeTableName.Text, txtTimeTableNameL.Text, Session["BrandId"].ToString(), Convert.ToDateTime(txtOnDutyTime.Text).ToString(), Convert.ToDateTime(txtOffDutyTime.Text).ToString(),"0", "0", txtBeginingIn.Text, txtBeginingOut.Text,txtEndingIn.Text, txtEndingOut.Text, mindiff.ToString(),txtBreakMinute.Text,false.ToString(), "", "", "", "", "", true.ToString(), DateTime.Now.ToString(), true.ToString(), Session["UserId"].ToString(), DateTime.Now.ToString());
        
            if (b != 0)
            {
                btnList_Click(null, null);
                DisplayMessage("Record Updated");
                Reset();
                FillGrid();


            }
            else
            {
                DisplayMessage("Record Not Updated");
            }

        }
    }


    public bool CheckValid(TextBox txt, string Error_messagevalue)
    {

        if (txt.Text == "")
        {

            DisplayMessage(Error_messagevalue);
            txt.Focus();
            return false;
        }
        else if (txt.Text == "__:__:__")
        {   
             DisplayMessage(Error_messagevalue);
            txt.Focus();
            return false;
        }

        else
        {
            return true;
        }

    }
    public bool Check24Format(TextBox txt, string ErrorMessage)
    {

        if (!System.Text.RegularExpressions.Regex.IsMatch(txt.Text.ToString(), "^((0?[1-9]|1[012])(:[0-5]\\d){0,2}(\\ [AP]M))$|^([01]\\d|2[0-3])(:[0-5]\\d){0,2}$"))
        {
            DisplayMessage(ErrorMessage);
            txt.Focus();
            return false;
        }
        else
        {
            return true; ;
        }



    }
    protected void btnEdit_Command(object sender, CommandEventArgs e)
    {
        editid.Value = e.CommandArgument.ToString();

        DataTable dtShift = objShiftDesc.GetShift_TimeTable();

        dtShift = new DataView(dtShift,"TimeTable_Id='"+editid.Value+"'","",DataViewRowState.CurrentRows).ToTable();

        if(dtShift.Rows.Count > 0)
        {
            DisplayMessage("You cannot edit this timetable");
            return;

        }

        dtShift = null;
        dtShift = objSch.GetSheduleDescription();
        dtShift = new DataView(dtShift, "TimeTable_Id='" + e.CommandArgument.ToString() + "'", "", DataViewRowState.CurrentRows).ToTable();

        if (dtShift.Rows.Count > 0)
        {
            DisplayMessage("You cannot edit this timetable");
            return;

        }
        DataTable dt = objTimeTable.GetTimeTableMasterById(Session["CompId"].ToString(), editid.Value);
        if (dt.Rows.Count > 0)
        {
           
            txtTimeTableName.Text = dt.Rows[0]["TimeTable_Name"].ToString();
            txtTimeTableNameL.Text = dt.Rows[0]["TimeTable_Name_L"].ToString();


            txtOnDutyTime.Text = dt.Rows[0]["OnDuty_Time"].ToString();


            txtOffDutyTime.Text = dt.Rows[0]["OffDuty_Time"].ToString(); 
            txtBeginingIn.Text = dt.Rows[0]["Beginning_In"].ToString();
            txtBeginingOut.Text = dt.Rows[0]["Beginning_Out"].ToString();
            txtEndingIn.Text = dt.Rows[0]["Ending_In"].ToString();
            txtEndingOut.Text = dt.Rows[0]["Ending_Out"].ToString();
            txtWorkMinute.Text = dt.Rows[0]["Work_Minute"].ToString();
            txtBreakMinute.Text = dt.Rows[0]["Break_Min"].ToString();

           
           

           
           
            btnNew_Click(null, null);
            btnNew.Text = Resources.Attendance.Edit;

        }



    }

    protected void IbtnDelete_Command(object sender, CommandEventArgs e)
    {
        DataTable dtShift = objShiftDesc.GetShift_TimeTable();

        dtShift = new DataView(dtShift, "TimeTable_Id='" + e.CommandArgument.ToString() + "'", "", DataViewRowState.CurrentRows).ToTable();

        if (dtShift.Rows.Count > 0)
        {
            DisplayMessage("You cannot delete this timetable");
            return;

        }
        dtShift = null;
        dtShift = objSch.GetSheduleDescription();
        dtShift = new DataView(dtShift, "TimeTable_Id='" + e.CommandArgument.ToString() + "'", "", DataViewRowState.CurrentRows).ToTable();

        if (dtShift.Rows.Count > 0)
        {
            DisplayMessage("You cannot delete this timetable");
            return;

        }


        int b = 0;
        b = objTimeTable.DeleteTimeTableMaster(Session["CompId"].ToString(), e.CommandArgument.ToString(), false.ToString(), Session["UserId"].ToString(), DateTime.Now.ToString());
        if (b != 0)
        {
            DisplayMessage("Record Deleted");

            FillGridBin();
            FillGrid();
            Reset();
        }
        else
        {
            DisplayMessage("Record Not Deleted");
        }
    }
    protected void gvTimeTable_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        gvTimeTable.PageIndex = e.NewPageIndex;

        DataTable dt = (DataTable)Session["dtFilter"];
        gvTimeTable.DataSource = dt;
        gvTimeTable.DataBind();
        AllPageCode();

    }
    protected void gvTimeTable_OnSorting(object sender, GridViewSortEventArgs e)
    {
        HDFSort.Value = HDFSort.Value == "ASC" ? "DESC" : "ASC";
        DataTable dt = new DataTable();
        dt = (DataTable)Session["dtFilter"];
        DataView dv = new DataView(dt);
        string Query = "" + e.SortExpression + " " + HDFSort.Value + "";
        dv.Sort = Query;
        dt = dv.ToTable();
        Session["dtFilter"] = dt;
        gvTimeTable.DataSource = dt;
        gvTimeTable.DataBind();
        AllPageCode();
    }

    
    [System.Web.Services.WebMethodAttribute(), System.Web.Script.Services.ScriptMethodAttribute()]
    public static string[] GetCompletionListTimeTableName(string prefixText, int count, string contextKey)
    {
        Att_TimeTable objAtt_TimeTable = new Att_TimeTable();
        DataTable dt = new DataView(objAtt_TimeTable.GetTimeTableMaster(HttpContext.Current.Session["CompId"].ToString()), "TimeTable_Name like '" + prefixText.ToString() + "%'", "", DataViewRowState.CurrentRows).ToTable();
        string[] txt = new string[dt.Rows.Count];

        for (int i = 0; i < dt.Rows.Count; i++)
        {
            txt[i] = dt.Rows[i]["TimeTable_Name"].ToString();
        }
        return txt;
    }

   
    public void DisplayMessage(string str)
    {
        ScriptManager.RegisterStartupScript(this, GetType(), "", "alert('" + str + "');", true);
    }
    protected void btnReset_Click(object sender, EventArgs e)
    {
        Reset();
    }
    protected void btnCancel_Click(object sender, EventArgs e)
    {
        FillGrid();
        FillGridBin();
        Reset();
        btnList_Click(null, null);
    }

    public void FillGrid()
    {
        DataTable dt = objTimeTable.GetTimeTableMaster(Session["CompId"].ToString());
        dt = new DataView(dt, "Brand_Id='" + Session["BrandId"].ToString() + "'", "", DataViewRowState.CurrentRows).ToTable();
      
        gvTimeTable.DataSource = dt;
        gvTimeTable.DataBind();
        AllPageCode();
        Session["dtFilter"] = dt;
        Session["TimeTable"] = dt;

        lblTotalRecords.Text = Resources.Attendance.Total_Records + ": " + dt.Rows.Count.ToString() + "";

    }
    public void FillGridBin()
    {
        DataTable dt = new DataTable();
        dt = objTimeTable.GetTimeTableMasterInactive(Session["CompId"].ToString());
        dt = new DataView(dt, "Brand_Id='" + Session["BrandId"].ToString() + "'", "", DataViewRowState.CurrentRows).ToTable();
      
        gvTimeTableBin.DataSource = dt;
        gvTimeTableBin.DataBind();


        Session["dtbinFilter"] = dt;
        Session["dtbinTimeTable"] = dt;
        lblbinTotalRecords.Text = Resources.Attendance.Total_Records + ": " + dt.Rows.Count.ToString() + "";
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


    protected void chkgvSelectAll_CheckedChanged(object sender, EventArgs e)
    {
        CheckBox chkSelAll = ((CheckBox)gvTimeTableBin.HeaderRow.FindControl("chkgvSelectAll"));
        for (int i = 0; i < gvTimeTableBin.Rows.Count; i++)
        {
            ((CheckBox)gvTimeTableBin.Rows[i].FindControl("chkgvSelect")).Checked = chkSelAll.Checked;
            if (chkSelAll.Checked)
            {
                if (!lblSelectedRecord.Text.Split(',').Contains(((Label)(gvTimeTableBin.Rows[i].FindControl("lblTimeTableId"))).Text.Trim().ToString()))
                {
                    lblSelectedRecord.Text += ((Label)(gvTimeTableBin.Rows[i].FindControl("lblTimeTableId"))).Text.Trim().ToString() + ",";
                }
            }
            else
            {
                string temp = string.Empty;
                string[] split = lblSelectedRecord.Text.Split(',');
                foreach (string item in split)
                {
                    if (item != ((Label)(gvTimeTableBin.Rows[i].FindControl("lblTimeTableId"))).Text.Trim().ToString())
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
        Label lb = (Label)gvTimeTableBin.Rows[index].FindControl("lblTimeTableId");
        if (((CheckBox)gvTimeTableBin.Rows[index].FindControl("chkgvSelect")).Checked)
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
    protected void ImgbtnSelectAll_Click(object sender, ImageClickEventArgs e)
    {
        DataTable dtUnit = (DataTable)Session["dtbinFilter"];

        if (ViewState["Select"] == null)
        {
            ViewState["Select"] = 1;
            foreach (DataRow dr in dtUnit.Rows)
            {
                if (!lblSelectedRecord.Text.Split(',').Contains(dr["TimeTable_Id"]))
                {
                    lblSelectedRecord.Text += dr["TimeTable_Id"] + ",";
                }
            }
            for (int i = 0; i < gvTimeTableBin.Rows.Count; i++)
            {
                string[] split = lblSelectedRecord.Text.Split(',');
                Label lblconid = (Label)gvTimeTableBin.Rows[i].FindControl("lblTimeTableId");
                for (int j = 0; j < lblSelectedRecord.Text.Split(',').Length; j++)
                {
                    if (lblSelectedRecord.Text.Split(',')[j] != "")
                    {
                        if (lblconid.Text.Trim().ToString() == lblSelectedRecord.Text.Split(',')[j].Trim().ToString())
                        {
                            ((CheckBox)gvTimeTableBin.Rows[i].FindControl("chkgvSelect")).Checked = true;
                        }
                    }
                }
            }
        }
        else
        {
            lblSelectedRecord.Text = "";
            DataTable dtUnit1 = (DataTable)Session["dtBinFilter"];
            gvTimeTableBin.DataSource = dtUnit1;
            gvTimeTableBin.DataBind();
            ViewState["Select"] = null;
        }



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
                    b = objTimeTable.DeleteTimeTableMaster(Session["CompId"].ToString(),lblSelectedRecord.Text.Split(',')[j].Trim(), true.ToString(), Session["UserId"].ToString().ToString(), DateTime.Now.ToString());

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
        }
        else
        {
            int fleg = 0;
            foreach (GridViewRow Gvr in gvTimeTableBin.Rows)
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
    
   public void Reset()
    {


       
        txtTimeTableName.Text = "";
        txtTimeTableNameL.Text = "";

        txtOnDutyTime.Text = "";
        txtOffDutyTime.Text = "";
        txtBeginingIn.Text = "";
        txtBeginingOut.Text = "";
        txtWorkMinute.Text = "";
        txtBreakMinute.Text = "";
        txtEndingIn.Text = "";
        txtEndingOut.Text = "";

        


        
        btnNew.Text = Resources.Attendance.New;
        lblSelectedRecord.Text = "";
        ViewState["Select"] = null;
        editid.Value = "";
        txtValue.Text = "";
        ddlOption.SelectedIndex = 2;
        ddlFieldName.SelectedIndex = 0;
   
        

    }

   
}
