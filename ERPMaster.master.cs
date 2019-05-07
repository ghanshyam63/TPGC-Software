using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;

public partial class ERPMaster : System.Web.UI.MasterPage
{
    Common ObjCom = new Common();
    CompanyMaster ObjCompanyMaster = new CompanyMaster();
    BrandMaster ObjBrandMaster = new BrandMaster();
    LocationMaster ObjLocationMaster = new LocationMaster();
    UserMaster objUserMaster = new UserMaster();
    ModuleMaster ObjModuleMaster = new ModuleMaster();
    SystemParameter ObjSysPeram = new SystemParameter();
    protected void Page_Load(object sender, EventArgs e)
    {

        if (!IsPostBack)
        {
            Page.Title = ObjSysPeram.GetSysTitle();




            // lblModuleIs.Text = "PegasusERP :: " + Session["HeaderText"].ToString();
            lnkUserName.Text = Session["UserId"].ToString();
            setHeaderImage();
            label4.Text = "Version " + ObjSysPeram.GetSysParameterByParamName("Application_Version").Rows[0]["Param_Value"].ToString();
            if (Session["lang"] == null)
            {
                Session["lang"] = "1";
                Session["CompId"] = Session["CompId"].ToString();

                body1.Style[HtmlTextWriterStyle.Direction] = "ltr";
            }
            else if (Session["lang"].ToString() == "2")
            {

                body1.Style[HtmlTextWriterStyle.Direction] = "rtl";
            }
            else if (Session["lang"].ToString() == "1")
            {

                body1.Style[HtmlTextWriterStyle.Direction] = "ltr";
            }
            BasePage bs = new BasePage();


       

            try
            {
                lblCompany1.Text = Session["CompName"].ToString();
                lblLocation1.Text = Session["LocName"].ToString();
                lblBrand1.Text = Session["BrandName"].ToString();
                lblLanguage.Text = Session["Language"].ToString();

            }
            catch
            {

            }
        }
        PopulateAcrDynamically();
    }

    private void setHeaderImage()
    {
        string imagepath = "";
        if (Session["AccordianId"] != null)
        {
            string Id = Session["AccordianId"].ToString();
            if (Id == "0")
            {
                imagepath = "<img alt=Pegasus :: Project Management src=../Images/time_attendance_banner.png complete=complete width=100%/>";
                LitImage.Text = imagepath;
            }
            else if (Id != "0")
            {
                DataTable dt1 = ObjModuleMaster.GetModuleMasterById(Id);
                imagepath = dt1.Rows[0]["Module_Banner"].ToString();
                LitImage.Text = imagepath;
            }
        }
    }

    public DataTable fillCompanybyUser()
    {
        DataTable dt = objUserMaster.GetUserMasterByUserId(Session["UserId"].ToString());
        DataTable dtReturn = ObjCompanyMaster.GetCompanyMaster();
        if (dt.Rows.Count != 0)
        {
            if (dt.Rows[0]["Company_Id"].ToString() != "0" && dt.Rows[0]["Emp_Id"].ToString() != "0")
            {
                dtReturn = new DataView(dtReturn, "Company_Id='" + dt.Rows[0]["Company_Id"].ToString() + "'", "", DataViewRowState.CurrentRows).ToTable();
            }

        }
        return dtReturn;
    }
    public void PopulateAcrDynamically()
    {


        HyperLink lbTitle;
        Literal litdd;
        AjaxControlToolkit.AccordionPane pn;
        int i = 0;
        int flag = 0;

        DataTable dtAllModule = ObjCom.GetAccodion(Session["UserId"].ToString()).DefaultView.ToTable(true, "Module_Id");

        foreach (DataRow datarow in dtAllModule.Rows)
        {
            DataTable dtM = new DataView(ObjCom.GetModuleName(), "Module_Id='" + datarow["Module_Id"].ToString() + "'", "", DataViewRowState.CurrentRows).ToTable();
            lbTitle = new HyperLink();
            litdd = new Literal();
            string strText = string.Empty;
            if (Session["lang"].ToString() == "1")
            {
                lbTitle.CssClass = "FrmTitle";
                lbTitle.Text = dtM.Rows[0]["Module_Name"].ToString();

                lbTitle.NavigateUrl = dtM.Rows[0]["DashBoard_Url"].ToString();
            }
            else
            {
                lbTitle.Text = dtM.Rows[0]["Module_Name_L"].ToString();
                lbTitle.NavigateUrl = dtM.Rows[0]["DashBoard_Url"].ToString();
            }

            DataTable dtAllChild = new DataView(ObjCom.GetAccodion(Session["UserId"].ToString()), "Module_Id=" + datarow["Module_Id"].ToString() + "", "Order_Appear", DataViewRowState.CurrentRows).ToTable();


            strText = "<table>";

            foreach (DataRow childrow in dtAllChild.Rows)
            {
                DataTable dtObject = new DataView(ObjCom.GetObjectName(), "Object_Id='" + childrow["Object_Id"].ToString() + "'", "", DataViewRowState.CurrentRows).ToTable();

                if (Session["AccordianId"] != null)
                {
                    string strAccordianId = Session["AccordianId"].ToString();
                    if (strAccordianId != null && strAccordianId != "0")
                    {
                        if (datarow["Module_Id"].ToString().Trim() == strAccordianId)
                        {
                            flag = 1;

                        }
                    }
                }


                if (Session["lang"].ToString() == "1")
                {
                    strText = strText + "<tr><td align='left'><a href=" + dtObject.Rows[0]["Page_Url"].ToString() + " class=acc>" + dtObject.Rows[0]["Object_Name"].ToString() + "</a></tr></td>";
                }
                else
                {
                    strText = strText + "<tr><td align='left'><a href=" + dtObject.Rows[0]["Page_Url"].ToString() + " class=acc>" + dtObject.Rows[0]["Object_Name_L"].ToString() + "</a></tr></td>";
                }
            }
            strText = strText + "</table>";
            litdd.Text = strText;
            pn = new AjaxControlToolkit.AccordionPane();
            pn.ID = "Pane" + i;
            pn.HeaderContainer.Controls.Add(lbTitle);
            if (flag == 1)
            {
                flag = 0;
                acrDynamic.SelectedIndex = i;
            }
            pn.ContentContainer.Controls.Add(litdd);
            acrDynamic.Panes.Add(pn);
            ++i;
        }



    }

    public void fillDropdown(DropDownList ddl, DataTable dt, string DataTextField, string DataValueField)
    {
        ddl.DataSource = dt;
        ddl.DataTextField = DataTextField;
        ddl.DataValueField = DataValueField;
        ddl.DataBind();

    }
    


    protected void Bld_onClick(object sender, EventArgs e)
    {
       

      
        Response.Redirect("~/MasterSetup/Home.aspx");

    }

    protected void btnlogout_Click(object sender, ImageClickEventArgs e)
    {
        Session.Abandon();
        Response.Redirect("~/ERPLogin.aspx");
    }
}
