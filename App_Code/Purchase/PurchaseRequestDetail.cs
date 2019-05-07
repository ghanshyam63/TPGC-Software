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
/// Summary description for PurchaseRequestDetail
/// </summary>
public class PurchaseRequestDetail
{
    DataAccessClass daClass = new DataAccessClass();
	public PurchaseRequestDetail()
	{
		//
		// TODO: Add constructor logic here
		//
	}

    public int InsertPurchaseRequestDetail(string CompanyId, string brandId, string Locationid, string RequestNo, string SerialNO, string ProductId,string ProductDescription,string UnitId,string ReqQty, string Field1, string Field2, string Field3, string Field4, string Field5, string Field6, string Field7, string IsActive, string CreatedBy, string CreatedDate, string ModifiedBy, string ModifiedDate)
    {

        PassDataToSql[] paramList = new PassDataToSql[22];
        paramList[0] = new PassDataToSql("@Company_Id", CompanyId, PassDataToSql.ParaTypeList.Int, PassDataToSql.ParaDirectonList.Input);
        paramList[1] = new PassDataToSql("@Brand_Id", brandId, PassDataToSql.ParaTypeList.Int, PassDataToSql.ParaDirectonList.Input);
        paramList[2] = new PassDataToSql("@Location_Id", Locationid, PassDataToSql.ParaTypeList.Int, PassDataToSql.ParaDirectonList.Input);

        paramList[3] = new PassDataToSql("@RequestNo", RequestNo, PassDataToSql.ParaTypeList.Nvarchar, PassDataToSql.ParaDirectonList.Input);
        paramList[4] = new PassDataToSql("@Serial_No", SerialNO, PassDataToSql.ParaTypeList.Nvarchar, PassDataToSql.ParaDirectonList.Input);
        paramList[5] = new PassDataToSql("@Product_Id", ProductId, PassDataToSql.ParaTypeList.Int, PassDataToSql.ParaDirectonList.Input);
        paramList[6] = new PassDataToSql("@ProductDescription", ProductDescription, PassDataToSql.ParaTypeList.Nvarchar, PassDataToSql.ParaDirectonList.Input);
              
        
        paramList[7] = new PassDataToSql("@UnitId", UnitId, PassDataToSql.ParaTypeList.Int, PassDataToSql.ParaDirectonList.Input);
        paramList[8] = new PassDataToSql("@ReqQty", ReqQty, PassDataToSql.ParaTypeList.Int, PassDataToSql.ParaDirectonList.Input);
        
       
        paramList[9] = new PassDataToSql("@Field1", Field1, PassDataToSql.ParaTypeList.Nvarchar, PassDataToSql.ParaDirectonList.Input);
        paramList[10] = new PassDataToSql("@Field2", Field2, PassDataToSql.ParaTypeList.Nvarchar, PassDataToSql.ParaDirectonList.Input);
        paramList[11] = new PassDataToSql("@Field3", Field3, PassDataToSql.ParaTypeList.Nvarchar, PassDataToSql.ParaDirectonList.Input);
        paramList[12] = new PassDataToSql("@Field4", Field4, PassDataToSql.ParaTypeList.Nvarchar, PassDataToSql.ParaDirectonList.Input);
        paramList[13] = new PassDataToSql("@Field5", Field5, PassDataToSql.ParaTypeList.Nvarchar, PassDataToSql.ParaDirectonList.Input);
        paramList[14] = new PassDataToSql("@Field6", Field6, PassDataToSql.ParaTypeList.Bit, PassDataToSql.ParaDirectonList.Input);
        paramList[15] = new PassDataToSql("@Field7", Field7, PassDataToSql.ParaTypeList.Date, PassDataToSql.ParaDirectonList.Input);

        paramList[16] = new PassDataToSql("@IsActive", IsActive, PassDataToSql.ParaTypeList.Bit, PassDataToSql.ParaDirectonList.Input);
        paramList[17] = new PassDataToSql("@CreatedBy", CreatedBy, PassDataToSql.ParaTypeList.Nvarchar, PassDataToSql.ParaDirectonList.Input);
        paramList[18] = new PassDataToSql("@CreatedDate", CreatedDate, PassDataToSql.ParaTypeList.DateTime, PassDataToSql.ParaDirectonList.Input);
        paramList[19] = new PassDataToSql("@ModifiedBy", ModifiedBy, PassDataToSql.ParaTypeList.Nvarchar, PassDataToSql.ParaDirectonList.Input);
        paramList[20] = new PassDataToSql("@ModifiedDate", ModifiedDate, PassDataToSql.ParaTypeList.DateTime, PassDataToSql.ParaDirectonList.Input);
        paramList[21] = new PassDataToSql("@ReferanceID", "0", PassDataToSql.ParaTypeList.Int, PassDataToSql.ParaDirectonList.Output);

        daClass.execute_Sp("sp_Inv_PurchaseRequestDetail_Insert", paramList);
        return Convert.ToInt32(paramList[21].ParaValue);
    }

    public void UpdatePurchaseRequestDetail(string CompanyId, string brandId, string Locationid, string RequestNo, string TransId,string SerialNO, string ProductId,string ProductDescription,string UnitId,string ReqQty, string Field1, string Field2, string Field3, string Field4, string Field5, string Field6, string Field7, string IsActive,  string ModifiedBy, string ModifiedDate)
    {

        PassDataToSql[] paramList = new PassDataToSql[20];
        paramList[0] = new PassDataToSql("@Company_Id", CompanyId, PassDataToSql.ParaTypeList.Int, PassDataToSql.ParaDirectonList.Input);
        paramList[1] = new PassDataToSql("@Brand_Id", brandId, PassDataToSql.ParaTypeList.Int, PassDataToSql.ParaDirectonList.Input);
        paramList[2] = new PassDataToSql("@Location_Id", Locationid, PassDataToSql.ParaTypeList.Int, PassDataToSql.ParaDirectonList.Input);

        paramList[3] = new PassDataToSql("@RequestNo", RequestNo, PassDataToSql.ParaTypeList.Nvarchar, PassDataToSql.ParaDirectonList.Input);
        paramList[4] = new PassDataToSql("@Serial_No", SerialNO, PassDataToSql.ParaTypeList.Nvarchar, PassDataToSql.ParaDirectonList.Input);
        paramList[5] = new PassDataToSql("@Product_Id", ProductId, PassDataToSql.ParaTypeList.Int, PassDataToSql.ParaDirectonList.Input);
        paramList[6] = new PassDataToSql("@ProductDescription", ProductDescription, PassDataToSql.ParaTypeList.Nvarchar, PassDataToSql.ParaDirectonList.Input);
              
        
        paramList[7] = new PassDataToSql("@UnitId", UnitId, PassDataToSql.ParaTypeList.Int, PassDataToSql.ParaDirectonList.Input);
        paramList[8] = new PassDataToSql("@ReqQty", ReqQty, PassDataToSql.ParaTypeList.Int, PassDataToSql.ParaDirectonList.Input);
        
       
        paramList[9] = new PassDataToSql("@Field1", Field1, PassDataToSql.ParaTypeList.Nvarchar, PassDataToSql.ParaDirectonList.Input);
        paramList[10] = new PassDataToSql("@Field2", Field2, PassDataToSql.ParaTypeList.Nvarchar, PassDataToSql.ParaDirectonList.Input);
        paramList[11] = new PassDataToSql("@Field3", Field3, PassDataToSql.ParaTypeList.Nvarchar, PassDataToSql.ParaDirectonList.Input);
        paramList[12] = new PassDataToSql("@Field4", Field4, PassDataToSql.ParaTypeList.Nvarchar, PassDataToSql.ParaDirectonList.Input);
        paramList[13] = new PassDataToSql("@Field5", Field5, PassDataToSql.ParaTypeList.Nvarchar, PassDataToSql.ParaDirectonList.Input);
        paramList[14] = new PassDataToSql("@Field6", Field6, PassDataToSql.ParaTypeList.Bit, PassDataToSql.ParaDirectonList.Input);
        paramList[15] = new PassDataToSql("@Field7", Field7, PassDataToSql.ParaTypeList.Date, PassDataToSql.ParaDirectonList.Input);

        paramList[16] = new PassDataToSql("@IsActive", IsActive, PassDataToSql.ParaTypeList.Bit, PassDataToSql.ParaDirectonList.Input);
        paramList[17] = new PassDataToSql("@ModifiedBy", ModifiedBy, PassDataToSql.ParaTypeList.Nvarchar, PassDataToSql.ParaDirectonList.Input);
        paramList[18] = new PassDataToSql("@ModifiedDate", ModifiedDate, PassDataToSql.ParaTypeList.DateTime, PassDataToSql.ParaDirectonList.Input);

        paramList[19] = new PassDataToSql("@Trans_Id", TransId, PassDataToSql.ParaTypeList.Int, PassDataToSql.ParaDirectonList.Input);

        daClass.execute_Sp("sp_Inv_PurchaseRequestDetail_Update", paramList);
        
    }

    public void DeletePurchaseRequestDetail(string CompanyId, string brand_ID, string Location_Id, string Trans_Id, string IsActive, string ModifiedBy, string ModifiedDate)
    {

        PassDataToSql[] paramList = new PassDataToSql[4];
        paramList[0] = new PassDataToSql("@Company_Id", CompanyId, PassDataToSql.ParaTypeList.Int, PassDataToSql.ParaDirectonList.Input);
        paramList[1] = new PassDataToSql("@Brand_ID", brand_ID, PassDataToSql.ParaTypeList.Int, PassDataToSql.ParaDirectonList.Input);
        paramList[2] = new PassDataToSql("@Location_Id", Location_Id, PassDataToSql.ParaTypeList.Int, PassDataToSql.ParaDirectonList.Input);
        paramList[3] = new PassDataToSql("@Trans_ID", Trans_Id, PassDataToSql.ParaTypeList.Int, PassDataToSql.ParaDirectonList.Input);
       
        daClass.execute_Sp("sp_Inv_PurchaseRequestDetail_DeleteRow", paramList);
      
    }
    public void DeletePurchaseRequestDetailBYReqID(string CompanyId, string brand_ID, string Location_Id, string ReqId)
    {

        PassDataToSql[] paramList = new PassDataToSql[4];
        paramList[0] = new PassDataToSql("@Company_Id", CompanyId, PassDataToSql.ParaTypeList.Int, PassDataToSql.ParaDirectonList.Input);
        paramList[1] = new PassDataToSql("@Brand_ID", brand_ID, PassDataToSql.ParaTypeList.Int, PassDataToSql.ParaDirectonList.Input);
        paramList[2] = new PassDataToSql("@Location_Id", Location_Id, PassDataToSql.ParaTypeList.Int, PassDataToSql.ParaDirectonList.Input);
        paramList[3] = new PassDataToSql("@ReqId", ReqId, PassDataToSql.ParaTypeList.Int, PassDataToSql.ParaDirectonList.Input);

        daClass.execute_Sp("sp_Inv_PurchaseRequestDetail_DeleteRowByReqId", paramList);

    }


     
    public DataTable GetPurchaseRequestDetail(string CompanyId, string BrandId, string LocationId,string RequestId ,string TransId)
    {

        DataTable dtInfo = new DataTable();
        PassDataToSql[] paramList = new PassDataToSql[6];
        paramList[0] = new PassDataToSql("@Company_Id", CompanyId, PassDataToSql.ParaTypeList.Int, PassDataToSql.ParaDirectonList.Input);
        paramList[1] = new PassDataToSql("@Brand_Id", BrandId, PassDataToSql.ParaTypeList.Int, PassDataToSql.ParaDirectonList.Input);
        paramList[2] = new PassDataToSql("@Location_Id", LocationId, PassDataToSql.ParaTypeList.Int, PassDataToSql.ParaDirectonList.Input);
        paramList[3] = new PassDataToSql("@Trans_Id", TransId, PassDataToSql.ParaTypeList.Int, PassDataToSql.ParaDirectonList.Input);
        paramList[4] = new PassDataToSql("@RequestNo", RequestId, PassDataToSql.ParaTypeList.Int, PassDataToSql.ParaDirectonList.Input);

        paramList[5] = new PassDataToSql("@optype", "1", PassDataToSql.ParaTypeList.Nvarchar, PassDataToSql.ParaDirectonList.Input);
         dtInfo = daClass.Reuturn_Datatable_Search("sp_Inv_PurchaseRequestDetail_SelectRow", paramList);

        return dtInfo;
    }

    public DataTable GetPurchaseRequestDetailbyRequestId(string CompanyId, string BrandId, string LocationId, string RequestId)
    {

        DataTable dtInfo = new DataTable();
        PassDataToSql[] paramList = new PassDataToSql[6];
        paramList[0] = new PassDataToSql("@Company_Id", CompanyId, PassDataToSql.ParaTypeList.Int, PassDataToSql.ParaDirectonList.Input);
        paramList[1] = new PassDataToSql("@Brand_Id", BrandId, PassDataToSql.ParaTypeList.Int, PassDataToSql.ParaDirectonList.Input);
        paramList[2] = new PassDataToSql("@Location_Id", LocationId, PassDataToSql.ParaTypeList.Int, PassDataToSql.ParaDirectonList.Input);
        paramList[3] = new PassDataToSql("@Trans_Id", "0", PassDataToSql.ParaTypeList.Int, PassDataToSql.ParaDirectonList.Input);
        paramList[4] = new PassDataToSql("@RequestNo", RequestId, PassDataToSql.ParaTypeList.Int, PassDataToSql.ParaDirectonList.Input);

        paramList[5] = new PassDataToSql("@optype", "2", PassDataToSql.ParaTypeList.Nvarchar, PassDataToSql.ParaDirectonList.Input);
        dtInfo = daClass.Reuturn_Datatable_Search("sp_Inv_PurchaseRequestDetail_SelectRow", paramList);

        return dtInfo;
    }


}
