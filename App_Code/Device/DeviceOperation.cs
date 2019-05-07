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
using zkemkeeper;
//using zkemkeeper

/// <summary>
/// Summary description for DeviceOperation
/// </summary>
public class DeviceOperation
{
    int DeviceNumber = 1;
    int Year = 0;
    int Month = 0;
    int Day = 0;
    int Hour = 0;
    int Minute = 0;
    int Second = 0;
    int WorkCode = 0;
    string StaffDeviceCode = "0";
    int VerifyMode = 0;
    string Timestr = null;
    int PunchDirection = 0;
    string Seperator = ",";
    string IPAddr = string.Empty;
    int  ErrorCode = 0;
    bool IsDeviceConnected = false;
    int Port = 0;

    zkemkeeper.CZKEM AxCZKEM1 = new zkemkeeper.CZKEM();
	public DeviceOperation()
	{
		//
		// TODO: Add constructor logic here
		//
	}


    public DeviceOperation(string IP, int Port)
    {
        IPAddr = IP;
        this.Port = Port;
    }

    public bool ConnectDevice()
    {
        bool retval = false;
        try
        {


            if (AxCZKEM1.Connect_Net(IPAddr, Port))
            {
                retval = true;
                IsDeviceConnected = true;

            }
               
            else
            {
               AxCZKEM1.GetLastError(ref ErrorCode);
            }
        }
        catch (Exception ex)
        {
           
            return false; 
           
        }
        return retval;
    }

    public void DisconnectDevice()
    {
        
        try
        {
            
                AxCZKEM1.Disconnect();
            
            IsDeviceConnected = false;
          
            
        }
        catch (Exception)
        {
            
            
        }

    }

    public DataTable DownloadUser()
    {
        if (!IsDeviceConnected)
        {
            ConnectDevice();
        }
        string DeviceStaffCode = "0";
        string DeviceStaffName = "0";
        int Privilege = 0;
        bool Enabled = false;
        string DeviceStaffPassword = "";
        string UserRecordFormat = null;
        string CardNo = "";
        string FingerPrintTemplate = "";
        int tempLength = 0;
        string FilePath = null;
        string ZKFinger = null;
        int FingerPosition = 0;



        AxCZKEM1.GetSysOption(1, "~ZKFPVersion",out ZKFinger);


        if (ZKFinger == "10")
        {
            FingerPosition = 15;
        }
        else
        {
            FingerPosition = 0;
        }

         DataTable dtUser = new DataTable();
            dtUser = GetUserDataTable();

        if (AxCZKEM1.ReadAllUserID(Convert.ToInt32(DeviceNumber)))
        {
            bool GetUsersFromDeviceStatus = false;

            GetUsersFromDeviceStatus = AxCZKEM1.SSR_GetAllUserInfo(DeviceNumber,out DeviceStaffCode,out DeviceStaffName,out DeviceStaffPassword, out Privilege,out Enabled);


           


            while (GetUsersFromDeviceStatus)
            {
                AxCZKEM1.GetStrCardNumber(out CardNo);

                AxCZKEM1.SSR_GetUserTmpStr(DeviceNumber, DeviceStaffCode, FingerPosition,out FingerPrintTemplate,out tempLength);

                UserRecordFormat = DeviceStaffName.ToString() + Seperator + DeviceStaffCode.ToString() + Seperator + Privilege.ToString() + Seperator + CardNo.ToString() + Seperator + DeviceStaffPassword.ToString() + Seperator + FingerPrintTemplate;
                //120,200
                try
                {
                    DataRow row = dtUser.NewRow();

                    row["Name"] = DeviceStaffName == null ? "" : DeviceStaffName;
                    row["EnrollId"] = DeviceStaffCode == null ? "" : DeviceStaffCode;
                    row["Privilege"] = Privilege;
                    row["CardNo"] = CardNo == null ? "" : CardNo;
                    row["DeviceStaffPassword"] = DeviceStaffPassword == null ? "" : DeviceStaffPassword;


                    if (FingerPrintTemplate == null)
                    {
                        row["FingerTemplate"] = "";
                    }
                    else
                    {
                        row["FingerTemplate"] = FingerPrintTemplate;
                    }
                    row["IsFingerTemp"]= FingerPrintTemplate==null || FingerPrintTemplate.Trim()=="" ?"False":"True";

                    dtUser.Rows.Add(row);


                }
                catch (Exception ex)
                {
                }

                

                GetUsersFromDeviceStatus = AxCZKEM1.SSR_GetAllUserInfo(DeviceNumber,out DeviceStaffCode,out DeviceStaffName,out DeviceStaffPassword,out Privilege,out Enabled);

            }

         


        }
        else
        {
        }
        if (IsDeviceConnected)
        {
            DisconnectDevice();
        }
        return dtUser;
    }

    public DataTable DownloadFaceTemplate()
    {
       
        string sUserID = "";
        string sName = "";
        string sPassword = "";
        int iPrivilege = 0;
        bool bEnabled = false;

        int iFaceIndex = 50;
        //the only possible parameter value
        string sTmpData = "";
        int iLength = 0;

        if (!IsDeviceConnected)
        {
            ConnectDevice();
        }

        DataTable dtFaceDownlaod = new DataTable();
        dtFaceDownlaod.Columns.Add(new DataColumn("sUserID"));
        dtFaceDownlaod.Columns.Add(new DataColumn("sName"));
        dtFaceDownlaod.Columns.Add(new DataColumn("sPassword"));
        dtFaceDownlaod.Columns.Add(new DataColumn("iPrivilege"));
        dtFaceDownlaod.Columns.Add(new DataColumn("iFaceIndex"));
        dtFaceDownlaod.Columns.Add(new DataColumn("sTmpData"));
        dtFaceDownlaod.Columns.Add(new DataColumn("iLength"));



        AxCZKEM1.EnableDevice(DeviceNumber, false);
        AxCZKEM1.ReadAllUserID(DeviceNumber);
        //read all the user information to the memory

        //get all the users' information from the memory
        try
        {
            while (AxCZKEM1.SSR_GetAllUserInfo(DeviceNumber, out sUserID, out sName, out sPassword, out iPrivilege, out bEnabled) == true)
            {
                //get the face templates from the memory
                if (AxCZKEM1.GetUserFaceStr(DeviceNumber, sUserID, iFaceIndex, ref sTmpData, ref iLength) == true)
                {
                    DataRow row = dtFaceDownlaod.NewRow();
                    row["sUserID"] = sUserID == null ? "" : sUserID;
                    row["sName"] = sName == null ? "" : sName;

                    row["sPassword"] = sPassword == null ? "" : sPassword;
                    row["iPrivilege"] = iPrivilege;
                    row["iFaceIndex"] = iFaceIndex;
                    row["sTmpData"] = sTmpData == null ? "" : sTmpData.ToString();
                    row["iLength"] = iLength;


                    dtFaceDownlaod.Rows.Add(row);
                }
            }

            AxCZKEM1.EnableDevice(DeviceNumber, true);
        }
        catch (Exception)
        {
            return dtFaceDownlaod;
           
        }
        return dtFaceDownlaod;
    
    }

    public DataTable GetUserDataTable()
    {
        DataTable dt = new DataTable();
        //(Name, EnrollId,Privilege,CardNo,DeviceStaffPassword,FingerTemplate)VALUES     ('" & DeviceStaffName & "', '" & DeviceStaffCode & "','" & Privilege.ToString() & "','" & CardNo & "','" & DeviceStaffPassword & "','" & FingerPrintTemplate & "')"
        dt.Columns.Add(new DataColumn("Name"));
        dt.Columns.Add(new DataColumn("EnrollId"));
        dt.Columns.Add(new DataColumn("Privilege"));
        dt.Columns.Add(new DataColumn("CardNo"));
        dt.Columns.Add(new DataColumn("DeviceStaffPassword"));
        dt.Columns.Add(new DataColumn("FingerTemplate"));
        dt.Columns.Add("IsFingerTemp");
        return dt;
    }


    public DataTable DownloadAttendanceLogs()
    {
        if (!IsDeviceConnected)
        {
            ConnectDevice();
        }
        bool LogStatus = true;
        string Timestr = null;
        string LogFormat = null;
        string FilePath = null;
        DataTable dtUserLog = new DataTable();
        dtUserLog = GetUserLogDataTable();
        //Device Number is any +ve integer value in case of TCP(ethernet) communication
        if (AxCZKEM1.ReadAllGLogData(DeviceNumber))
        {

            while ((LogStatus))
            {

                try
                {
                    LogStatus = AxCZKEM1.SSR_GetGeneralLogData(DeviceNumber,out StaffDeviceCode,out VerifyMode,out PunchDirection,out Year,out  Month,out Day,out Hour,out Minute,out Second,
                  ref  WorkCode);


                }
                catch (Exception ex)
                {

                }


                if (LogStatus)
                {
                    Timestr = new DateTime(Year,Month,Day,Hour,Minute,Second).ToString();
                       // Convert.ToString(Year) + "-" + String.Format("0#", Month) + "-" + String.Format("0#", Day) + " " + String.Format("0#", Hour) + ":" + String.Format("0#", Minute) + ":" + String.Format("0#", Second);

                    LogFormat = StaffDeviceCode.ToString() + Seperator + Timestr.ToString() + Seperator + PunchDirection.ToString();

                    try
                    {
                        DataRow row = dtUserLog.NewRow();

                        row["iMachineNumber"] = DeviceNumber;
                        row["dwEnrollNumber"] = StaffDeviceCode;
                        row["dwVerifyMode"] = VerifyMode;
                        row["dwInOutMode"] = PunchDirection;
                        row["timeStr"] = Timestr;


                        dtUserLog.Rows.Add(row);


                    }
                    catch (Exception ex)
                    {
                    }



                    //& DeviceNumber & "','" & StaffDeviceCode & "','" & VerifyMode & "','" & PunchDirection & "','" & Timestr.ToString() & "')"




                }
            }

        }


        if (IsDeviceConnected)
        {
            DisconnectDevice();
        }
        return dtUserLog;
    }

    public DataTable GetUserLogDataTable()
    {
        DataTable dt = new DataTable();
        //cmd.CommandText = "Insert Into TblLogData (iMachineNumber,dwEnrollNumber,dwVerifyMode,dwInOutMode,timeStr) Values ('" & DeviceNumber & "','" & StaffDeviceCode & "','" & VerifyMode & "','" & PunchDirection & "','" & Timestr.ToString() & "')"
        dt.Columns.Add(new DataColumn("iMachineNumber"));
        dt.Columns.Add(new DataColumn("dwEnrollNumber"));
        dt.Columns.Add(new DataColumn("dwVerifyMode"));
        dt.Columns.Add(new DataColumn("dwInOutMode"));
        dt.Columns.Add(new DataColumn("timeStr"));


        return dt;
    }

    public int GetErrorCode()
    {
        return ErrorCode;
    }

    public bool ClearLogData()
    {
        int idwErrorCode = 0;
        bool retval = false;


        if (!IsDeviceConnected)
        {
            ConnectDevice();
        }
        AxCZKEM1.EnableDevice(DeviceNumber, false);//disable the device
        if (AxCZKEM1.ClearGLog(DeviceNumber))
        {
            AxCZKEM1.RefreshData(DeviceNumber);//the data in the device should be refreshed
            retval = true;
        }

        AxCZKEM1.EnableDevice(DeviceNumber, true);//enable the device
         return retval;
    }

    public bool SaveUserInDevice(string UserName,string EnrollId,string privg,string CardNo,string DeviceId,string FingerTemplate,string pwd,string FaceIndex,string FaceData,string FaceLength)
    {
        

        bool retval = false;
        if (!IsDeviceConnected)
        {
            ConnectDevice();
        }
        string DeviceStaffName = "";
        //Code Modified on June 7   
        //int DeviceStaffCode = 0;
          
        string DeviceStaffCode = "0";
        int Privilege = 0;
        string DeviceStaffPassword = "";
        
        string FingerPrintTemplate = "";
        string ZKFinger = null;
        string UserRecord = null;
        bool eResult = false;
        int FingerPosition = 0;




        AxCZKEM1.GetSysOption(1, "~ZKFPVersion",out ZKFinger);



        if (ZKFinger == "10")
        {
            FingerPosition = 15;
        }
        else
        {
            FingerPosition = 0;
        }



        try
        {



            DeviceStaffName = UserName;
            DeviceStaffCode = EnrollId;
            Privilege = Convert.ToInt32( privg);
            CardNo = CardNo;
            DeviceStaffPassword = pwd;
            FingerPrintTemplate = FingerTemplate;
            DeviceId = "1";

            

            eResult = AxCZKEM1.SSR_SetUserInfo(Convert.ToInt32(DeviceId), DeviceStaffCode.ToString(), DeviceStaffName.Trim(), DeviceStaffPassword, Privilege, true);
           //code modified on june 7 
            
            //AxCZKEM1.DelUserTmp(Convert.ToInt32(DeviceId), Convert.toInt32(DeviceStaffCode), FingerPosition);
            
            //updated code 
            AxCZKEM1.SSR_DelUserTmp(Convert.ToInt32(DeviceId), DeviceStaffCode.ToString(), FingerPosition);
            

            eResult = AxCZKEM1.SSR_SetUserTmpStr(Convert.ToInt32(DeviceId), DeviceStaffCode.ToString(), FingerPosition, Convert.ToString(FingerPrintTemplate));
            //begin here code merged on mar 6 for face  save
            if (AxCZKEM1.DelUserFace(Convert.ToInt32(DeviceNumber), DeviceStaffCode.ToString(), Convert.ToInt32(FaceIndex)) == true)
             {
                 AxCZKEM1.RefreshData(Convert.ToInt32(DeviceNumber));
             }//the data in the device should be refreshed

            eResult = AxCZKEM1.SetUserFaceStr(Convert.ToInt32(DeviceNumber), DeviceStaffCode.ToString(), Convert.ToInt32(FaceIndex), FaceData, Convert.ToInt32(FaceLength));

            //end

            AxCZKEM1.RefreshData(DeviceNumber);


            retval = true;


        }
        catch (Exception ex)
        {
            return false;
        }


        if (IsDeviceConnected)
        {
            DisconnectDevice();
        }
         return retval;
    }
  

    public bool IsUserExist(string EnrollId)
    {
        bool retval = false;
         DataTable dt=DownloadUser();

         if (dt.Rows.Count > 0)
         {
             DataTable dtFilter = new DataView(dt, "EnrollId='"+EnrollId+"'", "", DataViewRowState.CurrentRows).ToTable();
             if (dtFilter.Rows.Count > 0)
             {
                 retval = true;
             }
         
         }
         return retval;
    }

    public DataTable DownloadUserWithFaceTemplate()
    {
        //download user

        //row["Name"] = DeviceStaffName;
        //row["EnrollId"] = DeviceStaffCode;
        //row["Privilege"] = Privilege;
        //row["CardNo"] = CardNo;
        //row["DeviceStaffPassword"] = DeviceStaffPassword;

        //row["FingerTemplate"] = FingerPrintTemplate;

        //dtFaceDownlaod.Columns.Add(new DataColumn("sUserID"));
        //dtFaceDownlaod.Columns.Add(new DataColumn("sName"));
        //dtFaceDownlaod.Columns.Add(new DataColumn("sPassword"));
        //dtFaceDownlaod.Columns.Add(new DataColumn("iPrivilege"));
        //dtFaceDownlaod.Columns.Add(new DataColumn("iFaceIndex"));
        //dtFaceDownlaod.Columns.Add(new DataColumn("sTmpData"));
        //dtFaceDownlaod.Columns.Add(new DataColumn("iLength"));
        DataTable dtUser = DownloadUser();
      

        dtUser.Columns.Add("FaceIndex");
        dtUser.Columns.Add("FaceData");
        dtUser.Columns.Add("FaceLength");
       
        dtUser.Columns.Add("IsFaceTemp");

        if (dtUser.Rows.Count == 0)
        {
            return dtUser;
        }

        //code commented on  june 11 because we are to download only user ,Face Download Service Download Face

       // DataTable dtFaceTemplate = DownloadFaceTemplate();

        //foreach (DataRow dr in dtUser.Rows)
        //{
        //    //filter dtfacetemplate for according to enroll no
        //    DataTable dtTempFace = new DataView(dtFaceTemplate, "sUserID='" + dr["EnrollId"].ToString() + "'", "", DataViewRowState.CurrentRows).ToTable();

        //    if (dtTempFace.Rows.Count > 0)
        //    {
        //        dr["FaceIndex"] = dtTempFace.Rows[0]["iFaceIndex"].ToString();
        //        dr["FaceData"] = dtTempFace.Rows[0]["sTmpData"].ToString(); ;
        //        dr["FaceLength"] = dtTempFace.Rows[0]["iLength"].ToString();
        //        dr["IsFaceTemp"] = "True";


        //    }
        //    else
        //    {
        //        dr["FaceIndex"] = "0";
        //        dr["FaceData"] = "";
        //        dr["FaceLength"] = "0";
        //        dr["IsFaceTemp"] = "False";
        //    }


        //}
        //Code Commented End

        return dtUser;

    }

    public DataTable DownloadUserWithFaceTemplateByEnrollId(DataTable dtEnrollId)
    {
        //download user

        //row["Name"] = DeviceStaffName;
        //row["EnrollId"] = DeviceStaffCode;
        //row["Privilege"] = Privilege;
        //row["CardNo"] = CardNo;
        //row["DeviceStaffPassword"] = DeviceStaffPassword;

        //row["FingerTemplate"] = FingerPrintTemplate;

        //dtFaceDownlaod.Columns.Add(new DataColumn("sUserID"));
        //dtFaceDownlaod.Columns.Add(new DataColumn("sName"));
        //dtFaceDownlaod.Columns.Add(new DataColumn("sPassword"));
        //dtFaceDownlaod.Columns.Add(new DataColumn("iPrivilege"));
        //dtFaceDownlaod.Columns.Add(new DataColumn("iFaceIndex"));
        //dtFaceDownlaod.Columns.Add(new DataColumn("sTmpData"));
        //dtFaceDownlaod.Columns.Add(new DataColumn("iLength"));
        DataTable dtUser = DownloadUserByEnrollNo(dtEnrollId);
        dtUser.Columns.Add("FaceIndex");
        dtUser.Columns.Add("FaceData");
        dtUser.Columns.Add("FaceLength");

        dtUser.Columns.Add("IsFaceTemp");


        DataTable dtFaceTemplate = DownloadFaceTemplate();

        foreach (DataRow dr in dtUser.Rows)
        {
            //filter dtfacetemplate for according to enroll no
            DataTable dtTempFace = new DataView(dtFaceTemplate, "sUserID='" + dr["EnrollId"].ToString() + "'", "", DataViewRowState.CurrentRows).ToTable();

            if (dtTempFace.Rows.Count > 0)
            {
                dr["FaceIndex"] = dtTempFace.Rows[0]["iFaceIndex"].ToString();
                dr["FaceData"] = dtTempFace.Rows[0]["sTmpData"].ToString(); ;
                dr["FaceLength"] = dtTempFace.Rows[0]["iLength"].ToString();
                dr["IsFaceTemp"] = "True";


            }
            else
            {
                dr["FaceIndex"] = "0";
                dr["FaceData"] = "";
                dr["FaceLength"] = "0";
                dr["IsFaceTemp"] = "False";
            }


        }

        return dtUser;

    }


    public bool DeleteUser(string EnrollId)
    {
        // return false;
        bool retval = false;
        if (!IsDeviceConnected)
        {
            ConnectDevice();
        }
        // code begin  for clear password info from device for given enroll no
       // DataTable dtData = DownloadUserWithFaceTemplate();
        //if (dtData.Rows.Count > 0)
        //{
        //    DataTable dtFilter = new DataView(dtData, "EnrollId='" + EnrollId + "'", "", DataViewRowState.CurrentRows).ToTable();
        //    if (dtFilter.Rows.Count > 0)
        //    {
        //        //row["Name"] = DeviceStaffName;
        //        //   row["EnrollId"] = DeviceStaffCode;
        //        //   row["Privilege"] = Privilege;
        //        //   row["CardNo"] = CardNo;
        //        //   row["DeviceStaffPassword"] = DeviceStaffPassword;

        //        //   row["FingerTemplate"] = FingerPrintTemplate;


        //        SaveUserInDevice(dtFilter.Rows[0]["Name"].ToString(), EnrollId, dtFilter.Rows[0]["Privilege"].ToString(), dtFilter.Rows[0]["CardNo"].ToString(), "1", dtFilter.Rows[0]["FingerTemplate"].ToString(), "", dtFilter.Rows[0]["FaceIndex"].ToString(), dtFilter.Rows[0]["FaceData"].ToString(), dtFilter.Rows[0]["FaceLength"].ToString());
        //    }

        //}



        //end
        try
        {
            AxCZKEM1.DelUserFace(DeviceNumber, EnrollId, 50);

        }
        catch (Exception ex)
        {

        }

        try
        {
            for (int i = 0; i <= 1; i++)
            {
                if (AxCZKEM1.DeleteEnrollData(DeviceNumber, Convert.ToInt32(EnrollId), 1, i) == true)
                {
                    AxCZKEM1.RefreshData(DeviceNumber);
                }




                if (AxCZKEM1.SSR_DeleteEnrollData(DeviceNumber, EnrollId, i) == true)
                {
                    AxCZKEM1.RefreshData(DeviceNumber);
                    //the data in the device should be refreshed
                    // MsgBox("DeleteEnrollData,", MsgBoxStyle.Information, "Success")

                }
                else
                {
                    //MsgBox("Operation failed,ErrorCode=", MsgBoxStyle.Exclamation, "Error")
                }
            }

        }
        catch (Exception ex)
        {
        }
        AxCZKEM1.EnableDevice(1, true);

        AxCZKEM1.RefreshData(DeviceNumber);
        if (IsDeviceConnected)
        {
            DisconnectDevice();
        }
        return retval;
    }

    public bool ClearAdministrators()
    {
        bool retval = false;
        if (!IsDeviceConnected)
        {
            ConnectDevice();
        }
         retval= AxCZKEM1.ClearAdministrators(DeviceNumber);
         return retval;
    }

    public bool RestartDevice()
    {
        bool retval = false;
        try
        {
            ConnectDevice();
            retval = AxCZKEM1.RestartDevice(DeviceNumber);
        }
        catch (Exception)
        {
            
          
        }
         
          return retval;
    }

    public string getSDKVersion()
    {
        string retval = string.Empty;
        AxCZKEM1.GetSDKVersion(ref retval);
         return retval;
    }


    public DataTable DownloadUserByEnrollNo(DataTable dtDistinctEnrollNo)
    {
        if (!IsDeviceConnected)
        {
            ConnectDevice();
        }
        string DeviceStaffCode = "0";
        string DeviceStaffName = "0";
        int Privilege = 0;
        bool Enabled = false;
        string DeviceStaffPassword = "";
        string UserRecordFormat = null;
        string CardNo = "";
        string FingerPrintTemplate = "";
        int tempLength = 0;
        string FilePath = null;
        string ZKFinger = null;
        int FingerPosition = 0;



        AxCZKEM1.GetSysOption(1, "~ZKFPVersion", out ZKFinger);


        if (ZKFinger == "10")
        {
            FingerPosition = 15;
        }
        else
        {
            FingerPosition = 0;
        }

        DataTable dtUser = new DataTable();
        dtUser = GetUserDataTable();

        if (AxCZKEM1.ReadAllUserID(Convert.ToInt32(DeviceNumber)))
        {
            bool GetUsersFromDeviceStatus = false;

            GetUsersFromDeviceStatus = AxCZKEM1.SSR_GetAllUserInfo(DeviceNumber, out DeviceStaffCode, out DeviceStaffName, out DeviceStaffPassword, out Privilege, out Enabled);



            int UserCount = 0;

            while (GetUsersFromDeviceStatus)
            {
              
                
                AxCZKEM1.GetStrCardNumber(out CardNo);

                AxCZKEM1.SSR_GetUserTmpStr(DeviceNumber, DeviceStaffCode, FingerPosition, out FingerPrintTemplate, out tempLength);

                UserRecordFormat = DeviceStaffName.ToString() + Seperator + DeviceStaffCode.ToString() + Seperator + Privilege.ToString() + Seperator + CardNo.ToString() + Seperator + DeviceStaffPassword.ToString() + Seperator + FingerPrintTemplate;

                DataTable dtSingleRec = new DataView(dtDistinctEnrollNo, "dwEnrollNumber='"+DeviceStaffCode.Trim()+"'", "", DataViewRowState.CurrentRows).ToTable();

                
                if (dtSingleRec.Rows.Count == 0)
                {
                    GetUsersFromDeviceStatus = AxCZKEM1.SSR_GetAllUserInfo(DeviceNumber, out DeviceStaffCode, out DeviceStaffName, out DeviceStaffPassword, out Privilege, out Enabled);
                    continue;
                }
                else
                {
                    UserCount++;
                   
                   
                }
                //120,200
                try
                {
                    DataRow row = dtUser.NewRow();

                    row["Name"] = DeviceStaffName == null ? "" : DeviceStaffName;
                    row["EnrollId"] = DeviceStaffCode == null ? "" : DeviceStaffCode;
                    row["Privilege"] = Privilege;
                    row["CardNo"] = CardNo == null ? "" : CardNo;
                    row["DeviceStaffPassword"] = DeviceStaffPassword == null ? "" : DeviceStaffPassword;


                    if (FingerPrintTemplate == null)
                    {
                        row["FingerTemplate"] = "";
                    }
                    else
                    {
                        row["FingerTemplate"] = FingerPrintTemplate;
                    }
                    row["IsFingerTemp"] = FingerPrintTemplate == null || FingerPrintTemplate.Trim() == "" ? "False" : "True";

                    dtUser.Rows.Add(row);


                }
                catch (Exception ex)
                {
                }
                if (UserCount == dtDistinctEnrollNo.Rows.Count)
                {
                    break;
                }


                GetUsersFromDeviceStatus = AxCZKEM1.SSR_GetAllUserInfo(DeviceNumber, out DeviceStaffCode, out DeviceStaffName, out DeviceStaffPassword, out Privilege, out Enabled);

            }




        }
        else
        {
        }
        if (IsDeviceConnected)
        {
            DisconnectDevice();
        }
        return dtUser;
    }

    public bool SetDeviceTime()
    {
        bool retval = false;
        if (!IsDeviceConnected)
        {
            ConnectDevice();
        }
        try
        {
            retval = AxCZKEM1.SetDeviceTime(DeviceNumber);
        }
        catch (Exception)
        {
            
          
        }
        if (IsDeviceConnected)
        {
            DisconnectDevice();
        }
         return retval;
    }

     public bool SetDeviceTime(int year,int Month,int Day,int Hour,int Minut,int Second)
    {
       //CZKEM1.SetDeviceTime2 MACHINENUMBER, dwYear, dwMonth, dwDay, dwHour, dwMinute, dwSecond
        bool retval = false;
        if (!IsDeviceConnected)
        {
            ConnectDevice();
        }
        try
        {
            retval = AxCZKEM1.SetDeviceTime2(DeviceNumber, year, Month, Day, Hour, Minut, Second);
        }
        catch (Exception)
        {
            
            throw;
        }
        if (IsDeviceConnected)
        {
            DisconnectDevice();
        }
         return retval;
    }

     public bool InitializeDevice()
     {
         bool retval = false;
         try
         {
             if (!IsDeviceConnected)
             {
                 ConnectDevice();
             }
            retval=  AxCZKEM1.ClearKeeperData(DeviceNumber);
            if (IsDeviceConnected)
            {
                DisconnectDevice();
            }
         }
         catch (Exception)
         {
             
             throw;
         }
         return retval;
     }


     public bool ChangeLanguage(int langid)
     { bool retval=false;
         try
         {
             if (!IsDeviceConnected)
             {
                 ConnectDevice();
             }
              retval = AxCZKEM1.SetDeviceInfo(DeviceNumber, 3, langid);
              if (IsDeviceConnected)
              {
                  DisconnectDevice();
              }
         }
         catch (Exception)
         {
             
             
         }
         return retval;

     }


     public bool PowerOffDevice()
     {
         bool retval = false;
         try
         {
             if (!IsDeviceConnected)
             {
                 ConnectDevice();
             }
             retval= AxCZKEM1.PowerOffDevice(DeviceNumber);
             if (IsDeviceConnected)
             {
                 DisconnectDevice();
             }
         }
         catch (Exception)
         {
             
            
         }
         return retval;
     }

     public bool GetSerialNo(out string SerialNo)
     { 
         
         bool retval=false;
         try
         {
             if (!IsDeviceConnected)
             {
                 ConnectDevice();
             }
             retval= AxCZKEM1.GetSerialNumber(DeviceNumber, out SerialNo);
             if (IsDeviceConnected)
             {
                 DisconnectDevice();
             }
         }
         catch (Exception)
         {
             SerialNo = "";
            
         }
         return retval;
     
     }

     public bool GetProcductCode(out string ProductCode)
     {

         bool retval = false;
         try
         {
             if (!IsDeviceConnected)
             {
                 ConnectDevice();
             }
             retval = AxCZKEM1.GetProductCode(DeviceNumber,out ProductCode);
             if (IsDeviceConnected)
             {
                 DisconnectDevice();
             }
         }
         catch (Exception)
         {
             ProductCode = "";

         }
         return retval;

     }

     public bool GetFirmwareVersion(out string FirmwareVersion)
     {
         FirmwareVersion = "";

         bool retval = false;
         try
         {
             if (!IsDeviceConnected)
             {
                 ConnectDevice();
             }
             retval = AxCZKEM1.GetFirmwareVersion(DeviceNumber, ref FirmwareVersion);
             if (IsDeviceConnected)
             {
                 DisconnectDevice();
             }
         }
         catch (Exception)
         {
             FirmwareVersion = "";

         }
         return retval;

     }

    


}

