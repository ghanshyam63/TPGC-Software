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

public partial class TempReport_Employee_Allowance_Report : System.Web.UI.Page
{
   // XtraReport1 objxxtrarepot = new XtraReport1();
    Allowance_Report objAlowaReport = new Allowance_Report();
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
        DataTable dtEmpAllow = (DataTable)Session["dtRecordEmp"];
        

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
        objAlowaReport.setcompanyAddress(CompanyAddress);
        objAlowaReport.setcompanyname(CompanyName);
        objAlowaReport.setmonth("Month");
        objAlowaReport.setyear("Year");
        objAlowaReport.setimage(Imageurl);
        objAlowaReport.settitle("Employee Allowance Report");
        objAlowaReport.DataSource = dtEmpAllow;
        objAlowaReport.DataMember = "dtEmpAllowance";
        ReportViewer1.Report = objAlowaReport;
        ReportToolbar1.ReportViewer = ReportViewer1;



    }
}
