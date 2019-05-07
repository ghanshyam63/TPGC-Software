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

public partial class TempReport_Employee_PaySlip_Report : System.Web.UI.Page
{
    Employee_Pay_Slip_Report objEmpPayslip = new Employee_Pay_Slip_Report();
    Document_Master ObjDoc = new Document_Master();
    CompanyMaster Objcompany = new CompanyMaster();
    Set_AddressChild Objaddress = new Set_AddressChild();
    EmployeeMaster objEmpmaster = new EmployeeMaster();
   

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
        DataTable dtEmpsSlip = (DataTable)Session["dtEmpPaySlipRecords"];
        string CompanyName = "";
        string CompanyAddress = "";
        string Imageurl = "";
        string CompanyContact = "";
        string EmpImageurl = "";
        string Mailid = "";
        string Websitename = "";

        DataTable DtCompany = Objcompany.GetCompanyMasterById(Session["CompId"].ToString());
        DataTable DtAddress = Objaddress.GetAddressChildDataByAddTypeAndAddRefId("Company", Session["CompId"].ToString());
        DataTable dtEmpmaster=objEmpmaster.GetEmployeeMasterByEmpCode(Session["CompId"].ToString(),dtEmpsSlip.Rows[0]["EmpCode"].ToString());

        if (dtEmpmaster.Rows.Count > 0)
        {
            EmpImageurl = "~/CompanyResource/" + Session["CompId"].ToString() + "/" + dtEmpmaster.Rows[0]["Emp_Image"].ToString();

        }

        if (DtCompany.Rows.Count > 0)
        {
            CompanyName = DtCompany.Rows[0]["Company_Name"].ToString();
            Imageurl = "~/CompanyResource/" + Session["CompId"].ToString() + "/" + DtCompany.Rows[0]["Logo_Path"].ToString();  
       
        }
        if (DtAddress.Rows.Count > 0)
        {
            CompanyAddress = DtAddress.Rows[0]["Address"].ToString();
            CompanyContact = DtAddress.Rows[0]["PhoneNo1"].ToString();
            Mailid = DtAddress.Rows[0]["EmailId1"].ToString();
            Websitename = DtAddress.Rows[0]["WebSite"].ToString();
            
        }
        objEmpPayslip.setcompanyAddress(CompanyAddress);
        objEmpPayslip.setcompanyname(CompanyName);
        objEmpPayslip.setempimage(EmpImageurl);
        objEmpPayslip.setmailid(Mailid);
        objEmpPayslip.setwebsite(Websitename);
        //objEmpPayslip.setnetamount(Session["netsalary"].ToString());

        objEmpPayslip.setimage(Imageurl);
        objEmpPayslip.setcontact(CompanyContact);
        string Type = "";
        string attendrecord = "";
        DataTable dt = new DataTable();
        dt = dtEmpsSlip;
        DataTable dtrecord = new DataTable();
        dtrecord = dtEmpsSlip; 
        string amount = "";
          dt = new DataView(dt, "TypeAllow<>'"+amount.Trim() +"'", "", DataViewRowState.CurrentRows).ToTable();
            
            objEmpPayslip.DataSource = dt;
            objEmpPayslip.DataMember = "dtEmpPayslip";
      
            ReportViewer1.Report = objEmpPayslip;
     
            ReportToolbar1.ReportViewer = ReportViewer1;

    }
}
