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

public partial class Inventory_ItemMaster : BasePage
{
    #region defind Class Object

    Inv_ProductMaster ObjProductMaster = new Inv_ProductMaster();
    Common ObjComman = new Common();
    Inv_UnitMaster ObjUnitMaster = new Inv_UnitMaster();
    CountryMaster ObjSysCountryMaster = new CountryMaster();
    BrandMaster ObjBrandMaster = new BrandMaster();
    LocationMaster ObjLocMaster = new LocationMaster();
    Inv_ProductBrandMaster ObjProductBrandMaster = new Inv_ProductBrandMaster();
    Inv_Product_Brand ObjProductBrand = new Inv_Product_Brand();
    Inv_Product_Category ObjProductCate = new Inv_Product_Category();
    Inv_Product_Location ObjProductLocation = new Inv_Product_Location();
    Inv_Product_CompanyBrand ObjCompanyBrand = new Inv_Product_CompanyBrand();
    Inv_ProductImage ObjProductImage = new Inv_ProductImage();
    Inv_ProductCategoryMaster ObjProductCateMaster = new Inv_ProductCategoryMaster();
    SystemParameter ObjSysPeram = new SystemParameter();
    Common cmn = new Common();

    //Set_Location_Brand ObjLocBrand = new Set_Location_Brand();
    Set_Suppliers ObjSupplierMaster = new Set_Suppliers();
    Ems_ContactMaster ObjContactMaster = new Ems_ContactMaster();
    Inv_ProductSuppliers ObjProductSupplier = new Inv_ProductSuppliers();
    //For Stock:-Start
    Inv_StockDetail objStockDetail = new Inv_StockDetail();
    
    //End

    string StrCompId = string.Empty;
    string StrUserId = string.Empty;

    #endregion


    protected void Page_Load(object sender, EventArgs e)
    {

        if (Session["UserId"] == null)
        {
            Response.Redirect("~/ERPLogin.aspx");
        }

        if (!IsPostBack)
        {
            if (Session["lang"] == null)
            {
                Session["lang"] = "1";
            }
            StrUserId = Session["UserId"].ToString();
            StrCompId = Session["CompId"].ToString();

            ViewState["CurrIndex"] = 0;
            ViewState["SubSize"] = 9;
            ViewState["CurrIndexbin"] = 0;
            ViewState["SubSizebin"] = 9;
            #region  remove
            FillddlBrandSearch(ddlbrandsearch);
            FillddlBrandSearch(ddlBinBrandSearch);
            FillProductBrand();
            FillBrand();
            FillProductCategory();
            FillProductCategorySerch(ddlcategorysearch);
            FillProductCategorySerch(ddlBinCategorySearch);


            #endregion
            btnList_Click(null, null);
            FillDataListGrid();
            FillbinDataListGrid();
            Page.Form.Attributes.Add("enctype", "multipart/form-data");

        }

        AllPageCode();
    }


    #region AllPageCode

    public void AllPageCode()
    {
        Page.Title = ObjSysPeram.GetSysTitle();
        StrUserId = Session["UserId"].ToString();
        StrCompId = Session["CompId"].ToString();
        Session["AccordianId"] = "11";
        Session["HeaderText"] = "Inventory";
        DataTable dtAllPageCode = cmn.GetAllPagePermission(StrUserId.ToString(), "11", "24");
        if (dtAllPageCode.Rows.Count != 0)
        {
            if (dtAllPageCode.Rows[0][0].ToString() == "SuperAdmin")
            {
                btnSave.Visible = true;
                foreach (GridViewRow Row in gvProduct.Rows)
                {
                    ((ImageButton)Row.FindControl("btnEdit")).Visible = true;

                }
                ddlstatus.Visible = true;
                ImgbtnSelectAll.Visible = true;
                imgBtnRestore.Visible = true;
                foreach (DataListItem dtlist in dtlistbinProduct.Items)
                {
                    ((CheckBox)dtlist.FindControl("chkActive")).Visible = true;
                }
                foreach (DataListItem dtlist in dtlistProduct.Items)
                {
                    ((LinkButton)dtlist.FindControl("lbldlProductName")).Enabled = true;
                    ((ImageButton)dtlist.FindControl("btnImgProduct")).Enabled = true;
                }
            }
            else
            {
                foreach (DataRow DtRow in dtAllPageCode.Rows)
                {
                    if (Convert.ToBoolean(DtRow["Op_Add"].ToString()))
                    {
                        btnSave.Visible = true;
                    }
                    if (Convert.ToBoolean(DtRow["Op_Edit"].ToString()))
                    {

                        foreach (GridViewRow Row in gvProduct.Rows)
                        {
                            ((ImageButton)Row.FindControl("btnEdit")).Visible = true;

                        }
                        foreach (DataListItem dtlist in dtlistProduct.Items)
                        {
                            ((LinkButton)dtlist.FindControl("lbldlProductName")).Enabled = true;
                            ((ImageButton)dtlist.FindControl("btnImgProduct")).Enabled = true;
                        }

                    }
                    if (Convert.ToBoolean(DtRow["Op_Delete"].ToString()))
                    {
                        ddlstatus.Visible = true;

                    }
                    if (Convert.ToBoolean(DtRow["Op_Restore"].ToString()))
                    {
                        ImgbtnSelectAll.Visible = true;
                        imgBtnRestore.Visible = true;
                        foreach (DataListItem dtlist in dtlistbinProduct.Items)
                        {
                            ((CheckBox)dtlist.FindControl("chkActive")).Visible = true;
                        }
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

    #region//System Funcation Or Event:-Start


    protected void btnbind_Click(object sender, ImageClickEventArgs e)
    {
        ViewState["CurrIndex"] = 0;
        ViewState["SubSize"] = 9;
        DataTable dtproduct = ObjProductMaster.GetProductMasterTrueAll(StrCompId.ToString());
        DataTable dtproductBrandSearch = new DataTable();


        DataTable dtproductCateSearch = new DataTable();
        if (ddlbrandsearch.SelectedIndex != 0)
        {
            dtproductBrandSearch = dtproduct.Clone();
            DataTable dtProductBrand = ObjProductBrand.GetDataBrandId(StrCompId.ToString(), ddlbrandsearch.SelectedValue);
            for (int i = 0; i < dtProductBrand.Rows.Count; i++)
            {

                dtproductBrandSearch.Merge((new DataView(dtproduct, "ProductId='" + dtProductBrand.Rows[i]["ProductId"].ToString() + "'", "", DataViewRowState.CurrentRows)).ToTable());
            }

            dtproduct.Rows.Clear();
            dtproduct.Merge(dtproductBrandSearch);
        }

        if (ddlcategorysearch.SelectedIndex != 0)
        {
            dtproductCateSearch = dtproduct.Clone();
            DataTable dtProductCate = ObjProductCate.GetProductByCategoryId(StrCompId.ToString(), ddlcategorysearch.SelectedValue);
            for (int i = 0; i < dtProductCate.Rows.Count; i++)
            {

                dtproductCateSearch.Merge((new DataView(dtproduct, "ProductId='" + dtProductCate.Rows[i]["ProductId"].ToString() + "'", "", DataViewRowState.CurrentRows)).ToTable());
            }
            dtproduct.Rows.Clear();
            dtproduct.Merge(dtproductCateSearch);
        }


        if (dtproduct.Rows.Count > 0)
        {

            lnkNext.Visible = true;
            lnkLast.Visible = true;
        }
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
            DataView view = new DataView(dtproduct, condition, "", DataViewRowState.CurrentRows);

            lblTotalRecord.Text = Resources.Attendance.Total_Records + " : " + (view.ToTable()).Rows.Count.ToString() + "";

            Session["dtProduct"] = view.ToTable();

            dtproduct = view.ToTable();
            if (dtproduct.Rows.Count <= 9)
            {
                dtlistProduct.DataSource = dtproduct;
                dtlistProduct.DataBind();
                gvProduct.DataSource = dtproduct;
                gvProduct.DataBind();
                lnkPrev.Visible = false;
                lnkFirst.Visible = false;
                lnkNext.Visible = false;
                lnkLast.Visible = false;


            }
            else
            {
                FillDataList(dtproduct, Convert.ToInt32(ViewState["CurrIndex"].ToString()), Convert.ToInt32(ViewState["SubSize"].ToString()));

            }
            AllPageCode();
            btnbind.Focus();
        }
    }
    protected void btnRefresh_Click(object sender, ImageClickEventArgs e)
    {

        lnkNext.Visible = true;
        lnkLast.Visible = true;

        ViewState["CurrIndex"] = 0;
        ViewState["SubSize"] = 9;
        FillDataListGrid();
        ddlFieldName.SelectedIndex = 1;
        ddlOption.SelectedIndex = 3;
        txtValue.Text = "";
        btngo_Click(null, null);


        txtValue.Focus(); ;
    }
    protected void btngo_Click(object sender, EventArgs e)
    {
        ViewState["CurrIndex"] = 0;
        ViewState["SubSize"] = 9;
        lnkFirst.Visible = false;
        lnkPrev.Visible = false;
        lnkNext.Visible = true;
        lnkLast.Visible = true;
        FillDataListGrid();

    }
    protected void imbBtnGrid_Click(object sender, ImageClickEventArgs e)
    {
        lnkNext.Visible = false;
        lnkLast.Visible = false;
        lnkFirst.Visible = false;
        lnkPrev.Visible = false;
        dtlistProduct.Visible = false;
        gvProduct.Visible = true;
        FillDataListGrid();
        imgBtnDatalist.Visible = true;
        imbBtnGrid.Visible = false;
        txtValue.Focus();


    }
    protected void imgBtnDatalist_Click(object sender, ImageClickEventArgs e)
    {

        ViewState["CurrIndex"] = 0;
        ViewState["SubSize"] = 9;

        lnkNext.Visible = true;
        lnkLast.Visible = true;
        dtlistProduct.Visible = true;
        gvProduct.Visible = false;
        FillDataListGrid();
        imgBtnDatalist.Visible = false;
        imbBtnGrid.Visible = true;

        txtValue.Focus();

    }
    protected void btnResetSreach_Click(object sender, EventArgs e)
    {
        ddlbrandsearch.SelectedIndex = 0;
        ddlcategorysearch.SelectedIndex = 0;

        FillDataListGrid();
        txtValue.Focus();
    }
    protected void lnkFirst_Click(object sender, EventArgs e)
    {
        lnkPrev.Visible = false;
        lnkFirst.Visible = false;
        lnkLast.Visible = true;
        lnkNext.Visible = true;
        ViewState["CurrIndex"] = 0;
        ViewState["SubSize"] = 9;
        DataTable dt = (DataTable)Session["dtProduct"];
        FillDataList(dt, Convert.ToInt32(ViewState["CurrIndex"].ToString()), Convert.ToInt32(ViewState["SubSize"].ToString()));
        ViewState["SubSize"] = 9;

        int index = dt.Rows.Count / Convert.ToInt32(ViewState["SubSize"].ToString());
        lnkNext.Focus();
    }
    protected void lnkLast_Click(object sender, EventArgs e)
    {
        ViewState["SubSize"] = 9;
        DataTable dt = (DataTable)Session["dtProduct"];
        int index = dt.Rows.Count / Convert.ToInt32(ViewState["SubSize"].ToString());

        ViewState["CurrIndex"] = index;
        int tot = dt.Rows.Count;

        if (tot % Convert.ToInt32(ViewState["SubSize"].ToString()) > 0)
        {
            FillDataList(dt, Convert.ToInt32(ViewState["CurrIndex"].ToString()), Convert.ToInt32(ViewState["SubSize"].ToString()));
        }
        else if (tot % Convert.ToInt32(ViewState["SubSize"].ToString()) == 0)
        {
            FillDataList(dt, index - 1, Convert.ToInt32(ViewState["SubSize"].ToString()));
        }
        else
        {
            FillDataList(dt, Convert.ToInt32(ViewState["CurrIndex"].ToString()), Convert.ToInt32(ViewState["SubSize"].ToString()));
        }
        lnkLast.Visible = false;
        lnkNext.Visible = false;
        lnkPrev.Visible = true;
        lnkFirst.Visible = true;
        lnkFirst.Focus();
    }
    protected void lnkPrev_Click(object sender, EventArgs e)
    {
        DataTable dt = (DataTable)Session["dtProduct"];
        ViewState["SubSize"] = 9;
        ViewState["CurrIndex"] = Convert.ToInt32(ViewState["CurrIndex"].ToString()) - 1;
        if (Convert.ToInt32(ViewState["CurrIndex"].ToString()) < 0)
        {
            ViewState["CurrIndex"] = 0;


        }

        if (Convert.ToInt16(ViewState["CurrIndex"]) == 0)
        {

            lnkFirst.Visible = false;
            lnkPrev.Visible = false;
            lnkNext.Visible = true;
            lnkLast.Visible = true;
        }
        else
        {
            lnkFirst.Visible = true;
            lnkLast.Visible = true;
            lnkNext.Visible = true;
        }

        FillDataList(dt, Convert.ToInt32(ViewState["CurrIndex"].ToString()), Convert.ToInt32(ViewState["SubSize"].ToString()));
        lnkNext.Focus();
    }
    protected void lnkNext_Click(object sender, EventArgs e)
    {
        ViewState["SubSize"] = 9;
        DataTable dt = (DataTable)Session["dtProduct"];
        int index = dt.Rows.Count / Convert.ToInt32(ViewState["SubSize"].ToString());
        int k1 = Convert.ToInt32(ViewState["CurrIndex"].ToString());
        ViewState["CurrIndex"] = Convert.ToInt32(ViewState["CurrIndex"].ToString()) + 1;
        int k = Convert.ToInt32(ViewState["CurrIndex"].ToString());
        if (Convert.ToInt32(ViewState["CurrIndex"].ToString()) >= index)
        {
            ViewState["CurrIndex"] = index;
            lnkNext.Visible = false;
            lnkLast.Visible = false;
        }
        int tot = dt.Rows.Count;

        if (k == index)
        {
            if (tot % Convert.ToInt32(ViewState["SubSize"].ToString()) > 0)
            {
                FillDataList(dt, Convert.ToInt32(ViewState["CurrIndex"].ToString()), Convert.ToInt32(ViewState["SubSize"].ToString()));
                lnkPrev.Visible = true;
                lnkFirst.Visible = true;

            }
            else
            {
                lnkPrev.Visible = true;
                lnkFirst.Visible = true;
                lnkLast.Visible = true;
                lnkNext.Visible = true;
            }
        }
        else if (k < index)
        {
            if (k + 1 == index)
            {
                if (tot % Convert.ToInt32(ViewState["SubSize"].ToString()) > 0)
                {
                    FillDataList(dt, Convert.ToInt32(ViewState["CurrIndex"].ToString()), Convert.ToInt32(ViewState["SubSize"].ToString()));
                    lnkPrev.Visible = true;
                    lnkFirst.Visible = true;
                }
                else
                {
                    FillDataList(dt, Convert.ToInt32(ViewState["CurrIndex"].ToString()), Convert.ToInt32(ViewState["SubSize"].ToString()));
                    lnkNext.Visible = false;
                    lnkLast.Visible = false;
                    lnkPrev.Visible = true;
                    lnkFirst.Visible = true;
                }
            }
            else
            {
                FillDataList(dt, Convert.ToInt32(ViewState["CurrIndex"].ToString()), Convert.ToInt32(ViewState["SubSize"].ToString()));
                lnkPrev.Visible = true;
                lnkFirst.Visible = true;
            }
        }
        else
        {
            FillDataList(dt, Convert.ToInt32(ViewState["CurrIndex"].ToString()), Convert.ToInt32(ViewState["SubSize"].ToString()));
        }

        lnkFirst.Focus();

    }
    protected void gvProduct_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        gvProduct.PageIndex = e.NewPageIndex;

        FillDataListGrid();
        gvProduct.BottomPagerRow.Focus();
    }

    protected void btnEdit_Command(object sender, CommandEventArgs e)
    {

        hdnProductId.Value = e.CommandArgument.ToString();
        txtProductId.Enabled = true;
        ProductEdit();
    }
    protected void btnloadimg_Click(object sender, EventArgs e)
    {
        if (fugProduct.HasFile)
        {

            imgProduct.ImageUrl = null;

            fugProduct.SaveAs(Server.MapPath("~/Temp/" + fugProduct.FileName));
            imgProduct.ImageUrl = "~/Temp/" + fugProduct.FileName;
            Stream fs = fugProduct.PostedFile.InputStream;
            BinaryReader br = new BinaryReader(fs);
            Byte[] bytes = br.ReadBytes((Int32)fs.Length);
            Session["Image"] = bytes;


            btnloadimg.Focus();


        }

    }

    protected void btnList_Click(object sender, EventArgs e)
    {
        pnlMenuList.BackColor = System.Drawing.ColorTranslator.FromHtml("#ccddee");

        pnlMenuNew.BackColor = System.Drawing.ColorTranslator.FromHtml("#90bde9");
        pnlMenuBin.BackColor = System.Drawing.ColorTranslator.FromHtml("#90bde9");

        pnlList.Visible = true;
        pnlBin.Visible = false;
        pnlNewEdit.Visible = false;
        ddlbrandsearch.Focus();
    }
    protected void btnNew_Click(object sender, EventArgs e)
    {
        pnlMenuNew.BackColor = System.Drawing.ColorTranslator.FromHtml("#ccddee");

        pnlMenuList.BackColor = System.Drawing.ColorTranslator.FromHtml("#90bde9");
        pnlMenuBin.BackColor = System.Drawing.ColorTranslator.FromHtml("#90bde9");
        pnlNewEdit.Visible = true;
        pnlBin.Visible = false;
        pnlList.Visible = false;
        txtProductId.Focus();

    }
    protected void btnBin_Click(object sender, EventArgs e)
    {
        pnlMenuBin.BackColor = System.Drawing.ColorTranslator.FromHtml("#ccddee");

        pnlMenuNew.BackColor = System.Drawing.ColorTranslator.FromHtml("#90bde9");
        pnlMenuList.BackColor = System.Drawing.ColorTranslator.FromHtml("#90bde9");
        pnlNewEdit.Visible = false;
        pnlBin.Visible = true;
        pnlList.Visible = false;
        ImgbtnSelectAll.Visible = false;
        imgBtnRestore.Visible = false;
        ddlBinBrandSearch.Focus();
    }
    protected void txtProductCountry_TextChanged(object sender, EventArgs e)
    {
        if (txtProductCountry.Text != "")
        {
            string CountryId = string.Empty;
            try
            {
                CountryId = new DataView(ObjSysCountryMaster.GetCountryMaster(), "Country_Name='" + txtProductCountry.Text.Trim() + "'", "", DataViewRowState.CurrentRows).ToTable().Rows[0]["Country_Id"].ToString();
                txtWholesalePrice.Focus();
            }
            catch
            {
                txtProductCountry.Focus();
                txtProductCountry.Text = "";
                DisplayMessage("Select Made In Country");

            }
        }
    }

    protected void btnpushBrandAll_Click(object sender, EventArgs e)
    {


        foreach (ListItem li in lstProductBrand.Items)
        {
            lstSelectedProductBrand.Items.Add(li);


        }
        foreach (ListItem li in lstSelectedProductBrand.Items)
        {

            lstProductBrand.Items.Remove(li);

        }

        btnpushBrandAll.Focus();

    }
    protected void btnpullBrandAll_Click(object sender, EventArgs e)
    {


        foreach (ListItem li in lstSelectedProductBrand.Items)
        {

            lstProductBrand.Items.Add(li);


        }
        foreach (ListItem li in lstProductBrand.Items)
        {

            lstSelectedProductBrand.Items.Remove(li);

        }

        btnpullBrandAll.Focus();

    }
    protected void btnpushBrand_Click(object sender, EventArgs e)
    {
        if (lstProductBrand.SelectedIndex >= 0)
        {

            foreach (ListItem li in lstProductBrand.Items)
            {
                if (li.Selected)
                {
                    lstSelectedProductBrand.Items.Add(li);

                }
            }
            foreach (ListItem li in lstSelectedProductBrand.Items)
            {
                lstProductBrand.Items.Remove(li);
            }
            lstSelectedProductBrand.SelectedIndex = -1;
        }
        btnpushBrand.Focus();


    }
    protected void btnpullBrand_Click(object sender, EventArgs e)
    {
        if (lstSelectedProductBrand.SelectedIndex >= 0)
        {

            foreach (ListItem li in lstSelectedProductBrand.Items)
            {
                if (li.Selected)
                {
                    lstProductBrand.Items.Add(li);

                }
            }
            foreach (ListItem li in lstProductBrand.Items)
            {


                lstSelectedProductBrand.Items.Remove(li);

            }
            lstProductBrand.SelectedIndex = -1;
        }
        btnpullBrand.Focus();

    }
    protected void btnPushAllLoc_Click(object sender, EventArgs e)
    {
        foreach (ListItem li in lstLocation.Items)
        {
            lstLocationSelect.Items.Add(li);
        }
        foreach (ListItem LocationItem in lstLocationSelect.Items)
        {
            lstLocation.Items.Remove(LocationItem);


        }
        btnPushAllLoc.Focus();
    }
    protected void btnPullAllLoc_Click(object sender, EventArgs e)
    {
        foreach (ListItem li in lstLocationSelect.Items)
        {
            lstLocation.Items.Add(li);
        }
        foreach (ListItem li in lstLocation.Items)
        {
            lstLocationSelect.Items.Remove(li);
        }
        btnPullAllLoc.Focus();
    }
    protected void btnPushLoc_Click(object sender, EventArgs e)
    {
        if (lstLocation.SelectedIndex >= 0)
        {
            foreach (ListItem li in lstLocation.Items)
            {
                if (li.Selected)
                {
                    lstLocationSelect.Items.Add(li);
                }
            }
            foreach (ListItem li in lstLocationSelect.Items)
            {
                lstLocation.Items.Remove(li);

            }

            lstLocationSelect.SelectedIndex = -1;

        }
        btnPushLoc.Focus();


    }
    protected void btnPullLoc_Click(object sender, EventArgs e)
    {
        if (lstLocationSelect.SelectedIndex >= 0)
        {
            foreach (ListItem li in lstLocationSelect.Items)
            {
                if (li.Selected)
                {
                    lstLocation.Items.Add(li);
                }
            }
            foreach (ListItem li in lstLocation.Items)
            {
                lstLocationSelect.Items.Remove(li);

            }

        }
        btnPullLoc.Focus();
    }

    protected void btnGetlocation_Click(object sender, EventArgs e)
    {
        DataTable dtLocation = new DataTable();
        dtLocation.Columns.Add("LocationId");
        dtLocation.Columns.Add("ELocationName");
        int j = 0;
        foreach (ListItem li in ChkBrand.Items)
        {
            if (li.Selected == true)
            {

                DataTable dt = new DataView(ObjLocMaster.GetLocationMaster(StrCompId.ToString()), "Brand_Id ='" + li.Value.ToString() + "'", "", DataViewRowState.CurrentRows).ToTable();
                if (dt.Rows.Count > 0)
                {

                    for (int i = 0; i < dt.Rows.Count; i++)
                    {
                        dtLocation.Rows.Add(j);
                        dtLocation.Rows[j]["LocationId"] = dt.Rows[i]["Location_Id"].ToString();
                        dtLocation.Rows[j]["ELocationName"] = dt.Rows[i]["Location_Name"].ToString();
                        j++;
                    }
                }

            }
        }
        Session["dtLoc"] = dtLocation;
        FillProductLocation();
        btnGetlocation.Focus();
    }

    protected void btnPushAllCate_Click(object sender, EventArgs e)
    {
        foreach (ListItem li in lstProductCategory.Items)
        {
            lstSelectProductCategory.Items.Add(li);


        }
        foreach (ListItem li in lstSelectProductCategory.Items)
        {

            lstProductCategory.Items.Remove(li);

        }
        btnPushAllCate.Focus();
    }
    protected void btnPullAllCate_Click(object sender, EventArgs e)
    {

        foreach (ListItem li in lstSelectProductCategory.Items)
        {

            lstProductCategory.Items.Add(li);


        }
        foreach (ListItem li in lstProductCategory.Items)
        {

            lstSelectProductCategory.Items.Remove(li);

        }

        btnPullAllCate.Focus();
    }
    protected void btnPushCate_Click(object sender, EventArgs e)
    {
        if (lstProductCategory.SelectedIndex >= 0)
        {

            foreach (ListItem li in lstProductCategory.Items)
            {
                if (li.Selected)
                {
                    lstSelectProductCategory.Items.Add(li);

                }
            }
            foreach (ListItem li in lstSelectProductCategory.Items)
            {
                lstProductCategory.Items.Remove(li);
            }
            lstSelectProductCategory.SelectedIndex = -1;
        }
        btnPushCate.Focus();
    }
    protected void btnPullCate_Click(object sender, EventArgs e)
    {
        if (lstSelectProductCategory.SelectedIndex >= 0)
        {

            foreach (ListItem li in lstSelectProductCategory.Items)
            {
                if (li.Selected)
                {
                    lstProductCategory.Items.Add(li);

                }
            }
            foreach (ListItem li in lstProductCategory.Items)
            {
                lstSelectProductCategory.Items.Remove(li);
            }
            lstProductCategory.SelectedIndex = -1;
        }
        btnPullCate.Focus();
    }




    protected void txtSuppliers_OnTextChanged(object sender, EventArgs e)
    {
        DataTable dt = (DataTable)Session["dtProductSupplierCode"];
        if (dt != null)
        {
            if (txtSuppliers.Text != "")
            {
                try
                {
                    string strSupplierId = "";
                    strSupplierId = (txtSuppliers.Text.Split('/'))[txtSuppliers.Text.Split('/').Length - 1];
                    string query = "Supplier_Id = '" + strSupplierId + "'";
                    dt = new DataView(dt, query, "", DataViewRowState.CurrentRows).ToTable();
                    if (dt.Rows.Count > 0)
                    {
                        DisplayMessage("Supplier Name Already Exists");
                        txtSuppliers.Text = "";
                        System.Web.UI.ScriptManager.GetCurrent(this).SetFocus(txtSuppliers);
                    }
                    else
                    {

                        DataTable dt1 = ObjSupplierMaster.GetSupplierAllTrueData(StrCompId.ToString(), "1");
                        dt1 = new DataView(dt1, "Supplier_Id='" + strSupplierId + "'", "", DataViewRowState.CurrentRows).ToTable();
                        if (dt1.Rows.Count > 0)
                        {
                            System.Web.UI.ScriptManager.GetCurrent(this).SetFocus(txtProductSupplierCode);
                        }
                        else
                        {
                            DisplayMessage("Invalid Supplier Name");
                            txtSuppliers.Text = "";
                            txtSuppliers.Focus();
                            System.Web.UI.ScriptManager.GetCurrent(this).SetFocus(txtSuppliers);
                        }
                    }
                }
                catch
                {
                    DisplayMessage("Invalid Supplier Name");
                    txtSuppliers.Text = "";
                    System.Web.UI.ScriptManager.GetCurrent(this).SetFocus(txtSuppliers);
                }
            }
        }
        else
        {
            if (txtSuppliers.Text != "")
            {
                string strSupplierId = "";
                strSupplierId = (txtSuppliers.Text.Split('/'))[txtSuppliers.Text.Split('/').Length - 1];
                DataTable dt1 = ObjSupplierMaster.GetSupplierAllTrueData(StrCompId.ToString(), "1");
                dt1 = new DataView(dt1, "Supplier_Id='" + strSupplierId + "'", "", DataViewRowState.CurrentRows).ToTable();
                if (dt1.Rows.Count > 0)
                {
                    System.Web.UI.ScriptManager.GetCurrent(this).SetFocus(txtProductSupplierCode);
                }
                else
                {
                    DisplayMessage("Invalid Supplier Name");

                    txtSuppliers.Focus();
                    txtSuppliers.Text = "";
                    System.Web.UI.ScriptManager.GetCurrent(this).SetFocus(txtSuppliers);
                }
            }
        }

    }



    protected void GridProductSupplierCode_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        GridProductSupplierCode.PageIndex = e.NewPageIndex;
        DataTable dt = (DataTable)Session["dtProductSupplierCode"];
        GridProductSupplierCode.DataSource = dt;
        GridProductSupplierCode.DataBind();
    }
    protected void IbtnAddProductSupplierCode_Click(object sender, ImageClickEventArgs e)
    {
        if (txtSuppliers.Text != "")
        {
            DataTable dt = (DataTable)Session["dtProductSupplierCode"];
            if (dt == null)
            {
                dt = new DataTable();
                dt.Columns.Add("Supplier_Id");
                dt.Columns.Add("Name");
                dt.Columns.Add("ProductSupplierCode");
            }

            string strSupplierId = "";
            string strSupplierName = "";
            if (txtSuppliers.Text != "")
            {
                strSupplierId = (txtSuppliers.Text.Split('/'))[txtSuppliers.Text.Split('/').Length - 1];
                strSupplierName = txtSuppliers.Text.Split('/')[0];
            }



            dt.Rows.Add(strSupplierId, strSupplierName, txtProductSupplierCode.Text);
            GridProductSupplierCode.DataSource = dt;
            GridProductSupplierCode.DataBind();
            Session["dtProductSupplierCode"] = dt;
            txtProductSupplierCode.Text = "";
            txtSuppliers.Text = "";
            System.Web.UI.ScriptManager.GetCurrent(this).SetFocus(txtSuppliers);
        }
        else
        {
            // DisplayMessage("Please Select Supplier First");
            System.Web.UI.ScriptManager.GetCurrent(this).SetFocus(txtSuppliers);
        }
    }
    protected void IbtnDeleteSupplier_Command(object sender, CommandEventArgs e)
    {
        DataTable dt = (DataTable)Session["dtProductSupplierCode"];
        if (dt != null)
        {
            if (dt.Rows.Count > 0)
            {
                var rows = dt.Select("Supplier_Id ='" + e.CommandArgument.ToString() + "'");
                foreach (var row in rows)
                    row.Delete();
                GridProductSupplierCode.DataSource = dt;
                GridProductSupplierCode.DataBind();
                Session["dtProductSupplierCode"] = dt;
            }
        }

    }




    protected void txtProductUnit_TextChanged(object sender, EventArgs e)
    {
        if (txtProductUnit.Text != "")
        {
            if (GetUnitId(txtProductUnit.Text) == "")
            {
                txtProductUnit.Text = "";
                DisplayMessage("Select Product Unit");
                txtProductUnit.Focus();
            }
            else
            {

                txtDesc.Focus();
            }

        }
    }

    protected void ddlMaintainStock_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (ddlMaintainStock.SelectedIndex == 1)
        {
            TdTypeOfBatchNo1.Visible = true;
            TdTypeOfBatchNo2.Visible = true;
            TdTypeOfBatchNo3.Visible = true;


        }
        else if (ddlMaintainStock.SelectedIndex == 3)
        {
            TdTypeOfBatchNo1.Visible = true;
            TdTypeOfBatchNo2.Visible = true;
            TdTypeOfBatchNo3.Visible = true;

        }
        else
        {
            TdTypeOfBatchNo1.Visible = false;
            TdTypeOfBatchNo2.Visible = false;
            TdTypeOfBatchNo3.Visible = false;

        }
        ddlTypeOfBatchNo.Focus();

    }
    protected void ddlItypeType_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (ddlItemType.SelectedIndex == 1)
        {
            tdR1.Visible = true;
            tdR2.Visible = true;
            tdR3.Visible = true;
            txtReorderQty.Focus();
        }
        else
        {
            tdR1.Visible = false;
            tdR2.Visible = false;
            tdR3.Visible = false;
        }
        txtReorderQty.Text = "";

    }

    protected void btnSave_Click(object sender, EventArgs e)
    {

        string HSCode = string.Empty;
        string HSSerialNo = string.Empty;
        string IsActive = string.Empty;
        if (txtEProductName.Text == "")
        {
            DisplayMessage("Enter Product Name");
            txtEProductName.Focus();
            return;
        }

        if (ddlItemType.SelectedIndex == 0)
        {
            DisplayMessage("Select Item Type");
            ddlItemType.Focus();
            return;
        }
        else
        {
            if (ddlItemType.SelectedValue == "S")
            {
                if (txtReorderQty.Text == "")
                {
                    DisplayMessage("Enter Reorder Level");
                    txtReorderQty.Focus();
                    return;
                }
            }

        }
        if (ddlMaintainStock.SelectedIndex == 0)
        {
            DisplayMessage("Select Inventory Type");
            ddlMaintainStock.Focus();
            return;
        }
        if (txtProductUnit.Text == "")
        {
            DisplayMessage("Enter Product Unit");
            txtProductUnit.Focus();
            return;

        }
        if (txtSalesPrice1.Text == "")
        {
            if (txtSalesPrice2.Text == "")
            {
                if (txtSalesPrice3.Text == "")
                {
                    DisplayMessage("Enter Sales Price");
                    txtSalesPrice1.Focus();
                    return;
                }
            }

        }
        if (txtProductId.Text == "")
        {
            txtProductId.Text = ObjProductMaster.GetAutoID(StrCompId.ToString());

        }


        if (txtModelNo.Text == "")
        {
            txtModelNo.Text = txtProductId.Text;
        }

        if (ChkHasBatchNo.Checked)
        {
            HSCode = true.ToString();
        }
        else
        {
            HSCode = false.ToString();
        }

        if (ChkHasSerialNo.Checked)
        {
            HSSerialNo = true.ToString();
        }
        else
        {
            HSSerialNo = false.ToString();
        }

        if (ddlstatus.SelectedItem.Text == "Active")
        {
            IsActive = true.ToString();
        }
        else
        {
            if (ddlstatus.SelectedItem.Text == "InActive")
            {
                IsActive = false.ToString();
            }
            else
            {
                IsActive = false.ToString();
            }

        }

        if (txtProductUnit.Text != "")
        {
            txtProductUnit.Text = txtProductUnit.Text.Split('/')[1].ToString();

        }

        if (txtLenth.Text == "")
        {
            txtLenth.Text = "0";
        }
        if (txtHeight.Text == "")
        {
            txtHeight.Text = "0";
        }
        if (txtDepth.Text == "")
        {
            txtDepth.Text = "0";
        }
        if (txtprofit.Text == "")
        {
            txtprofit.Text = "0";
        }

        int b = 0;
        if (txtProductCountry.Text != "")
        {
            string CountryId = string.Empty;
            try
            {
                CountryId = new DataView(ObjSysCountryMaster.GetCountryMaster(), "Country_Name='" + txtProductCountry.Text.Trim() + "'", "", DataViewRowState.CurrentRows).ToTable().Rows[0]["Country_Id"].ToString();
                txtProductCountry.Text = CountryId.ToString();
            }
            catch
            { }
        }
        if (hdnProductId.Value == "")
        {
            b = ObjProductMaster.InsertProductMaster(StrCompId.ToString(), "1", txtProductId.Text.Trim().ToString(), txtPartNo.Text.Trim().ToString(), txtModelNo.Text.Trim().ToString(), txtEProductName.Text.Trim().ToString(), txtLProductName.Text.Trim().ToString(), txtProductCountry.Text.Trim().ToString(), txtProductUnit.Text.Trim().ToString(), ddlItemType.SelectedValue.ToString(), txtHasCode.Text.Trim().ToString(), HSCode.ToString(), ddlTypeOfBatchNo.SelectedValue.ToString(), HSSerialNo.ToString(), txtReorderQty.Text.Trim().ToString(), txtCostPrice.Text.Trim().ToString(), txtDesc.Content.Trim().ToString(), txtSalesPrice1.Text.Trim(), txtSalesPrice2.Text.Trim(), txtSalesPrice3.Text.Trim(), txtProductColor.Text.Trim(), txtWholesalePrice.Text.Trim(), "ReseverQty", txtDamageQty.Text.Trim(), txtExpQty.Text.Trim().ToString(), txtMaxQty.Text.Trim().ToString(), txtMiniQty.Text.Trim().ToString(), txtprofit.Text.Trim().ToString(), txtDiscount.Text.Trim().ToString(), ddlMaintainStock.SelectedValue.ToString(), "URL", "0", "", txtLenth.Text.Trim().ToString(), txtHeight.Text.Trim().ToString(), txtDepth.Text.Trim().ToString(), txtAlterId1.Text.Trim().ToString(), txtAlterId2.Text.Trim().ToString(), txtAlterId3.Text.Trim().ToString(), "", "", "", "", "", true.ToString(), DateTime.Now.ToString(), IsActive.ToString(), StrUserId.ToString(), DateTime.Now.ToString(), StrUserId.ToString(), DateTime.Now.ToString());


        }
        else
        {
            b = ObjProductMaster.UpdateProductMaster(StrCompId.ToString(), "1", hdnProductId.Value.ToString(), txtProductId.Text.Trim(), txtPartNo.Text.Trim().ToString(), txtModelNo.Text.Trim().ToString(), txtEProductName.Text.Trim().ToString(), txtLProductName.Text.Trim().ToString(), txtProductCountry.Text.Trim().ToString(), txtProductUnit.Text.Trim().ToString(), ddlItemType.SelectedValue.ToString(), txtHasCode.Text.Trim().ToString(), HSCode.ToString(), ddlTypeOfBatchNo.SelectedValue.ToString(), HSSerialNo.ToString(), txtReorderQty.Text.Trim().ToString(), txtCostPrice.Text.Trim().ToString(), txtDesc.Content.Trim().ToString(), txtSalesPrice1.Text.Trim(), txtSalesPrice2.Text.Trim(), txtSalesPrice3.Text.Trim(), txtProductColor.Text.Trim(), txtWholesalePrice.Text.Trim(), "ReseverQty", txtDamageQty.Text.Trim(), txtExpQty.Text.Trim().ToString(), txtMaxQty.Text.Trim().ToString(), txtMiniQty.Text.Trim().ToString(), txtprofit.Text.Trim().ToString(), txtDiscount.Text.Trim().ToString(), ddlMaintainStock.SelectedValue.ToString(), "URL", "0", "", txtLenth.Text.Trim().ToString(), txtHeight.Text.Trim().ToString(), txtDepth.Text.Trim().ToString(), txtAlterId1.Text.Trim().ToString(), txtAlterId2.Text.Trim().ToString(), txtAlterId3.Text.Trim().ToString(), "", "", "", "", "", true.ToString(), DateTime.Now.ToString(), IsActive.ToString(), StrUserId.ToString(), DateTime.Now.ToString());
            ObjProductCate.DeleteProductCategory(StrCompId.ToString(), hdnProductId.Value);
            ObjCompanyBrand.DeleteProductCompanyBrand(StrCompId.ToString(), hdnProductId.Value);
            ObjProductLocation.DeleteProductLocation(StrCompId.ToString(), hdnProductId.Value);
            ObjProductBrand.DeleteProductBrand(StrCompId.ToString(), hdnProductId.Value);
            ObjProductSupplier.DeleteProductSuppliers(StrCompId.ToString(), "1", hdnProductId.Value);

        }
        if (b != 0)
        {
            string ProductId = string.Empty;
            if (hdnProductId.Value == "")
            {
                try
                {
                    ProductId = ObjProductMaster.GetProductMaserTrueAllByProductCode(StrCompId.ToString(), txtProductId.Text.Trim()).Rows[0]["ProductId"].ToString();
                }
                catch
                {

                }
            }
            else
            {
                ProductId = hdnProductId.Value.ToString();
            }
            DataTable dtstock = objStockDetail.GetStockDetail(StrCompId, ProductId);
            if (dtstock.Rows.Count == 0)
            {
               DataTable dtLoc= ObjLocMaster.GetLocationMaster(StrCompId.ToString());
               foreach (DataRow Dr in dtLoc.Rows)
               {
                   objStockDetail.InsertStockDetail(StrCompId.ToString(), Dr["Brand_Id"].ToString(), Dr["Location_Id"].ToString()   , ProductId.Trim(), "0", "0", "0", "0", "0", "0", "0", "0", "0", "", "", "", "", "", true.ToString(), DateTime.Now.ToString(), true.ToString(), StrUserId.ToString(), DateTime.Now.ToString(), StrUserId.ToString(), DateTime.Now.ToString());
               }
               
            }
            if (Session["Image"] != null)
            {
                ObjProductImage.InsertProductImage(StrCompId.ToString(), ProductId.Trim().ToString(), (byte[])Session["Image"], "", "", "", "", "", true.ToString(), DateTime.Now.ToString(), "True", Session["UserId"].ToString(), DateTime.Now.ToString(), Session["UserId"].ToString(), DateTime.Now.ToString());
            }
            foreach (ListItem li in lstSelectedProductBrand.Items)
            {

                ObjProductBrand.InsertProductBrand(StrCompId.ToString(), ProductId.Trim().ToString(), li.Value.ToString(), "", "", "", "", "", true.ToString(), DateTime.Now.ToString(), true.ToString(), Session["UserId"].ToString(), DateTime.Now.ToString(), Session["UserId"].ToString(), DateTime.Now.ToString());
            }

            foreach (ListItem li in lstSelectProductCategory.Items)
            {

                ObjProductCate.InsertProductCategory(StrCompId.ToString(), ProductId.Trim().ToString(), li.Value.ToString(), "", "", "", "", "", true.ToString(), DateTime.Now.ToString(), true.ToString(), Session["UserId"].ToString(), DateTime.Now.ToString(), Session["UserId"].ToString(), DateTime.Now.ToString());
            }



            foreach (ListItem li in ChkBrand.Items)
            {
                if (li.Selected)
                {
                    ObjCompanyBrand.InsertProductCompanyBrand(StrCompId.ToString(), ProductId.Trim().ToString(), li.Value.ToString(), "", "", "", "", "", true.ToString(), DateTime.Now.ToString(), true.ToString(), Session["UserId"].ToString(), DateTime.Now.ToString(), Session["UserId"].ToString(), DateTime.Now.ToString());
                }
            }



            foreach (ListItem li in lstLocationSelect.Items)
            {

                ObjProductLocation.InsertProductLocation(StrCompId.ToString(), ProductId.Trim().ToString(), li.Value.ToString(), "", "", "", "", "", true.ToString(), DateTime.Now.ToString(), true.ToString(), Session["UserId"].ToString(), DateTime.Now.ToString(), Session["UserId"].ToString(), DateTime.Now.ToString());

            }



            DataTable dtSupplier = (DataTable)Session["dtProductSupplierCode"];
            if (dtSupplier != null)
            {
                for (int i = 0; i < dtSupplier.Rows.Count; i++)
                {

                    ObjProductSupplier.InsertProductSuppliers(StrCompId.ToString(), "1", ProductId.Trim().ToString(), dtSupplier.Rows[i]["Supplier_Id"].ToString(), dtSupplier.Rows[i]["ProductSupplierCode"].ToString(), "", "", "", "", "", true.ToString(), DateTime.Now.ToString(), true.ToString(), Session["UserId"].ToString(), DateTime.Now.ToString(), Session["UserId"].ToString(), DateTime.Now.ToString());
                }

            }


            Reset();
            FillDataListGrid();

            if (hdnProductId.Value == "")
            {
                DisplayMessage("Record Saved");
                txtProductId.Focus();
            }
            else
            {
                DisplayMessage("Record Updated");
                btnList_Click(null, null);
            }
        }
    }

    protected void btnReset_Click(object sender, EventArgs e)
    {

        Reset();
        txtProductId.Focus();
    }
    protected void btnCancel_Click(object sender, EventArgs e)
    {
        Reset();
        btnList_Click(null, null);

    }

    protected void txtProductId_TextChanged(object sender, EventArgs e)
    {
        if (txtProductId.Text != "")
        {
            DataTable dt = new DataView(ObjProductMaster.GetProductMasterAll(StrCompId.ToString()), "ProductCode='" + txtProductId.Text.Trim().ToString() + "'", "", DataViewRowState.CurrentRows).ToTable();
            if (dt.Rows.Count != 0)
            {
                if (Convert.ToBoolean(dt.Rows[0]["IsActive"].ToString()))
                {
                    DisplayMessage("Product Id Is Already Exits");
                    txtProductId.Text = "";
                    txtProductId.Focus();
                }
                else
                {
                    DisplayMessage("Product Id Is Already Exits :- Go To Bin Tab");
                    txtProductId.Text = "";
                    txtProductId.Focus();

                }
            }
            else
            {
                txtModelNo.Focus();
            }


        }



    }




    #region // Bin Section Start

    protected void chkActive_CheckedChanged(object sender, EventArgs e)
    {
        int b = 0;
        for (int i = 0; i < dtlistbinProduct.Items.Count; i++)
        {

            CheckBox chb = (CheckBox)(dtlistbinProduct.Items[i].FindControl("chkActive"));
            HiddenField hdpid = (HiddenField)(dtlistbinProduct.Items[i].FindControl("hdnChkActive"));

            if (chb.Checked)
            {
                b = ObjProductMaster.RestoreProductMaster(StrCompId.ToString(), hdpid.Value, StrUserId.ToString(), DateTime.Now.ToString());
            }


        }
        if (b != 0)
        {
            DisplayMessage("Record Activated");
            btnbingo_Click(null, null);
            btngo_Click(null, null);
            try
            {
                ((DataListItem)((CheckBox)sender).Parent.Parent).FindControl("chkActive").Focus();
            }
            catch
            {
                txtbinVal.Focus();
            }
        }

    }
    protected void btnbinbind_Click(object sender, ImageClickEventArgs e)
    {
        ViewState["CurrIndexbin"] = 0;
        ViewState["SubSizebin"] = 9;
        DataTable dtproduct = ObjProductMaster.GetProductMasterFalseAll(StrCompId.ToString());

        DataTable dtproductBrandSearch = new DataTable();
        DataTable dtproductCateSearch = new DataTable();
        if (ddlBinBrandSearch.SelectedIndex != 0)
        {
            dtproductBrandSearch = dtproduct.Clone();
            DataTable dtProductBrand = ObjProductBrand.GetDataBrandId(StrCompId.ToString(), ddlBinBrandSearch.SelectedValue);
            for (int i = 0; i < dtProductBrand.Rows.Count; i++)
            {

                dtproductBrandSearch.Merge((new DataView(dtproduct, "ProductId='" + dtProductBrand.Rows[i]["ProductId"].ToString() + "'", "", DataViewRowState.CurrentRows)).ToTable());
            }

            dtproduct.Rows.Clear();
            dtproduct.Merge(dtproductBrandSearch);
        }

        if (ddlBinCategorySearch.SelectedIndex != 0)
        {
            dtproductCateSearch = dtproduct.Clone();
            DataTable dtProductCate = ObjProductCate.GetProductByCategoryId(StrCompId.ToString(), ddlBinCategorySearch.SelectedValue);
            for (int i = 0; i < dtProductCate.Rows.Count; i++)
            {

                dtproductCateSearch.Merge((new DataView(dtproduct, "ProductId='" + dtProductCate.Rows[i]["ProductId"].ToString() + "'", "", DataViewRowState.CurrentRows)).ToTable());
            }
            dtproduct.Rows.Clear();
            dtproduct.Merge(dtproductCateSearch);
        }

        if (dtproduct.Rows.Count > 0)
        {

            lnkbinNext.Visible = true;
            lnkbinLast.Visible = true;
        }
        if (ddlbinOption.SelectedIndex != 0)
        {
            string condition = string.Empty;
            if (ddlOption.SelectedIndex == 1)
            {
                condition = "convert(" + ddlbinFieldName.SelectedValue + ",System.String)='" + txtbinVal.Text.Trim() + "'";
            }
            else if (ddlOption.SelectedIndex == 2)
            {
                condition = "convert(" + ddlbinFieldName.SelectedValue + ",System.String) Like '" + txtbinVal.Text.Trim() + "%'";
            }
            else
            {
                condition = "convert(" + ddlbinFieldName.SelectedValue + ",System.String) like '%" + txtbinVal.Text.Trim() + "%'";
            }
            DataView view = new DataView(dtproduct, condition, "", DataViewRowState.CurrentRows);

            lblbinTotalRecord.Text = Resources.Attendance.Total_Records + " : " + (view.ToTable()).Rows.Count.ToString() + "";

            Session["dtProductBin"] = view.ToTable();

            dtproduct = view.ToTable();
            if (dtproduct.Rows.Count <= 9)
            {
                dtlistbinProduct.DataSource = dtproduct;
                dtlistbinProduct.DataBind();
                gvBinProduct.DataSource = dtproduct;
                gvBinProduct.DataBind();
                lnkbinPrev.Visible = false;
                lnkbinFirst.Visible = false;
                lnkbinNext.Visible = false;
                lnkbinLast.Visible = false;


            }
            else
            {
                FillBinDataList(dtproduct, Convert.ToInt32(ViewState["CurrIndexbin"].ToString()), Convert.ToInt32(ViewState["SubSizebin"].ToString()));

            }

            if (gvBinProduct.Visible == true)
            {
                if (dtproduct.Rows.Count != 0)
                {
                    AllPageCode();
                }
                else
                {
                    imgBtnRestore.Visible = false;
                    ImgbtnSelectAll.Visible = false;
                }

            }

            else
            {
                imgBtnRestore.Visible = false;
                ImgbtnSelectAll.Visible = false;

            }

            btnbinbind.Focus();
        }



    }
    protected void btnbinRefresh_Click(object sender, ImageClickEventArgs e)
    {

        lnkbinNext.Visible = true;
        lnkbinLast.Visible = true;
        ViewState["CurrIndexbin"] = 0;
        ViewState["SubSizebin"] = 9;
        FillbinDataListGrid();
        ddlbinFieldName.SelectedIndex = 1;
        ddlbinOption.SelectedIndex = 3;
        txtbinVal.Text = "";
        btnbingo_Click(null, null);
        lblSelectedRecord.Text = "";
        ViewState["Select"] = null;
        txtValue.Focus();

    }
    protected void btnbingo_Click(object sender, EventArgs e)
    {
        ViewState["CurrIndexbin"] = 0;
        ViewState["SubSizebin"] = 9;
        lnkbinFirst.Visible = false;
        lnkbinPrev.Visible = false;
        lnkbinNext.Visible = true;
        lnkbinLast.Visible = true;
        FillbinDataListGrid();
        btnbinbind.Focus();

    }
    protected void imgBtnbinGrid_Click(object sender, ImageClickEventArgs e)
    {

        lnkbinNext.Visible = false;
        lnkbinLast.Visible = false;
        lnkbinFirst.Visible = false;
        lnkbinPrev.Visible = false;
        dtlistbinProduct.Visible = false;
        gvBinProduct.Visible = true;

        FillbinDataListGrid();
        imgBtnbinDatalist.Visible = true;
        imgBtnbinGrid.Visible = false;
        txtbinVal.Focus();
        imgBtnbinDatalist.Focus();

    }
    protected void imgbtnbinDatalist_Click(object sender, ImageClickEventArgs e)
    {


        ViewState["CurrIndexbin"] = 0;
        ViewState["SubSizebin"] = 9;

        lnkbinNext.Visible = true;
        lnkbinLast.Visible = true;
        dtlistbinProduct.Visible = true;
        gvBinProduct.Visible = false;
        FillbinDataListGrid();
        imgBtnbinDatalist.Visible = false;
        imgBtnbinGrid.Visible = true;
        txtbinVal.Focus();
        imgBtnRestore.Visible = false;
        ImgbtnSelectAll.Visible = false;
        imgBtnbinGrid.Focus();
    }
    protected void btnBinResetSreach_Click(object sender, EventArgs e)
    {
        ddlBinBrandSearch.SelectedIndex = 0;
        ddlBinCategorySearch.SelectedIndex = 0;

        FillbinDataListGrid();
        txtbinVal.Focus();
    }
    protected void lnkbinFirst_Click(object sender, EventArgs e)
    {
        lnkbinPrev.Visible = false;
        lnkbinFirst.Visible = false;
        lnkbinLast.Visible = true;
        lnkbinNext.Visible = true;
        ViewState["CurrIndexbin"] = 0;
        ViewState["SubSizebin"] = 9;
        DataTable dt = (DataTable)Session["dtProductBin"];
        FillBinDataList(dt, Convert.ToInt32(ViewState["CurrIndexbin"].ToString()), Convert.ToInt32(ViewState["SubSizebin"].ToString()));
        ViewState["SubSizebin"] = 9;

        int index = dt.Rows.Count / Convert.ToInt32(ViewState["SubSizebin"].ToString());
        lnkbinNext.Focus();
    }
    protected void lnkbinLast_Click(object sender, EventArgs e)
    {
        ViewState["SubSizebin"] = 9;
        DataTable dt = (DataTable)Session["dtProductBin"];
        int index = dt.Rows.Count / Convert.ToInt32(ViewState["SubSizebin"].ToString());

        ViewState["CurrIndexbin"] = index;
        int tot = dt.Rows.Count;

        if (tot % Convert.ToInt32(ViewState["SubSizebin"].ToString()) > 0)
        {
            FillBinDataList(dt, Convert.ToInt32(ViewState["CurrIndexbin"].ToString()), Convert.ToInt32(ViewState["SubSizebin"].ToString()));
        }
        else if (tot % Convert.ToInt32(ViewState["SubSizebin"].ToString()) == 0)
        {
            FillBinDataList(dt, index - 1, Convert.ToInt32(ViewState["SubSizebin"].ToString()));
        }
        else
        {
            FillBinDataList(dt, Convert.ToInt32(ViewState["CurrIndexbin"].ToString()), Convert.ToInt32(ViewState["SubSizebin"].ToString()));
        }
        lnkbinLast.Visible = false;
        lnkbinNext.Visible = false;
        lnkbinPrev.Visible = true;
        lnkbinFirst.Visible = true;
        lnkbinFirst.Focus();
    }
    protected void lnkbinPrev_Click(object sender, EventArgs e)
    {
        DataTable dt = (DataTable)Session["dtProductBIn"];
        ViewState["SubSizebin"] = 9;
        ViewState["CurrIndexbin"] = Convert.ToInt32(ViewState["CurrIndexbin"].ToString()) - 1;
        if (Convert.ToInt32(ViewState["CurrIndexbin"].ToString()) < 0)
        {
            ViewState["CurrIndexbin"] = 0;


        }

        if (Convert.ToInt16(ViewState["CurrIndexbin"]) == 0)
        {

            lnkbinFirst.Visible = false;
            lnkbinPrev.Visible = false;
            lnkbinNext.Visible = true;
            lnkbinLast.Visible = true;
        }
        else
        {
            lnkbinFirst.Visible = true;
            lnkbinLast.Visible = true;
            lnkbinNext.Visible = true;
        }
        FillBinDataList(dt, Convert.ToInt32(ViewState["CurrIndexbin"].ToString()), Convert.ToInt32(ViewState["SubSizebin"].ToString()));
        lnkbinNext.Focus();
    }
    protected void lnkbinNext_Click(object sender, EventArgs e)
    {
        ViewState["SubSizebin"] = 9;
        DataTable dt = (DataTable)Session["dtProductBin"];
        int index = dt.Rows.Count / Convert.ToInt32(ViewState["SubSizebin"].ToString());
        int k1 = Convert.ToInt32(ViewState["CurrIndexbin"].ToString());
        ViewState["CurrIndexbin"] = Convert.ToInt32(ViewState["CurrIndexbin"].ToString()) + 1;
        int k = Convert.ToInt32(ViewState["CurrIndexbin"].ToString());
        if (Convert.ToInt32(ViewState["CurrIndexbin"].ToString()) >= index)
        {
            ViewState["CurrIndexbin"] = index;
            lnkbinNext.Visible = false;
            lnkbinLast.Visible = false;
        }
        int tot = dt.Rows.Count;

        if (k == index)
        {
            if (tot % Convert.ToInt32(ViewState["SubSizebin"].ToString()) > 0)
            {
                FillBinDataList(dt, Convert.ToInt32(ViewState["CurrIndexbin"].ToString()), Convert.ToInt32(ViewState["SubSizebin"].ToString()));
                lnkbinPrev.Visible = true;
                lnkbinFirst.Visible = true;

            }
            else
            {
                lnkbinPrev.Visible = true;
                lnkbinFirst.Visible = true;
                lnkbinLast.Visible = true;
                lnkbinNext.Visible = true;
            }
        }
        else if (k < index)
        {
            if (k + 1 == index)
            {
                if (tot % Convert.ToInt32(ViewState["SubSizebin"].ToString()) > 0)
                {
                    FillBinDataList(dt, Convert.ToInt32(ViewState["CurrIndexbin"].ToString()), Convert.ToInt32(ViewState["SubSizebin"].ToString()));
                    lnkbinPrev.Visible = true;
                    lnkbinFirst.Visible = true;
                }
                else
                {
                    FillBinDataList(dt, Convert.ToInt32(ViewState["CurrIndexbin"].ToString()), Convert.ToInt32(ViewState["SubSizebin"].ToString()));
                    lnkbinNext.Visible = false;
                    lnkbinLast.Visible = false;
                    lnkbinPrev.Visible = true;
                    lnkbinFirst.Visible = true;
                }
            }
            else
            {
                FillBinDataList(dt, Convert.ToInt32(ViewState["CurrIndexbin"].ToString()), Convert.ToInt32(ViewState["SubSizebin"].ToString()));
                lnkbinPrev.Visible = true;
                lnkbinFirst.Visible = true;
            }
        }
        else
        {
            FillDataList(dt, Convert.ToInt32(ViewState["CurrIndexbin"].ToString()), Convert.ToInt32(ViewState["SubSizebin"].ToString()));
        }
        lnkbinFirst.Focus();


    }
    protected void gvBinProduct_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        gvBinProduct.PageIndex = e.NewPageIndex;
        FillbinDataListGrid();
        string temp = string.Empty;
        bool isselcted;

        for (int i = 0; i < gvBinProduct.Rows.Count; i++)
        {
            Label lblconid = (Label)gvBinProduct.Rows[i].FindControl("lblProductId");
            string[] split = lblSelectedRecord.Text.Split(',');

            for (int j = 0; j < lblSelectedRecord.Text.Split(',').Length; j++)
            {
                if (lblSelectedRecord.Text.Split(',')[j] != "")
                {
                    if (lblconid.Text.Trim().ToString() == lblSelectedRecord.Text.Split(',')[j].Trim().ToString())
                    {
                        ((CheckBox)gvBinProduct.Rows[i].FindControl("chkgvSelect")).Checked = true;
                    }
                }
            }
        }
        gvBinProduct.BottomPagerRow.Focus();

    }
    protected void chkgvSelectAll_CheckedChanged(object sender, EventArgs e)
    {
        CheckBox chkSelAll = ((CheckBox)gvBinProduct.HeaderRow.FindControl("chkgvSelectAll"));
        for (int i = 0; i < gvBinProduct.Rows.Count; i++)
        {
            ((CheckBox)gvBinProduct.Rows[i].FindControl("chkgvSelect")).Checked = chkSelAll.Checked;
            if (chkSelAll.Checked)
            {
                if (!lblSelectedRecord.Text.Split(',').Contains(((Label)(gvBinProduct.Rows[i].FindControl("lblProductId"))).Text.Trim().ToString()))
                {
                    lblSelectedRecord.Text += ((Label)(gvBinProduct.Rows[i].FindControl("lblProductId"))).Text.Trim().ToString() + ",";
                }
            }
            else
            {
                string temp = string.Empty;
                string[] split = lblSelectedRecord.Text.Split(',');
                foreach (string item in split)
                {
                    if (item != ((Label)(gvBinProduct.Rows[i].FindControl("lblProductId"))).Text.Trim().ToString())
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
        chkSelAll.Focus();
    }
    protected void chkgvSelect_CheckedChanged(object sender, EventArgs e)
    {
        string empidlist = string.Empty;
        string temp = string.Empty;
        int index = ((GridViewRow)((CheckBox)sender).Parent.Parent).RowIndex;
        Label lb = (Label)gvBinProduct.Rows[index].FindControl("lblProductId");
        if (((CheckBox)gvBinProduct.Rows[index].FindControl("chkgvSelect")).Checked)
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
        ((CheckBox)gvBinProduct.Rows[index].FindControl("chkgvSelect")).Focus();
    }
    protected void ImgbtnSelectAll_Click(object sender, ImageClickEventArgs e)
    {
        DataTable dtProduct = (DataTable)Session["dtProductBin"];

        if (ViewState["Select"] == null)
        {
            ViewState["Select"] = 1;
            foreach (DataRow dr in dtProduct.Rows)
            {
                if (!lblSelectedRecord.Text.Split(',').Contains(dr["ProductId"]))
                {
                    lblSelectedRecord.Text += dr["ProductId"] + ",";
                }
            }
            for (int i = 0; i < gvBinProduct.Rows.Count; i++)
            {
                string[] split = lblSelectedRecord.Text.Split(',');
                Label lblconid = (Label)gvBinProduct.Rows[i].FindControl("lblProductId");
                for (int j = 0; j < lblSelectedRecord.Text.Split(',').Length; j++)
                {
                    if (lblSelectedRecord.Text.Split(',')[j] != "")
                    {
                        if (lblconid.Text.Trim().ToString() == lblSelectedRecord.Text.Split(',')[j].Trim().ToString())
                        {
                            ((CheckBox)gvBinProduct.Rows[i].FindControl("chkgvSelect")).Checked = true;
                        }
                    }
                }
            }
        }
        else
        {
            lblSelectedRecord.Text = "";
            DataTable dtProduct1 = (DataTable)Session["dtProductBin"];
            gvBinProduct.DataSource = dtProduct1;
            gvBinProduct.DataBind();
            AllPageCode();
            ViewState["Select"] = null;
        }
        ImgbtnSelectAll.Visible = false;

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
                    b = ObjProductMaster.RestoreProductMaster(StrCompId.ToString(), lblSelectedRecord.Text.Split(',')[j].Trim().ToString(), StrUserId.ToString(), DateTime.Now.ToString());
                }
            }
        }

        if (b != 0)
        {

            btngo_Click(null, null);
            btnbingo_Click(null, null);
            lblSelectedRecord.Text = "";
            ViewState["Select"] = null;
            DisplayMessage("Record Activated");
            btnbinRefresh_Click(null, null);
        }
        else
        {
            int fleg = 0;
            foreach (GridViewRow Gvr in gvBinProduct.Rows)
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
        imgBtnRestore.Focus();

    }

    #endregion End


    #endregion  //End

    #region Auto Complete Method

    //Country :- Start
    [System.Web.Services.WebMethodAttribute(), System.Web.Script.Services.ScriptMethodAttribute()]
    public static string[] GetCompletionList_Contry(string prefixText, int count, string contextKey)
    {
        CountryMaster ObjContryMaster = new CountryMaster();
        DataTable dtContryAll = ((DataTable)ObjContryMaster.GetCountryMaster()).DefaultView.ToTable(true, "Country_Name");
        string filtertext = "Country_Name like '" + prefixText + "%'";
        DataTable dtContry = new DataView(dtContryAll, filtertext, "", DataViewRowState.CurrentRows).ToTable();
        if (dtContry.Rows.Count == 0)
        {
            dtContry = dtContryAll.Copy();
        }
        string[] filterlist = new string[dtContry.Rows.Count];
        if (dtContry.Rows.Count > 0)
        {
            for (int i = 0; i < dtContry.Rows.Count; i++)
            {
                filterlist[i] = dtContry.Rows[i]["Country_Name"].ToString();
            }
        }
        return filterlist;
    }

    //Country :- End

    //Supplier :- Start
    [System.Web.Services.WebMethodAttribute(), System.Web.Script.Services.ScriptMethodAttribute()]
    public static string[] GetCompletionList_Supplier(string prefixText, int count, string contextKey)
    {
        Ems_ContactMaster ObjContMaster = new Ems_ContactMaster();
        Set_Suppliers ObjSupplier = new Set_Suppliers();
        DataTable dtContAll = ObjContMaster.GetContactAllData(HttpContext.Current.Session["CompId"].ToString());
        DataTable dtSupplier = ObjSupplier.GetSupplierAllTrueData(HttpContext.Current.Session["CompId"].ToString(), "1");
        DataTable dtMain = new DataTable();
        for (int i = 0; i < dtSupplier.Rows.Count; i++)
        {
            dtMain.Merge(new DataView(dtContAll, "Contact_Id='" + dtSupplier.Rows[i]["Supplier_Id"].ToString() + "'", "", DataViewRowState.CurrentRows).ToTable());
        }


        string filtertext = "Contact_Name like '" + prefixText + "%'";
        DataTable dtCon = new DataView(dtMain, filtertext, "", DataViewRowState.CurrentRows).ToTable();
        if (dtCon.Rows.Count == 0)
        {
            dtCon = dtMain.Copy();
        }
        string[] filterlist = new string[dtCon.Rows.Count];
        if (dtCon.Rows.Count > 0)
        {
            for (int i = 0; i < dtCon.Rows.Count; i++)
            {
                filterlist[i] = dtCon.Rows[i]["Contact_Name"].ToString() + "/" + dtCon.Rows[i]["Contact_Id"].ToString();
            }
        }
        return filterlist;
    }
    //Supplier :- End

    //Unit :- Start
    [System.Web.Services.WebMethodAttribute(), System.Web.Script.Services.ScriptMethodAttribute()]
    public static string[] GetCompletionList3(string prefixText, int count, string contextKey)
    {
        Inv_UnitMaster UMM = new Inv_UnitMaster();
        DataTable dt = new DataTable();
        try
        {
            dt = UMM.GetUnitMaster(HttpContext.Current.Session["CompId"].ToString());
            string filtertext = "Unit_Name like '" + prefixText + "%'";
            dt = new DataView(dt, filtertext, "", DataViewRowState.CurrentRows).ToTable();
            string[] filterlist = new string[dt.Rows.Count];
            if (dt.Rows.Count > 0)
            {
                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    filterlist[i] = "" + dt.Rows[i]["Unit_Name"].ToString() + "/" + dt.Rows[i]["Unit_Id"].ToString() + "";
                }
            }
            else
            {
                dt = UMM.GetUnitMaster("1");
                if (dt.Rows.Count > 0)
                {
                    filterlist = new string[dt.Rows.Count];
                    for (int i = 0; i < dt.Rows.Count; i++)
                    {
                        filterlist[i] = "" + dt.Rows[i]["Unit_Name"].ToString() + "/" + dt.Rows[i]["Unit_Id"].ToString() + "";
                    }
                }
            }
            return filterlist;
        }
        catch (Exception)
        {
            throw;
        }
    }
    //Unit :- End

    //Product Name :- Start
    [System.Web.Services.WebMethodAttribute(), System.Web.Script.Services.ScriptMethodAttribute()]
    public static string[] GetCompletionListProductName(string prefixText, int count, string contextKey)
    {
        Inv_ProductMaster PM = new Inv_ProductMaster();
        DataTable dt = new DataView(PM.GetProductMasterTrueAll(HttpContext.Current.Session["CompId"].ToString()), "EProductName like '" + prefixText.ToString() + "%'", "", DataViewRowState.CurrentRows).ToTable();
        string[] txt = new string[dt.Rows.Count];

        for (int i = 0; i < dt.Rows.Count; i++)
        {
            txt[i] = dt.Rows[i]["EProductName"].ToString();
        }
        return txt;
    }
    //Product Name :- End

    //Product Id :-Start
    [System.Web.Services.WebMethodAttribute(), System.Web.Script.Services.ScriptMethodAttribute()]
    public static string[] GetCompletionListProductId(string prefixText, int count, string contextKey)
    {
        Inv_ProductMaster PM = new Inv_ProductMaster();
        DataTable dt = new DataView(PM.GetProductMasterTrueAll(HttpContext.Current.Session["CompId"].ToString()), "EProductName like '" + prefixText.ToString() + "%'", "", DataViewRowState.CurrentRows).ToTable();
        string[] txt = new string[dt.Rows.Count];

        for (int i = 0; i < dt.Rows.Count; i++)
        {
            txt[i] = dt.Rows[i]["ProductCode"].ToString();
        }
        return txt;
    }
    //Product Id :- End

    #endregion


    #region //User Defind Funcation Start


    public void FillddlBrandSearch(DropDownList ddl)
    {

        DataTable dt = ObjProductBrandMaster.GetProductBrandTrueAllData(StrCompId.ToString());
        try
        {
            ddl.DataSource = dt;
            ddl.DataTextField = "Brand_Name";
            ddl.DataValueField = "PBrandId";
            ddl.DataBind();
            ddl.Items.Insert(0, "--Select One--");
            ddl.SelectedIndex = 0;
        }
        catch
        {
            ddl.Items.Insert(0, "--Select One--");
            ddl.SelectedIndex = 0;
        }
    }
    private void FillProductBrand()
    {
        DataTable dsBrand = null;
        dsBrand = ObjProductBrandMaster.GetProductBrandTrueAllData(StrCompId.ToString());
        if (dsBrand.Rows.Count > 0)
        {
            lstProductBrand.Items.Clear();
            lstSelectedProductBrand.Items.Clear();
            lstProductBrand.DataSource = dsBrand;
            lstProductBrand.DataTextField = "Brand_Name";
            lstProductBrand.DataValueField = "PBrandId";
            lstProductBrand.DataBind();
        }
        else
        {
            lstProductBrand.Items.Add("No Brands Available Here");
        }
    }
    public void FillBrand()
    {
        string strCompanyId = string.Empty;
        strCompanyId = StrCompId.ToString();
        DataTable dt = ObjBrandMaster.GetBrandMaster(StrCompId);
        try
        {
            ChkBrand.DataSource = dt;
            ChkBrand.DataTextField = "Brand_Name";
            ChkBrand.DataValueField = "Brand_Id";
            ChkBrand.DataBind();
        }
        catch
        {

        }
    }
    public void FillProductLocation()
    {

        try
        {
            DataTable dsLocation = null;
            dsLocation = ((DataTable)Session["dtLoc"]).DefaultView.ToTable(true, "LocationId", "ELocationName");
            Session["dtLoc"] = null;
            if (dsLocation.Rows.Count > 0)
            {
                lstLocation.Items.Clear();
                lstLocation.DataSource = dsLocation;
                lstLocation.DataTextField = "ELocationName";
                lstLocation.DataValueField = "LocationId";
                lstLocation.DataBind();
            }
            else
            {
                lstLocation.Items.Clear();
                lstLocation.Items.Add("No Category Available Here");
            }
        }
        catch
        {

        }
    }

    private void FillProductCategorySerch(DropDownList ddl)
    {
        DataTable dsCategory = null;
        dsCategory = ObjProductCateMaster.GetProductCategoryTrueAllData(StrCompId.ToString());
        if (dsCategory.Rows.Count > 0)
        {
            ddl.DataSource = dsCategory;
            ddl.DataTextField = "Category_Name";
            ddl.DataValueField = "Category_Id";
            ddl.DataBind();
            ddl.Items.Insert(0, "--Select One--");
            ddl.SelectedIndex = 0;
        }
        else
        {
            ddl.Items.Insert(0, "--Select One--");
            ddl.SelectedIndex = 0;
        }
    }


    private void FillProductCategory()
    {
        DataTable dsCategory = null;
        dsCategory = ObjProductCateMaster.GetProductCategoryTrueAllData(StrCompId.ToString());
        if (dsCategory.Rows.Count > 0)
        {
            lstSelectedProductBrand.Items.Clear();
            lstProductCategory.Items.Clear();
            lstProductCategory.DataSource = dsCategory;
            lstProductCategory.DataTextField = "Category_Name";
            lstProductCategory.DataValueField = "Category_Id";
            lstProductCategory.DataBind();
        }
        else
        {
            lstProductCategory.Items.Add("No Category Available Here");
        }
    }






    public string GetUnitId(string Value)
    {
        string UnitId = string.Empty;
        string[] Temp = Value.Split('/');
        if (Temp.Length == 2)
        {
            DataTable dt = ObjUnitMaster.GetUnitMasterById(StrCompId.ToString(), Temp[1].ToString());
            if (dt.Rows.Count != 0)
            {
                UnitId = dt.Rows[0]["Unit_Id"].ToString();
            }
        }
        return UnitId;

    }

    public void FillDataList(DataTable dt, int currentIndex, int SubSize)
    {
        int startRow = currentIndex * SubSize;
        int rowCounter = 0;
        DataTable dtBind = dt.Clone();

        while (rowCounter < SubSize)
        {
            if (startRow < dt.Rows.Count)
            {
                DataRow row = dtBind.NewRow();
                foreach (DataColumn dc in dt.Columns)
                {

                    row[dc.ColumnName] = dt.Rows[startRow][dc.ColumnName];
                }

                dtBind.Rows.Add(row);
                startRow++;
            }
            rowCounter++;
        }


        dtlistProduct.DataSource = dtBind;
        dtlistProduct.DataBind();

        AllPageCode();

    }



    private void FillDataListGrid()
    {
        DataTable dtproduct = ObjProductMaster.GetProductMasterTrueAll(StrCompId.ToString());


        DataTable dtproductBrandSearch = new DataTable();
        DataTable dtproductCateSearch = new DataTable();
        if (ddlbrandsearch.SelectedIndex != 0)
        {
            dtproductBrandSearch = dtproduct.Clone();
            DataTable dtProductBrand = ObjProductBrand.GetDataBrandId(StrCompId.ToString(), ddlbrandsearch.SelectedValue);
            for (int i = 0; i < dtProductBrand.Rows.Count; i++)
            {

                dtproductBrandSearch.Merge((new DataView(dtproduct, "ProductId='" + dtProductBrand.Rows[i]["ProductId"].ToString() + "'", "", DataViewRowState.CurrentRows)).ToTable());
            }

            dtproduct.Rows.Clear();
            dtproduct.Merge(dtproductBrandSearch);
        }

        if (ddlcategorysearch.SelectedIndex != 0)
        {
            dtproductCateSearch = dtproduct.Clone();
            DataTable dtProductCate = ObjProductCate.GetProductByCategoryId(StrCompId.ToString(), ddlcategorysearch.SelectedValue);
            for (int i = 0; i < dtProductCate.Rows.Count; i++)
            {

                dtproductCateSearch.Merge((new DataView(dtproduct, "ProductId='" + dtProductCate.Rows[i]["ProductId"].ToString() + "'", "", DataViewRowState.CurrentRows)).ToTable());
            }
            dtproduct.Rows.Clear();
            dtproduct.Merge(dtproductCateSearch);
        }

        Session["dtProduct"] = dtproduct;

        if (dtproduct.Rows.Count <= 9)
        {
            dtlistProduct.DataSource = dtproduct;
            dtlistProduct.DataBind();
            gvProduct.DataSource = dtproduct;
            gvProduct.DataBind();
            lnkPrev.Visible = false;
            lnkFirst.Visible = false;
            lnkNext.Visible = false;
            lnkLast.Visible = false;
        }
        else
        {
            lnkNext.Visible = true;
            lnkLast.Visible = true;
            lnkPrev.Visible = false;
            lnkFirst.Visible = false;
            FillDataList(dtproduct, Convert.ToInt32(ViewState["CurrIndex"].ToString()), Convert.ToInt32(ViewState["SubSize"].ToString()));
            if (gvProduct.Visible == true)
            {
                lnkPrev.Visible = false;
                lnkFirst.Visible = false;
                lnkNext.Visible = false;
                lnkLast.Visible = false;
                gvProduct.DataSource = dtproduct;
                gvProduct.DataBind();
            }


        }

        lblTotalRecord.Text = Resources.Attendance.Total_Records + " : " + dtproduct.Rows.Count.ToString();
        AllPageCode();
    }




    public void DisplayMessage(string str)
    {

        ScriptManager.RegisterStartupScript(this, GetType(), "", "alert('" + str + "');", true);

    }
    public void Reset()
    {
        txtProductId.Text = "";
        txtModelNo.Text = "";
        txtPartNo.Text = "";
        txtAlterId1.Text = "";
        txtAlterId2.Text = "";
        txtAlterId3.Text = "";
        txtCostPrice.Text = "";
        txtDamageQty.Text = "";
        txtDepth.Text = "";
        txtDesc.Content = "";
        txtDiscount.Text = "";
        txtEProductName.Text = "";
        txtExpQty.Text = "";
        txtHasCode.Text = "";
        txtHeight.Text = "";
        txtLenth.Text = "";
        txtLProductName.Text = "";
        txtMaxQty.Text = "";
        txtMiniQty.Text = "";

        txtProductColor.Text = "";
        txtProductCountry.Text = "";
        txtProductSupplierCode.Text = "";
        txtProductUnit.Text = "";
        txtprofit.Text = "";
        txtReorderQty.Text = "";
        txtSalesPrice1.Text = "";
        txtSalesPrice2.Text = "";
        txtSalesPrice3.Text = "";
        txtSuppliers.Text = "";
        txtWholesalePrice.Text = "";
        ddlItemType.SelectedIndex = 0;
        ddlItypeType_SelectedIndexChanged(null, null);
        ddlMaintainStock.SelectedIndex = 0;
        ddlstatus.SelectedIndex = 0;
        ddlTypeOfBatchNo.SelectedIndex = 0;
        ddlMaintainStock_SelectedIndexChanged(null, null);
        lstLocationSelect.Items.Clear();
        lstSelectedProductBrand.Items.Clear();
        lstSelectProductCategory.Items.Clear();
        FillBrand();
        FillProductCategory();
        FillProductBrand();
        btnGetlocation_Click(null, null);
        GridProductSupplierCode.DataSource = null;
        GridProductSupplierCode.DataBind();
        Session["dtProductSupplierCode"] = null;
        imgProduct.ImageUrl = null;
        hdnProductId.Value = "";
        txtProductId.Enabled = true;
        btnNew.Text = Resources.Attendance.New;
        ChkHasBatchNo.Checked = false;
        ChkHasSerialNo.Checked = false;
        FillbinDataListGrid();
        TabContainer1.ActiveTabIndex = 0;
        lblSelectedRecord.Text = "";
        ViewState["Select"] = null;
        Session["Image"] = null;
    }

    public void ProductEdit()
    {
        try
        {

            txtProductId.Enabled = false;
            TabContainer1.ActiveTabIndex = 0;
            btnNew_Click(null, null);
            btnNew.Text = Resources.Attendance.Edit;
            txtProductId.Text = hdnProductId.Value.ToString();
            DataTable dtProduct = ObjProductMaster.GetProductMasterById(StrCompId.ToString(), hdnProductId.Value);
            if (dtProduct.Rows.Count != 0)
            {
                imgProduct.ImageUrl = "~/Handler.ashx?ImID=" + hdnProductId.Value.ToString() + "";

                txtProductId.Text = dtProduct.Rows[0]["ProductCode"].ToString();
                txtEProductName.Text = dtProduct.Rows[0]["EProductName"].ToString();
                txtLProductName.Text = dtProduct.Rows[0]["LProductName"].ToString();
                txtAlterId1.Text = dtProduct.Rows[0]["AlternateId1"].ToString();
                txtAlterId2.Text = dtProduct.Rows[0]["AlternateId2"].ToString();
                txtAlterId3.Text = dtProduct.Rows[0]["AlternateId3"].ToString();
                txtCostPrice.Text = dtProduct.Rows[0]["CostPrice"].ToString();
                txtDamageQty.Text = dtProduct.Rows[0]["DamageQty"].ToString();
                txtDepth.Text = dtProduct.Rows[0]["DimDepth"].ToString();
                txtDesc.Content = dtProduct.Rows[0]["Description"].ToString();
                txtDiscount.Text = dtProduct.Rows[0]["Discount"].ToString();
                txtExpQty.Text = dtProduct.Rows[0]["ExpiredQty"].ToString();
                txtHasCode.Text = dtProduct.Rows[0]["HScode"].ToString();
                txtHeight.Text = dtProduct.Rows[0]["DimHieght"].ToString();
                txtLenth.Text = dtProduct.Rows[0]["DimLenth"].ToString();
                txtMaxQty.Text = dtProduct.Rows[0]["MaximumQty"].ToString();
                txtMiniQty.Text = dtProduct.Rows[0]["MinimumQty"].ToString();
                txtModelNo.Text = dtProduct.Rows[0]["ModelNo"].ToString();
                txtPartNo.Text = dtProduct.Rows[0]["PartNo"].ToString();
                txtProductColor.Text = dtProduct.Rows[0]["ProductColor"].ToString();
                try
                {
                    txtProductCountry.Text = new DataView(ObjSysCountryMaster.GetCountryMaster(), "Country_Id='" + dtProduct.Rows[0]["CountryID"].ToString().Trim() + "'", "", DataViewRowState.CurrentRows).ToTable().Rows[0]["Country_Name"].ToString();
                }
                catch
                {

                }
                try
                {
                    txtProductUnit.Text = ObjUnitMaster.GetUnitMasterById(StrCompId.ToString(), dtProduct.Rows[0]["UnitId"].ToString()).Rows[0]["Unit_Name"].ToString() + "/" + dtProduct.Rows[0]["UnitId"].ToString();
                }
                catch
                {

                }
                txtprofit.Text = dtProduct.Rows[0]["Profit"].ToString();
                txtSalesPrice1.Text = dtProduct.Rows[0]["SalesPrice1"].ToString();
                txtSalesPrice2.Text = dtProduct.Rows[0]["SalesPrice2"].ToString();
                txtSalesPrice3.Text = dtProduct.Rows[0]["SalesPrice3"].ToString();
                txtWholesalePrice.Text = dtProduct.Rows[0]["WSalePrice"].ToString();


                ddlItemType.SelectedValue = dtProduct.Rows[0]["ItemType"].ToString();
                ddlItypeType_SelectedIndexChanged(null, null);
                ddlMaintainStock.SelectedValue = dtProduct.Rows[0]["MaintainStock"].ToString();
                ddlMaintainStock_SelectedIndexChanged(null, null);
                txtReorderQty.Text = dtProduct.Rows[0]["ReorderQty"].ToString();

                ddlstatus.SelectedValue = "True";
                ddlTypeOfBatchNo.SelectedValue = dtProduct.Rows[0]["TypeOfBatchNo"].ToString();

                ChkHasBatchNo.Checked = Convert.ToBoolean(dtProduct.Rows[0]["HasBatchNo"].ToString());
                ChkHasSerialNo.Checked = Convert.ToBoolean(dtProduct.Rows[0]["HasSerialNo"].ToString());

                DataTable dtProductBrand = ObjProductBrand.GetDataProductId(StrCompId.ToString(), hdnProductId.Value);

                for (int i = 0; i < dtProductBrand.Rows.Count; i++)
                {
                    ListItem li = new ListItem();
                    li.Value = dtProductBrand.Rows[i]["PBrandId"].ToString();
                    li.Text = ObjProductBrandMaster.GetProductBrandByPBrandId(StrCompId.ToString(), dtProductBrand.Rows[i]["PBrandId"].ToString()).Rows[0]["Brand_Name"].ToString();
                    lstSelectedProductBrand.Items.Add(li);
                    lstProductBrand.Items.Remove(li);

                }
                DataTable dtProductCate = ObjProductCate.GetDataProductId(StrCompId.ToString(), hdnProductId.Value);
                for (int i = 0; i < dtProductCate.Rows.Count; i++)
                {
                    ListItem li = new ListItem();
                    li.Value = dtProductCate.Rows[i]["CategoryId"].ToString();
                    li.Text = ObjProductCateMaster.GetProductCategoryByCategoryId(StrCompId.ToString(), dtProductCate.Rows[i]["CategoryId"].ToString()).Rows[0]["Category_Name"].ToString();
                    lstSelectProductCategory.Items.Add(li);
                    lstProductCategory.Items.Remove(li);
                }

                DataTable dtCompBrand = ObjCompanyBrand.GetDataProductId(StrCompId.ToString(), hdnProductId.Value);
                for (int i = 0; i < dtCompBrand.Rows.Count; i++)
                {
                    foreach (ListItem chklst in ChkBrand.Items)
                    {
                        if (dtCompBrand.Rows[i]["BrandId"].ToString() == chklst.Value.ToString())
                        {
                            chklst.Selected = true;
                        }
                    }

                }
                btnGetlocation_Click(null, null);
                DataTable dtLocation = ObjProductLocation.GetProductLocationByProductId(StrCompId.ToString(), hdnProductId.Value);
                for (int i = 0; i < dtLocation.Rows.Count; i++)
                {
                    ListItem li = new ListItem();
                    li.Value = dtLocation.Rows[i]["LocationId"].ToString();
                    li.Text = ObjLocMaster.GetLocationMasterById(StrCompId.ToString(), dtLocation.Rows[i]["LocationId"].ToString()).Rows[0]["Location_Name"].ToString();
                    lstLocationSelect.Items.Add(li);
                    lstLocation.Items.Remove(li);
                }

                DataTable dtSupplier = ObjProductSupplier.GetProductSuppliersByProductId(StrCompId.ToString(), "1", hdnProductId.Value);
                dtSupplier.Columns.Add("Name");

                DataTable dt = new DataTable();
                dt.Columns.Add("Supplier_Id");
                dt.Columns.Add("Name");
                dt.Columns.Add("ProductSupplierCode");
                for (int i = 0; i < dtSupplier.Rows.Count; i++)
                {
                    try
                    {

                        dtSupplier.Rows[i]["Name"] = ObjContactMaster.GetContactByContactId(StrCompId.ToString(), dtSupplier.Rows[i]["Supplier_Id"].ToString()).Rows[0]["Contact_Name"].ToString();
                    }
                    catch
                    {

                    }
                    dt.Rows.Add(i);
                    dt.Rows[i]["Supplier_Id"] = dtSupplier.Rows[i]["Supplier_Id"].ToString();
                    dt.Rows[i]["Name"] = dtSupplier.Rows[i]["Name"].ToString();
                    dt.Rows[i]["ProductSupplierCode"] = dtSupplier.Rows[i]["ProductSupplierCode"].ToString();
                }
                Session["dtProductSupplierCode"] = dt;
                GridProductSupplierCode.DataSource = dtSupplier;
                GridProductSupplierCode.DataBind();

            }


        }
        catch
        {

        }


    }
    public string GetItemType(string IT)
    {
        string retval = string.Empty;
        if (IT == "A")
        {
            retval = "Assemble";
        }
        if (IT == "K")
        {
            retval = "KIT";
        }
        if (IT == "NS")
        {
            retval = "Non Stockable";
        }
        if (IT == "S")
        {
            retval = "Stockable";
        }
        return retval;

    }


    public string GetUnitName(string UnitId)
    {
        string ProductUnit = string.Empty;
        if (UnitId.ToString() != "")
        {
            DataTable dtUnit = ObjUnitMaster.GetUnitMasterById(StrCompId.ToString(), UnitId.ToString());
            if (dtUnit.Rows.Count != 0)
            {
                ProductUnit = dtUnit.Rows[0]["Unit_Name"].ToString();
            }
            return ProductUnit;
        }
        else
        {
            return ProductUnit = "No Unit";
        }
    }

    #region For Bin
    public void FillBinDataList(DataTable dt, int currentIndex, int SubSize)
    {
        int startRow = currentIndex * SubSize;
        int rowCounter = 0;
        DataTable dtBind = dt.Clone();

        while (rowCounter < SubSize)
        {
            if (startRow < dt.Rows.Count)
            {
                DataRow row = dtBind.NewRow();
                foreach (DataColumn dc in dt.Columns)
                {

                    row[dc.ColumnName] = dt.Rows[startRow][dc.ColumnName];
                }

                dtBind.Rows.Add(row);
                startRow++;
            }
            rowCounter++;
        }
        dtlistbinProduct.DataSource = dtBind;
        dtlistbinProduct.DataBind();
        AllPageCode();
        if (gvBinProduct.Visible != true)
        {
            imgBtnRestore.Visible = false;
            ImgbtnSelectAll.Visible = false;
        }

    }
    private void FillbinDataListGrid()
    {
        DataTable dtproduct = ObjProductMaster.GetProductMasterFalseAll(StrCompId.ToString());

        DataTable dtproductBrandSearch = new DataTable();
        DataTable dtproductCateSearch = new DataTable();
        if (ddlBinBrandSearch.SelectedIndex != 0)
        {
            dtproductBrandSearch = dtproduct.Clone();
            DataTable dtProductBrand = ObjProductBrand.GetDataBrandId(StrCompId.ToString(), ddlBinBrandSearch.SelectedValue);
            for (int i = 0; i < dtProductBrand.Rows.Count; i++)
            {

                dtproductBrandSearch.Merge((new DataView(dtproduct, "ProductId='" + dtProductBrand.Rows[i]["ProductId"].ToString() + "'", "", DataViewRowState.CurrentRows)).ToTable());
            }

            dtproduct.Rows.Clear();
            dtproduct.Merge(dtproductBrandSearch);
        }

        if (ddlBinCategorySearch.SelectedIndex != 0)
        {
            dtproductCateSearch = dtproduct.Clone();
            DataTable dtProductCate = ObjProductCate.GetProductByCategoryId(StrCompId.ToString(), ddlBinCategorySearch.SelectedValue);
            for (int i = 0; i < dtProductCate.Rows.Count; i++)
            {

                dtproductCateSearch.Merge((new DataView(dtproduct, "ProductId='" + dtProductCate.Rows[i]["ProductId"].ToString() + "'", "", DataViewRowState.CurrentRows)).ToTable());
            }
            dtproduct.Rows.Clear();
            dtproduct.Merge(dtproductCateSearch);
        }

        Session["dtProductBin"] = dtproduct;

        if (dtproduct.Rows.Count <= 9)
        {
            dtlistbinProduct.DataSource = dtproduct;
            dtlistbinProduct.DataBind();
            gvBinProduct.DataSource = dtproduct;
            gvBinProduct.DataBind();
            lnkbinPrev.Visible = false;
            lnkbinFirst.Visible = false;
            lnkbinNext.Visible = false;
            lnkbinLast.Visible = false;
        }
        else
        {
            lnkbinNext.Visible = true;
            lnkbinLast.Visible = true;
            lnkbinPrev.Visible = false;
            lnkbinFirst.Visible = false;
            FillBinDataList(dtproduct, Convert.ToInt32(ViewState["CurrIndexbin"].ToString()), Convert.ToInt32(ViewState["SubSizebin"].ToString()));
            if (gvBinProduct.Visible == true)
            {
                lnkbinPrev.Visible = false;
                lnkbinFirst.Visible = false;
                lnkbinNext.Visible = false;
                lnkbinLast.Visible = false;
                gvBinProduct.DataSource = dtproduct;
                gvBinProduct.DataBind();
            }
        }

        lblbinTotalRecord.Text = Resources.Attendance.Total_Records + " : " + dtproduct.Rows.Count.ToString();

        if (gvBinProduct.Visible == true)
        {
            if (dtproduct.Rows.Count != 0)
            {
                AllPageCode();
            }
            else
            {
                imgBtnRestore.Visible = false;
                ImgbtnSelectAll.Visible = false;
            }


        }
        else
        {
            imgBtnRestore.Visible = false;
            ImgbtnSelectAll.Visible = false;

        }

    }
    #endregion
    #endregion  //End Funcation


}
