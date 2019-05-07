using System;
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

public partial class AttendanceReport_EmployeeReport : System.Web.UI.Page
{

    Att_EmployeeReport RptShift = new Att_EmployeeReport();

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
        if (Session["EmpList"] == null)
        {
            Response.Redirect("../Attendance_Report/AttendanceReport.aspx");
        }
        else
        {
           
            Emplist = Session["EmpList"].ToString();

            DataTable dtFilter = new DataTable();

            AttendanceDataSet rptdata = new AttendanceDataSet();

            rptdata.EnforceConstraints = false;
            AttendanceDataSetTableAdapters.sp_Set_EmployeeInformation_ReportTableAdapter adp = new AttendanceDataSetTableAdapters.sp_Set_EmployeeInformation_ReportTableAdapter();

            adp.Fill(rptdata.sp_Set_EmployeeInformation_Report,int.Parse(Session["CompId"].ToString()));



            if (Emplist != "")
            {
                dtFilter = new DataView(rptdata.sp_Set_EmployeeInformation_Report, "Emp_Id in (" + Emplist.Substring(0, Emplist.Length - 1) + ") ", "", DataViewRowState.CurrentRows).ToTable();
            }

            for (int i = 0; i < dtFilter.Rows.Count;i++ )
            {
                if (dtFilter.Rows[i]["Template3"].ToString() != "")
                {
                    dtFilter.Rows[i]["Template3"] = "True";
                }
                else
                {
                    dtFilter.Rows[i]["Template3"] = "False";
                }
                if (dtFilter.Rows[i]["Template4"].ToString() != "")
                {
                    dtFilter.Rows[i]["Template4"] = "True";
                }
                else
                {
                    dtFilter.Rows[i]["Template4"] = "False";
                }

            }


            if (ddlReportTrOrFal.SelectedIndex == 0 && ddlFaceReportTrOrFal.SelectedIndex == 0)
            {

            }
            else if(ddlReportTrOrFal.SelectedIndex == 1 && ddlFaceReportTrOrFal.SelectedIndex == 0)
                {
                    dtFilter = new DataView(dtFilter, "Template3='True'", "", DataViewRowState.CurrentRows).ToTable();
                }
            
            else if(ddlReportTrOrFal.SelectedIndex == 2 && ddlFaceReportTrOrFal.SelectedIndex == 0)
                {
                    dtFilter = new DataView(dtFilter, "Template3='False'", "", DataViewRowState.CurrentRows).ToTable();
                }
           
             else if(ddlReportTrOrFal.SelectedIndex == 0 && ddlFaceReportTrOrFal.SelectedIndex == 1)
                {
                    dtFilter = new DataView(dtFilter, "Template4='True'", "", DataViewRowState.CurrentRows).ToTable();
                }
            
            else if(ddlReportTrOrFal.SelectedIndex == 0 && ddlFaceReportTrOrFal.SelectedIndex == 2)
                {
                    dtFilter = new DataView(dtFilter, "Template4='False'", "", DataViewRowState.CurrentRows).ToTable();
                }

            else if (ddlReportTrOrFal.SelectedIndex == 1 && ddlFaceReportTrOrFal.SelectedIndex == 1)
            {
                dtFilter = new DataView(dtFilter, "Template3='True' and Template4='True'", "", DataViewRowState.CurrentRows).ToTable();
            }

            else if (ddlReportTrOrFal.SelectedIndex ==2 && ddlFaceReportTrOrFal.SelectedIndex == 2)
            {
                dtFilter = new DataView(dtFilter, "Template3='False' and  Template4='False'", "", DataViewRowState.CurrentRows).ToTable();
            }
            else if (ddlReportTrOrFal.SelectedIndex == 1 && ddlFaceReportTrOrFal.SelectedIndex == 2)
            {
                dtFilter = new DataView(dtFilter, "Template3='True' and  Template4='False'", "", DataViewRowState.CurrentRows).ToTable();
            }
            else if (ddlReportTrOrFal.SelectedIndex == 2 && ddlFaceReportTrOrFal.SelectedIndex == 1)
            {
                dtFilter = new DataView(dtFilter, "Template3='False' and  Template4='True'", "", DataViewRowState.CurrentRows).ToTable();
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
            RptShift.setTitleName("Employee Report");
            RptShift.setcompanyname(CompanyName);
            RptShift.setaddress(CompanyAddress);


            RptShift.DataSource = dtFilter;
            RptShift.DataMember = "sp_Set_EmployeeInformation_Report";
            rptViewer.Report = RptShift;
            rptToolBar.ReportViewer = rptViewer;
            


        }

       
    }

    protected void ddlReportTrOrFal_SelectedIndexChanged(object sender, EventArgs e)
    {

        GetReport();


    }
    protected void ddlFaceReportTrOrFal_SelectedIndexChanged(object sender, EventArgs e)
    {

        GetReport();


    }
}
