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
using System.IO;
using System.Text.RegularExpressions;
using System.Data.OleDb;

public partial class Master_EmployeeMaster : BasePage
{
    #region defind Class Object

    Att_DeviceMaster objDevice = new Att_DeviceMaster();
    Ser_UserTransfer objSer = new Ser_UserTransfer();
    Common ObjComman = new Common();
    Set_Bank_Info objBankInfo = new Set_Bank_Info();
    CountryMaster ObjSysCountryMaster = new CountryMaster();
    BrandMaster ObjBrandMaster = new BrandMaster();
    LocationMaster ObjLocMaster = new LocationMaster();
    Set_BankMaster objBank = new Set_BankMaster();
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
            lnkPrev.Visible = false;
            lnkNext.Visible = false;
            lnkFirst.Visible = false;
            lnkLast.Visible = false;
            imbBtnGrid.Visible = true;
            chkYearCarry.Visible = false;

            imgBtnDatalist.Visible = false;
            ViewState["CurrIndexbin"] = 0;
            ViewState["SubSizebin"] = 9;
            lnkbinFirst.Visible = false;
            lnkbinPrev.Visible = false;
            lnkbinNext.Visible = false;
            lnkbinLast.Visible = false;


            Session["empimgpath"] = null;
            pnlMap.Visible = false;

            pnlshowdata.Visible = false;

            btnList_Click(null, null);
            FillCurrencyDDL();
            FillCurrency1DDL();
            FillDataListGrid();
            FillbinDataListGrid();
            FillDepDDL();
            FillLeaveTypeDDL();
            FillDesignationDDL();
            FillQualificationDDL();
            FillReligionDDL();
            FillNationalityDDL();
            Page.Form.Attributes.Add("enctype", "multipart/form-data");
            txtValue.Focus();

            bool IsCompOT = false;
            bool IsPartialComp = false;

            IsCompOT = Convert.ToBoolean(objAppParam.GetApplicationParameterValueByParamName("IsOverTime", Session["CompId"].ToString()));
            IsPartialComp = Convert.ToBoolean(objAppParam.GetApplicationParameterValueByParamName("Partial_Leave_Enable", Session["CompId"].ToString()));


            if (IsCompOT)
            {
                rbtnOTEnable.Checked = true;
                rbtnOTDisable.Checked = false;
                rbtOT_OnCheckedChanged(null, null);




            }
            else
            {
                rbtnOTEnable.Checked = false;
                rbtnOTDisable.Checked = true;
                rbtOT_OnCheckedChanged(null, null);


                rbtnOTEnable.Enabled = false;
                rbtnOTDisable.Enabled = false;

            }

            if (IsPartialComp)
            {
                rbtnPartialEnable.Checked = true;
                rbtnPartialDisable.Checked = false;

                rbtPartial_OnCheckedChanged(null, null);



            }
            else
            {
                rbtnPartialEnable.Checked = false;
                rbtnPartialDisable.Checked = true;

                rbtPartial_OnCheckedChanged(null, null);

                rbtnPartialEnable.Enabled = false;
                rbtnPartialDisable.Enabled = false;
            }


        }

        Page.Title = objSys.GetSysTitle();
        CalendarExtender2.Format = objSys.SetDateFormat();
        CalendarExtender1.Format = objSys.SetDateFormat();
        txtTermDate_CalendarExtender.Format = objSys.SetDateFormat();
    }

    protected void txtEmpName_OnTextChanged(object sender, EventArgs e)
    {

        if (editid.Value == "")
        {
            DataTable dt = objEmp.GetEmployeeMasterByEmpName(Session["CompId"].ToString().ToString(), txtEmployeeName.Text.Trim());
            if (dt.Rows.Count > 0)
            {
                txtEmployeeName.Text = "";
                DisplayMessage("Employee Name Already Exists");
                System.Web.UI.ScriptManager.GetCurrent(this).SetFocus(txtEmployeeName);
                return;
            }
            DataTable dt1 = objEmp.GetEmployeeMasterInactive(Session["CompId"].ToString().ToString());
            dt1 = new DataView(dt1, "Emp_Name='" + txtEmployeeName.Text + "'", "", DataViewRowState.CurrentRows).ToTable();
            if (dt1.Rows.Count > 0)
            {
                txtEmployeeName.Text = "";
                DisplayMessage("Employee Name Already Exists - Go to Bin Tab");
                System.Web.UI.ScriptManager.GetCurrent(this).SetFocus(txtEmployeeName);
                return;
            }
            txtEmployeeL.Focus();
        }
        else
        {
            DataTable dtTemp = objEmp.GetEmployeeMasterById(Session["CompId"].ToString().ToString(), editid.Value);
            if (dtTemp.Rows.Count > 0)
            {
                if (dtTemp.Rows[0]["Emp_Name"].ToString() != txtEmployeeName.Text)
                {
                    DataTable dt = objEmp.GetEmployeeMaster(Session["CompId"].ToString().ToString());
                    dt = new DataView(dt, "Emp_Name='" + txtEmployeeName.Text + "'", "", DataViewRowState.CurrentRows).ToTable();
                    if (dt.Rows.Count > 0)
                    {
                        txtEmployeeName.Text = "";
                        DisplayMessage("Employee Name Already Exists");
                        System.Web.UI.ScriptManager.GetCurrent(this).SetFocus(txtEmployeeName);
                        return;
                    }
                    DataTable dt1 = objEmp.GetEmployeeMasterInactive(Session["CompId"].ToString().ToString());
                    dt1 = new DataView(dt1, "Emp_Name='" + txtEmployeeName.Text + "'", "", DataViewRowState.CurrentRows).ToTable();
                    if (dt1.Rows.Count > 0)
                    {
                        txtEmployeeName.Text = "";
                        DisplayMessage("Employee Name Already Exists - Go to Bin Tab");
                        System.Web.UI.ScriptManager.GetCurrent(this).SetFocus(txtEmployeeName);
                        return;
                    }
                }
            }
            txtEmployeeL.Focus();
        }
    }


    public void FillCurrencyDDL()
    {
        DataTable dt = objCurrency.GetCurrencyMaster();

        DataTable dtComp = new DataTable();

        dtComp = objComp.GetCompanyMasterById(Session["CompId"].ToString());

        string Currency_Id = dtComp.Rows[0]["Currency_Id"].ToString();

        if (dt.Rows.Count > 0)
        {
            ddlCurrency.DataSource = null;
            ddlCurrency.DataBind();
            ddlCurrency.DataSource = dt;
            ddlCurrency.DataTextField = "Currency_Name";
            ddlCurrency.DataValueField = "Currency_Id";
            ddlCurrency.DataBind();
            ListItem li = new ListItem("--Select--", "0");
            ddlCurrency.Items.Insert(0, li);
            ddlCurrency.SelectedValue = Currency_Id;

        }
        else
        {
            try
            {
                ddlCurrency.Items.Clear();
                ddlCurrency.DataSource = null;
                ddlCurrency.DataBind();
                ListItem li = new ListItem("--Select--", "0");
                ddlCurrency.Items.Insert(0, li);
                ddlCurrency.SelectedIndex = 0;
            }
            catch
            {
                ListItem li = new ListItem("--Select--", "0");
                ddlCurrency.Items.Insert(0, li);
                ddlCurrency.SelectedIndex = 0;

            }
        }

        ddlWorkCalMethod.SelectedValue = objAppParam.GetApplicationParameterValueByParamName("Effective Work Calculation Method", Session["CompId"].ToString());


    }
    public void FillCurrency1DDL()
    {
        DataTable dt = objCurrency.GetCurrencyMaster();


        if (dt.Rows.Count > 0)
        {
            ddlCurrency1.DataSource = null;
            ddlCurrency1.DataBind();
            ddlCurrency1.DataSource = dt;
            ddlCurrency1.DataTextField = "Currency_Name";
            ddlCurrency1.DataValueField = "Currency_Id";
            ddlCurrency1.DataBind();
            ListItem li = new ListItem("--Select--", "0");
            ddlCurrency1.Items.Insert(0, li);


        }
        else
        {
            try
            {
                ddlCurrency1.Items.Clear();
                ddlCurrency1.DataSource = null;
                ddlCurrency1.DataBind();
                ListItem li = new ListItem("--Select--", "0");
                ddlCurrency1.Items.Insert(0, li);
                ddlCurrency1.SelectedIndex = 0;
            }
            catch
            {
                ListItem li = new ListItem("--Select--", "0");
                ddlCurrency1.Items.Insert(0, li);
                ddlCurrency1.SelectedIndex = 0;

            }
        }



    }
    public void AllPageCode()
    {


        Session["AccordianId"] = "8";
        Session["HeaderText"] = "Master Setup";
        Common cmn = new Common();
        DataTable dtAllPageCode = cmn.GetAllPagePermission(Session["UserId"].ToString(), "8", "15");
        if (dtAllPageCode.Rows.Count != 0)
        {
            if (dtAllPageCode.Rows[0][0].ToString() == "SuperAdmin")
            {
                btnSave.Visible = true;
                foreach (GridViewRow Row in gvEmp.Rows)
                {
                    ((ImageButton)Row.FindControl("btnEdit")).Visible = true;
                    ((ImageButton)Row.FindControl("btnDelete")).Visible = true;
                }
                imgBtnRestore.Visible = true;
                ImgbtnSelectAll.Visible = true;
                for (int i = 0; i < dtlistbinEmp.Items.Count; i++)
                {
                    ((CheckBox)dtlistbinEmp.Items[i].FindControl("chbAcInctive")).Visible = true;

                }

                for (int i = 0; i < dtlistEmp.Items.Count; i++)
                {
                    ((LinkButton)dtlistEmp.Items[i].FindControl("lnkEmpname")).Enabled = true;
                    ((ImageButton)dtlistEmp.Items[i].FindControl("ImgBtnEmp")).Enabled = true;
                }
            }
            else
            {
                foreach (DataRow DtRow in dtAllPageCode.Rows)
                {
                    if (Convert.ToBoolean(DtRow["Op_Add"].ToString()))
                    {
                        btnSave.Visible = true;
                    }
                    foreach (GridViewRow Row in gvEmp.Rows)
                    {
                        if (Convert.ToBoolean(DtRow["Op_Edit"].ToString()))
                        {
                            ((ImageButton)Row.FindControl("btnEdit")).Visible = true;
                        }
                        if (Convert.ToBoolean(DtRow["Op_Delete"].ToString()))
                        {
                            ((ImageButton)Row.FindControl("btnDelete")).Visible = true;
                        }
                    }

                    if (Convert.ToBoolean(DtRow["Op_Edit"].ToString()))
                    {
                        for (int i = 0; i < dtlistEmp.Items.Count; i++)
                        {
                            ((LinkButton)dtlistEmp.Items[i].FindControl("lnkEmpname")).Enabled = true;
                            ((ImageButton)dtlistEmp.Items[i].FindControl("ImgBtnEmp")).Enabled = true;
                        }
                    }

                    if (Convert.ToBoolean(DtRow["Op_Restore"].ToString()))
                    {
                        imgBtnRestore.Visible = true;
                        ImgbtnSelectAll.Visible = true;

                        for (int i = 0; i < dtlistbinEmp.Items.Count; i++)
                        {
                            ((CheckBox)dtlistbinEmp.Items[i].FindControl("chbAcInctive")).Visible = true;

                        }


                    }
                    if (Convert.ToBoolean(DtRow["Op_View"].ToString()))
                    {

                    }
                    if (Convert.ToBoolean(DtRow["Op_Print"].ToString()))
                    {

                    }
                    if (Convert.ToBoolean(DtRow["Op_Download"].ToString()))
                    {

                    }
                    if (Convert.ToBoolean(DtRow["Op_Upload"].ToString()))
                    {

                    }
                }

            }


        }


    }
    [System.Web.Services.WebMethodAttribute(), System.Web.Script.Services.ScriptMethodAttribute()]
    public static string[] GetCompletionListEmployeeCode(string prefixText, int count, string contextKey)
    {
        EmployeeMaster ObjEmployeeMaster = new EmployeeMaster();

        DataTable dt = ObjEmployeeMaster.GetEmployeeMaster(HttpContext.Current.Session["CompId"].ToString());

        dt = new DataView(dt, "Brand_Id='" + HttpContext.Current.Session["BrandId"].ToString() + "' and Location_Id='" + HttpContext.Current.Session["LocId"].ToString() + "' ", "", DataViewRowState.CurrentRows).ToTable();

        dt = new DataView(dt, "Emp_Code like '" + prefixText.ToString() + "%'", "", DataViewRowState.CurrentRows).ToTable();
        string[] txt = new string[dt.Rows.Count];

        for (int i = 0; i < dt.Rows.Count; i++)
        {
            txt[i] = dt.Rows[i]["Emp_Code"].ToString();
        }
        return txt;
    }
    [System.Web.Services.WebMethodAttribute(), System.Web.Script.Services.ScriptMethodAttribute()]
    public static string[] GetCompletionListEmployeeName(string prefixText, int count, string contextKey)
    {
        EmployeeMaster ObjEmployeeMaster = new EmployeeMaster();

        DataTable dt = ObjEmployeeMaster.GetEmployeeMaster(HttpContext.Current.Session["CompId"].ToString());

        dt = new DataView(dt, "Brand_Id='" + HttpContext.Current.Session["BrandId"].ToString() + "' and Location_Id='" + HttpContext.Current.Session["LocId"].ToString() + "' ", "", DataViewRowState.CurrentRows).ToTable();

        dt = new DataView(dt, "Emp_Name like '" + prefixText.ToString() + "%'", "", DataViewRowState.CurrentRows).ToTable();
        string[] txt = new string[dt.Rows.Count];

        for (int i = 0; i < dt.Rows.Count; i++)
        {
            txt[i] = dt.Rows[i]["Emp_Name"].ToString();
        }
        return txt;
    }


    public void FillDepDDL()
    {
        DataTable dt = objDep.GetDepartmentMaster(Session["CompId"].ToString());

        dt = new DataView(dt, "Brand_Id='" + Session["BrandId"].ToString() + "' and Location_Id='" + Session["LocId"].ToString() + "' ", "", DataViewRowState.CurrentRows).ToTable();


        if (dt.Rows.Count > 0)
        {
            ddlDepartment.DataSource = null;
            ddlDepartment.DataBind();
            ddlDepartment.DataSource = dt;
            ddlDepartment.DataTextField = "Dep_Name";
            ddlDepartment.DataValueField = "Dep_Id";
            ddlDepartment.DataBind();
            ListItem li = new ListItem("--Select--", "0");
            ddlDepartment.Items.Insert(0, li);
            ddlDepartment.SelectedIndex = 0;

        }
        else
        {
            try
            {
                ddlDepartment.Items.Clear();
                ddlDepartment.DataSource = null;
                ddlDepartment.DataBind();
                ListItem li = new ListItem("--Select--", "0");
                ddlDepartment.Items.Insert(0, li);
                ddlDepartment.SelectedIndex = 0;
            }
            catch
            {
                ListItem li = new ListItem("--Select--", "0");
                ddlDepartment.Items.Insert(0, li);
                ddlDepartment.SelectedIndex = 0;

            }
        }

    }


    public void FillLeaveTypeDDL()
    {
        DataTable dt = objLeaveType.GetLeaveMaster(Session["CompId"].ToString());
        dt = new DataView(dt, "Brand_Id='" + Session["BrandId"].ToString() + "'", "", DataViewRowState.CurrentRows).ToTable();

        if (dt.Rows.Count > 0)
        {
            ddlLeaveType.DataSource = null;
            ddlLeaveType.DataBind();
            ddlLeaveType.DataSource = dt;
            ddlLeaveType.DataTextField = "Leave_Name";
            ddlLeaveType.DataValueField = "Leave_Id";
            ddlLeaveType.DataBind();
            ListItem li = new ListItem("--Select--", "0");
            ddlLeaveType.Items.Insert(0, li);
            ddlLeaveType.SelectedIndex = 0;

        }
        else
        {
            ddlLeaveType.Items.Clear();
            ddlLeaveType.DataSource = null;
            ddlLeaveType.DataBind();
            ListItem li = new ListItem("--Select--", "0");
            ddlLeaveType.Items.Insert(0, li);
            ddlLeaveType.SelectedIndex = 0;

        }

    }










    public void FillReligionDDL()
    {
        DataTable dt = objRel.GetReligionMaster(Session["CompId"].ToString());

        dt = new DataView(dt, "", "", DataViewRowState.CurrentRows).ToTable();


        if (dt.Rows.Count > 0)
        {
            ddlReligion.DataSource = null;
            ddlReligion.DataBind();
            ddlReligion.DataSource = dt;
            ddlReligion.DataTextField = "Religion";
            ddlReligion.DataValueField = "Religion_Id";
            ddlReligion.DataBind();
            ListItem li = new ListItem("--Select--", "0");
            ddlReligion.Items.Insert(0, li);
            ddlReligion.SelectedIndex = 0;

        }
        else
        {
            try
            {
                ddlReligion.Items.Clear();
                ddlReligion.DataSource = null;
                ddlReligion.DataBind();
                ListItem li = new ListItem("--Select--", "0");
                ddlReligion.Items.Insert(0, li);
                ddlReligion.SelectedIndex = 0;
            }
            catch
            {
                ListItem li = new ListItem("--Select--", "0");
                ddlReligion.Items.Insert(0, li);
                ddlReligion.SelectedIndex = 0;

            }
        }

    }
    public void FillNationalityDDL()
    {
        DataTable dt = objNat.GetNationalityMaster(Session["CompId"].ToString());

        dt = new DataView(dt, "", "", DataViewRowState.CurrentRows).ToTable();


        if (dt.Rows.Count > 0)
        {
            ddlNationality.DataSource = null;
            ddlNationality.DataBind();
            ddlNationality.DataSource = dt;
            ddlNationality.DataTextField = "Nationality";
            ddlNationality.DataValueField = "Nationality_Id";
            ddlNationality.DataBind();
            ListItem li = new ListItem("--Select--", "0");
            ddlNationality.Items.Insert(0, li);
            ddlNationality.SelectedIndex = 0;

        }
        else
        {
            try
            {
                ddlNationality.Items.Clear();
                ddlNationality.DataSource = null;
                ddlNationality.DataBind();
                ListItem li = new ListItem("--Select--", "0");
                ddlNationality.Items.Insert(0, li);
                ddlNationality.SelectedIndex = 0;
            }
            catch
            {
                ListItem li = new ListItem("--Select--", "0");
                ddlNationality.Items.Insert(0, li);
                ddlNationality.SelectedIndex = 0;

            }
        }

    }
    public void FillDesignationDDL()
    {
        DataTable dt = objDesg.GetDesignationMaster(Session["CompId"].ToString());

        dt = new DataView(dt, "", "", DataViewRowState.CurrentRows).ToTable();


        if (dt.Rows.Count > 0)
        {
            ddlDesignation.DataSource = null;
            ddlDesignation.DataBind();
            ddlDesignation.DataSource = dt;
            ddlDesignation.DataTextField = "Designation";
            ddlDesignation.DataValueField = "Designation_Id";
            ddlDesignation.DataBind();
            ListItem li = new ListItem("--Select--", "0");
            ddlDesignation.Items.Insert(0, li);
            ddlDesignation.SelectedIndex = 0;

        }
        else
        {
            try
            {
                ddlDesignation.Items.Clear();
                ddlDesignation.DataSource = null;
                ddlDesignation.DataBind();
                ListItem li = new ListItem("--Select--", "0");
                ddlDesignation.Items.Insert(0, li);
                ddlDesignation.SelectedIndex = 0;
            }
            catch
            {
                ListItem li = new ListItem("--Select--", "0");
                ddlDesignation.Items.Insert(0, li);
                ddlDesignation.SelectedIndex = 0;

            }
        }

    }

    public void FillQualificationDDL()
    {
        DataTable dt = objQualif.GetQualificationMaster(Session["CompId"].ToString());

        dt = new DataView(dt, "", "", DataViewRowState.CurrentRows).ToTable();


        if (dt.Rows.Count > 0)
        {
            ddlQualification.DataSource = null;
            ddlQualification.DataBind();
            ddlQualification.DataSource = dt;
            ddlQualification.DataTextField = "Qualification";
            ddlQualification.DataValueField = "Qualification_Id";
            ddlQualification.DataBind();
            ListItem li = new ListItem("--Select--", "0");
            ddlQualification.Items.Insert(0, li);
            ddlQualification.SelectedIndex = 0;

        }
        else
        {
            try
            {
                ddlQualification.Items.Clear();
                ddlQualification.DataSource = null;
                ddlQualification.DataBind();
                ListItem li = new ListItem("--Select--", "0");
                ddlQualification.Items.Insert(0, li);
                ddlQualification.SelectedIndex = 0;
            }
            catch
            {
                ListItem li = new ListItem("--Select--", "0");
                ddlQualification.Items.Insert(0, li);
                ddlQualification.SelectedIndex = 0;

            }
        }

    }

    public bool checkDatalist(object empid)
    {
        DataTable dtemp = objEmp.GetEmployeeMasterInactive(Session["CompId"].ToString());
        dtemp = new DataView(dtemp, "Emp_Id='" + empid.ToString().Trim() + "'", "", DataViewRowState.CurrentRows).ToTable();

        string chkdata = string.Empty;

        if (dtemp.Rows.Count > 0)
        {
            chkdata = dtemp.Rows[0]["IsActive"].ToString();

        }





        return Convert.ToBoolean(chkdata);



    }


    protected void btnbind_Click(object sender, ImageClickEventArgs e)
    {
        ViewState["CurrIndex"] = 0;
        ViewState["SubSize"] = 9;
        DataTable dtproduct = objEmp.GetEmployeeMaster(Session["CompId"].ToString());

        dtproduct = new DataView(dtproduct, "Brand_Id='" + Session["BrandId"].ToString() + "'  and Location_Id='" + Session["LocId"].ToString() + "'", "", DataViewRowState.CurrentRows).ToTable();



        if (dtproduct.Rows.Count > 0)
        {

            lnkNext.Visible = true;
            lnkLast.Visible = true;
        }
        if (ddlOption.SelectedIndex != 0)
        {
            string condition = string.Empty;
            if (ddlOption.SelectedIndex == 1)
            {
                condition = "convert(" + ddlFieldName.SelectedValue + ",System.String)='" + txtValue.Text.Trim() + "'";
            }
            else if (ddlOption.SelectedIndex == 2)
            {
                condition = "convert(" + ddlFieldName.SelectedValue + ",System.String) Like '" + txtValue.Text.Trim() + "%'";
            }
            else
            {
                condition = "convert(" + ddlFieldName.SelectedValue + ",System.String) like '%" + txtValue.Text.Trim() + "%'";
            }
            DataView view = new DataView(dtproduct, condition, "", DataViewRowState.CurrentRows);

            lblTotalRecord.Text = Resources.Attendance.Total_Records + " : " + (view.ToTable()).Rows.Count.ToString() + "";

            Session["dtEmp"] = view.ToTable();

            dtproduct = view.ToTable();
            if (dtproduct.Rows.Count <= 9)
            {
                dtlistEmp.DataSource = dtproduct;
                dtlistEmp.DataBind();
                gvEmp.DataSource = dtproduct;
                gvEmp.DataBind();
                lnkPrev.Visible = false;
                lnkFirst.Visible = false;
                lnkNext.Visible = false;
                lnkLast.Visible = false;


            }
            else
            {
                FillDataList(dtproduct, Convert.ToInt32(ViewState["CurrIndex"].ToString()), Convert.ToInt32(ViewState["SubSize"].ToString()));

            }

        }
        AllPageCode();
    }




    protected void btnRefresh_Click(object sender, ImageClickEventArgs e)
    {

        lnkNext.Visible = true;
        lnkLast.Visible = true;

        ViewState["CurrIndex"] = 0;
        ViewState["SubSize"] = 9;
        FillDataListGrid();
        ddlFieldName.SelectedIndex = 1;
        ddlOption.SelectedIndex = 3;
        txtValue.Text = "";
        btngo_Click(null, null);


        System.Web.UI.ScriptManager.GetCurrent(this).SetFocus(txtValue);
    }
    protected void btngo_Click(object sender, EventArgs e)
    {
        ViewState["CurrIndex"] = 0;
        ViewState["SubSize"] = 9;
        lnkFirst.Visible = false;
        lnkPrev.Visible = false;
        lnkNext.Visible = true;
        lnkLast.Visible = true;
        FillDataListGrid();

    }


    protected void imbBtnGrid_Click(object sender, ImageClickEventArgs e)
    {
        lnkNext.Visible = false;
        lnkLast.Visible = false;
        lnkFirst.Visible = false;
        lnkPrev.Visible = false;
        dtlistEmp.Visible = false;
        gvEmp.Visible = true;
        FillDataListGrid();
        imgBtnDatalist.Visible = true;
        imbBtnGrid.Visible = false;
        txtValue.Focus();

    }
    protected void imgBtnDatalist_Click(object sender, ImageClickEventArgs e)
    {

        ViewState["CurrIndex"] = 0;
        ViewState["SubSize"] = 9;

        lnkNext.Visible = true;
        lnkLast.Visible = true;
        dtlistEmp.Visible = true;
        gvEmp.Visible = false;
        FillDataListGrid();
        imgBtnDatalist.Visible = false;
        imbBtnGrid.Visible = true;

        txtValue.Focus();

    }
    protected void btnResetSreach_Click(object sender, EventArgs e)
    {


        FillDataListGrid();
        System.Web.UI.ScriptManager.GetCurrent(this).SetFocus(txtValue);
    }


    protected void lnkFirst_Click(object sender, EventArgs e)
    {
        lnkPrev.Visible = false;
        lnkFirst.Visible = false;
        lnkLast.Visible = true;
        lnkNext.Visible = true;
        ViewState["CurrIndex"] = 0;
        ViewState["SubSize"] = 9;
        DataTable dt = (DataTable)Session["dtEmp"];
        FillDataList(dt, Convert.ToInt32(ViewState["CurrIndex"].ToString()), Convert.ToInt32(ViewState["SubSize"].ToString()));
        ViewState["SubSize"] = 9;

        int index = dt.Rows.Count / Convert.ToInt32(ViewState["SubSize"].ToString());
    }
    protected void lnkLast_Click(object sender, EventArgs e)
    {
        ViewState["SubSize"] = 9;
        DataTable dt = (DataTable)Session["dtEmp"];
        int index = dt.Rows.Count / Convert.ToInt32(ViewState["SubSize"].ToString());

        ViewState["CurrIndex"] = index;
        int tot = dt.Rows.Count;

        if (tot % Convert.ToInt32(ViewState["SubSize"].ToString()) > 0)
        {
            FillDataList(dt, Convert.ToInt32(ViewState["CurrIndex"].ToString()), Convert.ToInt32(ViewState["SubSize"].ToString()));
        }
        else if (tot % Convert.ToInt32(ViewState["SubSize"].ToString()) == 0)
        {
            FillDataList(dt, index - 1, Convert.ToInt32(ViewState["SubSize"].ToString()));
        }
        else
        {
            FillDataList(dt, Convert.ToInt32(ViewState["CurrIndex"].ToString()), Convert.ToInt32(ViewState["SubSize"].ToString()));
        }
        lnkLast.Visible = false;
        lnkNext.Visible = false;
        lnkPrev.Visible = true;
        lnkFirst.Visible = true;
    }
    protected void lnkPrev_Click(object sender, EventArgs e)
    {
        DataTable dt = (DataTable)Session["dtEmp"];
        ViewState["SubSize"] = 9;
        ViewState["CurrIndex"] = Convert.ToInt32(ViewState["CurrIndex"].ToString()) - 1;
        if (Convert.ToInt32(ViewState["CurrIndex"].ToString()) < 0)
        {
            ViewState["CurrIndex"] = 0;


        }

        if (Convert.ToInt16(ViewState["CurrIndex"]) == 0)
        {

            lnkFirst.Visible = false;
            lnkPrev.Visible = false;
            lnkNext.Visible = true;
            lnkLast.Visible = true;
        }
        else
        {
            lnkFirst.Visible = true;
            lnkLast.Visible = true;
            lnkNext.Visible = true;
        }
        FillDataList(dt, Convert.ToInt32(ViewState["CurrIndex"].ToString()), Convert.ToInt32(ViewState["SubSize"].ToString()));
    }
    protected void lnkNext_Click(object sender, EventArgs e)
    {
        ViewState["SubSize"] = 9;
        DataTable dt = (DataTable)Session["dtEmp"];
        int index = dt.Rows.Count / Convert.ToInt32(ViewState["SubSize"].ToString());
        int k1 = Convert.ToInt32(ViewState["CurrIndex"].ToString());
        ViewState["CurrIndex"] = Convert.ToInt32(ViewState["CurrIndex"].ToString()) + 1;
        int k = Convert.ToInt32(ViewState["CurrIndex"].ToString());
        if (Convert.ToInt32(ViewState["CurrIndex"].ToString()) >= index)
        {
            ViewState["CurrIndex"] = index;
            lnkNext.Visible = false;
            lnkLast.Visible = false;
        }
        int tot = dt.Rows.Count;

        if (k == index)
        {
            if (tot % Convert.ToInt32(ViewState["SubSize"].ToString()) > 0)
            {
                FillDataList(dt, Convert.ToInt32(ViewState["CurrIndex"].ToString()), Convert.ToInt32(ViewState["SubSize"].ToString()));
                lnkPrev.Visible = true;
                lnkFirst.Visible = true;

            }
            else
            {
                lnkPrev.Visible = true;
                lnkFirst.Visible = true;
                lnkLast.Visible = true;
                lnkNext.Visible = true;
            }
        }
        else if (k < index)
        {
            if (k + 1 == index)
            {
                if (tot % Convert.ToInt32(ViewState["SubSize"].ToString()) > 0)
                {
                    FillDataList(dt, Convert.ToInt32(ViewState["CurrIndex"].ToString()), Convert.ToInt32(ViewState["SubSize"].ToString()));
                    lnkPrev.Visible = true;
                    lnkFirst.Visible = true;
                }
                else
                {
                    FillDataList(dt, Convert.ToInt32(ViewState["CurrIndex"].ToString()), Convert.ToInt32(ViewState["SubSize"].ToString()));
                    lnkNext.Visible = false;
                    lnkLast.Visible = false;
                    lnkPrev.Visible = true;
                    lnkFirst.Visible = true;
                }
            }
            else
            {
                FillDataList(dt, Convert.ToInt32(ViewState["CurrIndex"].ToString()), Convert.ToInt32(ViewState["SubSize"].ToString()));
                lnkPrev.Visible = true;
                lnkFirst.Visible = true;
            }
        }
        else
        {
            FillDataList(dt, Convert.ToInt32(ViewState["CurrIndex"].ToString()), Convert.ToInt32(ViewState["SubSize"].ToString()));
        }



    }






    protected void rbtOT1_OnCheckedChanged(object sender, EventArgs e)
    {
        if (rbtnOTEnable1.Checked)
        {
            ddlOTCalc1.Enabled = true;
            txtNormal1.Enabled = true;
            txtWeekOffValue1.Enabled = true;
            txtHolidayValue1.Enabled = true;
            ddlNormalType1.Enabled = true;
            ddlWeekOffType1.Enabled = true;
            ddlHolidayValue1.Enabled = true;



        }
        else
        {
            ddlOTCalc1.Enabled = false;
            txtNormal1.Enabled = false;
            txtWeekOffValue1.Enabled = false;
            txtHolidayValue1.Enabled = false;
            ddlNormalType1.Enabled = false;
            ddlWeekOffType1.Enabled = false;
            ddlHolidayValue1.Enabled = false;

        }

    }


    protected void rbtOT_OnCheckedChanged(object sender, EventArgs e)
    {
        if (rbtnOTEnable.Checked)
        {
            ddlOTCalc.Enabled = true;
            txtNoralType.Enabled = true;
            txtWeekOffValue.Enabled = true;
            txtHolidayValue.Enabled = true;
            ddlNormalType.Enabled = true;
            ddlWeekOffType.Enabled = true;
            ddlHolidayType.Enabled = true;


        }
        else
        {
            ddlOTCalc.Enabled = false;
            txtNoralType.Enabled = false;
            txtWeekOffValue.Enabled = false;
            txtHolidayValue.Enabled = false;
            ddlNormalType.Enabled = false;
            ddlWeekOffType.Enabled = false;
            ddlHolidayType.Enabled = false;

        }

    }


    protected void rbtPartial_OnCheckedChanged(object sender, EventArgs e)
    {
        if (rbtnPartialEnable.Checked)
        {
            rbtnCarryYes.Enabled = true;
            rbtnCarryNo.Enabled = true;
            txtTotalMinutes.Enabled = true;
            txtMinuteday.Enabled = true;


        }
        else
        {
            rbtnCarryYes.Enabled = false;
            rbtnCarryNo.Enabled = false;
            txtTotalMinutes.Enabled = false;
            txtMinuteday.Enabled = false;

        }

    }

    protected void rbtPartial1_OnCheckedChanged(object sender, EventArgs e)
    {
        if (rbtnPartialEnable1.Checked)
        {
            rbtnCarryYes1.Enabled = true;
            rbtnCarryNo1.Enabled = true;
            txtTotalMinutesP1.Enabled = true;
            txtMinuteOTOne.Enabled = true;


        }
        else
        {
            rbtnCarryYes1.Enabled = false;
            rbtnCarryNo1.Enabled = false;
            txtTotalMinutesP1.Enabled = false;
            txtMinuteOTOne.Enabled = false;

        }

    }
    protected void gvEmp1_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        gvEmployee.PageIndex = e.NewPageIndex;
        gvEmployee.DataSource = (DataTable)Session["dtEmp1"];
        gvEmployee.DataBind();
    }


    protected void gvEmployeeNF_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        gvEmployeeNF.PageIndex = e.NewPageIndex;
        gvEmployeeNF.DataSource = (DataTable)Session["dtEmp2"];
        gvEmployeeNF.DataBind();
    }
    protected void gvEmployeePenalty_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        gvEmployeePenalty.PageIndex = e.NewPageIndex;
        gvEmployeePenalty.DataSource = (DataTable)Session["dtEmp10"];
        gvEmployeePenalty.DataBind();
    }

    protected void gvEmployeeOT_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        gvEmployeeOT.PageIndex = e.NewPageIndex;
        gvEmployeeOT.DataSource = (DataTable)Session["dtEmp5"];
        gvEmployeeOT.DataBind();
    }
    protected void gvEmployeeSal_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        gvEmployeeSal.PageIndex = e.NewPageIndex;
        gvEmployeeSal.DataSource = (DataTable)Session["dtEmp4"];
        gvEmployeeSal.DataBind();
    }

    protected void gvEmp_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        gvEmp.PageIndex = e.NewPageIndex;

        FillDataListGrid();
        AllPageCode();
    }




    protected void lnkEditCommand(object sender, CommandEventArgs e)
    {
        editid.Value = e.CommandArgument.ToString();
        Session["empimgpath"] = null;
        DataTable dtEmp = objEmp.GetEmployeeMasterById(Session["CompId"].ToString(), editid.Value);

        if (dtEmp.Rows.Count > 0)
        {

            txtEmployeeCode.Text = dtEmp.Rows[0]["Emp_Code"].ToString();
            txtEmployeeName.Text = dtEmp.Rows[0]["Emp_Name"].ToString();
            txtEmployeeL.Text = dtEmp.Rows[0]["Emp_Name_L"].ToString();

            txtCivilId.Text = dtEmp.Rows[0]["Civil_Id"].ToString();
            txtEmailId.Text = dtEmp.Rows[0]["Email_Id"].ToString();
            txtPhoneNo.Text = dtEmp.Rows[0]["Phone_No"].ToString();
            txtDob.Text = Convert.ToDateTime(dtEmp.Rows[0]["DOB"].ToString()).ToString(objSys.SetDateFormat());
            txtDoj.Text = Convert.ToDateTime(dtEmp.Rows[0]["DOJ"].ToString()).ToString(objSys.SetDateFormat());
            if (Convert.ToDateTime(dtEmp.Rows[0]["Termination_Date"]) == new DateTime(1900, 1, 1))
            {
                txtTermDate.Text = "";
            }
            else
            {
                txtTermDate.Text = Convert.ToDateTime(dtEmp.Rows[0]["Termination_Date"].ToString()).ToString(objSys.SetDateFormat());
            } try
            {
                ddlDepartment.SelectedValue = dtEmp.Rows[0]["Department_Id"].ToString();
                ddlDesignation.SelectedValue = dtEmp.Rows[0]["Designation_Id"].ToString();
                ddlEmpType.SelectedValue = dtEmp.Rows[0]["Emp_Type"].ToString();
                ddlReligion.SelectedValue = dtEmp.Rows[0]["Religion_Id"].ToString();
                ddlQualification.SelectedValue = dtEmp.Rows[0]["Qualification_Id"].ToString();
                ddlNationality.SelectedValue = dtEmp.Rows[0]["Nationality_Id"].ToString();
                ddlGender.SelectedValue = dtEmp.Rows[0]["Gender"].ToString();
            }
            catch
            {

            }

            try
            {
                imgLogo.ImageUrl = "~/CompanyResource/" + Session["CompId"].ToString() + "/" + dtEmp.Rows[0]["Emp_Image"].ToString();
                Session["empimgpath"] = dtEmp.Rows[0]["Emp_Image"].ToString();
            }
            catch
            {

            }
            DataTable dtChild = objAddChild.GetAddressChildDataByAddTypeAndAddRefId("Employee", editid.Value);
            if (dtChild.Rows.Count > 0)
            {
                GvAddressName.DataSource = dtChild;
                GvAddressName.DataBind();
            }
            DataTable dtBank = objBankInfo.GetBankInfoByRefId(editid.Value, Session["CompId"].ToString());
            if (dtBank.Rows.Count > 0)
            {
                txtBankName.Text = dtBank.Rows[0]["Bank_Name"].ToString();
                ddlAcountType.SelectedValue = dtBank.Rows[0]["Account_Type"].ToString();
                txtAccountNo.Text = dtBank.Rows[0]["Account_No"].ToString();
            }
            btnNew_Click(null, null);
            btnNew.Text = Resources.Attendance.Edit;
        }

    }
    protected void ImgBtnEmpEdit(object sender, CommandEventArgs e)
    {
        editid.Value = e.CommandArgument.ToString();
        Session["empimgpath"] = null;
        DataTable dtEmp = objEmp.GetEmployeeMasterById(Session["CompId"].ToString(), editid.Value);

        if (dtEmp.Rows.Count > 0)
        {

            txtEmployeeCode.Text = dtEmp.Rows[0]["Emp_Code"].ToString();
            txtEmployeeName.Text = dtEmp.Rows[0]["Emp_Name"].ToString();
            txtEmployeeL.Text = dtEmp.Rows[0]["Emp_Name_L"].ToString();

            txtCivilId.Text = dtEmp.Rows[0]["Civil_Id"].ToString();
            txtEmailId.Text = dtEmp.Rows[0]["Email_Id"].ToString();
            txtPhoneNo.Text = dtEmp.Rows[0]["Phone_No"].ToString();
            txtDob.Text = Convert.ToDateTime(dtEmp.Rows[0]["DOB"].ToString()).ToString(objSys.SetDateFormat());
            txtDoj.Text = Convert.ToDateTime(dtEmp.Rows[0]["DOJ"].ToString()).ToString(objSys.SetDateFormat());
            if (Convert.ToDateTime(dtEmp.Rows[0]["Termination_Date"]) == new DateTime(1900, 1, 1))
            {
                txtTermDate.Text = "";
            }
            else
            {
                txtTermDate.Text = Convert.ToDateTime(dtEmp.Rows[0]["Termination_Date"].ToString()).ToString(objSys.SetDateFormat());
            } try
            {
                ddlDepartment.SelectedValue = dtEmp.Rows[0]["Department_Id"].ToString();
                ddlDesignation.SelectedValue = dtEmp.Rows[0]["Designation_Id"].ToString();
                ddlEmpType.SelectedValue = dtEmp.Rows[0]["Emp_Type"].ToString();
                ddlReligion.SelectedValue = dtEmp.Rows[0]["Religion_Id"].ToString();
                ddlQualification.SelectedValue = dtEmp.Rows[0]["Qualification_Id"].ToString();
                ddlNationality.SelectedValue = dtEmp.Rows[0]["Nationality_Id"].ToString();
                ddlGender.SelectedValue = dtEmp.Rows[0]["Gender"].ToString();
            }
            catch
            {

            }

            try
            {
                imgLogo.ImageUrl = "~/CompanyResource/" + Session["CompId"].ToString() + "/" + dtEmp.Rows[0]["Emp_Image"].ToString();
                Session["empimgpath"] = dtEmp.Rows[0]["Emp_Image"].ToString();
            }
            catch
            {

            }

            DataTable dtChild = objAddChild.GetAddressChildDataByAddTypeAndAddRefId("Employee", editid.Value);
            if (dtChild.Rows.Count > 0)
            {
                GvAddressName.DataSource = dtChild;
                GvAddressName.DataBind();
            }


            DataTable dtBank = objBankInfo.GetBankInfoByRefId(editid.Value, Session["CompId"].ToString());
            if (dtBank.Rows.Count > 0)
            {
                txtBankName.Text = dtBank.Rows[0]["Bank_Name"].ToString();
                ddlAcountType.SelectedValue = dtBank.Rows[0]["Account_Type"].ToString();
                txtAccountNo.Text = dtBank.Rows[0]["Account_No"].ToString();
            }

            btnNew_Click(null, null);
            btnNew.Text = Resources.Attendance.Edit;
        }


    }

    protected void btnDeleteLeave_Command(object sender, CommandEventArgs e)
    {

        int b = 0;
        b = objEmpleave.DeleteEmployeeLeaveByEmpIdandleaveTypeIdIsActive(e.CommandName.ToString(), e.CommandArgument.ToString());

        if (b != 0)
        {
            DisplayMessage("Record Deleted");
            DataTable dtEmpLeave = objEmpleave.GetEmployeeLeaveByEmpId(Session["CompId"].ToString(), e.CommandName.ToString());
            if (dtEmpLeave.Rows.Count > 0)
            {
                gvLeaveEmp.DataSource = dtEmpLeave;
                gvLeaveEmp.DataBind();


                foreach (GridViewRow gvr in gvLeaveEmp.Rows)
                {
                    string Schtype = ((DropDownList)gvr.FindControl("ddlSchType0")).SelectedValue;

                    if (Schtype == "Yearly")
                    {
                        ((CheckBox)gvr.FindControl("chkYearCarry0")).Enabled = true;


                    }
                    else
                    {
                        ((CheckBox)gvr.FindControl("chkYearCarry0")).Enabled = false;


                    }
                }

            }
            else
            {
                gvLeaveEmp.DataSource = null;
                gvLeaveEmp.DataBind();
                pnl1.Visible = false;
                pnl2.Visible = false;

            }
        }


    }

    protected void btnDelete_Command(object sender, CommandEventArgs e)
    {

        int b = 0;

        editid.Value = e.CommandArgument.ToString();


        b = objEmp.DeleteEmployeeMaster(Session["CompId"].ToString(), editid.Value, false.ToString(), Session["UserId"].ToString(), DateTime.Now.ToString());

        if (b != 0)
        {
            DisplayMessage("Record Deleted");
            FillDataListGrid();
            FillbinDataListGrid();

        }
        else
        {

            DisplayMessage("Record Not Deleted");

        }

    }


    protected void btnEdit_Command(object sender, CommandEventArgs e)
    {

        editid.Value = e.CommandArgument.ToString();
        Session["empimgpath"] = null;
        DataTable dtEmp = objEmp.GetEmployeeMasterById(Session["CompId"].ToString(), editid.Value);

        if (dtEmp.Rows.Count > 0)
        {

            txtEmployeeCode.Text = dtEmp.Rows[0]["Emp_Code"].ToString();
            txtEmployeeName.Text = dtEmp.Rows[0]["Emp_Name"].ToString();
            txtEmployeeL.Text = dtEmp.Rows[0]["Emp_Name_L"].ToString();

            txtCivilId.Text = dtEmp.Rows[0]["Civil_Id"].ToString();
            txtEmailId.Text = dtEmp.Rows[0]["Email_Id"].ToString();
            txtPhoneNo.Text = dtEmp.Rows[0]["Phone_No"].ToString();
            txtDob.Text = Convert.ToDateTime(dtEmp.Rows[0]["DOB"].ToString()).ToString(objSys.SetDateFormat());
            txtDoj.Text = Convert.ToDateTime(dtEmp.Rows[0]["DOJ"].ToString()).ToString(objSys.SetDateFormat());

            if (Convert.ToDateTime(dtEmp.Rows[0]["Termination_Date"]) == new DateTime(1900, 1, 1))
            {
                txtTermDate.Text = "";
            }
            else
            {
                txtTermDate.Text = Convert.ToDateTime(dtEmp.Rows[0]["Termination_Date"].ToString()).ToString(objSys.SetDateFormat());
            }

            try
            {
                ddlDepartment.SelectedValue = dtEmp.Rows[0]["Department_Id"].ToString();
                ddlDesignation.SelectedValue = dtEmp.Rows[0]["Designation_Id"].ToString();

                ddlReligion.SelectedValue = dtEmp.Rows[0]["Religion_Id"].ToString();
                ddlQualification.SelectedValue = dtEmp.Rows[0]["Qualification_Id"].ToString();
                ddlNationality.SelectedValue = dtEmp.Rows[0]["Nationality_Id"].ToString();
                ddlGender.SelectedValue = dtEmp.Rows[0]["Gender"].ToString();
            }
            catch
            {

            }

            try
            {
                imgLogo.ImageUrl = "~/CompanyResource/" + Session["CompId"].ToString() + "/" + dtEmp.Rows[0]["Emp_Image"].ToString();
                Session["empimgpath"] = dtEmp.Rows[0]["Emp_Image"].ToString();
            }
            catch
            {

            }
            try
            {
                ddlEmpType.SelectedValue = dtEmp.Rows[0]["Emp_Type"].ToString();
            }
            catch
            {
                ddlEmpType.SelectedValue = "On Role";
            }


            DataTable dtChild = objAddChild.GetAddressChildDataByAddTypeAndAddRefId("Employee", editid.Value);
            if (dtChild.Rows.Count > 0)
            {
                GvAddressName.DataSource = dtChild;
                GvAddressName.DataBind();
            }

            DataTable dtBank = objBankInfo.GetBankInfoByRefId(editid.Value, Session["CompId"].ToString());
            if (dtBank.Rows.Count > 0)
            {
                txtBankName.Text = dtBank.Rows[0]["Bank_Name"].ToString();
                ddlAcountType.SelectedValue = dtBank.Rows[0]["Account_Type"].ToString();
                txtAccountNo.Text = dtBank.Rows[0]["Account_No"].ToString();
            }

            btnNew_Click(null, null);
            btnNew.Text = Resources.Attendance.Edit;
        }



    }


    protected void btnList_Click(object sender, EventArgs e)
    {
        pnlUpload.BackColor = System.Drawing.ColorTranslator.FromHtml("#90bde9");
        pnlPanelty.BackColor = System.Drawing.ColorTranslator.FromHtml("#90bde9");
        PnlEmpPenalty.Visible = false;

        pnlEmpUpload.Visible = false;
        pnlOTPartial.BackColor = System.Drawing.ColorTranslator.FromHtml("#90bde9");
        pnlOTPL.Visible = false;
        pnlMenuList.BackColor = System.Drawing.ColorTranslator.FromHtml("#ccddee");
        pnlNotice.BackColor = System.Drawing.ColorTranslator.FromHtml("#90bde9");
        PnlEmpNotification.Visible = false;
        pnlMenuNew.BackColor = System.Drawing.ColorTranslator.FromHtml("#90bde9");
        pnlMenuBin.BackColor = System.Drawing.ColorTranslator.FromHtml("#90bde9");
        pnlLeave.BackColor = System.Drawing.ColorTranslator.FromHtml("#90bde9");

        pnlSalary.BackColor = System.Drawing.ColorTranslator.FromHtml("#90bde9");


        gvEmpSalary.Visible = false;
        PnlEmpSalary.Visible = false;
        PnlEmployeeLeave.Visible = false;
        pnlList.Visible = true;
        pnlBin.Visible = false;
        pnlNewEdit.Visible = false;
        gvEmpLeave.Visible = false;
        dtlistEmp.Visible = true;
        gvEmp.Visible = false;
        lnkPrev.Visible = false;
        lnkNext.Visible = false;
        lnkFirst.Visible = false;
        lnkLast.Visible = false;
        ViewState["CurrIndex"] = 0;
        ViewState["SubSize"] = 9;

        FillbinDataListGrid();
        FillDataListGrid();

        imbBtnGrid.Visible = true;


        imgBtnDatalist.Visible = false;

        lblSelectedRecord.Text = "";

    }
    protected void btnNew_Click(object sender, EventArgs e)
    {
        pnlUpload.BackColor = System.Drawing.ColorTranslator.FromHtml("#90bde9");

        pnlPanelty.BackColor = System.Drawing.ColorTranslator.FromHtml("#90bde9");
        PnlEmpPenalty.Visible = false;
        pnlEmpUpload.Visible = false;
        pnlOTPartial.BackColor = System.Drawing.ColorTranslator.FromHtml("#90bde9");
        pnlOTPL.Visible = false;
        txtEmployeeCode.Focus();
        pnlMenuNew.BackColor = System.Drawing.ColorTranslator.FromHtml("#ccddee");
        pnlLeave.BackColor = System.Drawing.ColorTranslator.FromHtml("#90bde9");
        PnlEmployeeLeave.Visible = false;
        pnlMenuList.BackColor = System.Drawing.ColorTranslator.FromHtml("#90bde9");
        pnlMenuBin.BackColor = System.Drawing.ColorTranslator.FromHtml("#90bde9");
        pnlNotice.BackColor = System.Drawing.ColorTranslator.FromHtml("#90bde9");
        pnlSalary.BackColor = System.Drawing.ColorTranslator.FromHtml("#90bde9");


        gvEmpSalary.Visible = false;
        PnlEmpSalary.Visible = false;
        PnlEmpNotification.Visible = false;
        pnlNewEdit.Visible = true;
        pnlBin.Visible = false;
        pnlList.Visible = false;
        lblSelectedRecord.Text = "";
    }
    protected void btnBin_Click(object sender, EventArgs e)
    {
        pnlPanelty.BackColor = System.Drawing.ColorTranslator.FromHtml("#90bde9");
        PnlEmpPenalty.Visible = false;
        pnlUpload.BackColor = System.Drawing.ColorTranslator.FromHtml("#90bde9");


        pnlEmpUpload.Visible = false;
        pnlOTPartial.BackColor = System.Drawing.ColorTranslator.FromHtml("#90bde9");
        pnlOTPL.Visible = false;
        gvEmpLeave.Visible = false;
        pnlMenuBin.BackColor = System.Drawing.ColorTranslator.FromHtml("#ccddee");
        pnlLeave.BackColor = System.Drawing.ColorTranslator.FromHtml("#90bde9");
        PnlEmployeeLeave.Visible = false;
        pnlMenuNew.BackColor = System.Drawing.ColorTranslator.FromHtml("#90bde9");
        pnlMenuList.BackColor = System.Drawing.ColorTranslator.FromHtml("#90bde9");
        pnlNotice.BackColor = System.Drawing.ColorTranslator.FromHtml("#90bde9");
        PnlEmpNotification.Visible = false;
        pnlSalary.BackColor = System.Drawing.ColorTranslator.FromHtml("#90bde9");


        gvEmpSalary.Visible = false;
        PnlEmpSalary.Visible = false;
        pnlNewEdit.Visible = false;
        pnlBin.Visible = true;
        pnlList.Visible = false;
        ImgbtnSelectAll.Visible = false;
        imgBtnRestore.Visible = false;
        dtlistbinEmp.Visible = true;
        gvBinEmp.Visible = false;

        ViewState["CurrIndexbin"] = 0;
        ViewState["SubSizebin"] = 9;
        lnkbinFirst.Visible = false;
        lnkbinPrev.Visible = false;
        lnkbinNext.Visible = false;
        lnkbinLast.Visible = false;



        FillbinDataListGrid();
        imgBtnbinGrid.Visible = true;

        lblSelectedRecord.Text = "";
        imgBtnbinDatalist.Visible = false;
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
        gridEmpLeave.DataSource = null;
        gridEmpLeave.DataBind();
        chkYearCarry.Visible = false;
    }

    protected void btnLeave_Click(object sender, EventArgs e)
    {
        pnlPanelty.BackColor = System.Drawing.ColorTranslator.FromHtml("#90bde9");
        PnlEmpPenalty.Visible = false;
        pnlMenuBin.BackColor = System.Drawing.ColorTranslator.FromHtml("#90bde9");
        pnlSalary.BackColor = System.Drawing.ColorTranslator.FromHtml("#90bde9");
        pnlOTPartial.BackColor = System.Drawing.ColorTranslator.FromHtml("#90bde9");
        pnlOTPL.Visible = false;

        gvEmpSalary.Visible = false;
        PnlEmpSalary.Visible = false;
        pnlMenuNew.BackColor = System.Drawing.ColorTranslator.FromHtml("#90bde9");
        pnlMenuList.BackColor = System.Drawing.ColorTranslator.FromHtml("#90bde9");
        pnlLeave.BackColor = System.Drawing.ColorTranslator.FromHtml("#ccddee");
        pnlNotice.BackColor = System.Drawing.ColorTranslator.FromHtml("#90bde9");
        PnlEmpNotification.Visible = false;
        pnlNewEdit.Visible = false;
        pnlBin.Visible = false;
        pnlList.Visible = false;
        chkYearCarry.Visible = false;
        PnlEmployeeLeave.Visible = true;
        ImgbtnSelectAll.Visible = false;
        imgBtnRestore.Visible = false;
        dtlistbinEmp.Visible = false;
        gvBinEmp.Visible = false;

        ViewState["CurrIndexbin"] = 0;
        ViewState["SubSizebin"] = 9;
        lnkbinFirst.Visible = false;
        lnkbinPrev.Visible = false;
        lnkbinNext.Visible = false;
        lnkbinLast.Visible = false;

        lnkFirst.Visible = false;
        lnkLast.Visible = false;
        lnkNext.Visible = false;
        lnkPrev.Visible = false;
        chkYearCarry.Visible = false;

        imgBtnbinGrid.Visible = false;


        imgBtnbinDatalist.Visible = false;

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
        gridEmpLeave.DataSource = null;
        gridEmpLeave.DataBind();

        rbtnEmp.Checked = true;
        rbtnGroup.Checked = false;
        EmpGroup_CheckedChanged(null, null);
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
        gridEmpLeave.DataSource = null;
        gridEmpLeave.DataBind();
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
        gridEmpLeave.DataSource = null;
        gridEmpLeave.DataBind();

    }




    protected void ddlScheduleGrid_SelectedIndexChanged(object sender, EventArgs e)
    {
        foreach (GridViewRow gvr in gvLeaveEmp.Rows)
        {
            string Schtype = ((DropDownList)gvr.FindControl("ddlSchType0")).SelectedValue;

            if (Schtype == "Yearly")
            {
                ((CheckBox)gvr.FindControl("chkYearCarry0")).Enabled = true;


            }
            else
            {
                ((CheckBox)gvr.FindControl("chkYearCarry0")).Checked = false;
                ((CheckBox)gvr.FindControl("chkYearCarry0")).Enabled = false;


            }
        }


    }
    protected void btnEditLeave_Command(object sender, CommandEventArgs e)
    {
        string empid = e.CommandArgument.ToString();
        string empname = GetEmployeeName(empid);


        lblEmpNameLeave.Text = empname;
        lblEmpCodeLeave.Text = GetEmployeeCode(empid);
        DataTable dtEmpLeave = objEmpleave.GetEmployeeLeaveByEmpId(Session["CompId"].ToString(), empid);

        if (dtEmpLeave.Rows.Count > 0)
        {
            gvLeaveEmp.DataSource = dtEmpLeave;
            gvLeaveEmp.DataBind();


            foreach (GridViewRow gvr in gvLeaveEmp.Rows)
            {
                string Schtype = ((DropDownList)gvr.FindControl("ddlSchType0")).SelectedValue;

                if (Schtype == "Yearly")
                {
                    ((CheckBox)gvr.FindControl("chkYearCarry0")).Enabled = true;


                }
                else
                {
                    ((CheckBox)gvr.FindControl("chkYearCarry0")).Enabled = false;


                }
            }



            pnl1.Visible = true;
            pnl2.Visible = true;
        }
        else
        {
            DisplayMessage("No leave assign to this employee");
            return;
        }

    }
    protected void btnUpdateLeave_Click(object sender, EventArgs e)
    {
        int b = 0;

        foreach (GridViewRow gvr in gvLeaveEmp.Rows)
        {
            string NoOfLeave = ((TextBox)gvr.FindControl("txtNoOfLeave0")).Text;
            string PaidLeave = ((TextBox)gvr.FindControl("txtPAidLEave1")).Text;
            string PerSalary = ((TextBox)gvr.FindControl("TextBox1")).Text;
            string Schtype = ((DropDownList)gvr.FindControl("ddlSchType0")).SelectedValue;

            bool isYearcarry = ((CheckBox)gvr.FindControl("chkYearCarry0")).Checked;

            string leavetypeid = ((HiddenField)gvr.FindControl("hdnLeaveTypeId")).Value;
            string TransNo = ((HiddenField)gvr.FindControl("hdnTranNo")).Value;
            string empid = ((HiddenField)gvr.FindControl("hdnEmpId1")).Value;

            if (((TextBox)gvr.FindControl("txtNoOfLeave0")).Text == "")
            {
                DisplayMessage("Enter value");
                ((TextBox)gvr.FindControl("txtNoOfLeave0")).Focus();
                return;
            }
            if (((TextBox)gvr.FindControl("txtPAidLEave1")).Text == "")
            {
                DisplayMessage("Enter value");
                ((TextBox)gvr.FindControl("txtPAidLEave1")).Focus();
                return;
            }
            if (float.Parse(PaidLeave) > float.Parse(NoOfLeave))
            {
                DisplayMessage("No of paid leave cannot be greater than total leave");
                ((TextBox)gvr.FindControl("txtPAidLEave1")).Text = "0";
                ((TextBox)gvr.FindControl("txtPAidLEave1")).Focus();

                return;


            }
            string PrevSchType = string.Empty;
            string PrevAssignLeave = string.Empty;
            DataTable dtEmpleave = objEmpleave.GetEmployeeLeaveByTransId(Session["CompId"].ToString(), TransNo);
            if (dtEmpleave.Rows.Count > 0)
            {
                PrevSchType = dtEmpleave.Rows[0]["Shedule_Type"].ToString();
                PrevAssignLeave = dtEmpleave.Rows[0]["Total_Leave"].ToString();
            }


            if (Schtype == "Monthly")
            {



                b = objEmpleave.UpdateEmployeeLeaveByTransNo(TransNo, Session["CompId"].ToString(), empid, leavetypeid, NoOfLeave, PaidLeave, PerSalary, Schtype, false.ToString(), false.ToString(), "", "", "", "", "", true.ToString(), DateTime.Now.ToString(), true.ToString(), Session["UserId"].ToString(), DateTime.Now.ToString());



                SaveLeave("Yes", leavetypeid, empid, Schtype, NoOfLeave, "False", PrevSchType, PrevAssignLeave, TransNo);

            }
            else if (Schtype == "Yearly")
            {



                b = objEmpleave.UpdateEmployeeLeaveByTransNo(TransNo, Session["CompId"].ToString(), empid, leavetypeid, NoOfLeave, PaidLeave, PerSalary, Schtype, true.ToString(), isYearcarry.ToString(), "", "", "", "", "", true.ToString(), DateTime.Now.ToString(), true.ToString(), Session["UserId"].ToString(), DateTime.Now.ToString());

                SaveLeave("Yes", leavetypeid, empid, Schtype, NoOfLeave, "False", PrevSchType, PrevAssignLeave, TransNo);

            }


        }


        if (b != 0)
        {
            DisplayMessage("Record Updated");

        }
        else
        {
            DisplayMessage("Record Not Saved");
        }

    }

    protected void btnCancelPopSal_Click(object sender, EventArgs e)
    {


        pnlSal1.Visible = false;
        pnlSal2.Visible = false;

    }
    protected void btnCancelPopPenalty_Click(object sender, EventArgs e)
    {


        pnlPen1.Visible = false;
        pnlPen2.Visible = false;

    }
    protected void btnCancelPopOT_Click(object sender, EventArgs e)
    {


        pnlOT1.Visible = false;
        pnlOT2.Visible = false;

    }

    protected void btnCancelPopLeave_Click(object sender, EventArgs e)
    {
        pnl1.Visible = false;
        pnl2.Visible = false;
        gvLeaveEmp.DataSource = null;
        gvLeaveEmp.DataBind();
    }

    protected void btnCancelPopLeave_Click1(object sender, EventArgs e)
    {
        pnlNotice1.Visible = false;
        pnlNotice2.Visible = false;
        chkSMSDocExp.Checked = false;
        chkSMSAbsent.Checked = false;
        chkSMSLate.Checked = false;
        chkSMSEarly.Checked = false;
        ChkSmsLeave.Checked = false;
        ChkSMSPartial.Checked = false;
        chkSMSNoClock.Checked = false;
        ChkRptAbsent.Checked = false;
        chkRptLate.Checked = false;
        chkRptEarly.Checked = false;
        ChkRptInOut.Checked = false;
        ChkRptSalary.Checked = false;
        ChkRptOvertime.Checked = false;
        ChkRptLog.Checked = false;
        chkRptDoc.Checked = false;
        chkRptViolation.Checked = false;


        chkSMSDocExp1.Checked = false;
        chkSMSAbsent1.Checked = false;
        chkSMSLate1.Checked = false;
        chkSMSEarly1.Checked = false;
        ChkSmsLeave1.Checked = false;
        ChkSMSPartial1.Checked = false;
        chkSMSNoClock1.Checked = false;
        ChkRptAbsent1.Checked = false;
        chkRptLate1.Checked = false;
        chkRptEarly1.Checked = false;
        ChkRptInOut1.Checked = false;
        ChkRptSalary1.Checked = false;
        ChkRptOvertime1.Checked = false;
        ChkRptLog1.Checked = false;
        chkRptDoc1.Checked = false;
        chkRptViolation1.Checked = false;
    }
    protected void btnCancelLeave_Click(object sender, EventArgs e)
    {
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

        }
        ddlLeaveType.SelectedIndex = 0;
        ddlSchType.SelectedIndex = 0;
        txtPaidLeave.Text = "";
        txtTotalLeave.Text = "";
        txtPerSal.Text = "";
        gvLeaveEmp.DataSource = null;
        gvLeaveEmp.DataBind();
        chkMonthCarry.Checked = false;
        chkYearCarry.Checked = false;
        btnList_Click(null, null);
        Session["dtLeave"] = null;
        gridEmpLeave.DataSource = null;
        gridEmpLeave.DataBind();
        chkYearCarry.Visible = false;

        DataTable dtGroup = objEmpGroup.GetEmployeeGroup_Master(Session["CompId"].ToString());
        dtGroup = new DataView(dtGroup, "Brand_Id='" + Session["BrandId"].ToString() + "'  and Location_Id='" + Session["LocId"].ToString() + "'", "", DataViewRowState.CurrentRows).ToTable();
        if (dtGroup.Rows.Count > 0)
        {
            lbxGroup.DataSource = dtGroup;
            lbxGroup.DataTextField = "Group_Name";
            lbxGroup.DataValueField = "Group_Id";

            lbxGroup.DataBind();

        }

        gvEmployee.DataSource = null;
        gvEmployee.DataBind();
    }


    public string GetLeaveTypeName(object leavetypeid)
    {
        string leavetypename = string.Empty;
        DataTable dt = objLeaveType.GetLeaveMasterById(Session["CompId"].ToString(), leavetypeid.ToString());

        if (dt.Rows.Count > 0)
        {
            leavetypename = dt.Rows[0]["Leave_Name"].ToString();

        }
        return leavetypename;
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
            if (ddlLeaveType.SelectedIndex == 0)
            {

                DisplayMessage("Select Leave Type");
                return;
            }

            if (txtTotalLeave.Text == "")
            {
                DisplayMessage("Enter Total Leave");
                return;
            }
            if (txtPaidLeave.Text == "")
            {

                DisplayMessage("Enter Paid Leave");
                return;


            }
            if (txtPerSal.Text == "")
            {

                DisplayMessage("Enter Percentage of Salary");
                return;


            }

            if (ddlSchType.SelectedIndex == 0)
            {

                DisplayMessage("Select Schedule Type");
                return;
            }


            int totleave = int.Parse(txtTotalLeave.Text);
            int paidleave = int.Parse(txtPaidLeave.Text);

            if (paidleave > totleave)
            {

                txtPaidLeave.Text = "0";

                txtPaidLeave.Focus();


                DisplayMessage("Paid leave cannot be greater then total leave");

                return;

            }


            foreach (string str in EmpIds.Split(','))
            {
                if (str != "")
                {
                    DataTable dtEmpLeave = objEmpleave.GetEmployeeLeaveByEmpId(Session["CompId"].ToString(), str);
                    dtEmpLeave = new DataView(dtEmpLeave, "LeaveType_Id='" + ddlLeaveType.SelectedValue + "'", "", DataViewRowState.CurrentRows).ToTable();
                    if (dtEmpLeave.Rows.Count > 0)
                    {



                    }
                    else
                    {
                        b = objEmpleave.InsertEmployeeLeave(Session["CompId"].ToString(), str, ddlLeaveType.SelectedValue, txtTotalLeave.Text, txtPaidLeave.Text, txtPerSal.Text, ddlSchType.SelectedValue, true.ToString(), chkYearCarry.Checked.ToString(), "", "", "", "", "", true.ToString(), DateTime.Now.ToString(), true.ToString(), Session["UserId"].ToString(), DateTime.Now.ToString(), Session["UserId"].ToString(), DateTime.Now.ToString());


                        SaveLeave("No", ddlLeaveType.SelectedValue, str, ddlSchType.SelectedValue, txtTotalLeave.Text, chkYearCarry.Checked.ToString(), "", "", "");

                        TransNo = b.ToString();
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
            if (ddlLeaveType.SelectedIndex == 0)
            {

                DisplayMessage("Select Leave Type");
                return;
            }

            if (txtTotalLeave.Text == "")
            {
                DisplayMessage("Enter Total Leave");
                return;
            }
            if (txtPaidLeave.Text == "")
            {

                DisplayMessage("Enter Paid Leave");
                return;


            }
            if (txtPerSal.Text == "")
            {

                DisplayMessage("Enter Percentage of Salary");
                return;


            }

            if (ddlSchType.SelectedIndex == 0)
            {

                DisplayMessage("Select Schedule Type");
                return;
            }


            int totleave = int.Parse(txtTotalLeave.Text);
            int paidleave = int.Parse(txtPaidLeave.Text);

            if (paidleave > totleave)
            {

                txtPaidLeave.Text = "0";

                txtPaidLeave.Focus();


                DisplayMessage("Paid leave cannot be greater then total leave");

                return;

            }

            foreach (string str in lblSelectRecd.Text.Split(','))
            {
                if (str != "")
                {
                    DataTable dtEmpLeave = objEmpleave.GetEmployeeLeaveByEmpId(Session["CompId"].ToString(), str);
                    dtEmpLeave = new DataView(dtEmpLeave, "LeaveType_Id='" + ddlLeaveType.SelectedValue + "'", "", DataViewRowState.CurrentRows).ToTable();
                    if (dtEmpLeave.Rows.Count > 0)
                    {



                    }
                    else
                    {
                        b = objEmpleave.InsertEmployeeLeave(Session["CompId"].ToString(), str, ddlLeaveType.SelectedValue, txtTotalLeave.Text, txtPaidLeave.Text, txtPerSal.Text, ddlSchType.SelectedValue, true.ToString(), chkYearCarry.Checked.ToString(), "", "", "", "", "", true.ToString(), DateTime.Now.ToString(), true.ToString(), Session["UserId"].ToString(), DateTime.Now.ToString(), Session["UserId"].ToString(), DateTime.Now.ToString());


                        SaveLeave("No", ddlLeaveType.SelectedValue, str, ddlSchType.SelectedValue, txtTotalLeave.Text, chkYearCarry.Checked.ToString(), "", "", "");

                        TransNo = b.ToString();
                    }
                }
            }


        }
        if (b != 0)
        {


            DisplayMessage("Record Saved");
            DataTable dtEmpLeave = new DataTable();
            DataTable dt = objEmpleave.GetEmployeeLeaveByTransId(Session["CompId"].ToString(), TransNo);

            if (Session["dtLeave"] != null)
            {
                if (dt.Rows.Count > 0)
                {


                    ((DataTable)Session["dtLeave"]).Merge(dt);
                    dtEmpLeave = (DataTable)Session["dtLeave"];

                }
                else
                {
                    dtEmpLeave = (DataTable)Session["dtLeave"];
                }
            }
            else
            {
                if (dt.Rows.Count > 0)
                {
                    Session["dtLeave"] = dt;
                    dtEmpLeave = (DataTable)Session["dtLeave"];
                }

            }
            if (dtEmpLeave.Rows.Count > 0)
            {
                gridEmpLeave.DataSource = dtEmpLeave;
                gridEmpLeave.DataBind();

            }
            chkYearCarry.Visible = false;
            ddlLeaveType.SelectedIndex = 0;
            ddlSchType.SelectedIndex = 0;
            txtPaidLeave.Text = "";
            txtTotalLeave.Text = "";
            txtPerSal.Text = "";
            chkMonthCarry.Checked = false;
            chkYearCarry.Checked = false;
        }
        else
        {
            DisplayMessage("Record Not Saved");
        }


    }



    public void SaveLeave(string Edit, string LeaveTypeId, string EmpId, string SchType, string AssignLeave, string IsYearCarry, string PrevSchduleType, string PrevAssignLeave, string TransNo)
    {
        DateTime JoiningDate = new DateTime();
        DataTable dtEmp = objEmp.GetEmployeeMasterById(Session["CompId"].ToString(), EmpId);
        if (dtEmp.Rows.Count > 0)
        {
            JoiningDate = Convert.ToDateTime(dtEmp.Rows[0]["DOJ"].ToString());
        }
        else
        {
            return;
        }


        if (JoiningDate > DateTime.Now)
        {
            return;

        }


        DataTable dt = objAppParam.GetApplicationParameterByParamName("FinancialYearStartMonth", Session["CompId"].ToString());
        int FinancialYearMonth = 0;
        int leavepermonth = 0;
        int leavepermonthPrev = 0;
        if (dt.Rows.Count > 0)
        {
            FinancialYearMonth = int.Parse(dt.Rows[0]["Param_Value"].ToString());

        }
        double TotalDays = 0;
        DateTime FinancialYearStartDate = new DateTime();
        DateTime FinancialYearEndDate = new DateTime();
        if (JoiningDate.Month > FinancialYearMonth)
        {
            FinancialYearStartDate = new DateTime(DateTime.Now.Year, FinancialYearMonth, 1);
            FinancialYearEndDate = FinancialYearStartDate.AddYears(1).AddDays(-1);

            TotalDays = FinancialYearEndDate.Subtract(FinancialYearStartDate).Days;


            double Months1 = TotalDays / 30;


            leavepermonth = int.Parse(System.Math.Round(double.Parse(AssignLeave) / Months1).ToString());
            if (Edit == "Yes")
            {
                leavepermonthPrev = int.Parse(System.Math.Round(double.Parse(PrevAssignLeave) / Months1).ToString());


            }
        }
        else
        {

            FinancialYearStartDate = new DateTime(DateTime.Now.Year, FinancialYearMonth, 1);
            FinancialYearEndDate = FinancialYearStartDate.AddYears(1).AddDays(-1);
            TotalDays = FinancialYearEndDate.Subtract(FinancialYearStartDate).Days;



            double Months1 = TotalDays / 30;


            leavepermonth = int.Parse(System.Math.Round(double.Parse(AssignLeave) / Months1).ToString());
            if (Edit == "Yes")
            {
                leavepermonthPrev = int.Parse(System.Math.Round(double.Parse(PrevAssignLeave) / Months1).ToString());


            }
        }


        if (Edit == "No")
        {

            if (SchType == "Monthly")
            {
                if (JoiningDate.Year < FinancialYearStartDate.Year)
                {
                    JoiningDate = new DateTime(DateTime.Now.Year, DateTime.Now.Month, DateTime.Now.Day);

                    while (JoiningDate <= FinancialYearEndDate)
                    {
                        int leave = 0;
                        if (JoiningDate.Day != 1)
                        {

                            double JoinDay = 30 - JoiningDate.Day;

                            double Day = (Convert.ToDouble(JoinDay) * Convert.ToDouble(AssignLeave)) / 30;



                            leave = int.Parse(System.Math.Round(Day).ToString());
                        }
                        else
                        {
                            leave = int.Parse(Convert.ToDouble(AssignLeave).ToString());

                        }


                        objEmpleave.InsertEmployeeLeaveTrans(Session["CompId"].ToString(), EmpId, LeaveTypeId, JoiningDate.Year.ToString(), JoiningDate.Month.ToString(), "0", leave.ToString(), leave.ToString(), "0", leave.ToString(), "0", "", "", "", "", "", true.ToString(), DateTime.Now.ToString(), true.ToString(), Session["UserId"].ToString(), DateTime.Now.ToString(), Session["UserId"].ToString(), DateTime.Now.ToString());
                        JoiningDate = JoiningDate.AddMonths(1);
                        JoiningDate = new DateTime(JoiningDate.Year, JoiningDate.Month, 1);

                    }




                }
                else if (JoiningDate.Year == FinancialYearStartDate.Year)
                {

                    if (JoiningDate.Month == DateTime.Now.Month)
                    {

                    }

                    else
                    {

                        JoiningDate = new DateTime(DateTime.Now.Year, DateTime.Now.Month, DateTime.Now.Day);
                    }


                    while (JoiningDate <= FinancialYearEndDate)
                    {
                        int leave = 0;
                        if (JoiningDate.Day != 1)
                        {

                            double JoinDay = 30 - JoiningDate.Day;

                            double Day = (Convert.ToDouble(JoinDay) * Convert.ToDouble(AssignLeave)) / 30;



                            leave = int.Parse(System.Math.Round(Day).ToString());
                        }
                        else
                        {
                            leave = int.Parse(Convert.ToDouble(AssignLeave).ToString());

                        }


                        objEmpleave.InsertEmployeeLeaveTrans(Session["CompId"].ToString(), EmpId, LeaveTypeId, JoiningDate.Year.ToString(), JoiningDate.Month.ToString(), "0", leave.ToString(), leave.ToString(), "0", leave.ToString(), "0", "", "", "", "", "", true.ToString(), DateTime.Now.ToString(), true.ToString(), Session["UserId"].ToString(), DateTime.Now.ToString(), Session["UserId"].ToString(), DateTime.Now.ToString());
                        JoiningDate = JoiningDate.AddMonths(1);
                        JoiningDate = new DateTime(JoiningDate.Year, JoiningDate.Month, 1);

                    }
                }
                else if (JoiningDate > FinancialYearEndDate)
                {


                }


            }
            else if (SchType == "Yearly")
            {
                if (JoiningDate.Year < FinancialYearStartDate.Year)
                {
                    JoiningDate = new DateTime(DateTime.Now.Year, DateTime.Now.Month, DateTime.Now.Day);


                    double leave = 0;

                    if (JoiningDate.Day != 1)
                    {

                        double JoinDay = 30 - JoiningDate.Day;

                        double Day = (Convert.ToDouble(JoinDay) * Convert.ToDouble(leavepermonth)) / 30;



                        leave = double.Parse(System.Math.Round(Day).ToString());
                    }
                    else
                    {
                        leave = double.Parse(Convert.ToDouble(leavepermonth).ToString());

                    }


                    int monthJ = 0;
                    int yearJ = 0;



                    if (JoiningDate.Month == 12)
                    {
                        monthJ = 1;
                        yearJ = JoiningDate.Year + 1;
                    }
                    else
                    {
                        monthJ = JoiningDate.Month + 1;
                        yearJ = JoiningDate.Year;
                    }

                    JoiningDate = new DateTime(yearJ, monthJ, 1);


                    double RemainDays = FinancialYearEndDate.Subtract(JoiningDate).Days;

                    double remainmonth = RemainDays / 30;

                    remainmonth = System.Math.Round(remainmonth);

                    int totalleaves = 0;

                    double Totalmonths = remainmonth * leavepermonth;

                    totalleaves = int.Parse(System.Math.Round(Totalmonths).ToString()) + int.Parse(System.Math.Round(leave).ToString());




                    objEmpleave.InsertEmployeeLeaveTrans(Session["CompId"].ToString(), EmpId, LeaveTypeId, JoiningDate.Year.ToString(), "0", "0", totalleaves.ToString(), totalleaves.ToString(), "0", totalleaves.ToString(), "0", "", "", "", "", "", true.ToString(), DateTime.Now.ToString(), true.ToString(), Session["UserId"].ToString(), DateTime.Now.ToString(), Session["UserId"].ToString(), DateTime.Now.ToString());




                }
                else if (JoiningDate.Year == FinancialYearStartDate.Year)
                {
                    double leave = 0;

                    if (JoiningDate.Month == DateTime.Now.Month)
                    {


                    }

                    else
                    {
                        JoiningDate = new DateTime(DateTime.Now.Year, DateTime.Now.Month, DateTime.Now.Day);
                    }
                    if (JoiningDate.Day != 1)
                    {

                        double JoinDay = 30 - JoiningDate.Day;

                        double Day = (Convert.ToDouble(JoinDay) * Convert.ToDouble(leavepermonth)) / 30;



                        leave = double.Parse(System.Math.Round(Day).ToString());
                    }
                    else
                    {
                        leave = double.Parse(Convert.ToDouble(leavepermonth).ToString());

                    }


                    int monthJ = 0;
                    int yearJ = 0;



                    if (JoiningDate.Month == 12)
                    {
                        monthJ = 1;
                        yearJ = JoiningDate.Year + 1;
                    }
                    else
                    {
                        monthJ = JoiningDate.Month + 1;
                        yearJ = JoiningDate.Year;
                    }

                    JoiningDate = new DateTime(yearJ, monthJ, 1);


                    double RemainDays = FinancialYearEndDate.Subtract(JoiningDate).Days;

                    double remainmonth = RemainDays / 30;

                    remainmonth = System.Math.Round(remainmonth);

                    int totalleaves = 0;

                    double Totalmonths = remainmonth * leavepermonth;

                    totalleaves = int.Parse(System.Math.Round(Totalmonths).ToString()) + int.Parse(System.Math.Round(leave).ToString());




                    objEmpleave.InsertEmployeeLeaveTrans(Session["CompId"].ToString(), EmpId, LeaveTypeId, JoiningDate.Year.ToString(), "0", "0", totalleaves.ToString(), totalleaves.ToString(), "0", totalleaves.ToString(), "0", "", "", "", "", "", true.ToString(), DateTime.Now.ToString(), true.ToString(), Session["UserId"].ToString(), DateTime.Now.ToString(), Session["UserId"].ToString(), DateTime.Now.ToString());




                }
                else if (JoiningDate > FinancialYearEndDate)
                {


                }
            }

        }
        else
        {
            if (JoiningDate.Year < FinancialYearStartDate.Year)
            {

                if (SchType == "Yearly")
                {

                    if (PrevSchduleType == "Yearly")
                    {


                        JoiningDate = new DateTime(DateTime.Now.Year, DateTime.Now.Month, DateTime.Now.Day);


                        double leave = 0;

                        if (JoiningDate.Day != 1)
                        {

                            double JoinDay = 30 - JoiningDate.Day;

                            double Day = (Convert.ToDouble(JoinDay) * Convert.ToDouble(leavepermonth)) / 30;



                            leave = double.Parse(System.Math.Round(Day).ToString());
                        }
                        else
                        {
                            leave = double.Parse(Convert.ToDouble(leavepermonth).ToString());

                        }


                        int monthJ = 0;
                        int yearJ = 0;



                        if (JoiningDate.Month == 12)
                        {
                            monthJ = 1;
                            yearJ = JoiningDate.Year + 1;
                        }
                        else
                        {
                            monthJ = JoiningDate.Month + 1;
                            yearJ = JoiningDate.Year;
                        }

                        JoiningDate = new DateTime(yearJ, monthJ, 1);


                        double RemainDays = FinancialYearEndDate.Subtract(JoiningDate).Days;

                        double remainmonth = RemainDays / 30;

                        remainmonth = System.Math.Round(remainmonth);

                        int totalleaves = 0;

                        double Totalmonths = remainmonth * leavepermonth;

                        totalleaves = int.Parse(System.Math.Round(Totalmonths).ToString()) + int.Parse(System.Math.Round(leave).ToString());

                        objEmpleave.DeleteEmployeeLeaveTransByEmpIdandleaveTypeId(EmpId, LeaveTypeId, "0", JoiningDate.Year.ToString());



                        // objEmpleave.UpdateEmployeeLeaveTransaction(Session["CompId"].ToString(), EmpId, LeaveTypeId, JoiningDate.Year.ToString(), "0", "0", totalleaves.ToString(), totalleaves.ToString(), "0", totalleaves.ToString(),"0", Session["UserId"].ToString(), DateTime.Now.ToString());
                        objEmpleave.InsertEmployeeLeaveTrans(Session["CompId"].ToString(), EmpId, LeaveTypeId, JoiningDate.Year.ToString(), "0", "0", totalleaves.ToString(), totalleaves.ToString(), "0", totalleaves.ToString(), "0", "", "", "", "", "", true.ToString(), DateTime.Now.ToString(), true.ToString(), Session["UserId"].ToString(), DateTime.Now.ToString(), Session["UserId"].ToString(), DateTime.Now.ToString());



                    }
                    else if (PrevSchduleType == "Monthly")
                    {
                        //Monthly to yearly

                        JoiningDate = new DateTime(DateTime.Now.Year, DateTime.Now.Month, DateTime.Now.Day);
                        DateTime JoiningDate1 = JoiningDate;
                        int monthc = 0;
                        int yearc = 0;

                        if (DateTime.Now.Month == 12)
                        {
                            monthc = 1;
                            yearc = DateTime.Now.Year + 1;

                        }
                        else
                        {
                            monthc = DateTime.Now.Month + 1;

                            yearc = DateTime.Now.Year;
                        }
                        JoiningDate = new DateTime(yearc, monthc, 1);
                        int TotalLeaves = 0;
                        while (JoiningDate <= FinancialYearEndDate)
                        {
                            objEmpleave.DeleteEmployeeLeaveTransByEmpIdandleaveTypeId(EmpId, LeaveTypeId, JoiningDate.Month.ToString(), JoiningDate.Year.ToString());

                            TotalLeaves += leavepermonth;
                            JoiningDate = JoiningDate.AddMonths(1);
                        }

                        objEmpleave.InsertEmployeeLeaveTrans(Session["CompId"].ToString(), EmpId, LeaveTypeId, JoiningDate1.Year.ToString(), "0", "0", TotalLeaves.ToString(), TotalLeaves.ToString(), "0", TotalLeaves.ToString(), "0", "", "", "", "", "", true.ToString(), DateTime.Now.ToString(), true.ToString(), Session["UserId"].ToString(), DateTime.Now.ToString(), Session["UserId"].ToString(), DateTime.Now.ToString());



                    }
                }
                else if (SchType == "Monthly")
                {
                    if (PrevSchduleType == "Monthly")
                    {


                        JoiningDate = new DateTime(DateTime.Now.Year, DateTime.Now.Month, DateTime.Now.Day);







                        while (JoiningDate <= FinancialYearEndDate)
                        {
                            objEmpleave.DeleteEmployeeLeaveTransByEmpIdandleaveTypeId(EmpId, LeaveTypeId, JoiningDate.Month.ToString(), JoiningDate.Year.ToString());



                            int leave = 0;
                            if (JoiningDate.Day != 1)
                            {

                                double JoinDay = 30 - JoiningDate.Day;

                                double Day = (Convert.ToDouble(JoinDay) * Convert.ToDouble(AssignLeave)) / 30;



                                leave = int.Parse(System.Math.Round(Day).ToString());
                            }
                            else
                            {
                                leave = int.Parse(Convert.ToDouble(AssignLeave).ToString());

                            }


                            objEmpleave.InsertEmployeeLeaveTrans(Session["CompId"].ToString(), EmpId, LeaveTypeId, JoiningDate.Year.ToString(), JoiningDate.Month.ToString(), "0", leave.ToString(), leave.ToString(), "0", leave.ToString(), "0", "", "", "", "", "", true.ToString(), DateTime.Now.ToString(), true.ToString(), Session["UserId"].ToString(), DateTime.Now.ToString(), Session["UserId"].ToString(), DateTime.Now.ToString());
                            JoiningDate = JoiningDate.AddMonths(1);
                            JoiningDate = new DateTime(JoiningDate.Year, JoiningDate.Month, 1);

                        }





                    }

                    else if (PrevSchduleType == "Yearly")
                    {
                        // yearly to monthly

                        JoiningDate = new DateTime(DateTime.Now.Year, DateTime.Now.Month, DateTime.Now.Day);


                        double leave = 0;

                        if (JoiningDate.Day != 1)
                        {

                            double JoinDay = 30 - JoiningDate.Day;



                            double Day = (Convert.ToDouble(JoinDay) * Convert.ToDouble(leavepermonthPrev)) / 30;



                            leave = double.Parse(System.Math.Round(Day).ToString());
                        }
                        else
                        {
                            leave = double.Parse(Convert.ToDouble(leavepermonthPrev).ToString());

                        }



                        //here 12 aug 

                        objEmpleave.DeleteEmployeeLeaveTransByEmpIdandleaveTypeId(EmpId, LeaveTypeId, "0", JoiningDate.Year.ToString());


                        objEmpleave.InsertEmployeeLeaveTrans(Session["CompId"].ToString(), EmpId, LeaveTypeId, JoiningDate.Year.ToString(), "0", "0", leave.ToString(), leave.ToString(), "0", leave.ToString(), "0", "", "", "", "", "", true.ToString(), DateTime.Now.ToString(), true.ToString(), Session["UserId"].ToString(), DateTime.Now.ToString(), Session["UserId"].ToString(), DateTime.Now.ToString());
                        int monthJ = 0;
                        int yearJ = 0;



                        if (JoiningDate.Month == 12)
                        {
                            monthJ = 1;
                            yearJ = JoiningDate.Year + 1;
                        }
                        else
                        {
                            monthJ = JoiningDate.Month + 1;
                            yearJ = JoiningDate.Year;
                        }

                        JoiningDate = new DateTime(yearJ, monthJ, 1);




                        while (JoiningDate <= FinancialYearEndDate)
                        {

                            leave = double.Parse(Convert.ToDouble(AssignLeave).ToString());

                            objEmpleave.InsertEmployeeLeaveTrans(Session["CompId"].ToString(), EmpId, LeaveTypeId, JoiningDate.Year.ToString(), JoiningDate.Month.ToString(), "0", leave.ToString(), leave.ToString(), "0", leave.ToString(), "0", "", "", "", "", "", true.ToString(), DateTime.Now.ToString(), true.ToString(), Session["UserId"].ToString(), DateTime.Now.ToString(), Session["UserId"].ToString(), DateTime.Now.ToString());
                            JoiningDate = JoiningDate.AddMonths(1);


                        }



                    }

                }


            }
            else if (JoiningDate.Year == FinancialYearStartDate.Year)
            {
                if (SchType == "Yearly")
                {

                    if (PrevSchduleType == "Yearly")
                    {

                        if (JoiningDate.Month == DateTime.Now.Month)
                        {

                        }


                        else
                        {

                            JoiningDate = new DateTime(DateTime.Now.Year, DateTime.Now.Month, DateTime.Now.Day);
                        }

                        double leave = 0;

                        if (JoiningDate.Day != 1)
                        {

                            double JoinDay = 30 - JoiningDate.Day;

                            double Day = (Convert.ToDouble(JoinDay) * Convert.ToDouble(leavepermonth)) / 30;



                            leave = double.Parse(System.Math.Round(Day).ToString());
                        }
                        else
                        {
                            leave = double.Parse(Convert.ToDouble(leavepermonth).ToString());

                        }


                        int monthJ = 0;
                        int yearJ = 0;



                        if (JoiningDate.Month == 12)
                        {
                            monthJ = 1;
                            yearJ = JoiningDate.Year + 1;
                        }
                        else
                        {
                            monthJ = JoiningDate.Month + 1;
                            yearJ = JoiningDate.Year;
                        }

                        JoiningDate = new DateTime(yearJ, monthJ, 1);


                        double RemainDays = FinancialYearEndDate.Subtract(JoiningDate).Days;

                        double remainmonth = RemainDays / 30;

                        remainmonth = System.Math.Round(remainmonth);

                        int totalleaves = 0;

                        double Totalmonths = remainmonth * leavepermonth;

                        totalleaves = int.Parse(System.Math.Round(Totalmonths).ToString()) + int.Parse(System.Math.Round(leave).ToString());

                        objEmpleave.DeleteEmployeeLeaveTransByEmpIdandleaveTypeId(EmpId, LeaveTypeId, "0", JoiningDate.Year.ToString());


                        objEmpleave.InsertEmployeeLeaveTrans(Session["CompId"].ToString(), EmpId, LeaveTypeId, JoiningDate.Year.ToString(), "0", "0", totalleaves.ToString(), totalleaves.ToString(), "0", totalleaves.ToString(), "0", "", "", "", "", "", true.ToString(), DateTime.Now.ToString(), true.ToString(), Session["UserId"].ToString(), DateTime.Now.ToString(), Session["UserId"].ToString(), DateTime.Now.ToString());




                    }
                    else if (PrevSchduleType == "Monthly")
                    {

                        // Monthly to Yearly 14


                        //JoiningDate = new DateTime(DateTime.Now.Year, DateTime.Now.Month, DateTime.Now.Day);

                        DateTime JoininDate1 = JoiningDate;
                        if (JoiningDate.Month == DateTime.Now.Month)
                        {

                            while (JoiningDate <= FinancialYearEndDate)
                            {
                                objEmpleave.DeleteEmployeeLeaveTransByEmpIdandleaveTypeId(EmpId, LeaveTypeId, JoiningDate.Month.ToString(), JoiningDate.Year.ToString());
                                JoiningDate = JoiningDate.AddMonths(1);

                            }
                            double leave = 0;

                            JoiningDate = JoininDate1;


                            if (JoiningDate.Day != 1)
                            {

                                double JoinDay = 30 - JoiningDate.Day;

                                double Day = (Convert.ToDouble(JoinDay) * Convert.ToDouble(leavepermonth)) / 30;



                                leave = double.Parse(System.Math.Round(Day).ToString());
                            }
                            else
                            {
                                leave = double.Parse(Convert.ToDouble(leavepermonth).ToString());

                            }

                            int monthJ = 0;
                            int yearJ = 0;



                            if (JoiningDate.Month == 12)
                            {
                                monthJ = 1;
                                yearJ = JoiningDate.Year + 1;
                            }
                            else
                            {
                                monthJ = JoiningDate.Month + 1;
                                yearJ = JoiningDate.Year;
                            }

                            DateTime JoiningDate2 = new DateTime(yearJ, monthJ, 1);



                            double RemainDays = FinancialYearEndDate.Subtract(JoiningDate2).Days;

                            double remainmonth = RemainDays / 30;

                            remainmonth = System.Math.Round(remainmonth);

                            int totalleaves = 0;

                            double Totalmonths = remainmonth * leavepermonth;

                            totalleaves = int.Parse(System.Math.Round(Totalmonths).ToString()) + int.Parse(System.Math.Round(leave).ToString());




                            objEmpleave.InsertEmployeeLeaveTrans(Session["CompId"].ToString(), EmpId, LeaveTypeId, JoiningDate.Year.ToString(), "0", "0", totalleaves.ToString(), totalleaves.ToString(), "0", totalleaves.ToString(), "0", "", "", "", "", "", true.ToString(), DateTime.Now.ToString(), true.ToString(), Session["UserId"].ToString(), DateTime.Now.ToString(), Session["UserId"].ToString(), DateTime.Now.ToString());


                        }
                        else
                        {



                            JoiningDate = new DateTime(DateTime.Now.Year, DateTime.Now.Month, DateTime.Now.Day);

                            int monthJ = 0;
                            int yearJ = 0;



                            if (JoiningDate.Month == 12)
                            {
                                monthJ = 1;
                                yearJ = JoiningDate.Year + 1;
                            }
                            else
                            {
                                monthJ = JoiningDate.Month + 1;
                                yearJ = JoiningDate.Year;
                            }

                            JoiningDate = new DateTime(yearJ, monthJ, 1);


                            while (JoiningDate <= FinancialYearEndDate)
                            {
                                objEmpleave.DeleteEmployeeLeaveTransByEmpIdandleaveTypeId(EmpId, LeaveTypeId, JoiningDate.Month.ToString(), JoiningDate.Year.ToString());
                                JoiningDate = JoiningDate.AddMonths(1);
                            }




                            JoiningDate = new DateTime(yearJ, monthJ, 1);





                            double RemainDays = FinancialYearEndDate.Subtract(JoiningDate).Days;

                            double remainmonth = RemainDays / 30;

                            remainmonth = System.Math.Round(remainmonth);

                            int totalleaves = 0;

                            double Totalmonths = remainmonth * leavepermonth;

                            totalleaves = int.Parse(System.Math.Round(Totalmonths).ToString());

                            DataTable dtLeave = objEmpleave.GetEmployeeLeaveTransactionData(EmpId, LeaveTypeId, "0", JoiningDate.Year.ToString());



                            objEmpleave.DeleteEmployeeLeaveTransByEmpIdandleaveTypeId(EmpId, LeaveTypeId, "0", JoiningDate.Year.ToString());




                            if (dtLeave.Rows.Count > 0)
                            {
                                totalleaves += int.Parse(dtLeave.Rows[0]["Assign_Days"].ToString());
                            }



                            objEmpleave.InsertEmployeeLeaveTrans(Session["CompId"].ToString(), EmpId, LeaveTypeId, JoiningDate.Year.ToString(), "0", "0", totalleaves.ToString(), totalleaves.ToString(), "0", totalleaves.ToString(), "0", "", "", "", "", "", true.ToString(), DateTime.Now.ToString(), true.ToString(), Session["UserId"].ToString(), DateTime.Now.ToString(), Session["UserId"].ToString(), DateTime.Now.ToString());










                        }
                    }
                }
                else if (SchType == "Monthly")
                {


                    if (PrevSchduleType == "Monthly")
                    {

                        if (JoiningDate.Month == DateTime.Now.Month)
                        {

                        }


                        else
                        {

                            JoiningDate = new DateTime(DateTime.Now.Year, DateTime.Now.Month, DateTime.Now.Day);
                        }




                        while (JoiningDate <= FinancialYearEndDate)
                        {
                            objEmpleave.DeleteEmployeeLeaveTransByEmpIdandleaveTypeId(EmpId, LeaveTypeId, JoiningDate.Month.ToString(), JoiningDate.Year.ToString());



                            int leave = 0;
                            if (JoiningDate.Day != 1)
                            {

                                double JoinDay = 30 - JoiningDate.Day;

                                double Day = (Convert.ToDouble(JoinDay) * Convert.ToDouble(AssignLeave)) / 30;



                                leave = int.Parse(System.Math.Round(Day).ToString());
                            }
                            else
                            {
                                leave = int.Parse(Convert.ToDouble(AssignLeave).ToString());

                            }


                            objEmpleave.InsertEmployeeLeaveTrans(Session["CompId"].ToString(), EmpId, LeaveTypeId, JoiningDate.Year.ToString(), JoiningDate.Month.ToString(), "0", leave.ToString(), leave.ToString(), "0", leave.ToString(), "0", "", "", "", "", "", true.ToString(), DateTime.Now.ToString(), true.ToString(), Session["UserId"].ToString(), DateTime.Now.ToString(), Session["UserId"].ToString(), DateTime.Now.ToString());
                            JoiningDate = JoiningDate.AddMonths(1);
                            JoiningDate = new DateTime(JoiningDate.Year, JoiningDate.Month, 1);

                        }





                    }
                    else if (PrevSchduleType == "Yearly")
                    {
                        //yearly to monthly 17
                        if (JoiningDate.Month == DateTime.Now.Month)
                        {
                            // current month to end will delete





                            while (JoiningDate <= FinancialYearEndDate)
                            {
                                objEmpleave.DeleteEmployeeLeaveTransByEmpIdandleaveTypeId(EmpId, LeaveTypeId, "0", JoiningDate.Year.ToString());



                                int leave = 0;
                                if (JoiningDate.Day != 1)
                                {

                                    double JoinDay = 30 - JoiningDate.Day;

                                    double Day = (Convert.ToDouble(JoinDay) * Convert.ToDouble(AssignLeave)) / 30;



                                    leave = int.Parse(System.Math.Round(Day).ToString());
                                }
                                else
                                {
                                    leave = int.Parse(Convert.ToDouble(AssignLeave).ToString());

                                }


                                objEmpleave.InsertEmployeeLeaveTrans(Session["CompId"].ToString(), EmpId, LeaveTypeId, JoiningDate.Year.ToString(), JoiningDate.Month.ToString(), "0", leave.ToString(), leave.ToString(), "0", leave.ToString(), "0", "", "", "", "", "", true.ToString(), DateTime.Now.ToString(), true.ToString(), Session["UserId"].ToString(), DateTime.Now.ToString(), Session["UserId"].ToString(), DateTime.Now.ToString());
                                JoiningDate = JoiningDate.AddMonths(1);
                                JoiningDate = new DateTime(JoiningDate.Year, JoiningDate.Month, 1);

                            }
                        }
                        else
                        {
                            //next month to end will delete







                            JoiningDate = new DateTime(DateTime.Now.Year, DateTime.Now.Month, DateTime.Now.Day);




                            int leave = 0;
                            if (JoiningDate.Day != 1)
                            {

                                double JoinDay = 30 - JoiningDate.Day;

                                double Day = (Convert.ToDouble(JoinDay) * Convert.ToDouble(leavepermonthPrev)) / 30;

                                leave = int.Parse(System.Math.Round(Day).ToString());
                            }
                            else
                            {
                                leave = int.Parse(Convert.ToDouble(leavepermonthPrev).ToString());

                            }





                            int Totalleave = int.Parse(leave.ToString());


                            objEmpleave.DeleteEmployeeLeaveTransByEmpIdandleaveTypeId(EmpId, LeaveTypeId, "0", JoiningDate.Year.ToString());


                            objEmpleave.InsertEmployeeLeaveTrans(Session["CompId"].ToString(), EmpId, LeaveTypeId, JoiningDate.Year.ToString(), "0", "0", Totalleave.ToString(), Totalleave.ToString(), "0", Totalleave.ToString(), "0", "", "", "", "", "", true.ToString(), DateTime.Now.ToString(), true.ToString(), Session["UserId"].ToString(), DateTime.Now.ToString(), Session["UserId"].ToString(), DateTime.Now.ToString());









                            JoiningDate = new DateTime(DateTime.Now.Year, DateTime.Now.Month, DateTime.Now.Day);
                            int monthJ = 0;
                            int yearJ = 0;



                            if (JoiningDate.Month == 12)
                            {
                                monthJ = 1;
                                yearJ = JoiningDate.Year + 1;
                            }
                            else
                            {
                                monthJ = JoiningDate.Month + 1;
                                yearJ = JoiningDate.Year;
                            }

                            JoiningDate = new DateTime(yearJ, monthJ, 1);




                            while (JoiningDate <= FinancialYearEndDate)
                            {



                                int leave1 = 0;

                                leave1 = int.Parse(Convert.ToDouble(AssignLeave).ToString());


                                objEmpleave.InsertEmployeeLeaveTrans(Session["CompId"].ToString(), EmpId, LeaveTypeId, JoiningDate.Year.ToString(), JoiningDate.Month.ToString(), "0", leave1.ToString(), leave1.ToString(), "0", leave1.ToString(), "0", "", "", "", "", "", true.ToString(), DateTime.Now.ToString(), true.ToString(), Session["UserId"].ToString(), DateTime.Now.ToString(), Session["UserId"].ToString(), DateTime.Now.ToString());
                                JoiningDate = JoiningDate.AddMonths(1);
                                JoiningDate = new DateTime(JoiningDate.Year, JoiningDate.Month, 1);

                            }



                        }

                    }

                }

            }

        }






    }
    protected void ddlSchedule_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (ddlSchType.SelectedIndex != 0)
        {
            if (ddlSchType.SelectedIndex == 1)
            {
                chkYearCarry.Visible = false;
            }
            else if (ddlSchType.SelectedIndex == 2)
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


    protected void btnClosePanel_Click2(object sender, EventArgs e)
    {
        pnl1.Visible = false;
        pnl2.Visible = false;
    }
    protected void btnClosePanel_Click1(object sender, EventArgs e)
    {
        pnl1.Visible = false;
        pnl2.Visible = false;
    }
    protected void btnClosePanel_Click5(object sender, EventArgs e)
    {
        pnlSal1.Visible = false;
        pnlSal2.Visible = false;
    }

    protected void btnClosePanel_ClickPenalty(object sender, EventArgs e)
    {
        pnlPen1.Visible = false;
        pnlPen2.Visible = false;

    }
    protected void btnClosePanel_Click6(object sender, EventArgs e)
    {
        pnlOT1.Visible = false;
        pnlOT2.Visible = false;
    }
    protected void btnResetLeave_Click(object sender, EventArgs e)
    {
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

        }

        ddlLeaveType.SelectedIndex = 0;
        ddlSchType.SelectedIndex = 0;
        txtPaidLeave.Text = "";
        txtTotalLeave.Text = "";
        txtPerSal.Text = "";
        chkMonthCarry.Checked = false;
        chkYearCarry.Checked = false;
        gvLeaveEmp.DataSource = null;
        gvLeaveEmp.DataBind();
        Session["dtLeave"] = null;
        gridEmpLeave.DataSource = null;
        gridEmpLeave.DataBind();
        chkYearCarry.Visible = false;

        DataTable dtGroup = objEmpGroup.GetEmployeeGroup_Master(Session["CompId"].ToString());
        dtGroup = new DataView(dtGroup, "Brand_Id='" + Session["BrandId"].ToString() + "'  and Location_Id='" + Session["LocId"].ToString() + "'", "", DataViewRowState.CurrentRows).ToTable();
        if (dtGroup.Rows.Count > 0)
        {
            lbxGroup.DataSource = dtGroup;
            lbxGroup.DataTextField = "Group_Name";
            lbxGroup.DataValueField = "Group_Id";

            lbxGroup.DataBind();

        }

        gvEmployee.DataSource = null;
        gvEmployee.DataBind();

    }
    protected void txtPaidLeaveTextChanged(object sender, EventArgs e)
    {
    }
    protected void btnSave_Click(object sender, EventArgs e)
    {
        DataTable dtDevice = new DataTable();
        string EmpSync = string.Empty;
        EmpSync = objAppParam.GetApplicationParameterValueByParamName("Employee Synchronization", Session["CompId"].ToString());

        string BankId = "0";
        int b = 0;
        if (txtEmployeeCode.Text == "")
        {
            DisplayMessage("Enter Employee Code");
            txtEmployeeCode.Focus();
            return;
        }
        else if (txtEmployeeCode.Text.Trim().Contains('/'))
        {
            DisplayMessage("/ Sign not allow for Employee Code");
            txtEmployeeCode.Focus();
            return;

        }
        if (txtEmployeeName.Text == "")
        {
            DisplayMessage("Enter Employee Name");
            txtEmployeeName.Focus();
            return;
        }
        if (txtDob.Text == "")
        {
            txtDob.Text = DateTime.Now.ToString();

        }
        else
        {
            try
            {
                Convert.ToDateTime(txtDob.Text);

            }
            catch
            {
                DisplayMessage("Enter Correct Date of Birth Format");
                txtDob.Focus();
                return;

            }


        }
        if (txtEmailId.Text != "")
        {
            if (!IsValidEmail(txtEmailId.Text))
            {
                DisplayMessage("Enter Correct Email Id Format");
                txtEmailId.Focus();
                return;

            }

        }
        if (txtDoj.Text != "")
        {

            try
            {
                Convert.ToDateTime(txtDoj.Text);

            }
            catch
            {
                DisplayMessage("Enter Correct Date of joining Format");
                txtDoj.Focus();
                return;

            }
        }
        else
        {
            txtDoj.Text = DateTime.Now.ToString();
        }
        DateTime terminationDate = new DateTime();
        if (txtTermDate.Text != "")
        {

            try
            {
                Convert.ToDateTime(txtTermDate.Text);
                terminationDate = Convert.ToDateTime(txtTermDate.Text);
            }
            catch
            {
                DisplayMessage("Enter Correct Termination Date Format");
                txtTermDate.Focus();
                return;

            }
        }
        else
        {
            terminationDate = new DateTime(1900, 1, 1);

        }
        if (Session["empimgpath"] == null)
        {
            Session["empimgpath"] = "";

        }

        if (Session["empimgpath"] == null)
        {
            Session["empimgpath"] = "";

        }
        if (editid.Value == "")
        {

            DataTable dt = objEmp.GetEmployeeMaster(Session["CompId"].ToString());
            dt = new DataView(dt, "Brand_Id='" + Session["BrandId"].ToString() + "' and Location_Id='" + Session["LocId"].ToString() + "' ", "", DataViewRowState.CurrentRows).ToTable();


            dt = new DataView(dt, "Emp_Code='" + txtEmployeeCode.Text + "'", "", DataViewRowState.CurrentRows).ToTable();

            if (dt.Rows.Count > 0)
            {
                DisplayMessage("Employee Code Already Exists");
                txtEmployeeCode.Focus();
                return;

            }
            DataTable dt1 = objEmp.GetEmployeeMaster(Session["CompId"].ToString());
            dt1 = new DataView(dt1, "Brand_Id='" + Session["BrandId"].ToString() + "' and Location_Id='" + Session["LocId"].ToString() + "' ", "", DataViewRowState.CurrentRows).ToTable();


            dt1 = new DataView(dt1, "Emp_Name='" + txtEmployeeName.Text + "'", "", DataViewRowState.CurrentRows).ToTable();
            if (dt1.Rows.Count > 0)
            {
                DisplayMessage("Employee Name Already Exists");
                txtEmployeeName.Focus();
                return;

            }


            b = objEmp.InsertEmployeeMaster(Session["CompId"].ToString(), txtEmployeeName.Text, txtEmployeeL.Text, txtEmployeeCode.Text, Session["empimgpath"].ToString(), Session["BrandId"].ToString(), Session["LocId"].ToString(), ddlDepartment.SelectedValue, txtCivilId.Text, ddlDesignation.Text, ddlReligion.SelectedValue, ddlNationality.SelectedValue, ddlQualification.SelectedValue, txtDob.Text, txtDoj.Text, ddlEmpType.SelectedValue, terminationDate.ToString(), ddlGender.SelectedValue, "", "", "", "", "", true.ToString(), DateTime.Now.ToString(), true.ToString(), Session["UserId"].ToString(), DateTime.Now.ToString(), Session["UserId"].ToString(), DateTime.Now.ToString(), txtEmailId.Text, txtPhoneNo.Text);

            if (b != 0)
            {
                objEmpParam.InsertEmployeeParameterOnEmployeeInsert(Session["CompId"].ToString(), b.ToString(), Session["UserId"].ToString(), DateTime.Now.ToString());


                if (EmpSync == "Company")
                {
                    dtDevice = objDevice.GetDeviceMaster(Session["CompId"].ToString());

                    if (dtDevice.Rows.Count > 0)
                    {

                        objSer.DeleteUserTransfer(b.ToString());
                        for (int i = 0; i < dtDevice.Rows.Count; i++)
                        {
                            objSer.InsertUserTransfer(b.ToString(), dtDevice.Rows[i]["Device_Id"].ToString(), false.ToString(), DateTime.Now.ToString(), "1/1/1900", "", "", "", "", "", true.ToString(), DateTime.Now.ToString(), true.ToString(), Session["UserId"].ToString(), DateTime.Now.ToString(), Session["UserId"].ToString(), DateTime.Now.ToString());

                        }


                    }


                }
                else
                {
                    dtDevice = objDevice.GetDeviceMaster(Session["CompId"].ToString());
                    dtDevice = new DataView(dtDevice, "Location_Id='" + Session["LocId"].ToString() + "'", "", DataViewRowState.CurrentRows).ToTable();

                    if (dtDevice.Rows.Count > 0)
                    {


                        for (int i = 0; i < dtDevice.Rows.Count; i++)
                        {
                            objSer.InsertUserTransfer(b.ToString(), dtDevice.Rows[i]["Device_Id"].ToString(), false.ToString(), DateTime.Now.ToString(), "1/1/1900", "", "", "", "", "", true.ToString(), DateTime.Now.ToString(), true.ToString(), Session["UserId"].ToString(), DateTime.Now.ToString(), Session["UserId"].ToString(), DateTime.Now.ToString());

                        }


                    }
                }


                string strMaxId = string.Empty;
                strMaxId = b.ToString();

                foreach (GridViewRow gvr in GvAddressName.Rows)
                {
                    Label lblGvAddressName = (Label)gvr.FindControl("lblgvAddressName");

                    if (lblGvAddressName.Text != "")
                    {
                        DataTable dtAddId = AM.GetAddressDataByAddressName(Session["CompId"].ToString(), lblGvAddressName.Text);
                        if (dtAddId.Rows.Count > 0)
                        {
                            string strAddressId = dtAddId.Rows[0]["Trans_Id"].ToString();
                            objAddChild.InsertAddressChild(strAddressId, "Employee", strMaxId, "True", "", "", "", "", "", "True", DateTime.Now.ToString(), "True", Session["UserId"].ToString(), DateTime.Now.ToString(), Session["UserId"].ToString(), DateTime.Now.ToString());
                        }
                    }
                }

                if (txtBankName.Text != "")
                {
                    DataTable dt4 = objBank.GetBankMaster(Session["CompId"].ToString());

                    dt4 = new DataView(dt4, "Bank_Name='" + txtBankName.Text + "'", "", DataViewRowState.CurrentRows).ToTable();
                    if (dt4.Rows.Count == 0)
                    {
                        DisplayMessage("Bank Name Not Exists");
                        txtBankName.Focus();
                        return;

                    }
                    else
                    {
                        BankId = dt4.Rows[0]["Bank_Id"].ToString();

                        objBankInfo.DeleteBankInfo(editid.Value, "Employee");

                        objBankInfo.InsertBankInfo(Session["CompId"].ToString(), Session["BrandId"].ToString(), BankId, "Employee", editid.Value, ddlAcountType.SelectedValue, txtAccountNo.Text, "", "", "", "", "", "True", DateTime.Now.ToString(), "True", Session["UserId"].ToString(), DateTime.Now.ToString(), Session["UserId"].ToString(), DateTime.Now.ToString());
                    }
                }
                DisplayMessage("Record Saved");

                btnList_Click(null, null);

                Reset();
                FillDataListGrid();

            }
            else
            {
                DisplayMessage("Record Not Saved");
            }
        }
        else
        {
            string EmpCode = string.Empty;
            DataTable dt = objEmp.GetEmployeeMaster(Session["CompId"].ToString());


            dt = new DataView(dt, "Brand_Id='" + Session["BrandId"].ToString() + "' and Location_Id='" + Session["LocId"].ToString() + "' ", "", DataViewRowState.CurrentRows).ToTable();

            try
            {
                EmpCode = (new DataView(dt, "Emp_Id='" + editid.Value + "'", "", DataViewRowState.CurrentRows).ToTable()).Rows[0]["Emp_Code"].ToString();
            }
            catch
            {
                EmpCode = "";
            }


            dt = new DataView(dt, "Emp_Code='" + txtEmployeeCode.Text + "' and Emp_Code<>'" + EmpCode + "'", "", DataViewRowState.CurrentRows).ToTable();

            if (dt.Rows.Count > 0)
            {
                DisplayMessage("Employee Code Already Exists");
                txtEmployeeCode.Focus();
                return;

            }

            b = objEmp.UpdateEmployeeMaster(editid.Value, Session["CompId"].ToString(), txtEmployeeName.Text, txtEmployeeL.Text, txtEmployeeCode.Text, Session["empimgpath"].ToString(), Session["BrandId"].ToString(), Session["LocId"].ToString(), ddlDepartment.SelectedValue, txtCivilId.Text, ddlDesignation.Text, ddlReligion.SelectedValue, ddlNationality.SelectedValue, ddlQualification.SelectedValue, txtDob.Text, txtDoj.Text, ddlEmpType.SelectedValue, terminationDate.ToString(), ddlGender.SelectedValue, "", "", "", "", "", true.ToString(), DateTime.Now.ToString(), true.ToString(), Session["UserId"].ToString(), DateTime.Now.ToString(), txtEmailId.Text, txtPhoneNo.Text);



            if (b != 0)
            {
                if (EmpSync == "Company")
                {
                    dtDevice = objDevice.GetDeviceMaster(Session["CompId"].ToString());

                    if (dtDevice.Rows.Count > 0)
                    {

                        objSer.DeleteUserTransfer(b.ToString());
                        for (int i = 0; i < dtDevice.Rows.Count; i++)
                        {
                            objSer.InsertUserTransfer(b.ToString(), dtDevice.Rows[i]["Device_Id"].ToString(), false.ToString(), DateTime.Now.ToString(), "1/1/1900", "", "", "", "", "", true.ToString(), DateTime.Now.ToString(), true.ToString(), Session["UserId"].ToString(), DateTime.Now.ToString(), Session["UserId"].ToString(), DateTime.Now.ToString());

                        }


                    }


                }
                else
                {
                    dtDevice = objDevice.GetDeviceMaster(Session["CompId"].ToString());
                    dtDevice = new DataView(dtDevice, "Location_Id='" + Session["LocId"].ToString() + "'", "", DataViewRowState.CurrentRows).ToTable();

                    if (dtDevice.Rows.Count > 0)
                    {


                        for (int i = 0; i < dtDevice.Rows.Count; i++)
                        {
                            objSer.InsertUserTransfer(b.ToString(), dtDevice.Rows[i]["Device_Id"].ToString(), false.ToString(), DateTime.Now.ToString(), "1/1/1900", "", "", "", "", "", true.ToString(), DateTime.Now.ToString(), true.ToString(), Session["UserId"].ToString(), DateTime.Now.ToString(), Session["UserId"].ToString(), DateTime.Now.ToString());

                        }


                    }
                }

                objAddChild.DeleteAddressChild("Employee", editid.Value);
                foreach (GridViewRow gvr in GvAddressName.Rows)
                {
                    Label lblGvAddressName = (Label)gvr.FindControl("lblgvAddressName");

                    if (lblGvAddressName.Text != "")
                    {
                        DataTable dtAddId = AM.GetAddressDataByAddressName(Session["CompId"].ToString(), lblGvAddressName.Text);
                        if (dtAddId.Rows.Count > 0)
                        {
                            string strAddressId = dtAddId.Rows[0]["Trans_Id"].ToString();
                            objAddChild.InsertAddressChild(strAddressId, "Employee", editid.Value, "True", "", "", "", "", "", "True", DateTime.Now.ToString(), "True", Session["UserId"].ToString(), DateTime.Now.ToString(), Session["UserId"].ToString(), DateTime.Now.ToString());
                        }
                    }
                }


                //Bank Info of Employee 1 Oct 2013
                if (txtBankName.Text != "")
                {
                    DataTable dt1 = objBank.GetBankMaster(Session["CompId"].ToString());

                    dt1 = new DataView(dt1, "Bank_Name='" + txtBankName.Text + "'", "", DataViewRowState.CurrentRows).ToTable();
                    if (dt1.Rows.Count == 0)
                    {
                        DisplayMessage("Bank Name Not Exists");
                        txtBankName.Focus();
                        return;

                    }
                    else
                    {
                        BankId = dt1.Rows[0]["Bank_Id"].ToString();

                        objBankInfo.DeleteBankInfo(editid.Value, "Employee");

                        objBankInfo.InsertBankInfo(Session["CompId"].ToString(), Session["BrandId"].ToString(), BankId, "Employee", editid.Value, ddlAcountType.SelectedValue, txtAccountNo.Text, "", "", "", "", "", "True", DateTime.Now.ToString(), "True", Session["UserId"].ToString(), DateTime.Now.ToString(), Session["UserId"].ToString(), DateTime.Now.ToString());
                    }
                }

                DisplayMessage("Record Updated");

                btnList_Click(null, null);

                Reset();
                FillDataListGrid();

            }
            else
            {
                DisplayMessage("Record Not Updated");
            }












        }






    }
    bool IsValidEmail(string email)
    {
        try
        {
            var addr = new System.Net.Mail.MailAddress(email);
            return true;
        }
        catch
        {
            return false;
        }
    }
    protected void btnReset_Click(object sender, EventArgs e)
    {
        Reset();
    }
    protected void btnCancel_Click(object sender, EventArgs e)
    {
        Reset();
        btnList_Click(null, null);

    }








    protected void btnbinbind_Click(object sender, ImageClickEventArgs e)
    {
        ViewState["CurrIndexbin"] = 0;
        ViewState["SubSizebin"] = 9;
        DataTable dtproduct = objEmp.GetEmployeeMasterInactive(Session["CompId"].ToString().ToString());


        if (dtproduct.Rows.Count > 0)
        {

            lnkbinNext.Visible = true;
            lnkbinLast.Visible = true;
        }
        if (ddlbinOption.SelectedIndex != 0)
        {
            string condition = string.Empty;
            if (ddlbinOption.SelectedIndex == 1)
            {
                condition = "convert(" + ddlbinFieldName.SelectedValue + ",System.String)='" + txtbinVal.Text.Trim() + "'";
            }
            else if (ddlbinOption.SelectedIndex == 2)
            {
                condition = "convert(" + ddlbinFieldName.SelectedValue + ",System.String) Like '" + txtbinVal.Text.Trim() + "%'";
            }
            else
            {
                condition = "convert(" + ddlbinFieldName.SelectedValue + ",System.String) like '%" + txtbinVal.Text.Trim() + "%'";
            }
            DataView view = new DataView(dtproduct, condition, "", DataViewRowState.CurrentRows);

            lblbinTotalRecord.Text = Resources.Attendance.Total_Records + " : " + (view.ToTable()).Rows.Count.ToString() + "";

            Session["dtEmpBin"] = view.ToTable();

            dtproduct = view.ToTable();
            if (dtproduct.Rows.Count <= 9)
            {
                dtlistbinEmp.DataSource = dtproduct;
                dtlistbinEmp.DataBind();
                gvBinEmp.DataSource = dtproduct;
                gvBinEmp.DataBind();
                lnkbinPrev.Visible = false;
                lnkbinFirst.Visible = false;
                lnkbinNext.Visible = false;
                lnkbinLast.Visible = false;


            }
            else
            {
                FillBinDataList(dtproduct, Convert.ToInt32(ViewState["CurrIndexbin"].ToString()), Convert.ToInt32(ViewState["SubSizebin"].ToString()));

            }

            if (gvBinEmp.Visible == true)
            {
                if (dtproduct.Rows.Count == 0)
                {
                    imgBtnRestore.Visible = false;
                    ImgbtnSelectAll.Visible = false;
                }
                else
                {
                    AllPageCode();
                }

            }
        }



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
        gridEmpLeave.DataSource = null;
        gridEmpLeave.DataBind();
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
        gridEmpLeave.DataSource = null;
        gridEmpLeave.DataBind();
        chkYearCarry.Visible = false;
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
        gridEmpLeave.DataSource = null;
        gridEmpLeave.DataBind();
        chkYearCarry.Visible = false;
    }


    protected void btnbinRefresh_Click(object sender, ImageClickEventArgs e)
    {

        lnkbinNext.Visible = true;
        lnkbinLast.Visible = true;
        ViewState["CurrIndexbin"] = 0;
        ViewState["SubSizebin"] = 9;
        FillbinDataListGrid();
        ddlbinFieldName.SelectedIndex = 1;
        ddlbinOption.SelectedIndex = 3;
        txtbinVal.Text = "";
        btnbingo_Click(null, null);
        lblSelectedRecord.Text = "";
        ViewState["Select"] = null;
        System.Web.UI.ScriptManager.GetCurrent(this).SetFocus(txtValue);

    }
    protected void btnbingo_Click(object sender, EventArgs e)
    {
        ViewState["CurrIndexbin"] = 0;
        ViewState["SubSizebin"] = 9;
        lnkbinFirst.Visible = false;
        lnkbinPrev.Visible = false;
        lnkbinNext.Visible = true;
        lnkbinLast.Visible = true;
        FillbinDataListGrid();

    }
    protected void imgBtnbinGrid_Click(object sender, ImageClickEventArgs e)
    {

        lnkbinNext.Visible = false;
        lnkbinLast.Visible = false;
        lnkbinFirst.Visible = false;
        lnkbinPrev.Visible = false;
        dtlistbinEmp.Visible = false;
        gvBinEmp.Visible = true;

        FillbinDataListGrid();
        imgBtnbinDatalist.Visible = true;
        imgBtnbinGrid.Visible = false;
        txtbinVal.Focus();

    }
    protected void imgbtnbinDatalist_Click(object sender, ImageClickEventArgs e)
    {


        ViewState["CurrIndexbin"] = 0;
        ViewState["SubSizebin"] = 9;

        lnkbinNext.Visible = true;
        lnkbinLast.Visible = true;
        dtlistbinEmp.Visible = true;
        gvBinEmp.Visible = false;
        FillbinDataListGrid();
        imgBtnbinDatalist.Visible = false;
        imgBtnbinGrid.Visible = true;

        ImgbtnSelectAll.Visible = false;
        imgBtnRestore.Visible = false;
        txtbinVal.Focus();

    }
    protected void btnBinResetSreach_Click(object sender, EventArgs e)
    {

        FillbinDataListGrid();
        System.Web.UI.ScriptManager.GetCurrent(this).SetFocus(txtValue);
    }


    protected void lnkbinFirst_Click(object sender, EventArgs e)
    {
        lnkbinPrev.Visible = false;
        lnkbinFirst.Visible = false;
        lnkbinLast.Visible = true;
        lnkbinNext.Visible = true;
        ViewState["CurrIndexbin"] = 0;
        ViewState["SubSizebin"] = 9;
        DataTable dt = (DataTable)Session["dtEmpBin"];
        FillBinDataList(dt, Convert.ToInt32(ViewState["CurrIndexbin"].ToString()), Convert.ToInt32(ViewState["SubSizebin"].ToString()));
        ViewState["SubSizebin"] = 9;

        int index = dt.Rows.Count / Convert.ToInt32(ViewState["SubSizebin"].ToString());
    }
    protected void lnkbinLast_Click(object sender, EventArgs e)
    {
        ViewState["SubSizebin"] = 9;
        DataTable dt = (DataTable)Session["dtEmpBin"];
        int index = dt.Rows.Count / Convert.ToInt32(ViewState["SubSizebin"].ToString());

        ViewState["CurrIndexbin"] = index;
        int tot = dt.Rows.Count;

        if (tot % Convert.ToInt32(ViewState["SubSizebin"].ToString()) > 0)
        {
            FillBinDataList(dt, Convert.ToInt32(ViewState["CurrIndexbin"].ToString()), Convert.ToInt32(ViewState["SubSizebin"].ToString()));
        }
        else if (tot % Convert.ToInt32(ViewState["SubSizebin"].ToString()) == 0)
        {
            FillBinDataList(dt, index - 1, Convert.ToInt32(ViewState["SubSizebin"].ToString()));
        }
        else
        {
            FillBinDataList(dt, Convert.ToInt32(ViewState["CurrIndexbin"].ToString()), Convert.ToInt32(ViewState["SubSizebin"].ToString()));
        }
        lnkbinLast.Visible = false;
        lnkbinNext.Visible = false;
        lnkbinPrev.Visible = true;
        lnkbinFirst.Visible = true;
    }
    protected void lnkbinPrev_Click(object sender, EventArgs e)
    {
        DataTable dt = (DataTable)Session["dtEmpBin"];
        ViewState["SubSizebin"] = 9;
        ViewState["CurrIndexbin"] = Convert.ToInt32(ViewState["CurrIndexbin"].ToString()) - 1;
        if (Convert.ToInt32(ViewState["CurrIndexbin"].ToString()) < 0)
        {
            ViewState["CurrIndexbin"] = 0;


        }

        if (Convert.ToInt16(ViewState["CurrIndexbin"]) == 0)
        {

            lnkbinFirst.Visible = false;
            lnkbinPrev.Visible = false;
            lnkbinNext.Visible = true;
            lnkbinLast.Visible = true;
        }
        else
        {
            lnkbinFirst.Visible = true;
            lnkbinLast.Visible = true;
            lnkbinNext.Visible = true;
        }
        FillBinDataList(dt, Convert.ToInt32(ViewState["CurrIndexbin"].ToString()), Convert.ToInt32(ViewState["SubSizebin"].ToString()));
    }
    protected void lnkbinNext_Click(object sender, EventArgs e)
    {
        ViewState["SubSizebin"] = 9;
        DataTable dt = (DataTable)Session["dtEmpBin"];
        int index = dt.Rows.Count / Convert.ToInt32(ViewState["SubSizebin"].ToString());
        int k1 = Convert.ToInt32(ViewState["CurrIndexbin"].ToString());
        ViewState["CurrIndexbin"] = Convert.ToInt32(ViewState["CurrIndexbin"].ToString()) + 1;
        int k = Convert.ToInt32(ViewState["CurrIndexbin"].ToString());
        if (Convert.ToInt32(ViewState["CurrIndexbin"].ToString()) >= index)
        {
            ViewState["CurrIndexbin"] = index;
            lnkbinNext.Visible = false;
            lnkbinLast.Visible = false;
        }
        int tot = dt.Rows.Count;

        if (k == index)
        {
            if (tot % Convert.ToInt32(ViewState["SubSizebin"].ToString()) > 0)
            {
                FillBinDataList(dt, Convert.ToInt32(ViewState["CurrIndexbin"].ToString()), Convert.ToInt32(ViewState["SubSizebin"].ToString()));
                lnkbinPrev.Visible = true;
                lnkbinFirst.Visible = true;

            }
            else
            {
                lnkbinPrev.Visible = true;
                lnkbinFirst.Visible = true;
                lnkbinLast.Visible = true;
                lnkbinNext.Visible = true;
            }
        }
        else if (k < index)
        {
            if (k + 1 == index)
            {
                if (tot % Convert.ToInt32(ViewState["SubSizebin"].ToString()) > 0)
                {
                    FillBinDataList(dt, Convert.ToInt32(ViewState["CurrIndexbin"].ToString()), Convert.ToInt32(ViewState["SubSizebin"].ToString()));
                    lnkbinPrev.Visible = true;
                    lnkbinFirst.Visible = true;
                }
                else
                {
                    FillBinDataList(dt, Convert.ToInt32(ViewState["CurrIndexbin"].ToString()), Convert.ToInt32(ViewState["SubSizebin"].ToString()));
                    lnkbinNext.Visible = false;
                    lnkbinLast.Visible = false;
                    lnkbinPrev.Visible = true;
                    lnkbinFirst.Visible = true;
                }
            }
            else
            {
                FillBinDataList(dt, Convert.ToInt32(ViewState["CurrIndexbin"].ToString()), Convert.ToInt32(ViewState["SubSizebin"].ToString()));
                lnkbinPrev.Visible = true;
                lnkbinFirst.Visible = true;
            }
        }
        else
        {
            FillBinDataList(dt, Convert.ToInt32(ViewState["CurrIndexbin"].ToString()), Convert.ToInt32(ViewState["SubSizebin"].ToString()));
        }



    }

    protected void chbAcInctive_CheckedChanged(object sender, EventArgs e)
    {
        for (int i = 0; i < dtlistbinEmp.Items.Count; i++)
        {

            CheckBox chb = (CheckBox)(dtlistbinEmp.Items[i].FindControl("chbAcInctive"));
            HiddenField hdempid = (HiddenField)(dtlistbinEmp.Items[i].FindControl("hdnChbActive"));

            if (chb.Checked)
            {
                objEmp.DeleteEmployeeMaster(Session["CompId"].ToString(), hdempid.Value, true.ToString(), Session["UserId"].ToString(), DateTime.Now.ToString());

            }
            else
            {
                objEmp.DeleteEmployeeMaster(Session["CompId"].ToString(), hdempid.Value, false.ToString(), Session["UserId"].ToString(), DateTime.Now.ToString());

            }

        }

        FillbinDataListGrid();

        FillDataListGrid();



        DisplayMessage("Record Activated");

    }
    protected void gvBinEmp_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        gvBinEmp.PageIndex = e.NewPageIndex;
        FillbinDataListGrid();
        string temp = string.Empty;
        bool isselcted;

        for (int i = 0; i < gvBinEmp.Rows.Count; i++)
        {
            Label lblconid = (Label)gvBinEmp.Rows[i].FindControl("lblEmpId");
            string[] split = lblSelectedRecord.Text.Split(',');

            for (int j = 0; j < lblSelectedRecord.Text.Split(',').Length; j++)
            {
                if (lblSelectedRecord.Text.Split(',')[j] != "")
                {
                    if (lblconid.Text.Trim().ToString() == lblSelectedRecord.Text.Split(',')[j].Trim().ToString())
                    {
                        ((CheckBox)gvBinEmp.Rows[i].FindControl("chkgvSelect")).Checked = true;
                    }
                }
            }
        }
        AllPageCode();
    }
    protected void chkgvSelectAll_CheckedChanged(object sender, EventArgs e)
    {
        CheckBox chkSelAll = ((CheckBox)gvBinEmp.HeaderRow.FindControl("chkgvSelectAll"));
        for (int i = 0; i < gvBinEmp.Rows.Count; i++)
        {
            ((CheckBox)gvBinEmp.Rows[i].FindControl("chkgvSelect")).Checked = chkSelAll.Checked;
            if (chkSelAll.Checked)
            {
                if (!lblSelectedRecord.Text.Split(',').Contains(((Label)(gvBinEmp.Rows[i].FindControl("lblEmpId"))).Text.Trim().ToString()))
                {
                    lblSelectedRecord.Text += ((Label)(gvBinEmp.Rows[i].FindControl("lblEmpId"))).Text.Trim().ToString() + ",";
                }
            }
            else
            {
                string temp = string.Empty;
                string[] split = lblSelectedRecord.Text.Split(',');
                foreach (string item in split)
                {
                    if (item != ((Label)(gvBinEmp.Rows[i].FindControl("lblEmpId"))).Text.Trim().ToString())
                    {
                        if (item != "")
                        {
                            temp += item + ",";
                        }
                    }
                }
                lblSelectedRecord.Text = temp;
            }
        }
    }
    protected void chkgvSelect_CheckedChanged(object sender, EventArgs e)
    {
        string empidlist = string.Empty;
        string temp = string.Empty;
        int index = ((GridViewRow)((CheckBox)sender).Parent.Parent).RowIndex;
        Label lb = (Label)gvBinEmp.Rows[index].FindControl("lblEmpId");
        if (((CheckBox)gvBinEmp.Rows[index].FindControl("chkgvSelect")).Checked)
        {
            empidlist += lb.Text.Trim().ToString() + ",";
            lblSelectedRecord.Text += empidlist;

        }

        else
        {

            empidlist += lb.Text.ToString().Trim();
            lblSelectedRecord.Text += empidlist;
            string[] split = lblSelectedRecord.Text.Split(',');
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
            lblSelectedRecord.Text = temp;
        }
    }

    protected void ImgbtnSelectAll_Click(object sender, ImageClickEventArgs e)
    {
        DataTable dtProduct = (DataTable)Session["dtEmpBin"];

        if (ViewState["Select"] == null)
        {
            ViewState["Select"] = 1;
            foreach (DataRow dr in dtProduct.Rows)
            {
                if (!lblSelectedRecord.Text.Split(',').Contains(dr["Emp_Id"]))
                {
                    lblSelectedRecord.Text += dr["Emp_Id"] + ",";
                }
            }
            for (int i = 0; i < gvBinEmp.Rows.Count; i++)
            {
                string[] split = lblSelectedRecord.Text.Split(',');
                Label lblconid = (Label)gvBinEmp.Rows[i].FindControl("lblEmpId");
                for (int j = 0; j < lblSelectedRecord.Text.Split(',').Length; j++)
                {
                    if (lblSelectedRecord.Text.Split(',')[j] != "")
                    {
                        if (lblconid.Text.Trim().ToString() == lblSelectedRecord.Text.Split(',')[j].Trim().ToString())
                        {
                            ((CheckBox)gvBinEmp.Rows[i].FindControl("chkgvSelect")).Checked = true;
                        }
                    }
                }
            }
        }
        else
        {
            lblSelectedRecord.Text = "";
            DataTable dtProduct1 = (DataTable)Session["dtEmpBin"];
            gvBinEmp.DataSource = dtProduct1;
            gvBinEmp.DataBind();
            ViewState["Select"] = null;
        }

    }
    protected void imgBtnRestore_Click(object sender, ImageClickEventArgs e)
    {
        int b = 0;
        if (lblSelectedRecord.Text != "")
        {
            for (int j = 0; j < lblSelectedRecord.Text.Split(',').Length; j++)
            {
                if (lblSelectedRecord.Text.Split(',')[j] != "")
                {
                    b = objEmp.DeleteEmployeeMaster(Session["CompId"].ToString(), lblSelectedRecord.Text.Split(',')[j].Trim().ToString(), true.ToString(), Session["UserId"].ToString(), DateTime.Now.ToString());


                }
            }
        }

        if (b != 0)
        {


            FillbinDataListGrid();
            FillDataListGrid();
            lblSelectedRecord.Text = "";
            ViewState["Select"] = null;
            DisplayMessage("Record Activated");
        }
        else
        {
            int fleg = 0;
            foreach (GridViewRow Gvr in gvBinEmp.Rows)
            {
                CheckBox chk = (CheckBox)Gvr.FindControl("chkgvSelect");
                if (chk.Checked)
                {
                    fleg = 1;
                }
                else
                {
                    fleg = 0;
                }
            }
            if (fleg == 0)
            {
                DisplayMessage("Please Select Record");
            }
            else
            {
                DisplayMessage("Record Not Activated");
            }
        }

    }












    protected void FULogoPath_UploadedComplete(object sender, AjaxControlToolkit.AsyncFileUploadEventArgs e)
    {


        if (FULogoPath.HasFile)
        {
            if (!Directory.Exists(Server.MapPath("~/CompanyResource/") + Session["CompId"]))
            {
                Directory.CreateDirectory(Server.MapPath("~/CompanyResource/") + Session["CompId"]);
            }


            string path = Server.MapPath("~/CompanyResource/" + "/" + Session["CompId"] + "/") + FULogoPath.FileName;
            FULogoPath.SaveAs(path);
            Session["empimgpath"] = FULogoPath.FileName;
        }


    }

    protected void btnUpload1_Click(object sender, EventArgs e)
    {
        imgLogo.ImageUrl = "~/CompanyResource/" + "/" + Session["CompId"] + "/" + FULogoPath.FileName;

    }









    public void FillDataList(DataTable dt, int currentIndex, int SubSize)
    {
        int startRow = currentIndex * SubSize;
        int rowCounter = 0;
        DataTable dtBind = dt.Clone();

        while (rowCounter < SubSize)
        {
            if (startRow < dt.Rows.Count)
            {
                DataRow row = dtBind.NewRow();
                foreach (DataColumn dc in dt.Columns)
                {

                    row[dc.ColumnName] = dt.Rows[startRow][dc.ColumnName];
                }

                dtBind.Rows.Add(row);
                startRow++;
            }
            rowCounter++;
        }


        dtlistEmp.DataSource = dtBind;
        dtlistEmp.DataBind();
        AllPageCode();

    }



    private void FillDataListGrid()
    {


        DataTable dtproduct = objEmp.GetEmployeeMaster(Session["CompId"].ToString());

        dtproduct = new DataView(dtproduct, "Brand_Id='" + Session["BrandId"].ToString() + "'  and Location_Id='" + Session["LocId"].ToString() + "'", "", DataViewRowState.CurrentRows).ToTable();

        if (Session["SessionDepId"] != null)
        {
            dtproduct = new DataView(dtproduct, "Department_Id in(" + Session["SessionDepId"].ToString().Substring(0, Session["SessionDepId"].ToString().Length - 1) + ")", "", DataViewRowState.CurrentRows).ToTable();

        }

        Session["dtEmp"] = dtproduct;

        if (dtproduct.Rows.Count <= 9)
        {
            dtlistEmp.DataSource = dtproduct;
            dtlistEmp.DataBind();
            gvEmp.DataSource = dtproduct;
            gvEmp.DataBind();
            lnkPrev.Visible = false;
            lnkFirst.Visible = false;
            lnkNext.Visible = false;
            lnkLast.Visible = false;
        }
        else
        {
            lnkNext.Visible = true;
            lnkLast.Visible = true;
            FillDataList(dtproduct, Convert.ToInt32(ViewState["CurrIndex"].ToString()), Convert.ToInt32(ViewState["SubSize"].ToString()));
            if (gvEmp.Visible == true)
            {
                lnkPrev.Visible = false;
                lnkFirst.Visible = false;
                lnkNext.Visible = false;
                lnkLast.Visible = false;
                gvEmp.DataSource = dtproduct;
                gvEmp.DataBind();
            }
        }

        lblTotalRecord.Text = Resources.Attendance.Total_Records + " : " + dtproduct.Rows.Count.ToString();

        AllPageCode();

    }


    public string getdepartment(object empid)
    {
        string dept = string.Empty;

        DataTable dt = objEmp.GetEmployeeMasterAllData(Session["CompId"].ToString());

        dt = new DataView(dt, "Emp_Id='" + empid.ToString().Trim() + "'", "", DataViewRowState.CurrentRows).ToTable();

        if (dt.Rows.Count > 0)
        {

            DataTable dtDept = objDep.GetDepartmentMasterById(Session["CompId"].ToString(), dt.Rows[0]["Department_Id"].ToString());

            if (dtDept.Rows.Count > 0)
            {
                dept = dtDept.Rows[0]["Dep_Name"].ToString();

            }
            else
            {
                dept = "No Department";

            }



        }


        return dept;


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

    public string getImageByEmpId(object empid)
    {
        string img = string.Empty;
        DataTable dt = objEmp.GetEmployeeMasterAllData(Session["CompId"].ToString());

        dt = new DataView(dt, "Emp_Id='" + empid.ToString().Trim() + "'", "", DataViewRowState.CurrentRows).ToTable();

        if (dt.Rows.Count > 0)
        {

            if (dt.Rows[0]["Emp_Image"].ToString() != "")
            {
                img = "~/CompanyResource/" + Session["CompId"] + "/" + dt.Rows[0]["Emp_Image"].ToString();
            }
            else
            {
                img = "~/CompanyResource/User.png";

            }

        }
        return img;
    }
    public string getdesg(object empid)
    {
        string dept = string.Empty;

        DataTable dt = objEmp.GetEmployeeMasterAllData(Session["CompId"].ToString());

        dt = new DataView(dt, "Emp_Id='" + empid.ToString().Trim() + "'", "", DataViewRowState.CurrentRows).ToTable();

        if (dt.Rows.Count > 0)
        {

            DataTable dtDept = objDesg.GetDesignationMasterById(Session["CompId"].ToString(), dt.Rows[0]["Designation_Id"].ToString());

            if (dtDept.Rows.Count > 0)
            {
                dept = dtDept.Rows[0]["Designation"].ToString();

            }
            else
            {
                dept = "No Designation";

            }



        }


        return dept;

    }
    public string getdate(object strDate)
    {
        DateTime dtnew = DateTime.Parse(strDate.ToString());
        string strNew = dtnew.ToString(objSys.SetDateFormat());

        return strNew;
    }

    public string getEmailId(object empid)
    {
        string email = string.Empty;

        DataTable dt = objEmp.GetEmployeeMasterAllData(Session["CompId"].ToString());

        dt = new DataView(dt, "Emp_Id='" + empid.ToString().Trim() + "'", "", DataViewRowState.CurrentRows).ToTable();

        if (dt.Rows.Count > 0)
        {

            if (dt.Rows[0]["Email_Id"].ToString() != "")
            {
                email = dt.Rows[0]["Email_Id"].ToString();
            }
            else
            {

                email = "No Email Id";
            }



        }

        return email;
    }









    public string getMobileNo(object empid)
    {
        string email = string.Empty;

        DataTable dt = objEmp.GetEmployeeMasterAllData(Session["CompId"].ToString());

        dt = new DataView(dt, "Emp_Id='" + empid.ToString().Trim() + "'", "", DataViewRowState.CurrentRows).ToTable();

        if (dt.Rows.Count > 0)
        {

            if (dt.Rows[0]["Phone_No"].ToString() != "")
            {
                email = dt.Rows[0]["Phone_No"].ToString();
            }
            else
            {

                email = "No Mobile No.";
            }



        }

        return email;
    }

    public void DisplayMessage(string str)
    {

        ScriptManager.RegisterStartupScript(this, GetType(), "", "alert('" + str + "');", true);

    }
    public void Reset()
    {
        txtAccountNo.Text = "";
        txtBankName.Text = "";
        ddlAcountType.SelectedIndex = 0;

        txtEmployeeCode.Text = "";
        txtEmployeeName.Text = "";
        txtEmployeeL.Text = "";

        txtCivilId.Text = "";
        txtEmailId.Text = "";
        txtPhoneNo.Text = "";
        txtDob.Text = "";
        txtDoj.Text = "";
        try
        {
            ddlDepartment.SelectedIndex = 0;
            ddlDesignation.SelectedIndex = 0;
            ddlEmpType.SelectedIndex = 0;
            ddlReligion.SelectedIndex = 0;
            ddlQualification.SelectedIndex = 0;
            ddlNationality.SelectedIndex = 0;
            ddlGender.SelectedIndex = 0;
        }
        catch
        {

        }
        txtTermDate.Text = "";

        imgLogo.ImageUrl = "";

        Session["empimgpath"] = null;
        hdnProductId.Value = "";

        btnNew.Text = Resources.Attendance.New;

        txtAddressName.Text = "";

        lblSelectedRecord.Text = "";
        ViewState["Select"] = null;
        GvAddressName.DataSource = null;
        GvAddressName.DataBind();
        System.Web.UI.ScriptManager.GetCurrent(this).SetFocus(txtValue);

    }





    public void FillBinDataList(DataTable dt, int currentIndex, int SubSize)
    {
        int startRow = currentIndex * SubSize;
        int rowCounter = 0;
        DataTable dtBind = dt.Clone();

        while (rowCounter < SubSize)
        {
            if (startRow < dt.Rows.Count)
            {
                DataRow row = dtBind.NewRow();
                foreach (DataColumn dc in dt.Columns)
                {

                    row[dc.ColumnName] = dt.Rows[startRow][dc.ColumnName];
                }

                dtBind.Rows.Add(row);
                startRow++;
            }
            rowCounter++;
        }
        dtlistbinEmp.DataSource = dtBind;
        dtlistbinEmp.DataBind();
        AllPageCode();

    }
    private void FillbinDataListGrid()
    {
        DataTable dtproduct = objEmp.GetEmployeeMasterInactive(Session["CompId"].ToString().ToString());

        dtproduct = new DataView(dtproduct, "Brand_Id='" + Session["BrandId"].ToString() + "'  and Location_Id='" + Session["LocId"].ToString() + "'", "", DataViewRowState.CurrentRows).ToTable();
        if (Session["SessionDepId"] != null)
        {
            dtproduct = new DataView(dtproduct, "Department_Id in(" + Session["SessionDepId"].ToString().Substring(0, Session["SessionDepId"].ToString().Length - 1) + ")", "", DataViewRowState.CurrentRows).ToTable();

        }
        Session["dtEmpBin"] = dtproduct;

        if (dtproduct.Rows.Count <= 9)
        {
            dtlistbinEmp.DataSource = dtproduct;
            dtlistbinEmp.DataBind();
            gvBinEmp.DataSource = dtproduct;
            gvBinEmp.DataBind();
            lnkbinPrev.Visible = false;
            lnkbinFirst.Visible = false;
            lnkbinNext.Visible = false;
            lnkbinLast.Visible = false;
        }
        else
        {
            lnkbinNext.Visible = true;
            lnkbinLast.Visible = true;
            FillBinDataList(dtproduct, Convert.ToInt32(ViewState["CurrIndexbin"].ToString()), Convert.ToInt32(ViewState["SubSizebin"].ToString()));
            if (gvBinEmp.Visible == true)
            {
                lnkbinPrev.Visible = false;
                lnkbinFirst.Visible = false;
                lnkbinNext.Visible = false;
                lnkbinLast.Visible = false;
                gvBinEmp.DataSource = dtproduct;
                gvBinEmp.DataBind();
            }
        }

        lblbinTotalRecord.Text = Resources.Attendance.Total_Records + " : " + dtproduct.Rows.Count.ToString();

        if (gvBinEmp.Visible == true)
        {
            if (dtproduct.Rows.Count == 0)
            {
                imgBtnRestore.Visible = false;
                ImgbtnSelectAll.Visible = false;
            }
            else
            {
                imgBtnRestore.Visible = true;
                ImgbtnSelectAll.Visible = true;
            }

        }

        AllPageCode();
    }

    //Employee Notification

    protected void btnSalarybind_Click(object sender, ImageClickEventArgs e)
    {
        if (ddlOptionSal.SelectedIndex != 0)
        {
            string condition = string.Empty;
            if (ddlOptionSal.SelectedIndex == 1)
            {
                condition = "convert(" + ddlFieldSal.SelectedValue + ",System.String)='" + txtValueSal.Text.Trim() + "'";
            }
            else if (ddlOptionSal.SelectedIndex == 2)
            {
                condition = "convert(" + ddlFieldSal.SelectedValue + ",System.String) like '%" + txtValueSal.Text.Trim() + "%'";
            }
            else
            {
                condition = "convert(" + ddlFieldSal.SelectedValue + ",System.String) Like '" + txtValueSal.Text.Trim() + "%'";

            }
            DataTable dtEmp = (DataTable)Session["dtEmpSal"];
            DataView view = new DataView(dtEmp, condition, "", DataViewRowState.CurrentRows);
            gvEmpSalary.DataSource = view.ToTable();
            gvEmpSalary.DataBind();
            lblTotalRecordSal.Text = Resources.Attendance.Total_Records + " : " + (view.ToTable()).Rows.Count.ToString() + "";

        }

    }
    protected void btnSalaryRefresh_Click(object sender, ImageClickEventArgs e)
    {
        lblSelectRecordSal.Text = "";
        ddlFieldSal.SelectedIndex = 1;
        ddlOptionSal.SelectedIndex = 2;
        txtValueSal.Text = "";

        DataTable dtEmp = (DataTable)Session["dtEmpSal"];
        gvEmpSalary.DataSource = dtEmp;
        gvEmpSalary.DataBind();

    }
    protected void btnOTbind_Click(object sender, ImageClickEventArgs e)
    {
        if (ddlOptionOT.SelectedIndex != 0)
        {
            string condition = string.Empty;
            if (ddlOptionOT.SelectedIndex == 1)
            {
                condition = "convert(" + ddlFieldOT.SelectedValue + ",System.String)='" + txtValueOT.Text.Trim() + "'";
            }
            else if (ddlOption1.SelectedIndex == 2)
            {
                condition = "convert(" + ddlFieldOT.SelectedValue + ",System.String) like '%" + txtValueOT.Text.Trim() + "%'";
            }
            else
            {
                condition = "convert(" + ddlFieldOT.SelectedValue + ",System.String) Like '" + txtValueOT.Text.Trim() + "%'";

            }
            DataTable dtEmp = (DataTable)Session["dtEmpOT"];
            DataView view = new DataView(dtEmp, condition, "", DataViewRowState.CurrentRows);
            gvEmpOT.DataSource = view.ToTable();
            gvEmpOT.DataBind();
            lblTotalRecordsLeave.Text = Resources.Attendance.Total_Records + " : " + (view.ToTable()).Rows.Count.ToString() + "";

        }

    }



    protected void btnPenaltybind_Click(object sender, ImageClickEventArgs e)
    {
        if (ddlOptionPenalty.SelectedIndex != 0)
        {
            string condition = string.Empty;
            if (ddlOptionPenalty.SelectedIndex == 1)
            {
                condition = "convert(" + ddlFieldPenalty.SelectedValue + ",System.String)='" + txtValuePenalty.Text.Trim() + "'";
            }
            else if (ddlOptionPenalty.SelectedIndex == 2)
            {
                condition = "convert(" + ddlFieldPenalty.SelectedValue + ",System.String) like '%" + txtValuePenalty.Text.Trim() + "%'";
            }
            else
            {
                condition = "convert(" + ddlFieldPenalty.SelectedValue + ",System.String) Like '" + txtValuePenalty.Text.Trim() + "%'";

            }
            DataTable dtEmp = (DataTable)Session["dtEmpPenalty"];
            DataView view = new DataView(dtEmp, condition, "", DataViewRowState.CurrentRows);
            gvEmpPenalty.DataSource = view.ToTable();
            gvEmpPenalty.DataBind();
            lblTotalRecordPenalty.Text = Resources.Attendance.Total_Records + " : " + (view.ToTable()).Rows.Count.ToString() + "";

        }

    }
    protected void btnNFbind_Click(object sender, ImageClickEventArgs e)
    {
        if (ddlOptionNF.SelectedIndex != 0)
        {
            string condition = string.Empty;
            if (ddlOptionNF.SelectedIndex == 1)
            {
                condition = "convert(" + ddlFieldNF.SelectedValue + ",System.String)='" + txtValueNF.Text.Trim() + "'";
            }
            else if (ddlOption1.SelectedIndex == 2)
            {
                condition = "convert(" + ddlFieldNF.SelectedValue + ",System.String) like '%" + txtValueNF.Text.Trim() + "%'";
            }
            else
            {
                condition = "convert(" + ddlFieldNF.SelectedValue + ",System.String) Like '" + txtValueNF.Text.Trim() + "%'";

            }
            DataTable dtEmp = (DataTable)Session["dtEmpNF"];
            DataView view = new DataView(dtEmp, condition, "", DataViewRowState.CurrentRows);
            gvEmpNF.DataSource = view.ToTable();
            gvEmpNF.DataBind();
            lblTotalRecordsLeave.Text = Resources.Attendance.Total_Records + " : " + (view.ToTable()).Rows.Count.ToString() + "";

        }

    }
    protected void btnNFRefresh_Click(object sender, ImageClickEventArgs e)
    {
        lblSelectRecordNF.Text = "";
        ddlFieldNF.SelectedIndex = 1;
        ddlOptionNF.SelectedIndex = 2;
        txtValueNF.Text = "";

        DataTable dtEmp = (DataTable)Session["dtEmpNF"];
        gvEmpNF.DataSource = dtEmp;
        gvEmpNF.DataBind();
    }
    protected void btnOTRefresh_Click(object sender, ImageClickEventArgs e)
    {
        lblSelectRecordOT.Text = "";
        ddlFieldOT.SelectedIndex = 1;
        ddlOptionOT.SelectedIndex = 2;
        txtValueOT.Text = "";

        DataTable dtEmp = (DataTable)Session["dtEmpOT"];
        gvEmpOT.DataSource = dtEmp;
        gvEmpOT.DataBind();
    }




    protected void btnPenaltyRefresh_Click(object sender, ImageClickEventArgs e)
    {
        lblSelectRecordPenalty.Text = "";
        ddlFieldPenalty.SelectedIndex = 1;
        ddlOptionPenalty.SelectedIndex = 2;
        txtValuePenalty.Text = "";

        DataTable dtEmp = (DataTable)Session["dtEmpPenalty"];
        gvEmpPenalty.DataSource = dtEmp;
        gvEmpPenalty.DataBind();
    }
    protected void ImgbtnSelectAll_ClickSalary(object sender, ImageClickEventArgs e)
    {
        DataTable dtProduct = (DataTable)Session["dtEmpSal"];

        if (ViewState["Select"] == null)
        {
            ViewState["Select"] = 1;
            foreach (DataRow dr in dtProduct.Rows)
            {
                if (!lblSelectRecordSal.Text.Split(',').Contains(dr["Emp_Id"]))
                {
                    lblSelectRecordSal.Text += dr["Emp_Id"] + ",";
                }
            }
            for (int i = 0; i < gvEmpSalary.Rows.Count; i++)
            {
                string[] split = lblSelectRecordSal.Text.Split(',');
                Label lblconid = (Label)gvEmpSalary.Rows[i].FindControl("lblEmpId");
                for (int j = 0; j < lblSelectRecordSal.Text.Split(',').Length; j++)
                {
                    if (lblSelectRecordSal.Text.Split(',')[j] != "")
                    {
                        if (lblconid.Text.Trim().ToString() == lblSelectRecordSal.Text.Split(',')[j].Trim().ToString())
                        {
                            ((CheckBox)gvEmpSalary.Rows[i].FindControl("chkgvSelect")).Checked = true;
                        }
                    }
                }
            }
        }
        else
        {
            lblSelectRecordSal.Text = "";
            DataTable dtProduct1 = (DataTable)Session["dtEmpSal"];
            gvEmpSalary.DataSource = dtProduct1;
            gvEmpSalary.DataBind();
            ViewState["Select"] = null;
        }

    }
    protected void ImgbtnSelectAll_ClickNF(object sender, ImageClickEventArgs e)
    {
        DataTable dtProduct = (DataTable)Session["dtEmpNF"];

        if (ViewState["Select"] == null)
        {
            ViewState["Select"] = 1;
            foreach (DataRow dr in dtProduct.Rows)
            {
                if (!lblSelectRecordNF.Text.Split(',').Contains(dr["Emp_Id"]))
                {
                    lblSelectRecordNF.Text += dr["Emp_Id"] + ",";
                }
            }
            for (int i = 0; i < gvEmpNF.Rows.Count; i++)
            {
                string[] split = lblSelectRecordNF.Text.Split(',');
                Label lblconid = (Label)gvEmpNF.Rows[i].FindControl("lblEmpId");
                for (int j = 0; j < lblSelectRecordNF.Text.Split(',').Length; j++)
                {
                    if (lblSelectRecordNF.Text.Split(',')[j] != "")
                    {
                        if (lblconid.Text.Trim().ToString() == lblSelectRecordNF.Text.Split(',')[j].Trim().ToString())
                        {
                            ((CheckBox)gvEmpNF.Rows[i].FindControl("chkgvSelect")).Checked = true;
                        }
                    }
                }
            }
        }
        else
        {
            lblSelectRecordNF.Text = "";
            DataTable dtProduct1 = (DataTable)Session["dtEmpNF"];
            gvEmpNF.DataSource = dtProduct1;
            gvEmpNF.DataBind();
            ViewState["Select"] = null;
        }
    }


    protected void ImgbtnSelectAll_ClickOT(object sender, ImageClickEventArgs e)
    {
        DataTable dtProduct = (DataTable)Session["dtEmpOT"];

        if (ViewState["Select"] == null)
        {
            ViewState["Select"] = 1;
            foreach (DataRow dr in dtProduct.Rows)
            {
                if (!lblSelectRecordOT.Text.Split(',').Contains(dr["Emp_Id"]))
                {
                    lblSelectRecordOT.Text += dr["Emp_Id"] + ",";
                }
            }
            for (int i = 0; i < gvEmpOT.Rows.Count; i++)
            {
                string[] split = lblSelectRecordOT.Text.Split(',');
                Label lblconid = (Label)gvEmpOT.Rows[i].FindControl("lblEmpId");
                for (int j = 0; j < lblSelectRecordOT.Text.Split(',').Length; j++)
                {
                    if (lblSelectRecordOT.Text.Split(',')[j] != "")
                    {
                        if (lblconid.Text.Trim().ToString() == lblSelectRecordOT.Text.Split(',')[j].Trim().ToString())
                        {
                            ((CheckBox)gvEmpOT.Rows[i].FindControl("chkgvSelect")).Checked = true;
                        }
                    }
                }
            }
        }
        else
        {
            lblSelectRecordOT.Text = "";
            DataTable dtProduct1 = (DataTable)Session["dtEmpOT"];
            gvEmpOT.DataSource = dtProduct1;
            gvEmpOT.DataBind();
            ViewState["Select"] = null;
        }
    }



    protected void ImgbtnSelectAll_ClickPenalty(object sender, ImageClickEventArgs e)
    {
        DataTable dtProduct = (DataTable)Session["dtEmpPenalty"];

        if (ViewState["Select"] == null)
        {
            ViewState["Select"] = 1;
            foreach (DataRow dr in dtProduct.Rows)
            {
                if (!lblSelectRecordPenalty.Text.Split(',').Contains(dr["Emp_Id"]))
                {
                    lblSelectRecordPenalty.Text += dr["Emp_Id"] + ",";
                }
            }
            for (int i = 0; i < gvEmpPenalty.Rows.Count; i++)
            {
                string[] split = lblSelectRecordPenalty.Text.Split(',');
                Label lblconid = (Label)gvEmpPenalty.Rows[i].FindControl("lblEmpId");
                for (int j = 0; j < lblSelectRecordPenalty.Text.Split(',').Length; j++)
                {
                    if (lblSelectRecordPenalty.Text.Split(',')[j] != "")
                    {
                        if (lblconid.Text.Trim().ToString() == lblSelectRecordPenalty.Text.Split(',')[j].Trim().ToString())
                        {
                            ((CheckBox)gvEmpPenalty.Rows[i].FindControl("chkgvSelect")).Checked = true;
                        }
                    }
                }
            }
        }
        else
        {
            lblSelectRecordPenalty.Text = "";
            DataTable dtProduct1 = (DataTable)Session["dtEmpPenalty"];
            gvEmpPenalty.DataSource = dtProduct1;
            gvEmpPenalty.DataBind();
            ViewState["Select"] = null;
        }
    }





    protected void chkgvSelectAll_CheckedChangedNF(object sender, EventArgs e)
    {
        CheckBox chkSelAll = ((CheckBox)gvEmpNF.HeaderRow.FindControl("chkgvSelectAll"));
        for (int i = 0; i < gvEmpNF.Rows.Count; i++)
        {
            ((CheckBox)gvEmpNF.Rows[i].FindControl("chkgvSelect")).Checked = chkSelAll.Checked;
            if (chkSelAll.Checked)
            {
                if (!lblSelectRecordNF.Text.Split(',').Contains(((Label)(gvEmpNF.Rows[i].FindControl("lblEmpId"))).Text.Trim().ToString()))
                {
                    lblSelectRecordNF.Text += ((Label)(gvEmpNF.Rows[i].FindControl("lblEmpId"))).Text.Trim().ToString() + ",";
                }
            }
            else
            {
                string temp = string.Empty;
                string[] split = lblSelectRecordNF.Text.Split(',');
                foreach (string item in split)
                {
                    if (item != ((Label)(gvEmpNF.Rows[i].FindControl("lblEmpId"))).Text.Trim().ToString())
                    {
                        if (item != "")
                        {
                            temp += item + ",";
                        }
                    }
                }
                lblSelectRecordNF.Text = temp;
            }
        }
    }

    protected void chkgvSelectAll_CheckedChangedPenalty(object sender, EventArgs e)
    {
        CheckBox chkSelAll = ((CheckBox)gvEmpPenalty.HeaderRow.FindControl("chkgvSelectAll"));
        for (int i = 0; i < gvEmpPenalty.Rows.Count; i++)
        {
            ((CheckBox)gvEmpPenalty.Rows[i].FindControl("chkgvSelect")).Checked = chkSelAll.Checked;
            if (chkSelAll.Checked)
            {
                if (!lblSelectRecordPenalty.Text.Split(',').Contains(((Label)(gvEmpPenalty.Rows[i].FindControl("lblEmpId"))).Text.Trim().ToString()))
                {
                    lblSelectRecordPenalty.Text += ((Label)(gvEmpPenalty.Rows[i].FindControl("lblEmpId"))).Text.Trim().ToString() + ",";
                }
            }
            else
            {
                string temp = string.Empty;
                string[] split = lblSelectRecordPenalty.Text.Split(',');
                foreach (string item in split)
                {
                    if (item != ((Label)(gvEmpPenalty.Rows[i].FindControl("lblEmpId"))).Text.Trim().ToString())
                    {
                        if (item != "")
                        {
                            temp += item + ",";
                        }
                    }
                }
                lblSelectRecordPenalty.Text = temp;
            }
        }
    }
    protected void chkgvSelectAll_CheckedChangedOT(object sender, EventArgs e)
    {
        CheckBox chkSelAll = ((CheckBox)gvEmpOT.HeaderRow.FindControl("chkgvSelectAll"));
        for (int i = 0; i < gvEmpOT.Rows.Count; i++)
        {
            ((CheckBox)gvEmpOT.Rows[i].FindControl("chkgvSelect")).Checked = chkSelAll.Checked;
            if (chkSelAll.Checked)
            {
                if (!lblSelectRecordOT.Text.Split(',').Contains(((Label)(gvEmpOT.Rows[i].FindControl("lblEmpId"))).Text.Trim().ToString()))
                {
                    lblSelectRecordOT.Text += ((Label)(gvEmpOT.Rows[i].FindControl("lblEmpId"))).Text.Trim().ToString() + ",";
                }
            }
            else
            {
                string temp = string.Empty;
                string[] split = lblSelectRecordOT.Text.Split(',');
                foreach (string item in split)
                {
                    if (item != ((Label)(gvEmpOT.Rows[i].FindControl("lblEmpId"))).Text.Trim().ToString())
                    {
                        if (item != "")
                        {
                            temp += item + ",";
                        }
                    }
                }
                lblSelectRecordOT.Text = temp;
            }
        }
    }

    protected void chkgvSelectAll_CheckedChangedSal(object sender, EventArgs e)
    {
        CheckBox chkSelAll = ((CheckBox)gvEmpSalary.HeaderRow.FindControl("chkgvSelectAll"));
        for (int i = 0; i < gvEmpSalary.Rows.Count; i++)
        {
            ((CheckBox)gvEmpSalary.Rows[i].FindControl("chkgvSelect")).Checked = chkSelAll.Checked;
            if (chkSelAll.Checked)
            {
                if (!lblSelectRecordSal.Text.Split(',').Contains(((Label)(gvEmpSalary.Rows[i].FindControl("lblEmpId"))).Text.Trim().ToString()))
                {
                    lblSelectRecordSal.Text += ((Label)(gvEmpSalary.Rows[i].FindControl("lblEmpId"))).Text.Trim().ToString() + ",";
                }
            }
            else
            {
                string temp = string.Empty;
                string[] split = lblSelectRecordSal.Text.Split(',');
                foreach (string item in split)
                {
                    if (item != ((Label)(gvEmpSalary.Rows[i].FindControl("lblEmpId"))).Text.Trim().ToString())
                    {
                        if (item != "")
                        {
                            temp += item + ",";
                        }
                    }
                }
                lblSelectRecordSal.Text = temp;
            }
        }
    }
    protected void chkgvSelect_CheckedChangedNF(object sender, EventArgs e)
    {
        string empidlist = string.Empty;
        string temp = string.Empty;
        int index = ((GridViewRow)((CheckBox)sender).Parent.Parent).RowIndex;
        Label lb = (Label)gvEmpNF.Rows[index].FindControl("lblEmpId");
        if (((CheckBox)gvEmpNF.Rows[index].FindControl("chkgvSelect")).Checked)
        {
            empidlist += lb.Text.Trim().ToString() + ",";
            lblSelectRecordNF.Text += empidlist;

        }

        else
        {

            empidlist += lb.Text.ToString().Trim();
            lblSelectRecordNF.Text += empidlist;
            string[] split = lblSelectRecordNF.Text.Split(',');
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
            lblSelectRecordNF.Text = temp;
        }
    }

    protected void chkgvSelect_CheckedChangedPenalty(object sender, EventArgs e)
    {
        string empidlist = string.Empty;
        string temp = string.Empty;
        int index = ((GridViewRow)((CheckBox)sender).Parent.Parent).RowIndex;
        Label lb = (Label)gvEmpPenalty.Rows[index].FindControl("lblEmpId");
        if (((CheckBox)gvEmpPenalty.Rows[index].FindControl("chkgvSelect")).Checked)
        {
            empidlist += lb.Text.Trim().ToString() + ",";
            lblSelectRecordPenalty.Text += empidlist;

        }

        else
        {

            empidlist += lb.Text.ToString().Trim();
            lblSelectRecordPenalty.Text += empidlist;
            string[] split = lblSelectRecordPenalty.Text.Split(',');
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
            lblSelectRecordPenalty.Text = temp;
        }
    }
    protected void chkgvSelect_CheckedChangedOT(object sender, EventArgs e)
    {
        string empidlist = string.Empty;
        string temp = string.Empty;
        int index = ((GridViewRow)((CheckBox)sender).Parent.Parent).RowIndex;
        Label lb = (Label)gvEmpOT.Rows[index].FindControl("lblEmpId");
        if (((CheckBox)gvEmpOT.Rows[index].FindControl("chkgvSelect")).Checked)
        {
            empidlist += lb.Text.Trim().ToString() + ",";
            lblSelectRecordOT.Text += empidlist;

        }

        else
        {

            empidlist += lb.Text.ToString().Trim();
            lblSelectRecordOT.Text += empidlist;
            string[] split = lblSelectRecordOT.Text.Split(',');
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
            lblSelectRecordOT.Text = temp;
        }
    }



    protected void chkgvSelect_CheckedChangedSal(object sender, EventArgs e)
    {
        string empidlist = string.Empty;
        string temp = string.Empty;
        int index = ((GridViewRow)((CheckBox)sender).Parent.Parent).RowIndex;
        Label lb = (Label)gvEmpSalary.Rows[index].FindControl("lblEmpId");
        if (((CheckBox)gvEmpSalary.Rows[index].FindControl("chkgvSelect")).Checked)
        {
            empidlist += lb.Text.Trim().ToString() + ",";
            lblSelectRecordSal.Text += empidlist;

        }

        else
        {

            empidlist += lb.Text.ToString().Trim();
            lblSelectRecordSal.Text += empidlist;
            string[] split = lblSelectRecordSal.Text.Split(',');
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
            lblSelectRecordSal.Text = temp;
        }
    }

    protected void gvEmpSal_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {

        gvEmpSalary.PageIndex = e.NewPageIndex;
        DataTable dtEmp = (DataTable)Session["dtEmpSal"];
        gvEmpSalary.DataSource = dtEmp;
        gvEmpSalary.DataBind();
        string temp = string.Empty;


        for (int i = 0; i < gvEmpSalary.Rows.Count; i++)
        {
            Label lblconid = (Label)gvEmpSalary.Rows[i].FindControl("lblEmpId");
            string[] split = lblSelectRecordSal.Text.Split(',');

            for (int j = 0; j < lblSelectRecordSal.Text.Split(',').Length; j++)
            {
                if (lblSelectRecordSal.Text.Split(',')[j] != "")
                {
                    if (lblconid.Text.Trim().ToString() == lblSelectRecordSal.Text.Split(',')[j].Trim().ToString())
                    {
                        ((CheckBox)gvEmpSalary.Rows[i].FindControl("chkgvSelect")).Checked = true;
                    }
                }
            }
        }
    }




    protected void gvEmpPenalty_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        gvEmpPenalty.PageIndex = e.NewPageIndex;
        DataTable dtEmp = (DataTable)Session["dtEmpPenalty"];
        gvEmpPenalty.DataSource = dtEmp;
        gvEmpPenalty.DataBind();
        string temp = string.Empty;


        for (int i = 0; i < gvEmpPenalty.Rows.Count; i++)
        {
            Label lblconid = (Label)gvEmpPenalty.Rows[i].FindControl("lblEmpId");
            string[] split = lblSelectRecordPenalty.Text.Split(',');

            for (int j = 0; j < lblSelectRecordPenalty.Text.Split(',').Length; j++)
            {
                if (lblSelectRecordPenalty.Text.Split(',')[j] != "")
                {
                    if (lblconid.Text.Trim().ToString() == lblSelectRecordPenalty.Text.Split(',')[j].Trim().ToString())
                    {
                        ((CheckBox)gvEmpPenalty.Rows[i].FindControl("chkgvSelect")).Checked = true;
                    }
                }
            }
        }

    }
    protected void gvEmpOT_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {

        gvEmpOT.PageIndex = e.NewPageIndex;
        DataTable dtEmp = (DataTable)Session["dtEmpOT"];
        gvEmpOT.DataSource = dtEmp;
        gvEmpOT.DataBind();
        string temp = string.Empty;


        for (int i = 0; i < gvEmpOT.Rows.Count; i++)
        {
            Label lblconid = (Label)gvEmpOT.Rows[i].FindControl("lblEmpId");
            string[] split = lblSelectRecordOT.Text.Split(',');

            for (int j = 0; j < lblSelectRecordOT.Text.Split(',').Length; j++)
            {
                if (lblSelectRecordOT.Text.Split(',')[j] != "")
                {
                    if (lblconid.Text.Trim().ToString() == lblSelectRecordOT.Text.Split(',')[j].Trim().ToString())
                    {
                        ((CheckBox)gvEmpOT.Rows[i].FindControl("chkgvSelect")).Checked = true;
                    }
                }
            }
        }
    }
    protected void gvEmpNF_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        gvEmpNF.PageIndex = e.NewPageIndex;
        DataTable dtEmp = (DataTable)Session["dtEmpNF"];
        gvEmpNF.DataSource = dtEmp;
        gvEmpNF.DataBind();
        string temp = string.Empty;
        bool isselcted;

        for (int i = 0; i < gvEmpNF.Rows.Count; i++)
        {
            Label lblconid = (Label)gvEmpNF.Rows[i].FindControl("lblEmpId");
            string[] split = lblSelectRecordNF.Text.Split(',');

            for (int j = 0; j < lblSelectRecordNF.Text.Split(',').Length; j++)
            {
                if (lblSelectRecordNF.Text.Split(',')[j] != "")
                {
                    if (lblconid.Text.Trim().ToString() == lblSelectRecordNF.Text.Split(',')[j].Trim().ToString())
                    {
                        ((CheckBox)gvEmpNF.Rows[i].FindControl("chkgvSelect")).Checked = true;
                    }
                }
            }
        }

    }

    protected void btnEditPenalty_Command(object sender, CommandEventArgs e)
    {
        string empid = e.CommandArgument.ToString();
        hdnEmpIdPenalty.Value = empid;
        string empname = GetEmployeeName(empid);

        lblEmpNamePen.Text = empname;
        lblEmpCodePen.Text = GetEmployeeCode(empid);
        DataTable dtEmpPenalty = objEmpParam.GetEmployeeParameterByEmpId(empid, Session["CompId"].ToString());
        if (dtEmpPenalty.Rows.Count > 0)
        {
            pnlPen1.Visible = true;
            pnlPen2.Visible = true;

            if (Convert.ToBoolean(dtEmpPenalty.Rows[0]["Field1"].ToString()))
            {
                rbtnLateInEnable.Checked = true;
                rbtnLateInDisable.Checked = false;


            }
            else
            {
                rbtnLateInEnable.Checked = false;
                rbtnLateInDisable.Checked = true;


            }

            if (Convert.ToBoolean(dtEmpPenalty.Rows[0]["Field2"].ToString()))
            {

                rbtnEarlyEnable.Checked = true;
                rbtnEarlyDisable.Checked = false;


            }
            else
            {
                rbtnEarlyEnable.Checked = false;
                rbtnEarlyDisable.Checked = true;

            }

            if (Convert.ToBoolean(dtEmpPenalty.Rows[0]["Field3"].ToString()))
            {
                rbtnAbsentEnableP.Checked = true;
                rbtnAbsentDisableP.Checked = false;
            }
            else
            {
                rbtnAbsentEnableP.Checked = false;
                rbtnAbsentDisableP.Checked = true;
            }
        }
        else
        {
            DisplayMessage("No Parameter save for this employee");
            return;

        }
    }
    protected void btnEditOT_Command(object sender, CommandEventArgs e)
    {
        string empid = e.CommandArgument.ToString();
        hdnEmpIdOt.Value = empid;
        string empname = GetEmployeeName(empid);
        lblEmpNameOT.Text = empname;
        lblEmpCodeOT.Text = GetEmployeeCode(empid);
        DataTable dtEmpOt = objEmpParam.GetEmployeeParameterByEmpId(empid, Session["CompId"].ToString());

        if (dtEmpOt.Rows.Count > 0)
        {

            if (Convert.ToBoolean(dtEmpOt.Rows[0]["Is_Partial_Enable"].ToString()))
            {
                rbtnPartialEnable1.Checked = true;
                rbtnPartialDisable1.Checked = false;
                rbtPartial1_OnCheckedChanged(null, null);
            }
            else
            {
                rbtnPartialEnable1.Checked = false;
                rbtnPartialDisable1.Checked = true;
                rbtPartial1_OnCheckedChanged(null, null);
            }

            if (Convert.ToBoolean(dtEmpOt.Rows[0]["Is_Partial_Carry"].ToString()))
            {
                rbtnCarryYes1.Checked = true;
                rbtnCarryNo1.Checked = false;

            }
            else
            {
                rbtnCarryYes1.Checked = false;
                rbtnCarryNo1.Checked = true;
            }


            if (Convert.ToBoolean(dtEmpOt.Rows[0]["Is_OverTime"].ToString()))
            {
                rbtnOTEnable1.Checked = true;
                rbtnOTDisable1.Checked = false;
                rbtOT1_OnCheckedChanged(null, null);
            }
            else
            {
                rbtnOTEnable1.Checked = false;
                rbtnOTDisable1.Checked = true;
                rbtOT1_OnCheckedChanged(null, null);
            }

            ddlOTCalc1.SelectedValue = dtEmpOt.Rows[0]["Normal_OT_Method"].ToString();
            txtNormal1.Text = dtEmpOt.Rows[0]["Normal_OT_Value"].ToString();
            txtWeekOffValue1.Text = dtEmpOt.Rows[0]["Normal_WOT_Value"].ToString();
            txtHolidayValue1.Text = dtEmpOt.Rows[0]["Normal_HOT_Value"].ToString();
            ddlNormalType1.SelectedValue = dtEmpOt.Rows[0]["Normal_OT_Type"].ToString();
            ddlWeekOffType1.SelectedValue = dtEmpOt.Rows[0]["Normal_WOT_Type"].ToString();
            ddlHolidayValue1.SelectedValue = dtEmpOt.Rows[0]["Normal_HOT_Type"].ToString();

            txtTotalMinutesP1.Text = dtEmpOt.Rows[0]["Partial_Leave_Mins"].ToString();
            txtMinuteOTOne.Text = dtEmpOt.Rows[0]["Partial_Leave_Day"].ToString();



            bool IsCompOT = false;
            bool IsPartialComp = false;

            IsCompOT = Convert.ToBoolean(objAppParam.GetApplicationParameterValueByParamName("IsOverTime", Session["CompId"].ToString()));
            IsPartialComp = Convert.ToBoolean(objAppParam.GetApplicationParameterValueByParamName("Partial_Leave_Enable", Session["CompId"].ToString()));


            if (IsCompOT)
            {

                rbtnOTEnable1.Checked = true;
                rbtnOTDisable1.Checked = false;
                rbtOT1_OnCheckedChanged(null, null);


            }
            else
            {

                rbtnOTEnable1.Checked = false;
                rbtnOTDisable1.Checked = true;
                rbtOT1_OnCheckedChanged(null, null);
                rbtnOTEnable1.Enabled = false;
                rbtnOTDisable1.Enabled = false;

            }

            if (IsPartialComp)
            {

                rbtnPartialEnable1.Checked = true;
                rbtnPartialDisable1.Checked = false;
                rbtPartial1_OnCheckedChanged(null, null);


            }
            else
            {

                rbtnPartialEnable1.Checked = false;
                rbtnPartialDisable1.Checked = true;
                rbtPartial1_OnCheckedChanged(null, null);

                rbtnPartialEnable1.Enabled = false;
                rbtnPartialDisable1.Enabled = false;

            }
            pnlOT1.Visible = true;
            pnlOT2.Visible = true;
        }
        else
        {
            DisplayMessage("No Parameter save for this employee");
            return;
        }

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
    protected void lbxGroupPenalty_SelectedIndexChanged(object sender, EventArgs e)
    {
        string GroupIds = string.Empty;
        string EmpIds = string.Empty;
        for (int i = 0; i < lbxGroupPenalty.Items.Count; i++)
        {
            if (lbxGroupPenalty.Items[i].Selected == true)
            {
                GroupIds += lbxGroupPenalty.Items[i].Value + ",";

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
                Session["dtEmp10"] = dtEmp;
                gvEmployeePenalty.DataSource = dtEmp;
                gvEmployeePenalty.DataBind();

            }
            else
            {
                Session["dtEmp10"] = null;
                gvEmployeePenalty.DataSource = null;
                gvEmployeePenalty.DataBind();
            }

        }
        else
        {
            gvEmployeePenalty.DataSource = null;
            gvEmployeePenalty.DataBind();

        }

    }

    protected void lbxGroupOT_SelectedIndexChanged(object sender, EventArgs e)
    {
        string GroupIds = string.Empty;
        string EmpIds = string.Empty;
        for (int i = 0; i < lbxGroupOT.Items.Count; i++)
        {
            if (lbxGroupOT.Items[i].Selected == true)
            {
                GroupIds += lbxGroupOT.Items[i].Value + ",";

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
                Session["dtEmp5"] = dtEmp;
                gvEmployeeOT.DataSource = dtEmp;
                gvEmployeeOT.DataBind();

            }
            else
            {
                Session["dtEmp5"] = null;
                gvEmployeeOT.DataSource = null;
                gvEmployeeOT.DataBind();
            }

        }
        else
        {
            gvEmployeeOT.DataSource = null;
            gvEmployeeOT.DataBind();

        }
    }

    protected void lbxGroupSal_SelectedIndexChanged(object sender, EventArgs e)
    {
        string GroupIds = string.Empty;
        string EmpIds = string.Empty;
        for (int i = 0; i < lbxGroupSal.Items.Count; i++)
        {
            if (lbxGroupSal.Items[i].Selected == true)
            {
                GroupIds += lbxGroupSal.Items[i].Value + ",";

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
                Session["dtEmp4"] = dtEmp;
                gvEmployeeSal.DataSource = dtEmp;
                gvEmployeeSal.DataBind();

            }
            else
            {
                Session["dtEmp4"] = null;
                gvEmployeeSal.DataSource = null;
                gvEmployeeSal.DataBind();
            }
        }
        else
        {
            gvEmployeeSal.DataSource = null;
            gvEmployeeSal.DataBind();

        }
    }
    protected void lbxGroupNF_SelectedIndexChanged(object sender, EventArgs e)
    {
        string GroupIds = string.Empty;
        string EmpIds = string.Empty;
        for (int i = 0; i < lbxGroupNF.Items.Count; i++)
        {
            if (lbxGroupNF.Items[i].Selected == true)
            {
                GroupIds += lbxGroupNF.Items[i].Value + ",";

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
                Session["dtEmp2"] = dtEmp;
                gvEmployeeNF.DataSource = dtEmp;
                gvEmployeeNF.DataBind();

            }
            else
            {
                Session["dtEmp2"] = null;
                gvEmployeeNF.DataSource = null;
                gvEmployeeNF.DataBind();
            }

        }
        else
        {
            gvEmployeeNF.DataSource = null;
            gvEmployeeNF.DataBind();

        }
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
        gridEmpLeave.DataSource = null;
        gridEmpLeave.DataBind();

    }
    protected void EmpGroupNF_CheckedChanged(object sender, EventArgs e)
    {
        if (rbtnGroupNF.Checked)
        {
            pnlEmpNf.Visible = false;
            pnlGroupNF.Visible = true;

            DataTable dtGroup = objEmpGroup.GetEmployeeGroup_Master(Session["CompId"].ToString());
            dtGroup = new DataView(dtGroup, "Brand_Id='" + Session["BrandId"].ToString() + "'  and Location_Id='" + Session["LocId"].ToString() + "'", "", DataViewRowState.CurrentRows).ToTable();
            if (dtGroup.Rows.Count > 0)
            {
                lbxGroupNF.DataSource = dtGroup;
                lbxGroupNF.DataTextField = "Group_Name";
                lbxGroupNF.DataValueField = "Group_Id";

                lbxGroupNF.DataBind();

            }
            gvEmployeeNF.DataSource = null;
            gvEmployeeNF.DataBind();


            lbxGroupNF_SelectedIndexChanged(null, null);
        }
        else if (rbtnEmpNF.Checked)
        {
            pnlEmpNf.Visible = true;
            pnlGroupNF.Visible = false;

            lblEmp.Text = "";
            string GroupIds = string.Empty;
            string EmpIds = string.Empty;
            DataTable dtEmp = objEmp.GetEmployeeMaster(Session["CompId"].ToString());

            dtEmp = new DataView(dtEmp, "Brand_Id='" + Session["BrandId"].ToString() + "'  and Location_Id='" + Session["LocId"].ToString() + "'", "", DataViewRowState.CurrentRows).ToTable();




            if (dtEmp.Rows.Count > 0)
            {
                Session["dtEmp2"] = dtEmp;
                gvEmployeeNF.DataSource = dtEmp;
                gvEmployeeNF.DataBind();





            }

        }


    }



    protected void EmpGroupSal_CheckedChanged(object sender, EventArgs e)
    {
        if (rbtnGroupSal.Checked)
        {
            pnlEmpSal.Visible = false;
            pnlGroupSal.Visible = true;

            DataTable dtGroup = objEmpGroup.GetEmployeeGroup_Master(Session["CompId"].ToString());
            dtGroup = new DataView(dtGroup, "Brand_Id='" + Session["BrandId"].ToString() + "'  and Location_Id='" + Session["LocId"].ToString() + "'", "", DataViewRowState.CurrentRows).ToTable();
            if (dtGroup.Rows.Count > 0)
            {
                lbxGroupSal.DataSource = dtGroup;
                lbxGroupSal.DataTextField = "Group_Name";
                lbxGroupSal.DataValueField = "Group_Id";

                lbxGroupSal.DataBind();

            }
            gvEmployeeSal.DataSource = null;
            gvEmployeeSal.DataBind();


            lbxGroupSal_SelectedIndexChanged(null, null);
        }
        else if (rbtnEmpSal.Checked)
        {
            pnlEmpSal.Visible = true;
            pnlGroupSal.Visible = false;

            lblEmp.Text = "";
            string GroupIds = string.Empty;
            string EmpIds = string.Empty;
            DataTable dtEmp = objEmp.GetEmployeeMaster(Session["CompId"].ToString());

            dtEmp = new DataView(dtEmp, "Brand_Id='" + Session["BrandId"].ToString() + "'  and Location_Id='" + Session["LocId"].ToString() + "'", "", DataViewRowState.CurrentRows).ToTable();




            if (dtEmp.Rows.Count > 0)
            {
                Session["dtEmp4"] = dtEmp;
                gvEmployeeSal.DataSource = dtEmp;
                gvEmployeeSal.DataBind();





            }

        }


    }

    protected void EmpPenalty_CheckedChanged(object sender, EventArgs e)
    {
        if (rbtnPenaltyGroup.Checked)
        {
            PnlPenaltyEmp.Visible = false;
            PnlGroupPenalty.Visible = true;

            DataTable dtGroup = objEmpGroup.GetEmployeeGroup_Master(Session["CompId"].ToString());
            dtGroup = new DataView(dtGroup, "Brand_Id='" + Session["BrandId"].ToString() + "'  and Location_Id='" + Session["LocId"].ToString() + "'", "", DataViewRowState.CurrentRows).ToTable();
            if (dtGroup.Rows.Count > 0)
            {
                lbxGroupPenalty.DataSource = dtGroup;
                lbxGroupPenalty.DataTextField = "Group_Name";
                lbxGroupPenalty.DataValueField = "Group_Id";

                lbxGroupPenalty.DataBind();

            }
            gvEmployeePenalty.DataSource = null;
            gvEmployeePenalty.DataBind();


            lbxGroupPenalty_SelectedIndexChanged(null, null);
        }
        else if (rbtnPenaltyEmp.Checked)
        {
            PnlPenaltyEmp.Visible = true;
            PnlGroupPenalty.Visible = false;

            lblEmp.Text = "";
            string GroupIds = string.Empty;
            string EmpIds = string.Empty;
            DataTable dtEmp = objEmp.GetEmployeeMaster(Session["CompId"].ToString());

            dtEmp = new DataView(dtEmp, "Brand_Id='" + Session["BrandId"].ToString() + "'  and Location_Id='" + Session["LocId"].ToString() + "'", "", DataViewRowState.CurrentRows).ToTable();




            if (dtEmp.Rows.Count > 0)
            {
                Session["dtEmpPenalty"] = dtEmp;
                gvEmployeePenalty.DataSource = dtEmp;
                gvEmployeePenalty.DataBind();





            }

        }
    }

    protected void EmpGroupOT_CheckedChanged(object sender, EventArgs e)
    {
        if (rbtnGroupOT.Checked)
        {
            pnlEmpOT.Visible = false;
            pnlGroupOT.Visible = true;

            DataTable dtGroup = objEmpGroup.GetEmployeeGroup_Master(Session["CompId"].ToString());
            dtGroup = new DataView(dtGroup, "Brand_Id='" + Session["BrandId"].ToString() + "'  and Location_Id='" + Session["LocId"].ToString() + "'", "", DataViewRowState.CurrentRows).ToTable();
            if (dtGroup.Rows.Count > 0)
            {
                lbxGroupOT.DataSource = dtGroup;
                lbxGroupOT.DataTextField = "Group_Name";
                lbxGroupOT.DataValueField = "Group_Id";

                lbxGroupOT.DataBind();

            }
            gvEmployeeOT.DataSource = null;
            gvEmployeeOT.DataBind();


            lbxGroupOT_SelectedIndexChanged(null, null);
        }
        else if (rbtnEmpOT.Checked)
        {
            pnlEmpOT.Visible = true;
            pnlGroupOT.Visible = false;

            lblEmp.Text = "";
            string GroupIds = string.Empty;
            string EmpIds = string.Empty;
            DataTable dtEmp = objEmp.GetEmployeeMaster(Session["CompId"].ToString());

            dtEmp = new DataView(dtEmp, "Brand_Id='" + Session["BrandId"].ToString() + "'  and Location_Id='" + Session["LocId"].ToString() + "'", "", DataViewRowState.CurrentRows).ToTable();




            if (dtEmp.Rows.Count > 0)
            {
                Session["dtEmp5"] = dtEmp;
                gvEmployeeOT.DataSource = dtEmp;
                gvEmployeeOT.DataBind();





            }

        }


    }
    protected void btnEditSalary_Command(object sender, CommandEventArgs e)
    {
        string empid = e.CommandArgument.ToString();
        hdnEmpIdSal.Value = empid;
        string empname = GetEmployeeName(empid);
        lblEmpNameSal.Text = empname;
        lblEmpCodeSal.Text = GetEmployeeCode(empid);
        DataTable dtEmpSal = objEmpParam.GetEmployeeParameterByEmpId(empid, Session["CompId"].ToString());

        if (dtEmpSal.Rows.Count > 0)
        {
            txtBasic1.Text = dtEmpSal.Rows[0]["Basic_Salary"].ToString();
            try
            {
                ddlPaymentType1.SelectedValue = dtEmpSal.Rows[0]["Salary_Type"].ToString();
            }
            catch
            {
            }
            try
            {
                ddlCurrency1.SelectedValue = dtEmpSal.Rows[0]["Currency_Id"].ToString();
            }
            catch
            {

            }

            txtWorkMin1.Text = dtEmpSal.Rows[0]["Assign_Min"].ToString();

            try
            {
                ddlWorkCal1.SelectedValue = dtEmpSal.Rows[0]["Effective_Work_Cal_Method"].ToString();
            }
            catch
            {

            }







            if (Convert.ToBoolean(dtEmpSal.Rows[0]["Field6"].ToString()))
            {
                chkEmpINPayroll1.Checked = true;
                chkEmpPf1.Enabled = true;
                chkEmpEsic1.Enabled = true;
                chkEmpPf1.Checked = Convert.ToBoolean(dtEmpSal.Rows[0]["Field4"].ToString());
                chkEmpEsic1.Checked = Convert.ToBoolean(dtEmpSal.Rows[0]["Field5"].ToString());
            }
            else
            {
                chkEmpINPayroll1.Checked = false;
                chkEmpPf1.Enabled = false;
                chkEmpEsic1.Enabled = false;

                chkEmpPf1.Checked = false;
                chkEmpEsic1.Checked = false;
            }




            pnlSal1.Visible = true;
            pnlSal2.Visible = true;
        }
        else
        {
            DisplayMessage("No Parameter save for this employee");
            return;
        }
    }

    protected void btnEditNF_Command(object sender, CommandEventArgs e)
    {
        string empid = e.CommandArgument.ToString();
        hdnEmpIdNF.Value = empid;
        string empname = GetEmployeeName(empid);
        lblEmpNameNf.Text = empname;
        lblEmpCodeNF.Text = GetEmployeeCode(empid);
        DataTable dtEmpNF = objEmpNotice.GetEmployeeNotificationByEmpId(Session["CompId"].ToString(), empid);

        if (dtEmpNF.Rows.Count > 0)
        {
            chkSMSDocExp1.Checked = Convert.ToBoolean(dtEmpNF.Rows[0]["Is_SMS_Document_Expired"].ToString());
            chkSMSAbsent1.Checked = Convert.ToBoolean(dtEmpNF.Rows[0]["Is_SMS_Absent"].ToString());
            chkSMSLate1.Checked = Convert.ToBoolean(dtEmpNF.Rows[0]["Is_SMS_Late"].ToString());
            chkSMSEarly1.Checked = Convert.ToBoolean(dtEmpNF.Rows[0]["Is_SMS_Early"].ToString());
            ChkSmsLeave1.Checked = Convert.ToBoolean(dtEmpNF.Rows[0]["Is_SMS_LeaveApproved"].ToString());
            ChkSMSPartial1.Checked = Convert.ToBoolean(dtEmpNF.Rows[0]["Is_SMS_Partial_Fault"].ToString());
            chkSMSNoClock1.Checked = Convert.ToBoolean(dtEmpNF.Rows[0]["Is_SMS_NoClock"].ToString());
            ChkRptAbsent1.Checked = Convert.ToBoolean(dtEmpNF.Rows[0]["Is_Rpt_Absent"].ToString());
            chkRptLate1.Checked = Convert.ToBoolean(dtEmpNF.Rows[0]["Is_Rpt_Late"].ToString());
            chkRptEarly1.Checked = Convert.ToBoolean(dtEmpNF.Rows[0]["Is_Rpt_Early"].ToString());
            ChkRptInOut1.Checked = Convert.ToBoolean(dtEmpNF.Rows[0]["Is_Rpt_Attendance"].ToString());
            ChkRptSalary1.Checked = Convert.ToBoolean(dtEmpNF.Rows[0]["Is_Rpt_Salary"].ToString());
            ChkRptOvertime1.Checked = Convert.ToBoolean(dtEmpNF.Rows[0]["Is_Rpt_OverTime"].ToString());
            ChkRptLog1.Checked = Convert.ToBoolean(dtEmpNF.Rows[0]["Is_Rpt_Log"].ToString());
            chkRptDoc1.Checked = Convert.ToBoolean(dtEmpNF.Rows[0]["Is_Rpt_DocumentExp"].ToString());
            chkRptViolation1.Checked = Convert.ToBoolean(dtEmpNF.Rows[0]["Is_Rpt_Violation"].ToString());




            pnlNotice1.Visible = true;
            pnlNotice2.Visible = true;
        }
        else
        {
            DisplayMessage("No alert save for this employee");
            return;
        }

    }


    protected void btnOTPartial_Click(object sender, EventArgs e)
    {
        pnlPanelty.BackColor = System.Drawing.ColorTranslator.FromHtml("#90bde9");
        PnlEmpPenalty.Visible = false;
        pnlSalary.BackColor = System.Drawing.ColorTranslator.FromHtml("#90bde9");
        pnlMenuBin.BackColor = System.Drawing.ColorTranslator.FromHtml("#90bde9");

        pnlMenuNew.BackColor = System.Drawing.ColorTranslator.FromHtml("#90bde9");
        pnlMenuList.BackColor = System.Drawing.ColorTranslator.FromHtml("#90bde9");
        pnlLeave.BackColor = System.Drawing.ColorTranslator.FromHtml("#90bde9");
        pnlNotice.BackColor = System.Drawing.ColorTranslator.FromHtml("#90bde9");
        PnlEmpNotification.Visible = false;
        pnlNewEdit.Visible = false;
        pnlBin.Visible = false;
        pnlList.Visible = false;
        PnlEmpSalary.Visible = false;
        pnlOTPartial.BackColor = System.Drawing.ColorTranslator.FromHtml("#ccddee");
        pnlOTPL.Visible = true;
        pnlUpload.BackColor = System.Drawing.ColorTranslator.FromHtml("#90bde9");


        pnlEmpUpload.Visible = false;
        PnlEmployeeLeave.Visible = false;
        ViewState["Select"] = null;

        ImgbtnSelectAll.Visible = false;
        imgBtnRestore.Visible = false;
        dtlistbinEmp.Visible = false;
        gvBinEmp.Visible = false;

        ViewState["CurrIndexbin"] = 0;
        ViewState["SubSizebin"] = 9;
        lnkbinFirst.Visible = false;
        lnkbinPrev.Visible = false;
        lnkbinNext.Visible = false;
        lnkbinLast.Visible = false;

        lnkFirst.Visible = false;
        lnkLast.Visible = false;
        lnkNext.Visible = false;
        lnkPrev.Visible = false;


        imgBtnbinGrid.Visible = false;


        imgBtnbinDatalist.Visible = false;

        lblSelectRecordNF.Text = "";
        gvEmpLeave.Visible = false;
        gvEmpSalary.Visible = false;
        gvEmpOT.Visible = true;
        DataTable dtEmp = objEmp.GetEmployeeMaster(Session["CompId"].ToString());

        dtEmp = new DataView(dtEmp, "Brand_Id='" + Session["BrandId"].ToString() + "'  and Location_Id='" + Session["LocId"].ToString() + "'", "", DataViewRowState.CurrentRows).ToTable();

        if (Session["SessionDepId"] != null)
        {
            dtEmp = new DataView(dtEmp, "Department_Id in(" + Session["SessionDepId"].ToString().Substring(0, Session["SessionDepId"].ToString().Length - 1) + ")", "", DataViewRowState.CurrentRows).ToTable();

        }
        if (dtEmp.Rows.Count > 0)
        {
            Session["dtEmpOT"] = dtEmp;
            gvEmpOT.DataSource = dtEmp;
            gvEmpOT.DataBind();
            lblTotalRecordOT.Text = Resources.Attendance.Total_Records + " : " + dtEmp.Rows.Count.ToString() + "";

        }
        rbtnEmpOT.Checked = true;
        rbtnGroupOT.Checked = false;
        EmpGroupOT_CheckedChanged(null, null);

        if (Convert.ToBoolean(objAppParam.GetApplicationParameterValueByParamName("Partial_Leave_Enable", Session["CompId"].ToString())))
        {
            rbtnPartialEnable.Checked = true;
            rbtPartial_OnCheckedChanged(null, null);

            txtTotalMinutes.Text = objAppParam.GetApplicationParameterValueByParamName("Total Partial Leave Minutes", Session["CompId"].ToString());
            txtMinuteday.Text = objAppParam.GetApplicationParameterValueByParamName("Partial Leave Minute Use In A Day", Session["CompId"].ToString());
            if (Convert.ToBoolean(objAppParam.GetApplicationParameterValueByParamName("Carry Forward Partial Leave Minutes", Session["CompId"].ToString())))
            {
                rbtnCarryYes.Checked = true;
                rbtnCarryNo.Checked = false;

            }
            else
            {
                rbtnCarryYes.Checked = false;
                rbtnCarryNo.Checked = true;
            }


        }
        else
        {
            rbtnPartialEnable.Checked = false;
            rbtnPartialDisable.Checked = true;

            rbtPartial_OnCheckedChanged(null, null);
            txtTotalMinutes.Text = objAppParam.GetApplicationParameterValueByParamName("Total Partial Leave Minutes", Session["CompId"].ToString());
            txtMinuteday.Text = objAppParam.GetApplicationParameterValueByParamName("Partial Leave Minute Use In A Day", Session["CompId"].ToString());
            if (Convert.ToBoolean(objAppParam.GetApplicationParameterValueByParamName("Carry Forward Partial Leave Minutes", Session["CompId"].ToString())))
            {
                rbtnCarryYes.Checked = true;
                rbtnCarryNo.Checked = false;

            }
            else
            {
                rbtnCarryYes.Checked = false;
                rbtnCarryNo.Checked = true;
            }
            rbtnPartialEnable.Enabled = false;
            rbtnPartialDisable.Enabled = true;
        }

    }

    protected void btnPanelty_Click(object sender, EventArgs e)
    {
        pnlPanelty.BackColor = System.Drawing.ColorTranslator.FromHtml("#ccddee");
        pnlSalary.BackColor = System.Drawing.ColorTranslator.FromHtml("#90bde9");
        pnlMenuBin.BackColor = System.Drawing.ColorTranslator.FromHtml("#90bde9");

        pnlMenuNew.BackColor = System.Drawing.ColorTranslator.FromHtml("#90bde9");
        pnlMenuList.BackColor = System.Drawing.ColorTranslator.FromHtml("#90bde9");
        pnlLeave.BackColor = System.Drawing.ColorTranslator.FromHtml("#90bde9");
        pnlNotice.BackColor = System.Drawing.ColorTranslator.FromHtml("#90bde9");
        pnlOTPartial.BackColor = System.Drawing.ColorTranslator.FromHtml("#90bde9");
        pnlUpload.BackColor = System.Drawing.ColorTranslator.FromHtml("#90bde9");
        pnlPanelty.BackColor = System.Drawing.ColorTranslator.FromHtml("#ccddee");
        PnlEmpPenalty.Visible = true;
        pnlEmpUpload.Visible = false;

        pnlOTPL.Visible = false;

        PnlEmpNotification.Visible = false;
        pnlNewEdit.Visible = false;
        pnlBin.Visible = false;
        pnlList.Visible = false;
        PnlEmpSalary.Visible = false;

        PnlEmployeeLeave.Visible = false;
        ViewState["Select"] = null;

        ImgbtnSelectAll.Visible = false;
        imgBtnRestore.Visible = false;
        dtlistbinEmp.Visible = false;
        gvBinEmp.Visible = false;

        ViewState["CurrIndexbin"] = 0;
        ViewState["SubSizebin"] = 9;
        lnkbinFirst.Visible = false;
        lnkbinPrev.Visible = false;
        lnkbinNext.Visible = false;
        lnkbinLast.Visible = false;

        lnkFirst.Visible = false;
        lnkLast.Visible = false;
        lnkNext.Visible = false;
        lnkPrev.Visible = false;


        imgBtnbinGrid.Visible = false;


        imgBtnbinDatalist.Visible = false;

        lblSelectRecordNF.Text = "";
        gvEmpLeave.Visible = false;
        gvEmpSalary.Visible = false;
        gvEmpPenalty.Visible = true;
        DataTable dtEmp = objEmp.GetEmployeeMaster(Session["CompId"].ToString());

        dtEmp = new DataView(dtEmp, "Brand_Id='" + Session["BrandId"].ToString() + "'  and Location_Id='" + Session["LocId"].ToString() + "'", "", DataViewRowState.CurrentRows).ToTable();

        if (Session["SessionDepId"] != null)
        {
            dtEmp = new DataView(dtEmp, "Department_Id in(" + Session["SessionDepId"].ToString().Substring(0, Session["SessionDepId"].ToString().Length - 1) + ")", "", DataViewRowState.CurrentRows).ToTable();

        }
        if (dtEmp.Rows.Count > 0)
        {
            Session["dtEmpPenalty"] = dtEmp;
            gvEmpPenalty.DataSource = dtEmp;
            gvEmpPenalty.DataBind();
            lblTotalRecordPenalty.Text = Resources.Attendance.Total_Records + " : " + dtEmp.Rows.Count.ToString() + "";

        }
        rbtnPenaltyEmp.Checked = true;
        rbtnPenaltyGroup.Checked = false;
        EmpPenalty_CheckedChanged(null, null);

        rbtLateInEnable.Checked = true;
        rbtLateInDisable.Checked = false;




        rbtEarlyOutEnable.Checked = true;
        rbtEarlyOutDisable.Checked = false;
        rbtnAbsentDisable.Checked = false;
        rbtnAbsentEnable.Checked = true;



    }
    protected void btnUpload_Click(object sender, EventArgs e)
    {
        pnlSalary.BackColor = System.Drawing.ColorTranslator.FromHtml("#90bde9");
        pnlMenuBin.BackColor = System.Drawing.ColorTranslator.FromHtml("#90bde9");

        pnlMenuNew.BackColor = System.Drawing.ColorTranslator.FromHtml("#90bde9");
        pnlMenuList.BackColor = System.Drawing.ColorTranslator.FromHtml("#90bde9");
        pnlLeave.BackColor = System.Drawing.ColorTranslator.FromHtml("#90bde9");
        pnlNotice.BackColor = System.Drawing.ColorTranslator.FromHtml("#90bde9");
        pnlOTPartial.BackColor = System.Drawing.ColorTranslator.FromHtml("#90bde9");
        pnlUpload.BackColor = System.Drawing.ColorTranslator.FromHtml("#ccddee");
        pnlPanelty.BackColor = System.Drawing.ColorTranslator.FromHtml("#90bde9");
        PnlEmpPenalty.Visible = false;

        pnlEmpUpload.Visible = true;
        pnlOTPL.Visible = false;

        PnlEmpNotification.Visible = false;
        pnlNewEdit.Visible = false;
        pnlBin.Visible = false;
        pnlList.Visible = false;
        PnlEmpSalary.Visible = false;

        PnlEmployeeLeave.Visible = false;
        ViewState["Select"] = null;

        ImgbtnSelectAll.Visible = false;
        imgBtnRestore.Visible = false;
        dtlistbinEmp.Visible = false;
        gvBinEmp.Visible = false;

        ViewState["CurrIndexbin"] = 0;
        ViewState["SubSizebin"] = 9;
        lnkbinFirst.Visible = false;
        lnkbinPrev.Visible = false;
        lnkbinNext.Visible = false;
        lnkbinLast.Visible = false;

        lnkFirst.Visible = false;
        lnkLast.Visible = false;
        lnkNext.Visible = false;
        lnkPrev.Visible = false;


        imgBtnbinGrid.Visible = false;


        imgBtnbinDatalist.Visible = false;

        lblSelectRecordNF.Text = "";
        gvEmpLeave.Visible = false;
        gvEmpSalary.Visible = false;


    }




    protected void chkEmpINPayroll_OnCheckedChanged(object sender, EventArgs e)
    {
        if (chkEmpINPayroll.Checked)
        {
            chkEmpPf.Enabled = true;
            chkEmpEsic.Enabled = true;
            chkEmpPf.Checked = false;
            chkEmpEsic.Checked = false;
        }
        else
        {
            chkEmpPf.Enabled = false;
            chkEmpEsic.Enabled = false;
            chkEmpPf.Checked = false;
            chkEmpEsic.Checked = false;
        }

    }




    protected void chkEmpINPayroll_OnCheckedChanged1(object sender, EventArgs e)
    {
        if (chkEmpINPayroll1.Checked)
        {
            chkEmpPf1.Enabled = true;
            chkEmpEsic1.Enabled = true;
            chkEmpPf1.Checked = false;
            chkEmpEsic1.Checked = false;
        }
        else
        {
            chkEmpPf1.Enabled = false;
            chkEmpEsic1.Enabled = false;
            chkEmpPf1.Checked = false;
            chkEmpEsic1.Checked = false;
        }

    }

    protected void btnSalary_Click(object sender, EventArgs e)
    {
        chkEmpINPayroll.Checked = true;
        chkEmpPf.Enabled = true;
        chkEmpEsic.Enabled = true;
        pnlUpload.BackColor = System.Drawing.ColorTranslator.FromHtml("#90bde9");

        pnlPanelty.BackColor = System.Drawing.ColorTranslator.FromHtml("#90bde9");
        PnlEmpPenalty.Visible = false;
        pnlEmpUpload.Visible = false;
        pnlSalary.BackColor = System.Drawing.ColorTranslator.FromHtml("#ccddee");
        pnlMenuBin.BackColor = System.Drawing.ColorTranslator.FromHtml("#90bde9");

        pnlMenuNew.BackColor = System.Drawing.ColorTranslator.FromHtml("#90bde9");
        pnlMenuList.BackColor = System.Drawing.ColorTranslator.FromHtml("#90bde9");
        pnlLeave.BackColor = System.Drawing.ColorTranslator.FromHtml("#90bde9");
        pnlNotice.BackColor = System.Drawing.ColorTranslator.FromHtml("#90bde9");
        pnlOTPartial.BackColor = System.Drawing.ColorTranslator.FromHtml("#90bde9");
        pnlOTPL.Visible = false;

        PnlEmpNotification.Visible = false;
        pnlNewEdit.Visible = false;
        pnlBin.Visible = false;
        pnlList.Visible = false;
        PnlEmpSalary.Visible = true;

        PnlEmployeeLeave.Visible = false;
        ViewState["Select"] = null;

        ImgbtnSelectAll.Visible = false;
        imgBtnRestore.Visible = false;
        dtlistbinEmp.Visible = false;
        gvBinEmp.Visible = false;

        ViewState["CurrIndexbin"] = 0;
        ViewState["SubSizebin"] = 9;
        lnkbinFirst.Visible = false;
        lnkbinPrev.Visible = false;
        lnkbinNext.Visible = false;
        lnkbinLast.Visible = false;

        lnkFirst.Visible = false;
        lnkLast.Visible = false;
        lnkNext.Visible = false;
        lnkPrev.Visible = false;


        imgBtnbinGrid.Visible = false;


        imgBtnbinDatalist.Visible = false;

        lblSelectRecordNF.Text = "";
        gvEmpLeave.Visible = false;
        gvEmpSalary.Visible = true;
        DataTable dtEmp = objEmp.GetEmployeeMaster(Session["CompId"].ToString());

        dtEmp = new DataView(dtEmp, "Brand_Id='" + Session["BrandId"].ToString() + "'  and Location_Id='" + Session["LocId"].ToString() + "'", "", DataViewRowState.CurrentRows).ToTable();

        if (Session["SessionDepId"] != null)
        {
            dtEmp = new DataView(dtEmp, "Department_Id in(" + Session["SessionDepId"].ToString().Substring(0, Session["SessionDepId"].ToString().Length - 1) + ")", "", DataViewRowState.CurrentRows).ToTable();

        }
        if (dtEmp.Rows.Count > 0)
        {
            Session["dtEmpSal"] = dtEmp;
            gvEmpSalary.DataSource = dtEmp;
            gvEmpSalary.DataBind();
            lblTotalRecordSal.Text = Resources.Attendance.Total_Records + " : " + dtEmp.Rows.Count.ToString() + "";

        }
        rbtnEmpSal.Checked = true;
        rbtnGroupSal.Checked = false;
        EmpGroupSal_CheckedChanged(null, null);
    }


    protected void btnNotice_Click(object sender, EventArgs e)
    {
        pnlUpload.BackColor = System.Drawing.ColorTranslator.FromHtml("#90bde9");
        pnlPanelty.BackColor = System.Drawing.ColorTranslator.FromHtml("#90bde9");
        PnlEmpPenalty.Visible = false;

        pnlEmpUpload.Visible = false;
        pnlOTPartial.BackColor = System.Drawing.ColorTranslator.FromHtml("#90bde9");
        pnlOTPL.Visible = false;
        pnlMenuBin.BackColor = System.Drawing.ColorTranslator.FromHtml("#90bde9");

        pnlMenuNew.BackColor = System.Drawing.ColorTranslator.FromHtml("#90bde9");
        pnlMenuList.BackColor = System.Drawing.ColorTranslator.FromHtml("#90bde9");
        pnlLeave.BackColor = System.Drawing.ColorTranslator.FromHtml("#90bde9");
        pnlNotice.BackColor = System.Drawing.ColorTranslator.FromHtml("#ccddee");
        pnlSalary.BackColor = System.Drawing.ColorTranslator.FromHtml("#90bde9");


        gvEmpSalary.Visible = false;
        PnlEmpSalary.Visible = false;

        PnlEmpNotification.Visible = true;
        pnlNewEdit.Visible = false;
        pnlBin.Visible = false;
        pnlList.Visible = false;





        PnlEmployeeLeave.Visible = false;
        ViewState["Select"] = null;

        ImgbtnSelectAll.Visible = false;
        imgBtnRestore.Visible = false;
        dtlistbinEmp.Visible = false;
        gvBinEmp.Visible = false;

        ViewState["CurrIndexbin"] = 0;
        ViewState["SubSizebin"] = 9;
        lnkbinFirst.Visible = false;
        lnkbinPrev.Visible = false;
        lnkbinNext.Visible = false;
        lnkbinLast.Visible = false;

        lnkFirst.Visible = false;
        lnkLast.Visible = false;
        lnkNext.Visible = false;
        lnkPrev.Visible = false;


        imgBtnbinGrid.Visible = false;


        imgBtnbinDatalist.Visible = false;

        lblSelectRecordNF.Text = "";
        gvEmpLeave.Visible = false;
        gvEmpNF.Visible = true;
        DataTable dtEmp = objEmp.GetEmployeeMaster(Session["CompId"].ToString());

        dtEmp = new DataView(dtEmp, "Brand_Id='" + Session["BrandId"].ToString() + "'  and Location_Id='" + Session["LocId"].ToString() + "'", "", DataViewRowState.CurrentRows).ToTable();

        if (Session["SessionDepId"] != null)
        {
            dtEmp = new DataView(dtEmp, "Department_Id in(" + Session["SessionDepId"].ToString().Substring(0, Session["SessionDepId"].ToString().Length - 1) + ")", "", DataViewRowState.CurrentRows).ToTable();

        }
        if (dtEmp.Rows.Count > 0)
        {
            Session["dtEmpNF"] = dtEmp;
            gvEmpNF.DataSource = dtEmp;
            gvEmpNF.DataBind();
            lblTotalRecordNF.Text = Resources.Attendance.Total_Records + " : " + dtEmp.Rows.Count.ToString() + "";

        }





        rbtnEmpNF.Checked = true;
        rbtnGroupNF.Checked = false;
        EmpGroupNF_CheckedChanged(null, null);

    }





    protected void btnUpdatePenalty_Click(object sender, EventArgs e)
    {
        int b = 0;
        DataTable dt = objEmpParam.GetEmployeeParameterByEmpId(hdnEmpIdPenalty.Value, Session["CompId"].ToString());

        objEmpParam.DeleteEmployeeParameterByEmpId(hdnEmpIdPenalty.Value);

        bool IsLate = false;
        bool IsEarly = false;
        bool IsAbsent = false;
        if (rbtnLateInEnable.Checked)
        {
            IsLate = true;

        }

        if (rbtnEarlyEnable.Checked)
        {
            IsEarly = true;

        }

        if (rbtnAbsentEnableP.Checked)
        {
            IsAbsent = true;
        }
        b = objEmpParam.InsertEmployeeParameter(Session["CompId"].ToString(), hdnEmpIdPenalty.Value, dt.Rows[0]["Basic_Salary"].ToString(), dt.Rows[0]["Salary_Type"].ToString(), dt.Rows[0]["Currency_Id"].ToString(), dt.Rows[0]["Assign_Min"].ToString(), dt.Rows[0]["Effective_Work_Cal_Method"].ToString(), dt.Rows[0]["Is_OverTime"].ToString(), dt.Rows[0]["Normal_OT_Method"].ToString(), dt.Rows[0]["Normal_OT_Type"].ToString(), dt.Rows[0]["Normal_OT_Value"].ToString(), dt.Rows[0]["Normal_HOT_Type"].ToString(), dt.Rows[0]["Normal_HOT_Value"].ToString(), dt.Rows[0]["Normal_WOT_Type"].ToString(), dt.Rows[0]["Normal_WOT_Value"].ToString(), dt.Rows[0]["Is_Partial_Enable"].ToString(), dt.Rows[0]["Partial_Leave_Mins"].ToString(), dt.Rows[0]["Partial_Leave_Day"].ToString(), dt.Rows[0]["Is_Partial_Carry"].ToString(), IsLate.ToString(), IsEarly.ToString(), IsAbsent.ToString(), "", "", true.ToString(), DateTime.Now.ToString(), true.ToString(), Session["UserId"].ToString(), DateTime.Now.ToString(), Session["UserId"].ToString(), DateTime.Now.ToString());
        if (b != 0)
        {
            DisplayMessage("Record Updated");

        }
    }
    protected void btnUpdateOT_Click(object sender, EventArgs e)
    {
        int b = 0;
        DataTable dt = objEmpParam.GetEmployeeParameterByEmpId(hdnEmpIdOt.Value, Session["CompId"].ToString());

        objEmpParam.DeleteEmployeeParameterByEmpId(hdnEmpIdOt.Value);
        string strCarry = string.Empty;

        string OtEmp = string.Empty;

        string partialEnable = string.Empty;


        if (rbtnPartialEnable1.Checked)
        {
            partialEnable = "True";
        }
        else
        {
            partialEnable = "False";
        }

        if (rbtnOTEnable1.Checked)
        {
            OtEmp = "True";
        }
        else
        {
            OtEmp = "False";
        }
        if (rbtnCarryYes1.Checked)
        {
            strCarry = "True";

        }
        else
        {
            strCarry = "False";
        }

        b = objEmpParam.InsertEmployeeParameter(Session["CompId"].ToString(), hdnEmpIdOt.Value, dt.Rows[0]["Basic_Salary"].ToString(), dt.Rows[0]["Salary_Type"].ToString(), dt.Rows[0]["Currency_Id"].ToString(), dt.Rows[0]["Assign_Min"].ToString(), dt.Rows[0]["Effective_Work_Cal_Method"].ToString(), OtEmp, ddlOTCalc1.SelectedValue, ddlNormalType1.SelectedValue, GetText(txtNormal1.Text), ddlHolidayValue1.SelectedValue, GetText(txtHolidayValue1.Text), ddlWeekOffType1.SelectedValue, GetText(txtWeekOffValue1.Text), partialEnable, GetText(txtTotalMinutesP1.Text), GetText(txtMinuteOTOne.Text), strCarry, dt.Rows[0]["Field1"].ToString(), dt.Rows[0]["Field2"].ToString(), dt.Rows[0]["Field3"].ToString(), "", "", true.ToString(), DateTime.Now.ToString(), true.ToString(), Session["UserId"].ToString(), DateTime.Now.ToString(), Session["UserId"].ToString(), DateTime.Now.ToString());


        if (b != 0)
        {
            DisplayMessage("Record Updated");

        }

    }


    protected void btnUpdateSal_Click(object sender, EventArgs e)
    {

        if (txtBasic1.Text == "")
        {
            DisplayMessage("Enter Basic Salary");
            txtBasic1.Focus();
            return;

        }
        if (txtWorkMin1.Text == "")
        {
            DisplayMessage("Enter Work Minute");
            txtWorkMin1.Focus();
            return;

        }

        if (ddlCurrency1.SelectedIndex == 0)
        {
            DisplayMessage("Select Currency");
            ddlCurrency1.Focus();
            return;

        }



        bool EmpInPayroll = false;
        bool EmpPF = false;
        bool EmpESIC = false;
        if (chkEmpINPayroll1.Checked)
        {
            EmpPF = chkEmpPf1.Checked;
            EmpESIC = chkEmpEsic1.Checked;
            EmpInPayroll = true;
        }
        else
        {
            EmpInPayroll = false;
            EmpPF = false;
            EmpESIC = false;
        }





        int b = 0;
        DataTable dt = objEmpParam.GetEmployeeParameterByEmpId(hdnEmpIdSal.Value, Session["CompId"].ToString());

        objEmpParam.DeleteEmployeeParameterByEmpId(hdnEmpIdSal.Value);
        b = objEmpParam.InsertEmployeeParameter(Session["CompId"].ToString(), hdnEmpIdSal.Value, txtBasic1.Text, ddlPaymentType1.SelectedValue, ddlCurrency1.SelectedValue, txtWorkMin1.Text, ddlWorkCal1.SelectedValue, dt.Rows[0]["Is_OverTime"].ToString(), dt.Rows[0]["Normal_OT_Method"].ToString(), dt.Rows[0]["Normal_OT_Type"].ToString(), dt.Rows[0]["Normal_OT_Value"].ToString(), dt.Rows[0]["Normal_HOT_Type"].ToString(), dt.Rows[0]["Normal_HOT_Value"].ToString(), dt.Rows[0]["Normal_WOT_Type"].ToString(), dt.Rows[0]["Normal_WOT_Value"].ToString(), dt.Rows[0]["Is_Partial_Enable"].ToString(), dt.Rows[0]["Partial_Leave_Mins"].ToString(), dt.Rows[0]["Partial_Leave_Day"].ToString(), dt.Rows[0]["Is_Partial_Carry"].ToString(), dt.Rows[0]["Field1"].ToString(), dt.Rows[0]["Field2"].ToString(), dt.Rows[0]["Field3"].ToString(), EmpPF.ToString(), EmpESIC.ToString(), EmpInPayroll.ToString(), DateTime.Now.ToString(), true.ToString(), Session["UserId"].ToString(), DateTime.Now.ToString(), Session["UserId"].ToString(), DateTime.Now.ToString());


        if (b != 0)
        {
            DisplayMessage("Record Updated");

        }

    }
    protected void btnUpdateNotice_Click(object sender, EventArgs e)
    {
        int b = 0;





        objEmpNotice.DeleteEmployeeNotificationByEmpId(hdnEmpIdNF.Value);

        b = objEmpNotice.InsertEmployeeNotification(Session["CompId"].ToString(), hdnEmpIdNF.Value, chkSMSDocExp1.Checked.ToString(), chkSMSAbsent1.Checked.ToString(), chkSMSLate1.Checked.ToString(), chkSMSEarly1.Checked.ToString(), ChkSmsLeave1.Checked.ToString(), ChkSMSPartial1.Checked.ToString(), chkSMSNoClock1.Checked.ToString(), false.ToString(), false.ToString(), false.ToString(), false.ToString(), false.ToString(), ChkRptAbsent1.Checked.ToString(), chkRptLate1.Checked.ToString(), chkRptEarly1.Checked.ToString(), ChkRptInOut1.Checked.ToString(), ChkRptSalary1.Checked.ToString(), ChkRptOvertime1.Checked.ToString(), ChkRptLog1.Checked.ToString(), chkRptDoc1.Checked.ToString(), chkRptViolation1.Checked.ToString(), true.ToString(), true.ToString(), true.ToString(), true.ToString(), true.ToString(), true.ToString(), true.ToString(), true.ToString(), true.ToString(), true.ToString(), "", "", "", "", "", true.ToString(), DateTime.Now.ToString(), true.ToString(), Session["UserId"].ToString(), DateTime.Now.ToString(), Session["UserId"].ToString(), DateTime.Now.ToString());





        if (b != 0)
        {
            DisplayMessage("Record Updated");

        }

    }
    protected void btnSaveNF_Click(object sender, EventArgs e)
    {
        int b = 0;

        if (rbtnEmpNF.Checked)
        {

            if (lblSelectRecordNF.Text == "")
            {
                DisplayMessage("Select employee first");
                return;

            }

            foreach (string str in lblSelectRecordNF.Text.Split(','))
            {
                if (str != "")
                {

                    objEmpNotice.DeleteEmployeeNotificationByEmpId(str);

                    b = objEmpNotice.InsertEmployeeNotification(Session["CompId"].ToString(), str, chkSMSDocExp.Checked.ToString(), chkSMSAbsent.Checked.ToString(), chkSMSLate.Checked.ToString(), chkSMSEarly.Checked.ToString(), ChkSmsLeave.Checked.ToString(), ChkSMSPartial.Checked.ToString(), chkSMSNoClock.Checked.ToString(), false.ToString(), false.ToString(), false.ToString(), false.ToString(), false.ToString(), ChkRptAbsent.Checked.ToString(), chkRptLate.Checked.ToString(), chkRptEarly.Checked.ToString(), ChkRptInOut.Checked.ToString(), ChkRptSalary.Checked.ToString(), ChkRptOvertime.Checked.ToString(), ChkRptLog.Checked.ToString(), chkRptDoc.Checked.ToString(), chkRptViolation.Checked.ToString(), true.ToString(), true.ToString(), true.ToString(), true.ToString(), true.ToString(), true.ToString(), true.ToString(), true.ToString(), true.ToString(), true.ToString(), "", "", "", "", "", true.ToString(), DateTime.Now.ToString(), true.ToString(), Session["UserId"].ToString(), DateTime.Now.ToString(), Session["UserId"].ToString(), DateTime.Now.ToString());

                }
            }
        }
        else if (rbtnGroupNF.Checked)
        {

            string GroupIds = string.Empty;
            string EmpIds = string.Empty;





            for (int i = 0; i < lbxGroupNF.Items.Count; i++)
            {
                if (lbxGroupNF.Items[i].Selected)
                {
                    GroupIds += lbxGroupNF.Items[i].Value + ",";
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

                    objEmpNotice.DeleteEmployeeNotificationByEmpId(str);

                    b = objEmpNotice.InsertEmployeeNotification(Session["CompId"].ToString(), str, chkSMSDocExp.Checked.ToString(), chkSMSAbsent.Checked.ToString(), chkSMSLate.Checked.ToString(), chkSMSEarly.Checked.ToString(), ChkSmsLeave.Checked.ToString(), ChkSMSPartial.Checked.ToString(), chkSMSNoClock.Checked.ToString(), false.ToString(), false.ToString(), false.ToString(), false.ToString(), false.ToString(), ChkRptAbsent.Checked.ToString(), chkRptLate.Checked.ToString(), chkRptEarly.Checked.ToString(), ChkRptInOut.Checked.ToString(), ChkRptSalary.Checked.ToString(), ChkRptOvertime.Checked.ToString(), ChkRptLog.Checked.ToString(), chkRptDoc.Checked.ToString(), chkRptViolation.Checked.ToString(), true.ToString(), true.ToString(), true.ToString(), true.ToString(), true.ToString(), true.ToString(), true.ToString(), true.ToString(), true.ToString(), true.ToString(), "", "", "", "", "", true.ToString(), DateTime.Now.ToString(), true.ToString(), Session["UserId"].ToString(), DateTime.Now.ToString(), Session["UserId"].ToString(), DateTime.Now.ToString());

                }
            }



        }

        if (b != 0)
        {
            DisplayMessage("Record Saved");
            btnResetNF_Click(null, null);
        }

    }
    protected void btnSavePenalty_Click(object sender, EventArgs e)
    {



        if (rbtLateInEnable.Checked == false && rbtLateInDisable.Checked == false)
        {
            DisplayMessage("Please select late type");
            return;
        }
        if (rbtEarlyOutEnable.Checked == false && rbtEarlyOutDisable.Checked == false)
        {
            DisplayMessage("Please select early out type");
            return;
        }
        if (rbtnAbsentEnable.Checked == false && rbtnAbsentDisable.Checked == false)
        {
            DisplayMessage("Please select absent type");
            return;
        }



        int b = 0;
        bool IsLate = false;
        bool IsEarly = false;
        bool IsAbsent = false;
        if (rbtLateInEnable.Checked)
        {
            IsLate = true;

        }

        if (rbtEarlyOutEnable.Checked)
        {
            IsEarly = true;

        }

        if (rbtnAbsentDisable.Checked)
        {
            IsAbsent = true;
        }

        if (rbtnPenaltyEmp.Checked)
        {
            if (lblSelectRecordPenalty.Text == "")
            {
                DisplayMessage("Select Employee First");
                return;

            }

            foreach (string str in lblSelectRecordPenalty.Text.Split(','))
            {
                if (str != "")
                {

                    DataTable dt = objEmpParam.GetEmployeeParameterByEmpId(str, Session["CompId"].ToString());

                    objEmpParam.DeleteEmployeeParameterByEmpId(str);


                    if (dt.Rows.Count > 0)
                    {

                        b = objEmpParam.InsertEmployeeParameter(Session["CompId"].ToString(), str, dt.Rows[0]["Basic_Salary"].ToString(), dt.Rows[0]["Salary_Type"].ToString(), dt.Rows[0]["Currency_Id"].ToString(), dt.Rows[0]["Assign_Min"].ToString(), dt.Rows[0]["Effective_Work_Cal_Method"].ToString(), dt.Rows[0]["Is_OverTime"].ToString(), dt.Rows[0]["Normal_OT_Method"].ToString(), dt.Rows[0]["Normal_OT_Type"].ToString(), dt.Rows[0]["Normal_OT_Value"].ToString(), dt.Rows[0]["Normal_HOT_Type"].ToString(), dt.Rows[0]["Normal_HOT_Value"].ToString(), dt.Rows[0]["Normal_WOT_Type"].ToString(), dt.Rows[0]["Normal_WOT_Value"].ToString(), dt.Rows[0]["Is_Partial_Enable"].ToString(), dt.Rows[0]["Partial_Leave_Mins"].ToString(), dt.Rows[0]["Partial_Leave_Day"].ToString(), dt.Rows[0]["Is_Partial_Carry"].ToString(), IsLate.ToString(), IsEarly.ToString(), IsAbsent.ToString(), dt.Rows[0]["Field4"].ToString(), dt.Rows[0]["Field5"].ToString(), dt.Rows[0]["Field6"].ToString(), DateTime.Now.ToString(), true.ToString(), Session["UserId"].ToString(), DateTime.Now.ToString(), Session["UserId"].ToString(), DateTime.Now.ToString());

                    }
                    else
                    {

                        b = objEmpParam.InsertEmployeeParameter(Session["CompId"].ToString(), str, "0", "Monthly", "0", "0", "PairWise", false.ToString(), "Work Hour", "2", "0", "2", "0", "2", "0", false.ToString(), "0", "0", false.ToString(), IsLate.ToString(), IsEarly.ToString(), IsAbsent.ToString(), dt.Rows[0]["Field4"].ToString(), dt.Rows[0]["Field5"].ToString(), dt.Rows[0]["Field6"].ToString(), DateTime.Now.ToString(), true.ToString(), Session["UserId"].ToString(), DateTime.Now.ToString(), Session["UserId"].ToString(), DateTime.Now.ToString());


                    }


                }
            }
        }
        else if (rbtnPenaltyGroup.Checked)
        {

            string GroupIds = string.Empty;
            string EmpIds = string.Empty;





            for (int i = 0; i < lbxGroupPenalty.Items.Count; i++)
            {
                if (lbxGroupPenalty.Items[i].Selected)
                {
                    GroupIds += lbxGroupPenalty.Items[i].Value + ",";
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
                    if (str != "")
                    {

                        DataTable dt = objEmpParam.GetEmployeeParameterByEmpId(str, Session["CompId"].ToString());

                        objEmpParam.DeleteEmployeeParameterByEmpId(str);

                        if (dt.Rows.Count > 0)
                        {

                            b = objEmpParam.InsertEmployeeParameter(Session["CompId"].ToString(), str, dt.Rows[0]["Basic_Salary"].ToString(), dt.Rows[0]["Salary_Type"].ToString(), dt.Rows[0]["Currency_Id"].ToString(), dt.Rows[0]["Assign_Min"].ToString(), dt.Rows[0]["Effective_Work_Cal_Method"].ToString(), dt.Rows[0]["Is_OverTime"].ToString(), dt.Rows[0]["Normal_OT_Method"].ToString(), dt.Rows[0]["Normal_OT_Type"].ToString(), dt.Rows[0]["Normal_OT_Value"].ToString(), dt.Rows[0]["Normal_HOT_Type"].ToString(), dt.Rows[0]["Normal_HOT_Value"].ToString(), dt.Rows[0]["Normal_WOT_Type"].ToString(), dt.Rows[0]["Normal_WOT_Value"].ToString(), dt.Rows[0]["Is_Partial_Enable"].ToString(), dt.Rows[0]["Partial_Leave_Mins"].ToString(), dt.Rows[0]["Partial_Leave_Day"].ToString(), dt.Rows[0]["Is_Partial_Carry"].ToString(), IsLate.ToString(), IsEarly.ToString(), IsAbsent.ToString(), dt.Rows[0]["Field4"].ToString(), dt.Rows[0]["Field5"].ToString(), dt.Rows[0]["Field6"].ToString(), DateTime.Now.ToString(), true.ToString(), Session["UserId"].ToString(), DateTime.Now.ToString(), Session["UserId"].ToString(), DateTime.Now.ToString());

                        }
                        else
                        {

                            b = objEmpParam.InsertEmployeeParameter(Session["CompId"].ToString(), str, "0", "Monthly", "0", "0", "", false.ToString(), "", "0", "0", "0", "0", "0", "0", false.ToString(), "0", "0", false.ToString(), IsLate.ToString(), IsEarly.ToString(), IsAbsent.ToString(), dt.Rows[0]["Field4"].ToString(), dt.Rows[0]["Field5"].ToString(), dt.Rows[0]["Field6"].ToString(), DateTime.Now.ToString(), true.ToString(), Session["UserId"].ToString(), DateTime.Now.ToString(), Session["UserId"].ToString(), DateTime.Now.ToString());


                        }


                    }
                }

            }
            else
            {
                DisplayMessage("Select Group First");
            }
        }
        if (b != 0)
        {
            DisplayMessage("Record Saved");
            btnResetPenalty_Click(null, null);
        }

    }
    protected void btnSaveOT_Click(object sender, EventArgs e)
    {
        int b = 0;
        bool IsCompOT = false;
        bool IsPartialComp = false;

        IsCompOT = Convert.ToBoolean(objAppParam.GetApplicationParameterValueByParamName("IsOverTime", Session["CompId"].ToString()));
        IsPartialComp = Convert.ToBoolean(objAppParam.GetApplicationParameterValueByParamName("Partial_Leave_Enable", Session["CompId"].ToString()));

        string workminute = string.Empty;

        string CurrencyId = string.Empty;

        string CalMethod = string.Empty;

        DataTable dtComp = objComp.GetCompanyMasterById(Session["CompId"].ToString());

        if (dtComp.Rows.Count > 0)
        {
            CurrencyId = dtComp.Rows[0]["Currency_Id"].ToString();
        }


        workminute = objAppParam.GetApplicationParameterValueByParamName("Work Day Min", Session["CompId"].ToString());

        CalMethod = objAppParam.GetApplicationParameterValueByParamName("Effective Work Calculation Method", Session["CompId"].ToString());


        string strCarry = string.Empty;

        string OtEmp = string.Empty;

        string partialEnable = string.Empty;


        if (rbtnPartialEnable.Checked)
        {
            partialEnable = "True";
        }
        else
        {
            partialEnable = "False";
        }

        if (rbtnOTEnable.Checked)
        {
            OtEmp = "True";
        }
        else
        {
            OtEmp = "False";
        }
        if (rbtnCarryYes.Checked)
        {
            strCarry = "True";

        }
        else
        {
            strCarry = "False";
        }

        if (rbtnEmpOT.Checked)
        {
            if (lblSelectRecordOT.Text == "")
            {
                DisplayMessage("Select Employee First");
                return;

            }

            foreach (string str in lblSelectRecordOT.Text.Split(','))
            {
                if (str != "")
                {

                    DataTable dt = objEmpParam.GetEmployeeParameterByEmpId(str, Session["CompId"].ToString());

                    objEmpParam.DeleteEmployeeParameterByEmpId(str);


                    if (dt.Rows.Count > 0)
                    {

                        b = objEmpParam.InsertEmployeeParameter(Session["CompId"].ToString(), str, dt.Rows[0]["Basic_Salary"].ToString(), dt.Rows[0]["Salary_Type"].ToString(), dt.Rows[0]["Currency_Id"].ToString(), dt.Rows[0]["Assign_Min"].ToString(), dt.Rows[0]["Effective_Work_Cal_Method"].ToString(), OtEmp, ddlOTCalc.SelectedValue, ddlNormalType.SelectedValue, GetText(txtNoralType.Text), ddlHolidayType.SelectedValue, GetText(txtHolidayValue.Text), ddlWeekOffType.SelectedValue, GetText(txtWeekOffValue.Text), partialEnable, GetText(txtTotalMinutes.Text), GetText(txtMinuteday.Text), strCarry, dt.Rows[0]["Field1"].ToString(), dt.Rows[0]["Field2"].ToString(), dt.Rows[0]["Field3"].ToString(), "", "", true.ToString(), DateTime.Now.ToString(), true.ToString(), Session["UserId"].ToString(), DateTime.Now.ToString(), Session["UserId"].ToString(), DateTime.Now.ToString());

                    }
                    else
                    {




                        b = objEmpParam.InsertEmployeeParameter(Session["CompId"].ToString(), str, "0", "Monthly", CurrencyId, workminute, CalMethod, OtEmp, ddlOTCalc.SelectedValue, ddlNormalType.SelectedValue, GetText(txtNoralType.Text), ddlHolidayType.SelectedValue, GetText(txtHolidayValue.Text), ddlWeekOffType.SelectedValue, GetText(txtWeekOffValue.Text), partialEnable, GetText(txtTotalMinutes.Text), GetText(txtMinuteday.Text), strCarry, false.ToString(), false.ToString(), false.ToString(), "", "", true.ToString(), DateTime.Now.ToString(), true.ToString(), Session["UserId"].ToString(), DateTime.Now.ToString(), Session["UserId"].ToString(), DateTime.Now.ToString());


                    }


                }
            }
        }
        else if (rbtnGroupOT.Checked)
        {

            string GroupIds = string.Empty;
            string EmpIds = string.Empty;





            for (int i = 0; i < lbxGroupOT.Items.Count; i++)
            {
                if (lbxGroupOT.Items[i].Selected)
                {
                    GroupIds += lbxGroupOT.Items[i].Value + ",";
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
                    if (str != "")
                    {

                        DataTable dt = objEmpParam.GetEmployeeParameterByEmpId(str, Session["CompId"].ToString());

                        objEmpParam.DeleteEmployeeParameterByEmpId(str);


                        if (dt.Rows.Count > 0)
                        {

                            b = objEmpParam.InsertEmployeeParameter(Session["CompId"].ToString(), str, dt.Rows[0]["Basic_Salary"].ToString(), dt.Rows[0]["Salary_Type"].ToString(), dt.Rows[0]["Currency_Id"].ToString(), dt.Rows[0]["Assign_Min"].ToString(), dt.Rows[0]["Effective_Work_Cal_Method"].ToString(), OtEmp, ddlOTCalc.SelectedValue, ddlNormalType.SelectedValue, GetText(txtNoralType.Text), ddlHolidayType.SelectedValue, GetText(txtHolidayValue.Text), ddlWeekOffType.SelectedValue, GetText(txtWeekOffValue.Text), partialEnable, GetText(txtTotalMinutes.Text), GetText(txtMinuteday.Text), strCarry, dt.Rows[0]["Field1"].ToString(), dt.Rows[0]["Field2"].ToString(), dt.Rows[0]["Field3"].ToString(), dt.Rows[0]["Field4"].ToString(), dt.Rows[0]["Field5"].ToString(), dt.Rows[0]["Field6"].ToString(), DateTime.Now.ToString(), true.ToString(), Session["UserId"].ToString(), DateTime.Now.ToString(), Session["UserId"].ToString(), DateTime.Now.ToString());

                        }
                        else
                        {




                            b = objEmpParam.InsertEmployeeParameter(Session["CompId"].ToString(), str, "0", "Monthly", CurrencyId, workminute, CalMethod, OtEmp, ddlOTCalc.SelectedValue, ddlNormalType.SelectedValue, GetText(txtNoralType.Text), ddlHolidayType.SelectedValue, GetText(txtHolidayValue.Text), ddlWeekOffType.SelectedValue, GetText(txtWeekOffValue.Text), partialEnable, GetText(txtTotalMinutes.Text), GetText(txtMinuteday.Text), strCarry, false.ToString(), false.ToString(), false.ToString(), dt.Rows[0]["Field4"].ToString(), dt.Rows[0]["Field5"].ToString(), dt.Rows[0]["Field6"].ToString(), DateTime.Now.ToString(), true.ToString(), Session["UserId"].ToString(), DateTime.Now.ToString(), Session["UserId"].ToString(), DateTime.Now.ToString());


                        }


                    }
                }

            }
            else
            {
                DisplayMessage("Select Group First");
            }
        }
        if (b != 0)
        {
            DisplayMessage("Record Saved");
            btnResetOT_Click(null, null);
        }
    }

    public string GetText(string value)
    {
        string value1 = string.Empty; ;
        if (value == "")
        {
            value1 = "0";

        }
        else
        {
            value1 = value;
        }
        return value1;


    }

    protected void btnCancelPenalty_Click(object sender, EventArgs e)
    {
        lblSelectRecordPenalty.Text = "";
        gvEmpPenalty.Visible = true;
        DataTable dtEmp = objEmp.GetEmployeeMaster(Session["CompId"].ToString());

        dtEmp = new DataView(dtEmp, "Brand_Id='" + Session["BrandId"].ToString() + "'  and Location_Id='" + Session["LocId"].ToString() + "'", "", DataViewRowState.CurrentRows).ToTable();

        if (Session["SessionDepId"] != null)
        {
            dtEmp = new DataView(dtEmp, "Department_Id in(" + Session["SessionDepId"].ToString().Substring(0, Session["SessionDepId"].ToString().Length - 1) + ")", "", DataViewRowState.CurrentRows).ToTable();

        }
        if (dtEmp.Rows.Count > 0)
        {
            Session["dtEmpPenalty"] = dtEmp;
            gvEmpPenalty.DataSource = dtEmp;
            gvEmpPenalty.DataBind();

        }

        btnResetPenalty_Click(null, null);
        btnList_Click(null, null);
    }

    protected void btnCancelOT_Click(object sender, EventArgs e)
    {
        lblSelectRecordOT.Text = "";
        gvEmpOT.Visible = true;
        DataTable dtEmp = objEmp.GetEmployeeMaster(Session["CompId"].ToString());

        dtEmp = new DataView(dtEmp, "Brand_Id='" + Session["BrandId"].ToString() + "'  and Location_Id='" + Session["LocId"].ToString() + "'", "", DataViewRowState.CurrentRows).ToTable();

        if (Session["SessionDepId"] != null)
        {
            dtEmp = new DataView(dtEmp, "Department_Id in(" + Session["SessionDepId"].ToString().Substring(0, Session["SessionDepId"].ToString().Length - 1) + ")", "", DataViewRowState.CurrentRows).ToTable();

        }
        if (dtEmp.Rows.Count > 0)
        {
            Session["dtEmpSal"] = dtEmp;
            gvEmpOT.DataSource = dtEmp;
            gvEmpOT.DataBind();

        }

        btnResetOT_Click(null, null);
        btnList_Click(null, null);

    }

    protected void btnResetPenalty_Click(object sender, EventArgs e)
    {
        if (Session["dtEmpPenalty"] != null)
        {
            gvEmpPenalty.DataSource = (DataTable)Session["dtEmpPenalty"];
            gvEmpPenalty.DataBind();
        }
        lblSelectRecordPenalty.Text = "";

        rbtEarlyOutDisable.Checked = false;
        rbtEarlyOutEnable.Checked = false;
        rbtLateInDisable.Checked = false;
        rbtLateInEnable.Checked = false;
        rbtnAbsentDisable.Checked = false;
        rbtnAbsentEnable.Checked = false;

    }
    protected void btnResetOT_Click(object sender, EventArgs e)
    {
        ddlOTCalc.SelectedIndex = 0;
        txtNoralType.Text = "";
        txtWeekOffValue.Text = "";
        txtHolidayValue.Text = "";
        ddlNormalType.SelectedIndex = 0;
        ddlWeekOffType.SelectedIndex = 0;
        ddlHolidayType.SelectedIndex = 0;
        txtTotalMinutes.Text = "";
        txtMinuteday.Text = "";
        rbtnPartialEnable.Checked = true;
        rbtPartial_OnCheckedChanged(null, null);

        rbtnOTEnable.Checked = true;
        rbtOT_OnCheckedChanged(null, null);
        if (Session["dtEmpOT"] != null)
        {
            gvEmpOT.DataSource = (DataTable)Session["dtEmpOT"];
            gvEmpOT.DataBind();
        }
        lblSelectRecordOT.Text = "";




        DataTable dtGroup = objEmpGroup.GetEmployeeGroup_Master(Session["CompId"].ToString());
        dtGroup = new DataView(dtGroup, "Brand_Id='" + Session["BrandId"].ToString() + "'  and Location_Id='" + Session["LocId"].ToString() + "'", "", DataViewRowState.CurrentRows).ToTable();
        if (dtGroup.Rows.Count > 0)
        {
            lbxGroupOT.DataSource = dtGroup;
            lbxGroupOT.DataTextField = "Group_Name";
            lbxGroupOT.DataValueField = "Group_Id";

            lbxGroupOT.DataBind();

        }

        gvEmployeeOT.DataSource = null;
        gvEmployeeOT.DataBind();

        bool IsCompOT = false;
        bool IsPartialComp = false;

        IsCompOT = Convert.ToBoolean(objAppParam.GetApplicationParameterValueByParamName("IsOverTime", Session["CompId"].ToString()));
        IsPartialComp = Convert.ToBoolean(objAppParam.GetApplicationParameterValueByParamName("Partial_Leave_Enable", Session["CompId"].ToString()));


        if (IsCompOT)
        {
            rbtnOTEnable.Checked = true;
            rbtnOTDisable.Checked = false;
            rbtOT_OnCheckedChanged(null, null);




        }
        else
        {
            rbtnOTEnable.Checked = false;
            rbtnOTDisable.Checked = true;
            rbtOT_OnCheckedChanged(null, null);


            rbtnOTEnable.Enabled = false;
            rbtnOTDisable.Enabled = false;

        }

        if (IsPartialComp)
        {
            rbtnPartialEnable.Checked = true;
            rbtnPartialDisable.Checked = false;

            rbtPartial_OnCheckedChanged(null, null);



        }
        else
        {
            rbtnPartialEnable.Checked = false;
            rbtnPartialDisable.Checked = true;

            rbtPartial_OnCheckedChanged(null, null);

            rbtnPartialEnable.Enabled = false;
            rbtnPartialDisable.Enabled = false;
        }

    }
    protected void btnSaveSal_Click(object sender, EventArgs e)
    {

        if (txtBasic.Text == "")
        {
            DisplayMessage("Enter Basic Salary");
            txtBasic.Focus();
            return;

        }
        if (txtWorkMinute.Text == "")
        {
            DisplayMessage("Enter Work Minute");
            txtWorkMinute.Focus();
            return;

        }

        if (ddlCurrency.SelectedIndex == 0)
        {
            DisplayMessage("Select Currency");
            ddlCurrency.Focus();
            return;

        }

        int b = 0;
        bool IsCompOT = false;
        bool IsPartialComp = false;
        bool IsPartialCarry = false;
        string PartialMin = "";
        string PartialminOne = "";

        IsCompOT = Convert.ToBoolean(objAppParam.GetApplicationParameterValueByParamName("IsOverTime", Session["CompId"].ToString()));
        IsPartialComp = Convert.ToBoolean(objAppParam.GetApplicationParameterValueByParamName("Partial_Leave_Enable", Session["CompId"].ToString()));

        IsPartialCarry = Convert.ToBoolean(objAppParam.GetApplicationParameterValueByParamName("Carry Forward Partial Leave Minutes", Session["CompId"].ToString()));
        PartialMin = objAppParam.GetApplicationParameterValueByParamName("Total Partial Leave Minutes", Session["CompId"].ToString());
        PartialminOne = objAppParam.GetApplicationParameterValueByParamName("Partial Leave Minute Use In A Day", Session["CompId"].ToString());


        string NormalOT = string.Empty;

        NormalOT = objAppParam.GetApplicationParameterValueByParamName("Over Time Calculation Method", Session["CompId"].ToString());
        bool EmpInPayroll = false;
        bool EmpPF = false;
        bool EmpESIC = false;
        if (chkEmpINPayroll.Checked)
        {
            EmpPF = chkEmpPf.Checked;
            EmpESIC = chkEmpEsic.Checked;
            EmpInPayroll = true;
        }
        else
        {
            EmpInPayroll = false;
            EmpPF = false;
            EmpESIC = false;
        }





        if (rbtnEmpSal.Checked)
        {




            if (lblSelectRecordSal.Text == "")
            {
                DisplayMessage("Select employee first");
                return;

            }





            foreach (string str in lblSelectRecordSal.Text.Split(','))
            {
                if (str != "")
                {

                    DataTable dt = objEmpParam.GetEmployeeParameterByEmpId(str, Session["CompId"].ToString());

                    objEmpParam.DeleteEmployeeParameterByEmpId(str);


                    if (dt.Rows.Count > 0)
                    {
                        b = objEmpParam.InsertEmployeeParameter(Session["CompId"].ToString(), str, txtBasic.Text, ddlPayment.SelectedValue, ddlCurrency.SelectedValue, txtWorkMinute.Text, ddlWorkCalMethod.SelectedValue, dt.Rows[0]["Is_OverTime"].ToString(), dt.Rows[0]["Normal_OT_Method"].ToString(), dt.Rows[0]["Normal_OT_Type"].ToString(), dt.Rows[0]["Normal_OT_Value"].ToString(), dt.Rows[0]["Normal_HOT_Type"].ToString(), dt.Rows[0]["Normal_HOT_Value"].ToString(), dt.Rows[0]["Normal_WOT_Type"].ToString(), dt.Rows[0]["Normal_WOT_Value"].ToString(), dt.Rows[0]["Is_Partial_Enable"].ToString(), dt.Rows[0]["Partial_Leave_Mins"].ToString(), dt.Rows[0]["Partial_Leave_Day"].ToString(), dt.Rows[0]["Is_Partial_Carry"].ToString(), dt.Rows[0]["Field1"].ToString(), dt.Rows[0]["Field2"].ToString(), dt.Rows[0]["Field3"].ToString(), EmpPF.ToString(), EmpESIC.ToString(), EmpInPayroll.ToString(), DateTime.Now.ToString(), true.ToString(), Session["UserId"].ToString(), DateTime.Now.ToString(), Session["UserId"].ToString(), DateTime.Now.ToString());

                    }
                    else
                    {
                        b = objEmpParam.InsertEmployeeParameter(Session["CompId"].ToString(), str, txtBasic.Text, ddlPayment.SelectedValue, ddlCurrency.SelectedValue, txtWorkMinute.Text, ddlWorkCalMethod.SelectedValue, IsCompOT.ToString(), NormalOT, "2", "0", "2", "0", "2", "0", IsPartialComp.ToString(), PartialMin, PartialminOne, IsPartialCarry.ToString(), false.ToString(), false.ToString(), false.ToString(), EmpPF.ToString(), EmpESIC.ToString(), EmpInPayroll.ToString(), DateTime.Now.ToString(), true.ToString(), Session["UserId"].ToString(), DateTime.Now.ToString(), Session["UserId"].ToString(), DateTime.Now.ToString());


                    }


                }
            }



        }

        else if (rbtnGroupSal.Checked)
        {
            string GroupIds = string.Empty;
            string EmpIds = string.Empty;





            for (int i = 0; i < lbxGroupSal.Items.Count; i++)
            {
                if (lbxGroupSal.Items[i].Selected)
                {
                    GroupIds += lbxGroupSal.Items[i].Value + ",";
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
                    if (str != "")
                    {

                        DataTable dt = objEmpParam.GetEmployeeParameterByEmpId(str, Session["CompId"].ToString());

                        objEmpParam.DeleteEmployeeParameterByEmpId(str);


                        if (dt.Rows.Count > 0)
                        {
                            b = objEmpParam.InsertEmployeeParameter(Session["CompId"].ToString(), str, txtBasic.Text, ddlPayment.SelectedValue, ddlCurrency.SelectedValue, txtWorkMinute.Text, ddlWorkCalMethod.SelectedValue, dt.Rows[0]["Is_OverTime"].ToString(), dt.Rows[0]["Normal_OT_Method"].ToString(), dt.Rows[0]["Normal_OT_Type"].ToString(), dt.Rows[0]["Normal_OT_Value"].ToString(), dt.Rows[0]["Normal_HOT_Type"].ToString(), dt.Rows[0]["Normal_HOT_Value"].ToString(), dt.Rows[0]["Normal_WOT_Type"].ToString(), dt.Rows[0]["Normal_WOT_Value"].ToString(), dt.Rows[0]["Is_Partial_Enable"].ToString(), dt.Rows[0]["Partial_Leave_Mins"].ToString(), dt.Rows[0]["Partial_Leave_Day"].ToString(), dt.Rows[0]["Is_Partial_Carry"].ToString(), dt.Rows[0]["Field1"].ToString(), dt.Rows[0]["Field2"].ToString(), dt.Rows[0]["Field3"].ToString(), EmpPF.ToString(), EmpESIC.ToString(), EmpInPayroll.ToString(), DateTime.Now.ToString(), true.ToString(), Session["UserId"].ToString(), DateTime.Now.ToString(), Session["UserId"].ToString(), DateTime.Now.ToString());

                        }
                        else
                        {
                            b = objEmpParam.InsertEmployeeParameter(Session["CompId"].ToString(), str, txtBasic.Text, ddlPayment.SelectedValue, ddlCurrency.SelectedValue, txtWorkMinute.Text, ddlWorkCalMethod.SelectedValue, IsCompOT.ToString(), NormalOT, "2", "0", "2", "0", "2", "0", IsPartialComp.ToString(), PartialMin, PartialminOne, IsPartialCarry.ToString(), false.ToString(), false.ToString(), false.ToString(), EmpPF.ToString(), EmpESIC.ToString(), EmpInPayroll.ToString(), DateTime.Now.ToString(), true.ToString(), Session["UserId"].ToString(), DateTime.Now.ToString(), Session["UserId"].ToString(), DateTime.Now.ToString());


                        }



                    }
                }

            }
            else
            {
                DisplayMessage("Select Group First");
            }


        }

        if (b != 0)
        {
            DisplayMessage("Record Saved");
            btnResetSal_Click(null, null);
        }

    }
    protected void btnResetSal_Click(object sender, EventArgs e)
    {
        txtBasic.Text = "";
        txtWorkMinute.Text = "";
        ddlPayment.SelectedIndex = 0;
        ddlWorkCalMethod.SelectedIndex = 0;
        ddlCurrency.SelectedIndex = 0;

        chkEmpINPayroll.Checked = true;
        chkEmpINPayroll_OnCheckedChanged(null, null);

        if (Session["dtEmpSal"] != null)
        {
            gvEmpSalary.DataSource = (DataTable)Session["dtEmpSal"];
            gvEmpSalary.DataBind();
        }
        lblSelectRecordSal.Text = "";


        DataTable dtGroup = objEmpGroup.GetEmployeeGroup_Master(Session["CompId"].ToString());
        dtGroup = new DataView(dtGroup, "Brand_Id='" + Session["BrandId"].ToString() + "'  and Location_Id='" + Session["LocId"].ToString() + "'", "", DataViewRowState.CurrentRows).ToTable();
        if (dtGroup.Rows.Count > 0)
        {
            lbxGroupSal.DataSource = dtGroup;
            lbxGroupSal.DataTextField = "Group_Name";
            lbxGroupSal.DataValueField = "Group_Id";

            lbxGroupSal.DataBind();

        }

        gvEmployeeSal.DataSource = null;
        gvEmployeeSal.DataBind();


    }
    protected void btnCancelSal_Click(object sender, EventArgs e)
    {
        lblSelectRecordSal.Text = "";
        gvEmpSalary.Visible = true;
        DataTable dtEmp = objEmp.GetEmployeeMaster(Session["CompId"].ToString());

        dtEmp = new DataView(dtEmp, "Brand_Id='" + Session["BrandId"].ToString() + "'  and Location_Id='" + Session["LocId"].ToString() + "'", "", DataViewRowState.CurrentRows).ToTable();

        if (Session["SessionDepId"] != null)
        {
            dtEmp = new DataView(dtEmp, "Department_Id in(" + Session["SessionDepId"].ToString().Substring(0, Session["SessionDepId"].ToString().Length - 1) + ")", "", DataViewRowState.CurrentRows).ToTable();

        }
        if (dtEmp.Rows.Count > 0)
        {
            Session["dtEmpSal"] = dtEmp;
            gvEmpSalary.DataSource = dtEmp;
            gvEmpSalary.DataBind();

        }

        btnResetSal_Click(null, null);
        btnList_Click(null, null);

    }





    protected void btnClosePanel_ClickNf(object sender, EventArgs e)
    {
        pnlNotice1.Visible = false;
        pnlNotice2.Visible = false;


    }
    protected void btnCancelNF_Click(object sender, EventArgs e)
    {
        lblSelectRecordNF.Text = "";
        gvEmpNF.Visible = true;
        DataTable dtEmp = objEmp.GetEmployeeMaster(Session["CompId"].ToString());

        dtEmp = new DataView(dtEmp, "Brand_Id='" + Session["BrandId"].ToString() + "'  and Location_Id='" + Session["LocId"].ToString() + "'", "", DataViewRowState.CurrentRows).ToTable();

        if (Session["SessionDepId"] != null)
        {
            dtEmp = new DataView(dtEmp, "Department_Id in(" + Session["SessionDepId"].ToString().Substring(0, Session["SessionDepId"].ToString().Length - 1) + ")", "", DataViewRowState.CurrentRows).ToTable();

        }
        if (dtEmp.Rows.Count > 0)
        {
            Session["dtEmpLeave"] = dtEmp;
            gvEmpNF.DataSource = dtEmp;
            gvEmpNF.DataBind();

        }

        btnResetNF_Click(null, null);
        btnList_Click(null, null);
    }
    protected void btnResetNF_Click(object sender, EventArgs e)
    {
        lblSelectRecordNF.Text = "";
        gvEmpNF.Visible = true;
        DataTable dtEmp = objEmp.GetEmployeeMaster(Session["CompId"].ToString());

        dtEmp = new DataView(dtEmp, "Brand_Id='" + Session["BrandId"].ToString() + "'  and Location_Id='" + Session["LocId"].ToString() + "'", "", DataViewRowState.CurrentRows).ToTable();

        if (Session["SessionDepId"] != null)
        {
            dtEmp = new DataView(dtEmp, "Department_Id in(" + Session["SessionDepId"].ToString().Substring(0, Session["SessionDepId"].ToString().Length - 1) + ")", "", DataViewRowState.CurrentRows).ToTable();

        }
        if (dtEmp.Rows.Count > 0)
        {
            Session["dtEmpLeave"] = dtEmp;
            gvEmpNF.DataSource = dtEmp;
            gvEmpNF.DataBind();

        }
        chkSMSDocExp.Checked = false;
        chkSMSAbsent.Checked = false;
        chkSMSLate.Checked = false;
        chkSMSEarly.Checked = false;
        ChkSmsLeave.Checked = false;
        ChkSMSPartial.Checked = false;
        chkSMSNoClock.Checked = false;
        ChkRptAbsent.Checked = false;
        chkRptLate.Checked = false;
        chkRptEarly.Checked = false;
        ChkRptInOut.Checked = false;
        ChkRptSalary.Checked = false;
        ChkRptOvertime.Checked = false;
        ChkRptLog.Checked = false;
        chkRptDoc.Checked = false;
        chkRptViolation.Checked = false;

        DataTable dtGroup = objEmpGroup.GetEmployeeGroup_Master(Session["CompId"].ToString());
        dtGroup = new DataView(dtGroup, "Brand_Id='" + Session["BrandId"].ToString() + "'  and Location_Id='" + Session["LocId"].ToString() + "'", "", DataViewRowState.CurrentRows).ToTable();
        if (dtGroup.Rows.Count > 0)
        {
            lbxGroupNF.DataSource = dtGroup;
            lbxGroupNF.DataTextField = "Group_Name";
            lbxGroupNF.DataValueField = "Group_Id";

            lbxGroupNF.DataBind();

        }

        gvEmployeeNF.DataSource = null;
        gvEmployeeNF.DataBind();


    }
    #region Add AddressName Concept
    protected void txtAddressName_TextChanged(object sender, EventArgs e)
    {
        if (txtAddressName.Text != "")
        {
            DataTable dtAM = AM.GetAddressDataByAddressName(Session["CompId"].ToString(), txtAddressName.Text);
            if (dtAM.Rows.Count > 0)
            {
                System.Web.UI.ScriptManager.GetCurrent(this).SetFocus(imgAddAddressName);
            }
            else
            {
                txtAddressName.Text = "";
                DisplayMessage("Choose In Suggestions Only");
                System.Web.UI.ScriptManager.GetCurrent(this).SetFocus(txtAddressName);
                return;
            }
        }
        else
        {
            DisplayMessage("Enter Address Name");
            System.Web.UI.ScriptManager.GetCurrent(this).SetFocus(txtAddressName);
        }
    }
    protected void imgAddAddressName_Click(object sender, EventArgs e)
    {
        if (txtAddressName.Text != "")
        {
            string strA = "0";
            foreach (GridViewRow gve in GvAddressName.Rows)
            {
                Label lblCAddressName = (Label)gve.FindControl("lblgvAddressName");
                if (txtAddressName.Text == lblCAddressName.Text)
                {
                    strA = "1";
                }
            }


            if (hdnAddressId.Value == "")
            {
                if (strA == "0")
                {
                    FillAddressChidGird("Save");
                    System.Web.UI.ScriptManager.GetCurrent(this).SetFocus(txtAddressName);
                }
                else
                {
                    txtAddressName.Text = "";
                    DisplayMessage("Address Name Already Exists");
                    System.Web.UI.ScriptManager.GetCurrent(this).SetFocus(txtAddressName);
                }
            }
            else
            {
                if (txtAddressName.Text == hdnAddressName.Value)
                {
                    FillAddressChidGird("Edit");
                    System.Web.UI.ScriptManager.GetCurrent(this).SetFocus(txtAddressName);
                }
                else
                {
                    if (strA == "0")
                    {
                        FillAddressChidGird("Edit");
                        System.Web.UI.ScriptManager.GetCurrent(this).SetFocus(txtAddressName);
                    }
                    else
                    {
                        txtAddressName.Text = "";
                        DisplayMessage("Address Name Already Exists");
                        System.Web.UI.ScriptManager.GetCurrent(this).SetFocus(txtAddressName);
                    }
                }
            }
        }
        else
        {
            DisplayMessage("Enter Address Name");
            System.Web.UI.ScriptManager.GetCurrent(this).SetFocus(txtAddressName);
        }
        txtAddressName.Focus();
    }
    public void ResetAddressName()
    {
        txtAddressName.Text = "";
        hdnAddressId.Value = "";
        hdnAddressName.Value = "";
    }
    public DataTable CreateAddressDataTable()
    {
        DataTable dt = new DataTable();
        dt.Columns.Add("Trans_Id");
        dt.Columns.Add("Address_Name");
        return dt;
    }
    public DataTable FillAddressDataTabel()
    {
        string strNewSNo = string.Empty;
        DataTable dt = CreateAddressDataTable();
        if (GvAddressName.Rows.Count > 0)
        {
            for (int i = 0; i < GvAddressName.Rows.Count + 1; i++)
            {
                if (dt.Rows.Count != GvAddressName.Rows.Count)
                {
                    dt.Rows.Add(i);
                    Label lblSNo = (Label)GvAddressName.Rows[i].FindControl("lblSNo");
                    Label lblAddressName = (Label)GvAddressName.Rows[i].FindControl("lblgvAddressName");

                    dt.Rows[i]["Trans_Id"] = lblSNo.Text;
                    strNewSNo = lblSNo.Text;
                    dt.Rows[i]["Address_Name"] = lblAddressName.Text;
                }
                else
                {
                    dt.Rows.Add(i);
                    dt.Rows[i]["Trans_Id"] = (float.Parse(strNewSNo) + 1).ToString();
                    dt.Rows[i]["Address_Name"] = txtAddressName.Text;
                }
            }
        }
        else
        {
            dt.Rows.Add(0);
            dt.Rows[0]["Trans_Id"] = "1";
            dt.Rows[0]["Address_Name"] = txtAddressName.Text;
        }
        if (dt.Rows.Count > 0)
        {
            GvAddressName.DataSource = dt;
            GvAddressName.DataBind();
        }
        return dt;
    }
    public DataTable FillAddressDataTabelDelete()
    {
        DataTable dt = CreateAddressDataTable();
        for (int i = 0; i < GvAddressName.Rows.Count; i++)
        {
            dt.Rows.Add(i);
            Label lblSNo = (Label)GvAddressName.Rows[i].FindControl("lblSNo");
            Label lblgvAddressName = (Label)GvAddressName.Rows[i].FindControl("lblgvAddressName");


            dt.Rows[i]["Trans_Id"] = lblSNo.Text;
            dt.Rows[i]["Address_Name"] = lblgvAddressName.Text;
        }

        DataView dv = new DataView(dt);
        dv.RowFilter = "Trans_Id<>'" + hdnAddressId.Value + "'";
        dt = (DataTable)dv.ToTable();
        return dt;
    }
    protected void btnAddressEdit_Command(object sender, CommandEventArgs e)
    {
        hdnAddressId.Value = e.CommandArgument.ToString();
        FillAddressDataTabelEdit();
    }
    public DataTable FillAddressDataTabelEdit()
    {
        DataTable dt = CreateAddressDataTable();

        for (int i = 0; i < GvAddressName.Rows.Count; i++)
        {
            dt.Rows.Add(i);
            Label lblSNo = (Label)GvAddressName.Rows[i].FindControl("lblSNo");
            Label lblgvAddressName = (Label)GvAddressName.Rows[i].FindControl("lblgvAddressName");

            dt.Rows[i]["Trans_Id"] = lblSNo.Text;
            dt.Rows[i]["Address_Name"] = lblgvAddressName.Text;
        }

        DataView dv = new DataView(dt);
        dv.RowFilter = "Trans_Id='" + hdnAddressId.Value + "'";
        dt = (DataTable)dv.ToTable();
        if (dt.Rows.Count != 0)
        {
            txtAddressName.Text = dt.Rows[0]["Address_Name"].ToString();
            hdnAddressName.Value = dt.Rows[0]["Address_Name"].ToString();
        }
        return dt;
    }
    protected void btnAddressDelete_Command(object sender, CommandEventArgs e)
    {
        hdnAddressId.Value = e.CommandArgument.ToString();
        FillAddressChidGird("Del");
    }
    public void FillAddressChidGird(string CommandName)
    {
        DataTable dt = new DataTable();
        if (CommandName.ToString() == "Del")
        {
            dt = FillAddressDataTabelDelete();
        }
        else if (CommandName.ToString() == "Edit")
        {
            dt = FillAddressDataTableUpdate();
        }
        else
        {
            dt = FillAddressDataTabel();
        }
        GvAddressName.DataSource = dt;
        GvAddressName.DataBind();

        ResetAddressName();
    }
    public DataTable FillAddressDataTableUpdate()
    {
        DataTable dt = CreateAddressDataTable();
        for (int i = 0; i < GvAddressName.Rows.Count; i++)
        {
            dt.Rows.Add(i);
            Label lblSNo = (Label)GvAddressName.Rows[i].FindControl("lblSNo");
            Label lblgvAddressName = (Label)GvAddressName.Rows[i].FindControl("lblgvAddressName");

            dt.Rows[i]["Trans_Id"] = lblSNo.Text;
            dt.Rows[i]["Address_Name"] = lblgvAddressName.Text;
        }
        for (int i = 0; i < dt.Rows.Count; i++)
        {
            if (hdnAddressId.Value == dt.Rows[i]["Trans_Id"].ToString())
            {
                dt.Rows[i]["Address_Name"] = txtAddressName.Text;
            }
        }
        return dt;
    }
    #endregion

    #region Add New Address Concept
    protected void btnAddNewAddress_Click(object sender, EventArgs e)
    {
        pnlAddress1.Visible = true;
        pnlAddress2.Visible = true;
        FillAddressCategory();
        FillCountry();
    }
    protected void txtAddressNameNew_TextChanged(object sender, EventArgs e)
    {
        DataTable dt = AM.GetAddressDataByAddressName(Session["CompId"].ToString(), txtAddressNameNew.Text);
        if (dt.Rows.Count > 0)
        {
            txtAddressNameNew.Text = "";
            DisplayMessage("Address Name Already Exists");
            System.Web.UI.ScriptManager.GetCurrent(this).SetFocus(txtAddressNameNew);
            return;
        }

        DataTable dt1 = AM.GetAddressFalseAllData(Session["CompId"].ToString());
        dt1 = new DataView(dt1, "Address_Name='" + txtAddressNameNew.Text + "'", "", DataViewRowState.CurrentRows).ToTable();
        if (dt1.Rows.Count > 0)
        {
            txtAddressNameNew.Text = "";
            DisplayMessage("Address Name Already In Deleted Section");
            System.Web.UI.ScriptManager.GetCurrent(this).SetFocus(txtAddressNameNew);
            return;
        }
        System.Web.UI.ScriptManager.GetCurrent(this).SetFocus(txtAddress);
    }
    protected void btnAddressSave_Click(object sender, EventArgs e)
    {
        if (ddlAddressCategory.SelectedValue == "--Select--")
        {
            DisplayMessage("Select Address Category");
            System.Web.UI.ScriptManager.GetCurrent(this).SetFocus(ddlAddressCategory);
            return;
        }
        if (txtAddressNameNew.Text == "" || txtAddressNameNew.Text == null)
        {
            DisplayMessage("Enter Address Name");
            System.Web.UI.ScriptManager.GetCurrent(this).SetFocus(txtAddressNameNew);
            return;
        }

        if (txtAddress.Text == "" || txtAddress.Text == null)
        {
            DisplayMessage("Enter Address");
            System.Web.UI.ScriptManager.GetCurrent(this).SetFocus(txtAddress);
            return;
        }

        string strCountryId = string.Empty;
        if (ddlCountry.SelectedValue == "--Select--")
        {
            strCountryId = "0";
        }
        else
        {
            strCountryId = ddlCountry.SelectedValue;
        }
        if (txtEmailId1.Text != "")
        {
            if (!CheckEmailId1(txtEmailId1.Text))
            {
                DisplayMessage("Email Id 1 is invalid");
                txtEmailId1.Text = "";
                txtEmailId1.Focus();
                return;
            }
        }
        if (txtEmailId2.Text != "")
        {
            if (!CheckEmailId2(txtEmailId2.Text))
            {
                DisplayMessage("Email Id 2 is invalid");
                txtEmailId2.Text = "";
                txtEmailId2.Focus();
                return;
            }
        }
        string strLongitude = string.Empty;
        if (txtLongitude.Text != "")
        {
            strLongitude = txtLongitude.Text;
        }
        else
        {
            strLongitude = "0";
        }
        string strLatitude = string.Empty;
        if (txtLatitude.Text != "")
        {
            strLatitude = txtLatitude.Text;
        }
        else
        {
            strLatitude = "0";
        }

        int b = 0;

        DataTable dtadd2 = AM.GetAddressDataByAddressName(Session["CompId"].ToString(), txtAddressName.Text);
        if (dtadd2.Rows.Count > 0)
        {
            txtAddressName.Text = "";
            DisplayMessage("Address Name Already Exists");
            System.Web.UI.ScriptManager.GetCurrent(this).SetFocus(txtAddressName);
            return;
        }
        else
        {
            b = AM.InsertAddressMaster(Session["CompId"].ToString(), ddlAddressCategory.SelectedValue, txtAddressNameNew.Text, txtAddress.Text, txtStreet.Text, txtBlock.Text, txtAvenue.Text, strCountryId, txtState.Text, txtCity.Text, txtPinCode.Text, txtPhoneNo1.Text, txtPhoneNo2.Text, txtMobileNo1.Text, txtMobileNo2.Text, txtEmailId1.Text, txtEmailId2.Text, txtFaxNo.Text, txtWebsite.Text, strLongitude, strLatitude, "", "", "", "", "", "True", DateTime.Now.ToString(), "True", Session["UserId"].ToString(), DateTime.Now.ToString(), Session["UserId"].ToString(), DateTime.Now.ToString());
            if (b != 0)
            {
                DisplayMessage("Record Saved");
                txtAddressName.Text = txtAddressNameNew.Text;
                ResetAddress();
                pnlAddress1.Visible = false;
                pnlAddress2.Visible = false;
                System.Web.UI.ScriptManager.GetCurrent(this).SetFocus(ddlAddressCategory);
            }
            else
            {
                DisplayMessage("Record  Not Saved");
            }
        }
    }
  
    protected void btnAddressReset_Click(object sender, EventArgs e)
    {
        ResetAddress();
        System.Web.UI.ScriptManager.GetCurrent(this).SetFocus(ddlAddressCategory);
    }
    protected void btnAddressCancel_Click(object sender, EventArgs e)
    {
        ResetAddress();
        pnlAddress1.Visible = false;
        pnlAddress2.Visible = false;
    }
   
    protected void ResetAddress()
    {
        FillAddressCategory();
        txtAddressNameNew.Text = "";
        txtAddress.Text = "";
        txtStreet.Text = "";
        txtBlock.Text = "";
        txtAvenue.Text = "";
        FillCountry();
        txtState.Text = "";
        txtCity.Text = "";
        txtPinCode.Text = "";
        txtPhoneNo1.Text = "";
        txtPhoneNo2.Text = "";
        txtMobileNo1.Text = "";
        txtMobileNo2.Text = "";
        txtEmailId1.Text = "";
        txtEmailId2.Text = "";
        txtFaxNo.Text = "";
        txtWebsite.Text = "";
        txtLongitude.Text = "";
        txtLatitude.Text = "";

        System.Web.UI.ScriptManager.GetCurrent(this).SetFocus(ddlAddressCategory);
    }
    public bool CheckEmailId1(string EmailAddress)
    {
        return Regex.IsMatch(txtEmailId1.Text,
                      "\\w+([-+.']\\+)*@\\w+([-.]\\+)*\\.\\w+([-.]\\+)*");
    }
    public bool CheckEmailId2(string EmailAddress)
    {
        return Regex.IsMatch(txtEmailId2.Text,
                      "\\w+([-+.']\\+)*@\\w+([-.]\\+)*\\.\\w+([-.]\\+)*");
    }

    private void FillAddressCategory()
    {
        DataTable dsAddressCat = null;
        dsAddressCat = ObjAddressCat.GetAddressCategoryAll(Session["CompId"].ToString());
        if (dsAddressCat.Rows.Count > 0)
        {
            ddlAddressCategory.DataSource = dsAddressCat;
            ddlAddressCategory.DataTextField = "Address_Name";
            ddlAddressCategory.DataValueField = "Address_Category_Id";
            ddlAddressCategory.DataBind();
            ListItem li = new ListItem("--Select--", "0");
            ddlAddressCategory.Items.Insert(0, li);
            ddlAddressCategory.SelectedIndex = 0;
        }
        else
        {
            ddlAddressCategory.Items.Add("--Select--");

        }
    }
    private void FillCountry()
    {
        CountryMaster objCountry = new CountryMaster();
        DataTable dsCountry = null;
        dsCountry = objCountry.GetCountryMaster();
        if (dsCountry.Rows.Count > 0)
        {
            ddlCountry.DataSource = dsCountry;
            ddlCountry.DataTextField = "Country_Name";
            ddlCountry.DataValueField = "Country_Id";
            ddlCountry.DataBind();
            ListItem li = new ListItem("--Select--", "0");
            ddlCountry.Items.Insert(0, li);
            ddlCountry.SelectedIndex = 0;
        }
        else
        {
            ListItem li = new ListItem("--Select--", "0");
            ddlCountry.Items.Insert(0, li);
            ddlCountry.SelectedIndex = 0;
        }
    }
  
    protected void btnClosePanel_Click(object sender, EventArgs e)
    {
        pnlAddress1.Visible = false;
        pnlAddress2.Visible = false;
    }
    #endregion

    [System.Web.Services.WebMethodAttribute(), System.Web.Script.Services.ScriptMethodAttribute()]
    public static string[] GetCompletionListAddressName(string prefixText, int count, string contextKey)
    {
        Set_AddressMaster AddressN = new Set_AddressMaster();
        DataTable dt = AddressN.GetDistinctAddressName(HttpContext.Current.Session["CompId"].ToString(), prefixText);



        string[] str = new string[dt.Rows.Count];
        if (dt.Rows.Count > 0)
        {
            for (int i = 0; i < dt.Rows.Count; i++)
            {
                str[i] = dt.Rows[i]["Address_Name"].ToString();
            }
        }
        else
        {
            if (prefixText.Length > 2)
            {
                str = null;
            }
            else
            {
                dt = AddressN.GetAddressAllData(HttpContext.Current.Session["CompId"].ToString());
                if (dt.Rows.Count > 0)
                {
                    str = new string[dt.Rows.Count];
                    for (int i = 0; i < dt.Rows.Count; i++)
                    {
                        str[i] = dt.Rows[i]["Address_Name"].ToString();
                    }
                }
            }
        }
        return str;
    }

    [System.Web.Services.WebMethodAttribute(), System.Web.Script.Services.ScriptMethodAttribute()]
    public static string[] GetCompletionListNewAddress(string prefixText, int count, string contextKey)
    {
        Set_AddressMaster Address = new Set_AddressMaster();
        DataTable dt = Address.GetDistinctAddressName(HttpContext.Current.Session["CompId"].ToString(), prefixText);

        string[] str = new string[dt.Rows.Count];
        for (int i = 0; i < dt.Rows.Count; i++)
        {
            str[i] = dt.Rows[i]["Address_Name"].ToString();
        }
        return str;
    }

    [System.Web.Services.WebMethodAttribute(), System.Web.Script.Services.ScriptMethodAttribute()]
    public static string[] GetCompletionListBankName(string prefixText, int count, string contextKey)
    {
        Set_BankMaster objBankMaster = new Set_BankMaster();
        DataTable dt = new DataView(objBankMaster.GetBankMaster(HttpContext.Current.Session["CompId"].ToString()), "Bank_Name like '" + prefixText.ToString() + "%'", "", DataViewRowState.CurrentRows).ToTable();
        string[] txt = new string[dt.Rows.Count];

        for (int i = 0; i < dt.Rows.Count; i++)
        {
            txt[i] = dt.Rows[i]["Bank_Name"].ToString();
        }
        return txt;
    }

    protected void btnConnect_Click(object sender, EventArgs e)
    {
        int fileType = -1;

        if (fileLoad.HasFile)
        {
            string ext = fileLoad.FileName.Substring(fileLoad.FileName.Split('.')[0].Length);
            if ((ext != ".xls") && (ext != ".xlsx") && (ext != ".mdb") && (ext != ".accdb"))
            {
                Literal l4 = new Literal();
                l4.Text = @"<font size=4 color=red></font><script>alert(""Please load a excel/access file"");</script></br></br>";
                this.Controls.Add(l4);
                return;
            }

            if (ext == ".xls")
            {
                fileType = 0;
            }
            if (ext == ".xlsx")
            {
                fileType = 1;
            }
            if (ext == ".mdb")
            {
                fileType = 2;
            }
            if (ext == ".accdb")
            {
                fileType = 3;
            }
            string path = Server.MapPath("~/CompanyResource/" + Session["CompId"].ToString() + "/" + fileLoad.FileName);
            fileLoad.SaveAs(path);

            //DataTable dt//
            Import(path, fileType);
            //if (dt != null)
            //{
            //  //  gvLoadContact.DataSource = dt;
            //   // Session["dtContact"] = dt;
            //   // gvLoadContact.DataBind();
            //}
            Literal l5 = new Literal();
            l5.Text = @"<font size=4 color=red></font><script>alert(""file succesfully uploaded"");</script></br></br>";
            this.Controls.Add(l5);
        }
        else
        {
            Literal l4 = new Literal();
            l4.Text = @"<font size=4 color=red></font><script>alert(""Please load a  file"");</script></br></br>";
            this.Controls.Add(l4);
        }
        //tr0.Visible = true;


    }

    public void Import(String path, int fileType)
    {

        string strcon = string.Empty;

        if (fileType == 1)
        {
            Session["filetype"] = "excel";
            strcon = "Provider=Microsoft.ACE.OLEDB.12.0;" + "Data Source=" + @"'" + path + "';" + "Extended Properties=\"Excel 12.0;HDR=NO;iMEX=1\"";

        }
        else if (fileType == 0)
        {
            Session["filetype"] = "excel";
            strcon = "Provider=Microsoft.Jet.OLEDB.4.0;" + "Data Source=" + @"'" + path + "';" + "Extended Properties=\"Excel 8.0;HDR=NO;iMEX=1\"";
        }
        else
        {
            Session["filetype"] = "access";
            //Provider=Microsoft.ACE.OLEDB.12.0;Data Source=d:/abc.mdb;Persist Security Info=False
            strcon = "Provider=Microsoft.ACE.OLEDB.12.0;" + "Data Source=" + @"'" + path + "';" + "Persist Security Info=False";

        }

        Session["cnn"] = strcon;



        OleDbConnection conn = new OleDbConnection(strcon);
        conn.Open();

        DataTable tables = conn.GetOleDbSchemaTable(OleDbSchemaGuid.Tables, new object[] { null, null, null, "TABLE" });
        ddlTables.DataSource = tables;

        ddlTables.DataTextField = "TABLE_NAME";
        ddlTables.DataValueField = "TABLE_NAME";
        ddlTables.DataBind();
        // OleDbDataAdapter da = new OleDbDataAdapter("SELECT * FROM [Sheet1$]", conn);
        //create new dataset
        //  DataSet ds = new DataSet();
        // fill dataset
        // da.Fill(ds);
        //populate grid with data
        //this.gvLoadContact.DataSource = ds.Tables[0];
        ////close connection
        conn.Close();

        //return ds.Tables[0];
    }
    protected void btnviewcolumns_Click(object sender, EventArgs e)
    {
        OleDbConnection cnn = new OleDbConnection(Session["cnn"].ToString());
        OleDbDataAdapter adp = new OleDbDataAdapter("", "");
        adp.SelectCommand.CommandText = "Select *  From [" + ddlTables.SelectedValue.ToString() + "]";
        adp.SelectCommand.Connection = cnn;

        DataTable userTable = new DataTable();
        try
        {
            adp.Fill(userTable);
        }
        catch (Exception)
        {

            Literal l4 = new Literal();
            l4.Text = @"<font size=4 color=red></font><script>alert(""Error in Mapping File"");</script></br></br>";
            this.Controls.Add(l4);
            return;
        }
        Session["SourceData"] = userTable;
        DataTable dtcolumn = new DataTable();
        dtcolumn.Columns.Add("COLUMN_NAME");
        dtcolumn.Columns.Add("COLUMN");
        for (int i = 0; i < userTable.Columns.Count; i++)
        {
            dtcolumn.Rows.Add(dtcolumn.NewRow());
            if (Session["filetype"].ToString() != "excel")
            {
                dtcolumn.Rows[dtcolumn.Rows.Count - 1]["COLUMN_NAME"] = userTable.Columns[i].ToString();
                dtcolumn.Rows[dtcolumn.Rows.Count - 1]["COLUMN"] = userTable.Columns[i].ToString();
            }
            else
            {
                dtcolumn.Rows[dtcolumn.Rows.Count - 1]["COLUMN_NAME"] = userTable.Rows[0][i].ToString();
                dtcolumn.Rows[dtcolumn.Rows.Count - 1]["COLUMN"] = userTable.Columns[i].ToString();
            }
        }

        Session["SourceTbl"] = dtcolumn;
        //get destination table field 
        DataTable dtDestinationDt = objEmp.GetFieldName(chkNecField.Checked);



        gvFieldMapping.DataSource = dtDestinationDt;
        gvFieldMapping.DataBind();


        //get source field
        pnlUpload1.Visible = false;
        pnlMap.Visible = true;
        pnlUpload1.Visible = false;

    }

    protected void btnUpload_Click2(object sender, EventArgs e)
    {

        string query = "";
        //// get columns name
        DataTable dtSource = (DataTable)Session["SourceData"];



        DataTable dtDestTemp = new DataTable();

        for (int col = 0; col < gvFieldMapping.Rows.Count; col++)
        {
            if (((DropDownList)gvFieldMapping.Rows[col].FindControl("ddlExcelCol")).SelectedValue != "0")
            {
                dtDestTemp.Columns.Add(((Label)gvFieldMapping.Rows[col].FindControl("lblColName")).Text);


            }

        }

        for (int rowcountr = 0; rowcountr < dtSource.Rows.Count; rowcountr++)
        {
            dtDestTemp.Rows.Add(dtDestTemp.NewRow());

            for (int i = 0; i < gvFieldMapping.Rows.Count; i++)
            {
                if (((DropDownList)gvFieldMapping.Rows[i].FindControl("ddlExcelCol")).SelectedValue != "0")
                {
                    dtDestTemp.Rows[rowcountr][((Label)gvFieldMapping.Rows[i].FindControl("lblColName")).Text] = dtSource.Rows[rowcountr][((DropDownList)gvFieldMapping.Rows[i].FindControl("ddlExcelCol")).SelectedValue].ToString();

                }

            }

        }
        //EEmpFirstName','DOJ','DepartmentId','DesignationId','DOB','BrandId','LocationId','EmpId
        if (dtDestTemp.Columns.Contains("Emp_Name") && dtDestTemp.Columns.Contains("DOJ") && dtDestTemp.Columns.Contains("Department_Id") && dtDestTemp.Columns.Contains("Designation_Id") && dtDestTemp.Columns.Contains("DOB") && dtDestTemp.Columns.Contains("Brand_Id") && dtDestTemp.Columns.Contains("Location_Id") && dtDestTemp.Columns.Contains("Emp_Id"))
        {
            ddlFiltercol.DataSource = dtDestTemp.Columns;
        }
        else
        {
            DisplayMessage("Map all Necessary Field");

            return;

        }



        pnlshowdata.Visible = true;
        pnlUpload1.Visible = false;
        pnlMap.Visible = false;
        //ddlFiltercol.DataTextField = "Column_Name";
        //ddlFiltercol.DataValueField = "Column_Name";
        ddlFiltercol.DataBind();

        Session["dtDest"] = dtDestTemp;

        gvSelected.DataSource = dtDestTemp;
        gvSelected.DataBind();
    }
    protected void gvFieldMapping_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            string nec = gvFieldMapping.DataKeys[e.Row.RowIndex]["Nec"].ToString();
            if (nec.Trim() == "1")
            {
                ((Label)e.Row.FindControl("lblCompulsery")).Text = "*";
                ((Label)e.Row.FindControl("lblCompulsery")).ForeColor = System.Drawing.Color.Red;
            }
            DropDownList ddl = ((DropDownList)e.Row.FindControl("ddlExcelCol"));
            binddropdownlist(ddl);

        }

    }

    private void binddropdownlist(DropDownList ddl)
    {
        DataTable dt = (DataTable)Session["SourceTbl"];

        string filetype = Session["filetype"].ToString();
        int startingrow = 0;
        if (filetype == "excel")
            startingrow = 1;
        ListItem lst = new ListItem("--select one--", "0");

        if (ddl != null)
        {
            ddl.Items.Insert(0, lst);
            for (int i = 0; i < dt.Rows.Count; i++)
            {
                lst = new ListItem(dt.Rows[i]["COLUMN_NAME"].ToString(), dt.Rows[i]["COLUMN"].ToString());
                ddl.Items.Insert(i + 1, lst);
                //lst=new ListItem()
            }
        }

    }
    protected void btncancel_Click(object sender, EventArgs e)
    {
        pnlUpload1.Visible = true;
        pnlMap.Visible = false;
        pnlshowdata.Visible = false;


    }
    protected void btnFilter_Click(object sender, EventArgs e)
    {
        DataTable dt = (DataTable)Session["dtDest"];

        dt = new DataView(dt, "" + ddlFiltercol.SelectedValue + "='" + txtfiltercol.Text + "'", "", DataViewRowState.CurrentRows).ToTable();
        gvSelected.DataSource = dt;
        gvSelected.DataBind();
    }
    protected void btnresetgv_Click(object sender, EventArgs e)
    {
        pnlshowdata.Visible = false;
        pnlMap.Visible = true;
        pnlUpload1.Visible = false;

        txtfiltercol.Text = "";
        //btnUpload_Click(null, null);

        // trnew.Visible = false;

    }

    public void SetAllId()
    {



        objEmp.CompanyId = Session["CompId"].ToString();
        objEmp.Gender = "Male";

        objEmp.NationalityId = "0";
        objEmp.ReligionId = "0";
        objEmp.Service_Status = "Active";
        objEmp.EEmpFirstName = "";


        objEmp.CivilId = "0";
        objEmp.CreatedBy = Session["UserId"].ToString();
        objEmp.CreatedDate = DateTime.Now.ToString();

        objEmp.DepartmentId = "0";



        objEmp.DOL = "";
        objEmp.Education = "";




        objEmp.EmailId = "";

        objEmp.EmpType = "On Role";

        objEmp.NationalityId = "0";
        objEmp.ReligionId = "0";


        objEmp.IsActive = true.ToString();







        objEmp.ModifiedBy = Session["UserId"].ToString();
        objEmp.ModifiedDate = DateTime.Now.ToString();


        objEmp.PhoneNo = "123";

        objEmp.ReligionId = "0";


    }

    protected void btnUpload_Click1(object sender, EventArgs e)
    {
        string empids = "";

        int Insertedrowcount = 0;
        string compid = Session["CompId"].ToString();
        for (int rowcounter = 1; rowcounter < gvSelected.Rows.Count; rowcounter++)
        {
            SetAllId();
            for (int col = 0; col < gvSelected.Rows[rowcounter].Cells.Count; col++)
            {

                string colname = gvSelected.HeaderRow.Cells[col].Text;

                string colval = gvSelected.Rows[rowcounter].Cells[col].Text;
                colval = colval.Replace("&#160;", "");
                colval = colval.Replace("&nbsp;", "");






                if (colname == "Emp_Name")
                {


                    objEmp.EEmpFirstName = colval;

                }
                else if (colname == "DOJ")
                {
                    if (colval == "")
                    {
                        colval = DateTime.Now.ToString();
                    }
                    objEmp.DOJ = colval;
                }
                else if (colname == "Department_Id")
                {
                    if (colval != "")
                    {
                        objEmp.DepartmentId = colval;
                    }
                }
                else if (colname == "Designation_Id")
                {
                    if (colval != "")
                    {
                        objEmp.DesignationId = colval;
                    }
                }
                else if (colname == "Nationality_Id")
                {
                    if (colval != "")
                    {
                        objEmp.NationalityId = colval;
                    }
                }
                else if (colname == "Gender")
                {
                    objEmp.Gender = colval;
                }
                else if (colname == "DOB")
                {
                    if (colval == "")
                    {
                        colval = DateTime.Now.ToString();
                    }

                    objEmp.DOB = colval;
                }


                else if (colname == "Religion_Id")
                {
                    if (colval != "")
                    {
                        objEmp.ReligionId = colval;
                    }
                }

                else if (colname == "Brand_Id")
                {
                    objEmp.BrandId = colval;
                }
                else if (colname == "Location_Id")
                {
                    objEmp.LocationId = colval;
                }
                else if (colname == "Emp_Id")
                {
                    objEmp.EmpId = colval;
                }

                else if (colname == "Nationality_Id")
                {
                    if (colval != "")
                    {
                        objEmp.NationalityId = colval;
                    }
                }
                else if (colname == "Gender")
                {
                    objEmp.Gender = colval;
                }
                else if (colname == "DOB")
                {
                    objEmp.DOB = colval;
                }


                else if (colname == "Religion_Id")
                {
                    if (colval != "")
                    {
                        objEmp.ReligionId = colval;
                    }
                }
                else if (colname == "Company_Id")
                {
                    objEmp.CompanyId = colval;
                }
                else if (colname == "Brand_Id")
                {
                    objEmp.BrandId = colval;
                }
                else if (colname == "Location_Id")
                {
                    if (colval != "")
                    {


                        objEmp.LocationId = colval;
                    }
                }




                else if (colname == "Civil_Id")
                {
                    if (colval != "")
                    {

                        objEmp.CivilId = colval;
                    }
                }






                else if (colname == "Phone_No")
                {
                    objEmp.PhoneNo = colval;
                }
            }

            if (objEmp.EmpId == "")
            {
                continue;
            }


            DataTable dtEmp = objEmp.GetEmployeeMasterByEmpCode(Session["CompId"].ToString(), objEmp.EmpId);
            int b = 0;
            string EmpSync = string.Empty;
            EmpSync = objAppParam.GetApplicationParameterValueByParamName("Employee Synchronization", Session["CompId"].ToString());
            DataTable dtDevice = new DataTable();
            if (dtEmp.Rows.Count > 0)
            {
                b = 0;
            }
            else
            {
                b = objEmp.SaveEmpData();
                objEmpParam.InsertEmployeeParameterOnEmployeeInsert(Session["CompId"].ToString(), b.ToString(), Session["UserId"].ToString(), DateTime.Now.ToString());

                if (EmpSync == "Company")
                {
                    dtDevice = objDevice.GetDeviceMaster(Session["CompId"].ToString());

                    if (dtDevice.Rows.Count > 0)
                    {

                        objSer.DeleteUserTransfer(b.ToString());
                        for (int i = 0; i < dtDevice.Rows.Count; i++)
                        {
                            objSer.InsertUserTransfer(b.ToString(), dtDevice.Rows[i]["Device_Id"].ToString(), false.ToString(), DateTime.Now.ToString(), "1/1/1900", "", "", "", "", "", true.ToString(), DateTime.Now.ToString(), true.ToString(), Session["UserId"].ToString(), DateTime.Now.ToString(), Session["UserId"].ToString(), DateTime.Now.ToString());

                        }


                    }


                }
                else
                {
                    dtDevice = objDevice.GetDeviceMaster(Session["CompId"].ToString());
                    dtDevice = new DataView(dtDevice, "Location_Id='" + Session["LocId"].ToString() + "'", "", DataViewRowState.CurrentRows).ToTable();

                    if (dtDevice.Rows.Count > 0)
                    {


                        for (int i = 0; i < dtDevice.Rows.Count; i++)
                        {
                            objSer.InsertUserTransfer(b.ToString(), dtDevice.Rows[i]["Device_Id"].ToString(), false.ToString(), DateTime.Now.ToString(), "1/1/1900", "", "", "", "", "", true.ToString(), DateTime.Now.ToString(), true.ToString(), Session["UserId"].ToString(), DateTime.Now.ToString(), Session["UserId"].ToString(), DateTime.Now.ToString());

                        }


                    }
                }

            }


            if (b != 0)
            {
                Insertedrowcount++;
            }
            else
            {
                empids += "'" + objEmp.EmpId + "'" + ",";
            }

        }


        string emp = empids;

        if (emp != "")
        {

            DataTable dtemp = (DataTable)Session["dtDest"];



            emp = emp.Substring(0, emp.Length - 1);


            dtemp = new DataView(dtemp, "Emp_Id in(" + emp + ")", "", DataViewRowState.CurrentRows).ToTable();



            gvSelected.DataSource = dtemp;
            gvSelected.DataBind();
        }






        ScriptManager.RegisterStartupScript(this, GetType(), "", "alert('" + Insertedrowcount + " Row Inserted and Following Records Already Exists')", true);


    }


    protected void btnbacktoExcel_Click(object sender, EventArgs e)
    {
        pnlUpload1.Visible = true;
        //trmap.Visible = false;
    }
    protected void btnBackToMapData_Click(object sender, EventArgs e)
    {
        pnlshowdata.Visible = false;
        pnlUpload1.Visible = true;
        pnlMap.Visible = false;
        //trmap.Visible = false;
        //trnew.Visible = false;

    }

}
