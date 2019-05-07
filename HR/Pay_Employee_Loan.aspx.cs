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

public partial class Arca_Wing_Pay_Employee_Loan : System.Web.UI.Page
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
            GridBind();
            txtEmpName.Focus();
            btnLoan_Click(null, null);
        }
    }
    void GridBind()
    {
        DataTable Dt = new DataTable();
        Dt = ObjLoan.GetRecord_From_PayEmployeeLoan(strCompId, "0", "Pending");
        if (Dt.Rows.Count > 0)
        {
            GridViewLoan.DataSource = Dt;
            GridViewLoan.DataBind();
            Session["dtFilter"] = Dt;
        }
        else
        {
            DataTable Dtclear = new DataTable();
            GridViewLoan.DataSource = Dtclear;
            GridViewLoan.DataBind();
        }
    }
    protected void btList_Click(object sender, EventArgs e)
    {
        PanelList.BackColor = System.Drawing.ColorTranslator.FromHtml("#ccddee");
        pnlLeave.BackColor = System.Drawing.ColorTranslator.FromHtml("#90bde9");
        PanelSaveLoan.Visible = false;
        PanelUpdateLoan.Visible = true;
    }
    protected void btnLoan_Click(object sender, EventArgs e)
    {
        pnlLeave.BackColor = System.Drawing.ColorTranslator.FromHtml("#ccddee");

        PanelList.BackColor = System.Drawing.ColorTranslator.FromHtml("#90bde9");
        txtEmpName.Focus();
        PanelSaveLoan.Visible = true;
        PanelUpdateLoan.Visible = false;
        txtLoanAmount.Text = "";
        txtEmpName.Text = "";
        txtLoanName.Text = "";
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
            GridBind();
            txtEmpName.Focus();
            Reset();
        }
        else
        {
            DisplayMessage("Record Not Saved");
        }
    }
    public void DisplayMessage(string str)
    {

        ScriptManager.RegisterStartupScript(this, GetType(), "", "alert('" + str + "');", true);


    }
    protected void btnEdit_command(object sender, CommandEventArgs e)
    {
        pnlBackGroud.Visible = true;
        pnlLoanDetail.Visible = true;
        RbtnApproved.Focus();
        HiddeniD.Value = e.CommandArgument.ToString();
        DataTable Dt = new DataTable();
        Dt = ObjLoan.GetRecord_From_PayEmployeeLoan_usingLoanId(strCompId, HiddeniD.Value, "Pending");
        if (Dt.Rows.Count > 0)
        {
            txtPopUpEmployee.Text = Dt.Rows[0]["Emp_Name"].ToString();
            txtPopUpLoanName.Text = Dt.Rows[0]["Loan_Name"].ToString();
            txtPopupLoanAmount.Text = Dt.Rows[0]["Loan_Amount"].ToString();
        }
        RbtnApproved.Checked = false;
        RbtnCancelled.Checked = false;
        txtDuration.ReadOnly = true;
        txtInterest.ReadOnly = true;
    }
    protected void btnClosePanel_Click(object sender, EventArgs e)
    {
        pnlBackGroud.Visible = false;
        pnlLoanDetail.Visible = false;
    }
    protected void IbtnDelete_command(object sender, CommandEventArgs e)
    {
        HiddeniD.Value = e.CommandArgument.ToString();
        int CheckDeletion = 0;
        CheckDeletion = ObjLoan.DeleteRecord_in_Pay_Employee_Loan(Session["CompId"].ToString(), HiddeniD.Value, "Cancelled", Session["UserId"].ToString(), DateTime.Now.ToString());
        if (CheckDeletion != 0)
        {
            DisplayMessage("Record Deleted");
            DataTable dtGrid = new DataTable();
            GridBind();
        }
        else
        {
            DisplayMessage("Record Not Deleted");
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
    protected void btn_PopUp_Reset_Click(object sender, EventArgs e)
    {
        btnClosePanel_Click(null, null);
        ResetPanel();
    }
    protected void btn_PopUp_Save_Click(object sender, EventArgs e)
    {
        int UpdationCheck = 0;
        int checkedRadioClick = 0;
        if (RbtnApproved.Checked == true)
        {
            if (txtInterest.Text == "")
            {
                DisplayMessage("Enter Interest");
                txtInterest.Focus();
                return;
            }
            if (txtDuration.Text == "")
            {
                DisplayMessage("Enter Duration");
                txtDuration.Focus();
                return;
            }
            if (txtGrossAmount.Text == "")
            {
                DisplayMessage("Press Tab Key for GrossAmount");
                txtDuration.Focus();
                return;
            }


            checkedRadioClick = 1;
            UpdationCheck = ObjLoan.UpdateRecord_In_Pay_Employee_Loan(strCompId, HiddeniD.Value, DateTime.Now.ToString(), txtDuration.Text, txtInterest.Text, txtGrossAmount.Text, txtMonthlyInstallment.Text, "Running", Session["UserId"].ToString(), DateTime.Now.ToString());
            if (UpdationCheck != 0)
            {
                DisplayMessage("Record Saved");
                // Here we will write code for loan detail

                Double Intereset = Convert.ToDouble(txtInterest.Text);
                int Duration = Convert.ToInt32(txtDuration.Text);
                Double LoanInstallmentAmount = Convert.ToDouble(txtMonthlyInstallment.Text);

                int CurrentMonth = Convert.ToInt32(DateTime.Now.Month);
                int CurrentYear = Convert.ToInt32(DateTime.Now.Year);


                int i = 0;
                while (i < Duration)
                {
                    if (CurrentMonth == 12)
                    {
                        CurrentMonth = 1;
                        CurrentYear++;
                    }
                    else
                    {
                        CurrentMonth++;
                    }
                    int LoanDetailInsertion = 0;
                    LoanDetailInsertion = ObjLoan.Insert_In_Pay_Employee_LoanDetail(HiddeniD.Value, CurrentMonth.ToString(), CurrentYear.ToString(), "0", txtMonthlyInstallment.Text, txtMonthlyInstallment.Text, "0", "Pending", "", "", "", "", "", false.ToString(), DateTime.Now.ToString(), true.ToString(), Session["UserId"].ToString(), DateTime.Now.ToString(), Session["UserId"].ToString(), DateTime.Now.ToString());
                    i++;
                }
               
                ResetPanel();
            }
        }
        if (RbtnCancelled.Checked == true)
        {
            checkedRadioClick = 1;
            UpdationCheck = ObjLoan.UpdateRecord_In_Pay_Employee_Loan(strCompId, HiddeniD.Value, DateTime.Now.ToString(), "0", "0", "0", "0", "Cancelled", Session["UserId"].ToString(), DateTime.Now.ToString());
            if (UpdationCheck != 0)
            {
                DisplayMessage("Record Saved");
               
                ResetPanel();
            }
        }
        if (checkedRadioClick == 0)
        {
            DisplayMessage("Select RadioButton");
            RbtnApproved.Focus();
            return;
        }
        GridBind();
        btnClosePanel_Click(null, null);

    }
    void ResetPanel()
    {
        txtPopUpEmployee.Text = "";
        txtPopupLoanAmount.Text = "";
        txtPopUpLoanName.Text = "";
        txtMonthlyInstallment.Text = "";
        RbtnCancelled.Checked = false;
        RbtnApproved.Checked = false;
        txtInterest.ReadOnly = true;
        txtDuration.ReadOnly = true;
        txtInterest.Text = "";
        txtDuration.Text = "";
        txtGrossAmount.Text = "";
    }
    protected void TxtDuration_TextChanged(object sender, EventArgs e)
    {
        if (txtInterest.Text == "")
        {
            DisplayMessage("Enter Interest");
            txtDuration.Focus();
            return;
        }

        if (txtDuration.Text == "")
        {
            DisplayMessage("Enter Duration");
            txtDuration.Focus();
            return;
        }
        Double Intereset = Convert.ToDouble(txtInterest.Text);
        int Duration = Convert.ToInt32(txtDuration.Text);
        Double LoanAmount = Convert.ToDouble(txtPopupLoanAmount.Text);
        Double GrossAmount = (LoanAmount * Duration * Intereset) / (12 * 100);
        Double totalGrossAmount = GrossAmount + LoanAmount;
        txtGrossAmount.Text = totalGrossAmount.ToString();
        txtMonthlyInstallment.Text = (totalGrossAmount / Duration).ToString();
        btn_PopUp_Save.Focus();
    }
    protected void RbtnApproved_CheckedChanged(object sender, EventArgs e)
    {
        txtInterest.ReadOnly = false;
        txtDuration.ReadOnly = false;
        txtInterest.Focus();
    }
    protected void RbtnCancelled_CheckedChanged(object sender, EventArgs e)
    {
        txtInterest.ReadOnly = true;
        txtDuration.ReadOnly = true;
        txtInterest.Text = "";
        txtDuration.Text = "";
        btn_PopUp_Save.Focus();
    }
    protected void BtnReset_Click(object sender, EventArgs e)
    {
        Reset();
    }
    protected void BtnCancel_Click(object sender, EventArgs e)
    {
        Reset();
    }
    protected void GridViewLoan_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        GridViewLoan.PageIndex = e.NewPageIndex;
        GridBind();
    }
    protected void GridViewLoan_Sorting(object sender, GridViewSortEventArgs e)
    {
        HDFSort.Value = HDFSort.Value == "ASC" ? "DESC" : "ASC";
        DataTable dt = new DataTable();
        dt = (DataTable)Session["dtFilter"];
        DataView dv = new DataView(dt);
        string Query = "" + e.SortExpression + " " + HDFSort.Value + "";
        dv.Sort = Query;
        dt = dv.ToTable();
        Session["dtFilter"] = dt;

        GridViewLoan.DataSource = dt;
        GridViewLoan.DataBind();
    }
}
