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
/// Summary description for Pay_Employee_Loan
/// </summary>
public class Pay_Employee_Loan
{
    DataAccessClass da = new DataAccessClass();
    public int Insert_In_Pay_Employee_Loan(string CompanyId, string EmployeeId, string LoanName, string Loan_Req_Date, string LoanAmount, string Is_Status, string Field1, string Field2, string Field3, string Field4, string Field5, string Field6, string Field7, string IsActive, string CreatedBy, string CreatedDate, string ModifiedBy, string ModifiedDate)
    {
        PassDataToSql[] param = new PassDataToSql[19];
        param[0] = new PassDataToSql("@Company_Id", CompanyId, PassDataToSql.ParaTypeList.Int, PassDataToSql.ParaDirectonList.Input);

        param[1] = new PassDataToSql("@Emp_Id", EmployeeId, PassDataToSql.ParaTypeList.Int, PassDataToSql.ParaDirectonList.Input);
        param[2] = new PassDataToSql("@Loan_Name", LoanName, PassDataToSql.ParaTypeList.Nvarchar, PassDataToSql.ParaDirectonList.Input);
        param[3] = new PassDataToSql("@Loan_Request_Date", Loan_Req_Date, PassDataToSql.ParaTypeList.DateTime, PassDataToSql.ParaDirectonList.Input);
        param[4] = new PassDataToSql("@Loan_Amount", LoanAmount, PassDataToSql.ParaTypeList.Float, PassDataToSql.ParaDirectonList.Input);
        param[5] = new PassDataToSql("@Is_Status", Is_Status, PassDataToSql.ParaTypeList.Nvarchar, PassDataToSql.ParaDirectonList.Input);
        param[6] = new PassDataToSql("@Field1", Field1, PassDataToSql.ParaTypeList.Nvarchar, PassDataToSql.ParaDirectonList.Input);
        param[7] = new PassDataToSql("@Field2", Field2, PassDataToSql.ParaTypeList.Nvarchar, PassDataToSql.ParaDirectonList.Input);
        param[8] = new PassDataToSql("@Field3", Field3, PassDataToSql.ParaTypeList.Nvarchar, PassDataToSql.ParaDirectonList.Input);
        param[9] = new PassDataToSql("@Field4", Field4, PassDataToSql.ParaTypeList.Nvarchar, PassDataToSql.ParaDirectonList.Input);
        param[10] = new PassDataToSql("@Field5", Field5, PassDataToSql.ParaTypeList.Nvarchar, PassDataToSql.ParaDirectonList.Input);
        param[11] = new PassDataToSql("@Field6", Field6, PassDataToSql.ParaTypeList.Bit, PassDataToSql.ParaDirectonList.Input);
        param[12] = new PassDataToSql("@Field7", Field7, PassDataToSql.ParaTypeList.DateTime, PassDataToSql.ParaDirectonList.Input);
        param[13] = new PassDataToSql("@IsActive", IsActive, PassDataToSql.ParaTypeList.Bit, PassDataToSql.ParaDirectonList.Input);
        param[14] = new PassDataToSql("@CreatedBy", CreatedBy, PassDataToSql.ParaTypeList.Nvarchar, PassDataToSql.ParaDirectonList.Input);
        param[15] = new PassDataToSql("@CreatedDate", CreatedDate, PassDataToSql.ParaTypeList.DateTime, PassDataToSql.ParaDirectonList.Input);
        param[16] = new PassDataToSql("@ModifiedBy", ModifiedBy, PassDataToSql.ParaTypeList.Nvarchar, PassDataToSql.ParaDirectonList.Input);
        param[17] = new PassDataToSql("@ModifiedDate", ModifiedDate, PassDataToSql.ParaTypeList.DateTime, PassDataToSql.ParaDirectonList.Input);
        param[18] = new PassDataToSql("@ReferenceID", "0", PassDataToSql.ParaTypeList.Int, PassDataToSql.ParaDirectonList.Output);

        //param[0] = new PassDataToSql("@Company_id",CompanyId, PassDataToSql.ParaTypeList.Int, PassDataToSql.ParaDirectonList.Input);
        //param[0] = new PassDataToSql("@Company_id", CompanyId, PassDataToSql.ParaTypeList.Int, PassDataToSql.ParaDirectonList.Input);

        da.execute_Sp("sp_Pay_Employee_Loan_Insert", param);
        return Convert.ToInt32(param[18].ParaValue);

        //return Convert.ToInt32(param[3].ParaValue);

    }
    public DataTable GetRecord_From_PayEmployeeLoan(string CompanyId, string LoanId, string IsStatus)
    {
        DataTable Dt = new DataTable();
        PassDataToSql[] param = new PassDataToSql[4];
        param[0] = new PassDataToSql("@Company_Id", CompanyId, PassDataToSql.ParaTypeList.Int, PassDataToSql.ParaDirectonList.Input);

        param[1] = new PassDataToSql("@Loan_Id", LoanId, PassDataToSql.ParaTypeList.Int, PassDataToSql.ParaDirectonList.Input);

        param[2] = new PassDataToSql("@Is_Status", IsStatus, PassDataToSql.ParaTypeList.Nvarchar, PassDataToSql.ParaDirectonList.Input);
        param[3] = new PassDataToSql("@OpType", "1", PassDataToSql.ParaTypeList.Int, PassDataToSql.ParaDirectonList.Input);

        Dt = da.Reuturn_Datatable_Search("sp_Pay_Employee_Loan_SelectRow", param);
        return Dt;

    }
    public static DataTable GetLoanName(string prefixText, string comp)
    {
        DataAccessClass da = new DataAccessClass();
        DataTable Dt = new DataTable();
        PassDataToSql[] param = new PassDataToSql[4];
        param[0] = new PassDataToSql("@Company_Id", comp, PassDataToSql.ParaTypeList.Int, PassDataToSql.ParaDirectonList.Input);

        param[1] = new PassDataToSql("@Loan_Id", "0", PassDataToSql.ParaTypeList.Int, PassDataToSql.ParaDirectonList.Input);

        param[2] = new PassDataToSql("@Is_Status", "0", PassDataToSql.ParaTypeList.Nvarchar, PassDataToSql.ParaDirectonList.Input);
        param[3] = new PassDataToSql("@OpType", "3", PassDataToSql.ParaTypeList.Int, PassDataToSql.ParaDirectonList.Input);

        Dt = da.Reuturn_Datatable_Search("sp_Pay_Employee_Loan_SelectRow", param);
        return Dt;

    }

    public DataTable GetRecord_From_PayEmployeeLoan_usingLoanId(string CompanyId, string LoanId, string IsStatus)
    {
        DataTable Dt = new DataTable();
        PassDataToSql[] param = new PassDataToSql[4];
        param[0] = new PassDataToSql("@Company_Id", CompanyId, PassDataToSql.ParaTypeList.Int, PassDataToSql.ParaDirectonList.Input);

        param[1] = new PassDataToSql("@Loan_Id", LoanId, PassDataToSql.ParaTypeList.Int, PassDataToSql.ParaDirectonList.Input);

        param[2] = new PassDataToSql("@Is_Status", IsStatus, PassDataToSql.ParaTypeList.Nvarchar, PassDataToSql.ParaDirectonList.Input);
        param[3] = new PassDataToSql("@OpType", "2", PassDataToSql.ParaTypeList.Int, PassDataToSql.ParaDirectonList.Input);

        Dt = da.Reuturn_Datatable_Search("Sp_Pay_Employee_Loan_SelectRow", param);
        return Dt;

    }
    public int DeleteRecord_in_Pay_Employee_Loan(string companyId, string LoanId, string IsStatus, string ModifiedBy, string Modifieddate)
    {

        PassDataToSql[] param = new PassDataToSql[6];
        param[0] = new PassDataToSql("@Company_Id", companyId, PassDataToSql.ParaTypeList.Int, PassDataToSql.ParaDirectonList.Input);
        param[1] = new PassDataToSql("@Loan_Id", LoanId, PassDataToSql.ParaTypeList.Int, PassDataToSql.ParaDirectonList.Input);
        param[2] = new PassDataToSql("@Is_Status", IsStatus, PassDataToSql.ParaTypeList.Nvarchar, PassDataToSql.ParaDirectonList.Input);
        param[3] = new PassDataToSql("@ModifiedBy", ModifiedBy, PassDataToSql.ParaTypeList.Nvarchar, PassDataToSql.ParaDirectonList.Input);
        param[4] = new PassDataToSql("@ModifiedDate", Modifieddate, PassDataToSql.ParaTypeList.DateTime, PassDataToSql.ParaDirectonList.Input);
        param[5] = new PassDataToSql("@ReferenceID", "0", PassDataToSql.ParaTypeList.Int, PassDataToSql.ParaDirectonList.Output);

        //param[0] = new PassDataToSql("@Company_id",CompanyId, PassDataToSql.ParaTypeList.Int, PassDataToSql.ParaDirectonList.Input);
        //param[0] = new PassDataToSql("@Company_id", CompanyId, PassDataToSql.ParaTypeList.Int, PassDataToSql.ParaDirectonList.Input);

        da.execute_Sp("sp_Pay_Employee_Loan_RowStatus", param);
        return Convert.ToInt32(param[5].ParaValue);

    }
    public int UpdateRecord_In_Pay_Employee_Loan(string CompanyId, string LoanId, string Loan_Approval_Date, string LoanDuration, string LoanIntereset, string GrossAmount, string MonthlyInstallment, string IsStatus, string ModifiedBy, string Modifieddate)
    {

        PassDataToSql[] param = new PassDataToSql[11];
        param[0] = new PassDataToSql("@Company_Id", CompanyId, PassDataToSql.ParaTypeList.Int, PassDataToSql.ParaDirectonList.Input);
        param[1] = new PassDataToSql("@Loan_Id", LoanId, PassDataToSql.ParaTypeList.Int, PassDataToSql.ParaDirectonList.Input);
        param[2] = new PassDataToSql("@Loan_Approval_Date", Loan_Approval_Date, PassDataToSql.ParaTypeList.DateTime, PassDataToSql.ParaDirectonList.Input);
        param[3] = new PassDataToSql("@Loan_Duration", LoanDuration, PassDataToSql.ParaTypeList.Int, PassDataToSql.ParaDirectonList.Input);
        param[4] = new PassDataToSql("@Loan_Interest", LoanIntereset, PassDataToSql.ParaTypeList.Float, PassDataToSql.ParaDirectonList.Input);
        param[5] = new PassDataToSql("@Gross_Amount", GrossAmount, PassDataToSql.ParaTypeList.Float, PassDataToSql.ParaDirectonList.Input);
        param[6] = new PassDataToSql("@Monthly_Installment", MonthlyInstallment, PassDataToSql.ParaTypeList.Float, PassDataToSql.ParaDirectonList.Input);

        param[7] = new PassDataToSql("@Is_Status", IsStatus, PassDataToSql.ParaTypeList.Nvarchar, PassDataToSql.ParaDirectonList.Input);
        param[8] = new PassDataToSql("@ModifiedBy", ModifiedBy, PassDataToSql.ParaTypeList.Nvarchar, PassDataToSql.ParaDirectonList.Input);
        param[9] = new PassDataToSql("@ModifiedDate", Modifieddate, PassDataToSql.ParaTypeList.DateTime, PassDataToSql.ParaDirectonList.Input);
        param[10] = new PassDataToSql("@ReferenceID", "0", PassDataToSql.ParaTypeList.Int, PassDataToSql.ParaDirectonList.Output);

        //param[0] = new PassDataToSql("@Company_id",CompanyId, PassDataToSql.ParaTypeList.Int, PassDataToSql.ParaDirectonList.Input);
        //param[0] = new PassDataToSql("@Company_id", CompanyId, PassDataToSql.ParaTypeList.Int, PassDataToSql.ParaDirectonList.Input);

        da.execute_Sp("sp_Pay_Employee_Loan_Update", param);
        return Convert.ToInt32(param[10].ParaValue);
    }

    public int UpdateRecord_loandetials_Amt(string CompanyId, string LoanId, string trnsid, string prvbalnce, string totalamt, string Emppaidamt)
    {

        PassDataToSql[] param = new PassDataToSql[9];
        param[0] = new PassDataToSql("@Company_Id", CompanyId, PassDataToSql.ParaTypeList.Int, PassDataToSql.ParaDirectonList.Input);
        param[1] = new PassDataToSql("@Loan_Id", LoanId, PassDataToSql.ParaTypeList.Int, PassDataToSql.ParaDirectonList.Input);
        param[2] = new PassDataToSql("@Trans_Id", trnsid, PassDataToSql.ParaTypeList.Float, PassDataToSql.ParaDirectonList.Input);
        param[3] = new PassDataToSql("@Prv_Balance", prvbalnce, PassDataToSql.ParaTypeList.Float, PassDataToSql.ParaDirectonList.Input);
        param[4] = new PassDataToSql("@total_amount", totalamt, PassDataToSql.ParaTypeList.Float, PassDataToSql.ParaDirectonList.Input);
        param[5] = new PassDataToSql("@Employee_PaidAm", Emppaidamt, PassDataToSql.ParaTypeList.Float, PassDataToSql.ParaDirectonList.Input);
        param[6] = new PassDataToSql("@Sttus", "Pending", PassDataToSql.ParaTypeList.Nvarchar, PassDataToSql.ParaDirectonList.Input);
        param[7] = new PassDataToSql("@ReferenceID", "0", PassDataToSql.ParaTypeList.Int, PassDataToSql.ParaDirectonList.Output);
        param[8] = new PassDataToSql("@optyp", "1", PassDataToSql.ParaTypeList.Int, PassDataToSql.ParaDirectonList.Input);
        da.execute_Sp("sp_Pay_Employee_LoanDetail_Update", param);
        return Convert.ToInt32(param[8].ParaValue);
    
    }

    public int UpdateRecord_loandetials_Bystaus(string LoanId, string trnsid, string Emppaidamt, string status)
    {

        PassDataToSql[] param = new PassDataToSql[9];
        param[0] = new PassDataToSql("@Company_Id", "0", PassDataToSql.ParaTypeList.Int, PassDataToSql.ParaDirectonList.Input);
        param[1] = new PassDataToSql("@Loan_Id", LoanId, PassDataToSql.ParaTypeList.Int, PassDataToSql.ParaDirectonList.Input);
        param[2] = new PassDataToSql("@Trans_Id", trnsid, PassDataToSql.ParaTypeList.Float, PassDataToSql.ParaDirectonList.Input);
        param[3] = new PassDataToSql("@Prv_Balance", "0", PassDataToSql.ParaTypeList.Float, PassDataToSql.ParaDirectonList.Input);
        param[4] = new PassDataToSql("@total_amount", "0", PassDataToSql.ParaTypeList.Float, PassDataToSql.ParaDirectonList.Input);
        param[5] = new PassDataToSql("@Employee_PaidAm", Emppaidamt, PassDataToSql.ParaTypeList.Float, PassDataToSql.ParaDirectonList.Input);
        param[6] = new PassDataToSql("@Sttus", status, PassDataToSql.ParaTypeList.Nvarchar, PassDataToSql.ParaDirectonList.Input);
        param[7] = new PassDataToSql("@optyp", "2", PassDataToSql.ParaTypeList.Int, PassDataToSql.ParaDirectonList.Input);
        param[8] = new PassDataToSql("@ReferenceID", "0", PassDataToSql.ParaTypeList.Int, PassDataToSql.ParaDirectonList.Output);
       
        
        da.execute_Sp("sp_Pay_Employee_LoanDetail_Update", param);
        return Convert.ToInt32(param[8].ParaValue);

    }
    

    public int Insert_In_Pay_Employee_LoanDetail(string LOanId, string Month, string Year, string PreviousBalance, string MonthlyInstallment, string TotalAmount, string EmployeePaid, string Is_Status, string Field1, string Field2, string Field3, string Field4, string Field5, string Field6, string Field7, string IsActive, string CreatedBy, string CreatedDate, string ModifiedBy, string ModifiedDate)
    {
        PassDataToSql[] param = new PassDataToSql[21];
        param[0] = new PassDataToSql("@Loan_Id", LOanId, PassDataToSql.ParaTypeList.Int, PassDataToSql.ParaDirectonList.Input);

        param[1] = new PassDataToSql("@Month", Month, PassDataToSql.ParaTypeList.Int, PassDataToSql.ParaDirectonList.Input);
        param[2] = new PassDataToSql("@Year", Year, PassDataToSql.ParaTypeList.Int, PassDataToSql.ParaDirectonList.Input);
        param[3] = new PassDataToSql("@Previous_Balance", PreviousBalance, PassDataToSql.ParaTypeList.Float, PassDataToSql.ParaDirectonList.Input);
        param[4] = new PassDataToSql("@Montly_Installment", MonthlyInstallment, PassDataToSql.ParaTypeList.Float, PassDataToSql.ParaDirectonList.Input);
        param[5] = new PassDataToSql("@Total_Amount", TotalAmount, PassDataToSql.ParaTypeList.Float, PassDataToSql.ParaDirectonList.Input);
        param[6] = new PassDataToSql("@Employee_Paid", EmployeePaid, PassDataToSql.ParaTypeList.Float, PassDataToSql.ParaDirectonList.Input);

        param[7] = new PassDataToSql("@Is_Status", Is_Status, PassDataToSql.ParaTypeList.Nvarchar, PassDataToSql.ParaDirectonList.Input);
        param[8] = new PassDataToSql("@Field1", Field1, PassDataToSql.ParaTypeList.Nvarchar, PassDataToSql.ParaDirectonList.Input);
        param[9] = new PassDataToSql("@Field2", Field2, PassDataToSql.ParaTypeList.Nvarchar, PassDataToSql.ParaDirectonList.Input);
        param[10] = new PassDataToSql("@Field3", Field3, PassDataToSql.ParaTypeList.Nvarchar, PassDataToSql.ParaDirectonList.Input);
        param[11] = new PassDataToSql("@Field4", Field4, PassDataToSql.ParaTypeList.Nvarchar, PassDataToSql.ParaDirectonList.Input);
        param[12] = new PassDataToSql("@Field5", Field5, PassDataToSql.ParaTypeList.Nvarchar, PassDataToSql.ParaDirectonList.Input);
        param[13] = new PassDataToSql("@Field6", Field6, PassDataToSql.ParaTypeList.Bit, PassDataToSql.ParaDirectonList.Input);
        param[14] = new PassDataToSql("@Field7", Field7, PassDataToSql.ParaTypeList.DateTime, PassDataToSql.ParaDirectonList.Input);
        param[15] = new PassDataToSql("@IsActive", IsActive, PassDataToSql.ParaTypeList.Bit, PassDataToSql.ParaDirectonList.Input);
        param[16] = new PassDataToSql("@CreatedBy", CreatedBy, PassDataToSql.ParaTypeList.Nvarchar, PassDataToSql.ParaDirectonList.Input);
        param[17] = new PassDataToSql("@CreatedDate", CreatedDate, PassDataToSql.ParaTypeList.DateTime, PassDataToSql.ParaDirectonList.Input);
        param[18] = new PassDataToSql("@ModifiedBy", ModifiedBy, PassDataToSql.ParaTypeList.Nvarchar, PassDataToSql.ParaDirectonList.Input);
        param[19] = new PassDataToSql("@ModifiedDate", ModifiedDate, PassDataToSql.ParaTypeList.DateTime, PassDataToSql.ParaDirectonList.Input);
        param[20] = new PassDataToSql("@ReferenceID", "0", PassDataToSql.ParaTypeList.Int, PassDataToSql.ParaDirectonList.Output);

        //param[0] = new PassDataToSql("@Company_id",CompanyId, PassDataToSql.ParaTypeList.Int, PassDataToSql.ParaDirectonList.Input);
        //param[0] = new PassDataToSql("@Company_id", CompanyId, PassDataToSql.ParaTypeList.Int, PassDataToSql.ParaDirectonList.Input);

        da.execute_Sp("sp_Pay_Employee_LoanDetail_Insert", param);
        return Convert.ToInt32(param[20].ParaValue);

        //return Convert.ToInt32(param[3].ParaValue);

    }
    public DataTable GetRecord_From_PayEmployeeLoanDetail(string LoanId)
    {
        DataTable Dt = new DataTable();
        PassDataToSql[] param = new PassDataToSql[2];
        param[0] = new PassDataToSql("@Loan_Id", LoanId.ToString(), PassDataToSql.ParaTypeList.Int, PassDataToSql.ParaDirectonList.Input);
        param[1] = new PassDataToSql("@optype", "1", PassDataToSql.ParaTypeList.Int, PassDataToSql.ParaDirectonList.Input);
        Dt = da.Reuturn_Datatable_Search("sp_Pay_Employee_LoanDetail_SelectRow", param);
        return Dt;

    }

    public DataTable GetRecord_From_PayEmployeeLoanDetailAll()
    {
        DataTable Dt = new DataTable();
        PassDataToSql[] param = new PassDataToSql[2];
        param[0] = new PassDataToSql("@Loan_Id", "0", PassDataToSql.ParaTypeList.Int, PassDataToSql.ParaDirectonList.Input);
        param[1] = new PassDataToSql("@optype", "2", PassDataToSql.ParaTypeList.Int, PassDataToSql.ParaDirectonList.Input);
        Dt = da.Reuturn_Datatable_Search("sp_Pay_Employee_LoanDetail_SelectRow", param);
        return Dt;

    }


    public Pay_Employee_Loan()
    {
        //
        // TODO: Add constructor logic here
        //
    }
}
