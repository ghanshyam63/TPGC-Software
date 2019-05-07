﻿using System;
using System.Collections;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Xml.Linq;
using System.Data.SqlClient;

public partial class AttendanceReport_InOutReport : System.Web.UI.Page
{

    Att_InOutReport RptShift = new Att_InOutReport();
    SystemParameter objSys = new SystemParameter();
    CompanyMaster objComp=new CompanyMaster();
    Set_AddressChild ObjAddress=new Set_AddressChild ();
    protected void Page_Load(object sender, EventArgs e)
    {
        GetReport();
        Session["AccordianId"] = "6";
        Session["HeaderText"] = "Attendance Reports";
        
    }

   
    public void GetReport()
    {
        DateTime FromDate = new DateTime();
        DateTime ToDate = new DateTime();
        string Emplist = string.Empty;
        if (Session["EmpList"] == null && Session["FromDate"] == null && Session["ToDate"]==null)
        {
            Response.Redirect("../Attendance_Report/AttendanceReport.aspx");
        }
        else
        {
            FromDate = objSys.getDateForInput(Session["FromDate"].ToString());
            ToDate = objSys.getDateForInput(Session["ToDate"].ToString());

            Emplist = Session["EmpList"].ToString();

            DataTable dtFilter = new DataTable();

            AttendanceDataSet rptdata = new AttendanceDataSet();

            rptdata.EnforceConstraints = false;
            AttendanceDataSetTableAdapters.sp_Att_AttendanceRegister_ReportTableAdapter adp = new AttendanceDataSetTableAdapters.sp_Att_AttendanceRegister_ReportTableAdapter();

            adp.Fill(rptdata.sp_Att_AttendanceRegister_Report, Convert.ToDateTime(FromDate), Convert.ToDateTime(ToDate));



            if (Emplist != "")
            {
                dtFilter = new DataView(rptdata.sp_Att_AttendanceRegister_Report, "Emp_Id in (" + Emplist.Substring(0, Emplist.Length - 1) + ") ", "", DataViewRowState.CurrentRows).ToTable();
            }
          
            for (int k = 0; k < dtFilter.Rows.Count; k++)
            {

                if (Convert.ToBoolean(dtFilter.Rows[k]["Is_TempShift"].ToString()))
                {

                    dtFilter.Rows[k]["Shift_Name"] = "Temp Shift";
                }
              
                if (Convert.ToBoolean(dtFilter.Rows[k]["Is_Week_Off"].ToString()))
                {
                    dtFilter.Rows[k]["Field1"] = "Off";
                    dtFilter.Rows[k]["OverTime_Min"] = dtFilter.Rows[k]["Week_Off_Min"];
                }
                else if (Convert.ToBoolean(dtFilter.Rows[k]["Is_Holiday"].ToString()))
                {
                    dtFilter.Rows[k]["Field1"] = "Holiday";
                    dtFilter.Rows[k]["OverTime_Min"] = dtFilter.Rows[k]["Holiday_Min"];
                }
                else if (Convert.ToBoolean(dtFilter.Rows[k]["Is_Leave"].ToString()))
                {
                    dtFilter.Rows[k]["Field1"] = "Leave";

                }
                else if (Convert.ToBoolean(dtFilter.Rows[k]["Is_Absent"].ToString()))
                {
                    dtFilter.Rows[k]["Field1"] = "Absent";

                }
                else 
                {
                    dtFilter.Rows[k]["Field1"] = "Present";

                }


              
            }
            string CompanyName = "";
            string CompanyAddress = "";
            string Imageurl = "";

            DataTable DtCompany = objComp.GetCompanyMasterById(Session["CompId"].ToString());
            DataTable DtAddress = ObjAddress.GetAddressChildDataByAddTypeAndAddRefId("Company", Session["CompId"].ToString());
            if (DtCompany.Rows.Count > 0)
            {
                CompanyName = DtCompany.Rows[0]["Company_Name"].ToString();
                Imageurl = "~/CompanyResource/" + Session["CompId"].ToString() + "/" + DtCompany.Rows[0]["Logo_Path"].ToString();


            }
            if (DtAddress.Rows.Count > 0)
            {
                CompanyAddress = DtAddress.Rows[0]["Address"].ToString();
            }
            RptShift.SetImage(Imageurl);
            RptShift.setTitleName("In Out Report" + " From " + FromDate.ToString(objSys.SetDateFormat()) + " To " + ToDate.ToString(objSys.SetDateFormat()));
            RptShift.setcompanyname(CompanyName);
            RptShift.setaddress(CompanyAddress);


            RptShift.DataSource = dtFilter;
            RptShift.DataMember = "sp_Att_AttendanceRegister_Report";
            rptViewer.Report = RptShift;
            rptToolBar.ReportViewer = rptViewer;
            


        }

       
    }
}