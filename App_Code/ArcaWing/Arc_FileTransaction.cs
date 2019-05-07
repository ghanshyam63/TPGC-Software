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
/// Summary description for Arc_FileTransaction
/// </summary>
public class Arc_FileTransaction
{

    DataAccessClass da = new DataAccessClass();



    public int Insert_In_FileTransaction(string CompanyId, string Directoryid,string DocumentMasterid,string File_Type_id,string FileName,string File_UploadDate,byte[] FileData,string FilePath ,string Expiry_Date,string Field1, string Field2, string Field3, string Field4, string Field5, string Field6, string Field7, string IsActive, string CreatedBy, string CreatedDate, string ModifiedBy, string ModifiedDate)
    {
        PassDataToSql[] param = new PassDataToSql[22];
        param[0] = new PassDataToSql("@Company_Id", CompanyId, PassDataToSql.ParaTypeList.Int, PassDataToSql.ParaDirectonList.Input);

        param[1] = new PassDataToSql("@Directory_id",Directoryid, PassDataToSql.ParaTypeList.Int, PassDataToSql.ParaDirectonList.Input);
        param[2] = new PassDataToSql("@Document_Master_id",DocumentMasterid, PassDataToSql.ParaTypeList.Int, PassDataToSql.ParaDirectonList.Input);
        param[3] = new PassDataToSql("@File_Type_id",File_Type_id, PassDataToSql.ParaTypeList.Int, PassDataToSql.ParaDirectonList.Input);
        param[4] = new PassDataToSql("@File_Name", FileName, PassDataToSql.ParaTypeList.Nvarchar, PassDataToSql.ParaDirectonList.Input);
        param[5] = new PassDataToSql("@File_Upload_Date",File_UploadDate, PassDataToSql.ParaTypeList.DateTime, PassDataToSql.ParaDirectonList.Input);
        param[6] = new PassDataToSql("@File_Data", FileData, PassDataToSql.ParaTypeList.Image, PassDataToSql.ParaDirectonList.Input);
        param[7] = new PassDataToSql("@File_Path",FilePath, PassDataToSql.ParaTypeList.Nvarchar, PassDataToSql.ParaDirectonList.Input);
        param[8] = new PassDataToSql("@File_Expiry_Date", Expiry_Date, PassDataToSql.ParaTypeList.DateTime, PassDataToSql.ParaDirectonList.Input);

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

        da.execute_Sp("Sp_Arc_File_Transaction_Insert", param);
        return Convert.ToInt32(param[21].ParaValue);

        //return Convert.ToInt32(param[3].ParaValue);

    }

    public int Update_In_FileTransaction(string CompanyId, string Transactionid, string Directoryid, string DocumentMasterid, string File_Type_id, string FileName, string File_UploadDate, byte[] FileData, string FilePath, string Expiry_Date,string Field1, string Field2, string Field3, string Field4, string Field5, string Field6, string Field7, string IsActive, string ModifiedBy, string ModifiedDate)
    {
        PassDataToSql[] param = new PassDataToSql[21];
        param[0] = new PassDataToSql("@Company_Id", CompanyId, PassDataToSql.ParaTypeList.Int, PassDataToSql.ParaDirectonList.Input);
        param[1] = new PassDataToSql("@Trans_Id",Transactionid, PassDataToSql.ParaTypeList.Int, PassDataToSql.ParaDirectonList.Input);

        param[2] = new PassDataToSql("@Directory_id", Directoryid, PassDataToSql.ParaTypeList.Int, PassDataToSql.ParaDirectonList.Input);
        param[3] = new PassDataToSql("@Document_Master_id", DocumentMasterid, PassDataToSql.ParaTypeList.Int, PassDataToSql.ParaDirectonList.Input);
        param[4] = new PassDataToSql("@File_Type_id", File_Type_id, PassDataToSql.ParaTypeList.Int, PassDataToSql.ParaDirectonList.Input);
        param[5] = new PassDataToSql("@File_Name", FileName, PassDataToSql.ParaTypeList.Nvarchar, PassDataToSql.ParaDirectonList.Input);
        param[6] = new PassDataToSql("@File_Upload_Date", File_UploadDate, PassDataToSql.ParaTypeList.DateTime, PassDataToSql.ParaDirectonList.Input);
        param[7] = new PassDataToSql("@File_Data", FileData, PassDataToSql.ParaTypeList.Image, PassDataToSql.ParaDirectonList.Input);
        param[8] = new PassDataToSql("@File_Path", FilePath, PassDataToSql.ParaTypeList.Nvarchar, PassDataToSql.ParaDirectonList.Input);
        param[9] = new PassDataToSql("@File_Expiry_Date", Expiry_Date, PassDataToSql.ParaTypeList.DateTime, PassDataToSql.ParaDirectonList.Input);

        param[10] = new PassDataToSql("@Field1", Field1, PassDataToSql.ParaTypeList.Nvarchar, PassDataToSql.ParaDirectonList.Input);
        param[11] = new PassDataToSql("@Field2", Field2, PassDataToSql.ParaTypeList.Nvarchar, PassDataToSql.ParaDirectonList.Input);
        param[12] = new PassDataToSql("@Field3", Field3, PassDataToSql.ParaTypeList.Nvarchar, PassDataToSql.ParaDirectonList.Input);
        param[13] = new PassDataToSql("@Field4", Field4, PassDataToSql.ParaTypeList.Nvarchar, PassDataToSql.ParaDirectonList.Input);
        param[14] = new PassDataToSql("@Field5", Field5, PassDataToSql.ParaTypeList.Nvarchar, PassDataToSql.ParaDirectonList.Input);
        param[15] = new PassDataToSql("@Field6", Field6, PassDataToSql.ParaTypeList.Bit, PassDataToSql.ParaDirectonList.Input);
        param[16] = new PassDataToSql("@Field7", Field7, PassDataToSql.ParaTypeList.DateTime, PassDataToSql.ParaDirectonList.Input);
        param[17] = new PassDataToSql("@IsActive", IsActive, PassDataToSql.ParaTypeList.Bit, PassDataToSql.ParaDirectonList.Input);
        param[18] = new PassDataToSql("@ModifiedBy", ModifiedBy, PassDataToSql.ParaTypeList.Nvarchar, PassDataToSql.ParaDirectonList.Input);
        param[19] = new PassDataToSql("@ModifiedDate", ModifiedDate, PassDataToSql.ParaTypeList.DateTime, PassDataToSql.ParaDirectonList.Input);
        param[20] = new PassDataToSql("@ReferenceID", "0", PassDataToSql.ParaTypeList.Int, PassDataToSql.ParaDirectonList.Output);

        //param[0] = new PassDataToSql("@Company_id",CompanyId, PassDataToSql.ParaTypeList.Int, PassDataToSql.ParaDirectonList.Input);
        //param[0] = new PassDataToSql("@Company_id", CompanyId, PassDataToSql.ParaTypeList.Int, PassDataToSql.ParaDirectonList.Input);

        da.execute_Sp("Sp_Arc_File_Transaction_Update", param);
        return Convert.ToInt32(param[20].ParaValue);

        //return Convert.ToInt32(param[3].ParaValue);

    }
    public int Delete_in_FileTransaction(string CompanyId, string TransactionId, string IsActive, string ModifiedBy, string ModifiedDate)
    {
        PassDataToSql[] param = new PassDataToSql[6];
        param[0] = new PassDataToSql("@Company_Id", CompanyId, PassDataToSql.ParaTypeList.Int, PassDataToSql.ParaDirectonList.Input);
        param[1] = new PassDataToSql("@Trans_id",TransactionId, PassDataToSql.ParaTypeList.Int, PassDataToSql.ParaDirectonList.Input);
        param[2] = new PassDataToSql("@IsActive", IsActive, PassDataToSql.ParaTypeList.Bit, PassDataToSql.ParaDirectonList.Input);
        param[3] = new PassDataToSql("@ModifiedBy", ModifiedBy, PassDataToSql.ParaTypeList.Nvarchar, PassDataToSql.ParaDirectonList.Input);
        param[4] = new PassDataToSql("@ModifiedDate", ModifiedDate, PassDataToSql.ParaTypeList.Nvarchar, PassDataToSql.ParaDirectonList.Input);
        param[5] = new PassDataToSql("@ReferenceID", "0", PassDataToSql.ParaTypeList.Int, PassDataToSql.ParaDirectonList.Output);

        da.execute_Sp("Sp_Arc_File_Transaction_RowStatus", param);

        return Convert.ToInt32(param[5].ParaValue);
    }
    public DataTable Get_FileTransaction(string CompanyId, string TransactionId)
    {
        DataTable dt = new DataTable();
        PassDataToSql[] paramlist = new PassDataToSql[4];

        paramlist[0] = new PassDataToSql("@Company_Id", CompanyId, PassDataToSql.ParaTypeList.Int, PassDataToSql.ParaDirectonList.Input);
        paramlist[1] = new PassDataToSql("@Trans_id",TransactionId, PassDataToSql.ParaTypeList.Int, PassDataToSql.ParaDirectonList.Input);
        paramlist[2] = new PassDataToSql("@Document_id", "0", PassDataToSql.ParaTypeList.Nvarchar, PassDataToSql.ParaDirectonList.Input);
        paramlist[3] = new PassDataToSql("@Optype", "2", PassDataToSql.ParaTypeList.Int, PassDataToSql.ParaDirectonList.Input);

        dt = da.Reuturn_Datatable_Search("Sp_Arc_File_Transaction_SelectRow", paramlist);
        return dt;

    }
    public DataTable Get_FileTransactionInActive(string CompanyId, string TransactionId)
    {
        DataTable dt = new DataTable();
        PassDataToSql[] paramlist = new PassDataToSql[4];

        paramlist[0] = new PassDataToSql("@Company_Id", CompanyId, PassDataToSql.ParaTypeList.Int, PassDataToSql.ParaDirectonList.Input);
        paramlist[1] = new PassDataToSql("@Trans_id", TransactionId, PassDataToSql.ParaTypeList.Int, PassDataToSql.ParaDirectonList.Input);
        paramlist[2] = new PassDataToSql("@Document_id", "0", PassDataToSql.ParaTypeList.Nvarchar, PassDataToSql.ParaDirectonList.Input);
        paramlist[3] = new PassDataToSql("@Optype", "3", PassDataToSql.ParaTypeList.Int, PassDataToSql.ParaDirectonList.Input);

        dt = da.Reuturn_Datatable_Search("Sp_Arc_File_Transaction_SelectRow", paramlist);
        return dt;

    }
    public DataTable Get_FileTransaction_By_TransactionId(string CompanyId, string TransactionId)
    {
        DataTable dt = new DataTable();
        PassDataToSql[] paramlist = new PassDataToSql[4];

        paramlist[0] = new PassDataToSql("@Company_Id", CompanyId, PassDataToSql.ParaTypeList.Int, PassDataToSql.ParaDirectonList.Input);
        paramlist[1] = new PassDataToSql("@Trans_id", TransactionId, PassDataToSql.ParaTypeList.Int, PassDataToSql.ParaDirectonList.Input);
        paramlist[2] = new PassDataToSql("@Document_id", "0", PassDataToSql.ParaTypeList.Nvarchar, PassDataToSql.ParaDirectonList.Input);
        paramlist[3] = new PassDataToSql("@Optype", "4", PassDataToSql.ParaTypeList.Int, PassDataToSql.ParaDirectonList.Input);

        dt = da.Reuturn_Datatable_Search("Sp_Arc_File_Transaction_SelectRow", paramlist);
        return dt;

    }
    public DataTable Get_FileTransaction_By_Documentid(string CompanyId, string TransactionId,string Documentid)
    {
        DataTable dt = new DataTable();
        PassDataToSql[] paramlist = new PassDataToSql[4];

        paramlist[0] = new PassDataToSql("@Company_Id", CompanyId, PassDataToSql.ParaTypeList.Int, PassDataToSql.ParaDirectonList.Input);
        paramlist[1] = new PassDataToSql("@Trans_id", TransactionId, PassDataToSql.ParaTypeList.Int, PassDataToSql.ParaDirectonList.Input);
        paramlist[2] = new PassDataToSql("@Document_id",Documentid, PassDataToSql.ParaTypeList.Nvarchar, PassDataToSql.ParaDirectonList.Input);
        paramlist[3] = new PassDataToSql("@Optype", "5", PassDataToSql.ParaTypeList.Int, PassDataToSql.ParaDirectonList.Input);

        dt = da.Reuturn_Datatable_Search("Sp_Arc_File_Transaction_SelectRow", paramlist);
        return dt;

    }

    public Arc_FileTransaction()
	{
		//
		// TODO: Add constructor logic here
		//
	}
}
