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
/// Summary description for Pay_Employee_claim
/// </summary>
public class Pay_Employee_claim
{
    DataAccessClass da = new DataAccessClass();



    public int Insert_In_Pay_Employee_Claim(string CompanyId, string EmployeeId, string ClaimName, string ClaimDiscription, string ValueType, string Value, string ClaimReqDate, string ClaimApproved,string ClaimAppDate,string ClaimMonth, string ClaimYear, string Field1, string Field2, string Field3, string Field4, string Field5, string Field6, string Field7, string IsActive, string CreatedBy, string CreatedDate, string ModifiedBy, string ModifiedDate)
    {
        PassDataToSql[] param = new PassDataToSql[25];
        param[0] = new PassDataToSql("@Company_Id", CompanyId, PassDataToSql.ParaTypeList.Int, PassDataToSql.ParaDirectonList.Input);

        param[1] = new PassDataToSql("@Emp_Id", EmployeeId, PassDataToSql.ParaTypeList.Int, PassDataToSql.ParaDirectonList.Input);
        param[2] = new PassDataToSql("@Claim_Name",ClaimName, PassDataToSql.ParaTypeList.Nvarchar, PassDataToSql.ParaDirectonList.Input);
        param[3] = new PassDataToSql("@Claim_Discription",ClaimDiscription, PassDataToSql.ParaTypeList.Nvarchar, PassDataToSql.ParaDirectonList.Input);
        param[4] = new PassDataToSql("@Value_Type", ValueType, PassDataToSql.ParaTypeList.Int, PassDataToSql.ParaDirectonList.Input);
        param[5] = new PassDataToSql("@Value", Value, PassDataToSql.ParaTypeList.Float, PassDataToSql.ParaDirectonList.Input);
        param[6] = new PassDataToSql("@Claim_Req_Date",ClaimReqDate, PassDataToSql.ParaTypeList.DateTime, PassDataToSql.ParaDirectonList.Input);
        param[7] = new PassDataToSql("@Claim_Approved",ClaimApproved, PassDataToSql.ParaTypeList.Nvarchar, PassDataToSql.ParaDirectonList.Input);
        param[8] = new PassDataToSql("@Claim_App_Date", ClaimAppDate, PassDataToSql.ParaTypeList.DateTime, PassDataToSql.ParaDirectonList.Input);
        
        param[9] = new PassDataToSql("@Claim_Month",ClaimMonth, PassDataToSql.ParaTypeList.Int, PassDataToSql.ParaDirectonList.Input);
        param[10] = new PassDataToSql("@Claim_Year",ClaimYear, PassDataToSql.ParaTypeList.Int, PassDataToSql.ParaDirectonList.Input);

        param[11] = new PassDataToSql("@Field1", Field1, PassDataToSql.ParaTypeList.Nvarchar, PassDataToSql.ParaDirectonList.Input);
        param[12] = new PassDataToSql("@Field2", Field2, PassDataToSql.ParaTypeList.Nvarchar, PassDataToSql.ParaDirectonList.Input);
        param[13] = new PassDataToSql("@Field3", Field3, PassDataToSql.ParaTypeList.Nvarchar, PassDataToSql.ParaDirectonList.Input);
        param[14] = new PassDataToSql("@Field4", Field4, PassDataToSql.ParaTypeList.Nvarchar, PassDataToSql.ParaDirectonList.Input);
        param[15] = new PassDataToSql("@Field5", Field5, PassDataToSql.ParaTypeList.Nvarchar, PassDataToSql.ParaDirectonList.Input);
        param[16] = new PassDataToSql("@Field6", Field6, PassDataToSql.ParaTypeList.Bit, PassDataToSql.ParaDirectonList.Input);
        param[17] = new PassDataToSql("@Field7", Field7, PassDataToSql.ParaTypeList.DateTime, PassDataToSql.ParaDirectonList.Input);
        param[18] = new PassDataToSql("@IsActive", IsActive, PassDataToSql.ParaTypeList.Bit, PassDataToSql.ParaDirectonList.Input);
        param[19] = new PassDataToSql("@CreatedBy", CreatedBy, PassDataToSql.ParaTypeList.Nvarchar, PassDataToSql.ParaDirectonList.Input);
        param[20] = new PassDataToSql("@CreatedDate", CreatedDate, PassDataToSql.ParaTypeList.DateTime, PassDataToSql.ParaDirectonList.Input);
        param[21] = new PassDataToSql("@ModifiedBy", ModifiedBy, PassDataToSql.ParaTypeList.Nvarchar, PassDataToSql.ParaDirectonList.Input);
        param[22] = new PassDataToSql("@ModifiedDate", ModifiedDate, PassDataToSql.ParaTypeList.DateTime, PassDataToSql.ParaDirectonList.Input);
        param[23] = new PassDataToSql("@Op_Type", "1", PassDataToSql.ParaTypeList.Int, PassDataToSql.ParaDirectonList.Input);
        
        param[24] = new PassDataToSql("@ReferenceID", "0", PassDataToSql.ParaTypeList.Int, PassDataToSql.ParaDirectonList.Output);

        //param[0] = new PassDataToSql("@Company_id",CompanyId, PassDataToSql.ParaTypeList.Int, PassDataToSql.ParaDirectonList.Input);
        //param[0] = new PassDataToSql("@Company_id", CompanyId, PassDataToSql.ParaTypeList.Int, PassDataToSql.ParaDirectonList.Input);

        da.execute_Sp("Sp_Pay_Employee_Claim_Insert", param);
        return Convert.ToInt32(param[24].ParaValue);

        //return Convert.ToInt32(param[3].ParaValue);

    }
    
    public DataTable GetRecord_From_PayEmployeeClaim(string CompanyId,string Empid, string ClaimId, string Month, string Year, string ClaimApproved,string Field1, string Field2)
    {
        DataTable Dt = new DataTable();
        PassDataToSql[] param = new PassDataToSql[9];
        param[0] = new PassDataToSql("@Company_Id", CompanyId, PassDataToSql.ParaTypeList.Int, PassDataToSql.ParaDirectonList.Input);
        param[1] = new PassDataToSql("@EmpId", Empid, PassDataToSql.ParaTypeList.Int, PassDataToSql.ParaDirectonList.Input);

        param[2] = new PassDataToSql("@ClaimId", ClaimId, PassDataToSql.ParaTypeList.Int, PassDataToSql.ParaDirectonList.Input);
        param[3] = new PassDataToSql("@Month", Month, PassDataToSql.ParaTypeList.Int, PassDataToSql.ParaDirectonList.Input);
        param[4] = new PassDataToSql("@year", Year, PassDataToSql.ParaTypeList.Int, PassDataToSql.ParaDirectonList.Input);
        param[5] = new PassDataToSql("@claim_Approved",ClaimApproved, PassDataToSql.ParaTypeList.Nvarchar, PassDataToSql.ParaDirectonList.Input);
        param[6] = new PassDataToSql("@Field1", Field1, PassDataToSql.ParaTypeList.Nvarchar, PassDataToSql.ParaDirectonList.Input);
        param[7] = new PassDataToSql("@Field2", Field2, PassDataToSql.ParaTypeList.Nvarchar, PassDataToSql.ParaDirectonList.Input);
        param[8] = new PassDataToSql("@optype", "1", PassDataToSql.ParaTypeList.Int, PassDataToSql.ParaDirectonList.Input);

        Dt = da.Reuturn_Datatable_Search("Sp_Pay_Employee_Claim_SelectRow", param);
        return Dt;

    }
    public DataTable GetRecord_From_PayEmployeeClaim_usingClaimId(string CompanyId, string Empid, string ClaimId, string Month, string Year, string ClaimApproved,string Field1, string Field2)
    {
        DataTable Dt = new DataTable();
        PassDataToSql[] param = new PassDataToSql[9];
        param[0] = new PassDataToSql("@Company_Id", CompanyId, PassDataToSql.ParaTypeList.Int, PassDataToSql.ParaDirectonList.Input);
        param[1] = new PassDataToSql("@EmpId", Empid, PassDataToSql.ParaTypeList.Int, PassDataToSql.ParaDirectonList.Input);

        param[2] = new PassDataToSql("@ClaimId", ClaimId, PassDataToSql.ParaTypeList.Int, PassDataToSql.ParaDirectonList.Input);
        param[3] = new PassDataToSql("@Month", Month, PassDataToSql.ParaTypeList.Int, PassDataToSql.ParaDirectonList.Input);
        param[4] = new PassDataToSql("@year", Year, PassDataToSql.ParaTypeList.Int, PassDataToSql.ParaDirectonList.Input);
        param[5] = new PassDataToSql("@claim_Approved", ClaimApproved, PassDataToSql.ParaTypeList.Nvarchar, PassDataToSql.ParaDirectonList.Input);
        
        param[6] = new PassDataToSql("@Field1", Field1, PassDataToSql.ParaTypeList.Nvarchar, PassDataToSql.ParaDirectonList.Input);
        param[7] = new PassDataToSql("@Field2", Field2, PassDataToSql.ParaTypeList.Nvarchar, PassDataToSql.ParaDirectonList.Input);
        param[8] = new PassDataToSql("@optype", "2", PassDataToSql.ParaTypeList.Int, PassDataToSql.ParaDirectonList.Input);

        Dt = da.Reuturn_Datatable_Search("Sp_Pay_Employee_Claim_SelectRow", param);
        return Dt;

    }
    public int DeleteRecord_in_Pay_Employee_Claim(string companyId, string ClaimId, string ClaimApproved, string ModifiedBy, string Modifieddate)
    {

        PassDataToSql[] param = new PassDataToSql[6];
        param[0] = new PassDataToSql("@Company_Id", companyId, PassDataToSql.ParaTypeList.Int, PassDataToSql.ParaDirectonList.Input);
        param[1] = new PassDataToSql("@Claim_Id", ClaimId, PassDataToSql.ParaTypeList.Int, PassDataToSql.ParaDirectonList.Input);
        param[2] = new PassDataToSql("@Claim_Approved",ClaimApproved, PassDataToSql.ParaTypeList.Nvarchar, PassDataToSql.ParaDirectonList.Input);
        param[3] = new PassDataToSql("@ModifiedBy", ModifiedBy, PassDataToSql.ParaTypeList.Nvarchar, PassDataToSql.ParaDirectonList.Input);
        param[4] = new PassDataToSql("@ModifiedDate", Modifieddate, PassDataToSql.ParaTypeList.DateTime, PassDataToSql.ParaDirectonList.Input);
        param[5] = new PassDataToSql("@ReferenceID", "0", PassDataToSql.ParaTypeList.Int, PassDataToSql.ParaDirectonList.Output);

        //param[0] = new PassDataToSql("@Company_id",CompanyId, PassDataToSql.ParaTypeList.Int, PassDataToSql.ParaDirectonList.Input);
        //param[0] = new PassDataToSql("@Company_id", CompanyId, PassDataToSql.ParaTypeList.Int, PassDataToSql.ParaDirectonList.Input);

        da.execute_Sp("sp_Pay_Employee_Claim_RowStatus", param);
        return Convert.ToInt32(param[5].ParaValue);

    }
    public int UpdateRecord_In_Pay_Employee_Claim(string CompanyId, string Claimid, string Claimname, string ClaimDiscription, string value_type, string value, string Month, string Year, string ClaimApproved,string ModifiedBy, string Modifieddate)
    {

        PassDataToSql[] param = new PassDataToSql[13];
        param[0] = new PassDataToSql("@Company_Id", CompanyId, PassDataToSql.ParaTypeList.Int, PassDataToSql.ParaDirectonList.Input);
        param[1] = new PassDataToSql("@Claim_Id",Claimid, PassDataToSql.ParaTypeList.Int, PassDataToSql.ParaDirectonList.Input);
        param[2] = new PassDataToSql("@Claim_Name",Claimname, PassDataToSql.ParaTypeList.Nvarchar, PassDataToSql.ParaDirectonList.Input);
        param[3] = new PassDataToSql("@Claim_Description",ClaimDiscription, PassDataToSql.ParaTypeList.Nvarchar, PassDataToSql.ParaDirectonList.Input);
        param[4] = new PassDataToSql("@Value_Type", value_type, PassDataToSql.ParaTypeList.Int, PassDataToSql.ParaDirectonList.Input);
        param[5] = new PassDataToSql("@Value", value, PassDataToSql.ParaTypeList.Float, PassDataToSql.ParaDirectonList.Input);
        param[6] = new PassDataToSql("@Claim_Month", Month, PassDataToSql.ParaTypeList.Int, PassDataToSql.ParaDirectonList.Input);
        param[7] = new PassDataToSql("@Claim_Year", Year, PassDataToSql.ParaTypeList.Int, PassDataToSql.ParaDirectonList.Input);
        param[8] = new PassDataToSql("@Claim_Approved", ClaimApproved, PassDataToSql.ParaTypeList.Nvarchar, PassDataToSql.ParaDirectonList.Input);
        param[9] = new PassDataToSql("@Claim_App_Date", Modifieddate, PassDataToSql.ParaTypeList.DateTime, PassDataToSql.ParaDirectonList.Input);
        
        param[10] = new PassDataToSql("@ModifiedBy", ModifiedBy, PassDataToSql.ParaTypeList.Nvarchar, PassDataToSql.ParaDirectonList.Input);
        param[11] = new PassDataToSql("@ModifiedDate", Modifieddate, PassDataToSql.ParaTypeList.DateTime, PassDataToSql.ParaDirectonList.Input);
        param[12] = new PassDataToSql("@ReferenceID", "0", PassDataToSql.ParaTypeList.Int, PassDataToSql.ParaDirectonList.Output);

        //param[0] = new PassDataToSql("@Company_id",CompanyId, PassDataToSql.ParaTypeList.Int, PassDataToSql.ParaDirectonList.Input);
        //param[0] = new PassDataToSql("@Company_id", CompanyId, PassDataToSql.ParaTypeList.Int, PassDataToSql.ParaDirectonList.Input);

        da.execute_Sp("sp_Pay_Employee_Claim_Update", param);
        return Convert.ToInt32(param[12].ParaValue);
    }
    public static DataTable GetClaimName(string prefixText, string comp)
    {
        DataAccessClass da = new DataAccessClass();
        DataTable Dt = new DataTable();
        PassDataToSql[] param = new PassDataToSql[9];
        param[0] = new PassDataToSql("@Company_Id",comp, PassDataToSql.ParaTypeList.Int, PassDataToSql.ParaDirectonList.Input);
        param[1] = new PassDataToSql("@EmpId","0", PassDataToSql.ParaTypeList.Int, PassDataToSql.ParaDirectonList.Input);

        param[2] = new PassDataToSql("@ClaimId","0", PassDataToSql.ParaTypeList.Int, PassDataToSql.ParaDirectonList.Input);
        param[3] = new PassDataToSql("@Month","0", PassDataToSql.ParaTypeList.Int, PassDataToSql.ParaDirectonList.Input);
        param[4] = new PassDataToSql("@year","0", PassDataToSql.ParaTypeList.Int, PassDataToSql.ParaDirectonList.Input);
        param[5] = new PassDataToSql("@claim_Approved","", PassDataToSql.ParaTypeList.Nvarchar, PassDataToSql.ParaDirectonList.Input);
        param[6] = new PassDataToSql("@Field1","", PassDataToSql.ParaTypeList.Nvarchar, PassDataToSql.ParaDirectonList.Input);
        param[7] = new PassDataToSql("@Field2","", PassDataToSql.ParaTypeList.Nvarchar, PassDataToSql.ParaDirectonList.Input);
        param[8] = new PassDataToSql("@optype", "3", PassDataToSql.ParaTypeList.Int, PassDataToSql.ParaDirectonList.Input);

        Dt = da.Reuturn_Datatable_Search("Sp_Pay_Employee_Claim_SelectRow", param);
        return Dt;
    }
    public int Insert_In_Pay_Employee_ClaimRequest(string CompanyId, string EmployeeId, string ClaimName, string ClaimDiscription, string ValueType, string Value, string ClaimReqDate, string ClaimApproved, string ClaimAppDate, string ClaimMonth, string ClaimYear, string Field1, string Field2, string Field3, string Field4, string Field5, string Field6, string Field7, string IsActive, string CreatedBy, string CreatedDate, string ModifiedBy, string ModifiedDate)
    {
        PassDataToSql[] param = new PassDataToSql[25];
        param[0] = new PassDataToSql("@Company_Id", CompanyId, PassDataToSql.ParaTypeList.Int, PassDataToSql.ParaDirectonList.Input);

        param[1] = new PassDataToSql("@Emp_Id", EmployeeId, PassDataToSql.ParaTypeList.Int, PassDataToSql.ParaDirectonList.Input);
        param[2] = new PassDataToSql("@Claim_Name", ClaimName, PassDataToSql.ParaTypeList.Nvarchar, PassDataToSql.ParaDirectonList.Input);
        param[3] = new PassDataToSql("@Claim_Discription", ClaimDiscription, PassDataToSql.ParaTypeList.Nvarchar, PassDataToSql.ParaDirectonList.Input);
        param[4] = new PassDataToSql("@Value_Type", ValueType, PassDataToSql.ParaTypeList.Int, PassDataToSql.ParaDirectonList.Input);
        param[5] = new PassDataToSql("@Value", Value, PassDataToSql.ParaTypeList.Float, PassDataToSql.ParaDirectonList.Input);
        param[6] = new PassDataToSql("@Claim_Req_Date", ClaimReqDate, PassDataToSql.ParaTypeList.DateTime, PassDataToSql.ParaDirectonList.Input);
        param[7] = new PassDataToSql("@Claim_Approved", ClaimApproved, PassDataToSql.ParaTypeList.Nvarchar, PassDataToSql.ParaDirectonList.Input);
        param[8] = new PassDataToSql("@Claim_App_Date", ClaimAppDate, PassDataToSql.ParaTypeList.DateTime, PassDataToSql.ParaDirectonList.Input);

        param[9] = new PassDataToSql("@Claim_Month", ClaimMonth, PassDataToSql.ParaTypeList.Int, PassDataToSql.ParaDirectonList.Input);
        param[10] = new PassDataToSql("@Claim_Year", ClaimYear, PassDataToSql.ParaTypeList.Int, PassDataToSql.ParaDirectonList.Input);

        param[11] = new PassDataToSql("@Field1", Field1, PassDataToSql.ParaTypeList.Nvarchar, PassDataToSql.ParaDirectonList.Input);
        param[12] = new PassDataToSql("@Field2", Field2, PassDataToSql.ParaTypeList.Nvarchar, PassDataToSql.ParaDirectonList.Input);
        param[13] = new PassDataToSql("@Field3", Field3, PassDataToSql.ParaTypeList.Nvarchar, PassDataToSql.ParaDirectonList.Input);
        param[14] = new PassDataToSql("@Field4", Field4, PassDataToSql.ParaTypeList.Nvarchar, PassDataToSql.ParaDirectonList.Input);
        param[15] = new PassDataToSql("@Field5", Field5, PassDataToSql.ParaTypeList.Nvarchar, PassDataToSql.ParaDirectonList.Input);
        param[16] = new PassDataToSql("@Field6", Field6, PassDataToSql.ParaTypeList.Bit, PassDataToSql.ParaDirectonList.Input);
        param[17] = new PassDataToSql("@Field7", Field7, PassDataToSql.ParaTypeList.DateTime, PassDataToSql.ParaDirectonList.Input);
        param[18] = new PassDataToSql("@IsActive", IsActive, PassDataToSql.ParaTypeList.Bit, PassDataToSql.ParaDirectonList.Input);
        param[19] = new PassDataToSql("@CreatedBy", CreatedBy, PassDataToSql.ParaTypeList.Nvarchar, PassDataToSql.ParaDirectonList.Input);
        param[20] = new PassDataToSql("@CreatedDate", CreatedDate, PassDataToSql.ParaTypeList.DateTime, PassDataToSql.ParaDirectonList.Input);
        param[21] = new PassDataToSql("@ModifiedBy", ModifiedBy, PassDataToSql.ParaTypeList.Nvarchar, PassDataToSql.ParaDirectonList.Input);
        param[22] = new PassDataToSql("@ModifiedDate", ModifiedDate, PassDataToSql.ParaTypeList.DateTime, PassDataToSql.ParaDirectonList.Input);
        param[23] = new PassDataToSql("@Op_Type", "2", PassDataToSql.ParaTypeList.Int, PassDataToSql.ParaDirectonList.Input);

        param[24] = new PassDataToSql("@ReferenceID", "0", PassDataToSql.ParaTypeList.Int, PassDataToSql.ParaDirectonList.Output);

        //param[0] = new PassDataToSql("@Company_id",CompanyId, PassDataToSql.ParaTypeList.Int, PassDataToSql.ParaDirectonList.Input);
        //param[0] = new PassDataToSql("@Company_id", CompanyId, PassDataToSql.ParaTypeList.Int, PassDataToSql.ParaDirectonList.Input);

        da.execute_Sp("Sp_Pay_Employee_Claim_Insert", param);
        return Convert.ToInt32(param[24].ParaValue);

        //return Convert.ToInt32(param[3].ParaValue);

    }
 public DataTable GetRecord_From_PayEmployeeClaim_1(string CompanyId, string Empid, string ClaimId,string strclaimapproved ,string Month, string Year, string Field1, string Field2)
    {
        DataTable Dt = new DataTable();
        PassDataToSql[] param = new PassDataToSql[9];
        param[0] = new PassDataToSql("@Company_Id", CompanyId, PassDataToSql.ParaTypeList.Int, PassDataToSql.ParaDirectonList.Input);
        param[1] = new PassDataToSql("@EmpId", Empid, PassDataToSql.ParaTypeList.Int, PassDataToSql.ParaDirectonList.Input);

        param[2] = new PassDataToSql("@ClaimId", "0", PassDataToSql.ParaTypeList.Int, PassDataToSql.ParaDirectonList.Input);
        param[3] = new PassDataToSql("@Month", Month, PassDataToSql.ParaTypeList.Int, PassDataToSql.ParaDirectonList.Input);
        param[4] = new PassDataToSql("@year", Year, PassDataToSql.ParaTypeList.Int, PassDataToSql.ParaDirectonList.Input);
        param[5] = new PassDataToSql("@claim_Approved", strclaimapproved, PassDataToSql.ParaTypeList.Nvarchar, PassDataToSql.ParaDirectonList.Input);
        param[6] = new PassDataToSql("@Field1", Field1, PassDataToSql.ParaTypeList.Nvarchar, PassDataToSql.ParaDirectonList.Input);
        param[7] = new PassDataToSql("@Field2", Field2, PassDataToSql.ParaTypeList.Nvarchar, PassDataToSql.ParaDirectonList.Input);
        param[8] = new PassDataToSql("@optype", "4", PassDataToSql.ParaTypeList.Int, PassDataToSql.ParaDirectonList.Input);

        Dt = da.Reuturn_Datatable_Search("Sp_Pay_Employee_Claim_SelectRow", param);
        return Dt;

    }
    
    
    
    
    




	public Pay_Employee_claim()
	{
		//
		// TODO: Add constructor logic here
		//
	}
}
