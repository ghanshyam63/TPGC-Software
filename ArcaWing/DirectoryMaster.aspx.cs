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

public partial class New_ArcWing_DirectoryMaster : System.Web.UI.Page
{
    string StrBrandId = "1";
    string StrLocId = "1";

    string strCompId = string.Empty;
    Common cmn = new Common();
    Arc_Directory_Master objDir = new Arc_Directory_Master();
    SystemParameter objSys = new SystemParameter();
    EmployeeMaster objEmp = new EmployeeMaster();
    protected void Page_Load(object sender, EventArgs e)
    {
        Session["BrandId"] = StrBrandId;
        Session["LocId"] = StrLocId;
        strCompId = Session["CompId"].ToString();
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

        }
    }

    public void AllPageCode()
    {

        Page.Title = objSys.GetSysTitle();
        Session["AccordianId"] = "17";
        Session["HeaderText"] = "ArcaWing";
        DataTable dtAllPageCode = cmn.GetAllPagePermission(Session["UserId"].ToString(), "17", "62");
        if (dtAllPageCode.Rows.Count != 0)
        {
            if (dtAllPageCode.Rows[0][0].ToString() == "SuperAdmin")
            {
                btnSave.Visible = true;
                foreach (GridViewRow Row in gvDirMaster.Rows)
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
                    foreach (GridViewRow Row in gvDirMaster.Rows)
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
    public void FillGrid()
    {
        string Directoryid = "0";
        DataTable dt = objDir.getDirectoryMaster(strCompId, Directoryid);
        //dt = new DataView(dt, "Brand_Id='" + Session["BrandId"].ToString() + "' and Location_Id='" + Session["LocId"].ToString() + "' ", "", DataViewRowState.CurrentRows).ToTable();

        gvDirMaster.DataSource = dt;
        gvDirMaster.DataBind();
        AllPageCode();
        Session["dtFilter"] = dt;
        Session["Dir"] = dt;

        lblTotalRecords.Text = Resources.Attendance.Total_Records + ": " + dt.Rows.Count.ToString() + "";

    }
    public void FillGridBin()
    {
        string Directoryid = "0";

        DataTable dt = new DataTable();
        dt = objDir.GetDirectoryMasterInActive(strCompId, Directoryid);
        //dt = new DataView(dt, "Brand_Id='" + Session["BrandId"].ToString() + "' and Location_Id='" + Session["LocId"].ToString() + "' ", "", DataViewRowState.CurrentRows).ToTable();

        gvDirMasterBin.DataSource = dt;
        gvDirMasterBin.DataBind();


        Session["dtbinFilter"] = dt;
        Session["dtbinDir"] = dt;
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
        gvDirMaster.PageIndex = e.NewPageIndex;

        DataTable dt = (DataTable)Session["dtFilter"];
        gvDirMaster.DataSource = dt;
        gvDirMaster.DataBind();
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
        gvDirMaster.DataSource = dt;
        gvDirMaster.DataBind();
        AllPageCode();
    }
    protected void gvDepMasterBin_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {

        gvDirMasterBin.PageIndex = e.NewPageIndex;
        if (HDFSortbin.Value == "")
            FillGridBin();
        else
        {
            DataTable dt = (DataTable)Session["dtbinFilter"];
            gvDirMasterBin.DataSource = dt;
            gvDirMasterBin.DataBind();
            AllPageCode();
        }
        string temp = string.Empty;
        
        for (int i = 0; i < gvDirMasterBin.Rows.Count; i++)
        {
            Label lblconid = (Label)gvDirMasterBin.Rows[i].FindControl("lblDirId");
            string[] split = lblSelectedRecord.Text.Split(',');

            for (int j = 0; j < lblSelectedRecord.Text.Split(',').Length; j++)
            {
                if (lblSelectedRecord.Text.Split(',')[j] != "")
                {
                    if (lblconid.Text.Trim().ToString() == lblSelectedRecord.Text.Split(',')[j].Trim().ToString())
                    {
                        ((CheckBox)gvDirMasterBin.Rows[i].FindControl("chkgvSelect")).Checked = true;
                    }
                }
            }
        }

    }
    protected void gvDepMasterBin_OnSorting(object sender, GridViewSortEventArgs e)
    {
        string Documentid = "0";
        lblSelectedRecord.Text = "";
        HDFSortbin.Value = HDFSortbin.Value == "ASC" ? "DESC" : "ASC";
        DataTable dt = new DataTable();
        dt = objDir.GetDirectoryMasterInActive(strCompId, Documentid);
        //dt = new DataView(dt, "Brand_Id='" + Session["BrandId"].ToString() + "' and Location_Id='" + Session["LocId"].ToString() + "' ", "", DataViewRowState.CurrentRows).ToTable();

        DataView dv = new DataView(dt);
        string Query = "" + e.SortExpression + " " + HDFSortbin.Value + "";
        dv.Sort = Query;
        dt = dv.ToTable();
        Session["dtbinFilter"] = dt;
        gvDirMasterBin.DataSource = dt;
        gvDirMasterBin.DataBind();
        AllPageCode();

    }

    protected void chkgvSelectAll_CheckedChanged(object sender, EventArgs e)
    {
        CheckBox chkSelAll = ((CheckBox)gvDirMasterBin.HeaderRow.FindControl("chkgvSelectAll"));
        for (int i = 0; i < gvDirMasterBin.Rows.Count; i++)
        {
            ((CheckBox)gvDirMasterBin.Rows[i].FindControl("chkgvSelect")).Checked = chkSelAll.Checked;
            if (chkSelAll.Checked)
            {
                if (!lblSelectedRecord.Text.Split(',').Contains(((Label)(gvDirMasterBin.Rows[i].FindControl("lblDirId"))).Text.Trim().ToString()))
                {
                    lblSelectedRecord.Text += ((Label)(gvDirMasterBin.Rows[i].FindControl("lblDirId"))).Text.Trim().ToString() + ",";
                }
            }
            else
            {
                string temp = string.Empty;
                string[] split = lblSelectedRecord.Text.Split(',');
                foreach (string item in split)
                {
                    if (item != ((Label)(gvDirMasterBin.Rows[i].FindControl("lblDirId"))).Text.Trim().ToString())
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
        Label lb = (Label)gvDirMasterBin.Rows[index].FindControl("lblDirId");
        if (((CheckBox)gvDirMasterBin.Rows[index].FindControl("chkgvSelect")).Checked)
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
        DataTable dt = objDir.GetDirectoryMaster_By_DirectoryId(strCompId, editid.Value);
        if (dt.Rows.Count > 0)
        {
            try
            {
                txtDirectName.Text = dt.Rows[0]["Directory_Name"].ToString();
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
        b = objDir.DeleteDirectoryMaster(strCompId, e.CommandArgument.ToString(), false.ToString(), Session["UserId"].ToString(), DateTime.Now.ToString());
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
        txtDirectName.Text = "";
        btnNew.Text = Resources.Attendance.New;
        lblSelectedRecord.Text = "";
        ViewState["Select"] = null;
        editid.Value = "";
        txtValue.Text = "";
        txtDirectName.Focus();
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
        txtDirectName.Focus();
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
        txtValue.Focus();
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
            DataTable dtCust = (DataTable)Session["Dir"];


            DataView view = new DataView(dtCust, condition, "", DataViewRowState.CurrentRows);
            Session["dtFilter"] = view.ToTable();
            lblTotalRecords.Text = Resources.Attendance.Total_Records + ": " + view.ToTable().Rows.Count.ToString() + "";

            gvDirMaster.DataSource = view.ToTable();
            gvDirMaster.DataBind();
            btnRefresh.Focus();

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
            DataTable dtCust = (DataTable)Session["dtbinDir"];


            DataView view = new DataView(dtCust, condition, "", DataViewRowState.CurrentRows);
            Session["dtbinFilter"] = view.ToTable();
            lblbinTotalRecords.Text = Resources.Attendance.Total_Records + ": " + view.ToTable().Rows.Count.ToString() + "";

            gvDirMasterBin.DataSource = view.ToTable();
            gvDirMasterBin.DataBind();


            if (view.ToTable().Rows.Count == 0)
            {
                imgBtnRestore.Visible = false;
                ImgbtnSelectAll.Visible = false;
            }
            else
            {
                AllPageCode();
            }
            btnbinRefresh.Focus();
        }
    }

    protected void btnbinRefresh_Click(object sender, ImageClickEventArgs e)
    {
        FillGrid();
        FillGridBin();

        ddlbinOption.SelectedIndex = 2;
        ddlbinFieldName.SelectedIndex = 0;
        txtbinValue.Text = "";
        txtbinValue.Focus();
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
                    b = objDir.DeleteDirectoryMaster(strCompId, lblSelectedRecord.Text.Split(',')[j].Trim(), true.ToString(), Session["UserId"].ToString(), DateTime.Now.ToString());

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
            foreach (GridViewRow Gvr in gvDirMasterBin.Rows)
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
                if (!lblSelectedRecord.Text.Split(',').Contains(dr["Id"]))
                {
                    lblSelectedRecord.Text += dr["Id"] + ",";
                }
            }
            for (int i = 0; i < gvDirMasterBin.Rows.Count; i++)
            {
                string[] split = lblSelectedRecord.Text.Split(',');
                Label lblconid = (Label)gvDirMasterBin.Rows[i].FindControl("lblDirId");
                for (int j = 0; j < lblSelectedRecord.Text.Split(',').Length; j++)
                {
                    if (lblSelectedRecord.Text.Split(',')[j] != "")
                    {
                        if (lblconid.Text.Trim().ToString() == lblSelectedRecord.Text.Split(',')[j].Trim().ToString())
                        {
                            ((CheckBox)gvDirMasterBin.Rows[i].FindControl("chkgvSelect")).Checked = true;
                        }
                    }
                }
            }
        }
        else
        {
            lblSelectedRecord.Text = "";
            DataTable dtUnit1 = (DataTable)Session["dtBinFilter"];
            gvDirMasterBin.DataSource = dtUnit1;
            gvDirMasterBin.DataBind();
            ViewState["Select"] = null;
        }
    }
    protected void btnSave_Click(object sender, EventArgs e)
    {
        int b = 0;
        if (txtDirectName.Text == "")
        {
            DisplayMessage("Enter Directory Name");
            txtDirectName.Focus();
            return;
        }


        DataTable DtInactive = new DataTable();

        DtInactive = objDir.GetDirectoryMasterInActive(strCompId, "0");
        DtInactive = new DataView(DtInactive, "Directory_Name='" + txtDirectName.Text + "'", "", DataViewRowState.CurrentRows).ToTable();
        if (DtInactive.Rows.Count > 0)
        {
            DisplayMessage("Already Exists - Go to bin section");
            txtDirectName.Focus();
            txtDirectName.Text = "";
            return;

        }


        if (editid.Value == "")
        {

            DataTable dt = objDir.getDirectoryMaster(strCompId, "0");


            dt = new DataView(dt, "Directory_Name='" + txtDirectName.Text + "'", "", DataViewRowState.CurrentRows).ToTable();
            if (dt.Rows.Count > 0)
            {
                DisplayMessage("Directory Name Already Exists");
                txtDirectName.Focus();
                txtDirectName.Text = "";
                return;

            }






            b = objDir.InsertDirectorymaster(strCompId, txtDirectName.Text, "", "0", "", "", "", false.ToString(), DateTime.Now.ToString(), true.ToString(), Session["UserId"].ToString(), DateTime.Now.ToString(), Session["UserId"].ToString(), DateTime.Now.ToString());

            if (b != 0)
            {
                string NewDirectory = Server.MapPath(txtDirectName.Text);

                int i = CreateDirectoryIfNotExist(NewDirectory);

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
            string Documentid = "0";
            DataTable dt = objDir.getDirectoryMaster(strCompId, "0");


            string DirectoryName = string.Empty;






            try
            {
                DirectoryName = (new DataView(dt, "Id='" + editid.Value + "'", "", DataViewRowState.CurrentRows).ToTable()).Rows[0]["Directory_Name"].ToString();
            }
            catch
            {
                DirectoryName = "";
            }
            dt = new DataView(dt, "Directory_Name='" + txtDirectName.Text + "' and Directory_Name<>'" + DirectoryName.ToString() + "'  ", "", DataViewRowState.CurrentRows).ToTable();
            if (dt.Rows.Count > 0)
            {
                DisplayMessage("Directory Name Already Exists");
                txtDirectName.Text = "";
                txtDirectName.Focus();
                return;

            }



            DataTable Dt = objDir.GetDirectoryMaster_By_DirectoryId(strCompId, editid.Value);
            if (txtDirectName.Text != Dt.Rows[0]["Directory_Name"].ToString())
            {

                string Createdirectory = Server.MapPath(txtDirectName.Text.Trim());
                string OLdDirectory = Server.MapPath(Dt.Rows[0]["Directory_Name"].ToString());
                Directory.Move(OLdDirectory, Createdirectory);
            }

            b = objDir.UpdateDirectoryMaster(strCompId, editid.Value, txtDirectName.Text, "", "0", "", "", "", false.ToString(), DateTime.Now.ToString(), true.ToString(), Session["UserId"].ToString(), DateTime.Now.ToString());

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
    public static string[] GetCompletionListDirName(string prefixText, int count, string contextKey)
    {
        Arc_Directory_Master objdir = new Arc_Directory_Master();
        DataTable dt = objdir.getDirectoryMaster(HttpContext.Current.Session["CompId"].ToString(), "0");
        dt = new DataView(dt, "Directory_Name like '" + prefixText.ToString() + "%'", "", DataViewRowState.CurrentRows).ToTable();

        string[] txt = new string[dt.Rows.Count];

        for (int i = 0; i < dt.Rows.Count; i++)
        {
            txt[i] = dt.Rows[i]["Directory_Name"].ToString();
        }
        return txt;
    }
    public int CreateDirectoryIfNotExist(string NewDirectory)
    {
        int checkDirectory = 0;
        try
        {
            // Checking the existance of directory
            if (!Directory.Exists(NewDirectory))
            {
                //If No any such directory then creates the new one
                Directory.CreateDirectory(NewDirectory);


            }
            else
            {

                checkDirectory = 1;
            }
        }
        catch (IOException _err)
        {
            Response.Write(_err.Message);
        }
        return checkDirectory;
    }

    [System.Web.Services.WebMethodAttribute(), System.Web.Script.Services.ScriptMethodAttribute()]
    public static string[] GetCompletionListDirectoryName(string prefixText, int count, string contextKey)
    {
        Arc_Directory_Master objdir = new Arc_Directory_Master();
        DataTable dt = objdir.getDirectoryMaster(HttpContext.Current.Session["CompId"].ToString(), "0");
        dt = new DataView(dt, "Directory_Name like '" + prefixText.ToString() + "%'", "", DataViewRowState.CurrentRows).ToTable();

        string[] txt = new string[dt.Rows.Count];

        for (int i = 0; i < dt.Rows.Count; i++)
        {
            txt[i] = dt.Rows[i]["Directory_Name"].ToString();
        }
        return txt;
    }







}
