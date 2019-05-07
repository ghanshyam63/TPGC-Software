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

public partial class MasterSetUp_Home : System.Web.UI.Page
{
    CompanyMaster ObjCompanyMaster = new CompanyMaster();
    BrandMaster ObjBrandMaster = new BrandMaster();
    LocationMaster ObjLocationMaster = new LocationMaster();
    UserMaster objUserMaster = new UserMaster();
    SystemParameter objSys = new SystemParameter();
    RoleDataPermission objRoleData = new RoleDataPermission();
    RoleMaster objRole = new RoleMaster();
    protected void Page_Load(object sender, EventArgs e)
    {
        Session["AccordianId"] = "8";
        SystemParameter objSys = new SystemParameter();
        Page.Title = objSys.GetSysTitle();
        if (!IsPostBack)
        {
            pnl1.Visible = true;
            pnl2.Visible = true;

            fillDropdown(ddlCompany, fillCompanybyUser(), "Company_Name", "Company_Id");

            try
            {
                ddlCompany.SelectedIndex = 0;
            }
            catch
            {

            }
            ddlCompany_SelectedIndexChanged(null, null);
        }
        else
        {
            try
            {
                Label lblcomp = (Label)Master.FindControl("lblCompany1");
                Label lblBrand = (Label)Master.FindControl("lblBrand1");
                Label lblLocation = (Label)Master.FindControl("lblLocation1");
                Label lblLanguage1 = (Label)Master.FindControl("lblLanguage");

                lblcomp.Text = ddlCompany.SelectedItem.Text;
                lblBrand.Text = ddlBrand.SelectedItem.Text;
                lblLocation.Text = ddlLocation.SelectedItem.Text;
                lblLanguage1.Text = ddlLanguage.SelectedItem.Text;
                Session["CompName"] = lblcomp.Text;
                Session["LocName"] = lblLocation.Text;
                Session["BrandName"] = lblBrand.Text;
                Session["Language"] = lblLanguage1.Text;
            }
            catch
            {
                //Session.Abandon();
                //Session.Clear();
                //Response.Redirect("~/ERPLogin.aspx");
            }
        }
    }
    protected void ddlLanguage_SelectedIndexChanged(object sender, EventArgs e)
    {

        Session["Language"] = ddlLanguage.SelectedItem.Text;
        Session["lang"] = ddlLanguage.SelectedValue;
    }
    public DataTable fillCompanybyUser()
    {
        DataTable dt = objUserMaster.GetUserMasterByUserId(Session["UserId"].ToString());
        DataTable dtReturn = ObjCompanyMaster.GetCompanyMaster();
        if (dt.Rows.Count != 0)
        {
            if (dt.Rows[0]["Company_Id"].ToString() != "0" && dt.Rows[0]["Emp_Id"].ToString() != "0")
            {
                dtReturn = new DataView(dtReturn, "Company_Id in('" + dt.Rows[0]["Company_Id"].ToString() + "')", "", DataViewRowState.CurrentRows).ToTable();
            }
        }
        return dtReturn;
    }
    public bool GetStatus(string RoleId)
    {
        bool status = false;
        DataTable dtRole = objRole.GetRoleMaster();
        dtRole = new DataView(dtRole, "Role_Id='" + RoleId + "'", "", DataViewRowState.CurrentRows).ToTable();

        if (dtRole.Rows.Count > 0)
        {
            string str = dtRole.Rows[0]["Role_Name"].ToString().Trim().ToLower();
            if (str == "super admin")
            {
                status = true;
            }
        }
        return status;
    }
    protected void ddlCompany_SelectedIndexChanged(object sender, EventArgs e)
    {


        Session["CompId"] = ddlCompany.SelectedValue.ToString();

        DataTable dtBrand = ObjBrandMaster.GetBrandMaster(Session["CompId"].ToString());

        string BrandIds = GetRoleDataPermission(Session["RoleId"].ToString(), "B");

        if (!GetStatus(Session["RoleId"].ToString()))
        {
            if (BrandIds != "")
            {
                dtBrand = new DataView(dtBrand, "Brand_Id in(" + BrandIds.Substring(0, BrandIds.Length - 1) + ")", "", DataViewRowState.CurrentRows).ToTable();
            }


        }


        fillDropdown(ddlBrand, dtBrand, "Brand_Name", "Brand_Id");

        ddlBrand_SelectedIndexChanged(null, null);



    }

    public string GetRoleDataPermission(string RoleId, string RecordType)
    {
        string IDs = string.Empty;
        DataTable dtRoleData = objRoleData.GetRoleDataPermissionById(RoleId);

        if (dtRoleData.Rows.Count > 0)
        {
            dtRoleData = new DataView(dtRoleData, "Record_Type='" + RecordType + "'", "", DataViewRowState.CurrentRows).ToTable();
            if (dtRoleData.Rows.Count > 0)
            {
                for (int i = 0; i < dtRoleData.Rows.Count; i++)
                {
                    IDs += dtRoleData.Rows[i]["Record_Id"].ToString() + ",";

                }

            }

        }
        return IDs;

    }
    protected void ddlBrand_SelectedIndexChanged(object sender, EventArgs e)
    {

        Session["BrandId"] = ddlBrand.SelectedValue.ToString();

        DataTable dtLoc = ObjLocationMaster.GetLocationMaster(Session["CompId"].ToString());
        dtLoc = new DataView(dtLoc, "Brand_Id='" + ddlBrand.SelectedValue + "'", "", DataViewRowState.CurrentRows).ToTable();


        string LocIds = GetRoleDataPermission(Session["RoleId"].ToString(), "L");

        if (!GetStatus(Session["RoleId"].ToString()))
        {
            if (LocIds != "")
            {
                dtLoc = new DataView(dtLoc, "Location_Id in(" + LocIds.Substring(0, LocIds.Length - 1) + ")", "", DataViewRowState.CurrentRows).ToTable();
            }


        }





        fillDropdown(ddlLocation, dtLoc, "Location_Name", "Location_Id");

        ddlLocation_SelectedIndexChanged(null, null);



    }
    protected void ddlLocation_SelectedIndexChanged(object sender, EventArgs e)
    {

        Session["LocId"] = ddlLocation.SelectedValue.ToString();


        string DeparmentIds = GetRoleDataPermission(Session["RoleId"].ToString(), "D");


        if (!GetStatus(Session["RoleId"].ToString()))
        {
            if (DeparmentIds != "")
            {
                Session["SessionDepId"] = DeparmentIds;
            }

        }


    }
    public void fillDropdown(DropDownList ddl, DataTable dt, string DataTextField, string DataValueField)
    {
        ddl.DataSource = dt;
        ddl.DataTextField = DataTextField;
        ddl.DataValueField = DataValueField;
        ddl.DataBind();

    }



    protected void btnSave_Click(object sender, EventArgs e)
    {

        Session["CompId"] = ddlCompany.SelectedValue.ToString();
        Session["LocId"] = ddlLocation.SelectedValue.ToString();
        Session["BrandId"] = ddlBrand.SelectedValue.ToString();

        pnl1.Visible = false;
        pnl2.Visible = false;

        try
        {
            Label lblcomp = (Label)Master.FindControl("lblCompany1");
            Label lblBrand = (Label)Master.FindControl("lblBrand1");
            Label lblLocation = (Label)Master.FindControl("lblLocation1");
            Label lblLanguage1 = (Label)Master.FindControl("lblLanguage");

            lblcomp.Text = ddlCompany.SelectedItem.Text;
            try
            {
                lblBrand.Text = ddlBrand.SelectedItem.Text;
            }
            catch
            {
                lblBrand.Text = "";
            }
            try
            {
                lblLocation.Text = ddlLocation.SelectedItem.Text;
            }
            catch
            {
                lblLocation.Text = "";
            }
            lblLanguage1.Text = ddlLanguage.SelectedItem.Text;
            Session["CompName"] = lblcomp.Text;
            Session["LocName"] = lblLocation.Text;
            Session["BrandName"] = lblBrand.Text;
            Session["Language"] = lblLanguage1.Text;
        }
        catch
        {


        }

    }



}
