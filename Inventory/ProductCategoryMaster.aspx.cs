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

public partial class Inventory_ProductCategoryMaster : BasePage
{
    #region defind Class Object
    DataAccessClass daClass = new DataAccessClass();
    Inv_ProductCategoryMaster ObjProductCateMaster = new Inv_ProductCategoryMaster();
    SystemParameter ObjSysPeram = new SystemParameter();
    Common cmn = new Common();
    ArrayList arr = new ArrayList();
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
        Session["AccordianId"] = "11";
        Session["HeaderText"] = "Inventory";
        DataTable dtAllPageCode = cmn.GetAllPagePermission(StrUserId.ToString(), "11", "23");
        if (dtAllPageCode.Rows.Count != 0)
        {
            if (dtAllPageCode.Rows[0][0].ToString() == "SuperAdmin")
            {
                btnSave.Visible = true;
                foreach (GridViewRow Row in gvCategoryProduct.Rows)
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
                    foreach (GridViewRow Row in gvCategoryProduct.Rows)
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
        txtCategoryName.Focus();
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
        txtValueBin.Focus();
    }
    protected void ImgbtnSelectAll_Click(object sender, ImageClickEventArgs e)
    {
        DataTable dtPbrand = (DataTable)Session["dtInactive"];

        if (ViewState["Select"] == null)
        {
            ViewState["Select"] = 1;
            foreach (DataRow dr in dtPbrand.Rows)
            {
                if (!lblSelectedRecord.Text.Split(',').Contains(dr["Category_Id"]))
                {
                    lblSelectedRecord.Text += dr["Category_Id"] + ",";
                }
            }
            for (int i = 0; i < gvProductCategoryBin.Rows.Count; i++)
            {
                string[] split = lblSelectedRecord.Text.Split(',');
                Label lblconid = (Label)gvProductCategoryBin.Rows[i].FindControl("lblCategoryId");
                for (int j = 0; j < lblSelectedRecord.Text.Split(',').Length; j++)
                {
                    if (lblSelectedRecord.Text.Split(',')[j] != "")
                    {
                        if (lblconid.Text.Trim().ToString() == lblSelectedRecord.Text.Split(',')[j].Trim().ToString())
                        {
                            ((CheckBox)gvProductCategoryBin.Rows[i].FindControl("chkSelect")).Checked = true;
                        }
                    }
                }
            }
        }
        else
        {
            lblSelectedRecord.Text = "";
            DataTable dtUnit1 = (DataTable)Session["dtInactive"];
            gvProductCategoryBin.DataSource = dtUnit1;
            gvProductCategoryBin.DataBind();
            AllPageCode();
            ViewState["Select"] = null;
        }
        ImgbtnSelectAll.Focus();
    }
    protected void btnSave_Click(object sender, EventArgs e)
    {
        if (txtCategoryName.Text == "")
        {
            DisplayMessage("Enter Category Name");
            txtCategoryName.Focus();
            return;
        }

        string ddlValues;
        DataTable dt = ObjProductCateMaster.GetProductCategoryByCategoryName(StrCompId, txtCategoryName.Text);
        int b = 0;
        if (editid.Value == "")
        {
            if (dt.Rows.Count == 0)
            {
                if (ddlPerentCategory.SelectedIndex == 0)
                {
                    ddlValues = "";
                }
                else
                {
                    ddlValues = ddlPerentCategory.SelectedValue.ToString();
                }
                b = ObjProductCateMaster.InsertProductCategoryMaster(StrCompId, StrBrandId, txtCategoryName.Text, txtLCategoryName.Text, "", ddlValues, txtDescription.Text, "", "", "", "", "", "True", DateTime.Now.ToString(), true.ToString(), Session["UserId"].ToString(), DateTime.Now.ToString(), Session["UserId"].ToString(), DateTime.Now.ToString());
                if (b != 0)
                {
                    DisplayMessage("Record Saved");
                    Reset(1);
                    txtCategoryName.Focus();
                }
                else
                {
                    DisplayMessage("Record Not Saved");
                }
            }
            else
            {
                DisplayMessage("Category Name Already Exists");
                txtCategoryName.Focus();
                txtCategoryName.Focus();
            }
        }
        else
        {
            if (dt.Rows.Count == 0) //condition added to check if new edited value already exists or not.
            {
                if (ddlPerentCategory.SelectedIndex == 0)
                {
                    ddlValues = "";
                }
                else
                {
                    ddlValues = ddlPerentCategory.SelectedValue.ToString();
                }
                b = ObjProductCateMaster.UpdateProductCategoryMaster(StrCompId, StrBrandId, editid.Value.ToString(), txtCategoryName.Text, txtLCategoryName.Text, "", ddlValues, txtDescription.Text, "", "", "", "", "", "True", DateTime.Now.ToString(), true.ToString(), Session["UserId"].ToString(), DateTime.Now.ToString());
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
                if (dt.Rows[0]["Category_Id"].ToString() == editid.Value)
                {
                    if (ddlPerentCategory.SelectedIndex == 0)
                    {
                        ddlValues = "";
                    }
                    else
                    {
                        ddlValues = ddlPerentCategory.SelectedValue.ToString();
                    }

                    b = ObjProductCateMaster.UpdateProductCategoryMaster(StrCompId, StrBrandId, editid.Value.ToString(), txtCategoryName.Text, txtLCategoryName.Text, "", ddlValues, txtDescription.Text, "", "", "", "", "", "True", DateTime.Now.ToString(), true.ToString(), Session["UserId"].ToString(), DateTime.Now.ToString());
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
                    txtCategoryName.Focus();
                    txtCategoryName.Text = "";

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

        DataTable dt = ObjProductCateMaster.GetProductCategoryByCategoryId(StrCompId, editid.Value);
        if (dt.Rows.Count != 0)
        {
            txtCategoryName.Text = dt.Rows[0]["Category_Name"].ToString();
            txtLCategoryName.Text = dt.Rows[0]["Category_Name_L"].ToString();

            if (dt.Rows[0]["Parent_Id"].ToString() == "" || dt.Rows[0]["Parent_Id"].ToString() == "0")
            {
                ddlPerentCategory.SelectedIndex = 0;
            }
            else
            {
                DataTable dtp = ObjProductCateMaster.GetProductCategoryTrueAllData(StrCompId);
                DataView dtv = new DataView(dtp);
                dtv.RowFilter = "Category_Id='" + dt.Rows[0]["Parent_Id"].ToString() + "'";
                dtp = dtv.ToTable();
                if (dtp.Rows.Count != 0)
                {
                    ddlPerentCategory.SelectedValue = dtp.Rows[0]["Category_Id"].ToString();
                }
                else
                {
                    ddlPerentCategory.SelectedIndex = 0;
                }
            }
            txtDescription.Text = dt.Rows[0]["Description"].ToString();
            
            btnNew_Click(null, null);
            btnNew.Text = Resources.Attendance.Edit;

        }
    }
    protected void btnReset_Click(object sender, EventArgs e)
    {
        Reset(0);
        txtCategoryName.Focus();
    }
    protected void btnCancel_Click(object sender, EventArgs e)
    {
        Reset(1);
        btnList_Click(null, null);
        FillGrid();
        FillGridBin();
        txtValue.Focus();
    }
    protected void gvCategoryProduct_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        gvCategoryProduct.PageIndex = e.NewPageIndex;

        DataTable dt = (DataTable)Session["dtFilter"];
        gvCategoryProduct.DataSource = dt;
        gvCategoryProduct.DataBind();
        AllPageCode();
        gvCategoryProduct.BottomPagerRow.Focus();
    }
    protected void btnbind_Click(object sender, ImageClickEventArgs e)
    {
        string condition = string.Empty;
        if (ddlOption.SelectedIndex != 0)
        {
            if (ddlFieldName.SelectedValue == "ParentCategory" && txtValue.Text == "")
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
            gvCategoryProduct.Visible = true;
            btnGridView.ToolTip = Resources.Attendance.Tree_View; //Show grid if tree view is current shown

            DataTable dtCust = (DataTable)Session["Category"];
            DataView view = new DataView(dtCust, condition, "", DataViewRowState.CurrentRows);
            Session["dtFilter"] = view.ToTable();
            lblTotalRecords.Text = Resources.Attendance.Total_Records + " : " + view.ToTable().Rows.Count.ToString() + "";
            gvCategoryProduct.DataSource = view.ToTable();
            gvCategoryProduct.DataBind();
            AllPageCode();
            btnbind.Focus();
        }
    }
    protected void btnRefreshReport_Click(object sender, ImageClickEventArgs e)
    {
        TreeViewCategory.Visible = false;
        gvCategoryProduct.Visible = true;
        btnGridView.ToolTip = Resources.Attendance.Tree_View; ;
        BindTreeView(); //Update TreeView on Refresh        
        FillGrid();
        ddlOption.SelectedIndex = 2;
        ddlFieldName.SelectedIndex = 1;
        txtValue.Text = "";
        txtValue.Focus();
    }
    protected void btnGridView_Click(object sender, ImageClickEventArgs e)
    {
        if (TreeViewCategory.Visible == true)//To show grid view
        {
            TreeViewCategory.Visible = false;
            gvCategoryProduct.Visible = true;
            btnGridView.ToolTip = Resources.Attendance.Tree_View;
        }
        else //To show tree view
        {
            btnGridView.ToolTip = Resources.Attendance.Grid_View;
            gvCategoryProduct.Visible = false;
            TreeViewCategory.Visible = true;
            trdel2.Visible = false;
            trdel.Visible = false;
            trgv.Visible = false;
            BindTreeView();
            FillGrid();
            txtValue.Text = "";
        }
        btnGridView.Focus();
    }
    protected void btnTreeView_Click(object sender, ImageClickEventArgs e)
    {
        if (TreeViewCategory.Visible == true)
        {
            TreeViewCategory.Visible = false;
            gvCategoryProduct.Visible = true;
            btnGridView.ToolTip = Resources.Attendance.Tree_View;
        }
        else
        {
            btnGridView.ToolTip = Resources.Attendance.Grid_View;
            gvCategoryProduct.Visible = false;
            TreeViewCategory.Visible = true;
            trdel2.Visible = false;
            trdel.Visible = false;
            trgv.Visible = false;
        }
        btnTreeView.Focus();
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
            b = ObjProductCateMaster.DeleteProductCategoryMaster(StrCompId, Session["DeleteNodeValue"].ToString(), false.ToString(), Session["UserId"].ToString(), DateTime.Now.ToString());
            if (b != 0)
            {
                ObjProductCateMaster.UpdateParentId(StrCompId, Session["DeleteNodeValue"].ToString());
                DisplayMessage("Record Deleted");
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
                ChildrenNodeId[i] = dt1.Rows[i]["Category_Id"].ToString();
            }

            for (int i = 0; i < dt1.Rows.Count; i++)
            {
                if (ddlgroup0.SelectedValue.ToString() == "--Select--")
                    ObjProductCateMaster.UpdateParentIdbyCategoryId(StrCompId, ChildrenNodeId[i], "");
                else
                    ObjProductCateMaster.UpdateParentIdbyCategoryId(StrCompId, ChildrenNodeId[i], ddlgroup0.SelectedValue.ToString());
            }

            int b = 0;
            b = ObjProductCateMaster.DeleteProductCategoryMaster(StrCompId, Session["DeleteNodeValue"].ToString(), false.ToString(), Session["UserId"].ToString(), DateTime.Now.ToString());
            if (b != 0)
            {
                ObjProductCateMaster.UpdateParentId(StrCompId, Session["DeleteNodeValue"].ToString());
                DisplayMessage("Record Deleted");
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
                ObjProductCateMaster.DeleteProductCategoryMaster(StrCompId, arr[i].ToString(), false.ToString(), Session["UserId"].ToString(), DateTime.Now.ToString());
                ObjProductCateMaster.UpdateParentId(StrCompId, arr[i].ToString());
            }

            DisplayMessage("Record Deleted");
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
        txtValue.Focus();
    }
    protected void rdbtndelchild_CheckedChanged(object sender, EventArgs e)
    {
        btnDeleteNode.Text = Resources.Attendance.Delete_Child_Also.ToString();
        trdel2.Visible = true;
        ddlgroup0.Visible = false;
        rdbtndelchild.Focus();
    }
    protected void rbtnmovechild_CheckedChanged(object sender, EventArgs e)
    {
        btnDeleteNode.Text = Resources.Attendance.Move_Child.ToString();
        trdel2.Visible = true;
        ddlgroup0.Visible = true;
        rbtnmovechild.Focus();
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
        txtValue.Focus();
    }
    protected void IbtnDelete_Command(object sender, CommandEventArgs e)//delete(in grid) button click event modified
    {
        //Code to show information of Category to delete 
        DataTable dtable = ObjProductCateMaster.GetProductCategoryByCategoryId(StrCompId, e.CommandArgument.ToString());
        lblDelCategoryId.Text = /*Resources.Attendance.Category_Id.ToString() + " : " + */dtable.Rows[0]["Category_Id"].ToString();
        lblDelCategoryName.Text = /*Resources.Attendance.Category_Name.ToString() + " : " + */dtable.Rows[0]["Category_Name"].ToString();
        if (dtable.Rows[0]["Parent_Id"].ToString() != "")
        {
            DataTable dtable2 = ObjProductCateMaster.GetProductCategoryByCategoryId(StrCompId, dtable.Rows[0]["Parent_Id"].ToString());
            lblDelParentCategory.Text = /*Resources.Attendance.Parent_Category.ToString() + " : " + */dtable2.Rows[0]["Category_Name"].ToString();
            rowDelParentCategory.Visible = true;
        }
        else
        {
            rowDelParentCategory.Visible = false;
            lblDelParentCategory.Text = /*Resources.Attendance.Parent_Category.ToString() + " : ---";*/ "---";
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
        panelPopUpPanel.Focus();
    }
    protected void gvCategoryProductBin_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        gvProductCategoryBin.PageIndex = e.NewPageIndex;
        DataTable dt = (DataTable)Session["dtInactive"];
        gvProductCategoryBin.DataSource = dt;
        gvProductCategoryBin.DataBind();
        AllPageCode();
        string temp = string.Empty;

        for (int i = 0; i < gvProductCategoryBin.Rows.Count; i++)
        {
            Label lblconid = (Label)gvProductCategoryBin.Rows[i].FindControl("lblCategoryId");
            string[] split = lblSelectedRecord.Text.Split(',');

            for (int j = 0; j < lblSelectedRecord.Text.Split(',').Length; j++)
            {
                if (lblSelectedRecord.Text.Split(',')[j] != "")
                {
                    if (lblconid.Text.Trim().ToString() == lblSelectedRecord.Text.Split(',')[j].Trim().ToString())
                    {
                        ((CheckBox)gvProductCategoryBin.Rows[i].FindControl("chkSelect")).Checked = true;
                    }
                }
            }
        }
        gvProductCategoryBin.BottomPagerRow.Focus();
    }
    protected void gvProductCategoryBin_OnSorting(object sender, GridViewSortEventArgs e)
    {
        SortExpression = e.SortExpression;
        HDFSortbin.Value = HDFSortbin.Value == "ASC" ? "DESC" : "ASC";
        DataTable dt = new DataTable();
        dt = ObjProductCateMaster.GetProductCategoryFalseAllData(StrCompId);
        DataView dv = new DataView(dt);
        string Query = "" + SortExpression + " " + HDFSortbin.Value + "";
        dv.Sort = Query;
        dt = dv.ToTable();
        Session["dtInactive"] = dt;
        gvProductCategoryBin.DataSource = dt;
        gvProductCategoryBin.DataBind();
        lblSelectedRecord.Text = "";
        AllPageCode();
        gvProductCategoryBin.HeaderRow.Focus();
    }
    protected void gvProductCategory_OnSorting(object sender, GridViewSortEventArgs e)
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
        gvCategoryProduct.DataSource = dt;
        gvCategoryProduct.DataBind();
        AllPageCode();
        gvCategoryProduct.HeaderRow.Focus();

    }
    //Search panel on Bin Tab
    protected void btnRefreshBin_Click(object sender, EventArgs e)
    {
        BindTreeView(); //Update TreeView on Refresh
        //Update Bin Tab
        FillGrid();
        FillGridBin();
        txtValueBin.Text = "";
        ddlOptionBin.SelectedIndex = 2;
        ddlFieldNameBin.SelectedIndex = 1;
        lblSelectedRecord.Text = "";
        txtValueBin.Focus();
    }
    protected void btnbindBin_Click(object sender, EventArgs e)
    {
        string condition = string.Empty;
        if (ddlOptionBin.SelectedIndex != 0)
        {
            if (ddlFieldNameBin.SelectedValue == "ParentCategory" && txtValueBin.Text == "")
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
            gvProductCategoryBin.DataSource = view.ToTable();
            gvProductCategoryBin.DataBind();
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

        }
        btnbindBin.Focus();
    }
    protected void txtCategoryName_TextChanged(object sender, EventArgs e)
    {
        if (editid.Value == "")
        {
            DataTable dt = ObjProductCateMaster.GetProductCategoryByCategoryName(StrCompId, txtCategoryName.Text.Trim());
            if (dt.Rows.Count > 0)
            {
                txtCategoryName.Text = "";
                DisplayMessage("Category Name Already Exists");
                txtCategoryName.Focus();

            }
            else
            {
                DataTable dt1 = ObjProductCateMaster.GetProductCategoryFalseAllData(StrCompId);
                dt1 = new DataView(dt1, "Category_Name='" + txtCategoryName.Text + "'", "", DataViewRowState.CurrentRows).ToTable();
                if (dt1.Rows.Count > 0)
                {
                    txtCategoryName.Text = "";
                    DisplayMessage("Category Name Already Exists - Go to Bin Tab");
                    txtCategoryName.Focus();

                }
            }
        }
        else
        {
            DataTable dtTemp = ObjProductCateMaster.GetProductCategoryByCategoryId(StrCompId, editid.Value);
            if (dtTemp.Rows.Count > 0)
            {
                if (dtTemp.Rows[0]["Category_Name"].ToString() != txtCategoryName.Text.Trim())
                {
                    DataTable dt = ObjProductCateMaster.GetProductCategoryByCategoryName(StrCompId, txtCategoryName.Text.Trim());
                    if (dt.Rows.Count > 0)
                    {
                        txtCategoryName.Text = "";
                        DisplayMessage("Category Name Already Exists");
                        txtCategoryName.Focus();
                    }
                    else
                    {
                        DataTable dt1 = ObjProductCateMaster.GetProductCategoryFalseAllData(StrCompId);
                        dt1 = new DataView(dt1, "Category_Name='" + txtCategoryName.Text + "'", "", DataViewRowState.CurrentRows).ToTable();
                        if (dt1.Rows.Count > 0)
                        {
                            txtCategoryName.Text = "";
                            DisplayMessage("Category Name Already Exists - Go to Bin Tab");
                            txtCategoryName.Focus();

                        }
                    }
                }
            }
        }
        txtLCategoryName.Focus();
    }
    protected void btnRestoreSelected_Click(object sender, CommandEventArgs e)
    {
        int Msg = 0;
        DataTable dt = ObjProductCateMaster.GetProductCategoryFalseAllData(StrCompId);
        if (gvProductCategoryBin.Rows.Count != 0)
        {
            if (lblSelectedRecord.Text != "")
            {
                for (int j = 0; j < lblSelectedRecord.Text.Split(',').Length; j++)
                {
                    if (lblSelectedRecord.Text.Split(',')[j] != "")
                    {
                        Msg = ObjProductCateMaster.DeleteProductCategoryMaster(StrCompId, lblSelectedRecord.Text.Split(',')[j].Trim().ToString(), true.ToString(), Session["UserId"].ToString(), DateTime.Now.ToString());
                        if (Msg != 0)
                        {
                            ObjProductCateMaster.UpdateParentId(StrCompId, lblSelectedRecord.Text.Split(',')[j].Trim().ToString().ToString());
                        }
                    }
                }
            }

            if (Msg != 0)
            {
                lblSelectedRecord.Text = "";
                DisplayMessage("Record Activated");
                btnRefreshBin_Click(null, null);

            }
            else
            {
                int fleg = 0;
                foreach (GridViewRow Gvr in gvProductCategoryBin.Rows)
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
        txtValueBin.Focus();
    }
    protected void chkCurrent_CheckedChanged(object sender, EventArgs e)
    {
        CheckBox chkSelAll = ((CheckBox)gvProductCategoryBin.HeaderRow.FindControl("chkCurrent"));
        for (int i = 0; i < gvProductCategoryBin.Rows.Count; i++)
        {
            ((CheckBox)gvProductCategoryBin.Rows[i].FindControl("chkSelect")).Checked = chkSelAll.Checked;
            if (chkSelAll.Checked)
            {
                if (!lblSelectedRecord.Text.Split(',').Contains(((Label)(gvProductCategoryBin.Rows[i].FindControl("lblCategoryId"))).Text.Trim().ToString()))
                {
                    lblSelectedRecord.Text += ((Label)(gvProductCategoryBin.Rows[i].FindControl("lblCategoryId"))).Text.Trim().ToString() + ",";
                }
            }
            else
            {
                string temp = string.Empty;
                string[] split = lblSelectedRecord.Text.Split(',');
                foreach (string item in split)
                {
                    if (item != ((Label)(gvProductCategoryBin.Rows[i].FindControl("lblCategoryId"))).Text.Trim().ToString())
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
        Label lb = (Label)gvProductCategoryBin.Rows[index].FindControl("lblCategoryId");
        if (((CheckBox)gvProductCategoryBin.Rows[index].FindControl("chkSelect")).Checked)
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
        ((CheckBox)gvProductCategoryBin.Rows[index].FindControl("chkSelect")).Focus();
    }
    protected void btnClosePanel_Click(object sender, EventArgs e)
    {
        Reset(1);
        panelOverlay.Visible = false;
        panelPopUpPanel.Visible = false;
        txtValue.Focus();
    }
    #endregion

    #region Auto Complete Funcation
    [System.Web.Services.WebMethodAttribute(), System.Web.Script.Services.ScriptMethodAttribute()]
    public static string[] GetCompletionList(string prefixText, int count, string contextKey)
    {
        Inv_ProductCategoryMaster ObjProductCateMaster = new Inv_ProductCategoryMaster();
        DataTable dt = ObjProductCateMaster.GetDistinctCategoryName(HttpContext.Current.Session["CompId"].ToString(), prefixText);
        string[] txt = new string[dt.Rows.Count];

        for (int i = 0; i < dt.Rows.Count; i++)
        {
            txt[i] = dt.Rows[i]["Category_Name"].ToString() + "";
        }
        return txt;
    }
    #endregion

    #region User defind Funcation
    public void FillGrid()
    {
        DataTable dt = ObjProductCateMaster.GetProductCategoryTrueAllData(StrCompId);
        if (dt.Rows.Count > 0)
        {
            gvCategoryProduct.DataSource = dt;
            gvCategoryProduct.DataBind();
        }
        else
        {
            gvCategoryProduct.DataSource = null;
            gvCategoryProduct.DataBind();
        }

        Session["Category"] = dt;
        Session["dtFilter"] = dt;
        AllPageCode();
        lblTotalRecords.Text = Resources.Attendance.Total_Records + " : " + dt.Rows.Count.ToString() + "";
    }
    public void FillPerentDDl()
    {
        try
        {
            DataTable dt = ObjProductCateMaster.GetProductCategoryTrueAllData(StrCompId);
            ddlPerentCategory.DataSource = dt;
            ddlPerentCategory.DataTextField = "Category_Name";
            ddlPerentCategory.DataValueField = "Category_Id";
            ddlPerentCategory.DataBind();
            ddlPerentCategory.Items.Insert(0, "--Select--");
            ddlPerentCategory.SelectedIndex = 0;
        }
        catch
        {
            ddlPerentCategory.Items.Insert(0, "--Select--");
            ddlPerentCategory.SelectedIndex = 0;
        }
    }
    public void Reset(int RC)
    {
        txtCategoryName.Text = "";
        txtLCategoryName.Text = "";
        txtDescription.Text = "";
        ddlPerentCategory.SelectedIndex = 0;
        btnNew.Text = Resources.Attendance.New;
        editid.Value = "";
        ddlFieldName.SelectedIndex = 1;
        ddlOption.SelectedIndex = 2;
        txtValue.Text = "";
        ddlFieldNameBin.SelectedIndex = 1;
        ddlOptionBin.SelectedIndex = 2;
        txtValueBin.Text = "";
        btnNew.Text = Resources.Attendance.New;
        if (RC == 1)
        {

           txtValue.Focus();
        }

        else
        {
           txtCategoryName.Focus();
        }
        //delete functionality and BIN tab 
        btnDeleteNode.Text = Resources.Attendance.Delete.ToString();
        Session["DeleteNodeValue"] = "";
        arr.Clear();
        TreeViewCategory.Visible = false;
        gvCategoryProduct.Visible = true;
        FillGrid();
        FillPerentDDl();
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
            DataTable dt = ObjProductCateMaster.GetProductCategoryByCategoryId(StrCompId, Pid.ToString());
            if (dt.Rows.Count != 0)
            {
                retval = dt.Rows[0]["Category_Name"].ToString();
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
        string x = "Parent_Id=" + "' '" + "";
        dt = ObjProductCateMaster.GetProductCategoryTrueAllData(StrCompId);
        dt = new DataView(dt, x, "", DataViewRowState.OriginalRows).ToTable();
        int i = 0;
        while (i < dt.Rows.Count)
        {
            TreeNode tn = new TreeNode();
            tn.Text = dt.Rows[i]["Category_Name"].ToString();
            tn.Value = dt.Rows[i]["Category_Id"].ToString();
            TreeViewCategory.Nodes.Add(tn);
            FillChild((dt.Rows[i]["Category_Id"].ToString()), tn);
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
            tn1.Text = dt.Rows[i]["Category_Name"].ToString();
            tn1.Value = dt.Rows[i]["Category_Id"].ToString();
            tn.ChildNodes.Add(tn1);
            FillChild((dt.Rows[i]["Category_Id"].ToString()), tn1);
            i++;
        }
        TreeViewCategory.DataBind();
    }
    public DataTable GetProductCategoryByParentId(string ParentId) //Function to get entries of same ProductId
    {
        DataTable dt = ObjProductCateMaster.GetProductCategoryByCategoryId(StrCompId, "");
        string query = "Parent_Id='" + ParentId + "'";
        dt = ObjProductCateMaster.GetProductCategoryTrueAllData(StrCompId);
        dt = new DataView(dt, query, "", DataViewRowState.OriginalRows).ToTable();
        return dt;
    }
    public void FillMoveChildDropDownList(string strExceptId) //Function to fill up items in drop down list of New Parent after delete
    {
        DataTable dt = ObjProductCateMaster.GetProductCategoryTrueAllData(StrCompId);
        string query = "Category_Id not in(";

        FindChildNode(strExceptId);

        for (int i = 0; i < arr.Count; i++)
        {
            query += "'" + arr[i].ToString() + "',";
        }
        query = query.Substring(0, query.Length - 1).ToString() + ")";
        dt = new DataView(dt, query, "", DataViewRowState.OriginalRows).ToTable();

        ddlgroup0.DataSource = dt;
        ddlgroup0.DataTextField = "Category_Name";
        ddlgroup0.DataValueField = "Category_Id";
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
                FindChildNode(dt.Rows[i]["Category_Id"].ToString());
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
        dt = ObjProductCateMaster.GetProductCategoryFalseAllData(StrCompId);
        gvProductCategoryBin.DataSource = dt;
        gvProductCategoryBin.DataBind();
        Session["CategoryBin"] = dt;
        Session["dtInactive"] = dt;
        if (dt != null)
        {
            lblTotalRecordsBin.Text = Resources.Attendance.Total_Records + " : " + dt.Rows.Count.ToString() + "";
        }
        else
        {
            lblTotalRecordsBin.Text = Resources.Attendance.Total_Records + " : " + "0";
        }
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
        DataTable dt = ObjProductCateMaster.GetProductCategoryTrueAllData(StrCompId);
        string query = "Category_Id not in(";

        FindChildNode(strExceptId);

        for (int i = 0; i < arr.Count; i++)
        {
            if (arr[i].ToString() == "")
            {
                arr[i] = "0";
            }

            query += "'" + arr[i].ToString() + "',";
        }
        query = query.Substring(0, query.Length - 1).ToString() + ")";
        dt = new DataView(dt, query, "", DataViewRowState.OriginalRows).ToTable();

        ddlPerentCategory.DataSource = dt;
        ddlPerentCategory.DataTextField = "Category_Name";
        ddlPerentCategory.DataValueField = "Category_Id";
        ddlPerentCategory.DataBind();
        ddlPerentCategory.Items.Insert(0, "--Select--");
        ddlPerentCategory.SelectedIndex = 0;

        arr.Clear();
    }
    #endregion
}
