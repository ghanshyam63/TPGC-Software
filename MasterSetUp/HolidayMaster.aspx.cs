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

public partial class MasterSetUp_HolidayMaster : BasePage
{
   
    Common cmn = new Common();
    SystemParameter objSys = new SystemParameter();
    HolidayMaster objHoliday = new HolidayMaster();
    Set_EmployeeGroup_Master objEmpGroup = new Set_EmployeeGroup_Master();
    EmployeeMaster objEmp = new EmployeeMaster();
    Set_Group_Employee objGroupEmp = new Set_Group_Employee();
    Set_Holiday_Group objHolidayGroup = new Set_Holiday_Group();
    Set_Employee_Holiday objEmpHoliday = new Set_Employee_Holiday();


    protected void Page_Load(object sender, EventArgs e)
    {
        Session["BrandId"] = "1";
       
        if (Session["UserId"] == null)
        {
            Response.Redirect("~/ERPLogin.aspx");
        }
        AllPageCode();
        if (!IsPostBack)
        {
            txtValue.Focus();
           
            FillGridBin();
            FillGrid();
            
            btnList_Click(null, null);
            pnlHolidayGroup.Visible = false;
        }
        CalendarExtender1.Format = objSys.SetDateFormat();
        CalendarExtender2.Format = objSys.SetDateFormat();
    }

    public string GetDate(object obj)
    {

        DateTime Date = new DateTime();
        Date = Convert.ToDateTime(obj.ToString());

     

        return Date.ToString(objSys.SetDateFormat());

    }

    public void AllPageCode()
    {
        Page.Title = objSys.GetSysTitle();

        Session["AccordianId"] = "8";
        Session["HeaderText"] = "Master Setup";
        DataTable dtAllPageCode = cmn.GetAllPagePermission(Session["UserId"].ToString(), "8", "35");
        if (dtAllPageCode.Rows.Count != 0)
        {
            if (dtAllPageCode.Rows[0][0].ToString() == "SuperAdmin")
            {
                btnSave.Visible = true;
                foreach (GridViewRow Row in gvHolidayMaster.Rows)
                {
                    ((ImageButton)Row.FindControl("btnEdit")).Visible = true;
                    ((ImageButton)Row.FindControl("IbtnDelete")).Visible = true;
                }
                imgBtnRestore.Visible = true;
                ImgbtnSelectAll.Visible = true;
            }
            else
            {
                foreach (DataRow DtRow in dtAllPageCode.Rows)
                {
                    if (Convert.ToBoolean(DtRow["Op_Add"].ToString()))
                    {
                        btnSave.Visible = true;
                    }
                    foreach (GridViewRow Row in gvHolidayMaster.Rows)
                    {
                        if (Convert.ToBoolean(DtRow["Op_Edit"].ToString()))
                        {
                            ((ImageButton)Row.FindControl("btnEdit")).Visible = true;
                        }
                        if (Convert.ToBoolean(DtRow["Op_Delete"].ToString()))
                        {
                            ((ImageButton)Row.FindControl("IbtnDelete")).Visible = true;
                        }
                    }
                    if (Convert.ToBoolean(DtRow["Op_Restore"].ToString()))
                    {
                        imgBtnRestore.Visible = true;
                        ImgbtnSelectAll.Visible = true;
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
    protected void btnList_Click(object sender, EventArgs e)
    {
        txtValue.Focus();
        pnlMenuList.BackColor = System.Drawing.ColorTranslator.FromHtml("#ccddee");

        pnlMenuNew.BackColor = System.Drawing.ColorTranslator.FromHtml("#90bde9");
        pnlMenuBin.BackColor = System.Drawing.ColorTranslator.FromHtml("#90bde9");

        PnlList.Visible = true;
        PnlNewEdit.Visible = false;
        PnlBin.Visible = false;
    }
    protected void btnNew_Click(object sender, EventArgs e)
    {
         txtHolidayName.Focus();
        pnlMenuNew.BackColor = System.Drawing.ColorTranslator.FromHtml("#ccddee");

        pnlMenuList.BackColor = System.Drawing.ColorTranslator.FromHtml("#90bde9");
        pnlMenuBin.BackColor = System.Drawing.ColorTranslator.FromHtml("#90bde9");

        PnlList.Visible = false;
        PnlNewEdit.Visible = true;
        PnlBin.Visible = false;

    }





    protected void txtHolidayName_OnTextChanged(object sender, EventArgs e)
    {

        if (editid.Value == "")
        {
            DataTable dt = objHoliday.GetHolidayMasterByHolidayName(Session["CompId"].ToString().ToString(), txtHolidayName.Text.Trim());
            if (dt.Rows.Count > 0)
            {
                txtHolidayName.Text = "";
                DisplayMessage("Holiday Name Already Exists");
                System.Web.UI.ScriptManager.GetCurrent(this).SetFocus(txtHolidayName);
                return;
            }
            DataTable dt1 = objHoliday.GetHolidayMasterInactive(Session["CompId"].ToString().ToString());
            dt1 = new DataView(dt1, "Holiday_Name='" + txtHolidayName.Text + "'", "", DataViewRowState.CurrentRows).ToTable();
            if (dt1.Rows.Count > 0)
            {
                txtHolidayName.Text = "";
                DisplayMessage("Holiday Name Already Exists - Go to Bin Tab");
                System.Web.UI.ScriptManager.GetCurrent(this).SetFocus(txtHolidayName);
                return;
            }
            txtHolidayNameL.Focus();
        }
        else
        {
            DataTable dtTemp = objHoliday.GetHolidayMasterById(Session["CompId"].ToString().ToString(), editid.Value);
            if (dtTemp.Rows.Count > 0)
            {
                if (dtTemp.Rows[0]["Holiday_Name"].ToString() != txtHolidayName.Text)
                {
                    DataTable dt = objHoliday.GetHolidayMaster(Session["CompId"].ToString().ToString());
                    dt = new DataView(dt, "Holiday_Name='" + txtHolidayName.Text + "'", "", DataViewRowState.CurrentRows).ToTable();
                    if (dt.Rows.Count > 0)
                    {
                        txtHolidayName.Text = "";
                        DisplayMessage("Holiday Name Already Exists");
                        System.Web.UI.ScriptManager.GetCurrent(this).SetFocus(txtHolidayName);
                        return;
                    }
                    DataTable dt1 = objHoliday.GetHolidayMaster(Session["CompId"].ToString().ToString());
                    dt1 = new DataView(dt1, "Holiday_Name='" + txtHolidayName.Text + "'", "", DataViewRowState.CurrentRows).ToTable();
                    if (dt1.Rows.Count > 0)
                    {
                        txtHolidayName.Text = "";
                        DisplayMessage("Holiday Name Already Exists - Go to Bin Tab");
                        System.Web.UI.ScriptManager.GetCurrent(this).SetFocus(txtHolidayName);
                        return;
                    }
                }
            }
            txtHolidayNameL.Focus();
        }
    }
  
    protected void btnBin_Click(object sender, EventArgs e)
    {
        txtbinValue.Focus();
        pnlMenuBin.BackColor = System.Drawing.ColorTranslator.FromHtml("#ccddee");

        pnlMenuNew.BackColor = System.Drawing.ColorTranslator.FromHtml("#90bde9");
        pnlMenuList.BackColor = System.Drawing.ColorTranslator.FromHtml("#90bde9");
        PnlNewEdit.Visible = false;
        PnlBin.Visible = true;
        PnlList.Visible = false;
        FillGridBin();
    }

    protected void btnRefresh_Click(object sender, ImageClickEventArgs e)
    {
        FillGrid();
        FillGridBin();

        ddlOption.SelectedIndex = 2;
        ddlFieldName.SelectedIndex = 0;
        txtValue.Text = "";
    }

    protected void btnbind_Click(object sender, ImageClickEventArgs e)
    {
        if (ddlOption.SelectedIndex != 0)
        {
            string condition = string.Empty;


            if (ddlOption.SelectedIndex == 1)
            {
                condition = "convert(" + ddlFieldName.SelectedValue + ",System.String)='" + txtValue.Text.Trim() + "'";
            }
            else if (ddlOption.SelectedIndex == 2)
            {
                condition = "convert(" + ddlFieldName.SelectedValue + ",System.String) like '%" + txtValue.Text.Trim() + "%'";
            }
            else
            {
                condition = "convert(" + ddlFieldName.SelectedValue + ",System.String) Like '" + txtValue.Text.Trim() + "%'";
            }
            DataTable dtCust = (DataTable)Session["Holiday"];


            DataView view = new DataView(dtCust, condition, "", DataViewRowState.CurrentRows);
            Session["dtFilter"] = view.ToTable();
            lblTotalRecords.Text = Resources.Attendance.Total_Records + ": " + view.ToTable().Rows.Count.ToString() + "";

            gvHolidayMaster.DataSource = view.ToTable();
            gvHolidayMaster.DataBind();
            AllPageCode();


        }


    }
    protected void btnbinbind_Click(object sender, ImageClickEventArgs e)
    {
        if (ddlbinOption.SelectedIndex != 0)
        {
            string condition = string.Empty;


            if (ddlbinOption.SelectedIndex == 1)
            {
                condition = "convert(" + ddlbinFieldName.SelectedValue + ",System.String)='" + txtbinValue.Text.Trim() + "'";
            }
            else if (ddlbinOption.SelectedIndex == 2)
            {
                condition = "convert(" + ddlbinFieldName.SelectedValue + ",System.String) like '%" + txtbinValue.Text + "%'";
            }
            else
            {
                condition = "convert(" + ddlbinFieldName.SelectedValue + ",System.String) Like '" + txtbinValue.Text + "%'";
            }
            DataTable dtCust = (DataTable)Session["dtbinHoliday"];


            DataView view = new DataView(dtCust, condition, "", DataViewRowState.CurrentRows);
            Session["dtbinFilter"] = view.ToTable();
            lblbinTotalRecords.Text = Resources.Attendance.Total_Records + ": " + view.ToTable().Rows.Count.ToString() + "";

            gvHolidayMasterBin.DataSource = view.ToTable();
            gvHolidayMasterBin.DataBind();


            if (view.ToTable().Rows.Count == 0)
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

    protected void btnbinRefresh_Click(object sender, ImageClickEventArgs e)
    {
        FillGrid();
        FillGridBin();

        ddlbinOption.SelectedIndex = 2;
        ddlbinFieldName.SelectedIndex = 0;
        txtbinValue.Text = "";
        lblSelectedRecord.Text = "";
    }

    protected void gvHolidayMasterBin_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {

        gvHolidayMasterBin.PageIndex = e.NewPageIndex;
        if (HDFSortbin.Value == "")
            FillGridBin();
        else
        {
            DataTable dt = (DataTable)Session["dtbinFilter"];
            gvHolidayMasterBin.DataSource = dt;
            gvHolidayMasterBin.DataBind();
            AllPageCode();
        }
        string temp = string.Empty;
        bool isselcted;

        for (int i = 0; i < gvHolidayMasterBin.Rows.Count; i++)
        {
            Label lblconid = (Label)gvHolidayMasterBin.Rows[i].FindControl("lblHolidayId");
            string[] split = lblSelectedRecord.Text.Split(',');

            for (int j = 0; j < lblSelectedRecord.Text.Split(',').Length; j++)
            {
                if (lblSelectedRecord.Text.Split(',')[j] != "")
                {
                    if (lblconid.Text.Trim().ToString() == lblSelectedRecord.Text.Split(',')[j].Trim().ToString())
                    {
                        ((CheckBox)gvHolidayMasterBin.Rows[i].FindControl("chkgvSelect")).Checked = true;
                    }
                }
            }
        }

    }
    protected void gvHolidayMasterBin_OnSorting(object sender, GridViewSortEventArgs e)
    {
        lblSelectedRecord.Text = "";
        HDFSortbin.Value = HDFSortbin.Value == "ASC" ? "DESC" : "ASC";
        DataTable dt = new DataTable();
        dt = objHoliday.GetHolidayMasterInactive(Session["CompId"].ToString());
        DataView dv = new DataView(dt);
        string Query = "" + e.SortExpression + " " + HDFSortbin.Value + "";
        dv.Sort = Query;
        dt = dv.ToTable();
        Session["dtbinFilter"] = dt;
        gvHolidayMasterBin.DataSource = dt;
        gvHolidayMasterBin.DataBind();
        AllPageCode();

    }
    

   
    protected void btnSave_Click(object sender, EventArgs e)
    {
        int b = 0;

      

        if (txtHolidayName.Text == "")
        {
            DisplayMessage("Enter Holiday Name");
            txtHolidayName.Focus();
            return;
        }

        if (txtFromDate.Text == "")
        {
            DisplayMessage("Enter From Date");
            txtFromDate.Focus();
            return;

        }
        else
        {
            try
            {
                Convert.ToDateTime(txtFromDate.Text);
            }
            catch
            {
                DisplayMessage("Enter Correct From Date Format dd-MMM-yyyy");
                txtFromDate.Focus();
                return;

            }

        }
        if(Convert.ToDateTime(txtFromDate.Text) > Convert.ToDateTime(txtToDate.Text))
        {
            DisplayMessage("From Date cannot be greater than To Date");
            txtFromDate.Text = "";
            txtToDate.Text = "";
            txtFromDate.Focus();
            
            return;

        }

        if (txtToDate.Text == "")
        {
            DisplayMessage("Enter To Date");
            txtToDate.Focus();
            return;

        }
        else
        {
            try
            {
                Convert.ToDateTime(txtToDate.Text);
            }
            catch
            {
                DisplayMessage("Enter Correct To Date Format dd-MMM-yyyy");
                txtToDate.Focus();
                return;

            }

        }


        if (editid.Value == "")
        {
            
            DataTable dt1 = objHoliday.GetHolidayMaster(Session["CompId"].ToString());

            dt1 = new DataView(dt1, "Holiday_Name='" + txtHolidayName.Text + "'", "", DataViewRowState.CurrentRows).ToTable();
            if (dt1.Rows.Count > 0)
            {
                DisplayMessage("Holiday Name Already Exists");
                txtHolidayName.Focus();
                return;

            }




            b = objHoliday.InsertHolidayMaster(Session["CompId"].ToString(), txtHolidayName.Text, txtHolidayNameL.Text,Session["BrandId"].ToString(),txtFromDate.Text,txtToDate.Text,"", "", "", "", "", true.ToString(), DateTime.Now.ToString(), true.ToString(), Session["UserId"].ToString(), DateTime.Now.ToString(), Session["UserId"].ToString(), DateTime.Now.ToString());
            if (b != 0)
            {
                DisplayMessage("Record Saved");
                FillGrid();
                editid.Value = b.ToString();
                pnlHoliday.Visible = false;
                pnlHolidayGroup.Visible = true;
                rbtnEmp.Checked = true;
                EmpGroup_CheckedChanged(null, null);
            }
            else
            {
                DisplayMessage("Record Not Saved");
            }
        }
        else
        {
            string HolidayTypeName = string.Empty;
            DataTable dt1 = objHoliday.GetHolidayMaster(Session["CompId"].ToString());
            try
            {
                HolidayTypeName = new DataView(dt1, "Holiday_Id='" + editid.Value + "'", "", DataViewRowState.CurrentRows).ToTable().Rows[0]["Holiday_Name"].ToString();
            }
            catch
            {
                HolidayTypeName = "";
            }
            dt1 = new DataView(dt1, "Holiday_Name='" + txtHolidayName.Text + "' and Holiday_Name<>'"+HolidayTypeName+"'", "", DataViewRowState.CurrentRows).ToTable();
            if (dt1.Rows.Count > 0)
            {
                DisplayMessage("Holiday Name Already Exists");
                txtHolidayName.Focus();
                return;

            }
            b = objHoliday.UpdateHolidayMaster(editid.Value, Session["CompId"].ToString(), txtHolidayName.Text, txtHolidayNameL.Text, Session["BrandId"].ToString(), txtFromDate.Text, txtToDate.Text, "", "", "", "", "", true.ToString(), DateTime.Now.ToString(), true.ToString(), Session["UserId"].ToString(), DateTime.Now.ToString());

            if (b != 0)
            {
               
                DisplayMessage("Record Updated");
             
                FillGrid();
                pnlHoliday.Visible = false;
                pnlHolidayGroup.Visible = true;
                rbtnEmp.Checked = true;
                EmpGroup_CheckedChanged(null,null);
                
            }
            else
            {
                DisplayMessage("Record Not Updated");
            }

        }
    }


    
    protected void ImgbtnSelectAll_Click1(object sender, ImageClickEventArgs e)
    {
        DataTable dtGroup = (DataTable)Session["dtEmp"];

        if (ViewState["Select"] == null)
        {
            ViewState["Select"] = 1;
            foreach (DataRow dr in dtGroup.Rows)
            {
                if (!lblEmp.Text.Split(',').Contains(dr["Emp_Id"]))
                {
                    lblEmp.Text += dr["Emp_Id"] + ",";
                }
            }
            for (int i = 0; i < gvEmployee.Rows.Count; i++)
            {
                string[] split = lblEmp.Text.Split(',');
                Label lblconid = (Label)gvEmployee.Rows[i].FindControl("lblEmpId");
                for (int j = 0; j < lblEmp.Text.Split(',').Length; j++)
                {
                    if (lblEmp.Text.Split(',')[j] != "")
                    {
                        if (lblconid.Text.Trim().ToString() == lblEmp.Text.Split(',')[j].Trim().ToString())
                        {
                            ((CheckBox)gvEmployee.Rows[i].FindControl("chkgvSelect")).Checked = true;
                        }
                    }
                }
            }
        }
        else
        {
            lblEmp.Text = "";
            DataTable dtGroup1 = (DataTable)Session["dtEmp"];
            gvEmployee.DataSource = dtGroup1;
            gvEmployee.DataBind();
            ViewState["Select"] = null;
        }



    }
    protected void chkgvSelect_CheckedChanged1(object sender, EventArgs e)
    {
        string empidlist = string.Empty;
        string temp = string.Empty;
        int index = ((GridViewRow)((CheckBox)sender).Parent.Parent).RowIndex;
        Label lb = (Label)gvEmployee.Rows[index].FindControl("lblEmpId");
        if (((CheckBox)gvEmployee.Rows[index].FindControl("chkgvSelect")).Checked)
        {
            empidlist += lb.Text.Trim().ToString() + ",";

            if (!lblEmp.Text.Split(',').Contains(lb.Text))
            {
                lblEmp.Text += empidlist;
            }
        }

        else
        {

            empidlist += lb.Text.ToString().Trim();
            lblEmp.Text += empidlist;
            string[] split = lblEmp.Text.Split(',');
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
            lblEmp.Text = temp;
        }
    }

    protected void chkgvSelectAll_CheckedChanged1(object sender, EventArgs e)
    {
        CheckBox chkSelAll = ((CheckBox)gvEmployee.HeaderRow.FindControl("chkgvSelectAll"));
        for (int i = 0; i < gvEmployee.Rows.Count; i++)
        {
            ((CheckBox)gvEmployee.Rows[i].FindControl("chkgvSelect")).Checked = chkSelAll.Checked;
            if (chkSelAll.Checked)
            {
                if (!lblEmp.Text.Split(',').Contains(((Label)(gvEmployee.Rows[i].FindControl("lblEmpId"))).Text.Trim().ToString()))
                {
                    lblEmp.Text += ((Label)(gvEmployee.Rows[i].FindControl("lblEmpId"))).Text.Trim().ToString() + ",";
                }
            }
            else
            {
                string temp = string.Empty;
                string[] split = lblEmp.Text.Split(',');
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
                lblEmp.Text = temp;
            }
        }
    }

    protected void btnEmpRefresh_Click(object sender, ImageClickEventArgs e)
    {


        txtVal1.Text = "";
        ddlFieldName1.SelectedIndex = 1;
        ddlOption1.SelectedIndex = 2;



        EmpGroup_CheckedChanged(null, null);
    }
    protected void btnbindEmp_Click(object sender, EventArgs e)
    {
        string condition = string.Empty;
        if (ddlOption1.SelectedIndex != 0)
        {


            if (ddlOption1.SelectedIndex == 1)
            {
                condition = "convert(" + ddlFieldName1.SelectedValue + ",System.String)='" + txtVal1.Text + "'";
            }
            else if (ddlOption1.SelectedIndex == 2)
            {
                condition = "convert(" + ddlFieldName1.SelectedValue + ",System.String) like '%" + txtVal1.Text + "%'";
            }
            else
            {
                condition = "convert(" + ddlFieldName1.SelectedValue + ",System.String) Like '" + txtVal1.Text + "%'";
            }

            DataTable dtCust = (DataTable)Session["dtEmp"];
            DataView view = new DataView(dtCust, condition, "", DataViewRowState.CurrentRows);
            Session["dtBinFilter"] = view.ToTable();
            lblTotalRecordEmp.Text = Resources.Attendance.Total_Records + " : " + view.ToTable().Rows.Count.ToString() + "";
            gvEmployee.DataSource = view.ToTable();
            gvEmployee.DataBind();

          
            if (view.ToTable().Rows.Count == 0)
            {
                imgBtnRestore.Visible = false;
                ImgbtnSelectAll.Visible = false;
            }
            else
            {
                AllPageCode();
            }
            System.Web.UI.ScriptManager.GetCurrent(this).SetFocus(txtVal1);
        }
    }
    protected void gvEmployee_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        gvEmployee.PageIndex = e.NewPageIndex;
        gvEmployee.DataSource = (DataTable)Session["dtEmp"];
        gvEmployee.DataBind();
        foreach (GridViewRow gvr in gvEmployee.Rows)
        {
            Label lb = (Label)gvr.FindControl("lblEmpId");
            if (lblEmp.Text.Split(',').Contains(lb.Text))
            {
                if (!lblEmp.Text.Split(',').Contains(lb.Text))
                {
                    lblEmp.Text += lb.Text + ",";
                }
                CheckBox chk = (CheckBox)gvr.FindControl("chkgvSelect");
                chk.Checked = true;
            }

        }
    }
    protected void gvEmp_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        gvEmp.PageIndex = e.NewPageIndex;
        gvEmp.DataSource = (DataTable)Session["dtEmp1"];
        gvEmp.DataBind();
    }
    protected void  lbxGroup_SelectedIndexChanged(object sender, EventArgs e)
{
       string GroupIds = string.Empty;
        string EmpIds = string.Empty;
        for (int i = 0; i < lbxGroup.Items.Count;i++ )
        {
            if(lbxGroup.Items[i].Selected==true)
            {
                GroupIds+=lbxGroup.Items[i].Value+",";

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
                gvEmp.DataSource = dtEmp;
                gvEmp.DataBind();

            }
            else
            {

                Session["dtEmp1"] = null;
                gvEmp.DataSource = null;
                gvEmp.DataBind();
            }

        }
        else
        {
            gvEmp.DataSource = null;
            gvEmp.DataBind();

        }
    }

    protected void EmpGroup_CheckedChanged(object sender, EventArgs e)
    {
        if (rbtnGroup.Checked)
        {
            pnlEmp.Visible = false;
            pnlGroup.Visible = true;
            btnDelete.Visible = true;
            DataTable dtGroup = objEmpGroup.GetEmployeeGroup_Master(Session["CompId"].ToString());
            dtGroup = new DataView(dtGroup, "Brand_Id='" + Session["BrandId"].ToString() + "'  and Location_Id='" + Session["LocId"].ToString() + "'", "", DataViewRowState.CurrentRows).ToTable();
            if (dtGroup.Rows.Count > 0)
            {
                lbxGroup.DataSource = dtGroup;
                lbxGroup.DataTextField = "Group_Name";
                lbxGroup.DataValueField = "Group_Id";

                lbxGroup.DataBind();

            }

            DataTable dtHolidayGroup = objHolidayGroup.GetHolidayGroupByHolidayId(Session["CompId"].ToString(),editid.Value);

            if(dtHolidayGroup.Rows.Count > 0)
            {
                for (int i = 0; i < lbxGroup.Items.Count;i++)
                {
                   DataTable   dt= new DataView(dtHolidayGroup,"Group_Id='"+lbxGroup.Items[i].Value+"'","",DataViewRowState.CurrentRows).ToTable();

                    if(dt.Rows.Count > 0)
                    {
                        lbxGroup.Items[i].Selected=true;

                    }

                }

            }

            lbxGroup_SelectedIndexChanged(null,null);
        }
        else if (rbtnEmp.Checked)
        {
            pnlEmp.Visible = true;
            pnlGroup.Visible = false;
            btnDelete.Visible = false;
            lblEmp.Text = "";
            string GroupIds = string.Empty;
            string EmpIds = string.Empty;
            DataTable dtEmp = objEmp.GetEmployeeMaster(Session["CompId"].ToString());

            dtEmp = new DataView(dtEmp, "Brand_Id='" + Session["BrandId"].ToString() + "'  and Location_Id='" + Session["LocId"].ToString() + "'", "", DataViewRowState.CurrentRows).ToTable();
         
            

            
          //  dtEmpInGroup = new DataView(dtEmpInGroup, "Group_Id in(" + GroupIds.Substring(0, GroupIds.Length - 1) + ")", "", DataViewRowState.CurrentRows).ToTable();

            //for (int i = 0; i < dtEmpInGroup.Rows.Count; i++)
            //{
            //    if (!EmpIds.Split(',').Contains(dtEmpInGroup.Rows[i]["Emp_Id"].ToString()))
            //    {
            //        EmpIds += dtEmpInGroup.Rows[i]["Emp_Id"].ToString() + ",";
            //    }
            //}
            //dtEmp = new DataView(dtEmp, "Emp_Id in(" + EmpIds.Substring(0, EmpIds.Length - 1) + ")", "", DataViewRowState.CurrentRows).ToTable();

            if (dtEmp.Rows.Count > 0)
            {
                Session["dtEmp"] = dtEmp;
                gvEmployee.DataSource = dtEmp;
                gvEmployee.DataBind();
                lblTotalRecordEmp.Text = Resources.Attendance.Total_Records + " : " + dtEmp.Rows.Count.ToString() + "";
           


                DataTable dtEmpInGrp = objEmpHoliday.GetEmployeeHolidayMasterById(Session["CompId"].ToString(),editid.Value);
               
              

                if(dtEmpInGrp.Rows.Count > 0)
                {
                    for (int i = 0; i < dtEmpInGrp.Rows.Count;i++ )
                    {
                        lblEmp.Text += dtEmpInGrp.Rows[i]["Emp_Id"].ToString() + ",";
                    }
                }




                foreach (GridViewRow gvr in gvEmployee.Rows)
                {
                    Label lb = (Label)gvr.FindControl("lblEmpId");
                    if (lblEmp.Text.Split(',').Contains(lb.Text))
                    {
                        if (!lblEmp.Text.Split(',').Contains(lb.Text))
                        {
                            lblEmp.Text += lb.Text + ",";
                        }
                        CheckBox chk = (CheckBox)gvr.FindControl("chkgvSelect");
                        chk.Checked = true;
                    }

                }



            }

        }


    }



    protected void btnDeleteHoliday_Click(object sender, EventArgs e)
    {
        string SaveGroupIds = string.Empty;
        string EmpIds = string.Empty;

        for (int i = 0; i < lbxGroup.Items.Count; i++)
        {
            if (lbxGroup.Items[i].Selected)
            {
                objHolidayGroup.DeleteHolidayGroupMaster(Session["CompId"].ToString(),editid.Value);

                SaveGroupIds += lbxGroup.Items[i].Value + ",";
                
            }

        }
        DataTable dtEmpInGroup = objGroupEmp.GetGroup_EmployeeById(Session["CompId"].ToString());

        dtEmpInGroup = new DataView(dtEmpInGroup, "Group_Id in(" + SaveGroupIds.Substring(0, SaveGroupIds.Length - 1) + ")", "", DataViewRowState.CurrentRows).ToTable();

        for (int i=0;i<dtEmpInGroup.Rows.Count;i++)
        {
             DateTime FromDate=Convert.ToDateTime(txtFromDate.Text);
                          DateTime ToDate=Convert.ToDateTime(txtToDate.Text);
                        while(FromDate<=ToDate)
                        {
            objEmpHoliday.DeleteEmployeeHolidayMasterByEmpIdandDate(Session["CompId"].ToString(),editid.Value,dtEmpInGroup.Rows[i]["Emp_Id"].ToString(),FromDate.ToString());
                        
                           FromDate = FromDate.AddDays(1);
                        }

        }

        for (int i = 0; i < lbxGroup.Items.Count; i++)
        {
            lbxGroup.Items[i].Selected = false;
           
        }
        DisplayMessage("Record Deleted");

        }
    protected void btnSaveHoliday_Click(object sender, EventArgs e)
    {
        int b =0;
        if (rbtnGroup.Checked)
        {
            string GroupIds = string.Empty;
            string EmpIds = string.Empty;

            string EmpIds1 = string.Empty;
            string SaveGroupIds = string.Empty;

            DataTable dtGroup = objHolidayGroup.GetHolidayGroupByHolidayId(Session["CompId"].ToString(), editid.Value);
            for (int i = 0; i < dtGroup.Rows.Count; i++)
            {

                SaveGroupIds += dtGroup.Rows[i]["Group_Id"].ToString() + ",";
                
            }

            objHolidayGroup.DeleteHolidayGroupMaster(Session["CompId"].ToString(),editid.Value);

            for (int i = 0; i < lbxGroup.Items.Count;i++)
            {
                if(lbxGroup.Items[i].Selected)
                {
                    GroupIds += lbxGroup.Items[i].Value + ",";
                b=objHolidayGroup.InsertHolidayGroupMaster(Session["CompId"].ToString(),editid.Value,lbxGroup.Items[i].Value,"", "", "", "", "", true.ToString(), DateTime.Now.ToString(), true.ToString(), Session["UserId"].ToString(), DateTime.Now.ToString(), Session["UserId"].ToString(), DateTime.Now.ToString());
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
                

                DataTable dtEmp1 = objEmp.GetEmployeeMaster(Session["CompId"].ToString());

                dtEmp1 = new DataView(dtEmp1, "Brand_Id='" + Session["BrandId"].ToString() + "'  and Location_Id='" + Session["LocId"].ToString() + "'", "", DataViewRowState.CurrentRows).ToTable();
                DataTable dtEmpInGroup1 = objGroupEmp.GetGroup_EmployeeById(Session["CompId"].ToString());
                if (SaveGroupIds != "")
                {
                    dtEmpInGroup1 = new DataView(dtEmpInGroup1, "Group_Id in(" + SaveGroupIds.Substring(0, SaveGroupIds.Length - 1) + ")", "", DataViewRowState.CurrentRows).ToTable();



                    for (int i = 0; i < dtEmpInGroup1.Rows.Count; i++)
                    {
                        if (!EmpIds1.Split(',').Contains(dtEmpInGroup1.Rows[i]["Emp_Id"].ToString()))
                        {
                            EmpIds1 += dtEmpInGroup1.Rows[i]["Emp_Id"].ToString() + ",";
                        }
                    }

                }
                foreach(string str in EmpIds1.Split(','))
                {
                    if(str!="")
                    {
                     DateTime FromDate=Convert.ToDateTime(txtFromDate.Text);
                          DateTime ToDate=Convert.ToDateTime(txtToDate.Text);
                        while(FromDate<=ToDate)
                        {
                objEmpHoliday.DeleteEmployeeHolidayMasterByEmpIdandDate(Session["CompId"].ToString(),editid.Value,str,FromDate.ToString());
                              FromDate = FromDate.AddDays(1);
                        }
                    }
            }
                  if (dtEmp.Rows.Count > 0)
                {
                    for (int i = 0; i < dtEmp.Rows.Count;i++)
                    {
                       
                          DateTime FromDate=Convert.ToDateTime(txtFromDate.Text);
                          DateTime ToDate=Convert.ToDateTime(txtToDate.Text);
                        while(FromDate<=ToDate)
                        {
                          
                       b= objEmpHoliday.InsertEmployeeHolidayMaster(Session["CompId"].ToString(),editid.Value,FromDate.ToString(),dtEmp.Rows[i]["Emp_Id"].ToString(), "", "", "", "", "", true.ToString(), DateTime.Now.ToString(), true.ToString(), Session["UserId"].ToString(), DateTime.Now.ToString(), Session["UserId"].ToString(), DateTime.Now.ToString());

                        FromDate = FromDate.AddDays(1);

                        }
                    }
                }

            }

        }
        else if (rbtnEmp.Checked)
        {
            objEmpHoliday.DeleteEmployeeHolidayMaster(Session["CompId"].ToString(),editid.Value);          

            foreach(string str in lblEmp.Text.Split(','))
            {
                if (str != "")
                {
                    DateTime FromDate = Convert.ToDateTime(txtFromDate.Text);
                    DateTime ToDate = Convert.ToDateTime(txtToDate.Text);
                    while (FromDate <= ToDate)
                    {

                        b=objEmpHoliday.InsertEmployeeHolidayMaster(Session["CompId"].ToString(), editid.Value, FromDate.ToString(), str, "", "", "", "", "", true.ToString(), DateTime.Now.ToString(), true.ToString(), Session["UserId"].ToString(), DateTime.Now.ToString(), Session["UserId"].ToString(), DateTime.Now.ToString());

                        FromDate = FromDate.AddDays(1);

                    }
                }
            }

        }
        if(b!=0)
        {
        DisplayMessage("Record Saved");
        Reset();
        btnList_Click(null,null);
        }
     }

    protected void btnCancelHoliday_Click(object sender, EventArgs e)
    {

        pnlHoliday.Visible = true;
        pnlHolidayGroup.Visible = false;
        

     }
    
        
        protected void btnEdit_Command(object sender, CommandEventArgs e)
    {
        editid.Value = e.CommandArgument.ToString();


        DataTable dt = objHoliday.GetHolidayMasterById(Session["CompId"].ToString(), editid.Value);
        if (dt.Rows.Count > 0)
        {
           
            txtHolidayName.Text = dt.Rows[0]["Holiday_Name"].ToString();
            txtHolidayNameL.Text = dt.Rows[0]["Holiday_Name_L"].ToString();


            txtFromDate.Text = Convert.ToDateTime(dt.Rows[0]["From_Date"].ToString()).ToString(objSys.SetDateFormat());
            txtToDate.Text = Convert.ToDateTime(dt.Rows[0]["To_Date"].ToString()).ToString(objSys.SetDateFormat());
           
           

           
           
            btnNew_Click(null, null);
            btnNew.Text = Resources.Attendance.Edit;

        }



    }

    protected void IbtnDelete_Command(object sender, CommandEventArgs e)
    {
        int b = 0;
        b = objHoliday.DeleteHolidayMaster(Session["CompId"].ToString(), e.CommandArgument.ToString(), false.ToString(), Session["UserId"].ToString(), DateTime.Now.ToString());
        if (b != 0)
        {
            DisplayMessage("Record Deleted");

            FillGridBin();
            FillGrid();
            Reset();
        }
        else
        {
            DisplayMessage("Record Not Deleted");
        }
    }
    protected void gvHolidayMaster_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        gvHolidayMaster.PageIndex = e.NewPageIndex;

        DataTable dt = (DataTable)Session["dtFilter"];
        gvHolidayMaster.DataSource = dt;
        gvHolidayMaster.DataBind();
        AllPageCode();

    }
    protected void gvHolidayMaster_OnSorting(object sender, GridViewSortEventArgs e)
    {
        HDFSort.Value = HDFSort.Value == "ASC" ? "DESC" : "ASC";
        DataTable dt = new DataTable();
        dt = (DataTable)Session["dtFilter"];
        DataView dv = new DataView(dt);
        string Query = "" + e.SortExpression + " " + HDFSort.Value + "";
        dv.Sort = Query;
        dt = dv.ToTable();
        Session["dtFilter"] = dt;
        gvHolidayMaster.DataSource = dt;
        gvHolidayMaster.DataBind();
        AllPageCode();
    }

    
    [System.Web.Services.WebMethodAttribute(), System.Web.Script.Services.ScriptMethodAttribute()]
    public static string[] GetCompletionListHolidayName(string prefixText, int count, string contextKey)
    {
        HolidayMaster objHolidayMaster = new HolidayMaster();
        DataTable dt = new DataView(objHolidayMaster.GetHolidayMaster(HttpContext.Current.Session["CompId"].ToString()), "Holiday_Name like '" + prefixText.ToString() + "%'", "", DataViewRowState.CurrentRows).ToTable();
        string[] txt = new string[dt.Rows.Count];

        for (int i = 0; i < dt.Rows.Count; i++)
        {
            txt[i] = dt.Rows[i]["Holiday_Name"].ToString();
        }
        return txt;
    }

   
    public void DisplayMessage(string str)
    {
        ScriptManager.RegisterStartupScript(this, GetType(), "", "alert('" + str + "');", true);
    }
    protected void btnReset_Click(object sender, EventArgs e)
    {
        Reset();
    }
    protected void btnCancel_Click(object sender, EventArgs e)
    {
        FillGrid();
        FillGridBin();
        Reset();
        btnList_Click(null, null);
    }

    public void FillGrid()
    {
        DataTable dt = objHoliday.GetHolidayMaster(Session["CompId"].ToString());
        gvHolidayMaster.DataSource = dt;
        gvHolidayMaster.DataBind();
        AllPageCode();
        Session["dtFilter"] = dt;
        Session["Holiday"] = dt;

        lblTotalRecords.Text = Resources.Attendance.Total_Records + ": " + dt.Rows.Count.ToString() + "";

    }
    public void FillGridBin()
    {
        DataTable dt = new DataTable();
        dt = objHoliday.GetHolidayMasterInactive(Session["CompId"].ToString());

        gvHolidayMasterBin.DataSource = dt;
        gvHolidayMasterBin.DataBind();


        Session["dtbinFilter"] = dt;
        Session["dtbinHoliday"] = dt;
        lblbinTotalRecords.Text = Resources.Attendance.Total_Records + ": " + dt.Rows.Count.ToString() + "";
        if (dt.Rows.Count == 0)
        {
            imgBtnRestore.Visible = false;
            ImgbtnSelectAll.Visible = false;
        }
        else
        {

            AllPageCode();
        }

    }


    protected void chkgvSelectAll_CheckedChanged(object sender, EventArgs e)
    {
        CheckBox chkSelAll = ((CheckBox)gvHolidayMasterBin.HeaderRow.FindControl("chkgvSelectAll"));
        for (int i = 0; i < gvHolidayMasterBin.Rows.Count; i++)
        {
            ((CheckBox)gvHolidayMasterBin.Rows[i].FindControl("chkgvSelect")).Checked = chkSelAll.Checked;
            if (chkSelAll.Checked)
            {
                if (!lblSelectedRecord.Text.Split(',').Contains(((Label)(gvHolidayMasterBin.Rows[i].FindControl("lblHolidayId"))).Text.Trim().ToString()))
                {
                    lblSelectedRecord.Text += ((Label)(gvHolidayMasterBin.Rows[i].FindControl("lblHolidayId"))).Text.Trim().ToString() + ",";
                }
            }
            else
            {
                string temp = string.Empty;
                string[] split = lblSelectedRecord.Text.Split(',');
                foreach (string item in split)
                {
                    if (item != ((Label)(gvHolidayMasterBin.Rows[i].FindControl("lblHolidayId"))).Text.Trim().ToString())
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
        Label lb = (Label)gvHolidayMasterBin.Rows[index].FindControl("lblHolidayId");
        if (((CheckBox)gvHolidayMasterBin.Rows[index].FindControl("chkgvSelect")).Checked)
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
        DataTable dtUnit = (DataTable)Session["dtbinFilter"];

        if (ViewState["Select"] == null)
        {
            ViewState["Select"] = 1;
            foreach (DataRow dr in dtUnit.Rows)
            {
                if (!lblSelectedRecord.Text.Split(',').Contains(dr["Holiday_Id"]))
                {
                    lblSelectedRecord.Text += dr["Holiday_Id"] + ",";
                }
            }
            for (int i = 0; i < gvHolidayMasterBin.Rows.Count; i++)
            {
                string[] split = lblSelectedRecord.Text.Split(',');
                Label lblconid = (Label)gvHolidayMasterBin.Rows[i].FindControl("lblHolidayId");
                for (int j = 0; j < lblSelectedRecord.Text.Split(',').Length; j++)
                {
                    if (lblSelectedRecord.Text.Split(',')[j] != "")
                    {
                        if (lblconid.Text.Trim().ToString() == lblSelectedRecord.Text.Split(',')[j].Trim().ToString())
                        {
                            ((CheckBox)gvHolidayMasterBin.Rows[i].FindControl("chkgvSelect")).Checked = true;
                        }
                    }
                }
            }
        }
        else
        {
            lblSelectedRecord.Text = "";
            DataTable dtUnit1 = (DataTable)Session["dtBinFilter"];
            gvHolidayMasterBin.DataSource = dtUnit1;
            gvHolidayMasterBin.DataBind();
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
                    b = objHoliday.DeleteHolidayMaster(Session["CompId"].ToString(),lblSelectedRecord.Text.Split(',')[j].Trim(), true.ToString(), Session["UserId"].ToString().ToString(), DateTime.Now.ToString());

                }
            }
        }

        if (b != 0)
        {

            FillGrid();
            FillGridBin();
            lblSelectedRecord.Text = "";
            ViewState["Select"] = null;
            DisplayMessage("Record Activated");
        }
        else
        {
            int fleg = 0;
            foreach (GridViewRow Gvr in gvHolidayMasterBin.Rows)
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
    
   public void Reset()
    {


       
        txtHolidayName.Text = "";
        txtHolidayNameL.Text = "";
        txtFromDate.Text = "";
        txtToDate.Text = "";
       
        


        
        btnNew.Text = Resources.Attendance.New;
        lblSelectedRecord.Text = "";
        ViewState["Select"] = null;
        editid.Value = "";
        txtValue.Text = "";
        ddlOption.SelectedIndex = 2;
        ddlFieldName.SelectedIndex = 0;

        pnlHolidayGroup.Visible = false;
        pnlHoliday.Visible = true;

        rbtnEmp.Checked = false;
        rbtnGroup.Checked = false;

    }

   

}
