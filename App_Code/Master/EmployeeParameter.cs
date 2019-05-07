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
/// Summary description for EmployeeParameter
/// </summary>
public class EmployeeParameter
{
    DataAccessClass daClass = new DataAccessClass();
	public EmployeeParameter()
	{
		//
		// TODO: Add constructor logic here
		//
	}

    public void DeleteEmployeeParameterByEmpId(string empid)
    {


        PassDataToSql[] paramList = new PassDataToSql[1];
        paramList[0] = new PassDataToSql("@Emp_Id", empid, PassDataToSql.ParaTypeList.Int, PassDataToSql.ParaDirectonList.Input);

        daClass.execute_Sp("sp_Set_Employee_Parameter_Delete", paramList);



    }

    public string GetEmployeeParameterByParameterName(string EmpId,string ParamName)
    {
        string ParamValue = string.Empty;
        DataTable dtInfo = new DataTable();
        PassDataToSql[] paramList = new PassDataToSql[3];
        paramList[0] = new PassDataToSql("@Company_Id","0", PassDataToSql.ParaTypeList.Int, PassDataToSql.ParaDirectonList.Input);

        paramList[1] = new PassDataToSql("@Emp_Id", EmpId, PassDataToSql.ParaTypeList.Int, PassDataToSql.ParaDirectonList.Input);

        paramList[2] = new PassDataToSql("@optype", "3", PassDataToSql.ParaTypeList.Nvarchar, PassDataToSql.ParaDirectonList.Input);
        dtInfo = daClass.Reuturn_Datatable_Search("sp_Set_Employee_Parameter_SelectRow", paramList);

        if(dtInfo.Rows.Count > 0)
        {

            ParamValue=dtInfo.Rows[0][ParamName].ToString();

        }


        return ParamValue;
    }

    public DataTable GetEmployeeParameterByEmpId(string EmpId, string CompanyId)
    {
        DataTable dtInfo = new DataTable();
        PassDataToSql[] paramList = new PassDataToSql[3];
         paramList[0] = new PassDataToSql("@Company_Id", CompanyId, PassDataToSql.ParaTypeList.Int, PassDataToSql.ParaDirectonList.Input);

        paramList[1] = new PassDataToSql("@Emp_Id", EmpId, PassDataToSql.ParaTypeList.Int, PassDataToSql.ParaDirectonList.Input);

        paramList[2] = new PassDataToSql("@optype", "2", PassDataToSql.ParaTypeList.Nvarchar, PassDataToSql.ParaDirectonList.Input);
        dtInfo = daClass.Reuturn_Datatable_Search("sp_Set_Employee_Parameter_SelectRow", paramList);

        return dtInfo;
    }
    public DataTable GetEmployeeParameter(string CompanyId)
    {
        DataTable dtInfo = new DataTable();
        PassDataToSql[] paramList = new PassDataToSql[3];
        paramList[0] = new PassDataToSql("@Company_Id", CompanyId, PassDataToSql.ParaTypeList.Int, PassDataToSql.ParaDirectonList.Input);

        paramList[1] = new PassDataToSql("@Emp_Id","", PassDataToSql.ParaTypeList.Nvarchar, PassDataToSql.ParaDirectonList.Input);

        paramList[2] = new PassDataToSql("@optype", "2", PassDataToSql.ParaTypeList.Nvarchar, PassDataToSql.ParaDirectonList.Input);
        dtInfo = daClass.Reuturn_Datatable_Search("sp_Set_Employee_Parameter_SelectRow", paramList);

        return dtInfo;
    }
    public int InsertEmployeeParameter(string CompanyId,string 	Emp_Id	 ,string Basic_Salary	 ,string Salary_Type	 ,string Currency_Id ,string Assign_Min	 ,string Effective_Work_Cal_Method ,string Is_OverTime ,string Normal_OT_Method ,string Normal_OT_Type	 ,string Normal_OT_Value ,string Normal_HOT_Type ,string Normal_HOT_Value	 ,string Normal_WOT_Type ,string Normal_WOT_Value ,string Is_Partial_Enable ,string Partial_Leave_Mins ,string Partial_Leave_Day ,string Is_Partial_Carry ,string 	Field1 ,string Field2 ,string Field3 ,string Field4 ,string Field5 ,string Field6 ,string Field7,string IsActive, string createdby, string createddate, string ModifiedBy, string ModifiedDate)
    {

        PassDataToSql[] paramList = new PassDataToSql[32];


        paramList[0] = new PassDataToSql("@Emp_Id", Emp_Id, PassDataToSql.ParaTypeList.Int, PassDataToSql.ParaDirectonList.Input);
        paramList[1] = new PassDataToSql("@Basic_Salary", Basic_Salary, PassDataToSql.ParaTypeList.Float, PassDataToSql.ParaDirectonList.Input);
        paramList[2] = new PassDataToSql("@Salary_Type", Salary_Type, PassDataToSql.ParaTypeList.Nvarchar, PassDataToSql.ParaDirectonList.Input);
        paramList[3] = new PassDataToSql("@Currency_Id", Currency_Id, PassDataToSql.ParaTypeList.Int, PassDataToSql.ParaDirectonList.Input);
        paramList[4] = new PassDataToSql("@Assign_Min", Assign_Min, PassDataToSql.ParaTypeList.Int, PassDataToSql.ParaDirectonList.Input);
        paramList[5] = new PassDataToSql("@Effective_Work_Cal_Method", Effective_Work_Cal_Method, PassDataToSql.ParaTypeList.Nvarchar, PassDataToSql.ParaDirectonList.Input);
        paramList[6] = new PassDataToSql("@Is_OverTime", Is_OverTime, PassDataToSql.ParaTypeList.Bit, PassDataToSql.ParaDirectonList.Input);
        paramList[7] = new PassDataToSql("@Normal_OT_Method", Normal_OT_Method, PassDataToSql.ParaTypeList.Nvarchar, PassDataToSql.ParaDirectonList.Input);
        paramList[8] = new PassDataToSql("@Normal_OT_Type", Normal_OT_Type, PassDataToSql.ParaTypeList.Int, PassDataToSql.ParaDirectonList.Input);
        paramList[9] = new PassDataToSql("@Normal_OT_Value", Normal_OT_Value, PassDataToSql.ParaTypeList.Float, PassDataToSql.ParaDirectonList.Input);
        paramList[10] = new PassDataToSql("@Normal_HOT_Type", Normal_HOT_Type, PassDataToSql.ParaTypeList.Int, PassDataToSql.ParaDirectonList.Input);
        paramList[11] = new PassDataToSql("@Normal_HOT_Value", Normal_HOT_Value, PassDataToSql.ParaTypeList.Float, PassDataToSql.ParaDirectonList.Input);
        paramList[12] = new PassDataToSql("@Normal_WOT_Type", Normal_WOT_Type, PassDataToSql.ParaTypeList.Int, PassDataToSql.ParaDirectonList.Input);
        paramList[13] = new PassDataToSql("@Normal_WOT_Value", Normal_WOT_Value, PassDataToSql.ParaTypeList.Float, PassDataToSql.ParaDirectonList.Input);
        paramList[14] = new PassDataToSql("@Is_Partial_Enable", Is_Partial_Enable, PassDataToSql.ParaTypeList.Bit, PassDataToSql.ParaDirectonList.Input);
        paramList[15] = new PassDataToSql("@Partial_Leave_Mins", Partial_Leave_Mins, PassDataToSql.ParaTypeList.Int, PassDataToSql.ParaDirectonList.Input);
        paramList[16] = new PassDataToSql("@Partial_Leave_Day", Partial_Leave_Day, PassDataToSql.ParaTypeList.Int, PassDataToSql.ParaDirectonList.Input);
        paramList[17] = new PassDataToSql("@Is_Partial_Carry", Is_Partial_Carry, PassDataToSql.ParaTypeList.Bit, PassDataToSql.ParaDirectonList.Input);
        paramList[18] = new PassDataToSql("@Field1", Field1, PassDataToSql.ParaTypeList.Nvarchar, PassDataToSql.ParaDirectonList.Input);

        paramList[19] = new PassDataToSql("@Field2", Field2, PassDataToSql.ParaTypeList.Nvarchar, PassDataToSql.ParaDirectonList.Input);
        paramList[20] = new PassDataToSql("@Field3", Field3, PassDataToSql.ParaTypeList.Nvarchar, PassDataToSql.ParaDirectonList.Input);
        paramList[21] = new PassDataToSql("@Field4", Field4, PassDataToSql.ParaTypeList.Nvarchar, PassDataToSql.ParaDirectonList.Input);
        paramList[22] = new PassDataToSql("@Field5", Field5, PassDataToSql.ParaTypeList.Nvarchar, PassDataToSql.ParaDirectonList.Input);
        paramList[23] = new PassDataToSql("@Field6", Field6, PassDataToSql.ParaTypeList.Bit, PassDataToSql.ParaDirectonList.Input);
        paramList[24] = new PassDataToSql("@Field7", Field7, PassDataToSql.ParaTypeList.Date, PassDataToSql.ParaDirectonList.Input);

        paramList[25] = new PassDataToSql("@IsActive", IsActive, PassDataToSql.ParaTypeList.Bit, PassDataToSql.ParaDirectonList.Input);
        paramList[26] = new PassDataToSql("@Company_Id", CompanyId, PassDataToSql.ParaTypeList.Int, PassDataToSql.ParaDirectonList.Input);
        paramList[27] = new PassDataToSql("@CreatedBy", ModifiedBy, PassDataToSql.ParaTypeList.Nvarchar, PassDataToSql.ParaDirectonList.Input);
        paramList[28] = new PassDataToSql("@CreatedDate", ModifiedDate, PassDataToSql.ParaTypeList.Date, PassDataToSql.ParaDirectonList.Input);

        paramList[29] = new PassDataToSql("@ModifiedBy", ModifiedBy, PassDataToSql.ParaTypeList.Nvarchar, PassDataToSql.ParaDirectonList.Input);
        paramList[30] = new PassDataToSql("@ModifiedDate", ModifiedDate, PassDataToSql.ParaTypeList.Date, PassDataToSql.ParaDirectonList.Input);
        paramList[31] = new PassDataToSql("@ReferenceID", "0", PassDataToSql.ParaTypeList.Int, PassDataToSql.ParaDirectonList.Output);

        daClass.execute_Sp("sp_Set_Employee_Parameter_Insert", paramList);
        return Convert.ToInt32(paramList[31].ParaValue);
    }






    public int InsertEmployeeParameterOnEmployeeInsert(string CompanyId, string Emp_Id, string ModifiedBy, string ModifiedDate)
    {
        Set_ApplicationParameter objAppParam = new Set_ApplicationParameter();
        PassDataToSql[] paramList = new PassDataToSql[32];

         string Basic_Salary="0";
         string Salary_Type = "Montly";
        string Currency_Id = "1"; 
        string Assign_Min = string.Empty;
        try
        {
            Assign_Min = objAppParam.GetApplicationParameterValueByParamName("Work Day Min", CompanyId);
        }
        catch
        {
            Assign_Min = "540";
        }
        string Effective_Work_Cal_Method =string.Empty;
        try
        {
             Effective_Work_Cal_Method = objAppParam.GetApplicationParameterValueByParamName("Effective Work Calculation Method", CompanyId);
        }
        catch
        {
            Effective_Work_Cal_Method = "InOut";
        }
        
        string Is_OverTime = string.Empty;
        try
        {
             Is_OverTime = Convert.ToBoolean(objAppParam.GetApplicationParameterValueByParamName("IsOverTime", CompanyId)).ToString();
        }
        catch
        {
            Is_OverTime = false.ToString();
        }
        string Normal_OT_Method = string.Empty;
        try
        {

             Normal_OT_Method = objAppParam.GetApplicationParameterValueByParamName("Over Time Calculation Method", CompanyId);
        }
        catch
        {
            Normal_OT_Method = "Work Hour";
        }
        string Normal_OT_Type = "2";
        string Normal_OT_Value = "100";
        string Normal_HOT_Type = "2";
        string Normal_HOT_Value = "100";
        string Normal_WOT_Type = "2";
        string Normal_WOT_Value = "100";
        string Is_Partial_Enable = string.Empty;

        try
        {
            Is_Partial_Enable = Convert.ToBoolean(objAppParam.GetApplicationParameterValueByParamName("Partial_Leave_Enable", CompanyId)).ToString();
        }
        catch
        {
            Is_Partial_Enable=false.ToString();
        }
        string Partial_Leave_Mins = string.Empty;

        try
        {
            Partial_Leave_Mins = objAppParam.GetApplicationParameterValueByParamName("Total Partial Leave Minutes", CompanyId);

        }
        catch
        {
            Partial_Leave_Mins = "240";
        }


        string Partial_Leave_Day = string.Empty;


        try
        {
            Partial_Leave_Day = objAppParam.GetApplicationParameterValueByParamName("Partial Leave Minute Use In A Day", CompanyId);
        }
        catch
        {
            Partial_Leave_Day = "60";
        }

        string Is_Partial_Carry = false.ToString(); 
        string Field1 = string.Empty;
        try
        {
            Field1 = Convert.ToBoolean(objAppParam.GetApplicationParameterValueByParamName("Is_Late_Penalty", CompanyId)).ToString();
        }
        catch
        {
            Field1 = false.ToString();
        }



        string Field2 = string.Empty;
        try
        {
            Field2 = Convert.ToBoolean(objAppParam.GetApplicationParameterValueByParamName("Is_Early_Penalty", CompanyId)).ToString();
        }
        catch
        {
            Field2 = false.ToString();
        }
        string Field3 = string.Empty;
        
            Field3 = false.ToString();

            string Field4 = false.ToString(); 
        string Field5 = false.ToString(); 
        string Field6 = false.ToString();
        string Field7 = DateTime.Now.ToString(); 

       

        paramList[0] = new PassDataToSql("@Emp_Id", Emp_Id, PassDataToSql.ParaTypeList.Int, PassDataToSql.ParaDirectonList.Input);
        paramList[1] = new PassDataToSql("@Basic_Salary", Basic_Salary, PassDataToSql.ParaTypeList.Float, PassDataToSql.ParaDirectonList.Input);
        paramList[2] = new PassDataToSql("@Salary_Type", Salary_Type, PassDataToSql.ParaTypeList.Nvarchar, PassDataToSql.ParaDirectonList.Input);
        paramList[3] = new PassDataToSql("@Currency_Id", Currency_Id, PassDataToSql.ParaTypeList.Int, PassDataToSql.ParaDirectonList.Input);
        paramList[4] = new PassDataToSql("@Assign_Min", Assign_Min, PassDataToSql.ParaTypeList.Int, PassDataToSql.ParaDirectonList.Input);
        paramList[5] = new PassDataToSql("@Effective_Work_Cal_Method", Effective_Work_Cal_Method, PassDataToSql.ParaTypeList.Nvarchar, PassDataToSql.ParaDirectonList.Input);
        paramList[6] = new PassDataToSql("@Is_OverTime", Is_OverTime, PassDataToSql.ParaTypeList.Bit, PassDataToSql.ParaDirectonList.Input);
        paramList[7] = new PassDataToSql("@Normal_OT_Method", Normal_OT_Method, PassDataToSql.ParaTypeList.Nvarchar, PassDataToSql.ParaDirectonList.Input);
        paramList[8] = new PassDataToSql("@Normal_OT_Type", Normal_OT_Type, PassDataToSql.ParaTypeList.Int, PassDataToSql.ParaDirectonList.Input);
        paramList[9] = new PassDataToSql("@Normal_OT_Value", Normal_OT_Value, PassDataToSql.ParaTypeList.Float, PassDataToSql.ParaDirectonList.Input);
        paramList[10] = new PassDataToSql("@Normal_HOT_Type", Normal_HOT_Type, PassDataToSql.ParaTypeList.Int, PassDataToSql.ParaDirectonList.Input);
        paramList[11] = new PassDataToSql("@Normal_HOT_Value", Normal_HOT_Value, PassDataToSql.ParaTypeList.Float, PassDataToSql.ParaDirectonList.Input);
        paramList[12] = new PassDataToSql("@Normal_WOT_Type", Normal_WOT_Type, PassDataToSql.ParaTypeList.Int, PassDataToSql.ParaDirectonList.Input);
        paramList[13] = new PassDataToSql("@Normal_WOT_Value", Normal_WOT_Value, PassDataToSql.ParaTypeList.Float, PassDataToSql.ParaDirectonList.Input);
        paramList[14] = new PassDataToSql("@Is_Partial_Enable", Is_Partial_Enable, PassDataToSql.ParaTypeList.Bit, PassDataToSql.ParaDirectonList.Input);
        paramList[15] = new PassDataToSql("@Partial_Leave_Mins", Partial_Leave_Mins, PassDataToSql.ParaTypeList.Int, PassDataToSql.ParaDirectonList.Input);
        paramList[16] = new PassDataToSql("@Partial_Leave_Day", Partial_Leave_Day, PassDataToSql.ParaTypeList.Int, PassDataToSql.ParaDirectonList.Input);
        paramList[17] = new PassDataToSql("@Is_Partial_Carry", Is_Partial_Carry, PassDataToSql.ParaTypeList.Bit, PassDataToSql.ParaDirectonList.Input);
        paramList[18] = new PassDataToSql("@Field1", Field1, PassDataToSql.ParaTypeList.Nvarchar, PassDataToSql.ParaDirectonList.Input);

        paramList[19] = new PassDataToSql("@Field2", Field2, PassDataToSql.ParaTypeList.Nvarchar, PassDataToSql.ParaDirectonList.Input);
        paramList[20] = new PassDataToSql("@Field3", Field3, PassDataToSql.ParaTypeList.Nvarchar, PassDataToSql.ParaDirectonList.Input);
        paramList[21] = new PassDataToSql("@Field4", Field4, PassDataToSql.ParaTypeList.Nvarchar, PassDataToSql.ParaDirectonList.Input);
        paramList[22] = new PassDataToSql("@Field5", Field5, PassDataToSql.ParaTypeList.Nvarchar, PassDataToSql.ParaDirectonList.Input);
        paramList[23] = new PassDataToSql("@Field6", Field6, PassDataToSql.ParaTypeList.Bit, PassDataToSql.ParaDirectonList.Input);
        paramList[24] = new PassDataToSql("@Field7", Field7, PassDataToSql.ParaTypeList.Date, PassDataToSql.ParaDirectonList.Input);

        paramList[25] = new PassDataToSql("@IsActive", true.ToString(), PassDataToSql.ParaTypeList.Bit, PassDataToSql.ParaDirectonList.Input);
        paramList[26] = new PassDataToSql("@Company_Id", CompanyId, PassDataToSql.ParaTypeList.Int, PassDataToSql.ParaDirectonList.Input);
        paramList[27] = new PassDataToSql("@CreatedBy", ModifiedBy, PassDataToSql.ParaTypeList.Nvarchar, PassDataToSql.ParaDirectonList.Input);
        paramList[28] = new PassDataToSql("@CreatedDate", ModifiedDate, PassDataToSql.ParaTypeList.Date, PassDataToSql.ParaDirectonList.Input);

        paramList[29] = new PassDataToSql("@ModifiedBy", ModifiedBy, PassDataToSql.ParaTypeList.Nvarchar, PassDataToSql.ParaDirectonList.Input);
        paramList[30] = new PassDataToSql("@ModifiedDate", ModifiedDate, PassDataToSql.ParaTypeList.Date, PassDataToSql.ParaDirectonList.Input);
        paramList[31] = new PassDataToSql("@ReferenceID", "0", PassDataToSql.ParaTypeList.Int, PassDataToSql.ParaDirectonList.Output);

        daClass.execute_Sp("sp_Set_Employee_Parameter_Insert", paramList);
        return Convert.ToInt32(paramList[31].ParaValue);
    }


    public int UpdateEmployeeParameter(string CompanyId, string Emp_Id, string Basic_Salary, string Salary_Type, string Currency_Id, string Assign_Min, string Effective_Work_Cal_Method, string Is_OverTime, string Normal_OT_Method, string Normal_OT_Type, string Normal_OT_Value, string Normal_HOT_Type, string Normal_HOT_Value, string Normal_WOT_Type, string Normal_WOT_Value, string Is_Partial_Enable, string Partial_Leave_Mins, string Partial_Leave_Day, string Is_Partial_Carry, string Field1, string Field2, string Field3, string Field4, string Field5, string Field6, string Field7, string IsActive, string createdby, string createddate, string ModifiedBy, string ModifiedDate)
    {

        PassDataToSql[] paramList = new PassDataToSql[30];


        paramList[0] = new PassDataToSql("@Emp_Id", Emp_Id, PassDataToSql.ParaTypeList.Int, PassDataToSql.ParaDirectonList.Input);
        paramList[1] = new PassDataToSql("@Basic_Salary", Basic_Salary, PassDataToSql.ParaTypeList.Float, PassDataToSql.ParaDirectonList.Input);
        paramList[2] = new PassDataToSql("@Salary_Type", Salary_Type, PassDataToSql.ParaTypeList.Nvarchar, PassDataToSql.ParaDirectonList.Input);
        paramList[3] = new PassDataToSql("@Currency_Id", Currency_Id, PassDataToSql.ParaTypeList.Int, PassDataToSql.ParaDirectonList.Input);
        paramList[4] = new PassDataToSql("@Assign_Min", Assign_Min, PassDataToSql.ParaTypeList.Int, PassDataToSql.ParaDirectonList.Input);
        paramList[5] = new PassDataToSql("@Effective_Work_Cal_Method", Effective_Work_Cal_Method, PassDataToSql.ParaTypeList.Nvarchar, PassDataToSql.ParaDirectonList.Input);
        paramList[6] = new PassDataToSql("@Is_OverTime", Is_OverTime, PassDataToSql.ParaTypeList.Bit, PassDataToSql.ParaDirectonList.Input);
        paramList[7] = new PassDataToSql("@Normal_OT_Method", Normal_OT_Method, PassDataToSql.ParaTypeList.Nvarchar, PassDataToSql.ParaDirectonList.Input);
        paramList[8] = new PassDataToSql("@Normal_OT_Type", Normal_OT_Type, PassDataToSql.ParaTypeList.Int, PassDataToSql.ParaDirectonList.Input);
        paramList[9] = new PassDataToSql("@Normal_OT_Value", Normal_OT_Value, PassDataToSql.ParaTypeList.Float, PassDataToSql.ParaDirectonList.Input);
        paramList[10] = new PassDataToSql("@Normal_HOT_Type", Normal_HOT_Type, PassDataToSql.ParaTypeList.Int, PassDataToSql.ParaDirectonList.Input);
        paramList[11] = new PassDataToSql("@Normal_HOT_Value", Normal_HOT_Value, PassDataToSql.ParaTypeList.Float, PassDataToSql.ParaDirectonList.Input);
        paramList[12] = new PassDataToSql("@Normal_WOT_Type", Normal_WOT_Type, PassDataToSql.ParaTypeList.Int, PassDataToSql.ParaDirectonList.Input);
        paramList[13] = new PassDataToSql("@Normal_WOT_Value", Normal_WOT_Value, PassDataToSql.ParaTypeList.Float, PassDataToSql.ParaDirectonList.Input);
        paramList[14] = new PassDataToSql("@Is_Partial_Enable", Is_Partial_Enable, PassDataToSql.ParaTypeList.Bit, PassDataToSql.ParaDirectonList.Input);
        paramList[15] = new PassDataToSql("@Partial_Leave_Mins", Partial_Leave_Mins, PassDataToSql.ParaTypeList.Int, PassDataToSql.ParaDirectonList.Input);
        paramList[16] = new PassDataToSql("@Partial_Leave_Day", Partial_Leave_Day, PassDataToSql.ParaTypeList.Int, PassDataToSql.ParaDirectonList.Input);
        paramList[17] = new PassDataToSql("@Is_Partial_Carry", Is_Partial_Carry, PassDataToSql.ParaTypeList.Bit, PassDataToSql.ParaDirectonList.Input);
        paramList[18] = new PassDataToSql("@Field1", Field1, PassDataToSql.ParaTypeList.Nvarchar, PassDataToSql.ParaDirectonList.Input);

        paramList[19] = new PassDataToSql("@Field2", Field2, PassDataToSql.ParaTypeList.Nvarchar, PassDataToSql.ParaDirectonList.Input);
        paramList[20] = new PassDataToSql("@Field3", Field3, PassDataToSql.ParaTypeList.Nvarchar, PassDataToSql.ParaDirectonList.Input);
        paramList[21] = new PassDataToSql("@Field4", Field4, PassDataToSql.ParaTypeList.Nvarchar, PassDataToSql.ParaDirectonList.Input);
        paramList[22] = new PassDataToSql("@Field5", Field5, PassDataToSql.ParaTypeList.Nvarchar, PassDataToSql.ParaDirectonList.Input);
        paramList[23] = new PassDataToSql("@Field6", Field6, PassDataToSql.ParaTypeList.Bit, PassDataToSql.ParaDirectonList.Input);
        paramList[24] = new PassDataToSql("@Field7", Field7, PassDataToSql.ParaTypeList.Date, PassDataToSql.ParaDirectonList.Input);

        paramList[25] = new PassDataToSql("@IsActive", IsActive, PassDataToSql.ParaTypeList.Bit, PassDataToSql.ParaDirectonList.Input);
        paramList[26] = new PassDataToSql("@Company_Id", CompanyId, PassDataToSql.ParaTypeList.Int, PassDataToSql.ParaDirectonList.Input);
      
        paramList[27] = new PassDataToSql("@ModifiedBy", ModifiedBy, PassDataToSql.ParaTypeList.Nvarchar, PassDataToSql.ParaDirectonList.Input);
        paramList[28] = new PassDataToSql("@ModifiedDate", ModifiedDate, PassDataToSql.ParaTypeList.Date, PassDataToSql.ParaDirectonList.Input);
        paramList[29] = new PassDataToSql("@ReferenceID", "0", PassDataToSql.ParaTypeList.Int, PassDataToSql.ParaDirectonList.Output);

        daClass.execute_Sp("sp_Set_Employee_Parameter_Update", paramList);
        return Convert.ToInt32(paramList[29].ParaValue);
    }
}
