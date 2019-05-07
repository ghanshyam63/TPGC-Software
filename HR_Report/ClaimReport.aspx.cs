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
using PegasusDataAccess;

public partial class Reports_ClaimReport : System.Web.UI.Page
{
    EmployeeClaimreport Objreport = new EmployeeClaimreport();
    CompanyMaster Objcompany = new CompanyMaster();
    Set_AddressChild Objaddress = new Set_AddressChild();
    Pay_Employee_claim objclaim = new Pay_Employee_claim();
    EmployeeParameter ObjSalary = new EmployeeParameter();
    string EmpId = string.Empty;
    protected void Page_Load(object sender, EventArgs e)
    {


        GetReport();
    }
    void GetReport()
    {
        DataTable dt = new DataTable();
        dt.Columns.Add("Company_Name");
        dt.Columns.Add("Image");
        dt.Columns.Add("Month");
        dt.Columns.Add("Year");
        dt.Columns.Add("Emp_Id");
        dt.Columns.Add("Emp_Name");
        dt.Columns.Add("Claim_Name");
        dt.Columns.Add("Claim_Amount");
        dt.Columns.Add("Sum_Claim");

        string Id = (string)Session["Querystring"];
        int ClaimType = (int)Session["ClaimType"];
        double SumClaim = 0;
        foreach (string str in Id.Split(','))
        {
            if (str != "")
            {
                DataTable dtClaimRecod = (DataTable)Session["ClaimRecord"];
                dtClaimRecod = new DataView(dtClaimRecod, "Emp_Id=" + str + "", "", DataViewRowState.CurrentRows).ToTable();
                if (dtClaimRecod.Rows.Count > 0)
                {
                    DataTable dtsalary = ObjSalary.GetEmployeeParameterByEmpId(str, Session["CompId"].ToString());
                    string[] Montharr = new string[12];


                    Montharr[0] = "January";
                    Montharr[1] = "February";
                    Montharr[2] = "March";
                    Montharr[3] = "April";
                    Montharr[4] = "May";
                    Montharr[5] = "June";
                    Montharr[6] = "July";
                    Montharr[7] = "August";
                    Montharr[8] = "September";
                    Montharr[9] = "October";
                    Montharr[10] = "November";
                    Montharr[11] = "December";
                    for (int i = 0; i < dtClaimRecod.Rows.Count; i++)
                    {
                        string salary = string.Empty;
                        if (dtsalary.Rows.Count > 0)
                        {

                            salary = dtsalary.Rows[0]["Basic_Salary"].ToString();
                        }
                        string Claimamount = string.Empty;
                        if (dtClaimRecod.Rows[i]["Value_Type"].ToString() == "2")
                        {
                            string value = dtClaimRecod.Rows[i]["Value"].ToString();
                            Claimamount = (float.Parse(salary) * float.Parse(value) / 100).ToString("0.000");
                        }
                        else
                        {
                            Claimamount =Convert.ToDouble(dtClaimRecod.Rows[i]["Value"]).ToString("0.000");
                        }

                        int Month = Convert.ToInt32(dtClaimRecod.Rows[i]["Claim_Month"]);

                        DataRow dr = dt.NewRow();
                        dr[0] = "";
                        dr[1] = "";
                        dr[2] = Montharr[Month - 1].ToString();
                        dr[3] = dtClaimRecod.Rows[i]["Claim_Year"].ToString();
                        dr[4] = dtClaimRecod.Rows[i]["Emp_Id"].ToString();
                        dr[5] = dtClaimRecod.Rows[i]["Emp_Name"].ToString();
                        dr[6] = dtClaimRecod.Rows[i]["Claim_Name"].ToString();
                        dr[7] = Claimamount;
                        SumClaim += double.Parse(Claimamount);

                        dr[8] = SumClaim.ToString("0.000");



                        //dr[7] = System.Math.Round(Claimamount, 3);
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
        Objreport.SetCompanyName(CompanyName);
        Objreport.SetAddress(CompanyAddress);
        Objreport.setSumClaimAmount(SumClaim);
        if (ClaimType == 1)
        {
            Objreport.setTitleName("Claim Approved Report");
        }
        if (ClaimType == 2)
        {
            Objreport.setTitleName("Claim Cancelled Report");
        }
        if (ClaimType == 3)
        {
            Objreport.setTitleName("Claim Pending Report");
        }

        Objreport.DataSource = dt;
        Objreport.DataMember = "EmployeeClaim";
        ReportViewer1.Report = Objreport;
        ReportToolbar1.ReportViewer = ReportViewer1;





    }
}
