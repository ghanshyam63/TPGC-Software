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

public partial class Device_DeviceParameter : BasePage
{
    BrandMaster objBrand = new BrandMaster();
    DepartmentMaster DM = new DepartmentMaster();
    LocationMaster LM = new LocationMaster();
    Att_Device_Parameter objDevParam=new Att_Device_Parameter ();
    protected void Page_Load(object sender, EventArgs e)
    {
        if (Session["UserId"] == null)
        {
            Response.Redirect("~/ERPLogin.aspx");
        }
        Session["AccordianId"] = "14";
        Session["HeaderText"] = "Device Setup";
        if(!IsPostBack)
        {
            FillBrand(Session["CompId"].ToString(),ddlBrand);
            getServiceParam();
        }
    }

    public void getServiceParam()
    {

      
            hdSId.Value = "0";
            txtServiceInterval.Text = objDevParam.GetDeviceParameterValueByParamName("Service_Interval", Session["CompId"].ToString());
            txtServiceLogPath.Text = objDevParam.GetDeviceParameterValueByParamName("Service_Log_Path", Session["CompId"].ToString());
            chkNewUser.Checked = Convert.ToBoolean(objDevParam.GetDeviceParameterValueByParamName("Add_New_User", Session["CompId"].ToString()));

            chkNewUser_CheckedChanged(null, null);

           
            if (chkNewUser.Checked)
            {
                if (objDevParam.GetDeviceParameterValueByParamName("Default_Brand_Id", Session["CompId"].ToString()) != "0" || objDevParam.GetDeviceParameterValueByParamName("Default_Location_Id", Session["CompId"].ToString()) != "0" || objDevParam.GetDeviceParameterValueByParamName("Default_Department_Id", Session["CompId"].ToString()) != "0")
                {
                    chkDefault.Checked = false;


                    chkDefault_CheckedChanged(null, null);



                    if (ddlBrand.SelectedValue != objDevParam.GetDeviceParameterValueByParamName("Default_Brand_Id", Session["CompId"].ToString())) ;
                    {
                       
                        ddlBrand.SelectedValue = objDevParam.GetDeviceParameterValueByParamName("Default_Brand_Id", Session["CompId"].ToString());
                         ddlbrand_OnSelectedIndexChanged(null, null);
                    }
                    if (ddlLocation.SelectedValue != objDevParam.GetDeviceParameterValueByParamName("Default_Location_Id", Session["CompId"].ToString()))
                    {
                        ddlLocation.SelectedValue = objDevParam.GetDeviceParameterValueByParamName("Default_Location_Id", Session["CompId"].ToString());
                        ddllocation_OnSelectedIndexChanged(null, null);
                    }
                    if (ddlDepartment.SelectedValue != objDevParam.GetDeviceParameterValueByParamName("Default_Department_Id", Session["CompId"].ToString()))
                    {
                        ddlDepartment.SelectedValue = objDevParam.GetDeviceParameterValueByParamName("Default_Department_Id", Session["CompId"].ToString());
                    }
                }
                else
                {
                    chkDefault.Checked = true;
                    chkDefault_CheckedChanged(null, null);
                }
            }





       

    }
   protected void chkNewUser_CheckedChanged(object sender, EventArgs e)
    {
        if(chkNewUser.Checked)
        {
            trdfval.Visible = true;
            pnlAddNewUser.Visible = true;
            chkDefault.Checked = true;

            chkDefault_CheckedChanged(null,null);
            
        }
        else
        {
            trdfval.Visible = false;
            pnlAddNewUser.Visible = false;
            
            chkDefault.Checked = true;
            chkDefault_CheckedChanged(null, null);
        }
    }
    protected void chkDefault_CheckedChanged(object sender, EventArgs e)
    {
        if (chkDefault.Checked)
        {
            trDefault.Visible = false;
            FillBrand(Session["CompId"].ToString(),ddlBrand);
            ddlbrand_OnSelectedIndexChanged(null, null);
          

        }
        else
        {

            trDefault.Visible = true;
        }

    }



    
    protected void BtnUpdate_Click(object sender, EventArgs e)
    {
        int b = 0;
        string compid = string.Empty;
        string brandid = string.Empty;
        string locid = string.Empty;
        string deptid = string.Empty;
        if (chkNewUser.Checked == true && chkDefault.Checked == false)
        {
           
            brandid = ddlBrand.SelectedValue;
            locid = ddlLocation.SelectedValue;
            deptid = ddlDepartment.SelectedValue;
            compid = Session["CompId"].ToString();
        }
        else
        {
            compid = Session["CompId"].ToString();
            brandid = Session["BrandId"].ToString();
            locid = Session["LocId"].ToString();
            deptid = "0";

        }


      
 b=objDevParam.UpdateDeviceParameterMaster(Session["CompId"].ToString(),"Service_Interval",txtServiceInterval.Text,true.ToString(),Session["UserId"].ToString(),DateTime.Now.ToString());
 b=objDevParam.UpdateDeviceParameterMaster(Session["CompId"].ToString(),"Service_Log_Path",txtServiceLogPath.Text,true.ToString(),Session["UserId"].ToString(),DateTime.Now.ToString());
 b=objDevParam.UpdateDeviceParameterMaster(Session["CompId"].ToString(),"Add_New_User",chkNewUser.Checked.ToString(),true.ToString(),Session["UserId"].ToString(),DateTime.Now.ToString());
 b=objDevParam.UpdateDeviceParameterMaster(Session["CompId"].ToString(),"Default_Company_Id",compid,true.ToString(),Session["UserId"].ToString(),DateTime.Now.ToString());
 b=objDevParam.UpdateDeviceParameterMaster(Session["CompId"].ToString(),"Default_Brand_Id",brandid,true.ToString(),Session["UserId"].ToString(),DateTime.Now.ToString());
 b=objDevParam.UpdateDeviceParameterMaster(Session["CompId"].ToString(),"Default_Location_Id",locid,true.ToString(),Session["UserId"].ToString(),DateTime.Now.ToString());

 b=objDevParam.UpdateDeviceParameterMaster(Session["CompId"].ToString(),"Default_Department_Id",deptid,true.ToString(),Session["UserId"].ToString(),DateTime.Now.ToString());
    

        if (b!=0)
        {
            DisplayMessage("Record Updated");
        }
        else
        {
            DisplayMessage("Record Not Updated");
        }
    }
    public void DisplayMessage(string str)
    {
        ScriptManager.RegisterStartupScript(this, GetType(), "", "alert('" + str + "');", true);
    }
    protected void ddlbrand_OnSelectedIndexChanged(object sender, EventArgs e)
    {
        filllocation(ddlBrand.SelectedValue, ddlLocation);
        

    }

    public void FillBrand(string CompanyId, DropDownList ddl)
    {

        DataTable dt = objBrand.GetBrandMaster(Session["CompId"].ToString());
        if (dt.Rows.Count > 0)
        {
            ddl.DataSource = dt;
            ddl.DataTextField = "Brand_Name";
            ddl.DataValueField = "Brand_Id";
            ddl.DataBind();
            ddl.Items.Insert(0, "--Select One--");
            ddl.SelectedIndex = 0;

        }
        else
        {

        }


    }

    protected void ddllocation_OnSelectedIndexChanged(object sender, EventArgs e)
    {
        fillDepartMent(ddlLocation.SelectedValue, ddlDepartment);

    }

    public void fillDepartMent(string LocationId, DropDownList ddl)
    {
        string DeptId = string.Empty;

        DataTable dt = DM.GetDepartmentMaster(Session["CompId"].ToString());
       


        try
        {
            DataView dv = new DataView(dt);

            dv.RowFilter = "Location_Id ='" + LocationId + "'";
            dt = dv.ToTable();
           



            ddl.DataSource = dt;
            ddl.DataTextField = "Dep_Name";
            ddl.DataValueField = "Dep_Id";
            ddl.DataBind();
            ddl.Items.Insert(0, "--Select One--");
            ddl.SelectedIndex = 0;
        }
        catch
        { }



    }


    public void filllocation(string BrandId, DropDownList ddl)
    {
        DataTable dt = LM.GetLocationMaster(Session["CompId"].ToString());
        string Loc = string.Empty;

        



        try
        {
            DataView dv = new DataView(dt);

            dv.RowFilter = "Brand_Id ='" + BrandId + "'";
            dt = dv.ToTable();

            if (Loc.Length > 0)
            {
                dt = new DataView(dt, "Location_Id in (" + Loc.Substring(0, Loc.Length - 1) + ")", "Location_Name", DataViewRowState.CurrentRows).ToTable();
            }
            ddl.DataSource = dt;
            ddl.DataTextField = "Location_Name";
            ddl.DataValueField = "Location_Id";
            ddl.DataBind();
            ddl.Items.Insert(0, "--Select One--");
            ddl.SelectedIndex = 0;
        }
        catch
        { }



    }
   

}
