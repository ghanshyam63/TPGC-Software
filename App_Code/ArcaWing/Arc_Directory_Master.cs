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
using System.Data.SqlClient;
using PegasusDataAccess;

/// <summary>
/// Summary description for Arc_Directory_Master
/// </summary>
public class Arc_Directory_Master
{
    DataAccessClass da = new DataAccessClass();

    public int InsertDirectorymaster(string CompanyId, string DirectoryName,  string Field1, string Field2, string Field3, string Field4, string Field5, string Field6, string Field7, string IsActive, string CreatedBy, string CreatedDate, string ModifiedBy, string ModifiedDate)
    {
        PassDataToSql[] param = new PassDataToSql[15];
        param[0] = new PassDataToSql("@Company_Id", CompanyId, PassDataToSql.ParaTypeList.Int, PassDataToSql.ParaDirectonList.Input);

        param[1] = new PassDataToSql("@Directory_Name", DirectoryName, PassDataToSql.ParaTypeList.Nvarchar, PassDataToSql.ParaDirectonList.Input);
       
        param[2] = new PassDataToSql("@Field1", Field1, PassDataToSql.ParaTypeList.Nvarchar, PassDataToSql.ParaDirectonList.Input);
        param[3] = new PassDataToSql("@Field2", Field2, PassDataToSql.ParaTypeList.Nvarchar, PassDataToSql.ParaDirectonList.Input);
        param[4] = new PassDataToSql("@Field3", Field3, PassDataToSql.ParaTypeList.Nvarchar, PassDataToSql.ParaDirectonList.Input);
        param[5] = new PassDataToSql("@Field4", Field4, PassDataToSql.ParaTypeList.Nvarchar, PassDataToSql.ParaDirectonList.Input);
        param[6] = new PassDataToSql("@Field5", Field5, PassDataToSql.ParaTypeList.Nvarchar, PassDataToSql.ParaDirectonList.Input);
        param[7] = new PassDataToSql("@Field6", Field6, PassDataToSql.ParaTypeList.Bit, PassDataToSql.ParaDirectonList.Input);
        param[8] = new PassDataToSql("@Field7", Field7, PassDataToSql.ParaTypeList.DateTime, PassDataToSql.ParaDirectonList.Input);
        param[9] = new PassDataToSql("@IsActive", IsActive, PassDataToSql.ParaTypeList.Bit, PassDataToSql.ParaDirectonList.Input);
        param[10] = new PassDataToSql("@CreatedBy", CreatedBy, PassDataToSql.ParaTypeList.Nvarchar, PassDataToSql.ParaDirectonList.Input);
        param[11] = new PassDataToSql("@CreatedDate", CreatedDate, PassDataToSql.ParaTypeList.DateTime, PassDataToSql.ParaDirectonList.Input);
        param[12] = new PassDataToSql("@ModifiedBy", ModifiedBy, PassDataToSql.ParaTypeList.Nvarchar, PassDataToSql.ParaDirectonList.Input);
        param[13] = new PassDataToSql("@ModifiedDate", ModifiedDate, PassDataToSql.ParaTypeList.DateTime, PassDataToSql.ParaDirectonList.Input);
        param[14] = new PassDataToSql("@ReferenceID", "0", PassDataToSql.ParaTypeList.Int, PassDataToSql.ParaDirectonList.Output);

        //param[0] = new PassDataToSql("@Company_id",CompanyId, PassDataToSql.ParaTypeList.Int, PassDataToSql.ParaDirectonList.Input);
        //param[0] = new PassDataToSql("@Company_id", CompanyId, PassDataToSql.ParaTypeList.Int, PassDataToSql.ParaDirectonList.Input);

        da.execute_Sp("Sp_Arc_Directory_Creation_Insert", param);
        return Convert.ToInt32(param[14].ParaValue);

        //return Convert.ToInt32(param[3].ParaValue);

    }
    public int UpdateDirectoryMaster(string CompanyId, string DirectoryId, string DirectoryName, string Field1, string Field2, string Field3, string Field4, string Field5, string Field6, string Field7, string IsActive, string ModifiedBy, string ModifiedDate)
    {

        PassDataToSql[] param = new PassDataToSql[14];
        param[0] = new PassDataToSql("@Company_Id", CompanyId, PassDataToSql.ParaTypeList.Int, PassDataToSql.ParaDirectonList.Input);
        param[1] = new PassDataToSql("@Id",DirectoryId, PassDataToSql.ParaTypeList.Int, PassDataToSql.ParaDirectonList.Input);
        param[2] = new PassDataToSql("@Directory_Name",DirectoryName, PassDataToSql.ParaTypeList.Nvarchar, PassDataToSql.ParaDirectonList.Input);
        param[3] = new PassDataToSql("@Field1", Field1, PassDataToSql.ParaTypeList.Nvarchar, PassDataToSql.ParaDirectonList.Input);
        param[4] = new PassDataToSql("@Field2", Field2, PassDataToSql.ParaTypeList.Nvarchar, PassDataToSql.ParaDirectonList.Input);
        param[5] = new PassDataToSql("@Field3", Field3, PassDataToSql.ParaTypeList.Nvarchar, PassDataToSql.ParaDirectonList.Input);
        param[6] = new PassDataToSql("@Field4", Field4, PassDataToSql.ParaTypeList.Nvarchar, PassDataToSql.ParaDirectonList.Input);
        param[7] = new PassDataToSql("@Field5", Field5, PassDataToSql.ParaTypeList.Nvarchar, PassDataToSql.ParaDirectonList.Input);
        param[8] = new PassDataToSql("@Field6", Field6, PassDataToSql.ParaTypeList.Bit, PassDataToSql.ParaDirectonList.Input);
        param[9] = new PassDataToSql("@Field7", Field7, PassDataToSql.ParaTypeList.Date, PassDataToSql.ParaDirectonList.Input);
        param[10] = new PassDataToSql("@IsActive", IsActive, PassDataToSql.ParaTypeList.Bit, PassDataToSql.ParaDirectonList.Input);
        param[11] = new PassDataToSql("@ModifiedBy", ModifiedBy, PassDataToSql.ParaTypeList.Nvarchar, PassDataToSql.ParaDirectonList.Input);
        param[12] = new PassDataToSql("@ModifiedDate", ModifiedDate, PassDataToSql.ParaTypeList.Nvarchar, PassDataToSql.ParaDirectonList.Input);
        param[13] = new PassDataToSql("@ReferenceID", "0", PassDataToSql.ParaTypeList.Int, PassDataToSql.ParaDirectonList.Output);

        da.execute_Sp("Sp_Arc_Directory_Creation_Update", param);

        return Convert.ToInt32(param[13].ParaValue);
    }
    public int DeleteDirectoryMaster(string CompanyId, string DirectoryId, string IsActive, string ModifiedBy, string ModifiedDate)
    {
        PassDataToSql[] param = new PassDataToSql[6];
        param[0] = new PassDataToSql("@Company_Id",CompanyId, PassDataToSql.ParaTypeList.Int, PassDataToSql.ParaDirectonList.Input);
        param[1] = new PassDataToSql("@id",DirectoryId, PassDataToSql.ParaTypeList.Int, PassDataToSql.ParaDirectonList.Input);
        param[2] = new PassDataToSql("@IsActive", IsActive, PassDataToSql.ParaTypeList.Bit, PassDataToSql.ParaDirectonList.Input);
        param[3] = new PassDataToSql("@ModifiedBy", ModifiedBy, PassDataToSql.ParaTypeList.Nvarchar, PassDataToSql.ParaDirectonList.Input);
        param[4] = new PassDataToSql("@ModifiedDate", ModifiedDate, PassDataToSql.ParaTypeList.Nvarchar, PassDataToSql.ParaDirectonList.Input);
        param[5] = new PassDataToSql("@ReferenceID", "0", PassDataToSql.ParaTypeList.Int, PassDataToSql.ParaDirectonList.Output);

        da.execute_Sp("Sp_Arc_Directory_Creation_RowStatus", param);

        return Convert.ToInt32(param[5].ParaValue);
    }
    public DataTable getDirectoryMaster(string CompanyId, string Directoryid)
    {
        DataTable dt = new DataTable();
        PassDataToSql[] paramlist = new PassDataToSql[4];

        paramlist[0] = new PassDataToSql("@Company_Id", CompanyId, PassDataToSql.ParaTypeList.Int, PassDataToSql.ParaDirectonList.Input);
        paramlist[1] = new PassDataToSql("@id",Directoryid, PassDataToSql.ParaTypeList.Int, PassDataToSql.ParaDirectonList.Input);
        paramlist[2] = new PassDataToSql("@Directory_Name", "", PassDataToSql.ParaTypeList.Nvarchar, PassDataToSql.ParaDirectonList.Input);
        paramlist[3] = new PassDataToSql("@Optype", "2", PassDataToSql.ParaTypeList.Int, PassDataToSql.ParaDirectonList.Input);

        dt = da.Reuturn_Datatable_Search("Sp_Arc_Directory_Creation_SelectRow", paramlist);
        return dt;

    }
    public DataTable GetDirectoryMasterInActive(string Compnayid, string DirectoryId)
    {
        DataTable dt = new DataTable();
        PassDataToSql[] paramlist = new PassDataToSql[4];

        paramlist[0] = new PassDataToSql("@Company_Id", Compnayid, PassDataToSql.ParaTypeList.Int, PassDataToSql.ParaDirectonList.Input);
        paramlist[1] = new PassDataToSql("@id", DirectoryId, PassDataToSql.ParaTypeList.Int, PassDataToSql.ParaDirectonList.Input);
        paramlist[2] = new PassDataToSql("@Directory_Name", "", PassDataToSql.ParaTypeList.Nvarchar, PassDataToSql.ParaDirectonList.Input);
        paramlist[3] = new PassDataToSql("@Optype", "3", PassDataToSql.ParaTypeList.Int, PassDataToSql.ParaDirectonList.Input);

        dt = da.Reuturn_Datatable_Search("Sp_Arc_Directory_Creation_SelectRow", paramlist);
        return dt;


    }
    public DataTable GetDirectoryMaster_By_DirectoryId(string CompanyId, string DirectoryId)
    {
        DataTable dt = new DataTable();
        PassDataToSql[] paramlist = new PassDataToSql[4];

        paramlist[0] = new PassDataToSql("@Company_Id", CompanyId, PassDataToSql.ParaTypeList.Int, PassDataToSql.ParaDirectonList.Input);
        paramlist[1] = new PassDataToSql("@id",DirectoryId, PassDataToSql.ParaTypeList.Int, PassDataToSql.ParaDirectonList.Input);
        paramlist[2] = new PassDataToSql("@Directory_Name", "", PassDataToSql.ParaTypeList.Nvarchar, PassDataToSql.ParaDirectonList.Input);
        paramlist[3] = new PassDataToSql("@Optype", "4", PassDataToSql.ParaTypeList.Int, PassDataToSql.ParaDirectonList.Input);

        dt = da.Reuturn_Datatable_Search("Sp_Arc_Directory_Creation_SelectRow", paramlist);
        return dt;
    }


    
    public Arc_Directory_Master()
	{
		//
		// TODO: Add constructor logic here
		//
	}
}
