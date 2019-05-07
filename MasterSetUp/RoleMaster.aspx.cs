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

public partial class MasterSetUp_RoleMaster : BasePage
{
   
    RoleMaster objRole = new RoleMaster();

    RolePermission objRolePermission = new RolePermission();

    RoleDataPermission objRoleDataPerm = new RoleDataPermission();
    SystemParameter objSysParam = new SystemParameter();

    ModuleMaster objModule = new ModuleMaster();
    ObjectMaster ObjItObject = new ObjectMaster();
    Common cmn = new Common();
    SystemParameter objSys = new SystemParameter();
    BrandMaster objBrand = new BrandMaster();
    LocationMaster objLocation = new LocationMaster();
    DepartmentMaster objDepartment = new DepartmentMaster();

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
            PnlRolePerm.Visible = false;
            FillGridBin();
            FillGrid();
            FillLocation();
            FillDepartment();
            btnList_Click(null, null);
            FillBrand();
           
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
    }


    public void FillBrand()
    {
        DataTable dtBrand = objBrand.GetBrandMaster(Session["CompId"].ToString());
        if (dtBrand.Rows.Count > 0)
        {
            chkBrand.DataSource = dtBrand;
            chkBrand.DataTextField = "Brand_Name";
            chkBrand.DataValueField = "Brand_Id";
            chkBrand.DataBind();
            foreach (ListItem lst1 in chkBrand.Items)
            {

                chkBrand.Items.FindByValue(lst1.Value).Selected = true;

            }
        }
        


    }

    protected void txtRoleName_OnTextChanged(object sender, EventArgs e)
    {

        if (editid.Value == "")
        {
            DataTable dt = objRole.GetRoleMasterByRoleName(Session["CompId"].ToString().ToString(), txtRoleName.Text.Trim());
            if (dt.Rows.Count > 0)
            {
                txtRoleName.Text = "";
                DisplayMessage("Role Name Already Exists");
                System.Web.UI.ScriptManager.GetCurrent(this).SetFocus(txtRoleName);
                return;
            }
            DataTable dt1 = objRole.GetRoleMasterInactive(Session["CompId"].ToString().ToString());
            dt1 = new DataView(dt1, "Role_Name='" + txtRoleName.Text + "'", "", DataViewRowState.CurrentRows).ToTable();
            if (dt1.Rows.Count > 0)
            {
                txtRoleName.Text = "";
                DisplayMessage("Role Name Already Exists - Go to Bin Tab");
                System.Web.UI.ScriptManager.GetCurrent(this).SetFocus(txtRoleName);
                return;
            }
            txtRoleNameL.Focus();
        }
        else
        {
            DataTable dtTemp = objRole.GetRoleMasterById(Session["CompId"].ToString().ToString(), editid.Value);
            if (dtTemp.Rows.Count > 0)
            {
                if (dtTemp.Rows[0]["Role_Name"].ToString() != txtRoleName.Text)
                {
                    DataTable dt = objRole.GetRoleMaster(Session["CompId"].ToString().ToString());
                    dt = new DataView(dt, "Role_Name='" + txtRoleName.Text + "'", "", DataViewRowState.CurrentRows).ToTable();
                    if (dt.Rows.Count > 0)
                    {
                        txtRoleName.Text = "";
                        DisplayMessage("Role Name Already Exists");
                        System.Web.UI.ScriptManager.GetCurrent(this).SetFocus(txtRoleName);
                        return;
                    }
                    DataTable dt1 = objRole.GetRoleMaster(Session["CompId"].ToString().ToString());
                    dt1 = new DataView(dt1, "Role_Name='" + txtRoleName.Text + "'", "", DataViewRowState.CurrentRows).ToTable();
                    if (dt1.Rows.Count > 0)
                    {
                        txtRoleName.Text = "";
                        DisplayMessage("Role Name Already Exists - Go to Bin Tab");
                        System.Web.UI.ScriptManager.GetCurrent(this).SetFocus(txtRoleName);
                        return;
                    }
                }
            }
            txtRoleNameL.Focus();
        }
    }
  
    public void FillLocation()
    {
        DataTable dtLocation = objLocation.GetLocationMaster(Session["CompId"].ToString());
        if (dtLocation.Rows.Count > 0)
        {
            chkLocation.DataSource = dtLocation;
            chkLocation.DataTextField = "Location_Name";
            chkLocation.DataValueField = "Location_Id";
            chkLocation.DataBind();
            foreach (ListItem lst1 in chkLocation.Items)
            {

                chkLocation.Items.FindByValue(lst1.Value).Selected = true;

            }
        }



    }

    protected void chkBrand_SelectedIndexChanged(object sender, EventArgs e)
    {

        DataTable dtLocation = objLocation.GetLocationMaster(Session["CompId"].ToString());
        DataTable dtFinal = dtLocation.Clone();
        string selectedLocation = string.Empty;
        for (int i = 0; i < chkBrand.Items.Count; i++)
        {
            if (chkBrand.Items[i].Selected)
            {
                selectedLocation += chkBrand.Items[i].Value + ",";
            }
        }
        foreach (ListItem lst in chkBrand.Items)
        {
            if (lst.Selected)
            {
                dtFinal.Merge(new DataView(dtLocation, "Brand_Id='" + lst.Value + "'", "", DataViewRowState.CurrentRows).ToTable());
            }
        }



        chkLocation.DataSource = dtFinal;
        chkLocation.DataTextField = "Location_Name";
        chkLocation.DataValueField = "Location_Id";
        chkLocation.DataBind();


        foreach (ListItem lst in chkLocation.Items)
        {

            chkLocation.Items.FindByValue(lst.Value).Selected = true;

        }

        chkLocation_SelectedIndexChanged(null, null);
       
    }
    protected void chkLocation_SelectedIndexChanged(object sender, EventArgs e)
    {
        DataTable dtLDempart =objDepartment.GetDepartmentMaster(Session["CompId"].ToString());
        DataTable dtFinal = dtLDempart.Clone();
        string selectedLocation = string.Empty;
        for (int i = 0; i < chkDepartment.Items.Count; i++)
        {
            if (chkDepartment.Items[i].Selected)
            {
                selectedLocation += chkDepartment.Items[i].Value + ",";
            }
        }
        foreach (ListItem lst in chkLocation.Items)
        {
            if (lst.Selected)
            {
                dtFinal.Merge(new DataView(dtLDempart, "Location_Id='" + lst.Value + "'", "", DataViewRowState.CurrentRows).ToTable());
            }
        }
        DataTable dtFin = new DataTable();

        if (dtFinal.Rows.Count > 0)
        {

            dtFin = dtFinal.Clone();
            dtFin.Columns.Add("Dep_Name1");

            for (int i = 0; i < dtFinal.Rows.Count; i++)
            {
                dtFin.ImportRow(dtFinal.Rows[i]);
                dtFin.Rows[i]["Dep_Name1"] = GetDept(dtFin.Rows[i]["Dep_Id"]);

            }

        }


        chkDepartment.DataSource = dtFin;
        chkDepartment.DataTextField = "Dep_Name1";
        chkDepartment.DataValueField = "Dep_Id";
        chkDepartment.DataBind();

        foreach (ListItem lst1 in chkDepartment.Items)
        {

            chkDepartment.Items.FindByValue(lst1.Value).Selected = true;

        }
    }
    public void SaveBrandLocDept(string RoleId)
    {
       

        foreach (ListItem item in chkBrand.Items)
        {
            if (item.Selected)
            {
                objRoleDataPerm.InsertRoleDataPermission(RoleId,Session["CompId"].ToString(),"B",item.Value,true.ToString(),Session["UserId"].ToString(),DateTime.Now.ToString(),Session["UserId"].ToString(),DateTime.Now.ToString());
            }
        }
        foreach (ListItem item in chkLocation.Items)
        {
            if (item.Selected)
            {

               


                objRoleDataPerm.InsertRoleDataPermission(RoleId, Session["CompId"].ToString(), "L", item.Value, true.ToString(), Session["UserId"].ToString(), DateTime.Now.ToString(), Session["UserId"].ToString(), DateTime.Now.ToString());
   
            }
        }
        foreach (ListItem item in chkDepartment.Items)
        {
            if (item.Selected)
            {


            
                objRoleDataPerm.InsertRoleDataPermission(RoleId, Session["CompId"].ToString(), "D", item.Value, true.ToString(), Session["UserId"].ToString(), DateTime.Now.ToString(), Session["UserId"].ToString(), DateTime.Now.ToString());
   


            }
        }

    }


    public string GetBrandIdByLocationId(string LocationId)
    {
        string BrandId = string.Empty;

          DataTable dtLocation = objLocation.GetLocationMasterById(Session["CompId"].ToString(),LocationId);
          if (dtLocation.Rows.Count > 0)
          {
              BrandId=dtLocation.Rows[0]["BrandId"].ToString().Trim();

          }

        return BrandId;
    }

    public string GetLocationIdByDepartmentId(string DeptId)
    {

        string LocationId = string.Empty;

        DataTable dtDept = objDepartment.GetDepartmentMasterById(Session["CompId"].ToString(),DeptId);
        if (dtDept.Rows.Count > 0)
        {
            LocationId = dtDept.Rows[0]["Location_Id"].ToString().Trim();

        }

        return LocationId;
    }
    public string GetDept(object obj)
    {
        string retval = "";
        string LocationName = "";
        DepartmentMaster objDept = new DepartmentMaster();
        DataTable dtDept = objDept.GetDepartmentMasterById(Session["CompId"].ToString(),obj.ToString());
        if (dtDept.Rows.Count > 0)
        {

            LocationName = GetLocation(dtDept.Rows[0]["Location_Id"]);
            retval = dtDept.Rows[0]["Dep_Name"].ToString() + "," + LocationName;
        }
        return retval;

    }
    protected string GetLocation(object obj)
    {


        string retval = string.Empty;
        try
        {
           
            retval = (objLocation.GetLocationMasterById(Session["CompId"].ToString(),obj.ToString())).Rows[0]["Location_Name"].ToString();
        }
        catch (Exception)
        {

            return "";
        }
        return retval;

    }
    public void FillDepartment()
    {
        DataTable dtDepartment = objDepartment.GetDepartmentMaster(Session["CompId"].ToString());
        if (dtDepartment.Rows.Count > 0)
        {
            chkDepartment.DataSource = dtDepartment;
            chkDepartment.DataTextField = "Dep_Name";
            chkDepartment.DataValueField = "Dep_Id";
            chkDepartment.DataBind();
            foreach (ListItem lst1 in chkDepartment.Items)
            {

                chkDepartment.Items.FindByValue(lst1.Value).Selected = true;

            }
        }



    }
    public void AllPageCode()
    {

        Page.Title = objSys.GetSysTitle();
        Session["AccordianId"] = "8";
        Session["HeaderText"] = "Master Setup";
        DataTable dtAllPageCode = cmn.GetAllPagePermission(Session["UserId"].ToString(), "8", "25");
        if (dtAllPageCode.Rows.Count != 0)
        {
            if (dtAllPageCode.Rows[0][0].ToString() == "SuperAdmin")
            {
                btnSave.Visible = true;
                foreach (GridViewRow Row in gvRoleMaster.Rows)
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
                    foreach (GridViewRow Row in gvRoleMaster.Rows)
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
        catch { }

        navTree.DataBind();

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
         txtRoleName.Focus();
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
            DataTable dtCust = (DataTable)Session["Role"];


            DataView view = new DataView(dtCust, condition, "", DataViewRowState.CurrentRows);
            Session["dtFilter"] = view.ToTable();
            lblTotalRecords.Text = Resources.Attendance.Total_Records + ": " + view.ToTable().Rows.Count.ToString() + "";

            gvRoleMaster.DataSource = view.ToTable();
            gvRoleMaster.DataBind();
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
            DataTable dtCust = (DataTable)Session["dtbinRole"];


            DataView view = new DataView(dtCust, condition, "", DataViewRowState.CurrentRows);
            Session["dtbinFilter"] = view.ToTable();
            lblbinTotalRecords.Text = Resources.Attendance.Total_Records + ": " + view.ToTable().Rows.Count.ToString() + "";

            gvRoleMasterBin.DataSource = view.ToTable();
            gvRoleMasterBin.DataBind();


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

    protected void gvRoleMasterBin_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {

        gvRoleMasterBin.PageIndex = e.NewPageIndex;
        if (HDFSortbin.Value == "")
            FillGridBin();
        else
        {
            DataTable dt = (DataTable)Session["dtbinFilter"];
            gvRoleMasterBin.DataSource = dt;
            gvRoleMasterBin.DataBind();
            AllPageCode();
        }
        string temp = string.Empty;
        bool isselcted;

        for (int i = 0; i < gvRoleMasterBin.Rows.Count; i++)
        {
            Label lblconid = (Label)gvRoleMasterBin.Rows[i].FindControl("lblRoleId");
            string[] split = lblSelectedRecord.Text.Split(',');

            for (int j = 0; j < lblSelectedRecord.Text.Split(',').Length; j++)
            {
                if (lblSelectedRecord.Text.Split(',')[j] != "")
                {
                    if (lblconid.Text.Trim().ToString() == lblSelectedRecord.Text.Split(',')[j].Trim().ToString())
                    {
                        ((CheckBox)gvRoleMasterBin.Rows[i].FindControl("chkgvSelect")).Checked = true;
                    }
                }
            }
        }

    }
    protected void gvRoleMasterBin_OnSorting(object sender, GridViewSortEventArgs e)
    {
        lblSelectedRecord.Text = "";
        HDFSortbin.Value = HDFSortbin.Value == "ASC" ? "DESC" : "ASC";
        DataTable dt = new DataTable();
        dt = objRole.GetRoleMasterInactive(Session["CompId"].ToString());
        DataView dv = new DataView(dt);
        string Query = "" + e.SortExpression + " " + HDFSortbin.Value + "";
        dv.Sort = Query;
        dt = dv.ToTable();
        Session["dtbinFilter"] = dt;
        gvRoleMasterBin.DataSource = dt;
        gvRoleMasterBin.DataBind();
        AllPageCode();

    }
    

   
    protected void btnSave_Click(object sender, EventArgs e)
    {
        int b = 0;

      

        if (txtRoleName.Text == "")
        {
            DisplayMessage("Enter Role Name");
            txtRoleName.Focus();
            return;
        }



        if (editid.Value == "")
        {

            DataTable dt1 = objRole.GetRoleMaster(Session["CompId"].ToString());

            dt1 = new DataView(dt1, "Role_Name='" + txtRoleName.Text + "'", "", DataViewRowState.CurrentRows).ToTable();
            if (dt1.Rows.Count > 0)
            {
                DisplayMessage("Role Name Already Exists");
                txtRoleName.Focus();
                return;

            }




            b = objRole.InsertRoleMaster(Session["CompId"].ToString(), txtRoleName.Text, txtRoleNameL.Text, "", "", "", "", "", true.ToString(), DateTime.Now.ToString(), true.ToString(), Session["UserId"].ToString(), DateTime.Now.ToString(), Session["UserId"].ToString(), DateTime.Now.ToString());
            if (b != 0)
            {
                DisplayMessage("Record Saved");
                FillGrid();

                editid.Value = b.ToString();

              


                SaveBrandLocDept(editid.Value);
                PnlRolePerm.Visible = true;
                BindTree();
                
            }
            else
            {
                DisplayMessage("Record Not Saved");
            }
        }
        else
        {
            DataTable dt1 = objRole.GetRoleMaster(Session["CompId"].ToString());
            string RoleName = string.Empty;

            try
            {
                RoleName = (new DataView(dt1, "Role_Id='" + editid.Value + "'", "", DataViewRowState.CurrentRows).ToTable()).Rows[0]["Role_Name"].ToString();
            }
            catch
            {
                RoleName = "";
            }
            


            dt1 = new DataView(dt1, "Role_Name='" + txtRoleName.Text + "' and Role_Name<>'"+RoleName+"'", "", DataViewRowState.CurrentRows).ToTable();
            if (dt1.Rows.Count > 0)
            {
                DisplayMessage("Role Name Already Exists");
                txtRoleName.Focus();
                return;

            }




            b = objRole.UpdateRoleMaster(Session["CompId"].ToString(), editid.Value, txtRoleName.Text, txtRoleNameL.Text, "", "", "", "", "", true.ToString(), DateTime.Now.ToString(), true.ToString(), Session["UserId"].ToString(), DateTime.Now.ToString());

            if (b != 0)
            {
                
                DisplayMessage("Record Updated");
               
                FillGrid();

                FillGridBin();
                PnlRolePerm.Visible = true;
               
               
              
                btnSavePerm_Click(null, null);

                objRoleDataPerm.DeleteRoleDataPermission(editid.Value);
                SaveBrandLocDept(editid.Value);

                Reset();

                btnList_Click(null,null);

            }
            else
            {
                DisplayMessage("Record Not Updated");
            }

        }
    }



    

    public void BindTree()
    {
        string AppId = string.Empty;
        DataTable dtApp = objSysParam.GetSysParameterByParamName("Application_Id");
        if (dtApp.Rows.Count > 0)
        {
            AppId=dtApp.Rows[0]["Param_Value"].ToString().Trim();

        }
        else
        {
            return;
        }

      

        string RoleId = string.Empty;
        string moduleids=string.Empty;
      

        navTree.DataSource = null;
        navTree.DataBind();
        navTree.Nodes.Clear();
       
            DataTable dtRoleP = objRolePermission.GetRolePermissionById(editid.Value);

            if(dtRoleP.Rows.Count > 0)
            {


            }


            DataTable dtModule = objModule.GetModuleMaster();

            DataTable DtModuleApp = new DataTable();

            DtModuleApp = objModule.GetModuleObjectByApplicatonId(AppId);


            for (int i = 0; i < DtModuleApp.Rows.Count; i++)
            {
                moduleids += "'" + DtModuleApp.Rows[i]["Module_Id"].ToString() + "'" + ",";
            }

            dtModule = new DataView(dtModule,"Module_Id in ("+ moduleids.Substring(0, moduleids.Length - 1)+")","",DataViewRowState.CurrentRows).ToTable();

            DataTable DtOpType = ObjItObject.GetOpType();

            foreach (DataRow datarow in dtModule.Rows)
            {

                TreeNode tn = new TreeNode();


                tn = new TreeNode(datarow["Module_Name"].ToString(), datarow["Module_Id"].ToString());

                DataTable dtModuleSaved = new DataView(dtRoleP, "Module_Id='" + datarow["Module_Id"].ToString() + "'", "", DataViewRowState.CurrentRows).ToTable();

               if (dtModuleSaved.Rows.Count > 0)
                {
                    tn.Checked = true;

               }

               
               
                DataTable dtAllChild = ObjItObject.GetObjectMasterByModuleId_ApplicationId(datarow["Module_Id"].ToString(),AppId);




                dtAllChild = new DataView(dtAllChild, "", "Order_Appear", DataViewRowState.CurrentRows).ToTable();
                foreach (DataRow childrow in dtAllChild.Rows)
                {
                    string GetUrl = string.Empty;
                    GetUrl = childrow[0].ToString();



                    TreeNode tnChild = new TreeNode(childrow[1].ToString(), GetUrl);
                    DataTable dtObj = new DataView(dtRoleP, "Object_Id='" + GetUrl + "'", "", DataViewRowState.CurrentRows).ToTable();

                if (dtObj.Rows.Count > 0)
                  {
                        tnChild.Checked = true;
                  }
                    tn.ChildNodes.Add(tnChild);
                   foreach (DataRow drOpType in DtOpType.Rows)
                   {
                       TreeNode tnOpType = new TreeNode(drOpType[1].ToString(), drOpType[0].ToString());

                       if (drOpType["Op_Id"].ToString() == "1")
                       {
                           DataTable dtOp = new DataView(dtRoleP, "Op_Add='True' and Object_Id='" + GetUrl + "'", "", DataViewRowState.CurrentRows).ToTable();

                           if(dtOp.Rows.Count > 0)
                           {
                               tnOpType.Checked = true;
                           }
                       }
                       else if (drOpType["Op_Id"].ToString() == "2")
                       {
                           DataTable dtOp = new DataView(dtRoleP, "Op_Edit='True' and Object_Id='" + GetUrl + "'", "", DataViewRowState.CurrentRows).ToTable();

                           if (dtOp.Rows.Count > 0)
                           {
                               tnOpType.Checked = true;
                           }

                       }
                       else if (drOpType["Op_Id"].ToString() == "3")
                       {
                           DataTable dtOp = new DataView(dtRoleP, "Op_Delete='True' and Object_Id='" + GetUrl + "'", "", DataViewRowState.CurrentRows).ToTable();

                           if (dtOp.Rows.Count > 0)
                           {
                               tnOpType.Checked = true;
                           }

                       }
                       else if (drOpType["Op_Id"].ToString() == "4")
                       {
                           DataTable dtOp = new DataView(dtRoleP, "Op_Restore='True' and Object_Id='" + GetUrl + "'", "", DataViewRowState.CurrentRows).ToTable();

                           if (dtOp.Rows.Count > 0)
                           {
                               tnOpType.Checked = true;
                           }

                       }
                       else if (drOpType["Op_Id"].ToString() == "5")
                       {
                           DataTable dtOp = new DataView(dtRoleP, "Op_View='True' and Object_Id='" + GetUrl + "'", "", DataViewRowState.CurrentRows).ToTable();

                           if (dtOp.Rows.Count > 0)
                           {
                               tnOpType.Checked = true;
                           }

                       }
                       else if (drOpType["Op_Id"].ToString() == "6")
                       {
                           DataTable dtOp = new DataView(dtRoleP, "Op_Print='True' and Object_Id='" + GetUrl + "'", "", DataViewRowState.CurrentRows).ToTable();

                           if (dtOp.Rows.Count > 0)
                           {
                               tnOpType.Checked = true;
                           }

                       }
                       else if (drOpType["Op_Id"].ToString() == "7")
                       {
                           DataTable dtOp = new DataView(dtRoleP, "Op_Download='True' and Object_Id='" + GetUrl + "'", "", DataViewRowState.CurrentRows).ToTable();

                           if (dtOp.Rows.Count > 0)
                           {
                               tnOpType.Checked = true;
                           }

                       }
                       else if (drOpType["Op_Id"].ToString() == "8")
                       {
                           DataTable dtOp = new DataView(dtRoleP, "Op_Upload='True' and Object_Id='" + GetUrl + "'", "", DataViewRowState.CurrentRows).ToTable();

                           if (dtOp.Rows.Count > 0)
                           {
                               tnOpType.Checked = true;
                           }

                       }
                       tnChild.ChildNodes.Add(tnOpType);
                   }

                }

                navTree.Nodes.Add(tn);
            }
      



        navTree.DataBind();
        navTree.CollapseAll();


        return;

       

       



    }
     protected void btnSavePerm_Click(object sender, EventArgs e)
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

          
           

            
            objRolePermission.DeleteRolePermission(editid.Value);


            foreach (TreeNode ModuleNode in navTree.Nodes)
            {
                //here save one row for module
                if (ModuleNode.Checked)
                {

                     
                    foreach (TreeNode ObjNode in ModuleNode.ChildNodes)
                    {
                        if (ObjNode.Checked)
                        {
                            int refid1 = 0;

                            refid1=objRolePermission.InsertRolePermission(editid.Value,ModuleNode.Value,ObjNode.Value,true.ToString(),Session["UserId"].ToString(),DateTime.Now.ToString(),Session["UserId"].ToString(),DateTime.Now.ToString());

                            string refid = refid1.ToString();
                            string OpIds = string.Empty;
                            foreach (TreeNode OpNode in ObjNode.ChildNodes)
                            {
                                if (OpNode.Checked)
                                {
                                    if (OpNode.Value == "1")
                                    {
                                        objRolePermission.InsertRoleOpPermission(refid, true.ToString(), false.ToString(), false.ToString(), false.ToString(), false.ToString(), false.ToString(), false.ToString(), false.ToString());
                                    }
                                    else if (OpNode.Value == "2")
                                    {
                                        objRolePermission.InsertRoleOpPermission(refid, false.ToString(), true.ToString(), false.ToString(), false.ToString(), false.ToString(), false.ToString(), false.ToString(), false.ToString());
                                    }
                                    else if (OpNode.Value == "3")
                                    {
                                        objRolePermission.InsertRoleOpPermission(refid, false.ToString(), false.ToString(), true.ToString(), false.ToString(), false.ToString(), false.ToString(), false.ToString(), false.ToString());
                                    }
                                    else if (OpNode.Value == "4")
                                    {
                                        objRolePermission.InsertRoleOpPermission(refid, false.ToString(), false.ToString(), false.ToString(), true.ToString(), false.ToString(), false.ToString(), false.ToString(), false.ToString());
                                    }
                                    else if (OpNode.Value == "5")
                                    {
                                        objRolePermission.InsertRoleOpPermission(refid, false.ToString(), false.ToString(), false.ToString(), false.ToString(), true.ToString(), false.ToString(), false.ToString(), false.ToString());
                                    }
                                    else if (OpNode.Value == "6")
                                    {
                                        objRolePermission.InsertRoleOpPermission(refid, false.ToString(), false.ToString(), false.ToString(), false.ToString(), false.ToString(), true.ToString(), false.ToString(), false.ToString());

                                    }
                                    else if (OpNode.Value == "7")
                                    {
                                        objRolePermission.InsertRoleOpPermission(refid, false.ToString(), false.ToString(), false.ToString(), false.ToString(), false.ToString(), false.ToString(), true.ToString(), false.ToString());
                                    }
                                    else if (OpNode.Value == "8")
                                    {
                                        objRolePermission.InsertRoleOpPermission(refid, false.ToString(), false.ToString(), false.ToString(), false.ToString(), false.ToString(), false.ToString(), false.ToString(), true.ToString());
                                    }

                                }
                                else
                                {
                                    if (OpNode.Value == "1")
                                    {
                                        objRolePermission.InsertRoleOpPermission(refid, false.ToString(), false.ToString(), false.ToString(), false.ToString(), false.ToString(), false.ToString(), false.ToString(), false.ToString());
                                    }
                                    else if (OpNode.Value == "2")
                                    {
                                        objRolePermission.InsertRoleOpPermission(refid, false.ToString(), false.ToString(), false.ToString(), false.ToString(), false.ToString(), false.ToString(), false.ToString(), false.ToString());
                                    }
                                    else if (OpNode.Value == "3")
                                    {
                                        objRolePermission.InsertRoleOpPermission(refid, false.ToString(), false.ToString(), false.ToString(), false.ToString(), false.ToString(), false.ToString(), false.ToString(), false.ToString());
                                    }
                                    else if (OpNode.Value == "4")
                                    {
                                        objRolePermission.InsertRoleOpPermission(refid, false.ToString(), false.ToString(), false.ToString(), false.ToString(), false.ToString(), false.ToString(), false.ToString(), false.ToString());
                                    }
                                    else if (OpNode.Value == "5")
                                    {
                                        objRolePermission.InsertRoleOpPermission(refid, false.ToString(), false.ToString(), false.ToString(), false.ToString(), false.ToString(), false.ToString(), false.ToString(), false.ToString());
                                    }
                                    else if (OpNode.Value == "6")
                                    {
                                        objRolePermission.InsertRoleOpPermission(refid, false.ToString(), false.ToString(), false.ToString(), false.ToString(), false.ToString(), false.ToString(), false.ToString(), false.ToString());

                                    }
                                    else if (OpNode.Value == "7")
                                    {
                                        objRolePermission.InsertRoleOpPermission(refid, false.ToString(), false.ToString(), false.ToString(), false.ToString(), false.ToString(), false.ToString(), false.ToString(), false.ToString());
                                    }
                                    else if (OpNode.Value == "8")
                                    {
                                        objRolePermission.InsertRoleOpPermission(refid, false.ToString(), false.ToString(), false.ToString(), false.ToString(), false.ToString(), false.ToString(), false.ToString(), false.ToString());
                                    }
                                }


                            }

                             
                             
                        }
                        
                    }


                }

            }


            DisplayMessage("Record Saved");

            
           
        
     }

     protected void btnCancelPerm_Click(object sender, EventArgs e)
    {
        btnList_Click(null,null);
        Reset();
        FillGrid();
        FillGridBin();
        
        PnlRolePerm.Visible = false;
        PnlRole.Visible = true;
     }

   

     protected void navTree_TreeNodeCheckChanged(object sender, TreeNodeEventArgs e)
     {


     }

     protected void navTree_SelectedNodeChanged1(object sender, EventArgs e)
     {

     }
        

    protected void btnEdit_Command(object sender, CommandEventArgs e)
    {
        editid.Value = e.CommandArgument.ToString();
        string SelectedBrand = string.Empty;
        string SelectedLocation = string.Empty;
        string SelectedDepartment = string.Empty;
  

        DataTable dt = objRole.GetRoleMasterById(Session["CompId"].ToString(),editid.Value);
        if (dt.Rows.Count > 0)
        {
            
            txtRoleName.Text = dt.Rows[0]["Role_Name"].ToString();
            txtRoleNameL.Text = dt.Rows[0]["Role_Name_L"].ToString();


            FillBrand();
            FillLocation();
            FillDepartment();

            foreach (ListItem lst1 in chkBrand.Items)
            {

                chkBrand.Items.FindByValue(lst1.Value).Selected = false;

            }
            foreach (ListItem lst1 in chkLocation.Items)
            {

                chkLocation.Items.FindByValue(lst1.Value).Selected = false;

            }

            foreach (ListItem lst1 in chkDepartment.Items)
            {

                chkDepartment.Items.FindByValue(lst1.Value).Selected = false;

            }

            DataTable dtRoleData = objRoleDataPerm.GetRoleDataPermissionById(editid.Value);

            if(dtRoleData.Rows.Count > 0)
            {

                DataTable dtBrand = new DataView(dtRoleData,"Record_Type='B'","",DataViewRowState.CurrentRows).ToTable();

                 if(dtBrand.Rows.Count > 0)
                 {


                     foreach (DataRow dr in dtBrand.Rows)
                     {
                         SelectedBrand+=dr["Record_Id"]+",";
                     }

                     for (int j = 0; j < SelectedBrand.Split(',').Length; j++)
                     {
                         if (SelectedBrand.Split(',')[j] != "")
                         {
                             try
                             {
                                 chkBrand.Items.FindByValue(SelectedBrand.Split(',')[j]).Selected = true;
                             }
                             catch (Exception ex)
                             {

                                 continue;
                             }
                         }
                     }
                 }
                 chkBrand_SelectedIndexChanged(null, null);

                 foreach (ListItem lst1 in chkLocation.Items)
                 {

                     chkLocation.Items.FindByValue(lst1.Value).Selected = false;

                 }

                DataTable dtLocation = new DataView(dtRoleData, "Record_Type='L'", "", DataViewRowState.CurrentRows).ToTable();

                if (dtLocation.Rows.Count > 0)
                {
                    foreach (DataRow dr in dtLocation.Rows)
                     {
                         SelectedLocation+=dr["Record_Id"]+",";
                     }

                    for (int j = 0; j < SelectedLocation.Split(',').Length; j++)
                     {
                         if (SelectedLocation.Split(',')[j] != "")
                         {
                             try
                             {
                                 chkLocation.Items.FindByValue(SelectedLocation.Split(',')[j]).Selected = true;
                             }
                             catch (Exception ex)
                             {

                                 continue;
                             }
                         }
                     }
                }
                chkLocation_SelectedIndexChanged(null, null);

                foreach (ListItem lst1 in chkDepartment.Items)
                {

                    chkDepartment.Items.FindByValue(lst1.Value).Selected = false;

                }
                DataTable dtDepartment = new DataView(dtRoleData, "Record_Type='D'", "", DataViewRowState.CurrentRows).ToTable();
                if (dtDepartment.Rows.Count > 0)
                {
                    foreach (DataRow dr in dtDepartment.Rows)
                    {
                        SelectedDepartment  += dr["Record_Id"] + ",";
                    }

                    for (int j = 0; j < SelectedDepartment.Split(',').Length; j++)
                    {
                        if (SelectedDepartment.Split(',')[j] != "")
                        {
                            try
                            {
                                chkDepartment.Items.FindByValue(SelectedDepartment.Split(',')[j]).Selected = true;
                            }
                            catch (Exception ex)
                            {

                                continue;
                            }
                        }
                    }
                }

            }







            btnNew_Click(null, null);
            btnNew.Text = Resources.Attendance.Edit;

        }
        PnlRolePerm.Visible = true;
        BindTree();

    }

    protected void IbtnDelete_Command(object sender, CommandEventArgs e)
    {
        int b = 0;
        b = objRole.DeleteRoleMaster(Session["CompId"].ToString(),e.CommandArgument.ToString(), false.ToString(), Session["UserId"].ToString(), DateTime.Now.ToString());
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
    protected void gvRoleMaster_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        gvRoleMaster.PageIndex = e.NewPageIndex;

        DataTable dt = (DataTable)Session["dtFilter"];
        gvRoleMaster.DataSource = dt;
        gvRoleMaster.DataBind();
        AllPageCode();

    }
    protected void gvRoleMaster_OnSorting(object sender, GridViewSortEventArgs e)
    {
        HDFSort.Value = HDFSort.Value == "ASC" ? "DESC" : "ASC";
        DataTable dt = new DataTable();
        dt = (DataTable)Session["dtFilter"];
        DataView dv = new DataView(dt);
        string Query = "" + e.SortExpression + " " + HDFSort.Value + "";
        dv.Sort = Query;
        dt = dv.ToTable();
        Session["dtFilter"] = dt;
        gvRoleMaster.DataSource = dt;
        gvRoleMaster.DataBind();
        AllPageCode();
    }

    [System.Web.Services.WebMethodAttribute(), System.Web.Script.Services.ScriptMethodAttribute()]
    public static string[] GetCompletionListRoleCode(string prefixText, int count, string contextKey)
    {
        RoleMaster objRoleMaster = new RoleMaster();
        DataTable dt = new DataView(objRoleMaster.GetRoleMaster(HttpContext.Current.Session["CompId"].ToString()), "Role_Code like '" + prefixText.ToString() + "%'", "", DataViewRowState.CurrentRows).ToTable();
        string[] txt = new string[dt.Rows.Count];

        for (int i = 0; i < dt.Rows.Count; i++)
        {
            txt[i] = dt.Rows[i]["Role_Code"].ToString();
        }
        return txt;
    }
    [System.Web.Services.WebMethodAttribute(), System.Web.Script.Services.ScriptMethodAttribute()]
    public static string[] GetCompletionListRoleName(string prefixText, int count, string contextKey)
    {
        RoleMaster objRoleMaster = new RoleMaster();
        DataTable dt = new DataView(objRoleMaster.GetRoleMaster(HttpContext.Current.Session["CompId"].ToString()), "Role_Name like '" + prefixText.ToString() + "%'", "", DataViewRowState.CurrentRows).ToTable();
        string[] txt = new string[dt.Rows.Count];

        for (int i = 0; i < dt.Rows.Count; i++)
        {
            txt[i] = dt.Rows[i]["Role_Name"].ToString();
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
        DataTable dt = objRole.GetRoleMaster(Session["CompId"].ToString());
        gvRoleMaster.DataSource = dt;
        gvRoleMaster.DataBind();
        AllPageCode();
        Session["dtFilter"] = dt;
        Session["Role"] = dt;

        lblTotalRecords.Text = Resources.Attendance.Total_Records + ": " + dt.Rows.Count.ToString() + "";

    }
    public void FillGridBin()
    {
        DataTable dt = new DataTable();
        dt = objRole.GetRoleMasterInactive(Session["CompId"].ToString());

        gvRoleMasterBin.DataSource = dt;
        gvRoleMasterBin.DataBind();


        Session["dtbinFilter"] = dt;
        Session["dtbinRole"] = dt;
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
        CheckBox chkSelAll = ((CheckBox)gvRoleMasterBin.HeaderRow.FindControl("chkgvSelectAll"));
        for (int i = 0; i < gvRoleMasterBin.Rows.Count; i++)
        {
            ((CheckBox)gvRoleMasterBin.Rows[i].FindControl("chkgvSelect")).Checked = chkSelAll.Checked;
            if (chkSelAll.Checked)
            {
                if (!lblSelectedRecord.Text.Split(',').Contains(((Label)(gvRoleMasterBin.Rows[i].FindControl("lblRoleId"))).Text.Trim().ToString()))
                {
                    lblSelectedRecord.Text += ((Label)(gvRoleMasterBin.Rows[i].FindControl("lblRoleId"))).Text.Trim().ToString() + ",";
                }
            }
            else
            {
                string temp = string.Empty;
                string[] split = lblSelectedRecord.Text.Split(',');
                foreach (string item in split)
                {
                    if (item != ((Label)(gvRoleMasterBin.Rows[i].FindControl("lblRoleId"))).Text.Trim().ToString())
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
        Label lb = (Label)gvRoleMasterBin.Rows[index].FindControl("lblRoleId");
        if (((CheckBox)gvRoleMasterBin.Rows[index].FindControl("chkgvSelect")).Checked)
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
                if (!lblSelectedRecord.Text.Split(',').Contains(dr["Role_Id"]))
                {
                    lblSelectedRecord.Text += dr["Role_Id"] + ",";
                }
            }
            for (int i = 0; i < gvRoleMasterBin.Rows.Count; i++)
            {
                string[] split = lblSelectedRecord.Text.Split(',');
                Label lblconid = (Label)gvRoleMasterBin.Rows[i].FindControl("lblRoleId");
                for (int j = 0; j < lblSelectedRecord.Text.Split(',').Length; j++)
                {
                    if (lblSelectedRecord.Text.Split(',')[j] != "")
                    {
                        if (lblconid.Text.Trim().ToString() == lblSelectedRecord.Text.Split(',')[j].Trim().ToString())
                        {
                            ((CheckBox)gvRoleMasterBin.Rows[i].FindControl("chkgvSelect")).Checked = true;
                        }
                    }
                }
            }
        }
        else
        {
            lblSelectedRecord.Text = "";
            DataTable dtUnit1 = (DataTable)Session["dtBinFilter"];
            gvRoleMasterBin.DataSource = dtUnit1;
            gvRoleMasterBin.DataBind();
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
                    b = objRole.DeleteRoleMaster(Session["CompId"].ToString(), lblSelectedRecord.Text.Split(',')[j].Trim(), true.ToString(), Session["UserId"].ToString(), DateTime.Now.ToString());

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
            foreach (GridViewRow Gvr in gvRoleMasterBin.Rows)
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

       
      
        txtRoleName.Text = "";
        txtRoleNameL.Text = "";



        navTree.DataSource = null;
        navTree.DataBind();
        navTree.Nodes.Clear();


        
        btnNew.Text = Resources.Attendance.New;
        lblSelectedRecord.Text = "";
        ViewState["Select"] = null;
        editid.Value = "";
        txtValue.Text = "";
        ddlOption.SelectedIndex = 2;
        ddlFieldName.SelectedIndex = 0;

        PnlRolePerm.Visible = false;


        FillBrand();
        FillLocation();
        FillDepartment();

    }

   
}
