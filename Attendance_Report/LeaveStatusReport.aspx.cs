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

public partial class AttendanceReport_LeaveStatusReport : System.Web.UI.Page
{

    Att_LeaveStatusReport RptShift = new Att_LeaveStatusReport();
    SystemParameter ObjSys = new SystemParameter();
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
            FromDate = ObjSys.getDateForInput(Session["FromDate"].ToString());
            ToDate = ObjSys.getDateForInput(Session["ToDate"].ToString());

            Emplist = Session["EmpList"].ToString();

            DataTable dtFilter = new DataTable();

            AttendanceDataSet rptdata = new AttendanceDataSet();

            rptdata.EnforceConstraints = false;
            AttendanceDataSetTableAdapters.sp_Att_Leave_Request_ReportTableAdapter adp = new AttendanceDataSetTableAdapters.sp_Att_Leave_Request_ReportTableAdapter();

            adp.Fill(rptdata.sp_Att_Leave_Request_Report,int.Parse(Session["CompId"].ToString()), Convert.ToDateTime(FromDate), Convert.ToDateTime(ToDate));



            if (Emplist != "")
            {
                dtFilter = new DataView(rptdata.sp_Att_Leave_Request_Report, "Emp_Id in (" + Emplist.Substring(0, Emplist.Length - 1) + ") ", "", DataViewRowState.CurrentRows).ToTable();
            }


            for (int k = 0; k < dtFilter.Rows.Count; k++)
            {
                if (Convert.ToBoolean(dtFilter.Rows[k]["Is_Pending"].ToString()))
                {
                    dtFilter.Rows[k]["Field2"] = "Pending";
                    
                }
                else if (Convert.ToBoolean(dtFilter.Rows[k]["Is_Approved"].ToString()))
                {
                    dtFilter.Rows[k]["Field2"] = "Approved";
                   


                }
                else if (Convert.ToBoolean(dtFilter.Rows[k]["Is_Canceled"].ToString()))
                {
                    dtFilter.Rows[k]["Field2"] = "Canceled";
                   

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
            RptShift.setTitleName("Leave Status Report" + " From " + FromDate.ToString(ObjSys.SetDateFormat()) + " To " + ToDate.ToString(ObjSys.SetDateFormat()));
            RptShift.setcompanyname(CompanyName);
            RptShift.setaddress(CompanyAddress);


            RptShift.DataSource = dtFilter;
            RptShift.DataMember = "sp_Att_Leave_Request_Report";
            rptViewer.Report = RptShift;
            rptToolBar.ReportViewer = rptViewer;
            


        }

       
    }
}
