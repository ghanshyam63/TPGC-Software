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

public partial class HR_Report_LoanDetailReport : System.Web.UI.Page
{
    EmployeeLoanDetailReport Objreport = new EmployeeLoanDetailReport();
    EmployeeLoanInstallmentReport ObjReportInstallmet = new EmployeeLoanInstallmentReport();
    CompanyMaster Objcompany = new CompanyMaster();
    Set_AddressChild Objaddress = new Set_AddressChild();
    protected void Page_Load(object sender, EventArgs e)
    {
        getReport();
    }
    void getReport()
    {

        DataTable dtClaimRecord = (DataTable)Session["ClaimRecord"];
        string LoanType = (string)Session["Querystring"];
        double sumPaidamount = 0;
        //dtClaimRecord.Columns.Add("Sum_PaidAmount");
        if (dtClaimRecord.Rows.Count > 0)
        {
            for (int i = 0; i < dtClaimRecord.Rows.Count; i++)
            {
                DataRow dr = dtClaimRecord.NewRow();

                sumPaidamount += Convert.ToDouble(dtClaimRecord.Rows[i]["PaidAmount"]);
                dr[13] = sumPaidamount;

            }
        }
        //dtClaimRecord.Columns.Add("Empid");
        //dtClaimRecord.Columns.Add("EmpName");
        //dtClaimRecord.Columns.Add("Loan_Name");
        //dtClaimRecord.Columns.Add("Installment");
        //dtClaimRecord.Columns.Add("PaidAmount");
        //dtClaimRecord.Columns.Add("Status");
        //dtClaimRecord.Columns.Add("Month");
        //dtClaimRecord.Columns.Add("Year");
        //dtClaimRecord.Columns.Add("Loan_Id");
        //dtClaimRecord.Columns.Add("Loan_Amount");
        //dtClaimRecord.Columns.Add("Loan_Duration");
        //dtClaimRecord.Columns.Add("Loan_Interest");
        //dtClaimRecord.Columns.Add("Gross_Amount");

        //for (int j = 0; j < dtClaimRecord.Rows.Count; j++)
        //{
        //    DataRow Tablerow = dtClaimRecord.NewRow();
        //    Tablerow[0] = dtClaimRecord.Rows[0]["Empid"].ToString();
        //    Tablerow[1] = dtClaimRecord.Rows[0]["EmpName"].ToString();
        //    Tablerow[2] = dtClaimRecord.Rows[0]["Loan_Name"].ToString();
        //    Tablerow[3] = dtClaimRecord.Rows[0]["Installment"].ToString();
        //    Tablerow[4] = dtClaimRecord.Rows[0]["PaidAmount"].ToString();
        //    Tablerow[5] = dtClaimRecord.Rows[0]["Status"].ToString();
        //    Tablerow[6] = dtClaimRecord.Rows[0]["Month"].ToString();
        //    Tablerow[7] = dtClaimRecord.Rows[0]["Year"].ToString();
        //    Tablerow[8] = dtClaimRecord.Rows[0]["Loan_Id"].ToString();
        //    Tablerow[9] = dtClaimRecord.Rows[0]["Loan_Amount"].ToString();
        //    Tablerow[10] = dtClaimRecord.Rows[0]["Loan_Duration"].ToString();
        //    Tablerow[11] = dtClaimRecord.Rows[0]["Loan_Interest"].ToString();
        //    Tablerow[12] = dtClaimRecord.Rows[0]["Gross_Amount"].ToString();


        //dtClaimRecord.Rows.Add(Tablerow);
        //}
        string CompanyName = "";
        string CompanyAddress = "";
        string Imageurl = "";
        Objreport.setsum(sumPaidamount);
       
        DataTable DtCompany = Objcompany.GetCompanyMasterById(Session["CompId"].ToString());
        DataTable DtAddress = Objaddress.GetAddressChildDataByAddTypeAndAddRefId("Company", Session["CompId"].ToString());
        if (DtCompany.Rows.Count > 0)
        {
            CompanyName = DtCompany.Rows[0]["Company_Name"].ToString();
            Imageurl ="~/CompanyResource/"+Session["CompId"].ToString()+"/"+DtCompany.Rows[0]["Logo_Path"].ToString();


        }
        if (DtAddress.Rows.Count > 0)
        {
            CompanyAddress = DtAddress.Rows[0]["Address"].ToString();
        }
        if (LoanType == "1")
        {

            Objreport.DataSource = dtClaimRecord;
            Objreport.DataMember = "LoanDetail";
            ReportViewer1.Report = Objreport;
            ReportToolbar1.ReportViewer = ReportViewer1;
            Objreport.SetImage(Imageurl);
            Objreport.setTitleName("Loan Detail Report");
            Objreport.setaddress(CompanyAddress);
            Objreport.setcompanyname(CompanyName);
        }
        else
        {
            ObjReportInstallmet.DataSource = dtClaimRecord;
            ObjReportInstallmet.DataMember = "LoanDetail";
            ReportViewer1.Report = ObjReportInstallmet;
            ReportToolbar1.ReportViewer = ReportViewer1;
            ObjReportInstallmet.SetImage(Imageurl);
            ObjReportInstallmet.setTitleName("Monthly Installment Report");
            ObjReportInstallmet.setaddress(CompanyAddress);
            ObjReportInstallmet.setcompanyname(CompanyName);
        }
        


    }
}
