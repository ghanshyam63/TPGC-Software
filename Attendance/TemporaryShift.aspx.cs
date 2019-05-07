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

public partial class Attendance_TemporaryShift : BasePage
{
    EmployeeMaster objEmp = new EmployeeMaster();
    Att_ShiftManagement objShift = new Att_ShiftManagement();
    Set_EmployeeGroup_Master objEmpGroup = new Set_EmployeeGroup_Master();

    Set_Group_Employee objGroupEmp = new Set_Group_Employee();
    SystemParameter objSys = new SystemParameter();
    Att_ShiftDescription objShiftdesc = new Att_ShiftDescription();

    Att_ScheduleMaster objEmpSch = new Att_ScheduleMaster();
    Att_TimeTable objTimeTable=new Att_TimeTable ();
    Att_TimeTable objTimetable = new Att_TimeTable();
    protected void Page_Load(object sender, EventArgs e)
    {
        if (Session["UserId"] == null)
        {
            Response.Redirect("~/ERPLogin.aspx");
        }
        Session["AccordianId"] = "9";
        Session["HeaderText"] = "Attendance Setup";
        Page.Title = objSys.GetSysTitle();
        if (!IsPostBack)
        {
            Session["EmpFiltered"] = null;
            FillDataListGrid();
            PnlNewEdit.Visible = false;
            tbl.Visible = false;
            FillGvEmp();
            FillShift();
            Pnl1.Visible = false;
            pnlEmpSel.Visible = false;

            rbtnEmp.Checked = true;
            rbtnGroup.Checked = false;
            EmpGroup_CheckedChanged(null, null);
        }
        txtFrom_CalendarExtender.Format = objSys.SetDateFormat();
        txtFromDate_CalendarExtender.Format = objSys.SetDateFormat();
        txtTo_CalendarExtender.Format = objSys.SetDateFormat();
        txtToDate_CalendarExtender.Format = objSys.SetDateFormat();
    }
    protected void EmpGroup_CheckedChanged(object sender, EventArgs e)
    {
        if (rbtnGroup.Checked)
        {
            pnlShifttoemp.Visible = false;
            pnlEmpGroup.Visible = true;

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
            pnlShifttoemp.Visible = true;
            pnlEmpGroup.Visible = false;







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

    protected void gvEmp1_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        gvEmployee.PageIndex = e.NewPageIndex;
        gvEmployee.DataSource = (DataTable)Session["dtEmp1"];
        gvEmployee.DataBind();
    }
    public void FillShift()
    {

        DataTable dtShift = objShift.GetShiftMaster(Session["CompId"].ToString());
        ddlShift.DataSource = dtShift;
        ddlShift.DataTextField = "Shift_Name";
        ddlShift.DataValueField = "Shift_Id";
        ddlShift.DataBind();
        ListItem lst = new ListItem("--Select--", "0");
        ddlShift.Items.Insert(0, lst);
    }


    protected void btnCancel1_Click(object sender, EventArgs e)
    {

        btnNew_Click(null, null);
}



    protected void btnCancel2_Click(object sender, EventArgs e)
    {
        pnlMenuList.BackColor = System.Drawing.ColorTranslator.FromHtml("#ccddee");

        pnlMenuNew.BackColor = System.Drawing.ColorTranslator.FromHtml("#90bde9");

        PnlList.Visible = true;
        PnlNewEdit.Visible = false;
        txtFrom.Text = "";
        txtTo.Text = "";
        Session["EmpFiltered"] = null;
        FillGvEmp();
        pnlEmpSel.Visible = true;
        trGroup.Visible = true;

    }
    protected void btnList_Click(object sender, EventArgs e)
    {

        pnlMenuList.BackColor = System.Drawing.ColorTranslator.FromHtml("#ccddee");

        pnlMenuNew.BackColor = System.Drawing.ColorTranslator.FromHtml("#90bde9");

        PnlList.Visible = true;
        PnlNewEdit.Visible = false;

    }
    protected void gvEmp_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        gvEmp.PageIndex = e.NewPageIndex;

        FillDataListGrid();
    }

    protected void btnRefresh_Click(object sender, ImageClickEventArgs e)
    {



        FillDataListGrid();
        ddlFieldName.SelectedIndex = 1;
        ddlOption.SelectedIndex = 3;
        txtValue.Text = "";



        System.Web.UI.ScriptManager.GetCurrent(this).SetFocus(txtValue);
    }
    protected void chkTimeTableList_SelectedIndexChanged(object sender, EventArgs e)
    {
        bool isoverlap = false;



        CheckBoxList list = (CheckBoxList)sender;
        string[] control = Request.Form.Get("__EVENTTARGET").Split('$');
        int idx = control.Length - 1;
        string timetableid = string.Empty;
        try
        {
            timetableid = list.Items[Int32.Parse(control[idx])].Value;
        }
        catch (Exception ex)
        {
            return;
        }

        if (list.Items[Int32.Parse(control[idx])].Selected)
        {
            DataTable dtin = objTimeTable.GetTimeTableMasterById(Session["CompId"].ToString(), timetableid);

            DateTime dtintime = Convert.ToDateTime(dtin.Rows[0]["OnDuty_Time"]);
            DateTime dtouttime = Convert.ToDateTime(dtin.Rows[0]["OffDuty_Time"]);
            DateTime OnDutyTime = Convert.ToDateTime(dtin.Rows[0]["OnDuty_Time"]);
            DateTime OffDutyTime = Convert.ToDateTime(dtin.Rows[0]["OffDuty_Time"]);
            if (dtintime > dtouttime)
            {
                dtouttime = dtouttime.AddHours(24);
            }

            if (OnDutyTime > OffDutyTime)
            {
                OffDutyTime = OffDutyTime.AddHours(24);
            }

            for (int i = 0; i < chkTimeTableList.Items.Count; i++)
            {
                if (chkTimeTableList.Items[i].Selected && chkTimeTableList.Items[i].Value != timetableid)
                {
                    DataTable dtin1 = objTimeTable.GetTimeTableMasterById(Session["CompId"].ToString(), chkTimeTableList.Items[i].Value);
                    DateTime dtintime1 = Convert.ToDateTime(dtin1.Rows[0]["OnDuty_Time"]);
                    DateTime dtouttime1 = Convert.ToDateTime(dtin1.Rows[0]["OffDuty_Time"]);

                    DateTime OnDutyTime1 = Convert.ToDateTime(dtin1.Rows[0]["OnDuty_Time"]);
                    DateTime OffDutyTime1 = Convert.ToDateTime(dtin1.Rows[0]["OffDuty_Time"]);


                    if (dtintime1 > dtouttime1)
                    {
                        dtouttime1 = dtouttime1.AddHours(24);
                    }

                    if (OnDutyTime1 > OffDutyTime1)
                    {
                        OffDutyTime1 = OffDutyTime1.AddHours(24);
                    }

                    if (dtintime >= dtintime1 && dtintime <= dtouttime1)
                    {
                        isoverlap = true;
                        break;
                    }
                    if (dtouttime >= dtintime1 && dtouttime <= dtouttime1)
                    {
                        isoverlap = true;
                        break;
                    }

                    if (dtintime1 >= dtintime && dtintime1 <= dtouttime)
                    {
                        isoverlap = true;
                        break;
                    }

                    if (dtouttime1 >= dtintime && dtouttime1 <= dtouttime)
                    {
                        isoverlap = true;
                        break;
                    }
                }
            }
        }
        if (isoverlap)
        {
            list.Items[Int32.Parse(control[idx])].Selected = false;

            DisplayMessage("Time Overlaped");



        }


    }
    protected void chkUnselect_CheckedChanged(object sender, EventArgs e)
    {// GridViewRow gv=new GridViewRow();

        string empid = GvEmpListSelected.DataKeys[((GridViewRow)((CheckBox)sender).Parent.Parent).RowIndex][0].ToString();
        DataTable dtEmpFilter = (DataTable)Session["EmpFiltered"];
        dtEmpFilter = new DataView(dtEmpFilter, "Emp_Code not ='" + empid + "'", "", DataViewRowState.CurrentRows).ToTable();
        GvEmpListSelected.DataSource = dtEmpFilter;
        GvEmpListSelected.DataBind();
        Session["EmpFiltered"] = dtEmpFilter;
        ((CheckBox)GvEmpList.HeaderRow.FindControl("chkSelAll")).Checked = false;
        for (int i = 0; i < GvEmpList.Rows.Count; i++)
        {
            if (GvEmpList.DataKeys[i][0].ToString() == empid)
            {
                ((CheckBox)GvEmpList.Rows[i].FindControl("chkSelect")).Checked = false;
            }



        }
    }
    protected void btnShifttoEmpGroup_Click(object sender, EventArgs e)
    {
    }
    protected void chkUnselectAll_CheckedChanged(object sender, EventArgs e)
    {
        if (((CheckBox)sender).Checked)
        {
            Session["EmpFiltered"] = null;
            GvEmpListSelected.DataSource = null;
            GvEmpListSelected.DataBind();
        }
    }
    protected void btnNext1_Click(object sender, EventArgs e)
    {
      
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

            if(GroupIds=="")
            {
                DisplayMessage("Select Group First");
                return;

            }






            pnlEmpGroup.Visible = false;
            if (Session["dtEmp1"] != null)
            {
                DataTable dt = (DataTable)Session["dtEmp1"];
                Session["EmpFiltered"] = dt;
                GvEmpListSelected.DataSource = dt;
                GvEmpListSelected.DataBind();
            }
        }
        else
        {
            if (GvEmpListSelected.Rows.Count == 0)
            {

                DisplayMessage("Please select at least one employee");
                
                return;
            }
        }
        PnlTimeTableList.Visible = false;
        pnlAddDays.Visible = true;
        Pnl1.Visible = true;
        pnlEmpSel.Visible = true;
        pnlemployee.Visible = false;

        btnNext.Visible = false;
        pnlShifttoemp.Visible = true;
        trGroup.Visible = false;
        btnCancel2.Visible = false;
    }
    protected void btnCancel_Click1(object sender, EventArgs e)
    {

        pnlShifttoemp.Visible = true;
        pnlEmpGroup.Visible = false;

    }
    protected void btnRefresh3Report_Click(object sender, ImageClickEventArgs e)
    {
        DataTable dtEmp = (DataTable)Session["EmpDt"];
        GvEmpListSelected.DataSource = dtEmp;
        GvEmpListSelected.DataBind();
        Session["EmpFiltered"] = dtEmp;
        ddlSelectOption0.SelectedIndex = 2;
        ddlSelectField0.SelectedIndex = 0;
        txtval0.Text = "";
    }
    public void FillGvEmpSelected()
    {
        DataTable dtEmpMain = (DataTable)Session["EmpDt"];
        DataTable dtEmp = dtEmpMain.Clone();

        if (Session["EmpFiltered"] != null)
        {
            dtEmp = (DataTable)Session["EmpFiltered"];
        }
        //if (Session["EmpFiltered"] != null)
        //{
        //    dtEmp = (DataTable)Session["EmpFiltered"];
        //}
        //else
        //{

        // }

        for (int i = 0; i < GvEmpList.Rows.Count; i++)
        {
            if (((CheckBox)GvEmpList.Rows[i].FindControl("chkSelect")).Checked)
            {
                if (new DataView(dtEmp, "Emp_Code='" + GvEmpList.DataKeys[i][0].ToString() + "'", "", DataViewRowState.CurrentRows).ToTable().Rows.Count == 0)
                {

                    dtEmp.Merge(new DataView(dtEmpMain, "Emp_Code='" + GvEmpList.DataKeys[i][0].ToString() + "'", "", DataViewRowState.CurrentRows).ToTable());
                }
            }
        }
        GvEmpListSelected.DataSource = dtEmp;
        GvEmpListSelected.DataBind();
        Session["EmpFiltered"] = dtEmp;
    }
    
    protected void btnEmp0_Click(object sender, ImageClickEventArgs e)
    {
        if (ddlSelectOption0.SelectedIndex != 0)
        {
            string condition = string.Empty;
            if (ddlSelectOption0.SelectedIndex == 1)
            {
                condition = "convert(" + ddlSelectField0.SelectedValue + ",System.String)='" + txtval0.Text + "'";
            }
            else if (ddlSelectOption.SelectedIndex == 3)
            {
                condition = "convert(" + ddlSelectField0.SelectedValue + ",System.String) Like '" + txtval0.Text + "%'";
            }
            else
            {
                condition = "convert(" + ddlSelectField0.SelectedValue + ",System.String) like '%" + txtval0.Text + "%'";
            }
            DataTable dtEmp = (DataTable)Session["EmpFiltered"];
            if (txtval0.Text != "")
            {
                DataView view = new DataView(dtEmp, condition, "", DataViewRowState.CurrentRows);

                GvEmpListSelected.DataSource = view.ToTable();
                GvEmpListSelected.DataBind();
                Session["dtFilter"] = view.ToTable();
            }
        }
    }
    protected void chkSelect_CheckedChanged(object sender, EventArgs e)
    {
        FillGvEmpSelected();
    }
    protected void btnRefresh2Report_Click(object sender, ImageClickEventArgs e)
    {
        FillGvEmp();
        DataTable dt = (DataTable)Session["EmpFiltered"];
        if (Session["EmpFiltered"] != null)
        {
            if (dt.Rows.Count > 0)
            {
                GvEmpListSelected.DataSource = dt;
                GvEmpListSelected.DataBind();
            }
        }
        ddlSelectOption.SelectedIndex = 2;
        ddlSelectField.SelectedIndex = 0;
        txtval.Text = "";
    }
    private void FillGvEmp()
    {
        DataTable dtEmp = objEmp.GetEmployeeMaster(Session["CompId"].ToString());

        dtEmp = new DataView(dtEmp, "Brand_Id='" + Session["BrandId"].ToString() + "'  and Location_Id='" + Session["LocId"].ToString() + "'", "", DataViewRowState.CurrentRows).ToTable();

        if (Session["SessionDepId"] != null)
        {
            dtEmp = new DataView(dtEmp, "Department_Id in(" + Session["SessionDepId"].ToString().Substring(0, Session["SessionDepId"].ToString().Length - 1) + ")", "", DataViewRowState.CurrentRows).ToTable();

        }

        Session["EmpDt"] = dtEmp;
        GvEmpList.DataSource = dtEmp;
        GvEmpList.DataBind();
        GvEmpListSelected.DataSource = null;
        GvEmpListSelected.DataBind();
    }
    protected void btnEmp_Click(object sender, ImageClickEventArgs e)
    {
        if (ddlSelectOption.SelectedIndex != 0)
        {
            string condition = string.Empty;
            if (ddlSelectOption.SelectedIndex == 1)
            {
                condition = "convert(" + ddlSelectField.SelectedValue + ",System.String)='" + txtval.Text + "'";
            }
            else if (ddlSelectOption.SelectedIndex == 3)
            {
                condition = "convert(" + ddlSelectField.SelectedValue + ",System.String) Like '" + txtval.Text + "%'";
            }
            else
            {
                condition = "convert(" + ddlSelectField.SelectedValue + ",System.String) like '%" + txtval.Text + "%'";
            }
            DataTable dtEmp = (DataTable)Session["EmpDt"];
            DataView view = new DataView(dtEmp, condition, "", DataViewRowState.CurrentRows);

            GvEmpList.DataSource = view.ToTable();
            GvEmpList.DataBind();
            // Session["EmpDt"] = view.ToTable();
        }
    }
    protected void chkSelAll_CheckedChanged(object sender, EventArgs e)
    {
        CheckBox chk = (CheckBox)sender;
        for (int i = 0; i < GvEmpList.Rows.Count; i++)
        {
            ((CheckBox)GvEmpList.Rows[i].FindControl("chkSelect")).Checked = chk.Checked;
        }

        FillGvEmpSelected();

    }
    protected void BtnDelete_Click(object sender, EventArgs e)
    {
         try{
         if (txtFrom.Text == "")
            {
                DisplayMessage("From Date Required");
                txtFrom.Focus();
                return;
            }
            if (txtTo.Text == "")
            {
                DisplayMessage("To Date Required");
                txtTo.Focus();
                return;
            }

            if (objSys.getDateForInput(txtFrom.Text.ToString()) > objSys.getDateForInput(txtTo.Text.ToString()))
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

         if (rbtnEmp.Checked)
         {
             if (GvEmpListSelected.Rows.Count == 0)
             {

                 DisplayMessage("Please select at least one employee");



                 return;
             }

             for (int i = 0; i < GvEmpListSelected.Rows.Count;i++)
             {
                 objEmpSch.DeleteScheduleDescByEmpIdandDate(GetEmpId(GvEmpListSelected.DataKeys[i][0].ToString()), objSys.getDateForInput(txtFrom.Text).ToString(), objSys.getDateForInput(txtTo.Text).ToString());

             }

         }
         else
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
                     if (str != "")
                     {
                         objEmpSch.DeleteScheduleDescByEmpIdandDate(str, objSys.getDateForInput(txtFrom.Text).ToString(), objSys.getDateForInput(txtTo.Text).ToString());

                     }
                 }



             }
             else
             {
                 DisplayMessage("Select Group First");
                 return;
             }

         }
         DisplayMessage("Record Deleted"); 
    }
    protected void btnIsTemprary_Click(object sender, EventArgs e)
    {

        if (GvEmpListSelected.Rows.Count == 0)
        {

            DisplayMessage("Please select at least one employee");



            return;
        }
        try
        {
            if (txtFrom.Text == "")
            {
                DisplayMessage("From Date Required");
                txtFrom.Focus();
                return;
            }
            if (txtTo.Text == "")
            {
                DisplayMessage("To Date Required");
                txtTo.Focus();
                return;
            }

            if (objSys.getDateForInput(txtFrom.Text.ToString()) >objSys.getDateForInput(txtTo.Text.ToString()))
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
        Session["IsTemp"] = true;

        tbl.Visible = true;
        updt1.Visible = false;
        Pnl1.Visible = false;
       
        bindchecklist();
        trGroup.Visible = false;

        pnlAddDays.Visible = true;
        PnlTimeTableList.Visible = true;

    }
    protected void btnCancelPanel_Click1(object sender, EventArgs e)
    {
        Session["IsTemp"] = null;
        pnlMain.Visible = true;
        updt1.Visible =true;
        tbl.Visible = false;
        pnlAddDays.Visible = false;
        PnlTimeTableList.Visible = false;

        Pnl1.Visible = true;
        trGroup.Visible = false;
    }
    protected void btnsaveTempShift_Click(object sender, EventArgs e)
    {
        int flag = 0;
        int flagc = 0;
        int b = 0;
        for (int i = 0; i < chkTimeTableList.Items.Count; i++)
        {

            if (chkTimeTableList.Items[i].Selected == true)
            {
                flag = 1;

            }

        }
        if (flag == 0)
        {
            DisplayMessage("Please select timetable");
            return;

        }
        for (int j = 0; j < chkDayUnderPeriod.Items.Count; j++)
        {

            if (chkDayUnderPeriod.Items[j].Selected == true)
            {
                flagc = 1;

            }

        }

        if (flagc == 0)
        {
            DisplayMessage("Please select day");
            return;

        }
        string[] weekdays = new string[8];
        weekdays[1] = "Sunday";
        weekdays[2] = "Monday";
        weekdays[3] = "Tuesday";
        weekdays[4] = "Wednesday";
        weekdays[5] = "Thursday";
        weekdays[6] = "Friday";
        weekdays[7] = "Saturday";



        if (rbtnEmp.Checked)
        {
            for (int i = 0; i < GvEmpListSelected.Rows.Count; i++)
            {

                DataTable dtSch = objEmpSch.GetSheduleMaster();

                dtSch = new DataView(dtSch, "Shift_Type='Temp Shift'  and Shift_Id='" + ddlShift.SelectedValue + "' and Emp_Id='" + GetEmpId(GvEmpListSelected.DataKeys[i][0].ToString()) + "'", "", DataViewRowState.CurrentRows).ToTable();

                if (dtSch.Rows.Count == 0)
                {
                    b = objEmpSch.InsertScheduleMaster(Session["CompId"].ToString(), Session["BrandId"].ToString(), Session["LocId"].ToString(), GetEmpId(GvEmpListSelected.DataKeys[i][0].ToString()), ddlShift.SelectedValue, "Temp Shift", objSys.getDateForInput(txtFrom.Text).ToString(), objSys.getDateForInput(txtTo.Text).ToString(), "", "", "", "", "", "", true.ToString(), DateTime.Now.ToString(), true.ToString(), Session["UserId"].ToString(), DateTime.Now.ToString(), Session["UserId"].ToString(), DateTime.Now.ToString());
                }
                else
                {
                    b = objEmpSch.UpdateScheduleMaster(dtSch.Rows[0]["Schedule_Id"].ToString(), Session["CompId"].ToString(), Session["BrandId"].ToString(), Session["LocId"].ToString(), GetEmpId(GvEmpListSelected.DataKeys[i][0].ToString()), ddlShift.SelectedValue, "Temp Shift", objSys.getDateForInput(txtFrom.Text).ToString(), objSys.getDateForInput(txtTo.Text).ToString(), "", "", "", "", "", "", true.ToString(), DateTime.Now.ToString(), true.ToString(), Session["UserId"].ToString(), DateTime.Now.ToString());


                }
                DataTable dtTime = new DataTable();
                for (int dayno = 0; dayno < chkDayUnderPeriod.Items.Count; dayno++)
                {
                    dtTime = objEmpSch.GetSheduleDescriptionByEmpId(GetEmpId(GvEmpListSelected.DataKeys[i][0].ToString()), Convert.ToDateTime(chkDayUnderPeriod.Items[dayno].Value).ToString());

                    if (chkDayUnderPeriod.Items[dayno].Selected)
                    {
                        if (dtTime.Rows.Count > 0)
                        {
                            for (int j = 0; j < chkTimeTableList.Items.Count; j++)
                            {

                                if (chkTimeTableList.Items[j].Selected)
                                {
                                    
                                    if (dtTime.Rows[0]["Is_Off"].ToString() == "True")
                                    {
                                        objEmpSch.DeleteScheduleDescByEmpIdandDate(GetEmpId(GvEmpListSelected.DataKeys[i][0].ToString()), Convert.ToDateTime(chkDayUnderPeriod.Items[dayno].Value).ToString(), Convert.ToDateTime(chkDayUnderPeriod.Items[dayno].Value).ToString());


                                        objEmpSch.InsertScheduleDescription(b.ToString(), chkTimeTableList.Items[j].Value, Convert.ToDateTime(chkDayUnderPeriod.Items[dayno].Value).ToString(), false.ToString(),true.ToString(), false.ToString(), GetEmpId(GvEmpListSelected.DataKeys[i][0].ToString()), ddlShift.SelectedValue, "", "", "", "", "", true.ToString(), DateTime.Now.ToString(), true.ToString(), Session["UserId"].ToString(), DateTime.Now.ToString(), Session["UserId"].ToString(), DateTime.Now.ToString());
                                            
                                       


                                    }
                                    else
                                    {


                                        DateTime OnDutyTime = Convert.ToDateTime(GetDutyTime("On", chkTimeTableList.Items[j].Value));
                                        DateTime OffDutyTime = Convert.ToDateTime(GetDutyTime("Off", chkTimeTableList.Items[j].Value));
                                        int flag1 = 0;
                                            for (int s = 0; s < dtTime.Rows.Count; s++)
                                            {

                                                if (ISOverLapTimeTable(Convert.ToDateTime(dtTime.Rows[s]["OnDuty_Time"].ToString()), Convert.ToDateTime(dtTime.Rows[s]["OffDuty_Time"].ToString()), OnDutyTime, OffDutyTime))
                                                {
                                                    flag1 = 1;
                                                }

                                            }


                                            if (flag1 == 0)
                                            {
                                                objEmpSch.InsertScheduleDescription(b.ToString(), chkTimeTableList.Items[j].Value, Convert.ToDateTime(chkDayUnderPeriod.Items[dayno].Value).ToString(), false.ToString(),true.ToString(), false.ToString(), GetEmpId(GvEmpListSelected.DataKeys[i][0].ToString()), ddlShift.SelectedValue, "", "", "", "", "", true.ToString(), DateTime.Now.ToString(), true.ToString(), Session["UserId"].ToString(), DateTime.Now.ToString(), Session["UserId"].ToString(), DateTime.Now.ToString());


                                            }
                                        

                                    }


                                }
                            }
                        }
                        else
                        {
                            for (int j = 0; j < chkTimeTableList.Items.Count; j++)
                            {
                                if (chkTimeTableList.Items[j].Selected)
                                {

                                    objEmpSch.InsertScheduleDescription(b.ToString(), chkTimeTableList.Items[j].Value, chkDayUnderPeriod.Items[dayno].Value, false.ToString(), true.ToString(), false.ToString(), GetEmpId(GvEmpListSelected.DataKeys[i][0].ToString()), "0", "", "", "", "", "", true.ToString(), DateTime.Now.ToString(), true.ToString(), Session["UserId"].ToString(), DateTime.Now.ToString(), Session["UserId"].ToString(), DateTime.Now.ToString());
                                }


                            }
                        }

                    }



                }

            }
        }

        else
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
                return;
            }

            foreach (string str in EmpIds.Split(','))
            {

                if (str != "")
                {

                    DataTable dtSch = objEmpSch.GetSheduleMaster();

                    dtSch = new DataView(dtSch, "Shift_Type='Temp Shift'  and Shift_Id='" + ddlShift.SelectedValue + "' and Emp_Id='" + str + "'", "", DataViewRowState.CurrentRows).ToTable();

                    if (dtSch.Rows.Count == 0)
                    {
                        b = objEmpSch.InsertScheduleMaster(Session["CompId"].ToString(), Session["BrandId"].ToString(), Session["LocId"].ToString(), str, ddlShift.SelectedValue, "Temp Shift", objSys.getDateForInput(txtFrom.Text).ToString(), objSys.getDateForInput(txtTo.Text).ToString(), "", "", "", "", "", "", true.ToString(), DateTime.Now.ToString(), true.ToString(), Session["UserId"].ToString(), DateTime.Now.ToString(), Session["UserId"].ToString(), DateTime.Now.ToString());
                    }
                    else
                    {
                        b = objEmpSch.UpdateScheduleMaster(dtSch.Rows[0]["Schedule_Id"].ToString(), Session["CompId"].ToString(), Session["BrandId"].ToString(), Session["LocId"].ToString(), str, ddlShift.SelectedValue, "Temp Shift", objSys.getDateForInput(txtFrom.Text).ToString(), objSys.getDateForInput(txtTo.Text).ToString(), "", "", "", "", "", "", true.ToString(), DateTime.Now.ToString(), true.ToString(), Session["UserId"].ToString(), DateTime.Now.ToString());


                    }
                    DataTable dtTime = new DataTable();
                    for (int dayno = 0; dayno < chkDayUnderPeriod.Items.Count; dayno++)
                    {
                        dtTime = objEmpSch.GetSheduleDescriptionByEmpId(str, Convert.ToDateTime(chkDayUnderPeriod.Items[dayno].Value).ToString());

                        if (chkDayUnderPeriod.Items[dayno].Selected)
                        {
                            if (dtTime.Rows.Count > 0)
                            {
                                for (int j = 0; j < chkTimeTableList.Items.Count; j++)
                                {

                                    if (chkTimeTableList.Items[j].Selected)
                                    {

                                        if (dtTime.Rows[0]["Is_Off"].ToString() == "True")
                                        {
                                            objEmpSch.DeleteScheduleDescByEmpIdandDate(str, Convert.ToDateTime(chkDayUnderPeriod.Items[dayno].Value).ToString(), Convert.ToDateTime(chkDayUnderPeriod.Items[dayno].Value).ToString());


                                            objEmpSch.InsertScheduleDescription(b.ToString(), chkTimeTableList.Items[j].Value, Convert.ToDateTime(chkDayUnderPeriod.Items[dayno].Value).ToString(), false.ToString(), true.ToString(), false.ToString(), str, ddlShift.SelectedValue, "", "", "", "", "", true.ToString(), DateTime.Now.ToString(), true.ToString(), Session["UserId"].ToString(), DateTime.Now.ToString(), Session["UserId"].ToString(), DateTime.Now.ToString());




                                        }
                                        else
                                        {


                                            DateTime OnDutyTime = Convert.ToDateTime(GetDutyTime("On", chkTimeTableList.Items[j].Value));
                                            DateTime OffDutyTime = Convert.ToDateTime(GetDutyTime("Off", chkTimeTableList.Items[j].Value));
                                            int flag1 = 0;
                                            for (int s = 0; s < dtTime.Rows.Count; s++)
                                            {

                                                if (ISOverLapTimeTable(Convert.ToDateTime(dtTime.Rows[s]["OnDuty_Time"].ToString()), Convert.ToDateTime(dtTime.Rows[s]["OffDuty_Time"].ToString()), OnDutyTime, OffDutyTime))
                                                {
                                                    flag1 = 1;
                                                }

                                            }


                                            if (flag1 == 0)
                                            {
                                                objEmpSch.InsertScheduleDescription(b.ToString(), chkTimeTableList.Items[j].Value, Convert.ToDateTime(chkDayUnderPeriod.Items[dayno].Value).ToString(), false.ToString(), true.ToString(), false.ToString(), str, ddlShift.SelectedValue, "", "", "", "", "", true.ToString(), DateTime.Now.ToString(), true.ToString(), Session["UserId"].ToString(), DateTime.Now.ToString(), Session["UserId"].ToString(), DateTime.Now.ToString());


                                            }


                                        }


                                    }
                                }
                            }
                            else
                            {
                                for (int j = 0; j < chkTimeTableList.Items.Count; j++)
                                {
                                    if (chkTimeTableList.Items[j].Selected)
                                    {

                                        objEmpSch.InsertScheduleDescription(b.ToString(), chkTimeTableList.Items[j].Value, chkDayUnderPeriod.Items[dayno].Value, false.ToString(), true.ToString(), false.ToString(), str, "0", "", "", "", "", "", true.ToString(), DateTime.Now.ToString(), true.ToString(), Session["UserId"].ToString(), DateTime.Now.ToString(), Session["UserId"].ToString(), DateTime.Now.ToString());
                                    }


                                }
                            }

                        }



                    }
                }

            }

        }

        DisplayMessage("Record Updated");


    }

    protected void btnsave_Click(object sender, EventArgs e)
    {
        int b = 0;
        bool IsTemp = false;
       


        try
        {
            if (txtFrom.Text == "")
            {
                DisplayMessage("From Date Required");
                txtFrom.Focus();
                return;
            }
            if (txtTo.Text == "")
            {
                DisplayMessage("To Date Required");
                txtTo.Focus();
                return;
            }

            if (Convert.ToDateTime(txtFrom.Text.ToString()) > Convert.ToDateTime(txtTo.Text.ToString()))
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
        if (ddlShift.SelectedIndex == 0)
        {

            DisplayMessage("Please select a shift");

            return;
        }



        string[] weekdays = new string[8];
        weekdays[1] = "Sunday";
        weekdays[2] = "Monday";
        weekdays[3] = "Tuesday";
        weekdays[4] = "Wednesday";
        weekdays[5] = "Thursday";
        weekdays[6] = "Friday";
        weekdays[7] = "Saturday";



        if (rbtnEmp.Checked)
        {

            if (GvEmpListSelected.Rows.Count == 0)
            {

                DisplayMessage("Please select at least one employee");



                return;
            }


            //start

            for (int i = 0; i < GvEmpListSelected.Rows.Count; i++)
            {

                DataTable dtSch = objEmpSch.GetSheduleMaster();

                dtSch = new DataView(dtSch, "Shift_Type='Normal Shift'  and Shift_Id='" + ddlShift.SelectedValue + "' and Emp_Id='" + GetEmpId(GvEmpListSelected.DataKeys[i][0].ToString()) + "'", "", DataViewRowState.CurrentRows).ToTable();

                if (dtSch.Rows.Count == 0)
                {
                    b = objEmpSch.InsertScheduleMaster(Session["CompId"].ToString(), Session["BrandId"].ToString(), Session["LocId"].ToString(), GetEmpId(GvEmpListSelected.DataKeys[i][0].ToString()), ddlShift.SelectedValue, "Normal Shift", Convert.ToDateTime(txtFrom.Text).ToString(), Convert.ToDateTime(txtTo.Text).ToString(), "", "", "", "", "", "", true.ToString(), DateTime.Now.ToString(), true.ToString(), Session["UserId"].ToString(), DateTime.Now.ToString(), Session["UserId"].ToString(), DateTime.Now.ToString());
                }
                else
                {
                    b = objEmpSch.UpdateScheduleMaster(dtSch.Rows[0]["Schedule_Id"].ToString(), Session["CompId"].ToString(), Session["BrandId"].ToString(), Session["LocId"].ToString(), GetEmpId(GvEmpListSelected.DataKeys[i][0].ToString()), ddlShift.SelectedValue, "Normal Shift", Convert.ToDateTime(txtFrom.Text).ToString(), Convert.ToDateTime(txtTo.Text).ToString(), "", "", "", "", "", "", true.ToString(), DateTime.Now.ToString(), true.ToString(), Session["UserId"].ToString(), DateTime.Now.ToString());


                }

                if (b != 0)
                {
                    DateTime DtFromDate = Convert.ToDateTime(txtFrom.Text.ToString());
                    DateTime DtToDate = Convert.ToDateTime(txtTo.Text.ToString());
                    int days = DtToDate.Subtract(DtFromDate).Days + 1;
                    if (IsTemp == false)
                    {

                        int counter = 0;
                        // From Date to To Date
                        DataTable dtShift = objShift.GetShiftMasterById(Session["CompId"].ToString(), ddlShift.SelectedValue);


                        DataTable dtShiftD = objShiftdesc.GetShiftDescriptionByShiftId(ddlShift.SelectedValue);

                        if (dtShiftD.Rows.Count == 0)
                        {
                            DisplayMessage("Shift Not Defined");
                            return;

                        }

                        dtShiftD = new DataView(dtShiftD, "", "", DataViewRowState.CurrentRows).ToTable();


                        DataTable dtTime = new DataTable();
                        DateTime dtStartCheck = Convert.ToDateTime(dtShift.Rows[0]["Apply_From"].ToString());
                        DataTable dtTempShift = new DataTable();
                        int TotalDays = 1;
                        int index = Convert.ToInt16(dtShift.Rows[0]["Cycle_Unit"].ToString().Trim());
                        int cycle = Convert.ToInt16(dtShift.Rows[0]["Cycle_No"].ToString().Trim());

                        string cycletype = string.Empty;
                        string cycleday = string.Empty;
                        DateTime ApplyFromDate = new DateTime();
                        if (index == 7)
                        {

                            bool IsweekOff = false;
                            int daysShift = cycle * index;
                            string weekday = DtFromDate.DayOfWeek.ToString();


                            int state = 0;
                            int k = GetCycleDay(weekday);
                            int j = 1;
                            int a = k;
                            int f = 0;
                            while (DtFromDate <= DtToDate)
                            {
                                if (k % 7 == 0)
                                {
                                    if (f != 0)
                                    {
                                        j++;
                                        if (j > cycle)
                                        {
                                            j = 1;

                                        }
                                    }
                                }

                                if (k <= daysShift)
                                {

                                    if (k > 7)
                                    {
                                        f = 1;

                                    }
                                    a = GetCycleDay(DtFromDate.DayOfWeek.ToString());


                                    DataTable dtGetTemp1 = new DataView(dtShiftD, "Cycle_Type='Week-" + j.ToString() + "' and Cycle_Day='" + a.ToString() + "'", "", DataViewRowState.CurrentRows).ToTable();

                                    if (dtGetTemp1.Rows.Count > 0)
                                    {
                                        dtTime = objEmpSch.GetSheduleDescriptionByEmpId(GetEmpId(GvEmpListSelected.DataKeys[i][0].ToString()), DtFromDate.ToString());

                                        if (dtTime.Rows.Count > 0)
                                        {

                                            for (int t = 0; t < dtGetTemp1.Rows.Count; t++)
                                            {
                                                DateTime OnDutyTime = Convert.ToDateTime(GetDutyTime("On", dtGetTemp1.Rows[t]["TimeTable_Id"].ToString()));
                                                DateTime OffDutyTime = Convert.ToDateTime(GetDutyTime("Off", dtGetTemp1.Rows[t]["TimeTable_Id"].ToString()));
                                                int flag1 = 0;

                                                if (dtTime.Rows.Count > 0)
                                                {
                                                    for (int s = 0; s < dtTime.Rows.Count; s++)
                                                    {
                                                        if (!Convert.ToBoolean(dtTime.Rows[s]["Is_Off"].ToString()))
                                                        {
                                                            if (ISOverLapTimeTable(Convert.ToDateTime(dtTime.Rows[s]["OnDuty_Time"].ToString()), Convert.ToDateTime(dtTime.Rows[s]["OffDuty_Time"].ToString()), OnDutyTime, OffDutyTime))
                                                            {
                                                                flag1 = 1;
                                                            }
                                                        }

                                                    }
                                                    if (flag1 == 0)
                                                    {
                                                        objEmpSch.InsertScheduleDescription(b.ToString(), dtGetTemp1.Rows[t]["TimeTable_Id"].ToString(), DtFromDate.ToString(), false.ToString(), false.ToString(), false.ToString(), GetEmpId(GvEmpListSelected.DataKeys[i][0].ToString()), ddlShift.SelectedValue, "", "", "", "", "", true.ToString(), DateTime.Now.ToString(), true.ToString(), Session["UserId"].ToString(), DateTime.Now.ToString(), Session["UserId"].ToString(), DateTime.Now.ToString());


                                                    }


                                                }
                                                else
                                                {
                                                    objEmpSch.InsertScheduleDescription(b.ToString(), dtGetTemp1.Rows[t]["TimeTable_Id"].ToString(), DtFromDate.ToString(), false.ToString(), false.ToString(), false.ToString(), GetEmpId(GvEmpListSelected.DataKeys[i][0].ToString()), ddlShift.SelectedValue, "", "", "", "", "", true.ToString(), DateTime.Now.ToString(), true.ToString(), Session["UserId"].ToString(), DateTime.Now.ToString(), Session["UserId"].ToString(), DateTime.Now.ToString());


                                                }

                                            }
                                        }
                                        else
                                        {
                                            dtTime = objEmpSch.GetSheduleDescriptionByEmpId(GetEmpId(GvEmpListSelected.DataKeys[i][0].ToString()), DtFromDate.ToString());

                                            for (int t = 0; t < dtGetTemp1.Rows.Count; t++)
                                            {
                                                DateTime OnDutyTime = Convert.ToDateTime(GetDutyTime("On", dtGetTemp1.Rows[t]["TimeTable_Id"].ToString()));
                                                DateTime OffDutyTime = Convert.ToDateTime(GetDutyTime("Off", dtGetTemp1.Rows[t]["TimeTable_Id"].ToString()));
                                                int flag1 = 0;

                                                if (dtTime.Rows.Count > 0)
                                                {
                                                    for (int s = 0; s < dtTime.Rows.Count; s++)
                                                    {
                                                        if (!Convert.ToBoolean(dtTime.Rows[s]["Is_Off"].ToString()))
                                                        {
                                                            if (ISOverLapTimeTable(Convert.ToDateTime(dtTime.Rows[s]["OnDuty_Time"].ToString()), Convert.ToDateTime(dtTime.Rows[s]["OffDuty_Time"].ToString()), OnDutyTime, OffDutyTime))
                                                            {
                                                                flag1 = 1;
                                                            }
                                                        }

                                                    }
                                                    if (flag1 == 0)
                                                    {
                                                        objEmpSch.InsertScheduleDescription(b.ToString(), dtGetTemp1.Rows[t]["TimeTable_Id"].ToString(), DtFromDate.ToString(), false.ToString(), false.ToString(), false.ToString(), GetEmpId(GvEmpListSelected.DataKeys[i][0].ToString()), ddlShift.SelectedValue, "", "", "", "", "", true.ToString(), DateTime.Now.ToString(), true.ToString(), Session["UserId"].ToString(), DateTime.Now.ToString(), Session["UserId"].ToString(), DateTime.Now.ToString());


                                                    }


                                                }
                                                else
                                                {
                                                    objEmpSch.InsertScheduleDescription(b.ToString(), dtGetTemp1.Rows[t]["TimeTable_Id"].ToString(), DtFromDate.ToString(), false.ToString(), false.ToString(), false.ToString(), GetEmpId(GvEmpListSelected.DataKeys[i][0].ToString()), ddlShift.SelectedValue, "", "", "", "", "", true.ToString(), DateTime.Now.ToString(), true.ToString(), Session["UserId"].ToString(), DateTime.Now.ToString(), Session["UserId"].ToString(), DateTime.Now.ToString());


                                                }

                                            }

                                        }
                                    }
                                    else
                                    {

                                        DataTable dts = objEmpSch.GetSheduleDescriptionByEmpId(GetEmpId(GvEmpListSelected.DataKeys[i][0].ToString()), DtFromDate.ToString());

                                        if (dts.Rows.Count == 0)
                                        {
                                            objEmpSch.InsertScheduleDescription(b.ToString(), "0", DtFromDate.ToString(), true.ToString(), false.ToString(), false.ToString(), GetEmpId(GvEmpListSelected.DataKeys[i][0].ToString()), ddlShift.SelectedValue, "", "", "", "", "", true.ToString(), DateTime.Now.ToString(), true.ToString(), Session["UserId"].ToString(), DateTime.Now.ToString(), Session["UserId"].ToString(), DateTime.Now.ToString());
                                        }
                                    }




                                    k++;
                                }
                                else
                                {
                                    k = 1;
                                    j = 1;
                                    DataTable dtGetTemp1 = new DataView(dtShiftD, "Cycle_Type='Week-" + j.ToString() + "' and Cycle_Day='" + a.ToString() + "'", "", DataViewRowState.CurrentRows).ToTable();
                                    dtTime = objEmpSch.GetSheduleDescriptionByEmpId(GetEmpId(GvEmpListSelected.DataKeys[i][0].ToString()), DtFromDate.ToString());

                                    if (dtGetTemp1.Rows.Count > 0)
                                    {
                                        if (dtTime.Rows.Count > 0)
                                        {

                                            for (int t = 0; t < dtGetTemp1.Rows.Count; t++)
                                            {
                                                DateTime OnDutyTime = Convert.ToDateTime(GetDutyTime("On", dtGetTemp1.Rows[t]["TimeTable_Id"].ToString()));
                                                DateTime OffDutyTime = Convert.ToDateTime(GetDutyTime("Off", dtGetTemp1.Rows[t]["TimeTable_Id"].ToString()));
                                                int flag1 = 0;

                                                if (dtTime.Rows.Count > 0)
                                                {
                                                    for (int s = 0; s < dtTime.Rows.Count; s++)
                                                    {
                                                        if (!Convert.ToBoolean(dtTime.Rows[s]["Is_Off"].ToString()))
                                                        {
                                                            if (!Convert.ToBoolean(dtTime.Rows[s]["Is_Off"].ToString()))
                                                            {

                                                                if (ISOverLapTimeTable(Convert.ToDateTime(dtTime.Rows[s]["OnDuty_Time"].ToString()), Convert.ToDateTime(dtTime.Rows[s]["OffDuty_Time"].ToString()), OnDutyTime, OffDutyTime))
                                                                {
                                                                    flag1 = 1;
                                                                }
                                                            }
                                                        }

                                                    }
                                                    if (flag1 == 0)
                                                    {
                                                        objEmpSch.InsertScheduleDescription(b.ToString(), dtGetTemp1.Rows[t]["TimeTable_Id"].ToString(), DtFromDate.ToString(), false.ToString(), false.ToString(), false.ToString(), GetEmpId(GvEmpListSelected.DataKeys[i][0].ToString()), ddlShift.SelectedValue, "", "", "", "", "", true.ToString(), DateTime.Now.ToString(), true.ToString(), Session["UserId"].ToString(), DateTime.Now.ToString(), Session["UserId"].ToString(), DateTime.Now.ToString());


                                                    }


                                                }
                                                else
                                                {
                                                    objEmpSch.InsertScheduleDescription(b.ToString(), dtGetTemp1.Rows[t]["TimeTable_Id"].ToString(), DtFromDate.ToString(), false.ToString(), false.ToString(), false.ToString(), GetEmpId(GvEmpListSelected.DataKeys[i][0].ToString()), ddlShift.SelectedValue, "", "", "", "", "", true.ToString(), DateTime.Now.ToString(), true.ToString(), Session["UserId"].ToString(), DateTime.Now.ToString(), Session["UserId"].ToString(), DateTime.Now.ToString());


                                                }

                                            }
                                        }
                                        else
                                        {
                                            dtTime = objEmpSch.GetSheduleDescriptionByEmpId(GetEmpId(GvEmpListSelected.DataKeys[i][0].ToString()), DtFromDate.ToString());

                                            for (int t = 0; t < dtGetTemp1.Rows.Count; t++)
                                            {
                                                DateTime OnDutyTime = Convert.ToDateTime(GetDutyTime("On", dtGetTemp1.Rows[t]["TimeTable_Id"].ToString()));
                                                DateTime OffDutyTime = Convert.ToDateTime(GetDutyTime("Off", dtGetTemp1.Rows[t]["TimeTable_Id"].ToString()));
                                                int flag1 = 0;

                                                if (dtTime.Rows.Count > 0)
                                                {
                                                    for (int s = 0; s < dtTime.Rows.Count; s++)
                                                    {
                                                        if (!Convert.ToBoolean(dtTime.Rows[s]["Is_Off"].ToString()))
                                                        {
                                                            if (ISOverLapTimeTable(Convert.ToDateTime(dtTime.Rows[s]["OnDuty_Time"].ToString()), Convert.ToDateTime(dtTime.Rows[s]["OffDuty_Time"].ToString()), OnDutyTime, OffDutyTime))
                                                            {
                                                                flag1 = 1;
                                                            }
                                                        }

                                                    }
                                                    if (flag1 == 0)
                                                    {
                                                        objEmpSch.InsertScheduleDescription(b.ToString(), dtGetTemp1.Rows[t]["TimeTable_Id"].ToString(), DtFromDate.ToString(), false.ToString(), false.ToString(), false.ToString(), GetEmpId(GvEmpListSelected.DataKeys[i][0].ToString()), ddlShift.SelectedValue, "", "", "", "", "", true.ToString(), DateTime.Now.ToString(), true.ToString(), Session["UserId"].ToString(), DateTime.Now.ToString(), Session["UserId"].ToString(), DateTime.Now.ToString());


                                                    }


                                                }
                                                else
                                                {
                                                    objEmpSch.InsertScheduleDescription(b.ToString(), dtGetTemp1.Rows[t]["TimeTable_Id"].ToString(), DtFromDate.ToString(), false.ToString(), false.ToString(), false.ToString(), GetEmpId(GvEmpListSelected.DataKeys[i][0].ToString()), ddlShift.SelectedValue, "", "", "", "", "", true.ToString(), DateTime.Now.ToString(), true.ToString(), Session["UserId"].ToString(), DateTime.Now.ToString(), Session["UserId"].ToString(), DateTime.Now.ToString());


                                                }

                                            }

                                        }
                                    }
                                    else
                                    {
                                        DataTable dts = objEmpSch.GetSheduleDescriptionByEmpId(GetEmpId(GvEmpListSelected.DataKeys[i][0].ToString()), DtFromDate.ToString());

                                        if (dts.Rows.Count == 0)
                                        {
                                            objEmpSch.InsertScheduleDescription(b.ToString(), "0", DtFromDate.ToString(), true.ToString(), false.ToString(), false.ToString(), GetEmpId(GvEmpListSelected.DataKeys[i][0].ToString()), ddlShift.SelectedValue, "", "", "", "", "", true.ToString(), DateTime.Now.ToString(), true.ToString(), Session["UserId"].ToString(), DateTime.Now.ToString(), Session["UserId"].ToString(), DateTime.Now.ToString());
                                        }
                                    }

                                }



                                DtFromDate = DtFromDate.AddDays(1);
                            }

                        }
                        else if (index == 31)
                        {
                            int daysShift = cycle * index;

                            int k = DtFromDate.Day;
                            int a = 0;
                            int j = 1;
                            int mon = DtFromDate.Month;
                            while (DtFromDate <= DtToDate)
                            {
                                if (k <= daysShift)
                                {
                                    a = DtFromDate.Day;


                                    DataTable dtGetTemp1 = new DataView(dtShiftD, "Cycle_Type='Month-" + j.ToString() + "' and Cycle_Day='" + a.ToString() + "'", "", DataViewRowState.CurrentRows).ToTable();

                                    //
                                    if (dtGetTemp1.Rows.Count > 0)
                                    {


                                        for (int t = 0; t < dtGetTemp1.Rows.Count; t++)
                                        {
                                            dtTime = objEmpSch.GetSheduleDescriptionByEmpId(GetEmpId(GvEmpListSelected.DataKeys[i][0].ToString()), DtFromDate.ToString());

                                            if (dtGetTemp1.Rows[t]["TimeTable_Id"].ToString() == "")
                                            {

                                                DataTable dts = objEmpSch.GetSheduleDescriptionByEmpId(GetEmpId(GvEmpListSelected.DataKeys[i][0].ToString()), DtFromDate.ToString());

                                                if (dts.Rows.Count == 0)
                                                {
                                                    objEmpSch.InsertScheduleDescription(b.ToString(), "0", DtFromDate.ToString(), true.ToString(), false.ToString(), false.ToString(), GetEmpId(GvEmpListSelected.DataKeys[i][0].ToString()), ddlShift.SelectedValue, "", "", "", "", "", true.ToString(), DateTime.Now.ToString(), true.ToString(), Session["UserId"].ToString(), DateTime.Now.ToString(), Session["UserId"].ToString(), DateTime.Now.ToString());
                                                }
                                            }
                                            else
                                            {
                                                DateTime OnDutyTime = Convert.ToDateTime(GetDutyTime("On", dtGetTemp1.Rows[t]["TimeTable_Id"].ToString()));
                                                DateTime OffDutyTime = Convert.ToDateTime(GetDutyTime("Off", dtGetTemp1.Rows[t]["TimeTable_Id"].ToString()));
                                                int flag1 = 0;

                                                if (dtTime.Rows.Count > 0)
                                                {
                                                    for (int s = 0; s < dtTime.Rows.Count; s++)
                                                    {
                                                        if (!Convert.ToBoolean(dtTime.Rows[s]["Is_Off"].ToString()))
                                                        {
                                                            if (ISOverLapTimeTable(Convert.ToDateTime(dtTime.Rows[s]["OnDuty_Time"].ToString()), Convert.ToDateTime(dtTime.Rows[s]["OffDuty_Time"].ToString()), OnDutyTime, OffDutyTime))
                                                            {
                                                                flag1 = 1;
                                                            }
                                                        }

                                                    }
                                                    if (flag1 == 0)
                                                    {
                                                        objEmpSch.InsertScheduleDescription(b.ToString(), dtGetTemp1.Rows[t]["TimeTable_Id"].ToString(), DtFromDate.ToString(), false.ToString(), false.ToString(), false.ToString(), GetEmpId(GvEmpListSelected.DataKeys[i][0].ToString()), ddlShift.SelectedValue, "", "", "", "", "", true.ToString(), DateTime.Now.ToString(), true.ToString(), Session["UserId"].ToString(), DateTime.Now.ToString(), Session["UserId"].ToString(), DateTime.Now.ToString());


                                                    }


                                                }
                                                else
                                                {

                                                    for (int t1 = 0; t1 < dtGetTemp1.Rows.Count; t1++)
                                                    {



                                                        objEmpSch.InsertScheduleDescription(b.ToString(), dtGetTemp1.Rows[t1]["TimeTable_Id"].ToString(), DtFromDate.ToString(), false.ToString(), false.ToString(), false.ToString(), GetEmpId(GvEmpListSelected.DataKeys[i][0].ToString()), ddlShift.SelectedValue, "", "", "", "", "", true.ToString(), DateTime.Now.ToString(), true.ToString(), Session["UserId"].ToString(), DateTime.Now.ToString(), Session["UserId"].ToString(), DateTime.Now.ToString());





                                                    }


                                                }
                                            }

                                        }



                                    }
                                    else
                                    {
                                        DataTable dts = objEmpSch.GetSheduleDescriptionByEmpId(GetEmpId(GvEmpListSelected.DataKeys[i][0].ToString()), DtFromDate.ToString());

                                        if (dts.Rows.Count == 0)
                                        {
                                            objEmpSch.InsertScheduleDescription(b.ToString(), "0", DtFromDate.ToString(), true.ToString(), false.ToString(), false.ToString(), GetEmpId(GvEmpListSelected.DataKeys[i][0].ToString()), ddlShift.SelectedValue, "", "", "", "", "", true.ToString(), DateTime.Now.ToString(), true.ToString(), Session["UserId"].ToString(), DateTime.Now.ToString(), Session["UserId"].ToString(), DateTime.Now.ToString());
                                        }
                                    }


                                }

                                k++;
                                if (k > daysShift)
                                {

                                    k = 1;
                                    j = 1;
                                }


                                DtFromDate = DtFromDate.AddDays(1);
                                if (DtFromDate.Day == 1)
                                {

                                    j++;

                                }
                            }

                        }
                        else if (index == 1)
                        {
                            int k = 1;
                            int a = k;
                            int daysShift = cycle * index;
                            while (DtFromDate <= DtToDate)
                            {
                                if (k <= daysShift)
                                {
                                    a = k;


                                    DataTable dtGetTemp1 = new DataView(dtShiftD, "Cycle_Type='Day' and Cycle_Day='" + a.ToString() + "'", "", DataViewRowState.CurrentRows).ToTable();

                                    //
                                    if (dtGetTemp1.Rows.Count > 0)
                                    {


                                        for (int t = 0; t < dtGetTemp1.Rows.Count; t++)
                                        {
                                            dtTime = objEmpSch.GetSheduleDescriptionByEmpId(GetEmpId(GvEmpListSelected.DataKeys[i][0].ToString()), DtFromDate.ToString());

                                            if (dtGetTemp1.Rows[t]["TimeTable_Id"].ToString() == "")
                                            {

                                                DataTable dts = objEmpSch.GetSheduleDescriptionByEmpId(GetEmpId(GvEmpListSelected.DataKeys[i][0].ToString()), DtFromDate.ToString());

                                                if (dts.Rows.Count == 0)
                                                {
                                                    objEmpSch.InsertScheduleDescription(b.ToString(), "0", DtFromDate.ToString(), true.ToString(), false.ToString(), false.ToString(), GetEmpId(GvEmpListSelected.DataKeys[i][0].ToString()), ddlShift.SelectedValue, "", "", "", "", "", true.ToString(), DateTime.Now.ToString(), true.ToString(), Session["UserId"].ToString(), DateTime.Now.ToString(), Session["UserId"].ToString(), DateTime.Now.ToString());
                                                }
                                            }
                                            else
                                            {
                                                DateTime OnDutyTime = Convert.ToDateTime(GetDutyTime("On", dtGetTemp1.Rows[t]["TimeTable_Id"].ToString()));
                                                DateTime OffDutyTime = Convert.ToDateTime(GetDutyTime("Off", dtGetTemp1.Rows[t]["TimeTable_Id"].ToString()));
                                                int flag1 = 0;

                                                if (dtTime.Rows.Count > 0)
                                                {
                                                    for (int s = 0; s < dtTime.Rows.Count; s++)
                                                    {
                                                        if (!Convert.ToBoolean(dtTime.Rows[s]["Is_Off"].ToString()))
                                                        {
                                                            if (ISOverLapTimeTable(Convert.ToDateTime(dtTime.Rows[s]["OnDuty_Time"].ToString()), Convert.ToDateTime(dtTime.Rows[s]["OffDuty_Time"].ToString()), OnDutyTime, OffDutyTime))
                                                            {
                                                                flag1 = 1;
                                                            }
                                                        }

                                                    }
                                                    if (flag1 == 0)
                                                    {
                                                        objEmpSch.InsertScheduleDescription(b.ToString(), dtGetTemp1.Rows[t]["TimeTable_Id"].ToString(), DtFromDate.ToString(), false.ToString(), false.ToString(), false.ToString(), GetEmpId(GvEmpListSelected.DataKeys[i][0].ToString()), ddlShift.SelectedValue, "", "", "", "", "", true.ToString(), DateTime.Now.ToString(), true.ToString(), Session["UserId"].ToString(), DateTime.Now.ToString(), Session["UserId"].ToString(), DateTime.Now.ToString());


                                                    }


                                                }
                                                else
                                                {

                                                    for (int t1 = 0; t1 < dtGetTemp1.Rows.Count; t1++)
                                                    {



                                                        objEmpSch.InsertScheduleDescription(b.ToString(), dtGetTemp1.Rows[t1]["TimeTable_Id"].ToString(), DtFromDate.ToString(), false.ToString(), false.ToString(), false.ToString(), GetEmpId(GvEmpListSelected.DataKeys[i][0].ToString()), ddlShift.SelectedValue, "", "", "", "", "", true.ToString(), DateTime.Now.ToString(), true.ToString(), Session["UserId"].ToString(), DateTime.Now.ToString(), Session["UserId"].ToString(), DateTime.Now.ToString());





                                                    }


                                                }
                                            }

                                        }



                                    }
                                    else
                                    {
                                        DataTable dts = objEmpSch.GetSheduleDescriptionByEmpId(GetEmpId(GvEmpListSelected.DataKeys[i][0].ToString()), DtFromDate.ToString());

                                        if (dts.Rows.Count == 0)
                                        {
                                            objEmpSch.InsertScheduleDescription(b.ToString(), "0", DtFromDate.ToString(), true.ToString(), false.ToString(), false.ToString(), GetEmpId(GvEmpListSelected.DataKeys[i][0].ToString()), ddlShift.SelectedValue, "", "", "", "", "", true.ToString(), DateTime.Now.ToString(), true.ToString(), Session["UserId"].ToString(), DateTime.Now.ToString(), Session["UserId"].ToString(), DateTime.Now.ToString());
                                        }
                                    }


                                }

                                k++;
                                if (k > daysShift)
                                {

                                    k = 1;
                                   
                                }


                                DtFromDate = DtFromDate.AddDays(1);
                                
                            }




                        }


                    }
                    else
                    {//temporary


                    }
                }

            }
            //end
        }
        else
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
                return;
            }



          


            

            foreach(string str in EmpIds.Split(','))
                {

                    if (str != "")
                    {
                DataTable dtSch = objEmpSch.GetSheduleMaster();

                dtSch = new DataView(dtSch, "Shift_Type='Normal Shift'  and Shift_Id='" + ddlShift.SelectedValue + "' and Emp_Id='" + str + "'", "", DataViewRowState.CurrentRows).ToTable();

                if (dtSch.Rows.Count == 0)
                {
                    b = objEmpSch.InsertScheduleMaster(Session["CompId"].ToString(), Session["BrandId"].ToString(), Session["LocId"].ToString(), str, ddlShift.SelectedValue, "Normal Shift", Convert.ToDateTime(txtFrom.Text).ToString(), Convert.ToDateTime(txtTo.Text).ToString(), "", "", "", "", "", "", true.ToString(), DateTime.Now.ToString(), true.ToString(), Session["UserId"].ToString(), DateTime.Now.ToString(), Session["UserId"].ToString(), DateTime.Now.ToString());
                }
                else
                {
                    b = objEmpSch.UpdateScheduleMaster(dtSch.Rows[0]["Schedule_Id"].ToString(), Session["CompId"].ToString(), Session["BrandId"].ToString(), Session["LocId"].ToString(), str, ddlShift.SelectedValue, "Normal Shift", Convert.ToDateTime(txtFrom.Text).ToString(), Convert.ToDateTime(txtTo.Text).ToString(), "", "", "", "", "", "", true.ToString(), DateTime.Now.ToString(), true.ToString(), Session["UserId"].ToString(), DateTime.Now.ToString());


                }

                if (b != 0)
                {
                    DateTime DtFromDate = Convert.ToDateTime(txtFrom.Text.ToString());
                    DateTime DtToDate = Convert.ToDateTime(txtTo.Text.ToString());
                    int days = DtToDate.Subtract(DtFromDate).Days + 1;
                    if (IsTemp == false)
                    {

                        int counter = 0;
                        // From Date to To Date
                        DataTable dtShift = objShift.GetShiftMasterById(Session["CompId"].ToString(), ddlShift.SelectedValue);


                        DataTable dtShiftD = objShiftdesc.GetShiftDescriptionByShiftId(ddlShift.SelectedValue);

                        if (dtShiftD.Rows.Count == 0)
                        {
                            DisplayMessage("Shift Not Defined");
                            return;

                        }

                        dtShiftD = new DataView(dtShiftD, "", "", DataViewRowState.CurrentRows).ToTable();


                        DataTable dtTime = new DataTable();
                        DateTime dtStartCheck = Convert.ToDateTime(dtShift.Rows[0]["Apply_From"].ToString());
                        DataTable dtTempShift = new DataTable();
                        int TotalDays = 1;
                        int index = Convert.ToInt16(dtShift.Rows[0]["Cycle_Unit"].ToString().Trim());
                        int cycle = Convert.ToInt16(dtShift.Rows[0]["Cycle_No"].ToString().Trim());

                        string cycletype = string.Empty;
                        string cycleday = string.Empty;
                        DateTime ApplyFromDate = new DateTime();
                        if (index == 7)
                        {

                            bool IsweekOff = false;
                            int daysShift = cycle * index;
                            string weekday = DtFromDate.DayOfWeek.ToString();


                            int state = 0;
                            int k = GetCycleDay(weekday);
                            int j = 1;
                            int a = k;
                            int f = 0;
                            while (DtFromDate <= DtToDate)
                            {
                                if (k % 7 == 0)
                                {
                                    if (f != 0)
                                    {
                                        j++;
                                        if (j > cycle)
                                        {
                                            j = 1;

                                        }
                                    }
                                }

                                if (k <= daysShift)
                                {

                                    if (k > 7)
                                    {
                                        f = 1;

                                    }
                                    a = GetCycleDay(DtFromDate.DayOfWeek.ToString());


                                    DataTable dtGetTemp1 = new DataView(dtShiftD, "Cycle_Type='Week-" + j.ToString() + "' and Cycle_Day='" + a.ToString() + "'", "", DataViewRowState.CurrentRows).ToTable();

                                    if (dtGetTemp1.Rows.Count > 0)
                                    {
                                        dtTime = objEmpSch.GetSheduleDescriptionByEmpId(str, DtFromDate.ToString());

                                        if (dtTime.Rows.Count > 0)
                                        {

                                            for (int t = 0; t < dtGetTemp1.Rows.Count; t++)
                                            {
                                                DateTime OnDutyTime = Convert.ToDateTime(GetDutyTime("On", dtGetTemp1.Rows[t]["TimeTable_Id"].ToString()));
                                                DateTime OffDutyTime = Convert.ToDateTime(GetDutyTime("Off", dtGetTemp1.Rows[t]["TimeTable_Id"].ToString()));
                                                int flag1 = 0;

                                                if (dtTime.Rows.Count > 0)
                                                {
                                                    for (int s = 0; s < dtTime.Rows.Count; s++)
                                                    {
                                                        if (!Convert.ToBoolean(dtTime.Rows[s]["Is_Off"].ToString()))
                                                        {
                                                            if (ISOverLapTimeTable(Convert.ToDateTime(dtTime.Rows[s]["OnDuty_Time"].ToString()), Convert.ToDateTime(dtTime.Rows[s]["OffDuty_Time"].ToString()), OnDutyTime, OffDutyTime))
                                                            {
                                                                flag1 = 1;
                                                            }
                                                        }

                                                    }
                                                    if (flag1 == 0)
                                                    {
                                                        objEmpSch.InsertScheduleDescription(b.ToString(), dtGetTemp1.Rows[t]["TimeTable_Id"].ToString(), DtFromDate.ToString(), false.ToString(), false.ToString(), false.ToString(), str, ddlShift.SelectedValue, "", "", "", "", "", true.ToString(), DateTime.Now.ToString(), true.ToString(), Session["UserId"].ToString(), DateTime.Now.ToString(), Session["UserId"].ToString(), DateTime.Now.ToString());


                                                    }


                                                }
                                                else
                                                {
                                                    objEmpSch.InsertScheduleDescription(b.ToString(), dtGetTemp1.Rows[t]["TimeTable_Id"].ToString(), DtFromDate.ToString(), false.ToString(), false.ToString(), false.ToString(), str, ddlShift.SelectedValue, "", "", "", "", "", true.ToString(), DateTime.Now.ToString(), true.ToString(), Session["UserId"].ToString(), DateTime.Now.ToString(), Session["UserId"].ToString(), DateTime.Now.ToString());


                                                }

                                            }
                                        }
                                        else
                                        {
                                            dtTime = objEmpSch.GetSheduleDescriptionByEmpId(str, DtFromDate.ToString());

                                            for (int t = 0; t < dtGetTemp1.Rows.Count; t++)
                                            {
                                                DateTime OnDutyTime = Convert.ToDateTime(GetDutyTime("On", dtGetTemp1.Rows[t]["TimeTable_Id"].ToString()));
                                                DateTime OffDutyTime = Convert.ToDateTime(GetDutyTime("Off", dtGetTemp1.Rows[t]["TimeTable_Id"].ToString()));
                                                int flag1 = 0;

                                                if (dtTime.Rows.Count > 0)
                                                {
                                                    for (int s = 0; s < dtTime.Rows.Count; s++)
                                                    {
                                                        if (!Convert.ToBoolean(dtTime.Rows[s]["Is_Off"].ToString()))
                                                        {
                                                            if (ISOverLapTimeTable(Convert.ToDateTime(dtTime.Rows[s]["OnDuty_Time"].ToString()), Convert.ToDateTime(dtTime.Rows[s]["OffDuty_Time"].ToString()), OnDutyTime, OffDutyTime))
                                                            {
                                                                flag1 = 1;
                                                            }
                                                        }

                                                    }
                                                    if (flag1 == 0)
                                                    {
                                                        objEmpSch.InsertScheduleDescription(b.ToString(), dtGetTemp1.Rows[t]["TimeTable_Id"].ToString(), DtFromDate.ToString(), false.ToString(), false.ToString(), false.ToString(), str, ddlShift.SelectedValue, "", "", "", "", "", true.ToString(), DateTime.Now.ToString(), true.ToString(), Session["UserId"].ToString(), DateTime.Now.ToString(), Session["UserId"].ToString(), DateTime.Now.ToString());


                                                    }


                                                }
                                                else
                                                {
                                                    objEmpSch.InsertScheduleDescription(b.ToString(), dtGetTemp1.Rows[t]["TimeTable_Id"].ToString(), DtFromDate.ToString(), false.ToString(), false.ToString(), false.ToString(), str, ddlShift.SelectedValue, "", "", "", "", "", true.ToString(), DateTime.Now.ToString(), true.ToString(), Session["UserId"].ToString(), DateTime.Now.ToString(), Session["UserId"].ToString(), DateTime.Now.ToString());


                                                }

                                            }

                                        }
                                    }
                                    else
                                    {

                                        DataTable dts = objEmpSch.GetSheduleDescriptionByEmpId(str, DtFromDate.ToString());

                                        if (dts.Rows.Count == 0)
                                        {
                                            objEmpSch.InsertScheduleDescription(b.ToString(), "0", DtFromDate.ToString(), true.ToString(), false.ToString(), false.ToString(), str, ddlShift.SelectedValue, "", "", "", "", "", true.ToString(), DateTime.Now.ToString(), true.ToString(), Session["UserId"].ToString(), DateTime.Now.ToString(), Session["UserId"].ToString(), DateTime.Now.ToString());
                                        }
                                    }




                                    k++;
                                }
                                else
                                {
                                    k = 1;
                                    j = 1;
                                    DataTable dtGetTemp1 = new DataView(dtShiftD, "Cycle_Type='Week-" + j.ToString() + "' and Cycle_Day='" + a.ToString() + "'", "", DataViewRowState.CurrentRows).ToTable();
                                    dtTime = objEmpSch.GetSheduleDescriptionByEmpId(str, DtFromDate.ToString());

                                    if (dtGetTemp1.Rows.Count > 0)
                                    {
                                        if (dtTime.Rows.Count > 0)
                                        {

                                            for (int t = 0; t < dtGetTemp1.Rows.Count; t++)
                                            {
                                                DateTime OnDutyTime = Convert.ToDateTime(GetDutyTime("On", dtGetTemp1.Rows[t]["TimeTable_Id"].ToString()));
                                                DateTime OffDutyTime = Convert.ToDateTime(GetDutyTime("Off", dtGetTemp1.Rows[t]["TimeTable_Id"].ToString()));
                                                int flag1 = 0;

                                                if (dtTime.Rows.Count > 0)
                                                {
                                                    for (int s = 0; s < dtTime.Rows.Count; s++)
                                                    {
                                                        if (!Convert.ToBoolean(dtTime.Rows[s]["Is_Off"].ToString()))
                                                        {
                                                            if (ISOverLapTimeTable(Convert.ToDateTime(dtTime.Rows[s]["OnDuty_Time"].ToString()), Convert.ToDateTime(dtTime.Rows[s]["OffDuty_Time"].ToString()), OnDutyTime, OffDutyTime))
                                                            {
                                                                flag1 = 1;
                                                            }
                                                        }

                                                    }
                                                    if (flag1 == 0)
                                                    {
                                                        objEmpSch.InsertScheduleDescription(b.ToString(), dtGetTemp1.Rows[t]["TimeTable_Id"].ToString(), DtFromDate.ToString(), false.ToString(), false.ToString(), false.ToString(), str, ddlShift.SelectedValue, "", "", "", "", "", true.ToString(), DateTime.Now.ToString(), true.ToString(), Session["UserId"].ToString(), DateTime.Now.ToString(), Session["UserId"].ToString(), DateTime.Now.ToString());


                                                    }


                                                }
                                                else
                                                {
                                                    objEmpSch.InsertScheduleDescription(b.ToString(), dtGetTemp1.Rows[t]["TimeTable_Id"].ToString(), DtFromDate.ToString(), false.ToString(), false.ToString(), false.ToString(), str, ddlShift.SelectedValue, "", "", "", "", "", true.ToString(), DateTime.Now.ToString(), true.ToString(), Session["UserId"].ToString(), DateTime.Now.ToString(), Session["UserId"].ToString(), DateTime.Now.ToString());


                                                }

                                            }
                                        }
                                        else
                                        {
                                            dtTime = objEmpSch.GetSheduleDescriptionByEmpId(str, DtFromDate.ToString());

                                            for (int t = 0; t < dtGetTemp1.Rows.Count; t++)
                                            {
                                                DateTime OnDutyTime = Convert.ToDateTime(GetDutyTime("On", dtGetTemp1.Rows[t]["TimeTable_Id"].ToString()));
                                                DateTime OffDutyTime = Convert.ToDateTime(GetDutyTime("Off", dtGetTemp1.Rows[t]["TimeTable_Id"].ToString()));
                                                int flag1 = 0;

                                                if (dtTime.Rows.Count > 0)
                                                {
                                                    for (int s = 0; s < dtTime.Rows.Count; s++)
                                                    {
                                                        if (!Convert.ToBoolean(dtTime.Rows[s]["Is_Off"].ToString()))
                                                        {
                                                            if (ISOverLapTimeTable(Convert.ToDateTime(dtTime.Rows[s]["OnDuty_Time"].ToString()), Convert.ToDateTime(dtTime.Rows[s]["OffDuty_Time"].ToString()), OnDutyTime, OffDutyTime))
                                                            {
                                                                flag1 = 1;

                                                            }
                                                        }

                                                    }
                                                    if (flag1 == 0)
                                                    {
                                                        objEmpSch.InsertScheduleDescription(b.ToString(), dtGetTemp1.Rows[t]["TimeTable_Id"].ToString(), DtFromDate.ToString(), false.ToString(), false.ToString(), false.ToString(), str, ddlShift.SelectedValue, "", "", "", "", "", true.ToString(), DateTime.Now.ToString(), true.ToString(), Session["UserId"].ToString(), DateTime.Now.ToString(), Session["UserId"].ToString(), DateTime.Now.ToString());


                                                    }


                                                }   
                                                else
                                                {
                                                    objEmpSch.InsertScheduleDescription(b.ToString(), dtGetTemp1.Rows[t]["TimeTable_Id"].ToString(), DtFromDate.ToString(), false.ToString(), false.ToString(), false.ToString(), str, ddlShift.SelectedValue, "", "", "", "", "", true.ToString(), DateTime.Now.ToString(), true.ToString(), Session["UserId"].ToString(), DateTime.Now.ToString(), Session["UserId"].ToString(), DateTime.Now.ToString());


                                                }

                                            }

                                        }
                                    }
                                    else
                                    {
                                        DataTable dts = objEmpSch.GetSheduleDescriptionByEmpId(str, DtFromDate.ToString());

                                        if (dts.Rows.Count == 0)
                                        {
                                            objEmpSch.InsertScheduleDescription(b.ToString(), "0", DtFromDate.ToString(), true.ToString(), false.ToString(), false.ToString(), str, ddlShift.SelectedValue, "", "", "", "", "", true.ToString(), DateTime.Now.ToString(), true.ToString(), Session["UserId"].ToString(), DateTime.Now.ToString(), Session["UserId"].ToString(), DateTime.Now.ToString());
                                        }
                                    }

                                }



                                DtFromDate = DtFromDate.AddDays(1);
                            }

                        }
                        else if (index == 31)
                        {
                            int daysShift = cycle * index;

                            int k = DtFromDate.Day;
                            int a = 0;
                            int j = 1;
                            int mon = DtFromDate.Month;
                            while (DtFromDate <= DtToDate)
                            {
                                if (k <= daysShift)
                                {
                                    a = DtFromDate.Day;


                                    DataTable dtGetTemp1 = new DataView(dtShiftD, "Cycle_Type='Month-" + j.ToString() + "' and Cycle_Day='" + a.ToString() + "'", "", DataViewRowState.CurrentRows).ToTable();

                                    //
                                    if (dtGetTemp1.Rows.Count > 0)
                                    {


                                        for (int t = 0; t < dtGetTemp1.Rows.Count; t++)
                                        {
                                            dtTime = objEmpSch.GetSheduleDescriptionByEmpId(str, DtFromDate.ToString());

                                            if (dtGetTemp1.Rows[t]["TimeTable_Id"].ToString() == "")
                                            {

                                                DataTable dts = objEmpSch.GetSheduleDescriptionByEmpId(str, DtFromDate.ToString());

                                                if (dts.Rows.Count == 0)
                                                {
                                                    objEmpSch.InsertScheduleDescription(b.ToString(), "0", DtFromDate.ToString(), true.ToString(), false.ToString(), false.ToString(), str, ddlShift.SelectedValue, "", "", "", "", "", true.ToString(), DateTime.Now.ToString(), true.ToString(), Session["UserId"].ToString(), DateTime.Now.ToString(), Session["UserId"].ToString(), DateTime.Now.ToString());
                                                }
                                            }
                                            else
                                            {
                                                DateTime OnDutyTime = Convert.ToDateTime(GetDutyTime("On", dtGetTemp1.Rows[t]["TimeTable_Id"].ToString()));
                                                DateTime OffDutyTime = Convert.ToDateTime(GetDutyTime("Off", dtGetTemp1.Rows[t]["TimeTable_Id"].ToString()));
                                                int flag1 = 0;

                                                if (dtTime.Rows.Count > 0)
                                                {
                                                    for (int s = 0; s < dtTime.Rows.Count; s++)
                                                    {
                                                        if (!Convert.ToBoolean(dtTime.Rows[s]["Is_Off"].ToString()))
                                                        {
                                                            if (!Convert.ToBoolean(dtTime.Rows[s]["Is_Off"].ToString()))
                                                            {
                                                                if (ISOverLapTimeTable(Convert.ToDateTime(dtTime.Rows[s]["OnDuty_Time"].ToString()), Convert.ToDateTime(dtTime.Rows[s]["OffDuty_Time"].ToString()), OnDutyTime, OffDutyTime))
                                                                {
                                                                    flag1 = 1;
                                                                }
                                                            }
                                                        }

                                                    }
                                                    if (flag1 == 0)
                                                    {
                                                        objEmpSch.InsertScheduleDescription(b.ToString(), dtGetTemp1.Rows[t]["TimeTable_Id"].ToString(), DtFromDate.ToString(), false.ToString(), false.ToString(), false.ToString(), str, ddlShift.SelectedValue, "", "", "", "", "", true.ToString(), DateTime.Now.ToString(), true.ToString(), Session["UserId"].ToString(), DateTime.Now.ToString(), Session["UserId"].ToString(), DateTime.Now.ToString());


                                                    }


                                                }
                                                else
                                                {

                                                    for (int t1 = 0; t1 < dtGetTemp1.Rows.Count; t1++)
                                                    {



                                                        objEmpSch.InsertScheduleDescription(b.ToString(), dtGetTemp1.Rows[t1]["TimeTable_Id"].ToString(), DtFromDate.ToString(), false.ToString(), false.ToString(), false.ToString(), str, ddlShift.SelectedValue, "", "", "", "", "", true.ToString(), DateTime.Now.ToString(), true.ToString(), Session["UserId"].ToString(), DateTime.Now.ToString(), Session["UserId"].ToString(), DateTime.Now.ToString());





                                                    }


                                                }
                                            }

                                        }



                                    }
                                    else
                                    {
                                        DataTable dts = objEmpSch.GetSheduleDescriptionByEmpId(str, DtFromDate.ToString());

                                        if (dts.Rows.Count == 0)
                                        {
                                            objEmpSch.InsertScheduleDescription(b.ToString(), "0", DtFromDate.ToString(), true.ToString(), false.ToString(), false.ToString(), str, ddlShift.SelectedValue, "", "", "", "", "", true.ToString(), DateTime.Now.ToString(), true.ToString(), Session["UserId"].ToString(), DateTime.Now.ToString(), Session["UserId"].ToString(), DateTime.Now.ToString());
                                        }
                                    }


                                }

                                k++;
                                if (k > daysShift)
                                {

                                    k = 1;
                                    j = 1;
                                }


                                DtFromDate = DtFromDate.AddDays(1);
                                if (DtFromDate.Day == 1)
                                {

                                    j++;

                                }
                            }

                        }
                        else if (index == 1)
                        {
                            int k = 1;
                            int a = k;
                            int daysShift = cycle * index;
                            while (DtFromDate <= DtToDate)
                            {
                                if (k <= daysShift)
                                {
                                    a = k;


                                    DataTable dtGetTemp1 = new DataView(dtShiftD, "Cycle_Type='Day' and Cycle_Day='" + a.ToString() + "'", "", DataViewRowState.CurrentRows).ToTable();

                                    //
                                    if (dtGetTemp1.Rows.Count > 0)
                                    {


                                        for (int t = 0; t < dtGetTemp1.Rows.Count; t++)
                                        {
                                            dtTime = objEmpSch.GetSheduleDescriptionByEmpId(str, DtFromDate.ToString());

                                            if (dtGetTemp1.Rows[t]["TimeTable_Id"].ToString() == "")
                                            {

                                                DataTable dts = objEmpSch.GetSheduleDescriptionByEmpId(str, DtFromDate.ToString());

                                                if (dts.Rows.Count == 0)
                                                {
                                                    objEmpSch.InsertScheduleDescription(b.ToString(), "0", DtFromDate.ToString(), true.ToString(), false.ToString(), false.ToString(), str, ddlShift.SelectedValue, "", "", "", "", "", true.ToString(), DateTime.Now.ToString(), true.ToString(), Session["UserId"].ToString(), DateTime.Now.ToString(), Session["UserId"].ToString(), DateTime.Now.ToString());
                                                }
                                            }
                                            else
                                            {
                                                DateTime OnDutyTime = Convert.ToDateTime(GetDutyTime("On", dtGetTemp1.Rows[t]["TimeTable_Id"].ToString()));
                                                DateTime OffDutyTime = Convert.ToDateTime(GetDutyTime("Off", dtGetTemp1.Rows[t]["TimeTable_Id"].ToString()));
                                                int flag1 = 0;

                                                if (dtTime.Rows.Count > 0)
                                                {
                                                    for (int s = 0; s < dtTime.Rows.Count; s++)
                                                    {
                                                        if (!Convert.ToBoolean(dtTime.Rows[s]["Is_Off"].ToString()))
                                                        {
                                                            if (ISOverLapTimeTable(Convert.ToDateTime(dtTime.Rows[s]["OnDuty_Time"].ToString()), Convert.ToDateTime(dtTime.Rows[s]["OffDuty_Time"].ToString()), OnDutyTime, OffDutyTime))
                                                            {
                                                                flag1 = 1;
                                                            }
                                                        }

                                                    }
                                                    if (flag1 == 0)
                                                    {
                                                        objEmpSch.InsertScheduleDescription(b.ToString(), dtGetTemp1.Rows[t]["TimeTable_Id"].ToString(), DtFromDate.ToString(), false.ToString(), false.ToString(), false.ToString(), str, ddlShift.SelectedValue, "", "", "", "", "", true.ToString(), DateTime.Now.ToString(), true.ToString(), Session["UserId"].ToString(), DateTime.Now.ToString(), Session["UserId"].ToString(), DateTime.Now.ToString());


                                                    }


                                                }
                                                else
                                                {

                                                    for (int t1 = 0; t1 < dtGetTemp1.Rows.Count; t1++)
                                                    {



                                                        objEmpSch.InsertScheduleDescription(b.ToString(), dtGetTemp1.Rows[t1]["TimeTable_Id"].ToString(), DtFromDate.ToString(), false.ToString(), false.ToString(), false.ToString(), str, ddlShift.SelectedValue, "", "", "", "", "", true.ToString(), DateTime.Now.ToString(), true.ToString(), Session["UserId"].ToString(), DateTime.Now.ToString(), Session["UserId"].ToString(), DateTime.Now.ToString());





                                                    }


                                                }
                                            }

                                        }



                                    }
                                    else
                                    {
                                        DataTable dts = objEmpSch.GetSheduleDescriptionByEmpId(str, DtFromDate.ToString());

                                        if (dts.Rows.Count == 0)
                                        {
                                            objEmpSch.InsertScheduleDescription(b.ToString(), "0", DtFromDate.ToString(), true.ToString(), false.ToString(), false.ToString(), str, ddlShift.SelectedValue, "", "", "", "", "", true.ToString(), DateTime.Now.ToString(), true.ToString(), Session["UserId"].ToString(), DateTime.Now.ToString(), Session["UserId"].ToString(), DateTime.Now.ToString());
                                        }
                                    }


                                }

                                k++;
                                if (k > daysShift)
                                {

                                    k = 1;

                                }


                                DtFromDate = DtFromDate.AddDays(1);

                            }




                        }


                    }
                    else
                    {//temporary


                    }
                }
            }
            }
        }



        DisplayMessage("Record Saved");



    }
    protected void lnkBackToList_Click(object sender, EventArgs e)
    {
        pnlMenuNew.BackColor = System.Drawing.ColorTranslator.FromHtml("#90bde9");

        pnlMenuList.BackColor = System.Drawing.ColorTranslator.FromHtml("#ccddee");
        
      
        GvShiftReport.DataSource = null;
        GvShiftReport.DataBind();
        Session["IsTemp"] = null;
        PnlList.Visible = true;
        pnlView.Visible = false;
        btnNew.Text = "New";
        txtFromDate.Text = "";
        txtToDate.Text = "";
    }
    private string GetDutyTime(string OnOff, string timetableid)
    {
        string retval = "";
        DataTable dtTimeTableId = objTimetable.GetTimeTableMasterById(Session["CompId"].ToString(), timetableid);
        if (OnOff == "On")
        {
            retval = dtTimeTableId.Rows[0]["OnDuty_Time"].ToString();
        }
        else
        {
            retval = dtTimeTableId.Rows[0]["OffDuty_Time"].ToString();
        }
        return retval;

    }

    public string GetEmpId(string empcode)
    {

        string empId = string.Empty;

        DataTable dt = objEmp.GetEmployeeMasterAllData(Session["CompId"].ToString());
        dt = new DataView(dt, "Emp_Code='" + empcode.ToString().Trim() + "'", "", DataViewRowState.CurrentRows).ToTable();

        if (dt.Rows.Count > 0)
        {

            empId = dt.Rows[0]["Emp_Id"].ToString();



        }


        return empId;



    }

    public bool ISOverLapTimeTable(DateTime dtintime1, DateTime dtouttime1, DateTime dtintime, DateTime dtouttime)
    {
        dtintime1 = new DateTime(DateTime.Now.Year, DateTime.Now.Month, DateTime.Now.Day, dtintime1.Hour, dtintime1.Minute, dtintime1.Second);
        dtouttime1 = new DateTime(DateTime.Now.Year, DateTime.Now.Month, DateTime.Now.Day, dtouttime1.Hour, dtouttime1.Minute, dtouttime1.Second);

        bool isoverlap = false;
        if (dtintime >= dtintime1 && dtintime <= dtouttime1)
        {
            isoverlap = true;

        }
        else if (dtouttime >= dtintime1 && dtouttime <= dtouttime1)
        {
            isoverlap = true;

        }

        else if (dtintime1 >= dtintime && dtintime1 <= dtouttime)
        {
            isoverlap = true;

        }

        else if (dtouttime1 >= dtintime && dtouttime1 <= dtouttime)
        {
            isoverlap = true;

        }
        else if (dtintime1 == dtintime && dtouttime1 == dtouttime)
        {
            isoverlap = true;

        }
        return isoverlap;
    }

    public int GetCycleDay(string day)
    {
        string cycleday = string.Empty;
        string[] weekdays = new string[8];
        weekdays[1] = "Sunday";
        weekdays[2] = "Monday";
        weekdays[3] = "Tuesday";
        weekdays[4] = "Wednesday";
        weekdays[5] = "Thursday";
        weekdays[6] = "Friday";
        weekdays[7] = "Saturday";

        for (int i = 1; i <= 7; i++)
        {
            if (weekdays[i] == day)
            {
                cycleday = i.ToString();

            }
        }

        return int.Parse(cycleday);

    }
    protected void btnClearAll_Click(object sender, EventArgs e)
    {
        for (int t = 0; t < chkTimeTableList.Items.Count; t++)
        {
            chkTimeTableList.Items[t].Selected = false;
        }
        for (int j = 0; j < chkDayUnderPeriod.Items.Count; j++)
        {
            chkDayUnderPeriod.Items[j].Selected = false;
        }
    }
    protected void btnBack_Click(object sender, EventArgs e)
    {
        PnlTimeTableList.Visible = true;
        pnlAddDays.Visible = false;

    }
    public void DisplayMessage(string str)
    {
        ScriptManager.RegisterStartupScript(this, GetType(), "", "alert('" + str + "');", true);
    }
    protected void btnback_Click(object sender, EventArgs e)
    {

    }
    protected void btnbind_Click(object sender, ImageClickEventArgs e)
    {
        DataTable dtproduct = objEmp.GetEmployeeMaster(Session["CompId"].ToString());

        dtproduct = new DataView(dtproduct, "Brand_Id='" + Session["BrandId"].ToString() + "'  and Location_Id='" + Session["LocId"].ToString() + "'", "", DataViewRowState.CurrentRows).ToTable();



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
            if (dtproduct.Rows.Count > 0)
            {

                gvEmp.DataSource = dtproduct;
                gvEmp.DataBind();



            }


        }

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

        if (dtproduct.Rows.Count > 0)
        {

            gvEmp.DataSource = dtproduct;
            gvEmp.DataBind();

        }
        else
        {
            gvEmp.DataSource = null;
            gvEmp.DataBind();

        }

        lblTotalRecord.Text = Resources.Attendance.Total_Records + " : " + dtproduct.Rows.Count.ToString();



    }
    protected void btnsubmit_Click(object sender, EventArgs e)
     {
        if (txtFromDate.Text == "" || txtToDate.Text == "")
        {
            
                ScriptManager.RegisterStartupScript(this, GetType(), "", "alert('Select From Date And To Date');", true);
            
           


            return;
        }



        if (objSys.getDateForInput(txtFromDate.Text.ToString()) > objSys.getDateForInput(txtToDate.Text.ToString()))
        {
            
                ScriptManager.RegisterStartupScript(this, GetType(), "", "alert('To Date should be greater');", true);
           



            return;
        }
        DataTable dtShiftAllDate = objEmpSch.GetSheduleDescription(GetEmpId(editid.Value));
        dtShiftAllDate = new DataView(dtShiftAllDate, "Att_Date>='" + objSys.getDateForInput(txtFromDate.Text.ToString()) + "' and Att_Date<='" + objSys.getDateForInput(txtToDate.Text.ToString()) + "'", "Att_Date", DataViewRowState.CurrentRows).ToTable();
     
       
        GvShiftReport.DataSource = dtShiftAllDate;
        GvShiftReport.DataBind();
    }
    public void bindchecklist()
    {//
        DataTable dt = objTimetable.GetTimeTableMaster(Session["CompId"].ToString());
        chkTimeTableList.DataSource = dt;
        chkTimeTableList.DataTextField = "TimeTable_Name";
        chkTimeTableList.DataValueField = "TimeTable_Id";
        chkTimeTableList.DataBind();


        //bind dayunderperiod checkboxlist

        DateTime dtfrom = objSys.getDateForInput(txtFrom.Text);
        DateTime dtto = objSys.getDateForInput(txtTo.Text);


        DataTable dtfordays = new DataTable();
        dtfordays.Columns.Add("days");
        dtfordays.Columns.Add("dayno");
        DateTime TempDate = dtfrom;
        int totaldays = dtto.Subtract(dtfrom).Days + 1;
        for (int i = 1; i <= totaldays; i++)
        {

            dtfordays.Rows.Add(dtfordays.NewRow());
            dtfordays.Rows[dtfordays.Rows.Count - 1]["days"] = TempDate.ToString("MMM") + ":" + TempDate.Day + ":" + TempDate.DayOfWeek.ToString();
            dtfordays.Rows[dtfordays.Rows.Count - 1]["dayno"] = TempDate.ToString();
            TempDate = TempDate.AddDays(1);
        }

        chkDayUnderPeriod.DataSource = dtfordays;
        chkDayUnderPeriod.DataTextField = "days";
        chkDayUnderPeriod.DataValueField = "dayno";
        chkDayUnderPeriod.DataBind();

        for (int j = 0; j < chkDayUnderPeriod.Items.Count; j++)
        {
            chkDayUnderPeriod.Items[j].Selected = true;
        }


    }

    protected void GvShiftreport_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
           

            String s = GvShiftReport.DataKeys[e.Row.RowIndex]["OnDuty_Time"].ToString();
            if(s=="")
            {
                return;

            }
            ((Label)e.Row.FindControl("lblOnDuty")).Text = Convert.ToDateTime(s).ToString("HH:mm");
            String s1 = GvShiftReport.DataKeys[e.Row.RowIndex]["OffDuty_Time"].ToString();
            ((Label)e.Row.FindControl("lblOffDuty")).Text = Convert.ToDateTime(s1).ToString("HH:mm");
            String empid = GvShiftReport.DataKeys[e.Row.RowIndex]["Emp_Id"].ToString(); 

            string date=GvShiftReport.DataKeys[e.Row.RowIndex]["Att_Date"].ToString();

            Set_Employee_Holiday objempholi = new Set_Employee_Holiday();

            bool b = objempholi.GetEmployeeHolidayOnDateAndEmpId(date,empid);

            if (b)
            {
                ((CheckBox)e.Row.FindControl("chkHoliday")).Checked = true;
            }
            else
            {
                ((CheckBox)e.Row.FindControl("chkHoliday")).Checked = false;
            }
        }
    }
    protected void btnEdit_Command(object sender, CommandEventArgs e)
        {
        Common objcom = new Common();
        editid.Value = e.CommandArgument.ToString();

        lblempname.Text = objcom.GetEmpName(GetEmpId(editid.Value));

        btnNew_Click(null, null);
        pnlMain.Visible = false;
        pnlView.Visible = true;
        btnNew.Text = "View";
        txtFromDate.Text = "";
        txtToDate.Text = "";
    }
    protected void btnNew_Click(object sender, EventArgs e)
    {

        pnlMenuNew.BackColor = System.Drawing.ColorTranslator.FromHtml("#ccddee");

        pnlMenuList.BackColor = System.Drawing.ColorTranslator.FromHtml("#90bde9");

        PnlList.Visible = false;
        PnlNewEdit.Visible = true;
        pnlMain.Visible = true;
        pnlView.Visible = false;
        pnlemployee.Visible = true;
        pnlEmpSel.Visible = false;
        FillGvEmp();
        rbtnEmp.Checked = true;
        rbtnGroup.Checked = false;
        Session["EmpFiltered"] = null;
        Pnl1.Visible = false;
        btnNext.Visible = true;
        EmpGroup_CheckedChanged(null,null);
        trGroup.Visible = true;
        btnCancel2.Visible = true;
        txtFrom.Text = "";
        txtTo.Text = "";
    }
}
