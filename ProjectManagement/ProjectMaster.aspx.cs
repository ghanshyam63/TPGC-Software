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
using System.Text;

public partial class ProjectManagement_ProjectMaster : System.Web.UI.Page
{

   
    #region Defined Class Object
    Common cmn = new Common();
    Prj_ProjectMaster objProjctMaster = new Prj_ProjectMaster();
    SystemParameter ObjSysParam = new SystemParameter();
    Set_CustomerMaster objCustomermaster = new Set_CustomerMaster();
    Ems_ContactMaster objContactmaster = new Ems_ContactMaster();
    Arc_Directory_Master objDirectorymaster = new Arc_Directory_Master();
    Arc_FileTransaction objFiletransection = new Arc_FileTransaction();

    string StrCompId = string.Empty;
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

            BindGrid();
            ddlOption.SelectedIndex = 2;
           
           

        }
      AllPageCode();

    }

   

    public void AllPageCode()
    {
        Page.Title = ObjSysParam.GetSysTitle();
        StrUserId = Session["UserId"].ToString();
        StrCompId = Session["CompId"].ToString();
        //Session["AccordianId"] = "19";
       // Session["HeaderText"] = "HR";
        DataTable dtAllPageCode = cmn.GetAllPagePermission(StrUserId.ToString(), "19", "65");
        if (dtAllPageCode.Rows.Count != 0)
        {
            if (dtAllPageCode.Rows[0][0].ToString() == "SuperAdmin")
            {
               // btnCSave.Visible = true;
               // foreach (GridViewRow Row in GvAllowance.Rows)
                //{
                  //  ((ImageButton)Row.FindControl("btnEdit")).Visible = true;
                  //  ((ImageButton)Row.FindControl("IbtnDelete")).Visible = true;
              //  }
               // imgBtnRestore.Visible = true;
               // ImgbtnSelectAll.Visible = true;
            }
            else
            {
                foreach (DataRow DtRow in dtAllPageCode.Rows)
                {
                    if (Convert.ToBoolean(DtRow["Op_Add"].ToString()))
                    {
                       // btnCSave.Visible = true;
                    }
                    //foreach (GridViewRow Row in GvAllowance.Rows)
                    //{
                    //    if (Convert.ToBoolean(DtRow["Op_Edit"].ToString()))
                    //    {
                    //        ((ImageButton)Row.FindControl("btnEdit")).Visible = true;
                    //    }
                    //    if (Convert.ToBoolean(DtRow["Op_Delete"].ToString()))
                    //    {
                    //        ((ImageButton)Row.FindControl("IbtnDelete")).Visible = true;
                    //    }
                    //}
                    if (Convert.ToBoolean(DtRow["Op_Restore"].ToString()))
                    {
                       // imgBtnRestore.Visible = true;
                       // ImgbtnSelectAll.Visible = true;
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

    public void BindGrid()
    {
        DataTable dtProjectMAster = objProjctMaster.GetAllRecordProjectMasteer();
       
        if (dtProjectMAster.Rows.Count > 0)
        {

            GvrProjectteam.DataSource = dtProjectMAster;
            GvrProjectteam.DataBind();
            Session["dtFilter"] = dtProjectMAster;
            Session["dtProjectmaster"] = dtProjectMAster;
        }
        else
        {
            DataTable Dtclear = new DataTable();
            GvrProjectteam.DataSource = Dtclear;
            GvrProjectteam.DataBind();
        }
        lblTotalRecords.Text = Resources.Attendance.Total_Records + ": " + dtProjectMAster.Rows.Count + "";
        AllPageCode();
    
    
    
    }
    
    public void Reset()
    {

        txtprojectname.Text = "";
        txtprojectlocalname.Text = "";
        txtcustomername.Text = "";
        txtprojecttype.Text = "";
        txtstartdate.Text = "";
        txtexpenddate.Text = "";
        txtenddate.Text = "";
        txtprojecttitle.Text = "";
        
        txtfilename.Text = "";
        Editor1.Content = "";
        System.Web.UI.ScriptManager.GetCurrent(this).SetFocus(txtprojectname);
       
    }
    protected void GvrProjectteam_Sorting(object sender, GridViewSortEventArgs e)
    {
        DataTable dt = (DataTable)Session["dtFilter"];
        string sortdir = "DESC";
        if (ViewState["SortDir"] != null)
        {
            sortdir = ViewState["SortDir"].ToString();
            if (sortdir == "ASC")
            {
                e.SortDirection = SortDirection.Descending;
                ViewState["SortDir"] = "DESC";

            }
            else
            {
                e.SortDirection = SortDirection.Ascending;
                ViewState["SortDir"] = "ASC";
            }
        }
        else
        {
            ViewState["SortDir"] = "DESC";
        }

        dt = (new DataView(dt, "", e.SortExpression + " " + ViewState["SortDir"].ToString(), DataViewRowState.CurrentRows)).ToTable();
        Session["dtFilter"] = dt;
        GvrProjectteam.DataSource = dt;
        GvrProjectteam.DataBind();

        AllPageCode();
    }

    protected void GvrProjectteam_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        GvrProjectteam.PageIndex = e.NewPageIndex;
        DataTable dt = (DataTable)Session["dtFilter"];
        GvrProjectteam.DataSource = dt;
        GvrProjectteam.DataBind();
        AllPageCode();
    }

    protected void btnRefresh_Click(object sender, ImageClickEventArgs e)
    {
       
        ddlFieldName.SelectedIndex = 1;
        ddlOption.SelectedIndex = 1;
        txtValue.Text = "";
        BindGrid();
    }
      
 
    protected void btnbindrpt_Click(object sender, ImageClickEventArgs e)
    {
        if (ddlOption.SelectedIndex != 0)
        {
            string condition = string.Empty;
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
            DataTable dtProjectteam = (DataTable)Session["dtProjectmaster"];

            DataView view = new DataView(dtProjectteam, condition, "", DataViewRowState.CurrentRows);
            GvrProjectteam.DataSource = view.ToTable();
            Session["dtFilter"] = view.ToTable();
            lblTotalRecords.Text = Resources.Attendance.Total_Records + " : " + view.ToTable().Rows.Count + "";
            GvrProjectteam.DataBind();
             AllPageCode();
        }
    }
    
    public void btnsave_Click(object sender, EventArgs e)
    {
        if (txtprojectname.Text == "")
        {
            DisplayMessage("Enter Project Name");
            System.Web.UI.ScriptManager.GetCurrent(this).SetFocus(txtprojectname);
            return;
        
        }
        if (txtcustomername.Text == "")
        {
            DisplayMessage("Enter Customer Name");
            System.Web.UI.ScriptManager.GetCurrent(this).SetFocus(txtcustomername);
            return;

        }
        if (txtprojecttype.Text == "")
        {
            DisplayMessage("Enter Project Type Name");
            System.Web.UI.ScriptManager.GetCurrent(this).SetFocus(txtprojecttype);
            return;

        }
        if (txtstartdate.Text == "")
        {
            DisplayMessage("Enter Start Date Name");
            System.Web.UI.ScriptManager.GetCurrent(this).SetFocus(txtstartdate);
            return;

        }
        if (txtexpenddate.Text == "")
        {
            DisplayMessage("Enter Expected End Date Name");
            System.Web.UI.ScriptManager.GetCurrent(this).SetFocus(txtexpenddate);
            return;

        }
        if (txtenddate.Text == "")
        {
            DisplayMessage("Enter End Date Name");
            System.Web.UI.ScriptManager.GetCurrent(this).SetFocus(txtenddate);
            return;
        }
        if (txtprojecttitle.Text == "")
        {
            DisplayMessage("Enter Project Title Name");
            System.Web.UI.ScriptManager.GetCurrent(this).SetFocus(txtprojecttitle);
            return;
        }

        try
        {
            Convert.ToDateTime(txtstartdate.Text);
        }
        catch (Exception)
        {

            DisplayMessage("Start Date Not In Proper Format");

            return;
        }

        try
        {
            Convert.ToDateTime(txtexpenddate.Text);
        }
        catch (Exception)
        {

            DisplayMessage("Expected End Date Not In Proper Format");

            return;
        }

        //if (Convert.ToDateTime(txtstartdate.Text) <= Convert.ToDateTime(txtenddate.Text) || Convert.ToDateTime(txtstartdate.Text) <= Convert.ToDateTime(txtexpenddate.Text))
        //{
        //    DisplayMessage("End Date Can Not Be Greater Than Start Date");
        //    return;

        //}


        hdnfileid.Value = "0";
        hdnfileid.Value = ddlFilename.SelectedValue;

        int a = 0;
        a = objProjctMaster.InsertProjectMaster(txtprojectname.Text, txtprojectlocalname.Text, txtprojecttype.Text, HidCustId.Value, txtstartdate.Text, txtexpenddate.Text, txtenddate.Text, txtprojecttitle.Text,Editor1.Content,hdnfileid.Value, "", "", "", "", "", "", "", true.ToString(), Session["UserId"].ToString(), DateTime.Now.ToString(), Session["UserId"].ToString(), DateTime.Now.ToString());
           DisplayMessage("Rcord Save Successfully");
           Reset();
    }

    public void btncencelpnl_Click(object sender, EventArgs E)
    {
        pnlpopup.Visible = false;

        pnlprojectrecord.Visible = true;
    
    }

    public void btnfiledwnload_Click(object sender, EventArgs e)
    {
        pnlpopup.Visible = true;
        pnl2.Visible = true;
       
     
        pnlprojectrecord.Visible = false;
        pnllist.Visible = false;
        DataTable dtDirectry = objDirectorymaster.getDirectoryMaster(Session["CompId"].ToString(), "0");

        if (dtDirectry.Rows.Count > 0)
        {

            ddlDirctoryname.DataSource = dtDirectry;
            ddlDirctoryname.DataTextField = "Directory_Name";
            ddlDirctoryname.DataValueField = "Id";
            ddlDirctoryname.DataBind();

            ddlDirctoryname.Items.Add("--Select--");
            ddlDirctoryname.SelectedValue = "--Select--";
        }
        else
        {
            ddlDirctoryname.Items.Add("--Select--");
            ddlDirctoryname.SelectedValue = "--Select--";
        }




    }


    public void btnsubmitpopup_Click(object sender, EventArgs e)
    {
        pnlpopup.Visible = false;
        pnlprojectrecord.Visible = true;
        txtfilename.Text = ddlFilename.SelectedItem.Text;


    }
    public void btncencel_Click(object sender, EventArgs e)
    {
        Reset();
    }
    public void btnreset_Click(object sender, EventArgs e)
    {
        Reset();
    }

    protected void txtcustomername_TextChanged(object sender, EventArgs e)
    {
        
      
        string custid = string.Empty;
        if (txtcustomername.Text != "")
        {
            custid = txtcustomername.Text.Split('/')[txtcustomername.Text.Split('/').Length - 1];



            DataTable dtContactmaster = objContactmaster.GetAllCustomerName(Session["CompId"].ToString());


            dtContactmaster = new DataView(dtContactmaster, "Contact_Id='" + custid + "'", "", DataViewRowState.CurrentRows).ToTable();

            if (dtContactmaster.Rows.Count > 0)
            {
                custid = dtContactmaster.Rows[0]["Contact_Id"].ToString();
                HidCustId.Value = custid;
            }
            else
            {
                DisplayMessage("Customer Not Exists");
                txtcustomername.Text = "";
                txtcustomername.Focus();
                HidCustId.Value = "";
                return;
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
  
    protected void ddlDirctoryname_SelectedIndexChanged(object sender, EventArgs e)
    {
       
        if (ddlDirctoryname.SelectedValue==ddlDirctoryname.SelectedValue)
        {


            DataTable dtFilename=objFiletransection.Get_FileTransaction_By_Documentid(Session["CompId"].ToString(),"0",ddlDirctoryname.SelectedValue.ToString());

            if (dtFilename.Rows.Count > 0)
            {

                ddlFilename.DataSource = dtFilename;
                ddlFilename.DataTextField = "File_Name";
                ddlFilename.DataValueField = "Trans_Id";
                ddlFilename.DataBind();
               
                ddlFilename.Items.Add("--Select--");
                ddlFilename.SelectedValue = "--Select--";
            }
            else
            {
                ddlFilename.Items.Add("--Select--");
                ddlFilename.SelectedValue = "--Select--";
            }

        }



    }


    protected void btList_Click(object sender, EventArgs e)
    {
        BindGrid();
        pnllist.Visible = true;
        pnlprojectrecord.Visible = false;
        PanelList.BackColor = System.Drawing.ColorTranslator.FromHtml(" #ccddee");
        pnlnew.BackColor = System.Drawing.ColorTranslator.FromHtml("#90bde9");
    }
    
    protected void btnnew_Click(object sender, EventArgs e)
    {
        pnlprojectrecord.Visible = true;
        pnllist.Visible = false;
        PanelList.BackColor = System.Drawing.ColorTranslator.FromHtml("#90bde9");
        pnlnew.BackColor = System.Drawing.ColorTranslator.FromHtml("#ccddee");

    }
    protected void Imgbtn_Click(object sender, ImageClickEventArgs e)
    {
        pnlpopup.Visible = false;
        pnlprojectrecord.Visible = true;
    }
}
