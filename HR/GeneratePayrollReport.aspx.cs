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

public partial class HR_GeneratePayrollReport : System.Web.UI.Page
{
    #region defind Class Object


    Common ObjComman = new Common();
    Pay_Employee_claim ObjClaim = new Pay_Employee_claim();

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

    Set_ApplicationParameter objAppParam = new Set_ApplicationParameter();
    CurrencyMaster objCurrency = new CurrencyMaster();
    CompanyMaster objComp = new CompanyMaster();
    EmployeeParameter objEmpParam = new EmployeeParameter();
    Pay_Employee_Loan ObjLoan = new Pay_Employee_Loan();
    Pay_Employee_Penalty ObjPenalty = new Pay_Employee_Penalty();
    Arc_Directory_Master objDir = new Arc_Directory_Master();
    Arc_FileTransaction ObjFile = new Arc_FileTransaction();
    
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
            TxtYear.Text = DateTime.Now.Year.ToString();
            int CurrentMonth = Convert.ToInt32(DateTime.Now.Month.ToString());
            ddlMonth.SelectedValue = (CurrentMonth).ToString();


            Session["empimgpath"] = null;

            Page.Form.Attributes.Add("enctype", "multipart/form-data");


            bool IsCompOT = false;
            bool IsPartialComp = false;

            IsCompOT = Convert.ToBoolean(objAppParam.GetApplicationParameterValueByParamName("IsOverTime", Session["CompId"].ToString()));
            IsPartialComp = Convert.ToBoolean(objAppParam.GetApplicationParameterValueByParamName("Partial_Leave_Enable", Session["CompId"].ToString()));


            btnLeave_Click(null, null);




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
    protected void btnLeave_Click(object sender, EventArgs e)
    {

        pnlLeave.BackColor = System.Drawing.ColorTranslator.FromHtml("#ccddee");

        PanelList.BackColor = System.Drawing.ColorTranslator.FromHtml("#90bde9");

        PnlEmployeeLeave.Visible = true;

        PanelSearchList.Visible = false;







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
    protected void btnLeast_Click(object sender, EventArgs e)
    {
        PanelList.BackColor = System.Drawing.ColorTranslator.FromHtml("#ccddee");
        pnlLeave.BackColor = System.Drawing.ColorTranslator.FromHtml("#90bde9");
        PnlEmployeeLeave.Visible = false;

        PanelSearchList.Visible = true;
    }
    protected void btnSave_Click(object sender, EventArgs e)
    {
        Session["Querystring"] = null;
        Session["ClaimRecord"] = null;
        Session["Document"] = null;
        Session["ClaimType"] = null;
        Session["Month"] = null;
        Session["Year"] = null;
        int b = 0;
        DataTable DtClaimRecord = new DataTable();
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
        if (Rbtndocument.Checked == true)
        {
            DtClaimRecord = ObjFile.Get_FileTransaction(Session["CompId"].ToString(), "0");

        }
        if (RbtnDirectory.Checked == true)
        {
            DtClaimRecord = ObjFile.Get_FileTransaction(Session["CompId"].ToString(), "0");

        }

        if (rbtnPenalty.Checked == true)
        {
            DtClaimRecord =  ObjPenalty.GetRecord_From_PayEmployeePenalty(Session["CompId"].ToString(), "0", "0", ddlMonth.SelectedValue, TxtYear.Text, "", "");
       
        }

        if (rbtnLoan.Checked == true)
        {
            DtClaimRecord = ObjLoan.GetRecord_From_PayEmployeeLoan(Session["CompId"].ToString(), "0", "Running");
            string startadate = ddlMonth.SelectedValue + "/" + "1" + "/"+TxtYear.Text;
                int DaysInmonth=DateTime.DaysInMonth(Convert.ToInt32(TxtYear.Text),Convert.ToInt32(ddlMonth.SelectedValue));
                string enddate = ddlMonth.SelectedValue + "/" + DaysInmonth + "/" + TxtYear.Text;


                DtClaimRecord = new DataView(DtClaimRecord, "Loan_Approval_Date>='" + startadate + "' and Loan_Approval_Date <='" + enddate + "'", "", DataViewRowState.CurrentRows).ToTable();
            Session["Month"] = ddlMonth.SelectedItem.Text;
            Session["Year"] = TxtYear.Text;
        
        }
       if(rbtnClaimType.Checked==true)
        {

            if (ddlClaimType.SelectedIndex == 0)
            {
                DisplayMessage("Select Claim Type");
                ddlClaimType.Focus();
                return;
            }
            int ClaimType = Convert.ToInt32(ddlClaimType.SelectedIndex);
            Session["ClaimType"] = ClaimType;

            string TransNo = string.Empty;


            if (ddlClaimType.SelectedIndex == 1)
            {
                DtClaimRecord = ObjClaim.GetRecord_From_PayEmployeeClaim(Session["CompId"].ToString(), "0", "0", ddlMonth.SelectedValue, TxtYear.Text, "Approved", "", "");
            }
            if (ddlClaimType.SelectedIndex == 2)
            {
                DtClaimRecord = ObjClaim.GetRecord_From_PayEmployeeClaim(Session["CompId"].ToString(), "0", "0", ddlMonth.SelectedValue, TxtYear.Text, "Cancelled", "", "");
            }
            if (ddlClaimType.SelectedIndex == 3)
            {
                DtClaimRecord = ObjClaim.GetRecord_From_PayEmployeeClaim(Session["CompId"].ToString(), "0", "0", ddlMonth.SelectedValue, TxtYear.Text, "Pending", "", "");
            }
        }
       
       
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
                //DisplayMessage("Select Group First");
                if (Rbtndocument.Checked == true)
                {
                    Session["Querystring"] = EmpIds;
                    Session["ClaimRecord"] = DtClaimRecord;
                    Session["Document"] = "Document";
                    
                    Response.Redirect("../HR_Report/EmpDirectoryReport.aspx");
               
                }
                if (RbtnDirectory.Checked == true)
                {
                    Session["Document"] = "Directory";
                    Session["Querystring"] = EmpIds;
                    Session["ClaimRecord"] = DtClaimRecord;
                    Response.Redirect("../HR_Report/EmpDirectoryReport.aspx");
               

                }
                if (rbtnPenalty.Checked == true)
                {
                    Session["Querystring"] = EmpIds;
                    Session["ClaimRecord"] = DtClaimRecord;
                  Response.Redirect("../HR_Report/PenaltyReport.aspx");
               

                }
               

                if (rbtnLoan.Checked == true)
                {
                    Session["Querystring"] = EmpIds;
                    Session["ClaimRecord"] = DtClaimRecord;
                    //Response.Redirect("~/Reports/ClaimReport.aspx");
                   Response.Redirect("../HR_Report/LoanReport.aspx");
                }
                if (RbtnloanDetail.Checked == true)
                {
                    if (Ddlloantype.SelectedIndex == 0)
                    {
                        Ddlloantype.Focus();
                        DisplayMessage("Select Loan Type");
                        return;
                    }
                    Session["Querystring"] = Ddlloantype.SelectedValue;

                    string[] Empid = EmpIds.Split(',');
                 
        


                    DtClaimRecord.Columns.Add("Empid");
                    DtClaimRecord.Columns.Add("EmpName");
                    DtClaimRecord.Columns.Add("Loan_Name");
                    DtClaimRecord.Columns.Add("Installment");
                    DtClaimRecord.Columns.Add("PaidAmount");
                    DtClaimRecord.Columns.Add("Status");
                    DtClaimRecord.Columns.Add("Month");
                    DtClaimRecord.Columns.Add("Year");
                    DtClaimRecord.Columns.Add("Loan_Id");
                    DtClaimRecord.Columns.Add("Loan_Amount");
                    DtClaimRecord.Columns.Add("Loan_Duration");
                    DtClaimRecord.Columns.Add("Loan_Interest");
                    DtClaimRecord.Columns.Add("Gross_Amount");
                    DtClaimRecord.Columns.Add("Sum_PaidAmount");
              
                    for (int i = 0; i < Empid.Length-1; i++)
                    {
                        
                        DataTable dtloanmaster = new DataTable();
                        DataTable dtloandetail = new DataTable();
                        dtloanmaster = ObjLoan.GetRecord_From_PayEmployeeLoan(Session["CompId"].ToString(), "0", "Running");


                        dtloanmaster = new DataView(dtloanmaster, "Emp_id=" + Empid[i].ToString() + "", "", DataViewRowState.CurrentRows).ToTable();
                        if (dtloanmaster.Rows.Count > 0)
                        {
                            for (int k = 0; k < dtloanmaster.Rows.Count; k++)
                            {

                                string loanid = dtloanmaster.Rows[k]["Loan_id"].ToString();

                                dtloandetail = ObjLoan.GetRecord_From_PayEmployeeLoanDetail(loanid);
                                if (dtloandetail.Rows.Count > 0)
                                {
                                    if (Ddlloantype.SelectedValue=="2")
                                    {
                                        dtloandetail = new DataView(dtloandetail, "Month=" + ddlMonth.SelectedValue + " and Year=" + TxtYear.Text + "", "", DataViewRowState.CurrentRows).ToTable();
                                    }

                                    for (int j = 0; j < dtloandetail.Rows.Count; j++)
                                    {
                                        int month = Convert.ToInt32(dtloandetail.Rows[j]["Month"].ToString());
                                        DataRow Tablerow = DtClaimRecord.NewRow();
                                       
                                        Tablerow[0] = dtloanmaster.Rows[k]["Emp_Id"].ToString();
                                        Tablerow[1] = dtloanmaster.Rows[k]["Emp_Name"].ToString();
                                        Tablerow[2] = dtloanmaster.Rows[k]["Loan_Name"].ToString();
                                        Tablerow[3] = dtloandetail.Rows[j]["Montly_Installment"].ToString();
                                        Tablerow[4] = dtloandetail.Rows[j]["Employee_Paid"].ToString();
                                        Tablerow[5] = dtloandetail.Rows[j]["Is_Status"].ToString();
                                        Tablerow[6] = Montharr[month - 1].ToString();
                                        Tablerow[7] = dtloandetail.Rows[j]["Year"].ToString();
                                        Tablerow[8] = dtloanmaster.Rows[k]["Loan_Id"].ToString();
                                        Tablerow[9] = dtloanmaster.Rows[k]["Loan_Amount"].ToString();
                                        Tablerow[10] = dtloanmaster.Rows[k]["Loan_Duration"].ToString();
                                        Tablerow[11] = dtloanmaster.Rows[k]["Loan_Interest"].ToString();
                                        Tablerow[12] = dtloanmaster.Rows[k]["Gross_Amount"].ToString();

                                 
                                        DtClaimRecord.Rows.Add(Tablerow);
                                    }
                                }
                            }




                        }







                    }
                   
                    Session["ClaimRecord"] = DtClaimRecord;
                   Response.Redirect("../HR_Report/LoanDetailReport.aspx");
              
                    
                } //end scope of rbtnloanddetail
                
            
           
             if(rbtnClaimType.Checked==true)
                {
                    Session["Querystring"] = EmpIds;
                    Session["ClaimRecord"] = DtClaimRecord;
                    
                   Response.Redirect("../HR_Report/ClaimReport.aspx");
                }

               // code for loan detail report
               


            }
         
          



        }

        else
        {
            if (lblSelectRecd.Text == "")
            {

                DisplayMessage("Select Employee First");
                return;

            }



            //DisplayMessage("Select Group First");
            

            //Response.Redirect("~/Reports/ClaimReport.aspx");
            if (Rbtndocument.Checked == true)
            {
                Session["Querystring"] = lblSelectRecd.Text;
                Session["ClaimRecord"] = DtClaimRecord;
                Session["Document"] = "Document";
                Response.Redirect("../HR_Report/EmpDirectoryReport.aspx");

            }
            if (RbtnDirectory.Checked == true)
            {
                Session["Document"] = "Directory";
                Session["Querystring"] = lblSelectRecd.Text;
                Session["ClaimRecord"] = DtClaimRecord;
               Response.Redirect("../HR_Report/EmpDirectoryReport.aspx");

            }
            if (rbtnPenalty.Checked == true)
            {
                Session["Querystring"] = lblSelectRecd.Text;
                Session["ClaimRecord"] = DtClaimRecord;
                Response.Redirect("../HR_Report/PenaltyReport.aspx");

            }
            if (rbtnLoan.Checked == true)
            {
                Session["Querystring"] = lblSelectRecd.Text;
                Session["ClaimRecord"] = DtClaimRecord;


                //Response.Redirect("~/Reports/ClaimReport.aspx");
                Response.Redirect("../HR_Report/LoanReport.aspx");
            }
            if (RbtnloanDetail.Checked == true)
            {

                if (Ddlloantype.SelectedIndex == 0)
                {
                    Ddlloantype.Focus();
                    DisplayMessage("Select Loan Type");
                    return;
                }

                Session["Querystring"] = Ddlloantype.SelectedValue;

                string[] Empid = lblSelectRecd.Text.Split(',');



                DtClaimRecord.Columns.Add("Empid");
                DtClaimRecord.Columns.Add("EmpName");
                DtClaimRecord.Columns.Add("Loan_Name");
                DtClaimRecord.Columns.Add("Installment");
                DtClaimRecord.Columns.Add("PaidAmount");
                DtClaimRecord.Columns.Add("Status");
                DtClaimRecord.Columns.Add("Month");
                DtClaimRecord.Columns.Add("Year");
                DtClaimRecord.Columns.Add("Loan_Id");
                DtClaimRecord.Columns.Add("Loan_Amount");
                DtClaimRecord.Columns.Add("Loan_Duration");
                DtClaimRecord.Columns.Add("Loan_Interest");
                DtClaimRecord.Columns.Add("Gross_Amount");
                DtClaimRecord.Columns.Add("Sum_PaidAmount");

                for (int i = 0; i < Empid.Length - 1; i++)
                {
                    DataTable dtloanmaster = new DataTable();
                    DataTable dtloandetail = new DataTable();
                    dtloanmaster = ObjLoan.GetRecord_From_PayEmployeeLoan(Session["CompId"].ToString(), "0", "Running");


                    dtloanmaster = new DataView(dtloanmaster, "Emp_id=" + Empid[i].ToString() + "", "", DataViewRowState.CurrentRows).ToTable();
                    if (dtloanmaster.Rows.Count > 0)
                    {
                        for (int k = 0; k < dtloanmaster.Rows.Count; k++)
                        {

                            string loanid = dtloanmaster.Rows[k]["Loan_id"].ToString();

                            dtloandetail = ObjLoan.GetRecord_From_PayEmployeeLoanDetail(loanid);
                            if (dtloandetail.Rows.Count > 0)
                            {
                                if (Ddlloantype.SelectedValue == "2")
                                {
                                    dtloandetail = new DataView(dtloandetail, "Month=" + ddlMonth.SelectedValue + " and Year=" + TxtYear.Text + "", "", DataViewRowState.CurrentRows).ToTable();
                                }

                                //dtloandetail = new DataView(dtloandetail, "Month=" + ddlMonth.SelectedValue + " and Year=" + TxtYear.Text + "", "", DataViewRowState.CurrentRows).ToTable();

                                for (int j = 0; j < dtloandetail.Rows.Count; j++)
                                {
                                    int month = Convert.ToInt32(dtloandetail.Rows[j]["Month"].ToString());
                                    DataRow dr_Row = DtClaimRecord.NewRow();
                    
                                    dr_Row[0] = dtloanmaster.Rows[k]["Emp_Id"].ToString();
                                    dr_Row[1] = dtloanmaster.Rows[k]["Emp_Name"].ToString();
                                    dr_Row[2] = dtloanmaster.Rows[k]["Loan_Name"].ToString();
                                    dr_Row[3] = dtloandetail.Rows[j]["Montly_Installment"].ToString();
                                    dr_Row[4] = dtloandetail.Rows[j]["Employee_Paid"].ToString();
                                    dr_Row[5] = dtloandetail.Rows[j]["Is_Status"].ToString();
                                    dr_Row[6] = Montharr[month - 1].ToString();
                                    dr_Row[7] = TxtYear.Text;
                                    dr_Row[8] = dtloanmaster.Rows[k]["Loan_Id"].ToString();
                                    dr_Row[9] = dtloanmaster.Rows[k]["Loan_Amount"].ToString();
                                    dr_Row[10] = dtloanmaster.Rows[k]["Loan_Duration"].ToString();
                                    dr_Row[11] = dtloanmaster.Rows[k]["Loan_Interest"].ToString();
                                    dr_Row[12] = dtloanmaster.Rows[k]["Gross_Amount"].ToString();

                                    DtClaimRecord.Rows.Add(dr_Row);
                                }
                            }
                        }




                    }







                }
                Session["ClaimRecord"] = DtClaimRecord;
               Response.Redirect("../HR_Report/LoanDetailReport.aspx");
              
            } //end scope of rbtnloanddetail
                     
                    
            
            if(rbtnClaimType.Checked==true)
            {
                Session["Querystring"] = lblSelectRecd.Text;
                Session["ClaimRecord"] = DtClaimRecord;

                Response.Redirect("../HR_Report/ClaimReport.aspx");
            }
            
            

           

        }
        if (b != 0)
        {


           
            rbtnEmp.Checked = true;
            rbtnGroup.Checked = false;

            EmpGroup_CheckedChanged(null, null);
            





        }
        else
        {
            TxtYear.Text = DateTime.Now.Year.ToString();
            int CurrentMonth = Convert.ToInt32(DateTime.Now.Month.ToString());

            ddlMonth.SelectedValue = (CurrentMonth).ToString();
            foreach (GridViewRow Gvrow in gvEmpLeave.Rows)
            {
                CheckBox ChkHeader = (CheckBox)gvEmpLeave.HeaderRow.FindControl("chkgvSelectAll");
                CheckBox ChkItem = (CheckBox)Gvrow.FindControl("chkgvSelect");

                ChkHeader.Checked = false;
                ChkItem.Checked = false;


            }
            //DisplayMessage("Record Not Saved");
        }
        
      

    }
}
