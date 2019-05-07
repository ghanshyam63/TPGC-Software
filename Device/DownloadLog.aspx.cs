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

public partial class Device_DownLoadLog : BasePage
{
    Att_DeviceMaster objDevice = new Att_DeviceMaster();
    Common cmn = new Common();
    SystemParameter objSys = new SystemParameter();
    Att_AttendanceLog objAttlog = new Att_AttendanceLog();
    Device_Operation_Lan objDeviceOp = new Device_Operation_Lan();
    EmployeeInformation objEmpInfo = new EmployeeInformation();
    EmployeeMaster objEmp = new EmployeeMaster();
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
        DataTable dtAllPageCode = cmn.GetAllPagePermission(Session["UserId"].ToString(), "14", "77");
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



    protected void gvLog_OnPageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        gvLog.PageIndex = e.NewPageIndex;

        DataTable dt = (DataTable)Session["dtDownloadLog"];
        gvLog.DataSource = dt;
        gvLog.DataBind();


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
        Session["DevId"]=deviceid;

        bool b = false;
       
            b = objDeviceOp.Device_Connection(ip, Convert.ToInt32(port),0);
        


        if (b == true)
        {
                            

          
            lblDeviceId.Text = deviceid;
            lblDeviceWithId.Text = gvDevice.DataKeys[index]["Device_Name"].ToString();

            DataTable dtLog = new DataTable();

            dtLog = objDeviceOp.GetUserLog(ip,Convert.ToInt32(port));

            dtLog=FilterLog(dtLog);

            if (dtLog.Rows.Count > 0)
            {

                gvLog.DataSource = dtLog;
                gvLog.DataBind();
                Session["dtDownloadLog"] = dtLog;
            }
            else
            {
                DisplayMessage("No Log Data Exists");
                return;
            }
        }
        else
        {



            DisplayMessage("Unable to connect the device");
            return;

        }
        pnlList.Visible = false;
        pnlDeviceOp.Visible = true;
        Session["IPForOp"] = ip;
        Session["PortForOp"] = port;
        Session["DeviceIdForOp"] = gvDevice.DataKeys[index]["Device_Id"].ToString();
    }
    public string GetEmployeeCode(object empid)
    {

        string empname = string.Empty;

        DataTable dt = objEmp.GetEmployeeMasterAllData(Session["CompId"].ToString());
        dt = new DataView(dt, "Emp_Id='" + empid.ToString().Trim() + "'", "", DataViewRowState.CurrentRows).ToTable();

        if (dt.Rows.Count > 0)
        {

            empname = dt.Rows[0]["Emp_Code"].ToString();

            if (empname == "")
            {
                empname = "No Code";
            }

        }
        else
        {
            empname = "No Code";

        }

        return empname;



    }
    public DataTable FilterLog(DataTable dt)
    {
        DataTable dtLog = new DataTable();
        DataTable dtEmp = new DataTable();
        DataTable dtAttLog = new DataTable();
        dtEmp = objEmpInfo.GetEmployeeAccessInfo();
        dtEmp = dtEmp.DefaultView.ToTable(true,"Emp_Id");
        string EmpIds=string.Empty;
       
        
        for (int i = 0; i < dtEmp.Rows.Count;i++ )
        {
            EmpIds+=GetEmployeeCode(dtEmp.Rows[i]["Emp_Id"].ToString())+",";           
        }

        if(EmpIds!="")
        {
        dtLog = new DataView(dt,"sdwEnrollNumber in("+EmpIds+")","",DataViewRowState.CurrentRows).ToTable();
        }


        dtAttLog = objAttlog.GetAttendanceLog(Session["CompId"].ToString());


        DataTable dtFilter = new DataTable();
        DataTable dtFinal = new DataTable();
        dtFinal = dtLog.Clone();
        for (int j = 0; j < dtLog.Rows.Count;j++ )
        {
            dtFilter = new DataView(dtAttLog,"Emp_Id='"+GetEmpId(dtLog.Rows[j]["sdwEnrollNumber"].ToString())+"' and Event_Time='"+dtLog.Rows[j]["sTimeString"].ToString()+"' and Device_Id='"+Session["DevId"].ToString()+"' ", "", DataViewRowState.CurrentRows).ToTable();

            if(dtFilter.Rows.Count == 0)
            {
                dtFinal.ImportRow(dtLog.Rows[j]);


            }


        }



        return dtFinal;



    }
    public string GetEmpId(string empcode)
    {

        string empId = string.Empty;

        DataTable dt = objEmp.GetEmployeeMasterAllData(Session["CompId"].ToString());
        dt = new DataView(dt, "Emp_Code='" + empcode.ToString().Trim() + "'", "", DataViewRowState.CurrentRows).ToTable();

        if (dt.Rows.Count > 0)
        {

            empId = dt.Rows[0]["Emp_Id"].ToString();



        }


        return empId;



    }


    protected void btnSave_Click(object sender, EventArgs e)
    {
        DataTable dtLog = new DataTable();
        dtLog = (DataTable)Session["dtDownloadLog"];
        int b = 0;
        if(dtLog.Rows.Count > 0)
        {
            for (int i = 0; i < dtLog.Rows.Count;i++ )
            {
                b = objAttlog.InsertAttendanceLog(Session["CompId"].ToString(), GetEmpId(dtLog.Rows[i]["sdwEnrollNumber"].ToString()), Session["DevId"].ToString(), Convert.ToDateTime(dtLog.Rows[i]["sTimeString"].ToString()).ToString(), Convert.ToDateTime(dtLog.Rows[i]["sTimeString"].ToString()).ToString(), dtLog.Rows[i]["idwInOutMode"].ToString(), "In", "By Device", true.ToString(), Session["UserId"].ToString(), DateTime.Now.ToString(), Session["UserId"].ToString(), DateTime.Now.ToString());


            }

        }
        Session["dtDownloadLog"] = null;
        gvLog.DataSource = null;
        gvLog.DataBind();
        pnlList.Visible = true;
        pnlDeviceOp.Visible = false;
        DisplayMessage("Log Saved");
            

        }



    protected void btnCancel_Click(object sender, EventArgs e)
    {
        Session["dtDownloadLog"] = null;
        gvLog.DataSource = null;
        gvLog.DataBind();
        pnlList.Visible = true;
        pnlDeviceOp.Visible = false;

    }
    protected void lnkBackFromManage_Click(object sender, EventArgs e)
    {
        Session["IPforManage"] = "";
        Session["PortForManage"] = "";

        pnlList.Visible = true;
        pnlDeviceOp.Visible = false;
    }



   

}
