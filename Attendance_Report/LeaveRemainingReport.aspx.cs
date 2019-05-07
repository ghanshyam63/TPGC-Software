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

public partial class AttendanceReport_LeaveRemainingReport : System.Web.UI.Page
{

    Att_LeaveRemainingReport RptShift = new Att_LeaveRemainingReport();
    Set_ApplicationParameter objAppParam = new Set_ApplicationParameter();
    CompanyMaster objComp=new CompanyMaster();
    Set_AddressChild ObjAddress=new Set_AddressChild ();
    SystemParameter ObjSys = new SystemParameter();
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
            AttendanceDataSetTableAdapters.sp_Att_Employee_Leave_Trans_ReportTableAdapter adp = new AttendanceDataSetTableAdapters.sp_Att_Employee_Leave_Trans_ReportTableAdapter();

            adp.Fill(rptdata.sp_Att_Employee_Leave_Trans_Report, int.Parse(Session["CompId"].ToString()));

            DateTime JoiningDate = new DateTime();
            int FinancialYearMonth = 0;
            if (Emplist != "")
            {
                dtFilter = new DataView(rptdata.sp_Att_Employee_Leave_Trans_Report, "Emp_Id in (" + Emplist.Substring(0, Emplist.Length - 1) + ") ", "Shedule_Type", DataViewRowState.CurrentRows).ToTable();
            }
            DataTable dt = objAppParam.GetApplicationParameterByParamName("FinancialYearStartMonth", Session["CompId"].ToString());

            if (dt.Rows.Count > 0)
            {
                FinancialYearMonth = int.Parse(dt.Rows[0]["Param_Value"].ToString());

            }
            DateTime FinancialYearStartDate = new DateTime();
            DateTime FinancialYearEndDate = new DateTime();
            if (JoiningDate.Month > FinancialYearMonth)
            {
                FinancialYearStartDate = new DateTime(DateTime.Now.Year, FinancialYearMonth, 1);
                FinancialYearEndDate = FinancialYearStartDate.AddYears(1).AddDays(-1);
            }
            else
            {

                FinancialYearStartDate = new DateTime(DateTime.Now.Year, FinancialYearMonth, 1);
                FinancialYearEndDate = FinancialYearStartDate.AddYears(1).AddDays(-1);
            }

            DataTable dtFilter1 = new DataTable();
            dtFilter1 = dtFilter;

            dtFilter1 = new DataView(dtFilter, "Year in('" + DateTime.Now.Year.ToString() + "','" + FinancialYearEndDate.Year.ToString() + "'  ) and Month='0'", "Shedule_Type", DataViewRowState.CurrentRows).ToTable();

            dtFilter = new DataView(dtFilter, "Year in('" + DateTime.Now.Year.ToString() + "','" + FinancialYearEndDate.Year.ToString()+ "'  ) ", "Shedule_Type", DataViewRowState.CurrentRows).ToTable();

            string Months = string.Empty;
            DateTime ftime=DateTime.Now;
            while (ftime<=FinancialYearEndDate)
            {
                Months += ftime.Month + ",";

                ftime = ftime.AddMonths(1);
            }

            dtFilter = new DataView(dtFilter,"Month in("+Months+")", "Shedule_Type desc", DataViewRowState.CurrentRows).ToTable();
            dtFilter.Merge(dtFilter1);

            dtFilter = new DataView(dtFilter,"","Shedule_Type desc", DataViewRowState.CurrentRows).ToTable();
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
            RptShift.setTitleName("Leave Remaining Report");
            RptShift.setcompanyname(CompanyName);
            RptShift.setaddress(CompanyAddress);


            RptShift.DataSource = dtFilter;
            RptShift.DataMember = "sp_Att_Employee_Leave_Trans_Report";
            rptViewer.Report = RptShift;
            rptToolBar.ReportViewer = rptViewer;
            


        }

       
    }
}
