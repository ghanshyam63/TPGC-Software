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
/// Summary description for UserPermission
/// </summary>
public class UserPermission
{
    DataAccessClass daClass = new DataAccessClass();
	public UserPermission()
	{
		//
		// TODO: Add constructor logic here
		//
	}

    public int InsertUserOpPermission(string User_Record_Id, string Op_Add, string Op_Edit, string Op_Delete, string Op_Restore, string Op_View, string Op_Print, string Op_Download, string Op_Upload)
    {


        PassDataToSql[] paramList = new PassDataToSql[10];
        paramList[0] = new PassDataToSql("@User_Record_Id",User_Record_Id, PassDataToSql.ParaTypeList.Int, PassDataToSql.ParaDirectonList.Input);


        paramList[1] = new PassDataToSql("@Op_Add", Op_Add, PassDataToSql.ParaTypeList.Bit, PassDataToSql.ParaDirectonList.Input);
        paramList[2] = new PassDataToSql("@Op_Edit", Op_Edit, PassDataToSql.ParaTypeList.Bit, PassDataToSql.ParaDirectonList.Input);
        paramList[3] = new PassDataToSql("@Op_Delete", Op_Delete, PassDataToSql.ParaTypeList.Bit, PassDataToSql.ParaDirectonList.Input);
        paramList[4] = new PassDataToSql("@Op_Restore", Op_Restore, PassDataToSql.ParaTypeList.Bit, PassDataToSql.ParaDirectonList.Input);
        paramList[5] = new PassDataToSql("@Op_View", Op_View, PassDataToSql.ParaTypeList.Bit, PassDataToSql.ParaDirectonList.Input);
        paramList[6] = new PassDataToSql("@Op_Print", Op_Print, PassDataToSql.ParaTypeList.Bit, PassDataToSql.ParaDirectonList.Input);
        paramList[7] = new PassDataToSql("@Op_Download", Op_Download, PassDataToSql.ParaTypeList.Bit, PassDataToSql.ParaDirectonList.Input);

        paramList[8] = new PassDataToSql("@Op_Upload", Op_Upload, PassDataToSql.ParaTypeList.Bit, PassDataToSql.ParaDirectonList.Input);
        paramList[9] = new PassDataToSql("@ReferenceID", "0", PassDataToSql.ParaTypeList.Int, PassDataToSql.ParaDirectonList.Output);

        daClass.execute_Sp("sp_Set_UserOpPermission_Insert", paramList);
        return Convert.ToInt32(paramList[9].ParaValue);
    }





    public int InsertUserPermission(string UserId, string ModuleId, string ObjectId, string IsActive, string CreatedBy, string CreatedDate, string ModifiedBy, string ModifiedDate)
    {


        PassDataToSql[] paramList = new PassDataToSql[9];
        paramList[0] = new PassDataToSql("@User_Id", UserId, PassDataToSql.ParaTypeList.Nvarchar, PassDataToSql.ParaDirectonList.Input);


        paramList[1] = new PassDataToSql("@Module_Id", ModuleId, PassDataToSql.ParaTypeList.Int, PassDataToSql.ParaDirectonList.Input);
        paramList[2] = new PassDataToSql("@Object_Id", ObjectId, PassDataToSql.ParaTypeList.Int, PassDataToSql.ParaDirectonList.Input);



        paramList[3] = new PassDataToSql("@IsActive", IsActive, PassDataToSql.ParaTypeList.Bit, PassDataToSql.ParaDirectonList.Input);
        paramList[4] = new PassDataToSql("@CreatedBy", CreatedBy, PassDataToSql.ParaTypeList.Nvarchar, PassDataToSql.ParaDirectonList.Input);
        paramList[5] = new PassDataToSql("@CreatedDate", CreatedDate, PassDataToSql.ParaTypeList.Date, PassDataToSql.ParaDirectonList.Input);
        paramList[6] = new PassDataToSql("@ModifiedBy", ModifiedBy, PassDataToSql.ParaTypeList.Nvarchar, PassDataToSql.ParaDirectonList.Input);
        paramList[7] = new PassDataToSql("@ModifiedDate", ModifiedDate, PassDataToSql.ParaTypeList.Date, PassDataToSql.ParaDirectonList.Input);
        paramList[8] = new PassDataToSql("@ReferenceID", "0", PassDataToSql.ParaTypeList.Int, PassDataToSql.ParaDirectonList.Output);

        daClass.execute_Sp("sp_Set_UserPermission_Insert", paramList);
        return Convert.ToInt32(paramList[8].ParaValue);
    }

    public int UpdateUserPermission(string UserId, string ModuleId, string ObjectId, string IsActive, string ModifiedBy, string ModifiedDate)
    {

        PassDataToSql[] paramList = new PassDataToSql[7];
        paramList[0] = new PassDataToSql("@User_Id", UserId, PassDataToSql.ParaTypeList.Nvarchar, PassDataToSql.ParaDirectonList.Input);


        paramList[1] = new PassDataToSql("@Module_Id", ModuleId, PassDataToSql.ParaTypeList.Int, PassDataToSql.ParaDirectonList.Input);
        paramList[2] = new PassDataToSql("@Object_Id", ObjectId, PassDataToSql.ParaTypeList.Int, PassDataToSql.ParaDirectonList.Input);



        paramList[3] = new PassDataToSql("@IsActive", IsActive, PassDataToSql.ParaTypeList.Bit, PassDataToSql.ParaDirectonList.Input);
        paramList[4] = new PassDataToSql("@ModifiedBy", ModifiedBy, PassDataToSql.ParaTypeList.Nvarchar, PassDataToSql.ParaDirectonList.Input);
        paramList[5] = new PassDataToSql("@ModifiedDate", ModifiedDate, PassDataToSql.ParaTypeList.Date, PassDataToSql.ParaDirectonList.Input);
        paramList[6] = new PassDataToSql("@ReferenceID", "0", PassDataToSql.ParaTypeList.Int, PassDataToSql.ParaDirectonList.Output);

        daClass.execute_Sp("sp_Set_UserPermission_Update", paramList);
        return Convert.ToInt32(paramList[6].ParaValue);
    }

    public int DeleteUserPermission(string UserId)
    {

        PassDataToSql[] paramList = new PassDataToSql[2];
        paramList[0] = new PassDataToSql("@User_Id", UserId, PassDataToSql.ParaTypeList.Nvarchar, PassDataToSql.ParaDirectonList.Input);
        paramList[1] = new PassDataToSql("@ReferenceID", "0", PassDataToSql.ParaTypeList.Int, PassDataToSql.ParaDirectonList.Output);
        daClass.execute_Sp("sp_Set_UserPermission_RowStatus", paramList);
        return Convert.ToInt32(paramList[1].ParaValue);
    }





    public DataTable GetUserOpPermission(string RefId)
    {

        DataTable dtInfo = new DataTable();
        PassDataToSql[] paramList = new PassDataToSql[1];

        paramList[0] = new PassDataToSql("@Ref_Id", RefId, PassDataToSql.ParaTypeList.Nvarchar, PassDataToSql.ParaDirectonList.Input);

        dtInfo = daClass.Reuturn_Datatable_Search("Set_UserOpPermission_SelectRow", paramList);

        return dtInfo;
    }
    public DataTable GetUserPermissionById(string UserId)
    {

        DataTable dtInfo = new DataTable();
        PassDataToSql[] paramList = new PassDataToSql[2];

        paramList[0] = new PassDataToSql("@User_Id", UserId, PassDataToSql.ParaTypeList.Nvarchar, PassDataToSql.ParaDirectonList.Input);

        paramList[1] = new PassDataToSql("@optype", "4", PassDataToSql.ParaTypeList.Nvarchar, PassDataToSql.ParaDirectonList.Input);
        dtInfo = daClass.Reuturn_Datatable_Search("sp_Set_UserPermission_SelectRow", paramList);

        return dtInfo;
    }
    public DataTable GetUserPermission()
    {

        DataTable dtInfo = new DataTable();
        PassDataToSql[] paramList = new PassDataToSql[2];

        paramList[0] = new PassDataToSql("@User_Id", "0", PassDataToSql.ParaTypeList.Nvarchar, PassDataToSql.ParaDirectonList.Input);

        paramList[1] = new PassDataToSql("@optype", "2", PassDataToSql.ParaTypeList.Nvarchar, PassDataToSql.ParaDirectonList.Input);
        dtInfo = daClass.Reuturn_Datatable_Search("sp_Set_UserPermission_SelectRow", paramList);

        return dtInfo;
    }


    public DataTable GetUserPermissionInactive()
    {
        DataTable dtInfo = new DataTable();
        PassDataToSql[] paramList = new PassDataToSql[2];

        paramList[0] = new PassDataToSql("@User_Id", "0", PassDataToSql.ParaTypeList.Nvarchar, PassDataToSql.ParaDirectonList.Input);

        paramList[1] = new PassDataToSql("@optype", "3", PassDataToSql.ParaTypeList.Nvarchar, PassDataToSql.ParaDirectonList.Input);
        dtInfo = daClass.Reuturn_Datatable_Search("sp_Set_UserPermission_SelectRow", paramList);

        return dtInfo;
    }
}
