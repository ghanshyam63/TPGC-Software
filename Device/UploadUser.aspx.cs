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

public partial class Device_UploadUser : BasePage
{
    EmployeeMaster objEmp = new EmployeeMaster();
    Common cmn = new Common();
    Att_DeviceMaster objDevice = new Att_DeviceMaster();
    SystemParameter objSys = new SystemParameter();
    Device_Operation_Lan objDeviceOp = new Device_Operation_Lan();
    protected void Page_Load(object sender, EventArgs e)
    {
        if (Session["UserId"] == null)
        {
            Response.Redirect("~/ERPLogin.aspx");
        }
        Page.Title = objSys.GetSysTitle();
        if(!IsPostBack)
        {
        FillGrid();
        pnlFailedRec.Visible = false;
        pnlDestDevice.Visible = false;
        }
        AllPageCode();
    }

    public void AllPageCode()
    {

        Session["AccordianId"] = "14";
        Session["HeaderText"] = "Device Setup";
        DataTable dtAllPageCode = cmn.GetAllPagePermission(Session["UserId"].ToString(), "14", "79");
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
                    foreach (GridViewRow Row in gvEmp.Rows)
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
    protected void btnUploadUser_Click(object sender, EventArgs e)
    {
        DataTable dtuser = new DataTable();
        DataTable dtFailedRec = new DataTable();
        if (Session["dtEmpUpload"] != null)
        {


             dtuser = (DataTable)Session["dtEmpUpload"];
        }
        if (lblSelectRecd.Text != "")
        {
           

            dtuser = new DataView(dtuser, "Emp_Code in (" + lblSelectRecd.Text.Substring(0, lblSelectRecd.Text.Length - 1) + ")", "", DataViewRowState.CurrentRows).ToTable();

        }


        bool b = false;

        dtFailedRec = dtuser.Clone();
        bool IsDeviceSelected = false;

        foreach (GridViewRow gvdevicerow in gvDevice.Rows)
        {
            if (((CheckBox)gvdevicerow.FindControl("chkSelectDevice")).Checked)
            {
                DataTable dt = objDevice.GetDeviceMasterById(Session["CompId"].ToString(), gvDevice.DataKeys[gvdevicerow.RowIndex]["Device_Id"].ToString());

                string IP = dt.Rows[0]["IP_Address"].ToString();
                int port = Convert.ToInt32(dt.Rows[0]["Port"]);

                IsDeviceSelected = true;
                for (int i = 0; i < dtuser.Rows.Count;i++ )
                {
                    b = false;
                    DataTable dtAllUser = new DataTable();

                    dtAllUser.Columns.Add("Name");
                    dtAllUser.Columns.Add("UserID");
                    dtAllUser.Columns.Add("Privilege");
                    dtAllUser.Columns.Add("Password");
                    dtAllUser.Columns.Add("CardNumber");
                    dtAllUser.Columns.Add("Enabled");

                    DataRow dr = dtAllUser.NewRow();
                    dr["Name"]=dtuser.Rows[i]["Emp_Name"].ToString();
                    dr["UserID"]=dtuser.Rows[i]["Emp_Code"].ToString();
                    dr["Privilege"] = dtuser.Rows[i]["Template1"].ToString();

                    dr["Password"] = dtuser.Rows[i]["Template2"].ToString();
                    
                    
                    dr["CardNumber"] = dtuser.Rows[i]["CardNo"].ToString();



                    dr["Enabled"] = dtuser.Rows[i]["sEnabled"].ToString();

                    dtAllUser.Rows.Add(dr);

                    try
                    {
                     //   b = objDeviceOp.UploadCardUser(dtAllUser, IP, port);

                    }
                    catch
                    {

                    }
                   
                    DataTable dtFinger = new DataTable();

                    dtFinger.Columns.Add("sdwEnrollNumber");
                    dtFinger.Columns.Add("sName");
                    dtFinger.Columns.Add("idwFingerIndex");
                    dtFinger.Columns.Add("sTmpData");
                    dtFinger.Columns.Add("iPrivilege");
                   
                    dtFinger.Columns.Add("sPassword");
                    dtFinger.Columns.Add("sEnabled");
                 
                    dtFinger.Columns.Add("iFlag");
                   
                    DataRow dr1 = dtFinger.NewRow();
                   
                    dr1["sdwEnrollNumber"] = dtuser.Rows[i]["Emp_Code"].ToString();
                    dr1["sName"] = dtuser.Rows[i]["Emp_Name"].ToString();
                    dr1["idwFingerIndex"] = dtuser.Rows[i]["idwFingerIndex"].ToString();
                    dr1["sPassword"] = dtuser.Rows[i]["Template2"].ToString();



                    dr1["iPrivilege"] = dtuser.Rows[i]["Template1"].ToString();
                      dr1["sTmpData"] = dtuser.Rows[i]["Template3"].ToString();
                     
                      dr1["sEnabled"] = dtuser.Rows[i]["sEnabled"].ToString();
                      
                      dr1["iFlag"] = dtuser.Rows[i]["iFlag"].ToString();
                      dtFinger.Rows.Add(dr1);

                      try
                      {
                          if (dtuser.Rows[i]["Template3"].ToString() != "")
                          {
                              if(chkUploadFP.Checked)
                              {
                                  b = false;
                              b = objDeviceOp.UploadUser(dtFinger, IP, port);
                              }
                          }
                      }
                      catch
                      {

                      }
                    
                   
                    DataTable dtFace = new DataTable();
                    dtFace.Columns.Add("sUserID");
                    dtFace.Columns.Add("sName");
                    dtFace.Columns.Add("sPassword");
                    dtFace.Columns.Add("iPrivilege");
                    dtFace.Columns.Add("iFaceIndex");
                    dtFace.Columns.Add("sTmpData");
                    dtFace.Columns.Add("iLength");
                    dtFace.Columns.Add("bEnabled");

                 


                    DataRow dr2 = dtFace.NewRow();
                    dr2["sUserID"] = dtuser.Rows[i]["Emp_Code"].ToString();
                    dr2["sName"] = dtuser.Rows[i]["Emp_Name"].ToString();
                    
                    dr2["sPassword"] = dtuser.Rows[i]["Template2"].ToString();
                    dr2["iPrivilege"] = dtuser.Rows[i]["Template1"].ToString();
                    dr2["iFaceIndex"] = dtuser.Rows[i]["Template5"].ToString();
                    dr2["sTmpData"] = dtuser.Rows[i]["Template4"].ToString();
                    dr2["iLength"] = dtuser.Rows[i]["Template8"].ToString();
                    dr2["bEnabled"] = dtuser.Rows[i]["sEnabled"].ToString();
                   

                    dtFace.Rows.Add(dr2);
                    try
                    {
                        if (chkUploadFace.Checked)
                        {
                            b = false;
                            b = objDeviceOp.UploadUserFace(IP, port, dtFace);
                        }
                    }
                    catch
                    {
                    }


                   if (b == false)
                   {
                       dtFailedRec.ImportRow(dtuser.Rows[i]);

                   }
                }
            }

           

        }
       

        if (!IsDeviceSelected)
        {
            DisplayMessage("Please Select Device");

        }
        else
        {
            if (dtFailedRec.Rows.Count > 0)
            {
                pnlFailedRec.Visible = true;
                gvFailedRecord.DataSource = dtFailedRec;
                gvFailedRecord.DataBind();
                lblFailedRec.Text ="Failed Records :"+dtFailedRec.Rows.Count.ToString();
            }
            else
            {
                pnlDestDevice.Visible = false;
                pnlFailedRec.Visible = false;
                pnlList.Visible = true;
            }
            DisplayMessage("User Uploaded");
        }

    }
    protected void btnBackToList_Click(object sender, EventArgs e)
    {
        gvFailedRecord.DataSource = null;
        gvFailedRecord.DataBind();
        pnlDestDevice.Visible = false;
        pnlFailedRec.Visible = false;
        pnlList.Visible = true;
       
    }
    public void FillDeviceGrid()
    {
        DataTable dt = objDevice.GetDeviceMaster(Session["CompId"].ToString());
        dt = new DataView(dt, "Brand_Id='" + Session["BrandId"].ToString() + "' and Location_Id='" + Session["LocId"].ToString() + "'", "", DataViewRowState.CurrentRows).ToTable();

        gvDevice.DataSource = dt;
        gvDevice.DataBind();

        Session["dtFilter"] = dt;
        Session["Device"] = dt;


    }
    protected void chkSelAll_CheckedChanged1(object sender, EventArgs e)
    {
        bool b = ((CheckBox)sender).Checked;
        foreach (GridViewRow gvrow in gvDevice.Rows)
        {
            ((CheckBox)gvrow.FindControl("chkSelectDevice")).Checked = b;
        }
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
  
    protected void btnNext_Click(object sender, EventArgs e)
    {


        if (lblSelectRecd.Text == "")
        {
            DisplayMessage("Please select at least one employee");
            return;
        }
        else
        {
            pnlDestDevice.Visible = true;
            pnlList.Visible = false;


            DataTable dt = objDevice.GetDeviceMaster(Session["CompId"].ToString());
            dt = new DataView(dt, "Brand_Id='" + Session["BrandId"].ToString() + "' and Location_Id='" + Session["LocId"].ToString() + "'", "", DataViewRowState.CurrentRows).ToTable();

            if(dt.Rows.Count >0)
            {
            gvDevice.DataSource = dt;
            gvDevice.DataBind();
          
            }
            else
            {
                DisplayMessage("No device exists");
            }
           
           


        }
    }

    public void DisplayMessage(string str)
    {
        ScriptManager.RegisterStartupScript(this, GetType(), "", "alert('" + str + "');", true);
    }
    protected void btnBack_Click(object sender, EventArgs e)
    {
        pnlDestDevice.Visible = false;
        pnlFailedRec.Visible = false;
        pnlList.Visible = true;
        chkUploadFace.Checked = false;
        chkUploadFP.Checked = false;
        FillGrid();
        lblSelectRecd.Text = "";

    }
    public void FillGrid()
    {
        DataTable dtEmp = objEmp.GetEmployeeMasterWithDeviceData(Session["CompId"].ToString());

        dtEmp = new DataView(dtEmp, "Brand_Id='" + Session["BrandId"].ToString() + "'  and Location_Id='" + Session["LocId"].ToString() + "'", "", DataViewRowState.CurrentRows).ToTable();

        if (Session["SessionDepId"] != null)
        {
            dtEmp = new DataView(dtEmp, "Department_Id in(" + Session["SessionDepId"].ToString().Substring(0, Session["SessionDepId"].ToString().Length - 1) + ")", "", DataViewRowState.CurrentRows).ToTable();

        }
        if (dtEmp.Rows.Count > 0)
        {
            Session["dtEmpUpload"] = dtEmp;
            gvEmp.DataSource = dtEmp;
            gvEmp.DataBind();
            lblTotalRecords.Text = Resources.Attendance.Total_Records + " : " + dtEmp.Rows.Count.ToString() + "";

        }
    }

    protected void btnbind_Click(object sender, ImageClickEventArgs e)
    {
        if (ddlOption1.SelectedIndex != 0)
        {
            string condition = string.Empty;
            if (ddlOption1.SelectedIndex == 1)
            {
                condition = "convert(" + ddlField1.SelectedValue + ",System.String)='" + txtValue1.Text.Trim() + "'";
            }
            else if (ddlOption1.SelectedIndex == 2)
            {
                condition = "convert(" + ddlField1.SelectedValue + ",System.String) like '%" + txtValue1.Text.Trim() + "%'";
            }
            else
            {
                condition = "convert(" + ddlField1.SelectedValue + ",System.String) Like '" + txtValue1.Text.Trim() + "%'";

            }
            DataTable dtEmp = (DataTable)Session["dtEmpUpload"];
            DataView view = new DataView(dtEmp, condition, "", DataViewRowState.CurrentRows);
            gvEmp.DataSource = view.ToTable();
            gvEmp.DataBind();
            Session["dtEmpUpload"] = view.ToTable();
            lblTotalRecords.Text = Resources.Attendance.Total_Records + " : " + view.ToTable().Rows.Count.ToString() + "";


        }
       
       
    }
    protected void btnRefresh_Click(object sender, ImageClickEventArgs e)
    {
        lblSelectRecd.Text = "";
        ddlField1.SelectedIndex = 1;
        ddlOption1.SelectedIndex = 2;
        txtValue1.Text = "";
        FillGrid();
       
        
       
    }
    protected void gvEmp_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        gvEmp.PageIndex = e.NewPageIndex;
        DataTable dtEmp = (DataTable)Session["dtEmpUpload"];
        gvEmp.DataSource = dtEmp;
        gvEmp.DataBind();
        string temp = string.Empty;
      

        for (int i = 0; i < gvEmp.Rows.Count; i++)
        {
            Label lblconid = (Label)gvEmp.Rows[i].FindControl("lblEmpId");
            string[] split = lblSelectRecd.Text.Split(',');

            for (int j = 0; j < lblSelectRecd.Text.Split(',').Length; j++)
            {
                if (lblSelectRecd.Text.Split(',')[j] != "")
                {
                    if ("'"+lblconid.Text.Trim().ToString()+"'" == lblSelectRecd.Text.Split(',')[j].Trim().ToString())
                    {
                        ((CheckBox)gvEmp.Rows[i].FindControl("chkgvSelect")).Checked = true;
                    }
                }
            }
        }
     
        
       
    }
    protected void chkgvSelectAll_CheckedChanged(object sender, EventArgs e)
    {
        CheckBox chkSelAll = ((CheckBox)gvEmp.HeaderRow.FindControl("chkgvSelectAll"));
        for (int i = 0; i < gvEmp.Rows.Count; i++)
        {
            ((CheckBox)gvEmp.Rows[i].FindControl("chkgvSelect")).Checked = chkSelAll.Checked;
            if (chkSelAll.Checked)
            {
                if (!lblSelectRecd.Text.Split(',').Contains(((Label)(gvEmp.Rows[i].FindControl("lblEmpId"))).Text.Trim().ToString()))
                {
                    lblSelectRecd.Text += "'"+((Label)(gvEmp.Rows[i].FindControl("lblEmpId"))).Text.Trim().ToString() +"'" +",";
                }
            }
            else
            {
                string temp = string.Empty;
                string[] split = lblSelectRecd.Text.Split(',');
                foreach (string item in split)
                {
                    if (item != ((Label)(gvEmp.Rows[i].FindControl("lblEmpId"))).Text.Trim().ToString())
                    {
                        if (item != "")
                        {
                            temp += item + ",";
                        }
                    }
                }
                lblSelectRecd.Text = temp;
            }
        }
      
    }
    protected void ImgbtnSelectAll_Click(object sender, ImageClickEventArgs e)
    {
        DataTable dtProduct = (DataTable)Session["dtEmpUpload"];

        if (ViewState["Select"] == null)
        {
            ViewState["Select"] = 1;
            foreach (DataRow dr in dtProduct.Rows)
            {
                if (!lblSelectRecd.Text.Split(',').Contains("'"+dr["Emp_Code"]+"'"))
                {
                    lblSelectRecd.Text += "'"+dr["Emp_Code"] +"'"+ ",";
                }
            }
            for (int i = 0; i < gvEmp.Rows.Count; i++)
            {
                string[] split = lblSelectRecd.Text.Split(',');
                Label lblconid = (Label)gvEmp.Rows[i].FindControl("lblEmpId");
                for (int j = 0; j < lblSelectRecd.Text.Split(',').Length; j++)
                {
                    if (lblSelectRecd.Text.Split(',')[j] != "")
                    {
                        if ("'"+lblconid.Text.Trim().ToString()+"'" == lblSelectRecd.Text.Split(',')[j].Trim().ToString())
                        {
                            ((CheckBox)gvEmp.Rows[i].FindControl("chkgvSelect")).Checked = true;
                        }
                    }
                }
            }
        }
        else
        {
            lblSelectRecd.Text = "";
            DataTable dtProduct1 = (DataTable)Session["dtEmpUpload"];
            gvEmp.DataSource = dtProduct1;
            gvEmp.DataBind();
            ViewState["Select"] = null;
        }
       
    }

    protected void chkgvSelect_CheckedChanged(object sender, EventArgs e)
    {
        string empidlist = string.Empty;
        string temp = string.Empty;
        int index = ((GridViewRow)((CheckBox)sender).Parent.Parent).RowIndex;
        Label lb = (Label)gvEmp.Rows[index].FindControl("lblEmpId");
        if (((CheckBox)gvEmp.Rows[index].FindControl("chkgvSelect")).Checked)
        {
            empidlist +="'"+ lb.Text.Trim().ToString() +"'"+ ",";
            if (!lblSelectRecd.Text.Contains(empidlist))
            {
            lblSelectRecd.Text += empidlist;
            }
        }

        else
        {

            empidlist +="'"+ lb.Text.ToString().Trim()+"'";
            lblSelectRecd.Text += empidlist;
            string[] split = lblSelectRecd.Text.Split(',');
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
            lblSelectRecd.Text = temp;
        }
      

    }

}
