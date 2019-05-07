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
public partial class Arca_Wing_FileTransactionMaster : System.Web.UI.Page
{
    string StrBrandId = "1";
    string StrLocId = "1";
    string strCompId = string.Empty;
    Common cmn = new Common();
    Arc_FileTransaction ObjFile = new Arc_FileTransaction();
    Arc_Directory_Master ObjDirectory = new Arc_Directory_Master();
    Arc_FileType ObjFileType = new Arc_FileType();
    Document_Master ObjDocument = new Document_Master();

    SystemParameter objSys = new SystemParameter();
    EmployeeMaster objEmp = new EmployeeMaster();
    protected void Page_Load(object sender, EventArgs e)
    {
        strCompId = Session["CompId"].ToString();

        Session["BrandId"] = StrBrandId;
        Session["LocId"] = StrLocId;
        if (Session["UserId"] == null)
        {
            Response.Redirect("~/ERPLogin.aspx");
        }
        AllPageCode();
        if (!IsPostBack)
        {
            txtValue.Focus();
            BindDocumentList();
            BindDirectoryList();
            BindFiletypeList();
            FillGridBin();
            FillGrid();
            btnList_Click(null, null);
            txtExpiryDate.Text =Convert.ToDateTime(System.DateTime.Now.AddYears(20)).ToString("dd-MMM-yyyy");
        }
    }
    public void AllPageCode()
    {
        Page.Title = objSys.GetSysTitle();
        Session["AccordianId"] = "17";
        Session["HeaderText"] = "ArcaWing";
        DataTable dtAllPageCode = cmn.GetAllPagePermission(Session["UserId"].ToString(), "17", "64");
        if (dtAllPageCode.Rows.Count != 0)
        {
            if (dtAllPageCode.Rows[0][0].ToString() == "SuperAdmin")
            {
                btnSave.Visible = true;
                foreach (GridViewRow Row in gvFileMaster.Rows)
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
                    foreach (GridViewRow Row in gvFileMaster.Rows)
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
    void BindDocumentList()
    {

        DataTable dtdocument = new DataTable();
        string Documentid = "0";
        dtdocument = ObjDocument.getdocumentmaster(strCompId, Documentid);
        ddlDocumentName.DataSource = dtdocument;
        ddlDocumentName.DataTextField = "Document_name";
        ddlDocumentName.DataValueField = "Id";
        ddlDocumentName.DataBind();
        ddlDocumentName.Items.Insert(0, "--Select--");
    }
    void BindDirectoryList()
    {
        DataTable DtDirectory = new DataTable();
        string Directoryid = "0";
        DtDirectory = ObjDirectory.getDirectoryMaster(strCompId, Directoryid);
        ddlDirectory.DataSource = DtDirectory;
        ddlDirectory.DataTextField = "Directory_Name";
        ddlDirectory.DataValueField = "Id";
        ddlDirectory.DataBind();
        ddlDirectory.Items.Insert(0, "--Select--");

    }
    void BindFiletypeList()
    {
        DataTable Dtfiletype = new DataTable();
        string Filetypeid = "0";
        Dtfiletype = ObjFileType.Get_FileType(strCompId, Filetypeid);
        ddlFiletype.DataSource = Dtfiletype;
        ddlFiletype.DataTextField = "File_type";
        ddlFiletype.DataValueField = "Id";
        ddlFiletype.DataBind();
        ddlFiletype.Items.Insert(0, "--Select--");
    }



    public void FillGrid()
    {
        DataTable dt = ObjFile.Get_FileTransaction(strCompId, "0");
        //dt = new DataView(dt, "Brand_Id='" + Session["BrandId"].ToString() + "' and Location_Id='" + Session["LocId"].ToString() + "' ", "", DataViewRowState.CurrentRows).ToTable();

        gvFileMaster.DataSource = dt;
        gvFileMaster.DataBind();
        AllPageCode();
        Session["dtFilter"] = dt;
        Session["File"] = dt;

        lblTotalRecords.Text = Resources.Attendance.Total_Records + ": " + dt.Rows.Count.ToString() + "";

    }
    public void FillGridBin()
    {
        string FileTypeId = "0";

        DataTable dt = new DataTable();
        dt = ObjFile.Get_FileTransactionInActive(strCompId, FileTypeId);
        //dt = new DataView(dt, "Brand_Id='" + Session["BrandId"].ToString() + "' and Location_Id='" + Session["LocId"].ToString() + "' ", "", DataViewRowState.CurrentRows).ToTable();

        gvFileMasterBin.DataSource = dt;
        gvFileMasterBin.DataBind();


        Session["dtbinFilter"] = dt;
        Session["dtbinFile"] = dt;
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
    protected void gvDepMaster_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        gvFileMaster.PageIndex = e.NewPageIndex;

        DataTable dt = (DataTable)Session["dtFilter"];
        gvFileMaster.DataSource = dt;
        gvFileMaster.DataBind();
        AllPageCode();

    }
    protected void gvDepMaster_OnSorting(object sender, GridViewSortEventArgs e)
    {
        HDFSort.Value = HDFSort.Value == "ASC" ? "DESC" : "ASC";
        DataTable dt = new DataTable();
        dt = (DataTable)Session["dtFilter"];
        DataView dv = new DataView(dt);
        string Query = "" + e.SortExpression + " " + HDFSort.Value + "";
        dv.Sort = Query;
        dt = dv.ToTable();
        Session["dtFilter"] = dt;
        gvFileMaster.DataSource = dt;
        gvFileMaster.DataBind();
        AllPageCode();
    }
    protected void gvDepMasterBin_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {

        gvFileMasterBin.PageIndex = e.NewPageIndex;
        if (HDFSortbin.Value == "")
            FillGridBin();
        else
        {
            DataTable dt = (DataTable)Session["dtbinFilter"];
            gvFileMasterBin.DataSource = dt;
            gvFileMasterBin.DataBind();
            AllPageCode();
        }
        string temp = string.Empty;
        bool isselcted;

        for (int i = 0; i < gvFileMasterBin.Rows.Count; i++)
        {
            Label lblconid = (Label)gvFileMasterBin.Rows[i].FindControl("lblFileId");
            string[] split = lblSelectedRecord.Text.Split(',');

            for (int j = 0; j < lblSelectedRecord.Text.Split(',').Length; j++)
            {
                if (lblSelectedRecord.Text.Split(',')[j] != "")
                {
                    if (lblconid.Text.Trim().ToString() == lblSelectedRecord.Text.Split(',')[j].Trim().ToString())
                    {
                        ((CheckBox)gvFileMasterBin.Rows[i].FindControl("chkgvSelect")).Checked = true;
                    }
                }
            }
        }

    }
    protected void gvDepMasterBin_OnSorting(object sender, GridViewSortEventArgs e)
    {
        string FileTypeid = "0";
        lblSelectedRecord.Text = "";
        HDFSortbin.Value = HDFSortbin.Value == "ASC" ? "DESC" : "ASC";
        DataTable dt = new DataTable();
        dt = ObjFile.Get_FileTransactionInActive(strCompId, FileTypeid);
        //dt = new DataView(dt, "Brand_Id='" + Session["BrandId"].ToString() + "' and Location_Id='" + Session["LocId"].ToString() + "' ", "", DataViewRowState.CurrentRows).ToTable();

        DataView dv = new DataView(dt);
        string Query = "" + e.SortExpression + " " + HDFSortbin.Value + "";
        dv.Sort = Query;
        dt = dv.ToTable();
        Session["dtbinFilter"] = dt;
        gvFileMasterBin.DataSource = dt;
        gvFileMasterBin.DataBind();
        AllPageCode();

    }

    protected void chkgvSelectAll_CheckedChanged(object sender, EventArgs e)
    {
        CheckBox chkSelAll = ((CheckBox)gvFileMasterBin.HeaderRow.FindControl("chkgvSelectAll"));
        for (int i = 0; i < gvFileMasterBin.Rows.Count; i++)
        {
            ((CheckBox)gvFileMasterBin.Rows[i].FindControl("chkgvSelect")).Checked = chkSelAll.Checked;
            if (chkSelAll.Checked)
            {
                if (!lblSelectedRecord.Text.Split(',').Contains(((Label)(gvFileMasterBin.Rows[i].FindControl("lblFileId"))).Text.Trim().ToString()))
                {
                    lblSelectedRecord.Text += ((Label)(gvFileMasterBin.Rows[i].FindControl("lblFileId"))).Text.Trim().ToString() + ",";
                }
            }
            else
            {
                string temp = string.Empty;
                string[] split = lblSelectedRecord.Text.Split(',');
                foreach (string item in split)
                {
                    if (item != ((Label)(gvFileMasterBin.Rows[i].FindControl("lblFileId"))).Text.Trim().ToString())
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
        Label lb = (Label)gvFileMasterBin.Rows[index].FindControl("lblFileId");
        if (((CheckBox)gvFileMasterBin.Rows[index].FindControl("chkgvSelect")).Checked)
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
    private void download(DataTable dt)
    {
        Byte[] bytes = (Byte[])dt.Rows[0]["File_Data"];
        Response.Buffer = true;
        Response.Charset = "";
        Response.Cache.SetCacheability(HttpCacheability.NoCache);
        //Response.ContentType = dt.Rows[0]["ContentType"].ToString();
        Response.AddHeader("content-disposition", "attachment;filename="
        + dt.Rows[0]["File_Name"].ToString());
        Response.BinaryWrite(bytes);
        Response.Flush();
        Response.End();
    }


    protected void OnDownloadCommand(object sender, CommandEventArgs e)
    {
        editid.Value = e.CommandArgument.ToString();
        DataTable dt = new DataTable();

        dt = ObjFile.Get_FileTransaction_By_TransactionId(strCompId, editid.Value);
        download(dt);
        Reset();
    }

    protected void btnEdit_Command(object sender, CommandEventArgs e)
    {
        editid.Value = e.CommandArgument.ToString();


        DataTable dt = ObjFile.Get_FileTransaction_By_TransactionId(strCompId, editid.Value);

        if (dt.Rows.Count > 0)
        {

            try
            {
                ddlDirectory.SelectedValue = dt.Rows[0]["Directory_Id"].ToString();
                ddlDocumentName.SelectedValue = dt.Rows[0]["Document_Master_Id"].ToString();
                ddlFiletype.SelectedValue = dt.Rows[0]["File_Type_id"].ToString();
                //txtFileName.Text = dt.Rows[0]["File_Name"].ToString();
                string FileName = dt.Rows[0]["File_Name"].ToString();
                string FilePath = dt.Rows[0]["File_Path"].ToString();
                string extension = Path.GetExtension(FilePath);
                string result = FileName.Substring(0, FileName.Length - extension.Length);
                txtFileName.Text = result.ToString();
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
        b = ObjFile.Delete_in_FileTransaction(strCompId, e.CommandArgument.ToString(), false.ToString(), Session["UserId"].ToString(), DateTime.Now.ToString());
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
    public void DisplayMessage(string str)
    {
        ScriptManager.RegisterStartupScript(this, GetType(), "", "alert('" + str + "');", true);
    }
    public void Reset()
    {


        txtFileName.Text = "";
        ddlDirectory.SelectedIndex = 0;
        ddlDocumentName.SelectedIndex = 0;
        ddlFiletype.SelectedIndex = 0;

        btnNew.Text = Resources.Attendance.New;
        lblSelectedRecord.Text = "";
        ViewState["Select"] = null;
        editid.Value = "";
        txtValue.Text = "";
        ddlOption.SelectedIndex = 2;
        ddlFieldName.SelectedIndex = 0;
        ddlDirectory.Focus();
        txtExpiryDate.Text = Convert.ToDateTime(System.DateTime.Now.AddYears(20)).ToString("dd-MMM-yyyy");



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

        ddlDirectory.Focus();
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
        ddlFieldName.SelectedIndex = 1;
        txtValue.Text = "";
        txtValue.Focus();
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
            DataTable dtCust = (DataTable)Session["File"];


            DataView view = new DataView(dtCust, condition, "", DataViewRowState.CurrentRows);
            Session["dtFilter"] = view.ToTable();
            lblTotalRecords.Text = Resources.Attendance.Total_Records + ": " + view.ToTable().Rows.Count.ToString() + "";

            gvFileMaster.DataSource = view.ToTable();
            gvFileMaster.DataBind();

            AllPageCode();
            btnRefresh.Focus();

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
            DataTable dtCust = (DataTable)Session["dtbinFile"];


            DataView view = new DataView(dtCust, condition, "", DataViewRowState.CurrentRows);
            Session["dtbinFilter"] = view.ToTable();
            lblbinTotalRecords.Text = Resources.Attendance.Total_Records + ": " + view.ToTable().Rows.Count.ToString() + "";

            gvFileMasterBin.DataSource = view.ToTable();
            gvFileMasterBin.DataBind();


            if (view.ToTable().Rows.Count == 0)
            {
                imgBtnRestore.Visible = false;
                ImgbtnSelectAll.Visible = false;
            }
            else
            {
                AllPageCode();
            }
            btnbinRefresh.Focus();
        }
    }

    protected void btnbinRefresh_Click(object sender, ImageClickEventArgs e)
    {
        FillGrid();
        FillGridBin();

        ddlbinOption.SelectedIndex = 2;
        ddlbinFieldName.SelectedIndex = 1;
        txtbinValue.Text = "";
        lblSelectedRecord.Text = "";
        txtbinValue.Focus();
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
                    b = ObjFile.Delete_in_FileTransaction(strCompId, lblSelectedRecord.Text.Split(',')[j].Trim(), true.ToString(), Session["UserId"].ToString(), DateTime.Now.ToString());

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
            foreach (GridViewRow Gvr in gvFileMasterBin.Rows)
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

    protected void ImgbtnSelectAll_Click(object sender, ImageClickEventArgs e)
    {

        DataTable dtUnit = (DataTable)Session["dtbinFilter"];

        if (ViewState["Select"] == null)
        {
            ViewState["Select"] = 1;
            foreach (DataRow dr in dtUnit.Rows)
            {
                if (!lblSelectedRecord.Text.Split(',').Contains(dr["Trans_Id"]))
                {
                    lblSelectedRecord.Text += dr["Trans_Id"] + ",";
                }
            }
            for (int i = 0; i < gvFileMasterBin.Rows.Count; i++)
            {
                string[] split = lblSelectedRecord.Text.Split(',');
                Label lblconid = (Label)gvFileMasterBin.Rows[i].FindControl("lblFileId");
                for (int j = 0; j < lblSelectedRecord.Text.Split(',').Length; j++)
                {
                    if (lblSelectedRecord.Text.Split(',')[j] != "")
                    {
                        if (lblconid.Text.Trim().ToString() == lblSelectedRecord.Text.Split(',')[j].Trim().ToString())
                        {
                            ((CheckBox)gvFileMasterBin.Rows[i].FindControl("chkgvSelect")).Checked = true;
                        }
                    }
                }
            }
        }
        else
        {
            lblSelectedRecord.Text = "";
            DataTable dtUnit1 = (DataTable)Session["dtBinFilter"];
            gvFileMasterBin.DataSource = dtUnit1;
            gvFileMasterBin.DataBind();
            ViewState["Select"] = null;
        }
    }
    protected void btnSave_Click(object sender, EventArgs e)
    {
        int b = 0;
        if (ddlDirectory.SelectedIndex == 0)
        {
            DisplayMessage("Select Directory Name");
            ddlDirectory.Focus();
            return;
        }
        if (ddlDocumentName.SelectedIndex == 0)
        {
            DisplayMessage("Select Document Name");
            ddlDocumentName.Focus();
            return;
        }


        if (txtFileName.Text == "")
        {
            DisplayMessage("Enter File Name");
            txtFileName.Focus();
            return;
        }


        if (UploadFile.HasFile == false)
        {
            DisplayMessage("Upload The File");
            UploadFile.Focus();
            return;
        }


        string filepath = "~/" + "ArcaWing" + "/" + ddlDirectory.SelectedItem + "/" + UploadFile.FileName;
        UploadFile.SaveAs(Server.MapPath(filepath));
        string filename = UploadFile.FileName;
        string ext = Path.GetExtension(filepath);

        string NewfileName = txtFileName.Text + "" + ext;

        Stream fs = UploadFile.PostedFile.InputStream;
        BinaryReader br = new BinaryReader(fs);
        Byte[] bytes = br.ReadBytes((Int32)fs.Length);

        if (editid.Value == "")
        {
            string FileTypeId = "0";

            DataTable dt = ObjFile.Get_FileTransaction(strCompId, FileTypeId);


            dt = new DataView(dt, "File_Name='" + txtFileName.Text + "'", "", DataViewRowState.CurrentRows).ToTable();
            if (dt.Rows.Count > 0)
            {
                DisplayMessage("File Name Already Exists");
                txtFileName.Focus();
                txtFileName.Text = "";
                return;

            }
            //dt = new DataView(dt, "Directory_Id="+ddlDirectory.SelectedValue+" ", "", DataViewRowState.CurrentRows).ToTable();
            //if (dt.Rows.Count > 0)
            //{
            //    DisplayMessage("Directory Exists");
            //    txtContentName.Focus();
            //    return;

            //}






            b = ObjFile.Insert_In_FileTransaction(strCompId, ddlDirectory.SelectedValue, ddlDocumentName.SelectedValue, "0", NewfileName, DateTime.Now.ToString(), bytes, filepath,txtExpiryDate.Text,"", "0", "", "", "", false.ToString(), DateTime.Now.ToString(), true.ToString(), Session["UserId"].ToString(), DateTime.Now.ToString(), Session["UserId"].ToString(), DateTime.Now.ToString());

            if (b != 0)
            {
                DisplayMessage("Record Saved");
                FillGrid();
                Reset();

            }
            else
            {
                DisplayMessage("Record Not Saved");
            }
        }
        else
        {
            string FileTransactionid = "0";
            DataTable dt = ObjFile.Get_FileTransaction(strCompId, FileTransactionid);


            string FileTransaction = string.Empty;


            try
            {
                FileTransaction = (new DataView(dt, "Trans_Id='" + editid.Value + "'", "", DataViewRowState.CurrentRows).ToTable()).Rows[0]["File_Name"].ToString();
            }
            catch
            {
                FileTransaction = "";
            }
            dt = new DataView(dt, "File_Name='" + txtFileName.Text + "' and File_Name<>'" + FileTransaction + "'  ", "", DataViewRowState.CurrentRows).ToTable();
            if (dt.Rows.Count > 0)
            {
                DisplayMessage("File Name Already Exists");
                txtFileName.Focus();
                txtFileName.Text = "";
                return;

            }
            //dt = new DataView(dt, "Content_Type='" + txtContentName.Text + "'  ", "", DataViewRowState.CurrentRows).ToTable();
            //if (dt.Rows.Count > 0)
            //{
            //    DisplayMessage("Content Name Already Exists");
            //    txtContentName.Focus();
            //    return;

            //}






            b = ObjFile.Update_In_FileTransaction(strCompId, editid.Value, ddlDirectory.SelectedValue, ddlDocumentName.SelectedValue, ddlFiletype.SelectedValue, NewfileName, DateTime.Now.ToString(), bytes, filepath, DateTime.Now.ToString(),"", "0", "", "", "", false.ToString(), DateTime.Now.ToString(), true.ToString(), Session["UserId"].ToString(), DateTime.Now.ToString());

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




}
