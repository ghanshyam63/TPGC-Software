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
/// Summary description for BillOfMaterial
/// </summary>
public class BillOfMaterial
{
    DataAccessClass daClass = new DataAccessClass();
    public BillOfMaterial()
    {
        //
        // TODO: Add constructor logic here
        //
    }

    public int Insert_BOM(string CompanyId, string BrandId, string ProductId, string TDate, string TransType, string SubProductID, string ModelNo, string OptionID, string OptionDescription, string ShortDescription, string OptionCategoryID, string UnitPrice, string Quantity, string PDefault, string Field1, string Field2, string Field3, string Field4, string Field5, string Field6, string Field7, string IsActive, string CreatedBy, string CreatedDate, string ModifiedBy, string ModifiedDate)
    {
        PassDataToSql[] ParamList = new PassDataToSql[26];

        ParamList[0] = new PassDataToSql("@Company_Id", CompanyId, PassDataToSql.ParaTypeList.Int, PassDataToSql.ParaDirectonList.Input);
        ParamList[1] = new PassDataToSql("@ProductId", ProductId, PassDataToSql.ParaTypeList.Int, PassDataToSql.ParaDirectonList.Input);
        ParamList[2] = new PassDataToSql("@TDate", TDate, PassDataToSql.ParaTypeList.Date, PassDataToSql.ParaDirectonList.Input);
        ParamList[3] = new PassDataToSql("@TransType", TransType, PassDataToSql.ParaTypeList.Nvarchar, PassDataToSql.ParaDirectonList.Input);
        ParamList[4] = new PassDataToSql("@SubProductID", SubProductID, PassDataToSql.ParaTypeList.Int, PassDataToSql.ParaDirectonList.Input);
        ParamList[5] = new PassDataToSql("@ModelNo", ModelNo, PassDataToSql.ParaTypeList.Nvarchar, PassDataToSql.ParaDirectonList.Input);
        ParamList[6] = new PassDataToSql("@OptionID", OptionID, PassDataToSql.ParaTypeList.Nvarchar, PassDataToSql.ParaDirectonList.Input);
        ParamList[7] = new PassDataToSql("@OptionDescription", OptionDescription, PassDataToSql.ParaTypeList.Nvarchar, PassDataToSql.ParaDirectonList.Input);
        ParamList[8] = new PassDataToSql("@ShortDescription", ShortDescription, PassDataToSql.ParaTypeList.Nvarchar, PassDataToSql.ParaDirectonList.Input);
        ParamList[9] = new PassDataToSql("@OptionCategoryID", OptionCategoryID, PassDataToSql.ParaTypeList.Nvarchar, PassDataToSql.ParaDirectonList.Input);
        ParamList[10] = new PassDataToSql("@UnitPrice", UnitPrice, PassDataToSql.ParaTypeList.Float, PassDataToSql.ParaDirectonList.Input);
        ParamList[11] = new PassDataToSql("@Quantity", Quantity, PassDataToSql.ParaTypeList.Float, PassDataToSql.ParaDirectonList.Input);
        ParamList[12] = new PassDataToSql("@PDefault", PDefault, PassDataToSql.ParaTypeList.Nvarchar, PassDataToSql.ParaDirectonList.Input);

        ParamList[13] = new PassDataToSql("@Field1", Field1, PassDataToSql.ParaTypeList.Nvarchar, PassDataToSql.ParaDirectonList.Input);
        ParamList[14] = new PassDataToSql("@Field2", Field2, PassDataToSql.ParaTypeList.Nvarchar, PassDataToSql.ParaDirectonList.Input);
        ParamList[15] = new PassDataToSql("@Field3", Field3, PassDataToSql.ParaTypeList.Nvarchar, PassDataToSql.ParaDirectonList.Input);
        ParamList[16] = new PassDataToSql("@Field4", Field4, PassDataToSql.ParaTypeList.Nvarchar, PassDataToSql.ParaDirectonList.Input);
        ParamList[17] = new PassDataToSql("@Field5", Field5, PassDataToSql.ParaTypeList.Nvarchar, PassDataToSql.ParaDirectonList.Input);
        ParamList[18] = new PassDataToSql("@Field6", Field6, PassDataToSql.ParaTypeList.Bit, PassDataToSql.ParaDirectonList.Input);
        ParamList[19] = new PassDataToSql("@Field7", Field7, PassDataToSql.ParaTypeList.Date, PassDataToSql.ParaDirectonList.Input);
      
        ParamList[20] = new PassDataToSql("@IsActive", IsActive, PassDataToSql.ParaTypeList.Bit, PassDataToSql.ParaDirectonList.Input);
        ParamList[21] = new PassDataToSql("@CreatedBy", CreatedBy, PassDataToSql.ParaTypeList.Nvarchar, PassDataToSql.ParaDirectonList.Input);
        ParamList[22] = new PassDataToSql("@CreatedDate",CreatedDate, PassDataToSql.ParaTypeList.Date, PassDataToSql.ParaDirectonList.Input);
        ParamList[23] = new PassDataToSql("@ModifiedBy", ModifiedBy, PassDataToSql.ParaTypeList.Nvarchar, PassDataToSql.ParaDirectonList.Input);
        ParamList[24] = new PassDataToSql("@ModifiedDate", ModifiedDate, PassDataToSql.ParaTypeList.Date, PassDataToSql.ParaDirectonList.Input);
        ParamList[25] = new PassDataToSql("@ReferenceID", "0", PassDataToSql.ParaTypeList.Int, PassDataToSql.ParaDirectonList.Output);

        daClass.execute_Sp("sp_Inv_BOM_Insert", ParamList);
        return Convert.ToInt32(ParamList[25].ParaValue.ToString());

    }
    public int Update_BOM(string TransId, string CompanyId, string BrandId, string ProductId, string TDate, string TransType, string SubProductID, string ModelNo, string OptionID, string OptionDescription, string ShortDescription, string OptionCategoryID, string UnitPrice, string Quantity, string PDefault, string Field1, string Field2, string Field3, string Field4, string Field5, string Field6, string Field7, string IsActive, string ModifiedBy, string ModifiedDate)
    {
        PassDataToSql[] ParamList = new PassDataToSql[25];
        ParamList[0] = new PassDataToSql("@TransId", TransId, PassDataToSql.ParaTypeList.Int, PassDataToSql.ParaDirectonList.Input);
        ParamList[1] = new PassDataToSql("@Company_Id", CompanyId, PassDataToSql.ParaTypeList.Int, PassDataToSql.ParaDirectonList.Input);
        ParamList[2] = new PassDataToSql("@ProductId", ProductId, PassDataToSql.ParaTypeList.Int, PassDataToSql.ParaDirectonList.Input);
        ParamList[3] = new PassDataToSql("@TDate", TDate, PassDataToSql.ParaTypeList.Date, PassDataToSql.ParaDirectonList.Input);
        ParamList[4] = new PassDataToSql("@TransType", TransType, PassDataToSql.ParaTypeList.Nvarchar, PassDataToSql.ParaDirectonList.Input);
        ParamList[5] = new PassDataToSql("@SubProductID", SubProductID, PassDataToSql.ParaTypeList.Int, PassDataToSql.ParaDirectonList.Input);
        ParamList[6] = new PassDataToSql("@ModelNo", ModelNo, PassDataToSql.ParaTypeList.Nvarchar, PassDataToSql.ParaDirectonList.Input);
        ParamList[7] = new PassDataToSql("@OptionID", OptionID, PassDataToSql.ParaTypeList.Nvarchar, PassDataToSql.ParaDirectonList.Input);
        ParamList[8] = new PassDataToSql("@OptionDescription", OptionDescription, PassDataToSql.ParaTypeList.Nvarchar, PassDataToSql.ParaDirectonList.Input);
        ParamList[9] = new PassDataToSql("@ShortDescription", ShortDescription, PassDataToSql.ParaTypeList.Nvarchar, PassDataToSql.ParaDirectonList.Input);
        ParamList[10] = new PassDataToSql("@OptionCategoryID", OptionCategoryID, PassDataToSql.ParaTypeList.Nvarchar, PassDataToSql.ParaDirectonList.Input);
        ParamList[11] = new PassDataToSql("@UnitPrice", UnitPrice, PassDataToSql.ParaTypeList.Float, PassDataToSql.ParaDirectonList.Input);
        ParamList[12] = new PassDataToSql("@Quantity", Quantity, PassDataToSql.ParaTypeList.Float, PassDataToSql.ParaDirectonList.Input);
        ParamList[13] = new PassDataToSql("@PDefault", PDefault, PassDataToSql.ParaTypeList.Nvarchar, PassDataToSql.ParaDirectonList.Input);

        ParamList[14] = new PassDataToSql("@Field1", Field1, PassDataToSql.ParaTypeList.Nvarchar, PassDataToSql.ParaDirectonList.Input);
        ParamList[15] = new PassDataToSql("@Field2", Field2, PassDataToSql.ParaTypeList.Nvarchar, PassDataToSql.ParaDirectonList.Input);
        ParamList[16] = new PassDataToSql("@Field3", Field3, PassDataToSql.ParaTypeList.Nvarchar, PassDataToSql.ParaDirectonList.Input);
        ParamList[17] = new PassDataToSql("@Field4", Field4, PassDataToSql.ParaTypeList.Nvarchar, PassDataToSql.ParaDirectonList.Input);
        ParamList[18] = new PassDataToSql("@Field5", Field5, PassDataToSql.ParaTypeList.Nvarchar, PassDataToSql.ParaDirectonList.Input);
        ParamList[19] = new PassDataToSql("@Field6", Field6, PassDataToSql.ParaTypeList.Bit, PassDataToSql.ParaDirectonList.Input);
        ParamList[20] = new PassDataToSql("@Field7", Field7, PassDataToSql.ParaTypeList.Date, PassDataToSql.ParaDirectonList.Input);
  

        ParamList[21] = new PassDataToSql("@IsActive", IsActive, PassDataToSql.ParaTypeList.Bit, PassDataToSql.ParaDirectonList.Input);
        ParamList[22] = new PassDataToSql("@ModifiedBy", ModifiedBy, PassDataToSql.ParaTypeList.Nvarchar, PassDataToSql.ParaDirectonList.Input);
        ParamList[23] = new PassDataToSql("@ModifiedDate", ModifiedDate, PassDataToSql.ParaTypeList.Date, PassDataToSql.ParaDirectonList.Input);
        ParamList[24] = new PassDataToSql("@ReferenceID", "0", PassDataToSql.ParaTypeList.Int, PassDataToSql.ParaDirectonList.Output);
       
        daClass.execute_Sp("sp_Inv_BOM_Update", ParamList);
        return Convert.ToInt32(ParamList[24].ParaValue);

    }
    public int DeleteOrRestore_BOM(string TransId, string CompanyId, string IsActive, string ModifiedBy, string ModifiedDate)
    {
        PassDataToSql[] ParamList = new PassDataToSql[3];
        ParamList[0] = new PassDataToSql("@TransId", TransId, PassDataToSql.ParaTypeList.Int, PassDataToSql.ParaDirectonList.Input);
        ParamList[1] = new PassDataToSql("@Company_Id", CompanyId, PassDataToSql.ParaTypeList.Nvarchar, PassDataToSql.ParaDirectonList.Input);
        ParamList[2] = new PassDataToSql("@ReferenceID", "0", PassDataToSql.ParaTypeList.Int, PassDataToSql.ParaDirectonList.Output);
        daClass.execute_Sp("sp_Inv_BOM_Delete", ParamList);
        return Convert.ToInt32(ParamList[2].ParaValue.ToString());

    }
    #region this funcation get all record which is in table
    public DataTable BOM_All(string CompanyId)
    {
        PassDataToSql[] ParamList = new PassDataToSql[4];
        ParamList[0] = new PassDataToSql("@TransId", "0", PassDataToSql.ParaTypeList.Int, PassDataToSql.ParaDirectonList.Input);
        ParamList[1] = new PassDataToSql("@Company_Id", CompanyId, PassDataToSql.ParaTypeList.Int, PassDataToSql.ParaDirectonList.Input);
        ParamList[2] = new PassDataToSql("@ProductId", "0", PassDataToSql.ParaTypeList.Int, PassDataToSql.ParaDirectonList.Input);
        ParamList[3] = new PassDataToSql("@Optype", "1", PassDataToSql.ParaTypeList.Nvarchar, PassDataToSql.ParaDirectonList.Input);
        DataTable dt = daClass.Reuturn_Datatable_Search("sp_Inv_BOM_SelectRow", ParamList);
        return dt;

    }
    #endregion

    #region this funcation get record by Id which is not Delete.
    public DataTable BOM_ById(string CompanyId, string TransId)
    {
        PassDataToSql[] ParamList = new PassDataToSql[4];
        ParamList[0] = new PassDataToSql("@TransId", TransId, PassDataToSql.ParaTypeList.Int, PassDataToSql.ParaDirectonList.Input);
        ParamList[1] = new PassDataToSql("@Company_Id", CompanyId, PassDataToSql.ParaTypeList.Int, PassDataToSql.ParaDirectonList.Input);
        ParamList[2] = new PassDataToSql("@ProductId", "0", PassDataToSql.ParaTypeList.Int, PassDataToSql.ParaDirectonList.Input);
        ParamList[3] = new PassDataToSql("@Optype", "2", PassDataToSql.ParaTypeList.Nvarchar, PassDataToSql.ParaDirectonList.Input);
        DataTable dt = daClass.Reuturn_Datatable_Search("sp_Inv_BOM_SelectRow", ParamList);
        return dt;

    }
    #endregion


    #region this funcation get record by Product Id which is not Delete.
    public DataTable BOM_ByProductId(string CompanyId, string ProductId)
    {
        PassDataToSql[] ParamList = new PassDataToSql[4];
        ParamList[0] = new PassDataToSql("@TransId", "0", PassDataToSql.ParaTypeList.Int, PassDataToSql.ParaDirectonList.Input);
        ParamList[1] = new PassDataToSql("@Company_Id", CompanyId, PassDataToSql.ParaTypeList.Int, PassDataToSql.ParaDirectonList.Input);
        ParamList[2] = new PassDataToSql("@ProductId", ProductId, PassDataToSql.ParaTypeList.Int, PassDataToSql.ParaDirectonList.Input);
        ParamList[3] = new PassDataToSql("@Optype", "3", PassDataToSql.ParaTypeList.Nvarchar, PassDataToSql.ParaDirectonList.Input);
        DataTable dt = daClass.Reuturn_Datatable_Search("sp_Inv_BOM_SelectRow", ParamList);
        return dt;

    }
    #endregion
}
