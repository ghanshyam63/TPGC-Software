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

public partial class Attendance_ReportMail : BasePage
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

    Ser_ReportMaster objReportMst = new Ser_ReportMaster();
    Ser_ReportType objReportType = new Ser_ReportType();
    Ser_ReportEmployee objReportEmp = new Ser_ReportEmployee();

    protected void Page_Load(object sender, EventArgs e)
    {

        if (Session["UserId"] == null)
        {
            Response.Redirect("~/ERPLogin.aspx");
        }
        Page.Title = objSys.GetSysTitle();
        if (!IsPostBack)
        {

            pnlEmpAtt.Visible = true;
            Session["ReportTransId"] = null;
            FillGrid();
            rbtnEmpSal.Checked = true;
            rbtnGroupSal.Checked = false;
            EmpGroupSal_CheckedChanged(null, null);
            pnlEmpNf.Visible = false;
            pnlReportType.Visible = false;
        }
        Session["AccordianId"] = "9";
        Session["HeaderText"] = "Attendance Setup";

    }

    public void FillGrid()
    {
        DataTable dtEmp = objEmp.GetEmployeeMasterForReport(Session["CompId"].ToString());

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



    public void FillGrid1()
    {
        DataTable dtEmp = objEmp.GetEmployeeMaster(Session["CompId"].ToString());

        dtEmp = new DataView(dtEmp, "Brand_Id='" + Session["BrandId"].ToString() + "'  and Location_Id='" + Session["LocId"].ToString() + "'", "", DataViewRowState.CurrentRows).ToTable();

        if (Session["SessionDepId"] != null)
        {
            dtEmp = new DataView(dtEmp, "Department_Id in(" + Session["SessionDepId"].ToString().Substring(0, Session["SessionDepId"].ToString().Length - 1) + ")", "", DataViewRowState.CurrentRows).ToTable();

        }
        if (dtEmp.Rows.Count > 0)
        {
            Session["dtEmpR"] = dtEmp;
            gvEmpNF.DataSource = dtEmp;
            gvEmpNF.DataBind();
            lblTotalRecordNF.Text = Resources.Attendance.Total_Records + " : " + dtEmp.Rows.Count.ToString() + "";

        }
    }



      protected void gvEmployee_OnRowDataBound(object sender, GridViewRowEventArgs e)
    {

        if (e.Row.RowType == DataControlRowType.DataRow)
        {

            string s = gvEmployee.DataKeys[e.Row.RowIndex]["Trans_Id"].ToString();

            if(s=="")
            {
                ImageButton img = (ImageButton)e.Row.FindControl("btnEdit");
                img.Visible = false;

            }
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
            DataTable dtEmp = objEmp.GetEmployeeMasterForReport(Session["CompId"].ToString());

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



  




    public double OnMinuteEarlyPenalty(double PerMinSal,string EmpId)
    {
        double sal = 0;

        string Type = string.Empty;
        string Method = string.Empty;
        double Value = 0;
        bool IsEmpEarly = false;
        try
        {
            IsEmpEarly = Convert.ToBoolean(objEmpParam.GetEmployeeParameterByParameterName(EmpId, "Field2"));
        }
        catch
        {

        }


         Method = objAppParam.GetApplicationParameterValueByParamName("Is_Early_Penalty_Method", Session["CompId"].ToString());
        if (IsEmpEarly)
        {
            if (Method == "Salary")
            {
                Type = objAppParam.GetApplicationParameterValueByParamName("Is_Early_Penalty_Salary_Type", Session["CompId"].ToString());
                Value = double.Parse(objAppParam.GetApplicationParameterValueByParamName("Is_Early_Penalty_Salary_Value", Session["CompId"].ToString()));
      
                if (Type == "2")
                {
                    sal = (PerMinSal * Value) / 100;

                }
                else
                {
                    sal = Value / 60;
                }
            }
            else
            {
                sal = PerMinSal;
            }
        }
        return sal;

    }





    protected void btnLogPost_Click(object sender, EventArgs e)
    {

    }


    protected void btnNext_Click(object sender, EventArgs e)
    {
        string empidlist = string.Empty;
       
        if (rbtnEmpSal.Checked)
        {
            empidlist = lblSelectRecord.Text;

            if (empidlist == "")
            {
                DisplayMessage("Select At least One Employee");
                return;
            }
            else
            {
                pnlEmpAtt.Visible = false;
                pnlReportType.Visible = true;
                DataTable dt = GetReportDatatable();
                gvReportType.DataSource = dt;
                gvReportType.DataBind();
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
                        empidlist += str + ",";

                    }
                }

                if (empidlist == "")
                {
                    DisplayMessage("Employees are not exists in selected groups");
                    return;
                }
                else
                {
                    pnlEmpAtt.Visible = false;
                    pnlReportType.Visible = true;
                    DataTable dt = GetReportDatatable();
                    gvReportType.DataSource = dt;
                    gvReportType.DataBind();
                }
            }
            else
            {
                DisplayMessage("Select Group First");
            }

        }
        int b = 0;

        for (int i = 0; i < empidlist.Split(',').Length - 1;i++ )
        {
            objReportMst.DeleteReportMasterByTransId(empidlist.Split(',')[i].ToString());
            b = 0;
           b= objReportMst.InsertReportMaster(empidlist.Split(',')[i].ToString(),Session["CompId"].ToString(),"0","1/1/1900","Mail","", "", "", "", "",false.ToString(),DateTime.Now.ToString(),true.ToString(), Session["UserId"].ToString(), DateTime.Now.ToString(), Session["UserId"].ToString(), DateTime.Now.ToString());

            Session["ReportTransId"]+=b.ToString()+",";
        }


    }



    protected void gvEmpNF_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        gvEmpNF.PageIndex = e.NewPageIndex;
        DataTable dtEmp = (DataTable)Session["dtEmpR"];
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
    
         protected void chkSelAll_CheckedChangedR(object sender, EventArgs e)
    {

             CheckBox ChkAll=(CheckBox)gvReportType.HeaderRow.FindControl("chkSelAll");

        foreach (GridViewRow gvr in gvReportType.Rows)
        { 
            
            CheckBox chk=(CheckBox)gvr.FindControl("chkReportSel");

        if (ChkAll.Checked)
        {
            chk.Checked = true;
             }
            else
            {
                chk.Checked = false;
            }
            
        }

         }

    


        

         protected void btnSave_Click(object sender, EventArgs e)
    {

             if(lblSelectRecordNF.Text=="")
             {
                 DisplayMessage("Please select at least one employee from list");
                 return;
             }
             if(txtDays.Text=="")
             {
                 DisplayMessage("Please enter schedule days");
                 return;
             }

             if (Session["ReportTransId"] != null)
             {

                 string TransIds = string.Empty;
                 TransIds = Session["ReportTransId"].ToString();
                 for (int i = 0; i < TransIds.Split(',').Length - 1; i++)
                 {
                     objReportEmp.DeleteReportEmployeeByTransId(TransIds.Split(',')[i]);

                     string EmpIds=lblSelectRecordNF.Text;
                     for (int j = 0; j < EmpIds.Split(',').Length - 1;j++)
                     {

                         objReportEmp.InsertReportEmployee(TransIds.Split(',')[i], EmpIds.Split(',')[j], "", "", "", "", "", false.ToString(), DateTime.Now.ToString(), true.ToString(), Session["UserId"].ToString(), DateTime.Now.ToString(), Session["UserId"].ToString(), DateTime.Now.ToString());


                     }

                       objReportMst.UpdateReportMaster(TransIds.Split(',')[i],Session["CompId"].ToString(),txtDays.Text,"1/1/1900","Mail",true.ToString(),Session["UserId"].ToString(), DateTime.Now.ToString());

                 }


             }
             DisplayMessage("Record Saved");
             rbtnEmpSal.Checked = true;
             rbtnGroupSal.Checked = false;
             EmpGroupSal_CheckedChanged(null, null);
             btnCancel_Click(null,null);
            
       
         }

        
         protected void btnCancel_Click(object sender, EventArgs e)
    {

        pnlEmpAtt.Visible = true;
        pnlEmpNf.Visible = false;
        pnlReportType.Visible = false;     
       
        lblSelectRecord.Text = "";
        lblSelectRecordNF.Text = "";
        FillGrid();
        txtDays.Text = "";
        Edit.Value = "";
        Session["ReportTransId"] = null;

     }
    protected void btnEmpNext_Click(object sender, EventArgs e)
    {
        bool b=false;
        foreach (GridViewRow gvr in gvReportType.Rows)
        {

            CheckBox chk = (CheckBox)gvr.FindControl("chkReportSel");

            if(chk.Checked)
            {
                b = true;

            }
        }



        if(b==false)
        {
            DisplayMessage("Please Select Report");
            return;


        }


        if (Session["ReportTransId"]!=null)
        {

            string TransIds = string.Empty;
            TransIds = Session["ReportTransId"].ToString();

          
            for (int i = 0; i < TransIds.Split(',').Length - 1;i++)
            {
                objReportType.DeleteReportTypeByTransId(TransIds.Split(',')[i]);



                foreach (GridViewRow gvr in gvReportType.Rows)
                {

                    CheckBox chk = (CheckBox)gvr.FindControl("chkReportSel");
                    Label lblRptName = (Label)gvr.FindControl("lblReportName");
                   
                    if(chk.Checked)
                    {

                        objReportType.InsertReportType(TransIds.Split(',')[i], lblRptName.Text, "", "", "", "", "", false.ToString(), DateTime.Now.ToString(), true.ToString(), Session["UserId"].ToString(), DateTime.Now.ToString(), Session["UserId"].ToString(), DateTime.Now.ToString());

                    }
                                         
                    

                }

                
           
            }
            

        }
        FillGrid1();
        if (Edit.Value != "")
        {
            DataTable dtReport = new DataTable();
            dtReport = objReportMst.GetReportMasterByTransId(Edit.Value);
            if(dtReport.Rows.Count>0)
            {
                txtDays.Text=dtReport.Rows[0]["Schedule_Days"].ToString();

            }


            DataTable dtEmployee = new DataTable();
            dtEmployee = objReportEmp.GetReportEmployeeByTransId(Edit.Value);
            if (dtEmployee.Rows.Count > 0)
            {
                for (int i = 0; i < dtEmployee.Rows.Count; i++)
                {
                    lblSelectRecordNF.Text += dtEmployee.Rows[i]["Emp_Id"].ToString() + ",";

                }

            }

            foreach (GridViewRow gvr in gvEmpNF.Rows)
            {

                CheckBox chk = (CheckBox)gvr.FindControl("chkgvSelect");

                Label lblEmpId = (Label)gvr.FindControl("lblEmpId");

                for (int i = 0; i < lblSelectRecordNF.Text.Split(',').Length - 1; i++)
                {
                    if (lblSelectRecordNF.Text.Split(',')[i] == lblEmpId.Text)
                    {
                        chk.Checked = true;

                    }

                }



            }

        }

        pnlEmpAtt.Visible = false;
        pnlEmpNf.Visible = true;
        pnlReportType.Visible = false;
       
        
      
        
            }

    protected void btnEdit_Command(object sender, CommandEventArgs e)
    {
        Edit.Value=e.CommandArgument.ToString();
        Session["ReportTransId"] = Edit.Value+",";

        pnlEmpAtt.Visible = false;
        pnlReportType.Visible = true;
        DataTable dt = GetReportDatatable();
        gvReportType.DataSource = dt;
        gvReportType.DataBind();


        DataTable dtReport = new DataTable();
        dtReport = objReportType.GetReportTypeByTransId(Edit.Value);
        if(dtReport.Rows.Count > 0)
        {
            for (int i = 0; i < dtReport.Rows.Count;i++)
            {
                for (int j = 0; j < gvReportType.Rows.Count;j++)
                {
                      CheckBox chk = (CheckBox)gvReportType.Rows[j].FindControl("chkReportSel");
                    Label lblRptName = (Label)gvReportType.Rows[j].FindControl("lblReportName");
                  

                    if(dtReport.Rows[i]["Report_Name"].ToString()==lblRptName.Text)
                    {
                        chk.Checked = true;

                    }


                }
            }

        }



    }
    public DataTable GetReportDatatable()
    {
        DataTable dt = new DataTable();
        dt.Columns.Add("ReportName");

        DataRow dr1 = dt.NewRow();
        dr1["ReportName"]="Late In Report";
        dt.Rows.Add(dr1);

        DataRow dr2 = dt.NewRow();
        dr2["ReportName"] = "Early Out Report";
        dt.Rows.Add(dr2);
        DataRow dri = dt.NewRow();
        dri["ReportName"] = "In Out Report";
        dt.Rows.Add(dri);
        
        DataRow drl = dt.NewRow();
        drl["ReportName"] = "Log Report";
        dt.Rows.Add(drl);

        DataRow dra = dt.NewRow();
        dra["ReportName"] = "Absent Report";
        dt.Rows.Add(dra);

        DataRow drd = dt.NewRow();
        drd["ReportName"] = "Document Expiration Report";
        dt.Rows.Add(drd);

        return dt;

    }

    protected void btnBackToReport_Click(object sender, EventArgs e)
    {
        DataTable dt=GetReportDatatable();
        gvReportType.DataSource = dt;
        gvReportType.DataBind();
        gvEmpNF.DataSource = null;
        gvEmpNF.DataBind();

        pnlReportType.Visible = true;
        pnlEmpNf.Visible = false;
        lblSelectRecordNF.Text = "";
       
}

    protected void btnBackToEmp_Click(object sender, EventArgs e)
    {
        pnlReportType.Visible = false ;
        pnlEmpAtt.Visible = true;
       FillGrid();
       lblSelectRecord.Text = "";
       Edit.Value = "";
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
    protected void ImgbtnSelectAll_ClickNF(object sender, ImageClickEventArgs e)
    {
        DataTable dtProduct = (DataTable)Session["dtEmpR"];

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
            DataTable dtProduct1 = (DataTable)Session["dtEmpR"];
            gvEmpNF.DataSource = dtProduct1;
            gvEmpNF.DataBind();
            ViewState["Select"] = null;
        }
    }
    protected void btnNFRefresh_Click(object sender, ImageClickEventArgs e)
    {
        lblSelectRecordNF.Text = "";
        ddlFieldNF.SelectedIndex = 1;
        ddlOptionNF.SelectedIndex = 2;
        txtValueNF.Text = "";

        DataTable dtEmp = (DataTable)Session["dtEmpR"];
        gvEmpNF.DataSource = dtEmp;
        gvEmpNF.DataBind();
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
            else if (ddlOptionNF.SelectedIndex == 2)
            {
                condition = "convert(" + ddlFieldNF.SelectedValue + ",System.String) like '%" + txtValueNF.Text.Trim() + "%'";
            }
            else
            {
                condition = "convert(" + ddlFieldNF.SelectedValue + ",System.String) Like '" + txtValueNF.Text.Trim() + "%'";

            }
            DataTable dtEmp = (DataTable)Session["dtEmpR"];
            DataView view = new DataView(dtEmp, condition, "", DataViewRowState.CurrentRows);
            gvEmpNF.DataSource = view.ToTable();
            gvEmpNF.DataBind();
            lblTotalRecordNF.Text = Resources.Attendance.Total_Records + " : " + (view.ToTable()).Rows.Count.ToString() + "";

        }

    }
    public void DisplayMessage(string str)
    {
        ScriptManager.RegisterStartupScript(this, GetType(), "", "alert('" + str + "');", true);
    }
    protected void btnReset_Click(object sender, EventArgs e)
    {
       
        
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
