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
using Device_SDK;

public partial class Attendance_DeviceMaster : BasePage
{
   
    Common cmn = new Common();
    SystemParameter objSys = new SystemParameter();
    Att_DeviceMaster objDevice = new Att_DeviceMaster(); 
    Att_ShiftDescription objShiftDesc = new Att_ShiftDescription();
     Ser_UserTransfer objSer = new Ser_UserTransfer();
    Att_ScheduleMaster objSch = new Att_ScheduleMaster();
    Set_ApplicationParameter objAppParam = new Set_ApplicationParameter();
    protected void Page_Load(object sender, EventArgs e)
    {
       
       
        if (Session["UserId"] == null)
        {
            Response.Redirect("~/ERPLogin.aspx");
        }
        Page.Title = objSys.GetSysTitle();
        AllPageCode();
        if (!IsPostBack)
        {
            txtValue.Focus();
           
            FillGridBin();
            FillGrid();
            
            btnList_Click(null, null);
            
        }
       
    }

    public void AllPageCode()
    {
       

        Session["AccordianId"] = "14";
        Session["HeaderText"] = "Device Setup";
        DataTable dtAllPageCode = cmn.GetAllPagePermission(Session["UserId"].ToString(), "14", "75");
        if (dtAllPageCode.Rows.Count != 0)
        {
            if (dtAllPageCode.Rows[0][0].ToString() == "SuperAdmin")
            {
                btnSave.Visible = true;
                foreach (GridViewRow Row in gvDevice.Rows)
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
                    foreach (GridViewRow Row in gvDevice.Rows)
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
    protected void txtDeviceName_OnTextChanged(object sender, EventArgs e)
    {

        if (editid.Value == "")
        {
            DataTable dt = objDevice.GetDeviceMasterByDeviceName(Session["CompId"].ToString().ToString(), txtDeviceName.Text.Trim());
            if (dt.Rows.Count > 0)
            {
                txtDeviceName.Text = "";
                DisplayMessage("Device Name Already Exists");
                System.Web.UI.ScriptManager.GetCurrent(this).SetFocus(txtDeviceName);
                return;
            }
            DataTable dt1 = objDevice.GetDeviceMasterInactive(Session["CompId"].ToString().ToString());
            dt1 = new DataView(dt1, "Device_Name='" + txtDeviceName.Text + "'", "", DataViewRowState.CurrentRows).ToTable();
            if (dt1.Rows.Count > 0)
            {
                txtDeviceName.Text = "";
                DisplayMessage("Device Name Already Exists - Go to Bin Tab");
                System.Web.UI.ScriptManager.GetCurrent(this).SetFocus(txtDeviceName);
                return;
            }
            txtDeviceNameL.Focus();
        }
        else
        {
            DataTable dtTemp = objDevice.GetDeviceMasterById(Session["CompId"].ToString().ToString(), editid.Value);
            if (dtTemp.Rows.Count > 0)
            {
                if (dtTemp.Rows[0]["Device_Name"].ToString() != txtDeviceName.Text)
                {
                    DataTable dt = objDevice.GetDeviceMaster(Session["CompId"].ToString().ToString());
                    dt = new DataView(dt, "Device_Name='" + txtDeviceName.Text + "'", "", DataViewRowState.CurrentRows).ToTable();
                    if (dt.Rows.Count > 0)
                    {
                        txtDeviceName.Text = "";
                        DisplayMessage("Device Name Already Exists");
                        System.Web.UI.ScriptManager.GetCurrent(this).SetFocus(txtDeviceName);
                        return;
                    }
                    DataTable dt1 = objDevice.GetDeviceMaster(Session["CompId"].ToString().ToString());
                    dt1 = new DataView(dt1, "Device_Name='" + txtDeviceName.Text + "'", "", DataViewRowState.CurrentRows).ToTable();
                    if (dt1.Rows.Count > 0)
                    {
                        txtDeviceName.Text = "";
                        DisplayMessage("Device Name Already Exists - Go to Bin Tab");
                        System.Web.UI.ScriptManager.GetCurrent(this).SetFocus(txtDeviceName);
                        return;
                    }
                }
            }
            txtDeviceNameL.Focus();
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
    }
    protected void btnNew_Click(object sender, EventArgs e)
    {
         txtDeviceName.Focus();
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
            DataTable dtCust = (DataTable)Session["Device"];


            DataView view = new DataView(dtCust, condition, "", DataViewRowState.CurrentRows);
            Session["dtFilter"] = view.ToTable();
            lblTotalRecords.Text = Resources.Attendance.Total_Records + ": " + view.ToTable().Rows.Count.ToString() + "";

            gvDevice.DataSource = view.ToTable();
            gvDevice.DataBind();
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
            DataTable dtCust = (DataTable)Session["dtbinDevice"];


            DataView view = new DataView(dtCust, condition, "", DataViewRowState.CurrentRows);
            Session["dtbinFilter"] = view.ToTable();
            lblbinTotalRecords.Text = Resources.Attendance.Total_Records + ": " + view.ToTable().Rows.Count.ToString() + "";

            gvDeviceBin.DataSource = view.ToTable();
            gvDeviceBin.DataBind();


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
    private int GetMinuteDiff(string greatertime, string lesstime)
    {

        if (greatertime == "__:__:__" || greatertime=="")
        {
            return 0;
        }
        if (lesstime == "__:__:__" || lesstime=="")
        {
            return 0;
        }
        int retval = 0;
        int actTimeHour = Convert.ToInt32(greatertime.Split(':')[0]);
        int ondutyhour = Convert.ToInt32(lesstime.Split(':')[0]);
        int actTimeMinute = Convert.ToInt32(greatertime.Split(':')[1]);
        int ondutyMinute = Convert.ToInt32(lesstime.Split(':')[1]);
        int totalActTimeMinute = actTimeHour * 60 + actTimeMinute;
        int totalOnDutyTimeMinute = ondutyhour * 60 + ondutyMinute;
        if (totalActTimeMinute - totalOnDutyTimeMinute < 0)
        {
            retval = 1440 + (totalActTimeMinute - totalOnDutyTimeMinute);
        }
        else
        {
            retval = (totalActTimeMinute - totalOnDutyTimeMinute);
        }
        return retval;
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

    protected void gvDeviceBin_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {

        gvDeviceBin.PageIndex = e.NewPageIndex;
        if (HDFSortbin.Value == "")
            FillGridBin();
        else
        {
            DataTable dt = (DataTable)Session["dtbinFilter"];
            gvDeviceBin.DataSource = dt;
            gvDeviceBin.DataBind();
            AllPageCode();
        }
        string temp = string.Empty;
        bool isselcted;

        for (int i = 0; i < gvDeviceBin.Rows.Count; i++)
        {
            Label lblconid = (Label)gvDeviceBin.Rows[i].FindControl("lblDeviceId");
            string[] split = lblSelectedRecord.Text.Split(',');

            for (int j = 0; j < lblSelectedRecord.Text.Split(',').Length; j++)
            {
                if (lblSelectedRecord.Text.Split(',')[j] != "")
                {
                    if (lblconid.Text.Trim().ToString() == lblSelectedRecord.Text.Split(',')[j].Trim().ToString())
                    {
                        ((CheckBox)gvDeviceBin.Rows[i].FindControl("chkgvSelect")).Checked = true;
                    }
                }
            }
        }

    }
    protected void gvDeviceBin_OnSorting(object sender, GridViewSortEventArgs e)
    {
        lblSelectedRecord.Text = "";
        HDFSortbin.Value = HDFSortbin.Value == "ASC" ? "DESC" : "ASC";
        DataTable dt = new DataTable();
        dt = objDevice.GetDeviceMasterInactive(Session["CompId"].ToString());
        DataView dv = new DataView(dt);
        string Query = "" + e.SortExpression + " " + HDFSortbin.Value + "";
        dv.Sort = Query;
        dt = dv.ToTable();
        Session["dtbinFilter"] = dt;
        gvDeviceBin.DataSource = dt;
        gvDeviceBin.DataBind();
        AllPageCode();

    }
    

   
    protected void btnSave_Click(object sender, EventArgs e)
    {
        int b = 0;

      

        if (txtDeviceName.Text == "")
        {
            DisplayMessage("Enter Device Name");
            txtDeviceName.Focus();
            return;
        }

       if(txtIPAddress.Text=="")
       {
           DisplayMessage("Enter IP Address");
           txtIPAddress.Focus();
           return;

       }

        

      

       

       


        if (editid.Value == "")
        {
            
            DataTable dt1 = objDevice.GetDeviceMaster(Session["CompId"].ToString());

           DataTable  dt2 = new DataView(dt1, "Device_Name='" + txtDeviceName.Text + "'", "", DataViewRowState.CurrentRows).ToTable();
            if (dt2.Rows.Count > 0)
            {
                DisplayMessage("Device Name Already Exists");
                txtDeviceName.Focus();
                return;

            }
            DataTable dt3 = new DataView(dt1, "IP_Address='" + txtIPAddress.Text + "'", "", DataViewRowState.CurrentRows).ToTable();
            if (dt3.Rows.Count > 0)
            {
                DisplayMessage("IP Address Already Exists");
                txtIPAddress.Focus();
                return;

            }



            b = objDevice.InsertDeviceMaster(Session["CompId"].ToString(), txtDeviceName.Text, txtDeviceNameL.Text, Session["BrandId"].ToString(), Session["LocId"].ToString(),txtIPAddress.Text,ddlCommType.Text,txtPortNumber.Text,  "", "", "", "", "", true.ToString(), DateTime.Now.ToString(), true.ToString(), Session["UserId"].ToString(), DateTime.Now.ToString(), Session["UserId"].ToString(), DateTime.Now.ToString());
            if (b != 0)
            {
               
                EmployeeMaster objEmp=new EmployeeMaster ();

                    DataTable dtEmp=new DataTable();

                    string EmpSync = string.Empty;
                    EmpSync = objAppParam.GetApplicationParameterValueByParamName("Employee Synchronization", Session["CompId"].ToString());

                if (EmpSync == "Company")
                {
                    dtEmp = objEmp.GetEmployeeMaster(Session["CompId"].ToString());

                    if (dtEmp.Rows.Count > 0)
                        {

                           
                            for (int i = 0; i < dtEmp.Rows.Count; i++)
                            {
                                objSer.InsertUserTransfer(dtEmp.Rows[i]["Emp_Id"].ToString(),b.ToString(), false.ToString(), DateTime.Now.ToString(), "1/1/1900", "", "", "", "", "", true.ToString(), DateTime.Now.ToString(), true.ToString(), Session["UserId"].ToString(), DateTime.Now.ToString(), Session["UserId"].ToString(), DateTime.Now.ToString());

                            }
                        

                        }


                }
                else
                {
                    dtEmp = objEmp.GetEmployeeMaster(Session["CompId"].ToString());
                    dtEmp = new DataView(dtEmp, "Location_Id='" + Session["LocId"].ToString() + "'", "", DataViewRowState.CurrentRows).ToTable();

                    if (dtEmp.Rows.Count > 0)
                    {


                        for (int i = 0; i < dtEmp.Rows.Count; i++)
                        {
                            objSer.InsertUserTransfer(dtEmp.Rows[i]["Emp_Id"].ToString(),b.ToString(), false.ToString(), DateTime.Now.ToString(), "1/1/1900", "", "", "", "", "", true.ToString(), DateTime.Now.ToString(), true.ToString(), Session["UserId"].ToString(), DateTime.Now.ToString(), Session["UserId"].ToString(), DateTime.Now.ToString());

                        }


                    }
                }

                DisplayMessage("Record Saved");
                FillGrid();
                Reset();
                btnList_Click(null, null);
            }
            else
            {
                DisplayMessage("Record Not Saved");
            }
        }
        else
        {
            string DeviceTypeName = string.Empty;
            DataTable dt1 = objDevice.GetDeviceMaster(Session["CompId"].ToString());
            try
            {
                DeviceTypeName = new DataView(dt1, "Device_Id='" + editid.Value + "'", "", DataViewRowState.CurrentRows).ToTable().Rows[0]["Device_Name"].ToString();
            }
            catch
            {
                DeviceTypeName = "";
            }
            dt1 = new DataView(dt1, "Device_Name='" + txtDeviceName.Text + "' and Device_Name<>'"+DeviceTypeName+"'", "", DataViewRowState.CurrentRows).ToTable();
            if (dt1.Rows.Count > 0)
            {
                DisplayMessage("Device Name Already Exists");
                txtDeviceName.Focus();
                return;

            }
            DataTable dt3 = objDevice.GetDeviceMaster(Session["CompId"].ToString());
            dt3 = new DataView(dt3, "IP_Address='" + txtIPAddress.Text + "' and Device_Id<>'" + editid.Value + "' ", "", DataViewRowState.CurrentRows).ToTable();
            if (dt3.Rows.Count > 0)
            {
                DisplayMessage("IP Address Already Exists");
                txtIPAddress.Focus();
                return;

            }





            b = objDevice.UpdateDeviceMaster(editid.Value, Session["CompId"].ToString(), txtDeviceName.Text, txtDeviceNameL.Text, Session["BrandId"].ToString(), Session["LocId"].ToString(), txtIPAddress.Text, ddlCommType.Text, txtPortNumber.Text, "", "", "", "", "", true.ToString(), DateTime.Now.ToString(), true.ToString(), Session["UserId"].ToString(), DateTime.Now.ToString());
          
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

    protected void lnkConnect_Click(object sender, ImageClickEventArgs e)
    {


               int errorcode = 0;
        int index = ((GridViewRow)((ImageButton)sender).Parent.Parent).RowIndex;
        string port = gvDevice.DataKeys[index]["Port"].ToString();
        string IP = gvDevice.DataKeys[index]["IP_Address"].ToString();
        string DeviceId = gvDevice.DataKeys[index]["Device_Id"].ToString();

        Device_Operation_Lan objDeviceOp = new Device_Operation_Lan();
       
        
        bool b = false;
            b = objDeviceOp.Device_Connection(IP,Convert.ToInt32(port),0);


        if (b == true)
        {
           
               DisplayMessage("Device Is Functional");          


           
        }
        else
        {


           
           DisplayMessage("Unable to connect the device");
           
           
       }
    }
     public void DisplayMessage(string str)
    {
        ScriptManager.RegisterStartupScript(this, GetType(), "", "alert('" + str + "');", true);
    }

    
    protected void btnEdit_Command(object sender, CommandEventArgs e)
    {
        editid.Value = e.CommandArgument.ToString();

       

       
        DataTable dt = objDevice.GetDeviceMasterById(Session["CompId"].ToString(), editid.Value);
        if (dt.Rows.Count > 0)
        {
           
            txtDeviceName.Text = dt.Rows[0]["Device_Name"].ToString();
            txtDeviceNameL.Text = dt.Rows[0]["Device_Name_L"].ToString();


            txtIPAddress.Text = dt.Rows[0]["IP_Address"].ToString();

            txtPortNumber.Text = dt.Rows[0]["Port"].ToString();
            try
            {
                ddlCommType.SelectedValue = dt.Rows[0]["Communication_Type"].ToString();
            }
            catch
            {

            }

           
           
            btnNew_Click(null, null);
            btnNew.Text = Resources.Attendance.Edit;

        }



    }

    protected void IbtnDelete_Command(object sender, CommandEventArgs e)
    {
      
       


        int b = 0;
        b = objDevice.DeleteDeviceMaster(Session["CompId"].ToString(), e.CommandArgument.ToString(), false.ToString(), Session["UserId"].ToString(), DateTime.Now.ToString());
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
    protected void gvDevice_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        gvDevice.PageIndex = e.NewPageIndex;

        DataTable dt = (DataTable)Session["dtFilter"];
        gvDevice.DataSource = dt;
        gvDevice.DataBind();
        AllPageCode();

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
        AllPageCode();
    }

    
    [System.Web.Services.WebMethodAttribute(), System.Web.Script.Services.ScriptMethodAttribute()]
    public static string[] GetCompletionListDeviceName(string prefixText, int count, string contextKey)
    {
        Att_DeviceMaster objAtt_Device = new Att_DeviceMaster();
        DataTable dt = new DataView(objAtt_Device.GetDeviceMaster(HttpContext.Current.Session["CompId"].ToString()), "Device_Name like '" + prefixText.ToString() + "%'", "", DataViewRowState.CurrentRows).ToTable();
        string[] txt = new string[dt.Rows.Count];

        for (int i = 0; i < dt.Rows.Count; i++)
        {
            txt[i] = dt.Rows[i]["Device_Name"].ToString();
        }
        return txt;
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
        DataTable dt = objDevice.GetDeviceMaster(Session["CompId"].ToString());
        dt = new DataView(dt, "Brand_Id='" + Session["BrandId"].ToString() + "' and Location_Id='" + Session["LocId"].ToString() + "'", "", DataViewRowState.CurrentRows).ToTable();
      
        gvDevice.DataSource = dt;
        gvDevice.DataBind();
        AllPageCode();
        Session["dtFilter"] = dt;
        Session["Device"] = dt;

        lblTotalRecords.Text = Resources.Attendance.Total_Records + ": " + dt.Rows.Count.ToString() + "";

    }
    public void FillGridBin()
    {
        DataTable dt = new DataTable();
        dt = objDevice.GetDeviceMasterInactive(Session["CompId"].ToString());
        dt = new DataView(dt, "Brand_Id='" + Session["BrandId"].ToString() + "' and Location_Id='" + Session["LocId"].ToString() + "'", "", DataViewRowState.CurrentRows).ToTable();
      
        gvDeviceBin.DataSource = dt;
        gvDeviceBin.DataBind();


        Session["dtbinFilter"] = dt;
        Session["dtbinDevice"] = dt;
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
        CheckBox chkSelAll = ((CheckBox)gvDeviceBin.HeaderRow.FindControl("chkgvSelectAll"));
        for (int i = 0; i < gvDeviceBin.Rows.Count; i++)
        {
            ((CheckBox)gvDeviceBin.Rows[i].FindControl("chkgvSelect")).Checked = chkSelAll.Checked;
            if (chkSelAll.Checked)
            {
                if (!lblSelectedRecord.Text.Split(',').Contains(((Label)(gvDeviceBin.Rows[i].FindControl("lblDeviceId"))).Text.Trim().ToString()))
                {
                    lblSelectedRecord.Text += ((Label)(gvDeviceBin.Rows[i].FindControl("lblDeviceId"))).Text.Trim().ToString() + ",";
                }
            }
            else
            {
                string temp = string.Empty;
                string[] split = lblSelectedRecord.Text.Split(',');
                foreach (string item in split)
                {
                    if (item != ((Label)(gvDeviceBin.Rows[i].FindControl("lblDeviceId"))).Text.Trim().ToString())
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
        Label lb = (Label)gvDeviceBin.Rows[index].FindControl("lblDeviceId");
        if (((CheckBox)gvDeviceBin.Rows[index].FindControl("chkgvSelect")).Checked)
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
                if (!lblSelectedRecord.Text.Split(',').Contains(dr["Device_Id"]))
                {
                    lblSelectedRecord.Text += dr["Device_Id"] + ",";
                }
            }
            for (int i = 0; i < gvDeviceBin.Rows.Count; i++)
            {
                string[] split = lblSelectedRecord.Text.Split(',');
                Label lblconid = (Label)gvDeviceBin.Rows[i].FindControl("lblDeviceId");
                for (int j = 0; j < lblSelectedRecord.Text.Split(',').Length; j++)
                {
                    if (lblSelectedRecord.Text.Split(',')[j] != "")
                    {
                        if (lblconid.Text.Trim().ToString() == lblSelectedRecord.Text.Split(',')[j].Trim().ToString())
                        {
                            ((CheckBox)gvDeviceBin.Rows[i].FindControl("chkgvSelect")).Checked = true;
                        }
                    }
                }
            }
        }
        else
        {
            lblSelectedRecord.Text = "";
            DataTable dtUnit1 = (DataTable)Session["dtBinFilter"];
            gvDeviceBin.DataSource = dtUnit1;
            gvDeviceBin.DataBind();
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
                    b = objDevice.DeleteDeviceMaster(Session["CompId"].ToString(),lblSelectedRecord.Text.Split(',')[j].Trim(), true.ToString(), Session["UserId"].ToString().ToString(), DateTime.Now.ToString());

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
            foreach (GridViewRow Gvr in gvDeviceBin.Rows)
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


       
        txtDeviceName.Text = "";
        txtDeviceNameL.Text = "";



        ddlCommType.SelectedIndex = 0;
        txtIPAddress.Text = "";
        txtPortNumber.Text = "";


        
        btnNew.Text = Resources.Attendance.New;
        lblSelectedRecord.Text = "";
        ViewState["Select"] = null;
        editid.Value = "";
        txtValue.Text = "";
        ddlOption.SelectedIndex = 2;
        ddlFieldName.SelectedIndex = 0;
   
        

    }

   [System.Web.Services.WebMethodAttribute(), System.Web.Script.Services.ScriptMethodAttribute()]
   public static string[] GetCompletionListIpAddress(string prefixText, int count, string contextKey)
   {
       Att_DeviceMaster  objDevice = new Att_DeviceMaster();
       DataTable dt = new DataView(objDevice.GetDeviceMaster(HttpContext.Current.Session["CompId"].ToString()), "IP_Address like '" + prefixText.ToString() + "%'", "", DataViewRowState.CurrentRows).ToTable();
       string[] txt = new string[dt.Rows.Count];

       for (int i = 0; i < dt.Rows.Count; i++)
       {
           txt[i] = dt.Rows[i]["IP_Address"].ToString();
       }
       return txt;
   }
}
