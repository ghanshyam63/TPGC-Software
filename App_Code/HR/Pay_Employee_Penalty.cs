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
/// Summary description for Pay_Employee_Penalty
/// </summary>
public class Pay_Employee_Penalty
{
    DataAccessClass da = new DataAccessClass();
   

    public int Insert_In_Pay_Employee_Penalty(string CompanyId, string EmployeeId, string PenaltyName, string PenaltyDiscription, string ValueType, string Value, string PenaltyDate, string PenaltyMonth, string PenaltyYear, string Field1, string Field2, string Field3, string Field4, string Field5, string Field6, string Field7, string IsActive, string CreatedBy, string CreatedDate, string ModifiedBy, string ModifiedDate)
    {
        PassDataToSql[] param = new PassDataToSql[22];
        param[0] = new PassDataToSql("@Company_Id", CompanyId, PassDataToSql.ParaTypeList.Int, PassDataToSql.ParaDirectonList.Input);

        param[1] = new PassDataToSql("@Emp_Id", EmployeeId, PassDataToSql.ParaTypeList.Int, PassDataToSql.ParaDirectonList.Input);
        param[2] = new PassDataToSql("@Penalty_Name",PenaltyName, PassDataToSql.ParaTypeList.Nvarchar, PassDataToSql.ParaDirectonList.Input);
        param[3] = new PassDataToSql("@Penalty_Discription",PenaltyDiscription, PassDataToSql.ParaTypeList.Nvarchar, PassDataToSql.ParaDirectonList.Input);
        param[4] = new PassDataToSql("@Value_Type", ValueType, PassDataToSql.ParaTypeList.Int, PassDataToSql.ParaDirectonList.Input);
        param[5] = new PassDataToSql("@Value",Value, PassDataToSql.ParaTypeList.Float, PassDataToSql.ParaDirectonList.Input);
        param[6] = new PassDataToSql("@Penalty_Date", PenaltyDate, PassDataToSql.ParaTypeList.DateTime, PassDataToSql.ParaDirectonList.Input);
        param[7] = new PassDataToSql("@Penalty_Month",PenaltyMonth, PassDataToSql.ParaTypeList.Int, PassDataToSql.ParaDirectonList.Input);
        param[8] = new PassDataToSql("@Penalty_Year",PenaltyYear, PassDataToSql.ParaTypeList.Int, PassDataToSql.ParaDirectonList.Input);

        param[9] = new PassDataToSql("@Field1", Field1, PassDataToSql.ParaTypeList.Nvarchar, PassDataToSql.ParaDirectonList.Input);
        param[10] = new PassDataToSql("@Field2", Field2, PassDataToSql.ParaTypeList.Nvarchar, PassDataToSql.ParaDirectonList.Input);
        param[11] = new PassDataToSql("@Field3", Field3, PassDataToSql.ParaTypeList.Nvarchar, PassDataToSql.ParaDirectonList.Input);
        param[12] = new PassDataToSql("@Field4", Field4, PassDataToSql.ParaTypeList.Nvarchar, PassDataToSql.ParaDirectonList.Input);
        param[13] = new PassDataToSql("@Field5", Field5, PassDataToSql.ParaTypeList.Nvarchar, PassDataToSql.ParaDirectonList.Input);
        param[14] = new PassDataToSql("@Field6", Field6, PassDataToSql.ParaTypeList.Bit, PassDataToSql.ParaDirectonList.Input);
        param[15] = new PassDataToSql("@Field7", Field7, PassDataToSql.ParaTypeList.DateTime, PassDataToSql.ParaDirectonList.Input);
        param[16] = new PassDataToSql("@IsActive", IsActive, PassDataToSql.ParaTypeList.Bit, PassDataToSql.ParaDirectonList.Input);
        param[17] = new PassDataToSql("@CreatedBy", CreatedBy, PassDataToSql.ParaTypeList.Nvarchar, PassDataToSql.ParaDirectonList.Input);
        param[18] = new PassDataToSql("@CreatedDate", CreatedDate, PassDataToSql.ParaTypeList.DateTime, PassDataToSql.ParaDirectonList.Input);
        param[19] = new PassDataToSql("@ModifiedBy", ModifiedBy, PassDataToSql.ParaTypeList.Nvarchar, PassDataToSql.ParaDirectonList.Input);
        param[20] = new PassDataToSql("@ModifiedDate", ModifiedDate, PassDataToSql.ParaTypeList.DateTime, PassDataToSql.ParaDirectonList.Input);
        param[21] = new PassDataToSql("@ReferenceID", "0", PassDataToSql.ParaTypeList.Int, PassDataToSql.ParaDirectonList.Output);

        //param[0] = new PassDataToSql("@Company_id",CompanyId, PassDataToSql.ParaTypeList.Int, PassDataToSql.ParaDirectonList.Input);
        //param[0] = new PassDataToSql("@Company_id", CompanyId, PassDataToSql.ParaTypeList.Int, PassDataToSql.ParaDirectonList.Input);

        da.execute_Sp("Sp_Pay_Employee_Penalty_Insert", param);
        return Convert.ToInt32(param[21].ParaValue);

        //return Convert.ToInt32(param[3].ParaValue);

    }
    public DataTable GetRecord_From_PayEmployeePenalty(string CompanyId,string Empid, string PenaltyId, string Month, string Year, string Field1, string Field2)
    {
        DataTable Dt = new DataTable();
        PassDataToSql[] param = new PassDataToSql[8];
        param[0] = new PassDataToSql("@Company_Id",CompanyId, PassDataToSql.ParaTypeList.Int, PassDataToSql.ParaDirectonList.Input);

        param[1] = new PassDataToSql("@EmpId",Empid, PassDataToSql.ParaTypeList.Int, PassDataToSql.ParaDirectonList.Input);

        param[2] = new PassDataToSql("@PenaltyId",PenaltyId, PassDataToSql.ParaTypeList.Int, PassDataToSql.ParaDirectonList.Input);
        param[3] = new PassDataToSql("@Month",Month, PassDataToSql.ParaTypeList.Int, PassDataToSql.ParaDirectonList.Input);
        param[4] = new PassDataToSql("@year", Year, PassDataToSql.ParaTypeList.Int, PassDataToSql.ParaDirectonList.Input);
        param[5] = new PassDataToSql("@Field1", Field1, PassDataToSql.ParaTypeList.Nvarchar, PassDataToSql.ParaDirectonList.Input);
        param[6] = new PassDataToSql("@Field2", Field2, PassDataToSql.ParaTypeList.Nvarchar, PassDataToSql.ParaDirectonList.Input);
        param[7] = new PassDataToSql("@Optype", "1", PassDataToSql.ParaTypeList.Int, PassDataToSql.ParaDirectonList.Input);

        Dt = da.Reuturn_Datatable_Search("Sp_Pay_Employee_penalty_SelectRow", param);
        return Dt;

    }
    public DataTable GetRecord_From_PayEmployeePenalty_usingPenaltyId(string CompanyId, string Empid, string PenaltyId, string Month, string Year, string Field1, string Field2)
    {
        DataTable Dt = new DataTable();
        PassDataToSql[] param = new PassDataToSql[8];
        param[0] = new PassDataToSql("@Company_Id", CompanyId, PassDataToSql.ParaTypeList.Int, PassDataToSql.ParaDirectonList.Input);

        param[1] = new PassDataToSql("@EmpId", Empid, PassDataToSql.ParaTypeList.Int, PassDataToSql.ParaDirectonList.Input);

        param[2] = new PassDataToSql("@PenaltyId", PenaltyId, PassDataToSql.ParaTypeList.Int, PassDataToSql.ParaDirectonList.Input);
        param[3] = new PassDataToSql("@Month", Month, PassDataToSql.ParaTypeList.Int, PassDataToSql.ParaDirectonList.Input);
        param[4] = new PassDataToSql("@year", Year, PassDataToSql.ParaTypeList.Int, PassDataToSql.ParaDirectonList.Input);
        param[5] = new PassDataToSql("@Field1", Field1, PassDataToSql.ParaTypeList.Nvarchar, PassDataToSql.ParaDirectonList.Input);
        param[6] = new PassDataToSql("@Field2", Field2, PassDataToSql.ParaTypeList.Nvarchar, PassDataToSql.ParaDirectonList.Input);
        param[7] = new PassDataToSql("@Optype", "2", PassDataToSql.ParaTypeList.Int, PassDataToSql.ParaDirectonList.Input);

        Dt = da.Reuturn_Datatable_Search("Sp_Pay_Employee_penalty_SelectRow", param);
        return Dt;

    }
    public int DeleteRecord_in_Pay_Employee_penalty(string companyId,string PenaltyId,string IsActive,string ModifiedBy,string Modifieddate)
    {

        PassDataToSql[] param = new PassDataToSql[6];
        param[0] = new PassDataToSql("@Company_Id",companyId, PassDataToSql.ParaTypeList.Int, PassDataToSql.ParaDirectonList.Input);
        param[1] = new PassDataToSql("@Penalty_Id",PenaltyId, PassDataToSql.ParaTypeList.Int, PassDataToSql.ParaDirectonList.Input);
        param[2] = new PassDataToSql("@IsActive", IsActive, PassDataToSql.ParaTypeList.Bit, PassDataToSql.ParaDirectonList.Input);
        param[3] = new PassDataToSql("@ModifiedBy",ModifiedBy, PassDataToSql.ParaTypeList.Nvarchar, PassDataToSql.ParaDirectonList.Input);
        param[4] = new PassDataToSql("@ModifiedDate", Modifieddate, PassDataToSql.ParaTypeList.DateTime, PassDataToSql.ParaDirectonList.Input);
        param[5] = new PassDataToSql("@ReferenceID", "0", PassDataToSql.ParaTypeList.Int, PassDataToSql.ParaDirectonList.Output);

        //param[0] = new PassDataToSql("@Company_id",CompanyId, PassDataToSql.ParaTypeList.Int, PassDataToSql.ParaDirectonList.Input);
        //param[0] = new PassDataToSql("@Company_id", CompanyId, PassDataToSql.ParaTypeList.Int, PassDataToSql.ParaDirectonList.Input);

        da.execute_Sp("sp_Pay_Employee_Penalty_RowStatus", param);
        return Convert.ToInt32(param[5].ParaValue);

    }
    public int UpdateRecord_In_Pay_Employee_Penalty( string CompanyId,string Penaltyid,string penaltyname,string penaltyDiscription,string value_type,string value,string Month,string Year,string ModifiedBy,string Modifieddate)
    {

        PassDataToSql[] param = new PassDataToSql[11];
        param[0] = new PassDataToSql("@Company_Id",CompanyId, PassDataToSql.ParaTypeList.Int, PassDataToSql.ParaDirectonList.Input);
        param[1] = new PassDataToSql("@Penalty_Id",Penaltyid, PassDataToSql.ParaTypeList.Int, PassDataToSql.ParaDirectonList.Input);
        param[2] = new PassDataToSql("@Penalty_Name",penaltyname, PassDataToSql.ParaTypeList.Nvarchar, PassDataToSql.ParaDirectonList.Input);
        param[3] = new PassDataToSql("@Penalty_Description",penaltyDiscription, PassDataToSql.ParaTypeList.Nvarchar, PassDataToSql.ParaDirectonList.Input);
        param[4] = new PassDataToSql("@Value_Type",value_type, PassDataToSql.ParaTypeList.Int, PassDataToSql.ParaDirectonList.Input);
        param[5] = new PassDataToSql("@Value",value, PassDataToSql.ParaTypeList.Float, PassDataToSql.ParaDirectonList.Input);
        param[6] = new PassDataToSql("@Penalty_Month", Month, PassDataToSql.ParaTypeList.Int, PassDataToSql.ParaDirectonList.Input);
        param[7] = new PassDataToSql("@Penalty_Year", Year, PassDataToSql.ParaTypeList.Int, PassDataToSql.ParaDirectonList.Input);
        param[8] = new PassDataToSql("@ModifiedBy", ModifiedBy, PassDataToSql.ParaTypeList.Nvarchar, PassDataToSql.ParaDirectonList.Input);
        param[9] = new PassDataToSql("@ModifiedDate", Modifieddate, PassDataToSql.ParaTypeList.DateTime, PassDataToSql.ParaDirectonList.Input);
        param[10] = new PassDataToSql("@ReferenceID", "0", PassDataToSql.ParaTypeList.Int, PassDataToSql.ParaDirectonList.Output);

        //param[0] = new PassDataToSql("@Company_id",CompanyId, PassDataToSql.ParaTypeList.Int, PassDataToSql.ParaDirectonList.Input);
        //param[0] = new PassDataToSql("@Company_id", CompanyId, PassDataToSql.ParaTypeList.Int, PassDataToSql.ParaDirectonList.Input);

        da.execute_Sp("sp_Pay_Employee_Penalty_Update", param);
        return Convert.ToInt32(param[10].ParaValue);
    }
    public DataTable GetPenaltyName(string prefixText, string CompanyId)
    {
        DataTable Dt = new DataTable();
        PassDataToSql[] param = new PassDataToSql[8];
        param[0] = new PassDataToSql("@Company_Id", CompanyId, PassDataToSql.ParaTypeList.Int, PassDataToSql.ParaDirectonList.Input);

        param[1] = new PassDataToSql("@EmpId","0", PassDataToSql.ParaTypeList.Int, PassDataToSql.ParaDirectonList.Input);

        param[2] = new PassDataToSql("@PenaltyId","0", PassDataToSql.ParaTypeList.Int, PassDataToSql.ParaDirectonList.Input);
        param[3] = new PassDataToSql("@Month", "0", PassDataToSql.ParaTypeList.Int, PassDataToSql.ParaDirectonList.Input);
        param[4] = new PassDataToSql("@year","0", PassDataToSql.ParaTypeList.Int, PassDataToSql.ParaDirectonList.Input);
        param[5] = new PassDataToSql("@Field1","", PassDataToSql.ParaTypeList.Nvarchar, PassDataToSql.ParaDirectonList.Input);
        param[6] = new PassDataToSql("@Field2","", PassDataToSql.ParaTypeList.Nvarchar, PassDataToSql.ParaDirectonList.Input);
        param[7] = new PassDataToSql("@Optype", "3", PassDataToSql.ParaTypeList.Int, PassDataToSql.ParaDirectonList.Input);

        Dt = da.Reuturn_Datatable_Search("Sp_Pay_Employee_penalty_SelectRow", param);
        return Dt;

    }
    public DataTable GetRecord_From_PayEmployeePenalty_1(string CompanyId, string Empid, string PenaltyId, string Month, string Year, string Field1, string Field2)
    {
        DataTable Dt = new DataTable();
        PassDataToSql[] param = new PassDataToSql[8];
        param[0] = new PassDataToSql("@Company_Id", CompanyId, PassDataToSql.ParaTypeList.Int, PassDataToSql.ParaDirectonList.Input);

        param[1] = new PassDataToSql("@EmpId", Empid, PassDataToSql.ParaTypeList.Int, PassDataToSql.ParaDirectonList.Input);

        param[2] = new PassDataToSql("@PenaltyId", "0", PassDataToSql.ParaTypeList.Int, PassDataToSql.ParaDirectonList.Input);
        param[3] = new PassDataToSql("@Month", Month, PassDataToSql.ParaTypeList.Int, PassDataToSql.ParaDirectonList.Input);
        param[4] = new PassDataToSql("@year", Year, PassDataToSql.ParaTypeList.Int, PassDataToSql.ParaDirectonList.Input);
        param[5] = new PassDataToSql("@Field1", Field1, PassDataToSql.ParaTypeList.Nvarchar, PassDataToSql.ParaDirectonList.Input);
        param[6] = new PassDataToSql("@Field2", Field2, PassDataToSql.ParaTypeList.Nvarchar, PassDataToSql.ParaDirectonList.Input);
        param[7] = new PassDataToSql("@Optype", "4", PassDataToSql.ParaTypeList.Int, PassDataToSql.ParaDirectonList.Input);

        Dt = da.Reuturn_Datatable_Search("Sp_Pay_Employee_penalty_SelectRow", param);
        return Dt;

    }
    
    
    
    
    public Pay_Employee_Penalty()
	{
		//
		// TODO: Add constructor logic here
		//
	}
}
