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
/// Summary description for CompanyMaster
/// </summary>
public class CompanyMaster
{
    DataAccessClass daClass = new DataAccessClass();
	public CompanyMaster()
	{
		//
		// TODO: Add constructor logic here
		//
	}



    public int InsertCompanyMaster(string ECompanyName, string LCompanyName, string CompanyCode, string Logo_Path, string Parent_Company_Id, string Emp_Id, string Currency_Id, string Country_Id, string Commerical_License_No, string Field1, string Field2, string Field3, string Field4, string Field5, string Field6, string Field7, string IsActive, string CreatedBy, string CreatedDate, string ModifiedBy, string ModifiedDate)
    {

        PassDataToSql[] paramList = new PassDataToSql[22];
      

        paramList[0] = new PassDataToSql("@Company_Name", ECompanyName, PassDataToSql.ParaTypeList.Nvarchar, PassDataToSql.ParaDirectonList.Input);
        paramList[1] = new PassDataToSql("@Company_Name_L", LCompanyName, PassDataToSql.ParaTypeList.Nvarchar, PassDataToSql.ParaDirectonList.Input);
        paramList[2] = new PassDataToSql("@Company_Code", CompanyCode, PassDataToSql.ParaTypeList.Nvarchar, PassDataToSql.ParaDirectonList.Input);
        paramList[3] = new PassDataToSql("@Logo_Path", Logo_Path, PassDataToSql.ParaTypeList.Nvarchar, PassDataToSql.ParaDirectonList.Input);
        paramList[4] = new PassDataToSql("@Parent_Company_Id", Parent_Company_Id, PassDataToSql.ParaTypeList.Int, PassDataToSql.ParaDirectonList.Input);
        paramList[5] = new PassDataToSql("@Emp_Id", Emp_Id, PassDataToSql.ParaTypeList.Int, PassDataToSql.ParaDirectonList.Input);
        paramList[6] = new PassDataToSql("@Currency_Id", Currency_Id, PassDataToSql.ParaTypeList.Int, PassDataToSql.ParaDirectonList.Input);
        paramList[7] = new PassDataToSql("@Country_Id", Country_Id, PassDataToSql.ParaTypeList.Int, PassDataToSql.ParaDirectonList.Input);
        paramList[8] = new PassDataToSql("@Commerical_License_No",Commerical_License_No, PassDataToSql.ParaTypeList.Nvarchar, PassDataToSql.ParaDirectonList.Input);
        paramList[9] = new PassDataToSql("@Field1", Field1, PassDataToSql.ParaTypeList.Nvarchar, PassDataToSql.ParaDirectonList.Input);
        paramList[10] = new PassDataToSql("@Field2", Field2, PassDataToSql.ParaTypeList.Nvarchar, PassDataToSql.ParaDirectonList.Input);
        paramList[11] = new PassDataToSql("@Field3", Field3, PassDataToSql.ParaTypeList.Nvarchar, PassDataToSql.ParaDirectonList.Input);
        paramList[12] = new PassDataToSql("@Field4", Field4, PassDataToSql.ParaTypeList.Nvarchar, PassDataToSql.ParaDirectonList.Input);
        paramList[13] = new PassDataToSql("@Field5", Field5, PassDataToSql.ParaTypeList.Nvarchar, PassDataToSql.ParaDirectonList.Input);
        paramList[14] = new PassDataToSql("@Field6", Field6, PassDataToSql.ParaTypeList.Bit, PassDataToSql.ParaDirectonList.Input);
        paramList[15] = new PassDataToSql("@Field7", Field7, PassDataToSql.ParaTypeList.Date, PassDataToSql.ParaDirectonList.Input);
        paramList[16] = new PassDataToSql("@IsActive", IsActive, PassDataToSql.ParaTypeList.Bit, PassDataToSql.ParaDirectonList.Input);
        paramList[17] = new PassDataToSql("@CreatedBy", CreatedBy, PassDataToSql.ParaTypeList.Nvarchar, PassDataToSql.ParaDirectonList.Input);
        paramList[18] = new PassDataToSql("@CreatedDate", CreatedDate, PassDataToSql.ParaTypeList.Date, PassDataToSql.ParaDirectonList.Input);
        paramList[19] = new PassDataToSql("@ModifiedBy", ModifiedBy, PassDataToSql.ParaTypeList.Nvarchar, PassDataToSql.ParaDirectonList.Input);
        paramList[20] = new PassDataToSql("@ModifiedDate", ModifiedDate, PassDataToSql.ParaTypeList.Date, PassDataToSql.ParaDirectonList.Input);
        paramList[21] = new PassDataToSql("@ReferenceID", "0", PassDataToSql.ParaTypeList.Int, PassDataToSql.ParaDirectonList.Output);

        daClass.execute_Sp("sp_Set_CompanyMaster_Insert", paramList);
        return Convert.ToInt32(paramList[21].ParaValue);
    }

    public int UpdateCompanyMaster(string CompanyId,string ECompanyName, string LCompanyName, string CompanyCode, string Logo_Path, string Parent_Company_Id, string Emp_Id, string Currency_Id, string Country_Id, string Commerical_License_No, string Field1, string Field2, string Field3, string Field4, string Field5, string Field6, string Field7, string IsActive, string ModifiedBy, string ModifiedDate)
    {

        PassDataToSql[] paramList = new PassDataToSql[21];

        paramList[0] = new PassDataToSql("@Company_Id",CompanyId, PassDataToSql.ParaTypeList.Int, PassDataToSql.ParaDirectonList.Input);
        paramList[1] = new PassDataToSql("@Company_Name", ECompanyName, PassDataToSql.ParaTypeList.Nvarchar, PassDataToSql.ParaDirectonList.Input);
        paramList[2] = new PassDataToSql("@Company_Name_L", LCompanyName, PassDataToSql.ParaTypeList.Nvarchar, PassDataToSql.ParaDirectonList.Input);
        paramList[3] = new PassDataToSql("@Company_Code", CompanyCode, PassDataToSql.ParaTypeList.Nvarchar, PassDataToSql.ParaDirectonList.Input);
        paramList[4] = new PassDataToSql("@Logo_Path", Logo_Path, PassDataToSql.ParaTypeList.Nvarchar, PassDataToSql.ParaDirectonList.Input);
        paramList[5] = new PassDataToSql("@Parent_Company_Id", Parent_Company_Id, PassDataToSql.ParaTypeList.Int, PassDataToSql.ParaDirectonList.Input);
        paramList[6] = new PassDataToSql("@Emp_Id", Emp_Id, PassDataToSql.ParaTypeList.Int, PassDataToSql.ParaDirectonList.Input);
        paramList[7] = new PassDataToSql("@Currency_Id", Currency_Id, PassDataToSql.ParaTypeList.Int, PassDataToSql.ParaDirectonList.Input);
        paramList[8] = new PassDataToSql("@Country_Id", Country_Id, PassDataToSql.ParaTypeList.Int, PassDataToSql.ParaDirectonList.Input);
        paramList[9] = new PassDataToSql("@Commerical_License_No", Commerical_License_No, PassDataToSql.ParaTypeList.Nvarchar, PassDataToSql.ParaDirectonList.Input);
     
        paramList[10] = new PassDataToSql("@Field1", Field1, PassDataToSql.ParaTypeList.Nvarchar, PassDataToSql.ParaDirectonList.Input);
        paramList[11] = new PassDataToSql("@Field2", Field2, PassDataToSql.ParaTypeList.Nvarchar, PassDataToSql.ParaDirectonList.Input);
        paramList[12] = new PassDataToSql("@Field3", Field3, PassDataToSql.ParaTypeList.Nvarchar, PassDataToSql.ParaDirectonList.Input);
        paramList[13] = new PassDataToSql("@Field4", Field4, PassDataToSql.ParaTypeList.Nvarchar, PassDataToSql.ParaDirectonList.Input);
        paramList[14] = new PassDataToSql("@Field5", Field5, PassDataToSql.ParaTypeList.Nvarchar, PassDataToSql.ParaDirectonList.Input);
        paramList[15] = new PassDataToSql("@Field6", Field6, PassDataToSql.ParaTypeList.Bit, PassDataToSql.ParaDirectonList.Input);
        paramList[16] = new PassDataToSql("@Field7", Field7, PassDataToSql.ParaTypeList.Date, PassDataToSql.ParaDirectonList.Input);


        paramList[17] = new PassDataToSql("@IsActive", IsActive, PassDataToSql.ParaTypeList.Bit, PassDataToSql.ParaDirectonList.Input);
        paramList[18] = new PassDataToSql("@ModifiedBy", ModifiedBy, PassDataToSql.ParaTypeList.Nvarchar, PassDataToSql.ParaDirectonList.Input);
        paramList[19] = new PassDataToSql("@ModifiedDate", ModifiedDate, PassDataToSql.ParaTypeList.Date, PassDataToSql.ParaDirectonList.Input);
        paramList[20] = new PassDataToSql("@ReferenceID", "0", PassDataToSql.ParaTypeList.Int, PassDataToSql.ParaDirectonList.Output);

        daClass.execute_Sp("sp_Set_CompanyMaster_Update", paramList);
        return Convert.ToInt32(paramList[20].ParaValue);
    }

    public int DeleteCompanyMaster(string CompanyId,string IsActive, string ModifiedBy, string ModifiedDate)
    {

        PassDataToSql[] paramList = new PassDataToSql[5];
         paramList[0] = new PassDataToSql("@Company_Id", CompanyId, PassDataToSql.ParaTypeList.Int, PassDataToSql.ParaDirectonList.Input);
        paramList[1] = new PassDataToSql("@IsActive", IsActive, PassDataToSql.ParaTypeList.Bit, PassDataToSql.ParaDirectonList.Input);
        paramList[2] = new PassDataToSql("@ModifiedBy", ModifiedBy, PassDataToSql.ParaTypeList.Nvarchar, PassDataToSql.ParaDirectonList.Input);
        paramList[3] = new PassDataToSql("@ModifiedDate", ModifiedDate, PassDataToSql.ParaTypeList.Date, PassDataToSql.ParaDirectonList.Input);
        paramList[4] = new PassDataToSql("@ReferenceID", "0", PassDataToSql.ParaTypeList.Int, PassDataToSql.ParaDirectonList.Output);
        daClass.execute_Sp("sp_Set_CompanyMaster_RowStatus", paramList);
        return Convert.ToInt32(paramList[4].ParaValue);
    }
    public DataTable GetCompanyMasterById(string CompanyId)
    {

        DataTable dtInfo = new DataTable();
        PassDataToSql[] paramList = new PassDataToSql[3];
        paramList[0] = new PassDataToSql("@Company_Id", CompanyId, PassDataToSql.ParaTypeList.Nvarchar, PassDataToSql.ParaDirectonList.Input);
        paramList[1] = new PassDataToSql("@Company_Name", "", PassDataToSql.ParaTypeList.Nvarchar, PassDataToSql.ParaDirectonList.Input);
        paramList[2] = new PassDataToSql("@optype", "2", PassDataToSql.ParaTypeList.Nvarchar, PassDataToSql.ParaDirectonList.Input);
        dtInfo = daClass.Reuturn_Datatable_Search("sp_Set_CompanyMaster_SelectRow", paramList);

        return dtInfo;
    }
    public DataTable GetCompanyMaster()
    {

        DataTable dtInfo = new DataTable();
        PassDataToSql[] paramList = new PassDataToSql[3];
        paramList[0] = new PassDataToSql("@Company_Id","0", PassDataToSql.ParaTypeList.Int, PassDataToSql.ParaDirectonList.Input);
        paramList[1] = new PassDataToSql("@Company_Name", "", PassDataToSql.ParaTypeList.Nvarchar, PassDataToSql.ParaDirectonList.Input);
        paramList[2] = new PassDataToSql("@optype", "1", PassDataToSql.ParaTypeList.Nvarchar, PassDataToSql.ParaDirectonList.Input);
        dtInfo = daClass.Reuturn_Datatable_Search("sp_Set_CompanyMaster_SelectRow", paramList);

        return dtInfo;
    }
    public string GetMaxCompanyId()
    {
        string CompanyId="";
        DataTable dtInfo = new DataTable();
        PassDataToSql[] paramList = new PassDataToSql[3];
        paramList[0] = new PassDataToSql("@Company_Id", "0", PassDataToSql.ParaTypeList.Int, PassDataToSql.ParaDirectonList.Input);
        paramList[1] = new PassDataToSql("@Company_Name", "", PassDataToSql.ParaTypeList.Nvarchar, PassDataToSql.ParaDirectonList.Input);
        paramList[2] = new PassDataToSql("@optype", "5", PassDataToSql.ParaTypeList.Nvarchar, PassDataToSql.ParaDirectonList.Input);
        dtInfo = daClass.Reuturn_Datatable_Search("sp_Set_CompanyMaster_SelectRow", paramList);

       if(dtInfo.Rows.Count > 0)
       {
           CompanyId=dtInfo.Rows[0][0].ToString();

       }

        return CompanyId;
    }

    public DataTable GetCompanyMasterInactive()
    {
        DataTable dtInfo = new DataTable();
        PassDataToSql[] paramList = new PassDataToSql[3];
        paramList[0] = new PassDataToSql("@Company_Id","0", PassDataToSql.ParaTypeList.Int, PassDataToSql.ParaDirectonList.Input);
        paramList[1] = new PassDataToSql("@Company_Name", "", PassDataToSql.ParaTypeList.Nvarchar, PassDataToSql.ParaDirectonList.Input);
        paramList[2] = new PassDataToSql("@optype", "3", PassDataToSql.ParaTypeList.Nvarchar, PassDataToSql.ParaDirectonList.Input);
        dtInfo = daClass.Reuturn_Datatable_Search("sp_Set_CompanyMaster_SelectRow", paramList);

        return dtInfo;
    }
    public DataTable GetCompanyMasterByCompanyName(string strCompanyName)
    {
        DataTable dtInfo = new DataTable();
        PassDataToSql[] paramList = new PassDataToSql[3];
        paramList[0] = new PassDataToSql("@Company_Id", "0", PassDataToSql.ParaTypeList.Int, PassDataToSql.ParaDirectonList.Input);
        paramList[1] = new PassDataToSql("@Company_Name",strCompanyName, PassDataToSql.ParaTypeList.Nvarchar, PassDataToSql.ParaDirectonList.Input);
        paramList[2] = new PassDataToSql("@optype", "4", PassDataToSql.ParaTypeList.Nvarchar, PassDataToSql.ParaDirectonList.Input);
        dtInfo = daClass.Reuturn_Datatable_Search("sp_Set_CompanyMaster_SelectRow", paramList);

        return dtInfo;
    }
   

}
