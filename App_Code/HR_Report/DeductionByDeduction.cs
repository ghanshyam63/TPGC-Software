using System;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using DevExpress.XtraReports.UI;

/// <summary>
/// Summary description for DeductionByDeduction
/// </summary>
public class DeductionByDeduction : DevExpress.XtraReports.UI.XtraReport
{
	private DevExpress.XtraReports.UI.DetailBand Detail;
	private DevExpress.XtraReports.UI.TopMarginBand TopMargin;
    private DevExpress.XtraReports.UI.BottomMarginBand BottomMargin;
    private XRLine xrLine1;
    private XRLabel xrLabel5;
    private XRLabel xrLabel3;
    private XRLabel xrLabel1;
    private XRLabel xrLabel4;
    private XRPictureBox xrPictureBox1;
    private XRLabel xrLabel2;
    private XRLabel xrLabel7;
    private XRLabel xrLabel6;
    private Deduction_By_DeductionDataSet deduction_By_DeductionDataSet1;
    private GroupHeaderBand GroupHeader1;
    private GroupHeaderBand GroupHeader2;
    private XRTable xrTable1;
    private XRTableRow xrTableRow1;
    private XRTableCell xrTableCell1;
    private XRTableCell xrTableCell3;
    private XRLine xrLine2;
    private XRTableCell xrTableCell2;
    private XRTableCell xrTableCell4;
    private XRTable xrTable2;
    private XRTableRow xrTableRow2;
    private XRTableCell xrTableCell5;
    private XRTableCell xrTableCell6;
    private XRTableCell xrTableCell7;
    private XRTableCell xrTableCell8;
    private XRLine xrLine3;
    private XRTable xrTable3;
    private XRTableRow xrTableRow3;
    private XRTableCell xrTableCell9;
    private XRTableCell xrTableCell10;
    private XRTableCell xrTableCell11;
    private XRTableCell xrTableCell12;
    private GroupFooterBand GroupFooter1;
    private XRLine xrLine4;
    private XRTable xrTable4;
    private XRTableRow xrTableRow4;
    private XRTableCell xrTableCell13;
    private XRTableCell xrTableCell14;
    private XRTableCell xrTableCell15;
	/// <summary>
	/// Required designer variable.
	/// </summary>
	private System.ComponentModel.IContainer components = null;

	public DeductionByDeduction()
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
        string resourceFileName = "DeductionByDeduction.resx";
        DevExpress.XtraReports.UI.XRSummary xrSummary1 = new DevExpress.XtraReports.UI.XRSummary();
        DevExpress.XtraReports.UI.XRSummary xrSummary2 = new DevExpress.XtraReports.UI.XRSummary();
        this.Detail = new DevExpress.XtraReports.UI.DetailBand();
        this.xrTable3 = new DevExpress.XtraReports.UI.XRTable();
        this.xrTableRow3 = new DevExpress.XtraReports.UI.XRTableRow();
        this.xrTableCell9 = new DevExpress.XtraReports.UI.XRTableCell();
        this.xrTableCell12 = new DevExpress.XtraReports.UI.XRTableCell();
        this.xrTableCell10 = new DevExpress.XtraReports.UI.XRTableCell();
        this.xrTableCell11 = new DevExpress.XtraReports.UI.XRTableCell();
        this.xrLine3 = new DevExpress.XtraReports.UI.XRLine();
        this.TopMargin = new DevExpress.XtraReports.UI.TopMarginBand();
        this.xrLabel7 = new DevExpress.XtraReports.UI.XRLabel();
        this.xrLabel6 = new DevExpress.XtraReports.UI.XRLabel();
        this.xrLine1 = new DevExpress.XtraReports.UI.XRLine();
        this.xrLabel5 = new DevExpress.XtraReports.UI.XRLabel();
        this.xrLabel3 = new DevExpress.XtraReports.UI.XRLabel();
        this.xrLabel1 = new DevExpress.XtraReports.UI.XRLabel();
        this.xrLabel4 = new DevExpress.XtraReports.UI.XRLabel();
        this.xrPictureBox1 = new DevExpress.XtraReports.UI.XRPictureBox();
        this.xrLabel2 = new DevExpress.XtraReports.UI.XRLabel();
        this.BottomMargin = new DevExpress.XtraReports.UI.BottomMarginBand();
        this.deduction_By_DeductionDataSet1 = new Deduction_By_DeductionDataSet();
        this.GroupHeader1 = new DevExpress.XtraReports.UI.GroupHeaderBand();
        this.xrTable2 = new DevExpress.XtraReports.UI.XRTable();
        this.xrTableRow2 = new DevExpress.XtraReports.UI.XRTableRow();
        this.xrTableCell5 = new DevExpress.XtraReports.UI.XRTableCell();
        this.xrTableCell6 = new DevExpress.XtraReports.UI.XRTableCell();
        this.xrTableCell7 = new DevExpress.XtraReports.UI.XRTableCell();
        this.xrTableCell8 = new DevExpress.XtraReports.UI.XRTableCell();
        this.GroupHeader2 = new DevExpress.XtraReports.UI.GroupHeaderBand();
        this.xrTable1 = new DevExpress.XtraReports.UI.XRTable();
        this.xrTableRow1 = new DevExpress.XtraReports.UI.XRTableRow();
        this.xrTableCell1 = new DevExpress.XtraReports.UI.XRTableCell();
        this.xrTableCell2 = new DevExpress.XtraReports.UI.XRTableCell();
        this.xrTableCell3 = new DevExpress.XtraReports.UI.XRTableCell();
        this.xrTableCell4 = new DevExpress.XtraReports.UI.XRTableCell();
        this.xrLine2 = new DevExpress.XtraReports.UI.XRLine();
        this.GroupFooter1 = new DevExpress.XtraReports.UI.GroupFooterBand();
        this.xrLine4 = new DevExpress.XtraReports.UI.XRLine();
        this.xrTable4 = new DevExpress.XtraReports.UI.XRTable();
        this.xrTableRow4 = new DevExpress.XtraReports.UI.XRTableRow();
        this.xrTableCell13 = new DevExpress.XtraReports.UI.XRTableCell();
        this.xrTableCell15 = new DevExpress.XtraReports.UI.XRTableCell();
        this.xrTableCell14 = new DevExpress.XtraReports.UI.XRTableCell();
        ((System.ComponentModel.ISupportInitialize)(this.xrTable3)).BeginInit();
        ((System.ComponentModel.ISupportInitialize)(this.deduction_By_DeductionDataSet1)).BeginInit();
        ((System.ComponentModel.ISupportInitialize)(this.xrTable2)).BeginInit();
        ((System.ComponentModel.ISupportInitialize)(this.xrTable1)).BeginInit();
        ((System.ComponentModel.ISupportInitialize)(this.xrTable4)).BeginInit();
        ((System.ComponentModel.ISupportInitialize)(this)).BeginInit();
        // 
        // Detail
        // 
        this.Detail.Controls.AddRange(new DevExpress.XtraReports.UI.XRControl[] {
            this.xrTable3,
            this.xrLine3});
        this.Detail.HeightF = 26.04167F;
        this.Detail.Name = "Detail";
        this.Detail.Padding = new DevExpress.XtraPrinting.PaddingInfo(0, 0, 0, 0, 100F);
        this.Detail.TextAlignment = DevExpress.XtraPrinting.TextAlignment.TopLeft;
        // 
        // xrTable3
        // 
        this.xrTable3.Font = new System.Drawing.Font("Verdana", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
        this.xrTable3.LocationFloat = new DevExpress.Utils.PointFloat(0.0001271566F, 10.00001F);
        this.xrTable3.Name = "xrTable3";
        this.xrTable3.Rows.AddRange(new DevExpress.XtraReports.UI.XRTableRow[] {
            this.xrTableRow3});
        this.xrTable3.SizeF = new System.Drawing.SizeF(649.9998F, 11.24999F);
        this.xrTable3.StylePriority.UseFont = false;
        // 
        // xrTableRow3
        // 
        this.xrTableRow3.Cells.AddRange(new DevExpress.XtraReports.UI.XRTableCell[] {
            this.xrTableCell9,
            this.xrTableCell12,
            this.xrTableCell10,
            this.xrTableCell11});
        this.xrTableRow3.Name = "xrTableRow3";
        this.xrTableRow3.Weight = 1;
        // 
        // xrTableCell9
        // 
        this.xrTableCell9.DataBindings.AddRange(new DevExpress.XtraReports.UI.XRBinding[] {
            new DevExpress.XtraReports.UI.XRBinding("Text", null, "dtDeductionByDeduction.EmpId")});
        this.xrTableCell9.Name = "xrTableCell9";
        this.xrTableCell9.StylePriority.UseTextAlignment = false;
        this.xrTableCell9.Text = "xrTableCell9";
        this.xrTableCell9.TextAlignment = DevExpress.XtraPrinting.TextAlignment.TopCenter;
        this.xrTableCell9.Weight = 0.46153825116574831;
        // 
        // xrTableCell12
        // 
        this.xrTableCell12.DataBindings.AddRange(new DevExpress.XtraReports.UI.XRBinding[] {
            new DevExpress.XtraReports.UI.XRBinding("Text", null, "dtDeductionByDeduction.EmpName")});
        this.xrTableCell12.Name = "xrTableCell12";
        this.xrTableCell12.Text = "xrTableCell12";
        this.xrTableCell12.Weight = 1.07211562794348;
        // 
        // xrTableCell10
        // 
        this.xrTableCell10.DataBindings.AddRange(new DevExpress.XtraReports.UI.XRBinding[] {
            new DevExpress.XtraReports.UI.XRBinding("Text", null, "dtDeductionByDeduction.ActAmount")});
        this.xrTableCell10.Name = "xrTableCell10";
        this.xrTableCell10.Text = "xrTableCell10";
        this.xrTableCell10.Weight = 0.58173076877932561;
        // 
        // xrTableCell11
        // 
        this.xrTableCell11.DataBindings.AddRange(new DevExpress.XtraReports.UI.XRBinding[] {
            new DevExpress.XtraReports.UI.XRBinding("Text", null, "dtDeductionByDeduction.GivenAmount")});
        this.xrTableCell11.Name = "xrTableCell11";
        this.xrTableCell11.Text = "xrTableCell11";
        this.xrTableCell11.Weight = 0.88461535211144615;
        // 
        // xrLine3
        // 
        this.xrLine3.LocationFloat = new DevExpress.Utils.PointFloat(0F, 0F);
        this.xrLine3.Name = "xrLine3";
        this.xrLine3.SizeF = new System.Drawing.SizeF(649.9999F, 9.458333F);
        // 
        // TopMargin
        // 
        this.TopMargin.Controls.AddRange(new DevExpress.XtraReports.UI.XRControl[] {
            this.xrLabel7,
            this.xrLabel6,
            this.xrLine1,
            this.xrLabel5,
            this.xrLabel3,
            this.xrLabel1,
            this.xrLabel4,
            this.xrPictureBox1,
            this.xrLabel2});
        this.TopMargin.Font = new System.Drawing.Font("Times New Roman", 9.75F);
        this.TopMargin.HeightF = 92.12503F;
        this.TopMargin.Name = "TopMargin";
        this.TopMargin.Padding = new DevExpress.XtraPrinting.PaddingInfo(0, 0, 0, 0, 100F);
        this.TopMargin.StylePriority.UseFont = false;
        this.TopMargin.TextAlignment = DevExpress.XtraPrinting.TextAlignment.TopLeft;
        // 
        // xrLabel7
        // 
        this.xrLabel7.DataBindings.AddRange(new DevExpress.XtraReports.UI.XRBinding[] {
            new DevExpress.XtraReports.UI.XRBinding("Text", null, "dtDeductionByDeduction.Year")});
        this.xrLabel7.Font = new System.Drawing.Font("Verdana", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
        this.xrLabel7.LocationFloat = new DevExpress.Utils.PointFloat(458.3333F, 62.5F);
        this.xrLabel7.Name = "xrLabel7";
        this.xrLabel7.Padding = new DevExpress.XtraPrinting.PaddingInfo(2, 2, 0, 0, 100F);
        this.xrLabel7.SizeF = new System.Drawing.SizeF(191.6667F, 23F);
        this.xrLabel7.StylePriority.UseFont = false;
        this.xrLabel7.Text = "[Year]";
        // 
        // xrLabel6
        // 
        this.xrLabel6.DataBindings.AddRange(new DevExpress.XtraReports.UI.XRBinding[] {
            new DevExpress.XtraReports.UI.XRBinding("Text", null, "dtDeductionByDeduction.Month")});
        this.xrLabel6.Font = new System.Drawing.Font("Verdana", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
        this.xrLabel6.LocationFloat = new DevExpress.Utils.PointFloat(100F, 62.5F);
        this.xrLabel6.Name = "xrLabel6";
        this.xrLabel6.Padding = new DevExpress.XtraPrinting.PaddingInfo(2, 2, 0, 0, 100F);
        this.xrLabel6.SizeF = new System.Drawing.SizeF(100F, 23F);
        this.xrLabel6.StylePriority.UseFont = false;
        this.xrLabel6.Text = "xrLabel6";
        // 
        // xrLine1
        // 
        this.xrLine1.LocationFloat = new DevExpress.Utils.PointFloat(0.0001271566F, 76.54168F);
        this.xrLine1.Name = "xrLine1";
        this.xrLine1.SizeF = new System.Drawing.SizeF(650F, 13.00001F);
        // 
        // xrLabel5
        // 
        this.xrLabel5.Font = new System.Drawing.Font("Verdana", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
        this.xrLabel5.LocationFloat = new DevExpress.Utils.PointFloat(332.2917F, 62.5F);
        this.xrLabel5.Name = "xrLabel5";
        this.xrLabel5.Padding = new DevExpress.XtraPrinting.PaddingInfo(2, 2, 0, 0, 100F);
        this.xrLabel5.SizeF = new System.Drawing.SizeF(100F, 23F);
        this.xrLabel5.StylePriority.UseFont = false;
        this.xrLabel5.Text = "xrLabel5";
        // 
        // xrLabel3
        // 
        this.xrLabel3.Font = new System.Drawing.Font("Verdana", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
        this.xrLabel3.LocationFloat = new DevExpress.Utils.PointFloat(189.5834F, 25F);
        this.xrLabel3.Name = "xrLabel3";
        this.xrLabel3.Padding = new DevExpress.XtraPrinting.PaddingInfo(2, 2, 0, 0, 100F);
        this.xrLabel3.SizeF = new System.Drawing.SizeF(360.4166F, 23F);
        this.xrLabel3.StylePriority.UseFont = false;
        this.xrLabel3.StylePriority.UseTextAlignment = false;
        this.xrLabel3.Text = "xrLabel3";
        this.xrLabel3.TextAlignment = DevExpress.XtraPrinting.TextAlignment.TopCenter;
        // 
        // xrLabel1
        // 
        this.xrLabel1.Font = new System.Drawing.Font("Verdana", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
        this.xrLabel1.LocationFloat = new DevExpress.Utils.PointFloat(0F, 0F);
        this.xrLabel1.Name = "xrLabel1";
        this.xrLabel1.Padding = new DevExpress.XtraPrinting.PaddingInfo(2, 2, 0, 0, 100F);
        this.xrLabel1.SizeF = new System.Drawing.SizeF(189.5834F, 23F);
        this.xrLabel1.StylePriority.UseFont = false;
        this.xrLabel1.Text = "xrLabel1";
        // 
        // xrLabel4
        // 
        this.xrLabel4.Font = new System.Drawing.Font("Verdana", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
        this.xrLabel4.LocationFloat = new DevExpress.Utils.PointFloat(0F, 62.5F);
        this.xrLabel4.Name = "xrLabel4";
        this.xrLabel4.Padding = new DevExpress.XtraPrinting.PaddingInfo(2, 2, 0, 0, 100F);
        this.xrLabel4.SizeF = new System.Drawing.SizeF(100F, 17.41665F);
        this.xrLabel4.StylePriority.UseFont = false;
        this.xrLabel4.Text = "xrLabel4";
        // 
        // xrPictureBox1
        // 
        this.xrPictureBox1.LocationFloat = new DevExpress.Utils.PointFloat(550F, 0F);
        this.xrPictureBox1.Name = "xrPictureBox1";
        this.xrPictureBox1.SizeF = new System.Drawing.SizeF(100F, 56.00001F);
        this.xrPictureBox1.Sizing = DevExpress.XtraPrinting.ImageSizeMode.StretchImage;
        // 
        // xrLabel2
        // 
        this.xrLabel2.Font = new System.Drawing.Font("Verdana", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
        this.xrLabel2.LocationFloat = new DevExpress.Utils.PointFloat(0F, 25F);
        this.xrLabel2.Name = "xrLabel2";
        this.xrLabel2.Padding = new DevExpress.XtraPrinting.PaddingInfo(2, 2, 0, 0, 100F);
        this.xrLabel2.SizeF = new System.Drawing.SizeF(189.5834F, 23F);
        this.xrLabel2.StylePriority.UseFont = false;
        this.xrLabel2.Text = "xrLabel2";
        // 
        // BottomMargin
        // 
        this.BottomMargin.HeightF = 3.125F;
        this.BottomMargin.Name = "BottomMargin";
        this.BottomMargin.Padding = new DevExpress.XtraPrinting.PaddingInfo(0, 0, 0, 0, 100F);
        this.BottomMargin.TextAlignment = DevExpress.XtraPrinting.TextAlignment.TopLeft;
        // 
        // deduction_By_DeductionDataSet1
        // 
        this.deduction_By_DeductionDataSet1.DataSetName = "Deduction_By_DeductionDataSet";
        this.deduction_By_DeductionDataSet1.SchemaSerializationMode = System.Data.SchemaSerializationMode.IncludeSchema;
        // 
        // GroupHeader1
        // 
        this.GroupHeader1.Controls.AddRange(new DevExpress.XtraReports.UI.XRControl[] {
            this.xrTable2});
        this.GroupHeader1.HeightF = 17.70833F;
        this.GroupHeader1.Name = "GroupHeader1";
        // 
        // xrTable2
        // 
        this.xrTable2.Font = new System.Drawing.Font("Verdana", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
        this.xrTable2.LocationFloat = new DevExpress.Utils.PointFloat(6.357829E-05F, 0F);
        this.xrTable2.Name = "xrTable2";
        this.xrTable2.Rows.AddRange(new DevExpress.XtraReports.UI.XRTableRow[] {
            this.xrTableRow2});
        this.xrTable2.SizeF = new System.Drawing.SizeF(649.9999F, 12.58333F);
        this.xrTable2.StylePriority.UseFont = false;
        // 
        // xrTableRow2
        // 
        this.xrTableRow2.Cells.AddRange(new DevExpress.XtraReports.UI.XRTableCell[] {
            this.xrTableCell5,
            this.xrTableCell6,
            this.xrTableCell7,
            this.xrTableCell8});
        this.xrTableRow2.Name = "xrTableRow2";
        this.xrTableRow2.Weight = 1;
        // 
        // xrTableCell5
        // 
        this.xrTableCell5.Font = new System.Drawing.Font("Verdana", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
        this.xrTableCell5.Name = "xrTableCell5";
        this.xrTableCell5.StylePriority.UseFont = false;
        this.xrTableCell5.Text = "Employee Id";
        this.xrTableCell5.Weight = 0.46153842271432;
        // 
        // xrTableCell6
        // 
        this.xrTableCell6.Name = "xrTableCell6";
        this.xrTableCell6.Text = "Employee Name";
        this.xrTableCell6.Weight = 1.0721154970248175;
        // 
        // xrTableCell7
        // 
        this.xrTableCell7.Name = "xrTableCell7";
        this.xrTableCell7.Text = "Actual Amount";
        this.xrTableCell7.Weight = 0.58173061257987269;
        // 
        // xrTableCell8
        // 
        this.xrTableCell8.Name = "xrTableCell8";
        this.xrTableCell8.Text = "Paid Amount";
        this.xrTableCell8.Weight = 0.88461546768098964;
        // 
        // GroupHeader2
        // 
        this.GroupHeader2.Controls.AddRange(new DevExpress.XtraReports.UI.XRControl[] {
            this.xrTable1,
            this.xrLine2});
        this.GroupHeader2.GroupFields.AddRange(new DevExpress.XtraReports.UI.GroupField[] {
            new DevExpress.XtraReports.UI.GroupField("DeductionId", DevExpress.XtraReports.UI.XRColumnSortOrder.Ascending)});
        this.GroupHeader2.HeightF = 24.45835F;
        this.GroupHeader2.Level = 1;
        this.GroupHeader2.Name = "GroupHeader2";
        // 
        // xrTable1
        // 
        this.xrTable1.LocationFloat = new DevExpress.Utils.PointFloat(6.357829E-05F, 0F);
        this.xrTable1.Name = "xrTable1";
        this.xrTable1.Rows.AddRange(new DevExpress.XtraReports.UI.XRTableRow[] {
            this.xrTableRow1});
        this.xrTable1.SizeF = new System.Drawing.SizeF(649.9999F, 15.625F);
        // 
        // xrTableRow1
        // 
        this.xrTableRow1.Cells.AddRange(new DevExpress.XtraReports.UI.XRTableCell[] {
            this.xrTableCell1,
            this.xrTableCell2,
            this.xrTableCell3,
            this.xrTableCell4});
        this.xrTableRow1.Name = "xrTableRow1";
        this.xrTableRow1.Weight = 1;
        // 
        // xrTableCell1
        // 
        this.xrTableCell1.Font = new System.Drawing.Font("Verdana", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
        this.xrTableCell1.Name = "xrTableCell1";
        this.xrTableCell1.StylePriority.UseFont = false;
        this.xrTableCell1.Text = "Deduction Id";
        this.xrTableCell1.Weight = 0.0902367489507577;
        // 
        // xrTableCell2
        // 
        this.xrTableCell2.DataBindings.AddRange(new DevExpress.XtraReports.UI.XRBinding[] {
            new DevExpress.XtraReports.UI.XRBinding("Text", null, "dtDeductionByDeduction.DeductionId")});
        this.xrTableCell2.Font = new System.Drawing.Font("Verdana", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
        this.xrTableCell2.Name = "xrTableCell2";
        this.xrTableCell2.StylePriority.UseFont = false;
        this.xrTableCell2.Text = "xrTableCell2";
        this.xrTableCell2.Weight = 0.20961225390472063;
        // 
        // xrTableCell3
        // 
        this.xrTableCell3.Font = new System.Drawing.Font("Verdana", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
        this.xrTableCell3.Name = "xrTableCell3";
        this.xrTableCell3.StylePriority.UseFont = false;
        this.xrTableCell3.Text = "Deduction Name";
        this.xrTableCell3.Weight = 0.11373581632008281;
        // 
        // xrTableCell4
        // 
        this.xrTableCell4.DataBindings.AddRange(new DevExpress.XtraReports.UI.XRBinding[] {
            new DevExpress.XtraReports.UI.XRBinding("Text", null, "dtDeductionByDeduction.Deduction")});
        this.xrTableCell4.Font = new System.Drawing.Font("Verdana", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
        this.xrTableCell4.Name = "xrTableCell4";
        this.xrTableCell4.StylePriority.UseFont = false;
        this.xrTableCell4.Text = "xrTableCell4";
        this.xrTableCell4.Weight = 0.17295363875135231;
        // 
        // xrLine2
        // 
        this.xrLine2.LocationFloat = new DevExpress.Utils.PointFloat(0.0002702077F, 10.00001F);
        this.xrLine2.Name = "xrLine2";
        this.xrLine2.SizeF = new System.Drawing.SizeF(649.9999F, 14.45834F);
        // 
        // GroupFooter1
        // 
        this.GroupFooter1.Controls.AddRange(new DevExpress.XtraReports.UI.XRControl[] {
            this.xrLine4,
            this.xrTable4});
        this.GroupFooter1.HeightF = 30.20833F;
        this.GroupFooter1.Name = "GroupFooter1";
        // 
        // xrLine4
        // 
        this.xrLine4.LocationFloat = new DevExpress.Utils.PointFloat(0F, 0F);
        this.xrLine4.Name = "xrLine4";
        this.xrLine4.SizeF = new System.Drawing.SizeF(649.9999F, 5.83334F);
        // 
        // xrTable4
        // 
        this.xrTable4.LocationFloat = new DevExpress.Utils.PointFloat(99.99999F, 10.00001F);
        this.xrTable4.Name = "xrTable4";
        this.xrTable4.Rows.AddRange(new DevExpress.XtraReports.UI.XRTableRow[] {
            this.xrTableRow4});
        this.xrTable4.SizeF = new System.Drawing.SizeF(549.9999F, 14.37499F);
        // 
        // xrTableRow4
        // 
        this.xrTableRow4.Cells.AddRange(new DevExpress.XtraReports.UI.XRTableCell[] {
            this.xrTableCell13,
            this.xrTableCell15,
            this.xrTableCell14});
        this.xrTableRow4.Name = "xrTableRow4";
        this.xrTableRow4.Weight = 1;
        // 
        // xrTableCell13
        // 
        this.xrTableCell13.Font = new System.Drawing.Font("Verdana", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
        this.xrTableCell13.Name = "xrTableCell13";
        this.xrTableCell13.StylePriority.UseFont = false;
        this.xrTableCell13.Text = "Total";
        this.xrTableCell13.Weight = 0.39928197005652172;
        // 
        // xrTableCell15
        // 
        this.xrTableCell15.DataBindings.AddRange(new DevExpress.XtraReports.UI.XRBinding[] {
            new DevExpress.XtraReports.UI.XRBinding("Text", null, "dtDeductionByDeduction.ActAmount")});
        this.xrTableCell15.Font = new System.Drawing.Font("Verdana", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
        this.xrTableCell15.Name = "xrTableCell15";
        this.xrTableCell15.StylePriority.UseFont = false;
        xrSummary1.Running = DevExpress.XtraReports.UI.SummaryRunning.Group;
        this.xrTableCell15.Summary = xrSummary1;
        this.xrTableCell15.Text = "xrTableCell15";
        this.xrTableCell15.Weight = 0.21665084533752338;
        // 
        // xrTableCell14
        // 
        this.xrTableCell14.DataBindings.AddRange(new DevExpress.XtraReports.UI.XRBinding[] {
            new DevExpress.XtraReports.UI.XRBinding("Text", null, "dtDeductionByDeduction.GivenAmount")});
        this.xrTableCell14.Font = new System.Drawing.Font("Verdana", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
        this.xrTableCell14.Name = "xrTableCell14";
        this.xrTableCell14.StylePriority.UseFont = false;
        xrSummary2.Running = DevExpress.XtraReports.UI.SummaryRunning.Group;
        this.xrTableCell14.Summary = xrSummary2;
        this.xrTableCell14.Text = "xrTableCell14";
        this.xrTableCell14.Weight = 0.32945219487162508;
        // 
        // DeductionByDeduction
        // 
        this.Bands.AddRange(new DevExpress.XtraReports.UI.Band[] {
            this.Detail,
            this.TopMargin,
            this.BottomMargin,
            this.GroupHeader1,
            this.GroupHeader2,
            this.GroupFooter1});
        this.DataMember = "dtDeductionByDeduction";
        this.DataSource = this.deduction_By_DeductionDataSet1;
        this.Margins = new System.Drawing.Printing.Margins(100, 100, 92, 3);
        this.Version = "10.2";
        ((System.ComponentModel.ISupportInitialize)(this.xrTable3)).EndInit();
        ((System.ComponentModel.ISupportInitialize)(this.deduction_By_DeductionDataSet1)).EndInit();
        ((System.ComponentModel.ISupportInitialize)(this.xrTable2)).EndInit();
        ((System.ComponentModel.ISupportInitialize)(this.xrTable1)).EndInit();
        ((System.ComponentModel.ISupportInitialize)(this.xrTable4)).EndInit();
        ((System.ComponentModel.ISupportInitialize)(this)).EndInit();

	}

	#endregion
}
