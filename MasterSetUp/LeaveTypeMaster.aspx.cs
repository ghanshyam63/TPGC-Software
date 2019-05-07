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

public partial class MasterSetUp_LeaveMaster : BasePage
{
   
    Common cmn = new Common();
    SystemParameter objSys = new SystemParameter();
    LeaveMaster objLeave = new LeaveMaster();
    protected void Page_Load(object sender, EventArgs e)
    {
        Session["BrandId"] = "1";
       
        if (Session["UserId"] == null)
        {
            Response.Redirect("~/ERPLogin.aspx");
        }
        AllPageCode();
        if (!IsPostBack)
        {
            trPartial.Visible = false;
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
        DataTable dtAllPageCode = cmn.GetAllPagePermission(Session["UserId"].ToString(), "8", "35");
        if (dtAllPageCode.Rows.Count != 0)
        {
            if (dtAllPageCode.Rows[0][0].ToString() == "SuperAdmin")
            {
                btnSave.Visible = true;
                foreach (GridViewRow Row in gvLeaveMaster.Rows)
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
                    foreach (GridViewRow Row in gvLeaveMaster.Rows)
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

    protected void txtLeaveName_OnTextChanged(object sender, EventArgs e)
    {

        if (editid.Value == "")
        {
            DataTable dt = objLeave.GetLeaveMasterByLeaveName(Session["CompId"].ToString().ToString(), txtLeaveName.Text.Trim());
            if (dt.Rows.Count > 0)
            {
                txtLeaveName.Text = "";
                DisplayMessage("Leave Type Name Already Exists");
                System.Web.UI.ScriptManager.GetCurrent(this).SetFocus(txtLeaveName);
                return;
            }
            DataTable dt1 = objLeave.GetLeaveMasterInactive(Session["CompId"].ToString().ToString());
            dt1 = new DataView(dt1, "Leave_Name='" + txtLeaveName.Text + "'", "", DataViewRowState.CurrentRows).ToTable();
            if (dt1.Rows.Count > 0)
            {
                txtLeaveName.Text = "";
                DisplayMessage("Leave Type Name Already Exists - Go to Bin Tab");
                System.Web.UI.ScriptManager.GetCurrent(this).SetFocus(txtLeaveName);
                return;
            }
            txtLeaveNameL.Focus();
        }
        else
        {
            DataTable dtTemp = objLeave.GetLeaveMasterById(Session["CompId"].ToString().ToString(), editid.Value);
            if (dtTemp.Rows.Count > 0)
            {
                if (dtTemp.Rows[0]["Leave_Name"].ToString() != txtLeaveName.Text)
                {
                    DataTable dt = objLeave.GetLeaveMaster(Session["CompId"].ToString().ToString());
                    dt = new DataView(dt, "Leave_Name='" + txtLeaveName.Text + "'", "", DataViewRowState.CurrentRows).ToTable();
                    if (dt.Rows.Count > 0)
                    {
                        txtLeaveName.Text = "";
                        DisplayMessage("Leave Type Name Already Exists");
                        System.Web.UI.ScriptManager.GetCurrent(this).SetFocus(txtLeaveName);
                        return;
                    }
                    DataTable dt1 = objLeave.GetLeaveMaster(Session["CompId"].ToString().ToString());
                    dt1 = new DataView(dt1, "Leave_Name='" + txtLeaveName.Text + "'", "", DataViewRowState.CurrentRows).ToTable();
                    if (dt1.Rows.Count > 0)
                    {
                        txtLeaveName.Text = "";
                        DisplayMessage("Leave Type Name Already Exists - Go to Bin Tab");
                        System.Web.UI.ScriptManager.GetCurrent(this).SetFocus(txtLeaveName);
                        return;
                    }
                }
            }
            txtLeaveNameL.Focus();
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
         txtLeaveName.Focus();
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
            DataTable dtCust = (DataTable)Session["Leave"];


            DataView view = new DataView(dtCust, condition, "", DataViewRowState.CurrentRows);
            Session["dtFilter"] = view.ToTable();
            lblTotalRecords.Text = Resources.Attendance.Total_Records + ": " + view.ToTable().Rows.Count.ToString() + "";

            gvLeaveMaster.DataSource = view.ToTable();
            gvLeaveMaster.DataBind();
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
            DataTable dtCust = (DataTable)Session["dtbinLeave"];


            DataView view = new DataView(dtCust, condition, "", DataViewRowState.CurrentRows);
            Session["dtbinFilter"] = view.ToTable();
            lblbinTotalRecords.Text = Resources.Attendance.Total_Records + ": " + view.ToTable().Rows.Count.ToString() + "";

            gvLeaveMasterBin.DataSource = view.ToTable();
            gvLeaveMasterBin.DataBind();


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

    protected void gvLeaveMasterBin_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {

        gvLeaveMasterBin.PageIndex = e.NewPageIndex;
        if (HDFSortbin.Value == "")
            FillGridBin();
        else
        {
            DataTable dt = (DataTable)Session["dtbinFilter"];
            gvLeaveMasterBin.DataSource = dt;
            gvLeaveMasterBin.DataBind();
            AllPageCode();
        }
        string temp = string.Empty;
        bool isselcted;

        for (int i = 0; i < gvLeaveMasterBin.Rows.Count; i++)
        {
            Label lblconid = (Label)gvLeaveMasterBin.Rows[i].FindControl("lblLeaveId");
            string[] split = lblSelectedRecord.Text.Split(',');

            for (int j = 0; j < lblSelectedRecord.Text.Split(',').Length; j++)
            {
                if (lblSelectedRecord.Text.Split(',')[j] != "")
                {
                    if (lblconid.Text.Trim().ToString() == lblSelectedRecord.Text.Split(',')[j].Trim().ToString())
                    {
                        ((CheckBox)gvLeaveMasterBin.Rows[i].FindControl("chkgvSelect")).Checked = true;
                    }
                }
            }
        }

    }
    protected void gvLeaveMasterBin_OnSorting(object sender, GridViewSortEventArgs e)
    {
        lblSelectedRecord.Text = "";
        HDFSortbin.Value = HDFSortbin.Value == "ASC" ? "DESC" : "ASC";
        DataTable dt = new DataTable();
        dt = objLeave.GetLeaveMasterInactive(Session["CompId"].ToString());
        DataView dv = new DataView(dt);
        string Query = "" + e.SortExpression + " " + HDFSortbin.Value + "";
        dv.Sort = Query;
        dt = dv.ToTable();
        Session["dtbinFilter"] = dt;
        gvLeaveMasterBin.DataSource = dt;
        gvLeaveMasterBin.DataBind();
        AllPageCode();

    }
    

   
    protected void btnSave_Click(object sender, EventArgs e)
    {
        int b = 0;

      

        if (txtLeaveName.Text == "")
        {
            DisplayMessage("Enter Leave Type Name");
            txtLeaveName.Focus();
            return;
        }



        if (editid.Value == "")
        {
            
            DataTable dt1 = objLeave.GetLeaveMaster(Session["CompId"].ToString());

            dt1 = new DataView(dt1, "Leave_Name='" + txtLeaveName.Text + "'", "", DataViewRowState.CurrentRows).ToTable();
            if (dt1.Rows.Count > 0)
            {
                DisplayMessage("Leave Type Name Already Exists");
                txtLeaveName.Focus();
                return;

            }




            b = objLeave.InsertLeaveMaster(Session["CompId"].ToString(), txtLeaveName.Text, txtLeaveNameL.Text,Session["BrandId"].ToString(),false.ToString(),"", "", "", "", "", true.ToString(), DateTime.Now.ToString(), true.ToString(), Session["UserId"].ToString(), DateTime.Now.ToString(), Session["UserId"].ToString(), DateTime.Now.ToString());
            if (b != 0)
            {
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
            string leaveTypeName = string.Empty;
            DataTable dt1 = objLeave.GetLeaveMaster(Session["CompId"].ToString());
            try
            {
                leaveTypeName = new DataView(dt1, "Leave_Id='" + editid.Value + "'", "", DataViewRowState.CurrentRows).ToTable().Rows[0]["Leave_Name"].ToString();
            }
            catch
            {
                leaveTypeName = "";
            }
            dt1 = new DataView(dt1, "Leave_Name='" + txtLeaveName.Text + "' and Leave_Name<>'"+leaveTypeName+"'", "", DataViewRowState.CurrentRows).ToTable();
            if (dt1.Rows.Count > 0)
            {
                DisplayMessage("Leave Type Name Already Exists");
                txtLeaveName.Focus();
                return;

            }
            b = objLeave.UpdateLeaveMaster(editid.Value, Session["CompId"].ToString(), txtLeaveName.Text, txtLeaveNameL.Text,Session["BrandId"].ToString(),false.ToString(), "", "", "", "", "", true.ToString(), DateTime.Now.ToString(), true.ToString(), Session["UserId"].ToString(), DateTime.Now.ToString());

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


        DataTable dt = objLeave.GetLeaveMasterById(Session["CompId"].ToString(), editid.Value);
        if (dt.Rows.Count > 0)
        {
            chkIsPartial.Checked = Convert.ToBoolean(dt.Rows[0]["Is_Partial"].ToString());
            txtLeaveName.Text = dt.Rows[0]["Leave_Name"].ToString();
            txtLeaveNameL.Text = dt.Rows[0]["Leave_Name_L"].ToString();
           


           
           

           
           
            btnNew_Click(null, null);
            btnNew.Text = Resources.Attendance.Edit;

        }



    }

    protected void IbtnDelete_Command(object sender, CommandEventArgs e)
    {
        int b = 0;
        b = objLeave.DeleteLeaveMaster(Session["CompId"].ToString(), e.CommandArgument.ToString(), false.ToString(), Session["UserId"].ToString(), DateTime.Now.ToString());
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
    protected void gvLeaveMaster_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        gvLeaveMaster.PageIndex = e.NewPageIndex;

        DataTable dt = (DataTable)Session["dtFilter"];
        gvLeaveMaster.DataSource = dt;
        gvLeaveMaster.DataBind();
        AllPageCode();

    }
    protected void gvLeaveMaster_OnSorting(object sender, GridViewSortEventArgs e)
    {
        HDFSort.Value = HDFSort.Value == "ASC" ? "DESC" : "ASC";
        DataTable dt = new DataTable();
        dt = (DataTable)Session["dtFilter"];
        DataView dv = new DataView(dt);
        string Query = "" + e.SortExpression + " " + HDFSort.Value + "";
        dv.Sort = Query;
        dt = dv.ToTable();
        Session["dtFilter"] = dt;
        gvLeaveMaster.DataSource = dt;
        gvLeaveMaster.DataBind();
        AllPageCode();
    }

    
    [System.Web.Services.WebMethodAttribute(), System.Web.Script.Services.ScriptMethodAttribute()]
    public static string[] GetCompletionListLeaveName(string prefixText, int count, string contextKey)
    {
        LeaveMaster objLeaveMaster = new LeaveMaster();
        DataTable dt = new DataView(objLeaveMaster.GetLeaveMaster(HttpContext.Current.Session["CompId"].ToString()), "Leave_Name like '" + prefixText.ToString() + "%'", "", DataViewRowState.CurrentRows).ToTable();
        string[] txt = new string[dt.Rows.Count];

        for (int i = 0; i < dt.Rows.Count; i++)
        {
            txt[i] = dt.Rows[i]["Leave_Name"].ToString();
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
        DataTable dt = objLeave.GetLeaveMaster(Session["CompId"].ToString());
        gvLeaveMaster.DataSource = dt;
        gvLeaveMaster.DataBind();
        AllPageCode();
        Session["dtFilter"] = dt;
        Session["Leave"] = dt;

        lblTotalRecords.Text = Resources.Attendance.Total_Records + ": " + dt.Rows.Count.ToString() + "";

    }
    public void FillGridBin()
    {
        DataTable dt = new DataTable();
        dt = objLeave.GetLeaveMasterInactive(Session["CompId"].ToString());

        gvLeaveMasterBin.DataSource = dt;
        gvLeaveMasterBin.DataBind();


        Session["dtbinFilter"] = dt;
        Session["dtbinLeave"] = dt;
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
        CheckBox chkSelAll = ((CheckBox)gvLeaveMasterBin.HeaderRow.FindControl("chkgvSelectAll"));
        for (int i = 0; i < gvLeaveMasterBin.Rows.Count; i++)
        {
            ((CheckBox)gvLeaveMasterBin.Rows[i].FindControl("chkgvSelect")).Checked = chkSelAll.Checked;
            if (chkSelAll.Checked)
            {
                if (!lblSelectedRecord.Text.Split(',').Contains(((Label)(gvLeaveMasterBin.Rows[i].FindControl("lblLeaveId"))).Text.Trim().ToString()))
                {
                    lblSelectedRecord.Text += ((Label)(gvLeaveMasterBin.Rows[i].FindControl("lblLeaveId"))).Text.Trim().ToString() + ",";
                }
            }
            else
            {
                string temp = string.Empty;
                string[] split = lblSelectedRecord.Text.Split(',');
                foreach (string item in split)
                {
                    if (item != ((Label)(gvLeaveMasterBin.Rows[i].FindControl("lblLeaveId"))).Text.Trim().ToString())
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
        Label lb = (Label)gvLeaveMasterBin.Rows[index].FindControl("lblLeaveId");
        if (((CheckBox)gvLeaveMasterBin.Rows[index].FindControl("chkgvSelect")).Checked)
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
                if (!lblSelectedRecord.Text.Split(',').Contains(dr["Leave_Id"]))
                {
                    lblSelectedRecord.Text += dr["Leave_Id"] + ",";
                }
            }
            for (int i = 0; i < gvLeaveMasterBin.Rows.Count; i++)
            {
                string[] split = lblSelectedRecord.Text.Split(',');
                Label lblconid = (Label)gvLeaveMasterBin.Rows[i].FindControl("lblLeaveId");
                for (int j = 0; j < lblSelectedRecord.Text.Split(',').Length; j++)
                {
                    if (lblSelectedRecord.Text.Split(',')[j] != "")
                    {
                        if (lblconid.Text.Trim().ToString() == lblSelectedRecord.Text.Split(',')[j].Trim().ToString())
                        {
                            ((CheckBox)gvLeaveMasterBin.Rows[i].FindControl("chkgvSelect")).Checked = true;
                        }
                    }
                }
            }
        }
        else
        {
            lblSelectedRecord.Text = "";
            DataTable dtUnit1 = (DataTable)Session["dtBinFilter"];
            gvLeaveMasterBin.DataSource = dtUnit1;
            gvLeaveMasterBin.DataBind();
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
                    b = objLeave.DeleteLeaveMaster(Session["CompId"].ToString(),lblSelectedRecord.Text.Split(',')[j].Trim(), true.ToString(), Session["UserId"].ToString().ToString(), DateTime.Now.ToString());

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
            foreach (GridViewRow Gvr in gvLeaveMasterBin.Rows)
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


        chkIsPartial.Checked = false;
        txtLeaveName.Text = "";
        txtLeaveNameL.Text = "";
      
        
       
        


        
        btnNew.Text = Resources.Attendance.New;
        lblSelectedRecord.Text = "";
        ViewState["Select"] = null;
        editid.Value = "";
        txtValue.Text = "";
        ddlOption.SelectedIndex = 2;
        ddlFieldName.SelectedIndex = 0;
   
        

    }

   
}
