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
/// Summary description for Pay_Employee_Due_Payment
/// </summary>
public class Pay_Employee_Due_Payment
{
    DataAccessClass da = new DataAccessClass();

    public int Insert_In_Pay_Employee_Loan(string CompanyId, string EmployeeId, string month, string year, string type,string Amount,string date, string description,string Is_conclude, string Field1, string Field2, string Field3, string Field4, string Field5, string Field6, string Field7, string IsActive, string CreatedBy, string CreatedDate, string ModifiedBy, string ModifiedDate)
    {
        PassDataToSql[] param = new PassDataToSql[22];
        param[0] = new PassDataToSql("@Company_Id", CompanyId, PassDataToSql.ParaTypeList.Int, PassDataToSql.ParaDirectonList.Input);

        param[1] = new PassDataToSql("@Emp_Id", EmployeeId, PassDataToSql.ParaTypeList.Int, PassDataToSql.ParaDirectonList.Input);
        param[2] = new PassDataToSql("@Month", month, PassDataToSql.ParaTypeList.Int, PassDataToSql.ParaDirectonList.Input);
        param[3] = new PassDataToSql("@Year", year, PassDataToSql.ParaTypeList.Int, PassDataToSql.ParaDirectonList.Input);
        param[4] = new PassDataToSql("@Type", type, PassDataToSql.ParaTypeList.Int, PassDataToSql.ParaDirectonList.Input);
        param[5] = new PassDataToSql("@Amount", Amount, PassDataToSql.ParaTypeList.Float, PassDataToSql.ParaDirectonList.Input);
        param[6] = new PassDataToSql("@Date", date, PassDataToSql.ParaTypeList.DateTime, PassDataToSql.ParaDirectonList.Input);
        param[7] = new PassDataToSql("@descrption", description, PassDataToSql.ParaTypeList.Nvarchar, PassDataToSql.ParaDirectonList.Input);
        param[8] = new PassDataToSql("@Is_Conclude", Is_conclude, PassDataToSql.ParaTypeList.Bit, PassDataToSql.ParaDirectonList.Input);
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

        da.execute_Sp("sp_Pay_Employee_Due_Payment_Temp_Insert", param);
        return Convert.ToInt32(param[21].ParaValue);

        //return Convert.ToInt32(param[3].ParaValue);

    }

    public DataTable GetRecord_Emp_Due_paymentType1(string Empid, string Month, string Year)
    {
        DataTable Dt = new DataTable();
        PassDataToSql[] param = new PassDataToSql[5];
        param[0] = new PassDataToSql("@Trans_Id", "0", PassDataToSql.ParaTypeList.Int, PassDataToSql.ParaDirectonList.Input);
        param[1] = new PassDataToSql("@Emp_Id", Empid, PassDataToSql.ParaTypeList.Int, PassDataToSql.ParaDirectonList.Input);
        param[2] = new PassDataToSql("@Month", Month, PassDataToSql.ParaTypeList.Int, PassDataToSql.ParaDirectonList.Input);
        param[3] = new PassDataToSql("@year", Year, PassDataToSql.ParaTypeList.Int, PassDataToSql.ParaDirectonList.Input);
        param[4] = new PassDataToSql("@Type", "1", PassDataToSql.ParaTypeList.Int, PassDataToSql.ParaDirectonList.Input);

        Dt = da.Reuturn_Datatable_Search("sp_Pay_Employee_Due_Payment_Temp_Select_Row", param);
        return Dt;

    }
    public DataTable GetRecord_Emp_Due_paymentByType2(string Empid, string Month, string Year)
    {
        DataTable Dt = new DataTable();
        PassDataToSql[] param = new PassDataToSql[5];
        param[0] = new PassDataToSql("@Trans_Id", "0", PassDataToSql.ParaTypeList.Int, PassDataToSql.ParaDirectonList.Input);
        param[1] = new PassDataToSql("@Emp_Id", Empid, PassDataToSql.ParaTypeList.Int, PassDataToSql.ParaDirectonList.Input);
        param[2] = new PassDataToSql("@Month", Month, PassDataToSql.ParaTypeList.Int, PassDataToSql.ParaDirectonList.Input);
        param[3] = new PassDataToSql("@year", Year, PassDataToSql.ParaTypeList.Int, PassDataToSql.ParaDirectonList.Input);
        param[4] = new PassDataToSql("@Type", "2", PassDataToSql.ParaTypeList.Int, PassDataToSql.ParaDirectonList.Input);

        Dt = da.Reuturn_Datatable_Search("sp_Pay_Employee_Due_Payment_Temp_Select_Row", param);
        return Dt;
    }

    public DataTable GetAllRecord_Emp_Due_paymentByEmpId(string Empid)
    {
        DataTable Dt = new DataTable();
        PassDataToSql[] param = new PassDataToSql[5];
        param[0] = new PassDataToSql("@Trans_Id", "0", PassDataToSql.ParaTypeList.Int, PassDataToSql.ParaDirectonList.Input);
        param[1] = new PassDataToSql("@Emp_Id", Empid, PassDataToSql.ParaTypeList.Int, PassDataToSql.ParaDirectonList.Input);
        param[2] = new PassDataToSql("@Month", "0", PassDataToSql.ParaTypeList.Int, PassDataToSql.ParaDirectonList.Input);
        param[3] = new PassDataToSql("@year", "0", PassDataToSql.ParaTypeList.Int, PassDataToSql.ParaDirectonList.Input);
        param[4] = new PassDataToSql("@Type", "3", PassDataToSql.ParaTypeList.Int, PassDataToSql.ParaDirectonList.Input);

        Dt = da.Reuturn_Datatable_Search("sp_Pay_Employee_Due_Payment_Temp_Select_Row", param);
        return Dt;
    }



    public DataTable GetFalseAllRecord_Emp_Due_paymentByEmpId(string Empid)
    {
        DataTable Dt = new DataTable();
        PassDataToSql[] param = new PassDataToSql[5];
        param[0] = new PassDataToSql("@Trans_Id", "0", PassDataToSql.ParaTypeList.Int, PassDataToSql.ParaDirectonList.Input);
        param[1] = new PassDataToSql("@Emp_Id", Empid, PassDataToSql.ParaTypeList.Int, PassDataToSql.ParaDirectonList.Input);
        param[2] = new PassDataToSql("@Month", "0", PassDataToSql.ParaTypeList.Int, PassDataToSql.ParaDirectonList.Input);
        param[3] = new PassDataToSql("@year", "0", PassDataToSql.ParaTypeList.Int, PassDataToSql.ParaDirectonList.Input);
        param[4] = new PassDataToSql("@Type", "4", PassDataToSql.ParaTypeList.Int, PassDataToSql.ParaDirectonList.Input);

        Dt = da.Reuturn_Datatable_Search("sp_Pay_Employee_Due_Payment_Temp_Select_Row", param);
        return Dt;
    }

    public DataTable GetAllRecord_ByEmpId(string Empid)
    {
        DataTable Dt = new DataTable();
        PassDataToSql[] param = new PassDataToSql[5];
        param[0] = new PassDataToSql("@Trans_Id", "0", PassDataToSql.ParaTypeList.Int, PassDataToSql.ParaDirectonList.Input);
        param[1] = new PassDataToSql("@Emp_Id", Empid, PassDataToSql.ParaTypeList.Int, PassDataToSql.ParaDirectonList.Input);
        param[2] = new PassDataToSql("@Month", "0", PassDataToSql.ParaTypeList.Int, PassDataToSql.ParaDirectonList.Input);
        param[3] = new PassDataToSql("@year", "0", PassDataToSql.ParaTypeList.Int, PassDataToSql.ParaDirectonList.Input);
        param[4] = new PassDataToSql("@Type", "4", PassDataToSql.ParaTypeList.Int, PassDataToSql.ParaDirectonList.Input);

        Dt = da.Reuturn_Datatable_Search("sp_Pay_Employee_Due_Payment_Temp_Select_Row", param);
        return Dt;
    }

    public DataTable GetAllfalseRecord_ByEmpId(string Empid)
    {
        DataTable Dt = new DataTable();
        PassDataToSql[] param = new PassDataToSql[5];
        param[0] = new PassDataToSql("@Trans_Id", "0", PassDataToSql.ParaTypeList.Int, PassDataToSql.ParaDirectonList.Input);
        param[1] = new PassDataToSql("@Emp_Id", Empid, PassDataToSql.ParaTypeList.Int, PassDataToSql.ParaDirectonList.Input);
        param[2] = new PassDataToSql("@Month", "0", PassDataToSql.ParaTypeList.Int, PassDataToSql.ParaDirectonList.Input);
        param[3] = new PassDataToSql("@year", "0", PassDataToSql.ParaTypeList.Int, PassDataToSql.ParaDirectonList.Input);
        param[4] = new PassDataToSql("@Type", "5", PassDataToSql.ParaTypeList.Int, PassDataToSql.ParaDirectonList.Input);

        Dt = da.Reuturn_Datatable_Search("sp_Pay_Employee_Due_Payment_Temp_Select_Row", param);
        return Dt;
    }



	public Pay_Employee_Due_Payment()
	{
		//
		// TODO: Add constructor logic here
		//
	}
}
