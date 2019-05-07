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

public partial class MasterSetUp_UserMaster : BasePage
{
  
    Common objCmn = new Common();
    UserMaster objUser = new UserMaster();

    UserPermission objUserPermission = new UserPermission();

    SystemParameter objSysParam = new SystemParameter();

    ModuleMaster objModule = new ModuleMaster();
    ObjectMaster ObjItObject = new ObjectMaster();
    RoleMaster objRole = new RoleMaster();
    RolePermission objRolePermission = new RolePermission();
    SystemParameter objSys = new SystemParameter();
    EmployeeMaster objEmp = new EmployeeMaster();
    protected void Page_Load(object sender, EventArgs e)
    {
      
   
        //End Remove
        Session["BrandId"] = "1";
        Session["LocId"] = "1";
        if (Session["UserId"] == null)
        {
            Response.Redirect("~/ERPLogin.aspx");
        }

        AllPageCode();
        if (!IsPostBack)
        {
            txtValue.Focus();
            PnlUserPerm.Visible = false;
            FillGridBin();
            FillGrid();
            FillddlRole();
            
            btnList_Click(null, null);
            string userid = Session["UserId"].ToString().Trim();
            userid = userid.ToLower();



            DataTable dtUser = objUser.GetUserMasterByUserId(userid);
            if (dtUser.Rows.Count > 0)
            {
                if (dtUser.Rows[0]["Role_Name"].ToString() == "Super Admin")
                {
                    trSuperUser.Visible = true;
                    rbtnUser.Checked = true;
                }
                else
                {


                    trSuperUser.Visible = false;
                }

            }

                     
           
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

        txtPassword.Attributes.Add("Value", txtPassword.Text);
    }


    public void AllPageCode()
    {

        Page.Title = objSys.GetSysTitle();
        Session["AccordianId"] = "8";
        Session["HeaderText"] = "Master Setup";
        Common cmn = new Common();
        DataTable dtAllPageCode = cmn.GetAllPagePermission(Session["UserId"].ToString(), "8", "26");
        if (dtAllPageCode.Rows.Count != 0)
        {
            if (dtAllPageCode.Rows[0][0].ToString() == "SuperAdmin")
            {
                btnSave.Visible = true;
                foreach (GridViewRow Row in gvUserMaster.Rows)
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
                    foreach (GridViewRow Row in gvUserMaster.Rows)
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
    public void FillddlRole()
    {
        
        
        
        DataTable dt =new DataTable ();
       
            
             dt= objRole.GetRoleMaster(Session["CompId"].ToString());
      

      

       

        if (dt.Rows.Count > 0)
        {
            ddlRole.DataSource = null;
            ddlRole.DataBind();
            ddlRole.DataSource = dt;
            ddlRole.DataTextField = "Role_Name";
            ddlRole.DataValueField = "Role_Id";
            ddlRole.DataBind();
            ListItem li = new ListItem("--Select--", "0");
            ddlRole.Items.Insert(0, li);
            ddlRole.SelectedIndex = 0;

        }
        else
        {
            try
            {
                ddlRole.Items.Clear();
                ddlRole.DataSource = null;
                ddlRole.DataBind();
                ListItem li = new ListItem("--Select--", "0");
                ddlRole.Items.Insert(0, li);
                ddlRole.SelectedIndex = 0;
            }
            catch
            {
                ListItem li = new ListItem("--Select--", "0");
                ddlRole.Items.Insert(0, li);
                ddlRole.SelectedIndex = 0;

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





    protected void chkEditRoleCheckedChanged(object sender, EventArgs e)
    {
        if(chkEditRole.Checked)
    {
        navTree.Enabled = true;

        
    }
    else
        {
            navTree.Enabled = false;
           
           
            
    }
       BindTree();
          }
    protected void rbtnUserCheckedChanged(object sender, EventArgs e)
    {
        if(rbtnSuperAdmin.Checked)
        {
            trEditRole.Visible = false;
            trEmp.Visible = false;
            trRole.Visible = false;
            PnlUserPerm.Visible = false;

        }
        else if(rbtnUser.Checked)
        {

            trEditRole.Visible = true;
            trEmp.Visible = true;
            trRole.Visible = true;

        }

    }
    protected void ddlRole_SelectIndexChanged(object sender, EventArgs e)
    {
        if (ddlRole.SelectedIndex != 0)
        {
            PnlUserPerm.Visible = true;
            hdnRoleId.Value = ddlRole.SelectedValue;

            if (chkEditRole.Checked)
            {
                chkEditRoleCheckedChanged(null, null);

            }
            else
            {
                chkEditRoleCheckedChanged(null, null);

            }
            BindTreeChanged();

        }
        else
        {
            PnlUserPerm.Visible = false;
            

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
        FillGrid();
    }
    protected void btnNew_Click(object sender, EventArgs e)
    {
         txtEmp.Focus();
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
            DataTable dtCust = (DataTable)Session["User"];


            DataView view = new DataView(dtCust, condition, "", DataViewRowState.CurrentRows);
            Session["dtFilter"] = view.ToTable();
            lblTotalRecords.Text = Resources.Attendance.Total_Records + ": " + view.ToTable().Rows.Count.ToString() + "";

            gvUserMaster.DataSource = view.ToTable();
            gvUserMaster.DataBind();
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
            DataTable dtCust = (DataTable)Session["dtbinUser"];


            DataView view = new DataView(dtCust, condition, "", DataViewRowState.CurrentRows);
            Session["dtbinFilter"] = view.ToTable();
            lblbinTotalRecords.Text = Resources.Attendance.Total_Records + ": " + view.ToTable().Rows.Count.ToString() + "";

            gvUserMasterBin.DataSource = view.ToTable();
            gvUserMasterBin.DataBind();


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

    protected void gvUserMasterBin_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {

        gvUserMasterBin.PageIndex = e.NewPageIndex;
        if (HDFSortbin.Value == "")
            FillGridBin();
        else
        {
            DataTable dt = (DataTable)Session["dtbinFilter"];
            gvUserMasterBin.DataSource = dt;
            gvUserMasterBin.DataBind();
            AllPageCode();
        }
        string temp = string.Empty;
        bool isselcted;

        for (int i = 0; i < gvUserMasterBin.Rows.Count; i++)
        {
            Label lblconid = (Label)gvUserMasterBin.Rows[i].FindControl("lblUserId");
            string[] split = lblSelectedRecord.Text.Split(',');

            for (int j = 0; j < lblSelectedRecord.Text.Split(',').Length; j++)
            {
                if (lblSelectedRecord.Text.Split(',')[j] != "")
                {
                    if (lblconid.Text.Trim().ToString() == lblSelectedRecord.Text.Split(',')[j].Trim().ToString())
                    {
                        ((CheckBox)gvUserMasterBin.Rows[i].FindControl("chkgvSelect")).Checked = true;
                    }
                }
            }
        }

    }
    protected void gvUserMasterBin_OnSorting(object sender, GridViewSortEventArgs e)
    {
        lblSelectedRecord.Text = "";
        HDFSortbin.Value = HDFSortbin.Value == "ASC" ? "DESC" : "ASC";
        DataTable dt = new DataTable();
        dt = objUser.GetUserMasterInactive(Session["CompId"].ToString());
        DataView dv = new DataView(dt);
        string Query = "" + e.SortExpression + " " + HDFSortbin.Value + "";
        dv.Sort = Query;
        dt = dv.ToTable();
        Session["dtbinFilter"] = dt;
        gvUserMasterBin.DataSource = dt;
        gvUserMasterBin.DataBind();
        AllPageCode();

    }
    

   
    protected void btnSave_Click(object sender, EventArgs e)
    {
        int b = 0;

        string optype = string.Empty;

        string RoleName = string.Empty;
        DataTable dtUser = objUser.GetUserMasterByUserId(Session["UserId"].ToString());
        if (dtUser.Rows.Count > 0)
        {
            if (dtUser.Rows[0]["Role_Name"].ToString() == "Super Admin")
            {
                optype = "4";
            }
            else
            {
                optype = "2";

            }

        }

       

        string empid = txtEmp.Text.Split('/')[txtEmp.Text.Split('/').Length - 1];

        DataTable dtEmp = objEmp.GetEmployeeMaster(Session["CompId"].ToString());

        dtEmp = new DataView(dtEmp,"Emp_Code='"+empid+"' and Brand_Id='"+Session["BrandId"].ToString()+"' and Location_Id='"+Session["LocId"].ToString()+"'","",DataViewRowState.CurrentRows).ToTable();

        if(dtEmp.Rows.Count > 0)
        {
            empid=dtEmp.Rows[0]["Emp_Id"].ToString();
        }
        else
        {
            empid = "0";
        }




        if (txtUserName.Text == "")
        {
            DisplayMessage("Enter User Name");
            txtUserName.Focus();
            return;
        }

        if (txtPassword.Text == "")
        {
            DisplayMessage("Enter Password");
            txtPassword.Focus();
            return;
        }

       
        if (editid.Value == "")
        {

            DataTable dt1 = objUser.GetUserMaster();

            dt1 = new DataView(dt1, "User_Id='" + txtUserName.Text + "'", "", DataViewRowState.CurrentRows).ToTable();
            if (dt1.Rows.Count > 0)
            {
                DisplayMessage("User Id Already Exists");
                txtUserName.Focus();
                return;

            }


            if (rbtnSuperAdmin.Checked)
            {
                DataTable dtRole = objRole.GetRoleMasterByRoleName("0", "Super Admin");
                string roleid=string.Empty;
                if(dtRole.Rows.Count > 0)
                {
                    roleid=dtRole.Rows[0]["Role_Id"].ToString();

                }
                else
                {
                    roleid = "0";
                }

                b = objUser.InsertUserMaster("0", txtUserName.Text, txtPassword.Text,"0",roleid,false.ToString(),"", "", "", "", "", true.ToString(), DateTime.Now.ToString(), true.ToString(), Session["UserId"].ToString(), DateTime.Now.ToString(), Session["UserId"].ToString(), DateTime.Now.ToString());
                txtPassword.Attributes.Add("Value", txtPassword.Text);
            }
            else
            {
                if (dtEmp.Rows.Count == 0)
                {
                    DisplayMessage("Employee does not exists");
                    return;

                }
                if (ddlRole.SelectedIndex == 0)
                {
                    DisplayMessage("Select Role Name");
                    ddlRole.Focus();
                    return;

                }

                DataTable dt2 = objUser.GetUserMaster();

                dt2 = new DataView(dt1, "User_Id='" + txtUserName.Text + "'", "", DataViewRowState.CurrentRows).ToTable();
                if (dt2.Rows.Count > 0)
                {
                    DisplayMessage("User Id Already Exists");
                    txtUserName.Focus();
                    return;

                }
                b = objUser.InsertUserMaster(Session["CompId"].ToString(), txtUserName.Text, txtPassword.Text, empid, ddlRole.SelectedValue, chkEditRole.Checked.ToString(), "", "", "", "", "", true.ToString(), DateTime.Now.ToString(), true.ToString(), Session["UserId"].ToString(), DateTime.Now.ToString(), Session["UserId"].ToString(), DateTime.Now.ToString());
                txtPassword.Attributes.Add("Value", txtPassword.Text);
            }


         
            if (b != 0)
            {
                DisplayMessage("Record Saved");




                if (rbtnSuperAdmin.Checked)
                {
                    FillGrid();
                    Reset();
                    btnList_Click(null,null);
                   
                }
                else
                {
                    FillGrid();

                    editid.Value = txtUserName.Text;


                    if (chkEditRole.Checked)
                    {

                        btnSavePerm_Click(null, null);

                    }
                    
                    Reset();
                    btnList_Click(null, null);
                }
             
              
               
                
            }
            else
            {
                DisplayMessage("Record Not Saved");
            }
        }
        else
        {
            DataTable dt1 = objUser.GetUserMasterByUserIdByCompId(Session["UserId"].ToString(), optype);

            dt1 = new DataView(dt1, "User_Id='" + txtUserName.Text + "'", "", DataViewRowState.CurrentRows).ToTable();
            if (dt1.Rows.Count > 0 && dt1.Rows[0]["User_Id"].ToString()!=editid.Value)
            {
                DisplayMessage("User Id Already Exists");
                txtUserName.Focus();
                return;

            }
            


            if (rbtnSuperAdmin.Checked)
            {
                DataTable dtRole = objRole.GetRoleMasterByRoleName("0", "Super Admin");
                string roleid = string.Empty;
                if (dtRole.Rows.Count > 0)
                {
                    roleid = dtRole.Rows[0]["Role_Id"].ToString();

                }
                else
                {
                    roleid = "0";
                }

                b = objUser.UpdateUserMaster(editid.Value,"0", txtUserName.Text, txtPassword.Text,empid,roleid,false.ToString(),"", "", "", "", "", true.ToString(), DateTime.Now.ToString(), true.ToString(), Session["UserId"].ToString(), DateTime.Now.ToString());

            }
            else
            {
                if (ddlRole.SelectedIndex == 0)
                {
                    DisplayMessage("Select Role Name");
                    ddlRole.Focus();
                    return;

                }

                b = objUser.UpdateUserMaster(editid.Value, Session["CompId"].ToString(), txtUserName.Text, txtPassword.Text, empid, ddlRole.SelectedValue, chkEditRole.Checked.ToString(), "", "", "", "", "", true.ToString(), DateTime.Now.ToString(), true.ToString(), Session["UserId"].ToString(), DateTime.Now.ToString());
            }
         
            if (b != 0)
            {
                
                DisplayMessage("Record Updated");
                if (rbtnSuperAdmin.Checked)
                {
                    txtPassword.Attributes.Add("Value", txtPassword.Text);
                    FillGrid();
                    Reset();
                    btnList_Click(null, null);

                }
                else
                {
                    FillGrid();
                    txtPassword.Attributes.Add("Value", txtPassword.Text);
                    if (chkEditRole.Checked)
                    {

                        btnSavePerm_Click(null, null);

                    }
                    Reset();
                    btnList_Click(null, null);
                }
              
               
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
            AppId = dtApp.Rows[0]["Param_Value"].ToString().Trim();

        }
        else
        {
            return;
        }



        string RoleId = string.Empty;
        string moduleids = string.Empty;


        navTree.DataSource = null;
        navTree.DataBind();
        navTree.Nodes.Clear();

        DataTable dtRoleP =new DataTable ();

        if(chkEditRole.Checked)
        {
            if(editid.Value!="")
            {
            dtRoleP = objUserPermission.GetUserPermissionById(editid.Value);
                if(dtRoleP.Rows.Count==0)
                {

                    dtRoleP = objRolePermission.GetRolePermissionById(hdnRoleId.Value);
                }
            }
            else
            {
                dtRoleP = objRolePermission.GetRolePermissionById(hdnRoleId.Value);
            }
        }
        else
        {
            objUserPermission.DeleteUserPermission(editid.Value);
            dtRoleP = objRolePermission.GetRolePermissionById(hdnRoleId.Value);
        }


        if (dtRoleP.Rows.Count > 0)
        {


        }


        DataTable dtModule = objModule.GetModuleMaster();

        DataTable DtModuleApp = new DataTable();

        DtModuleApp = objModule.GetModuleObjectByApplicatonId(AppId);


        for (int i = 0; i < DtModuleApp.Rows.Count; i++)
        {
            moduleids += "'" + DtModuleApp.Rows[i]["Module_Id"].ToString() + "'" + ",";
        }

        dtModule = new DataView(dtModule, "Module_Id in (" + moduleids.Substring(0, moduleids.Length - 1) + ")", "", DataViewRowState.CurrentRows).ToTable();

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



            DataTable dtAllChild = ObjItObject.GetObjectMasterByModuleId_ApplicationId(datarow["Module_Id"].ToString(), AppId);




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

                        if (dtOp.Rows.Count > 0)
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
        navTree.ExpandAll();

        if (chkEditRole.Checked)
        {

            navTree.Enabled = true;
           
        }
        else
        {
            navTree.Enabled = false;

        }




        return;







    }



    public void BindTreeChanged()
    {
        string AppId = string.Empty;
        DataTable dtApp = objSysParam.GetSysParameterByParamName("Application_Id");
        if (dtApp.Rows.Count > 0)
        {
            AppId = dtApp.Rows[0]["Param_Value"].ToString().Trim();

        }
        else
        {
            return;
        }



        string RoleId = string.Empty;
        string moduleids = string.Empty;


        navTree.DataSource = null;
        navTree.DataBind();
        navTree.Nodes.Clear();

        DataTable dtRoleP = new DataTable();

        
            
            dtRoleP = objRolePermission.GetRolePermissionById(hdnRoleId.Value);
      


        if (dtRoleP.Rows.Count > 0)
        {


        }


        DataTable dtModule = objModule.GetModuleMaster();

        DataTable DtModuleApp = new DataTable();

        DtModuleApp = objModule.GetModuleObjectByApplicatonId(AppId);


        for (int i = 0; i < DtModuleApp.Rows.Count; i++)
        {
            moduleids += "'" + DtModuleApp.Rows[i]["Module_Id"].ToString() + "'" + ",";
        }

        dtModule = new DataView(dtModule, "Module_Id in (" + moduleids.Substring(0, moduleids.Length - 1) + ")", "", DataViewRowState.CurrentRows).ToTable();

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



            DataTable dtAllChild = ObjItObject.GetObjectMasterByModuleId_ApplicationId(datarow["Module_Id"].ToString(), AppId);




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

                        if (dtOp.Rows.Count > 0)
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
        navTree.ExpandAll();

        if (chkEditRole.Checked)
        {

            navTree.Enabled = true;

        }
        else
        {
            navTree.Enabled = false;

        }




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

          
           

            
            objUserPermission.DeleteUserPermission(editid.Value);


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

                            refid1=objUserPermission.InsertUserPermission(editid.Value,ModuleNode.Value,ObjNode.Value,true.ToString(),Session["UserId"].ToString(),DateTime.Now.ToString(),Session["UserId"].ToString(),DateTime.Now.ToString());

                            string refid = refid1.ToString();
                            string OpIds = string.Empty;
                            foreach (TreeNode OpNode in ObjNode.ChildNodes)
                            {
                                if (OpNode.Checked)
                                {
                                    if (OpNode.Value == "1")
                                    {
                                        objUserPermission.InsertUserOpPermission(refid, true.ToString(), false.ToString(), false.ToString(), false.ToString(), false.ToString(), false.ToString(), false.ToString(), false.ToString());
                                    }
                                    else if (OpNode.Value == "2")
                                    {
                                        objUserPermission.InsertUserOpPermission(refid, false.ToString(), true.ToString(), false.ToString(), false.ToString(), false.ToString(), false.ToString(), false.ToString(), false.ToString());
                                    }
                                    else if (OpNode.Value == "3")
                                    {
                                        objUserPermission.InsertUserOpPermission(refid, false.ToString(), false.ToString(), true.ToString(), false.ToString(), false.ToString(), false.ToString(), false.ToString(), false.ToString());
                                    }
                                    else if (OpNode.Value == "4")
                                    {
                                        objUserPermission.InsertUserOpPermission(refid, false.ToString(), false.ToString(), false.ToString(), true.ToString(), false.ToString(), false.ToString(), false.ToString(), false.ToString());
                                    }
                                    else if (OpNode.Value == "5")
                                    {
                                        objUserPermission.InsertUserOpPermission(refid, false.ToString(), false.ToString(), false.ToString(), false.ToString(), true.ToString(), false.ToString(), false.ToString(), false.ToString());
                                    }
                                    else if (OpNode.Value == "6")
                                    {
                                        objUserPermission.InsertUserOpPermission(refid, false.ToString(), false.ToString(), false.ToString(), false.ToString(), false.ToString(), true.ToString(), false.ToString(), false.ToString());

                                    }
                                    else if (OpNode.Value == "7")
                                    {
                                        objUserPermission.InsertUserOpPermission(refid, false.ToString(), false.ToString(), false.ToString(), false.ToString(), false.ToString(), false.ToString(), true.ToString(), false.ToString());
                                    }
                                    else if (OpNode.Value == "8")
                                    {
                                        objUserPermission.InsertUserOpPermission(refid, false.ToString(), false.ToString(), false.ToString(), false.ToString(), false.ToString(), false.ToString(), false.ToString(), true.ToString());
                                    }

                                }
                                else
                                {
                                    if (OpNode.Value == "1")
                                    {
                                        objUserPermission.InsertUserOpPermission(refid, false.ToString(), false.ToString(), false.ToString(), false.ToString(), false.ToString(), false.ToString(), false.ToString(), false.ToString());
                                    }
                                    else if (OpNode.Value == "2")
                                    {
                                        objUserPermission.InsertUserOpPermission(refid, false.ToString(), false.ToString(), false.ToString(), false.ToString(), false.ToString(), false.ToString(), false.ToString(), false.ToString());
                                    }
                                    else if (OpNode.Value == "3")
                                    {
                                        objUserPermission.InsertUserOpPermission(refid, false.ToString(), false.ToString(), false.ToString(), false.ToString(), false.ToString(), false.ToString(), false.ToString(), false.ToString());
                                    }
                                    else if (OpNode.Value == "4")
                                    {
                                        objUserPermission.InsertUserOpPermission(refid, false.ToString(), false.ToString(), false.ToString(), false.ToString(), false.ToString(), false.ToString(), false.ToString(), false.ToString());
                                    }
                                    else if (OpNode.Value == "5")
                                    {
                                        objUserPermission.InsertUserOpPermission(refid, false.ToString(), false.ToString(), false.ToString(), false.ToString(), false.ToString(), false.ToString(), false.ToString(), false.ToString());
                                    }
                                    else if (OpNode.Value == "6")
                                    {
                                        objUserPermission.InsertUserOpPermission(refid, false.ToString(), false.ToString(), false.ToString(), false.ToString(), false.ToString(), false.ToString(), false.ToString(), false.ToString());

                                    }
                                    else if (OpNode.Value == "7")
                                    {
                                        objUserPermission.InsertUserOpPermission(refid, false.ToString(), false.ToString(), false.ToString(), false.ToString(), false.ToString(), false.ToString(), false.ToString(), false.ToString());
                                    }
                                    else if (OpNode.Value == "8")
                                    {
                                        objUserPermission.InsertUserOpPermission(refid, false.ToString(), false.ToString(), false.ToString(), false.ToString(), false.ToString(), false.ToString(), false.ToString(), false.ToString());
                                    }
                                }


                            }

                             
                             
                        }
                        
                    }


                }

            }


            DisplayMessage("Record Saved");

            btnList_Click(null, null);
            Reset();
            FillGrid();
            FillGridBin();

           
        
     }

     protected void btnCancelPerm_Click(object sender, EventArgs e)
    {
        btnList_Click(null,null);
        Reset();
        FillGrid();
        FillGridBin();
        
        PnlUserPerm.Visible = false;
        PnlUser.Visible = true;
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
       

        DataTable dt = objUser.GetUserMasterByUserId(editid.Value);
        if (dt.Rows.Count > 0)
        {
            
            txtUserName.Text = dt.Rows[0]["User_Id"].ToString();

            txtPassword.Attributes.Add("Value",dt.Rows[0]["Password"].ToString());

           
            DataTable dtUser = objUser.GetUserMasterByUserId(editid.Value.ToLower());
            if (dtUser.Rows.Count > 0)
            {
                if (dtUser.Rows[0]["Role_Name"].ToString() == "Super Admin")
                {
                   
                    rbtnUser.Checked = false;
                    rbtnSuperAdmin.Checked = true;
                    trRole.Visible = false;
                    trEmp.Visible = false;
                    trEditRole.Visible = false;
                }
                else
                {
                    rbtnSuperAdmin.Checked = false;
                    rbtnUser.Checked = true;

                    trRole.Visible = true;
                    trEmp.Visible = true;
                    trEditRole.Visible = true;
                    ddlRole.SelectedValue = dt.Rows[0]["Role_Id"].ToString();
                    hdnRoleId.Value = ddlRole.SelectedValue;

                    txtEmp.Text = objCmn.GetEmpName(dt.Rows[0]["Emp_Id"].ToString());

                    if (Convert.ToBoolean(dt.Rows[0]["Is_Modified"].ToString()))
                    {
                        chkEditRole.Checked = true;
                        chkEditRoleCheckedChanged(null, null);
                        BindTree();
                    }
                    else
                    {
                        chkEditRole.Checked = false;
                        chkEditRoleCheckedChanged(null, null);
                        ddlRole_SelectIndexChanged(null, null);
                    }
                }
            }
            btnNew_Click(null, null);
            btnNew.Text = Resources.Attendance.Edit;

        }
        PnlUserPerm.Visible = true;
      

    }

    protected void IbtnDelete_Command(object sender, CommandEventArgs e)
    {
        int b = 0;
        b = objUser.DeleteUserMaster(Session["CompId"].ToString(),e.CommandArgument.ToString(), false.ToString(), Session["UserId"].ToString(), DateTime.Now.ToString());
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
    protected void gvUserMaster_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        gvUserMaster.PageIndex = e.NewPageIndex;

        DataTable dt = (DataTable)Session["dtFilter"];
        gvUserMaster.DataSource = dt;
        gvUserMaster.DataBind();
        AllPageCode();
       
         string userid = Session["UserId"].ToString().Trim();
        userid = userid.ToLower();
        if (userid == "superadmin")
        {
            foreach (GridViewRow Row in gvUserMaster.Rows)
            {


                if (((Label)Row.FindControl("lblUserId1")).Text.Trim().ToLower() == "superadmin")
                {
                    ((ImageButton)Row.FindControl("btnEdit")).Visible = false;
                    ((ImageButton)Row.FindControl("IbtnDelete")).Visible = false;
                }
            }
        }
        else
        {
            foreach (GridViewRow Row in gvUserMaster.Rows)
            {
                DataTable dtRole = objRole.GetRoleMasterByRoleName("0", "Super Admin");
                string roleid = string.Empty;
                if (dtRole.Rows.Count > 0)
                {
                    roleid = dtRole.Rows[0]["Role_Id"].ToString();

                }
                else
                {
                    roleid = "0";
                }
                string lblRolid = ((Label)Row.FindControl("lblRoleId")).Text.Trim();
                string userId = ((Label)Row.FindControl("lblUserId1")).Text.Trim();
                if (userId.Trim().ToLower() == Session["UserId"].ToString().Trim().ToLower())
                {
                    ((ImageButton)Row.FindControl("btnEdit")).Visible = true;
                    ((ImageButton)Row.FindControl("IbtnDelete")).Visible = false;

                }
                else
                {
                    if (((Label)Row.FindControl("lblRoleId")).Text.Trim() == roleid)
                    {
                        DataTable dtUser1 = objUser.GetUserMasterByUserId(userId);

                        dtUser1 = new DataView(dtUser1, "CreatedBy='" + Session["UserId"].ToString().Trim() + "'", "", DataViewRowState.CurrentRows).ToTable();


                        if (dtUser1.Rows.Count > 0)
                        {
                            ((ImageButton)Row.FindControl("btnEdit")).Visible = true;
                            ((ImageButton)Row.FindControl("IbtnDelete")).Visible = true;


                        }
                        else
                        {




                            ((ImageButton)Row.FindControl("btnEdit")).Visible = false;
                            ((ImageButton)Row.FindControl("IbtnDelete")).Visible = false;
                        }
                    }
                    else
                    {
                        DataTable dtUser1 = objUser.GetUserMasterByUserId(userId);

                        dtUser1 = new DataView(dtUser1, "CreatedBy='" + Session["UserId"].ToString().Trim() + "'", "", DataViewRowState.CurrentRows).ToTable();
                        if (dtUser1.Rows.Count > 0)
                        {
                            ((ImageButton)Row.FindControl("btnEdit")).Visible = true;
                            ((ImageButton)Row.FindControl("IbtnDelete")).Visible = true;

                        }
                        else
                        {

                            ((ImageButton)Row.FindControl("btnEdit")).Visible = false;
                            ((ImageButton)Row.FindControl("IbtnDelete")).Visible = false;
                        }
                    }
                }
            }
        }
    
    }
    protected void gvUserMaster_OnSorting(object sender, GridViewSortEventArgs e)
    {
        HDFSort.Value = HDFSort.Value == "ASC" ? "DESC" : "ASC";
        DataTable dt = new DataTable();
        dt = (DataTable)Session["dtFilter"];
        DataView dv = new DataView(dt);
        string Query = "" + e.SortExpression + " " + HDFSort.Value + "";
        dv.Sort = Query;
        dt = dv.ToTable();
        Session["dtFilter"] = dt;
        gvUserMaster.DataSource = dt;
        gvUserMaster.DataBind();
        AllPageCode();
        string userid = Session["UserId"].ToString().Trim();
        userid = userid.ToLower();
        if (userid == "superadmin")
        {
            foreach (GridViewRow Row in gvUserMaster.Rows)
            {


                if (((Label)Row.FindControl("lblUserId1")).Text.Trim().ToLower() == "superadmin")
                {
                    ((ImageButton)Row.FindControl("btnEdit")).Visible = false;
                    ((ImageButton)Row.FindControl("IbtnDelete")).Visible = false;
                }
            }
        }
        else
        {
            foreach (GridViewRow Row in gvUserMaster.Rows)
            {
                DataTable dtRole = objRole.GetRoleMasterByRoleName("0", "Super Admin");
                string roleid = string.Empty;
                if (dtRole.Rows.Count > 0)
                {
                    roleid = dtRole.Rows[0]["Role_Id"].ToString();

                }
                else
                {
                    roleid = "0";
                }
                string lblRolid = ((Label)Row.FindControl("lblRoleId")).Text.Trim();
                string userId = ((Label)Row.FindControl("lblUserId1")).Text.Trim();
                if (userId.Trim().ToLower() == Session["UserId"].ToString().Trim().ToLower())
                {
                    ((ImageButton)Row.FindControl("btnEdit")).Visible = true;
                    ((ImageButton)Row.FindControl("IbtnDelete")).Visible = false;

                }
                else
                {
                    if (((Label)Row.FindControl("lblRoleId")).Text.Trim() == roleid)
                    {
                        DataTable dtUser1 = objUser.GetUserMasterByUserId(userId);

                        dtUser1 = new DataView(dtUser1, "CreatedBy='" + Session["UserId"].ToString().Trim() + "'", "", DataViewRowState.CurrentRows).ToTable();


                        if (dtUser1.Rows.Count > 0)
                        {
                            ((ImageButton)Row.FindControl("btnEdit")).Visible = true;
                            ((ImageButton)Row.FindControl("IbtnDelete")).Visible = true;


                        }
                        else
                        {




                            ((ImageButton)Row.FindControl("btnEdit")).Visible = false;
                            ((ImageButton)Row.FindControl("IbtnDelete")).Visible = false;
                        }
                    }

                    else
                    {

                        DataTable dtUser1 = objUser.GetUserMasterByUserId(userId);

                        dtUser1 = new DataView(dtUser1, "CreatedBy='" + Session["UserId"].ToString().Trim() + "'", "", DataViewRowState.CurrentRows).ToTable();
                        if (dtUser1.Rows.Count > 0)
                        {
                            ((ImageButton)Row.FindControl("btnEdit")).Visible = true;
                            ((ImageButton)Row.FindControl("IbtnDelete")).Visible = true;

                        }
                        else
                        {

                            ((ImageButton)Row.FindControl("btnEdit")).Visible = false;
                            ((ImageButton)Row.FindControl("IbtnDelete")).Visible = false;
                        }
                    }
                }
            }
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
        string optype=string.Empty;

        string RoleName=string.Empty;
        DataTable dtUser=objUser.GetUserMasterByUserId(Session["UserId"].ToString());



        if(dtUser.Rows.Count > 0)
        {
            if(dtUser.Rows[0]["Role_Name"].ToString()=="Super Admin")
            {
                optype="4";
            }
            else
            { optype="5";

            }

        }

       

        DataTable dt = objUser.GetUserMasterByUserIdByCompId(Session["UserId"].ToString(),optype);
        gvUserMaster.DataSource = dt;
        gvUserMaster.DataBind();
        AllPageCode();
        Session["dtFilter"] = dt;
        Session["User"] = dt;

        lblTotalRecords.Text = Resources.Attendance.Total_Records + ": " + dt.Rows.Count.ToString() + "";

        string userid = Session["UserId"].ToString().Trim();
        userid = userid.ToLower();
        if (userid == "superadmin")
        {
            foreach (GridViewRow Row in gvUserMaster.Rows)
            {
               

                if (((Label)Row.FindControl("lblUserId1")).Text.Trim().ToLower() =="superadmin" )
                {
                    ((ImageButton)Row.FindControl("btnEdit")).Visible = false;
                    ((ImageButton)Row.FindControl("IbtnDelete")).Visible = false;
                }
            }
        }
        else
        {

            foreach (GridViewRow Row in gvUserMaster.Rows)
            {
                DataTable dtRole = objRole.GetRoleMasterByRoleName("0", "Super Admin");
                string roleid = string.Empty;
                if (dtRole.Rows.Count > 0)
                {
                    roleid = dtRole.Rows[0]["Role_Id"].ToString();

                }
                else
                {
                    roleid = "0";
                }
                string lblRolid = ((Label)Row.FindControl("lblRoleId")).Text.Trim();
                string userId = ((Label)Row.FindControl("lblUserId1")).Text.Trim();

                if (userId.Trim().ToLower() == Session["UserId"].ToString().Trim().ToLower())
                {
                    ((ImageButton)Row.FindControl("btnEdit")).Visible = true;
                    ((ImageButton)Row.FindControl("IbtnDelete")).Visible = false;

                }
                else
                {
                    if (((Label)Row.FindControl("lblRoleId")).Text.Trim() == roleid)
                    {
                        DataTable dtUser1 = objUser.GetUserMasterByUserId(userId);

                        dtUser1 = new DataView(dtUser1, "CreatedBy='" + Session["UserId"].ToString().Trim() + "'", "", DataViewRowState.CurrentRows).ToTable();


                        if (dtUser1.Rows.Count > 0)
                        {
                            ((ImageButton)Row.FindControl("btnEdit")).Visible = true;
                            ((ImageButton)Row.FindControl("IbtnDelete")).Visible = true;


                        }
                        else
                        {




                            ((ImageButton)Row.FindControl("btnEdit")).Visible = false;
                            ((ImageButton)Row.FindControl("IbtnDelete")).Visible = false;
                        }
                    }
                    else
                    {

                        DataTable dtUser1 = objUser.GetUserMasterByUserId(userId);

                        dtUser1 = new DataView(dtUser1, "CreatedBy='" + Session["UserId"].ToString().Trim() + "'", "", DataViewRowState.CurrentRows).ToTable();
                        if(dtUser1.Rows.Count > 0)
                        {
                            ((ImageButton)Row.FindControl("btnEdit")).Visible = true;
                            ((ImageButton)Row.FindControl("IbtnDelete")).Visible = true;

                        }
                        else
                        {

                            ((ImageButton)Row.FindControl("btnEdit")).Visible = false;
                            ((ImageButton)Row.FindControl("IbtnDelete")).Visible = false;
                        }

                    }
                }
            }
        }
      
    }
    public void FillGridBin()
    {
        DataTable dt = new DataTable();
        dt = objUser.GetUserMasterInactive(Session["CompId"].ToString());

        gvUserMasterBin.DataSource = dt;
        gvUserMasterBin.DataBind();


        Session["dtbinFilter"] = dt;
        Session["dtbinUser"] = dt;
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
        CheckBox chkSelAll = ((CheckBox)gvUserMasterBin.HeaderRow.FindControl("chkgvSelectAll"));
        for (int i = 0; i < gvUserMasterBin.Rows.Count; i++)
        {
            ((CheckBox)gvUserMasterBin.Rows[i].FindControl("chkgvSelect")).Checked = chkSelAll.Checked;
            if (chkSelAll.Checked)
            {
                if (!lblSelectedRecord.Text.Split(',').Contains(((Label)(gvUserMasterBin.Rows[i].FindControl("lblUserId"))).Text.Trim().ToString()))
                {
                    lblSelectedRecord.Text += ((Label)(gvUserMasterBin.Rows[i].FindControl("lblUserId"))).Text.Trim().ToString() + ",";
                }
            }
            else
            {
                string temp = string.Empty;
                string[] split = lblSelectedRecord.Text.Split(',');
                foreach (string item in split)
                {
                    if (item != ((Label)(gvUserMasterBin.Rows[i].FindControl("lblUserId"))).Text.Trim().ToString())
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
        Label lb = (Label)gvUserMasterBin.Rows[index].FindControl("lblUserId");
        if (((CheckBox)gvUserMasterBin.Rows[index].FindControl("chkgvSelect")).Checked)
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
                if (!lblSelectedRecord.Text.Split(',').Contains(dr["User_Id"]))
                {
                    lblSelectedRecord.Text += dr["User_Id"] + ",";
                }
            }
            for (int i = 0; i < gvUserMasterBin.Rows.Count; i++)
            {
                string[] split = lblSelectedRecord.Text.Split(',');
                Label lblconid = (Label)gvUserMasterBin.Rows[i].FindControl("lblUserId");
                for (int j = 0; j < lblSelectedRecord.Text.Split(',').Length; j++)
                {
                    if (lblSelectedRecord.Text.Split(',')[j] != "")
                    {
                        if (lblconid.Text.Trim().ToString() == lblSelectedRecord.Text.Split(',')[j].Trim().ToString())
                        {
                            ((CheckBox)gvUserMasterBin.Rows[i].FindControl("chkgvSelect")).Checked = true;
                        }
                    }
                }
            }
        }
        else
        {
            lblSelectedRecord.Text = "";
            DataTable dtUnit1 = (DataTable)Session["dtBinFilter"];
            gvUserMasterBin.DataSource = dtUnit1;
            gvUserMasterBin.DataBind();
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
                    b = objUser.DeleteUserMaster(Session["CompId"].ToString(), lblSelectedRecord.Text.Split(',')[j].Trim(), true.ToString(), Session["UserId"].ToString(), DateTime.Now.ToString());

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
            foreach (GridViewRow Gvr in gvUserMasterBin.Rows)
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

       
      
        txtUserName.Text = "";
        txtPassword.Text = "";
        txtEmp.Text = "";

        txtPassword.Attributes.Add("Value",txtPassword.Text);


        navTree.DataSource = null;
        navTree.DataBind();
        navTree.Nodes.Clear();


        chkEditRole.Checked = false;
        btnNew.Text = Resources.Attendance.New;
        lblSelectedRecord.Text = "";
        ViewState["Select"] = null;
        editid.Value = "";
        txtValue.Text = "";
        ddlOption.SelectedIndex = 2;
        ddlFieldName.SelectedIndex = 0;
        hdnRoleId.Value = "";
        ddlRole.SelectedIndex = 0;
        rbtnSuperAdmin.Checked = false;
        rbtnUser.Checked = true;

        trRole.Visible = true;
        trEmp.Visible = true;
        trEditRole.Visible = true;
    }
   [System.Web.Services.WebMethodAttribute(), System.Web.Script.Services.ScriptMethodAttribute()]
   public static string[] GetCompletionList(string prefixText, int count, string contextKey)
   {
       DataTable dt = Common.GetEmployee(prefixText, HttpContext.Current.Session["CompId"].ToString());

       dt = new DataView(dt, "Brand_Id='" + HttpContext.Current.Session["BrandId"].ToString() + "' and Location_Id='" + HttpContext.Current.Session["LocId"].ToString() + "'", "", DataViewRowState.CurrentRows).ToTable();


       string[] str = new string[dt.Rows.Count];
       for (int i = 0; i < dt.Rows.Count; i++)
       {
           str[i] = "" + dt.Rows[i][1].ToString() + "/(" + dt.Rows[i][2].ToString() + ")/" + dt.Rows[i][0].ToString() + "";
       }
       return str;
   }


   [System.Web.Services.WebMethodAttribute(), System.Web.Script.Services.ScriptMethodAttribute()]
   public static string[] GetCompletionListUser(string prefixText, int count, string contextKey)
   {
       UserMaster objUser1 = new UserMaster();
       string optype = string.Empty;

       string RoleName = string.Empty;
       DataTable dtUser = objUser1.GetUserMasterByUserId(HttpContext.Current.Session["UserId"].ToString());
       if (dtUser.Rows.Count > 0)
       {
           if (dtUser.Rows[0]["Role_Name"].ToString() == "Super Admin")
           {
               optype = "4";
           }
           else
           {
               optype = "5";

           }

       }
      
       DataTable dt = new DataView(objUser1.GetUserMasterByUserIdByCompId(HttpContext.Current.Session["CompId"].ToString(),optype), "User_Id like '" + prefixText.ToString() + "%'", "", DataViewRowState.CurrentRows).ToTable();
       string[] txt = new string[dt.Rows.Count];

       for (int i = 0; i < dt.Rows.Count; i++)
       {
           txt[i] = dt.Rows[i]["User_Id"].ToString();
       }
       return txt;
   }

   protected void ddlEmp_TextChanged(object sender, EventArgs e)
   {
       string empid = txtEmp.Text.Split('/')[txtEmp.Text.Split('/').Length - 1];
       txtUserName.Text = empid;
       txtPassword.Attributes.Add("Value", empid);
       ddlRole.Focus();
   }
}
