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
using iTextSharp.text;
using iTextSharp.text.html.simpleparser;
using iTextSharp.text.pdf;
using System.IO;
using System.Globalization;
public partial class AttendanceRegister : BasePage
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

        ScriptManager sm = (ScriptManager)Master.FindControl("SM1");
        sm.RegisterPostBackControl(btnExportPdf);


         
          sm.RegisterPostBackControl(btnExportToExcel);
       
        Session["AccordianId"] = "6";
        Session["HeaderText"] = "Attendance Reports";
        Page.Title = objSys.GetSysTitle();
        if (!IsPostBack)
        {
            txtYear.Text = System.DateTime.Now.Year.ToString();
            ddlMonth.SelectedValue = System.DateTime.Now.Month.ToString();

            pnlEmpAtt.Visible = true;

            FillGrid();
            rbtnEmpSal.Checked = true;
            rbtnGroupSal.Checked = false;
            EmpGroupSal_CheckedChanged(null, null);
            pnlReport.Visible = false;
        }
        if (ViewState["SalaryReportSelected"] != null && ViewState["SalaryReportSelected"].ToString() == "1")
        {
            GetReport();
        }
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
        ViewState["SalaryReportSelected"] = 0;
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


        if (ddlMonth.SelectedIndex == 0)
        {
            DisplayMessage("Please select month");
            ddlMonth.Focus();
            return;

        }
        if (txtYear.Text == "")
        {
            DisplayMessage("Please enter year");
            txtYear.Focus();
            return;

        }
        ViewState["SalaryReportSelected"] = "1";
       
        if (rbtnEmpSal.Checked)
        {
            if (empidlist == "")
            {
                DisplayMessage("Select Atleast One Employee");
                return;
            }
            else
            {
                Session["SelectedEmpId"] = empidlist;
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
                else
                {
                    Session["SelectedEmpId"] = empidlist;
                }
            }
            else
            {
                DisplayMessage("Select Group First");
            }

        }
        
        pnlEmpAtt.Visible = false;
        pnlReport.Visible = true;
        Session["DtShortCode"] = null;

        DataTable dtColorCode = new DataTable();
        dtColorCode.Columns.Add("Type");
        dtColorCode.Columns.Add("ShortId");
        dtColorCode.Columns.Add("ColorCode");

        DataRow dr1 = dtColorCode.NewRow();
        dr1["Type"] = "Absent";
        dr1["ColorCode"] = objAppParam.GetApplicationParameterValueByParamName("Absnet_Color_Code", Session["CompId"].ToString());
        dr1["ShortId"] = "A";
        dtColorCode.Rows.Add(dr1);

        DataRow dr2 = dtColorCode.NewRow();
        dr2["Type"] = "Normal";
        dr2["ColorCode"] = objAppParam.GetApplicationParameterValueByParamName("Present_Color_Code", Session["CompId"].ToString());
        dr2["ShortId"] = "N";
        dtColorCode.Rows.Add(dr2);


        DataRow dr3 = dtColorCode.NewRow();
        dr3["Type"] = "Holiday";
        dr3["ColorCode"] = objAppParam.GetApplicationParameterValueByParamName("Holiday_Color_Code", Session["CompId"].ToString());
        dr3["ShortId"] = "H";
        dtColorCode.Rows.Add(dr3);

        DataRow dr4 = dtColorCode.NewRow();
        dr4["Type"] = "Leave";
        dr4["ColorCode"] = objAppParam.GetApplicationParameterValueByParamName("Leave_Color_Code", Session["CompId"].ToString());
        dr4["ShortId"] = "L";
        dtColorCode.Rows.Add(dr4);
        DataRow dr5 = dtColorCode.NewRow();

        dr5["Type"] = "WeekOff";
        dr5["ColorCode"] = "66FF99";
        dr5["ShortId"] = "W";
        dtColorCode.Rows.Add(dr5);

        Session["DtShortCode"] = dtColorCode;
        fillColorInTable();
        Panel1.Visible = true;
      
        GetReport();
        //for (int i = 0; i < empidlist.Split(',').Length; i++)
        //{
        //    if (empidlist.Split(',')[i] == "")
        //    {
        //        continue;
        //    }

        //}       
        


      




        }

    private void fillColorInTable()
    {
        DataTable dtColorCode = (DataTable)Session["DtShortCode"];
        dtColorCode = new DataView(dtColorCode, "Type in('Normal','Absent','Holiday','WeekOff','Leave')", "", DataViewRowState.CurrentRows).ToTable();

        TableRow tblHeader = new TableRow();
        TableRow tblColor = new TableRow();
        TableRow TblShortCode = new TableRow();
        TableRow TblColorCode = new TableRow();
        for (int rowcounter = 0; rowcounter < dtColorCode.Rows.Count; rowcounter++)
        {
            TableCell tch = new TableCell();
            TableCell tcc = new TableCell();
            TableCell tcsh = new TableCell();
            TableCell tccode = new TableCell();

            if (dtColorCode.Rows[rowcounter]["Type"].ToString() == "Normal")
            {
                dtColorCode.Rows[rowcounter]["Type"] = "Present";

              
                dtColorCode.Rows[rowcounter]["ShortId"] = "P";
            }
            if (dtColorCode.Rows[rowcounter]["Type"].ToString() == "Leave")
            {
                dtColorCode.Rows[rowcounter]["ShortId"] = "L";


            }
            if (Session["lang"].ToString() == "1")
            {
                tch.Text = dtColorCode.Rows[rowcounter]["Type"].ToString();

            }
           






            tcc.BackColor = System.Drawing.ColorTranslator.FromHtml(hextoint(dtColorCode.Rows[rowcounter]["ColorCode"].ToString()).ToString());
            tcc.Height = 25;
            tcsh.Text = dtColorCode.Rows[rowcounter]["ShortId"].ToString();
            tccode.Text = "#" + dtColorCode.Rows[rowcounter]["ColorCode"].ToString();
            tblColor.Cells.Add(tcc);
            tblHeader.Cells.Add(tch);
            tcsh.HorizontalAlign = HorizontalAlign.Center;
            TblShortCode.Cells.Add(tcsh);
            TblColorCode.Cells.Add(tccode);
        }
        tblColorCode.Rows.Add(tblHeader);
        tblColorCode.Rows.Add(tblColor);
        tblColorCode.Rows.Add(TblShortCode);
        tblColorCode.Rows.Add(TblColorCode);
        tblColorCode.BorderWidth = 1;

    }


    public int hextoint(string hexValue)
    {
        return int.Parse(hexValue, NumberStyles.AllowHexSpecifier);
    }


    public void GetReport()
    {
        if (Session["SelectedEmpId"]==null)
        {
            return;
        }

        Table1.Rows.Clear();
      
            DataTable dtShortCode = (DataTable)Session["DtShortCode"];
          

            //set color and short code
            string Normal = "P";
             string Absent = (new DataView(dtShortCode, "Type='Absent'", "", DataViewRowState.CurrentRows).ToTable()).Rows[0]["ShortId"].ToString();
            
      
            string WeekOff = (new DataView(dtShortCode, "Type='WeekOff'", "", DataViewRowState.CurrentRows).ToTable()).Rows[0]["ShortId"].ToString();
            string Holiday = (new DataView(dtShortCode, "Type='Holiday'", "", DataViewRowState.CurrentRows).ToTable()).Rows[0]["ShortId"].ToString();
            string Leave = "L";
            string Normalcol = (new DataView(dtShortCode, "Type='Normal'", "", DataViewRowState.CurrentRows).ToTable()).Rows[0]["ColorCOde"].ToString();
             string Absentcol = (new DataView(dtShortCode, "Type='Absent'", "", DataViewRowState.CurrentRows).ToTable()).Rows[0]["ColorCOde"].ToString();
            
            string Leavecol = (new DataView(dtShortCode, "Type='Leave'", "", DataViewRowState.CurrentRows).ToTable()).Rows[0]["ColorCOde"].ToString();
            string WeekOffcol = (new DataView(dtShortCode, "Type='WeekOff'", "", DataViewRowState.CurrentRows).ToTable()).Rows[0]["ColorCOde"].ToString();
            string Holidaycol = (new DataView(dtShortCode, "Type='Holiday'", "", DataViewRowState.CurrentRows).ToTable()).Rows[0]["ColorCOde"].ToString();

        DateTime dtFrom = new DateTime(Convert.ToInt32(txtYear.Text), Convert.ToInt32(ddlMonth.SelectedValue), 1);
            DateTime InitDtFrom = Convert.ToDateTime(dtFrom.ToString());
            int totalDays = DateTime.DaysInMonth(Convert.ToInt32(txtYear.Text), Convert.ToInt32(ddlMonth.SelectedValue.ToString()));
            DateTime dtTo = dtFrom.AddDays(totalDays - 1);
            DateTime dtFromBK = dtFrom;
            AttendanceDataSet rptdata = new AttendanceDataSet();
            rptdata.EnforceConstraints = false;
            AttendanceDataSetTableAdapters.sp_Att_AttendanceRegister_ReportTableAdapter adp = new AttendanceDataSetTableAdapters.sp_Att_AttendanceRegister_ReportTableAdapter();
            adp.Fill(rptdata.sp_Att_AttendanceRegister_Report, Convert.ToDateTime(dtFrom.ToString()), Convert.ToDateTime(dtTo.ToString()));

            DataTable dtFilter = new DataTable();
            if (Session["SelectedEmpId"].ToString() != "")
            {
                dtFilter = new DataView(rptdata.sp_Att_AttendanceRegister_Report, "Emp_Id in (" + Session["SelectedEmpId"].ToString().Substring(0, Session["SelectedEmpId"].ToString().Length - 1) + ") ", "", DataViewRowState.CurrentRows).ToTable();
            }
            else
            {
                rptdata.sp_Att_AttendanceRegister_Report.Rows.Clear();
            }


            TableRow tr = new TableRow();
            TableRow trWeekDay = new TableRow();
            trWeekDay.Cells.Add(new TableCell());
            trWeekDay.Cells.Add(new TableCell());
            trWeekDay.Cells.Add(new TableCell());
            TableCell tcSNO = new TableCell();
            tcSNO.Text = "SNO";
            tr.Cells.Add(tcSNO);
            TableCell tcId = new TableCell();
            tcId.Wrap = false;
            tcId.Text = "ID";

            tr.Cells.Add(tcId);
            TableCell tcName = new TableCell();
            tcName.Wrap = false;
            tcName.Text = "Name";

            tr.Cells.Add(tcName);
            int count = 1;

            while (count <= totalDays)
            {
                TableCell tcDay = new TableCell();
                TableCell tcWeekDay = new TableCell();

                tcDay.Text = count.ToString();
                tcWeekDay.Text = dtFrom.AddDays(count - 1).DayOfWeek.ToString().Substring(0, 2).ToUpper();
                tr.Cells.Add(tcDay);
                trWeekDay.Cells.Add(tcWeekDay);
                count++;
            }
            TableCell tcPresent = new TableCell();
            tcPresent.RowSpan = 2;

            tcPresent.Text = "Present/Total";
            tr.Cells.Add(tcPresent);

            Table1.Rows.Add(tr);
            Table1.Rows.Add(trWeekDay);

            DataTable dtEmpList = dtFilter.DefaultView.ToTable(true, "Emp_Id");
         int empCounter = 0;
         if (dtEmpList.Rows.Count > 0)
         {

             while (empCounter < dtEmpList.Rows.Count)
             {

                 DataTable dtAttbyEmpId = new DataView(dtFilter, "Emp_Id = '" + dtEmpList.Rows[empCounter][0].ToString() + "'", "Att_Date", DataViewRowState.CurrentRows).ToTable();

                 DateTime dtFromTemp = InitDtFrom;
                 DateTime dtToTemp = dtTo;
                 int maxshift = 0;
                 while (dtFromTemp < dtToTemp)
                 {

                     dtFromTemp = dtFromTemp.AddDays(1);
                     DataTable dtTempDateRecordEmp = new DataView(dtAttbyEmpId, "Att_Date = '" + dtFromTemp.ToString("dd-MMM-yyyy") + "'", "Att_Date", DataViewRowState.CurrentRows).ToTable();

                     if (maxshift < dtTempDateRecordEmp.Rows.Count)
                     {
                         maxshift = dtTempDateRecordEmp.Rows.Count;
                     }
                 }

                 TableRow[] trEmp = new TableRow[maxshift];
                 int[] absent = new int[maxshift];
                 int[] present = new int[maxshift];
                 int[] early = new int[maxshift];
                 int[] late = new int[maxshift];
                 int[] total = new int[maxshift];


                 try
                 {
                     present[0] = 0;
                     early[0] = 0;
                     late[0] = 0;
                     total[0] = 0;
                 }
                 catch
                 {

                 }
                 int tempcounter = 0;

                 while (tempcounter < maxshift)
                 {
                     trEmp[tempcounter] = new TableRow();
                     tempcounter++;
                 }

                 TableCell tcSNOEmp = new TableCell();
                 tcSNOEmp.Text = (empCounter + 1).ToString();
                 if (maxshift > 0)
                 {

                     trEmp[0].Cells.Add(tcSNOEmp);



                     TableCell tcNameEmp1 = new TableCell();
                     tcNameEmp1.Text = dtAttbyEmpId.Rows[1]["Emp_Code"].ToString();

                     trEmp[0].Cells.Add(tcNameEmp1);





                     TableCell tcNameEmp = new TableCell();
                     tcNameEmp.Text = dtAttbyEmpId.Rows[0]["Emp_Name"].ToString();

                     trEmp[0].Cells.Add(tcNameEmp);




                     dtFrom = dtFromBK;
                 }
                 int presentcount = 0;
                 while (dtFrom <= dtTo)
                    {

                     int shiftCounter = 0;

                     DataTable dtTempDateRecordEmp = new DataView(dtAttbyEmpId, "Att_Date = '" + dtFrom.ToString("dd-MMM-yyyy") + "'", "Att_Date", DataViewRowState.CurrentRows).ToTable();

                     
                     while (shiftCounter < maxshift)
                     {
                         TableCell tcDay = new TableCell();
                         string attEmp = string.Empty;

                         if (shiftCounter < dtTempDateRecordEmp.Rows.Count)
                         {                        




                                 
                                     if ((Convert.ToBoolean(dtTempDateRecordEmp.Rows[shiftCounter]["Is_Week_Off"].ToString())))
                                     {
                                         attEmp = attEmp + WeekOff;
                                         tcDay.BackColor = System.Drawing.ColorTranslator.FromHtml(hextoint(WeekOffcol).ToString());
                                     }
                                     else if ((Convert.ToBoolean(dtTempDateRecordEmp.Rows[shiftCounter]["Is_Holiday"].ToString())))
                                     {
                                         attEmp = attEmp + Holiday;
                                         tcDay.BackColor = System.Drawing.ColorTranslator.FromHtml(hextoint(Holidaycol).ToString());
                                     }


                                     else if (Convert.ToBoolean(dtTempDateRecordEmp.Rows[shiftCounter]["Is_Leave"].ToString()))
                                     {
                                         attEmp = attEmp + Leave;
                                         tcDay.BackColor = System.Drawing.ColorTranslator.FromHtml(hextoint(Leavecol).ToString());
                                     }
                                     else if (Convert.ToBoolean(dtTempDateRecordEmp.Rows[shiftCounter]["Is_Absent"].ToString()))
                                     {
                                         attEmp = attEmp + Absent;
                                         tcDay.BackColor = System.Drawing.ColorTranslator.FromHtml(hextoint(Absentcol).ToString());
                                     }
                                     else
                                     {
                                         attEmp = attEmp + Normal;

                                         tcDay.BackColor = System.Drawing.ColorTranslator.FromHtml(hextoint(Normalcol).ToString());
                                         presentcount++;
                                     }
                                     absent[shiftCounter] = absent[shiftCounter] + 1;
                               

                               
                             





                         }
                         else
                         {
                             attEmp = "-";
                         }


                         tcDay.Text = attEmp;
                         trEmp[shiftCounter].Cells.Add(tcDay);
                         trEmp[shiftCounter].Cells.Add(tcDay);

                         if (dtFrom == dtTo)
                         {
                             TableCell tcabsnt = new TableCell();
                             tcabsnt.Text = presentcount.ToString() + "/" + totalDays.ToString();
                             trEmp[shiftCounter].Cells.Add(tcabsnt);




                         }



                         //

                         shiftCounter = shiftCounter + 1;




                     }


                     dtFrom = dtFrom.AddDays(1);

                 }



                 for (int maxcounter = 1; maxcounter <= maxshift; maxcounter++)
                 {
                     if (maxcounter < maxshift)
                     {
                         trEmp[maxcounter].Cells.AddAt(0, new TableCell());
                         trEmp[maxcounter].Cells.AddAt(0, new TableCell());
                         trEmp[maxcounter].Cells.AddAt(0, new TableCell());

                     }

                     Table1.Rows.Add(trEmp[maxcounter - 1]);
                 }




                 empCounter++;
             }

         }
         lblmonthname.Text = "Month " + ": " + ddlMonth.SelectedItem.Text;
    
    }
    protected void btnExportPdf_Command(object sender, CommandEventArgs e)
    {
        GridView gv = ((GridView)((ImageButton)sender).FindControl("gvRpt"));

        if (e.CommandArgument.ToString() == "1")
        {
            btnGenerate_Click(null, null);
            Response.ContentType = "application/pdf";
            Response.AddHeader("content-disposition", "attachment;filename=AttendanceRegister.pdf");
            Response.Cache.SetCacheability(HttpCacheability.NoCache);
            StringWriter sw = new StringWriter();
            HtmlTextWriter hw = new HtmlTextWriter(sw);
            Panel11.RenderControl(hw);
            StringReader sr = new StringReader(sw.ToString());
            Document pdfDoc = new Document(PageSize.A2, 0f, 0f, 10f, 0f);
            HTMLWorker htmlparser = new HTMLWorker(pdfDoc);
            PdfWriter.GetInstance(pdfDoc, Response.OutputStream);
            pdfDoc.Open();
            htmlparser.Parse(sr);
            pdfDoc.Close();
            Response.Write(pdfDoc);
            Response.End();

        }
        else if (e.CommandArgument.ToString() == "2")
        {
            btnGenerate_Click(null, null);
            btnExportPdf.Visible = false;
            btnExportToExcel.Visible = false;
            Panel11.OpenInBrowser = true;
           Panel11.ExportType = ControlFreak.ExportPanel.AppType.Excel;


          

        }
    }
    protected void lnkback_Click(object sender, EventArgs e)
    {
        pnlReport.Visible = false;
        pnlEmpAtt.Visible = true;
        btnReset_Click(null,null);
        Session["SelectedEmpId"] = null;
    }
    protected void lnkChangeFilter_Click(object sender, EventArgs e)
    {




        txtYear.Text = "";
        ddlMonth.SelectedIndex = 0;
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
        txtYear.Text = "";
        ddlMonth.SelectedIndex = 0;
      
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
