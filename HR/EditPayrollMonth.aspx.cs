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

public partial class HR_EditPayrollMonth : System.Web.UI.Page
{
    #region defind Class Object

    Pay_Employee_Due_Payment objEmpDuePay = new Pay_Employee_Due_Payment();
    Pay_Employee_Loan objloan = new Pay_Employee_Loan();

    Pay_Employee_Month objPayEmpMonth = new Pay_Employee_Month();
    Pay_Employee_Deduction objpayrolldeduc = new Pay_Employee_Deduction();
    Pay_Employe_Allowance objpayrollAllowance = new Pay_Employe_Allowance();
    Pay_Employee_Penalty objPEpenalty = new Pay_Employee_Penalty();
    Pay_Employee_claim objPEClaim = new Pay_Employee_claim();
    Common ObjComman = new Common();
    Set_Pay_Employee_Allow_Deduc ObjAllDeduc = new Set_Pay_Employee_Allow_Deduc();
    Set_Allowance ObjAllow = new Set_Allowance();
    Set_Deduction ObjDeduc = new Set_Deduction();
    CountryMaster ObjSysCountryMaster = new CountryMaster();
    BrandMaster ObjBrandMaster = new BrandMaster();
    LocationMaster ObjLocMaster = new LocationMaster();

    Set_EmployeeGroup_Master objEmpGroup = new Set_EmployeeGroup_Master();

    Set_Group_Employee objGroupEmp = new Set_Group_Employee();
    DepartmentMaster objDep = new DepartmentMaster();
    ReligionMaster objRel = new ReligionMaster();
    NationalityMaster objNat = new NationalityMaster();
    DesignationMaster objDesg = new DesignationMaster();
    QualificationMaster objQualif = new QualificationMaster();
    EmployeeMaster objEmp = new EmployeeMaster();
    SystemParameter objSys = new SystemParameter();
    Set_AddressMaster AM = new Set_AddressMaster();
    Set_AddressCategory ObjAddressCat = new Set_AddressCategory();
    Set_AddressChild objAddChild = new Set_AddressChild();

    Att_Employee_Leave objEmpleave = new Att_Employee_Leave();
    LeaveMaster objLeaveType = new LeaveMaster();
    Att_Employee_Notification objEmpNotice = new Att_Employee_Notification();
    Set_ApplicationParameter objAppParam = new Set_ApplicationParameter();
    CurrencyMaster objCurrency = new CurrencyMaster();
    CompanyMaster objComp = new CompanyMaster();
    EmployeeParameter objEmpParam = new EmployeeParameter();
    Set_Allowance objAllowance = new Set_Allowance();
    Set_Deduction objDeduction = new Set_Deduction();

    #endregion
    protected void Page_Load(object sender, EventArgs e)
    {
        if (Session["UserId"] == null)
        {
            Response.Redirect("~/ERPLogin.aspx");
        }

        if (!IsPostBack)
        {
            if (Session["lang"] == null)
            {
                Session["lang"] = "1";
            }
            ViewState["CurrIndex"] = 0;
            ViewState["SubSize"] = 9;
            ViewState["CurrIndexbin"] = 0;
            ViewState["SubSizebin"] = 9;
            Session["dtLeave"] = null;



            Session["empimgpath"] = null;

            Page.Form.Attributes.Add("enctype", "multipart/form-data");
            bool IsCompOT = false;
            bool IsPartialComp = false;

            IsCompOT = Convert.ToBoolean(objAppParam.GetApplicationParameterValueByParamName("IsOverTime", Session["CompId"].ToString()));
            IsPartialComp = Convert.ToBoolean(objAppParam.GetApplicationParameterValueByParamName("Partial_Leave_Enable", Session["CompId"].ToString()));
            GetEmplyeeData();
        }

        Page.Title = objSys.GetSysTitle();


    }

    public string GetEmployeeName(object empid)
    {

        string empname = string.Empty;

        DataTable dt = objEmp.GetEmployeeMasterAllData(Session["CompId"].ToString());
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
    public string GetEmployeeCode(object empid)
    {

        string empname = string.Empty;

        DataTable dt = objEmp.GetEmployeeMasterAllData(Session["CompId"].ToString());
        dt = new DataView(dt, "Emp_Id='" + empid.ToString().Trim() + "'", "", DataViewRowState.CurrentRows).ToTable();

        if (dt.Rows.Count > 0)
        {

            empname = dt.Rows[0]["Emp_Code"].ToString();

            if (empname == "")
            {
                empname = "No Code";
            }

        }
        else
        {
            empname = "No Code";

        }

        return empname;



    }

    public void GetEmplyeeData()
    {
        DataTable dtEmp = objEmp.GetEmployeeMaster(Session["CompId"].ToString());

        dtEmp = new DataView(dtEmp, "Brand_Id='" + Session["BrandId"].ToString() + "'  and Location_Id='" + Session["LocId"].ToString() + "'", "", DataViewRowState.CurrentRows).ToTable();

        if (Session["SessionDepId"] != null)
        {
            dtEmp = new DataView(dtEmp, "Department_Id in(" + Session["SessionDepId"].ToString().Substring(0, Session["SessionDepId"].ToString().Length - 1) + ")", "", DataViewRowState.CurrentRows).ToTable();

        }
        if (dtEmp.Rows.Count > 0)
        {
            Session["dtEmpLeave"] = dtEmp;
            gvEmpLeave.DataSource = dtEmp;
            gvEmpLeave.DataBind();
            lblTotalRecordsLeave.Text = Resources.Attendance.Total_Records + " : " + dtEmp.Rows.Count.ToString() + "";

        }
        else
        {
            Session["dtEmpLeave"] = null;
            gvEmpLeave.DataSource = null;
            gvEmpLeave.DataBind();
        }

    }

    protected void gvEmpLeave_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        gvEmpLeave.PageIndex = e.NewPageIndex;
        DataTable dtEmp = (DataTable)Session["dtEmpLeave"];
        gvEmpLeave.DataSource = dtEmp;
        gvEmpLeave.DataBind();
        string temp = string.Empty;
        bool isselcted;

        for (int i = 0; i < gvEmpLeave.Rows.Count; i++)
        {
            Label lblconid = (Label)gvEmpLeave.Rows[i].FindControl("lblEmpId");
            string[] split = lblSelectRecd.Text.Split(',');

            for (int j = 0; j < lblSelectRecd.Text.Split(',').Length; j++)
            {
                if (lblSelectRecd.Text.Split(',')[j] != "")
                {
                    if (lblconid.Text.Trim().ToString() == lblSelectRecd.Text.Split(',')[j].Trim().ToString())
                    {
                        ((CheckBox)gvEmpLeave.Rows[i].FindControl("chkgvSelect")).Checked = true;
                    }
                }
            }
        }
        Session["dtLeave"] = null;

       
    }

    protected void btnLeavebind_Click(object sender, ImageClickEventArgs e)
    {
        if (ddlOption1.SelectedIndex != 0)
        {
            string condition = string.Empty;
            if (ddlOption1.SelectedIndex == 1)
            {
                condition = "convert(" + ddlField1.SelectedValue + ",System.String)='" + txtValue1.Text.Trim() + "'";
            }
            else if (ddlOption1.SelectedIndex == 2)
            {
                condition = "convert(" + ddlField1.SelectedValue + ",System.String) like '%" + txtValue1.Text.Trim() + "%'";
            }
            else
            {
                condition = "convert(" + ddlField1.SelectedValue + ",System.String) Like '" + txtValue1.Text.Trim() + "%'";

            }
            DataTable dtEmp = (DataTable)Session["dtEmpLeave"];
            DataView view = new DataView(dtEmp, condition, "", DataViewRowState.CurrentRows);
            gvEmpLeave.DataSource = view.ToTable();
            gvEmpLeave.DataBind();

        }
        Session["dtLeave"] = null;

       
    }
    protected void btnEmpEdit_Command(object sender, CommandEventArgs e)
    {

        Session["EmpIdc"] = e.CommandArgument.ToString();

     

           DataTable dtempe = new DataTable();
           dtempe = objPayEmpMonth.GetPayEmpMonth(Session["EmpIdc"].ToString());
        if (dtempe.Rows.Count > 0 && dtempe.Rows.Count !=null)
        {
            Panel4.Visible = false;
            pnlLeave.Visible = true;
            pnlpenaltyclaim.Visible = false;
            pnlgvallowance.Visible = true;
            pnlgvdeduuc.Visible = false;
            pnlheadername.Visible = true;
           
          lblEmpCodeOT.Text = GetEmployeeCode(e.CommandArgument.ToString());
          lblEmpNameOT.Text = GetEmployeeName(e.CommandArgument.ToString());
        
        btnAllowance_Click(null, null);
        }
        else
        {
            DisplayMessage("Payroll Not Generated");

        }

    }

    protected void btnLeaveRefresh_Click(object sender, ImageClickEventArgs e)
    {
        lblSelectRecd.Text = "";
        ddlField1.SelectedIndex = 1;
        ddlOption1.SelectedIndex = 2;
        txtValue1.Text = "";

        DataTable dtEmp = (DataTable)Session["dtEmpLeave"];
        gvEmpLeave.DataSource = dtEmp;
        gvEmpLeave.DataBind();
        Session["dtLeave"] = null;
    }
    protected void btnAllowance_Click(object sender, EventArgs e)
    {

        pnlmenuloan.BackColor = System.Drawing.ColorTranslator.FromHtml("#90bde9");
        pnlMenuDeduction.BackColor = System.Drawing.ColorTranslator.FromHtml("#90bde9");
        pnlMenupenaltyclaim.BackColor = System.Drawing.ColorTranslator.FromHtml("#90bde9");
        pnlMenuAllowance.BackColor = System.Drawing.ColorTranslator.FromHtml("#ccddee");
        pnlattendance.BackColor = System.Drawing.ColorTranslator.FromHtml("#90bde9");
        pnlattendance.BackColor = System.Drawing.ColorTranslator.FromHtml("#90bde9");
        pnlgvdeduuc.Visible = false;
        pnlattnd.Visible = false;
        pnlpenaltyclaim.Visible = false;
        pnlloan.Visible = false;
       
       

            DataTable dtBindGrid = new DataTable();
            dtBindGrid = objpayrollAllowance.GetPayAllowPayaRoll(Session["EmpIdc"].ToString());

            Session["monthAllow"] = dtBindGrid.Rows[0]["Month"].ToString();
            Session["YearAllow"] = dtBindGrid.Rows[0]["Year"].ToString();
            dtBindGrid = new DataView(dtBindGrid, "Month='" + Session["monthAllow"].ToString() + "' and Year='" + Session["YearAllow"] .ToString()+ "'", "", DataViewRowState.CurrentRows).ToTable();
        if (dtBindGrid.Rows.Count > 0)
            {
               

                pnlgvallowance.Visible = true;
                gvallowance.DataSource = dtBindGrid;
                gvallowance.DataBind();
                foreach (GridViewRow gvr2 in gvallowance.Rows)
                {
                    HiddenField hdnallowanceId = (HiddenField)gvr2.FindControl("hdnallowId");

                    HiddenField hdnRefId = (HiddenField)gvr2.FindControl("hdnRefId");
                    Label lblgvRefValue = (Label)gvr2.FindControl("lblType");

                    if (hdnallowanceId.Value != "0" && hdnallowanceId.Value != "")
                    {

                        DataTable dtAllowance = ObjAllow.GetAllowanceTruebyId(Session["CompId"].ToString(), hdnallowanceId.Value);

                        dtAllowance = new DataView(dtAllowance, "Allowance_Id=" + hdnallowanceId.Value.ToString() + " ", "", DataViewRowState.CurrentRows).ToTable();
                        if (dtAllowance.Rows.Count > 0)
                        {
                            lblgvRefValue.Text = dtAllowance.Rows[0]["Allowance"].ToString();
                        }
                        else
                        {
                            lblgvRefValue.Text = "";
                        }

                    }
                }
            }
            else
            {
             

                gvallowance.DataSource = null;
                gvallowance.DataBind();
                pnlgvallowance.Visible = false;
            }
        
       
    }

    protected void btncencelDeduc_Click(object sender, EventArgs e)
    {
        pnlheadername.Visible = false;
        pnlpenaltyclaim.Visible = false;
        pnlgvallowance.Visible = false;
        pnlgvdeduuc.Visible = false;
        pnlLeave.Visible = false;
        Panel4.Visible = true;
        pnlattnd.Visible = false;
    }

    protected void btncencelpenaltyclaim_Click(object sender, EventArgs e)
    {
        pnlheadername.Visible = false;
        pnlpenaltyclaim.Visible = false;
        pnlgvallowance.Visible = false;
        pnlgvdeduuc.Visible = false;
        pnlLeave.Visible = false;
        Panel4.Visible = true;
        pnlattnd.Visible = false;
    }
    protected void btncencelallow_Click(object sender, EventArgs e)
    {
        pnlheadername.Visible = false;
        pnlpenaltyclaim.Visible = false;
        pnlgvallowance.Visible = false;
        pnlgvdeduuc.Visible = false;
        pnlLeave.Visible = false;
        pnlattnd.Visible = false;
        Panel4.Visible = true;

    }

    protected void gvallowance_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        gvallowance.PageIndex = e.NewPageIndex;
        gvallowance.DataSource = (DataTable)Session["PayAllow"];
        gvallowance.DataBind();
    }

    protected void BtnSave_Click(object sender, EventArgs e)
    {
        int c = 0;
        double totaldeduc = 0;
        double txtamtdeduc = 0;
        double lblamtdeduc = 0;
        int typed = 0;
        string id;
        string Deducname;
        string actvalue;
        string updatevale;

        int d = 0;
        foreach (GridViewRow gvrduc in gvdeduction.Rows)
        {

            Label lblamtded = (Label)gvrduc.FindControl("lblDeductValue");
           // HiddenField Empid = (HiddenField)gvrduc.FindControl("");

            HiddenField hdntransIdded = (HiddenField)gvrduc.FindControl("hdntransIddeduc");
            TextBox txtvalueDeduc = (TextBox)gvrduc.FindControl("txtDeducValue");
            Label lblname = (Label)gvrduc.FindControl("lblTypededuc");

            id = "Deduction Id=" + hdntransIdded.Value;
            Deducname = "Deduction Name=" + lblname.Text;
            actvalue = "Actual Value=" + txtvalueDeduc.Text;

            
            
            d = objpayrolldeduc.UpdateRecordPayEmpDeduction(hdntransIdded.Value, Session["EmpIdc"].ToString(), txtvalueDeduc.Text);

            lblamtdeduc = Convert.ToDouble(lblamtded.Text);
            txtamtdeduc = Convert.ToDouble(txtvalueDeduc.Text);

            if (txtamtdeduc != lblamtdeduc && txtamtdeduc != 0 && txtamtdeduc != null)
            {

                if (txtamtdeduc > lblamtdeduc)
                {
                    totaldeduc = txtamtdeduc - lblamtdeduc;
                    typed = 2;
                }
                if (lblamtdeduc > txtamtdeduc)
                {
                    totaldeduc = lblamtdeduc - txtamtdeduc;
                    typed = 1;
                }
                updatevale = " Updated Value= " + totaldeduc.ToString();

                if (((CheckBox)gvrduc.FindControl("chkdeduc")).Checked)
                {
                    int p = objEmpDuePay.Insert_In_Pay_Employee_Loan(Session["CompId"].ToString(), Session["EmpIdc"].ToString(), Session["monthdeduc"].ToString(), Session["yeardeuc"].ToString(), typed.ToString(), totaldeduc.ToString(), DateTime.Now.ToString(), id.ToString() + "," + Deducname + "," + actvalue + "," + updatevale, "True", "", "", "", "", "", true.ToString(), DateTime.Now.ToString(), "True", Session["UserId"].ToString(), DateTime.Now.ToString(), Session["UserId"].ToString(), DateTime.Now.ToString());
                }
                else
                {
                    int p = objEmpDuePay.Insert_In_Pay_Employee_Loan(Session["CompId"].ToString(), Session["EmpIdc"].ToString(), Session["monthdeduc"].ToString(), Session["yeardeuc"].ToString(), typed.ToString(), totaldeduc.ToString(), DateTime.Now.ToString(), id.ToString()+"," + Deducname +"," + actvalue +","+ updatevale, "False", "", "", "", "", "", true.ToString(), DateTime.Now.ToString(), "True", Session["UserId"].ToString(), DateTime.Now.ToString(), Session["UserId"].ToString(), DateTime.Now.ToString());
                }
            
            }



        }
        if (d != 0)
        {
            DisplayMessage("Record Updated");

        }

    }
    
    protected void btnsaveallow_Click(object sender, EventArgs e)
    {
        int c = 0;
        double totalamount = 0;
        double txtamtal = 0;
        double lblamtal = 0;
        int type = 0; 
        int trnsid;
        string id;
        string allowancename;
        string actvalue;
        string updatevale;
        foreach (GridViewRow gvr1 in gvallowance.Rows)
        {
            Label lblamt = (Label)gvr1.FindControl("lblAllowValue");
            HiddenField Empid = (HiddenField)gvr1.FindControl("hdnEmpId");
            HiddenField hdnTId = (HiddenField)gvr1.FindControl("hdntransId");
            TextBox txtvalue2 = (TextBox)gvr1.FindControl("txtValue");
            HiddenField allwId = (HiddenField)gvr1.FindControl("hdnallowId");
            CheckBox chkallow = (CheckBox)gvr1.FindControl("chkallowance");
            Label lblname = (Label)gvr1.FindControl("lblType");
            
            id = "Allowance Id=" + allwId.Value;
            allowancename = "Allowance Name=" + lblname.Text;
            actvalue = "Actual Value=" + txtvalue2.Text;


            trnsid = Convert.ToInt32(hdnTId.Value);
            c = objpayrollAllowance.UpdatePayEMpAllowActVale(hdnTId.Value, Empid.Value, txtvalue2.Text);
            
            lblamtal = Convert.ToDouble(lblamt.Text);
            txtamtal = Convert.ToDouble(txtvalue2.Text);

            if (txtamtal != lblamtal && txtamtal != 0 && txtamtal != null)
            {
                
                    if (txtamtal > lblamtal)
                    {
                        totalamount = txtamtal - lblamtal;
                        type = 2;

                    }
                    if (lblamtal > txtamtal)
                    {
                        totalamount = lblamtal - txtamtal;
                        type = 1;

                    }
                    updatevale ="Updated Value=" +totalamount.ToString();
                    
                if (((CheckBox)gvr1.FindControl("chkallowance")).Checked)
                    {

                        int p = objEmpDuePay.Insert_In_Pay_Employee_Loan(Session["CompId"].ToString(), Session["EmpIdc"].ToString(), Session["monthAllow"].ToString(), Session["YearAllow"].ToString(), type.ToString(), totalamount.ToString(), DateTime.Now.ToString(), id.ToString() +"," + allowancename + "," + actvalue +","+ updatevale , "True", "", "", "", "", "", true.ToString(), DateTime.Now.ToString(), "True", Session["UserId"].ToString(), DateTime.Now.ToString(), Session["UserId"].ToString(), DateTime.Now.ToString());
                    }
                    else
                    {
                        int p = objEmpDuePay.Insert_In_Pay_Employee_Loan(Session["CompId"].ToString(), Session["EmpIdc"].ToString(), Session["monthAllow"].ToString(), Session["YearAllow"].ToString(), type.ToString(), totalamount.ToString(), DateTime.Now.ToString(), id.ToString() + "," + allowancename + "," + actvalue + "," + updatevale , "False", "", "", "", "", "", true.ToString(), DateTime.Now.ToString(), "True", Session["UserId"].ToString(), DateTime.Now.ToString(), Session["UserId"].ToString(), DateTime.Now.ToString());
                    }


            }
            
        }
 
        //DataTable dtBindGrid = new DataTable();
        //dtBindGrid = objpayrollAllowance.GetPayAllowPayaRoll(Session["EmpIdc"].ToString());
        //gvallowance.DataSource = dtBindGrid;
        //gvallowance.DataBind();

        //if (dtBindGrid.Rows.Count == 0)
        //{
        //    pnlgvallowance.Visible = false;

        //}
        if (c != 0)
        {
            btnAllowance_Click(null, null);
            DisplayMessage("Record Updated");

        }
       
    }

    public void DisplayMessage(string str)
    {

        ScriptManager.RegisterStartupScript(this, GetType(), "", "alert('" + str + "');", true);

    }

    protected void btndeduction_Click(object sender, EventArgs e)
    {
        pnlmenuloan.BackColor = System.Drawing.ColorTranslator.FromHtml("#90bde9");
        pnlMenuDeduction.BackColor = System.Drawing.ColorTranslator.FromHtml("#ccddee");
        pnlMenupenaltyclaim.BackColor = System.Drawing.ColorTranslator.FromHtml("#90bde9");
        pnlMenuAllowance.BackColor = System.Drawing.ColorTranslator.FromHtml("#90bde9");
        pnlattendance.BackColor = System.Drawing.ColorTranslator.FromHtml("#90bde9");

        pnlgvallowance.Visible = false;
        pnlpenaltyclaim.Visible = false;
        pnlloan.Visible = false;
        pnlattnd.Visible = false;
        DataTable dtBindGrid1 = new DataTable();
        dtBindGrid1 = objpayrolldeduc.GetPayDeducPayaRoll(Session["EmpIdc"].ToString());
        dtBindGrid1 = new DataView(dtBindGrid1, "Month='" + dtBindGrid1.Rows[0]["Month"].ToString() + "' and Year='" + dtBindGrid1.Rows[0]["Year"].ToString() + "'", "", DataViewRowState.CurrentRows).ToTable();
        
        if (dtBindGrid1.Rows.Count > 0)
        {
            Session["monthdeduc"] = dtBindGrid1.Rows[0]["Month"].ToString();
            Session["yeardeuc"] = dtBindGrid1.Rows[0]["Year"].ToString();

            pnlgvdeduuc.Visible = true;
            gvdeduction.DataSource = dtBindGrid1;
            gvdeduction.DataBind();
            Session["deducData"] = dtBindGrid1;
            foreach (GridViewRow gvrd in gvdeduction.Rows)
            {
                HiddenField hdndeductionId = (HiddenField)gvrd.FindControl("hdnDeducId");
                HiddenField hdnRefIdded = (HiddenField)gvrd.FindControl("hdnRefId");
                Label lblgvRefValue = (Label)gvrd.FindControl("lblTypededuc");

                if (hdndeductionId.Value != "0" && hdndeductionId.Value != "")
                {

                    DataTable dtDeduction = objDeduction.GetDeductionTruebyId(Session["CompId"].ToString(), hdndeductionId.Value);

                    dtDeduction = new DataView(dtDeduction, "Deduction_Id=" + hdndeductionId.Value.ToString() + " ", "", DataViewRowState.CurrentRows).ToTable();
                    if (dtDeduction.Rows.Count > 0)
                    {
                        lblgvRefValue.Text = dtDeduction.Rows[0]["Deduction"].ToString();
                    }
                    else
                    {
                        lblgvRefValue.Text = "";
                    }
                }
            }


        }
        else
        {
           
           // DisplayMessage("NO Payroll Generated");
           
              // No Payroll generated
        
            gvdeduction.DataSource = null;
            gvdeduction.DataBind();
            pnlgvdeduuc.Visible = false;

        }

    }

    protected void gvdeduction_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        gvdeduction.PageIndex = e.NewPageIndex;
        gvdeduction.DataSource = (DataTable)Session["deducData"];
        gvdeduction.DataBind();
    }

    protected void btnclaimpenalty_Click(object sender, EventArgs e)
    {
        int monthclaim = 0;
        int yearclaim = 0;
        double totaldueamt = 0;
        double dueamt = 0;
        double dueamtt = 0;
        pnlmenuloan.BackColor = System.Drawing.ColorTranslator.FromHtml("#90bde9");
        pnlMenuDeduction.BackColor = System.Drawing.ColorTranslator.FromHtml("#90bde9");
        pnlMenupenaltyclaim.BackColor = System.Drawing.ColorTranslator.FromHtml("#ccddee");
        pnlMenuAllowance.BackColor = System.Drawing.ColorTranslator.FromHtml("#90bde9");

        pnlattendance.BackColor = System.Drawing.ColorTranslator.FromHtml("#90bde9");
        int i = 0;
        pnlpenaltyclaim.Visible = true;
        pnlgvallowance.Visible = false;
        pnlgvdeduuc.Visible = false;
        pnlloan.Visible = false;
        pnlattnd.Visible = false;
        lbldueamt.Text = "0.00";
        txtdueamt.Text = "0.00";
        DataTable dtclaimpenaltry = new DataTable();
        dtclaimpenaltry = objPayEmpMonth.GetPenaltyClaim(Session["EmpIdc"].ToString());
        dtclaimpenaltry = new DataView(dtclaimpenaltry, "Month='" + dtclaimpenaltry.Rows[0]["Month"].ToString() + "' and Year='" + dtclaimpenaltry.Rows[0]["Year"].ToString() + "'", "", DataViewRowState.CurrentRows).ToTable();
        if (dtclaimpenaltry.Rows.Count > 0)
        {
            pnlpenaltyclaim.Visible = true;
            lbltotalpenalty.Text = dtclaimpenaltry.Rows[0]["Employee_Penalty"].ToString();
            txttotalpeanlty.Text = dtclaimpenaltry.Rows[0]["Employee_Penalty"].ToString();
            lbltotalclaim.Text = dtclaimpenaltry.Rows[0]["Employee_Claim"].ToString();
            txttotalclaim.Text = dtclaimpenaltry.Rows[0]["Employee_Claim"].ToString();

            Session["monthclaim1"] = Convert.ToInt32(dtclaimpenaltry.Rows[0]["Month"].ToString());
            yearclaim=Convert.ToInt32(dtclaimpenaltry.Rows[0]["Year"].ToString());

            monthclaim = Convert.ToInt32(dtclaimpenaltry.Rows[0]["Month"].ToString());
            Session["yearclaim1"] = yearclaim.ToString();
        }
        else
        {
           
            pnlpenaltyclaim.Visible = false;

        }

        int year = yearclaim;
        if (monthclaim == 1)
        {
            monthclaim = 12;
            yearclaim = year - 1;

        }
        else
        {
            monthclaim = monthclaim - 1;

        } 
        DataTable dtdueamt = new DataTable();
        dueamt = 0;
        dueamtt = 0;
        dtdueamt=objEmpDuePay.GetRecord_Emp_Due_paymentType1(Session["EmpIdc"].ToString(),monthclaim.ToString(),yearclaim.ToString());
        //dtdueamt = new DataView(dtdueamt, " Month=" + (monthclaim - 1) + " and Year="+ yearclaim + "", "", DataViewRowState.CurrentRows).ToTable();
        if (dtdueamt.Rows.Count > 0 && dtdueamt.Rows[0]["Amount"].ToString() != "")
        {
            dueamt = Convert.ToDouble(dtdueamt.Rows[0]["Amount"]);
        }
       
            DataTable dtdueamt1 = new DataTable();

            dtdueamt1 = objEmpDuePay.GetRecord_Emp_Due_paymentByType2(Session["EmpIdc"].ToString(), monthclaim.ToString(), yearclaim.ToString());

            if (dtdueamt1.Rows.Count > 0 && dtdueamt1.Rows[0]["Amount"].ToString() != "")
            {

                dueamtt = Convert.ToDouble(dtdueamt1.Rows[0]["Amount"]);
               
            }
            totaldueamt = dueamt - dueamtt;
            lbldueamt.Text = totaldueamt.ToString();
            txtdueamt.Text = totaldueamt.ToString();

    
       
    }

    protected void btnsaveclaimpenalty_Click(object sender, EventArgs e)
    {
        int c = 0;
        double totalclaim = 0;
        double txtamtclaim = 0;
        double lblamtclaim = 0;
        double txtpenalty = 0;
        double lblpenalty = 0;
        double totalpenalty = 0;
        int typed = 0;
         int p = 0;         
        
         string actvaluepenalty;
         string actvalueclaim;
         
         string updatevaleclaim;
         string updatevalepenalty;


        p = objPayEmpMonth.UpdateRecord_Pay_Employee_Penalty_claim(Session["EmpIdc"].ToString(), txttotalpeanlty.Text, txttotalclaim.Text);

        lblamtclaim = Convert.ToDouble(lbltotalclaim.Text);
        txtamtclaim = Convert.ToDouble(txttotalclaim.Text);
        lblpenalty = Convert.ToDouble(txttotalpeanlty.Text);
        txtpenalty = Convert.ToDouble(lbltotalpenalty.Text);
        
        actvalueclaim ="Actual Value Claim="+ txttotalclaim.Text;

        actvaluepenalty = "Actual Value Penalty=" + txttotalpeanlty.Text;

        

        if (txtamtclaim != lblamtclaim && txtamtclaim != 0 && txtamtclaim != null)
        {
            
             if (txtamtclaim > lblamtclaim)
            {
                totalclaim = txtamtclaim - lblamtclaim;
                typed = 1;
            }
            if (lblamtclaim > txtamtclaim)
            {
                totalclaim = lblamtclaim - txtamtclaim;
                typed = 2;
            }


            updatevaleclaim = " Updated Value= " + totalclaim.ToString();

            if(chkclaim.Checked)
            {
                c = objEmpDuePay.Insert_In_Pay_Employee_Loan(Session["CompId"].ToString(), Session["EmpIdc"].ToString(), Session["monthclaim1"].ToString(), Session["yearclaim1"].ToString(), typed.ToString(), totalclaim.ToString(), DateTime.Now.ToString(), actvalueclaim + "," + updatevaleclaim, "True", "", "", "", "", "", true.ToString(), DateTime.Now.ToString(), "True", Session["UserId"].ToString(), DateTime.Now.ToString(), Session["UserId"].ToString(), DateTime.Now.ToString());
            }
            else
            {
                c = objEmpDuePay.Insert_In_Pay_Employee_Loan(Session["CompId"].ToString(), Session["EmpIdc"].ToString(), Session["monthclaim1"].ToString(), Session["yearclaim1"].ToString(), typed.ToString(), totalclaim.ToString(), DateTime.Now.ToString(), actvalueclaim + "," + updatevaleclaim, "False", "", "", "", "", "", true.ToString(), DateTime.Now.ToString(), "True", Session["UserId"].ToString(), DateTime.Now.ToString(), Session["UserId"].ToString(), DateTime.Now.ToString());
            
            }
        
        }

        if (txtpenalty != lblpenalty && txtpenalty != 0 && txtpenalty != null)
        {

            if (txtpenalty > lblpenalty)
            {
                totalpenalty = txtpenalty - lblpenalty;
                typed = 2;
            }
            if (lblpenalty > txtpenalty)
            {
                totalpenalty = lblpenalty - txtpenalty;
                typed = 1;
            }
            updatevalepenalty = " Updated Value= "+ totalpenalty.ToString();

            if(chkpenalty.Checked)
            {
            c = objEmpDuePay.Insert_In_Pay_Employee_Loan(Session["CompId"].ToString(), Session["EmpIdc"].ToString(), Session["monthclaim1"].ToString(), Session["yearclaim1"].ToString(), typed.ToString(), totalpenalty.ToString(), DateTime.Now.ToString(),actvaluepenalty +","+ updatevalepenalty , "True", "", "", "", "", "", true.ToString(), DateTime.Now.ToString(), "True", Session["UserId"].ToString(), DateTime.Now.ToString(), Session["UserId"].ToString(), DateTime.Now.ToString());
            }
            else
            {
                c = objEmpDuePay.Insert_In_Pay_Employee_Loan(Session["CompId"].ToString(), Session["EmpIdc"].ToString(), Session["monthclaim1"].ToString(), Session["yearclaim1"].ToString(), typed.ToString(), totalpenalty.ToString(), DateTime.Now.ToString(), actvaluepenalty +","+ updatevalepenalty, "False", "", "", "", "", "", true.ToString(), DateTime.Now.ToString(), "True", Session["UserId"].ToString(), DateTime.Now.ToString(), Session["UserId"].ToString(), DateTime.Now.ToString());
            
            }
  
        }

        if (p != 0)
        {
            DisplayMessage("Record Updated");
            btnclaimpenalty_Click(null, null);

        }

    }

    protected void btnloan_Click(object sender, EventArgs e)
    {
        pnlmenuloan.BackColor = System.Drawing.ColorTranslator.FromHtml("#ccddee");
        pnlMenuDeduction.BackColor = System.Drawing.ColorTranslator.FromHtml("#90bde9");
        pnlMenupenaltyclaim.BackColor = System.Drawing.ColorTranslator.FromHtml("#90bde9");
        pnlMenuAllowance.BackColor = System.Drawing.ColorTranslator.FromHtml("#90bde9");
        pnlattendance.BackColor = System.Drawing.ColorTranslator.FromHtml("#90bde9");

        pnlpenaltyclaim.Visible = false;
        pnlgvallowance.Visible = false;
        pnlgvdeduuc.Visible = false;
        pnlattnd.Visible = false;
        DataTable dtempedit = new DataTable();
        dtempedit = objPayEmpMonth.GetPayEmpMonth(Session["EmpIdc"].ToString());
        if (dtempedit.Rows.Count > 0)
        {
            DataTable Dtloan = new DataTable();
            Dtloan = objloan.GetRecord_From_PayEmployeeLoan(Session["CompId"].ToString(), "0", "Running");

            Dtloan = new DataView(Dtloan, " Emp_Id=" + Session["EmpIdc"].ToString() + "", "", DataViewRowState.CurrentRows).ToTable();
            if (Dtloan.Rows.Count > 0)
            {
                DataTable dtloandetial = new DataTable();

                for (int i = 0; i < Dtloan.Rows.Count; i++)
                {
                    dtloandetial.Merge(objloan.GetRecord_From_PayEmployeeLoanDetail(Dtloan.Rows[i]["Loan_Id"].ToString()));

                }

                if (dtloandetial.Rows.Count > 0)
                {
                    dtloandetial = new DataView(dtloandetial, " Month=" + dtempedit.Rows[0]["Month"] + " and Year=" + dtempedit.Rows[0]["Year"] + "", "", DataViewRowState.CurrentRows).ToTable();
                }

               
                if (dtloandetial.Rows.Count > 0)
                {
                    pnlloan.Visible = true;
                    gvloan.DataSource = dtloandetial;
                    gvloan.DataBind();

                    foreach (GridViewRow gvrl in gvloan.Rows)
                    {
                        HiddenField hdnId = (HiddenField)gvrl.FindControl("hdnloanId");
                        Label lblloanna = (Label)gvrl.FindControl("lblloanname");

                        if (hdnId.Value != "0" && hdnId.Value != "")
                        {

                            DataTable Dtloann = objloan.GetRecord_From_PayEmployeeLoan(Session["CompId"].ToString(), hdnId.Value.ToString(), "Running");
                            Dtloann = new DataView(Dtloann, "Loan_Id=" + hdnId.Value.ToString() + " ", "", DataViewRowState.CurrentRows).ToTable();

                            if (Dtloann.Rows.Count > 0)
                            {
                                lblloanna.Text = Dtloann.Rows[0]["Loan_Name"].ToString();
                            }
                            else
                            {
                                lblloanna.Text = "";
                            }

                        }

                    }

                }


                else
                {
                    gvloan.DataSource = null;
                    gvloan.DataBind();
                }
            }
        }
        else
        {
           // DisplayMessage("NO Payroll Generated");
           
              
        }
    }
    protected void gvloan_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        gvloan.PageIndex = e.NewPageIndex;
        gvloan.DataSource = (DataTable)Session["loandata"];
        gvloan.DataBind();
    }

    protected void btnsaveloan_Click(object sender, EventArgs e)
    {
        // Before Validation on Save
        // Validation  on Save Button too

        // Loop Acc to Grid View Rows
        // Check Acc to Loan Id and next Month Record 
        // Add in Month =1 if value is greater than 13 then we will set month = 1 and add in year one
        // Check Record Exists
        // Current Month Employee Paid Amount 
        // And Update in Next Month PB and Taotal Amount
        // Else
        // No Can edit installment
        
        double pvbal=0;
        double instllamt=0;
        double totalamt=0;
        double txtamount=0;

        foreach (GridViewRow gvrloan in gvloan.Rows)
        {
            TextBox txtamt = (TextBox)gvrloan.FindControl("txtloanamt");
            HiddenField hdnlnId = (HiddenField)gvrloan.FindControl("hdnloanId");
            HiddenField hdntrnsid = (HiddenField)gvrloan.FindControl("hdntrnasLoanId");
            Label lblloanamt = (Label)gvrloan.FindControl("lblloanamt");

            int counter = 0;

            instllamt = Convert.ToDouble(lblloanamt.Text);

            txtamount = Convert.ToDouble(txtamt.Text);

            int trnsid = 0;

            trnsid = Convert.ToInt32(hdntrnsid.Value);
            if (instllamt != txtamount)
            {
                DataTable dtlndedetials = new DataTable();
                dtlndedetials = objloan.GetRecord_From_PayEmployeeLoanDetailAll();
                dtlndedetials = new DataView(dtlndedetials, "Loan_Id=" + hdnlnId.Value + " and Trans_Id =" + (trnsid+1) +" ", "", DataViewRowState.CurrentRows).ToTable();



                if (dtlndedetials.Rows.Count > 0)
                {

                    if (instllamt > txtamount)
                    {
                        pvbal = instllamt - txtamount;

                    }
                    if (txtamount > instllamt)
                    {
                        pvbal = txtamount - instllamt;

                    }

                    totalamt = instllamt + pvbal;
                    objloan.UpdateRecord_loandetials_Amt(Session["CompId"].ToString(), hdnlnId.Value.ToString(), (trnsid + 1).ToString(), pvbal.ToString(), totalamt.ToString(), "0");

                }
                else
                {
                    counter = 1;

                }

            }
          
            if (counter == 1)
            {

                DisplayMessage("Amount is not adjusted");
                return;
            }
            else
            {
                objloan.UpdateRecord_loandetials_Amt(Session["CompId"].ToString(), hdnlnId.Value.ToString(), trnsid.ToString(), "0", lblloanamt.Text, txtamt.Text);
                DisplayMessage("Record Updated");
                return;

            }
        }

    

    }

    protected void btncenellaon_Click(object sender, EventArgs e)
    {
        pnlheadername.Visible = false;
        pnlpenaltyclaim.Visible = false;
        pnlgvallowance.Visible = false;
        pnlgvdeduuc.Visible = false;
        pnlLeave.Visible = false;
        pnlloan.Visible = false;
        pnlattnd.Visible = false;
        Panel4.Visible = true;

    }

    protected void lnkbtnback_Click(object sender, EventArgs e)
    {
        pnlpenaltyclaim.Visible = false;
        pnlgvallowance.Visible = false;
        pnlgvdeduuc.Visible = false;
        pnlLeave.Visible = false;
        pnlloan.Visible = false;
        pnlattnd.Visible = false;
        Panel4.Visible = true;
        pnlheadername.Visible = false;
    }

    protected void btnattendance_Click(object sender, EventArgs e)
    {

        pnlmenuloan.BackColor = System.Drawing.ColorTranslator.FromHtml("#90bde9");
        pnlMenuDeduction.BackColor = System.Drawing.ColorTranslator.FromHtml("#90bde9");
        pnlMenupenaltyclaim.BackColor = System.Drawing.ColorTranslator.FromHtml("#90bde9");
        pnlMenuAllowance.BackColor = System.Drawing.ColorTranslator.FromHtml("#90bde9");

        pnlattendance.BackColor = System.Drawing.ColorTranslator.FromHtml("#ccddee");

        pnlgvallowance.Visible = false;
        pnlgvdeduuc.Visible = false;
        pnlloan.Visible = false;
        pnlpenaltyclaim.Visible = false;

        DataTable dtattendance = new DataTable();
        dtattendance=objPayEmpMonth.Getallrecords(Session["EmpIdc"].ToString());
        if (dtattendance.Rows.Count > 0)
        {
            pnlgvallowance.Visible = false;
            pnlgvdeduuc.Visible = false;
            pnlloan.Visible = false;
            pnlpenaltyclaim.Visible = false;
            pnlattnd.Visible = true;

            lbltrnsid.Text= dtattendance.Rows[0]["Trans_Id"].ToString();
            
            lblmonth.Text = dtattendance.Rows[0]["Month"].ToString();
            lblyear.Text = dtattendance.Rows[0]["Year"].ToString();

            txtworkedminsal.Text = dtattendance.Rows[0]["Worked_Min_Salary"].ToString();
            txtnormalOtminsal.Text = dtattendance.Rows[0]["Normal_OT_Min_Salary"].ToString();
            txtWeekoffotminsal.Text = dtattendance.Rows[0]["Week_Off_OT_Min_Salary"].ToString();
            txthloidayotminsal.Text = dtattendance.Rows[0]["Holiday_OT_Min_Salary"].ToString();
            txtleavedayssal.Text = dtattendance.Rows[0]["Leave_Days_Salary"].ToString();
            txtweekoffsal.Text = dtattendance.Rows[0]["Week_Off_Salary"].ToString();
            txtholidayssal.Text = dtattendance.Rows[0]["Holidays_Salary"].ToString();
            txtAbsaentsal.Text = dtattendance.Rows[0]["Absent_Salary"].ToString();
            txtlateminsal.Text = dtattendance.Rows[0]["Late_Min_Penalty"].ToString();
            txtearlyminsal.Text = dtattendance.Rows[0]["Early_Min_Penalty"].ToString();
            txtpatialvoisal.Text = dtattendance.Rows[0]["Patial_Violation_Penalty"].ToString();
        }

    }
    protected void btnsaveattend_Click(object sender, EventArgs e)
    {

        objPayEmpMonth.UpdateRecord_Salary_By_TransId(Session["EmpIdc"].ToString(), lblmonth.Text, lblyear.Text, txtworkedminsal.Text, txtnormalOtminsal.Text, txtWeekoffotminsal.Text, txthloidayotminsal.Text, txtleavedayssal.Text, txtweekoffsal.Text, txtholidayssal.Text, txtAbsaentsal.Text, txtlateminsal.Text, txtearlyminsal.Text, txtpatialvoisal.Text);
        btnattendance_Click(null, null);
        DisplayMessage("Record Updated");
        return;
       
    }
    protected void btncencelattend_Click(object sender, EventArgs e)
    {
        pnlheadername.Visible = false;
        pnlpenaltyclaim.Visible = false;
        pnlgvallowance.Visible = false;
        pnlgvdeduuc.Visible = false;
        pnlLeave.Visible = false;
        pnlattnd.Visible = false;
        Panel4.Visible = true;
       
    }
   
}