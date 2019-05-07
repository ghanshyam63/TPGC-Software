using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;


public partial class Inventory_TransferIn : System.Web.UI.Page
{
    Inv_UnitMaster objUnit = new Inv_UnitMaster();
    Inv_ProductMaster ObjProductMaster = new Inv_ProductMaster();
    Inv_TransferRequestHeader ObjTransReq = new Inv_TransferRequestHeader();
    Inv_TransferRequestDetail ObjTransferReqDetail = new Inv_TransferRequestDetail();
    Inv_TransferHeader ObjTransferHeader = new Inv_TransferHeader();
    Inv_TransferDetail ObjTransferDetail = new Inv_TransferDetail();
    LocationMaster objLocation = new LocationMaster();
    SystemParameter ObjSysParam = new SystemParameter();
    Inv_StockBatchMaster ObjStockBatchMaster = new Inv_StockBatchMaster();
    Inv_ProductLedger ObjProductledger = new Inv_ProductLedger();
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


            txtTransferDate.Text = DateTime.Now.ToString(ObjSysParam.GetSysParameterByParamName("Date_Format").Rows[0]["Param_Value"].ToString());
            FillGrid();
            btnNew.Enabled = false;
            btnList_Click(null, null);

        }
        AllPageCode();

    }


    protected void btnSave_Click(object sender, EventArgs e)
    {
        string Post = string.Empty;
        if (chkPost.Checked == true)
        {
            Post = "Y";
        }
        else
        {
            Post = "N";
        }

        ObjTransferHeader.UpdateTransferHeaderForTransferIn(strCompId, StrBrandId, StrLocId, editid.Value, DateTime.Now.ToString(), Post, Session["UserId"].ToString(), DateTime.Now.ToString());
        DataTable dtTransHeader = ObjTransferHeader.GetTransferHeaderForTransferIn(strCompId, StrBrandId, StrLocId, editid.Value);

        string TransId = editid.Value;
        foreach (GridViewRow Row in gvEditProduct.Rows)
        {

            if (((TextBox)Row.FindControl("txtInQty")).Text.Trim() != "")
            {
                if (((TextBox)Row.FindControl("txtInQty")).Text.Trim() != "0.000")
                {

                    ObjTransferDetail.UpdateTransferDetailForTransferIn(strCompId, StrBrandId, StrLocId, ((Label)Row.FindControl("lblTransId")).Text.Trim(), ((TextBox)Row.FindControl("txtInqty")).Text.Trim(), UserId.ToString(), DateTime.Now.ToString());
                    if (Post == "Y")
                    {
                        ObjTransferReqDetail.UpdateTransferRequestDetailForTransferIn(strCompId, StrBrandId, StrLocId, dtTransHeader.Rows[0]["RequestNo"].ToString(), ((TextBox)Row.FindControl("txtInqty")).Text.Trim(), ((Label)Row.FindControl("lblPId")).Text.Trim(), UserId.ToString(), DateTime.Now.ToString());
                        ObjProductledger.InsertProductLedger(strCompId.ToString(), StrBrandId.ToString(), StrLocId.ToString(), "TI", editid.Value.ToString(), dtTransHeader.Rows[0]["FromLocationId"].ToString(), ((Label)Row.FindControl("lblPId")).Text.Trim(), ((Label)Row.FindControl("lblUnitId")).Text.Trim(), "I", "0", "0", ((TextBox)Row.FindControl("txtInQty")).Text.Trim(), "0", "1/1/1800", "0", "1/1/1800", "0", "1/1/1800", "0","", "", "", "", "", true.ToString(), "1/1/1800", true.ToString(), Session["UserId"].ToString().ToString(), "1/1/1800", Session["UserId"].ToString().ToString(), "1/1/1800");              
                        ObjProductledger.InsertProductLedger(strCompId.ToString(), StrBrandId.ToString(), dtTransHeader.Rows[0]["FromLocationId"].ToString(), "TO", editid.Value.ToString(),StrLocId ,((Label)Row.FindControl("lblPId")).Text.Trim(), ((Label)Row.FindControl("lblUnitId")).Text.Trim(), "O", "0", "0", "0",((Label)Row.FindControl("lbloutqty")).Text.Trim(), "1/1/1800", "0", "1/1/1800", "0", "1/1/1800","0", "", "", "", "","", true.ToString(), "1/1/1800", true.ToString(), Session["UserId"].ToString().ToString(), "1/1/1800", Session["UserId"].ToString().ToString(), "1/1/1800");
                    }
                }
            }
        }
        DisplayMessage("Record Saved");
        btnList_Click(null, null);

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

        FillGrid();
        editid.Value = "";
        gvEditProduct.DataSource = null;
        gvEditProduct.DataBind();
        btnNew.Text = Resources.Attendance.New;
        chkPost.Checked = false;
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
        FillGrid();
        pnlMenuList.BackColor = System.Drawing.ColorTranslator.FromHtml("#ccddee");
        pnlMenuNew.BackColor = System.Drawing.ColorTranslator.FromHtml("#90bde9");
        PnlList.Visible = true;
        PnlNewEdit.Visible = false;
        txtValue.Focus();
    }
    protected void btnNew_Click(object sender, EventArgs e)
    {
        pnlMenuNew.BackColor = System.Drawing.ColorTranslator.FromHtml("#ccddee");

        pnlMenuList.BackColor = System.Drawing.ColorTranslator.FromHtml("#90bde9");
        PnlList.Visible = false;
        PnlNewEdit.Visible = true;

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

    protected void GvTransfer_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {

        DataTable dt = (DataTable)Session["DtTransfer"];
        GvTransfer.DataSource = dt;
        GvTransfer.DataBind();
        AllPageCode();
    }
    public void FillGrid()
    {
        DataTable dt = ObjTransferHeader.GetTransferHeaderForTransferIn(strCompId, StrBrandId, StrLocId);
        string Post = "Y";
        dt = new DataView(dt, "Post<>'" + Post + "'", "", DataViewRowState.CurrentRows).ToTable(); ;
        dt.Columns.Add("Request_No");
        dt.Columns.Add("Request_Date");


        for (int i = 0; i < dt.Rows.Count; i++)
        {
            try
            {
                DataTable dtRequest = ObjTransReq.GetRecordUsingTransId(strCompId, StrBrandId, dt.Rows[i]["ToLocationId"].ToString(), dt.Rows[i]["RequestNo"].ToString());
                dt.Rows[i]["Request_No"] = dtRequest.Rows[0]["RequestNo"].ToString();
                dt.Rows[i]["Request_Date"] = dtRequest.Rows[0]["TDate"].ToString();
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
    protected void btnEdit_Command(object sender, CommandEventArgs e)
    {
        DataTable dTranfer = ObjTransferHeader.GetTransferHeaderForTransferIn(strCompId, StrBrandId, StrLocId);

        dTranfer = new DataView(dTranfer, "Trans_Id=" + e.CommandArgument.ToString() + "", "", DataViewRowState.CurrentRows).ToTable();
        if (dTranfer.Rows.Count != 0)
        {

            editid.Value = e.CommandArgument.ToString();
            txtTransferDate.Text = Convert.ToDateTime(dTranfer.Rows[0]["TDate"].ToString()).ToString(ObjSysParam.GetSysParameterByParamName("Date_Format").Rows[0]["Param_Value"].ToString());
            txtVoucherNo.Text = dTranfer.Rows[0]["VoucherNo"].ToString();


            DataTable dtTranReqHeader = ObjTransReq.GetRecordUsingTransId(strCompId.ToString(), StrBrandId.ToString(), StrLocId.ToString(), dTranfer.Rows[0]["RequestNo"].ToString());
            if (dtTranReqHeader.Rows.Count != 0)
            {
                ViewState["RequestId"] = dTranfer.Rows[0]["RequestNo"].ToString();
                pnlTrans.Visible = true;
                txtTransReqDate.Text = Convert.ToDateTime(dtTranReqHeader.Rows[0]["TDate"].ToString()).ToString(ObjSysParam.GetSysParameterByParamName("Date_Format").Rows[0]["Param_Value"].ToString());
                txtTransNo.Text = dtTranReqHeader.Rows[0]["RequestNo"].ToString();

                try
                {
                    txtLocationName.ReadOnly = true;
                    txtLocationName.Text = objLocation.GetLocationMasterById(strCompId, dTranfer.Rows[0]["FromLocationId"].ToString()).Rows[0]["Location_Name"].ToString();
                }
                catch
                {
                    txtLocationName.Text = "";
                }

                DataTable dtTrasDetail = ObjTransferDetail.GetTransferDetailbyTransferId(strCompId.ToString(), StrBrandId.ToString(), dTranfer.Rows[0]["FromLocationId"].ToString(), e.CommandArgument.ToString());
                if (dtTrasDetail.Rows.Count > 0)
                {

                    gvEditProduct.DataSource = dtTrasDetail;
                    gvEditProduct.DataBind();
                    btnNew_Click(null, null);
                    btnNew.Text = Resources.Attendance.Edit;
                }


            }

        }
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
                    //    ((ImageButton)Row.FindControl("IbtnDelete")).Visible = true;
                    //}
                }
                //imgBtnRestore.Visible = true;
                //ImgbtnSelectAll.Visible = true;
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
                        //if (Convert.ToBoolean(DtRow["Op_Delete"].ToString()))
                        //{
                        //    ((ImageButton)Row.FindControl("IbtnDelete")).Visible = true;
                        //}
                    }

                    //if (Convert.ToBoolean(DtRow["Op_Restore"].ToString()))
                    //{
                    //    imgBtnRestore.Visible = true;
                    //    ImgbtnSelectAll.Visible = true;
                    //}
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
        if (txt.Text == "")
        {
            txt.Text = "0.000";
        }
        if (lb.Text == "")
        {
            lb.Text = "0.000";
        }
        if (float.Parse(txt.Text.ToString()) > float.Parse(lb.Text.ToString()))
        {
            DisplayMessage("Out quantity  should not be greater than request quantity");
            txt.Focus();
        }

    }

}
