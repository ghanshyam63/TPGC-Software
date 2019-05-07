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

public partial class HR_EmployeeLoanRequest : System.Web.UI.Page
{
    Pay_Employee_Loan ObjLoan = new Pay_Employee_Loan();
    EmployeeMaster ObjEmp = new EmployeeMaster();
    string strCompId = string.Empty;
    protected void Page_Load(object sender, EventArgs e)
    {
        Session["AccordianId"] = "19";
        Session["HeaderText"] = "HR";

        strCompId = Session["CompId"].ToString();
        if (!IsPostBack)
        {
            
            txtEmpName.Focus();
            }

    }
    public void DisplayMessage(string str)
    {

        ScriptManager.RegisterStartupScript(this, GetType(), "", "alert('" + str + "');", true);


    }
    protected void BtnSave_Click(object sender, EventArgs e)
    {
        if (txtEmpName.Text == "")
        {
            DisplayMessage("Enter Employee Name");
            txtEmpName.Focus();
            return;
        }
        if (txtLoanName.Text == "")
        {
            DisplayMessage("Enter Loan Name");
            txtLoanName.Focus();
            return;
        }
        if (txtLoanAmount.Text == "")
        {
            DisplayMessage("Enter Loan amount");
            txtLoanAmount.Focus();
            return;
        }
        int CheckInsertion = 0;
        CheckInsertion = ObjLoan.Insert_In_Pay_Employee_Loan(strCompId, HidEmpId.Value, txtLoanName.Text, DateTime.Now.ToString(), txtLoanAmount.Text, "Pending", "", "", "", "", "", true.ToString(), DateTime.Now.ToString(), true.ToString(), Session["UserId"].ToString(), DateTime.Now.ToString(), Session["UserId"].ToString(), DateTime.Now.ToString());
        if (CheckInsertion != 0)
        {
            DisplayMessage("Record Saved");
          
            txtEmpName.Focus();
            Reset();
        }
        else
        {
            DisplayMessage("Record Not Saved");
        }
    }
    void Reset()
    {
        txtEmpName.Text = "";
        txtLoanName.Text = "";
        txtLoanAmount.Text = "";
        txtEmpName.Focus();
        HidEmpId.Value = "";
    }
    protected void TxtEmpName_TextChanged(object sender, EventArgs e)
    {
        string empid = string.Empty;
        if (txtEmpName.Text != "")
        {
            empid = txtEmpName.Text.Split('/')[txtEmpName.Text.Split('/').Length - 1];

            DataTable dtEmp = ObjEmp.GetEmployeeMaster(strCompId);

            dtEmp = new DataView(dtEmp, "Emp_Code='" + empid + "'", "", DataViewRowState.CurrentRows).ToTable();

            if (dtEmp.Rows.Count > 0)
            {
                empid = dtEmp.Rows[0]["Emp_Id"].ToString();
                HidEmpId.Value = empid;
            }
            else
            {
                DisplayMessage("Employee Not Exists");
                txtEmpName.Text = "";
                txtEmpName.Focus();
                HidEmpId.Value = "";
                return;
            }
        }
    }
    [System.Web.Services.WebMethodAttribute(), System.Web.Script.Services.ScriptMethodAttribute()]
    public static string[] GetCompletionListEmployeeName(string prefixText, int count, string contextKey)
    {
        DataTable dt = Common.GetEmployee(prefixText, HttpContext.Current.Session["CompId"].ToString());

        string[] str = new string[dt.Rows.Count];
        for (int i = 0; i < dt.Rows.Count; i++)
        {
            str[i] = "" + dt.Rows[i][1].ToString() + "/(" + dt.Rows[i][2].ToString() + ")/" + dt.Rows[i][0].ToString() + "";
        }
        return str;
    }
    [System.Web.Services.WebMethodAttribute(), System.Web.Script.Services.ScriptMethodAttribute()]
    public static string[] GetLoanName(string prefixText, int count, string contextKey)
    {
        DataTable dt = Pay_Employee_Loan.GetLoanName("", HttpContext.Current.Session["CompId"].ToString());

        dt = new DataView(dt, "Loan_Name lIKE '" + prefixText + "%'", "", DataViewRowState.CurrentRows).ToTable();


        string[] str = new string[dt.Rows.Count];
        for (int i = 0; i < dt.Rows.Count; i++)
        {
            str[i] = dt.Rows[i][0].ToString();
        }
        return str;
    }
    protected void BtnReset_Click(object sender, EventArgs e)
    {
        Reset();
    }
    protected void BtnCancel_Click(object sender, EventArgs e)
    {
        Reset();
    }
}
