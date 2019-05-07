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

public partial class TempReport_Employee_Deduction_Report : System.Web.UI.Page
{

    //Allowance_Report objAlowaReport = new Allowance_Report();
    Deduction_Report objDeductionReport = new Deduction_Report();
    
    Document_Master ObjDoc = new Document_Master();
    CompanyMaster Objcompany = new CompanyMaster();
    Set_AddressChild Objaddress = new Set_AddressChild();
    protected void Page_Load(object sender, EventArgs e)
    {
        if (Session["CompId"].ToString() == null)
        {
            Response.Redirect("../TempReport/GenerateReportTemp.aspx");

        }
        getrecoerd();



    }

    public void getrecoerd()
    {
        DataTable dtEmpDeduc = (DataTable)Session["dtRecordDeduc"];


        string CompanyName = "";
        string CompanyAddress = "";
        string Imageurl = "";

        DataTable DtCompany = Objcompany.GetCompanyMasterById(Session["CompId"].ToString());
        DataTable DtAddress = Objaddress.GetAddressChildDataByAddTypeAndAddRefId("Company", Session["CompId"].ToString());
        if (DtCompany.Rows.Count > 0)
        {
            CompanyName = DtCompany.Rows[0]["Company_Name"].ToString();
            Imageurl = "~/CompanyResource/" + Session["CompId"].ToString() + "/" + DtCompany.Rows[0]["Logo_Path"].ToString();

        }
        if (DtAddress.Rows.Count > 0)
        {
            CompanyAddress = DtAddress.Rows[0]["Address"].ToString();
        }
        objDeductionReport.setcompanyAddress(CompanyAddress);
        objDeductionReport.setcompanyname(CompanyName);
        objDeductionReport.setmonth("Month");
        objDeductionReport.setyear("Year");
        objDeductionReport.setimage(Imageurl);
        objDeductionReport.settitle("Employee Deduction Report");
        objDeductionReport.DataSource = dtEmpDeduc;
        objDeductionReport.DataMember = "dtEmpDeduction";
        ReportViewer1.Report = objDeductionReport;
        ReportToolbar1.ReportViewer = ReportViewer1;



    }
}
