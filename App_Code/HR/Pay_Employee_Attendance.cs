using System;
using System.Data;
using System.Configuration;
using System.Linq;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Xml.Linq;
using PegasusDataAccess;
/// <summary>
/// Summary description for Pay_Employee_Attendance
/// </summary>
public class Pay_Employee_Attendance
{
    DataAccessClass da = new DataAccessClass();
    public Pay_Employee_Attendance()
    {
        //
        // TODO: Add constructor logic here
        //
    }
    public DataTable GetRecord_Emp_Attendance(string strEmp_Id, string strmonth, string stryear)
    {
        DataTable dtInfo1 = new DataTable();
        PassDataToSql[] paramList = new PassDataToSql[5];
        paramList[0] = new PassDataToSql("@Trans_Id", "0", PassDataToSql.ParaTypeList.Int, PassDataToSql.ParaDirectonList.Input);
        paramList[1] = new PassDataToSql("@Emp_Id", strEmp_Id, PassDataToSql.ParaTypeList.Int, PassDataToSql.ParaDirectonList.Input);
        paramList[2] = new PassDataToSql("@month", strmonth, PassDataToSql.ParaTypeList.Int, PassDataToSql.ParaDirectonList.Input);
        paramList[3] = new PassDataToSql("@year", stryear, PassDataToSql.ParaTypeList.Int, PassDataToSql.ParaDirectonList.Input);
        paramList[4] = new PassDataToSql("@optype", "1", PassDataToSql.ParaTypeList.Nvarchar, PassDataToSql.ParaDirectonList.Input);
        dtInfo1 = da.Reuturn_Datatable_Search("sp_Pay_Employee_Attendance_Select_Row", paramList);
        return dtInfo1;
    }
    public int DeletePayEmployeeAttendance(string empid,string month,string year)
    {

        PassDataToSql[] paramList = new PassDataToSql[4];
        paramList[0] = new PassDataToSql("@Emp_Id", empid, PassDataToSql.ParaTypeList.Int, PassDataToSql.ParaDirectonList.Input);
        paramList[1] = new PassDataToSql("@Month", month, PassDataToSql.ParaTypeList.Int, PassDataToSql.ParaDirectonList.Input);
        paramList[2] = new PassDataToSql("@Year", year, PassDataToSql.ParaTypeList.Int, PassDataToSql.ParaDirectonList.Input);
          paramList[3] = new PassDataToSql("@ReferenceID", "0", PassDataToSql.ParaTypeList.Int, PassDataToSql.ParaDirectonList.Output);
          da.execute_Sp("sp_Pay_Employee_Attendance_Delete", paramList);
        return Convert.ToInt32(paramList[3].ParaValue);
    }

    public int InsertPayEmployeeAttendance(string CompanyId, string Emp_Id, string Month, string Year, string Total_Days, string Days_In_WorkMin, string Worked_Days, string Week_Off_Days, string Holiday_Days, string Leave_Days, string Absent_Days, string Assigned_Worked_Min, string Basic_Salary, string Basic_Min_Salary, string Normal_OT_Salary, string Week_Off_OT_Salary, string Holiday_OT_Salary, string Absent_Penalty, string Late_Penalty_Min, string Early_Penalty_Min, string Partial_Penalty_Min, string Total_Worked_Min, string Holiday_OT_Min, string Week_Off_OT_Min, string Normal_OT_Min, string Late_Min, string Early_Min, string Partial_Min, string Basic_Work_Salary, string Normal_OT_Work_Salary, string WeekOff_OT_Work_Salary, string Holiday_OT_Work_Salary, string Week_Off_Days_Salary, string Holiday_Days_Salary, string Leave_Days_Salary, string Absent_Day_Penalty, string Late_Min_Penalty, string Early_Min_Penalty, string Parital_Violation_Penalty, string Posted_Date, string Field1, string Field2, string Field3, string Field4, string Field5, string Field6, string Field7, string Field8, string Field9, string Field10, string IsActive, string CreatedBy, string CreatedDate, string ModifiedBy, string ModifiedDate)
    {

        PassDataToSql[] paramList = new PassDataToSql[56];
        paramList[0] = new PassDataToSql("@Company_Id", CompanyId, PassDataToSql.ParaTypeList.Int, PassDataToSql.ParaDirectonList.Input);
        paramList[1] = new PassDataToSql("@Emp_Id", Emp_Id, PassDataToSql.ParaTypeList.Int, PassDataToSql.ParaDirectonList.Input);
        paramList[2] = new PassDataToSql("@Month", Month, PassDataToSql.ParaTypeList.Int, PassDataToSql.ParaDirectonList.Input);
        paramList[3] = new PassDataToSql("@Year", Year, PassDataToSql.ParaTypeList.Int, PassDataToSql.ParaDirectonList.Input);
        paramList[4] = new PassDataToSql("@Total_Days", Total_Days, PassDataToSql.ParaTypeList.Int, PassDataToSql.ParaDirectonList.Input);
        paramList[5] = new PassDataToSql("@Days_In_WorkMin", Days_In_WorkMin, PassDataToSql.ParaTypeList.Int, PassDataToSql.ParaDirectonList.Input);
        paramList[6] = new PassDataToSql("@Worked_Days", Worked_Days, PassDataToSql.ParaTypeList.Int, PassDataToSql.ParaDirectonList.Input);
        paramList[7] = new PassDataToSql("@Week_Off_Days", Week_Off_Days, PassDataToSql.ParaTypeList.Int, PassDataToSql.ParaDirectonList.Input);
        paramList[8] = new PassDataToSql("@Holiday_Days", Holiday_Days, PassDataToSql.ParaTypeList.Int, PassDataToSql.ParaDirectonList.Input);
        paramList[9] = new PassDataToSql("@Leave_Days", Leave_Days, PassDataToSql.ParaTypeList.Int, PassDataToSql.ParaDirectonList.Input);
        paramList[10] = new PassDataToSql("@Absent_Days", Absent_Days, PassDataToSql.ParaTypeList.Int, PassDataToSql.ParaDirectonList.Input);
        paramList[11] = new PassDataToSql("@Assigned_Worked_Min", Assigned_Worked_Min, PassDataToSql.ParaTypeList.Int, PassDataToSql.ParaDirectonList.Input);
        paramList[12] = new PassDataToSql("@Basic_Salary", Basic_Salary, PassDataToSql.ParaTypeList.Float, PassDataToSql.ParaDirectonList.Input);
        paramList[13] = new PassDataToSql("@Basic_Min_Salary", Basic_Min_Salary, PassDataToSql.ParaTypeList.Float, PassDataToSql.ParaDirectonList.Input);
        paramList[14] = new PassDataToSql("@Normal_OT_Salary", Normal_OT_Salary, PassDataToSql.ParaTypeList.Float, PassDataToSql.ParaDirectonList.Input);
        paramList[15] = new PassDataToSql("@Week_Off_OT_Salary", Week_Off_OT_Salary, PassDataToSql.ParaTypeList.Float, PassDataToSql.ParaDirectonList.Input);
        paramList[16] = new PassDataToSql("@Holiday_OT_Salary", Holiday_OT_Salary, PassDataToSql.ParaTypeList.Float, PassDataToSql.ParaDirectonList.Input);
        paramList[17] = new PassDataToSql("@Absent_Penalty", Absent_Penalty, PassDataToSql.ParaTypeList.Float, PassDataToSql.ParaDirectonList.Input);
        paramList[18] = new PassDataToSql("@Late_Penalty_Min", Late_Penalty_Min, PassDataToSql.ParaTypeList.Float, PassDataToSql.ParaDirectonList.Input);
        paramList[19] = new PassDataToSql("@Early_Penalty_Min", Early_Penalty_Min, PassDataToSql.ParaTypeList.Float, PassDataToSql.ParaDirectonList.Input);
        paramList[20] = new PassDataToSql("@Partial_Penalty_Min", Partial_Penalty_Min, PassDataToSql.ParaTypeList.Float, PassDataToSql.ParaDirectonList.Input);
        paramList[21] = new PassDataToSql("@Total_Worked_Min", Total_Worked_Min, PassDataToSql.ParaTypeList.Int, PassDataToSql.ParaDirectonList.Input);
        paramList[22] = new PassDataToSql("@Holiday_OT_Min", Holiday_OT_Min, PassDataToSql.ParaTypeList.Int, PassDataToSql.ParaDirectonList.Input);
        paramList[23] = new PassDataToSql("@Week_Off_OT_Min", Week_Off_OT_Min, PassDataToSql.ParaTypeList.Int, PassDataToSql.ParaDirectonList.Input);
        paramList[24] = new PassDataToSql("@Normal_OT_Min", Normal_OT_Min, PassDataToSql.ParaTypeList.Int, PassDataToSql.ParaDirectonList.Input);
        paramList[25] = new PassDataToSql("@Late_Min", Late_Min, PassDataToSql.ParaTypeList.Int, PassDataToSql.ParaDirectonList.Input);
        paramList[26] = new PassDataToSql("@Early_Min", Early_Min, PassDataToSql.ParaTypeList.Int, PassDataToSql.ParaDirectonList.Input);
        paramList[27] = new PassDataToSql("@Partial_Min", Partial_Min, PassDataToSql.ParaTypeList.Int, PassDataToSql.ParaDirectonList.Input);
        paramList[28] = new PassDataToSql("@Basic_Work_Salary", Basic_Work_Salary, PassDataToSql.ParaTypeList.Float, PassDataToSql.ParaDirectonList.Input);
        paramList[29] = new PassDataToSql("@Normal_OT_Work_Salary", Normal_OT_Work_Salary, PassDataToSql.ParaTypeList.Float, PassDataToSql.ParaDirectonList.Input);
        paramList[30] = new PassDataToSql("@WeekOff_OT_Work_Salary", WeekOff_OT_Work_Salary, PassDataToSql.ParaTypeList.Float, PassDataToSql.ParaDirectonList.Input);
        paramList[31] = new PassDataToSql("@Week_Off_Days_Salary", Week_Off_Days_Salary, PassDataToSql.ParaTypeList.Float, PassDataToSql.ParaDirectonList.Input);
        paramList[32] = new PassDataToSql("@Holiday_Days_Salary", Holiday_Days_Salary, PassDataToSql.ParaTypeList.Float, PassDataToSql.ParaDirectonList.Input);
        paramList[33] = new PassDataToSql("@Leave_Days_Salary", Leave_Days_Salary, PassDataToSql.ParaTypeList.Float, PassDataToSql.ParaDirectonList.Input);
        paramList[34] = new PassDataToSql("@Absent_Day_Penalty", Absent_Day_Penalty, PassDataToSql.ParaTypeList.Float, PassDataToSql.ParaDirectonList.Input);
        paramList[35] = new PassDataToSql("@Late_Min_Penalty", Late_Min_Penalty, PassDataToSql.ParaTypeList.Float, PassDataToSql.ParaDirectonList.Input);
        paramList[36] = new PassDataToSql("@Early_Min_Penalty", Early_Min_Penalty, PassDataToSql.ParaTypeList.Float, PassDataToSql.ParaDirectonList.Input);
        paramList[37] = new PassDataToSql("@Parital_Violation_Penalty", Parital_Violation_Penalty, PassDataToSql.ParaTypeList.Float, PassDataToSql.ParaDirectonList.Input);
        paramList[38] = new PassDataToSql("@Posted_Date", Posted_Date, PassDataToSql.ParaTypeList.Date, PassDataToSql.ParaDirectonList.Input);

        paramList[39] = new PassDataToSql("@Field1", Field1, PassDataToSql.ParaTypeList.Nvarchar, PassDataToSql.ParaDirectonList.Input);

        paramList[40] = new PassDataToSql("@Field2", Field2, PassDataToSql.ParaTypeList.Nvarchar, PassDataToSql.ParaDirectonList.Input);
        paramList[41] = new PassDataToSql("@Field3", Field3, PassDataToSql.ParaTypeList.Nvarchar, PassDataToSql.ParaDirectonList.Input);
        paramList[42] = new PassDataToSql("@Field4", Field4, PassDataToSql.ParaTypeList.Nvarchar, PassDataToSql.ParaDirectonList.Input);
        paramList[43] = new PassDataToSql("@Field5", Field5, PassDataToSql.ParaTypeList.Nvarchar, PassDataToSql.ParaDirectonList.Input);
        paramList[44] = new PassDataToSql("@Field6", Field6, PassDataToSql.ParaTypeList.Nvarchar, PassDataToSql.ParaDirectonList.Input);
        paramList[45] = new PassDataToSql("@Field7", Field7, PassDataToSql.ParaTypeList.Nvarchar, PassDataToSql.ParaDirectonList.Input);

        paramList[46] = new PassDataToSql("@Field8", Field8, PassDataToSql.ParaTypeList.Nvarchar, PassDataToSql.ParaDirectonList.Input);
        paramList[47] = new PassDataToSql("@Field9", Field9, PassDataToSql.ParaTypeList.Nvarchar, PassDataToSql.ParaDirectonList.Input);
        paramList[48] = new PassDataToSql("@Field10", Field10, PassDataToSql.ParaTypeList.Nvarchar, PassDataToSql.ParaDirectonList.Input);
        paramList[49] = new PassDataToSql("@IsActive", IsActive, PassDataToSql.ParaTypeList.Bit, PassDataToSql.ParaDirectonList.Input);
        paramList[50] = new PassDataToSql("@CreatedBy", CreatedBy, PassDataToSql.ParaTypeList.Nvarchar, PassDataToSql.ParaDirectonList.Input);
        paramList[51] = new PassDataToSql("@CreatedDate", CreatedDate, PassDataToSql.ParaTypeList.Date, PassDataToSql.ParaDirectonList.Input);
        paramList[52] = new PassDataToSql("@ModifiedBy", ModifiedBy, PassDataToSql.ParaTypeList.Nvarchar, PassDataToSql.ParaDirectonList.Input);
        paramList[53] = new PassDataToSql("@ModifiedDate", ModifiedDate, PassDataToSql.ParaTypeList.Date, PassDataToSql.ParaDirectonList.Input);
        paramList[54] = new PassDataToSql("@ReferenceID", "0", PassDataToSql.ParaTypeList.Int, PassDataToSql.ParaDirectonList.Output);
        paramList[55] = new PassDataToSql("@Holiday_OT_Work_Salary", Holiday_OT_Work_Salary, PassDataToSql.ParaTypeList.Float, PassDataToSql.ParaDirectonList.Input);
      
        da.execute_Sp("sp_Pay_Employee_Attendance_Insert", paramList);
        return Convert.ToInt32(paramList[54].ParaValue);
    }

}
