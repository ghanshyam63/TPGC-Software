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

public partial class AttendanceReport_HolidayReport : System.Web.UI.Page
{

    Att_HolidayReport RptShift = new Att_HolidayReport();
    SystemParameter objsys = new SystemParameter();
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
            FromDate = objsys.getDateForInput(Session["FromDate"].ToString());
            ToDate = objsys.getDateForInput(Session["ToDate"].ToString());

            Emplist = Session["EmpList"].ToString();

            DataTable dtFilter = new DataTable();

            AttendanceDataSet rptdata = new AttendanceDataSet();

            rptdata.EnforceConstraints = false;
            AttendanceDataSetTableAdapters.sp_Set_Employee_Holiday_ReportTableAdapter adp = new AttendanceDataSetTableAdapters.sp_Set_Employee_Holiday_ReportTableAdapter();

            adp.Fill(rptdata.sp_Set_Employee_Holiday_Report,int.Parse(Session["CompId"].ToString()), Convert.ToDateTime(FromDate), Convert.ToDateTime(ToDate));



            if (Emplist != "")
            {
                dtFilter = new DataView(rptdata.sp_Set_Employee_Holiday_Report, "Emp_Id in (" + Emplist.Substring(0, Emplist.Length - 1) + ") ", "", DataViewRowState.CurrentRows).ToTable();
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
            RptShift.setTitleName("Holiday Report" + " From " + FromDate.ToString(objsys.SetDateFormat()) + " To " + ToDate.ToString(objsys.SetDateFormat()));
            RptShift.setcompanyname(CompanyName);
            RptShift.setaddress(CompanyAddress);


            RptShift.DataSource = dtFilter;
            RptShift.DataMember = "sp_Set_Employee_Holiday_Report";
            rptViewer.Report = RptShift;
            rptToolBar.ReportViewer = rptViewer;
            


        }

       
    }
}
