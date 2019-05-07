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
/// Summary description for Att_AttendanceLog
/// </summary>
public class Att_AttendanceLog
{
    DataAccessClass daClass = new DataAccessClass();
	public Att_AttendanceLog()
	{
		//
		// TODO: Add constructor logic here
		//
	}

    public int InsertAttendanceLog(string CompanyId, string Emp_Id,string Device_Id,string Event_Date,string Event_Time,string Func_Code,string Type1,string Verified_Type, string IsActive, string CreatedBy, string CreatedDate, string ModifiedBy, string ModifiedDate)
    {
        PassDataToSql[] paramList = new PassDataToSql[14];
        paramList[0] = new PassDataToSql("@Company_Id", CompanyId, PassDataToSql.ParaTypeList.Int, PassDataToSql.ParaDirectonList.Input);

        paramList[1] = new PassDataToSql("@Emp_Id", Emp_Id, PassDataToSql.ParaTypeList.Int, PassDataToSql.ParaDirectonList.Input);
        paramList[2] = new PassDataToSql("@Device_Id", Device_Id, PassDataToSql.ParaTypeList.Nvarchar, PassDataToSql.ParaDirectonList.Input);
        paramList[3] = new PassDataToSql("@Event_Date", Event_Date, PassDataToSql.ParaTypeList.Date, PassDataToSql.ParaDirectonList.Input);
        paramList[4] = new PassDataToSql("@Event_Time", Event_Time, PassDataToSql.ParaTypeList.DateTime, PassDataToSql.ParaDirectonList.Input);
        paramList[5] = new PassDataToSql("@Func_Code", Func_Code, PassDataToSql.ParaTypeList.Nvarchar, PassDataToSql.ParaDirectonList.Input);
        paramList[6] = new PassDataToSql("@Type", Type1, PassDataToSql.ParaTypeList.Nvarchar, PassDataToSql.ParaDirectonList.Input);
        paramList[7] = new PassDataToSql("@Verified_Type", Verified_Type, PassDataToSql.ParaTypeList.Nvarchar, PassDataToSql.ParaDirectonList.Input);

       
       

        paramList[8] = new PassDataToSql("@IsActive", IsActive, PassDataToSql.ParaTypeList.Bit, PassDataToSql.ParaDirectonList.Input);
        paramList[9] = new PassDataToSql("@CreatedBy", CreatedBy, PassDataToSql.ParaTypeList.Nvarchar, PassDataToSql.ParaDirectonList.Input);
        paramList[10] = new PassDataToSql("@CreatedDate", CreatedDate, PassDataToSql.ParaTypeList.Date, PassDataToSql.ParaDirectonList.Input);
        paramList[11] = new PassDataToSql("@ModifiedBy", ModifiedBy, PassDataToSql.ParaTypeList.Nvarchar, PassDataToSql.ParaDirectonList.Input);
        paramList[12] = new PassDataToSql("@ModifiedDate", ModifiedDate, PassDataToSql.ParaTypeList.Date, PassDataToSql.ParaDirectonList.Input);
        paramList[13] = new PassDataToSql("@ReferenceID", "0", PassDataToSql.ParaTypeList.Int, PassDataToSql.ParaDirectonList.Output);

        daClass.execute_Sp("sp_Att_AttendanceLog_Insert", paramList);
        return Convert.ToInt32(paramList[13].ParaValue);
    }



    public DataTable GetAttendanceLog(string CompanyId)
    {

        DataTable dtInfo = new DataTable();
        PassDataToSql[] paramList = new PassDataToSql[4];
        paramList[0] = new PassDataToSql("@Company_Id", CompanyId, PassDataToSql.ParaTypeList.Int, PassDataToSql.ParaDirectonList.Input);
        paramList[1] = new PassDataToSql("@From_Date", DateTime.Now.ToString(), PassDataToSql.ParaTypeList.Date, PassDataToSql.ParaDirectonList.Input);
        paramList[2] = new PassDataToSql("@To_Date", DateTime.Now.ToString(), PassDataToSql.ParaTypeList.Date, PassDataToSql.ParaDirectonList.Input);
        paramList[3] = new PassDataToSql("@optype", "1", PassDataToSql.ParaTypeList.Int, PassDataToSql.ParaDirectonList.Input);
        dtInfo = daClass.Reuturn_Datatable_Search("sp_Att_AttendanceLog_SelectRow", paramList);

        return dtInfo;
    }
    public DataTable GetAttendanceLogByMonth_Year_EmpId(string empid, string month, string year)
    {

        DataTable dtInfo = new DataTable();
        PassDataToSql[] paramList = new PassDataToSql[3];
        paramList[0] = new PassDataToSql("@Emp_Id", empid, PassDataToSql.ParaTypeList.Int, PassDataToSql.ParaDirectonList.Input);
        paramList[1] = new PassDataToSql("@Month", month, PassDataToSql.ParaTypeList.Int, PassDataToSql.ParaDirectonList.Input);
        paramList[2] = new PassDataToSql("@Year", year, PassDataToSql.ParaTypeList.Int, PassDataToSql.ParaDirectonList.Input);

        dtInfo = daClass.Reuturn_Datatable_Search("sp_Att_AttendanceLog_SelectRowBy_Month_Year_Emp_Id", paramList);

        return dtInfo;
    }

    public DataTable GetAttendanceLogByDate(string CompanyId,string fromdate,string todate)
    {

        DataTable dtInfo = new DataTable();
        PassDataToSql[] paramList = new PassDataToSql[4];
        paramList[0] = new PassDataToSql("@Company_Id", CompanyId, PassDataToSql.ParaTypeList.Int, PassDataToSql.ParaDirectonList.Input);
        paramList[1] = new PassDataToSql("@From_Date", fromdate, PassDataToSql.ParaTypeList.Date, PassDataToSql.ParaDirectonList.Input);
        paramList[2] = new PassDataToSql("@To_Date", todate, PassDataToSql.ParaTypeList.Date, PassDataToSql.ParaDirectonList.Input);
        paramList[3] = new PassDataToSql("@optype", "2", PassDataToSql.ParaTypeList.Int, PassDataToSql.ParaDirectonList.Input);
        dtInfo = daClass.Reuturn_Datatable_Search("sp_Att_AttendanceLog_SelectRow", paramList);

        return dtInfo;
    }
    public DataTable GetAttendanceLogByDateByEmpId(string EmpId, string fromdate, string todate)
    {

        DataTable dtInfo = new DataTable();
        PassDataToSql[] paramList = new PassDataToSql[4];
        paramList[0] = new PassDataToSql("@EmpId", EmpId, PassDataToSql.ParaTypeList.Int, PassDataToSql.ParaDirectonList.Input);
        paramList[1] = new PassDataToSql("@From_Date", fromdate, PassDataToSql.ParaTypeList.Date, PassDataToSql.ParaDirectonList.Input);
        paramList[2] = new PassDataToSql("@To_Date", todate, PassDataToSql.ParaTypeList.Date, PassDataToSql.ParaDirectonList.Input);
        paramList[3] = new PassDataToSql("@optype", "2", PassDataToSql.ParaTypeList.Int, PassDataToSql.ParaDirectonList.Input);
        dtInfo = daClass.Reuturn_Datatable_Search("sp_Att_AttendanceLog_SelectRow_EmpId", paramList);

        return dtInfo;
    }
    public int DeleteAttendanceLog(string TransId)
    {

        PassDataToSql[] paramList = new PassDataToSql[2];
        paramList[0] = new PassDataToSql("@Trans_Id", TransId, PassDataToSql.ParaTypeList.Int, PassDataToSql.ParaDirectonList.Input);
         paramList[1] = new PassDataToSql("@ReferenceID", "0", PassDataToSql.ParaTypeList.Int, PassDataToSql.ParaDirectonList.Output);
         daClass.execute_Sp("sp_Att_AttendanceLog_Delete", paramList);
        return Convert.ToInt32(paramList[1].ParaValue);
    }
}
