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
using System.Resources;
using System.IO;
using System.Collections;
/// <summary>
/// Summary description for SystemParameter
/// </summary>
public class SystemParameter
{
    DataAccessClass daClass = new DataAccessClass();
	public SystemParameter()
	{
		//
		// TODO: Add constructor logic here
		//
	}
    
    public int UpdateSysParameterMaster(string TransId, string Param_Name, string Param_Value, string IsActive, string ModifiedBy, string ModifiedDate)
    {

        PassDataToSql[] paramList = new PassDataToSql[7];


        paramList[0] = new PassDataToSql("@Param_Name", Param_Name, PassDataToSql.ParaTypeList.Nvarchar, PassDataToSql.ParaDirectonList.Input);
        paramList[1] = new PassDataToSql("@Param_Value", Param_Value, PassDataToSql.ParaTypeList.Nvarchar, PassDataToSql.ParaDirectonList.Input);
        
        paramList[2] = new PassDataToSql("@IsActive", IsActive, PassDataToSql.ParaTypeList.Bit, PassDataToSql.ParaDirectonList.Input);
        paramList[3] = new PassDataToSql("@TransId", TransId, PassDataToSql.ParaTypeList.Nvarchar, PassDataToSql.ParaDirectonList.Input);

        paramList[4] = new PassDataToSql("@ModifiedBy", ModifiedBy, PassDataToSql.ParaTypeList.Nvarchar, PassDataToSql.ParaDirectonList.Input);
        paramList[5] = new PassDataToSql("@ModifiedDate", ModifiedDate, PassDataToSql.ParaTypeList.Date, PassDataToSql.ParaDirectonList.Input);
        paramList[6] = new PassDataToSql("@ReferenceID", "0", PassDataToSql.ParaTypeList.Int, PassDataToSql.ParaDirectonList.Output);

        daClass.execute_Sp("sp_Sys_Parameter_Update", paramList);
        return Convert.ToInt32(paramList[6].ParaValue);
    }

    public DataTable GetSysParameterByParamName(string ParamName)
    {
        DataTable dtInfo = new DataTable();
        PassDataToSql[] paramList = new PassDataToSql[3];
        paramList[0] = new PassDataToSql("@TransId", "0", PassDataToSql.ParaTypeList.Int, PassDataToSql.ParaDirectonList.Input);
        paramList[1] = new PassDataToSql("@Param_Name", ParamName, PassDataToSql.ParaTypeList.Nvarchar, PassDataToSql.ParaDirectonList.Input);
        paramList[2] = new PassDataToSql("@optype", "1", PassDataToSql.ParaTypeList.Nvarchar, PassDataToSql.ParaDirectonList.Input);
        dtInfo = daClass.Reuturn_Datatable_Search("sp_Sys_Parameter_SelectRow", paramList);

        return dtInfo;
    }
    public string GetSysTitle()
    {
        DataTable dtInfo = new DataTable();
        PassDataToSql[] paramList = new PassDataToSql[3];
        paramList[0] = new PassDataToSql("@TransId", "0", PassDataToSql.ParaTypeList.Int, PassDataToSql.ParaDirectonList.Input);
        paramList[1] = new PassDataToSql("@Param_Name","Application_Title", PassDataToSql.ParaTypeList.Nvarchar, PassDataToSql.ParaDirectonList.Input);
        paramList[2] = new PassDataToSql("@optype", "1", PassDataToSql.ParaTypeList.Nvarchar, PassDataToSql.ParaDirectonList.Input);
        dtInfo = daClass.Reuturn_Datatable_Search("sp_Sys_Parameter_SelectRow", paramList);

        return dtInfo.Rows[0]["Param_Value"].ToString();
    }

    

    public void SetPageSize()
    {
        int size = 10;
       
        DataTable dtParam = new DataTable();

        PassDataToSql[] paramList = new PassDataToSql[3];
        paramList[0] = new PassDataToSql("@TransId", "0", PassDataToSql.ParaTypeList.Int, PassDataToSql.ParaDirectonList.Input);
        paramList[1] = new PassDataToSql("@Param_Name", "Grid_Size", PassDataToSql.ParaTypeList.Nvarchar, PassDataToSql.ParaDirectonList.Input);
        paramList[2] = new PassDataToSql("@optype", "1", PassDataToSql.ParaTypeList.Nvarchar, PassDataToSql.ParaDirectonList.Input);
        dtParam = daClass.Reuturn_Datatable_Search("sp_Sys_Parameter_SelectRow", paramList);

        try
        {
            if (dtParam.Rows.Count > 0)
            {
                size = Convert.ToInt32(dtParam.Rows[0]["Param_Value"]);
            }
        }
        catch (Exception)
        {


        }

        HttpContext.Current.Session["GridSize"] = size.ToString();



        
      

    }


    public DateTime getDateForInput(string givendate)
    {
        DateTime dtdate = new DateTime();
        if (givendate.ToString().Trim() != "")
        {

            string d = "";
            string m = "";
            string y = "";

            string DateFormat = string.Empty;
            DateFormat = SetDateFormat();


            if (DateFormat.Trim() == "dd-MM-yyyy")
            {
                d = givendate.Split('-')[0];
                m = givendate.Split('-')[1];
                y = givendate.Split('-')[2].Split(' ')[0];
                dtdate = new DateTime(Convert.ToInt32(y), Convert.ToInt32(m), Convert.ToInt32(d));

            }
            else if (DateFormat.Trim() == "dd-MMM-yyyy")
            {
                d = givendate.Split('-')[0];
                m = givendate.Split('-')[1];
                y = givendate.Split('-')[2];
                dtdate = Convert.ToDateTime(givendate);
            }

            else if (DateFormat.Trim() == "dd/MMM/yyyy")
            {
                d = givendate.Split('/')[0];
                m = givendate.Split('/')[1];
                y = givendate.Split('/')[2];
                dtdate = Convert.ToDateTime(givendate);
            }

            else if (DateFormat.Trim() == "dd/MM/yyyy")
            {
                d = givendate.Split('-')[0];
                m = givendate.Split('-')[1];
                y = givendate.Split('-')[2];
                dtdate = new DateTime(Convert.ToInt32(y), Convert.ToInt32(m), Convert.ToInt32(d));
            }

            else if (DateFormat.Trim() == "MM/dd/yyyy")
            {
                d = givendate.Split('/')[1];
                m = givendate.Split('/')[0];
                y = givendate.Split('/')[2];
                dtdate = new DateTime(Convert.ToInt32(y), Convert.ToInt32(m), Convert.ToInt32(d));
            }

            else if (DateFormat.Trim() == "MM/dd/yyyy")
            {
                d = givendate.Split('/')[1];
                m = givendate.Split('/')[0];
                y = givendate.Split('/')[2];
                dtdate = new DateTime(Convert.ToInt32(y), Convert.ToInt32(m), Convert.ToInt32(d));
            }

            else if (DateFormat.Trim() == "MM-dd-yyyy")
            {
                d = givendate.Split('-')[1];
                m = givendate.Split('-')[0];
                y = givendate.Split('-')[2];
                dtdate = new DateTime(Convert.ToInt32(y), Convert.ToInt32(m), Convert.ToInt32(d));
            }

            else if (DateFormat.Trim() == "MM-dd-yy")
            {
                d = givendate.Split('-')[1];
                m = givendate.Split('-')[0];
                y = givendate.Split('-')[2];
                dtdate = new DateTime(Convert.ToInt32(y), Convert.ToInt32(m), Convert.ToInt32(d));
            }

            else if (DateFormat.Trim() == "MM/dd/yy")
            {
                d = givendate.Split('-')[1];
                m = givendate.Split('-')[0];
                y = givendate.Split('-')[2];
                dtdate = new DateTime(Convert.ToInt32(y), Convert.ToInt32(m), Convert.ToInt32(d));
            }
        }
        return dtdate;
    }


    public string SetDateFormat()
    {
        string DateFormat = "dd-MMM-yyyy";

        DataTable dtDate = GetSysParameterByParamName("Date_Format");
        if (dtDate.Rows.Count > 0)
        {
            DateFormat = dtDate.Rows[0]["Param_Value"].ToString();

        }

        return DateFormat;

       
    }
    public DataTable GetTable()
    {
        DataTable dtTemp = new DataTable();
        dtTemp.Columns.Add(new DataColumn("Key"));
        dtTemp.Columns.Add(new DataColumn("Value"));
        return dtTemp;
    }
    public DataTable GetArabicMessage()
    {

        System.Collections.SortedList slist = new SortedList();
        DataTable dtForArebicMess = GetTable();
        string filename = HttpContext.Current.Request.PhysicalApplicationPath + "App_GlobalResources\\" + "ArabicMessage.resx";
        Stream stream = new FileStream(filename, FileMode.Open, FileAccess.Read, FileShare.Read);
        ResXResourceReader RrX = new ResXResourceReader(stream);
        IDictionaryEnumerator RrEn = RrX.GetEnumerator();

        while (RrEn.MoveNext())
        {
            slist.Add(RrEn.Key, RrEn.Value);
            DataRow row = dtForArebicMess.NewRow();
            row[0] = RrEn.Key;
            row[1] = RrEn.Value;
            dtForArebicMess.Rows.Add(row);

        }
        RrX.Close();
        stream.Dispose();
        return dtForArebicMess;

    }
    public static int GetPageSize()
    {
        int size = 10;
        try
        {
            size = Convert.ToInt32(HttpContext.Current.Session["GridSize"]);
        }
        catch (Exception)
        {


        }

        return size;

    }
}
