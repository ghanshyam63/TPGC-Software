using System;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using DevExpress.XtraReports.UI;

/// <summary>
/// Summary description for EmployeePFReport
/// </summary>
public class EmployeePFReport : DevExpress.XtraReports.UI.XtraReport
{
	private DevExpress.XtraReports.UI.DetailBand Detail;
	private DevExpress.XtraReports.UI.TopMarginBand TopMargin;
	private DevExpress.XtraReports.UI.BottomMarginBand BottomMargin;
    private GroupHeaderBand GroupHeader1;
    private XRPanel xrPanel2;
    private XRLabel xrLabel9;
    private XRLabel xrLabel8;
    private XRLabel xrLabel3;
    private XRLabel xrLabel4;
    private XRLabel xrLabel2;
    private XRPictureBox xrPictureBox1;
    private XRLabel xrLabel1;
    private XRPanel xrPanel1;
    private XRTable xrTable1;
    private XRTableRow xrTableRow1;
    private XRTableCell xrTableCell1;
    private XRTableCell xrTableCell2;
    private XRTableCell xrTableCell3;
    private XRTableCell xrTableCell4;
    private XRTableCell xrTableCell5;
    private XRTableCell xrTableCell6;
    private GroupFooterBand GroupFooter1;
    private XRPanel xrPanel3;
    private XRTable xrTable2;
    private XRTableRow xrTableRow2;
    private XRTableCell xrTableCell7;
    private XRTableCell xrTableCell8;
    private XRTableCell xrTableCell9;
    private XRTableCell xrTableCell10;
    private XRTableCell xrTableCell11;
    private XRTableCell xrTableCell12;
    private XRPanel xrPanel4;
    private XRTable xrTable3;
    private XRTableRow xrTableRow3;
    private XRTableCell xrTableCell13;
    private XRTableCell xrTableCell15;
    private XRTableCell xrTableCell16;
    private XRTableCell xrTableCell17;
    private XRTableCell xrTableCell18;
    private EpmloyeePF_DataSet epmloyeePF_DataSet1;
    private XRPageBreak xrPageBreak1;
	/// <summary>
	/// Required designer variable.
	/// </summary>
	private System.ComponentModel.IContainer components = null;

	public EmployeePFReport()
	{
		InitializeComponent();
		//
		// TODO: Add constructor logic here
		//
	}


    public void setimage(string url)
    {
        xrPictureBox1.ImageUrl = url;
        
    }
   

    public void setcompanyname(string companyname)
    {
        xrLabel1.Text = companyname;
      
    }
    public void setcompanyAddress(string Address)
    {
        xrLabel2.Text = Address;
        
    }
    public void setcontact(string Contact)
    {
        xrLabel4.Text = Contact;
        
    }

    public void setwebsite(string web)
    {
        xrLabel8.Text = web;

    }
    public void setmailid(string mailid)
    {
        xrLabel9.Text = mailid;

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
        string resourceFileName = "EmployeePFReport.resx";
        DevExpress.XtraReports.UI.XRSummary xrSummary1 = new DevExpress.XtraReports.UI.XRSummary();
        DevExpress.XtraReports.UI.XRSummary xrSummary2 = new DevExpress.XtraReports.UI.XRSummary();
        DevExpress.XtraReports.UI.XRSummary xrSummary3 = new DevExpress.XtraReports.UI.XRSummary();
        DevExpress.XtraReports.UI.XRSummary xrSummary4 = new DevExpress.XtraReports.UI.XRSummary();
        this.Detail = new DevExpress.XtraReports.UI.DetailBand();
        this.xrPanel3 = new DevExpress.XtraReports.UI.XRPanel();
        this.xrTable2 = new DevExpress.XtraReports.UI.XRTable();
        this.xrTableRow2 = new DevExpress.XtraReports.UI.XRTableRow();
        this.xrTableCell7 = new DevExpress.XtraReports.UI.XRTableCell();
        this.xrTableCell8 = new DevExpress.XtraReports.UI.XRTableCell();
        this.xrTableCell9 = new DevExpress.XtraReports.UI.XRTableCell();
        this.xrTableCell10 = new DevExpress.XtraReports.UI.XRTableCell();
        this.xrTableCell11 = new DevExpress.XtraReports.UI.XRTableCell();
        this.xrTableCell12 = new DevExpress.XtraReports.UI.XRTableCell();
        this.TopMargin = new DevExpress.XtraReports.UI.TopMarginBand();
        this.xrPanel2 = new DevExpress.XtraReports.UI.XRPanel();
        this.xrLabel9 = new DevExpress.XtraReports.UI.XRLabel();
        this.xrLabel8 = new DevExpress.XtraReports.UI.XRLabel();
        this.xrLabel3 = new DevExpress.XtraReports.UI.XRLabel();
        this.xrLabel4 = new DevExpress.XtraReports.UI.XRLabel();
        this.xrLabel2 = new DevExpress.XtraReports.UI.XRLabel();
        this.xrPictureBox1 = new DevExpress.XtraReports.UI.XRPictureBox();
        this.xrLabel1 = new DevExpress.XtraReports.UI.XRLabel();
        this.BottomMargin = new DevExpress.XtraReports.UI.BottomMarginBand();
        this.GroupHeader1 = new DevExpress.XtraReports.UI.GroupHeaderBand();
        this.xrPanel1 = new DevExpress.XtraReports.UI.XRPanel();
        this.xrTable1 = new DevExpress.XtraReports.UI.XRTable();
        this.xrTableRow1 = new DevExpress.XtraReports.UI.XRTableRow();
        this.xrTableCell1 = new DevExpress.XtraReports.UI.XRTableCell();
        this.xrTableCell2 = new DevExpress.XtraReports.UI.XRTableCell();
        this.xrTableCell4 = new DevExpress.XtraReports.UI.XRTableCell();
        this.xrTableCell5 = new DevExpress.XtraReports.UI.XRTableCell();
        this.xrTableCell6 = new DevExpress.XtraReports.UI.XRTableCell();
        this.xrTableCell3 = new DevExpress.XtraReports.UI.XRTableCell();
        this.GroupFooter1 = new DevExpress.XtraReports.UI.GroupFooterBand();
        this.xrPageBreak1 = new DevExpress.XtraReports.UI.XRPageBreak();
        this.xrPanel4 = new DevExpress.XtraReports.UI.XRPanel();
        this.xrTable3 = new DevExpress.XtraReports.UI.XRTable();
        this.xrTableRow3 = new DevExpress.XtraReports.UI.XRTableRow();
        this.xrTableCell13 = new DevExpress.XtraReports.UI.XRTableCell();
        this.xrTableCell15 = new DevExpress.XtraReports.UI.XRTableCell();
        this.xrTableCell16 = new DevExpress.XtraReports.UI.XRTableCell();
        this.xrTableCell17 = new DevExpress.XtraReports.UI.XRTableCell();
        this.xrTableCell18 = new DevExpress.XtraReports.UI.XRTableCell();
        this.epmloyeePF_DataSet1 = new EpmloyeePF_DataSet();
        ((System.ComponentModel.ISupportInitialize)(this.xrTable2)).BeginInit();
        ((System.ComponentModel.ISupportInitialize)(this.xrTable1)).BeginInit();
        ((System.ComponentModel.ISupportInitialize)(this.xrTable3)).BeginInit();
        ((System.ComponentModel.ISupportInitialize)(this.epmloyeePF_DataSet1)).BeginInit();
        ((System.ComponentModel.ISupportInitialize)(this)).BeginInit();
        // 
        // Detail
        // 
        this.Detail.Controls.AddRange(new DevExpress.XtraReports.UI.XRControl[] {
            this.xrPanel3});
        this.Detail.HeightF = 16.04168F;
        this.Detail.Name = "Detail";
        this.Detail.Padding = new DevExpress.XtraPrinting.PaddingInfo(0, 0, 0, 0, 100F);
        this.Detail.TextAlignment = DevExpress.XtraPrinting.TextAlignment.TopLeft;
        // 
        // xrPanel3
        // 
        this.xrPanel3.Borders = ((DevExpress.XtraPrinting.BorderSide)((((DevExpress.XtraPrinting.BorderSide.Left | DevExpress.XtraPrinting.BorderSide.Top)
                    | DevExpress.XtraPrinting.BorderSide.Right)
                    | DevExpress.XtraPrinting.BorderSide.Bottom)));
        this.xrPanel3.Controls.AddRange(new DevExpress.XtraReports.UI.XRControl[] {
            this.xrTable2});
        this.xrPanel3.LocationFloat = new DevExpress.Utils.PointFloat(46.875F, 0F);
        this.xrPanel3.Name = "xrPanel3";
        this.xrPanel3.SizeF = new System.Drawing.SizeF(544.3309F, 16.04168F);
        this.xrPanel3.StylePriority.UseBorders = false;
        // 
        // xrTable2
        // 
        this.xrTable2.LocationFloat = new DevExpress.Utils.PointFloat(9.916718F, 0F);
        this.xrTable2.Name = "xrTable2";
        this.xrTable2.Rows.AddRange(new DevExpress.XtraReports.UI.XRTableRow[] {
            this.xrTableRow2});
        this.xrTable2.SizeF = new System.Drawing.SizeF(534.4142F, 16.04168F);
        // 
        // xrTableRow2
        // 
        this.xrTableRow2.Cells.AddRange(new DevExpress.XtraReports.UI.XRTableCell[] {
            this.xrTableCell7,
            this.xrTableCell8,
            this.xrTableCell9,
            this.xrTableCell10,
            this.xrTableCell11,
            this.xrTableCell12});
        this.xrTableRow2.Name = "xrTableRow2";
        this.xrTableRow2.Weight = 1;
        // 
        // xrTableCell7
        // 
        this.xrTableCell7.Borders = DevExpress.XtraPrinting.BorderSide.Right;
        this.xrTableCell7.DataBindings.AddRange(new DevExpress.XtraReports.UI.XRBinding[] {
            new DevExpress.XtraReports.UI.XRBinding("Text", null, "dtEmployeePF.EmpName")});
        this.xrTableCell7.Name = "xrTableCell7";
        this.xrTableCell7.StylePriority.UseBorders = false;
        this.xrTableCell7.Text = "Employee Name";
        this.xrTableCell7.Weight = 0.59821075159829928;
        // 
        // xrTableCell8
        // 
        this.xrTableCell8.Borders = DevExpress.XtraPrinting.BorderSide.Right;
        this.xrTableCell8.DataBindings.AddRange(new DevExpress.XtraReports.UI.XRBinding[] {
            new DevExpress.XtraReports.UI.XRBinding("Text", null, "dtEmployeePF.EmpPFAccountNo")});
        this.xrTableCell8.Name = "xrTableCell8";
        this.xrTableCell8.StylePriority.UseBorders = false;
        this.xrTableCell8.Text = "PF Account No.";
        this.xrTableCell8.Weight = 0.631315980003414;
        // 
        // xrTableCell9
        // 
        this.xrTableCell9.Borders = DevExpress.XtraPrinting.BorderSide.Right;
        this.xrTableCell9.DataBindings.AddRange(new DevExpress.XtraReports.UI.XRBinding[] {
            new DevExpress.XtraReports.UI.XRBinding("Text", null, "dtEmployeePF.EmpBasicSal")});
        this.xrTableCell9.Name = "xrTableCell9";
        this.xrTableCell9.StylePriority.UseBorders = false;
        this.xrTableCell9.Text = "Basic Salary";
        this.xrTableCell9.Weight = 0.49543314610146355;
        // 
        // xrTableCell10
        // 
        this.xrTableCell10.Borders = DevExpress.XtraPrinting.BorderSide.Right;
        this.xrTableCell10.DataBindings.AddRange(new DevExpress.XtraReports.UI.XRBinding[] {
            new DevExpress.XtraReports.UI.XRBinding("Text", null, "dtEmployeePF.EmployeeContributionPF")});
        this.xrTableCell10.Multiline = true;
        this.xrTableCell10.Name = "xrTableCell10";
        this.xrTableCell10.StylePriority.UseBorders = false;
        this.xrTableCell10.Text = "Employee Contribution PF";
        this.xrTableCell10.Weight = 0.76462201060791735;
        // 
        // xrTableCell11
        // 
        this.xrTableCell11.Borders = DevExpress.XtraPrinting.BorderSide.Right;
        this.xrTableCell11.DataBindings.AddRange(new DevExpress.XtraReports.UI.XRBinding[] {
            new DevExpress.XtraReports.UI.XRBinding("Text", null, "dtEmployeePF.EmployerContributionPF")});
        this.xrTableCell11.Name = "xrTableCell11";
        this.xrTableCell11.StylePriority.UseBorders = false;
        this.xrTableCell11.Text = "Employer Contribution PF";
        this.xrTableCell11.Weight = 0.81595327613681645;
        // 
        // xrTableCell12
        // 
        this.xrTableCell12.Borders = DevExpress.XtraPrinting.BorderSide.Right;
        this.xrTableCell12.DataBindings.AddRange(new DevExpress.XtraReports.UI.XRBinding[] {
            new DevExpress.XtraReports.UI.XRBinding("Text", null, "dtEmployeePF.TotalAmount")});
        this.xrTableCell12.Name = "xrTableCell12";
        this.xrTableCell12.StylePriority.UseBorders = false;
        this.xrTableCell12.Text = "Total";
        this.xrTableCell12.Weight = 0.29349085017020976;
        // 
        // TopMargin
        // 
        this.TopMargin.Controls.AddRange(new DevExpress.XtraReports.UI.XRControl[] {
            this.xrPanel2});
        this.TopMargin.HeightF = 130F;
        this.TopMargin.Name = "TopMargin";
        this.TopMargin.Padding = new DevExpress.XtraPrinting.PaddingInfo(0, 0, 0, 0, 100F);
        this.TopMargin.TextAlignment = DevExpress.XtraPrinting.TextAlignment.TopLeft;
        // 
        // xrPanel2
        // 
        this.xrPanel2.Controls.AddRange(new DevExpress.XtraReports.UI.XRControl[] {
            this.xrLabel9,
            this.xrLabel8,
            this.xrLabel3,
            this.xrLabel4,
            this.xrLabel2,
            this.xrPictureBox1,
            this.xrLabel1});
        this.xrPanel2.LocationFloat = new DevExpress.Utils.PointFloat(46.875F, 29.16667F);
        this.xrPanel2.Name = "xrPanel2";
        this.xrPanel2.SizeF = new System.Drawing.SizeF(544.3309F, 100.4583F);
        // 
        // xrLabel9
        // 
        this.xrLabel9.Borders = DevExpress.XtraPrinting.BorderSide.None;
        this.xrLabel9.Font = new System.Drawing.Font("Verdana", 6F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
        this.xrLabel9.LocationFloat = new DevExpress.Utils.PointFloat(9.916718F, 66.41667F);
        this.xrLabel9.Name = "xrLabel9";
        this.xrLabel9.Padding = new DevExpress.XtraPrinting.PaddingInfo(2, 2, 0, 0, 100F);
        this.xrLabel9.SizeF = new System.Drawing.SizeF(164.5833F, 10.49997F);
        this.xrLabel9.StylePriority.UseBorders = false;
        this.xrLabel9.StylePriority.UseFont = false;
        this.xrLabel9.Text = "xrLabel9";
        // 
        // xrLabel8
        // 
        this.xrLabel8.Borders = DevExpress.XtraPrinting.BorderSide.None;
        this.xrLabel8.Font = new System.Drawing.Font("Verdana", 6F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
        this.xrLabel8.LocationFloat = new DevExpress.Utils.PointFloat(355.2031F, 66.41667F);
        this.xrLabel8.Name = "xrLabel8";
        this.xrLabel8.Padding = new DevExpress.XtraPrinting.PaddingInfo(2, 2, 0, 0, 100F);
        this.xrLabel8.SizeF = new System.Drawing.SizeF(166.1884F, 10.49998F);
        this.xrLabel8.StylePriority.UseBorders = false;
        this.xrLabel8.StylePriority.UseFont = false;
        this.xrLabel8.Text = "xrLabel8";
        // 
        // xrLabel3
        // 
        this.xrLabel3.Borders = DevExpress.XtraPrinting.BorderSide.None;
        this.xrLabel3.Font = new System.Drawing.Font("Verdana", 6.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
        this.xrLabel3.LocationFloat = new DevExpress.Utils.PointFloat(354.159F, 51.75005F);
        this.xrLabel3.Name = "xrLabel3";
        this.xrLabel3.Padding = new DevExpress.XtraPrinting.PaddingInfo(2, 2, 0, 0, 100F);
        this.xrLabel3.SizeF = new System.Drawing.SizeF(57.72382F, 14.66664F);
        this.xrLabel3.StylePriority.UseBorders = false;
        this.xrLabel3.StylePriority.UseFont = false;
        this.xrLabel3.StylePriority.UseTextAlignment = false;
        this.xrLabel3.Text = "Phone No :";
        this.xrLabel3.TextAlignment = DevExpress.XtraPrinting.TextAlignment.TopLeft;
        // 
        // xrLabel4
        // 
        this.xrLabel4.Borders = DevExpress.XtraPrinting.BorderSide.None;
        this.xrLabel4.Font = new System.Drawing.Font("Verdana", 6.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
        this.xrLabel4.LocationFloat = new DevExpress.Utils.PointFloat(411.8828F, 51.75004F);
        this.xrLabel4.Name = "xrLabel4";
        this.xrLabel4.Padding = new DevExpress.XtraPrinting.PaddingInfo(2, 2, 0, 0, 100F);
        this.xrLabel4.SizeF = new System.Drawing.SizeF(114.6494F, 14.66664F);
        this.xrLabel4.StylePriority.UseBorders = false;
        this.xrLabel4.StylePriority.UseFont = false;
        this.xrLabel4.Text = "xrLabel4";
        // 
        // xrLabel2
        // 
        this.xrLabel2.Borders = DevExpress.XtraPrinting.BorderSide.None;
        this.xrLabel2.Font = new System.Drawing.Font("Verdana", 6.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
        this.xrLabel2.LocationFloat = new DevExpress.Utils.PointFloat(354.159F, 41.33335F);
        this.xrLabel2.Name = "xrLabel2";
        this.xrLabel2.Padding = new DevExpress.XtraPrinting.PaddingInfo(2, 2, 0, 0, 100F);
        this.xrLabel2.SizeF = new System.Drawing.SizeF(178.9761F, 10.41668F);
        this.xrLabel2.StylePriority.UseBorders = false;
        this.xrLabel2.StylePriority.UseFont = false;
        this.xrLabel2.StylePriority.UseTextAlignment = false;
        this.xrLabel2.Text = "xrLabel2";
        this.xrLabel2.TextAlignment = DevExpress.XtraPrinting.TextAlignment.TopLeft;
        // 
        // xrPictureBox1
        // 
        this.xrPictureBox1.Borders = DevExpress.XtraPrinting.BorderSide.None;
        this.xrPictureBox1.LocationFloat = new DevExpress.Utils.PointFloat(10.00025F, 10.00002F);
        this.xrPictureBox1.Name = "xrPictureBox1";
        this.xrPictureBox1.SizeF = new System.Drawing.SizeF(72.9167F, 41.75001F);
        this.xrPictureBox1.Sizing = DevExpress.XtraPrinting.ImageSizeMode.StretchImage;
        this.xrPictureBox1.StylePriority.UseBorders = false;
        // 
        // xrLabel1
        // 
        this.xrLabel1.Borders = DevExpress.XtraPrinting.BorderSide.None;
        this.xrLabel1.Font = new System.Drawing.Font("Verdana", 10F, ((System.Drawing.FontStyle)((System.Drawing.FontStyle.Bold | System.Drawing.FontStyle.Underline))));
        this.xrLabel1.LocationFloat = new DevExpress.Utils.PointFloat(149.4815F, 28.75002F);
        this.xrLabel1.Name = "xrLabel1";
        this.xrLabel1.Padding = new DevExpress.XtraPrinting.PaddingInfo(2, 2, 0, 0, 100F);
        this.xrLabel1.SizeF = new System.Drawing.SizeF(241.2119F, 12.58333F);
        this.xrLabel1.StylePriority.UseBorders = false;
        this.xrLabel1.StylePriority.UseFont = false;
        this.xrLabel1.StylePriority.UseTextAlignment = false;
        this.xrLabel1.Text = "xrLabel1";
        this.xrLabel1.TextAlignment = DevExpress.XtraPrinting.TextAlignment.TopCenter;
        // 
        // BottomMargin
        // 
        this.BottomMargin.HeightF = 29F;
        this.BottomMargin.Name = "BottomMargin";
        this.BottomMargin.Padding = new DevExpress.XtraPrinting.PaddingInfo(0, 0, 0, 0, 100F);
        this.BottomMargin.TextAlignment = DevExpress.XtraPrinting.TextAlignment.TopLeft;
        // 
        // GroupHeader1
        // 
        this.GroupHeader1.Controls.AddRange(new DevExpress.XtraReports.UI.XRControl[] {
            this.xrPanel1});
        this.GroupHeader1.HeightF = 14.37499F;
        this.GroupHeader1.Name = "GroupHeader1";
        // 
        // xrPanel1
        // 
        this.xrPanel1.Borders = ((DevExpress.XtraPrinting.BorderSide)((((DevExpress.XtraPrinting.BorderSide.Left | DevExpress.XtraPrinting.BorderSide.Top)
                    | DevExpress.XtraPrinting.BorderSide.Right)
                    | DevExpress.XtraPrinting.BorderSide.Bottom)));
        this.xrPanel1.Controls.AddRange(new DevExpress.XtraReports.UI.XRControl[] {
            this.xrTable1});
        this.xrPanel1.LocationFloat = new DevExpress.Utils.PointFloat(46.875F, 0F);
        this.xrPanel1.Name = "xrPanel1";
        this.xrPanel1.SizeF = new System.Drawing.SizeF(544.3309F, 14.37499F);
        this.xrPanel1.StylePriority.UseBorders = false;
        // 
        // xrTable1
        // 
        this.xrTable1.LocationFloat = new DevExpress.Utils.PointFloat(3.051758E-05F, 0F);
        this.xrTable1.Name = "xrTable1";
        this.xrTable1.Rows.AddRange(new DevExpress.XtraReports.UI.XRTableRow[] {
            this.xrTableRow1});
        this.xrTable1.SizeF = new System.Drawing.SizeF(544.3307F, 14.37499F);
        // 
        // xrTableRow1
        // 
        this.xrTableRow1.Cells.AddRange(new DevExpress.XtraReports.UI.XRTableCell[] {
            this.xrTableCell1,
            this.xrTableCell2,
            this.xrTableCell4,
            this.xrTableCell5,
            this.xrTableCell6,
            this.xrTableCell3});
        this.xrTableRow1.Name = "xrTableRow1";
        this.xrTableRow1.Weight = 1;
        // 
        // xrTableCell1
        // 
        this.xrTableCell1.Borders = DevExpress.XtraPrinting.BorderSide.Right;
        this.xrTableCell1.Name = "xrTableCell1";
        this.xrTableCell1.StylePriority.UseBorders = false;
        this.xrTableCell1.Text = "  Employee Name";
        this.xrTableCell1.Weight = 0.65287996087899758;
        // 
        // xrTableCell2
        // 
        this.xrTableCell2.Borders = DevExpress.XtraPrinting.BorderSide.Right;
        this.xrTableCell2.Name = "xrTableCell2";
        this.xrTableCell2.StylePriority.UseBorders = false;
        this.xrTableCell2.Text = "PF Account No.";
        this.xrTableCell2.Weight = 0.61981412834605254;
        // 
        // xrTableCell4
        // 
        this.xrTableCell4.Borders = DevExpress.XtraPrinting.BorderSide.Right;
        this.xrTableCell4.Name = "xrTableCell4";
        this.xrTableCell4.StylePriority.UseBorders = false;
        this.xrTableCell4.Text = "Basic Salary";
        this.xrTableCell4.Weight = 0.48640849539808761;
        // 
        // xrTableCell5
        // 
        this.xrTableCell5.Borders = DevExpress.XtraPrinting.BorderSide.Right;
        this.xrTableCell5.Multiline = true;
        this.xrTableCell5.Name = "xrTableCell5";
        this.xrTableCell5.StylePriority.UseBorders = false;
        this.xrTableCell5.Text = "Employee Contribution PF";
        this.xrTableCell5.Weight = 0.75069197485322914;
        // 
        // xrTableCell6
        // 
        this.xrTableCell6.Borders = DevExpress.XtraPrinting.BorderSide.Right;
        this.xrTableCell6.Name = "xrTableCell6";
        this.xrTableCell6.StylePriority.UseBorders = false;
        this.xrTableCell6.Text = "Employer Contribution PF";
        this.xrTableCell6.Weight = 0.80108827210055111;
        // 
        // xrTableCell3
        // 
        this.xrTableCell3.Borders = DevExpress.XtraPrinting.BorderSide.Right;
        this.xrTableCell3.Name = "xrTableCell3";
        this.xrTableCell3.StylePriority.UseBorders = false;
        this.xrTableCell3.Text = "Total";
        this.xrTableCell3.Weight = 0.28814318304120246;
        // 
        // GroupFooter1
        // 
        this.GroupFooter1.Controls.AddRange(new DevExpress.XtraReports.UI.XRControl[] {
            this.xrPageBreak1,
            this.xrPanel4});
        this.GroupFooter1.HeightF = 20.12499F;
        this.GroupFooter1.Name = "GroupFooter1";
        // 
        // xrPageBreak1
        // 
        this.xrPageBreak1.LocationFloat = new DevExpress.Utils.PointFloat(0F, 18.12499F);
        this.xrPageBreak1.Name = "xrPageBreak1";
        // 
        // xrPanel4
        // 
        this.xrPanel4.Borders = ((DevExpress.XtraPrinting.BorderSide)((((DevExpress.XtraPrinting.BorderSide.Left | DevExpress.XtraPrinting.BorderSide.Top)
                    | DevExpress.XtraPrinting.BorderSide.Right)
                    | DevExpress.XtraPrinting.BorderSide.Bottom)));
        this.xrPanel4.Controls.AddRange(new DevExpress.XtraReports.UI.XRControl[] {
            this.xrTable3});
        this.xrPanel4.LocationFloat = new DevExpress.Utils.PointFloat(46.87514F, 0F);
        this.xrPanel4.Name = "xrPanel4";
        this.xrPanel4.SizeF = new System.Drawing.SizeF(544.3309F, 18.12499F);
        this.xrPanel4.StylePriority.UseBorders = false;
        // 
        // xrTable3
        // 
        this.xrTable3.LocationFloat = new DevExpress.Utils.PointFloat(0.0001449585F, 0F);
        this.xrTable3.Name = "xrTable3";
        this.xrTable3.Rows.AddRange(new DevExpress.XtraReports.UI.XRTableRow[] {
            this.xrTableRow3});
        this.xrTable3.SizeF = new System.Drawing.SizeF(544.3308F, 18.12499F);
        // 
        // xrTableRow3
        // 
        this.xrTableRow3.Cells.AddRange(new DevExpress.XtraReports.UI.XRTableCell[] {
            this.xrTableCell13,
            this.xrTableCell15,
            this.xrTableCell16,
            this.xrTableCell17,
            this.xrTableCell18});
        this.xrTableRow3.Name = "xrTableRow3";
        this.xrTableRow3.Weight = 1;
        // 
        // xrTableCell13
        // 
        this.xrTableCell13.Borders = DevExpress.XtraPrinting.BorderSide.Right;
        this.xrTableCell13.Font = new System.Drawing.Font("Verdana", 9F, System.Drawing.FontStyle.Bold);
        this.xrTableCell13.Name = "xrTableCell13";
        this.xrTableCell13.StylePriority.UseBorders = false;
        this.xrTableCell13.StylePriority.UseFont = false;
        this.xrTableCell13.Text = " Gross Total";
        this.xrTableCell13.Weight = 1.2726929694053544;
        // 
        // xrTableCell15
        // 
        this.xrTableCell15.Borders = DevExpress.XtraPrinting.BorderSide.Right;
        this.xrTableCell15.DataBindings.AddRange(new DevExpress.XtraReports.UI.XRBinding[] {
            new DevExpress.XtraReports.UI.XRBinding("Text", null, "dtEmployeePF.EmpBasicSal")});
        this.xrTableCell15.Name = "xrTableCell15";
        this.xrTableCell15.StylePriority.UseBorders = false;
        xrSummary1.Running = DevExpress.XtraReports.UI.SummaryRunning.Group;
        this.xrTableCell15.Summary = xrSummary1;
        this.xrTableCell15.Text = "xrTableCell15";
        this.xrTableCell15.Weight = 0.48640961521778336;
        // 
        // xrTableCell16
        // 
        this.xrTableCell16.Borders = DevExpress.XtraPrinting.BorderSide.Right;
        this.xrTableCell16.DataBindings.AddRange(new DevExpress.XtraReports.UI.XRBinding[] {
            new DevExpress.XtraReports.UI.XRBinding("Text", null, "dtEmployeePF.EmployeeContributionPF")});
        this.xrTableCell16.Multiline = true;
        this.xrTableCell16.Name = "xrTableCell16";
        this.xrTableCell16.StylePriority.UseBorders = false;
        xrSummary2.Running = DevExpress.XtraReports.UI.SummaryRunning.Group;
        this.xrTableCell16.Summary = xrSummary2;
        this.xrTableCell16.Text = "xrTableCell16";
        this.xrTableCell16.Weight = 0.75069197485322914;
        // 
        // xrTableCell17
        // 
        this.xrTableCell17.Borders = DevExpress.XtraPrinting.BorderSide.Right;
        this.xrTableCell17.DataBindings.AddRange(new DevExpress.XtraReports.UI.XRBinding[] {
            new DevExpress.XtraReports.UI.XRBinding("Text", null, "dtEmployeePF.EmployerContributionPF")});
        this.xrTableCell17.Name = "xrTableCell17";
        this.xrTableCell17.StylePriority.UseBorders = false;
        xrSummary3.Running = DevExpress.XtraReports.UI.SummaryRunning.Group;
        this.xrTableCell17.Summary = xrSummary3;
        this.xrTableCell17.Text = "xrTableCell17";
        this.xrTableCell17.Weight = 0.80108827210055111;
        // 
        // xrTableCell18
        // 
        this.xrTableCell18.Borders = DevExpress.XtraPrinting.BorderSide.Right;
        this.xrTableCell18.DataBindings.AddRange(new DevExpress.XtraReports.UI.XRBinding[] {
            new DevExpress.XtraReports.UI.XRBinding("Text", null, "dtEmployeePF.TotalAmount")});
        this.xrTableCell18.Name = "xrTableCell18";
        this.xrTableCell18.StylePriority.UseBorders = false;
        xrSummary4.Running = DevExpress.XtraReports.UI.SummaryRunning.Group;
        this.xrTableCell18.Summary = xrSummary4;
        this.xrTableCell18.Text = "xrTableCell18";
        this.xrTableCell18.Weight = 0.28814318304120246;
        // 
        // epmloyeePF_DataSet1
        // 
        this.epmloyeePF_DataSet1.DataSetName = "EpmloyeePF_DataSet";
        this.epmloyeePF_DataSet1.SchemaSerializationMode = System.Data.SchemaSerializationMode.IncludeSchema;
        // 
        // EmployeePFReport
        // 
        this.Bands.AddRange(new DevExpress.XtraReports.UI.Band[] {
            this.Detail,
            this.TopMargin,
            this.BottomMargin,
            this.GroupHeader1,
            this.GroupFooter1});
        this.DataMember = "dtEmployeePF";
        this.DataSource = this.epmloyeePF_DataSet1;
        this.Margins = new System.Drawing.Printing.Margins(100, 100, 130, 29);
        this.Version = "10.2";
        ((System.ComponentModel.ISupportInitialize)(this.xrTable2)).EndInit();
        ((System.ComponentModel.ISupportInitialize)(this.xrTable1)).EndInit();
        ((System.ComponentModel.ISupportInitialize)(this.xrTable3)).EndInit();
        ((System.ComponentModel.ISupportInitialize)(this.epmloyeePF_DataSet1)).EndInit();
        ((System.ComponentModel.ISupportInitialize)(this)).EndInit();

	}

	#endregion
}
