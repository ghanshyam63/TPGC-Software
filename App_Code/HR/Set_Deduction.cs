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
/// Summary description for Set_Deduction
/// </summary>
public class Set_Deduction
{
    DataAccessClass daClass = new DataAccessClass();
    public Set_Deduction()
    {
        //
        // TODO: Add constructor logic here
        //
    }
    public int InsertDeduction(string CompanyId, string Deduction, string Deduction_L, string Field1, string Field2, string Field3, string Field4, string Field5, string Field6, string Field7, string strIsActive, string strCreatedBy, string strCreatedDate, string strModifiedBy, string strModifiedDate)
    {
        PassDataToSql[] paramList = new PassDataToSql[16];
        paramList[0] = new PassDataToSql("@Company_Id", CompanyId, PassDataToSql.ParaTypeList.Int, PassDataToSql.ParaDirectonList.Input);
        paramList[1] = new PassDataToSql("@Deduction", Deduction, PassDataToSql.ParaTypeList.Nvarchar, PassDataToSql.ParaDirectonList.Input);
        paramList[2] = new PassDataToSql("@Deduction_L", Deduction_L, PassDataToSql.ParaTypeList.Nvarchar, PassDataToSql.ParaDirectonList.Input);

        paramList[3] = new PassDataToSql("@Field1", Field1, PassDataToSql.ParaTypeList.Nvarchar, PassDataToSql.ParaDirectonList.Input);
        paramList[4] = new PassDataToSql("@Field2", Field2, PassDataToSql.ParaTypeList.Nvarchar, PassDataToSql.ParaDirectonList.Input);
        paramList[5] = new PassDataToSql("@Field3", Field3, PassDataToSql.ParaTypeList.Nvarchar, PassDataToSql.ParaDirectonList.Input);
        paramList[6] = new PassDataToSql("@Field4", Field4, PassDataToSql.ParaTypeList.Nvarchar, PassDataToSql.ParaDirectonList.Input);
        paramList[7] = new PassDataToSql("@Field5", Field5, PassDataToSql.ParaTypeList.Nvarchar, PassDataToSql.ParaDirectonList.Input);
        paramList[8] = new PassDataToSql("@Field6", Field6, PassDataToSql.ParaTypeList.Bit, PassDataToSql.ParaDirectonList.Input);
        paramList[9] = new PassDataToSql("@Field7", Field7, PassDataToSql.ParaTypeList.Date, PassDataToSql.ParaDirectonList.Input);


        paramList[10] = new PassDataToSql("@IsActive", strIsActive, PassDataToSql.ParaTypeList.Bit, PassDataToSql.ParaDirectonList.Input);
        paramList[11] = new PassDataToSql("@CreatedBy", strCreatedBy, PassDataToSql.ParaTypeList.Nvarchar, PassDataToSql.ParaDirectonList.Input);
        paramList[12] = new PassDataToSql("@CreatedDate", strCreatedDate, PassDataToSql.ParaTypeList.Date, PassDataToSql.ParaDirectonList.Input);
        paramList[13] = new PassDataToSql("@ModifiedBy", strModifiedBy, PassDataToSql.ParaTypeList.Nvarchar, PassDataToSql.ParaDirectonList.Input);
        paramList[14] = new PassDataToSql("@ModifiedDate", strModifiedDate, PassDataToSql.ParaTypeList.Date, PassDataToSql.ParaDirectonList.Input);
        paramList[15] = new PassDataToSql("@ReferenceID", "0", PassDataToSql.ParaTypeList.Int, PassDataToSql.ParaDirectonList.Output);

        daClass.execute_Sp("sp_Set_DeductionMaster_Insert", paramList);
        return Convert.ToInt32(paramList[15].ParaValue);
    }
    public int UpdateDeduction(string CompanyId,string strDeduction_Id, string Deduction, string Deduction_L, string Field1, string Field2, string Field3, string Field4, string Field5, string Field6, string Field7, string strIsActive, string strModifiedBy, string strModifiedDate)
    {
        PassDataToSql[] paramList = new PassDataToSql[15];
        paramList[0] = new PassDataToSql("@Company_Id", CompanyId, PassDataToSql.ParaTypeList.Int, PassDataToSql.ParaDirectonList.Input);
        paramList[1] = new PassDataToSql("@Deduction_Id", strDeduction_Id, PassDataToSql.ParaTypeList.Int, PassDataToSql.ParaDirectonList.Input);
        paramList[2] = new PassDataToSql("@Deduction", Deduction, PassDataToSql.ParaTypeList.Nvarchar, PassDataToSql.ParaDirectonList.Input);
        paramList[3] = new PassDataToSql("@Deduction_L", Deduction_L, PassDataToSql.ParaTypeList.Nvarchar, PassDataToSql.ParaDirectonList.Input);
        paramList[4] = new PassDataToSql("@Field1", Field1, PassDataToSql.ParaTypeList.Nvarchar, PassDataToSql.ParaDirectonList.Input);
        paramList[5] = new PassDataToSql("@Field2", Field2, PassDataToSql.ParaTypeList.Nvarchar, PassDataToSql.ParaDirectonList.Input);
        paramList[6] = new PassDataToSql("@Field3", Field3, PassDataToSql.ParaTypeList.Nvarchar, PassDataToSql.ParaDirectonList.Input);
        paramList[7] = new PassDataToSql("@Field4", Field4, PassDataToSql.ParaTypeList.Nvarchar, PassDataToSql.ParaDirectonList.Input);
        paramList[8] = new PassDataToSql("@Field5", Field5, PassDataToSql.ParaTypeList.Nvarchar, PassDataToSql.ParaDirectonList.Input);
        paramList[9] = new PassDataToSql("@Field6", Field6, PassDataToSql.ParaTypeList.Bit, PassDataToSql.ParaDirectonList.Input);
        paramList[10] = new PassDataToSql("@Field7", Field7, PassDataToSql.ParaTypeList.Date, PassDataToSql.ParaDirectonList.Input);
        paramList[11] = new PassDataToSql("@IsActive", strIsActive, PassDataToSql.ParaTypeList.Bit, PassDataToSql.ParaDirectonList.Input);
        paramList[12] = new PassDataToSql("@ModifiedBy", strModifiedBy, PassDataToSql.ParaTypeList.Nvarchar, PassDataToSql.ParaDirectonList.Input);
        paramList[13] = new PassDataToSql("@ModifiedDate", strModifiedDate, PassDataToSql.ParaTypeList.Date, PassDataToSql.ParaDirectonList.Input);
        paramList[14] = new PassDataToSql("@ReferenceID", "0", PassDataToSql.ParaTypeList.Int, PassDataToSql.ParaDirectonList.Output);

        daClass.execute_Sp("sp_Set_DeductionMaster_Update", paramList);
        return Convert.ToInt32(paramList[14].ParaValue);
    }
    public int DeleteDeduction(string CompanyId,string strDeduction_Id, string strIsActive, string strModifiedBy, string strModifiedDate)
    {
        PassDataToSql[] paramList = new PassDataToSql[6];
        paramList[0] = new PassDataToSql("@Company_Id", CompanyId, PassDataToSql.ParaTypeList.Int, PassDataToSql.ParaDirectonList.Input);
        paramList[1] = new PassDataToSql("@Deduction_Id", strDeduction_Id, PassDataToSql.ParaTypeList.Int, PassDataToSql.ParaDirectonList.Input);
        paramList[2] = new PassDataToSql("@IsActive", strIsActive, PassDataToSql.ParaTypeList.Nvarchar, PassDataToSql.ParaDirectonList.Input);
        paramList[3] = new PassDataToSql("@ModifiedBy", strModifiedBy, PassDataToSql.ParaTypeList.Nvarchar, PassDataToSql.ParaDirectonList.Input);
        paramList[4] = new PassDataToSql("@ModifiedDate", strModifiedDate, PassDataToSql.ParaTypeList.Date, PassDataToSql.ParaDirectonList.Input);
        paramList[5] = new PassDataToSql("@ReferenceID", "0", PassDataToSql.ParaTypeList.Int, PassDataToSql.ParaDirectonList.Output);

        daClass.execute_Sp("sp_Set_DeductionMaster_RowStatus", paramList);
        return Convert.ToInt32(paramList[5].ParaValue);
    }
    public DataTable GetDeductionTrueAll(string CompanyId)
    {
        DataTable dtInfo = new DataTable();
        PassDataToSql[] paramList = new PassDataToSql[4];
        paramList[0] = new PassDataToSql("@Company_Id", CompanyId, PassDataToSql.ParaTypeList.Int, PassDataToSql.ParaDirectonList.Input);
        paramList[1] = new PassDataToSql("@Deduction_Id", "0", PassDataToSql.ParaTypeList.Int, PassDataToSql.ParaDirectonList.Input);
        paramList[2] = new PassDataToSql("@Deduction", "", PassDataToSql.ParaTypeList.Nvarchar, PassDataToSql.ParaDirectonList.Input);
        paramList[3] = new PassDataToSql("@optype", "2", PassDataToSql.ParaTypeList.Nvarchar, PassDataToSql.ParaDirectonList.Input);

        dtInfo = daClass.Reuturn_Datatable_Search("sp_Set_DeductionMaster_SelectRow", paramList);
        return dtInfo;
    }
    public DataTable GetDeductionAll(string CompanyId)
    {
        DataTable dtInfo = new DataTable();
        PassDataToSql[] paramList = new PassDataToSql[4];
        paramList[0] = new PassDataToSql("@Company_Id", CompanyId, PassDataToSql.ParaTypeList.Int, PassDataToSql.ParaDirectonList.Input);
        paramList[1] = new PassDataToSql("@Deduction_Id", "0", PassDataToSql.ParaTypeList.Int, PassDataToSql.ParaDirectonList.Input);
        paramList[2] = new PassDataToSql("@Deduction", "", PassDataToSql.ParaTypeList.Nvarchar, PassDataToSql.ParaDirectonList.Input);
        paramList[3] = new PassDataToSql("@optype", "1", PassDataToSql.ParaTypeList.Nvarchar, PassDataToSql.ParaDirectonList.Input);

        dtInfo = daClass.Reuturn_Datatable_Search("sp_Set_DeductionMaster_SelectRow", paramList);
        return dtInfo;
    }
    public DataTable GetDeductionTruebyId(string CompanyId,string strDeduction_Id)
    {
        DataTable dtInfo = new DataTable();
        PassDataToSql[] paramList = new PassDataToSql[4];
        paramList[0] = new PassDataToSql("@Company_Id", CompanyId, PassDataToSql.ParaTypeList.Int, PassDataToSql.ParaDirectonList.Input);
        paramList[1] = new PassDataToSql("@Deduction_Id", strDeduction_Id, PassDataToSql.ParaTypeList.Int, PassDataToSql.ParaDirectonList.Input);
        paramList[2] = new PassDataToSql("@Deduction", "", PassDataToSql.ParaTypeList.Nvarchar, PassDataToSql.ParaDirectonList.Input);
        paramList[3] = new PassDataToSql("@optype", "3", PassDataToSql.ParaTypeList.Nvarchar, PassDataToSql.ParaDirectonList.Input);

        dtInfo = daClass.Reuturn_Datatable_Search("sp_Set_DeductionMaster_SelectRow", paramList);
        return dtInfo;
    }
    public DataTable GetDeductionByDeduction(string CompanyId, string Deduction)
    {
        DataTable dtInfo = new DataTable();
        PassDataToSql[] paramList = new PassDataToSql[4];
        paramList[0] = new PassDataToSql("@Company_Id", CompanyId, PassDataToSql.ParaTypeList.Int, PassDataToSql.ParaDirectonList.Input);
        paramList[1] = new PassDataToSql("@Deduction_Id", "0", PassDataToSql.ParaTypeList.Int, PassDataToSql.ParaDirectonList.Input);
        paramList[2] = new PassDataToSql("@Deduction", Deduction, PassDataToSql.ParaTypeList.Nvarchar, PassDataToSql.ParaDirectonList.Input);
        paramList[3] = new PassDataToSql("@optype", "5", PassDataToSql.ParaTypeList.Nvarchar, PassDataToSql.ParaDirectonList.Input);

        dtInfo = daClass.Reuturn_Datatable_Search("sp_Set_DeductionMaster_SelectRow", paramList);
        return dtInfo;
    }
    public DataTable GetDeductionFalseAll(string CompanyId)
    {
        DataTable dtInfo = new DataTable();
        PassDataToSql[] paramList = new PassDataToSql[4];
        paramList[0] = new PassDataToSql("@Company_Id", CompanyId, PassDataToSql.ParaTypeList.Int, PassDataToSql.ParaDirectonList.Input);
        paramList[1] = new PassDataToSql("@Deduction_Id", "0", PassDataToSql.ParaTypeList.Int, PassDataToSql.ParaDirectonList.Input);
        paramList[2] = new PassDataToSql("@Deduction", "", PassDataToSql.ParaTypeList.Nvarchar, PassDataToSql.ParaDirectonList.Input);
        paramList[3] = new PassDataToSql("@optype", "4", PassDataToSql.ParaTypeList.Nvarchar, PassDataToSql.ParaDirectonList.Input);

        dtInfo = daClass.Reuturn_Datatable_Search("sp_Set_DeductionMaster_SelectRow", paramList);
        return dtInfo;
    }
    public DataTable GetDistinctDeduction(string CompanyId,string prefixlength)
    {
        DataTable dtInfo = new DataTable();
        PassDataToSql[] paramList = new PassDataToSql[4];
        paramList[0] = new PassDataToSql("@Company_Id", CompanyId, PassDataToSql.ParaTypeList.Int, PassDataToSql.ParaDirectonList.Input);
        paramList[1] = new PassDataToSql("@Deduction_Id", "0", PassDataToSql.ParaTypeList.Int, PassDataToSql.ParaDirectonList.Input);
        paramList[2] = new PassDataToSql("@Deduction", prefixlength, PassDataToSql.ParaTypeList.Nvarchar, PassDataToSql.ParaDirectonList.Input);
        paramList[3] = new PassDataToSql("@optype", "6", PassDataToSql.ParaTypeList.Nvarchar, PassDataToSql.ParaDirectonList.Input);

        dtInfo = daClass.Reuturn_Datatable_Search("sp_Set_DeductionMaster_SelectRow", paramList);
        return dtInfo;
    
    }
}
