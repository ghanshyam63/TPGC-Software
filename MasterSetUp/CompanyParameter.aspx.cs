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

public partial class MasterSetUp_CompanyParameter : BasePage
{
    CompanyMaster objComp = new CompanyMaster();
    SystemParameter objSys = new SystemParameter();
    Att_ShiftManagement objShift = new Att_ShiftManagement();
    Set_ApplicationParameter objAppParam = new Set_ApplicationParameter();
    protected void Page_Load(object sender, EventArgs e)
    {


        if (Session["UserId"] == null)
        {
            Response.Redirect("~/ERPLogin.aspx");
        }

        objSys.GetSysTitle();
        if (!IsPostBack)
        {
            
            if (Request.QueryString["CompanyId"] != null)
            {

                btnTime_Click(null, null);

                string compid = Request.QueryString["CompanyId"].ToString().Substring(1, Request.QueryString["CompanyId"].ToString().Length - 2);
                DataTable dtComp = objComp.GetCompanyMasterById(compid);
                if (dtComp.Rows.Count > 0)
                {
                    lblHeader.Text = dtComp.Rows[0]["Company_Name"].ToString();
                    FillShift(compid);
                    SetCompanyParameter(compid);
                    hdnCompanyId.Value = compid;
                    
                }
                else
                {
                    lblHeader.Text = "";
                }

            }

        }
        objSys.GetSysTitle();
    }

    public void FillShift(string compid)
    {
        DataTable dt = objShift.GetShiftMaster(compid);
        dt = new DataView(dt, "Brand_Id='" + Session["BrandId"].ToString() + "'", "", DataViewRowState.CurrentRows).ToTable();

        if (dt.Rows.Count > 0)
        {
            ddlDefaultShift.DataSource = dt;
            ddlDefaultShift.DataTextField = "Shift_Name";
            ddlDefaultShift.DataValueField = "Shift_Id";
            ddlDefaultShift.DataBind();
            ListItem lst = new ListItem("--Select--", "0");
            ddlDefaultShift.Items.Insert(0, lst);


        }
        else
        {
            ddlDefaultShift.DataSource = null;
            
            ddlDefaultShift.DataBind();
            ListItem lst = new ListItem("--Select--", "0");
            ddlDefaultShift.Items.Insert(0, lst);


        }


    }

    public void SetCompanyParameter(string CompId)
    {

        txtShortestTime.Text = objAppParam.GetApplicationParameterValueByParamName("Shortest Time Table", CompId);
        txtMinDiffTime.Text = objAppParam.GetApplicationParameterValueByParamName("Min Difference Between TimeTable in Shift", CompId);
        txtWorkDayMin.Text = objAppParam.GetApplicationParameterValueByParamName("Work Day Min", CompId);
        txtServiceRunTime.Text = objAppParam.GetApplicationParameterValueByParamName("Service_Run_Time", CompId);
       
        ddlExculeDay.SelectedValue = objAppParam.GetApplicationParameterValueByParamName("Exclude Day As Absent or IsOff", CompId);
        ddlFinancialYear.SelectedValue = objAppParam.GetApplicationParameterValueByParamName("FinancialYearStartMonth", CompId);
        ddlEmpSync.SelectedValue = objAppParam.GetApplicationParameterValueByParamName("Employee Synchronization", CompId);



        if (objAppParam.GetApplicationParameterValueByParamName("Display TimeTable In All Brand", CompId) == "True")
        {
            rbtnBrandYes.Checked = true;
        }
        else
        {
            rbtnBrandNo.Checked = true;
        }

        for (int i = 0; i < ChkWeekOffList.Items.Count; i++)
        {

            ChkWeekOffList.Items[i].Selected = false;
        }
        for (int i = 0; i < ChkWeekOffList.Items.Count; i++)
        {
            if (ChkWeekOffList.Items[i].Text == objAppParam.GetApplicationParameterValueByParamName("Week Off Days", CompId))
            {
                ChkWeekOffList.Items[i].Selected = true;
            }
            else
            {
                ChkWeekOffList.Items[i].Selected = false;
            }


        }


        if (Convert.ToBoolean(objAppParam.GetApplicationParameterValueByParamName("IsOverTime", CompId)))
        {
            rbtnOtEnable.Checked = true;
            rbtnOtDisable.Checked = false;
            rbtOT_OnCheckedChanged(null, null);
        }
        else
        {
            rbtnOtEnable.Checked = false;
            rbtnOtDisable.Checked = true;
            rbtOT_OnCheckedChanged(null, null);
        }

        txtMaxOTMint.Text = objAppParam.GetApplicationParameterValueByParamName("Max Over Time Min", CompId);
        txtMinOTMint.Text = objAppParam.GetApplicationParameterValueByParamName("Min OVer Time Min", CompId);

        ddlCalculationMethod.SelectedValue = objAppParam.GetApplicationParameterValueByParamName("Over Time Calculation Method", CompId);

        ddlWorkCal.SelectedValue = objAppParam.GetApplicationParameterValueByParamName("Effective Work Calculation Method", CompId);
        ddlShiftPref.SelectedValue = objAppParam.GetApplicationParameterValueByParamName("Without Shift Preference", CompId);
        ddlSalCal.SelectedValue = objAppParam.GetApplicationParameterValueByParamName("Salary Calculate According To", CompId);

        ddlSalCal_OnSelectedIndexChanged(null, null);

        ddlPaySal.SelectedValue = objAppParam.GetApplicationParameterValueByParamName("Pay Salary Acc To Work Hour or Ref Hour", CompId);

        ddlPaySal_OnSelectedIndexChanged(null, null);


        txtFixedDays.Text = objAppParam.GetApplicationParameterValueByParamName("Days In Month", CompId);
        txtWorkPercentFrom1.Text = objAppParam.GetApplicationParameterValueByParamName("WorkPercentFrom1", CompId);
        txtWorkPercentFrom2.Text = objAppParam.GetApplicationParameterValueByParamName("WorkPercentFrom2", CompId);
        txtWorkPercentFrom3.Text = objAppParam.GetApplicationParameterValueByParamName("WorkPercentFrom3", CompId);
        txtWorkPercentTo1.Text = objAppParam.GetApplicationParameterValueByParamName("WorkPercentTo1", CompId);
        txtWorkPercentTo2.Text = objAppParam.GetApplicationParameterValueByParamName("WorkPercentTo2", CompId);
        txtWorkPercentTo3.Text = objAppParam.GetApplicationParameterValueByParamName("WorkPercentTo3", CompId);
        txtValue1.Text = objAppParam.GetApplicationParameterValueByParamName("Value1", CompId);

        txtValue2.Text = objAppParam.GetApplicationParameterValueByParamName("Value2", CompId);
        txtValue3.Text = objAppParam.GetApplicationParameterValueByParamName("Value3", CompId);


        if (Convert.ToBoolean(objAppParam.GetApplicationParameterValueByParamName("SMS_Enable", CompId)))
        {
            rbtnSMSEnable.Checked = true;
            rbtnSMSDisable.Checked = false;
            rbtSMS_OnCheckedChanged(null, null);
        }
        else
        {
            rbtnSMSEnable.Checked = false;
            rbtnSMSDisable.Checked = true;
            rbtSMS_OnCheckedChanged(null, null);
        }
        if (Convert.ToBoolean(objAppParam.GetApplicationParameterValueByParamName("Email_Enable", CompId)))
        {
            rbtnEmailEnable.Checked = true;
            rbtnEmailDisable.Checked = false;
            rbtEmail_OnCheckedChanged(null, null);
        }
        else
        {
            rbtnEmailEnable.Checked = false;
            rbtnEmailDisable.Checked = true;
            rbtEmail_OnCheckedChanged(null, null);
        }


        txtSMSAPI.Text = objAppParam.GetApplicationParameterValueByParamName("SMS_API", CompId);

        txtSmsPassword.Attributes.Add("Value", objAppParam.GetApplicationParameterValueByParamName("SMS_User_Password", CompId));

        txtUserId.Text = objAppParam.GetApplicationParameterValueByParamName("SMS_User_Id", CompId);

        txtSenderId.Text = objAppParam.GetApplicationParameterValueByParamName("Sender_Id", CompId);

        txtEmail.Text = objAppParam.GetApplicationParameterValueByParamName("Master_Email", CompId);

        txtPasswordEmail.Attributes.Add("Value", objAppParam.GetApplicationParameterValueByParamName("Master_Email_Password", CompId));

        txtSMTP.Text = objAppParam.GetApplicationParameterValueByParamName("Master_Email_SMTP", CompId);

        txtPort.Text = objAppParam.GetApplicationParameterValueByParamName("Master_Email_Port", CompId);
        try
        {
            chkEnableSSL.Checked = Convert.ToBoolean(objAppParam.GetApplicationParameterValueByParamName("Master_Email_EnableSSL", CompId));
        }
        catch
        {
            chkEnableSSL.Checked = false;
        }


        if (Convert.ToBoolean(objAppParam.GetApplicationParameterValueByParamName("Partial_Leave_Enable", CompId)))
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
        }

        txtTotalMinutes.Text = objAppParam.GetApplicationParameterValueByParamName("Total Partial Leave Minutes", CompId);
        txtMinuteday.Text = objAppParam.GetApplicationParameterValueByParamName("Partial Leave Minute Use In A Day", CompId);
        if (Convert.ToBoolean(objAppParam.GetApplicationParameterValueByParamName("Carry Forward Partial Leave Minutes", CompId)))
        {
            rbtnCarryYes.Checked = true;
            rbtnCarryNo.Checked = false;

        }
        else
        {
            rbtnCarryYes.Checked = false;
            rbtnCarryNo.Checked = true;
        }


        txtViolation.Text = objAppParam.GetApplicationParameterValueByParamName("Partial_Violation_Min", CompId);

        if (objAppParam.GetApplicationParameterValueByParamName("With Key Preference", CompId) == "Yes")
        {
            rbtnKeyEnable.Checked = true;
            rbtnKeyDisable.Checked = false;
            rbtKeyPref_OnCheckedChanged(null, null);
        }
        else
        {
            rbtnKeyEnable.Checked = false;
            rbtnKeyDisable.Checked = true;
            rbtKeyPref_OnCheckedChanged(null, null);
        }

        txtInKey.Text = objAppParam.GetApplicationParameterValueByParamName("In Func Key", CompId);
        txtOutKey.Text = objAppParam.GetApplicationParameterValueByParamName("Out Func Key", CompId);
        txtBreakInKey.Text = objAppParam.GetApplicationParameterValueByParamName("Break In Func Key", CompId);
        txtBreakOutKey.Text = objAppParam.GetApplicationParameterValueByParamName("Break Out Func Key", CompId);
        txtPartialInKey.Text = objAppParam.GetApplicationParameterValueByParamName("Partial Leave In  Func Key", CompId);
        txtPartialOutKey.Text = objAppParam.GetApplicationParameterValueByParamName("Partial Leave Out  Func Key", CompId);

        if (Convert.ToBoolean(objAppParam.GetApplicationParameterValueByParamName("Consider Next Day Log", CompId)))
        {
            ChkNextDayLog.Checked = true;
        }
        else
        {
            ChkNextDayLog.Checked = false;
        }

        txtAbsentColorCode.Text = objAppParam.GetApplicationParameterValueByParamName("Absnet_Color_Code", CompId);
        txtPresentColorCode.Text = objAppParam.GetApplicationParameterValueByParamName("Present_Color_Code", CompId);
        txtLateColorCode.Text = objAppParam.GetApplicationParameterValueByParamName("Late_Color_Code", CompId);
        txtEarlyColorCode.Text = objAppParam.GetApplicationParameterValueByParamName("Early_Color_Code", CompId);
        txtLeaveColorCode.Text = objAppParam.GetApplicationParameterValueByParamName("Leave_Color_Code", CompId);
        txtHolidayColorCode.Text = objAppParam.GetApplicationParameterValueByParamName("Holiday_Color_Code", CompId);





        if (Convert.ToBoolean(objAppParam.GetApplicationParameterValueByParamName("Is_Late_Penalty", CompId)))
        {
            rbtLateInEnable.Checked = true;
            rbtLateInDisable.Checked = false;
            rbtLateIn_OnCheckedChanged(null, null);


        }
        else
        {
            rbtLateInEnable.Checked = false;
            rbtLateInDisable.Checked = true;
            rbtLateIn_OnCheckedChanged(null, null);
        }

        if (objAppParam.GetApplicationParameterValueByParamName("Is_Late_Penalty_Method", CompId).Trim() == "Salary")
        {
            rbtnLateSalary.Checked = true;
            rbtnLateMinutes.Checked = false;
            rbtLateType_OnCheckedChanged(null,null);

            txtLateRelaxMin.Text = objAppParam.GetApplicationParameterValueByParamName("Late_Relaxation_Min", CompId);
            txtLateCount.Text = objAppParam.GetApplicationParameterValueByParamName("Late_Occurence", CompId);
            txtLateValue.Text = objAppParam.GetApplicationParameterValueByParamName("Is_Late_Penalty_Salary_Value", CompId);
            ddlLateType.SelectedValue = objAppParam.GetApplicationParameterValueByParamName("Is_Late_Penalty_Salary_Type", CompId);


        }
        else
        {
            rbtnLateSalary.Checked = false;
            rbtnLateMinutes.Checked = true;
            rbtLateType_OnCheckedChanged(null, null);
            ddlLateMinTime.SelectedValue = objAppParam.GetApplicationParameterValueByParamName("Late_Penalty_Min_Deduct", CompId);
            txtLateRelaxMin.Text = objAppParam.GetApplicationParameterValueByParamName("Late_Relaxation_Min", CompId);
            txtLateRelaxMinWithMTimes.Text = objAppParam.GetApplicationParameterValueByParamName("Late_Relaxation_Min", CompId);
            
            txtLateCount.Text = objAppParam.GetApplicationParameterValueByParamName("Late_Occurence", CompId);
            txtLateValue.Text = objAppParam.GetApplicationParameterValueByParamName("Is_Late_Penalty_Salary_Value", CompId);
            ddlLateType.SelectedValue = objAppParam.GetApplicationParameterValueByParamName("Is_Late_Penalty_Salary_Type", CompId);

            }
        if (Convert.ToBoolean(objAppParam.GetApplicationParameterValueByParamName("Is_Early_Penalty", CompId)))
        {
            rbtEarlyOutEnable.Checked = true;
            rbtEarlyOutDisable.Checked = false;
            rbtEarlyOut_OnCheckedChanged(null, null);
        }
        else
        {
            rbtEarlyOutEnable.Checked = false;
            rbtEarlyOutDisable.Checked = true;
            rbtEarlyOut_OnCheckedChanged(null, null);
        }
        if (objAppParam.GetApplicationParameterValueByParamName("Is_Early_Penalty_Method", CompId).Trim() == "Salary")
        {
            rbtnEarlySalary.Checked = true;
            rbtnEarlyMinutes.Checked = false;
            rbtEarlyType_OnCheckedChanged(null, null);
            txtEarlyRelaxMin.Text = objAppParam.GetApplicationParameterValueByParamName("Early_Relaxation_Min", CompId);
            txtEarlyCount.Text = objAppParam.GetApplicationParameterValueByParamName("Early_Occurence", CompId);
            txtEarlyValue.Text = objAppParam.GetApplicationParameterValueByParamName("Is_Early_Penalty_Salary_Value", CompId);
            ddlEarlyType.SelectedValue = objAppParam.GetApplicationParameterValueByParamName("Is_Early_Penalty_Salary_Type", CompId);

        }
        else
        {
            rbtnEarlySalary.Checked = false;
            rbtnEarlyMinutes.Checked = true;
            rbtEarlyType_OnCheckedChanged(null, null);
            ddlEarlyMinTime.SelectedValue = objAppParam.GetApplicationParameterValueByParamName("Early_Penalty_Min_Deduct", CompId);

            txtEarlyRelaxMin.Text = objAppParam.GetApplicationParameterValueByParamName("Early_Relaxation_Min", CompId);

            txtEarlyRelaxMinWithMinTimes.Text = objAppParam.GetApplicationParameterValueByParamName("Early_Relaxation_Min", CompId);
            
            txtEarlyCount.Text = objAppParam.GetApplicationParameterValueByParamName("Early_Occurence", CompId);
            txtEarlyValue.Text = objAppParam.GetApplicationParameterValueByParamName("Is_Early_Penalty_Salary_Value", CompId);
            ddlEarlyType.SelectedValue = objAppParam.GetApplicationParameterValueByParamName("Is_Early_Penalty_Salary_Type", CompId);

        }


        if (objAppParam.GetApplicationParameterValueByParamName("Partial_Penalty_Method", CompId).Trim() == "Salary")
        {
            rbtnPartialSalary.Checked = true;
            rbtnPartialMinutes.Checked = false;
            rbtPartialType_OnCheckedChanged(null, null);

            txtPartialValue.Text = objAppParam.GetApplicationParameterValueByParamName("Partial_Penalty_Salary_Value", CompId);
            ddlPartialType.SelectedValue = objAppParam.GetApplicationParameterValueByParamName("Partial_Penalty_Salary_Type", CompId);
          
        }
        else
        {
            rbtnPartialSalary.Checked = false;
            rbtnPartialMinutes.Checked = true;
            rbtPartialType_OnCheckedChanged(null, null);

            ddlPartialMinTime.SelectedValue = objAppParam.GetApplicationParameterValueByParamName("Partial_Penalty_Min_Deduct", CompId);

        }
        txtAbsentDeduc.Text = objAppParam.GetApplicationParameterValueByParamName("Absent_Value", CompId);
        ddlAbsentType.SelectedValue = objAppParam.GetApplicationParameterValueByParamName("Absent_Type", CompId);


        if (objAppParam.GetApplicationParameterValueByParamName("Default_Shift", CompId) != "0")
        {
            try
            {
                ddlDefaultShift.SelectedValue = objAppParam.GetApplicationParameterValueByParamName("Default_Shift", CompId);
            }
            catch
            {

            }
        }
        else
        {
            ddlDefaultShift.SelectedIndex = 0;
        }



        chkNoClockIn.Checked = Convert.ToBoolean(objAppParam.GetApplicationParameterValueByParamName("No_Clock_In_CountAsAbsent", CompId));
        chkNoClockOut.Checked = Convert.ToBoolean(objAppParam.GetApplicationParameterValueByParamName("No_Clock_Out_CountAsAbsent", CompId));

        txtEmpPF.Text = objAppParam.GetApplicationParameterValueByParamName("Employee_PF", CompId);
        txtEmployerPf.Text = objAppParam.GetApplicationParameterValueByParamName("Employer_PF", CompId);
        txtEmpESIC.Text = objAppParam.GetApplicationParameterValueByParamName("Employee_ESIC", CompId);
        txtEmployerESIC.Text = objAppParam.GetApplicationParameterValueByParamName("Employer_ESIC", CompId); 


    }


    protected void rbtPartial_OnCheckedChanged(object sender, EventArgs e)
    {
        if (rbtnPartialEnable.Checked)
        {
            rbtnCarryYes.Enabled = true;
            rbtnCarryNo.Enabled = true;
            txtTotalMinutes.Enabled = true;
            txtMinuteday.Enabled = true;
            txtViolation.Enabled = true;

        }
        else
        {
            rbtnCarryYes.Enabled = false;
            rbtnCarryNo.Enabled = false;
            txtTotalMinutes.Enabled = false;
            txtMinuteday.Enabled = false;
            txtViolation.Enabled = false;
        }

    }
    protected void btnSavePartial_Click(object sender, EventArgs e)
    {
        if (rbtnPartialEnable.Checked)
        {

            objAppParam.UpdateApplicationParameterMaster(hdnCompanyId.Value, "Partial_Leave_Enable", true.ToString(), "", "", true.ToString(), Session["UserId"].ToString(), DateTime.Now.ToString());
        }
        else
        {
            objAppParam.UpdateApplicationParameterMaster(hdnCompanyId.Value, "Partial_Leave_Enable", false.ToString(), "", "", true.ToString(), Session["UserId"].ToString(), DateTime.Now.ToString());


        }
        objAppParam.UpdateApplicationParameterMaster(hdnCompanyId.Value, "Total Partial Leave Minutes", txtTotalMinutes.Text, "", "", true.ToString(), Session["UserId"].ToString(), DateTime.Now.ToString());
        objAppParam.UpdateApplicationParameterMaster(hdnCompanyId.Value, "Partial Leave Minute Use In A Day", txtMinuteday.Text, "", "", true.ToString(), Session["UserId"].ToString(), DateTime.Now.ToString());
        objAppParam.UpdateApplicationParameterMaster(hdnCompanyId.Value, "Partial_Violation_Min", txtViolation.Text, "", "", true.ToString(), Session["UserId"].ToString(), DateTime.Now.ToString());

        if (rbtnCarryYes.Checked)
        {


            objAppParam.UpdateApplicationParameterMaster(hdnCompanyId.Value, "Carry Forward Partial Leave Minutes", true.ToString(), "", "", true.ToString(), Session["UserId"].ToString(), DateTime.Now.ToString());
        }
        else
        {
            objAppParam.UpdateApplicationParameterMaster(hdnCompanyId.Value, "Carry Forward Partial Leave Minutes", false.ToString(), "", "", true.ToString(), Session["UserId"].ToString(), DateTime.Now.ToString());
        }

        if (rbtnOtEnable.Checked)
        {
            objAppParam.UpdateApplicationParameterMaster(hdnCompanyId.Value, "IsOverTime", true.ToString(), "", "", true.ToString(), Session["UserId"].ToString(), DateTime.Now.ToString());

        }
        else
        {
            objAppParam.UpdateApplicationParameterMaster(hdnCompanyId.Value, "IsOverTime", false.ToString(), "", "", true.ToString(), Session["UserId"].ToString(), DateTime.Now.ToString());


        }
        objAppParam.UpdateApplicationParameterMaster(hdnCompanyId.Value, "Max Over Time Min", txtMaxOTMint.Text, "", "", true.ToString(), Session["UserId"].ToString(), DateTime.Now.ToString());
        objAppParam.UpdateApplicationParameterMaster(hdnCompanyId.Value, "Min OVer Time Min", txtMinOTMint.Text, "", "", true.ToString(), Session["UserId"].ToString(), DateTime.Now.ToString());
        objAppParam.UpdateApplicationParameterMaster(hdnCompanyId.Value, "Over Time Calculation Method", ddlCalculationMethod.SelectedValue, "", "", true.ToString(), Session["UserId"].ToString(), DateTime.Now.ToString());

        DisplayMessage("Record Updated");

    }

    protected void btnCancelPartial_Click(object sender, EventArgs e)
    {

        Response.Redirect("../MasterSetup/CompanyMaster.aspx");

    }
    protected void btnSaveSMSEmail_Click(object sender, EventArgs e)
    {
        if (txtEmail.Text != "")
        {

            if (!IsValidEmail(txtEmail.Text))
            {
                DisplayMessage("Email is invalid");
                txtEmail.Text = "";
                txtEmail.Focus();
                return;
            }
        }

        if (rbtnSMSEnable.Checked)
        {

            objAppParam.UpdateApplicationParameterMaster(hdnCompanyId.Value, "SMS_Enable", true.ToString(), "", "", true.ToString(), Session["UserId"].ToString(), DateTime.Now.ToString());
        }
        else
        {
            objAppParam.UpdateApplicationParameterMaster(hdnCompanyId.Value, "SMS_Enable", false.ToString(), "", "", true.ToString(), Session["UserId"].ToString(), DateTime.Now.ToString());


        } if (rbtnEmailEnable.Checked)
        {

            objAppParam.UpdateApplicationParameterMaster(hdnCompanyId.Value, "Email_Enable", true.ToString(), "", "", true.ToString(), Session["UserId"].ToString(), DateTime.Now.ToString());
        }
        else
        {
            objAppParam.UpdateApplicationParameterMaster(hdnCompanyId.Value, "Email_Enable", false.ToString(), "", "", true.ToString(), Session["UserId"].ToString(), DateTime.Now.ToString());

        }

        objAppParam.UpdateApplicationParameterMaster(hdnCompanyId.Value, "SMS_API", txtSMSAPI.Text, "", "", true.ToString(), Session["UserId"].ToString(), DateTime.Now.ToString());
        objAppParam.UpdateApplicationParameterMaster(hdnCompanyId.Value, "SMS_User_Password", txtSmsPassword.Text, "", "", true.ToString(), Session["UserId"].ToString(), DateTime.Now.ToString());
        objAppParam.UpdateApplicationParameterMaster(hdnCompanyId.Value, "SMS_User_Id", txtUserId.Text, "", "", true.ToString(), Session["UserId"].ToString(), DateTime.Now.ToString());
        objAppParam.UpdateApplicationParameterMaster(hdnCompanyId.Value, "Sender_Id", txtSenderId.Text, "", "", true.ToString(), Session["UserId"].ToString(), DateTime.Now.ToString());
        objAppParam.UpdateApplicationParameterMaster(hdnCompanyId.Value, "Master_Email", txtEmail.Text, "", "", true.ToString(), Session["UserId"].ToString(), DateTime.Now.ToString());
        objAppParam.UpdateApplicationParameterMaster(hdnCompanyId.Value, "Master_Email_Password", txtPasswordEmail.Text, "", "", true.ToString(), Session["UserId"].ToString(), DateTime.Now.ToString());
        objAppParam.UpdateApplicationParameterMaster(hdnCompanyId.Value, "Master_Email_SMTP", txtSMTP.Text, "", "", true.ToString(), Session["UserId"].ToString(), DateTime.Now.ToString());
        objAppParam.UpdateApplicationParameterMaster(hdnCompanyId.Value, "Master_Email_Port", txtPort.Text, "", "", true.ToString(), Session["UserId"].ToString(), DateTime.Now.ToString());
        objAppParam.UpdateApplicationParameterMaster(hdnCompanyId.Value, "Master_Email_EnableSSL", chkEnableSSL.Checked.ToString(), "", "", true.ToString(), Session["UserId"].ToString(), DateTime.Now.ToString());


        DisplayMessage("Record Updated");
    }

    public void DisplayMessage(string str)
    {
        ScriptManager.RegisterStartupScript(this, GetType(), "", "alert('" + str + "');", true);
    }
    protected void txtWorkPercentTo1_TextChanged(object sender, EventArgs e)
    {
        if (txtWorkPercentTo1.Text == "")
        {

            txtWorkPercentTo1.Text = "0";
        }

        if (Convert.ToDouble(txtWorkPercentTo1.Text) >= Convert.ToDouble(txtWorkPercentTo2.Text))
        {
            txtWorkPercentFrom2.Text = (Convert.ToDouble(txtWorkPercentTo1.Text) + 1).ToString();

            txtWorkPercentTo2.Text = (Convert.ToDouble(txtWorkPercentFrom2.Text) + 1).ToString();

            txtWorkPercentTo2_TextChanged(null, null);


        }
        else
        {

            txtWorkPercentFrom2.Text = (Convert.ToDouble(txtWorkPercentTo1.Text) + 1).ToString();
        }
    }
    protected void txtWorkPercentTo2_TextChanged(object sender, EventArgs e)
    {

        if (Convert.ToDouble(txtWorkPercentTo2.Text) < Convert.ToDouble(txtWorkPercentFrom2.Text))
        {

            DisplayMessage("Value Should be Greater than " + txtWorkPercentFrom2.Text + " ");


        }


        txtWorkPercentFrom3.Text = (Convert.ToDouble(txtWorkPercentTo2.Text) + 1).ToString();


    }



    protected void ddlPaySal_OnSelectedIndexChanged(object sender, EventArgs e)
    {

        if (ddlPaySal.SelectedValue == "Work Hour")
        {
            pnlWorkPercent.Visible = false;

        }
        else
        {
            pnlWorkPercent.Visible = true;
        }


    }
    protected void btnSaveWork_Click(object sender, EventArgs e)
    {
        objAppParam.UpdateApplicationParameterMaster(hdnCompanyId.Value, "Effective Work Calculation Method", ddlWorkCal.SelectedValue, "", "", true.ToString(), Session["UserId"].ToString(), DateTime.Now.ToString());
        objAppParam.UpdateApplicationParameterMaster(hdnCompanyId.Value, "Pay Salary Acc To Work Hour or Ref Hour", ddlPaySal.SelectedValue, "", "", true.ToString(), Session["UserId"].ToString(), DateTime.Now.ToString());
        objAppParam.UpdateApplicationParameterMaster(hdnCompanyId.Value, "Without Shift Preference", ddlShiftPref.SelectedValue, "", "", true.ToString(), Session["UserId"].ToString(), DateTime.Now.ToString());
        objAppParam.UpdateApplicationParameterMaster(hdnCompanyId.Value, "Salary Calculate According To", ddlSalCal.SelectedValue, "", "", true.ToString(), Session["UserId"].ToString(), DateTime.Now.ToString());
        objAppParam.UpdateApplicationParameterMaster(hdnCompanyId.Value, "Days In Month", txtFixedDays.Text, "", "", true.ToString(), Session["UserId"].ToString(), DateTime.Now.ToString());


        objAppParam.UpdateApplicationParameterMaster(hdnCompanyId.Value, "WorkPercentTo1", txtWorkPercentTo1.Text, "", "", true.ToString(), Session["UserId"].ToString(), DateTime.Now.ToString());
        objAppParam.UpdateApplicationParameterMaster(hdnCompanyId.Value, "WorkPercentTo2", txtWorkPercentTo2.Text, "", "", true.ToString(), Session["UserId"].ToString(), DateTime.Now.ToString());
        objAppParam.UpdateApplicationParameterMaster(hdnCompanyId.Value, "WorkPercentTo3", txtWorkPercentTo3.Text, "", "", true.ToString(), Session["UserId"].ToString(), DateTime.Now.ToString());
        objAppParam.UpdateApplicationParameterMaster(hdnCompanyId.Value, "WorkPercentFrom1", txtWorkPercentFrom1.Text, "", "", true.ToString(), Session["UserId"].ToString(), DateTime.Now.ToString());
        objAppParam.UpdateApplicationParameterMaster(hdnCompanyId.Value, "WorkPercentFrom2", txtWorkPercentFrom2.Text, "", "", true.ToString(), Session["UserId"].ToString(), DateTime.Now.ToString());
        objAppParam.UpdateApplicationParameterMaster(hdnCompanyId.Value, "WorkPercentFrom3", txtWorkPercentFrom3.Text, "", "", true.ToString(), Session["UserId"].ToString(), DateTime.Now.ToString());
        objAppParam.UpdateApplicationParameterMaster(hdnCompanyId.Value, "Value1", txtValue1.Text, "", "", true.ToString(), Session["UserId"].ToString(), DateTime.Now.ToString());
        objAppParam.UpdateApplicationParameterMaster(hdnCompanyId.Value, "Value2", txtValue2.Text, "", "", true.ToString(), Session["UserId"].ToString(), DateTime.Now.ToString());
        objAppParam.UpdateApplicationParameterMaster(hdnCompanyId.Value, "Value3", txtValue3.Text, "", "", true.ToString(), Session["UserId"].ToString(), DateTime.Now.ToString());



        objAppParam.UpdateApplicationParameterMaster(hdnCompanyId.Value, "Employee_PF", txtEmpPF.Text, "", "", true.ToString(), Session["UserId"].ToString(), DateTime.Now.ToString());

        objAppParam.UpdateApplicationParameterMaster(hdnCompanyId.Value, "Employer_PF", txtEmployerPf.Text, "", "", true.ToString(), Session["UserId"].ToString(), DateTime.Now.ToString());

        objAppParam.UpdateApplicationParameterMaster(hdnCompanyId.Value, "Employee_ESIC", txtEmpESIC.Text, "", "", true.ToString(), Session["UserId"].ToString(), DateTime.Now.ToString());

        objAppParam.UpdateApplicationParameterMaster(hdnCompanyId.Value, "Employer_ESIC", txtEmployerESIC.Text, "", "", true.ToString(), Session["UserId"].ToString(), DateTime.Now.ToString());



        DisplayMessage("Record Updated");
    }
    protected void btnCancelWork_Click(object sender, EventArgs e)
    {
        Response.Redirect("../MasterSetup/CompanyMaster.aspx");

    }
    protected void btnSavePanelty_Click(object sender, EventArgs e)
    {
          if(rbtLateInEnable.Checked)
          {
              if (rbtnLateSalary.Checked)
              {
                  if(txtLateRelaxMin.Text=="")
                  {
                      DisplayMessage("Enter late Relaxation minute");
                      txtLateRelaxMin.Focus();
                      return;

                  }
                  if (txtLateCount.Text == "")
                  {
                      DisplayMessage("Enter late count");
                      txtLateCount.Focus();
                      return;


                  }
                  if (txtLateValue.Text == "")
                  {
                      DisplayMessage("Enter late value");
                      txtLateValue.Focus();
                      return;


                  }
              }
              else
              {
                  if (txtLateRelaxMinWithMTimes.Text == "")
                  {
                      DisplayMessage("Enter late Relaxation minute");
                      txtLateRelaxMinWithMTimes.Focus();
                      return;

                  }
              }

          }


          if (rbtEarlyOutEnable.Checked)
          {
              if (rbtnEarlySalary.Checked)
              {
                  if (txtEarlyRelaxMin.Text == "")
                  {
                      DisplayMessage("Enter Early Relaxation minute");
                      txtEarlyRelaxMin.Focus();
                      return;

                  }
                  if (txtEarlyCount.Text == "")
                  {
                      DisplayMessage("Enter Early count");
                      txtEarlyCount.Focus();
                      return;


                  }
                  if (txtEarlyValue.Text == "")
                  {
                      DisplayMessage("Enter Early value");
                      txtEarlyValue.Focus();
                      return;


                  }
              }
              else
              {
                  if (txtEarlyRelaxMinWithMinTimes.Text == "")
                  {
                      DisplayMessage("Enter Early Relaxation minute");
                      txtEarlyRelaxMinWithMinTimes.Focus();
                      return;

                  }
              }

          }
        

        if(txtAbsentDeduc.Text=="")
        {
            DisplayMessage("Enter absent deduction value");
            txtAbsentDeduc.Focus();
            return;

        }
         if (rbtnEarlySalary.Checked)
            {
               

                if (txtPartialValue.Text == "")
                {
                    DisplayMessage("Enter partial value");
                    txtPartialValue.Focus();
                    return;

                }

                if (txtViolation.Text == "")
                {
                    DisplayMessage("Enter partial violation value");
                    txtViolation.Focus();
                    return;

                }
            }
            else
            {
                if (txtViolation.Text == "")
                {
                    DisplayMessage("Enter partial violation value");
                    txtViolation.Focus();
                    return;

                }
            }


        if(rbtLateInDisable.Checked)
        {
            objAppParam.UpdateApplicationParameterMaster(hdnCompanyId.Value, "Is_Late_Penalty", false.ToString(), "", "", true.ToString(), Session["UserId"].ToString(), DateTime.Now.ToString());
     
        }
        else
        {
            objAppParam.UpdateApplicationParameterMaster(hdnCompanyId.Value, "Is_Late_Penalty", true.ToString(), "", "", true.ToString(), Session["UserId"].ToString(), DateTime.Now.ToString());

            if (rbtnLateSalary.Checked)
            {
                objAppParam.UpdateApplicationParameterMaster(hdnCompanyId.Value, "Is_Late_Penalty_Method", "Salary", "", "", true.ToString(), Session["UserId"].ToString(), DateTime.Now.ToString());

                objAppParam.UpdateApplicationParameterMaster(hdnCompanyId.Value, "Late_Relaxation_Min",txtLateRelaxMin.Text, "", "", true.ToString(), Session["UserId"].ToString(), DateTime.Now.ToString());
                objAppParam.UpdateApplicationParameterMaster(hdnCompanyId.Value, "Late_Occurence", txtLateCount.Text, "", "", true.ToString(), Session["UserId"].ToString(), DateTime.Now.ToString());
                objAppParam.UpdateApplicationParameterMaster(hdnCompanyId.Value, "Is_Late_Penalty_Salary_Type",ddlLateType.SelectedValue, "", "", true.ToString(), Session["UserId"].ToString(), DateTime.Now.ToString());
                objAppParam.UpdateApplicationParameterMaster(hdnCompanyId.Value, "Is_Late_Penalty_Salary_Value", txtLateValue.Text, "", "", true.ToString(), Session["UserId"].ToString(), DateTime.Now.ToString());


            }
            else
            {
                objAppParam.UpdateApplicationParameterMaster(hdnCompanyId.Value, "Is_Late_Penalty_Method", "Min", "", "", true.ToString(), Session["UserId"].ToString(), DateTime.Now.ToString());

                objAppParam.UpdateApplicationParameterMaster(hdnCompanyId.Value, "Late_Penalty_Min_Deduct",ddlLateMinTime.SelectedValue, "", "", true.ToString(), Session["UserId"].ToString(), DateTime.Now.ToString());
                objAppParam.UpdateApplicationParameterMaster(hdnCompanyId.Value, "Late_Relaxation_Min", txtLateRelaxMinWithMTimes.Text, "", "", true.ToString(), Session["UserId"].ToString(), DateTime.Now.ToString());
               

            }

        }

        if (rbtEarlyOutDisable.Checked)
        {
            objAppParam.UpdateApplicationParameterMaster(hdnCompanyId.Value, "Is_Early_Penalty", false.ToString(), "", "", true.ToString(), Session["UserId"].ToString(), DateTime.Now.ToString());

        }
        else
        {
            objAppParam.UpdateApplicationParameterMaster(hdnCompanyId.Value, "Is_Early_Penalty", true.ToString(), "", "", true.ToString(), Session["UserId"].ToString(), DateTime.Now.ToString());

            if (rbtnEarlySalary.Checked)
            {
                objAppParam.UpdateApplicationParameterMaster(hdnCompanyId.Value, "Is_Early_Penalty_Method", "Salary", "", "", true.ToString(), Session["UserId"].ToString(), DateTime.Now.ToString());

                objAppParam.UpdateApplicationParameterMaster(hdnCompanyId.Value, "Early_Relaxation_Min", txtEarlyRelaxMin.Text, "", "", true.ToString(), Session["UserId"].ToString(), DateTime.Now.ToString());
                objAppParam.UpdateApplicationParameterMaster(hdnCompanyId.Value, "Early_Occurence", txtEarlyCount.Text, "", "", true.ToString(), Session["UserId"].ToString(), DateTime.Now.ToString());
                objAppParam.UpdateApplicationParameterMaster(hdnCompanyId.Value, "Is_Early_Penalty_Salary_Type", ddlEarlyType.SelectedValue, "", "", true.ToString(), Session["UserId"].ToString(), DateTime.Now.ToString());
                objAppParam.UpdateApplicationParameterMaster(hdnCompanyId.Value, "Is_Early_Penalty_Salary_Value", txtEarlyValue.Text, "", "", true.ToString(), Session["UserId"].ToString(), DateTime.Now.ToString());


            }
            else
            {
                objAppParam.UpdateApplicationParameterMaster(hdnCompanyId.Value, "Is_Early_Penalty_Method", "Min", "", "", true.ToString(), Session["UserId"].ToString(), DateTime.Now.ToString());

                objAppParam.UpdateApplicationParameterMaster(hdnCompanyId.Value, "Early_Penalty_Min_Deduct", ddlEarlyMinTime.SelectedValue, "", "", true.ToString(), Session["UserId"].ToString(), DateTime.Now.ToString());

                objAppParam.UpdateApplicationParameterMaster(hdnCompanyId.Value, "Early_Relaxation_Min", txtEarlyRelaxMinWithMinTimes.Text, "", "", true.ToString(), Session["UserId"].ToString(), DateTime.Now.ToString());
               
            }

        }

        objAppParam.UpdateApplicationParameterMaster(hdnCompanyId.Value, "Absent_Type",ddlAbsentType.SelectedValue, "", "", true.ToString(), Session["UserId"].ToString(), DateTime.Now.ToString());
        objAppParam.UpdateApplicationParameterMaster(hdnCompanyId.Value, "Absent_Value",txtAbsentDeduc.Text, "", "", true.ToString(), Session["UserId"].ToString(), DateTime.Now.ToString());





        if (rbtnPartialSalary.Checked)
        {
            objAppParam.UpdateApplicationParameterMaster(hdnCompanyId.Value, "Partial_Penalty_Method", "Salary", "", "", true.ToString(), Session["UserId"].ToString(), DateTime.Now.ToString());

            objAppParam.UpdateApplicationParameterMaster(hdnCompanyId.Value, "Partial_Penalty_Salary_Type", ddlPartialType.SelectedValue, "", "", true.ToString(), Session["UserId"].ToString(), DateTime.Now.ToString());
             objAppParam.UpdateApplicationParameterMaster(hdnCompanyId.Value, "Partial_Penalty_Salary_Value", txtPartialValue.Text, "", "", true.ToString(), Session["UserId"].ToString(), DateTime.Now.ToString());


        }
        else
        {
            objAppParam.UpdateApplicationParameterMaster(hdnCompanyId.Value, "Partial_Penalty_Method", "Min", "", "", true.ToString(), Session["UserId"].ToString(), DateTime.Now.ToString());

            objAppParam.UpdateApplicationParameterMaster(hdnCompanyId.Value, "Partial_Penalty_Min_Deduct", ddlPartialMinTime.SelectedValue, "", "", true.ToString(), Session["UserId"].ToString(), DateTime.Now.ToString());
            objAppParam.UpdateApplicationParameterMaster(hdnCompanyId.Value, "Partial_Penalty_Salary_Value", txtPartialValue.Text, "", "", true.ToString(), Session["UserId"].ToString(), DateTime.Now.ToString());


        }


        objAppParam.UpdateApplicationParameterMaster(hdnCompanyId.Value, "Partial_Violation_Min", txtViolation.Text, "", "", true.ToString(), Session["UserId"].ToString(), DateTime.Now.ToString());


        objAppParam.UpdateApplicationParameterMaster(hdnCompanyId.Value, "No_Clock_In_CountAsAbsent", chkNoClockIn.Checked.ToString(), "", "", true.ToString(), Session["UserId"].ToString(), DateTime.Now.ToString());


        objAppParam.UpdateApplicationParameterMaster(hdnCompanyId.Value, "No_Clock_Out_CountAsAbsent", chkNoClockOut.Checked.ToString(), "", "", true.ToString(), Session["UserId"].ToString(), DateTime.Now.ToString());

        DisplayMessage("Record Updated");


    }
    protected void btnCancelPanelty_Click(object sender, EventArgs e)
    {
        Response.Redirect("../MasterSetup/CompanyMaster.aspx");

    }
    protected void btnSaveColorCode_Click(object sender, EventArgs e)
    {
        objAppParam.UpdateApplicationParameterMaster(hdnCompanyId.Value, "Absnet_Color_Code", txtAbsentColorCode.Text, "", "", true.ToString(), Session["UserId"].ToString(), DateTime.Now.ToString());
        objAppParam.UpdateApplicationParameterMaster(hdnCompanyId.Value, "Leave_Color_Code", txtLeaveColorCode.Text, "", "", true.ToString(), Session["UserId"].ToString(), DateTime.Now.ToString());
        objAppParam.UpdateApplicationParameterMaster(hdnCompanyId.Value, "Present_Color_Code", txtPresentColorCode.Text, "", "", true.ToString(), Session["UserId"].ToString(), DateTime.Now.ToString());
        objAppParam.UpdateApplicationParameterMaster(hdnCompanyId.Value, "Holiday_Color_Code", txtHolidayColorCode.Text, "", "", true.ToString(), Session["UserId"].ToString(), DateTime.Now.ToString());
        objAppParam.UpdateApplicationParameterMaster(hdnCompanyId.Value, "Late_Color_Code", txtLateColorCode.Text, "", "", true.ToString(), Session["UserId"].ToString(), DateTime.Now.ToString());
        objAppParam.UpdateApplicationParameterMaster(hdnCompanyId.Value, "Early_Color_Code", txtEarlyColorCode.Text, "", "", true.ToString(), Session["UserId"].ToString(), DateTime.Now.ToString());

        DisplayMessage("Record Updated");

   }

    protected void btnCancelColorCode_Click(object sender, EventArgs e)
    {
        Response.Redirect("../MasterSetup/CompanyMaster.aspx");

       }
    protected void btnSaveKeyPreference_Click(object sender, EventArgs e)
    {
        objAppParam.UpdateApplicationParameterMaster(hdnCompanyId.Value, "In Func Key", txtInKey.Text, "", "", true.ToString(), Session["UserId"].ToString(), DateTime.Now.ToString());
        objAppParam.UpdateApplicationParameterMaster(hdnCompanyId.Value, "Out Func Key", txtOutKey.Text, "", "", true.ToString(), Session["UserId"].ToString(), DateTime.Now.ToString());
        objAppParam.UpdateApplicationParameterMaster(hdnCompanyId.Value, "Break In Func Key", txtBreakInKey.Text, "", "", true.ToString(), Session["UserId"].ToString(), DateTime.Now.ToString());
        objAppParam.UpdateApplicationParameterMaster(hdnCompanyId.Value, "Break Out Func Key", txtBreakOutKey.Text, "", "", true.ToString(), Session["UserId"].ToString(), DateTime.Now.ToString());
        objAppParam.UpdateApplicationParameterMaster(hdnCompanyId.Value, "Partial Leave In  Func Key", txtPartialInKey.Text, "", "", true.ToString(), Session["UserId"].ToString(), DateTime.Now.ToString());
        objAppParam.UpdateApplicationParameterMaster(hdnCompanyId.Value, "Partial Leave Out  Func Key", txtPartialOutKey.Text, "", "", true.ToString(), Session["UserId"].ToString(), DateTime.Now.ToString());



        if (rbtnKeyEnable.Checked)
        {
            objAppParam.UpdateApplicationParameterMaster(hdnCompanyId.Value, "With Key Preference", "Yes", "", "", true.ToString(), Session["UserId"].ToString(), DateTime.Now.ToString());

        }
        else
        {
            objAppParam.UpdateApplicationParameterMaster(hdnCompanyId.Value, "With Key Preference", "No", "", "", true.ToString(), Session["UserId"].ToString(), DateTime.Now.ToString());

        }
        if (ChkNextDayLog.Checked)
        {
            objAppParam.UpdateApplicationParameterMaster(hdnCompanyId.Value, "Consider Next Day Log", true.ToString(), "", "", true.ToString(), Session["UserId"].ToString(), DateTime.Now.ToString());

        }
        else
        {
            objAppParam.UpdateApplicationParameterMaster(hdnCompanyId.Value, "Consider Next Day Log", false.ToString(), "", "", true.ToString(), Session["UserId"].ToString(), DateTime.Now.ToString());

        }

        DisplayMessage("Record Updated");
    }

    protected void btnCancelKeyPreference_Click(object sender, EventArgs e)
    {
        Response.Redirect("../MasterSetup/CompanyMaster.aspx");

    }


    protected void btnSaveTime_Click(object sender, EventArgs e)
    {
        objAppParam.UpdateApplicationParameterMaster(hdnCompanyId.Value, "Shortest Time Table", txtShortestTime.Text, "", "", true.ToString(), Session["UserId"].ToString(), DateTime.Now.ToString());
        objAppParam.UpdateApplicationParameterMaster(hdnCompanyId.Value, "Min Difference Between TimeTable in Shift", txtMinDiffTime.Text, "", "", true.ToString(), Session["UserId"].ToString(), DateTime.Now.ToString());
        objAppParam.UpdateApplicationParameterMaster(hdnCompanyId.Value, "Service_Run_Time", txtServiceRunTime.Text, "", "", true.ToString(), Session["UserId"].ToString(), DateTime.Now.ToString());

        if (rbtnBrandNo.Checked)
        {
            objAppParam.UpdateApplicationParameterMaster(hdnCompanyId.Value, "Display TimeTable In All Brand", rbtnBrandNo.Checked.ToString(), "", "", true.ToString(), Session["UserId"].ToString(), DateTime.Now.ToString());

        }
        else
        {
            objAppParam.UpdateApplicationParameterMaster(hdnCompanyId.Value, "Display TimeTable In All Brand", rbtnBrandYes.Checked.ToString(), "", "", true.ToString(), Session["UserId"].ToString(), DateTime.Now.ToString());

        }

        int flag = 0;
        for (int i = 0; i < ChkWeekOffList.Items.Count; i++)
        {
            if (ChkWeekOffList.Items[i].Selected)
            {
                flag = 1;

                objAppParam.UpdateApplicationParameterMaster(hdnCompanyId.Value, "Week Off Days", ChkWeekOffList.Items[i].Text, "", "", true.ToString(), Session["UserId"].ToString(), DateTime.Now.ToString());
            }
        }

        if(flag==0)
        {
            objAppParam.UpdateApplicationParameterMaster(hdnCompanyId.Value, "Week Off Days","No", "", "", true.ToString(), Session["UserId"].ToString(), DateTime.Now.ToString());

        }
        objAppParam.UpdateApplicationParameterMaster(hdnCompanyId.Value, "Exclude Day As Absent or IsOff", ddlExculeDay.SelectedValue, "", "", true.ToString(), Session["UserId"].ToString(), DateTime.Now.ToString());
        objAppParam.UpdateApplicationParameterMaster(hdnCompanyId.Value, "FinancialYearStartMonth", ddlFinancialYear.SelectedValue, "", "", true.ToString(), Session["UserId"].ToString(), DateTime.Now.ToString());
        objAppParam.UpdateApplicationParameterMaster(hdnCompanyId.Value, "Employee Synchronization", ddlEmpSync.SelectedValue, "", "", true.ToString(), Session["UserId"].ToString(), DateTime.Now.ToString());


        if (ddlDefaultShift.SelectedIndex != 0)
        {
            objAppParam.UpdateApplicationParameterMaster(hdnCompanyId.Value, "Default_Shift", ddlDefaultShift.SelectedValue, "", "", true.ToString(), Session["UserId"].ToString(), DateTime.Now.ToString());


        }
        else
        {
            objAppParam.UpdateApplicationParameterMaster(hdnCompanyId.Value, "Default_Shift","0", "", "", true.ToString(), Session["UserId"].ToString(), DateTime.Now.ToString());


        }

        DisplayMessage("Record Updated");


    }

    protected void btnCancelTime_Click(object sender, EventArgs e)
    {

        Response.Redirect("../MasterSetup/CompanyMaster.aspx");
    }




    protected void btnCancelSMSEmail_Click(object sender, EventArgs e)
    {
        Response.Redirect("../MasterSetup/CompanyMaster.aspx");

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
    protected void btnSaveOverTime_Click(object sender, EventArgs e)
    {
        if (rbtnOtEnable.Checked)
        {
            objAppParam.UpdateApplicationParameterMaster(hdnCompanyId.Value, "IsOverTime", true.ToString(), "", "", true.ToString(), Session["UserId"].ToString(), DateTime.Now.ToString());

        }
        else
        {
            objAppParam.UpdateApplicationParameterMaster(hdnCompanyId.Value, "IsOverTime", false.ToString(), "", "", true.ToString(), Session["UserId"].ToString(), DateTime.Now.ToString());


        }
        objAppParam.UpdateApplicationParameterMaster(hdnCompanyId.Value, "Max Over Time Min", txtMaxOTMint.Text, "", "", true.ToString(), Session["UserId"].ToString(), DateTime.Now.ToString());
        objAppParam.UpdateApplicationParameterMaster(hdnCompanyId.Value, "Min OVer Time Min", txtMinOTMint.Text, "", "", true.ToString(), Session["UserId"].ToString(), DateTime.Now.ToString());
        objAppParam.UpdateApplicationParameterMaster(hdnCompanyId.Value, "Over Time Calculation Method", ddlCalculationMethod.SelectedValue, "", "", true.ToString(), Session["UserId"].ToString(), DateTime.Now.ToString());

        DisplayMessage("Record Updated");

    }
    protected void rbtKeyPref_OnCheckedChanged(object sender, EventArgs e)
    {
        if (rbtnKeyEnable.Checked)
        {
            txtInKey.Enabled = true;
            txtOutKey.Enabled = true;
            txtPartialInKey.Enabled = true;
            txtPartialOutKey.Enabled = true;
            txtBreakInKey.Enabled = true;
            txtBreakOutKey.Enabled = true;
            ChkNextDayLog.Enabled = true;
        }
        else
        {
            txtInKey.Enabled = false;
            txtOutKey.Enabled = false;
            txtPartialInKey.Enabled = false;
            txtPartialOutKey.Enabled = false;
            txtBreakInKey.Enabled = false;
            txtBreakOutKey.Enabled = false;
            ChkNextDayLog.Enabled = false;

        }

    }
    protected void rbtSMS_OnCheckedChanged(object sender, EventArgs e)
    {
        if (rbtnSMSEnable.Checked)
        {

            txtSMSAPI.Enabled = true;
            txtSmsPassword.Enabled = true;
            txtSenderId.Enabled = true;
            txtUserId.Enabled = true;


        }
        else
        {
            txtSMSAPI.Enabled = false;
            txtSmsPassword.Enabled = false;
            txtSenderId.Enabled = false;
            txtUserId.Enabled = false;


        }
    }
    protected void rbtEmail_OnCheckedChanged(object sender, EventArgs e)
    {
        if (rbtnEmailEnable.Checked)
        {
            txtEmail.Enabled = true;
            txtPasswordEmail.Enabled = true;
            txtSMTP.Enabled = true;
            txtPort.Enabled = true;
            chkEnableSSL.Enabled = true;
        }
        else
        {

            txtEmail.Enabled = false;
            txtPasswordEmail.Enabled = false;
            txtSMTP.Enabled = false;
            txtPort.Enabled = false;
            chkEnableSSL.Enabled = false;
        }



    }


    protected void rbtPartialType_OnCheckedChanged(object sender, EventArgs e)
    {
        if (rbtnPartialSalary.Checked)
        {
            pnlPartialMin.Visible = false;
            pnlPartialSal.Visible = true;

        }
        else
        {
            pnlPartialMin.Visible = true;
            pnlPartialSal.Visible = false;
        }


 }
    protected void rbtLateType_OnCheckedChanged(object sender, EventArgs e)
    {
        if (rbtnLateSalary.Checked)
        {
            pnlLateMin.Visible = false;
            pnlLateSal.Visible = true;

        }
        else
        {
            pnlLateMin.Visible = true;
            pnlLateSal.Visible = false;
        }
      

 }
    protected void rbtEarlyType_OnCheckedChanged(object sender, EventArgs e)
    {
        if (rbtnEarlySalary.Checked)
        {
            pnlEarlyMin.Visible = false;
            pnlEarlySal.Visible = true;

        }
        else
        {
            pnlEarlyMin.Visible = true;
            pnlEarlySal.Visible = false;
        }

    }
    protected void rbtLateIn_OnCheckedChanged(object sender, EventArgs e)
    {
      if(rbtLateInEnable.Checked)
      {
          rbtnLateMinutes.Enabled = true;
          rbtnLateSalary.Enabled = true;
          txtLateRelaxMin.Enabled = true;
          txtLateCount.Enabled = true;
          txtLateValue.Enabled = true;
          ddlLateType.Enabled = true;
          ddlLateMinTime.Enabled = true;

      }
      else
      {
          rbtnLateMinutes.Enabled = false;
          rbtnLateSalary.Enabled = false;
          txtLateRelaxMin.Enabled = false;
          txtLateCount.Enabled = false;
          txtLateValue.Enabled = false;
          ddlLateType.Enabled = false;
          ddlLateMinTime.Enabled = false;
      }

 }
    protected void rbtEarlyOut_OnCheckedChanged(object sender, EventArgs e)
    {
        if (rbtEarlyOutEnable.Checked)
        {
            rbtnEarlyMinutes.Enabled = true;
            rbtnEarlySalary.Enabled = true;
            txtEarlyRelaxMin.Enabled = true;
            txtEarlyCount.Enabled = true;
            txtEarlyValue.Enabled = true;
            ddlEarlyType.Enabled = true;
            ddlEarlyMinTime.Enabled = true;
        }
        else
        {
            rbtnEarlyMinutes.Enabled = false;
            rbtnEarlySalary.Enabled = false;
            txtEarlyRelaxMin.Enabled = false;
            txtEarlyCount.Enabled = false;
            txtEarlyValue.Enabled = false;
            ddlEarlyType.Enabled = false;
            ddlEarlyMinTime.Enabled = false;
        }

    }
    protected void rbtOT_OnCheckedChanged(object sender, EventArgs e)
    {
        if (rbtnOtDisable.Checked)
        {
            txtMaxOTMint.Enabled = false;
            txtMinOTMint.Enabled = false;
            ddlCalculationMethod.Enabled = false;
        }
        else
        {
            txtMaxOTMint.Enabled = true;
            txtMinOTMint.Enabled = true;
            ddlCalculationMethod.Enabled = true;
        }

    }
    protected void btnCancelOverTime_Click(object sender, EventArgs e)
    {
        Response.Redirect("../MasterSetup/CompanyMaster.aspx");
    }
    protected void ChkWeekOffList_OnSelectedIndexChanged(object sender, EventArgs e)
    {
        CheckBoxList li = (CheckBoxList)(sender);
        string id = li.Text;

        for (int i = 0; i < ChkWeekOffList.Items.Count; i++)
        {

            ChkWeekOffList.Items[i].Selected = false;
        }
        for (int i = 0; i < ChkWeekOffList.Items.Count; i++)
        {
            if (ChkWeekOffList.Items[i].Text == id)
            {
                ChkWeekOffList.Items[i].Selected = true;
            }
            else
            {
                ChkWeekOffList.Items[i].Selected = false;
            }


        }


    }



    protected void ddlSalCal_OnSelectedIndexChanged(object sender, EventArgs e)
    {
        if (ddlSalCal.SelectedValue == "Monthly")
        {
            txtFixedDays.Visible = false;
            lblDay.Visible = false;


        }
        else
        {
            txtFixedDays.Visible = true;
            lblDay.Visible = true;

        }



    }
    protected void btnKeyPref_Click(object sender, EventArgs e)
    {
        pnlSMSEmailB.BackColor = System.Drawing.ColorTranslator.FromHtml("#90bde9");

      
        pnlTimetableList.BackColor = System.Drawing.ColorTranslator.FromHtml("#90bde9");
        pnlWorkCal.BackColor = System.Drawing.ColorTranslator.FromHtml("#90bde9");
        pnlPartialLeave.BackColor = System.Drawing.ColorTranslator.FromHtml("#90bde9");
        pnlColorCode.BackColor = System.Drawing.ColorTranslator.FromHtml("#90bde9");
        pnlKeyPref.BackColor = System.Drawing.ColorTranslator.FromHtml("#ccddee");

        pnlPaneltyCalc.Visible = false;
        pnlPartialLeave1.Visible = false;
        PnlOT.Visible = false;
        pnlWork.Visible = false;
        PnlTime.Visible = false;
        pnlKeyPreference.Visible = true;
        pnlSMSEmail.Visible = false;
        pnlColor.Visible = false;
    }


    protected void btnPanelty_Click(object sender, EventArgs e)
    {
        pnlPanelty.BackColor = System.Drawing.ColorTranslator.FromHtml("#ccddee");
        pnlColorCode.BackColor = System.Drawing.ColorTranslator.FromHtml("#90bde9");
        pnlSMSEmailB.BackColor = System.Drawing.ColorTranslator.FromHtml("#90bde9");
        pnlKeyPref.BackColor = System.Drawing.ColorTranslator.FromHtml("#90bde9");
      
        pnlTimetableList.BackColor = System.Drawing.ColorTranslator.FromHtml("#90bde9");
        pnlWorkCal.BackColor = System.Drawing.ColorTranslator.FromHtml("#90bde9");
        pnlPartialLeave.BackColor = System.Drawing.ColorTranslator.FromHtml("#90bde9");
        pnlPartialLeave1.Visible = false;
        PnlOT.Visible = false;
        pnlWork.Visible = false;
        PnlTime.Visible = false;
        pnlKeyPreference.Visible = false;
        pnlSMSEmail.Visible = false;
        pnlColor.Visible = false;
        pnlPaneltyCalc.Visible = true;

    }


    protected void btnColorCode_Click(object sender, EventArgs e)
    {
        pnlPanelty.BackColor = System.Drawing.ColorTranslator.FromHtml("#90bde9");
        pnlPaneltyCalc.Visible = false;
        pnlColorCode.BackColor = System.Drawing.ColorTranslator.FromHtml("#ccddee");
        pnlSMSEmailB.BackColor = System.Drawing.ColorTranslator.FromHtml("#90bde9");
        pnlKeyPref.BackColor = System.Drawing.ColorTranslator.FromHtml("#90bde9");
      
        pnlTimetableList.BackColor = System.Drawing.ColorTranslator.FromHtml("#90bde9");
        pnlWorkCal.BackColor = System.Drawing.ColorTranslator.FromHtml("#90bde9");
        pnlPartialLeave.BackColor = System.Drawing.ColorTranslator.FromHtml("#90bde9");
        pnlPartialLeave1.Visible = false;
        PnlOT.Visible = false;
        pnlWork.Visible = false;
        PnlTime.Visible = false;
        pnlKeyPreference.Visible = false;
        pnlSMSEmail.Visible = false;
        pnlColor.Visible = true;

    }
    protected void btnPartialLeave_Click(object sender, EventArgs e)
    {
        pnlPanelty.BackColor = System.Drawing.ColorTranslator.FromHtml("#90bde9");
        pnlPaneltyCalc.Visible = false;
        pnlSMSEmailB.BackColor = System.Drawing.ColorTranslator.FromHtml("#90bde9");
        pnlKeyPref.BackColor = System.Drawing.ColorTranslator.FromHtml("#90bde9");
     
        pnlTimetableList.BackColor = System.Drawing.ColorTranslator.FromHtml("#90bde9");
        pnlWorkCal.BackColor = System.Drawing.ColorTranslator.FromHtml("#90bde9");
        pnlPartialLeave.BackColor = System.Drawing.ColorTranslator.FromHtml("#ccddee");
        pnlColorCode.BackColor = System.Drawing.ColorTranslator.FromHtml("#90bde9");
        pnlColor.Visible = false;
        pnlPartialLeave1.Visible = true;
        PnlOT.Visible = false;
        pnlWork.Visible = false;
        PnlTime.Visible = false;
        pnlKeyPreference.Visible = false;
        pnlSMSEmail.Visible = false;
    }
    protected void btnSMSEmail_Click(object sender, EventArgs e)
    {
        pnlKeyPref.BackColor = System.Drawing.ColorTranslator.FromHtml("#90bde9");

        pnlSMSEmailB.BackColor = System.Drawing.ColorTranslator.FromHtml("#ccddee");
        pnlPanelty.BackColor = System.Drawing.ColorTranslator.FromHtml("#90bde9");
        pnlPaneltyCalc.Visible = false;
     
        pnlTimetableList.BackColor = System.Drawing.ColorTranslator.FromHtml("#90bde9");
        pnlWorkCal.BackColor = System.Drawing.ColorTranslator.FromHtml("#90bde9");
        pnlPartialLeave.BackColor = System.Drawing.ColorTranslator.FromHtml("#90bde9");
        pnlColorCode.BackColor = System.Drawing.ColorTranslator.FromHtml("#90bde9");
        pnlColor.Visible = false;
        pnlPartialLeave1.Visible = false;
        PnlOT.Visible = false;
        pnlWork.Visible = false;
        PnlTime.Visible = false;

        pnlSMSEmail.Visible = true;
        pnlKeyPreference.Visible = false;
    }
    protected void btnWorkCalc_Click(object sender, EventArgs e)
    {
        pnlPanelty.BackColor = System.Drawing.ColorTranslator.FromHtml("#90bde9");
        pnlPaneltyCalc.Visible = false;
        pnlKeyPref.BackColor = System.Drawing.ColorTranslator.FromHtml("#90bde9");
        pnlSMSEmailB.BackColor = System.Drawing.ColorTranslator.FromHtml("#90bde9");
        pnlPartialLeave.BackColor = System.Drawing.ColorTranslator.FromHtml("#90bde9");

      
        pnlTimetableList.BackColor = System.Drawing.ColorTranslator.FromHtml("#90bde9");

        pnlWorkCal.BackColor = System.Drawing.ColorTranslator.FromHtml("#ccddee");
        pnlColorCode.BackColor = System.Drawing.ColorTranslator.FromHtml("#90bde9");
        pnlColor.Visible = false;
        PnlOT.Visible = false;
        pnlWork.Visible = true;
        PnlTime.Visible = false;

        pnlSMSEmail.Visible = false;
        pnlPartialLeave1.Visible = false;
        pnlKeyPreference.Visible = false;
    }
    protected void btnTime_Click(object sender, EventArgs e)
    {
        pnlPanelty.BackColor = System.Drawing.ColorTranslator.FromHtml("#90bde9");
        pnlPaneltyCalc.Visible = false;
        pnlKeyPref.BackColor = System.Drawing.ColorTranslator.FromHtml("#90bde9");
        pnlPartialLeave.BackColor = System.Drawing.ColorTranslator.FromHtml("#90bde9");
        pnlTimetableList.BackColor = System.Drawing.ColorTranslator.FromHtml("#ccddee");

       
        pnlWorkCal.BackColor = System.Drawing.ColorTranslator.FromHtml("#90bde9");
        pnlSMSEmailB.BackColor = System.Drawing.ColorTranslator.FromHtml("#90bde9");
        pnlColorCode.BackColor = System.Drawing.ColorTranslator.FromHtml("#90bde9");
        pnlColor.Visible = false;
        pnlPartialLeave1.Visible = false;
        PnlTime.Visible = true;
        PnlOT.Visible = false;
        pnlWork.Visible = false;
        pnlSMSEmail.Visible = false;
        pnlKeyPreference.Visible = false;
    }
    protected void btnOT_Click(object sender, EventArgs e)
    {
        pnlPanelty.BackColor = System.Drawing.ColorTranslator.FromHtml("#90bde9");
        pnlPaneltyCalc.Visible = false;
        pnlKeyPref.BackColor = System.Drawing.ColorTranslator.FromHtml("#90bde9");
        pnlPartialLeave.BackColor = System.Drawing.ColorTranslator.FromHtml("#90bde9");
      

        pnlTimetableList.BackColor = System.Drawing.ColorTranslator.FromHtml("#90bde9");
        pnlWorkCal.BackColor = System.Drawing.ColorTranslator.FromHtml("#90bde9");
        pnlSMSEmailB.BackColor = System.Drawing.ColorTranslator.FromHtml("#90bde9");
        pnlColorCode.BackColor = System.Drawing.ColorTranslator.FromHtml("#90bde9");
        pnlColor.Visible = false;
        pnlPartialLeave1.Visible = false;
        PnlTime.Visible = false;
        PnlOT.Visible = true;
        pnlWork.Visible = false;
        pnlSMSEmail.Visible = false;
        pnlKeyPreference.Visible = false;
    }

}
