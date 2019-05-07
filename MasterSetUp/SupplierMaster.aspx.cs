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
using System.Text.RegularExpressions;
using System.Collections.Generic;

public partial class MasterSetUp_SupplierMaster : System.Web.UI.Page
{
    #region defind Class Object
    Common cmn = new Common();
    Set_Suppliers objSupplier = new Set_Suppliers();
    Ems_CompanyContactMaster objCompany = new Ems_CompanyContactMaster();
    Set_AddressCategory ObjAddressCat = new Set_AddressCategory();
    Set_AddressChild objAddChild = new Set_AddressChild();
    CountryMaster ObjCountry = new CountryMaster();
    Set_AddressMaster objAddMaster = new Set_AddressMaster();
    CurrencyMaster objCurr = new CurrencyMaster();
    Ems_ContactMaster objContact = new Ems_ContactMaster();
    Ems_Contact_Group objCG = new Ems_Contact_Group();
    SystemParameter ObjSysParam = new SystemParameter();

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
            FillAddressCategory();
            FillCountry();
        }
        AllPageCode();
    }

    #region AllPageCode
    public void AllPageCode()
    {
        StrCompId = Session["CompId"].ToString();
        StrUserId = Session["UserId"].ToString();
        Page.Title = ObjSysParam.GetSysTitle();
        Session["AccordianId"] = "8";
        Session["HeaderText"] = "Master Setup";
        Common cmn = new Common();
        DataTable dtAllPageCode = cmn.GetAllPagePermission(Session["UserId"].ToString(), "8", "20");
        if (dtAllPageCode.Rows.Count != 0)
        {
            if (dtAllPageCode.Rows[0][0].ToString() == "SuperAdmin")
            {
                btnCompanySave.Visible = true;
                btnSupplierSave.Visible = true;
                btnAddressSave.Visible = true;
                foreach (GridViewRow Row in GvAddressName.Rows)
                {
                    ((ImageButton)Row.FindControl("imgBtnAddressEdit")).Visible = true;
                    ((ImageButton)Row.FindControl("imgBtnDelete")).Visible = true;
                }
                foreach (GridViewRow Row in GvSupplier.Rows)
                {
                    ((ImageButton)Row.FindControl("btnEdit")).Visible = true;
                    ((ImageButton)Row.FindControl("IbtnDelete")).Visible = true;
                }
                foreach (GridViewRow Row in GvCompanyAddressName.Rows)
                {
                    ((ImageButton)Row.FindControl("imgBtnCompanyAddressEdit")).Visible = true;
                    ((ImageButton)Row.FindControl("imgBtnCompanyAddressDelete")).Visible = true;
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
                        btnCompanySave.Visible = true;
                        btnSupplierSave.Visible = true;
                        btnAddressSave.Visible = true;
                    }
                    foreach (GridViewRow Row in GvSupplier.Rows)
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
                    foreach (GridViewRow Row in GvCompanyAddressName.Rows)
                    {
                        if (Convert.ToBoolean(DtRow["Op_Edit"].ToString()))
                        {
                            ((ImageButton)Row.FindControl("imgBtnCompanyAddressEdit")).Visible = true;
                        }
                        if (Convert.ToBoolean(DtRow["Op_Delete"].ToString()))
                        {
                            ((ImageButton)Row.FindControl("imgBtnCompanyAddressDelete")).Visible = true;
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
        txtSupplierName.Focus();
    }
    protected void btnEdit_Command(object sender, CommandEventArgs e)
    {
        hdnSupplierId.Value = e.CommandArgument.ToString();

        DataTable dtSupplierEdit = objSupplier.GetSupplierAllDataBySupplierIdWithoutBrand(StrCompId, hdnSupplierId.Value);

        btnNew.Text = Resources.Attendance.Edit;
        if (dtSupplierEdit.Rows.Count > 0)
        {
            txtSupplierName.Text = dtSupplierEdit.Rows[0]["Contact_Name"].ToString();
            txtLSupplierName.Text = dtSupplierEdit.Rows[0]["Contact_Name_L"].ToString();
            txtNickName.Text = dtSupplierEdit.Rows[0]["Nick_Name"].ToString();
            txtCivilId.Text = dtSupplierEdit.Rows[0]["Civil_Id"].ToString();

            string strCompanyContactId = dtSupplierEdit.Rows[0]["Contact_Company_Id"].ToString();
            DataTable dtCompany = objCompany.GetCompanyContactByContactCompanyId(StrCompId, strCompanyContactId);
            if (dtCompany.Rows.Count > 0)
            {
                txtCname.Text = dtCompany.Rows[0]["Company_Name"].ToString() + "/" + strCompanyContactId;
            }

            DataTable dtChild = objAddChild.GetAddressChildDataByAddTypeAndAddRefId("Contact", hdnSupplierId.Value);
            if (dtChild.Rows.Count > 0)
            {
                GvAddressName.DataSource = dtChild;
                GvAddressName.DataBind();
            }

            txtAccountNo.Text = dtSupplierEdit.Rows[0]["Account_No"].ToString();
            txtDebitAmount.Text = dtSupplierEdit.Rows[0]["Db_Amount"].ToString();
            txtCreditAmount.Text = dtSupplierEdit.Rows[0]["Cr_Amount"].ToString();
            txtODebitAmount.Text = dtSupplierEdit.Rows[0]["O_Db_Amount"].ToString();
            txtOCreditAmount.Text = dtSupplierEdit.Rows[0]["O_Cr_Amount"].ToString();
            txtPurchaseLimit.Text = dtSupplierEdit.Rows[0]["Purchase_Limit"].ToString();
            txtReturnDays.Text = dtSupplierEdit.Rows[0]["Return_Days"].ToString();
        }
        btnNew_Click(null, null);

    }
    protected void GvSupplier_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        GvSupplier.PageIndex = e.NewPageIndex;
        DataTable dt = (DataTable)Session["dtFilter"];
        GvSupplier.DataSource = dt;
        GvSupplier.DataBind();
        GvSupplier.BottomPagerRow.Focus();
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
            DataTable dtAdd = (DataTable)Session["dtCurrency"];
            DataView view = new DataView(dtAdd, condition, "", DataViewRowState.CurrentRows);
            GvSupplier.DataSource = view.ToTable();
            Session["dtFilter"] = view.ToTable();
            lblTotalRecords.Text = Resources.Attendance.Total_Records + " : " + view.ToTable().Rows.Count + "";
            GvSupplier.DataBind();
            AllPageCode();
            btnbind.Focus();
        }
    }
    protected void GvSupplier_Sorting(object sender, GridViewSortEventArgs e)
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
        GvSupplier.DataSource = dt;
        GvSupplier.DataBind();
        AllPageCode();
        GvSupplier.HeaderRow.Focus();
    }
    protected void IbtnDelete_Command(object sender, CommandEventArgs e)
    {
        hdnSupplierId.Value = e.CommandArgument.ToString();
        int b = 0;
        b = objSupplier.DeleteSupplierMaster(StrCompId, StrBrandId, hdnSupplierId.Value, "false", StrUserId, DateTime.Now.ToString());
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
        try
        {
            int I = ((GridViewRow)((ImageButton)sender).Parent.Parent).RowIndex;
            ((ImageButton)GvSupplier.Rows[I].FindControl("IbtnDelete")).Focus();
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
        ddlOption.SelectedIndex = 3;
        txtValue.Text = "";
        txtValue.Focus();
    }
    protected void btnSupplierCancel_Click(object sender, EventArgs e)
    {
        Reset();
        btnList_Click(null, null);
        FillGridBin();
        FillGrid();
    }
    protected void BtnReset_Click(object sender, EventArgs e)
    {
        Reset();
        System.Web.UI.ScriptManager.GetCurrent(this).SetFocus(ddlAddressCategory);
    }
    protected void btnSupplierSave_Click(object sender, EventArgs e)
    {
        string strTaxable = string.Empty;
        if (txtSupplierName.Text == "")
        {
            DisplayMessage("Fill Supplier Name");
            txtSupplierName.Focus();
            return;
        }

        if (txtCname.Text == "")
        {
            DisplayMessage("Fill Company Name");
            txtCname.Focus();
            return;
        }
        else
        {
            if (GetCompanyContactId() == "")
            {
                DisplayMessage("Please Choose Company In Suggestions Only");
                txtCname.Text = "";
                txtCname.Focus();
                return;
            }
        }

        if (txtDebitAmount.Text != "")
        {
            float flTemp = 0;
            if (float.TryParse(txtDebitAmount.Text, out flTemp))
            {

            }
            else
            {
                txtDebitAmount.Text = "";
                ScriptManager.RegisterStartupScript(this, typeof(string), "OPEN_WINDOW", "window.alert('Enter Numeric Value Only');", true);
                System.Web.UI.ScriptManager.GetCurrent(this).SetFocus(txtDebitAmount);
                return;
            }
        }
        else
        {
            txtDebitAmount.Text = "0";
        }

        if (txtCreditAmount.Text != "")
        {
            float flTemp = 0;
            if (float.TryParse(txtCreditAmount.Text, out flTemp))
            {

            }
            else
            {
                txtCreditAmount.Text = "";
                ScriptManager.RegisterStartupScript(this, typeof(string), "OPEN_WINDOW", "window.alert('Enter Numeric Value Only');", true);
                System.Web.UI.ScriptManager.GetCurrent(this).SetFocus(txtCreditAmount);
                return;
            }
        }
        else
        {
            txtCreditAmount.Text = "0";
        }

        if (txtODebitAmount.Text != "")
        {
            float flTemp = 0;
            if (float.TryParse(txtODebitAmount.Text, out flTemp))
            {

            }
            else
            {
                txtODebitAmount.Text = "";
                ScriptManager.RegisterStartupScript(this, typeof(string), "OPEN_WINDOW", "window.alert('Enter Numeric Value Only');", true);
                System.Web.UI.ScriptManager.GetCurrent(this).SetFocus(txtODebitAmount);
                return;
            }
        }
        else
        {
            txtODebitAmount.Text = "0";
        }

        if (txtOCreditAmount.Text != "")
        {
            float flTemp = 0;
            if (float.TryParse(txtOCreditAmount.Text, out flTemp))
            {

            }
            else
            {
                txtOCreditAmount.Text = "";
                ScriptManager.RegisterStartupScript(this, typeof(string), "OPEN_WINDOW", "window.alert('Enter Numeric Value Only');", true);
                System.Web.UI.ScriptManager.GetCurrent(this).SetFocus(txtOCreditAmount);
                return;
            }
        }
        else
        {
            txtOCreditAmount.Text = "0";
        }

        if (txtPurchaseLimit.Text != "")
        {
            float flTemp = 0;
            if (float.TryParse(txtPurchaseLimit.Text, out flTemp))
            {

            }
            else
            {
                txtPurchaseLimit.Text = "";
                ScriptManager.RegisterStartupScript(this, typeof(string), "OPEN_WINDOW", "window.alert('Enter Numeric Value Only');", true);
                System.Web.UI.ScriptManager.GetCurrent(this).SetFocus(txtPurchaseLimit);
                return;
            }
        }
        else
        {
            txtPurchaseLimit.Text = "0";
        }

        if (txtReturnDays.Text != "")
        {
            float flTemp = 0;
            if (float.TryParse(txtReturnDays.Text, out flTemp))
            {

            }
            else
            {
                txtReturnDays.Text = "";
                ScriptManager.RegisterStartupScript(this, typeof(string), "OPEN_WINDOW", "window.alert('Enter Numeric Value Only');", true);
                System.Web.UI.ScriptManager.GetCurrent(this).SetFocus(txtReturnDays);
                return;
            }
        }
        else
        {
            txtReturnDays.Text = "0";
        }

        int b = 0;
        if (hdnSupplierId.Value != "")
        {
            objContact.UpdateContactMasterCS(StrCompId, hdnSupplierId.Value, txtSupplierName.Text, txtLSupplierName.Text, txtCivilId.Text, txtNickName.Text, GetCompanyContactId(), "True", Session["UserId"].ToString(), DateTime.Now.ToString());
            b = objSupplier.UpdateSupplierMaster(StrCompId, StrBrandId, hdnSupplierId.Value, txtAccountNo.Text, txtDebitAmount.Text, txtCreditAmount.Text, txtODebitAmount.Text, txtOCreditAmount.Text, txtPurchaseLimit.Text, txtReturnDays.Text, "", "", "", "", "", "True", Session["UserId"].ToString(), DateTime.Now.ToString());

            //Add Address Insert Section.
            objAddChild.DeleteAddressChild("Contact", hdnSupplierId.Value);
            foreach (GridViewRow gvr in GvAddressName.Rows)
            {
                Label lblGvAddressName = (Label)gvr.FindControl("lblgvAddressName");

                if (lblGvAddressName.Text != "")
                {
                    DataTable dtAddId = objAddMaster.GetAddressDataByAddressName(StrCompId, lblGvAddressName.Text);
                    if (dtAddId.Rows.Count > 0)
                    {
                        string strAddressId = dtAddId.Rows[0]["Trans_Id"].ToString();
                        objAddChild.InsertAddressChild(strAddressId, "Contact", hdnSupplierId.Value, "True", "", "", "", "", "", "True", DateTime.Now.ToString(), "True", Session["UserId"].ToString(), DateTime.Now.ToString(), Session["UserId"].ToString(), DateTime.Now.ToString());
                    }
                }
            }
            //End  

            hdnSupplierId.Value = "";
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
            objContact.InsertContactMaster(StrCompId, "", txtSupplierName.Text, txtLSupplierName.Text, txtCivilId.Text, txtNickName.Text, "0", "0", "0", GetCompanyContactId(), "False", "False", "", "", "", "", "", "True", DateTime.Now.ToString(), "True", Session["UserId"].ToString(), DateTime.Now.ToString(), Session["UserId"].ToString(), DateTime.Now.ToString());

            string strMaxId = string.Empty;
            DataTable dtMaxId = objContact.GetMaxContactId(StrCompId);
            if (dtMaxId.Rows.Count > 0)
            {
                strMaxId = dtMaxId.Rows[0][0].ToString();
                objCG.InsertContactGroup(StrCompId, strMaxId, "2", "True", Session["UserId"].ToString(), DateTime.Now.ToString(), Session["UserId"].ToString(), DateTime.Now.ToString());
                b = objSupplier.InsertSupplierMaster(StrCompId, StrBrandId, strMaxId, txtAccountNo.Text, txtDebitAmount.Text, txtCreditAmount.Text, txtODebitAmount.Text, txtOCreditAmount.Text, txtPurchaseLimit.Text, txtReturnDays.Text, "", "", "", "", "", "True", Session["UserId"].ToString(), DateTime.Now.ToString(), Session["UserId"].ToString(), DateTime.Now.ToString());
            }

            //Add Address Insert Section.
            objAddChild.DeleteAddressChild("Contact", strMaxId);
            foreach (GridViewRow gvr in GvAddressName.Rows)
            {
                Label lblGvAddressName = (Label)gvr.FindControl("lblgvAddressName");

                if (lblGvAddressName.Text != "")
                {
                    DataTable dtAddId = objAddMaster.GetAddressDataByAddressName(StrCompId, lblGvAddressName.Text);
                    if (dtAddId.Rows.Count > 0)
                    {
                        string strAddressId = dtAddId.Rows[0]["Trans_Id"].ToString();
                        objAddChild.InsertAddressChild(strAddressId, "Contact", strMaxId, "True", "", "", "", "", "", "True", DateTime.Now.ToString(), "True", Session["UserId"].ToString(), DateTime.Now.ToString(), Session["UserId"].ToString(), DateTime.Now.ToString());
                    }
                }
            }
            //End  

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
    private string GetCompanyContactId()
    {
        string retval = string.Empty;
        if (txtCname.Text != "")
        {
            retval = (txtCname.Text.Split('/'))[txtCname.Text.Split('/').Length - 1];

            DataTable dtCompany = objCompany.GetCompanyContactByContactCompanyId(StrCompId, retval);
            if (dtCompany.Rows.Count > 0)
            {

            }
            else
            {
                retval = "";
            }
        }
        else
        {
            retval = "";
        }
        return retval;
    }
    protected void IbtnRestore_Command(object sender, CommandEventArgs e)
    {
        hdnSupplierId.Value = e.CommandArgument.ToString();
        int b = 0;
        b = objSupplier.DeleteSupplierMaster(StrCompId, StrBrandId, hdnSupplierId.Value, true.ToString(), StrUserId, DateTime.Now.ToString());
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
    [System.Web.Services.WebMethodAttribute(), System.Web.Script.Services.ScriptMethodAttribute()]
    public static string[] GetCompletionList2(string prefixText, int count, string contextKey)
    {
        Ems_CompanyContactMaster objCompanyCon = new Ems_CompanyContactMaster();
        DataTable dtComp = objCompanyCon.GetDistinctCompanyName(HttpContext.Current.Session["CompId"].ToString(), prefixText);
        string[] txt = new string[dtComp.Rows.Count];

        if (dtComp.Rows.Count > 0)
        {
            for (int i = 0; i < dtComp.Rows.Count; i++)
            {
                txt[i] = dtComp.Rows[i]["Company_Name"].ToString() + "/" + dtComp.Rows[i]["Contact_Company_Id"].ToString() + "";
            }
        }
        else
        {
            if (prefixText.Length > 2)
            {
                txt = null;
            }
            else
            {
                dtComp = objCompanyCon.GetCompanyContactTrueAllData(HttpContext.Current.Session["CompId"].ToString());
                if (dtComp.Rows.Count > 0)
                {
                    txt = new string[dtComp.Rows.Count];
                    for (int i = 0; i < dtComp.Rows.Count; i++)
                    {
                        txt[i] = dtComp.Rows[i]["Company_Name"].ToString() + "/" + dtComp.Rows[i]["Contact_Company_Id"].ToString() + "";
                    }
                }
            }
        }
        return txt;
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
    protected void GvSupplierBin_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        GvSupplierBin.PageIndex = e.NewPageIndex;

        DataTable dt = (DataTable)Session["dtInactive"];
        GvSupplierBin.DataSource = dt;
        GvSupplierBin.DataBind();
        AllPageCode();
        GvSupplierBin.BottomPagerRow.Focus();
     
        string temp = string.Empty;

        for (int i = 0; i < GvSupplierBin.Rows.Count; i++)
        {
            Label lblconid = (Label)GvSupplierBin.Rows[i].FindControl("lblgvSupplierId");
            string[] split = lblSelectedRecord.Text.Split(',');

            for (int j = 0; j < lblSelectedRecord.Text.Split(',').Length; j++)
            {
                if (lblSelectedRecord.Text.Split(',')[j] != "")
                {
                    if (lblconid.Text.Trim().ToString() == lblSelectedRecord.Text.Split(',')[j].Trim().ToString())
                    {
                        ((CheckBox)GvSupplierBin.Rows[i].FindControl("chkSelect")).Checked = true;
                    }
                }
            }
        }
    }
    protected void GvSupplierBin_OnSorting(object sender, GridViewSortEventArgs e)
    {
        HDFSortbin.Value = HDFSortbin.Value == "ASC" ? "DESC" : "ASC";
        DataTable dt = new DataTable();
        dt = objSupplier.GetSupplierAllFalseData(StrCompId, StrBrandId);
        DataView dv = new DataView(dt);
        string Query = "" + e.SortExpression + " " + HDFSortbin.Value + "";
        dv.Sort = Query;
        dt = dv.ToTable();
        Session["dtInactive"] = dt;
        GvSupplierBin.DataSource = dt;
        GvSupplierBin.DataBind();
        AllPageCode();
        lblSelectedRecord.Text = "";
        GvSupplierBin.HeaderRow.Focus();
    }
    public void FillGridBin()
    {
        DataTable dt = new DataTable();
        dt = objSupplier.GetSupplierAllFalseData(StrCompId, StrBrandId);
        GvSupplierBin.DataSource = dt;
        GvSupplierBin.DataBind();
        Session["dtPBrandBin"] = dt;
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


            DataTable dtCust = (DataTable)Session["dtPBrandBin"];
            DataView view = new DataView(dtCust, condition, "", DataViewRowState.CurrentRows);
            Session["dtInactive"] = view.ToTable();
            lblTotalRecordsBin.Text = Resources.Attendance.Total_Records + " : " + view.ToTable().Rows.Count.ToString() + "";
            GvSupplierBin.DataSource = view.ToTable();
            GvSupplierBin.DataBind();
            lblSelectedRecord.Text = "";
            if (view.ToTable().Rows.Count == 0)
            {
                ImgbtnSelectAll.Visible = false;
                imgBtnRestore.Visible = false;
            }
            else
            {
                AllPageCode();
            }
            btnbindBin.Focus();
        }
    }
    protected void btnRestoreSelected_Click(object sender, CommandEventArgs e)
    {
        int b = 0;
        DataTable dt = objSupplier.GetSupplierAllFalseData(StrCompId, StrBrandId);

        if (GvSupplierBin.Rows.Count != 0)
        {
            if (lblSelectedRecord.Text != "")
            {
                for (int j = 0; j < lblSelectedRecord.Text.Split(',').Length; j++)
                {
                    if (lblSelectedRecord.Text.Split(',')[j] != "")
                    {
                        //Msg = objTax.DeleteTaxMaster(lblSelectedRecord.Text.Split(',')[j].Trim().ToString(), true.ToString(), Session["UserId"].ToString(), DateTime.Now.ToString());
                        b = objSupplier.DeleteSupplierMaster(StrCompId, StrBrandId, lblSelectedRecord.Text.Split(',')[j].Trim().ToString(), true.ToString(), Session["UserId"].ToString(), DateTime.Now.ToString());
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
                foreach (GridViewRow Gvr in GvSupplierBin.Rows)
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
      
    }
    protected void chkCurrent_CheckedChanged(object sender, EventArgs e)
    {
        CheckBox chkSelAll = ((CheckBox)GvSupplierBin.HeaderRow.FindControl("chkCurrent"));
        for (int i = 0; i < GvSupplierBin.Rows.Count; i++)
        {
            ((CheckBox)GvSupplierBin.Rows[i].FindControl("chkSelect")).Checked = chkSelAll.Checked;
            if (chkSelAll.Checked)
            {
                if (!lblSelectedRecord.Text.Split(',').Contains(((Label)(GvSupplierBin.Rows[i].FindControl("lblgvSupplierId"))).Text.Trim().ToString()))
                {
                    lblSelectedRecord.Text += ((Label)(GvSupplierBin.Rows[i].FindControl("lblgvSupplierId"))).Text.Trim().ToString() + ",";
                }
            }
            else
            {
                string temp = string.Empty;
                string[] split = lblSelectedRecord.Text.Split(',');
                foreach (string item in split)
                {
                    if (item != ((Label)(GvSupplierBin.Rows[i].FindControl("lblgvSupplierId"))).Text.Trim().ToString())
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
        Label lb = (Label)GvSupplierBin.Rows[index].FindControl("lblgvSupplierId");
        if (((CheckBox)GvSupplierBin.Rows[index].FindControl("chkSelect")).Checked)
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
        ((CheckBox)GvSupplierBin.Rows[index].FindControl("chkSelect")).Focus();
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
                if (!lblSelectedRecord.Text.Split(',').Contains(dr["Supplier_Id"]))
                {
                    lblSelectedRecord.Text += dr["Supplier_Id"] + ",";
                }
            }
            for (int i = 0; i < GvSupplierBin.Rows.Count; i++)
            {
                string[] split = lblSelectedRecord.Text.Split(',');
                Label lblconid = (Label)GvSupplierBin.Rows[i].FindControl("lblgvSupplierId");
                for (int j = 0; j < lblSelectedRecord.Text.Split(',').Length; j++)
                {
                    if (lblSelectedRecord.Text.Split(',')[j] != "")
                    {
                        if (lblconid.Text.Trim().ToString() == lblSelectedRecord.Text.Split(',')[j].Trim().ToString())
                        {
                            ((CheckBox)GvSupplierBin.Rows[i].FindControl("chkSelect")).Checked = true;
                        }
                    }
                }
            }
        }
        else
        {
            lblSelectedRecord.Text = "";
            DataTable dtUnit1 = (DataTable)Session["dtInactive"];
            GvSupplierBin.DataSource = dtUnit1;
            GvSupplierBin.DataBind();
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
                    b = objSupplier.DeleteSupplierMaster(StrCompId, StrBrandId, lblSelectedRecord.Text.Split(',')[j].Trim(), true.ToString(), Session["UserId"].ToString(), DateTime.Now.ToString());
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
            foreach (GridViewRow Gvr in GvSupplierBin.Rows)
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
        imgBtnRestore.Focus();
    }
    #endregion

    #endregion

    #region User defind Funcation
    private void FillGrid()
    {
        DataTable dtBrand = objSupplier.GetSupplierAllTrueData(StrCompId, StrBrandId);
        lblTotalRecords.Text = Resources.Attendance.Total_Records + " : " + dtBrand.Rows.Count + "";
        Session["dtCurrency"] = dtBrand;
        Session["dtFilter"] = dtBrand;
        if (dtBrand != null && dtBrand.Rows.Count > 0)
        {
            GvSupplier.DataSource = dtBrand;
            GvSupplier.DataBind();
        }
        else
        {
            GvSupplier.DataSource = null;
            GvSupplier.DataBind();
        }
        lblTotalRecords.Text = Resources.Attendance.Total_Records + " : " + dtBrand.Rows.Count.ToString() + "";
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
        txtSupplierName.Text = "";
        txtLSupplierName.Text = "";
        txtNickName.Text = "";
        txtCivilId.Text = "";
        txtCname.Text = "";
        txtAddressName.Text = "";
        txtAccountNo.Text = "";
        txtDebitAmount.Text = "";
        txtCreditAmount.Text = "";
        txtODebitAmount.Text = "";
        txtOCreditAmount.Text = "";
        txtPurchaseLimit.Text = "";
        txtReturnDays.Text = "";

        GvAddressName.DataSource = null;
        GvAddressName.DataBind();

        hdnSupplierId.Value = "";
        btnNew.Text = Resources.Attendance.New;
        ddlFieldName.SelectedIndex = 1;
        ddlOption.SelectedIndex = 2;
        txtValue.Text = "";
        txtValueBin.Text = "";
        lblSelectedRecord.Text = "";
        System.Web.UI.ScriptManager.GetCurrent(this).SetFocus(txtValue);
    }
    #endregion

    #region Add New Company
    private void FillParentCompany()
    {
        DataTable dsPCompany = null;
        dsPCompany = objCompany.GetCompanyContactTrueAllData(StrCompId);
        if (dsPCompany.Rows.Count > 0)
        {
            ddlParentCompany.DataSource = dsPCompany;
            ddlParentCompany.DataTextField = "Company_Name";
            ddlParentCompany.DataValueField = "Contact_Company_Id";
            ddlParentCompany.DataBind();

            ddlParentCompany.Items.Add("--Select--");
            ddlParentCompany.SelectedValue = "--Select--";
        }
        else
        {
            ddlParentCompany.Items.Add("--Select--");
            ddlParentCompany.SelectedValue = "--Select--";
        }
    }
    private void FillCurrency()
    {
        DataTable dsCurrency = null;
        dsCurrency = objCurr.GetCurrencyMaster();
        if (dsCurrency.Rows.Count > 0)
        {
            ddlCurrency.DataSource = dsCurrency;
            ddlCurrency.DataTextField = "Currency_Name";
            ddlCurrency.DataValueField = "Currency_ID";
            ddlCurrency.DataBind();

            ddlCurrency.Items.Add("--Select--");
            ddlCurrency.SelectedValue = "--Select--";
        }
        else
        {
            ddlCurrency.Items.Add("--Select--");
            ddlCurrency.SelectedValue = "--Select--";
        }
    }
    public string GetParentCompanyName(string strCompanyId)
    {
        string strParentCompany = string.Empty;
        DataTable dtPCompany = objCompany.GetCompanyContactByContactCompanyId(StrCompId, strCompanyId);
        if (dtPCompany.Rows.Count > 0)
        {
            strParentCompany = dtPCompany.Rows[0]["ECompanyName"].ToString();
        }
        return strParentCompany;
    }
    public string GetCurrencyName(string strCurrencyId)
    {
        string strCurrencyName = string.Empty;
        DataTable dtCurrencyName = objCurr.GetCurrencyMaster();
        if (dtCurrencyName.Rows.Count > 0)
        {
            dtCurrencyName = new DataView(dtCurrencyName, "CurrencyID='" + strCurrencyId + "'", "", DataViewRowState.CurrentRows).ToTable();
            if (dtCurrencyName.Rows.Count > 0)
            {
                strCurrencyName = dtCurrencyName.Rows[0]["ECurrencyName"].ToString();
            }
        }
        return strCurrencyName;
    }
    protected void btnCompanySave_Click(object sender, EventArgs e)
    {
        string strParentCompany = string.Empty;
        int b = 0;
        if (txtCompanyName.Text != "")
        {
            DataTable dtc = objCompany.GetCompanyContactAllData(StrCompId);
            dtc = new DataView(dtc, "Company_Name='" + txtCompanyName.Text + "'", "", DataViewRowState.CurrentRows).ToTable();
            if (dtc.Rows.Count > 0)
            {
                DisplayMessage("Company Name Already Exists");
                txtCompanyName.Text = "";
                txtCompanyName.Focus();
                return;
            }

            if (ddlCurrency.SelectedValue == "--Select--")
            {
                DisplayMessage("Select Currency Name");
                System.Web.UI.ScriptManager.GetCurrent(this).SetFocus(ddlCurrency);
                return;
            }

            if (ddlParentCompany.SelectedValue == "--Select--")
            {
                strParentCompany = "0";
            }
            else if (ddlParentCompany.SelectedValue != "--Select--")
            {
                strParentCompany = ddlParentCompany.SelectedValue;
            }

            b = objCompany.InsertCompanyContactMaster(StrCompId, strParentCompany, txtCompanyName.Text, txtCompanyNameL.Text, ddlCurrency.SelectedValue, "", "", "", "", "", "True", DateTime.Now.ToString(), "True", Session["UserId"].ToString(), DateTime.Now.ToString(), Session["UserId"].ToString(), DateTime.Now.ToString());


            if (b != 0)
            {
                string strCompanyMaxId = string.Empty;
                DataTable dtMaxId = objCompany.GetCompanyContactMaxId(StrCompId);
                if (dtMaxId.Rows.Count > 0)
                {
                    strCompanyMaxId = dtMaxId.Rows[0][0].ToString();
                    foreach (GridViewRow gvr in GvCompanyAddressName.Rows)
                    {
                        Label lblGvAddressName = (Label)gvr.FindControl("lblgvCAddressName");

                        if (lblGvAddressName.Text != "")
                        {
                            DataTable dtAddId = objAddMaster.GetAddressDataByAddressName(StrCompId, lblGvAddressName.Text);
                            if (dtAddId.Rows.Count > 0)
                            {
                                string strAddressId = dtAddId.Rows[0]["Trans_Id"].ToString();
                                objAddChild.InsertAddressChild(strAddressId, "CompanyContact", strCompanyMaxId, "True", "", "", "", "", "", "True", DateTime.Now.ToString(), "True", Session["UserId"].ToString(), DateTime.Now.ToString(), Session["UserId"].ToString(), DateTime.Now.ToString());
                            }
                        }
                    }
                }
                txtCname.Text = txtCompanyName.Text + "/" + strCompanyMaxId;
                DisplayMessage("Record Saved");
                ClearAddCompany();
                btnCompanySave.Visible = false;
                pnlCompany1.Visible = false;
                pnlCompany2.Visible = false;

                txtCname.Focus();
            }
            else
            {
                DisplayMessage("Record Not Saved");
                ClearAddCompany();
            }
        }
        else
        {
            DisplayMessage("Fill Company Name");
            System.Web.UI.ScriptManager.GetCurrent(this).SetFocus(txtCompanyName);
        }
    }
    protected void ImgClose_Click(object sender, ImageClickEventArgs e)
    {
        pnlCompany1.Visible = false;
        pnlCompany2.Visible = false;
    }
    protected void ImgAddCompany_Click(object sender, EventArgs e)
    {
        pnlCompany1.Visible = true;
        pnlCompany2.Visible = true;
        AllPageCode();
        txtCompanyName.Focus();
        txtCompanyName.Text = "";
        txtCompanyNameL.Text = "";
        FillParentCompany();
        FillCurrency();
        GvCompanyAddressName.DataSource = null;
        GvCompanyAddressName.DataBind();
        ListItem lst = new ListItem("--Select One--", "0");
    }
    public void ClearAddCompany()
    {
        txtCompanyName.Text = "";
        txtCompanyNameL.Text = "";
        FillParentCompany();
        FillCurrency();
        txtCompanyAddress.Text = "";
        GvCompanyAddressName.DataSource = null;
        GvCompanyAddressName.DataBind();
    }
    #endregion

    #region Add AddressName Concept
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
    protected void txtAddressName_TextChanged(object sender, EventArgs e)
    {
        if (txtAddressName.Text != "")
        {
            DataTable dtAM = objAddMaster.GetAddressDataByAddressName(StrCompId, txtAddressName.Text);
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
            AllPageCode();
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
        AllPageCode();
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
        DataTable dt = objAddMaster.GetAddressDataByAddressName(StrCompId, txtAddressNameNew.Text);
        if (dt.Rows.Count > 0)
        {
            txtAddressNameNew.Text = "";
            DisplayMessage("Address Name Already Exists");
            System.Web.UI.ScriptManager.GetCurrent(this).SetFocus(txtAddressNameNew);
            return;
        }

        DataTable dt1 = objAddMaster.GetAddressFalseAllData(StrCompId);
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
            float flTemp = 0;
            if (float.TryParse(txtLongitude.Text, out flTemp))
            {
                strLongitude = txtLongitude.Text;
            }
            else
            {
                txtLongitude.Text = "";
                ScriptManager.RegisterStartupScript(this, typeof(string), "OPEN_WINDOW", "window.alert('Enter Numeric Value Only');", true);
                System.Web.UI.ScriptManager.GetCurrent(this).SetFocus(txtLongitude);
                return;
            }
        }
        else
        {
            strLongitude = "0";
        }
        string strLatitude = string.Empty;
        if (txtLatitude.Text != "")
        {
            float flTemp = 0;
            if (float.TryParse(txtLatitude.Text, out flTemp))
            {
                strLatitude = txtLatitude.Text;
            }
            else
            {
                txtLatitude.Text = "";
                ScriptManager.RegisterStartupScript(this, typeof(string), "OPEN_WINDOW", "window.alert('Enter Numeric Value Only');", true);
                System.Web.UI.ScriptManager.GetCurrent(this).SetFocus(txtLatitude);
                return;
            }
        }
        else
        {
            strLatitude = "0";
        }

        int b = 0;

        DataTable dtadd2 = objAddMaster.GetAddressDataByAddressName(StrCompId, txtAddressName.Text);
        if (dtadd2.Rows.Count > 0)
        {
            txtAddressName.Text = "";
            DisplayMessage("Address Name Already Exists");
            System.Web.UI.ScriptManager.GetCurrent(this).SetFocus(txtAddressName);
            return;
        }
        else
        {
            b = objAddMaster.InsertAddressMaster(StrCompId, ddlAddressCategory.SelectedValue, txtAddressNameNew.Text, txtAddress.Text, txtStreet.Text, txtBlock.Text, txtAvenue.Text, strCountryId, txtState.Text, txtCity.Text, txtPinCode.Text, txtPhoneNo1.Text, txtPhoneNo2.Text, txtMobileNo1.Text, txtMobileNo2.Text, txtEmailId1.Text, txtEmailId2.Text, txtFaxNo.Text, txtWebsite.Text, strLongitude, strLatitude, "", "", "", "", "", "True", DateTime.Now.ToString(), "True", Session["UserId"].ToString(), DateTime.Now.ToString(), Session["UserId"].ToString(), DateTime.Now.ToString());
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
        dsAddressCat = ObjAddressCat.GetAddressCategoryAll(StrCompId);
        if (dsAddressCat.Rows.Count > 0)
        {
            ddlAddressCategory.DataSource = dsAddressCat;
            ddlAddressCategory.DataTextField = "Address_Name";
            ddlAddressCategory.DataValueField = "Address_Category_Id";
            ddlAddressCategory.DataBind();

            ddlAddressCategory.Items.Add("--Select--");
            ddlAddressCategory.SelectedValue = "--Select--";
        }
        else
        {
            ddlAddressCategory.Items.Add("--Select--");
            ddlAddressCategory.SelectedValue = "--Select--";
        }
    }
    private void FillCountry()
    {
        DataTable dsCountry = null;
        dsCountry = ObjCountry.GetCountryMaster();
        if (dsCountry.Rows.Count > 0)
        {
            ddlCountry.DataSource = dsCountry;
            ddlCountry.DataTextField = "Country_Name";
            ddlCountry.DataValueField = "Country_Id";
            ddlCountry.DataBind();

            ddlCountry.Items.Add("--Select--");
            ddlCountry.SelectedValue = "--Select--";
        }
        else
        {
            ddlCountry.Items.Add("--Select--");
            ddlCountry.SelectedValue = "--Select--";
        }
    }
    protected void btnClosePanel_Click(object sender, EventArgs e)
    {
        pnlAddress1.Visible = false;
        pnlAddress2.Visible = false;
    }
    #endregion

    #region Add Company AddressName Concept
    protected void txtCompanyAddress_TextChanged(object sender, EventArgs e)
    {
        if (txtCompanyAddress.Text != "")
        {
            DataTable dtAM = objAddMaster.GetAddressDataByAddressName(StrCompId, txtCompanyAddress.Text);
            if (dtAM.Rows.Count > 0)
            {
                System.Web.UI.ScriptManager.GetCurrent(this).SetFocus(imgAddCompanyAddressName);
            }
            else
            {
                txtCompanyAddress.Text = "";
                DisplayMessage("Choose In Suggestions Only");
                System.Web.UI.ScriptManager.GetCurrent(this).SetFocus(txtCompanyAddress);
                return;
            }
        }
        else
        {
            DisplayMessage("Enter Address Name");
            System.Web.UI.ScriptManager.GetCurrent(this).SetFocus(txtCompanyAddress);
        }
    }
    protected void imgAddCompanyAddressName_Click(object sender, ImageClickEventArgs e)
    {
        if (txtCompanyAddress.Text != "")
        {
            string strA = "0";
            foreach (GridViewRow gve in GvCompanyAddressName.Rows)
            {
                Label lblCAddressName = (Label)gve.FindControl("lblgvCAddressName");
                if (txtCompanyAddress.Text == lblCAddressName.Text)
                {
                    strA = "1";
                }
            }


            if (hdnCompanyAddressId.Value == "")
            {
                if (strA == "0")
                {
                    FillCompanyAddressChidGird("Save");
                    System.Web.UI.ScriptManager.GetCurrent(this).SetFocus(txtCompanyAddress);
                }
                else
                {
                    txtCompanyAddress.Text = "";
                    DisplayMessage("Address Name Already Exists");
                    System.Web.UI.ScriptManager.GetCurrent(this).SetFocus(txtCompanyAddress);
                }
            }
            else
            {
                if (txtCompanyAddress.Text == hdnCompanyAddressName.Value)
                {
                    FillCompanyAddressChidGird("Edit");
                    System.Web.UI.ScriptManager.GetCurrent(this).SetFocus(txtCompanyAddress);
                }
                else
                {
                    if (strA == "0")
                    {
                        FillCompanyAddressChidGird("Edit");
                        System.Web.UI.ScriptManager.GetCurrent(this).SetFocus(txtCompanyAddress);
                    }
                    else
                    {
                        txtCompanyAddress.Text = "";
                        DisplayMessage("Address Name Already Exists");
                        System.Web.UI.ScriptManager.GetCurrent(this).SetFocus(txtCompanyAddress);
                    }
                }
            }
        }
        else
        {
            DisplayMessage("Enter Address Name");
            System.Web.UI.ScriptManager.GetCurrent(this).SetFocus(txtCompanyAddress);
        }
        txtCompanyAddress.Focus();
    }
    public void ResetCompanyAddressName()
    {
        txtCompanyAddress.Text = "";
        hdnCompanyAddressId.Value = "";
        hdnCompanyAddressName.Value = "";
    }
    public DataTable CreateCompanyAddressDataTable()
    {
        DataTable dt = new DataTable();
        dt.Columns.Add("Trans_Id");
        dt.Columns.Add("Address_Name");
        return dt;
    }
    public DataTable FillCompanyAddressDataTabel()
    {
        string strNewSNo = string.Empty;
        DataTable dt = CreateCompanyAddressDataTable();
        if (GvCompanyAddressName.Rows.Count > 0)
        {
            for (int i = 0; i < GvCompanyAddressName.Rows.Count + 1; i++)
            {
                if (dt.Rows.Count != GvCompanyAddressName.Rows.Count)
                {
                    dt.Rows.Add(i);
                    Label lblSNo = (Label)GvCompanyAddressName.Rows[i].FindControl("lblCSNo");
                    Label lblAddressName = (Label)GvCompanyAddressName.Rows[i].FindControl("lblgvCAddressName");

                    dt.Rows[i]["Trans_Id"] = lblSNo.Text;
                    strNewSNo = lblSNo.Text;
                    dt.Rows[i]["Address_Name"] = lblAddressName.Text;
                }
                else
                {
                    dt.Rows.Add(i);
                    dt.Rows[i]["Trans_Id"] = (float.Parse(strNewSNo) + 1).ToString();
                    dt.Rows[i]["Address_Name"] = txtCompanyAddress.Text;
                }
            }
        }
        else
        {
            dt.Rows.Add(0);
            dt.Rows[0]["Trans_Id"] = "1";
            dt.Rows[0]["Address_Name"] = txtCompanyAddress.Text;
        }
        if (dt.Rows.Count > 0)
        {
            GvCompanyAddressName.DataSource = dt;
            GvCompanyAddressName.DataBind();
            AllPageCode();
        }
        return dt;
    }
    public DataTable FillCompanyAddressDataTabelDelete()
    {
        DataTable dt = CreateCompanyAddressDataTable();
        for (int i = 0; i < GvCompanyAddressName.Rows.Count; i++)
        {
            dt.Rows.Add(i);
            Label lblCSNo = (Label)GvCompanyAddressName.Rows[i].FindControl("lblCSNo");
            Label lblgvCAddressName = (Label)GvCompanyAddressName.Rows[i].FindControl("lblgvCAddressName");


            dt.Rows[i]["Trans_Id"] = lblCSNo.Text;
            dt.Rows[i]["Address_Name"] = lblgvCAddressName.Text;
        }

        DataView dv = new DataView(dt);
        dv.RowFilter = "Trans_Id<>'" + hdnCompanyAddressId.Value + "'";
        dt = (DataTable)dv.ToTable();
        return dt;
    }
    protected void imgBtnCompanyAddressEdit_Command(object sender, CommandEventArgs e)
    {
        hdnCompanyAddressId.Value = e.CommandArgument.ToString();
        FillCompanyAddressDataTabelEdit();
    }
    public DataTable FillCompanyAddressDataTabelEdit()
    {
        DataTable dt = CreateCompanyAddressDataTable();

        for (int i = 0; i < GvCompanyAddressName.Rows.Count; i++)
        {
            dt.Rows.Add(i);
            Label lblCSNo = (Label)GvCompanyAddressName.Rows[i].FindControl("lblCSNo");
            Label lblgvCAddressName = (Label)GvCompanyAddressName.Rows[i].FindControl("lblgvCAddressName");

            dt.Rows[i]["Trans_Id"] = lblCSNo.Text;
            dt.Rows[i]["Address_Name"] = lblgvCAddressName.Text;
        }

        DataView dv = new DataView(dt);
        dv.RowFilter = "Trans_Id='" + hdnCompanyAddressId.Value + "'";
        dt = (DataTable)dv.ToTable();
        if (dt.Rows.Count != 0)
        {
            txtCompanyAddress.Text = dt.Rows[0]["Address_Name"].ToString();
            hdnCompanyAddressName.Value = dt.Rows[0]["Address_Name"].ToString();
        }
        return dt;
    }
    protected void imgBtnCompanyAddressDelete_Command(object sender, CommandEventArgs e)
    {
        hdnCompanyAddressId.Value = e.CommandArgument.ToString();
        FillCompanyAddressChidGird("Del");
    }
    public void FillCompanyAddressChidGird(string CommandName)
    {
        DataTable dt = new DataTable();
        if (CommandName.ToString() == "Del")
        {
            dt = FillCompanyAddressDataTabelDelete();
        }
        else if (CommandName.ToString() == "Edit")
        {
            dt = FillCompanyAddressDataTableUpdate();
        }
        else
        {
            dt = FillCompanyAddressDataTabel();
        }
        GvCompanyAddressName.DataSource = dt;
        GvCompanyAddressName.DataBind();
        AllPageCode();

        ResetCompanyAddressName();
    }
    public DataTable FillCompanyAddressDataTableUpdate()
    {
        DataTable dt = CreateCompanyAddressDataTable();
        for (int i = 0; i < GvCompanyAddressName.Rows.Count; i++)
        {
            dt.Rows.Add(i);
            Label lblCSNo = (Label)GvCompanyAddressName.Rows[i].FindControl("lblCSNo");
            Label lblgvCAddressName = (Label)GvCompanyAddressName.Rows[i].FindControl("lblgvCAddressName");

            dt.Rows[i]["Trans_Id"] = lblCSNo.Text;
            dt.Rows[i]["Address_Name"] = lblgvCAddressName.Text;
        }
        for (int i = 0; i < dt.Rows.Count; i++)
        {
            if (hdnCompanyAddressId.Value == dt.Rows[i]["Trans_Id"].ToString())
            {
                dt.Rows[i]["Address_Name"] = txtCompanyAddress.Text;
            }
        }
        return dt;
    }
    #endregion
}
