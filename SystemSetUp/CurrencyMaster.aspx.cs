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

public partial class SystemSetUp_CurrencyMaster : BasePage
{
    #region defind Class Object
    Common cmn = new Common();
    CurrencyMaster objCurr = new CurrencyMaster();
    string StrCompId = string.Empty;
    string StrBrandId = "1";
    string StrUserId = string.Empty;
    #endregion

    protected void Page_Load(object sender, EventArgs e)
    {
       
        if (Session["UserId"] == null)
        {
            Response.Redirect("~/Login.aspx");
        }
        StrCompId = Session["CompId"].ToString();
        StrUserId = Session["UserId"].ToString();
        Session["AccordianId"] = "7";
        Session["HeaderText"] = "System Setup";
        if (!IsPostBack)
        {
            ddlOption.SelectedIndex = 2;
            btnList_Click(null, null);
            FillGridBin();
            FillGrid();
        }
    }

    #region System defind Funcation
    protected void btnList_Click(object sender, EventArgs e)
    {
        pnlMenuList.BackColor = System.Drawing.ColorTranslator.FromHtml("#ccddee");
        pnlMenuNew.BackColor = System.Drawing.ColorTranslator.FromHtml("#90bde9");
        pnlMenuBin.BackColor = System.Drawing.ColorTranslator.FromHtml("#90bde9");

        PnlList.Visible = true;
        PnlNewEdit.Visible = false;
        PnlBin.Visible = false;
    }
    protected void btnNew_Click(object sender, EventArgs e)
    {
        pnlMenuNew.BackColor = System.Drawing.ColorTranslator.FromHtml("#ccddee");

        pnlMenuList.BackColor = System.Drawing.ColorTranslator.FromHtml("#90bde9");
        pnlMenuBin.BackColor = System.Drawing.ColorTranslator.FromHtml("#90bde9");

        PnlList.Visible = false;
        PnlNewEdit.Visible = true;
        PnlBin.Visible = false;

    }
    protected void btnEdit_Command(object sender, CommandEventArgs e)
    {
        editid.Value = e.CommandArgument.ToString();

        DataTable dtCurrEdit = objCurr.GetCurrencyMasterById(editid.Value);

        btnNew.Text = Resources.Attendance.Edit;

        txtCurrencyCode.Text = dtCurrEdit.Rows[0]["Currency_Code"].ToString();
        txtCurrencyName.Text = dtCurrEdit.Rows[0]["Currency_Name"].ToString();
        txtLCurrencyName.Text = dtCurrEdit.Rows[0]["Currency_Name_L"].ToString();
        txtCurrencySymbol.Text = dtCurrEdit.Rows[0]["Currency_Symbol"].ToString();
        txtCurrencyValue.Text = dtCurrEdit.Rows[0]["Currency_Value"].ToString();

        btnNew_Click(null, null);

        System.Web.UI.ScriptManager.GetCurrent(this).SetFocus(txtCurrencyCode);
    }
    protected void GvCurrency_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        GvCurrency.PageIndex = e.NewPageIndex;
        DataTable dt = (DataTable)Session["dtFilter"];
        GvCurrency.DataSource = dt;
        GvCurrency.DataBind();
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
            DataTable dtCurrency = (DataTable)Session["dtCurrency"];
            DataView view = new DataView(dtCurrency, condition, "", DataViewRowState.CurrentRows);
            GvCurrency.DataSource = view.ToTable();
            Session["dtFilter"] = view.ToTable();
            lblTotalRecords.Text = Resources.Attendance.Total_Records + " : " + view.ToTable().Rows.Count + "";
            GvCurrency.DataBind();
        }
    }
    protected void GvCurrency_Sorting(object sender, GridViewSortEventArgs e)
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
        GvCurrency.DataSource = dt;
        GvCurrency.DataBind();
    }
    protected void IbtnDelete_Command(object sender, CommandEventArgs e)
    {
        editid.Value = e.CommandArgument.ToString();
        int b = 0;
        b = objCurr.DeleteCurrencyMaster(editid.Value, "false", StrUserId, DateTime.Now.ToString());
        if (b != 0)
        {
            DisplayMessage("Record Delete");
        }
        else
        {
            DisplayMessage("Record  Not Deleted");
        }
        FillGridBin(); //Update grid view in bin tab
        FillGrid();
        Reset();
    }
    protected void btnRefreshReport_Click(object sender, ImageClickEventArgs e)
    {
        FillGrid();
        FillGridBin();
        ddlFieldName.SelectedIndex = 1;
        ddlOption.SelectedIndex = 3;
        txtValue.Text = "";
    }
    protected void BtnCCancel_Click(object sender, EventArgs e)
    {
        Reset();
        btnList_Click(null, null);
        FillGridBin();
        FillGrid();
    }
    protected void BtnReset_Click(object sender, EventArgs e)
    {
        Reset();
        System.Web.UI.ScriptManager.GetCurrent(this).SetFocus(txtCurrencyCode);
    }
    protected void btnCSave_Click(object sender, EventArgs e)
    {
        //Condition added to check brand name entered before save
        if (txtCurrencyName.Text == "" || txtCurrencyName.Text == null)
        {
            DisplayMessage("Enter Currency Name");
            System.Web.UI.ScriptManager.GetCurrent(this).SetFocus(txtCurrencyName);
            return;
        }

        if (txtCurrencyValue.Text == "" || txtCurrencyValue.Text == null)
        {
            DisplayMessage("Enter Currency Value");
            System.Web.UI.ScriptManager.GetCurrent(this).SetFocus(txtCurrencyValue);
            return;
        }
        else
        {
            float flTemp = 0;
            if (float.TryParse(txtCurrencyValue.Text, out flTemp))
            {

            }
            else
            {
                DisplayMessage("Enter Numeric Value Only");
                txtCurrencyValue.Text = "";
                System.Web.UI.ScriptManager.GetCurrent(this).SetFocus(txtCurrencyValue);
                return;
            }
        }
        int b = 0;
        if (editid.Value != "")
        {
            //Code to check whether the new name after edit does not exists


            DataTable dtCurr1 = objCurr.GetCurrencyMaster();
            dtCurr1 = new DataView(dtCurr1, "Currency_Code='" + txtCurrencyCode.Text + "'", "", DataViewRowState.CurrentRows).ToTable();
            if (dtCurr1.Rows.Count > 0)
            {
                txtCurrencyCode.Text = "";
                DisplayMessage("Currency Code Already Exists");
                System.Web.UI.ScriptManager.GetCurrent(this).SetFocus(txtCurrencyCode);
                return;
            }

            DataTable dtPBrand = objCurr.GetCurrencyMasterByCurrencyName(txtCurrencyName.Text);
            if (dtPBrand.Rows.Count > 0)
            {
                if (dtPBrand.Rows[0]["Currency_ID"].ToString() != editid.Value)
                {
                    txtCurrencyName.Text = "";
                    DisplayMessage("Currency Name Already Exists");
                    System.Web.UI.ScriptManager.GetCurrent(this).SetFocus(txtCurrencyName);
                    return;
                }
            }

            b = objCurr.UpdateCurrencyMaster(editid.Value, txtCurrencyName.Text, txtLCurrencyName.Text, txtCurrencyCode.Text, txtCurrencySymbol.Text, txtCurrencyValue.Text, "False", "", "", "", "", "", "True", DateTime.Now.ToString(), "True", Session["UserId"].ToString(), DateTime.Now.ToString());

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

            DataTable dtCurr1 = objCurr.GetCurrencyMaster();
            dtCurr1 = new DataView(dtCurr1,"Currency_Code='"+txtCurrencyCode.Text+"'","",DataViewRowState.CurrentRows).ToTable();

            if (dtCurr1.Rows.Count > 0)
            {
                txtCurrencyCode.Text = "";
                DisplayMessage("Currency Code Already Exists");
                System.Web.UI.ScriptManager.GetCurrent(this).SetFocus(txtCurrencyCode);
                return;
            }



            DataTable dtCurr = objCurr.GetCurrencyMasterByCurrencyName(txtCurrencyName.Text);
            if (dtCurr.Rows.Count > 0)
            {
                txtCurrencyName.Text = "";
                DisplayMessage("Currency Name Already Exists");
                System.Web.UI.ScriptManager.GetCurrent(this).SetFocus(txtCurrencyName);
                return;
            }

            b = objCurr.InsertCurrencyMaster(txtCurrencyName.Text, txtLCurrencyName.Text, txtCurrencyCode.Text, txtCurrencySymbol.Text, txtCurrencyValue.Text, "False", "", "", "", "", "", "True", DateTime.Now.ToString(), "True", Session["UserId"].ToString(), DateTime.Now.ToString(), Session["UserId"].ToString(), DateTime.Now.ToString());
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
        b = objCurr.DeleteCurrencyMaster(editid.Value, true.ToString(), StrUserId, DateTime.Now.ToString());
        if (b != 0)
        {
            DisplayMessage("Record Activated");
        }
        else
        {
            DisplayMessage("Record Not Activated");
        }
        FillGrid();
        FillGridBin();
        Reset();
    }
    protected void txtCurrencyName_TextChanged(object sender, EventArgs e)
    {
        if (editid.Value == "")
        {
            DataTable dt = objCurr.GetCurrencyMasterByCurrencyName(txtCurrencyName.Text);
            if (dt.Rows.Count > 0)
            {
                txtCurrencyName.Text = "";
                DisplayMessage("Currency Name Already Exists");
                System.Web.UI.ScriptManager.GetCurrent(this).SetFocus(txtCurrencyName);
                return;
            }
            DataTable dt1 = objCurr.GetCurrencyMasterInactive();
            dt1 = new DataView(dt1, "Currency_Name='" + txtCurrencyName.Text + "'", "", DataViewRowState.CurrentRows).ToTable();
            if (dt1.Rows.Count > 0)
            {
                txtCurrencyName.Text = "";
                DisplayMessage("Currency Name Already Exists - Go to Bin Tab");
                System.Web.UI.ScriptManager.GetCurrent(this).SetFocus(txtCurrencyName);
                return;
            }
        }
        else
        {
            DataTable dtTemp = objCurr.GetCurrencyMasterById(editid.Value);
            if (dtTemp.Rows.Count > 0)
            {
                if (dtTemp.Rows[0]["Currency_Name"].ToString() != txtCurrencyName.Text)
                {
                    DataTable dt = objCurr.GetCurrencyMasterByCurrencyName(txtCurrencyName.Text);
                    if (dt.Rows.Count > 0)
                    {
                        txtCurrencyName.Text = "";
                        DisplayMessage("Currency Name Already Exists");
                        System.Web.UI.ScriptManager.GetCurrent(this).SetFocus(txtCurrencyName);
                        return;
                    }
                    DataTable dt1 = objCurr.GetCurrencyMasterInactive();
                    dt1 = new DataView(dt1, "Currency_Name='" + txtCurrencyName.Text + "'", "", DataViewRowState.CurrentRows).ToTable();
                    if (dt1.Rows.Count > 0)
                    {
                        txtCurrencyName.Text = "";
                        DisplayMessage("Currency Name Already Exists - Go to Bin Tab");
                        System.Web.UI.ScriptManager.GetCurrent(this).SetFocus(txtCurrencyName);
                        return;
                    }
                }
            }
        }
        System.Web.UI.ScriptManager.GetCurrent(this).SetFocus(txtLCurrencyName);
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
    }
    protected void GvCurrencyBin_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        GvCurrencyBin.PageIndex = e.NewPageIndex;

        DataTable dt = (DataTable)Session["dtInactive"];
        GvCurrencyBin.DataSource = dt;
        GvCurrencyBin.DataBind();

        string temp = string.Empty;

        for (int i = 0; i < GvCurrencyBin.Rows.Count; i++)
        {
            Label lblconid = (Label)GvCurrencyBin.Rows[i].FindControl("lblgvCurrencyId");
            string[] split = lblSelectedRecord.Text.Split(',');

            for (int j = 0; j < lblSelectedRecord.Text.Split(',').Length; j++)
            {
                if (lblSelectedRecord.Text.Split(',')[j] != "")
                {
                    if (lblconid.Text.Trim().ToString() == lblSelectedRecord.Text.Split(',')[j].Trim().ToString())
                    {
                        ((CheckBox)GvCurrencyBin.Rows[i].FindControl("chkSelect")).Checked = true;
                    }
                }
            }
        }
    }
    protected void GvCurrencyBin_OnSorting(object sender, GridViewSortEventArgs e)
    {
        HDFSortbin.Value = HDFSortbin.Value == "ASC" ? "DESC" : "ASC";
        DataTable dt = new DataTable();
        dt = objCurr.GetCurrencyMasterInactive();
        DataView dv = new DataView(dt);
        string Query = "" + e.SortExpression + " " + HDFSortbin.Value + "";
        dv.Sort = Query;
        dt = dv.ToTable();
        Session["dtInactive"] = dt;
        GvCurrencyBin.DataSource = dt;
        GvCurrencyBin.DataBind();
        lblSelectedRecord.Text = "";
    }
    public void FillGridBin()
    {
        DataTable dt = new DataTable();
        dt = objCurr.GetCurrencyMasterInactive();
        GvCurrencyBin.DataSource = dt;
        GvCurrencyBin.DataBind();
        Session["dtPBrandBin"] = dt;
        Session["dtInactive"] = dt;
        lblTotalRecordsBin.Text = Resources.Attendance.Total_Records + " : " + dt.Rows.Count.ToString() + "";
        lblSelectedRecord.Text = "";
        if (dt.Rows.Count == 0)
        {
            ImgbtnSelectAll.Visible = false;
            imgBtnRestore.Visible = false;
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


            DataTable dtCust = (DataTable)Session["dtPBrandBin"];
            DataView view = new DataView(dtCust, condition, "", DataViewRowState.CurrentRows);
            Session["dtInactive"] = view.ToTable();
            lblTotalRecordsBin.Text = Resources.Attendance.Total_Records + " : " + view.ToTable().Rows.Count.ToString() + "";
            GvCurrencyBin.DataSource = view.ToTable();
            GvCurrencyBin.DataBind();
            lblSelectedRecord.Text = "";
            if (view.ToTable().Rows.Count == 0)
            {
                FillGridBin();
            }
            System.Web.UI.ScriptManager.GetCurrent(this).SetFocus(txtValueBin);
        }
    }
    protected void btnRestoreSelected_Click(object sender, CommandEventArgs e)
    {
        int b = 0;
        DataTable dt = objCurr.GetCurrencyMasterInactive();

        if (GvCurrencyBin.Rows.Count != 0)
        {
            if (lblSelectedRecord.Text != "")
            {
                for (int j = 0; j < lblSelectedRecord.Text.Split(',').Length; j++)
                {
                    if (lblSelectedRecord.Text.Split(',')[j] != "")
                    {
                        //Msg = objTax.DeleteTaxMaster(lblSelectedRecord.Text.Split(',')[j].Trim().ToString(), true.ToString(), Session["UserId"].ToString(), DateTime.Now.ToString());
                        b = objCurr.DeleteCurrencyMaster(lblSelectedRecord.Text.Split(',')[j].Trim().ToString(), true.ToString(), Session["UserId"].ToString(), DateTime.Now.ToString());
                    }
                }
            }

            if (b != 0)
            {
                FillGrid();
                FillGridBin();

                lblSelectedRecord.Text = "";
                DisplayMessage("Record Activate");
            }
            else
            {
                int fleg = 0;
                foreach (GridViewRow Gvr in GvCurrencyBin.Rows)
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
        }
        System.Web.UI.ScriptManager.GetCurrent(this).SetFocus(txtValueBin);
    }
    protected void chkCurrent_CheckedChanged(object sender, EventArgs e)
    {
        CheckBox chkSelAll = ((CheckBox)GvCurrencyBin.HeaderRow.FindControl("chkCurrent"));
        for (int i = 0; i < GvCurrencyBin.Rows.Count; i++)
        {
            ((CheckBox)GvCurrencyBin.Rows[i].FindControl("chkSelect")).Checked = chkSelAll.Checked;
            if (chkSelAll.Checked)
            {
                if (!lblSelectedRecord.Text.Split(',').Contains(((Label)(GvCurrencyBin.Rows[i].FindControl("lblgvCurrencyId"))).Text.Trim().ToString()))
                {
                    lblSelectedRecord.Text += ((Label)(GvCurrencyBin.Rows[i].FindControl("lblgvCurrencyId"))).Text.Trim().ToString() + ",";
                }
            }
            else
            {
                string temp = string.Empty;
                string[] split = lblSelectedRecord.Text.Split(',');
                foreach (string item in split)
                {
                    if (item != ((Label)(GvCurrencyBin.Rows[i].FindControl("lblgvCurrencyId"))).Text.Trim().ToString())
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
    protected void chkSelect_CheckedChanged(object sender, EventArgs e)
    {
        string empidlist = string.Empty;
        string temp = string.Empty;
        int index = ((GridViewRow)((CheckBox)sender).Parent.Parent).RowIndex;
        Label lb = (Label)GvCurrencyBin.Rows[index].FindControl("lblgvCurrencyId");
        if (((CheckBox)GvCurrencyBin.Rows[index].FindControl("chkSelect")).Checked)
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
                if (!lblSelectedRecord.Text.Split(',').Contains(dr["Currency_ID"]))
                {
                    lblSelectedRecord.Text += dr["Currency_ID"] + ",";
                }
            }
            for (int i = 0; i < GvCurrencyBin.Rows.Count; i++)
            {
                string[] split = lblSelectedRecord.Text.Split(',');
                Label lblconid = (Label)GvCurrencyBin.Rows[i].FindControl("lblgvCurrencyId");
                for (int j = 0; j < lblSelectedRecord.Text.Split(',').Length; j++)
                {
                    if (lblSelectedRecord.Text.Split(',')[j] != "")
                    {
                        if (lblconid.Text.Trim().ToString() == lblSelectedRecord.Text.Split(',')[j].Trim().ToString())
                        {
                            ((CheckBox)GvCurrencyBin.Rows[i].FindControl("chkSelect")).Checked = true;
                        }
                    }
                }
            }
        }
        else
        {
            lblSelectedRecord.Text = "";
            DataTable dtUnit1 = (DataTable)Session["dtInactive"];
            GvCurrencyBin.DataSource = dtUnit1;
            GvCurrencyBin.DataBind();
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
                    b = objCurr.DeleteCurrencyMaster(lblSelectedRecord.Text.Split(',')[j].Trim(), true.ToString(), Session["UserId"].ToString(), DateTime.Now.ToString());
                }
            }
        }

        if (b != 0)
        {
            FillGrid();
            FillGridBin();
            lblSelectedRecord.Text = "";
            ViewState["Select"] = null;
            DisplayMessage("Record Activate");
        }
        else
        {
            int fleg = 0;
            foreach (GridViewRow Gvr in GvCurrencyBin.Rows)
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
    }
    #endregion

    #endregion

    #region User defind Funcation
    private void FillGrid()
    {
        DataTable dtBrand = objCurr.GetCurrencyMaster();
        lblTotalRecords.Text = Resources.Attendance.Total_Records + " : " + dtBrand.Rows.Count + "";
        Session["dtCurrency"] = dtBrand;
        Session["dtFilter"] = dtBrand;
        if (dtBrand != null && dtBrand.Rows.Count > 0)
        {
            GvCurrency.DataSource = dtBrand;
            GvCurrency.DataBind();
        }
        else
        {
            GvCurrency.DataSource = null;
            GvCurrency.DataBind();
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
        txtCurrencyCode.Text = "";
        txtCurrencyName.Text = "";
        txtLCurrencyName.Text = "";
        txtCurrencySymbol.Text = "";
        txtCurrencyValue.Text = "";

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

    #region Auto Complete Function
    [System.Web.Services.WebMethodAttribute(), System.Web.Script.Services.ScriptMethodAttribute()]
    public static string[] GetCompletionList(string prefixText, int count, string contextKey)
    {
        CurrencyMaster Curr = new CurrencyMaster();
        DataTable dt = Curr.GetDistinctCurrencyName(prefixText);

        string[] str = new string[dt.Rows.Count];
        for (int i = 0; i < dt.Rows.Count; i++)
        {
            str[i] = dt.Rows[i]["Currency_Name"].ToString();
        }
        return str;
    }
    #endregion
}
