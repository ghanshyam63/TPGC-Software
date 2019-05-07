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

public partial class HR_EmployeeClaimRequest : System.Web.UI.Page
{
    Pay_Employee_claim ObjClaim = new Pay_Employee_claim();
    EmployeeMaster ObjEmp = new EmployeeMaster();

    string strCompId = "";
    protected void Page_Load(object sender, EventArgs e)
    {
        strCompId = Session["CompId"].ToString();

        if (!IsPostBack)
        {
            ddlMonth.SelectedValue = DateTime.Now.Month.ToString();
            TxtYear.Text = DateTime.Now.Year.ToString();
        }
        TxtClaimName.Focus();

    }
    public void DisplayMessage(string str)
    {

        ScriptManager.RegisterStartupScript(this, GetType(), "", "alert('" + str + "');", true);

    }
    void Reset()
    {
        TxtClaimName.Text = "";
        TxtClaimDiscription.Text = "";
        ddlMonth.SelectedIndex = 0;
        txtCalValue.Text = "";
        DdlValueType.SelectedIndex = 0;
        TxtYear.Text = "";
        TxtClaimName.Focus();
        TxtYear.Text = DateTime.Now.Year.ToString();
        int CurrentMonth = Convert.ToInt32(DateTime.Now.Month.ToString());
        TxtEmployeeId.Text = "";

        ddlMonth.SelectedValue = (CurrentMonth).ToString();
        HidEmpId.Value = "";
    }
    protected void btnSaveLeave_Click(object sender, EventArgs e)
    {
        int b = 0;

        if (TxtClaimName.Text == "")
        {
            DisplayMessage("Enter Claim Name");
            TxtClaimName.Focus();
            return;
        }
        if (DdlValueType.SelectedIndex == 0)
        {
            DisplayMessage("Select Value Type");
            DdlValueType.Focus();
            return;

        }
        if (txtCalValue.Text == "")
        {
            DisplayMessage("Enter Value");
            txtCalValue.Focus();
            return;
        }
        if (ddlMonth.SelectedIndex == 0)
        {
            DisplayMessage("Select Month");
            ddlMonth.Focus();
            return;
        }
        if (TxtYear.Text == "")
        {
            DisplayMessage("Enter Year");
            TxtYear.Focus();
            return;
        }
        b = ObjClaim.Insert_In_Pay_Employee_ClaimRequest(Session["CompId"].ToString(), HidEmpId.Value, TxtClaimName.Text.Trim(), TxtClaimDiscription.Text, DdlValueType.SelectedValue, txtCalValue.Text, DateTime.Now.ToString(), "Pending", DateTime.Now.ToString(), ddlMonth.SelectedValue, TxtYear.Text, "", "", "", "", "", true.ToString(), DateTime.Now.ToString(), true.ToString(), Session["UserId"].ToString(), DateTime.Now.ToString(), Session["UserId"].ToString(), DateTime.Now.ToString());

        if (b != 0)
        {
            DisplayMessage("Record Saved");
            Reset();
        }


    }
    protected void btnReset_Click(object sender, EventArgs e)
    {
        Reset();
    }
    protected void btnCancel_Click(object sender, EventArgs e)
    {
        Reset();
    }
    [System.Web.Services.WebMethodAttribute(), System.Web.Script.Services.ScriptMethodAttribute()]
    public static string[] GetClaimName(string prefixText, int count, string contextKey)
    {
        DataTable dt = Pay_Employee_claim.GetClaimName("", HttpContext.Current.Session["CompId"].ToString());

        dt = new DataView(dt, "Claim_Name lIKE '" + prefixText + "%'", "", DataViewRowState.CurrentRows).ToTable();


        string[] str = new string[dt.Rows.Count];
        for (int i = 0; i < dt.Rows.Count; i++)
        {
            str[i] = dt.Rows[i][0].ToString();
        }
        return str;
    }
    protected void TxtEmployeeId_TextChanged(object sender, EventArgs e)
    {
        string empid = string.Empty;
        if (TxtEmployeeId.Text != "")
        {
            empid = TxtEmployeeId.Text.Split('/')[TxtEmployeeId.Text.Split('/').Length - 1];

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
                TxtEmployeeId.Text = "";
                TxtEmployeeId.Focus();
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
}
