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

public partial class TempReport_Employee_DuePayement : System.Web.UI.Page
{
    //XtraReport1 objxxtrarepot = new XtraReport1();


    Document_Master ObjDoc = new Document_Master();
    CompanyMaster Objcompany = new CompanyMaster();
    Set_AddressChild Objaddress = new Set_AddressChild();
    Pay_Employee_DuePayement objEmpDuePayReport = new Pay_Employee_DuePayement();
   // Pay_Employee_Due_Payment objduepayemp = new Pay_Employee_Due_Payment();
   
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
        DataTable dtDuePay = (DataTable)Session["dtFalseRecords"];
        
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
        objEmpDuePayReport.setcompanyAddress(CompanyAddress);
        objEmpDuePayReport.setcompanyname(CompanyName);
        objEmpDuePayReport.setmonth("Month");
        objEmpDuePayReport.setyear("Year");
        objEmpDuePayReport.setimage(Imageurl);
        objEmpDuePayReport.settitle("Non Adjusted Report");
        objEmpDuePayReport.DataSource = dtDuePay;
        objEmpDuePayReport.DataMember = "dtEmpDuePayment";
        ReportViewer1.Report = objEmpDuePayReport;
        ReportToolbar1.ReportViewer = ReportViewer1;


    }

}
