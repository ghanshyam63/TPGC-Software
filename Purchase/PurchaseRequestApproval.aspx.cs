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
using System.Threading;
using System.ComponentModel;

public partial class Purchase_PurchaseRequestApproval : BasePage
{
    PurchaseRequestHeader ObjInvPurchaseRequest = new PurchaseRequestHeader();
    PurchaseRequestDetail ObjPurchaseRequestDetail = new PurchaseRequestDetail();
    SystemParameter ObjSysParam = new SystemParameter();
    Common cmn = new Common();
    Inv_ProductMaster ObjProductMaster = new Inv_ProductMaster();
    Inv_UnitMaster objUnit = new Inv_UnitMaster();
    string StrCompId = string.Empty;
    string StrBrandId = string.Empty;
    string strLocationId = string.Empty;
    string StrUserId = string.Empty;

    protected void Page_Load(object sender, EventArgs e)
    {
        if (Session["UserId"] == null)
        {
            Response.Redirect("ERPLogin.aspx");
        }
        else
        {
            StrUserId = Session["UserId"].ToString();
        }
        StrCompId = Session["CompId"].ToString();
        StrBrandId = "1";
        strLocationId = "1";
        if (!IsPostBack)
        {
            txtDescription.Visible = false;

            FillRequestGrid();

        }
        AllPageCode();
    }


    public void FillRequestGrid()
    {
        DataTable dt = new DataView(ObjInvPurchaseRequest.GetPurchaseRequestHeaderTrueAll(StrCompId, StrBrandId, strLocationId), "DepartmentApproval='False'", "", DataViewRowState.CurrentRows).ToTable();
        gvPurchaseRequest.DataSource = dt;
        gvPurchaseRequest.DataBind();
        lblTotalRecords.Text = Resources.Attendance.Total_Records__0 + dt.Rows.Count;
        Session["DtRecord"] = dt;
        Session["dtFilter"] = dt;
        AllPageCode();
    }
    protected void btnApprove_Command(object sender, CommandEventArgs e)
    {
        int b = 0;
        b = ObjInvPurchaseRequest.UpdatePurchaseRequestHeaderApproval(e.CommandArgument.ToString(), StrCompId.ToString(), StrBrandId.ToString(), strLocationId.ToString(), true.ToString(), false.ToString(), true.ToString(), StrUserId.ToString(), DateTime.Now.ToString());
        if (b != 0)
        {
            FillRequestGrid();
        }
        IbtnBack_Command(null, null);
    }
    protected void IbtnReject_Command(object sender, CommandEventArgs e)
    {

        int b = 0;
        b = ObjInvPurchaseRequest.UpdatePurchaseRequestHeaderApproval(e.CommandArgument.ToString(), StrCompId.ToString(), StrBrandId.ToString(), strLocationId.ToString(), false.ToString(), false.ToString(), false.ToString(), StrUserId.ToString(), DateTime.Now.ToString());
        if (b != 0)
        {
            FillRequestGrid();
        }
        IbtnBack_Command(null, null);
    }
    protected void btnRefresh_Click(object sender, ImageClickEventArgs e)
    {
        txtValue.Text = "";
        ddlFieldName.SelectedIndex = 0;
        ddlOption.SelectedIndex = 3;
        txtValue.Focus();
        FillRequestGrid();
    }
    protected internal void IbtnDetail_Command(object sender, CommandEventArgs e)
    {
        DataTable dt = ObjPurchaseRequestDetail.GetPurchaseRequestDetailbyRequestId(StrCompId, StrBrandId, strLocationId, e.CommandArgument.ToString());
        gvProductRequest.DataSource = dt;
        gvProductRequest.DataBind();
        foreach (GridViewRow Row in gvPurchaseRequest.Rows)
        {
            if (((ImageButton)Row.FindControl("IbtnDetail")).CommandArgument.ToString() != e.CommandArgument.ToString())
            {
                Row.Visible = false;

            }
            else
            {
                ((ImageButton)Row.FindControl("IbtnBack")).Visible = true;
            }

        }
        lblDescription.Text = Resources.Attendance.Description + " : " ;
        txtDescription.Visible = true;
        txtDescription.Text = ObjInvPurchaseRequest.GetPurchaseRequestTrueAllByReqId(StrCompId, StrBrandId, strLocationId, e.CommandArgument.ToString()).Rows[0]["TermCondition"].ToString();
        ((ImageButton)sender).Visible = false;
        gvPurchaseRequest.HeaderRow.Cells[0].Text = Resources.Attendance.Back;
        pnlSearchRecords.Visible = false;

    }
    protected void IbtnBack_Command(object sender, CommandEventArgs e)
    {
        pnlSearchRecords.Visible = true;
        FillRequestGrid();
        gvProductRequest.DataSource = null;
        gvProductRequest.DataBind();
        pnlSearchRecords.Visible = true;
        gvPurchaseRequest.HeaderRow.Cells[0].Text = Resources.Attendance.Detail;
        lblDescription.Text = "";

        txtDescription.Text = "";
        txtDescription.Visible = false;

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

            btnbind.Focus();
            btnRefresh.Focus();

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

    #region AllPageCode
    public void AllPageCode()
    {
        Page.Title = ObjSysParam.GetSysTitle();
        StrUserId = Session["UserId"].ToString();
        StrCompId = Session["CompId"].ToString();
        Session["AccordianId"] = "12";
        Session["HeaderText"] = "Purchase";
        DataTable dtAllPageCode = cmn.GetAllPagePermission(StrUserId.ToString(), "12", "74");
        if (dtAllPageCode.Rows.Count != 0)
        {
            if (dtAllPageCode.Rows[0][0].ToString() == "SuperAdmin")
            {

                foreach (GridViewRow Row in gvPurchaseRequest.Rows)
                {
                    ((ImageButton)Row.FindControl("IbtnApprove")).Visible = true;
                    ((ImageButton)Row.FindControl("IbtnReject")).Visible = true;

                }

            }
            else
            {
                foreach (DataRow DtRow in dtAllPageCode.Rows)
                {
                    if (Convert.ToBoolean(DtRow["Op_Add"].ToString()))
                    {

                    }

                    if (Convert.ToBoolean(DtRow["Op_Edit"].ToString()))
                    {

                    }
                    if (Convert.ToBoolean(DtRow["Op_Delete"].ToString()))
                    {

                    }
                    foreach (GridViewRow Row in gvPurchaseRequest.Rows)
                    {
                        if (Convert.ToBoolean(DtRow["Op_Edit"].ToString()))
                        {


                        }
                        if (Convert.ToBoolean(DtRow["Op_Delete"].ToString()))
                        {
                        }
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
    #endregion

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

}
