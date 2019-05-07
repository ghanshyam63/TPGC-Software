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

public partial class Arca_Wing_PayEmployeePenalty : System.Web.UI.Page
{
    #region defind Class Object


    Common ObjComman = new Common();

    Pay_Employee_Month objPayEmpMonth = new Pay_Employee_Month();
    Pay_Employee_Penalty ObjPenalty = new Pay_Employee_Penalty();

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
    EmployeeMaster ObjEmployee = new EmployeeMaster();
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

        TxtPenaltyName.Focus();
            


    }
    void GridBind_PenaltyList(string Month,string Year)
    {
        DataTable Dt = new DataTable();
        Dt = ObjPenalty.GetRecord_From_PayEmployeePenalty(Session["CompId"].ToString(), "0", "0",Month, Year, "", "");
        if (Dt.Rows.Count > 0)
        {
            GridViewPenaltyList.DataSource = Dt;
            GridViewPenaltyList.DataBind();
           
        }
        else
        {
            Dt.Clear();
            GridViewPenaltyList.DataSource = Dt;
            GridViewPenaltyList.DataBind();
            DisplayMessage("Record Not found");
            DdlMonthList.Focus();

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
        TxtPenaltyName.Focus();
 
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
        TxtPenaltyName.Text = "";
        TxtPenaltyDiscription.Text = "";
        ddlMonth.SelectedIndex = 0;
        txtCalValue.Text = "";
        DdlValueType.SelectedIndex = 0;
        TxtYear.Text = "";
        TxtPenaltyName.Focus();
        TxtYear.Text = DateTime.Now.Year.ToString();
        
        int CurrentMonth =Convert.ToInt32(DateTime.Now.Month.ToString());

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
                        b = ObjPenalty.Insert_In_Pay_Employee_Penalty(Session["CompId"].ToString(), str, TxtPenaltyName.Text.Trim(), TxtPenaltyDiscription.Text.Trim(), DdlValueType.SelectedValue, txtCalValue.Text, DateTime.Now.ToString(), ddlMonth.SelectedValue, TxtYear.Text, "", "", "", "", "", true.ToString(), DateTime.Now.ToString(), true.ToString(), Session["UserId"].ToString(), DateTime.Now.ToString(), Session["UserId"].ToString(), DateTime.Now.ToString());
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
            if (TxtPenaltyName.Text == "")
            {
                DisplayMessage("Enter Penalty Name");
                TxtPenaltyName.Focus();
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

                        b = ObjPenalty.Insert_In_Pay_Employee_Penalty(Session["CompId"].ToString(), str, TxtPenaltyName.Text.Trim(), TxtPenaltyDiscription.Text.Trim(), DdlValueType.SelectedValue, txtCalValue.Text, DateTime.Now.ToString(), ddlMonth.SelectedValue, TxtYear.Text, "", "", "", "", "", true.ToString(), DateTime.Now.ToString(), true.ToString(), Session["UserId"].ToString(), DateTime.Now.ToString(), Session["UserId"].ToString(), DateTime.Now.ToString());

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

        // Reset();



    }

    protected void btnReset_Click(object sender, EventArgs e)
    {
        Reset();
    }
    protected void btnCancel_Click(object sender, EventArgs e)
    {
        Reset();
    }
    protected void btnLeast_Click(object sender, EventArgs e)
    {
        PanelList.BackColor = System.Drawing.ColorTranslator.FromHtml("#ccddee");

        pnlLeave.BackColor = System.Drawing.ColorTranslator.FromHtml("#90bde9");

        DdlMonthList.Focus();
        PanelSearchList.Visible = true;
        PnlEmployeeLeave.Visible = false;
        int CurrentMonth = Convert.ToInt32(DateTime.Now.Month);
        DdlMonthList.SelectedValue = (CurrentMonth).ToString();
        TxtYearList.Text = DateTime.Now.Year.ToString();
        GridBind_PenaltyList(DdlMonthList.SelectedValue, TxtYearList.Text);
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
        GridBind_PenaltyList(DdlMonthList.SelectedValue, TxtYearList.Text);
        BtnRefreshList.Focus();
    }
    protected void BtnRefreshList_Click(object sender, ImageClickEventArgs e)
    {
        int CurrentMonth = Convert.ToInt32(DateTime.Now.Month);
        DdlMonthList.SelectedValue = (CurrentMonth).ToString();
        TxtYearList.Text = DateTime.Now.Year.ToString();
        GridBind_PenaltyList(DdlMonthList.SelectedValue, TxtYearList.Text);
        DdlMonthList.Focus();

    }
    protected void btnEdit_command(object sender, CommandEventArgs e)
    {
        PanelUpdatePenalty.Visible = true;
       HiddeniD.Value = e.CommandArgument.ToString();
        DataTable Dt = new DataTable();
        Dt = ObjPenalty.GetRecord_From_PayEmployeePenalty_usingPenaltyId(Session["CompId"].ToString(), "0",HiddeniD.Value, "0", "0", "", "");
        if (Dt.Rows.Count > 0)
        {
            TxtPenaltyNameList.Text = Dt.Rows[0]["Penalty_Name"].ToString();
            TxtPenaltyDiscList.Text=Dt.Rows[0]["Penalty_Description"].ToString();
            DdlvalueTypelist.SelectedValue = Dt.Rows[0]["value_Type"].ToString();
            Txtvaluelist.Text = Dt.Rows[0]["Value"].ToString();
            DdlMonthListPanel.SelectedValue = Dt.Rows[0]["Penalty_Month"].ToString();
            TxtpanelYearList.Text = Dt.Rows[0]["Penalty_Year"].ToString();
            txtEmployeeId.Text = Dt.Rows[0]["Emp_Id"].ToString();
            txtEmployeeName.Text = Dt.Rows[0]["Emp_Name"].ToString();
            TxtPenaltyNameList.Focus();
        }

    }
    protected void IbtnDelete_command(object sender, CommandEventArgs e)
    {
        string PenaltyId=e.CommandArgument.ToString();
        int CheckDeletion = 0;
        CheckDeletion = ObjPenalty.DeleteRecord_in_Pay_Employee_penalty(Session["CompId"].ToString(), PenaltyId, "False", Session["UserId"].ToString(), DateTime.Now.ToString());
        if (CheckDeletion != 0)
        {
            DisplayMessage("Record Deleted");
            DataTable dtGrid = new DataTable();
            dtGrid = ObjPenalty.GetRecord_From_PayEmployeePenalty(Session["CompId"].ToString(), "0", "0", DdlMonthList.SelectedValue, TxtYear.Text, "", "");
            GridViewPenaltyList.DataSource = dtGrid;
            GridViewPenaltyList.DataBind();
        }
        else
        {
            DisplayMessage("Record Not Deleted");
        }
    }
    void ResetPanel()
    {
        TxtPenaltyNameList.Text = "";
        TxtPenaltyDiscList.Text = "";
        DdlvalueTypelist.SelectedIndex = 0;
        Txtvaluelist.Text = "";
        DdlMonthListPanel.SelectedIndex = 0;
        TxtpanelYearList.Text = "";
        HiddeniD.Value = "";
        PanelUpdatePenalty.Visible = false;
        txtEmployeeId.Text = "";
        txtEmployeeName.Text = "";




    }

    protected void BtnUpdate_Click(object sender, EventArgs e)
    {

        if (HiddeniD.Value == "")
        {
            
            DisplayMessage("Edit The Record");
            DdlMonthList.Focus();
            return;
        }
        int UpdationCheck = 0;
        UpdationCheck = ObjPenalty.UpdateRecord_In_Pay_Employee_Penalty(Session["CompId"].ToString(), HiddeniD.Value, TxtPenaltyNameList.Text, TxtPenaltyDiscList.Text, DdlvalueTypelist.SelectedValue, Txtvaluelist.Text, DdlMonthListPanel.SelectedValue, TxtpanelYearList.Text, Session["UserId"].ToString(), DateTime.Now.ToString());

        if (UpdationCheck != 0)
        {
            DisplayMessage("Record Updated");
           
            DataTable dtGrid = new DataTable();
            dtGrid = ObjPenalty.GetRecord_From_PayEmployeePenalty(Session["CompId"].ToString(), "0", "0", DdlMonthList.SelectedValue, TxtYear.Text, "", "");
            GridViewPenaltyList.DataSource = dtGrid;
            GridViewPenaltyList.DataBind();
            DdlMonthList.Focus();
            ResetPanel();

        }
        else
        {
            DisplayMessage("Record Not Updated");
        }
    }

    protected void GridViewPenaltyList_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        GridViewPenaltyList.PageIndex = e.NewPageIndex;
        GridBind_PenaltyList(DdlMonthList.SelectedValue, TxtYearList.Text);
    }

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
    [System.Web.Services.WebMethodAttribute(), System.Web.Script.Services.ScriptMethodAttribute()]
   
    public static string[] GetPenaltyName(string prefixText, int count, string contextKey)
    {
        Pay_Employee_Penalty objpenalty = new Pay_Employee_Penalty();
        DataTable dt = objpenalty.GetPenaltyName("", HttpContext.Current.Session["CompId"].ToString());

        dt = new DataView(dt, "Penalty_Name lIKE '" + prefixText + "%'", "", DataViewRowState.CurrentRows).ToTable();


        string[] str = new string[dt.Rows.Count];
        for (int i = 0; i < dt.Rows.Count; i++)
        {
            str[i] = dt.Rows[i][0].ToString();
        }
        return str;
    }
}
