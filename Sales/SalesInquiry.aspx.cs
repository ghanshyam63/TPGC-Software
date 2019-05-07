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
using System.Text;
using System.Text.RegularExpressions;
using System.Collections.Generic;

public partial class Sales_SalesInquiry : System.Web.UI.Page
{
    #region defind Class Object
    Common cmn = new Common();
    //Set_Suppliers objSupplier = new Set_Suppliers();
    Inv_SalesInquiryHeader objSInquiryHeader = new Inv_SalesInquiryHeader();
    Inv_SalesInquiryDetail objSInquiryDetail = new Inv_SalesInquiryDetail();
    Ems_ContactMaster objContact = new Ems_ContactMaster();
    EmployeeMaster objEmployee = new EmployeeMaster();
    Inv_ProductMaster objProductM = new Inv_ProductMaster();
    Inv_UnitMaster UM = new Inv_UnitMaster();
    CurrencyMaster objCurrency = new CurrencyMaster();
    SystemParameter ObjSysParam = new SystemParameter();
    UserMaster ObjUser = new UserMaster();
    Set_DocNumber objDocNo = new Set_DocNumber();

    string StrCompId = string.Empty;
    string StrBrandId = string.Empty;
    string strLocationId = string.Empty;
    string StrUserId = "admin";
    #endregion

    protected void Page_Load(object sender, EventArgs e)
    {
        //Remove
        StrUserId = Session["UserId"].ToString();
        StrCompId = Session["CompId"].ToString();
        StrBrandId = Session["BrandId"].ToString();
        strLocationId = Session["LocId"].ToString();
        //End Remove
        if (Session["UserId"] == null)
        {
            Response.Redirect("~/Login.aspx");
        }
        AllPageCode();
        if (!IsPostBack)
        {
            ddlOption.SelectedIndex = 2;
            btnList_Click(null, null);
            FillGridBin();
            FillGrid();
            FillCurrency();
            txtInquiryNo.Text = GetDocumentNumber(); //updated by jitendra on 28-9-2013
            //txtInquiryNo.Text = objSInquiryHeader.GetAutoID(StrCompId, StrBrandId, strLocationId);
            txtInquiryDate.Text = DateTime.Now.ToString(ObjSysParam.GetSysParameterByParamName("Date_Format").Rows[0]["Param_Value"].ToString());
            txtTenderDate.Text = DateTime.Now.ToString(ObjSysParam.GetSysParameterByParamName("Date_Format").Rows[0]["Param_Value"].ToString());

            txtOrderCompletionDate.Text = DateTime.Now.ToString(ObjSysParam.GetSysParameterByParamName("Date_Format").Rows[0]["Param_Value"].ToString());
        }
    }
    public void AllPageCode()
    {
        Page.Title = ObjSysParam.GetSysTitle();
        StrUserId = Session["UserId"].ToString();
        StrCompId = Session["CompId"].ToString();
        StrBrandId = Session["BrandId"].ToString();
        strLocationId = Session["LocId"].ToString();
        Session["AccordianId"] = "13";
        Session["HeaderText"] = "Sales";
        DataTable dtAllPageCode = cmn.GetAllPagePermission(StrUserId.ToString(), "13", "54");
        if (dtAllPageCode.Rows.Count != 0)
        {
            if (dtAllPageCode.Rows[0][0].ToString() == "SuperAdmin")
            {
                btnSInquirySave.Visible = true;
                foreach (GridViewRow Row in GvSalesInquiry.Rows)
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
                        btnSInquirySave.Visible = true;
                    }
                    foreach (GridViewRow Row in GvSalesInquiry.Rows)
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
    public string GetDocumentNumber()
    {
        string DocumentNo = string.Empty;

        DataTable Dt = objDocNo.GetDocumentNumberAll(StrCompId, "13", "54");

        if (Dt.Rows.Count > 0)
        {
            if (Dt.Rows[0]["Prefix"].ToString() != "")
            {
                DocumentNo += Dt.Rows[0]["Prefix"].ToString();
            }



            if (Convert.ToBoolean(Dt.Rows[0]["CompId"].ToString()))
            {
                DocumentNo += StrCompId;
            }

            if (Convert.ToBoolean(Dt.Rows[0]["BrandId"].ToString()))
            {
                DocumentNo += StrBrandId;
            }

            if (Convert.ToBoolean(Dt.Rows[0]["LocationId"].ToString()))
            {
                DocumentNo += strLocationId;
            }

            if (Convert.ToBoolean(Dt.Rows[0]["DeptId"].ToString()))
            {
                DocumentNo += (string)Session["SessionDepId"];
            }

            if (Convert.ToBoolean(Dt.Rows[0]["EmpId"].ToString()))
            {

                DataTable Dtuser = ObjUser.GetUserMasterByUserId(Session["UserId"].ToString());
                DocumentNo += Dtuser.Rows[0]["Emp_Id"].ToString();

            }

            if (Convert.ToBoolean(Dt.Rows[0]["Year"].ToString()))
            {
                DocumentNo += DateTime.Now.Year.ToString();
            }

            if (Convert.ToBoolean(Dt.Rows[0]["Month"].ToString()))
            {
                DocumentNo += DateTime.Now.Month.ToString();
            }

            if (Convert.ToBoolean(Dt.Rows[0]["Day"].ToString()))
            {
                DocumentNo += DateTime.Now.Day.ToString();
            }

            if (Dt.Rows[0]["Suffix"].ToString() != "")
            {
                DocumentNo += Dt.Rows[0]["Suffix"].ToString();
            }
            if (DocumentNo != "")
            {
                DocumentNo += "-" + (Convert.ToInt32(objSInquiryHeader.GetSIHeaderAll(StrCompId.ToString(), StrBrandId, strLocationId).Rows.Count) + 1).ToString();
            }
            else
            {
                DocumentNo += (Convert.ToInt32(objSInquiryHeader.GetSIHeaderAll(StrCompId.ToString(), StrBrandId, strLocationId).Rows.Count) + 1).ToString();

            }
        }
        else
        {
            DocumentNo += (Convert.ToInt32(objSInquiryHeader.GetSIHeaderAll(StrCompId.ToString(), StrBrandId, strLocationId).Rows.Count) + 1).ToString();
        }

        return DocumentNo;

    } 


    #region System defind Funcation
    protected void btnList_Click(object sender, EventArgs e)
    {
        pnlMenuList.BackColor = System.Drawing.ColorTranslator.FromHtml("#ccddee");
        pnlMenuNew.BackColor = System.Drawing.ColorTranslator.FromHtml("#90bde9");
        pnlMenuBin.BackColor = System.Drawing.ColorTranslator.FromHtml("#90bde9");

        PnlList.Visible = true;
        PnlNewEdit.Visible = false;
        PnlBin.Visible = false;
    }
    protected void btnNew_Click(object sender, EventArgs e)
    {
        pnlMenuNew.BackColor = System.Drawing.ColorTranslator.FromHtml("#ccddee");

        pnlMenuList.BackColor = System.Drawing.ColorTranslator.FromHtml("#90bde9");
        pnlMenuBin.BackColor = System.Drawing.ColorTranslator.FromHtml("#90bde9");

        PnlList.Visible = false;
        PnlNewEdit.Visible = true;
        PnlBin.Visible = false;
    }

    protected void btnEdit_Command(object sender, CommandEventArgs e)
    {
        hdnSInquiryId.Value = e.CommandArgument.ToString();

        DataTable dtSInquiryEdit = objSInquiryHeader.GetSIHeaderAllBySInquiryId(StrCompId, StrBrandId, strLocationId, hdnSInquiryId.Value);

        btnNew.Text = Resources.Attendance.Edit;
        if (dtSInquiryEdit.Rows.Count > 0)
        {
            txtInquiryNo.Text = dtSInquiryEdit.Rows[0]["SInquiryNo"].ToString();
            txtInquiryNo.ReadOnly = true;
            txtInquiryDate.Text = Convert.ToDateTime(dtSInquiryEdit.Rows[0]["IDate"].ToString()).ToString(ObjSysParam.GetSysParameterByParamName("Date_Format").Rows[0]["Param_Value"].ToString());
            ddlInquiryType.SelectedValue = dtSInquiryEdit.Rows[0]["InquiryType"].ToString();
            txtOrderCompletionDate.Text = Convert.ToDateTime(dtSInquiryEdit.Rows[0]["OrderCompletionDate"].ToString()).ToString(ObjSysParam.GetSysParameterByParamName("Date_Format").Rows[0]["Param_Value"].ToString());
            txtTenderDate.Text = Convert.ToDateTime(dtSInquiryEdit.Rows[0]["TenderDate"].ToString()).ToString(ObjSysParam.GetSysParameterByParamName("Date_Format").Rows[0]["Param_Value"].ToString());
            txtTenderNo.Text = dtSInquiryEdit.Rows[0]["TenderNo"].ToString();

            string strCustomerId = dtSInquiryEdit.Rows[0]["Customer_Id"].ToString();
            txtCustomerName.Text = GetCustomerName(strCustomerId) + "/" + strCustomerId;
            ddlCurrency.SelectedValue = dtSInquiryEdit.Rows[0]["Currency_Id"].ToString();
            txtRemark.Text = dtSInquiryEdit.Rows[0]["Remark"].ToString();
            string strREmployeeId = dtSInquiryEdit.Rows[0]["ReceivedEmpID"].ToString();
            txtReceivedEmp.Text = GetEmployeeName(strREmployeeId) + "/" + strREmployeeId;
            string strHEmployeeId = dtSInquiryEdit.Rows[0]["HandledEmpID"].ToString();
            txtHandledEmp.Text = GetEmployeeName(strHEmployeeId) + "/" + strHEmployeeId;

            string strBuyingPriority = dtSInquiryEdit.Rows[0]["BuyingPriority"].ToString();
            if (strBuyingPriority == "True")
            {
                chkBuyingPriority.Checked = true;
            }
            else if (strBuyingPriority == "False")
            {
                chkBuyingPriority.Checked = false;
            }
            string strSendEmail = dtSInquiryEdit.Rows[0]["EmailSendFlag"].ToString();
            if (strSendEmail == "True")
            {
                chkSendMail.Checked = true;
            }
            else if (strSendEmail == "False")
            {
                chkSendMail.Checked = false;
            }
            string strPost = dtSInquiryEdit.Rows[0]["Post"].ToString();
            if (strPost == "True")
            {
                chkPost.Checked = true;
            }
            else if (strPost == "False")
            {
                chkPost.Checked = false;
            }

            txtCondition1.Text = dtSInquiryEdit.Rows[0]["Condition1"].ToString();
            txtCondition2.Text = dtSInquiryEdit.Rows[0]["Condition2"].ToString();
            txtCondition3.Text = dtSInquiryEdit.Rows[0]["Condition3"].ToString();
            txtCondition4.Text = dtSInquiryEdit.Rows[0]["Condition4"].ToString();
            txtCondition5.Text = dtSInquiryEdit.Rows[0]["Condition5"].ToString();
            try
            {
                if (dtSInquiryEdit.Rows[0]["Field1"].ToString() == "FWInPur")
                {

                    ChkSendInPurchase.Checked = true;
                }
                else
                {
                    if (dtSInquiryEdit.Rows[0]["Field1"].ToString() == "Send Inquiry To Supplier")
                    {
                        ChkSendInPurchase.Checked = true;
                        PnlNewContant.Enabled = false;
                    }
                    else
                    {
                        ChkSendInPurchase.Checked = false;
                        PnlNewContant.Enabled = true;
                    }
                }


            }

            catch
            {

            }
            //Add Child Concept
            DataTable dtDetail = objSInquiryDetail.GetSIDetailBySInquiryId(StrCompId, StrBrandId, strLocationId, hdnSInquiryId.Value);
            if (dtDetail.Rows.Count > 0)
            {
                GvProduct.DataSource = dtDetail;
                GvProduct.DataBind();
            }
        }
        btnNew_Click(null, null);
        System.Web.UI.ScriptManager.GetCurrent(this).SetFocus(txtInquiryDate);
    }
    protected void GvSalesInquiry_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        GvSalesInquiry.PageIndex = e.NewPageIndex;
        DataTable dt = (DataTable)Session["dtFilter"];
        GvSalesInquiry.DataSource = dt;
        GvSalesInquiry.DataBind();
    }
    protected void btnbindrpt_Click(object sender, ImageClickEventArgs e)
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
            DataTable dtAdd = (DataTable)Session["dtCurrency"];
            DataView view = new DataView(dtAdd, condition, "", DataViewRowState.CurrentRows);
            GvSalesInquiry.DataSource = view.ToTable();
            Session["dtFilter"] = view.ToTable();
            lblTotalRecords.Text = Resources.Attendance.Total_Records + " : " + view.ToTable().Rows.Count + "";
            GvSalesInquiry.DataBind();
        }
    }
    protected void GvSalesInquiry_Sorting(object sender, GridViewSortEventArgs e)
    {
        DataTable dt = (DataTable)Session["dtFilter"];
        string sortdir = "DESC";
        if (ViewState["SortDir"] != null)
        {
            sortdir = ViewState["SortDir"].ToString();
            if (sortdir == "ASC")
            {
                e.SortDirection = SortDirection.Descending;
                ViewState["SortDir"] = "DESC";
            }
            else
            {
                e.SortDirection = SortDirection.Ascending;
                ViewState["SortDir"] = "ASC";
            }
        }
        else
        {
            ViewState["SortDir"] = "DESC";
        }

        dt = (new DataView(dt, "", e.SortExpression + " " + ViewState["SortDir"].ToString(), DataViewRowState.CurrentRows)).ToTable();
        Session["dtFilter"] = dt;
        GvSalesInquiry.DataSource = dt;
        GvSalesInquiry.DataBind();
    }
    protected void IbtnDelete_Command(object sender, CommandEventArgs e)
    {
        hdnSInquiryId.Value = e.CommandArgument.ToString();
        int b = 0;
        b = objSInquiryHeader.DeleteSIHeader(StrCompId, StrBrandId, strLocationId, hdnSInquiryId.Value, "false", StrUserId, DateTime.Now.ToString());
        if (b != 0)
        {
            DisplayMessage("Record Delete");
        }
        else
        {
            DisplayMessage("Record  Not Deleted");
        }
        FillGridBin(); //Update grid view in bin tab
        FillGrid();
        Reset();
    }
    protected void btnRefreshReport_Click(object sender, ImageClickEventArgs e)
    {
        FillGrid();
        FillGridBin();
        ddlFieldName.SelectedIndex = 1;
        ddlOption.SelectedIndex = 3;
        txtValue.Text = "";
    }
    protected void btnSInquiryCancel_Click(object sender, EventArgs e)
    {
        Reset();
        btnList_Click(null, null);
        FillGridBin();
        FillGrid();
    }
    protected void BtnReset_Click(object sender, EventArgs e)
    {
        Reset();
        System.Web.UI.ScriptManager.GetCurrent(this).SetFocus(txtInquiryDate);
    }
    protected void btnSInquirySave_Click(object sender, EventArgs e)
    {
        string strCustomerId = string.Empty;
        string strReceivedEmployeeId = string.Empty;
        string strHandledEmployeeId = string.Empty;
        string strBuyingPriority = string.Empty;
        string strSendMail = string.Empty;
        string strPost = string.Empty;
        if (txtInquiryDate.Text == "")
        {
            DisplayMessage("Enter Sales Inquiry date");
            txtInquiryDate.Focus();
            txtInquiryDate.Text = DateTime.Now.ToString(ObjSysParam.GetSysParameterByParamName("Date_Format").Rows[0]["Param_Value"].ToString());
           
            return;
        }
        if (txtTenderDate.Text == "")
        {
            DisplayMessage("Enter Sales Tender date");
            txtTenderDate.Focus();
            txtTenderDate.Text = DateTime.Now.ToString(ObjSysParam.GetSysParameterByParamName("Date_Format").Rows[0]["Param_Value"].ToString());
            return;

        }

        if (txtInquiryNo.Text == "")
        {
            DisplayMessage("Enter Inquiry No.");
            System.Web.UI.ScriptManager.GetCurrent(this).SetFocus(txtInquiryNo);
            return;
        }
        else
        {
            if (hdnSInquiryId.Value == "0")
            {
                DataTable dtInquiryNo = objSInquiryHeader.GetSIHeaderAllBySInquiryNo(StrCompId, StrBrandId, strLocationId, txtInquiryNo.Text);
                if (dtInquiryNo.Rows.Count > 0)
                {
                    DisplayMessage("Sales Inquiry No. Already Exits");
                    txtInquiryNo.Text = "";
                    System.Web.UI.ScriptManager.GetCurrent(this).SetFocus(txtInquiryNo);
                    return;
                }
            }
        }
        if (txtCustomerName.Text == "")
        {
            DisplayMessage("Fill Customer Name");
            txtCustomerName.Focus();
            return;
        }
        else
        {
            strCustomerId = GetCustomerId();
            if (strCustomerId == "" || strCustomerId == "0")
            {
                DisplayMessage("Select Customer Name In Suggestions Only");
                txtCustomerName.Text = "";
                System.Web.UI.ScriptManager.GetCurrent(this).SetFocus(txtCustomerName);
                return;
            }
        }

        if (ddlCurrency.SelectedValue == "--Select--")
        {
            DisplayMessage("Select Currency Name");
            ddlCurrency.Focus();
            return;
        }
        if (ddlInquiryType.SelectedValue == "--Select--")
        {
            DisplayMessage("Select Inquiry Type");
            ddlInquiryType.Focus();
            return;
        }
        if (txtOrderCompletionDate.Text == "")
        {
            DisplayMessage("Enter Order Close Date");
            txtOrderCompletionDate.Focus();
            return;
        }
        if (GvProduct.Rows.Count == 0)
        {
            DisplayMessage("Emter Product");
            btnProductSave.Focus();
            return;
        }
        //here we et the validation of product add is necessary

        if (txtReceivedEmp.Text == "")
        {
            DisplayMessage("Enter Received Employee Name");
            txtReceivedEmp.Focus();
            return;
        }
        else
        {
            strReceivedEmployeeId = GetEmployeeId(txtReceivedEmp.Text);
            if (strReceivedEmployeeId == "" || strReceivedEmployeeId == "0")
            {
                DisplayMessage("Select Employee In Suggestions Only");
                txtReceivedEmp.Text = "";
                System.Web.UI.ScriptManager.GetCurrent(this).SetFocus(txtReceivedEmp);
                return;
            }
        }

        if (txtHandledEmp.Text == "")
        {
            DisplayMessage("Enter Handled Employee Name");
            txtHandledEmp.Focus();
            return;
        }
        else
        {
            strHandledEmployeeId = GetEmployeeId(txtHandledEmp.Text);
            if (strHandledEmployeeId == "" || strHandledEmployeeId == "0")
            {
                DisplayMessage("Select In Suggestions Only");
                txtHandledEmp.Text = "";
                System.Web.UI.ScriptManager.GetCurrent(this).SetFocus(txtHandledEmp);
                return;
            }
        }

        if (chkBuyingPriority.Checked == true)
        {
            strBuyingPriority = "True";
        }
        else
        {
            strBuyingPriority = "False";
        }
        if (chkSendMail.Checked == true)
        {
            strSendMail = "True";
        }
        else
        {
            strSendMail = "False";
        }
        if (chkPost.Checked == true)
        {
            strPost = "True";
        }
        else
        {
            strPost = "False";
        }

        
        string InquiryStatus = string.Empty;
        if (PnlNewContant.Enabled != false)
        {
            if (ChkSendInPurchase.Checked)
            {
                InquiryStatus = "FWInPur";
            }
            else
            {
                InquiryStatus = "Direct";
            }
        }
        else
        {
            InquiryStatus = "Send Inquiry To Supplier";
        }
        int b = 0;
        if (hdnSInquiryId.Value != "0")
        {
            b = objSInquiryHeader.UpdateSIHeader(StrCompId, StrBrandId, strLocationId, hdnSInquiryId.Value, txtInquiryNo.Text, txtInquiryDate.Text, txtTenderNo.Text.Trim(), txtTenderDate.Text.Trim(), ddlInquiryType.SelectedValue, txtOrderCompletionDate.Text, strCustomerId, ddlCurrency.SelectedValue, txtRemark.Text, strReceivedEmployeeId, strHandledEmployeeId, strBuyingPriority, strSendMail, txtCondition1.Text, txtCondition2.Text, txtCondition3.Text, txtCondition4.Text, txtCondition5.Text, strPost, InquiryStatus.ToString(), "", "", "", "", "True", DateTime.Now.ToString(), "True", Session["UserId"].ToString(), DateTime.Now.ToString());

            //Add Detail Section.
            objSInquiryDetail.DeleteSIDetail(StrCompId, StrBrandId, strLocationId, hdnSInquiryId.Value);

            foreach (GridViewRow gvr in GvProduct.Rows)
            {
                Label lblSerialNo = (Label)gvr.FindControl("lblSNo");
                Label lblProductId = (Label)gvr.FindControl("lblgvProductId");
                Label lblUnitId = (Label)gvr.FindControl("lblgvUnitId");
                Label lblProductDescription = (Label)gvr.FindControl("lblgvProductDescription");
                Label lblCurrencyId = (Label)gvr.FindControl("lblgvCurrencyId");
                Label lblEstimatedUnitPrice = (Label)gvr.FindControl("lblgvEstimatedUnitPrice");
                Label lblRequiredQty = (Label)gvr.FindControl("lblgvRequiredQty");
                objSInquiryDetail.InsertSIDetail(StrCompId, StrBrandId, strLocationId, hdnSInquiryId.Value, lblSerialNo.Text, lblProductId.Text, lblUnitId.Text, lblProductDescription.Text, lblCurrencyId.Text, lblEstimatedUnitPrice.Text, lblRequiredQty.Text, "0", "", "", "", "", "", "True", DateTime.Now.ToString(), "True", Session["UserId"].ToString(), DateTime.Now.ToString(), Session["UserId"].ToString(), DateTime.Now.ToString());
            }
            //End  

            if (b != 0)
            {
                DisplayMessage("Record Updated");
               
                FillGrid();
                btnList_Click(null, null);
                Reset();
            }
            else
            {
                DisplayMessage("Record  Not Updated");
            }
        }
        else
        {
            b = objSInquiryHeader.InsertSIHeader(StrCompId, StrBrandId, strLocationId, txtInquiryNo.Text, txtInquiryDate.Text, txtTenderNo.Text.Trim(), txtTenderDate.Text.Trim(), ddlInquiryType.SelectedValue, txtOrderCompletionDate.Text, strCustomerId, ddlCurrency.SelectedValue, txtRemark.Text, strReceivedEmployeeId, strHandledEmployeeId, strBuyingPriority, strSendMail, txtCondition1.Text, txtCondition2.Text, txtCondition3.Text, txtCondition4.Text, txtCondition5.Text, strPost, InquiryStatus.ToString(), "", "", "", "", "True", DateTime.Now.ToString(), "True", Session["UserId"].ToString(), DateTime.Now.ToString(), Session["UserId"].ToString(), DateTime.Now.ToString());

            string strMaxId = string.Empty;
            DataTable dtMaxId = objSInquiryHeader.GetMaxSalesInquiryId(StrCompId, StrBrandId, strLocationId);
            if (dtMaxId.Rows.Count > 0)
            {
                strMaxId = dtMaxId.Rows[0][0].ToString();
                //Add Detail Section.
                objSInquiryDetail.DeleteSIDetail(StrCompId, StrBrandId, strLocationId, strMaxId);
                foreach (GridViewRow gvr in GvProduct.Rows)
                {
                    Label lblSerialNo = (Label)gvr.FindControl("lblSNo");
                    Label lblProductId = (Label)gvr.FindControl("lblgvProductId");
                    Label lblUnitId = (Label)gvr.FindControl("lblgvUnitId");
                    Label lblProductDescription = (Label)gvr.FindControl("lblgvProductDescription");
                    Label lblCurrencyId = (Label)gvr.FindControl("lblgvCurrencyId");
                    Label lblEstimatedUnitPrice = (Label)gvr.FindControl("lblgvEstimatedUnitPrice");
                    Label lblRequiredQty = (Label)gvr.FindControl("lblgvRequiredQty");

                    objSInquiryDetail.InsertSIDetail(StrCompId, StrBrandId, strLocationId, strMaxId, lblSerialNo.Text, lblProductId.Text, lblUnitId.Text, lblProductDescription.Text, lblCurrencyId.Text, lblEstimatedUnitPrice.Text, lblRequiredQty.Text, "0", "", "", "", "", "", "True", DateTime.Now.ToString(), "True", Session["UserId"].ToString(), DateTime.Now.ToString(), Session["UserId"].ToString(), DateTime.Now.ToString());
                }
                //End  
            }

            if (b != 0)
            {
                DisplayMessage("Record Saved");
                Reset();
            }
            else
            {
                DisplayMessage("Record  Not Saved");
            }
        }
    }
    private string GetCustomerId()
    {
        string retval = string.Empty;
        if (txtCustomerName.Text != "")
        {
            DataTable dtSupp = objContact.GetContactByContactName(StrCompId.ToString(), txtCustomerName.Text.Trim().Split('/')[0].ToString());
            if (dtSupp.Rows.Count > 0)
            {
                retval = (txtCustomerName.Text.Split('/'))[txtCustomerName.Text.Split('/').Length - 1];

                DataTable dtCompany = objContact.GetContactByContactId(StrCompId, retval);
                if (dtCompany.Rows.Count > 0)
                {

                }
                else
                {
                    retval = "";
                }
            }
            else
            {
                retval = "";
            }
        }
        else
        {
            retval = "";
        }
        return retval;
    }
    private string GetEmployeeId(string strEmployeeName)
    {
        string retval = string.Empty;
        if (strEmployeeName != "")
        {
            DataTable dtEmployee = objEmployee.GetEmployeeMasterByEmpName(StrCompId, strEmployeeName.Split('/')[0].ToString());
            if (dtEmployee.Rows.Count > 0)
            {
                retval = (strEmployeeName.Split('/'))[strEmployeeName.Split('/').Length - 1];

                DataTable dtEmp = objEmployee.GetEmployeeMasterById(StrCompId, retval);
                if (dtEmp.Rows.Count > 0)
                {

                }
                else
                {
                    retval = "";
                }
            }
            else
            {
                retval = "";
            }
        }
        else
        {
            retval = "";
        }
        return retval;
    }
    protected void IbtnRestore_Command(object sender, CommandEventArgs e)
    {
        hdnSInquiryId.Value = e.CommandArgument.ToString();
        int b = 0;
        b = objSInquiryHeader.DeleteSIHeader(StrCompId, StrBrandId, strLocationId, hdnSInquiryId.Value, true.ToString(), StrUserId, DateTime.Now.ToString());
        if (b != 0)
        {
            DisplayMessage("Record Activated");
        }
        else
        {
            DisplayMessage("Record Not Activated");
        }
        FillGrid();
        FillGridBin();
        Reset();
    }

    #region Auto Complete
    [System.Web.Services.WebMethodAttribute(), System.Web.Script.Services.ScriptMethodAttribute()]
    public static string[] GetCompletionListCustomer(string prefixText, int count, string contextKey)
    {
        Ems_ContactMaster objCustomer = new Ems_ContactMaster();
        DataTable dtCustomer = objCustomer.GetDistinctCustomerName("1", prefixText);
        string[] txt = new string[dtCustomer.Rows.Count];

        if (dtCustomer.Rows.Count > 0)
        {
            for (int i = 0; i < dtCustomer.Rows.Count; i++)
            {
                txt[i] = dtCustomer.Rows[i]["Contact_Name"].ToString() + "/" + dtCustomer.Rows[i]["Contact_Id"].ToString() + "";
            }
        }
        else
        {
            //if (prefixText.Length > 2)
            //{
            //    txt = null;
            //}
            //else
            //{
            dtCustomer = objCustomer.GetAllCustomerName("1");
            if (dtCustomer.Rows.Count > 0)
            {
                txt = new string[dtCustomer.Rows.Count];
                for (int i = 0; i < dtCustomer.Rows.Count; i++)
                {
                    txt[i] = dtCustomer.Rows[i]["Contact_Name"].ToString() + "/" + dtCustomer.Rows[i]["Contact_Id"].ToString() + "";
                }
            }
            //}
        }
        return txt;
    }

    [System.Web.Services.WebMethodAttribute(), System.Web.Script.Services.ScriptMethodAttribute()]
    public static string[] GetCompletionListEmployeeName(string prefixText, int count, string contextKey)
    {
        EmployeeMaster ObjEmployeeMaster = new EmployeeMaster();
        DataTable dt1 = ObjEmployeeMaster.GetEmployeeMaster(HttpContext.Current.Session["CompId"].ToString());
        dt1 = new DataView(dt1, "Brand_Id='1' and Location_Id='1'", "", DataViewRowState.CurrentRows).ToTable();

        DataTable dt = new DataView(dt1, "Emp_Name like '" + prefixText.ToString() + "%'", "", DataViewRowState.CurrentRows).ToTable();
        string[] txt = new string[dt.Rows.Count];
        if (dt.Rows.Count > 0)
        {
            for (int i = 0; i < dt.Rows.Count; i++)
            {
                //txt[i] = dt.Rows[i]["Emp_Name"].ToString();
                txt[i] = dt.Rows[i]["Emp_Name"].ToString() + "/" + dt.Rows[i]["Emp_Id"].ToString() + "";
            }
        }
        else
        {
            if (dt1.Rows.Count > 0)
            {
                txt = new string[dt1.Rows.Count];
                for (int i = 0; i < dt1.Rows.Count; i++)
                {
                    //txt[i] = dt1.Rows[i]["Emp_Name"].ToString();
                    txt[i] = dt1.Rows[i]["Emp_Name"].ToString() + "/" + dt1.Rows[i]["Emp_Id"].ToString() + "";
                }
            }
        }
        return txt;
    }
    #endregion

    #region Bin Section
    protected void btnBin_Click(object sender, EventArgs e)
    {
        pnlMenuBin.BackColor = System.Drawing.ColorTranslator.FromHtml("#ccddee");

        pnlMenuNew.BackColor = System.Drawing.ColorTranslator.FromHtml("#90bde9");
        pnlMenuList.BackColor = System.Drawing.ColorTranslator.FromHtml("#90bde9");
        PnlNewEdit.Visible = false;
        PnlBin.Visible = true;
        PnlList.Visible = false;
        FillGridBin();
    }
    protected void GvSalesInquiryBin_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        GvSalesInquiryBin.PageIndex = e.NewPageIndex;

        DataTable dt = (DataTable)Session["dtInactive"];
        GvSalesInquiryBin.DataSource = dt;
        GvSalesInquiryBin.DataBind();

        string temp = string.Empty;

        for (int i = 0; i < GvSalesInquiryBin.Rows.Count; i++)
        {
            Label lblconid = (Label)GvSalesInquiryBin.Rows[i].FindControl("lblgvInquiryId");
            string[] split = lblSelectedRecord.Text.Split(',');

            for (int j = 0; j < lblSelectedRecord.Text.Split(',').Length; j++)
            {
                if (lblSelectedRecord.Text.Split(',')[j] != "")
                {
                    if (lblconid.Text.Trim().ToString() == lblSelectedRecord.Text.Split(',')[j].Trim().ToString())
                    {
                        ((CheckBox)GvSalesInquiryBin.Rows[i].FindControl("chkSelect")).Checked = true;
                    }
                }
            }
        }
    }
    protected void GvSalesInquiryBin_OnSorting(object sender, GridViewSortEventArgs e)
    {
        HDFSortbin.Value = HDFSortbin.Value == "ASC" ? "DESC" : "ASC";
        DataTable dt = new DataTable();
        dt = objSInquiryHeader.GetSIHeaderAllFalse(StrCompId, StrBrandId, strLocationId);
        DataView dv = new DataView(dt);
        string Query = "" + e.SortExpression + " " + HDFSortbin.Value + "";
        dv.Sort = Query;
        dt = dv.ToTable();
        Session["dtInactive"] = dt;
        GvSalesInquiryBin.DataSource = dt;
        GvSalesInquiryBin.DataBind();
        lblSelectedRecord.Text = "";
    }
    public void FillGridBin()
    {
        DataTable dt = new DataTable();
        dt = objSInquiryHeader.GetSIHeaderAllFalse(StrCompId, StrBrandId, strLocationId);
        GvSalesInquiryBin.DataSource = dt;
        GvSalesInquiryBin.DataBind();
        Session["dtPBrandBin"] = dt;
        Session["dtInactive"] = dt;
        lblTotalRecordsBin.Text = Resources.Attendance.Total_Records + " : " + dt.Rows.Count.ToString() + "";
        lblSelectedRecord.Text = "";
        if (dt.Rows.Count == 0)
        {
            ImgbtnSelectAll.Visible = false;
            imgBtnRestore.Visible = false;
        }
        else
        {
            ImgbtnSelectAll.Visible = true;
            imgBtnRestore.Visible = true;
        }
    }
    protected void btnbindBin_Click(object sender, EventArgs e)
    {
        string condition = string.Empty;
        if (ddlOptionBin.SelectedIndex != 0)
        {
            if (ddlOptionBin.SelectedIndex == 1)
            {
                condition = "convert(" + ddlFieldNameBin.SelectedValue + ",System.String)='" + txtValueBin.Text.Trim() + "'";
            }
            else if (ddlOptionBin.SelectedIndex == 2)
            {
                condition = "convert(" + ddlFieldNameBin.SelectedValue + ",System.String) like '%" + txtValueBin.Text.Trim() + "%'";
            }
            else
            {
                condition = "convert(" + ddlFieldNameBin.SelectedValue + ",System.String) Like '" + txtValueBin.Text.Trim() + "%'";
            }


            DataTable dtCust = (DataTable)Session["dtPBrandBin"];
            DataView view = new DataView(dtCust, condition, "", DataViewRowState.CurrentRows);
            Session["dtInactive"] = view.ToTable();
            lblTotalRecordsBin.Text = Resources.Attendance.Total_Records + " : " + view.ToTable().Rows.Count.ToString() + "";
            GvSalesInquiryBin.DataSource = view.ToTable();
            GvSalesInquiryBin.DataBind();
            lblSelectedRecord.Text = "";
            if (view.ToTable().Rows.Count == 0)
            {
                FillGridBin();
            }
            System.Web.UI.ScriptManager.GetCurrent(this).SetFocus(txtValueBin);
        }
    }
    protected void btnRestoreSelected_Click(object sender, CommandEventArgs e)
    {
        int b = 0;
        DataTable dt = objSInquiryHeader.GetSIHeaderAllFalse(StrCompId, StrBrandId, strLocationId);

        if (GvSalesInquiryBin.Rows.Count != 0)
        {
            if (lblSelectedRecord.Text != "")
            {
                for (int j = 0; j < lblSelectedRecord.Text.Split(',').Length; j++)
                {
                    if (lblSelectedRecord.Text.Split(',')[j] != "")
                    {
                        //Msg = objTax.DeleteTaxMaster(lblSelectedRecord.Text.Split(',')[j].Trim().ToString(), true.ToString(), Session["UserId"].ToString(), DateTime.Now.ToString());
                        b = objSInquiryHeader.DeleteSIHeader(StrCompId, StrBrandId, strLocationId, lblSelectedRecord.Text.Split(',')[j].Trim().ToString(), true.ToString(), Session["UserId"].ToString(), DateTime.Now.ToString());
                    }
                }
            }

            if (b != 0)
            {
                FillGrid();
                FillGridBin();

                lblSelectedRecord.Text = "";
                DisplayMessage("Record Activate");
            }
            else
            {
                int fleg = 0;
                foreach (GridViewRow Gvr in GvSalesInquiryBin.Rows)
                {
                    CheckBox chk = (CheckBox)Gvr.FindControl("chkSelect");
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
        System.Web.UI.ScriptManager.GetCurrent(this).SetFocus(txtValueBin);
    }
    protected void chkCurrent_CheckedChanged(object sender, EventArgs e)
    {
        CheckBox chkSelAll = ((CheckBox)GvSalesInquiryBin.HeaderRow.FindControl("chkCurrent"));
        for (int i = 0; i < GvSalesInquiryBin.Rows.Count; i++)
        {
            ((CheckBox)GvSalesInquiryBin.Rows[i].FindControl("chkSelect")).Checked = chkSelAll.Checked;
            if (chkSelAll.Checked)
            {
                if (!lblSelectedRecord.Text.Split(',').Contains(((Label)(GvSalesInquiryBin.Rows[i].FindControl("lblgvInquiryId"))).Text.Trim().ToString()))
                {
                    lblSelectedRecord.Text += ((Label)(GvSalesInquiryBin.Rows[i].FindControl("lblgvInquiryId"))).Text.Trim().ToString() + ",";
                }
            }
            else
            {
                string temp = string.Empty;
                string[] split = lblSelectedRecord.Text.Split(',');
                foreach (string item in split)
                {
                    if (item != ((Label)(GvSalesInquiryBin.Rows[i].FindControl("lblgvInquiryId"))).Text.Trim().ToString())
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
    protected void chkSelect_CheckedChanged(object sender, EventArgs e)
    {
        string empidlist = string.Empty;
        string temp = string.Empty;
        int index = ((GridViewRow)((CheckBox)sender).Parent.Parent).RowIndex;
        Label lb = (Label)GvSalesInquiryBin.Rows[index].FindControl("lblgvInquiryId");
        if (((CheckBox)GvSalesInquiryBin.Rows[index].FindControl("chkSelect")).Checked)
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
    protected void btnRefreshBin_Click(object sender, EventArgs e)
    {
        FillGrid();
        FillGridBin();
        txtValueBin.Text = "";
        ddlOptionBin.SelectedIndex = 2;
        ddlFieldNameBin.SelectedIndex = 1;
        lblSelectedRecord.Text = "";
        System.Web.UI.ScriptManager.GetCurrent(this).SetFocus(txtValueBin);
    }
    protected void ImgbtnSelectAll_Click(object sender, ImageClickEventArgs e)
    {
        DataTable dtPbrand = (DataTable)Session["dtInactive"];

        if (ViewState["Select"] == null)
        {
            ViewState["Select"] = 1;
            foreach (DataRow dr in dtPbrand.Rows)
            {
                if (!lblSelectedRecord.Text.Split(',').Contains(dr["SInquiryID"]))
                {
                    lblSelectedRecord.Text += dr["SInquiryID"] + ",";
                }
            }
            for (int i = 0; i < GvSalesInquiryBin.Rows.Count; i++)
            {
                string[] split = lblSelectedRecord.Text.Split(',');
                Label lblconid = (Label)GvSalesInquiryBin.Rows[i].FindControl("lblgvInquiryId");
                for (int j = 0; j < lblSelectedRecord.Text.Split(',').Length; j++)
                {
                    if (lblSelectedRecord.Text.Split(',')[j] != "")
                    {
                        if (lblconid.Text.Trim().ToString() == lblSelectedRecord.Text.Split(',')[j].Trim().ToString())
                        {
                            ((CheckBox)GvSalesInquiryBin.Rows[i].FindControl("chkSelect")).Checked = true;
                        }
                    }
                }
            }
        }
        else
        {
            lblSelectedRecord.Text = "";
            DataTable dtUnit1 = (DataTable)Session["dtInactive"];
            GvSalesInquiryBin.DataSource = dtUnit1;
            GvSalesInquiryBin.DataBind();
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
                    b = objSInquiryHeader.DeleteSIHeader(StrCompId, StrBrandId, strLocationId, lblSelectedRecord.Text.Split(',')[j].Trim(), true.ToString(), Session["UserId"].ToString(), DateTime.Now.ToString());
                }
            }
        }

        if (b != 0)
        {
            FillGrid();
            FillGridBin();
            lblSelectedRecord.Text = "";
            ViewState["Select"] = null;
            DisplayMessage("Record Activate");
        }
        else
        {
            int fleg = 0;
            foreach (GridViewRow Gvr in GvSalesInquiryBin.Rows)
            {
                CheckBox chk = (CheckBox)Gvr.FindControl("chkSelect");
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
    #endregion

    #endregion

    #region User defind Funcation
    protected string GetDate(string strDate)
    {
        string strNewDate = string.Empty;
        if (strDate != "")
        {
            strNewDate = DateTime.Parse(strDate).ToString("dd/MMM/yyyy");
        }
        else
        {
            strNewDate = "";
        }
        return strNewDate;
    }
    private void FillGrid()
    {
        DataTable dtBrand = objSInquiryHeader.GetSIHeaderAllTrue(StrCompId, StrBrandId, strLocationId);
        lblTotalRecords.Text = Resources.Attendance.Total_Records + " : " + dtBrand.Rows.Count + "";
        Session["dtCurrency"] = dtBrand;
        Session["dtFilter"] = dtBrand;
        if (dtBrand != null && dtBrand.Rows.Count > 0)
        {
            GvSalesInquiry.DataSource = dtBrand;
            GvSalesInquiry.DataBind();
        }
        else
        {
            GvSalesInquiry.DataSource = null;
            GvSalesInquiry.DataBind();
        }
        lblTotalRecords.Text = Resources.Attendance.Total_Records + " : " + dtBrand.Rows.Count.ToString() + "";
    }
    private void FillCurrency()
    {
        DataTable dsCurrency = null;
        dsCurrency = objCurrency.GetCurrencyMaster();
        if (dsCurrency.Rows.Count > 0)
        {
            ddlCurrency.DataSource = dsCurrency;
            ddlCurrency.DataTextField = "Currency_Name";
            ddlCurrency.DataValueField = "Currency_ID";
            ddlCurrency.DataBind();

            ddlCurrency.Items.Add("--Select--");
            ddlCurrency.SelectedValue = "--Select--";
        }
        else
        {
            ddlCurrency.Items.Add("--Select--");
            ddlCurrency.SelectedValue = "--Select--";
        }
    }
    public void DisplayMessage(string str)
    {
        if (Session["lang"] == null)
        {
            Session["lang"] = "1";
        }
        if (Session["lang"].ToString() == "1")
        {
            ScriptManager.RegisterStartupScript(this, GetType(), "", "alert('" + str + "');", true);
        }
        else if (Session["lang"].ToString() == "2")
        {
            ScriptManager.RegisterStartupScript(this, GetType(), "", "alert('" + GetArebicMessage(str) + "');", true);
        }
    }
    public string GetArebicMessage(string EnglishMessage)
    {
        string ArebicMessage = string.Empty;
        DataTable dtres = (DataTable)Session["MessageDt"];
        if (dtres.Rows.Count != 0)
        {
            ArebicMessage = (new DataView(dtres, "Key='" + EnglishMessage + "'", "", DataViewRowState.CurrentRows).ToTable()).Rows[0]["Value"].ToString();
        }
        if (ArebicMessage == "")
        {
            ArebicMessage = EnglishMessage;
        }
        return ArebicMessage;
    }
    public void Reset()
    {
        FillGrid();
        FillCurrency();
        txtInquiryNo.ReadOnly = false;
        txtInquiryNo.Text = objSInquiryHeader.GetAutoID(StrCompId, StrBrandId, strLocationId);
        txtInquiryDate.Text = DateTime.Now.ToString(ObjSysParam.GetSysParameterByParamName("Date_Format").Rows[0]["Param_Value"].ToString());
        txtOrderCompletionDate.Text = DateTime.Now.ToString(ObjSysParam.GetSysParameterByParamName("Date_Format").Rows[0]["Param_Value"].ToString());
        PnlNewContant.Enabled = true;
        txtCustomerName.Text = "";
        txtRemark.Text = "";
        txtReceivedEmp.Text = "";
        txtHandledEmp.Text = "";
        chkBuyingPriority.Checked = false;
        chkSendMail.Checked = false;
        chkPost.Checked = false;
        txtCondition1.Text = "";
        txtCondition2.Text = "";
        txtCondition3.Text = "";
        txtCondition4.Text = "";
        txtCondition5.Text = "";

        GvProduct.DataSource = null;
        GvProduct.DataBind();
        ChkSendInPurchase.Checked = false;
        hdnSInquiryId.Value = "0";

        btnNew.Text = Resources.Attendance.New;
        ddlFieldName.SelectedIndex = 1;
        ddlOption.SelectedIndex = 2;
        txtValue.Text = "";
        txtValueBin.Text = "";
        lblSelectedRecord.Text = "";
        System.Web.UI.ScriptManager.GetCurrent(this).SetFocus(txtValue);
        txtInquiryNo.Text = GetDocumentNumber();
    
    }
    #endregion

    #region Invoice Section
    protected void txtCustomerName_TextChanged(object sender, EventArgs e)
    {
        string strCustomerId = string.Empty;
        if (txtCustomerName.Text != "")
        {
            strCustomerId = GetCustomerId();
            if (strCustomerId != "" && strCustomerId != "0")
            {
            }
            else
            {
                DisplayMessage("Select In Suggestions Only");
                txtCustomerName.Text = "";
                System.Web.UI.ScriptManager.GetCurrent(this).SetFocus(txtCustomerName);
            }
        }
    }
    protected void txtReceivedEmp_TextChanged(object sender, EventArgs e)
    {
        string strEmployeeId = string.Empty;
        if (txtReceivedEmp.Text != "")
        {
            strEmployeeId = GetEmployeeId(txtReceivedEmp.Text);
            if (strEmployeeId != "" && strEmployeeId != "0")
            {

            }
            else
            {
                DisplayMessage("Select Employee In Suggestions Only");
                txtReceivedEmp.Text = "";
                System.Web.UI.ScriptManager.GetCurrent(this).SetFocus(txtReceivedEmp);
            }
        }
    }
    protected void txtHandledEmp_TextChanged(object sender, EventArgs e)
    {
        string strEmployeeId = string.Empty;
        if (txtHandledEmp.Text != "")
        {
            strEmployeeId = GetEmployeeId(txtHandledEmp.Text);
            if (strEmployeeId != "" && strEmployeeId != "0")
            {

            }
            else
            {
                DisplayMessage("Select Employee In Suggestions Only");
                txtHandledEmp.Text = "";
                System.Web.UI.ScriptManager.GetCurrent(this).SetFocus(txtHandledEmp);
            }
        }
    }
    protected string GetCustomerName(string strCustomerId)
    {
        string strCustomerName = string.Empty;
        if (strCustomerId != "0" && strCustomerId != "")
        {
            DataTable dtCName = objContact.GetContactByContactId(StrCompId, strCustomerId);
            if (dtCName.Rows.Count > 0)
            {
                strCustomerName = dtCName.Rows[0]["Contact_Name"].ToString();
            }
        }
        else
        {
            strCustomerName = "";
        }
        return strCustomerName;
    }
    protected string GetEmployeeName(string strEmployeeId)
    {
        string strEmployeeName = string.Empty;
        if (strEmployeeId != "0" && strEmployeeId != "")
        {
            DataTable dtEName = objEmployee.GetEmployeeMasterById(StrCompId, strEmployeeId);
            if (dtEName.Rows.Count > 0)
            {
                strEmployeeName = dtEName.Rows[0]["Emp_Name"].ToString();
            }
        }
        else
        {
            strEmployeeName = "";
        }
        return strEmployeeName;
    }
    protected string GetProductName(string strProductId)
    {
        string strProductName = string.Empty;
        if (strProductId != "0" && strProductId != "")
        {
            DataTable dtPName = objProductM.GetProductMasterById(StrCompId, strProductId);
            if (dtPName.Rows.Count > 0)
            {
                strProductName = dtPName.Rows[0]["EProductName"].ToString();
            }
        }
        else
        {
            strProductName = "";
        }
        return strProductName;
    }
    protected string GetUnitName(string strUnitId)
    {
        string strUnitName = string.Empty;
        if (strUnitId != "0" && strUnitId != "")
        {
            DataTable dtUName = UM.GetUnitMasterById(StrCompId, strUnitId);
            if (dtUName.Rows.Count > 0)
            {
                strUnitName = dtUName.Rows[0]["Unit_Name"].ToString();
            }
        }
        else
        {
            strUnitName = "";
        }
        return strUnitName;
    }
    #endregion

    #region Add Product Concept
    private void FillUnit()
    {
        DataTable dsUnit = null;
        dsUnit = UM.GetUnitMaster(StrCompId);
        if (dsUnit.Rows.Count > 0)
        {
            ddlUnit.DataSource = dsUnit;
            ddlUnit.DataTextField = "Unit_Name";
            ddlUnit.DataValueField = "Unit_Id";
            ddlUnit.DataBind();

            ddlUnit.Items.Add("--Select--");
            ddlUnit.SelectedValue = "--Select--";
        }
        else
        {
            ddlUnit.Items.Add("--Select--");
            ddlUnit.SelectedValue = "--Select--";
        }
    }
    private void FillProductCurrency()
    {
        DataTable dsPCurrency = null;
        dsPCurrency = objCurrency.GetCurrencyMaster();
        if (dsPCurrency.Rows.Count > 0)
        {
            ddlPCurrency.DataSource = dsPCurrency;
            ddlPCurrency.DataTextField = "Currency_Name";
            ddlPCurrency.DataValueField = "Currency_ID";
            ddlPCurrency.DataBind();

            ddlPCurrency.Items.Add("--Select--");
            ddlPCurrency.SelectedValue = "--Select--";
        }
        else
        {
            ddlPCurrency.Items.Add("--Select--");
            ddlPCurrency.SelectedValue = "--Select--";
        }
    }
    protected string GetCurrencyName(string strCurrencyId)
    {
        string strCurrencyName = string.Empty;
        if (strCurrencyId != "0" && strCurrencyId != "")
        {
            DataTable dtCName = objCurrency.GetCurrencyMasterById(strCurrencyId);
            if (dtCName.Rows.Count > 0)
            {
                strCurrencyName = dtCName.Rows[0]["Currency_Name"].ToString();
            }
        }
        else
        {
            strCurrencyName = "";
        }
        return strCurrencyName;
    }
    [System.Web.Services.WebMethodAttribute(), System.Web.Script.Services.ScriptMethodAttribute()]
    public static string[] GetCompletionListProductName(string prefixText, int count, string contextKey)
    {
        Inv_ProductMaster PM = new Inv_ProductMaster();
        DataTable dt = PM.GetDistinctProductName("1", prefixText);
        string[] str = new string[dt.Rows.Count];
        if (dt.Rows.Count > 0)
        {
            for (int i = 0; i < dt.Rows.Count; i++)
            {
                str[i] = dt.Rows[i]["EProductName"].ToString();
            }
        }
        else
        {
            if (prefixText.Length > 2)
            {
                str = null;
            }
            else
            {
                dt = PM.GetProductMasterTrueAll("1");
                if (dt.Rows.Count > 0)
                {
                    str = new string[dt.Rows.Count];
                    for (int i = 0; i < dt.Rows.Count; i++)
                    {
                        str[i] = dt.Rows[i]["EProductName"].ToString();
                    }
                }
            }
        }
        return str;
    }
    protected void btnAddNewProduct_Click(object sender, EventArgs e)
    {
        pnlProduct1.Visible = true;
        pnlProduct2.Visible = true;
        FillUnit();
        FillProductCurrency();
        txtRequiredQty.Text = "1";
    }
    protected void txtProductName_TextChanged(object sender, EventArgs e)
    {
        if (txtProductName.Text != "")
        {
            DataTable dtProduct = objProductM.GetProductMasterTrueAll(StrCompId);
            dtProduct = new DataView(dtProduct, "EProductName ='" + txtProductName.Text + "'", "", DataViewRowState.CurrentRows).ToTable();
            if (dtProduct.Rows.Count > 0)
            {

                DataTable dt = new DataTable();
                dt.Columns.Add("Product_Id");
                for (int i = 0; i < GvProduct.Rows.Count; i++)
                {
                    dt.Rows.Add(i);
                    Label lblgvProductId = (Label)GvProduct.Rows[i].FindControl("lblgvProductId");
                    
                    dt.Rows[i]["Product_Id"] = lblgvProductId.Text;
                      
                }

                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    if (dt.Rows[i]["Product_Id"].ToString() == dtProduct.Rows[0]["ProductId"].ToString())
                    {

                        DisplayMessage("Product is already exists!");
                        txtProductName.Text = "";
                        txtProductName.Focus();
                        return;
                    }
                }
                string strUnitId = dtProduct.Rows[0]["UnitId"].ToString();
                if (strUnitId != "0" && strUnitId != "")
                {
                    ddlUnit.SelectedValue = strUnitId;
                }
                else
                {
                    FillUnit();
                }
                txtPDescription.Text = dtProduct.Rows[0]["Description"].ToString();
                hdnNewProductId.Value = dtProduct.Rows[0]["ProductId"].ToString();
                txtPDesc.Visible = false;
                pnlPDescription.Visible = true;
         
            }
            else
            {
                FillUnit();
                txtPDescription.Text = "";
                hdnNewProductId.Value = "0";
                txtPDesc.Visible = true;
                pnlPDescription.Visible = false;
         
        
            }
        }
        else
        {
            DisplayMessage("Enter Product Name");
            System.Web.UI.ScriptManager.GetCurrent(this).SetFocus(txtProductName);
        }
    }
    protected void btnProductSave_Click(object sender, EventArgs e)
    {
        string Description = string.Empty;
        if (txtProductName.Text != "")
        {
            string strA = "0";
            foreach (GridViewRow gve in GvProduct.Rows)
            {
                Label lblgvProductName = (Label)gve.FindControl("lblgvProductName");
                if (txtProductName.Text == lblgvProductName.Text)
                {
                    strA = "1";
                }
            }

            if (hdnNewProductId.Value == "0")
            {
                if (txtProductName.Text != "")
                {
                    DataTable dt = objProductM.GetProductMasterTrueAll(StrCompId);
                    dt = new DataView(dt, "EProductName ='" + txtProductName.Text + "'", "", DataViewRowState.CurrentRows).ToTable();
                    if (dt.Rows.Count > 0)
                    {
                        hdnNewProductId.Value = dt.Rows[0]["ProductId"].ToString();

                    }
                    else
                    {
                        hdnNewProductId.Value = "0";
                    }
                }
            }

            if (ddlUnit.SelectedValue != "--Select--")
            {
                hdnUnitId.Value = ddlUnit.SelectedValue;
            }
            else
            {
                DisplayMessage("Select Unit Name");
                System.Web.UI.ScriptManager.GetCurrent(this).SetFocus(ddlUnit);
                return;
            }
            if (txtRequiredQty.Text != "")
            {

            }
            else
            {
                DisplayMessage("Enter Required Quantity");
                System.Web.UI.ScriptManager.GetCurrent(this).SetFocus(txtRequiredQty);
                return;
            }
            if (ddlPCurrency.SelectedValue != "--Select--")
            {
                hdnCurrencyId.Value = ddlPCurrency.SelectedValue;
            }
            else
            {
                DisplayMessage("Select Currency Name");
                System.Web.UI.ScriptManager.GetCurrent(this).SetFocus(ddlPCurrency);
                return;
            }

            if (txtEstimatedUnitPrice.Text != "")
            {
                float flTemp = 0;
                if (float.TryParse(txtEstimatedUnitPrice.Text, out flTemp))
                {
                }
                else
                {
                    txtEstimatedUnitPrice.Text = "";
                    ScriptManager.RegisterStartupScript(this, typeof(string), "OPEN_WINDOW", "window.alert('Enter Numeric Value Only');", true);
                    System.Web.UI.ScriptManager.GetCurrent(this).SetFocus(txtEstimatedUnitPrice);
                    return;
                }
            }
            else
            {
                txtEstimatedUnitPrice.Text = "0";
            }


            if (hdnProductId.Value == "")
            {
                //if (strA == "0")//comment by jitendra upadhyay on 15-10-2013
                //{
                    FillProductChidGird("Save");
                    System.Web.UI.ScriptManager.GetCurrent(this).SetFocus(btnAddNewProduct);
                    pnlProduct1.Visible = false;
                    pnlProduct2.Visible = false;
                //}
                //else
                //{
                //    txtProductName.Text = "";
                //    DisplayMessage("Product Name Already Exists");
                //    System.Web.UI.ScriptManager.GetCurrent(this).SetFocus(txtProductName);
                //}
            }
            else
            {
                if (txtProductName.Text == hdnProductName.Value)
                {
                    FillProductChidGird("Edit");
                    System.Web.UI.ScriptManager.GetCurrent(this).SetFocus(btnAddNewProduct);
                    pnlProduct1.Visible = false;
                    pnlProduct2.Visible = false;
                }
                else
                {
                    //if (strA == "0")
                    //{
                      FillProductChidGird("Edit");
                        System.Web.UI.ScriptManager.GetCurrent(this).SetFocus(btnAddNewProduct);
                        pnlProduct1.Visible = false;
                        pnlProduct2.Visible = false;
                    //}
                    //else
                    //{
                    //    txtProductName.Text = "";
                    //    DisplayMessage("Product Name Already Exists");
                    //    System.Web.UI.ScriptManager.GetCurrent(this).SetFocus(txtProductName);
                    //}
                }
            }
        }
        else
        {
            DisplayMessage("Enter Product Name");
            System.Web.UI.ScriptManager.GetCurrent(this).SetFocus(txtProductName);
        }
        txtProductName.Focus();
        txtPDesc.Text = "";
    }
    protected void btnProductCancel_Click(object sender, EventArgs e)
    {
        ResetProduct();
    }
    protected void btnClosePanel_Click(object sender, EventArgs e)
    {
        pnlProduct1.Visible = false;
        pnlProduct2.Visible = false;
    }
    public void ResetProduct()
    {
        txtProductName.Text = "";
        FillUnit();
        txtPDescription.Text = "";
        txtRequiredQty.Text = "1";
        FillProductCurrency();
        txtEstimatedUnitPrice.Text = "";
        hdnProductId.Value = "";
        hdnProductName.Value = "";
        hdnNewProductId.Value = "0";
    }
    public DataTable CreateProductDataTable()
    {
        DataTable dt = new DataTable();
        dt.Columns.Add("Serial_No");
        dt.Columns.Add("Product_Id");
        dt.Columns.Add("UnitId");
        dt.Columns.Add("ProductDescription");
        dt.Columns.Add("Quantity");
        dt.Columns.Add("Currency_Id");
        dt.Columns.Add("EstimatedUnitPrice");
        return dt;
    }
    public DataTable FillProductDataTabel()
    {
        string strNewSNo = string.Empty;
        DataTable dt = CreateProductDataTable();
        if (GvProduct.Rows.Count > 0)
        {
            for (int i = 0; i < GvProduct.Rows.Count + 1; i++)
            {
                if (dt.Rows.Count != GvProduct.Rows.Count)
                {
                    dt.Rows.Add(i);
                    Label lblgvSNo = (Label)GvProduct.Rows[i].FindControl("lblSNo");
                    Label lblgvProductId = (Label)GvProduct.Rows[i].FindControl("lblgvProductId");
                    Label lblgvUnitId = (Label)GvProduct.Rows[i].FindControl("lblgvUnitId");
                    Label lblgvPDescription = (Label)GvProduct.Rows[i].FindControl("lblgvProductDescription");
                    Label lblgvReqQty = (Label)GvProduct.Rows[i].FindControl("lblgvRequiredQty");
                    Label lblgvCurrencyId = (Label)GvProduct.Rows[i].FindControl("lblgvCurrencyId");
                    Label lblgvEstimatedUnitPrice = (Label)GvProduct.Rows[i].FindControl("lblgvEstimatedUnitPrice");

                    dt.Rows[i]["Serial_No"] = lblgvSNo.Text;
                    strNewSNo = lblgvSNo.Text;
                    dt.Rows[i]["Product_Id"] = lblgvProductId.Text;
                    dt.Rows[i]["UnitId"] = lblgvUnitId.Text;
                    dt.Rows[i]["ProductDescription"] = lblgvPDescription.Text;
                    dt.Rows[i]["Quantity"] = lblgvReqQty.Text;
                    dt.Rows[i]["Currency_Id"] = lblgvCurrencyId.Text;
                    dt.Rows[i]["EstimatedUnitPrice"] = lblgvEstimatedUnitPrice.Text;
                }
                else
                {
                    dt.Rows.Add(i);
                    dt.Rows[i]["Serial_No"] = (float.Parse(strNewSNo) + 1).ToString();
                    dt.Rows[i]["Product_Id"] = hdnNewProductId.Value;
                    dt.Rows[i]["UnitId"] = hdnUnitId.Value;
                    if (hdnNewProductId.Value == "0")
                    {
                        dt.Rows[i]["ProductDescription"] = txtPDesc.Text;
                    }
                    else
                    {
                        dt.Rows[i]["ProductDescription"] = txtPDescription.Text;
                   
                    }
                    dt.Rows[i]["Quantity"] = txtRequiredQty.Text;
                    dt.Rows[i]["Currency_Id"] = hdnCurrencyId.Value;
                    dt.Rows[i]["EstimatedUnitPrice"] = txtEstimatedUnitPrice.Text;
                }
            }
        }
        else
        {

            dt.Rows.Add(0);
            dt.Rows[0]["Serial_No"] = "1";
            dt.Rows[0]["Product_Id"] = hdnNewProductId.Value;
            dt.Rows[0]["UnitId"] = hdnUnitId.Value;
            if (hdnNewProductId.Value == "0")
            {
                dt.Rows[0]["ProductDescription"] = txtPDesc.Text;
          
            }
            else
            {

                dt.Rows[0]["ProductDescription"] = txtPDescription.Text;
            }
            dt.Rows[0]["Quantity"] = txtRequiredQty.Text;
            dt.Rows[0]["Currency_Id"] = hdnCurrencyId.Value;
            dt.Rows[0]["EstimatedUnitPrice"] = txtEstimatedUnitPrice.Text;
        }
        if (dt.Rows.Count > 0)
        {
            GvProduct.DataSource = dt;
            GvProduct.DataBind();

            foreach (GridViewRow gvr in GvProduct.Rows)
            {
                Label lblProductId = (Label)gvr.FindControl("lblgvProductId");
                Label lblProductName = (Label)gvr.FindControl("lblgvProductName");
                Label lblProductDesc = (Label)gvr.FindControl("lblgvProductDescription");

                if (lblProductId.Text == "0")
                {
                    if (lblProductDesc.Text != "")
                    {
                        lblProductName.Text = lblProductDesc.Text;
                    }
                }
            }
        }
        return dt;
    }
    public DataTable FillProductDataTabelDelete()
    {
        DataTable dt = CreateProductDataTable();
        for (int i = 0; i < GvProduct.Rows.Count; i++)
        {
            dt.Rows.Add(i);
            Label lblgvSNo = (Label)GvProduct.Rows[i].FindControl("lblSNo");
            Label lblgvProductId = (Label)GvProduct.Rows[i].FindControl("lblgvProductId");
            Label lblgvUnitId = (Label)GvProduct.Rows[i].FindControl("lblgvUnitId");
            Label lblgvPDescription = (Label)GvProduct.Rows[i].FindControl("lblgvProductDescription");
            Label lblgvReqQty = (Label)GvProduct.Rows[i].FindControl("lblgvRequiredQty");
            Label lblgvCurrencyId = (Label)GvProduct.Rows[i].FindControl("lblgvCurrencyId");
            Label lblgvEstimatedUnitPrice = (Label)GvProduct.Rows[i].FindControl("lblgvEstimatedUnitPrice");

            dt.Rows[i]["Serial_No"] = lblgvSNo.Text;
            dt.Rows[i]["Product_Id"] = lblgvProductId.Text;
            dt.Rows[i]["UnitId"] = lblgvUnitId.Text;
            dt.Rows[i]["ProductDescription"] = lblgvPDescription.Text;
            dt.Rows[i]["Quantity"] = lblgvReqQty.Text;
            dt.Rows[i]["Currency_Id"] = lblgvCurrencyId.Text;
            dt.Rows[i]["EstimatedUnitPrice"] = lblgvEstimatedUnitPrice.Text;
        }

        DataView dv = new DataView(dt);
        dv.RowFilter = "Serial_No<>'" + hdnProductId.Value + "'";
        dt = (DataTable)dv.ToTable();
        return dt;
    }
    protected void imgBtnProductEdit_Command(object sender, CommandEventArgs e)
    {
        hdnProductId.Value = e.CommandArgument.ToString();
        FillProductDataTabelEdit();
        pnlProduct1.Visible = true;
        pnlProduct2.Visible = true;
    }
    public DataTable FillProductDataTabelEdit()
    {
        DataTable dt = CreateProductDataTable();

        for (int i = 0; i < GvProduct.Rows.Count; i++)
        {
            dt.Rows.Add(i);
            Label lblgvSNo = (Label)GvProduct.Rows[i].FindControl("lblSNo");
            Label lblgvProductId = (Label)GvProduct.Rows[i].FindControl("lblgvProductId");
            Label lblgvUnitId = (Label)GvProduct.Rows[i].FindControl("lblgvUnitId");
            Label lblgvPDescription = (Label)GvProduct.Rows[i].FindControl("lblgvProductDescription");
            Label lblgvReqQty = (Label)GvProduct.Rows[i].FindControl("lblgvRequiredQty");
            Label lblgvCurrencyId = (Label)GvProduct.Rows[i].FindControl("lblgvCurrencyId");
            Label lblgvEstimatedUnitPrice = (Label)GvProduct.Rows[i].FindControl("lblgvEstimatedUnitPrice");

            dt.Rows[i]["Serial_No"] = lblgvSNo.Text;
            dt.Rows[i]["Product_Id"] = lblgvProductId.Text;
            dt.Rows[i]["UnitId"] = lblgvUnitId.Text;
            dt.Rows[i]["ProductDescription"] = lblgvPDescription.Text;
            dt.Rows[i]["Quantity"] = lblgvReqQty.Text;
            dt.Rows[i]["Currency_Id"] = lblgvCurrencyId.Text;
            dt.Rows[i]["EstimatedUnitPrice"] = lblgvEstimatedUnitPrice.Text;
        }

        DataView dv = new DataView(dt);
        dv.RowFilter = "Serial_No='" + hdnProductId.Value + "'";
        dt = (DataTable)dv.ToTable();
        if (dt.Rows.Count != 0)
        {
            txtProductName.Text = GetProductName(dt.Rows[0]["Product_Id"].ToString());
            FillUnit();
            ddlUnit.SelectedValue = dt.Rows[0]["UnitId"].ToString();
            if (txtProductName.Text != "")
            {
                txtPDescription.Text = dt.Rows[0]["ProductDescription"].ToString();
                pnlPDescription.Visible = true;
                txtPDesc.Visible = false;
            }
            else
            {
                txtProductName.Text = dt.Rows[0]["ProductDescription"].ToString();
                txtPDesc.Text = dt.Rows[0]["ProductDescription"].ToString();
                pnlPDescription.Visible = false;
                txtPDesc.Visible = true;
            }

            //txtPDescription.Text = dt.Rows[0]["ProductDescription"].ToString();
            txtRequiredQty.Text = dt.Rows[0]["Quantity"].ToString();
            FillProductCurrency();
            ddlPCurrency.SelectedValue = dt.Rows[0]["Currency_Id"].ToString();
            txtEstimatedUnitPrice.Text = dt.Rows[0]["EstimatedUnitPrice"].ToString();
            hdnProductName.Value = GetProductName(dt.Rows[0]["Product_Id"].ToString());
        }
        return dt;
    }
    protected void imgBtnProductDelete_Command(object sender, CommandEventArgs e)
    {
        hdnProductId.Value = e.CommandArgument.ToString();
        FillProductChidGird("Del");
    }
    public void FillProductChidGird(string CommandName)
    {
        DataTable dt = new DataTable();
        if (CommandName.ToString() == "Del")
        {
            dt = FillProductDataTabelDelete();
        }
        else if (CommandName.ToString() == "Edit")
        {
            dt = FillProductDataTableUpdate();
        }
        else
        {
            dt = FillProductDataTabel();
        }
        GvProduct.DataSource = dt;
        GvProduct.DataBind();

        foreach (GridViewRow gvr in GvProduct.Rows)
        {
            Label lblProductId = (Label)gvr.FindControl("lblgvProductId");
            Label lblProductName = (Label)gvr.FindControl("lblgvProductName");
            Label lblProductDesc = (Label)gvr.FindControl("lblgvProductDescription");

            if (lblProductId.Text == "0")
            {
                if (lblProductDesc.Text != "")
                {
                    lblProductName.Text = lblProductDesc.Text;
                }
            }
        }

        ResetProduct();
    }
    public DataTable FillProductDataTableUpdate()
    {
        DataTable dt = CreateProductDataTable();
        for (int i = 0; i < GvProduct.Rows.Count; i++)
        {
            dt.Rows.Add(i);
            Label lblSNo = (Label)GvProduct.Rows[i].FindControl("lblSNo");
            Label lblgvProductId = (Label)GvProduct.Rows[i].FindControl("lblgvProductId");
            Label lblgvUnitId = (Label)GvProduct.Rows[i].FindControl("lblgvUnitId");
            Label lblgvPDescription = (Label)GvProduct.Rows[i].FindControl("lblgvProductDescription");
            Label lblgvReqQty = (Label)GvProduct.Rows[i].FindControl("lblgvRequiredQty");
            Label lblgvCurrencyId = (Label)GvProduct.Rows[i].FindControl("lblgvCurrencyId");
            Label lblgvEstimatedUnitPrice = (Label)GvProduct.Rows[i].FindControl("lblgvEstimatedUnitPrice");

            dt.Rows[i]["Serial_No"] = lblSNo.Text;
            dt.Rows[i]["Product_Id"] = lblgvProductId.Text;
            dt.Rows[i]["UnitId"] = lblgvUnitId.Text;
            dt.Rows[i]["ProductDescription"] = lblgvPDescription.Text;
            dt.Rows[i]["Quantity"] = lblgvReqQty.Text;
            dt.Rows[i]["Currency_Id"] = lblgvCurrencyId.Text;
            dt.Rows[i]["EstimatedUnitPrice"] = lblgvEstimatedUnitPrice.Text;
        }
        for (int i = 0; i < dt.Rows.Count; i++)
        {
            if (hdnProductId.Value == dt.Rows[i]["Serial_No"].ToString())
            {
                dt.Rows[i]["Product_Id"] = hdnNewProductId.Value;
                dt.Rows[i]["UnitId"] = hdnUnitId.Value;
                if (hdnNewProductId.Value == "0")
                {
                    dt.Rows[i]["ProductDescription"] = txtPDesc.Text;
               
                }
                else
                {
                    dt.Rows[i]["ProductDescription"] = txtPDescription.Text;
                }
                dt.Rows[i]["Quantity"] = txtRequiredQty.Text;
                dt.Rows[i]["Currency_Id"] = hdnCurrencyId.Value;
                dt.Rows[i]["EstimatedUnitPrice"] = txtEstimatedUnitPrice.Text;
            }
        }
        return dt;
    }

    #endregion
    public string getStatus(string Status)
    {
        if (Status == "FWInPur")
        {
            Status = "Forward In Purchase";
        }
        return Status;
    }
}
