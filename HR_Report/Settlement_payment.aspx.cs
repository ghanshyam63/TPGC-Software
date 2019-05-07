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

public partial class Temp_Report_Settlement_payment : System.Web.UI.Page
{

    XtraReport1 objxxtrarepot = new XtraReport1();

    Document_Master ObjDoc = new Document_Master();
    CompanyMaster Objcompany = new CompanyMaster();
    Set_AddressChild Objaddress = new Set_AddressChild();

    Pay_Employee_Due_Payment objduepayemp = new Pay_Employee_Due_Payment();
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
        DataTable dtpaydueemp = (DataTable)Session["dtrecords"];
       // dtpaydueemp.Columns.Add("EmpId");
       // dtpaydueemp.Columns.Add(Session["empname"].ToString());
       // dtpaydueemp.Columns.Add("Amount");
       // dtpaydueemp.Columns.Add("Description");
       // dtpaydueemp.Columns.Add("Month");
       // dtpaydueemp.Columns.Add("Year");
       // dtpaydueemp.Columns.Add("Type");

       // DataTable dtempduetemp = new DataTable();
       // dtempduetemp = objduepayemp.GetAllRecord_ByEmpId(Session["EmpIdc"].ToString());
       // if (dtempduetemp.Rows.Count > 0)
       // {
       //     for (int i = 0; i < dtempduetemp.Rows.Count; i++)
       //     {
       //         DataRow dr = dtpaydueemp.NewRow();
       //         dr[0] = dtempduetemp.Rows[i]["Emp_Id"].ToString();
       //         dr[1] = dtempduetemp.Rows[i]["Amount"].ToString();
       //         dr[2] = dtempduetemp.Rows[i]["Description"].ToString();
       //         dr[3] = dtempduetemp.Rows[i]["Month"].ToString();
       //         dr[4] = dtempduetemp.Rows[i]["Year"].ToString();
       //         dr[5] = dtempduetemp.Rows[i]["Type"].ToString();
                
       //         dtempduetemp.Rows.Add(dr);
       //     }
       // }

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
        objxxtrarepot.setcompanyAddress(CompanyAddress);
        objxxtrarepot.setcompanyname(CompanyName);
        objxxtrarepot.setmonth("Month");
        objxxtrarepot.setyear("Year");
        objxxtrarepot.setimage(Imageurl);
        objxxtrarepot.settitle("Adjustment Report");        
        objxxtrarepot.DataSource = dtpaydueemp;
        objxxtrarepot.DataMember = "settlementdt";
        ReportViewer1.Report = objxxtrarepot;
        ReportToolbar1.ReportViewer = ReportViewer1;
      
      

    }


}
