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

public partial class Attendance_LogProcess : BasePage
{
    Att_AttendanceLog objAttLog = new Att_AttendanceLog();
    Set_EmployeeGroup_Master objEmpGroup = new Set_EmployeeGroup_Master();
    SystemParameter objSys = new SystemParameter();
    Set_Group_Employee objGroupEmp = new Set_Group_Employee();
    EmployeeMaster objEmp = new EmployeeMaster();
    Att_TimeTable objTimeTable = new Att_TimeTable();
    Set_ApplicationParameter objAppParam = new Set_ApplicationParameter();
    Att_ScheduleMaster objEmpSch = new Att_ScheduleMaster();
    Att_ShiftDescription objShift = new Att_ShiftDescription();
    Set_Employee_Holiday objEmpHoliday = new Set_Employee_Holiday();
    Att_Leave_Request ObjLeaveReq = new Att_Leave_Request();
    EmployeeParameter objEmpParam = new EmployeeParameter();
    Att_AttendanceRegister objAttReg = new Att_AttendanceRegister();
    Att_PartialLeave_Request objPartialReq = new Att_PartialLeave_Request();
    Pay_Employee_Attendance objPayEmpAtt = new Pay_Employee_Attendance();

    protected void Page_Load(object sender, EventArgs e)
    {

        if (Session["UserId"] == null)
        {
            Response.Redirect("~/ERPLogin.aspx");
        }
        Page.Title = objSys.GetSysTitle();
        if (!IsPostBack)
        {

            pnlEmpAtt.Visible = true;

            FillGrid();
            rbtnEmpSal.Checked = true;
            rbtnGroupSal.Checked = false;
            EmpGroupSal_CheckedChanged(null, null);

        }
        Session["AccordianId"] = "9";
        Session["HeaderText"] = "Attendance Setup";

    }

    public void FillGrid()
    {
        DataTable dtEmp = objEmp.GetEmployeeMaster(Session["CompId"].ToString());

        dtEmp = new DataView(dtEmp, "Brand_Id='" + Session["BrandId"].ToString() + "'  and Location_Id='" + Session["LocId"].ToString() + "'", "", DataViewRowState.CurrentRows).ToTable();

        if (Session["SessionDepId"] != null)
        {
            dtEmp = new DataView(dtEmp, "Department_Id in(" + Session["SessionDepId"].ToString().Substring(0, Session["SessionDepId"].ToString().Length - 1) + ")", "", DataViewRowState.CurrentRows).ToTable();

        }
        if (dtEmp.Rows.Count > 0)
        {
            Session["dtEmp"] = dtEmp;
            gvEmployee.DataSource = dtEmp;
            gvEmployee.DataBind();
            lblTotalRecord.Text = Resources.Attendance.Total_Records + " : " + dtEmp.Rows.Count.ToString() + "";

        }
    }




    protected void lbxGroupSal_SelectedIndexChanged(object sender, EventArgs e)
    {
        string GroupIds = string.Empty;
        string EmpIds = string.Empty;
        for (int i = 0; i < lbxGroupSal.Items.Count; i++)
        {
            if (lbxGroupSal.Items[i].Selected == true)
            {
                GroupIds += lbxGroupSal.Items[i].Value + ",";

            }

        }
        if (GroupIds != "")
        {
            DataTable dtEmp = objEmp.GetEmployeeMaster(Session["CompId"].ToString());

            dtEmp = new DataView(dtEmp, "Brand_Id='" + Session["BrandId"].ToString() + "'  and Location_Id='" + Session["LocId"].ToString() + "'", "", DataViewRowState.CurrentRows).ToTable();
            DataTable dtEmpInGroup = objGroupEmp.GetGroup_EmployeeById(Session["CompId"].ToString());

            dtEmpInGroup = new DataView(dtEmpInGroup, "Group_Id in(" + GroupIds.Substring(0, GroupIds.Length - 1) + ")", "", DataViewRowState.CurrentRows).ToTable();

            for (int i = 0; i < dtEmpInGroup.Rows.Count; i++)
            {
                if (!EmpIds.Split(',').Contains(dtEmpInGroup.Rows[i]["Emp_Id"].ToString()))
                {
                    EmpIds += dtEmpInGroup.Rows[i]["Emp_Id"].ToString() + ",";
                }
            }
            if (EmpIds != "")
            {
                dtEmp = new DataView(dtEmp, "Emp_Id in(" + EmpIds.Substring(0, EmpIds.Length - 1) + ")", "", DataViewRowState.CurrentRows).ToTable();
            }
            else
            {
                dtEmp = new DataTable();
            }
            if (dtEmp.Rows.Count > 0)
            {
                Session["dtEmp4"] = dtEmp;
                gvEmployeeSal.DataSource = dtEmp;
                gvEmployeeSal.DataBind();

            }
            else
            {
                Session["dtEmp4"] = null;
                gvEmployeeSal.DataSource = dtEmp;
                gvEmployeeSal.DataBind();
            }
        }
        else
        {
            gvEmployeeSal.DataSource = null;
            gvEmployeeSal.DataBind();

        }
    }
    protected void EmpGroupSal_CheckedChanged(object sender, EventArgs e)
    {

        if (rbtnGroupSal.Checked)
        {
            pnlEmp.Visible = false;
            pnlGroupSal.Visible = true;

            DataTable dtGroup = objEmpGroup.GetEmployeeGroup_Master(Session["CompId"].ToString());
            dtGroup = new DataView(dtGroup, "Brand_Id='" + Session["BrandId"].ToString() + "'  and Location_Id='" + Session["LocId"].ToString() + "'", "", DataViewRowState.CurrentRows).ToTable();
            if (dtGroup.Rows.Count > 0)
            {
                lbxGroupSal.DataSource = dtGroup;
                lbxGroupSal.DataTextField = "Group_Name";
                lbxGroupSal.DataValueField = "Group_Id";

                lbxGroupSal.DataBind();

            }
            gvEmployeeSal.DataSource = null;
            gvEmployeeSal.DataBind();


            lbxGroupSal_SelectedIndexChanged(null, null);
        }
        else if (rbtnEmpSal.Checked)
        {
            pnlEmp.Visible = true;
            pnlGroupSal.Visible = false;

            lblEmp.Text = "";
            lblSelectRecord.Text = "";
            string GroupIds = string.Empty;
            string EmpIds = string.Empty;
            DataTable dtEmp = objEmp.GetEmployeeMaster(Session["CompId"].ToString());

            dtEmp = new DataView(dtEmp, "Brand_Id='" + Session["BrandId"].ToString() + "'  and Location_Id='" + Session["LocId"].ToString() + "'", "", DataViewRowState.CurrentRows).ToTable();

            if (Session["SessionDepId"] != null)
            {

                dtEmp = new DataView(dtEmp, "Department_Id in(" + Session["SessionDepId"].ToString().Substring(0, Session["SessionDepId"].ToString().Length - 1) + ")", "", DataViewRowState.CurrentRows).ToTable();

            }


            if (dtEmp.Rows.Count > 0)
            {
                Session["dtEmp4"] = dtEmp;

                gvEmployee.DataSource = dtEmp;
                gvEmployee.DataBind();

            }
            else
            {
                gvEmployee.DataSource = null;
                gvEmployee.DataBind();

            }

        }


    }

    protected void gvEmployeeSal_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        gvEmployeeSal.PageIndex = e.NewPageIndex;
        gvEmployeeSal.DataSource = (DataTable)Session["dtEmp4"];
        gvEmployeeSal.DataBind();
    }

    protected void gvEmp_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {

        gvEmployee.PageIndex = e.NewPageIndex;
        DataTable dtEmp = (DataTable)Session["dtEmp"];
        gvEmployee.DataSource = dtEmp;
        gvEmployee.DataBind();
        string temp = string.Empty;


        for (int i = 0; i < gvEmployee.Rows.Count; i++)
        {
            Label lblconid = (Label)gvEmployee.Rows[i].FindControl("lblEmpId");
            string[] split = lblSelectRecord.Text.Split(',');

            for (int j = 0; j < lblSelectRecord.Text.Split(',').Length; j++)
            {
                if (lblSelectRecord.Text.Split(',')[j] != "")
                {
                    if (lblconid.Text.Trim().ToString() == lblSelectRecord.Text.Split(',')[j].Trim().ToString())
                    {
                        ((CheckBox)gvEmployee.Rows[i].FindControl("chkgvSelect")).Checked = true;
                    }
                }
            }
        }
    }

    protected void chkgvSelect_CheckedChanged(object sender, EventArgs e)
    {
        string empidlist = string.Empty;
        string temp = string.Empty;
        int index = ((GridViewRow)((CheckBox)sender).Parent.Parent).RowIndex;
        Label lb = (Label)gvEmployee.Rows[index].FindControl("lblEmpId");
        if (((CheckBox)gvEmployee.Rows[index].FindControl("chkgvSelect")).Checked)
        {
            empidlist += lb.Text.Trim().ToString() + ",";
            lblSelectRecord.Text += empidlist;

        }

        else
        {

            empidlist += lb.Text.ToString().Trim();
            lblSelectRecord.Text += empidlist;
            string[] split = lblSelectRecord.Text.Split(',');
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
            lblSelectRecord.Text = temp;
        }
    }



    protected void ImgbtnSelectAll_Clickary(object sender, ImageClickEventArgs e)
    {
        DataTable dtProduct = (DataTable)Session["dtEmp"];

        if (ViewState["Select"] == null)
        {
            ViewState["Select"] = 1;
            foreach (DataRow dr in dtProduct.Rows)
            {
                if (!lblSelectRecord.Text.Split(',').Contains(dr["Emp_Id"]))
                {
                    lblSelectRecord.Text += dr["Emp_Id"] + ",";
                }
            }
            for (int i = 0; i < gvEmployee.Rows.Count; i++)
            {
                string[] split = lblSelectRecord.Text.Split(',');
                Label lblconid = (Label)gvEmployee.Rows[i].FindControl("lblEmpId");
                for (int j = 0; j < lblSelectRecord.Text.Split(',').Length; j++)
                {
                    if (lblSelectRecord.Text.Split(',')[j] != "")
                    {
                        if (lblconid.Text.Trim().ToString() == lblSelectRecord.Text.Split(',')[j].Trim().ToString())
                        {
                            ((CheckBox)gvEmployee.Rows[i].FindControl("chkgvSelect")).Checked = true;
                        }
                    }
                }
            }
        }
        else
        {
            lblSelectRecord.Text = "";
            DataTable dtProduct1 = (DataTable)Session["dtEmp"];
            gvEmployee.DataSource = dtProduct1;
            gvEmployee.DataBind();
            ViewState["Select"] = null;
        }

    }



    private string GetTime24(string timepart)
    {
        string str = "00:00";
        DateTime date = Convert.ToDateTime(timepart);
        str = date.ToString("HH:mm");
        return str;
    }

    public string GetPartialViolationMin(string EmpId, DateTime Date,string TimeTableId)
    {
        string PartialMin_Violation = string.Empty;
        int PartialMin = 0;
        int PartialViolationMin = 0;
        bool IsCompPartial = false;
        bool IsEmpPartial = false;
        string PartialInKey = string.Empty;
        string PartialOutKey = string.Empty;
        int PartialAssignMin = 0;
        int PartialMinInDay = 0;
        int CompViolationMin = 0;
        int EmpPartialAssignMin = 0;
        int PartialMinEmp = 0;
        int EmpTotalPartialMin = 0;
        string WithKeyPref = string.Empty;
        WithKeyPref = objAppParam.GetApplicationParameterValueByParamName("With Key Preference", Session["CompId"].ToString());

        DataTable dt = objEmpParam.GetEmployeeParameterByEmpId(EmpId, Session["CompId"].ToString());

        if (dt.Rows.Count > 0)
        {
            IsEmpPartial = Convert.ToBoolean(dt.Rows[0]["Is_Partial_Enable"].ToString());
            PartialAssignMin = Convert.ToInt32(dt.Rows[0]["Partial_Leave_Mins"].ToString());
            PartialMinInDay = Convert.ToInt32(dt.Rows[0]["Partial_Leave_Day"].ToString());
        }

        DataTable dtLogPartial = new DataTable();
        DataTable dtLogPartialIn = new DataTable();
        DataTable dtLogPartialOut = new DataTable();
        DateTime PartialIn = Convert.ToDateTime("1/1/1900");
        DateTime PartialOut = Convert.ToDateTime("1/1/1900");
     
        IsCompPartial = Convert.ToBoolean(objAppParam.GetApplicationParameterValueByParamName("Partial_Leave_Enable", Session["CompId"].ToString()));
        if (IsCompPartial && IsEmpPartial)
        {


           


            DataTable dtLog = new DataTable();



            DataTable dtPartialOffical = objPartialReq.GetPartialLeaveRequestByEmpIdAndCurrentMonthYear(Session["CompId"].ToString(),EmpId, Date.Month.ToString(),Date.Year.ToString());
            dtPartialOffical = new DataView(dtPartialOffical, "Emp_Id='"+EmpId+"' and Partial_Leave_Type<>'0'", "", DataViewRowState.CurrentRows).ToTable();
            DataTable DtReg = objAttReg.GetAttendanceRegDataByMonth_Year_EmpId(EmpId, Date.Month.ToString(), Date.Year.ToString());

            for (int k = 0; k < DtReg.Rows.Count; k++)
            {
                DataTable dt2=new DataView(dtPartialOffical,"Partial_Leave_Date<>'"+DtReg.Rows[k]["Att_Date"].ToString()+"'","",DataViewRowState.CurrentRows).ToTable();
                if (dt2.Rows.Count ==0)
                {
                
                EmpTotalPartialMin += int.Parse(DtReg.Rows[k]["Partial_Min"].ToString());
                }
            }


            dtLog = objAttLog.GetAttendanceLogByDate(Session["CompId"].ToString(), Date.ToString(), Date.ToString());

           


            dtLog = new DataView(dtLog, "Emp_Id='" + EmpId + "'", "", DataViewRowState.CurrentRows).ToTable();



            CompViolationMin = int.Parse(objAppParam.GetApplicationParameterValueByParamName("Partial_Violation_Min", Session["CompId"].ToString()));
            if (PartialAssignMin == 0)
            {
                PartialAssignMin = int.Parse(objAppParam.GetApplicationParameterValueByParamName("Total Partial Leave Minutes", Session["CompId"].ToString()));

            }
            if (PartialMinInDay == 0)
            {
                PartialMinInDay = int.Parse(objAppParam.GetApplicationParameterValueByParamName("Partial Leave Minute Use In A Day", Session["CompId"].ToString()));

            }


            DataTable dtPartialLeave = objPartialReq.GetPartialLeaveRequestByDate(Session["CompId"].ToString(), Date.ToString());

           
            dtPartialLeave = new DataView(dtPartialLeave, "Emp_Id='" + EmpId + "'", "", DataViewRowState.CurrentRows).ToTable();
          


            if (dtPartialLeave.Rows.Count > 0)
            {

                for (int i = 0; i < dtPartialLeave.Rows.Count; i++)
                {
                    EmpPartialAssignMin = 0;
                    EmpPartialAssignMin = GetTimeDifference(Convert.ToDateTime(dtPartialLeave.Rows[i]["From_Time"].ToString()), Convert.ToDateTime(dtPartialLeave.Rows[i]["To_Time"].ToString()));

                    DateTime FromTime = new DateTime(Convert.ToDateTime(dtPartialLeave.Rows[i]["Partial_Leave_Date"]).Year, Convert.ToDateTime(dtPartialLeave.Rows[i]["Partial_Leave_Date"]).Month, Convert.ToDateTime(dtPartialLeave.Rows[i]["Partial_Leave_Date"]).Day, Convert.ToDateTime(dtPartialLeave.Rows[i]["From_Time"].ToString()).Hour, Convert.ToDateTime(dtPartialLeave.Rows[i]["From_Time"].ToString()).Minute, Convert.ToDateTime(dtPartialLeave.Rows[i]["From_Time"].ToString()).Second);
                    DateTime ToTime = new DateTime(Convert.ToDateTime(dtPartialLeave.Rows[i]["Partial_Leave_Date"]).Year, Convert.ToDateTime(dtPartialLeave.Rows[i]["Partial_Leave_Date"]).Month, Convert.ToDateTime(dtPartialLeave.Rows[i]["Partial_Leave_Date"]).Day, Convert.ToDateTime(dtPartialLeave.Rows[i]["To_Time"].ToString()).Hour, Convert.ToDateTime(dtPartialLeave.Rows[i]["To_Time"].ToString()).Minute, Convert.ToDateTime(dtPartialLeave.Rows[i]["To_Time"].ToString()).Second);

                    FromTime = FromTime.AddMinutes(-CompViolationMin);
                   


                    //if shift id not equal to 0 then find shift onduty time and off duty time
                    // then findout partial leave between that shift
                    if (TimeTableId != "0" && TimeTableId != "")
                    {
                        DataTable dtshift = objEmpSch.GetSheduleDescriptionByEmpId(EmpId, Date.ToString());
                        dtshift = new DataView(dtshift, "TimeTable_Id='" + TimeTableId + "'", "", DataViewRowState.CurrentRows).ToTable();
                        if (dtshift.Rows.Count > 0)
                        {
                            DateTime OnDutyTime = new DateTime();
                            DateTime OffDutyTime = new DateTime();

                            OnDutyTime = new DateTime(Date.Year, Date.Month, Date.Day, Convert.ToDateTime(dtshift.Rows[0]["OnDuty_Time"].ToString()).Hour, Convert.ToDateTime(dtshift.Rows[0]["OnDuty_Time"].ToString()).Minute, Convert.ToDateTime(dtshift.Rows[0]["OnDuty_Time"].ToString()).Second);
                            OffDutyTime = new DateTime(Date.Year, Date.Month, Date.Day, Convert.ToDateTime(dtshift.Rows[0]["OffDuty_Time"].ToString()).Hour, Convert.ToDateTime(dtshift.Rows[0]["OffDuty_Time"].ToString()).Minute, Convert.ToDateTime(dtshift.Rows[0]["OffDuty_Time"].ToString()).Second);
                            

                            if(!((FromTime >=OnDutyTime) && (FromTime<=OffDutyTime)))
                            {
                                continue;

                            }

                        }
                    }
                    else
                    {
                        continue;
                    }
                    PartialInKey = objAppParam.GetApplicationParameterValueByParamName("Partial Leave In  Func Key", Session["CompId"].ToString());
                    PartialOutKey = objAppParam.GetApplicationParameterValueByParamName("Partial Leave Out  Func Key", Session["CompId"].ToString());

                    if (PartialInKey != "")
                    {
                        if (WithKeyPref == "Yes")
                        {
                            dtLogPartialIn = new DataView(dtLog, "Func_Code='" + PartialInKey + "'", "Event_Time", DataViewRowState.CurrentRows).ToTable();
                        }
                        else
                        {
                            dtLogPartialIn = new DataView(dtLog, "", "Event_Time", DataViewRowState.CurrentRows).ToTable();
                        }
                        dtLogPartialIn = new DataView(dtLogPartialIn, "Event_Time>='" + FromTime.ToString() + "' and Event_Time<='" + ToTime.ToString() + "'", "", DataViewRowState.CurrentRows).ToTable();

                        if (dtLogPartialIn.Rows.Count > 0)
                        {
                            PartialIn = Convert.ToDateTime(dtLogPartialIn.Rows[0]["Event_Time"].ToString());

                        }

                    }


                    if (PartialOutKey != "")
                    {
                        if (WithKeyPref == "Yes")
                        {
                            dtLogPartialOut = new DataView(dtLog, "Func_Code='" + PartialOutKey + "'", "Event_Time", DataViewRowState.CurrentRows).ToTable();
                        }
                        else
                        {
                            dtLogPartialOut = new DataView(dtLog, "", "Event_Time", DataViewRowState.CurrentRows).ToTable();
                        }


                        dtLogPartialOut = new DataView(dtLogPartialOut, "Event_Time>='" + ToTime + "'", "", DataViewRowState.CurrentRows).ToTable();

                        if (dtLogPartialOut.Rows.Count > 0)
                        {
                            PartialOut = Convert.ToDateTime(dtLogPartialOut.Rows[0]["Event_Time"].ToString());

                        }

                    }


                    if (PartialIn != Convert.ToDateTime("1/1/1900") && PartialOut != Convert.ToDateTime("1/1/1900"))
                    {

                        PartialMin = GetTimeDifference(PartialIn, PartialOut);


                        if ((EmpTotalPartialMin + PartialMin) < PartialAssignMin)
                        {
                            if ((PartialMin) < GetTimeDifference(FromTime, ToTime))
                            {
                                PartialMinEmp += PartialMin;

                            }
                            else
                            {
                                PartialMinEmp += GetTimeDifference(FromTime.AddMinutes(CompViolationMin), ToTime);
                                
                                PartialViolationMin += PartialMin - PartialMinEmp;

                            }

                        }
                        else
                        {
                            PartialMinEmp += GetTimeDifference(FromTime.AddMinutes(CompViolationMin), ToTime);
                            PartialViolationMin += PartialMin - PartialMinEmp;
                           
                        }
                    }
                    else if (PartialIn != Convert.ToDateTime("1/1/1900") && PartialOut == Convert.ToDateTime("1/1/1900"))
                    {
                        if (WithKeyPref == "Yes")
                        {
                            dtLogPartialOut = new DataView(dtLog, "Func_Code='" + PartialOutKey + "'", "Event_Time", DataViewRowState.CurrentRows).ToTable();
                        }
                        if (dtLogPartialOut.Rows.Count > 0)
                        {
                            PartialOut = Convert.ToDateTime(dtLogPartialOut.Rows[dtLogPartialOut.Rows.Count - 1]["Event_Time"].ToString());

                        }
                        if (PartialOut != Convert.ToDateTime("1/1/1900"))
                        {
                            PartialViolationMin += GetTimeDifference(PartialIn, PartialOut);
                            PartialMinEmp += GetTimeDifference(FromTime.AddMinutes(CompViolationMin), ToTime);
                        }
                        else
                        {
                            DataTable dtSch = objEmpSch.GetSheduleDescriptionByEmpId(EmpId, Date.ToString());
                            dtSch = new DataView(dtSch, "", "OffDuty_Time", DataViewRowState.CurrentRows).ToTable();

                            if (dtSch.Rows.Count > 0)
                            {
                                DateTime OffDutyTime = Convert.ToDateTime(dtSch.Rows[dtSch.Rows.Count - 1]["OffDuty_Time"].ToString());


                                OffDutyTime = new DateTime(DateTime.Now.Year, DateTime.Now.Month, DateTime.Now.Day, OffDutyTime.Hour, OffDutyTime.Minute, OffDutyTime.Second);

                                PartialViolationMin += GetTimeDifference(PartialIn, OffDutyTime);
                                PartialMinEmp += GetTimeDifference(FromTime.AddMinutes(CompViolationMin), ToTime);

                            }
                            else
                            {
                                string DefaultShiftId = string.Empty;
                                DataTable dtShift = new DataTable();
                                string OnDutyTime = string.Empty;
                                string OffDutyTime = string.Empty;
                                DefaultShiftId = objAppParam.GetApplicationParameterValueByParamName("Default_Shift", Session["CompId"].ToString());

                                dtShift = objShift.GetShiftDescriptionByShiftId(DefaultShiftId);
                                if (dtShift.Rows.Count > 0)
                                {
                                    OnDutyTime = GetTime24(dtShift.Rows[0]["OnDuty_Time"].ToString());
                                    OffDutyTime = GetTime24(dtShift.Rows[0]["OffDuty_Time"].ToString());

                                    PartialMinEmp += GetTimeDifference(FromTime.AddMinutes(CompViolationMin), ToTime);

                                    PartialViolationMin += GetTimeDifference(PartialIn, Convert.ToDateTime(OffDutyTime));

                                }





                            }



                        }
                    }
                    else if (PartialIn == Convert.ToDateTime("1/1/1900") && PartialOut != Convert.ToDateTime("1/1/1900"))
                    {



                        if (WithKeyPref == "Yes")
                        {
                            dtLogPartialIn = new DataView(dtLog, "Func_Code='" + PartialInKey + "'", "Event_Time", DataViewRowState.CurrentRows).ToTable();
                        }
                        else
                        {
                            dtLogPartialIn = new DataView(dtLog, "", "Event_Time", DataViewRowState.CurrentRows).ToTable();
                        }
                        if (dtLogPartialIn.Rows.Count > 0)
                        {
                            PartialIn = Convert.ToDateTime(dtLogPartialIn.Rows[0]["Event_Time"].ToString());

                        }
                        if (PartialIn != Convert.ToDateTime("1/1/1900"))
                        {
                            PartialViolationMin += GetTimeDifference(PartialIn, PartialOut);
                            PartialMinEmp += GetTimeDifference(FromTime.AddMinutes(CompViolationMin), ToTime);
                        }
                        else
                        {
                            DataTable dtSch = objEmpSch.GetSheduleDescriptionByEmpId(EmpId, Date.ToString());
                            dtSch = new DataView(dtSch, "", "OnDuty_Time", DataViewRowState.CurrentRows).ToTable();


                            DateTime OnDutyTime = Convert.ToDateTime(dtSch.Rows[dtSch.Rows.Count - 1]["OnDuty_Time"].ToString());


                            OnDutyTime = new DateTime(DateTime.Now.Year, DateTime.Now.Month, DateTime.Now.Day, OnDutyTime.Hour, OnDutyTime.Minute, OnDutyTime.Second);



                            if (dtSch.Rows.Count > 0)
                            {
                                PartialViolationMin += GetTimeDifference(OnDutyTime, PartialOut);
                                PartialMinEmp += GetTimeDifference(FromTime.AddMinutes(CompViolationMin), ToTime);
                            }
                            else
                            {

                                string DefaultShiftId = string.Empty;
                                DataTable dtShift = new DataTable();
                                string OnDutyTime1 = string.Empty;
                                string OffDutyTime1 = string.Empty;
                                DefaultShiftId = objAppParam.GetApplicationParameterValueByParamName("Default_Shift", Session["CompId"].ToString());

                                dtShift = objShift.GetShiftDescriptionByShiftId(DefaultShiftId);
                                if (dtShift.Rows.Count > 0)
                                {
                                    OnDutyTime1 = GetTime24(dtShift.Rows[0]["OnDuty_Time"].ToString());
                                    OffDutyTime1 = GetTime24(dtShift.Rows[0]["OffDuty_Time"].ToString());

                                    PartialMinEmp += GetTimeDifference(FromTime.AddMinutes(CompViolationMin), ToTime);

                                    PartialViolationMin += GetTimeDifference(Convert.ToDateTime(OnDutyTime1), PartialOut);
                                }




                            }


                        }
                    }

                    else 
                    {
                        if (WithKeyPref == "Yes")
                        {
                            dtLogPartialIn = new DataView(dtLog, "Func_Code='" + PartialInKey + "'", "Event_Time", DataViewRowState.CurrentRows).ToTable();
                        }
                        else
                        {
                            dtLogPartialIn = new DataView(dtLog, "", "Event_Time", DataViewRowState.CurrentRows).ToTable();
                        }
                        if (dtLogPartialIn.Rows.Count > 0)
                        {
                            PartialIn = Convert.ToDateTime(dtLogPartialIn.Rows[0]["Event_Time"].ToString());

                        }
                        if (WithKeyPref == "Yes")
                        {
                            dtLogPartialOut = new DataView(dtLog, "Func_Code='" + PartialOutKey + "'", "Event_Time", DataViewRowState.CurrentRows).ToTable();
                        }
                        else
                        {
                            dtLogPartialOut = new DataView(dtLog, "", "Event_Time", DataViewRowState.CurrentRows).ToTable();
                        }
                        if (dtLogPartialOut.Rows.Count > 0)
                        {
                            PartialOut = Convert.ToDateTime(dtLogPartialOut.Rows[dtLogPartialOut.Rows.Count - 1]["Event_Time"].ToString());

                        }


                        if (PartialIn != Convert.ToDateTime("1/1/1900") && PartialOut != Convert.ToDateTime("1/1/1900"))
                        {
                            PartialMin = GetTimeDifference(PartialIn, PartialOut);


                            if ((EmpTotalPartialMin + PartialMin) < PartialAssignMin)
                            {

                                if ((PartialMin) > PartialAssignMin)
                                {
                                    if ((PartialMin ) < PartialMinInDay)
                                    {
                                        PartialMinEmp += PartialMin;
                                    }
                                    else
                                    {
                                        PartialMinEmp += GetTimeDifference(FromTime.AddMinutes(CompViolationMin), ToTime);
                                        PartialViolationMin += (PartialMin ) - PartialMinInDay;
                                    }

                                }
                                else
                                {

                                    PartialViolationMin += PartialMin;
                                    PartialMinEmp += GetTimeDifference(FromTime.AddMinutes(CompViolationMin), ToTime);

                                }



                            }
                            else
                            {


                                PartialMinEmp += PartialMin;
                                PartialViolationMin += PartialMin - (PartialAssignMin - EmpTotalPartialMin);
                            }
                        }


                    }

                  
                   

                }
            }
        }

        if (PartialViolationMin<0)
        {
            PartialViolationMin = 0;

        }
        PartialMin_Violation = PartialMinEmp + "-" + PartialViolationMin;

        return PartialMin_Violation;
    }


    public double GetOTOneMinSalary(string EmpId, string OTType, double PerMinSal)
    {
        double OneMinSal = 0;

        string Normal_OT_Type = "";
        double Normal_OT_Value = 0;
        string Normal_HOT_Type = "";
        double Normal_HOT_Value = 0;
        string Normal_WOT_Type = "";
        double Normal_WOT_Value = 0;
        double BasicSalary = 0;
        double AssignMin = 0;

        //2 means per
        try
        {
            BasicSalary = double.Parse(objEmpParam.GetEmployeeParameterByParameterName(EmpId, "Basic_Salary"));
            AssignMin = double.Parse(objEmpParam.GetEmployeeParameterByParameterName(EmpId, "Assign_Min"));
        }
        catch
        {

        }
        if (OTType == "Normal")
        {
            try
            {
                Normal_OT_Type = objEmpParam.GetEmployeeParameterByParameterName(EmpId, "Normal_OT_Type");
                Normal_OT_Value = double.Parse(objEmpParam.GetEmployeeParameterByParameterName(EmpId, "Normal_OT_Value"));
            }
            catch
            {
            }
            if (Normal_OT_Type == "2")
            {
                OneMinSal = (PerMinSal * Normal_OT_Value) / 100;

            }
            else
            {
                OneMinSal = (PerMinSal * Normal_OT_Value) / 60;
            }
        }
        else if (OTType == "WeekOff")
        {
            try
            {
                Normal_WOT_Type = objEmpParam.GetEmployeeParameterByParameterName(EmpId, "Normal_WOT_Type");
                Normal_WOT_Value = double.Parse(objEmpParam.GetEmployeeParameterByParameterName(EmpId, "Normal_WOT_Value"));
            }
            catch
            {
            }
            if (Normal_WOT_Type == "2")
            {
                OneMinSal = (PerMinSal * Normal_WOT_Value) / 100;

            }
            else
            {
                OneMinSal = (PerMinSal * Normal_WOT_Value) / 60;
            }
        }
        else if (OTType == "Holiday")
        {
            try
            {
                Normal_HOT_Type = objEmpParam.GetEmployeeParameterByParameterName(EmpId, "Normal_HOT_Type");
                Normal_HOT_Value = double.Parse(objEmpParam.GetEmployeeParameterByParameterName(EmpId, "Normal_HOT_Value"));
            }
            catch
            {
            }
            if (Normal_HOT_Type == "2")
            {
                OneMinSal = (PerMinSal * Normal_HOT_Value) / 100;

            }
            else
            {
                OneMinSal = (PerMinSal * Normal_HOT_Value) / 60;
            }
        }

        return OneMinSal;


    }


    public double OnDayAbsentSalary(double PerMinSal,string EmpId)
    {
        double absentsal = 0;

        string AbsentType = string.Empty;
        double Value = 0;
        AbsentType = objAppParam.GetApplicationParameterValueByParamName("Absent_Type", Session["CompId"].ToString());
        Value = double.Parse(objAppParam.GetApplicationParameterValueByParamName("Absent_Value", Session["CompId"].ToString()));
        bool IsEmpAbsent = false;
        try
        {
            IsEmpAbsent = Convert.ToBoolean(objEmpParam.GetEmployeeParameterByParameterName(EmpId, "Field3"));

        }
        catch
        {

        }
        if (IsEmpAbsent)
        {
           
                if (AbsentType == "2")
                {
                    absentsal = (PerMinSal * Value) / 100;

                }
                else if (AbsentType == "1")
                {
                    absentsal = Value / 60;
                }
            
        }

       

        return absentsal;

    }
    public double OnMinuteLatePenalty(double PerMinSal,string EmpId)
    {
        double sal = 0;
        string Method = string.Empty;
        string Type = string.Empty;
        double Value = 0;
        bool IsEmpLate = false;
        try
        {
            IsEmpLate = Convert.ToBoolean(objEmpParam.GetEmployeeParameterByParameterName(EmpId, "Field1"));
        }
        catch
        {
        }


          Method = objAppParam.GetApplicationParameterValueByParamName("Is_Late_Penalty_Method", Session["CompId"].ToString());

        if (IsEmpLate)
        {
            if (Method == "Salary")
            {
                Type = objAppParam.GetApplicationParameterValueByParamName("Is_Late_Penalty_Salary_Type", Session["CompId"].ToString());
                Value = double.Parse(objAppParam.GetApplicationParameterValueByParamName("Is_Late_Penalty_Salary_Value", Session["CompId"].ToString()));
      
                if (Type == "2")
                {
                    sal = (PerMinSal * Value) / 100;

                }
                else
                {
                    sal = Value / 60;
                }
            }
            else
            {
                sal = PerMinSal;
            }
        }
        return sal;

    }





    public double OnMinuteEarlyPenalty(double PerMinSal,string EmpId)
    {
        double sal = 0;

        string Type = string.Empty;
        string Method = string.Empty;
        double Value = 0;
        bool IsEmpEarly = false;
        try
        {
            IsEmpEarly = Convert.ToBoolean(objEmpParam.GetEmployeeParameterByParameterName(EmpId, "Field2"));
        }
        catch
        {

        }


         Method = objAppParam.GetApplicationParameterValueByParamName("Is_Early_Penalty_Method", Session["CompId"].ToString());
        if (IsEmpEarly)
        {
            if (Method == "Salary")
            {
                Type = objAppParam.GetApplicationParameterValueByParamName("Is_Early_Penalty_Salary_Type", Session["CompId"].ToString());
                Value = double.Parse(objAppParam.GetApplicationParameterValueByParamName("Is_Early_Penalty_Salary_Value", Session["CompId"].ToString()));
      
                if (Type == "2")
                {
                    sal = (PerMinSal * Value) / 100;

                }
                else
                {
                    sal = Value / 60;
                }
            }
            else
            {
                sal = PerMinSal;
            }
        }
        return sal;

    }




    public double OnMinuteParialPenalty(double PerMinSal,string EmpId)
    {
        double sal = 0;
        string Method = string.Empty;
        string Type = string.Empty;
        double Value = 0;
        bool IsEmpPartial = false;
        try
        {
            IsEmpPartial = Convert.ToBoolean(objEmpParam.GetEmployeeParameterByParameterName(EmpId, "Is_Partial_Enable"));
        }
        catch
        {
        }
        
         Method = objAppParam.GetApplicationParameterValueByParamName("Partial_Penalty_Method", Session["CompId"].ToString());
        if (IsEmpPartial)
        {
            if (Method == "Salary")
            {
                Type = objAppParam.GetApplicationParameterValueByParamName("Partial_Penalty_Salary_Type", Session["CompId"].ToString());
                Value = double.Parse(objAppParam.GetApplicationParameterValueByParamName("Partial_Penalty_Salary_Value", Session["CompId"].ToString()));
       
                if (Type == "2")
                {
                    sal = (PerMinSal * Value) / 100;

                }
                else
                {
                    sal = Value / 60;
                }
            }
            else
            {
                sal = PerMinSal;
            }
        }
        return sal;

    }
    protected void btnLogPost_Click(object sender, EventArgs e)
    {
        int b = 0;
        string empidlist = lblSelectRecord.Text;


        if (ddlMonth.SelectedIndex == 0)
        {
            DisplayMessage("Please select month");
            ddlMonth.Focus();
            return;

        }
        if (txtYear.Text == "")
        {
            DisplayMessage("Please enter year");
            txtYear.Focus();
            return;

        }


        int Total_Days = 0;
        int Days_In_WorkMin = 0;
        int Worked_Days = 0;
        int Week_Off_Days = 0;
        int Holiday_Days = 0;
        int Leave_Days = 0;
        int Absent_Days = 0;
        int Assigned_Worked_Min = 0;


        double Basic_Salary = 0;
        double Basic_Min_Salary = 0;
        double Normal_OT_Salary = 0;
        double Week_Off_OT_Salary = 0;
        double Holiday_OT_Salary = 0;
        double Absent_Penalty = 0;
        double Late_Penalty_Min = 0;
        double Early_Penalty_Min = 0;
        double Partial_Penalty_Min = 0;
        int Total_Worked_Min = 0;
        int Holiday_OT_Min = 0;
        int Week_Off_OT_Min = 0;
        int Normal_OT_Min = 0;
        int Late_Min = 0;
        int Early_Min = 0;
        int Partial_Min = 0;
        double Basic_Work_Salary = 0;
        double Normal_OT_Work_Salary = 0;
        double WeekOff_OT_Work_Salary = 0;
        double Holiday_OT_Work_Salary = 0;
        double Week_Off_Days_Salary = 0;
        double Holiday_Days_Salary = 0;
        double Leave_Days_Salary = 0;
        double Absent_Day_Penalty = 0;
        double Late_Min_Penalty = 0;
        double Early_Min_Penalty = 0;
        double Parital_Violation_Penalty = 0;


        bool IsEmpLate = false;
        bool IsEmpEarly = false;
        bool IsEmpPartial = false;
        string LateMethod = string.Empty;
        string EarlyMethod = string.Empty;
        string PartialMethod = string.Empty;
        int LatePenaltyDedMin =0;
        int EarlyPenaltyDedMin =0;
        int PartialPenaltyDedMin = 0;

        DateTime Posted_Date = DateTime.Now;


        DataTable dtAttReg = new DataTable();
        DataTable dtTemp = new DataTable();
       
          if (rbtnEmpSal.Checked)
        {
            if (empidlist == "")
            {
                DisplayMessage("Select Atleast One Employee");
                return;
            }


        }
        else
        {
            string GroupIds = string.Empty;
            string EmpIds = string.Empty;
            empidlist = "";

            for (int i = 0; i < lbxGroupSal.Items.Count; i++)
            {
                if (lbxGroupSal.Items[i].Selected)
                {
                    GroupIds += lbxGroupSal.Items[i].Value + ",";
                }

            }

            if (GroupIds != "")
            {
                DataTable dtEmp = objEmp.GetEmployeeMaster(Session["CompId"].ToString());

                dtEmp = new DataView(dtEmp, "Brand_Id='" + Session["BrandId"].ToString() + "'  and Location_Id='" + Session["LocId"].ToString() + "'", "", DataViewRowState.CurrentRows).ToTable();
                DataTable dtEmpInGroup = objGroupEmp.GetGroup_EmployeeById(Session["CompId"].ToString());

                dtEmpInGroup = new DataView(dtEmpInGroup, "Group_Id in(" + GroupIds.Substring(0, GroupIds.Length - 1) + ")", "", DataViewRowState.CurrentRows).ToTable();

                for (int i = 0; i < dtEmpInGroup.Rows.Count; i++)
                {
                    if (!EmpIds.Split(',').Contains(dtEmpInGroup.Rows[i]["Emp_Id"].ToString()))
                    {
                        EmpIds += dtEmpInGroup.Rows[i]["Emp_Id"].ToString() + ",";
                    }
                }



                foreach (string str in EmpIds.Split(','))
                {
                    if (str != "")
                    {
                        empidlist += str+",";

                    }
                }

                if (empidlist == "")
                {
                    DisplayMessage("Employees are not exists in selected groups");
                    return;
                }
            }
            else
            {
                DisplayMessage("Select Group First");
            }

        }










            for (int i = 0; i < empidlist.Split(',').Length; i++)
            {
                if (empidlist.Split(',')[i] == "")
                {
                    continue;
                }


                 Total_Days = 0;
                 Days_In_WorkMin = 0;
                 Worked_Days = 0;
                 Week_Off_Days = 0;
                 Holiday_Days = 0;
                 Leave_Days = 0;
                 Absent_Days = 0;
                 Assigned_Worked_Min = 0;


                 Basic_Salary = 0;
                 Basic_Min_Salary = 0;
                 Normal_OT_Salary = 0;
                 Week_Off_OT_Salary = 0;
                 Holiday_OT_Salary = 0;
                 Absent_Penalty = 0;
                 Late_Penalty_Min = 0;
                 Early_Penalty_Min = 0;
                 Partial_Penalty_Min = 0;
                 Total_Worked_Min = 0;
                 Holiday_OT_Min = 0;
                 Week_Off_OT_Min = 0;
                 Normal_OT_Min = 0;
                 Late_Min = 0;
                 Early_Min = 0;
                 Partial_Min = 0;
                 Basic_Work_Salary = 0;
                 Normal_OT_Work_Salary = 0;
                 WeekOff_OT_Work_Salary = 0;
                 Holiday_OT_Work_Salary = 0;
                 Week_Off_Days_Salary = 0;
                 Holiday_Days_Salary = 0;
                 Leave_Days_Salary = 0;
                 Absent_Day_Penalty = 0;
                 Late_Min_Penalty = 0;
                 Early_Min_Penalty = 0;
                 Parital_Violation_Penalty = 0;


                 IsEmpLate = false;
                 IsEmpEarly = false;
                 IsEmpPartial = false;
                 LateMethod = string.Empty;
                 EarlyMethod = string.Empty;
                 PartialMethod = string.Empty;
                 LatePenaltyDedMin = 0;
                 EarlyPenaltyDedMin = 0;
                 PartialPenaltyDedMin = 0;
                 objPayEmpAtt.DeletePayEmployeeAttendance(empidlist.Split(',')[i].ToString(), ddlMonth.SelectedValue, txtYear.Text);

                try
                {
                IsEmpEarly = Convert.ToBoolean(objEmpParam.GetEmployeeParameterByParameterName(empidlist.Split(',')[i], "Field2"));
                IsEmpLate = Convert.ToBoolean(objEmpParam.GetEmployeeParameterByParameterName(empidlist.Split(',')[i], "Field1"));
                IsEmpPartial = Convert.ToBoolean(objEmpParam.GetEmployeeParameterByParameterName(empidlist.Split(',')[i], "Is_Partial_Enable"));

                LateMethod = objAppParam.GetApplicationParameterValueByParamName("Is_Late_Penalty_Method",Session["CompId"].ToString());
                EarlyMethod = objAppParam.GetApplicationParameterValueByParamName("Is_Early_Penalty_Method",Session["CompId"].ToString());
                PartialMethod = objAppParam.GetApplicationParameterValueByParamName("Partial_Penalty_Method", Session["CompId"].ToString());
                LatePenaltyDedMin = int.Parse(objAppParam.GetApplicationParameterValueByParamName("Late_Penalty_Min_Deduct", Session["CompId"].ToString()));
                EarlyPenaltyDedMin = int.Parse(objAppParam.GetApplicationParameterValueByParamName("Early_Penalty_Min_Deduct", Session["CompId"].ToString()));
                PartialPenaltyDedMin = int.Parse(objAppParam.GetApplicationParameterValueByParamName("Partial_Penalty_Min_Deduct", Session["CompId"].ToString()));

                
              

               
                dtAttReg = objAttReg.GetAttendanceRegDataByMonth_Year_EmpId(empidlist.Split(',')[i].ToString(), ddlMonth.SelectedValue, txtYear.Text);

                Total_Days = DateTime.DaysInMonth(int.Parse(txtYear.Text), int.Parse(ddlMonth.SelectedValue));

                Days_In_WorkMin = int.Parse(objEmpParam.GetEmployeeParameterByParameterName(empidlist.Split(',')[i].ToString(), "Assign_Min"));
                }
                catch
                {

                }
                try
                {
                    dtTemp = new DataView(dtAttReg, "Is_Week_Off='True'", "", DataViewRowState.CurrentRows).ToTable();

                    Week_Off_Days = dtTemp.Rows.Count;

                    dtTemp = new DataTable();
                    dtTemp = new DataView(dtAttReg, "Is_Holiday='True'", "", DataViewRowState.CurrentRows).ToTable();

                    Holiday_Days = dtTemp.Rows.Count;

                    dtTemp = new DataTable();
                    dtTemp = new DataView(dtAttReg, "Is_Leave='True'", "", DataViewRowState.CurrentRows).ToTable();

                    Leave_Days = dtTemp.Rows.Count;
                    dtTemp = new DataTable();
                    dtTemp = new DataView(dtAttReg, "Is_Absent='True'", "", DataViewRowState.CurrentRows).ToTable();

                    Absent_Days = dtTemp.Rows.Count;

                    Worked_Days = Total_Days - (Week_Off_Days + Holiday_Days + Leave_Days + Absent_Days);

                    Assigned_Worked_Min = Days_In_WorkMin * Total_Days;
                }
                catch
                {
                }
                try
                {
                    Basic_Salary = double.Parse(objEmpParam.GetEmployeeParameterByParameterName(empidlist.Split(',')[i].ToString(), "Basic_Salary"));
                }
                catch
                {

                }

                Basic_Min_Salary = Basic_Salary / (Total_Days * Days_In_WorkMin);
                if (Basic_Min_Salary.ToString()=="NaN")
                {
                    Basic_Min_Salary = 0;

                }
                Normal_OT_Salary = GetOTOneMinSalary(empidlist.Split(',')[i].ToString(), "Normal", Basic_Min_Salary);
                Week_Off_OT_Salary = GetOTOneMinSalary(empidlist.Split(',')[i].ToString(), "WeekOff", Basic_Min_Salary);
                Holiday_OT_Salary = GetOTOneMinSalary(empidlist.Split(',')[i].ToString(), "Holiday", Basic_Min_Salary);

                Absent_Penalty = OnDayAbsentSalary(Basic_Min_Salary, empidlist.Split(',')[i].ToString());

                Late_Penalty_Min = OnMinuteLatePenalty(Basic_Min_Salary, empidlist.Split(',')[i].ToString());

                Early_Penalty_Min = OnMinuteEarlyPenalty(Basic_Min_Salary, empidlist.Split(',')[i].ToString());

                Partial_Penalty_Min = OnMinuteParialPenalty(Basic_Min_Salary, empidlist.Split(',')[i].ToString());

                int TotalEffectWorkMin = 0;
                try
                {
                    dtTemp = new DataTable();
                    dtTemp = new DataView(dtAttReg, "EffectiveWork_Min<>0", "", DataViewRowState.CurrentRows).ToTable();


                    for (int k = 0; k < dtTemp.Rows.Count; k++)
                    {
                        TotalEffectWorkMin += int.Parse(dtTemp.Rows[k]["EffectiveWork_Min"].ToString());


                    }
                }
                catch
                {

                }

                Total_Worked_Min = TotalEffectWorkMin;
                int TotalHolidayMin = 0;

                try
                {
                    dtTemp = new DataTable();
                    dtTemp = new DataView(dtAttReg, "Holiday_Min<>0", "", DataViewRowState.CurrentRows).ToTable();


                  
                    for (int k = 0; k < dtTemp.Rows.Count; k++)
                    {
                        TotalHolidayMin += int.Parse(dtTemp.Rows[k]["Holiday_Min"].ToString());


                    }
                }
                catch
                {

                }

                Holiday_OT_Min = TotalHolidayMin;

              
                int WeekOffMin = 0;
                try
                {
                    dtTemp = new DataTable();
                    dtTemp = new DataView(dtAttReg, "Week_Off_Min<>0", "", DataViewRowState.CurrentRows).ToTable();

                    for (int k = 0; k < dtTemp.Rows.Count; k++)
                    {
                        WeekOffMin += int.Parse(dtTemp.Rows[k]["Week_Off_Min"].ToString());


                    }

                }
                catch
                {

                }
                Week_Off_OT_Min = WeekOffMin;
                int NormalOtMin = 0;
                try
                {
                    dtTemp = new DataTable();
                    dtTemp = new DataView(dtAttReg, "OverTime_Min<>0", "", DataViewRowState.CurrentRows).ToTable();

                  
                    for (int k = 0; k < dtTemp.Rows.Count; k++)
                    {
                        NormalOtMin += int.Parse(dtTemp.Rows[k]["OverTime_Min"].ToString());


                    }
                }
                catch
                {
                }

                Normal_OT_Min = NormalOtMin;


                int LateMin = 0;
                try
                {
                    dtTemp = new DataTable();
                    dtTemp = new DataView(dtAttReg, "LateMin<>'0'", "", DataViewRowState.CurrentRows).ToTable();

                    for (int k = 0; k < dtTemp.Rows.Count; k++)
                    {
                        LateMin += int.Parse(dtTemp.Rows[k]["LateMin"].ToString());


                    }
                }
                catch
                {

                }
                if(IsEmpLate && LateMethod=="Min")
                {
                    Late_Min = LateMin * LatePenaltyDedMin;

                }
                    else
                    {
                Late_Min = LateMin;
                    }
              
                int EarlyMin = 0;
                try
                {
                    dtTemp = new DataTable();
                    dtTemp = new DataView(dtAttReg, "EarlyMin<>'0'", "", DataViewRowState.CurrentRows).ToTable();

                    for (int k = 0; k < dtTemp.Rows.Count; k++)
                    {
                        EarlyMin += int.Parse(dtTemp.Rows[k]["EarlyMin"].ToString());


                    }
                }
                catch
                {

                }
                if (IsEmpEarly && EarlyMethod== "Min")
                {
                    Early_Min = EarlyMin * EarlyPenaltyDedMin;

                }
                else
                {
                    Early_Min = EarlyMin;
                }
                int PartialMin = 0;
                try
                {
                    dtTemp = new DataTable();
                    dtTemp = new DataView(dtAttReg, "Partial_Violation_Min<>'0'", "", DataViewRowState.CurrentRows).ToTable();

                  
                    for (int k = 0; k < dtTemp.Rows.Count; k++)
                    {
                        PartialMin += int.Parse(dtTemp.Rows[k]["Partial_Violation_Min"].ToString());


                    }

                }
                catch
                {
                }

                if (IsEmpPartial && PartialMethod == "Min")
                {
                    Partial_Min = PartialMin * PartialPenaltyDedMin;

                }
                else
                {
                    Partial_Min = PartialMin;
                }
               

                Basic_Work_Salary = Basic_Min_Salary * Total_Worked_Min;

                Normal_OT_Work_Salary = Normal_OT_Salary * Normal_OT_Min;
                WeekOff_OT_Work_Salary = Week_Off_OT_Salary * Week_Off_OT_Min;
                Holiday_OT_Work_Salary = Holiday_OT_Salary * Holiday_OT_Min;

                Holiday_Days_Salary = (Basic_Salary * Holiday_Days) / Total_Days;
                if (Holiday_Days_Salary.ToString() == "NaN")
                {
                    Holiday_Days_Salary = 0;

                }

                Week_Off_Days_Salary = (Basic_Salary * Week_Off_Days) / Total_Days;

                if (Week_Off_Days_Salary.ToString() == "NaN")
                {
                    Week_Off_Days_Salary = 0;

                }
                Leave_Days_Salary = (Basic_Salary * Leave_Days) / Total_Days;
                if (Leave_Days_Salary.ToString() == "NaN")
                {
                    Leave_Days_Salary = 0;

                }

                Late_Min_Penalty = Late_Min * Late_Penalty_Min;
                Early_Min_Penalty = Early_Penalty_Min * Early_Min;
                Parital_Violation_Penalty = Partial_Penalty_Min * PartialMin;
                Absent_Day_Penalty = Absent_Penalty * (Absent_Days * Days_In_WorkMin);

                Basic_Work_Salary = Basic_Work_Salary - Late_Min_Penalty - Early_Min_Penalty - Parital_Violation_Penalty - Absent_Day_Penalty;

                objPayEmpAtt.InsertPayEmployeeAttendance(Session["CompId"].ToString(), empidlist.Split(',')[i].ToString(), ddlMonth.SelectedValue, txtYear.Text, Total_Days.ToString(), Days_In_WorkMin.ToString(), Worked_Days.ToString(), Week_Off_Days.ToString(), Holiday_Days.ToString(), Leave_Days.ToString(), Absent_Days.ToString(), Assigned_Worked_Min.ToString(), Basic_Salary.ToString(), Basic_Min_Salary.ToString(), Normal_OT_Salary.ToString(), Week_Off_OT_Salary.ToString(), Holiday_OT_Salary.ToString(), Absent_Penalty.ToString(), Late_Penalty_Min.ToString(), Early_Penalty_Min.ToString(), Partial_Penalty_Min.ToString(), Total_Worked_Min.ToString(), Holiday_OT_Min.ToString(), Week_Off_OT_Min.ToString(), Normal_OT_Min.ToString(), Late_Min.ToString(), Early_Min.ToString(), Partial_Min.ToString(), Basic_Work_Salary.ToString(), Normal_OT_Work_Salary.ToString(), WeekOff_OT_Work_Salary.ToString(), Holiday_OT_Work_Salary.ToString(), Week_Off_Days_Salary.ToString(), Holiday_Days_Salary.ToString(), Leave_Days_Salary.ToString(), Absent_Day_Penalty.ToString(), Late_Min_Penalty.ToString(), Early_Min_Penalty.ToString(), Parital_Violation_Penalty.ToString(), Posted_Date.ToString(), "", "", "", "", "", "", "", "", "", "", true.ToString(), Session["UserId"].ToString(), DateTime.Now.ToString(), Session["UserId"].ToString(), DateTime.Now.ToString());

            }


            DisplayMessage("Log Posted");


        



    }
    protected void btnLogProcess_Click(object sender, EventArgs e)
    {


        int b = 0;
        // Selected Emp Id 
        string empidlist = lblSelectRecord.Text;

        // Validation on Month And Year
        if (ddlMonth.SelectedIndex == 0)
        {
            DisplayMessage("Please select month");
            ddlMonth.Focus();
            return;

        }
        if (txtYear.Text == "")
        {
            DisplayMessage("Please enter year");
            txtYear.Focus();
            return;

        }

        // Make Date From to To
        txtFromDate.Text = new DateTime(int.Parse(txtYear.Text), int.Parse(ddlMonth.SelectedValue), 1).ToString("dd-MMM-yyyy");
        txtToDate.Text = new DateTime(int.Parse(txtYear.Text), int.Parse(ddlMonth.SelectedValue), DateTime.DaysInMonth(int.Parse(txtYear.Text), int.Parse(ddlMonth.SelectedValue))).ToString("dd-MMM-yyyy");




        string OnDutyTime = string.Empty;
        string OffDutyTime = string.Empty;
        string BeginingIn = string.Empty;
        string BeginingOut = string.Empty;
        string EndingIn = string.Empty;
        string EndingOut = string.Empty;
        DateTime BIn = new DateTime();
        DateTime BOut = new DateTime();
        DateTime EIn = new DateTime();
        DateTime EOut = new DateTime();
        DateTime OnTime = new DateTime();
        DateTime OffTime = new DateTime();
        string PartialInKey = string.Empty;
        string PartialOutKey = string.Empty;
        string InKey = string.Empty;
        string OutKey = string.Empty;
        string BreakInKey = string.Empty;
        string BreakOutKey = string.Empty;

        string WeekOff = string.Empty;
        string WorkCalMethod = string.Empty;

        DataTable dtTimeTable = new DataTable();
        string InTime = string.Empty;
        string OutTime = string.Empty;
        DataTable TempIn = new DataTable();
        DataTable TempOut = new DataTable();
        DataTable TempInOut = new DataTable();
        bool IsWeekOff = false;
        bool IsHoliday = false;
        bool IsLeave = false;
        bool IsAbsent = false;
        bool IsTempShift = false;
        bool IsLate = false;
        bool IsEarlyOut = false;
        bool IsOverTime = true;
        bool IsCompOT = false;


        int EffectiveWorkMin = 0;
        int WorkMin = 0;
        int BreakMin = 0;
        int AssignMin = 0;
        int MaxOt = 0;
        int MinOt = 0;
        int OverTimeMin = 0;
        int LateMin = 0;
        int LateRelaxMin = 0;
        int LatePenaltyMin = 0;
        int EarlyMin = 0;
        int EarlyRelaxMin = 0;
        int EarlyPenaltyMin = 0;
        string Partial_Min_Deduct = string.Empty;
        DataTable dtSchedule = new DataTable();
        string DefaultShiftId = string.Empty;
        string PartialMethod = string.Empty;
        bool NoClockIn = false;
        bool NoClockOut = false;
        string WithKeyPref = string.Empty;
        WithKeyPref = objAppParam.GetApplicationParameterValueByParamName("With Key Preference", Session["CompId"].ToString());

        NoClockIn = Convert.ToBoolean(objAppParam.GetApplicationParameterValueByParamName("No_Clock_In_CountAsAbsent", Session["CompId"].ToString()));
        NoClockOut = Convert.ToBoolean(objAppParam.GetApplicationParameterValueByParamName("No_Clock_Out_CountAsAbsent", Session["CompId"].ToString()));


        PartialMethod = objAppParam.GetApplicationParameterValueByParamName("Partial_Penalty_Method", Session["CompId"].ToString());


        Partial_Min_Deduct = objAppParam.GetApplicationParameterValueByParamName("Partial_Penalty_Min_Deduct", Session["CompId"].ToString());
        DefaultShiftId = objAppParam.GetApplicationParameterValueByParamName("Default_Shift", Session["CompId"].ToString());
        // Company Level Over Time Parameter
        IsCompOT = Convert.ToBoolean(objAppParam.GetApplicationParameterValueByParamName("IsOverTime", Session["CompId"].ToString()));
        MaxOt = int.Parse(objAppParam.GetApplicationParameterValueByParamName("Max Over Time Min", Session["CompId"].ToString()));
        MinOt = int.Parse(objAppParam.GetApplicationParameterValueByParamName("Min OVer Time Min", Session["CompId"].ToString()));
        WeekOff = objAppParam.GetApplicationParameterValueByParamName("Week Off Days", Session["CompId"].ToString());
        PartialInKey = objAppParam.GetApplicationParameterValueByParamName("Partial Leave In  Func Key", Session["CompId"].ToString());
        PartialOutKey = objAppParam.GetApplicationParameterValueByParamName("Partial Leave Out  Func Key", Session["CompId"].ToString());


        InKey = objAppParam.GetApplicationParameterValueByParamName("In Func Key", Session["CompId"].ToString());
        OutKey = objAppParam.GetApplicationParameterValueByParamName("Out Func Key", Session["CompId"].ToString());
        BreakInKey = objAppParam.GetApplicationParameterValueByParamName("Break In Func Key", Session["CompId"].ToString());
        BreakOutKey = objAppParam.GetApplicationParameterValueByParamName("Break Out Func Key", Session["CompId"].ToString());

        DataTable dtShift = new DataTable();
        dtShift = objShift.GetShiftDescriptionByShiftId(DefaultShiftId);

        if (rbtnEmpSal.Checked)
        {
            if (empidlist == "")
            {
                DisplayMessage("Select Atleast One Employee");
                return;
            }


        }
        else
        {
            string GroupIds = string.Empty;
            string EmpIds = string.Empty;


            for (int i = 0; i < lbxGroupSal.Items.Count; i++)
            {
                if (lbxGroupSal.Items[i].Selected)
                {
                    GroupIds += lbxGroupSal.Items[i].Value + ",";
                }

            }

            if (GroupIds != "")
            {
                empidlist = "";
                DataTable dtEmp = objEmp.GetEmployeeMaster(Session["CompId"].ToString());

                dtEmp = new DataView(dtEmp, "Brand_Id='" + Session["BrandId"].ToString() + "'  and Location_Id='" + Session["LocId"].ToString() + "'", "", DataViewRowState.CurrentRows).ToTable();
                DataTable dtEmpInGroup = objGroupEmp.GetGroup_EmployeeById(Session["CompId"].ToString());

                dtEmpInGroup = new DataView(dtEmpInGroup, "Group_Id in(" + GroupIds.Substring(0, GroupIds.Length - 1) + ")", "", DataViewRowState.CurrentRows).ToTable();

                for (int i = 0; i < dtEmpInGroup.Rows.Count; i++)
                {
                    if (!EmpIds.Split(',').Contains(dtEmpInGroup.Rows[i]["Emp_Id"].ToString()))
                    {
                        EmpIds += dtEmpInGroup.Rows[i]["Emp_Id"].ToString() + ",";
                    }
                }



                foreach (string str in EmpIds.Split(','))
                {
                    if (str != "")
                    {
                        empidlist += str+",";

                    }
                }

                if (empidlist == "")
                {
                    DisplayMessage("Employees are not exists in selected groups");
                    return;
                }
            }
            else
            {
                DisplayMessage("Select Group First");
            }

        }

           
       
            for (int i = 0; i < empidlist.Split(',').Length; i++)
            {
                if (empidlist.Split(',')[i] == "")
                {
                    continue;
                }

                DataTable dtLog = new DataTable();
                DataTable dtPartial = new DataTable();
                DataTable dtBreakInOut = new DataTable();
                DataTable dtPartialLog = new DataTable();
                DateTime FromDate = Convert.ToDateTime(txtFromDate.Text);
                DateTime ToDate = Convert.ToDateTime(txtToDate.Text);

                dtLog = objAttLog.GetAttendanceLogByDateByEmpId(empidlist.Split(',')[i], FromDate.AddDays(-1).ToString(), ToDate.AddDays(1).ToString());

                if(WithKeyPref=="Yes")
                {

                    dtBreakInOut = new DataView(dtLog, "Func_Code in('" + BreakInKey + "','" + BreakOutKey + "') ", "", DataViewRowState.CurrentRows).ToTable();
          
                if (PartialInKey != "" && PartialOutKey != "")
                {
                    // Avoid Partial Key Log In DataTable
                    dtPartialLog = new DataView(dtLog, "Func_Code in('" + PartialInKey + "','" + PartialOutKey + "') ", "", DataViewRowState.CurrentRows).ToTable();
                }

                dtLog = new DataView(dtLog, "Func_Code in('" + InKey + "','" + OutKey + "') ", "", DataViewRowState.CurrentRows).ToTable();
          

                }
                WorkCalMethod = GetWorkCalculationMethod(empidlist.Split(',')[i].ToString());


               
                    // Delete Record in Attendace Register Acc to EmpId and From Date to To Date
                    objAttReg.DeleteAttendanceRegister(empidlist.Split(',')[i].ToString(), FromDate.ToString(), ToDate.ToString());

                    while (FromDate <= ToDate)
                    {
                        IsOverTime = false;
                        OverTimeMin = 0;
                        IsLate = false;
                        LateMin = 0;
                        IsEarlyOut = false;
                        EarlyMin = 0;
                        AssignMin = 0;
                        WorkMin = 0;
                        LateRelaxMin = 0;
                        LatePenaltyMin = 0;
                        EarlyRelaxMin = 0;
                        EarlyPenaltyMin = 0;
                        EffectiveWorkMin = 0;
                        //Here We are Getting Schedule of Particular Date Only
                        dtSchedule = objEmpSch.GetSheduleDescriptionByEmpId(empidlist.Split(',')[i].ToString(), FromDate.ToString());

                        // if Employee Has no Schedule then we will run else part  otherwise if
                        if (dtSchedule.Rows.Count > 0)
                        {

                            for (int t = 0; t < dtSchedule.Rows.Count; t++)
                            {
                                OverTimeMin = 0;
                                IsLate = false;
                                LateMin = 0;
                                IsEarlyOut = false;
                                EarlyMin = 0;
                                AssignMin = 0;
                                WorkMin = 0;
                                LateRelaxMin = 0;
                                LatePenaltyMin = 0;
                                EffectiveWorkMin = 0;
                                EarlyRelaxMin = 0;
                                EarlyPenaltyMin = 0;
                                if (!Convert.ToBoolean(dtSchedule.Rows[t]["Is_Off"].ToString()))
                                {
                                    OnDutyTime = GetTime24(dtSchedule.Rows[t]["OnDuty_Time"].ToString());
                                    OffDutyTime = GetTime24(dtSchedule.Rows[t]["OffDuty_Time"].ToString());
                                    BeginingIn = dtSchedule.Rows[t]["Beginning_In"].ToString();
                                    EndingIn = dtSchedule.Rows[t]["Ending_In"].ToString();
                                    BeginingOut = dtSchedule.Rows[t]["Beginning_Out"].ToString();
                                    EndingOut = dtSchedule.Rows[t]["Ending_Out"].ToString();

                                    BIn = Convert.ToDateTime(dtSchedule.Rows[t]["Beginning_In"].ToString());
                                    EIn = Convert.ToDateTime(dtSchedule.Rows[t]["Ending_In"].ToString());
                                    BOut = Convert.ToDateTime(dtSchedule.Rows[t]["Beginning_Out"].ToString());
                                    EOut = Convert.ToDateTime(dtSchedule.Rows[t]["Ending_Out"].ToString());
                                     OnTime = Convert.ToDateTime(dtSchedule.Rows[t]["OnDuty_Time"].ToString());
                                    OffTime = Convert.ToDateTime(dtSchedule.Rows[t]["OffDuty_Time"].ToString());
                                    BreakMin = int.Parse(dtSchedule.Rows[t]["Break_Min"].ToString());

                                    AssignMin = int.Parse(dtSchedule.Rows[t]["Work_Minute"].ToString());

                                   


                                    if (Convert.ToDateTime(BeginingIn) <= Convert.ToDateTime(EndingOut))
                                    {
                                        TempInOut = new DataView(dtLog, "Event_Date='" + FromDate.ToString() + "'", "Event_Time", DataViewRowState.CurrentRows).ToTable();

                                    }
                                    else
                                    {
                                        TempInOut = new DataView(dtLog, "Event_Date>='" + FromDate.ToString() + "'  and Event_Date<='" + FromDate.AddDays(1).ToString() + "'", "Event_Time", DataViewRowState.CurrentRows).ToTable();

                                    }
                                    //If Log Exists
                                    if (TempInOut.Rows.Count > 0)
                                    {
                                        //InTime

                                        if (Convert.ToDateTime(BeginingIn) <= Convert.ToDateTime(EndingIn))
                                        {
                                            if (WithKeyPref == "Yes")
                                            {
                                                TempIn = new DataView(dtLog, "Event_Date='" + FromDate.ToString() + "' and Func_Code='"+InKey+"' ", "Event_Time", DataViewRowState.CurrentRows).ToTable();
                                            }
                                            else
                                            {
                                                TempIn = new DataView(dtLog, "Event_Date='" + FromDate.ToString() + "'", "Event_Time", DataViewRowState.CurrentRows).ToTable();
                                            }
                                            BIn = new DateTime(FromDate.Year,FromDate.Month,FromDate.Day,BIn.Hour,BIn.Minute,BIn.Second);
                                            EIn = new DateTime(FromDate.Year, FromDate.Month, FromDate.Day, EIn.Hour, EIn.Minute, EIn.Second);
                                            TempIn = new DataView(TempIn, "Event_Time>='" + BIn + "'  and Event_Time<='" + EIn + "'", "Event_Time", DataViewRowState.CurrentRows).ToTable();
                                        }
                                        else
                                        {
                                            if (WithKeyPref == "Yes")
                                            {
                                                TempIn = new DataView(dtLog, "Func_Code='" + InKey + "' and Event_Date>='" + FromDate.ToString() + "'  and Event_Date<='" + FromDate.AddDays(1).ToString() + "'", "Event_Time", DataViewRowState.CurrentRows).ToTable();
                                            }
                                            else
                                            {
                                                TempIn = new DataView(dtLog, "Event_Date>='" + FromDate.ToString() + "'  and Event_Date<='" + FromDate.AddDays(1).ToString() + "'", "Event_Time", DataViewRowState.CurrentRows).ToTable();
                                            }
                                            BIn = new DateTime(FromDate.Year, FromDate.Month, FromDate.Day, BIn.Hour, BIn.Minute, BIn.Second);
                                            EIn = new DateTime(FromDate.AddDays(1).Year, FromDate.AddDays(1).Month, FromDate.AddDays(1).Day, EIn.Hour, EIn.Minute, EIn.Second);

                                            TempIn = new DataView(TempIn, "Event_Time>='" + BIn + "'  and Event_Time<='" + EIn + "'", "Event_Time", DataViewRowState.CurrentRows).ToTable();
                                           
                                        }

                                        if (TempIn.Rows.Count > 0)
                                        {
                                          
                                            InTime = TempIn.Rows[0]["Event_Time"].ToString();
                                        }
                                        else
                                        {
                                            InTime = "1/1/1900";
                                        }


                                        // Out Time

                                        if (Convert.ToDateTime(BeginingIn) <= Convert.ToDateTime(EndingOut))
                                        {
                                            if (WithKeyPref == "Yes")
                                            {
                                                TempOut = new DataView(dtLog, "Func_Code='"+OutKey+"' and Event_Date='" + FromDate.ToString() + "'", "Event_Time", DataViewRowState.CurrentRows).ToTable();
                                            }
                                            else
                                            {
                                                TempOut = new DataView(dtLog, "Event_Date='" + FromDate.ToString() + "'", "Event_Time", DataViewRowState.CurrentRows).ToTable();
                                            }
                                            BOut = new DateTime(FromDate.Year, FromDate.Month, FromDate.Day, BOut.Hour, BOut.Minute, BOut.Second);
                                            EOut = new DateTime(FromDate.Year, FromDate.Month, FromDate.Day, EOut.Hour, EOut.Minute, EOut.Second);
                                            TempOut = new DataView(TempOut, "Event_Time>='" + BOut + "' and Event_Time<='" + EOut + "'", "Event_Time", DataViewRowState.CurrentRows).ToTable();
                                        }
                                        else
                                        {
                                            if (Convert.ToDateTime(BeginingOut) <= Convert.ToDateTime(EndingOut))
                                            {
                                                if (WithKeyPref == "Yes")
                                                {
                                                    TempOut = new DataView(dtLog, "Func_Code='"+OutKey+"' and Event_Date='" + FromDate.AddDays(1).ToString() + "'", "Event_Time", DataViewRowState.CurrentRows).ToTable();
                                                }
                                                else
                                                {
                                                    TempOut = new DataView(dtLog, "Event_Date='" + FromDate.AddDays(1).ToString() + "'", "Event_Time", DataViewRowState.CurrentRows).ToTable();
                                                }
                                                BOut = new DateTime(FromDate.AddDays(1).Year, FromDate.AddDays(1).Month, FromDate.AddDays(1).Day, BOut.Hour, BOut.Minute, BOut.Second);
                                                EOut = new DateTime(FromDate.AddDays(1).Year, FromDate.AddDays(1).Month, FromDate.AddDays(1).Day, EOut.Hour, EOut.Minute, EOut.Second);


                                                TempOut = new DataView(TempOut, "Event_Time>='" + BOut + "' and Event_Time<='" + EOut + "'", "Event_Time", DataViewRowState.CurrentRows).ToTable();
                                            
                                            }
                                            else
                                            {
                                                if (WithKeyPref == "Yes")
                                                {
                                                    TempOut = new DataView(dtLog, "Func_Code='"+OutKey+"' and Event_Date>='" + FromDate.ToString() + "'  and Event_Date<='" + FromDate.AddDays(1).ToString() + "'", "Event_Time", DataViewRowState.CurrentRows).ToTable();
                                                }
                                                else
                                                {
                                                    TempOut = new DataView(dtLog, "Event_Date>='" + FromDate.ToString() + "'  and Event_Date<='" + FromDate.AddDays(1).ToString() + "'", "Event_Time", DataViewRowState.CurrentRows).ToTable();
                                                }
                                                BOut = new DateTime(FromDate.Year, FromDate.Month, FromDate.Day, BOut.Hour, BOut.Minute, BOut.Second);
                                                EOut = new DateTime(FromDate.AddDays(1).Year, FromDate.AddDays(1).Month, FromDate.AddDays(1).Day, EOut.Hour, EOut.Minute, EOut.Second);


                                                TempOut = new DataView(TempOut, "Event_Time>='" + BOut + "' and Event_Time<='" + EOut + "'", "Event_Time", DataViewRowState.CurrentRows).ToTable();
                                            
                                            }
                                        }

                                        if (TempOut.Rows.Count > 0)
                                        {

                                            OutTime = TempOut.Rows[0]["Event_Time"].ToString();
                                        }
                                        else
                                        {
                                            OutTime = "1/1/1900";
                                        }
                                        if (InTime == "1/1/1900" || OutTime == "1/1/1900")
                                        {
                                            if (Convert.ToDateTime(BeginingIn) <= Convert.ToDateTime(EndingOut))
                                            {
                                               
                                                    TempInOut = new DataView(dtLog, "Event_Date='" + FromDate.ToString() + "'", "Event_Time", DataViewRowState.CurrentRows).ToTable();
                                               
                                                BIn = new DateTime(FromDate.Year, FromDate.Month, FromDate.Day, BIn.Hour, BIn.Minute, BIn.Second);
                                                EOut = new DateTime(FromDate.Year, FromDate.Month, FromDate.Day, EOut.Hour, EOut.Minute, EOut.Second);

                                                TempInOut = new DataView(TempInOut, "Event_Time>='" + BIn + "' and Event_Time<='" + EOut + "'", "Event_Time", DataViewRowState.CurrentRows).ToTable();
                                            
                                            }
                                            else
                                            {
                                                TempInOut = new DataView(dtLog, "Event_Date>='" + FromDate.ToString() + "'  and Event_Date<='" + FromDate.AddDays(1).ToString() + "'", "Event_Time", DataViewRowState.CurrentRows).ToTable();
                                                BIn = new DateTime(FromDate.Year, FromDate.Month, FromDate.Day, BIn.Hour, BIn.Minute, BIn.Second);
                                                EOut = new DateTime(FromDate.AddDays(1).Year, FromDate.AddDays(1).Month, FromDate.AddDays(1).Day, EOut.Hour, EOut.Minute, EOut.Second);

                                                TempInOut = new DataView(TempInOut, "Event_Time>='" + BIn + "' and Event_Time<='" + EOut + "'", "Event_Time", DataViewRowState.CurrentRows).ToTable();
  
                                            }
                                            if (TempInOut.Rows.Count > 0)
                                            {
                                                if (WithKeyPref == "Yes")
                                                {
                                                    DataTable DtIn = new DataView(TempInOut, "Func_Code='" + InKey + "'", "Event_Time", DataViewRowState.CurrentRows).ToTable();
                                                    DataTable DtOut = new DataView(TempInOut, "Func_Code='" + OutKey + "'", "Event_Time", DataViewRowState.CurrentRows).ToTable();
                                                    try
                                                    {
                                                        InTime = DtIn.Rows[0]["Event_Time"].ToString();
                                                    }
                                                    catch
                                                    {

                                                    }
                                                    try
                                                    {
                                                        OutTime = DtOut.Rows[DtOut.Rows.Count - 1]["Event_Time"].ToString();
                                                    }
                                                    catch
                                                    {

                                                    }
                                                }
                                                else
                                                {
                                                    InTime = TempInOut.Rows[0]["Event_Time"].ToString();
                                                    OutTime = TempInOut.Rows[TempInOut.Rows.Count - 1]["Event_Time"].ToString();
                                                }
                                            }
                                        }

                                        if (InTime == "1/1/1900" && OutTime != "1/1/1900")
                                        {
                                            if (NoClockIn)
                                            {
                                                if (objEmpHoliday.GetEmployeeHolidayOnDateAndEmpId(FromDate.ToString(), empidlist.Split(',')[i].ToString()))
                                                {
                                                    objAttReg.InsertAttendanceRegister(Session["CompId"].ToString(), empidlist.Split(',')[i].ToString(), FromDate.ToString(), dtSchedule.Rows[t]["Shift_Id"].ToString(), dtSchedule.Rows[t]["Is_Temp"].ToString(), dtSchedule.Rows[t]["TimeTable_Id"].ToString(), dtSchedule.Rows[t]["OnDuty_Time"].ToString(), dtSchedule.Rows[t]["OffDuty_Time"].ToString(), "1/1/1990", "1/1/1900", false.ToString(), "0", "0", "0", false.ToString(), "0", "0", "0", false.ToString(), true.ToString(), false.ToString(), false.ToString(), "0", "0", "0", "0", "0", AssignMin.ToString(), AssignMin.ToString(), "", "", "", "", "", true.ToString(), DateTime.Now.ToString(), true.ToString(), Session["UserId"].ToString(), DateTime.Now.ToString(), Session["UserId"].ToString(), DateTime.Now.ToString());
                                                    continue;
                                                }
                                                else
                                                {
                                                    objAttReg.InsertAttendanceRegister(Session["CompId"].ToString(), empidlist.Split(',')[i].ToString(), FromDate.ToString(), dtSchedule.Rows[t]["Shift_Id"].ToString(), dtSchedule.Rows[t]["Is_Temp"].ToString(), dtSchedule.Rows[t]["TimeTable_Id"].ToString(), dtSchedule.Rows[t]["OnDuty_Time"].ToString(), dtSchedule.Rows[t]["OffDuty_Time"].ToString(), "1/1/1990", OutTime.ToString(), false.ToString(), "0", "0", "0", false.ToString(), "0", "0", "0", false.ToString(), false.ToString(), false.ToString(), true.ToString(), "0", "0", "0", "0", "0", "0", AssignMin.ToString(), "", "", "", "", "", true.ToString(), DateTime.Now.ToString(), true.ToString(), Session["UserId"].ToString(), DateTime.Now.ToString(), Session["UserId"].ToString(), DateTime.Now.ToString());
                                                    continue;
                                                }
                                            }
                                            else
                                            {
                                                if (objEmpHoliday.GetEmployeeHolidayOnDateAndEmpId(FromDate.ToString(), empidlist.Split(',')[i].ToString()))
                                                {
                                                    objAttReg.InsertAttendanceRegister(Session["CompId"].ToString(), empidlist.Split(',')[i].ToString(), FromDate.ToString(), dtSchedule.Rows[t]["Shift_Id"].ToString(), dtSchedule.Rows[t]["Is_Temp"].ToString(), dtSchedule.Rows[t]["TimeTable_Id"].ToString(), dtSchedule.Rows[t]["OnDuty_Time"].ToString(), dtSchedule.Rows[t]["OffDuty_Time"].ToString(), "1/1/1990", "1/1/1900", false.ToString(), "0", "0", "0", false.ToString(), "0", "0", "0", false.ToString(), true.ToString(), false.ToString(), false.ToString(), "0", "0", "0", "0", "0", AssignMin.ToString(), AssignMin.ToString(), "", "", "", "", "", true.ToString(), DateTime.Now.ToString(), true.ToString(), Session["UserId"].ToString(), DateTime.Now.ToString(), Session["UserId"].ToString(), DateTime.Now.ToString());
                                                    continue;
                                                }
                                                else
                                                {
                                                    objAttReg.InsertAttendanceRegister(Session["CompId"].ToString(), empidlist.Split(',')[i].ToString(), FromDate.ToString(), dtSchedule.Rows[t]["Shift_Id"].ToString(), dtSchedule.Rows[t]["Is_Temp"].ToString(), dtSchedule.Rows[t]["TimeTable_Id"].ToString(), dtSchedule.Rows[t]["OnDuty_Time"].ToString(), dtSchedule.Rows[t]["OffDuty_Time"].ToString(), "1/1/1990", OutTime.ToString(), false.ToString(), "0", "0", "0", false.ToString(), "0", "0", "0", false.ToString(), false.ToString(), false.ToString(), false.ToString(), "0", "0", "0", "0", "0", "0", AssignMin.ToString(), "", "", "", "", "", true.ToString(), DateTime.Now.ToString(), true.ToString(), Session["UserId"].ToString(), DateTime.Now.ToString(), Session["UserId"].ToString(), DateTime.Now.ToString());
                                                    continue;
                                                }
            
                                            }
                                        }

                                        if (InTime != "1/1/1900" && OutTime == "1/1/1900")
                                        {
                                            if (NoClockOut)
                                            {
                                                if (objEmpHoliday.GetEmployeeHolidayOnDateAndEmpId(FromDate.ToString(), empidlist.Split(',')[i].ToString()))
                                                {
                                                    objAttReg.InsertAttendanceRegister(Session["CompId"].ToString(), empidlist.Split(',')[i].ToString(), FromDate.ToString(), dtSchedule.Rows[t]["Shift_Id"].ToString(), dtSchedule.Rows[t]["Is_Temp"].ToString(), dtSchedule.Rows[t]["TimeTable_Id"].ToString(), dtSchedule.Rows[t]["OnDuty_Time"].ToString(), dtSchedule.Rows[t]["OffDuty_Time"].ToString(), "1/1/1990", "1/1/1900", false.ToString(), "0", "0", "0", false.ToString(), "0", "0", "0", false.ToString(), true.ToString(), false.ToString(), false.ToString(), "0", "0", "0", "0", "0", AssignMin.ToString(), AssignMin.ToString(), "", "", "", "", "", true.ToString(), DateTime.Now.ToString(), true.ToString(), Session["UserId"].ToString(), DateTime.Now.ToString(), Session["UserId"].ToString(), DateTime.Now.ToString());
                                                    continue;
                                                }
                                                else
                                                {
                                                    objAttReg.InsertAttendanceRegister(Session["CompId"].ToString(), empidlist.Split(',')[i].ToString(), FromDate.ToString(), dtSchedule.Rows[t]["Shift_Id"].ToString(), dtSchedule.Rows[t]["Is_Temp"].ToString(), dtSchedule.Rows[t]["TimeTable_Id"].ToString(), dtSchedule.Rows[t]["OnDuty_Time"].ToString(), dtSchedule.Rows[t]["OffDuty_Time"].ToString(), InTime.ToString(), "1/1/1990", false.ToString(), "0", "0", "0", false.ToString(), "0", "0", "0", false.ToString(), false.ToString(), false.ToString(), true.ToString(), "0", "0", "0", "0", "0", "0", AssignMin.ToString(), "", "", "", "", "", true.ToString(), DateTime.Now.ToString(), true.ToString(), Session["UserId"].ToString(), DateTime.Now.ToString(), Session["UserId"].ToString(), DateTime.Now.ToString());
                                                    continue;
                                                }
                                            }
                                            else
                                            {
                                                if (objEmpHoliday.GetEmployeeHolidayOnDateAndEmpId(FromDate.ToString(), empidlist.Split(',')[i].ToString()))
                                                {
                                                    objAttReg.InsertAttendanceRegister(Session["CompId"].ToString(), empidlist.Split(',')[i].ToString(), FromDate.ToString(), dtSchedule.Rows[t]["Shift_Id"].ToString(), dtSchedule.Rows[t]["Is_Temp"].ToString(), dtSchedule.Rows[t]["TimeTable_Id"].ToString(), dtSchedule.Rows[t]["OnDuty_Time"].ToString(), dtSchedule.Rows[t]["OffDuty_Time"].ToString(), "1/1/1990", "1/1/1900", false.ToString(), "0", "0", "0", false.ToString(), "0", "0", "0", false.ToString(), true.ToString(), false.ToString(), false.ToString(), "0", "0", "0", "0", "0", AssignMin.ToString(), AssignMin.ToString(), "", "", "", "", "", true.ToString(), DateTime.Now.ToString(), true.ToString(), Session["UserId"].ToString(), DateTime.Now.ToString(), Session["UserId"].ToString(), DateTime.Now.ToString());
                                                    continue;
                                                }
                                                else
                                                {
                                                    objAttReg.InsertAttendanceRegister(Session["CompId"].ToString(), empidlist.Split(',')[i].ToString(), FromDate.ToString(), dtSchedule.Rows[t]["Shift_Id"].ToString(), dtSchedule.Rows[t]["Is_Temp"].ToString(), dtSchedule.Rows[t]["TimeTable_Id"].ToString(), dtSchedule.Rows[t]["OnDuty_Time"].ToString(), dtSchedule.Rows[t]["OffDuty_Time"].ToString(), InTime.ToString(), "1/1/1990", false.ToString(), "0", "0", "0", false.ToString(), "0", "0", "0", false.ToString(), false.ToString(), false.ToString(), false.ToString(), "0", "0", "0", "0", "0", "0", AssignMin.ToString(), "", "", "", "", "", true.ToString(), DateTime.Now.ToString(), true.ToString(), Session["UserId"].ToString(), DateTime.Now.ToString(), Session["UserId"].ToString(), DateTime.Now.ToString());
                                                    continue;
                                                }
                                            }
                                        }
                                        if (InTime == "1/1/1900" && OutTime == "1/1/1900")
                                        {
                                            if (objEmpHoliday.GetEmployeeHolidayOnDateAndEmpId(FromDate.ToString(), empidlist.Split(',')[i].ToString()))
                                            {
                                                objAttReg.InsertAttendanceRegister(Session["CompId"].ToString(), empidlist.Split(',')[i].ToString(), FromDate.ToString(), dtSchedule.Rows[t]["Shift_Id"].ToString(), dtSchedule.Rows[t]["Is_Temp"].ToString(), dtSchedule.Rows[t]["TimeTable_Id"].ToString(), dtSchedule.Rows[t]["OnDuty_Time"].ToString(), dtSchedule.Rows[t]["OffDuty_Time"].ToString(), "1/1/1990", "1/1/1900", false.ToString(), "0", "0", "0", false.ToString(), "0", "0", "0", false.ToString(), true.ToString(), false.ToString(), false.ToString(), "0", "0", "0", "0", "0", AssignMin.ToString(), AssignMin.ToString(), "", "", "", "", "", true.ToString(), DateTime.Now.ToString(), true.ToString(), Session["UserId"].ToString(), DateTime.Now.ToString(), Session["UserId"].ToString(), DateTime.Now.ToString());

                                            }

                                            else if (ObjLeaveReq.IsLeaveOnDate(FromDate.ToString(), empidlist.Split(',')[i].ToString()))
                                            {
                                                objAttReg.InsertAttendanceRegister(Session["CompId"].ToString(), empidlist.Split(',')[i].ToString(), FromDate.ToString(), dtSchedule.Rows[t]["Shift_Id"].ToString(), dtSchedule.Rows[t]["Is_Temp"].ToString(), dtSchedule.Rows[t]["TimeTable_Id"].ToString(), dtSchedule.Rows[t]["OnDuty_Time"].ToString(), dtSchedule.Rows[t]["OffDuty_Time"].ToString(), "1/1/1990", "1/1/1900", false.ToString(), "0", "0", "0", false.ToString(), "0", "0", "0", false.ToString(), false.ToString(), true.ToString(), false.ToString(), "0", "0", "0", "0", "0", AssignMin.ToString(), AssignMin.ToString(), "", "", "", "", "", true.ToString(), DateTime.Now.ToString(), true.ToString(), Session["UserId"].ToString(), DateTime.Now.ToString(), Session["UserId"].ToString(), DateTime.Now.ToString());
                                            }

                                            else
                                            {
                                                objAttReg.InsertAttendanceRegister(Session["CompId"].ToString(), empidlist.Split(',')[i].ToString(), FromDate.ToString(), dtSchedule.Rows[t]["Shift_Id"].ToString(), dtSchedule.Rows[t]["Is_Temp"].ToString(), dtSchedule.Rows[t]["TimeTable_Id"].ToString(), dtSchedule.Rows[t]["OnDuty_Time"].ToString(), dtSchedule.Rows[t]["OffDuty_Time"].ToString(), "1/1/1990", "1/1/1900", false.ToString(), "0", "0", "0", false.ToString(), "0", "0", "0", false.ToString(), false.ToString(), false.ToString(), true.ToString(), "0", "0", "0", "0", "0", "0", AssignMin.ToString(), "", "", "", "", "", true.ToString(), DateTime.Now.ToString(), true.ToString(), Session["UserId"].ToString(), DateTime.Now.ToString(), Session["UserId"].ToString(), DateTime.Now.ToString());


                                            }
                                        }
                                        else
                                        {//Effective Work calculation
                                            TempIn = new DataTable();
                                            TempOut = new DataTable();
                                            TempInOut = new DataTable();
                                            if (WorkCalMethod == "PairWise")
                                            {
                                                if (Convert.ToDateTime(BeginingIn) <= Convert.ToDateTime(EndingOut))
                                                {//same day log
                                                   
                                                        TempInOut = new DataView(dtLog, "Event_Date='" + FromDate.ToString() + "'   and  Event_Time>='" + InTime + "' and Event_Time <='" + OutTime + "'", "Event_Time", DataViewRowState.CurrentRows).ToTable();
                                                   
                                                     
                                                    
                                                    TempInOut = new DataView(dtLog, "Event_Time>='" + InTime + "' and Event_Time <='" + OutTime + "'", "Event_Time", DataViewRowState.CurrentRows).ToTable();
                                                   
                                                   
                                                    OnTime = new DateTime(FromDate.Year,FromDate.Month,FromDate.Day,OnTime.Hour,OnTime.Minute,OnTime.Second);
                                                    OffTime = new DateTime(FromDate.Year, FromDate.Month, FromDate.Day, OffTime.Hour, OffTime.Minute, OffTime.Second);
                                                
                                                }
                                                else
                                                {//next day and same day log

                                                    TempInOut = new DataView(dtLog, "Event_Date>='" + FromDate.ToString() + "'  and Event_Date<='" + FromDate.AddDays(1).ToString() + "'", "Event_Time", DataViewRowState.CurrentRows).ToTable();


                                                    TempInOut = new DataView(dtLog, "Event_Time>='" + InTime + "' and Event_Time <='" + OutTime + "'", "Event_Time", DataViewRowState.CurrentRows).ToTable();
                                                    OnTime = new DateTime(FromDate.Year, FromDate.Month, FromDate.Day, OnTime.Hour, OnTime.Minute, OnTime.Second);
                                                    OffTime = new DateTime(FromDate.AddDays(1).Year, FromDate.AddDays(1).Month, FromDate.AddDays(1).Day, OffTime.Hour, OffTime.Minute, OffTime.Second);
                                                
                                                }
                                                DateTime InTimeF = Convert.ToDateTime("1/1/1900");
                                                DateTime OutTimeF = Convert.ToDateTime("1/1/1900");
                                                InTime = Convert.ToDateTime("1/1/1900").ToString();
                                                for (int l = 0; l < TempInOut.Rows.Count; l++)
                                                {

                                                    if (TempInOut.Rows.Count==1)
                                                    {
                                                        InTime = TempInOut.Rows[l]["Event_Time"].ToString();
                                                    }
                                                    else if (TempInOut.Rows.Count > 1 && l == 0)
                                                    {
                                                        InTime = TempInOut.Rows[l]["Event_Time"].ToString();
                                                        InTimeF = Convert.ToDateTime(TempInOut.Rows[l]["Event_Time"].ToString());
                                                        OutTimeF = Convert.ToDateTime(TempInOut.Rows[l + 1]["Event_Time"].ToString());
                                                        WorkMin += GetTimeDifference(InTimeF, OutTimeF);
                                                    }
                                                    else if (l % 2 == 0)
                                                    {
                                                        try
                                                        {
                                                            InTimeF = Convert.ToDateTime(TempInOut.Rows[l]["Event_Time"].ToString());
                                                            OutTimeF = Convert.ToDateTime(TempInOut.Rows[l + 1]["Event_Time"].ToString());
                                                            WorkMin += GetTimeDifference(InTimeF, OutTimeF);
                                                        }
                                                        catch
                                                        {

                                                        }
                                                    }





                                                }
                                                try
                                                {
                                                    InTimeF = Convert.ToDateTime(InTime);
                                                }
                                                catch
                                                {
                                                    InTimeF = Convert.ToDateTime("1/1/1900");
                                                }
                                                if (InTimeF > OnTime)
                                                {
                                                    LateMin = GetTimeDifference(Convert.ToDateTime(OnTime), InTimeF);
                                                    IsLate = true;
                                                    LateRelaxMin = int.Parse(GetLateRelaxMinPenaltyMin(empidlist.Split(',')[i].ToString(), FromDate, LateMin).Split('-')[0]);
                                                    LatePenaltyMin = int.Parse(GetLateRelaxMinPenaltyMin(empidlist.Split(',')[i].ToString(), FromDate, LateMin).Split('-')[1]);

                                                }
                                                if (OutTimeF <OffTime)
                                                {
                                                    EarlyMin = GetTimeDifference(OutTimeF, Convert.ToDateTime(OffTime));
                                                    IsEarlyOut = true;

                                                    EarlyRelaxMin = int.Parse(GetEarlyRelaxMinPenaltyMin(empidlist.Split(',')[i].ToString(), FromDate, EarlyMin).Split('-')[0]);
                                                    EarlyPenaltyMin = int.Parse(GetEarlyRelaxMinPenaltyMin(empidlist.Split(',')[i].ToString(), FromDate, EarlyMin).Split('-')[1]);

                                                }
                                                int PartialViolationMin = 0;
                                                int PartialMin = 0;

                                                PartialMin = int.Parse(GetPartialViolationMin(empidlist.Split(',')[i].ToString(), FromDate, dtSchedule.Rows[t]["TimeTable_Id"].ToString()).Split('-')[0]);
                                                PartialViolationMin = int.Parse(GetPartialViolationMin(empidlist.Split(',')[i].ToString(), FromDate, dtSchedule.Rows[t]["TimeTable_Id"].ToString()).Split('-')[1]);
                                                WorkMin += PartialMin;

                                                EffectiveWorkMin = WorkMin;
                                                WorkMin = int.Parse(getWorkMinute(WorkMin.ToString(), AssignMin.ToString()));
                                             
                                                OverTimeMin = GetOverTimeMin(empidlist.Split(',')[i].ToString(), InTimeF, OutTimeF, Convert.ToDateTime(OnTime), Convert.ToDateTime(OffTime), EffectiveWorkMin);
                                               

                                                if (objEmpHoliday.GetEmployeeHolidayOnDateAndEmpId(FromDate.ToString(), empidlist.Split(',')[i].ToString()))
                                                {
                                                    WorkMin = EffectiveWorkMin;

                                                    objAttReg.InsertAttendanceRegister(Session["CompId"].ToString(), empidlist.Split(',')[i].ToString(), FromDate.ToString(), dtSchedule.Rows[t]["Shift_Id"].ToString(), dtSchedule.Rows[t]["Is_Temp"].ToString(), dtSchedule.Rows[t]["TimeTable_Id"].ToString(), dtSchedule.Rows[t]["OnDuty_Time"].ToString(), dtSchedule.Rows[t]["OffDuty_Time"].ToString(), InTimeF.ToString(), OutTimeF.ToString(), false.ToString(), "0", "0", "0", false.ToString(), "0", "0", "0", false.ToString(), true.ToString(), false.ToString(), false.ToString(), "0", WorkMin.ToString(), "0", "0", "0", AssignMin.ToString(), AssignMin.ToString(), "", "", "", "", "", true.ToString(), DateTime.Now.ToString(), true.ToString(), Session["UserId"].ToString(), DateTime.Now.ToString(), Session["UserId"].ToString(), DateTime.Now.ToString());

                                                }
                                                else
                                                {

                                                   
                                                        LateMin=0;
                                                        EarlyMin = 0;

                                                        if (LatePenaltyMin>0)
                                                    {
                                                       
                                                        LateMin = LatePenaltyMin;
                                                        LatePenaltyMin = 0;
                                                     }
                                                        
                                                        if (EarlyPenaltyMin > 0)
                                                        {
                                                           
                                                            EarlyMin = EarlyPenaltyMin;
                                                            EarlyPenaltyMin = 0;

                                                        }


                                                    objAttReg.InsertAttendanceRegister(Session["CompId"].ToString(), empidlist.Split(',')[i].ToString(), FromDate.ToString(), dtSchedule.Rows[t]["Shift_Id"].ToString(), dtSchedule.Rows[t]["Is_Temp"].ToString(), dtSchedule.Rows[t]["TimeTable_Id"].ToString(), dtSchedule.Rows[t]["OnDuty_Time"].ToString(), dtSchedule.Rows[t]["OffDuty_Time"].ToString(), InTimeF.ToString(), OutTimeF.ToString(), IsLate.ToString(), LateMin.ToString(), LateRelaxMin.ToString(),LatePenaltyMin.ToString(), IsEarlyOut.ToString(), EarlyMin.ToString(), EarlyRelaxMin.ToString(), EarlyPenaltyMin.ToString(), false.ToString(), false.ToString(), false.ToString(), false.ToString(), "0", "0", OverTimeMin.ToString(), PartialMin.ToString(), PartialViolationMin.ToString(), WorkMin.ToString(), AssignMin.ToString(), "", "", "", "", "", true.ToString(), DateTime.Now.ToString(), true.ToString(), Session["UserId"].ToString(), DateTime.Now.ToString(), Session["UserId"].ToString(), DateTime.Now.ToString());


                                                }
                                            }
                                            else
                                            {
                                                DateTime InTimeF = Convert.ToDateTime("1/1/1900");
                                                DateTime OutTimeF = Convert.ToDateTime("1/1/1900");

                                                if (Convert.ToDateTime(BeginingIn) <= Convert.ToDateTime(EndingOut))
                                                {//same day log
                                                    TempInOut = new DataView(dtLog, "Event_Date='" + FromDate.ToString() + "'   and  Event_Time>='" + InTime + "' and Event_Time <='" + OutTime + "'", "Event_Time", DataViewRowState.CurrentRows).ToTable();

                                                    OnTime = new DateTime(FromDate.Year, FromDate.Month, FromDate.Day, OnTime.Hour, OnTime.Minute, OnTime.Second);
                                                    OffTime = new DateTime(FromDate.Year, FromDate.Month, FromDate.Day, OffTime.Hour, OffTime.Minute, OffTime.Second);
                                                
                                                }
                                                else
                                                {//next day and same day log

                                                    TempInOut = new DataView(dtLog, "Event_Date>='" + FromDate.ToString() + "'  and Event_Date<='" + FromDate.AddDays(1).ToString() + "'", "Event_Time", DataViewRowState.CurrentRows).ToTable();


                                                    TempInOut = new DataView(dtLog, "Event_Time>='" + InTime + "' and Event_Time <='" + OutTime + "'", "Event_Time", DataViewRowState.CurrentRows).ToTable();
                                                    OnTime = new DateTime(FromDate.Year, FromDate.Month, FromDate.Day, OnTime.Hour, OnTime.Minute, OnTime.Second);
                                                    OffTime = new DateTime(FromDate.AddDays(1).Year, FromDate.AddDays(1).Month, FromDate.AddDays(1).Day, OffTime.Hour, OffTime.Minute, OffTime.Second);
                                                
                                                }

                                                if (TempInOut.Rows.Count > 0)
                                                {
                                                    if (WithKeyPref == "Yes")
                                                    {
                                                        DataTable DtIn = new DataView(TempInOut, "Func_Code='" + InKey + "'", "Event_Time", DataViewRowState.CurrentRows).ToTable();
                                                        DataTable DtOut = new DataView(TempInOut, "Func_Code='" + OutKey + "'", "Event_Time", DataViewRowState.CurrentRows).ToTable();
                                                        InTimeF =Convert.ToDateTime(DtIn.Rows[0]["Event_Time"].ToString());
                                                        OutTimeF = Convert.ToDateTime(DtOut.Rows[DtOut.Rows.Count - 1]["Event_Time"].ToString());
                                                    }
                                                    else
                                                    {
                                                        InTimeF = Convert.ToDateTime(TempInOut.Rows[0]["Event_Time"].ToString());
                                                        OutTimeF = Convert.ToDateTime(TempInOut.Rows[TempInOut.Rows.Count - 1]["Event_Time"].ToString());
                                                    }
                                                }
                                                if (InTimeF > Convert.ToDateTime(OnTime))
                                                {
                                                    LateMin = GetTimeDifference(Convert.ToDateTime(OnTime), InTimeF);
                                                    IsLate = true;
                                                    LateRelaxMin = int.Parse(GetLateRelaxMinPenaltyMin(empidlist.Split(',')[i].ToString(), FromDate, LateMin).Split('-')[0]);
                                                    LatePenaltyMin = int.Parse(GetLateRelaxMinPenaltyMin(empidlist.Split(',')[i].ToString(), FromDate, LateMin).Split('-')[1]);

                                                }
                                                if (OutTimeF < Convert.ToDateTime(OffTime))
                                                {
                                                    EarlyMin = GetTimeDifference(OutTimeF, Convert.ToDateTime(OffTime));
                                                    IsEarlyOut = true;

                                                    EarlyRelaxMin = int.Parse(GetEarlyRelaxMinPenaltyMin(empidlist.Split(',')[i].ToString(), FromDate, EarlyMin).Split('-')[0]);
                                                    EarlyPenaltyMin = int.Parse(GetEarlyRelaxMinPenaltyMin(empidlist.Split(',')[i].ToString(), FromDate, EarlyMin).Split('-')[1]);

                                                }
                                                int PartialViolationMin = 0;
                                                int PartialMin = 0;
                                                PartialMin = int.Parse(GetPartialViolationMin(empidlist.Split(',')[i].ToString(), FromDate, dtSchedule.Rows[t]["TimeTable_Id"].ToString()).Split('-')[0]);
                                                PartialViolationMin = int.Parse(GetPartialViolationMin(empidlist.Split(',')[i].ToString(), FromDate, dtSchedule.Rows[t]["TimeTable_Id"].ToString()).Split('-')[1]);
                                                LateMin = 0;
                                                EarlyMin = 0;

                                                if (LatePenaltyMin > 0)
                                                {
                                                   
                                                    LateMin = LatePenaltyMin;
                                                    LatePenaltyMin = 0;
                                                }

                                                if (EarlyPenaltyMin > 0)
                                                {
                                                   
                                                    EarlyMin = EarlyPenaltyMin;
                                                    EarlyPenaltyMin = 0;

                                                }


                                                WorkMin = GetTimeDifference(InTimeF, OutTimeF);
                                                EffectiveWorkMin = WorkMin;
                                                WorkMin = int.Parse(getWorkMinute(WorkMin.ToString(), AssignMin.ToString()));
                                                OverTimeMin = GetOverTimeMin(empidlist.Split(',')[i].ToString(), InTimeF, OutTimeF, Convert.ToDateTime(OnTime), Convert.ToDateTime(OffTime), EffectiveWorkMin);
                                                if (objEmpHoliday.GetEmployeeHolidayOnDateAndEmpId(FromDate.ToString(), empidlist.Split(',')[i].ToString()))
                                                {
                                                    WorkMin = EffectiveWorkMin;
                                                    objAttReg.InsertAttendanceRegister(Session["CompId"].ToString(), empidlist.Split(',')[i].ToString(), FromDate.ToString(), dtSchedule.Rows[t]["Shift_Id"].ToString(), dtSchedule.Rows[t]["Is_Temp"].ToString(), dtSchedule.Rows[t]["TimeTable_Id"].ToString(), dtSchedule.Rows[t]["OnDuty_Time"].ToString(), dtSchedule.Rows[t]["OffDuty_Time"].ToString(),InTimeF.ToString(),OutTimeF.ToString(), false.ToString(), "0", "0", "0", false.ToString(), "0", "0", "0", false.ToString(), true.ToString(), false.ToString(), false.ToString(), "0", WorkMin.ToString(), "0", "0", "0", AssignMin.ToString(), AssignMin.ToString(), "", "", "", "", "", true.ToString(), DateTime.Now.ToString(), true.ToString(), Session["UserId"].ToString(), DateTime.Now.ToString(), Session["UserId"].ToString(), DateTime.Now.ToString());

                                                }
                                                else
                                                {
                                                    objAttReg.InsertAttendanceRegister(Session["CompId"].ToString(), empidlist.Split(',')[i].ToString(), FromDate.ToString(), dtSchedule.Rows[t]["Shift_Id"].ToString(), dtSchedule.Rows[t]["Is_Temp"].ToString(), dtSchedule.Rows[t]["TimeTable_Id"].ToString(), dtSchedule.Rows[t]["OnDuty_Time"].ToString(), dtSchedule.Rows[t]["OffDuty_Time"].ToString(), InTimeF.ToString(), OutTimeF.ToString(), IsLate.ToString(), LateMin.ToString(), LateRelaxMin.ToString(), LatePenaltyMin.ToString(), IsEarlyOut.ToString(), EarlyMin.ToString(), EarlyRelaxMin.ToString(), EarlyPenaltyMin.ToString(), false.ToString(), false.ToString(), false.ToString(), false.ToString(), "0", "0", OverTimeMin.ToString(), PartialMin.ToString(), PartialViolationMin.ToString(), WorkMin.ToString(), AssignMin.ToString(), "", "", "", "", "", true.ToString(), DateTime.Now.ToString(), true.ToString(), Session["UserId"].ToString(), DateTime.Now.ToString(), Session["UserId"].ToString(), DateTime.Now.ToString());
                                                }
                                            }

                                        }
                                    }
                                    else
                                    {
                                        if (objEmpHoliday.GetEmployeeHolidayOnDateAndEmpId(FromDate.ToString(), empidlist.Split(',')[i].ToString()))
                                        {
                                            objAttReg.InsertAttendanceRegister(Session["CompId"].ToString(), empidlist.Split(',')[i].ToString(), FromDate.ToString(), dtSchedule.Rows[t]["Shift_Id"].ToString(), dtSchedule.Rows[t]["Is_Temp"].ToString(), dtSchedule.Rows[t]["TimeTable_Id"].ToString(), dtSchedule.Rows[t]["OnDuty_Time"].ToString(), dtSchedule.Rows[t]["OffDuty_Time"].ToString(), "1/1/1990", "1/1/1900", false.ToString(), "0", "0", "0", false.ToString(), "0", "0", "0", false.ToString(), true.ToString(), false.ToString(), false.ToString(), "0", "0", "0", "0", "0", AssignMin.ToString(), AssignMin.ToString(), "", "", "", "", "", true.ToString(), DateTime.Now.ToString(), true.ToString(), Session["UserId"].ToString(), DateTime.Now.ToString(), Session["UserId"].ToString(), DateTime.Now.ToString());

                                        }

                                        else if (ObjLeaveReq.IsLeaveOnDate(FromDate.ToString(), empidlist.Split(',')[i].ToString()))
                                        {
                                            objAttReg.InsertAttendanceRegister(Session["CompId"].ToString(), empidlist.Split(',')[i].ToString(), FromDate.ToString(), dtSchedule.Rows[t]["Shift_Id"].ToString(), dtSchedule.Rows[t]["Is_Temp"].ToString(), dtSchedule.Rows[t]["TimeTable_Id"].ToString(), dtSchedule.Rows[t]["OnDuty_Time"].ToString(), dtSchedule.Rows[t]["OffDuty_Time"].ToString(), "1/1/1990", "1/1/1900", false.ToString(), "0", "0", "0", false.ToString(), "0", "0", "0", false.ToString(), false.ToString(), true.ToString(), false.ToString(), "0", "0", "0", "0", "0", AssignMin.ToString(), AssignMin.ToString(), "", "", "", "", "", true.ToString(), DateTime.Now.ToString(), true.ToString(), Session["UserId"].ToString(), DateTime.Now.ToString(), Session["UserId"].ToString(), DateTime.Now.ToString());
                                        }

                                        else
                                        {
                                            objAttReg.InsertAttendanceRegister(Session["CompId"].ToString(), empidlist.Split(',')[i].ToString(), FromDate.ToString(), dtSchedule.Rows[t]["Shift_Id"].ToString(), dtSchedule.Rows[t]["Is_Temp"].ToString(), dtSchedule.Rows[t]["TimeTable_Id"].ToString(), dtSchedule.Rows[t]["OnDuty_Time"].ToString(), dtSchedule.Rows[t]["OffDuty_Time"].ToString(), "1/1/1990", "1/1/1900", false.ToString(), "0", "0", "0", false.ToString(), "0", "0", "0", false.ToString(), false.ToString(), false.ToString(), true.ToString(), "0", "0", "0", "0", "0", "0", AssignMin.ToString(), "", "", "", "", "", true.ToString(), DateTime.Now.ToString(), true.ToString(), Session["UserId"].ToString(), DateTime.Now.ToString(), Session["UserId"].ToString(), DateTime.Now.ToString());


                                        }
                                    }



                                }
                                else
                                {
                                    //Here Code Off WeekOff
                                    DataTable dtLogOnDate = new DataTable();
                                    dtLogOnDate = new DataView(dtLog, "Event_Date='" + FromDate.ToString() + "'", "Event_Time", DataViewRowState.CurrentRows).ToTable();
                                    if (dtLogOnDate.Rows.Count > 0)
                                    {
                                       

                                            if (Convert.ToDateTime(BeginingIn) <= Convert.ToDateTime(EndingOut))
                                    {
                                        TempInOut = new DataView(dtLog, "Event_Date='" + FromDate.ToString() + "'", "Event_Time", DataViewRowState.CurrentRows).ToTable();

                                    }
                                    else
                                    {
                                        TempInOut = new DataView(dtLog, "Event_Date>='" + FromDate.ToString() + "'  and Event_Date<='" + FromDate.AddDays(1).ToString() + "'", "Event_Time", DataViewRowState.CurrentRows).ToTable();

                                    }
                                    //If Log Exists
                                            if (TempInOut.Rows.Count > 0)
                                            {
                                                //InTime

                                                if (Convert.ToDateTime(BeginingIn) <= Convert.ToDateTime(EndingIn))
                                                {
                                                    if (WithKeyPref == "Yes")
                                                    {

                                                        TempIn = new DataView(dtLog, "Func_Code='" + InKey + "' and Event_Date='" + FromDate.ToString() + "'", "Event_Time", DataViewRowState.CurrentRows).ToTable();
                                                    }
                                                    else
                                                    {
                                                        TempIn = new DataView(dtLog, "Event_Date='" + FromDate.ToString() + "'", "Event_Time", DataViewRowState.CurrentRows).ToTable();
                                                    }
                                                    BIn = new DateTime(FromDate.Year, FromDate.Month, FromDate.Day, BIn.Hour, BIn.Minute, BIn.Second);
                                                    EIn = new DateTime(FromDate.Year, FromDate.Month, FromDate.Day, EIn.Hour, EIn.Minute, EIn.Second);
                                                    TempIn = new DataView(TempIn, "Event_Time>='" + BIn + "'  and Event_Time<='" + EIn + "'", "Event_Time", DataViewRowState.CurrentRows).ToTable();
                                                }
                                                else
                                                {
                                                    if (WithKeyPref == "Yes")
                                                    {
                                                        TempIn = new DataView(dtLog, "Func_Code='" + InKey + "' and Event_Date>='" + FromDate.ToString() + "'  and Event_Date<='" + FromDate.AddDays(1).ToString() + "'", "Event_Time", DataViewRowState.CurrentRows).ToTable();
                                                    }
                                                    else
                                                    {
                                                        TempIn = new DataView(dtLog, "Event_Date>='" + FromDate.ToString() + "'  and Event_Date<='" + FromDate.AddDays(1).ToString() + "'", "Event_Time", DataViewRowState.CurrentRows).ToTable();
                                                    }
                                                    BIn = new DateTime(FromDate.Year, FromDate.Month, FromDate.Day, BIn.Hour, BIn.Minute, BIn.Second);
                                                    EIn = new DateTime(FromDate.AddDays(1).Year, FromDate.AddDays(1).Month, FromDate.AddDays(1).Day, EIn.Hour, EIn.Minute, EIn.Second);

                                                    TempIn = new DataView(TempIn, "Event_Time>='" + BIn + "'  and Event_Time<='" + EIn + "'", "Event_Time", DataViewRowState.CurrentRows).ToTable();

                                                }
                                            }
                                                if (TempIn.Rows.Count > 0)
                                                {

                                                    InTime = TempIn.Rows[0]["Event_Time"].ToString();
                                                }
                                                else
                                                {
                                                    InTime = "1/1/1900";
                                                }


                                                // Out Time

                                                if (Convert.ToDateTime(BeginingIn) <= Convert.ToDateTime(EndingOut))
                                                {
                                                    if (WithKeyPref == "Yes")
                                                    {
                                                        TempOut = new DataView(dtLog, "Func_Code='" + OutKey + "' and  Event_Date='" + FromDate.ToString() + "'", "Event_Time", DataViewRowState.CurrentRows).ToTable();
                                                  
                                                    }
                                                    else
                                                    {
                                                        TempOut = new DataView(dtLog, "Event_Date='" + FromDate.ToString() + "'", "Event_Time", DataViewRowState.CurrentRows).ToTable();
                                                    }
                                                    BOut = new DateTime(FromDate.Year, FromDate.Month, FromDate.Day, BOut.Hour, BOut.Minute, BOut.Second);
                                                    EOut = new DateTime(FromDate.Year, FromDate.Month, FromDate.Day, EOut.Hour, EOut.Minute, EOut.Second);
                                                    TempOut = new DataView(TempOut, "Event_Time>='" + BOut + "' and Event_Time<='" + EOut + "'", "Event_Time", DataViewRowState.CurrentRows).ToTable();

                                                }
                                                else
                                                {
                                                    if (Convert.ToDateTime(BeginingOut) <= Convert.ToDateTime(EndingOut))
                                                    {
                                                        if (WithKeyPref == "Yes") 
                                                        {
                                                            TempOut = new DataView(dtLog, "Func_Code='" + OutKey + "' and Event_Date='" + FromDate.AddDays(1).ToString() + "'", "Event_Time", DataViewRowState.CurrentRows).ToTable();
                                                
                                                        }
                                                        else
                                                        {
                                                            TempOut = new DataView(dtLog, "Event_Date='" + FromDate.AddDays(1).ToString() + "'", "Event_Time", DataViewRowState.CurrentRows).ToTable();
                                                        }
                                                        BOut = new DateTime(FromDate.AddDays(1).Year, FromDate.AddDays(1).Month, FromDate.AddDays(1).Day, BOut.Hour, BOut.Minute, BOut.Second);
                                                        EOut = new DateTime(FromDate.AddDays(1).Year, FromDate.AddDays(1).Month, FromDate.AddDays(1).Day, EOut.Hour, EOut.Minute, EOut.Second);


                                                        TempOut = new DataView(TempOut, "Event_Time>='" + BOut + "' and Event_Time<='" + EOut + "'", "Event_Time", DataViewRowState.CurrentRows).ToTable();

                                                    }
                                                    else
                                                    {
                                                        if (WithKeyPref == "Yes")
                                                        {
                                                            TempOut = new DataView(dtLog,"Func_Code='"+OutKey+"' and Event_Date>='" + FromDate.ToString() + "'  and Event_Date<='" + FromDate.AddDays(1).ToString() + "'", "Event_Time", DataViewRowState.CurrentRows).ToTable();
                                                        }
                                                        else
                                                        {
                                                            TempOut = new DataView(dtLog, "Event_Date>='" + FromDate.ToString() + "'  and Event_Date<='" + FromDate.AddDays(1).ToString() + "'", "Event_Time", DataViewRowState.CurrentRows).ToTable();
                                                        }
                                                        BOut = new DateTime(FromDate.Year, FromDate.Month, FromDate.Day, BOut.Hour, BOut.Minute, BOut.Second);
                                                        EOut = new DateTime(FromDate.AddDays(1).Year, FromDate.AddDays(1).Month, FromDate.AddDays(1).Day, EOut.Hour, EOut.Minute, EOut.Second);


                                                        TempOut = new DataView(TempOut, "Event_Time>='" + BOut + "' and Event_Time<='" + EOut + "'", "Event_Time", DataViewRowState.CurrentRows).ToTable();

                                                    }
                                                }

                                                if (TempOut.Rows.Count > 0)
                                                {

                                                    OutTime = TempOut.Rows[0]["Event_Time"].ToString();
                                                }
                                                else
                                                {
                                                    OutTime = "1/1/1900";
                                                }
                                                if (InTime == "1/1/1900" || OutTime == "1/1/1900")
                                                {
                                                    if (Convert.ToDateTime(BeginingIn) <= Convert.ToDateTime(EndingOut))
                                                    {
                                                        TempInOut = new DataView(dtLog, "Event_Date='" + FromDate.ToString() + "'", "Event_Time", DataViewRowState.CurrentRows).ToTable();

                                                        BIn = new DateTime(FromDate.Year, FromDate.Month, FromDate.Day, BIn.Hour, BIn.Minute, BIn.Second);
                                                        EOut = new DateTime(FromDate.Year, FromDate.Month, FromDate.Day, EOut.Hour, EOut.Minute, EOut.Second);

                                                        TempInOut = new DataView(TempInOut, "Event_Time>='" + BIn + "' and Event_Time<='" + EOut + "'", "Event_Time", DataViewRowState.CurrentRows).ToTable();

                                                    }
                                                    else
                                                    {
                                                        TempInOut = new DataView(dtLog, "Event_Date>='" + FromDate.ToString() + "'  and Event_Date<='" + FromDate.AddDays(1).ToString() + "'", "Event_Time", DataViewRowState.CurrentRows).ToTable();
                                                        BIn = new DateTime(FromDate.Year, FromDate.Month, FromDate.Day, BIn.Hour, BIn.Minute, BIn.Second);
                                                        EOut = new DateTime(FromDate.AddDays(1).Year, FromDate.AddDays(1).Month, FromDate.AddDays(1).Day, EOut.Hour, EOut.Minute, EOut.Second);

                                                        TempInOut = new DataView(TempInOut, "Event_Time>='" + BIn + "' and Event_Time<='" + EOut + "'", "Event_Time", DataViewRowState.CurrentRows).ToTable();

                                                    }
                                                    if (TempInOut.Rows.Count > 0)
                                                    {
                                                        if (WithKeyPref == "Yes")
                                                        {
                                                            DataTable DtIn = new DataView(TempInOut, "Func_Code='" + InKey + "'", "Event_Time", DataViewRowState.CurrentRows).ToTable();
                                                            DataTable DtOut = new DataView(TempInOut, "Func_Code='" + OutKey + "'", "Event_Time", DataViewRowState.CurrentRows).ToTable();
                                                            try
                                                            {
                                                                InTime = DtIn.Rows[0]["Event_Time"].ToString();
                                                            }
                                                            catch
                                                            {
                                                            }
                                                            try
                                                            {
                                                                OutTime = DtOut.Rows[DtOut.Rows.Count - 1]["Event_Time"].ToString();
                                                            }
                                                            catch
                                                            {


                                                            }
                                                        }
                                                        else
                                                        {
                                                            InTime = TempInOut.Rows[0]["Event_Time"].ToString();
                                                            OutTime = TempInOut.Rows[TempInOut.Rows.Count - 1]["Event_Time"].ToString();
                                                        }
                                                    }
                                                }
                                            
                                        //Here Count WeekOffMin
                                        TempIn = new DataTable();
                                        TempOut = new DataTable();
                                        TempInOut = new DataTable();
                                        if (WorkCalMethod == "PairWise")
                                        {

                                             if (Convert.ToDateTime(BeginingIn) <= Convert.ToDateTime(EndingOut))
                                                {//same day log
                                                    TempInOut = new DataView(dtLog, "Event_Date='" + FromDate.ToString() + "'   and  Event_Time>='" + InTime + "' and Event_Time <='" + OutTime + "'", "Event_Time", DataViewRowState.CurrentRows).ToTable();
                                                    TempInOut = new DataView(dtLog, "Event_Time>='" + InTime + "' and Event_Time <='" + OutTime + "'", "Event_Time", DataViewRowState.CurrentRows).ToTable();
                                                   
                                                   
                                                    OnTime = new DateTime(FromDate.Year,FromDate.Month,FromDate.Day,OnTime.Hour,OnTime.Minute,OnTime.Second);
                                                    OffTime = new DateTime(FromDate.Year, FromDate.Month, FromDate.Day, OffTime.Hour, OffTime.Minute, OffTime.Second);
                                                
                                                }
                                                else
                                                {//next day and same day log

                                                    TempInOut = new DataView(dtLog, "Event_Date>='" + FromDate.ToString() + "'  and Event_Date<='" + FromDate.AddDays(1).ToString() + "'", "Event_Time", DataViewRowState.CurrentRows).ToTable();


                                                    TempInOut = new DataView(dtLog, "Event_Time>='" + InTime + "' and Event_Time <='" + OutTime + "'", "Event_Time", DataViewRowState.CurrentRows).ToTable();
                                                    OnTime = new DateTime(FromDate.Year, FromDate.Month, FromDate.Day, OnTime.Hour, OnTime.Minute, OnTime.Second);
                                                    OffTime = new DateTime(FromDate.AddDays(1).Year, FromDate.AddDays(1).Month, FromDate.AddDays(1).Day, OffTime.Hour, OffTime.Minute, OffTime.Second);
                                                
                                                }
                                                DateTime InTimeF = Convert.ToDateTime("1/1/1900");
                                                DateTime OutTimeF = Convert.ToDateTime("1/1/1900");
                                                InTime = Convert.ToDateTime("1/1/1900").ToString();
                                                for (int l = 0; l < TempInOut.Rows.Count; l++)
                                                {
                                                    if (TempInOut.Rows.Count == 1)
                                                    {
                                                        InTime = TempInOut.Rows[l]["Event_Time"].ToString();
                                                    }
                                                    else if (TempInOut.Rows.Count > 1 && l == 0)
                                                    {
                                                        InTime = TempInOut.Rows[l]["Event_Time"].ToString();
                                                        InTimeF = Convert.ToDateTime(TempInOut.Rows[l]["Event_Time"].ToString());
                                                        OutTimeF = Convert.ToDateTime(TempInOut.Rows[l + 1]["Event_Time"].ToString());
                                                        WorkMin += GetTimeDifference(InTimeF, OutTimeF);
                                                    }
                                                    else if (l % 2 == 0)
                                                    {
                                                        try
                                                        {
                                                            InTimeF = Convert.ToDateTime(TempInOut.Rows[l]["Event_Time"].ToString());
                                                            OutTimeF = Convert.ToDateTime(TempInOut.Rows[l + 1]["Event_Time"].ToString());
                                                            WorkMin += GetTimeDifference(InTimeF, OutTimeF);
                                                        }
                                                        catch
                                                        {
                                                            
                                                        }
                                                    }





                                                }

                                                InTimeF = Convert.ToDateTime(InTime);
                                                OverTimeMin = WorkMin;
                                            objAttReg.InsertAttendanceRegister(Session["CompId"].ToString(), empidlist.Split(',')[i].ToString(), FromDate.ToString(), "0", false.ToString(), "0", Convert.ToDateTime("1/1/1900").ToString(), Convert.ToDateTime("1/1/1900").ToString(), InTimeF.ToString(), OutTimeF.ToString(), false.ToString(), "0", "0", "0", false.ToString(), "0", "0", "0", true.ToString(), false.ToString(), false.ToString(), false.ToString(), OverTimeMin.ToString(), "0", "0", "0", "0", GetAssignWorkMin(empidlist.Split(',')[i].ToString()), GetAssignWorkMin(empidlist.Split(',')[i].ToString()), "", "", "", "", "", true.ToString(), DateTime.Now.ToString(), true.ToString(), Session["UserId"].ToString(), DateTime.Now.ToString(), Session["UserId"].ToString(), DateTime.Now.ToString());

                                            
                                        }
                                        else
                                        {
                                            //Inout WeekOffMin

                                             DateTime InTimeF = Convert.ToDateTime("1/1/1900");
                                                DateTime OutTimeF = Convert.ToDateTime("1/1/1900");

                                                if (Convert.ToDateTime(BeginingIn) <= Convert.ToDateTime(EndingOut))
                                                {//same day log
                                                    TempInOut = new DataView(dtLog, "Event_Date='" + FromDate.ToString() + "'   and  Event_Time>='" + InTime + "' and Event_Time <='" + OutTime + "'", "Event_Time", DataViewRowState.CurrentRows).ToTable();

                                                    OnTime = new DateTime(FromDate.Year, FromDate.Month, FromDate.Day, OnTime.Hour, OnTime.Minute, OnTime.Second);
                                                    OffTime = new DateTime(FromDate.Year, FromDate.Month, FromDate.Day, OffTime.Hour, OffTime.Minute, OffTime.Second);
                                                
                                                }
                                                else
                                                {//next day and same day log

                                                    TempInOut = new DataView(dtLog, "Event_Date>='" + FromDate.ToString() + "'  and Event_Date<='" + FromDate.AddDays(1).ToString() + "'", "Event_Time", DataViewRowState.CurrentRows).ToTable();


                                                    TempInOut = new DataView(dtLog, "Event_Time>='" + InTime + "' and Event_Time <='" + OutTime + "'", "Event_Time", DataViewRowState.CurrentRows).ToTable();
                                                    OnTime = new DateTime(FromDate.Year, FromDate.Month, FromDate.Day, OnTime.Hour, OnTime.Minute, OnTime.Second);
                                                    OffTime = new DateTime(FromDate.AddDays(1).Year, FromDate.AddDays(1).Month, FromDate.AddDays(1).Day, OffTime.Hour, OffTime.Minute, OffTime.Second);
                                                
                                                }

                                                if (TempInOut.Rows.Count > 0)
                                                {
                                                    if (WithKeyPref == "Yes")
                                                    {
                                                        DataTable DtIn = new DataView(TempInOut, "Func_Code='" + InKey + "'", "Event_Time", DataViewRowState.CurrentRows).ToTable();
                                                        DataTable DtOut = new DataView(TempInOut, "Func_Code='" + OutKey + "'", "Event_Time", DataViewRowState.CurrentRows).ToTable();
                                                        InTimeF = Convert.ToDateTime(DtIn.Rows[0]["Event_Time"].ToString());
                                                        OutTimeF = Convert.ToDateTime(DtOut.Rows[DtOut.Rows.Count - 1]["Event_Time"].ToString());
                                                    }
                                                    else
                                                    {

                                                        InTimeF = Convert.ToDateTime(TempInOut.Rows[0]["Event_Time"].ToString());
                                                        OutTimeF = Convert.ToDateTime(TempInOut.Rows[TempInOut.Rows.Count - 1]["Event_Time"].ToString());
                                                    }

                                                }





                                            OverTimeMin = GetTimeDifference(InTimeF, OutTimeF); ;
                                            objAttReg.InsertAttendanceRegister(Session["CompId"].ToString(), empidlist.Split(',')[i].ToString(), FromDate.ToString(), "0", false.ToString(), "0", Convert.ToDateTime("1/1/1900").ToString(), Convert.ToDateTime("1/1/1900").ToString(), InTimeF.ToString(), OutTimeF.ToString(), false.ToString(), "0", "0", "0", false.ToString(), "0", "0", "0", true.ToString(), false.ToString(), false.ToString(), false.ToString(), OverTimeMin.ToString(), "0", "0", "0", "0", GetAssignWorkMin(empidlist.Split(',')[i].ToString()), GetAssignWorkMin(empidlist.Split(',')[i].ToString()), "", "", "", "", "", true.ToString(), DateTime.Now.ToString(), true.ToString(), Session["UserId"].ToString(), DateTime.Now.ToString(), Session["UserId"].ToString(), DateTime.Now.ToString());


                                        }
                                        //
                                    }
                                    else
                                    {
                                        objAttReg.InsertAttendanceRegister(Session["CompId"].ToString(), empidlist.Split(',')[i].ToString(), FromDate.ToString(), "0", false.ToString(), "0", "1/1/1900", "1/1/1900", "1/1/1990","1/1/1900", false.ToString(), "0", "0", "0", false.ToString(), "0", "0", "0", true.ToString(), false.ToString(), false.ToString(), false.ToString(), "0", "0", "0", "0", "0", GetAssignWorkMin(empidlist.Split(',')[i].ToString()), GetAssignWorkMin(empidlist.Split(',')[i].ToString()), "", "", "", "", "", true.ToString(), DateTime.Now.ToString(), true.ToString(), Session["UserId"].ToString(), DateTime.Now.ToString(), Session["UserId"].ToString(), DateTime.Now.ToString());


                                    }

                                }

                            }
                        
                        }
                        else
                        {
                            // Employee Has Not Schedule
                            if (DefaultShiftId != "" && DefaultShiftId != "0")
                                {
                                // Means Default Shift Exists in COmpany

                                //// Here we will find company level week off

                                //// This information will also get with company level
                                //// Log Data
                                //// From Date -1 From +1
                                //DataTable dtLogOnDate = new DataTable();
                                //dtLogOnDate = new DataView(dtLog, "Event_Date>='" + FromDate.AddDays(-1).ToString() + "'  and Event_Date<='" + FromDate.AddDays(1).ToString() + "'", "Event_Time", DataViewRowState.CurrentRows).ToTable();


                             // / How many time table has in that shift
                                for (int t = 0; t < dtShift.Rows.Count; t++)
                                {
                                    OnDutyTime = GetTime24(dtShift.Rows[t]["OnDuty_Time"].ToString());
                                    OffDutyTime = GetTime24(dtShift.Rows[t]["OffDuty_Time"].ToString());
                                    BeginingIn = dtShift.Rows[t]["Beginning_In"].ToString();
                                    EndingIn = dtShift.Rows[t]["Ending_In"].ToString();
                                    BeginingOut = dtShift.Rows[t]["Beginning_Out"].ToString();
                                    EndingOut = dtShift.Rows[t]["Ending_Out"].ToString();

                                    BIn = Convert.ToDateTime(dtShift.Rows[t]["Beginning_In"].ToString());
                                    EIn = Convert.ToDateTime(dtShift.Rows[t]["Ending_In"].ToString());
                                    BOut = Convert.ToDateTime(dtShift.Rows[t]["Beginning_Out"].ToString());
                                    EOut = Convert.ToDateTime(dtShift.Rows[t]["Ending_Out"].ToString());
                                    OnTime = Convert.ToDateTime(dtShift.Rows[t]["OnDuty_Time"].ToString());
                                    OffTime = Convert.ToDateTime(dtShift.Rows[t]["OffDuty_Time"].ToString());
            
                                    AssignMin = int.Parse(GetAssignWorkMin(empidlist.Split(',')[i].ToString()));


                                    //

                                    if (Convert.ToDateTime(BeginingIn) <= Convert.ToDateTime(EndingOut))
                                    {
                                        TempInOut = new DataView(dtLog, "Event_Date='" + FromDate.ToString() + "'", "Event_Time", DataViewRowState.CurrentRows).ToTable();

                                    }
                                    else
                                    {
                                        TempInOut = new DataView(dtLog, "Event_Date>='" + FromDate.ToString() + "'  and Event_Date<='" + FromDate.AddDays(1).ToString() + "'", "Event_Time", DataViewRowState.CurrentRows).ToTable();

                                    }
                                    //If Log Exists
                                    if (TempInOut.Rows.Count > 0)
                                    {
                                        //InTime

                                        if (Convert.ToDateTime(BeginingIn) <= Convert.ToDateTime(EndingIn))
                                        {

                                            if (WithKeyPref == "Yes")
                                            {
                                                TempIn = new DataView(dtLog, "Event_Date='" + FromDate.ToString() + "' and Func_Code='" + InKey + "' ", "Event_Time", DataViewRowState.CurrentRows).ToTable();
                                            }
                                            else
                                            {
                                                TempIn = new DataView(dtLog, "Event_Date='" + FromDate.ToString() + "'", "Event_Time", DataViewRowState.CurrentRows).ToTable();
                                            }
                                            BIn = new DateTime(FromDate.Year, FromDate.Month, FromDate.Day, BIn.Hour, BIn.Minute, BIn.Second);
                                            EIn = new DateTime(FromDate.Year, FromDate.Month, FromDate.Day, EIn.Hour, EIn.Minute, EIn.Second);
                                            TempIn = new DataView(TempIn, "Event_Time>='" + BIn + "'  and Event_Time<='" + EIn + "'", "Event_Time", DataViewRowState.CurrentRows).ToTable();
                                        }
                                        else
                                        {
                                            if (WithKeyPref == "Yes")
                                            {
                                                TempIn = new DataView(dtLog, "Func_Code='" + InKey + "' and Event_Date>='" + FromDate.ToString() + "'  and Event_Date<='" + FromDate.AddDays(1).ToString() + "'", "Event_Time", DataViewRowState.CurrentRows).ToTable();
                                            }
                                            else
                                            {
                                                TempIn = new DataView(dtLog, "Event_Date>='" + FromDate.ToString() + "'  and Event_Date<='" + FromDate.AddDays(1).ToString() + "'", "Event_Time", DataViewRowState.CurrentRows).ToTable();
                                            }
                                            BIn = new DateTime(FromDate.Year, FromDate.Month, FromDate.Day, BIn.Hour, BIn.Minute, BIn.Second);
                                            EIn = new DateTime(FromDate.AddDays(1).Year, FromDate.AddDays(1).Month, FromDate.AddDays(1).Day, EIn.Hour, EIn.Minute, EIn.Second);

                                            TempIn = new DataView(TempIn, "Event_Time>='" + BIn + "'  and Event_Time<='" + EIn + "'", "Event_Time", DataViewRowState.CurrentRows).ToTable();

                                        }

                                        if (TempIn.Rows.Count > 0)
                                        {

                                            InTime = TempIn.Rows[0]["Event_Time"].ToString();
                                        }
                                        else
                                        {
                                            InTime = "1/1/1900";
                                        }


                                        // Out Time


                                        if (Convert.ToDateTime(BeginingIn) <= Convert.ToDateTime(EndingOut))
                                        {
                                            if (WithKeyPref == "Yes")
                                            {
                                                TempOut = new DataView(dtLog, "Func_Code='" + OutKey + "' and Event_Date='" + FromDate.ToString() + "'", "Event_Time", DataViewRowState.CurrentRows).ToTable();
                                            }
                                            else
                                            {
                                                TempOut = new DataView(dtLog, "Event_Date='" + FromDate.ToString() + "'", "Event_Time", DataViewRowState.CurrentRows).ToTable();
                                            } 
                                            BOut = new DateTime(FromDate.Year, FromDate.Month, FromDate.Day, BOut.Hour, BOut.Minute, BOut.Second);
                                            EOut = new DateTime(FromDate.Year, FromDate.Month, FromDate.Day, EOut.Hour, EOut.Minute, EOut.Second);
                                            TempOut = new DataView(TempOut, "Event_Time>='" + BOut + "' and Event_Time<='" + EOut + "'", "Event_Time", DataViewRowState.CurrentRows).ToTable();

                                        }
                                        else
                                        {
                                            if (Convert.ToDateTime(BeginingOut) <= Convert.ToDateTime(EndingOut))
                                            {
                                                if (WithKeyPref == "Yes")
                                                {
                                                    TempOut = new DataView(dtLog, "Func_Code='" + OutKey + "' and Event_Date='" + FromDate.AddDays(1).ToString() + "'", "Event_Time", DataViewRowState.CurrentRows).ToTable();
                                                }
                                                else
                                                {
                                                    TempOut = new DataView(dtLog, "Event_Date='" + FromDate.AddDays(1).ToString() + "'", "Event_Time", DataViewRowState.CurrentRows).ToTable();
                                                }
                                                BOut = new DateTime(FromDate.AddDays(1).Year, FromDate.AddDays(1).Month, FromDate.AddDays(1).Day, BOut.Hour, BOut.Minute, BOut.Second);
                                                EOut = new DateTime(FromDate.AddDays(1).Year, FromDate.AddDays(1).Month, FromDate.AddDays(1).Day, EOut.Hour, EOut.Minute, EOut.Second);


                                                TempOut = new DataView(TempOut, "Event_Time>='" + BOut + "' and Event_Time<='" + EOut + "'", "Event_Time", DataViewRowState.CurrentRows).ToTable();

                                            }
                                            else
                                            {
                                                if (WithKeyPref == "Yes")
                                                {
                                                    TempOut = new DataView(dtLog, "Func_Code='" + OutKey + "' and Event_Date>='" + FromDate.ToString() + "'  and Event_Date<='" + FromDate.AddDays(1).ToString() + "'", "Event_Time", DataViewRowState.CurrentRows).ToTable();
                                                }
                                                else
                                                {
                                                    TempOut = new DataView(dtLog, "Event_Date>='" + FromDate.ToString() + "'  and Event_Date<='" + FromDate.AddDays(1).ToString() + "'", "Event_Time", DataViewRowState.CurrentRows).ToTable();
                                                }
                                                BOut = new DateTime(FromDate.Year, FromDate.Month, FromDate.Day, BOut.Hour, BOut.Minute, BOut.Second);
                                                EOut = new DateTime(FromDate.AddDays(1).Year, FromDate.AddDays(1).Month, FromDate.AddDays(1).Day, EOut.Hour, EOut.Minute, EOut.Second);


                                                TempOut = new DataView(TempOut, "Event_Time>='" + BOut + "' and Event_Time<='" + EOut + "'", "Event_Time", DataViewRowState.CurrentRows).ToTable();

                                            }
                                        }

                                        if (TempOut.Rows.Count > 0)
                                        {

                                            OutTime = TempOut.Rows[TempOut.Rows.Count-1]["Event_Time"].ToString();
                                        }
                                        else
                                        {
                                            OutTime = "1/1/1900";
                                        }

                                        if (InTime == "1/1/1900" || OutTime == "1/1/1900")
                                        {
                                            if (Convert.ToDateTime(BeginingIn) <= Convert.ToDateTime(EndingOut))
                                            {
                                                TempInOut = new DataView(dtLog, "Event_Date='" + FromDate.ToString() + "'", "Event_Time", DataViewRowState.CurrentRows).ToTable();

                                                BIn = new DateTime(FromDate.Year, FromDate.Month, FromDate.Day, BIn.Hour, BIn.Minute, BIn.Second);
                                                EOut = new DateTime(FromDate.Year, FromDate.Month, FromDate.Day, EOut.Hour, EOut.Minute, EOut.Second);

                                                TempInOut = new DataView(TempInOut, "Event_Time>='" + BIn + "' and Event_Time<='" + EOut + "'", "Event_Time", DataViewRowState.CurrentRows).ToTable();

                                            }
                                            else
                                            {
                                                TempInOut = new DataView(dtLog, "Event_Date>='" + FromDate.ToString() + "'  and Event_Date<='" + FromDate.AddDays(1).ToString() + "'", "Event_Time", DataViewRowState.CurrentRows).ToTable();
                                                BIn = new DateTime(FromDate.Year, FromDate.Month, FromDate.Day, BIn.Hour, BIn.Minute, BIn.Second);
                                                EOut = new DateTime(FromDate.AddDays(1).Year, FromDate.AddDays(1).Month, FromDate.AddDays(1).Day, EOut.Hour, EOut.Minute, EOut.Second);

                                                TempInOut = new DataView(TempInOut, "Event_Time>='" + BIn + "' and Event_Time<='" + EOut + "'", "Event_Time", DataViewRowState.CurrentRows).ToTable();

                                            }
                                            if (TempInOut.Rows.Count > 0)
                                            {
                                                if (WithKeyPref == "Yes")
                                                {
                                                    DataTable DtIn = new DataView(TempInOut, "Func_Code='" + InKey + "'", "Event_Time", DataViewRowState.CurrentRows).ToTable();
                                                    DataTable DtOut = new DataView(TempInOut, "Func_Code='" + OutKey + "'", "Event_Time", DataViewRowState.CurrentRows).ToTable();
                                                    try
                                                    {
                                                        InTime = DtIn.Rows[0]["Event_Time"].ToString();
                                                    }
                                                    catch
                                                    {

                                                    }
                                                    try
                                                    {
                                                        OutTime = DtOut.Rows[DtOut.Rows.Count - 1]["Event_Time"].ToString();
                                                    }
                                                    catch
                                                    {

                                                    }
                                                }
                                                else
                                                {
                                                    InTime = TempInOut.Rows[0]["Event_Time"].ToString();
                                                    OutTime = TempInOut.Rows[TempOut.Rows.Count - 1]["Event_Time"].ToString();
                                                }
                                            }
                                        }

                                        if (InTime == "1/1/1900" && OutTime != "1/1/1900")
                                        {
                                            if (NoClockIn)
                                            {
                                                if (objEmpHoliday.GetEmployeeHolidayOnDateAndEmpId(FromDate.ToString(), empidlist.Split(',')[i].ToString()))
                                                {
                                                    objAttReg.InsertAttendanceRegister(Session["CompId"].ToString(), empidlist.Split(',')[i].ToString(), FromDate.ToString(), dtShift.Rows[t]["Shift_Id"].ToString(), false.ToString(), dtShift.Rows[t]["TimeTable_Id"].ToString(), dtShift.Rows[t]["OnDuty_Time"].ToString(), dtShift.Rows[t]["OffDuty_Time"].ToString(), "1/1/1990", "1/1/1900", false.ToString(), "0", "0", "0", false.ToString(), "0", "0", "0", false.ToString(), true.ToString(), false.ToString(), false.ToString(), "0", "0", "0", "0", "0", AssignMin.ToString(), AssignMin.ToString(), "", "", "", "", "", true.ToString(), DateTime.Now.ToString(), true.ToString(), Session["UserId"].ToString(), DateTime.Now.ToString(), Session["UserId"].ToString(), DateTime.Now.ToString());
                                                    continue;
                                                }
                                                else
                                                {
                                                    objAttReg.InsertAttendanceRegister(Session["CompId"].ToString(), empidlist.Split(',')[i].ToString(), FromDate.ToString(), dtShift.Rows[t]["Shift_Id"].ToString(), false.ToString(), dtShift.Rows[t]["TimeTable_Id"].ToString(), dtShift.Rows[t]["OnDuty_Time"].ToString(), dtShift.Rows[t]["OffDuty_Time"].ToString(), "1/1/1990", OutTime.ToString(), false.ToString(), "0", "0", "0", false.ToString(), "0", "0", "0", false.ToString(), false.ToString(), false.ToString(), true.ToString(), "0", "0", "0", "0", "0", "0", AssignMin.ToString(), "", "", "", "", "", true.ToString(), DateTime.Now.ToString(), true.ToString(), Session["UserId"].ToString(), DateTime.Now.ToString(), Session["UserId"].ToString(), DateTime.Now.ToString());
                                                    continue;
                                                }
                                            }
                                            else
                                            {
                                                if (objEmpHoliday.GetEmployeeHolidayOnDateAndEmpId(FromDate.ToString(), empidlist.Split(',')[i].ToString()))
                                                {
                                                    objAttReg.InsertAttendanceRegister(Session["CompId"].ToString(), empidlist.Split(',')[i].ToString(), FromDate.ToString(), dtShift.Rows[t]["Shift_Id"].ToString(), false.ToString(), dtShift.Rows[t]["TimeTable_Id"].ToString(), dtShift.Rows[t]["OnDuty_Time"].ToString(), dtShift.Rows[t]["OffDuty_Time"].ToString(), "1/1/1990", "1/1/1900", false.ToString(), "0", "0", "0", false.ToString(), "0", "0", "0", false.ToString(), true.ToString(), false.ToString(), false.ToString(), "0", "0", "0", "0", "0", AssignMin.ToString(), AssignMin.ToString(), "", "", "", "", "", true.ToString(), DateTime.Now.ToString(), true.ToString(), Session["UserId"].ToString(), DateTime.Now.ToString(), Session["UserId"].ToString(), DateTime.Now.ToString());
                                                    continue;
                                                }
                                                else
                                                {
                                                    objAttReg.InsertAttendanceRegister(Session["CompId"].ToString(), empidlist.Split(',')[i].ToString(), FromDate.ToString(), dtShift.Rows[t]["Shift_Id"].ToString(), false.ToString(), dtShift.Rows[t]["TimeTable_Id"].ToString(), dtShift.Rows[t]["OnDuty_Time"].ToString(), dtShift.Rows[t]["OffDuty_Time"].ToString(), "1/1/1990", OutTime.ToString(), false.ToString(), "0", "0", "0", false.ToString(), "0", "0", "0", false.ToString(), false.ToString(), false.ToString(), false.ToString(), "0", "0", "0", "0", "0", "0", AssignMin.ToString(), "", "", "", "", "", true.ToString(), DateTime.Now.ToString(), true.ToString(), Session["UserId"].ToString(), DateTime.Now.ToString(), Session["UserId"].ToString(), DateTime.Now.ToString());
                                                    continue;
                                                }

                                            }
                                        }

                                        if (InTime != "1/1/1900" && OutTime == "1/1/1900")
                                        {
                                            if (NoClockOut)
                                            {
                                                if (objEmpHoliday.GetEmployeeHolidayOnDateAndEmpId(FromDate.ToString(), empidlist.Split(',')[i].ToString()))
                                                {
                                                    objAttReg.InsertAttendanceRegister(Session["CompId"].ToString(), empidlist.Split(',')[i].ToString(), FromDate.ToString(), dtShift.Rows[t]["Shift_Id"].ToString(), false.ToString(), dtShift.Rows[t]["TimeTable_Id"].ToString(), dtShift.Rows[t]["OnDuty_Time"].ToString(), dtShift.Rows[t]["OffDuty_Time"].ToString(), "1/1/1990", "1/1/1900", false.ToString(), "0", "0", "0", false.ToString(), "0", "0", "0", false.ToString(), true.ToString(), false.ToString(), false.ToString(), "0", "0", "0", "0", "0", AssignMin.ToString(), AssignMin.ToString(), "", "", "", "", "", true.ToString(), DateTime.Now.ToString(), true.ToString(), Session["UserId"].ToString(), DateTime.Now.ToString(), Session["UserId"].ToString(), DateTime.Now.ToString());
                                                    continue;
                                                }
                                                else
                                                {
                                                    objAttReg.InsertAttendanceRegister(Session["CompId"].ToString(), empidlist.Split(',')[i].ToString(), FromDate.ToString(), dtShift.Rows[t]["Shift_Id"].ToString(), false.ToString(), dtShift.Rows[t]["TimeTable_Id"].ToString(), dtShift.Rows[t]["OnDuty_Time"].ToString(), dtShift.Rows[t]["OffDuty_Time"].ToString(), InTime.ToString(), "1/1/1990", false.ToString(), "0", "0", "0", false.ToString(), "0", "0", "0", false.ToString(), false.ToString(), false.ToString(), true.ToString(), "0", "0", "0", "0", "0", "0", AssignMin.ToString(), "", "", "", "", "", true.ToString(), DateTime.Now.ToString(), true.ToString(), Session["UserId"].ToString(), DateTime.Now.ToString(), Session["UserId"].ToString(), DateTime.Now.ToString());
                                                    continue;
                                                }
                                            }
                                            else
                                            {
                                                if (objEmpHoliday.GetEmployeeHolidayOnDateAndEmpId(FromDate.ToString(), empidlist.Split(',')[i].ToString()))
                                                {
                                                    objAttReg.InsertAttendanceRegister(Session["CompId"].ToString(), empidlist.Split(',')[i].ToString(), FromDate.ToString(), dtShift.Rows[t]["Shift_Id"].ToString(), false.ToString(), dtShift.Rows[t]["TimeTable_Id"].ToString(), dtShift.Rows[t]["OnDuty_Time"].ToString(), dtShift.Rows[t]["OffDuty_Time"].ToString(), "1/1/1990", "1/1/1900", false.ToString(), "0", "0", "0", false.ToString(), "0", "0", "0", false.ToString(), true.ToString(), false.ToString(), false.ToString(), "0", "0", "0", "0", "0", AssignMin.ToString(), AssignMin.ToString(), "", "", "", "", "", true.ToString(), DateTime.Now.ToString(), true.ToString(), Session["UserId"].ToString(), DateTime.Now.ToString(), Session["UserId"].ToString(), DateTime.Now.ToString());
                                                    continue;
                                                }
                                                else
                                                {
                                                    objAttReg.InsertAttendanceRegister(Session["CompId"].ToString(), empidlist.Split(',')[i].ToString(), FromDate.ToString(), dtShift.Rows[t]["Shift_Id"].ToString(), false.ToString(), dtShift.Rows[t]["TimeTable_Id"].ToString(), dtShift.Rows[t]["OnDuty_Time"].ToString(), dtShift.Rows[t]["OffDuty_Time"].ToString(), InTime.ToString(), "1/1/1990", false.ToString(), "0", "0", "0", false.ToString(), "0", "0", "0", false.ToString(), false.ToString(), false.ToString(), false.ToString(), "0", "0", "0", "0", "0", "0", AssignMin.ToString(), "", "", "", "", "", true.ToString(), DateTime.Now.ToString(), true.ToString(), Session["UserId"].ToString(), DateTime.Now.ToString(), Session["UserId"].ToString(), DateTime.Now.ToString());
                                                    continue;
                                                }
                                            }
                                        }
                                        if (InTime == "1/1/1900" && OutTime == "1/1/1900")
                                        {
                                            if (objEmpHoliday.GetEmployeeHolidayOnDateAndEmpId(FromDate.ToString(), empidlist.Split(',')[i].ToString()))
                                            {
                                                objAttReg.InsertAttendanceRegister(Session["CompId"].ToString(), empidlist.Split(',')[i].ToString(), FromDate.ToString(), dtShift.Rows[t]["Shift_Id"].ToString(), false.ToString(), dtShift.Rows[t]["TimeTable_Id"].ToString(), dtShift.Rows[t]["OnDuty_Time"].ToString(), dtShift.Rows[t]["OffDuty_Time"].ToString(), "1/1/1990", "1/1/1900", false.ToString(), "0", "0", "0", false.ToString(), "0", "0", "0", false.ToString(), true.ToString(), false.ToString(), false.ToString(), "0", "0", "0", "0", "0", AssignMin.ToString(), AssignMin.ToString(), "", "", "", "", "", true.ToString(), DateTime.Now.ToString(), true.ToString(), Session["UserId"].ToString(), DateTime.Now.ToString(), Session["UserId"].ToString(), DateTime.Now.ToString());

                                            }

                                            else if (ObjLeaveReq.IsLeaveOnDate(FromDate.ToString(), empidlist.Split(',')[i].ToString()))
                                            {
                                                objAttReg.InsertAttendanceRegister(Session["CompId"].ToString(), empidlist.Split(',')[i].ToString(), FromDate.ToString(), dtShift.Rows[t]["Shift_Id"].ToString(), false.ToString(), dtShift.Rows[t]["TimeTable_Id"].ToString(), dtShift.Rows[t]["OnDuty_Time"].ToString(), dtShift.Rows[t]["OffDuty_Time"].ToString(), "1/1/1990", "1/1/1900", false.ToString(), "0", "0", "0", false.ToString(), "0", "0", "0", false.ToString(), false.ToString(), true.ToString(), false.ToString(), "0", "0", "0", "0", "0", AssignMin.ToString(), AssignMin.ToString(), "", "", "", "", "", true.ToString(), DateTime.Now.ToString(), true.ToString(), Session["UserId"].ToString(), DateTime.Now.ToString(), Session["UserId"].ToString(), DateTime.Now.ToString());
                                            }

                                            else
                                            {
                                                objAttReg.InsertAttendanceRegister(Session["CompId"].ToString(), empidlist.Split(',')[i].ToString(), FromDate.ToString(), dtShift.Rows[t]["Shift_Id"].ToString(), false.ToString(), dtShift.Rows[t]["TimeTable_Id"].ToString(), dtShift.Rows[t]["OnDuty_Time"].ToString(), dtShift.Rows[t]["OffDuty_Time"].ToString(), "1/1/1990", "1/1/1900", false.ToString(), "0", "0", "0", false.ToString(), "0", "0", "0", false.ToString(), false.ToString(), false.ToString(), true.ToString(), "0", "0", "0", "0", "0", "0", AssignMin.ToString(), "", "", "", "", "", true.ToString(), DateTime.Now.ToString(), true.ToString(), Session["UserId"].ToString(), DateTime.Now.ToString(), Session["UserId"].ToString(), DateTime.Now.ToString());


                                            }
                                        }
                                        else
                                        {//Effective Work calculation
                                            TempIn = new DataTable();
                                            TempOut = new DataTable();
                                            TempInOut = new DataTable();
                                            if (WorkCalMethod == "PairWise")
                                            {
                                                if (Convert.ToDateTime(BeginingIn) <= Convert.ToDateTime(EndingOut))
                                                {//same day log
                                                    TempInOut = new DataView(dtLog, "Event_Date='" + FromDate.ToString() + "'   and  Event_Time>='" + InTime + "' and Event_Time <='" + OutTime + "'", "Event_Time", DataViewRowState.CurrentRows).ToTable();
                                                    TempInOut = new DataView(dtLog, "Event_Time>='" + InTime + "' and Event_Time <='" + OutTime + "'", "Event_Time", DataViewRowState.CurrentRows).ToTable();


                                                    OnTime = new DateTime(FromDate.Year, FromDate.Month, FromDate.Day, OnTime.Hour, OnTime.Minute, OnTime.Second);
                                                    OffTime = new DateTime(FromDate.Year, FromDate.Month, FromDate.Day, OffTime.Hour, OffTime.Minute, OffTime.Second);

                                                }
                                                else
                                                {//next day and same day log

                                                    TempInOut = new DataView(dtLog, "Event_Date>='" + FromDate.ToString() + "'  and Event_Date<='" + FromDate.AddDays(1).ToString() + "'", "Event_Time", DataViewRowState.CurrentRows).ToTable();


                                                    TempInOut = new DataView(dtLog, "Event_Time>='" + InTime + "' and Event_Time <='" + OutTime + "'", "Event_Time", DataViewRowState.CurrentRows).ToTable();
                                                    OnTime = new DateTime(FromDate.Year, FromDate.Month, FromDate.Day, OnTime.Hour, OnTime.Minute, OnTime.Second);
                                                    OffTime = new DateTime(FromDate.AddDays(1).Year, FromDate.AddDays(1).Month, FromDate.AddDays(1).Day, OffTime.Hour, OffTime.Minute, OffTime.Second);

                                                }
                                                DateTime InTimeF = Convert.ToDateTime("1/1/1900");
                                                DateTime OutTimeF = Convert.ToDateTime("1/1/1900");
                                                InTime = Convert.ToDateTime("1/1/1900").ToString();
                                                for (int l = 0; l < TempInOut.Rows.Count; l++)
                                                {

                                                    if (TempInOut.Rows.Count == 1)
                                                    {
                                                        InTime = TempInOut.Rows[l]["Event_Time"].ToString();
                                                    }
                                                    else if (TempInOut.Rows.Count > 1 && l == 0)
                                                    {
                                                        InTime = TempInOut.Rows[l]["Event_Time"].ToString();
                                                        InTimeF = Convert.ToDateTime(TempInOut.Rows[l]["Event_Time"].ToString());
                                                        OutTimeF = Convert.ToDateTime(TempInOut.Rows[l + 1]["Event_Time"].ToString());
                                                        WorkMin += GetTimeDifference(InTimeF, OutTimeF);
                                                    }
                                                    else if (l % 2 == 0)
                                                    {
                                                        try
                                                        {
                                                            InTimeF = Convert.ToDateTime(TempInOut.Rows[l]["Event_Time"].ToString());
                                                            OutTimeF = Convert.ToDateTime(TempInOut.Rows[l + 1]["Event_Time"].ToString());
                                                            WorkMin += GetTimeDifference(InTimeF, OutTimeF);
                                                        }
                                                        catch
                                                        {

                                                        }
                                                    }

                                                    
                                                }
                                                try
                                                {
                                                    InTimeF = Convert.ToDateTime(InTime);
                                                }
                                                catch
                                                {
                                                    InTimeF = Convert.ToDateTime("1/1/1900");
                                                }

                                                
                                                int PartialViolationMin = 0;
                                                int PartialMin = 0;
                                                try
                                                {
                                                    PartialMin = int.Parse(GetPartialViolationMin(empidlist.Split(',')[i].ToString(), FromDate, dtSchedule.Rows[t]["TimeTable_Id"].ToString()).Split('-')[0]);
                                                    PartialViolationMin = int.Parse(GetPartialViolationMin(empidlist.Split(',')[i].ToString(), FromDate, dtSchedule.Rows[t]["TimeTable_Id"].ToString()).Split('-')[1]);
                                                }
                                                catch
                                                {

                                                }
                                                WorkMin += PartialMin;

                                                EffectiveWorkMin = WorkMin;
                                                WorkMin = int.Parse(getWorkMinute(WorkMin.ToString(), AssignMin.ToString()));

                                                OverTimeMin = GetOverTimeMinWithOutShift(empidlist.Split(',')[i].ToString(), EffectiveWorkMin);
                                                if (WeekOff == FromDate.DayOfWeek.ToString())
                                                {
                                                    WorkMin = EffectiveWorkMin;
                                                    objAttReg.InsertAttendanceRegister(Session["CompId"].ToString(), empidlist.Split(',')[i].ToString(), FromDate.ToString(), dtShift.Rows[t]["Shift_Id"].ToString(), false.ToString(), dtShift.Rows[t]["TimeTable_Id"].ToString(), dtShift.Rows[t]["OnDuty_Time"].ToString(), dtShift.Rows[t]["OffDuty_Time"].ToString(), InTimeF.ToString(), OutTimeF.ToString(), false.ToString(), "0", "0", "0", false.ToString(), "0", "0", "0", true.ToString(), false.ToString(), false.ToString(), false.ToString(), WorkMin.ToString(), "0", "0", "0", "0", AssignMin.ToString(), AssignMin.ToString(), "", "", "", "", "", true.ToString(), DateTime.Now.ToString(), true.ToString(), Session["UserId"].ToString(), DateTime.Now.ToString(), Session["UserId"].ToString(), DateTime.Now.ToString());
                                                }
                                                else if (objEmpHoliday.GetEmployeeHolidayOnDateAndEmpId(FromDate.ToString(), empidlist.Split(',')[i].ToString()))
                                                {
                                                    WorkMin = EffectiveWorkMin;
                                                    objAttReg.InsertAttendanceRegister(Session["CompId"].ToString(), empidlist.Split(',')[i].ToString(), FromDate.ToString(), dtShift.Rows[t]["Shift_Id"].ToString(), false.ToString(), dtShift.Rows[t]["TimeTable_Id"].ToString(), dtShift.Rows[t]["OnDuty_Time"].ToString(), dtShift.Rows[t]["OffDuty_Time"].ToString(), InTimeF.ToString(), OutTimeF.ToString(), false.ToString(), "0", "0", "0", false.ToString(), "0", "0", "0", false.ToString(), true.ToString(), false.ToString(), false.ToString(), "0", WorkMin.ToString(), "0", "0", "0", AssignMin.ToString(), AssignMin.ToString(), "", "", "", "", "", true.ToString(), DateTime.Now.ToString(), true.ToString(), Session["UserId"].ToString(), DateTime.Now.ToString(), Session["UserId"].ToString(), DateTime.Now.ToString());

                                                }
                                                else
                                                {

                                                    objAttReg.InsertAttendanceRegister(Session["CompId"].ToString(), empidlist.Split(',')[i].ToString(), FromDate.ToString(), dtShift.Rows[t]["Shift_Id"].ToString(),false.ToString(), dtShift.Rows[t]["TimeTable_Id"].ToString(), dtShift.Rows[t]["OnDuty_Time"].ToString(), dtShift.Rows[t]["OffDuty_Time"].ToString(), InTimeF.ToString(), OutTimeF.ToString(), IsLate.ToString(), LateMin.ToString(), LateRelaxMin.ToString(), LatePenaltyMin.ToString(), IsEarlyOut.ToString(), EarlyMin.ToString(), EarlyRelaxMin.ToString(), EarlyPenaltyMin.ToString(), false.ToString(), false.ToString(), false.ToString(), false.ToString(), "0", "0", OverTimeMin.ToString(), PartialMin.ToString(), PartialViolationMin.ToString(), WorkMin.ToString(), AssignMin.ToString(), "", "", "", "", "", true.ToString(), DateTime.Now.ToString(), true.ToString(), Session["UserId"].ToString(), DateTime.Now.ToString(), Session["UserId"].ToString(), DateTime.Now.ToString());


                                                }
                                            }
                                            else
                                            {
                                                DateTime InTimeF = Convert.ToDateTime("1/1/1900");
                                                DateTime OutTimeF = Convert.ToDateTime("1/1/1900");

                                                if (Convert.ToDateTime(BeginingIn) <= Convert.ToDateTime(EndingOut))
                                                {//same day log
                                                    TempInOut = new DataView(dtLog, "Event_Date='" + FromDate.ToString() + "'   and  Event_Time>='" + InTime + "' and Event_Time <='" + OutTime + "'", "Event_Time", DataViewRowState.CurrentRows).ToTable();

                                                    OnTime = new DateTime(FromDate.Year, FromDate.Month, FromDate.Day, OnTime.Hour, OnTime.Minute, OnTime.Second);
                                                    OffTime = new DateTime(FromDate.Year, FromDate.Month, FromDate.Day, OffTime.Hour, OffTime.Minute, OffTime.Second);

                                                }
                                                else
                                                {//next day and same day log

                                                    TempInOut = new DataView(dtLog, "Event_Date>='" + FromDate.ToString() + "'  and Event_Date<='" + FromDate.AddDays(1).ToString() + "'", "Event_Time", DataViewRowState.CurrentRows).ToTable();


                                                    TempInOut = new DataView(dtLog, "Event_Time>='" + InTime + "' and Event_Time <='" + OutTime + "'", "Event_Time", DataViewRowState.CurrentRows).ToTable();
                                                    OnTime = new DateTime(FromDate.Year, FromDate.Month, FromDate.Day, OnTime.Hour, OnTime.Minute, OnTime.Second);
                                                    OffTime = new DateTime(FromDate.AddDays(1).Year, FromDate.AddDays(1).Month, FromDate.AddDays(1).Day, OffTime.Hour, OffTime.Minute, OffTime.Second);

                                                }

                                                if (TempInOut.Rows.Count > 0)
                                                {
                                                    if (WithKeyPref == "Yes")
                                                    {
                                                        DataTable DtIn = new DataView(TempInOut, "Func_Code='" + InKey + "'", "Event_Time", DataViewRowState.CurrentRows).ToTable();
                                                        DataTable DtOut = new DataView(TempInOut, "Func_Code='" + OutKey + "'", "Event_Time", DataViewRowState.CurrentRows).ToTable();
                                                        InTimeF = Convert.ToDateTime(DtIn.Rows[0]["Event_Time"].ToString());
                                                        OutTimeF = Convert.ToDateTime(DtOut.Rows[DtOut.Rows.Count - 1]["Event_Time"].ToString());
                                                    }
                                                    else
                                                    {
                                                        InTimeF = Convert.ToDateTime(TempInOut.Rows[0]["Event_Time"].ToString());
                                                        OutTimeF = Convert.ToDateTime(TempInOut.Rows[TempInOut.Rows.Count - 1]["Event_Time"].ToString());
                                                    }

                                                }
                                               
                                                int PartialViolationMin = 0;
                                                int PartialMin = 0;
                                                try
                                                {


                                                    PartialMin = int.Parse(GetPartialViolationMin(empidlist.Split(',')[i].ToString(), FromDate, dtSchedule.Rows[t]["TimeTable_Id"].ToString()).Split('-')[0]);
                                                    PartialViolationMin = int.Parse(GetPartialViolationMin(empidlist.Split(',')[i].ToString(), FromDate, dtSchedule.Rows[t]["TimeTable_Id"].ToString()).Split('-')[1]);

                                                }
                                                catch
                                                {

                                                }

                                                WorkMin = GetTimeDifference(InTimeF, OutTimeF);
                                                EffectiveWorkMin = WorkMin;
                                                WorkMin = int.Parse(getWorkMinute(WorkMin.ToString(), AssignMin.ToString()));
                                                OverTimeMin = GetOverTimeMinWithOutShift(empidlist.Split(',')[i].ToString(),EffectiveWorkMin);
                                                if (WeekOff == FromDate.DayOfWeek.ToString())
                                                {
                                                    WorkMin = EffectiveWorkMin;
                                                    objAttReg.InsertAttendanceRegister(Session["CompId"].ToString(), empidlist.Split(',')[i].ToString(), FromDate.ToString(), dtShift.Rows[t]["Shift_Id"].ToString(), false.ToString(), dtShift.Rows[t]["TimeTable_Id"].ToString(), dtShift.Rows[t]["OnDuty_Time"].ToString(), dtShift.Rows[t]["OffDuty_Time"].ToString(), InTimeF.ToString(), OutTimeF.ToString(), false.ToString(), "0", "0", "0", false.ToString(), "0", "0", "0", true.ToString(), false.ToString(), false.ToString(), false.ToString(), WorkMin.ToString(), "0", "0", "0", "0", AssignMin.ToString(), AssignMin.ToString(), "", "", "", "", "", true.ToString(), DateTime.Now.ToString(), true.ToString(), Session["UserId"].ToString(), DateTime.Now.ToString(), Session["UserId"].ToString(), DateTime.Now.ToString());
                                                }
                                                else if (objEmpHoliday.GetEmployeeHolidayOnDateAndEmpId(FromDate.ToString(), empidlist.Split(',')[i].ToString()))
                                                {
                                                    WorkMin = EffectiveWorkMin;
                                                    objAttReg.InsertAttendanceRegister(Session["CompId"].ToString(), empidlist.Split(',')[i].ToString(), FromDate.ToString(), dtShift.Rows[t]["Shift_Id"].ToString(), false.ToString(), dtShift.Rows[t]["TimeTable_Id"].ToString(), dtShift.Rows[t]["OnDuty_Time"].ToString(), dtShift.Rows[t]["OffDuty_Time"].ToString(), InTimeF.ToString(), OutTimeF.ToString(), false.ToString(), "0", "0", "0", false.ToString(), "0", "0", "0", false.ToString(), true.ToString(), false.ToString(), false.ToString(), "0", WorkMin.ToString(), "0", "0", "0", AssignMin.ToString(), AssignMin.ToString(), "", "", "", "", "", true.ToString(), DateTime.Now.ToString(), true.ToString(), Session["UserId"].ToString(), DateTime.Now.ToString(), Session["UserId"].ToString(), DateTime.Now.ToString());

                                                }
                                                else
                                                {
                                                    objAttReg.InsertAttendanceRegister(Session["CompId"].ToString(), empidlist.Split(',')[i].ToString(), FromDate.ToString(), dtShift.Rows[t]["Shift_Id"].ToString(), false.ToString(), dtShift.Rows[t]["TimeTable_Id"].ToString(), dtShift.Rows[t]["OnDuty_Time"].ToString(), dtShift.Rows[t]["OffDuty_Time"].ToString(), InTimeF.ToString(), OutTimeF.ToString(), IsLate.ToString(), LateMin.ToString(), LateRelaxMin.ToString(), LatePenaltyMin.ToString(), IsEarlyOut.ToString(), EarlyMin.ToString(), EarlyRelaxMin.ToString(), EarlyPenaltyMin.ToString(), false.ToString(), false.ToString(), false.ToString(), false.ToString(), "0", "0", OverTimeMin.ToString(), PartialMin.ToString(), PartialViolationMin.ToString(), WorkMin.ToString(), AssignMin.ToString(), "", "", "", "", "", true.ToString(), DateTime.Now.ToString(), true.ToString(), Session["UserId"].ToString(), DateTime.Now.ToString(), Session["UserId"].ToString(), DateTime.Now.ToString());
                                                }
                                            }

                                        }
                                    }
                                    else
                                    {

                                        if (WeekOff == FromDate.DayOfWeek.ToString())
                                        {

                                            objAttReg.InsertAttendanceRegister(Session["CompId"].ToString(), empidlist.Split(',')[i].ToString(), FromDate.ToString(), dtShift.Rows[t]["Shift_Id"].ToString(), false.ToString(), dtShift.Rows[t]["TimeTable_Id"].ToString(), dtShift.Rows[t]["OnDuty_Time"].ToString(), dtShift.Rows[t]["OffDuty_Time"].ToString(), "1/1/1990", "1/1/1900", false.ToString(), "0", "0", "0", false.ToString(), "0", "0", "0", true.ToString(), false.ToString(), false.ToString(), false.ToString(),"0", "0", "0", "0", "0", AssignMin.ToString(), AssignMin.ToString(), "", "", "", "", "", true.ToString(), DateTime.Now.ToString(), true.ToString(), Session["UserId"].ToString(), DateTime.Now.ToString(), Session["UserId"].ToString(), DateTime.Now.ToString());
                                        }
                                        else if (objEmpHoliday.GetEmployeeHolidayOnDateAndEmpId(FromDate.ToString(), empidlist.Split(',')[i].ToString()))
                                        {
                                            objAttReg.InsertAttendanceRegister(Session["CompId"].ToString(), empidlist.Split(',')[i].ToString(), FromDate.ToString(), dtShift.Rows[t]["Shift_Id"].ToString(), false.ToString(), dtShift.Rows[t]["TimeTable_Id"].ToString(), dtShift.Rows[t]["OnDuty_Time"].ToString(), dtShift.Rows[t]["OffDuty_Time"].ToString(), "1/1/1990", "1/1/1900", false.ToString(), "0", "0", "0", false.ToString(), "0", "0", "0", false.ToString(), true.ToString(), false.ToString(), false.ToString(), "0", "0", "0", "0", "0", AssignMin.ToString(), AssignMin.ToString(), "", "", "", "", "", true.ToString(), DateTime.Now.ToString(), true.ToString(), Session["UserId"].ToString(), DateTime.Now.ToString(), Session["UserId"].ToString(), DateTime.Now.ToString());

                                        }

                                        else if (ObjLeaveReq.IsLeaveOnDate(FromDate.ToString(), empidlist.Split(',')[i].ToString()))
                                        {
                                            objAttReg.InsertAttendanceRegister(Session["CompId"].ToString(), empidlist.Split(',')[i].ToString(), FromDate.ToString(), dtShift.Rows[t]["Shift_Id"].ToString(), false.ToString(), dtShift.Rows[t]["TimeTable_Id"].ToString(), dtShift.Rows[t]["OnDuty_Time"].ToString(), dtShift.Rows[t]["OffDuty_Time"].ToString(), "1/1/1990", "1/1/1900", false.ToString(), "0", "0", "0", false.ToString(), "0", "0", "0", false.ToString(), false.ToString(), true.ToString(), false.ToString(), "0", "0", "0", "0", "0", AssignMin.ToString(), AssignMin.ToString(), "", "", "", "", "", true.ToString(), DateTime.Now.ToString(), true.ToString(), Session["UserId"].ToString(), DateTime.Now.ToString(), Session["UserId"].ToString(), DateTime.Now.ToString());
                                        }

                                        else
                                        {
                                            objAttReg.InsertAttendanceRegister(Session["CompId"].ToString(), empidlist.Split(',')[i].ToString(), FromDate.ToString(), dtShift.Rows[t]["Shift_Id"].ToString(), false.ToString(), dtShift.Rows[t]["TimeTable_Id"].ToString(), dtShift.Rows[t]["OnDuty_Time"].ToString(), dtShift.Rows[t]["OffDuty_Time"].ToString(), "1/1/1990", "1/1/1900", false.ToString(), "0", "0", "0", false.ToString(), "0", "0", "0", false.ToString(), false.ToString(), false.ToString(), true.ToString(), "0", "0", "0", "0", "0", "0", AssignMin.ToString(), "", "", "", "", "", true.ToString(), DateTime.Now.ToString(), true.ToString(), Session["UserId"].ToString(), DateTime.Now.ToString(), Session["UserId"].ToString(), DateTime.Now.ToString());


                                        }
                                    }




                                    //
                                }
                            }
                            else
                            {
                                //Prahlad Khatri 
                                // We will insert absent of that day


                                objAttReg.InsertAttendanceRegister(Session["CompId"].ToString(), empidlist.Split(',')[i].ToString(), FromDate.ToString(), "0", false.ToString(), "0", "1/1/1900", "1/1/1990", "1/1/1990", "1/1/1900", false.ToString(), "0", "0", "0", false.ToString(), "0", "0", "0", false.ToString(), false.ToString(), false.ToString(), true.ToString(), "0", "0", "0", "0", "0", "0", GetAssignWorkMin(empidlist.Split(',')[i].ToString()), "", "", "", "", "", true.ToString(), DateTime.Now.ToString(), true.ToString(), Session["UserId"].ToString(), DateTime.Now.ToString(), Session["UserId"].ToString(), DateTime.Now.ToString());

                            }

                        }

                        FromDate = FromDate.AddDays(1);
                    }

                }


         
        
       
        


        DisplayMessage("Log Processed");




    }



    public string GetLateRelaxMinPenaltyMin(string empid, DateTime date, int LateMin)
    {
        string LateRelaxMinPenaltyMin = "0";

        bool IsLateFun = false;

        int RelaxMin = 0;

        int PenaltyMin = 0;
        int RelaxMinPrev = 0;
        int LateCount = 0;
        string PenaltyMethod = string.Empty;
        DataTable dtAttReg = new DataTable();
        bool IsEmpLate = false;
        try
        {
             IsEmpLate = Convert.ToBoolean(objEmpParam.GetEmployeeParameterByParameterName(empid, "Field1"));
        }
        catch
        {

        }
        
        dtAttReg = objAttReg.GetAttendanceRegDataByMonth_Year_EmpId(empid, date.Month.ToString(), date.Year.ToString());
        dtAttReg = new DataView(dtAttReg, "Late_Relaxation_Min<>'0'", "Att_Date", DataViewRowState.CurrentRows).ToTable();

        try
        {
            IsLateFun = Convert.ToBoolean(objAppParam.GetApplicationParameterValueByParamName("Is_Late_Penalty", Session["CompId"].ToString()));

            RelaxMin = int.Parse(objAppParam.GetApplicationParameterValueByParamName("Late_Relaxation_Min", Session["CompId"].ToString()));
        }
        catch
        {
        }

        if (IsLateFun)
        {

            PenaltyMethod = objAppParam.GetApplicationParameterValueByParamName("Is_Late_Penalty_Method", Session["CompId"].ToString());

            if (PenaltyMethod == "Salary")
            {

               
                LateCount = int.Parse(objAppParam.GetApplicationParameterValueByParamName("Late_Occurence", Session["CompId"].ToString()));


                if (LateMin > 0)
                {

                    if (dtAttReg.Rows.Count > 0)
                    {
                        if (dtAttReg.Rows.Count >= LateCount)
                        {
                            RelaxMin = 0;
                            PenaltyMin = LateMin;
                        }
                        else
                        {

                            RelaxMinPrev = int.Parse(dtAttReg.Rows[dtAttReg.Rows.Count - 1]["Late_Relaxation_Min"].ToString());




                            if (RelaxMinPrev < RelaxMin)
                            {
                                if (LateMin > RelaxMin)
                                {
                                    PenaltyMin = LateMin - RelaxMin;
                                    RelaxMin = 0;
                                }
                                else
                                {
                                    RelaxMin = LateMin + RelaxMinPrev;
                                }
                            }
                            else
                            {

                                int LastLate = int.Parse(dtAttReg.Rows[dtAttReg.Rows.Count - 1]["Late_Relaxation_Min"].ToString());

                                if (LastLate < RelaxMin && LastLate != 0)
                                {

                                    PenaltyMin = RelaxMin - LastLate;
                                    RelaxMin = LastLate + PenaltyMin;
                                    PenaltyMin = LateMin - PenaltyMin;

                                }
                                else
                                {



                                    RelaxMin = 0;
                                    PenaltyMin = LateMin;
                                }


                            }
                        }
                    }
                    else
                    {
                        if (LateMin > RelaxMin)
                        {
                            PenaltyMin = LateMin - RelaxMin;
                            RelaxMin = 0;
                        }
                        else
                        {
                            RelaxMin = LateMin;
                        }
                    }




                }
            }
            else
            {
                int PenaltyDedMin = 0;

                if (LateMin > 0)
                {
                    PenaltyDedMin = int.Parse(objAppParam.GetApplicationParameterValueByParamName("Late_Penalty_Min_Deduct", Session["CompId"].ToString()));

                   
                    int TotLateMin = 0;
                    for (int i = 0; i<dtAttReg.Rows.Count;i++ )
                    {
                        TotLateMin+=int.Parse(dtAttReg.Rows[i]["Late_Relaxation_Min"].ToString());

                    }

                    if (TotLateMin >= RelaxMin)
                    {
                      //  PenaltyMin = PenaltyDedMin * LateMin;
                        PenaltyMin = LateMin;

                        RelaxMin = 0;
                    }
                    else
                    {
                        if ((TotLateMin + LateMin) > RelaxMin)
                        {
                            RelaxMin = RelaxMin - TotLateMin;
                            PenaltyMin = LateMin-RelaxMin;
                            
                        }
                        else
                        {
                            RelaxMin = LateMin;
                        }
                    }
                }

            }
        }
        LateRelaxMinPenaltyMin = RelaxMin.ToString() + "-" + PenaltyMin.ToString();
        return LateRelaxMinPenaltyMin;


    }
    public string GetEarlyRelaxMinPenaltyMin(string empid, DateTime date, int EarlyMin)
    {
        string EarlyRelaxMinPenaltyMin = "0";

        bool IsEarlyFun = false;

        int RelaxMin = 0;

        int PenaltyMin = 0;
        int RelaxMinPrev = 0;
        int EarlyCount = 0;
        string PenaltyMethod = string.Empty;
        DataTable dtAttReg = new DataTable();
        dtAttReg = objAttReg.GetAttendanceRegDataByMonth_Year_EmpId(empid, date.Month.ToString(), date.Year.ToString());
        dtAttReg = new DataView(dtAttReg, "Early_Relaxation_Min<>'0'", "Att_Date", DataViewRowState.CurrentRows).ToTable();

        bool IsEmpEarly = false;
        try
        {
             IsEmpEarly = Convert.ToBoolean(objEmpParam.GetEmployeeParameterByParameterName(empid, "Field2"));
        }
        catch
        {

        }
        try
        {
            IsEarlyFun = Convert.ToBoolean(objAppParam.GetApplicationParameterValueByParamName("Is_Early_Penalty", Session["CompId"].ToString()));
            RelaxMin = int.Parse(objAppParam.GetApplicationParameterValueByParamName("Early_Relaxation_Min", Session["CompId"].ToString()));
        }
        catch
        {

        }
        if (IsEarlyFun)
        {

            PenaltyMethod = objAppParam.GetApplicationParameterValueByParamName("Is_Early_Penalty_Method", Session["CompId"].ToString());

            if (PenaltyMethod == "Salary")
            {

              

                EarlyCount = int.Parse(objAppParam.GetApplicationParameterValueByParamName("Early_Occurence", Session["CompId"].ToString()));


                if (EarlyMin > 0)
                {

                    if (dtAttReg.Rows.Count > 0)
                    {
                        if (dtAttReg.Rows.Count >= EarlyCount)
                        {
                            RelaxMin = 0;
                            PenaltyMin = EarlyMin;
                        }
                        else
                        {

                            RelaxMinPrev = int.Parse(dtAttReg.Rows[dtAttReg.Rows.Count - 1]["Early_Relaxation_Min"].ToString());




                            if (RelaxMinPrev < RelaxMin && RelaxMinPrev != 0)
                            {
                                if (EarlyMin > RelaxMin)
                                {
                                    PenaltyMin = EarlyMin - RelaxMin;
                                    RelaxMin = 0;
                                }
                                else
                                {
                                    RelaxMin = EarlyMin + RelaxMinPrev;
                                }
                            }
                            else
                            {

                                int LastEarly = int.Parse(dtAttReg.Rows[dtAttReg.Rows.Count - 1]["Early_Relaxation_Min"].ToString());

                                if (LastEarly < RelaxMin && LastEarly != 0)
                                {

                                    PenaltyMin = RelaxMin - LastEarly;
                                    RelaxMin = LastEarly + PenaltyMin;
                                    PenaltyMin = EarlyMin - PenaltyMin;

                                }
                                else
                                {



                                    RelaxMin = 0;
                                    PenaltyMin = EarlyMin;
                                }


                            }
                        }
                    }
                    else
                    {
                        if (EarlyMin > RelaxMin)
                        {
                            PenaltyMin = EarlyMin - RelaxMin;
                            RelaxMin = 0;
                        }
                        else
                        {
                            RelaxMin = EarlyMin;
                        }
                    }




                }
            }
            else
            {
                int PenaltyDedMin = 0;

                if (EarlyMin > 0)
                {
                    PenaltyDedMin = int.Parse(objAppParam.GetApplicationParameterValueByParamName("Early_Penalty_Min_Deduct", Session["CompId"].ToString()));

                    int TotEarlyMin = 0;
                    for (int i = 0; i < dtAttReg.Rows.Count; i++)
                    {
                        TotEarlyMin += int.Parse(dtAttReg.Rows[i]["Early_Relaxation_Min"].ToString());

                    }

                    if (TotEarlyMin >= RelaxMin)
                    {
                       // PenaltyMin = PenaltyDedMin * EarlyMin;
                        PenaltyMin = EarlyMin;
                        
                        RelaxMin = 0;
                    }
                    else
                    {


                        

                        if ((TotEarlyMin + EarlyMin) > RelaxMin)
                        {
                            RelaxMin = RelaxMin - TotEarlyMin;
                            PenaltyMin = EarlyMin - RelaxMin;
                            
                        }
                        else
                        {
                            RelaxMin = EarlyMin;
                        }
                    }


                }

            }
        }
        EarlyRelaxMinPenaltyMin = RelaxMin.ToString() + "-" + PenaltyMin.ToString();

       
      
        return EarlyRelaxMinPenaltyMin;


    }

    public string GetAssignWorkMin(string EmpId)
    {
        string AssignMin = string.Empty;


        DataTable dt = objEmpParam.GetEmployeeParameterByEmpId(EmpId, Session["CompId"].ToString());

        if (dt.Rows.Count > 0)
        {
            AssignMin = dt.Rows[0]["Assign_Min"].ToString();

        }
        else
        {
            AssignMin = objAppParam.GetApplicationParameterValueByParamName("Work Day Min", Session["CompId"].ToString());
        }

        return AssignMin;
    }
    public string getWorkMinute(string effec, string assign)
    {
        int TempInt = 0;
        double effecmin = Convert.ToDouble(effec);
        double assignmin = Convert.ToDouble(assign);
        double minut = effecmin;

        if (objAppParam.GetApplicationParameterValueByParamName("Pay Salary Acc To Work Hour or Ref Hour", Session["CompId"].ToString()) == "Ref Hour")
        {
            int WorkPercentTo1 = Convert.ToInt16(objAppParam.GetApplicationParameterValueByParamName("WorkPercentTo1", Session["CompId"].ToString()));
            int WorkPercentTo2 = Convert.ToInt16(objAppParam.GetApplicationParameterValueByParamName("WorkPercentTo2", Session["CompId"].ToString()));
            int WorkPercentTo3 = Convert.ToInt16(objAppParam.GetApplicationParameterValueByParamName("WorkPercentTo3", Session["CompId"].ToString()));
            int WorkPercentFrom1 = Convert.ToInt16(objAppParam.GetApplicationParameterValueByParamName("WorkPercentFrom1", Session["CompId"].ToString()));
            int WorkPercentFrom2 = Convert.ToInt16(objAppParam.GetApplicationParameterValueByParamName("WorkPercentFrom2", Session["CompId"].ToString()));
            int WorkPercentFrom3 = Convert.ToInt16(objAppParam.GetApplicationParameterValueByParamName("WorkPercentFrom3", Session["CompId"].ToString()));
            int Value1 = Convert.ToInt16(objAppParam.GetApplicationParameterValueByParamName("Value1", Session["CompId"].ToString()));
            int Value2 = Convert.ToInt16(objAppParam.GetApplicationParameterValueByParamName("Value2", Session["CompId"].ToString()));
            int Value3 = Convert.ToInt16(objAppParam.GetApplicationParameterValueByParamName("Value3", Session["CompId"].ToString()));



            if (effecmin < assignmin)
            {
                double workper = (effecmin * 100) / assignmin;



                if (workper >= WorkPercentFrom1 && workper <= WorkPercentTo1)
                {
                    minut = (assignmin * Value1) / 100;
                }

                else if (workper >= WorkPercentFrom2 && workper <= WorkPercentTo2)
                {
                    minut = (assignmin * Value2) / 100;
                }
                else if (workper >= WorkPercentFrom3 && workper <= WorkPercentTo3)
                {
                    minut = (assignmin * Value3) / 100;

                }




            }
            TempInt = Convert.ToInt32(minut);
            if (Convert.ToInt32(TempInt) > Convert.ToInt32(assign))
            {
                TempInt = Convert.ToInt32(assign);
            }
            


        }
        else
        {
            if (Convert.ToInt32(effec) > Convert.ToInt32(assign))
            {
                TempInt = Convert.ToInt32(assign);
            }
            else
            {
                TempInt = Convert.ToInt32(effec);
            }
        }

        return (TempInt.ToString());
    }


    public int GetOverTimeMinWithOutShift(string EmpId,int WorkMin)
    {
        int OtMin = 0;
        bool IsCompOT = false;
        bool IsEmpOT = false;
        int MaxOt = 0;
        int MinOt = 0;
        string OverTimeMethod = string.Empty;

        IsCompOT = Convert.ToBoolean(objAppParam.GetApplicationParameterValueByParamName("IsOverTime", Session["CompId"].ToString()));
        MaxOt = int.Parse(objAppParam.GetApplicationParameterValueByParamName("Max Over Time Min", Session["CompId"].ToString()));
        MinOt = int.Parse(objAppParam.GetApplicationParameterValueByParamName("Min OVer Time Min", Session["CompId"].ToString()));

        if (IsCompOT)
        {
            DataTable dt = objEmpParam.GetEmployeeParameterByEmpId(EmpId, Session["CompId"].ToString());

            if (dt.Rows.Count > 0)
            {
                IsEmpOT = Convert.ToBoolean(dt.Rows[0]["Is_OverTime"].ToString());

                if (IsEmpOT)
                {


                    int assignMin = int.Parse(GetAssignWorkMin(EmpId));

                    OtMin = WorkMin;
                    OtMin = OtMin - assignMin;
                    if (OtMin < 0)
                    {
                        OtMin = 0;
                    }



                }

            }

        }
        if (OtMin < MinOt)
        {
            OtMin = 0;

        }
        if (OtMin > MaxOt)
        {
            OtMin = MaxOt;

        }
        return OtMin;

    }

    public int GetOverTimeMin(string EmpId, DateTime InTime, DateTime OutTime, DateTime OnDutyTime, DateTime OffDutyTime, int WorkMin)
    {
        int OtMin = 0;
        bool IsCompOT = false;
        bool IsEmpOT = false;
        int MaxOt = 0;
        int MinOt = 0;
        string OverTimeMethod = string.Empty;

        IsCompOT = Convert.ToBoolean(objAppParam.GetApplicationParameterValueByParamName("IsOverTime", Session["CompId"].ToString()));
        MaxOt = int.Parse(objAppParam.GetApplicationParameterValueByParamName("Max Over Time Min", Session["CompId"].ToString()));
        MinOt = int.Parse(objAppParam.GetApplicationParameterValueByParamName("Min OVer Time Min", Session["CompId"].ToString()));

        if (IsCompOT)
        {
            DataTable dt = objEmpParam.GetEmployeeParameterByEmpId(EmpId, Session["CompId"].ToString());

            if (dt.Rows.Count > 0)
            {
                IsEmpOT = Convert.ToBoolean(dt.Rows[0]["Is_OverTime"].ToString());

                if (IsEmpOT)
                {
                    OverTimeMethod = dt.Rows[0]["Normal_OT_Method"].ToString();
                    if (OverTimeMethod == "In")
                    {
                        if (InTime < OnDutyTime)
                        {
                            OtMin = GetTimeDifference(InTime, OnDutyTime);
                        }

                    }
                    else if (OverTimeMethod == "Out")
                    {
                        if (OutTime > OffDutyTime)
                        {
                            OtMin = GetTimeDifference(OffDutyTime, OutTime);
                        }
                    }
                    else if (OverTimeMethod == "Both")
                    {
                        if (InTime < OnDutyTime)
                        {
                            OtMin = GetTimeDifference(InTime, OnDutyTime);

                        }
                        if (OutTime > OffDutyTime)
                        {
                            OtMin += GetTimeDifference(OffDutyTime, OutTime);
                        }
                    }
                    else if (OverTimeMethod == "Work Hour")
                    {
                        int assignMin = int.Parse(GetAssignWorkMin(EmpId));

                        if (WorkMin > assignMin)
                        {
                            OtMin = WorkMin - assignMin;

                        }
                    }


                }

            }

        }
        if (OtMin < MinOt)
        {
            OtMin = 0;

        }
        if (OtMin > MaxOt)
        {
            OtMin = MaxOt;

        }
        return OtMin;
    }



    public string GetWorkCalculationMethod(string EmpId)
    {
        string WorkCalMethod = string.Empty;


        DataTable dt = objEmpParam.GetEmployeeParameterByEmpId(EmpId, Session["CompId"].ToString());

        if (dt.Rows.Count > 0)
        {
            WorkCalMethod = dt.Rows[0]["Effective_Work_Cal_Method"].ToString();

        }
        else
        {
            WorkCalMethod = objAppParam.GetApplicationParameterValueByParamName("Effective Work Calculation Method", Session["CompId"].ToString());
        }

        return WorkCalMethod;

    }
    private int GetTimeDifference(DateTime inTime, DateTime outTime)
    {
        outTime = Convert.ToDateTime(outTime);
        // On Duty time  = in Time
        // OutTime ==  Actual In Time
        int timeDifference = 0;
        if (outTime >= inTime)
        {
            timeDifference = outTime.Subtract(inTime).Hours * 60 + outTime.Subtract(inTime).Minutes;
        }
        else
        {
            DateTime TempDateIn = new DateTime(inTime.Year, inTime.Month, inTime.Day, 23, 59, 0);
            DateTime TempDateOut = new DateTime(outTime.Year, outTime.Month, outTime.Day, 0, 0, 0);
            timeDifference = TempDateIn.Subtract(inTime).Hours * 60 + TempDateIn.Subtract(inTime).Minutes;
            timeDifference += outTime.Subtract(TempDateOut).Hours * 60 + outTime.Subtract(TempDateOut).Minutes + 1;
        }
        return timeDifference;
    }

    public int GetWorkMinute(string EmpId)
    {
        string WorkMin = string.Empty;


        DataTable dt = objEmpParam.GetEmployeeParameterByEmpId(EmpId, Session["CompId"].ToString());

        if (dt.Rows.Count > 0)
        {
            WorkMin = dt.Rows[0]["Assign_Min"].ToString();

        }
        else
        {
            WorkMin = objAppParam.GetApplicationParameterValueByParamName("Work Day Min", Session["CompId"].ToString());
        }

        return int.Parse(WorkMin);

    }

    public void DisplayMessage(string str)
    {
        ScriptManager.RegisterStartupScript(this, GetType(), "", "alert('" + str + "');", true);
    }
    protected void btnReset_Click(object sender, EventArgs e)
    {
        txtFromDate.Text = "";
        txtToDate.Text = "";
        ddlMonth.SelectedIndex = 0;
        txtYear.Text = "";
        lblSelectRecord.Text = "";
        DataTable dtEmp = objEmp.GetEmployeeMaster(Session["CompId"].ToString());

        dtEmp = new DataView(dtEmp, "Brand_Id='" + Session["BrandId"].ToString() + "'  and Location_Id='" + Session["LocId"].ToString() + "'", "", DataViewRowState.CurrentRows).ToTable();

        if (Session["SessionDepId"] != null)
        {
            dtEmp = new DataView(dtEmp, "Department_Id in(" + Session["SessionDepId"].ToString().Substring(0, Session["SessionDepId"].ToString().Length - 1) + ")", "", DataViewRowState.CurrentRows).ToTable();

        }
        if (dtEmp.Rows.Count > 0)
        {
            Session["dtEmp"] = dtEmp;
            gvEmployee.DataSource = dtEmp;
            gvEmployee.DataBind();
            lblTotalRecord.Text = Resources.Attendance.Total_Records + " : " + dtEmp.Rows.Count.ToString() + "";

        }

        rbtnEmpSal.Checked = true;
        rbtnGroupSal.Checked = false;
        EmpGroupSal_CheckedChanged(null, null);
        btnaryRefresh_Click1(null, null);
    }


    protected void btnarybind_Click1(object sender, ImageClickEventArgs e)
    {
        if (ddlOption.SelectedIndex != 0)
        {
            string condition = string.Empty;
            if (ddlOption.SelectedIndex == 1)
            {
                condition = "convert(" + ddlField.SelectedValue + ",System.String)='" + txtValue.Text.Trim() + "'";
            }
            else if (ddlOption.SelectedIndex == 2)
            {
                condition = "convert(" + ddlField.SelectedValue + ",System.String) like '%" + txtValue.Text.Trim() + "%'";
            }
            else
            {
                condition = "convert(" + ddlField.SelectedValue + ",System.String) Like '" + txtValue.Text.Trim() + "%'";

            }
            DataTable dtEmp = (DataTable)Session["dtEmp"];
            DataView view = new DataView(dtEmp, condition, "", DataViewRowState.CurrentRows);
            gvEmployee.DataSource = view.ToTable();
            gvEmployee.DataBind();
            lblTotalRecord.Text = Resources.Attendance.Total_Records + " : " + (view.ToTable()).Rows.Count.ToString() + "";

        }

    }


    protected void btnaryRefresh_Click1(object sender, ImageClickEventArgs e)
    {
        lblSelectRecord.Text = "";
        ddlField.SelectedIndex = 1;
        ddlOption.SelectedIndex = 2;
        txtValue.Text = "";

        DataTable dtEmp = (DataTable)Session["dtEmp"];
        gvEmployee.DataSource = dtEmp;
        gvEmployee.DataBind();

    }

    protected void chkgvSelectAll_CheckedChanged(object sender, EventArgs e)
    {
        CheckBox chkSelAll = ((CheckBox)gvEmployee.HeaderRow.FindControl("chkgvSelectAll"));
        for (int i = 0; i < gvEmployee.Rows.Count; i++)
        {
            ((CheckBox)gvEmployee.Rows[i].FindControl("chkgvSelect")).Checked = chkSelAll.Checked;
            if (chkSelAll.Checked)
            {
                if (!lblSelectRecord.Text.Split(',').Contains(((Label)(gvEmployee.Rows[i].FindControl("lblEmpId"))).Text.Trim().ToString()))
                {
                    lblSelectRecord.Text += ((Label)(gvEmployee.Rows[i].FindControl("lblEmpId"))).Text.Trim().ToString() + ",";
                }
            }
            else
            {
                string temp = string.Empty;
                string[] split = lblSelectRecord.Text.Split(',');
                foreach (string item in split)
                {
                    if (item != ((Label)(gvEmployee.Rows[i].FindControl("lblEmpId"))).Text.Trim().ToString())
                    {
                        if (item != "")
                        {
                            temp += item + ",";
                        }
                    }
                }
                lblSelectRecord.Text = temp;
            }
        }


    }

}
