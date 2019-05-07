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


public partial class Device_DeleteUser : BasePage
{
    Att_DeviceMaster objDevice = new Att_DeviceMaster();
    Common cmn = new Common();
    SystemParameter objSys = new SystemParameter();
    EmployeeMaster objEmp=new EmployeeMaster();
    Device_Operation_Lan objDeviceOp = new Device_Operation_Lan();
    EmployeeInformation objEmpInfo = new EmployeeInformation();
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
        DataTable dtAllPageCode = cmn.GetAllPagePermission(Session["UserId"].ToString(), "14", "80");
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
    protected void btnDownload_Click(object sender, EventArgs e)
    {
        bool bIs = false;
        bool IsDeviceSelected = false;

        DataTable dtEmp = objEmp.GetEmployeeMaster(Session["CompId"].ToString());

        dtEmp = new DataView(dtEmp, "Brand_Id='" + Session["BrandId"].ToString() + "'  and Location_Id='" + Session["LocId"].ToString() + "'", "", DataViewRowState.CurrentRows).ToTable();
        DataTable dtUser = new DataTable();
        foreach (GridViewRow gvdevicerow in gvDevice.Rows)
        {
            if (((CheckBox)gvdevicerow.FindControl("chkSelectDevice")).Checked)
            {
                IsDeviceSelected = true;
                IsDeviceSelected = true;
                int index = gvdevicerow.RowIndex;
                string port = gvDevice.DataKeys[index]["Port"].ToString();
                string IP = gvDevice.DataKeys[index]["IP_Address"].ToString();
                string DeviceId = gvDevice.DataKeys[index]["Device_Id"].ToString();
                 
                bIs = objDeviceOp.Device_Connection(IP, Convert.ToInt32(port),0);

                if (bIs)
                {
                    
                    DataTable dtUserTemp = new DataTable();

                    dtUserTemp = objDeviceOp.GetUser(IP, Convert.ToInt32(port));
                    if (dtUserTemp.Rows.Count > 0)
                    {
                        
                        dtUserTemp.Columns.Add("Device_Id");
                        dtUserTemp.Columns.Add("IP");
                        dtUserTemp.Columns.Add("Port");
                        dtUserTemp.Columns.Add("Emp_Id");   
                        for (int userrowid = 0; userrowid < dtUserTemp.Rows.Count; userrowid++)
                        {
                            DataTable dtEmpTemp = new DataTable();
                            dtUserTemp.Rows[userrowid]["Device_Id"] = DeviceId;
                            dtUserTemp.Rows[userrowid]["IP"] = IP;
                            dtUserTemp.Rows[userrowid]["Port"] = port;
                            if ((!Convert.IsDBNull(dtUserTemp.Rows[userrowid]["sdwEnrollNumber"])) && dtUserTemp.Rows[userrowid]["sdwEnrollNumber"].ToString() != "")
                            {
                                dtEmpTemp = new DataView(dtEmp, "Emp_Code='" + dtUserTemp.Rows[userrowid]["sdwEnrollNumber"] + "'", "", DataViewRowState.CurrentRows).ToTable();
                            }



                            
                            if (dtEmpTemp.Rows.Count > 0)
                            {
                                dtUserTemp.Rows[userrowid]["sdwEnrollNumber"] = dtUserTemp.Rows[userrowid]["sdwEnrollNumber"].ToString();

                                dtUserTemp.Rows[userrowid]["Emp_Id"] = dtUserTemp.Rows[userrowid]["sdwEnrollNumber"];
                            }
                            else
                            {
                                dtUserTemp.Rows[userrowid]["Emp_Id"] = dtUserTemp.Rows[userrowid]["sdwEnrollNumber"];
                          
                                dtUserTemp.Rows[userrowid]["sdwEnrollNumber"] = "";
                               }

                        }

                        //now merge it to User table
                        if (dtUser.Rows.Count == 0)
                        {
                            dtUser = dtUserTemp;
                        }
                        else
                        {
                            dtUser.Merge(dtUserTemp);
                        }



                    }

                }
            }
        }

        if (rbtnNew.Checked)
        {
            try
            {
                dtUser = new DataView(dtUser, "sdwEnrollNumber=''", "", DataViewRowState.CurrentRows).ToTable();
            }
            catch
            {
                dtUser = null;
            }
        }
        if (dtUser != null && dtUser.Rows.Count > 0)
        {
            
            Session["DtDeviceUser"] = dtUser;


            gvUser.DataSource = dtUser;
            gvUser.DataBind();
           lblUserCount.Text = dtUser.Rows.Count.ToString();


            DisplayMessage(dtUser.Rows.Count.ToString() + " " + "Users Downloaded");

            pnlDeviceOp.Visible = true;
            pnlList.Visible = false;
            
        }
        else
        {

            if (!IsDeviceSelected)
            {
                DisplayMessage("Please Select Device");

            }
            else
            {
                DisplayMessage("User does not exists");

            }
        }
    }

    protected void btnDeleteSelected_Click(object sender, EventArgs e)
    {
       
        string optype = "1";
        DataTable dtUser = new DataTable();
        dtUser.Columns.Add("sdwEnrollNumber");
        dtUser.Columns.Add("sName");
        dtUser.Columns.Add("sPassWord");
        dtUser.Columns.Add("iPrivilege");
        dtUser.Columns.Add("sTmpData");
        dtUser.Columns.Add("sCardNumber");
        dtUser.Columns.Add("Emp_Id");
        dtUser.Columns.Add("IP");
        dtUser.Columns.Add("Port");
        dtUser.Columns.Add("Device_Id");
        dtUser.Columns.Add("sEnabled");

        if (chkUser.Checked && chkFinger.Checked && chkFace.Checked)
        {
            optype = "5";
        }

        else if (chkUser.Checked && chkFinger.Checked && !chkFace.Checked)
        {
            optype = "6";
        }
        else  if (chkUser.Checked && !chkFinger.Checked && chkFace.Checked)
        {
            optype = "7";
        }
        else if (chkFinger.Checked && chkFace.Checked)
        {
            optype = "4";
        }

        else if (chkFace.Checked)
        {
            optype = "3";
        }
        else if (chkFinger.Checked)
        {
            optype = "2";
        }
        else if (chkUser.Checked)
        {
            optype = "1";
        }

        for (int rowcount = 0; rowcount < gvUser.Rows.Count; rowcount++)
        {

            if (((CheckBox)gvUser.Rows[rowcount].FindControl("chkSel")).Checked)
            {
                string EnrollNo = gvUser.DataKeys[rowcount]["sdwEnrollNumber"].ToString();

                string Password = gvUser.DataKeys[rowcount]["sPassword"].ToString();
                string EmpId = gvUser.DataKeys[rowcount]["Emp_Id"].ToString();
                string empname = gvUser.DataKeys[rowcount]["sName"].ToString();
                string Privilege = gvUser.DataKeys[rowcount]["iPrivilege"].ToString();
                string FingerTemplate = string.Empty;
                string DeviceId = gvUser.DataKeys[rowcount]["Device_Id"].ToString();
                string ip = gvUser.DataKeys[rowcount]["IP"].ToString();
                string Port = gvUser.DataKeys[rowcount]["Port"].ToString();

                string iflag = string.Empty;
                string senabled = gvUser.DataKeys[rowcount]["bEnabled"].ToString();
                string CardNo = gvUser.DataKeys[rowcount]["sCardNumber"].ToString();
               
                DataRow dr = dtUser.NewRow();
                dr["sdwEnrollNumber"] = EnrollNo;
                dr["sName"] = empname;
                dr["sPassword"] = Password;
                dr["iPrivilege"] = Privilege;
                dr["sEnabled"] = senabled;
                dr["sCardNumber"] = CardNo;
                dr["Emp_Id"] = EmpId;
                dr["Device_Id"] = DeviceId;
                dr["IP"] = ip;
                dr["Port"] = Port;
                dr["sEnabled"] = senabled;
                dtUser.Rows.Add(dr);
            }
        }




     
        DataTable dtFinger = new DataTable();
        DataTable dtFace = new DataTable();

        DataTable dtDistinctDevice = dtUser.DefaultView.ToTable(true, "Device_Id");
        for (int devicecounter = 0; devicecounter < dtDistinctDevice.Rows.Count; devicecounter++)
        {
            DataTable dtUserByDevice = new DataView(dtUser, "Device_Id='" + dtDistinctDevice.Rows[devicecounter][0].ToString() + "'", "", DataViewRowState.CurrentRows).ToTable();
            if (dtUserByDevice.Rows.Count > 0)
            {
                string ip = dtUserByDevice.Rows[0]["IP"].ToString();
                string Port = dtUserByDevice.Rows[0]["Port"].ToString();
                bool IsDeviceConnected = false;
                if (objDeviceOp.Device_Connection(ip, Convert.ToInt32(Port),0))
                {
                    IsDeviceConnected = true;

                }
                for (int rowcount = 0; rowcount < dtUserByDevice.Rows.Count; rowcount++)
                {
                    objEmp = new EmployeeMaster();
                    if (rowcount % 50 == 0)
                    {
                        ;
                    }

                    string EnrollNo = dtUserByDevice.Rows[rowcount]["sdwEnrollNumber"].ToString();
                    string DeviceEmpName = dtUserByDevice.Rows[rowcount]["sName"].ToString();
                    string Password = dtUserByDevice.Rows[rowcount]["sPassword"].ToString();
                    string EmpId = dtUserByDevice.Rows[rowcount]["Emp_Id"].ToString();

                    string Privilege = dtUserByDevice.Rows[rowcount]["iPrivilege"].ToString();
                    string DeviceId = dtUserByDevice.Rows[rowcount]["Device_Id"].ToString();
                    string senabled = dtUserByDevice.Rows[rowcount]["sEnabled"].ToString();
                    string CardNo = dtUserByDevice.Rows[rowcount]["sCardNumber"].ToString();
                    string FingerTemplate = string.Empty;
                    string iflag = string.Empty;
                    string faceindex = string.Empty;
                    string facedata = string.Empty;
                    string facelength = string.Empty;
                    string fingerindex = string.Empty;

                    bool b = false;
                    if (IsDeviceConnected)
                    {
                        if (optype == "1")
                        {
                            try
                            {

                                b = objDeviceOp.DelSingleUser(ip, Convert.ToInt32(Port), Convert.ToInt32(EmpId));

                            }
                            catch
                            {

                            }



                        }

                        else if (optype == "2")
                        {
                            try
                            {
                                b = objDeviceOp.DelSingleUserFinger(ip, Convert.ToInt32(Port), Convert.ToInt32(EmpId));
                            }
                            catch
                            {

                            }


                      
                        }
                        else if (optype == "3")
                        {
                            try
                            {
                                b = objDeviceOp.DelSingleUserFace(ip, Convert.ToInt32(Port), (EmpId));

                            }
                            catch
                            {

                            }


                           
                            
                        }
                        else if (optype == "4")
                        {

                            try
                            {

                                b = objDeviceOp.DelSingleUserFinger(ip, Convert.ToInt32(Port), Convert.ToInt32(EmpId));

                                b = objDeviceOp.DelSingleUserFace(ip, Convert.ToInt32(Port), (EmpId));

                            }
                            catch
                            {
                            }


                        }
                        

                        else if (optype == "5")
                        {

                            try
                            {
                            b = objDeviceOp.DelSingleUserFinger(ip, Convert.ToInt32(Port), Convert.ToInt32(EmpId));

                            b = objDeviceOp.DelSingleUserFace(ip, Convert.ToInt32(Port), (EmpId));

                            b=objDeviceOp.DelSingleUser(ip,Convert.ToInt32(Port),Convert.ToInt32(EmpId));
                            }
                            catch
                            {
                            }

                        }

                        else if (optype == "6")
                        {
                            try
                            {
                            b=objDeviceOp.DelSingleUser(ip,Convert.ToInt32(Port),Convert.ToInt32(EmpId));
                            b = objDeviceOp.DelSingleUserFinger(ip, Convert.ToInt32(Port), Convert.ToInt32(EmpId));
                           }
                            catch
                            {
                            }

                        }

                        else if (optype == "7")
                        {
                            try
                            {
                            b = objDeviceOp.DelSingleUser(ip, Convert.ToInt32(Port), Convert.ToInt32(EmpId));
                            b = objDeviceOp.DelSingleUserFace(ip, Convert.ToInt32(Port), (EmpId));
                            }
                            catch
                            {
                            }

                        }

                    }
                   
                   
                    
                    
                   
                   
                   
                    
                    

           }
               
            }
        }

        gvUser.DataSource = null;
        gvUser.DataBind();
        pnlList.Visible = true;
        pnlDeviceOp.Visible = false;

            DisplayMessage("Users Deleted");
       

    
    }
    protected void chkSelAll_CheckedChanged(object sender, EventArgs e)
    {
        bool chk = ((CheckBox)gvUser.HeaderRow.FindControl("chkSelAll")).Checked;
        foreach (GridViewRow gvrow in gvUser.Rows)
        {
            ((CheckBox)gvrow.FindControl("chkSel")).Checked = chk;
        }

    }
    protected void gvUser_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        gvUser.PageIndex = e.NewPageIndex;
        DataTable dt = (DataTable)Session["LiveUser"];
        gvUser.DataSource = dt;
        gvUser.DataBind();

    }
    protected void chkSelAll_CheckedChanged1(object sender, EventArgs e)
    {
        bool b = ((CheckBox)sender).Checked;
        foreach (GridViewRow gvrow in gvDevice.Rows)
        {
            ((CheckBox)gvrow.FindControl("chkSelectDevice")).Checked = b;
        }
    }
    protected void lnkBackFromManage_Click(object sender, EventArgs e)
    {
        Session["IPforManage"] = "";
        Session["PortForManage"] = "";

        pnlList.Visible = true;
        pnlDeviceOp.Visible = false;
    }



   
   


}
