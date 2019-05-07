using System;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using DevExpress.XtraReports.UI;

/// <summary>
/// Summary description for EmployeeClaimreport
/// </summary>
public class DirectoryWiseReport  : DevExpress.XtraReports.UI.XtraReport
{
	private DevExpress.XtraReports.UI.DetailBand Detail;
	private DevExpress.XtraReports.UI.TopMarginBand TopMargin;
	private DevExpress.XtraReports.UI.BottomMarginBand BottomMargin;
    private EmployeeClaimApproved employeeClaimApproved1;
    private XRPictureBox xrPictureBox1;
    private XRLabel xrLabel2;
    private EmployeeLoanDataset employeeLoanDataset1;
    private XRTable xrTable2;
    private XRTableRow xrTableRow2;
    private XRTableCell xrTableCell8;
    private XRTableCell xrTableCell9;
    private XRTableCell xrTableCell10;
    private XRTableCell xrTableCell11;
    private LoanDetailDataset loanDetailDataset1;
    private LoanDetailDataset loanDetailDataset2;
    private GroupHeaderBand GroupHeader1;
    private XRTable xrTable1;
    private XRTable xrTable6;
    private XRTableRow xrTableRow10;
    private XRTableCell xrTableCell24;
    private XRTableCell xrTableCell25;
    private XRTableCell xrTableCell26;
    private XRTableCell xrTableCell27;
    private XRLabel xrLabel3;
    private XRLabel xrLabel1;
    private EmployeeDirectoryDataSet employeeDirectoryDataSet1;
    private XRTable xrTable3;
    private XRTableRow xrTableRow4;
    private XRTableCell xrTableCell5;
    private XRTableCell xrTableCell6;
    private XRTableRow xrTableRow5;
    private XRTableCell xrTableCell13;
    private XRTableCell xrTableCell7;
    private PageFooterBand PageFooter;
    private XRPageInfo xrPageInfo1;
    private XRPageInfo xrPageInfo2;
	/// <summary>
	/// Required designer variable.
	/// </summary>
	private System.ComponentModel.IContainer components = null;

    public DirectoryWiseReport()
	{
		InitializeComponent();
		//
		// TODO: Add constructor logic here
		//
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
    public void SetImage(string Url)
    {
      xrPictureBox1.ImageUrl=Url;
    }
    public void setTitleName(string Title)
    {
        xrLabel2.Text = Title;
    }
    public void setaddress(string address)
    {
        xrLabel3.Text = address;
    }
    public void setcompanyname(string companyname)
    {
        xrLabel1.Text = companyname;
    }
	#region Designer generated code

	/// <summary>
	/// Required method for Designer support - do not modify
	/// the contents of this method with the code editor.
	/// </summary>
	private void InitializeComponent() {
        string resourceFileName = "DirectoryWiseReport.resx";
        System.Resources.ResourceManager resources = global::Resources.DirectoryWiseReport.ResourceManager;
        this.Detail = new DevExpress.XtraReports.UI.DetailBand();
        this.xrTable2 = new DevExpress.XtraReports.UI.XRTable();
        this.xrTableRow2 = new DevExpress.XtraReports.UI.XRTableRow();
        this.xrTableCell8 = new DevExpress.XtraReports.UI.XRTableCell();
        this.xrTableCell9 = new DevExpress.XtraReports.UI.XRTableCell();
        this.xrTableCell10 = new DevExpress.XtraReports.UI.XRTableCell();
        this.xrTableCell11 = new DevExpress.XtraReports.UI.XRTableCell();
        this.TopMargin = new DevExpress.XtraReports.UI.TopMarginBand();
        this.xrLabel1 = new DevExpress.XtraReports.UI.XRLabel();
        this.xrLabel3 = new DevExpress.XtraReports.UI.XRLabel();
        this.xrLabel2 = new DevExpress.XtraReports.UI.XRLabel();
        this.xrPictureBox1 = new DevExpress.XtraReports.UI.XRPictureBox();
        this.BottomMargin = new DevExpress.XtraReports.UI.BottomMarginBand();
        this.employeeClaimApproved1 = new EmployeeClaimApproved();
        this.employeeLoanDataset1 = new EmployeeLoanDataset();
        this.loanDetailDataset1 = new LoanDetailDataset();
        this.loanDetailDataset2 = new LoanDetailDataset();
        this.GroupHeader1 = new DevExpress.XtraReports.UI.GroupHeaderBand();
        this.xrTable3 = new DevExpress.XtraReports.UI.XRTable();
        this.xrTableRow4 = new DevExpress.XtraReports.UI.XRTableRow();
        this.xrTableCell5 = new DevExpress.XtraReports.UI.XRTableCell();
        this.xrTableCell6 = new DevExpress.XtraReports.UI.XRTableCell();
        this.xrTable6 = new DevExpress.XtraReports.UI.XRTable();
        this.xrTableRow10 = new DevExpress.XtraReports.UI.XRTableRow();
        this.xrTableCell24 = new DevExpress.XtraReports.UI.XRTableCell();
        this.xrTableCell25 = new DevExpress.XtraReports.UI.XRTableCell();
        this.xrTableCell26 = new DevExpress.XtraReports.UI.XRTableCell();
        this.xrTableCell27 = new DevExpress.XtraReports.UI.XRTableCell();
        this.xrTable1 = new DevExpress.XtraReports.UI.XRTable();
        this.xrTableRow5 = new DevExpress.XtraReports.UI.XRTableRow();
        this.xrTableCell7 = new DevExpress.XtraReports.UI.XRTableCell();
        this.xrTableCell13 = new DevExpress.XtraReports.UI.XRTableCell();
        this.employeeDirectoryDataSet1 = new EmployeeDirectoryDataSet();
        this.PageFooter = new DevExpress.XtraReports.UI.PageFooterBand();
        this.xrPageInfo1 = new DevExpress.XtraReports.UI.XRPageInfo();
        this.xrPageInfo2 = new DevExpress.XtraReports.UI.XRPageInfo();
        ((System.ComponentModel.ISupportInitialize)(this.xrTable2)).BeginInit();
        ((System.ComponentModel.ISupportInitialize)(this.employeeClaimApproved1)).BeginInit();
        ((System.ComponentModel.ISupportInitialize)(this.employeeLoanDataset1)).BeginInit();
        ((System.ComponentModel.ISupportInitialize)(this.loanDetailDataset1)).BeginInit();
        ((System.ComponentModel.ISupportInitialize)(this.loanDetailDataset2)).BeginInit();
        ((System.ComponentModel.ISupportInitialize)(this.xrTable3)).BeginInit();
        ((System.ComponentModel.ISupportInitialize)(this.xrTable6)).BeginInit();
        ((System.ComponentModel.ISupportInitialize)(this.xrTable1)).BeginInit();
        ((System.ComponentModel.ISupportInitialize)(this.employeeDirectoryDataSet1)).BeginInit();
        ((System.ComponentModel.ISupportInitialize)(this)).BeginInit();
        // 
        // Detail
        // 
        this.Detail.Controls.AddRange(new DevExpress.XtraReports.UI.XRControl[] {
            this.xrTable2});
        this.Detail.HeightF = 15.625F;
        this.Detail.Name = "Detail";
        this.Detail.Padding = new DevExpress.XtraPrinting.PaddingInfo(0, 0, 0, 0, 100F);
        this.Detail.SortFields.AddRange(new DevExpress.XtraReports.UI.GroupField[] {
            new DevExpress.XtraReports.UI.GroupField("Emp_Name", DevExpress.XtraReports.UI.XRColumnSortOrder.Ascending)});
        this.Detail.TextAlignment = DevExpress.XtraPrinting.TextAlignment.TopLeft;
        // 
        // xrTable2
        // 
        this.xrTable2.Borders = ((DevExpress.XtraPrinting.BorderSide)(((DevExpress.XtraPrinting.BorderSide.Left | DevExpress.XtraPrinting.BorderSide.Right)
                    | DevExpress.XtraPrinting.BorderSide.Bottom)));
        this.xrTable2.Font = new System.Drawing.Font("Verdana", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
        this.xrTable2.LocationFloat = new DevExpress.Utils.PointFloat(4.768372E-05F, 0F);
        this.xrTable2.Name = "xrTable2";
        this.xrTable2.Rows.AddRange(new DevExpress.XtraReports.UI.XRTableRow[] {
            this.xrTableRow2});
        this.xrTable2.SizeF = new System.Drawing.SizeF(739.9998F, 14.99999F);
        this.xrTable2.StylePriority.UseBorders = false;
        this.xrTable2.StylePriority.UseFont = false;
        // 
        // xrTableRow2
        // 
        this.xrTableRow2.Cells.AddRange(new DevExpress.XtraReports.UI.XRTableCell[] {
            this.xrTableCell8,
            this.xrTableCell9,
            this.xrTableCell10,
            this.xrTableCell11});
        this.xrTableRow2.Name = "xrTableRow2";
        this.xrTableRow2.Weight = 1;
        // 
        // xrTableCell8
        // 
        this.xrTableCell8.DataBindings.AddRange(new DevExpress.XtraReports.UI.XRBinding[] {
            new DevExpress.XtraReports.UI.XRBinding("Text", null, "EmpDirectory.Document_Name")});
        this.xrTableCell8.Name = "xrTableCell8";
        this.xrTableCell8.StylePriority.UseTextAlignment = false;
        this.xrTableCell8.Text = "xrTableCell8";
        this.xrTableCell8.TextAlignment = DevExpress.XtraPrinting.TextAlignment.TopLeft;
        this.xrTableCell8.Weight = 2.0872395684590273;
        // 
        // xrTableCell9
        // 
        this.xrTableCell9.DataBindings.AddRange(new DevExpress.XtraReports.UI.XRBinding[] {
            new DevExpress.XtraReports.UI.XRBinding("Text", null, "EmpDirectory.File_Name")});
        this.xrTableCell9.Name = "xrTableCell9";
        this.xrTableCell9.StylePriority.UseTextAlignment = false;
        this.xrTableCell9.Text = "xrTableCell9";
        this.xrTableCell9.TextAlignment = DevExpress.XtraPrinting.TextAlignment.TopLeft;
        this.xrTableCell9.Weight = 1.1848953808749894;
        // 
        // xrTableCell10
        // 
        this.xrTableCell10.DataBindings.AddRange(new DevExpress.XtraReports.UI.XRBinding[] {
            new DevExpress.XtraReports.UI.XRBinding("Text", null, "EmpDirectory.File_Upload_Date")});
        this.xrTableCell10.Name = "xrTableCell10";
        this.xrTableCell10.StylePriority.UseTextAlignment = false;
        this.xrTableCell10.Text = "xrTableCell10";
        this.xrTableCell10.TextAlignment = DevExpress.XtraPrinting.TextAlignment.TopCenter;
        this.xrTableCell10.Weight = 1.4856773054385353;
        this.xrTableCell10.BeforePrint += new System.Drawing.Printing.PrintEventHandler(this.xrTableCell10_BeforePrint);
        // 
        // xrTableCell11
        // 
        this.xrTableCell11.DataBindings.AddRange(new DevExpress.XtraReports.UI.XRBinding[] {
            new DevExpress.XtraReports.UI.XRBinding("Text", null, "EmpDirectory.File_Expiry_Date")});
        this.xrTableCell11.Name = "xrTableCell11";
        this.xrTableCell11.StylePriority.UseTextAlignment = false;
        this.xrTableCell11.Text = "xrTableCell11";
        this.xrTableCell11.TextAlignment = DevExpress.XtraPrinting.TextAlignment.TopCenter;
        this.xrTableCell11.Weight = 1.7171864100833836;
        this.xrTableCell11.BeforePrint += new System.Drawing.Printing.PrintEventHandler(this.xrTableCell11_BeforePrint);
        // 
        // TopMargin
        // 
        this.TopMargin.Controls.AddRange(new DevExpress.XtraReports.UI.XRControl[] {
            this.xrLabel1,
            this.xrLabel3,
            this.xrLabel2,
            this.xrPictureBox1});
        this.TopMargin.HeightF = 61.375F;
        this.TopMargin.Name = "TopMargin";
        this.TopMargin.Padding = new DevExpress.XtraPrinting.PaddingInfo(0, 0, 0, 0, 100F);
        this.TopMargin.TextAlignment = DevExpress.XtraPrinting.TextAlignment.TopLeft;
        // 
        // xrLabel1
        // 
        this.xrLabel1.Font = new System.Drawing.Font("Verdana", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
        this.xrLabel1.LocationFloat = new DevExpress.Utils.PointFloat(0F, 10.00001F);
        this.xrLabel1.Name = "xrLabel1";
        this.xrLabel1.Padding = new DevExpress.XtraPrinting.PaddingInfo(2, 2, 0, 0, 100F);
        this.xrLabel1.SizeF = new System.Drawing.SizeF(391.6667F, 16.75F);
        this.xrLabel1.StylePriority.UseFont = false;
        this.xrLabel1.Text = "xrLabel1";
        // 
        // xrLabel3
        // 
        this.xrLabel3.Font = new System.Drawing.Font("Verdana", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
        this.xrLabel3.LocationFloat = new DevExpress.Utils.PointFloat(4.768372E-05F, 26.75F);
        this.xrLabel3.Name = "xrLabel3";
        this.xrLabel3.Padding = new DevExpress.XtraPrinting.PaddingInfo(2, 2, 0, 0, 100F);
        this.xrLabel3.SizeF = new System.Drawing.SizeF(391.6667F, 18.29168F);
        this.xrLabel3.StylePriority.UseFont = false;
        this.xrLabel3.Text = "xrLabel3";
        // 
        // xrLabel2
        // 
        this.xrLabel2.Font = new System.Drawing.Font("Verdana", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
        this.xrLabel2.LocationFloat = new DevExpress.Utils.PointFloat(221.875F, 45.04169F);
        this.xrLabel2.Name = "xrLabel2";
        this.xrLabel2.Padding = new DevExpress.XtraPrinting.PaddingInfo(2, 2, 0, 0, 100F);
        this.xrLabel2.SizeF = new System.Drawing.SizeF(276.0416F, 12.58334F);
        this.xrLabel2.StylePriority.UseFont = false;
        this.xrLabel2.StylePriority.UseTextAlignment = false;
        this.xrLabel2.Text = "xrLabel2";
        this.xrLabel2.TextAlignment = DevExpress.XtraPrinting.TextAlignment.TopCenter;
        // 
        // xrPictureBox1
        // 
        this.xrPictureBox1.Image = ((System.Drawing.Image)(resources.GetObject("xrPictureBox1.Image")));
        this.xrPictureBox1.LocationFloat = new DevExpress.Utils.PointFloat(637.5001F, 1.589457E-05F);
        this.xrPictureBox1.Name = "xrPictureBox1";
        this.xrPictureBox1.SizeF = new System.Drawing.SizeF(102.4999F, 60.37499F);
        this.xrPictureBox1.Sizing = DevExpress.XtraPrinting.ImageSizeMode.StretchImage;
        // 
        // BottomMargin
        // 
        this.BottomMargin.HeightF = 23F;
        this.BottomMargin.Name = "BottomMargin";
        this.BottomMargin.Padding = new DevExpress.XtraPrinting.PaddingInfo(0, 0, 0, 0, 100F);
        this.BottomMargin.TextAlignment = DevExpress.XtraPrinting.TextAlignment.TopLeft;
        // 
        // employeeClaimApproved1
        // 
        this.employeeClaimApproved1.DataSetName = "EmployeeClaimApproved";
        this.employeeClaimApproved1.SchemaSerializationMode = System.Data.SchemaSerializationMode.IncludeSchema;
        // 
        // employeeLoanDataset1
        // 
        this.employeeLoanDataset1.DataSetName = "EmployeeLoanDataset";
        this.employeeLoanDataset1.SchemaSerializationMode = System.Data.SchemaSerializationMode.IncludeSchema;
        // 
        // loanDetailDataset1
        // 
        this.loanDetailDataset1.DataSetName = "LoanDetailDataset";
        this.loanDetailDataset1.SchemaSerializationMode = System.Data.SchemaSerializationMode.IncludeSchema;
        // 
        // loanDetailDataset2
        // 
        this.loanDetailDataset2.DataSetName = "LoanDetailDataset";
        this.loanDetailDataset2.SchemaSerializationMode = System.Data.SchemaSerializationMode.IncludeSchema;
        // 
        // GroupHeader1
        // 
        this.GroupHeader1.Controls.AddRange(new DevExpress.XtraReports.UI.XRControl[] {
            this.xrTable3,
            this.xrTable6,
            this.xrTable1});
        this.GroupHeader1.GroupFields.AddRange(new DevExpress.XtraReports.UI.GroupField[] {
            new DevExpress.XtraReports.UI.GroupField("Directory_Name", DevExpress.XtraReports.UI.XRColumnSortOrder.Ascending)});
        this.GroupHeader1.HeightF = 42.10415F;
        this.GroupHeader1.Name = "GroupHeader1";
        this.GroupHeader1.StylePriority.UseTextAlignment = false;
        this.GroupHeader1.TextAlignment = DevExpress.XtraPrinting.TextAlignment.TopLeft;
        // 
        // xrTable3
        // 
        this.xrTable3.Font = new System.Drawing.Font("Verdana", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
        this.xrTable3.LocationFloat = new DevExpress.Utils.PointFloat(0F, 10F);
        this.xrTable3.Name = "xrTable3";
        this.xrTable3.Rows.AddRange(new DevExpress.XtraReports.UI.XRTableRow[] {
            this.xrTableRow4});
        this.xrTable3.SizeF = new System.Drawing.SizeF(340.625F, 16.66666F);
        this.xrTable3.StylePriority.UseFont = false;
        // 
        // xrTableRow4
        // 
        this.xrTableRow4.Cells.AddRange(new DevExpress.XtraReports.UI.XRTableCell[] {
            this.xrTableCell5,
            this.xrTableCell6});
        this.xrTableRow4.Name = "xrTableRow4";
        this.xrTableRow4.Weight = 1;
        // 
        // xrTableCell5
        // 
        this.xrTableCell5.Name = "xrTableCell5";
        this.xrTableCell5.Text = "Directory Name :";
        this.xrTableCell5.Weight = 1.3645832824707032;
        // 
        // xrTableCell6
        // 
        this.xrTableCell6.DataBindings.AddRange(new DevExpress.XtraReports.UI.XRBinding[] {
            new DevExpress.XtraReports.UI.XRBinding("Text", null, "EmpDirectory.Directory_Name")});
        this.xrTableCell6.Font = new System.Drawing.Font("Verdana", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
        this.xrTableCell6.Name = "xrTableCell6";
        this.xrTableCell6.StylePriority.UseFont = false;
        this.xrTableCell6.Text = "xrTableCell6";
        this.xrTableCell6.Weight = 2.0416667175292966;
        // 
        // xrTable6
        // 
        this.xrTable6.Borders = ((DevExpress.XtraPrinting.BorderSide)((((DevExpress.XtraPrinting.BorderSide.Left | DevExpress.XtraPrinting.BorderSide.Top)
                    | DevExpress.XtraPrinting.BorderSide.Right)
                    | DevExpress.XtraPrinting.BorderSide.Bottom)));
        this.xrTable6.Font = new System.Drawing.Font("Verdana", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
        this.xrTable6.LocationFloat = new DevExpress.Utils.PointFloat(4.768372E-05F, 26.66667F);
        this.xrTable6.Name = "xrTable6";
        this.xrTable6.Rows.AddRange(new DevExpress.XtraReports.UI.XRTableRow[] {
            this.xrTableRow10});
        this.xrTable6.SizeF = new System.Drawing.SizeF(739.9998F, 15.43748F);
        this.xrTable6.StylePriority.UseBorders = false;
        this.xrTable6.StylePriority.UseFont = false;
        // 
        // xrTableRow10
        // 
        this.xrTableRow10.Cells.AddRange(new DevExpress.XtraReports.UI.XRTableCell[] {
            this.xrTableCell24,
            this.xrTableCell25,
            this.xrTableCell26,
            this.xrTableCell27});
        this.xrTableRow10.Name = "xrTableRow10";
        this.xrTableRow10.Weight = 1;
        // 
        // xrTableCell24
        // 
        this.xrTableCell24.Name = "xrTableCell24";
        this.xrTableCell24.StylePriority.UseTextAlignment = false;
        this.xrTableCell24.Text = "Document Name";
        this.xrTableCell24.TextAlignment = DevExpress.XtraPrinting.TextAlignment.TopLeft;
        this.xrTableCell24.Weight = 2.38541638093283;
        // 
        // xrTableCell25
        // 
        this.xrTableCell25.Name = "xrTableCell25";
        this.xrTableCell25.StylePriority.UseTextAlignment = false;
        this.xrTableCell25.Text = "File Name";
        this.xrTableCell25.TextAlignment = DevExpress.XtraPrinting.TextAlignment.TopLeft;
        this.xrTableCell25.Weight = 1.3541659456971162;
        // 
        // xrTableCell26
        // 
        this.xrTableCell26.Name = "xrTableCell26";
        this.xrTableCell26.StylePriority.UseTextAlignment = false;
        this.xrTableCell26.Text = "File Upload Date";
        this.xrTableCell26.TextAlignment = DevExpress.XtraPrinting.TextAlignment.TopCenter;
        this.xrTableCell26.Weight = 1.6979163041384673;
        // 
        // xrTableCell27
        // 
        this.xrTableCell27.Name = "xrTableCell27";
        this.xrTableCell27.StylePriority.UseTextAlignment = false;
        this.xrTableCell27.Text = "Expiry Date";
        this.xrTableCell27.TextAlignment = DevExpress.XtraPrinting.TextAlignment.TopCenter;
        this.xrTableCell27.Weight = 1.9624985553222383;
        // 
        // xrTable1
        // 
        this.xrTable1.Font = new System.Drawing.Font("Verdana", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
        this.xrTable1.LocationFloat = new DevExpress.Utils.PointFloat(440F, 10F);
        this.xrTable1.Name = "xrTable1";
        this.xrTable1.Rows.AddRange(new DevExpress.XtraReports.UI.XRTableRow[] {
            this.xrTableRow5});
        this.xrTable1.SizeF = new System.Drawing.SizeF(300F, 16.66667F);
        this.xrTable1.StylePriority.UseFont = false;
        // 
        // xrTableRow5
        // 
        this.xrTableRow5.Cells.AddRange(new DevExpress.XtraReports.UI.XRTableCell[] {
            this.xrTableCell7,
            this.xrTableCell13});
        this.xrTableRow5.Name = "xrTableRow5";
        this.xrTableRow5.Weight = 1;
        // 
        // xrTableCell7
        // 
        this.xrTableCell7.Name = "xrTableCell7";
        this.xrTableCell7.Text = "Creation Date :";
        this.xrTableCell7.Weight = 1.2479165240393135;
        // 
        // xrTableCell13
        // 
        this.xrTableCell13.DataBindings.AddRange(new DevExpress.XtraReports.UI.XRBinding[] {
            new DevExpress.XtraReports.UI.XRBinding("Text", null, "EmpDirectory.Directory_Created_Date")});
        this.xrTableCell13.Font = new System.Drawing.Font("Verdana", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
        this.xrTableCell13.Name = "xrTableCell13";
        this.xrTableCell13.StylePriority.UseFont = false;
        this.xrTableCell13.Text = "xrTableCell13";
        this.xrTableCell13.Weight = 1.7520834759606865;
        this.xrTableCell13.BeforePrint += new System.Drawing.Printing.PrintEventHandler(this.xrTableCell13_BeforePrint);
        // 
        // employeeDirectoryDataSet1
        // 
        this.employeeDirectoryDataSet1.DataSetName = "EmployeeDirectoryDataSet";
        this.employeeDirectoryDataSet1.SchemaSerializationMode = System.Data.SchemaSerializationMode.IncludeSchema;
        // 
        // PageFooter
        // 
        this.PageFooter.Controls.AddRange(new DevExpress.XtraReports.UI.XRControl[] {
            this.xrPageInfo2,
            this.xrPageInfo1});
        this.PageFooter.HeightF = 25F;
        this.PageFooter.Name = "PageFooter";
        // 
        // xrPageInfo1
        // 
        this.xrPageInfo1.LocationFloat = new DevExpress.Utils.PointFloat(6.357829E-05F, 0F);
        this.xrPageInfo1.Name = "xrPageInfo1";
        this.xrPageInfo1.Padding = new DevExpress.XtraPrinting.PaddingInfo(2, 2, 0, 0, 96F);
        this.xrPageInfo1.PageInfo = DevExpress.XtraPrinting.PageInfo.DateTime;
        this.xrPageInfo1.SizeF = new System.Drawing.SizeF(373.9583F, 22.99998F);
        // 
        // xrPageInfo2
        // 
        this.xrPageInfo2.Format = "Page{0}Of  {1}";
        this.xrPageInfo2.LocationFloat = new DevExpress.Utils.PointFloat(373.9583F, 0F);
        this.xrPageInfo2.Name = "xrPageInfo2";
        this.xrPageInfo2.Padding = new DevExpress.XtraPrinting.PaddingInfo(2, 2, 0, 0, 96F);
        this.xrPageInfo2.SizeF = new System.Drawing.SizeF(366.0416F, 22.99998F);
        this.xrPageInfo2.StylePriority.UseTextAlignment = false;
        this.xrPageInfo2.TextAlignment = DevExpress.XtraPrinting.TextAlignment.TopRight;
        // 
        // DirectoryWiseReport
        // 
        this.Bands.AddRange(new DevExpress.XtraReports.UI.Band[] {
            this.Detail,
            this.TopMargin,
            this.BottomMargin,
            this.GroupHeader1,
            this.PageFooter});
        this.DataMember = "EmpDirectory";
        this.DataSource = this.employeeDirectoryDataSet1;
        this.Margins = new System.Drawing.Printing.Margins(67, 10, 61, 23);
        this.Version = "10.2";
        ((System.ComponentModel.ISupportInitialize)(this.xrTable2)).EndInit();
        ((System.ComponentModel.ISupportInitialize)(this.employeeClaimApproved1)).EndInit();
        ((System.ComponentModel.ISupportInitialize)(this.employeeLoanDataset1)).EndInit();
        ((System.ComponentModel.ISupportInitialize)(this.loanDetailDataset1)).EndInit();
        ((System.ComponentModel.ISupportInitialize)(this.loanDetailDataset2)).EndInit();
        ((System.ComponentModel.ISupportInitialize)(this.xrTable3)).EndInit();
        ((System.ComponentModel.ISupportInitialize)(this.xrTable6)).EndInit();
        ((System.ComponentModel.ISupportInitialize)(this.xrTable1)).EndInit();
        ((System.ComponentModel.ISupportInitialize)(this.employeeDirectoryDataSet1)).EndInit();
        ((System.ComponentModel.ISupportInitialize)(this)).EndInit();

	}

	#endregion

    private void xrTableCell10_BeforePrint(object sender, System.Drawing.Printing.PrintEventArgs e)
    {
        if (xrTableCell10.Text != "")
        {
            xrTableCell10.Text = Convert.ToDateTime(xrTableCell10.Text).ToString("dd-MMM-yyyy");
        }
    }

    private void xrTableCell11_BeforePrint(object sender, System.Drawing.Printing.PrintEventArgs e)
    {
        if (xrTableCell11.Text != "")
        {
            xrTableCell11.Text = Convert.ToDateTime(xrTableCell11.Text).ToString("dd-MMM-yyyy");
        }
    }

    private void xrTableCell13_BeforePrint(object sender, System.Drawing.Printing.PrintEventArgs e)
    {
        if (xrTableCell13.Text != "")
        {
            xrTableCell13.Text = Convert.ToDateTime(xrTableCell13.Text).ToString("dd-MMM-yyyy");
        }
    }
}
