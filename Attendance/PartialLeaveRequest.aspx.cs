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

public partial class Attendance_PartialLeaveRequest : System.Web.UI.Page
{

   
    SystemParameter objSys = new SystemParameter();
   
    EmployeeMaster objEmp = new EmployeeMaster();

    Att_PartialLeave_Request objPartial = new Att_PartialLeave_Request();
    EmployeeParameter objEmpParam = new EmployeeParameter();
    Set_ApplicationParameter objAppParam = new Set_ApplicationParameter();

    protected void Page_Load(object sender, EventArgs e)
    {
        if (Session["UserId"] == null)
        {
            Response.Redirect("~/ERPLogin.aspx");
        }
        Page.Title=objSys.GetSysTitle();
        if (!IsPostBack)
        {
            pnlList.Visible = true;
            pnlNew.Visible = false;
            FillLeaveStatus();
            btnList_Click(null, null);

        }
        Session["AccordianId"] = "9";
        Session["HeaderText"] = "Attendance Setup";
        CalendarExtender2.Format = objSys.SetDateFormat();
    }

    public string GetDate(object obj)
    {

        DateTime Date = new DateTime();
        Date = Convert.ToDateTime(obj.ToString());



        return Date.ToString(objSys.SetDateFormat());

    }

    protected void btnList_Click(object sender, EventArgs e)
    {

        pnlMenuList.BackColor = System.Drawing.ColorTranslator.FromHtml("#ccddee");

        pnlMenuNew.BackColor = System.Drawing.ColorTranslator.FromHtml("#90bde9");
        pnlList.Visible = true;
        pnlNew.Visible = false;

       
    }
    protected void btnNew_Click(object sender, EventArgs e)
    {

        pnlMenuNew.BackColor = System.Drawing.ColorTranslator.FromHtml("#ccddee");

        pnlMenuList.BackColor = System.Drawing.ColorTranslator.FromHtml("#90bde9");
        pnlList.Visible = false;
        pnlNew.Visible = true;

    }
    protected void txtEmpName_textChanged(object sender, EventArgs e)
    {
        string empid = string.Empty;

        if (txtEmpName.Text != "")
        {
            empid = txtEmpName.Text.Split('/')[txtEmpName.Text.Split('/').Length - 1];

            DataTable dtEmp = objEmp.GetEmployeeMaster(Session["CompId"].ToString());

            dtEmp = new DataView(dtEmp, "Emp_Code='" + empid + "'", "", DataViewRowState.CurrentRows).ToTable();

            if (dtEmp.Rows.Count > 0)
            {
                empid = dtEmp.Rows[0]["Emp_Id"].ToString();
                hdnEmpId.Value = empid;

                ;
            }
            else
            {
                DisplayMessage("Employee Not Exists");
                txtEmpName.Text = "";
                txtEmpName.Focus();
                hdnEmpId.Value = "";
                return;
            }



        }




    }
    protected void btnApply_Click(object sender, EventArgs e)
    {
        int b = 0;
        if(rbtnOfficial.Checked==false && rbtnPersonal.Checked==false)
        {
            DisplayMessage("Please select Personal or Official");
            return;

        }

        if(txtEmpName.Text=="")
        {
            DisplayMessage("Enter Employee Name");
            txtEmpName.Focus();
            return;

        }

        if (txtApplyDate.Text == "")
        {
            DisplayMessage("Enter Apply Date");
            txtApplyDate.Focus();
          
            return;

        }
        else
        {
            try
            {
                objSys.getDateForInput(txtApplyDate.Text);
            }
            catch
            {
                DisplayMessage("Enter Correct Apply Date Format dd-MMM-yyyy");
                txtApplyDate.Focus();
               
                return;

            }

        }
        if (txtInTime.Text == "")
        {
            DisplayMessage("Enter In Time");
            txtInTime.Focus();

            return;
        }   
        if(txtOuttime.Text=="")
        {
            DisplayMessage("Enter Out Time");
            txtOuttime.Focus();

            return;
        }
        if(txtDescription.Text=="")
        {
            DisplayMessage("Enter Description");
            txtDescription.Focus();

            return;

        }
        if (rbtnPersonal.Checked)
        {
            bool IsCompPartial = Convert.ToBoolean(objAppParam.GetApplicationParameterValueByParamName("Partial_Leave_Enable", Session["CompId"].ToString()));
            if (IsCompPartial)
            {
                DataTable dtEmpParam = objEmpParam.GetEmployeeParameterByEmpId(hdnEmpId.Value,Session["CompId"].ToString());

                if (dtEmpParam.Rows.Count > 0)
                {

                    bool IsEmpPartial = Convert.ToBoolean(dtEmpParam.Rows[0]["Is_Partial_Enable"].ToString());
                    if (IsEmpPartial)
                    {
                        int totalminutes =0;
                        int useinday = 0;

                        double leaveCount = 0;


                        totalminutes=int.Parse(dtEmpParam.Rows[0]["Partial_Leave_Mins"].ToString());
                        useinday = int.Parse(dtEmpParam.Rows[0]["Partial_Leave_Day"].ToString());

                        leaveCount = double.Parse(dtEmpParam.Rows[0]["Partial_Leave_Mins"].ToString()) / double.Parse(dtEmpParam.Rows[0]["Partial_Leave_Day"].ToString());

                        leaveCount = System.Math.Round(leaveCount);

                        leaveCount = leaveCount - getCurrentMonthLeaveCount(objSys.getDateForInput(txtApplyDate.Text));

                        if (leaveCount > 0)
                        {

                            if (totalminutes > 0)
                            {
                                int CurrentUseMin = getCurrentMonth(objSys.getDateForInput(txtApplyDate.Text));
                                if (CurrentUseMin > 0)
                                {
                                    totalminutes = totalminutes - CurrentUseMin;

                                }

                                int OneDayMin = getMinuteInADay(objSys.getDateForInput(txtApplyDate.Text));
                                if(OneDayMin >= useinday)
                                {
                                    DisplayMessage("You cannot request more than " + useinday.ToString() + " minutes in a day");
                                    return;
                                }

                                int RequestMin = GetMinuteDiff(txtOuttime.Text, txtInTime.Text);
                                if (RequestMin <= totalminutes)
                                {
                                    if (RequestMin > useinday)
                                    {
                                        DisplayMessage("You cannot request more than " + useinday.ToString() + " minutes in a day");
                                        return;
                                    }
                                    else
                                    {



                                        int remainmin = totalminutes - RequestMin;
if(hdnEdit.Value=="")
{
    b = objPartial.InsertPartialLeaveRequest(Session["CompId"].ToString(), hdnEmpId.Value, "0", DateTime.Now.ToString(), objSys.getDateForInput(txtApplyDate.Text).ToString(), txtInTime.Text, txtOuttime.Text, "Pending", txtDescription.Text, "", "", "", "", "", "", true.ToString(), DateTime.Now.ToString(), true.ToString(), Session["UserId"].ToString(), DateTime.Now.ToString(), Session["UserId"].ToString(), DateTime.Now.ToString());

         if (b != 0)
         {
             DisplayMessage("Partial leave submitted");
             btnReset_Click(null, null);
             FillLeaveStatus();
         }


}
                                        else
            {
                b = objPartial.UpdatePartialLeaveRequest(hdnEdit.Value, Session["CompId"].ToString(), hdnEmpId.Value, "1", DateTime.Now.ToString(), objSys.getDateForInput(txtApplyDate.Text).ToString(), txtInTime.Text, txtOuttime.Text, "Pending", txtDescription.Text, "", "", "", "", "", "", true.ToString(), DateTime.Now.ToString(), true.ToString(), Session["UserId"].ToString(), DateTime.Now.ToString());


                if (b != 0)
                {
                    DisplayMessage("Partial leave updated");
                    btnCancel_Click(null, null);
                    FillLeaveStatus();
                    hdnEdit.Value = "";
                    btnNew.Text = Resources.Attendance.New;
                    txtEmpName.Enabled = true;
                }


            }
                                   
                                      

                                    }
                                }
                                else
                                {
                                    DisplayMessage("Employee does not have sufficient balance");
                                    return;

                                }
                            }
                            else
                            {
                                DisplayMessage("Employee does not have sufficient balance");
                                return;
                            }

                        }
                        else
                        {
                            DisplayMessage("Employee does not have sufficient balance");
                            return;

                        }
                    }
                    else
                    {
                        DisplayMessage("Partial leave cannot assign to this employee");
                        return;
                    }

                }
                else
                {

                    DisplayMessage("Partial leave cannot assign to this employee");
                    return;
                }




            }
            else
            {
                DisplayMessage("Company does not provide partial leave");
                return;

            }


        }
        else
        {
             bool IsCompPartial = Convert.ToBoolean(objAppParam.GetApplicationParameterValueByParamName("Partial_Leave_Enable", Session["CompId"].ToString()));
             if (IsCompPartial)
             {


                 if (hdnEdit.Value == "")
                 {

                     b = objPartial.InsertPartialLeaveRequest(Session["CompId"].ToString(), hdnEmpId.Value, "1", DateTime.Now.ToString(), objSys.getDateForInput(txtApplyDate.Text).ToString(), txtInTime.Text, txtOuttime.Text, "Pending", txtDescription.Text, "", "", "", "", "", "", true.ToString(), DateTime.Now.ToString(), true.ToString(), Session["UserId"].ToString(), DateTime.Now.ToString(), Session["UserId"].ToString(), DateTime.Now.ToString());

                     if (b != 0)
                     {
                         DisplayMessage("Partial leave submitted");
                         btnReset_Click(null, null);
                         FillLeaveStatus();
                     }
                 }
                 else
                 {
                     b = objPartial.UpdatePartialLeaveRequest(hdnEdit.Value, Session["CompId"].ToString(), hdnEmpId.Value, "1", DateTime.Now.ToString(), objSys.getDateForInput(txtApplyDate.Text).ToString(), txtInTime.Text, txtOuttime.Text, "Pending", txtDescription.Text, "", "", "", "", "", "", true.ToString(), DateTime.Now.ToString(), true.ToString(), Session["UserId"].ToString(), DateTime.Now.ToString());
                     if (b != 0)
                     {
                         DisplayMessage("Partial leave updated");
                         btnCancel_Click(null, null);
                         FillLeaveStatus();
                         hdnEdit.Value = "";
                         btnNew.Text = Resources.Attendance.New;
                         txtEmpName.Enabled = true; 
                     }

                 }
             }
             else
             {
                 DisplayMessage("Company does not provide partial leave");
                 return;


             }

        }


    }


    public int getCurrentMonthLeaveCount(DateTime applydate)
    {
        int Count = 0;

        DataTable dt = objPartial.GetPartialLeaveRequestByEmpIdAndCurrentMonthYear(Session["CompId"].ToString(), hdnEmpId.Value, applydate.Month.ToString(), applydate.Year.ToString());
        dt = new DataView(dt, "Is_Confirmed='Approved' and Partial_Leave_Type='0'", "", DataViewRowState.CurrentRows).ToTable();
        if (dt.Rows.Count > 0)
        {
            for (int i = 0; i < dt.Rows.Count; i++)
            {
                Count++;
            }

        }
        else
        {
            Count = 0;

        }

        return Count;
    }

    public int getCurrentMonth(DateTime applydate)
    {
        int useminutes = 0;

        DataTable dt = objPartial.GetPartialLeaveRequestByEmpIdAndCurrentMonthYear(Session["CompId"].ToString(), hdnEmpId.Value, applydate.Month.ToString(), applydate.Year.ToString());
        dt = new DataView(dt, "Is_Confirmed='Approved' and Partial_Leave_Type='0'", "", DataViewRowState.CurrentRows).ToTable(); 
        if (dt.Rows.Count > 0)
        {
            for (int i = 0; i < dt.Rows.Count;i++)
            {
                useminutes += GetMinuteDiff(dt.Rows[i]["To_Time"].ToString(), dt.Rows[i]["From_Time"].ToString());

            }

        }
        else
        {
            useminutes = 0;

        }

        return useminutes;
    }

    public int getMinuteInADay(DateTime applydate)
    {
        int useminutes = 0;

        DataTable dt = objPartial.GetPartialLeaveRequestById(Session["CompId"].ToString(), hdnEmpId.Value);
        dt = new DataView(dt, "Is_Confirmed='Approved' and Partial_Leave_Type='0' and Partial_Leave_Date='"+applydate.ToString()+"'", "", DataViewRowState.CurrentRows).ToTable();
        if (dt.Rows.Count > 0)
        {
            for (int i = 0; i < dt.Rows.Count; i++)
            {
                useminutes += GetMinuteDiff(dt.Rows[i]["To_Time"].ToString(), dt.Rows[i]["From_Time"].ToString());

            }

        }
        else
        {
            useminutes = 0;

        }

        return useminutes;
    }
    private int GetMinuteDiff(string greatertime, string lesstime)
    {
        int retval = 0;
        int actTimeHour = Convert.ToInt32(greatertime.Split(':')[0]);
        int ondutyhour = Convert.ToInt32(lesstime.Split(':')[0]);
        int actTimeMinute = Convert.ToInt32(greatertime.Split(':')[1]);
        int ondutyMinute = Convert.ToInt32(lesstime.Split(':')[1]);
        int totalActTimeMinute = actTimeHour * 60 + actTimeMinute;
        int totalOnDutyTimeMinute = ondutyhour * 60 + ondutyMinute;
        if (totalActTimeMinute - totalOnDutyTimeMinute < 0)
        {
            retval = 1440 + (totalActTimeMinute - totalOnDutyTimeMinute);
        }
        else
        {
            retval = (totalActTimeMinute - totalOnDutyTimeMinute);
        }
        return retval;
    }
    public string leavetype(string type)
    {
        string t=string.Empty;
        if(type=="0")
        {
            t = "Personal";

        }
        else
        {
            t = "Official";

        }
        return t;
    }
    public void FillLeaveStatus()
    {
        DataTable dtLeave = objPartial.GetPartialLeaveRequest(Session["CompId"].ToString());

        dtLeave = new DataView(dtLeave, "Brand_Id='" + HttpContext.Current.Session["BrandId"].ToString() + "' and Location_Id='" + HttpContext.Current.Session["LocId"].ToString() + "'", "", DataViewRowState.CurrentRows).ToTable();
      
        dtLeave = new DataView(dtLeave, "Is_Confirmed='Pending'", "", DataViewRowState.CurrentRows).ToTable();

        if (dtLeave.Rows.Count > 0)
        {
            gvLeaveStatus.DataSource = dtLeave;
            gvLeaveStatus.DataBind();
            Session["dtLeaveStatus"] = dtLeave;
            

        }
        else
        {
            gvLeaveStatus.DataSource = null;
            gvLeaveStatus.DataBind();
            Session["dtLeaveStatus"] = null;

        }



    }
    protected void gvLeaveStatus_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        gvLeaveStatus.PageIndex = e.NewPageIndex;

        DataTable dt = (DataTable)Session["dtLeaveStatus"];
        gvLeaveStatus.DataSource = dt;
        gvLeaveStatus.DataBind();

        foreach (GridViewRow gvr in gvLeaveStatus.Rows)
        {
            Label lblStatus = (Label)(gvr.FindControl("lblStatus"));
            if (lblStatus.Text != "Pending")
            {
                ImageButton imgBtnApprove = (ImageButton)(gvr.FindControl("IbtnApprove"));
                ImageButton imgBtnReject = (ImageButton)(gvr.FindControl("IbtnReject"));
                imgBtnApprove.Visible = false;
                imgBtnReject.Visible = false;

            }

        }
    }
    protected void btnApprove_Command(object sender, CommandEventArgs e)
    {
        int b = 0;



        DataTable dtEmpParam = objEmpParam.GetEmployeeParameterByEmpId(e.CommandName.ToString(), Session["CompId"].ToString());
        
          int totalminutes =0;
                        int useinday = 0;

                        double leaveCount = 0;


                        totalminutes=int.Parse(dtEmpParam.Rows[0]["Partial_Leave_Mins"].ToString());
                        useinday = int.Parse(dtEmpParam.Rows[0]["Partial_Leave_Day"].ToString());

                        leaveCount = double.Parse(dtEmpParam.Rows[0]["Partial_Leave_Mins"].ToString()) / double.Parse(dtEmpParam.Rows[0]["Partial_Leave_Day"].ToString());

                        leaveCount = System.Math.Round(leaveCount);

                        DataTable dt = objPartial.GetPartialLeaveRequestByTransId(Session["CompId"].ToString(), e.CommandArgument.ToString());
                     

         
                       if (dt.Rows.Count > 0)
                        {
                           if(dt.Rows[0]["Partial_Leave_Type"].ToString()=="1")
                           {

                               b = objPartial.PartialLeaveApproveReject(e.CommandArgument.ToString(), "Approved", true.ToString(), Session["UserId"].ToString(), DateTime.Now.ToString());

                               if (b != 0)
                               {
                                   DisplayMessage("Leave Approved");
                                   FillLeaveStatus();

                               }
                           }
                           else
                           {


                            hdnEmpId.Value = e.CommandName.ToString();
                            leaveCount = leaveCount - getCurrentMonthLeaveCount(Convert.ToDateTime(dt.Rows[0]["Partial_Leave_Date"].ToString()));

                            int OneDayMin = getMinuteInADay(Convert.ToDateTime(dt.Rows[0]["Partial_Leave_Date"].ToString()));
                            if (OneDayMin >= useinday)
                            {
                                DisplayMessage("You cannot request more than " + useinday.ToString() + " minutes in a day");
                                return;
                            }
                                if (leaveCount > 0)
                                {
                                    if (totalminutes > 0)
                                    {
                                        int CurrentUseMin = getCurrentMonth(Convert.ToDateTime(dt.Rows[0]["Partial_Leave_Date"].ToString()));
                                        if (CurrentUseMin > 0)
                                        {
                                            totalminutes = totalminutes - CurrentUseMin;

                                        }

                                        int RequestMin = GetMinuteDiff(dt.Rows[0]["To_Time"].ToString(), dt.Rows[0]["From_Time"].ToString());
                                        if (RequestMin <= totalminutes)
                                        {
                                            if (RequestMin > useinday)
                                            {
                                                DisplayMessage("You cannot request more than " + useinday.ToString() + " minutes in a day");
                                                return;
                                            }
                                            else
                                            {




                                            }
                                        }
                                        else
                                        {
                                            DisplayMessage("Employee does not have sufficient balance");
                                            return;

                                        }
                                    }
                                    else
                                    {
                                        DisplayMessage("Employee does not have sufficient balance");
                                        return;
                                    }
                                }
                                else
                                {
                                    DisplayMessage("Employee does not have sufficient balance");
                                    return;
                                }
                                b = objPartial.PartialLeaveApproveReject(e.CommandArgument.ToString(), "Approved", true.ToString(), Session["UserId"].ToString(), DateTime.Now.ToString());

                                if (b != 0)
                                {
                                    DisplayMessage("Leave Approved");
                                    FillLeaveStatus();

                                }
                       }
                        }


       

    }


    protected void btnEdit_Command(object sender, CommandEventArgs e)
        {
            txtEmpName.Enabled = false;
        Common cmn=new Common ();
        hdnEmpId.Value = e.CommandName.ToString();
        hdnEdit.Value = e.CommandArgument.ToString();
        DataTable dt = new DataTable();
        dt = objPartial.GetPartialLeaveRequestByTransId(Session["CompId"].ToString(),e.CommandArgument.ToString());

        if(dt.Rows.Count > 0)
        {
            txtEmpName.Text = cmn.GetEmpName(e.CommandName.ToString());

            if (dt.Rows[0]["Partial_Leave_Type"].ToString() == "0")
            {
                rbtnPersonal.Checked = true;
                rbtnOfficial.Checked = false;
            }
            else
            {
                rbtnOfficial.Checked = true;
                rbtnPersonal.Checked = false;
            }

            txtApplyDate.Text = Convert.ToDateTime(dt.Rows[0]["Partial_Leave_Date"].ToString()).ToString(objSys.SetDateFormat());
            txtInTime.Text = dt.Rows[0]["From_Time"].ToString();
            txtOuttime.Text = dt.Rows[0]["To_Time"].ToString();
            txtDescription.Text = dt.Rows[0]["Description"].ToString();


        btnNew_Click(null, null);
        btnNew.Text = Resources.Attendance.Edit;


        }
        }


    protected void IbtnReject_Command(object sender, CommandEventArgs e)
    {

        int b = 0;
        b = objPartial.PartialLeaveApproveReject(e.CommandArgument.ToString(), "Canceled", true.ToString(), Session["UserId"].ToString(), DateTime.Now.ToString());

        if (b != 0)
        {
            DisplayMessage("Leave Rejected");
            FillLeaveStatus();

        }

    }
    protected void btnReset_Click(object sender, EventArgs e)
    {
        rbtnOfficial.Checked= false;
        rbtnPersonal.Checked = false;
        txtEmpName.Text = "";
        txtApplyDate.Text = "";
        txtInTime.Text = "";
        txtOuttime.Text = "";
        txtDescription.Text = "";
        txtEmpName.Enabled = true;
        btnNew.Text = Resources.Attendance.New;
        hdnEdit.Value = "";

    }
    protected void btnCancel_Click(object sender, EventArgs e)
    {
        rbtnOfficial.Checked = false;
        rbtnPersonal.Checked = false;
        txtEmpName.Text = "";
        txtApplyDate.Text = "";
        txtInTime.Text = "";
        txtOuttime.Text = "";
        txtDescription.Text = "";
        txtEmpName.Enabled = true;
        btnNew.Text = Resources.Attendance.New;
        hdnEdit.Value = "";
        btnList_Click(null,null);

    }



    public void DisplayMessage(string str)
    {
        ScriptManager.RegisterStartupScript(this, GetType(), "", "alert('" + str + "');", true);
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
}
