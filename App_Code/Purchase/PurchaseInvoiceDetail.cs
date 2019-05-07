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
/// Summary description for PurchaseInvoiceDetail
/// </summary>
public class PurchaseInvoiceDetail
{
    DataAccessClass daClass = new DataAccessClass();
    public PurchaseInvoiceDetail()
    {
        //
        // TODO: Add constructor logic here
        //
    }

    public int InsertPurchaseInvoiceDetail(string CompanyId, string brandId, string Locationid, string InvoiceNo, string SerialNo, string ProductId, string POId, string UnitId, string UnitCost, string OrderQty, string FreeQty, string InvoiceQty, string RecQty, string DiscountValue, string TaxValue, string Field1, string Field2, string Field3, string Field4, string Field5, string Field6, string Field7, string IsActive, string CreatedBy, string CreatedDate, string ModifiedBy, string ModifiedDate)
    {

        PassDataToSql[] paramList = new PassDataToSql[28];

        paramList[0] = new PassDataToSql("@Company_Id", CompanyId, PassDataToSql.ParaTypeList.Int, PassDataToSql.ParaDirectonList.Input);
        paramList[1] = new PassDataToSql("@Brand_Id", brandId, PassDataToSql.ParaTypeList.Int, PassDataToSql.ParaDirectonList.Input);
        paramList[2] = new PassDataToSql("@Location_Id", Locationid, PassDataToSql.ParaTypeList.Int, PassDataToSql.ParaDirectonList.Input);

        paramList[3] = new PassDataToSql("@InvoiceNo", InvoiceNo, PassDataToSql.ParaTypeList.Int, PassDataToSql.ParaDirectonList.Input);
        paramList[4] = new PassDataToSql("@SerialNo", SerialNo, PassDataToSql.ParaTypeList.Int, PassDataToSql.ParaDirectonList.Input);
        paramList[5] = new PassDataToSql("@ProductId", ProductId, PassDataToSql.ParaTypeList.Int, PassDataToSql.ParaDirectonList.Input);

        paramList[6] = new PassDataToSql("@POId", POId, PassDataToSql.ParaTypeList.Int, PassDataToSql.ParaDirectonList.Input);
        paramList[7] = new PassDataToSql("@UnitId", UnitId, PassDataToSql.ParaTypeList.Int, PassDataToSql.ParaDirectonList.Input);
        paramList[8] = new PassDataToSql("@UnitCost", UnitCost, PassDataToSql.ParaTypeList.Float, PassDataToSql.ParaDirectonList.Input);
        paramList[9] = new PassDataToSql("@OrderQty", OrderQty, PassDataToSql.ParaTypeList.Float, PassDataToSql.ParaDirectonList.Input);
        paramList[10] = new PassDataToSql("@FreeQty", FreeQty, PassDataToSql.ParaTypeList.Float, PassDataToSql.ParaDirectonList.Input);

        paramList[11] = new PassDataToSql("@RecQty", RecQty, PassDataToSql.ParaTypeList.Float, PassDataToSql.ParaDirectonList.Input);
        paramList[12] = new PassDataToSql("@InvoiceQty", InvoiceQty, PassDataToSql.ParaTypeList.Float, PassDataToSql.ParaDirectonList.Input);

        paramList[13] = new PassDataToSql("@TaxValue", TaxValue, PassDataToSql.ParaTypeList.Float, PassDataToSql.ParaDirectonList.Input);
        paramList[14] = new PassDataToSql("@DiscountValue", DiscountValue, PassDataToSql.ParaTypeList.Float, PassDataToSql.ParaDirectonList.Input);

        paramList[15] = new PassDataToSql("@Field1", Field1, PassDataToSql.ParaTypeList.Nvarchar, PassDataToSql.ParaDirectonList.Input);
        paramList[16] = new PassDataToSql("@Field2", Field2, PassDataToSql.ParaTypeList.Nvarchar, PassDataToSql.ParaDirectonList.Input);
        paramList[17] = new PassDataToSql("@Field3", Field3, PassDataToSql.ParaTypeList.Nvarchar, PassDataToSql.ParaDirectonList.Input);
        paramList[18] = new PassDataToSql("@Field4", Field4, PassDataToSql.ParaTypeList.Nvarchar, PassDataToSql.ParaDirectonList.Input);
        paramList[19] = new PassDataToSql("@Field5", Field5, PassDataToSql.ParaTypeList.Nvarchar, PassDataToSql.ParaDirectonList.Input);
        paramList[20] = new PassDataToSql("@Field6", Field6, PassDataToSql.ParaTypeList.Bit, PassDataToSql.ParaDirectonList.Input);
        paramList[21] = new PassDataToSql("@Field7", Field7, PassDataToSql.ParaTypeList.Date, PassDataToSql.ParaDirectonList.Input);

        paramList[22] = new PassDataToSql("@IsActive", IsActive, PassDataToSql.ParaTypeList.Bit, PassDataToSql.ParaDirectonList.Input);
        paramList[23] = new PassDataToSql("@CreatedBy", CreatedBy, PassDataToSql.ParaTypeList.Nvarchar, PassDataToSql.ParaDirectonList.Input);
        paramList[24] = new PassDataToSql("@CreatedDate", CreatedDate, PassDataToSql.ParaTypeList.DateTime, PassDataToSql.ParaDirectonList.Input);
        paramList[25] = new PassDataToSql("@ModifiedBy", ModifiedBy, PassDataToSql.ParaTypeList.Nvarchar, PassDataToSql.ParaDirectonList.Input);
        paramList[26] = new PassDataToSql("@ModifiedDate", ModifiedDate, PassDataToSql.ParaTypeList.DateTime, PassDataToSql.ParaDirectonList.Input);
        paramList[27] = new PassDataToSql("@ReferenceID", "0", PassDataToSql.ParaTypeList.Int, PassDataToSql.ParaDirectonList.Output);


        daClass.execute_Sp("sp_Inv_PurchaseInvoiceDetail_Insert", paramList);
        return Convert.ToInt32(paramList[27].ParaValue);
    }

    public void UpdatePurchaseInvoiceDetail(string CompanyId, string brandId, string Locationid, string TransId, string InvoiceNo, string SerialNo, string ProductId, string POId, string UnitId, string UnitCost, string OrderQty, string FreeQty, string InvoiceQty, string RecQty, string DiscountValue, string TaxValue, string Field1, string Field2, string Field3, string Field4, string Field5, string Field6, string Field7, string IsActive, string ModifiedBy, string ModifiedDate)
    {

        PassDataToSql[] paramList = new PassDataToSql[26];
        paramList[0] = new PassDataToSql("@Company_Id", CompanyId, PassDataToSql.ParaTypeList.Int, PassDataToSql.ParaDirectonList.Input);
        paramList[1] = new PassDataToSql("@Brand_Id", brandId, PassDataToSql.ParaTypeList.Int, PassDataToSql.ParaDirectonList.Input);
        paramList[2] = new PassDataToSql("@Location_Id", Locationid, PassDataToSql.ParaTypeList.Int, PassDataToSql.ParaDirectonList.Input);

        paramList[3] = new PassDataToSql("@InvoiceNo", InvoiceNo, PassDataToSql.ParaTypeList.Int, PassDataToSql.ParaDirectonList.Input);
        paramList[4] = new PassDataToSql("@SerialNo", SerialNo, PassDataToSql.ParaTypeList.Int, PassDataToSql.ParaDirectonList.Input);
        paramList[5] = new PassDataToSql("@ProductId", ProductId, PassDataToSql.ParaTypeList.Int, PassDataToSql.ParaDirectonList.Input);

        paramList[6] = new PassDataToSql("@POId", POId, PassDataToSql.ParaTypeList.Int, PassDataToSql.ParaDirectonList.Input);
        paramList[7] = new PassDataToSql("@UnitId", UnitId, PassDataToSql.ParaTypeList.Int, PassDataToSql.ParaDirectonList.Input);
        paramList[8] = new PassDataToSql("@UnitCost", UnitCost, PassDataToSql.ParaTypeList.Float, PassDataToSql.ParaDirectonList.Input);
        paramList[9] = new PassDataToSql("@OrderQty", OrderQty, PassDataToSql.ParaTypeList.Float, PassDataToSql.ParaDirectonList.Input);
        paramList[10] = new PassDataToSql("@FreeQty", FreeQty, PassDataToSql.ParaTypeList.Float, PassDataToSql.ParaDirectonList.Input);
        paramList[11] = new PassDataToSql("@RecQty", RecQty, PassDataToSql.ParaTypeList.Float, PassDataToSql.ParaDirectonList.Input);
        paramList[12] = new PassDataToSql("@InvoiceQty", InvoiceQty, PassDataToSql.ParaTypeList.Float, PassDataToSql.ParaDirectonList.Input);

        paramList[13] = new PassDataToSql("@TaxValue", TaxValue, PassDataToSql.ParaTypeList.Float, PassDataToSql.ParaDirectonList.Input);
        paramList[14] = new PassDataToSql("@DiscountValue", DiscountValue, PassDataToSql.ParaTypeList.Float, PassDataToSql.ParaDirectonList.Input);

        paramList[15] = new PassDataToSql("@Field1", Field1, PassDataToSql.ParaTypeList.Nvarchar, PassDataToSql.ParaDirectonList.Input);
        paramList[16] = new PassDataToSql("@Field2", Field2, PassDataToSql.ParaTypeList.Nvarchar, PassDataToSql.ParaDirectonList.Input);
        paramList[17] = new PassDataToSql("@Field3", Field3, PassDataToSql.ParaTypeList.Nvarchar, PassDataToSql.ParaDirectonList.Input);
        paramList[18] = new PassDataToSql("@Field4", Field4, PassDataToSql.ParaTypeList.Nvarchar, PassDataToSql.ParaDirectonList.Input);
        paramList[19] = new PassDataToSql("@Field5", Field5, PassDataToSql.ParaTypeList.Nvarchar, PassDataToSql.ParaDirectonList.Input);
        paramList[20] = new PassDataToSql("@Field6", Field6, PassDataToSql.ParaTypeList.Bit, PassDataToSql.ParaDirectonList.Input);
        paramList[21] = new PassDataToSql("@Field7", Field7, PassDataToSql.ParaTypeList.Date, PassDataToSql.ParaDirectonList.Input);

        paramList[22] = new PassDataToSql("@IsActive", IsActive, PassDataToSql.ParaTypeList.Bit, PassDataToSql.ParaDirectonList.Input);
        paramList[23] = new PassDataToSql("@ModifiedBy", ModifiedBy, PassDataToSql.ParaTypeList.Nvarchar, PassDataToSql.ParaDirectonList.Input);
        paramList[24] = new PassDataToSql("@ModifiedDate", ModifiedDate, PassDataToSql.ParaTypeList.DateTime, PassDataToSql.ParaDirectonList.Input);
        paramList[25] = new PassDataToSql("@TransId", TransId, PassDataToSql.ParaTypeList.Int, PassDataToSql.ParaDirectonList.Input);

        daClass.execute_Sp("sp_Inv_PurchaseInvoiceDetail_Update", paramList);

    }

    public void DeletePurchaseInvoiceDetail(string CompanyId, string brand_ID, string Location_Id, string Trans_Id)
    {

        PassDataToSql[] paramList = new PassDataToSql[4];
        paramList[0] = new PassDataToSql("@Company_Id", CompanyId, PassDataToSql.ParaTypeList.Int, PassDataToSql.ParaDirectonList.Input);
        paramList[1] = new PassDataToSql("@Brand_ID", brand_ID, PassDataToSql.ParaTypeList.Int, PassDataToSql.ParaDirectonList.Input);
        paramList[2] = new PassDataToSql("@Location_Id", Location_Id, PassDataToSql.ParaTypeList.Int, PassDataToSql.ParaDirectonList.Input);
        paramList[3] = new PassDataToSql("@TransID", Trans_Id, PassDataToSql.ParaTypeList.Int, PassDataToSql.ParaDirectonList.Input);


        daClass.execute_Sp("sp_Inv_PurchaseInvoiceDetail_DeleteRow", paramList);

    }
    public void DeletePurchaseInvoiceDetailByInvoiceNo(string CompanyId, string brand_ID, string Location_Id, string Invoice)
    {

        PassDataToSql[] paramList = new PassDataToSql[4];
        paramList[0] = new PassDataToSql("@Company_Id", CompanyId, PassDataToSql.ParaTypeList.Int, PassDataToSql.ParaDirectonList.Input);
        paramList[1] = new PassDataToSql("@Brand_ID", brand_ID, PassDataToSql.ParaTypeList.Int, PassDataToSql.ParaDirectonList.Input);
        paramList[2] = new PassDataToSql("@Location_Id", Location_Id, PassDataToSql.ParaTypeList.Int, PassDataToSql.ParaDirectonList.Input);
        paramList[3] = new PassDataToSql("@InvoiceNo", Invoice, PassDataToSql.ParaTypeList.Int, PassDataToSql.ParaDirectonList.Input);

        daClass.execute_Sp("sp_Inv_PurchaseInvoiceDetail_DeleteRowByInvoiceNo", paramList);

    }

    public DataTable GetPurchaseInvoiceDetail(string CompanyId, string BrandId, string LocationId, string TransId)
    {

        DataTable dtInfo = new DataTable();
        PassDataToSql[] paramList = new PassDataToSql[8];
        paramList[0] = new PassDataToSql("@Company_Id", CompanyId, PassDataToSql.ParaTypeList.Int, PassDataToSql.ParaDirectonList.Input);
        paramList[1] = new PassDataToSql("@Brand_Id", BrandId, PassDataToSql.ParaTypeList.Int, PassDataToSql.ParaDirectonList.Input);
        paramList[2] = new PassDataToSql("@Location_Id", LocationId, PassDataToSql.ParaTypeList.Int, PassDataToSql.ParaDirectonList.Input);
        paramList[3] = new PassDataToSql("@TransId", TransId, PassDataToSql.ParaTypeList.Int, PassDataToSql.ParaDirectonList.Input);
        paramList[4] = new PassDataToSql("@InvoiceNo", "0", PassDataToSql.ParaTypeList.Int, PassDataToSql.ParaDirectonList.Input);
        paramList[5] = new PassDataToSql("@POId", "0", PassDataToSql.ParaTypeList.Int, PassDataToSql.ParaDirectonList.Input);
        paramList[6] = new PassDataToSql("@ProductId", "0", PassDataToSql.ParaTypeList.Int, PassDataToSql.ParaDirectonList.Input);

        paramList[7] = new PassDataToSql("@optype", "1", PassDataToSql.ParaTypeList.Nvarchar, PassDataToSql.ParaDirectonList.Input);


        dtInfo = daClass.Reuturn_Datatable_Search("sp_Inv_PurchaseInvoiceDetail_SelectRow", paramList);

        return dtInfo;
    }

    public DataTable GetPurchaseInvoiceDetailByInvoiceNo(string CompanyId, string BrandId, string LocationId, string InvoiceNo)
    {

        DataTable dtInfo = new DataTable();
        PassDataToSql[] paramList = new PassDataToSql[8];
        paramList[0] = new PassDataToSql("@Company_Id", CompanyId, PassDataToSql.ParaTypeList.Int, PassDataToSql.ParaDirectonList.Input);
        paramList[1] = new PassDataToSql("@Brand_Id", BrandId, PassDataToSql.ParaTypeList.Int, PassDataToSql.ParaDirectonList.Input);
        paramList[2] = new PassDataToSql("@Location_Id", LocationId, PassDataToSql.ParaTypeList.Int, PassDataToSql.ParaDirectonList.Input);
        paramList[3] = new PassDataToSql("@TransId", "0", PassDataToSql.ParaTypeList.Int, PassDataToSql.ParaDirectonList.Input);
        paramList[4] = new PassDataToSql("@InvoiceNo", InvoiceNo, PassDataToSql.ParaTypeList.Int, PassDataToSql.ParaDirectonList.Input);
        paramList[5] = new PassDataToSql("@POId", "0", PassDataToSql.ParaTypeList.Int, PassDataToSql.ParaDirectonList.Input);
        paramList[6] = new PassDataToSql("@optype", "2", PassDataToSql.ParaTypeList.Nvarchar, PassDataToSql.ParaDirectonList.Input);
        paramList[7] = new PassDataToSql("@ProductId", "0", PassDataToSql.ParaTypeList.Int, PassDataToSql.ParaDirectonList.Input);
        dtInfo = daClass.Reuturn_Datatable_Search("sp_Inv_PurchaseInvoiceDetail_SelectRow", paramList);

        return dtInfo;
    }

    public DataTable GetPurchaseInvoiceDetailbyInvoiceId_POId(string CompanyId, string BrandId, string LocationId, string InvoiceId, string POId)
    {

        DataTable dtInfo = new DataTable();
        PassDataToSql[] paramList = new PassDataToSql[8];
        paramList[0] = new PassDataToSql("@Company_Id", CompanyId, PassDataToSql.ParaTypeList.Int, PassDataToSql.ParaDirectonList.Input);
        paramList[1] = new PassDataToSql("@Brand_Id", BrandId, PassDataToSql.ParaTypeList.Int, PassDataToSql.ParaDirectonList.Input);
        paramList[2] = new PassDataToSql("@Location_Id", LocationId, PassDataToSql.ParaTypeList.Int, PassDataToSql.ParaDirectonList.Input);
        paramList[3] = new PassDataToSql("@TransId", "0", PassDataToSql.ParaTypeList.Int, PassDataToSql.ParaDirectonList.Input);
        paramList[4] = new PassDataToSql("@InvoiceNo", InvoiceId, PassDataToSql.ParaTypeList.Int, PassDataToSql.ParaDirectonList.Input);
        paramList[5] = new PassDataToSql("@POId", POId, PassDataToSql.ParaTypeList.Int, PassDataToSql.ParaDirectonList.Input);
        paramList[6] = new PassDataToSql("@ProductId", "0", PassDataToSql.ParaTypeList.Int, PassDataToSql.ParaDirectonList.Input);
        paramList[7] = new PassDataToSql("@optype", "3", PassDataToSql.ParaTypeList.Nvarchar, PassDataToSql.ParaDirectonList.Input);
        dtInfo = daClass.Reuturn_Datatable_Search("sp_Inv_PurchaseInvoiceDetail_SelectRow", paramList);

        return dtInfo;
    }


    public DataTable GetPurchaseInvoiceDetailbyPOId_ProductId(string CompanyId, string BrandId, string LocationId, string POId, string ProductId)
    {

        DataTable dtInfo = new DataTable();
        PassDataToSql[] paramList = new PassDataToSql[8];
        paramList[0] = new PassDataToSql("@Company_Id", CompanyId, PassDataToSql.ParaTypeList.Int, PassDataToSql.ParaDirectonList.Input);
        paramList[1] = new PassDataToSql("@Brand_Id", BrandId, PassDataToSql.ParaTypeList.Int, PassDataToSql.ParaDirectonList.Input);
        paramList[2] = new PassDataToSql("@Location_Id", LocationId, PassDataToSql.ParaTypeList.Int, PassDataToSql.ParaDirectonList.Input);
        paramList[3] = new PassDataToSql("@TransId", "0", PassDataToSql.ParaTypeList.Int, PassDataToSql.ParaDirectonList.Input);
        paramList[4] = new PassDataToSql("@InvoiceNo", "0", PassDataToSql.ParaTypeList.Int, PassDataToSql.ParaDirectonList.Input);
        paramList[5] = new PassDataToSql("@POId", POId, PassDataToSql.ParaTypeList.Int, PassDataToSql.ParaDirectonList.Input);
        paramList[6] = new PassDataToSql("@ProductId", ProductId, PassDataToSql.ParaTypeList.Int, PassDataToSql.ParaDirectonList.Input);
        paramList[7] = new PassDataToSql("@optype", "4", PassDataToSql.ParaTypeList.Nvarchar, PassDataToSql.ParaDirectonList.Input);
        dtInfo = daClass.Reuturn_Datatable_Search("sp_Inv_PurchaseInvoiceDetail_SelectRow", paramList);

        return dtInfo;
    }

    public void UpdatePurchaseInvoiceDetailByGoods(string CompanyId, string brandId, string Locationid, string TransId, string Qty)
    {
        PassDataToSql[] paramList = new PassDataToSql[5];
        paramList[0] = new PassDataToSql("@Company_Id", CompanyId, PassDataToSql.ParaTypeList.Int, PassDataToSql.ParaDirectonList.Input);
        paramList[1] = new PassDataToSql("@Brand_Id", brandId, PassDataToSql.ParaTypeList.Int, PassDataToSql.ParaDirectonList.Input);
        paramList[2] = new PassDataToSql("@Location_Id", Locationid, PassDataToSql.ParaTypeList.Int, PassDataToSql.ParaDirectonList.Input);
        paramList[3] = new PassDataToSql("@RecQty", Qty, PassDataToSql.ParaTypeList.Nvarchar, PassDataToSql.ParaDirectonList.Input);
        paramList[4] = new PassDataToSql("@TransId", TransId, PassDataToSql.ParaTypeList.Int, PassDataToSql.ParaDirectonList.Input);
        daClass.execute_Sp("sp_Inv_PurchaseInvoiceDetailByGoods_Update", paramList);

    }

}
