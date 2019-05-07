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
using System.Text.RegularExpressions;

public partial class MasterSetUp_BrandMaster : BasePage
{
 
    

    BrandMaster objBrand = new BrandMaster();
    Common cmn = new Common();
    SystemParameter objSys = new SystemParameter();
    Set_AddressMaster AM = new Set_AddressMaster();
    Set_AddressCategory ObjAddressCat = new Set_AddressCategory();
    Set_AddressChild objAddChild = new Set_AddressChild();
    EmployeeMaster objEmp = new EmployeeMaster();

   
    protected void Page_Load(object sender, EventArgs e)
    {
    
      
        //End Remove

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
            
            btnList_Click(null, null);

        }
    }

    public void AllPageCode()
    {
        Page.Title = objSys.GetSysTitle();

        Session["AccordianId"] = "8";
        Session["HeaderText"] = "Master Setup";
        DataTable dtAllPageCode = cmn.GetAllPagePermission(Session["UserId"].ToString(), "8", "12");
        if (dtAllPageCode.Rows.Count != 0)
        {
            if (dtAllPageCode.Rows[0][0].ToString() == "SuperAdmin")
            {
                btnSave.Visible = true;
                foreach (GridViewRow Row in gvBrandMaster.Rows)
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
                    foreach (GridViewRow Row in gvBrandMaster.Rows)
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

        string[] str = new string[dt.Rows.Count];
        for (int i = 0; i < dt.Rows.Count; i++)
        {
            str[i] = "" + dt.Rows[i][1].ToString() + "/(" + dt.Rows[i][2].ToString() + ")/" + dt.Rows[i][0].ToString() + "";
        }
        return str;
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
         txtBrandCode.Focus();
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
            DataTable dtCust = (DataTable)Session["Brand"];


            DataView view = new DataView(dtCust, condition, "", DataViewRowState.CurrentRows);
            Session["dtFilter"] = view.ToTable();
            lblTotalRecords.Text = Resources.Attendance.Total_Records + ": " + view.ToTable().Rows.Count.ToString() + "";

            gvBrandMaster.DataSource = view.ToTable();
            gvBrandMaster.DataBind();

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
            DataTable dtCust = (DataTable)Session["dtbinBrand"];


            DataView view = new DataView(dtCust, condition, "", DataViewRowState.CurrentRows);
            Session["dtbinFilter"] = view.ToTable();
            lblbinTotalRecords.Text = Resources.Attendance.Total_Records + ": " + view.ToTable().Rows.Count.ToString() + "";

            gvBrandMasterBin.DataSource = view.ToTable();
            gvBrandMasterBin.DataBind();


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

    protected void gvBrandMasterBin_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {

        gvBrandMasterBin.PageIndex = e.NewPageIndex;
        if (HDFSortbin.Value == "")
            FillGridBin();
        else
        {
            DataTable dt = (DataTable)Session["dtbinFilter"];
            gvBrandMasterBin.DataSource = dt;
            gvBrandMasterBin.DataBind();
            AllPageCode();
        }
        string temp = string.Empty;
        bool isselcted;

        for (int i = 0; i < gvBrandMasterBin.Rows.Count; i++)
        {
            Label lblconid = (Label)gvBrandMasterBin.Rows[i].FindControl("lblBrandId");
            string[] split = lblSelectedRecord.Text.Split(',');

            for (int j = 0; j < lblSelectedRecord.Text.Split(',').Length; j++)
            {
                if (lblSelectedRecord.Text.Split(',')[j] != "")
                {
                    if (lblconid.Text.Trim().ToString() == lblSelectedRecord.Text.Split(',')[j].Trim().ToString())
                    {
                        ((CheckBox)gvBrandMasterBin.Rows[i].FindControl("chkgvSelect")).Checked = true;
                    }
                }
            }
        }

    }
    protected void gvBrandMasterBin_OnSorting(object sender, GridViewSortEventArgs e)
    {
        lblSelectedRecord.Text = "";
        HDFSortbin.Value = HDFSortbin.Value == "ASC" ? "DESC" : "ASC";
        DataTable dt = new DataTable();
        dt = objBrand.GetBrandMasterInactive(Session["CompId"].ToString());
        DataView dv = new DataView(dt);
        string Query = "" + e.SortExpression + " " + HDFSortbin.Value + "";
        dv.Sort = Query;
        dt = dv.ToTable();
        Session["dtbinFilter"] = dt;
        gvBrandMasterBin.DataSource = dt;
        gvBrandMasterBin.DataBind();
        AllPageCode();

    }
    

   
    protected void btnSave_Click(object sender, EventArgs e)
    {
        int b = 0;

        if (txtBrandCode.Text == "")
        {
            DisplayMessage("Enter Brand Code");
            txtBrandCode.Focus();
            return;
        }

        if (txtBrandName.Text == "")
        {
            DisplayMessage("Enter Brand Name");
            txtBrandName.Focus();
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
            DataTable dt = objBrand.GetBrandMaster(Session["CompId"].ToString());

            dt = new DataView(dt, "Brand_Code='" + txtBrandCode.Text + "'", "", DataViewRowState.CurrentRows).ToTable();
            if (dt.Rows.Count > 0)
            {
                DisplayMessage("Brand Code Already Exists");
                txtBrandCode.Focus();
                return;

            }
            DataTable dt1 = objBrand.GetBrandMaster(Session["CompId"].ToString());

            dt1 = new DataView(dt1, "Brand_Name='" + txtBrandName.Text + "'", "", DataViewRowState.CurrentRows).ToTable();
            if (dt1.Rows.Count > 0)
            {
                DisplayMessage("Brand Name Already Exists");
                txtBrandName.Focus();
                return;

            }


          

            b = objBrand.InsertBrandMaster(Session["CompId"].ToString(), txtBrandName.Text, txtBrandNameL.Text, txtBrandCode.Text,empid, Session["empimgpath"].ToString(),"", "", "", "", "", false.ToString(), DateTime.Now.ToString(), true.ToString(), Session["UserId"].ToString(), DateTime.Now.ToString(), Session["UserId"].ToString(), DateTime.Now.ToString());

            if (b != 0)
            {
                string strMaxId=string.Empty;
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
                            objAddChild.InsertAddressChild(strAddressId, "Brand", strMaxId, "True", "", "", "", "", "", "True", DateTime.Now.ToString(), "True", Session["UserId"].ToString(), DateTime.Now.ToString(), Session["UserId"].ToString(), DateTime.Now.ToString());
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
            DataTable dt = objBrand.GetBrandMaster(Session["CompId"].ToString());

             
            string BrandCode=string.Empty;
            string BrandName = string.Empty;

            try
            {
                BrandCode = (new DataView(dt, "Brand_Id='" + editid.Value + "'", "", DataViewRowState.CurrentRows).ToTable()).Rows[0]["Brand_Code"].ToString();
            }
            catch
            {
                BrandCode = "";
            }

             dt = new DataView(dt, "Brand_Code='" + txtBrandCode.Text + "' and Brand_Code<>'"+BrandCode+"' ", "", DataViewRowState.CurrentRows).ToTable();

             if (dt.Rows.Count > 0)
             {
                 DisplayMessage("Brand Code Already Exists");
                 txtBrandCode.Focus();
                 return;

             }

           
            
            DataTable dt1 = objBrand.GetBrandMaster(Session["CompId"].ToString());
            try
            {
                BrandName = (new DataView(dt1, "Brand_Id='" + editid.Value + "'", "", DataViewRowState.CurrentRows).ToTable()).Rows[0]["Brand_Name"].ToString();
            }
            catch
            {
                BrandName = "";
            }
             dt1 = new DataView(dt1, "Brand_Name='" + txtBrandName.Text + "' and Brand_Name<>'"+BrandName+"' ", "", DataViewRowState.CurrentRows).ToTable();
             if (dt1.Rows.Count > 0)
             {
                 DisplayMessage("Brand Name Already Exists");
                 txtBrandName.Focus();
                 return;

             }
            
            
            b = objBrand.UpdateBrandMaster(Session["CompId"].ToString(),editid.Value,txtBrandName.Text, txtBrandNameL.Text, txtBrandCode.Text,empid, Session["empimgpath"].ToString(), "", "", "", "", "", false.ToString(), DateTime.Now.ToString(), true.ToString(), Session["UserId"].ToString(), DateTime.Now.ToString());

            if (b != 0)
            {


                objAddChild.DeleteAddressChild("Brand", editid.Value);
                foreach (GridViewRow gvr in GvAddressName.Rows)
                {
                    Label lblGvAddressName = (Label)gvr.FindControl("lblgvAddressName");

                    if (lblGvAddressName.Text != "")
                    {
                        DataTable dtAddId = AM.GetAddressDataByAddressName(Session["CompId"].ToString(), lblGvAddressName.Text);
                        if (dtAddId.Rows.Count > 0)
                        {
                            string strAddressId = dtAddId.Rows[0]["Trans_Id"].ToString();
                            objAddChild.InsertAddressChild(strAddressId, "Brand", editid.Value, "True", "", "", "", "", "", "True", DateTime.Now.ToString(), "True", Session["UserId"].ToString(), DateTime.Now.ToString(), Session["UserId"].ToString(), DateTime.Now.ToString());
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



    protected void btnEdit_Command(object sender, CommandEventArgs e)
    {
        editid.Value = e.CommandArgument.ToString();
       

        DataTable dt = objBrand.GetBrandMasterById(Session["CompId"].ToString(),editid.Value);
        if (dt.Rows.Count > 0)
        {
            txtBrandCode.Text = dt.Rows[0]["Brand_Code"].ToString();
            txtBrandName.Text = dt.Rows[0]["Brand_Name"].ToString();
            txtBrandNameL.Text = dt.Rows[0]["Brand_Name_L"].ToString();


            if (dt.Rows[0]["Emp_Id"].ToString().Trim() != "0" || dt.Rows[0]["Emp_Id"].ToString().Trim() != "")
            {
            txtManagerName.Text = cmn.GetEmpName(dt.Rows[0]["Emp_Id"].ToString());
            }
            else
            {
                txtManagerName.Text = "";

            }



            imgLogo.ImageUrl = "~/CompanyResource/" + "/" + Session["CompId"].ToString() + "/" + dt.Rows[0]["Logo_Path"].ToString();

            Session["empimgpath"] = dt.Rows[0]["Logo_Path"].ToString();


            DataTable dtChild = objAddChild.GetAddressChildDataByAddTypeAndAddRefId("Brand", editid.Value);
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
        b = objBrand.DeleteBrandMaster(Session["CompId"].ToString(),e.CommandArgument.ToString(), false.ToString(), Session["UserId"].ToString(), DateTime.Now.ToString());
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
    protected void gvBrandMaster_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        gvBrandMaster.PageIndex = e.NewPageIndex;

        DataTable dt = (DataTable)Session["dtFilter"];
        gvBrandMaster.DataSource = dt;
        gvBrandMaster.DataBind();
        AllPageCode();

    }
    protected void gvBrandMaster_OnSorting(object sender, GridViewSortEventArgs e)
    {
        HDFSort.Value = HDFSort.Value == "ASC" ? "DESC" : "ASC";
        DataTable dt = new DataTable();
        dt = (DataTable)Session["dtFilter"];
        DataView dv = new DataView(dt);
        string Query = "" + e.SortExpression + " " + HDFSort.Value + "";
        dv.Sort = Query;
        dt = dv.ToTable();
        Session["dtFilter"] = dt;
        gvBrandMaster.DataSource = dt;
        gvBrandMaster.DataBind();
        AllPageCode();
    }

    [System.Web.Services.WebMethodAttribute(), System.Web.Script.Services.ScriptMethodAttribute()]
    public static string[] GetCompletionListBrandCode(string prefixText, int count, string contextKey)
    {
        BrandMaster objBrandMaster = new BrandMaster();
        DataTable dt = new DataView(objBrandMaster.GetBrandMaster(HttpContext.Current.Session["CompId"].ToString()), "Brand_Code like '" + prefixText.ToString() + "%'", "", DataViewRowState.CurrentRows).ToTable();
        string[] txt = new string[dt.Rows.Count];

        for (int i = 0; i < dt.Rows.Count; i++)
        {
            txt[i] = dt.Rows[i]["Brand_Code"].ToString();
        }
        return txt;
    }
    [System.Web.Services.WebMethodAttribute(), System.Web.Script.Services.ScriptMethodAttribute()]
    public static string[] GetCompletionListBrandName(string prefixText, int count, string contextKey)
    {
        BrandMaster objBrandMaster = new BrandMaster();
        DataTable dt = new DataView(objBrandMaster.GetBrandMaster(HttpContext.Current.Session["CompId"].ToString()), "Brand_Name like '" + prefixText.ToString() + "%'", "", DataViewRowState.CurrentRows).ToTable();
        string[] txt = new string[dt.Rows.Count];

        for (int i = 0; i < dt.Rows.Count; i++)
        {
            txt[i] = dt.Rows[i]["Brand_Name"].ToString();
        }
        return txt;
    }

    protected void FULogoPath_UploadedComplete(object sender, AjaxControlToolkit.AsyncFileUploadEventArgs e)
    {

        if (Session["CompId"] == null)
        {
            string compid = (int.Parse(objBrand.GetMaxBrandId(Session["CompId"].ToString())) + 1).ToString();
            Session["CompId"] = compid;
        }
        if (FULogoPath.HasFile)
        {
            if (!Directory.Exists(Server.MapPath("~/CompanyResource/") + Session["CompId"]))
            {
                Directory.CreateDirectory(Server.MapPath("~/CompanyResource/") + Session["CompId"]);
            }


            string path = Server.MapPath("~/CompanyResource/" + "/" + Session["CompId"] + "/") + FULogoPath.FileName;
            FULogoPath.SaveAs(path);
            Session["empimgpath"] = FULogoPath.FileName;
        }
    }


    public void DisplayMessage(string str)
    {
        ScriptManager.RegisterStartupScript(this, GetType(), "", "alert('" + str + "');", true);
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

    public void FillGrid()
    {
        DataTable dt = objBrand.GetBrandMaster(Session["CompId"].ToString());

        gvBrandMaster.DataSource = dt;
        gvBrandMaster.DataBind();
        AllPageCode();
        Session["dtFilter"] = dt;
        Session["Brand"] = dt;

        lblTotalRecords.Text = Resources.Attendance.Total_Records + ": " + dt.Rows.Count.ToString() + "";

    }
    public void FillGridBin()
    {
        DataTable dt = new DataTable();
        dt = objBrand.GetBrandMasterInactive(Session["CompId"].ToString());

        gvBrandMasterBin.DataSource = dt;
        gvBrandMasterBin.DataBind();


        Session["dtbinFilter"] = dt;
        Session["dtbinBrand"] = dt;
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
        CheckBox chkSelAll = ((CheckBox)gvBrandMasterBin.HeaderRow.FindControl("chkgvSelectAll"));
        for (int i = 0; i < gvBrandMasterBin.Rows.Count; i++)
        {
            ((CheckBox)gvBrandMasterBin.Rows[i].FindControl("chkgvSelect")).Checked = chkSelAll.Checked;
            if (chkSelAll.Checked)
            {
                if (!lblSelectedRecord.Text.Split(',').Contains(((Label)(gvBrandMasterBin.Rows[i].FindControl("lblBrandId"))).Text.Trim().ToString()))
                {
                    lblSelectedRecord.Text += ((Label)(gvBrandMasterBin.Rows[i].FindControl("lblBrandId"))).Text.Trim().ToString() + ",";
                }
            }
            else
            {
                string temp = string.Empty;
                string[] split = lblSelectedRecord.Text.Split(',');
                foreach (string item in split)
                {
                    if (item != ((Label)(gvBrandMasterBin.Rows[i].FindControl("lblBrandId"))).Text.Trim().ToString())
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
        Label lb = (Label)gvBrandMasterBin.Rows[index].FindControl("lblBrandId");
        if (((CheckBox)gvBrandMasterBin.Rows[index].FindControl("chkgvSelect")).Checked)
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
                if (!lblSelectedRecord.Text.Split(',').Contains(dr["Brand_Id"]))
                {
                    lblSelectedRecord.Text += dr["Brand_Id"] + ",";
                }
            }
            for (int i = 0; i < gvBrandMasterBin.Rows.Count; i++)
            {
                string[] split = lblSelectedRecord.Text.Split(',');
                Label lblconid = (Label)gvBrandMasterBin.Rows[i].FindControl("lblBrandId1");
                for (int j = 0; j < lblSelectedRecord.Text.Split(',').Length; j++)
                {
                    if (lblSelectedRecord.Text.Split(',')[j] != "")
                    {
                        if (lblconid.Text.Trim().ToString() == lblSelectedRecord.Text.Split(',')[j].Trim().ToString())
                        {
                            ((CheckBox)gvBrandMasterBin.Rows[i].FindControl("chkgvSelect")).Checked = true;
                        }
                    }
                }
            }
        }
        else
        {
            lblSelectedRecord.Text = "";
            DataTable dtUnit1 = (DataTable)Session["dtBinFilter"];
            gvBrandMasterBin.DataSource = dtUnit1;
            gvBrandMasterBin.DataBind();
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
                    b = objBrand.DeleteBrandMaster(Session["CompId"].ToString(),lblSelectedRecord.Text.Split(',')[j].Trim(), true.ToString(), Session["UserId"].ToString().ToString(), DateTime.Now.ToString());

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
            foreach (GridViewRow Gvr in gvBrandMasterBin.Rows)
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
    
   public void Reset()
    {

       
        txtBrandCode.Text = "";
        txtBrandName.Text = "";
        txtBrandNameL.Text = "";
      
        txtManagerName.Text = "";
        imgLogo.ImageUrl = "";
        txtAddressName.Text = "";


        
        btnNew.Text = Resources.Attendance.New;
        lblSelectedRecord.Text = "";
        ViewState["Select"] = null;
        editid.Value = "";
        txtValue.Text = "";
        ddlOption.SelectedIndex = 2;
        ddlFieldName.SelectedIndex = 0;
        GvAddressName.DataSource = null;
        GvAddressName.DataBind();
        System.Web.UI.ScriptManager.GetCurrent(this).SetFocus(txtValue);
        

    }


   protected void txtBrand_OnTextChanged(object sender, EventArgs e)
    {

        if (editid.Value == "")
        {
            DataTable dt = objBrand.GetBrandMasterByBrandName(Session["CompId"].ToString().ToString(), txtBrandName.Text.Trim());
            if (dt.Rows.Count > 0)
            {
                txtBrandName.Text = "";
                DisplayMessage("Brand Name Already Exists");
                System.Web.UI.ScriptManager.GetCurrent(this).SetFocus(txtBrandName);
                return;
            }
            DataTable dt1 = objBrand.GetBrandMasterInactive(Session["CompId"].ToString().ToString());
            dt1 = new DataView(dt1, "Brand_Name='" + txtBrandName.Text + "'", "", DataViewRowState.CurrentRows).ToTable();
            if (dt1.Rows.Count > 0)
            {
                txtBrandName.Text = "";
                DisplayMessage("Brand Name Already Exists - Go to Bin Tab");
                System.Web.UI.ScriptManager.GetCurrent(this).SetFocus(txtBrandName);
                return;
            }
            txtBrandNameL.Focus();
        }
        else
        {
            DataTable dtTemp = objBrand.GetBrandMasterById(Session["CompId"].ToString().ToString(), editid.Value);
            if (dtTemp.Rows.Count > 0)
            {
                if (dtTemp.Rows[0]["Brand_Name"].ToString() != txtBrandName.Text)
                {
                    DataTable dt = objBrand.GetBrandMaster(Session["CompId"].ToString().ToString());
                    dt = new DataView(dt, "Brand_Name='" + txtBrandName.Text + "'", "", DataViewRowState.CurrentRows).ToTable();
                    if (dt.Rows.Count > 0)
                    {
                        txtBrandName.Text = "";
                        DisplayMessage("Brand Name Already Exists");
                        System.Web.UI.ScriptManager.GetCurrent(this).SetFocus(txtBrandName);
                        return;
                    }
                    DataTable dt1 = objBrand.GetBrandMasterInactive(Session["CompId"].ToString().ToString());
                    dt1 = new DataView(dt1, "Brand_Name='" + txtBrandName.Text + "'", "", DataViewRowState.CurrentRows).ToTable();
                    if (dt1.Rows.Count > 0)
                    {
                        txtBrandName.Text = "";
                        DisplayMessage("Brand Name Already Exists - Go to Bin Tab");
                        System.Web.UI.ScriptManager.GetCurrent(this).SetFocus(txtBrandName);
                        return;
                    }
                }
            }
            txtBrandNameL.Focus();
        }
        }
    protected void btnUpload_Click(object sender, EventArgs e)
    {
        imgLogo.ImageUrl = "~/CompanyResource/" + "/" + Session["CompId"] + "/" + FULogoPath.FileName;
     
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
