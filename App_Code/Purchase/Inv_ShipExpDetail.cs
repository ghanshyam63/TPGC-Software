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
/// Summary description for Inv_ShipExpDetail
/// </summary>
public class Inv_ShipExpDetail
{
    DataAccessClass daClass = new DataAccessClass();
    public Inv_ShipExpDetail()
    {
        //
        // TODO: Add constructor logic here
        //
    }
    	
    public void ShipExpDetail_Insert(string CompanyId, string BrandId, string LocationId, string InvoiceId, string ExpensesId, string AccountNo, string ExpensesCharges,string ExpCurrencyID,string  ExpExchangeRate,string FCExpAmount,string Field1, string Field2, string Field3, string Field4, string Field5, string Field6, string Field7, string IsActive, string CreatedBy, string CreatedDate, string ModifiedBy, string ModfiedDate)
    {
        PassDataToSql[] paramList = new PassDataToSql[22];
        paramList[0] = new PassDataToSql("@Company_Id", CompanyId, PassDataToSql.ParaTypeList.Int, PassDataToSql.ParaDirectonList.Input);
        paramList[1] = new PassDataToSql("@Brand_Id", BrandId, PassDataToSql.ParaTypeList.Int, PassDataToSql.ParaDirectonList.Input);
        paramList[2] = new PassDataToSql("@Location_Id", LocationId, PassDataToSql.ParaTypeList.Int, PassDataToSql.ParaDirectonList.Input);

        paramList[3] = new PassDataToSql("@InvoiceNo", InvoiceId, PassDataToSql.ParaTypeList.Int, PassDataToSql.ParaDirectonList.Input);
        paramList[4] = new PassDataToSql("@Expense_Id", ExpensesId, PassDataToSql.ParaTypeList.Int, PassDataToSql.ParaDirectonList.Input);
        paramList[5] = new PassDataToSql("@Account_No", AccountNo, PassDataToSql.ParaTypeList.Nvarchar, PassDataToSql.ParaDirectonList.Input);


        paramList[6] = new PassDataToSql("@Exp_Charges", ExpensesCharges, PassDataToSql.ParaTypeList.Float, PassDataToSql.ParaDirectonList.Input);
        paramList[7] = new PassDataToSql("@Field1", Field1, PassDataToSql.ParaTypeList.Nvarchar, PassDataToSql.ParaDirectonList.Input);
        paramList[8] = new PassDataToSql("@Field2", Field2, PassDataToSql.ParaTypeList.Nvarchar, PassDataToSql.ParaDirectonList.Input);

        paramList[9] = new PassDataToSql("@Field3", Field3, PassDataToSql.ParaTypeList.Nvarchar, PassDataToSql.ParaDirectonList.Input);
        paramList[10] = new PassDataToSql("@Field4", Field4, PassDataToSql.ParaTypeList.Nvarchar, PassDataToSql.ParaDirectonList.Input);

        paramList[11] = new PassDataToSql("@Field5", Field5, PassDataToSql.ParaTypeList.Nvarchar, PassDataToSql.ParaDirectonList.Input);
        paramList[12] = new PassDataToSql("@Field6", Field6, PassDataToSql.ParaTypeList.Bit, PassDataToSql.ParaDirectonList.Input);

        paramList[13] = new PassDataToSql("@Field7", Field7, PassDataToSql.ParaTypeList.DateTime, PassDataToSql.ParaDirectonList.Input);
        paramList[14] = new PassDataToSql("@IsActive", IsActive, PassDataToSql.ParaTypeList.Bit, PassDataToSql.ParaDirectonList.Input);

        paramList[15] = new PassDataToSql("@CreatedBy", CreatedBy, PassDataToSql.ParaTypeList.Nvarchar, PassDataToSql.ParaDirectonList.Input);
        paramList[16] = new PassDataToSql("@CreatedDate", CreatedDate, PassDataToSql.ParaTypeList.DateTime, PassDataToSql.ParaDirectonList.Input);

        paramList[17] = new PassDataToSql("@ModifiedBy", CreatedBy, PassDataToSql.ParaTypeList.Nvarchar, PassDataToSql.ParaDirectonList.Input);
        paramList[18] = new PassDataToSql("@ModifiedDate", CreatedDate, PassDataToSql.ParaTypeList.DateTime, PassDataToSql.ParaDirectonList.Input);

        paramList[19] = new PassDataToSql("@ExpCurrencyID", ExpCurrencyID, PassDataToSql.ParaTypeList.Int, PassDataToSql.ParaDirectonList.Input);
        paramList[20] = new PassDataToSql("@ExpExchangeRate", ExpExchangeRate, PassDataToSql.ParaTypeList.Float, PassDataToSql.ParaDirectonList.Input);
        paramList[21] = new PassDataToSql("@FCExpAmount", FCExpAmount, PassDataToSql.ParaTypeList.Nvarchar, PassDataToSql.ParaDirectonList.Input);
    
        daClass.execute_Sp("sp_Inv_ShipExpDetail_Insert", paramList);

    }
    public void ShipExpDetail_Update(string CompanyId, string BrandId, string LocationId, string InvoiceId, string ExpensesId, string AccountNo, string ExpensesCharges, string ExpCurrencyID, string ExpExchangeRate, string FCExpAmount, string Field1, string Field2, string Field3, string Field4, string Field5, string Field6, string Field7, string IsActive, string CreatedBy, string CreatedDate, string ModifiedBy, string ModfiedDate)
    {
        PassDataToSql[] paramList = new PassDataToSql[22];
        paramList[0] = new PassDataToSql("@Company_Id", CompanyId, PassDataToSql.ParaTypeList.Int, PassDataToSql.ParaDirectonList.Input);
        paramList[1] = new PassDataToSql("@Brand_Id", BrandId, PassDataToSql.ParaTypeList.Int, PassDataToSql.ParaDirectonList.Input);
        paramList[2] = new PassDataToSql("@Location_Id", LocationId, PassDataToSql.ParaTypeList.Int, PassDataToSql.ParaDirectonList.Input);

        paramList[3] = new PassDataToSql("@InvoiceNo", InvoiceId, PassDataToSql.ParaTypeList.Int, PassDataToSql.ParaDirectonList.Input);
        paramList[4] = new PassDataToSql("@Expense_Id", ExpensesId, PassDataToSql.ParaTypeList.Int, PassDataToSql.ParaDirectonList.Input);
        paramList[5] = new PassDataToSql("@Account_No", AccountNo, PassDataToSql.ParaTypeList.Nvarchar, PassDataToSql.ParaDirectonList.Input);


        paramList[6] = new PassDataToSql("@Exp_Charges", ExpensesCharges, PassDataToSql.ParaTypeList.Float, PassDataToSql.ParaDirectonList.Input);
        paramList[7] = new PassDataToSql("@Field1", Field1, PassDataToSql.ParaTypeList.Nvarchar, PassDataToSql.ParaDirectonList.Input);
        paramList[8] = new PassDataToSql("@Field2", Field2, PassDataToSql.ParaTypeList.Nvarchar, PassDataToSql.ParaDirectonList.Input);

        paramList[9] = new PassDataToSql("@Field3", Field3, PassDataToSql.ParaTypeList.Nvarchar, PassDataToSql.ParaDirectonList.Input);
        paramList[10] = new PassDataToSql("@Field4", Field4, PassDataToSql.ParaTypeList.Nvarchar, PassDataToSql.ParaDirectonList.Input);

        paramList[11] = new PassDataToSql("@Field5", Field5, PassDataToSql.ParaTypeList.Nvarchar, PassDataToSql.ParaDirectonList.Input);
        paramList[12] = new PassDataToSql("@Field6", Field6, PassDataToSql.ParaTypeList.Bit, PassDataToSql.ParaDirectonList.Input);

        paramList[13] = new PassDataToSql("@Field7", Field7, PassDataToSql.ParaTypeList.DateTime, PassDataToSql.ParaDirectonList.Input);
        paramList[14] = new PassDataToSql("@IsActive", IsActive, PassDataToSql.ParaTypeList.Bit, PassDataToSql.ParaDirectonList.Input);

        paramList[15] = new PassDataToSql("@CreatedBy", CreatedBy, PassDataToSql.ParaTypeList.Nvarchar, PassDataToSql.ParaDirectonList.Input);
        paramList[16] = new PassDataToSql("@CreatedDate", CreatedDate, PassDataToSql.ParaTypeList.DateTime, PassDataToSql.ParaDirectonList.Input);

        paramList[17] = new PassDataToSql("@ModifiedBy", CreatedBy, PassDataToSql.ParaTypeList.Nvarchar, PassDataToSql.ParaDirectonList.Input);
        paramList[18] = new PassDataToSql("@ModifiedDate", CreatedDate, PassDataToSql.ParaTypeList.DateTime, PassDataToSql.ParaDirectonList.Input);
        
        paramList[19] = new PassDataToSql("@ExpCurrencyID", ExpCurrencyID, PassDataToSql.ParaTypeList.Int, PassDataToSql.ParaDirectonList.Input);
        paramList[20] = new PassDataToSql("@ExpExchangeRate", ExpExchangeRate, PassDataToSql.ParaTypeList.Float, PassDataToSql.ParaDirectonList.Input);
        paramList[21] = new PassDataToSql("@FCExpAmount", FCExpAmount, PassDataToSql.ParaTypeList.Nvarchar, PassDataToSql.ParaDirectonList.Input);
   
        daClass.execute_Sp("sp_Inv_ShipExpDetail_Update", paramList);

    }
    public void ShipExpDetail_Delete(string CompanyId, string BrandId, string LocationId, string InvoiceId, string ExpensesId)
    {
        PassDataToSql[] paramList = new PassDataToSql[6];
        paramList[0] = new PassDataToSql("@Company_Id", CompanyId, PassDataToSql.ParaTypeList.Int, PassDataToSql.ParaDirectonList.Input);
        paramList[1] = new PassDataToSql("@Brand_Id", BrandId, PassDataToSql.ParaTypeList.Int, PassDataToSql.ParaDirectonList.Input);
        paramList[2] = new PassDataToSql("@Location_Id", LocationId, PassDataToSql.ParaTypeList.Int, PassDataToSql.ParaDirectonList.Input);

        paramList[3] = new PassDataToSql("@InvoiceNo", InvoiceId, PassDataToSql.ParaTypeList.Int, PassDataToSql.ParaDirectonList.Input);
        paramList[4] = new PassDataToSql("@Expense_Id", ExpensesId, PassDataToSql.ParaTypeList.Int, PassDataToSql.ParaDirectonList.Input);
        paramList[5] = new PassDataToSql("@optype", "1", PassDataToSql.ParaTypeList.Int, PassDataToSql.ParaDirectonList.Input);
       
        daClass.execute_Sp("sp_Inv_ShipExpDetail_DeleteRow", paramList);

    }
    public void ShipExpDetail_Delete(string CompanyId, string BrandId, string LocationId, string InvoiceId)
    {
        PassDataToSql[] paramList = new PassDataToSql[6];
        paramList[0] = new PassDataToSql("@Company_Id", CompanyId, PassDataToSql.ParaTypeList.Int, PassDataToSql.ParaDirectonList.Input);
        paramList[1] = new PassDataToSql("@Brand_Id", BrandId, PassDataToSql.ParaTypeList.Int, PassDataToSql.ParaDirectonList.Input);
        paramList[2] = new PassDataToSql("@Location_Id", LocationId, PassDataToSql.ParaTypeList.Int, PassDataToSql.ParaDirectonList.Input);

        paramList[3] = new PassDataToSql("@InvoiceNo", InvoiceId, PassDataToSql.ParaTypeList.Int, PassDataToSql.ParaDirectonList.Input);
        paramList[4] = new PassDataToSql("@Expense_Id", "0", PassDataToSql.ParaTypeList.Int, PassDataToSql.ParaDirectonList.Input);
        paramList[5] = new PassDataToSql("@optype", "2", PassDataToSql.ParaTypeList.Int, PassDataToSql.ParaDirectonList.Input);

        daClass.execute_Sp("sp_Inv_ShipExpDetail_DeleteRow", paramList);

    }
    public DataTable Get_ShipExpDetailByInvoiceId(string CompanyId, string BrandId, string LocationId, string InvoiceId)
    {
        PassDataToSql[] paramList = new PassDataToSql[6];
        paramList[0] = new PassDataToSql("@Company_Id", CompanyId, PassDataToSql.ParaTypeList.Int, PassDataToSql.ParaDirectonList.Input);
        paramList[1] = new PassDataToSql("@Brand_Id", BrandId, PassDataToSql.ParaTypeList.Int, PassDataToSql.ParaDirectonList.Input);
        paramList[2] = new PassDataToSql("@Location_Id", LocationId, PassDataToSql.ParaTypeList.Int, PassDataToSql.ParaDirectonList.Input);

        paramList[3] = new PassDataToSql("@InvoiceNo", InvoiceId, PassDataToSql.ParaTypeList.Int, PassDataToSql.ParaDirectonList.Input);
        paramList[4] = new PassDataToSql("@Expense_Id", "0", PassDataToSql.ParaTypeList.Int, PassDataToSql.ParaDirectonList.Input);
        paramList[5] = new PassDataToSql("@Optype", "1", PassDataToSql.ParaTypeList.Int, PassDataToSql.ParaDirectonList.Input);

        DataTable dt = daClass.Reuturn_Datatable_Search("sp_Inv_ShipExpDetail_SelectRow", paramList);
        return dt;
    }
    public DataTable Get_ShipExpDetailByInvoiceIdandExpId(string CompanyId, string BrandId, string LocationId, string InvoiceId, string ExpensesId)
    {
        PassDataToSql[] paramList = new PassDataToSql[6];
        paramList[0] = new PassDataToSql("@Company_Id", CompanyId, PassDataToSql.ParaTypeList.Int, PassDataToSql.ParaDirectonList.Input);
        paramList[1] = new PassDataToSql("@Brand_Id", BrandId, PassDataToSql.ParaTypeList.Int, PassDataToSql.ParaDirectonList.Input);
        paramList[2] = new PassDataToSql("@Location_Id", LocationId, PassDataToSql.ParaTypeList.Int, PassDataToSql.ParaDirectonList.Input);

        paramList[3] = new PassDataToSql("@InvoiceNo", InvoiceId, PassDataToSql.ParaTypeList.Int, PassDataToSql.ParaDirectonList.Input);
        paramList[4] = new PassDataToSql("@Expense_Id", ExpensesId, PassDataToSql.ParaTypeList.Int, PassDataToSql.ParaDirectonList.Input);
        paramList[5] = new PassDataToSql("@Optype", "2", PassDataToSql.ParaTypeList.Int, PassDataToSql.ParaDirectonList.Input);

        DataTable dt = daClass.Reuturn_Datatable_Search("sp_Inv_ShipExpDetail_SelectRow", paramList);
        return dt;
    }
}
