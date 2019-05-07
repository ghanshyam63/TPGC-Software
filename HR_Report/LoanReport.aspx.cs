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

public partial class Reports_LoanReport : System.Web.UI.Page
{
    EmployeeLoanReport Objreport = new EmployeeLoanReport();
    CompanyMaster Objcompany = new CompanyMaster();
    Set_AddressChild Objaddress = new Set_AddressChild();
    protected void Page_Load(object sender, EventArgs e)
    {


        GetReport();
    }
    void GetReport()
    {
        DataTable dt = new DataTable();
        dt.Columns.Add("EmpId");
        dt.Columns.Add("EmpName");
        dt.Columns.Add("Month");
        dt.Columns.Add("Loan_Name");
        dt.Columns.Add("Loan_Amount");
        dt.Columns.Add("Loan_Duration");
        dt.Columns.Add("Loan_Interest");
        dt.Columns.Add("Gross_Amount");
        dt.Columns.Add("Company_Name");
        dt.Columns.Add("Year");
        dt.Columns.Add("Monthly_Installmet");
        dt.Columns.Add("Loan_Approval_Date");


        string Id = (string)Session["Querystring"];
        
        foreach (string str in Id.Split(','))
        {
            if (str != "")
            {
                DataTable dtClaimRecord = (DataTable)Session["ClaimRecord"];
                dtClaimRecord = new DataView(dtClaimRecord, "Emp_Id=" + str + "", "", DataViewRowState.CurrentRows).ToTable();
                if (dtClaimRecord.Rows.Count > 0)
                {
                    string[] Montharr = new string[12];


                    for (int i = 0; i < dtClaimRecord.Rows.Count; i++)
                    {

                        

                       
                        DataRow dr = dt.NewRow();
                        dr[0] = dtClaimRecord.Rows[i]["Emp_Id"].ToString();
                        dr[1] = dtClaimRecord.Rows[i]["Emp_Name"].ToString();
                        dr[2] = (string)Session["Month"];
                        dr[3] = dtClaimRecord.Rows[i]["Loan_Name"].ToString();
                        dr[4] = dtClaimRecord.Rows[i]["Loan_Amount"].ToString();
                        dr[5] = dtClaimRecord.Rows[i]["Loan_Duration"].ToString();
                        dr[6] = dtClaimRecord.Rows[i]["Loan_Interest"].ToString();
                        dr[7] = dtClaimRecord.Rows[i]["Gross_Amount"].ToString();
                        dr[8] = "Pegasus Limited";
                        dr[9] = (string)Session["Year"];
                        dr[10] = dtClaimRecord.Rows[i]["Monthly_Installment"].ToString();
                        dr[11]=dtClaimRecord.Rows[i]["Loan_Approval_Date"].ToString();
                        
                        dt.Rows.Add(dr);
                    }
                }



            }
        }
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
        Objreport.SetImage(Imageurl);
        //if (ClaimType == 1)
        //{
        //    Objreport.setTitleName("Claim Approved Report");
        //}
        //if (ClaimType == 2)
        //{
        //    Objreport.setTitleName("Claim Cancelled Report");
        //}
        //if (ClaimType == 3)
        //{
        //    Objreport.setTitleName("Claim Pending Report");
        //}
        Objreport.setTitleName("Loan Approved Report");
        Objreport.setcompanyname(CompanyName);
        Objreport.setaddress(CompanyAddress);

        Objreport.DataSource = dt;
        Objreport.DataMember = "EmpLoan";
        ReportViewer1.Report = Objreport;
        ReportToolbar1.ReportViewer = ReportViewer1;





    }

}
