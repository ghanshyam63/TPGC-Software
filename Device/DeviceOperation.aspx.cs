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
using Device_SDK;

public partial class Device_DeviceOperation : BasePage
{
    Att_DeviceMaster objDevice = new Att_DeviceMaster();
    Common cmn = new Common();
    SystemParameter objSys = new SystemParameter();
    Device_Operation_Lan objDeviceOp = new Device_Operation_Lan();
    protected void Page_Load(object sender, EventArgs e)
    {
        if (Session["UserId"] == null)
        {
            Response.Redirect("~/ERPLogin.aspx");
        }
        Page.Title = objSys.GetSysTitle();
        if (!IsPostBack)
        {
          

           
            FillGrid();
            pnlDeviceOp.Visible = false;


        }

        AllPageCode();
    }

    public void AllPageCode()
    {
       
        Session["AccordianId"] = "14";
        Session["HeaderText"] = "Device Setup";
        DataTable dtAllPageCode = cmn.GetAllPagePermission(Session["UserId"].ToString(), "14", "76");
        if (dtAllPageCode.Rows.Count != 0)
        {
            if (dtAllPageCode.Rows[0][0].ToString() == "SuperAdmin")
            {
                
            }
            else
            {
                foreach (DataRow DtRow in dtAllPageCode.Rows)
                {
                    if (Convert.ToBoolean(DtRow["Op_Add"].ToString()))
                    {
                        
                    }
                    foreach (GridViewRow Row in gvDevice.Rows)
                    {
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
    public void FillGrid()
    {
        DataTable dt = objDevice.GetDeviceMaster(Session["CompId"].ToString());
        dt = new DataView(dt, "Brand_Id='" + Session["BrandId"].ToString() + "' and Location_Id='" + Session["LocId"].ToString() + "'", "", DataViewRowState.CurrentRows).ToTable();

        gvDevice.DataSource = dt;
        gvDevice.DataBind();
      
        Session["dtFilter"] = dt;
        Session["Device"] = dt;

  
    }
    public void DisplayMessage(string str)
    {
        ScriptManager.RegisterStartupScript(this, GetType(), "", "alert('" + str + "');", true);
    }

    protected void gvDevice_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        gvDevice.PageIndex = e.NewPageIndex;

        DataTable dt = (DataTable)Session["dtFilter"];
        gvDevice.DataSource = dt;
        gvDevice.DataBind();
     

    }
    protected void gvDevice_OnSorting(object sender, GridViewSortEventArgs e)
    {
        HDFSort.Value = HDFSort.Value == "ASC" ? "DESC" : "ASC";
        DataTable dt = new DataTable();
        dt = (DataTable)Session["dtFilter"];
        DataView dv = new DataView(dt);
        string Query = "" + e.SortExpression + " " + HDFSort.Value + "";
        dv.Sort = Query;
        dt = dv.ToTable();
        Session["dtFilter"] = dt;
        gvDevice.DataSource = dt;
        gvDevice.DataBind();
        
    }
    protected void LnkDeviceOp_Click(object sender, ImageClickEventArgs e)
    {

   
              int index = ((GridViewRow)((ImageButton)sender).Parent.Parent).RowIndex;
        string ip = gvDevice.DataKeys[index]["IP_Address"].ToString();
        string deviceid = gvDevice.DataKeys[index]["Device_Id"].ToString();
        string port = gvDevice.DataKeys[index]["Port"].ToString();
        

        bool b = false;

       

      
       
            b = objDeviceOp.Device_Connection(ip, Convert.ToInt32(port),0);
       


        if (b == true)
        {

            DisplayMessage("Device Is Functional");


         

            pnlList.Visible = false;
            pnlDeviceOp.Visible = true;
            lblDeviceId.Text = deviceid;
            lblDeviceWithId.Text = gvDevice.DataKeys[index]["Device_Name"].ToString();

        }
        else
        {



            DisplayMessage("Unable to connect the device");


        }

        Session["IPForOp"] = ip;
        Session["PortForOp"] = port;
        Session["DeviceIdForOp"] = gvDevice.DataKeys[index]["Device_Id"].ToString();
    }

    protected void lnkBackFromManage_Click(object sender, EventArgs e)
    {
        Session["IPforManage"] = "";
        Session["PortForManage"] = "";

        pnlList.Visible = true;
        pnlDeviceOp.Visible = false;
    }



    protected void btnClearLog_Click(object sender, EventArgs e)
    {
       

        string port = Session["PortForOp"].ToString();
        string IP = Session["IPForOp"].ToString();
        string Deviceid = Session["DeviceIdForOp"].ToString();


        
             



        if (objDeviceOp.ClearLog(Session["IPforOp"].ToString(), Convert.ToInt32(Session["PortForOp"])))
        {

           
                DisplayMessage("All Attendance Logs Have Been Cleared From Terminal");
          
           


        }
        else
        {
            
           
           
                DisplayMessage("Operation Failed");
            
           

        }
    }
    protected void btnClearadmin_Click(object sender, EventArgs e)
    {

        bool b = objDeviceOp.ClearAdminPrivilege(Session["IPforOp"].ToString(), Convert.ToInt32(Session["PortForOp"]));
        if (b)
        {
                ScriptManager.RegisterStartupScript(this, GetType(), "", "alert('Admin Privilege Cleared')", true);
         
           



        }
        else
        {

            DisplayMessage("Error In Operation");
           
        }
    }
    protected void btnInitialize_Click(object sender, EventArgs e)
    {

       
        if (objDeviceOp.Device_Connection(Session["IPForOp"].ToString(), Convert.ToInt32(Session["PortForOp"]),0))
        {
            bool b = false;
             b = objDeviceOp.InitializeDevice(Session["IPForOp"].ToString(), Convert.ToInt32(Session["PortForOp"]));
            if (b)
            {

                DisplayMessage("Device Initialized");
                



            }
            else
            {

                DisplayMessage("Error In Operation");
                
            }
        }
        else
        {

            DisplayMessage("Unable To Connected Device");
            



        }

    }

    protected void btnRestrat_Click(object sender, EventArgs e)
    {


       
        bool b = objDeviceOp.RestartDevice(Session["IPForOp"].ToString(), Convert.ToInt32(Session["PortForOp"]));
        if (b)
        {

            DisplayMessage("Device Restarted Successfully");
            



        }
        else
        {

            DisplayMessage("Error In Operation");
           

        }

    }
    protected void btnPowerOff_Click(object sender, EventArgs e)
    {


        bool b = objDeviceOp.PowerOffDevice(Session["IPForOp"].ToString(), Convert.ToInt32(Session["PortForOp"]));
        if (b)
        {


            DisplayMessage("Device Off Successfully");

         


        }
        else
        {

            DisplayMessage("Error In Operation");
            
            
        }
    }
    protected void btnSynctime_Click(object sender, EventArgs e)
    {
       
       

        bool b = objDeviceOp.SynchTime(Session["IPforOp"].ToString(), Convert.ToInt32(Session["PortForOp"]));
        if (b)
        {
            
                DisplayMessage("Device Time Set With System Time");
           



        }
        else
        {

            DisplayMessage("Error In Operation");
           
        }
    }

}
