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

public partial class HR_EmployeeDirectory : System.Web.UI.Page
{
    Arc_FileTransaction ObjFile = new Arc_FileTransaction();
    Arc_Directory_Master objDir = new Arc_Directory_Master();
    Document_Master ObjDocument = new Document_Master();
    EmployeeMaster ObjEmp = new EmployeeMaster();

    string strCompId = "";
    protected void Page_Load(object sender, EventArgs e)
    {

        strCompId = Session["CompId"].ToString();
        if (!IsPostBack)
        {
            BindDocumentList();
            BindDirectoryList();
            txtExpiryDate.Text = Convert.ToDateTime(System.DateTime.Now.AddYears(20)).ToString("dd-MMM-yyyy");

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
        DtDirectory = objDir.getDirectoryMaster(strCompId, Directoryid);

        ddlDirectory.DataSource = DtDirectory;
        ddlDirectory.DataTextField = "Directory_Name";
        ddlDirectory.DataValueField = "Id";
        ddlDirectory.DataBind();
        ddlDirectory.Items.Insert(0, "--Select--");
        Session["Dir"] = DtDirectory;

    }
    public void FillGrid()
    {
        DataTable dt = ObjFile.Get_FileTransaction(strCompId, "0");
        //dt = new DataView(dt, "Brand_Id='" + Session["BrandId"].ToString() + "' and Location_Id='" + Session["LocId"].ToString() + "' ", "", DataViewRowState.CurrentRows).ToTable();


        Session["File"] = dt;


    }
    public void DisplayMessage(string str)
    {

        ScriptManager.RegisterStartupScript(this, GetType(), "", "alert('" + str + "');", true);

    }
    public int CreateDirectoryIfNotExist(string NewDirectory)
    {
        int checkDirectory = 0;
        try
        {
            // Checking the existance of directory
            if (!Directory.Exists(NewDirectory))
            {
                //If No any such directory then creates the new one
                Directory.CreateDirectory(NewDirectory);


            }
            else
            {

                checkDirectory = 1;
            }
        }
        catch (IOException _err)
        {
            Response.Write(_err.Message);
        }
        return checkDirectory;
    }
    protected void btnSaveLeave_Click(object sender, EventArgs e)
    {
        FillGrid();
        int Directoryid;
        DataTable dt = (DataTable)Session["File"];
        //dt = new DataView(dt, "Brand_Id='" + Session["BrandId"].ToString() + "' and Location_Id='" + Session["LocId"].ToString() + "' ", "", DataViewRowState.CurrentRows).ToTable();


        DataTable DtDirectory = new DataTable();

        DtDirectory = (DataTable)Session["Dir"];

        if (TxtEmployeeId.Text == "")
        {
            TxtEmployeeId.Focus();
            DisplayMessage("Enter Employee Name");
            return;

        }
        TxtEmployeeId.Enabled = false;

        DtDirectory = new DataView(DtDirectory, "Directory_Name ='" + HidEmpId.Value + "'", "", DataViewRowState.CurrentRows).ToTable();

        if (DtDirectory.Rows.Count > 0)
        {
            Directoryid = Convert.ToInt32(DtDirectory.Rows[0]["Id"].ToString());
            dt = new DataView(dt, "Directory_Id =" + Directoryid + "", "", DataViewRowState.CurrentRows).ToTable();
        }

        if (DtDirectory.Rows.Count > 0)
        {

            if (dt.Rows.Count > 0)
            {
                gvFileMaster.DataSource = dt;
                gvFileMaster.DataBind();
            }
            else
            {
                dt.Clear();
                gvFileMaster.DataSource = dt;
                gvFileMaster.DataBind();
                DisplayMessage("Directory Exists");
            }
            ddlDirectory.SelectedValue = DtDirectory.Rows[0]["Id"].ToString();


        }
        else
        {
            DataTable Dtclear = new DataTable();
            gvFileMaster.DataSource = Dtclear;
            gvFileMaster.DataBind();
            int b = 0;
            string NewDirectory = Server.MapPath(HidEmpId.Value);
            int i = CreateDirectoryIfNotExist(NewDirectory.ToString());
            DisplayMessage("Directory Created");
            b = objDir.InsertDirectorymaster(strCompId, HidEmpId.Value, "", "0", "", "", "", false.ToString(), DateTime.Now.ToString(), true.ToString(), Session["UserId"].ToString(), DateTime.Now.ToString(), Session["UserId"].ToString(), DateTime.Now.ToString());


            BindDirectoryList();
            DataTable DtDir = new DataTable();
            string Dirid = "0";
            DtDir = objDir.getDirectoryMaster(strCompId, Dirid);
            DtDir = new DataView(DtDir, "Directory_Name ='" + HidEmpId.Value + "'", "", DataViewRowState.CurrentRows).ToTable();



            ddlDirectory.SelectedValue = DtDir.Rows[0]["Id"].ToString();
        }
    }
    protected void btnReset_Click(object sender, EventArgs e)
    {
        Reset();
    }
    protected void btnSave_Click(object sender, EventArgs e)
    {

        int b = 0;

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


        string filepath = "~/" + "HR" + "/" + HidEmpId.Value + "/" + UploadFile.FileName;
        UploadFile.SaveAs(Server.MapPath(filepath));
        string filename = UploadFile.FileName;
        string ext = Path.GetExtension(filepath);

        string NewfileName = txtFileName.Text + "" + ext;

        Stream fs = UploadFile.PostedFile.InputStream;
        BinaryReader br = new BinaryReader(fs);
        Byte[] bytes = br.ReadBytes((Int32)fs.Length);

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

        b = ObjFile.Insert_In_FileTransaction(strCompId, ddlDirectory.SelectedValue, ddlDocumentName.SelectedValue, "0", NewfileName, DateTime.Now.ToString(), bytes, filepath, txtExpiryDate.Text, "", "0", "", "", "", false.ToString(), DateTime.Now.ToString(), true.ToString(), Session["UserId"].ToString(), DateTime.Now.ToString(), Session["UserId"].ToString(), DateTime.Now.ToString());

        if (b != 0)
        {
            DisplayMessage("Record Saved");
            FillGrid();
            DataTable DtFile = (DataTable)Session["File"];
            DtFile = new DataView(DtFile, "Directory_Id =" + ddlDirectory.SelectedValue + "", "", DataViewRowState.CurrentRows).ToTable();

            gvFileMaster.DataSource = DtFile;
            gvFileMaster.DataBind();
            Reset();
            Paneladddocument.Visible = false;

        }
        else
        {
            DisplayMessage("Record Not Saved");
        }
    }
    public void Reset()
    {
        txtFileName.Text = "";
        txtExpiryDate.Text = Convert.ToDateTime(System.DateTime.Now.AddYears(20)).ToString("dd-MMM-yyyy");


        Paneladddocument.Visible = false;
        ddlDocumentName.SelectedIndex = 0;
        Paneladddocument.Visible = false;
        ViewState["Select"] = null;
        editid.Value = "";
        ddlDirectory.Focus();
        Paneladddocument.Visible = false;
    }
    protected void btnCancel_Click(object sender, EventArgs e)
    {
        Paneladddocument.Visible = false;
    }
    protected void btnAddDocument_Click(object sender, EventArgs e)
    {
        if (TxtEmployeeId.Enabled == true)
        {
            DisplayMessage("First Of All Search Directory");
            TxtEmployeeId.Focus();

            return;
        }

        Paneladddocument.Visible = true;

        ddlDocumentName.Focus();
    }
    protected void BtnEmployeeReset_Click(object sender, EventArgs e)
    {
        Reset();
        HidEmpId.Value = "";
        TxtEmployeeId.Enabled = true;
        TxtEmployeeId.Text = "";
        ddlDirectory.SelectedIndex = 0;
        DataTable dtclear = new DataTable();
        gvFileMaster.DataSource = dtclear;
        gvFileMaster.DataBind();
    }
    [System.Web.Services.WebMethodAttribute(), System.Web.Script.Services.ScriptMethodAttribute()]
    public static string[] GetCompletionListEmployeeName(string prefixText, int count, string contextKey)
    {
        DataTable dt = Common.GetEmployee(prefixText, HttpContext.Current.Session["CompId"].ToString());

        string[] str = new string[dt.Rows.Count];
        for (int i = 0; i < dt.Rows.Count; i++)
        {
            str[i] = "" + dt.Rows[i][1].ToString() + "/(" + dt.Rows[i][2].ToString() + ")/" + dt.Rows[i][0].ToString() + "";
        }
        return str;
    }

    protected void TxtEmployeeId_TextChanged(object sender, EventArgs e)
    {

        string empid = string.Empty;
        if (TxtEmployeeId.Text != "")
        {
            empid = TxtEmployeeId.Text.Split('/')[TxtEmployeeId.Text.Split('/').Length - 1];

            DataTable dtEmp = ObjEmp.GetEmployeeMaster(strCompId);

            dtEmp = new DataView(dtEmp, "Emp_Code='" + empid + "'", "", DataViewRowState.CurrentRows).ToTable();

            if (dtEmp.Rows.Count > 0)
            {
                empid = dtEmp.Rows[0]["Emp_Id"].ToString();
                HidEmpId.Value = empid;
            }
            else
            {
                DisplayMessage("Employee Not Exists");
                TxtEmployeeId.Text = "";
                TxtEmployeeId.Focus();
                HidEmpId.Value = "";
                return;
            }
        }

    }
}
