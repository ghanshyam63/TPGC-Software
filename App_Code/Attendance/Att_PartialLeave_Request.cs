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
/// Summary description for Att_PartialLeave_Request
/// </summary>
public class Att_PartialLeave_Request
{
    DataAccessClass daClass = new DataAccessClass();
	public Att_PartialLeave_Request()
	{
		//
		// TODO: Add constructor logic here
		//
	}


    public int InsertPartialLeaveRequest(string CompanyId, string Emp_Id, string Partial_Leave_Type, string Request_Date_Time, string Partial_Leave_Date, string From_Time, string To_Time, string Is_Confirmed,string Description ,string Approved_Date_Time, string Field1, string Field2, string Field3, string Field4, string Field5, string Field6, string Field7, string IsActive, string CreatedBy, string CreatedDate, string ModifiedBy, string ModifiedDate)
    {


        PassDataToSql[] paramList = new PassDataToSql[23];
        paramList[0] = new PassDataToSql("@Company_Id", CompanyId, PassDataToSql.ParaTypeList.Int, PassDataToSql.ParaDirectonList.Input);

        paramList[1] = new PassDataToSql("@Emp_Id", Emp_Id, PassDataToSql.ParaTypeList.Nvarchar, PassDataToSql.ParaDirectonList.Input);
        paramList[2] = new PassDataToSql("@Partial_Leave_Type", Partial_Leave_Type, PassDataToSql.ParaTypeList.Int, PassDataToSql.ParaDirectonList.Input);

        paramList[3] = new PassDataToSql("@Request_Date_Time", Request_Date_Time, PassDataToSql.ParaTypeList.Date, PassDataToSql.ParaDirectonList.Input);
        paramList[4] = new PassDataToSql("@Partial_Leave_Date", Partial_Leave_Date, PassDataToSql.ParaTypeList.Date, PassDataToSql.ParaDirectonList.Input);
        paramList[5] = new PassDataToSql("@From_Time", From_Time, PassDataToSql.ParaTypeList.Nvarchar, PassDataToSql.ParaDirectonList.Input);
        paramList[6] = new PassDataToSql("@To_Time", To_Time, PassDataToSql.ParaTypeList.Nvarchar, PassDataToSql.ParaDirectonList.Input);
        paramList[7] = new PassDataToSql("@Is_Confirmed", Is_Confirmed, PassDataToSql.ParaTypeList.Nvarchar, PassDataToSql.ParaDirectonList.Input);
        paramList[8] = new PassDataToSql("@Description", Description, PassDataToSql.ParaTypeList.Nvarchar, PassDataToSql.ParaDirectonList.Input);
        paramList[9] = new PassDataToSql("@Approved_Date_Time", Approved_Date_Time, PassDataToSql.ParaTypeList.Nvarchar, PassDataToSql.ParaDirectonList.Input);

        paramList[10] = new PassDataToSql("@Field1", Field1, PassDataToSql.ParaTypeList.Nvarchar, PassDataToSql.ParaDirectonList.Input);

        paramList[11] = new PassDataToSql("@Field2", Field2, PassDataToSql.ParaTypeList.Nvarchar, PassDataToSql.ParaDirectonList.Input);
        paramList[12] = new PassDataToSql("@Field3", Field3, PassDataToSql.ParaTypeList.Nvarchar, PassDataToSql.ParaDirectonList.Input);
        paramList[13] = new PassDataToSql("@Field4", Field4, PassDataToSql.ParaTypeList.Nvarchar, PassDataToSql.ParaDirectonList.Input);
        paramList[14] = new PassDataToSql("@Field5", Field5, PassDataToSql.ParaTypeList.Nvarchar, PassDataToSql.ParaDirectonList.Input);
        paramList[15] = new PassDataToSql("@Field6", Field6, PassDataToSql.ParaTypeList.Bit, PassDataToSql.ParaDirectonList.Input);
        paramList[16] = new PassDataToSql("@Field7", Field7, PassDataToSql.ParaTypeList.Date, PassDataToSql.ParaDirectonList.Input);


        paramList[17] = new PassDataToSql("@IsActive", IsActive, PassDataToSql.ParaTypeList.Bit, PassDataToSql.ParaDirectonList.Input);
        paramList[18] = new PassDataToSql("@CreatedBy", CreatedBy, PassDataToSql.ParaTypeList.Nvarchar, PassDataToSql.ParaDirectonList.Input);
        paramList[19] = new PassDataToSql("@CreatedDate", CreatedDate, PassDataToSql.ParaTypeList.Date, PassDataToSql.ParaDirectonList.Input);
        paramList[20] = new PassDataToSql("@ModifiedBy", ModifiedBy, PassDataToSql.ParaTypeList.Nvarchar, PassDataToSql.ParaDirectonList.Input);
        paramList[21] = new PassDataToSql("@ModifiedDate", ModifiedDate, PassDataToSql.ParaTypeList.Date, PassDataToSql.ParaDirectonList.Input);
        paramList[22] = new PassDataToSql("@ReferenceID", "0", PassDataToSql.ParaTypeList.Int, PassDataToSql.ParaDirectonList.Output);
       
        daClass.execute_Sp("sp_Att_PartialLeave_Request_Insert", paramList);
        return Convert.ToInt32(paramList[22].ParaValue);
    }
    public int UpdatePartialLeaveRequest(string Trans_Id, string CompanyId, string Emp_Id, string Partial_Leave_Type, string Request_Date_Time, string Partial_Leave_Date, string From_Time, string To_Time, string Is_Confirmed, string Description, string Approved_Date_Time, string Field1, string Field2, string Field3, string Field4, string Field5, string Field6, string Field7, string IsActive,  string ModifiedBy, string ModifiedDate)
    {


        PassDataToSql[] paramList = new PassDataToSql[15];
        paramList[0] = new PassDataToSql("@Company_Id", CompanyId, PassDataToSql.ParaTypeList.Int, PassDataToSql.ParaDirectonList.Input);

        paramList[1] = new PassDataToSql("@Emp_Id", Emp_Id, PassDataToSql.ParaTypeList.Nvarchar, PassDataToSql.ParaDirectonList.Input);
        paramList[2] = new PassDataToSql("@Partial_Leave_Type", Partial_Leave_Type, PassDataToSql.ParaTypeList.Int, PassDataToSql.ParaDirectonList.Input);

        paramList[3] = new PassDataToSql("@Request_Date_Time", Request_Date_Time, PassDataToSql.ParaTypeList.Date, PassDataToSql.ParaDirectonList.Input);
        paramList[4] = new PassDataToSql("@Partial_Leave_Date", Partial_Leave_Date, PassDataToSql.ParaTypeList.Date, PassDataToSql.ParaDirectonList.Input);
        paramList[5] = new PassDataToSql("@From_Time", From_Time, PassDataToSql.ParaTypeList.Nvarchar, PassDataToSql.ParaDirectonList.Input);
        paramList[6] = new PassDataToSql("@To_Time", To_Time, PassDataToSql.ParaTypeList.Nvarchar, PassDataToSql.ParaDirectonList.Input);
        paramList[7] = new PassDataToSql("@Is_Confirmed", Is_Confirmed, PassDataToSql.ParaTypeList.Nvarchar, PassDataToSql.ParaDirectonList.Input);
        paramList[8] = new PassDataToSql("@Description", Description, PassDataToSql.ParaTypeList.Nvarchar, PassDataToSql.ParaDirectonList.Input);
        paramList[9] = new PassDataToSql("@Approved_Date_Time", Approved_Date_Time, PassDataToSql.ParaTypeList.Nvarchar, PassDataToSql.ParaDirectonList.Input);

      

        paramList[10] = new PassDataToSql("@IsActive", IsActive, PassDataToSql.ParaTypeList.Bit, PassDataToSql.ParaDirectonList.Input);
        paramList[11] = new PassDataToSql("@ModifiedBy", ModifiedBy, PassDataToSql.ParaTypeList.Nvarchar, PassDataToSql.ParaDirectonList.Input);
        paramList[12] = new PassDataToSql("@ModifiedDate", ModifiedDate, PassDataToSql.ParaTypeList.Date, PassDataToSql.ParaDirectonList.Input);
        paramList[13] = new PassDataToSql("@ReferenceID", "0", PassDataToSql.ParaTypeList.Int, PassDataToSql.ParaDirectonList.Output);
        paramList[14] = new PassDataToSql("@Trans_Id",Trans_Id, PassDataToSql.ParaTypeList.Int, PassDataToSql.ParaDirectonList.Input);

        daClass.execute_Sp("sp_Att_PartialLeave_Request_Update", paramList);
        return Convert.ToInt32(paramList[13].ParaValue);
    }

    public int PartialLeaveApproveReject(string Trans_Id, string Is_Confirmed, string IsActive, string ModifiedBy, string ModifiedDate)
    {


        PassDataToSql[] paramList = new PassDataToSql[6];
          paramList[0] = new PassDataToSql("@Is_Confirmed", Is_Confirmed, PassDataToSql.ParaTypeList.Nvarchar, PassDataToSql.ParaDirectonList.Input);
      

        paramList[1] = new PassDataToSql("@IsActive", IsActive, PassDataToSql.ParaTypeList.Bit, PassDataToSql.ParaDirectonList.Input);
        paramList[2] = new PassDataToSql("@ModifiedBy", ModifiedBy, PassDataToSql.ParaTypeList.Nvarchar, PassDataToSql.ParaDirectonList.Input);
        paramList[3] = new PassDataToSql("@ModifiedDate", ModifiedDate, PassDataToSql.ParaTypeList.Date, PassDataToSql.ParaDirectonList.Input);
        paramList[4] = new PassDataToSql("@ReferenceID", "0", PassDataToSql.ParaTypeList.Int, PassDataToSql.ParaDirectonList.Output);
        paramList[5] = new PassDataToSql("@Trans_Id", Trans_Id, PassDataToSql.ParaTypeList.Int, PassDataToSql.ParaDirectonList.Input);

        daClass.execute_Sp("sp_Att_PartialLeave_Request_ApprovedReject", paramList);
        return Convert.ToInt32(paramList[4].ParaValue);
    }


        public DataTable GetPartialLeaveRequestByEmpIdAndCurrentMonthYear(string CompanyId, string EmpId,string month,string year)
    {

        DataTable dtInfo = new DataTable();
        PassDataToSql[] paramList = new PassDataToSql[4];
        paramList[0] = new PassDataToSql("@Company_Id", CompanyId, PassDataToSql.ParaTypeList.Int, PassDataToSql.ParaDirectonList.Input);
        paramList[1] = new PassDataToSql("@Emp_Id", EmpId, PassDataToSql.ParaTypeList.Int, PassDataToSql.ParaDirectonList.Input);
        paramList[2] = new PassDataToSql("@Month", month, PassDataToSql.ParaTypeList.Int, PassDataToSql.ParaDirectonList.Input);
        paramList[3] = new PassDataToSql("@year", year, PassDataToSql.ParaTypeList.Int, PassDataToSql.ParaDirectonList.Input);
       
        dtInfo = daClass.Reuturn_Datatable_Search("sp_Att_PartialLeave_Request_SelectOfCurrentMonth", paramList);

        return dtInfo;
    }
    



        public DataTable GetPartialLeaveRequestByTransId(string CompanyId, string TransId)
    {

        DataTable dtInfo = new DataTable();
        PassDataToSql[] paramList = new PassDataToSql[2];
        paramList[0] = new PassDataToSql("@Company_Id", CompanyId, PassDataToSql.ParaTypeList.Int, PassDataToSql.ParaDirectonList.Input);
        paramList[1] = new PassDataToSql("@Trans_Id", TransId, PassDataToSql.ParaTypeList.Int, PassDataToSql.ParaDirectonList.Input);
        dtInfo = daClass.Reuturn_Datatable_Search("sp_Att_PartialLeave_Request_SelectRowByTransId", paramList);

        return dtInfo;
    }
    public DataTable GetPartialLeaveRequestById(string CompanyId, string EmpId)
    {

        DataTable dtInfo = new DataTable();
        PassDataToSql[] paramList = new PassDataToSql[3];
        paramList[0] = new PassDataToSql("@Company_Id", CompanyId, PassDataToSql.ParaTypeList.Int, PassDataToSql.ParaDirectonList.Input);
        paramList[1] = new PassDataToSql("@Emp_Id", EmpId, PassDataToSql.ParaTypeList.Nvarchar, PassDataToSql.ParaDirectonList.Input);
        paramList[2] = new PassDataToSql("@optype", "2", PassDataToSql.ParaTypeList.Nvarchar, PassDataToSql.ParaDirectonList.Input);
        dtInfo = daClass.Reuturn_Datatable_Search("sp_Att_PartialLeave_Request_SelectRow", paramList);

        return dtInfo;
    }
    public DataTable GetPartialLeaveRequest(string CompanyId)
    {

        DataTable dtInfo = new DataTable();
        PassDataToSql[] paramList = new PassDataToSql[3];
        paramList[0] = new PassDataToSql("@Company_Id", CompanyId, PassDataToSql.ParaTypeList.Int, PassDataToSql.ParaDirectonList.Input);
        paramList[1] = new PassDataToSql("@Emp_Id", "0", PassDataToSql.ParaTypeList.Nvarchar, PassDataToSql.ParaDirectonList.Input);
        paramList[2] = new PassDataToSql("@optype", "1", PassDataToSql.ParaTypeList.Nvarchar, PassDataToSql.ParaDirectonList.Input);
        dtInfo = daClass.Reuturn_Datatable_Search("sp_Att_PartialLeave_Request_SelectRow", paramList);

        return dtInfo;
    }

    public DataTable GetPartialLeaveRequestByDate(string CompanyId,string Date)
    {

        DataTable dtInfo = new DataTable();
        PassDataToSql[] paramList = new PassDataToSql[3];
        paramList[0] = new PassDataToSql("@Company_Id", CompanyId, PassDataToSql.ParaTypeList.Int, PassDataToSql.ParaDirectonList.Input);
        paramList[1] = new PassDataToSql("@Emp_Id", "0", PassDataToSql.ParaTypeList.Nvarchar, PassDataToSql.ParaDirectonList.Input);
        paramList[2] = new PassDataToSql("@optype", "1", PassDataToSql.ParaTypeList.Nvarchar, PassDataToSql.ParaDirectonList.Input);
        dtInfo = daClass.Reuturn_Datatable_Search("sp_Att_PartialLeave_Request_SelectRow", paramList);

        dtInfo = new DataView(dtInfo, "Partial_Leave_Date='" + Date.ToString() + "' and Is_Confirmed='Approved'", "From_Time", DataViewRowState.CurrentRows).ToTable();
        return dtInfo;
    }
}
