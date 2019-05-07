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

public partial class Inventory_Transfer : BasePage
{
    Inv_UnitMaster objUnit = new Inv_UnitMaster();
    Inv_ProductMaster ObjProductMaster = new Inv_ProductMaster();
    Inv_TransferRequestHeader ObjTransReq = new Inv_TransferRequestHeader();
    Inv_TransferRequestDetail ObjTransferReqDetail = new Inv_TransferRequestDetail();
    Inv_TransferHeader ObjTransferHeader = new Inv_TransferHeader();
    Inv_TransferDetail ObjTransferDetail = new Inv_TransferDetail();
    LocationMaster objLocation = new LocationMaster();
    SystemParameter ObjSysParam = new SystemParameter();
    Common cmn = new Common();
    string strCompId = string.Empty;
    string StrBrandId = string.Empty;
    string StrLocId = string.Empty;
    string UserId = string.Empty;
    protected void Page_Load(object sender, EventArgs e)
    {

        if (Session["UserId"] == null)
        {
            Response.Redirect("~/ERPLogin.aspx");
        }
        StrBrandId = Session["BrandId"].ToString();
        StrLocId = Session["LocId"].ToString();
        strCompId = Session["CompId"].ToString();
        if (!IsPostBack)
        {

            FillTransferRequestgrid();
            txtTransferDate.Text = DateTime.Now.ToString(ObjSysParam.GetSysParameterByParamName("Date_Format").Rows[0]["Param_Value"].ToString());
            FillGrid();
            FillGridBin();
            btnList_Click(null, null);

        }
        AllPageCode();

    }


    protected void btnSave_Click(object sender, EventArgs e)
    {

        if (txtTransferDate.Text == "")
        {
            DisplayMessage("Select Date");
            txtTransferDate.Text = DateTime.Now.ToString(ObjSysParam.GetSysParameterByParamName("Date_Format").Rows[0]["Param_Value"].ToString());

            txtTransferDate.Focus();
            return;
        }
        if (txtVoucherNo.Text == "")
        {
            DisplayMessage("Enter Voucher No.");
            txtVoucherNo.Focus();
            return;
        }
        string LocationId = string.Empty;
        DataTable Dtlocation = objLocation.GetLocationMasterByLocationName(strCompId, txtLocationName.Text);
        if (Dtlocation.Rows.Count != 0)
        {
            LocationId = Dtlocation.Rows[0]["Location_Id"].ToString();
        }
        else
        {
            LocationId = "0";
        }




        bool b = false;

        string StrReqId = ViewState["RequestId"].ToString();

        if (editid.Value == "")
        {
            foreach (GridViewRow Row in GvProduct.Rows)
            {

                if (((TextBox)Row.FindControl("txtOutQty")).Text.Trim() != "")
                {

                    if (((TextBox)Row.FindControl("txtOutQty")).Text.Trim() != "0")
                    {
                        b = true;

                    }
                }

            }
            if (!b)
            {

                DisplayMessage("Enter Product Quantity");
                GvProduct.Focus();
                return;

            }


            ObjTransferHeader.InsertTransferHeader(strCompId, StrBrandId, StrLocId, txtTransferDate.Text.Trim(), StrReqId.Trim(), LocationId.Trim(), txtDescription.Text.Trim(), "N", DateTime.Now.ToString(), null, txtVoucherNo.Text.Trim(), "", "", "", "", "", true.ToString(), DateTime.Now.ToString(), true.ToString(), UserId.ToString(), DateTime.Now.ToString(), UserId.ToString(), DateTime.Now.ToString());

            string TransId = ObjTransferHeader.getAutoId();
            foreach (GridViewRow Row in GvProduct.Rows)
            {

                if (((TextBox)Row.FindControl("txtOutQty")).Text.Trim() != "")
                {
                    if (((TextBox)Row.FindControl("txtOutQty")).Text.Trim() != "0")
                    {

                        ObjTransferDetail.InsertTransferDetail(strCompId, StrBrandId, StrLocId, TransId, ((Label)Row.FindControl("lblSerialNO")).Text.Trim(), ((Label)Row.FindControl("lblPId")).Text.Trim(), ((Label)Row.FindControl("lblunitcost")).Text.Trim(), ((Label)Row.FindControl("lblUnitId")).Text.Trim(), ((Label)Row.FindControl("lblReqQty")).Text.Trim(), ((TextBox)Row.FindControl("txtOutQty")).Text.Trim(), "0", "", "", "", "", "", true.ToString(), DateTime.Now.ToString(), true.ToString(), UserId.ToString(), DateTime.Now.ToString(), UserId.ToString(), DateTime.Now.ToString());
                    }
                }
            }
            DisplayMessage("Record Saved");
            int insertintransReq = 0;
            insertintransReq = ObjTransReq.UpdateStatusInTransferRequestHeader(StrReqId, strCompId, StrBrandId, LocationId.Trim(), UserId.ToString(), DateTime.Now.ToString(), "2");

        }
        else
        {
            foreach (GridViewRow Row in gvEditProduct.Rows)
            {

                if (((TextBox)Row.FindControl("txtOutQty")).Text.Trim() != "")
                {

                    if (((TextBox)Row.FindControl("txtOutQty")).Text.Trim() != "0")
                    {
                        b = true;

                    }
                }

            }
            if (!b)
            {

                DisplayMessage("Enter Product Quantity");
                GvProduct.Focus();
                return;

            }
            ObjTransferHeader.UpdateTransferHeader(editid.Value, strCompId, StrBrandId, StrLocId, txtTransferDate.Text.Trim(), StrReqId.Trim(), LocationId.Trim(), txtDescription.Text.Trim(), "N", DateTime.Now.ToString(), null, txtVoucherNo.Text.Trim(), "", "", "", "", "", true.ToString(), DateTime.Now.ToString(), true.ToString(), UserId.ToString(), DateTime.Now.ToString(), UserId.ToString(), DateTime.Now.ToString());
            ObjTransferDetail.DeleteTransferDetailbyHeaderTransId(strCompId, StrBrandId, StrLocId, editid.Value);
            string TransId = editid.Value;
            foreach (GridViewRow Row in gvEditProduct.Rows)
            {

                if (((TextBox)Row.FindControl("txtOutQty")).Text.Trim() != "")
                {
                    if (((TextBox)Row.FindControl("txtOutQty")).Text.Trim() != "0")
                    {

                        ObjTransferDetail.InsertTransferDetail(strCompId, StrBrandId, StrLocId, TransId, ((Label)Row.FindControl("lblSerialNO")).Text.Trim(), ((Label)Row.FindControl("lblPId")).Text.Trim(), ((Label)Row.FindControl("lblunitcost")).Text.Trim(), ((Label)Row.FindControl("lblUnitId")).Text.Trim(), ((Label)Row.FindControl("lblReqQty")).Text.Trim(), ((TextBox)Row.FindControl("txtOutQty")).Text.Trim(), "0", "", "", "", "", "", true.ToString(), DateTime.Now.ToString(), true.ToString(), UserId.ToString(), DateTime.Now.ToString(), UserId.ToString(), DateTime.Now.ToString());
                    }
                }
            }
            DisplayMessage("Record Update");

        }


        Reset();
        ViewState["RequestId"] = null;
    }
    public void Reset()
    {
        txtVoucherNo.Text = "";
        editid.Value = "";
        txtTransferDate.Text = DateTime.Now.ToString(ObjSysParam.GetSysParameterByParamName("Date_Format").Rows[0]["Param_Value"].ToString());
        txtTransNo.Text = "";
        txtTransReqDate.Text = "";
        txtLocationName.Text = "";
        pnlTrans.Visible = false;
        txtDescription.Text = "";
        GvProduct.DataSource = null;
        GvProduct.DataBind();
        FillGrid();
        editid.Value = "";
        gvEditProduct.DataSource = null;
        gvEditProduct.DataBind();
        btnNew.Text = Resources.Attendance.New;

        AllPageCode();
    }
    public void DisplayMessage(string str)
    {
        ScriptManager.RegisterStartupScript(this, GetType(), "", "alert('" + str + "');", true);
    }
    protected void btnCancel_Click(object sender, EventArgs e)
    {
        Reset();
        btnList_Click(null, null);
    }
    protected void BtnReset_Click(object sender, EventArgs e)
    {
        Reset();
    }
    protected void btnRefreshReport_Click(object sender, ImageClickEventArgs e)
    {
        txtValue.Text = "";
        ddlFieldName.SelectedIndex = 0;
        ddlOption.SelectedIndex = 2;
        txtValue.Focus();
        FillGrid();
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
            DataTable dtCust = (DataTable)Session["DtTransfer"];


            DataView view = new DataView(dtCust, condition, "", DataViewRowState.CurrentRows);
            Session["dtFilter"] = view.ToTable();
            lblTotalRecords.Text = Resources.Attendance.Total_Records + ": " + view.ToTable().Rows.Count.ToString() + "";

            GvTransfer.DataSource = view.ToTable();
            GvTransfer.DataBind();
            AllPageCode();

            btnbind.Focus();

        }
    }
    protected void btnList_Click(object sender, EventArgs e)
    {
        pnlMenuList.BackColor = System.Drawing.ColorTranslator.FromHtml("#ccddee");
        pnlMenuRequest.BackColor = System.Drawing.ColorTranslator.FromHtml("#90bde9");
        pnlMenuNew.BackColor = System.Drawing.ColorTranslator.FromHtml("#90bde9");
        pnlMenuBin.BackColor = System.Drawing.ColorTranslator.FromHtml("#90bde9");
        PnlTransferRequest.Visible = false;
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
        pnlMenuRequest.BackColor = System.Drawing.ColorTranslator.FromHtml("#90bde9");
        PnlList.Visible = false;
        PnlNewEdit.Visible = true;
        PnlBin.Visible = false;
        PnlTransferRequest.Visible = false;

    }
    protected void btnBin_Click(object sender, EventArgs e)
    {
        pnlMenuBin.BackColor = System.Drawing.ColorTranslator.FromHtml("#ccddee");

        pnlMenuNew.BackColor = System.Drawing.ColorTranslator.FromHtml("#90bde9");
        pnlMenuList.BackColor = System.Drawing.ColorTranslator.FromHtml("#90bde9");
        pnlMenuRequest.BackColor = System.Drawing.ColorTranslator.FromHtml("#90bde9");
        PnlTransferRequest.Visible = false;
        PnlNewEdit.Visible = false;
        PnlBin.Visible = true;
        PnlList.Visible = false;
        FillGridBin();
        txtValueBin.Focus();
    }
    protected void btnTransRequest_Click(object sender, EventArgs e)
    {
        pnlMenuBin.BackColor = System.Drawing.ColorTranslator.FromHtml("#90bde9");

        pnlMenuNew.BackColor = System.Drawing.ColorTranslator.FromHtml("#90bde9");
        pnlMenuList.BackColor = System.Drawing.ColorTranslator.FromHtml("#90bde9");
        pnlMenuRequest.BackColor = System.Drawing.ColorTranslator.FromHtml("#ccddee");
        PnlNewEdit.Visible = false;
        PnlBin.Visible = false;
        PnlList.Visible = false;
        PnlTransferRequest.Visible = true;


    }


    public string UnitName(string UnitId)
    {
        string UnitName = string.Empty;
        DataTable dt = objUnit.GetUnitMasterById(strCompId.ToString(), UnitId.ToString());
        if (dt.Rows.Count != 0)
        {
            UnitName = dt.Rows[0]["Unit_Name"].ToString();
        }
        return UnitName;
    }
    public string ProductName(string ProductId)
    {
        string ProductName = string.Empty;
        DataTable dt = ObjProductMaster.GetProductMasterById(strCompId.ToString(), ProductId.ToString());
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
    public void FillTransferRequestgrid()
    {
        DataTable dt = new DataView(ObjTransReq.GetAllRecord_TrueByRequestLocation(strCompId.ToString(), StrBrandId.ToString(), StrLocId.ToString(), "0"), "Post='Y' and RequestLocationId='" + StrLocId.ToString() + "'", "", DataViewRowState.CurrentRows).ToTable();
        dt.Columns.Add("Location_Name");


        for (int i = 0; i < dt.Rows.Count; i++)
        {
            DataTable Dtlocation = objLocation.GetLocationMasterById(strCompId, dt.Rows[i]["Location_ID"].ToString());
            dt.Rows[i]["Location_Name"] = Dtlocation.Rows[0]["Location_Name"].ToString();
        }
        gvTransferRequest.DataSource = dt;
        gvTransferRequest.DataBind();

        Session["DtTransferRequestFilter"] = dt;
        AllPageCode();

    }
    protected void btnTransferRequest_Command(object sender, CommandEventArgs e)
    {
        DataTable dtTranReqHeader = ObjTransReq.GetRecordUsingTransIdByRequestLocation(strCompId.ToString(), StrBrandId.ToString(), StrLocId.ToString(), e.CommandArgument.ToString());
        if (dtTranReqHeader.Rows.Count != 0)
        {
            ViewState["RequestId"] = e.CommandArgument.ToString();
            pnlTrans.Visible = true;
            txtTransReqDate.Text = Convert.ToDateTime(dtTranReqHeader.Rows[0]["TDate"].ToString()).ToString(ObjSysParam.GetSysParameterByParamName("Date_Format").Rows[0]["Param_Value"].ToString());
            txtTransNo.Text = dtTranReqHeader.Rows[0]["RequestNo"].ToString();
            try
            {
                txtLocationName.ReadOnly = true;
                txtLocationName.Text = objLocation.GetLocationMasterById(strCompId, dtTranReqHeader.Rows[0]["Location_ID"].ToString()).Rows[0]["Location_Name"].ToString();
            }
            catch
            {
                txtLocationName.Text = "";
            }
            DataTable dtTrasDetail = ObjTransferReqDetail.GetTransferRequestDetailbyRequestId(strCompId.ToString(), StrBrandId.ToString(), dtTranReqHeader.Rows[0]["Location_ID"].ToString(), e.CommandArgument.ToString());
            GvProduct.DataSource = dtTrasDetail;
            GvProduct.DataBind();
            gvEditProduct.DataSource = null;
            gvEditProduct.DataBind();
            btnNew_Click(null, null);

        }



    }
    protected void gvTransferRequest_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        gvTransferRequest.PageIndex = e.NewPageIndex;

        DataTable dt = (DataTable)Session["DtTransferRequestFilter"];
        gvTransferRequest.DataSource = dt;
        gvTransferRequest.DataBind();
        AllPageCode();
    }

    protected void GvTransfer_Sorting(object sender, GridViewSortEventArgs e)
    {
        hdnSort.Value = hdnSort.Value == "ASC" ? "DESC" : "ASC";
        DataTable dt = new DataTable();
        dt = (DataTable)Session["dtTransfer"];
        DataView dv = new DataView(dt);
        string Query = "" + e.SortExpression + " " + hdnSort.Value + "";
        dv.Sort = Query;
        dt = dv.ToTable();
        Session["dtFilter"] = dt;
        GvTransfer.DataSource = dt;
        GvTransfer.DataBind();
        AllPageCode();
        GvTransfer.HeaderRow.Focus();
    }

    protected void gvTransferRequest_Sorting(object sender, GridViewSortEventArgs e)
    {
        hdnTransferRequest.Value = hdnTransferRequest.Value == "ASC" ? "DESC" : "ASC";
        DataTable dt = new DataTable();
        dt = (DataTable)Session["DtTransferRequestFilter"];
        DataView dv = new DataView(dt);
        string Query = "" + e.SortExpression + " " + hdnTransferRequest.Value + "";
        dv.Sort = Query;
        dt = dv.ToTable();
        Session["dtFilter"] = dt;
        gvTransferRequest.DataSource = dt;
        gvTransferRequest.DataBind();
        AllPageCode();
        gvTransferRequest.HeaderRow.Focus();
    }
    protected void GvTransfer_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        gvTransferRequest.PageIndex = e.NewPageIndex;

        DataTable dt = (DataTable)Session["DtTransfer"];
        GvTransfer.DataSource = dt;
        GvTransfer.DataBind();
        AllPageCode();
    }
    public void FillGrid()
    {
        DataTable dt = ObjTransferHeader.GetTransferHeader(strCompId, StrBrandId, StrLocId);

        dt.Columns.Add("Location_Name");


        for (int i = 0; i < dt.Rows.Count; i++)
        {
            try
            {
                DataTable Dtlocation = objLocation.GetLocationMasterById(strCompId, dt.Rows[i]["ToLocationID"].ToString());
                dt.Rows[i]["Location_Name"] = Dtlocation.Rows[0]["Location_Name"].ToString();
            }
            catch
            {

            }
        }
        GvTransfer.DataSource = dt;
        GvTransfer.DataBind();
        lblTotalRecords.Text = Resources.Attendance.Total_Records + " : " + dt.Rows.Count;
        Session["dtTransfer"] = dt;
        Session["dtFilter"] = dt;
        AllPageCode();
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
    protected void btnEdit_Command(object sender, CommandEventArgs e)
    {
        DataTable dTranfer = ObjTransferHeader.GetTransferHeader(strCompId, StrBrandId, StrLocId, e.CommandArgument.ToString());
        if (dTranfer.Rows.Count != 0)
        {
            editid.Value = e.CommandArgument.ToString();
            txtTransferDate.Text = Convert.ToDateTime(dTranfer.Rows[0]["TDate"].ToString()).ToString(ObjSysParam.GetSysParameterByParamName("Date_Format").Rows[0]["Param_Value"].ToString());
            txtVoucherNo.Text = dTranfer.Rows[0]["VoucherNo"].ToString();
            txtDescription.Text = dTranfer.Rows[0]["Remark"].ToString();


            DataTable dtTranReqHeader = ObjTransReq.GetRecordUsingTransIdByRequestLocation(strCompId.ToString(), StrBrandId.ToString(), StrLocId.ToString(), dTranfer.Rows[0]["RequestNo"].ToString());
            if (dtTranReqHeader.Rows.Count != 0)
            {
                ViewState["RequestId"] = dTranfer.Rows[0]["RequestNo"].ToString();
                pnlTrans.Visible = true;
                txtTransReqDate.Text = Convert.ToDateTime(dtTranReqHeader.Rows[0]["TDate"].ToString()).ToString(ObjSysParam.GetSysParameterByParamName("Date_Format").Rows[0]["Param_Value"].ToString());
                txtTransNo.Text = dtTranReqHeader.Rows[0]["RequestNo"].ToString();

                try
                {
                    txtLocationName.ReadOnly = true;
                    txtLocationName.Text = objLocation.GetLocationMasterById(strCompId, dtTranReqHeader.Rows[0]["Location_ID"].ToString()).Rows[0]["Location_Name"].ToString();
                }
                catch
                {
                    txtLocationName.Text = "";
                }

                DataTable dtTrasDetail = ObjTransferDetail.GetTransferDetailbyTransferId(strCompId.ToString(), StrBrandId.ToString(), StrLocId.ToString(), e.CommandArgument.ToString());
                gvEditProduct.DataSource = dtTrasDetail;
                gvEditProduct.DataBind();
                GvProduct.DataSource = null;
                GvProduct.DataBind();
                btnNew_Click(null, null);
                btnNew.Text = Resources.Attendance.Edit;


            }

        }
    }
    protected void IbtnDelete_Command(object sender, CommandEventArgs e)
    {

        ObjTransferHeader.DeleteTransferHeader(strCompId, StrBrandId, StrLocId, e.CommandArgument.ToString(), false.ToString(), UserId.ToString(), DateTime.Now.ToString());
        FillGrid();
        FillGridBin();
        DisplayMessage("Record Deleted");
    }

    #region AllPageCode
    public void AllPageCode()
    {
        Page.Title = ObjSysParam.GetSysTitle();

        StrBrandId = Session["BrandId"].ToString();
        StrLocId = Session["LocId"].ToString();

        UserId = Session["UserId"].ToString();
        strCompId = Session["CompId"].ToString();
        Session["AccordianId"] = "11";
        Session["HeaderText"] = "Inventory";
        DataTable dtAllPageCode = cmn.GetAllPagePermission(UserId.ToString(), "11", "94");
        if (dtAllPageCode.Rows.Count != 0)
        {
            if (dtAllPageCode.Rows[0][0].ToString() == "SuperAdmin")
            {
                btnSave.Visible = true;

                foreach (GridViewRow Row in GvTransfer.Rows)
                {
                    //((ImageButton)Row.FindControl("IbtnPrint")).Visible = true;
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
                    foreach (GridViewRow Row in GvTransfer.Rows)
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
    #endregion


    #region Bin Section
    protected void btnbindBin_Click(object sender, ImageClickEventArgs e)
    {


        if (ddlOptionBin.SelectedIndex != 0)
        {
            string condition = string.Empty;


            if (ddlOptionBin.SelectedIndex == 1)
            {
                condition = "convert(" + ddlFieldNameBin.SelectedValue + ",System.String)='" + txtValueBin.Text.Trim() + "'";
            }
            else if (ddlOption.SelectedIndex == 2)
            {
                condition = "convert(" + ddlFieldNameBin.SelectedValue + ",System.String) like '%" + txtValueBin.Text + "%'";
            }
            else
            {
                condition = "convert(" + ddlFieldNameBin.SelectedValue + ",System.String) Like '" + txtValueBin.Text + "%'";
            }
            DataTable dtTrans = (DataTable)Session["DtBinTransfer"];


            DataView view = new DataView(dtTrans, condition, "", DataViewRowState.CurrentRows);
            Session["dtBinFilter"] = view.ToTable();
            lblTotalRecordsBin.Text = Resources.Attendance.Total_Records + ": " + view.ToTable().Rows.Count.ToString() + "";

            gvTransferBin.DataSource = view.ToTable();
            gvTransferBin.DataBind();


            if (view.ToTable().Rows.Count == 0)
            {
                imgBtnRestore.Visible = false;
                ImgbtnSelectAll.Visible = false;
            }
            else
            {
                AllPageCode();
            }
            btnbindBin.Focus();

        }

    }
    protected void btnRefreshBin_Click(object sender, ImageClickEventArgs e)
    {
        FillGrid();
        FillGridBin();
        ddlOptionBin.SelectedIndex = 2;
        ddlFieldNameBin.SelectedIndex = 0;
        txtValueBin.Text = "";
        lblSelectedRecord.Text = "";
        txtValueBin.Focus();
    }
    protected void gvTransferBin_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        gvTransferBin.PageIndex = e.NewPageIndex;
        if (HDFSortbin.Value == "")
            FillGridBin();
        else
        {
            DataTable dt = (DataTable)Session["dtbinFilter"];
            gvTransferBin.DataSource = dt;
            gvTransferBin.DataBind();
        }
        AllPageCode();

        string temp = string.Empty;
        bool isselcted;

        for (int i = 0; i < gvTransferBin.Rows.Count; i++)
        {
            Label lblconid = (Label)gvTransferBin.Rows[i].FindControl("lblTransId");
            string[] split = lblSelectedRecord.Text.Split(',');

            for (int j = 0; j < lblSelectedRecord.Text.Split(',').Length; j++)
            {
                if (lblSelectedRecord.Text.Split(',')[j] != "")
                {
                    if (lblconid.Text.Trim().ToString() == lblSelectedRecord.Text.Split(',')[j].Trim().ToString())
                    {
                        ((CheckBox)gvTransferBin.Rows[i].FindControl("chkgvSelect")).Checked = true;
                    }
                }
            }
        }

        gvTransferBin.BottomPagerRow.Focus();

    }
    protected void gvTransferBin_Sorting(object sender, GridViewSortEventArgs e)
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
        gvTransferBin.DataSource = dt;
        gvTransferBin.DataBind();
        AllPageCode();
        gvTransferBin.HeaderRow.Focus();

    }
    protected void chkgvSelectAll_CheckedChanged(object sender, EventArgs e)
    {
        CheckBox chkSelAll = ((CheckBox)gvTransferBin.HeaderRow.FindControl("chkgvSelectAll"));
        for (int i = 0; i < gvTransferBin.Rows.Count; i++)
        {
            ((CheckBox)gvTransferBin.Rows[i].FindControl("chkgvSelect")).Checked = chkSelAll.Checked;
            if (chkSelAll.Checked)
            {
                if (!lblSelectedRecord.Text.Split(',').Contains(((Label)(gvTransferBin.Rows[i].FindControl("lblTransId"))).Text.Trim().ToString()))
                {
                    lblSelectedRecord.Text += ((Label)(gvTransferBin.Rows[i].FindControl("lblTransId"))).Text.Trim().ToString() + ",";
                }
            }
            else
            {
                string temp = string.Empty;
                string[] split = lblSelectedRecord.Text.Split(',');
                foreach (string item in split)
                {
                    if (item != ((Label)(gvTransferBin.Rows[i].FindControl("lblTransId"))).Text.Trim().ToString())
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
        ((CheckBox)gvTransferBin.HeaderRow.FindControl("chkgvSelectAll")).Focus();
    }
    protected void chkgvSelect_CheckedChanged(object sender, EventArgs e)
    {
        string empidlist = string.Empty;
        string temp = string.Empty;
        int index = ((GridViewRow)((CheckBox)sender).Parent.Parent).RowIndex;
        Label lb = (Label)gvTransferBin.Rows[index].FindControl("lblTransId");
        if (((CheckBox)gvTransferBin.Rows[index].FindControl("chkgvSelect")).Checked)
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
        ((CheckBox)gvTransferBin.Rows[index].FindControl("chkgvSelect")).Focus();
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
            for (int i = 0; i < gvTransferBin.Rows.Count; i++)
            {
                string[] split = lblSelectedRecord.Text.Split(',');
                Label lblconid = (Label)gvTransferBin.Rows[i].FindControl("lblTransId");
                for (int j = 0; j < lblSelectedRecord.Text.Split(',').Length; j++)
                {
                    if (lblSelectedRecord.Text.Split(',')[j] != "")
                    {
                        if (lblconid.Text.Trim().ToString() == lblSelectedRecord.Text.Split(',')[j].Trim().ToString())
                        {
                            ((CheckBox)gvTransferBin.Rows[i].FindControl("chkgvSelect")).Checked = true;
                        }
                    }
                }
            }
        }
        else
        {
            lblSelectedRecord.Text = "";
            DataTable dtPr1 = (DataTable)Session["dtBinFilter"];
            gvTransferBin.DataSource = dtPr1;
            gvTransferBin.DataBind();
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
                    b = ObjTransferHeader.DeleteTransferHeader(strCompId.ToString(), StrBrandId.ToString(), StrLocId.ToString(), lblSelectedRecord.Text.Split(',')[j].Trim(), true.ToString(), UserId.ToString(), DateTime.Now.ToString());

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
            foreach (GridViewRow Gvr in gvTransferBin.Rows)
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
        txtValueBin.Focus();
    }


    public void FillGridBin()
    {

        DataTable dt = ObjTransferHeader.GetTransferHeaderFalse(strCompId.ToString(), StrBrandId.ToString(), StrLocId.ToString());




        dt.Columns.Add("Location_Name");


        for (int i = 0; i < dt.Rows.Count; i++)
        {
            try
            {
                DataTable Dtlocation = objLocation.GetLocationMasterById(strCompId, dt.Rows[i]["ToLocationID"].ToString());
                dt.Rows[i]["Location_Name"] = Dtlocation.Rows[0]["Location_Name"].ToString();
            }
            catch
            {

            }
        }
        gvTransferBin.DataSource = dt;
        gvTransferBin.DataBind();

        lblTotalRecordsBin.Text = Resources.Attendance.Total_Records + " : " + dt.Rows.Count;
        Session["DtBinTransfer"] = dt;
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
    protected void txtTransferDate_TextChanged(object sender, EventArgs e)
    {
        if (txtTransferDate.Text == "")
        {
            txtTransferDate.Text = DateTime.Now.ToString(ObjSysParam.GetSysParameterByParamName("Date_Format").Rows[0]["Param_Value"].ToString());
            txtVoucherNo.Focus();
        }
    }

    protected void txtOutQty_TextChanged(object sender, EventArgs e)
    {
        TextBox txt = (TextBox)sender;
        GridViewRow row = (GridViewRow)((TextBox)sender).Parent.Parent;
        Label lb = (Label)row.FindControl("lblReqQty");
        if (float.Parse(txt.Text.ToString()) > float.Parse(lb.Text.ToString()))
        {
            DisplayMessage("Out quantity  should not be greater than request quantity");
            txt.Focus();
        }
        
    }

}

