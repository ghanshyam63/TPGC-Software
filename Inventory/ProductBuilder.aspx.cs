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

public partial class Inventory_ProductBuilder : System.Web.UI.Page
{
    #region Defind Class Object
    BillOfMaterial ObjInvBOM = new BillOfMaterial();
    Inv_ProductMaster ObjInvProductMaster = new Inv_ProductMaster();
    Inv_OptionCategoryMaster ObjOpCate = new Inv_OptionCategoryMaster();
    SystemParameter ObjSysParam = new SystemParameter();
    string StrCompId = string.Empty;
    string StrUserId = string.Empty;

    #endregion
    protected void Page_Load(object sender, EventArgs e)
    {
        if (Session["UserId"] == null)
        {
            Response.Redirect("~/ERPLogin.aspx");
        }
        StrCompId = Session["CompId"].ToString();
        StrUserId = Session["UserId"].ToString();
        Session["AccordianId"] = "11";
        Session["HeaderText"] = "Inventory";
        if (!IsPostBack)
        {

            FillProductGrid();
            btnList_Click(null, null);


        }
        Page.Title = ObjSysParam.GetSysTitle();
       
    }


    #region System Defind Funcation:-Event
    protected void btnList_Click(object sender, EventArgs e)
    {
        pnlMenuList.BackColor = System.Drawing.ColorTranslator.FromHtml("#ccddee");

        pnlMenuNew.BackColor = System.Drawing.ColorTranslator.FromHtml("#90bde9");


        pnlList.Visible = true;

        pnlNew.Visible = false;
        txtValue.Focus();
    }
    protected void btnNew_Click(object sender, EventArgs e)
    {
        pnlMenuNew.BackColor = System.Drawing.ColorTranslator.FromHtml("#ccddee");

        pnlMenuList.BackColor = System.Drawing.ColorTranslator.FromHtml("#90bde9");

        pnlNew.Visible = true;

        pnlList.Visible = false;
        txtOptionPartNo.Focus();
    }

    protected void btnEdit_Command(object sender, CommandEventArgs e)
    {

        GridViewRow Row = (GridViewRow)((ImageButton)sender).Parent.Parent; ;

        txtProductId.Text = ((Label)Row.FindControl("lblProductName")).Text.ToString();
        txtModelNo.Text = ((Label)Row.FindControl("lblModelNo")).Text.ToString();
        txtProductPartNo.Text = ((Label)Row.FindControl("lblModelNo")).Text.ToString();
        txtProductId_TextChanged(null, null);
        btnNew_Click(null, null);
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
                condition = "convert(" + ddlFieldName.SelectedValue + ",System.String) Like '" + txtValue.Text.Trim() + "%'";
            }
            else
            {
                condition = "convert(" + ddlFieldName.SelectedValue + ",System.String) like '%" + txtValue.Text.Trim() + "%'";
            }
            DataView view = new DataView((DataTable)Session["DtProduct"], condition, "", DataViewRowState.CurrentRows);

            lblTotalRecord.Text = Resources.Attendance.Total_Records + " : " + (view.ToTable()).Rows.Count.ToString() + "";

            GridProduct.DataSource = view.ToTable();
            GridProduct.DataBind();
            Session["DtFilter"] = view.ToTable();

            btnbind.Focus();
        }

    }

    protected void btnRefresh_Click(object sender, ImageClickEventArgs e)
    {

        ddlFieldName.SelectedIndex = 1;
        ddlOption.SelectedIndex = 3;
        txtValue.Text = "";
        FillProductGrid();

        txtValue.Focus(); ;
    }

    protected void GridProduct_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        GridProduct.DataSource = (DataTable)Session["DtFilter"];
        GridProduct.DataBind();
        GridProduct.Focus();

    }

    protected void txtProductId_TextChanged(object sender, EventArgs e)
    {
        if (txtProductId.Text != "")
        {
            try
            {
                DataTable dt = new DataView(ObjInvProductMaster.GetProductMasterTrueAll(StrCompId.ToString()), "EProductName='" + txtProductId.Text.ToString().Trim() + "'", "", DataViewRowState.CurrentRows).ToTable();
                if (dt.Rows.Count != 0)
                {
                    txtModelNo.Text = dt.Rows[0]["ModelNo"].ToString();
                    txtProductPartNo.Text = dt.Rows[0]["ModelNo"].ToString();
                    txtOptionPartNo.Focus();
                    ViewState["ProductId"] = dt.Rows[0]["ProductId"].ToString();
                    fillOptionCategorygrid();

                }
                else
                {
                    txtProductId.Text = "";
                    DisplayMessage("Select Product Name");
                    txtProductId.Focus();

                }
            }
            catch
            {

            }
        }


    }


    protected void gvOptionCategory_DataBound(object sender, EventArgs e)
    {
        foreach (GridViewRow GvOptrow in gvOptionCategory.Rows)
        {
            try
            {
                string ProductId = ViewState["ProductId"].ToString();
                RadioButtonList RdoList = (RadioButtonList)GvOptrow.FindControl("rdoOption");
                Label lblOpCatiD = (Label)GvOptrow.FindControl("lblOptionCategoryId");
                DataTable dtOption = new DataView(ObjInvBOM.BOM_ByProductId(StrCompId.ToString(), ProductId.ToString()), "OptionCategoryId='" + lblOpCatiD.Text.Trim() + "'", "", DataViewRowState.CurrentRows).ToTable();
                foreach (DataRow row in dtOption.Rows)
                {
                    var txt = string.Empty;
                    string ProductName = string.Empty;
                    if (row["SubProductId"].ToString() == "0")
                    {

                        txt = row["OptionId"].ToString() + "  ( " + row["ShortDescription"].ToString() + " )";
                    }
                    else
                    {
                        ProductName = new DataView(ObjInvProductMaster.GetProductMasterTrueAll(StrCompId.ToString()), "ProductId = '" + row["SubProductId"].ToString().Trim() + "'", "", DataViewRowState.CurrentRows).ToTable().Rows[0]["EProductName"].ToString();
                        txt = row["OptionId"].ToString() + "  (  " + ProductName.ToString() + " ," + row["ShortDescription"].ToString() + " )";
                    }
                    var val = row["TransID"].ToString();
                    var item = new ListItem(txt, val);
                    RdoList.Items.Add(item);
                }
            }
            catch
            {

            }
        }
    }
    protected void rdoOption_SelectedIndexChanged(object sender, EventArgs e)
    {
        GridViewRow Row = (GridViewRow)((RadioButtonList)sender).Parent.Parent;

        Label lblCategoryId = (Label)Row.FindControl("lblOptionCategoryId");
        RadioButtonList RdoList = (RadioButtonList)Row.FindControl("rdoOption");
        string PartNo = string.Empty;
        string OldPartOption = string.Empty;
        string value = string.Empty;
        for (int i = 0; i < RdoList.Items.Count; i++)
        {
            if (RdoList.Items[i].Selected)
            {
                PartNo = RdoList.Items[i].Value.ToString();
            }
            else
            {
                try
                {

                    value = txtOptionPartNo.Text.Replace(RdoList.Items[i].Value.ToString(), "");

                    txtOptionPartNo.Text = value;
                }
                catch
                {

                }
            }

        }
        txtOptionPartNo.Text += PartNo.ToString();

    }
    protected void txtOptionPartNo_TextChanged(object sender, EventArgs e)
    {
        DataTable dt = new DataTable();
        DataTable dtTemp = new DataTable();
        fillOptionCategorygrid();
        bool b = true;
        if (txtOptionPartNo.Text != "")
        {
            for (int i = 0; i < txtOptionPartNo.Text.Length; i++)
            {
                char c = txtOptionPartNo.Text[i];

                dtTemp = ObjInvBOM.BOM_ById(StrCompId.ToString(), c.ToString());
                if (dtTemp.Rows.Count != 0)
                {
                    dt.Merge(dtTemp);
                    for (int j = 0; j < dt.Rows.Count; j++)
                    {
                        dtTemp = null;
                        dtTemp = new DataView(dt, "OptionCategoryId='" + dt.Rows[j]["OptionCategoryId"].ToString() + "'", "", DataViewRowState.CurrentRows).ToTable();
                        if (dtTemp.Rows.Count == 2)
                        {
                            b = false;
                        }
                    }

                }
                else
                {
                    b = false;
                }
            }


            if (!b)
            {
                DisplayMessage("Invalid Part No");
                txtOptionPartNo.Text = "";
                txtOptionPartNo.Focus();
                fillOptionCategorygrid();
            }
            else
            {
                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    foreach (GridViewRow Row in gvOptionCategory.Rows)
                    {

                        RadioButtonList RdoList = (RadioButtonList)Row.FindControl("rdoOption");
                        for (int j = 0; j < RdoList.Items.Count; j++)
                        {
                            if (RdoList.Items[j].Value == dt.Rows[i]["TransId"].ToString())
                            {
                                RdoList.Items[j].Selected = true;
                            }


                        }

                    }


                }


            }
        }
    }

    protected void btnReset_Click(object sender, EventArgs e)
    {
        txtProductId.Text = "";
        txtModelNo.Text = "";
        txtProductPartNo.Text = "";
        txtOptionPartNo.Text = "";
        gvOptionCategory.DataSource = null;
        gvOptionCategory.DataBind();
        txtProductId.Focus();
    }
    protected void btnCancel_Click(object sender, EventArgs e)
    {

        FillProductGrid();
        txtValue.Text = "";
        ddlFieldName.SelectedIndex = 1;
        ddlOption.SelectedIndex = 3;
        btnReset_Click(null, null);
        btnList_Click(null, null);
        txtValue.Focus();
    }


    #endregion

    #region AutoComplete Method
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
    public string GetItemType(string IT)
    {
        string retval = string.Empty;
        if (IT == "A")
        {
            retval = "Assemble  (Search as A)";
        }
        if (IT == "K")
        {
            retval = "KIT  (Search as K)";
        }
        if (IT == "S")
        {
            retval = "Stockable (Search as S)";
        }
        if (IT == "NS")
        {
            retval = "Non-Stockable (Search as NS)";
        }

        return retval;

    }

    private void FillProductGrid()
    {
        DataTable dtproduct = ObjInvProductMaster.GetProductMasterTrueAll(StrCompId.ToString());

        GridProduct.DataSource = dtproduct;
        GridProduct.DataBind();
        Session["DtFilter"] = dtproduct;
        Session["DtProduct"] = dtproduct;
        lblTotalRecord.Text = Resources.Attendance.Total_Records + " : " + dtproduct.Rows.Count;

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


    public void fillOptionCategorygrid()
    {
        try
        {
            string ProductId = ViewState["ProductId"].ToString();

            DataTable dt = ObjInvBOM.BOM_ByProductId(StrCompId.ToString(), ProductId.ToString()).DefaultView.ToTable(true, "OptionCategoryId");
            gvOptionCategory.DataSource = dt;
            gvOptionCategory.DataBind();
            if (dt.Rows.Count == 0)
            {
                txtOptionPartNo.ReadOnly = true;
            }
            else
            {
                txtOptionPartNo.ReadOnly = false;
            }
        }
        catch
        {

        }
    }

    public string GetOpCateName(string OpCatId)
    {
        string OpCateName = string.Empty;
        try
        {
            OpCateName = ObjOpCate.GetOptionCategoryTruebyId(StrCompId.ToString(), OpCatId.ToString()).Rows[0]["EName"].ToString();
        }
        catch
        {

        }
        return OpCateName;
    }

    #endregion







}
