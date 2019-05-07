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
public partial class Academic_StudentEnquiry : BasePage
{



    Common cmn = new Common();
    SystemParameter objSys = new SystemParameter();
    StudentEnquiryMaster objDep = new StudentEnquiryMaster();
    ClassMaster objEmp = new ClassMaster();
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
        Session["HeaderText"] = "System Setup";
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
        DataTable dt = objDep.GetStudentEnquiryMaster(Session["CompId"].ToString());

              if (dt.Rows.Count > 0)
        {
            ddlClass.DataSource = null;
            ddlClass.DataBind();
            ddlClass.DataSource = dt;
            ddlClass.DataTextField = "StudentName";
            ddlClass.DataValueField = "Student_Id";
            ddlClass.DataBind();
            ListItem li = new ListItem("--Select--", "0");
            ddlClass.Items.Insert(0, li);
            ddlClass.SelectedIndex = 0;

        }
          
        else
        {
            try
            {
                ddlClass.Items.Clear();
                ddlClass.DataSource = null;
                ddlClass.DataBind();
                ListItem li = new ListItem("--Select--", "0");
                ddlClass.Items.Insert(0, li);
                ddlClass.SelectedIndex = 0;
            }
            catch
            {
                ListItem li = new ListItem("--Select--", "0");
                ddlClass.Items.Insert(0, li);
                ddlClass.SelectedIndex = 0;

            }
        }

    }

    public void FillGrid()
    {
        DataTable dt = objDep.GetStudentEnquiryMaster(Session["CompId"].ToString());
        gvDepMaster.DataSource = dt;
        gvDepMaster.DataBind();
        AllPageCode();
        Session["dtFilter"] = dt;
        Session["Student"] = dt;

        lblTotalRecords.Text = Resources.Attendance.Total_Records + ": " + dt.Rows.Count.ToString() + "";

    }
    public void FillGridBin()
    {
        DataTable dt = new DataTable();
        dt = objDep.GetStudentEnquiryMasterInactive(Session["CompId"].ToString());
        

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

    protected void txtStudentName_OnTextChanged(object sender, EventArgs e)
    {

        if (editid.Value == "")
        {
            DataTable dt = objDep.GetStudentEnquiryMasterByStudentName(Session["CompId"].ToString().ToString(), txtStudentName.Text.Trim());
            if (dt.Rows.Count > 0)
            {
                txtStudentName.Text = "";
                DisplayMessage("Student Name Already Exists");
                System.Web.UI.ScriptManager.GetCurrent(this).SetFocus(txtStudentName);
                return;
            }
            DataTable dt1 = objDep.GetStudentEnquiryMasterInactive(Session["CompId"].ToString().ToString());
            dt1 = new DataView(dt1, "StudentName='" + txtStudentName.Text + "'", "", DataViewRowState.CurrentRows).ToTable();
            if (dt1.Rows.Count > 0)
            {
                txtStudentName.Text = "";
                DisplayMessage("Student Name Already Exists - Go to Bin Tab");
                System.Web.UI.ScriptManager.GetCurrent(this).SetFocus(txtStudentName);
                return;
            }
            txtFatherName.Focus();

        }
        else
        {
            DataTable dtTemp = objDep.GetStudentEnquiryMasterById(Session["CompId"].ToString().ToString(), editid.Value);
            if (dtTemp.Rows.Count > 0)
            {
                if (dtTemp.Rows[0]["StudentName"].ToString() != txtStudentName.Text)
                {
                    DataTable dt = objDep.GetStudentEnquiryMaster(Session["CompId"].ToString().ToString());
                    dt = new DataView(dt, "StudentName='" + txtStudentName.Text + "'", "", DataViewRowState.CurrentRows).ToTable();
                    if (dt.Rows.Count > 0)
                    {
                        txtStudentName.Text = "";
                        DisplayMessage("Student Name Already Exists");
                        System.Web.UI.ScriptManager.GetCurrent(this).SetFocus(txtStudentName);
                        return;
                    }
                    DataTable dt1 = objDep.GetStudentEnquiryMasterInactive(Session["CompId"].ToString().ToString());
                    dt1 = new DataView(dt1, "StudentName='" + txtStudentName.Text + "'", "", DataViewRowState.CurrentRows).ToTable();
                    if (dt1.Rows.Count > 0)
                    {
                        txtStudentName.Text = "";
                        DisplayMessage("Student Name Already Exists - Go to Bin Tab");
                        System.Web.UI.ScriptManager.GetCurrent(this).SetFocus(txtStudentName);
                        return;
                    }
                }
            }
            txtFatherName.Focus();
            txtMotherName.Focus();
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
            Label lblconid = (Label)gvDepMasterBin.Rows[i].FindControl("lblStudentId");
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
        dt = objDep.GetStudentEnquiryMasterInactive(Session["CompId"].ToString());
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
                if (!lblSelectedRecord.Text.Split(',').Contains(((Label)(gvDepMasterBin.Rows[i].FindControl("lblStudentId"))).Text.Trim().ToString()))
                {
                    lblSelectedRecord.Text += ((Label)(gvDepMasterBin.Rows[i].FindControl("lblStudentId"))).Text.Trim().ToString() + ",";
                }
            }
            else
            {
                string temp = string.Empty;
                string[] split = lblSelectedRecord.Text.Split(',');
                foreach (string item in split)
                {
                    if (item != ((Label)(gvDepMasterBin.Rows[i].FindControl("lblStudentId"))).Text.Trim().ToString())
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
        Label lb = (Label)gvDepMasterBin.Rows[index].FindControl("lblStudentId");
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


        DataTable dt = objDep.GetStudentEnquiryMasterById(Session["CompId"].ToString(), editid.Value);

        if (dt.Rows.Count > 0)
        {
            FillddlParentDepDDL();

            try
            {
                txtFatherName.Text = dt.Rows[0]["FatherName"].ToString();
                txtStudentName.Text = dt.Rows[0]["StudentName"].ToString();
                txtMotherName.Text = dt.Rows[0]["MotherName"].ToString();

                ddlClass.SelectedValue = dt.Rows[0]["Class_Id"].ToString();


                txtMobileNo.Text = dt.Rows[0]["Mobile_No"].ToString();
                txtMobileNo2.Text = dt.Rows[0]["Mobile"].ToString();
                txtDetail.Text = dt.Rows[0]["Detail"].ToString();
                txtAmount.Text = dt.Rows[0]["Amount"].ToString();
                txtIDate.Text = dt.Rows[0]["IDate"].ToString();
                txtAddressName.Text = dt.Rows[0]["Address"].ToString();
            }
            catch
            {

            }
            
            btnNew_Click(null, null);
            btnNew.Text = Resources.Attendance.Edit;

        }



    }

    protected void IbtnDelete_Command(object sender, CommandEventArgs e)
    {
        int b = 0;
        b = objDep.DeleteStudentEnquiryMaster(Session["CompId"].ToString(), e.CommandArgument.ToString(), false.ToString(), Session["UserId"].ToString(), DateTime.Now.ToString());
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


        txtStudentName.Text = "";
        txtFatherName.Text = "";
        txtMotherName.Text = "";

        txtAddressName.Text = "";
        txtDetail.Text = "";

        ddlClass.SelectedIndex = 0;

        txtMobileNo.Text = "";
        txtMobileNo2.Text = "";
        txtAmount.Text = "";
        txtIDate.Text = "";


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

        txtStudentName.Focus();
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
                    b = objDep.DeleteStudentEnquiryMaster(Session["CompId"].ToString(), lblSelectedRecord.Text.Split(',')[j].Trim(), true.ToString(), Session["UserId"].ToString(), DateTime.Now.ToString());

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
                if (!lblSelectedRecord.Text.Split(',').Contains(dr["Student_Id"]))
                {
                    lblSelectedRecord.Text += dr["Student_Id"] + ",";
                }
            }
            for (int i = 0; i < gvDepMasterBin.Rows.Count; i++)
            {
                string[] split = lblSelectedRecord.Text.Split(',');
                Label lblconid = (Label)gvDepMasterBin.Rows[i].FindControl("lblStudentId");
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
        if (txtStudentName.Text == "")
        {
            DisplayMessage("Enter Student Name");
            txtStudentName.Focus();
            return;
        }
       
       


        if (editid.Value == "")
        {
            DataTable dt = objDep.GetStudentEnquiryMaster(Session["CompId"].ToString());

            dt = new DataView(dt, "StudentName='" + txtStudentName.Text + "'", "", DataViewRowState.CurrentRows).ToTable();
            if (dt.Rows.Count > 0)
            {
                if (dt.Rows[0]["Student_Id"].ToString() != editid.Value)
                {
                    txtStudentName.Text = "";
                    DisplayMessage("Student Name Already Exists");
                    System.Web.UI.ScriptManager.GetCurrent(this).SetFocus(txtStudentName);
                    return;

                }
            }
            b = objDep.InsertStudentEnquiryMaster(Session["CompId"].ToString(), txtStudentName.Text, txtFatherName.Text, txtMotherName.Text, txtAmount.Text, txtDetail.Text,txtIDate.Text,txtAddressName.Text, ddlClass.SelectedValue, txtMobileNo.Text, txtMobileNo2.Text,  "", "0", "", "", "", false.ToString(), DateTime.Now.ToString(), true.ToString(), Session["UserId"].ToString(), DateTime.Now.ToString(), Session["UserId"].ToString(), DateTime.Now.ToString());

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
            DataTable dt = objDep.GetStudentEnquiryMaster(Session["CompId"].ToString());


            
            string StudentName = string.Empty;

            try
            {
                StudentName = (new DataView(dt, "Student_Id='" + editid.Value + "'", "", DataViewRowState.CurrentRows).ToTable()).Rows[0]["StudentName"].ToString();
            }
            catch
            {
                StudentName = "";
            }

            dt = new DataView(dt, "StudentName='" + txtStudentName.Text + "' and StudentName<>'" + StudentName + "' ", "", DataViewRowState.CurrentRows).ToTable();

            if (dt.Rows.Count > 0)
            {
                DisplayMessage("Student Name Already Exists");
                txtStudentName.Focus();
                return;

            }
            b = objDep.UpdateStudentEnquiryMaster(editid.Value, Session["CompId"].ToString(),txtFatherName.Text, txtStudentName.Text, txtMotherName.Text, txtAmount.Text, txtAddressName.Text, ddlClass.SelectedValue, txtMobileNo.Text, txtMobileNo2.Text,txtIDate.Text, txtDetail.Text, "", "0", "", "", "", false.ToString(), DateTime.Now.ToString(), true.ToString(), Session["UserId"].ToString(), DateTime.Now.ToString());

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
    public static string[] GetCompletionListStudentName(string prefixText, int count, string contextKey)
    {
        StudentEnquiryMaster objDepMaster = new StudentEnquiryMaster();
        DataTable dt = new DataView(objDepMaster.GetStudentEnquiryMaster(HttpContext.Current.Session["CompId"].ToString()), "StudentName like '" + prefixText.ToString() + "%'", "", DataViewRowState.CurrentRows).ToTable();


        string[] txt = new string[dt.Rows.Count];

        for (int i = 0; i < dt.Rows.Count; i++)
        {
            txt[i] = dt.Rows[i]["StudentName"].ToString();
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
