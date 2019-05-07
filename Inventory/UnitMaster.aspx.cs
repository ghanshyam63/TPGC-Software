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
using PegasusDataAccess;

public partial class Inventory_UnitMaster : BasePage
{
    #region defind Class Object
    DataAccessClass daClass = new DataAccessClass();
    Inv_UnitMaster ObjInvUnitMaster = new Inv_UnitMaster();
    SystemParameter ObjSysPeram = new SystemParameter();
    Common cmn = new Common();
    string StrCompId = string.Empty;
    string StrUserId = string.Empty;

    #endregion
    protected void Page_Load(object sender, EventArgs e)
    {
        if (Session["UserId"] == null)
        {
            Response.Redirect("~/ERPLogin.aspx");
        }
        if (!IsPostBack)
        {
            StrUserId = Session["UserId"].ToString();
            StrCompId = Session["CompId"].ToString();

            FillConversionUnitDDL("");
            FillGridBin();
            FillGrid();
            btnList_Click(null, null);

        }
        AllPageCode();
    }

    #region AllPageCode
    public void AllPageCode()
    {
        Page.Title = ObjSysPeram.GetSysTitle();
        StrUserId = Session["UserId"].ToString();
        StrCompId = Session["CompId"].ToString();
        Session["AccordianId"] = "11";
        Session["HeaderText"] = "Inventory";
        DataTable dtAllPageCode = cmn.GetAllPagePermission(StrUserId.ToString(), "11", "21");
        if (dtAllPageCode.Rows.Count != 0)
        {
            if (dtAllPageCode.Rows[0][0].ToString() == "SuperAdmin")
            {
                btnSave.Visible = true;
                foreach (GridViewRow Row in gvunitMaster.Rows)
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
                    foreach (GridViewRow Row in gvunitMaster.Rows)
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
    #endregion


    #region System defind Funcation
    protected void btnList_Click(object sender, EventArgs e)
    {
        pnlMenuList.BackColor = System.Drawing.ColorTranslator.FromHtml("#ccddee");

        pnlMenuNew.BackColor = System.Drawing.ColorTranslator.FromHtml("#90bde9");
        pnlMenuBin.BackColor = System.Drawing.ColorTranslator.FromHtml("#90bde9");

        PnlList.Visible = true;
        PnlNewEdit.Visible = false;
        PnlBin.Visible = false;
        txtValue.Focus();
    }
    protected void btnNew_Click(object sender, EventArgs e)
    {
        pnlMenuNew.BackColor = System.Drawing.ColorTranslator.FromHtml("#ccddee");

        pnlMenuList.BackColor = System.Drawing.ColorTranslator.FromHtml("#90bde9");
        pnlMenuBin.BackColor = System.Drawing.ColorTranslator.FromHtml("#90bde9");

        PnlList.Visible = false;
        PnlNewEdit.Visible = true;
        PnlBin.Visible = false;
        txtEUnitName.Focus();

    }
    protected void btnBin_Click(object sender, EventArgs e)
    {
        pnlMenuBin.BackColor = System.Drawing.ColorTranslator.FromHtml("#ccddee");

        pnlMenuNew.BackColor = System.Drawing.ColorTranslator.FromHtml("#90bde9");
        pnlMenuList.BackColor = System.Drawing.ColorTranslator.FromHtml("#90bde9");
        PnlNewEdit.Visible = false;
        PnlBin.Visible = true;
        PnlList.Visible = false;
        FillGridBin();
        txtbinValue.Focus();
    }

    protected void btnEdit_Command(object sender, CommandEventArgs e)
    {

        //System.Threading.Thread.Sleep(20000);
        editid.Value = e.CommandArgument.ToString();
        DataTable dt = ObjInvUnitMaster.GetUnitMasterById(StrCompId.ToString(), editid.Value);
        if (dt.Rows.Count != 0)
        {
            txtEUnitName.Text = dt.Rows[0]["Unit_Name"].ToString();
            txtConversion_Qty.Text = dt.Rows[0]["Coversion_Qty"].ToString();

            try
            {

                FillConversionUnitDDL(e.CommandArgument.ToString());
                if (dt.Rows[0]["Conversion_Unit"].ToString() != "0")
                {
                    ddlConversion_Unit.SelectedValue = dt.Rows[0]["Conversion_Unit"].ToString();
                }
                else
                {
                    ddlConversion_Unit.SelectedValue = "--Select--";
                }
            }
            catch
            {
                ddlConversion_Unit.SelectedValue = "--Select--";
            }

            txtlUnitName.Text = dt.Rows[0]["Unit_Name_L"].ToString();
            btnNew_Click(null, null);
            btnNew.Text = Resources.Attendance.Edit;
        }

    }
    protected void IbtnDelete_Command(object sender, CommandEventArgs e)
    {
        int b = 0;
        b = ObjInvUnitMaster.DeleteUnitMaster(StrCompId.ToString(), e.CommandArgument.ToString(), false.ToString(), Session["UserId"].ToString(), DateTime.Now.ToString());
        if (b != 0)
        {
            DisplayMessage("Record Deleted");
            FillConversionUnitDDL("");
            FillGridBin();
            FillGrid();
            Reset();
            try
            {
                int i = ((GridViewRow)((ImageButton)sender).Parent.Parent).RowIndex;
                ((ImageButton)gvunitMaster.Rows[i].FindControl("IbtnDelete")).Focus();
            }
            catch
            {
                txtValue.Focus();
            }
        }
        else
        {
            DisplayMessage("Record Not Delete");
        }
    }
    protected void btnRefresh_Click(object sender, ImageClickEventArgs e)
    {
        FillGrid();
        FillGridBin();
        FillConversionUnitDDL("");
        ddlOption.SelectedIndex = 3;
        ddlFieldName.SelectedIndex = 0;
        txtValue.Text = "";
        txtValue.Focus();
    }
    protected void btnReset_Click(object sender, EventArgs e)
    {
        Reset();
        txtEUnitName.Focus();
    }
    protected void btnCancel_Click(object sender, EventArgs e)
    {
        FillGrid();
        FillGridBin();
        Reset();
        btnList_Click(null, null);

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
            DataTable dtCust = (DataTable)Session["Unit"];


            DataView view = new DataView(dtCust, condition, "", DataViewRowState.CurrentRows);
            Session["dtFilter"] = view.ToTable();
            lblTotalRecords.Text = Resources.Attendance.Total_Records + ": " + view.ToTable().Rows.Count.ToString() + "";

            gvunitMaster.DataSource = view.ToTable();
            gvunitMaster.DataBind();

            AllPageCode();
            btnbind.Focus();

        }

    }


    protected void gvunitMaster_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        gvunitMaster.PageIndex = e.NewPageIndex;

        DataTable dt = (DataTable)Session["dtFilter"];
        gvunitMaster.DataSource = dt;
        gvunitMaster.DataBind();
        AllPageCode();
       gvunitMaster.BottomPagerRow.Focus();

    }
    protected void gvUnitMaster_OnSorting(object sender, GridViewSortEventArgs e)
    {

        HDFSort.Value = HDFSort.Value == "ASC" ? "DESC" : "ASC";
        DataTable dt = new DataTable();
        dt = (DataTable)Session["dtFilter"];
        DataView dv = new DataView(dt);
        string Query = "" + e.SortExpression + " " + HDFSort.Value + "";
        dv.Sort = Query;
        dt = dv.ToTable();
        Session["dtFilter"] = dt;
        gvunitMaster.DataSource = dt;
        gvunitMaster.DataBind();
        AllPageCode();
        gvunitMaster.HeaderRow.Focus();

    }
    protected void btnSave_Click(object sender, EventArgs e)
    {
        if (txtEUnitName.Text == "")
        {
            DisplayMessage("Enter Unit Name");
            return;
        }
        if (txtConversion_Qty.Text == "")
        {
            txtConversion_Qty.Text = "1";
        }
        else
        {
            if (txtConversion_Qty.Text == "0")
            {
                txtConversion_Qty.Text = "1";
            }

        }


        string strConversionUnit = "";
        if (ddlConversion_Unit.SelectedValue == "--Select--")
        {
            strConversionUnit = "";
        }
        else
        {
            strConversionUnit = ddlConversion_Unit.SelectedValue.ToString();
        }

        int b = 0;



        if (editid.Value == "")
        {


            b = ObjInvUnitMaster.InsertUnitMaster(StrCompId.ToString(), txtEUnitName.Text, txtlUnitName.Text, "", strConversionUnit, txtConversion_Qty.Text, "", "", "", "", "", true.ToString(), DateTime.Now.ToString(), true.ToString(), StrUserId.ToString(), DateTime.Now.ToString(), StrUserId.ToString(), DateTime.Now.ToString());
            if (b != 0)
            {
                DisplayMessage("Record Saved");
                Reset();
                FillGrid();
                txtEUnitName.Focus();
            }
            else
            {
                DisplayMessage("Record Not Saved");
            }
        }
        else
        {
            b = ObjInvUnitMaster.UpdateUnitMaster(StrCompId.ToString(), editid.Value.ToString(), txtEUnitName.Text, txtlUnitName.Text, "", strConversionUnit, txtConversion_Qty.Text, "", "", "", "", "", true.ToString(), DateTime.Now.ToString(), true.ToString(), StrUserId, DateTime.Now.ToString());
            if (b != 0)
            {
                btnList_Click(null, null);
                DisplayMessage("Record Update");
                Reset();
                FillGrid();
            }
            else
            {
                DisplayMessage("Record Not Update");
            }
        }
    }

    protected void txtEUnitName_TextChanged(object sender, EventArgs e)
    {
        if (txtEUnitName.Text != "")
        {
            DataTable dtUnit = ObjInvUnitMaster.GetUnitMasterAll(StrCompId.ToString());
            dtUnit = new DataView(dtUnit, "unit_Name='" + txtEUnitName.Text.Trim() + "'", "", DataViewRowState.CurrentRows).ToTable();
            if (dtUnit.Rows.Count > 0)
            {
                if (dtUnit.Rows[0]["Unit_Id"].ToString() != editid.Value)
                {
                    if (Convert.ToBoolean(dtUnit.Rows[0]["IsActive"].ToString()))
                    {
                        DisplayMessage("Unit Name Already Exists");


                    }
                    else
                    {

                        DisplayMessage("Unit Name Already Exists :- Go to Bin Tab");

                    }
                    txtEUnitName.Text = "";
                    txtEUnitName.Focus();
                }

            }
            else
            {
                txtlUnitName.Focus();
            }
        }

    }


    #region Bin Section
    protected void btnbinbind_Click(object sender, ImageClickEventArgs e)
    {


        if (ddlbinOption.SelectedIndex != 0)
        {
            string condition = string.Empty;


            if (ddlbinOption.SelectedIndex == 1)
            {
                condition = "convert(" + ddlbinFieldName.SelectedValue + ",System.String)='" + txtbinValue.Text.Trim() + "'";
            }
            else if (ddlOption.SelectedIndex == 2)
            {
                condition = "convert(" + ddlbinFieldName.SelectedValue + ",System.String) like '%" + txtbinValue.Text + "%'";
            }
            else
            {
                condition = "convert(" + ddlbinFieldName.SelectedValue + ",System.String) Like '" + txtbinValue.Text + "%'";
            }
            DataTable dtCust = (DataTable)Session["dtbinUnit"];


            DataView view = new DataView(dtCust, condition, "", DataViewRowState.CurrentRows);
            Session["dtbinFilter"] = view.ToTable();
            lblbinTotalRecords.Text = Resources.Attendance.Total_Records + ": " + view.ToTable().Rows.Count.ToString() + "";

            gvUnitMasterBin.DataSource = view.ToTable();
            gvUnitMasterBin.DataBind();


            if (view.ToTable().Rows.Count == 0)
            {
                imgBtnRestore.Visible = false;
                ImgbtnSelectAll.Visible = false;
            }
            else
            {
                AllPageCode();
            }
            btnbinbind.Focus();

        }

    }
    protected void btnbinRefresh_Click(object sender, ImageClickEventArgs e)
    {
        FillGrid();
        FillGridBin();
        FillConversionUnitDDL("");
        ddlbinOption.SelectedIndex = 3;
        ddlbinFieldName.SelectedIndex = 0;
        txtbinValue.Text = "";
        lblSelectedRecord.Text = "";
        txtbinValue.Focus();
    }
    protected void gvUnitMasterBin_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        gvUnitMasterBin.PageIndex = e.NewPageIndex;
        if (HDFSortbin.Value == "")
            FillGridBin();
        else
        {
            DataTable dt = (DataTable)Session["dtbinFilter"];
            gvUnitMasterBin.DataSource = dt;
            gvUnitMasterBin.DataBind();
        }
        AllPageCode();

        string temp = string.Empty;
        bool isselcted;

        for (int i = 0; i < gvUnitMasterBin.Rows.Count; i++)
        {
            Label lblconid = (Label)gvUnitMasterBin.Rows[i].FindControl("lblUnitId");
            string[] split = lblSelectedRecord.Text.Split(',');

            for (int j = 0; j < lblSelectedRecord.Text.Split(',').Length; j++)
            {
                if (lblSelectedRecord.Text.Split(',')[j] != "")
                {
                    if (lblconid.Text.Trim().ToString() == lblSelectedRecord.Text.Split(',')[j].Trim().ToString())
                    {
                        ((CheckBox)gvUnitMasterBin.Rows[i].FindControl("chkgvSelect")).Checked = true;
                    }
                }
            }
        }

        gvUnitMasterBin.BottomPagerRow.Focus();

    }
    protected void gvUnitMasterBin_OnSorting(object sender, GridViewSortEventArgs e)
    {
        lblSelectedRecord.Text = "";
        HDFSortbin.Value = HDFSortbin.Value == "ASC" ? "DESC" : "ASC";
        DataTable dt = new DataTable();
        dt = ObjInvUnitMaster.GetUnitMasterInactive(StrCompId.ToString());
        DataView dv = new DataView(dt);
        string Query = "" + e.SortExpression + " " + HDFSortbin.Value + "";
        dv.Sort = Query;
        dt = dv.ToTable();
        Session["dtbinFilter"] = dt;
        gvUnitMasterBin.DataSource = dt;
        gvUnitMasterBin.DataBind();
        AllPageCode();
        gvUnitMasterBin.HeaderRow.Focus();

    }
    protected void chkgvSelectAll_CheckedChanged(object sender, EventArgs e)
    {
        CheckBox chkSelAll = ((CheckBox)gvUnitMasterBin.HeaderRow.FindControl("chkgvSelectAll"));
        for (int i = 0; i < gvUnitMasterBin.Rows.Count; i++)
        {
            ((CheckBox)gvUnitMasterBin.Rows[i].FindControl("chkgvSelect")).Checked = chkSelAll.Checked;
            if (chkSelAll.Checked)
            {
                if (!lblSelectedRecord.Text.Split(',').Contains(((Label)(gvUnitMasterBin.Rows[i].FindControl("lblUnitId"))).Text.Trim().ToString()))
                {
                    lblSelectedRecord.Text += ((Label)(gvUnitMasterBin.Rows[i].FindControl("lblUnitId"))).Text.Trim().ToString() + ",";
                }
            }
            else
            {
                string temp = string.Empty;
                string[] split = lblSelectedRecord.Text.Split(',');
                foreach (string item in split)
                {
                    if (item != ((Label)(gvUnitMasterBin.Rows[i].FindControl("lblUnitId"))).Text.Trim().ToString())
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
        ((CheckBox)gvUnitMasterBin.HeaderRow.FindControl("chkgvSelectAll")).Focus();
    }
    protected void chkgvSelect_CheckedChanged(object sender, EventArgs e)
    {
        string empidlist = string.Empty;
        string temp = string.Empty;
        int index = ((GridViewRow)((CheckBox)sender).Parent.Parent).RowIndex;
        Label lb = (Label)gvUnitMasterBin.Rows[index].FindControl("lblUnitId");
        if (((CheckBox)gvUnitMasterBin.Rows[index].FindControl("chkgvSelect")).Checked)
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
        ((CheckBox)gvUnitMasterBin.Rows[index].FindControl("chkgvSelect")).Focus();
    }
    protected void ImgbtnSelectAll_Click(object sender, ImageClickEventArgs e)
    {
        DataTable dtUnit = (DataTable)Session["dtbinFilter"];

        if (ViewState["Select"] == null)
        {
            ViewState["Select"] = 1;
            foreach (DataRow dr in dtUnit.Rows)
            {
                if (!lblSelectedRecord.Text.Split(',').Contains(dr["Unit_Id"]))
                {
                    lblSelectedRecord.Text += dr["Unit_Id"] + ",";
                }
            }
            for (int i = 0; i < gvUnitMasterBin.Rows.Count; i++)
            {
                string[] split = lblSelectedRecord.Text.Split(',');
                Label lblconid = (Label)gvUnitMasterBin.Rows[i].FindControl("lblUnitId");
                for (int j = 0; j < lblSelectedRecord.Text.Split(',').Length; j++)
                {
                    if (lblSelectedRecord.Text.Split(',')[j] != "")
                    {
                        if (lblconid.Text.Trim().ToString() == lblSelectedRecord.Text.Split(',')[j].Trim().ToString())
                        {
                            ((CheckBox)gvUnitMasterBin.Rows[i].FindControl("chkgvSelect")).Checked = true;
                        }
                    }
                }
            }
        }
        else
        {
            lblSelectedRecord.Text = "";
            DataTable dtUnit1 = (DataTable)Session["dtBinFilter"];
            gvUnitMasterBin.DataSource = dtUnit1;
            gvUnitMasterBin.DataBind();
            AllPageCode();
            ViewState["Select"] = null;
        }

        ImgbtnSelectAll.Focus();

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
                    b = ObjInvUnitMaster.DeleteUnitMaster(StrCompId.ToString(), lblSelectedRecord.Text.Split(',')[j].Trim(), true.ToString(), StrUserId.ToString(), DateTime.Now.ToString());

                }
            }
        }

        if (b != 0)
        {
            FillConversionUnitDDL("");
            FillGrid();
            FillGridBin();
            lblSelectedRecord.Text = "";
            ViewState["Select"] = null;
            DisplayMessage("Record Activated");
        }
        else
        {
            int fleg = 0;
            foreach (GridViewRow Gvr in gvUnitMasterBin.Rows)
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
        txtbinValue.Focus();
    }
    #endregion

    #endregion

    #region Auto Complete Funcation
    [System.Web.Services.WebMethodAttribute(), System.Web.Script.Services.ScriptMethodAttribute()]
    public static string[] GetCompletionList(string prefixText, int count, string contextKey)
    {
        Inv_UnitMaster ObjUnitMaster = new Inv_UnitMaster();
        DataTable dt = new DataView(ObjUnitMaster.GetUnitMaster(HttpContext.Current.Session["CompId"].ToString()), "Unit_Name like '" + prefixText.ToString() + "%'", "", DataViewRowState.CurrentRows).ToTable();
        string[] txt = new string[dt.Rows.Count];

        for (int i = 0; i < dt.Rows.Count; i++)
        {
            txt[i] = dt.Rows[i]["Unit_Name"].ToString();
        }
        return txt;
    }
    #endregion

    #region User defind Funcation

    public void FillGrid()
    {
        DataTable dt = ObjInvUnitMaster.GetUnitMaster(StrCompId.ToString());
        gvunitMaster.DataSource = dt;
        gvunitMaster.DataBind();
        Session["dtFilter"] = dt;
        Session["Unit"] = dt;

        lblTotalRecords.Text = Resources.Attendance.Total_Records + ": " + dt.Rows.Count.ToString() + "";
        AllPageCode();
    }
    public void Reset()
    {
        FillConversionUnitDDL("");
        txtConversion_Qty.Text = "";
        txtEUnitName.Text = "";
        txtlUnitName.Text = "";
        btnNew.Text = Resources.Attendance.New;
        lblSelectedRecord.Text = "";
        ViewState["Select"] = null;
        editid.Value = "";
        txtValue.Text = "";
        ddlOption.SelectedIndex = 3;
        ddlFieldName.SelectedIndex = 0;
    }
    public void DisplayMessage(string str)
    {
        ScriptManager.RegisterStartupScript(this, GetType(), "", "alert('" + str + "');", true);
    }
    public void FillGridBin()
    {

        DataTable dt = new DataTable();
        dt = ObjInvUnitMaster.GetUnitMasterInactive(StrCompId.ToString());
        gvUnitMasterBin.DataSource = dt;
        gvUnitMasterBin.DataBind();

        Session["dtbinFilter"] = dt;
        Session["dtbinUnit"] = dt;
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
    public void FillConversionUnitDDL(string strExceptId)
    {
        DataTable dt = ObjInvUnitMaster.GetUnitMaster(StrCompId.ToString());

        if (strExceptId != "")
        {
            string query = "Unit_Id<>'" + strExceptId + "'";
            dt = new DataView(dt, query, "", DataViewRowState.OriginalRows).ToTable();
        }

        if (dt.Rows.Count > 0)
        {
            ddlConversion_Unit.DataSource = null;
            ddlConversion_Unit.DataBind();
            ddlConversion_Unit.DataSource = dt;
            ddlConversion_Unit.DataTextField = "Unit_Name";
            ddlConversion_Unit.DataValueField = "Unit_Id";
            ddlConversion_Unit.DataBind();

            ddlConversion_Unit.Items.Insert(0, "--Select--");
            ddlConversion_Unit.SelectedIndex = 0;

        }
        else
        {
            try
            {
                ddlConversion_Unit.Items.Clear();
                ddlConversion_Unit.DataSource = null;
                ddlConversion_Unit.DataBind();
                ddlConversion_Unit.Items.Insert(0, "--Select--");
                ddlConversion_Unit.SelectedIndex = 0;
            }
            catch
            {
                ddlConversion_Unit.Items.Insert(0, "--Select--");
                ddlConversion_Unit.SelectedIndex = 0;

            }
        }

    }
    public string GetUnitName(string UnitId)
    {
        DataTable dt = new DataView(ObjInvUnitMaster.GetUnitMasterAll(StrCompId.ToString()), "Unit_Id='" + UnitId.ToString() + "'", "", DataViewRowState.CurrentRows).ToTable();
        string strUnitName = "NA";
        if (dt.Rows.Count != 0)
        {
            strUnitName = dt.Rows[0]["Unit_Name"].ToString();
        }
        return strUnitName;
    }
    #endregion


}
