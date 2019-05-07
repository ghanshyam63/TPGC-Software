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

public partial class HR_Report_PenaltyReport : System.Web.UI.Page
{
    EmployeePenaltyReport objReport = new EmployeePenaltyReport();
    CompanyMaster Objcompany = new CompanyMaster();
    Set_AddressChild Objaddress = new Set_AddressChild();
    EmployeeParameter ObjSalary = new EmployeeParameter();
    protected void Page_Load(object sender, EventArgs e)
    {
        GetReport();
    }
    void GetReport()
    {
        DataTable dt = new DataTable();
        dt.Columns.Add("EmpId");
        dt.Columns.Add("EmpName");
        dt.Columns.Add("Penalty_Name");
        dt.Columns.Add("Penalty_Value");
        dt.Columns.Add("Penalty_Date");
       
        dt.Columns.Add("Month");
        dt.Columns.Add("Year");


        string Id = (string)Session["Querystring"];
        //int ClaimType = (int)Session["ClaimType"];
        double Penaltysum = 0;
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
                    double salary = 0;
                    for (int i = 0; i < dtClaimRecod.Rows.Count; i++)
                    {
                        if (dtsalary.Rows.Count > 0)
                        {

                             salary = Convert.ToDouble(dtsalary.Rows[0]["Basic_Salary"]);
                        }
                        double Claimamount = 0;
                        if (dtClaimRecod.Rows[i]["Value_Type"].ToString() == "2")
                        {
                            int value = Convert.ToInt32(dtClaimRecod.Rows[i]["Value"]);
                            Claimamount = salary * value / 100;
                        }
                        else
                        {
                            Claimamount = Convert.ToDouble(dtClaimRecod.Rows[i]["Value"].ToString());
                        }

                        int Month = Convert.ToInt32(dtClaimRecod.Rows[i]["Penalty_Month"]);

                        DataRow dr = dt.NewRow();
                        dr[0] = dtClaimRecod.Rows[i]["Emp_Id"].ToString();
                        dr[1] = dtClaimRecod.Rows[i]["Emp_Name"].ToString();
                        dr[2] = dtClaimRecod.Rows[i]["Penalty_Name"].ToString();
                        dr[3] = Claimamount.ToString("0.000");
                        dr[4] = dtClaimRecod.Rows[i]["Penalty_Date"].ToString();
                        dr[5] = Montharr[Month - 1].ToString();
                        dr[6] = dtClaimRecod.Rows[i]["Penalty_Year"].ToString();
                        Penaltysum += Claimamount;
                        
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
        objReport.SetImage(Imageurl);
        objReport.setTitleName("Employee Penalty Report");
        objReport.SetCompanyName(CompanyName);
        objReport.SetAddress(CompanyAddress);
        objReport.setSum(Penaltysum);
       

        objReport.DataSource = dt;
        objReport.DataMember = "EmpPenalty";
        ReportViewer1.Report = objReport;
        ReportToolbar1.ReportViewer = ReportViewer1;





    }

}
