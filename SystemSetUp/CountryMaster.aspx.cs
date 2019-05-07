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

public partial class MasterSetUp_CountryMaster : BasePage
{
   
    Common cmn = new Common();
    SystemParameter objSys = new SystemParameter();
    CountryMaster objCountry = new CountryMaster();
    protected void Page_Load(object sender, EventArgs e)
    {
       
       
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

        Session["AccordianId"] = "7";
        Session["HeaderText"] = "System Setup";
        DataTable dtAllPageCode = cmn.GetAllPagePermission(Session["UserId"].ToString(), "7", "5");
        if (dtAllPageCode.Rows.Count != 0)
        {
            if (dtAllPageCode.Rows[0][0].ToString() == "SuperAdmin")
            {
                btnSave.Visible = true;
                foreach (GridViewRow Row in gvCountryMaster.Rows)
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
                    foreach (GridViewRow Row in gvCountryMaster.Rows)
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
         txtCountryCode.Focus();
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


    protected void txtCountryName_TextChanged(object sender, EventArgs e)
    {


        if (editid.Value == "")
        {
            DataTable dt = objCountry.GetCountryMasterByCountryName(txtCountryName.Text);
            if (dt.Rows.Count > 0)
            {
                txtCountryName.Text = "";
                DisplayMessage("Country Name Already Exists");
                System.Web.UI.ScriptManager.GetCurrent(this).SetFocus(txtCountryName);
                return;
            }
            DataTable dt1 = objCountry.GetCountryMasterInactive();
            dt1 = new DataView(dt1, "Country_Name='" + txtCountryName.Text + "'", "", DataViewRowState.CurrentRows).ToTable();
            if (dt1.Rows.Count > 0)
            {
                txtCountryName.Text = "";
                DisplayMessage("Country Name Already Exists - Go to Bin Tab");
                System.Web.UI.ScriptManager.GetCurrent(this).SetFocus(txtCountryName);
                return;
            }
        }
        else
        {
            DataTable dtTemp = objCountry.GetCountryMasterById(editid.Value);
            if (dtTemp.Rows.Count > 0)
            {
                if (dtTemp.Rows[0]["Country_Name"].ToString() != txtCountryName.Text)
                {
                    DataTable dt = objCountry.GetCountryMasterByCountryName(txtCountryName.Text);
                    if (dt.Rows.Count > 0)
                    {
                        txtCountryName.Text = "";
                        DisplayMessage("Country Name Already Exists");
                        System.Web.UI.ScriptManager.GetCurrent(this).SetFocus(txtCountryName);
                        return;
                    }
                    DataTable dt1 = objCountry.GetCountryMasterInactive();
                    dt1 = new DataView(dt1, "Country_Name='" + txtCountryName.Text + "'", "", DataViewRowState.CurrentRows).ToTable();
                    if (dt1.Rows.Count > 0)
                    {
                        txtCountryName.Text = "";
                        DisplayMessage("Country Name Already Exists - Go to Bin Tab");
                        System.Web.UI.ScriptManager.GetCurrent(this).SetFocus(txtCountryName);
                        return;
                    }
                }
            }
        }
        System.Web.UI.ScriptManager.GetCurrent(this).SetFocus(txtCountryNameL);


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
            DataTable dtCust = (DataTable)Session["Country"];


            DataView view = new DataView(dtCust, condition, "", DataViewRowState.CurrentRows);
            Session["dtFilter"] = view.ToTable();
            lblTotalRecords.Text = Resources.Attendance.Total_Records + ": " + view.ToTable().Rows.Count.ToString() + "";

            gvCountryMaster.DataSource = view.ToTable();
            gvCountryMaster.DataBind();
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
            DataTable dtCust = (DataTable)Session["dtbinCountry"];


            DataView view = new DataView(dtCust, condition, "", DataViewRowState.CurrentRows);
            Session["dtbinFilter"] = view.ToTable();
            lblbinTotalRecords.Text = Resources.Attendance.Total_Records + ": " + view.ToTable().Rows.Count.ToString() + "";

            gvCountryMasterBin.DataSource = view.ToTable();
            gvCountryMasterBin.DataBind();


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

    protected void gvCountryMasterBin_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {

        gvCountryMasterBin.PageIndex = e.NewPageIndex;
        if (HDFSortbin.Value == "")
            FillGridBin();
        else
        {
            DataTable dt = (DataTable)Session["dtbinFilter"];
            gvCountryMasterBin.DataSource = dt;
            gvCountryMasterBin.DataBind();
            AllPageCode();
        }
        string temp = string.Empty;
        bool isselcted;

        for (int i = 0; i < gvCountryMasterBin.Rows.Count; i++)
        {
            Label lblconid = (Label)gvCountryMasterBin.Rows[i].FindControl("lblCountryId");
            string[] split = lblSelectedRecord.Text.Split(',');

            for (int j = 0; j < lblSelectedRecord.Text.Split(',').Length; j++)
            {
                if (lblSelectedRecord.Text.Split(',')[j] != "")
                {
                    if (lblconid.Text.Trim().ToString() == lblSelectedRecord.Text.Split(',')[j].Trim().ToString())
                    {
                        ((CheckBox)gvCountryMasterBin.Rows[i].FindControl("chkgvSelect")).Checked = true;
                    }
                }
            }
        }

    }
    protected void gvCountryMasterBin_OnSorting(object sender, GridViewSortEventArgs e)
    {
        lblSelectedRecord.Text = "";
        HDFSortbin.Value = HDFSortbin.Value == "ASC" ? "DESC" : "ASC";
        DataTable dt = new DataTable();
        dt = objCountry.GetCountryMasterInactive();
        DataView dv = new DataView(dt);
        string Query = "" + e.SortExpression + " " + HDFSortbin.Value + "";
        dv.Sort = Query;
        dt = dv.ToTable();
        Session["dtbinFilter"] = dt;
        gvCountryMasterBin.DataSource = dt;
        gvCountryMasterBin.DataBind();
        AllPageCode();

    }
    

   
    protected void btnSave_Click(object sender, EventArgs e)
    {
        int b = 0;

        if (txtCountryCode.Text == "")
        {
            DisplayMessage("Enter Country Code");
            txtCountryCode.Focus();
            return;
        }

        if (txtCountryName.Text == "")
        {
            DisplayMessage("Enter Country Name");
            txtCountryName.Focus();
            return;
        }



        if (editid.Value == "")
        {
            DataTable dt = objCountry.GetCountryMaster();

            dt = new DataView(dt, "Country_Code='" + txtCountryCode.Text + "'", "", DataViewRowState.CurrentRows).ToTable();
            if (dt.Rows.Count > 0)
            {
                DisplayMessage("Country Code Already Exists");
                txtCountryCode.Focus();
                return;

            }
            DataTable dt1 = objCountry.GetCountryMaster();

            dt1 = new DataView(dt1, "Country_Name='" + txtCountryName.Text + "'", "", DataViewRowState.CurrentRows).ToTable();
            if (dt1.Rows.Count > 0)
            {
                DisplayMessage("Country Name Already Exists");
                txtCountryName.Focus();
                return;

            }
            else
            {
                dt1 = objCountry.GetCountryMasterInactive();
                dt1 = new DataView(dt1, "Country_Name='" + txtCountryName.Text + "'", "", DataViewRowState.CurrentRows).ToTable();
                if (dt1.Rows.Count > 0)
                {
                    DisplayMessage("Country Name Already Exists in Bin Section");
                    txtCountryName.Focus();
                    return;

                }
            }



            b = objCountry.InsertCountryMaster(txtCountryName.Text, txtCountryNameL.Text, txtCountryCode.Text, "", "", "", "", "", true.ToString(), DateTime.Now.ToString(), true.ToString(), Session["UserId"].ToString(), DateTime.Now.ToString(), Session["UserId"].ToString(), DateTime.Now.ToString());
            if (b != 0)
            {
              

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


            DataTable dt2 = objCountry.GetCountryMaster();

            dt2 = new DataView(dt2, "Country_Name='" + txtCountryName.Text + "'", "", DataViewRowState.CurrentRows).ToTable();
            if (dt2.Rows[0]["Country_Id"].ToString() != editid.Value && editid.Value != "")
            {
                DisplayMessage("Country Name Already Exists");
                txtCountryName.Focus();
                return;

            }


            DataTable dt3 = objCountry.GetCountryMaster();

            dt3 = new DataView(dt3, "Country_Code='" + txtCountryCode.Text + "'", "", DataViewRowState.CurrentRows).ToTable();
            try
            {
                if (dt3.Rows[0]["Country_Id"].ToString() != editid.Value && editid.Value != "")
                {
                    DisplayMessage("Country Code Already Exists");
                    txtCountryCode.Focus();
                    return;

                }
            }
            catch
            {

            }


            b = objCountry.UpdateCountryMaster(editid.Value,txtCountryName.Text, txtCountryNameL.Text, txtCountryCode.Text,"","","","","",true.ToString(),DateTime.Now.ToString(),true.ToString(), Session["UserId"].ToString(), DateTime.Now.ToString());

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



    protected void btnEdit_Command(object sender, CommandEventArgs e)
    {
        editid.Value = e.CommandArgument.ToString();
       

        DataTable dt = objCountry.GetCountryMasterById(editid.Value);
        if (dt.Rows.Count > 0)
        {
            txtCountryCode.Text = dt.Rows[0]["Country_Code"].ToString();
            txtCountryName.Text = dt.Rows[0]["Country_Name"].ToString();
            txtCountryNameL.Text = dt.Rows[0]["Country_Name_L"].ToString();
           


           
           

           
           
            btnNew_Click(null, null);
            btnNew.Text = Resources.Attendance.Edit;

        }



    }

    protected void IbtnDelete_Command(object sender, CommandEventArgs e)
    {
        int b = 0;
        b = objCountry.DeleteCountryMaster(e.CommandArgument.ToString(), false.ToString(), Session["UserId"].ToString(), DateTime.Now.ToString());
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
    protected void gvCountryMaster_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        gvCountryMaster.PageIndex = e.NewPageIndex;

        DataTable dt = (DataTable)Session["dtFilter"];
        gvCountryMaster.DataSource = dt;
        gvCountryMaster.DataBind();
        AllPageCode();

    }
    protected void gvCountryMaster_OnSorting(object sender, GridViewSortEventArgs e)
    {
        HDFSort.Value = HDFSort.Value == "ASC" ? "DESC" : "ASC";
        DataTable dt = new DataTable();
        dt = (DataTable)Session["dtFilter"];
        DataView dv = new DataView(dt);
        string Query = "" + e.SortExpression + " " + HDFSort.Value + "";
        dv.Sort = Query;
        dt = dv.ToTable();
        Session["dtFilter"] = dt;
        gvCountryMaster.DataSource = dt;
        gvCountryMaster.DataBind();
        AllPageCode();
    }

    [System.Web.Services.WebMethodAttribute(), System.Web.Script.Services.ScriptMethodAttribute()]
    public static string[] GetCompletionListCountryCode(string prefixText, int count, string contextKey)
    {
        CountryMaster objCountryMaster = new CountryMaster();
        DataTable dt = new DataView(objCountryMaster.GetCountryMaster(), "Country_Code like '" + prefixText.ToString() + "%'", "", DataViewRowState.CurrentRows).ToTable();
        string[] txt = new string[dt.Rows.Count];

        for (int i = 0; i < dt.Rows.Count; i++)
        {
            txt[i] = dt.Rows[i]["Country_Code"].ToString();
        }
        return txt;
    }
    [System.Web.Services.WebMethodAttribute(), System.Web.Script.Services.ScriptMethodAttribute()]
    public static string[] GetCompletionListCountryName(string prefixText, int count, string contextKey)
    {
        CountryMaster objCountryMaster = new CountryMaster();
        DataTable dt = new DataView(objCountryMaster.GetCountryMaster(), "Country_Name like '" + prefixText.ToString() + "%'", "", DataViewRowState.CurrentRows).ToTable();
        string[] txt = new string[dt.Rows.Count];

        for (int i = 0; i < dt.Rows.Count; i++)
        {
            txt[i] = dt.Rows[i]["Country_Name"].ToString();
        }
        return txt;
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
        DataTable dt = objCountry.GetCountryMaster();
        gvCountryMaster.DataSource = dt;
        gvCountryMaster.DataBind();
        AllPageCode();
        Session["dtFilter"] = dt;
        Session["Country"] = dt;

        lblTotalRecords.Text = Resources.Attendance.Total_Records + ": " + dt.Rows.Count.ToString() + "";

    }
    public void FillGridBin()
    {
        DataTable dt = new DataTable();
        dt = objCountry.GetCountryMasterInactive();

        gvCountryMasterBin.DataSource = dt;
        gvCountryMasterBin.DataBind();


        Session["dtbinFilter"] = dt;
        Session["dtbinCountry"] = dt;
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
        CheckBox chkSelAll = ((CheckBox)gvCountryMasterBin.HeaderRow.FindControl("chkgvSelectAll"));
        for (int i = 0; i < gvCountryMasterBin.Rows.Count; i++)
        {
            ((CheckBox)gvCountryMasterBin.Rows[i].FindControl("chkgvSelect")).Checked = chkSelAll.Checked;
            if (chkSelAll.Checked)
            {
                if (!lblSelectedRecord.Text.Split(',').Contains(((Label)(gvCountryMasterBin.Rows[i].FindControl("lblCountryId"))).Text.Trim().ToString()))
                {
                    lblSelectedRecord.Text += ((Label)(gvCountryMasterBin.Rows[i].FindControl("lblCountryId"))).Text.Trim().ToString() + ",";
                }
            }
            else
            {
                string temp = string.Empty;
                string[] split = lblSelectedRecord.Text.Split(',');
                foreach (string item in split)
                {
                    if (item != ((Label)(gvCountryMasterBin.Rows[i].FindControl("lblCountryId"))).Text.Trim().ToString())
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
        Label lb = (Label)gvCountryMasterBin.Rows[index].FindControl("lblCountryId");
        if (((CheckBox)gvCountryMasterBin.Rows[index].FindControl("chkgvSelect")).Checked)
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
                if (!lblSelectedRecord.Text.Split(',').Contains(dr["Country_Id"]))
                {
                    lblSelectedRecord.Text += dr["Country_Id"] + ",";
                }
            }
            for (int i = 0; i < gvCountryMasterBin.Rows.Count; i++)
            {
                string[] split = lblSelectedRecord.Text.Split(',');
                Label lblconid = (Label)gvCountryMasterBin.Rows[i].FindControl("lblCountryId");
                for (int j = 0; j < lblSelectedRecord.Text.Split(',').Length; j++)
                {
                    if (lblSelectedRecord.Text.Split(',')[j] != "")
                    {
                        if (lblconid.Text.Trim().ToString() == lblSelectedRecord.Text.Split(',')[j].Trim().ToString())
                        {
                            ((CheckBox)gvCountryMasterBin.Rows[i].FindControl("chkgvSelect")).Checked = true;
                        }
                    }
                }
            }
        }
        else
        {
            lblSelectedRecord.Text = "";
            DataTable dtUnit1 = (DataTable)Session["dtBinFilter"];
            gvCountryMasterBin.DataSource = dtUnit1;
            gvCountryMasterBin.DataBind();
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
                    b = objCountry.DeleteCountryMaster(lblSelectedRecord.Text.Split(',')[j].Trim(), true.ToString(), Session["UserId"].ToString().ToString(), DateTime.Now.ToString());

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
            foreach (GridViewRow Gvr in gvCountryMasterBin.Rows)
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

       
        txtCountryCode.Text = "";
        txtCountryName.Text = "";
        txtCountryNameL.Text = "";
      
        
       
        


        
        btnNew.Text = Resources.Attendance.New;
        lblSelectedRecord.Text = "";
        ViewState["Select"] = null;
        editid.Value = "";
        txtValue.Text = "";
        ddlOption.SelectedIndex = 2;
        ddlFieldName.SelectedIndex = 0;
   
        

    }

   
}
