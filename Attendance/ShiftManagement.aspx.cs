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

public partial class Attendance_ShiftManagement : BasePage
{
    Set_ApplicationParameter objparam = new Set_ApplicationParameter();
    Common cmn = new Common();
    SystemParameter objSys = new SystemParameter();
    Att_ShiftManagement objShift = new Att_ShiftManagement();
    Att_TimeTable objTimeTable = new Att_TimeTable();
    Att_ShiftDescription objShiftDesc = new Att_ShiftDescription();

    Att_ScheduleMaster objSch = new Att_ScheduleMaster();
    protected void Page_Load(object sender, EventArgs e)
    {


        if (Session["UserId"] == null)
        {
            Response.Redirect("~/ERPLogin.aspx");
        }
        AllPageCode();
        if (!IsPostBack)
        {
            txtValue.Focus();

            FillGridBin();
            FillGrid();

            btnList_Click(null, null);
            btnAddTime.Visible = false;
            btnClearAll.Visible = false;
            btnDelete.Visible = false;
           
          
        }
        CalendarExtender2.Format = objSys.SetDateFormat();
    }

    public void AllPageCode()
    {
        Page.Title = objSys.GetSysTitle();

        Session["AccordianId"] = "9";
        Session["HeaderText"] = "Attendance Setup";
        DataTable dtAllPageCode = cmn.GetAllPagePermission(Session["UserId"].ToString(), "9", "42");
        if (dtAllPageCode.Rows.Count != 0)
        {
            if (dtAllPageCode.Rows[0][0].ToString() == "SuperAdmin")
            {
                btnSave.Visible = true;
                foreach (GridViewRow Row in gvShift.Rows)
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
                    foreach (GridViewRow Row in gvShift.Rows)
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

    public string GetDate(object obj)
    {

        DateTime Date = new DateTime();
        Date = Convert.ToDateTime(obj.ToString());



        return Date.ToString(objSys.SetDateFormat());

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




    protected void txtShiftName_OnTextChanged(object sender, EventArgs e)
    {

        if (editid.Value == "")
        {
            DataTable dt = objShift.GetShiftMasterByShiftName(Session["CompId"].ToString().ToString(), txtShiftName.Text.Trim());
            if (dt.Rows.Count > 0)
            {
                txtShiftName.Text = "";
                DisplayMessage("Shift Name Already Exists");
                System.Web.UI.ScriptManager.GetCurrent(this).SetFocus(txtShiftName);
                return;
            }
            DataTable dt1 = objShift.GetShiftMasterInactive(Session["CompId"].ToString().ToString());
            dt1 = new DataView(dt1, "Shift_Name='" + txtShiftName.Text + "'", "", DataViewRowState.CurrentRows).ToTable();
            if (dt1.Rows.Count > 0)
            {
                txtShiftName.Text = "";
                DisplayMessage("Shift Name Already Exists - Go to Bin Tab");
                System.Web.UI.ScriptManager.GetCurrent(this).SetFocus(txtShiftName);
                return;
            }
            txtShiftNameL.Focus();
        }
        else
        {
            DataTable dtTemp = objShift.GetShiftMasterById(Session["CompId"].ToString().ToString(), editid.Value);
            if (dtTemp.Rows.Count > 0)
            {
                if (dtTemp.Rows[0]["Shift_Name"].ToString() != txtShiftName.Text)
                {
                    DataTable dt = objShift.GetShiftMaster(Session["CompId"].ToString().ToString());
                    dt = new DataView(dt, "Shift_Name='" + txtShiftName.Text + "'", "", DataViewRowState.CurrentRows).ToTable();
                    if (dt.Rows.Count > 0)
                    {
                        txtShiftName.Text = "";
                        DisplayMessage("Shift Name Already Exists");
                        System.Web.UI.ScriptManager.GetCurrent(this).SetFocus(txtShiftName);
                        return;
                    }
                    DataTable dt1 = objShift.GetShiftMaster(Session["CompId"].ToString().ToString());
                    dt1 = new DataView(dt1, "Shift_Name='" + txtShiftName.Text + "'", "", DataViewRowState.CurrentRows).ToTable();
                    if (dt1.Rows.Count > 0)
                    {
                        txtShiftName.Text = "";
                        DisplayMessage("Shift Name Already Exists - Go to Bin Tab");
                        System.Web.UI.ScriptManager.GetCurrent(this).SetFocus(txtShiftName);
                        return;
                    }
                }
            }
            txtShiftNameL.Focus();
        }
    }
  
    protected void btnNew_Click(object sender, EventArgs e)
    {
        txtShiftName.Focus();
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
        PnlViewShift.Visible = false;
        FillGridBin();
    }

    protected void btnRefresh_Click(object sender, ImageClickEventArgs e)
    {
        FillGrid();
        FillGridBin();

        ddlOption.SelectedIndex = 2;
        ddlFieldName.SelectedIndex = 2;
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
            DataTable dtCust = (DataTable)Session["Shift"];


            DataView view = new DataView(dtCust, condition, "", DataViewRowState.CurrentRows);
            Session["dtFilter"] = view.ToTable();
            lblTotalRecords.Text = Resources.Attendance.Total_Records + ": " + view.ToTable().Rows.Count.ToString() + "";

            gvShift.DataSource = view.ToTable();
            gvShift.DataBind();
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
            DataTable dtCust = (DataTable)Session["dtbinShift"];


            DataView view = new DataView(dtCust, condition, "", DataViewRowState.CurrentRows);
            Session["dtbinFilter"] = view.ToTable();
            lblbinTotalRecords.Text = Resources.Attendance.Total_Records + ": " + view.ToTable().Rows.Count.ToString() + "";

            gvShiftBin.DataSource = view.ToTable();
            gvShiftBin.DataBind();


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

    protected void gvShiftBin_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {

        gvShiftBin.PageIndex = e.NewPageIndex;
        if (HDFSortbin.Value == "")
            FillGridBin();
        else
        {
            DataTable dt = (DataTable)Session["dtbinFilter"];
            gvShiftBin.DataSource = dt;
            gvShiftBin.DataBind();
            AllPageCode();
        }
        string temp = string.Empty;
        bool isselcted;

        for (int i = 0; i < gvShiftBin.Rows.Count; i++)
        {
            Label lblconid = (Label)gvShiftBin.Rows[i].FindControl("lblShiftId");
            string[] split = lblSelectedRecord.Text.Split(',');

            for (int j = 0; j < lblSelectedRecord.Text.Split(',').Length; j++)
            {
                if (lblSelectedRecord.Text.Split(',')[j] != "")
                {
                    if (lblconid.Text.Trim().ToString() == lblSelectedRecord.Text.Split(',')[j].Trim().ToString())
                    {
                        ((CheckBox)gvShiftBin.Rows[i].FindControl("chkgvSelect")).Checked = true;
                    }
                }
            }
        }

    }

    protected void gvShiftView_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        gvShiftView.PageIndex = e.NewPageIndex;

        DataTable dt = (DataTable)Session["ViewShiftDt"];
        if (dt.Rows.Count > 0)
        {
            gvShiftView.DataSource = dt;
            gvShiftView.DataBind();

        }


    }

    protected void gvShiftBin_OnSorting(object sender, GridViewSortEventArgs e)
    {
        lblSelectedRecord.Text = "";
        HDFSortbin.Value = HDFSortbin.Value == "ASC" ? "DESC" : "ASC";
        DataTable dt = new DataTable();
        dt = objShift.GetShiftMasterInactive(Session["CompId"].ToString());
        DataView dv = new DataView(dt);
        string Query = "" + e.SortExpression + " " + HDFSortbin.Value + "";
        dv.Sort = Query;
        dt = dv.ToTable();
        Session["dtbinFilter"] = dt;
        gvShiftBin.DataSource = dt;
        gvShiftBin.DataBind();
        AllPageCode();

    }

    protected void btnBack_Click(object sender, EventArgs e)
    {
        PanelShiftAss.Visible = false;

    }
    protected void btnOk_Click(object sender, EventArgs e)
    {
        int TransId = 0;

        DataTable dtScatch = (DataTable)Session["dtScatch"];
        int flag = 0;

        for (int j = 0; j < chkTimeTableList.Items.Count; j++)
        {

            if (chkTimeTableList.Items[j].Selected)
            {
                flag = 1;

            }

        }


        if (flag == 0)
        {

            DisplayMessage("Please select a Time Table");


        }
        int flag1 = 0;

        for (int j = 0; j < chkDayUnderPeriod.Items.Count; j++)
        {

            if (chkDayUnderPeriod.Items[j].Selected)
            {
                flag1 = 1;

            }

        }


        if (flag1 == 0)
        {

            DisplayMessage("Please select a Day for Time Table");


        }
        string strMsg = string.Empty;

        DataTable dtShiftDes = objShiftDesc.GetShiftDescriptionByShiftId(editid.Value);

        if (dtShiftDes.Rows.Count == 0)
        {
            for (int i = 0; i < chkDayUnderPeriod.Items.Count; i++)
            {


                string CycleType = string.Empty;
                string CycleDay = string.Empty;

                if (ddlCycleUnit.SelectedIndex == 1)
                {
                    CycleType = chkDayUnderPeriod.Items[i].Value.Split('-')[0].ToString();
                    CycleDay = chkDayUnderPeriod.Items[i].Value.Split('-')[1].ToString();

                }
                else
                {
                    CycleType = chkDayUnderPeriod.Items[i].Value.Split('-')[0].ToString() + "-" + chkDayUnderPeriod.Items[i].Value.Split('-')[1].ToString();
                    CycleDay = chkDayUnderPeriod.Items[i].Value.Split('-')[2].ToString();
                }
                if (chkDayUnderPeriod.Items[i].Selected == false)
                {
                    for (int j = 0; j < chkTimeTableList.Items.Count; j++)
                    {

                        if (chkTimeTableList.Items[j].Selected)
                        {

                            DataTable dt = objShiftDesc.GetShiftDescriptionByShiftId(editid.Value);

                            dt = new DataView(dt, "Cycle_Type='" + CycleType + "' and Cycle_Day='" + CycleDay + "'", "", DataViewRowState.CurrentRows).ToTable();

                            if(dt.Rows.Count ==0)
                            {
                           // TransId=objShiftDesc.InsertShiftDescription(editid.Value, CycleType, CycleDay,"", "", "", "", "", true.ToString(), DateTime.Now.ToString(), true.ToString(), Session["UserId"].ToString(), DateTime.Now.ToString(), Session["UserId"].ToString(), DateTime.Now.ToString());
                            
                           // objShiftDesc.InsertShift_TimeTable(TransId.ToString(), chkTimeTableList.Items[j].Value, "", "", "", "", "", true.ToString(), DateTime.Now.ToString(), true.ToString(), Session["UserId"].ToString(), DateTime.Now.ToString(), Session["UserId"].ToString(), DateTime.Now.ToString());

                            }

                                        }
                    }


                }
                else
                {
                    for (int j = 0; j < chkTimeTableList.Items.Count; j++)
                    {
                        string Value = chkTimeTableList.Items[j].Value.ToString();
                        if (chkTimeTableList.Items[j].Selected)
                        {
                            DataTable dtShift = objShiftDesc.GetShiftDescriptionByShiftId(editid.Value);

                            dtShift = new DataView(dtShift, "Cycle_Type='" + CycleType + "' and Cycle_Day = '" + CycleDay + "'", "", DataViewRowState.CurrentRows).ToTable();

                            DataTable dtDisTimeTable = dtShift.DefaultView.ToTable(true, "TimeTable_Id");

                            if (dtDisTimeTable.Rows.Count > 0)
                            {
                                int f = 0;
                                for (int k = 0; k < dtDisTimeTable.Rows.Count; k++)
                                {
                                    DataTable dtin = objTimeTable.GetTimeTableMasterById(Session["CompId"].ToString(), dtDisTimeTable.Rows[k]["TimeTable_Id"].ToString());
                                    DateTime dtintime = Convert.ToDateTime(dtin.Rows[0]["OnDuty_Time"]);
                                    DateTime dtouttime = Convert.ToDateTime(dtin.Rows[0]["OffDuty_Time"]);


                                    DataTable dtin1 = objTimeTable.GetTimeTableMasterById(Session["CompId"].ToString(), chkTimeTableList.Items[j].Value);
                                    DateTime dtintime1 = Convert.ToDateTime(dtin1.Rows[0]["OnDuty_Time"]);
                                    DateTime dtouttime1 = Convert.ToDateTime(dtin1.Rows[0]["OffDuty_Time"]);

                                    if (ISOverLapTimeTable(dtintime1, dtouttime1, dtintime, dtouttime))
                                    {

                                        f = 1;
                                        strMsg += chkDayUnderPeriod.Items[i].Text + ",";
                                    }
                                    else
                                    {


                                    }

                                }
                                if (f == 0)
                                {
                                    TransId=int.Parse(dtShift.Rows[0]["Trans_Id"].ToString());
                                    //TransId = objShiftDesc.InsertShiftDescription(editid.Value, CycleType, CycleDay, "", "", "", "", "", true.ToString(), DateTime.Now.ToString(), true.ToString(), Session["UserId"].ToString(), DateTime.Now.ToString(), Session["UserId"].ToString(), DateTime.Now.ToString());
                                    objShiftDesc.InsertShift_TimeTable(TransId.ToString(), chkTimeTableList.Items[j].Value, "", "", "", "", "", true.ToString(), DateTime.Now.ToString(), true.ToString(), Session["UserId"].ToString(), DateTime.Now.ToString(), Session["UserId"].ToString(), DateTime.Now.ToString());

                                }

                            }

                            else
                            {

                                TransId=objShiftDesc.InsertShiftDescription(editid.Value, CycleType, CycleDay, "", "", "", "", "", true.ToString(), DateTime.Now.ToString(), true.ToString(), Session["UserId"].ToString(), DateTime.Now.ToString(), Session["UserId"].ToString(), DateTime.Now.ToString());
                                objShiftDesc.InsertShift_TimeTable(TransId.ToString(), chkTimeTableList.Items[j].Value, "", "", "", "", "", true.ToString(), DateTime.Now.ToString(), true.ToString(), Session["UserId"].ToString(), DateTime.Now.ToString(), Session["UserId"].ToString(), DateTime.Now.ToString());


                            }

                        }



                    }



                }



            }


        }
        else
        {
            DataTable dtDisTimeTable = new DataTable();
            for (int i = 0; i < chkDayUnderPeriod.Items.Count; i++)
            {

                string CycleType = string.Empty;
                string CycleDay = string.Empty;
                if (ddlCycleUnit.SelectedIndex == 1)
                {
                    CycleType = chkDayUnderPeriod.Items[i].Value.Split('-')[0].ToString();
                    CycleDay = chkDayUnderPeriod.Items[i].Value.Split('-')[1].ToString();

                }
                else
                {
                    CycleType = chkDayUnderPeriod.Items[i].Value.Split('-')[0].ToString() + "-" + chkDayUnderPeriod.Items[i].Value.Split('-')[1].ToString();
                    CycleDay = chkDayUnderPeriod.Items[i].Value.Split('-')[2].ToString();
                }
                if (chkDayUnderPeriod.Items[i].Selected)
                {
                    for (int j = 0; j < chkTimeTableList.Items.Count; j++)
                    {
                        if (chkTimeTableList.Items[j].Selected)
                        {

                            DataTable dtShift = objShiftDesc.GetShiftDescriptionByShiftId(editid.Value);

                            dtShift = new DataView(dtShift, "Cycle_Type = '" + CycleType + "'  and Cycle_Day='" + CycleDay + "' and convert(TimeTable_Id,System.String) <> ''", "", DataViewRowState.CurrentRows).ToTable();


                             dtDisTimeTable = dtShift.DefaultView.ToTable(true, "TimeTable_Id");

                            if (dtDisTimeTable.Rows.Count > 0)
                            {

                                for (int k = 0; k < dtDisTimeTable.Rows.Count; k++)
                                {
                                    DataTable dtin = objTimeTable.GetTimeTableMasterById(Session["CompId"].ToString(), dtDisTimeTable.Rows[k]["TimeTable_Id"].ToString());
                                    DateTime dtintime = Convert.ToDateTime(dtin.Rows[0]["OnDuty_Time"]);
                                    DateTime dtouttime = Convert.ToDateTime(dtin.Rows[0]["OffDuty_Time"]);


                                    DataTable dtin1 = objTimeTable.GetTimeTableMasterById(Session["CompId"].ToString(), chkTimeTableList.Items[j].Value);
                                    DateTime dtintime1 = Convert.ToDateTime(dtin1.Rows[0]["OnDuty_Time"]);
                                    DateTime dtouttime1 = Convert.ToDateTime(dtin1.Rows[0]["OffDuty_Time"]);

                                    DataTable dt1 = new DataView(dtShift, "TimeTable_Id='" + dtDisTimeTable.Rows[k]["TimeTable_Id"].ToString() + "'", "", DataViewRowState.CurrentRows).ToTable();
                                    if (dt1.Rows.Count > 0)
                                    {

                                        if (ISOverLapTimeTable(dtintime1, dtouttime1, dtintime, dtouttime))
                                        {
                                            strMsg += chkDayUnderPeriod.Items[i].Text + ",";
                                            break;

                                        }
                                        else
                                        {
                                            dtShift = objShiftDesc.GetShiftDescriptionByShiftId(editid.Value);

                                            dtShift = new DataView(dtShift, "Cycle_Type = '" + CycleType + "'  and Cycle_Day='" + CycleDay + "'", "", DataViewRowState.CurrentRows).ToTable();



                                            DataTable dtDisTimeTable1 = new DataTable();
                                            dtDisTimeTable1 = dtShift.DefaultView.ToTable(true, "TimeTable_Id");

                                            DataTable dtTimeTab = new DataTable();
                                            dtTimeTab = new DataView(dtDisTimeTable1, "TimeTable_Id='" + chkTimeTableList.Items[j].Value + "'", "", DataViewRowState.CurrentRows).ToTable();

                                            if (dtTimeTab.Rows.Count == 0)
                                            {
                                                TransId =int.Parse(dtShift.Rows[0]["Trans_Id"].ToString());
                                                //TransId = objShiftDesc.InsertShiftDescription(editid.Value, CycleType, CycleDay, "", "", "", "", "", true.ToString(), DateTime.Now.ToString(), true.ToString(), Session["UserId"].ToString(), DateTime.Now.ToString(), Session["UserId"].ToString(), DateTime.Now.ToString());
                                                objShiftDesc.InsertShift_TimeTable(TransId.ToString(), chkTimeTableList.Items[j].Value, "", "", "", "", "", true.ToString(), DateTime.Now.ToString(), true.ToString(), Session["UserId"].ToString(), DateTime.Now.ToString(), Session["UserId"].ToString(), DateTime.Now.ToString());


                                            }




                                        }
                                    }

                                }


                            //if (dtShift.Rows.Count > 0)
                            //{
                            //    for (int m = 0; m < dtShift.Rows.Count; m++)
                            //    {
                            //        objShiftDesc.DeleteShiftDescriptionByTransId(dtShift.Rows[m]["Trans_Id"].ToString());
                            //        objShiftDesc.DeleteShift_TimeTableByRefId(dtShift.Rows[m]["Trans_Id"].ToString());

                            //    }

                            //   TransId= objShiftDesc.InsertShiftDescription(editid.Value, CycleType, CycleDay, "", "", "", "", "", true.ToString(), DateTime.Now.ToString(), true.ToString(), Session["UserId"].ToString(), DateTime.Now.ToString(), Session["UserId"].ToString(), DateTime.Now.ToString());
                            //    objShiftDesc.InsertShift_TimeTable(TransId.ToString(), chkTimeTableList.Items[j].Value, "", "", "", "", "", true.ToString(), DateTime.Now.ToString(), true.ToString(), Session["UserId"].ToString(), DateTime.Now.ToString(), Session["UserId"].ToString(), DateTime.Now.ToString());

                            }
                            else
                            {

                                dtShift = objShiftDesc.GetShiftDescriptionByShiftId(editid.Value);
                                dtShift = new DataView(dtShift, "Cycle_Type='" + CycleType + "' and Cycle_Day = '" + CycleDay + "' and  convert(TimeTable_Id,System.String) <> '' ", "", DataViewRowState.CurrentRows).ToTable();
                                 dtDisTimeTable = dtShift.DefaultView.ToTable(true, "TimeTable_Id");

                                if (dtDisTimeTable.Rows.Count > 0)
                                {

                                    for (int k = 0; k < dtDisTimeTable.Rows.Count; k++)
                                    {
                                        DataTable dtin = objTimeTable.GetTimeTableMasterById(Session["CompId"].ToString(), dtDisTimeTable.Rows[k]["TimeTable_Id"].ToString());
                                        DateTime dtintime = Convert.ToDateTime(dtin.Rows[0]["OnDuty_Time"]);
                                        DateTime dtouttime = Convert.ToDateTime(dtin.Rows[0]["OffDuty_Time"]);


                                        DataTable dtin1 = objTimeTable.GetTimeTableMasterById(Session["CompId"].ToString(), chkTimeTableList.Items[j].Value);
                                        DateTime dtintime1 = Convert.ToDateTime(dtin1.Rows[0]["OnDuty_Time"]);
                                        DateTime dtouttime1 = Convert.ToDateTime(dtin1.Rows[0]["OffDuty_Time"]);

                                        DataTable dt1 = new DataView(dtShift, "TimeTable_Id='" + dtDisTimeTable.Rows[k]["TimeTable_Id"].ToString() + "'", "", DataViewRowState.CurrentRows).ToTable();
                                        if (dt1.Rows.Count > 0)
                                        {

                                            if (ISOverLapTimeTable(dtintime1, dtouttime1, dtintime, dtouttime))
                                            {
                                                strMsg += chkDayUnderPeriod.Items[i].Text + ",";
                                                break;

                                            }
                                            else
                                            {
                                                dtShift = objShiftDesc.GetShiftDescriptionByShiftId(editid.Value);

                                                dtShift = new DataView(dtShift, "Cycle_Type = '" + CycleType + "'  and Cycle_Day='" + CycleDay + "'", "", DataViewRowState.CurrentRows).ToTable();



                                                DataTable dtDisTimeTable1 = new DataTable();
                                                dtDisTimeTable1 = dtShift.DefaultView.ToTable(true, "TimeTable_Id");

                                                DataTable dtTimeTab = new DataTable();
                                                dtTimeTab = new DataView(dtDisTimeTable1, "TimeTable_Id='" + chkTimeTableList.Items[j].Value + "'", "", DataViewRowState.CurrentRows).ToTable();

                                                if (dtTimeTab.Rows.Count == 0)
                                                {
                                                    TransId = int.Parse(dtShift.Rows[0]["Trans_Id"].ToString());

                                                   //TransId= objShiftDesc.InsertShiftDescription(editid.Value, CycleType, CycleDay, "", "", "", "", "", true.ToString(), DateTime.Now.ToString(), true.ToString(), Session["UserId"].ToString(), DateTime.Now.ToString(), Session["UserId"].ToString(), DateTime.Now.ToString());
                                                    objShiftDesc.InsertShift_TimeTable(TransId.ToString(), chkTimeTableList.Items[j].Value, "", "", "", "", "", true.ToString(), DateTime.Now.ToString(), true.ToString(), Session["UserId"].ToString(), DateTime.Now.ToString(), Session["UserId"].ToString(), DateTime.Now.ToString());


                                                }




                                            }
                                        }

                                    }

                                }

                                else
                                {
                                    dtShift = objShiftDesc.GetShiftDescriptionByShiftId(editid.Value);

                                    dtShift = new DataView(dtShift, "Cycle_Type = '" + CycleType + "'  and Cycle_Day='" + CycleDay + "'", "", DataViewRowState.CurrentRows).ToTable();

                                    if (dtShift.Rows.Count == 0)
                                    {
                                        TransId = objShiftDesc.InsertShiftDescription(editid.Value, CycleType, CycleDay, "", "", "", "", "", true.ToString(), DateTime.Now.ToString(), true.ToString(), Session["UserId"].ToString(), DateTime.Now.ToString(), Session["UserId"].ToString(), DateTime.Now.ToString());

                                        objShiftDesc.InsertShift_TimeTable(TransId.ToString(), chkTimeTableList.Items[j].Value, "", "", "", "", "", true.ToString(), DateTime.Now.ToString(), true.ToString(), Session["UserId"].ToString(), DateTime.Now.ToString(), Session["UserId"].ToString(), DateTime.Now.ToString());


                                    }
                                    else
                                    {
                                        TransId = int.Parse(dtShift.Rows[0]["Trans_Id"].ToString());

                                        objShiftDesc.InsertShift_TimeTable(TransId.ToString(), chkTimeTableList.Items[j].Value, "", "", "", "", "", true.ToString(), DateTime.Now.ToString(), true.ToString(), Session["UserId"].ToString(), DateTime.Now.ToString(), Session["UserId"].ToString(), DateTime.Now.ToString());

                                    }
                               
                                }


                            }

                        }


                    }

                }
            }


        }








        if (editid.Value == "")
        {

            
                DisplayMessage("Record Saved");
            
        }

        else
        {
            if (strMsg != "")
            {
                DisplayMessage(strMsg + " " + "Overlapped Days!");
            }
            else
            {
                DisplayMessage("Record Updated");
            }
        }

        Button1_Click(null, null);

    }
    protected void btnCancelPanel_Click1(object sender, EventArgs e)
    {





        PanelShiftAss.Visible = false;
        TrShiftName.Visible = false;
        PanView.Visible = false;
        ViewShift(editid.Value);


        PanelShiftAss.Visible = false;
        TrShiftName.Visible = false;
        pnlShiftNew.Visible = true;
    }
    protected void Button1_Click(object sender, EventArgs e)
    {
        for (int t = 0; t < chkTimeTableList.Items.Count; t++)
        {
            chkTimeTableList.Items[t].Selected = false;
        }
        for (int j = 0; j < chkDayUnderPeriod.Items.Count; j++)
        {
            chkDayUnderPeriod.Items[j].Selected = false;
        }

    }
    protected void btnNext_Click(object sender, EventArgs e)
    {
        if (chkTimeTableList.SelectedItem == null)
        {

            DisplayMessage("Select Atleast One TimeTable");
            //btnshowpopup_ModalPopupExtender.Show();
            return;
        }
        else
        {
            PanelShiftAss.Visible = false;
        }
    }
    protected void Button2_Click(object sender, EventArgs e)
    {
        DataTable dttimetable1 = objShiftDesc.GetShiftDescriptionByShiftId(editid.Value);

        if (dttimetable1.Rows.Count == 0)
        {
            DisplayMessage("Please Save Record First");
            return;

        }

        DataTable dt = objTimeTable.GetTimeTableMaster(Session["CompId"].ToString());

        DataTable dtTime1 = new DataTable();
        dtTime1.Columns.Add("EDutyTime");
        dtTime1.Columns.Add("TimeTableId");

        for (int i = 0; i < dt.Rows.Count; i++)
        {
            DataRow dr = dtTime1.NewRow();

            dr["EDutyTime"] = dt.Rows[i]["ETimeTableName"] + "(" + Convert.ToDateTime(dt.Rows[i]["OnDutyTime"]).ToString("HH:mm") + "-" + Convert.ToDateTime(dt.Rows[i]["OffDutyTime"]).ToString("HH:mm") + ")";
            dr["TimeTableId"] = dt.Rows[i]["TimeTableId"].ToString();
            dtTime1.Rows.Add(dr);

        }


        chkTimeTableList.DataSource = dtTime1;
        chkTimeTableList.DataTextField = "EDutyTime";
        chkTimeTableList.DataValueField = "TimeTableId";
        chkTimeTableList.DataBind();

        editid.Value = txtShiftId.Text;




        GetDays();

        foreach (DataListItem dl in dlView.Items)
        {
            DataList GvTime = (DataList)dl.FindControl("GvTime");

            Label lblDay = (Label)dl.FindControl("lblDays");
            Label lblTime = (Label)dl.FindControl("lblTime");

            string strDaysValue = "";
            strDaysValue = lblDay.Text.Split(',')[0];


            if (strDaysValue == "Thu ")
            {
                strDaysValue = "Thursday";
            }
            else if (strDaysValue == "Fri ")
            {
                strDaysValue = "Friday";
            }
            else if (strDaysValue == "Sat ")
            {
                strDaysValue = "Saturday";
            }
            else if (strDaysValue == "Sun ")
            {
                strDaysValue = "Sunday";
            }
            else if (strDaysValue == "Mon ")
            {
                strDaysValue = "Monday";
            }
            else if (strDaysValue == "Tue ")
            {
                strDaysValue = "Tuesday";
            }
            else if (strDaysValue == "Wed ")
            {
                strDaysValue = "Wednesday";
            }


            //DataTable dtTime = objShiftDesc.GetShiftDiscriptionByVal(editid.Value, strDaysValue);
            //if (dtTime.Rows.Count > 0)
            //{
            //    GvTime.DataSource = dtTime;
            //    GvTime.DataBind();
            //}
        }






        Session["IsView"] = true;
        PanView.Visible = true;



        DataTable dttimetable = objShiftDesc.GetShiftDescriptionByShiftId(editid.Value);
        if (dttimetable.Rows.Count > 0)
        {
            for (int i = 0; i < chkTimeTableList.Items.Count; i++)
            {
                for (int j = 0; j < dttimetable.Rows.Count; j++)
                {
                    if (chkTimeTableList.Items[i].Value == dttimetable.Rows[j]["TimeTable_Id"].ToString())
                    {
                        chkTimeTableList.Items[i].Selected = true;
                    }
                }
            }
        }


    }
    protected void GetDays()
    {

        string[] weekdays = new string[8];
        weekdays[1] = "Monday";
        weekdays[2] = "Tuesday";
        weekdays[3] = "Wednesday";
        weekdays[4] = "Thursday";
        weekdays[5] = "Friday";
        weekdays[6] = "Saturday";
        weekdays[7] = "Sunday";







        int days = Convert.ToInt32(ddlCycleUnit.SelectedValue);
        int cycleno = Convert.ToInt32(txtCycleNo.Text);

        DateTime newappfromdt = Convert.ToDateTime(txtApplyFrom.Text);
        string appfromdt = newappfromdt.ToShortDateString();
        string appfromday = Convert.ToDateTime(newappfromdt).DayOfWeek.ToString().Substring(0, 3);


        DataTable dtfordays = new DataTable();
        dtfordays.Columns.Add("days");
        dtfordays.Columns.Add("dayno");
        int totaldays = days * cycleno;
        int colrept = 0;
        if (totaldays % 7 > 0)
        {
            colrept = 1;
        }
        chkDayUnderPeriod.RepeatColumns = totaldays / 7 + colrept;

        if (ddlCycleUnit.SelectedValue == "1")
        {
            for (int i = 1; i <= totaldays; i++)
            {

                dtfordays.Rows.Add(dtfordays.NewRow());
                dtfordays.Rows[dtfordays.Rows.Count - 1]["days"] = "Days " + i.ToString() + " , " + appfromdt + " , " + appfromday;
                dtfordays.Rows[dtfordays.Rows.Count - 1]["dayno"] = "Days " + i.ToString();
                appfromdt = (Convert.ToDateTime(appfromdt).AddDays(1)).ToShortDateString().ToString();
                appfromday = Convert.ToDateTime(appfromdt).DayOfWeek.ToString().Substring(0, 3);
            }
        }
        else if (ddlCycleUnit.SelectedValue == "7")
        {
            //Added by Fatima
            appfromday = Convert.ToDateTime(appfromdt).DayOfWeek.ToString();
            for (int j = 1; j <= weekdays.Length; j++)
            {
                if (weekdays[j].Equals(appfromday))
                {
                    appfromday = Convert.ToDateTime(appfromdt).ToShortDateString();
                    for (int i = 1; i <= totaldays; i++)
                    {

                        dtfordays.Rows.Add(dtfordays.NewRow());
                        dtfordays.Rows[dtfordays.Rows.Count - 1]["days"] = weekdays[j % 7].ToString().Substring(0, 3) + " , " + appfromdt;
                        dtfordays.Rows[dtfordays.Rows.Count - 1]["dayno"] = weekdays[j % 7].ToString();
                        appfromdt = (Convert.ToDateTime(appfromdt).AddDays(1)).ToShortDateString().ToString();
                        j++;
                    }
                    break;
                }
            }

        }
        else
        {
            string month = Convert.ToDateTime(appfromdt.ToString()).Month.ToString();
            DateTime startdate = new DateTime(Convert.ToDateTime(appfromdt.ToString()).Year, Convert.ToDateTime(appfromdt.ToString()).Month, 1);
            string stdate = Convert.ToDateTime(startdate.ToString()).ToShortDateString().ToString();

            string startday = Convert.ToDateTime(startdate.ToString()).DayOfWeek.ToString().Substring(0, 3);

            for (int i = 1; i <= totaldays; i++)
            {
                int dayno = 1;
                if (i % 31 == 0)
                {
                    dayno = 31;
                }
                else
                {
                    dayno = i % 31;
                }


                dtfordays.Rows.Add(dtfordays.NewRow());
                if (month.Equals(Convert.ToDateTime(stdate).Month.ToString()))
                {
                    dtfordays.Rows[dtfordays.Rows.Count - 1]["days"] = dayno.ToString() + " Days" + " ,( " + stdate + " , " + startday + " )";
                    dtfordays.Rows[dtfordays.Rows.Count - 1]["dayno"] = dayno.ToString() + " Days";
                    stdate = (Convert.ToDateTime(stdate).AddDays(1)).ToShortDateString().ToString();
                    startday = Convert.ToDateTime(stdate).DayOfWeek.ToString().Substring(0, 3);
                    if (dayno == 31)
                        month = Convert.ToDateTime(stdate).Month.ToString();
                }
                else
                {
                    dtfordays.Rows[dtfordays.Rows.Count - 1]["days"] = dayno.ToString() + " Days";
                    dtfordays.Rows[dtfordays.Rows.Count - 1]["dayno"] = dayno.ToString() + " Days";
                    if (dayno == 31)
                        month = Convert.ToDateTime(stdate).Month.ToString();
                }
                //dtfordays.Rows[dtfordays.Rows.Count - 1]["days"] = dayno.ToString() + " Days" + " , " + stdate + " , " + startday;
                //dtfordays.Rows[dtfordays.Rows.Count - 1]["dayno"] = dayno.ToString() + " Days";
                // stdate = (Convert.ToDateTime(stdate).AddDays(1)).ToShortDateString().ToString();
                //startday = Convert.ToDateTime(stdate).DayOfWeek.ToString().Substring(0, 3);
            }
        }

        dlView.DataSource = dtfordays;
        dlView.DataBind();
    }
    public bool ISOverLapTimeTable(DateTime dtintime1, DateTime dtouttime1, DateTime dtintime, DateTime dtouttime)
    {
        bool isoverlap = false;
        if (dtintime >= dtintime1 && dtintime <= dtouttime1)
        {
            isoverlap = true;

        }
        else if (dtouttime >= dtintime1 && dtouttime <= dtouttime1)
        {
            isoverlap = true;

        }

        else if (dtintime1 >= dtintime && dtintime1 <= dtouttime)
        {
            isoverlap = true;

        }

        else if (dtouttime1 >= dtintime && dtouttime1 <= dtouttime)
        {
            isoverlap = true;

        }
        else if (dtintime1 == dtintime && dtouttime1 == dtouttime)
        {
            isoverlap = true;

        }
        return isoverlap;
    }
    protected void chkTimeTableList_SelectedIndexChanged(object sender, EventArgs e)
    {


        bool isoverlap = false;



        CheckBoxList list = (CheckBoxList)sender;
        string[] control = Request.Form.Get("__EVENTTARGET").Split('$');
        int idx = control.Length - 1;
        string timetableid = string.Empty;
        try
        {
            timetableid = list.Items[Int32.Parse(control[idx])].Value;
        }
        catch (Exception ex)
        {
            return;
        }

        if (list.Items[Int32.Parse(control[idx])].Selected)
        {
            DataTable dtin = objTimeTable.GetTimeTableMasterById(Session["CompId"].ToString(), timetableid);

            DateTime dtintime = Convert.ToDateTime(dtin.Rows[0]["OnDuty_Time"]);
            DateTime dtouttime = Convert.ToDateTime(dtin.Rows[0]["OffDuty_Time"]);
            DateTime OnDutyTime = Convert.ToDateTime(dtin.Rows[0]["OnDuty_Time"]);
            DateTime OffDutyTime = Convert.ToDateTime(dtin.Rows[0]["OffDuty_Time"]);
            if (dtintime > dtouttime)
            {
                dtouttime = dtouttime.AddHours(24);
            }

            if (OnDutyTime > OffDutyTime)
            {
                OffDutyTime = OffDutyTime.AddHours(24);
            }

            for (int i = 0; i < chkTimeTableList.Items.Count; i++)
            {
                if (chkTimeTableList.Items[i].Selected && chkTimeTableList.Items[i].Value != timetableid)
                {
                    DataTable dtin1 = objTimeTable.GetTimeTableMasterById(Session["CompId"].ToString(), chkTimeTableList.Items[i].Value);
                    DateTime dtintime1 = Convert.ToDateTime(dtin1.Rows[0]["OnDuty_Time"]);
                    DateTime dtouttime1 = Convert.ToDateTime(dtin1.Rows[0]["OffDuty_Time"]);

                    DateTime OnDutyTime1 = Convert.ToDateTime(dtin1.Rows[0]["OnDuty_Time"]);
                    DateTime OffDutyTime1 = Convert.ToDateTime(dtin1.Rows[0]["OffDuty_Time"]);


                    if (dtintime1 > dtouttime1)
                    {
                        dtouttime1 = dtouttime1.AddHours(24);
                    }

                    if (OnDutyTime1 > OffDutyTime1)
                    {
                        OffDutyTime1 = OffDutyTime1.AddHours(24);
                    }

                    if (dtintime >= dtintime1 && dtintime <= dtouttime1)
                    {
                        isoverlap = true;
                        break;
                    }
                    if (dtouttime >= dtintime1 && dtouttime <= dtouttime1)
                    {
                        isoverlap = true;
                        break;
                    }

                    if (dtintime1 >= dtintime && dtintime1 <= dtouttime)
                    {
                        isoverlap = true;
                        break;
                    }

                    if (dtouttime1 >= dtintime && dtouttime1 <= dtouttime)
                    {
                        isoverlap = true;
                        break;
                    }
                }
            }
        }
        if (isoverlap)
        {
            list.Items[Int32.Parse(control[idx])].Selected = false;

            DisplayMessage("Time Overlaped");



        }


    }
    protected void btnSave_Click(object sender, EventArgs e)
    {
        int b = 0;



        if (txtShiftName.Text == "")
        {
            DisplayMessage("Enter Shift Name");
            txtShiftName.Focus();
            return;
        }

        if (txtCycleNo.Text == "")
        {
            DisplayMessage("Enter Cycle No.");
            txtCycleNo.Focus();
            return;
        }
        if (txtApplyFrom.Text == "")
        {
            DisplayMessage("Enter Apply Date");
            txtApplyFrom.Focus();
            return;
        }
        else
        {

            try
            {
                Convert.ToDateTime(txtApplyFrom.Text);
            }
            catch
            {
                DisplayMessage("Enter Correct To Date Format dd-MMM-yyyy");
                txtApplyFrom.Text = "";
                txtApplyFrom.Focus();
                return;
            }

        }




        if (editid.Value == "")
        {

            DataTable dt1 = objShift.GetShiftMaster(Session["CompId"].ToString());

            dt1 = new DataView(dt1, "Shift_Name='" + txtShiftName.Text + "'", "", DataViewRowState.CurrentRows).ToTable();
            if (dt1.Rows.Count > 0)
            {
                DisplayMessage("Shift Name Already Exists");
                txtShiftName.Focus();
                return;

            }




            b = objShift.InsertShiftMaster(Session["CompId"].ToString(), txtShiftName.Text, txtShiftNameL.Text, Session["BrandId"].ToString(), txtCycleNo.Text, ddlCycleUnit.SelectedValue, txtApplyFrom.Text, "", "", "", "", "", true.ToString(), DateTime.Now.ToString(), true.ToString(), Session["UserId"].ToString(), DateTime.Now.ToString(), Session["UserId"].ToString(), DateTime.Now.ToString());
            if (b != 0)
            {
                DisplayMessage("Record Saved");
                editid.Value = b.ToString();
                btnAddTime.Visible = true;
                btnClearAll.Visible = true;
                btnDelete.Visible = true;

                
             

                ViewShift(editid.Value);
            }
            else
            {
                DisplayMessage("Record Not Saved");
            }
        }
        else
        {
            string ShiftTypeName = string.Empty;
            DataTable dt1 = objShift.GetShiftMaster(Session["CompId"].ToString());
            try
            {
                ShiftTypeName = new DataView(dt1, "Shift_Id='" + editid.Value + "'", "", DataViewRowState.CurrentRows).ToTable().Rows[0]["Shift_Name"].ToString();
            }
            catch
            {
                ShiftTypeName = "";
            }
            dt1 = new DataView(dt1, "Shift_Name='" + txtShiftName.Text + "' and Shift_Name<>'" + ShiftTypeName + "'", "", DataViewRowState.CurrentRows).ToTable();
            if (dt1.Rows.Count > 0)
            {
                DisplayMessage("Shift Name Already Exists");
                txtShiftName.Focus();
                return;

            }
            b = objShift.UpdateShiftMaster(editid.Value, Session["CompId"].ToString(), txtShiftName.Text, txtShiftNameL.Text, Session["BrandId"].ToString(), txtCycleNo.Text, ddlCycleUnit.SelectedValue, txtApplyFrom.Text, "", "", "", "", "", true.ToString(), DateTime.Now.ToString(), true.ToString(), Session["UserId"].ToString(), DateTime.Now.ToString());

            if (b != 0)
            {

                DisplayMessage("Record Updated");

                objShiftDesc.DeleteShiftDescriptionByShiftId(editid.Value);
                objShiftDesc.DeleteShift_TimeTableByShiftId(editid.Value);

                ViewShift(editid.Value);
                btnAddTime.Visible = true;
                btnClearAll.Visible = true;
                btnDelete.Visible = true;
            }
            else
            {
                DisplayMessage("Record Not Updated");
            }

        }
    }



    public void SubDtScatch()
    {
        string[] weekdays = new string[8];
        weekdays[1] = "Monday";
        weekdays[2] = "Tuesday";
        weekdays[3] = "Wednesday";
        weekdays[4] = "Thursday";
        weekdays[5] = "Friday";
        weekdays[6] = "Saturday";
        weekdays[7] = "Sunday";
        string[] arr = Convert.ToDateTime(txtApplyFrom.Text).ToString("dd-MM-yyyy").Split('-');
        DataTable dtScatch = new DataTable();
        dtScatch.Columns.Add(new DataColumn("Date"));
        dtScatch.Columns.Add(new DataColumn("Day"));
        dtScatch.Columns.Add(new DataColumn("Cycle_Type"));
        dtScatch.Columns.Add(new DataColumn("Cycle_Day"));
        DateTime dtApply = new DateTime(Convert.ToInt16(arr[2]), Convert.ToInt16(arr[1]), Convert.ToInt16(arr[0]));

        int index = ddlCycleUnit.SelectedIndex;



        int TotalDays = 1;

        switch (index)
        {
            case 0: TotalDays = TotalDays * Convert.ToInt16(txtCycleNo.Text) * 7;
                break;
            case 1: TotalDays = TotalDays * Convert.ToInt16(txtCycleNo.Text);
                break;
            case 2: TotalDays = TotalDays * Convert.ToInt16(txtCycleNo.Text) * 31;
                break;
        }
        int a = 1;
        int j = 0;
        for (int k = 1; k <= TotalDays; k++)
        {

            DataRow row = dtScatch.NewRow();
            row[0] = dtApply.ToShortDateString();
            row[1] = dtApply.DayOfWeek.ToString();


            if (index == 0)
            {

                for (int i = 1; i <= 7; i++)
                {

                    if (weekdays[i].ToString() == row[1].ToString())
                    {


                        row[2] = "Week-" + a.ToString();
                        row[3] = i.ToString();
                    }
                }
                if (k % 7 == 0)
                {
                    a = a + 1;

                }
            }
            else if (index == 2)
            {
                if (k % 31 == 0)
                {

                    j = 1;
                }
                else
                {
                    j = j + 1;
                }
                row[2] = "Month-" + a.ToString();
                row[3] = j.ToString();
                if (k % 31 == 0)
                {
                    a = a + 1;
                }
            }
            else
            {
                row[2] = "Day";
                row[3] = k.ToString();
            }


            dtScatch.Rows.Add(row);

            dtApply = dtApply.AddDays(1);
        }
        Session["dtScatch"] = dtScatch;


    }

    protected void btnAddShift_Click(object sender, EventArgs e)
    {
        if (txtShiftName.Text == "")
        {
            DisplayMessage("Enter Shift Name");
            txtShiftName.Focus();
            return;
        }

        if (txtCycleNo.Text == "")
        {
            DisplayMessage("Enter Cycle No.");
            txtCycleNo.Focus();
            return;
        }
        if (txtApplyFrom.Text == "")
        {
            DisplayMessage("Enter Apply Date");
            txtApplyFrom.Focus();
            return;
        }
        else
        {

            try
            {
                Convert.ToDateTime(txtApplyFrom.Text);
            }
            catch
            {
                DisplayMessage("Enter Correct To Date Format dd-MMM-yyyy");
                txtApplyFrom.Text = "";
                txtApplyFrom.Focus();
                return;
            }

        }

        SubDtScatch();
        int b = 0;

        DataTable dt = objTimeTable.GetTimeTableMaster(Session["CompId"].ToString());
        DataTable dtTime1 = new DataTable();
        dtTime1.Columns.Add("EDutyTime");
        dtTime1.Columns.Add("TimeTableId");

        for (int i = 0; i < dt.Rows.Count; i++)
        {
            DataRow dr = dtTime1.NewRow();

            dr["EDutyTime"] = dt.Rows[i]["TimeTable_Name"] + "(" + Convert.ToDateTime(dt.Rows[i]["OnDuty_Time"]).ToString("HH:mm") + "-" + Convert.ToDateTime(dt.Rows[i]["OffDuty_Time"]).ToString("HH:mm") + ")";
            dr["TimeTableId"] = dt.Rows[i]["TimeTable_Id"].ToString();
            dtTime1.Rows.Add(dr);

        }

        chkTimeTableList.DataSource = dtTime1;
        chkTimeTableList.DataTextField = "EDutyTime";
        chkTimeTableList.DataValueField = "TimeTableId";
        chkTimeTableList.DataBind();


        if (editid.Value != "")
        {
            PnlViewShift.Visible = false;
            b = objShift.UpdateShiftMaster(editid.Value, Session["CompId"].ToString(), txtShiftName.Text, txtShiftNameL.Text, Session["BrandId"].ToString(), txtCycleNo.Text, ddlCycleUnit.SelectedValue, txtApplyFrom.Text, "", "", "", "", "", true.ToString(), DateTime.Now.ToString(), true.ToString(), Session["UserId"].ToString(), DateTime.Now.ToString());

            DisplayMessage("Record Updated");





            for (int i = 0; i < chkTimeTableList.Items.Count; i++)
            {

                chkTimeTableList.Items[i].Selected = false;

            }




            for (int i = 0; i < chkDayUnderPeriod.Items.Count; i++)
            {


                chkDayUnderPeriod.Items[i].Selected = true;


            }




        }
        else
        {
            string shiftid = "";
            b = objShift.InsertShiftMaster(Session["CompId"].ToString(), txtShiftName.Text, txtShiftNameL.Text, Session["BrandId"].ToString(), txtCycleNo.Text, ddlCycleUnit.SelectedValue, txtApplyFrom.Text, "", "", "", "", "", true.ToString(), DateTime.Now.ToString(), true.ToString(), Session["UserId"].ToString(), DateTime.Now.ToString(), Session["UserId"].ToString(), DateTime.Now.ToString());
            editid.Value = shiftid;


          

            DisplayMessage("Record Saved");


            btnAddTime.Visible = true;

            btnClearAll.Visible = true;

        }
        if (b != 0)
        {

            FillGrid();
            FillGridBin();
            pnlShiftNew.Visible = false;

            chkTimeTableList.Enabled = true;
            chkDayUnderPeriod.Enabled = true;

            PanelShiftAss.Visible = true;

            TrShiftName.Visible = true;
            btnshowpopup_Click(null, null);

            lblShiftNameIs.Text = txtShiftName.Text;
            txtShiftId.Text = editid.Value;

            btnNext.Visible = false;
            btnOk.Visible = true;
            btnBack.Visible = false;
            btnCancelPanel.Visible = true;

            PnlViewShift.Visible = false;


        }

    }
    protected void btnshowpopup_Click(object sender, ImageClickEventArgs e)
    {
        bindchecklist();
       
        if (Session["IsView"] != null)
        {
            Session["IsView"] = null;
        }
    }
    private void FindAndCheckChkTimeTable(string timetableid)
    {
        for (int i = 0; i < chkTimeTableList.Items.Count; i++)
        {
            if (chkTimeTableList.Items[i].Value == timetableid)
            {
                chkTimeTableList.Items[i].Selected = false;
                break;
            }
        }
    }

    protected void btnDelete_Click(object sender, EventArgs e)
    {
        int flag = 0;
        foreach (GridViewRow gvr in gvShiftNew.Rows)
        {
            CheckBox chk = (CheckBox)gvr.FindControl("ChkDay");
            if (chk.Checked)
            {
                flag = 1;
            }
        }

        if (flag == 0)
        {
            DisplayMessage("Please First Select Date in List");
            return;
        }
        foreach (GridViewRow gvr in gvShiftNew.Rows)
        {
            CheckBox chk = (CheckBox)gvr.FindControl("ChkDay");
            HiddenField CycleType = (HiddenField)gvr.FindControl("hdnCycle_Type");
            HiddenField CycleDay = (HiddenField)gvr.FindControl("hdnCycle_Day");
            if (chk.Checked)
            {
                objShiftDesc.DeleteShiftDescriptionByCycleDayCycleDay(editid.Value, CycleType.Value, CycleDay.Value);

            }
        }
        ViewShift(editid.Value);
        DisplayMessage("Record Deleted");



    }
    protected void btnClearAll_OnClick(object sender, EventArgs e)
    {

        PnlViewShift.Visible = false;
        objShiftDesc.DeleteShiftDescriptionByShiftId(editid.Value);
        objShiftDesc.DeleteShift_TimeTableByShiftId(editid.Value);

        gvShiftView.DataSource = null;
        gvShiftView.DataBind();
        btnAddTime.Visible = false;
        btnClearAll.Visible = false;
        btnDelete.Visible = false;
    }

    protected void btnSelectAll_OnClick(object sender, EventArgs e)
    { 
       
        for (int j = 0; j < chkDayUnderPeriod.Items.Count; j++)
        {
            chkDayUnderPeriod.Items[j].Selected = true;
        }

       }
    public void bindchecklist()
    {

        string[] weekdays = new string[8];
        weekdays[1] = "Monday";
        weekdays[2] = "Tuesday";
        weekdays[3] = "Wednesday";
        weekdays[4] = "Thursday";
        weekdays[5] = "Friday";
        weekdays[6] = "Saturday";
        weekdays[7] = "Sunday";





     
        string WeekOff = string.Empty;
        WeekOff = objparam.GetApplicationParameterValueByParamName("Week Off Days", Session["CompId"].ToString());
        


        int days = Convert.ToInt32(ddlCycleUnit.SelectedValue);
        int cycleno = Convert.ToInt32(txtCycleNo.Text);

        DateTime newappfromdt = Convert.ToDateTime(txtApplyFrom.Text);
        string appfromdt = newappfromdt.ToShortDateString();
        string appfromday = Convert.ToDateTime(newappfromdt).DayOfWeek.ToString().Substring(0, 3);

        DataTable dtfordays = new DataTable();
        dtfordays.Columns.Add("days");
        dtfordays.Columns.Add("dayno");
        int totaldays = days * cycleno;
        int colrept = 0;
        if (totaldays % 7 > 0)
        {
            colrept = 1;
        }
        //chkDayUnderPeriod.RepeatColumns = totaldays / 7 + colrept;

        if (ddlCycleUnit.SelectedValue == "1")
        {
            for (int i = 1; i <= totaldays; i++)
            {

                dtfordays.Rows.Add(dtfordays.NewRow());
                dtfordays.Rows[dtfordays.Rows.Count - 1]["days"] = "Day " + "-" + i.ToString();
                dtfordays.Rows[dtfordays.Rows.Count - 1]["dayno"] = "Day " + "-" + i.ToString();
                appfromdt = (Convert.ToDateTime(appfromdt).AddDays(1)).ToShortDateString().ToString();
                appfromday = Convert.ToDateTime(appfromdt).DayOfWeek.ToString().Substring(0, 3);
            }
        }
        else if (ddlCycleUnit.SelectedValue == "7")
        {
            appfromday = Convert.ToDateTime(appfromdt).DayOfWeek.ToString();
            int CycleType = 0;
            for (int j = 1; j < weekdays.Length; j++)
            {
                if (weekdays[j].Equals(appfromday))
                {

                    appfromday = Convert.ToDateTime(appfromdt).ToShortDateString();
                    for (int i = 0; i < totaldays; i++)
                    {
                       
                        if (i % 7 == 0)
                        {
                            CycleType += 1;
                        }


                        if (WeekOff == "No")
                        {
                            


                                dtfordays.Rows.Add(dtfordays.NewRow());

                                dtfordays.Rows[dtfordays.Rows.Count - 1]["days"] = "Week-" + CycleType.ToString() + "-" + weekdays[j].ToString();


                                dtfordays.Rows[dtfordays.Rows.Count - 1]["dayno"] = "Week-" + CycleType.ToString() + "-" + (j).ToString();
                            
                        }
                        else
                        {
                            if (weekdays[j].ToString() != WeekOff)
                            {
                                
                                dtfordays.Rows.Add(dtfordays.NewRow());

                                dtfordays.Rows[dtfordays.Rows.Count - 1]["days"] = "Week-" + CycleType.ToString() + "-" + weekdays[j].ToString();


                                dtfordays.Rows[dtfordays.Rows.Count - 1]["dayno"] = "Week-" + CycleType.ToString() + "-" + (j).ToString();
                            }
                        }
                                appfromdt = (Convert.ToDateTime(appfromdt).AddDays(1)).ToShortDateString().ToString();
                        j++;
                        if(j==8)
                        {
                            j = 1;
                        }
                    }
                    break;
                }
            }

        }
        else
        {
            string month = Convert.ToDateTime(appfromdt.ToString()).Month.ToString();
            DateTime startdate = new DateTime(Convert.ToDateTime(appfromdt.ToString()).Year, Convert.ToDateTime(appfromdt.ToString()).Month, 1);

            startdate = Convert.ToDateTime(txtApplyFrom.Text);
            string stdate = Convert.ToDateTime(startdate.ToString()).ToShortDateString().ToString();

            string startday = Convert.ToDateTime(startdate.ToString()).DayOfWeek.ToString().Substring(0, 3);
            int CycleType = 1;
            int dayno = 1;
            for (int i = 1; i <= totaldays; i++)
            {


                if (i % 31 == 0)
                {
                    dayno = 31;

                }
                else
                {
                    dayno = i % 31;
                }


                dtfordays.Rows.Add(dtfordays.NewRow());
                if (month.Equals(Convert.ToDateTime(stdate).Month.ToString()))
                {
                    dtfordays.Rows[dtfordays.Rows.Count - 1]["days"] = "Month-" + CycleType.ToString() + "-" + dayno.ToString();
                    dtfordays.Rows[dtfordays.Rows.Count - 1]["dayno"] = "Month-" + CycleType.ToString() + "-" + dayno.ToString();
                    stdate = (Convert.ToDateTime(stdate).AddDays(1)).ToShortDateString().ToString();
                    startday = Convert.ToDateTime(stdate).DayOfWeek.ToString().Substring(0, 3);
                    if (dayno == 31)
                        month = Convert.ToDateTime(stdate).Month.ToString();
                }
                else
                {
                    dtfordays.Rows[dtfordays.Rows.Count - 1]["days"] = "Month-" + CycleType.ToString() + "-" + dayno.ToString();
                    dtfordays.Rows[dtfordays.Rows.Count - 1]["dayno"] = "Month-" + CycleType.ToString() + "-" + dayno.ToString();
                    stdate = (Convert.ToDateTime(stdate).AddDays(1)).ToShortDateString().ToString();
                    startday = Convert.ToDateTime(stdate).DayOfWeek.ToString().Substring(0, 3);
                    if (dayno == 31)
                        month = Convert.ToDateTime(stdate).Month.ToString();
                }
                if (i % 31 == 0)
                {

                    CycleType = CycleType + 1;
                }


            }
        }

        chkDayUnderPeriod.DataSource = dtfordays;
        chkDayUnderPeriod.DataTextField = "days";
        chkDayUnderPeriod.DataValueField = "dayno";
        chkDayUnderPeriod.DataBind();

        for (int j = 0; j < chkDayUnderPeriod.Items.Count; j++)
        {
            chkDayUnderPeriod.Items[j].Selected = true;
        }











    }
    protected void btnDelete_Command(object sender, CommandEventArgs e)
    {





    }
    protected void btnEdit_Command(object sender, CommandEventArgs e)
    {
        DataTable dtShift = new DataTable();
        dtShift = objSch.GetSheduleDescription();
        dtShift = new DataView(dtShift, "Shift_Id='" + e.CommandArgument.ToString() + "'", "", DataViewRowState.CurrentRows).ToTable();

        if (dtShift.Rows.Count > 0)
        {
            DisplayMessage("You cannot edit this shift");
            return;

        }
        editid.Value = e.CommandArgument.ToString();


        DataTable dt = objShift.GetShiftMasterById(Session["CompId"].ToString(), editid.Value);
        if (dt.Rows.Count > 0)
        {

            txtShiftName.Text = dt.Rows[0]["Shift_Name"].ToString();
            txtShiftNameL.Text = dt.Rows[0]["Shift_Name_L"].ToString();

            txtApplyFrom.Text = Convert.ToDateTime(dt.Rows[0]["Apply_From"]).ToString(objSys.SetDateFormat());
            txtCycleNo.Text = dt.Rows[0]["Cycle_No"].ToString();
            ddlCycleUnit.SelectedValue = dt.Rows[0]["Cycle_Unit"].ToString();






            btnNew_Click(null, null);
            btnNew.Text = Resources.Attendance.Edit;

            btnAddTime.Visible = true;
            btnClearAll.Visible = true;
            btnDelete.Visible = true;

            ViewShift(editid.Value);

        }



    }

    protected void ChkAllDay_CheckedChanged(object sender, EventArgs e)
    {
        if (((CheckBox)(sender)).Checked)
        {
            foreach (GridViewRow gvr in gvShiftView.Rows)
            {
                CheckBox chk = (CheckBox)gvr.FindControl("ChkDay");
                chk.Checked = true;
            }
        }
        else
        {
            foreach (GridViewRow gvr in gvShiftView.Rows)
            {
                CheckBox chk = (CheckBox)gvr.FindControl("ChkDay");
                chk.Checked = false;
            }
        }

    }
    protected void ChkAllDay_CheckedChanged1(object sender, EventArgs e)
    {
        if (((CheckBox)(sender)).Checked)
        {
            foreach (GridViewRow gvr in gvShiftNew.Rows)
            {
                CheckBox chk = (CheckBox)gvr.FindControl("ChkDay");
                chk.Checked = true;
            }
        }
        else
        {
            foreach (GridViewRow gvr in gvShiftNew.Rows)
            {
                CheckBox chk = (CheckBox)gvr.FindControl("ChkDay");
                chk.Checked = false;
            }
        }

    }
    public string WriteDays(object o)
    {
        DateTime dt = Convert.ToDateTime(o.ToString());

        return dt.ToString("dd-MMM-yyyy") + "   " + dt.DayOfWeek;
    }
    public void ViewShift(string shiftId)
    {
        PnlViewShift.Visible = true;
        DataTable dttimetable1 = objShiftDesc.GetShiftDescriptionByShiftId(shiftId);
        SubDtScatch();
        DataTable dtScatch = (DataTable)Session["dtScatch"];

        DataTable dt = objTimeTable.GetTimeTableMaster(Session["CompId"].ToString());

        DataTable dtTime1 = new DataTable();
        dtTime1.Columns.Add("EDutyTime");
        dtTime1.Columns.Add("TimeTable_Id");
        dtTime1.Columns.Add("Cycle_Type");
        dtTime1.Columns.Add("Cycle_Day");

        string WeekOff = string.Empty;
        WeekOff = objparam.GetApplicationParameterValueByParamName("Week Off Days", Session["CompId"].ToString());
       
        for (int i = 0; i < dtScatch.Rows.Count; i++)
        {
            DataTable dtTemp = new DataView(dttimetable1, "Cycle_Day = '" + dtScatch.Rows[i]["Cycle_Day"].ToString() + "' and  Cycle_Type='" + dtScatch.Rows[i]["Cycle_Type"].ToString() + "' and convert(TimeTable_Id,System.String) <> ''", "", DataViewRowState.CurrentRows).ToTable();
            DataRow dr = dtTime1.NewRow();
            string sTime = string.Empty;
            if (dtTemp.Rows.Count > 0)
            {
                
                for (int k = 0; k < dtTemp.Rows.Count; k++)
                {

                    DataTable dtTempTime = new DataView(dt, "TimeTable_Id = '" + dtTemp.Rows[k]["TimeTable_Id"].ToString() + "'", "", DataViewRowState.CurrentRows).ToTable();
                    if (dtTempTime.Rows.Count > 0)
                    {
                        sTime = sTime + dtTempTime.Rows[0]["TimeTable_Name"].ToString() + "(" + dtTempTime.Rows[0]["OnDuty_Time"].ToString() + "-" + dtTempTime.Rows[0]["OffDuty_Time"].ToString() + ")" + ",";
                    }

                }
                dr["TimeTable_Id"] = sTime;
                dr["EDutyTime"] = Convert.ToDateTime(dtScatch.Rows[i][0].ToString()).ToString("dd-MMM-yyyy");
                dr["Cycle_Type"] = dtScatch.Rows[i]["Cycle_Type"].ToString();
                dr["Cycle_Day"] = dtScatch.Rows[i]["Cycle_Day"].ToString();
                dtTime1.Rows.Add(dr);
            }
            else
            {
                dr["TimeTable_Id"] = "IsOff";
                dr["EDutyTime"] = Convert.ToDateTime(dtScatch.Rows[i][0].ToString()).ToString("dd-MMM-yyyy");
                dr["Cycle_Type"] = dtScatch.Rows[i]["Cycle_Type"].ToString();
                dr["Cycle_Day"] = dtScatch.Rows[i]["Cycle_Day"].ToString();
                dtTime1.Rows.Add(dr);
            }
        }

        int max = 0;
        for (int i = 0; i < dttimetable1.Rows.Count;i++)
        {
            DataTable dt2 = objShiftDesc.GetShift_TimeTableByRefId(dttimetable1.Rows[i]["Trans_Id"].ToString());

            if(dt2.Rows.Count > 0)
            {
                if(dt2.Rows.Count>max)
                {

                max = dt2.Rows.Count;

                }
            }


        }
        string[] weekdays = new string[8];
        weekdays[1] = "Monday";
        weekdays[2] = "Tuesday";
        weekdays[3] = "Wednesday";
        weekdays[4] = "Thursday";
        weekdays[5] = "Friday";
        weekdays[6] = "Saturday";
        weekdays[7] = "Sunday";

        DataTable dtdata = new DataTable();
        dtdata.Columns.Add("Day");
      
        dtdata.Columns.Add("Cycle_Type");
        dtdata.Columns.Add("Cycle_Day");
        if(max > 0)
        {
            int max2 = max;
            max2 = 2 * max2;
            for (int i = 0; i < max2;i++)
            {
                dtdata.Columns.Add("t"+(i+1));

            }

        }

        string sTime1 = "";
        for (int i = 0; i < dtScatch.Rows.Count; i++)
        {
            DataTable dtTemp = new DataView(dttimetable1, "Cycle_Day = '" + dtScatch.Rows[i]["Cycle_Day"].ToString() + "' and  Cycle_Type='" + dtScatch.Rows[i]["Cycle_Type"].ToString() + "' and convert(TimeTable_Id,System.String) <> ''", "", DataViewRowState.CurrentRows).ToTable();
            DataRow dr = dtdata.NewRow();

           if(WeekOff!="No")
           {
               if (ddlCycleUnit.SelectedValue=="7")
               {
               if (weekdays[int.Parse(dtScatch.Rows[i]["Cycle_Day"].ToString())].ToString()==WeekOff && ddlCycleUnit.SelectedValue == "7")
               {
                   continue;

               }
               }
               else if (ddlCycleUnit.SelectedValue=="31")
               {

               }

           }

            if (dtTemp.Rows.Count > 0)
            {
                if(ddlCycleUnit.SelectedValue=="7")
                {
                    dr["Day"] = dtScatch.Rows[i]["Cycle_Type"].ToString() + "-" + weekdays[int.Parse(dtScatch.Rows[i]["Cycle_Day"].ToString())];
                }
                else
                {
                    dr["Day"] = dtScatch.Rows[i]["Cycle_Type"].ToString() + "-" + dtScatch.Rows[i]["Cycle_Day"].ToString();
             

                }
                
                dr["Cycle_Type"] = dtScatch.Rows[i]["Cycle_Type"].ToString();
                dr["Cycle_Day"] = dtScatch.Rows[i]["Cycle_Day"].ToString();
               
                for (int k = 0; k < dtTemp.Rows.Count; k++)
                {
                    

                    if (k == 0)
                    {
                        DataTable dtTempTime = new DataView(dt, "TimeTable_Id = '" + dtTemp.Rows[k]["TimeTable_Id"].ToString() + "'", "", DataViewRowState.CurrentRows).ToTable();
                        if (dtTempTime.Rows.Count > 0)
                        {
                            sTime1 = dtTempTime.Rows[0]["TimeTable_Name"].ToString();
                        }
                        dr["t" + (k+1).ToString()] = sTime1;
                    }
                    else
                    {
                        if (k % 2 != 0)
                        {

                            DataTable dtTempTime = new DataView(dt, "TimeTable_Id = '" + dtTemp.Rows[k]["TimeTable_Id"].ToString() + "'", "", DataViewRowState.CurrentRows).ToTable();
                            if (dtTempTime.Rows.Count > 0)
                            {
                                sTime1 = dtTempTime.Rows[0]["TimeTable_Name"].ToString();
                            }
                            dr["t" + (k + 2).ToString()] = sTime1;
                        }
                        else
                        {
                            DataTable dtTempTime = new DataView(dt, "TimeTable_Id = '" + dtTemp.Rows[k]["TimeTable_Id"].ToString() + "'", "", DataViewRowState.CurrentRows).ToTable();
                            if (dtTempTime.Rows.Count > 0)
                            {
                                sTime1 = dtTempTime.Rows[0]["TimeTable_Name"].ToString();
                            }
                            dr["t" + (k + 3).ToString()] = sTime1;
                        }
                    }
                }
                dtdata.Rows.Add(dr);
                    dr = dtdata.NewRow();
                for (int k = 0; k < dtTemp.Rows.Count; k++)
                {

                    if (k == 0)
                    {
                        DataTable dtTempTime = new DataView(dt, "TimeTable_Id = '" + dtTemp.Rows[k]["TimeTable_Id"].ToString() + "'", "", DataViewRowState.CurrentRows).ToTable();
                        if (dtTempTime.Rows.Count > 0)
                        {
                               dr["t" + (k+1).ToString()] = dtTempTime.Rows[0]["OnDuty_Time"].ToString();

                            dr["t" + (k + 2).ToString()] = dtTempTime.Rows[0]["OffDuty_Time"].ToString();
                        }
                    }
                    else
                    {



                        if(k%2!=0)
                        {
                            DataTable dtTempTime = new DataView(dt, "TimeTable_Id = '" + dtTemp.Rows[k]["TimeTable_Id"].ToString() + "'", "", DataViewRowState.CurrentRows).ToTable();
                            if (dtTempTime.Rows.Count > 0)
                            {

                                dr["t" + (k + 2).ToString()] = dtTempTime.Rows[0]["OnDuty_Time"].ToString();

                                dr["t" + (k + 3).ToString()] = dtTempTime.Rows[0]["OffDuty_Time"].ToString();
                            }

                        }
                        else
                        {
                            DataTable dtTempTime = new DataView(dt, "TimeTable_Id = '" + dtTemp.Rows[k]["TimeTable_Id"].ToString() + "'", "", DataViewRowState.CurrentRows).ToTable();
                            if (dtTempTime.Rows.Count > 0)
                            {

                                dr["t" + (k + 3).ToString()] = dtTempTime.Rows[0]["OnDuty_Time"].ToString();

                                dr["t" + (k + 4).ToString()] = dtTempTime.Rows[0]["OffDuty_Time"].ToString();
                            }
                        }

                    }


                }
                dtdata.Rows.Add(dr);

            }
            else
            {

                if (ddlCycleUnit.SelectedValue == "7")
                {
                    dr["Day"] = dtScatch.Rows[i]["Cycle_Type"].ToString() + "-" + weekdays[int.Parse(dtScatch.Rows[i]["Cycle_Day"].ToString())];
                }
                else
                {
                    dr["Day"] = dtScatch.Rows[i]["Cycle_Type"].ToString() + "-" + dtScatch.Rows[i]["Cycle_Day"].ToString();


                }
                try
                {
                    dr["t1"] = "Off";
                }
                catch
                {

                }
                dtdata.Rows.Add(dr);
            }

        }

      



        Session["ViewShiftDt"] = dtTime1;
        gvShiftView.DataSource = dtTime1;
        gvShiftView.DataBind();

        gvShiftView.Visible = false;

            gvShiftNew.DataSource = dtdata;
        gvShiftNew.DataBind();


        gvShiftNew.HeaderRow.Cells[2].Visible=false;
        gvShiftNew.HeaderRow.Cells[3].Visible = false;
        for (int i = 4; i < gvShiftNew.HeaderRow.Cells.Count; i++)
        {
            gvShiftNew.HeaderRow.Cells[i].Text="";
        }
        for (int i = 4; i < gvShiftNew.HeaderRow.Cells.Count; i++)
        {
            gvShiftNew.HeaderRow.Cells[i].BorderStyle = BorderStyle.None;
        }

        try
        {
            int count = gvShiftNew.HeaderRow.Cells.Count - 2;

            count = count / 2;
            count = count + 2;
            gvShiftNew.HeaderRow.Cells[count].Text = "Time Table";

        }
        catch
        {
        }

    }

    
    protected void gvShiftNew_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            e.Row.Cells[2].Visible = false;
            e.Row.Cells[3].Visible = false;
          
            CheckBox chb = (CheckBox)e.Row.FindControl("chkDay");
          
            HiddenField hdCycleDay = (HiddenField)e.Row.FindControl("hdnCycle_Type");
          
            if(hdCycleDay.Value=="")
            {
                chb.Visible = false;

            }
        }
    }
    protected void IbtnDelete_Command(object sender, CommandEventArgs e)
    {

        DataTable dtShift = new DataTable();
        dtShift = objSch.GetSheduleDescription();
        dtShift = new DataView(dtShift, "Shift_Id='" + e.CommandArgument.ToString() + "'", "", DataViewRowState.CurrentRows).ToTable();

        if (dtShift.Rows.Count > 0)
        {
            DisplayMessage("You cannot delete this shift");
            return;

        }
        int b = 0;
        b = objShift.DeleteShiftMaster(Session["CompId"].ToString(), e.CommandArgument.ToString(), false.ToString(), Session["UserId"].ToString(), DateTime.Now.ToString());
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
    protected void gvShift_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        gvShift.PageIndex = e.NewPageIndex;

        DataTable dt = (DataTable)Session["dtFilter"];
        gvShift.DataSource = dt;
        gvShift.DataBind();
        AllPageCode();

    }
    protected void gvShift_OnSorting(object sender, GridViewSortEventArgs e)
    {
        HDFSort.Value = HDFSort.Value == "ASC" ? "DESC" : "ASC";
        DataTable dt = new DataTable();
        dt = (DataTable)Session["dtFilter"];
        DataView dv = new DataView(dt);
        string Query = "" + e.SortExpression + " " + HDFSort.Value + "";
        dv.Sort = Query;
        dt = dv.ToTable();
        Session["dtFilter"] = dt;
        gvShift.DataSource = dt;
        gvShift.DataBind();
        AllPageCode();
    }


    [System.Web.Services.WebMethodAttribute(), System.Web.Script.Services.ScriptMethodAttribute()]
    public static string[] GetCompletionListShiftName(string prefixText, int count, string contextKey)
    {
        Att_ShiftManagement objAtt_Shift = new Att_ShiftManagement();
        DataTable dt = new DataView(objAtt_Shift.GetShiftMaster(HttpContext.Current.Session["CompId"].ToString()), "Shift_Name like '" + prefixText.ToString() + "%'", "", DataViewRowState.CurrentRows).ToTable();
        string[] txt = new string[dt.Rows.Count];

        for (int i = 0; i < dt.Rows.Count; i++)
        {
            txt[i] = dt.Rows[i]["Shift_Name"].ToString();
        }
        return txt;
    }


    public void DisplayMessage(string str)
    {
        ScriptManager.RegisterStartupScript(this, GetType(), "", "alert('" + str + "');", true);
    }
    protected void btnReset_Click(object sender, EventArgs e)
    {
        Reset();
        dlView.DataSource = null;
        dlView.DataBind();
        PnlViewShift.Visible = false;
    }
    protected void btnCancel_Click(object sender, EventArgs e)
    {
        FillGrid();
        FillGridBin();
        dlView.DataSource = null;
        dlView.DataBind();
        PnlViewShift.Visible = false;
        Reset();
        btnList_Click(null, null);
    }

    public void FillGrid()
    {

        DataTable dt = objShift.GetShiftMaster(Session["CompId"].ToString());
        dt = new DataView(dt, "Brand_Id='" + Session["BrandId"].ToString() + "'", "", DataViewRowState.CurrentRows).ToTable();

        gvShift.DataSource = dt;
        gvShift.DataBind();
        AllPageCode();
        Session["dtFilter"] = dt;
        Session["Shift"] = dt;

        lblTotalRecords.Text = Resources.Attendance.Total_Records + ": " + dt.Rows.Count.ToString() + "";

    }
    public void FillGridBin()
    {
        DataTable dt = new DataTable();
        dt = objShift.GetShiftMasterInactive(Session["CompId"].ToString());
        dt = new DataView(dt, "Brand_Id='" + Session["BrandId"].ToString() + "'", "", DataViewRowState.CurrentRows).ToTable();

        gvShiftBin.DataSource = dt;
        gvShiftBin.DataBind();


        Session["dtbinFilter"] = dt;
        Session["dtbinShift"] = dt;
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
        CheckBox chkSelAll = ((CheckBox)gvShiftBin.HeaderRow.FindControl("chkgvSelectAll"));
        for (int i = 0; i < gvShiftBin.Rows.Count; i++)
        {
            ((CheckBox)gvShiftBin.Rows[i].FindControl("chkgvSelect")).Checked = chkSelAll.Checked;
            if (chkSelAll.Checked)
            {
                if (!lblSelectedRecord.Text.Split(',').Contains(((Label)(gvShiftBin.Rows[i].FindControl("lblShiftId"))).Text.Trim().ToString()))
                {
                    lblSelectedRecord.Text += ((Label)(gvShiftBin.Rows[i].FindControl("lblShiftId"))).Text.Trim().ToString() + ",";
                }
            }
            else
            {
                string temp = string.Empty;
                string[] split = lblSelectedRecord.Text.Split(',');
                foreach (string item in split)
                {
                    if (item != ((Label)(gvShiftBin.Rows[i].FindControl("lblShiftId"))).Text.Trim().ToString())
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
        Label lb = (Label)gvShiftBin.Rows[index].FindControl("lblShiftId");
        if (((CheckBox)gvShiftBin.Rows[index].FindControl("chkgvSelect")).Checked)
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
                if (!lblSelectedRecord.Text.Split(',').Contains(dr["Shift_Id"]))
                {
                    lblSelectedRecord.Text += dr["Shift_Id"] + ",";
                }
            }
            for (int i = 0; i < gvShiftBin.Rows.Count; i++)
            {
                string[] split = lblSelectedRecord.Text.Split(',');
                Label lblconid = (Label)gvShiftBin.Rows[i].FindControl("lblShiftId");
                for (int j = 0; j < lblSelectedRecord.Text.Split(',').Length; j++)
                {
                    if (lblSelectedRecord.Text.Split(',')[j] != "")
                    {
                        if (lblconid.Text.Trim().ToString() == lblSelectedRecord.Text.Split(',')[j].Trim().ToString())
                        {
                            ((CheckBox)gvShiftBin.Rows[i].FindControl("chkgvSelect")).Checked = true;
                        }
                    }
                }
            }
        }
        else
        {
            lblSelectedRecord.Text = "";
            DataTable dtUnit1 = (DataTable)Session["dtBinFilter"];
            gvShiftBin.DataSource = dtUnit1;
            gvShiftBin.DataBind();
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
                    b = objShift.DeleteShiftMaster(Session["CompId"].ToString(), lblSelectedRecord.Text.Split(',')[j].Trim(), true.ToString(), Session["UserId"].ToString().ToString(), DateTime.Now.ToString());

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
            foreach (GridViewRow Gvr in gvShiftBin.Rows)
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



        txtShiftName.Text = "";
        txtShiftNameL.Text = "";

        txtCycleNo.Text = "";
        ddlCycleUnit.SelectedIndex = 0;
        txtApplyFrom.Text = "";





        btnNew.Text = Resources.Attendance.New;
        lblSelectedRecord.Text = "";
        ViewState["Select"] = null;
        editid.Value = "";
        txtValue.Text = "";
        ddlOption.SelectedIndex = 2;
        ddlFieldName.SelectedIndex = 0;
        btnDelete.Visible = false;
        btnAddTime.Visible = false;
        btnClearAll.Visible = false;


    }


}
