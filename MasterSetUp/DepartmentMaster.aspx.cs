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

public partial class MasterSetUp_DepartmentMaster : BasePage
{
   
  
   
    Common cmn = new Common();
    SystemParameter objSys = new SystemParameter();
    DepartmentMaster objDep = new DepartmentMaster();
    EmployeeMaster objEmp = new EmployeeMaster();
    protected void Page_Load(object sender, EventArgs e)
    {
       
        
       
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
            FillddlParentDepDDL();
            btnList_Click(null, null);

        }
    }

    public void AllPageCode()
    {

        Page.Title = objSys.GetSysTitle();
        Session["AccordianId"] = "8";
        Session["HeaderText"] = "Master Setup";
        DataTable dtAllPageCode = cmn.GetAllPagePermission(Session["UserId"].ToString(), "8", "14");
        if (dtAllPageCode.Rows.Count != 0)
        {
            if (dtAllPageCode.Rows[0][0].ToString() == "SuperAdmin")
            {
                btnSave.Visible = true;
                foreach (GridViewRow Row in gvDepMaster.Rows)
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
                    foreach (GridViewRow Row in gvDepMaster.Rows)
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

    public void FillddlParentDepDDL()
    {
        DataTable dt = objDep.GetDepartmentMaster(Session["CompId"].ToString());

        dt = new DataView(dt,"Brand_Id='"+Session["BrandId"].ToString()+"' and Location_Id='"+Session["LocId"].ToString()+"' ","",DataViewRowState.CurrentRows).ToTable();


        if (dt.Rows.Count > 0)
        {
            ddlParentDep.DataSource = null;
            ddlParentDep.DataBind();
            ddlParentDep.DataSource = dt;
            ddlParentDep.DataTextField = "Dep_Name";
            ddlParentDep.DataValueField = "Dep_Id";
            ddlParentDep.DataBind();
            ListItem li = new ListItem("--Select--", "0");
            ddlParentDep.Items.Insert(0, li);
            ddlParentDep.SelectedIndex = 0;

        }
        else
        {
            try
            {
                ddlParentDep.Items.Clear();
                ddlParentDep.DataSource = null;
                ddlParentDep.DataBind();
                ListItem li = new ListItem("--Select--", "0");
                ddlParentDep.Items.Insert(0, li);
                ddlParentDep.SelectedIndex = 0;
            }
            catch
            {
                ListItem li = new ListItem("--Select--", "0");
                ddlParentDep.Items.Insert(0, li);
                ddlParentDep.SelectedIndex = 0;

            }
        }

    }

    public void FillGrid()
    {
        DataTable dt = objDep.GetDepartmentMaster(Session["CompId"].ToString());
        dt = new DataView(dt, "Brand_Id='" + Session["BrandId"].ToString() + "' and Location_Id='" + Session["LocId"].ToString() + "' ", "", DataViewRowState.CurrentRows).ToTable();

        gvDepMaster.DataSource = dt;
        gvDepMaster.DataBind();
        AllPageCode();
        Session["dtFilter"] = dt;
        Session["Dep"] = dt;

        lblTotalRecords.Text = Resources.Attendance.Total_Records + ": " + dt.Rows.Count.ToString() + "";

    }
    public void FillGridBin()
    {
        DataTable dt = new DataTable();
        dt = objDep.GetDepartmentMasterInactive(Session["CompId"].ToString());
        dt = new DataView(dt, "Brand_Id='" + Session["BrandId"].ToString() + "' and Location_Id='" + Session["LocId"].ToString() + "' ", "", DataViewRowState.CurrentRows).ToTable();

        gvDepMasterBin.DataSource = dt;
        gvDepMasterBin.DataBind();


        Session["dtbinFilter"] = dt;
        Session["dtbinDep"] = dt;
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
    protected void gvDepMaster_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        gvDepMaster.PageIndex = e.NewPageIndex;

        DataTable dt = (DataTable)Session["dtFilter"];
        gvDepMaster.DataSource = dt;
        gvDepMaster.DataBind();
        AllPageCode();

    }
    protected void gvDepMaster_OnSorting(object sender, GridViewSortEventArgs e)
    {
        HDFSort.Value = HDFSort.Value == "ASC" ? "DESC" : "ASC";
        DataTable dt = new DataTable();
        dt = (DataTable)Session["dtFilter"];
        DataView dv = new DataView(dt);
        string Query = "" + e.SortExpression + " " + HDFSort.Value + "";
        dv.Sort = Query;
        dt = dv.ToTable();
        Session["dtFilter"] = dt;
        gvDepMaster.DataSource = dt;
        gvDepMaster.DataBind();
        AllPageCode();
    }

    protected void txtDepName_OnTextChanged(object sender, EventArgs e)
    {

        if (editid.Value == "")
        {
            DataTable dt = objDep.GetDepartmentMasterByDepName(Session["CompId"].ToString().ToString(), txtDeptName.Text.Trim());
            if (dt.Rows.Count > 0)
            {
                txtDeptName.Text = "";
                DisplayMessage("Department Name Already Exists");
                System.Web.UI.ScriptManager.GetCurrent(this).SetFocus(txtDeptName);
                return;
            }
            DataTable dt1 = objDep.GetDepartmentMasterInactive(Session["CompId"].ToString().ToString());
            dt1 = new DataView(dt1, "Dep_Name='" + txtDeptName.Text + "'", "", DataViewRowState.CurrentRows).ToTable();
            if (dt1.Rows.Count > 0)
            {
                txtDeptName.Text = "";
                DisplayMessage("Department Name Already Exists - Go to Bin Tab");
                System.Web.UI.ScriptManager.GetCurrent(this).SetFocus(txtDeptName);
                return;
            }
            txtDeptNameL.Focus();
        }
        else
        {
            DataTable dtTemp = objDep.GetDepartmentMasterById(Session["CompId"].ToString().ToString(), editid.Value);
            if (dtTemp.Rows.Count > 0)
            {
                if (dtTemp.Rows[0]["Dep_Name"].ToString() != txtDeptName.Text)
                {
                    DataTable dt = objDep.GetDepartmentMaster(Session["CompId"].ToString().ToString());
                    dt = new DataView(dt, "Dep_Name='" + txtDeptName.Text + "'", "", DataViewRowState.CurrentRows).ToTable();
                    if (dt.Rows.Count > 0)
                    {
                        txtDeptName.Text = "";
                        DisplayMessage("Department Name Already Exists");
                        System.Web.UI.ScriptManager.GetCurrent(this).SetFocus(txtDeptName);
                        return;
                    }
                    DataTable dt1 = objDep.GetDepartmentMasterInactive(Session["CompId"].ToString().ToString());
                    dt1 = new DataView(dt1, "Dep_Name='" + txtDeptName.Text + "'", "", DataViewRowState.CurrentRows).ToTable();
                    if (dt1.Rows.Count > 0)
                    {
                        txtDeptName.Text = "";
                        DisplayMessage("Department Name Already Exists - Go to Bin Tab");
                        System.Web.UI.ScriptManager.GetCurrent(this).SetFocus(txtDeptName);
                        return;
                    }
                }
            }
            txtDeptNameL.Focus();
        }
    }
    protected void gvDepMasterBin_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {

        gvDepMasterBin.PageIndex = e.NewPageIndex;
        if (HDFSortbin.Value == "")
            FillGridBin();
        else
        {
            DataTable dt = (DataTable)Session["dtbinFilter"];
            gvDepMasterBin.DataSource = dt;
            gvDepMasterBin.DataBind();
            AllPageCode();
        }
        string temp = string.Empty;
        bool isselcted;

        for (int i = 0; i < gvDepMasterBin.Rows.Count; i++)
        {
            Label lblconid = (Label)gvDepMasterBin.Rows[i].FindControl("lblDepId");
            string[] split = lblSelectedRecord.Text.Split(',');

            for (int j = 0; j < lblSelectedRecord.Text.Split(',').Length; j++)
            {
                if (lblSelectedRecord.Text.Split(',')[j] != "")
                {
                    if (lblconid.Text.Trim().ToString() == lblSelectedRecord.Text.Split(',')[j].Trim().ToString())
                    {
                        ((CheckBox)gvDepMasterBin.Rows[i].FindControl("chkgvSelect")).Checked = true;
                    }
                }
            }
        }

    }
    protected void gvDepMasterBin_OnSorting(object sender, GridViewSortEventArgs e)
    {
        lblSelectedRecord.Text = "";
        HDFSortbin.Value = HDFSortbin.Value == "ASC" ? "DESC" : "ASC";
        DataTable dt = new DataTable();
        dt = objDep.GetDepartmentMasterInactive(Session["CompId"].ToString());
        dt = new DataView(dt, "Brand_Id='" + Session["BrandId"].ToString() + "' and Location_Id='" + Session["LocId"].ToString() + "' ", "", DataViewRowState.CurrentRows).ToTable();

        DataView dv = new DataView(dt);
        string Query = "" + e.SortExpression + " " + HDFSortbin.Value + "";
        dv.Sort = Query;
        dt = dv.ToTable();
        Session["dtbinFilter"] = dt;
        gvDepMasterBin.DataSource = dt;
        gvDepMasterBin.DataBind();
        AllPageCode();

    }

    protected void chkgvSelectAll_CheckedChanged(object sender, EventArgs e)
    {
        CheckBox chkSelAll = ((CheckBox)gvDepMasterBin.HeaderRow.FindControl("chkgvSelectAll"));
        for (int i = 0; i < gvDepMasterBin.Rows.Count; i++)
        {
            ((CheckBox)gvDepMasterBin.Rows[i].FindControl("chkgvSelect")).Checked = chkSelAll.Checked;
            if (chkSelAll.Checked)
            {
                if (!lblSelectedRecord.Text.Split(',').Contains(((Label)(gvDepMasterBin.Rows[i].FindControl("lblDepId"))).Text.Trim().ToString()))
                {
                    lblSelectedRecord.Text += ((Label)(gvDepMasterBin.Rows[i].FindControl("lblDepId"))).Text.Trim().ToString() + ",";
                }
            }
            else
            {
                string temp = string.Empty;
                string[] split = lblSelectedRecord.Text.Split(',');
                foreach (string item in split)
                {
                    if (item != ((Label)(gvDepMasterBin.Rows[i].FindControl("lblDepId"))).Text.Trim().ToString())
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
        Label lb = (Label)gvDepMasterBin.Rows[index].FindControl("lblDepId");
        if (((CheckBox)gvDepMasterBin.Rows[index].FindControl("chkgvSelect")).Checked)
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

    protected void btnEdit_Command(object sender, CommandEventArgs e)
    {
        editid.Value = e.CommandArgument.ToString();


        DataTable dt = objDep.GetDepartmentMasterById(Session["CompId"].ToString(), editid.Value);
      
        if (dt.Rows.Count > 0)
        {
            FillddlParentDepDDL();

            try
            {
                txtDeptCode.Text = dt.Rows[0]["Dep_Code"].ToString();
                txtDeptName.Text = dt.Rows[0]["Dep_Name"].ToString();
                txtDeptNameL.Text = dt.Rows[0]["Dep_Name_L"].ToString();
            
                ddlParentDep.SelectedValue = dt.Rows[0]["Parent_Id"].ToString();

               
                txtPhoneNo.Text = dt.Rows[0]["Phone_No"].ToString();
                txtFax.Text = dt.Rows[0]["FaxNo"].ToString();
            }
            catch
            {

            }
            if (dt.Rows[0]["Emp_Id"].ToString().Trim() != "0" || dt.Rows[0]["Emp_Id"].ToString().Trim() != "")
            {
                txtManagerName.Text = cmn.GetEmpName(dt.Rows[0]["Emp_Id"].ToString());
            }
            else
            {
                txtManagerName.Text = "";

            }

            btnNew_Click(null, null);
            btnNew.Text = Resources.Attendance.Edit;

        }



    }

    protected void IbtnDelete_Command(object sender, CommandEventArgs e)
    {
        int b = 0;
        b = objDep.DeleteDepartmentMaster(Session["CompId"].ToString(), e.CommandArgument.ToString(), false.ToString(), Session["UserId"].ToString(), DateTime.Now.ToString());
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
    public void DisplayMessage(string str)
    {
        ScriptManager.RegisterStartupScript(this, GetType(), "", "alert('" + str + "');", true);
    }
    public void Reset()
    {


        txtDeptCode.Text = "";
        txtDeptName.Text = "";
        txtDeptNameL.Text = "";
    
        txtManagerName.Text = "";

        ddlParentDep.SelectedIndex = 0;

        txtPhoneNo.Text = "";
        txtFax.Text = "";


        btnNew.Text = Resources.Attendance.New;
        lblSelectedRecord.Text = "";
        ViewState["Select"] = null;
        editid.Value = "";
        txtValue.Text = "";
        ddlOption.SelectedIndex = 2;
        ddlFieldName.SelectedIndex = 0;



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

        txtDeptName.Focus();
        pnlMenuNew.BackColor = System.Drawing.ColorTranslator.FromHtml("#ccddee");

        pnlMenuList.BackColor = System.Drawing.ColorTranslator.FromHtml("#90bde9");
        pnlMenuBin.BackColor = System.Drawing.ColorTranslator.FromHtml("#90bde9");

        PnlList.Visible = false;
        PnlNewEdit.Visible = true;
        PnlBin.Visible = false;
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
            DataTable dtCust = (DataTable)Session["Dep"];


            DataView view = new DataView(dtCust, condition, "", DataViewRowState.CurrentRows);
            Session["dtFilter"] = view.ToTable();
            lblTotalRecords.Text = Resources.Attendance.Total_Records + ": " + view.ToTable().Rows.Count.ToString() + "";

            gvDepMaster.DataSource = view.ToTable();
            gvDepMaster.DataBind();

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
            DataTable dtCust = (DataTable)Session["dtbinDep"];


            DataView view = new DataView(dtCust, condition, "", DataViewRowState.CurrentRows);
            Session["dtbinFilter"] = view.ToTable();
            lblbinTotalRecords.Text = Resources.Attendance.Total_Records + ": " + view.ToTable().Rows.Count.ToString() + "";

            gvDepMasterBin.DataSource = view.ToTable();
            gvDepMasterBin.DataBind();


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

    protected void imgBtnRestore_Click(object sender, ImageClickEventArgs e)
    {
        int b = 0;
        if (lblSelectedRecord.Text != "")
        {
            for (int j = 0; j < lblSelectedRecord.Text.Split(',').Length; j++)
            {
                if (lblSelectedRecord.Text.Split(',')[j] != "")
                {
                    b = objDep.DeleteDepartmentMaster(Session["CompId"].ToString(), lblSelectedRecord.Text.Split(',')[j].Trim(), true.ToString(), Session["UserId"].ToString(), DateTime.Now.ToString());

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
            foreach (GridViewRow Gvr in gvDepMasterBin.Rows)
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

    protected void ImgbtnSelectAll_Click(object sender, ImageClickEventArgs e)
    {

        DataTable dtUnit = (DataTable)Session["dtbinFilter"];

        if (ViewState["Select"] == null)
        {
            ViewState["Select"] = 1;
            foreach (DataRow dr in dtUnit.Rows)
            {
                if (!lblSelectedRecord.Text.Split(',').Contains(dr["Dep_Id"]))
                {
                    lblSelectedRecord.Text += dr["Dep_Id"] + ",";
                }
            }
            for (int i = 0; i < gvDepMasterBin.Rows.Count; i++)
            {
                string[] split = lblSelectedRecord.Text.Split(',');
                Label lblconid = (Label)gvDepMasterBin.Rows[i].FindControl("lblDepId");
                for (int j = 0; j < lblSelectedRecord.Text.Split(',').Length; j++)
                {
                    if (lblSelectedRecord.Text.Split(',')[j] != "")
                    {
                        if (lblconid.Text.Trim().ToString() == lblSelectedRecord.Text.Split(',')[j].Trim().ToString())
                        {
                            ((CheckBox)gvDepMasterBin.Rows[i].FindControl("chkgvSelect")).Checked = true;
                        }
                    }
                }
            }
        }
        else
        {
            lblSelectedRecord.Text = "";
            DataTable dtUnit1 = (DataTable)Session["dtBinFilter"];
            gvDepMasterBin.DataSource = dtUnit1;
            gvDepMasterBin.DataBind();
            ViewState["Select"] = null;
        }
    }
    protected void btnSave_Click(object sender, EventArgs e)
    {
        int b = 0;
        if (txtDeptName.Text == "")
        {
            DisplayMessage("Enter Department Name");
            txtDeptName.Focus();
            return;
        }
        if (txtDeptCode.Text == "")
        {
            DisplayMessage("Enter Department Code");
            txtDeptCode.Focus();
            return;
        }


        string empid = string.Empty;
        if (txtManagerName.Text != "")
        {
            empid = txtManagerName.Text.Split('/')[txtManagerName.Text.Split('/').Length - 1];

            DataTable dtEmp = objEmp.GetEmployeeMaster(Session["CompId"].ToString());

            dtEmp = new DataView(dtEmp, "Brand_Id='" + Session["BrandId"].ToString() + "' and Location_Id='" + Session["LocId"].ToString() + "' ", "", DataViewRowState.CurrentRows).ToTable();


            dtEmp = new DataView(dtEmp, "Emp_Code='" + empid + "'", "", DataViewRowState.CurrentRows).ToTable();

            if (dtEmp.Rows.Count > 0)
            {
                empid = dtEmp.Rows[0]["Emp_Id"].ToString();
            }
            else
            {
                DisplayMessage("Employee Not Exists");
                txtManagerName.Text = "";
                txtManagerName.Focus();
                return;
            }

        }
        else
        {
            empid = "0";
        }



        if (editid.Value == "")
        {
            DataTable dt = objDep.GetDepartmentMaster(Session["CompId"].ToString());
            dt = new DataView(dt, "Brand_Id='" + Session["BrandId"].ToString() + "' and Location_Id='" + Session["LocId"].ToString() + "' ", "", DataViewRowState.CurrentRows).ToTable();

            dt = new DataView(dt, "Dep_Code='" + txtDeptCode.Text + "'", "", DataViewRowState.CurrentRows).ToTable();
            if (dt.Rows.Count > 0)
            {
                DisplayMessage("Department Code Already Exists");
                txtDeptCode.Focus();
                return;

            }
            DataTable dt1 = objDep.GetDepartmentMaster(Session["CompId"].ToString());
            dt1 = new DataView(dt1, "Brand_Id='" + Session["BrandId"].ToString() + "' and Location_Id='" + Session["LocId"].ToString() + "' ", "", DataViewRowState.CurrentRows).ToTable();

            dt1 = new DataView(dt1, "Dep_Name='" + txtDeptName.Text + "'", "", DataViewRowState.CurrentRows).ToTable();
            if (dt1.Rows.Count > 0)
            {
                DisplayMessage("Department Name Already Exists");
                txtDeptName.Focus();
                return;

            }




            b = objDep.InsertDepartmentMaster(Session["CompId"].ToString(), txtDeptName.Text, txtDeptNameL.Text, txtDeptCode.Text, Session["BrandId"].ToString(), Session["LocId"].ToString(), ddlParentDep.SelectedValue, txtPhoneNo.Text, txtFax.Text,empid, "", "0", "", "", "",false.ToString(),DateTime.Now.ToString(),true.ToString(), Session["UserId"].ToString(), DateTime.Now.ToString(), Session["UserId"].ToString(), DateTime.Now.ToString());

            if (b != 0)
            {
                DisplayMessage("Record Saved");
                FillGrid();
                Reset();

            }
            else
            {
                DisplayMessage("Record Not Saved");
            }
        }
        else
        {
            DataTable dt = objDep.GetDepartmentMaster(Session["CompId"].ToString());


            string DepartmentCode = string.Empty;
            string DepartmentName = string.Empty;

            try
            {
                DepartmentCode = (new DataView(dt, "Dep_Id='" + editid.Value + "'", "", DataViewRowState.CurrentRows).ToTable()).Rows[0]["Dep_Code"].ToString();
            }
            catch
            {
                DepartmentCode = "";
            }

            dt = new DataView(dt, "Dep_Code='" + txtDeptCode.Text + "' and Dep_Code<>'" + DepartmentCode + "' ", "", DataViewRowState.CurrentRows).ToTable();

            if (dt.Rows.Count > 0)
            {
                DisplayMessage("Department Code Already Exists");
                 txtDeptCode.Focus();
                return;

            }



            DataTable dt1 = objDep.GetDepartmentMaster(Session["CompId"].ToString());
            try
            {
                DepartmentName = (new DataView(dt1, "Dep_Id='" + editid.Value + "'", "", DataViewRowState.CurrentRows).ToTable()).Rows[0]["Dep_Name"].ToString();
            }
            catch
            {
                DepartmentName = "";
            }
            dt1 = new DataView(dt1, "Dep_Name='" + txtDeptName.Text + "' and Dep_Name<>'" + DepartmentName + "' ", "", DataViewRowState.CurrentRows).ToTable();
            if (dt1.Rows.Count > 0)
            {
                DisplayMessage("Department Name Already Exists");
                txtDeptName.Focus();
                return;

            }

            b = objDep.UpdateDepartmentMaster(editid.Value, Session["CompId"].ToString(), Session["LocId"].ToString(), txtDeptName.Text, txtDeptNameL.Text, txtDeptCode.Text, Session["BrandId"].ToString(), ddlParentDep.SelectedValue, txtPhoneNo.Text, txtFax.Text,empid, "", "0", "", "", "",false.ToString(),DateTime.Now.ToString(),true.ToString(), Session["UserId"].ToString(), DateTime.Now.ToString());

            if (b != 0)
            {
                btnList_Click(null, null);
                DisplayMessage("Record Updated");
                Reset();
                FillGrid();


            }
            else
            {
                DisplayMessage("Record Not Updated");
            }

        }
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




    [System.Web.Services.WebMethodAttribute(), System.Web.Script.Services.ScriptMethodAttribute()]
    public static string[] GetCompletionListDepCode(string prefixText, int count, string contextKey)
    {
        DepartmentMaster objDepMaster = new DepartmentMaster();
        DataTable dt = new DataView(objDepMaster.GetDepartmentMaster(HttpContext.Current.Session["CompId"].ToString()), "Dep_Code like '" + prefixText.ToString() + "%'", "", DataViewRowState.CurrentRows).ToTable();
        dt = new DataView(dt, "Brand_Id='" + HttpContext.Current.Session["BrandId"].ToString() + "' and Location_Id='" + HttpContext.Current.Session["LocId"].ToString() + "' ", "", DataViewRowState.CurrentRows).ToTable();

        string[] txt = new string[dt.Rows.Count];

        for (int i = 0; i < dt.Rows.Count; i++)
        {
            txt[i] = dt.Rows[i]["Dep_Code"].ToString();
        }
        return txt;
    }
    [System.Web.Services.WebMethodAttribute(), System.Web.Script.Services.ScriptMethodAttribute()]
    public static string[] GetCompletionListDepName(string prefixText, int count, string contextKey)
    {
        DepartmentMaster objDepMaster = new DepartmentMaster();

        DataTable dt = new DataView(objDepMaster.GetDepartmentMaster(HttpContext.Current.Session["CompId"].ToString()), "Dep_Name like '" + prefixText.ToString() + "%'", "", DataViewRowState.CurrentRows).ToTable();
        dt = new DataView(dt, "Brand_Id='" + HttpContext.Current.Session["BrandId"].ToString() + "' and Location_Id='" + HttpContext.Current.Session["LocId"].ToString() + "' ", "", DataViewRowState.CurrentRows).ToTable();

        string[] txt = new string[dt.Rows.Count];

        for (int i = 0; i < dt.Rows.Count; i++)
        {
            txt[i] = dt.Rows[i]["Dep_Name"].ToString();
        }
        return txt;
    }
    [System.Web.Services.WebMethodAttribute(), System.Web.Script.Services.ScriptMethodAttribute()]
    public static string[] GetCompletionListEmployeeName(string prefixText, int count, string contextKey)
    {
        DataTable dt = Common.GetEmployee(prefixText, HttpContext.Current.Session["CompId"].ToString());
        dt = new DataView(dt, "Brand_Id='" + HttpContext.Current.Session["BrandId"].ToString() + "' and Location_Id='" + HttpContext.Current.Session["LocId"].ToString() + "' ", "", DataViewRowState.CurrentRows).ToTable();

        string[] str = new string[dt.Rows.Count];
        for (int i = 0; i < dt.Rows.Count; i++)
        {
            str[i] = "" + dt.Rows[i][1].ToString() + "/(" + dt.Rows[i][2].ToString() + ")/" + dt.Rows[i][0].ToString() + "";
        }
        return str;
    }
}
