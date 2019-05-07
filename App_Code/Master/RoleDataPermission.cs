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
/// Summary description for RoleDataPermission
/// </summary>
public class RoleDataPermission
{
    DataAccessClass daClass = new DataAccessClass();

	public RoleDataPermission()
	{
		//
		// TODO: Add constructor logic here
		//
	}



    public int InsertRoleDataPermission(string RoleId, string CompanyId, string RecordType,string RecordId, string IsActive, string CreatedBy, string CreatedDate, string ModifiedBy, string ModifiedDate)
    {
       
        PassDataToSql[] paramList = new PassDataToSql[10];
        paramList[0] = new PassDataToSql("@Role_Id", RoleId, PassDataToSql.ParaTypeList.Int, PassDataToSql.ParaDirectonList.Input);


        paramList[1] = new PassDataToSql("@Company_Id", CompanyId, PassDataToSql.ParaTypeList.Int, PassDataToSql.ParaDirectonList.Input);
        paramList[2] = new PassDataToSql("@Record_Type",RecordType, PassDataToSql.ParaTypeList.Nvarchar, PassDataToSql.ParaDirectonList.Input);



        paramList[3] = new PassDataToSql("@IsActive", IsActive, PassDataToSql.ParaTypeList.Bit, PassDataToSql.ParaDirectonList.Input);
        paramList[4] = new PassDataToSql("@CreatedBy", CreatedBy, PassDataToSql.ParaTypeList.Nvarchar, PassDataToSql.ParaDirectonList.Input);
        paramList[5] = new PassDataToSql("@CreatedDate", CreatedDate, PassDataToSql.ParaTypeList.Date, PassDataToSql.ParaDirectonList.Input);
        paramList[6] = new PassDataToSql("@ModifiedBy", ModifiedBy, PassDataToSql.ParaTypeList.Nvarchar, PassDataToSql.ParaDirectonList.Input);
        paramList[7] = new PassDataToSql("@ModifiedDate", ModifiedDate, PassDataToSql.ParaTypeList.Date, PassDataToSql.ParaDirectonList.Input);
        paramList[8] = new PassDataToSql("@ReferenceID", "0", PassDataToSql.ParaTypeList.Int, PassDataToSql.ParaDirectonList.Output);
         paramList[9] = new PassDataToSql("@Record_Id",RecordId , PassDataToSql.ParaTypeList.Int, PassDataToSql.ParaDirectonList.Input);

        daClass.execute_Sp("sp_Set_RoleDataPermission_Insert", paramList);
        return Convert.ToInt32(paramList[8].ParaValue);
    }

    public int UpdateRoleDataPermission(string RoleId, string CompanyId, string RecordType, string RecordId, string IsActive, string ModifiedBy, string ModifiedDate)
    {

        PassDataToSql[] paramList = new PassDataToSql[8];
        paramList[0] = new PassDataToSql("@Role_Id", RoleId, PassDataToSql.ParaTypeList.Int, PassDataToSql.ParaDirectonList.Input);


        paramList[1] = new PassDataToSql("@Company_Id", CompanyId, PassDataToSql.ParaTypeList.Int, PassDataToSql.ParaDirectonList.Input);
        paramList[2] = new PassDataToSql("@Record_Type",RecordType, PassDataToSql.ParaTypeList.Int, PassDataToSql.ParaDirectonList.Input);



        paramList[3] = new PassDataToSql("@IsActive", IsActive, PassDataToSql.ParaTypeList.Bit, PassDataToSql.ParaDirectonList.Input);
        paramList[4] = new PassDataToSql("@ModifiedBy", ModifiedBy, PassDataToSql.ParaTypeList.Nvarchar, PassDataToSql.ParaDirectonList.Input);
        paramList[5] = new PassDataToSql("@ModifiedDate", ModifiedDate, PassDataToSql.ParaTypeList.Date, PassDataToSql.ParaDirectonList.Input);
        paramList[6] = new PassDataToSql("@ReferenceID", "0", PassDataToSql.ParaTypeList.Int, PassDataToSql.ParaDirectonList.Output);
        paramList[7] = new PassDataToSql("@Record_Id",RecordId , PassDataToSql.ParaTypeList.Int, PassDataToSql.ParaDirectonList.Input);

        daClass.execute_Sp("sp_Set_RoleDataPermission_Update", paramList);
        return Convert.ToInt32(paramList[6].ParaValue);
    }

    public int DeleteRoleDataPermission(string RoleId)
    {

        PassDataToSql[] paramList = new PassDataToSql[2];
        paramList[0] = new PassDataToSql("@Role_Id", RoleId, PassDataToSql.ParaTypeList.Int, PassDataToSql.ParaDirectonList.Input);
        paramList[1] = new PassDataToSql("@ReferenceID", "0", PassDataToSql.ParaTypeList.Int, PassDataToSql.ParaDirectonList.Output);
        daClass.execute_Sp("sp_Set_RoleDataPermission_RowStatus", paramList);
        return Convert.ToInt32(paramList[1].ParaValue);
    }


    public DataTable GetRoleDataPermissionById(string RoleId)
    {

        DataTable dtInfo = new DataTable();
        PassDataToSql[] paramList = new PassDataToSql[2];

        paramList[0] = new PassDataToSql("@Role_Id", RoleId, PassDataToSql.ParaTypeList.Nvarchar, PassDataToSql.ParaDirectonList.Input);

        paramList[1] = new PassDataToSql("@optype", "4", PassDataToSql.ParaTypeList.Nvarchar, PassDataToSql.ParaDirectonList.Input);
        dtInfo = daClass.Reuturn_Datatable_Search("sp_Set_RoleDataPermission_SelectRow", paramList);

        return dtInfo;
    }
    public DataTable GetRoleDataPermission()
    {

        DataTable dtInfo = new DataTable();
        PassDataToSql[] paramList = new PassDataToSql[2];

        paramList[0] = new PassDataToSql("@Role_Id", "0", PassDataToSql.ParaTypeList.Nvarchar, PassDataToSql.ParaDirectonList.Input);

        paramList[1] = new PassDataToSql("@optype", "2", PassDataToSql.ParaTypeList.Nvarchar, PassDataToSql.ParaDirectonList.Input);
        dtInfo = daClass.Reuturn_Datatable_Search("sp_Set_RoleDataPermission_SelectRow", paramList);

        return dtInfo;
    }


    public DataTable GetRoleDataPermissionInactive()
    {
        DataTable dtInfo = new DataTable();
        PassDataToSql[] paramList = new PassDataToSql[2];

        paramList[0] = new PassDataToSql("@Role_Id", "0", PassDataToSql.ParaTypeList.Nvarchar, PassDataToSql.ParaDirectonList.Input);

        paramList[1] = new PassDataToSql("@optype", "3", PassDataToSql.ParaTypeList.Nvarchar, PassDataToSql.ParaDirectonList.Input);
        dtInfo = daClass.Reuturn_Datatable_Search("sp_Set_RoleDataPermission_SelectRow", paramList);

        return dtInfo;
    }

}
