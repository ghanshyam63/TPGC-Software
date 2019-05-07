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
public partial class MasterSetUp_DistrictMaster : BasePage
{
    #region Defined Class Object
    Common cmn = new Common();
    SystemParameter objSys = new SystemParameter();
    DistrictMaster objDesg = new DistrictMaster();

    #endregion
    protected void Page_Load(object sender, EventArgs e)
    {

        if (Session["UserId"] == null)
        {
            Response.Redirect("~/ERPLogin.aspx");
        }
        AllPageCode();
        if (!IsPostBack)
        {
            ddlOption.SelectedIndex = 2;
            btnNew_Click(null, null);
            FillGridBin();
            FillGrid();
            ddlFieldName.SelectedIndex = 0;
            txtDistrictName.Focus();
        }


    }
    public void AllPageCode()
    {

        Page.Title = objSys.GetSysTitle();
        Session["AccordianId"] = "7";
        Session["HeaderText"] = "System Setup";
        DataTable dtAllPageCode = cmn.GetAllPagePermission(Session["UserId"].ToString(), "7", "8");
        if (dtAllPageCode.Rows.Count != 0)
        {
            if (dtAllPageCode.Rows[0][0].ToString() == "SuperAdmin")
            {
                btnCSave.Visible = true;
                foreach (GridViewRow Row in GvDistrict.Rows)
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
                    foreach (GridViewRow Row in GvDistrict.Rows)
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
    #region User Defined Funcation
    public void FillGridBin()
    {

        DataTable dt = new DataTable();
        dt = objDesg.GetDistrictMasterInactive(Session["CompId"].ToString().ToString());
        GvDistrictBin.DataSource = dt;
        GvDistrictBin.DataBind();
        Session["dtBinDistrict"] = dt;
        Session["dtBinFilter"] = dt;
        lblTotalRecordsBin.Text = Resources.Attendance.Total_Records + ": " + dt.Rows.Count.ToString() + "";

        lblSelectedRecord.Text = "";
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
    private void FillGrid()
    {
        DataTable dtBrand = objDesg.GetDistrictMaster(Session["CompId"].ToString().ToString());
        lblTotalRecords.Text = Resources.Attendance.Total_Records + ": " + dtBrand.Rows.Count + "";
        Session["dtDistrict"] = dtBrand;
        Session["dtFilter"] = dtBrand;
        if (dtBrand != null && dtBrand.Rows.Count > 0)
        {
            GvDistrict.DataSource = dtBrand;
            GvDistrict.DataBind();
            AllPageCode();
        }
        else
        {
            GvDistrict.DataSource = null;
            GvDistrict.DataBind();
        }

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

        txtDistrictName.Text = "";
        txtDistrictNameL.Text = "";
        editid.Value = "";
        btnNew.Text = Resources.Attendance.New;
        ddlFieldName.SelectedIndex = 0;
        ddlOption.SelectedIndex = 2;
        txtValue.Text = "";
        txtValueBin.Text = "";
        lblSelectedRecord.Text = "";
        System.Web.UI.ScriptManager.GetCurrent(this).SetFocus(txtValue);
        txtDistrictNameL.Text = "";
    }
    #endregion


    #region System Defined Funcation

    protected void btnNew_Click(object sender, EventArgs e)
    {
        pnlMenuNew.BackColor = System.Drawing.ColorTranslator.FromHtml("#ccddee");


        pnlMenuBin.BackColor = System.Drawing.ColorTranslator.FromHtml("#90bde9");

        pnlList.Visible = true;

        pnlBin.Visible = false;

    }
    protected void btnBin_Click(object sender, EventArgs e)
    {
        pnlMenuBin.BackColor = System.Drawing.ColorTranslator.FromHtml("#ccddee");

        pnlMenuNew.BackColor = System.Drawing.ColorTranslator.FromHtml("#90bde9");

        pnlList.Visible = false;
        pnlBin.Visible = true;

        FillGridBin();
    }
    protected void BtnReset_Click(object sender, EventArgs e)
    {
        Reset();
        System.Web.UI.ScriptManager.GetCurrent(this).SetFocus(txtDistrictName);
    }
    protected void btnCSave_Click(object sender, EventArgs e)
    {

        if (txtDistrictName.Text == "" || txtDistrictName.Text == null)
        {
            DisplayMessage("Enter District Name");
            System.Web.UI.ScriptManager.GetCurrent(this).SetFocus(txtDistrictName);
            return;
        }
        int b = 0;
        if (editid.Value != "")
        {

            DataTable dtCate = objDesg.GetDistrictMaster(Session["CompId"].ToString().ToString());
            dtCate = new DataView(dtCate, "District='" + txtDistrictName.Text + "'", "", DataViewRowState.CurrentRows).ToTable();
            if (dtCate.Rows.Count > 0)
            {
                if (dtCate.Rows[0]["District_ID"].ToString() != editid.Value)
                {
                    txtDistrictName.Text = "";
                    DisplayMessage("District Already Exists");
                    System.Web.UI.ScriptManager.GetCurrent(this).SetFocus(txtDistrictName);
                    return;
                }
            }


            b = objDesg.UpdateDistrictMaster(Session["CompId"].ToString().ToString(), editid.Value, txtDistrictName.Text, txtDistrictNameL.Text.Trim().ToString(), "", "", "", "", "", true.ToString(), DateTime.Now.ToString(), "True", Session["UserId"].ToString(), DateTime.Now.ToString());
            editid.Value = "";
            if (b != 0)
            {
                Reset();
                FillGrid();
                DisplayMessage("Record Updated");

            }
            else
            {
                DisplayMessage("Record  Not Updated");
            }
        }
        else
        {
            DataTable dtPro = objDesg.GetDistrictMaster(Session["CompId"].ToString().ToString());
            dtPro = new DataView(dtPro, "District='" + txtDistrictName.Text + "'", "", DataViewRowState.CurrentRows).ToTable();

            if (dtPro.Rows.Count > 0)
            {
                txtDistrictName.Text = "";
                DisplayMessage("District Name Already Exists");
                System.Web.UI.ScriptManager.GetCurrent(this).SetFocus(txtDistrictName);
                return;
            }

            b = objDesg.InsertDistrictMaster(Session["CompId"].ToString().ToString(), txtDistrictName.Text, txtDistrictNameL.Text.Trim(), "", "", "", "", "", true.ToString(), DateTime.Now.ToString(), "True", Session["UserId"].ToString(), DateTime.Now.ToString(), Session["UserId"].ToString(), DateTime.Now.ToString());
            if (b != 0)
            {
                DisplayMessage("Record Saved");
                Reset();
                FillGrid();
            }
            else
            {
                DisplayMessage("Record  Not Saved");
            }
        }
    }
    protected void IbtnRestore_Command(object sender, CommandEventArgs e)
    {
        editid.Value = e.CommandArgument.ToString();
        int b = 0;
        String CompanyId = Session["CompId"].ToString();
        String UserId = Session["UserId"].ToString();
        b = objDesg.DeleteDistrictMaster(Session["CompId"].ToString().ToString(), editid.Value, true.ToString(), Session["UserId"].ToString(), DateTime.Now.ToString());
        if (b != 0)
        {
            DisplayMessage("Record Restored");
        }
        else
        {
            DisplayMessage("Record  Restore Fail");
        }
        FillGrid();
        FillGridBin();
        Reset();



    }
    protected void GvDistrictBin_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        GvDistrictBin.PageIndex = e.NewPageIndex;

        DataTable dt = (DataTable)Session["dtBinFilter"];
        GvDistrictBin.DataSource = dt;
        GvDistrictBin.DataBind();
        AllPageCode();
        string temp = string.Empty;


        for (int i = 0; i < GvDistrictBin.Rows.Count; i++)
        {
            Label lblconid = (Label)GvDistrictBin.Rows[i].FindControl("lblDistrictId");
            string[] split = lblSelectedRecord.Text.Split(',');

            for (int j = 0; j < lblSelectedRecord.Text.Split(',').Length; j++)
            {
                if (lblSelectedRecord.Text.Split(',')[j] != "")
                {
                    if (lblconid.Text.Trim().ToString() == lblSelectedRecord.Text.Split(',')[j].Trim().ToString())
                    {
                        ((CheckBox)GvDistrictBin.Rows[i].FindControl("chkgvSelect")).Checked = true;
                    }
                }
            }
        }

    }
    protected void GvDistrictBin_OnSorting(object sender, GridViewSortEventArgs e)
    {
        HDFSortbin.Value = HDFSortbin.Value == "ASC" ? "DESC" : "ASC";
        DataTable dt = new DataTable();
        dt = objDesg.GetDistrictMasterInactive(Session["CompId"].ToString().ToString());
        DataView dv = new DataView(dt);
        string Query = "" + e.SortExpression + " " + HDFSortbin.Value + "";
        dv.Sort = Query;
        dt = dv.ToTable();
        Session["dtBinFilter"] = dt;
        GvDistrictBin.DataSource = dt;
        GvDistrictBin.DataBind();
        AllPageCode();
        lblSelectedRecord.Text = "";

    }
    protected void btnEdit_Command(object sender, CommandEventArgs e)
    {
        editid.Value = e.CommandArgument.ToString();

        DataTable dtTax = objDesg.GetDistrictMasterById(Session["CompId"].ToString().ToString(), editid.Value);

        btnNew.Text = Resources.Attendance.Edit;

        txtDistrictName.Text = dtTax.Rows[0]["District"].ToString();
        txtDistrictNameL.Text = dtTax.Rows[0]["District_L"].ToString();
        System.Web.UI.ScriptManager.GetCurrent(this).SetFocus(txtDistrictName);
    }
    protected void GvDistrict_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        GvDistrict.PageIndex = e.NewPageIndex;
        DataTable dt = (DataTable)Session["dtFilter"];
        GvDistrict.DataSource = dt;
        GvDistrict.DataBind();
        AllPageCode();

    }


    protected void btnbindrpt_Click(object sender, ImageClickEventArgs e)
    {

        if (ddlOption.SelectedIndex != 0)
        {
            string condition = string.Empty;
            if (ddlOption.SelectedIndex == 1)
            {
                condition = "convert(" + ddlFieldName.SelectedValue + ",System.String)='" + txtValue.Text + "'";
            }
            else if (ddlOption.SelectedIndex == 2)
            {
                condition = "convert(" + ddlFieldName.SelectedValue + ",System.String) like '%" + txtValue.Text + "%'";
            }
            else
            {
                condition = "convert(" + ddlFieldName.SelectedValue + ",System.String) Like '" + txtValue.Text + "%'";
            }
            DataTable dtCurrency = (DataTable)Session["dtDistrict"];
            DataView view = new DataView(dtCurrency, condition, "", DataViewRowState.CurrentRows);
            GvDistrict.DataSource = view.ToTable();
            Session["dtFilter"] = view.ToTable();
            lblTotalRecords.Text = Resources.Attendance.Total_Records + " : " + view.ToTable().Rows.Count + "";
            GvDistrict.DataBind();
            AllPageCode();
        }
    }
    protected void GvDistrict_Sorting(object sender, GridViewSortEventArgs e)
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
        GvDistrict.DataSource = dt;
        GvDistrict.DataBind();
        AllPageCode();

    }
    protected void IbtnDelete_Command(object sender, CommandEventArgs e)
    {
        editid.Value = e.CommandArgument.ToString();
        int b = 0;
        String CompanyId = Session["CompId"].ToString().ToString();
        String UserId = Session["UserId"].ToString().ToString();
        b = objDesg.DeleteDistrictMaster(Session["CompId"].ToString().ToString(), editid.Value, "false", Session["UserId"].ToString().ToString(), DateTime.Now.ToString());
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
    }
    protected void btnRefreshReport_Click(object sender, ImageClickEventArgs e)
    {
        FillGrid();
        FillGridBin();
        ddlFieldName.SelectedIndex = 0;
        ddlOption.SelectedIndex = 2;
        txtValue.Text = "";
    }

    [System.Web.Services.WebMethodAttribute(), System.Web.Script.Services.ScriptMethodAttribute()]
    public static string[] GetCompletionListDistrict(string prefixText, int count, string contextKey)
    {
        DistrictMaster objDistrict = new DistrictMaster();
        DataTable dt = new DataView(objDistrict.GetDistrictMaster(HttpContext.Current.Session["CompId"].ToString()), "District like '" + prefixText.ToString() + "%'", "", DataViewRowState.CurrentRows).ToTable();
        string[] txt = new string[dt.Rows.Count];

        for (int i = 0; i < dt.Rows.Count; i++)
        {
            txt[i] = dt.Rows[i]["District"].ToString();
        }
        return txt;
    }
    protected void btnbindBin_Click(object sender, EventArgs e)
    {
        string condition = string.Empty;
        if (ddlOptionBin.SelectedIndex != 0)
        {


            if (ddlOptionBin.SelectedIndex == 1)
            {
                condition = "convert(" + ddlFieldNameBin.SelectedValue + ",System.String)='" + txtValueBin.Text + "'";
            }
            else if (ddlOptionBin.SelectedIndex == 2)
            {
                condition = "convert(" + ddlFieldNameBin.SelectedValue + ",System.String) like '%" + txtValueBin.Text + "%'";
            }
            else
            {
                condition = "convert(" + ddlFieldNameBin.SelectedValue + ",System.String) Like '" + txtValueBin.Text + "%'";
            }

            DataTable dtCust = (DataTable)Session["dtBinDistrict"];
            DataView view = new DataView(dtCust, condition, "", DataViewRowState.CurrentRows);
            Session["dtBinFilter"] = view.ToTable();
            lblTotalRecordsBin.Text = Resources.Attendance.Total_Records + " : " + view.ToTable().Rows.Count.ToString() + "";
            GvDistrictBin.DataSource = view.ToTable();
            GvDistrictBin.DataBind();

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
            System.Web.UI.ScriptManager.GetCurrent(this).SetFocus(txtValueBin);
        }
    }
    protected void btnRestoreSelected_Click(object sender, EventArgs e)
    {
        int Msg = 0;
        DataTable dt = objDesg.GetDistrictMasterInactive(Session["CompId"].ToString().ToString());

        if (GvDistrictBin.Rows.Count != 0)
        {
            if (lblSelectedRecord.Text != "")
            {
                for (int j = 0; j < lblSelectedRecord.Text.Split(',').Length; j++)
                {
                    if (lblSelectedRecord.Text.Split(',')[j] != "")
                    {

                        Msg = objDesg.DeleteDistrictMaster(Session["CompId"].ToString().ToString(), lblSelectedRecord.Text.Split(',')[j].Trim().ToString(), true.ToString(), Session["UserId"].ToString(), DateTime.Now.ToString());
                    }
                }
            }

            if (Msg != 0)
            {
                FillGrid();
                FillGridBin();
                ViewState["Select"] = null;
                lblSelectedRecord.Text = "";
                DisplayMessage("Record Activated");

            }
            else
            {
                int fleg = 0;
                foreach (GridViewRow Gvr in GvDistrictBin.Rows)
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
        System.Web.UI.ScriptManager.GetCurrent(this).SetFocus(txtValueBin);
    }
    protected void ImgbtnSelectAll_Click(object sender, ImageClickEventArgs e)
    {
        DataTable dtDistrict = (DataTable)Session["dtbinFilter"];

        if (ViewState["Select"] == null)
        {
            ViewState["Select"] = 1;
            foreach (DataRow dr in dtDistrict.Rows)
            {
                if (!lblSelectedRecord.Text.Split(',').Contains(dr["District_ID"]))
                {
                    lblSelectedRecord.Text += dr["District_ID"] + ",";
                }
            }
            for (int i = 0; i < GvDistrictBin.Rows.Count; i++)
            {
                string[] split = lblSelectedRecord.Text.Split(',');
                Label lblconid = (Label)GvDistrictBin.Rows[i].FindControl("lblDistrictId");
                for (int j = 0; j < lblSelectedRecord.Text.Split(',').Length; j++)
                {
                    if (lblSelectedRecord.Text.Split(',')[j] != "")
                    {
                        if (lblconid.Text.Trim().ToString() == lblSelectedRecord.Text.Split(',')[j].Trim().ToString())
                        {
                            ((CheckBox)GvDistrictBin.Rows[i].FindControl("chkgvSelect")).Checked = true;
                        }
                    }
                }
            }
        }
        else
        {
            lblSelectedRecord.Text = "";
            DataTable dtDistrict1 = (DataTable)Session["dtBinFilter"];
            GvDistrictBin.DataSource = dtDistrict1;
            GvDistrictBin.DataBind();
            ViewState["Select"] = null;
        }



    }
    protected void chkgvSelectAll_CheckedChanged(object sender, EventArgs e)
    {
        CheckBox chkSelAll = ((CheckBox)GvDistrictBin.HeaderRow.FindControl("chkgvSelectAll"));
        for (int i = 0; i < GvDistrictBin.Rows.Count; i++)
        {
            ((CheckBox)GvDistrictBin.Rows[i].FindControl("chkgvSelect")).Checked = chkSelAll.Checked;
            if (chkSelAll.Checked)
            {
                if (!lblSelectedRecord.Text.Split(',').Contains(((Label)(GvDistrictBin.Rows[i].FindControl("lblDistrictId"))).Text.Trim().ToString()))
                {
                    lblSelectedRecord.Text += ((Label)(GvDistrictBin.Rows[i].FindControl("lblDistrictId"))).Text.Trim().ToString() + ",";
                }
            }
            else
            {
                string temp = string.Empty;
                string[] split = lblSelectedRecord.Text.Split(',');
                foreach (string item in split)
                {
                    if (item != ((Label)(GvDistrictBin.Rows[i].FindControl("lblDistrictId"))).Text.Trim().ToString())
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
        Label lb = (Label)GvDistrictBin.Rows[index].FindControl("lblDistrictId");
        if (((CheckBox)GvDistrictBin.Rows[index].FindControl("chkgvSelect")).Checked)
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
    protected void btnRefreshBin_Click(object sender, EventArgs e)
    {

        FillGrid();
        FillGridBin();
        txtValueBin.Text = "";
        ddlOptionBin.SelectedIndex = 2;
        ddlFieldNameBin.SelectedIndex = 0;
        lblSelectedRecord.Text = "";
        ViewState["Select"] = null;
        System.Web.UI.ScriptManager.GetCurrent(this).SetFocus(txtValueBin);
    }
    protected void txtDistrictName_TextChanged(object sender, EventArgs e)
    {
        if (editid.Value == "")
        {
            DataTable dt = objDesg.GetDistrictMasterByDistrictName(Session["CompId"].ToString().ToString(), txtDistrictName.Text.Trim());
            if (dt.Rows.Count > 0)
            {
                txtDistrictName.Text = "";
                DisplayMessage("District Name Already Exists");
                System.Web.UI.ScriptManager.GetCurrent(this).SetFocus(txtDistrictName);
                return;
            }
            DataTable dt1 = objDesg.GetDistrictMasterInactive(Session["CompId"].ToString().ToString());
            dt1 = new DataView(dt1, "District='" + txtDistrictName.Text + "'", "", DataViewRowState.CurrentRows).ToTable();
            if (dt1.Rows.Count > 0)
            {
                txtDistrictName.Text = "";
                DisplayMessage("District Name Already Exists - Go to Bin Tab");
                System.Web.UI.ScriptManager.GetCurrent(this).SetFocus(txtDistrictName);
                return;
            }
            txtDistrictNameL.Focus();
        }
        else
        {
            DataTable dtTemp = objDesg.GetDistrictMasterById(Session["CompId"].ToString().ToString(), editid.Value);
            if (dtTemp.Rows.Count > 0)
            {
                if (dtTemp.Rows[0]["District"].ToString() != txtDistrictName.Text)
                {
                    DataTable dt = objDesg.GetDistrictMaster(Session["CompId"].ToString().ToString());
                    dt = new DataView(dt, "District='" + txtDistrictName.Text + "'", "", DataViewRowState.CurrentRows).ToTable();
                    if (dt.Rows.Count > 0)
                    {
                        txtDistrictName.Text = "";
                        DisplayMessage("District Name Already Exists");
                        System.Web.UI.ScriptManager.GetCurrent(this).SetFocus(txtDistrictName);
                        return;
                    }
                    DataTable dt1 = objDesg.GetDistrictMasterInactive(Session["CompId"].ToString().ToString());
                    dt1 = new DataView(dt1, "District='" + txtDistrictName.Text + "'", "", DataViewRowState.CurrentRows).ToTable();
                    if (dt1.Rows.Count > 0)
                    {
                        txtDistrictName.Text = "";
                        DisplayMessage("District Name Already Exists - Go to Bin Tab");
                        System.Web.UI.ScriptManager.GetCurrent(this).SetFocus(txtDistrictName);
                        return;
                    }
                }
            }
            txtDistrictNameL.Focus();
        }

    }
    #endregion

}
