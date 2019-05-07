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
/// Summary description for Inv_SalesQuotationDetail
/// </summary>
public class Inv_SalesQuotationDetail
{
    DataAccessClass daClass = new DataAccessClass();
	public Inv_SalesQuotationDetail()
	{
		//
		// TODO: Add constructor logic here
		//
	}
    public int InsertQuotationDetail(string strCompany_Id, string strBrand_Id, string strLocation_Id, string strSQuotation_Id, string strSerial_No, string strProduct_Id, string strProductDescription, string strCurrency_Id, string strUnitPrice, string strQuantity, string strTaxPercent, string strTaxValue, string strPriceAfterTax, string strDiscountPercent, string strDiscountValue, string strPriceAfterDiscount, string strPost, string strField1, string strField2, string strField3, string strField4, string strField5, string strField6, string strField7, string strIsActive, string strCreatedBy, string strCreatedDate, string strModifiedBy, string strModifiedDate)
    {
        PassDataToSql[] paramList = new PassDataToSql[30];

        paramList[0] = new PassDataToSql("@Company_Id", strCompany_Id, PassDataToSql.ParaTypeList.Int, PassDataToSql.ParaDirectonList.Input);
        paramList[1] = new PassDataToSql("@Brand_Id", strBrand_Id, PassDataToSql.ParaTypeList.Int, PassDataToSql.ParaDirectonList.Input);
        paramList[2] = new PassDataToSql("@Location_Id", strLocation_Id, PassDataToSql.ParaTypeList.Int, PassDataToSql.ParaDirectonList.Input);
        paramList[3] = new PassDataToSql("@SQuotation_Id", strSQuotation_Id, PassDataToSql.ParaTypeList.Int, PassDataToSql.ParaDirectonList.Input);
        paramList[4] = new PassDataToSql("@Serial_No", strSerial_No, PassDataToSql.ParaTypeList.Nvarchar, PassDataToSql.ParaDirectonList.Input);
        paramList[5] = new PassDataToSql("@Product_Id", strProduct_Id, PassDataToSql.ParaTypeList.Int, PassDataToSql.ParaDirectonList.Input);
        paramList[6] = new PassDataToSql("@ProductDescription", strProductDescription, PassDataToSql.ParaTypeList.Nvarchar, PassDataToSql.ParaDirectonList.Input);
        paramList[7] = new PassDataToSql("@Currency_Id", strCurrency_Id, PassDataToSql.ParaTypeList.Int, PassDataToSql.ParaDirectonList.Input);
        paramList[8] = new PassDataToSql("@UnitPrice", strUnitPrice, PassDataToSql.ParaTypeList.Float, PassDataToSql.ParaDirectonList.Input);
        paramList[9] = new PassDataToSql("@Quantity", strQuantity, PassDataToSql.ParaTypeList.Float, PassDataToSql.ParaDirectonList.Input);
        paramList[10] = new PassDataToSql("@TaxPercent", strTaxPercent, PassDataToSql.ParaTypeList.Float, PassDataToSql.ParaDirectonList.Input);
        paramList[11] = new PassDataToSql("@TaxValue", strTaxValue, PassDataToSql.ParaTypeList.Float, PassDataToSql.ParaDirectonList.Input);
        paramList[12] = new PassDataToSql("@PriceAfterTax", strPriceAfterTax, PassDataToSql.ParaTypeList.Float, PassDataToSql.ParaDirectonList.Input);
        paramList[13] = new PassDataToSql("@DiscountPercent", strDiscountPercent, PassDataToSql.ParaTypeList.Float, PassDataToSql.ParaDirectonList.Input);
        paramList[14] = new PassDataToSql("@DiscountValue", strDiscountValue, PassDataToSql.ParaTypeList.Float, PassDataToSql.ParaDirectonList.Input);
        paramList[15] = new PassDataToSql("@PriceAfterDiscount", strPriceAfterDiscount, PassDataToSql.ParaTypeList.Float, PassDataToSql.ParaDirectonList.Input);        
        paramList[16] = new PassDataToSql("@Post", strPost, PassDataToSql.ParaTypeList.Bit, PassDataToSql.ParaDirectonList.Input);
        paramList[17] = new PassDataToSql("@Field1", strField1, PassDataToSql.ParaTypeList.Nvarchar, PassDataToSql.ParaDirectonList.Input);
        paramList[18] = new PassDataToSql("@Field2", strField2, PassDataToSql.ParaTypeList.Nvarchar, PassDataToSql.ParaDirectonList.Input);
        paramList[19] = new PassDataToSql("@Field3", strField3, PassDataToSql.ParaTypeList.Nvarchar, PassDataToSql.ParaDirectonList.Input);
        paramList[20] = new PassDataToSql("@Field4", strField4, PassDataToSql.ParaTypeList.Nvarchar, PassDataToSql.ParaDirectonList.Input);
        paramList[21] = new PassDataToSql("@Field5", strField5, PassDataToSql.ParaTypeList.Nvarchar, PassDataToSql.ParaDirectonList.Input);
        paramList[22] = new PassDataToSql("@Field6", strField6, PassDataToSql.ParaTypeList.Bit, PassDataToSql.ParaDirectonList.Input);
        paramList[23] = new PassDataToSql("@Field7", strField7, PassDataToSql.ParaTypeList.Date, PassDataToSql.ParaDirectonList.Input);
        paramList[24] = new PassDataToSql("@IsActive", strIsActive, PassDataToSql.ParaTypeList.Bit, PassDataToSql.ParaDirectonList.Input);
        paramList[25] = new PassDataToSql("@CreatedBy", strCreatedBy, PassDataToSql.ParaTypeList.Nvarchar, PassDataToSql.ParaDirectonList.Input);
        paramList[26] = new PassDataToSql("@CreatedDate", strCreatedDate, PassDataToSql.ParaTypeList.Date, PassDataToSql.ParaDirectonList.Input);
        paramList[27] = new PassDataToSql("@ModifiedBy", strModifiedBy, PassDataToSql.ParaTypeList.Nvarchar, PassDataToSql.ParaDirectonList.Input);
        paramList[28] = new PassDataToSql("@ModifiedDate", strModifiedDate, PassDataToSql.ParaTypeList.Date, PassDataToSql.ParaDirectonList.Input);
        paramList[29] = new PassDataToSql("@ReferenceID", "0", PassDataToSql.ParaTypeList.Int, PassDataToSql.ParaDirectonList.Output);

        daClass.execute_Sp("sp_Inv_SalesQuotationDetail_Insert", paramList);
        return Convert.ToInt32(paramList[29].ParaValue);
    }
    public int UpdateQuotationDetail(string strCompany_Id, string strBrand_Id, string strLocation_Id, string strTrans_Id, string strSQuotation_Id, string strSerial_No, string strProduct_Id, string strProductDescription, string strCurrency_Id, string strUnitPrice, string strQuantity, string strTaxPercent, string strTaxValue, string strPriceAfterTax, string strDiscountPercent, string strDiscountValue, string strPriceAfterDiscount, string strPost, string strField1, string strField2, string strField3, string strField4, string strField5, string strField6, string strField7, string strIsActive, string strModifiedBy, string strModifiedDate)
    {
        PassDataToSql[] paramList = new PassDataToSql[29];

        paramList[0] = new PassDataToSql("@Company_Id", strTrans_Id, PassDataToSql.ParaTypeList.Int, PassDataToSql.ParaDirectonList.Input);
        paramList[1] = new PassDataToSql("@Brand_Id", strCompany_Id, PassDataToSql.ParaTypeList.Int, PassDataToSql.ParaDirectonList.Input);
        paramList[2] = new PassDataToSql("@Location_Id", strLocation_Id, PassDataToSql.ParaTypeList.Int, PassDataToSql.ParaDirectonList.Input);
        paramList[3] = new PassDataToSql("@Trans_Id", strTrans_Id, PassDataToSql.ParaTypeList.Int, PassDataToSql.ParaDirectonList.Input);
        paramList[4] = new PassDataToSql("@SQuotation_Id", strSQuotation_Id, PassDataToSql.ParaTypeList.Int, PassDataToSql.ParaDirectonList.Input);
        paramList[5] = new PassDataToSql("@Serial_No", strSerial_No, PassDataToSql.ParaTypeList.Nvarchar, PassDataToSql.ParaDirectonList.Input);
        paramList[6] = new PassDataToSql("@Product_Id", strProduct_Id, PassDataToSql.ParaTypeList.Int, PassDataToSql.ParaDirectonList.Input);
        paramList[7] = new PassDataToSql("@ProductDescription", strProductDescription, PassDataToSql.ParaTypeList.Nvarchar, PassDataToSql.ParaDirectonList.Input);
        paramList[8] = new PassDataToSql("@Currency_Id", strCurrency_Id, PassDataToSql.ParaTypeList.Int, PassDataToSql.ParaDirectonList.Input);
        paramList[9] = new PassDataToSql("@UnitPrice", strUnitPrice, PassDataToSql.ParaTypeList.Float, PassDataToSql.ParaDirectonList.Input);
        paramList[10] = new PassDataToSql("@Quantity", strQuantity, PassDataToSql.ParaTypeList.Float, PassDataToSql.ParaDirectonList.Input);
        paramList[11] = new PassDataToSql("@TaxPercent", strTaxPercent, PassDataToSql.ParaTypeList.Float, PassDataToSql.ParaDirectonList.Input);
        paramList[12] = new PassDataToSql("@TaxValue", strTaxValue, PassDataToSql.ParaTypeList.Float, PassDataToSql.ParaDirectonList.Input);
        paramList[13] = new PassDataToSql("@PriceAfterTax", strPriceAfterTax, PassDataToSql.ParaTypeList.Float, PassDataToSql.ParaDirectonList.Input);
        paramList[14] = new PassDataToSql("@DiscountPercent", strDiscountPercent, PassDataToSql.ParaTypeList.Float, PassDataToSql.ParaDirectonList.Input);
        paramList[15] = new PassDataToSql("@DiscountValue", strDiscountValue, PassDataToSql.ParaTypeList.Float, PassDataToSql.ParaDirectonList.Input);
        paramList[16] = new PassDataToSql("@PriceAfterDiscount", strPriceAfterDiscount, PassDataToSql.ParaTypeList.Float, PassDataToSql.ParaDirectonList.Input);        
        paramList[17] = new PassDataToSql("@Post", strPost, PassDataToSql.ParaTypeList.Bit, PassDataToSql.ParaDirectonList.Input);
        paramList[18] = new PassDataToSql("@Field1", strField1, PassDataToSql.ParaTypeList.Nvarchar, PassDataToSql.ParaDirectonList.Input);
        paramList[19] = new PassDataToSql("@Field2", strField2, PassDataToSql.ParaTypeList.Nvarchar, PassDataToSql.ParaDirectonList.Input);
        paramList[20] = new PassDataToSql("@Field3", strField3, PassDataToSql.ParaTypeList.Nvarchar, PassDataToSql.ParaDirectonList.Input);
        paramList[21] = new PassDataToSql("@Field4", strField4, PassDataToSql.ParaTypeList.Nvarchar, PassDataToSql.ParaDirectonList.Input);
        paramList[22] = new PassDataToSql("@Field5", strField5, PassDataToSql.ParaTypeList.Nvarchar, PassDataToSql.ParaDirectonList.Input);
        paramList[23] = new PassDataToSql("@Field6", strField6, PassDataToSql.ParaTypeList.Bit, PassDataToSql.ParaDirectonList.Input);
        paramList[24] = new PassDataToSql("@Field7", strField7, PassDataToSql.ParaTypeList.Date, PassDataToSql.ParaDirectonList.Input);
        paramList[25] = new PassDataToSql("@IsActive", strIsActive, PassDataToSql.ParaTypeList.Bit, PassDataToSql.ParaDirectonList.Input);
        paramList[26] = new PassDataToSql("@ModifiedBy", strModifiedBy, PassDataToSql.ParaTypeList.Nvarchar, PassDataToSql.ParaDirectonList.Input);
        paramList[27] = new PassDataToSql("@ModifiedDate", strModifiedDate, PassDataToSql.ParaTypeList.Date, PassDataToSql.ParaDirectonList.Input);
        paramList[28] = new PassDataToSql("@ReferenceID", "0", PassDataToSql.ParaTypeList.Int, PassDataToSql.ParaDirectonList.Output);

        daClass.execute_Sp("sp_Inv_SalesQuotationDetail_Update", paramList);
        return Convert.ToInt32(paramList[28].ParaValue);
    }
    public void DeleteQuotationDetail(string strCompany_Id, string strBrand_Id, string strLocation_Id, string strSQuotation_Id)
    {
        PassDataToSql[] paramList = new PassDataToSql[4];
        paramList[0] = new PassDataToSql("@Company_Id", strCompany_Id, PassDataToSql.ParaTypeList.Nvarchar, PassDataToSql.ParaDirectonList.Input);
        paramList[1] = new PassDataToSql("@Brand_Id", strBrand_Id, PassDataToSql.ParaTypeList.Nvarchar, PassDataToSql.ParaDirectonList.Input);
        paramList[2] = new PassDataToSql("@Location_Id", strLocation_Id, PassDataToSql.ParaTypeList.Nvarchar, PassDataToSql.ParaDirectonList.Input);
        paramList[3] = new PassDataToSql("@SQuotation_Id", strSQuotation_Id, PassDataToSql.ParaTypeList.Nvarchar, PassDataToSql.ParaDirectonList.Input);
        daClass.execute_Sp("sp_Inv_SalesQuotationDetail_RowStatus", paramList);
    }
    public DataTable GetQuotationDetailAllTrue(string strCompany_Id, string strBrand_Id, string strLocation_Id)
    {
        DataTable dtInfo = new DataTable();
        PassDataToSql[] paramList = new PassDataToSql[9];
        paramList[0] = new PassDataToSql("@Company_Id", strCompany_Id, PassDataToSql.ParaTypeList.Nvarchar, PassDataToSql.ParaDirectonList.Input);
        paramList[1] = new PassDataToSql("@Brand_Id", strBrand_Id, PassDataToSql.ParaTypeList.Nvarchar, PassDataToSql.ParaDirectonList.Input);
        paramList[2] = new PassDataToSql("@Location_Id", strLocation_Id, PassDataToSql.ParaTypeList.Nvarchar, PassDataToSql.ParaDirectonList.Input);
        paramList[3] = new PassDataToSql("@Trans_Id", "", PassDataToSql.ParaTypeList.Nvarchar, PassDataToSql.ParaDirectonList.Input);
        paramList[4] = new PassDataToSql("@SQuotation_Id", "", PassDataToSql.ParaTypeList.Nvarchar, PassDataToSql.ParaDirectonList.Input);
        paramList[5] = new PassDataToSql("@Product_Id", "", PassDataToSql.ParaTypeList.Nvarchar, PassDataToSql.ParaDirectonList.Input);
        paramList[6] = new PassDataToSql("@ProductDescription", "", PassDataToSql.ParaTypeList.Nvarchar, PassDataToSql.ParaDirectonList.Input);
        paramList[7] = new PassDataToSql("@Serial_No", "", PassDataToSql.ParaTypeList.Nvarchar, PassDataToSql.ParaDirectonList.Input);
        paramList[8] = new PassDataToSql("@optype", "1", PassDataToSql.ParaTypeList.Nvarchar, PassDataToSql.ParaDirectonList.Input);

        dtInfo = daClass.Reuturn_Datatable_Search("sp_Inv_SalesQuotationDetail_SelectRow", paramList);
        return dtInfo;
    }
    public DataTable GetQuotationDetailAllFalse(string strCompany_Id, string strBrand_Id, string strLocation_Id)
    {
        DataTable dtInfo = new DataTable();
        PassDataToSql[] paramList = new PassDataToSql[9];
        paramList[0] = new PassDataToSql("@Company_Id", strCompany_Id, PassDataToSql.ParaTypeList.Nvarchar, PassDataToSql.ParaDirectonList.Input);
        paramList[1] = new PassDataToSql("@Brand_Id", strBrand_Id, PassDataToSql.ParaTypeList.Nvarchar, PassDataToSql.ParaDirectonList.Input);
        paramList[2] = new PassDataToSql("@Location_Id", strLocation_Id, PassDataToSql.ParaTypeList.Nvarchar, PassDataToSql.ParaDirectonList.Input);
        paramList[3] = new PassDataToSql("@Trans_Id", "", PassDataToSql.ParaTypeList.Nvarchar, PassDataToSql.ParaDirectonList.Input);
        paramList[4] = new PassDataToSql("@SQuotation_Id", "", PassDataToSql.ParaTypeList.Nvarchar, PassDataToSql.ParaDirectonList.Input);
        paramList[5] = new PassDataToSql("@Product_Id", "", PassDataToSql.ParaTypeList.Nvarchar, PassDataToSql.ParaDirectonList.Input);
        paramList[6] = new PassDataToSql("@ProductDescription", "", PassDataToSql.ParaTypeList.Nvarchar, PassDataToSql.ParaDirectonList.Input);       
        paramList[7] = new PassDataToSql("@Serial_No", "", PassDataToSql.ParaTypeList.Nvarchar, PassDataToSql.ParaDirectonList.Input);
        paramList[8] = new PassDataToSql("@optype", "2", PassDataToSql.ParaTypeList.Nvarchar, PassDataToSql.ParaDirectonList.Input);

        dtInfo = daClass.Reuturn_Datatable_Search("sp_Inv_SalesQuotationDetail_SelectRow", paramList);
        return dtInfo;
    }
    public DataTable GetQuotationDetailByTrans_Id(string strCompany_Id, string strBrand_Id, string strLocation_Id, string strTrans_Id)
    {
        DataTable dtInfo = new DataTable();
        PassDataToSql[] paramList = new PassDataToSql[9];
        paramList[0] = new PassDataToSql("@Company_Id", strCompany_Id, PassDataToSql.ParaTypeList.Nvarchar, PassDataToSql.ParaDirectonList.Input);
        paramList[1] = new PassDataToSql("@Brand_Id", strBrand_Id, PassDataToSql.ParaTypeList.Nvarchar, PassDataToSql.ParaDirectonList.Input);
        paramList[2] = new PassDataToSql("@Location_Id", strLocation_Id, PassDataToSql.ParaTypeList.Nvarchar, PassDataToSql.ParaDirectonList.Input);
        paramList[3] = new PassDataToSql("@Trans_Id", strTrans_Id, PassDataToSql.ParaTypeList.Nvarchar, PassDataToSql.ParaDirectonList.Input);
        paramList[4] = new PassDataToSql("@SQuotation_Id", "", PassDataToSql.ParaTypeList.Nvarchar, PassDataToSql.ParaDirectonList.Input);
        paramList[5] = new PassDataToSql("@Product_Id", "", PassDataToSql.ParaTypeList.Nvarchar, PassDataToSql.ParaDirectonList.Input);
        paramList[6] = new PassDataToSql("@ProductDescription", "", PassDataToSql.ParaTypeList.Nvarchar, PassDataToSql.ParaDirectonList.Input);       
        paramList[7] = new PassDataToSql("@Serial_No", "", PassDataToSql.ParaTypeList.Nvarchar, PassDataToSql.ParaDirectonList.Input);
        paramList[8] = new PassDataToSql("@optype", "3", PassDataToSql.ParaTypeList.Nvarchar, PassDataToSql.ParaDirectonList.Input);

        dtInfo = daClass.Reuturn_Datatable_Search("sp_Inv_SalesQuotationDetail_SelectRow", paramList);
        return dtInfo;
    }
    public DataTable GetQuotationDetailBySQuotation_Id(string strCompany_Id, string strBrand_Id, string strLocation_Id, string strSQuotation_Id)
    {
        DataTable dtInfo = new DataTable();
        PassDataToSql[] paramList = new PassDataToSql[9];
        paramList[0] = new PassDataToSql("@Company_Id", strCompany_Id, PassDataToSql.ParaTypeList.Nvarchar, PassDataToSql.ParaDirectonList.Input);
        paramList[1] = new PassDataToSql("@Brand_Id", strBrand_Id, PassDataToSql.ParaTypeList.Nvarchar, PassDataToSql.ParaDirectonList.Input);
        paramList[2] = new PassDataToSql("@Location_Id", strLocation_Id, PassDataToSql.ParaTypeList.Nvarchar, PassDataToSql.ParaDirectonList.Input);
        paramList[3] = new PassDataToSql("@Trans_Id", "", PassDataToSql.ParaTypeList.Nvarchar, PassDataToSql.ParaDirectonList.Input);
        paramList[4] = new PassDataToSql("@SQuotation_Id", strSQuotation_Id, PassDataToSql.ParaTypeList.Nvarchar, PassDataToSql.ParaDirectonList.Input);
        paramList[5] = new PassDataToSql("@Product_Id", "", PassDataToSql.ParaTypeList.Nvarchar, PassDataToSql.ParaDirectonList.Input);
        paramList[6] = new PassDataToSql("@ProductDescription", "", PassDataToSql.ParaTypeList.Nvarchar, PassDataToSql.ParaDirectonList.Input);       
        paramList[7] = new PassDataToSql("@Serial_No", "", PassDataToSql.ParaTypeList.Nvarchar, PassDataToSql.ParaDirectonList.Input);
        paramList[8] = new PassDataToSql("@optype", "4", PassDataToSql.ParaTypeList.Nvarchar, PassDataToSql.ParaDirectonList.Input);

        dtInfo = daClass.Reuturn_Datatable_Search("sp_Inv_SalesQuotationDetail_SelectRow", paramList);
        return dtInfo;
    }
    public DataTable GetQuotationDetailBySerial_No(string strCompany_Id, string strBrand_Id, string strLocation_Id, string strSerialNo)
    {
        DataTable dtInfo = new DataTable();
        PassDataToSql[] paramList = new PassDataToSql[9];
        paramList[0] = new PassDataToSql("@Company_Id", strCompany_Id, PassDataToSql.ParaTypeList.Nvarchar, PassDataToSql.ParaDirectonList.Input);
        paramList[1] = new PassDataToSql("@Brand_Id", strBrand_Id, PassDataToSql.ParaTypeList.Nvarchar, PassDataToSql.ParaDirectonList.Input);
        paramList[2] = new PassDataToSql("@Location_Id", strLocation_Id, PassDataToSql.ParaTypeList.Nvarchar, PassDataToSql.ParaDirectonList.Input);
        paramList[3] = new PassDataToSql("@Trans_Id", "", PassDataToSql.ParaTypeList.Nvarchar, PassDataToSql.ParaDirectonList.Input);
        paramList[4] = new PassDataToSql("@SQuotation_Id", "", PassDataToSql.ParaTypeList.Nvarchar, PassDataToSql.ParaDirectonList.Input);
        paramList[5] = new PassDataToSql("@Product_Id", "", PassDataToSql.ParaTypeList.Nvarchar, PassDataToSql.ParaDirectonList.Input);
        paramList[6] = new PassDataToSql("@ProductDescription", "", PassDataToSql.ParaTypeList.Nvarchar, PassDataToSql.ParaDirectonList.Input);       
        paramList[7] = new PassDataToSql("@Serial_No", strSerialNo, PassDataToSql.ParaTypeList.Nvarchar, PassDataToSql.ParaDirectonList.Input);
        paramList[8] = new PassDataToSql("@optype", "5", PassDataToSql.ParaTypeList.Nvarchar, PassDataToSql.ParaDirectonList.Input);

        dtInfo = daClass.Reuturn_Datatable_Search("sp_Inv_SalesQuotationDetail_SelectRow", paramList);
        return dtInfo;
    }
    public DataTable GetQuotationDetailBySQuotation_IdAndProductId(string strCompany_Id, string strBrand_Id, string strLocation_Id, string strSQuotation_Id, string strProduct_Id)
    {
        DataTable dtInfo = new DataTable();
        PassDataToSql[] paramList = new PassDataToSql[9];
        paramList[0] = new PassDataToSql("@Company_Id", strCompany_Id, PassDataToSql.ParaTypeList.Nvarchar, PassDataToSql.ParaDirectonList.Input);
        paramList[1] = new PassDataToSql("@Brand_Id", strBrand_Id, PassDataToSql.ParaTypeList.Nvarchar, PassDataToSql.ParaDirectonList.Input);
        paramList[2] = new PassDataToSql("@Location_Id", strLocation_Id, PassDataToSql.ParaTypeList.Nvarchar, PassDataToSql.ParaDirectonList.Input);
        paramList[3] = new PassDataToSql("@Trans_Id", "", PassDataToSql.ParaTypeList.Nvarchar, PassDataToSql.ParaDirectonList.Input);
        paramList[4] = new PassDataToSql("@SQuotation_Id", strSQuotation_Id, PassDataToSql.ParaTypeList.Nvarchar, PassDataToSql.ParaDirectonList.Input);
        paramList[5] = new PassDataToSql("@Product_Id", strProduct_Id, PassDataToSql.ParaTypeList.Nvarchar, PassDataToSql.ParaDirectonList.Input);
        paramList[6] = new PassDataToSql("@ProductDescription", "", PassDataToSql.ParaTypeList.Nvarchar, PassDataToSql.ParaDirectonList.Input);
        paramList[7] = new PassDataToSql("@Serial_No", "", PassDataToSql.ParaTypeList.Nvarchar, PassDataToSql.ParaDirectonList.Input);
        paramList[8] = new PassDataToSql("@optype", "6", PassDataToSql.ParaTypeList.Nvarchar, PassDataToSql.ParaDirectonList.Input);

        dtInfo = daClass.Reuturn_Datatable_Search("sp_Inv_SalesQuotationDetail_SelectRow", paramList);
        return dtInfo;
    }
    public DataTable GetQuotationDetailBySQuotation_IdAndProductDescription(string strCompany_Id, string strBrand_Id, string strLocation_Id, string strSQuotation_Id, string strProduct_Id, string strProductDescription)
    {
        DataTable dtInfo = new DataTable();
        PassDataToSql[] paramList = new PassDataToSql[9];
        paramList[0] = new PassDataToSql("@Company_Id", strCompany_Id, PassDataToSql.ParaTypeList.Nvarchar, PassDataToSql.ParaDirectonList.Input);
        paramList[1] = new PassDataToSql("@Brand_Id", strBrand_Id, PassDataToSql.ParaTypeList.Nvarchar, PassDataToSql.ParaDirectonList.Input);
        paramList[2] = new PassDataToSql("@Location_Id", strLocation_Id, PassDataToSql.ParaTypeList.Nvarchar, PassDataToSql.ParaDirectonList.Input);
        paramList[3] = new PassDataToSql("@Trans_Id", "", PassDataToSql.ParaTypeList.Nvarchar, PassDataToSql.ParaDirectonList.Input);
        paramList[4] = new PassDataToSql("@SQuotation_Id", strSQuotation_Id, PassDataToSql.ParaTypeList.Nvarchar, PassDataToSql.ParaDirectonList.Input);
        paramList[5] = new PassDataToSql("@Product_Id", strProduct_Id, PassDataToSql.ParaTypeList.Nvarchar, PassDataToSql.ParaDirectonList.Input);
        paramList[6] = new PassDataToSql("@ProductDescription", strProductDescription, PassDataToSql.ParaTypeList.Nvarchar, PassDataToSql.ParaDirectonList.Input);
        paramList[7] = new PassDataToSql("@Serial_No", "", PassDataToSql.ParaTypeList.Nvarchar, PassDataToSql.ParaDirectonList.Input);
        paramList[8] = new PassDataToSql("@optype", "7", PassDataToSql.ParaTypeList.Nvarchar, PassDataToSql.ParaDirectonList.Input);

        dtInfo = daClass.Reuturn_Datatable_Search("sp_Inv_SalesQuotationDetail_SelectRow", paramList);
        return dtInfo;
    }
}
