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

public partial class MasterSetUp_BankMaster : BasePage
{
   
    Common cmn = new Common();
    SystemParameter objSys = new SystemParameter();
    Set_BankMaster objBank = new Set_BankMaster();
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

        Session["AccordianId"] = "8";
        Session["HeaderText"] = "Master Setup";
        DataTable dtAllPageCode = cmn.GetAllPagePermission(Session["UserId"].ToString(), "8", "46");
        if (dtAllPageCode.Rows.Count != 0)
        {
            if (dtAllPageCode.Rows[0][0].ToString() == "SuperAdmin")
            {
                btnSave.Visible = true;
                foreach (GridViewRow Row in gvBankMaster.Rows)
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
                    foreach (GridViewRow Row in gvBankMaster.Rows)
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
         txtBankCode.Focus();
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
            DataTable dtCust = (DataTable)Session["Bank"];


            DataView view = new DataView(dtCust, condition, "", DataViewRowState.CurrentRows);
            Session["dtFilter"] = view.ToTable();
            lblTotalRecords.Text = Resources.Attendance.Total_Records + ": " + view.ToTable().Rows.Count.ToString() + "";

            gvBankMaster.DataSource = view.ToTable();
            gvBankMaster.DataBind();
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
            DataTable dtCust = (DataTable)Session["dtbinBank"];


            DataView view = new DataView(dtCust, condition, "", DataViewRowState.CurrentRows);
            Session["dtbinFilter"] = view.ToTable();
            lblbinTotalRecords.Text = Resources.Attendance.Total_Records + ": " + view.ToTable().Rows.Count.ToString() + "";

            gvBankMasterBin.DataSource = view.ToTable();
            gvBankMasterBin.DataBind();


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

    protected void gvBankMasterBin_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {

        gvBankMasterBin.PageIndex = e.NewPageIndex;
        if (HDFSortbin.Value == "")
            FillGridBin();
        else
        {
            DataTable dt = (DataTable)Session["dtbinFilter"];
            gvBankMasterBin.DataSource = dt;
            gvBankMasterBin.DataBind();
            AllPageCode();
        }
        string temp = string.Empty;
        bool isselcted;

        for (int i = 0; i < gvBankMasterBin.Rows.Count; i++)
        {
            Label lblconid = (Label)gvBankMasterBin.Rows[i].FindControl("lblBankId");
            string[] split = lblSelectedRecord.Text.Split(',');

            for (int j = 0; j < lblSelectedRecord.Text.Split(',').Length; j++)
            {
                if (lblSelectedRecord.Text.Split(',')[j] != "")
                {
                    if (lblconid.Text.Trim().ToString() == lblSelectedRecord.Text.Split(',')[j].Trim().ToString())
                    {
                        ((CheckBox)gvBankMasterBin.Rows[i].FindControl("chkgvSelect")).Checked = true;
                    }
                }
            }
        }

    }
    protected void gvBankMasterBin_OnSorting(object sender, GridViewSortEventArgs e)
    {
        lblSelectedRecord.Text = "";
        HDFSortbin.Value = HDFSortbin.Value == "ASC" ? "DESC" : "ASC";
        DataTable dt = new DataTable();
        dt = objBank.GetBankMasterInactive(Session["CompId"].ToString());
        DataView dv = new DataView(dt);
        string Query = "" + e.SortExpression + " " + HDFSortbin.Value + "";
        dv.Sort = Query;
        dt = dv.ToTable();
        Session["dtbinFilter"] = dt;
        gvBankMasterBin.DataSource = dt;
        gvBankMasterBin.DataBind();
        AllPageCode();

    }
    

   
    protected void btnSave_Click(object sender, EventArgs e)
    {
        int b = 0;

        if (txtBankCode.Text == "")
        {
            DisplayMessage("Enter Bank Code");
            txtBankCode.Focus();
            return;
        }

        if (txtBankName.Text == "")
        {
            DisplayMessage("Enter Bank Name");
            txtBankName.Focus();
            return;
        }



        if (editid.Value == "")
        {
            DataTable dt = objBank.GetBankMaster(Session["CompId"].ToString());

            dt = new DataView(dt, "Bank_Code='" + txtBankCode.Text + "'", "", DataViewRowState.CurrentRows).ToTable();
            if (dt.Rows.Count > 0)
            {
                DisplayMessage("Bank Code Already Exists");
                txtBankCode.Focus();
                return;

            }
            DataTable dt1 = objBank.GetBankMaster(Session["CompId"].ToString());

            dt1 = new DataView(dt1, "Bank_Name='" + txtBankName.Text + "'", "", DataViewRowState.CurrentRows).ToTable();
            if (dt1.Rows.Count > 0)
            {
                DisplayMessage("Bank Name Already Exists");
                txtBankName.Focus();
                return;

            }




            b = objBank.InsertBankMaster(Session["CompId"].ToString(),txtBankCode.Text, txtBankName.Text, txtBankNameL.Text,txtAddress.Text, "", "", "", "", "", true.ToString(), DateTime.Now.ToString(), true.ToString(), Session["UserId"].ToString(), DateTime.Now.ToString(), Session["UserId"].ToString(), DateTime.Now.ToString());
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

            DataTable dt = objBank.GetBankMaster(Session["CompId"].ToString());

            DataTable dt2 = objBank.GetBankMasterById(Session["CompId"].ToString(),editid.Value);
            string BankName = string.Empty;
            string BankCode = string.Empty;
            if(dt2.Rows.Count > 0)
            {
                BankName=dt2.Rows[0]["Bank_Name"].ToString();
                BankCode=dt2.Rows[0]["Bank_Code"].ToString();

            }


            dt = new DataView(dt, "Bank_Code='" + txtBankCode.Text + "' and Bank_Code<>'"+BankCode+"'", "", DataViewRowState.CurrentRows).ToTable();
            if (dt.Rows.Count > 0)
            {
                DisplayMessage("Bank Code Already Exists");
                txtBankCode.Focus();
                return;

            }
            DataTable dt1 = objBank.GetBankMaster(Session["CompId"].ToString());

            dt1 = new DataView(dt1, "Bank_Name='" + txtBankName.Text + "' and Bank_Name<>'"+BankName+"'", "", DataViewRowState.CurrentRows).ToTable();
            if (dt1.Rows.Count > 0)
            {
                DisplayMessage("Bank Name Already Exists");
                txtBankName.Focus();
                return;

            }

            b = objBank.UpdateBankMaster(Session["CompId"].ToString(), editid.Value, txtBankCode.Text, txtAddress.Text, txtBankName.Text, txtBankNameL.Text, "", "", "", "", "", true.ToString(), DateTime.Now.ToString(), true.ToString(), Session["UserId"].ToString(), DateTime.Now.ToString());

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
       

        DataTable dt = objBank.GetBankMasterById(Session["CompId"].ToString(),editid.Value);
        if (dt.Rows.Count > 0)
        {
            txtBankCode.Text = dt.Rows[0]["Bank_Code"].ToString();
            txtBankName.Text = dt.Rows[0]["Bank_Name"].ToString();
            txtBankNameL.Text = dt.Rows[0]["Bank_Name_L"].ToString();

            txtAddress.Text = dt.Rows[0]["Address"].ToString();

           
           

           
           
            btnNew_Click(null, null);
            btnNew.Text = Resources.Attendance.Edit;

        }



    }

    protected void IbtnDelete_Command(object sender, CommandEventArgs e)
    {
        int b = 0;
        b = objBank.DeleteBankMaster(Session["CompId"].ToString(), e.CommandArgument.ToString(), false.ToString(), Session["UserId"].ToString(), DateTime.Now.ToString());
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
    protected void gvBankMaster_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        gvBankMaster.PageIndex = e.NewPageIndex;

        DataTable dt = (DataTable)Session["dtFilter"];
        gvBankMaster.DataSource = dt;
        gvBankMaster.DataBind();
        AllPageCode();

    }
    protected void gvBankMaster_OnSorting(object sender, GridViewSortEventArgs e)
    {
        HDFSort.Value = HDFSort.Value == "ASC" ? "DESC" : "ASC";
        DataTable dt = new DataTable();
        dt = (DataTable)Session["dtFilter"];
        DataView dv = new DataView(dt);
        string Query = "" + e.SortExpression + " " + HDFSort.Value + "";
        dv.Sort = Query;
        dt = dv.ToTable();
        Session["dtFilter"] = dt;
        gvBankMaster.DataSource = dt;
        gvBankMaster.DataBind();
        AllPageCode();
    }

    [System.Web.Services.WebMethodAttribute(), System.Web.Script.Services.ScriptMethodAttribute()]
    public static string[] GetCompletionListBankCode(string prefixText, int count, string contextKey)
    {
        Set_BankMaster objBankMaster = new Set_BankMaster();
        DataTable dt = new DataView(objBankMaster.GetBankMaster(HttpContext.Current.Session["CompId"].ToString()), "Bank_Code like '" + prefixText.ToString() + "%'", "", DataViewRowState.CurrentRows).ToTable();
        string[] txt = new string[dt.Rows.Count];

        for (int i = 0; i < dt.Rows.Count; i++)
        {
            txt[i] = dt.Rows[i]["Bank_Code"].ToString();
        }
        return txt;
    }
    [System.Web.Services.WebMethodAttribute(), System.Web.Script.Services.ScriptMethodAttribute()]
    public static string[] GetCompletionListBankName(string prefixText, int count, string contextKey)
    {
        Set_BankMaster objBankMaster = new Set_BankMaster();
        DataTable dt = new DataView(objBankMaster.GetBankMaster(HttpContext.Current.Session["CompId"].ToString()), "Bank_Name like '" + prefixText.ToString() + "%'", "", DataViewRowState.CurrentRows).ToTable();
        string[] txt = new string[dt.Rows.Count];

        for (int i = 0; i < dt.Rows.Count; i++)
        {
            txt[i] = dt.Rows[i]["Bank_Name"].ToString();
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
        DataTable dt = objBank.GetBankMaster(Session["CompId"].ToString());
        gvBankMaster.DataSource = dt;
        gvBankMaster.DataBind();
        AllPageCode();
        Session["dtFilter"] = dt;
        Session["Bank"] = dt;

        lblTotalRecords.Text = Resources.Attendance.Total_Records + ": " + dt.Rows.Count.ToString() + "";

    }
    public void FillGridBin()
    {
        DataTable dt = new DataTable();
        dt = objBank.GetBankMasterInactive(Session["CompId"].ToString());

        gvBankMasterBin.DataSource = dt;
        gvBankMasterBin.DataBind();


        Session["dtbinFilter"] = dt;
        Session["dtbinBank"] = dt;
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
        CheckBox chkSelAll = ((CheckBox)gvBankMasterBin.HeaderRow.FindControl("chkgvSelectAll"));
        for (int i = 0; i < gvBankMasterBin.Rows.Count; i++)
        {
            ((CheckBox)gvBankMasterBin.Rows[i].FindControl("chkgvSelect")).Checked = chkSelAll.Checked;
            if (chkSelAll.Checked)
            {
                if (!lblSelectedRecord.Text.Split(',').Contains(((Label)(gvBankMasterBin.Rows[i].FindControl("lblBankId"))).Text.Trim().ToString()))
                {
                    lblSelectedRecord.Text += ((Label)(gvBankMasterBin.Rows[i].FindControl("lblBankId"))).Text.Trim().ToString() + ",";
                }
            }
            else
            {
                string temp = string.Empty;
                string[] split = lblSelectedRecord.Text.Split(',');
                foreach (string item in split)
                {
                    if (item != ((Label)(gvBankMasterBin.Rows[i].FindControl("lblBankId"))).Text.Trim().ToString())
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
        Label lb = (Label)gvBankMasterBin.Rows[index].FindControl("lblBankId");
        if (((CheckBox)gvBankMasterBin.Rows[index].FindControl("chkgvSelect")).Checked)
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
                if (!lblSelectedRecord.Text.Split(',').Contains(dr["Bank_Id"]))
                {
                    lblSelectedRecord.Text += dr["Bank_Id"] + ",";
                }
            }
            for (int i = 0; i < gvBankMasterBin.Rows.Count; i++)
            {
                string[] split = lblSelectedRecord.Text.Split(',');
                Label lblconid = (Label)gvBankMasterBin.Rows[i].FindControl("lblBankId");
                for (int j = 0; j < lblSelectedRecord.Text.Split(',').Length; j++)
                {
                    if (lblSelectedRecord.Text.Split(',')[j] != "")
                    {
                        if (lblconid.Text.Trim().ToString() == lblSelectedRecord.Text.Split(',')[j].Trim().ToString())
                        {
                            ((CheckBox)gvBankMasterBin.Rows[i].FindControl("chkgvSelect")).Checked = true;
                        }
                    }
                }
            }
        }
        else
        {
            lblSelectedRecord.Text = "";
            DataTable dtUnit1 = (DataTable)Session["dtBinFilter"];
            gvBankMasterBin.DataSource = dtUnit1;
            gvBankMasterBin.DataBind();
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
                    b = objBank.DeleteBankMaster(Session["CompId"].ToString(), lblSelectedRecord.Text.Split(',')[j].Trim(), true.ToString(), Session["UserId"].ToString().ToString(), DateTime.Now.ToString());

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
            foreach (GridViewRow Gvr in gvBankMasterBin.Rows)
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

       
        txtBankCode.Text = "";
        txtBankName.Text = "";
        txtBankNameL.Text = "";

        txtAddress.Text = "";
       
        


        
        btnNew.Text = Resources.Attendance.New;
        lblSelectedRecord.Text = "";
        ViewState["Select"] = null;
        editid.Value = "";
        txtValue.Text = "";
        ddlOption.SelectedIndex = 2;
        ddlFieldName.SelectedIndex = 0;
   
        

    }

   
}
