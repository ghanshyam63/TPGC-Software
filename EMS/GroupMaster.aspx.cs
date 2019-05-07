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
using PegasusDataAccess;
using System.Text;

public partial class EMS_GroupMaster : System.Web.UI.Page
{
    #region defind Class Object
    DataAccessClass daClass = new DataAccessClass();
    Ems_GroupMaster ObjGroupMaster = new Ems_GroupMaster();
    ArrayList arr = new ArrayList();
    SystemParameter ObjSysPeram = new SystemParameter();
    Common cmn = new Common();
    string SortExpression = "";
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
            StrUserId = Session["UserId"].ToString();
            StrCompId = Session["CompId"].ToString();

            ddlOption.SelectedIndex = 2;
            btnList_Click(null, null);
            FillPerentDDl();
            FillGridBin();
            FillGrid();
        }
        AllPageCode();
        BindTreeView();
    }


    #region AllPageCode
    public void AllPageCode()
    {
        Page.Title = ObjSysPeram.GetSysTitle();
        StrUserId = Session["UserId"].ToString();
        StrCompId = Session["CompId"].ToString();
        Session["AccordianId"] = "8";
        Session["HeaderText"] = "Master Setup";
        DataTable dtAllPageCode = cmn.GetAllPagePermission(StrUserId.ToString(), "8", "34");
        if (dtAllPageCode.Rows.Count != 0)
        {
            if (dtAllPageCode.Rows[0][0].ToString() == "SuperAdmin")
            {
                btnSave.Visible = true;
                foreach (GridViewRow Row in GvGroup.Rows)
                {
                    ((ImageButton)Row.FindControl("btnEdit")).Visible = true;
                    ((ImageButton)Row.FindControl("IbtnDelete")).Visible = true;
                }
                ImgbtnSelectAll.Visible = true;
                btnRestoreSelected.Visible = true;
            }
            else
            {
                foreach (DataRow DtRow in dtAllPageCode.Rows)
                {
                    if (Convert.ToBoolean(DtRow["Op_Add"].ToString()))
                    {
                        btnSave.Visible = true;
                    }
                    foreach (GridViewRow Row in GvGroup.Rows)
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
                        ImgbtnSelectAll.Visible = true;
                        btnRestoreSelected.Visible = true;
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
    protected void ImgbtnSelectAll_Click(object sender, ImageClickEventArgs e)
    {
        DataTable dtPbrand = (DataTable)Session["dtInactive"];

        if (ViewState["Select"] == null)
        {
            ViewState["Select"] = 1;
            foreach (DataRow dr in dtPbrand.Rows)
            {
                if (!lblSelectedRecord.Text.Split(',').Contains(dr["Group_Id"]))
                {
                    lblSelectedRecord.Text += dr["Group_Id"] + ",";
                }
            }
            for (int i = 0; i < GvGroupBin.Rows.Count; i++)
            {
                string[] split = lblSelectedRecord.Text.Split(',');
                Label lblconid = (Label)GvGroupBin.Rows[i].FindControl("lblgvBGroupId");
                for (int j = 0; j < lblSelectedRecord.Text.Split(',').Length; j++)
                {
                    if (lblSelectedRecord.Text.Split(',')[j] != "")
                    {
                        if (lblconid.Text.Trim().ToString() == lblSelectedRecord.Text.Split(',')[j].Trim().ToString())
                        {
                            ((CheckBox)GvGroupBin.Rows[i].FindControl("chkSelect")).Checked = true;
                        }
                    }
                }
            }
        }
        else
        {
            lblSelectedRecord.Text = "";
            DataTable dtUnit1 = (DataTable)Session["dtInactive"];
            GvGroupBin.DataSource = dtUnit1;
            GvGroupBin.DataBind();
            AllPageCode();
            ViewState["Select"] = null;
        }
    }
    protected void btnSave_Click(object sender, EventArgs e)
    {
        if (txtGroupName.Text == "")
        {
            DisplayMessage("Enter Group Name");
            System.Web.UI.ScriptManager.GetCurrent(this).SetFocus(txtGroupName);
            return;
        }

        string ddlValues;
        DataTable dt = ObjGroupMaster.GetGroupMasterByGroupName(StrCompId, txtGroupName.Text);
        int b = 0;
        if (editid.Value == "")
        {
            if (dt.Rows.Count == 0)
            {
                if (ddlPerentGroup.SelectedIndex == 0)
                {
                    ddlValues = "0";
                }
                else
                {
                    ddlValues = ddlPerentGroup.SelectedValue.ToString();
                }
                b = ObjGroupMaster.InsertGroupMaster(StrCompId, txtGroupName.Text, txtLGroupName.Text, ddlValues, "", "", "", "", "", "True", DateTime.Now.ToString(), true.ToString(), Session["UserId"].ToString(), DateTime.Now.ToString(), Session["UserId"].ToString(), DateTime.Now.ToString());
                if (b != 0)
                {
                    DisplayMessage("Record Saved");
                    Reset(1);
                }
                else
                {
                    DisplayMessage("Record Not Saved");
                }
            }
            else
            {
                DisplayMessage("Category Name Already Exists");
            }
        }
        else
        {
            if (dt.Rows.Count == 0) //condition added to check if new edited value already exists or not.
            {
                if (ddlPerentGroup.SelectedIndex == 0)
                {
                    ddlValues = "0";
                }
                else
                {
                    ddlValues = ddlPerentGroup.SelectedValue.ToString();
                }
                b = ObjGroupMaster.UpdateGroupMaster(StrCompId, editid.Value.ToString(), txtGroupName.Text, txtLGroupName.Text, ddlValues, "", "", "", "", "", "True", DateTime.Now.ToString(), true.ToString(), Session["UserId"].ToString(), DateTime.Now.ToString());
                if (b != 0)
                {
                    DisplayMessage("Record Updated");
                    btnList_Click(null, null);
                    Reset(1);
                }
                else
                {
                    DisplayMessage("Record Not Updated");
                }
            }
            else if (dt.Rows.Count == 1)
            {
                if (dt.Rows[0]["Group_Id"].ToString() == editid.Value)
                {
                    if (ddlPerentGroup.SelectedIndex == 0)
                    {
                        ddlValues = "0";
                    }
                    else
                    {
                        ddlValues = ddlPerentGroup.SelectedValue.ToString();
                    }

                    b = ObjGroupMaster.UpdateGroupMaster(StrCompId, editid.Value.ToString(), txtGroupName.Text, txtLGroupName.Text, ddlValues, "", "", "", "", "", "True", DateTime.Now.ToString(), true.ToString(), Session["UserId"].ToString(), DateTime.Now.ToString());
                    if (b != 0)
                    {
                        DisplayMessage("Record Updated");
                        Reset(1);
                        btnList_Click(null, null);
                    }
                    else
                    {
                        DisplayMessage("Record Not Updated");
                    }
                }
                else
                {
                    DisplayMessage("Category Name Already Exists");
                }
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
        FillParentDropDownListExcludingChildren(editid.Value); //For filling up parent ddl excluding children for current node, so that it cannot be moved to its children

        DataTable dt = ObjGroupMaster.GetGroupMasterByGroupId(StrCompId, editid.Value);
        if (dt.Rows.Count != 0)
        {
            txtGroupName.Text = dt.Rows[0]["Group_Name"].ToString();
            txtLGroupName.Text = dt.Rows[0]["Group_Name_L"].ToString();

            if (dt.Rows[0]["Parent_Id"].ToString() == "" || dt.Rows[0]["Parent_Id"].ToString() == "0")
            {
                ddlPerentGroup.SelectedIndex = 0;
            }
            else
            {
                DataTable dtp = ObjGroupMaster.GetGroupMasterTrueAllData(StrCompId);
                DataView dtv = new DataView(dtp);
                dtv.RowFilter = "Group_Id='" + dt.Rows[0]["Parent_Id"].ToString() + "'";
                dtp = dtv.ToTable();
                if (dtp.Rows.Count != 0)
                {
                    ddlPerentGroup.SelectedValue = dtp.Rows[0]["Group_Id"].ToString();
                }
                else
                {
                    ddlPerentGroup.SelectedIndex = 0;
                }
            }

            btnNew_Click(null, null);
            System.Web.UI.ScriptManager.GetCurrent(this).SetFocus(txtGroupName);
        }
    }
    protected void btnReset_Click(object sender, EventArgs e)
    {
        Reset(0);
        System.Web.UI.ScriptManager.GetCurrent(this).SetFocus(txtGroupName);
    }
    protected void btnCancel_Click(object sender, EventArgs e)
    {
        Reset(1);
        btnList_Click(null, null);
        FillGrid();
        FillGridBin();
        System.Web.UI.ScriptManager.GetCurrent(this).SetFocus(txtValue);
    }
    protected void GvGroup_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        GvGroup.PageIndex = e.NewPageIndex;

        DataTable dt = (DataTable)Session["dtFilter"];
        GvGroup.DataSource = dt;
        GvGroup.DataBind();
        AllPageCode();
    }
    protected void btnbind_Click(object sender, ImageClickEventArgs e)
    {
        string condition = string.Empty;
        if (ddlOption.SelectedIndex != 0)
        {
            if (ddlFieldName.SelectedValue == "ParentGroupName" && txtValue.Text == "")
            {
                condition = ddlFieldName.SelectedValue + " " + "is null";
            }
            else
            {
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
            }

            TreeViewCategory.Visible = false;
            GvGroup.Visible = true;
            btnGridView.ToolTip = Resources.Attendance.Tree_View; //Show grid if tree view is current shown

            DataTable dtCust = (DataTable)Session["Category"];
            DataView view = new DataView(dtCust, condition, "", DataViewRowState.CurrentRows);
            Session["dtFilter"] = view.ToTable();
            lblTotalRecords.Text = Resources.Attendance.Total_Records + " : " + view.ToTable().Rows.Count.ToString() + "";
            GvGroup.DataSource = view.ToTable();
            GvGroup.DataBind();
            AllPageCode();
        }
    }
    protected void btnRefreshReport_Click(object sender, ImageClickEventArgs e)
    {
        TreeViewCategory.Visible = false;
        GvGroup.Visible = true;
        btnGridView.ToolTip = Resources.Attendance.Tree_View; ;
        BindTreeView(); //Update TreeView on Refresh        
        FillGrid();
        ddlOption.SelectedIndex = 2;
        ddlFieldName.SelectedIndex = 1;
        txtValue.Text = "";
        System.Web.UI.ScriptManager.GetCurrent(this).SetFocus(txtValue);
    }
    protected void btnGridView_Click(object sender, ImageClickEventArgs e)
    {
        if (TreeViewCategory.Visible == true)//To show grid view
        {
            TreeViewCategory.Visible = false;
            GvGroup.Visible = true;
            btnGridView.ToolTip = Resources.Attendance.Tree_View;
        }
        else //To show tree view
        {
            btnGridView.ToolTip = Resources.Attendance.Grid_View;
            GvGroup.Visible = false;
            TreeViewCategory.Visible = true;
            trdel2.Visible = false;
            trdel.Visible = false;
            trgv.Visible = false;
            BindTreeView();
            FillGrid();
            txtValue.Text = "";
        }
    }
    protected void btnTreeView_Click(object sender, ImageClickEventArgs e)
    {
        if (TreeViewCategory.Visible == true)
        {
            TreeViewCategory.Visible = false;
            GvGroup.Visible = true;
            btnGridView.ToolTip = Resources.Attendance.Tree_View;
        }
        else
        {
            btnGridView.ToolTip = Resources.Attendance.Grid_View;
            GvGroup.Visible = false;
            TreeViewCategory.Visible = true;
            trdel2.Visible = false;
            trdel.Visible = false;
            trgv.Visible = false;
        }
    }
    protected void TreeViewCategory_SelectedNodeChanged(object sender, EventArgs e)
    {
        CommandEventArgs CmdEvntArgs = new CommandEventArgs("", (object)TreeViewCategory.SelectedValue.ToString());
        btnEdit_Command(sender, CmdEvntArgs);
    }
    protected void btnDeleteNode_Click(object sender, EventArgs e)
    {
        if (!rbtnmovechild.Checked && !rdbtndelchild.Checked && !trdel.Visible)
        {
            //Lower most node delete code
            int b = 0;
            b = ObjGroupMaster.DeleteGroupMaster(StrCompId, Session["DeleteNodeValue"].ToString(), false.ToString(), Session["UserId"].ToString(), DateTime.Now.ToString());
            if (b != 0)
            {
                ObjGroupMaster.UpdateParentId(StrCompId, Session["DeleteNodeValue"].ToString());
                DisplayMessage("Parent Group Is Deleted");
                DisplayMessage("Record has been Deleted And Moved To Bin");
                FillGridBin(); //Update Bin tab
                FillGrid();
                FillPerentDDl();
                BindTreeView(); //Update tree view if Record has been Deleted And Moved To Bin
                trgv.Visible = false;
                trdel.Visible = false;
                trdel2.Visible = false;
                Reset(1);
            }
            else
            {
                DisplayMessage("Record Not Deleted");
            }

            panelOverlay.Visible = false;
            panelPopUpPanel.Visible = false;
        }
        else if (rbtnmovechild.Checked && !rdbtndelchild.Checked)
        {
            //Move child node to selected node
            DataTable dt1 = GetProductCategoryByParentId(Session["DeleteNodeValue"].ToString());
            string[] ChildrenNodeId = new string[dt1.Rows.Count];
            for (int i = 0; i < dt1.Rows.Count; i++)
            {
                ChildrenNodeId[i] = dt1.Rows[i]["Group_Id"].ToString();
            }

            for (int i = 0; i < dt1.Rows.Count; i++)
            {
                if (ddlgroup0.SelectedValue.ToString() == "--Select--")
                    ObjGroupMaster.UpdateParentIdbyCategoryId(StrCompId, ChildrenNodeId[i], "");
                else
                    ObjGroupMaster.UpdateParentIdbyCategoryId(StrCompId, ChildrenNodeId[i], ddlgroup0.SelectedValue.ToString());
            }

            int b = 0;
            b = ObjGroupMaster.DeleteGroupMaster(StrCompId, Session["DeleteNodeValue"].ToString(), false.ToString(), Session["UserId"].ToString(), DateTime.Now.ToString());
            if (b != 0)
            {
                ObjGroupMaster.UpdateParentId(StrCompId, Session["DeleteNodeValue"].ToString());
                DisplayMessage("Record has been Deleted And Moved To Bin");
                FillGridBin(); //Update Bin Tab
                FillGrid();
                FillPerentDDl();
                BindTreeView();//Update tree view if Record has been Deleted And Moved To Bin
                trgv.Visible = false;
                trdel.Visible = false;
                trdel2.Visible = false;
            }
            else
            {
                DisplayMessage("Record Not Deleted");
            }

            Reset(1);
            panelOverlay.Visible = false;
            panelPopUpPanel.Visible = false;
        }
        else if (!rbtnmovechild.Checked && rdbtndelchild.Checked)
        {
            //Delete all children of that node
            FindChildNode(Session["DeleteNodeValue"].ToString());
            for (int i = 0; i < arr.Count; i++)
            {
                ObjGroupMaster.DeleteGroupMaster(StrCompId, arr[i].ToString(), false.ToString(), Session["UserId"].ToString(), DateTime.Now.ToString());
                ObjGroupMaster.UpdateParentId(StrCompId, arr[i].ToString());
            }

            DisplayMessage("Record has been Deleted And Moved To Bin");
            FillGridBin(); //Update bin tab
            FillGrid();
            FillPerentDDl();
            BindTreeView(); //Update tree view if Record has been Deleted And Moved To Bin
            trgv.Visible = false;
            trdel.Visible = false;
            trdel2.Visible = false;

            Reset(1);
            panelOverlay.Visible = false;
            panelPopUpPanel.Visible = false;
        }
    }
    protected void rdbtndelchild_CheckedChanged(object sender, EventArgs e)
    {
        btnDeleteNode.Text = Resources.Attendance.Delete_Child_Also.ToString();
        trdel2.Visible = true;
        ddlgroup0.Visible = false;
    }
    protected void rbtnmovechild_CheckedChanged(object sender, EventArgs e)
    {
        btnDeleteNode.Text = Resources.Attendance.Move_Child.ToString();
        trdel2.Visible = true;
        ddlgroup0.Visible = true;
    }
    protected void btnCancelDelete_Click(object sender, EventArgs e)
    {
        trgv.Visible = false;
        trdel.Visible = false;
        trdel2.Visible = false;
        ddlgroup0.Visible = false;
        rbtnmovechild.Checked = false;
        rdbtndelchild.Checked = false;
        Session["DeleteNodeValue"] = "";
        btnDeleteNode.Text = Resources.Attendance.Delete.ToString();
        panelOverlay.Visible = false;
        panelPopUpPanel.Visible = false;
    }
    protected void IbtnDelete_Command(object sender, CommandEventArgs e)//delete(in grid) button click event modified
    {
        //Code to show information of Category to delete 
        DataTable dtable = ObjGroupMaster.GetGroupMasterByGroupId(StrCompId, e.CommandArgument.ToString());
        lblDelGroupId.Text = /*Resources.Attendance.Category_Id.ToString() + " : " + */dtable.Rows[0]["Group_Id"].ToString();
        lblDelGroupName.Text = /*Resources.Attendance.Category_Name.ToString() + " : " + */dtable.Rows[0]["Group_Name"].ToString();
        if (dtable.Rows[0]["Parent_Id"].ToString() != "0")
        {
            DataTable dtable2 = ObjGroupMaster.GetGroupMasterByGroupId(StrCompId, dtable.Rows[0]["Parent_Id"].ToString());
            lblDelParentGroup.Text = /*Resources.Attendance.Parent_Category.ToString() + " : " + */dtable2.Rows[0]["Group_Name"].ToString();
            rowDelParentCategory.Visible = true;
        }
        else
        {
            rowDelParentCategory.Visible = false;
            lblDelParentGroup.Text = /*Resources.Attendance.Parent_Category.ToString() + " : ---";*/ "---";
        }
        //For modal dialog box
        btnCancelDelete_Click(null, null);
        panelOverlay.Visible = true;
        panelPopUpPanel.Visible = true;
        //Check before delete if child is present
        DataTable dt = GetProductCategoryByParentId(e.CommandArgument.ToString());
        if (dt.Rows.Count > 0)
        {
            trgv.Visible = true;
            trdel.Visible = true;
            rbtnmovechild.Visible = true;
            rdbtndelchild.Visible = true;
            FillMoveChildDropDownList(e.CommandArgument.ToString());
            Session["DeleteNodeValue"] = e.CommandArgument.ToString();
        }
        else
        {
            trgv.Visible = true;
            trdel2.Visible = true;
            Session["DeleteNodeValue"] = e.CommandArgument.ToString();
        }
    }
    protected void GvGroupBin_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        GvGroupBin.PageIndex = e.NewPageIndex;
        DataTable dt = (DataTable)Session["dtInactive"];
        GvGroupBin.DataSource = dt;
        GvGroupBin.DataBind();

        string temp = string.Empty;

        for (int i = 0; i < GvGroupBin.Rows.Count; i++)
        {
            Label lblconid = (Label)GvGroupBin.Rows[i].FindControl("lblGroupName");
            string[] split = lblSelectedRecord.Text.Split(',');

            for (int j = 0; j < lblSelectedRecord.Text.Split(',').Length; j++)
            {
                if (lblSelectedRecord.Text.Split(',')[j] != "")
                {
                    if (lblconid.Text.Trim().ToString() == lblSelectedRecord.Text.Split(',')[j].Trim().ToString())
                    {
                        ((CheckBox)GvGroupBin.Rows[i].FindControl("chkSelect")).Checked = true;
                    }
                }
            }
        }
    }
    protected void GvGroupBin_OnSorting(object sender, GridViewSortEventArgs e)
    {
        SortExpression = e.SortExpression;
        HDFSortbin.Value = HDFSortbin.Value == "ASC" ? "DESC" : "ASC";
        DataTable dt = new DataTable();
        dt = ObjGroupMaster.GetGroupMasterFalseAllData(StrCompId);
        DataView dv = new DataView(dt);
        string Query = "" + SortExpression + " " + HDFSortbin.Value + "";
        dv.Sort = Query;
        dt = dv.ToTable();
        Session["dtInactive"] = dt;
        GvGroupBin.DataSource = dt;
        GvGroupBin.DataBind();
        AllPageCode();
        lblSelectedRecord.Text = "";
    }
    protected void GvGroup_OnSorting(object sender, GridViewSortEventArgs e)
    {
        SortExpression = e.SortExpression;
        HDFSort.Value = HDFSort.Value == "ASC" ? "DESC" : "ASC";
        DataTable dt = new DataTable();
        dt = (DataTable)Session["dtFilter"];
        DataView dv = new DataView(dt);
        string Query = "" + SortExpression + " " + HDFSort.Value + "";
        dv.Sort = Query;
        dt = dv.ToTable();
        Session["dtFilter"] = dt;
        GvGroup.DataSource = dt;
        GvGroup.DataBind();
        AllPageCode();
    }
    //Search panel on Bin Tab
    protected void btnRefreshBin_Click(object sender, EventArgs e)
    {
        BindTreeView(); //Update TreeView on Refresh
        FillGridBin(); //Update Bin Tab
        FillGrid();

        txtValueBin.Text = "";
        ddlOptionBin.SelectedIndex = 2;
        ddlFieldNameBin.SelectedIndex = 1;
        lblSelectedRecord.Text = "";
        System.Web.UI.ScriptManager.GetCurrent(this).SetFocus(txtValueBin);
    }
    protected void btnbindBin_Click(object sender, EventArgs e)
    {
        string condition = string.Empty;
        if (ddlOptionBin.SelectedIndex != 0)
        {
            if (ddlFieldNameBin.SelectedValue == "ParentGroupName" && txtValueBin.Text == "")
            {
                condition = ddlFieldNameBin.SelectedValue + " " + "is null";
            }
            else
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
            }

            DataTable dtCust = (DataTable)Session["CategoryBin"];
            DataView view = new DataView(dtCust, condition, "", DataViewRowState.CurrentRows);
            Session["dtInactive"] = view.ToTable();
            lblTotalRecordsBin.Text = Resources.Attendance.Total_Records + " : " + view.ToTable().Rows.Count.ToString() + "";
            GvGroupBin.DataSource = view.ToTable();
            GvGroupBin.DataBind();
            lblSelectedRecord.Text = "";
            if (view.ToTable().Rows.Count == 0)
            {
                ImgbtnSelectAll.Visible = false;
                btnRestoreSelected.Visible = false;
            }
            else
            {
                AllPageCode();
            }
            System.Web.UI.ScriptManager.GetCurrent(this).SetFocus(txtValueBin);
        }
    }
    protected void txtGroupName_TextChanged(object sender, EventArgs e)
    {
        if (editid.Value == "")
        {
            DataTable dt = ObjGroupMaster.GetGroupMasterByGroupName(StrCompId, txtGroupName.Text);
            if (dt.Rows.Count > 0)
            {
                txtGroupName.Text = "";
                DisplayMessage("Group Name Already Exists");
                System.Web.UI.ScriptManager.GetCurrent(this).SetFocus(txtGroupName);
                return;
            }
            DataTable dt1 = ObjGroupMaster.GetGroupMasterFalseAllData(StrCompId);
            dt1 = new DataView(dt1, "Group_Name='" + txtGroupName.Text + "'", "", DataViewRowState.CurrentRows).ToTable();
            if (dt1.Rows.Count > 0)
            {
                txtGroupName.Text = "";
                DisplayMessage("Group Name Already Exists - Go to Bin Tab");
                System.Web.UI.ScriptManager.GetCurrent(this).SetFocus(txtGroupName);
                return;
            }
        }
        else
        {
            DataTable dtTemp = ObjGroupMaster.GetGroupMasterByGroupId(StrCompId, editid.Value);
            if (dtTemp.Rows.Count > 0)
            {
                if (dtTemp.Rows[0]["Group_Name"].ToString() != txtGroupName.Text)
                {
                    DataTable dt = ObjGroupMaster.GetGroupMasterByGroupName(StrCompId, txtGroupName.Text);
                    if (dt.Rows.Count > 0)
                    {
                        txtGroupName.Text = "";
                        DisplayMessage("Group Name Already Exists");
                        System.Web.UI.ScriptManager.GetCurrent(this).SetFocus(txtGroupName);
                        return;
                    }
                    DataTable dt1 = ObjGroupMaster.GetGroupMasterFalseAllData(StrCompId);
                    dt1 = new DataView(dt1, "Group_Name='" + txtGroupName.Text + "'", "", DataViewRowState.CurrentRows).ToTable();
                    if (dt1.Rows.Count > 0)
                    {
                        txtGroupName.Text = "";
                        DisplayMessage("Group Name Already Exists - Go to Bin Tab");
                        System.Web.UI.ScriptManager.GetCurrent(this).SetFocus(txtGroupName);
                        return;
                    }
                }
            }
        }
        System.Web.UI.ScriptManager.GetCurrent(this).SetFocus(txtLGroupName);
    }
    protected void btnRestoreSelected_Click(object sender, CommandEventArgs e)
    {
        int Msg = 0;
        DataTable dt = ObjGroupMaster.GetGroupMasterFalseAllData(StrCompId);
        if (GvGroupBin.Rows.Count != 0)
        {
            if (lblSelectedRecord.Text != "")
            {
                for (int j = 0; j < lblSelectedRecord.Text.Split(',').Length; j++)
                {
                    if (lblSelectedRecord.Text.Split(',')[j] != "")
                    {
                        Msg = ObjGroupMaster.DeleteGroupMaster(StrCompId, lblSelectedRecord.Text.Split(',')[j].Trim().ToString(), true.ToString(), Session["UserId"].ToString(), DateTime.Now.ToString());
                        if (Msg != 0)
                        {
                            ObjGroupMaster.UpdateParentId(StrCompId, lblSelectedRecord.Text.Split(',')[j].Trim().ToString().ToString());
                        }
                    }
                }
            }

            if (Msg != 0)
            {
                btnRefreshBin_Click(null, null);
                lblSelectedRecord.Text = "";
                DisplayMessage("Record Activate");
                System.Web.UI.ScriptManager.GetCurrent(this).SetFocus(txtValue);
            }
            else
            {
                int fleg = 0;
                foreach (GridViewRow Gvr in GvGroupBin.Rows)
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
        CheckBox chkSelAll = ((CheckBox)GvGroupBin.HeaderRow.FindControl("chkCurrent"));
        for (int i = 0; i < GvGroupBin.Rows.Count; i++)
        {
            ((CheckBox)GvGroupBin.Rows[i].FindControl("chkSelect")).Checked = chkSelAll.Checked;
            if (chkSelAll.Checked)
            {
                if (!lblSelectedRecord.Text.Split(',').Contains(((Label)(GvGroupBin.Rows[i].FindControl("lblgvBGroupId"))).Text.Trim().ToString()))
                {
                    lblSelectedRecord.Text += ((Label)(GvGroupBin.Rows[i].FindControl("lblgvBGroupId"))).Text.Trim().ToString() + ",";
                }
            }
            else
            {
                string temp = string.Empty;
                string[] split = lblSelectedRecord.Text.Split(',');
                foreach (string item in split)
                {
                    if (item != ((Label)(GvGroupBin.Rows[i].FindControl("lblgvBGroupId"))).Text.Trim().ToString())
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
        Label lb = (Label)GvGroupBin.Rows[index].FindControl("lblgvBGroupId");
        if (((CheckBox)GvGroupBin.Rows[index].FindControl("chkSelect")).Checked)
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
    protected void btnClosePanel_Click(object sender, EventArgs e)
    {
        Reset(1);
        panelOverlay.Visible = false;
        panelPopUpPanel.Visible = false;
    }
    #endregion

    #region Auto Complete Funcation
    [System.Web.Services.WebMethodAttribute(), System.Web.Script.Services.ScriptMethodAttribute()]
    public static string[] GetCompletionList(string prefixText, int count, string contextKey)
    {
        Ems_GroupMaster ObjGroupMaster = new Ems_GroupMaster();
        DataTable dt = ObjGroupMaster.GetDistinctGroupName(HttpContext.Current.Session["CompId"].ToString(), prefixText);
        string[] txt = new string[dt.Rows.Count];

        for (int i = 0; i < dt.Rows.Count; i++)
        {
            txt[i] = dt.Rows[i]["Group_Name"].ToString() + "";
        }
        return txt;
    }
    #endregion

    #region User defind Funcation
    public void FillGrid()
    {
        DataTable dt = ObjGroupMaster.GetGroupMasterTrueAllData(StrCompId);
        if (dt.Rows.Count > 0)
        {
            GvGroup.DataSource = dt;
            GvGroup.DataBind();
        }
        else
        {
            GvGroup.DataSource = null;
            GvGroup.DataBind();
        }

        Session["Category"] = dt;
        Session["dtFilter"] = dt;
        AllPageCode();
        lblTotalRecords.Text = Resources.Attendance.Total_Records + " : " + dt.Rows.Count.ToString() + "";
    }
    public void FillPerentDDl()
    {
        DataTable dt = ObjGroupMaster.GetGroupMasterTrueAllData(StrCompId);
        ddlPerentGroup.DataSource = dt;
        ddlPerentGroup.DataTextField = "Group_Name";
        ddlPerentGroup.DataValueField = "Group_Id";
        ddlPerentGroup.DataBind();
        ddlPerentGroup.Items.Insert(0, "--Select--");
        ddlPerentGroup.SelectedIndex = 0;
    }
    public void Reset(int RC)
    {
        if (RC == 1)
        {
            txtGroupName.Text = "";
            txtLGroupName.Text = "";
            ddlPerentGroup.SelectedIndex = 0;
            btnNew.Text = Resources.Attendance.New;
            editid.Value = "";
            ddlFieldName.SelectedIndex = 1;
            ddlOption.SelectedIndex = 2;
            txtValue.Text = "";
            ddlFieldNameBin.SelectedIndex = 1;
            ddlOptionBin.SelectedIndex = 2;
            txtValueBin.Text = "";
            System.Web.UI.ScriptManager.GetCurrent(this).SetFocus(txtValue);
        }
        else
        {
            txtGroupName.Text = "";
            txtLGroupName.Text = "";
            ddlPerentGroup.SelectedIndex = 0;
            btnNew.Text = Resources.Attendance.New;
            editid.Value = "";
            ddlFieldName.SelectedIndex = 1;
            ddlOption.SelectedIndex = 2;
            txtValue.Text = "";
            ddlFieldNameBin.SelectedIndex = 1;
            ddlOptionBin.SelectedIndex = 2;
            txtValueBin.Text = "";
            System.Web.UI.ScriptManager.GetCurrent(this).SetFocus(txtGroupName);
        }
        //delete functionality and BIN tab 
        btnDeleteNode.Text = Resources.Attendance.Delete.ToString();
        Session["DeleteNodeValue"] = "";
        arr.Clear();
        TreeViewCategory.Visible = false;
        GvGroup.Visible = true;
        FillGrid();
        btnGridView.ToolTip = Resources.Attendance.Tree_View;
        btnNew.Text = Resources.Attendance.New;
        lblSelectedRecord.Text = "";
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
    public string GetParent(object Pid)
    {
        string retval = String.Empty;
        if (Pid.ToString() != "")
        {
            DataTable dt = ObjGroupMaster.GetGroupMasterByGroupId(StrCompId, Pid.ToString());
            if (dt.Rows.Count != 0)
            {
                retval = dt.Rows[0]["Group_Name"].ToString();
            }
            return retval;
        }
        else
        {
            return retval;
        }
    }
    //To apply tree view, delete options and BIN tab
    private void BindTreeView()//fucntion to fill up TreeView according to parent child nodes
    {
        TreeViewCategory.Nodes.Clear();
        DataTable dt = new DataTable();
        string x = "Parent_Id=" + "'0'" + "";
        dt = ObjGroupMaster.GetGroupMasterTrueAllData(StrCompId);
        dt = new DataView(dt, x, "", DataViewRowState.OriginalRows).ToTable();
        int i = 0;
        while (i < dt.Rows.Count)
        {
            TreeNode tn = new TreeNode();
            tn.Text = dt.Rows[i]["Group_Name"].ToString();
            tn.Value = dt.Rows[i]["Group_Id"].ToString();
            TreeViewCategory.Nodes.Add(tn);
            FillChild((dt.Rows[i]["Group_Id"].ToString()), tn);
            i++;
        }
        TreeViewCategory.DataBind();
    }
    private void FillChild(string index, TreeNode tn)//fill up child nodes and respective child nodes of them 
    {
        DataTable dt = new DataTable();
        dt = GetProductCategoryByParentId(index);

        int i = 0;
        while (i < dt.Rows.Count)
        {
            TreeNode tn1 = new TreeNode();
            tn1.Text = dt.Rows[i]["Group_Name"].ToString();
            tn1.Value = dt.Rows[i]["Group_Id"].ToString();
            tn.ChildNodes.Add(tn1);
            FillChild((dt.Rows[i]["Group_Id"].ToString()), tn1);
            i++;
        }
        TreeViewCategory.DataBind();
    }
    public DataTable GetProductCategoryByParentId(string ParentId) //Function to get entries of same ProductId
    {
        DataTable dt = ObjGroupMaster.GetGroupMasterByGroupId(StrCompId, "");
        string query = "Parent_Id='" + ParentId + "'";
        dt = ObjGroupMaster.GetGroupMasterTrueAllData(StrCompId);
        dt = new DataView(dt, query, "", DataViewRowState.OriginalRows).ToTable();
        return dt;
    }
    public void FillMoveChildDropDownList(string strExceptId) //Function to fill up items in drop down list of New Parent after delete
    {
        DataTable dt = ObjGroupMaster.GetGroupMasterTrueAllData(StrCompId);
        string query = "Group_Id not in(";

        FindChildNode(strExceptId);

        for (int i = 0; i < arr.Count; i++)
        {
            query += "'" + arr[i].ToString() + "',";
        }
        query = query.Substring(0, query.Length - 1).ToString() + ")";
        dt = new DataView(dt, query, "", DataViewRowState.OriginalRows).ToTable();

        ddlgroup0.DataSource = dt;
        ddlgroup0.DataTextField = "Group_Name";
        ddlgroup0.DataValueField = "Group_Id";
        ddlgroup0.DataBind();
        ddlgroup0.Items.Insert(0, "--Select--");
        ddlgroup0.SelectedIndex = 0;

        arr.Clear();
    }
    private void FindChildNode(string p)  //Function to find child nodes and child of child nodes and so on
    {
        arr.Add(p);
        DataTable dt = GetProductCategoryByParentId(p.ToString());
        if (dt != null && dt.Rows.Count > 0)
        {
            for (int i = 0; i < dt.Rows.Count; i++)
            {
                FindChildNode(dt.Rows[i]["Group_Id"].ToString());
            }
        }
        else
        {
            return;
        }
    }
    public void FillGridBin()//Function to fill up Inactive items grid in Bin Tab...
    {
        DataTable dt = new DataTable();
        dt = ObjGroupMaster.GetGroupMasterFalseAllData(StrCompId);
        GvGroupBin.DataSource = dt;
        GvGroupBin.DataBind();
        Session["CategoryBin"] = dt;
        Session["dtInactive"] = dt;
        if (dt != null)
            lblTotalRecordsBin.Text = Resources.Attendance.Total_Records + " : " + dt.Rows.Count.ToString() + "";
        else lblTotalRecordsBin.Text = Resources.Attendance.Total_Records + " : " + "0";

        lblSelectedRecord.Text = "";
        if (dt.Rows.Count == 0)
        {
            ImgbtnSelectAll.Visible = false;
            btnRestoreSelected.Visible = false;
        }
        else
        {
            AllPageCode();
        }
    }
    public void FillParentDropDownListExcludingChildren(string strExceptId)
    {
        DataTable dt = ObjGroupMaster.GetGroupMasterTrueAllData(StrCompId);
        string query = "Group_Id not in(";

        FindChildNode(strExceptId);

        for (int i = 0; i < arr.Count; i++)
        {
            query += "'" + arr[i].ToString() + "',";
        }
        query = query.Substring(0, query.Length - 1).ToString() + ")";
        dt = new DataView(dt, query, "", DataViewRowState.OriginalRows).ToTable();

        ddlPerentGroup.DataSource = dt;
        ddlPerentGroup.DataTextField = "Group_Name";
        ddlPerentGroup.DataValueField = "Group_Id";
        ddlPerentGroup.DataBind();
        ddlPerentGroup.Items.Insert(0, "--Select--");
        ddlPerentGroup.SelectedIndex = 0;

        arr.Clear();
    }
    #endregion
}
