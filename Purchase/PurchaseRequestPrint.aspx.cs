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

public partial class Purchase_PurchaseRequestPrint : BasePage
{
    PurchaseRequestHeader InvPr = new PurchaseRequestHeader();
    PurchaseRequestDetail InvPrDetails = new PurchaseRequestDetail();
    SystemParameter ObjSysPeram = new SystemParameter();
    Inv_ProductMaster ObjProductMaster = new Inv_ProductMaster();
    Inv_UnitMaster objUnit = new Inv_UnitMaster();
    CompanyMaster ObjCompanyMaster = new CompanyMaster();
    Set_AddressChild objAddChild = new Set_AddressChild();
    Set_AddressMaster ObjAddMaster = new Set_AddressMaster();
    string strCompId = string.Empty;
    string strBrandId = string.Empty;
    string strLocationId = string.Empty;

    protected void Page_Load(object sender, EventArgs e)
    {

        strCompId = Session["CompId"].ToString();
        strBrandId = "1";
        strLocationId = "1";
        if (Request.QueryString["RId"] != null)
        {
            try
            {
                string Id = new DataView(InvPr.GetPurchaseRequestHeaderTrueAll(Session["CompId"].ToString(), strBrandId.ToString(), strLocationId.ToString()), "RequestNo='" + Request.QueryString["RId"].ToString() + "'", "", DataViewRowState.CurrentRows).ToTable().Rows[0]["Trans_Id"].ToString();
                fillRequest(Id.ToString());
            }
            catch
            {

            }

        }
    }

    public void fillRequest(string ReqId)
    {
        DataTable dt = InvPr.GetPurchaseRequestTrueAllByReqId(strCompId.ToString(), strBrandId.ToString(), strLocationId.ToString(), ReqId.Trim());
        if (dt.Rows.Count != 0)
        {
            txtRequestNo.Text = dt.Rows[0]["RequestNo"].ToString();
            txtRequestdate.Text = dt.Rows[0]["RequestDate"].ToString();
            txtRemark.Text = dt.Rows[0]["TermCondition"].ToString();
            DataTable dtComp = ObjCompanyMaster.GetCompanyMasterById(Session["CompId"].ToString());
            lblCompanyName.Text = dtComp.Rows[0]["Company_Name"].ToString();
            string RefName = objAddChild.GetAddressChildDataByAddTypeAndAddRefId("Company", Session["CompId"].ToString()).Rows[0]["Address_Name"].ToString();
            DataTable dtAdd = ObjAddMaster.GetAddressDataByAddressName(Session["CompId"].ToString(), RefName);
            lblAddress.Text = dtAdd.Rows[0]["Address"].ToString();
            lblPhone.Text = dtAdd.Rows[0]["PhoneNo1"].ToString();
            fillgridDetail(ReqId);

        }

    }

    public void fillgridDetail(string ReqId)
    {
        DataTable dt = InvPrDetails.GetPurchaseRequestDetailbyRequestId(strCompId.ToString(), strBrandId.ToString(), strLocationId.ToString(), ReqId.ToString());
        gvProductRequestDetails.DataSource = dt;
        gvProductRequestDetails.DataBind();


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
}
