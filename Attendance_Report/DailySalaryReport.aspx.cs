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
using System.Data.SqlClient;

public partial class AttendanceReport_DailySalaryReport : System.Web.UI.Page
{

    Att_DailySalaryReport1 RptShift = new Att_DailySalaryReport1();
    EmployeeParameter objEmpParam = new EmployeeParameter();
    Set_ApplicationParameter objAppParam = new Set_ApplicationParameter();
    CompanyMaster objComp=new CompanyMaster();
    Set_AddressChild ObjAddress=new Set_AddressChild ();
    SystemParameter objSys = new SystemParameter();
    protected void Page_Load(object sender, EventArgs e)
    {
        GetReport();
        Session["AccordianId"] = "6";
        Session["HeaderText"] = "Attendance Reports";
        
    }

   
    public void GetReport()
    {
        DateTime FromDate = new DateTime();
        DateTime ToDate = new DateTime();
        DateTime FromDate1 = new DateTime();
        string Emplist = string.Empty;
        if (Session["EmpList"] == null)
        {
            Response.Redirect("../Attendance_Report/AttendanceReport.aspx");
        }
        else
        {
            FromDate = objSys.getDateForInput(Session["FromDate"].ToString());
            ToDate = objSys.getDateForInput(Session["ToDate"].ToString());
            FromDate1 = FromDate;
            Emplist = Session["EmpList"].ToString();

            DataTable dtFilter = new DataTable();

            AttendanceDataSet rptdata = new AttendanceDataSet();

            rptdata.EnforceConstraints = false;
            AttendanceDataSetTableAdapters.sp_Att_AttendanceRegister_ReportTableAdapter adp = new AttendanceDataSetTableAdapters.sp_Att_AttendanceRegister_ReportTableAdapter();

            adp.Fill(rptdata.sp_Att_AttendanceRegister_Report, Convert.ToDateTime(FromDate), Convert.ToDateTime(ToDate));



            if (Emplist != "")
            {
                dtFilter = new DataView(rptdata.sp_Att_AttendanceRegister_Report, "Emp_Id in (" + Emplist.Substring(0, Emplist.Length - 1) + ") ", "", DataViewRowState.CurrentRows).ToTable();
            }

            DataTable dtEmp = dtFilter.DefaultView.ToTable(true,"Emp_Id");
            DataTable dtTemp = new DataTable();

            DataTable dtSalary = new DataTable();
            dtSalary = dtFilter.Clone();
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
            int LatePenaltyDedMin = 0;
            int EarlyPenaltyDedMin = 0;
            int PartialPenaltyDedMin = 0;
            int PartialMin = 0;
            int TotalOTMin = 0;
            double TotalGrossSalary = 0;
            int j = 0;
            for (int i = 0; i < dtEmp.Rows.Count;i++)
            {
                FromDate = FromDate1;
                IsEmpLate = false;
                IsEmpEarly = false;
                IsEmpPartial = false;
                LateMethod = string.Empty;
                EarlyMethod = string.Empty;
                PartialMethod = string.Empty;
                LatePenaltyDedMin = 0;
                EarlyPenaltyDedMin = 0;
                PartialPenaltyDedMin = 0;
                TotalOTMin = 0;
                
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
                TotalGrossSalary = 0;
                try
                {
                    IsEmpEarly = Convert.ToBoolean(objEmpParam.GetEmployeeParameterByParameterName(dtEmp.Rows[i]["Emp_Id"].ToString(), "Field2"));
                    IsEmpLate = Convert.ToBoolean(objEmpParam.GetEmployeeParameterByParameterName(dtEmp.Rows[i]["Emp_Id"].ToString(), "Field1"));
                    IsEmpPartial = Convert.ToBoolean(objEmpParam.GetEmployeeParameterByParameterName(dtEmp.Rows[i]["Emp_Id"].ToString(), "Is_Partial_Enable"));

                    LateMethod = objAppParam.GetApplicationParameterValueByParamName("Is_Late_Penalty_Method", Session["CompId"].ToString());
                    EarlyMethod = objAppParam.GetApplicationParameterValueByParamName("Is_Early_Penalty_Method", Session["CompId"].ToString());
                    PartialMethod = objAppParam.GetApplicationParameterValueByParamName("Partial_Penalty_Method", Session["CompId"].ToString());
                    LatePenaltyDedMin = int.Parse(objAppParam.GetApplicationParameterValueByParamName("Late_Penalty_Min_Deduct", Session["CompId"].ToString()));
                    EarlyPenaltyDedMin = int.Parse(objAppParam.GetApplicationParameterValueByParamName("Early_Penalty_Min_Deduct", Session["CompId"].ToString()));
                    PartialPenaltyDedMin = int.Parse(objAppParam.GetApplicationParameterValueByParamName("Partial_Penalty_Min_Deduct", Session["CompId"].ToString()));
                    Total_Days = ToDate.Subtract(FromDate).Days+1;

                    Days_In_WorkMin = int.Parse(objEmpParam.GetEmployeeParameterByParameterName(dtEmp.Rows[i]["Emp_Id"].ToString(), "Assign_Min"));
                }
                catch
                {

                }

                try
                {
                    Basic_Salary = double.Parse(objEmpParam.GetEmployeeParameterByParameterName(dtEmp.Rows[i]["Emp_Id"].ToString(), "Basic_Salary"));
                }
                catch
                {

                }

                int assingmin = 0;
                try
                {
                    assingmin = int.Parse(objEmpParam.GetEmployeeParameterByParameterName(dtEmp.Rows[i]["Emp_Id"].ToString(), "Assign_Min"));
                }
                catch
                {
                }

                Basic_Min_Salary = Basic_Salary / (Total_Days * assingmin);
                if (Basic_Min_Salary.ToString() == "NaN")
                {
                    Basic_Min_Salary = 0;

                }

                Normal_OT_Salary = GetOTOneMinSalary(dtEmp.Rows[i]["Emp_Id"].ToString(), "Normal", Basic_Min_Salary);
                Week_Off_OT_Salary = GetOTOneMinSalary(dtEmp.Rows[i]["Emp_Id"].ToString(), "WeekOff", Basic_Min_Salary);
                Holiday_OT_Salary = GetOTOneMinSalary(dtEmp.Rows[i]["Emp_Id"].ToString(), "Holiday", Basic_Min_Salary);

                Absent_Penalty = OnDayAbsentSalary(Basic_Min_Salary, dtEmp.Rows[i]["Emp_Id"].ToString());

                Late_Penalty_Min = OnMinuteLatePenalty(Basic_Min_Salary, dtEmp.Rows[i]["Emp_Id"].ToString());

                Early_Penalty_Min = OnMinuteEarlyPenalty(Basic_Min_Salary, dtEmp.Rows[i]["Emp_Id"].ToString());

                Partial_Penalty_Min = OnMinuteParialPenalty(Basic_Min_Salary, dtEmp.Rows[i]["Emp_Id"].ToString());

               
                while (FromDate <= ToDate)
                {
                    PartialMin = 0;
                   
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
                    PartialMin = 0;
                   

                    dtTemp = dtFilter.Clone();
                    dtTemp = new DataView(dtFilter,"Att_Date='"+FromDate.ToString("dd-MMM-yyyy")+"' and Emp_Id='"+dtEmp.Rows[i]["Emp_Id"].ToString()+"'   ","",DataViewRowState.CurrentRows).ToTable();


                    if (dtTemp.Rows.Count > 0)
                    {

                        for (int k = 0; k < dtTemp.Rows.Count;k++ )
                        {
                            dtSalary.ImportRow(dtTemp.Rows[k]);
                           
                            dtSalary.Rows[j]["Field2"] = "";
                            dtSalary.Rows[j]["Field3"] = "";
                            dtSalary.Rows[j]["Field4"] = "";
                            //
                            Late_Min = int.Parse(dtTemp.Rows[k]["LateMin"].ToString());

                            if (IsEmpLate && LateMethod == "Min")
                            {
                                Late_Min = Late_Min * LatePenaltyDedMin;

                            }

                            Early_Min = int.Parse(dtTemp.Rows[k]["EarlyMin"].ToString());

                            if (IsEmpEarly && EarlyMethod == "Min")
                            {
                                Early_Min = Early_Min * EarlyPenaltyDedMin;

                            }
                            
                            PartialMin = int.Parse(dtTemp.Rows[k]["Partial_Violation_Min"].ToString());
                            Total_Worked_Min = int.Parse(dtTemp.Rows[k]["TotalAssign_Min"].ToString());
                            Days_In_WorkMin = int.Parse(dtTemp.Rows[k]["EffectiveWork_Min"].ToString());

                           // Days_In_WorkMin = int.Parse(getWorkMinute(Days_In_WorkMin.ToString(),Total_Worked_Min.ToString()));

                            Normal_OT_Min = int.Parse(dtTemp.Rows[k]["OverTime_Min"].ToString()); 
                            Week_Off_OT_Min = int.Parse(dtTemp.Rows[k]["Week_Off_Min"].ToString());
                            Holiday_OT_Min = int.Parse(dtTemp.Rows[k]["Holiday_Min"].ToString());  

                            Late_Min_Penalty = Late_Min * Late_Penalty_Min;
                            Early_Min_Penalty = Early_Penalty_Min * Early_Min;
                            Parital_Violation_Penalty = Partial_Penalty_Min * PartialMin;
                            Absent_Day_Penalty = Absent_Penalty * Total_Worked_Min;

                            Basic_Work_Salary = Basic_Min_Salary * Days_In_WorkMin;

                            Normal_OT_Work_Salary = Normal_OT_Salary * Normal_OT_Min;
                            WeekOff_OT_Work_Salary = Week_Off_OT_Salary * Week_Off_OT_Min;
                            Holiday_OT_Work_Salary = Holiday_OT_Salary * Holiday_OT_Min;

                            double TotalSalary = 0;

                            TotalOTMin += int.Parse(dtTemp.Rows[k]["OverTime_Min"].ToString());
                            if (Convert.ToBoolean(dtTemp.Rows[k]["Is_Week_Off"].ToString()))
                          {
                              dtSalary.Rows[j]["Field4"] = System.Math.Round(WeekOff_OT_Work_Salary,2).ToString();
                              TotalSalary = Total_Worked_Min * Basic_Min_Salary;
                              dtSalary.Rows[j]["OverTime_Min"] = dtTemp.Rows[k]["Week_Off_Min"];
                              TotalOTMin += int.Parse(dtTemp.Rows[k]["Week_Off_Min"].ToString());
                          }
                            else if (Convert.ToBoolean(dtTemp.Rows[k]["Is_Holiday"].ToString()))
                            {
                                dtSalary.Rows[j]["Field4"] = System.Math.Round(Holiday_OT_Work_Salary,2).ToString();
                                TotalSalary = Total_Worked_Min * Basic_Min_Salary;
                                dtSalary.Rows[j]["OverTime_Min"] = dtTemp.Rows[k]["Holiday_Min"];
                                TotalOTMin += int.Parse(dtTemp.Rows[k]["Holiday_Min"].ToString());
                            }
                            else if (Convert.ToBoolean(dtTemp.Rows[k]["Is_Leave"].ToString()))
                            {
                                TotalSalary = Total_Worked_Min*Basic_Min_Salary;
                                dtSalary.Rows[j]["Field4"] = "0";
                            }
                            else if (Convert.ToBoolean(dtTemp.Rows[k]["Is_Absent"].ToString()))
                            {
                                TotalSalary = Basic_Work_Salary - Absent_Day_Penalty;
                                dtSalary.Rows[j]["Field4"] = "0";
                            }
                            else if (Days_In_WorkMin!=0)
                            {
                                dtSalary.Rows[j]["Field4"] = System.Math.Round(Normal_OT_Work_Salary,2).ToString();
                            }
                            TotalSalary = Basic_Work_Salary - Late_Min_Penalty - Early_Min_Penalty - Parital_Violation_Penalty;
                                
                            dtSalary.Rows[j]["Field3"] = System.Math.Round(TotalSalary,2).ToString();

                            dtSalary.Rows[j]["Field2"] = TotalOTMin.ToString();
                            try
                            {
                                TotalGrossSalary =TotalGrossSalary+ TotalSalary + double.Parse(dtSalary.Rows[j]["Field4"].ToString());
                            }
                            catch
                            {
                                TotalGrossSalary = TotalGrossSalary + TotalSalary;
                            }
                            dtSalary.Rows[j]["Field5"] = System.Math.Round(TotalGrossSalary, 2).ToString();

                            //
                           
                            j++;
                        }

                    }


                    FromDate = FromDate.AddDays(1);
                }
            }

          
            string CompanyName = "";
            string CompanyAddress = "";
            string Imageurl = "";

            DataTable DtCompany = objComp.GetCompanyMasterById(Session["CompId"].ToString());
            DataTable DtAddress = ObjAddress.GetAddressChildDataByAddTypeAndAddRefId("Company", Session["CompId"].ToString());
            if (DtCompany.Rows.Count > 0)
            {
                CompanyName = DtCompany.Rows[0]["Company_Name"].ToString();
                Imageurl = "~/CompanyResource/" + Session["CompId"].ToString() + "/" + DtCompany.Rows[0]["Logo_Path"].ToString();


            }
            if (DtAddress.Rows.Count > 0)
            {
                CompanyAddress = DtAddress.Rows[0]["Address"].ToString();
            }
            RptShift.SetImage(Imageurl);
            RptShift.setTitleName("Daily Salary Report" + " From " + FromDate1.ToString(objSys.SetDateFormat()) + " To " + ToDate.ToString(objSys.SetDateFormat()));
            RptShift.setcompanyname(CompanyName);
            RptShift.setaddress(CompanyAddress);


            RptShift.DataSource = dtSalary;
            RptShift.DataMember = "sp_Att_AttendanceRegister_Report";
            rptViewer.Report = RptShift;
            rptToolBar.ReportViewer = rptViewer;
            


        }

       
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
            else
            {
                TempInt = Convert.ToInt32(effec);
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
    public double OnDayAbsentSalary(double PerMinSal, string EmpId)
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
    public double OnMinuteLatePenalty(double PerMinSal, string EmpId)
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





    public double OnMinuteEarlyPenalty(double PerMinSal, string EmpId)
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




    public double OnMinuteParialPenalty(double PerMinSal, string EmpId)
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

}
