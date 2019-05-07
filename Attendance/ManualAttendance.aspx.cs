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

public partial class Attendance_ManualAttendance : BasePage
{
    Att_AttendanceLog objAttLog = new Att_AttendanceLog();
    Set_EmployeeGroup_Master objEmpGroup = new Set_EmployeeGroup_Master();
    SystemParameter objSys = new SystemParameter();
    Set_Group_Employee objGroupEmp = new Set_Group_Employee();
    EmployeeMaster objEmp = new EmployeeMaster();

    protected void Page_Load(object sender, EventArgs e)
    {
      
        if (Session["UserId"] == null)
        {
            Response.Redirect("~/ERPLogin.aspx");
        }
        Page.Title = objSys.GetSysTitle();
        if(!IsPostBack)
        {
            pnlAttList.Visible = true;
            pnlEmpAtt.Visible = false;
            FillGrid();
            btnList_Click(null,null);
            rbtnByManual.Checked = true;
            rbtnByTour.Checked = false;
        }
        Session["AccordianId"] = "9";
        Session["HeaderText"] = "Attendance Setup";
        CalendarExtender1.Format = objSys.SetDateFormat();
        CalendarExtender2.Format = objSys.SetDateFormat();
        txtFrom_CalendarExtender.Format = objSys.SetDateFormat();
    }


    public string GetDate(object obj)
    {

        DateTime Date = new DateTime();
        Date = Convert.ToDateTime(obj.ToString());



        return Date.ToString(objSys.SetDateFormat());

    }
    protected void btnResetLog_Click(object sender, EventArgs e)
    {
        btnList_Click(null,null);
 }
    protected void btnSearch_Click(object sender, EventArgs e)
    {
        if (txtFromDate.Text == "" || txtFromDate.Text == "")
        {

            DisplayMessage("Fill From Date And To Date");

            return;
        }
        try
        {
            objSys.getDateForInput(txtFromDate.Text);
        }
        catch (Exception)
        {

            DisplayMessage("From Date Not In Proper Format");

            return;
        }

        try
        {
            objSys.getDateForInput(txtToDate.Text);
        }
        catch (Exception)
        {

            DisplayMessage("To Date Not In Proper Format");

            return;
        }

        if (objSys.getDateForInput(txtToDate.Text) < objSys.getDateForInput(txtFromDate.Text))
        {
            DisplayMessage("From Date Cannot Be Greater Than To Date");
            return;

        }

        DataTable dt = new DataTable();

        dt = objAttLog.GetAttendanceLogByDate(Session["CompId"].ToString(), objSys.getDateForInput(txtFromDate.Text).ToString(), objSys.getDateForInput(txtToDate.Text).ToString());
        dt = new DataView(dt, "Verified_Type='By Manual'", "", DataViewRowState.CurrentRows).ToTable();

        if (dt.Rows.Count > 0)
        {
            gvEmpLog.DataSource = dt;
            gvEmpLog.DataBind();

            Session["dtEmpLog"] = dt;
            lblTotalRecord1.Text = Resources.Attendance.Total_Records + " : " + dt.Rows.Count.ToString() + "";


        }
        else
        {
            gvEmpLog.DataSource = null;
            gvEmpLog.DataBind();

            Session["dtEmpLog"] = null;
        }

         }
    public void FillGrid()
    {
        DataTable dt = objAttLog.GetAttendanceLog(Session["CompId"].ToString());
        dt = new DataView(dt, "Verified_Type='By Manual'", "", DataViewRowState.CurrentRows).ToTable();
        if (dt.Rows.Count > 0)
        {
            gvEmpLog.DataSource = dt;
            gvEmpLog.DataBind();

            Session["dtEmpLog"] = dt;
            lblTotalRecord1.Text = Resources.Attendance.Total_Records + " : " + dt.Rows.Count.ToString() + "";

        }
        else
        {
            gvEmpLog.DataSource = null;
            gvEmpLog.DataBind();
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
            if(EmpIds!="")
            {
            dtEmp = new DataView(dtEmp, "Emp_Id in(" + EmpIds.Substring(0, EmpIds.Length - 1) + ")", "", DataViewRowState.CurrentRows).ToTable();
            }
            else
            {
                dtEmp=new DataTable ();
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

    protected void gvEmployeeSal_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        gvEmployeeSal.PageIndex = e.NewPageIndex;
        gvEmployeeSal.DataSource = (DataTable)Session["dtEmp4"];
        gvEmployeeSal.DataBind();
    }
    protected void gvEmpLog_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {

        gvEmpLog.PageIndex = e.NewPageIndex;
        DataTable dtEmp = (DataTable)Session["dtEmpLog"];
        gvEmpLog.DataSource = dtEmp;
        gvEmpLog.DataBind();
        string temp = string.Empty;


        for (int i = 0; i < gvEmpLog.Rows.Count; i++)
        {
            Label lblconid = (Label)gvEmpLog.Rows[i].FindControl("lblTransId");
            string[] split = lblSelectRecord1.Text.Split(',');

            for (int j = 0; j < lblSelectRecord1.Text.Split(',').Length; j++)
            {
                if (lblSelectRecord1.Text.Split(',')[j] != "")
                {
                    if (lblconid.Text.Trim().ToString() == lblSelectRecord1.Text.Split(',')[j].Trim().ToString())
                    {
                        ((CheckBox)gvEmpLog.Rows[i].FindControl("chkgvSelect")).Checked = true;
                    }
                }
            }
        }
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



    protected void chkgvSelect_CheckedChanged1(object sender, EventArgs e)
    {
        string empidlist = string.Empty;
        string temp = string.Empty;
        int index = ((GridViewRow)((CheckBox)sender).Parent.Parent).RowIndex;
        Label lb = (Label)gvEmpLog.Rows[index].FindControl("lblTransId");
        if (((CheckBox)gvEmpLog.Rows[index].FindControl("chkgvSelect")).Checked)
        {
            empidlist += lb.Text.Trim().ToString() + ",";
            lblSelectRecord1.Text += empidlist;

        }

        else
        {

            empidlist += lb.Text.ToString().Trim();
            lblSelectRecord1.Text += empidlist;
            string[] split = lblSelectRecord1.Text.Split(',');
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
            lblSelectRecord1.Text = temp;
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



    protected void ImgbtnDeleteAll_Click1(object sender, ImageClickEventArgs e)
    {
        if(gvEmpLog.Rows.Count==0)
        {

            DisplayMessage("No Data Exists");
            return;
        }

        if(lblSelectRecord1.Text=="")
        {
            DisplayMessage("Please select record");
            return;

        }

        string[] split = lblSelectRecord1.Text.Split(',');
        foreach (string item in split)
        {
            if (item != "")
            {
                objAttLog.DeleteAttendanceLog(item);

            }
        }

        FillGrid();
        lblSelectRecord1.Text = "";
        DisplayMessage("Record Deleted");
       

 }

    protected void ImgbtnSelectAll_Click1(object sender, ImageClickEventArgs e)
    {
        DataTable dtProduct = (DataTable)Session["dtEmpLog"];

        if (ViewState["Select"] == null)
        {
            ViewState["Select"] = 1;
            foreach (DataRow dr in dtProduct.Rows)
            {
                if (!lblSelectRecord1.Text.Split(',').Contains(dr["Trans_Id"]))
                {
                    lblSelectRecord1.Text += dr["Trans_Id"] + ",";
                }
            }
            for (int i = 0; i < gvEmpLog.Rows.Count; i++)
            {
                string[] split = lblSelectRecord1.Text.Split(',');
                Label lblconid = (Label)gvEmpLog.Rows[i].FindControl("lblTransId");
                for (int j = 0; j < lblSelectRecord1.Text.Split(',').Length; j++)
                {
                    if (lblSelectRecord1.Text.Split(',')[j] != "")
                    {
                        if (lblconid.Text.Trim().ToString() == lblSelectRecord1.Text.Split(',')[j].Trim().ToString())
                        {
                            ((CheckBox)gvEmpLog.Rows[i].FindControl("chkgvSelect")).Checked = true;
                        }
                    }
                }
            }
        }
        else
        {
            lblSelectRecord1.Text = "";
            DataTable dtProduct1 = (DataTable)Session["dtEmpLog"];
            gvEmpLog.DataSource = dtProduct1;
            gvEmpLog.DataBind();
            ViewState["Select"] = null;
        }

    }
    protected void btnList_Click(object sender, EventArgs e)
    {
        
        pnlMenuList.BackColor = System.Drawing.ColorTranslator.FromHtml("#ccddee");

        pnlMenuNew.BackColor = System.Drawing.ColorTranslator.FromHtml("#90bde9");
        pnlAttList.Visible = true;
        pnlEmpAtt.Visible = false;
        btnReset_Click(null,null);
        FillGrid();
        lblSelectRecord1.Text = "";
        lblTotalRecord.Text = "";
        txtToDate.Text = "";
        txtFromDate.Text = "";
    }
    protected void btnNew_Click(object sender, EventArgs e)
    {
       
        pnlMenuNew.BackColor = System.Drawing.ColorTranslator.FromHtml("#ccddee");

        pnlMenuList.BackColor = System.Drawing.ColorTranslator.FromHtml("#90bde9");
        pnlAttList.Visible = false;
        pnlEmpAtt.Visible = true;
        rbtnByManual.Checked = true;
        rbtnByTour.Checked = false;
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

        lblSelectRecord1.Text = "";
        txtToDate.Text="";
            txtFromDate.Text="";
        
        rbtnEmpSal.Checked = true;
        rbtnGroupSal.Checked = false;
        EmpGroupSal_CheckedChanged(null, null);
    }

    protected void btnSave_Click(object sender, EventArgs e)
    {
        int b = 0;
        string Verifiedtype = string.Empty;

        if(rbtnByManual.Checked)
        {
            Verifiedtype = "By Manual";
        }
        else
        {
            Verifiedtype = "By Tour";
        }


        string empidlist = lblSelectRecord.Text;
        if (txtDate.Text == "" || txtOnDuty.Text == "")
        {

            DisplayMessage("Fill Date And Time");

            return;
        }
        try
        {
            objSys.getDateForInput(txtDate.Text);
        }
        catch (Exception)
        {

            DisplayMessage("Date Not In Proper Format");

            return;
        }
        DateTime EventTime = new DateTime();
        EventTime = new DateTime(objSys.getDateForInput(txtDate.Text).Year, objSys.getDateForInput(txtDate.Text).Month, objSys.getDateForInput(txtDate.Text).Day, Convert.ToDateTime(txtOnDuty.Text).Hour, Convert.ToDateTime(txtOnDuty.Text).Minute, Convert.ToDateTime(txtOnDuty.Text).Second);

        if (rbtnEmpSal.Checked)
        {



            if (empidlist == "")
            {

                DisplayMessage("Select Atleast One Employee");


                return;
            }
           
            for (int i = 0; i < empidlist.Split(',').Length; i++)
            {
                if (empidlist.Split(',')[i] == "")
                {
                    continue;
                }
                b = objAttLog.InsertAttendanceLog(Session["CompId"].ToString(), empidlist.Split(',')[i], "", objSys.getDateForInput(txtDate.Text).ToString(), EventTime.ToString(), ddlFunction.Text, ddlType.SelectedValue, Verifiedtype, true.ToString(), Session["UserId"].ToString(), DateTime.Now.ToString(), Session["UserId"].ToString(), DateTime.Now.ToString());


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

                        b = objAttLog.InsertAttendanceLog(Session["CompId"].ToString(), str, "", objSys.getDateForInput(txtDate.Text).ToString(), EventTime.ToString(), ddlFunction.Text, ddlType.SelectedValue, Verifiedtype, true.ToString(), Session["UserId"].ToString(), DateTime.Now.ToString(), Session["UserId"].ToString(), DateTime.Now.ToString());


                    }
                }

            }
            else
            {
                DisplayMessage("Select Group First");
            }
        }

      
        if(b!=0)
        {
        DisplayMessage("Record Saved");
        }




    }

    public void DisplayMessage(string str)
    {
        ScriptManager.RegisterStartupScript(this, GetType(), "", "alert('" + str + "');", true);
    }
    protected void btnReset_Click(object sender, EventArgs e)
    {
        rbtnByManual.Checked = true;
        rbtnByTour.Checked = false;
        txtDate.Text = "";
        txtOnDuty.Text = "";
        ddlFunction.SelectedIndex = 0;
        ddlType.SelectedIndex = 0;
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
      
    }
    protected void btnarybind_Click(object sender, ImageClickEventArgs e)
    {
        if (ddlOption1.SelectedIndex != 0)
        {
            string condition1 = string.Empty;
            if (ddlOption1.SelectedIndex == 1)
            {
                condition1 = "convert(" + ddlField1.SelectedValue + ",System.String)='" + txtVal1.Text.Trim() + "'";
            }
            else if (ddlOption.SelectedIndex == 2)
            {
                condition1 = "convert(" + ddlField1.SelectedValue + ",System.String) like '%" + txtVal1.Text.Trim() + "%'";
            }
            else
            {
                condition1 = "convert(" + ddlField1.SelectedValue + ",System.String) Like '" + txtVal1.Text.Trim() + "%'";

            }
            DataTable dtEmp = (DataTable)Session["dtEmpLog"];
            DataView view = new DataView(dtEmp, condition1, "", DataViewRowState.CurrentRows);
            Session["dtEmpLog"]=view.ToTable();
            gvEmpLog.DataSource = view.ToTable();
            gvEmpLog.DataBind();
            lblTotalRecord1.Text = Resources.Attendance.Total_Records + " : " + (view.ToTable()).Rows.Count.ToString() + "";

        }

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

    }

    protected void btnaryRefresh_Click(object sender, ImageClickEventArgs e)
    {
        lblSelectRecord.Text = "";
        ddlField1.SelectedIndex = 1;
        ddlOption1.SelectedIndex = 2;
        txtVal1.Text = "";
        btnResetLog_Click(null,null);
        DataTable dtEmp = (DataTable)Session["dtEmpLog"];
        gvEmpLog.DataSource = dtEmp;
        gvEmpLog.DataBind();

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

         protected void chkgvSelectAll_CheckedChanged1(object sender, EventArgs e)
    {
        CheckBox chkSelAll = ((CheckBox)gvEmpLog.HeaderRow.FindControl("chkgvSelectAll"));
        for (int i = 0; i < gvEmpLog.Rows.Count; i++)
        {
            ((CheckBox)gvEmpLog.Rows[i].FindControl("chkgvSelect")).Checked = chkSelAll.Checked;
            if (chkSelAll.Checked)
            {
                if (!lblSelectRecord1.Text.Split(',').Contains(((Label)(gvEmpLog.Rows[i].FindControl("lblTransId"))).Text.Trim().ToString()))
                {
                    lblSelectRecord1.Text += ((Label)(gvEmpLog.Rows[i].FindControl("lblTransId"))).Text.Trim().ToString() + ",";
                }
            }
            else
            {
                string temp = string.Empty;
                string[] split = lblSelectRecord1.Text.Split(',');
                foreach (string item in split)
                {
                    if (item != ((Label)(gvEmpLog.Rows[i].FindControl("lblTransId"))).Text.Trim().ToString())
                    {
                        if (item != "")
                        {
                            temp += item + ",";
                        }
                    }
                }
                lblSelectRecord1.Text = temp;
            }
        }
    }
  
}
