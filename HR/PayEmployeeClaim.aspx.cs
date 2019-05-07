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

public partial class Arca_Wing_Pay_Employee_Claim : System.Web.UI.Page
{
    #region defind Class Object


    Common ObjComman = new Common();
   
    Pay_Employee_claim ObjClaim = new Pay_Employee_claim();
    Pay_Employee_Month objPayEmpMonth = new Pay_Employee_Month(); 
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

        TxtClaimName.Focus();



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
            dtEmp = new DataView(dtEmp, "Emp_Id in(" + EmpIds.Substring(0, EmpIds.Length - 1) + ")", "", DataViewRowState.CurrentRows).ToTable();

            if (dtEmp.Rows.Count > 0)
            {
                Session["dtEmp1"] = dtEmp;
                gvEmployee.DataSource = dtEmp;
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
        TxtClaimName.Focus();

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

        ddlMonth.SelectedValue = (CurrentMonth).ToString();
        foreach (GridViewRow Gvrow in gvEmpLeave.Rows)
        {
            CheckBox ChkHeader = (CheckBox)gvEmpLeave.HeaderRow.FindControl("chkgvSelectAll");
            CheckBox ChkItem = (CheckBox)Gvrow.FindControl("chkgvSelect");

            ChkHeader.Checked = false;
            ChkItem.Checked = false;


        }
    }
    protected void btnSaveLeave_Click(object sender, EventArgs e)
    {
        int b = 0;


        


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
                if (str != "")
                {
                    DataTable dtempmonth = new DataTable();
                    dtempmonth = objPayEmpMonth.GetAllRecordPostedEmpMonth(str, ddlMonth.SelectedIndex.ToString(), TxtYear.Text.ToString());
                    if (dtempmonth.Rows.Count > 0)
                    {
                        DisplayMessage("Payroll Already Generated");
                        return;

                    }
                    else
                    {


                        b = ObjClaim.Insert_In_Pay_Employee_Claim(Session["CompId"].ToString(), str, TxtClaimName.Text.Trim(), TxtClaimDiscription.Text, DdlValueType.SelectedValue, txtCalValue.Text, DateTime.Now.ToString(), "Approved", DateTime.Now.ToString(), ddlMonth.SelectedValue, TxtYear.Text, "", "", "", "", "", true.ToString(), DateTime.Now.ToString(), true.ToString(), Session["UserId"].ToString(), DateTime.Now.ToString(), Session["UserId"].ToString(), DateTime.Now.ToString());

                    }


                }
            }



        }

        else
        {
            if (lblSelectRecd.Text == "")
            {

                DisplayMessage("Select Employee First");
                return;

            }
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

            foreach (string str in lblSelectRecd.Text.Split(','))
            {
                if (str != "")
                {
                     DataTable dtempmonth = new DataTable();
                    dtempmonth = objPayEmpMonth.GetAllRecordPostedEmpMonth(str, ddlMonth.SelectedIndex.ToString(), TxtYear.Text.ToString());
                    if (dtempmonth.Rows.Count > 0)
                    {
                        DisplayMessage("Payroll Already Generated");
                        return;

                    }
                    else
                    {
                          b = ObjClaim.Insert_In_Pay_Employee_Claim(Session["CompId"].ToString(), str, TxtClaimName.Text.Trim(), TxtClaimDiscription.Text, DdlValueType.SelectedValue, txtCalValue.Text, DateTime.Now.ToString(), "Approved", DateTime.Now.ToString(), ddlMonth.SelectedValue, TxtYear.Text, "", "", "", "", "", true.ToString(), DateTime.Now.ToString(), true.ToString(), Session["UserId"].ToString(), DateTime.Now.ToString(), Session["UserId"].ToString(), DateTime.Now.ToString());

                    }

                }
            }


        }
        if (b != 0)
        {


            DisplayMessage("Record Saved");
            rbtnEmp.Checked = true;
            rbtnGroup.Checked = false;

            EmpGroup_CheckedChanged(null, null);
            Reset();

        }
        else
        {
            DisplayMessage("Record Not Saved");
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
    void GridBind_ClaimList(string Month, string Year)
    {
        DataTable Dt = new DataTable();
        Dt = ObjClaim.GetRecord_From_PayEmployeeClaim(Session["CompId"].ToString(), "0", "0", Month, Year,"Pending","", "");
        if (Dt.Rows.Count > 0)
        {
            GridViewClaimList.DataSource = Dt;
            GridViewClaimList.DataBind();
            Session["dtFilter"] = Dt;
        }
        else
        {
            Dt.Clear();
            GridViewClaimList.DataSource = Dt;
            GridViewClaimList.DataBind();
            DisplayMessage("Record Not found");
            DdlMonthList.Focus();}

    }
    protected void btnLeast_Click(object sender, EventArgs e)
    {
        PanelUpdateClaim.Visible = false;
        PanelList.BackColor = System.Drawing.ColorTranslator.FromHtml("#ccddee");
        pnlLeave.BackColor = System.Drawing.ColorTranslator.FromHtml("#90bde9");
        DdlMonthList.Focus();
        PanelSearchList.Visible = true;
        PnlEmployeeLeave.Visible = false;
        int CurrentMonth = Convert.ToInt32(DateTime.Now.Month);
        DdlMonthList.SelectedValue = (CurrentMonth).ToString();
        TxtYearList.Text = DateTime.Now.Year.ToString();
        GridBind_ClaimList(DdlMonthList.SelectedValue, TxtYearList.Text);
    }
    protected void BtnBindList_Click(object sender, ImageClickEventArgs e)
    {
        if (DdlMonthList.SelectedIndex == 0)
        {
            DisplayMessage("Select Month");
            ddlMonth.Focus();
            return;
        }
        if (TxtYearList.Text == "")
        {
            DisplayMessage("Enter Year");
            TxtYearList.Focus();
            return;
        }
        GridBind_ClaimList(DdlMonthList.SelectedValue, TxtYearList.Text);
        BtnRefreshList.Focus();
    }
    protected void BtnRefreshList_Click(object sender, ImageClickEventArgs e)
    {
        int CurrentMonth = Convert.ToInt32(DateTime.Now.Month);
        DdlMonthList.SelectedValue = (CurrentMonth).ToString();
        TxtYearList.Text = DateTime.Now.Year.ToString();
        GridBind_ClaimList(DdlMonthList.SelectedValue, TxtYearList.Text);
        DdlMonthList.Focus();

    }
    protected void btnEdit_command(object sender, CommandEventArgs e)
    {
        PanelUpdateClaim.Visible = true;
        HiddeniD.Value = e.CommandArgument.ToString();
        DataTable Dt = new DataTable();
        Dt = ObjClaim.GetRecord_From_PayEmployeeClaim_usingClaimId(Session["CompId"].ToString(), "0", HiddeniD.Value, "0", "0", "","", "");
        if (Dt.Rows.Count > 0)
        {
            TxtClaimNameList.Text = Dt.Rows[0]["Claim_Name"].ToString();
            TxtClaimDiscList.Text = Dt.Rows[0]["Claim_Description"].ToString();
            DdlvalueTypelist.SelectedValue = Dt.Rows[0]["value_Type"].ToString();
            Txtvaluelist.Text = Dt.Rows[0]["Value"].ToString();
            DdlMonthListPanel.SelectedValue = Dt.Rows[0]["Claim_Month"].ToString();
            TxtpanelYearList.Text = Dt.Rows[0]["Claim_Year"].ToString();
            txtEmployeeId.Text = Dt.Rows[0]["Emp_Id"].ToString();
            TxtEmployeeName.Text = Dt.Rows[0]["Emp_Name"].ToString();
                

          
            TxtClaimNameList.Focus();
        }
        
    }
    protected void IbtnDelete_command(object sender, CommandEventArgs e)
    {
        HiddeniD.Value = e.CommandArgument.ToString();
        int CheckDeletion = 0;
        CheckDeletion = ObjClaim.DeleteRecord_in_Pay_Employee_Claim(Session["CompId"].ToString(),HiddeniD.Value, "False", Session["UserId"].ToString(), DateTime.Now.ToString());
        if (CheckDeletion != 0)
        {
            DisplayMessage("Record Deleted");
            DataTable dtGrid = new DataTable();
            dtGrid = ObjClaim.GetRecord_From_PayEmployeeClaim(Session["CompId"].ToString(), "0", "0", DdlMonthList.SelectedValue, TxtYear.Text,"Cancelled" ,"", "");
            GridViewClaimList.DataSource = dtGrid;
            GridViewClaimList.DataBind();
        }
        else
        {
            DisplayMessage("Record Not Deleted");
        }
    }
    void ResetPanel()
    {
        TxtClaimNameList.Text = "";
        TxtClaimDiscList.Text = "";
        DdlvalueTypelist.SelectedIndex = 0;
        Txtvaluelist.Text = "";
        DdlMonthListPanel.SelectedIndex = 0;
        TxtpanelYearList.Text = "";
        HiddeniD.Value = "";
        RbtnApproved.Checked = false;
        RbtnCancelled.Checked = false;
        PanelUpdateClaim.Visible = false;
        txtEmployeeId.Text = "";
        TxtEmployeeName.Text = "";
     
    
    }

    protected void BtnUpdate_Click(object sender, EventArgs e)
    {

        if (HiddeniD.Value == "")
        {DisplayMessage("Edit The Record");
            DdlMonthList.Focus();
            return;
        }

        int count = 0;
        string Status = "";
        if (RbtnApproved.Checked == true)
        {
            count = 1;
            Status = "Approved";
        }
        if (RbtnCancelled.Checked == true)
        {
            count = 1;
            Status = "Cancelled";
        }

        if (count == 0)
        {
            DisplayMessage("Select RadioButton");
            RbtnApproved.Focus();
            Status = "";
            return;
        }

        int UpdationCheck = 0;
        UpdationCheck = ObjClaim.UpdateRecord_In_Pay_Employee_Claim(Session["CompId"].ToString(), HiddeniD.Value, TxtClaimNameList.Text, TxtClaimDiscList.Text, DdlvalueTypelist.SelectedValue, Txtvaluelist.Text, DdlMonthListPanel.SelectedValue, TxtpanelYearList.Text, Status,Session["UserId"].ToString(), DateTime.Now.ToString());

        if (UpdationCheck != 0)
        {
            DisplayMessage("Record Updated");
            DataTable dtGrid = new DataTable();
            dtGrid = ObjClaim.GetRecord_From_PayEmployeeClaim(Session["CompId"].ToString(), "0", "0", DdlMonthList.SelectedValue, TxtYear.Text, "Pending","", "");
            GridViewClaimList.DataSource = dtGrid;
            GridViewClaimList.DataBind();
            PanelUpdateClaim.Visible = false;
            ResetPanel();
            DdlMonthList.Focus();
        }
        else
        {
            DisplayMessage("Record Not Updated");
        }
    }

    protected void GridViewPenaltyList_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        GridViewClaimList.PageIndex = e.NewPageIndex;
        GridBind_ClaimList(DdlMonthList.SelectedValue, TxtYearList.Text);
    }


    protected void btnLeave_Click1(object sender, EventArgs e)
    {}
    protected void BtnResetPenalty_Click(object sender, EventArgs e)
    {
        ResetPanel();
        DdlMonthList.Focus();
    }
    public string GetType(string Type)
    {
        if (Type == "1")
        {
            Type = "Fixed";
        }
        else if (Type == "2")
        {
            Type = "Percentage";

        }
        return Type;
    }
    protected void GridViewPenaltyList_Sorting(object sender, GridViewSortEventArgs e)
    {
        HdfSort.Value = HdfSort.Value == "ASC" ? "DESC" : "ASC";
        DataTable dt = new DataTable();
        dt = (DataTable)Session["dtFilter"];
        DataView dv = new DataView(dt);
        string Query = "" + e.SortExpression + " " + HdfSort.Value + "";
        dv.Sort = Query;
        dt = dv.ToTable();
        Session["dtFilter"] = dt;

        GridViewClaimList.DataSource = dt;
        GridViewClaimList.DataBind();
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
   
}
