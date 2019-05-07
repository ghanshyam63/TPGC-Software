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

public partial class AttendanceReport : BasePage
{
    Att_AttendanceLog objAttLog = new Att_AttendanceLog();
    Set_EmployeeGroup_Master objEmpGroup = new Set_EmployeeGroup_Master();
    SystemParameter objSys = new SystemParameter();
    Set_Group_Employee objGroupEmp = new Set_Group_Employee();
    EmployeeMaster objEmp = new EmployeeMaster();
    Att_TimeTable objTimeTable = new Att_TimeTable();
    Set_ApplicationParameter objAppParam = new Set_ApplicationParameter();
    Att_ScheduleMaster objEmpSch = new Att_ScheduleMaster();
    Att_ShiftDescription objShift = new Att_ShiftDescription();
    Set_Employee_Holiday objEmpHoliday = new Set_Employee_Holiday();
    Att_Leave_Request ObjLeaveReq = new Att_Leave_Request();
    EmployeeParameter objEmpParam = new EmployeeParameter();
    Att_AttendanceRegister objAttReg = new Att_AttendanceRegister();
    Att_PartialLeave_Request objPartialReq = new Att_PartialLeave_Request();
    Pay_Employee_Attendance objPayEmpAtt = new Pay_Employee_Attendance();

    protected void Page_Load(object sender, EventArgs e)
    {

        if (Session["UserId"] == null)
        {
            Response.Redirect("~/ERPLogin.aspx");
        }


       
        Session["AccordianId"] = "6";
        Session["HeaderText"] = "Attendance Reports";
        Page.Title = objSys.GetSysTitle();
        if (!IsPostBack)
        {

            pnlEmpAtt.Visible = true;

            FillGrid();
            rbtnEmpSal.Checked = true;
            rbtnGroupSal.Checked = false;
            EmpGroupSal_CheckedChanged(null, null);
            pnlReport.Visible = false;
 if (Session["EmpList"] != null)
            {
                pnlEmpAtt.Visible = false;
                pnlReport.Visible = true;
            }

            

        }
        txtFrom_CalendarExtender.Format = objSys.SetDateFormat();
        CalendarExtender1.Format = objSys.SetDateFormat();

    }

    public void FillGrid()
    {
        DataTable dtEmp = objEmp.GetEmployeeMaster(Session["CompId"].ToString());

        dtEmp = new DataView(dtEmp, "Brand_Id='" + Session["BrandId"].ToString() + "'  and Location_Id='" + Session["LocId"].ToString() + "'", "", DataViewRowState.CurrentRows).ToTable();


        if (Session["SessionDepId"] != null)
        {
           
            dtEmp = new DataView(dtEmp, "Department_Id in(" + Session["SessionDepId"].ToString().Substring(0, Session["SessionDepId"].ToString().Length - 1) + ")", "", DataViewRowState.CurrentRows).ToTable();

        }
        if (dtEmp.Rows.Count > 0)
        {
            Session["dtEmp"] = dtEmp;
            gvEmployee.DataSource = dtEmp;
            gvEmployee.DataBind();
            lblTotalRecord.Text = Resources.Attendance.Total_Records + " : " + dtEmp.Rows.Count.ToString() + "";

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
                gvEmployeeSal.DataSource = dtEmp;
                gvEmployeeSal.DataBind();
            }
        }
        else
        {
            gvEmployeeSal.DataSource = null;
            gvEmployeeSal.DataBind();

        }
    }
    protected void EmpGroupSal_CheckedChanged(object sender, EventArgs e)
    {

        if (rbtnGroupSal.Checked)
        {
            pnlEmp.Visible = false;
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
            pnlEmp.Visible = true;
            pnlGroupSal.Visible = false;

            lblEmp.Text = "";
            lblSelectRecord.Text = "";
            string GroupIds = string.Empty;
            string EmpIds = string.Empty;
            DataTable dtEmp = objEmp.GetEmployeeMaster(Session["CompId"].ToString());

            dtEmp = new DataView(dtEmp, "Brand_Id='" + Session["BrandId"].ToString() + "'  and Location_Id='" + Session["LocId"].ToString() + "'", "", DataViewRowState.CurrentRows).ToTable();


            if (Session["SessionDepId"] != null)
            {

                dtEmp = new DataView(dtEmp, "Department_Id in(" + Session["SessionDepId"].ToString().Substring(0, Session["SessionDepId"].ToString().Length - 1) + ")", "", DataViewRowState.CurrentRows).ToTable();

            }

            if (dtEmp.Rows.Count > 0)
            {
                Session["dtEmp4"] = dtEmp;

                gvEmployee.DataSource = dtEmp;
                gvEmployee.DataBind();

            }
            else
            {
                gvEmployee.DataSource = null;
                gvEmployee.DataBind();

            }

        }


    }

    protected void gvEmployeeSal_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        gvEmployeeSal.PageIndex = e.NewPageIndex;
        gvEmployeeSal.DataSource = (DataTable)Session["dtEmp4"];
        gvEmployeeSal.DataBind();
    }

    protected void gvEmp_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {

        gvEmployee.PageIndex = e.NewPageIndex;
        DataTable dtEmp = (DataTable)Session["dtEmp"];
        gvEmployee.DataSource = dtEmp;
        gvEmployee.DataBind();
        string temp = string.Empty;


        for (int i = 0; i < gvEmployee.Rows.Count; i++)
        {
            Label lblconid = (Label)gvEmployee.Rows[i].FindControl("lblEmpId");
            string[] split = lblSelectRecord.Text.Split(',');

            for (int j = 0; j < lblSelectRecord.Text.Split(',').Length; j++)
            {
                if (lblSelectRecord.Text.Split(',')[j] != "")
                {
                    if (lblconid.Text.Trim().ToString() == lblSelectRecord.Text.Split(',')[j].Trim().ToString())
                    {
                        ((CheckBox)gvEmployee.Rows[i].FindControl("chkgvSelect")).Checked = true;
                    }
                }
            }
        }
    }

    protected void chkgvSelect_CheckedChanged(object sender, EventArgs e)
    {
        string empidlist = string.Empty;
        string temp = string.Empty;
        int index = ((GridViewRow)((CheckBox)sender).Parent.Parent).RowIndex;
        Label lb = (Label)gvEmployee.Rows[index].FindControl("lblEmpId");
        if (((CheckBox)gvEmployee.Rows[index].FindControl("chkgvSelect")).Checked)
        {
            empidlist += lb.Text.Trim().ToString() + ",";
            lblSelectRecord.Text += empidlist;

        }

        else
        {

            empidlist += lb.Text.ToString().Trim();
            lblSelectRecord.Text += empidlist;
            string[] split = lblSelectRecord.Text.Split(',');
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
            lblSelectRecord.Text = temp;
        }
    }



    protected void ImgbtnSelectAll_Clickary(object sender, ImageClickEventArgs e)
    {
        DataTable dtProduct = (DataTable)Session["dtEmp"];

        if (ViewState["Select"] == null)
        {
            ViewState["Select"] = 1;
            foreach (DataRow dr in dtProduct.Rows)
            {
                if (!lblSelectRecord.Text.Split(',').Contains(dr["Emp_Id"]))
                {
                    lblSelectRecord.Text += dr["Emp_Id"] + ",";
                }
            }
            for (int i = 0; i < gvEmployee.Rows.Count; i++)
            {
                string[] split = lblSelectRecord.Text.Split(',');
                Label lblconid = (Label)gvEmployee.Rows[i].FindControl("lblEmpId");
                for (int j = 0; j < lblSelectRecord.Text.Split(',').Length; j++)
                {
                    if (lblSelectRecord.Text.Split(',')[j] != "")
                    {
                        if (lblconid.Text.Trim().ToString() == lblSelectRecord.Text.Split(',')[j].Trim().ToString())
                        {
                            ((CheckBox)gvEmployee.Rows[i].FindControl("chkgvSelect")).Checked = true;
                        }
                    }
                }
            }
        }
        else
        {
            lblSelectRecord.Text = "";
            DataTable dtProduct1 = (DataTable)Session["dtEmp"];
            gvEmployee.DataSource = dtProduct1;
            gvEmployee.DataBind();
            ViewState["Select"] = null;
        }

    }



    private string GetTime24(string timepart)
    {
        string str = "00:00";
        DateTime date = Convert.ToDateTime(timepart);
        str = date.ToString("HH:mm");
        return str;
    }







   
    protected void btnGenerate_Click(object sender, EventArgs e)
    {


        int b = 0;
        // Selected Emp Id 
        string empidlist = lblSelectRecord.Text;


        try
        {
            if (txtFromDate.Text == "")
            {
                DisplayMessage("From Date Required");
                txtFromDate.Focus();
                return;
            }
            if (txtToDate.Text == "")
            {
                DisplayMessage("To Date Required");
                txtToDate.Focus();
                return;
            }

            if (objSys.getDateForInput(txtFromDate.Text.ToString()) > objSys.getDateForInput(txtToDate.Text.ToString()))
            {
                DisplayMessage("To Date should be greater");




                return;
            }
        }
        catch (Exception)
        {

            DisplayMessage("Date not in proper format");

            return;
        }
       
       
        if (rbtnEmpSal.Checked)
        {
            if (empidlist == "")
            {
                DisplayMessage("Select Atleast One Employee");
                return;
            }


        }
        else
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
                empidlist = "";
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
                        empidlist += str+",";

                    }
                }

                if (empidlist == "")
                {
                    DisplayMessage("Employees are not exists in selected groups");
                    return;
                }
            }
            else
            {
                DisplayMessage("Select Group First");
            }

        }
        Session["EmpList"]=empidlist;
        Session["FromDate"] = txtFromDate.Text;
        Session["ToDate"] = txtToDate.Text;
        pnlEmpAtt.Visible = false;
        pnlReport.Visible = true;

        //for (int i = 0; i < empidlist.Split(',').Length; i++)
        //{
        //    if (empidlist.Split(',')[i] == "")
        //    {
        //        continue;
        //    }

        //}       
        


      




    }



    protected void lnkChangeFilter_Click(object sender, EventArgs e)
    {
       
        txtFromDate.Text = "";
        txtToDate.Text = "";

        txtFromDate.Text = "";
        pnlEmpAtt.Visible = true;
        pnlReport.Visible = false;
        lblSelectRecord.Text = "";
        FillGrid();
        rbtnEmpSal.Checked = true;
        rbtnGroupSal.Checked = false;
        EmpGroupSal_CheckedChanged(null, null);

    }


    private int GetTimeDifference(DateTime inTime, DateTime outTime)
    {
        outTime = Convert.ToDateTime(outTime);
        // On Duty time  = in Time
        // OutTime ==  Actual In Time
        int timeDifference = 0;
        if (outTime >= inTime)
        {
            timeDifference = outTime.Subtract(inTime).Hours * 60 + outTime.Subtract(inTime).Minutes;
        }
        else
        {
            DateTime TempDateIn = new DateTime(inTime.Year, inTime.Month, inTime.Day, 23, 59, 0);
            DateTime TempDateOut = new DateTime(outTime.Year, outTime.Month, outTime.Day, 0, 0, 0);
            timeDifference = TempDateIn.Subtract(inTime).Hours * 60 + TempDateIn.Subtract(inTime).Minutes;
            timeDifference += outTime.Subtract(TempDateOut).Hours * 60 + outTime.Subtract(TempDateOut).Minutes + 1;
        }
        return timeDifference;
    }

    
    public void DisplayMessage(string str)
    {
        ScriptManager.RegisterStartupScript(this, GetType(), "", "alert('" + str + "');", true);
    }
    protected void btnReset_Click(object sender, EventArgs e)
    {
        txtFromDate.Text = "";
        txtToDate.Text = "";
      
        lblSelectRecord.Text = "";
        DataTable dtEmp = objEmp.GetEmployeeMaster(Session["CompId"].ToString());

        dtEmp = new DataView(dtEmp, "Brand_Id='" + Session["BrandId"].ToString() + "'  and Location_Id='" + Session["LocId"].ToString() + "'", "", DataViewRowState.CurrentRows).ToTable();

        if (Session["SessionDepId"] != null)
        {
            dtEmp = new DataView(dtEmp, "Department_Id in(" + Session["SessionDepId"].ToString().Substring(0, Session["SessionDepId"].ToString().Length - 1) + ")", "", DataViewRowState.CurrentRows).ToTable();

        }
        if (dtEmp.Rows.Count > 0)
        {
            Session["dtEmp"] = dtEmp;
            gvEmployee.DataSource = dtEmp;
            gvEmployee.DataBind();
            lblTotalRecord.Text = Resources.Attendance.Total_Records + " : " + dtEmp.Rows.Count.ToString() + "";

        }

        rbtnEmpSal.Checked = true;
        rbtnGroupSal.Checked = false;
        EmpGroupSal_CheckedChanged(null, null);
        btnaryRefresh_Click1(null, null);
    }


    protected void btnarybind_Click1(object sender, ImageClickEventArgs e)
    {
        if (ddlOption.SelectedIndex != 0)
        {
            string condition = string.Empty;
            if (ddlOption.SelectedIndex == 1)
            {
                condition = "convert(" + ddlField.SelectedValue + ",System.String)='" + txtValue.Text.Trim() + "'";
            }
            else if (ddlOption.SelectedIndex == 2)
            {
                condition = "convert(" + ddlField.SelectedValue + ",System.String) like '%" + txtValue.Text.Trim() + "%'";
            }
            else
            {
                condition = "convert(" + ddlField.SelectedValue + ",System.String) Like '" + txtValue.Text.Trim() + "%'";

            }
            DataTable dtEmp = (DataTable)Session["dtEmp"];
            DataView view = new DataView(dtEmp, condition, "", DataViewRowState.CurrentRows);
            gvEmployee.DataSource = view.ToTable();
            gvEmployee.DataBind();
            lblTotalRecord.Text = Resources.Attendance.Total_Records + " : " + (view.ToTable()).Rows.Count.ToString() + "";

        }

    }


    protected void btnaryRefresh_Click1(object sender, ImageClickEventArgs e)
    {
        lblSelectRecord.Text = "";
        ddlField.SelectedIndex = 1;
        ddlOption.SelectedIndex = 2;
        txtValue.Text = "";

        DataTable dtEmp = (DataTable)Session["dtEmp"];
        gvEmployee.DataSource = dtEmp;
        gvEmployee.DataBind();
        lblTotalRecord.Text = Resources.Attendance.Total_Records + " : " + dtEmp.Rows.Count.ToString() + "";


    }

    protected void chkgvSelectAll_CheckedChanged(object sender, EventArgs e)
    {
        CheckBox chkSelAll = ((CheckBox)gvEmployee.HeaderRow.FindControl("chkgvSelectAll"));
        for (int i = 0; i < gvEmployee.Rows.Count; i++)
        {
            ((CheckBox)gvEmployee.Rows[i].FindControl("chkgvSelect")).Checked = chkSelAll.Checked;
            if (chkSelAll.Checked)
            {
                if (!lblSelectRecord.Text.Split(',').Contains(((Label)(gvEmployee.Rows[i].FindControl("lblEmpId"))).Text.Trim().ToString()))
                {
                    lblSelectRecord.Text += ((Label)(gvEmployee.Rows[i].FindControl("lblEmpId"))).Text.Trim().ToString() + ",";
                }
            }
            else
            {
                string temp = string.Empty;
                string[] split = lblSelectRecord.Text.Split(',');
                foreach (string item in split)
                {
                    if (item != ((Label)(gvEmployee.Rows[i].FindControl("lblEmpId"))).Text.Trim().ToString())
                    {
                        if (item != "")
                        {
                            temp += item + ",";
                        }
                    }
                }
                lblSelectRecord.Text = temp;
            }
        }


    }

}
