using System;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using DevExpress.XtraReports.UI;

/// <summary>
/// Summary description for XtraReport1
/// </summary>
public class XtraReport1 : DevExpress.XtraReports.UI.XtraReport
{
	private DevExpress.XtraReports.UI.DetailBand Detail;
	private DevExpress.XtraReports.UI.TopMarginBand TopMargin;
	private DevExpress.XtraReports.UI.BottomMarginBand BottomMargin;
    private Settlement_Payment_DataSet settlement_Payment_DataSet1;
    private XRPictureBox xrPictureBox1;
    private XRLabel xrLabel1;
    private XRLabel xrLabel2;
    private XRLabel xrLabel3;
    private XRLabel xrLabel4;
    private XRLabel xrLabel5;
    private XRLabel xrLabel6;
    private XRLabel xrLabel7;
    private GroupHeaderBand GroupHeader1;
    private XRTable xrTable3;
    private XRTableRow xrTableRow4;
    private XRTableCell xrTableCell8;
    private XRTableCell xrTableCell9;
    private XRTable xrTable4;
    private XRTableRow xrTableRow5;
    private XRTableCell xrTableCell11;
    private XRTableCell xrllblamt;
    private GroupFooterBand GroupFooter1;
    private GroupHeaderBand GroupHeader2;
    private XRLabel xrLabel8;
    private XRLabel xrLabel10;
    private XRLabel xrLabel11;
    private GroupFooterBand GroupFooter2;
    private XRLabel xrLabel13;
    private XRLabel xrllbltotalamt;
    private XRTable xrTable1;
    private XRTableRow xrTableRow2;
    private XRTableCell xrllbltype;
    private XRLabel xrLabel12;
    private XRLine xrLine3;
    private XRLine xrLine2;
    private XRLine xrLine4;
    private XRLine xrLine1;
	/// <summary>
	/// Required designer variable.
	/// </summary>
	private System.ComponentModel.IContainer components = null;

	public XtraReport1()
	{
		InitializeComponent();
		//
		// TODO: Add constructor logic here
		//
	}


    public void setcompanyname(string companyname)
    {
        xrLabel1.Text = companyname;
    }

    public void setmonth(string month)
    {
        xrLabel4.Text = month;
    }
    public void setyear(string year)
    {
        xrLabel5.Text = year;
    }


    public void settitle(string TitleName)
    {
        xrLabel3.Text = TitleName;
    }
    public void setcompanyAddress(string Address)
    {
        xrLabel2.Text = Address;
    }
    public void setimage(string url)
    {
        xrPictureBox1.ImageUrl = url;
    }
    
    
    //public void Grossamt(string grossamt)
    //{
    //    double Sub = 0;
    //    Sub = Convert.ToDouble(xrLabel9.Summary.GetResult()) - Convert.ToDouble(xrLabel9.Summary.GetResult());
    //    //Sub = Convert.ToDouble(xrLabel9.Text) - Convert.ToDouble(xrLabel9.Text);
    //    xrLabel12.Text = Sub.ToString();
    //}

    
	
	/// <summary> 
	/// Clean up any resources being used.
	/// </summary>
	/// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
	protected override void Dispose(bool disposing) {
		if (disposing && (components != null)) {
			components.Dispose();
		}
		base.Dispose(disposing);
	}

	#region Designer generated code

	/// <summary>
	/// Required method for Designer support - do not modify
	/// the contents of this method with the code editor.
	/// </summary>
	private void InitializeComponent() {
        string resourceFileName = "XtraReport1.resx";
        DevExpress.XtraReports.UI.XRSummary xrSummary1 = new DevExpress.XtraReports.UI.XRSummary();
        this.Detail = new DevExpress.XtraReports.UI.DetailBand();
        this.xrTable4 = new DevExpress.XtraReports.UI.XRTable();
        this.xrTableRow5 = new DevExpress.XtraReports.UI.XRTableRow();
        this.xrTableCell11 = new DevExpress.XtraReports.UI.XRTableCell();
        this.xrllblamt = new DevExpress.XtraReports.UI.XRTableCell();
        this.TopMargin = new DevExpress.XtraReports.UI.TopMarginBand();
        this.xrLine1 = new DevExpress.XtraReports.UI.XRLine();
        this.xrLabel7 = new DevExpress.XtraReports.UI.XRLabel();
        this.xrLabel6 = new DevExpress.XtraReports.UI.XRLabel();
        this.xrLabel5 = new DevExpress.XtraReports.UI.XRLabel();
        this.xrLabel4 = new DevExpress.XtraReports.UI.XRLabel();
        this.xrLabel3 = new DevExpress.XtraReports.UI.XRLabel();
        this.xrLabel2 = new DevExpress.XtraReports.UI.XRLabel();
        this.xrLabel1 = new DevExpress.XtraReports.UI.XRLabel();
        this.xrPictureBox1 = new DevExpress.XtraReports.UI.XRPictureBox();
        this.BottomMargin = new DevExpress.XtraReports.UI.BottomMarginBand();
        this.settlement_Payment_DataSet1 = new Settlement_Payment_DataSet();
        this.GroupHeader1 = new DevExpress.XtraReports.UI.GroupHeaderBand();
        this.xrLine4 = new DevExpress.XtraReports.UI.XRLine();
        this.xrTable3 = new DevExpress.XtraReports.UI.XRTable();
        this.xrTableRow4 = new DevExpress.XtraReports.UI.XRTableRow();
        this.xrTableCell8 = new DevExpress.XtraReports.UI.XRTableCell();
        this.xrTableCell9 = new DevExpress.XtraReports.UI.XRTableCell();
        this.xrTable1 = new DevExpress.XtraReports.UI.XRTable();
        this.xrTableRow2 = new DevExpress.XtraReports.UI.XRTableRow();
        this.xrllbltype = new DevExpress.XtraReports.UI.XRTableCell();
        this.GroupFooter1 = new DevExpress.XtraReports.UI.GroupFooterBand();
        this.xrLine3 = new DevExpress.XtraReports.UI.XRLine();
        this.xrLabel12 = new DevExpress.XtraReports.UI.XRLabel();
        this.xrllbltotalamt = new DevExpress.XtraReports.UI.XRLabel();
        this.GroupHeader2 = new DevExpress.XtraReports.UI.GroupHeaderBand();
        this.xrLine2 = new DevExpress.XtraReports.UI.XRLine();
        this.xrLabel13 = new DevExpress.XtraReports.UI.XRLabel();
        this.xrLabel11 = new DevExpress.XtraReports.UI.XRLabel();
        this.xrLabel10 = new DevExpress.XtraReports.UI.XRLabel();
        this.xrLabel8 = new DevExpress.XtraReports.UI.XRLabel();
        this.GroupFooter2 = new DevExpress.XtraReports.UI.GroupFooterBand();
        ((System.ComponentModel.ISupportInitialize)(this.xrTable4)).BeginInit();
        ((System.ComponentModel.ISupportInitialize)(this.settlement_Payment_DataSet1)).BeginInit();
        ((System.ComponentModel.ISupportInitialize)(this.xrTable3)).BeginInit();
        ((System.ComponentModel.ISupportInitialize)(this.xrTable1)).BeginInit();
        ((System.ComponentModel.ISupportInitialize)(this)).BeginInit();
        // 
        // Detail
        // 
        this.Detail.Controls.AddRange(new DevExpress.XtraReports.UI.XRControl[] {
            this.xrTable4});
        this.Detail.HeightF = 11.45833F;
        this.Detail.Name = "Detail";
        this.Detail.Padding = new DevExpress.XtraPrinting.PaddingInfo(0, 0, 0, 0, 100F);
        this.Detail.TextAlignment = DevExpress.XtraPrinting.TextAlignment.TopLeft;
        // 
        // xrTable4
        // 
        this.xrTable4.Borders = ((DevExpress.XtraPrinting.BorderSide)((((DevExpress.XtraPrinting.BorderSide.Left | DevExpress.XtraPrinting.BorderSide.Top)
                    | DevExpress.XtraPrinting.BorderSide.Right)
                    | DevExpress.XtraPrinting.BorderSide.Bottom)));
        this.xrTable4.Font = new System.Drawing.Font("Verdana", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
        this.xrTable4.LocationFloat = new DevExpress.Utils.PointFloat(0F, 0F);
        this.xrTable4.Name = "xrTable4";
        this.xrTable4.Rows.AddRange(new DevExpress.XtraReports.UI.XRTableRow[] {
            this.xrTableRow5});
        this.xrTable4.SizeF = new System.Drawing.SizeF(649.9999F, 11.45833F);
        this.xrTable4.StylePriority.UseBorders = false;
        this.xrTable4.StylePriority.UseFont = false;
        // 
        // xrTableRow5
        // 
        this.xrTableRow5.Cells.AddRange(new DevExpress.XtraReports.UI.XRTableCell[] {
            this.xrTableCell11,
            this.xrllblamt});
        this.xrTableRow5.Name = "xrTableRow5";
        this.xrTableRow5.Weight = 1;
        // 
        // xrTableCell11
        // 
        this.xrTableCell11.Borders = DevExpress.XtraPrinting.BorderSide.None;
        this.xrTableCell11.DataBindings.AddRange(new DevExpress.XtraReports.UI.XRBinding[] {
            new DevExpress.XtraReports.UI.XRBinding("Text", null, "settlementdt.Description")});
        this.xrTableCell11.Name = "xrTableCell11";
        this.xrTableCell11.StylePriority.UseBorders = false;
        this.xrTableCell11.Text = "xrTableCell11";
        this.xrTableCell11.Weight = 4.5547448549779039;
        // 
        // xrllblamt
        // 
        this.xrllblamt.Borders = DevExpress.XtraPrinting.BorderSide.None;
        this.xrllblamt.DataBindings.AddRange(new DevExpress.XtraReports.UI.XRBinding[] {
            new DevExpress.XtraReports.UI.XRBinding("Text", null, "settlementdt.Amount")});
        this.xrllblamt.Name = "xrllblamt";
        this.xrllblamt.StylePriority.UseBorders = false;
        this.xrllblamt.Weight = 1.9452544886979188;
        // 
        // TopMargin
        // 
        this.TopMargin.Controls.AddRange(new DevExpress.XtraReports.UI.XRControl[] {
            this.xrLine1,
            this.xrLabel7,
            this.xrLabel6,
            this.xrLabel5,
            this.xrLabel4,
            this.xrLabel3,
            this.xrLabel2,
            this.xrLabel1,
            this.xrPictureBox1});
        this.TopMargin.HeightF = 91F;
        this.TopMargin.Name = "TopMargin";
        this.TopMargin.Padding = new DevExpress.XtraPrinting.PaddingInfo(0, 0, 0, 0, 100F);
        this.TopMargin.TextAlignment = DevExpress.XtraPrinting.TextAlignment.TopLeft;
        // 
        // xrLine1
        // 
        this.xrLine1.LocationFloat = new DevExpress.Utils.PointFloat(0F, 85.37498F);
        this.xrLine1.Name = "xrLine1";
        this.xrLine1.SizeF = new System.Drawing.SizeF(649.9999F, 2F);
        // 
        // xrLabel7
        // 
        this.xrLabel7.Borders = DevExpress.XtraPrinting.BorderSide.None;
        this.xrLabel7.DataBindings.AddRange(new DevExpress.XtraReports.UI.XRBinding[] {
            new DevExpress.XtraReports.UI.XRBinding("Text", null, "settlementdt.Year")});
        this.xrLabel7.LocationFloat = new DevExpress.Utils.PointFloat(540F, 60.66669F);
        this.xrLabel7.Name = "xrLabel7";
        this.xrLabel7.Padding = new DevExpress.XtraPrinting.PaddingInfo(2, 2, 0, 0, 100F);
        this.xrLabel7.SizeF = new System.Drawing.SizeF(100F, 24.70829F);
        this.xrLabel7.StylePriority.UseBorders = false;
        this.xrLabel7.Text = "xrLabel7";
        // 
        // xrLabel6
        // 
        this.xrLabel6.Borders = DevExpress.XtraPrinting.BorderSide.None;
        this.xrLabel6.DataBindings.AddRange(new DevExpress.XtraReports.UI.XRBinding[] {
            new DevExpress.XtraReports.UI.XRBinding("Text", null, "settlementdt.Month")});
        this.xrLabel6.LocationFloat = new DevExpress.Utils.PointFloat(110.0695F, 60.66669F);
        this.xrLabel6.Name = "xrLabel6";
        this.xrLabel6.Padding = new DevExpress.XtraPrinting.PaddingInfo(2, 2, 0, 0, 100F);
        this.xrLabel6.SizeF = new System.Drawing.SizeF(100F, 24.70829F);
        this.xrLabel6.StylePriority.UseBorders = false;
        this.xrLabel6.Text = "[Month]";
        // 
        // xrLabel5
        // 
        this.xrLabel5.Borders = DevExpress.XtraPrinting.BorderSide.None;
        this.xrLabel5.Font = new System.Drawing.Font("Verdana", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
        this.xrLabel5.LocationFloat = new DevExpress.Utils.PointFloat(402.0833F, 60.66669F);
        this.xrLabel5.Name = "xrLabel5";
        this.xrLabel5.Padding = new DevExpress.XtraPrinting.PaddingInfo(2, 2, 0, 0, 100F);
        this.xrLabel5.SizeF = new System.Drawing.SizeF(100F, 24.70829F);
        this.xrLabel5.StylePriority.UseBorders = false;
        this.xrLabel5.StylePriority.UseFont = false;
        this.xrLabel5.Text = "xrLabel5";
        // 
        // xrLabel4
        // 
        this.xrLabel4.Borders = DevExpress.XtraPrinting.BorderSide.None;
        this.xrLabel4.Font = new System.Drawing.Font("Verdana", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
        this.xrLabel4.LocationFloat = new DevExpress.Utils.PointFloat(0.0001907349F, 60.66669F);
        this.xrLabel4.Name = "xrLabel4";
        this.xrLabel4.Padding = new DevExpress.XtraPrinting.PaddingInfo(2, 2, 0, 0, 100F);
        this.xrLabel4.SizeF = new System.Drawing.SizeF(100F, 20.54164F);
        this.xrLabel4.StylePriority.UseBorders = false;
        this.xrLabel4.StylePriority.UseFont = false;
        this.xrLabel4.Text = "xrLabel4";
        // 
        // xrLabel3
        // 
        this.xrLabel3.Borders = DevExpress.XtraPrinting.BorderSide.None;
        this.xrLabel3.Font = new System.Drawing.Font("Verdana", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
        this.xrLabel3.LocationFloat = new DevExpress.Utils.PointFloat(196.0996F, 33.00001F);
        this.xrLabel3.Name = "xrLabel3";
        this.xrLabel3.Padding = new DevExpress.XtraPrinting.PaddingInfo(2, 2, 0, 0, 100F);
        this.xrLabel3.SizeF = new System.Drawing.SizeF(259.3749F, 12.5F);
        this.xrLabel3.StylePriority.UseBorders = false;
        this.xrLabel3.StylePriority.UseFont = false;
        this.xrLabel3.StylePriority.UseTextAlignment = false;
        this.xrLabel3.Text = "xrLabel3";
        this.xrLabel3.TextAlignment = DevExpress.XtraPrinting.TextAlignment.TopCenter;
        // 
        // xrLabel2
        // 
        this.xrLabel2.Borders = DevExpress.XtraPrinting.BorderSide.None;
        this.xrLabel2.Font = new System.Drawing.Font("Verdana", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
        this.xrLabel2.LocationFloat = new DevExpress.Utils.PointFloat(0F, 33.00001F);
        this.xrLabel2.Name = "xrLabel2";
        this.xrLabel2.Padding = new DevExpress.XtraPrinting.PaddingInfo(2, 2, 0, 0, 100F);
        this.xrLabel2.SizeF = new System.Drawing.SizeF(190F, 12.5F);
        this.xrLabel2.StylePriority.UseBorders = false;
        this.xrLabel2.StylePriority.UseFont = false;
        this.xrLabel2.Text = "xrLabel2";
        // 
        // xrLabel1
        // 
        this.xrLabel1.Borders = DevExpress.XtraPrinting.BorderSide.None;
        this.xrLabel1.Font = new System.Drawing.Font("Verdana", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
        this.xrLabel1.LocationFloat = new DevExpress.Utils.PointFloat(0F, 10.00001F);
        this.xrLabel1.Name = "xrLabel1";
        this.xrLabel1.Padding = new DevExpress.XtraPrinting.PaddingInfo(2, 2, 0, 0, 100F);
        this.xrLabel1.SizeF = new System.Drawing.SizeF(190F, 35.5F);
        this.xrLabel1.StylePriority.UseBorders = false;
        this.xrLabel1.StylePriority.UseFont = false;
        this.xrLabel1.Text = "xrLabel1";
        // 
        // xrPictureBox1
        // 
        this.xrPictureBox1.Borders = DevExpress.XtraPrinting.BorderSide.None;
        this.xrPictureBox1.LocationFloat = new DevExpress.Utils.PointFloat(550F, 0F);
        this.xrPictureBox1.Name = "xrPictureBox1";
        this.xrPictureBox1.SizeF = new System.Drawing.SizeF(100F, 58.41666F);
        this.xrPictureBox1.Sizing = DevExpress.XtraPrinting.ImageSizeMode.StretchImage;
        this.xrPictureBox1.StylePriority.UseBorders = false;
        // 
        // BottomMargin
        // 
        this.BottomMargin.HeightF = 16.83327F;
        this.BottomMargin.Name = "BottomMargin";
        this.BottomMargin.Padding = new DevExpress.XtraPrinting.PaddingInfo(0, 0, 0, 0, 100F);
        this.BottomMargin.TextAlignment = DevExpress.XtraPrinting.TextAlignment.TopLeft;
        // 
        // settlement_Payment_DataSet1
        // 
        this.settlement_Payment_DataSet1.DataSetName = "Settlement_Payment_DataSet";
        this.settlement_Payment_DataSet1.SchemaSerializationMode = System.Data.SchemaSerializationMode.IncludeSchema;
        // 
        // GroupHeader1
        // 
        this.GroupHeader1.Controls.AddRange(new DevExpress.XtraReports.UI.XRControl[] {
            this.xrLine4,
            this.xrTable3,
            this.xrTable1});
        this.GroupHeader1.GroupFields.AddRange(new DevExpress.XtraReports.UI.GroupField[] {
            new DevExpress.XtraReports.UI.GroupField("Type", DevExpress.XtraReports.UI.XRColumnSortOrder.Ascending)});
        this.GroupHeader1.HeightF = 32.37502F;
        this.GroupHeader1.Name = "GroupHeader1";
        // 
        // xrLine4
        // 
        this.xrLine4.LocationFloat = new DevExpress.Utils.PointFloat(0F, 29.16666F);
        this.xrLine4.Name = "xrLine4";
        this.xrLine4.SizeF = new System.Drawing.SizeF(649.9999F, 2F);
        // 
        // xrTable3
        // 
        this.xrTable3.Borders = DevExpress.XtraPrinting.BorderSide.None;
        this.xrTable3.Font = new System.Drawing.Font("Verdana", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
        this.xrTable3.LocationFloat = new DevExpress.Utils.PointFloat(0.0001907349F, 16.66666F);
        this.xrTable3.Name = "xrTable3";
        this.xrTable3.Rows.AddRange(new DevExpress.XtraReports.UI.XRTableRow[] {
            this.xrTableRow4});
        this.xrTable3.SizeF = new System.Drawing.SizeF(649.9998F, 12.5F);
        this.xrTable3.StylePriority.UseBorders = false;
        this.xrTable3.StylePriority.UseFont = false;
        // 
        // xrTableRow4
        // 
        this.xrTableRow4.Cells.AddRange(new DevExpress.XtraReports.UI.XRTableCell[] {
            this.xrTableCell8,
            this.xrTableCell9});
        this.xrTableRow4.Name = "xrTableRow4";
        this.xrTableRow4.Weight = 1;
        // 
        // xrTableCell8
        // 
        this.xrTableCell8.Font = new System.Drawing.Font("Verdana", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
        this.xrTableCell8.Name = "xrTableCell8";
        this.xrTableCell8.StylePriority.UseFont = false;
        this.xrTableCell8.Text = "Naration";
        this.xrTableCell8.Weight = 2.241245109018557;
        // 
        // xrTableCell9
        // 
        this.xrTableCell9.Font = new System.Drawing.Font("Verdana", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
        this.xrTableCell9.Name = "xrTableCell9";
        this.xrTableCell9.StylePriority.UseFont = false;
        this.xrTableCell9.Text = "Amount";
        this.xrTableCell9.Weight = 0.95719818997284412;
        // 
        // xrTable1
        // 
        this.xrTable1.Borders = DevExpress.XtraPrinting.BorderSide.None;
        this.xrTable1.Font = new System.Drawing.Font("Verdana", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
        this.xrTable1.LocationFloat = new DevExpress.Utils.PointFloat(0F, 0F);
        this.xrTable1.Name = "xrTable1";
        this.xrTable1.Rows.AddRange(new DevExpress.XtraReports.UI.XRTableRow[] {
            this.xrTableRow2});
        this.xrTable1.SizeF = new System.Drawing.SizeF(251.0417F, 16.66666F);
        this.xrTable1.StylePriority.UseBorders = false;
        this.xrTable1.StylePriority.UseFont = false;
        // 
        // xrTableRow2
        // 
        this.xrTableRow2.Cells.AddRange(new DevExpress.XtraReports.UI.XRTableCell[] {
            this.xrllbltype});
        this.xrTableRow2.Name = "xrTableRow2";
        this.xrTableRow2.Weight = 1;
        // 
        // xrllbltype
        // 
        this.xrllbltype.DataBindings.AddRange(new DevExpress.XtraReports.UI.XRBinding[] {
            new DevExpress.XtraReports.UI.XRBinding("Text", null, "settlementdt.Type")});
        this.xrllbltype.Font = new System.Drawing.Font("Verdana", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
        this.xrllbltype.Name = "xrllbltype";
        this.xrllbltype.StylePriority.UseFont = false;
        this.xrllbltype.Weight = 2;
        // 
        // GroupFooter1
        // 
        this.GroupFooter1.Controls.AddRange(new DevExpress.XtraReports.UI.XRControl[] {
            this.xrLine3,
            this.xrLabel12,
            this.xrllbltotalamt});
        this.GroupFooter1.HeightF = 21.54166F;
        this.GroupFooter1.Name = "GroupFooter1";
        // 
        // xrLine3
        // 
        this.xrLine3.LocationFloat = new DevExpress.Utils.PointFloat(0F, 0F);
        this.xrLine3.Name = "xrLine3";
        this.xrLine3.SizeF = new System.Drawing.SizeF(649.9999F, 2F);
        // 
        // xrLabel12
        // 
        this.xrLabel12.Font = new System.Drawing.Font("Verdana", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
        this.xrLabel12.LocationFloat = new DevExpress.Utils.PointFloat(305.2083F, 2F);
        this.xrLabel12.Name = "xrLabel12";
        this.xrLabel12.Padding = new DevExpress.XtraPrinting.PaddingInfo(2, 2, 0, 0, 100F);
        this.xrLabel12.SizeF = new System.Drawing.SizeF(100F, 19.25001F);
        this.xrLabel12.StylePriority.UseFont = false;
        this.xrLabel12.Text = "Total";
        // 
        // xrllbltotalamt
        // 
        this.xrllbltotalamt.DataBindings.AddRange(new DevExpress.XtraReports.UI.XRBinding[] {
            new DevExpress.XtraReports.UI.XRBinding("Text", null, "settlementdt.Amount")});
        this.xrllbltotalamt.LocationFloat = new DevExpress.Utils.PointFloat(455.4745F, 2F);
        this.xrllbltotalamt.Name = "xrllbltotalamt";
        this.xrllbltotalamt.Padding = new DevExpress.XtraPrinting.PaddingInfo(2, 2, 0, 0, 100F);
        this.xrllbltotalamt.SizeF = new System.Drawing.SizeF(85.41675F, 19.25001F);
        xrSummary1.IgnoreNullValues = true;
        xrSummary1.Running = DevExpress.XtraReports.UI.SummaryRunning.Group;
        this.xrllbltotalamt.Summary = xrSummary1;
        this.xrllbltotalamt.Text = "xrllbltotalamt";
        // 
        // GroupHeader2
        // 
        this.GroupHeader2.Controls.AddRange(new DevExpress.XtraReports.UI.XRControl[] {
            this.xrLine2,
            this.xrLabel13,
            this.xrLabel11,
            this.xrLabel10,
            this.xrLabel8});
        this.GroupHeader2.GroupFields.AddRange(new DevExpress.XtraReports.UI.GroupField[] {
            new DevExpress.XtraReports.UI.GroupField("EmpId", DevExpress.XtraReports.UI.XRColumnSortOrder.Ascending)});
        this.GroupHeader2.HeightF = 14.5F;
        this.GroupHeader2.Level = 1;
        this.GroupHeader2.Name = "GroupHeader2";
        // 
        // xrLine2
        // 
        this.xrLine2.LocationFloat = new DevExpress.Utils.PointFloat(0F, 12.5F);
        this.xrLine2.Name = "xrLine2";
        this.xrLine2.SizeF = new System.Drawing.SizeF(649.9999F, 2F);
        // 
        // xrLabel13
        // 
        this.xrLabel13.Font = new System.Drawing.Font("Verdana", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
        this.xrLabel13.LocationFloat = new DevExpress.Utils.PointFloat(402.0833F, 0F);
        this.xrLabel13.Name = "xrLabel13";
        this.xrLabel13.Padding = new DevExpress.XtraPrinting.PaddingInfo(2, 2, 0, 0, 100F);
        this.xrLabel13.SizeF = new System.Drawing.SizeF(122.9167F, 10.5F);
        this.xrLabel13.StylePriority.UseFont = false;
        this.xrLabel13.Text = "Employee Name";
        // 
        // xrLabel11
        // 
        this.xrLabel11.Font = new System.Drawing.Font("Verdana", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
        this.xrLabel11.LocationFloat = new DevExpress.Utils.PointFloat(0F, 0F);
        this.xrLabel11.Name = "xrLabel11";
        this.xrLabel11.Padding = new DevExpress.XtraPrinting.PaddingInfo(2, 2, 0, 0, 100F);
        this.xrLabel11.SizeF = new System.Drawing.SizeF(100F, 10.5F);
        this.xrLabel11.StylePriority.UseFont = false;
        this.xrLabel11.Text = "Employee Id";
        // 
        // xrLabel10
        // 
        this.xrLabel10.DataBindings.AddRange(new DevExpress.XtraReports.UI.XRBinding[] {
            new DevExpress.XtraReports.UI.XRBinding("Text", null, "settlementdt.Empname")});
        this.xrLabel10.LocationFloat = new DevExpress.Utils.PointFloat(540F, 0F);
        this.xrLabel10.Name = "xrLabel10";
        this.xrLabel10.Padding = new DevExpress.XtraPrinting.PaddingInfo(2, 2, 0, 0, 100F);
        this.xrLabel10.SizeF = new System.Drawing.SizeF(100F, 10.5F);
        this.xrLabel10.Text = "xrLabel10";
        // 
        // xrLabel8
        // 
        this.xrLabel8.DataBindings.AddRange(new DevExpress.XtraReports.UI.XRBinding[] {
            new DevExpress.XtraReports.UI.XRBinding("Text", null, "settlementdt.EmpId")});
        this.xrLabel8.LocationFloat = new DevExpress.Utils.PointFloat(110.0695F, 0F);
        this.xrLabel8.Name = "xrLabel8";
        this.xrLabel8.Padding = new DevExpress.XtraPrinting.PaddingInfo(2, 2, 0, 0, 100F);
        this.xrLabel8.SizeF = new System.Drawing.SizeF(100F, 10.5F);
        this.xrLabel8.Text = "xrLabel8";
        // 
        // GroupFooter2
        // 
        this.GroupFooter2.HeightF = 9.541702F;
        this.GroupFooter2.Level = 1;
        this.GroupFooter2.Name = "GroupFooter2";
        // 
        // XtraReport1
        // 
        this.Bands.AddRange(new DevExpress.XtraReports.UI.Band[] {
            this.Detail,
            this.TopMargin,
            this.BottomMargin,
            this.GroupHeader1,
            this.GroupFooter1,
            this.GroupHeader2,
            this.GroupFooter2});
        this.DataMember = "settlementdt";
        this.DataSource = this.settlement_Payment_DataSet1;
        this.Margins = new System.Drawing.Printing.Margins(100, 100, 91, 17);
        this.Version = "10.2";
        ((System.ComponentModel.ISupportInitialize)(this.xrTable4)).EndInit();
        ((System.ComponentModel.ISupportInitialize)(this.settlement_Payment_DataSet1)).EndInit();
        ((System.ComponentModel.ISupportInitialize)(this.xrTable3)).EndInit();
        ((System.ComponentModel.ISupportInitialize)(this.xrTable1)).EndInit();
        ((System.ComponentModel.ISupportInitialize)(this)).EndInit();

	}

	#endregion

    //private void xrllblgrosssum_BeforePrint(object sender, System.Drawing.Printing.PrintEventArgs e)
    //{
    //    //double sub = 0;
     
    //    //if (xrllblamt.Text != "" && xrllbltotalamt.Text != "" && xrllbltype.Text=="Subtraction")
    //    //{
    //    //    sub = Convert.ToDouble(xrllbltotalamt.Text) - Convert.ToDouble(xrllblamt.Text);

    //    //    xrllblgrosssum.Text = sub.ToString();
    //    //}

    //}
}
