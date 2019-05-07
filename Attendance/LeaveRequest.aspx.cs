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


public partial class Attendance_LeaveRequest : System.Web.UI.Page
{
    EmployeeMaster objEmp = new EmployeeMaster();
    UserMaster objUser = new UserMaster();
    Att_Leave_Request objleaveReq = new Att_Leave_Request();

    Att_Employee_Leave objEmpleave = new Att_Employee_Leave();

    LeaveMaster objleave = new LeaveMaster();
    HolidayMaster objHoliday = new HolidayMaster();
    SystemParameter objSys = new SystemParameter();
    Set_Employee_Holiday objEmpHoliday = new Set_Employee_Holiday();
    Set_ApplicationParameter objAppParam = new Set_ApplicationParameter();
    protected void Page_Load(object sender, EventArgs e)
    {
        if (Session["UserId"] == null)
        {
            Response.Redirect("~/ERPLogin.aspx");
        }
        Session["AccordianId"] = "9";
        Session["HeaderText"] = "Attendance Setup";
        if(!IsPostBack)
        {
            ddlLeaveType.Visible = false;
        FillLeaveSummary(Session["UserId"].ToString());

        }
        objSys.GetSysTitle();
        CalendarExtender1.Format = objSys.SetDateFormat();
        CalendarExtender2.Format = objSys.SetDateFormat();

    }





    public string GetDate(object obj)
    {

        DateTime Date = new DateTime();
        Date = Convert.ToDateTime(obj.ToString());



        return Date.ToString(objSys.SetDateFormat());

    }

    public string GetScheduleType(object Date,object EmpId,object leavetypeid)
    {
        string ScheduleType = string.Empty;
        string Date1 = Date.ToString();
        string year=Convert.ToDateTime(Date1).Year.ToString();
        string empid = EmpId.ToString();
        string leaveId=leavetypeid.ToString();
        DataTable dt = objAppParam.GetApplicationParameterByParamName("FinancialYearStartMonth", Session["CompId"].ToString());
        int FinancialYearMonth = 0;

        if (dt.Rows.Count > 0)
        {
            FinancialYearMonth = int.Parse(dt.Rows[0]["Param_Value"].ToString());

        }
        DateTime FinancialYearStartDate = new DateTime();
        DateTime FinancialYearEndDate = new DateTime();
        if (DateTime.Now.Month < FinancialYearMonth)
        {

            FinancialYearStartDate = new DateTime(DateTime.Now.Year - 1, FinancialYearMonth, 1);

            FinancialYearEndDate = FinancialYearStartDate.AddYears(1).AddDays(-1);
        }
        else
        {
            FinancialYearStartDate = new DateTime(DateTime.Now.Year, FinancialYearMonth, 1);
            FinancialYearEndDate = FinancialYearStartDate.AddYears(1).AddDays(-1);

        }
      
             year = string.Empty;

        if (FinancialYearStartDate.Year == FinancialYearEndDate.Year)
        {
            year = FinancialYearStartDate.Year.ToString();
            

        }
        else
        {


            year = FinancialYearStartDate.Year.ToString();
           
           
        }
        DataTable dtLeave = objEmpleave.GetEmployeeLeaveTransactionDataByEmpId(empid);

        dtLeave = new DataView(dtLeave,"Year='"+year+"' and Leave_Type_Id='"+leaveId+"'","",DataViewRowState.CurrentRows).ToTable();

        if(dtLeave.Rows.Count > 0)
        {
            if(dtLeave.Rows[0]["Month"].ToString()=="0")
            {
                ScheduleType = "Yearly";
            }
            else
            {
                ScheduleType = "Monthly";
            }

        }

        return ScheduleType;

    }

    public void FillLeaveStatus(string EmpId)
    {
        DataTable dtLeave = objleaveReq.GetLeaveRequestById(Session["CompId"].ToString(),EmpId);

        dtLeave = new DataView(dtLeave,"Is_Pending='True'","",DataViewRowState.CurrentRows).ToTable();

        if(dtLeave.Rows.Count > 0)
        {
            gvLeaveStatus.DataSource = dtLeave;
            gvLeaveStatus.DataBind();
            
        }
    }

    public void FillLeaveSummary(string UserId)
    
{
        DataTable dtUser = objUser.GetUserMasterByUserId(UserId);

        string empid = string.Empty;
        
        if(dtUser.Rows.Count > 0)
        {
            empid=dtUser.Rows[0]["Emp_Id"].ToString();
            hdnEmpId.Value = empid;
        }

        if(empid!="")
        {
            DataTable dtLeaveSummary = objEmpleave.GetEmployeeLeaveTransactionDataByEmpId(empid);

            string months = string.Empty;
           
            DateTime FromDate=DateTime.Now;
            DateTime ToDate = DateTime.Now.AddMonths(2);
            while (FromDate <= ToDate)
            {
                months += FromDate.Month.ToString() + ",";
                FromDate = FromDate.AddMonths(1);
               
            }
            DataTable dt = objAppParam.GetApplicationParameterByParamName("FinancialYearStartMonth", Session["CompId"].ToString());
            int FinancialYearMonth = 0;
        
            if (dt.Rows.Count > 0)
            {
                FinancialYearMonth = int.Parse(dt.Rows[0]["Param_Value"].ToString());

            }
            DateTime FinancialYearStartDate = new DateTime();
            DateTime FinancialYearEndDate = new DateTime();
            if (DateTime.Now.Month < FinancialYearMonth)
            {
               
                FinancialYearStartDate = new DateTime(DateTime.Now.Year-1, FinancialYearMonth, 1);

                FinancialYearEndDate = FinancialYearStartDate.AddYears(1).AddDays(-1);
            }
            else
            {
                FinancialYearStartDate = new DateTime(DateTime.Now.Year, FinancialYearMonth, 1);
                FinancialYearEndDate = FinancialYearStartDate.AddYears(1).AddDays(-1);

            }
            string year = string.Empty;
            string year1 = string.Empty;
           
            if (FinancialYearStartDate.Year == FinancialYearEndDate.Year)
            {
                year = FinancialYearStartDate.Year.ToString();
                year1 = year;

            }
            else
            {
              

                year += FinancialYearStartDate.Year.ToString() + ",";
                year1 = year;
                year += FinancialYearEndDate.Year.ToString() + ",";
            }



            DataTable dtleave = dtLeaveSummary;
            dtleave = new DataView(dtleave, "month='0' and year in("+year1+")", "", DataViewRowState.CurrentRows).ToTable();

            dtLeaveSummary = new DataView(dtLeaveSummary,"month in("+months+") and year in ("+year+") ","",DataViewRowState.CurrentRows).ToTable();

            dtLeaveSummary.Merge(dtleave);
            if (dtLeaveSummary.Rows.Count > 0)
            {
                gvLeaveSummary.DataSource = dtLeaveSummary;
                gvLeaveSummary.DataBind();

            }
            else
            {
                DataTable dtLeave1 = objEmpleave.GetEmployeeLeaveByEmpId(Session["CompId"].ToString(),empid);

                
                if(dtLeave1.Rows.Count > 0)
                {

                    DataTable dtReq = objleaveReq.GetLeaveRequestById(Session["CompId"].ToString(),empid);

                    dtReq = new DataView(dtReq,"Is_Pending='True'","",DataViewRowState.CurrentRows).ToTable();

                    if(dtReq.Rows.Count > 0)
                    {
                        for (int i = 0; i < dtReq.Rows.Count;i++ )
                        {
                            int b = 0;
                            b=objleaveReq.UpdateLeaveRequestByTransId(dtReq.Rows[i]["Trans_Id"].ToString(),Session["CompId"].ToString(),false.ToString(),false.ToString(),true.ToString(),"",true.ToString(), Session["UserId"].ToString(), DateTime.Now.ToString());
                            //

                            if (b != 0)
                            {
                                string Schedule = string.Empty;
                                DataTable dtLeave = new DataTable();
                                objleaveReq.DeleteLeaveRequestChildByRefId(dtReq.Rows[i]["Trans_Id"].ToString());

                                dtLeave = objEmpleave.GetEmployeeLeaveTransactionData(empid, dtReq.Rows[i]["Leave_Type_Id"].ToString(), Convert.ToDateTime(dtReq.Rows[i]["From_Date"].ToString()).Month.ToString(),Convert.ToDateTime(dtReq.Rows[i]["From_Date"].ToString()).Year.ToString());

                                if (dtLeave.Rows.Count > 0)
                                {


                                    Schedule = "Monthly";
                                }
                                else
                                {
                                    dtLeave = objEmpleave.GetEmployeeLeaveTransactionData(empid, dtReq.Rows[i]["Leave_Type_Id"].ToString(), "0", Convert.ToDateTime(dtReq.Rows[i]["From_Date"].ToString()).Year.ToString());
                                    Schedule = "Yearly";
                                }

                                int remain = 0;
                                int useddays = 0;
                                int Totaldays = 0;

                                if (dtLeave.Rows.Count > 0)
                                {

                                    if (dtLeave.Rows.Count > 0)
                                    {


                                        remain = int.Parse(dtLeave.Rows[0]["Remaining_Days"].ToString());
                                        useddays = int.Parse(dtLeave.Rows[0]["Used_Days"].ToString());
                                        Totaldays = int.Parse(dtLeave.Rows[0]["Total_Days"].ToString());
                                    }

                                }

                                int DaysCount = 0;

                                DateTime dtFrom =Convert.ToDateTime(dtReq.Rows[i]["From_Date"].ToString());
                                DateTime dtTo = Convert.ToDateTime(dtReq.Rows[i]["To_Date"].ToString());

                                while(dtFrom<=dtTo)
                                {

                                    DaysCount +=1;
                                    dtFrom = dtFrom.AddDays(1);

                                }


                                useddays = useddays - (DaysCount);

                                remain = remain + (DaysCount);

                                if (Schedule == "Yearly")
                                {

                                    objEmpleave.UpdateEmployeeLeaveTransaction(Session["CompId"].ToString(), empid, dtReq.Rows[i]["Leave_Type_Id"].ToString(), dtFrom.Year.ToString(), "0", "0", Totaldays.ToString(), Totaldays.ToString(), useddays.ToString(), remain.ToString(), "0", Session["UserId"].ToString(), DateTime.Now.ToString());
                                }
                                else
                                {
                                    objEmpleave.UpdateEmployeeLeaveTransaction(Session["CompId"].ToString(), empid, dtReq.Rows[i]["Leave_Type_Id"].ToString(), dtFrom.Year.ToString(), dtFrom.Month.ToString(), "0", Totaldays.ToString(), Totaldays.ToString(), useddays.ToString(), remain.ToString(), "0", Session["UserId"].ToString(), DateTime.Now.ToString());


                                }
                               // DisplayMessage("Leave Rejected");
                                
                            }




                            //
                        }
                    }

                    for (int i = 0; i < dtLeave1.Rows.Count;i++)
                    {
                        if(dtLeave1.Rows[i]["Shedule_Type"].ToString()=="Monthly")
                        {

                            while(FinancialYearStartDate <= FinancialYearEndDate)
                            {

                                objEmpleave.InsertEmployeeLeaveTrans(Session["CompId"].ToString(), empid, dtLeave1.Rows[i]["LeaveType_Id"].ToString(),FinancialYearStartDate.Year.ToString(),FinancialYearStartDate.Month.ToString(),"0",dtLeave1.Rows[i]["Total_Leave"].ToString(),dtLeave1.Rows[i]["Total_Leave"].ToString(),"0",dtLeave1.Rows[i]["Total_Leave"].ToString(),"0","","","","","",true.ToString(),DateTime.Now.ToString(),true.ToString(),Session["UserId"].ToString(),DateTime.Now.ToString(),Session["UserId"].ToString(),DateTime.Now.ToString());

                                FinancialYearStartDate = FinancialYearStartDate.AddMonths(1);

                            }
                            
                           
                        }
                        else
                        {
                            if (dtLeave1.Rows[i]["Is_YearCarry"].ToString() == "True")
                            {
                                DateTime PrevFinancialDate = FinancialYearStartDate.AddYears(-1);

                                DataTable dtL = objEmpleave.GetEmployeeLeaveTransactionDataByEmpId(empid);

                                dtL = new DataView(dtL, "Month='0' and year='" + PrevFinancialDate.Year.ToString() + "'", "", DataViewRowState.CurrentRows).ToTable();

                                int RemainingDays = 0;
                                RemainingDays = int.Parse(dtL.Rows[0]["Remaining_Days"].ToString());

                                int Totaldays = int.Parse(dtLeave1.Rows[i]["Total_Leave"].ToString()) + RemainingDays;


                                objEmpleave.InsertEmployeeLeaveTrans(Session["CompId"].ToString(), empid, dtLeave1.Rows[i]["LeaveType_Id"].ToString(), FinancialYearStartDate.Year.ToString(), "0",RemainingDays.ToString(),Totaldays.ToString(), Totaldays.ToString(),"0",Totaldays.ToString(),"0", "", "", "", "", "", true.ToString(), DateTime.Now.ToString(), true.ToString(), Session["UserId"].ToString(), DateTime.Now.ToString(), Session["UserId"].ToString(), DateTime.Now.ToString());

                               
                            }
                            else
                            {
                                objEmpleave.InsertEmployeeLeaveTrans(Session["CompId"].ToString(), empid, dtLeave1.Rows[i]["LeaveType_Id"].ToString(), FinancialYearStartDate.Year.ToString(), "0", "0", dtLeave1.Rows[i]["Total_Leave"].ToString(), dtLeave1.Rows[i]["Total_Leave"].ToString(), "0", dtLeave1.Rows[i]["Total_Leave"].ToString(), "0", "", "", "", "", "", true.ToString(), DateTime.Now.ToString(), true.ToString(), Session["UserId"].ToString(), DateTime.Now.ToString(), Session["UserId"].ToString(), DateTime.Now.ToString());
                                
                            }

                        }
                    }
                    FillLeaveSummary(Session["UserId"].ToString());
                }

            }
            FillLeaveStatus(empid);
            FillLeaveApproveRejected(empid);
        }

    }

    public void SaveLeave(string Edit, string LeaveTypeId, string EmpId, string SchType, string AssignLeave, string IsYearCarry, string PrevSchduleType, string PrevAssignLeave, string TransNo)
    {
        DateTime JoiningDate = new DateTime();
        DataTable dtEmp = objEmp.GetEmployeeMasterById(Session["CompId"].ToString(), EmpId);
        if (dtEmp.Rows.Count > 0)
        {
            JoiningDate = Convert.ToDateTime(dtEmp.Rows[0]["DOJ"].ToString());
        }
        else
        {
            return;
        }


        if (JoiningDate > DateTime.Now)
        {
            return;

        }


        DataTable dt = objAppParam.GetApplicationParameterByParamName("FinancialYearStartMonth", Session["CompId"].ToString());
        int FinancialYearMonth = 0;
        int leavepermonth = 0;
        int leavepermonthPrev = 0;
        if (dt.Rows.Count > 0)
        {
            FinancialYearMonth = int.Parse(dt.Rows[0]["Param_Value"].ToString());

        }
        double TotalDays = 0;
        DateTime FinancialYearStartDate = new DateTime();
        DateTime FinancialYearEndDate = new DateTime();
        if (JoiningDate.Month > FinancialYearMonth)
        {
            FinancialYearStartDate = new DateTime(DateTime.Now.Year, FinancialYearMonth, 1);
            FinancialYearEndDate = FinancialYearStartDate.AddYears(1).AddDays(-1);

            TotalDays = FinancialYearEndDate.Subtract(FinancialYearStartDate).Days;


            double Months1 = TotalDays / 30;


            leavepermonth = int.Parse(System.Math.Round(double.Parse(AssignLeave) / Months1).ToString());
            if (Edit == "Yes")
            {
                leavepermonthPrev = int.Parse(System.Math.Round(double.Parse(PrevAssignLeave) / Months1).ToString());


            }
        }
        else
        {

            FinancialYearStartDate = new DateTime(DateTime.Now.Year, FinancialYearMonth, 1);
            FinancialYearEndDate = FinancialYearStartDate.AddYears(1).AddDays(-1);
            TotalDays = FinancialYearEndDate.Subtract(FinancialYearStartDate).Days;



            double Months1 = TotalDays / 30;


            leavepermonth = int.Parse(System.Math.Round(double.Parse(AssignLeave) / Months1).ToString());
            if (Edit == "Yes")
            {
                leavepermonthPrev = int.Parse(System.Math.Round(double.Parse(PrevAssignLeave) / Months1).ToString());


            }
        }


        if (Edit == "No")
        {

            if (SchType == "Monthly")
            {
                if (JoiningDate.Year < FinancialYearStartDate.Year)
                {
                    JoiningDate = new DateTime(DateTime.Now.Year, DateTime.Now.Month, DateTime.Now.Day);

                    while (JoiningDate <= FinancialYearEndDate)
                    {
                        int leave = 0;
                        if (JoiningDate.Day != 1)
                        {

                            double JoinDay = 30 - JoiningDate.Day;

                            double Day = (Convert.ToDouble(JoinDay) * Convert.ToDouble(AssignLeave)) / 30;



                            leave = int.Parse(System.Math.Round(Day).ToString());
                        }
                        else
                        {
                            leave = int.Parse(Convert.ToDouble(AssignLeave).ToString());

                        }


                        objEmpleave.InsertEmployeeLeaveTrans(Session["CompId"].ToString(), EmpId, LeaveTypeId, JoiningDate.Year.ToString(), JoiningDate.Month.ToString(), "0", leave.ToString(), leave.ToString(), "0", leave.ToString(), "0", "", "", "", "", "", true.ToString(), DateTime.Now.ToString(), true.ToString(), Session["UserId"].ToString(), DateTime.Now.ToString(), Session["UserId"].ToString(), DateTime.Now.ToString());
                        JoiningDate = JoiningDate.AddMonths(1);
                        JoiningDate = new DateTime(JoiningDate.Year, JoiningDate.Month, 1);

                    }




                }
                else if (JoiningDate.Year == FinancialYearStartDate.Year)
                {

                    if (JoiningDate.Month == DateTime.Now.Month)
                    {

                    }

                    else
                    {

                        JoiningDate = new DateTime(DateTime.Now.Year, DateTime.Now.Month, DateTime.Now.Day);
                    }


                    while (JoiningDate <= FinancialYearEndDate)
                    {
                        int leave = 0;
                        if (JoiningDate.Day != 1)
                        {

                            double JoinDay = 30 - JoiningDate.Day;

                            double Day = (Convert.ToDouble(JoinDay) * Convert.ToDouble(AssignLeave)) / 30;



                            leave = int.Parse(System.Math.Round(Day).ToString());
                        }
                        else
                        {
                            leave = int.Parse(Convert.ToDouble(AssignLeave).ToString());

                        }


                        objEmpleave.InsertEmployeeLeaveTrans(Session["CompId"].ToString(), EmpId, LeaveTypeId, JoiningDate.Year.ToString(), JoiningDate.Month.ToString(), "0", leave.ToString(), leave.ToString(), "0", leave.ToString(), "0", "", "", "", "", "", true.ToString(), DateTime.Now.ToString(), true.ToString(), Session["UserId"].ToString(), DateTime.Now.ToString(), Session["UserId"].ToString(), DateTime.Now.ToString());
                        JoiningDate = JoiningDate.AddMonths(1);
                        JoiningDate = new DateTime(JoiningDate.Year, JoiningDate.Month, 1);

                    }
                }
                else if (JoiningDate > FinancialYearEndDate)
                {


                }


            }
            else if (SchType == "Yearly")
            {
                if (JoiningDate.Year < FinancialYearStartDate.Year)
                {
                    JoiningDate = new DateTime(DateTime.Now.Year, DateTime.Now.Month, DateTime.Now.Day);


                    double leave = 0;

                    if (JoiningDate.Day != 1)
                    {

                        double JoinDay = 30 - JoiningDate.Day;

                        double Day = (Convert.ToDouble(JoinDay) * Convert.ToDouble(leavepermonth)) / 30;



                        leave = double.Parse(System.Math.Round(Day).ToString());
                    }
                    else
                    {
                        leave = double.Parse(Convert.ToDouble(leavepermonth).ToString());

                    }


                    int monthJ = 0;
                    int yearJ = 0;



                    if (JoiningDate.Month == 12)
                    {
                        monthJ = 1;
                        yearJ = JoiningDate.Year + 1;
                    }
                    else
                    {
                        monthJ = JoiningDate.Month + 1;
                        yearJ = JoiningDate.Year;
                    }

                    JoiningDate = new DateTime(yearJ, monthJ, 1);


                    double RemainDays = FinancialYearEndDate.Subtract(JoiningDate).Days;

                    double remainmonth = RemainDays / 30;

                    remainmonth = System.Math.Round(remainmonth);

                    int totalleaves = 0;

                    double Totalmonths = remainmonth * leavepermonth;

                    totalleaves = int.Parse(System.Math.Round(Totalmonths).ToString()) + int.Parse(System.Math.Round(leave).ToString());




                    objEmpleave.InsertEmployeeLeaveTrans(Session["CompId"].ToString(), EmpId, LeaveTypeId, JoiningDate.Year.ToString(), "0", "0", totalleaves.ToString(), totalleaves.ToString(), "0", totalleaves.ToString(), "0", "", "", "", "", "", true.ToString(), DateTime.Now.ToString(), true.ToString(), Session["UserId"].ToString(), DateTime.Now.ToString(), Session["UserId"].ToString(), DateTime.Now.ToString());




                }
                else if (JoiningDate.Year == FinancialYearStartDate.Year)
                {
                    double leave = 0;

                    if (JoiningDate.Month == DateTime.Now.Month)
                    {


                    }

                    else
                    {
                        JoiningDate = new DateTime(DateTime.Now.Year, DateTime.Now.Month, DateTime.Now.Day);
                    }
                    if (JoiningDate.Day != 1)
                    {

                        double JoinDay = 30 - JoiningDate.Day;

                        double Day = (Convert.ToDouble(JoinDay) * Convert.ToDouble(leavepermonth)) / 30;



                        leave = double.Parse(System.Math.Round(Day).ToString());
                    }
                    else
                    {
                        leave = double.Parse(Convert.ToDouble(leavepermonth).ToString());

                    }


                    int monthJ = 0;
                    int yearJ = 0;



                    if (JoiningDate.Month == 12)
                    {
                        monthJ = 1;
                        yearJ = JoiningDate.Year + 1;
                    }
                    else
                    {
                        monthJ = JoiningDate.Month + 1;
                        yearJ = JoiningDate.Year;
                    }

                    JoiningDate = new DateTime(yearJ, monthJ, 1);


                    double RemainDays = FinancialYearEndDate.Subtract(JoiningDate).Days;

                    double remainmonth = RemainDays / 30;

                    remainmonth = System.Math.Round(remainmonth);

                    int totalleaves = 0;

                    double Totalmonths = remainmonth * leavepermonth;

                    totalleaves = int.Parse(System.Math.Round(Totalmonths).ToString()) + int.Parse(System.Math.Round(leave).ToString());




                    objEmpleave.InsertEmployeeLeaveTrans(Session["CompId"].ToString(), EmpId, LeaveTypeId, JoiningDate.Year.ToString(), "0", "0", totalleaves.ToString(), totalleaves.ToString(), "0", totalleaves.ToString(), "0", "", "", "", "", "", true.ToString(), DateTime.Now.ToString(), true.ToString(), Session["UserId"].ToString(), DateTime.Now.ToString(), Session["UserId"].ToString(), DateTime.Now.ToString());




                }
                else if (JoiningDate > FinancialYearEndDate)
                {


                }
            }

        }
        else
        {
            if (JoiningDate.Year < FinancialYearStartDate.Year)
            {

                if (SchType == "Yearly")
                {

                    if (PrevSchduleType == "Yearly")
                    {


                        JoiningDate = new DateTime(DateTime.Now.Year, DateTime.Now.Month, DateTime.Now.Day);


                        double leave = 0;

                        if (JoiningDate.Day != 1)
                        {

                            double JoinDay = 30 - JoiningDate.Day;

                            double Day = (Convert.ToDouble(JoinDay) * Convert.ToDouble(leavepermonth)) / 30;



                            leave = double.Parse(System.Math.Round(Day).ToString());
                        }
                        else
                        {
                            leave = double.Parse(Convert.ToDouble(leavepermonth).ToString());

                        }


                        int monthJ = 0;
                        int yearJ = 0;



                        if (JoiningDate.Month == 12)
                        {
                            monthJ = 1;
                            yearJ = JoiningDate.Year + 1;
                        }
                        else
                        {
                            monthJ = JoiningDate.Month + 1;
                            yearJ = JoiningDate.Year;
                        }

                        JoiningDate = new DateTime(yearJ, monthJ, 1);


                        double RemainDays = FinancialYearEndDate.Subtract(JoiningDate).Days;

                        double remainmonth = RemainDays / 30;

                        remainmonth = System.Math.Round(remainmonth);

                        int totalleaves = 0;

                        double Totalmonths = remainmonth * leavepermonth;

                        totalleaves = int.Parse(System.Math.Round(Totalmonths).ToString()) + int.Parse(System.Math.Round(leave).ToString());

                        objEmpleave.DeleteEmployeeLeaveTransByEmpIdandleaveTypeId(EmpId, LeaveTypeId, "0", JoiningDate.Year.ToString());



                        // objEmpleave.UpdateEmployeeLeaveTransaction(Session["CompId"].ToString(), EmpId, LeaveTypeId, JoiningDate.Year.ToString(), "0", "0", totalleaves.ToString(), totalleaves.ToString(), "0", totalleaves.ToString(),"0", Session["UserId"].ToString(), DateTime.Now.ToString());
                        objEmpleave.InsertEmployeeLeaveTrans(Session["CompId"].ToString(), EmpId, LeaveTypeId, JoiningDate.Year.ToString(), "0", "0", totalleaves.ToString(), totalleaves.ToString(), "0", totalleaves.ToString(), "0", "", "", "", "", "", true.ToString(), DateTime.Now.ToString(), true.ToString(), Session["UserId"].ToString(), DateTime.Now.ToString(), Session["UserId"].ToString(), DateTime.Now.ToString());



                    }
                    else if (PrevSchduleType == "Monthly")
                    {
                        //Monthly to yearly

                        JoiningDate = new DateTime(DateTime.Now.Year, DateTime.Now.Month, DateTime.Now.Day);
                        DateTime JoiningDate1 = JoiningDate;
                        int monthc = 0;
                        int yearc = 0;

                        if (DateTime.Now.Month == 12)
                        {
                            monthc = 1;
                            yearc = DateTime.Now.Year + 1;

                        }
                        else
                        {
                            monthc = DateTime.Now.Month + 1;

                            yearc = DateTime.Now.Year;
                        }
                        JoiningDate = new DateTime(yearc, monthc, 1);
                        int TotalLeaves = 0;
                        while (JoiningDate <= FinancialYearEndDate)
                        {
                            objEmpleave.DeleteEmployeeLeaveTransByEmpIdandleaveTypeId(EmpId, LeaveTypeId, JoiningDate.Month.ToString(), JoiningDate.Year.ToString());

                            TotalLeaves += leavepermonth;
                            JoiningDate = JoiningDate.AddMonths(1);
                        }

                        objEmpleave.InsertEmployeeLeaveTrans(Session["CompId"].ToString(), EmpId, LeaveTypeId, JoiningDate1.Year.ToString(), "0", "0", TotalLeaves.ToString(), TotalLeaves.ToString(), "0", TotalLeaves.ToString(), "0", "", "", "", "", "", true.ToString(), DateTime.Now.ToString(), true.ToString(), Session["UserId"].ToString(), DateTime.Now.ToString(), Session["UserId"].ToString(), DateTime.Now.ToString());



                    }
                }
                else if (SchType == "Monthly")
                {
                    if (PrevSchduleType == "Monthly")
                    {


                        JoiningDate = new DateTime(DateTime.Now.Year, DateTime.Now.Month, DateTime.Now.Day);







                        while (JoiningDate <= FinancialYearEndDate)
                        {
                            objEmpleave.DeleteEmployeeLeaveTransByEmpIdandleaveTypeId(EmpId, LeaveTypeId, JoiningDate.Month.ToString(), JoiningDate.Year.ToString());



                            int leave = 0;
                            if (JoiningDate.Day != 1)
                            {

                                double JoinDay = 30 - JoiningDate.Day;

                                double Day = (Convert.ToDouble(JoinDay) * Convert.ToDouble(AssignLeave)) / 30;



                                leave = int.Parse(System.Math.Round(Day).ToString());
                            }
                            else
                            {
                                leave = int.Parse(Convert.ToDouble(AssignLeave).ToString());

                            }


                            objEmpleave.InsertEmployeeLeaveTrans(Session["CompId"].ToString(), EmpId, LeaveTypeId, JoiningDate.Year.ToString(), JoiningDate.Month.ToString(), "0", leave.ToString(), leave.ToString(), "0", leave.ToString(), "0", "", "", "", "", "", true.ToString(), DateTime.Now.ToString(), true.ToString(), Session["UserId"].ToString(), DateTime.Now.ToString(), Session["UserId"].ToString(), DateTime.Now.ToString());
                            JoiningDate = JoiningDate.AddMonths(1);
                            JoiningDate = new DateTime(JoiningDate.Year, JoiningDate.Month, 1);

                        }





                    }

                    else if (PrevSchduleType == "Yearly")
                    {
                        // yearly to monthly

                        JoiningDate = new DateTime(DateTime.Now.Year, DateTime.Now.Month, DateTime.Now.Day);


                        double leave = 0;

                        if (JoiningDate.Day != 1)
                        {

                            double JoinDay = 30 - JoiningDate.Day;



                            double Day = (Convert.ToDouble(JoinDay) * Convert.ToDouble(leavepermonthPrev)) / 30;



                            leave = double.Parse(System.Math.Round(Day).ToString());
                        }
                        else
                        {
                            leave = double.Parse(Convert.ToDouble(leavepermonthPrev).ToString());

                        }



                        //here 12 aug 

                        objEmpleave.DeleteEmployeeLeaveTransByEmpIdandleaveTypeId(EmpId, LeaveTypeId, "0", JoiningDate.Year.ToString());


                        objEmpleave.InsertEmployeeLeaveTrans(Session["CompId"].ToString(), EmpId, LeaveTypeId, JoiningDate.Year.ToString(), "0", "0", leave.ToString(), leave.ToString(), "0", leave.ToString(), "0", "", "", "", "", "", true.ToString(), DateTime.Now.ToString(), true.ToString(), Session["UserId"].ToString(), DateTime.Now.ToString(), Session["UserId"].ToString(), DateTime.Now.ToString());
                        int monthJ = 0;
                        int yearJ = 0;



                        if (JoiningDate.Month == 12)
                        {
                            monthJ = 1;
                            yearJ = JoiningDate.Year + 1;
                        }
                        else
                        {
                            monthJ = JoiningDate.Month + 1;
                            yearJ = JoiningDate.Year;
                        }

                        JoiningDate = new DateTime(yearJ, monthJ, 1);




                        while (JoiningDate <= FinancialYearEndDate)
                        {

                            leave = double.Parse(Convert.ToDouble(AssignLeave).ToString());

                            objEmpleave.InsertEmployeeLeaveTrans(Session["CompId"].ToString(), EmpId, LeaveTypeId, JoiningDate.Year.ToString(), JoiningDate.Month.ToString(), "0", leave.ToString(), leave.ToString(), "0", leave.ToString(), "0", "", "", "", "", "", true.ToString(), DateTime.Now.ToString(), true.ToString(), Session["UserId"].ToString(), DateTime.Now.ToString(), Session["UserId"].ToString(), DateTime.Now.ToString());
                            JoiningDate = JoiningDate.AddMonths(1);


                        }



                    }

                }


            }
            else if (JoiningDate.Year == FinancialYearStartDate.Year)
            {
                if (SchType == "Yearly")
                {

                    if (PrevSchduleType == "Yearly")
                    {

                        if (JoiningDate.Month == DateTime.Now.Month)
                        {

                        }


                        else
                        {

                            JoiningDate = new DateTime(DateTime.Now.Year, DateTime.Now.Month, DateTime.Now.Day);
                        }

                        double leave = 0;

                        if (JoiningDate.Day != 1)
                        {

                            double JoinDay = 30 - JoiningDate.Day;

                            double Day = (Convert.ToDouble(JoinDay) * Convert.ToDouble(leavepermonth)) / 30;



                            leave = double.Parse(System.Math.Round(Day).ToString());
                        }
                        else
                        {
                            leave = double.Parse(Convert.ToDouble(leavepermonth).ToString());

                        }


                        int monthJ = 0;
                        int yearJ = 0;



                        if (JoiningDate.Month == 12)
                        {
                            monthJ = 1;
                            yearJ = JoiningDate.Year + 1;
                        }
                        else
                        {
                            monthJ = JoiningDate.Month + 1;
                            yearJ = JoiningDate.Year;
                        }

                        JoiningDate = new DateTime(yearJ, monthJ, 1);


                        double RemainDays = FinancialYearEndDate.Subtract(JoiningDate).Days;

                        double remainmonth = RemainDays / 30;

                        remainmonth = System.Math.Round(remainmonth);

                        int totalleaves = 0;

                        double Totalmonths = remainmonth * leavepermonth;

                        totalleaves = int.Parse(System.Math.Round(Totalmonths).ToString()) + int.Parse(System.Math.Round(leave).ToString());

                        objEmpleave.DeleteEmployeeLeaveTransByEmpIdandleaveTypeId(EmpId, LeaveTypeId, "0", JoiningDate.Year.ToString());


                        objEmpleave.InsertEmployeeLeaveTrans(Session["CompId"].ToString(), EmpId, LeaveTypeId, JoiningDate.Year.ToString(), "0", "0", totalleaves.ToString(), totalleaves.ToString(), "0", totalleaves.ToString(), "0", "", "", "", "", "", true.ToString(), DateTime.Now.ToString(), true.ToString(), Session["UserId"].ToString(), DateTime.Now.ToString(), Session["UserId"].ToString(), DateTime.Now.ToString());




                    }
                    else if (PrevSchduleType == "Monthly")
                    {

                        // Monthly to Yearly 14


                        //JoiningDate = new DateTime(DateTime.Now.Year, DateTime.Now.Month, DateTime.Now.Day);

                        DateTime JoininDate1 = JoiningDate;
                        if (JoiningDate.Month == DateTime.Now.Month)
                        {

                            while (JoiningDate <= FinancialYearEndDate)
                            {
                                objEmpleave.DeleteEmployeeLeaveTransByEmpIdandleaveTypeId(EmpId, LeaveTypeId, JoiningDate.Month.ToString(), JoiningDate.Year.ToString());
                                JoiningDate = JoiningDate.AddMonths(1);

                            }
                            double leave = 0;

                            JoiningDate = JoininDate1;


                            if (JoiningDate.Day != 1)
                            {

                                double JoinDay = 30 - JoiningDate.Day;

                                double Day = (Convert.ToDouble(JoinDay) * Convert.ToDouble(leavepermonth)) / 30;



                                leave = double.Parse(System.Math.Round(Day).ToString());
                            }
                            else
                            {
                                leave = double.Parse(Convert.ToDouble(leavepermonth).ToString());

                            }

                            int monthJ = 0;
                            int yearJ = 0;



                            if (JoiningDate.Month == 12)
                            {
                                monthJ = 1;
                                yearJ = JoiningDate.Year + 1;
                            }
                            else
                            {
                                monthJ = JoiningDate.Month + 1;
                                yearJ = JoiningDate.Year;
                            }

                            DateTime JoiningDate2 = new DateTime(yearJ, monthJ, 1);



                            double RemainDays = FinancialYearEndDate.Subtract(JoiningDate2).Days;

                            double remainmonth = RemainDays / 30;

                            remainmonth = System.Math.Round(remainmonth);

                            int totalleaves = 0;

                            double Totalmonths = remainmonth * leavepermonth;

                            totalleaves = int.Parse(System.Math.Round(Totalmonths).ToString()) + int.Parse(System.Math.Round(leave).ToString());




                            objEmpleave.InsertEmployeeLeaveTrans(Session["CompId"].ToString(), EmpId, LeaveTypeId, JoiningDate.Year.ToString(), "0", "0", totalleaves.ToString(), totalleaves.ToString(), "0", totalleaves.ToString(), "0", "", "", "", "", "", true.ToString(), DateTime.Now.ToString(), true.ToString(), Session["UserId"].ToString(), DateTime.Now.ToString(), Session["UserId"].ToString(), DateTime.Now.ToString());


                        }
                        else
                        {



                            JoiningDate = new DateTime(DateTime.Now.Year, DateTime.Now.Month, DateTime.Now.Day);

                            int monthJ = 0;
                            int yearJ = 0;



                            if (JoiningDate.Month == 12)
                            {
                                monthJ = 1;
                                yearJ = JoiningDate.Year + 1;
                            }
                            else
                            {
                                monthJ = JoiningDate.Month + 1;
                                yearJ = JoiningDate.Year;
                            }

                            JoiningDate = new DateTime(yearJ, monthJ, 1);


                            while (JoiningDate <= FinancialYearEndDate)
                            {
                                objEmpleave.DeleteEmployeeLeaveTransByEmpIdandleaveTypeId(EmpId, LeaveTypeId, JoiningDate.Month.ToString(), JoiningDate.Year.ToString());
                                JoiningDate = JoiningDate.AddMonths(1);
                            }




                            JoiningDate = new DateTime(yearJ, monthJ, 1);





                            double RemainDays = FinancialYearEndDate.Subtract(JoiningDate).Days;

                            double remainmonth = RemainDays / 30;

                            remainmonth = System.Math.Round(remainmonth);

                            int totalleaves = 0;

                            double Totalmonths = remainmonth * leavepermonth;

                            totalleaves = int.Parse(System.Math.Round(Totalmonths).ToString());

                            DataTable dtLeave = objEmpleave.GetEmployeeLeaveTransactionData(EmpId, LeaveTypeId, "0", JoiningDate.Year.ToString());



                            objEmpleave.DeleteEmployeeLeaveTransByEmpIdandleaveTypeId(EmpId, LeaveTypeId, "0", JoiningDate.Year.ToString());




                            if (dtLeave.Rows.Count > 0)
                            {
                                totalleaves += int.Parse(dtLeave.Rows[0]["Assign_Days"].ToString());
                            }



                            objEmpleave.InsertEmployeeLeaveTrans(Session["CompId"].ToString(), EmpId, LeaveTypeId, JoiningDate.Year.ToString(), "0", "0", totalleaves.ToString(), totalleaves.ToString(), "0", totalleaves.ToString(), "0", "", "", "", "", "", true.ToString(), DateTime.Now.ToString(), true.ToString(), Session["UserId"].ToString(), DateTime.Now.ToString(), Session["UserId"].ToString(), DateTime.Now.ToString());










                        }
                    }
                }
                else if (SchType == "Monthly")
                {


                    if (PrevSchduleType == "Monthly")
                    {

                        if (JoiningDate.Month == DateTime.Now.Month)
                        {

                        }


                        else
                        {

                            JoiningDate = new DateTime(DateTime.Now.Year, DateTime.Now.Month, DateTime.Now.Day);
                        }




                        while (JoiningDate <= FinancialYearEndDate)
                        {
                            objEmpleave.DeleteEmployeeLeaveTransByEmpIdandleaveTypeId(EmpId, LeaveTypeId, JoiningDate.Month.ToString(), JoiningDate.Year.ToString());



                            int leave = 0;
                            if (JoiningDate.Day != 1)
                            {

                                double JoinDay = 30 - JoiningDate.Day;

                                double Day = (Convert.ToDouble(JoinDay) * Convert.ToDouble(AssignLeave)) / 30;



                                leave = int.Parse(System.Math.Round(Day).ToString());
                            }
                            else
                            {
                                leave = int.Parse(Convert.ToDouble(AssignLeave).ToString());

                            }


                            objEmpleave.InsertEmployeeLeaveTrans(Session["CompId"].ToString(), EmpId, LeaveTypeId, JoiningDate.Year.ToString(), JoiningDate.Month.ToString(), "0", leave.ToString(), leave.ToString(), "0", leave.ToString(), "0", "", "", "", "", "", true.ToString(), DateTime.Now.ToString(), true.ToString(), Session["UserId"].ToString(), DateTime.Now.ToString(), Session["UserId"].ToString(), DateTime.Now.ToString());
                            JoiningDate = JoiningDate.AddMonths(1);
                            JoiningDate = new DateTime(JoiningDate.Year, JoiningDate.Month, 1);

                        }





                    }
                    else if (PrevSchduleType == "Yearly")
                    {
                        //yearly to monthly 17
                        if (JoiningDate.Month == DateTime.Now.Month)
                        {
                            // current month to end will delete





                            while (JoiningDate <= FinancialYearEndDate)
                            {
                                objEmpleave.DeleteEmployeeLeaveTransByEmpIdandleaveTypeId(EmpId, LeaveTypeId, "0", JoiningDate.Year.ToString());



                                int leave = 0;
                                if (JoiningDate.Day != 1)
                                {

                                    double JoinDay = 30 - JoiningDate.Day;

                                    double Day = (Convert.ToDouble(JoinDay) * Convert.ToDouble(AssignLeave)) / 30;



                                    leave = int.Parse(System.Math.Round(Day).ToString());
                                }
                                else
                                {
                                    leave = int.Parse(Convert.ToDouble(AssignLeave).ToString());

                                }


                                objEmpleave.InsertEmployeeLeaveTrans(Session["CompId"].ToString(), EmpId, LeaveTypeId, JoiningDate.Year.ToString(), JoiningDate.Month.ToString(), "0", leave.ToString(), leave.ToString(), "0", leave.ToString(), "0", "", "", "", "", "", true.ToString(), DateTime.Now.ToString(), true.ToString(), Session["UserId"].ToString(), DateTime.Now.ToString(), Session["UserId"].ToString(), DateTime.Now.ToString());
                                JoiningDate = JoiningDate.AddMonths(1);
                                JoiningDate = new DateTime(JoiningDate.Year, JoiningDate.Month, 1);

                            }
                        }
                        else
                        {
                            //next month to end will delete







                            JoiningDate = new DateTime(DateTime.Now.Year, DateTime.Now.Month, DateTime.Now.Day);




                            int leave = 0;
                            if (JoiningDate.Day != 1)
                            {

                                double JoinDay = 30 - JoiningDate.Day;

                                double Day = (Convert.ToDouble(JoinDay) * Convert.ToDouble(leavepermonthPrev)) / 30;

                                leave = int.Parse(System.Math.Round(Day).ToString());
                            }
                            else
                            {
                                leave = int.Parse(Convert.ToDouble(leavepermonthPrev).ToString());

                            }





                            int Totalleave = int.Parse(leave.ToString());


                            objEmpleave.DeleteEmployeeLeaveTransByEmpIdandleaveTypeId(EmpId, LeaveTypeId, "0", JoiningDate.Year.ToString());


                            objEmpleave.InsertEmployeeLeaveTrans(Session["CompId"].ToString(), EmpId, LeaveTypeId, JoiningDate.Year.ToString(), "0", "0", Totalleave.ToString(), Totalleave.ToString(), "0", Totalleave.ToString(), "0", "", "", "", "", "", true.ToString(), DateTime.Now.ToString(), true.ToString(), Session["UserId"].ToString(), DateTime.Now.ToString(), Session["UserId"].ToString(), DateTime.Now.ToString());









                            JoiningDate = new DateTime(DateTime.Now.Year, DateTime.Now.Month, DateTime.Now.Day);
                            int monthJ = 0;
                            int yearJ = 0;



                            if (JoiningDate.Month == 12)
                            {
                                monthJ = 1;
                                yearJ = JoiningDate.Year + 1;
                            }
                            else
                            {
                                monthJ = JoiningDate.Month + 1;
                                yearJ = JoiningDate.Year;
                            }

                            JoiningDate = new DateTime(yearJ, monthJ, 1);




                            while (JoiningDate <= FinancialYearEndDate)
                            {



                                int leave1 = 0;

                                leave1 = int.Parse(Convert.ToDouble(AssignLeave).ToString());


                                objEmpleave.InsertEmployeeLeaveTrans(Session["CompId"].ToString(), EmpId, LeaveTypeId, JoiningDate.Year.ToString(), JoiningDate.Month.ToString(), "0", leave1.ToString(), leave1.ToString(), "0", leave1.ToString(), "0", "", "", "", "", "", true.ToString(), DateTime.Now.ToString(), true.ToString(), Session["UserId"].ToString(), DateTime.Now.ToString(), Session["UserId"].ToString(), DateTime.Now.ToString());
                                JoiningDate = JoiningDate.AddMonths(1);
                                JoiningDate = new DateTime(JoiningDate.Year, JoiningDate.Month, 1);

                            }



                        }

                    }

                }

            }

        }






    }


    public string GetLeaveStatus(object TransId)
    {
        string status = string.Empty;
        DataTable dt = objleaveReq.GetLeaveRequest(Session["CompId"].ToString());
        dt = new DataView(dt, "Trans_Id='" + TransId.ToString() + "'", "", DataViewRowState.CurrentRows).ToTable();
        if (dt.Rows.Count > 0)
        {
            if (Convert.ToBoolean(dt.Rows[0]["Is_Pending"].ToString()))
            {
                status = "Pending";
            }
            else if (Convert.ToBoolean(dt.Rows[0]["Is_Approved"].ToString()))
            {
                status = "Approved";
            }
            else if (Convert.ToBoolean(dt.Rows[0]["Is_Canceled"].ToString()))
            {
                status = "Rejected";
            }

        }

        return status;

    }
    public void FillLeaveApproveRejected(string empid)
    {
        DataTable dtLeave = objleaveReq.GetLeaveRequest(Session["CompId"].ToString());

        dtLeave = new DataView(dtLeave,"Emp_Id='"+empid+"' and Is_Pending<>'True'", "", DataViewRowState.CurrentRows).ToTable();

        if (dtLeave.Rows.Count > 0)
        {
            Session["dtLeaveStatus"] = dtLeave;
            gvLeave.DataSource = dtLeave;
            gvLeave.DataBind();

        }
    }
    public string GetMonthName(object month,object monthname)
    {
        string month1 = string.Empty;
        if (month.ToString() == "0")
        {
            month1 = "-";
        }
        else
        {
            month1 = monthname.ToString();

        }
        return month1;
    }
   
    protected void txtToDate_textChanged(object sender, EventArgs e)
    {
        lblDays.Text = "";
        if (txtFromDate.Text == "")
        {
            DisplayMessage("Enter From Date");
            txtFromDate.Focus();
            return;

        }
        else
        {
            try
            {
                objSys.getDateForInput(txtFromDate.Text);
            }
            catch
            {
                DisplayMessage("Enter Correct From Date Format "+objSys.SetDateFormat()+"");
                txtFromDate.Focus();
                return;

            }

        }

        if (txtToDate.Text != "")
        {
            try
            {
                objSys.getDateForInput(txtToDate.Text);
            }
            catch
            {
                DisplayMessage("Enter Correct To Date Format "+objSys.SetDateFormat()+"");
                txtToDate.Focus();
                return;

            }


            if (objSys.getDateForInput(txtFromDate.Text) > objSys.getDateForInput(txtToDate.Text))
            {
                DisplayMessage("From Date cannot be greater than To Date");
                txtFromDate.Text = "";
                txtToDate.Text = "";
                txtFromDate.Focus();

                return;

            }




            //DataTable dtHoliday = objEmpHoliday.GetEmployeeHolidayMaster(Session["CompId"].ToString());

            //DataTable dtLeaveReq = objleaveReq.GetLeaveRequestById(Session["CompId"].ToString(), hdnEmpId.Value);






            DateTime fromdate = objSys.getDateForInput(txtFromDate.Text);
            DateTime todate = objSys.getDateForInput(txtToDate.Text);
            int days = 0;
            while (fromdate <= todate)
            {
                //new DataView(dtHoliday, "Holiday_Date='" + fromdate.ToString() + "' and Emp_Id='" + hdnEmpId.Value + "'", "", DataViewRowState.CurrentRows).ToTable();
                //DataTable dtLeaveReq1 = new DataView(dtLeaveReq, "From_Date >='" + fromdate.ToString() + "' and To_Date<='" + fromdate.ToString() + "'", "", DataViewRowState.CurrentRows).ToTable();

                //if (dtHoliday.Rows.Count == 0 )
                //{
                    days += 1;

                //}


                fromdate = fromdate.AddDays(1);

            }



            // Here require week off code
            lblDays.Text = days.ToString();
        }

    }


    
     protected void gvLeave_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        gvLeave.PageIndex = e.NewPageIndex;

        DataTable dt = (DataTable)Session["dtLeaveStatus"];
        gvLeave.DataSource = dt;
        gvLeave.DataBind();
      

    }
    protected void btnApply_Click(object sender, EventArgs e)
    {
        if (txtFromDate.Text == "")
        {
            DisplayMessage("Enter From Date");
            txtFromDate.Focus();
            rbtnMonthly.Checked = false;
            rbtnYearly.Checked = false;
            return;

        }
        else
        {
            try
            {
                Convert.ToDateTime(txtFromDate.Text);
            }
            catch
            {
                DisplayMessage("Enter Correct From Date Format dd-MMM-yyyy");
                txtFromDate.Focus();
                rbtnMonthly.Checked = false;
                rbtnYearly.Checked = false;
                return;

            }

        }
        if (Convert.ToDateTime(txtFromDate.Text) > Convert.ToDateTime(txtToDate.Text))
        {
            DisplayMessage("From Date cannot be greater than To Date");
            txtFromDate.Text = "";
            txtToDate.Text = "";
            txtFromDate.Focus();
            rbtnMonthly.Checked = false;
            rbtnYearly.Checked = false;
            return;

        }

        if (txtToDate.Text == "")
        {
            DisplayMessage("Enter To Date");
            txtToDate.Focus();
            rbtnMonthly.Checked = false;
            rbtnYearly.Checked = false;
            return;

        }
        else
        {
            try
            {
                Convert.ToDateTime(txtToDate.Text);
            }
            catch
            {
                DisplayMessage("Enter Correct To Date Format dd-MMM-yyyy");
                txtToDate.Focus();
                rbtnMonthly.Checked = false;
                rbtnYearly.Checked = false;
                return;

            }

        }


        if(rbtnMonthly.Checked==false&& rbtnYearly.Checked==false)
        {
            DisplayMessage("Please select Monthly or Yearly");
            return;

        }






        DateTime FromDate = Convert.ToDateTime(txtFromDate.Text);
        DateTime ToDate = Convert.ToDateTime(txtToDate.Text);

       
        int dayscount = int.Parse(lblDays.Text);

        string month = FromDate.Month.ToString();
        DateTime fromdate2 = Convert.ToDateTime(txtFromDate.Text);
        DateTime todate2 = Convert.ToDateTime(txtToDate.Text);

        DataTable dtLeaveSummary = objEmpleave.GetEmployeeLeaveTransactionDataByEmpId(hdnEmpId.Value);
        DataTable dtHoliday2 = objEmpHoliday.GetEmployeeHolidayMaster(Session["CompId"].ToString());

        
        while (fromdate2 <= todate2)
        {

            DataTable dtHoliday1 = new DataView(dtHoliday2, "Holiday_Date='" + fromdate2.ToString() + "' and Emp_Id='"+hdnEmpId.Value+"'", "", DataViewRowState.CurrentRows).ToTable();



            if(dtHoliday1.Rows.Count > 0)
            {

                DisplayMessage("You have holiday on date "+fromdate2.ToString("dd-MMM-yyyy")+" so cannot apply");
                     rbtnMonthly.Checked = false;
            rbtnYearly.Checked = false;
            ddlLeaveType.Items.Clear();
            ddlLeaveType.DataSource = null;
            ddlLeaveType.DataBind();
            ddlLeaveType.Visible = false;
            txtFromDate.Text = "";
            txtToDate.Text = "";
            lblDays.Text = "";
                    return;
            }

            fromdate2 = fromdate2.AddDays(1);
        
        }

         DataTable dtLeaveR = objleaveReq.GetLeaveRequestById(Session["CompId"].ToString(), hdnEmpId.Value);
         dtLeaveR = new DataView(dtLeaveR,"Is_Canceled<>'True'","",DataViewRowState.CurrentRows).ToTable();
         DateTime fromdate1 = Convert.ToDateTime(txtFromDate.Text);
            DateTime todate1 = Convert.ToDateTime(txtToDate.Text);
          
            while (fromdate1 <= todate1)
            {
                DataTable dtLeaveReq2 = new DataView(dtLeaveR, "From_Date <='" + fromdate1.ToString() + "' and To_Date>='" + fromdate1.ToString() + "'", "", DataViewRowState.CurrentRows).ToTable();
          
                if(dtLeaveReq2.Rows.Count > 0)
                {
                    DisplayMessage("You have already apply leave between from date and to date");
                     rbtnMonthly.Checked = false;
            rbtnYearly.Checked = false;
            ddlLeaveType.Items.Clear();
            ddlLeaveType.DataSource = null;
            ddlLeaveType.DataBind();
            ddlLeaveType.Visible = false;
            txtFromDate.Text = "";
            txtToDate.Text = "";
            lblDays.Text = "";
                    return;

                }


                fromdate1=fromdate1.AddDays(1);
            }
        
        
        if(rbtnMonthly.Checked)
        {


            string months = string.Empty;
            string year = string.Empty;
            DateTime FromDate2 = DateTime.Now;
            DateTime ToDate2 = DateTime.Now.AddMonths(2);
            while (FromDate2 <= ToDate2)
            {
                months += FromDate2.Month.ToString() + ",";
                FromDate2 = FromDate2.AddMonths(1);
                string year1 = FromDate2.Year.ToString();
                if (!year.Split(',').Contains(year1))
                {
                    year += year1 + ",";
                }
            }

            DataTable dt = objAppParam.GetApplicationParameterByParamName("FinancialYearStartMonth", Session["CompId"].ToString());
            int FinancialYearMonth = 0;

            if (dt.Rows.Count > 0)
            {
                FinancialYearMonth = int.Parse(dt.Rows[0]["Param_Value"].ToString());

            }

            DateTime FinancialYearStartDate = new DateTime();
            DateTime FinancialYearEndDate = new DateTime();
            if (DateTime.Now.Month < FinancialYearMonth)
            {

                FinancialYearStartDate = new DateTime(DateTime.Now.Year - 1, FinancialYearMonth, 1);

                FinancialYearEndDate = FinancialYearStartDate.AddYears(1).AddDays(-1);
            }
            else
            {
                FinancialYearStartDate = new DateTime(DateTime.Now.Year, FinancialYearMonth, 1);
                FinancialYearEndDate = FinancialYearStartDate.AddYears(1).AddDays(-1);

            }

            string year4 = string.Empty;
            string months4 = string.Empty;

            months4 = months;
            if (FinancialYearStartDate.Year == FinancialYearEndDate.Year)
            {
                year4 = FinancialYearStartDate.Year.ToString();


            }
            else
            {
                year4 += FinancialYearStartDate.Year.ToString() + ",";

                year4 += FinancialYearEndDate.Year.ToString() + ",";
            }

            DateTime DateFrm = Convert.ToDateTime(txtFromDate.Text);

            if (!months4.Split(',').Contains(DateFrm.Month.ToString()) && !year4.Split(',').Contains(DateFrm.Year.ToString()))
            {
                DisplayMessage("You cannot request leave for this month");
                return;

            }


            //


            dtLeaveSummary = new DataView(dtLeaveSummary, "month in(" + month + ") and year in (" + year4 + ") ", "", DataViewRowState.CurrentRows).ToTable();

        int remainingdays = 0;
        if(dtLeaveSummary.Rows.Count > 0)
        {
            remainingdays = int.Parse(dtLeaveSummary.Rows[0]["Remaining_Days"].ToString());
            
        }
        if (dayscount > remainingdays)
        {
            DisplayMessage("You do not have sufficient leave");
            return;
        }
        else
        {
            int b=0;
            b=objleaveReq.InsertLeaveRequest(Session["CompId"].ToString(),ddlLeaveType.SelectedValue,hdnEmpId.Value,DateTime.Now.ToString(),txtFromDate.Text,txtToDate.Text,true.ToString(),false.ToString(),false.ToString(),"","","", "", "", "", "", true.ToString(), DateTime.Now.ToString(), true.ToString(), Session["UserId"].ToString(), DateTime.Now.ToString(), Session["UserId"].ToString(), DateTime.Now.ToString());

            DataTable dtLeave= objEmpleave.GetEmployeeLeaveTransactionData(hdnEmpId.Value,ddlLeaveType.SelectedValue,FromDate.Month.ToString(),FromDate.Year.ToString());

            string TransNo = string.Empty;
            int remain = 0;
            int useddays = 0;
            int totaldays = 0;
            if (dtLeave.Rows.Count > 0)
            {

                TransNo = dtLeave.Rows[0]["Trans_Id"].ToString();
                remain = int.Parse(dtLeave.Rows[0]["Remaining_Days"].ToString());
                totaldays = int.Parse(dtLeave.Rows[0]["Total_Days"].ToString());

            }

            remain = remain - dayscount;
            useddays = totaldays - remain;

            objEmpleave.UpdateEmployeeLeaveTransactionByTransNo(TransNo, Session["CompId"].ToString(), hdnEmpId.Value, ddlLeaveType.SelectedValue, FromDate.Year.ToString(), FromDate.Month.ToString(), "0", "0", "0", useddays.ToString(), remain.ToString(), dayscount.ToString(), Session["UserId"].ToString(), DateTime.Now.ToString());
                

            
            while (FromDate <= ToDate)
            {
               objleaveReq.InsertLeaveRequestChild(b.ToString(), ddlLeaveType.SelectedValue, FromDate.ToString(), true.ToString(), "1", "", "", "", "", "", false.ToString(), DateTime.Now.ToString(), true.ToString(), Session["UserId"].ToString(), DateTime.Now.ToString(), Session["UserId"].ToString(), DateTime.Now.ToString());


               


                FromDate = FromDate.AddDays(1);

            }
            DisplayMessage("Leave submitted");

            FillLeaveSummary(Session["UserId"].ToString());
            FillLeaveStatus(hdnEmpId.Value);
            rbtnMonthly.Checked = false;
            rbtnYearly.Checked = false;
            ddlLeaveType.Items.Clear();
            ddlLeaveType.DataSource = null;
            ddlLeaveType.DataBind();
            ddlLeaveType.Visible = false;
            txtFromDate.Text = "";
            txtToDate.Text = "";
            lblDays.Text = "";
        }

         }
         else if (rbtnYearly.Checked)
         {
             string year4 = string.Empty;

             DataTable dt = objAppParam.GetApplicationParameterByParamName("FinancialYearStartMonth", Session["CompId"].ToString());
             int FinancialYearMonth = 0;

             if (dt.Rows.Count > 0)
             {
                 FinancialYearMonth = int.Parse(dt.Rows[0]["Param_Value"].ToString());

             }

             DateTime FinancialYearStartDate = new DateTime();
             DateTime FinancialYearEndDate = new DateTime();
             if (DateTime.Now.Month < FinancialYearMonth)
             {

                 FinancialYearStartDate = new DateTime(DateTime.Now.Year - 1, FinancialYearMonth, 1);

                 FinancialYearEndDate = FinancialYearStartDate.AddYears(1).AddDays(-1);
             }
             else
             {
                 FinancialYearStartDate = new DateTime(DateTime.Now.Year, FinancialYearMonth, 1);
                 FinancialYearEndDate = FinancialYearStartDate.AddYears(1).AddDays(-1);

             }
            

             if (FinancialYearStartDate.Year == FinancialYearEndDate.Year)
             {
                 year4 = FinancialYearStartDate.Year.ToString();


             }
             else
             {
                 year4 = FinancialYearStartDate.Year.ToString();

                
             }





             dtLeaveSummary = new DataView(dtLeaveSummary, "month='0' and Year in(" + year4 + ")", "", DataViewRowState.CurrentRows).ToTable();


             int remainingdays = 0;
             if (dtLeaveSummary.Rows.Count > 0)
             {
                 remainingdays = int.Parse(dtLeaveSummary.Rows[0]["Remaining_Days"].ToString());

             }
             if (dayscount > remainingdays)
             {
                 DisplayMessage("You do not have sufficient leave");
                 return;
             }
             else
             {
                 int b = 0;
                 b = objleaveReq.InsertLeaveRequest(Session["CompId"].ToString(),ddlLeaveType.SelectedValue, hdnEmpId.Value, DateTime.Now.ToString(), txtFromDate.Text, txtToDate.Text, true.ToString(), false.ToString(), false.ToString(), "", "", "", "", "", "", "", true.ToString(), DateTime.Now.ToString(), true.ToString(), Session["UserId"].ToString(), DateTime.Now.ToString(), Session["UserId"].ToString(), DateTime.Now.ToString());

                 DataTable dtLeave = objEmpleave.GetEmployeeLeaveTransactionData(hdnEmpId.Value, ddlLeaveType.SelectedValue, "0",year4);
                
                 string TransNo = string.Empty;
                 int remain = 0;
                 int useddays = 0;
                 int totaldays = 0;
                 if (dtLeave.Rows.Count > 0)
                 {

                     TransNo = dtLeave.Rows[0]["Trans_Id"].ToString();
                     remain = int.Parse(dtLeave.Rows[0]["Remaining_Days"].ToString());
                     totaldays = int.Parse(dtLeave.Rows[0]["Total_Days"].ToString());

                 }
               
                 remain = remain - dayscount;
                 useddays = totaldays - remain;
                
                 objEmpleave.UpdateEmployeeLeaveTransactionByTransNo(TransNo, Session["CompId"].ToString(), hdnEmpId.Value, ddlLeaveType.SelectedValue, year4,"0", "0", "0", "0",useddays.ToString(), remain.ToString(), dayscount.ToString(), Session["UserId"].ToString(), DateTime.Now.ToString());
                
                 while (FromDate <= ToDate)
                 {
                  
                         objleaveReq.InsertLeaveRequestChild(b.ToString(), ddlLeaveType.SelectedValue, FromDate.ToString(), true.ToString(), "1", "", "", "", "", "", false.ToString(), DateTime.Now.ToString(), true.ToString(), Session["UserId"].ToString(), DateTime.Now.ToString(), Session["UserId"].ToString(), DateTime.Now.ToString());


                    


                     FromDate = FromDate.AddDays(1);

                 }
                 DisplayMessage("Leave submitted");
                 FillLeaveSummary(Session["UserId"].ToString());
                 FillLeaveStatus(hdnEmpId.Value);
                 rbtnMonthly.Checked = false;
                 rbtnYearly.Checked = false;
                 ddlLeaveType.Items.Clear();
                 ddlLeaveType.DataSource = null;
                 ddlLeaveType.DataBind();
                 ddlLeaveType.Visible = false;
                 txtFromDate.Text = "";
                 txtToDate.Text = "";
                 lblDays.Text = "";

             }
         }
}

    protected void rbtnMonthlyYearly(object sender, EventArgs e)
    {
        if (txtFromDate.Text == "")
        {
            DisplayMessage("Enter From Date");
            rbtnMonthly.Checked = false;
            rbtnYearly.Checked = false;
            txtFromDate.Focus();
            return;

        }
        else
        {
            try
            {
                Convert.ToDateTime(txtFromDate.Text);
            }
            catch
            {
                DisplayMessage("Enter Correct From Date Format dd-MMM-yyyy");
                txtFromDate.Focus();
                rbtnMonthly.Checked = false;
                rbtnYearly.Checked = false;
                return;

            }

        }

        if (txtToDate.Text == "")
        {
            DisplayMessage("Enter To Date");
            txtToDate.Focus();
            rbtnMonthly.Checked = false;
            rbtnYearly.Checked = false;
            return;

        }
        else
        {
            try
            {
                Convert.ToDateTime(txtToDate.Text);
            }
            catch
            {
                DisplayMessage("Enter Correct To Date Format dd-MMM-yyyy");
                txtToDate.Focus();
                rbtnMonthly.Checked = false;
                rbtnYearly.Checked = false;
                return;

            }

        }
        if (Convert.ToDateTime(txtFromDate.Text) > Convert.ToDateTime(txtToDate.Text))
        {
            DisplayMessage("From Date cannot be greater than To Date");
            txtFromDate.Text = "";
            txtToDate.Text = "";
            txtFromDate.Focus();
            rbtnMonthly.Checked = false;
            rbtnYearly.Checked = false;
            return;

        }

        

        if(rbtnMonthly.Checked)
        {
            DataTable dtLeaveSummary = objEmpleave.GetEmployeeLeaveTransactionDataByEmpId(hdnEmpId.Value);

            string months = string.Empty;
            string year = string.Empty;
            DateTime FromDate = Convert.ToDateTime(txtFromDate.Text);
            DateTime ToDate = Convert.ToDateTime(txtToDate.Text);
            while (FromDate <= ToDate)
            {
                months += FromDate.Month.ToString() + ",";
                FromDate = FromDate.AddMonths(1);
                string year1 = FromDate.Year.ToString();
                if (!year.Split(',').Contains(year1))
                {
                    year += year1 + ",";
                }
            }


            string months4 = string.Empty;
            string year4 = string.Empty;
            DateTime FromDate1 = DateTime.Now;
            DateTime ToDate1 = DateTime.Now.AddMonths(2);
            while (FromDate <= ToDate)
            {
                months4 += FromDate1.Month.ToString() + ",";
                FromDate = FromDate1.AddMonths(1);
                string year5 = FromDate1.Year.ToString();
                if (!year4.Split(',').Contains(year5))
                {
                    year4 += year5 + ",";
                }
            }

            DataTable dt = objAppParam.GetApplicationParameterByParamName("FinancialYearStartMonth", Session["CompId"].ToString());
            int FinancialYearMonth = 0;

            if (dt.Rows.Count > 0)
            {
                FinancialYearMonth = int.Parse(dt.Rows[0]["Param_Value"].ToString());

            }

            DateTime FinancialYearStartDate = new DateTime();
            DateTime FinancialYearEndDate = new DateTime();
            if (DateTime.Now.Month < FinancialYearMonth)
            {

                FinancialYearStartDate = new DateTime(DateTime.Now.Year - 1, FinancialYearMonth, 1);

                FinancialYearEndDate = FinancialYearStartDate.AddYears(1).AddDays(-1);
            }
            else
            {
                FinancialYearStartDate = new DateTime(DateTime.Now.Year, FinancialYearMonth, 1);
                FinancialYearEndDate = FinancialYearStartDate.AddYears(1).AddDays(-1);

            }
            year = string.Empty;

            if (FinancialYearStartDate.Year == FinancialYearEndDate.Year)
            {
                year4 = FinancialYearStartDate.Year.ToString();


            }
            else
            {
                year4 += FinancialYearStartDate.Year.ToString() + ",";

                year4 += FinancialYearEndDate.Year.ToString() + ",";
            }

            DateTime DateFrm = Convert.ToDateTime(txtFromDate.Text);

            if (!months4.Split(',').Contains(DateFrm.Month.ToString()) && !year4.Split(',').Contains(DateFrm.Year.ToString()))
            {
                DisplayMessage("You cannot request leave for this month");
                txtFromDate.Text = "";
                txtToDate.Text = "";
                txtFromDate.Focus();
                rbtnMonthly.Checked = false;
                rbtnYearly.Checked = false;
                lblDays.Text = "";
                return;

            }




            dtLeaveSummary = new DataView(dtLeaveSummary, "month in(" + months + ") and year in (" + year4 + ") ", "", DataViewRowState.CurrentRows).ToTable();
             dtLeaveSummary = dtLeaveSummary.DefaultView.ToTable(true, "Leave_Type_Id");
             DataTable dtleave = new DataTable();
             for (int i=0;i<dtLeaveSummary.Rows.Count;i++)
             {
                  dtleave = objleave.GetLeaveMasterById(Session["CompId"].ToString(),dtLeaveSummary.Rows[i]["Leave_Type_Id"].ToString());
             }
             if (dtleave.Rows.Count > 0)
            {
                ddlLeaveType.DataSource = dtleave;
                ddlLeaveType.DataTextField = "Leave_Name";
                ddlLeaveType.DataValueField = "Leave_Id";

                ddlLeaveType.DataBind();
                ddlLeaveType.Visible = true;
            }
             else
             {
                 ddlLeaveType.Items.Clear();
                 ddlLeaveType.DataSource = null;
                 ddlLeaveType.DataBind();
                 ddlLeaveType.Visible = false;
             }
        }
        else if(rbtnYearly.Checked)
        {
            DataTable dtLeaveSummary = objEmpleave.GetEmployeeLeaveTransactionDataByEmpId(hdnEmpId.Value);

            string months = string.Empty;
            string year = string.Empty;
            DateTime FromDate = DateTime.Now;

          
            string year4 = string.Empty;


            DataTable dt = objAppParam.GetApplicationParameterByParamName("FinancialYearStartMonth", Session["CompId"].ToString());
            int FinancialYearMonth = 0;

            if (dt.Rows.Count > 0)
            {
                FinancialYearMonth = int.Parse(dt.Rows[0]["Param_Value"].ToString());

            }

            DateTime FinancialYearStartDate = new DateTime();
            DateTime FinancialYearEndDate = new DateTime();
            if (DateTime.Now.Month < FinancialYearMonth)
            {

                FinancialYearStartDate = new DateTime(DateTime.Now.Year - 1, FinancialYearMonth, 1);

                FinancialYearEndDate = FinancialYearStartDate.AddYears(1).AddDays(-1);
            }
            else
            {
                FinancialYearStartDate = new DateTime(DateTime.Now.Year, FinancialYearMonth, 1);
                FinancialYearEndDate = FinancialYearStartDate.AddYears(1).AddDays(-1);

            }
            year = string.Empty;

            if (FinancialYearStartDate.Year == FinancialYearEndDate.Year)
            {
                year4 = FinancialYearStartDate.Year.ToString();


            }
            else
            {
                year4 += FinancialYearStartDate.Year.ToString() + ",";

                
            }

            dtLeaveSummary = new DataView(dtLeaveSummary, "month='0'and year in (" + year4.ToString() + ") ", "", DataViewRowState.CurrentRows).ToTable();

            if (dtLeaveSummary.Rows.Count > 0)
            {
                ddlLeaveType.DataSource = dtLeaveSummary;

                ddlLeaveType.DataTextField = "Leave_Name";
                ddlLeaveType.DataValueField = "Leave_Type_Id";


                ddlLeaveType.DataBind();
                ddlLeaveType.Visible = true;
            }
            else
            {
                ddlLeaveType.Items.Clear();
                ddlLeaveType.DataSource = null;
                ddlLeaveType.DataBind();
                ddlLeaveType.Visible = false;
            }
        }
        else if(rbtnMonthly.Checked ==false && rbtnYearly.Checked==false)
        {

            ddlLeaveType.Items.Clear();
            ddlLeaveType.DataSource = null;
            ddlLeaveType.DataBind();
            ddlLeaveType.Visible = false;
        }


    }

    public void DisplayMessage(string str)
    {
        ScriptManager.RegisterStartupScript(this, GetType(), "", "alert('" + str + "');", true);
    }
}
