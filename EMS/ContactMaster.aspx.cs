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
using System.Collections.Generic;
using System.Text;
using System.Text.RegularExpressions;

public partial class EmailMarkettingSystem_ContactMaster : BasePage
{
    CompanyMaster objComp = new CompanyMaster();
    Ems_CompanyContactMaster objCompany = new Ems_CompanyContactMaster();
    Ems_ContactMaster objContact = new Ems_ContactMaster();
    Ems_GroupMaster objGroup = new Ems_GroupMaster();
    Ems_Contact_Group objCG = new Ems_Contact_Group();
    Common com = new Common();
    LocationMaster ObjLoc = new LocationMaster();
    Common cmn = new Common();
    Set_AddressCategory ObjAddCat = new Set_AddressCategory();
    Set_AddressMaster objAddMaster = new Set_AddressMaster();
    Set_AddressChild objAddChild = new Set_AddressChild();
    CountryMaster objCountry = new CountryMaster();
    CurrencyMaster objCurr = new CurrencyMaster();
    DepartmentMaster DM = new DepartmentMaster();
    DesignationMaster DesigM = new DesignationMaster();
    ReligionMaster objRM = new ReligionMaster();
    BrandMaster objBrandMaster = new BrandMaster();
    Set_CustomerMaster objCustomer = new Set_CustomerMaster();
    Set_Suppliers objSupplier = new Set_Suppliers();
    SystemParameter ObjSysParam = new SystemParameter();
    string StrCompId = string.Empty;

    protected void Page_Load(object sender, EventArgs e)
    {

        if (Session["UserId"] == null)
        {
            Response.Redirect("~/ERPLogin.aspx");
        }
        if (!IsPostBack)
        {
            StrCompId = Session["CompId"].ToString();
            btnList_Click(null, null);
            FillDepartment();
            FillDesignation();
            FillReligion();
            FillGrid();
            fillgridbin();
        }
        else
        {
            try
            {
                if (navTree.SelectedNode.Checked == true)
                {
                    UnSelectChild(navTree.SelectedNode);
                }
                else
                {
                    SelectChild(navTree.SelectedNode);
                }
            }
            catch (Exception)
            {

            }
        }

        txtContactName.ReadOnly = false;
        AllPageCode();
    }


    #region AllPageCode
    public void AllPageCode()
    {
        StrCompId = Session["CompId"].ToString();

        Page.Title = ObjSysParam.GetSysTitle();
        Session["AccordianId"] = "8";
        Session["HeaderText"] = "Master Setup";
        Common cmn = new Common();
        DataTable dtAllPageCode = cmn.GetAllPagePermission(Session["UserId"].ToString(), "8", "19");
        if (dtAllPageCode.Rows.Count != 0)
        {
            if (dtAllPageCode.Rows[0][0].ToString() == "SuperAdmin")
            {
                btnCompanySave.Visible = true;
                btnsave.Visible = true;
                btnAddressSave.Visible = true;
                btnGSave.Visible = true;
                foreach (GridViewRow Row in GvAddressName.Rows)
                {
                    ((ImageButton)Row.FindControl("imgBtnAddressEdit")).Visible = true;
                    ((ImageButton)Row.FindControl("imgBtnDelete")).Visible = true;
                }

                foreach (GridViewRow Row in GvCompanyAddressName.Rows)
                {
                    ((ImageButton)Row.FindControl("imgBtnCompanyAddressEdit")).Visible = true;
                    ((ImageButton)Row.FindControl("imgBtnCompanyAddressDelete")).Visible = true;
                }
                foreach (GridViewRow Row in GridContact.Rows)
                {
                    ((ImageButton)Row.FindControl("imgBtnEdit")).Visible = true;
                    ((ImageButton)Row.FindControl("imgBtnDelete")).Visible = true;
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
                        btnsave.Visible = true;
                        btnAddressSave.Visible = true;
                        btnGSave.Visible = true;
                    }
                    foreach (GridViewRow Row in GvAddressName.Rows)
                    {
                        if (Convert.ToBoolean(DtRow["Op_Edit"].ToString()))
                        {
                            ((ImageButton)Row.FindControl("imgBtnAddressEdit")).Visible = true;
                        }
                        if (Convert.ToBoolean(DtRow["Op_Delete"].ToString()))
                        {
                            ((ImageButton)Row.FindControl("imgBtnDelete")).Visible = true;
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
                    foreach (GridViewRow Row in GridContact.Rows)
                    {
                        if (Convert.ToBoolean(DtRow["Op_Edit"].ToString()))
                        {
                            ((ImageButton)Row.FindControl("imgBtnEdit")).Visible = true;
                        }
                        if (Convert.ToBoolean(DtRow["Op_Delete"].ToString()))
                        {
                            ((ImageButton)Row.FindControl("imgBtnDelete")).Visible = true;
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
    public void FillGrid()
    {
        DataTable dtContact = objContact.GetContactTrueAllData(StrCompId);
        if (dtContact.Rows.Count > 0)
        {
            GridContact.DataSource = dtContact;
            GridContact.DataBind();
            lblTotalRecords.Text = Resources.Attendance.Total_Records + " : " + dtContact.Rows.Count.ToString();
        }
        else
        {
            GridContact.DataSource = null;
            GridContact.DataBind();
            lblTotalRecords.Text = Resources.Attendance.Total_Records + " : " + dtContact.Rows.Count.ToString();
        }
        Session["dtContact"] = dtContact;
        Session["dtFilter"] = dtContact;
        AllPageCode();
    }
    public void FillBrandCustomer()
    {
        DataTable dtBrandCustomer = objBrandMaster.GetBrandMaster(StrCompId);
        if (dtBrandCustomer.Rows.Count > 0)
        {
            GvCustomerBrand.DataSource = dtBrandCustomer;
            GvCustomerBrand.DataBind();
        }
        else
        {
            GvCustomerBrand.DataSource = null;
            GvCustomerBrand.DataBind();
        }
        Session["BrandCustomer"] = dtBrandCustomer;
    }
    public void FillBrandSupplier()
    {
        DataTable dtBrandSupplier = objBrandMaster.GetBrandMaster(StrCompId);
        if (dtBrandSupplier.Rows.Count > 0)
        {
            GvSupplierBrand.DataSource = dtBrandSupplier;
            GvSupplierBrand.DataBind();
        }
        else
        {
            GvSupplierBrand.DataSource = null;
            GvSupplierBrand.DataBind();
        }
        Session["BrandSupplier"] = dtBrandSupplier;
    }
    private void FillDepartment()
    {
        DataTable dsDepartment = null;
        dsDepartment = DM.GetDepartmentMaster(StrCompId);
        if (dsDepartment.Rows.Count > 0)
        {
            ddlDepartment.DataSource = dsDepartment;
            ddlDepartment.DataTextField = "Dep_Name";
            ddlDepartment.DataValueField = "Dep_Id";
            ddlDepartment.DataBind();

            ddlDepartment.Items.Add("--Select--");
            ddlDepartment.SelectedValue = "--Select--";
        }
        else
        {
            ddlDepartment.Items.Add("--Select--");
            ddlDepartment.SelectedValue = "--Select--";
        }
    }
    private void FillDesignation()
    {
        DataTable dsDesignation = null;
        dsDesignation = DesigM.GetDesignationMaster(StrCompId);
        if (dsDesignation.Rows.Count > 0)
        {
            ddlDesignation.DataSource = dsDesignation;
            ddlDesignation.DataTextField = "Designation";
            ddlDesignation.DataValueField = "Designation_Id";
            ddlDesignation.DataBind();

            ddlDesignation.Items.Add("--Select--");
            ddlDesignation.SelectedValue = "--Select--";
        }
        else
        {
            ddlDesignation.Items.Add("--Select--");
            ddlDesignation.SelectedValue = "--Select--";
        }
    }
    private void FillReligion()
    {
        DataTable dsReligion = null;
        dsReligion = objRM.GetReligionMaster(StrCompId);
        if (dsReligion.Rows.Count > 0)
        {
            ddlReligion.DataSource = dsReligion;
            ddlReligion.DataTextField = "Religion";
            ddlReligion.DataValueField = "Religion_Id";
            ddlReligion.DataBind();

            ddlReligion.Items.Add("--Select--");
            ddlReligion.SelectedValue = "--Select--";
        }
        else
        {
            ddlReligion.Items.Add("--Select--");
            ddlReligion.SelectedValue = "--Select--";
        }
    }
    protected void btnsave_Click(object sender, EventArgs e)
    {
        int b = 0;
        string strIsEmail = string.Empty;
        string strIsSMS = string.Empty;
        string strDepartmentId = string.Empty;
        string strDesignationId = string.Empty;
        string strReligionId = string.Empty;

        if (ddlDepartment.SelectedValue == "--Select--")
        {
            strDepartmentId = "0";
        }
        else
        {
            strDepartmentId = ddlDepartment.SelectedValue;
        }
        if (ddlDesignation.SelectedValue == "--Select--")
        {
            strDesignationId = "0";
        }
        else
        {
            strDesignationId = ddlDesignation.SelectedValue;
        }
        if (ddlReligion.SelectedValue == "--Select--")
        {
            strReligionId = "0";
        }
        else
        {
            strReligionId = ddlReligion.SelectedValue;
        }

        if (chkIsEmail.Checked == true)
        {
            strIsEmail = "True";
        }
        else if (chkIsEmail.Checked == false)
        {
            strIsEmail = "False";
        }
        if (chkIsSMS.Checked == true)
        {
            strIsSMS = "True";
        }
        else if (chkIsSMS.Checked == false)
        {
            strIsSMS = "False";
        }

        if (txtContactName.Text == "")
        {
            DisplayMessage("Fill Contact Name");
            txtContactName.Focus();
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


        if (hdnContactId.Value != "")
        {
            b = objContact.UpdateContactMaster(StrCompId, hdnContactId.Value, txtContactCode.Text, txtContactName.Text, txtLContactName.Text, txtCivilId.Text, txtNickName.Text, strDepartmentId, strDesignationId, strReligionId, GetCompanyContactId(), strIsEmail, strIsSMS, "", "", "", "", "", "True", DateTime.Now.ToString(), "True", Session["UserId"].ToString(), DateTime.Now.ToString());

            if (b != 0)
            {
                //Add Address Insert Section.
                objAddChild.DeleteAddressChild("Contact", hdnContactId.Value);
                foreach (GridViewRow gvr in GvAddressName.Rows)
                {
                    Label lblGvAddressName = (Label)gvr.FindControl("lblgvAddressName");

                    if (lblGvAddressName.Text != "")
                    {
                        DataTable dtAddId = objAddMaster.GetAddressDataByAddressName(StrCompId, lblGvAddressName.Text);
                        if (dtAddId.Rows.Count > 0)
                        {
                            string strAddressId = dtAddId.Rows[0]["Trans_Id"].ToString();
                            objAddChild.InsertAddressChild(strAddressId, "Contact", hdnContactId.Value, "True", "", "", "", "", "", "True", DateTime.Now.ToString(), "True", Session["UserId"].ToString(), DateTime.Now.ToString(), Session["UserId"].ToString(), DateTime.Now.ToString());
                        }
                    }
                }
                //End  

                DisplayMessage("Record Updated");
                FillGrid();
                PnlGroup.Visible = true;
                panNewEdit.Visible = false;
                txtConId.Text = hdnContactId.Value;

                txtContactName.ReadOnly = true;
                txtgroupContactName.Text = txtContactName.Text;

                BindTree();
                FillBrandCustomer();
                FillBrandSupplier();
                //ResetField();
            }
            else
            {
                DisplayMessage("Record Not Updated");
                FillGrid();
            }
        }
        else
        {
            if (txtContactName.Text == "")
            {
                DisplayMessage("Please Enter Contact Name");
                txtContactName.Focus();
                return;
            }

            string cid = string.Empty;

            b = objContact.InsertContactMaster(StrCompId, txtContactCode.Text, txtContactName.Text, txtLContactName.Text, txtCivilId.Text, txtNickName.Text, strDepartmentId, strDesignationId, strReligionId, GetCompanyContactId(), strIsEmail, strIsSMS, "", "", "", "", "", "True", DateTime.Now.ToString(), "True", Session["UserId"].ToString(), DateTime.Now.ToString(), Session["UserId"].ToString(), DateTime.Now.ToString());
            if (b != 0)
            {
                //Add Address Insert Section.
                string strMaxId = string.Empty;
                DataTable dtMaxId = objContact.GetMaxContactId(StrCompId);
                if (dtMaxId.Rows.Count > 0)
                {
                    strMaxId = dtMaxId.Rows[0][0].ToString();

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
                }
                else
                {

                }
                //End  

                DisplayMessage("Record Saved");
                FillGrid();
                hdnContactId.Value = strMaxId;
                BindTree();
                FillBrandCustomer();
                FillBrandSupplier();
                txtConId.Text = strMaxId;

                txtContactName.ReadOnly = true;
                txtgroupContactName.Text = txtContactName.Text;

                PnlGroup.Visible = true;
                panNewEdit.Visible = false;
            }
            else
            {
                DisplayMessage("Record Not Saved");
                FillGrid();
                ResetField();
            }
            Session["Tempdt"] = null;
        }
    }
    protected void btnReset_Click(object sender, EventArgs e)
    {
        ResetField();
    }
    protected void btnCancel_Click(object sender, EventArgs e)
    {
        ResetField();
        Session["Tempdt"] = null;
        btnList_Click(null, null);
    }
    protected void btnRefresh_Click(object sender, ImageClickEventArgs e)
    {
        FillGrid();
        txtValue.Text = "";
        ddlOption.SelectedIndex = 2;
        ddlFieldName.SelectedIndex = 1;
        txtValue.Focus();
        txtValue.Focus();
    }
    protected void btnbind_Click(object sender, ImageClickEventArgs e)
    {
        if (ddlOption.SelectedIndex != 0)
        {
            string condition = string.Empty;
            if (ddlOption.SelectedIndex == 1)
            {
                condition = ddlFieldName.SelectedValue + "='" + txtValue.Text.Trim() + "'";
            }
            else if (ddlOption.SelectedIndex == 2)
            {
                condition = ddlFieldName.SelectedValue + " Like '%" + txtValue.Text.Trim() + "%'";
            }
            else
            {
                condition = ddlFieldName.SelectedValue + " like '" + txtValue.Text.Trim() + "%'";
            }
            DataTable dtCompany = (DataTable)Session["dtContact"];
            DataView view = new DataView(dtCompany, condition, "", DataViewRowState.CurrentRows);
            GridContact.DataSource = view.ToTable();
            GridContact.DataBind();
            lblTotalRecords.Text = Resources.Attendance.Total_Records + " : " + view.ToTable().Rows.Count + "";
            Session["dtFilter"] = view.ToTable();
        }
        AllPageCode();
        btnbind.Focus();
    }
    protected void BtnEdit(object sender, CommandEventArgs e)
    {
        string strDepartmentId = string.Empty;
        string strDesignationId = string.Empty;
        string strReligionId = string.Empty;

        ResetField();
        btnNew.Text = Resources.Attendance.Edit;
        btnNew_Click(null, null);

        string ConId = e.CommandArgument.ToString();
        DataTable dtCon = objContact.GetContactByContactId(StrCompId, ConId);
        hdnContactId.Value = ConId;
        hdnCompId.Value = dtCon.Rows[0]["Contact_Company_Id"].ToString();

        DataTable dtcomp = objCompany.GetCompanyContactByContactCompanyId(StrCompId, dtCon.Rows[0]["Contact_Company_Id"].ToString());

        string CompName = string.Empty;
        try
        {
            CompName = dtcomp.Rows[0]["Company_Name"].ToString() + "/" + hdnCompId.Value;
        }
        catch
        {

        }

        txtCname.Text = CompName;
        txtContactCode.Text = dtCon.Rows[0]["Contact_Code"].ToString();
        txtContactName.Text = dtCon.Rows[0]["Contact_Name"].ToString();
        txtLContactName.Text = dtCon.Rows[0]["Contact_Name_L"].ToString();
        txtCivilId.Text = dtCon.Rows[0]["Civil_Id"].ToString();
        txtNickName.Text = dtCon.Rows[0]["Nick_Name"].ToString();
        strDepartmentId = dtCon.Rows[0]["Dep_Id"].ToString();
        if (strDepartmentId != "0")
        {
            ddlDepartment.SelectedValue = strDepartmentId;
        }
        else
        {

        }
        strDesignationId = dtCon.Rows[0]["Designation_Id"].ToString();
        if (strDesignationId != "0")
        {
            ddlDesignation.SelectedValue = strDesignationId;
        }
        else
        {

        }
        strReligionId = dtCon.Rows[0]["Religion_Id"].ToString();
        if (strReligionId != "0")
        {
            ddlReligion.SelectedValue = strReligionId;
        }
        else
        {
            FillReligion();
        }

        string strIsMail = dtCon.Rows[0]["IsEmail"].ToString();
        if (strIsMail == "True")
        {
            chkIsEmail.Checked = true;
        }
        else if (strIsMail == "False")
        {
            chkIsEmail.Checked = false;
        }
        string strIsSMS = dtCon.Rows[0]["IsSMS"].ToString();
        if (strIsSMS == "True")
        {
            chkIsSMS.Checked = true;
        }
        else if (strIsSMS == "False")
        {
            chkIsSMS.Checked = false;
        }

        //Add Address Section For Edit
        DataTable dtChild = objAddChild.GetAddressChildDataByAddTypeAndAddRefId("Contact", hdnContactId.Value);
        if (dtChild.Rows.Count > 0)
        {
            GvAddressName.DataSource = dtChild;
            GvAddressName.DataBind();
            AllPageCode();
        }

        txtContactName.Focus();
    }
    protected void BtnDelete(object sender, CommandEventArgs e)
    {
        string conid = e.CommandArgument.ToString();

        objContact.DeleteContactMaster(StrCompId, conid, "False", Session["UserId"].ToString(), DateTime.Now.ToString());

        DisplayMessage("Record Deleted");
        objCG.DeleteContactGroup(StrCompId, conid);
        fillgridbin();
        FillGrid();

        try
        {
            ((ImageButton)GridContact.Rows[((GridViewRow)((ImageButton)sender).Parent.Parent).RowIndex].FindControl("imgBtnDelete")).Focus();

        }
        catch
        {
            txtValue.Focus();
        }
    }
    protected void GridContact_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        GridContact.PageIndex = e.NewPageIndex;
        GridContact.DataSource = (DataTable)Session["dtFilter"];
        GridContact.DataBind();
        AllPageCode();
        GridContact.BottomPagerRow.Focus();
    }
    [System.Web.Services.WebMethodAttribute(), System.Web.Script.Services.ScriptMethodAttribute()]
    public static string[] GetCompletionList(string prefixText, int count, string contextKey)
    {
        Ems_ContactMaster ObjEmsC = new Ems_ContactMaster();
        DataTable dt = ObjEmsC.GetDistinctContactName(HttpContext.Current.Session["CompId"].ToString(), prefixText);

        string[] txt = new string[dt.Rows.Count];
        if (dt.Rows.Count > 0)
        {
            for (int i = 0; i < dt.Rows.Count; i++)
            {
                txt[i] = dt.Rows[i]["Contact_Name"].ToString() + "";
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
                dt = ObjEmsC.GetContactAllData(HttpContext.Current.Session["CompId"].ToString());
                if (dt.Rows.Count > 0)
                {
                    txt = new string[dt.Rows.Count];
                    for (int i = 0; i < dt.Rows.Count; i++)
                    {
                        txt[i] = dt.Rows[i]["Contact_Name"].ToString() + "";
                    }
                }
            }
        }
        return txt;
    }

    [System.Web.Services.WebMethodAttribute(), System.Web.Script.Services.ScriptMethodAttribute()]
    public static string[] GetCompletionList1(string prefixText, int count, string contextKey)
    {
        List<string> items = new List<string>();
        Ems_GroupMaster cs = new Ems_GroupMaster();
        DataTable dt = new DataTable();
        dt = cs.GetDistinctGroupName(HttpContext.Current.Session["CompId"].ToString(), prefixText);

        int tot = Convert.ToInt16(dt.Rows.Count);

        for (int i = 0; i < tot; i++)
        {
            if (HttpContext.Current.Session["Tempdt"] == null)
            {
                items.Add(dt.Rows[i]["GroupName"].ToString());
            }
            else
            {
                DataTable dtTemp = (DataTable)HttpContext.Current.Session["Tempdt"];
                if (dtTemp.Rows.Count == 0)
                {
                    items.Add(dt.Rows[i]["GroupName"].ToString());
                }
                else
                {
                    string gname = string.Empty;
                    for (int j = 0; j < dtTemp.Rows.Count; j++)
                    {
                        try
                        {
                            gname += "'" + dtTemp.Rows[j]["GroupId"].ToString() + "',";
                        }
                        catch
                        {

                        }
                    }
                    DataView dv = new DataView(dt);
                    string a = "GroupName Not In(" + gname.Substring(0, gname.Length - 1) + ")";
                    dv.RowFilter = a;
                    dt = dv.ToTable();
                    items.Add(dt.Rows[i]["GroupName"].ToString());
                }
            }
        }
        return items.ToArray();
    }
    [System.Web.Services.WebMethodAttribute(), System.Web.Script.Services.ScriptMethodAttribute()]
    public static string[] GetCompletionList0(string prefixText, int count, string contextKey)
    {
        List<string> items = new List<string>();

        LocationMaster objLoc = new LocationMaster();
        DataTable dt = objLoc.GetLocationMaster(HttpContext.Current.Session["CompId"].ToString());
        dt = new DataView(dt, "ELocationName Like '" + prefixText.ToString() + "%' and IsActive='True'", "", DataViewRowState.OriginalRows).ToTable();
        string[] txt = new string[dt.Rows.Count];

        if (dt.Rows.Count > 0)
        {
            for (int i = 0; i < dt.Rows.Count; i++)
            {
                txt[i] = dt.Rows[i]["ELocationName"].ToString() + "";
            }
        }
        else
        {
            dt = objLoc.GetLocationMaster(HttpContext.Current.Session["CompId"].ToString());
            if (dt.Rows.Count > 0)
            {
                txt = new string[dt.Rows.Count];
                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    txt[i] = dt.Rows[i]["ELocationName"].ToString() + "";
                }
            }
        }
        return txt;
    }



    /*Bin Start*/
    public void fillgridbin()
    {
        DataTable dt = objContact.GetContactFalseAllData(Session["CompId"].ToString());
        GridContactBin.DataSource = dt;
        GridContactBin.DataBind();
        lblTotalRecordsBin.Text = Resources.Attendance.Total_Records + " : " + dt.Rows.Count.ToString();
        Session["DtBin"] = dt;
        Session["dtFilterbin"] = dt;
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
    protected void GridContactBin_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        GridContactBin.PageIndex = e.NewPageIndex;
        DataTable dt = (DataTable)Session["dtFilterbin"];
        GridContactBin.DataSource = dt;
        GridContactBin.DataBind();
        AllPageCode();
        GridContactBin.BottomPagerRow.Focus();
        string temp = string.Empty;
        //bool isselcted;

        for (int i = 0; i < GridContactBin.Rows.Count; i++)
        {
            Label lblconid = (Label)GridContactBin.Rows[i].FindControl("Label5");
            string[] split = lblSelectedRecord.Text.Split(',');

            for (int j = 0; j < lblSelectedRecord.Text.Split(',').Length; j++)
            {
                if (lblSelectedRecord.Text.Split(',')[j] != "")
                {
                    if (lblconid.Text.Trim().ToString() == lblSelectedRecord.Text.Split(',')[j].Trim().ToString())
                    {
                        ((CheckBox)GridContactBin.Rows[i].FindControl("ChkActive")).Checked = true;
                    }
                }
            }
        }
    }
    protected void chkActive_CheckedChanged(object sender, EventArgs e)
    {
        string empidlist = string.Empty;
        string temp = string.Empty;
        int index = ((GridViewRow)((CheckBox)sender).Parent.Parent).RowIndex;
        Label lb = (Label)GridContactBin.Rows[index].FindControl("Label5");
        if (((CheckBox)GridContactBin.Rows[index].FindControl("chkActive")).Checked)
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
        ((CheckBox)GridContactBin.Rows[index].FindControl("chkActive")).Focus();
    }
    protected void lnkActive_Click(object sender, EventArgs e)
    {
        int Msg = 0;
        DataTable dt = objContact.GetContactFalseAllData(HttpContext.Current.Session["CompId"].ToString());
        if (GridContactBin.Rows.Count != 0)
        {
            if (lblSelectedRecord.Text != "")
            {
                for (int j = 0; j < lblSelectedRecord.Text.Split(',').Length; j++)
                {
                    if (lblSelectedRecord.Text.Split(',')[j] != "")
                    {
                        Msg = objContact.DeleteContactMaster(StrCompId, lblSelectedRecord.Text.Split(',')[j].Trim().ToString(), "True", Session["UserId"].ToString(), DateTime.Now.ToString());
                    }
                }
            }

            if (Msg != 0)
            {
                FillGrid();
                fillgridbin();

                lblSelectedRecord.Text = "";
                DisplayMessage("Record has been Activated And Moved To Active");
                btnRefreshBin_Click(null, null);


            }
            else
            {
                int fleg = 0;
                foreach (GridViewRow Gvr in GridContactBin.Rows)
                {
                    CheckBox chk = (CheckBox)Gvr.FindControl("ChkActive");
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
                    txtValueBin.Focus();

                }
                else
                {
                    DisplayMessage("Record Not Activated");
                }
            }
        }
    }
    protected void chkActiveAll_CheckedChanged(object sender, EventArgs e)
    {
        CheckBox chkSelAll = ((CheckBox)GridContactBin.HeaderRow.FindControl("chkActiveAll"));
        for (int i = 0; i < GridContactBin.Rows.Count; i++)
        {
            ((CheckBox)GridContactBin.Rows[i].FindControl("ChkActive")).Checked = chkSelAll.Checked;
            if (chkSelAll.Checked)
            {
                if (!lblSelectedRecord.Text.Split(',').Contains(((Label)(GridContactBin.Rows[i].FindControl("Label5"))).Text.Trim().ToString()))
                {
                    lblSelectedRecord.Text += ((Label)(GridContactBin.Rows[i].FindControl("Label5"))).Text.Trim().ToString() + ",";
                }
            }
            else
            {
                string temp = string.Empty;
                string[] split = lblSelectedRecord.Text.Split(',');
                foreach (string item in split)
                {
                    if (item != ((Label)(GridContactBin.Rows[i].FindControl("Label5"))).Text.Trim().ToString())
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
    protected void btnRefreshBin_Click(object sender, ImageClickEventArgs e)
    {
        fillgridbin();
        txtValueBin.Text = "";
        ddlOptionBin.SelectedIndex = 2;
        ddlFieldNameBin.SelectedIndex = 1;

        txtValueBin.Focus();
        lblSelectedRecord.Text = "";
    }
    protected void btnbindBin_Click(object sender, ImageClickEventArgs e)
    {
        lblSelectedRecord.Text = "";
        if (ddlOptionBin.SelectedIndex != 0)
        {
            string condition = string.Empty;
            if (ddlOptionBin.SelectedIndex == 1)
            {
                condition = ddlFieldNameBin.SelectedValue + "='" + txtValueBin.Text.Trim() + "'";
            }
            else if (ddlOptionBin.SelectedIndex == 2)
            {
                condition = ddlFieldNameBin.SelectedValue + " Like '%" + txtValueBin.Text.Trim() + "%'";
            }
            else
            {
                condition = ddlFieldNameBin.SelectedValue + " like '" + txtValueBin.Text.Trim() + "%'";
            }
            DataTable dtCompany = (DataTable)Session["DtBin"];
            DataView view = new DataView(dtCompany, condition, "", DataViewRowState.CurrentRows);
            GridContactBin.DataSource = view.ToTable();
            GridContactBin.DataBind();
            lblTotalRecordsBin.Text = Resources.Attendance.Total_Records + " : " + view.ToTable().Rows.Count + "";
            Session["dtFilterbin"] = view.ToTable();

            ddlOptionBin.SelectedIndex = 2;
            ddlFieldNameBin.SelectedIndex = 1;
            txtValueBin.Focus();
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
        btnbindBin.Focus();
    }

    protected void GridContactBin_Sorting(object sender, GridViewSortEventArgs e)
    {
        DataTable dt = (DataTable)Session["dtFilterbin"];
        string sortdir = "DESC";
        if (ViewState["SortDirBin"] != null)
        {
            sortdir = ViewState["SortDirBin"].ToString();
            if (sortdir == "ASC")
            {
                e.SortDirection = SortDirection.Descending;
                ViewState["SortDirBin"] = "DESC";
            }
            else
            {
                e.SortDirection = SortDirection.Ascending;
                ViewState["SortDirBin"] = "ASC";
            }
        }
        else
        {
            ViewState["SortDirBin"] = "DESC";
        }
        dt = (new DataView(dt, "", e.SortExpression + " " + ViewState["SortDirBin"].ToString(), DataViewRowState.CurrentRows)).ToTable();
        Session["dtFilterbin"] = dt;
        GridContactBin.DataSource = dt;
        GridContactBin.DataBind();
        AllPageCode();
        GridContactBin.HeaderRow.Focus();
    }
    //Bin End
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
    public void ResetField()
    {
        txtContactName.ReadOnly = false;
        hdnContactId.Value = "";
        hdnCompId.Value = "";
        BindTree();
        FillDepartment();
        FillDesignation();
        FillReligion();
        txtContactCode.Text = "";
        txtNickName.Text = "";
        txtContactName.Text = "";
        txtLContactName.Text = "";
        txtCivilId.Text = "";
        txtCname.Text = "";
        txtConId.Text = "";
        chkIsEmail.Checked = false;
        chkIsSMS.Checked = false;

        btnNew.Text = Resources.Attendance.New;

        GvAddressName.DataSource = null;
        GvAddressName.DataBind();
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
    private string GetCompanyContactId()
    {
        string retval = string.Empty;
        if (txtCname.Text != "")
        {
            retval = (txtCname.Text.Split('/'))[txtCname.Text.Split('/').Length - 1];

            float flTemp = 0;
            if (float.TryParse(retval, out flTemp))
            {
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
        }
        else
        {
            retval = "";
        }
        return retval;
    }
    public string getGroupIdbyName(string GroupName)
    {
        string GroupId = string.Empty;
        try
        {
            DataTable dt = objGroup.GetGroupMasterAllData(StrCompId);
            dt = new DataView(dt, "GroupName ='" + GroupName.ToString() + "'", "", DataViewRowState.CurrentRows).ToTable();
            if (dt.Rows.Count != 0)
            {
                GroupId = dt.Rows[0][0].ToString();
            }

            return GroupId;
        }
        catch
        {
            return GroupId;
        }
    }
    public string getGroupNameById(string GroupId)
    {
        string GroupName = string.Empty;
        try
        {
            DataTable dt = objGroup.GetGroupMasterAllData(StrCompId);
            dt = new DataView(dt, "GroupId ='" + GroupId.ToString() + "'", "", DataViewRowState.CurrentRows).ToTable();
            if (dt.Rows.Count != 0)
            {
                GroupName = dt.Rows[0]["GroupName"].ToString();
            }
            return GroupName;
        }
        catch
        {
            return GroupId;
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
    protected void GridContact_Sorting(object sender, GridViewSortEventArgs e)
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
        GridContact.DataSource = dt;
        GridContact.DataBind();
        AllPageCode();
        GridContact.HeaderRow.Focus();
    }

    //New Code On 12-06-2013 :: Add New Company Section
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

    #region New Group Concept
    protected void btnGSave_Click(object sender, EventArgs e)
    {
        try
        {
            if (navTree.SelectedNode.Checked == true)
            {
                UnSelectChild(navTree.SelectedNode);
            }
            else
            {
                SelectChild(navTree.SelectedNode);
            }
        }
        catch (Exception)
        {

        }
        objCG.DeleteContactGroup(StrCompId, hdnContactId.Value);

        int refid = 0;
        foreach (TreeNode ModuleNode in navTree.Nodes)
        {
            //here save one row for module
            if (ModuleNode.Checked)
            {
                refid = objCG.InsertContactGroup(StrCompId, hdnContactId.Value, ModuleNode.Value, "True", Session["UserId"].ToString(), DateTime.Now.ToString(), Session["UserId"].ToString(), DateTime.Now.ToString());

                foreach (TreeNode ObjNode in ModuleNode.ChildNodes)
                {
                    if (ObjNode.Checked)
                    {
                        int refid1 = 0;
                        refid1 = objCG.InsertContactGroup(StrCompId, hdnContactId.Value, ObjNode.Value, "True", Session["UserId"].ToString(), DateTime.Now.ToString(), Session["UserId"].ToString(), DateTime.Now.ToString());
                    }
                }
            }
        }

        foreach (GridViewRow gvCustomer in GvCustomerBrand.Rows)
        {
            CheckBox chkCActive = (CheckBox)gvCustomer.FindControl("chkCBActive");
            HiddenField hdnCBrandId = (HiddenField)gvCustomer.FindControl("hdnCBBrandId");

            if (chkCActive.Checked == true)
            {
                DataTable dtCust = objCustomer.GetCustomerAllDataByCustomerId(StrCompId, hdnCBrandId.Value, hdnContactId.Value);
                if (dtCust.Rows.Count > 0)
                {

                }
                else
                {
                    objCustomer.InsertCustomerMaster(StrCompId, hdnCBrandId.Value, hdnContactId.Value, "", "0", "0", "0", "0", "0", "0", "0", "0", "0", "0", "False", "", "", "", "", "", "", "True", Session["UserId"].ToString(), DateTime.Now.ToString(), Session["UserId"].ToString(), DateTime.Now.ToString());
                }
            }
        }

        foreach (GridViewRow gvSupplier in GvSupplierBrand.Rows)
        {
            CheckBox chkSActive = (CheckBox)gvSupplier.FindControl("chkSBActive");
            HiddenField hdnSBrandId = (HiddenField)gvSupplier.FindControl("hdnSBBrandId");

            if (chkSActive.Checked == true)
            {
                DataTable dtSup = objSupplier.GetSupplierAllDataBySupplierId(StrCompId, hdnSBrandId.Value, hdnContactId.Value);
                if (dtSup.Rows.Count > 0)
                {

                }
                else
                {
                    objSupplier.InsertSupplierMaster(StrCompId, hdnSBrandId.Value, hdnContactId.Value, "", "0", "0", "0", "0", "0", "0", "", "", "", "", "", "True", Session["UserId"].ToString(), DateTime.Now.ToString(), Session["UserId"].ToString(), DateTime.Now.ToString());
                }
            }
        }

        if (refid != 0)
        {
            DisplayMessage("Record Saved");
            btnList_Click(null, null);
            ResetField();
            FillGrid();
            fillgridbin();
            PnlGroup.Visible = false;
            PnlNew.Visible = true;
        }
    }
    protected void btnGCancel_Click(object sender, EventArgs e)
    {
        btnList_Click(null, null);
        ResetField();
        PnlGroup.Visible = false;
        panNewEdit.Visible = true;
    }
    private void UnSelectChild(TreeNode treeNode)
    {
        int i = 0;
        treeNode.Checked = false;
        while (i < treeNode.ChildNodes.Count)
        {
            treeNode.ChildNodes[i].Checked = false;
            UnSelectChild(treeNode.ChildNodes[i]);
            i++;
        }

        navTree.DataBind();

        foreach (TreeNode ModuleNode in navTree.Nodes)
        {
            if (ModuleNode.Checked == false)
            {
                if (ModuleNode.Value == "1")
                {
                    pnlCustomerBrand.Visible = false;
                }
                if (ModuleNode.Value == "2")
                {
                    pnlSupplierBrand.Visible = false;
                }
            }
        }
    }
    private void SelectChild(TreeNode treeNode)
    {
        int i = 0;
        treeNode.Checked = true;
        while (i < treeNode.ChildNodes.Count)
        {
            treeNode.ChildNodes[i].Checked = true;
            SelectChild(treeNode.ChildNodes[i]);
            i++;
        }
        try
        {
            treeNode.Parent.Checked = true;
            treeNode.Parent.Parent.Checked = true;
        }
        catch
        {

        }

        navTree.DataBind();

        foreach (TreeNode ModuleNode in navTree.Nodes)
        {
            if (ModuleNode.Checked)
            {
                if (ModuleNode.Value == "1")
                {
                    pnlCustomerBrand.Visible = true;
                }
                if (ModuleNode.Value == "2")
                {
                    pnlSupplierBrand.Visible = true;
                }
            }
        }
    }
    public void BindTree()
    {
        string RoleId = string.Empty;
        string moduleids = string.Empty;

        navTree.DataSource = null;
        navTree.DataBind();
        navTree.Nodes.Clear();
        if (hdnContactId.Value != "")
        {
            DataTable dtRoleP = objCG.GetContactGroupByContactId(StrCompId, hdnContactId.Value);
            if (dtRoleP.Rows.Count > 0)
            {

            }
            DataTable dtGroup = objGroup.GetGroupMasterTrueAllData(StrCompId);
            DataTable DtGroupMainNode = new DataTable();

            DtGroupMainNode = objGroup.GetGroupMasterOnlyMainNode(StrCompId);


            for (int i = 0; i < DtGroupMainNode.Rows.Count; i++)
            {
                moduleids += "'" + DtGroupMainNode.Rows[i]["Group_Id"].ToString() + "'" + ",";
            }

            dtGroup = new DataView(dtGroup, "Group_Id in (" + moduleids.Substring(0, moduleids.Length - 1) + ")", "", DataViewRowState.CurrentRows).ToTable();

            foreach (DataRow datarow in dtGroup.Rows)
            {
                TreeNode tn = new TreeNode();

                tn = new TreeNode(datarow["Group_Name"].ToString(), datarow["Group_Id"].ToString());

                DataTable dtModuleSaved = new DataView(dtRoleP, "Group_Id='" + datarow["Group_Id"].ToString() + "'", "", DataViewRowState.CurrentRows).ToTable();

                if (dtModuleSaved.Rows.Count > 0)
                {
                    tn.Checked = true;
                    if (datarow["Group_Id"].ToString() == "1")
                    {
                        pnlCustomerBrand.Visible = true;
                    }
                    if (datarow["Group_Id"].ToString() == "2")
                    {
                        pnlSupplierBrand.Visible = true;
                    }
                }

                //New Added
                DataTable dtAllChild = objGroup.GetGroupMasterByParentId(StrCompId, datarow["Group_Id"].ToString());

                foreach (DataRow childrow in dtAllChild.Rows)
                {
                    string GetUrl = string.Empty;
                    GetUrl = childrow[1].ToString();

                    TreeNode tnChild = new TreeNode(childrow[2].ToString(), GetUrl);
                    DataTable dtObj = new DataView(dtRoleP, "Group_Id='" + GetUrl + "'", "", DataViewRowState.CurrentRows).ToTable();

                    if (dtObj.Rows.Count > 0)
                    {
                        tnChild.Checked = true;
                    }
                    tn.ChildNodes.Add(tnChild);
                }
                //End
                navTree.Nodes.Add(tn);
            }
        }
        navTree.DataBind();
        navTree.CollapseAll();
        return;
    }
    protected void navTree_TreeNodeCheckChanged(object sender, TreeNodeEventArgs e)
    {

    }
    protected void navTree_SelectedNodeChanged1(object sender, EventArgs e)
    {

    }
    protected void GvCustomerBrand_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        GvCustomerBrand.PageIndex = e.NewPageIndex;
        DataTable dt = (DataTable)Session["BrandCustomer"];
        GvCustomerBrand.DataSource = dt;
        GvCustomerBrand.DataBind();
    }
    protected void GvCustomerBrand_Sorting(object sender, GridViewSortEventArgs e)
    {
        DataTable dt = (DataTable)Session["BrandCustomer"];
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
        Session["BrandCustomer"] = dt;
        GvCustomerBrand.DataSource = dt;
        GvCustomerBrand.DataBind();
    }
    protected void GvSupplierBrand_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        GvSupplierBrand.PageIndex = e.NewPageIndex;
        DataTable dt = (DataTable)Session["BrandSupplier"];
        GvSupplierBrand.DataSource = dt;
        GvSupplierBrand.DataBind();
    }
    protected void GvSupplierBrand_Sorting(object sender, GridViewSortEventArgs e)
    {
        DataTable dt = (DataTable)Session["BrandSupplier"];
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
        Session["BrandSupplier"] = dt;
        GvSupplierBrand.DataSource = dt;
        GvSupplierBrand.DataBind();
    }
    protected void chkCBActiveAll_CheckedChanged(object sender, EventArgs e)
    {
        CheckBox chkSelAll = ((CheckBox)GvCustomerBrand.HeaderRow.FindControl("chkCBActiveAll"));
        for (int i = 0; i < GvCustomerBrand.Rows.Count; i++)
        {
            ((CheckBox)GvCustomerBrand.Rows[i].FindControl("chkCBActive")).Checked = chkSelAll.Checked;
            if (chkSelAll.Checked)
            {
                if (!lblSelectedRecord.Text.Split(',').Contains(((HiddenField)(GvCustomerBrand.Rows[i].FindControl("hdnCBBrandId"))).Value.Trim().ToString()))
                {
                    lblSelectedRecord.Text += ((HiddenField)(GvCustomerBrand.Rows[i].FindControl("hdnCBBrandId"))).Value.Trim().ToString() + ",";
                }
            }
            else
            {
                string temp = string.Empty;
                string[] split = lblSelectedRecord.Text.Split(',');
                foreach (string item in split)
                {
                    if (item != ((HiddenField)(GvCustomerBrand.Rows[i].FindControl("hdnCBBrandId"))).Value.Trim().ToString())
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
    protected void chkCBActive_CheckedChanged(object sender, EventArgs e)
    {
        string empidlist = string.Empty;
        string temp = string.Empty;
        int index = ((GridViewRow)((CheckBox)sender).Parent.Parent).RowIndex;
        HiddenField lb = (HiddenField)GvCustomerBrand.Rows[index].FindControl("hdnCBBrandId");
        if (((CheckBox)GvCustomerBrand.Rows[index].FindControl("chkCBActive")).Checked)
        {
            empidlist += lb.Value.Trim().ToString() + ",";
            lblSelectedRecord.Text += empidlist;
        }
        else
        {
            empidlist += lb.Value.ToString().Trim();
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
    protected void chkSBActiveAll_CheckedChanged(object sender, EventArgs e)
    {
        CheckBox chkSelAll = ((CheckBox)GvSupplierBrand.HeaderRow.FindControl("chkSBActiveAll"));
        for (int i = 0; i < GvSupplierBrand.Rows.Count; i++)
        {
            ((CheckBox)GvSupplierBrand.Rows[i].FindControl("chkSBActive")).Checked = chkSelAll.Checked;
            if (chkSelAll.Checked)
            {
                if (!lblSelectedRecord.Text.Split(',').Contains(((HiddenField)(GvSupplierBrand.Rows[i].FindControl("hdnSBBrandId"))).Value.Trim().ToString()))
                {
                    lblSelectedRecord.Text += ((HiddenField)(GvSupplierBrand.Rows[i].FindControl("hdnSBBrandId"))).Value.Trim().ToString() + ",";
                }
            }
            else
            {
                string temp = string.Empty;
                string[] split = lblSelectedRecord.Text.Split(',');
                foreach (string item in split)
                {
                    if (item != ((HiddenField)(GvSupplierBrand.Rows[i].FindControl("hdnSBBrandId"))).Value.Trim().ToString())
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
    protected void chkSBActive_CheckedChanged(object sender, EventArgs e)
    {
        string empidlist = string.Empty;
        string temp = string.Empty;
        int index = ((GridViewRow)((CheckBox)sender).Parent.Parent).RowIndex;
        HiddenField lb = (HiddenField)GvSupplierBrand.Rows[index].FindControl("hdnSBBrandId");
        if (((CheckBox)GvSupplierBrand.Rows[index].FindControl("chkSBActive")).Checked)
        {
            empidlist += lb.Value.Trim().ToString() + ",";
            lblSelectedRecord.Text += empidlist;
        }
        else
        {
            empidlist += lb.Value.ToString().Trim();
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
    #endregion

    //For Email Validation
    public bool CheckValid(TextBox txt, string Error_messagevalue)
    {
        if (txt.Text == "")
        {
            DisplayMessage(Error_messagevalue);
            txt.Focus();
            return false;
        }
        else
        {
            return true;
        }
    }
    protected void btnList_Click(object sender, EventArgs e)
    {
        pnlMenuList.BackColor = System.Drawing.ColorTranslator.FromHtml("#ccddee");

        pnlMenuNew.BackColor = System.Drawing.ColorTranslator.FromHtml("#90bde9");
        pnlMenuBin.BackColor = System.Drawing.ColorTranslator.FromHtml("#90bde9");

        PnlList.Visible = true;
        PnlNew.Visible = false;
        PnlBin.Visible = false;
        txtValue.Focus();
    }
    protected void btnNew_Click(object sender, EventArgs e)
    {
        pnlMenuNew.BackColor = System.Drawing.ColorTranslator.FromHtml("#ccddee");

        pnlMenuList.BackColor = System.Drawing.ColorTranslator.FromHtml("#90bde9");
        pnlMenuBin.BackColor = System.Drawing.ColorTranslator.FromHtml("#90bde9");

        PnlList.Visible = false;
        panNewEdit.Visible = true;
        PnlBin.Visible = false;
        PnlGroup.Visible = false;
        PnlNew.Visible = true;
        txtContactCode.Focus();
    }
    protected void btnBin_Click(object sender, EventArgs e)
    {
        pnlMenuBin.BackColor = System.Drawing.ColorTranslator.FromHtml("#ccddee");

        pnlMenuNew.BackColor = System.Drawing.ColorTranslator.FromHtml("#90bde9");
        pnlMenuList.BackColor = System.Drawing.ColorTranslator.FromHtml("#90bde9");
        PnlNew.Visible = false;
        PnlBin.Visible = true;
        PnlList.Visible = false;
        fillgridbin();
        txtValueBin.Focus();
    }
    protected void ImgbtnSelectAll_Click(object sender, ImageClickEventArgs e)
    {
        DataTable dtContact = (DataTable)Session["dtFilterbin"];

        if (ViewState["Select"] == null)
        {
            ViewState["Select"] = 1;
            foreach (DataRow dr in dtContact.Rows)
            {
                if (!lblSelectedRecord.Text.Split(',').Contains(dr["Contact_Id"]))
                {
                    lblSelectedRecord.Text += dr["Contact_Id"] + ",";
                }
            }
            for (int i = 0; i < GridContactBin.Rows.Count; i++)
            {
                string[] split = lblSelectedRecord.Text.Split(',');
                Label lblconid = (Label)GridContactBin.Rows[i].FindControl("Label5");
                for (int j = 0; j < lblSelectedRecord.Text.Split(',').Length; j++)
                {
                    if (lblSelectedRecord.Text.Split(',')[j] != "")
                    {
                        if (lblconid.Text.Trim().ToString() == lblSelectedRecord.Text.Split(',')[j].Trim().ToString())
                        {
                            ((CheckBox)GridContactBin.Rows[i].FindControl("chkActive")).Checked = true;
                        }
                    }
                }
            }
        }
        else
        {
            lblSelectedRecord.Text = "";
            DataTable dtContact1 = (DataTable)Session["dtFilterbin"];
            GridContactBin.DataSource = dtContact1;
            GridContactBin.DataBind();
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
                    b = objContact.DeleteContactMaster(StrCompId, lblSelectedRecord.Text.Split(',')[j].Trim(), "True", Session["UserId"].ToString(), DateTime.Now.ToString());
                }
            }
        }

        if (b != 0)
        {
            FillGrid();
            fillgridbin();
            lblSelectedRecord.Text = "";
            ViewState["Select"] = null;
            DisplayMessage("Record Activate");
        }
        else
        {
            int fleg = 0;
            foreach (GridViewRow Gvr in GridContactBin.Rows)
            {
                CheckBox chk = (CheckBox)Gvr.FindControl("chkActive");
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
        dsAddressCat = ObjAddCat.GetAddressCategoryAll(StrCompId);
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
        dsCountry = objCountry.GetCountryMaster();
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
