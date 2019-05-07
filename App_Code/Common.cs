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
using System.Data.SqlClient;

/// <summary>
/// Summary description for Common
/// </summary>
public class Common
{
    DataAccessClass daClass = new DataAccessClass();
    public Common()
    {
        //
        // TODO: Add constructor logic here
        //

    }

   

    public static string ChangeTDForDefaultLeft()
    {
        string retval = string.Empty;
        try
        {
            string lang = HttpContext.Current.Session["lang"].ToString();
        }
        catch (Exception)
        {

            return "left";
        }

        if (HttpContext.Current.Session["lang"] != null && HttpContext.Current.Session["lang"].ToString() == "2")
        {
            retval = "right";
        }
        else
        {
            retval = "left";
        }
        return retval;
    }

    public static string ChangeTDForDefaultRight()
    {
        string retval = string.Empty;
        if (HttpContext.Current.Session["lang"] != null && HttpContext.Current.Session["lang"] == "2")
        {
            retval = "left";
        }
        else
        {
            retval = "right";
        }
        return retval;
    }


    public DataTable GetModuleName()
    {

        DataTable dtInfo = new DataTable();
        PassDataToSql[] paramList = new PassDataToSql[1];
        paramList[0] = new PassDataToSql("@optype", "2", PassDataToSql.ParaTypeList.Nvarchar, PassDataToSql.ParaDirectonList.Input);
        dtInfo = daClass.Reuturn_Datatable_Search("sp_Application_Menu", paramList);

        return dtInfo;
    }

    public DataTable GetObjectName()
    {

        DataTable dtInfo = new DataTable();
        PassDataToSql[] paramList = new PassDataToSql[1];
        paramList[0] = new PassDataToSql("@optype", "1", PassDataToSql.ParaTypeList.Nvarchar, PassDataToSql.ParaDirectonList.Input);
        dtInfo = daClass.Reuturn_Datatable_Search("sp_Application_Menu", paramList);

        return dtInfo;
    }

    public DataTable GetAccodion(string UserId)
    {
        DataTable dtInfo = new DataTable();
        PassDataToSql[] paramList = new PassDataToSql[1];
        paramList[0] = new PassDataToSql("@UserId", UserId.ToString(), PassDataToSql.ParaTypeList.Nvarchar, PassDataToSql.ParaDirectonList.Input);
        dtInfo = daClass.Reuturn_Datatable_Search("Set_Accodion", paramList);

        return dtInfo;
    }
       
    public DataTable GetAllPagePermission(string UserId,string ModuleId,string ObjectId)
    {
        DataTable dtInfo = new DataTable();
        PassDataToSql[] paramList = new PassDataToSql[3];
        paramList[0] = new PassDataToSql("@UserId", UserId.ToString(), PassDataToSql.ParaTypeList.Nvarchar, PassDataToSql.ParaDirectonList.Input);
        paramList[1] = new PassDataToSql("@ModuleId", ModuleId.ToString(), PassDataToSql.ParaTypeList.Int, PassDataToSql.ParaDirectonList.Input);
        paramList[2] = new PassDataToSql("@ObjectId", ObjectId.ToString(), PassDataToSql.ParaTypeList.Int, PassDataToSql.ParaDirectonList.Input);
        dtInfo = daClass.Reuturn_Datatable_Search("Set_AllPageCode", paramList);

        return dtInfo;
    }
    public static DataTable GetEmployee(string prefixText, string comp)
    {
        DataTable dt = new DataTable();
        SqlConnection con = new SqlConnection();
        SqlDataAdapter da = new SqlDataAdapter();
        con.ConnectionString = ConfigurationManager.ConnectionStrings["PegaConnection"].ConnectionString;
        con.Open();
        SqlCommand cmd = new SqlCommand();
        cmd.Connection = con;
        cmd.CommandText = "Set_EmployeeMaster_SelectEmployeeName";
        cmd.Parameters.AddWithValue("@CompanyId", comp);
        cmd.Parameters.AddWithValue("@EmpName", prefixText);
        cmd.CommandType = CommandType.StoredProcedure;
        da.SelectCommand = cmd;
        da.Fill(dt);
        dt = new DataView(dt, "Brand_Id='" + HttpContext.Current.Session["BrandId"].ToString() + "' and Location_Id='" + HttpContext.Current.Session["LocId"].ToString() + "'", "", DataViewRowState.CurrentRows).ToTable();
      

        return dt;


    }
    public string GetEmpName(string Emp_Id)
    {
        string retval = "";
        DataAccessClass daClass = new DataAccessClass();
        PassDataToSql[] ps = new PassDataToSql[2];
        ps[0] = new PassDataToSql("@CompanyId", HttpContext.Current.Session["CompId"].ToString(), PassDataToSql.ParaTypeList.Nvarchar, PassDataToSql.ParaDirectonList.Input);
        ps[1] = new PassDataToSql("@Emp_Id", Emp_Id, PassDataToSql.ParaTypeList.Nvarchar, PassDataToSql.ParaDirectonList.Input);



        DataTable dt = daClass.Reuturn_Datatable_Search("Set_EmployeeMaster_SelectEmployeeNameByEmpCode", ps);
        if (dt != null && dt.Rows.Count > 0)
            retval = "" + dt.Rows[0]["Emp_Name"].ToString() + "/(" + dt.Rows[0]["Designation"].ToString() + ")/" + dt.Rows[0][0].ToString() + "";
        return retval;
    }
}
   