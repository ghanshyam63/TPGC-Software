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
using System.Text;
public partial class HRSetup_DeductionMaster : System.Web.UI.Page
{
    #region Defined Class Object
    Common cmn = new Common();
    Set_Deduction ObjAddDed = new Set_Deduction();
    SystemParameter ObjSysParam = new SystemParameter();
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

            ddlOption.SelectedIndex = 2;
            FillGridBin();
            FillGrid();
            pnlBin.Visible = false;

        }
        AllPageCode();
    }

    #region AllPageCode

    public void AllPageCode()
    {
        Page.Title = ObjSysParam.GetSysTitle();
        StrUserId = Session["UserId"].ToString();
        StrCompId = Session["CompId"].ToString();
        Session["AccordianId"] = "19";
        Session["HeaderText"] = "HR";
        DataTable dtAllPageCode = cmn.GetAllPagePermission(StrUserId.ToString(), "19", "66");
        if (dtAllPageCode.Rows.Count != 0)
        {
            if (dtAllPageCode.Rows[0][0].ToString() == "SuperAdmin")
            {
                btnCSave.Visible = true;
                foreach (GridViewRow Row in GvDeduction.Rows)
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
                    foreach (GridViewRow Row in GvDeduction.Rows)
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

    #region User Defined Funcation
    public void FillGridBin()
    {

        DataTable dt = new DataTable();
        dt = ObjAddDed.GetDeductionFalseAll(StrCompId.ToString());
        GvDeductionBin.DataSource = dt;
        GvDeductionBin.DataBind();
        Session["dtBinDeduction"] = dt;
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
        DataTable dtBrand = ObjAddDed.GetDeductionTrueAll(StrCompId.ToString());
        lblTotalRecords.Text = Resources.Attendance.Total_Records + ": " + dtBrand.Rows.Count + "";
        Session["dtDeduction"] = dtBrand;
        Session["dtFilter"] = dtBrand;
        if (dtBrand != null && dtBrand.Rows.Count > 0)
        {
            GvDeduction.DataSource = dtBrand;
            GvDeduction.DataBind();
        }
        else
        {
            GvDeduction.DataSource = null;
            GvDeduction.DataBind();
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

        txtDeductionName.Text = "";
        editid.Value = "";
        btnNew.Text = Resources.Attendance.New;
        ddlFieldName.SelectedIndex = 1;
        ddlOption.SelectedIndex = 2;
        txtValue.Text = "";
        txtValueBin.Text = "";
        lblSelectedRecord.Text = "";
        System.Web.UI.ScriptManager.GetCurrent(this).SetFocus(txtValue);
        txtDeductionNameL.Text = "";
    }
    #endregion
    #region Auto Complete Method/Funcation
    [System.Web.Services.WebMethodAttribute(), System.Web.Script.Services.ScriptMethodAttribute()]
    public static string[] GetCompletionList(string prefixText, int count, string contextKey)
    {
        Set_Deduction obj = new Set_Deduction();
        DataTable dt = obj.GetDistinctDeduction(HttpContext.Current.Session["CompId"].ToString(), prefixText);
        string[] str = new string[dt.Rows.Count];

        if (dt.Rows.Count > 0)
        {
            for (int i = 0; i < dt.Rows.Count; i++)
            {
                str[i] = dt.Rows[i][0].ToString();
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
                dt = obj.GetDeductionTrueAll("1");
                if (dt.Rows.Count > 0)
                {
                    str = new string[dt.Rows.Count];
                    for (int i = 0; i < dt.Rows.Count; i++)
                    {
                        str[i] = dt.Rows[i]["Deduction"].ToString();
                    }
                }
            }
        }
        return str;
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
    protected void IbtnDelete_Command(object sender, CommandEventArgs e)
    {
        editid.Value = e.CommandArgument.ToString();
        int b = 0;
        String CompanyId = StrCompId.ToString();
        String UserId = StrUserId.ToString();
        b = ObjAddDed.DeleteDeduction(StrCompId.ToString(), editid.Value, "false", StrUserId.ToString(), DateTime.Now.ToString());
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
        System.Web.UI.ScriptManager.GetCurrent(this).SetFocus(txtDeductionName);
    }

    protected void btnCSave_Click(object sender, EventArgs e)
    {
        if (txtDeductionName.Text == "" || txtDeductionName.Text == null)
        {
            DisplayMessage("Enter Deduction Name");
            System.Web.UI.ScriptManager.GetCurrent(this).SetFocus(txtDeductionName);
            return;
        }
        int b = 0;
        if (editid.Value != "")
        {

            DataTable dtCate = ObjAddDed.GetDeductionTrueAll(StrCompId.ToString());
            dtCate = new DataView(dtCate, "Deduction='" + txtDeductionName.Text + "'", "", DataViewRowState.CurrentRows).ToTable();
            if (dtCate.Rows.Count > 0)
            {
                if (dtCate.Rows[0]["Deduction_Id"].ToString() != editid.Value)
                {
                    txtDeductionName.Text = "";
                    DisplayMessage("Deduction Already Exists");
                    System.Web.UI.ScriptManager.GetCurrent(this).SetFocus(txtDeductionName);
                    return;
                }
            }


            b = ObjAddDed.UpdateDeduction(StrCompId.ToString(), editid.Value, txtDeductionName.Text, txtDeductionNameL.Text.Trim().ToString(), "", "", "", "", "", true.ToString(), DateTime.Now.ToString(), "True", Session["UserId"].ToString(), DateTime.Now.ToString());
            editid.Value = "";
            if (b != 0)
            {
                Reset();
                FillGrid();
                DisplayMessage("Record Updated");
                System.Web.UI.ScriptManager.GetCurrent(this).SetFocus(txtDeductionName);
            }
            else
            {
                DisplayMessage("Record  Not Updated");
                System.Web.UI.ScriptManager.GetCurrent(this).SetFocus(txtDeductionName);
            }
        }

        else
        {
            DataTable dtPro = ObjAddDed.GetDeductionTrueAll(StrCompId.ToString());
            dtPro = new DataView(dtPro, "Deduction='" + txtDeductionName.Text + "'", "", DataViewRowState.CurrentRows).ToTable();

            if (dtPro.Rows.Count > 0)
            {
                txtDeductionName.Text = "";
                DisplayMessage("Deduction Name Already Exists");
                System.Web.UI.ScriptManager.GetCurrent(this).SetFocus(txtDeductionName);
                return;
            }
            b = ObjAddDed.InsertDeduction(StrCompId.ToString(), txtDeductionName.Text, txtDeductionNameL.Text.Trim(), "", "", "", "", "", true.ToString(), DateTime.Now.ToString(), "True", Session["UserId"].ToString(), DateTime.Now.ToString(), Session["UserId"].ToString(), DateTime.Now.ToString());
            if (b != 0)
            {
                DisplayMessage("Record Saved");
                Reset();
                FillGrid();
                System.Web.UI.ScriptManager.GetCurrent(this).SetFocus(txtDeductionName);
            }
            else
            {
                DisplayMessage("Record  Not Saved");
                System.Web.UI.ScriptManager.GetCurrent(this).SetFocus(txtDeductionName);
            }
        }
    }


    protected void GvDeductionBin_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        GvDeductionBin.PageIndex = e.NewPageIndex;

        DataTable dt = (DataTable)Session["dtBinFilter"];
        GvDeductionBin.DataSource = dt;
        GvDeductionBin.DataBind();
        AllPageCode();
        string temp = string.Empty;


        for (int i = 0; i < GvDeductionBin.Rows.Count; i++)
        {
            Label lblconid = (Label)GvDeductionBin.Rows[i].FindControl("lblgvDeductionId");
            string[] split = lblSelectedRecord.Text.Split(',');

            for (int j = 0; j < lblSelectedRecord.Text.Split(',').Length; j++)
            {
                if (lblSelectedRecord.Text.Split(',')[j] != "")
                {
                    if (lblconid.Text.Trim().ToString() == lblSelectedRecord.Text.Split(',')[j].Trim().ToString())
                    {
                        ((CheckBox)GvDeductionBin.Rows[i].FindControl("chkgvSelect")).Checked = true;
                    }
                }
            }
        }

    }

    protected void btnEdit_Command(object sender, CommandEventArgs e)
    {
        editid.Value = e.CommandArgument.ToString();

        DataTable dtTax = ObjAddDed.GetDeductionTruebyId(StrCompId.ToString(), editid.Value);

        btnNew.Text = Resources.Attendance.Edit;

        txtDeductionName.Text = dtTax.Rows[0]["Deduction"].ToString();
        txtDeductionNameL.Text = dtTax.Rows[0]["Deduction_L"].ToString();
        System.Web.UI.ScriptManager.GetCurrent(this).SetFocus(txtDeductionName);
    }


    protected void GvDeduction_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        GvDeduction.PageIndex = e.NewPageIndex;
        DataTable dt = (DataTable)Session["dtFilter"];
        GvDeduction.DataSource = dt;
        GvDeduction.DataBind();
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
            DataTable dtDeduction = (DataTable)Session["dtDeduction"];
            DataView view = new DataView(dtDeduction, condition, "", DataViewRowState.CurrentRows);
            GvDeduction.DataSource = view.ToTable();
            Session["dtFilter"] = view.ToTable();
            lblTotalRecords.Text = Resources.Attendance.Total_Records + " : " + view.ToTable().Rows.Count + "";
            GvDeduction.DataBind();
            AllPageCode();
        }
    }


    protected void btnRefresh_Click(object sender, ImageClickEventArgs e)
    {
        FillGrid();
        FillGridBin();
        ddlFieldName.SelectedIndex = 1;
        ddlOption.SelectedIndex = 2;
        txtValue.Text = "";
    }

    protected void btnRestoreSelected_Click(object sender, ImageClickEventArgs e)
    {
        int Msg = 0;
        DataTable dt = ObjAddDed.GetDeductionFalseAll(StrCompId.ToString());

        if (GvDeductionBin.Rows.Count != 0)
        {
            if (lblSelectedRecord.Text != "")
            {
                for (int j = 0; j < lblSelectedRecord.Text.Split(',').Length; j++)
                {
                    if (lblSelectedRecord.Text.Split(',')[j] != "")
                    {

                        Msg = ObjAddDed.DeleteDeduction(StrCompId.ToString(), lblSelectedRecord.Text.Split(',')[j].Trim().ToString(), true.ToString(), Session["UserId"].ToString(), DateTime.Now.ToString());
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
                foreach (GridViewRow Gvr in GvDeductionBin.Rows)
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
        DataTable dtDeduction = (DataTable)Session["dtbinFilter"];

        if (ViewState["Select"] == null)
        {
            ViewState["Select"] = 1;
            foreach (DataRow dr in dtDeduction.Rows)
            {
                if (!lblSelectedRecord.Text.Split(',').Contains(dr["Deduction_Id"]))
                {
                    lblSelectedRecord.Text += dr["Deduction_Id"] + ",";
                }
            }
            for (int i = 0; i < GvDeductionBin.Rows.Count; i++)
            {
                string[] split = lblSelectedRecord.Text.Split(',');
                Label lblconid = (Label)GvDeductionBin.Rows[i].FindControl("lblgvDeductionId");
                for (int j = 0; j < lblSelectedRecord.Text.Split(',').Length; j++)
                {
                    if (lblSelectedRecord.Text.Split(',')[j] != "")
                    {
                        if (lblconid.Text.Trim().ToString() == lblSelectedRecord.Text.Split(',')[j].Trim().ToString())
                        {
                            ((CheckBox)GvDeductionBin.Rows[i].FindControl("chkgvSelect")).Checked = true;
                        }
                    }
                }
            }
        }
        else
        {
            lblSelectedRecord.Text = "";
            DataTable dtAddressCategory1 = (DataTable)Session["dtBinFilter"];
            GvDeductionBin.DataSource = dtAddressCategory1;
            GvDeductionBin.DataBind();
            ViewState["Select"] = null;
        }


    }
    protected void GvDeduction_Sorting(object sender, GridViewSortEventArgs e)
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
        GvDeduction.DataSource = dt;
        GvDeduction.DataBind();

        AllPageCode();
    }

    protected void GvDeductionBin_Sorting(object sender, GridViewSortEventArgs e)
    {
        HDFSortbin.Value = HDFSortbin.Value == "ASC" ? "DESC" : "ASC";
        DataTable dt = new DataTable();
        dt = ObjAddDed.GetDeductionFalseAll(StrCompId.ToString());
        DataView dv = new DataView(dt);
        string Query = "" + e.SortExpression + " " + HDFSortbin.Value + "";
        dv.Sort = Query;
        dt = dv.ToTable();
        Session["dtInactive"] = dt;
        GvDeductionBin.DataSource = dt;
        GvDeductionBin.DataBind();
        lblSelectedRecord.Text = "";
        AllPageCode();
    }
    protected void btnRefreshBin_Click(object sender, ImageClickEventArgs e)
    {
        FillGrid();
        FillGridBin();
        txtValueBin.Text = "";
        ddlOptionBin.SelectedIndex = 2;
        ddlFieldNameBin.SelectedIndex = 1;
        lblSelectedRecord.Text = "";
        ViewState["Select"] = null;
        System.Web.UI.ScriptManager.GetCurrent(this).SetFocus(txtValueBin);
    }
    protected void btnbindBin_Click(object sender, ImageClickEventArgs e)
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

            DataTable dtCust = (DataTable)Session["dtBinDeduction"];
            DataView view = new DataView(dtCust, condition, "", DataViewRowState.CurrentRows);
            Session["dtBinFilter"] = view.ToTable();
            lblTotalRecordsBin.Text = Resources.Attendance.Total_Records + " : " + view.ToTable().Rows.Count.ToString() + "";
            GvDeductionBin.DataSource = view.ToTable();
            GvDeductionBin.DataBind();

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
    protected void chkgvSelect_CheckedChanged(object sender, EventArgs e)
    {
        string empidlist = string.Empty;
        string temp = string.Empty;
        int index = ((GridViewRow)((CheckBox)sender).Parent.Parent).RowIndex;
        Label lb = (Label)GvDeductionBin.Rows[index].FindControl("lblgvDeductionId");
        if (((CheckBox)GvDeductionBin.Rows[index].FindControl("chkgvSelect")).Checked)
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
    protected void chkgvSelectAll_CheckedChanged(object sender, EventArgs e)
    {
        CheckBox chkSelAll = ((CheckBox)GvDeductionBin.HeaderRow.FindControl("chkgvSelectAll"));
        for (int i = 0; i < GvDeductionBin.Rows.Count; i++)
        {
            ((CheckBox)GvDeductionBin.Rows[i].FindControl("chkgvSelect")).Checked = chkSelAll.Checked;
            if (chkSelAll.Checked)
            {
                if (!lblSelectedRecord.Text.Split(',').Contains(((Label)(GvDeductionBin.Rows[i].FindControl("lblgvDeductionId"))).Text.Trim().ToString()))
                {
                    lblSelectedRecord.Text += ((Label)(GvDeductionBin.Rows[i].FindControl("lblgvDeductionId"))).Text.Trim().ToString() + ",";
                }
            }
            else
            {
                string temp = string.Empty;
                string[] split = lblSelectedRecord.Text.Split(',');
                foreach (string item in split)
                {
                    if (item != ((Label)(GvDeductionBin.Rows[i].FindControl("lblgvDeductionId"))).Text.Trim().ToString())
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
    protected void txtDeductionName_TextChanged(object sender, EventArgs e)
    {
        if (editid.Value == "")
        {
            DataTable dt = ObjAddDed.GetDeductionByDeduction(StrCompId.ToString(), txtDeductionName.Text.Trim());
            if (dt.Rows.Count > 0)
            {
                txtDeductionName.Text = "";
                DisplayMessage("Deduction Name Already Exists");
                System.Web.UI.ScriptManager.GetCurrent(this).SetFocus(txtDeductionName);
                return;
            }
            DataTable dt1 = ObjAddDed.GetDeductionFalseAll(StrCompId.ToString());
            dt1 = new DataView(dt1, "Deduction='" + txtDeductionName.Text + "'", "", DataViewRowState.CurrentRows).ToTable();
            if (dt1.Rows.Count > 0)
            {
                txtDeductionName.Text = "";
                DisplayMessage("Deduction Name Already Exists - Go to Bin Tab");
                System.Web.UI.ScriptManager.GetCurrent(this).SetFocus(txtDeductionName);
                return;
            }
        }
        else
        {
            DataTable dtTemp = ObjAddDed.GetDeductionTruebyId(StrCompId.ToString(), editid.Value);
            if (dtTemp.Rows.Count > 0)
            {
                if (dtTemp.Rows[0]["Deduction"].ToString() != txtDeductionName.Text)
                {
                    DataTable dt = ObjAddDed.GetDeductionTrueAll(StrCompId.ToString());
                    dt = new DataView(dt, "Deduction='" + txtDeductionName.Text + "'", "", DataViewRowState.CurrentRows).ToTable();
                    if (dt.Rows.Count > 0)
                    {
                        txtDeductionName.Text = "";
                        DisplayMessage("Deduction Name Already Exists");
                        System.Web.UI.ScriptManager.GetCurrent(this).SetFocus(txtDeductionName);
                        return;
                    }
                    DataTable dt1 = ObjAddDed.GetDeductionFalseAll(StrCompId.ToString());
                    dt1 = new DataView(dt1, "Deduction='" + txtDeductionName.Text + "'", "", DataViewRowState.CurrentRows).ToTable();
                    if (dt1.Rows.Count > 0)
                    {
                        txtDeductionName.Text = "";
                        DisplayMessage("Deduction Name Already Exists - Go to Bin Tab");
                        System.Web.UI.ScriptManager.GetCurrent(this).SetFocus(txtDeductionName);
                        return;
                    }
                }
            }
        }
        System.Web.UI.ScriptManager.GetCurrent(this).SetFocus(txtDeductionNameL);
    }
    #endregion

}
