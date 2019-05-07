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

public partial class Inventory_TransferRequest : System.Web.UI.Page
{
    #region Class Object

    Inv_TransferRequestHeader ObjTrans = new Inv_TransferRequestHeader();
    Inv_TransferRequestDetail OBjtransDetail = new Inv_TransferRequestDetail();
    Inv_UnitMaster objUnit = new Inv_UnitMaster();
    Inv_ProductMaster ObjProductMaster = new Inv_ProductMaster();
    SystemParameter ObjSysParam = new SystemParameter();
    UserMaster ObjUser = new UserMaster();
    LocationMaster objLocation = new LocationMaster();
    Set_DocNumber objDocNo = new Set_DocNumber();
    Common cmn = new Common();
    string StrCompId = string.Empty;
    string StrBrandId = string.Empty;
    string StrLocationId = string.Empty;
    string UserId = string.Empty;
    #endregion
    protected void Page_Load(object sender, EventArgs e)
    {

        if (Session["UserId"] == null)
        {
            Response.Redirect("~/ERPLogin.aspx");
        }

        if (!IsPostBack)
        {

            StrBrandId = Session["BrandId"].ToString();
            StrLocationId = Session["LocId"].ToString();
            StrCompId = Session["CompId"].ToString();
            UserId = Session["UserId"].ToString();
            txtlRequestNo.Text = GetDocumentNumber(); //updated by jitendra on 27-9-2013
            btnList_Click(null, null);
            FillUnit();
            txtRequestdate.Text = DateTime.Now.ToString(ObjSysParam.GetSysParameterByParamName("Date_Format").Rows[0]["Param_Value"].ToString());
            Fillgrid();
            FillGridBin();
            btnReset_Click(null, null);
        }
        AllPageCode();
    }



    #region System Funcation :-
    protected void btnList_Click(object sender, EventArgs e)
    {
        pnlMenuList.BackColor = System.Drawing.ColorTranslator.FromHtml("#ccddee");

        pnlMenuNew.BackColor = System.Drawing.ColorTranslator.FromHtml("#90bde9");
        pnlMenuBin.BackColor = System.Drawing.ColorTranslator.FromHtml("#90bde9");

        PnlList.Visible = true;
        PnlNewEdit.Visible = false;
        PnlBin.Visible = false;
        txtValue.Focus();
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
    protected void btnBin_Click(object sender, EventArgs e)
    {
        pnlMenuBin.BackColor = System.Drawing.ColorTranslator.FromHtml("#ccddee");

        pnlMenuNew.BackColor = System.Drawing.ColorTranslator.FromHtml("#90bde9");
        pnlMenuList.BackColor = System.Drawing.ColorTranslator.FromHtml("#90bde9");
        PnlNewEdit.Visible = false;
        PnlBin.Visible = true;
        PnlList.Visible = false;
        FillGridBin();
        txtbinValue.Focus();
    }

    protected void btnSave_Click(object sender, EventArgs e)
    {
        if (txtlRequestNo.Text == "")
        {
            ViewState["Return"] = 1;
            DisplayMessage("Enter Request No.");
            txtlRequestNo.Focus();
            return;
        }
        if (txtLocationName.Text == "")
        {
            ViewState["Return"] = 1;
            DisplayMessage("Enter Location Name");
            txtLocationName.Focus();
            return;
      
        }
        if (gvProductRequest.Rows.Count == 0)
        {
            ViewState["Return"] = 1;
            DisplayMessage("Enter Product Details");
            btnAddProduct.Focus();
            return;

        }
        if (txtTermCondition.Text == "")
        {
            ViewState["Return"] = 1;
            DisplayMessage("Enter Discription");

            txtTermCondition.Focus();
            return;
        }
        if (txtLocationName.Text == "")
        {
            DisplayMessage("Enter Location Name");
            txtLocationName.Focus();
            return;
        }
        string post = string.Empty;
        if (ChkPost.Checked == true)
        {
            post = "Y";
        }
        else
        {
            post = "N";
        }
        string LocationId = string.Empty;
        DataTable Dtlocation = objLocation.GetLocationMasterByLocationName(StrCompId, txtLocationName.Text);
        if (Dtlocation.Rows.Count == 0)
        {
            DisplayMessage("Invalid Location");
            txtLocationName.Focus();
            return;
        }
        else
        {
        LocationId = Dtlocation.Rows[0]["Location_Id"].ToString();
        }


        int b = 0;
        if (editid.Value == "")
        {

            b = ObjTrans.InsertTransferRequestHeader(StrCompId.ToString(), StrBrandId.ToString(), StrLocationId.ToString(), txtlRequestNo.Text, txtRequestdate.Text, txtTermCondition.Text, post,"0" ,LocationId.ToString(), false.ToString(), false.ToString(), "", "", "", false.ToString(), DateTime.Now.ToString(), true.ToString(), UserId.ToString(), DateTime.Now.ToString(), UserId.ToString(), DateTime.Now.ToString());

            if (b != 0)
            {
                DisplayMessage("Record Saved");

            }
        }
        else
        {


           

            b = ObjTrans.UpdateTransferRequestHeader(editid.Value, StrCompId.ToString(), StrBrandId.ToString(), StrLocationId.ToString(), txtlRequestNo.Text, txtRequestdate.Text, txtTermCondition.Text, post,"0",LocationId.ToString(), "", false.ToString(), "", "", "", false.ToString(), DateTime.Now.ToString(), true.ToString(), UserId.ToString(), DateTime.Now.ToString(), UserId.ToString(), DateTime.Now.ToString());

            if (b != 0)
            {
                DisplayMessage("Record Update");
                btnList_Click(null, null);
            }

        }
        ViewState["RequestNo"] = txtlRequestNo.Text;



        Fillgrid();
        Reset();
    }

    protected void btnReset_Click(object sender, EventArgs e)
    {
        if (editid.Value == "")
        {
                string Id = ObjTrans.getAutoId();
                OBjtransDetail.DeleteTransferRequestDetailBYReqID(StrCompId.ToString(), StrBrandId.ToString(), StrLocationId.ToString(), Id.ToString());
            
        }
        Reset();
        txtlRequestNo.Focus();
    }
    protected void btnCancel_Click(object sender, EventArgs e)
    {
        if (editid.Value == "")
        {
                string Id = ObjTrans.getAutoId();
                OBjtransDetail.DeleteTransferRequestDetailBYReqID(StrCompId.ToString(), StrBrandId.ToString(), StrLocationId.ToString(), Id.ToString());
            
        }

        Reset();
        btnList_Click(null, null);

    }

    protected void btnEdit_Command(object sender, CommandEventArgs e)
    {
       
        DataTable dt = ObjTrans.GetRecordUsingTransId(StrCompId.ToString(), StrBrandId.ToString(), StrLocationId.ToString(), e.CommandArgument.ToString());
        if (dt.Rows.Count != 0)
        {
            if (dt.Rows[0]["Status"].ToString() != "0")
            {
                DisplayMessage("Transfer Request in use,can not be Update");
                return;
            }
            editid.Value = e.CommandArgument.ToString();
            txtlRequestNo.Text = dt.Rows[0]["RequestNo"].ToString();
            DataTable dtlocation = objLocation.GetLocationMasterById(StrCompId, dt.Rows[0]["RequestLocationID"].ToString());



            txtRequestdate.Text = Convert.ToDateTime(dt.Rows[0]["TDate"].ToString()).ToString(ObjSysParam.GetSysParameterByParamName("Date_Format").Rows[0]["Param_Value"].ToString());
            txtTermCondition.Text = dt.Rows[0]["Remark"].ToString();
            txtLocationName.Text = dtlocation.Rows[0]["Location_Name"].ToString();
            string Post = dt.Rows[0]["Post"].ToString();
            if (Post.Trim() == "Y")
            {
                ChkPost.Checked = true;
                ChkPost.Enabled = false;
            }
            else
            {
                ChkPost.Checked = false;
                ChkPost.Enabled = true;
            }
            fillgridDetail();
            btnNew_Click(null, null);
            btnNew.Text = Resources.Attendance.Edit;
        }
    }
    protected void btnRefresh_Click(object sender, ImageClickEventArgs e)
    {
        txtValue.Text = "";
        ddlFieldName.SelectedIndex = 0;
        ddlOption.SelectedIndex = 3;
        txtValue.Focus();
        Fillgrid();
    }

    protected void IbtnDelete_Command(object sender, CommandEventArgs e)
    {
        DataTable DtTransHeader = ObjTrans.GetRecordUsingTransId(StrCompId, StrBrandId, StrLocationId, e.CommandArgument.ToString());
        if (DtTransHeader.Rows.Count > 0)
        {
            if (DtTransHeader.Rows[0]["Status"].ToString() != "0")
            {
                DisplayMessage("Transfer Request in use,can not be Delete");
                return;
            }
        }
        ObjTrans.DeleteTransferRequestHeader(StrCompId.ToString(), StrBrandId.ToString(), StrLocationId.ToString(), e.CommandArgument.ToString(), false.ToString(), UserId.ToString(), DateTime.Now.ToString());
        Fillgrid();
        FillGridBin();
        DisplayMessage("Record Deleted");

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
            DataTable dtCust = (DataTable)Session["DtTransferRequest"];


            DataView view = new DataView(dtCust, condition, "", DataViewRowState.CurrentRows);
            Session["dtFilter"] = view.ToTable();
            lblTotalRecords.Text = Resources.Attendance.Total_Records + ": " + view.ToTable().Rows.Count.ToString() + "";

            gvTransferRequest.DataSource = view.ToTable();
            gvTransferRequest.DataBind();
            AllPageCode();

            btnbind.Focus();

        }

    }



    protected void btnClosePanel_Click(object sender, ImageClickEventArgs e)
    {
        pnlProduct1.Visible = false;
        pnlProduct2.Visible = false;
    }
    protected void btnProductSave_Click(object sender, EventArgs e)
    {
        if (txtProductName.Text == "")
        {
            DisplayMessage("Enter Product Name");
            txtProductName.Text = "";
            txtProductName.Focus();
            return;
        }
        if (ddlUnit.SelectedIndex == 0)
        {
            DisplayMessage("Select Unit");
            ddlUnit.SelectedIndex = 0;
            ddlUnit.Focus();
            return;

        }

        if (txtUnitCost.Text == "")
        {
            DisplayMessage("Enter Unit Cost");
            txtUnitCost.Focus();
            return;

        }


        if (txtRequestQty.Text == "")
        {
            txtRequestQty.Text = "1";
        }
        string ReqId = string.Empty;
        string ProductId = string.Empty;
        string UnitId = string.Empty;
        if (editid.Value == "")
        {
            ReqId = ObjTrans.getAutoId();   // confusion
        }
        else
        {
            ReqId = editid.Value.ToString();
        }
        string serailNo = string.Empty;

        if (hidProduct.Value == "")
        {
            int Serial = gvProductRequest.Rows.Count;
            Serial = Serial + 1;
            serailNo = Serial.ToString();
        }else
        {
         serailNo = ViewState["SerialNo"].ToString();
        }
       
        

        if (txtProductName.Text != "")
        {
            DataTable dt = new DataView(ObjProductMaster.GetProductMasterAll(StrCompId.ToString()), "EProductName='" + txtProductName.Text.ToString() + "'", "", DataViewRowState.CurrentRows).ToTable();
            if (dt.Rows.Count != 0)
            {
                ProductId = dt.Rows[0]["ProductId"].ToString();
            }
            else
            {
                ProductId = "0";
            }
        }
        if (ddlUnit.SelectedIndex == 0)
        {

        }
        else
        {
            UnitId = ddlUnit.SelectedValue.ToString();
        }
        if (hidProduct.Value == "")
        {
            OBjtransDetail.InsertTransferRequestDetail(StrCompId.ToString(), StrBrandId.ToString(), StrLocationId.ToString(),  serailNo.ToString(), ProductId.ToString(),ReqId.ToString(),  UnitId.ToString(),txtUnitCost.Text,txtRequestQty.Text.ToString(),"0","0", "", "", "", "", "", true.ToString(), DateTime.Now.ToString(), true.ToString(), UserId.ToString(), DateTime.Now.ToString(), UserId.ToString(), DateTime.Now.ToString());

        }
        else
        {
            OBjtransDetail.UpdateTransferRequestDetail(StrCompId.ToString(), StrBrandId.ToString(), StrLocationId.ToString(), hidProduct.Value.ToString(),ReqId, serailNo.ToString(), ProductId.ToString(),  UnitId.ToString(),txtUnitCost.Text, txtRequestQty.Text.ToString(),"0","0", "", "", "", "", "", true.ToString(), DateTime.Now.ToString(), true.ToString(), UserId.ToString(), DateTime.Now.ToString());
            pnlProduct1.Visible = false;

            pnlProduct2.Visible = false;
        }
        fillgridDetail();
        ResetDetail();


    }


    protected void btnEdit_Command1(object sender, CommandEventArgs e)
    {
        pnlProduct1.Visible = true;
        pnlProduct2.Visible = true;
        hidProduct.Value = e.CommandArgument.ToString();
        DataTable dt = OBjtransDetail.GetTransferRequestDetail(StrCompId.ToString(), StrBrandId.ToString(), StrLocationId.ToString(), "0", e.CommandArgument.ToString());
        if (dt.Rows.Count != 0)
        {
            if (dt.Rows[0]["ProductID"].ToString() != "0")
            {
                txtProductName.Text = ProductName(dt.Rows[0]["ProductID"].ToString());
            }
            else
            {
               
            }
       
            ddlUnit.SelectedValue = dt.Rows[0]["UnitId"].ToString();
            //txtPDescription.Text = dt.Rows[0]["ProductDescription"].ToString();
            txtRequestQty.Text = dt.Rows[0]["Quantity"].ToString();
            txtUnitCost.Text = dt.Rows[0]["UnitCost"].ToString();
            ViewState["SerialNo"] = dt.Rows[0]["SerialNo"].ToString();
        }
    }
    protected void IbtnDelete_Command1(object sender, CommandEventArgs e)
    {
        OBjtransDetail.DeleteTransferRequestDetail(StrCompId.ToString(), StrBrandId.ToString(), StrLocationId.ToString(), e.CommandArgument.ToString(), "False", UserId.ToString(), DateTime.Now.ToString());
        fillgridDetail();

    }
    protected void btnProductCancel_Click(object sender, EventArgs e)
    {
        btnClosePanel_Click(null, null);
        ResetDetail();

    }
    protected void btnAddProduct_Click(object sender, EventArgs e)
    {
        pnlProduct1.Visible = true;
        pnlProduct2.Visible = true;
        txtProductName.Focus();
    }
    protected void txtProductName_TextChanged(object sender, EventArgs e)
    {
        if (txtProductName.Text != "")
        {
            DataTable dt = ObjProductMaster.GetProductMasterTrueAll(StrCompId);
            dt = new DataView(dt, "EProductName ='" + txtProductName.Text + "'", "", DataViewRowState.CurrentRows).ToTable();
            if (dt.Rows.Count > 0)
            {
               

                string strUnitId = dt.Rows[0]["UnitId"].ToString();
                if (strUnitId != "0" && strUnitId != "")
                {
                    ddlUnit.SelectedValue = strUnitId;
                }
                else
                {
                    FillUnit();
                }
                txtPDescription.Text = dt.Rows[0]["Description"].ToString();
                txtUnitCost.Focus();
                }
            else
            {
                DisplayMessage("Select Product");
                txtProductName.Focus();
                txtProductName.Text = "";
                txtPDescription.Text = "";
                return;
                //FillUnit();
  }
        }
        else
        {
            DisplayMessage("Enter Product Name");
            System.Web.UI.ScriptManager.GetCurrent(this).SetFocus(txtProductName);
        }



    }
    protected void gvTransferRequest_Sorting(object sender, GridViewSortEventArgs e)
    {
        HDFSort.Value = HDFSort.Value == "ASC" ? "DESC" : "ASC";
        DataTable dt = new DataTable();
        dt = (DataTable)Session["dtFilter"];
        DataView dv = new DataView(dt);
        string Query = "" + e.SortExpression + " " + HDFSort.Value + "";
        dv.Sort = Query;
        dt = dv.ToTable();
        Session["dtFilter"] = dt;
        gvTransferRequest.DataSource = dt;
        gvTransferRequest.DataBind();
        AllPageCode();
        gvTransferRequest.HeaderRow.Focus();

    }
    protected void gvTransferRequest_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        gvTransferRequest.PageIndex = e.NewPageIndex;

        DataTable dt = (DataTable)Session["dtFilter"];
        gvTransferRequest.DataSource = dt;
        gvTransferRequest.DataBind();
        AllPageCode();
    }
    #endregion
    public string GetDocumentNumber()
    {
        string DocumentNo = string.Empty;

        DataTable Dt = objDocNo.GetDocumentNumberAll(StrCompId, "11", "93");

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
                DocumentNo += StrLocationId;
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
                DocumentNo += "-" + (Convert.ToInt32(ObjTrans.GetAllRecord(StrCompId.ToString(), StrBrandId, StrLocationId, "0").Rows.Count) + 1).ToString();
            }
            else
            {
                DocumentNo += (Convert.ToInt32(ObjTrans.GetAllRecord(StrCompId.ToString(), StrBrandId, StrLocationId, "0").Rows.Count) + 1).ToString();

            }
        }
        else
        {
            DocumentNo += (Convert.ToInt32(ObjTrans.GetAllRecord(StrCompId.ToString(), StrBrandId, StrLocationId, "0").Rows.Count) + 1).ToString();
        }

        return DocumentNo;

    }

    #region AllPageCode
    public void AllPageCode()
    {
        Page.Title = ObjSysParam.GetSysTitle();

        StrBrandId = Session["BrandId"].ToString();
        StrLocationId = Session["LocId"].ToString();

        UserId = Session["UserId"].ToString();
        StrCompId = Session["CompId"].ToString();
        Session["AccordianId"] = "11";
        Session["HeaderText"] = "Inventory";
        DataTable dtAllPageCode = cmn.GetAllPagePermission(UserId.ToString(), "11", "93");
        if (dtAllPageCode.Rows.Count != 0)
        {
            if (dtAllPageCode.Rows[0][0].ToString() == "SuperAdmin")
            {
                btnSave.Visible = true;
                btnProductSave.Visible = true;
                foreach (GridViewRow Row in gvTransferRequest.Rows)
                {
                    //((ImageButton)Row.FindControl("IbtnPrint")).Visible = true;
                    ((ImageButton)Row.FindControl("btnEdit")).Visible = true;
                    ((ImageButton)Row.FindControl("IbtnDelete")).Visible = true;
                }
                foreach (GridViewRow Row in gvProductRequest.Rows)
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
                        btnProductSave.Visible = true;
                    }
                    foreach (GridViewRow Row in gvTransferRequest.Rows)
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
                    foreach (GridViewRow Row in gvProductRequest.Rows)
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
                    foreach (GridViewRow Row in gvProductRequest.Rows)
                    {
                        if (Convert.ToBoolean(DtRow["Op_Print"].ToString()))
                        {
                            ((ImageButton)Row.FindControl("IbtnPrint")).Visible = true;
                        }

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
    #endregion

    #region Auto Complete Method

    [System.Web.Services.WebMethodAttribute(), System.Web.Script.Services.ScriptMethodAttribute()]
    public static string[] GetCompletionListProductName(string prefixText, int count, string contextKey)
    {
        Inv_ProductMaster ObjInvProductMaster = new Inv_ProductMaster();
        DataTable dt = ObjInvProductMaster.GetProductMasterTrueAll(HttpContext.Current.Session["CompId"].ToString());
        DataTable dtTemp = dt.Copy();

        dt = new DataView(dt, "EProductName like '" + prefixText.ToString() + "%'", "", DataViewRowState.CurrentRows).ToTable();
        if (dt.Rows.Count == 0)
        {
            dt = dtTemp.Copy();

        }

        string[] txt = new string[dt.Rows.Count];


        for (int i = 0; i < dt.Rows.Count; i++)
        {
            txt[i] = dt.Rows[i]["EProductName"].ToString();
        }


        return txt;
    }
    #endregion

    #region User Defind Funcation

    public void ResetDetail()
    {
        txtProductName.Text = "";
        txtUnitCost.Text = "0";
        ddlUnit.SelectedIndex = 0;
        txtRequestQty.Text = "1";
        hidProduct.Value = "";
        txtPDescription.Text = "";
    }


    public void FillUnit()
    {
        DataTable dt = objUnit.GetUnitMasterAll(StrCompId.ToString());
        ddlUnit.DataSource = dt;
        ddlUnit.DataTextField = "Unit_Name";
        ddlUnit.DataValueField = "Unit_Id";
        ddlUnit.DataBind();
        ddlUnit.Items.Insert(0, "Select");
        ddlUnit.SelectedIndex = 0;
    }
    public void fillgridDetail()
    {
        string ReqId = ObjTrans.getAutoId();
        if (editid.Value == "")
        {
            ReqId = ObjTrans.getAutoId();
        }
        else
        {
            ReqId = editid.Value.ToString();
        }
        DataTable dt = OBjtransDetail.GetTransferRequestDetailbyRequestId(StrCompId, StrBrandId, StrLocationId, ReqId.ToString());
        gvProductRequest.DataSource = dt;
        gvProductRequest.DataBind();
        AllPageCode();

    }
    public string ProductName(string ProductId)
    {
        string ProductName = string.Empty;
        DataTable dt = ObjProductMaster.GetProductMasterById(StrCompId.ToString(), ProductId.ToString());
        if (dt.Rows.Count != 0)
        {
            ProductName = dt.Rows[0]["EProductName"].ToString();
        }
        else
        {
            ProductName = "0";
        }
        return ProductName;

    }
    public string UnitName(string UnitId)
    {
        string UnitName = string.Empty;
        DataTable dt = objUnit.GetUnitMasterById(StrCompId.ToString(), UnitId.ToString());
        if (dt.Rows.Count != 0)
        {
            UnitName = dt.Rows[0]["Unit_Name"].ToString();
        }
        return UnitName;
    }
    public void Fillgrid()
    {
        DataTable dt = ObjTrans.GetAllRecord_True(StrCompId.ToString(), StrBrandId.ToString(), StrLocationId.ToString(), "0");

        dt.Columns.Add("Location_Name");


        for (int i = 0; i < dt.Rows.Count; i++)
        {
            DataTable Dtlocation = objLocation.GetLocationMasterById(StrCompId, dt.Rows[i]["RequestLocationID"].ToString());
            dt.Rows[i]["Location_Name"] = Dtlocation.Rows[0]["Location_Name"].ToString();
        }
        gvTransferRequest.DataSource = dt;
        gvTransferRequest.DataBind();

        lblTotalRecords.Text = Resources.Attendance.Total_Records + " : " + dt.Rows.Count;
        Session["DtTransferRequest"] = dt;
        Session["DtFilter"] = dt;
        AllPageCode();

    }
    public void Reset()
    {
        txtTermCondition.Text = "";
        ChkPost.Checked = false;
        txtLocationName.Text = "";
        txtRequestdate.Text = DateTime.Now.ToString(ObjSysParam.GetSysParameterByParamName("Date_Format").Rows[0]["Param_Value"].ToString());
        ResetDetail();
        txtlRequestNo.Text = GetDocumentNumber();
        gvProductRequest.DataSource = null;
        gvProductRequest.DataBind();
        editid.Value = "";
        btnNew.Text = Resources.Attendance.New;
        ViewState["DepartmentApproval"] = null;
        txtlRequestNo.Text = GetDocumentNumber();
    }
    public void DisplayMessage(string str)
    {
        ScriptManager.RegisterStartupScript(this, GetType(), "", "alert('" + str + "');", true);
    }
    #endregion

    #region Bin Section
    protected void btnbinbind_Click(object sender, ImageClickEventArgs e)
    {


        if (ddlbinOption.SelectedIndex != 0)
        {
            string condition = string.Empty;


            if (ddlbinOption.SelectedIndex == 1)
            {
                condition = "convert(" + ddlbinFieldName.SelectedValue + ",System.String)='" + txtbinValue.Text.Trim() + "'";
            }
            else if (ddlOption.SelectedIndex == 2)
            {
                condition = "convert(" + ddlbinFieldName.SelectedValue + ",System.String) like '%" + txtbinValue.Text + "%'";
            }
            else
            {
                condition = "convert(" + ddlbinFieldName.SelectedValue + ",System.String) Like '" + txtbinValue.Text + "%'";
            }
            DataTable dtCust = (DataTable)Session["DtBinTransferRequest"];


            DataView view = new DataView(dtCust, condition, "", DataViewRowState.CurrentRows);
            Session["dtBinFilter"] = view.ToTable();
            lblbinTotalRecords.Text = Resources.Attendance.Total_Records + ": " + view.ToTable().Rows.Count.ToString() + "";

            gvBinTransferRequest.DataSource = view.ToTable();
            gvBinTransferRequest.DataBind();


            if (view.ToTable().Rows.Count == 0)
            {
                imgBtnRestore.Visible = false;
                ImgbtnSelectAll.Visible = false;
            }
            else
            {
                AllPageCode();
            }
            btnbinbind.Focus();

        }

    }
    protected void btnbinRefresh_Click(object sender, ImageClickEventArgs e)
    {
        Fillgrid();
        FillGridBin();
        ddlbinOption.SelectedIndex = 3;
        ddlbinFieldName.SelectedIndex = 0;
        txtbinValue.Text = "";
        lblSelectedRecord.Text = "";
        txtbinValue.Focus();
    }
    protected void gvBinTransferRequest_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        gvBinTransferRequest.PageIndex = e.NewPageIndex;
        if (HDFSortbin.Value == "")
            FillGridBin();
        else
        {
            DataTable dt = (DataTable)Session["dtbinFilter"];
            gvBinTransferRequest.DataSource = dt;
            gvBinTransferRequest.DataBind();
        }
        AllPageCode();

        string temp = string.Empty;
        bool isselcted;

        for (int i = 0; i < gvBinTransferRequest.Rows.Count; i++)
        {
            Label lblconid = (Label)gvBinTransferRequest.Rows[i].FindControl("lblReqId");
            string[] split = lblSelectedRecord.Text.Split(',');

            for (int j = 0; j < lblSelectedRecord.Text.Split(',').Length; j++)
            {
                if (lblSelectedRecord.Text.Split(',')[j] != "")
                {
                    if (lblconid.Text.Trim().ToString() == lblSelectedRecord.Text.Split(',')[j].Trim().ToString())
                    {
                        ((CheckBox)gvBinTransferRequest.Rows[i].FindControl("chkgvSelect")).Checked = true;
                    }
                }
            }
        }

        gvBinTransferRequest.BottomPagerRow.Focus();

    }
    protected void gvBinTransferRequest_Sorting(object sender, GridViewSortEventArgs e)
    {
        lblSelectedRecord.Text = "";
        HDFSortbin.Value = HDFSortbin.Value == "ASC" ? "DESC" : "ASC";
        DataTable dt = new DataTable();
        dt = (DataTable)Session["dtBinFilter"];
        DataView dv = new DataView(dt);
        string Query = "" + e.SortExpression + " " + HDFSortbin.Value + "";
        dv.Sort = Query;
        dt = dv.ToTable();
        Session["dtbinFilter"] = dt;
        gvBinTransferRequest.DataSource = dt;
        gvBinTransferRequest.DataBind();
        AllPageCode();
        gvBinTransferRequest.HeaderRow.Focus();

    }
    protected void chkgvSelectAll_CheckedChanged(object sender, EventArgs e)
    {
        CheckBox chkSelAll = ((CheckBox)gvBinTransferRequest.HeaderRow.FindControl("chkgvSelectAll"));
        for (int i = 0; i < gvBinTransferRequest.Rows.Count; i++)
        {
            ((CheckBox)gvBinTransferRequest.Rows[i].FindControl("chkgvSelect")).Checked = chkSelAll.Checked;
            if (chkSelAll.Checked)
            {
                if (!lblSelectedRecord.Text.Split(',').Contains(((Label)(gvBinTransferRequest.Rows[i].FindControl("lblReqId"))).Text.Trim().ToString()))
                {
                    lblSelectedRecord.Text += ((Label)(gvBinTransferRequest.Rows[i].FindControl("lblReqId"))).Text.Trim().ToString() + ",";
                }
            }
            else
            {
                string temp = string.Empty;
                string[] split = lblSelectedRecord.Text.Split(',');
                foreach (string item in split)
                {
                    if (item != ((Label)(gvBinTransferRequest.Rows[i].FindControl("lblReqId"))).Text.Trim().ToString())
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
        ((CheckBox)gvBinTransferRequest.HeaderRow.FindControl("chkgvSelectAll")).Focus();
    }
    protected void chkgvSelect_CheckedChanged(object sender, EventArgs e)
    {
        string empidlist = string.Empty;
        string temp = string.Empty;
        int index = ((GridViewRow)((CheckBox)sender).Parent.Parent).RowIndex;
        Label lb = (Label)gvBinTransferRequest.Rows[index].FindControl("lblReqId");
        if (((CheckBox)gvBinTransferRequest.Rows[index].FindControl("chkgvSelect")).Checked)
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
        ((CheckBox)gvBinTransferRequest.Rows[index].FindControl("chkgvSelect")).Focus();
    }
    protected void ImgbtnSelectAll_Click(object sender, ImageClickEventArgs e)
    {
        DataTable dtPr = (DataTable)Session["dtBinFilter"];

        if (ViewState["Select"] == null)
        {
            ViewState["Select"] = 1;
            foreach (DataRow dr in dtPr.Rows)
            {
                if (!lblSelectedRecord.Text.Split(',').Contains(dr["Trans_Id"]))
                {
                    lblSelectedRecord.Text += dr["Trans_Id"] + ",";
                }
            }
            for (int i = 0; i < gvBinTransferRequest.Rows.Count; i++)
            {
                string[] split = lblSelectedRecord.Text.Split(',');
                Label lblconid = (Label)gvBinTransferRequest.Rows[i].FindControl("lblReqId");
                for (int j = 0; j < lblSelectedRecord.Text.Split(',').Length; j++)
                {
                    if (lblSelectedRecord.Text.Split(',')[j] != "")
                    {
                        if (lblconid.Text.Trim().ToString() == lblSelectedRecord.Text.Split(',')[j].Trim().ToString())
                        {
                            ((CheckBox)gvBinTransferRequest.Rows[i].FindControl("chkgvSelect")).Checked = true;
                        }
                    }
                }
            }
        }
        else
        {
            lblSelectedRecord.Text = "";
            DataTable dtPr1 = (DataTable)Session["dtBinFilter"];
            gvBinTransferRequest.DataSource = dtPr1;
            gvBinTransferRequest.DataBind();
            AllPageCode();
            ViewState["Select"] = null;
        }

        ImgbtnSelectAll.Focus();

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
                    b = ObjTrans.DeleteTransferRequestHeader(StrCompId.ToString(), StrBrandId.ToString(), StrLocationId.ToString(), lblSelectedRecord.Text.Split(',')[j].Trim(), true.ToString(), UserId.ToString(), DateTime.Now.ToString());

                }
            }
        }

        if (b != 0)
        {

            Fillgrid();
            FillGridBin();
            lblSelectedRecord.Text = "";
            ViewState["Select"] = null;
            DisplayMessage("Record Activated");
        }
        else
        {
            int fleg = 0;
            foreach (GridViewRow Gvr in gvBinTransferRequest.Rows)
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
        txtbinValue.Focus();
    }


    public void FillGridBin()
    {

        DataTable dt = ObjTrans.GetAllRecord_False(StrCompId.ToString(), StrBrandId.ToString(), StrLocationId.ToString(), "0");




        dt.Columns.Add("Location_Name");


        for (int i = 0; i < dt.Rows.Count; i++)
        {
            DataTable Dtlocation = objLocation.GetLocationMasterById(StrCompId, dt.Rows[i]["RequestLocationID"].ToString());
            dt.Rows[i]["Location_Name"] = Dtlocation.Rows[0]["Location_Name"].ToString();
        }
        gvBinTransferRequest.DataSource = dt;
        gvBinTransferRequest.DataBind();

        lblbinTotalRecords.Text = Resources.Attendance.Total_Records + " : " + dt.Rows.Count;
        Session["DtBinTransferRequest"] = dt;
        Session["DtBinFilter"] = dt;
        if (dt.Rows.Count != 0)
        {
            AllPageCode();
        }
        else
        {
            imgBtnRestore.Visible = false;
            ImgbtnSelectAll.Visible = false;
        }
    }
    #endregion

   
       [System.Web.Services.WebMethodAttribute(), System.Web.Script.Services.ScriptMethodAttribute()]
    public static string[] GetCompletionListLocationName(string prefixText, int count, string contextKey)
    {
        LocationMaster objLocationMaster = new LocationMaster();


        DataTable dt = new DataView(objLocationMaster.GetLocationMaster(HttpContext.Current.Session["CompId"].ToString()), "Location_Name like '" + prefixText.ToString() + "%'", "", DataViewRowState.CurrentRows).ToTable();

        dt = new DataView(dt, "Brand_Id='" + HttpContext.Current.Session["BrandId"].ToString() + "'", "", DataViewRowState.CurrentRows).ToTable();
        if (dt.Rows.Count == 0)
        {
            dt=objLocationMaster.GetLocationMaster(HttpContext.Current.Session["CompId"].ToString());
                    dt = new DataView(dt, "Brand_Id='" + HttpContext.Current.Session["BrandId"].ToString() + "'", "", DataViewRowState.CurrentRows).ToTable();

        }
        else
        {
        
        }
            string[] txt = new string[dt.Rows.Count];

            for (int i = 0; i < dt.Rows.Count; i++)
            {
                txt[i] = dt.Rows[i]["Location_Name"].ToString();
            }
        
        return txt;
    }


    protected void txtLocationName_TextChanged(object sender, EventArgs e)
    {
       
        if (txtLocationName.Text != "")
        {
            DataTable Dtlocation = objLocation.GetLocationMasterByLocationName(StrCompId, txtLocationName.Text);
            if (Dtlocation.Rows.Count == 0)
            {
                DisplayMessage("Select Location");
                txtLocationName.Focus();
                txtLocationName.Text = "";
                return;
            }
            else
            {
                ChkPost.Focus();
            }
        }
        else
        {
            
        }

    }
    public string GetPost(string Post)
    {
        string Result = string.Empty;
        if (Post == "Y")
        {
            Result = "Yes";
        }
        else
        {
            Result = "No";
        }
        return Result;
    }
    public string GetStatus(string status)
    {
        string Result = string.Empty;
        if (status.Trim() == "0")
        {
            Result = "Not Open";
        }
       if(status.Trim()=="1")
        {
            Result = "Processing";
        }
       if (status.Trim() == "2")
       {
           Result = "Transfer Out";
       }
        return Result;
    }
    protected void txtRequestdate_TextChanged(object sender, EventArgs e)
    {
        if (txtRequestdate.Text == "")
        {
            txtRequestdate.Text = DateTime.Now.ToString(ObjSysParam.GetSysParameterByParamName("Date_Format").Rows[0]["Param_Value"].ToString());
       
        }
    }
}
