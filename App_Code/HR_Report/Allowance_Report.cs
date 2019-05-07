using System;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using DevExpress.XtraReports.UI;

/// <summary>
/// Summary description for Allowance_Report
/// </summary>
public class Allowance_Report : DevExpress.XtraReports.UI.XtraReport
{
	private DevExpress.XtraReports.UI.DetailBand Detail;
	private DevExpress.XtraReports.UI.TopMarginBand TopMargin;
	private DevExpress.XtraReports.UI.BottomMarginBand BottomMargin;
    private XRLabel xrLabel1;
    private XRLabel xrLabel2;
    private XRLabel xrLabel3;
    private XRPictureBox xrPictureBox1;
    private XRLabel xrLabel4;
    private XRLabel xrLabel5;
    private GroupHeaderBand GroupHeader1;
    private XRLine xrLine1;
    private XRLabel xrLabel11;
    private XRLabel xrLabel13;
    private XRLabel xrLabel7;
    private XRLabel xrLabel6;
    private Employee_Allowance employee_Allowance1;
    private GroupHeaderBand GroupHeader2;
    private XRTable xrTable1;
    private XRTableRow xrTableRow1;
    private XRTableCell xrTableCell1;
    private XRTableCell xrTableCell2;
    private XRTableCell xrTableCell3;
    private XRLine xrLine4;
    private XRLine xrLine2;
    private XRTable xrTable2;
    private XRTableRow xrTableRow2;
    private XRTableCell xrTableCell5;
    private XRTableCell xrTableCell6;
    private GroupFooterBand GroupFooter1;
    private XRLine xrLine3;
    private XRLabel xrLabel10;
    private XRLabel xrLabel14;
    private Employee_Allowance employee_Allowance2;
    private Employee_Allowance employee_Allowance3;
    private XRTableCell xrTableCell4;
    private XRTable xrTable3;
    private XRTableRow xrTableRow3;
    private XRTableCell xrTableCell7;
    private XRTableCell xrTableCell8;
    private XRTableCell xrTableCell9;
	/// <summary>
	/// Required designer variable.
	/// </summary>
	private System.ComponentModel.IContainer components = null;

	public Allowance_Report()
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
        string resourceFileName = "Allowance_Report.resx";
        DevExpress.XtraReports.UI.XRSummary xrSummary1 = new DevExpress.XtraReports.UI.XRSummary();
        DevExpress.XtraReports.UI.XRSummary xrSummary2 = new DevExpress.XtraReports.UI.XRSummary();
        this.Detail = new DevExpress.XtraReports.UI.DetailBand();
        this.xrTable2 = new DevExpress.XtraReports.UI.XRTable();
        this.xrTableRow2 = new DevExpress.XtraReports.UI.XRTableRow();
        this.xrTableCell4 = new DevExpress.XtraReports.UI.XRTableCell();
        this.xrTableCell5 = new DevExpress.XtraReports.UI.XRTableCell();
        this.xrTableCell6 = new DevExpress.XtraReports.UI.XRTableCell();
        this.TopMargin = new DevExpress.XtraReports.UI.TopMarginBand();
        this.xrLabel14 = new DevExpress.XtraReports.UI.XRLabel();
        this.xrLabel10 = new DevExpress.XtraReports.UI.XRLabel();
        this.xrLabel5 = new DevExpress.XtraReports.UI.XRLabel();
        this.xrLabel4 = new DevExpress.XtraReports.UI.XRLabel();
        this.xrPictureBox1 = new DevExpress.XtraReports.UI.XRPictureBox();
        this.xrLabel3 = new DevExpress.XtraReports.UI.XRLabel();
        this.xrLabel2 = new DevExpress.XtraReports.UI.XRLabel();
        this.xrLabel1 = new DevExpress.XtraReports.UI.XRLabel();
        this.xrLine1 = new DevExpress.XtraReports.UI.XRLine();
        this.BottomMargin = new DevExpress.XtraReports.UI.BottomMarginBand();
        this.GroupHeader1 = new DevExpress.XtraReports.UI.GroupHeaderBand();
        this.xrLine4 = new DevExpress.XtraReports.UI.XRLine();
        this.xrTable1 = new DevExpress.XtraReports.UI.XRTable();
        this.xrTableRow1 = new DevExpress.XtraReports.UI.XRTableRow();
        this.xrTableCell1 = new DevExpress.XtraReports.UI.XRTableCell();
        this.xrTableCell2 = new DevExpress.XtraReports.UI.XRTableCell();
        this.xrTableCell3 = new DevExpress.XtraReports.UI.XRTableCell();
        this.xrLabel13 = new DevExpress.XtraReports.UI.XRLabel();
        this.xrLabel11 = new DevExpress.XtraReports.UI.XRLabel();
        this.employee_Allowance1 = new Employee_Allowance();
        this.xrLabel6 = new DevExpress.XtraReports.UI.XRLabel();
        this.xrLabel7 = new DevExpress.XtraReports.UI.XRLabel();
        this.GroupHeader2 = new DevExpress.XtraReports.UI.GroupHeaderBand();
        this.xrLine2 = new DevExpress.XtraReports.UI.XRLine();
        this.GroupFooter1 = new DevExpress.XtraReports.UI.GroupFooterBand();
        this.xrLine3 = new DevExpress.XtraReports.UI.XRLine();
        this.employee_Allowance2 = new Employee_Allowance();
        this.employee_Allowance3 = new Employee_Allowance();
        this.xrTable3 = new DevExpress.XtraReports.UI.XRTable();
        this.xrTableRow3 = new DevExpress.XtraReports.UI.XRTableRow();
        this.xrTableCell7 = new DevExpress.XtraReports.UI.XRTableCell();
        this.xrTableCell8 = new DevExpress.XtraReports.UI.XRTableCell();
        this.xrTableCell9 = new DevExpress.XtraReports.UI.XRTableCell();
        ((System.ComponentModel.ISupportInitialize)(this.xrTable2)).BeginInit();
        ((System.ComponentModel.ISupportInitialize)(this.xrTable1)).BeginInit();
        ((System.ComponentModel.ISupportInitialize)(this.employee_Allowance1)).BeginInit();
        ((System.ComponentModel.ISupportInitialize)(this.employee_Allowance2)).BeginInit();
        ((System.ComponentModel.ISupportInitialize)(this.employee_Allowance3)).BeginInit();
        ((System.ComponentModel.ISupportInitialize)(this.xrTable3)).BeginInit();
        ((System.ComponentModel.ISupportInitialize)(this)).BeginInit();
        // 
        // Detail
        // 
        this.Detail.Controls.AddRange(new DevExpress.XtraReports.UI.XRControl[] {
            this.xrTable2});
        this.Detail.HeightF = 25F;
        this.Detail.Name = "Detail";
        this.Detail.Padding = new DevExpress.XtraPrinting.PaddingInfo(0, 0, 0, 0, 100F);
        this.Detail.TextAlignment = DevExpress.XtraPrinting.TextAlignment.TopLeft;
        // 
        // xrTable2
        // 
        this.xrTable2.LocationFloat = new DevExpress.Utils.PointFloat(0F, 0F);
        this.xrTable2.Name = "xrTable2";
        this.xrTable2.Rows.AddRange(new DevExpress.XtraReports.UI.XRTableRow[] {
            this.xrTableRow2});
        this.xrTable2.SizeF = new System.Drawing.SizeF(649.9999F, 25F);
        // 
        // xrTableRow2
        // 
        this.xrTableRow2.Cells.AddRange(new DevExpress.XtraReports.UI.XRTableCell[] {
            this.xrTableCell4,
            this.xrTableCell5,
            this.xrTableCell6});
        this.xrTableRow2.Name = "xrTableRow2";
        this.xrTableRow2.Weight = 1;
        // 
        // xrTableCell4
        // 
        this.xrTableCell4.DataBindings.AddRange(new DevExpress.XtraReports.UI.XRBinding[] {
            new DevExpress.XtraReports.UI.XRBinding("Text", null, "dtEmpAllowance.Allowance")});
        this.xrTableCell4.Name = "xrTableCell4";
        this.xrTableCell4.Text = "xrTableCell4";
        this.xrTableCell4.Weight = 0.67179474673548;
        // 
        // xrTableCell5
        // 
        this.xrTableCell5.DataBindings.AddRange(new DevExpress.XtraReports.UI.XRBinding[] {
            new DevExpress.XtraReports.UI.XRBinding("Text", null, "dtEmpAllowance.ActAmount")});
        this.xrTableCell5.Name = "xrTableCell5";
        this.xrTableCell5.Weight = 0.59766144972991087;
        // 
        // xrTableCell6
        // 
        this.xrTableCell6.DataBindings.AddRange(new DevExpress.XtraReports.UI.XRBinding[] {
            new DevExpress.XtraReports.UI.XRBinding("Text", null, "dtEmpAllowance.GivenAmount")});
        this.xrTableCell6.Name = "xrTableCell6";
        this.xrTableCell6.Text = "[GivenAmount]";
        this.xrTableCell6.Weight = 0.77741851743231416;
        // 
        // TopMargin
        // 
        this.TopMargin.Controls.AddRange(new DevExpress.XtraReports.UI.XRControl[] {
            this.xrLabel14,
            this.xrLabel10,
            this.xrLabel5,
            this.xrLabel4,
            this.xrPictureBox1,
            this.xrLabel3,
            this.xrLabel2,
            this.xrLabel1,
            this.xrLine1});
        this.TopMargin.HeightF = 107.0833F;
        this.TopMargin.Name = "TopMargin";
        this.TopMargin.Padding = new DevExpress.XtraPrinting.PaddingInfo(0, 0, 0, 0, 100F);
        this.TopMargin.TextAlignment = DevExpress.XtraPrinting.TextAlignment.TopLeft;
        // 
        // xrLabel14
        // 
        this.xrLabel14.DataBindings.AddRange(new DevExpress.XtraReports.UI.XRBinding[] {
            new DevExpress.XtraReports.UI.XRBinding("Text", null, "dtEmpAllowance.Year")});
        this.xrLabel14.LocationFloat = new DevExpress.Utils.PointFloat(539.9999F, 67.00001F);
        this.xrLabel14.Name = "xrLabel14";
        this.xrLabel14.Padding = new DevExpress.XtraPrinting.PaddingInfo(2, 2, 0, 0, 100F);
        this.xrLabel14.SizeF = new System.Drawing.SizeF(100F, 23F);
        this.xrLabel14.Text = "xrLabel14";
        // 
        // xrLabel10
        // 
        this.xrLabel10.DataBindings.AddRange(new DevExpress.XtraReports.UI.XRBinding[] {
            new DevExpress.XtraReports.UI.XRBinding("Text", null, "dtEmpAllowance.Month")});
        this.xrLabel10.LocationFloat = new DevExpress.Utils.PointFloat(107.2917F, 67.00001F);
        this.xrLabel10.Name = "xrLabel10";
        this.xrLabel10.Padding = new DevExpress.XtraPrinting.PaddingInfo(2, 2, 0, 0, 100F);
        this.xrLabel10.SizeF = new System.Drawing.SizeF(100F, 23F);
        this.xrLabel10.Text = "xrLabel10";
        // 
        // xrLabel5
        // 
        this.xrLabel5.Font = new System.Drawing.Font("Verdana", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
        this.xrLabel5.LocationFloat = new DevExpress.Utils.PointFloat(403.125F, 67.00001F);
        this.xrLabel5.Name = "xrLabel5";
        this.xrLabel5.Padding = new DevExpress.XtraPrinting.PaddingInfo(2, 2, 0, 0, 100F);
        this.xrLabel5.SizeF = new System.Drawing.SizeF(100F, 23F);
        this.xrLabel5.StylePriority.UseFont = false;
        this.xrLabel5.Text = "xrLabel5";
        // 
        // xrLabel4
        // 
        this.xrLabel4.Font = new System.Drawing.Font("Verdana", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
        this.xrLabel4.LocationFloat = new DevExpress.Utils.PointFloat(0F, 67.00001F);
        this.xrLabel4.Name = "xrLabel4";
        this.xrLabel4.Padding = new DevExpress.XtraPrinting.PaddingInfo(2, 2, 0, 0, 100F);
        this.xrLabel4.SizeF = new System.Drawing.SizeF(100F, 23F);
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
        // xrLabel3
        // 
        this.xrLabel3.Font = new System.Drawing.Font("Verdana", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
        this.xrLabel3.LocationFloat = new DevExpress.Utils.PointFloat(187.5F, 20.50001F);
        this.xrLabel3.Name = "xrLabel3";
        this.xrLabel3.Padding = new DevExpress.XtraPrinting.PaddingInfo(2, 2, 0, 0, 100F);
        this.xrLabel3.SizeF = new System.Drawing.SizeF(315.625F, 23F);
        this.xrLabel3.StylePriority.UseFont = false;
        this.xrLabel3.StylePriority.UseTextAlignment = false;
        this.xrLabel3.Text = "xrLabel3";
        this.xrLabel3.TextAlignment = DevExpress.XtraPrinting.TextAlignment.TopCenter;
        // 
        // xrLabel2
        // 
        this.xrLabel2.Font = new System.Drawing.Font("Verdana", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
        this.xrLabel2.LocationFloat = new DevExpress.Utils.PointFloat(0F, 33.00001F);
        this.xrLabel2.Name = "xrLabel2";
        this.xrLabel2.Padding = new DevExpress.XtraPrinting.PaddingInfo(2, 2, 0, 0, 100F);
        this.xrLabel2.SizeF = new System.Drawing.SizeF(164.5833F, 23F);
        this.xrLabel2.StylePriority.UseFont = false;
        this.xrLabel2.Text = "xrLabel2";
        // 
        // xrLabel1
        // 
        this.xrLabel1.Font = new System.Drawing.Font("Verdana", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
        this.xrLabel1.LocationFloat = new DevExpress.Utils.PointFloat(0F, 10.00001F);
        this.xrLabel1.Name = "xrLabel1";
        this.xrLabel1.Padding = new DevExpress.XtraPrinting.PaddingInfo(2, 2, 0, 0, 100F);
        this.xrLabel1.SizeF = new System.Drawing.SizeF(164.5833F, 23F);
        this.xrLabel1.StylePriority.UseFont = false;
        this.xrLabel1.Text = "xrLabel1";
        // 
        // xrLine1
        // 
        this.xrLine1.LocationFloat = new DevExpress.Utils.PointFloat(9.536743E-05F, 95.83334F);
        this.xrLine1.Name = "xrLine1";
        this.xrLine1.SizeF = new System.Drawing.SizeF(650F, 7.083313F);
        // 
        // BottomMargin
        // 
        this.BottomMargin.HeightF = 0F;
        this.BottomMargin.Name = "BottomMargin";
        this.BottomMargin.Padding = new DevExpress.XtraPrinting.PaddingInfo(0, 0, 0, 0, 100F);
        this.BottomMargin.TextAlignment = DevExpress.XtraPrinting.TextAlignment.TopLeft;
        // 
        // GroupHeader1
        // 
        this.GroupHeader1.Controls.AddRange(new DevExpress.XtraReports.UI.XRControl[] {
            this.xrLine4,
            this.xrTable1});
        this.GroupHeader1.HeightF = 25F;
        this.GroupHeader1.Name = "GroupHeader1";
        // 
        // xrLine4
        // 
        this.xrLine4.LocationFloat = new DevExpress.Utils.PointFloat(9.536743E-05F, 18.75F);
        this.xrLine4.Name = "xrLine4";
        this.xrLine4.SizeF = new System.Drawing.SizeF(649.9999F, 2F);
        // 
        // xrTable1
        // 
        this.xrTable1.Font = new System.Drawing.Font("Verdana", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
        this.xrTable1.LocationFloat = new DevExpress.Utils.PointFloat(0F, 0F);
        this.xrTable1.Name = "xrTable1";
        this.xrTable1.Rows.AddRange(new DevExpress.XtraReports.UI.XRTableRow[] {
            this.xrTableRow1});
        this.xrTable1.SizeF = new System.Drawing.SizeF(650F, 18.75F);
        this.xrTable1.StylePriority.UseFont = false;
        // 
        // xrTableRow1
        // 
        this.xrTableRow1.Cells.AddRange(new DevExpress.XtraReports.UI.XRTableCell[] {
            this.xrTableCell1,
            this.xrTableCell2,
            this.xrTableCell3});
        this.xrTableRow1.Name = "xrTableRow1";
        this.xrTableRow1.Weight = 1;
        // 
        // xrTableCell1
        // 
        this.xrTableCell1.Name = "xrTableCell1";
        this.xrTableCell1.Text = "Allowance";
        this.xrTableCell1.Weight = 2.0729167175292966;
        // 
        // xrTableCell2
        // 
        this.xrTableCell2.Font = new System.Drawing.Font("Verdana", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
        this.xrTableCell2.Name = "xrTableCell2";
        this.xrTableCell2.StylePriority.UseFont = false;
        this.xrTableCell2.Text = "Actual Amount";
        this.xrTableCell2.Weight = 1.9583332824707032;
        // 
        // xrTableCell3
        // 
        this.xrTableCell3.Name = "xrTableCell3";
        this.xrTableCell3.Text = "Paid Amount";
        this.xrTableCell3.Weight = 2.46875;
        // 
        // xrLabel13
        // 
        this.xrLabel13.Font = new System.Drawing.Font("Verdana", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
        this.xrLabel13.LocationFloat = new DevExpress.Utils.PointFloat(403.1249F, 0F);
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
        // employee_Allowance1
        // 
        this.employee_Allowance1.DataSetName = "Employee_Allowance";
        this.employee_Allowance1.SchemaSerializationMode = System.Data.SchemaSerializationMode.IncludeSchema;
        // 
        // xrLabel6
        // 
        this.xrLabel6.DataBindings.AddRange(new DevExpress.XtraReports.UI.XRBinding[] {
            new DevExpress.XtraReports.UI.XRBinding("Text", null, "dtEmpAllowance.EmpId")});
        this.xrLabel6.LocationFloat = new DevExpress.Utils.PointFloat(107.2917F, 0F);
        this.xrLabel6.Name = "xrLabel6";
        this.xrLabel6.Padding = new DevExpress.XtraPrinting.PaddingInfo(2, 2, 0, 0, 100F);
        this.xrLabel6.SizeF = new System.Drawing.SizeF(100F, 10.5F);
        this.xrLabel6.Text = "xrLabel6";
        // 
        // xrLabel7
        // 
        this.xrLabel7.DataBindings.AddRange(new DevExpress.XtraReports.UI.XRBinding[] {
            new DevExpress.XtraReports.UI.XRBinding("Text", null, "dtEmpAllowance.EmpName")});
        this.xrLabel7.LocationFloat = new DevExpress.Utils.PointFloat(540F, 0F);
        this.xrLabel7.Name = "xrLabel7";
        this.xrLabel7.Padding = new DevExpress.XtraPrinting.PaddingInfo(2, 2, 0, 0, 100F);
        this.xrLabel7.SizeF = new System.Drawing.SizeF(100F, 10.5F);
        this.xrLabel7.Text = "[EmpName]";
        // 
        // GroupHeader2
        // 
        this.GroupHeader2.Controls.AddRange(new DevExpress.XtraReports.UI.XRControl[] {
            this.xrLine2,
            this.xrLabel11,
            this.xrLabel6,
            this.xrLabel13,
            this.xrLabel7});
        this.GroupHeader2.GroupFields.AddRange(new DevExpress.XtraReports.UI.GroupField[] {
            new DevExpress.XtraReports.UI.GroupField("EmpId", DevExpress.XtraReports.UI.XRColumnSortOrder.Ascending)});
        this.GroupHeader2.HeightF = 20.83333F;
        this.GroupHeader2.Level = 1;
        this.GroupHeader2.Name = "GroupHeader2";
        // 
        // xrLine2
        // 
        this.xrLine2.LocationFloat = new DevExpress.Utils.PointFloat(0F, 10.49999F);
        this.xrLine2.Name = "xrLine2";
        this.xrLine2.SizeF = new System.Drawing.SizeF(649.9999F, 2F);
        // 
        // GroupFooter1
        // 
        this.GroupFooter1.Controls.AddRange(new DevExpress.XtraReports.UI.XRControl[] {
            this.xrTable3,
            this.xrLine3});
        this.GroupFooter1.HeightF = 21.875F;
        this.GroupFooter1.Name = "GroupFooter1";
        // 
        // xrLine3
        // 
        this.xrLine3.LocationFloat = new DevExpress.Utils.PointFloat(9.536743E-05F, 0F);
        this.xrLine3.Name = "xrLine3";
        this.xrLine3.SizeF = new System.Drawing.SizeF(649.9999F, 2F);
        // 
        // employee_Allowance2
        // 
        this.employee_Allowance2.DataSetName = "Employee_Allowance";
        this.employee_Allowance2.SchemaSerializationMode = System.Data.SchemaSerializationMode.IncludeSchema;
        // 
        // employee_Allowance3
        // 
        this.employee_Allowance3.DataSetName = "Employee_Allowance";
        this.employee_Allowance3.SchemaSerializationMode = System.Data.SchemaSerializationMode.IncludeSchema;
        // 
        // xrTable3
        // 
        this.xrTable3.LocationFloat = new DevExpress.Utils.PointFloat(0F, 3.125F);
        this.xrTable3.Name = "xrTable3";
        this.xrTable3.Rows.AddRange(new DevExpress.XtraReports.UI.XRTableRow[] {
            this.xrTableRow3});
        this.xrTable3.SizeF = new System.Drawing.SizeF(651.0416F, 18.75F);
        // 
        // xrTableRow3
        // 
        this.xrTableRow3.Cells.AddRange(new DevExpress.XtraReports.UI.XRTableCell[] {
            this.xrTableCell7,
            this.xrTableCell8,
            this.xrTableCell9});
        this.xrTableRow3.Name = "xrTableRow3";
        this.xrTableRow3.Weight = 1;
        // 
        // xrTableCell7
        // 
        this.xrTableCell7.Font = new System.Drawing.Font("Verdana", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
        this.xrTableCell7.Name = "xrTableCell7";
        this.xrTableCell7.StylePriority.UseFont = false;
        this.xrTableCell7.StylePriority.UseTextAlignment = false;
        this.xrTableCell7.Text = "Total";
        this.xrTableCell7.TextAlignment = DevExpress.XtraPrinting.TextAlignment.TopLeft;
        this.xrTableCell7.Weight = 0.98303982706498927;
        // 
        // xrTableCell8
        // 
        this.xrTableCell8.DataBindings.AddRange(new DevExpress.XtraReports.UI.XRBinding[] {
            new DevExpress.XtraReports.UI.XRBinding("Text", null, "dtEmpAllowance.ActAmount")});
        this.xrTableCell8.Name = "xrTableCell8";
        xrSummary1.Running = DevExpress.XtraReports.UI.SummaryRunning.Group;
        this.xrTableCell8.Summary = xrSummary1;
        this.xrTableCell8.Text = "xrTableCell8";
        this.xrTableCell8.Weight = 0.87456042966002678;
        // 
        // xrTableCell9
        // 
        this.xrTableCell9.DataBindings.AddRange(new DevExpress.XtraReports.UI.XRBinding[] {
            new DevExpress.XtraReports.UI.XRBinding("Text", null, "dtEmpAllowance.GivenAmount")});
        this.xrTableCell9.Name = "xrTableCell9";
        xrSummary2.Running = DevExpress.XtraReports.UI.SummaryRunning.Group;
        this.xrTableCell9.Summary = xrSummary2;
        this.xrTableCell9.Text = "xrTableCell9";
        this.xrTableCell9.Weight = 1.142399743274984;
        // 
        // Allowance_Report
        // 
        this.Bands.AddRange(new DevExpress.XtraReports.UI.Band[] {
            this.Detail,
            this.TopMargin,
            this.BottomMargin,
            this.GroupHeader1,
            this.GroupHeader2,
            this.GroupFooter1});
        this.DataMember = "dtEmpAllowance";
        this.DataSource = this.employee_Allowance3;
        this.Margins = new System.Drawing.Printing.Margins(100, 100, 107, 0);
        this.Version = "10.2";
        ((System.ComponentModel.ISupportInitialize)(this.xrTable2)).EndInit();
        ((System.ComponentModel.ISupportInitialize)(this.xrTable1)).EndInit();
        ((System.ComponentModel.ISupportInitialize)(this.employee_Allowance1)).EndInit();
        ((System.ComponentModel.ISupportInitialize)(this.employee_Allowance2)).EndInit();
        ((System.ComponentModel.ISupportInitialize)(this.employee_Allowance3)).EndInit();
        ((System.ComponentModel.ISupportInitialize)(this.xrTable3)).EndInit();
        ((System.ComponentModel.ISupportInitialize)(this)).EndInit();

	}

	#endregion
}
