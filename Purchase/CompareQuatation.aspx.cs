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

public partial class Purchase_CompareQuatation : BasePage
{
    Inv_PurchaseQuoteHeader objQuoteHeader = new Inv_PurchaseQuoteHeader();
    Inv_PurchaseQuoteDetail ObjQuoteDetail = new Inv_PurchaseQuoteDetail();
    Inv_ProductMaster ObjProductMaster = new Inv_ProductMaster();
    Ems_ContactMaster objContact = new Ems_ContactMaster();
    string StrCompId = string.Empty;
    string StrBrandId = string.Empty;
    string StrLocationId = string.Empty;
    protected void Page_Load(object sender, EventArgs e)
    {
        StrCompId = Session["CompId"].ToString();
        StrBrandId = "1";
        StrLocationId = "1";
        fillDataList(Request.QueryString["RPQId"].ToString());

    }

    public void fillDataList(string RPQNo)
    {

        string Id = objQuoteHeader.GetQuoteHeaderAllDataByRPQ_No(StrCompId, StrBrandId, StrLocationId, RPQNo.Trim()).Rows[0]["Trans_Id"].ToString();
        DataTable dtPQDetail = GetRecord(ObjQuoteDetail.GetQuoteDetailByRPQ_No(StrCompId.ToString(), StrBrandId.ToString(), StrLocationId.ToString(), Id.ToString()));
        if (dtPQDetail.Rows.Count != 0)
        {
            datalistProduct.DataSource = dtPQDetail;
            datalistProduct.DataBind();
        }

    }
    public DataTable GetRecord(DataTable dt)
    {
        DataTable dtreturn = new DataTable();

        dtreturn.Columns.Add("Product_Id");
        dtreturn.Columns.Add("Product_Description");
        dtreturn.Rows.Add();
        for (int i = 0; i < dt.Rows.Count; i++)
        {
            if (dt.Rows[i]["Product_Id"].ToString() != "0")
            {
                DataTable dtTemp = new DataView(dtreturn, "Product_Id='" + dt.Rows[i]["Product_Id"].ToString() + "'", "", DataViewRowState.CurrentRows).ToTable();
                if (dtTemp.Rows.Count == 0)
                {
                    if (dtreturn.Rows.Count == 1)
                    {
                        dtreturn.Rows[0]["Product_Id"] = dt.Rows[i]["Product_Id"].ToString();
                        dtreturn.Rows[0]["Product_Description"] = dt.Rows[i]["ProductDescription"].ToString();
                    }
                    else
                    {

                        dtreturn.Rows[dtreturn.Rows.Count - 1]["Product_Id"] = dt.Rows[i]["Product_Id"].ToString();
                        dtreturn.Rows[dtreturn.Rows.Count - 1]["Product_Description"] = dt.Rows[i]["ProductDescription"].ToString();
                    }
                    dtreturn.Rows.Add();
                }

            }
            else
            {
                DataTable dtTemp = new DataView(dtreturn, "Product_Id='" + dt.Rows[i]["Product_Id"].ToString() + "' and Product_Description='" + dt.Rows[i]["ProductDescription"].ToString() + "'", "", DataViewRowState.CurrentRows).ToTable();
                if (dtTemp.Rows.Count == 0)
                {
                    if (dtreturn.Rows.Count == 1)
                    {
                        dtreturn.Rows[0]["Product_Id"] = dt.Rows[i]["Product_Id"].ToString();
                        dtreturn.Rows[0]["Product_Description"] = dt.Rows[i]["ProductDescription"].ToString();
                    }
                    else
                    {
                        dtreturn.Rows[dtreturn.Rows.Count - 1]["Product_Id"] = dt.Rows[i]["Product_Id"].ToString();
                        dtreturn.Rows[dtreturn.Rows.Count - 1]["Product_Description"] = dt.Rows[i]["ProductDescription"].ToString();
                    }
                    dtreturn.Rows.Add();
                }
            }

        }
        dtreturn.Rows.RemoveAt(dtreturn.Rows.Count - 1);

        return dtreturn;
    }
    public string ProductName(string ProductId, string Description)
    {
        string ProductName = string.Empty;
        DataTable dt = ObjProductMaster.GetProductMasterById(StrCompId.ToString(), ProductId.ToString());
        if (dt.Rows.Count != 0)
        {
            ProductName = dt.Rows[0]["EProductName"].ToString();
        }
        else
        {
            ProductName = Description.ToString();
        }
        return ProductName;

    }
    protected void datalistProduct_DataBinding(object sender, EventArgs e)
    {

        //

    }
    protected void datalistProduct_ItemDataBound(object sender, DataListItemEventArgs e)
    {
        Label l1 = (Label)e.Item.FindControl("lblProductId");
        GridView gvSup = (GridView)e.Item.FindControl("gvSupplier");
        string Id = objQuoteHeader.GetQuoteHeaderAllDataByRPQ_No(StrCompId, StrBrandId, StrLocationId, Request.QueryString["RPQId"].ToString()).Rows[0]["Trans_Id"].ToString();
        DataTable dtPQDetail = new DataTable();
        if (l1.Text.Trim() != "0")
        {
            dtPQDetail = new DataView(ObjQuoteDetail.GetQuoteDetailByRPQ_No(StrCompId.ToString(), StrBrandId.ToString(), StrLocationId.ToString(), Id.ToString()), "Product_Id='" + l1.Text.Trim() + "'", "", DataViewRowState.CurrentRows).ToTable();
        }
        else
        {
            Label lNm = (Label)e.Item.FindControl("txtProductName");
            dtPQDetail = new DataView(ObjQuoteDetail.GetQuoteDetailByRPQ_No(StrCompId.ToString(), StrBrandId.ToString(), StrLocationId.ToString(), Id.ToString()), "Product_Id='" + l1.Text.Trim() + "' and ProductDescription='" + lNm.Text.Trim() + "'", "", DataViewRowState.CurrentRows).ToTable();
        }
        gvSup.DataSource = dtPQDetail;
        gvSup.DataBind();


    }
    protected string GetSupplierName(string strSupplierId)
    {
        string strSupplierName = string.Empty;
        if (strSupplierId != "0" && strSupplierId != "")
        {
            DataTable dtSName = objContact.GetContactByContactId(StrCompId, strSupplierId);
            if (dtSName.Rows.Count > 0)
            {
                strSupplierName = dtSName.Rows[0]["Contact_Name"].ToString();
            }
        }
        else
        {
            strSupplierName = "";
        }
        return strSupplierName;
    }
}
