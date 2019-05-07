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
using DevExpress.XtraReports.UI;

public partial class Temp_Report_GenerateReportTemp : System.Web.UI.Page
{

    #region defind Class Object

    Employee_Pay_Slip_Report objEmpPaySlip = new Employee_Pay_Slip_Report();
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

    protected void btnreports_Click(object sender, EventArgs e)
    {

       
        string[] month = new string[12];
        month[0] = "January";
        month[1] = "february";
        month[2] = "March";
        month[3] = "April";
        month[4] = "May";
        month[5] = "June";
        month[6] = "July";
        month[7] = "August";
        month[8] = "September";
        month[9] = "October";
        month[10] = "Novemeber";
        month[11] = "December";

        DataTable dtreport = new DataTable();
        DataTable dtpaydueemp = new DataTable();
        
        dtpaydueemp.Columns.Add("EmpId");
        dtpaydueemp.Columns.Add("Empname");            
        
        dtpaydueemp.Columns.Add("Type");
        dtpaydueemp.Columns.Add("Amount");
        dtpaydueemp.Columns.Add("Description");
        dtpaydueemp.Columns.Add("Month");   
        dtpaydueemp.Columns.Add("Year");
       
       // dtpaydueemp.Columns.Add(Session["empname"].ToString())
        
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

                foreach (string str in EmpIds.Split(','))
                {
                    string EmployeeName = GetEmployeeName(str);

                    if ((str != ""))
                    {
                        dtreport = objEmpDuePay.GetAllRecord_ByEmpId(str);
                        if (dtreport.Rows.Count > 0)
                        {
                            for (int i = 0; i < dtreport.Rows.Count; i++)
                            {
                                DataRow dr = dtpaydueemp.NewRow();
                                dr[0] = dtreport.Rows[i]["Emp_Id"].ToString();
                                dr[1] = EmployeeName;
                                dr[2] = dtreport.Rows[i]["Type"].ToString();
                                dr[3] = dtreport.Rows[i]["Amount"].ToString();
                                dr[4] = dtreport.Rows[i]["Description"].ToString();
                                dr[5] = month[Convert.ToInt32(dtreport.Rows[i]["Month"].ToString())].ToString();
                                dr[6] = dtreport.Rows[i]["Year"].ToString();

                                dtpaydueemp.Rows.Add(dr);
                            }
                        }


                    }


                }

            }
            else
            {
                DisplayMessage("Select Group First");
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
                    string EmployeeName = GetEmployeeName(str);

                   

                    dtreport = objEmpDuePay.GetAllRecord_ByEmpId(str);

                        for (int i = 0; i < dtreport.Rows.Count; i++)
                        {
                            DataRow dr = dtpaydueemp.NewRow();
                            dr[0] = dtreport.Rows[i]["Emp_Id"].ToString();
                            dr[1] = EmployeeName;

                            if (dtreport.Rows[i]["Type"].ToString() == "1")
                            {
                                dr[2] = "Addition";
                                

                            }
                            else
                            {
                                dr[2] = "Subtraction";
                              
                            }
                            dr[3] = dtreport.Rows[i]["Amount"].ToString();
                            dr[4] = dtreport.Rows[i]["Description"].ToString();
                            dr[5] = month[Convert.ToInt32(dtreport.Rows[i]["Month"].ToString())].ToString();
                            dr[6] = dtreport.Rows[i]["Year"].ToString();
                            
                                                         
                           

                            dtpaydueemp.Rows.Add(dr);
                        }
                    }


                }

            
        }

        if (dtpaydueemp.Rows.Count > 0)
        {

            Session["dtrecords"] = dtpaydueemp;
        }
        else
        {
            DisplayMessage("Record Not found");
        }
        Response.Redirect("../HR_Report/Settlement_payment.aspx");

    }


    protected void btnallowancereport_Click(object sender, EventArgs e)
    {

        string[] month = new string[12];
        month[0] = "January";
        month[1] = "february";
        month[2] = "March";
        month[3] = "April";
        month[4] = "May";
        month[5] = "June";
        month[6] = "July";
        month[7] = "August";
        month[8] = "September";
        month[9] = "October";
        month[10] = "Novemeber";
        month[11] = "December";

        DataTable dtReport = new DataTable();
        DataTable dtEmpAllowance = new DataTable();

        dtEmpAllowance.Columns.Add("EmpId");
        dtEmpAllowance.Columns.Add("EmpName");

        dtEmpAllowance.Columns.Add("ActAmount");
        dtEmpAllowance.Columns.Add("GivenAmount");
        dtEmpAllowance.Columns.Add("Allowance");
        dtEmpAllowance.Columns.Add("Month");
        dtEmpAllowance.Columns.Add("Year");


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

                foreach (string str in EmpIds.Split(','))
                {
                    string EmployeeName = GetEmployeeName(str);

                    if ((str != ""))
                    {
                        dtReport = objpayrollall.GetPostedAllowanceAll(str, ddlMonth.SelectedIndex.ToString(), TxtYear.Text);



                        for (int i = 0; i < dtReport.Rows.Count; i++)
                        {
                            DataRow dr = dtEmpAllowance.NewRow();

                            dr["EmpId"] = dtReport.Rows[i]["Emp_Id"].ToString();
                            dr["EmpName"] = EmployeeName;
                            dr["Month"] = month[Convert.ToInt32(dtReport.Rows[i]["Month"].ToString())].ToString();
                            dr["year"] = dtReport.Rows[i]["Year"].ToString();
                            dr["ActAmount"] = dtReport.Rows[i]["Allowance_Value"].ToString();
                            dr["GivenAmount"] = dtReport.Rows[i]["Act_Allowance_Value"].ToString();
                           
                            if (dtReport.Rows[i]["Allowance_Id"].ToString() != "0" && dtReport.Rows[i]["Allowance_Id"].ToString() != "")
                            {

                                DataTable dtAllowance = ObjAllow.GetAllowanceTruebyId(Session["CompId"].ToString(), dtReport.Rows[i]["Allowance_Id"].ToString());

                                dtAllowance = new DataView(dtAllowance, "Allowance_Id=" + dtReport.Rows[i]["Allowance_Id"].ToString() + " ", "", DataViewRowState.CurrentRows).ToTable();
                                if (dtAllowance.Rows.Count > 0)
                                {
                                    dr["Allowance"] = dtAllowance.Rows[0]["Allowance"].ToString();
                                }
                                else
                                {
                                    dr["Allowance"] = "";
                                }

                            }


                            dtEmpAllowance.Rows.Add(dr);
                        }

                    }

                }
            }
            else
            {
                DisplayMessage("Select Group First");
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
                    string EmployeeName = GetEmployeeName(str);

                    dtReport = objpayrollall.GetPostedAllowanceAll(str, ddlMonth.SelectedIndex.ToString(), TxtYear.Text);
         
                    for (int i = 0; i < dtReport.Rows.Count; i++)
                    {
                        DataRow dr = dtEmpAllowance.NewRow();

                        dr["EmpId"] = dtReport.Rows[i]["Emp_Id"].ToString();
                        dr["EmpName"] = EmployeeName;
                        dr["Month"] = month[Convert.ToInt32(dtReport.Rows[i]["Month"].ToString())].ToString();
                        dr["year"]=dtReport.Rows[i]["Year"].ToString();
                        dr["ActAmount"] = dtReport.Rows[i]["Allowance_Value"].ToString();
                        dr["GivenAmount"] = dtReport.Rows[i]["Act_Allowance_Value"].ToString();
                       

                        if (dtReport.Rows[i]["Allowance_Id"].ToString() != "0" && dtReport.Rows[i]["Allowance_Id"].ToString() != "")
                        {

                            DataTable dtAllowance = ObjAllow.GetAllowanceTruebyId(Session["CompId"].ToString(), dtReport.Rows[i]["Allowance_Id"].ToString());

                            dtAllowance = new DataView(dtAllowance, "Allowance_Id=" + dtReport.Rows[i]["Allowance_Id"].ToString() + " ", "", DataViewRowState.CurrentRows).ToTable();
                            if (dtAllowance.Rows.Count > 0)
                            {
                                dr["Allowance"] = dtAllowance.Rows[0]["Allowance"].ToString();
                            }
                            else
                            {
                                dr["Allowance"] = "";
                            }

                        }


                        dtEmpAllowance.Rows.Add(dr);
                    }
                }


            }


        }

        if (dtEmpAllowance.Rows.Count > 0)
        {

            Session["dtRecordEmp"] = dtEmpAllowance;
        }
        else
        {
            DisplayMessage("Record Not found");
        }
        Response.Redirect("../HR_Report/Employee_Allowance_Report.aspx");


    }


    protected void btndeductionreport_Click(object sender, EventArgs e)
    {

        string[] month = new string[12];
        month[0] = "January";
        month[1] = "february";
        month[2] = "March";
        month[3] = "April";
        month[4] = "May";
        month[5] = "June";
        month[6] = "July";
        month[7] = "August";
        month[8] = "September";
        month[9] = "October";
        month[10] = "Novemeber";
        month[11] = "December";

        DataTable dtReportDeduc = new DataTable();
        DataTable dtEmpDeduction = new DataTable();

        dtEmpDeduction.Columns.Add("EmpId");
        dtEmpDeduction.Columns.Add("EmpName");

        dtEmpDeduction.Columns.Add("ActAmount");
        dtEmpDeduction.Columns.Add("GivenAmount");
        dtEmpDeduction.Columns.Add("Deduction");
        dtEmpDeduction.Columns.Add("Month");
        dtEmpDeduction.Columns.Add("Year");


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

                foreach (string str in EmpIds.Split(','))
                {
                    string EmployeeName = GetEmployeeName(str);

                    if ((str != ""))
                    {
                        dtReportDeduc = objpayrolldeduc.GetRecordPostedDeductionAll(str, ddlMonth.SelectedIndex.ToString(), TxtYear.Text);



                        for (int i = 0; i < dtReportDeduc.Rows.Count; i++)
                        {
                            DataRow dr = dtEmpDeduction.NewRow();

                            dr["EmpId"] = dtReportDeduc.Rows[i]["Emp_Id"].ToString();
                            dr["EmpName"] = EmployeeName;
                            dr["Month"] = month[Convert.ToInt32(dtReportDeduc.Rows[i]["Month"].ToString())].ToString();
                            dr["year"] = dtReportDeduc.Rows[i]["Year"].ToString();
                            dr["ActAmount"] = dtReportDeduc.Rows[i]["Deduction_Value"].ToString();
                            dr["GivenAmount"] = dtReportDeduc.Rows[i]["Act_Deduction_Value"].ToString();

                            if (dtReportDeduc.Rows[i]["Deduction_Id"].ToString() != "0" && dtReportDeduc.Rows[i]["Deduction_Id"].ToString() != "")
                            {
                                DataTable dtDeduction = objDeduction.GetDeductionTruebyId(Session["CompId"].ToString(), dtReportDeduc.Rows[i]["Deduction_Id"].ToString());

                                dtDeduction = new DataView(dtDeduction, "Deduction_Id=" + dtReportDeduc.Rows[i]["Deduction_Id"].ToString() + " ", "", DataViewRowState.CurrentRows).ToTable();
                                if (dtDeduction.Rows.Count > 0)
                                {
                                    dr["Deduction"] = dtDeduction.Rows[0]["Deduction"].ToString();
                                }
                                else
                                {
                                    dr["Deduction"] = "";
                                }

                            }


                            dtEmpDeduction.Rows.Add(dr);
                        }

                    }

                }
            }
            else
            {
                DisplayMessage("Select Group First");
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
                    string EmployeeName = GetEmployeeName(str);

                    dtReportDeduc = objpayrolldeduc.GetRecordPostedDeductionAll(str, ddlMonth.SelectedIndex.ToString(), TxtYear.Text);



                    for (int i = 0; i < dtReportDeduc.Rows.Count; i++)
                    {
                        DataRow dr = dtEmpDeduction.NewRow();

                        dr["EmpId"] = dtReportDeduc.Rows[i]["Emp_Id"].ToString();
                        dr["EmpName"] = EmployeeName;
                        dr["Month"] = month[Convert.ToInt32(dtReportDeduc.Rows[i]["Month"].ToString())].ToString();
                        dr["year"] = dtReportDeduc.Rows[i]["Year"].ToString();
                        dr["ActAmount"] = dtReportDeduc.Rows[i]["Deduction_Value"].ToString();
                        dr["GivenAmount"] = dtReportDeduc.Rows[i]["Act_Deduction_Value"].ToString();

                        if (dtReportDeduc.Rows[i]["Deduction_Id"].ToString() != "0" && dtReportDeduc.Rows[i]["Deduction_Id"].ToString() != "")
                        {
                            DataTable dtDeduction = objDeduction.GetDeductionTruebyId(Session["CompId"].ToString(), dtReportDeduc.Rows[i]["Deduction_Id"].ToString());

                            dtDeduction = new DataView(dtDeduction, "Deduction_Id=" + dtReportDeduc.Rows[i]["Deduction_Id"].ToString() + " ", "", DataViewRowState.CurrentRows).ToTable();
                            if (dtDeduction.Rows.Count > 0)
                            {
                                dr["Deduction"] = dtDeduction.Rows[0]["Deduction"].ToString();
                            }
                            else
                            {
                                dr["Deduction"] = "";
                            }

                        }


                        dtEmpDeduction.Rows.Add(dr);
                    }
                }


            }


        }

        if (dtEmpDeduction.Rows.Count > 0)
        {

            Session["dtRecordDeduc"] = dtEmpDeduction;
        }
        else
        {
            DisplayMessage("Record Not found");
        }
        Response.Redirect("../HR_Report/Employee_Deduction_Report.aspx");


    }

    protected void btnByAllowance_Click(object sender, EventArgs e)
    {

        string[] month = new string[12];
        month[0] = "January";
        month[1] = "february";
        month[2] = "March";
        month[3] = "April";
        month[4] = "May";
        month[5] = "June";
        month[6] = "July";
        month[7] = "August";
        month[8] = "September";
        month[9] = "October";
        month[10] = "Novemeber";
        month[11] = "December";

        DataTable dtReportByAllow = new DataTable();
        DataTable dtEmpByAllowance = new DataTable();

        dtEmpByAllowance.Columns.Add("EmpId");
        dtEmpByAllowance.Columns.Add("EmpName");

        dtEmpByAllowance.Columns.Add("AllowanceId");
        dtEmpByAllowance.Columns.Add("Allowance");
        dtEmpByAllowance.Columns.Add("ActAmount");
        dtEmpByAllowance.Columns.Add("GivenAmount");
        dtEmpByAllowance.Columns.Add("Month");
        dtEmpByAllowance.Columns.Add("Year");


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

                foreach (string str in EmpIds.Split(','))
                {
                    string EmployeeName = GetEmployeeName(str);

                    if ((str != ""))
                    {
                        dtReportByAllow = objpayrollall.GetPostedAllowanceAll(str, ddlMonth.SelectedIndex.ToString(), TxtYear.Text);



                        for (int i = 0; i < dtReportByAllow.Rows.Count; i++)
                        {
                            DataRow dr = dtEmpByAllowance.NewRow();

                            dr["EmpId"] = dtReportByAllow.Rows[i]["Emp_Id"].ToString();
                            dr["EmpName"] = EmployeeName;
                            dr["Month"] = month[Convert.ToInt32(dtReportByAllow.Rows[i]["Month"].ToString())].ToString();
                            dr["year"] = dtReportByAllow.Rows[i]["Year"].ToString();
                            dr["ActAmount"] = dtReportByAllow.Rows[i]["Allowance_Value"].ToString();
                            dr["GivenAmount"] = dtReportByAllow.Rows[i]["Act_Allowance_Value"].ToString();

                            dr["AllowanceId"] = dtReportByAllow.Rows[i]["Allowance_Id"].ToString();

                            if (dtReportByAllow.Rows[i]["Allowance_Id"].ToString() != "0" && dtReportByAllow.Rows[i]["Allowance_Id"].ToString() != "")
                            {

                                DataTable dtAllowance = ObjAllow.GetAllowanceTruebyId(Session["CompId"].ToString(), dtReportByAllow.Rows[i]["Allowance_Id"].ToString());

                                dtAllowance = new DataView(dtAllowance, "Allowance_Id=" + dtReportByAllow.Rows[i]["Allowance_Id"].ToString() + " ", "", DataViewRowState.CurrentRows).ToTable();
                                if (dtAllowance.Rows.Count > 0)
                                {
                                    dr["Allowance"] = dtAllowance.Rows[0]["Allowance"].ToString();
                                }
                                else
                                {
                                    dr["Allowance"] = "";
                                }

                            }


                            dtEmpByAllowance.Rows.Add(dr);
                        }

                    }

                }
            }
            else
            {
                DisplayMessage("Select Group First");
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
                    string EmployeeName = GetEmployeeName(str);

                    dtReportByAllow = objpayrollall.GetPostedAllowanceAll(str, ddlMonth.SelectedIndex.ToString(), TxtYear.Text);
         
                    for (int i = 0; i < dtReportByAllow.Rows.Count; i++)
                    {
                        DataRow dr = dtEmpByAllowance.NewRow();

                        dr["EmpId"] = dtReportByAllow.Rows[i]["Emp_Id"].ToString();
                        dr["EmpName"] = EmployeeName;
                        dr["Month"] = month[Convert.ToInt32(dtReportByAllow.Rows[i]["Month"].ToString())].ToString();
                        dr["year"]=dtReportByAllow.Rows[i]["Year"].ToString();
                        dr["ActAmount"] = dtReportByAllow.Rows[i]["Allowance_Value"].ToString();
                        dr["GivenAmount"] = dtReportByAllow.Rows[i]["Act_Allowance_Value"].ToString();
                        dr["AllowanceId"] = dtReportByAllow.Rows[i]["Allowance_Id"].ToString();

                        if (dtReportByAllow.Rows[i]["Allowance_Id"].ToString() != "0" && dtReportByAllow.Rows[i]["Allowance_Id"].ToString() != "")
                        {

                            DataTable dtAllowance = ObjAllow.GetAllowanceTruebyId(Session["CompId"].ToString(), dtReportByAllow.Rows[i]["Allowance_Id"].ToString());

                            dtAllowance = new DataView(dtAllowance, "Allowance_Id=" + dtReportByAllow.Rows[i]["Allowance_Id"].ToString() + " ", "", DataViewRowState.CurrentRows).ToTable();
                            if (dtAllowance.Rows.Count > 0)
                            {
                                dr["Allowance"] = dtAllowance.Rows[0]["Allowance"].ToString();
                            }
                            else
                            {
                                dr["Allowance"] = "";
                            }

                        }


                        dtEmpByAllowance.Rows.Add(dr);
                    }
                }


            }


        }

        if (dtEmpByAllowance.Rows.Count > 0)
        {

            Session["dtRecordByAllow"] = dtEmpByAllowance;
        }
        else
        {
            DisplayMessage("Record Not found");
        }
        Response.Redirect("../HR_Report/Employee_ByAllowance_Report.aspx");


    }

    protected void btndeuctionbydeduction_Click(object sender, EventArgs e)
    {

        string[] month = new string[12];
        month[0] = "January";
        month[1] = "february";
        month[2] = "March";
        month[3] = "April";
        month[4] = "May";
        month[5] = "June";
        month[6] = "July";
        month[7] = "August";
        month[8] = "September";
        month[9] = "October";
        month[10] = "Novemeber";
        month[11] = "December";

        DataTable dtReportByDeduc = new DataTable();
        DataTable dtEmpByDeduction = new DataTable();

        dtEmpByDeduction.Columns.Add("EmpId");
        dtEmpByDeduction.Columns.Add("EmpName");

        dtEmpByDeduction.Columns.Add("ActAmount");
        dtEmpByDeduction.Columns.Add("GivenAmount");
        dtEmpByDeduction.Columns.Add("Deduction");
        dtEmpByDeduction.Columns.Add("DeductionId");
        dtEmpByDeduction.Columns.Add("Month");
        dtEmpByDeduction.Columns.Add("Year");


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

                foreach (string str in EmpIds.Split(','))
                {
                    string EmployeeName = GetEmployeeName(str);

                    if ((str != ""))
                    {
                        dtReportByDeduc = objpayrolldeduc.GetRecordPostedDeductionAll(str, ddlMonth.SelectedIndex.ToString(), TxtYear.Text);


                        for (int i = 0; i < dtReportByDeduc.Rows.Count; i++)
                        {
                            DataRow dr = dtEmpByDeduction.NewRow();

                            dr["EmpId"] = dtReportByDeduc.Rows[i]["Emp_Id"].ToString();
                            dr["EmpName"] = EmployeeName;
                            dr["Month"] = month[Convert.ToInt32(dtReportByDeduc.Rows[i]["Month"].ToString())].ToString();
                            dr["year"] = dtReportByDeduc.Rows[i]["Year"].ToString();
                            dr["ActAmount"] = dtReportByDeduc.Rows[i]["Deduction_Value"].ToString();
                            dr["GivenAmount"] = dtReportByDeduc.Rows[i]["Act_Deduction_Value"].ToString();
                            dr["DeductionId"] = dtReportByDeduc.Rows[i]["Deduction_Id"].ToString();

                            if (dtReportByDeduc.Rows[i]["Deduction_Id"].ToString() != "0" && dtReportByDeduc.Rows[i]["Deduction_Id"].ToString() != "")
                            {
                                DataTable dtDeduction = objDeduction.GetDeductionTruebyId(Session["CompId"].ToString(), dtReportByDeduc.Rows[i]["Deduction_Id"].ToString());

                                dtDeduction = new DataView(dtDeduction, "Deduction_Id=" + dtReportByDeduc.Rows[i]["Deduction_Id"].ToString() + " ", "", DataViewRowState.CurrentRows).ToTable();
                                if (dtDeduction.Rows.Count > 0)
                                {
                                    dr["Deduction"] = dtDeduction.Rows[0]["Deduction"].ToString();
                                }
                                else
                                {
                                    dr["Deduction"] = "";
                                }

                            }


                            dtEmpByDeduction.Rows.Add(dr);
                        }

                    }

                }
            }
            else
            {
                DisplayMessage("Select Group First");
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
                    string EmployeeName = GetEmployeeName(str);

                    dtReportByDeduc = objpayrolldeduc.GetRecordPostedDeductionAll(str, ddlMonth.SelectedIndex.ToString(), TxtYear.Text);



                    for (int i = 0; i < dtReportByDeduc.Rows.Count; i++)
                    {
                        DataRow dr = dtEmpByDeduction.NewRow();

                        dr["EmpId"] = dtReportByDeduc.Rows[i]["Emp_Id"].ToString();
                        dr["EmpName"] = EmployeeName;
                        dr["Month"] = month[Convert.ToInt32(dtReportByDeduc.Rows[i]["Month"].ToString())].ToString();
                        dr["year"] = dtReportByDeduc.Rows[i]["Year"].ToString();
                        dr["ActAmount"] = dtReportByDeduc.Rows[i]["Deduction_Value"].ToString();
                        dr["GivenAmount"] = dtReportByDeduc.Rows[i]["Act_Deduction_Value"].ToString();
                        dr["DeductionId"] = dtReportByDeduc.Rows[i]["Deduction_Id"].ToString();
                        
                        
                        if (dtReportByDeduc.Rows[i]["Deduction_Id"].ToString() != "0" && dtReportByDeduc.Rows[i]["Deduction_Id"].ToString() != "")
                        {
                            DataTable dtDeduction = objDeduction.GetDeductionTruebyId(Session["CompId"].ToString(), dtReportByDeduc.Rows[i]["Deduction_Id"].ToString());

                            dtDeduction = new DataView(dtDeduction, "Deduction_Id=" + dtReportByDeduc.Rows[i]["Deduction_Id"].ToString() + " ", "", DataViewRowState.CurrentRows).ToTable();
                            if (dtDeduction.Rows.Count > 0)
                            {
                                dr["Deduction"] = dtDeduction.Rows[0]["Deduction"].ToString();
                            }
                            else
                            {
                                dr["Deduction"] = "";
                            }

                        }


                        dtEmpByDeduction.Rows.Add(dr);
                    }
                }


            }


        }

        if (dtEmpByDeduction.Rows.Count > 0)
        {

            Session["dtRecordByDeduc"] = dtEmpByDeduction;
        }
        else
        {
            DisplayMessage("Record Not found");
        }
        Response.Redirect("../HR_Report/Employee_DeductionByDeduction.aspx");


    }

   

    protected void btnnonsettlereport_Click(object sender, EventArgs e)
   
    {

       
        string[] month = new string[12];
        month[0] = "January";
        month[1] = "february";
        month[2] = "March";
        month[3] = "April";
        month[4] = "May";
        month[5] = "June";
        month[6] = "July";
        month[7] = "August";
        month[8] = "September";
        month[9] = "October";
        month[10] = "Novemeber";
        month[11] = "December";

        DataTable dtDuePayreport = new DataTable();
        DataTable dtPayEmpFalse = new DataTable();
        
        dtPayEmpFalse.Columns.Add("EmpId");
        dtPayEmpFalse.Columns.Add("Empname");            
        
        dtPayEmpFalse.Columns.Add("Type");
        dtPayEmpFalse.Columns.Add("Amount");
        dtPayEmpFalse.Columns.Add("Description");
        dtPayEmpFalse.Columns.Add("Month");   
        dtPayEmpFalse.Columns.Add("Year");
       
       // dtPayEmpFalse.Columns.Add(Session["empname"].ToString())
        
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

                foreach (string str in EmpIds.Split(','))
                {
                    string EmployeeName = GetEmployeeName(str);

                    if ((str != ""))
                    {
                        dtDuePayreport = objEmpDuePay.GetAllfalseRecord_ByEmpId(str);
                        if (dtDuePayreport.Rows.Count > 0)
                        {
                            for (int i = 0; i < dtDuePayreport.Rows.Count; i++)
                            {
                                DataRow dr = dtPayEmpFalse.NewRow();
                                dr[0] = dtDuePayreport.Rows[i]["Emp_Id"].ToString();
                                dr[1] = EmployeeName;
                                dr[2] = dtDuePayreport.Rows[i]["Type"].ToString();
                                dr[3] = dtDuePayreport.Rows[i]["Amount"].ToString();
                                dr[4] = dtDuePayreport.Rows[i]["Description"].ToString();
                                dr[5] = month[Convert.ToInt32(dtDuePayreport.Rows[i]["Month"].ToString())].ToString();
                                dr[6] = dtDuePayreport.Rows[i]["Year"].ToString();

                                dtPayEmpFalse.Rows.Add(dr);
                            }
                        }


                    }


                }

            }
            else
            {
                DisplayMessage("Select Group First");
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
                    string EmployeeName = GetEmployeeName(str);

                    dtDuePayreport = objEmpDuePay.GetAllfalseRecord_ByEmpId(str);

                        for (int i = 0; i < dtDuePayreport.Rows.Count; i++)
                        {
                            DataRow dr = dtPayEmpFalse.NewRow();
                            dr[0] = dtDuePayreport.Rows[i]["Emp_Id"].ToString();
                            dr[1] = EmployeeName;

                            if (dtDuePayreport.Rows[i]["Type"].ToString() == "1")
                            {
                                dr[2] = "Addition";
                                

                            }
                            else
                            {
                                dr[2] = "Subtraction";
                              
                            }
                            dr[3] = dtDuePayreport.Rows[i]["Amount"].ToString();
                            dr[4] = dtDuePayreport.Rows[i]["Description"].ToString();
                            dr[5] = month[Convert.ToInt32(dtDuePayreport.Rows[i]["Month"].ToString())].ToString();
                            dr[6] = dtDuePayreport.Rows[i]["Year"].ToString();
                       
                            dtPayEmpFalse.Rows.Add(dr);
                        }
                    }


                }           
        }

        if (dtPayEmpFalse.Rows.Count > 0)
        {

            Session["dtFalseRecords"] = dtPayEmpFalse;
        }
        else
        {
            DisplayMessage("Record Not found");
        }
        Response.Redirect("../HR_Report/Employee_DuePayement.aspx");

    }

    protected void btnemppayslip_Click(object sender, EventArgs e)
    {
        double sumallowance = 0;
        double sumdeduction = 0;
        double netsalary = 0;
        double sumloan = 0;
        double sumclaim = 0;
        double sumpenalty = 0;
        double totalAllowClaim = 0;
        double totalDeducPenaLoan = 0;
        double basicsal = 0;
        double prvmonthbal = 0;
        double totaldays = 0;

        double totalattndsal = 0;
        double totalOTsal = 0;
        double totalpenasal = 0;
        double grossattendance = 0;
        string doj = "";
        string EmployeeName = "";
        string EmployeeCode = "";
        string[] month = new string[13];
        month[0] = "--Select--";
        month[1] = "JAN'";
        month[2] = "FEB'";
        month[3] = "MAR'";
        month[4] = "APR'";
        month[5] = "MAY'";
        month[6] = "JUN'";
        month[7] = "JUL'";
        month[8] = "AUG'";
        month[9] = "SEP'";
        month[10] = "OCT'";
        month[11] = "NOV'";
        month[12] = "DEC'";

        DataTable dtEmployeePaySlip = new DataTable();
        DataTable dtEmployeePayRecord = new DataTable();

        dtEmployeePaySlip.Columns.Add("EmpCode");
        dtEmployeePaySlip.Columns.Add("EmpName");

        dtEmployeePaySlip.Columns.Add("Designation");
        dtEmployeePaySlip.Columns.Add("Department");
        dtEmployeePaySlip.Columns.Add("DOJ");
        dtEmployeePaySlip.Columns.Add("Month");
        dtEmployeePaySlip.Columns.Add("Year");
        dtEmployeePaySlip.Columns.Add("BankAccountNo");
        dtEmployeePaySlip.Columns.Add("AllowanceName");
        dtEmployeePaySlip.Columns.Add("AllowanceActAmt");
        dtEmployeePaySlip.Columns.Add("NetSalary");
        dtEmployeePaySlip.Columns.Add("TypeAllow");

        dtEmployeePaySlip.Columns.Add("DaysPresent");
        dtEmployeePaySlip.Columns.Add("WeekOff");
        dtEmployeePaySlip.Columns.Add("daysAbsent");
        dtEmployeePaySlip.Columns.Add("Holiday");
        dtEmployeePaySlip.Columns.Add("Leaves");
        dtEmployeePaySlip.Columns.Add("Totaldays");
        
        //Attendance Salary
        dtEmployeePaySlip.Columns.Add("WorkedSal");
        dtEmployeePaySlip.Columns.Add("WeekOffSal");
        dtEmployeePaySlip.Columns.Add("HolidaysSal");
        dtEmployeePaySlip.Columns.Add("LeavedaysSal");
        //Overtime Days
        //dtEmployeePaySlip.Columns.Add("NormalOT");
        //dtEmployeePaySlip.Columns.Add("WeekOT");
        //dtEmployeePaySlip.Columns.Add("HolidayOT");
        //Overtime Salary
        dtEmployeePaySlip.Columns.Add("NormalOTSal");
        dtEmployeePaySlip.Columns.Add("WeekOffOTSal");
        dtEmployeePaySlip.Columns.Add("HolidaysOTSal");
        //Penalty Days/Min
        //dtEmployeePaySlip.Columns.Add("LatePenalty");
        //dtEmployeePaySlip.Columns.Add("EarlyPenalty");
        //dtEmployeePaySlip.Columns.Add("PartialPenalty");
        //dtEmployeePaySlip.Columns.Add("AbsentPenalty");
        //Penalty Sal
        dtEmployeePaySlip.Columns.Add("LatePenaSal");
        dtEmployeePaySlip.Columns.Add("EarlyPenaSal");
        dtEmployeePaySlip.Columns.Add("PartialPenaSal");
        dtEmployeePaySlip.Columns.Add("AbsentPenaSal");
        // all Total Amount 

        //dtEmployeePaySlip.Columns.Add("ClaimName");
        //dtEmployeePaySlip.Columns.Add("PenaltyName");
        //dtEmployeePaySlip.Columns.Add("LoanName");

        //dtEmployeePaySlip.Columns.Add("TotalEmpPenalty");
        //dtEmployeePaySlip.Columns.Add("TotalEmpClaim");
        //dtEmployeePaySlip.Columns.Add("TotalEmpLoan");
      
        //dtEmployeePaySlip.Columns.Add("");
        //dtEmployeePaySlip.Columns.Add("TotalOTSal");
        //dtEmployeePaySlip.Columns.Add("TotalPenaltySal");
        
        //Total and gross amount attendance salary and penalty
        dtEmployeePaySlip.Columns.Add("GrossAttendanceSal");
        dtEmployeePaySlip.Columns.Add("totalAttendSal");
        dtEmployeePaySlip.Columns.Add("TotalOTSal");
        dtEmployeePaySlip.Columns.Add("TotalPenaltySal"); 
        
        // Total Adjustment amount
        dtEmployeePaySlip.Columns.Add("TotalAdjustmentAmt");


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

                foreach (string str in EmpIds.Split(','))
                {
                    

                    if ((str != ""))
                    {
                        DataTable dtemppara = new DataTable();
                        dtemppara = objempparam.GetEmployeeParameterByEmpId(str, Session["CompId"].ToString());
                        if (dtemppara.Rows.Count > 0)
                        {

                            basicsal = Convert.ToDouble(dtemppara.Rows[0]["Basic_Salary"].ToString());
                        }


                        EmployeeName = GetEmployeeName(str);
                        EmployeeCode = GetEmployeeCode(str);

                        sumallowance = 0;
                        sumdeduction = 0;
                        netsalary = 0;
                        sumclaim = 0;
                        sumpenalty = 0;
                        sumloan = 0;
                        totalAllowClaim = 0;
                        totalDeducPenaLoan = 0;
                        prvmonthbal = 0;
                        totaldays = 0;
                        grossattendance = 0;


                        DataTable dtAllowance = new DataTable();
                        DataTable dtDeuction = new DataTable();
                        DataTable dtEmpInfo = new DataTable();
                        DataTable dtDepartment = new DataTable();
                        DataTable dtDesignation = new DataTable();
                        DataTable dtLoan = new DataTable();
                        DataTable dtPenalty = new DataTable();
                        DataTable dtClaim = new DataTable();
                        DataTable dtPayEmpMonth = new DataTable();
                        DataTable dtEmpAttendance = new DataTable();
                        DataTable dtPrvMonthBal = new DataTable();

                        dtEmpInfo = objEmp.GetEmployeeMasterByEmpCode(Session["CompId"].ToString(),
                            EmployeeCode.ToString());
                        dtDepartment = objDep.GetDepartmentMasterById(Session["CompId"].ToString(),
                            dtEmpInfo.Rows[0]["Department_Id"].ToString());
                        dtDesignation = objDesg.GetDesignationMasterById(
                            Session["CompId"].ToString(), dtEmpInfo.Rows[0]["Designation_Id"].ToString());
                        dtAllowance = objpayrollall.GetPostedAllowanceAll(str,
                            ddlMonth.SelectedIndex.ToString(), TxtYear.Text);

                        dtDeuction = objpayrolldeduc.GetRecordPostedDeductionAll(str, ddlMonth.SelectedValue.ToString(), TxtYear.Text);

                        dtPayEmpMonth = objPayEmpMonth.Getallrecords(str);
                        dtPayEmpMonth = new DataView(dtPayEmpMonth,
                            "Emp_Id=" + str + " and Month=" + ddlMonth.SelectedValue.ToString() +
                            " and Year='" + TxtYear.Text + "'", "", DataViewRowState.CurrentRows).ToTable();


                        dtPenalty = objPEpenalty.GetRecord_From_PayEmployeePenalty(Session["CompId"].ToString(), str, "0", ddlMonth.SelectedValue.ToString(), TxtYear.Text, "", "");
                        dtPenalty = new DataView(dtPenalty, "Emp_Id=" + str + " and Penalty_Month=" + ddlMonth.SelectedValue.ToString() + " and Penalty_Year='" + TxtYear.Text + "' ", "", DataViewRowState.CurrentRows).ToTable();

                        dtClaim = objPEClaim.GetRecord_From_PayEmployeeClaim(Session["CompId"].ToString(), str, "0", ddlMonth.SelectedValue.ToString(), TxtYear.Text, "Approved", "", "");
                        dtClaim = new DataView(dtClaim, "Emp_Id=" + str + " and Claim_Month=" + ddlMonth.SelectedValue.ToString() + " and Claim_Year='" + TxtYear.Text + "'", "", DataViewRowState.CurrentRows).ToTable();

                        dtLoan = objEmpLoan.GetRecord_From_PayEmployeeLoan(Session["CompId"].ToString(), "0", "Running");
                        //dtLoan = new DataView(dtLoan, "Emp_Id=" + str + " and Month=" + ddlMonth.SelectedValue.ToString() + " and Year='" + TxtYear.Text + "'", "", DataViewRowState.CurrentRows).ToTable();

                        dtEmpAttendance = objEmpAttendance.GetRecord_Emp_Attendance(str, ddlMonth.SelectedValue.ToString(), TxtYear.Text);



                        for (int i = 0; i < dtEmpInfo.Rows.Count; i++)
                        {
                            if (dtEmpAttendance.Rows.Count > 0)
                            {

                                DataRow dr = dtEmployeePaySlip.NewRow();
                                dr["TypeAllow"] = "";

                                //Attendance Days
                                dr["DaysPresent"] = dtEmpAttendance.Rows[0]["Worked_Days"].ToString();
                                dr["WeekOff"] = dtEmpAttendance.Rows[0]["Week_Off_Days"].ToString();
                                dr["daysAbsent"] = dtEmpAttendance.Rows[0]["Absent_Days"].ToString();
                                dr["Holiday"] = dtEmpAttendance.Rows[0]["Holiday_Days"].ToString();
                                dr["Leaves"] = dtEmpAttendance.Rows[0]["Leave_Days"].ToString();

                                double dayspres = Convert.ToDouble(dtEmpAttendance.Rows[0]["Worked_Days"].ToString());
                                double weekofdays = Convert.ToDouble(dtEmpAttendance.Rows[0]["Week_Off_Days"].ToString());
                                double daysabsent = Convert.ToDouble(dtEmpAttendance.Rows[0]["Absent_Days"].ToString());
                                double holidays = Convert.ToDouble(dtEmpAttendance.Rows[0]["Holiday_Days"].ToString());
                                double daysleave = Convert.ToDouble(dtEmpAttendance.Rows[0]["Leave_Days"].ToString());
                                totaldays = dayspres + weekofdays + daysabsent + daysleave + holidays;
                                dr["Totaldays"] = totaldays.ToString();

                                //Attendance Salary
                                dr["WorkedSal"] = dtEmpAttendance.Rows[0]["Basic_Work_Salary"].ToString();
                                dr["LeavedaysSal"] = dtEmpAttendance.Rows[0]["Leave_Days_Salary"].ToString();
                                dr["HolidaysSal"] = dtEmpAttendance.Rows[0]["Holiday_Days_Salary"].ToString();
                                dr["WeekOffSal"] = dtEmpAttendance.Rows[0]["Week_Off_Days_Salary"].ToString();

                                double worksal = Convert.ToDouble(dtEmpAttendance.Rows[0]["Basic_Work_Salary"].ToString());
                                double leavesal = Convert.ToDouble(dtEmpAttendance.Rows[0]["Leave_Days_Salary"].ToString());
                                double holidayssal = Convert.ToDouble(dtEmpAttendance.Rows[0]["Holiday_Days_Salary"].ToString());
                                double weekofsal = Convert.ToDouble(dtEmpAttendance.Rows[0]["Week_Off_Days_Salary"].ToString());

                                totalattndsal = worksal + leavesal + holidayssal + weekofsal;
                                dr["totalAttendSal"] = totalattndsal.ToString();

                                dr["NormalOTSal"] = dtEmpAttendance.Rows[0]["Normal_OT_Work_Salary"].ToString();
                                dr["WeekOffOTSal"] = dtEmpAttendance.Rows[0]["WeekOff_OT_Work_Salary"].ToString();
                                dr["HolidaysOTSal"] = dtEmpAttendance.Rows[0]["Holiday_OT_Work_Salary"].ToString();

                                double noramlotsal = Convert.ToDouble(dtEmpAttendance.Rows[0]["Normal_OT_Work_Salary"].ToString());
                                double weekotsal = Convert.ToDouble(dtEmpAttendance.Rows[0]["WeekOff_OT_Work_Salary"].ToString());
                                double holidaotsal = Convert.ToDouble(dtEmpAttendance.Rows[0]["Holiday_OT_Work_Salary"].ToString());
                                totalOTsal = noramlotsal + weekotsal + holidaotsal;
                                dr["TotalOTSal"] = totalOTsal.ToString();


                                dr["LatePenaSal"] = dtEmpAttendance.Rows[0]["Late_Min_Penalty"].ToString();
                                dr["EarlyPenaSal"] = dtEmpAttendance.Rows[0]["Early_Min_Penalty"].ToString();
                                dr["PartialPenaSal"] = dtEmpAttendance.Rows[0]["Parital_Violation_Penalty"].ToString();
                                dr["AbsentPenaSal"] = dtEmpAttendance.Rows[0]["Absent_Day_Penalty"].ToString();

                                double latesal = Convert.ToDouble(dtEmpAttendance.Rows[0]["Late_Min_Penalty"].ToString());
                                double earlysal = Convert.ToDouble(dtEmpAttendance.Rows[0]["Early_Min_Penalty"].ToString());
                                double partialsal = Convert.ToDouble(dtEmpAttendance.Rows[0]["Parital_Violation_Penalty"].ToString());
                                double absentsal = Convert.ToDouble(dtEmpAttendance.Rows[0]["Absent_Day_Penalty"].ToString());

                                totalpenasal = latesal + earlysal + partialsal + absentsal;
                                dr["TotalPenaltySal"] = totalpenasal.ToString();

                                grossattendance = (totalattndsal + totalOTsal) - totalpenasal;

                                dr["GrossAttendanceSal"] = grossattendance.ToString();



                                dr["EmpCode"] = EmployeeCode;
                                dr["EmpName"] = EmployeeName;
                                DateTime dt = Convert.ToDateTime(dtEmpInfo.Rows[0]["DOJ"].ToString());
                                dr["DOJ"] = dt.ToString("dd-MMM-yyyy");


                                if (dtDesignation.Rows.Count > 0)
                                {
                                    dr["Designation"] = dtDesignation.Rows[0]["Designation"].ToString();
                                }
                                if (dtDepartment.Rows.Count > 0)
                                {
                                    dr["Department"] = dtDepartment.Rows[0]["Dep_Name"].ToString();
                                }
                                dr["Month"] = month[Convert.ToInt32(ddlMonth.SelectedIndex.ToString())].ToString();
                                dr["Year"] = TxtYear.Text.ToString();
                                dr["BankAccountNo"] = dtEmpInfo.Rows[0]["Account_No"].ToString();

                                //totalAllowClaim = sumclaim + sumallowance + grossattendance;  

                                //totalDeducPenaLoan = sumdeduction + sumpenalty + sumloan;

                                //netsalary = totalAllowClaim - totalDeducPenaLoan + (prvmonthbal);



                                //dr["NetSalary"] = netsalary.ToString();

                                dtEmployeePaySlip.Rows.Add(dr);


                            }
                            else
                            {
                                DataRow dr = dtEmployeePaySlip.NewRow();
                                //dr["TypeAllow"] = "PREVIOUS MONTH ADJUSTMENTS";
                                //dr["AllowanceActAmt"] = "0";
                                //dr["AllowanceName"] = "NO Deduction";
                                try
                                {

                                    dr["DaysPresent"] = "0";
                                    dr["WeekOff"] = "0";
                                    dr["daysAbsent"] = "0";
                                    dr["Holiday"] = "0";
                                    dr["Leaves"] = "0";
                                    dr["Totaldays"] = "0";


                                    dr["WorkedSal"] = "0";
                                    dr["LeavedaysSal"] = "0";
                                    dr["HolidaysSal"] = "0";
                                    dr["WeekOffSal"] = "0";

                                    dr["totalAttendSal"] = "0";

                                    dr["NormalOTSal"] = "0";
                                    dr["WeekOffOTSal"] = "0";
                                    dr["HolidaysOTSal"] = "0";
                                    dr["TotalOTSal"] = "0";


                                    dr["LatePenaSal"] = "0";
                                    dr["EarlyPenaSal"] = "0";
                                    dr["PartialPenaSal"] = "0";
                                    dr["AbsentPenaSal"] = "0";
                                }
                                catch
                                {

                                }


                                dr["TotalPenaltySal"] = totalpenasal.ToString();
                                dr["GrossAttendanceSal"] = grossattendance.ToString();


                                dr["EmpCode"] = EmployeeCode;
                                dr["EmpName"] = EmployeeName;
                                DateTime dt = Convert.ToDateTime(dtEmpInfo.Rows[0]["DOJ"].ToString());
                                dr["DOJ"] = dt.ToString("dd-MMM-yyyy");
                                if (dtDesignation.Rows.Count > 0)
                                {
                                    dr["Designation"] = dtDesignation.Rows[0]["Designation"].ToString();
                                }
                                if (dtDepartment.Rows.Count > 0)
                                {
                                    dr["Department"] = dtDepartment.Rows[0]["Dep_Name"].ToString();
                                }
                                dr["Month"] = month[Convert.ToInt32(ddlMonth.SelectedIndex.ToString())].ToString();
                                dr["Year"] = TxtYear.Text.ToString();
                                dr["BankAccountNo"] = dtEmpInfo.Rows[0]["Account_No"].ToString();


                                dtEmployeePaySlip.Rows.Add(dr);


                            }


                            if (dtAllowance.Rows.Count > 0)
                            {


                                for (int j = 0; j < dtAllowance.Rows.Count; j++)
                                {
                                    DataRow dr = dtEmployeePaySlip.NewRow();
                                    dr["TypeAllow"] = "ALLOWANCES";
                                    dr["AllowanceActAmt"] = dtAllowance.Rows[j]["Act_Allowance_Value"].ToString();
                                    double allow = Convert.ToDouble(dtAllowance.Rows[j]["Act_Allowance_Value"].ToString());
                                    sumallowance += allow;
                                    if (dtAllowance.Rows[j]["Allowance_Id"].ToString() != "0" && dtAllowance.Rows[j]["Allowance_Id"].ToString() != "")
                                    {

                                        DataTable dtAllowancename = ObjAllow.GetAllowanceTruebyId(Session["CompId"].ToString(), dtAllowance.Rows[j]["Allowance_Id"].ToString());

                                        dtAllowancename = new DataView(dtAllowancename, "Allowance_Id=" + dtAllowance.Rows[j]["Allowance_Id"].ToString() + " ", "", DataViewRowState.CurrentRows).ToTable();
                                        if (dtAllowancename.Rows.Count > 0)
                                        {
                                            dr["AllowanceName"] = dtAllowancename.Rows[0]["Allowance"].ToString();
                                        }
                                        else
                                        {
                                            dr["AllowanceName"] = "";
                                        }

                                    }
                                    try
                                    {

                                        dr["DaysPresent"] = dtEmpAttendance.Rows[0]["Worked_Days"].ToString();
                                        dr["WeekOff"] = dtEmpAttendance.Rows[0]["Week_Off_Days"].ToString();
                                        dr["daysAbsent"] = dtEmpAttendance.Rows[0]["Absent_Days"].ToString();
                                        dr["Holiday"] = dtEmpAttendance.Rows[0]["Holiday_Days"].ToString();
                                        dr["Leaves"] = dtEmpAttendance.Rows[0]["Leave_Days"].ToString();
                                        dr["Totaldays"] = totaldays.ToString();


                                        dr["WorkedSal"] = dtEmpAttendance.Rows[0]["Basic_Work_Salary"].ToString();
                                        dr["LeavedaysSal"] = dtEmpAttendance.Rows[0]["Leave_Days_Salary"].ToString();
                                        dr["HolidaysSal"] = dtEmpAttendance.Rows[0]["Holiday_Days_Salary"].ToString();
                                        dr["WeekOffSal"] = dtEmpAttendance.Rows[0]["Week_Off_Days_Salary"].ToString();


                                        dr["totalAttendSal"] = totalattndsal.ToString();

                                        dr["NormalOTSal"] = dtEmpAttendance.Rows[0]["Normal_OT_Work_Salary"].ToString();
                                        dr["WeekOffOTSal"] = dtEmpAttendance.Rows[0]["WeekOff_OT_Work_Salary"].ToString();
                                        dr["HolidaysOTSal"] = dtEmpAttendance.Rows[0]["Holiday_OT_Work_Salary"].ToString();

                                        dr["TotalOTSal"] = totalOTsal.ToString();


                                        dr["LatePenaSal"] = dtEmpAttendance.Rows[0]["Late_Min_Penalty"].ToString();
                                        dr["EarlyPenaSal"] = dtEmpAttendance.Rows[0]["Early_Min_Penalty"].ToString();
                                        dr["PartialPenaSal"] = dtEmpAttendance.Rows[0]["Parital_Violation_Penalty"].ToString();
                                        dr["AbsentPenaSal"] = dtEmpAttendance.Rows[0]["Absent_Day_Penalty"].ToString();
                                    }
                                    catch
                                    {


                                    }
                                    dr["TotalPenaltySal"] = totalpenasal.ToString();
                                    dr["GrossAttendanceSal"] = grossattendance.ToString();


                                    dr["EmpCode"] = EmployeeCode;
                                    dr["EmpName"] = EmployeeName;
                                    DateTime dt = Convert.ToDateTime(dtEmpInfo.Rows[0]["DOJ"].ToString());
                                    dr["DOJ"] = dt.ToString("dd-MMM-yyyy");

                                    if (dtDesignation.Rows.Count > 0)
                                    {
                                        dr["Designation"] = dtDesignation.Rows[0]["Designation"].ToString();
                                    }
                                    if (dtDepartment.Rows.Count > 0)
                                    {
                                        dr["Department"] = dtDepartment.Rows[0]["Dep_Name"].ToString();
                                    }
                                    dr["Month"] = month[Convert.ToInt32(ddlMonth.SelectedIndex.ToString())].ToString();
                                    dr["Year"] = TxtYear.Text.ToString();
                                    dr["BankAccountNo"] = dtEmpInfo.Rows[0]["Account_No"].ToString();


                                    dtEmployeePaySlip.Rows.Add(dr);

                                }



                            }
                            else
                            {
                                DataRow dr = dtEmployeePaySlip.NewRow();
                                dr["TypeAllow"] = "ALLOWANCES";
                                dr["AllowanceActAmt"] = "0";
                                dr["AllowanceName"] = "No Allowances";
                                try
                                {

                                    dr["DaysPresent"] = dtEmpAttendance.Rows[0]["Worked_Days"].ToString();
                                    dr["WeekOff"] = dtEmpAttendance.Rows[0]["Week_Off_Days"].ToString();
                                    dr["daysAbsent"] = dtEmpAttendance.Rows[0]["Absent_Days"].ToString();
                                    dr["Holiday"] = dtEmpAttendance.Rows[0]["Holiday_Days"].ToString();
                                    dr["Leaves"] = dtEmpAttendance.Rows[0]["Leave_Days"].ToString();
                                    dr["Totaldays"] = totaldays.ToString();


                                    dr["WorkedSal"] = dtEmpAttendance.Rows[0]["Basic_Work_Salary"].ToString();
                                    dr["LeavedaysSal"] = dtEmpAttendance.Rows[0]["Leave_Days_Salary"].ToString();
                                    dr["HolidaysSal"] = dtEmpAttendance.Rows[0]["Holiday_Days_Salary"].ToString();
                                    dr["WeekOffSal"] = dtEmpAttendance.Rows[0]["Week_Off_Days_Salary"].ToString();

                                    dr["totalAttendSal"] = totalattndsal.ToString();

                                    dr["NormalOTSal"] = dtEmpAttendance.Rows[0]["Normal_OT_Work_Salary"].ToString();
                                    dr["WeekOffOTSal"] = dtEmpAttendance.Rows[0]["WeekOff_OT_Work_Salary"].ToString();
                                    dr["HolidaysOTSal"] = dtEmpAttendance.Rows[0]["Holiday_OT_Work_Salary"].ToString();
                                    dr["TotalOTSal"] = totalOTsal.ToString();


                                    dr["LatePenaSal"] = dtEmpAttendance.Rows[0]["Late_Min_Penalty"].ToString();
                                    dr["EarlyPenaSal"] = dtEmpAttendance.Rows[0]["Early_Min_Penalty"].ToString();
                                    dr["PartialPenaSal"] = dtEmpAttendance.Rows[0]["Parital_Violation_Penalty"].ToString();
                                    dr["AbsentPenaSal"] = dtEmpAttendance.Rows[0]["Absent_Day_Penalty"].ToString();
                                }
                                catch
                                {

                                }


                                dr["TotalPenaltySal"] = totalpenasal.ToString();
                                dr["GrossAttendanceSal"] = grossattendance.ToString();


                                dr["EmpCode"] = EmployeeCode;
                                dr["EmpName"] = EmployeeName;
                                DateTime dt = Convert.ToDateTime(dtEmpInfo.Rows[0]["DOJ"].ToString());
                                dr["DOJ"] = dt.ToString("dd-MMM-yyyy");
                                if (dtDesignation.Rows.Count > 0)
                                {
                                    dr["Designation"] = dtDesignation.Rows[0]["Designation"].ToString();
                                }
                                if (dtDepartment.Rows.Count > 0)
                                {
                                    dr["Department"] = dtDepartment.Rows[0]["Dep_Name"].ToString();
                                }
                                dr["Month"] = month[Convert.ToInt32(ddlMonth.SelectedIndex.ToString())].ToString();
                                dr["Year"] = TxtYear.Text.ToString();
                                dr["BankAccountNo"] = dtEmpInfo.Rows[0]["Account_No"].ToString();


                                dtEmployeePaySlip.Rows.Add(dr);


                            }



                            if (dtClaim.Rows.Count > 0)
                            {
                                for (int c = 0; c < dtClaim.Rows.Count; c++)
                                {
                                    DataRow dr = dtEmployeePaySlip.NewRow();
                                    dr["TypeAllow"] = "CLAIMS";

                                    dr["AllowanceName"] = dtClaim.Rows[c]["Claim_Name"].ToString();



                                    if (dtClaim.Rows[c]["Value_Type"].ToString() == "1")
                                    {
                                        Double h = Convert.ToDouble(dtClaim.Rows[c]["Value"]);
                                        dr["AllowanceActAmt"] = dtClaim.Rows[c]["Value"].ToString();

                                        sumclaim += h;
                                    }
                                    else
                                    {

                                        double val = Convert.ToDouble((basicsal * Convert.ToDouble(dtClaim.Rows[c]["Value"].ToString())) / 100);
                                        dr["AllowanceActAmt"] = val.ToString();
                                        sumclaim += val;
                                    }

                                    try
                                    {

                                        dr["DaysPresent"] = dtEmpAttendance.Rows[0]["Worked_Days"].ToString();
                                        dr["WeekOff"] = dtEmpAttendance.Rows[0]["Week_Off_Days"].ToString();
                                        dr["daysAbsent"] = dtEmpAttendance.Rows[0]["Absent_Days"].ToString();
                                        dr["Holiday"] = dtEmpAttendance.Rows[0]["Holiday_Days"].ToString();
                                        dr["Leaves"] = dtEmpAttendance.Rows[0]["Leave_Days"].ToString();
                                        dr["Totaldays"] = totaldays.ToString();


                                        dr["WorkedSal"] = dtEmpAttendance.Rows[0]["Basic_Work_Salary"].ToString();
                                        dr["LeavedaysSal"] = dtEmpAttendance.Rows[0]["Leave_Days_Salary"].ToString();
                                        dr["HolidaysSal"] = dtEmpAttendance.Rows[0]["Holiday_Days_Salary"].ToString();
                                        dr["WeekOffSal"] = dtEmpAttendance.Rows[0]["Week_Off_Days_Salary"].ToString();

                                        //double worksal = Convert.ToDouble(dtEmpAttendance.Rows[0]["Basic_Work_Salary"].ToString());
                                        //double leavesal = Convert.ToDouble(dtEmpAttendance.Rows[0]["Leave_Days_Salary"].ToString());
                                        //double holidayssal = Convert.ToDouble(dtEmpAttendance.Rows[0]["Holiday_Days_Salary"].ToString());
                                        //double weekofsal = Convert.ToDouble(dtEmpAttendance.Rows[0]["Week_Off_Days_Salary"].ToString());

                                        //totalattndsal = worksal + leavesal + holidayssal + weekofsal;
                                        dr["totalAttendSal"] = totalattndsal.ToString();

                                        dr["NormalOTSal"] = dtEmpAttendance.Rows[0]["Normal_OT_Work_Salary"].ToString();
                                        dr["WeekOffOTSal"] = dtEmpAttendance.Rows[0]["WeekOff_OT_Work_Salary"].ToString();
                                        dr["HolidaysOTSal"] = dtEmpAttendance.Rows[0]["Holiday_OT_Work_Salary"].ToString();

                                        //double noramlotsal = Convert.ToDouble(dtEmpAttendance.Rows[0]["Normal_OT_Work_Salary"].ToString());
                                        // double weekotsal = Convert.ToDouble(dtEmpAttendance.Rows[0]["WeekOff_OT_Work_Salary"].ToString());
                                        // double holidaotsal = Convert.ToDouble(dtEmpAttendance.Rows[0]["Holiday_OT_Work_Salary"].ToString());
                                        // totalOTsal = noramlotsal + weekotsal + holidaotsal;
                                        dr["TotalOTSal"] = totalOTsal.ToString();


                                        dr["LatePenaSal"] = dtEmpAttendance.Rows[0]["Late_Min_Penalty"].ToString();
                                        dr["EarlyPenaSal"] = dtEmpAttendance.Rows[0]["Early_Min_Penalty"].ToString();
                                        dr["PartialPenaSal"] = dtEmpAttendance.Rows[0]["Parital_Violation_Penalty"].ToString();
                                        dr["AbsentPenaSal"] = dtEmpAttendance.Rows[0]["Absent_Day_Penalty"].ToString();
                                    }
                                    catch
                                    {

                                    }
                                    // double latesal = Convert.ToDouble(dtEmpAttendance.Rows[0]["Late_Min_Penalty"].ToString());
                                    // double earlysal = Convert.ToDouble(dtEmpAttendance.Rows[0]["Early_Min_Penalty"].ToString());
                                    // double partialsal = Convert.ToDouble(dtEmpAttendance.Rows[0]["Parital_Violation_Penalty"].ToString());
                                    // double absentsal = Convert.ToDouble(dtEmpAttendance.Rows[0]["Absent_Day_Penalty"].ToString());

                                    //totalpenasal = latesal + earlysal + partialsal + absentsal;
                                    dr["TotalPenaltySal"] = totalpenasal.ToString();
                                    dr["GrossAttendanceSal"] = grossattendance.ToString();


                                    dr["EmpCode"] = EmployeeCode;
                                    dr["EmpName"] = EmployeeName;
                                    DateTime dt = Convert.ToDateTime(dtEmpInfo.Rows[0]["DOJ"].ToString());
                                    dr["DOJ"] = dt.ToString("dd-MMM-yyyy");

                                    if (dtDesignation.Rows.Count > 0)
                                    {
                                        dr["Designation"] = dtDesignation.Rows[0]["Designation"].ToString();
                                    }
                                    if (dtDepartment.Rows.Count > 0)
                                    {
                                        dr["Department"] = dtDepartment.Rows[0]["Dep_Name"].ToString();
                                    }
                                    dr["Month"] = month[Convert.ToInt32(ddlMonth.SelectedIndex.ToString())].ToString();
                                    dr["Year"] = TxtYear.Text.ToString();
                                    dr["BankAccountNo"] = dtEmpInfo.Rows[0]["Account_No"].ToString();
                                    //dr["DaysPresent"] = dtEmpAttendance.Rows[0]["Worked_Days"].ToString();
                                    //dr["WeekOff"] = dtEmpAttendance.Rows[0]["Week_Off_Days"].ToString();
                                    //dr["daysAbsent"] = dtEmpAttendance.Rows[0]["Absent_Days"].ToString();
                                    //dr["Holiday"] = dtEmpAttendance.Rows[0]["Holiday_Days"].ToString();
                                    //dr["Leaves"] = dtEmpAttendance.Rows[0]["Leave_Days"].ToString();
                                    //totalDeducPenaLoan = sumdeduction + sumpenalty + sumloan;

                                    //netsalary = totalAllowClaim - totalDeducPenaLoan + (prvmonthbal);

                                    //dr["NetSalary"] = netsalary.ToString();

                                    dtEmployeePaySlip.Rows.Add(dr);
                                }
                            }

                            else
                            {
                                DataRow dr = dtEmployeePaySlip.NewRow();
                                dr["TypeAllow"] = "CLAIMS";
                                dr["AllowanceActAmt"] = "0";
                                dr["AllowanceName"] = "No Claims";
                                try
                                {

                                    dr["DaysPresent"] = dtEmpAttendance.Rows[0]["Worked_Days"].ToString();
                                    dr["WeekOff"] = dtEmpAttendance.Rows[0]["Week_Off_Days"].ToString();
                                    dr["daysAbsent"] = dtEmpAttendance.Rows[0]["Absent_Days"].ToString();
                                    dr["Holiday"] = dtEmpAttendance.Rows[0]["Holiday_Days"].ToString();
                                    dr["Leaves"] = dtEmpAttendance.Rows[0]["Leave_Days"].ToString();
                                    dr["Totaldays"] = totaldays.ToString();


                                    dr["WorkedSal"] = dtEmpAttendance.Rows[0]["Basic_Work_Salary"].ToString();
                                    dr["LeavedaysSal"] = dtEmpAttendance.Rows[0]["Leave_Days_Salary"].ToString();
                                    dr["HolidaysSal"] = dtEmpAttendance.Rows[0]["Holiday_Days_Salary"].ToString();
                                    dr["WeekOffSal"] = dtEmpAttendance.Rows[0]["Week_Off_Days_Salary"].ToString();

                                    dr["totalAttendSal"] = totalattndsal.ToString();

                                    dr["NormalOTSal"] = dtEmpAttendance.Rows[0]["Normal_OT_Work_Salary"].ToString();
                                    dr["WeekOffOTSal"] = dtEmpAttendance.Rows[0]["WeekOff_OT_Work_Salary"].ToString();
                                    dr["HolidaysOTSal"] = dtEmpAttendance.Rows[0]["Holiday_OT_Work_Salary"].ToString();
                                    dr["TotalOTSal"] = totalOTsal.ToString();


                                    dr["LatePenaSal"] = dtEmpAttendance.Rows[0]["Late_Min_Penalty"].ToString();
                                    dr["EarlyPenaSal"] = dtEmpAttendance.Rows[0]["Early_Min_Penalty"].ToString();
                                    dr["PartialPenaSal"] = dtEmpAttendance.Rows[0]["Parital_Violation_Penalty"].ToString();
                                    dr["AbsentPenaSal"] = dtEmpAttendance.Rows[0]["Absent_Day_Penalty"].ToString();
                                }
                                catch
                                {

                                }


                                dr["TotalPenaltySal"] = totalpenasal.ToString();
                                dr["GrossAttendanceSal"] = grossattendance.ToString();


                                dr["EmpCode"] = EmployeeCode;
                                dr["EmpName"] = EmployeeName;
                                DateTime dt = Convert.ToDateTime(dtEmpInfo.Rows[0]["DOJ"].ToString());
                                dr["DOJ"] = dt.ToString("dd-MMM-yyyy");
                                if (dtDesignation.Rows.Count > 0)
                                {
                                    dr["Designation"] = dtDesignation.Rows[0]["Designation"].ToString();
                                }
                                if (dtDepartment.Rows.Count > 0)
                                {
                                    dr["Department"] = dtDepartment.Rows[0]["Dep_Name"].ToString();
                                }
                                dr["Month"] = month[Convert.ToInt32(ddlMonth.SelectedIndex.ToString())].ToString();
                                dr["Year"] = TxtYear.Text.ToString();
                                dr["BankAccountNo"] = dtEmpInfo.Rows[0]["Account_No"].ToString();


                                dtEmployeePaySlip.Rows.Add(dr);


                            }



                            if (dtPenalty.Rows.Count > 0)
                            {
                                for (int p = 0; p < dtPenalty.Rows.Count; p++)
                                {
                                    DataRow dr = dtEmployeePaySlip.NewRow();
                                    dr["TypeAllow"] = "PENALTIES";
                                    dr["AllowanceName"] = dtPenalty.Rows[p]["Penalty_Name"].ToString();
                                    if (dtPenalty.Rows[p]["Value_Type"].ToString() == "1")
                                    {
                                        dr["AllowanceActAmt"] = dtPenalty.Rows[p]["Value"].ToString();
                                        Double tp = Convert.ToDouble(dtPenalty.Rows[p]["Value"].ToString());

                                        sumpenalty += tp;
                                    }
                                    else
                                    {
                                        double val = Convert.ToDouble((basicsal * Convert.ToDouble(dtPenalty.Rows[p]["Value"].ToString())) / 100);
                                        sumpenalty += val;
                                        dr["AllowanceActAmt"] = val.ToString();
                                    }

                                    try
                                    {

                                        dr["DaysPresent"] = dtEmpAttendance.Rows[0]["Worked_Days"].ToString();
                                        dr["WeekOff"] = dtEmpAttendance.Rows[0]["Week_Off_Days"].ToString();
                                        dr["daysAbsent"] = dtEmpAttendance.Rows[0]["Absent_Days"].ToString();
                                        dr["Holiday"] = dtEmpAttendance.Rows[0]["Holiday_Days"].ToString();
                                        dr["Leaves"] = dtEmpAttendance.Rows[0]["Leave_Days"].ToString();
                                        dr["Totaldays"] = totaldays.ToString();


                                        dr["WorkedSal"] = dtEmpAttendance.Rows[0]["Basic_Work_Salary"].ToString();
                                        dr["LeavedaysSal"] = dtEmpAttendance.Rows[0]["Leave_Days_Salary"].ToString();
                                        dr["HolidaysSal"] = dtEmpAttendance.Rows[0]["Holiday_Days_Salary"].ToString();
                                        dr["WeekOffSal"] = dtEmpAttendance.Rows[0]["Week_Off_Days_Salary"].ToString();

                                        //double worksal = Convert.ToDouble(dtEmpAttendance.Rows[0]["Basic_Work_Salary"].ToString());
                                        //double leavesal = Convert.ToDouble(dtEmpAttendance.Rows[0]["Leave_Days_Salary"].ToString());
                                        //double holidayssal = Convert.ToDouble(dtEmpAttendance.Rows[0]["Holiday_Days_Salary"].ToString());
                                        //double weekofsal = Convert.ToDouble(dtEmpAttendance.Rows[0]["Week_Off_Days_Salary"].ToString());

                                        //totalattndsal = worksal + leavesal + holidayssal + weekofsal;
                                        dr["totalAttendSal"] = totalattndsal.ToString();

                                        dr["NormalOTSal"] = dtEmpAttendance.Rows[0]["Normal_OT_Work_Salary"].ToString();
                                        dr["WeekOffOTSal"] = dtEmpAttendance.Rows[0]["WeekOff_OT_Work_Salary"].ToString();
                                        dr["HolidaysOTSal"] = dtEmpAttendance.Rows[0]["Holiday_OT_Work_Salary"].ToString();

                                        //double noramlotsal = Convert.ToDouble(dtEmpAttendance.Rows[0]["Normal_OT_Work_Salary"].ToString());
                                        // double weekotsal = Convert.ToDouble(dtEmpAttendance.Rows[0]["WeekOff_OT_Work_Salary"].ToString());
                                        // double holidaotsal = Convert.ToDouble(dtEmpAttendance.Rows[0]["Holiday_OT_Work_Salary"].ToString());
                                        // totalOTsal = noramlotsal + weekotsal + holidaotsal;
                                        dr["TotalOTSal"] = totalOTsal.ToString();


                                        dr["LatePenaSal"] = dtEmpAttendance.Rows[0]["Late_Min_Penalty"].ToString();
                                        dr["EarlyPenaSal"] = dtEmpAttendance.Rows[0]["Early_Min_Penalty"].ToString();
                                        dr["PartialPenaSal"] = dtEmpAttendance.Rows[0]["Parital_Violation_Penalty"].ToString();
                                        dr["AbsentPenaSal"] = dtEmpAttendance.Rows[0]["Absent_Day_Penalty"].ToString();
                                    }
                                    catch
                                    {

                                    }
                                    // double latesal = Convert.ToDouble(dtEmpAttendance.Rows[0]["Late_Min_Penalty"].ToString());
                                    // double earlysal = Convert.ToDouble(dtEmpAttendance.Rows[0]["Early_Min_Penalty"].ToString());
                                    // double partialsal = Convert.ToDouble(dtEmpAttendance.Rows[0]["Parital_Violation_Penalty"].ToString());
                                    // double absentsal = Convert.ToDouble(dtEmpAttendance.Rows[0]["Absent_Day_Penalty"].ToString());

                                    //totalpenasal = latesal + earlysal + partialsal + absentsal;
                                    dr["TotalPenaltySal"] = totalpenasal.ToString();
                                    dr["GrossAttendanceSal"] = grossattendance.ToString();


                                    dr["EmpCode"] = EmployeeCode;
                                    dr["EmpName"] = EmployeeName;
                                    DateTime dt = Convert.ToDateTime(dtEmpInfo.Rows[0]["DOJ"].ToString());
                                    dr["DOJ"] = dt.ToString("dd-MMM-yyyy");

                                    if (dtDesignation.Rows.Count > 0)
                                    {
                                        dr["Designation"] = dtDesignation.Rows[0]["Designation"].ToString();
                                    }
                                    if (dtDepartment.Rows.Count > 0)
                                    {
                                        dr["Department"] = dtDepartment.Rows[0]["Dep_Name"].ToString();
                                    }
                                    dr["Month"] = month[Convert.ToInt32(ddlMonth.SelectedIndex.ToString())].ToString();
                                    dr["Year"] = TxtYear.Text.ToString();
                                    dr["BankAccountNo"] = dtEmpInfo.Rows[0]["Account_No"].ToString();
                                    //dr["DaysPresent"] = dtEmpAttendance.Rows[0]["Worked_Days"].ToString();
                                    //dr["WeekOff"] = dtEmpAttendance.Rows[0]["Week_Off_Days"].ToString();
                                    //dr["daysAbsent"] = dtEmpAttendance.Rows[0]["Absent_Days"].ToString();
                                    //dr["Holiday"] = dtEmpAttendance.Rows[0]["Holiday_Days"].ToString();
                                    //dr["Leaves"] = dtEmpAttendance.Rows[0]["Leave_Days"].ToString();

                                    //totalDeducPenaLoan = sumdeduction + sumpenalty + sumloan;

                                    //netsalary = totalAllowClaim - totalDeducPenaLoan + (prvmonthbal);



                                    //dr["NetSalary"] = netsalary.ToString();

                                    dtEmployeePaySlip.Rows.Add(dr);


                                }

                            }

                            else
                            {
                                DataRow dr = dtEmployeePaySlip.NewRow();
                                dr["TypeAllow"] = "PENALTIES";
                                dr["AllowanceActAmt"] = "0";
                                dr["AllowanceName"] = "No Penalties";
                                try
                                {

                                    dr["DaysPresent"] = dtEmpAttendance.Rows[0]["Worked_Days"].ToString();
                                    dr["WeekOff"] = dtEmpAttendance.Rows[0]["Week_Off_Days"].ToString();
                                    dr["daysAbsent"] = dtEmpAttendance.Rows[0]["Absent_Days"].ToString();
                                    dr["Holiday"] = dtEmpAttendance.Rows[0]["Holiday_Days"].ToString();
                                    dr["Leaves"] = dtEmpAttendance.Rows[0]["Leave_Days"].ToString();
                                    dr["Totaldays"] = totaldays.ToString();


                                    dr["WorkedSal"] = dtEmpAttendance.Rows[0]["Basic_Work_Salary"].ToString();
                                    dr["LeavedaysSal"] = dtEmpAttendance.Rows[0]["Leave_Days_Salary"].ToString();
                                    dr["HolidaysSal"] = dtEmpAttendance.Rows[0]["Holiday_Days_Salary"].ToString();
                                    dr["WeekOffSal"] = dtEmpAttendance.Rows[0]["Week_Off_Days_Salary"].ToString();

                                    dr["totalAttendSal"] = totalattndsal.ToString();

                                    dr["NormalOTSal"] = dtEmpAttendance.Rows[0]["Normal_OT_Work_Salary"].ToString();
                                    dr["WeekOffOTSal"] = dtEmpAttendance.Rows[0]["WeekOff_OT_Work_Salary"].ToString();
                                    dr["HolidaysOTSal"] = dtEmpAttendance.Rows[0]["Holiday_OT_Work_Salary"].ToString();
                                    dr["TotalOTSal"] = totalOTsal.ToString();


                                    dr["LatePenaSal"] = dtEmpAttendance.Rows[0]["Late_Min_Penalty"].ToString();
                                    dr["EarlyPenaSal"] = dtEmpAttendance.Rows[0]["Early_Min_Penalty"].ToString();
                                    dr["PartialPenaSal"] = dtEmpAttendance.Rows[0]["Parital_Violation_Penalty"].ToString();
                                    dr["AbsentPenaSal"] = dtEmpAttendance.Rows[0]["Absent_Day_Penalty"].ToString();
                                }
                                catch
                                {

                                }


                                dr["TotalPenaltySal"] = totalpenasal.ToString();
                                dr["GrossAttendanceSal"] = grossattendance.ToString();


                                dr["EmpCode"] = EmployeeCode;
                                dr["EmpName"] = EmployeeName;
                                DateTime dt = Convert.ToDateTime(dtEmpInfo.Rows[0]["DOJ"].ToString());
                                dr["DOJ"] = dt.ToString("dd-MMM-yyyy");
                                if (dtDesignation.Rows.Count > 0)
                                {
                                    dr["Designation"] = dtDesignation.Rows[0]["Designation"].ToString();
                                }
                                if (dtDepartment.Rows.Count > 0)
                                {
                                    dr["Department"] = dtDepartment.Rows[0]["Dep_Name"].ToString();
                                }
                                dr["Month"] = month[Convert.ToInt32(ddlMonth.SelectedIndex.ToString())].ToString();
                                dr["Year"] = TxtYear.Text.ToString();
                                dr["BankAccountNo"] = dtEmpInfo.Rows[0]["Account_No"].ToString();



                                dtEmployeePaySlip.Rows.Add(dr);


                            }


                            dtLoan = new DataView(dtLoan, " Emp_Id=" + str + "", "", DataViewRowState.CurrentRows).ToTable();
                            if (dtLoan.Rows.Count > 0)
                            {
                                for (int l = 0; l < dtLoan.Rows.Count; l++)
                                {
                                    DataRow dr = dtEmployeePaySlip.NewRow();
                                    DataTable dtloandetial = new DataTable();

                                    dtloandetial = objEmpLoan.GetRecord_From_PayEmployeeLoanDetail(dtLoan.Rows[l]["Loan_Id"].ToString());
                                    dtloandetial = new DataView(dtloandetial, "Loan_Id=" + dtLoan.Rows[l]["Loan_Id"].ToString() + " and Month='" + dtloandetial.Rows[l]["Month"].ToString() + "' and Year=" + dtloandetial.Rows[l]["Year"].ToString() + " ", "", DataViewRowState.CurrentRows).ToTable();

                                    if (dtloandetial.Rows.Count > 0)
                                    {

                                        dr["TypeAllow"] = "LOANS";
                                        dr["AllowanceName"] = dtLoan.Rows[l]["Loan_Name"].ToString();
                                        dr["AllowanceActAmt"] = dtloandetial.Rows[l]["Total_Amount"].ToString();
                                        double loan = Convert.ToDouble(dtloandetial.Rows[l]["Total_Amount"].ToString());
                                        sumloan += loan;

                                    }

                                    try
                                    {
                                        dr["DaysPresent"] = dtEmpAttendance.Rows[0]["Worked_Days"].ToString();
                                        dr["WeekOff"] = dtEmpAttendance.Rows[0]["Week_Off_Days"].ToString();
                                        dr["daysAbsent"] = dtEmpAttendance.Rows[0]["Absent_Days"].ToString();
                                        dr["Holiday"] = dtEmpAttendance.Rows[0]["Holiday_Days"].ToString();
                                        dr["Leaves"] = dtEmpAttendance.Rows[0]["Leave_Days"].ToString();
                                        dr["Totaldays"] = totaldays.ToString();


                                        dr["WorkedSal"] = dtEmpAttendance.Rows[0]["Basic_Work_Salary"].ToString();
                                        dr["LeavedaysSal"] = dtEmpAttendance.Rows[0]["Leave_Days_Salary"].ToString();
                                        dr["HolidaysSal"] = dtEmpAttendance.Rows[0]["Holiday_Days_Salary"].ToString();
                                        dr["WeekOffSal"] = dtEmpAttendance.Rows[0]["Week_Off_Days_Salary"].ToString();

                                        //double worksal = Convert.ToDouble(dtEmpAttendance.Rows[0]["Basic_Work_Salary"].ToString());
                                        //double leavesal = Convert.ToDouble(dtEmpAttendance.Rows[0]["Leave_Days_Salary"].ToString());
                                        //double holidayssal = Convert.ToDouble(dtEmpAttendance.Rows[0]["Holiday_Days_Salary"].ToString());
                                        //double weekofsal = Convert.ToDouble(dtEmpAttendance.Rows[0]["Week_Off_Days_Salary"].ToString());

                                        //totalattndsal = worksal + leavesal + holidayssal + weekofsal;
                                        dr["totalAttendSal"] = totalattndsal.ToString();

                                        dr["NormalOTSal"] = dtEmpAttendance.Rows[0]["Normal_OT_Work_Salary"].ToString();
                                        dr["WeekOffOTSal"] = dtEmpAttendance.Rows[0]["WeekOff_OT_Work_Salary"].ToString();
                                        dr["HolidaysOTSal"] = dtEmpAttendance.Rows[0]["Holiday_OT_Work_Salary"].ToString();

                                        //double noramlotsal = Convert.ToDouble(dtEmpAttendance.Rows[0]["Normal_OT_Work_Salary"].ToString());
                                        // double weekotsal = Convert.ToDouble(dtEmpAttendance.Rows[0]["WeekOff_OT_Work_Salary"].ToString());
                                        // double holidaotsal = Convert.ToDouble(dtEmpAttendance.Rows[0]["Holiday_OT_Work_Salary"].ToString());
                                        // totalOTsal = noramlotsal + weekotsal + holidaotsal;
                                        dr["TotalOTSal"] = totalOTsal.ToString();


                                        dr["LatePenaSal"] = dtEmpAttendance.Rows[0]["Late_Min_Penalty"].ToString();
                                        dr["EarlyPenaSal"] = dtEmpAttendance.Rows[0]["Early_Min_Penalty"].ToString();
                                        dr["PartialPenaSal"] = dtEmpAttendance.Rows[0]["Parital_Violation_Penalty"].ToString();
                                        dr["AbsentPenaSal"] = dtEmpAttendance.Rows[0]["Absent_Day_Penalty"].ToString();
                                    }
                                    catch
                                    {


                                    }
                                    // double latesal = Convert.ToDouble(dtEmpAttendance.Rows[0]["Late_Min_Penalty"].ToString());
                                    // double earlysal = Convert.ToDouble(dtEmpAttendance.Rows[0]["Early_Min_Penalty"].ToString());
                                    // double partialsal = Convert.ToDouble(dtEmpAttendance.Rows[0]["Parital_Violation_Penalty"].ToString());
                                    // double absentsal = Convert.ToDouble(dtEmpAttendance.Rows[0]["Absent_Day_Penalty"].ToString());

                                    //totalpenasal = latesal + earlysal + partialsal + absentsal;
                                    dr["TotalPenaltySal"] = totalpenasal.ToString();
                                    dr["GrossAttendanceSal"] = grossattendance.ToString();


                                    dr["EmpCode"] = EmployeeCode;
                                    dr["EmpName"] = EmployeeName;
                                    DateTime dt = Convert.ToDateTime(dtEmpInfo.Rows[0]["DOJ"].ToString());
                                    dr["DOJ"] = dt.ToString("dd-MMM-yyyy");
                                    if (dtDesignation.Rows.Count > 0)
                                    {
                                        dr["Designation"] = dtDesignation.Rows[0]["Designation"].ToString();
                                    }
                                    if (dtDepartment.Rows.Count > 0)
                                    {
                                        dr["Department"] = dtDepartment.Rows[0]["Dep_Name"].ToString();
                                    }
                                    dr["Month"] = month[Convert.ToInt32(ddlMonth.SelectedIndex.ToString())].ToString();
                                    dr["Year"] = TxtYear.Text.ToString();
                                    dr["BankAccountNo"] = dtEmpInfo.Rows[0]["Account_No"].ToString();
                                    //dr["DaysPresent"] = dtEmpAttendance.Rows[0]["Worked_Days"].ToString();
                                    //dr["WeekOff"] = dtEmpAttendance.Rows[0]["Week_Off_Days"].ToString();
                                    //dr["daysAbsent"] = dtEmpAttendance.Rows[0]["Absent_Days"].ToString();
                                    //dr["Holiday"] = dtEmpAttendance.Rows[0]["Holiday_Days"].ToString();
                                    //dr["Leaves"] = dtEmpAttendance.Rows[0]["Leave_Days"].ToString();


                                    //totalDeducPenaLoan = sumdeduction + sumpenalty + sumloan;

                                    //netsalary = totalAllowClaim - totalDeducPenaLoan + (prvmonthbal);

                                    //dr["NetSalary"] = netsalary.ToString();

                                    dtEmployeePaySlip.Rows.Add(dr);


                                }

                            }

                            else
                            {
                                DataRow dr = dtEmployeePaySlip.NewRow();
                                dr["TypeAllow"] = "LOANS";
                                dr["AllowanceActAmt"] = "0";
                                dr["AllowanceName"] = "No Loans";
                                try
                                {

                                    dr["DaysPresent"] = dtEmpAttendance.Rows[0]["Worked_Days"].ToString();
                                    dr["WeekOff"] = dtEmpAttendance.Rows[0]["Week_Off_Days"].ToString();
                                    dr["daysAbsent"] = dtEmpAttendance.Rows[0]["Absent_Days"].ToString();
                                    dr["Holiday"] = dtEmpAttendance.Rows[0]["Holiday_Days"].ToString();
                                    dr["Leaves"] = dtEmpAttendance.Rows[0]["Leave_Days"].ToString();
                                    dr["Totaldays"] = totaldays.ToString();


                                    dr["WorkedSal"] = dtEmpAttendance.Rows[0]["Basic_Work_Salary"].ToString();
                                    dr["LeavedaysSal"] = dtEmpAttendance.Rows[0]["Leave_Days_Salary"].ToString();
                                    dr["HolidaysSal"] = dtEmpAttendance.Rows[0]["Holiday_Days_Salary"].ToString();
                                    dr["WeekOffSal"] = dtEmpAttendance.Rows[0]["Week_Off_Days_Salary"].ToString();

                                    dr["totalAttendSal"] = totalattndsal.ToString();

                                    dr["NormalOTSal"] = dtEmpAttendance.Rows[0]["Normal_OT_Work_Salary"].ToString();
                                    dr["WeekOffOTSal"] = dtEmpAttendance.Rows[0]["WeekOff_OT_Work_Salary"].ToString();
                                    dr["HolidaysOTSal"] = dtEmpAttendance.Rows[0]["Holiday_OT_Work_Salary"].ToString();
                                    dr["TotalOTSal"] = totalOTsal.ToString();


                                    dr["LatePenaSal"] = dtEmpAttendance.Rows[0]["Late_Min_Penalty"].ToString();
                                    dr["EarlyPenaSal"] = dtEmpAttendance.Rows[0]["Early_Min_Penalty"].ToString();
                                    dr["PartialPenaSal"] = dtEmpAttendance.Rows[0]["Parital_Violation_Penalty"].ToString();
                                    dr["AbsentPenaSal"] = dtEmpAttendance.Rows[0]["Absent_Day_Penalty"].ToString();
                                }
                                catch
                                {

                                }


                                dr["TotalPenaltySal"] = totalpenasal.ToString();
                                dr["GrossAttendanceSal"] = grossattendance.ToString();


                                dr["EmpCode"] = EmployeeCode;
                                dr["EmpName"] = EmployeeName;
                                DateTime dt = Convert.ToDateTime(dtEmpInfo.Rows[0]["DOJ"].ToString());
                                dr["DOJ"] = dt.ToString("dd-MMM-yyyy");
                                if (dtDesignation.Rows.Count > 0)
                                {
                                    dr["Designation"] = dtDesignation.Rows[0]["Designation"].ToString();
                                }
                                if (dtDepartment.Rows.Count > 0)
                                {
                                    dr["Department"] = dtDepartment.Rows[0]["Dep_Name"].ToString();
                                }
                                dr["Month"] = month[Convert.ToInt32(ddlMonth.SelectedIndex.ToString())].ToString();
                                dr["Year"] = TxtYear.Text.ToString();
                                dr["BankAccountNo"] = dtEmpInfo.Rows[0]["Account_No"].ToString();


                                dtEmployeePaySlip.Rows.Add(dr);


                            }


                            DataTable dtEmpPrvablmonth = new DataTable();

                            dtEmpPrvablmonth = objPayEmpMonth.GetAllRecordPostedEmpMonth(str, ddlMonth.SelectedValue.ToString(), TxtYear.Text);
                            //dtPrvMonthBal = objPayEmpMonth.GetRecord_PrvBal_Fields(str, ddlMonth.SelectedValue.ToString(), TxtYear.Text);
                            if (dtEmpPrvablmonth.Rows.Count > 0)
                            {
                                for (int prv = 0; prv < dtEmpPrvablmonth.Rows.Count; prv++)
                                {
                                    DataRow dr = dtEmployeePaySlip.NewRow();
                                    dr["TypeAllow"] = "PREVIOUS MONTH ADJUSTMENTS";
                                    dr["AllowanceName"] = "PREVIOUS MONTH";
                                    dr["AllowanceActAmt"] = dtEmpPrvablmonth.Rows[0]["Previous_Month_Balance"].ToString();

                                    double pr = Convert.ToDouble(dtEmpPrvablmonth.Rows[0]["Previous_Month_Balance"].ToString());
                                    prvmonthbal += pr;
                                    try
                                    {
                                        dr["DaysPresent"] = dtEmpAttendance.Rows[0]["Worked_Days"].ToString();
                                        dr["WeekOff"] = dtEmpAttendance.Rows[0]["Week_Off_Days"].ToString();
                                        dr["daysAbsent"] = dtEmpAttendance.Rows[0]["Absent_Days"].ToString();
                                        dr["Holiday"] = dtEmpAttendance.Rows[0]["Holiday_Days"].ToString();
                                        dr["Leaves"] = dtEmpAttendance.Rows[0]["Leave_Days"].ToString();
                                        dr["Totaldays"] = totaldays.ToString();


                                        dr["WorkedSal"] = dtEmpAttendance.Rows[0]["Basic_Work_Salary"].ToString();
                                        dr["LeavedaysSal"] = dtEmpAttendance.Rows[0]["Leave_Days_Salary"].ToString();
                                        dr["HolidaysSal"] = dtEmpAttendance.Rows[0]["Holiday_Days_Salary"].ToString();
                                        dr["WeekOffSal"] = dtEmpAttendance.Rows[0]["Week_Off_Days_Salary"].ToString();

                                        //double worksal = Convert.ToDouble(dtEmpAttendance.Rows[0]["Basic_Work_Salary"].ToString());
                                        //double leavesal = Convert.ToDouble(dtEmpAttendance.Rows[0]["Leave_Days_Salary"].ToString());
                                        //double holidayssal = Convert.ToDouble(dtEmpAttendance.Rows[0]["Holiday_Days_Salary"].ToString());
                                        //double weekofsal = Convert.ToDouble(dtEmpAttendance.Rows[0]["Week_Off_Days_Salary"].ToString());

                                        //totalattndsal = worksal + leavesal + holidayssal + weekofsal;
                                        dr["totalAttendSal"] = totalattndsal.ToString();

                                        dr["NormalOTSal"] = dtEmpAttendance.Rows[0]["Normal_OT_Work_Salary"].ToString();
                                        dr["WeekOffOTSal"] = dtEmpAttendance.Rows[0]["WeekOff_OT_Work_Salary"].ToString();
                                        dr["HolidaysOTSal"] = dtEmpAttendance.Rows[0]["Holiday_OT_Work_Salary"].ToString();

                                        //double noramlotsal = Convert.ToDouble(dtEmpAttendance.Rows[0]["Normal_OT_Work_Salary"].ToString());
                                        // double weekotsal = Convert.ToDouble(dtEmpAttendance.Rows[0]["WeekOff_OT_Work_Salary"].ToString());
                                        // double holidaotsal = Convert.ToDouble(dtEmpAttendance.Rows[0]["Holiday_OT_Work_Salary"].ToString());
                                        // totalOTsal = noramlotsal + weekotsal + holidaotsal;
                                        dr["TotalOTSal"] = totalOTsal.ToString();


                                        dr["LatePenaSal"] = dtEmpAttendance.Rows[0]["Late_Min_Penalty"].ToString();
                                        dr["EarlyPenaSal"] = dtEmpAttendance.Rows[0]["Early_Min_Penalty"].ToString();
                                        dr["PartialPenaSal"] = dtEmpAttendance.Rows[0]["Parital_Violation_Penalty"].ToString();
                                        dr["AbsentPenaSal"] = dtEmpAttendance.Rows[0]["Absent_Day_Penalty"].ToString();
                                    }
                                    catch
                                    {

                                    }
                                    // double latesal = Convert.ToDouble(dtEmpAttendance.Rows[0]["Late_Min_Penalty"].ToString());
                                    // double earlysal = Convert.ToDouble(dtEmpAttendance.Rows[0]["Early_Min_Penalty"].ToString());
                                    // double partialsal = Convert.ToDouble(dtEmpAttendance.Rows[0]["Parital_Violation_Penalty"].ToString());
                                    // double absentsal = Convert.ToDouble(dtEmpAttendance.Rows[0]["Absent_Day_Penalty"].ToString());

                                    //totalpenasal = latesal + earlysal + partialsal + absentsal;
                                    dr["TotalPenaltySal"] = totalpenasal.ToString();
                                    dr["GrossAttendanceSal"] = grossattendance.ToString();


                                    dr["EmpCode"] = EmployeeCode;
                                    dr["EmpName"] = EmployeeName;
                                    DateTime dt = Convert.ToDateTime(dtEmpInfo.Rows[0]["DOJ"].ToString());
                                    dr["DOJ"] = dt.ToString("dd-MMM-yyyy");
                                    if (dtDesignation.Rows.Count > 0)
                                    {
                                        dr["Designation"] = dtDesignation.Rows[0]["Designation"].ToString();
                                    }
                                    if (dtDepartment.Rows.Count > 0)
                                    {
                                        dr["Department"] = dtDepartment.Rows[0]["Dep_Name"].ToString();
                                    }
                                    dr["Month"] = month[Convert.ToInt32(ddlMonth.SelectedIndex.ToString())].ToString();
                                    dr["Year"] = TxtYear.Text.ToString();
                                    dr["BankAccountNo"] = dtEmpInfo.Rows[0]["Account_No"].ToString();


                                    dtEmployeePaySlip.Rows.Add(dr);


                                }

                            }
                            else
                            {
                                DataRow dr = dtEmployeePaySlip.NewRow();
                                dr["TypeAllow"] = "PREVIOUS MONTH ADJUSTMENTS";
                                dr["AllowanceActAmt"] = "0";
                                dr["AllowanceName"] = "No Adjustment";
                                try
                                {

                                    dr["DaysPresent"] = dtEmpAttendance.Rows[0]["Worked_Days"].ToString();
                                    dr["WeekOff"] = dtEmpAttendance.Rows[0]["Week_Off_Days"].ToString();
                                    dr["daysAbsent"] = dtEmpAttendance.Rows[0]["Absent_Days"].ToString();
                                    dr["Holiday"] = dtEmpAttendance.Rows[0]["Holiday_Days"].ToString();
                                    dr["Leaves"] = dtEmpAttendance.Rows[0]["Leave_Days"].ToString();
                                    dr["Totaldays"] = totaldays.ToString();


                                    dr["WorkedSal"] = dtEmpAttendance.Rows[0]["Basic_Work_Salary"].ToString();
                                    dr["LeavedaysSal"] = dtEmpAttendance.Rows[0]["Leave_Days_Salary"].ToString();
                                    dr["HolidaysSal"] = dtEmpAttendance.Rows[0]["Holiday_Days_Salary"].ToString();
                                    dr["WeekOffSal"] = dtEmpAttendance.Rows[0]["Week_Off_Days_Salary"].ToString();

                                    dr["totalAttendSal"] = totalattndsal.ToString();

                                    dr["NormalOTSal"] = dtEmpAttendance.Rows[0]["Normal_OT_Work_Salary"].ToString();
                                    dr["WeekOffOTSal"] = dtEmpAttendance.Rows[0]["WeekOff_OT_Work_Salary"].ToString();
                                    dr["HolidaysOTSal"] = dtEmpAttendance.Rows[0]["Holiday_OT_Work_Salary"].ToString();
                                    dr["TotalOTSal"] = totalOTsal.ToString();


                                    dr["LatePenaSal"] = dtEmpAttendance.Rows[0]["Late_Min_Penalty"].ToString();
                                    dr["EarlyPenaSal"] = dtEmpAttendance.Rows[0]["Early_Min_Penalty"].ToString();
                                    dr["PartialPenaSal"] = dtEmpAttendance.Rows[0]["Parital_Violation_Penalty"].ToString();
                                    dr["AbsentPenaSal"] = dtEmpAttendance.Rows[0]["Absent_Day_Penalty"].ToString();
                                }
                                catch
                                {

                                }


                                dr["TotalPenaltySal"] = totalpenasal.ToString();
                                dr["GrossAttendanceSal"] = grossattendance.ToString();


                                dr["EmpCode"] = EmployeeCode;
                                dr["EmpName"] = EmployeeName;
                                DateTime dt = Convert.ToDateTime(dtEmpInfo.Rows[0]["DOJ"].ToString());
                                dr["DOJ"] = dt.ToString("dd-MMM-yyyy");
                                if (dtDesignation.Rows.Count > 0)
                                {
                                    dr["Designation"] = dtDesignation.Rows[0]["Designation"].ToString();
                                }
                                if (dtDepartment.Rows.Count > 0)
                                {
                                    dr["Department"] = dtDepartment.Rows[0]["Dep_Name"].ToString();
                                }
                                dr["Month"] = month[Convert.ToInt32(ddlMonth.SelectedIndex.ToString())].ToString();
                                dr["Year"] = TxtYear.Text.ToString();
                                dr["BankAccountNo"] = dtEmpInfo.Rows[0]["Account_No"].ToString();



                                dtEmployeePaySlip.Rows.Add(dr);


                            }


                            if (dtDeuction.Rows.Count > 0)
                            {

                                for (int k = 0; k < dtDeuction.Rows.Count; k++)
                                {
                                    DataRow dr = dtEmployeePaySlip.NewRow();
                                    dr["TypeAllow"] = "DEDUCTIONS";
                                    dr["AllowanceActAmt"] = dtDeuction.Rows[k]["Act_Deduction_Value"].ToString();
                                    double deduc = Convert.ToDouble(dtDeuction.Rows[k]["Act_Deduction_Value"].ToString());
                                    sumdeduction += deduc;

                                    if (dtDeuction.Rows[k]["Deduction_Id"].ToString() != "0" && dtDeuction.Rows[k]["Deduction_Id"].ToString() != "")
                                    {
                                        DataTable dtDeductionname = objDeduction.GetDeductionTruebyId(Session["CompId"].ToString(), dtDeuction.Rows[k]["Deduction_Id"].ToString());

                                        dtDeductionname = new DataView(dtDeductionname, "Deduction_Id=" + dtDeuction.Rows[k]["Deduction_Id"].ToString() + " ", "", DataViewRowState.CurrentRows).ToTable();
                                        if (dtDeductionname.Rows.Count > 0)
                                        {
                                            dr["AllowanceName"] = dtDeductionname.Rows[0]["Deduction"].ToString();
                                        }
                                        else
                                        {
                                            dr["AllowanceName"] = "";
                                        }

                                    }
                                    try
                                    {

                                        dr["DaysPresent"] = dtEmpAttendance.Rows[0]["Worked_Days"].ToString();
                                        dr["WeekOff"] = dtEmpAttendance.Rows[0]["Week_Off_Days"].ToString();
                                        dr["daysAbsent"] = dtEmpAttendance.Rows[0]["Absent_Days"].ToString();
                                        dr["Holiday"] = dtEmpAttendance.Rows[0]["Holiday_Days"].ToString();
                                        dr["Leaves"] = dtEmpAttendance.Rows[0]["Leave_Days"].ToString();
                                        dr["Totaldays"] = totaldays.ToString();


                                        dr["WorkedSal"] = dtEmpAttendance.Rows[0]["Basic_Work_Salary"].ToString();
                                        dr["LeavedaysSal"] = dtEmpAttendance.Rows[0]["Leave_Days_Salary"].ToString();
                                        dr["HolidaysSal"] = dtEmpAttendance.Rows[0]["Holiday_Days_Salary"].ToString();
                                        dr["WeekOffSal"] = dtEmpAttendance.Rows[0]["Week_Off_Days_Salary"].ToString();

                                        dr["totalAttendSal"] = totalattndsal.ToString();

                                        dr["NormalOTSal"] = dtEmpAttendance.Rows[0]["Normal_OT_Work_Salary"].ToString();
                                        dr["WeekOffOTSal"] = dtEmpAttendance.Rows[0]["WeekOff_OT_Work_Salary"].ToString();
                                        dr["HolidaysOTSal"] = dtEmpAttendance.Rows[0]["Holiday_OT_Work_Salary"].ToString();
                                        dr["TotalOTSal"] = totalOTsal.ToString();


                                        dr["LatePenaSal"] = dtEmpAttendance.Rows[0]["Late_Min_Penalty"].ToString();
                                        dr["EarlyPenaSal"] = dtEmpAttendance.Rows[0]["Early_Min_Penalty"].ToString();
                                        dr["PartialPenaSal"] = dtEmpAttendance.Rows[0]["Parital_Violation_Penalty"].ToString();
                                        dr["AbsentPenaSal"] = dtEmpAttendance.Rows[0]["Absent_Day_Penalty"].ToString();
                                    }
                                    catch
                                    {

                                    }


                                    dr["TotalPenaltySal"] = totalpenasal.ToString();
                                    dr["GrossAttendanceSal"] = grossattendance.ToString();


                                    dr["EmpCode"] = EmployeeCode;
                                    dr["EmpName"] = EmployeeName;
                                    DateTime dt = Convert.ToDateTime(dtEmpInfo.Rows[0]["DOJ"].ToString());
                                    dr["DOJ"] = dt.ToString("dd-MMM-yyyy");
                                    if (dtDesignation.Rows.Count > 0)
                                    {
                                        dr["Designation"] = dtDesignation.Rows[0]["Designation"].ToString();
                                    }
                                    if (dtDepartment.Rows.Count > 0)
                                    {
                                        dr["Department"] = dtDepartment.Rows[0]["Dep_Name"].ToString();
                                    }
                                    dr["Month"] = month[Convert.ToInt32(ddlMonth.SelectedIndex.ToString())].ToString();
                                    dr["Year"] = TxtYear.Text.ToString();
                                    dr["BankAccountNo"] = dtEmpInfo.Rows[0]["Account_No"].ToString();

                                    totalAllowClaim = sumallowance + sumclaim + grossattendance;

                                    totalDeducPenaLoan = sumdeduction + sumpenalty + sumloan;

                                    netsalary = totalAllowClaim - totalDeducPenaLoan + (prvmonthbal);
                                    dr["NetSalary"] = netsalary.ToString();

                                    dtEmployeePaySlip.Rows.Add(dr);

                                }

                            }
                            else
                            {

                                DataRow dr = dtEmployeePaySlip.NewRow();


                                dr["TypeAllow"] = "DEDUCTIONS";
                                dr["AllowanceActAmt"] = "0";
                                dr["AllowanceName"] = "No Deductions";
                                try
                                {
                                    dr["DaysPresent"] = dtEmpAttendance.Rows[0]["Worked_Days"].ToString();
                                    dr["WeekOff"] = dtEmpAttendance.Rows[0]["Week_Off_Days"].ToString();
                                    dr["daysAbsent"] = dtEmpAttendance.Rows[0]["Absent_Days"].ToString();
                                    dr["Holiday"] = dtEmpAttendance.Rows[0]["Holiday_Days"].ToString();
                                    dr["Leaves"] = dtEmpAttendance.Rows[0]["Leave_Days"].ToString();
                                    dr["Totaldays"] = totaldays.ToString();


                                    dr["WorkedSal"] = dtEmpAttendance.Rows[0]["Basic_Work_Salary"].ToString();
                                    dr["LeavedaysSal"] = dtEmpAttendance.Rows[0]["Leave_Days_Salary"].ToString();
                                    dr["HolidaysSal"] = dtEmpAttendance.Rows[0]["Holiday_Days_Salary"].ToString();
                                    dr["WeekOffSal"] = dtEmpAttendance.Rows[0]["Week_Off_Days_Salary"].ToString();

                                    dr["totalAttendSal"] = totalattndsal.ToString();

                                    dr["NormalOTSal"] = dtEmpAttendance.Rows[0]["Normal_OT_Work_Salary"].ToString();
                                    dr["WeekOffOTSal"] = dtEmpAttendance.Rows[0]["WeekOff_OT_Work_Salary"].ToString();
                                    dr["HolidaysOTSal"] = dtEmpAttendance.Rows[0]["Holiday_OT_Work_Salary"].ToString();
                                    dr["TotalOTSal"] = totalOTsal.ToString();


                                    dr["LatePenaSal"] = dtEmpAttendance.Rows[0]["Late_Min_Penalty"].ToString();
                                    dr["EarlyPenaSal"] = dtEmpAttendance.Rows[0]["Early_Min_Penalty"].ToString();
                                    dr["PartialPenaSal"] = dtEmpAttendance.Rows[0]["Parital_Violation_Penalty"].ToString();
                                    dr["AbsentPenaSal"] = dtEmpAttendance.Rows[0]["Absent_Day_Penalty"].ToString();
                                }
                                catch
                                {

                                }

                                dr["TotalPenaltySal"] = totalpenasal.ToString();
                                dr["GrossAttendanceSal"] = grossattendance.ToString();
                                dr["EmpCode"] = EmployeeCode;
                                dr["EmpName"] = EmployeeName;
                                DateTime dt = Convert.ToDateTime(dtEmpInfo.Rows[0]["DOJ"].ToString());
                                dr["DOJ"] = dt.ToString("dd-MMM-yyyy");
                                if (dtDesignation.Rows.Count > 0)
                                {
                                    dr["Designation"] = dtDesignation.Rows[0]["Designation"].ToString();
                                }
                                if (dtDepartment.Rows.Count > 0)
                                {
                                    dr["Department"] = dtDepartment.Rows[0]["Dep_Name"].ToString();
                                }
                                dr["Month"] = month[Convert.ToInt32(ddlMonth.SelectedIndex.ToString())].ToString();
                                dr["Year"] = TxtYear.Text.ToString();
                                dr["BankAccountNo"] = dtEmpInfo.Rows[0]["Account_No"].ToString();

                                totalAllowClaim = sumallowance + sumclaim + grossattendance;
                                totalDeducPenaLoan = sumdeduction + sumpenalty + sumloan;
                                netsalary = totalAllowClaim - totalDeducPenaLoan + (prvmonthbal);
                                dr["NetSalary"] = netsalary.ToString();

                                dtEmployeePaySlip.Rows.Add(dr);
                            }


                        }



                    }


                }

            }
            else
            {
                DisplayMessage("Select Group First");
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
                Session["netsalary"] = null;
                if ((str != ""))
                {
                    DataTable dtemppara = new DataTable();
                    dtemppara = objempparam.GetEmployeeParameterByEmpId(str, Session["CompId"].ToString());
                    if (dtemppara.Rows.Count > 0)
                    {

                        basicsal = Convert.ToDouble(dtemppara.Rows[0]["Basic_Salary"].ToString());
                    }


                    EmployeeName = GetEmployeeName(str);
                    EmployeeCode = GetEmployeeCode(str);

                    sumallowance = 0;
                    sumdeduction = 0;
                    netsalary = 0;
                    sumclaim = 0;
                    sumpenalty = 0;
                    sumloan = 0;
                    totalAllowClaim = 0;
                    totalDeducPenaLoan = 0;
                    prvmonthbal = 0;
                    totaldays = 0;
                    grossattendance = 0;


                    DataTable dtAllowance = new DataTable();
                    DataTable dtDeuction = new DataTable();
                    DataTable dtEmpInfo = new DataTable();
                    DataTable dtDepartment = new DataTable();
                    DataTable dtDesignation = new DataTable();
                    DataTable dtLoan = new DataTable();
                    DataTable dtPenalty = new DataTable();
                    DataTable dtClaim = new DataTable();
                    DataTable dtPayEmpMonth = new DataTable();
                    DataTable dtEmpAttendance = new DataTable();
                    DataTable dtPrvMonthBal = new DataTable();

                    dtEmpInfo = objEmp.GetEmployeeMasterByEmpCode(Session["CompId"].ToString(), 
                        EmployeeCode.ToString());
                    dtDepartment = objDep.GetDepartmentMasterById(Session["CompId"].ToString(), 
                        dtEmpInfo.Rows[0]["Department_Id"].ToString());
                    dtDesignation = objDesg.GetDesignationMasterById(
                        Session["CompId"].ToString(), dtEmpInfo.Rows[0]["Designation_Id"].ToString());
                    dtAllowance = objpayrollall.GetPostedAllowanceAll(str, 
                        ddlMonth.SelectedIndex.ToString(), TxtYear.Text);
                  
                    dtDeuction = objpayrolldeduc.GetRecordPostedDeductionAll(str, ddlMonth.SelectedValue.ToString(), TxtYear.Text);

                    dtPayEmpMonth = objPayEmpMonth.Getallrecords(str);
                    dtPayEmpMonth = new DataView(dtPayEmpMonth, 
                        "Emp_Id=" + str + " and Month=" + ddlMonth.SelectedValue.ToString() + 
                        " and Year='" + TxtYear.Text + "'", "", DataViewRowState.CurrentRows).ToTable();


                    dtPenalty = objPEpenalty.GetRecord_From_PayEmployeePenalty(Session["CompId"].ToString(), str, "0", ddlMonth.SelectedValue.ToString(), TxtYear.Text, "", "");
                    dtPenalty = new DataView(dtPenalty, "Emp_Id=" + str + " and Penalty_Month=" + ddlMonth.SelectedValue.ToString() + " and Penalty_Year='" + TxtYear.Text + "' ", "", DataViewRowState.CurrentRows).ToTable();

                    dtClaim = objPEClaim.GetRecord_From_PayEmployeeClaim(Session["CompId"].ToString(), str, "0", ddlMonth.SelectedValue.ToString(), TxtYear.Text, "Approved", "", "");
                    dtClaim = new DataView(dtClaim, "Emp_Id="+str+" and Claim_Month="+ddlMonth.SelectedValue.ToString()+" and Claim_Year='"+TxtYear.Text+"'", "", DataViewRowState.CurrentRows).ToTable();

                    dtLoan = objEmpLoan.GetRecord_From_PayEmployeeLoan(Session["CompId"].ToString(), "0", "Running");
                    //dtLoan = new DataView(dtLoan, "Emp_Id=" + str + " and Month=" + ddlMonth.SelectedValue.ToString() + " and Year='" + TxtYear.Text + "'", "", DataViewRowState.CurrentRows).ToTable();

                    dtEmpAttendance = objEmpAttendance.GetRecord_Emp_Attendance(str, ddlMonth.SelectedValue.ToString(), TxtYear.Text);
                   
                    

                    for ( int i = 0; i < dtEmpInfo.Rows.Count; i++)
                    {
                        if (dtEmpAttendance.Rows.Count > 0)
                        {
                           
                                DataRow dr = dtEmployeePaySlip.NewRow();
                                dr["TypeAllow"] = "";
                                
                            //Attendance Days
                                dr["DaysPresent"] = dtEmpAttendance.Rows[0]["Worked_Days"].ToString();
                                dr["WeekOff"] = dtEmpAttendance.Rows[0]["Week_Off_Days"].ToString();
                                dr["daysAbsent"] = dtEmpAttendance.Rows[0]["Absent_Days"].ToString();
                                dr["Holiday"] = dtEmpAttendance.Rows[0]["Holiday_Days"].ToString();
                                dr["Leaves"] = dtEmpAttendance.Rows[0]["Leave_Days"].ToString();
                                
                                double dayspres = Convert.ToDouble(dtEmpAttendance.Rows[0]["Worked_Days"].ToString());
                                double weekofdays = Convert.ToDouble(dtEmpAttendance.Rows[0]["Week_Off_Days"].ToString());
                                double daysabsent = Convert.ToDouble(dtEmpAttendance.Rows[0]["Absent_Days"].ToString());
                                double holidays = Convert.ToDouble(dtEmpAttendance.Rows[0]["Holiday_Days"].ToString()); 
                                double daysleave = Convert.ToDouble(dtEmpAttendance.Rows[0]["Leave_Days"].ToString());
                                totaldays = dayspres + weekofdays + daysabsent + daysleave+holidays;
                                dr["Totaldays"] = totaldays.ToString();

                               //Attendance Salary
                                dr["WorkedSal"] = dtEmpAttendance.Rows[0]["Basic_Work_Salary"].ToString();
                                dr["LeavedaysSal"] = dtEmpAttendance.Rows[0]["Leave_Days_Salary"].ToString();
                                dr["HolidaysSal"] = dtEmpAttendance.Rows[0]["Holiday_Days_Salary"].ToString();
                                dr["WeekOffSal"] = dtEmpAttendance.Rows[0]["Week_Off_Days_Salary"].ToString();

                                double worksal = Convert.ToDouble(dtEmpAttendance.Rows[0]["Basic_Work_Salary"].ToString());
                                double leavesal = Convert.ToDouble(dtEmpAttendance.Rows[0]["Leave_Days_Salary"].ToString());
                                double holidayssal = Convert.ToDouble(dtEmpAttendance.Rows[0]["Holiday_Days_Salary"].ToString());
                                double weekofsal = Convert.ToDouble(dtEmpAttendance.Rows[0]["Week_Off_Days_Salary"].ToString());

                                totalattndsal = worksal + leavesal + holidayssal + weekofsal;
                                dr["totalAttendSal"] = totalattndsal.ToString();

                                dr["NormalOTSal"] = dtEmpAttendance.Rows[0]["Normal_OT_Work_Salary"].ToString();
                                dr["WeekOffOTSal"] = dtEmpAttendance.Rows[0]["WeekOff_OT_Work_Salary"].ToString();
                                dr["HolidaysOTSal"] = dtEmpAttendance.Rows[0]["Holiday_OT_Work_Salary"].ToString();

                                double noramlotsal = Convert.ToDouble(dtEmpAttendance.Rows[0]["Normal_OT_Work_Salary"].ToString());
                                double weekotsal = Convert.ToDouble(dtEmpAttendance.Rows[0]["WeekOff_OT_Work_Salary"].ToString());
                                double holidaotsal = Convert.ToDouble(dtEmpAttendance.Rows[0]["Holiday_OT_Work_Salary"].ToString());
                                totalOTsal = noramlotsal + weekotsal + holidaotsal;
                                dr["TotalOTSal"] = totalOTsal.ToString();


                                dr["LatePenaSal"] = dtEmpAttendance.Rows[0]["Late_Min_Penalty"].ToString();
                               dr["EarlyPenaSal"] = dtEmpAttendance.Rows[0]["Early_Min_Penalty"].ToString();
                                dr["PartialPenaSal"] = dtEmpAttendance.Rows[0]["Parital_Violation_Penalty"].ToString();
                                dr["AbsentPenaSal"] = dtEmpAttendance.Rows[0]["Absent_Day_Penalty"].ToString();

                                double latesal = Convert.ToDouble(dtEmpAttendance.Rows[0]["Late_Min_Penalty"].ToString());
                                double earlysal = Convert.ToDouble(dtEmpAttendance.Rows[0]["Early_Min_Penalty"].ToString());
                                double partialsal = Convert.ToDouble(dtEmpAttendance.Rows[0]["Parital_Violation_Penalty"].ToString());
                                double absentsal = Convert.ToDouble(dtEmpAttendance.Rows[0]["Absent_Day_Penalty"].ToString());

                                totalpenasal = latesal + earlysal + partialsal + absentsal;
                                dr["TotalPenaltySal"] = totalpenasal.ToString();
                               
                                grossattendance=(totalattndsal+totalOTsal)-totalpenasal;
                                
                               dr["GrossAttendanceSal"] = grossattendance.ToString();

                                
                                   
                                dr["EmpCode"] = EmployeeCode;
                                dr["EmpName"] = EmployeeName;
                                DateTime dt = Convert.ToDateTime(dtEmpInfo.Rows[0]["DOJ"].ToString());
                                dr["DOJ"] = dt.ToString("dd-MMM-yyyy");


                                if (dtDesignation.Rows.Count > 0)
                                {
                                    dr["Designation"] = dtDesignation.Rows[0]["Designation"].ToString();
                                }
                                if (dtDepartment.Rows.Count > 0)
                                {
                                    dr["Department"] = dtDepartment.Rows[0]["Dep_Name"].ToString();
                                }
                                dr["Month"] = month[Convert.ToInt32(ddlMonth.SelectedIndex.ToString())].ToString();
                                dr["Year"] = TxtYear.Text.ToString();
                                dr["BankAccountNo"] = dtEmpInfo.Rows[0]["Account_No"].ToString();

                                //totalAllowClaim = sumclaim + sumallowance + grossattendance;  

                                //totalDeducPenaLoan = sumdeduction + sumpenalty + sumloan;

                                //netsalary = totalAllowClaim - totalDeducPenaLoan + (prvmonthbal);



                              //dr["NetSalary"] = netsalary.ToString();
                                  
                              dtEmployeePaySlip.Rows.Add(dr);

                         
                        }
                      else
                        {
                            DataRow dr = dtEmployeePaySlip.NewRow();
                            //dr["TypeAllow"] = "PREVIOUS MONTH ADJUSTMENTS";
                            //dr["AllowanceActAmt"] = "0";
                            //dr["AllowanceName"] = "NO Deduction";
                            try
                            {

                                dr["DaysPresent"] = "0";
                                dr["WeekOff"] = "0";
                                dr["daysAbsent"] = "0";
                                dr["Holiday"] = "0";
                                dr["Leaves"] = "0";
                                dr["Totaldays"] = "0";


                                dr["WorkedSal"] = "0";
                                dr["LeavedaysSal"] = "0";
                                dr["HolidaysSal"] = "0";
                                dr["WeekOffSal"] = "0";

                                dr["totalAttendSal"] = "0";

                                dr["NormalOTSal"] = "0";
                                dr["WeekOffOTSal"] = "0";
                                dr["HolidaysOTSal"] = "0";
                                dr["TotalOTSal"] = "0";


                                dr["LatePenaSal"] = "0";
                                dr["EarlyPenaSal"] = "0";
                                dr["PartialPenaSal"] = "0";
                                dr["AbsentPenaSal"] = "0";
                            }
                            catch
                            {

                            }


                            dr["TotalPenaltySal"] = totalpenasal.ToString();
                            dr["GrossAttendanceSal"] = grossattendance.ToString();


                            dr["EmpCode"] = EmployeeCode;
                            dr["EmpName"] = EmployeeName;
                            DateTime dt = Convert.ToDateTime(dtEmpInfo.Rows[0]["DOJ"].ToString());
                            dr["DOJ"] = dt.ToString("dd-MMM-yyyy");
                            if (dtDesignation.Rows.Count > 0)
                            {
                                dr["Designation"] = dtDesignation.Rows[0]["Designation"].ToString();
                            }
                            if (dtDepartment.Rows.Count > 0)
                            {
                                dr["Department"] = dtDepartment.Rows[0]["Dep_Name"].ToString();
                            }
                            dr["Month"] = month[Convert.ToInt32(ddlMonth.SelectedIndex.ToString())].ToString();
                            dr["Year"] = TxtYear.Text.ToString();
                            dr["BankAccountNo"] = dtEmpInfo.Rows[0]["Account_No"].ToString();


                            dtEmployeePaySlip.Rows.Add(dr);


                        }


                        if (dtAllowance.Rows.Count > 0)
                        {


                            for (int j = 0; j < dtAllowance.Rows.Count; j++)
                            {
                                DataRow dr = dtEmployeePaySlip.NewRow();
                                dr["TypeAllow"] = "ALLOWANCES";
                                dr["AllowanceActAmt"] = dtAllowance.Rows[j]["Act_Allowance_Value"].ToString();
                                double allow = Convert.ToDouble(dtAllowance.Rows[j]["Act_Allowance_Value"].ToString());
                                sumallowance += allow;
                                if (dtAllowance.Rows[j]["Allowance_Id"].ToString() != "0" && dtAllowance.Rows[j]["Allowance_Id"].ToString() != "")
                                {

                                    DataTable dtAllowancename = ObjAllow.GetAllowanceTruebyId(Session["CompId"].ToString(), dtAllowance.Rows[j]["Allowance_Id"].ToString());

                                    dtAllowancename = new DataView(dtAllowancename, "Allowance_Id=" + dtAllowance.Rows[j]["Allowance_Id"].ToString() + " ", "", DataViewRowState.CurrentRows).ToTable();
                                    if (dtAllowancename.Rows.Count > 0)
                                    {
                                        dr["AllowanceName"] = dtAllowancename.Rows[0]["Allowance"].ToString();
                                    }
                                    else
                                    {
                                        dr["AllowanceName"] = "";
                                    }

                                }
                               try
                               {

                                    dr["DaysPresent"] = dtEmpAttendance.Rows[0]["Worked_Days"].ToString();
                                    dr["WeekOff"] = dtEmpAttendance.Rows[0]["Week_Off_Days"].ToString();
                                    dr["daysAbsent"] = dtEmpAttendance.Rows[0]["Absent_Days"].ToString();
                                    dr["Holiday"] = dtEmpAttendance.Rows[0]["Holiday_Days"].ToString();
                                    dr["Leaves"] = dtEmpAttendance.Rows[0]["Leave_Days"].ToString();
                                    dr["Totaldays"] = totaldays.ToString();


                                    dr["WorkedSal"] = dtEmpAttendance.Rows[0]["Basic_Work_Salary"].ToString();
                                    dr["LeavedaysSal"] = dtEmpAttendance.Rows[0]["Leave_Days_Salary"].ToString();
                                    dr["HolidaysSal"] = dtEmpAttendance.Rows[0]["Holiday_Days_Salary"].ToString();
                                    dr["WeekOffSal"] = dtEmpAttendance.Rows[0]["Week_Off_Days_Salary"].ToString();


                                    dr["totalAttendSal"] = totalattndsal.ToString();

                                    dr["NormalOTSal"] = dtEmpAttendance.Rows[0]["Normal_OT_Work_Salary"].ToString();
                                    dr["WeekOffOTSal"] = dtEmpAttendance.Rows[0]["WeekOff_OT_Work_Salary"].ToString();
                                    dr["HolidaysOTSal"] = dtEmpAttendance.Rows[0]["Holiday_OT_Work_Salary"].ToString();

                                    dr["TotalOTSal"] = totalOTsal.ToString();


                                    dr["LatePenaSal"] = dtEmpAttendance.Rows[0]["Late_Min_Penalty"].ToString();
                                    dr["EarlyPenaSal"] = dtEmpAttendance.Rows[0]["Early_Min_Penalty"].ToString();
                                    dr["PartialPenaSal"] = dtEmpAttendance.Rows[0]["Parital_Violation_Penalty"].ToString();
                                    dr["AbsentPenaSal"] = dtEmpAttendance.Rows[0]["Absent_Day_Penalty"].ToString();
                            }
                                catch 
                                { 
                                

                                }
                                dr["TotalPenaltySal"] = totalpenasal.ToString();
                                dr["GrossAttendanceSal"] = grossattendance.ToString();


                                dr["EmpCode"] = EmployeeCode;
                                dr["EmpName"] = EmployeeName;
                                DateTime dt = Convert.ToDateTime(dtEmpInfo.Rows[0]["DOJ"].ToString());
                                dr["DOJ"] = dt.ToString("dd-MMM-yyyy");

                                if (dtDesignation.Rows.Count > 0)
                                {
                                    dr["Designation"] = dtDesignation.Rows[0]["Designation"].ToString();
                                }
                                if (dtDepartment.Rows.Count > 0)
                                {
                                    dr["Department"] = dtDepartment.Rows[0]["Dep_Name"].ToString();
                                }
                                dr["Month"] = month[Convert.ToInt32(ddlMonth.SelectedIndex.ToString())].ToString();
                                dr["Year"] = TxtYear.Text.ToString();
                                dr["BankAccountNo"] = dtEmpInfo.Rows[0]["Account_No"].ToString();
                               
                              
                                dtEmployeePaySlip.Rows.Add(dr);

                            }
                            


                        }
                        else
                        {
                            DataRow dr = dtEmployeePaySlip.NewRow();
                            dr["TypeAllow"] = "ALLOWANCES";
                            dr["AllowanceActAmt"] = "0";
                            dr["AllowanceName"] = "No Allowances";
                            try
                            {

                                dr["DaysPresent"] = dtEmpAttendance.Rows[0]["Worked_Days"].ToString();
                                dr["WeekOff"] = dtEmpAttendance.Rows[0]["Week_Off_Days"].ToString();
                                dr["daysAbsent"] = dtEmpAttendance.Rows[0]["Absent_Days"].ToString();
                                dr["Holiday"] = dtEmpAttendance.Rows[0]["Holiday_Days"].ToString();
                                dr["Leaves"] = dtEmpAttendance.Rows[0]["Leave_Days"].ToString();
                                dr["Totaldays"] = totaldays.ToString();


                                dr["WorkedSal"] = dtEmpAttendance.Rows[0]["Basic_Work_Salary"].ToString();
                                dr["LeavedaysSal"] = dtEmpAttendance.Rows[0]["Leave_Days_Salary"].ToString();
                                dr["HolidaysSal"] = dtEmpAttendance.Rows[0]["Holiday_Days_Salary"].ToString();
                                dr["WeekOffSal"] = dtEmpAttendance.Rows[0]["Week_Off_Days_Salary"].ToString();

                                dr["totalAttendSal"] = totalattndsal.ToString();

                                dr["NormalOTSal"] = dtEmpAttendance.Rows[0]["Normal_OT_Work_Salary"].ToString();
                                dr["WeekOffOTSal"] = dtEmpAttendance.Rows[0]["WeekOff_OT_Work_Salary"].ToString();
                                dr["HolidaysOTSal"] = dtEmpAttendance.Rows[0]["Holiday_OT_Work_Salary"].ToString();
                                dr["TotalOTSal"] = totalOTsal.ToString();


                                dr["LatePenaSal"] = dtEmpAttendance.Rows[0]["Late_Min_Penalty"].ToString();
                                dr["EarlyPenaSal"] = dtEmpAttendance.Rows[0]["Early_Min_Penalty"].ToString();
                                dr["PartialPenaSal"] = dtEmpAttendance.Rows[0]["Parital_Violation_Penalty"].ToString();
                                dr["AbsentPenaSal"] = dtEmpAttendance.Rows[0]["Absent_Day_Penalty"].ToString();
                            }
                            catch
                            {

                            }


                            dr["TotalPenaltySal"] = totalpenasal.ToString();
                            dr["GrossAttendanceSal"] = grossattendance.ToString();


                            dr["EmpCode"] = EmployeeCode;
                            dr["EmpName"] = EmployeeName;
                            DateTime dt = Convert.ToDateTime(dtEmpInfo.Rows[0]["DOJ"].ToString());
                            dr["DOJ"] = dt.ToString("dd-MMM-yyyy");
                            if (dtDesignation.Rows.Count > 0)
                            {
                                dr["Designation"] = dtDesignation.Rows[0]["Designation"].ToString();
                            }
                            if (dtDepartment.Rows.Count > 0)
                            {
                                dr["Department"] = dtDepartment.Rows[0]["Dep_Name"].ToString();
                            }
                            dr["Month"] = month[Convert.ToInt32(ddlMonth.SelectedIndex.ToString())].ToString();
                            dr["Year"] = TxtYear.Text.ToString();
                            dr["BankAccountNo"] = dtEmpInfo.Rows[0]["Account_No"].ToString();

                          
                            dtEmployeePaySlip.Rows.Add(dr);


                        }



                        if (dtClaim.Rows.Count > 0)
                        {
                            for (int c = 0; c < dtClaim.Rows.Count; c++)
                            {
                                DataRow dr = dtEmployeePaySlip.NewRow();
                               dr["TypeAllow"] = "CLAIMS";

                               dr["AllowanceName"] = dtClaim.Rows[c]["Claim_Name"].ToString();



                                if (dtClaim.Rows[c]["Value_Type"].ToString() == "1")
                                {
                                    Double h = Convert.ToDouble(dtClaim.Rows[c]["Value"]);
                                    dr["AllowanceActAmt"] = dtClaim.Rows[c]["Value"].ToString();

                                    sumclaim += h;
                                }
                                else
                                {

                                    double val = Convert.ToDouble((basicsal * Convert.ToDouble(dtClaim.Rows[c]["Value"].ToString())) / 100);
                                    dr["AllowanceActAmt"] = val.ToString();
                                    sumclaim += val;
                                }
                                
                                try
                                {

                                    dr["DaysPresent"] = dtEmpAttendance.Rows[0]["Worked_Days"].ToString();
                                    dr["WeekOff"] = dtEmpAttendance.Rows[0]["Week_Off_Days"].ToString();
                                    dr["daysAbsent"] = dtEmpAttendance.Rows[0]["Absent_Days"].ToString();
                                    dr["Holiday"] = dtEmpAttendance.Rows[0]["Holiday_Days"].ToString();
                                    dr["Leaves"] = dtEmpAttendance.Rows[0]["Leave_Days"].ToString();
                                    dr["Totaldays"] = totaldays.ToString();


                                    dr["WorkedSal"] = dtEmpAttendance.Rows[0]["Basic_Work_Salary"].ToString();
                                    dr["LeavedaysSal"] = dtEmpAttendance.Rows[0]["Leave_Days_Salary"].ToString();
                                    dr["HolidaysSal"] = dtEmpAttendance.Rows[0]["Holiday_Days_Salary"].ToString();
                                    dr["WeekOffSal"] = dtEmpAttendance.Rows[0]["Week_Off_Days_Salary"].ToString();

                                    //double worksal = Convert.ToDouble(dtEmpAttendance.Rows[0]["Basic_Work_Salary"].ToString());
                                    //double leavesal = Convert.ToDouble(dtEmpAttendance.Rows[0]["Leave_Days_Salary"].ToString());
                                    //double holidayssal = Convert.ToDouble(dtEmpAttendance.Rows[0]["Holiday_Days_Salary"].ToString());
                                    //double weekofsal = Convert.ToDouble(dtEmpAttendance.Rows[0]["Week_Off_Days_Salary"].ToString());

                                    //totalattndsal = worksal + leavesal + holidayssal + weekofsal;
                                    dr["totalAttendSal"] = totalattndsal.ToString();

                                    dr["NormalOTSal"] = dtEmpAttendance.Rows[0]["Normal_OT_Work_Salary"].ToString();
                                    dr["WeekOffOTSal"] = dtEmpAttendance.Rows[0]["WeekOff_OT_Work_Salary"].ToString();
                                    dr["HolidaysOTSal"] = dtEmpAttendance.Rows[0]["Holiday_OT_Work_Salary"].ToString();

                                    //double noramlotsal = Convert.ToDouble(dtEmpAttendance.Rows[0]["Normal_OT_Work_Salary"].ToString());
                                    // double weekotsal = Convert.ToDouble(dtEmpAttendance.Rows[0]["WeekOff_OT_Work_Salary"].ToString());
                                    // double holidaotsal = Convert.ToDouble(dtEmpAttendance.Rows[0]["Holiday_OT_Work_Salary"].ToString());
                                    // totalOTsal = noramlotsal + weekotsal + holidaotsal;
                                    dr["TotalOTSal"] = totalOTsal.ToString();


                                    dr["LatePenaSal"] = dtEmpAttendance.Rows[0]["Late_Min_Penalty"].ToString();
                                    dr["EarlyPenaSal"] = dtEmpAttendance.Rows[0]["Early_Min_Penalty"].ToString();
                                    dr["PartialPenaSal"] = dtEmpAttendance.Rows[0]["Parital_Violation_Penalty"].ToString();
                                    dr["AbsentPenaSal"] = dtEmpAttendance.Rows[0]["Absent_Day_Penalty"].ToString();
                            }
                                catch 
                                { 
                                
                                }
                                // double latesal = Convert.ToDouble(dtEmpAttendance.Rows[0]["Late_Min_Penalty"].ToString());
                                // double earlysal = Convert.ToDouble(dtEmpAttendance.Rows[0]["Early_Min_Penalty"].ToString());
                                // double partialsal = Convert.ToDouble(dtEmpAttendance.Rows[0]["Parital_Violation_Penalty"].ToString());
                                // double absentsal = Convert.ToDouble(dtEmpAttendance.Rows[0]["Absent_Day_Penalty"].ToString());

                                //totalpenasal = latesal + earlysal + partialsal + absentsal;
                                dr["TotalPenaltySal"] = totalpenasal.ToString();
                                dr["GrossAttendanceSal"] = grossattendance.ToString();


                                dr["EmpCode"] = EmployeeCode;
                                dr["EmpName"] = EmployeeName;
                                DateTime dt = Convert.ToDateTime(dtEmpInfo.Rows[0]["DOJ"].ToString());
                                dr["DOJ"] = dt.ToString("dd-MMM-yyyy");

                                if (dtDesignation.Rows.Count > 0)
                                {
                                    dr["Designation"] = dtDesignation.Rows[0]["Designation"].ToString();
                                }
                                if (dtDepartment.Rows.Count > 0)
                                {
                                    dr["Department"] = dtDepartment.Rows[0]["Dep_Name"].ToString();
                                }
                                dr["Month"] = month[Convert.ToInt32(ddlMonth.SelectedIndex.ToString())].ToString();
                                dr["Year"] = TxtYear.Text.ToString();
                                dr["BankAccountNo"] = dtEmpInfo.Rows[0]["Account_No"].ToString();
                                //dr["DaysPresent"] = dtEmpAttendance.Rows[0]["Worked_Days"].ToString();
                                //dr["WeekOff"] = dtEmpAttendance.Rows[0]["Week_Off_Days"].ToString();
                                //dr["daysAbsent"] = dtEmpAttendance.Rows[0]["Absent_Days"].ToString();
                                //dr["Holiday"] = dtEmpAttendance.Rows[0]["Holiday_Days"].ToString();
                                //dr["Leaves"] = dtEmpAttendance.Rows[0]["Leave_Days"].ToString();
                                //totalDeducPenaLoan = sumdeduction + sumpenalty + sumloan;

                                //netsalary = totalAllowClaim - totalDeducPenaLoan + (prvmonthbal);

                                //dr["NetSalary"] = netsalary.ToString();
                              
                                dtEmployeePaySlip.Rows.Add(dr);
                            }
                        }

                        else
                        {
                            DataRow dr = dtEmployeePaySlip.NewRow();
                            dr["TypeAllow"] = "CLAIMS";
                            dr["AllowanceActAmt"] = "0";
                            dr["AllowanceName"] = "No Claims";
                            try
                            {

                                dr["DaysPresent"] = dtEmpAttendance.Rows[0]["Worked_Days"].ToString();
                                dr["WeekOff"] = dtEmpAttendance.Rows[0]["Week_Off_Days"].ToString();
                                dr["daysAbsent"] = dtEmpAttendance.Rows[0]["Absent_Days"].ToString();
                                dr["Holiday"] = dtEmpAttendance.Rows[0]["Holiday_Days"].ToString();
                                dr["Leaves"] = dtEmpAttendance.Rows[0]["Leave_Days"].ToString();
                                dr["Totaldays"] = totaldays.ToString();


                                dr["WorkedSal"] = dtEmpAttendance.Rows[0]["Basic_Work_Salary"].ToString();
                                dr["LeavedaysSal"] = dtEmpAttendance.Rows[0]["Leave_Days_Salary"].ToString();
                                dr["HolidaysSal"] = dtEmpAttendance.Rows[0]["Holiday_Days_Salary"].ToString();
                                dr["WeekOffSal"] = dtEmpAttendance.Rows[0]["Week_Off_Days_Salary"].ToString();

                                dr["totalAttendSal"] = totalattndsal.ToString();

                                dr["NormalOTSal"] = dtEmpAttendance.Rows[0]["Normal_OT_Work_Salary"].ToString();
                                dr["WeekOffOTSal"] = dtEmpAttendance.Rows[0]["WeekOff_OT_Work_Salary"].ToString();
                                dr["HolidaysOTSal"] = dtEmpAttendance.Rows[0]["Holiday_OT_Work_Salary"].ToString();
                                dr["TotalOTSal"] = totalOTsal.ToString();


                                dr["LatePenaSal"] = dtEmpAttendance.Rows[0]["Late_Min_Penalty"].ToString();
                                dr["EarlyPenaSal"] = dtEmpAttendance.Rows[0]["Early_Min_Penalty"].ToString();
                                dr["PartialPenaSal"] = dtEmpAttendance.Rows[0]["Parital_Violation_Penalty"].ToString();
                                dr["AbsentPenaSal"] = dtEmpAttendance.Rows[0]["Absent_Day_Penalty"].ToString();
                            }
                            catch
                            {

                            }


                            dr["TotalPenaltySal"] = totalpenasal.ToString();
                            dr["GrossAttendanceSal"] = grossattendance.ToString();


                            dr["EmpCode"] = EmployeeCode;
                            dr["EmpName"] = EmployeeName;
                            DateTime dt = Convert.ToDateTime(dtEmpInfo.Rows[0]["DOJ"].ToString());
                            dr["DOJ"] = dt.ToString("dd-MMM-yyyy");
                            if (dtDesignation.Rows.Count > 0)
                            {
                                dr["Designation"] = dtDesignation.Rows[0]["Designation"].ToString();
                            }
                            if (dtDepartment.Rows.Count > 0)
                            {
                                dr["Department"] = dtDepartment.Rows[0]["Dep_Name"].ToString();
                            }
                            dr["Month"] = month[Convert.ToInt32(ddlMonth.SelectedIndex.ToString())].ToString();
                            dr["Year"] = TxtYear.Text.ToString();
                            dr["BankAccountNo"] = dtEmpInfo.Rows[0]["Account_No"].ToString();

                           
                            dtEmployeePaySlip.Rows.Add(dr);


                        }



                        if (dtPenalty.Rows.Count > 0)
                        {
                            for (int p = 0; p < dtPenalty.Rows.Count; p++)
                            {
                                DataRow dr = dtEmployeePaySlip.NewRow();
                              dr["TypeAllow"] = "PENALTIES";
                              dr["AllowanceName"] = dtPenalty.Rows[p]["Penalty_Name"].ToString();
                                if (dtPenalty.Rows[p]["Value_Type"].ToString() == "1")
                                {
                                    dr["AllowanceActAmt"] = dtPenalty.Rows[p]["Value"].ToString();
                                    Double tp = Convert.ToDouble(dtPenalty.Rows[p]["Value"].ToString());

                                    sumpenalty += tp;
                                }
                                else
                                {
                                    double val = Convert.ToDouble((basicsal * Convert.ToDouble(dtPenalty.Rows[p]["Value"].ToString())) / 100);
                                    sumpenalty += val;
                                    dr["AllowanceActAmt"] = val.ToString();
                                }

                                try
                                {

                                    dr["DaysPresent"] = dtEmpAttendance.Rows[0]["Worked_Days"].ToString();
                                    dr["WeekOff"] = dtEmpAttendance.Rows[0]["Week_Off_Days"].ToString();
                                    dr["daysAbsent"] = dtEmpAttendance.Rows[0]["Absent_Days"].ToString();
                                    dr["Holiday"] = dtEmpAttendance.Rows[0]["Holiday_Days"].ToString();
                                    dr["Leaves"] = dtEmpAttendance.Rows[0]["Leave_Days"].ToString();
                                    dr["Totaldays"] = totaldays.ToString();


                                    dr["WorkedSal"] = dtEmpAttendance.Rows[0]["Basic_Work_Salary"].ToString();
                                    dr["LeavedaysSal"] = dtEmpAttendance.Rows[0]["Leave_Days_Salary"].ToString();
                                    dr["HolidaysSal"] = dtEmpAttendance.Rows[0]["Holiday_Days_Salary"].ToString();
                                    dr["WeekOffSal"] = dtEmpAttendance.Rows[0]["Week_Off_Days_Salary"].ToString();

                                    //double worksal = Convert.ToDouble(dtEmpAttendance.Rows[0]["Basic_Work_Salary"].ToString());
                                    //double leavesal = Convert.ToDouble(dtEmpAttendance.Rows[0]["Leave_Days_Salary"].ToString());
                                    //double holidayssal = Convert.ToDouble(dtEmpAttendance.Rows[0]["Holiday_Days_Salary"].ToString());
                                    //double weekofsal = Convert.ToDouble(dtEmpAttendance.Rows[0]["Week_Off_Days_Salary"].ToString());

                                    //totalattndsal = worksal + leavesal + holidayssal + weekofsal;
                                    dr["totalAttendSal"] = totalattndsal.ToString();

                                    dr["NormalOTSal"] = dtEmpAttendance.Rows[0]["Normal_OT_Work_Salary"].ToString();
                                    dr["WeekOffOTSal"] = dtEmpAttendance.Rows[0]["WeekOff_OT_Work_Salary"].ToString();
                                    dr["HolidaysOTSal"] = dtEmpAttendance.Rows[0]["Holiday_OT_Work_Salary"].ToString();

                                    //double noramlotsal = Convert.ToDouble(dtEmpAttendance.Rows[0]["Normal_OT_Work_Salary"].ToString());
                                    // double weekotsal = Convert.ToDouble(dtEmpAttendance.Rows[0]["WeekOff_OT_Work_Salary"].ToString());
                                    // double holidaotsal = Convert.ToDouble(dtEmpAttendance.Rows[0]["Holiday_OT_Work_Salary"].ToString());
                                    // totalOTsal = noramlotsal + weekotsal + holidaotsal;
                                    dr["TotalOTSal"] = totalOTsal.ToString();


                                    dr["LatePenaSal"] = dtEmpAttendance.Rows[0]["Late_Min_Penalty"].ToString();
                                    dr["EarlyPenaSal"] = dtEmpAttendance.Rows[0]["Early_Min_Penalty"].ToString();
                                    dr["PartialPenaSal"] = dtEmpAttendance.Rows[0]["Parital_Violation_Penalty"].ToString();
                                    dr["AbsentPenaSal"] = dtEmpAttendance.Rows[0]["Absent_Day_Penalty"].ToString();
                            }
                                catch 
                                { 
                                
                                }
                                // double latesal = Convert.ToDouble(dtEmpAttendance.Rows[0]["Late_Min_Penalty"].ToString());
                                // double earlysal = Convert.ToDouble(dtEmpAttendance.Rows[0]["Early_Min_Penalty"].ToString());
                                // double partialsal = Convert.ToDouble(dtEmpAttendance.Rows[0]["Parital_Violation_Penalty"].ToString());
                                // double absentsal = Convert.ToDouble(dtEmpAttendance.Rows[0]["Absent_Day_Penalty"].ToString());

                                //totalpenasal = latesal + earlysal + partialsal + absentsal;
                                 dr["TotalPenaltySal"] = totalpenasal.ToString();
                                 dr["GrossAttendanceSal"] = grossattendance.ToString();


                                dr["EmpCode"] = EmployeeCode;
                                dr["EmpName"] = EmployeeName;
                                DateTime dt = Convert.ToDateTime(dtEmpInfo.Rows[0]["DOJ"].ToString());
                                dr["DOJ"] = dt.ToString("dd-MMM-yyyy");

                                if (dtDesignation.Rows.Count > 0)
                                {
                                    dr["Designation"] = dtDesignation.Rows[0]["Designation"].ToString();
                                }
                                if (dtDepartment.Rows.Count > 0)
                                {
                                    dr["Department"] = dtDepartment.Rows[0]["Dep_Name"].ToString();
                                }
                                dr["Month"] = month[Convert.ToInt32(ddlMonth.SelectedIndex.ToString())].ToString();
                                dr["Year"] = TxtYear.Text.ToString();
                                dr["BankAccountNo"] = dtEmpInfo.Rows[0]["Account_No"].ToString();
                                //dr["DaysPresent"] = dtEmpAttendance.Rows[0]["Worked_Days"].ToString();
                                //dr["WeekOff"] = dtEmpAttendance.Rows[0]["Week_Off_Days"].ToString();
                                //dr["daysAbsent"] = dtEmpAttendance.Rows[0]["Absent_Days"].ToString();
                                //dr["Holiday"] = dtEmpAttendance.Rows[0]["Holiday_Days"].ToString();
                                //dr["Leaves"] = dtEmpAttendance.Rows[0]["Leave_Days"].ToString();

                                //totalDeducPenaLoan = sumdeduction + sumpenalty + sumloan;

                                //netsalary = totalAllowClaim - totalDeducPenaLoan + (prvmonthbal);



                                //dr["NetSalary"] = netsalary.ToString();
                              
                                dtEmployeePaySlip.Rows.Add(dr);


                            }

                        }

                        else
                        {
                            DataRow dr = dtEmployeePaySlip.NewRow();
                            dr["TypeAllow"] = "PENALTIES";
                            dr["AllowanceActAmt"] = "0";
                            dr["AllowanceName"] = "No Penalties";
                            try
                            {

                                dr["DaysPresent"] = dtEmpAttendance.Rows[0]["Worked_Days"].ToString();
                                dr["WeekOff"] = dtEmpAttendance.Rows[0]["Week_Off_Days"].ToString();
                                dr["daysAbsent"] = dtEmpAttendance.Rows[0]["Absent_Days"].ToString();
                                dr["Holiday"] = dtEmpAttendance.Rows[0]["Holiday_Days"].ToString();
                                dr["Leaves"] = dtEmpAttendance.Rows[0]["Leave_Days"].ToString();
                                dr["Totaldays"] = totaldays.ToString();


                                dr["WorkedSal"] = dtEmpAttendance.Rows[0]["Basic_Work_Salary"].ToString();
                                dr["LeavedaysSal"] = dtEmpAttendance.Rows[0]["Leave_Days_Salary"].ToString();
                                dr["HolidaysSal"] = dtEmpAttendance.Rows[0]["Holiday_Days_Salary"].ToString();
                                dr["WeekOffSal"] = dtEmpAttendance.Rows[0]["Week_Off_Days_Salary"].ToString();

                                dr["totalAttendSal"] = totalattndsal.ToString();

                                dr["NormalOTSal"] = dtEmpAttendance.Rows[0]["Normal_OT_Work_Salary"].ToString();
                                dr["WeekOffOTSal"] = dtEmpAttendance.Rows[0]["WeekOff_OT_Work_Salary"].ToString();
                                dr["HolidaysOTSal"] = dtEmpAttendance.Rows[0]["Holiday_OT_Work_Salary"].ToString();
                                dr["TotalOTSal"] = totalOTsal.ToString();


                                dr["LatePenaSal"] = dtEmpAttendance.Rows[0]["Late_Min_Penalty"].ToString();
                                dr["EarlyPenaSal"] = dtEmpAttendance.Rows[0]["Early_Min_Penalty"].ToString();
                                dr["PartialPenaSal"] = dtEmpAttendance.Rows[0]["Parital_Violation_Penalty"].ToString();
                                dr["AbsentPenaSal"] = dtEmpAttendance.Rows[0]["Absent_Day_Penalty"].ToString();
                            }
                            catch
                            {

                            }


                            dr["TotalPenaltySal"] = totalpenasal.ToString();
                            dr["GrossAttendanceSal"] = grossattendance.ToString();


                            dr["EmpCode"] = EmployeeCode;
                            dr["EmpName"] = EmployeeName;
                            DateTime dt = Convert.ToDateTime(dtEmpInfo.Rows[0]["DOJ"].ToString());
                            dr["DOJ"] = dt.ToString("dd-MMM-yyyy");
                            if (dtDesignation.Rows.Count > 0)
                            {
                                dr["Designation"] = dtDesignation.Rows[0]["Designation"].ToString();
                            }
                            if (dtDepartment.Rows.Count > 0)
                            {
                                dr["Department"] = dtDepartment.Rows[0]["Dep_Name"].ToString();
                            }
                            dr["Month"] = month[Convert.ToInt32(ddlMonth.SelectedIndex.ToString())].ToString();
                            dr["Year"] = TxtYear.Text.ToString();
                            dr["BankAccountNo"] = dtEmpInfo.Rows[0]["Account_No"].ToString();

                          

                            dtEmployeePaySlip.Rows.Add(dr);


                        }


                        dtLoan = new DataView(dtLoan, " Emp_Id=" + str + "", "", DataViewRowState.CurrentRows).ToTable();
                        if (dtLoan.Rows.Count > 0)
                        {
                            for (int l = 0; l < dtLoan.Rows.Count; l++)
                            {
                                DataRow dr = dtEmployeePaySlip.NewRow();
                                DataTable dtloandetial = new DataTable();

                                dtloandetial = objEmpLoan.GetRecord_From_PayEmployeeLoanDetail(dtLoan.Rows[l]["Loan_Id"].ToString());
                                dtloandetial = new DataView(dtloandetial, "Loan_Id=" + dtLoan.Rows[l]["Loan_Id"].ToString() + " and Month='" + dtloandetial.Rows[l]["Month"].ToString() + "' and Year=" + dtloandetial.Rows[l]["Year"].ToString() + " ", "", DataViewRowState.CurrentRows).ToTable();

                                if (dtloandetial.Rows.Count > 0)
                                {

                                    dr["TypeAllow"] = "LOANS";
                                    dr["AllowanceName"] = dtLoan.Rows[l]["Loan_Name"].ToString();
                                    dr["AllowanceActAmt"] = dtloandetial.Rows[l]["Total_Amount"].ToString();
                                    double loan = Convert.ToDouble(dtloandetial.Rows[l]["Total_Amount"].ToString());
                                    sumloan += loan;

                                }

                                try
                                {
                                    dr["DaysPresent"] = dtEmpAttendance.Rows[0]["Worked_Days"].ToString();
                                    dr["WeekOff"] = dtEmpAttendance.Rows[0]["Week_Off_Days"].ToString();
                                    dr["daysAbsent"] = dtEmpAttendance.Rows[0]["Absent_Days"].ToString();
                                    dr["Holiday"] = dtEmpAttendance.Rows[0]["Holiday_Days"].ToString();
                                    dr["Leaves"] = dtEmpAttendance.Rows[0]["Leave_Days"].ToString();
                                    dr["Totaldays"] = totaldays.ToString();


                                    dr["WorkedSal"] = dtEmpAttendance.Rows[0]["Basic_Work_Salary"].ToString();
                                    dr["LeavedaysSal"] = dtEmpAttendance.Rows[0]["Leave_Days_Salary"].ToString();
                                    dr["HolidaysSal"] = dtEmpAttendance.Rows[0]["Holiday_Days_Salary"].ToString();
                                    dr["WeekOffSal"] = dtEmpAttendance.Rows[0]["Week_Off_Days_Salary"].ToString();

                                    //double worksal = Convert.ToDouble(dtEmpAttendance.Rows[0]["Basic_Work_Salary"].ToString());
                                    //double leavesal = Convert.ToDouble(dtEmpAttendance.Rows[0]["Leave_Days_Salary"].ToString());
                                    //double holidayssal = Convert.ToDouble(dtEmpAttendance.Rows[0]["Holiday_Days_Salary"].ToString());
                                    //double weekofsal = Convert.ToDouble(dtEmpAttendance.Rows[0]["Week_Off_Days_Salary"].ToString());

                                    //totalattndsal = worksal + leavesal + holidayssal + weekofsal;
                                    dr["totalAttendSal"] = totalattndsal.ToString();

                                    dr["NormalOTSal"] = dtEmpAttendance.Rows[0]["Normal_OT_Work_Salary"].ToString();
                                    dr["WeekOffOTSal"] = dtEmpAttendance.Rows[0]["WeekOff_OT_Work_Salary"].ToString();
                                    dr["HolidaysOTSal"] = dtEmpAttendance.Rows[0]["Holiday_OT_Work_Salary"].ToString();

                                    //double noramlotsal = Convert.ToDouble(dtEmpAttendance.Rows[0]["Normal_OT_Work_Salary"].ToString());
                                    // double weekotsal = Convert.ToDouble(dtEmpAttendance.Rows[0]["WeekOff_OT_Work_Salary"].ToString());
                                    // double holidaotsal = Convert.ToDouble(dtEmpAttendance.Rows[0]["Holiday_OT_Work_Salary"].ToString());
                                    // totalOTsal = noramlotsal + weekotsal + holidaotsal;
                                    dr["TotalOTSal"] = totalOTsal.ToString();


                                    dr["LatePenaSal"] = dtEmpAttendance.Rows[0]["Late_Min_Penalty"].ToString();
                                    dr["EarlyPenaSal"] = dtEmpAttendance.Rows[0]["Early_Min_Penalty"].ToString();
                                    dr["PartialPenaSal"] = dtEmpAttendance.Rows[0]["Parital_Violation_Penalty"].ToString();
                                    dr["AbsentPenaSal"] = dtEmpAttendance.Rows[0]["Absent_Day_Penalty"].ToString();
                                }
                                catch 
                                {


                                }
                                // double latesal = Convert.ToDouble(dtEmpAttendance.Rows[0]["Late_Min_Penalty"].ToString());
                                // double earlysal = Convert.ToDouble(dtEmpAttendance.Rows[0]["Early_Min_Penalty"].ToString());
                                // double partialsal = Convert.ToDouble(dtEmpAttendance.Rows[0]["Parital_Violation_Penalty"].ToString());
                                // double absentsal = Convert.ToDouble(dtEmpAttendance.Rows[0]["Absent_Day_Penalty"].ToString());

                                //totalpenasal = latesal + earlysal + partialsal + absentsal;
                                 dr["TotalPenaltySal"] = totalpenasal.ToString();
                                 dr["GrossAttendanceSal"] = grossattendance.ToString();


                                dr["EmpCode"] = EmployeeCode;
                                dr["EmpName"] = EmployeeName;
                                DateTime dt = Convert.ToDateTime(dtEmpInfo.Rows[0]["DOJ"].ToString());
                                dr["DOJ"] = dt.ToString("dd-MMM-yyyy");
                                if (dtDesignation.Rows.Count > 0)
                                {
                                    dr["Designation"] = dtDesignation.Rows[0]["Designation"].ToString();
                                }
                                if (dtDepartment.Rows.Count > 0)
                                {
                                    dr["Department"] = dtDepartment.Rows[0]["Dep_Name"].ToString();
                                }
                                dr["Month"] = month[Convert.ToInt32(ddlMonth.SelectedIndex.ToString())].ToString();
                                dr["Year"] = TxtYear.Text.ToString();
                                dr["BankAccountNo"] = dtEmpInfo.Rows[0]["Account_No"].ToString();
                                //dr["DaysPresent"] = dtEmpAttendance.Rows[0]["Worked_Days"].ToString();
                                //dr["WeekOff"] = dtEmpAttendance.Rows[0]["Week_Off_Days"].ToString();
                                //dr["daysAbsent"] = dtEmpAttendance.Rows[0]["Absent_Days"].ToString();
                                //dr["Holiday"] = dtEmpAttendance.Rows[0]["Holiday_Days"].ToString();
                                //dr["Leaves"] = dtEmpAttendance.Rows[0]["Leave_Days"].ToString();


                                //totalDeducPenaLoan = sumdeduction + sumpenalty + sumloan;

                                //netsalary = totalAllowClaim - totalDeducPenaLoan + (prvmonthbal);

                                //dr["NetSalary"] = netsalary.ToString();
                              
                                dtEmployeePaySlip.Rows.Add(dr);


                            }

                        }

                        else
                        {
                            DataRow dr = dtEmployeePaySlip.NewRow();
                            dr["TypeAllow"] = "LOANS";
                            dr["AllowanceActAmt"] = "0";
                            dr["AllowanceName"] = "No Loans";
                            try
                            {

                                dr["DaysPresent"] = dtEmpAttendance.Rows[0]["Worked_Days"].ToString();
                                dr["WeekOff"] = dtEmpAttendance.Rows[0]["Week_Off_Days"].ToString();
                                dr["daysAbsent"] = dtEmpAttendance.Rows[0]["Absent_Days"].ToString();
                                dr["Holiday"] = dtEmpAttendance.Rows[0]["Holiday_Days"].ToString();
                                dr["Leaves"] = dtEmpAttendance.Rows[0]["Leave_Days"].ToString();
                                dr["Totaldays"] = totaldays.ToString();


                                dr["WorkedSal"] = dtEmpAttendance.Rows[0]["Basic_Work_Salary"].ToString();
                                dr["LeavedaysSal"] = dtEmpAttendance.Rows[0]["Leave_Days_Salary"].ToString();
                                dr["HolidaysSal"] = dtEmpAttendance.Rows[0]["Holiday_Days_Salary"].ToString();
                                dr["WeekOffSal"] = dtEmpAttendance.Rows[0]["Week_Off_Days_Salary"].ToString();

                                dr["totalAttendSal"] = totalattndsal.ToString();

                                dr["NormalOTSal"] = dtEmpAttendance.Rows[0]["Normal_OT_Work_Salary"].ToString();
                                dr["WeekOffOTSal"] = dtEmpAttendance.Rows[0]["WeekOff_OT_Work_Salary"].ToString();
                                dr["HolidaysOTSal"] = dtEmpAttendance.Rows[0]["Holiday_OT_Work_Salary"].ToString();
                                dr["TotalOTSal"] = totalOTsal.ToString();


                                dr["LatePenaSal"] = dtEmpAttendance.Rows[0]["Late_Min_Penalty"].ToString();
                                dr["EarlyPenaSal"] = dtEmpAttendance.Rows[0]["Early_Min_Penalty"].ToString();
                                dr["PartialPenaSal"] = dtEmpAttendance.Rows[0]["Parital_Violation_Penalty"].ToString();
                                dr["AbsentPenaSal"] = dtEmpAttendance.Rows[0]["Absent_Day_Penalty"].ToString();
                            }
                            catch
                            {

                            }


                            dr["TotalPenaltySal"] = totalpenasal.ToString();
                            dr["GrossAttendanceSal"] = grossattendance.ToString();


                            dr["EmpCode"] = EmployeeCode;
                            dr["EmpName"] = EmployeeName;
                            DateTime dt = Convert.ToDateTime(dtEmpInfo.Rows[0]["DOJ"].ToString());
                            dr["DOJ"] = dt.ToString("dd-MMM-yyyy");
                            if (dtDesignation.Rows.Count > 0)
                            {
                                dr["Designation"] = dtDesignation.Rows[0]["Designation"].ToString();
                            }
                            if (dtDepartment.Rows.Count > 0)
                            {
                                dr["Department"] = dtDepartment.Rows[0]["Dep_Name"].ToString();
                            }
                            dr["Month"] = month[Convert.ToInt32(ddlMonth.SelectedIndex.ToString())].ToString();
                            dr["Year"] = TxtYear.Text.ToString();
                            dr["BankAccountNo"] = dtEmpInfo.Rows[0]["Account_No"].ToString();

                          
                            dtEmployeePaySlip.Rows.Add(dr);


                        }

                       
                        DataTable dtEmpPrvablmonth = new DataTable();

                        dtEmpPrvablmonth = objPayEmpMonth.GetAllRecordPostedEmpMonth(str, ddlMonth.SelectedValue.ToString(), TxtYear.Text);
                        //dtPrvMonthBal = objPayEmpMonth.GetRecord_PrvBal_Fields(str, ddlMonth.SelectedValue.ToString(), TxtYear.Text);
                        if (dtEmpPrvablmonth.Rows.Count > 0)
                        {
                            for (int prv = 0; prv < dtEmpPrvablmonth.Rows.Count; prv++)
                            {
                                DataRow dr = dtEmployeePaySlip.NewRow();
                               dr["TypeAllow"] = "PREVIOUS MONTH ADJUSTMENTS";
                               dr["AllowanceName"] = "PREVIOUS MONTH";
                               dr["AllowanceActAmt"] = dtEmpPrvablmonth.Rows[0]["Previous_Month_Balance"].ToString();

                               double pr = Convert.ToDouble(dtEmpPrvablmonth.Rows[0]["Previous_Month_Balance"].ToString());
                                prvmonthbal += pr;
                                try
                                {
                                    dr["DaysPresent"] = dtEmpAttendance.Rows[0]["Worked_Days"].ToString();
                                    dr["WeekOff"] = dtEmpAttendance.Rows[0]["Week_Off_Days"].ToString();
                                    dr["daysAbsent"] = dtEmpAttendance.Rows[0]["Absent_Days"].ToString();
                                    dr["Holiday"] = dtEmpAttendance.Rows[0]["Holiday_Days"].ToString();
                                    dr["Leaves"] = dtEmpAttendance.Rows[0]["Leave_Days"].ToString();
                                    dr["Totaldays"] = totaldays.ToString();


                                    dr["WorkedSal"] = dtEmpAttendance.Rows[0]["Basic_Work_Salary"].ToString();
                                    dr["LeavedaysSal"] = dtEmpAttendance.Rows[0]["Leave_Days_Salary"].ToString();
                                    dr["HolidaysSal"] = dtEmpAttendance.Rows[0]["Holiday_Days_Salary"].ToString();
                                    dr["WeekOffSal"] = dtEmpAttendance.Rows[0]["Week_Off_Days_Salary"].ToString();

                                    //double worksal = Convert.ToDouble(dtEmpAttendance.Rows[0]["Basic_Work_Salary"].ToString());
                                    //double leavesal = Convert.ToDouble(dtEmpAttendance.Rows[0]["Leave_Days_Salary"].ToString());
                                    //double holidayssal = Convert.ToDouble(dtEmpAttendance.Rows[0]["Holiday_Days_Salary"].ToString());
                                    //double weekofsal = Convert.ToDouble(dtEmpAttendance.Rows[0]["Week_Off_Days_Salary"].ToString());

                                    //totalattndsal = worksal + leavesal + holidayssal + weekofsal;
                                    dr["totalAttendSal"] = totalattndsal.ToString();

                                    dr["NormalOTSal"] = dtEmpAttendance.Rows[0]["Normal_OT_Work_Salary"].ToString();
                                    dr["WeekOffOTSal"] = dtEmpAttendance.Rows[0]["WeekOff_OT_Work_Salary"].ToString();
                                    dr["HolidaysOTSal"] = dtEmpAttendance.Rows[0]["Holiday_OT_Work_Salary"].ToString();

                                    //double noramlotsal = Convert.ToDouble(dtEmpAttendance.Rows[0]["Normal_OT_Work_Salary"].ToString());
                                    // double weekotsal = Convert.ToDouble(dtEmpAttendance.Rows[0]["WeekOff_OT_Work_Salary"].ToString());
                                    // double holidaotsal = Convert.ToDouble(dtEmpAttendance.Rows[0]["Holiday_OT_Work_Salary"].ToString());
                                    // totalOTsal = noramlotsal + weekotsal + holidaotsal;
                                    dr["TotalOTSal"] = totalOTsal.ToString();


                                    dr["LatePenaSal"] = dtEmpAttendance.Rows[0]["Late_Min_Penalty"].ToString();
                                    dr["EarlyPenaSal"] = dtEmpAttendance.Rows[0]["Early_Min_Penalty"].ToString();
                                    dr["PartialPenaSal"] = dtEmpAttendance.Rows[0]["Parital_Violation_Penalty"].ToString();
                                    dr["AbsentPenaSal"] = dtEmpAttendance.Rows[0]["Absent_Day_Penalty"].ToString();
                                }
                                catch 
                                {

                                }
                                // double latesal = Convert.ToDouble(dtEmpAttendance.Rows[0]["Late_Min_Penalty"].ToString());
                                // double earlysal = Convert.ToDouble(dtEmpAttendance.Rows[0]["Early_Min_Penalty"].ToString());
                                // double partialsal = Convert.ToDouble(dtEmpAttendance.Rows[0]["Parital_Violation_Penalty"].ToString());
                                // double absentsal = Convert.ToDouble(dtEmpAttendance.Rows[0]["Absent_Day_Penalty"].ToString());

                                //totalpenasal = latesal + earlysal + partialsal + absentsal;
                                 dr["TotalPenaltySal"] = totalpenasal.ToString();
                                 dr["GrossAttendanceSal"] = grossattendance.ToString();


                                dr["EmpCode"] = EmployeeCode;
                                dr["EmpName"] = EmployeeName;
                                DateTime dt = Convert.ToDateTime(dtEmpInfo.Rows[0]["DOJ"].ToString());
                                dr["DOJ"] = dt.ToString("dd-MMM-yyyy");
                                if (dtDesignation.Rows.Count > 0)
                                {
                                    dr["Designation"] = dtDesignation.Rows[0]["Designation"].ToString();
                                }
                                if (dtDepartment.Rows.Count > 0)
                                {
                                    dr["Department"] = dtDepartment.Rows[0]["Dep_Name"].ToString();
                                }
                                dr["Month"] = month[Convert.ToInt32(ddlMonth.SelectedIndex.ToString())].ToString();
                                dr["Year"] = TxtYear.Text.ToString();
                                dr["BankAccountNo"] = dtEmpInfo.Rows[0]["Account_No"].ToString();
                               
                              
                                dtEmployeePaySlip.Rows.Add(dr);


                            }

                        }
                        else
                        {
                        DataRow dr = dtEmployeePaySlip.NewRow();
                        dr["TypeAllow"] = "PREVIOUS MONTH ADJUSTMENTS";
                            dr["AllowanceActAmt"] = "0";
                            dr["AllowanceName"] = "No Adjustment";
                            try
                            {

                                dr["DaysPresent"] = dtEmpAttendance.Rows[0]["Worked_Days"].ToString();
                                dr["WeekOff"] = dtEmpAttendance.Rows[0]["Week_Off_Days"].ToString();
                                dr["daysAbsent"] = dtEmpAttendance.Rows[0]["Absent_Days"].ToString();
                                dr["Holiday"] = dtEmpAttendance.Rows[0]["Holiday_Days"].ToString();
                                dr["Leaves"] = dtEmpAttendance.Rows[0]["Leave_Days"].ToString();
                                dr["Totaldays"] = totaldays.ToString();


                                dr["WorkedSal"] = dtEmpAttendance.Rows[0]["Basic_Work_Salary"].ToString();
                                dr["LeavedaysSal"] = dtEmpAttendance.Rows[0]["Leave_Days_Salary"].ToString();
                                dr["HolidaysSal"] = dtEmpAttendance.Rows[0]["Holiday_Days_Salary"].ToString();
                                dr["WeekOffSal"] = dtEmpAttendance.Rows[0]["Week_Off_Days_Salary"].ToString();

                                dr["totalAttendSal"] = totalattndsal.ToString();

                                dr["NormalOTSal"] = dtEmpAttendance.Rows[0]["Normal_OT_Work_Salary"].ToString();
                                dr["WeekOffOTSal"] = dtEmpAttendance.Rows[0]["WeekOff_OT_Work_Salary"].ToString();
                                dr["HolidaysOTSal"] = dtEmpAttendance.Rows[0]["Holiday_OT_Work_Salary"].ToString();
                                dr["TotalOTSal"] = totalOTsal.ToString();


                                dr["LatePenaSal"] = dtEmpAttendance.Rows[0]["Late_Min_Penalty"].ToString();
                                dr["EarlyPenaSal"] = dtEmpAttendance.Rows[0]["Early_Min_Penalty"].ToString();
                                dr["PartialPenaSal"] = dtEmpAttendance.Rows[0]["Parital_Violation_Penalty"].ToString();
                                dr["AbsentPenaSal"] = dtEmpAttendance.Rows[0]["Absent_Day_Penalty"].ToString();
                            }
                            catch
                            {

                            }


                            dr["TotalPenaltySal"] = totalpenasal.ToString();
                            dr["GrossAttendanceSal"] = grossattendance.ToString();


                            dr["EmpCode"] = EmployeeCode;
                            dr["EmpName"] = EmployeeName;
                            DateTime dt = Convert.ToDateTime(dtEmpInfo.Rows[0]["DOJ"].ToString());
                            dr["DOJ"] = dt.ToString("dd-MMM-yyyy");
                            if (dtDesignation.Rows.Count > 0)
                            {
                                dr["Designation"] = dtDesignation.Rows[0]["Designation"].ToString();
                            }
                            if (dtDepartment.Rows.Count > 0)
                            {
                                dr["Department"] = dtDepartment.Rows[0]["Dep_Name"].ToString();
                            }
                            dr["Month"] = month[Convert.ToInt32(ddlMonth.SelectedIndex.ToString())].ToString();
                            dr["Year"] = TxtYear.Text.ToString();
                            dr["BankAccountNo"] = dtEmpInfo.Rows[0]["Account_No"].ToString();

                         

                            dtEmployeePaySlip.Rows.Add(dr);


                        }


                        if (dtDeuction.Rows.Count > 0)
                        {

                            for (int k = 0; k < dtDeuction.Rows.Count; k++)
                            {
                                DataRow dr = dtEmployeePaySlip.NewRow();
                                dr["TypeAllow"] = "DEDUCTIONS";
                                dr["AllowanceActAmt"] = dtDeuction.Rows[k]["Act_Deduction_Value"].ToString();
                                double deduc = Convert.ToDouble(dtDeuction.Rows[k]["Act_Deduction_Value"].ToString());
                                sumdeduction += deduc;

                                if (dtDeuction.Rows[k]["Deduction_Id"].ToString() != "0" && dtDeuction.Rows[k]["Deduction_Id"].ToString() != "")
                                {
                                    DataTable dtDeductionname = objDeduction.GetDeductionTruebyId(Session["CompId"].ToString(), dtDeuction.Rows[k]["Deduction_Id"].ToString());

                                    dtDeductionname = new DataView(dtDeductionname, "Deduction_Id=" + dtDeuction.Rows[k]["Deduction_Id"].ToString() + " ", "", DataViewRowState.CurrentRows).ToTable();
                                    if (dtDeductionname.Rows.Count > 0)
                                    {
                                        dr["AllowanceName"] = dtDeductionname.Rows[0]["Deduction"].ToString();
                                    }
                                    else
                                    {
                                        dr["AllowanceName"] = "";
                                    }

                                }
                                try
                                {

                                    dr["DaysPresent"] = dtEmpAttendance.Rows[0]["Worked_Days"].ToString();
                                    dr["WeekOff"] = dtEmpAttendance.Rows[0]["Week_Off_Days"].ToString();
                                    dr["daysAbsent"] = dtEmpAttendance.Rows[0]["Absent_Days"].ToString();
                                    dr["Holiday"] = dtEmpAttendance.Rows[0]["Holiday_Days"].ToString();
                                    dr["Leaves"] = dtEmpAttendance.Rows[0]["Leave_Days"].ToString();
                                    dr["Totaldays"] = totaldays.ToString();


                                    dr["WorkedSal"] = dtEmpAttendance.Rows[0]["Basic_Work_Salary"].ToString();
                                    dr["LeavedaysSal"] = dtEmpAttendance.Rows[0]["Leave_Days_Salary"].ToString();
                                    dr["HolidaysSal"] = dtEmpAttendance.Rows[0]["Holiday_Days_Salary"].ToString();
                                    dr["WeekOffSal"] = dtEmpAttendance.Rows[0]["Week_Off_Days_Salary"].ToString();

                                    dr["totalAttendSal"] = totalattndsal.ToString();

                                    dr["NormalOTSal"] = dtEmpAttendance.Rows[0]["Normal_OT_Work_Salary"].ToString();
                                    dr["WeekOffOTSal"] = dtEmpAttendance.Rows[0]["WeekOff_OT_Work_Salary"].ToString();
                                    dr["HolidaysOTSal"] = dtEmpAttendance.Rows[0]["Holiday_OT_Work_Salary"].ToString();
                                    dr["TotalOTSal"] = totalOTsal.ToString();


                                    dr["LatePenaSal"] = dtEmpAttendance.Rows[0]["Late_Min_Penalty"].ToString();
                                    dr["EarlyPenaSal"] = dtEmpAttendance.Rows[0]["Early_Min_Penalty"].ToString();
                                    dr["PartialPenaSal"] = dtEmpAttendance.Rows[0]["Parital_Violation_Penalty"].ToString();
                                    dr["AbsentPenaSal"] = dtEmpAttendance.Rows[0]["Absent_Day_Penalty"].ToString();
                                }
                                catch
                                {

                                }


                                dr["TotalPenaltySal"] = totalpenasal.ToString();
                                dr["GrossAttendanceSal"] = grossattendance.ToString();


                                dr["EmpCode"] = EmployeeCode;
                                dr["EmpName"] = EmployeeName;
                                DateTime dt = Convert.ToDateTime(dtEmpInfo.Rows[0]["DOJ"].ToString());
                                dr["DOJ"] = dt.ToString("dd-MMM-yyyy");
                                if (dtDesignation.Rows.Count > 0)
                                {
                                    dr["Designation"] = dtDesignation.Rows[0]["Designation"].ToString();
                                }
                                if (dtDepartment.Rows.Count > 0)
                                {
                                    dr["Department"] = dtDepartment.Rows[0]["Dep_Name"].ToString();
                                }
                                dr["Month"] = month[Convert.ToInt32(ddlMonth.SelectedIndex.ToString())].ToString();
                                dr["Year"] = TxtYear.Text.ToString();
                                dr["BankAccountNo"] = dtEmpInfo.Rows[0]["Account_No"].ToString();

                                totalAllowClaim = sumallowance + sumclaim + grossattendance;

                                totalDeducPenaLoan = sumdeduction + sumpenalty + sumloan;

                                netsalary = totalAllowClaim - totalDeducPenaLoan + (prvmonthbal);
                                dr["NetSalary"] = netsalary.ToString();

                                dtEmployeePaySlip.Rows.Add(dr);

                            }

                        }
                        else
                        {

                            DataRow dr = dtEmployeePaySlip.NewRow();
                          
                         
                            dr["TypeAllow"] = "DEDUCTIONS";
                            dr["AllowanceActAmt"] = "0";
                            dr["AllowanceName"] = "No Deductions";
                            try
                            {
                                dr["DaysPresent"] = dtEmpAttendance.Rows[0]["Worked_Days"].ToString();
                                dr["WeekOff"] = dtEmpAttendance.Rows[0]["Week_Off_Days"].ToString();
                                dr["daysAbsent"] = dtEmpAttendance.Rows[0]["Absent_Days"].ToString();
                                dr["Holiday"] = dtEmpAttendance.Rows[0]["Holiday_Days"].ToString();
                                dr["Leaves"] = dtEmpAttendance.Rows[0]["Leave_Days"].ToString();
                                dr["Totaldays"] = totaldays.ToString();


                                dr["WorkedSal"] = dtEmpAttendance.Rows[0]["Basic_Work_Salary"].ToString();
                                dr["LeavedaysSal"] = dtEmpAttendance.Rows[0]["Leave_Days_Salary"].ToString();
                                dr["HolidaysSal"] = dtEmpAttendance.Rows[0]["Holiday_Days_Salary"].ToString();
                                dr["WeekOffSal"] = dtEmpAttendance.Rows[0]["Week_Off_Days_Salary"].ToString();

                                dr["totalAttendSal"] = totalattndsal.ToString();

                                dr["NormalOTSal"] = dtEmpAttendance.Rows[0]["Normal_OT_Work_Salary"].ToString();
                                dr["WeekOffOTSal"] = dtEmpAttendance.Rows[0]["WeekOff_OT_Work_Salary"].ToString();
                                dr["HolidaysOTSal"] = dtEmpAttendance.Rows[0]["Holiday_OT_Work_Salary"].ToString();
                                dr["TotalOTSal"] = totalOTsal.ToString();


                                dr["LatePenaSal"] = dtEmpAttendance.Rows[0]["Late_Min_Penalty"].ToString();
                                dr["EarlyPenaSal"] = dtEmpAttendance.Rows[0]["Early_Min_Penalty"].ToString();
                                dr["PartialPenaSal"] = dtEmpAttendance.Rows[0]["Parital_Violation_Penalty"].ToString();
                                dr["AbsentPenaSal"] = dtEmpAttendance.Rows[0]["Absent_Day_Penalty"].ToString();
                            }
                            catch
                            {

                            }

                            dr["TotalPenaltySal"] = totalpenasal.ToString();
                            dr["GrossAttendanceSal"] = grossattendance.ToString();
                            dr["EmpCode"] = EmployeeCode;
                            dr["EmpName"] = EmployeeName;
                            DateTime dt = Convert.ToDateTime(dtEmpInfo.Rows[0]["DOJ"].ToString());
                            dr["DOJ"] = dt.ToString("dd-MMM-yyyy");
                            if (dtDesignation.Rows.Count > 0)
                            {
                                dr["Designation"] = dtDesignation.Rows[0]["Designation"].ToString();
                            }
                            if (dtDepartment.Rows.Count > 0)
                            {
                                dr["Department"] = dtDepartment.Rows[0]["Dep_Name"].ToString();
                            }
                            dr["Month"] = month[Convert.ToInt32(ddlMonth.SelectedIndex.ToString())].ToString();
                            dr["Year"] = TxtYear.Text.ToString();
                            dr["BankAccountNo"] = dtEmpInfo.Rows[0]["Account_No"].ToString();

                            totalAllowClaim = sumallowance + sumclaim + grossattendance;
                            totalDeducPenaLoan = sumdeduction + sumpenalty + sumloan;
                            netsalary = totalAllowClaim - totalDeducPenaLoan + (prvmonthbal);
                            dr["NetSalary"] = netsalary.ToString();

                            dtEmployeePaySlip.Rows.Add(dr);
                        }
                       

                    }
      
                }
                //DataRow dr1 = dtEmployeePaySlip.NewRow();
                //// here we get net salary
                //totalAllowClaim = sumallowance + sumclaim + grossattendance;
                //totalDeducPenaLoan = sumdeduction + sumpenalty + sumloan;
                //netsalary = totalAllowClaim - totalDeducPenaLoan + (prvmonthbal);
                //dr1["NetSalary"] = netsalary.ToString();
                //dr1["EmpCode"] = EmployeeCode;

                //dtEmployeePaySlip.Rows.Add(dr1);

               // totalAllowClaim = sumallowance + sumclaim + grossattendance;
               // totalDeducPenaLoan = sumdeduction + sumpenalty + sumloan;
               //netsalary = totalAllowClaim - totalDeducPenaLoan + (prvmonthbal);
               //Session["netsalary"] = netsalary.ToString();
               //objEmpPaySlip.setnetamount(Session["netsalary"].ToString());
                // objEmpPaySlip.setnetamount(netsalary.ToString());

            }
        }

        if (dtEmployeePaySlip.Rows.Count > 0)
        {

            Session["dtEmpPaySlipRecords"] = dtEmployeePaySlip;
            Response.Redirect("../HR_Report/Employee_PaySlip_Report.aspx");
        }
        else
        {
            DisplayMessage("Record Not found");
        }
       


    }
    


       protected void btnAttendReport_Click(object sender, EventArgs e)
         {
           
             double netsalary = 0;
           
             double basicsal = 0;
             double totalattndsal = 0;
             double totalOTsal = 0;
             double totalpenasal = 0;

             string doj = "";
             string EmployeeName = "";
             string EmployeeCode = "";
             string[] month = new string[13];
             month[0] = "--Select--";
             month[1] = "JAN'";
             month[2] = "FEB'";
             month[3] = "MAR'";
             month[4] = "APR'";
             month[5] = "MAY'";
             month[6] = "JUN'";
             month[7] = "JUL'";
             month[8] = "AUG'";
             month[9] = "SEP'";
             month[10] = "OCT'";
             month[11] = "NOV'";
             month[12] = "DEC'";

             DataTable dtEmpAttendSalary = new DataTable();
             DataTable dtEmpRecords = new DataTable();

             dtEmpAttendSalary.Columns.Add("EmpCode");
             dtEmpAttendSalary.Columns.Add("EmpName");

             dtEmpAttendSalary.Columns.Add("Designation");
             dtEmpAttendSalary.Columns.Add("Department");
             dtEmpAttendSalary.Columns.Add("DOJ");
             dtEmpAttendSalary.Columns.Add("Month");
             dtEmpAttendSalary.Columns.Add("Year");
             dtEmpAttendSalary.Columns.Add("BankAccountNo");
            
             dtEmpAttendSalary.Columns.Add("NetSalary");
             //Attendance Days
             dtEmpAttendSalary.Columns.Add("DaysPresent");
             dtEmpAttendSalary.Columns.Add("WeekOff");
             dtEmpAttendSalary.Columns.Add("Holiday");
             dtEmpAttendSalary.Columns.Add("Leaves");
             //Attendance Salary
             dtEmpAttendSalary.Columns.Add("WorkedSal");
             dtEmpAttendSalary.Columns.Add("WeekOffSal");
             dtEmpAttendSalary.Columns.Add("HolidaysSal");
             dtEmpAttendSalary.Columns.Add("LeavedaysSal");
           //Overtime Days
             dtEmpAttendSalary.Columns.Add("NormalOT");
             dtEmpAttendSalary.Columns.Add("WeekOT");
             dtEmpAttendSalary.Columns.Add("HolidayOT");
           //Overtime Salary
             dtEmpAttendSalary.Columns.Add("NormalOTSal");
             dtEmpAttendSalary.Columns.Add("WeekOffOTSal");
             dtEmpAttendSalary.Columns.Add("HolidaysOTSal");
           //Penalty Days/Min
             dtEmpAttendSalary.Columns.Add("LatePenalty");
             dtEmpAttendSalary.Columns.Add("EarlyPenalty");
             dtEmpAttendSalary.Columns.Add("PartialPenalty");
             dtEmpAttendSalary.Columns.Add("AbsentPenalty");
           //Penalty Sal
             dtEmpAttendSalary.Columns.Add("LatePenaSal");
             dtEmpAttendSalary.Columns.Add("EarlyPenaSal");
             dtEmpAttendSalary.Columns.Add("PartialPenaSal");
             dtEmpAttendSalary.Columns.Add("AbsentPenaSal");
             // all Total Amount 

             dtEmpAttendSalary.Columns.Add("totalAttendSal");
             dtEmpAttendSalary.Columns.Add("TotalOTSal");
             dtEmpAttendSalary.Columns.Add("TotalPenaltySal");
            
            



             dtEmpAttendSalary.Columns.Add("DeductionName");
             dtEmpAttendSalary.Columns.Add("DeductionActAmt");



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

                     foreach (string str in EmpIds.Split(','))
                     {
                         

                         if ((str != ""))
                         {

                             EmployeeName = GetEmployeeName(str);
                             EmployeeCode = GetEmployeeCode(str);
                             DataTable dtemppara = new DataTable();
                             dtemppara = objempparam.GetEmployeeParameterByEmpId(str, Session["CompId"].ToString());
                             if (dtemppara.Rows.Count > 0)
                             {

                                 basicsal = Convert.ToDouble(dtemppara.Rows[0]["Basic_Salary"].ToString());
                             }


                             EmployeeName = GetEmployeeName(str);
                             EmployeeCode = GetEmployeeCode(str);


                             netsalary = 0;



                             DataTable dtEmpInfo = new DataTable();
                             DataTable dtDepartment = new DataTable();
                             DataTable dtDesignation = new DataTable();

                             DataTable dtPayEmpMonth = new DataTable();
                             DataTable dtEmpAttendance = new DataTable();


                             dtEmpInfo = objEmp.GetEmployeeMasterByEmpCode(Session["CompId"].ToString(), EmployeeCode.ToString());
                             dtDepartment = objDep.GetDepartmentMasterById(Session["CompId"].ToString(), dtEmpInfo.Rows[0]["Department_Id"].ToString());
                             dtDesignation = objDesg.GetDesignationMasterById(Session["CompId"].ToString(), dtEmpInfo.Rows[0]["Designation_Id"].ToString());

                             dtPayEmpMonth = objPayEmpMonth.Getallrecords(str);
                             dtPayEmpMonth = new DataView(dtPayEmpMonth, "Emp_Id=" + str + " and Month=" + ddlMonth.SelectedValue.ToString() + " and Year='" + TxtYear.Text + "'", "", DataViewRowState.CurrentRows).ToTable();



                             dtEmpAttendance = objEmpAttendance.GetRecord_Emp_Attendance(str, ddlMonth.SelectedValue.ToString(), TxtYear.Text);



                             for (int i = 0; i < dtEmpInfo.Rows.Count; i++)
                             {
                                 if (dtEmpAttendance.Rows.Count > 0)
                                 {

                                     for (int a = 0; a < dtEmpAttendance.Rows.Count; a++)
                                     {
                                         DataRow dr = dtEmpAttendSalary.NewRow();

                                         dr["DaysPresent"] = dtEmpAttendance.Rows[0]["Worked_Days"].ToString();
                                         dr["WeekOff"] = dtEmpAttendance.Rows[0]["Week_Off_Days"].ToString();
                                         dr["Holiday"] = dtEmpAttendance.Rows[0]["Holiday_Days"].ToString();
                                         dr["Leaves"] = dtEmpAttendance.Rows[0]["Leave_Days"].ToString();

                                         dr["NormalOT"] = dtEmpAttendance.Rows[0]["Normal_OT_Min"].ToString();
                                         dr["WeekOT"] = dtEmpAttendance.Rows[0]["Week_Off_OT_Min"].ToString();
                                         dr["HolidayOT"] = dtEmpAttendance.Rows[0]["Holiday_OT_Min"].ToString();

                                         dr["LatePenalty"] = dtEmpAttendance.Rows[0]["Late_Penalty_Min"].ToString();
                                         dr["EarlyPenalty"] = dtEmpAttendance.Rows[0]["Early_Penalty_Min"].ToString();
                                         dr["PartialPenalty"] = dtEmpAttendance.Rows[0]["Partial_Penalty_Min"].ToString();
                                         dr["AbsentPenalty"] = dtEmpAttendance.Rows[0]["Absent_Penalty"].ToString();


                                         dr["WorkedSal"] = dtEmpAttendance.Rows[0]["Basic_Work_Salary"].ToString();
                                         dr["LeavedaysSal"] = dtEmpAttendance.Rows[0]["Leave_Days_Salary"].ToString();
                                         dr["HolidaysSal"] = dtEmpAttendance.Rows[0]["Holiday_Days_Salary"].ToString();
                                         dr["WeekOffSal"] = dtEmpAttendance.Rows[0]["Week_Off_Days_Salary"].ToString();

                                         double worksal = Convert.ToDouble(dtEmpAttendance.Rows[0]["Basic_Work_Salary"].ToString());
                                         double leavesal = Convert.ToDouble(dtEmpAttendance.Rows[0]["Leave_Days_Salary"].ToString());
                                         double holidayssal = Convert.ToDouble(dtEmpAttendance.Rows[0]["Holiday_Days_Salary"].ToString());
                                         double weekofsal = Convert.ToDouble(dtEmpAttendance.Rows[0]["Week_Off_Days_Salary"].ToString());

                                         totalattndsal = worksal + leavesal + holidayssal + weekofsal;
                                         dr["totalAttendSal"] = totalattndsal.ToString();

                                         dr["NormalOTSal"] = dtEmpAttendance.Rows[0]["Normal_OT_Work_Salary"].ToString();
                                         dr["WeekOffOTSal"] = dtEmpAttendance.Rows[0]["WeekOff_OT_Work_Salary"].ToString();
                                         dr["HolidaysOTSal"] = dtEmpAttendance.Rows[0]["Holiday_OT_Work_Salary"].ToString();

                                         double noramlotsal = Convert.ToDouble(dtEmpAttendance.Rows[0]["Normal_OT_Work_Salary"].ToString());
                                         double weekotsal = Convert.ToDouble(dtEmpAttendance.Rows[0]["WeekOff_OT_Work_Salary"].ToString());
                                         double holidaotsal = Convert.ToDouble(dtEmpAttendance.Rows[0]["Holiday_OT_Work_Salary"].ToString());
                                         totalOTsal = noramlotsal + weekotsal + holidaotsal;
                                         dr["TotalOTSal"] = totalOTsal.ToString();


                                         dr["LatePenaSal"] = dtEmpAttendance.Rows[0]["Late_Min_Penalty"].ToString();
                                         dr["EarlyPenaSal"] = dtEmpAttendance.Rows[0]["Early_Min_Penalty"].ToString();
                                         dr["PartialPenaSal"] = dtEmpAttendance.Rows[0]["Parital_Violation_Penalty"].ToString();
                                         dr["AbsentPenaSal"] = dtEmpAttendance.Rows[0]["Absent_Day_Penalty"].ToString();

                                         double latesal = Convert.ToDouble(dtEmpAttendance.Rows[0]["Late_Min_Penalty"].ToString());
                                         double earlysal = Convert.ToDouble(dtEmpAttendance.Rows[0]["Early_Min_Penalty"].ToString());
                                         double partialsal = Convert.ToDouble(dtEmpAttendance.Rows[0]["Parital_Violation_Penalty"].ToString());
                                         double absentsal = Convert.ToDouble(dtEmpAttendance.Rows[0]["Absent_Day_Penalty"].ToString());

                                         totalpenasal = latesal + earlysal + partialsal + absentsal;
                                         dr["TotalPenaltySal"] = totalpenasal.ToString();

                                         dr["EmpCode"] = EmployeeCode;
                                         dr["EmpName"] = EmployeeName;
                                         DateTime dt = Convert.ToDateTime(dtEmpInfo.Rows[0]["DOJ"].ToString());
                                         dr["DOJ"] = dt.ToString("dd-MMM-yyyy");


                                         if (dtDesignation.Rows.Count > 0)
                                         {
                                             dr["Designation"] = dtDesignation.Rows[0]["Designation"].ToString();
                                         }
                                         if (dtDepartment.Rows.Count > 0)
                                         {
                                             dr["Department"] = dtDepartment.Rows[0]["Dep_Name"].ToString();
                                         }
                                         dr["Month"] = month[Convert.ToInt32(ddlMonth.SelectedIndex.ToString())].ToString();
                                         dr["Year"] = TxtYear.Text.ToString();
                                         dr["BankAccountNo"] = dtEmpInfo.Rows[0]["Account_No"].ToString();
                                         netsalary = (totalattndsal + totalOTsal) - totalpenasal;
                                         dr["NetSalary"] = netsalary.ToString();

                                         dtEmpAttendSalary.Rows.Add(dr);


                                     }

                                 }





                             }

                            

                             

                             }


                         }


                     }

                 
                 else
                 {
                     DisplayMessage("Select Group First");
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
                         DataTable dtemppara = new DataTable();
                         dtemppara = objempparam.GetEmployeeParameterByEmpId(str, Session["CompId"].ToString());
                         if (dtemppara.Rows.Count > 0)
                         {

                             basicsal = Convert.ToDouble(dtemppara.Rows[0]["Basic_Salary"].ToString());
                         }


                         EmployeeName = GetEmployeeName(str);
                         EmployeeCode = GetEmployeeCode(str);

                      
                         netsalary = 0;
                         

                        
                         DataTable dtEmpInfo = new DataTable();
                         DataTable dtDepartment = new DataTable();
                         DataTable dtDesignation = new DataTable();
                         
                         DataTable dtPayEmpMonth = new DataTable();
                         DataTable dtEmpAttendance = new DataTable();
                        

                         dtEmpInfo = objEmp.GetEmployeeMasterByEmpCode(Session["CompId"].ToString(), EmployeeCode.ToString());
                         dtDepartment = objDep.GetDepartmentMasterById(Session["CompId"].ToString(), dtEmpInfo.Rows[0]["Department_Id"].ToString());
                         dtDesignation = objDesg.GetDesignationMasterById(Session["CompId"].ToString(), dtEmpInfo.Rows[0]["Designation_Id"].ToString());
                        
                         dtPayEmpMonth = objPayEmpMonth.Getallrecords(str);
                         dtPayEmpMonth = new DataView(dtPayEmpMonth, "Emp_Id=" + str + " and Month=" + ddlMonth.SelectedValue.ToString() + " and Year='" + TxtYear.Text + "'", "", DataViewRowState.CurrentRows).ToTable();


                         
                         dtEmpAttendance = objEmpAttendance.GetRecord_Emp_Attendance(str, ddlMonth.SelectedValue.ToString(), TxtYear.Text);



                         for (int i = 0; i < dtEmpInfo.Rows.Count; i++)
                         {
                             if (dtEmpAttendance.Rows.Count > 0)
                             {

                                 for (int a = 0; a < dtEmpAttendance.Rows.Count; a++)
                                 {
                                     DataRow dr = dtEmpAttendSalary.NewRow();
                                    
                                     dr["DaysPresent"] = dtEmpAttendance.Rows[0]["Worked_Days"].ToString();
                                     dr["WeekOff"] = dtEmpAttendance.Rows[0]["Week_Off_Days"].ToString();
                                     dr["Holiday"] = dtEmpAttendance.Rows[0]["Holiday_Days"].ToString();
                                     dr["Leaves"] = dtEmpAttendance.Rows[0]["Leave_Days"].ToString();

                                     dr["NormalOT"] = dtEmpAttendance.Rows[0]["Normal_OT_Min"].ToString();
                                     dr["WeekOT"] = dtEmpAttendance.Rows[0]["Week_Off_OT_Min"].ToString();
                                     dr["HolidayOT"] = dtEmpAttendance.Rows[0]["Holiday_OT_Min"].ToString();

                                     dr["LatePenalty"] = dtEmpAttendance.Rows[0]["Late_Penalty_Min"].ToString();
                                     dr["EarlyPenalty"] = dtEmpAttendance.Rows[0]["Early_Penalty_Min"].ToString();
                                     dr["PartialPenalty"] = dtEmpAttendance.Rows[0]["Partial_Penalty_Min"].ToString();
                                     dr["AbsentPenalty"] = dtEmpAttendance.Rows[0]["Absent_Penalty"].ToString();


                                     dr["WorkedSal"] = dtEmpAttendance.Rows[0]["Basic_Work_Salary"].ToString();
                                     dr["LeavedaysSal"] = dtEmpAttendance.Rows[0]["Leave_Days_Salary"].ToString();
                                     dr["HolidaysSal"] = dtEmpAttendance.Rows[0]["Holiday_Days_Salary"].ToString();
                                     dr["WeekOffSal"] = dtEmpAttendance.Rows[0]["Week_Off_Days_Salary"].ToString();

                                     double worksal = Convert.ToDouble(dtEmpAttendance.Rows[0]["Basic_Work_Salary"].ToString());
                                      double leavesal = Convert.ToDouble(dtEmpAttendance.Rows[0]["Leave_Days_Salary"].ToString());
                                      double holidayssal = Convert.ToDouble(dtEmpAttendance.Rows[0]["Holiday_Days_Salary"].ToString());
                                      double weekofsal = Convert.ToDouble(dtEmpAttendance.Rows[0]["Week_Off_Days_Salary"].ToString());

                                      totalattndsal = worksal + leavesal + holidayssal + weekofsal;
                                      dr["totalAttendSal"] = totalattndsal.ToString();

                                      dr["NormalOTSal"] = dtEmpAttendance.Rows[0]["Normal_OT_Work_Salary"].ToString();
                                      dr["WeekOffOTSal"] = dtEmpAttendance.Rows[0]["WeekOff_OT_Work_Salary"].ToString();
                                      dr["HolidaysOTSal"] = dtEmpAttendance.Rows[0]["Holiday_OT_Work_Salary"].ToString();

                                      double noramlotsal = Convert.ToDouble(dtEmpAttendance.Rows[0]["Normal_OT_Work_Salary"].ToString());
                                      double weekotsal = Convert.ToDouble(dtEmpAttendance.Rows[0]["WeekOff_OT_Work_Salary"].ToString());
                                      double holidaotsal = Convert.ToDouble(dtEmpAttendance.Rows[0]["Holiday_OT_Work_Salary"].ToString());
                                     totalOTsal = noramlotsal + weekotsal + holidaotsal;
                                     dr["TotalOTSal"] = totalOTsal.ToString();

                                     
                                     dr["LatePenaSal"] = dtEmpAttendance.Rows[0]["Late_Min_Penalty"].ToString();
                                     dr["EarlyPenaSal"] = dtEmpAttendance.Rows[0]["Early_Min_Penalty"].ToString();
                                     dr["PartialPenaSal"] = dtEmpAttendance.Rows[0]["Parital_Violation_Penalty"].ToString();
                                     dr["AbsentPenaSal"] = dtEmpAttendance.Rows[0]["Absent_Day_Penalty"].ToString();

                                     double latesal = Convert.ToDouble(dtEmpAttendance.Rows[0]["Late_Min_Penalty"].ToString());
                                     double earlysal = Convert.ToDouble(dtEmpAttendance.Rows[0]["Early_Min_Penalty"].ToString());
                                     double partialsal = Convert.ToDouble(dtEmpAttendance.Rows[0]["Parital_Violation_Penalty"].ToString());
                                     double absentsal = Convert.ToDouble(dtEmpAttendance.Rows[0]["Absent_Day_Penalty"].ToString());

                                     totalpenasal = latesal + earlysal + partialsal + absentsal;
                                     dr["TotalPenaltySal"] = totalpenasal.ToString();

                                     dr["EmpCode"] = EmployeeCode;
                                     dr["EmpName"] = EmployeeName;
                                     DateTime dt = Convert.ToDateTime(dtEmpInfo.Rows[0]["DOJ"].ToString());
                                     dr["DOJ"] = dt.ToString("dd-MMM-yyyy");


                                     if (dtDesignation.Rows.Count > 0)
                                     {
                                         dr["Designation"] = dtDesignation.Rows[0]["Designation"].ToString();
                                     }
                                     if (dtDepartment.Rows.Count > 0)
                                     {
                                         dr["Department"] = dtDepartment.Rows[0]["Dep_Name"].ToString();
                                     }
                                     dr["Month"] = month[Convert.ToInt32(ddlMonth.SelectedIndex.ToString())].ToString();
                                     dr["Year"] = TxtYear.Text.ToString();
                                     dr["BankAccountNo"] = dtEmpInfo.Rows[0]["Account_No"].ToString();
                                     netsalary = (totalattndsal + totalOTsal) - totalpenasal;
                                     dr["NetSalary"] = netsalary.ToString();

                                     dtEmpAttendSalary.Rows.Add(dr);


                                 }

                             }



                            

                             }


                            

                            
                                    
                                 }

                             }

                 
             }

             if (dtEmpAttendSalary.Rows.Count > 0 || dtEmpAttendSalary.Rows.Count!=0)
             {

                 Session["dtEmpAttendRecords"] = dtEmpAttendSalary;
                 Response.Redirect("../HR_Report/Employee_Attendence_Salary_Report.aspx");
             }
             else
             {
                 DisplayMessage("Record Not found");
             }
           

         
         }




       protected void btntotalsalreport_Click(object sender, EventArgs e)
         {

             double netsalary = 0;

             double basicsal = 0;
             double totalattndsal = 0;
             double totalOTsal = 0;
             double totalpenasal = 0;
           double totalallowattendclaim=0;
           double totaldeducloanpenalty=0;

             string doj = "";
             string EmployeeName = "";
             string EmployeeCode = "";
             string[] month = new string[13];
             month[0] = "--Select--";
             month[1] = "JAN'";
             month[2] = "fEB'";
             month[3] = "MAR'";
             month[4] = "APR'";
             month[5] = "MAY'";
             month[6] = "JUN'";
             month[7] = "JUL'";
             month[8] = "AUG'";
             month[9] = "SEP'";
             month[10] = "OCT'";
             month[11] = "NOV'";
             month[12] = "DEC'";

             DataTable dtEmptotalsal = new DataTable();
             DataTable dtEmpRecords = new DataTable();

             dtEmptotalsal.Columns.Add("EmpCode");
             dtEmptotalsal.Columns.Add("EmpName");

             dtEmptotalsal.Columns.Add("Designation");
             dtEmptotalsal.Columns.Add("Department");
             dtEmptotalsal.Columns.Add("DOJ");
             dtEmptotalsal.Columns.Add("Month");
             dtEmptotalsal.Columns.Add("Year");
             dtEmptotalsal.Columns.Add("BankAccountNo");

             
           
             //Attendance Salary
             dtEmptotalsal.Columns.Add("WorkedSal");
             dtEmptotalsal.Columns.Add("WeekOffSal");
             dtEmptotalsal.Columns.Add("HolidaysSal");
             dtEmptotalsal.Columns.Add("LeavedaysSal");
             
             //Overtime Salary
             dtEmptotalsal.Columns.Add("NormalOTSal");
             dtEmptotalsal.Columns.Add("WeekOffOTSal");
             dtEmptotalsal.Columns.Add("HolidaysOTSal");
            
             //Penalty Sal
             dtEmptotalsal.Columns.Add("LatePenaSal");
             dtEmptotalsal.Columns.Add("EarlyPenaSal");
             dtEmptotalsal.Columns.Add("PartialPenaSal");
             dtEmptotalsal.Columns.Add("AbsentPenaSal");
             // all Total Amount 

             dtEmptotalsal.Columns.Add("AllowanceActAmt");
             dtEmptotalsal.Columns.Add("DeductionActAmt");
             dtEmptotalsal.Columns.Add("TotalEmpPenalty");
             dtEmptotalsal.Columns.Add("TotalEmpClaim");
             dtEmptotalsal.Columns.Add("TotalEmpLoan");

             dtEmptotalsal.Columns.Add("NetSalary");
             
                dtEmptotalsal.Columns.Add("totalAttendSal");
             dtEmptotalsal.Columns.Add("TotalAdjustmentAmt");
             dtEmptotalsal.Columns.Add("TotalDeducloanpenaty");





             //dtEmptotalsal.Columns.Add("DeductionName");
            // dtEmptotalsal.Columns.Add("DeductionActAmt");



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

                     foreach (string str in EmpIds.Split(','))
                     {
                         

                         if ((str != ""))
                         {
                             EmployeeName = GetEmployeeName(str);
                             EmployeeCode = GetEmployeeCode(str);

                             totalattndsal = 0;
                             totalOTsal = 0;
                             totalpenasal = 0;
                             totalallowattendclaim = 0;
                             totaldeducloanpenalty = 0;


                             DataTable dtemppara = new DataTable();
                             dtemppara = objempparam.GetEmployeeParameterByEmpId(str, Session["CompId"].ToString());
                             if (dtemppara.Rows.Count > 0)
                             {

                                 basicsal = Convert.ToDouble(dtemppara.Rows[0]["Basic_Salary"].ToString());
                             }


                             EmployeeName = GetEmployeeName(str);
                             EmployeeCode = GetEmployeeCode(str);


                             netsalary = 0;



                             DataTable dtEmpInfo = new DataTable();
                             DataTable dtDepartment = new DataTable();
                             DataTable dtDesignation = new DataTable();

                             DataTable dtPayEmpMonth = new DataTable();
                             DataTable dtEmpAttendance = new DataTable();


                             dtEmpInfo = objEmp.GetEmployeeMasterByEmpCode(Session["CompId"].ToString(), EmployeeCode.ToString());
                             dtDepartment = objDep.GetDepartmentMasterById(Session["CompId"].ToString(), dtEmpInfo.Rows[0]["Department_Id"].ToString());
                             dtDesignation = objDesg.GetDesignationMasterById(Session["CompId"].ToString(), dtEmpInfo.Rows[0]["Designation_Id"].ToString());

                             dtPayEmpMonth = objPayEmpMonth.GetAllRecordPostedEmpMonth(str, ddlMonth.SelectedValue.ToString(), TxtYear.Text);
                             dtPayEmpMonth = new DataView(dtPayEmpMonth, "Emp_Id=" + str + " and Month=" + ddlMonth.SelectedValue.ToString() + " and Year='" + TxtYear.Text + "'", "", DataViewRowState.CurrentRows).ToTable();



                             //  dtEmpAttendance = objEmpAttendance.GetRecord_Emp_Attendance(str, ddlMonth.SelectedValue.ToString(), TxtYear.Text);



                             for (int i = 0; i < dtEmpInfo.Rows.Count; i++)
                             {
                                 if (dtPayEmpMonth.Rows.Count > 0)
                                 {

                                     for (int a = 0; a < dtPayEmpMonth.Rows.Count; a++)
                                     {
                                         DataRow dr = dtEmptotalsal.NewRow();



                                         //dr["WorkedSal"] = dtPayEmpMonth.Rows[0]["Worked_Min_Salary"].ToString();
                                         //dr["LeavedaysSal"] = dtPayEmpMonth.Rows[0]["Leave_Days_Salary"].ToString();
                                         //dr["HolidaysSal"] = dtPayEmpMonth.Rows[0]["Holidays_Salary"].ToString();
                                         //dr["WeekOffSal"] = dtPayEmpMonth.Rows[0]["Week_Off_Salary"].ToString();

                                         double worksal = Convert.ToDouble(dtPayEmpMonth.Rows[0]["Worked_Min_Salary"].ToString());
                                         double leavesal = Convert.ToDouble(dtPayEmpMonth.Rows[0]["Leave_Days_Salary"].ToString());
                                         double holidayssal = Convert.ToDouble(dtPayEmpMonth.Rows[0]["Holidays_Salary"].ToString());
                                         double weekofsal = Convert.ToDouble(dtPayEmpMonth.Rows[0]["Week_Off_Salary"].ToString());

                                         totalattndsal = worksal + leavesal + holidayssal + weekofsal;
                                         // dr["totalAttendSal"] = totalattndsal.ToString();

                                         //dr["NormalOTSal"] = dtPayEmpMonth.Rows[0]["Normal_OT_Min_Salary"].ToString();
                                         //dr["WeekOffOTSal"] = dtPayEmpMonth.Rows[0]["Week_Off_OT_Min_Salary"].ToString();
                                         //dr["HolidaysOTSal"] = dtPayEmpMonth.Rows[0]["Holiday_OT_Min_Salary"].ToString();

                                         double noramlotsal = Convert.ToDouble(dtPayEmpMonth.Rows[0]["Normal_OT_Min_Salary"].ToString());
                                         double weekotsal = Convert.ToDouble(dtPayEmpMonth.Rows[0]["Week_Off_OT_Min_Salary"].ToString());
                                         double holidaotsal = Convert.ToDouble(dtPayEmpMonth.Rows[0]["Holiday_OT_Min_Salary"].ToString());
                                         totalOTsal = noramlotsal + weekotsal + holidaotsal;
                                         // dr["TotalOTSal"] = totalOTsal.ToString();


                                         //dr["LatePenaSal"] = dtPayEmpMonth.Rows[0]["Late_Min_Penalty"].ToString();
                                         //dr["EarlyPenaSal"] = dtPayEmpMonth.Rows[0]["Early_Min_Penalty"].ToString();
                                         //dr["PartialPenaSal"] = dtPayEmpMonth.Rows[0]["Parital_Violation_Penalty"].ToString();
                                         //dr["AbsentPenaSal"] = dtPayEmpMonth.Rows[0]["Absent_Day_Penalty"].ToString();

                                         double latesal = Convert.ToDouble(dtPayEmpMonth.Rows[0]["Late_Min_Penalty"].ToString());
                                         double earlysal = Convert.ToDouble(dtPayEmpMonth.Rows[0]["Early_Min_Penalty"].ToString());
                                         double partialsal = Convert.ToDouble(dtPayEmpMonth.Rows[0]["Patial_Violation_Penalty"].ToString());
                                         double absentsal = Convert.ToDouble(dtPayEmpMonth.Rows[0]["Absent_Salary"].ToString());

                                         totalpenasal = latesal + earlysal + partialsal + absentsal;
                                         // dr["TotalPenaltySal"] = totalpenasal.ToString();
                                         netsalary = (totalattndsal + totalOTsal) - totalpenasal;
                                         dr["totalAttendSal"] = netsalary.ToString();





                                         dr["AllowanceActAmt"] = dtPayEmpMonth.Rows[0]["Total_Allowance"].ToString();
                                         double totallow = Convert.ToDouble(dtPayEmpMonth.Rows[0]["Total_Allowance"].ToString());


                                         dr["TotalEmpClaim"] = dtPayEmpMonth.Rows[0]["Employee_Claim"].ToString();
                                         double totclaim = Convert.ToDouble(dtPayEmpMonth.Rows[0]["Employee_Claim"].ToString());
                                         totalallowattendclaim = netsalary + totallow + totclaim;

                                         dr["NetSalary"] = totalallowattendclaim.ToString();


                                         dr["DeductionActAmt"] = dtPayEmpMonth.Rows[0]["Total_Deduction"].ToString();
                                         double totdeduc = Convert.ToDouble(dtPayEmpMonth.Rows[0]["Total_Deduction"].ToString());
                                         dr["TotalEmpLoan"] = dtPayEmpMonth.Rows[0]["Emlployee_Loan"].ToString();
                                         double totloan = Convert.ToDouble(dtPayEmpMonth.Rows[0]["Emlployee_Loan"].ToString());
                                         dr["TotalEmpPenalty"] = dtPayEmpMonth.Rows[0]["Employee_Penalty"].ToString();
                                         double totpenalty = Convert.ToDouble(dtPayEmpMonth.Rows[0]["Employee_Penalty"].ToString());
                                         totaldeducloanpenalty = totdeduc + totloan + totpenalty;

                                         dr["TotalDeducloanpenaty"] = totaldeducloanpenalty.ToString();


                                         dr["TotalAdjustmentAmt"] = dtPayEmpMonth.Rows[0]["Previous_Month_Balance"].ToString();





                                         dr["EmpCode"] = EmployeeCode;
                                         dr["EmpName"] = EmployeeName;
                                         DateTime dt = Convert.ToDateTime(dtEmpInfo.Rows[0]["DOJ"].ToString());
                                         dr["DOJ"] = dt.ToString("dd-MMM-yyyy");


                                         if (dtDesignation.Rows.Count > 0)
                                         {
                                             dr["Designation"] = dtDesignation.Rows[0]["Designation"].ToString();
                                         }
                                         if (dtDepartment.Rows.Count > 0)
                                         {
                                             dr["Department"] = dtDepartment.Rows[0]["Dep_Name"].ToString();
                                         }
                                         dr["Month"] = month[Convert.ToInt32(ddlMonth.SelectedIndex.ToString())].ToString();
                                         dr["Year"] = TxtYear.Text.ToString();
                                         dr["BankAccountNo"] = dtEmpInfo.Rows[0]["Account_No"].ToString();

                                         dtEmptotalsal.Rows.Add(dr);


                                     }

                                 }
                             }




                         }


             

            

                     }


                 }


                 else
                 {
                     DisplayMessage("Select Group First");
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
                          totalattndsal = 0;
                          totalOTsal = 0;
                          totalpenasal = 0;
                          totalallowattendclaim = 0;
                          totaldeducloanpenalty = 0;

            
                         DataTable dtemppara = new DataTable();
                         dtemppara = objempparam.GetEmployeeParameterByEmpId(str, Session["CompId"].ToString());
                         if (dtemppara.Rows.Count > 0)
                         {

                             basicsal = Convert.ToDouble(dtemppara.Rows[0]["Basic_Salary"].ToString());
                         }


                         EmployeeName = GetEmployeeName(str);
                         EmployeeCode = GetEmployeeCode(str);


                         netsalary = 0;



                         DataTable dtEmpInfo = new DataTable();
                         DataTable dtDepartment = new DataTable();
                         DataTable dtDesignation = new DataTable();

                         DataTable dtPayEmpMonth = new DataTable();
                         DataTable dtEmpAttendance = new DataTable();


                         dtEmpInfo = objEmp.GetEmployeeMasterByEmpCode(Session["CompId"].ToString(), EmployeeCode.ToString());
                         dtDepartment = objDep.GetDepartmentMasterById(Session["CompId"].ToString(), dtEmpInfo.Rows[0]["Department_Id"].ToString());
                         dtDesignation = objDesg.GetDesignationMasterById(Session["CompId"].ToString(), dtEmpInfo.Rows[0]["Designation_Id"].ToString());

                         dtPayEmpMonth = objPayEmpMonth.GetAllRecordPostedEmpMonth(str, ddlMonth.SelectedValue.ToString(), TxtYear.Text);
                         dtPayEmpMonth = new DataView(dtPayEmpMonth, "Emp_Id=" + str + " and Month=" + ddlMonth.SelectedValue.ToString() + " and Year='" + TxtYear.Text + "'", "", DataViewRowState.CurrentRows).ToTable();



                       //  dtEmpAttendance = objEmpAttendance.GetRecord_Emp_Attendance(str, ddlMonth.SelectedValue.ToString(), TxtYear.Text);



                         for (int i = 0; i < dtEmpInfo.Rows.Count; i++)
                         {
                             if (dtPayEmpMonth.Rows.Count > 0)
                             {

                                 for (int a = 0; a < dtPayEmpMonth.Rows.Count; a++)
                                 {
                                     DataRow dr = dtEmptotalsal.NewRow();



                                     //dr["WorkedSal"] = dtPayEmpMonth.Rows[0]["Worked_Min_Salary"].ToString();
                                     //dr["LeavedaysSal"] = dtPayEmpMonth.Rows[0]["Leave_Days_Salary"].ToString();
                                     //dr["HolidaysSal"] = dtPayEmpMonth.Rows[0]["Holidays_Salary"].ToString();
                                     //dr["WeekOffSal"] = dtPayEmpMonth.Rows[0]["Week_Off_Salary"].ToString();

                                     double worksal = Convert.ToDouble(dtPayEmpMonth.Rows[0]["Worked_Min_Salary"].ToString());
                                     double leavesal = Convert.ToDouble(dtPayEmpMonth.Rows[0]["Leave_Days_Salary"].ToString());
                                     double holidayssal = Convert.ToDouble(dtPayEmpMonth.Rows[0]["Holidays_Salary"].ToString());
                                     double weekofsal = Convert.ToDouble(dtPayEmpMonth.Rows[0]["Week_Off_Salary"].ToString());

                                     totalattndsal = worksal + leavesal + holidayssal + weekofsal;
                                    // dr["totalAttendSal"] = totalattndsal.ToString();

                                     //dr["NormalOTSal"] = dtPayEmpMonth.Rows[0]["Normal_OT_Min_Salary"].ToString();
                                     //dr["WeekOffOTSal"] = dtPayEmpMonth.Rows[0]["Week_Off_OT_Min_Salary"].ToString();
                                     //dr["HolidaysOTSal"] = dtPayEmpMonth.Rows[0]["Holiday_OT_Min_Salary"].ToString();

                                     double noramlotsal = Convert.ToDouble(dtPayEmpMonth.Rows[0]["Normal_OT_Min_Salary"].ToString());
                                     double weekotsal = Convert.ToDouble(dtPayEmpMonth.Rows[0]["Week_Off_OT_Min_Salary"].ToString());
                                     double holidaotsal = Convert.ToDouble(dtPayEmpMonth.Rows[0]["Holiday_OT_Min_Salary"].ToString());
                                     totalOTsal = noramlotsal + weekotsal + holidaotsal;
                                    // dr["TotalOTSal"] = totalOTsal.ToString();


                                     //dr["LatePenaSal"] = dtPayEmpMonth.Rows[0]["Late_Min_Penalty"].ToString();
                                     //dr["EarlyPenaSal"] = dtPayEmpMonth.Rows[0]["Early_Min_Penalty"].ToString();
                                     //dr["PartialPenaSal"] = dtPayEmpMonth.Rows[0]["Parital_Violation_Penalty"].ToString();
                                     //dr["AbsentPenaSal"] = dtPayEmpMonth.Rows[0]["Absent_Day_Penalty"].ToString();

                                     double latesal = Convert.ToDouble(dtPayEmpMonth.Rows[0]["Late_Min_Penalty"].ToString());
                                     double earlysal = Convert.ToDouble(dtPayEmpMonth.Rows[0]["Early_Min_Penalty"].ToString());
                                     double partialsal = Convert.ToDouble(dtPayEmpMonth.Rows[0]["Patial_Violation_Penalty"].ToString());
                                     double absentsal = Convert.ToDouble(dtPayEmpMonth.Rows[0]["Absent_Salary"].ToString());

                                     totalpenasal = latesal + earlysal + partialsal + absentsal;
                                    // dr["TotalPenaltySal"] = totalpenasal.ToString();
                                     netsalary = (totalattndsal + totalOTsal) - totalpenasal;
                                     dr["totalAttendSal"] = netsalary.ToString();

                                     

                                    
                                    
                                     dr["AllowanceActAmt"] = dtPayEmpMonth.Rows[0]["Total_Allowance"].ToString();
                                     double totallow = Convert.ToDouble(dtPayEmpMonth.Rows[0]["Total_Allowance"].ToString());
                                     

                                     dr["TotalEmpClaim"] = dtPayEmpMonth.Rows[0]["Employee_Claim"].ToString();
                                     double totclaim = Convert.ToDouble(dtPayEmpMonth.Rows[0]["Employee_Claim"].ToString());
                                     totalallowattendclaim=netsalary+totallow+totclaim;

                                     dr["NetSalary"] = totalallowattendclaim.ToString();


                                     dr["DeductionActAmt"] = dtPayEmpMonth.Rows[0]["Total_Deduction"].ToString();
                                     double totdeduc = Convert.ToDouble(dtPayEmpMonth.Rows[0]["Total_Deduction"].ToString());
                                     dr["TotalEmpLoan"] = dtPayEmpMonth.Rows[0]["Emlployee_Loan"].ToString();
                                     double totloan = Convert.ToDouble(dtPayEmpMonth.Rows[0]["Emlployee_Loan"].ToString());
                                     dr["TotalEmpPenalty"] = dtPayEmpMonth.Rows[0]["Employee_Penalty"].ToString();
                                     double totpenalty = Convert.ToDouble(dtPayEmpMonth.Rows[0]["Employee_Penalty"].ToString());
                                     totaldeducloanpenalty = totdeduc + totloan + totpenalty;

                                     dr["TotalDeducloanpenaty"] = totaldeducloanpenalty.ToString();


                                     dr["TotalAdjustmentAmt"] = dtPayEmpMonth.Rows[0]["Previous_Month_Balance"].ToString();

                                     
                                    

                                     
                                     dr["EmpCode"] = EmployeeCode;
                                     dr["EmpName"] = EmployeeName;
                                     DateTime dt = Convert.ToDateTime(dtEmpInfo.Rows[0]["DOJ"].ToString());
                                     dr["DOJ"] = dt.ToString("dd-MMM-yyyy");


                                     if (dtDesignation.Rows.Count > 0)
                                     {
                                         dr["Designation"] = dtDesignation.Rows[0]["Designation"].ToString();
                                     }
                                     if (dtDepartment.Rows.Count > 0)
                                     {
                                         dr["Department"] = dtDepartment.Rows[0]["Dep_Name"].ToString();
                                     }
                                     dr["Month"] = month[Convert.ToInt32(ddlMonth.SelectedIndex.ToString())].ToString();
                                     dr["Year"] = TxtYear.Text.ToString();
                                     dr["BankAccountNo"] = dtEmpInfo.Rows[0]["Account_No"].ToString();
                                    
                                     dtEmptotalsal.Rows.Add(dr);


                                 }

                             }
                        }

                     }

                 }


             }  

             if (dtEmptotalsal.Rows.Count > 0)
             {

                 Session["dtEmptotalSalRecord"] = dtEmptotalsal;
                 Response.Redirect("../HR_Report/Employee_TotalSalary_Report.aspx");
             }
             else
             {
                 DisplayMessage("Record Not found");
             }
            
         
         
         }

}
