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
/// Summary description for EmployeeInformation
/// </summary>
public class EmployeeInformation
{

    DataAccessClass daClass = new DataAccessClass();
	public EmployeeInformation()
	{
		//
		// TODO: Add constructor logic here
		//
	}

    public int UpdateAccessControlInfo(string enrollno, string EmpCardNo, string pwd, string priv, string fingertemp, string FaceData, string FaceIndex, string FaceLength, string idwFingerIndex, string iflag, string sEnabled,string Temp6,string Temp7)
    {

        int retval = 0;
        if (FaceLength == "")
        {
            FaceLength = "0";
        }
        if (idwFingerIndex == "")
        {
            idwFingerIndex = "0";
        }
        if (iflag == "")
        {
            iflag = "0";
        }
       
          

            PassDataToSql[] paramList = new PassDataToSql[17];

            paramList[0] = new PassDataToSql("@Emp_Id", enrollno, PassDataToSql.ParaTypeList.Nvarchar, PassDataToSql.ParaDirectonList.Input);
            paramList[1] = new PassDataToSql("@CardNo", EmpCardNo, PassDataToSql.ParaTypeList.Nvarchar, PassDataToSql.ParaDirectonList.Input);

            paramList[2] = new PassDataToSql("@Template1", priv, PassDataToSql.ParaTypeList.Nvarchar, PassDataToSql.ParaDirectonList.Input);

            paramList[3] = new PassDataToSql("@Template2", pwd, PassDataToSql.ParaTypeList.Nvarchar, PassDataToSql.ParaDirectonList.Input);
            paramList[4] = new PassDataToSql("@Template3", fingertemp, PassDataToSql.ParaTypeList.Nvarchar, PassDataToSql.ParaDirectonList.Input);
            //face data
            paramList[5] = new PassDataToSql("@Template4", FaceData, PassDataToSql.ParaTypeList.Text, PassDataToSql.ParaDirectonList.Input);
            //face index
            paramList[6] = new PassDataToSql("@Template5", FaceIndex, PassDataToSql.ParaTypeList.Nvarchar, PassDataToSql.ParaDirectonList.Input);

            paramList[7] = new PassDataToSql("@Template6",Temp6,PassDataToSql.ParaTypeList.Bit, PassDataToSql.ParaDirectonList.Input);
            paramList[8] = new PassDataToSql("@Template7", Temp7, PassDataToSql.ParaTypeList.Bit, PassDataToSql.ParaDirectonList.Input);

            paramList[9] = new PassDataToSql("@Template8", FaceLength, PassDataToSql.ParaTypeList.Int, PassDataToSql.ParaDirectonList.Input);
            paramList[10] = new PassDataToSql("@Template9", DateTime.Now.ToString(), PassDataToSql.ParaTypeList.Date, PassDataToSql.ParaDirectonList.Input);
            paramList[11] = new PassDataToSql("@Template10", DateTime.Now.ToString(), PassDataToSql.ParaTypeList.Date, PassDataToSql.ParaDirectonList.Input);

            paramList[12] = new PassDataToSql("@ReferenceID", "0", PassDataToSql.ParaTypeList.Int, PassDataToSql.ParaDirectonList.Output);

            paramList[13] = new PassDataToSql("@idwFingerIndex", idwFingerIndex, PassDataToSql.ParaTypeList.Int, PassDataToSql.ParaDirectonList.Input);

            paramList[14] = new PassDataToSql("@iflag", iflag, PassDataToSql.ParaTypeList.Int, PassDataToSql.ParaDirectonList.Input);
            paramList[15] = new PassDataToSql("@sEnabled", sEnabled, PassDataToSql.ParaTypeList.Nvarchar, PassDataToSql.ParaDirectonList.Input);
            paramList[16] = new PassDataToSql("@Status", "", PassDataToSql.ParaTypeList.Nvarchar, PassDataToSql.ParaDirectonList.Input);

            daClass.execute_Sp("Set_EmployeeInformation_Update", paramList);

            retval = Convert.ToInt32(paramList[12].ParaValue);

      
        return retval;
    }



    public int UpdateAccessControlFingerInfo(string enrollno, string EmpCardNo, string pwd, string priv, string fingertemp, string FaceData, string FaceIndex, string FaceLength, string idwFingerIndex, string iflag, string sEnabled, string Temp6, string Temp7)
    {

        int retval = 0;
        if (FaceLength == "")
        {
            FaceLength = "0";
        }
        if (idwFingerIndex == "")
        {
            idwFingerIndex = "0";
        }
        if (iflag == "")
        {
            iflag = "0";
        }



        PassDataToSql[] paramList = new PassDataToSql[17];

        paramList[0] = new PassDataToSql("@Emp_Id", enrollno, PassDataToSql.ParaTypeList.Nvarchar, PassDataToSql.ParaDirectonList.Input);
        paramList[1] = new PassDataToSql("@CardNo", EmpCardNo, PassDataToSql.ParaTypeList.Nvarchar, PassDataToSql.ParaDirectonList.Input);

        paramList[2] = new PassDataToSql("@Template1", priv, PassDataToSql.ParaTypeList.Nvarchar, PassDataToSql.ParaDirectonList.Input);

        paramList[3] = new PassDataToSql("@Template2", pwd, PassDataToSql.ParaTypeList.Nvarchar, PassDataToSql.ParaDirectonList.Input);
        paramList[4] = new PassDataToSql("@Template3", fingertemp, PassDataToSql.ParaTypeList.Nvarchar, PassDataToSql.ParaDirectonList.Input);
        //face data
        paramList[5] = new PassDataToSql("@Template4", FaceData, PassDataToSql.ParaTypeList.Text, PassDataToSql.ParaDirectonList.Input);
        //face index
        paramList[6] = new PassDataToSql("@Template5", FaceIndex, PassDataToSql.ParaTypeList.Nvarchar, PassDataToSql.ParaDirectonList.Input);

        paramList[7] = new PassDataToSql("@Template6", Temp6, PassDataToSql.ParaTypeList.Bit, PassDataToSql.ParaDirectonList.Input);
        paramList[8] = new PassDataToSql("@Template7", Temp7, PassDataToSql.ParaTypeList.Bit, PassDataToSql.ParaDirectonList.Input);

        paramList[9] = new PassDataToSql("@Template8", FaceLength, PassDataToSql.ParaTypeList.Int, PassDataToSql.ParaDirectonList.Input);
        paramList[10] = new PassDataToSql("@Template9", DateTime.Now.ToString(), PassDataToSql.ParaTypeList.Date, PassDataToSql.ParaDirectonList.Input);
        paramList[11] = new PassDataToSql("@Template10", DateTime.Now.ToString(), PassDataToSql.ParaTypeList.Date, PassDataToSql.ParaDirectonList.Input);

        paramList[12] = new PassDataToSql("@ReferenceID", "0", PassDataToSql.ParaTypeList.Int, PassDataToSql.ParaDirectonList.Output);

        paramList[13] = new PassDataToSql("@idwFingerIndex", idwFingerIndex, PassDataToSql.ParaTypeList.Int, PassDataToSql.ParaDirectonList.Input);

        paramList[14] = new PassDataToSql("@iflag", iflag, PassDataToSql.ParaTypeList.Int, PassDataToSql.ParaDirectonList.Input);
        paramList[15] = new PassDataToSql("@sEnabled",sEnabled, PassDataToSql.ParaTypeList.Nvarchar, PassDataToSql.ParaDirectonList.Input);
        paramList[16] = new PassDataToSql("@Status", "", PassDataToSql.ParaTypeList.Nvarchar, PassDataToSql.ParaDirectonList.Input);

        daClass.execute_Sp("Set_EmployeeInformation_UpdateFingerData", paramList);

        retval = Convert.ToInt32(paramList[12].ParaValue);


        return retval;
    }



    public int UpdateAccessControlFaceInfo(string enrollno, string EmpCardNo, string pwd, string priv, string fingertemp, string FaceData, string FaceIndex, string FaceLength, string idwFingerIndex, string iflag, string sEnabled, string Temp6, string Temp7)
    {

        int retval = 0;
        if (FaceLength == "")
        {
            FaceLength = "0";
        }
        if (idwFingerIndex == "")
        {
            idwFingerIndex = "0";
        }
        if (iflag == "")
        {
            iflag = "0";
        }



        PassDataToSql[] paramList = new PassDataToSql[17];

        paramList[0] = new PassDataToSql("@Emp_Id", enrollno, PassDataToSql.ParaTypeList.Nvarchar, PassDataToSql.ParaDirectonList.Input);
        paramList[1] = new PassDataToSql("@CardNo", EmpCardNo, PassDataToSql.ParaTypeList.Nvarchar, PassDataToSql.ParaDirectonList.Input);

        paramList[2] = new PassDataToSql("@Template1", priv, PassDataToSql.ParaTypeList.Nvarchar, PassDataToSql.ParaDirectonList.Input);

        paramList[3] = new PassDataToSql("@Template2", pwd, PassDataToSql.ParaTypeList.Nvarchar, PassDataToSql.ParaDirectonList.Input);
        paramList[4] = new PassDataToSql("@Template3", fingertemp, PassDataToSql.ParaTypeList.Nvarchar, PassDataToSql.ParaDirectonList.Input);
        //face data
        paramList[5] = new PassDataToSql("@Template4", FaceData, PassDataToSql.ParaTypeList.Text, PassDataToSql.ParaDirectonList.Input);
        //face index
        paramList[6] = new PassDataToSql("@Template5", FaceIndex, PassDataToSql.ParaTypeList.Nvarchar, PassDataToSql.ParaDirectonList.Input);

        paramList[7] = new PassDataToSql("@Template6", Temp6, PassDataToSql.ParaTypeList.Bit, PassDataToSql.ParaDirectonList.Input);
        paramList[8] = new PassDataToSql("@Template7", Temp7, PassDataToSql.ParaTypeList.Bit, PassDataToSql.ParaDirectonList.Input);

        paramList[9] = new PassDataToSql("@Template8", FaceLength, PassDataToSql.ParaTypeList.Int, PassDataToSql.ParaDirectonList.Input);
        paramList[10] = new PassDataToSql("@Template9", DateTime.Now.ToString(), PassDataToSql.ParaTypeList.Date, PassDataToSql.ParaDirectonList.Input);
        paramList[11] = new PassDataToSql("@Template10", DateTime.Now.ToString(), PassDataToSql.ParaTypeList.Date, PassDataToSql.ParaDirectonList.Input);

        paramList[12] = new PassDataToSql("@ReferenceID", "0", PassDataToSql.ParaTypeList.Int, PassDataToSql.ParaDirectonList.Output);

        paramList[13] = new PassDataToSql("@idwFingerIndex", idwFingerIndex, PassDataToSql.ParaTypeList.Int, PassDataToSql.ParaDirectonList.Input);

        paramList[14] = new PassDataToSql("@iflag", iflag, PassDataToSql.ParaTypeList.Int, PassDataToSql.ParaDirectonList.Input);
        paramList[15] = new PassDataToSql("@sEnabled",sEnabled, PassDataToSql.ParaTypeList.Nvarchar, PassDataToSql.ParaDirectonList.Input);
        paramList[16] = new PassDataToSql("@Status", "", PassDataToSql.ParaTypeList.Nvarchar, PassDataToSql.ParaDirectonList.Input);

        daClass.execute_Sp("Set_EmployeeInformation_UpdateFaceData", paramList);

        retval = Convert.ToInt32(paramList[12].ParaValue);


        return retval;
    }


    public int DeleteEmployeeInformationByEmpId(string EmpId)
    {
     
        PassDataToSql[] paramList = new PassDataToSql[2];
        paramList[0] = new PassDataToSql("@Emp_Id", EmpId, PassDataToSql.ParaTypeList.Int, PassDataToSql.ParaDirectonList.Input);
         paramList[1] = new PassDataToSql("@ReferenceID", "0", PassDataToSql.ParaTypeList.Int, PassDataToSql.ParaDirectonList.Output);
        daClass.execute_Sp("Set_EmployeeInformation_Delete", paramList);
        return Convert.ToInt32(paramList[1].ParaValue);
    }
    public DataTable  GetEmployeeAccessInfoByEmpId(string empid )
    {
        DataTable dt=new DataTable ();
        //string str = string.Empty;
        // str = "";
        PassDataToSql[] paramList = new PassDataToSql[2];
        paramList[0] = new PassDataToSql("@Emp_Id",empid, PassDataToSql.ParaTypeList.Int, PassDataToSql.ParaDirectonList.Input);
        paramList[1] = new PassDataToSql("@Optype","1", PassDataToSql.ParaTypeList.Int, PassDataToSql.ParaDirectonList.Input);
          dt= daClass.Reuturn_Datatable_Search("sp_Set_EmployeeInformation_SelectRow", paramList);
          return dt;
    }
    public DataTable GetEmployeeAccessInfo()
    {
        DataTable dt = new DataTable();
        //string str = string.Empty;
        // str = "";
        PassDataToSql[] paramList = new PassDataToSql[2];
        paramList[0] = new PassDataToSql("@Emp_Id","0", PassDataToSql.ParaTypeList.Int, PassDataToSql.ParaDirectonList.Input);
        paramList[1] = new PassDataToSql("@Optype", "2", PassDataToSql.ParaTypeList.Int, PassDataToSql.ParaDirectonList.Input);
        dt = daClass.Reuturn_Datatable_Search("sp_Set_EmployeeInformation_SelectRow", paramList);
        return dt;
    }
}
