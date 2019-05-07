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
/// Summary description for Att_AttendanceRegister
/// </summary>
public class Att_AttendanceRegister
{
    DataAccessClass daClass = new DataAccessClass();
	public Att_AttendanceRegister()
	{
		//
		// TODO: Add constructor logic here
		//
	}


    public int InsertAttendanceRegister(string CompanyId, string Emp_Id, string Att_Date, string Shift_Id, string Is_TempShift, string TimeTable_Id, string OnDuty_Time, string OffDuty_Time, string In_Time, string Out_Time, string IsLate, string LateMin, string Late_Relaxation_Min, string Late_Penalty_Min, string IsEarlyOut, string EarlyMin, string Early_Relaxation_Min, string Early_Penalty_Min, string Is_Week_Off, string Is_Holiday, string Is_Leave, string Is_Absent, string Week_Off_Min, string Holiday_Min, string OverTime_Min, string Partial_Min, string Partial_Violation_Min, string EffectiveWork_Min, string TotalAssign_Min, string Field1, string Field2, string Field3, string Field4, string Field5, string Field6, string Field7, string IsActive, string CreatedBy, string CreatedDate, string ModifiedBy, string ModifiedDate)
    {


        PassDataToSql[] paramList = new PassDataToSql[42];
        paramList[0] = new PassDataToSql("@Company_Id", CompanyId, PassDataToSql.ParaTypeList.Int, PassDataToSql.ParaDirectonList.Input);

        paramList[1] = new PassDataToSql("@Emp_Id", Emp_Id, PassDataToSql.ParaTypeList.Int, PassDataToSql.ParaDirectonList.Input);
       paramList[2] = new PassDataToSql("@Att_Date", Att_Date, PassDataToSql.ParaTypeList.DateTime, PassDataToSql.ParaDirectonList.Input);
       paramList[3] = new PassDataToSql("@Shift_Id", Shift_Id, PassDataToSql.ParaTypeList.Int, PassDataToSql.ParaDirectonList.Input);
       paramList[4] = new PassDataToSql("@Is_TempShift", Is_TempShift, PassDataToSql.ParaTypeList.Bit, PassDataToSql.ParaDirectonList.Input);
        paramList[5] = new PassDataToSql("@TimeTable_Id", TimeTable_Id, PassDataToSql.ParaTypeList.Int, PassDataToSql.ParaDirectonList.Input);
        paramList[6] = new PassDataToSql("@OnDuty_Time", OnDuty_Time, PassDataToSql.ParaTypeList.DateTime, PassDataToSql.ParaDirectonList.Input);
        paramList[7] = new PassDataToSql("@OffDuty_Time", OffDuty_Time, PassDataToSql.ParaTypeList.DateTime, PassDataToSql.ParaDirectonList.Input);
        paramList[8] = new PassDataToSql("@In_Time", In_Time, PassDataToSql.ParaTypeList.DateTime, PassDataToSql.ParaDirectonList.Input);
        paramList[9] = new PassDataToSql("@Out_Time", Out_Time, PassDataToSql.ParaTypeList.DateTime, PassDataToSql.ParaDirectonList.Input);	
        paramList[10] = new PassDataToSql("@IsLate", IsLate, PassDataToSql.ParaTypeList.Bit, PassDataToSql.ParaDirectonList.Input);
        paramList[11] = new PassDataToSql("@LateMin", LateMin, PassDataToSql.ParaTypeList.Int, PassDataToSql.ParaDirectonList.Input);
        paramList[12] = new PassDataToSql("@Late_Relaxation_Min", Late_Relaxation_Min, PassDataToSql.ParaTypeList.Int, PassDataToSql.ParaDirectonList.Input);
        paramList[13] = new PassDataToSql("@Late_Penalty_Min", Late_Penalty_Min, PassDataToSql.ParaTypeList.Int, PassDataToSql.ParaDirectonList.Input);
        paramList[14] = new PassDataToSql("@IsEarlyOut", IsEarlyOut, PassDataToSql.ParaTypeList.Bit, PassDataToSql.ParaDirectonList.Input);
        paramList[15] = new PassDataToSql("@EarlyMin", EarlyMin, PassDataToSql.ParaTypeList.Int, PassDataToSql.ParaDirectonList.Input);
        paramList[16] = new PassDataToSql("@Early_Relaxation_Min", Early_Relaxation_Min, PassDataToSql.ParaTypeList.Int, PassDataToSql.ParaDirectonList.Input);
        paramList[17] = new PassDataToSql("@Early_Penalty_Min", Early_Penalty_Min, PassDataToSql.ParaTypeList.Int, PassDataToSql.ParaDirectonList.Input);		
        paramList[18] = new PassDataToSql("@Is_Week_Off", Is_Week_Off, PassDataToSql.ParaTypeList.Bit, PassDataToSql.ParaDirectonList.Input);
        paramList[19] = new PassDataToSql("@Is_Holiday", Is_Holiday, PassDataToSql.ParaTypeList.Bit, PassDataToSql.ParaDirectonList.Input);
        paramList[20] = new PassDataToSql("@Is_Leave", Is_Leave, PassDataToSql.ParaTypeList.Bit, PassDataToSql.ParaDirectonList.Input);
        paramList[21] = new PassDataToSql("@Is_Absent", Is_Absent, PassDataToSql.ParaTypeList.Bit, PassDataToSql.ParaDirectonList.Input);
        paramList[22] = new PassDataToSql("@Week_Off_Min", Week_Off_Min, PassDataToSql.ParaTypeList.Int, PassDataToSql.ParaDirectonList.Input);
        paramList[23] = new PassDataToSql("@Holiday_Min", Holiday_Min, PassDataToSql.ParaTypeList.Int, PassDataToSql.ParaDirectonList.Input);
        paramList[24] = new PassDataToSql("@OverTime_Min", OverTime_Min, PassDataToSql.ParaTypeList.Int, PassDataToSql.ParaDirectonList.Input);
        paramList[25] = new PassDataToSql("@Partial_Min", Partial_Min, PassDataToSql.ParaTypeList.Int, PassDataToSql.ParaDirectonList.Input);
        paramList[26] = new PassDataToSql("@Partial_Violation_Min", Partial_Violation_Min, PassDataToSql.ParaTypeList.Int, PassDataToSql.ParaDirectonList.Input);
        paramList[27] = new PassDataToSql("@EffectiveWork_Min", EffectiveWork_Min, PassDataToSql.ParaTypeList.Int, PassDataToSql.ParaDirectonList.Input);
        paramList[28] = new PassDataToSql("@TotalAssign_Min", TotalAssign_Min, PassDataToSql.ParaTypeList.Int, PassDataToSql.ParaDirectonList.Input);	

       paramList[29] = new PassDataToSql("@Field1", Field1, PassDataToSql.ParaTypeList.Nvarchar, PassDataToSql.ParaDirectonList.Input);

        paramList[30] = new PassDataToSql("@Field2", Field2, PassDataToSql.ParaTypeList.Nvarchar, PassDataToSql.ParaDirectonList.Input);
        paramList[31] = new PassDataToSql("@Field3", Field3, PassDataToSql.ParaTypeList.Nvarchar, PassDataToSql.ParaDirectonList.Input);
        paramList[32] = new PassDataToSql("@Field4", Field4, PassDataToSql.ParaTypeList.Nvarchar, PassDataToSql.ParaDirectonList.Input);
        paramList[33] = new PassDataToSql("@Field5", Field5, PassDataToSql.ParaTypeList.Nvarchar, PassDataToSql.ParaDirectonList.Input);
        paramList[34] = new PassDataToSql("@Field6", Field6, PassDataToSql.ParaTypeList.Bit, PassDataToSql.ParaDirectonList.Input);
        paramList[35] = new PassDataToSql("@Field7", Field7, PassDataToSql.ParaTypeList.Date, PassDataToSql.ParaDirectonList.Input);


        paramList[36] = new PassDataToSql("@IsActive", IsActive, PassDataToSql.ParaTypeList.Bit, PassDataToSql.ParaDirectonList.Input);
        paramList[37] = new PassDataToSql("@CreatedBy", CreatedBy, PassDataToSql.ParaTypeList.Nvarchar, PassDataToSql.ParaDirectonList.Input);
        paramList[38] = new PassDataToSql("@CreatedDate", CreatedDate, PassDataToSql.ParaTypeList.Date, PassDataToSql.ParaDirectonList.Input);
        paramList[39] = new PassDataToSql("@ModifiedBy", ModifiedBy, PassDataToSql.ParaTypeList.Nvarchar, PassDataToSql.ParaDirectonList.Input);
        paramList[40] = new PassDataToSql("@ModifiedDate", ModifiedDate, PassDataToSql.ParaTypeList.Date, PassDataToSql.ParaDirectonList.Input);
        paramList[41] = new PassDataToSql("@ReferenceID", "0", PassDataToSql.ParaTypeList.Int, PassDataToSql.ParaDirectonList.Output);

        daClass.execute_Sp("sp_Att_AttendanceRegister_Insert", paramList);
        return Convert.ToInt32(paramList[41].ParaValue);
    }


    public int DeleteAttendanceRegister(string empid,string FromDate,string ToDate)
    {

        PassDataToSql[] paramList = new PassDataToSql[4];
        paramList[0] = new PassDataToSql("@Emp_Id", empid, PassDataToSql.ParaTypeList.Int, PassDataToSql.ParaDirectonList.Input);
        paramList[1] = new PassDataToSql("@From_Date", FromDate, PassDataToSql.ParaTypeList.Date, PassDataToSql.ParaDirectonList.Input);
        paramList[2] = new PassDataToSql("@To_Date", ToDate, PassDataToSql.ParaTypeList.Date, PassDataToSql.ParaDirectonList.Input);
       
        paramList[3] = new PassDataToSql("@ReferenceID", "0", PassDataToSql.ParaTypeList.Int, PassDataToSql.ParaDirectonList.Output);
        daClass.execute_Sp("sp_Att_AttendanceRegister_Delete", paramList);
        return Convert.ToInt32(paramList[3].ParaValue);
    }

    public DataTable GetAttendanceRegDataByMonth_Year_EmpId(string empid,string month,string year)
    {

        DataTable dtInfo = new DataTable();
        PassDataToSql[] paramList = new PassDataToSql[3];
        paramList[0] = new PassDataToSql("@Emp_Id", empid, PassDataToSql.ParaTypeList.Int, PassDataToSql.ParaDirectonList.Input);
        paramList[1] = new PassDataToSql("@Month", month, PassDataToSql.ParaTypeList.Int, PassDataToSql.ParaDirectonList.Input);
        paramList[2] = new PassDataToSql("@Year", year, PassDataToSql.ParaTypeList.Int, PassDataToSql.ParaDirectonList.Input);

        dtInfo = daClass.Reuturn_Datatable_Search("sp_Att_AttendanceRegister_SelectRowBy_Month_Year_Emp_Id", paramList);

        return dtInfo;
    }


    public DataTable GetAttendanceRegDataByEmpId(string empid, string fromdate, string todate)
    {
        if(empid=="")
        {
            empid = "0";

        }
        DataTable dtInfo = new DataTable();
        PassDataToSql[] paramList = new PassDataToSql[3];
        paramList[0] = new PassDataToSql("@Emp_Id", empid, PassDataToSql.ParaTypeList.Int, PassDataToSql.ParaDirectonList.Input);
        paramList[1] = new PassDataToSql("@FromDate", fromdate, PassDataToSql.ParaTypeList.Date, PassDataToSql.ParaDirectonList.Input);
        paramList[2] = new PassDataToSql("@ToDate", todate, PassDataToSql.ParaTypeList.Date, PassDataToSql.ParaDirectonList.Input);

        dtInfo = daClass.Reuturn_Datatable_Search("sp_Att_AttendanceRegister_SelectByDate", paramList);

        return dtInfo;
    }


   
}
