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
/// Summary description for Att_Employee_Notification
/// </summary>
public class Att_Employee_Notification
{
    DataAccessClass daClass = new DataAccessClass();
	public Att_Employee_Notification()
	{
		//
		// TODO: Add constructor logic here
		//
	}

    public void DeleteEmployeeNotificationByEmpId(string empid)
    {


        PassDataToSql[] paramList = new PassDataToSql[1];
        paramList[0] = new PassDataToSql("@Emp_Id", empid, PassDataToSql.ParaTypeList.Int, PassDataToSql.ParaDirectonList.Input);

         daClass.execute_Sp("sp_Set_Att_Employee_Notification_Delete", paramList);
        


    }
    public int InsertEmployeeNotification(string CompanyId, string Emp_Id, string Is_SMS_Document_Expired, string Is_SMS_Absent, string Is_SMS_Late, string Is_SMS_Early, string Is_SMS_LeaveApproved, string Is_SMS_Partial_Fault, string Is_SMS_NoClock, string Is_SMS1, string Is_SMS2, string Is_SMS3, string Is_SMS4, string Is_SMS5, string Is_Rpt_Absent, string Is_Rpt_Late, string Is_Rpt_Early, string Is_Rpt_Attendance, string Is_Rpt_Salary, string Is_Rpt_OverTime, string Is_Rpt_Log, string Is_Rpt_DocumentExp, string Is_Rpt_Violation, string Is_Report1, string Is_Report2, string Is_Report3, string Is_Report4, string Is_Report5, string Is_Report6, string Is_Report7, string Is_Report8, string Is_Report9, string Is_Report10, string Field1, string Field2, string Field3, string Field4, string Field5, string Field6, string Field7, string IsActive, string CreatedBy, string CreatedDate,string ModifiedBy, string ModifiedDate)	
    {
        PassDataToSql[] paramList = new PassDataToSql[46];

        paramList[0] = new PassDataToSql("@Company_Id", CompanyId, PassDataToSql.ParaTypeList.Int, PassDataToSql.ParaDirectonList.Input);

        paramList[1] = new PassDataToSql("@Emp_Id", Emp_Id, PassDataToSql.ParaTypeList.Int, PassDataToSql.ParaDirectonList.Input);

        paramList[2] = new PassDataToSql("@Is_SMS_Document_Expired", Is_SMS_Document_Expired, PassDataToSql.ParaTypeList.Bit, PassDataToSql.ParaDirectonList.Input);
        paramList[3] = new PassDataToSql("@Is_SMS_Absent", Is_SMS_Absent, PassDataToSql.ParaTypeList.Bit, PassDataToSql.ParaDirectonList.Input);
        paramList[4] = new PassDataToSql("@Is_SMS_Late", Is_SMS_Late, PassDataToSql.ParaTypeList.Bit, PassDataToSql.ParaDirectonList.Input);
        paramList[5] = new PassDataToSql("@Is_SMS_Early", Is_SMS_Early, PassDataToSql.ParaTypeList.Bit, PassDataToSql.ParaDirectonList.Input);
        paramList[6] = new PassDataToSql("@Is_SMS_LeaveApproved", Is_SMS_LeaveApproved, PassDataToSql.ParaTypeList.Bit, PassDataToSql.ParaDirectonList.Input);
        paramList[7] = new PassDataToSql("@Is_SMS_Partial_Fault", Is_SMS_Partial_Fault, PassDataToSql.ParaTypeList.Bit, PassDataToSql.ParaDirectonList.Input);
        paramList[8] = new PassDataToSql("@Is_SMS_NoClock", Is_SMS_NoClock, PassDataToSql.ParaTypeList.Bit, PassDataToSql.ParaDirectonList.Input);
        paramList[9] = new PassDataToSql("@Is_SMS1", Is_SMS1, PassDataToSql.ParaTypeList.Bit, PassDataToSql.ParaDirectonList.Input);
        paramList[10] = new PassDataToSql("@Is_SMS2", Is_SMS2, PassDataToSql.ParaTypeList.Bit, PassDataToSql.ParaDirectonList.Input);
        paramList[11] = new PassDataToSql("@Is_SMS3", Is_SMS3, PassDataToSql.ParaTypeList.Bit, PassDataToSql.ParaDirectonList.Input);
        paramList[12] = new PassDataToSql("@Is_SMS4", Is_SMS4, PassDataToSql.ParaTypeList.Bit, PassDataToSql.ParaDirectonList.Input);
        paramList[13] = new PassDataToSql("@Is_SMS5", Is_SMS5, PassDataToSql.ParaTypeList.Bit, PassDataToSql.ParaDirectonList.Input);
        paramList[14] = new PassDataToSql("@Is_Rpt_Absent", Is_Rpt_Absent, PassDataToSql.ParaTypeList.Bit, PassDataToSql.ParaDirectonList.Input);
        paramList[15] = new PassDataToSql("@Is_Rpt_Late", Is_Rpt_Late, PassDataToSql.ParaTypeList.Bit, PassDataToSql.ParaDirectonList.Input);
        paramList[16] = new PassDataToSql("@Is_Rpt_Early", Is_Rpt_Early, PassDataToSql.ParaTypeList.Bit, PassDataToSql.ParaDirectonList.Input);
        paramList[17] = new PassDataToSql("@Is_Rpt_Attendance", Is_Rpt_Attendance, PassDataToSql.ParaTypeList.Bit, PassDataToSql.ParaDirectonList.Input);
        paramList[18] = new PassDataToSql("@Is_Rpt_Salary", Is_Rpt_Salary, PassDataToSql.ParaTypeList.Bit, PassDataToSql.ParaDirectonList.Input);
        paramList[19] = new PassDataToSql("@Is_Rpt_OverTime", Is_Rpt_OverTime, PassDataToSql.ParaTypeList.Bit, PassDataToSql.ParaDirectonList.Input);
        paramList[20] = new PassDataToSql("@Is_Rpt_Log", Is_Rpt_Log, PassDataToSql.ParaTypeList.Bit, PassDataToSql.ParaDirectonList.Input);
        paramList[21] = new PassDataToSql("@Is_Rpt_DocumentExp", Is_Rpt_DocumentExp, PassDataToSql.ParaTypeList.Bit, PassDataToSql.ParaDirectonList.Input);
        paramList[22] = new PassDataToSql("@Is_Rpt_Violation", Is_Rpt_Violation, PassDataToSql.ParaTypeList.Bit, PassDataToSql.ParaDirectonList.Input);
        paramList[23] = new PassDataToSql("@Is_Report1", Is_Report1, PassDataToSql.ParaTypeList.Bit, PassDataToSql.ParaDirectonList.Input);
        paramList[24] = new PassDataToSql("@Is_Report2", Is_Report2, PassDataToSql.ParaTypeList.Bit, PassDataToSql.ParaDirectonList.Input);
        paramList[25] = new PassDataToSql("@Is_Report3", Is_Report3, PassDataToSql.ParaTypeList.Bit, PassDataToSql.ParaDirectonList.Input);
        paramList[26] = new PassDataToSql("@Is_Report4", Is_Report4, PassDataToSql.ParaTypeList.Bit, PassDataToSql.ParaDirectonList.Input);
        paramList[27] = new PassDataToSql("@Is_Report5", Is_Report5, PassDataToSql.ParaTypeList.Bit, PassDataToSql.ParaDirectonList.Input);
        paramList[28] = new PassDataToSql("@Is_Report6", Is_Report6, PassDataToSql.ParaTypeList.Bit, PassDataToSql.ParaDirectonList.Input);
        paramList[29] = new PassDataToSql("@Is_Report7", Is_Report7, PassDataToSql.ParaTypeList.Bit, PassDataToSql.ParaDirectonList.Input);
        paramList[30] = new PassDataToSql("@Is_Report8", Is_Report8, PassDataToSql.ParaTypeList.Bit, PassDataToSql.ParaDirectonList.Input);
        paramList[31] = new PassDataToSql("@Is_Report9", Is_Report9, PassDataToSql.ParaTypeList.Bit, PassDataToSql.ParaDirectonList.Input);
        paramList[32] = new PassDataToSql("@Is_Report10", Is_Report10, PassDataToSql.ParaTypeList.Bit, PassDataToSql.ParaDirectonList.Input);
      
        paramList[33] = new PassDataToSql("@Field1", Field1, PassDataToSql.ParaTypeList.Nvarchar, PassDataToSql.ParaDirectonList.Input);

        paramList[34] = new PassDataToSql("@Field2", Field2, PassDataToSql.ParaTypeList.Nvarchar, PassDataToSql.ParaDirectonList.Input);
        paramList[35] = new PassDataToSql("@Field3", Field3, PassDataToSql.ParaTypeList.Nvarchar, PassDataToSql.ParaDirectonList.Input);
        paramList[36] = new PassDataToSql("@Field4", Field4, PassDataToSql.ParaTypeList.Nvarchar, PassDataToSql.ParaDirectonList.Input);
        paramList[37] = new PassDataToSql("@Field5", Field5, PassDataToSql.ParaTypeList.Nvarchar, PassDataToSql.ParaDirectonList.Input);
        paramList[38] = new PassDataToSql("@Field6", Field6, PassDataToSql.ParaTypeList.Bit, PassDataToSql.ParaDirectonList.Input);
        paramList[39] = new PassDataToSql("@Field7", Field7, PassDataToSql.ParaTypeList.Date, PassDataToSql.ParaDirectonList.Input);


        paramList[40] = new PassDataToSql("@IsActive", IsActive, PassDataToSql.ParaTypeList.Bit, PassDataToSql.ParaDirectonList.Input);
        paramList[41] = new PassDataToSql("@CreatedBy", CreatedBy, PassDataToSql.ParaTypeList.Nvarchar, PassDataToSql.ParaDirectonList.Input);
        paramList[42] = new PassDataToSql("@CreatedDate", CreatedDate, PassDataToSql.ParaTypeList.Date, PassDataToSql.ParaDirectonList.Input);
        paramList[43] = new PassDataToSql("@ModifiedBy", ModifiedBy, PassDataToSql.ParaTypeList.Nvarchar, PassDataToSql.ParaDirectonList.Input);
        paramList[44] = new PassDataToSql("@ModifiedDate", ModifiedDate, PassDataToSql.ParaTypeList.Date, PassDataToSql.ParaDirectonList.Input);
        paramList[45] = new PassDataToSql("@ReferenceID", "0", PassDataToSql.ParaTypeList.Int, PassDataToSql.ParaDirectonList.Output);

        daClass.execute_Sp("sp_Set_Att_Employee_Notification_Insert", paramList);
        return Convert.ToInt32(paramList[45].ParaValue);
    }
    public int UpdateEmployeeNotificationEmpId(string CompanyId, string Emp_Id, string Is_SMS_Document_Expired, string Is_SMS_Absent, string Is_SMS_Late, string Is_SMS_Early, string Is_SMS_LeaveApproved, string Is_SMS_Partial_Fault, string Is_SMS_NoClock, string Is_SMS1, string Is_SMS2, string Is_SMS3, string Is_SMS4, string Is_SMS5, string Is_Rpt_Absent, string Is_Rpt_Late, string Is_Rpt_Early, string Is_Rpt_Attendance, string Is_Rpt_Salary, string Is_Rpt_OverTime, string Is_Rpt_Log, string Is_Rpt_DocumentExp, string Is_Rpt_Violation, string Is_Report1, string Is_Report2, string Is_Report3, string Is_Report4, string Is_Report5, string Is_Report6, string Is_Report7, string Is_Report8, string Is_Report9, string Is_Report10, string Field1, string Field2, string Field3, string Field4, string Field5, string Field6, string Field7, string IsActive, string ModifiedBy, string ModifiedDate)	
    {
        PassDataToSql[] paramList = new PassDataToSql[44];
        paramList[0] = new PassDataToSql("@Company_Id", CompanyId, PassDataToSql.ParaTypeList.Int, PassDataToSql.ParaDirectonList.Input);

        paramList[1] = new PassDataToSql("@Emp_Id", Emp_Id, PassDataToSql.ParaTypeList.Int, PassDataToSql.ParaDirectonList.Input);

        paramList[2] = new PassDataToSql("@Is_SMS_Document_Expired", Is_SMS_Document_Expired, PassDataToSql.ParaTypeList.Bit, PassDataToSql.ParaDirectonList.Input);
        paramList[3] = new PassDataToSql("@Is_SMS_Absent", Is_SMS_Absent, PassDataToSql.ParaTypeList.Bit, PassDataToSql.ParaDirectonList.Input);
        paramList[4] = new PassDataToSql("@Is_SMS_Late", Is_SMS_Late, PassDataToSql.ParaTypeList.Bit, PassDataToSql.ParaDirectonList.Input);
        paramList[5] = new PassDataToSql("@Is_SMS_Early", Is_SMS_Early, PassDataToSql.ParaTypeList.Bit, PassDataToSql.ParaDirectonList.Input);
        paramList[6] = new PassDataToSql("@Is_SMS_LeaveApproved", Is_SMS_LeaveApproved, PassDataToSql.ParaTypeList.Bit, PassDataToSql.ParaDirectonList.Input);
        paramList[7] = new PassDataToSql("@Is_SMS_Partial_Fault", Is_SMS_Partial_Fault, PassDataToSql.ParaTypeList.Bit, PassDataToSql.ParaDirectonList.Input);
        paramList[8] = new PassDataToSql("@Is_SMS_NoClock", Is_SMS_NoClock, PassDataToSql.ParaTypeList.Bit, PassDataToSql.ParaDirectonList.Input);
        paramList[9] = new PassDataToSql("@Is_SMS1", Is_SMS1, PassDataToSql.ParaTypeList.Bit, PassDataToSql.ParaDirectonList.Input);
        paramList[10] = new PassDataToSql("@Is_SMS2", Is_SMS2, PassDataToSql.ParaTypeList.Bit, PassDataToSql.ParaDirectonList.Input);
        paramList[11] = new PassDataToSql("@Is_SMS3", Is_SMS3, PassDataToSql.ParaTypeList.Bit, PassDataToSql.ParaDirectonList.Input);
        paramList[12] = new PassDataToSql("@Is_SMS4", Is_SMS4, PassDataToSql.ParaTypeList.Bit, PassDataToSql.ParaDirectonList.Input);
        paramList[13] = new PassDataToSql("@Is_SMS5", Is_SMS5, PassDataToSql.ParaTypeList.Bit, PassDataToSql.ParaDirectonList.Input);
        paramList[14] = new PassDataToSql("@Is_Rpt_Absent", Is_Rpt_Absent, PassDataToSql.ParaTypeList.Bit, PassDataToSql.ParaDirectonList.Input);
        paramList[15] = new PassDataToSql("@Is_Rpt_Late", Is_Rpt_Late, PassDataToSql.ParaTypeList.Bit, PassDataToSql.ParaDirectonList.Input);
        paramList[16] = new PassDataToSql("@Is_Rpt_Early", Is_Rpt_Early, PassDataToSql.ParaTypeList.Bit, PassDataToSql.ParaDirectonList.Input);
        paramList[17] = new PassDataToSql("@Is_Rpt_Attendance", Is_Rpt_Attendance, PassDataToSql.ParaTypeList.Bit, PassDataToSql.ParaDirectonList.Input);
        paramList[18] = new PassDataToSql("@Is_Rpt_Salary", Is_Rpt_Salary, PassDataToSql.ParaTypeList.Bit, PassDataToSql.ParaDirectonList.Input);
        paramList[19] = new PassDataToSql("@Is_Rpt_OverTime", Is_Rpt_OverTime, PassDataToSql.ParaTypeList.Bit, PassDataToSql.ParaDirectonList.Input);
        paramList[20] = new PassDataToSql("@Is_Rpt_Log", Is_Rpt_Log, PassDataToSql.ParaTypeList.Bit, PassDataToSql.ParaDirectonList.Input);
        paramList[21] = new PassDataToSql("@Is_Rpt_DocumentExp", Is_Rpt_DocumentExp, PassDataToSql.ParaTypeList.Bit, PassDataToSql.ParaDirectonList.Input);
        paramList[22] = new PassDataToSql("@Is_Rpt_Violation", Is_Rpt_Violation, PassDataToSql.ParaTypeList.Bit, PassDataToSql.ParaDirectonList.Input);
        paramList[23] = new PassDataToSql("@Is_Report1", Is_Report1, PassDataToSql.ParaTypeList.Bit, PassDataToSql.ParaDirectonList.Input);
        paramList[24] = new PassDataToSql("@Is_Report2", Is_Report2, PassDataToSql.ParaTypeList.Bit, PassDataToSql.ParaDirectonList.Input);
        paramList[25] = new PassDataToSql("@Is_Report3", Is_Report3, PassDataToSql.ParaTypeList.Bit, PassDataToSql.ParaDirectonList.Input);
        paramList[26] = new PassDataToSql("@Is_Report4", Is_Report4, PassDataToSql.ParaTypeList.Bit, PassDataToSql.ParaDirectonList.Input);
        paramList[27] = new PassDataToSql("@Is_Report5", Is_Report5, PassDataToSql.ParaTypeList.Bit, PassDataToSql.ParaDirectonList.Input);
        paramList[28] = new PassDataToSql("@Is_Report6", Is_Report6, PassDataToSql.ParaTypeList.Bit, PassDataToSql.ParaDirectonList.Input);
        paramList[29] = new PassDataToSql("@Is_Report7", Is_Report7, PassDataToSql.ParaTypeList.Bit, PassDataToSql.ParaDirectonList.Input);
        paramList[30] = new PassDataToSql("@Is_Report8", Is_Report8, PassDataToSql.ParaTypeList.Bit, PassDataToSql.ParaDirectonList.Input);
        paramList[31] = new PassDataToSql("@Is_Report9", Is_Report9, PassDataToSql.ParaTypeList.Bit, PassDataToSql.ParaDirectonList.Input);
        paramList[32] = new PassDataToSql("@Is_Report10", Is_Report10, PassDataToSql.ParaTypeList.Bit, PassDataToSql.ParaDirectonList.Input);
      
        paramList[33] = new PassDataToSql("@Field1", Field1, PassDataToSql.ParaTypeList.Nvarchar, PassDataToSql.ParaDirectonList.Input);

        paramList[34] = new PassDataToSql("@Field2", Field2, PassDataToSql.ParaTypeList.Nvarchar, PassDataToSql.ParaDirectonList.Input);
        paramList[35] = new PassDataToSql("@Field3", Field3, PassDataToSql.ParaTypeList.Nvarchar, PassDataToSql.ParaDirectonList.Input);
        paramList[36] = new PassDataToSql("@Field4", Field4, PassDataToSql.ParaTypeList.Nvarchar, PassDataToSql.ParaDirectonList.Input);
        paramList[37] = new PassDataToSql("@Field5", Field5, PassDataToSql.ParaTypeList.Nvarchar, PassDataToSql.ParaDirectonList.Input);
        paramList[38] = new PassDataToSql("@Field6", Field6, PassDataToSql.ParaTypeList.Bit, PassDataToSql.ParaDirectonList.Input);
        paramList[39] = new PassDataToSql("@Field7", Field7, PassDataToSql.ParaTypeList.Date, PassDataToSql.ParaDirectonList.Input);


        paramList[40] = new PassDataToSql("@IsActive", IsActive, PassDataToSql.ParaTypeList.Bit, PassDataToSql.ParaDirectonList.Input);
        paramList[41] = new PassDataToSql("@ModifiedBy", ModifiedBy, PassDataToSql.ParaTypeList.Nvarchar, PassDataToSql.ParaDirectonList.Input);
        paramList[42] = new PassDataToSql("@ModifiedDate", ModifiedDate, PassDataToSql.ParaTypeList.Date, PassDataToSql.ParaDirectonList.Input);
        paramList[43] = new PassDataToSql("@ReferenceID", "0", PassDataToSql.ParaTypeList.Int, PassDataToSql.ParaDirectonList.Output);
       
        daClass.execute_Sp("sp_Set_Att_Employee_Notification_UpdateByEmpId", paramList);
        return Convert.ToInt32(paramList[43].ParaValue);
    }
    public DataTable GetEmployeeNotificationByEmpId(string CompanyId, string EmpId)
    {

        DataTable dtInfo = new DataTable();
        PassDataToSql[] paramList = new PassDataToSql[4];
        paramList[0] = new PassDataToSql("@Company_Id", CompanyId, PassDataToSql.ParaTypeList.Int, PassDataToSql.ParaDirectonList.Input);
        paramList[1] = new PassDataToSql("@Emp_Id", EmpId, PassDataToSql.ParaTypeList.Nvarchar, PassDataToSql.ParaDirectonList.Input);
        paramList[2] = new PassDataToSql("@Trans_Id", "0", PassDataToSql.ParaTypeList.Nvarchar, PassDataToSql.ParaDirectonList.Input);
        paramList[3] = new PassDataToSql("@optype", "4", PassDataToSql.ParaTypeList.Nvarchar, PassDataToSql.ParaDirectonList.Input);
        dtInfo = daClass.Reuturn_Datatable_Search("sp_Set_Att_Employee_Notification_SelectRow", paramList);

        return dtInfo;
    }

    public DataTable GetEmployeeNotificationByTransId(string CompanyId, string TransId)
    {

        DataTable dtInfo = new DataTable();
        PassDataToSql[] paramList = new PassDataToSql[4];
        paramList[0] = new PassDataToSql("@Company_Id", CompanyId, PassDataToSql.ParaTypeList.Int, PassDataToSql.ParaDirectonList.Input);
        paramList[1] = new PassDataToSql("@Emp_Id", "0", PassDataToSql.ParaTypeList.Nvarchar, PassDataToSql.ParaDirectonList.Input);
        paramList[2] = new PassDataToSql("@Trans_Id", TransId, PassDataToSql.ParaTypeList.Nvarchar, PassDataToSql.ParaDirectonList.Input);
        paramList[3] = new PassDataToSql("@optype", "3", PassDataToSql.ParaTypeList.Nvarchar, PassDataToSql.ParaDirectonList.Input);
        dtInfo = daClass.Reuturn_Datatable_Search("sp_Set_Att_Employee_Notification_SelectRow", paramList);

        return dtInfo;
    }
}
