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
using System.Text.RegularExpressions;

public partial class MasterSetUp_LocationMaster : BasePage
{

  
    LocationMaster objLocation = new LocationMaster();
    Common cmn = new Common();
    SystemParameter objSys = new SystemParameter();
    Set_AddressMaster AM = new Set_AddressMaster();
    Set_AddressCategory ObjAddressCat = new Set_AddressCategory();
    Set_AddressChild objAddChild = new Set_AddressChild();
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
            FillddlParentLocationDDL();
            btnList_Click(null, null);

        }
    }

    public void AllPageCode()
    {

        Page.Title = objSys.GetSysTitle();
        Session["AccordianId"] = "8";
        Session["HeaderText"] = "Master Setup";
        DataTable dtAllPageCode = cmn.GetAllPagePermission(Session["UserId"].ToString(), "8", "13");
        if (dtAllPageCode.Rows.Count != 0)
        {
            if (dtAllPageCode.Rows[0][0].ToString() == "SuperAdmin")
            {
                btnSave.Visible = true;
                foreach (GridViewRow Row in gvLocationMaster.Rows)
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
                    foreach (GridViewRow Row in gvLocationMaster.Rows)
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
    [System.Web.Services.WebMethodAttribute(), System.Web.Script.Services.ScriptMethodAttribute()]
    public static string[] GetCompletionListEmployeeName(string prefixText, int count, string contextKey)
    {
        DataTable dt = Common.GetEmployee(prefixText, HttpContext.Current.Session["CompId"].ToString());
        dt = new DataView(dt, "Brand_Id='" + HttpContext.Current.Session["BrandId"].ToString() + "'", "", DataViewRowState.CurrentRows).ToTable();

        string[] str = new string[dt.Rows.Count];
        for (int i = 0; i < dt.Rows.Count; i++)
        {
            str[i] = "" + dt.Rows[i][1].ToString() + "/(" + dt.Rows[i][2].ToString() + ")/" + dt.Rows[i][0].ToString() + "";
        }
        return str;
    }
    public void FillddlParentLocationDDL()
    {
        DataTable dt = objLocation.GetLocationMaster(Session["CompId"].ToString());
        dt = new DataView(dt, "Brand_Id='" + Session["BrandId"].ToString() + "'", "", DataViewRowState.CurrentRows).ToTable();



        if (dt.Rows.Count > 0)
        {
            ddlParentLocation.DataSource = null;
            ddlParentLocation.DataBind();
            ddlParentLocation.DataSource = dt;
            ddlParentLocation.DataTextField = "Location_Name";
            ddlParentLocation.DataValueField = "Location_Id";
            ddlParentLocation.DataBind();
            ListItem li = new ListItem("--Select--", "0");
            ddlParentLocation.Items.Insert(0, li);
            ddlParentLocation.SelectedIndex = 0;

        }
        else
        {
            try
            {
                ddlParentLocation.Items.Clear();
                ddlParentLocation.DataSource = null;
                ddlParentLocation.DataBind();
                ListItem li = new ListItem("--Select--", "0");
                ddlParentLocation.Items.Insert(0, li);
                ddlParentLocation.SelectedIndex = 0;
            }
            catch
            {
                ListItem li = new ListItem("--Select--", "0");
                ddlParentLocation.Items.Insert(0, li);
                ddlParentLocation.SelectedIndex = 0;

            }
        }

    }

    public void FillGrid()
    {
        DataTable dt = objLocation.GetLocationMaster(Session["CompId"].ToString());
        dt = new DataView(dt, "Brand_Id='" + Session["BrandId"].ToString() + "'", "", DataViewRowState.CurrentRows).ToTable();

        gvLocationMaster.DataSource = dt;
        gvLocationMaster.DataBind();
        AllPageCode();
        Session["dtFilter"] = dt;
        Session["Location"] = dt;

        lblTotalRecords.Text = Resources.Attendance.Total_Records + ": " + dt.Rows.Count.ToString() + "";

    }
    public void FillGridBin()
    {
        DataTable dt = new DataTable();
        dt = objLocation.GetLocationMasterInactive(Session["CompId"].ToString());
        dt = new DataView(dt, "Brand_Id='" + Session["BrandId"].ToString() + "'", "", DataViewRowState.CurrentRows).ToTable();

        gvLocationMasterBin.DataSource = dt;
        gvLocationMasterBin.DataBind();
        AllPageCode();

        Session["dtbinFilter"] = dt;
        Session["dtbinLocation"] = dt;
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
    protected void gvLocationMaster_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        gvLocationMaster.PageIndex = e.NewPageIndex;

        DataTable dt = (DataTable)Session["dtFilter"];
        gvLocationMaster.DataSource = dt;
        gvLocationMaster.DataBind();
        AllPageCode();

    }
    protected void gvLocationMaster_OnSorting(object sender, GridViewSortEventArgs e)
    {
        HDFSort.Value = HDFSort.Value == "ASC" ? "DESC" : "ASC";
        DataTable dt = new DataTable();
        dt = (DataTable)Session["dtFilter"];
        DataView dv = new DataView(dt);
        string Query = "" + e.SortExpression + " " + HDFSort.Value + "";
        dv.Sort = Query;
        dt = dv.ToTable();
        Session["dtFilter"] = dt;
        gvLocationMaster.DataSource = dt;
        gvLocationMaster.DataBind();
        AllPageCode();
    }
    protected void gvLocationMasterBin_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {

       gvLocationMasterBin.PageIndex = e.NewPageIndex;
        if (HDFSortbin.Value == "")
            FillGridBin();
        else
        {
            DataTable dt = (DataTable)Session["dtbinFilter"];
            gvLocationMasterBin.DataSource = dt;
            gvLocationMasterBin.DataBind();
            AllPageCode();
        }
        string temp = string.Empty;
        bool isselcted;

        for (int i = 0; i < gvLocationMasterBin.Rows.Count; i++)
        {
            Label lblconid = (Label)gvLocationMasterBin.Rows[i].FindControl("lblLocationId");
            string[] split = lblSelectedRecord.Text.Split(',');

            for (int j = 0; j < lblSelectedRecord.Text.Split(',').Length; j++)
            {
                if (lblSelectedRecord.Text.Split(',')[j] != "")
                {
                    if (lblconid.Text.Trim().ToString() == lblSelectedRecord.Text.Split(',')[j].Trim().ToString())
                    {
                        ((CheckBox)gvLocationMasterBin.Rows[i].FindControl("chkgvSelect")).Checked = true;
                    }
                }
            }
        }

    }
    protected void gvLocationMasterBin_OnSorting(object sender, GridViewSortEventArgs e)
    {
        lblSelectedRecord.Text = "";
        HDFSortbin.Value = HDFSortbin.Value == "ASC" ? "DESC" : "ASC";
        DataTable dt = new DataTable();
        dt = objLocation.GetLocationMasterInactive(Session["CompId"].ToString());
        DataView dv = new DataView(dt);
        string Query = "" + e.SortExpression + " " + HDFSortbin.Value + "";
        dv.Sort = Query;
        dt = dv.ToTable();
        Session["dtbinFilter"] = dt;
        gvLocationMasterBin.DataSource = dt;
        gvLocationMasterBin.DataBind();
        AllPageCode();

    }

    protected void chkgvSelectAll_CheckedChanged(object sender, EventArgs e)
    {
        CheckBox chkSelAll = ((CheckBox)gvLocationMasterBin.HeaderRow.FindControl("chkgvSelectAll"));
        for (int i = 0; i < gvLocationMasterBin.Rows.Count; i++)
        {
            ((CheckBox)gvLocationMasterBin.Rows[i].FindControl("chkgvSelect")).Checked = chkSelAll.Checked;
            if (chkSelAll.Checked)
            {
                if (!lblSelectedRecord.Text.Split(',').Contains(((Label)(gvLocationMasterBin.Rows[i].FindControl("lblLocationId"))).Text.Trim().ToString()))
                {
                    lblSelectedRecord.Text += ((Label)(gvLocationMasterBin.Rows[i].FindControl("lblLocationId"))).Text.Trim().ToString() + ",";
                }
            }
            else
            {
                string temp = string.Empty;
                string[] split = lblSelectedRecord.Text.Split(',');
                foreach (string item in split)
                {
                    if (item != ((Label)(gvLocationMasterBin.Rows[i].FindControl("lblLocationId"))).Text.Trim().ToString())
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
        Label lb = (Label)gvLocationMasterBin.Rows[index].FindControl("lblLocationId");
        if (((CheckBox)gvLocationMasterBin.Rows[index].FindControl("chkgvSelect")).Checked)
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


        DataTable dt = objLocation.GetLocationMasterById(Session["CompId"].ToString(), editid.Value);
        if (dt.Rows.Count > 0)
        {
            FillddlParentLocationDDL();

            try
            {
                txtLocationCode.Text = dt.Rows[0]["Location_Code"].ToString();
                txtLocationName.Text = dt.Rows[0]["Location_Name"].ToString();
                txtLocationNameL.Text = dt.Rows[0]["Location_Name_L"].ToString();
                txtLocationType.Text = dt.Rows[0]["Location_Type"].ToString();
                ddlParentLocation.SelectedValue = dt.Rows[0]["Parent_Id"].ToString();

                
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


            DataTable dtChild = objAddChild.GetAddressChildDataByAddTypeAndAddRefId("Location", editid.Value);
            if (dtChild.Rows.Count > 0)
            {
                GvAddressName.DataSource = dtChild;
                GvAddressName.DataBind();
            }
            btnNew_Click(null, null);
            btnNew.Text = Resources.Attendance.Edit;

        }



    }

    protected void IbtnDelete_Command(object sender, CommandEventArgs e)
    {
        int b = 0;
        b = objLocation.DeleteLocationMaster(Session["CompId"].ToString(), e.CommandArgument.ToString(), false.ToString(), Session["UserId"].ToString(), DateTime.Now.ToString());
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


        txtLocationCode.Text = "";
        txtLocationName.Text = "";
        txtLocationNameL.Text = "";
        txtLocationType.Text = "";
        txtManagerName.Text = "";

        ddlParentLocation.SelectedIndex = 0;



        btnNew.Text = Resources.Attendance.New;
        lblSelectedRecord.Text = "";
        ViewState["Select"] = null;
        editid.Value = "";
        txtValue.Text = "";
        ddlOption.SelectedIndex = 2;
        ddlFieldName.SelectedIndex = 0;
        txtAddressName.Text = "";
        GvAddressName.DataSource = null;
        GvAddressName.DataBind();
        System.Web.UI.ScriptManager.GetCurrent(this).SetFocus(txtValue);



    }

    protected void txtLocation_OnTextChanged(object sender, EventArgs e)
    {

        if (editid.Value == "")
        {
            DataTable dt = objLocation.GetLocationMasterByLocationName(Session["CompId"].ToString().ToString(), txtLocationName.Text.Trim());
            if (dt.Rows.Count > 0)
            {
                txtLocationName.Text = "";
                DisplayMessage("Location Name Already Exists");
                System.Web.UI.ScriptManager.GetCurrent(this).SetFocus(txtLocationName);
                return;
            }
            DataTable dt1 = objLocation.GetLocationMasterInactive(Session["CompId"].ToString().ToString());
            dt1 = new DataView(dt1, "Location_Name='" + txtLocationName.Text + "'", "", DataViewRowState.CurrentRows).ToTable();
            if (dt1.Rows.Count > 0)
            {
                txtLocationName.Text = "";
                DisplayMessage("Location Name Already Exists - Go to Bin Tab");
                System.Web.UI.ScriptManager.GetCurrent(this).SetFocus(txtLocationName);
                return;
            }
            txtLocationNameL.Focus();
        }
        else
        {
            DataTable dtTemp = objLocation.GetLocationMasterById(Session["CompId"].ToString().ToString(), editid.Value);
            if (dtTemp.Rows.Count > 0)
            {
                if (dtTemp.Rows[0]["Location_Name"].ToString() != txtLocationName.Text)
                {
                    DataTable dt = objLocation.GetLocationMaster(Session["CompId"].ToString().ToString());
                    dt = new DataView(dt, "Location_Name='" + txtLocationName.Text + "'", "", DataViewRowState.CurrentRows).ToTable();
                    if (dt.Rows.Count > 0)
                    {
                        txtLocationName.Text = "";
                        DisplayMessage("Location Name Already Exists");
                        System.Web.UI.ScriptManager.GetCurrent(this).SetFocus(txtLocationName);
                        return;
                    }
                    DataTable dt1 = objLocation.GetLocationMasterInactive(Session["CompId"].ToString().ToString());
                    dt1 = new DataView(dt1, "Location_Name='" + txtLocationName.Text + "'", "", DataViewRowState.CurrentRows).ToTable();
                    if (dt1.Rows.Count > 0)
                    {
                        txtLocationName.Text = "";
                        DisplayMessage("Location Name Already Exists - Go to Bin Tab");
                        System.Web.UI.ScriptManager.GetCurrent(this).SetFocus(txtLocationName);
                        return;
                    }
                }
            }
            txtLocationNameL.Focus();
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

        txtLocationCode.Focus();
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
            DataTable dtCust = (DataTable)Session["Location"];


            DataView view = new DataView(dtCust, condition, "", DataViewRowState.CurrentRows);
            Session["dtFilter"] = view.ToTable();
            lblTotalRecords.Text = Resources.Attendance.Total_Records + ": " + view.ToTable().Rows.Count.ToString() + "";

            gvLocationMaster.DataSource = view.ToTable();
            gvLocationMaster.DataBind();
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
            DataTable dtCust = (DataTable)Session["dtbinLocation"];


            DataView view = new DataView(dtCust, condition, "", DataViewRowState.CurrentRows);
            Session["dtbinFilter"] = view.ToTable();
            lblbinTotalRecords.Text = Resources.Attendance.Total_Records + ": " + view.ToTable().Rows.Count.ToString() + "";

            gvLocationMasterBin.DataSource = view.ToTable();
            gvLocationMasterBin.DataBind();


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
                    b = objLocation.DeleteLocationMaster(Session["CompId"].ToString(), lblSelectedRecord.Text.Split(',')[j].Trim(), true.ToString(), Session["UserId"].ToString().ToString(), DateTime.Now.ToString());

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
            foreach (GridViewRow Gvr in gvLocationMasterBin.Rows)
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
                if (!lblSelectedRecord.Text.Split(',').Contains(dr["Location_Id"]))
                {
                    lblSelectedRecord.Text += dr["Location_Id"] + ",";
                }
            }
            for (int i = 0; i < gvLocationMasterBin.Rows.Count; i++)
            {
                string[] split = lblSelectedRecord.Text.Split(',');
                Label lblconid = (Label)gvLocationMasterBin.Rows[i].FindControl("lblLocationId");
                for (int j = 0; j < lblSelectedRecord.Text.Split(',').Length; j++)
                {
                    if (lblSelectedRecord.Text.Split(',')[j] != "")
                    {
                        if (lblconid.Text.Trim().ToString() == lblSelectedRecord.Text.Split(',')[j].Trim().ToString())
                        {
                            ((CheckBox)gvLocationMasterBin.Rows[i].FindControl("chkgvSelect")).Checked = true;
                        }
                    }
                }
            }
        }
        else
        {
            lblSelectedRecord.Text = "";
            DataTable dtUnit1 = (DataTable)Session["dtBinFilter"];
            gvLocationMasterBin.DataSource = dtUnit1;
            gvLocationMasterBin.DataBind();
            ViewState["Select"] = null;
        }
    }
    protected void btnSave_Click(object sender, EventArgs e)
    {
        int b = 0;

        if (txtLocationCode.Text == "")
        {
            DisplayMessage("Enter Location Code");
            txtLocationCode.Focus();
            return;
        }

        if (txtLocationName.Text == "")
        {
            DisplayMessage("Enter Location Name");
            txtLocationName.Focus();
            return;
        }


        string empid = string.Empty;
        if (txtManagerName.Text != "")
        {
            empid = txtManagerName.Text.Split('/')[txtManagerName.Text.Split('/').Length - 1];

            DataTable dtEmp = objEmp.GetEmployeeMaster(Session["CompId"].ToString());
            dtEmp = new DataView(dtEmp, "Brand_Id='" + Session["BrandId"].ToString() + "'", "", DataViewRowState.CurrentRows).ToTable();

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
            DataTable dt = objLocation.GetLocationMaster(Session["CompId"].ToString());
            dt = new DataView(dt, "Brand_Id='" + Session["BrandId"].ToString() + "'", "", DataViewRowState.CurrentRows).ToTable();

            dt = new DataView(dt, "Location_Code='" + txtLocationCode.Text + "'", "", DataViewRowState.CurrentRows).ToTable();
            if (dt.Rows.Count > 0)
            {
                DisplayMessage("Location Code Already Exists");
                txtLocationCode.Focus();
                return;

            }
            DataTable dt1 = objLocation.GetLocationMaster(Session["CompId"].ToString());
            dt1 = new DataView(dt1, "Brand_Id='" + Session["BrandId"].ToString() + "'", "", DataViewRowState.CurrentRows).ToTable();

            dt1 = new DataView(dt1, "Location_Name='" + txtLocationName.Text + "'", "", DataViewRowState.CurrentRows).ToTable();
            
            if (dt1.Rows.Count > 0)
            {
                DisplayMessage("Location Name Already Exists");
                txtLocationName.Focus();
                return;

            }




            b = objLocation.InsertLocationMaster(Session["CompId"].ToString(), txtLocationName.Text, txtLocationNameL.Text, txtLocationCode.Text,Session["BrandId"].ToString(),ddlParentLocation.SelectedValue,txtLocationType.Text,empid,"", "", "", "", "", false.ToString(), DateTime.Now.ToString(), true.ToString(), Session["UserId"].ToString(), DateTime.Now.ToString(), Session["UserId"].ToString(), DateTime.Now.ToString());

            if (b != 0)
            {
                string strMaxId = string.Empty;
                strMaxId = b.ToString();

                foreach (GridViewRow gvr in GvAddressName.Rows)
                {
                    Label lblGvAddressName = (Label)gvr.FindControl("lblgvAddressName");

                    if (lblGvAddressName.Text != "")
                    {
                        DataTable dtAddId = AM.GetAddressDataByAddressName(Session["CompId"].ToString(), lblGvAddressName.Text);
                        if (dtAddId.Rows.Count > 0)
                        {
                            string strAddressId = dtAddId.Rows[0]["Trans_Id"].ToString();
                            objAddChild.InsertAddressChild(strAddressId, "Location", strMaxId, "True", "", "", "", "", "", "True", DateTime.Now.ToString(), "True", Session["UserId"].ToString(), DateTime.Now.ToString(), Session["UserId"].ToString(), DateTime.Now.ToString());
                        }
                    }
                }

                DisplayMessage("Record Saved");
                FillGrid();
                Reset();
                btnList_Click(null, null);
            }
            else
            {
                DisplayMessage("Record Not Saved");
            }
        }
        else
        {
            DataTable dt = objLocation.GetLocationMaster(Session["CompId"].ToString());


            string LocationCode = string.Empty;
            string LocationName = string.Empty;

            try
            {
                LocationCode = (new DataView(dt, "Location_Id='" + editid.Value + "'", "", DataViewRowState.CurrentRows).ToTable()).Rows[0]["Location_Code"].ToString();
            }
            catch
            {
                LocationCode = "";
            }

            dt = new DataView(dt, "Location_Code='" + txtLocationCode.Text + "' and Location_Code<>'" + LocationCode + "' ", "", DataViewRowState.CurrentRows).ToTable();

            if (dt.Rows.Count > 0)
            {
                DisplayMessage("Location Code Already Exists");
                txtLocationCode.Focus();
                return;

            }



            DataTable dt1 = objLocation.GetLocationMaster(Session["CompId"].ToString());
            try
            {
                LocationName = (new DataView(dt1, "Location_Id='" + editid.Value + "'", "", DataViewRowState.CurrentRows).ToTable()).Rows[0]["Location_Name"].ToString();
            }
            catch
            {
                LocationName = "";
            }
            dt1 = new DataView(dt1, "Location_Name='" + txtLocationName.Text + "' and Location_Name<>'" + LocationName + "' ", "", DataViewRowState.CurrentRows).ToTable();
            if (dt1.Rows.Count > 0)
            {
                DisplayMessage("Location Name Already Exists");
                txtLocationName.Focus();
                return;

            }
            




            b = objLocation.UpdateLocationMaster(editid.Value,Session["CompId"].ToString(), txtLocationName.Text, txtLocationNameL.Text, txtLocationCode.Text, Session["BrandId"].ToString(), ddlParentLocation.SelectedValue, txtLocationType.Text,empid, "", "", "", "", "", false.ToString(), DateTime.Now.ToString(), true.ToString(), Session["UserId"].ToString(), DateTime.Now.ToString());

            if (b != 0)
            {



                objAddChild.DeleteAddressChild("Location", editid.Value);
                foreach (GridViewRow gvr in GvAddressName.Rows)
                {
                    Label lblGvAddressName = (Label)gvr.FindControl("lblgvAddressName");

                    if (lblGvAddressName.Text != "")
                    {
                        DataTable dtAddId = AM.GetAddressDataByAddressName(Session["CompId"].ToString(), lblGvAddressName.Text);
                        if (dtAddId.Rows.Count > 0)
                        {
                            string strAddressId = dtAddId.Rows[0]["Trans_Id"].ToString();
                            objAddChild.InsertAddressChild(strAddressId, "Location", editid.Value, "True", "", "", "", "", "", "True", DateTime.Now.ToString(), "True", Session["UserId"].ToString(), DateTime.Now.ToString(), Session["UserId"].ToString(), DateTime.Now.ToString());
                        }
                    }
                }



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
    public static string[] GetCompletionListLocationCode(string prefixText, int count, string contextKey)
    {
        LocationMaster objLocationMaster = new LocationMaster();
        DataTable dt = new DataView(objLocationMaster.GetLocationMaster(HttpContext.Current.Session["CompId"].ToString()), "Location_Code like '" + prefixText.ToString() + "%'", "", DataViewRowState.CurrentRows).ToTable();

        dt = new DataView(dt, "Brand_Id='" + HttpContext.Current.Session["BrandId"].ToString() + "'", "", DataViewRowState.CurrentRows).ToTable();

        string[] txt = new string[dt.Rows.Count];

        for (int i = 0; i < dt.Rows.Count; i++)
        {
            txt[i] = dt.Rows[i]["Location_Code"].ToString();
        }
        return txt;
    }
    [System.Web.Services.WebMethodAttribute(), System.Web.Script.Services.ScriptMethodAttribute()]
    public static string[] GetCompletionListLocationName(string prefixText, int count, string contextKey)
    {
        LocationMaster objLocationMaster = new LocationMaster();
        
        
        DataTable dt = new DataView(objLocationMaster.GetLocationMaster(HttpContext.Current.Session["CompId"].ToString()), "Location_Name like '" + prefixText.ToString() + "%'", "", DataViewRowState.CurrentRows).ToTable();

        dt = new DataView(dt, "Brand_Id='" + HttpContext.Current.Session["BrandId"].ToString() + "'", "", DataViewRowState.CurrentRows).ToTable();

        string[] txt = new string[dt.Rows.Count];

        for (int i = 0; i < dt.Rows.Count; i++)
        {
            txt[i] = dt.Rows[i]["Location_Name"].ToString();
        }
        return txt;
    }
    #region Add AddressName Concept
    protected void txtAddressName_TextChanged(object sender, EventArgs e)
    {
        if (txtAddressName.Text != "")
        {
            DataTable dtAM = AM.GetAddressDataByAddressName(Session["CompId"].ToString(), txtAddressName.Text);
            if (dtAM.Rows.Count > 0)
            {
                System.Web.UI.ScriptManager.GetCurrent(this).SetFocus(imgAddAddressName);
            }
            else
            {
                txtAddressName.Text = "";
                DisplayMessage("Choose In Suggestions Only");
                System.Web.UI.ScriptManager.GetCurrent(this).SetFocus(txtAddressName);
                return;
            }
        }
        else
        {
            DisplayMessage("Enter Address Name");
            System.Web.UI.ScriptManager.GetCurrent(this).SetFocus(txtAddressName);
        }
    }
    protected void imgAddAddressName_Click(object sender, EventArgs e)
    {
        if (txtAddressName.Text != "")
        {
            string strA = "0";
            foreach (GridViewRow gve in GvAddressName.Rows)
            {
                Label lblCAddressName = (Label)gve.FindControl("lblgvAddressName");
                if (txtAddressName.Text == lblCAddressName.Text)
                {
                    strA = "1";
                }
            }


            if (hdnAddressId.Value == "")
            {
                if (strA == "0")
                {
                    FillAddressChidGird("Save");
                    System.Web.UI.ScriptManager.GetCurrent(this).SetFocus(txtAddressName);
                }
                else
                {
                    txtAddressName.Text = "";
                    DisplayMessage("Address Name Already Exists");
                    System.Web.UI.ScriptManager.GetCurrent(this).SetFocus(txtAddressName);
                }
            }
            else
            {
                if (txtAddressName.Text == hdnAddressName.Value)
                {
                    FillAddressChidGird("Edit");
                    System.Web.UI.ScriptManager.GetCurrent(this).SetFocus(txtAddressName);
                }
                else
                {
                    if (strA == "0")
                    {
                        FillAddressChidGird("Edit");
                        System.Web.UI.ScriptManager.GetCurrent(this).SetFocus(txtAddressName);
                    }
                    else
                    {
                        txtAddressName.Text = "";
                        DisplayMessage("Address Name Already Exists");
                        System.Web.UI.ScriptManager.GetCurrent(this).SetFocus(txtAddressName);
                    }
                }
            }
        }
        else
        {
            DisplayMessage("Enter Address Name");
            System.Web.UI.ScriptManager.GetCurrent(this).SetFocus(txtAddressName);
        }
        txtAddressName.Focus();
    }
    public void ResetAddressName()
    {
        txtAddressName.Text = "";
        hdnAddressId.Value = "";
        hdnAddressName.Value = "";
    }
    public DataTable CreateAddressDataTable()
    {
        DataTable dt = new DataTable();
        dt.Columns.Add("Trans_Id");
        dt.Columns.Add("Address_Name");
        return dt;
    }
    public DataTable FillAddressDataTabel()
    {
        string strNewSNo = string.Empty;
        DataTable dt = CreateAddressDataTable();
        if (GvAddressName.Rows.Count > 0)
        {
            for (int i = 0; i < GvAddressName.Rows.Count + 1; i++)
            {
                if (dt.Rows.Count != GvAddressName.Rows.Count)
                {
                    dt.Rows.Add(i);
                    Label lblSNo = (Label)GvAddressName.Rows[i].FindControl("lblSNo");
                    Label lblAddressName = (Label)GvAddressName.Rows[i].FindControl("lblgvAddressName");

                    dt.Rows[i]["Trans_Id"] = lblSNo.Text;
                    strNewSNo = lblSNo.Text;
                    dt.Rows[i]["Address_Name"] = lblAddressName.Text;
                }
                else
                {
                    dt.Rows.Add(i);
                    dt.Rows[i]["Trans_Id"] = (float.Parse(strNewSNo) + 1).ToString();
                    dt.Rows[i]["Address_Name"] = txtAddressName.Text;
                }
            }
        }
        else
        {
            dt.Rows.Add(0);
            dt.Rows[0]["Trans_Id"] = "1";
            dt.Rows[0]["Address_Name"] = txtAddressName.Text;
        }
        if (dt.Rows.Count > 0)
        {
            GvAddressName.DataSource = dt;
            GvAddressName.DataBind();
        }
        return dt;
    }
    public DataTable FillAddressDataTabelDelete()
    {
        DataTable dt = CreateAddressDataTable();
        for (int i = 0; i < GvAddressName.Rows.Count; i++)
        {
            dt.Rows.Add(i);
            Label lblSNo = (Label)GvAddressName.Rows[i].FindControl("lblSNo");
            Label lblgvAddressName = (Label)GvAddressName.Rows[i].FindControl("lblgvAddressName");


            dt.Rows[i]["Trans_Id"] = lblSNo.Text;
            dt.Rows[i]["Address_Name"] = lblgvAddressName.Text;
        }

        DataView dv = new DataView(dt);
        dv.RowFilter = "Trans_Id<>'" + hdnAddressId.Value + "'";
        dt = (DataTable)dv.ToTable();
        return dt;
    }
    protected void btnAddressEdit_Command(object sender, CommandEventArgs e)
    {
        hdnAddressId.Value = e.CommandArgument.ToString();
        FillAddressDataTabelEdit();
    }
    public DataTable FillAddressDataTabelEdit()
    {
        DataTable dt = CreateAddressDataTable();

        for (int i = 0; i < GvAddressName.Rows.Count; i++)
        {
            dt.Rows.Add(i);
            Label lblSNo = (Label)GvAddressName.Rows[i].FindControl("lblSNo");
            Label lblgvAddressName = (Label)GvAddressName.Rows[i].FindControl("lblgvAddressName");

            dt.Rows[i]["Trans_Id"] = lblSNo.Text;
            dt.Rows[i]["Address_Name"] = lblgvAddressName.Text;
        }

        DataView dv = new DataView(dt);
        dv.RowFilter = "Trans_Id='" + hdnAddressId.Value + "'";
        dt = (DataTable)dv.ToTable();
        if (dt.Rows.Count != 0)
        {
            txtAddressName.Text = dt.Rows[0]["Address_Name"].ToString();
            hdnAddressName.Value = dt.Rows[0]["Address_Name"].ToString();
        }
        return dt;
    }
    protected void btnAddressDelete_Command(object sender, CommandEventArgs e)
    {
        hdnAddressId.Value = e.CommandArgument.ToString();
        FillAddressChidGird("Del");
    }
    public void FillAddressChidGird(string CommandName)
    {
        DataTable dt = new DataTable();
        if (CommandName.ToString() == "Del")
        {
            dt = FillAddressDataTabelDelete();
        }
        else if (CommandName.ToString() == "Edit")
        {
            dt = FillAddressDataTableUpdate();
        }
        else
        {
            dt = FillAddressDataTabel();
        }
        GvAddressName.DataSource = dt;
        GvAddressName.DataBind();

        ResetAddressName();
    }
    public DataTable FillAddressDataTableUpdate()
    {
        DataTable dt = CreateAddressDataTable();
        for (int i = 0; i < GvAddressName.Rows.Count; i++)
        {
            dt.Rows.Add(i);
            Label lblSNo = (Label)GvAddressName.Rows[i].FindControl("lblSNo");
            Label lblgvAddressName = (Label)GvAddressName.Rows[i].FindControl("lblgvAddressName");

            dt.Rows[i]["Trans_Id"] = lblSNo.Text;
            dt.Rows[i]["Address_Name"] = lblgvAddressName.Text;
        }
        for (int i = 0; i < dt.Rows.Count; i++)
        {
            if (hdnAddressId.Value == dt.Rows[i]["Trans_Id"].ToString())
            {
                dt.Rows[i]["Address_Name"] = txtAddressName.Text;
            }
        }
        return dt;
    }
    #endregion

    #region Add New Address Concept
    protected void btnAddNewAddress_Click(object sender, EventArgs e)
    {
        pnlAddress1.Visible = true;
        pnlAddress2.Visible = true;
        FillAddressCategory();
        FillCountry();
    }
    protected void txtAddressNameNew_TextChanged(object sender, EventArgs e)
    {
        DataTable dt = AM.GetAddressDataByAddressName(Session["CompId"].ToString(), txtAddressNameNew.Text);
        if (dt.Rows.Count > 0)
        {
            txtAddressNameNew.Text = "";
            DisplayMessage("Address Name Already Exists");
            System.Web.UI.ScriptManager.GetCurrent(this).SetFocus(txtAddressNameNew);
            return;
        }

        DataTable dt1 = AM.GetAddressFalseAllData(Session["CompId"].ToString());
        dt1 = new DataView(dt1, "Address_Name='" + txtAddressNameNew.Text + "'", "", DataViewRowState.CurrentRows).ToTable();
        if (dt1.Rows.Count > 0)
        {
            txtAddressNameNew.Text = "";
            DisplayMessage("Address Name Already In Deleted Section");
            System.Web.UI.ScriptManager.GetCurrent(this).SetFocus(txtAddressNameNew);
            return;
        }
        System.Web.UI.ScriptManager.GetCurrent(this).SetFocus(txtAddress);
    }
    protected void btnAddressSave_Click(object sender, EventArgs e)
    {
        if (ddlAddressCategory.SelectedValue == "--Select--")
        {
            DisplayMessage("Select Address Category");
            System.Web.UI.ScriptManager.GetCurrent(this).SetFocus(ddlAddressCategory);
            return;
        }
        if (txtAddressNameNew.Text == "" || txtAddressNameNew.Text == null)
        {
            DisplayMessage("Enter Address Name");
            System.Web.UI.ScriptManager.GetCurrent(this).SetFocus(txtAddressNameNew);
            return;
        }

        if (txtAddress.Text == "" || txtAddress.Text == null)
        {
            DisplayMessage("Enter Address");
            System.Web.UI.ScriptManager.GetCurrent(this).SetFocus(txtAddress);
            return;
        }

        string strCountryId = string.Empty;
        if (ddlCountry.SelectedValue == "--Select--")
        {
            strCountryId = "0";
        }
        else
        {
            strCountryId = ddlCountry.SelectedValue;
        }
        if (txtEmailId1.Text != "")
        {
            if (!CheckEmailId1(txtEmailId1.Text))
            {
                DisplayMessage("Email Id 1 is invalid");
                txtEmailId1.Text = "";
                txtEmailId1.Focus();
                return;
            }
        }
        if (txtEmailId2.Text != "")
        {
            if (!CheckEmailId2(txtEmailId2.Text))
            {
                DisplayMessage("Email Id 2 is invalid");
                txtEmailId2.Text = "";
                txtEmailId2.Focus();
                return;
            }
        }
        string strLongitude = string.Empty;
        if (txtLongitude.Text != "")
        {
            strLongitude = txtLongitude.Text;
        }
        else
        {
            strLongitude = "0";
        }
        string strLatitude = string.Empty;
        if (txtLatitude.Text != "")
        {
            strLatitude = txtLatitude.Text;
        }
        else
        {
            strLatitude = "0";
        }

        int b = 0;

        DataTable dtadd2 = AM.GetAddressDataByAddressName(Session["CompId"].ToString(), txtAddressName.Text);
        if (dtadd2.Rows.Count > 0)
        {
            txtAddressName.Text = "";
            DisplayMessage("Address Name Already Exists");
            System.Web.UI.ScriptManager.GetCurrent(this).SetFocus(txtAddressName);
            return;
        }
        else
        {
            b = AM.InsertAddressMaster(Session["CompId"].ToString(), ddlAddressCategory.SelectedValue, txtAddressNameNew.Text, txtAddress.Text, txtStreet.Text, txtBlock.Text, txtAvenue.Text, strCountryId, txtState.Text, txtCity.Text, txtPinCode.Text, txtPhoneNo1.Text, txtPhoneNo2.Text, txtMobileNo1.Text, txtMobileNo2.Text, txtEmailId1.Text, txtEmailId2.Text, txtFaxNo.Text, txtWebsite.Text, strLongitude, strLatitude, "", "", "", "", "", "True", DateTime.Now.ToString(), "True", Session["UserId"].ToString(), DateTime.Now.ToString(), Session["UserId"].ToString(), DateTime.Now.ToString());
            if (b != 0)
            {
                DisplayMessage("Record Saved");
                txtAddressName.Text = txtAddressNameNew.Text;
                ResetAddress();
                pnlAddress1.Visible = false;
                pnlAddress2.Visible = false;
                System.Web.UI.ScriptManager.GetCurrent(this).SetFocus(ddlAddressCategory);
            }
            else
            {
                DisplayMessage("Record  Not Saved");
            }
        }
    }
    protected void btnAddressReset_Click(object sender, EventArgs e)
    {
        ResetAddress();
        System.Web.UI.ScriptManager.GetCurrent(this).SetFocus(ddlAddressCategory);
    }
    protected void btnAddressCancel_Click(object sender, EventArgs e)
    {
        ResetAddress();
        pnlAddress1.Visible = false;
        pnlAddress2.Visible = false;
    }
    protected void ResetAddress()
    {
        FillAddressCategory();
        txtAddressNameNew.Text = "";
        txtAddress.Text = "";
        txtStreet.Text = "";
        txtBlock.Text = "";
        txtAvenue.Text = "";
        FillCountry();
        txtState.Text = "";
        txtCity.Text = "";
        txtPinCode.Text = "";
        txtPhoneNo1.Text = "";
        txtPhoneNo2.Text = "";
        txtMobileNo1.Text = "";
        txtMobileNo2.Text = "";
        txtEmailId1.Text = "";
        txtEmailId2.Text = "";
        txtFaxNo.Text = "";
        txtWebsite.Text = "";
        txtLongitude.Text = "";
        txtLatitude.Text = "";

        System.Web.UI.ScriptManager.GetCurrent(this).SetFocus(ddlAddressCategory);
    }
    public bool CheckEmailId1(string EmailAddress)
    {
        return Regex.IsMatch(txtEmailId1.Text,
                      "\\w+([-+.']\\+)*@\\w+([-.]\\+)*\\.\\w+([-.]\\+)*");
    }
    public bool CheckEmailId2(string EmailAddress)
    {
        return Regex.IsMatch(txtEmailId2.Text,
                      "\\w+([-+.']\\+)*@\\w+([-.]\\+)*\\.\\w+([-.]\\+)*");
    }

    private void FillAddressCategory()
    {
        DataTable dsAddressCat = null;
        dsAddressCat = ObjAddressCat.GetAddressCategoryAll(Session["CompId"].ToString());
        if (dsAddressCat.Rows.Count > 0)
        {
            ddlAddressCategory.DataSource = dsAddressCat;
            ddlAddressCategory.DataTextField = "Address_Name";
            ddlAddressCategory.DataValueField = "Address_Category_Id";
            ddlAddressCategory.DataBind();
            ListItem li = new ListItem("--Select--", "0");
            ddlAddressCategory.Items.Insert(0, li);
            ddlAddressCategory.SelectedIndex = 0;
        }
        else
        {
            ddlAddressCategory.Items.Add("--Select--");

        }
    }
    private void FillCountry()
    {
        CountryMaster objCountry = new CountryMaster();
        DataTable dsCountry = null;
        dsCountry = objCountry.GetCountryMaster();
        if (dsCountry.Rows.Count > 0)
        {
            ddlCountry.DataSource = dsCountry;
            ddlCountry.DataTextField = "Country_Name";
            ddlCountry.DataValueField = "Country_Id";
            ddlCountry.DataBind();
            ListItem li = new ListItem("--Select--", "0");
            ddlCountry.Items.Insert(0, li);
            ddlCountry.SelectedIndex = 0;
        }
        else
        {
            ListItem li = new ListItem("--Select--", "0");
            ddlCountry.Items.Insert(0, li);
            ddlCountry.SelectedIndex = 0;
        }
    }
    protected void btnClosePanel_Click(object sender, EventArgs e)
    {
        pnlAddress1.Visible = false;
        pnlAddress2.Visible = false;
    }
    #endregion

    [System.Web.Services.WebMethodAttribute(), System.Web.Script.Services.ScriptMethodAttribute()]
    public static string[] GetCompletionListAddressName(string prefixText, int count, string contextKey)
    {
        Set_AddressMaster AddressN = new Set_AddressMaster();
        DataTable dt = AddressN.GetDistinctAddressName(HttpContext.Current.Session["CompId"].ToString(), prefixText);



        string[] str = new string[dt.Rows.Count];
        if (dt.Rows.Count > 0)
        {
            for (int i = 0; i < dt.Rows.Count; i++)
            {
                str[i] = dt.Rows[i]["Address_Name"].ToString();
            }
        }
        else
        {
            if (prefixText.Length > 2)
            {
                str = null;
            }
            else
            {
                dt = AddressN.GetAddressAllData(HttpContext.Current.Session["CompId"].ToString());
                if (dt.Rows.Count > 0)
                {
                    str = new string[dt.Rows.Count];
                    for (int i = 0; i < dt.Rows.Count; i++)
                    {
                        str[i] = dt.Rows[i]["Address_Name"].ToString();
                    }
                }
            }
        }
        return str;
    }

    [System.Web.Services.WebMethodAttribute(), System.Web.Script.Services.ScriptMethodAttribute()]
    public static string[] GetCompletionListNewAddress(string prefixText, int count, string contextKey)
    {
        Set_AddressMaster Address = new Set_AddressMaster();
        DataTable dt = Address.GetDistinctAddressName(HttpContext.Current.Session["CompId"].ToString(), prefixText);

        string[] str = new string[dt.Rows.Count];
        for (int i = 0; i < dt.Rows.Count; i++)
        {
            str[i] = dt.Rows[i]["Address_Name"].ToString();
        }
        return str;
    }
   
}
