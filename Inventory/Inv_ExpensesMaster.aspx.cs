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

public partial class Inventory_Inv_ExpensesMaster : System.Web.UI.Page
{
    #region defind Class Object
    Common cmn = new Common();
    Inv_ShipExpMaster ObjShipExpMaster = new Inv_ShipExpMaster();
    SystemParameter ObjSysPeram = new SystemParameter();
    string StrCompId = string.Empty;
    string StrBrandId = "1";
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
            StrCompId = Session["CompId"].ToString();
            StrUserId = Session["UserId"].ToString();
            ddlOption.SelectedIndex = 2;
            btnList_Click(null, null);
            FillGridBin();
            FillGrid();
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
        DataTable dtAllPageCode = cmn.GetAllPagePermission(StrUserId.ToString(), "11", "55");
        if (dtAllPageCode.Rows.Count != 0)
        {
            if (dtAllPageCode.Rows[0][0].ToString() == "SuperAdmin")
            {
                btnCSave.Visible = true;
                foreach (GridViewRow Row in gvexpenses.Rows)
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
                        btnCSave.Visible = true;
                    }
                    foreach (GridViewRow Row in gvexpenses.Rows)
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
        txtExpName.Focus();
    }
    protected void btnEdit_Command(object sender, CommandEventArgs e)
    {
        editid.Value = e.CommandArgument.ToString();

        DataTable dt = ObjShipExpMaster.GetShipExpMasterById(StrCompId, editid.Value);

        btnNew.Text = Resources.Attendance.Edit;

        txtExpName.Text = dt.Rows[0]["Exp_Name"].ToString();
        txtLExpName.Text = dt.Rows[0]["Exp_Name_L"].ToString();
        txtAccountNo.Text = dt.Rows[0]["Account_No"].ToString();

        btnNew_Click(null, null);

    }
    protected void gvexpenses_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        gvexpenses.PageIndex = e.NewPageIndex;
        DataTable dt = (DataTable)Session["dtFilter"];
        gvexpenses.DataSource = dt;
        gvexpenses.DataBind();
        AllPageCode();
        gvexpenses.BottomPagerRow.Focus();
    }
    protected void btnbindrpt_Click(object sender, ImageClickEventArgs e)
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
            DataTable dtShipExp = (DataTable)Session["dtShipExp"];
            DataView view = new DataView(dtShipExp, condition, "", DataViewRowState.CurrentRows);
            gvexpenses.DataSource = view.ToTable();
            Session["dtFilter"] = view.ToTable();
            lblTotalRecords.Text = Resources.Attendance.Total_Records + " : " + view.ToTable().Rows.Count + "";
            gvexpenses.DataBind();
            AllPageCode();
            btnbind.Focus();

        }
    }
    protected void gvexpenses_Sorting(object sender, GridViewSortEventArgs e)
    {
        DataTable dt = (DataTable)Session["dtFilter"];
        string sortdir = "DESC";
        if (ViewState["SortDir"] != null)
        {
            sortdir = ViewState["SortDir"].ToString();
            if (sortdir == "ASC")
            {
                e.SortDirection = SortDirection.Descending;
                ViewState["SortDir"] = "DESC";

            }
            else
            {
                e.SortDirection = SortDirection.Ascending;
                ViewState["SortDir"] = "ASC";
            }
        }
        else
        {
            ViewState["SortDir"] = "DESC";
        }

        dt = (new DataView(dt, "", e.SortExpression + " " + ViewState["SortDir"].ToString(), DataViewRowState.CurrentRows)).ToTable();
        Session["dtFilter"] = dt;
        gvexpenses.DataSource = dt;
        gvexpenses.DataBind();
        AllPageCode();
        gvexpenses.HeaderRow.Focus();
    }
    protected void IbtnDelete_Command(object sender, CommandEventArgs e)
    {
        editid.Value = e.CommandArgument.ToString();
        int b = 0;
        b = ObjShipExpMaster.DeleteShipExpMaster(StrCompId, editid.Value, "false", StrUserId, DateTime.Now.ToString());
        if (b != 0)
        {
            DisplayMessage("Record Deleted");
        }
        else
        {
            DisplayMessage("Record  Not Deleted");
        }
        FillGridBin();
        FillGrid();
        Reset();
        try
        {
            int i = ((GridViewRow)((ImageButton)sender).Parent.Parent).RowIndex;
            ((ImageButton)gvexpenses.Rows[i].FindControl("IbtnDelete")).Focus();
        }
        catch
        {
            txtValue.Focus();
        }


    }
    protected void btnRefreshReport_Click(object sender, ImageClickEventArgs e)
    {
        FillGrid();
        FillGridBin();
        ddlFieldName.SelectedIndex = 1;
        ddlOption.SelectedIndex = 2;
        txtValue.Text = "";
        txtValue.Focus();
    }
    protected void BtnCCancel_Click(object sender, EventArgs e)
    {
        Reset();
        btnList_Click(null, null);
        FillGridBin();
        FillGrid();
        txtValue.Focus();
    }
    protected void BtnReset_Click(object sender, EventArgs e)
    {
        Reset();
        txtExpName.Focus();
    }
    protected void btnCSave_Click(object sender, EventArgs e)
    {

        if (txtExpName.Text == "")
        {
            DisplayMessage("Enter Expenses Name");
            txtExpName.Focus();
            return;
        }
        int b = 0;
        if (editid.Value != "")
        {
            //Code to check whether the new name after edit does not exists
            DataTable dtShipExp = ObjShipExpMaster.GetShipExpMasterByExpName(StrCompId, txtExpName.Text);
            if (dtShipExp.Rows.Count > 0)
            {
                if (dtShipExp.Rows[0]["Expense_Id"].ToString() != editid.Value)
                {
                    txtExpName.Text = "";
                    DisplayMessage("Expenses Name Already Exists");
                    txtExpName.Focus();
                    return;
                }
            }

            b = ObjShipExpMaster.UpdateShipExpMaster(StrCompId, editid.Value, txtExpName.Text, txtLExpName.Text, txtAccountNo.Text, "", "", "", "", "", "True", DateTime.Now.ToString(), "True", Session["UserId"].ToString(), DateTime.Now.ToString());

            editid.Value = "";
            if (b != 0)
            {
                DisplayMessage("Record Updated");
                Reset();
                FillGrid();
                btnList_Click(null, null);
            }
            else
            {
                DisplayMessage("Record  Not Updated");
            }
        }
        else
        {
            DataTable dtShip = ObjShipExpMaster.GetShipExpMasterByExpName(StrCompId, txtExpName.Text);
            if (dtShip.Rows.Count > 0)
            {
                txtExpName.Text = "";
                DisplayMessage("Expenses Name Already Exists");
                txtExpName.Focus();
                return;
            }

            b = ObjShipExpMaster.InsertShipExpMaster(StrCompId, txtExpName.Text, txtLExpName.Text, txtAccountNo.Text, "", "", "", "", "", "True", DateTime.Now.ToString(), "True", Session["UserId"].ToString(), DateTime.Now.ToString(), Session["UserId"].ToString(), DateTime.Now.ToString());
            if (b != 0)
            {
                DisplayMessage("Record Saved");

                Reset();
                FillGrid();
                txtExpName.Focus();
            }
            else
            {
                DisplayMessage("Record  Not Saved");
            }
        }
    }

    protected void txtExpName_TextChanged(object sender, EventArgs e)
    {
        if (editid.Value == "")
        {
            DataTable dtShip = ObjShipExpMaster.GetShipExpMasterByExpName(StrCompId, txtExpName.Text);
            if (dtShip.Rows.Count > 0)
            {
                txtExpName.Text = "";
                DisplayMessage("Expenses Name Already Exists");
                txtExpName.Focus();
                return;
            }
            DataTable dt1 = ObjShipExpMaster.GetShipExpMasterInactive(StrCompId);
            dt1 = new DataView(dt1, "Exp_Name='" + txtExpName.Text + "'", "", DataViewRowState.CurrentRows).ToTable();
            if (dt1.Rows.Count > 0)
            {
                txtExpName.Text = "";
                DisplayMessage("Expenses Name Already Exists - Go to Bin Tab");
                txtExpName.Focus();
                return;
            }
        }
        else
        {
            DataTable dtTemp = ObjShipExpMaster.GetShipExpMasterById(StrCompId, editid.Value);
            if (dtTemp.Rows.Count > 0)
            {
                if (dtTemp.Rows[0]["Exp_Name"].ToString() != txtExpName.Text)
                {
                    DataTable dt = ObjShipExpMaster.GetShipExpMasterByExpName(StrCompId, txtExpName.Text);
                    if (dt.Rows.Count > 0)
                    {
                        txtExpName.Text = "";
                        DisplayMessage("Expenses Name Already Exists");
                        txtExpName.Focus();
                        return;
                    }
                    DataTable dt1 = ObjShipExpMaster.GetShipExpMasterInactive(StrCompId);
                    dt1 = new DataView(dt1, "Exp_Name='" + txtExpName.Text + "'", "", DataViewRowState.CurrentRows).ToTable();
                    if (dt1.Rows.Count > 0)
                    {
                        txtExpName.Text = "";
                        DisplayMessage("Expenses Name Already Exists - Go to Bin Tab");
                        txtExpName.Focus();
                        return;
                    }
                }
            }
        }
        txtLExpName.Focus();
    }

    #region Bin Section
    protected void btnBin_Click(object sender, EventArgs e)
    {
        pnlMenuBin.BackColor = System.Drawing.ColorTranslator.FromHtml("#ccddee");

        pnlMenuNew.BackColor = System.Drawing.ColorTranslator.FromHtml("#90bde9");
        pnlMenuList.BackColor = System.Drawing.ColorTranslator.FromHtml("#90bde9");
        PnlNewEdit.Visible = false;
        PnlBin.Visible = true;
        PnlList.Visible = false;
        FillGridBin();
        txtValueBin.Focus();
    }
    protected void gvexpensesBin_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        gvexpensesBin.PageIndex = e.NewPageIndex;

        DataTable dt = (DataTable)Session["dtInactive"];
        gvexpensesBin.DataSource = dt;
        gvexpensesBin.DataBind();
        AllPageCode();
        string temp = string.Empty;

        for (int i = 0; i < gvexpensesBin.Rows.Count; i++)
        {
            Label lblconid = (Label)gvexpensesBin.Rows[i].FindControl("lblExpId");
            string[] split = lblSelectedRecord.Text.Split(',');

            for (int j = 0; j < lblSelectedRecord.Text.Split(',').Length; j++)
            {
                if (lblSelectedRecord.Text.Split(',')[j] != "")
                {
                    if (lblconid.Text.Trim().ToString() == lblSelectedRecord.Text.Split(',')[j].Trim().ToString())
                    {
                        ((CheckBox)gvexpensesBin.Rows[i].FindControl("chkSelect")).Checked = true;
                    }
                }
            }
        }
        gvexpensesBin.BottomPagerRow.Focus();
    }
    protected void gvexpensesBin_OnSorting(object sender, GridViewSortEventArgs e)
    {
        HDFSortbin.Value = HDFSortbin.Value == "ASC" ? "DESC" : "ASC";
        DataTable dt = new DataTable();
        dt = ObjShipExpMaster.GetShipExpMasterInactive(StrCompId);
        DataView dv = new DataView(dt);
        string Query = "" + e.SortExpression + " " + HDFSortbin.Value + "";
        dv.Sort = Query;
        dt = dv.ToTable();
        Session["dtInactive"] = dt;
        gvexpensesBin.DataSource = dt;
        gvexpensesBin.DataBind();
        AllPageCode();
        lblSelectedRecord.Text = "";
        gvexpensesBin.HeaderRow.Focus();
    }
    public void FillGridBin()
    {
        DataTable dt = new DataTable();
        dt = ObjShipExpMaster.GetShipExpMasterInactive(StrCompId);
        gvexpensesBin.DataSource = dt;
        gvexpensesBin.DataBind();
        Session["dtShipExpBin"] = dt;
        Session["dtInactive"] = dt;
        lblTotalRecordsBin.Text = Resources.Attendance.Total_Records + " : " + dt.Rows.Count.ToString() + "";
        lblSelectedRecord.Text = "";
        if (dt.Rows.Count == 0)
        {
            ImgbtnSelectAll.Visible = false;
            imgBtnRestore.Visible = false;
        }
        else
        {
            AllPageCode();
        }
    }
    protected void btnbindBin_Click(object sender, EventArgs e)
    {
        string condition = string.Empty;
        if (ddlOptionBin.SelectedIndex != 0)
        {
            if (ddlOptionBin.SelectedIndex == 1)
            {
                condition = "convert(" + ddlFieldNameBin.SelectedValue + ",System.String)='" + txtValueBin.Text.Trim() + "'";
            }
            else if (ddlOptionBin.SelectedIndex == 2)
            {
                condition = "convert(" + ddlFieldNameBin.SelectedValue + ",System.String) like '%" + txtValueBin.Text.Trim() + "%'";
            }
            else
            {
                condition = "convert(" + ddlFieldNameBin.SelectedValue + ",System.String) Like '" + txtValueBin.Text.Trim() + "%'";
            }

            DataTable dtCust = (DataTable)Session["dtShipExpBin"];
            DataView view = new DataView(dtCust, condition, "", DataViewRowState.CurrentRows);
            Session["dtInactive"] = view.ToTable();
            lblTotalRecordsBin.Text = Resources.Attendance.Total_Records + " : " + view.ToTable().Rows.Count.ToString() + "";
            gvexpensesBin.DataSource = view.ToTable();
            gvexpensesBin.DataBind();
            lblSelectedRecord.Text = "";
            if (view.ToTable().Rows.Count == 0)
            {
                imgBtnRestore.Visible = false;
                ImgbtnSelectAll.Visible = false;
            }
            else
            {
                AllPageCode();
            }
            btnbindBin.Focus();
        }
    }

    protected void chkCurrent_CheckedChanged(object sender, EventArgs e)
    {
        CheckBox chkSelAll = ((CheckBox)gvexpensesBin.HeaderRow.FindControl("chkCurrent"));
        for (int i = 0; i < gvexpensesBin.Rows.Count; i++)
        {
            ((CheckBox)gvexpensesBin.Rows[i].FindControl("chkSelect")).Checked = chkSelAll.Checked;
            if (chkSelAll.Checked)
            {
                if (!lblSelectedRecord.Text.Split(',').Contains(((Label)(gvexpensesBin.Rows[i].FindControl("lblExpId"))).Text.Trim().ToString()))
                {
                    lblSelectedRecord.Text += ((Label)(gvexpensesBin.Rows[i].FindControl("lblExpId"))).Text.Trim().ToString() + ",";
                }
            }
            else
            {
                string temp = string.Empty;
                string[] split = lblSelectedRecord.Text.Split(',');
                foreach (string item in split)
                {
                    if (item != ((Label)(gvexpensesBin.Rows[i].FindControl("lblExpId"))).Text.Trim().ToString())
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
        chkSelAll.Focus();
    }
    protected void chkSelect_CheckedChanged(object sender, EventArgs e)
    {
        string empidlist = string.Empty;
        string temp = string.Empty;
        int index = ((GridViewRow)((CheckBox)sender).Parent.Parent).RowIndex;
        Label lb = (Label)gvexpensesBin.Rows[index].FindControl("lblExpId");
        if (((CheckBox)gvexpensesBin.Rows[index].FindControl("chkSelect")).Checked)
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
        ((CheckBox)gvexpensesBin.Rows[index].FindControl("chkSelect")).Focus();
    }
    protected void btnRefreshBin_Click(object sender, EventArgs e)
    {
        FillGrid();
        FillGridBin();
        txtValueBin.Text = "";
        ddlOptionBin.SelectedIndex = 2;
        ddlFieldNameBin.SelectedIndex = 1;
        lblSelectedRecord.Text = "";
        System.Web.UI.ScriptManager.GetCurrent(this).SetFocus(txtValueBin);
    }
    protected void ImgbtnSelectAll_Click(object sender, ImageClickEventArgs e)
    {
        DataTable dtPbrand = (DataTable)Session["dtInactive"];

        if (ViewState["Select"] == null)
        {
            ViewState["Select"] = 1;
            foreach (DataRow dr in dtPbrand.Rows)
            {
                if (!lblSelectedRecord.Text.Split(',').Contains(dr["Expense_Id"]))
                {
                    lblSelectedRecord.Text += dr["Expense_Id"] + ",";
                }
            }
            for (int i = 0; i < gvexpensesBin.Rows.Count; i++)
            {
                string[] split = lblSelectedRecord.Text.Split(',');
                Label lblconid = (Label)gvexpensesBin.Rows[i].FindControl("lblExpId");
                for (int j = 0; j < lblSelectedRecord.Text.Split(',').Length; j++)
                {
                    if (lblSelectedRecord.Text.Split(',')[j] != "")
                    {
                        if (lblconid.Text.Trim().ToString() == lblSelectedRecord.Text.Split(',')[j].Trim().ToString())
                        {
                            ((CheckBox)gvexpensesBin.Rows[i].FindControl("chkSelect")).Checked = true;
                        }
                    }
                }
            }
        }
        else
        {
            lblSelectedRecord.Text = "";
            DataTable dtUnit1 = (DataTable)Session["dtInactive"];
            gvexpensesBin.DataSource = dtUnit1;
            gvexpensesBin.DataBind();
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
                    b = ObjShipExpMaster.DeleteShipExpMaster(StrCompId, lblSelectedRecord.Text.Split(',')[j].Trim(), true.ToString(), Session["UserId"].ToString(), DateTime.Now.ToString());
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
            btnRefreshBin_Click(null, null);
        }
        else
        {
            int fleg = 0;
            foreach (GridViewRow Gvr in gvexpensesBin.Rows)
            {
                CheckBox chk = (CheckBox)Gvr.FindControl("chkSelect");
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

        txtValueBin.Focus();

    }
    #endregion

    #endregion

    #region User defind Funcation
    private void FillGrid()
    {
        DataTable dtShipExp = ObjShipExpMaster.GetShipExpMaster(StrCompId);
        lblTotalRecords.Text = Resources.Attendance.Total_Records + " : " + dtShipExp.Rows.Count + "";
        Session["dtShipExp"] = dtShipExp;
        Session["dtFilter"] = dtShipExp;
        if (dtShipExp != null && dtShipExp.Rows.Count > 0)
        {
            gvexpenses.DataSource = dtShipExp;
            gvexpenses.DataBind();
        }
        else
        {
            gvexpenses.DataSource = null;
            gvexpenses.DataBind();
        }
        AllPageCode();
    }
    public void DisplayMessage(string str)
    {
        if (Session["lang"] == null)
        {
            Session["lang"] = "1";
        }
        if (Session["lang"].ToString() == "1")
        {
            ScriptManager.RegisterStartupScript(this, GetType(), "", "alert('" + str + "');", true);
        }
        else if (Session["lang"].ToString() == "2")
        {
            ScriptManager.RegisterStartupScript(this, GetType(), "", "alert('" + GetArebicMessage(str) + "');", true);
        }
    }
    public string GetArebicMessage(string EnglishMessage)
    {
        string ArebicMessage = string.Empty;
        DataTable dtres = (DataTable)Session["MessageDt"];
        if (dtres.Rows.Count != 0)
        {
            ArebicMessage = (new DataView(dtres, "Key='" + EnglishMessage + "'", "", DataViewRowState.CurrentRows).ToTable()).Rows[0]["Value"].ToString();
        }
        if (ArebicMessage == "")
        {
            ArebicMessage = EnglishMessage;
        }
        return ArebicMessage;
    }
    public void Reset()
    {

        txtExpName.Text = "";
        txtLExpName.Text = "";
        txtAccountNo.Text = "";
        editid.Value = "";
        btnNew.Text = Resources.Attendance.New;
        ddlFieldName.SelectedIndex = 1;
        ddlOption.SelectedIndex = 2;
        txtValue.Text = "";
        txtValueBin.Text = "";
        lblSelectedRecord.Text = "";
        System.Web.UI.ScriptManager.GetCurrent(this).SetFocus(txtValue);
    }
    #endregion

    #region Auto Complete Funcation
    [System.Web.Services.WebMethodAttribute(), System.Web.Script.Services.ScriptMethodAttribute()]
    public static string[] GetCompletionList(string prefixText, int count, string contextKey)
    {
        Inv_ShipExpMaster Obj = new Inv_ShipExpMaster();
        DataTable dt =new DataView(Obj.GetShipExpMaster(HttpContext.Current.Session["CompId"].ToString()),"Exp_Name like '"+ prefixText.ToString() +"%'","",DataViewRowState.CurrentRows).ToTable(true,"Exp_Name");

        string[] str = new string[dt.Rows.Count];
        for (int i = 0; i < dt.Rows.Count; i++)
        {
            str[i] = dt.Rows[i]["Exp_Name"].ToString();
        }
        return str;
    }
    #endregion
}
