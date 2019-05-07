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

public partial class HR_Report_EmpDirectoryReport : System.Web.UI.Page
{
    EmployeeDirectoryReport objReport = new EmployeeDirectoryReport();
    EmployeeMaster objemp = new EmployeeMaster();
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
        dt.Columns.Add("Directory_Name");
        dt.Columns.Add("Directory_Created_Date");
        dt.Columns.Add("Document_Name");

        dt.Columns.Add("File_Name");
        dt.Columns.Add("File_Upload_Date");
        dt.Columns.Add("File_Expiry_Date");


        string Id = (string)Session["Querystring"];
        string Document = (string)Session["Document"];
        //int ClaimType = (int)Session["ClaimType"];
        foreach (string str in Id.Split(','))
        {
            if (str != "")
            {
                DataTable dtClaimRecod = (DataTable)Session["ClaimRecord"];

              
                    if (Document.Trim() == "Document")
                    {
                        dtClaimRecod = new DataView(dtClaimRecod, "Directory_Name='" + str.Trim() + "' and File_Expiry_Date<'" + DateTime.Now.ToString() + "' ", "", DataViewRowState.CurrentRows).ToTable();

                    }
                    else
                    {

                        dtClaimRecod = new DataView(dtClaimRecod, "Directory_Name='" + str.Trim() + "'", "", DataViewRowState.CurrentRows).ToTable();
                    }
                
                if (dtClaimRecod.Rows.Count > 0)
                {
                    
                    for (int i = 0; i < dtClaimRecod.Rows.Count; i++)
                    {
                        string EmployeeName = GetEmployeeName(str);

                        //int salary = 10000;
                        //double Claimamount = 0;
                        //if (dtClaimRecod.Rows[i]["Value_Type"].ToString() == "2")
                        //{
                        //    int value = Convert.ToInt32(dtClaimRecod.Rows[i]["Value"]);
                        //    Claimamount = salary * value / 100;
                        //}
                        //else
                        //{
                        //    Claimamount = Convert.ToDouble(dtClaimRecod.Rows[i]["Value"].ToString());
                        //}

                        //int Month = Convert.ToInt32(dtClaimRecod.Rows[i]["Penalty_Month"]);

                        DataRow dr = dt.NewRow();
                        dr[0] = str.ToString();
                        dr[1] = EmployeeName;
                        dr[2] = dtClaimRecod.Rows[i]["Directory_Name"].ToString();
                        dr[3] = dtClaimRecod.Rows[i]["CreatedDate"].ToString();
                        dr[4] = dtClaimRecod.Rows[i]["Document_Name"].ToString();
                        dr[5] = dtClaimRecod.Rows[i]["File_Name"].ToString();
                        dr[6] = dtClaimRecod.Rows[i]["File_Upload_Date"].ToString();
                        dr[7] = dtClaimRecod.Rows[i]["File_Expiry_Date"].ToString();

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
        if (Document == "Document")
        {
            objReport.setTitleName("Document Expiry Report");
        }
        else
        {
            objReport.setTitleName("Employee Directory Report");
        }
        objReport.setcompanyname(CompanyName);
        objReport.setaddress(CompanyAddress);


        objReport.DataSource = dt;
        objReport.DataMember = "EmpDirectory";
        ReportViewer1.Report = objReport;
        ReportToolbar1.ReportViewer = ReportViewer1;


       




    }
    public string GetEmployeeName(object empid)
    {

        string empname = string.Empty;

        DataTable dt = objemp.GetEmployeeMasterAllData(Session["CompId"].ToString());
        dt = new DataView(dt, "Emp_Id='" + empid.ToString().Trim() + "'", "", DataViewRowState.CurrentRows).ToTable();

        if (dt.Rows.Count > 0)
        {

            empname = dt.Rows[0]["Emp_Name"].ToString();

            if (empname == "")
            {
                empname = "No Name";
            }

        }
        else
        {
            empname = "No Name";

        }

        return empname;



    }


}
