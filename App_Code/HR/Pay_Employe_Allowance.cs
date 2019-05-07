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
/// Summary description for Pay_Employe_Allowance
/// </summary>
public class Pay_Employe_Allowance
{
    DataAccessClass daClass = new DataAccessClass();

	public Pay_Employe_Allowance()
	{
		//
		// TODO: Add constructor logic here
		//
	}


    

    public int InsertPayrollEmpAllowance(string strEmp_Id, string strMonth,string strYear,string strRef_Id, string strAllowanceId, string strAllowanceType,string strAllowancValue, string strActallowanceValue)
     {
        PassDataToSql[] paramList = new PassDataToSql[9];
        paramList[0] = new PassDataToSql("@Emp_Id", strEmp_Id, PassDataToSql.ParaTypeList.Int, PassDataToSql.ParaDirectonList.Input);
        paramList[1] = new PassDataToSql("@Month", strMonth, PassDataToSql.ParaTypeList.Int, PassDataToSql.ParaDirectonList.Input);
        paramList[2] = new PassDataToSql("@Year", strYear, PassDataToSql.ParaTypeList.Int, PassDataToSql.ParaDirectonList.Input);
        paramList[3] = new PassDataToSql("@Ref_Id", strRef_Id, PassDataToSql.ParaTypeList.Int, PassDataToSql.ParaDirectonList.Input);
        paramList[4] = new PassDataToSql("@Allowance_Id",strAllowanceId, PassDataToSql.ParaTypeList.Int, PassDataToSql.ParaDirectonList.Input);
        paramList[5] = new PassDataToSql("@Allowance_Type", strAllowanceType, PassDataToSql.ParaTypeList.Int, PassDataToSql.ParaDirectonList.Input);
        paramList[6] = new PassDataToSql("@Allowance_Value", strAllowancValue, PassDataToSql.ParaTypeList.Float, PassDataToSql.ParaDirectonList.Input);
        paramList[7] = new PassDataToSql("@Act_Allowance_Value", strActallowanceValue, PassDataToSql.ParaTypeList.Float, PassDataToSql.ParaDirectonList.Input);
        paramList[8] = new PassDataToSql("@ReferenceID", "0", PassDataToSql.ParaTypeList.Int, PassDataToSql.ParaDirectonList.Output);
        daClass.execute_Sp("sp_Pay_Employe_Allowance_Temp_Insert", paramList);
        return Convert.ToInt32(paramList[8].ParaValue);

}

    // post payroll records inserted into Pay Emp Allowance Table
    public int InsertPostPayrollAllowance(string Emp_Id, string Month, string Year, string Ref_Id, string AllowanceId, string AllowanceType, string AllowancValue, string ActallowanceValue, string postdate, string field1, string field2, string field3, string field4, string field5, string field6, string field7, string field8, string field9, string field10, string strIsActive, string strCreatedBy, string strCreatedDate, string strModifiedBy, string strModifiedDate)
    {
        PassDataToSql[] paramList = new PassDataToSql[25];
        paramList[0] = new PassDataToSql("@Emp_Id", Emp_Id, PassDataToSql.ParaTypeList.Int, PassDataToSql.ParaDirectonList.Input);
        paramList[1] = new PassDataToSql("@Month", Month, PassDataToSql.ParaTypeList.Int, PassDataToSql.ParaDirectonList.Input);
        paramList[2] = new PassDataToSql("@Year", Year, PassDataToSql.ParaTypeList.Int, PassDataToSql.ParaDirectonList.Input);
        paramList[3] = new PassDataToSql("@Ref_Id", Ref_Id, PassDataToSql.ParaTypeList.Int, PassDataToSql.ParaDirectonList.Input);
        paramList[4] = new PassDataToSql("@Allowance_Id",AllowanceId, PassDataToSql.ParaTypeList.Int, PassDataToSql.ParaDirectonList.Input);
        paramList[5] = new PassDataToSql("@Allowance_Type", Getdata(AllowanceType), PassDataToSql.ParaTypeList.Int, PassDataToSql.ParaDirectonList.Input);
        paramList[6] = new PassDataToSql("@Allowance_Value", Getdata(AllowancValue), PassDataToSql.ParaTypeList.Float, PassDataToSql.ParaDirectonList.Input);
        paramList[7] = new PassDataToSql("@Act_Allowance_Value", Getdata(ActallowanceValue), PassDataToSql.ParaTypeList.Float, PassDataToSql.ParaDirectonList.Input);
        paramList[8] = new PassDataToSql("@Posted_Date", postdate, PassDataToSql.ParaTypeList.DateTime, PassDataToSql.ParaDirectonList.Input);

        paramList[9] = new PassDataToSql("@Field1", field1, PassDataToSql.ParaTypeList.Nvarchar, PassDataToSql.ParaDirectonList.Input);
        paramList[10] = new PassDataToSql("@Field2", field2, PassDataToSql.ParaTypeList.Nvarchar, PassDataToSql.ParaDirectonList.Input);
        paramList[11] = new PassDataToSql("@Field3", field3, PassDataToSql.ParaTypeList.Nvarchar, PassDataToSql.ParaDirectonList.Input);
        paramList[12] = new PassDataToSql("@Field4", field4, PassDataToSql.ParaTypeList.Nvarchar, PassDataToSql.ParaDirectonList.Input);
        paramList[13] = new PassDataToSql("@Field5", field5, PassDataToSql.ParaTypeList.Nvarchar, PassDataToSql.ParaDirectonList.Input);
        paramList[14] = new PassDataToSql("@Field6", field6, PassDataToSql.ParaTypeList.Nvarchar, PassDataToSql.ParaDirectonList.Input);
        paramList[15] = new PassDataToSql("@Field7", field7, PassDataToSql.ParaTypeList.Nvarchar, PassDataToSql.ParaDirectonList.Input);
        paramList[16] = new PassDataToSql("@Field8", field8, PassDataToSql.ParaTypeList.Nvarchar, PassDataToSql.ParaDirectonList.Input);
        paramList[17] = new PassDataToSql("@Field9", field9, PassDataToSql.ParaTypeList.Nvarchar, PassDataToSql.ParaDirectonList.Input);
        paramList[18] = new PassDataToSql("@Field10", field10, PassDataToSql.ParaTypeList.Nvarchar, PassDataToSql.ParaDirectonList.Input);


        paramList[19] = new PassDataToSql("@IsActive", strIsActive, PassDataToSql.ParaTypeList.Bit, PassDataToSql.ParaDirectonList.Input);
        paramList[20] = new PassDataToSql("@CreatedBy", strCreatedBy, PassDataToSql.ParaTypeList.Nvarchar, PassDataToSql.ParaDirectonList.Input);
        paramList[21] = new PassDataToSql("@CreatedDate", strCreatedDate, PassDataToSql.ParaTypeList.Date, PassDataToSql.ParaDirectonList.Input);
        paramList[22] = new PassDataToSql("@ModifiedBy", strModifiedBy, PassDataToSql.ParaTypeList.Nvarchar, PassDataToSql.ParaDirectonList.Input);
        paramList[23] = new PassDataToSql("@ModifiedDate", strModifiedDate, PassDataToSql.ParaTypeList.Date, PassDataToSql.ParaDirectonList.Input);
        
        
        paramList[24] = new PassDataToSql("@ReferenceID", "0", PassDataToSql.ParaTypeList.Int, PassDataToSql.ParaDirectonList.Output);
        daClass.execute_Sp("sp_Pay_Employe_Allowance_Insert", paramList);
        return Convert.ToInt32(paramList[24].ParaValue);
    }

    //Get all posted records from Pay Emp Allowance Table

    public DataTable GetPostedAllowanceAll(string Empid,string month,string year)
    {
        DataTable dtInfo = new DataTable();
        PassDataToSql[] paramList = new PassDataToSql[4];
        paramList[0] = new PassDataToSql("@Emp_Id", Empid, PassDataToSql.ParaTypeList.Int, PassDataToSql.ParaDirectonList.Input);
        paramList[1] = new PassDataToSql("@Month", month, PassDataToSql.ParaTypeList.Int, PassDataToSql.ParaDirectonList.Input);
        paramList[2] = new PassDataToSql("@Year", year, PassDataToSql.ParaTypeList.Int, PassDataToSql.ParaDirectonList.Input);
        paramList[3] = new PassDataToSql("@optype", "1", PassDataToSql.ParaTypeList.Nvarchar, PassDataToSql.ParaDirectonList.Input);

        dtInfo = daClass.Reuturn_Datatable_Search("sp_Pay_Employe_Allowance_Select_Row", paramList);
        return dtInfo;
    }


    public int DeletePayAllowance(string strEmpId,string strMonth,string strYear)
    {
        PassDataToSql[] paramList = new PassDataToSql[4];
        paramList[0] = new PassDataToSql("@Emp_Id", strEmpId, PassDataToSql.ParaTypeList.Int, PassDataToSql.ParaDirectonList.Input);
        paramList[1] = new PassDataToSql("@Month", strMonth, PassDataToSql.ParaTypeList.Int, PassDataToSql.ParaDirectonList.Input);
        paramList[2] = new PassDataToSql("@Year", strYear, PassDataToSql.ParaTypeList.Int, PassDataToSql.ParaDirectonList.Input);
        paramList[3] = new PassDataToSql("@ReferenceID", "0", PassDataToSql.ParaTypeList.Int, PassDataToSql.ParaDirectonList.Output);

        daClass.execute_Sp("sp_Pay_Employe_Allowance_Temp_Delete", paramList);
        return Convert.ToInt32(paramList[3].ParaValue);
    }
    public DataTable GetPayAllowPayaRoll(string strEmp_Id)
    {
        DataTable dtInfo = new DataTable();
        PassDataToSql[] paramList = new PassDataToSql[2];
        paramList[0] = new PassDataToSql("@Emp_Id", strEmp_Id, PassDataToSql.ParaTypeList.Int, PassDataToSql.ParaDirectonList.Input);
        paramList[1] = new PassDataToSql("@Trans_Id", "0", PassDataToSql.ParaTypeList.Int, PassDataToSql.ParaDirectonList.Input);
        
        //paramList[4] = new PassDataToSql("@optype", "1", PassDataToSql.ParaTypeList.Nvarchar, PassDataToSql.ParaDirectonList.Input);
        daClass = new DataAccessClass();
        dtInfo = daClass.Reuturn_Datatable_Search("sp_Pay_Employe_Allowance_Temp_Select_Row", paramList);
        return dtInfo;
    }
    //public int UpdateRecordPayEmpAllowance(string TransId, string EmpId, string Ref_Id, string Month, string Year, string AllowanceId, string Deductype, string Allowancevalue, string ActAllowanceValue)
    //{

    //    PassDataToSql[] param = new PassDataToSql[10];
    //    param[0] = new PassDataToSql("@Trans_Id", TransId, PassDataToSql.ParaTypeList.Int, PassDataToSql.ParaDirectonList.Input);
    //    param[1] = new PassDataToSql("@Emp_Id", EmpId, PassDataToSql.ParaTypeList.Int, PassDataToSql.ParaDirectonList.Input);
    //    param[2] = new PassDataToSql("@Ref_Id", Ref_Id, PassDataToSql.ParaTypeList.Int, PassDataToSql.ParaDirectonList.Input);
    //    param[3] = new PassDataToSql("@Month", Month, PassDataToSql.ParaTypeList.Int, PassDataToSql.ParaDirectonList.Input);
    //    param[4] = new PassDataToSql("@Year", Year, PassDataToSql.ParaTypeList.Int, PassDataToSql.ParaDirectonList.Input);
    //    param[5] = new PassDataToSql("@Allowance_Id", AllowanceId, PassDataToSql.ParaTypeList.Int, PassDataToSql.ParaDirectonList.Input);
    //    param[6] = new PassDataToSql("@Allowance_Type", Deductype, PassDataToSql.ParaTypeList.Float, PassDataToSql.ParaDirectonList.Input);
    //    param[7] = new PassDataToSql("@Allowance_Value", Allowancevalue, PassDataToSql.ParaTypeList.Int, PassDataToSql.ParaDirectonList.Input);
    //    param[8] = new PassDataToSql("@Act_Allowance_Value", ActAllowanceValue, PassDataToSql.ParaTypeList.Nvarchar, PassDataToSql.ParaDirectonList.Input);
    //    param[9] = new PassDataToSql("@ReferenceID", "0", PassDataToSql.ParaTypeList.Int, PassDataToSql.ParaDirectonList.Output);
    //    daClass.execute_Sp("Sp_Pay_Employe_Allowance_Temp_Update", param);
    //    return Convert.ToInt32(param[9].ParaValue);

    //}


    public DataTable GetPayAllowancePostedAll(string strEmp_Id)
    {
        DataTable dtInfo = new DataTable();
        PassDataToSql[] paramList = new PassDataToSql[4];
        paramList[0] = new PassDataToSql("@Emp_Id", strEmp_Id, PassDataToSql.ParaTypeList.Int, PassDataToSql.ParaDirectonList.Input);

        paramList[1] = new PassDataToSql("@Month", "0", PassDataToSql.ParaTypeList.Int, PassDataToSql.ParaDirectonList.Input);
        paramList[2] = new PassDataToSql("@Year", "0", PassDataToSql.ParaTypeList.Int, PassDataToSql.ParaDirectonList.Input);

        paramList[3] = new PassDataToSql("@optype", "2", PassDataToSql.ParaTypeList.Nvarchar, PassDataToSql.ParaDirectonList.Input);
        daClass = new DataAccessClass();
        dtInfo = daClass.Reuturn_Datatable_Search("sp_Pay_Employe_Allowance_Select_Row", paramList);
        return dtInfo;
    }

    public int UpdatePayEMpAllowActVale(string TransId, string EmpId, string ActAllowanceValue)
    {
        PassDataToSql[] param = new PassDataToSql[4];
        param[0] = new PassDataToSql("@Trans_Id", TransId, PassDataToSql.ParaTypeList.Int, PassDataToSql.ParaDirectonList.Input);
        param[1] = new PassDataToSql("@Emp_Id", EmpId, PassDataToSql.ParaTypeList.Int, PassDataToSql.ParaDirectonList.Input);
        param[2] = new PassDataToSql("@Act_Allowance_Value", ActAllowanceValue, PassDataToSql.ParaTypeList.Float, PassDataToSql.ParaDirectonList.Input);
        param[3] = new PassDataToSql("@ReferenceID", "0", PassDataToSql.ParaTypeList.Int, PassDataToSql.ParaDirectonList.Output);

        daClass.execute_Sp("Sp_Pay_Employe_Allowance_Temp_Update", param);
        return Convert.ToInt32(param[3].ParaValue);
    
    }


    public string Getdata(string type)
    {
        if (type == "")
        {
            type = "0";

        }
        return type;
    }
}
