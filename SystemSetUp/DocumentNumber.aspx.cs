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

public partial class SystemSetUp_DocumentNumber : System.Web.UI.Page
{
    string StrBrandId = string.Empty;
    string StrLocId = string.Empty;
    string strCompId = string.Empty;
    Common cmn = new Common();
    ModuleMaster objModule = new ModuleMaster();
    ObjectMaster objectEntry = new ObjectMaster();
    SystemParameter objSys = new SystemParameter();
    Set_DocNumber objDocNo = new Set_DocNumber();
    UserMaster ObjUser = new UserMaster();
    EmployeeMaster Objemployee = new EmployeeMaster();
    protected void Page_Load(object sender, EventArgs e)
    {



        StrBrandId = Session["BrandId"].ToString();
        StrLocId = Session["LocId"].ToString();
        strCompId = Session["CompId"].ToString();
        if (Session["UserId"] == null)
        {
            Response.Redirect("~/ERPLogin.aspx");
        }
        AllPageCode();
 
        if (!IsPostBack)
        {
            txtValue.Focus();

            FillModule();
            FillGrid();
            btnList_Click(null, null);
            ddlObjectName.Items.Insert(0, "--Select--");
            ddlObjectName.SelectedIndex = 0;

        }
    }
    public string GetDocumentNumber()
    {
        string DocumentNo = string.Empty;

        DataTable Dt = objDocNo.GetDocumentNumberAll(strCompId, "7", "90");
        if (Dt.Rows.Count > 0)
        {
            if (Dt.Rows[0]["Prefix"].ToString() != "")
            {
                DocumentNo += Dt.Rows[0]["Prefix"].ToString();
            }



            if (Convert.ToBoolean(Dt.Rows[0]["CompId"].ToString()))
            {
                DocumentNo += strCompId;
            }

            if (Convert.ToBoolean(Dt.Rows[0]["BrandId"].ToString()))
            {
                DocumentNo += StrBrandId;
            }

            if (Convert.ToBoolean(Dt.Rows[0]["LocationId"].ToString()))
            {
                DocumentNo += StrLocId;
            }

            if (Convert.ToBoolean(Dt.Rows[0]["DeptId"].ToString()))
            {
                DocumentNo += (string)Session["SessionDepId"];
            }

            if (Convert.ToBoolean(Dt.Rows[0]["EmpId"].ToString()))
            {

                DataTable Dtuser = ObjUser.GetUserMasterByUserId(Session["UserId"].ToString());
                DocumentNo += Dtuser.Rows[0]["Emp_Id"].ToString();

            }

            if (Convert.ToBoolean(Dt.Rows[0]["Year"].ToString()))
            {
                DocumentNo += DateTime.Now.Year.ToString();
            }

            if (Convert.ToBoolean(Dt.Rows[0]["Month"].ToString()))
            {
                DocumentNo += DateTime.Now.Month.ToString();
            }

            if (Convert.ToBoolean(Dt.Rows[0]["Day"].ToString()))
            {
                DocumentNo += DateTime.Now.Day.ToString();
            }

            if (Dt.Rows[0]["Suffix"].ToString() != "")
            {
                DocumentNo += Dt.Rows[0]["Suffix"].ToString();
            }
            if (DocumentNo != "")
            {
                DocumentNo += "-" + (Convert.ToInt32(objDocNo.GetDocumentNumberAll(strCompId.ToString()).Rows.Count) + 1).ToString();
            }
            else
            {
                DocumentNo += (Convert.ToInt32(objDocNo.GetDocumentNumberAll(strCompId.ToString()).Rows.Count) + 1).ToString();
        
            }
        }
        else
        {
            DocumentNo += (Convert.ToInt32(objDocNo.GetDocumentNumberAll(strCompId.ToString()).Rows.Count) + 1).ToString();
        }

        return DocumentNo;

    } 
    
    public void AllPageCode()
    {
        Page.Title = objSys.GetSysTitle();
        Session["AccordianId"] = "7";//module id
        Session["HeaderText"] = "SystemSetUp";//module Name
        DataTable dtAllPageCode = cmn.GetAllPagePermission(Session["UserId"].ToString(), "7", "90");
        if (dtAllPageCode.Rows.Count != 0)
        {
            if (dtAllPageCode.Rows[0][0].ToString() == "SuperAdmin")
            {
                btnSave.Visible = true;
                foreach (GridViewRow Row in gvDocMaster.Rows)
                {
                    ((ImageButton)Row.FindControl("btnEdit")).Visible = true;
                    ((ImageButton)Row.FindControl("IbtnDelete")).Visible = true;
                }
                //imgBtnRestore.Visible = true;
                //ImgbtnSelectAll.Visible = true;
            }
            else
            {
                foreach (DataRow DtRow in dtAllPageCode.Rows)
                {
                    if (Convert.ToBoolean(DtRow["Op_Add"].ToString()))
                    {
                        btnSave.Visible = true;
                    }
                    foreach (GridViewRow Row in gvDocMaster.Rows)
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
    public void FillModule()
    {
        DataTable Dt = new DataTable();
        DataTable Dtapplication = objSys.GetSysParameterByParamName("Application_Id");

        Dt = objModule.GetModuleMaster();
        Dt = new DataView(Dt, "Application_Id="+Dtapplication.Rows[0]["Param_Value"].ToString()+"", "", DataViewRowState.CurrentRows).ToTable();
        if (Dt.Rows.Count > 0)
        {
            ddlModuleName.DataSource = Dt;
            ddlModuleName.DataTextField = "Module_Name";
            ddlModuleName.DataValueField = "Module_Id";
            ddlModuleName.DataBind();
        }
        else
        {
            Dt.Clear();
            ddlModuleName.DataSource = Dt;
            ddlModuleName.DataBind();
        }
        ddlModuleName.Items.Insert(0, "--Select--");
    }
    public void FillGrid()
    {
        DataTable dt = objDocNo.GetDocumentNumberAll(strCompId);
        if (dt.Rows.Count > 0)
        {
        }
        else
        {
            return;
        }

        dt.Columns.Add("Module_Name");
        dt.Columns.Add("Object_Name");
       
        //dt = new DataView(dt, "Brand_Id='" + Session["BrandId"].ToString() + "' and Location_Id='" + Session["LocId"].ToString() + "' ", "", DataViewRowState.CurrentRows).ToTable();
        for (int i = 0; i < dt.Rows.Count; i++)
        {
            DataTable Dtmodule = objModule.GetModuleMasterById(dt.Rows[i]["Module_Id"].ToString());
            DataTable dtObject = objectEntry.GetObjectMasterById(dt.Rows[i]["Object_Id"].ToString());
           
            try
            {
                dt.Rows[i]["Module_Name"] = Dtmodule.Rows[0]["Module_Name"].ToString();
                dt.Rows[i]["Object_Name"] = dtObject.Rows[0]["Object_Name"].ToString();
                
            }
            catch
            {

            }

        }
        gvDocMaster.DataSource = dt;
        gvDocMaster.DataBind();
        AllPageCode();
        Session["dtFilter"] = dt;
        Session["Doc"] = dt;

        lblTotalRecords.Text = Resources.Attendance.Total_Records + ": " + dt.Rows.Count.ToString() + "";

    }
 protected void gvDepMaster_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        gvDocMaster.PageIndex = e.NewPageIndex;

        DataTable dt = (DataTable)Session["dtFilter"];
        gvDocMaster.DataSource = dt;
        gvDocMaster.DataBind();
        AllPageCode();

    }
    protected void gvDepMaster_OnSorting(object sender, GridViewSortEventArgs e)
    {
        HDFSort.Value = HDFSort.Value == "ASC" ? "DESC" : "ASC";
        DataTable dt = new DataTable();
        dt = (DataTable)Session["dtFilter"];
        DataView dv = new DataView(dt);
        string Query = "" + e.SortExpression + " " + HDFSort.Value + "";
        dv.Sort = Query;
        dt = dv.ToTable();
        Session["dtFilter"] = dt;
        gvDocMaster.DataSource = dt;
        gvDocMaster.DataBind();
        AllPageCode();
    }
 protected void btnEdit_Command(object sender, CommandEventArgs e)
    {
        editid.Value = e.CommandArgument.ToString();
        DataTable dt = objDocNo.GetDocumentNumberAll(strCompId);
        dt = new DataView(dt, "Trans_Id=" + editid.Value + "", "", DataViewRowState.CurrentRows).ToTable();

        if (dt.Rows.Count > 0)
        {
            if (Convert.ToBoolean(dt.Rows[0]["IsUse"].ToString()))
            {
                DisplayMessage("Document number in use,can not be Update");
                return;
            }

            try
            {
                ddlModuleName.Enabled = false;
                ddlObjectName.Enabled = false;

                ddlModuleName.SelectedValue = dt.Rows[0]["Module_Id"].ToString();
                ddlModuleName_SelectedIndexChanged1(null, null);
                ddlObjectName.SelectedValue = dt.Rows[0]["Object_Id"].ToString();
                txtPrefixName.Text = dt.Rows[0]["Prefix"].ToString();
                txtSuffixName.Text = dt.Rows[0]["Suffix"].ToString();
                String CompanyId = dt.Rows[0]["CompId"].ToString();
                String BrandId = dt.Rows[0]["BrandId"].ToString();
                string LocationId = dt.Rows[0]["LocationId"].ToString();
                string DeptId = dt.Rows[0]["DeptId"].ToString();
                string EmpId = dt.Rows[0]["EmpId"].ToString();
                string Year = dt.Rows[0]["Year"].ToString();
                string Month = dt.Rows[0]["Month"].ToString();
                string Day = dt.Rows[0]["Day"].ToString();
                if (CompanyId.Trim() == "True")
                {
                    chkCompanyId.Checked = true;
                }
                else
                {
                    chkCompanyId.Checked = false;
                }
                if (BrandId.Trim() == "True")
                {
                    chkBrandId.Checked = true;
                }
                else
                {
                    chkBrandId.Checked = false;
                }
                if (LocationId.Trim() == "True")
                {
                    chkLocationId.Checked = true;
                }
                else
                {
                    chkLocationId.Checked = false;
                }
                if (DeptId.Trim() == "True")
                {
                    chkDepartmentId.Checked = true;

                }
                else
                {
                    chkDepartmentId.Checked = false;
                }
                if (EmpId.Trim() == "True")
                {
                    chkEmpId.Checked = true;

                }
                else
                {
                    chkEmpId.Checked = false;
                }
                if (Year.Trim() == "True")
                {
                    chkYear.Checked = true;
                }
                else
                {
                    chkYear.Checked = false;
                }
                if (Month.Trim() == "True")
                {
                    chkMonth.Checked = true;
                }
                else
                {
                    chkMonth.Checked = false;
                }
                if (Day.Trim() == "True")
                {
                    chkDay.Checked = true;

                }
                else
                {
                    chkDay.Checked = false;
                }

            }
            catch
            {

            }


            btnNew_Click(null, null);
            btnNew.Text = Resources.Attendance.Edit;

        }



    } protected void IbtnDelete_Command(object sender, CommandEventArgs e)
    {
        int b = 0;
        DataTable dt = objDocNo.GetDocumentNumberAll(strCompId);
        dt = new DataView(dt, "Trans_Id=" + e.CommandArgument.ToString() + "", "", DataViewRowState.CurrentRows).ToTable();
        if (dt.Rows.Count > 0)
        {

            if (Convert.ToBoolean(dt.Rows[0]["IsUse"].ToString()))
            {
                DisplayMessage("Document number in use,can not be delete");
                return;
            }
        }


        b = objDocNo.DeleteDocumentNumber(strCompId, e.CommandArgument.ToString(), "0", "0");
        if (b != 0)
        {
            DisplayMessage("Record Deleted");


            FillGrid();
            Reset();
        }
        else
        {
            DisplayMessage("Record Not Deleted");
        }
    }
    public void DisplayMessage(string str)
    {
        ScriptManager.RegisterStartupScript(this, GetType(), "", "alert('" + str + "');", true);
    }
    public void Reset()
    {
        ddlModuleName.Focus();
        ddlModuleName.SelectedIndex = 0;
        txtPrefixName.Text = "";
        txtSuffixName.Text = "";
        chkCompanyId.Checked = false;
        chkBrandId.Checked = false;
        chkLocationId.Checked = false;
        chkEmpId.Checked = false;
        chkMonth.Checked = false;
        chkDay.Checked = false;
        chkYear.Checked = false;
        chkDepartmentId.Checked = false;
        ddlObjectName.Items.Clear();
        ddlObjectName.Items.Insert(0, "--Select--");
        ddlObjectName.SelectedIndex = 0;

        btnNew.Text = Resources.Attendance.New;
        lblSelectedRecord.Text = "";
        ViewState["Select"] = null;
        editid.Value = "";
        txtValue.Text = "";
        ddlOption.SelectedIndex = 2;
        ddlFieldName.SelectedIndex = 0;
        ddlModuleName.Enabled = true;
        ddlObjectName.Enabled = true;
    }
    protected void btnList_Click(object sender, EventArgs e)
    {
        txtValue.Focus();
        pnlMenuList.BackColor = System.Drawing.ColorTranslator.FromHtml("#ccddee");

        pnlMenuNew.BackColor = System.Drawing.ColorTranslator.FromHtml("#90bde9");


        PnlList.Visible = true;
        PnlNewEdit.Visible = false;

    }
    protected void btnNew_Click(object sender, EventArgs e)
    {

        ddlModuleName.Focus();
        pnlMenuNew.BackColor = System.Drawing.ColorTranslator.FromHtml("#ccddee");

        pnlMenuList.BackColor = System.Drawing.ColorTranslator.FromHtml("#90bde9");


        PnlList.Visible = false;
        PnlNewEdit.Visible = true;

    }
    protected void btnRefresh_Click(object sender, ImageClickEventArgs e)
    {
        FillGrid();


        ddlOption.SelectedIndex = 2;
        ddlFieldName.SelectedIndex = 0;
        txtValue.Text = "";
        txtValue.Focus();
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
            DataTable dtCust = (DataTable)Session["Doc"];


            DataView view = new DataView(dtCust, condition, "", DataViewRowState.CurrentRows);
            Session["dtFilter"] = view.ToTable();
            lblTotalRecords.Text = Resources.Attendance.Total_Records + ": " + view.ToTable().Rows.Count.ToString() + "";

            gvDocMaster.DataSource = view.ToTable();
            gvDocMaster.DataBind();
            btnRefresh.Focus();

            AllPageCode();

        }
    }
    protected void btnSave_Click(object sender, EventArgs e)
    {
        int b = 0;

        if (ddlModuleName.SelectedIndex == 0)
        {
            DisplayMessage("Select Module");
            ddlModuleName.Focus();
            return;
        }
        if (ddlObjectName.SelectedIndex == 0)
        {
            DisplayMessage("Select Object");
            ddlObjectName.Focus();
            return;
        }
       if (editid.Value == "")
        {
            string Documentid = "0";
            DataTable dt = objDocNo.GetDocumentNumberAll(strCompId);

            dt = new DataView(dt, "Module_Id="+ddlModuleName.SelectedValue+" and Object_Id="+ddlObjectName.SelectedValue+"", "", DataViewRowState.CurrentRows).ToTable();
            if (dt.Rows.Count > 0)
            {
                DisplayMessage("Document number is generated for this module and object");
                Reset();
                return;
               
            }

            b = objDocNo.InsertDocumentNumber(strCompId, ddlModuleName.SelectedValue, ddlObjectName.SelectedValue, txtPrefixName.Text, txtSuffixName.Text, chkCompanyId.Checked.ToString(), chkBrandId.Checked.ToString(), chkLocationId.Checked.ToString(), chkDepartmentId.Checked.ToString(), chkEmpId.Checked.ToString(), chkYear.Checked.ToString(), chkMonth.Checked.ToString(), chkDay.Checked.ToString(), "False");

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
            b = objDocNo.UpdateDocumentNumber(strCompId, editid.Value, ddlModuleName.SelectedValue, ddlObjectName.SelectedValue, txtPrefixName.Text, txtSuffixName.Text, chkCompanyId.Checked.ToString(), chkBrandId.Checked.ToString(), chkLocationId.Checked.ToString(), chkDepartmentId.Checked.ToString(), chkEmpId.Checked.ToString(), chkYear.Checked.ToString(), chkMonth.Checked.ToString(), chkDay.Checked.ToString());

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
    protected void btnReset_Click(object sender, EventArgs e)
    {

        Reset();
    
        
    }
    protected void btnCancel_Click(object sender, EventArgs e)
    {
        FillGrid();

        Reset();
        btnList_Click(null, null);
    }
    protected void ddlModuleName_SelectedIndexChanged1(object sender, EventArgs e)
        {
        DataTable dtObject = new DataTable();
        if (ddlModuleName.SelectedIndex == 0)
        {
            DisplayMessage("Select Module");

            ddlModuleName.Focus();
            
            ddlObjectName.DataSource =null ;
            ddlObjectName.DataBind();
            ddlObjectName.Items.Insert(0, "--Select--");
            ddlObjectName.SelectedIndex = 0;
            return;
        }
        dtObject = cmn.GetObjectName();
        if (dtObject.Rows.Count > 0)
        {
            dtObject = new DataView(dtObject, "Module_Id=" + ddlModuleName.SelectedValue + "", "", DataViewRowState.CurrentRows).ToTable();
            if (dtObject.Rows.Count > 0)
            {
                ddlObjectName.DataSource = dtObject;
                ddlObjectName.DataTextField = "Object_Name";
                ddlObjectName.DataValueField = "Object_Id";
                ddlObjectName.DataBind();
                ddlObjectName.Items.Insert(0, "--Select--");
                ddlObjectName.SelectedIndex = 0;

            }
            else
            {


                ddlModuleName.Focus();

                ddlObjectName.Items.Clear();
                ddlObjectName.Items.Insert(0, "--Select--");
                ddlObjectName.SelectedIndex = 0;
                return;
            }


        }
        else
        {
            ddlObjectName.Items.Clear();
            ddlObjectName.Items.Insert(0, "--Select--");
            ddlObjectName.SelectedIndex = 0;
        }
    }
    public String SetobjectName(string ObjectId)
    {
        string ObjectName = string.Empty;
        DataTable Dt = objectEntry.GetObjectMasterById(ObjectId);
        ObjectName = Dt.Rows[0]["Object_Name"].ToString();
        return ObjectName;
    }
    public string setModuleName(string ModuleId)
    {
        string ModuleName = string.Empty;
        DataTable Dt = objModule.GetModuleMasterById(ModuleId);
        ModuleName = Dt.Rows[0]["Module_Name"].ToString();
        return ModuleName;
    }
}
