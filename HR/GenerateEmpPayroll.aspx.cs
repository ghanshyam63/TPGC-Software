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

public partial class HR_GenerateEmpPayroll : System.Web.UI.Page
{
    #region defind Class Object

    Pay_Employee_Due_Payment objEmpDuePay = new Pay_Employee_Due_Payment();

    Pay_Employee_Attendance objEmpAttendance = new Pay_Employee_Attendance();

    Pay_Employee_Loan objEmpLoan = new Pay_Employee_Loan();
    Pay_Employee_Month objPayEmpMonth = new Pay_Employee_Month();
    Pay_Employee_Deduction objpayrolldeduc = new Pay_Employee_Deduction();
    Pay_Employe_Allowance objpayrollall = new Pay_Employe_Allowance();
    Pay_Employee_Penalty objPEpenalty = new Pay_Employee_Penalty();
    Pay_Employee_claim objPEClaim = new Pay_Employee_claim();
    
    
    Common ObjComman = new Common();
    Set_Pay_Employee_Allow_Deduc ObjAllDeduc = new Set_Pay_Employee_Allow_Deduc();
    Set_Allowance ObjAllow = new Set_Allowance();
    Set_Deduction ObjDeduc = new Set_Deduction();
    CountryMaster ObjSysCountryMaster = new CountryMaster();
    BrandMaster ObjBrandMaster = new BrandMaster();
    LocationMaster ObjLocMaster = new LocationMaster();
    EmployeeParameter objempparam = new EmployeeParameter();

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

            btnLeave_Click(null, null);
            bool IsCompOT = false;
            bool IsPartialComp = false;

            IsCompOT = Convert.ToBoolean(objAppParam.GetApplicationParameterValueByParamName("IsOverTime", Session["CompId"].ToString()));
            IsPartialComp = Convert.ToBoolean(objAppParam.GetApplicationParameterValueByParamName("Partial_Leave_Enable", Session["CompId"].ToString()));




            ddlMonth.SelectedIndex = System.DateTime.Now.Month;


        }

        Page.Title = objSys.GetSysTitle();




    }
    protected void lbxGroup_SelectedIndexChanged(object sender, EventArgs e)
    {
        string GroupIds = string.Empty;
        string EmpIds = string.Empty;
        for (int i = 0; i < lbxGroup.Items.Count; i++)
        {
            if (lbxGroup.Items[i].Selected == true)
            {
                GroupIds += lbxGroup.Items[i].Value + ",";

            }

        }
        if (GroupIds != "")
        {
            DataTable dtEmp = objEmp.GetEmployeeMaster(Session["CompId"].ToString());

            dtEmp = new DataView(dtEmp, "Brand_Id='" + Session["BrandId"].ToString() + "'  and Location_Id='" + Session["LocId"].ToString() + "'", "", DataViewRowState.CurrentRows).ToTable();
            DataTable dtEmpInGroup = objGroupEmp.GetGroup_EmployeeById(Session["CompId"].ToString());

            dtEmpInGroup = new DataView(dtEmpInGroup, "Group_Id in(" + GroupIds.Substring(0, GroupIds.Length - 1) + ")", "", DataViewRowState.CurrentRows).ToTable();

            for (int i = 0; i < dtEmpInGroup.Rows.Count; i++)
            {
                if (!EmpIds.Split(',').Contains(dtEmpInGroup.Rows[i]["Emp_Id"].ToString()))
                {
                    EmpIds += dtEmpInGroup.Rows[i]["Emp_Id"].ToString() + ",";
                }
            }
            if (EmpIds != "")
            {
                dtEmp = new DataView(dtEmp, "Emp_Id in(" + EmpIds.Substring(0, EmpIds.Length - 1) + ")", "", DataViewRowState.CurrentRows).ToTable();

            }
            else
            {
                dtEmp = new DataTable();
            }

            if (dtEmp.Rows.Count > 0)
            {
                Session["dtEmp1"] = dtEmp;
                gvEmployee.DataSource = dtEmp;
                gvEmployee.DataBind();

            }
            else
            {
                Session["dtEmp1"] = null;
                gvEmployee.DataSource = null;
                gvEmployee.DataBind();

            }

        }
        else
        {
            gvEmployee.DataSource = null;
            gvEmployee.DataBind();

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

        //chkYearCarry.Visible = false;
    }
    protected void chkgvSelectAll_CheckedChangedLeave(object sender, EventArgs e)
    {
        CheckBox chkSelAll = ((CheckBox)gvEmpLeave.HeaderRow.FindControl("chkgvSelectAll"));
        for (int i = 0; i < gvEmpLeave.Rows.Count; i++)
        {
            ((CheckBox)gvEmpLeave.Rows[i].FindControl("chkgvSelect")).Checked = chkSelAll.Checked;
            if (chkSelAll.Checked)
            {
                if (!lblSelectRecd.Text.Split(',').Contains(((Label)(gvEmpLeave.Rows[i].FindControl("lblEmpId"))).Text.Trim().ToString()))
                {
                    lblSelectRecd.Text += ((Label)(gvEmpLeave.Rows[i].FindControl("lblEmpId"))).Text.Trim().ToString() + ",";
                }
            }
            else
            {
                string temp = string.Empty;
                string[] split = lblSelectRecd.Text.Split(',');
                foreach (string item in split)
                {
                    if (item != ((Label)(gvEmpLeave.Rows[i].FindControl("lblEmpId"))).Text.Trim().ToString())
                    {
                        if (item != "")
                        {
                            temp += item + ",";
                        }
                    }
                }
                lblSelectRecd.Text = temp;
            }
        }
        Session["dtLeave"] = null;

    }
   
    protected void chkgvSelect_CheckedChangedLeave(object sender, EventArgs e)
    {
        string empidlist = string.Empty;
        string temp = string.Empty;
        int index = ((GridViewRow)((CheckBox)sender).Parent.Parent).RowIndex;
        Label lb = (Label)gvEmpLeave.Rows[index].FindControl("lblEmpId");
        if (((CheckBox)gvEmpLeave.Rows[index].FindControl("chkgvSelect")).Checked)
        {
            empidlist += lb.Text.Trim().ToString() + ",";
            lblSelectRecd.Text += empidlist;

        }

        else
        {

            empidlist += lb.Text.ToString().Trim();
            lblSelectRecd.Text += empidlist;
            string[] split = lblSelectRecd.Text.Split(',');
            foreach (string item in split)
            {
                if (item != empidlist)
                {
                    if (item != "")
                    {
                        if (item != "")
                        {
                            temp += item + ",";
                        }
                    }
                }
            }
            lblSelectRecd.Text = temp;
        }
        Session["dtLeave"] = null;

    }
    protected void ImgbtnSelectAll_ClickLeave(object sender, ImageClickEventArgs e)
    {
        DataTable dtProduct = (DataTable)Session["dtEmpLeave"];

        if (ViewState["Select"] == null)
        {
            ViewState["Select"] = 1;
            foreach (DataRow dr in dtProduct.Rows)
            {
                if (!lblSelectRecd.Text.Split(',').Contains(dr["Emp_Id"]))
                {
                    lblSelectRecd.Text += dr["Emp_Id"] + ",";
                }
            }
            for (int i = 0; i < gvEmpLeave.Rows.Count; i++)
            {
                string[] split = lblSelectRecd.Text.Split(',');
                Label lblconid = (Label)gvEmpLeave.Rows[i].FindControl("lblEmpId");
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
        }
        else
        {
            lblSelectRecd.Text = "";
            DataTable dtProduct1 = (DataTable)Session["dtEmpLeave"];
            gvEmpLeave.DataSource = dtProduct1;
            gvEmpLeave.DataBind();
            ViewState["Select"] = null;
        }
        Session["dtLeave"] = null;

        //chkYearCarry.Visible = false;
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

        //chkYearCarry.Visible = false;
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

        //chkYearCarry.Visible = false;
    }
    protected void btnLeave_Click(object sender, EventArgs e)
    {

        pnlLeave.BackColor = System.Drawing.ColorTranslator.FromHtml("#ccddee");


        PnlEmployeeLeave.Visible = true;
        lblSelectRecd.Text = "";
        gvEmpLeave.Visible = true;
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
        Session["dtLeave"] = null;


        rbtnEmp.Checked = true;
        rbtnGroup.Checked = false;
        EmpGroup_CheckedChanged(null, null);
    }
    protected void gvEmp1_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        gvEmployee.PageIndex = e.NewPageIndex;
        gvEmployee.DataSource = (DataTable)Session["dtEmp1"];
        gvEmployee.DataBind();
    }
    protected void EmpGroup_CheckedChanged(object sender, EventArgs e)
    {
        if (rbtnGroup.Checked)
        {
            Panel4.Visible = false;
            pnlGroup.Visible = true;

            DataTable dtGroup = objEmpGroup.GetEmployeeGroup_Master(Session["CompId"].ToString());
            dtGroup = new DataView(dtGroup, "Brand_Id='" + Session["BrandId"].ToString() + "'  and Location_Id='" + Session["LocId"].ToString() + "'", "", DataViewRowState.CurrentRows).ToTable();
            if (dtGroup.Rows.Count > 0)
            {
                lbxGroup.DataSource = dtGroup;
                lbxGroup.DataTextField = "Group_Name";
                lbxGroup.DataValueField = "Group_Id";

                lbxGroup.DataBind();

            }



            lbxGroup_SelectedIndexChanged(null, null);
        }
        else if (rbtnEmp.Checked)
        {
            Panel4.Visible = true;
            pnlGroup.Visible = false;

            lblEmp.Text = "";
            string GroupIds = string.Empty;
            string EmpIds = string.Empty;
            DataTable dtEmp = objEmp.GetEmployeeMaster(Session["CompId"].ToString());

            dtEmp = new DataView(dtEmp, "Brand_Id='" + Session["BrandId"].ToString() + "'  and Location_Id='" + Session["LocId"].ToString() + "'", "", DataViewRowState.CurrentRows).ToTable();




            if (dtEmp.Rows.Count > 0)
            {
                Session["dtEmp"] = dtEmp;
                gvEmployee.DataSource = dtEmp;
                gvEmployee.DataBind();









            }

        }


    }

    
    public void DisplayMessage(string str)
    {

        ScriptManager.RegisterStartupScript(this, GetType(), "", "alert('" + str + "');", true);

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

    public string GetType(string Type)
    {
        if (Type == "1")
        {
            Type = "Allowance";
        }
        else if (Type == "2")
        {
            Type = "Deducation";

        }
        return Type;
    }
    public string GetCalculation(string Value_Type)
    {
        if (Value_Type == "1")
        {
            Value_Type = "Fixed";
        }
        else if (Value_Type == "2")
        {
            Value_Type = "Percentage";

        }
        return Value_Type;
    }

    protected void btnGenratePayroll_Click(object sender, EventArgs e)
    {
        double sumpenalty = 0;
        double sum = 0;
        double sumallow = 0;
        double sumdeduc = 0;
        double sumloan = 0;
        int b = 0;
        int a = 0;
        int mainflag = 0;

        int monthclaim = 0;
        int yearclaim = 0;
        double totaldueamt;
        double dueamt = 0;
        double dueamtt = 0;

        double basicsal = 0;

        DataTable dtEmpPay = GetTable();

        string TransNo = string.Empty;


        if (rbtnGroup.Checked)
        {
            string GroupIds = string.Empty;
            string EmpIds = string.Empty;

            for (int i = 0; i < lbxGroup.Items.Count; i++)
            {
                if (lbxGroup.Items[i].Selected)
                {
                    GroupIds += lbxGroup.Items[i].Value + ",";
                }

            }

            if (GroupIds != "")
            {
                DataTable dtEmp = objEmp.GetEmployeeMaster(Session["CompId"].ToString());

                dtEmp = new DataView(dtEmp, "Brand_Id='" + Session["BrandId"].ToString() + "'  and Location_Id='" + Session["LocId"].ToString() + "'", "", DataViewRowState.CurrentRows).ToTable();
                DataTable dtEmpInGroup = objGroupEmp.GetGroup_EmployeeById(Session["CompId"].ToString());

                dtEmpInGroup = new DataView(dtEmpInGroup, "Group_Id in(" + GroupIds.Substring(0, GroupIds.Length - 1) + ")", "", DataViewRowState.CurrentRows).ToTable();

                for (int i = 0; i < dtEmpInGroup.Rows.Count; i++)
                {
                    if (!EmpIds.Split(',').Contains(dtEmpInGroup.Rows[i]["Emp_Id"].ToString()))
                    {
                        EmpIds += dtEmpInGroup.Rows[i]["Emp_Id"].ToString() + ",";
                    }
                }

            }
            else
            {
                DisplayMessage("Select Group First");
            }



            foreach (string str in EmpIds.Split(','))
            {

                if ((str != ""))
                {
                    DataTable dtempmonth = new DataTable();
                    dtempmonth = objPayEmpMonth.GetAllRecordPostedEmpMonth(str, ddlMonth.SelectedIndex.ToString(), TxtYear.Text.ToString());
                    if (dtempmonth.Rows.Count > 0)
                    {
                        DisplayMessage("Payroll Already Generated");
                        
                        btnLeave_Click(null, null);
                        return;

                    }
                    else
                    {

                        DataTable dtemppara = new DataTable();
                        dtemppara = objempparam.GetEmployeeParameterByEmpId(str, Session["CompId"].ToString());
                        if (dtemppara.Rows.Count > 0)
                        {

                            basicsal = Convert.ToDouble(dtemppara.Rows[0]["Basic_Salary"].ToString());
                        }
                        DataTable dtEmpMonth = new DataTable();

                        // Pay_Employe_Month_Temp: Get Data Of Employee
                        dtEmpMonth = objPayEmpMonth.GetPayEmpMonth(str);
                        int m = 0;
                        if (dtEmpMonth.Rows.Count > 0)
                        {
                            if (dtEmpMonth.Rows[0]["Emp_Id"].ToString() == str && dtEmpMonth.Rows[0]["Month"].ToString() == ddlMonth.SelectedValue && dtEmpMonth.Rows[0]["Year"].ToString() == TxtYear.Text)
                            {
                                // Here Delete From Master And Child Table Record 
                                // If Same Month And Year Record
                                m = objPayEmpMonth.DeleteEmpMonth(str, ddlMonth.SelectedValue, TxtYear.Text);
                                a = objpayrolldeduc.DeletePayDeduction(str, ddlMonth.SelectedValue, TxtYear.Text);
                                b = objpayrollall.DeletePayAllowance(str, ddlMonth.SelectedValue, TxtYear.Text);
                            }
                            else
                            {
                                mainflag = 1;
                            }

                        }

                        if ((mainflag != 1) && (str != ""))
                        {

                            int ma = objPayEmpMonth.Insert_Pay_Employee_Month(Session["CompId"].ToString(), str, ddlMonth.SelectedValue.ToString(), TxtYear.Text, "0", "0", "0", "0", "0", "0", "0", "0", "0", "0", "0", "0", "0", "0", "0", "0");
                            // Get Trans Id of thie Employee acc to EmpId,Month,Year = Ref id of allowance and Deduction Temp
                            // Get Employee Allowance

                            //   DataTable dtemptransId = new DataTable();
                            // dtemptransId = objPayEmpMonth.GetRecordByEmpIdMonthYear(str, ddlMonth.SelectedValue, TxtYear.Text);

                            sumallow = 0;
                            DataTable dtEmpAllowDeduc = GetEmpAllowDedu(Convert.ToInt32(str));

                            dtEmpAllowDeduc = new DataView(dtEmpAllowDeduc, "Type = 1", "", DataViewRowState.CurrentRows).ToTable();

                            for (int i = 0; i < dtEmpAllowDeduc.Rows.Count; i++)
                            {

                                DataRow row = dtEmpPay.NewRow();
                                row[0] = str;


                                row[1] = "";
                                row[2] = ddlMonth.SelectedItem.Text;
                                row[3] = TxtYear.Text;
                                row[4] = dtEmpAllowDeduc.Rows[i]["Type"].ToString();
                                if (row[4].ToString() == "1")
                                {
                                    row[4] = "Allowance";
                                }
                                else
                                {
                                    row[4] = "Deduction";

                                }
                                row[5] = dtEmpAllowDeduc.Rows[i]["Ref_Id"].ToString();
                                row[6] = dtEmpAllowDeduc.Rows[i]["Value_Type"].ToString();
                                if (row[6].ToString() == "1")
                                {
                                    GetCalculation(dtEmpAllowDeduc.Rows[i]["Value_Type"].ToString());
                                    row[7] = dtEmpAllowDeduc.Rows[i]["Value"].ToString();
                                    Double al = Convert.ToDouble(dtEmpAllowDeduc.Rows[i]["Value"]);
                                    sumallow += al;

                                }
                                else
                                {
                                    double val = Convert.ToDouble((basicsal * Convert.ToDouble(dtEmpAllowDeduc.Rows[i]["Value"].ToString())) / 100);
                                    row[7] = val.ToString();
                                    sumallow += val;
                                }

                                dtEmpPay.Rows.Add(row);

                                // Insert Allowance Temp Here\\\
                                // Update Ref Id = Trans Id of this Employee  in  Pay_Employe_Month_Temp
                                int k = objpayrollall.InsertPayrollEmpAllowance(str, ddlMonth.SelectedValue.ToString(), TxtYear.Text, ma.ToString(), dtEmpAllowDeduc.Rows[i]["Ref_Id"].ToString(), dtEmpAllowDeduc.Rows[i]["Value_Type"].ToString(), row[7].ToString(), row[7].ToString());

                            }

                            sumdeduc = 0;
                            dtEmpAllowDeduc = GetEmpAllowDedu(Convert.ToInt32(str));

                            dtEmpAllowDeduc = new DataView(dtEmpAllowDeduc, "Type = 2", "", DataViewRowState.CurrentRows).ToTable();



                            for (int i = 0; i < dtEmpAllowDeduc.Rows.Count; i++)
                            {

                                DataRow row = dtEmpPay.NewRow();
                                row[0] = str;

                                row[1] = "";
                                row[2] = ddlMonth.SelectedItem.Text;
                                row[3] = TxtYear.Text;
                                row[4] = dtEmpAllowDeduc.Rows[i]["Type"].ToString();
                                if (row[4].ToString() == "1")
                                {
                                    row[4] = "Allowance";
                                }
                                else
                                {
                                    row[4] = "Deduction";

                                }
                                row[5] = dtEmpAllowDeduc.Rows[i]["Ref_Id"].ToString();
                                row[6] = dtEmpAllowDeduc.Rows[i]["Value_Type"].ToString();
                                if (row[6].ToString() == "1")
                                {
                                    row[7] = dtEmpAllowDeduc.Rows[i]["Value"].ToString();
                                    Double deduc = Convert.ToDouble(dtEmpAllowDeduc.Rows[i]["Value"]);
                                    sumdeduc += deduc;
                                }
                                else
                                {
                                    double val = Convert.ToDouble((basicsal * Convert.ToDouble(dtEmpAllowDeduc.Rows[i]["Value"].ToString())) / 100);
                                    row[7] = val.ToString();
                                    sumdeduc += val;
                                }

                                dtEmpPay.Rows.Add(row);

                                // Insert Deduction Temp Here\\\
                                // Update Ref Id = Trans Id of this Employee  in  Pay_Employe_Month_Temp
                                int j = objpayrolldeduc.InsertPayrollEmpDeduction(str, ddlMonth.SelectedValue.ToString(), TxtYear.Text, ma.ToString(), dtEmpAllowDeduc.Rows[i]["Ref_Id"].ToString(), dtEmpAllowDeduc.Rows[i]["Value_Type"].ToString(), row[7].ToString(), row[7].ToString());


                            }


                            // Here we will get all Penality record of this Employee acc to selected Month and Year
                            // We will get data table and loop accrding to rows

                            sumpenalty = 0;
                            DataTable dtEmpPenalty = objPEpenalty.GetRecord_From_PayEmployeePenalty_1(Session["CompId"].ToString(), str, "", ddlMonth.SelectedIndex.ToString(), TxtYear.Text, "", "");
                            for (int i = 0; i < dtEmpPenalty.Rows.Count; i++)
                            {

                                DataRow row = dtEmpPay.NewRow();
                                row[0] = str;
                                row[1] = "";
                                row[2] = ddlMonth.SelectedItem.Text;
                                row[3] = TxtYear.Text;
                                row[4] = dtEmpPenalty.Rows[i]["Penalty_Name"].ToString(); ;
                                row[5] = dtEmpPenalty.Rows[i]["Penalty_Id"].ToString();
                                row[6] = dtEmpPenalty.Rows[i]["Value_Type"].ToString();
                                if (row[6].ToString() == "1")
                                {
                                    row[7] = dtEmpPenalty.Rows[i]["Value"].ToString();
                                    Double p = Convert.ToDouble(dtEmpPenalty.Rows[i]["Value"]);

                                    sumpenalty += p;
                                }
                                else
                                {
                                    double val = Convert.ToDouble((basicsal * Convert.ToDouble(dtEmpPenalty.Rows[i]["Value"].ToString())) / 100);
                                    row[7] = val.ToString();
                                }
                                row[6] = GetCalculation(dtEmpPenalty.Rows[i]["Value_Type"].ToString());
                                dtEmpPay.Rows.Add(row);


                            }



                            if (sumpenalty.ToString() == "")
                            {
                                lblpenaltyshow.Text = "0";
                            }
                            else
                            {
                                lblpenaltyshow.Text = sumpenalty.ToString();
                            }

                            // Here we will get all claim record of this Employee acc to selected Month and Year only IsApproved Record
                            // We will get data table and loop accrding to rows

                            sum = 0;
                            DataTable dtEmpClaim = objPEClaim.GetRecord_From_PayEmployeeClaim_1(Session["CompId"].ToString(), str, "", "Approved", ddlMonth.SelectedIndex.ToString(), TxtYear.Text, "", "");
                            for (int i = 0; i < dtEmpClaim.Rows.Count; i++)
                            {

                                DataRow row = dtEmpPay.NewRow();
                                row[0] = str;
                                row[1] = "";
                                row[2] = ddlMonth.SelectedItem.Text;
                                row[3] = TxtYear.Text;
                                row[4] = dtEmpClaim.Rows[i]["Claim_Approved"].ToString();
                                row[5] = dtEmpClaim.Rows[i]["Claim_Id"].ToString();
                                row[6] = dtEmpClaim.Rows[i]["Value_Type"].ToString();
                                if (row[6].ToString() == "1")
                                {
                                    Double h = Convert.ToDouble(dtEmpClaim.Rows[i]["Value"]);
                                    row[7] = dtEmpClaim.Rows[i]["Value"].ToString();
                                    sum += h;
                                }
                                else
                                {

                                    double val = Convert.ToDouble((basicsal * Convert.ToDouble(dtEmpClaim.Rows[i]["Value"].ToString())) / 100);
                                    row[7] = val.ToString();
                                    sum += val;


                                }
                                row[6] = GetCalculation(dtEmpClaim.Rows[i]["Value_Type"].ToString());
                                dtEmpPay.Rows.Add(row);
                            }


                            sumloan = 0;
                            DataTable Dtloan = new DataTable();
                            Dtloan = objEmpLoan.GetRecord_From_PayEmployeeLoan(Session["CompId"].ToString(), "0", "Running");

                            Dtloan = new DataView(Dtloan, " Emp_Id=" + str + "", "", DataViewRowState.CurrentRows).ToTable();
                            for (int i = 0; i < Dtloan.Rows.Count; i++)
                            {
                                DataTable dtloandetial = new DataTable();
                                dtloandetial = objEmpLoan.GetRecord_From_PayEmployeeLoanDetail(Dtloan.Rows[0]["Loan_Id"].ToString());
                                dtloandetial = new DataView(dtloandetial, "Loan_Id=" + Dtloan.Rows[0]["Loan_Id"].ToString() + " and Month=" + ddlMonth.SelectedValue + " and Year=" + TxtYear.Text + "", "", DataViewRowState.CurrentRows).ToTable();
                                for (int j = 0; j < dtloandetial.Rows.Count; j++)
                                {

                                    DataRow row = dtEmpPay.NewRow();
                                    if (dtloandetial.Rows[j]["Total_Amount"].ToString() != "")
                                    {
                                        row[6] = dtloandetial.Rows[j]["Total_Amount"].ToString();
                                        double loan = Convert.ToDouble(dtloandetial.Rows[j]["Total_Amount"]);

                                        sumloan += loan;
                                    }
                                    else
                                    {
                                        sumloan = 0;
                                    }
                                }


                            }

                            monthclaim = ddlMonth.SelectedIndex;
                            yearclaim = Convert.ToInt32(TxtYear.Text);
                            if (monthclaim == 1)
                            {
                                monthclaim = 12;
                                yearclaim = yearclaim - 1;

                            }
                            else
                            {
                                monthclaim = monthclaim - 1;
                            }
                            
                            totaldueamt = 0;
                            
                            
                            DataTable dtdueamt = new DataTable();

                            dtdueamt = objEmpDuePay.GetRecord_Emp_Due_paymentType1(str, monthclaim.ToString(), yearclaim.ToString());

                            DataTable dtdueamt1 = new DataTable();
                            dtdueamt1 = objEmpDuePay.GetRecord_Emp_Due_paymentByType2(str, monthclaim.ToString(), yearclaim.ToString());


                            if (dtdueamt.Rows.Count > 0 && dtdueamt1.Rows.Count > 0 && dtdueamt1.Rows.Count != null && dtdueamt.Rows.Count != null && dtdueamt.Rows[0]["Amount"].ToString() != "" && dtdueamt1.Rows[0]["Amount"].ToString() != "")
                            {
                                dueamt = Convert.ToDouble(dtdueamt.Rows[0]["Amount"]);
                                dueamtt = Convert.ToDouble(dtdueamt1.Rows[0]["Amount"]);
                                totaldueamt = dueamt - dueamtt;
                            }
                            int u = 0;
                            DataTable dtemprecord = new DataTable();
                            dtemprecord = objPayEmpMonth.GetRecord_PrvBal_Fields(str, ddlMonth.SelectedIndex.ToString(), TxtYear.Text.ToString());

                            if (dtemprecord.Rows.Count > 0)
                            {
                                u = objPayEmpMonth.UpdateRecord_PrvBal_fields_By_TransId(dtemprecord.Rows[0]["Trans_Id"].ToString(), totaldueamt.ToString(), dtemprecord.Rows[0]["Field1"].ToString(), dtemprecord.Rows[0]["Field2"].ToString(), dtemprecord.Rows[0]["Field3"].ToString(), dtemprecord.Rows[0]["Field4"].ToString(), dtemprecord.Rows[0]["Field5"].ToString(), dtemprecord.Rows[0]["Field6"].ToString(), dtemprecord.Rows[0]["Field7"].ToString(), dtemprecord.Rows[0]["Field8"].ToString(), dtemprecord.Rows[0]["Field9"].ToString(), dtemprecord.Rows[0]["Field10"].ToString());

                            }

                        }



                        if ((mainflag != 1) && (str != ""))
                            sumpenalty = 0;
                        sum = 0;
                        sumallow = 0;
                        sumdeduc = 0;
                        {
                            int dty = Convert.ToInt32(TxtYear.Text);
                            int dtm = ddlMonth.SelectedIndex;
                            int days = System.DateTime.DaysInMonth(dty, dtm);
                            int p = 0;

                            p = objPayEmpMonth.UpdateRecord_Pay_Employee_Month(Session["CompId"].ToString(), str, ddlMonth.SelectedValue.ToString(), TxtYear.Text.ToString(), sumpenalty.ToString(), sum.ToString(), sumloan.ToString(), sumallow.ToString(), sumdeduc.ToString());

                            DataTable dtEmpattendnce = new DataTable();
                            dtEmpattendnce = objEmpAttendance.GetRecord_Emp_Attendance(str, ddlMonth.SelectedValue.ToString(), TxtYear.Text);

                            if (dtEmpattendnce.Rows.Count > 0)
                            {

                                int s = objPayEmpMonth.UpdateRecord_Salary_By_TransId(str, ddlMonth.SelectedValue.ToString(), TxtYear.Text.ToString(), dtEmpattendnce.Rows[0]["Basic_Min_Salary"].ToString(), dtEmpattendnce.Rows[0]["Normal_OT_Salary"].ToString(), dtEmpattendnce.Rows[0]["Week_Off_OT_Salary"].ToString(), dtEmpattendnce.Rows[0]["Holiday_OT_Salary"].ToString(), dtEmpattendnce.Rows[0]["Leave_Days_Salary"].ToString(), dtEmpattendnce.Rows[0]["Week_Off_Days_Salary"].ToString(), dtEmpattendnce.Rows[0]["Holiday_Days_Salary"].ToString(), dtEmpattendnce.Rows[0]["Absent_Day_Penalty"].ToString(), dtEmpattendnce.Rows[0]["Late_Min_Penalty"].ToString(), dtEmpattendnce.Rows[0]["Early_Min_Penalty"].ToString(), dtEmpattendnce.Rows[0]["Parital_Violation_Penalty"].ToString());
                            }
                            // p = objPayEmpMonth.UpdateRecord_Pay_Employee_Month(Session["CompId"].ToString(), str, ddlMonth.SelectedValue.ToString(), TxtYear.Text, days.ToString(), "0", "0", "0", "0", "0", "0", "0", "0", "0", "0", "0", "0", "0", "0", "0", "0", sumpenalty.ToString(), sum.ToString(), sumloan.ToString(), sumallow.ToString(), sumdeduc.ToString());

                            // Update In Master Table Total Allowance /Deduction/Claim/Penalty
                            // Acc to Month Year And Employee Id
                        }


                    }
                }
            }
        }

        else
        {
            if (lblSelectRecd.Text == "")
            {

                DisplayMessage("Select employee first");
                return;

            }

            foreach (string str in lblSelectRecd.Text.Split(','))
            {
                if ((str != ""))
                {

                    DataTable dtempmonth = new DataTable();
                    dtempmonth = objPayEmpMonth.GetAllRecordPostedEmpMonth(str, ddlMonth.SelectedValue.ToString(), TxtYear.Text.ToString());
                    if (dtempmonth.Rows.Count > 0)
                    {
                        DisplayMessage("Payroll Already Generated");
                        
                        btnLeave_Click(null, null);
                        return;

                    }
                    else
                    {

                        DataTable dtEmpMonth = new DataTable();

                        // Pay_Employe_Month_Temp: Get Data Of Employee
                        dtEmpMonth = objPayEmpMonth.GetPayEmpMonth(str);
                        int m = 0;
                        if (dtEmpMonth.Rows.Count > 0)
                        {
                            if (dtEmpMonth.Rows[0]["Emp_Id"].ToString() == str && dtEmpMonth.Rows[0]["Month"].ToString() == ddlMonth.SelectedValue && dtEmpMonth.Rows[0]["Year"].ToString() == TxtYear.Text)
                            {
                                // Here Delete From Master And Child Table Record 
                                // If Same Month And Year Record
                                m = objPayEmpMonth.DeleteEmpMonth(str, ddlMonth.SelectedValue, TxtYear.Text);
                                a = objpayrolldeduc.DeletePayDeduction(str, ddlMonth.SelectedValue, TxtYear.Text);
                                b = objpayrollall.DeletePayAllowance(str, ddlMonth.SelectedValue, TxtYear.Text);
                            }
                            else
                            {
                                mainflag = 1;
                            }

                        }

                        if ((mainflag != 1) && (str != ""))
                        {

                            sumpenalty = 0;
                            sum = 0;
                            sumallow = 0;
                            sumdeduc = 0;

                            int ma = objPayEmpMonth.Insert_Pay_Employee_Month(Session["CompId"].ToString(), str, ddlMonth.SelectedValue.ToString(), TxtYear.Text, "0", "0", "0", "0", "0", "0", "0", "0", "0", "0", "0", "0", "0", "0", "0", "0");
                            // Get Trans Id of thie Employee acc to EmpId,Month,Year = Ref id of allowance and Deduction Temp
                            // Get Employee Allowance

                            DataTable dtEmpAllowDeduc = GetEmpAllowDedu(Convert.ToInt32(str));
                            sumallow = 0;
                            dtEmpAllowDeduc = new DataView(dtEmpAllowDeduc, "Type = 1", "", DataViewRowState.CurrentRows).ToTable();

                            for (int i = 0; i < dtEmpAllowDeduc.Rows.Count; i++)
                            {

                                DataRow row = dtEmpPay.NewRow();
                                row[0] = str;


                                row[1] = "";
                                row[2] = ddlMonth.SelectedItem.Text;
                                row[3] = TxtYear.Text;
                                row[4] = dtEmpAllowDeduc.Rows[i]["Type"].ToString();
                                row[5] = dtEmpAllowDeduc.Rows[i]["Ref_Id"].ToString();
                                row[6] = dtEmpAllowDeduc.Rows[i]["Value_Type"].ToString();
                                if (row[6].ToString() == "1")
                                {
                                    GetCalculation(dtEmpAllowDeduc.Rows[i]["Value_Type"].ToString());
                                    row[7] = dtEmpAllowDeduc.Rows[i]["Value"].ToString();
                                    double al = Convert.ToDouble(dtEmpAllowDeduc.Rows[i]["Value"]);
                                    sumallow += al;

                                }
                                else
                                {
                                    double val = Convert.ToDouble((basicsal * Convert.ToDouble(dtEmpAllowDeduc.Rows[i]["Value"].ToString())) / 100);
                                    row[7] = val.ToString();
                                    sumallow += val;
                                }

                                dtEmpPay.Rows.Add(row);

                                // Insert Allowance Temp Here\\\
                                // Update Ref Id = Trans Id of this Employee  in  Pay_Employe_Month_Temp
                                int k = objpayrollall.InsertPayrollEmpAllowance(str, ddlMonth.SelectedValue.ToString(), TxtYear.Text, ma.ToString(), dtEmpAllowDeduc.Rows[i]["Ref_Id"].ToString(), dtEmpAllowDeduc.Rows[i]["Value_Type"].ToString(), row[7].ToString(), row[7].ToString());

                            }

                            dtEmpAllowDeduc = GetEmpAllowDedu(Convert.ToInt32(str));

                            dtEmpAllowDeduc = new DataView(dtEmpAllowDeduc, "Type = 2", "", DataViewRowState.CurrentRows).ToTable();
                            sumdeduc = 0;
                            for (int i = 0; i < dtEmpAllowDeduc.Rows.Count; i++)
                            {
                                DataRow row = dtEmpPay.NewRow();
                                row[0] = str;

                                row[1] = "";
                                row[2] = ddlMonth.SelectedItem.Text;
                                row[3] = TxtYear.Text;
                                row[4] = dtEmpAllowDeduc.Rows[i]["Type"].ToString();
                                if (row[4].ToString() == "1")
                                {
                                    row[4] = "Allowance";
                                }
                                else
                                {
                                    row[4] = "Deduction";

                                }
                                row[5] = dtEmpAllowDeduc.Rows[i]["Ref_Id"].ToString();
                                row[6] = dtEmpAllowDeduc.Rows[i]["Value_Type"].ToString();
                                if (row[6].ToString() == "1")
                                {
                                    row[7] = dtEmpAllowDeduc.Rows[i]["Value"].ToString();
                                    double deduc = Convert.ToDouble(dtEmpAllowDeduc.Rows[i]["Value"]);
                                    sumdeduc += deduc;
                                }
                                else
                                {
                                    double val = Convert.ToDouble((basicsal * Convert.ToDouble(dtEmpAllowDeduc.Rows[i]["Value"].ToString())) / 100);
                                    row[7] = val.ToString();
                                    sumdeduc += val;
                                }

                                dtEmpPay.Rows.Add(row);

                                // Insert Deduction Temp Here\\\
                                // Update Ref Id = Trans Id of this Employee  in  Pay_Employe_Month_Temp
                                int j = objpayrolldeduc.InsertPayrollEmpDeduction(str, ddlMonth.SelectedValue.ToString(), TxtYear.Text, ma.ToString(), dtEmpAllowDeduc.Rows[i]["Ref_Id"].ToString(), dtEmpAllowDeduc.Rows[i]["Value_Type"].ToString(), row[7].ToString(), row[7].ToString());


                            }

                            // Here we will get all Penality record of this Employee acc to selected Month and Year
                            // We will get data table and loop accrding to rows
                            sumpenalty = 0;
                            DataTable dtEmpPenalty = objPEpenalty.GetRecord_From_PayEmployeePenalty_1(Session["CompId"].ToString(), str, "", ddlMonth.SelectedIndex.ToString(), TxtYear.Text, "", "");
                            for (int i = 0; i < dtEmpPenalty.Rows.Count; i++)
                            {

                                DataRow row = dtEmpPay.NewRow();
                                row[0] = str;
                                row[1] = "";
                                row[2] = ddlMonth.SelectedItem.Text;
                                row[3] = TxtYear.Text;
                                row[4] = dtEmpPenalty.Rows[i]["Penalty_Name"].ToString(); ;
                                row[5] = dtEmpPenalty.Rows[i]["Penalty_Id"].ToString();
                                row[6] = dtEmpPenalty.Rows[i]["Value_Type"].ToString();
                                if (row[6].ToString() == "1")
                                {
                                    row[7] = dtEmpPenalty.Rows[i]["Value"].ToString();
                                    double p = Convert.ToDouble(dtEmpPenalty.Rows[i]["Value"]);
                                    row[7] = dtEmpPenalty.Rows[i]["Value"].ToString();
                                    sumpenalty += p;
                                }
                                else
                                {
                                    double val = Convert.ToDouble((basicsal * Convert.ToDouble(dtEmpPenalty.Rows[i]["Value"].ToString())) / 100);
                                    row[7] = val.ToString();
                                    sumpenalty += val;
                                }
                                row[6] = GetCalculation(dtEmpPenalty.Rows[i]["Value_Type"].ToString());
                                dtEmpPay.Rows.Add(row);


                            }

                            // Here we will get all claim record of this Employee acc to selected Month and Year only IsApproved Record
                            // We will get data table and loop accrding to rows
                            sum = 0;
                            DataTable dtEmpClaim = objPEClaim.GetRecord_From_PayEmployeeClaim_1(Session["CompId"].ToString(), str, "", "Approved", ddlMonth.SelectedIndex.ToString(), TxtYear.Text, "", "");
                            for (int i = 0; i < dtEmpClaim.Rows.Count; i++)
                            {

                                DataRow row = dtEmpPay.NewRow();
                                row[0] = str;
                                row[1] = "";
                                row[2] = ddlMonth.SelectedItem.Text;
                                row[3] = TxtYear.Text;
                                row[4] = dtEmpClaim.Rows[i]["Claim_Approved"].ToString();
                                row[5] = dtEmpClaim.Rows[i]["Claim_Id"].ToString();
                                row[6] = dtEmpClaim.Rows[i]["Value_Type"].ToString();
                                if (row[6].ToString() == "1")
                                {
                                    Double h = Convert.ToDouble(dtEmpClaim.Rows[i]["Value"]);
                                    row[7] = dtEmpClaim.Rows[i]["Value"].ToString();
                                    sum += h;
                                }
                                else
                                {

                                    double val = Convert.ToDouble((basicsal * Convert.ToDouble(dtEmpClaim.Rows[i]["Value"].ToString())) / 100);
                                    row[7] = val.ToString();
                                    sum += val;


                                }
                                row[6] = GetCalculation(dtEmpClaim.Rows[i]["Value_Type"].ToString());
                                dtEmpPay.Rows.Add(row);
                            }
                            // Get employee loan 
                            double paidamount = 0;
                            sumloan = 0;
                            DataTable Dtloan = new DataTable();
                            Dtloan = objEmpLoan.GetRecord_From_PayEmployeeLoan(Session["CompId"].ToString(), "0", "Running");

                            Dtloan = new DataView(Dtloan, " Emp_Id=" + str + "", "", DataViewRowState.CurrentRows).ToTable();
                            for (int i = 0; i < Dtloan.Rows.Count; i++)
                            {
                                DataTable dtloandetial = new DataTable();

                                dtloandetial = objEmpLoan.GetRecord_From_PayEmployeeLoanDetail(Dtloan.Rows[i]["Loan_Id"].ToString());
                                dtloandetial = new DataView(dtloandetial, "Loan_Id=" + Dtloan.Rows[i]["Loan_Id"].ToString() + " and Month=" + ddlMonth.SelectedValue + " and Year=" + TxtYear.Text + " ", "", DataViewRowState.CurrentRows).ToTable();
                                                             
                                if (dtloandetial.Rows.Count > 0)
                                {
                                    Session["loanId"] = dtloandetial.Rows[0]["Loan_Id"].ToString();
                                    Session["TransId"] = dtloandetial.Rows[0]["Trans_Id"].ToString();
                                    paidamount = Convert.ToDouble(dtloandetial.Rows[0]["Total_Amount"].ToString());
                                    
                                    DataRow row = dtEmpPay.NewRow();

                                    double loan = Convert.ToDouble(dtloandetial.Rows[0]["Total_Amount"]);

                                    sumloan += loan;
                                }
                                else
                                {
                                  


                                }
                                if (Session["loanId"] != null && Session["TransId"] != null)
                                {
                                    objEmpLoan.UpdateRecord_loandetials_Bystaus(Session["loanId"].ToString(), Session["TransId"].ToString(), paidamount.ToString(), "Paid");
                                }
                                

                            }


                            // Calculate Due Payment of the employee

                            monthclaim = ddlMonth.SelectedIndex;
                            yearclaim = Convert.ToInt32(TxtYear.Text);
                            if (monthclaim == 1)
                            {
                                monthclaim = 12;
                                yearclaim = yearclaim - 1;

                            }

                            totaldueamt = 0;
                            monthclaim = monthclaim - 1;
                            DataTable dtdueamt = new DataTable();

                            dtdueamt = objEmpDuePay.GetRecord_Emp_Due_paymentType1(str, monthclaim.ToString(), yearclaim.ToString());

                            DataTable dtdueamt1 = new DataTable();
                            dtdueamt1 = objEmpDuePay.GetRecord_Emp_Due_paymentByType2(str, monthclaim.ToString(), yearclaim.ToString());


                            if (dtdueamt.Rows.Count > 0 && dtdueamt1.Rows.Count > 0 && dtdueamt1.Rows.Count != null && dtdueamt.Rows.Count != null && dtdueamt.Rows[0]["Amount"].ToString() != "" && dtdueamt1.Rows[0]["Amount"].ToString() != "")
                            {
                                dueamt = Convert.ToDouble(dtdueamt.Rows[0]["Amount"]);
                                dueamtt = Convert.ToDouble(dtdueamt1.Rows[0]["Amount"]);
                                totaldueamt = dueamt - dueamtt;
                            }
                            int u = 0;
                            DataTable dtemprecord = new DataTable();
                            dtemprecord = objPayEmpMonth.GetRecord_PrvBal_Fields(str, ddlMonth.SelectedValue.ToString(), TxtYear.Text.ToString());

                            if (dtemprecord.Rows.Count > 0)
                            {
                                u = objPayEmpMonth.UpdateRecord_PrvBal_fields_By_TransId(dtemprecord.Rows[0]["Trans_Id"].ToString(), totaldueamt.ToString(), dtemprecord.Rows[0]["Field1"].ToString(), dtemprecord.Rows[0]["Field2"].ToString(), dtemprecord.Rows[0]["Field3"].ToString(), dtemprecord.Rows[0]["Field4"].ToString(), dtemprecord.Rows[0]["Field5"].ToString(), dtemprecord.Rows[0]["Field6"].ToString(), dtemprecord.Rows[0]["Field7"].ToString(), dtemprecord.Rows[0]["Field8"].ToString(), dtemprecord.Rows[0]["Field9"].ToString(), dtemprecord.Rows[0]["Field10"].ToString());

                            }

                        }


                        if ((mainflag != 1) && (str != ""))
                        {
                            int dty = Convert.ToInt32(TxtYear.Text);
                            int dtm = ddlMonth.SelectedIndex;
                            int days = System.DateTime.DaysInMonth(dty, dtm);

                            int p = 0;
                            p = objPayEmpMonth.UpdateRecord_Pay_Employee_Month(Session["CompId"].ToString(), str, ddlMonth.SelectedValue.ToString(), TxtYear.Text.ToString(), sumpenalty.ToString(), sum.ToString(), sumloan.ToString(), sumallow.ToString(), sumdeduc.ToString());
                            DataTable dtEmpattendnce = new DataTable();
                            dtEmpattendnce = objEmpAttendance.GetRecord_Emp_Attendance(str, ddlMonth.SelectedValue.ToString(), TxtYear.Text);

                            if (dtEmpattendnce.Rows.Count > 0)
                            {

                                int s = objPayEmpMonth.UpdateRecord_Salary_By_TransId(str, ddlMonth.SelectedValue.ToString(), TxtYear.Text.ToString(), dtEmpattendnce.Rows[0]["Basic_Min_Salary"].ToString(), dtEmpattendnce.Rows[0]["Normal_OT_Salary"].ToString(), dtEmpattendnce.Rows[0]["Week_Off_OT_Salary"].ToString(), dtEmpattendnce.Rows[0]["Holiday_OT_Salary"].ToString(), dtEmpattendnce.Rows[0]["Leave_Days_Salary"].ToString(), dtEmpattendnce.Rows[0]["Week_Off_Days_Salary"].ToString(), dtEmpattendnce.Rows[0]["Holiday_Days_Salary"].ToString(), dtEmpattendnce.Rows[0]["Absent_Day_Penalty"].ToString(), dtEmpattendnce.Rows[0]["Late_Min_Penalty"].ToString(), dtEmpattendnce.Rows[0]["Early_Min_Penalty"].ToString(), dtEmpattendnce.Rows[0]["Parital_Violation_Penalty"].ToString());
                            }

                            // Update In Master Table Total Allowance /Deduction/Claim/Penalty
                            // Acc to Month Year And Employee Id
                        }

                    }
                }
            }
        }


        DisplayMessage("Generate payroll successfully");
        btnLeave_Click(null, null);
    }


    protected void btnpostpayroll_Click(object sender, EventArgs e)
    {

        DataTable dtEmpPay = GetTable();

        string TransNo = string.Empty;


        if (rbtnGroup.Checked)
        {
            string GroupIds = string.Empty;
            string EmpIds = string.Empty;

            for (int i = 0; i < lbxGroup.Items.Count; i++)
            {
                if (lbxGroup.Items[i].Selected)
                {
                    GroupIds += lbxGroup.Items[i].Value + ",";
                }

            }

            if (GroupIds != "")
            {
                DataTable dtEmp = objEmp.GetEmployeeMaster(Session["CompId"].ToString());

                dtEmp = new DataView(dtEmp, "Brand_Id='" + Session["BrandId"].ToString() + "'  and Location_Id='" + Session["LocId"].ToString() + "'", "", DataViewRowState.CurrentRows).ToTable();
                DataTable dtEmpInGroup = objGroupEmp.GetGroup_EmployeeById(Session["CompId"].ToString());

                dtEmpInGroup = new DataView(dtEmpInGroup, "Group_Id in(" + GroupIds.Substring(0, GroupIds.Length - 1) + ")", "", DataViewRowState.CurrentRows).ToTable();

                for (int i = 0; i < dtEmpInGroup.Rows.Count; i++)
                {
                    if (!EmpIds.Split(',').Contains(dtEmpInGroup.Rows[i]["Emp_Id"].ToString()))
                    {
                        EmpIds += dtEmpInGroup.Rows[i]["Emp_Id"].ToString() + ",";
                    }
                }

            }
            else
            {
                DisplayMessage("Select Group First");
            }



            foreach (string str in EmpIds.Split(','))
            {

                if ((str != ""))
                {

                    DataTable dtempmonthtemp = new DataTable();
                    dtempmonthtemp = objPayEmpMonth.Getallrecords(str);
                    dtempmonthtemp = new DataView(dtempmonthtemp, "Emp_Id=" + str + " and Month=" + ddlMonth.SelectedValue.ToString() + " and Year='" + TxtYear.Text + "' ", "", DataViewRowState.CurrentRows).ToTable();

                    if (dtempmonthtemp.Rows.Count > 0)
                    {
                        objPayEmpMonth.Insert_posted_Pay_Emp_Month(Session["CompId"].ToString(), str, dtempmonthtemp.Rows[0]["Month"].ToString(), dtempmonthtemp.Rows[0]["Year"].ToString(), dtempmonthtemp.Rows[0]["Worked_Min_Salary"].ToString(), dtempmonthtemp.Rows[0]["Normal_OT_Min_Salary"].ToString(), dtempmonthtemp.Rows[0]["Week_Off_OT_Min_Salary"].ToString(), dtempmonthtemp.Rows[0]["Holiday_OT_Min_Salary"].ToString(), dtempmonthtemp.Rows[0]["Leave_Days_Salary"].ToString(), dtempmonthtemp.Rows[0]["Week_Off_Salary"].ToString(), dtempmonthtemp.Rows[0]["Holidays_Salary"].ToString(), dtempmonthtemp.Rows[0]["Absent_Salary"].ToString(), dtempmonthtemp.Rows[0]["Late_Min_Penalty"].ToString(), dtempmonthtemp.Rows[0]["Early_Min_Penalty"].ToString(), dtempmonthtemp.Rows[0]["Patial_Violation_Penalty"].ToString(), dtempmonthtemp.Rows[0]["Employee_Penalty"].ToString(), dtempmonthtemp.Rows[0]["Employee_Claim"].ToString(), dtempmonthtemp.Rows[0]["Emlployee_Loan"].ToString(), dtempmonthtemp.Rows[0]["Total_Allowance"].ToString(), dtempmonthtemp.Rows[0]["Total_Deduction"].ToString(), dtempmonthtemp.Rows[0]["Previous_Month_Balance"].ToString(), System.DateTime.Now.ToString(), "0", "0", "0", "0", "0", "0", "0", "0", "0", "0", true.ToString(), Session["UserId"].ToString(), DateTime.Now.ToString(), Session["UserId"].ToString(), DateTime.Now.ToString());

                        DataTable dtemppayrollpost = new DataTable();
                        dtemppayrollpost = objPayEmpMonth.GetAllRecordPostedEmpMonth(str, dtempmonthtemp.Rows[0]["Month"].ToString(), dtempmonthtemp.Rows[0]["Year"].ToString());


                        DataTable dtallowancetemp = new DataTable();
                        dtallowancetemp = objpayrollall.GetPayAllowPayaRoll(str);

                        if (dtallowancetemp.Rows.Count > 0)
                        {
                            objpayrollall.InsertPostPayrollAllowance(str, dtallowancetemp.Rows[0]["Month"].ToString(), dtallowancetemp.Rows[0]["Year"].ToString(), dtemppayrollpost.Rows[0]["Trans_Id"].ToString(), dtallowancetemp.Rows[0]["Allowance_Id"].ToString(), dtallowancetemp.Rows[0]["Allowance_Type"].ToString(), dtallowancetemp.Rows[0]["Allowance_Value"].ToString(), dtallowancetemp.Rows[0]["Act_Allowance_Value"].ToString(), System.DateTime.Now.ToString(), "0", "0", "0", "0", "0", "0", "0", "0", "0", "0", true.ToString(), Session["UserId"].ToString(), DateTime.Now.ToString(), Session["UserId"].ToString(), DateTime.Now.ToString());
                            objpayrollall.DeletePayAllowance(str, dtallowancetemp.Rows[0]["Month"].ToString(), dtallowancetemp.Rows[0]["Year"].ToString());

                        }

                        DataTable dtdeductiontemp = new DataTable();
                        dtdeductiontemp = objpayrolldeduc.GetPayDeducPayaRoll(str);

                        if (dtdeductiontemp.Rows.Count > 0)
                        {
                            objpayrolldeduc.InsertPostPayrollDeduction(str, dtdeductiontemp.Rows[0]["Month"].ToString(), dtdeductiontemp.Rows[0]["Year"].ToString(), dtemppayrollpost.Rows[0]["Trans_Id"].ToString(), dtdeductiontemp.Rows[0]["Deduction_Id"].ToString(), dtdeductiontemp.Rows[0]["Deduction_Type"].ToString(), dtdeductiontemp.Rows[0]["Deduction_Value"].ToString(), dtdeductiontemp.Rows[0]["Act_Deduction_Value"].ToString(), System.DateTime.Now.ToString(), "0", "0", "0", "0", "0", "0", "0", "0", "0", "0", true.ToString(), Session["UserId"].ToString(), DateTime.Now.ToString(), Session["UserId"].ToString(), DateTime.Now.ToString());
                            objpayrolldeduc.DeletePayDeduction(str, dtdeductiontemp.Rows[0]["Month"].ToString(), dtdeductiontemp.Rows[0]["Year"].ToString());

                        }

                        objPayEmpMonth.DeleteEmpMonth(str, dtempmonthtemp.Rows[0]["Month"].ToString(), dtempmonthtemp.Rows[0]["Year"].ToString());
                    }

                    else
                    {
                        // DisplayMessage("You can't payroll posted");

                    }                   
                }
            }
        }

        else
        {
            if (lblSelectRecd.Text == "")
            {
                DisplayMessage("Select employee first");
                return;
            }

            foreach (string str in lblSelectRecd.Text.Split(','))
            {
                if ((str != ""))
                {

                    DataTable dtempmonthtemp = new DataTable();
                    dtempmonthtemp = objPayEmpMonth.Getallrecords(str);
                    dtempmonthtemp = new DataView(dtempmonthtemp, "Emp_Id="+str+" and Month="+ddlMonth.SelectedValue.ToString()+" and Year='"+TxtYear.Text+"' ", "", DataViewRowState.CurrentRows).ToTable();

                    if (dtempmonthtemp.Rows.Count > 0)
                    {
                        objPayEmpMonth.Insert_posted_Pay_Emp_Month(Session["CompId"].ToString(), str, dtempmonthtemp.Rows[0]["Month"].ToString(), dtempmonthtemp.Rows[0]["Year"].ToString(), dtempmonthtemp.Rows[0]["Worked_Min_Salary"].ToString(), dtempmonthtemp.Rows[0]["Normal_OT_Min_Salary"].ToString(), dtempmonthtemp.Rows[0]["Week_Off_OT_Min_Salary"].ToString(), dtempmonthtemp.Rows[0]["Holiday_OT_Min_Salary"].ToString(), dtempmonthtemp.Rows[0]["Leave_Days_Salary"].ToString(), dtempmonthtemp.Rows[0]["Week_Off_Salary"].ToString(), dtempmonthtemp.Rows[0]["Holidays_Salary"].ToString(), dtempmonthtemp.Rows[0]["Absent_Salary"].ToString(), dtempmonthtemp.Rows[0]["Late_Min_Penalty"].ToString(), dtempmonthtemp.Rows[0]["Early_Min_Penalty"].ToString(), dtempmonthtemp.Rows[0]["Patial_Violation_Penalty"].ToString(), dtempmonthtemp.Rows[0]["Employee_Penalty"].ToString(), dtempmonthtemp.Rows[0]["Employee_Claim"].ToString(), dtempmonthtemp.Rows[0]["Emlployee_Loan"].ToString(), dtempmonthtemp.Rows[0]["Total_Allowance"].ToString(), dtempmonthtemp.Rows[0]["Total_Deduction"].ToString(), dtempmonthtemp.Rows[0]["Previous_Month_Balance"].ToString(), System.DateTime.Now.ToString(), "0", "0", "0", "0", "0", "0", "0", "0", "0", "0", true.ToString(), Session["UserId"].ToString(), DateTime.Now.ToString(), Session["UserId"].ToString(), DateTime.Now.ToString());

                        DataTable dtemppayrollpost = new DataTable();
                        dtemppayrollpost = objPayEmpMonth.GetAllRecordPostedEmpMonth(str, dtempmonthtemp.Rows[0]["Month"].ToString(), dtempmonthtemp.Rows[0]["Year"].ToString());


                        DataTable dtallowancetemp = new DataTable();
                        dtallowancetemp = objpayrollall.GetPayAllowPayaRoll(str);

                        if (dtallowancetemp.Rows.Count > 0)
                        {
                            objpayrollall.InsertPostPayrollAllowance(str, dtallowancetemp.Rows[0]["Month"].ToString(), dtallowancetemp.Rows[0]["Year"].ToString(), dtemppayrollpost.Rows[0]["Trans_Id"].ToString(), dtallowancetemp.Rows[0]["Allowance_Id"].ToString(), dtallowancetemp.Rows[0]["Allowance_Type"].ToString(), dtallowancetemp.Rows[0]["Allowance_Value"].ToString(), dtallowancetemp.Rows[0]["Act_Allowance_Value"].ToString(), System.DateTime.Now.ToString(), "0", "0", "0", "0", "0", "0", "0", "0", "0", "0", true.ToString(), Session["UserId"].ToString(), DateTime.Now.ToString(), Session["UserId"].ToString(), DateTime.Now.ToString());
                            objpayrollall.DeletePayAllowance(str, dtallowancetemp.Rows[0]["Month"].ToString(), dtallowancetemp.Rows[0]["Year"].ToString());

                        }

                        DataTable dtdeductiontemp = new DataTable();
                        dtdeductiontemp = objpayrolldeduc.GetPayDeducPayaRoll(str);

                        if (dtdeductiontemp.Rows.Count > 0)
                        {
                            objpayrolldeduc.InsertPostPayrollDeduction(str, dtdeductiontemp.Rows[0]["Month"].ToString(), dtdeductiontemp.Rows[0]["Year"].ToString(), dtemppayrollpost.Rows[0]["Trans_Id"].ToString(), dtdeductiontemp.Rows[0]["Deduction_Id"].ToString(), dtdeductiontemp.Rows[0]["Deduction_Type"].ToString(), dtdeductiontemp.Rows[0]["Deduction_Value"].ToString(), dtdeductiontemp.Rows[0]["Act_Deduction_Value"].ToString(), System.DateTime.Now.ToString(), "0", "0", "0", "0", "0", "0", "0", "0", "0", "0", true.ToString(), Session["UserId"].ToString(), DateTime.Now.ToString(), Session["UserId"].ToString(), DateTime.Now.ToString());
                            objpayrolldeduc.DeletePayDeduction(str, dtdeductiontemp.Rows[0]["Month"].ToString(), dtdeductiontemp.Rows[0]["Year"].ToString());

                        }

                        objPayEmpMonth.DeleteEmpMonth(str, dtempmonthtemp.Rows[0]["Month"].ToString(), dtempmonthtemp.Rows[0]["Year"].ToString());
                    }

                    else
                    {
                       // DisplayMessage("You can't payroll posted");
                    
                    }
                }
            }

           
        }

        DisplayMessage("payroll posted successfully");
            btnLeave_Click(null, null);
        
    }

    public DataTable GetTable()
    {
        DataTable dt = new DataTable();
        dt.Columns.Add(new DataColumn("EmpId"));
        dt.Columns.Add(new DataColumn("EmpName"));
        dt.Columns.Add(new DataColumn("Month"));
        dt.Columns.Add(new DataColumn("Year"));
        dt.Columns.Add(new DataColumn("Type"));
        dt.Columns.Add(new DataColumn("RefId"));
        dt.Columns.Add(new DataColumn("ValueType"));
        dt.Columns.Add(new DataColumn("Value"));

        return dt;
    }
    public DataTable GetEmpAllowDedu(int EmpId)
    {

        DataTable DtAllDeduc = new DataTable();

        // Get Record all Alllowance & Deduction of this Employee

        DataTable dtEmp = new DataTable();
        dtEmp = ObjAllDeduc.GetPayAllowDeducByEmpId(EmpId.ToString());

        return dtEmp;
    }


}
