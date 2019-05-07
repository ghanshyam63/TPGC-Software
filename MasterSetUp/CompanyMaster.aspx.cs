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

public partial class MasterSetUp_CompanyMaster : BasePage
{



    EmployeeMaster objEmp = new EmployeeMaster();
    Common cmn = new Common();
    CompanyMaster objComp = new CompanyMaster();
    CountryMaster objCountry = new CountryMaster();
    CurrencyMaster objCurrency = new CurrencyMaster();
    SystemParameter objSys = new SystemParameter();

    Set_AddressMaster AM = new Set_AddressMaster();
    Set_AddressCategory ObjAddressCat = new Set_AddressCategory();
    Set_AddressChild objAddChild = new Set_AddressChild();

    Set_ApplicationParameter objAppParam = new Set_ApplicationParameter();
    Att_Device_Parameter objDeviceParam = new Att_Device_Parameter();

    protected void Page_Load(object sender, EventArgs e)
    {
        
        Session["AccordianId"] = "8";
        
        if (Session["UserId"] == null)
        {
            Response.Redirect("~/ERPLogin.aspx");
        }
       
        AllPageCode();
        if (!IsPostBack)
        {
            Session["empimgpath"] = null;
            txtValue.Focus();
           
            FillGridBin();
            FillGrid();
            FillCountryDDL();
            FillCurrencyDDL();
            FillddlParentCompanyDDL();
            btnList_Click(null, null);
            Session["EditValue"] = "";
        }
    }
    public void AllPageCode()
    {

        Page.Title=objSys.GetSysTitle();
        Session["AccordianId"] = "8";
        Session["HeaderText"] = "Master Setup";
        DataTable dtAllPageCode = cmn.GetAllPagePermission(Session["UserId"].ToString(), "8", "6");
        if (dtAllPageCode.Rows.Count != 0)
        {
            if (dtAllPageCode.Rows[0][0].ToString() == "SuperAdmin")
            {
                btnSave.Visible = true;
                foreach (GridViewRow Row in gvCompanyMaster.Rows)
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
                    foreach (GridViewRow Row in gvCompanyMaster.Rows)
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
        DataTable dt = Common.GetEmployee(prefixText, "0");

        string[] str = new string[dt.Rows.Count];
        for (int i = 0; i < dt.Rows.Count; i++)
        {
            str[i] = "" + dt.Rows[i][1].ToString() + "/(" + dt.Rows[i][2].ToString() + ")/" + dt.Rows[i][0].ToString() + "";
        }
        return str;
    }
    public void DisplayMessage(string str)
    {
        ScriptManager.RegisterStartupScript(this, GetType(), "", "alert('" + str + "');", true);
    }
    protected void btnEdit_Command(object sender, CommandEventArgs e)
    {
        editid.Value = e.CommandArgument.ToString();
        Session["EditValue"]=editid.Value;

        DataTable dt = objComp.GetCompanyMasterById(editid.Value);
        if (dt.Rows.Count > 0)
        {
            txtCompanyCode.Text =dt.Rows[0]["Company_Code"].ToString();
            txtCompanyName.Text = dt.Rows[0]["Company_Name"].ToString();
            txtCompanyNameL.Text = dt.Rows[0]["Company_Name_L"].ToString();
            txtLicenceNo.Text = dt.Rows[0]["Commerical_License_No"].ToString();
            
            
           
            imgLogo.ImageUrl ="~/CompanyResource/" + "/" + editid.Value + "/"+ dt.Rows[0]["Logo_Path"].ToString();

            Session["empimgpath"] = dt.Rows[0]["Logo_Path"].ToString();
            try
            {
                ddlParentCompany.SelectedValue = dt.Rows[0]["Parent_Company_Id"].ToString();
            }
            catch
            {
            }
            try
            {
                ddlCountry1.SelectedValue = dt.Rows[0]["Country_Id"].ToString();
            }
            catch
            {
            }
            try
            {

                ddlCurrency.SelectedValue = dt.Rows[0]["Currency_Id"].ToString();
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


            DataTable dtChild = objAddChild.GetAddressChildDataByAddTypeAndAddRefId("Company", editid.Value);
            if (dtChild.Rows.Count > 0)
            {
                GvAddressName.DataSource = dtChild;
                GvAddressName.DataBind();
            }


            btnNew_Click(null, null);
            btnNew.Text = Resources.Attendance.Edit;

        }



    }


    protected void IbtnParam_Command(object sender, CommandEventArgs e)
    {
        Response.Redirect("../MasterSetup/CompanyParameter.aspx?CompanyId='"+e.CommandArgument.ToString()+"'");

    }
    protected void IbtnDelete_Command(object sender, CommandEventArgs e)
    {
        int b = 0;
        b = objComp.DeleteCompanyMaster(e.CommandArgument.ToString(),false.ToString(), Session["UserId"].ToString(), DateTime.Now.ToString());
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
    protected void gvCompanyMaster_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        gvCompanyMaster.PageIndex = e.NewPageIndex;

        DataTable dt = (DataTable)Session["dtFilter"];
        gvCompanyMaster.DataSource = dt;
        gvCompanyMaster.DataBind();

        AllPageCode();
    }
    protected void gvCompanyMaster_OnSorting(object sender, GridViewSortEventArgs e)
    {
        HDFSort.Value = HDFSort.Value == "ASC" ? "DESC" : "ASC";
        DataTable dt = new DataTable();
        dt = (DataTable)Session["dtFilter"];
        DataView dv = new DataView(dt);
        string Query = "" + e.SortExpression + " " + HDFSort.Value + "";
        dv.Sort = Query;
        dt = dv.ToTable();
        Session["dtFilter"] = dt;
        gvCompanyMaster.DataSource = dt;
        gvCompanyMaster.DataBind(); AllPageCode();
    }


    protected void gvCompanyMasterBin_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {

        gvCompanyMasterBin.PageIndex = e.NewPageIndex;
        if (HDFSortbin.Value == "")
            FillGridBin();
        else
        {
            DataTable dt = (DataTable)Session["dtbinFilter"];
            gvCompanyMasterBin.DataSource = dt;
            gvCompanyMasterBin.DataBind();
            AllPageCode();
        }
        string temp = string.Empty;
        bool isselcted;

        for (int i = 0; i < gvCompanyMasterBin.Rows.Count; i++)
        {
            Label lblconid = (Label)gvCompanyMasterBin.Rows[i].FindControl("lblCompanyId");
            string[] split = lblSelectedRecord.Text.Split(',');

            for (int j = 0; j < lblSelectedRecord.Text.Split(',').Length; j++)
            {
                if (lblSelectedRecord.Text.Split(',')[j] != "")
                {
                    if (lblconid.Text.Trim().ToString() == lblSelectedRecord.Text.Split(',')[j].Trim().ToString())
                    {
                        ((CheckBox)gvCompanyMasterBin.Rows[i].FindControl("chkgvSelect")).Checked = true;
                    }
                }
            }
        }

    }
    protected void gvCompanyMasterBin_OnSorting(object sender, GridViewSortEventArgs e)
    {
        lblSelectedRecord.Text = "";
        HDFSortbin.Value = HDFSortbin.Value == "ASC" ? "DESC" : "ASC";
        DataTable dt = new DataTable();
        dt =  objComp.GetCompanyMasterInactive();
        DataView dv = new DataView(dt);
        string Query = "" + e.SortExpression + " " + HDFSortbin.Value + "";
        dv.Sort = Query;
        dt = dv.ToTable();
        Session["dtbinFilter"] = dt;
        gvCompanyMasterBin.DataSource = dt;
        gvCompanyMasterBin.DataBind();
        AllPageCode();

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
    protected void txtCompanyName_OnTextChanged(object sender, EventArgs e)
    {

        if (editid.Value == "")
        {
            DataTable dt = objComp.GetCompanyMasterByCompanyName(txtCompanyName.Text.Trim());
            if (dt.Rows.Count > 0)
            {
                txtCompanyName.Text = "";
                DisplayMessage("Company Name Already Exists");
                System.Web.UI.ScriptManager.GetCurrent(this).SetFocus(txtCompanyName);
                return;
            }
            DataTable dt1 = objComp.GetCompanyMasterInactive();
            dt1 = new DataView(dt1, "Company_Name='" + txtCompanyName.Text + "'", "", DataViewRowState.CurrentRows).ToTable();
            if (dt1.Rows.Count > 0)
            {
                txtCompanyName.Text = "";
                DisplayMessage("Company Name Already Exists - Go to Bin Tab");
                System.Web.UI.ScriptManager.GetCurrent(this).SetFocus(txtCompanyName);
                return;
            }
            txtCompanyNameL.Focus();
        }
        else
        {
            DataTable dtTemp = objComp.GetCompanyMasterById(editid.Value);
            if (dtTemp.Rows.Count > 0)
            {
                if (dtTemp.Rows[0]["Company_Name"].ToString() != txtCompanyName.Text)
                {
                    DataTable dt = objComp.GetCompanyMaster();
                    dt = new DataView(dt, "Company_Name='" + txtCompanyName.Text + "'", "", DataViewRowState.CurrentRows).ToTable();
                    if (dt.Rows.Count > 0)
                    {
                        txtCompanyName.Text = "";
                        DisplayMessage("Company Name Already Exists");
                        System.Web.UI.ScriptManager.GetCurrent(this).SetFocus(txtCompanyName);
                        return;
                    }
                    DataTable dt1 = objComp.GetCompanyMaster();
                    dt1 = new DataView(dt1, "Company_Name='" + txtCompanyName.Text + "'", "", DataViewRowState.CurrentRows).ToTable();
                    if (dt1.Rows.Count > 0)
                    {
                        txtCompanyName.Text = "";
                        DisplayMessage("Company Name Already Exists - Go to Bin Tab");
                        System.Web.UI.ScriptManager.GetCurrent(this).SetFocus(txtCompanyName);
                        return;
                    }
                }
            }
            txtCompanyNameL.Focus();
        }
    }
  
    
    protected void btnNew_Click(object sender, EventArgs e)
    {
        txtCompanyCode.Focus();
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
            DataTable dtCust = (DataTable)Session["Company"];


            DataView view = new DataView(dtCust, condition, "", DataViewRowState.CurrentRows);
            Session["dtFilter"] = view.ToTable();
            lblTotalRecords.Text = Resources.Attendance.Total_Records + ": " + view.ToTable().Rows.Count.ToString() + "";

            gvCompanyMaster.DataSource = view.ToTable();
            gvCompanyMaster.DataBind();
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
            DataTable dtCust = (DataTable)Session["dtbinCompany"];


            DataView view = new DataView(dtCust, condition, "", DataViewRowState.CurrentRows);
            Session["dtbinFilter"] = view.ToTable();
            lblbinTotalRecords.Text = Resources.Attendance.Total_Records + ": " + view.ToTable().Rows.Count.ToString() + "";

            gvCompanyMasterBin.DataSource = view.ToTable();
            gvCompanyMasterBin.DataBind();


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

  
 
    protected void btnSave_Click(object sender, EventArgs e)
    {
        int b = 0;
     
        if (txtCompanyCode.Text == "")
        {
            DisplayMessage("Enter Company Code");
            txtCompanyCode.Focus();
            return;
        }
       
        if (txtCompanyName.Text == "")
        {
            DisplayMessage("Enter Company Name");
            txtCompanyName.Focus();
            return;
        }

        if (Session["empimgpath"] == null)
        {
            Session["empimgpath"] = "";

        }



        string empid = string.Empty;
        if (txtManagerName.Text != "")
        {
            empid = txtManagerName.Text.Split('/')[txtManagerName.Text.Split('/').Length - 1];

            DataTable dtEmp = objEmp.GetEmployeeMaster(Session["CompId"].ToString());

            dtEmp = new DataView(dtEmp, "Emp_Code='" + empid + "'", "", DataViewRowState.CurrentRows).ToTable();

            if (dtEmp.Rows.Count > 0)
            {
                empid = dtEmp.Rows[0]["Emp_Id"].ToString();
            }
            else
            {
                empid = "0";
            }

        }
        else
        {
            empid = "0";
        }




        if (editid.Value == "")
        {
            DataTable dt = objComp.GetCompanyMaster();

            dt = new DataView(dt, "Company_Code='" + txtCompanyCode.Text + "'", "", DataViewRowState.CurrentRows).ToTable();
            if (dt.Rows.Count > 0)
            {
                DisplayMessage("Company Code Already Exists");
                txtCompanyCode.Focus();
                return;

            }
            DataTable dt1 = objComp.GetCompanyMaster();

            dt1 = new DataView(dt1, "Company_Name='" + txtCompanyName.Text + "'", "", DataViewRowState.CurrentRows).ToTable();
            if (dt1.Rows.Count > 0)
            {
                DisplayMessage("Company Name Already Exists");
                txtCompanyName.Focus();
                return;

            }

            if(ddlCountry1.SelectedIndex==0)
            {

                DisplayMessage("Select Country");
                ddlCountry1.Focus();
                return;

            }
            if (ddlCurrency.SelectedIndex == 0)
            {

                DisplayMessage("Select Currency");
                ddlCurrency.Focus();
                return;

            }


            b = objComp.InsertCompanyMaster(txtCompanyName.Text, txtCompanyNameL.Text, txtCompanyCode.Text, Session["empimgpath"].ToString(), ddlParentCompany.SelectedValue,empid, ddlCurrency.SelectedValue, ddlCountry1.SelectedValue, txtLicenceNo.Text, "", "", "", "", "",false.ToString(),DateTime.Now.ToString(),true.ToString(), Session["UserId"].ToString(), DateTime.Now.ToString(), Session["UserId"].ToString(), DateTime.Now.ToString());

            if (b != 0)
            {
                Session["EditValue"] = b.ToString();

                string strMaxId = string.Empty;

                strMaxId = b.ToString();

                    foreach (GridViewRow gvr in GvAddressName.Rows)
                    {
                        Label lblGvAddressName = (Label)gvr.FindControl("lblgvAddressName");

                        if (lblGvAddressName.Text != "")
                        {
                            DataTable dtAddId = AM.GetAddressDataByAddressName(strMaxId, lblGvAddressName.Text);
                            if (dtAddId.Rows.Count > 0)
                            {
                                string strAddressId = dtAddId.Rows[0]["Trans_Id"].ToString();
                                objAddChild.InsertAddressChild(strAddressId, "Company", strMaxId, "True", "", "", "", "", "", "True", DateTime.Now.ToString(), "True", Session["UserId"].ToString(), DateTime.Now.ToString(), Session["UserId"].ToString(), DateTime.Now.ToString());
                            }
                        }
                    }
                
                //

                   DataTable dtParam = objAppParam.GetApplicationParameterByCompanyId("","1");
                if(dtParam.Rows.Count > 0)
                {
                    for (int i = 0; i < dtParam.Rows.Count;i++)
                    {
                        objAppParam.InsertApplicationParameterMaster(b.ToString(), dtParam.Rows[i]["Param_Name"].ToString(), dtParam.Rows[i]["Param_Value"].ToString(), dtParam.Rows[i]["Param_Cat_Id"].ToString(), dtParam.Rows[i]["Description"].ToString(), true.ToString(), Session["UserId"].ToString(), DateTime.Now.ToString(), Session["UserId"].ToString(), DateTime.Now.ToString());

                    }

                }

                DataTable dtParam1 = objDeviceParam.GetDeviceParameterByCompanyId("", "1");
                if (dtParam1.Rows.Count > 0)
                {
                    for (int i = 0; i < dtParam1.Rows.Count; i++)
                    {
                        objDeviceParam.InsertDeviceParameterMaster(b.ToString(),"0","0", dtParam1.Rows[i]["Param_Name"].ToString(), dtParam1.Rows[i]["Param_Value"].ToString(),dtParam1.Rows[i]["Param_Description"].ToString(), true.ToString(), Session["UserId"].ToString(), DateTime.Now.ToString(), Session["UserId"].ToString(), DateTime.Now.ToString());

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

          
            string CompanyCode = string.Empty;
            string CompanyName = string.Empty;

            DataTable dt = objComp.GetCompanyMaster();

            try
            {
                CompanyCode = (new DataView(dt, "Company_Id='" + editid.Value + "'", "", DataViewRowState.CurrentRows).ToTable()).Rows[0]["Company_Code"].ToString();
            }
            catch
            {
                CompanyCode = "";
            }


            dt = new DataView(dt, "Company_Code='" + txtCompanyCode.Text + "' and Company_Code <>'"+CompanyCode+"'  ", "", DataViewRowState.CurrentRows).ToTable();
            if (dt.Rows.Count > 0)
            {
                DisplayMessage("Company Code Already Exists");
                txtCompanyCode.Focus();
                return;

            }
            DataTable dt1 = objComp.GetCompanyMaster();
            try
            {
                CompanyName = (new DataView(dt1, "Company_Id='" + editid.Value + "'", "", DataViewRowState.CurrentRows).ToTable()).Rows[0]["Company_Name"].ToString();
            }
            catch
            {
                CompanyName = "";
            }
            dt1 = new DataView(dt1, "Company_Name='" + txtCompanyName.Text + "' and Company_Name<>'"+CompanyName+"'", "", DataViewRowState.CurrentRows).ToTable();
            if (dt1.Rows.Count > 0)
            {
                DisplayMessage("Company Name Already Exists");
                txtCompanyName.Focus();
                return;

            }
            if (ddlCountry1.SelectedIndex == 0)
            {

                DisplayMessage("Select Country");
                ddlCountry1.Focus();
                return;

            }
            if (ddlCurrency.SelectedIndex == 0)
            {

                DisplayMessage("Select Currency");
                ddlCurrency.Focus();
                return;

            }
            b = objComp.UpdateCompanyMaster(editid.Value, txtCompanyName.Text, txtCompanyNameL.Text, txtCompanyCode.Text, Session["empimgpath"].ToString(), ddlParentCompany.SelectedValue,empid, ddlCurrency.SelectedValue, ddlCountry1.SelectedValue, txtLicenceNo.Text, "", "", "", "", "", false.ToString(), DateTime.Now.ToString(), true.ToString(), Session["UserId"].ToString(), DateTime.Now.ToString());

            if (b != 0)
            {
               
                objAddChild.DeleteAddressChild("Company", editid.Value);
                foreach (GridViewRow gvr in GvAddressName.Rows)
                {
                    Label lblGvAddressName = (Label)gvr.FindControl("lblgvAddressName");

                    if (lblGvAddressName.Text != "")
                    {
                        DataTable dtAddId = AM.GetAddressDataByAddressName(editid.Value, lblGvAddressName.Text);
                        if (dtAddId.Rows.Count > 0)
                        {
                            string strAddressId = dtAddId.Rows[0]["Trans_Id"].ToString();
                            objAddChild.InsertAddressChild(strAddressId, "Company", editid.Value, "True", "", "", "", "", "", "True", DateTime.Now.ToString(), "True", Session["UserId"].ToString(), DateTime.Now.ToString(), Session["UserId"].ToString(), DateTime.Now.ToString());
                        }
                    }
                }

                DataTable dtParam = objAppParam.GetApplicationParameterByCompanyId("",editid.Value);
                if (dtParam.Rows.Count == 0)
                {
                    dtParam = objAppParam.GetApplicationParameterByCompanyId("","1");
                    for (int i = 0; i < dtParam.Rows.Count; i++)
                    {
                        objAppParam.InsertApplicationParameterMaster(editid.Value, dtParam.Rows[i]["Param_Name"].ToString(), dtParam.Rows[i]["Param_Value"].ToString(), dtParam.Rows[i]["Param_Cat_Id"].ToString(), dtParam.Rows[i]["Description"].ToString(), true.ToString(), Session["UserId"].ToString(), DateTime.Now.ToString(), Session["UserId"].ToString(), DateTime.Now.ToString());

                    }

                }
                DataTable dtParam1 = objDeviceParam.GetDeviceParameterByCompanyId("", editid.Value);
                if (dtParam1.Rows.Count == 0)
                {
                    dtParam1 = objDeviceParam.GetDeviceParameterByCompanyId("", "1");

                    objDeviceParam.DeleteDeviceParameter(editid.Value);

                    for (int i = 0; i < dtParam1.Rows.Count; i++)
                    {
                        objDeviceParam.InsertDeviceParameterMaster(editid.Value, Session["BrandId"].ToString(), Session["LocId"].ToString(), dtParam1.Rows[i]["Param_Name"].ToString(), dtParam1.Rows[i]["Param_Value"].ToString(), dtParam1.Rows[i]["Param_Description"].ToString(), true.ToString(), Session["UserId"].ToString(), DateTime.Now.ToString(), Session["UserId"].ToString(), DateTime.Now.ToString());

                    }

                }
                else
                {
                    dtParam1 = objDeviceParam.GetDeviceParameterByCompanyId("", "1");

                    objDeviceParam.DeleteDeviceParameter(editid.Value);

                    for (int i = 0; i < dtParam1.Rows.Count; i++)
                    {
                        objDeviceParam.InsertDeviceParameterMaster(editid.Value, Session["BrandId"].ToString(), Session["LocId"].ToString(), dtParam1.Rows[i]["Param_Name"].ToString(), dtParam1.Rows[i]["Param_Value"].ToString(), dtParam1.Rows[i]["Param_Description"].ToString(), true.ToString(), Session["UserId"].ToString(), DateTime.Now.ToString(), Session["UserId"].ToString(), DateTime.Now.ToString());

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
        Session["EditValue"] = null ;

    }
    protected void btnCancel_Click(object sender, EventArgs e)
    {
        FillGrid();
        FillGridBin();
        Reset();
        btnList_Click(null, null);
        Session["EditValue"] = null;
    }

    [System.Web.Services.WebMethodAttribute(), System.Web.Script.Services.ScriptMethodAttribute()]
    public static string[] GetCompletionListCompanyCode(string prefixText, int count, string contextKey)
    {
        CompanyMaster ObjCompanyMaster = new CompanyMaster();
        DataTable dt = new DataView(ObjCompanyMaster.GetCompanyMaster(), "Company_Code like '" + prefixText.ToString() + "%'", "", DataViewRowState.CurrentRows).ToTable();
        string[] txt = new string[dt.Rows.Count];

        for (int i = 0; i < dt.Rows.Count; i++)
        {
            txt[i] = dt.Rows[i]["Company_Code"].ToString();
        }
        return txt;
    }
    [System.Web.Services.WebMethodAttribute(), System.Web.Script.Services.ScriptMethodAttribute()]
    public static string[] GetCompletionListCompanyName(string prefixText, int count, string contextKey)
    {
        CompanyMaster ObjCompanyMaster = new CompanyMaster();
        DataTable dt = new DataView(ObjCompanyMaster.GetCompanyMaster(), "Company_Name like '" + prefixText.ToString() + "%'", "", DataViewRowState.CurrentRows).ToTable();
        string[] txt = new string[dt.Rows.Count];

        for (int i = 0; i < dt.Rows.Count; i++)
        {
            txt[i] = dt.Rows[i]["Company_Name"].ToString();
        }
        return txt;
    }

    public void Reset()
    {

        FillCountryDDL();
        FillCurrencyDDL();
        FillddlParentCompanyDDL();
        txtCompanyCode.Text = "";
        txtCompanyName.Text = "";
        txtCompanyNameL.Text = "";
        txtLicenceNo.Text = "";
        txtManagerName.Text = "";
        imgLogo.ImageUrl = "";

        
        
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
        ddlAddressCategory.DataSource = null;
        ddlAddressCategory.DataBind();
        ddlAddressCategory.Items.Clear();
        System.Web.UI.ScriptManager.GetCurrent(this).SetFocus(txtValue);
    }
    protected void FULogoPath_UploadedComplete(object sender, AjaxControlToolkit.AsyncFileUploadEventArgs e)
    {

       
            string compid=(int.Parse(objComp.GetMaxCompanyId())+1).ToString();
        if(editid.Value!="")
        {
            compid = editid.Value;

        }
        
        if (FULogoPath.HasFile)
        {
            if (!Directory.Exists(Server.MapPath("~/CompanyResource/") + compid))
            {
                Directory.CreateDirectory(Server.MapPath("~/CompanyResource/") + compid);
            }


            string path = Server.MapPath("~/CompanyResource/" + "/" + compid + "/") + FULogoPath.FileName;
            FULogoPath.SaveAs(path);
             Session["empimgpath"] = FULogoPath.FileName;
                }
    }

    protected void btnUpload_Click(object sender, EventArgs e)
    {
        string compid = (int.Parse(objComp.GetMaxCompanyId()) + 1).ToString();
        if (editid.Value != "")
        {
            compid = editid.Value;

        }
        imgLogo.ImageUrl = "~/CompanyResource/" + "/" + compid + "/" + FULogoPath.FileName;
            
    }
    public void FillGrid()
    {
        DataTable dt = objComp.GetCompanyMaster();
         gvCompanyMaster.DataSource = dt;
        gvCompanyMaster.DataBind();
        Session["dtFilter"] = dt;
        Session["Company"] = dt;

        lblTotalRecords.Text = Resources.Attendance.Total_Records + ": " + dt.Rows.Count.ToString() + "";
        AllPageCode();
    }
    public void FillCountryDDL()
    {
        DataTable dt = objCountry.GetCountryMaster();

       

        if (dt.Rows.Count > 0)
        {
            ddlCountry1.DataSource = null;
            ddlCountry1.DataBind();
            ddlCountry1.DataSource = dt;
            ddlCountry1.DataTextField = "Country_Name";
            ddlCountry1.DataValueField = "Country_Id";
            ddlCountry1.DataBind();

            ListItem li = new ListItem("--Select--", "0");
            ddlCountry1.Items.Insert(0, li);
            ddlCountry1.SelectedIndex = 0;

        }
        else
        {
            try
            {
                ddlCountry1.Items.Clear();
                ddlCountry1.DataSource = null;
                ddlCountry1.DataBind();
                ListItem li = new ListItem("--Select--", "0");
                ddlCountry1.Items.Insert(0, li);
                ddlCountry1.SelectedIndex = 0;
            }
            catch
            {
                ListItem li = new ListItem("--Select--", "0");
                ddlCountry1.Items.Insert(0, li);
                ddlCountry1.SelectedIndex = 0;

            }
        }

    }


    public void FillCurrencyDDL()
    {
        DataTable dt = objCurrency.GetCurrencyMaster();



        if (dt.Rows.Count > 0)
        {
            ddlCurrency.DataSource = null;
            ddlCurrency.DataBind();
            ddlCurrency.DataSource = dt;
            ddlCurrency.DataTextField = "Currency_Name";
            ddlCurrency.DataValueField = "Currency_Id";
            ddlCurrency.DataBind();
            ListItem li = new ListItem("--Select--", "0");
            ddlCurrency.Items.Insert(0,li);
            ddlCurrency.SelectedIndex = 0;

        }
        else
        {
            try
            {
                ddlCurrency.Items.Clear();
                ddlCurrency.DataSource = null;
                ddlCurrency.DataBind();
                ListItem li = new ListItem("--Select--", "0");
                ddlCurrency.Items.Insert(0, li);
                ddlCurrency.SelectedIndex = 0;
            }
            catch
            {
                ListItem li = new ListItem("--Select--", "0");
                ddlCurrency.Items.Insert(0, li);
                ddlCurrency.SelectedIndex = 0;

            }
        }

    }

    public void FillddlParentCompanyDDL()
    {
        DataTable dt = objComp.GetCompanyMaster();



        if (dt.Rows.Count > 0)
        {
            ddlParentCompany.DataSource = null;
            ddlParentCompany.DataBind();
            ddlParentCompany.DataSource = dt;
            ddlParentCompany.DataTextField = "Company_Name";
            ddlParentCompany.DataValueField = "Company_Id";
            ddlParentCompany.DataBind();
            ListItem li = new ListItem("--Select--", "0");
            ddlParentCompany.Items.Insert(0,li);
            ddlParentCompany.SelectedIndex = 0;

        }
        else
        {
            try
            {
                ddlParentCompany.Items.Clear();
                ddlParentCompany.DataSource = null;
                ddlParentCompany.DataBind();
                ListItem li = new ListItem("--Select--", "0");
                ddlParentCompany.Items.Insert(0, li);
                ddlParentCompany.SelectedIndex = 0;
            }
            catch
            {
                ListItem li = new ListItem("--Select--", "0");
                ddlParentCompany.Items.Insert(0, li);
                ddlParentCompany.SelectedIndex = 0;

            }
        }
        
    }

    
     public void FillGridBin()
    {

        DataTable dt = new DataTable();
        dt = objComp.GetCompanyMasterInactive();
       
        gvCompanyMasterBin.DataSource = dt;
        gvCompanyMasterBin.DataBind();
         
    
        Session["dtbinFilter"] = dt;
        Session["dtbinCompany"] = dt;
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

     protected void chkgvSelectAll_CheckedChanged(object sender, EventArgs e)
     {
         CheckBox chkSelAll = ((CheckBox)gvCompanyMasterBin.HeaderRow.FindControl("chkgvSelectAll"));
         for (int i = 0; i < gvCompanyMasterBin.Rows.Count; i++)
         {
             ((CheckBox)gvCompanyMasterBin.Rows[i].FindControl("chkgvSelect")).Checked = chkSelAll.Checked;
             if (chkSelAll.Checked)
             {
                 if (!lblSelectedRecord.Text.Split(',').Contains(((Label)(gvCompanyMasterBin.Rows[i].FindControl("lblCompanyId"))).Text.Trim().ToString()))
                 {
                     lblSelectedRecord.Text += ((Label)(gvCompanyMasterBin.Rows[i].FindControl("lblCompanyId"))).Text.Trim().ToString() + ",";
                 }
             }
             else
             {
                 string temp = string.Empty;
                 string[] split = lblSelectedRecord.Text.Split(',');
                 foreach (string item in split)
                 {
                     if (item != ((Label)(gvCompanyMasterBin.Rows[i].FindControl("lblCompanyId"))).Text.Trim().ToString())
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
         Label lb = (Label)gvCompanyMasterBin.Rows[index].FindControl("lblCompanyId");
         if (((CheckBox)gvCompanyMasterBin.Rows[index].FindControl("chkgvSelect")).Checked)
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
     protected void ImgbtnSelectAll_Click(object sender, ImageClickEventArgs e)
     {
         DataTable dtUnit = (DataTable)Session["dtbinFilter"];

         if (ViewState["Select"] == null)
         {
             ViewState["Select"] = 1;
             foreach (DataRow dr in dtUnit.Rows)
             {
                 if (!lblSelectedRecord.Text.Split(',').Contains(dr["Company_Id"]))
                 {
                     lblSelectedRecord.Text += dr["Company_Id"] + ",";
                 }
             }
             for (int i = 0; i < gvCompanyMasterBin.Rows.Count; i++)
             {
                 string[] split = lblSelectedRecord.Text.Split(',');
                 Label lblconid = (Label)gvCompanyMasterBin.Rows[i].FindControl("lblCompanyId");
                 for (int j = 0; j < lblSelectedRecord.Text.Split(',').Length; j++)
                 {
                     if (lblSelectedRecord.Text.Split(',')[j] != "")
                     {
                         if (lblconid.Text.Trim().ToString() == lblSelectedRecord.Text.Split(',')[j].Trim().ToString())
                         {
                             ((CheckBox)gvCompanyMasterBin.Rows[i].FindControl("chkgvSelect")).Checked = true;
                         }
                     }
                 }
             }
         }
         else
         {
             lblSelectedRecord.Text = "";
             DataTable dtUnit1 = (DataTable)Session["dtBinFilter"];
             gvCompanyMasterBin.DataSource = dtUnit1;
             gvCompanyMasterBin.DataBind();
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
                     b = objComp.DeleteCompanyMaster(lblSelectedRecord.Text.Split(',')[j].Trim(), true.ToString(), Session["UserId"].ToString().ToString(), DateTime.Now.ToString());

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
             foreach (GridViewRow Gvr in gvCompanyMasterBin.Rows)
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

     protected void btnAddNewAddress_Click(object sender, EventArgs e)
     {

         if(editid.Value=="")
         {
             DisplayMessage("First save company then add address!");
             return;

         }
         pnlAddress1.Visible = true;
         pnlAddress2.Visible = true;
         FillAddressCategory();
         FillCountry();
     }
     protected void txtAddressNameNew_TextChanged(object sender, EventArgs e)
     {
         DataTable dt = AM.GetAddressDataByAddressName("0", txtAddressNameNew.Text);
         if (dt.Rows.Count > 0)
         {
             txtAddressNameNew.Text = "";
             DisplayMessage("Address Name Already Exists");
             System.Web.UI.ScriptManager.GetCurrent(this).SetFocus(txtAddressNameNew);
             return;
         }

         DataTable dt1 = AM.GetAddressFalseAllData("0");
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

         DataTable dtadd2 = AM.GetAddressDataByAddressName("0", txtAddressName.Text);
         if (dtadd2.Rows.Count > 0)
         {
             txtAddressName.Text = "";
             DisplayMessage("Address Name Already Exists");
             System.Web.UI.ScriptManager.GetCurrent(this).SetFocus(txtAddressName);
             return;
         }
         else
         {
             b = AM.InsertAddressMaster( Session["EditValue"].ToString(),ddlAddressCategory.SelectedValue, txtAddressNameNew.Text, txtAddress.Text, txtStreet.Text, txtBlock.Text, txtAvenue.Text, strCountryId, txtState.Text, txtCity.Text, txtPinCode.Text, txtPhoneNo1.Text, txtPhoneNo2.Text, txtMobileNo1.Text, txtMobileNo2.Text, txtEmailId1.Text, txtEmailId2.Text, txtFaxNo.Text, txtWebsite.Text, strLongitude, strLatitude, "", "", "", "", "", "True", DateTime.Now.ToString(), "True", Session["UserId"].ToString(), DateTime.Now.ToString(), Session["UserId"].ToString(), DateTime.Now.ToString());
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
         return System.Text.RegularExpressions.Regex.IsMatch(txtEmailId1.Text,
                       "\\w+([-+.']\\+)*@\\w+([-.]\\+)*\\.\\w+([-.]\\+)*");
     }
     public bool CheckEmailId2(string EmailAddress)
     {
         return System.Text.RegularExpressions.Regex.IsMatch(txtEmailId2.Text,
                       "\\w+([-+.']\\+)*@\\w+([-.]\\+)*\\.\\w+([-.]\\+)*");
     }
     private void FillAddressCategory()
     {
         DataTable dsAddressCat = null;
         ddlAddressCategory.Items.Clear();
         dsAddressCat = ObjAddressCat.GetAddressCategoryAll(Session["EditValue"].ToString());
         if (dsAddressCat.Rows.Count > 0)
         {
             ddlAddressCategory.DataSource = dsAddressCat;
             ddlAddressCategory.DataTextField = "Address_Name";
             ddlAddressCategory.DataValueField = "Address_Category_Id";
             ddlAddressCategory.DataBind();
             ListItem li = new ListItem("--Select--", "0");
             ddlAddressCategory.Items.Insert(0,li);
             ddlAddressCategory.SelectedIndex = 0;
         }
         else
         {
             ddlAddressCategory.DataSource = null;
           
             ddlAddressCategory.Items.Add("--Select--");
             ddlAddressCategory.DataBind();
            
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
     protected void txtAddressName_TextChanged(object sender, EventArgs e)
     {
         if (Session["EditValue"]==null)
         {
             DisplayMessage("First Save company then add address");
             return;

         }

         if (txtAddressName.Text != "") 
         {
             DataTable dtAM = AM.GetAddressDataByAddressName(Session["EditValue"].ToString(), txtAddressName.Text);
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
     protected void btnAddressDelete_Command(object sender, CommandEventArgs e)
     {
         hdnAddressId.Value = e.CommandArgument.ToString();
         FillAddressChidGird("Del");
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
 [System.Web.Services.WebMethodAttribute(), System.Web.Script.Services.ScriptMethodAttribute()]
    public static string[] GetCompletionListAddressName(string prefixText, int count, string contextKey)
    {
        Set_AddressMaster AddressN = new Set_AddressMaster();
        DataTable dt = AddressN.GetDistinctAddressName(HttpContext.Current.Session["EditValue"].ToString(), prefixText);



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
                dt = AddressN.GetAddressAllData("0");
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
        DataTable dt = Address.GetDistinctAddressName(HttpContext.Current.Session["EditValue"].ToString(), prefixText);

        string[] str = new string[dt.Rows.Count];
        for (int i = 0; i < dt.Rows.Count; i++)
        {
            str[i] = dt.Rows[i]["Address_Name"].ToString();
        }
        return str;
    }
   

   
  
}
