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

public partial class Academic_StudentAdmissionMaster : BasePage
{
    Common cmn = new Common();
    SystemParameter objSys = new SystemParameter();
    StudentAdmissionMaster objStudent = new StudentAdmissionMaster();
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

        }
    }

    public void AllPageCode()
    {
        Page.Title = objSys.GetSysTitle();

        Session["AccordianId"] = "8";
        Session["HeaderText"] = "Master Setup";
        DataTable dtAllPageCode = cmn.GetAllPagePermission(Session["UserId"].ToString(), "8", "46");
        if (dtAllPageCode.Rows.Count != 0)
        {
            if (dtAllPageCode.Rows[0][0].ToString() == "SuperAdmin")
            {
                btnSave.Visible = true;
                foreach (GridViewRow Row in gvStudentMaster.Rows)
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
                    foreach (GridViewRow Row in gvStudentMaster.Rows)
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
        txtStudent_Code.Focus();
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
            DataTable dtCust = (DataTable)Session["Bank"];


            DataView view = new DataView(dtCust, condition, "", DataViewRowState.CurrentRows);
            Session["dtFilter"] = view.ToTable();
            lblTotalRecords.Text = Resources.Attendance.Total_Records + ": " + view.ToTable().Rows.Count.ToString() + "";

            gvStudentMaster.DataSource = view.ToTable();
            gvStudentMaster.DataBind();
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
            DataTable dtCust = (DataTable)Session["dtbinBank"];


            DataView view = new DataView(dtCust, condition, "", DataViewRowState.CurrentRows);
            Session["dtbinFilter"] = view.ToTable();
            lblbinTotalRecords.Text = Resources.Attendance.Total_Records + ": " + view.ToTable().Rows.Count.ToString() + "";

            gvStudentMasterBin.DataSource = view.ToTable();
            gvStudentMasterBin.DataBind();


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

    public string getImageByStudentId(object StudentId)

    {
        string img = string.Empty;
        DataTable dt = objStudent.GetStudentAdmissionMaster(Session["CompId"].ToString());

        dt = new DataView(dt, "Student_Id='" + StudentId.ToString().Trim() + "'", "", DataViewRowState.CurrentRows).ToTable();

        if (dt.Rows.Count > 0)
        {

            if (dt.Rows[0]["Photo"].ToString() != "")
            {
                img = "~/CompanyResource/" + Session["CompId"] + "/" + dt.Rows[0]["Photo"].ToString();
            }
            else
            {
                img = "~/CompanyResource/User.png";

            }

        }
        return img;
    }
    public string getImagesByStudentId(object StudentId)

    {
        string img = string.Empty;
        DataTable dt = objStudent.GetStudentAdmissionMaster(Session["CompId"].ToString());

        dt = new DataView(dt, "Student_Id='" + StudentId.ToString().Trim() + "'", "", DataViewRowState.CurrentRows).ToTable();

        if (dt.Rows.Count > 0)
        {

            if (dt.Rows[0]["Sig1"].ToString() != "")
            {
                img = "~/CompanyResource/" + Session["CompId"] + "/" + dt.Rows[0]["Sig1"].ToString();
            }
            else
            {
                img = "~/CompanyResource/User.png";

            }

        }
        return img;
    }
    public string getImagessByStudentId(object StudentId)
    {
        string img = string.Empty;
        DataTable dt = objStudent.GetStudentAdmissionMaster(Session["CompId"].ToString());

        dt = new DataView(dt, "Student_Id='" + StudentId.ToString().Trim() + "'", "", DataViewRowState.CurrentRows).ToTable();

        if (dt.Rows.Count > 0)
        {

            if (dt.Rows[0]["Sig2"].ToString() != "")
            {
                img = "~/CompanyResource/" + Session["CompId"] + "/" + dt.Rows[0]["Sig2"].ToString();
            }
            else
            {
                img = "~/CompanyResource/User.png";

            }

        }
        return img;
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

    protected void gvStudentMasterBin_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {

        gvStudentMasterBin.PageIndex = e.NewPageIndex;
        if (HDFSortbin.Value == "")
            FillGridBin();
        else
        {
            DataTable dt = (DataTable)Session["dtbinFilter"];
            gvStudentMasterBin.DataSource = dt;
            gvStudentMasterBin.DataBind();
            AllPageCode();
        }
        string temp = string.Empty;
        bool isselcted;

        for (int i = 0; i < gvStudentMasterBin.Rows.Count; i++)
        {
            Label lblconid = (Label)gvStudentMasterBin.Rows[i].FindControl("lblStudentId");
            string[] split = lblSelectedRecord.Text.Split(',');

            for (int j = 0; j < lblSelectedRecord.Text.Split(',').Length; j++)
            {
                if (lblSelectedRecord.Text.Split(',')[j] != "")
                {
                    if (lblconid.Text.Trim().ToString() == lblSelectedRecord.Text.Split(',')[j].Trim().ToString())
                    {
                        ((CheckBox)gvStudentMasterBin.Rows[i].FindControl("chkgvSelect")).Checked = true;
                    }
                }
            }
        }

    }
    protected void gvStudentMasterBin_OnSorting(object sender, GridViewSortEventArgs e)
    {
        lblSelectedRecord.Text = "";
        HDFSortbin.Value = HDFSortbin.Value == "ASC" ? "DESC" : "ASC";
        DataTable dt = new DataTable();
        dt = objStudent.GetStudentAdmissionMasterInactive(Session["CompId"].ToString());
        DataView dv = new DataView(dt);
        string Query = "" + e.SortExpression + " " + HDFSortbin.Value + "";
        dv.Sort = Query;
        dt = dv.ToTable();
        Session["dtbinFilter"] = dt;
        gvStudentMasterBin.DataSource = dt;
        gvStudentMasterBin.DataBind();
        AllPageCode();

    }
    protected void btnSave_Click(object sender, EventArgs e)
    {
        int b = 0;
        if (txtSr_No.Text == "")
        {
            DisplayMessage("Enter Sr No.");
            txtSr_No.Focus();
            return;
        }
        if (txtStudent_Code.Text == "")
        {
            DisplayMessage("Enter Student Code");
            txtStudent_Code.Focus();
            return;
        }
        if (txtFormNo.Text == "")
        {
            DisplayMessage("Enter Form No");
            txtFormNo.Focus();
            return;
        }

        if (txtDob.Text == "")
        {
            txtDob.Text = DateTime.Now.ToString();

        }
        else
        {
            try
            {
                Convert.ToDateTime(txtDob.Text);

            }
            catch
            {
                DisplayMessage("Enter Correct Date of Birth Format");
                txtDob.Focus();
                return;

            }


        }
        if (txtDoa.Text == "")
        {
            txtDoa.Text = DateTime.Now.ToString();

        }
        else
        {
            try
            {
                Convert.ToDateTime(txtDoa.Text);

            }
            catch
            {
                DisplayMessage("Enter Correct Date of Admission Format");
                txtDob.Focus();
                return;

            }


        }
        if (txtEmail.Text != "")
        {
            if (!IsValidEmail(txtEmail.Text))
            {
                DisplayMessage("Enter Correct Email Id Format");
                txtEmail.Focus();
                return;

            }

        }
        if (Session["empimgpath"] == null)
        {
            Session["empimgpath"] = "";

        }
        if (Session["empimgpath"] == null)
        {
            Session["empimgpath"] = "";

        }
        if (Session["sigpath"] == null)
        {
            Session["sigpath"] = "";

        }
        if (Session["sigpath"] == null)
        {
            Session["sigpath"] = "";

        }
        if (Session["sigpath1"] == null)
        {
            Session["sigpath1"] = "";

        }
        if (Session["sigpath1"] == null)
        {
            Session["sigpath1"] = "";

        }

      
       
        if (editid.Value == "")
        {
            DataTable dt = objStudent.GetStudentAdmissionMaster(Session["CompId"].ToString());

            dt = new DataView(dt, "Sr_No ='" + txtSr_No.Text + "'", "", DataViewRowState.CurrentRows).ToTable();
            if (dt.Rows.Count > 0)
            {
                DisplayMessage("SR No Already Exists");
                txtSr_No.Focus();
                return;

            }
            DataTable dt1 = objStudent.GetStudentAdmissionMaster(Session["CompId"].ToString());

            dt1 = new DataView(dt1, "Student_Code='" + txtStudent_Code.Text + "'", "", DataViewRowState.CurrentRows).ToTable();
            if (dt1.Rows.Count > 0)
            {
                DisplayMessage("Student Code Already Exists");
                txtStudent_Code.Focus();
                return;

            }
            DataTable dt2 = objStudent.GetStudentAdmissionMaster(Session["CompId"].ToString());

            dt1 = new DataView(dt2, "FormNo='" + txtFormNo.Text + "'", "", DataViewRowState.CurrentRows).ToTable();
            if (dt2.Rows.Count > 0)
            {
                DisplayMessage("Form No Already Exists");
                txtFormNo.Focus();
                return;

            }
        b = objStudent.InsertStudentAdmissionMaster(Session["CompId"].ToString(),txtStudent_Code.Text,txtStudent_Name.Text, txtStudent_Name_L.Text, txtFormNo.Text, txtSr_No.Text, txtReg_No.Text, ddlCategory.SelectedValue, txtEnroll_No.Text, txtPermittedBy.Text, txtReligion.Text, txtNationality.Text, ddlCourse.SelectedValue,ddlclass.SelectedValue, txtDoa.Text, txtDob.Text, txtPlace.Text, txtCaste.Text, ddlStatus.SelectedValue, ddlPH.SelectedValue, txtStreet.Text, txtVillage.Text, txtGrampanchayat.Text, txtTehsil.Text, txtDistrict.Text, txtState.Text, txtPost.Text, txtPinCode.Text, txtPhoneNo.Text, txtMobile1.Text, txtMobile2.Text, txtEmail.Text, txtBankName.Text, txtBranch.Text, txtAccountNo.Text, txtFather_Name.Text, txtProfession.Text, txtPosition.Text, txtToatalIncome.Text, txtOfficePhoneNo.Text, txtMobileNo3.Text, txtOfficeAddr.Text, txtMother_Name.Text, txtProfession1.Text, txtPosition1.Text, txtToatalIncome1.Text, txtOfficePhoneNo1.Text, txtMobileNo4.Text, txtOfficeAddr1.Text, txtLocalGuardian.Text, txtMobileNo5.Text, txtAddress.Text, txtVillage1.Text,txtDocotor.Text, txtMobileNo6.Text, txtExam.Text, txtYear.Text, txtRollNo.Text, txtEnrNo.Text, txtX_Board.Text, txtXII_Board.Text, txtGraducation.Text, txtPostGraducation.Text, txtOtherBoard.Text, txtX_Inst.Text, txtXII_Inst.Text, txtGrd_Inst.Text, txtPost_Inst.Text, txtOther_Inst.Text, txtX_Year.Text, txtXII_Year.Text, txtGrd_Year.Text, txtPost_Year.Text, txtOther_Year.Text, txtX_MM.Text, txtXII_MM.Text, txtGrd_MM.Text, txtPost_MM.Text, txtOther_MM.Text, txtX_Obt.Text, txtXII_Obt.Text, txtGrd_Obt.Text, txtPost_Obt.Text, txtOther_Obt.Text, txtX_Per.Text, txtXII_Per.Text, txtGrd_Per.Text, txtPost_Per.Text, txtOther_Per.Text, txtCompSub1.Text, txtCompSub2.Text,txtCompSub3.Text, txtCompSub4.Text, txtOpSub1.Text, txtOpSub2.Text, txtOpSu3.Text, txtOpSub4.Text, txtOpSub5.Text, txtOpSub6.Text, txtOpSub7.Text, txtOpSub8.Text, txtOpSub9.Text, txtOpSub10.Text, txtOpSub11.Text, txtOpSub12.Text, txtOpSub13.Text, txtOpSub14.Text, txtOpSu15.Text, txtBloodGroup.Text, txtYera1.Text, txtYear2.Text, txtDC.Text, txtDCompSub1.Text, txtDCompSub2.Text, txtDCompSub3.Text, txtDCompSub4.Text, txtDOpsub1.Text, txtDOpsub2.Text, txtDOpsub3.Text, txtNoe1.Text, txtNoe2.Text, txtNoi1.Text, txtNoi2.Text, txtBoard1.Text, txtBoard2.Text, txtRollNo1.Text, txtRollNo2.Text, txtResult1.Text, txtResult2.Text, Session["empimgpath"].ToString(), Session["sigpath"].ToString(), Session["sigpath1"].ToString(),txtMq1.Text, txtTc.Text, txtMig.Text, txtsc.Text, txtSports.Text, txtBonafide.Text, txtSE.Text, txtCC.Text, txtPP.Text, txtPAR.Text, txtNCC.Text, "", "", "", "", "", true.ToString(), DateTime.Now.ToString(), true.ToString(), Session["UserId"].ToString(), DateTime.Now.ToString(), Session["UserId"].ToString(), DateTime.Now.ToString());
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

            DataTable dt = objStudent.GetStudentAdmissionMaster(Session["CompId"].ToString());

            DataTable dt3 = objStudent.GetStudentAdmissionMasterById(Session["CompId"].ToString(), editid.Value);
            string FormNo = string.Empty;
            string StudentCode = string.Empty;
            string SrNo = string.Empty;
            if (dt3.Rows.Count > 0)
            {
                SrNo = dt3.Rows[0]["Sr_No"].ToString();
                StudentCode = dt3.Rows[0]["Student_Code"].ToString();
                FormNo = dt3.Rows[0]["FormNo"].ToString();

            }


            dt = new DataView(dt, "Sr_No='" + txtStudent_Code.Text + "' and Sr_No<>'" + SrNo + "'", "", DataViewRowState.CurrentRows).ToTable();
            if (dt.Rows.Count > 0)
            {
                DisplayMessage("SR No Already Exists");
                txtSr_No.Focus();
                return;

            }
            DataTable dt1 = objStudent.GetStudentAdmissionMaster(Session["CompId"].ToString());

            dt1 = new DataView(dt1, "Student_Code='" + txtStudent_Code.Text + "' and Student_Code<>'" + StudentCode + "'", "", DataViewRowState.CurrentRows).ToTable();
            if (dt1.Rows.Count > 0)
            {
                DisplayMessage("Student Code Already Exists");
                txtStudent_Code.Focus();
                return;

            }
            DataTable dt2 = objStudent.GetStudentAdmissionMaster(Session["CompId"].ToString());

            dt1 = new DataView(dt2, "FormNo='" + txtStudent_Code.Text + "' and FormNo<>'" + FormNo + "'", "", DataViewRowState.CurrentRows).ToTable();
            if (dt1.Rows.Count > 0)
            {
                DisplayMessage("Form No Already Exists");
                txtFormNo.Focus();
                return;

            }

            b = objStudent.UpdateStudentAdmissionMaster(Session["CompId"].ToString(), editid.Value, txtStudent_Code.Text, txtStudent_Name.Text, txtStudent_Name_L.Text, txtFormNo.Text, txtSr_No.Text, txtReg_No.Text, ddlCategory.SelectedValue, txtEnroll_No.Text, txtPermittedBy.Text, txtReligion.Text, txtNationality.Text, ddlCourse.SelectedValue, ddlclass.SelectedValue, txtDoa.Text, txtDob.Text, txtPlace.Text, txtCaste.Text, ddlStatus.SelectedValue, ddlPH.SelectedValue, txtStreet.Text, txtVillage.Text, txtGrampanchayat.Text, txtTehsil.Text, txtDistrict.Text, txtState.Text, txtPost.Text, txtPinCode.Text, txtPhoneNo.Text, txtMobile1.Text, txtMobile2.Text, txtEmail.Text, txtBankName.Text, txtBranch.Text, txtAccountNo.Text, txtFather_Name.Text, txtProfession.Text, txtPosition.Text, txtToatalIncome.Text, txtOfficePhoneNo.Text, txtMobileNo3.Text, txtOfficeAddr.Text, txtMother_Name.Text, txtProfession1.Text, txtPosition1.Text, txtToatalIncome1.Text, txtOfficePhoneNo1.Text, txtMobileNo4.Text, txtOfficeAddr1.Text, txtLocalGuardian.Text, txtMobileNo5.Text, txtAddress.Text, txtVillage1.Text, txtDocotor.Text, txtMobileNo6.Text, txtExam.Text, txtYear.Text, txtRollNo.Text, txtEnrNo.Text, txtX_Board.Text, txtXII_Board.Text, txtGraducation.Text, txtPostGraducation.Text, txtOtherBoard.Text, txtX_Inst.Text, txtXII_Inst.Text, txtGrd_Inst.Text, txtPost_Inst.Text, txtOther_Inst.Text, txtX_Year.Text, txtXII_Year.Text, txtGrd_Year.Text, txtPost_Year.Text, txtOther_Year.Text, txtX_MM.Text, txtXII_MM.Text, txtGrd_MM.Text, txtPost_MM.Text, txtOther_MM.Text, txtX_Obt.Text, txtXII_Obt.Text, txtGrd_Obt.Text, txtPost_Obt.Text, txtOther_Obt.Text, txtX_Per.Text, txtXII_Per.Text, txtGrd_Per.Text, txtPost_Per.Text, txtOther_Per.Text, txtCompSub1.Text, txtCompSub2.Text, txtCompSub3.Text, txtCompSub4.Text, txtOpSub1.Text, txtOpSub2.Text, txtOpSu3.Text, txtOpSub4.Text, txtOpSub5.Text, txtOpSub6.Text, txtOpSub7.Text, txtOpSub8.Text, txtOpSub9.Text, txtOpSub10.Text, txtOpSub11.Text, txtOpSub12.Text, txtOpSub13.Text, txtOpSub14.Text, txtOpSu15.Text, txtBloodGroup.Text, txtYera1.Text, txtYear2.Text, txtDC.Text, txtDCompSub1.Text, txtDCompSub2.Text, txtDCompSub3.Text, txtDCompSub4.Text, txtDOpsub1.Text, txtDOpsub2.Text, txtDOpsub3.Text, txtNoe1.Text, txtNoe2.Text, txtNoi1.Text, txtNoi2.Text, txtBoard1.Text, txtBoard2.Text, txtRollNo1.Text, txtRollNo2.Text, txtResult1.Text, txtResult2.Text, Session["empimgpath"].ToString(), Session["sigpath"].ToString(), Session["sigpath1"].ToString(), txtMq1.Text, txtTc.Text, txtMig.Text, txtsc.Text, txtSports.Text, txtBonafide.Text, txtSE.Text, txtCC.Text, txtPP.Text, txtPAR.Text, txtNCC.Text, "", "", "", "", "", true.ToString(), DateTime.Now.ToString(), true.ToString(), Session["UserId"].ToString(), DateTime.Now.ToString());

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

    bool IsValidEmail(string email)
    {
        try
        {
            var addr = new System.Net.Mail.MailAddress(email);
            return true;
        }
        catch
        {
            return false;
        }
    }

    protected void btnEdit_Command(object sender, CommandEventArgs e)
    {
        editid.Value = e.CommandArgument.ToString();
         Session["empimgpath"] = null;

        DataTable dt = objStudent.GetStudentAdmissionMasterById(Session["CompId"].ToString(), editid.Value);
        if (dt.Rows.Count > 0)
        {
            txtStudent_Name.Text = dt.Rows[0]["Student_Name"].ToString();
            txtStudent_Name_L.Text = dt.Rows[0]["Student_Name_L"].ToString();
            txtFormNo.Text = dt.Rows[0]["FormNo"].ToString();
            txtSr_No.Text = dt.Rows[0]["Sr_No"].ToString();
            txtStudent_Code.Text = dt.Rows[0]["Student_Code"].ToString();
            txtReg_No.Text = dt.Rows[0]["Reg_No"].ToString();
            ddlCategory.SelectedValue = dt.Rows[0]["category"].ToString();
            txtEnroll_No.Text = dt.Rows[0]["Enroll_No"].ToString();
            txtPermittedBy.Text= dt.Rows[0]["PermittedBy"].ToString();
             txtReligion.Text= dt.Rows[0]["Religion"].ToString();
txtNationality.Text= dt.Rows[0]["Nationality"].ToString();
ddlCourse.SelectedValue= dt.Rows[0]["Course"].ToString();
ddlclass.SelectedValue = dt.Rows[0]["Class"].ToString();
txtDoa.Text = Convert.ToDateTime(dt.Rows[0]["DOB"].ToString()).ToString(objSys.SetDateFormat());
txtDob.Text = Convert.ToDateTime(dt.Rows[0]["DOJ"].ToString()).ToString(objSys.SetDateFormat());
txtPlace.Text = dt.Rows[0]["Place"].ToString();
txtCaste.Text = dt.Rows[0]["Caste"].ToString();
ddlStatus.SelectedValue = dt.Rows[0]["Status"].ToString();
ddlPH.SelectedValue = dt.Rows[0]["PH"].ToString();
txtStreet.Text = dt.Rows[0]["Street"].ToString();
txtVillage.Text = dt.Rows[0]["Village"].ToString();
txtGrampanchayat.Text = dt.Rows[0]["Grampanchayat"].ToString();
txtTehsil.Text = dt.Rows[0]["Tehsil"].ToString();
txtDistrict.Text = dt.Rows[0]["District"].ToString();
txtState.Text = dt.Rows[0]["State"].ToString();
txtPost.Text = dt.Rows[0]["Post"].ToString();
txtPinCode.Text = dt.Rows[0]["PinCode"].ToString();
txtPhoneNo.Text = dt.Rows[0]["PhoneNo"].ToString();
txtMobile1.Text = dt.Rows[0]["Mobile1"].ToString();
txtMobile2.Text = dt.Rows[0]["Mobile2"].ToString();
txtEmail.Text = dt.Rows[0]["Email"].ToString();
txtBankName.Text = dt.Rows[0]["BankName"].ToString();
  txtBranch.Text = dt.Rows[0]["Branch"].ToString();
    txtAccountNo.Text = dt.Rows[0]["AccountNo"].ToString();
    txtFather_Name.Text = dt.Rows[0]["Father_Name"].ToString();
      txtProfession.Text = dt.Rows[0]["Profession"].ToString();
    txtPosition.Text = dt.Rows[0]["Position"].ToString();
    txtToatalIncome.Text = dt.Rows[0]["ToatalIncom"].ToString();
    txtOfficePhoneNo.Text = dt.Rows[0]["OfficePhoneNo"].ToString();
    txtMobileNo3.Text = dt.Rows[0]["MobileNo3"].ToString();
    txtOfficeAddr.Text = dt.Rows[0]["OfficeAddr"].ToString();
    txtMother_Name.Text = dt.Rows[0]["Mother_Name"].ToString();
    txtProfession1.Text = dt.Rows[0]["Profession1"].ToString();
     txtPosition1.Text = dt.Rows[0]["Position1"].ToString();
    txtToatalIncome1.Text = dt.Rows[0]["ToatalIncome1"].ToString();
    txtOfficePhoneNo1.Text = dt.Rows[0]["OfficePhoneNo1"].ToString();
    txtMobileNo4.Text = dt.Rows[0]["MobileNo4"].ToString();
    txtOfficeAddr1.Text = dt.Rows[0]["OfficeAddr1"].ToString();
    txtLocalGuardian.Text = dt.Rows[0]["LocalGuardian"].ToString();
      txtMobileNo5.Text = dt.Rows[0]["MobileNo5."].ToString();
      txtAddress.Text = dt.Rows[0]["Address"].ToString();
      txtVillage1.Text = dt.Rows[0]["Village1"].ToString();
      txtDocotor.Text = dt.Rows[0]["Docotor"].ToString();
      txtMobileNo6.Text = dt.Rows[0]["MobileNo6"].ToString();
      txtExam.Text = dt.Rows[0]["Exam"].ToString();
     txtYear.Text = dt.Rows[0]["Year"].ToString();
     txtRollNo.Text = dt.Rows[0]["RollNo"].ToString();
      txtEnrNo.Text = dt.Rows[0]["EnrNo"].ToString();
      txtX_Board.Text = dt.Rows[0]["X_Board"].ToString();
      txtXII_Board.Text = dt.Rows[0]["XII_Board."].ToString();
      txtGraducation.Text = dt.Rows[0]["Graducation"].ToString();
       txtPostGraducation.Text = dt.Rows[0]["PostGraducation"].ToString();
       txtOtherBoard.Text = dt.Rows[0]["OtherBoard"].ToString();
       txtX_Inst.Text = dt.Rows[0]["X_Inst"].ToString();
       txtXII_Inst.Text = dt.Rows[0]["XII_Inst"].ToString();
       txtGrd_Inst.Text = dt.Rows[0]["Grd_Inst"].ToString();
       txtPost_Inst.Text = dt.Rows[0]["Post_Inst"].ToString();
       txtOther_Inst.Text = dt.Rows[0]["Other_Inst"].ToString();
       txtX_Year.Text = dt.Rows[0]["X_Year"].ToString();
       txtXII_Year.Text = dt.Rows[0]["XII_Year"].ToString();
       txtGrd_Year.Text = dt.Rows[0]["Grd_Year"].ToString();
       txtPost_Year.Text = dt.Rows[0]["Post_Year"].ToString();
       txtOther_Year.Text = dt.Rows[0]["Other_Year"].ToString();
       txtX_MM.Text = dt.Rows[0]["X_MM."].ToString();
       txtXII_MM.Text = dt.Rows[0]["XII_MM"].ToString();
       txtGrd_MM.Text = dt.Rows[0]["Grd_MM"].ToString();
        txtPost_MM.Text = dt.Rows[0]["Post_MM"].ToString();
       txtOther_MM.Text = dt.Rows[0]["Other_MM"].ToString();
       txtX_Obt.Text = dt.Rows[0]["X_Obt"].ToString();
       txtXII_Obt.Text = dt.Rows[0]["XII_Obt"].ToString();
       txtGrd_Obt.Text = dt.Rows[0]["Grd_Obt"].ToString();
       txtPost_Obt.Text = dt.Rows[0]["Post_Obt"].ToString();
       txtOther_Obt.Text = dt.Rows[0]["Other_Obt"].ToString();
        txtX_Per.Text = dt.Rows[0]["X_Per"].ToString();
        txtXII_Per.Text = dt.Rows[0]["XII_Per"].ToString();
          txtGrd_Per.Text = dt.Rows[0]["Grd_Per"].ToString();
          txtPost_Per.Text = dt.Rows[0]["Post_Per"].ToString();
          txtOther_Per.Text = dt.Rows[0]["Other_Per"].ToString();
          txtCompSub1.Text = dt.Rows[0]["CompSub1"].ToString();
          txtCompSub2.Text = dt.Rows[0]["CompSub2"].ToString();
          txtCompSub3.Text = dt.Rows[0]["CompSub3"].ToString();
          txtCompSub4.Text = dt.Rows[0]["CompSub4"].ToString();
           txtOpSub1.Text = dt.Rows[0]["OpSub1"].ToString();
            txtOpSub2.Text = dt.Rows[0]["OpSub2"].ToString();
            txtOpSu3.Text = dt.Rows[0]["OpSu3"].ToString();
            txtOpSub4.Text = dt.Rows[0]["OpSub4"].ToString();
            txtOpSub5.Text = dt.Rows[0]["OpSub5"].ToString();
            txtOpSub6.Text = dt.Rows[0]["OpSub6"].ToString();
            txtOpSub7.Text = dt.Rows[0]["OpSub7"].ToString();
            txtOpSub8.Text = dt.Rows[0]["OpSub8"].ToString();
            txtOpSub9.Text = dt.Rows[0]["OpSub9"].ToString();
            txtOpSub10.Text = dt.Rows[0]["OpSub10"].ToString();
            txtOpSub11.Text = dt.Rows[0]["OpSub11"].ToString();
            txtOpSub12.Text = dt.Rows[0]["OpSub12"].ToString();
            txtOpSub13.Text = dt.Rows[0]["OpSub13"].ToString();
            txtOpSub14.Text = dt.Rows[0]["OpSub14"].ToString();
              txtOpSu15.Text = dt.Rows[0]["OpSu15"].ToString();
              txtDC.Text = dt.Rows[0]["DC"].ToString();
              txtDCompSub1.Text = dt.Rows[0]["DCompSub1"].ToString();
               txtDCompSub2.Text = dt.Rows[0]["DCompSub2"].ToString();
               txtDCompSub3.Text = dt.Rows[0]["DCompSub3"].ToString();
               txtDCompSub4.Text = dt.Rows[0]["DCompSub4"].ToString();
               txtDOpsub1.Text = dt.Rows[0]["DOpsub1"].ToString();
               txtDOpsub2.Text = dt.Rows[0]["DOpsub2"].ToString();
               txtDOpsub3.Text = dt.Rows[0]["DOpsub3"].ToString();
               txtNoe1.Text = dt.Rows[0]["Noe1"].ToString();
               txtNoe2.Text = dt.Rows[0]["Noe2"].ToString();
               txtNoi1.Text = dt.Rows[0]["Noi1"].ToString();
                txtNoi2.Text = dt.Rows[0]["Noi2"].ToString();
               txtBoard1.Text = dt.Rows[0]["Board1"].ToString();
               txtBoard2.Text = dt.Rows[0]["Board2"].ToString();
               txtRollNo1.Text = dt.Rows[0]["RollNo1"].ToString();
                txtRollNo2.Text = dt.Rows[0]["RollNo2"].ToString();
                txtResult1.Text = dt.Rows[0]["Result1"].ToString();
                txtResult2.Text = dt.Rows[0]["Result2"].ToString();
                txtMq1.Text = dt.Rows[0]["Mq1"].ToString();
                txtTc.Text = dt.Rows[0]["Tc"].ToString();
                txtMig.Text = dt.Rows[0]["Mig"].ToString();
                txtsc.Text = dt.Rows[0]["sc"].ToString();
                txtSports.Text = dt.Rows[0]["Sports"].ToString();
                txtBonafide.Text = dt.Rows[0]["Bonafide"].ToString();
                txtSE.Text = dt.Rows[0]["SE"].ToString();
                txtCC.Text = dt.Rows[0]["CC"].ToString();
                txtPP.Text = dt.Rows[0]["PP"].ToString();
                txtPAR.Text = dt.Rows[0]["PAR"].ToString();
                txtNCC.Text = dt.Rows[0]["NCC"].ToString();
                txtBloodGroup.Text = dt.Rows[0]["BloodGroup"].ToString();
                txtYear2.Text = dt.Rows[0]["Year2"].ToString();
                txtYera1.Text = dt.Rows[0]["Yera1"].ToString();
            btnNew_Click(null, null);
            btnNew.Text = Resources.Attendance.Edit;
            try
            {
                imgLogo.ImageUrl = "~/CompanyResource/" + Session["CompId"].ToString() + "/" + dt.Rows[0]["Photo"].ToString();
                Session["empimgpath"] = dt.Rows[0]["Photo"].ToString();
            }
            catch
            {

            }
             try
            {
                imgLogo1.ImageUrl = "~/CompanyResource/" + Session["CompId"].ToString() + "/" + dt.Rows[0]["Sig1"].ToString();
                Session["sigpath"] = dt.Rows[0]["Sig1"].ToString();
            }
            catch
            {

            }
             try
            {
                imgLogo2.ImageUrl = "~/CompanyResource/" + Session["CompId"].ToString() + "/" + dt.Rows[0]["Sig2"].ToString();
                Session["sigpath1"] = dt.Rows[0]["Sig2"].ToString();
            }
            catch
            {

            }

        }

    }

    protected void IbtnDelete_Command(object sender, CommandEventArgs e)
    {
        int b = 0;
        b = objStudent.DeleteStudentAdmissionMaster(Session["CompId"].ToString(), e.CommandArgument.ToString(), false.ToString(), Session["UserId"].ToString(), DateTime.Now.ToString());
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
    protected void gvStudentMaster_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        gvStudentMaster.PageIndex = e.NewPageIndex;

        DataTable dt = (DataTable)Session["dtFilter"];
        gvStudentMaster.DataSource = dt;
        gvStudentMaster.DataBind();
        AllPageCode();

    }
    protected void gvStudentMaster_OnSorting(object sender, GridViewSortEventArgs e)
    {
        HDFSort.Value = HDFSort.Value == "ASC" ? "DESC" : "ASC";
        DataTable dt = new DataTable();
        dt = (DataTable)Session["dtFilter"];
        DataView dv = new DataView(dt);
        string Query = "" + e.SortExpression + " " + HDFSort.Value + "";
        dv.Sort = Query;
        dt = dv.ToTable();
        Session["dtFilter"] = dt;
        gvStudentMaster.DataSource = dt;
        gvStudentMaster.DataBind();
        AllPageCode();
    }

    [System.Web.Services.WebMethodAttribute(), System.Web.Script.Services.ScriptMethodAttribute()]
    public static string[] GetCompletionListStudentCode(string prefixText, int count, string contextKey)
    {
        StudentAdmissionMaster objStudentMaster = new StudentAdmissionMaster();
        DataTable dt = new DataView(objStudentMaster.GetStudentAdmissionMaster(HttpContext.Current.Session["CompId"].ToString()), "Student_Code like '" + prefixText.ToString() + "%'", "", DataViewRowState.CurrentRows).ToTable();
        string[] txt = new string[dt.Rows.Count];

        for (int i = 0; i < dt.Rows.Count; i++)
        {
            txt[i] = dt.Rows[i]["Student_Code"].ToString();
        }
        return txt;
    }
    [System.Web.Services.WebMethodAttribute(), System.Web.Script.Services.ScriptMethodAttribute()]
    public static string[] GetCompletionListStudentName(string prefixText, int count, string contextKey)
    {
        StudentAdmissionMaster objStudentMaster = new StudentAdmissionMaster();
        DataTable dt = new DataView(objStudentMaster.GetStudentAdmissionMaster(HttpContext.Current.Session["CompId"].ToString()), "Student_Name like '" + prefixText.ToString() + "%'", "", DataViewRowState.CurrentRows).ToTable();
        string[] txt = new string[dt.Rows.Count];

        for (int i = 0; i < dt.Rows.Count; i++)
        {
            txt[i] = dt.Rows[i]["Student_Name"].ToString();
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
        DataTable dt = objStudent.GetStudentAdmissionMaster(Session["CompId"].ToString());
        gvStudentMaster.DataSource = dt;
        gvStudentMaster.DataBind();
        AllPageCode();
        Session["dtFilter"] = dt;
        Session["Student"] = dt;

        lblTotalRecords.Text = Resources.Attendance.Total_Records + ": " + dt.Rows.Count.ToString() + "";

    }
    public void FillGridBin()
    {
        DataTable dt = new DataTable();
        dt = objStudent.GetStudentAdmissionMasterInactive(Session["CompId"].ToString());

        gvStudentMasterBin.DataSource = dt;
        gvStudentMasterBin.DataBind();


        Session["dtbinFilter"] = dt;
        Session["dtbinStudent"] = dt;
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
        CheckBox chkSelAll = ((CheckBox)gvStudentMasterBin.HeaderRow.FindControl("chkgvSelectAll"));
        for (int i = 0; i < gvStudentMasterBin.Rows.Count; i++)
        {
            ((CheckBox)gvStudentMasterBin.Rows[i].FindControl("chkgvSelect")).Checked = chkSelAll.Checked;
            if (chkSelAll.Checked)
            {
                if (!lblSelectedRecord.Text.Split(',').Contains(((Label)(gvStudentMasterBin.Rows[i].FindControl("lblStudentId"))).Text.Trim().ToString()))
                {
                    lblSelectedRecord.Text += ((Label)(gvStudentMasterBin.Rows[i].FindControl("lblStudentId"))).Text.Trim().ToString() + ",";
                }
            }
            else
            {
                string temp = string.Empty;
                string[] split = lblSelectedRecord.Text.Split(',');
                foreach (string item in split)
                {
                    if (item != ((Label)(gvStudentMasterBin.Rows[i].FindControl("lblStudentId"))).Text.Trim().ToString())
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
        Label lb = (Label)gvStudentMasterBin.Rows[index].FindControl("lblStudentId");
        if (((CheckBox)gvStudentMasterBin.Rows[index].FindControl("chkgvSelect")).Checked)
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
                if (!lblSelectedRecord.Text.Split(',').Contains(dr["Student_Id"]))
                {
                    lblSelectedRecord.Text += dr["Student_Id"] + ",";
                }
            }
            for (int i = 0; i < gvStudentMasterBin.Rows.Count; i++)
            {
                string[] split = lblSelectedRecord.Text.Split(',');
                Label lblconid = (Label)gvStudentMasterBin.Rows[i].FindControl("lblStudentId");
                for (int j = 0; j < lblSelectedRecord.Text.Split(',').Length; j++)
                {
                    if (lblSelectedRecord.Text.Split(',')[j] != "")
                    {
                        if (lblconid.Text.Trim().ToString() == lblSelectedRecord.Text.Split(',')[j].Trim().ToString())
                        {
                            ((CheckBox)gvStudentMasterBin.Rows[i].FindControl("chkgvSelect")).Checked = true;
                        }
                    }
                }
            }
        }
        else
        {
            lblSelectedRecord.Text = "";
            DataTable dtUnit1 = (DataTable)Session["dtBinFilter"];
            gvStudentMasterBin.DataSource = dtUnit1;
            gvStudentMasterBin.DataBind();
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
                    b = objStudent.DeleteStudentAdmissionMaster(Session["CompId"].ToString(), lblSelectedRecord.Text.Split(',')[j].Trim(), true.ToString(), Session["UserId"].ToString().ToString(), DateTime.Now.ToString());

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
            foreach (GridViewRow Gvr in gvStudentMasterBin.Rows)
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
         txtStudent_Name.Text  = "";
            txtStudent_Name_L.Text  = ""; 
            txtFormNo.Text  = "";
            txtSr_No.Text  = ""; 
            txtStudent_Code.Text  = ""; 
            txtReg_No.Text  = "";
            ddlCategory.SelectedValue  = ""; 
            txtEnroll_No.Text  = ""; 
            txtPermittedBy.Text = "";
             txtReligion.Text = ""; 
txtNationality.Text = ""; 
ddlCourse.SelectedValue = "";
ddlclass.SelectedValue  = ""; 
txtDoa.Text  = ""; 
txtDob.Text  = "";
txtPlace.Text  = "";
txtCaste.Text  = ""; 
ddlStatus.SelectedValue  = ""; 
ddlPH.SelectedValue  = "";
txtStreet.Text  = ""; 
txtVillage.Text  = "";
txtGrampanchayat.Text  = "";
txtTehsil.Text  = ""; 
txtDistrict.Text  = "";
txtState.Text  = ""; 
txtPost.Text  = ""; 
txtPinCode.Text  = ""; 
txtPhoneNo.Text  = ""; 
txtMobile1.Text  = ""; 
txtMobile2.Text  = "";
txtEmail.Text  = "";
txtBankName.Text  = "";
  txtBranch.Text  = ""; 
    txtAccountNo.Text  = ""; 
    txtFather_Name.Text  = ""; 
      txtProfession.Text  = "";
    txtPosition.Text  = ""; 
    txtToatalIncome.Text  = ""; 
    txtOfficePhoneNo.Text  = ""; 
    txtMobileNo3.Text  = ""; 
    txtOfficeAddr.Text  = "";
    txtMother_Name.Text  = ""; 
    txtProfession1.Text  = ""; 
     txtPosition1.Text  = ""; 
    txtToatalIncome1.Text  = "";
    txtOfficePhoneNo1.Text  = ""; 
    txtMobileNo4.Text  = ""; 
    txtOfficeAddr1.Text  = ""; 
    txtLocalGuardian.Text  = ""; 
      txtMobileNo5.Text  = ""; 
      txtAddress.Text  = "";
      txtVillage1.Text  = ""; 
      txtDocotor.Text  = ""; 
      txtMobileNo6.Text  = ""; 
      txtExam.Text  = ""; 
     txtYear.Text  = "";
     txtRollNo.Text  = ""; 
      txtEnrNo.Text  = ""; 
      txtX_Board.Text  = "";
      txtXII_Board.Text  = ""; 
      txtGraducation.Text  = ""; 
       txtPostGraducation.Text  = ""; 
       txtOtherBoard.Text  = ""; 
       txtX_Inst.Text  = ""; 
       txtXII_Inst.Text  = ""; 
       txtGrd_Inst.Text  = "";
       txtPost_Inst.Text  = "";
       txtOther_Inst.Text  = ""; 
       txtX_Year.Text  = ""; 
       txtXII_Year.Text  = "";
       txtGrd_Year.Text  = ""; 
       txtPost_Year.Text  = ""; 
       txtOther_Year.Text  = ""; 
       txtX_MM.Text  = "";
       txtXII_MM.Text  = ""; 
       txtGrd_MM.Text  = "";
        txtPost_MM.Text  = ""; 
       txtOther_MM.Text  = ""; 
       txtX_Obt.Text  = "";
       txtXII_Obt.Text  = ""; 
       txtGrd_Obt.Text  = ""; 
       txtPost_Obt.Text  = ""; 
       txtOther_Obt.Text  = "";
        txtX_Per.Text  = ""; 
        txtXII_Per.Text  = ""; 
          txtGrd_Per.Text  = "";
          txtPost_Per.Text  = "";
          txtOther_Per.Text  = "";
          txtCompSub1.Text  = ""; 
          txtCompSub2.Text  = ""; 
          txtCompSub3.Text  = ""; 
          txtCompSub4.Text  = ""; 
           txtOpSub1.Text  = "";
            txtOpSub2.Text  = "";
            txtOpSu3.Text  = ""; 
            txtOpSub4.Text  = "";
            txtOpSub5.Text  = "";
            txtOpSub6.Text  = "";
            txtOpSub7.Text  = "";
            txtOpSub8.Text  = "";
            txtOpSub9.Text  = "";
            txtOpSub10.Text  = ""; 
            txtOpSub11.Text  = ""; 
            txtOpSub12.Text  = ""; 
            txtOpSub13.Text  = ""; 
            txtOpSub14.Text  = ""; 
              txtOpSu15.Text  = "";
              txtDC.Text  = ""; 
              txtDCompSub1.Text  = ""; 
               txtDCompSub2.Text  = ""; 
               txtDCompSub3.Text  = "";
               txtDCompSub4.Text  = "";
               txtDOpsub1.Text  = ""; 
               txtDOpsub2.Text  = ""; 
               txtDOpsub3.Text  = ""; 
               txtNoe1.Text  = ""; 
               txtNoe2.Text  = ""; 
               txtNoi1.Text  = ""; 
                txtNoi2.Text  = "";
               txtBoard1.Text  = ""; 
               txtBoard2.Text  = ""; 
               txtRollNo1.Text  = "";
                txtRollNo2.Text  = "";
                txtResult1.Text  = "";
                txtResult2.Text  = "";
                txtMq1.Text  = ""; 
                txtTc.Text  = ""; 
                txtMig.Text  = "";
                txtsc.Text  = ""; 
                txtSports.Text  = ""; 
                txtBonafide.Text  = "";
                txtSE.Text  = ""; 
                txtCC.Text  = ""; 
                txtPP.Text  = ""; 
                txtPAR.Text  = ""; 
                txtNCC.Text  = "";
                txtBloodGroup.Text  = ""; 
                txtYear2.Text  = ""; 
                txtYera1.Text  = ""; 
        btnNew.Text = Resources.Attendance.New;
        lblSelectedRecord.Text = "";
        ViewState["Select"] = null;
        editid.Value = "";
        txtValue.Text = "";
        ddlOption.SelectedIndex = 2;
        ddlFieldName.SelectedIndex = 0;

    }

    protected void Photos_UploadedComplete(object sender, AjaxControlToolkit.AsyncFileUploadEventArgs e)
    {


        if (Photos.HasFile)
        {
            if (!Directory.Exists(Server.MapPath("~/CompanyResource/") + Session["CompId"]))
            {
                Directory.CreateDirectory(Server.MapPath("~/CompanyResource/") + Session["CompId"]);
            }


            string path = Server.MapPath("~/CompanyResource/" + "/" + Session["CompId"] + "/") + Photos.FileName;
            Photos.SaveAs(path);
            Session["empimgpath"] = Photos.FileName;
        }


    }

    protected void btnUpload1_Click(object sender, EventArgs e)
    {
        imgLogo.ImageUrl = "~/CompanyResource/" + "/" + Session["CompId"] + "/" + Photos.FileName;

    }

    protected void Sigs1_UploadedComplete(object sender, AjaxControlToolkit.AsyncFileUploadEventArgs e)
    {


        if (Sigs1.HasFile)
        {
            if (!Directory.Exists(Server.MapPath("~/CompanyResource/") + Session["CompId"]))
            {
                Directory.CreateDirectory(Server.MapPath("~/CompanyResource/") + Session["CompId"]);
            }
            string path = Server.MapPath("~/CompanyResource/" + "/" + Session["CompId"] + "/") + Sigs1.FileName;
            Sigs1.SaveAs(path);
            Session["sigpath"] = Sigs1.FileName;
        }
    }

    protected void btnUpload2_Click(object sender, EventArgs e)
    {
        imgLogo1.ImageUrl = "~/CompanyResource/" + "/" + Session["CompId"] + "/" + Sigs1.FileName;

    }

    protected void Sigs2_UploadedComplete(object sender, AjaxControlToolkit.AsyncFileUploadEventArgs e)
    {
        if (Sigs2.HasFile)
        {
            if (!Directory.Exists(Server.MapPath("~/CompanyResource/") + Session["CompId"]))
            {
                Directory.CreateDirectory(Server.MapPath("~/CompanyResource/") + Session["CompId"]);
            }
            string path = Server.MapPath("~/CompanyResource/" + "/" + Session["CompId"] + "/") + Sigs2.FileName;
            Sigs2.SaveAs(path);
            Session["sigpath1"] = Sigs2.FileName;
        }
    }

    protected void btnUpload3_Click(object sender, EventArgs e)
    {
        imgLogo2.ImageUrl = "~/CompanyResource/" + "/" + Session["CompId"] + "/" + Sigs2.FileName;

    }
}

   