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
/// Summary description for Inv_ProductCategoryMaster
/// </summary>
public class Inv_ProductCategoryMaster
{
    DataAccessClass daClass = new DataAccessClass();
    public Inv_ProductCategoryMaster()
    {
        //
        // TODO: Add constructor logic here
        //
    }
    public int InsertProductCategoryMaster(string strCompany_Id, string strBrand_Id, string strCategory_Name, string strCategory_Name_L, string strAccount_No, string strParent_Id, string strDescription, string strField1, string strField2, string strField3, string strField4, string strField5, string strField6, string strField7, string strIsActive, string strCreatedBy, string strCreatedDate, string strModifiedBy, string strModifiedDate)
    {
        PassDataToSql[] paramList = new PassDataToSql[20];
        paramList[0] = new PassDataToSql("@Company_Id", strCompany_Id, PassDataToSql.ParaTypeList.Int, PassDataToSql.ParaDirectonList.Input);
        paramList[1] = new PassDataToSql("@Brand_Id", strBrand_Id, PassDataToSql.ParaTypeList.Int, PassDataToSql.ParaDirectonList.Input);
        paramList[2] = new PassDataToSql("@Category_Name", strCategory_Name, PassDataToSql.ParaTypeList.Nvarchar, PassDataToSql.ParaDirectonList.Input);
        paramList[3] = new PassDataToSql("@Category_Name_L", strCategory_Name_L, PassDataToSql.ParaTypeList.Nvarchar, PassDataToSql.ParaDirectonList.Input);
        paramList[4] = new PassDataToSql("@Account_No", strAccount_No, PassDataToSql.ParaTypeList.Nvarchar, PassDataToSql.ParaDirectonList.Input);
        paramList[5] = new PassDataToSql("@Parent_Id", strParent_Id, PassDataToSql.ParaTypeList.Nvarchar, PassDataToSql.ParaDirectonList.Input);
        paramList[6] = new PassDataToSql("@Description", strDescription, PassDataToSql.ParaTypeList.Nvarchar, PassDataToSql.ParaDirectonList.Input);
        paramList[7] = new PassDataToSql("@Field1", strField1, PassDataToSql.ParaTypeList.Nvarchar, PassDataToSql.ParaDirectonList.Input);
        paramList[8] = new PassDataToSql("@Field2", strField2, PassDataToSql.ParaTypeList.Nvarchar, PassDataToSql.ParaDirectonList.Input);
        paramList[9] = new PassDataToSql("@Field3", strField3, PassDataToSql.ParaTypeList.Nvarchar, PassDataToSql.ParaDirectonList.Input);
        paramList[10] = new PassDataToSql("@Field4", strField4, PassDataToSql.ParaTypeList.Nvarchar, PassDataToSql.ParaDirectonList.Input);
        paramList[11] = new PassDataToSql("@Field5", strField5, PassDataToSql.ParaTypeList.Nvarchar, PassDataToSql.ParaDirectonList.Input);
        paramList[12] = new PassDataToSql("@Field6", strField6, PassDataToSql.ParaTypeList.Bit, PassDataToSql.ParaDirectonList.Input);
        paramList[13] = new PassDataToSql("@Field7", strField7, PassDataToSql.ParaTypeList.Date, PassDataToSql.ParaDirectonList.Input);
        paramList[14] = new PassDataToSql("@IsActive", strIsActive, PassDataToSql.ParaTypeList.Bit, PassDataToSql.ParaDirectonList.Input);
        paramList[15] = new PassDataToSql("@CreatedBy", strCreatedBy, PassDataToSql.ParaTypeList.Nvarchar, PassDataToSql.ParaDirectonList.Input);
        paramList[16] = new PassDataToSql("@CreatedDate", strCreatedDate, PassDataToSql.ParaTypeList.Date, PassDataToSql.ParaDirectonList.Input);
        paramList[17] = new PassDataToSql("@ModifiedBy", strModifiedBy, PassDataToSql.ParaTypeList.Nvarchar, PassDataToSql.ParaDirectonList.Input);
        paramList[18] = new PassDataToSql("@ModifiedDate", strModifiedDate, PassDataToSql.ParaTypeList.Date, PassDataToSql.ParaDirectonList.Input);
        paramList[19] = new PassDataToSql("@ReferenceID", "0", PassDataToSql.ParaTypeList.Int, PassDataToSql.ParaDirectonList.Output);

        daClass.execute_Sp("sp_Inv_Product_CategoryMaster_Insert", paramList);
        return Convert.ToInt32(paramList[19].ParaValue);
    }
    public int UpdateProductCategoryMaster(string strCompany_Id, string strBrand_Id, string strCategory_Id, string strCategory_Name, string strCategory_Name_L, string strAccount_No, string strParent_Id, string strDescription, string strField1, string strField2, string strField3, string strField4, string strField5, string strField6, string strField7, string strIsActive, string strModifiedBy, string strModifiedDate)
    {
        PassDataToSql[] paramList = new PassDataToSql[19];

        paramList[0] = new PassDataToSql("@Company_Id", strCompany_Id, PassDataToSql.ParaTypeList.Int, PassDataToSql.ParaDirectonList.Input);
        paramList[1] = new PassDataToSql("@Brand_Id", strBrand_Id, PassDataToSql.ParaTypeList.Int, PassDataToSql.ParaDirectonList.Input);
        paramList[2] = new PassDataToSql("@Category_Id", strCategory_Id, PassDataToSql.ParaTypeList.Nvarchar, PassDataToSql.ParaDirectonList.Input);
        paramList[3] = new PassDataToSql("@Category_Name", strCategory_Name, PassDataToSql.ParaTypeList.Nvarchar, PassDataToSql.ParaDirectonList.Input);
        paramList[4] = new PassDataToSql("@Category_Name_L", strCategory_Name_L, PassDataToSql.ParaTypeList.Nvarchar, PassDataToSql.ParaDirectonList.Input);
        paramList[5] = new PassDataToSql("@Account_No", strAccount_No, PassDataToSql.ParaTypeList.Nvarchar, PassDataToSql.ParaDirectonList.Input);
        paramList[6] = new PassDataToSql("@Parent_Id", strParent_Id, PassDataToSql.ParaTypeList.Nvarchar, PassDataToSql.ParaDirectonList.Input);
        paramList[7] = new PassDataToSql("@Description", strDescription, PassDataToSql.ParaTypeList.Nvarchar, PassDataToSql.ParaDirectonList.Input);
        paramList[8] = new PassDataToSql("@Field1", strField1, PassDataToSql.ParaTypeList.Nvarchar, PassDataToSql.ParaDirectonList.Input);
        paramList[9] = new PassDataToSql("@Field2", strField2, PassDataToSql.ParaTypeList.Nvarchar, PassDataToSql.ParaDirectonList.Input);
        paramList[10] = new PassDataToSql("@Field3", strField3, PassDataToSql.ParaTypeList.Nvarchar, PassDataToSql.ParaDirectonList.Input);
        paramList[11] = new PassDataToSql("@Field4", strField4, PassDataToSql.ParaTypeList.Nvarchar, PassDataToSql.ParaDirectonList.Input);
        paramList[12] = new PassDataToSql("@Field5", strField5, PassDataToSql.ParaTypeList.Nvarchar, PassDataToSql.ParaDirectonList.Input);
        paramList[13] = new PassDataToSql("@Field6", strField6, PassDataToSql.ParaTypeList.Bit, PassDataToSql.ParaDirectonList.Input);
        paramList[14] = new PassDataToSql("@Field7", strField7, PassDataToSql.ParaTypeList.Date, PassDataToSql.ParaDirectonList.Input);
        paramList[15] = new PassDataToSql("@IsActive", strIsActive, PassDataToSql.ParaTypeList.Bit, PassDataToSql.ParaDirectonList.Input);
        paramList[16] = new PassDataToSql("@ModifiedBy", strModifiedBy, PassDataToSql.ParaTypeList.Nvarchar, PassDataToSql.ParaDirectonList.Input);
        paramList[17] = new PassDataToSql("@ModifiedDate", strModifiedDate, PassDataToSql.ParaTypeList.Date, PassDataToSql.ParaDirectonList.Input);
        paramList[18] = new PassDataToSql("@ReferenceID", "0", PassDataToSql.ParaTypeList.Int, PassDataToSql.ParaDirectonList.Output);

        daClass.execute_Sp("sp_Inv_Product_CategoryMaster_Update", paramList);
        return Convert.ToInt32(paramList[18].ParaValue);
    }
    public int DeleteProductCategoryMaster(string strCompany_Id, string strCategory_Id, string IsActive, string ModifiedBy, string ModifiedDate)
    {
        PassDataToSql[] paramList = new PassDataToSql[6];
        paramList[0] = new PassDataToSql("@Company_Id", strCompany_Id, PassDataToSql.ParaTypeList.Nvarchar, PassDataToSql.ParaDirectonList.Input);
        paramList[1] = new PassDataToSql("@Category_Id", strCategory_Id, PassDataToSql.ParaTypeList.Nvarchar, PassDataToSql.ParaDirectonList.Input);
        paramList[2] = new PassDataToSql("@IsActive", IsActive, PassDataToSql.ParaTypeList.Bit, PassDataToSql.ParaDirectonList.Input);
        paramList[3] = new PassDataToSql("@ModifiedBy", ModifiedBy, PassDataToSql.ParaTypeList.Nvarchar, PassDataToSql.ParaDirectonList.Input);
        paramList[4] = new PassDataToSql("@ModifiedDate", ModifiedDate, PassDataToSql.ParaTypeList.Date, PassDataToSql.ParaDirectonList.Input);
        paramList[5] = new PassDataToSql("@ReferenceID", "0", PassDataToSql.ParaTypeList.Int, PassDataToSql.ParaDirectonList.Output);

        daClass.execute_Sp("sp_Inv_Product_CategoryMaster_RowStatus", paramList);
        return Convert.ToInt32(paramList[5].ParaValue);
    }
    public int UpdateParentId(string CompanyId, string ParentId)
    {
        PassDataToSql[] paramList = new PassDataToSql[3];
        paramList[0] = new PassDataToSql("@Company_Id", CompanyId, PassDataToSql.ParaTypeList.Nvarchar, PassDataToSql.ParaDirectonList.Input);
        paramList[1] = new PassDataToSql("@Parent_Id", ParentId, PassDataToSql.ParaTypeList.Nvarchar, PassDataToSql.ParaDirectonList.Input);
        paramList[2] = new PassDataToSql("@ReferenceID", "0", PassDataToSql.ParaTypeList.Int, PassDataToSql.ParaDirectonList.Output);

        daClass.execute_Sp("sp_Inv_Product_CategoryMaster_UpdateParent", paramList);
        return Convert.ToInt32(paramList[2].ParaValue);
    }
    public int UpdateParentIdbyCategoryId(string strCompany_Id, string strCategory_Id, string strParent_Id)
    {
        PassDataToSql[] paramList = new PassDataToSql[4];
        paramList[0] = new PassDataToSql("@Company_Id", strCompany_Id, PassDataToSql.ParaTypeList.Nvarchar, PassDataToSql.ParaDirectonList.Input);
        paramList[1] = new PassDataToSql("@Category_Id", strCategory_Id, PassDataToSql.ParaTypeList.Nvarchar, PassDataToSql.ParaDirectonList.Input);
        paramList[2] = new PassDataToSql("@Parent_Id", strParent_Id, PassDataToSql.ParaTypeList.Nvarchar, PassDataToSql.ParaDirectonList.Input);
        paramList[3] = new PassDataToSql("@ReferenceID", "0", PassDataToSql.ParaTypeList.Int, PassDataToSql.ParaDirectonList.Output);

        daClass.execute_Sp("sp_Inv_Product_CategoryMaster_UpdateParentIdOfCategoryId", paramList);
        return Convert.ToInt32(paramList[3].ParaValue);
    }
    public DataTable GetProductCategoryTrueAllData(string strCompanyId)
    {
        DataTable dtInfo = new DataTable();
        PassDataToSql[] paramList = new PassDataToSql[4];
        paramList[0] = new PassDataToSql("@Company_Id", strCompanyId, PassDataToSql.ParaTypeList.Nvarchar, PassDataToSql.ParaDirectonList.Input);
        paramList[1] = new PassDataToSql("@Category_Id", "", PassDataToSql.ParaTypeList.Nvarchar, PassDataToSql.ParaDirectonList.Input);
        paramList[2] = new PassDataToSql("@Category_Name", "", PassDataToSql.ParaTypeList.Nvarchar, PassDataToSql.ParaDirectonList.Input);
        paramList[3] = new PassDataToSql("@optype", "1", PassDataToSql.ParaTypeList.Nvarchar, PassDataToSql.ParaDirectonList.Input);

        dtInfo = daClass.Reuturn_Datatable_Search("sp_Inv_Product_CategoryMaster_SelectRow", paramList);
        return dtInfo;
    }
    public DataTable GetProductCategoryAllData(string strCompanyId)
    {
        DataTable dtInfo = new DataTable();
        PassDataToSql[] paramList = new PassDataToSql[4];
        paramList[0] = new PassDataToSql("@Company_Id", strCompanyId, PassDataToSql.ParaTypeList.Nvarchar, PassDataToSql.ParaDirectonList.Input);
        paramList[1] = new PassDataToSql("@Category_Id", "", PassDataToSql.ParaTypeList.Nvarchar, PassDataToSql.ParaDirectonList.Input);
        paramList[2] = new PassDataToSql("@Category_Name", "", PassDataToSql.ParaTypeList.Nvarchar, PassDataToSql.ParaDirectonList.Input);
        paramList[3] = new PassDataToSql("@optype", "2", PassDataToSql.ParaTypeList.Nvarchar, PassDataToSql.ParaDirectonList.Input);

        dtInfo = daClass.Reuturn_Datatable_Search("sp_Inv_Product_CategoryMaster_SelectRow", paramList);
        return dtInfo;
    }
    public DataTable GetProductCategoryFalseAllData(string strCompanyId)
    {
        DataTable dtInfo = new DataTable();
        PassDataToSql[] paramList = new PassDataToSql[4];
        paramList[0] = new PassDataToSql("@Company_Id", strCompanyId, PassDataToSql.ParaTypeList.Nvarchar, PassDataToSql.ParaDirectonList.Input);
        paramList[1] = new PassDataToSql("@Category_Id", "", PassDataToSql.ParaTypeList.Nvarchar, PassDataToSql.ParaDirectonList.Input);
        paramList[2] = new PassDataToSql("@Category_Name", "", PassDataToSql.ParaTypeList.Nvarchar, PassDataToSql.ParaDirectonList.Input);
        paramList[3] = new PassDataToSql("@optype", "3", PassDataToSql.ParaTypeList.Nvarchar, PassDataToSql.ParaDirectonList.Input);

        dtInfo = daClass.Reuturn_Datatable_Search("sp_Inv_Product_CategoryMaster_SelectRow", paramList);
        return dtInfo;
    }
    public DataTable GetProductCategoryByCategoryId(string strCompanyId, string strCategory_Id)
    {
        DataTable dtInfo = new DataTable();
        PassDataToSql[] paramList = new PassDataToSql[4];
        paramList[0] = new PassDataToSql("@Company_Id", strCompanyId, PassDataToSql.ParaTypeList.Nvarchar, PassDataToSql.ParaDirectonList.Input);
        paramList[1] = new PassDataToSql("@Category_Id", strCategory_Id, PassDataToSql.ParaTypeList.Nvarchar, PassDataToSql.ParaDirectonList.Input);
        paramList[2] = new PassDataToSql("@Category_Name", "", PassDataToSql.ParaTypeList.Nvarchar, PassDataToSql.ParaDirectonList.Input);
        paramList[3] = new PassDataToSql("@optype", "4", PassDataToSql.ParaTypeList.Nvarchar, PassDataToSql.ParaDirectonList.Input);

        dtInfo = daClass.Reuturn_Datatable_Search("sp_Inv_Product_CategoryMaster_SelectRow", paramList);
        return dtInfo;
    }
    public DataTable GetProductCategoryByCategoryName(string strCompanyId, string strCategory_Name)
    {
        DataTable dtInfo = new DataTable();
        PassDataToSql[] paramList = new PassDataToSql[4];
        paramList[0] = new PassDataToSql("@Company_Id", strCompanyId, PassDataToSql.ParaTypeList.Nvarchar, PassDataToSql.ParaDirectonList.Input);
        paramList[1] = new PassDataToSql("@Category_Id", "", PassDataToSql.ParaTypeList.Nvarchar, PassDataToSql.ParaDirectonList.Input);
        paramList[2] = new PassDataToSql("@Category_Name", strCategory_Name, PassDataToSql.ParaTypeList.Nvarchar, PassDataToSql.ParaDirectonList.Input);
        paramList[3] = new PassDataToSql("@optype", "5", PassDataToSql.ParaTypeList.Nvarchar, PassDataToSql.ParaDirectonList.Input);

        dtInfo = daClass.Reuturn_Datatable_Search("sp_Inv_Product_CategoryMaster_SelectRow", paramList);
        return dtInfo;
    }
    public DataTable GetDistinctCategoryName(string strCompanyId, string strPrefixText)
    {
        DataTable dtInfo = GetProductCategoryTrueAllData(strCompanyId.ToString());
    
        dtInfo = new DataView(dtInfo, "Category_Name Like '" + strPrefixText + "%'", "", DataViewRowState.CurrentRows).ToTable();

        return dtInfo;
    }
}
