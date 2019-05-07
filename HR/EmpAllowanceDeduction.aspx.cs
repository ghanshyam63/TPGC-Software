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

public partial class HR_EmpAllowanceDeduction : System.Web.UI.Page
{
    #region defind Class Object


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

            chkYearCarry.Visible = false;



            Session["empimgpath"] = null;

            Page.Form.Attributes.Add("enctype", "multipart/form-data");
            btnLeave_Click(null,null);

            bool IsCompOT = false;
            bool IsPartialComp = false;

            IsCompOT = Convert.ToBoolean(objAppParam.GetApplicationParameterValueByParamName("IsOverTime", Session["CompId"].ToString()));
            IsPartialComp = Convert.ToBoolean(objAppParam.GetApplicationParameterValueByParamName("Partial_Leave_Enable", Session["CompId"].ToString()));


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

        chkYearCarry.Visible = false;
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
    protected void ddlValueType_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (ddlValueType.SelectedIndex != 0)
        {
            if (ddlValueType.SelectedIndex == 1)
            {
                chkYearCarry.Visible = false;
            }
            else if (ddlValueType.SelectedIndex == 2)
            {
                chkYearCarry.Visible = true;

            }
        }
        else
        {
            chkYearCarry.Visible = false;
        }

        btnSaveLeave.Focus();
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

        chkYearCarry.Visible = false;
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

        chkYearCarry.Visible = false;
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

        chkYearCarry.Visible = false;
    }
   
    
    
    protected void btnLeave_Click(object sender, EventArgs e)
    {

        pnlLeave.BackColor = System.Drawing.ColorTranslator.FromHtml("#ccddee");

        chkYearCarry.Visible = false;
        PnlEmployeeLeave.Visible = true;
        chkYearCarry.Visible = false;
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

    protected void ddlType_SelectedIndexChanged1(object sender, EventArgs e)
    {
        if (ddlType.SelectedIndex == 1)
        {


            DataTable dsEmpCat = null;
            dsEmpCat = ObjAllow.GetAllowanceAll(Session["CompId"].ToString());
            if (dsEmpCat.Rows.Count > 0)
            {

                ddlAllDeduc.DataSource = dsEmpCat;
                ddlAllDeduc.DataTextField = "Allowance";
                ddlAllDeduc.DataValueField = "Allowance_Id";
                ddlAllDeduc.DataBind();

                ddlAllDeduc.Items.Add("--Select--");
                ddlAllDeduc.SelectedValue = "--Select--";
            }
            else
            {
              ddlAllDeduc.Items.Add("--Select--");
              ddlAllDeduc.SelectedValue = "--Select--";
            }

        }
        if (ddlType.SelectedIndex == 2)
        {


            DataTable dsEmpCat = null;
            dsEmpCat = ObjDeduc.GetDeductionAll(Session["CompId"].ToString());
            if (dsEmpCat.Rows.Count > 0)
            {

                ddlAllDeduc.DataSource = dsEmpCat;
                ddlAllDeduc.DataTextField = "Deduction";
                ddlAllDeduc.DataValueField = "Deduction_Id";
                ddlAllDeduc.DataBind();

                ddlAllDeduc.Items.Add("--Select--");
                ddlAllDeduc.SelectedValue = "--Select--";
            }
            else
            {
                ddlAllDeduc.Items.Add("--Select--");
                ddlAllDeduc.SelectedValue = "--Select--";
            }

        }

    }
    public void DisplayMessage(string str)
    {

        ScriptManager.RegisterStartupScript(this, GetType(), "", "alert('" + str + "');", true);

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
            if (ddlType.SelectedIndex == 0)
            {

                DisplayMessage("Select Type");
                return;
            }
            if (ddlAllDeduc.SelectedItem.Text == "--Select--")
            {
                DisplayMessage("Select Allowance or Deduction");
                return;
            }

            if (txtCalValue.Text == "")
            {
                DisplayMessage("Insert Calculation Value");
                return;

            }


            foreach (string str in EmpIds.Split(','))
            {
                if (str != "")
                {


                    DataTable DtAllDeduc = new DataTable();

                    DtAllDeduc = ObjAllDeduc.GetPayAllowDeducAll(Session["CompId"].ToString(), str, ddlType.SelectedValue, ddlAllDeduc.SelectedValue);

                    DtAllDeduc = new DataView(DtAllDeduc, "Emp_Id ='" + str + "' and Type='" + ddlType.SelectedValue + "' and Ref_Id= '" + ddlAllDeduc.SelectedValue + "' ", "", DataViewRowState.CurrentRows).ToTable();

                    if (DtAllDeduc.Rows.Count > 0)
                    {
                        b = ObjAllDeduc.UpdatePayEmpAllowDeduc(Session["CompId"].ToString(),DtAllDeduc.Rows[0]["Trans_Id"].ToString(), str, ddlType.SelectedValue, ddlAllDeduc.SelectedValue, ddlCalculation.SelectedValue.ToString(), ddlValueType.SelectedValue.ToString(), txtCalValue.Text, "", "", "", "", "", true.ToString(), DateTime.Now.ToString(), true.ToString(), Session["UserId"].ToString(), DateTime.Now.ToString(), Session["UserId"].ToString(), DateTime.Now.ToString());

                    }
                    else
                    {
                        b = ObjAllDeduc.InsertPayEmpAllowDeduc(Session["CompId"].ToString(), str, ddlType.SelectedValue, ddlAllDeduc.SelectedValue, ddlCalculation.SelectedValue.ToString(), ddlValueType.SelectedValue.ToString(), txtCalValue.Text, "", "", "", "", "", true.ToString(), DateTime.Now.ToString(), true.ToString(), Session["UserId"].ToString(), DateTime.Now.ToString(), Session["UserId"].ToString(), DateTime.Now.ToString());
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
            if (ddlType.SelectedIndex == 0)
            {

                DisplayMessage("Select Type");
                return;
            }
            if (ddlAllDeduc.SelectedItem.Text == "--Select--")
            {
                DisplayMessage("Select Allowance or Deduction");
                return;
            }

            if (txtCalValue.Text == "")
            {
                DisplayMessage("Insert Calculation Value");
                return;

            }


            foreach (string str in lblSelectRecd.Text.Split(','))
            {
                if (str != "")
                {
                    DataTable DtAllDeducEmp = new DataTable();

                    DtAllDeducEmp = ObjAllDeduc.GetPayAllowDeducAll(Session["CompId"].ToString(), str, ddlType.SelectedValue, ddlAllDeduc.SelectedValue);

                    DtAllDeducEmp = new DataView(DtAllDeducEmp, "Emp_Id ='" + str + "' and Type='" + ddlType.SelectedValue + "' and Ref_Id= '" + ddlAllDeduc.SelectedValue + "' ", "", DataViewRowState.CurrentRows).ToTable();

                    if (DtAllDeducEmp.Rows.Count > 0)
                    {
                        b = ObjAllDeduc.UpdatePayEmpAllowDeduc(Session["CompId"].ToString(),DtAllDeducEmp.Rows[0]["Trans_Id"].ToString() ,str, ddlType.SelectedValue, ddlAllDeduc.SelectedValue, ddlCalculation.SelectedValue.ToString(), ddlValueType.SelectedValue.ToString(), txtCalValue.Text, "0", "0", "0", "0", "0", true.ToString(), DateTime.Now.ToString(), true.ToString(), Session["UserId"].ToString(), DateTime.Now.ToString(), Session["UserId"].ToString(), DateTime.Now.ToString());

                    }
                    else
                    {
                        b = ObjAllDeduc.InsertPayEmpAllowDeduc(Session["CompId"].ToString(), str, ddlType.SelectedValue, ddlAllDeduc.SelectedValue, ddlCalculation.SelectedValue.ToString(), ddlValueType.SelectedValue.ToString(), txtCalValue.Text, "", "", "", "", "", true.ToString(), DateTime.Now.ToString(), true.ToString(), Session["UserId"].ToString(), DateTime.Now.ToString(), Session["UserId"].ToString(), DateTime.Now.ToString());
                    }
                }
            }
        }
        if (b != 0)
        {


            DisplayMessage("Record Saved");





        }
        else
        {
            DisplayMessage("Record Not Saved");
        }

        Reset();



    }

  
    
    protected void btnEmpEdit_Command(object sender, CommandEventArgs e)
    {

        pnl1.Visible = true;
        pnl2.Visible = true;
        RbtBoth.Checked = true;
        DataTable dtBindGrid = new DataTable();
       dtBindGrid = ObjAllDeduc.GetPayAllowDeducByEmpId(e.CommandArgument.ToString());
       if(dtBindGrid.Rows.Count>0)
        {
      
        gvLeaveEmp.DataSource = dtBindGrid;
        gvLeaveEmp.DataBind();

        foreach (GridViewRow gvr in gvLeaveEmp.Rows)
        {
            HiddenField hdnTypeId = (HiddenField)gvr.FindControl("hdngvType");
            HiddenField hdnRefId = (HiddenField)gvr.FindControl("hdnRefId");
            Label lblgvRefValue = (Label)gvr.FindControl("lblgvRefValue");

            if (hdnTypeId.Value != "0" && hdnTypeId.Value != "")
            {
                if (hdnTypeId.Value == "1")
                {
                    DataTable dtAllowance = ObjAllow.GetAllowanceTruebyId(Session["CompId"].ToString(), hdnRefId.Value);
                    if (dtAllowance.Rows.Count > 0)
                    {
                        lblgvRefValue.Text = dtAllowance.Rows[0]["Allowance"].ToString();
                    }
                    else
                    {
                        lblgvRefValue.Text = "";
                    }
                }
                else if (hdnTypeId.Value == "2")
                {
                    DataTable dtDeduction = objDeduction.GetDeductionTruebyId(Session["CompId"].ToString(), hdnRefId.Value);
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

        Session["PayAllow"] = dtBindGrid;
        lblEmpCodeLeave.Text = GetEmployeeCode(e.CommandArgument.ToString());
        lblEmpNameLeave.Text = GetEmployeeName(e.CommandArgument.ToString());
        Session["EmpId"] = e.CommandArgument.ToString();
    }
        else
       {
            pnl1.Visible = false;
            pnl2.Visible = false;
            DisplayMessage("Employee Has not Allowance or Deduction");
            return;
        
        }
    
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

    protected void Imgbtn_Click(object sender, EventArgs e)
    {
        pnl1.Visible = false;
        pnl2.Visible = false;
    }

    protected void Button2_Click(object sender, EventArgs e)
    {
        pnl1.Visible = false;
        pnl2.Visible = false;
    }



    protected void BtnSave_Click(object sender, EventArgs e)
    {


        int c = 0;

        foreach (GridViewRow gvr in gvLeaveEmp.Rows)
        {
            
            CheckBox chk = (CheckBox)gvr.FindControl("ChkEmpCheck");
            if (chk.Checked)
            {
                HiddenField Empid = (HiddenField)gvr.FindControl("hdnEmpId");
                HiddenField hdnTId = (HiddenField)gvr.FindControl("hdntransId");
                c = ObjAllDeduc.DeletePayAllowDeduc(Session["CompId"].ToString(), hdnTId.Value, "False", Session["UserId"].ToString(), DateTime.Now.ToString());

           
            }

            else
            {
                //string Value = gvLeaveEmp.DataKeys[e.RowIndex].Values["Value"].ToString();
                TextBox txtvalue1 = (TextBox)gvr.FindControl("txtValue");
                DropDownList ddlValuetyp = (DropDownList)gvr.FindControl("ddlSchType0");
                DropDownList ddlCalc = (DropDownList)gvr.FindControl("ddlCalcuationGrid");
                HiddenField hdnTrans = (HiddenField)gvr.FindControl("hdntransId");
                HiddenField hdngvType = (HiddenField)gvr.FindControl("hdngvType");
                Label lblgvtype=(Label)gvr.FindControl("lblType");
                HiddenField hdnrefId = (HiddenField)gvr.FindControl("hdnRefId");
                //Label lblvrefId = (Label)gvr.FindControl("lblRefId");


                c = ObjAllDeduc.UpdatePayEmpAllowDeduc(Session["CompId"].ToString(), hdnTrans.Value, Session["EmpId"].ToString(), hdngvType.Value.ToString(), hdnrefId.Value.ToString(), ddlCalc.SelectedValue.ToString(), ddlValuetyp.SelectedValue.ToString(), txtvalue1.Text, "", "", "", "", "", true.ToString(), DateTime.Now.ToString(), true.ToString(), Session["UserId"].ToString(), DateTime.Now.ToString(), Session["UserId"].ToString(), DateTime.Now.ToString());


           
            
            }



          
        }

        DataTable dtBindGrid = new DataTable();
        
        dtBindGrid = ObjAllDeduc.GetPayAllowDeducByEmpId(Session["EmpId"].ToString());
        gvLeaveEmp.DataSource = dtBindGrid;
        gvLeaveEmp.DataBind();
        
        foreach (GridViewRow gvr1 in gvLeaveEmp.Rows)
        {
            HiddenField hdnTypeId = (HiddenField)gvr1.FindControl("hdngvType");
            HiddenField hdnRefId = (HiddenField)gvr1.FindControl("hdnRefId");
            Label lblgvRefValue = (Label)gvr1.FindControl("lblgvRefValue");

            if (hdnTypeId.Value != "0" && hdnTypeId.Value != "")
            {
                if (hdnTypeId.Value == "1")
                {
                    DataTable dtAllowance = ObjAllow.GetAllowanceTruebyId(Session["CompId"].ToString(), hdnRefId.Value);
                    if (dtAllowance.Rows.Count > 0)
                    {
                        lblgvRefValue.Text = dtAllowance.Rows[0]["Allowance"].ToString();
                    }
                    else
                    {
                        lblgvRefValue.Text = "";
                    }
                }
                else if (hdnTypeId.Value == "2")
                {
                    DataTable dtDeduction = objDeduction.GetDeductionTruebyId(Session["CompId"].ToString(), hdnRefId.Value);
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
    


        Session["PayAllow"] = dtBindGrid;
        if (dtBindGrid.Rows.Count == 0)
        {
            pnl1.Visible = false;
            pnl2.Visible = false;

        }
        else
        {
            RbtBoth.Checked = true;
            RbtBoth_CheckedChanged(null, null);
        }
        if (c != 0)
        {
            DisplayMessage("Record Updated");
            Reset();

        }
        else
        {
           DisplayMessage("First Select Record");
        
        }

    }
   

    protected void gvLeaveEmp_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        gvLeaveEmp.PageIndex = e.NewPageIndex;
        gvLeaveEmp.DataSource = (DataTable)Session["PayAllow"];
        gvLeaveEmp.DataBind();
    }
    protected void RbtAllowance_CheckedChanged(object sender, EventArgs e)
    {
        int i = 0;
        if (RbtAllowance.Checked)
        {
            DataTable DtAllow = ObjAllDeduc.GetPayAllowDeducByEmpId(Session["EmpId"].ToString());
            DtAllow = new DataView(DtAllow, "Type='1'", "", DataViewRowState.CurrentRows).ToTable();

            if (DtAllow.Rows.Count > 0)
            {

                gvLeaveEmp.DataSource = DtAllow;
                gvLeaveEmp.DataBind();
                Session["PayAllow"] = DtAllow;

                foreach (GridViewRow gvr4 in gvLeaveEmp.Rows)
                {
                    HiddenField hdnTypeId = (HiddenField)gvr4.FindControl("hdngvType");
                    HiddenField hdnRefId = (HiddenField)gvr4.FindControl("hdnRefId");
                    Label lblgvRefValue = (Label)gvr4.FindControl("lblgvRefValue");

                    if (hdnTypeId.Value != "0" && hdnTypeId.Value != "")
                    {
                        if (hdnTypeId.Value == "1")
                        {
                            DataTable dtAllowance = ObjAllow.GetAllowanceTruebyId(Session["CompId"].ToString(), hdnRefId.Value);
                            if (dtAllowance.Rows.Count > 0)
                            {
                                lblgvRefValue.Text = dtAllowance.Rows[0]["Allowance"].ToString();
                            }
                            else
                            {
                                lblgvRefValue.Text = "";
                            }
                        }
                        else if (hdnTypeId.Value == "2")
                        {
                            DataTable dtDeduction = objDeduction.GetDeductionTruebyId(Session["CompId"].ToString(), hdnRefId.Value);
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

            }
            else
            {
                gvLeaveEmp.DataSource = null;
                gvLeaveEmp.DataBind();



            }

        }

    }
    protected void RbtDeduction_CheckedChanged(object sender, EventArgs e)
    {


        if (RbtDeduction.Checked)
        {
            DataTable DtDeduct = ObjAllDeduc.GetPayAllowDeducByEmpId(Session["EmpId"].ToString());
            DtDeduct = new DataView(DtDeduct, "Type='2'", "", DataViewRowState.CurrentRows).ToTable();

            if (DtDeduct.Rows.Count > 0)
            {

                gvLeaveEmp.DataSource = DtDeduct;
                gvLeaveEmp.DataBind();
                Session["PayAllow"] = DtDeduct;
               
                    foreach (GridViewRow gvr2 in gvLeaveEmp.Rows)
                    {
                        HiddenField hdnTypeId = (HiddenField)gvr2.FindControl("hdngvType");
                        HiddenField hdnRefId = (HiddenField)gvr2.FindControl("hdnRefId");
                        Label lblgvRefValue = (Label)gvr2.FindControl("lblgvRefValue");

                        if (hdnTypeId.Value != "0" && hdnTypeId.Value != "")
                        {
                            if (hdnTypeId.Value == "1")
                            {
                                DataTable dtAllowance = ObjAllow.GetAllowanceTruebyId(Session["CompId"].ToString(), hdnRefId.Value);
                                if (dtAllowance.Rows.Count > 0)
                                {
                                    lblgvRefValue.Text = dtAllowance.Rows[0]["Allowance"].ToString();
                                }
                                else
                                {
                                    lblgvRefValue.Text = "";
                                }
                            }
                            else if (hdnTypeId.Value == "2")
                            {
                                DataTable dtDeduction = objDeduction.GetDeductionTruebyId(Session["CompId"].ToString(), hdnRefId.Value);
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


                }



            }
            else
            {
                gvLeaveEmp.DataSource = null;
                gvLeaveEmp.DataBind();
            }

        
    }
    protected void RbtBoth_CheckedChanged(object sender, EventArgs e)
    {

        if (RbtBoth.Checked)
        {
            DataTable dtBothAllowDeduc = ObjAllDeduc.GetPayAllowDeducByEmpId(Session["EmpId"].ToString());
            dtBothAllowDeduc = new DataView(dtBothAllowDeduc, "", "", DataViewRowState.CurrentRows).ToTable();

            if (dtBothAllowDeduc.Rows.Count > 0)
            {
                gvLeaveEmp.DataSource = dtBothAllowDeduc;
                gvLeaveEmp.DataBind();
                Session["PayAllow"] = dtBothAllowDeduc;
                foreach (GridViewRow gvr3 in gvLeaveEmp.Rows)
                {
                    HiddenField hdnTypeId = (HiddenField)gvr3.FindControl("hdngvType");
                    HiddenField hdnRefId = (HiddenField)gvr3.FindControl("hdnRefId");
                    Label lblgvRefValue = (Label)gvr3.FindControl("lblgvRefValue");

                    if (hdnTypeId.Value != "0" && hdnTypeId.Value != "")
                    {
                        if (hdnTypeId.Value == "1")
                        {
                            DataTable dtAllowance = ObjAllow.GetAllowanceTruebyId(Session["CompId"].ToString(), hdnRefId.Value);
                            if (dtAllowance.Rows.Count > 0)
                            {
                                lblgvRefValue.Text = dtAllowance.Rows[0]["Allowance"].ToString();
                            }
                            else
                            {
                                lblgvRefValue.Text = "";
                            }
                        }
                        else if (hdnTypeId.Value == "2")
                        {
                            DataTable dtDeduction = objDeduction.GetDeductionTruebyId(Session["CompId"].ToString(), hdnRefId.Value);
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


            }

        }
        else
        {
            gvLeaveEmp.DataSource = null;
            gvLeaveEmp.DataBind();



        }


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
   
    public void Reset()
    {
        ddlType.SelectedIndex=0;
        ddlAllDeduc.Items.Clear();
        ddlCalculation.SelectedIndex = 0;
        txtCalValue.Text = "";
        ddlValueType.SelectedIndex = 0;
        btnLeave_Click(null, null);
    
    }



    protected void btnResetLeave_Click(object sender, EventArgs e)
    {
        ddlType.SelectedIndex = 0;
        if (ddlType.SelectedIndex ==0)
        {
            ddlAllDeduc.Items.Clear();

        }
        ddlCalculation.SelectedIndex =0;
        
        txtCalValue.Text = "";
        ddlValueType.SelectedIndex = 0;

        btnLeave_Click(null, null);

    }
}

