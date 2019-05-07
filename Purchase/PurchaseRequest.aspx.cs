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

public partial class Purchase_PurchaseRequest : BasePage
{
    #region Class Object
    PurchaseRequestDetail ObjPurchaseRequestDetail = new PurchaseRequestDetail();
    PurchaseRequestHeader ObjPurchaseReqestHeader = new PurchaseRequestHeader();
    Inv_UnitMaster objUnit = new Inv_UnitMaster();
    Inv_ProductMaster ObjProductMaster = new Inv_ProductMaster();
    SystemParameter ObjSysParam = new SystemParameter();
    UserMaster ObjUser = new UserMaster();
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
            txtlRequestNo.Text = GetDocumentNumber();
            btnList_Click(null, null);
            FillUnit();
            txtRequestdate.Text = DateTime.Now.ToString(ObjSysParam.GetSysParameterByParamName("Date_Format").Rows[0]["Param_Value"].ToString());
            txtExpDelDate.Text = DateTime.Now.ToString(ObjSysParam.GetSysParameterByParamName("Date_Format").Rows[0]["Param_Value"].ToString());
            Fillgrid();
            FillGridBin();
            Reset();
            btnReset_Click(null, null);
            txtValue.Focus();
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
        txtValue.Text = "";
    }
    protected void btnNew_Click(object sender, EventArgs e)
    {
        pnlMenuNew.BackColor = System.Drawing.ColorTranslator.FromHtml("#ccddee");

        pnlMenuList.BackColor = System.Drawing.ColorTranslator.FromHtml("#90bde9");
        pnlMenuBin.BackColor = System.Drawing.ColorTranslator.FromHtml("#90bde9");

        PnlList.Visible = false;
        PnlNewEdit.Visible = true;
        PnlBin.Visible = false;
        txtRequestdate.Focus();
     

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
        txtbinValue.Text = "";
    }

    protected void btnSave_Click(object sender, EventArgs e)
    {
           
        if (txtRequestdate.Text == "")
        {
            ViewState["Return"] = 1;
            DisplayMessage("Enter Request Date");
            txtRequestdate.Text = DateTime.Now.ToString(ObjSysParam.GetSysParameterByParamName("Date_Format").Rows[0]["Param_Value"].ToString());
     
            txtRequestdate.Focus();
            return;
        }


        if (txtlRequestNo.Text == "")
        {
            ViewState["Return"] = 1;
            DisplayMessage("Enter Request No.");
            txtlRequestNo.Focus();
            return;
        }
        if (txtExpDelDate.Text == "")
        {
            ViewState["Return"] = 1;
            DisplayMessage("Enter Expected Delievery Date");
            txtExpDelDate.Focus();
            txtExpDelDate.Text = DateTime.Now.ToString(ObjSysParam.GetSysParameterByParamName("Date_Format").Rows[0]["Param_Value"].ToString());
       
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
            DisplayMessage("Enter Term & Conditions");

            txtTermCondition.Focus();
            return;
        }


        int b = 0;
        if (editid.Value == "")
        {

            b = ObjPurchaseReqestHeader.InsertPurchaseRequestHeader(StrCompId.ToString(), StrBrandId.ToString(), StrLocationId.ToString(), txtlRequestNo.Text, txtRequestdate.Text, txtTermCondition.Text, "Padding", txtExpDelDate.Text, false.ToString(), false.ToString(), "", "", "", "", "", true.ToString(), DateTime.Now.ToString(), true.ToString(), UserId.ToString(), DateTime.Now.ToString(), UserId.ToString(), DateTime.Now.ToString());

            if (b != 0)
            {
                DisplayMessage("Record Saved");

            }
        }
        else
        {
            b = ObjPurchaseReqestHeader.UpdatePurchaseRequestHeader(editid.Value, StrCompId.ToString(), StrBrandId.ToString(), StrLocationId.ToString(), txtlRequestNo.Text, txtRequestdate.Text, txtTermCondition.Text, "Padding", txtExpDelDate.Text, Convert.ToBoolean(ViewState["DepartmentApproval"].ToString()).ToString(), false.ToString(), "", "", "", "", "", true.ToString(), DateTime.Now.ToString(), true.ToString(), UserId.ToString(), DateTime.Now.ToString());

            if (b != 0)
            {
                DisplayMessage("Record Update");
                btnList_Click(null, null);
            }

        }
        ViewState["RequestNo"] = txtlRequestNo.Text;
       
     
      
        Fillgrid();
        Reset();
        txtRequestdate.Focus();
        txtValue.Focus();
    }

    protected void btnReset_Click(object sender, EventArgs e)
    {
        if (editid.Value == "")
        {
            string Id = ObjPurchaseReqestHeader.getAutoId();
            ObjPurchaseRequestDetail.DeletePurchaseRequestDetailBYReqID(StrCompId.ToString(), StrBrandId.ToString(), StrLocationId.ToString(), Id.ToString());
        }
        Reset();
        txtlRequestNo.Focus();
    }
    protected void btnCancel_Click(object sender, EventArgs e)
    {
        if (editid.Value == "")
        {
            string Id = ObjPurchaseReqestHeader.getAutoId();
            ObjPurchaseRequestDetail.DeletePurchaseRequestDetailBYReqID(StrCompId.ToString(), StrBrandId.ToString(), StrLocationId.ToString(), Id.ToString());
        }

        Reset();
        btnList_Click(null, null);

    }

    protected void btnEdit_Command(object sender, CommandEventArgs e)
    {
        DataTable dt = ObjPurchaseReqestHeader.GetPurchaseRequestTrueAllByReqId(StrCompId.ToString(), StrBrandId.ToString(), StrLocationId.ToString(), e.CommandArgument.ToString());
        if (dt.Rows.Count != 0)
        {
            editid.Value = e.CommandArgument.ToString();
            txtlRequestNo.Text = dt.Rows[0]["RequestNo"].ToString();

            txtRequestdate.Text = Convert.ToDateTime(dt.Rows[0]["RequestDate"].ToString()).ToString(ObjSysParam.GetSysParameterByParamName("Date_Format").Rows[0]["Param_Value"].ToString());
            txtExpDelDate.Text = Convert.ToDateTime(dt.Rows[0]["ExpDelDate"].ToString()).ToString(ObjSysParam.GetSysParameterByParamName("Date_Format").Rows[0]["Param_Value"].ToString());
            ViewState["DepartmentApproval"] = dt.Rows[0]["DepartmentApproval"].ToString();
            txtTermCondition.Text = dt.Rows[0]["TermCondition"].ToString();
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
        ObjPurchaseReqestHeader.DeletePurchaseRequestHeader(StrCompId.ToString(), StrBrandId.ToString(), StrLocationId.ToString(), e.CommandArgument.ToString(), false.ToString(), UserId.ToString(), DateTime.Now.ToString());
        Fillgrid();
        FillGridBin();
        DisplayMessage("Record Deleted");
        try
        {
            int i = ((GridViewRow)((ImageButton)sender).Parent.Parent).RowIndex;
            ((ImageButton)gvPurchaseRequest.Rows[i].FindControl("IbtnDelete")).Focus();
        }
        catch
        {
            txtValue.Focus();
        }
 

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
            DataTable dtCust = (DataTable)Session["DtPurchaseRequest"];


            DataView view = new DataView(dtCust, condition, "", DataViewRowState.CurrentRows);
            Session["dtFilter"] = view.ToTable();
            lblTotalRecords.Text = Resources.Attendance.Total_Records + ": " + view.ToTable().Rows.Count.ToString() + "";

            gvPurchaseRequest.DataSource = view.ToTable();
            gvPurchaseRequest.DataBind();
            AllPageCode();

            btnRefresh.Focus();

        }

    }



    protected void btnClosePanel_Click(object sender, ImageClickEventArgs e)
    {
        pnlProduct1.Visible = false;
        pnlProduct2.Visible = false;
        txtTermCondition.Focus();
    }
    protected void btnProductSave_Click(object sender, EventArgs e)
    {
        string PDiscription = string.Empty;
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
        if (txtRequestQty.Text == "")
        {
            txtRequestQty.Text = "1";
        }
        string ReqId = string.Empty;
        string ProductId = string.Empty;
        string UnitId = string.Empty;
        if (editid.Value == "")
        {
            ReqId = ObjPurchaseReqestHeader.getAutoId();
        }
        else
        {
            ReqId = editid.Value.ToString();
        }
        int serialNo = 0;
        if (hidProduct.Value == "" || hidProduct.Value=="0")
        {
             DataTable dtProduct = ObjPurchaseRequestDetail.GetPurchaseRequestDetailbyRequestId(StrCompId, StrBrandId, StrLocationId, ReqId.Trim());
             if (dtProduct.Rows.Count > 0)
             {
                 dtProduct = new DataView(dtProduct, "", "Serial_No Desc", DataViewRowState.CurrentRows).ToTable();
                 serialNo =Convert.ToInt32(dtProduct.Rows[0]["Serial_No"].ToString());
                 serialNo += 1;
             }
             else
             {
                 serialNo = 1;
             }
            
           

        }
        else
        {
          serialNo =Convert.ToInt32(ViewState["SNO"].ToString());
        }
        if (txtProductName.Text != "")
        {
            DataTable dt = new DataView(ObjProductMaster.GetProductMasterAll(StrCompId.ToString()), "EProductName='" + txtProductName.Text.ToString() + "'", "", DataViewRowState.CurrentRows).ToTable();
            if (dt.Rows.Count != 0)
            {
                PDiscription = txtPDescription.Text;

                ProductId = dt.Rows[0]["ProductId"].ToString();
            }
            else
            {
                PDiscription = txtPDesc.Text;

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
        if (hidProduct.Value == "" || hidProduct.Value=="0")
        {
          
               
                

                ObjPurchaseRequestDetail.InsertPurchaseRequestDetail(StrCompId.ToString(), StrBrandId.ToString(), StrLocationId.ToString(), ReqId.ToString(), serialNo.ToString(), ProductId.ToString(), PDiscription, UnitId.ToString(), txtRequestQty.Text.ToString(), "", "", "", "", "", true.ToString(), DateTime.Now.ToString(), true.ToString(), UserId.ToString(), DateTime.Now.ToString(), UserId.ToString(), DateTime.Now.ToString());
            
        }
        else
        {
            
            ObjPurchaseRequestDetail.UpdatePurchaseRequestDetail(StrCompId.ToString(), StrBrandId.ToString(), StrLocationId.ToString(), ReqId.ToString(), hidProduct.Value.ToString(), serialNo.ToString(), ProductId.ToString(), PDiscription, UnitId.ToString(), txtRequestQty.Text, "", "", "", "", "", true.ToString(), DateTime.Now.ToString(), true.ToString(), UserId.ToString(), DateTime.Now.ToString());
          
            pnlProduct1.Visible = false;
            pnlProduct2.Visible = false;
        }
        
        fillgridDetail();
        ResetDetail();
        txtProductName.Focus();


    }


    protected void btnEdit_Command1(object sender, CommandEventArgs e)
    {
        pnlProduct1.Visible = true;
        pnlProduct2.Visible = true;
        hidProduct.Value = e.CommandArgument.ToString();
        DataTable dt = ObjPurchaseRequestDetail.GetPurchaseRequestDetail(StrCompId.ToString(), StrBrandId.ToString(), StrLocationId.ToString(), "0", e.CommandArgument.ToString());
        if (dt.Rows.Count != 0)
        {
            if (dt.Rows[0]["Product_Id"].ToString() != "0")
            {
                txtProductName.Text = ProductName(dt.Rows[0]["Product_Id"].ToString());
                pnlPDescription.Visible = true;
                txtPDesc.Visible = false;
                txtPDescription.Text = dt.Rows[0]["ProductDescription"].ToString();

            }
            else
            {
                txtProductName.Text = dt.Rows[0]["ProductDescription"].ToString();
                txtPDesc.Visible = true;
                txtPDesc.Text = dt.Rows[0]["ProductDescription"].ToString();
                pnlPDescription.Visible = false;
            }
            ddlUnit.SelectedValue = dt.Rows[0]["UnitId"].ToString();
         //   txtPDescription.Text = dt.Rows[0]["ProductDescription"].ToString();
            txtRequestQty.Text = dt.Rows[0]["ReqQty"].ToString();
            ViewState["SNO"] = dt.Rows[0]["Serial_No"].ToString();
            txtProductName.Focus();
        }
    }
    protected void IbtnDelete_Command1(object sender, CommandEventArgs e)
    {
        ObjPurchaseRequestDetail.DeletePurchaseRequestDetail(StrCompId.ToString(), StrBrandId.ToString(), StrLocationId.ToString(), e.CommandArgument.ToString(), "False", UserId.ToString(), DateTime.Now.ToString());
        fillgridDetail();

    }
    protected void btnProductCancel_Click(object sender, EventArgs e)
    {
        btnClosePanel_Click(null, null);
        ResetDetail();
        txtTermCondition.Focus();

    }
    protected void txtlRequestNo_TextChanged(object sender, EventArgs e)
    {
        if (txtlRequestNo.Text != "")
        {
            DataTable dt = new DataView(ObjPurchaseReqestHeader.GetPurchaseRequestHeaderTrueAll(StrCompId.ToString(), StrBrandId.ToString(), StrLocationId.ToString()), "RequestNo='" + txtlRequestNo.Text.Trim() + "'", "", DataViewRowState.CurrentRows).ToTable();
            if (dt.Rows.Count != 0)
            {
                if (Convert.ToBoolean(dt.Rows[0]["IsActive"].ToString()))
                {
                    DisplayMessage("Request No Already Exists");

                }
                else
                {

                    DisplayMessage("Request No Already Exists :- Go To Bin Tab");

                }


                txtlRequestNo.Text = "";
                txtlRequestNo.Focus();
            }
            else
            {
                btnAddProduct.Focus();
            }
        }
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
                string ReqId = ObjPurchaseReqestHeader.getAutoId();
                DataTable dtProduct = ObjPurchaseRequestDetail.GetPurchaseRequestDetailbyRequestId(StrCompId, StrBrandId, StrLocationId, ReqId.Trim());
                if (dtProduct.Rows.Count > 0)
                {
                    dtProduct = new DataView(dtProduct, "Product_Id=" + dt.Rows[0]["ProductId"].ToString() + "", "", DataViewRowState.CurrentRows).ToTable();
                    if (dtProduct.Rows.Count > 0)
                    {
                        DisplayMessage("Product Is already exists!");
                        txtProductName.Text = "";
                        txtProductName.Focus();
                        return;

                    }
                }
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
              
                txtPDesc.Visible = false;
                pnlPDescription.Visible = true;
          
            }
            else
            {
                FillUnit();

                txtPDescription.Text = "";
              
                txtPDesc.Visible = true;
                pnlPDescription.Visible = false;

            }
            ddlUnit.Focus();
        }
        else
        {
            DisplayMessage("Enter Product Name");
            System.Web.UI.ScriptManager.GetCurrent(this).SetFocus(txtProductName);
        }



    }
    protected void gvPurchaseRequest_Sorting(object sender, GridViewSortEventArgs e)
    {
        HDFSort.Value = HDFSort.Value == "ASC" ? "DESC" : "ASC";
        DataTable dt = new DataTable();
        dt = (DataTable)Session["dtFilter"];
        DataView dv = new DataView(dt);
        string Query = "" + e.SortExpression + " " + HDFSort.Value + "";
        dv.Sort = Query;
        dt = dv.ToTable();
        Session["dtFilter"] = dt;
        gvPurchaseRequest.DataSource = dt;
        gvPurchaseRequest.DataBind();
        AllPageCode();
        gvPurchaseRequest.HeaderRow.Focus();

    }
    protected void gvPurchaseRequest_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        gvPurchaseRequest.PageIndex = e.NewPageIndex;

        DataTable dt = (DataTable)Session["dtFilter"];
        gvPurchaseRequest.DataSource = dt;
        gvPurchaseRequest.DataBind();
        AllPageCode();
    }
    #endregion
    public string GetDocumentNumber()
    {
        string DocumentNo = string.Empty;

        DataTable Dt = objDocNo.GetDocumentNumberAll(StrCompId, "12", "44");
       
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
                DocumentNo += "-" + (Convert.ToInt32(ObjPurchaseReqestHeader.GetPurchaseRequestHeader(StrCompId.ToString()).Rows.Count) + 1).ToString();
            }
            else
            {
                DocumentNo += (Convert.ToInt32(ObjPurchaseReqestHeader.GetPurchaseRequestHeader(StrCompId.ToString()).Rows.Count) + 1).ToString();

            }
        }
        else
        {
            DocumentNo += (Convert.ToInt32(ObjPurchaseReqestHeader.GetPurchaseRequestHeader(StrCompId.ToString()).Rows.Count) + 1).ToString();
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
        Session["AccordianId"] = "12";
        Session["HeaderText"] = "Purchase";
        DataTable dtAllPageCode = cmn.GetAllPagePermission(UserId.ToString(), "12", "44");
        if (dtAllPageCode.Rows.Count != 0)
        {
            if (dtAllPageCode.Rows[0][0].ToString() == "SuperAdmin")
            {
                btnSave.Visible = true;
                btnProductSave.Visible = true;
                foreach (GridViewRow Row in gvPurchaseRequest.Rows)
                {
                    ((ImageButton)Row.FindControl("IbtnPrint")).Visible = true;
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
                    foreach (GridViewRow Row in gvPurchaseRequest.Rows)
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
        txtPDescription.Text = "";
        ddlUnit.SelectedIndex = 0;
        txtRequestQty.Text = "1";
        hidProduct.Value = "";
        txtPDesc.Text = "";
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
        string ReqId = ObjPurchaseReqestHeader.getAutoId();
        if (editid.Value == "")
        {
            ReqId = ObjPurchaseReqestHeader.getAutoId();
        }
        else
        {
            ReqId = editid.Value.ToString();
        }
        DataTable dt = ObjPurchaseRequestDetail.GetPurchaseRequestDetailbyRequestId(StrCompId, StrBrandId, StrLocationId, ReqId.ToString());
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
        DataTable dt = ObjPurchaseReqestHeader.GetPurchaseRequestHeaderTrueAll(StrCompId.ToString(), StrBrandId.ToString(), StrLocationId.ToString());
        gvPurchaseRequest.DataSource = dt;
        gvPurchaseRequest.DataBind();

        lblTotalRecords.Text = Resources.Attendance.Total_Records + " : " + dt.Rows.Count;
        Session["DtPurchaseRequest"] = dt;
        Session["DtFilter"] = dt;
        AllPageCode();

    }
    public void Reset()
    {
        txtTermCondition.Text = "";
        txtExpDelDate.Text = DateTime.Now.ToString(ObjSysParam.GetSysParameterByParamName("Date_Format").Rows[0]["Param_Value"].ToString());
        txtRequestdate.Text = DateTime.Now.ToString(ObjSysParam.GetSysParameterByParamName("Date_Format").Rows[0]["Param_Value"].ToString());
        ResetDetail();
        txtlRequestNo.Text = GetDocumentNumber();
        gvProductRequest.DataSource = null;
        gvProductRequest.DataBind();
        editid.Value = "";
        btnNew.Text = Resources.Attendance.New;
        ViewState["DepartmentApproval"] = null;
        txtRequestdate.Focus();
        txtValue.Focus();
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
            DataTable dtCust = (DataTable)Session["DtBinPurchaseRequest"];


            DataView view = new DataView(dtCust, condition, "", DataViewRowState.CurrentRows);
            Session["dtBinFilter"] = view.ToTable();
            lblbinTotalRecords.Text = Resources.Attendance.Total_Records + ": " + view.ToTable().Rows.Count.ToString() + "";

            gvBinPurchaseRequest.DataSource = view.ToTable();
            gvBinPurchaseRequest.DataBind();


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
            btnbinRefresh.Focus();

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
    protected void gvBinPurchaseRequest_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        gvBinPurchaseRequest.PageIndex = e.NewPageIndex;
        if (HDFSortbin.Value == "")
            FillGridBin();
        else
        {
            DataTable dt = (DataTable)Session["dtbinFilter"];
            gvBinPurchaseRequest.DataSource = dt;
            gvBinPurchaseRequest.DataBind();
        }
        AllPageCode();

        string temp = string.Empty;
        bool isselcted;

        for (int i = 0; i < gvBinPurchaseRequest.Rows.Count; i++)
        {
            Label lblconid = (Label)gvBinPurchaseRequest.Rows[i].FindControl("lblReqId");
            string[] split = lblSelectedRecord.Text.Split(',');

            for (int j = 0; j < lblSelectedRecord.Text.Split(',').Length; j++)
            {
                if (lblSelectedRecord.Text.Split(',')[j] != "")
                {
                    if (lblconid.Text.Trim().ToString() == lblSelectedRecord.Text.Split(',')[j].Trim().ToString())
                    {
                        ((CheckBox)gvBinPurchaseRequest.Rows[i].FindControl("chkgvSelect")).Checked = true;
                    }
                }
            }
        }

        gvBinPurchaseRequest.BottomPagerRow.Focus();

    }
    protected void gvBinPurchaseRequest_Sorting(object sender, GridViewSortEventArgs e)
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
        gvBinPurchaseRequest.DataSource = dt;
        gvBinPurchaseRequest.DataBind();
        AllPageCode();
        gvBinPurchaseRequest.HeaderRow.Focus();

    }
    protected void chkgvSelectAll_CheckedChanged(object sender, EventArgs e)
    {
        CheckBox chkSelAll = ((CheckBox)gvBinPurchaseRequest.HeaderRow.FindControl("chkgvSelectAll"));
        for (int i = 0; i < gvBinPurchaseRequest.Rows.Count; i++)
        {
            ((CheckBox)gvBinPurchaseRequest.Rows[i].FindControl("chkgvSelect")).Checked = chkSelAll.Checked;
            if (chkSelAll.Checked)
            {
                if (!lblSelectedRecord.Text.Split(',').Contains(((Label)(gvBinPurchaseRequest.Rows[i].FindControl("lblReqId"))).Text.Trim().ToString()))
                {
                    lblSelectedRecord.Text += ((Label)(gvBinPurchaseRequest.Rows[i].FindControl("lblReqId"))).Text.Trim().ToString() + ",";
                }
            }
            else
            {
                string temp = string.Empty;
                string[] split = lblSelectedRecord.Text.Split(',');
                foreach (string item in split)
                {
                    if (item != ((Label)(gvBinPurchaseRequest.Rows[i].FindControl("lblReqId"))).Text.Trim().ToString())
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
        ((CheckBox)gvBinPurchaseRequest.HeaderRow.FindControl("chkgvSelectAll")).Focus();
    }
    protected void chkgvSelect_CheckedChanged(object sender, EventArgs e)
    {
        string empidlist = string.Empty;
        string temp = string.Empty;
        int index = ((GridViewRow)((CheckBox)sender).Parent.Parent).RowIndex;
        Label lb = (Label)gvBinPurchaseRequest.Rows[index].FindControl("lblReqId");
        if (((CheckBox)gvBinPurchaseRequest.Rows[index].FindControl("chkgvSelect")).Checked)
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
        ((CheckBox)gvBinPurchaseRequest.Rows[index].FindControl("chkgvSelect")).Focus();
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
            for (int i = 0; i < gvBinPurchaseRequest.Rows.Count; i++)
            {
                string[] split = lblSelectedRecord.Text.Split(',');
                Label lblconid = (Label)gvBinPurchaseRequest.Rows[i].FindControl("lblReqId");
                for (int j = 0; j < lblSelectedRecord.Text.Split(',').Length; j++)
                {
                    if (lblSelectedRecord.Text.Split(',')[j] != "")
                    {
                        if (lblconid.Text.Trim().ToString() == lblSelectedRecord.Text.Split(',')[j].Trim().ToString())
                        {
                            ((CheckBox)gvBinPurchaseRequest.Rows[i].FindControl("chkgvSelect")).Checked = true;
                        }
                    }
                }
            }
        }
        else
        {
            lblSelectedRecord.Text = "";
            DataTable dtPr1 = (DataTable)Session["dtBinFilter"];
            gvBinPurchaseRequest.DataSource = dtPr1;
            gvBinPurchaseRequest.DataBind();
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
                    b = ObjPurchaseReqestHeader.DeletePurchaseRequestHeader(StrCompId.ToString(), StrBrandId.ToString(), StrLocationId.ToString(), lblSelectedRecord.Text.Split(',')[j].Trim(), true.ToString(), UserId.ToString(), DateTime.Now.ToString());

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
            foreach (GridViewRow Gvr in gvBinPurchaseRequest.Rows)
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

        DataTable dt = ObjPurchaseReqestHeader.GetPurchaseRequestHeaderFalseAll(StrCompId.ToString(), StrBrandId.ToString(), StrLocationId.ToString());
        gvBinPurchaseRequest.DataSource = dt;
        gvBinPurchaseRequest.DataBind();

        lblbinTotalRecords.Text = Resources.Attendance.Total_Records + " : " + dt.Rows.Count;
        Session["DtBinPurchaseRequest"] = dt;
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

    protected void IbtnPrint_Command(object sender, CommandEventArgs e)
    {
        ScriptManager.RegisterStartupScript(Page, typeof(Page), "New", "window.open('../Purchase/PurchaseRequestPrint.aspx?RId=" + e.CommandArgument.ToString() + "');", true);

    }

    protected void btnSavePrint_Click(object sender, EventArgs e)
    {

        btnSave_Click(null, null);
        if (ViewState["Return"] == null)
        {
            txtlRequestNo.Text = ViewState["RequestNo"].ToString();
            ScriptManager.RegisterStartupScript(Page, typeof(Page), "New", "window.open('../Purchase/PurchaseRequestPrint.aspx?RId=" + txtlRequestNo.Text.Trim() + "');", true);
            Reset();
            ViewState["RequestNo"] = null;
        }
        ViewState["Return"] = null;
       
    }
   
}