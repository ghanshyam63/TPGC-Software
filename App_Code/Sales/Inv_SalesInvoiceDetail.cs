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
/// Summary description for Inv_SalesInvoiceDetail
/// </summary>
public class Inv_SalesInvoiceDetail
{
    DataAccessClass daClass = new DataAccessClass();
    public Inv_SalesInvoiceDetail()
    {
        //
        // TODO: Add constructor logic here
        //
    }
    public int InsertSInvDetail(string strCompany_Id, string strBrand_Id, string strLocation_Id, string strInvoice_No, string strSerial_No, string strPaymentModeId, string strPOSNo, string strSIFromTransType, string strSIFromTransNo, string strProduct_Id, string strProductDescription, string strUnit_Id, string strUnitPrice, string strUnitCost, string strQuantity, string strReturnQty, string strTaxP, string strTaxV, string strDiscountP, string strDiscountV, string strPost, string strField1, string strField2, string strField3, string strField4, string strField5, string strField6, string strField7, string strIsActive, string strCreatedBy, string strCreatedDate, string strModifiedBy, string strModifiedDate)
    {
        PassDataToSql[] paramList = new PassDataToSql[34];

        paramList[0] = new PassDataToSql("@Company_Id", strCompany_Id, PassDataToSql.ParaTypeList.Int, PassDataToSql.ParaDirectonList.Input);
        paramList[1] = new PassDataToSql("@Brand_Id", strBrand_Id, PassDataToSql.ParaTypeList.Int, PassDataToSql.ParaDirectonList.Input);
        paramList[2] = new PassDataToSql("@Location_Id", strLocation_Id, PassDataToSql.ParaTypeList.Int, PassDataToSql.ParaDirectonList.Input);
        paramList[3] = new PassDataToSql("@Invoice_No", strInvoice_No, PassDataToSql.ParaTypeList.Int, PassDataToSql.ParaDirectonList.Input);
        paramList[4] = new PassDataToSql("@Serial_No", strSerial_No, PassDataToSql.ParaTypeList.Nvarchar, PassDataToSql.ParaDirectonList.Input);
        paramList[5] = new PassDataToSql("@PaymentModeId", strPaymentModeId, PassDataToSql.ParaTypeList.Int, PassDataToSql.ParaDirectonList.Input);
        paramList[6] = new PassDataToSql("@POSNo", strPOSNo, PassDataToSql.ParaTypeList.Nvarchar, PassDataToSql.ParaDirectonList.Input);
        paramList[7] = new PassDataToSql("@SIFromTransType", strSIFromTransType, PassDataToSql.ParaTypeList.Nvarchar, PassDataToSql.ParaDirectonList.Input);
        paramList[8] = new PassDataToSql("@SIFromTransNo", strSIFromTransNo, PassDataToSql.ParaTypeList.Nvarchar, PassDataToSql.ParaDirectonList.Input);
        paramList[9] = new PassDataToSql("@Product_Id", strProduct_Id, PassDataToSql.ParaTypeList.Int, PassDataToSql.ParaDirectonList.Input);
        paramList[10] = new PassDataToSql("@ProductDescription", strProductDescription, PassDataToSql.ParaTypeList.Nvarchar, PassDataToSql.ParaDirectonList.Input);
        paramList[11] = new PassDataToSql("@Unit_Id", strUnit_Id, PassDataToSql.ParaTypeList.Int, PassDataToSql.ParaDirectonList.Input);
        paramList[12] = new PassDataToSql("@UnitPrice", strUnitPrice, PassDataToSql.ParaTypeList.Float, PassDataToSql.ParaDirectonList.Input);
        paramList[13] = new PassDataToSql("@UnitCost", strUnitCost, PassDataToSql.ParaTypeList.Float, PassDataToSql.ParaDirectonList.Input);
        paramList[14] = new PassDataToSql("@Quantity", strQuantity, PassDataToSql.ParaTypeList.Float, PassDataToSql.ParaDirectonList.Input);
        paramList[15] = new PassDataToSql("@ReturnQty", strReturnQty, PassDataToSql.ParaTypeList.Float, PassDataToSql.ParaDirectonList.Input);
        paramList[16] = new PassDataToSql("@TaxP", strTaxP, PassDataToSql.ParaTypeList.Float, PassDataToSql.ParaDirectonList.Input);
        paramList[17] = new PassDataToSql("@TaxV", strTaxV, PassDataToSql.ParaTypeList.Float, PassDataToSql.ParaDirectonList.Input);
        paramList[18] = new PassDataToSql("@DiscountP", strDiscountP, PassDataToSql.ParaTypeList.Float, PassDataToSql.ParaDirectonList.Input);
        paramList[19] = new PassDataToSql("@DiscountV", strDiscountV, PassDataToSql.ParaTypeList.Float, PassDataToSql.ParaDirectonList.Input);
        paramList[20] = new PassDataToSql("@Post", strPost, PassDataToSql.ParaTypeList.Bit, PassDataToSql.ParaDirectonList.Input);
        paramList[21] = new PassDataToSql("@Field1", strField1, PassDataToSql.ParaTypeList.Nvarchar, PassDataToSql.ParaDirectonList.Input);
        paramList[22] = new PassDataToSql("@Field2", strField2, PassDataToSql.ParaTypeList.Nvarchar, PassDataToSql.ParaDirectonList.Input);
        paramList[23] = new PassDataToSql("@Field3", strField3, PassDataToSql.ParaTypeList.Nvarchar, PassDataToSql.ParaDirectonList.Input);
        paramList[24] = new PassDataToSql("@Field4", strField4, PassDataToSql.ParaTypeList.Nvarchar, PassDataToSql.ParaDirectonList.Input);
        paramList[25] = new PassDataToSql("@Field5", strField5, PassDataToSql.ParaTypeList.Nvarchar, PassDataToSql.ParaDirectonList.Input);
        paramList[26] = new PassDataToSql("@Field6", strField6, PassDataToSql.ParaTypeList.Bit, PassDataToSql.ParaDirectonList.Input);
        paramList[27] = new PassDataToSql("@Field7", strField7, PassDataToSql.ParaTypeList.Date, PassDataToSql.ParaDirectonList.Input);
        paramList[28] = new PassDataToSql("@IsActive", strIsActive, PassDataToSql.ParaTypeList.Bit, PassDataToSql.ParaDirectonList.Input);
        paramList[29] = new PassDataToSql("@CreatedBy", strCreatedBy, PassDataToSql.ParaTypeList.Nvarchar, PassDataToSql.ParaDirectonList.Input);
        paramList[30] = new PassDataToSql("@CreatedDate", strCreatedDate, PassDataToSql.ParaTypeList.Date, PassDataToSql.ParaDirectonList.Input);
        paramList[31] = new PassDataToSql("@ModifiedBy", strModifiedBy, PassDataToSql.ParaTypeList.Nvarchar, PassDataToSql.ParaDirectonList.Input);
        paramList[32] = new PassDataToSql("@ModifiedDate", strModifiedDate, PassDataToSql.ParaTypeList.Date, PassDataToSql.ParaDirectonList.Input);
        paramList[33] = new PassDataToSql("@ReferenceID", "0", PassDataToSql.ParaTypeList.Int, PassDataToSql.ParaDirectonList.Output);

        daClass.execute_Sp("sp_Inv_SalesInvoiceDetail_Insert", paramList);
        return Convert.ToInt32(paramList[33].ParaValue);
    }
    public int UpdateSInvDetail(string strCompany_Id, string strBrand_Id, string strLocation_Id, string strTrans_Id, string strInvoice_No, string strSerial_No, string strPaymentModeId, string strPOSNo, string strSIFromTransType, string strSIFromTransNo, string strProduct_Id, string strProductDescription, string strUnit_Id, string strUnitPrice, string strUnitCost, string strQuantity, string strReturnQty, string strTaxP, string strTaxV, string strDiscountP, string strDiscountV, string strPost, string strField1, string strField2, string strField3, string strField4, string strField5, string strField6, string strField7, string strIsActive, string strModifiedBy, string strModifiedDate)
    {
        PassDataToSql[] paramList = new PassDataToSql[33];

        paramList[0] = new PassDataToSql("@Company_Id", strTrans_Id, PassDataToSql.ParaTypeList.Int, PassDataToSql.ParaDirectonList.Input);
        paramList[1] = new PassDataToSql("@Brand_Id", strCompany_Id, PassDataToSql.ParaTypeList.Int, PassDataToSql.ParaDirectonList.Input);
        paramList[2] = new PassDataToSql("@Location_Id", strLocation_Id, PassDataToSql.ParaTypeList.Int, PassDataToSql.ParaDirectonList.Input);
        paramList[3] = new PassDataToSql("@Trans_Id", strTrans_Id, PassDataToSql.ParaTypeList.Int, PassDataToSql.ParaDirectonList.Input);
        paramList[4] = new PassDataToSql("@Invoice_No", strInvoice_No, PassDataToSql.ParaTypeList.Int, PassDataToSql.ParaDirectonList.Input);
        paramList[5] = new PassDataToSql("@Serial_No", strSerial_No, PassDataToSql.ParaTypeList.Nvarchar, PassDataToSql.ParaDirectonList.Input);
        paramList[6] = new PassDataToSql("@PaymentModeId", strPaymentModeId, PassDataToSql.ParaTypeList.Int, PassDataToSql.ParaDirectonList.Input);
        paramList[7] = new PassDataToSql("@POSNo", strPOSNo, PassDataToSql.ParaTypeList.Nvarchar, PassDataToSql.ParaDirectonList.Input);
        paramList[8] = new PassDataToSql("@SIFromTransType", strSIFromTransType, PassDataToSql.ParaTypeList.Nvarchar, PassDataToSql.ParaDirectonList.Input);
        paramList[9] = new PassDataToSql("@SIFromTransNo", strSIFromTransNo, PassDataToSql.ParaTypeList.Nvarchar, PassDataToSql.ParaDirectonList.Input);
        paramList[10] = new PassDataToSql("@Product_Id", strProduct_Id, PassDataToSql.ParaTypeList.Int, PassDataToSql.ParaDirectonList.Input);
        paramList[11] = new PassDataToSql("@ProductDescription", strProductDescription, PassDataToSql.ParaTypeList.Nvarchar, PassDataToSql.ParaDirectonList.Input);
        paramList[12] = new PassDataToSql("@Unit_Id", strUnit_Id, PassDataToSql.ParaTypeList.Int, PassDataToSql.ParaDirectonList.Input);
        paramList[13] = new PassDataToSql("@UnitPrice", strUnitPrice, PassDataToSql.ParaTypeList.Float, PassDataToSql.ParaDirectonList.Input);
        paramList[14] = new PassDataToSql("@UnitCost", strUnitCost, PassDataToSql.ParaTypeList.Float, PassDataToSql.ParaDirectonList.Input);
        paramList[15] = new PassDataToSql("@Quantity", strQuantity, PassDataToSql.ParaTypeList.Float, PassDataToSql.ParaDirectonList.Input);
        paramList[16] = new PassDataToSql("@ReturnQty", strReturnQty, PassDataToSql.ParaTypeList.Float, PassDataToSql.ParaDirectonList.Input);
        paramList[17] = new PassDataToSql("@TaxP", strTaxP, PassDataToSql.ParaTypeList.Float, PassDataToSql.ParaDirectonList.Input);
        paramList[18] = new PassDataToSql("@TaxV", strTaxV, PassDataToSql.ParaTypeList.Float, PassDataToSql.ParaDirectonList.Input);
        paramList[19] = new PassDataToSql("@DiscountP", strDiscountP, PassDataToSql.ParaTypeList.Float, PassDataToSql.ParaDirectonList.Input);
        paramList[20] = new PassDataToSql("@DiscountV", strDiscountV, PassDataToSql.ParaTypeList.Float, PassDataToSql.ParaDirectonList.Input);
        paramList[21] = new PassDataToSql("@Post", strPost, PassDataToSql.ParaTypeList.Bit, PassDataToSql.ParaDirectonList.Input);
        paramList[22] = new PassDataToSql("@Field1", strField1, PassDataToSql.ParaTypeList.Nvarchar, PassDataToSql.ParaDirectonList.Input);
        paramList[23] = new PassDataToSql("@Field2", strField2, PassDataToSql.ParaTypeList.Nvarchar, PassDataToSql.ParaDirectonList.Input);
        paramList[24] = new PassDataToSql("@Field3", strField3, PassDataToSql.ParaTypeList.Nvarchar, PassDataToSql.ParaDirectonList.Input);
        paramList[25] = new PassDataToSql("@Field4", strField4, PassDataToSql.ParaTypeList.Nvarchar, PassDataToSql.ParaDirectonList.Input);
        paramList[26] = new PassDataToSql("@Field5", strField5, PassDataToSql.ParaTypeList.Nvarchar, PassDataToSql.ParaDirectonList.Input);
        paramList[27] = new PassDataToSql("@Field6", strField6, PassDataToSql.ParaTypeList.Bit, PassDataToSql.ParaDirectonList.Input);
        paramList[28] = new PassDataToSql("@Field7", strField7, PassDataToSql.ParaTypeList.Date, PassDataToSql.ParaDirectonList.Input);
        paramList[29] = new PassDataToSql("@IsActive", strIsActive, PassDataToSql.ParaTypeList.Bit, PassDataToSql.ParaDirectonList.Input);
        paramList[30] = new PassDataToSql("@ModifiedBy", strModifiedBy, PassDataToSql.ParaTypeList.Nvarchar, PassDataToSql.ParaDirectonList.Input);
        paramList[31] = new PassDataToSql("@ModifiedDate", strModifiedDate, PassDataToSql.ParaTypeList.Date, PassDataToSql.ParaDirectonList.Input);
        paramList[32] = new PassDataToSql("@ReferenceID", "0", PassDataToSql.ParaTypeList.Int, PassDataToSql.ParaDirectonList.Output);

        daClass.execute_Sp("sp_Inv_SalesInvoiceDetail_Update", paramList);
        return Convert.ToInt32(paramList[32].ParaValue);
    }
    public void DeleteSInvDetail(string strCompany_Id, string strBrand_Id, string strLocation_Id, string strInvoice_No)
    {
        PassDataToSql[] paramList = new PassDataToSql[4];
        paramList[0] = new PassDataToSql("@Company_Id", strCompany_Id, PassDataToSql.ParaTypeList.Nvarchar, PassDataToSql.ParaDirectonList.Input);
        paramList[1] = new PassDataToSql("@Brand_Id", strBrand_Id, PassDataToSql.ParaTypeList.Nvarchar, PassDataToSql.ParaDirectonList.Input);
        paramList[2] = new PassDataToSql("@Location_Id", strLocation_Id, PassDataToSql.ParaTypeList.Nvarchar, PassDataToSql.ParaDirectonList.Input);
        paramList[3] = new PassDataToSql("@Invoice_No", strInvoice_No, PassDataToSql.ParaTypeList.Nvarchar, PassDataToSql.ParaDirectonList.Input);
        daClass.execute_Sp("sp_Inv_SalesInvoiceDetail_RowStatus", paramList);
    }
    public DataTable GetSInvDetailAllTrue(string strCompany_Id, string strBrand_Id, string strLocation_Id)
    {
        DataTable dtInfo = new DataTable();
        PassDataToSql[] paramList = new PassDataToSql[7];
        paramList[0] = new PassDataToSql("@Company_Id", strCompany_Id, PassDataToSql.ParaTypeList.Nvarchar, PassDataToSql.ParaDirectonList.Input);
        paramList[1] = new PassDataToSql("@Brand_Id", strBrand_Id, PassDataToSql.ParaTypeList.Nvarchar, PassDataToSql.ParaDirectonList.Input);
        paramList[2] = new PassDataToSql("@Location_Id", strLocation_Id, PassDataToSql.ParaTypeList.Nvarchar, PassDataToSql.ParaDirectonList.Input);
        paramList[3] = new PassDataToSql("@Trans_Id", "", PassDataToSql.ParaTypeList.Nvarchar, PassDataToSql.ParaDirectonList.Input);
        paramList[4] = new PassDataToSql("@Invoice_No", "", PassDataToSql.ParaTypeList.Nvarchar, PassDataToSql.ParaDirectonList.Input);
        paramList[5] = new PassDataToSql("@Serial_No", "", PassDataToSql.ParaTypeList.Nvarchar, PassDataToSql.ParaDirectonList.Input);
        paramList[6] = new PassDataToSql("@optype", "1", PassDataToSql.ParaTypeList.Nvarchar, PassDataToSql.ParaDirectonList.Input);

        dtInfo = daClass.Reuturn_Datatable_Search("sp_Inv_SalesInvoiceDetail_SelectRow", paramList);
        return dtInfo;
    }
    public DataTable GetSInvDetailAllFalse(string strCompany_Id, string strBrand_Id, string strLocation_Id)
    {
        DataTable dtInfo = new DataTable();
        PassDataToSql[] paramList = new PassDataToSql[7];
        paramList[0] = new PassDataToSql("@Company_Id", strCompany_Id, PassDataToSql.ParaTypeList.Nvarchar, PassDataToSql.ParaDirectonList.Input);
        paramList[1] = new PassDataToSql("@Brand_Id", strBrand_Id, PassDataToSql.ParaTypeList.Nvarchar, PassDataToSql.ParaDirectonList.Input);
        paramList[2] = new PassDataToSql("@Location_Id", strLocation_Id, PassDataToSql.ParaTypeList.Nvarchar, PassDataToSql.ParaDirectonList.Input);
        paramList[3] = new PassDataToSql("@Trans_Id", "", PassDataToSql.ParaTypeList.Nvarchar, PassDataToSql.ParaDirectonList.Input);
        paramList[4] = new PassDataToSql("@Invoice_No", "", PassDataToSql.ParaTypeList.Nvarchar, PassDataToSql.ParaDirectonList.Input);
        paramList[5] = new PassDataToSql("@Serial_No", "", PassDataToSql.ParaTypeList.Nvarchar, PassDataToSql.ParaDirectonList.Input);
        paramList[6] = new PassDataToSql("@optype", "2", PassDataToSql.ParaTypeList.Nvarchar, PassDataToSql.ParaDirectonList.Input);

        dtInfo = daClass.Reuturn_Datatable_Search("sp_Inv_SalesInvoiceDetail_SelectRow", paramList);
        return dtInfo;
    }
    public DataTable GetSInvDetailByTrans_Id(string strCompany_Id, string strBrand_Id, string strLocation_Id, string strTrans_Id)
    {
        DataTable dtInfo = new DataTable();
        PassDataToSql[] paramList = new PassDataToSql[7];
        paramList[0] = new PassDataToSql("@Company_Id", strCompany_Id, PassDataToSql.ParaTypeList.Nvarchar, PassDataToSql.ParaDirectonList.Input);
        paramList[1] = new PassDataToSql("@Brand_Id", strBrand_Id, PassDataToSql.ParaTypeList.Nvarchar, PassDataToSql.ParaDirectonList.Input);
        paramList[2] = new PassDataToSql("@Location_Id", strLocation_Id, PassDataToSql.ParaTypeList.Nvarchar, PassDataToSql.ParaDirectonList.Input);
        paramList[3] = new PassDataToSql("@Trans_Id", strTrans_Id, PassDataToSql.ParaTypeList.Nvarchar, PassDataToSql.ParaDirectonList.Input);
        paramList[4] = new PassDataToSql("@Invoice_No", "", PassDataToSql.ParaTypeList.Nvarchar, PassDataToSql.ParaDirectonList.Input);
        paramList[5] = new PassDataToSql("@Serial_No", "", PassDataToSql.ParaTypeList.Nvarchar, PassDataToSql.ParaDirectonList.Input);
        paramList[6] = new PassDataToSql("@optype", "3", PassDataToSql.ParaTypeList.Nvarchar, PassDataToSql.ParaDirectonList.Input);

        dtInfo = daClass.Reuturn_Datatable_Search("sp_Inv_SalesInvoiceDetail_SelectRow", paramList);
        return dtInfo;
    }
    public DataTable GetSInvDetailByInvoiceNo(string strCompany_Id, string strBrand_Id, string strLocation_Id, string strInvoice_No)
    {
        DataTable dtInfo = new DataTable();
        PassDataToSql[] paramList = new PassDataToSql[7];
        paramList[0] = new PassDataToSql("@Company_Id", strCompany_Id, PassDataToSql.ParaTypeList.Nvarchar, PassDataToSql.ParaDirectonList.Input);
        paramList[1] = new PassDataToSql("@Brand_Id", strBrand_Id, PassDataToSql.ParaTypeList.Nvarchar, PassDataToSql.ParaDirectonList.Input);
        paramList[2] = new PassDataToSql("@Location_Id", strLocation_Id, PassDataToSql.ParaTypeList.Nvarchar, PassDataToSql.ParaDirectonList.Input);
        paramList[3] = new PassDataToSql("@Trans_Id", "", PassDataToSql.ParaTypeList.Nvarchar, PassDataToSql.ParaDirectonList.Input);
        paramList[4] = new PassDataToSql("@Invoice_No", strInvoice_No, PassDataToSql.ParaTypeList.Nvarchar, PassDataToSql.ParaDirectonList.Input);
        paramList[5] = new PassDataToSql("@Serial_No", "", PassDataToSql.ParaTypeList.Nvarchar, PassDataToSql.ParaDirectonList.Input);
        paramList[6] = new PassDataToSql("@optype", "4", PassDataToSql.ParaTypeList.Nvarchar, PassDataToSql.ParaDirectonList.Input);

        dtInfo = daClass.Reuturn_Datatable_Search("sp_Inv_SalesInvoiceDetail_SelectRow", paramList);
        return dtInfo;
    }
    public DataTable GetSInvDetailBySerial_No(string strCompany_Id, string strBrand_Id, string strLocation_Id, string strSerial_No)
    {
        DataTable dtInfo = new DataTable();
        PassDataToSql[] paramList = new PassDataToSql[7];
        paramList[0] = new PassDataToSql("@Company_Id", strCompany_Id, PassDataToSql.ParaTypeList.Nvarchar, PassDataToSql.ParaDirectonList.Input);
        paramList[1] = new PassDataToSql("@Brand_Id", strBrand_Id, PassDataToSql.ParaTypeList.Nvarchar, PassDataToSql.ParaDirectonList.Input);
        paramList[2] = new PassDataToSql("@Location_Id", strLocation_Id, PassDataToSql.ParaTypeList.Nvarchar, PassDataToSql.ParaDirectonList.Input);
        paramList[3] = new PassDataToSql("@Trans_Id", "", PassDataToSql.ParaTypeList.Nvarchar, PassDataToSql.ParaDirectonList.Input);
        paramList[4] = new PassDataToSql("@Invoice_No", "", PassDataToSql.ParaTypeList.Nvarchar, PassDataToSql.ParaDirectonList.Input);
        paramList[5] = new PassDataToSql("@Serial_No", strSerial_No, PassDataToSql.ParaTypeList.Nvarchar, PassDataToSql.ParaDirectonList.Input);
        paramList[6] = new PassDataToSql("@optype", "5", PassDataToSql.ParaTypeList.Nvarchar, PassDataToSql.ParaDirectonList.Input);

        dtInfo = daClass.Reuturn_Datatable_Search("sp_Inv_SalesInvoiceDetail_SelectRow", paramList);
        return dtInfo;
    }
}
