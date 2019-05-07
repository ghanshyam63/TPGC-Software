using System;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using DevExpress.XtraReports.UI;

/// <summary>
/// Summary description for Att_ShiftReport
/// </summary>
public class Att_SummaryReport : DevExpress.XtraReports.UI.XtraReport
{
    Set_Employee_Holiday objEmpHoli = new Set_Employee_Holiday();

	private DevExpress.XtraReports.UI.DetailBand Detail;
	private DevExpress.XtraReports.UI.TopMarginBand TopMargin;
    private DevExpress.XtraReports.UI.BottomMarginBand BottomMargin;
    private PageFooterBand PageFooter;
    private XRTable xrTable2;
    private XRTableRow xrTableRow2;
    private XRTableCell xrTableCell5;
    private XRTableCell xrTableCell8;
    private AttendanceDataSet attendanceDataSet1;
   
    private ReportHeaderBand ReportHeader;
    private XRPageInfo xrPageInfo2;
    private XRPageInfo xrPageInfo1;
    private XRPanel xrPanel1;
    private XRLabel xrCompAddress;
    private XRLabel xrTitle;
    private XRPictureBox xrPictureBox1;
    private XRLabel xrCompName;
    private XRControlStyle xrControlStyle1;
    private XRTableCell xrTableCell11;
    private XRTableCell xrTableCell12;

   
    private XRTableCell xrTableCell13;
    private XRTableCell xrTableCell14;
    private XRTableCell xrTableCell15;
    private XRTableCell xrTableCell16;
    private PageHeaderBand PageHeader;
    private XRTable xrTable1;
    private XRTableRow xrTableRow1;
    private XRTableCell xrTableCell2;
    private XRTableCell xrTableCell1;
    private XRTableCell xrTableCell3;
    private XRTableCell xrTableCell4;
    private XRTableCell xrTableCell9;
    private XRTableCell xrTableCell10;
    private XRTableCell xrTableCell7;
    private XRTableCell xrTableCell17;
    private XRTableCell xrTableCell18;
    private XRTableCell xrTableCell19;
    private XRTableCell xrTableCell20;
    private XRTableCell xrTableCell21;
    private XRTableCell xrTableCell22;
    private XRTableRow xrTableRow3;
    private XRTableCell xrTableCell36;
    private XRTableCell xrTableCell28;
    private XRTableCell xrTableCell33;
    private XRTableCell xrTableCell24;
    private XRTableCell xrTableCell23;
    private XRTableCell xrTableCell6;
    private AttendanceDataSetTableAdapters.sp_Pay_Employee_Attendance_Select_RowTableAdapter sp_Pay_Employee_Attendance_Select_RowTableAdapter1;
    private XRTableCell xrTableCell25;
    private XRTableCell xrTableCell27;
    private XRTableCell xrTableCell29;
    private XRTableCell xrTableCell30;
    private XRTableCell xrTableCell31;
    private XRTableCell xrTableCell32;
    private XRTableCell xrTableCell26;
	/// <summary>
	/// Required designer variable.
	/// </summary>
	private System.ComponentModel.IContainer components = null;

    public Att_SummaryReport()
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

	#region Designer generated code

	/// <summary>
	/// Required method for Designer support - do not modify
	/// the contents of this method with the code editor.
	/// </summary>
	private void InitializeComponent() {
        string resourceFileName = "Att_SummaryReport.resx";
        this.Detail = new DevExpress.XtraReports.UI.DetailBand();
        this.xrTable2 = new DevExpress.XtraReports.UI.XRTable();
        this.xrTableRow2 = new DevExpress.XtraReports.UI.XRTableRow();
        this.xrTableCell5 = new DevExpress.XtraReports.UI.XRTableCell();
        this.xrTableCell8 = new DevExpress.XtraReports.UI.XRTableCell();
        this.xrTableCell12 = new DevExpress.XtraReports.UI.XRTableCell();
        this.xrTableCell27 = new DevExpress.XtraReports.UI.XRTableCell();
        this.xrTableCell26 = new DevExpress.XtraReports.UI.XRTableCell();
        this.xrTableCell29 = new DevExpress.XtraReports.UI.XRTableCell();
        this.xrTableCell32 = new DevExpress.XtraReports.UI.XRTableCell();
        this.xrTableCell16 = new DevExpress.XtraReports.UI.XRTableCell();
        this.xrTableCell13 = new DevExpress.XtraReports.UI.XRTableCell();
        this.xrTableCell15 = new DevExpress.XtraReports.UI.XRTableCell();
        this.xrTableCell14 = new DevExpress.XtraReports.UI.XRTableCell();
        this.xrTableCell11 = new DevExpress.XtraReports.UI.XRTableCell();
        this.xrTableCell25 = new DevExpress.XtraReports.UI.XRTableCell();
        this.xrTableCell30 = new DevExpress.XtraReports.UI.XRTableCell();
        this.xrTableCell31 = new DevExpress.XtraReports.UI.XRTableCell();
        this.TopMargin = new DevExpress.XtraReports.UI.TopMarginBand();
        this.BottomMargin = new DevExpress.XtraReports.UI.BottomMarginBand();
        this.PageFooter = new DevExpress.XtraReports.UI.PageFooterBand();
        this.xrPageInfo2 = new DevExpress.XtraReports.UI.XRPageInfo();
        this.xrPageInfo1 = new DevExpress.XtraReports.UI.XRPageInfo();
        this.attendanceDataSet1 = new AttendanceDataSet();
        this.ReportHeader = new DevExpress.XtraReports.UI.ReportHeaderBand();
        this.xrPanel1 = new DevExpress.XtraReports.UI.XRPanel();
        this.xrCompAddress = new DevExpress.XtraReports.UI.XRLabel();
        this.xrTitle = new DevExpress.XtraReports.UI.XRLabel();
        this.xrPictureBox1 = new DevExpress.XtraReports.UI.XRPictureBox();
        this.xrCompName = new DevExpress.XtraReports.UI.XRLabel();
        this.xrControlStyle1 = new DevExpress.XtraReports.UI.XRControlStyle();
        this.PageHeader = new DevExpress.XtraReports.UI.PageHeaderBand();
        this.xrTable1 = new DevExpress.XtraReports.UI.XRTable();
        this.xrTableRow3 = new DevExpress.XtraReports.UI.XRTableRow();
        this.xrTableCell24 = new DevExpress.XtraReports.UI.XRTableCell();
        this.xrTableCell28 = new DevExpress.XtraReports.UI.XRTableCell();
        this.xrTableCell33 = new DevExpress.XtraReports.UI.XRTableCell();
        this.xrTableCell36 = new DevExpress.XtraReports.UI.XRTableCell();
        this.xrTableRow1 = new DevExpress.XtraReports.UI.XRTableRow();
        this.xrTableCell2 = new DevExpress.XtraReports.UI.XRTableCell();
        this.xrTableCell1 = new DevExpress.XtraReports.UI.XRTableCell();
        this.xrTableCell3 = new DevExpress.XtraReports.UI.XRTableCell();
        this.xrTableCell4 = new DevExpress.XtraReports.UI.XRTableCell();
        this.xrTableCell9 = new DevExpress.XtraReports.UI.XRTableCell();
        this.xrTableCell10 = new DevExpress.XtraReports.UI.XRTableCell();
        this.xrTableCell7 = new DevExpress.XtraReports.UI.XRTableCell();
        this.xrTableCell17 = new DevExpress.XtraReports.UI.XRTableCell();
        this.xrTableCell18 = new DevExpress.XtraReports.UI.XRTableCell();
        this.xrTableCell19 = new DevExpress.XtraReports.UI.XRTableCell();
        this.xrTableCell23 = new DevExpress.XtraReports.UI.XRTableCell();
        this.xrTableCell6 = new DevExpress.XtraReports.UI.XRTableCell();
        this.xrTableCell20 = new DevExpress.XtraReports.UI.XRTableCell();
        this.xrTableCell21 = new DevExpress.XtraReports.UI.XRTableCell();
        this.xrTableCell22 = new DevExpress.XtraReports.UI.XRTableCell();
        this.sp_Pay_Employee_Attendance_Select_RowTableAdapter1 = new AttendanceDataSetTableAdapters.sp_Pay_Employee_Attendance_Select_RowTableAdapter();
        ((System.ComponentModel.ISupportInitialize)(this.xrTable2)).BeginInit();
        ((System.ComponentModel.ISupportInitialize)(this.attendanceDataSet1)).BeginInit();
        ((System.ComponentModel.ISupportInitialize)(this.xrTable1)).BeginInit();
        ((System.ComponentModel.ISupportInitialize)(this)).BeginInit();
        // 
        // Detail
        // 
        this.Detail.Controls.AddRange(new DevExpress.XtraReports.UI.XRControl[] {
            this.xrTable2});
        this.Detail.HeightF = 16.66667F;
        this.Detail.Name = "Detail";
        this.Detail.Padding = new DevExpress.XtraPrinting.PaddingInfo(0, 0, 0, 0, 100F);
        this.Detail.TextAlignment = DevExpress.XtraPrinting.TextAlignment.TopLeft;
        // 
        // xrTable2
        // 
        this.xrTable2.Borders = ((DevExpress.XtraPrinting.BorderSide)((((DevExpress.XtraPrinting.BorderSide.Left | DevExpress.XtraPrinting.BorderSide.Top)
                    | DevExpress.XtraPrinting.BorderSide.Right)
                    | DevExpress.XtraPrinting.BorderSide.Bottom)));
        this.xrTable2.Font = new System.Drawing.Font("Verdana", 8F);
        this.xrTable2.LocationFloat = new DevExpress.Utils.PointFloat(5.000008F, 0F);
        this.xrTable2.Name = "xrTable2";
        this.xrTable2.Rows.AddRange(new DevExpress.XtraReports.UI.XRTableRow[] {
            this.xrTableRow2});
        this.xrTable2.SizeF = new System.Drawing.SizeF(1147F, 16.66667F);
        this.xrTable2.StylePriority.UseBorders = false;
        this.xrTable2.StylePriority.UseFont = false;
        // 
        // xrTableRow2
        // 
        this.xrTableRow2.Borders = ((DevExpress.XtraPrinting.BorderSide)(((DevExpress.XtraPrinting.BorderSide.Left | DevExpress.XtraPrinting.BorderSide.Right)
                    | DevExpress.XtraPrinting.BorderSide.Bottom)));
        this.xrTableRow2.Cells.AddRange(new DevExpress.XtraReports.UI.XRTableCell[] {
            this.xrTableCell5,
            this.xrTableCell8,
            this.xrTableCell12,
            this.xrTableCell27,
            this.xrTableCell26,
            this.xrTableCell29,
            this.xrTableCell32,
            this.xrTableCell16,
            this.xrTableCell13,
            this.xrTableCell15,
            this.xrTableCell14,
            this.xrTableCell11,
            this.xrTableCell25,
            this.xrTableCell30,
            this.xrTableCell31});
        this.xrTableRow2.Name = "xrTableRow2";
        this.xrTableRow2.StylePriority.UseBorders = false;
        this.xrTableRow2.Weight = 1;
        // 
        // xrTableCell5
        // 
        this.xrTableCell5.DataBindings.AddRange(new DevExpress.XtraReports.UI.XRBinding[] {
            new DevExpress.XtraReports.UI.XRBinding("Text", null, "sp_Pay_Employee_Attendance_Select_Row.Emp_Code")});
        this.xrTableCell5.Name = "xrTableCell5";
        this.xrTableCell5.StylePriority.UseTextAlignment = false;
        this.xrTableCell5.Text = "xrTableCell5";
        this.xrTableCell5.TextAlignment = DevExpress.XtraPrinting.TextAlignment.TopCenter;
        this.xrTableCell5.Weight = 0.50127902490627019;
        // 
        // xrTableCell8
        // 
        this.xrTableCell8.DataBindings.AddRange(new DevExpress.XtraReports.UI.XRBinding[] {
            new DevExpress.XtraReports.UI.XRBinding("Text", null, "sp_Pay_Employee_Attendance_Select_Row.Emp_Name")});
        this.xrTableCell8.Name = "xrTableCell8";
        this.xrTableCell8.StylePriority.UseTextAlignment = false;
        this.xrTableCell8.Text = "xrTableCell8";
        this.xrTableCell8.TextAlignment = DevExpress.XtraPrinting.TextAlignment.TopLeft;
        this.xrTableCell8.Weight = 1.8175119574362169;
        // 
        // xrTableCell12
        // 
        this.xrTableCell12.DataBindings.AddRange(new DevExpress.XtraReports.UI.XRBinding[] {
            new DevExpress.XtraReports.UI.XRBinding("Text", null, "sp_Pay_Employee_Attendance_Select_Row.Total_Days")});
        this.xrTableCell12.Name = "xrTableCell12";
        this.xrTableCell12.StylePriority.UseTextAlignment = false;
        this.xrTableCell12.Text = "xrTableCell12";
        this.xrTableCell12.TextAlignment = DevExpress.XtraPrinting.TextAlignment.TopRight;
        this.xrTableCell12.Weight = 0.86831051153101391;
        this.xrTableCell12.BeforePrint += new System.Drawing.Printing.PrintEventHandler(this.xrTableCell12_BeforePrint);
        // 
        // xrTableCell27
        // 
        this.xrTableCell27.DataBindings.AddRange(new DevExpress.XtraReports.UI.XRBinding[] {
            new DevExpress.XtraReports.UI.XRBinding("Text", null, "sp_Pay_Employee_Attendance_Select_Row.Week_Off_Days")});
        this.xrTableCell27.Name = "xrTableCell27";
        this.xrTableCell27.StylePriority.UseTextAlignment = false;
        this.xrTableCell27.Text = "xrTableCell27";
        this.xrTableCell27.TextAlignment = DevExpress.XtraPrinting.TextAlignment.TopRight;
        this.xrTableCell27.Weight = 0.78497770122353649;
        // 
        // xrTableCell26
        // 
        this.xrTableCell26.DataBindings.AddRange(new DevExpress.XtraReports.UI.XRBinding[] {
            new DevExpress.XtraReports.UI.XRBinding("Text", null, "sp_Pay_Employee_Attendance_Select_Row.Holiday_Days")});
        this.xrTableCell26.Name = "xrTableCell26";
        this.xrTableCell26.StylePriority.UseTextAlignment = false;
        this.xrTableCell26.Text = "xrTableCell26";
        this.xrTableCell26.TextAlignment = DevExpress.XtraPrinting.TextAlignment.TopRight;
        this.xrTableCell26.Weight = 0.58210539109593717;
        // 
        // xrTableCell29
        // 
        this.xrTableCell29.DataBindings.AddRange(new DevExpress.XtraReports.UI.XRBinding[] {
            new DevExpress.XtraReports.UI.XRBinding("Text", null, "sp_Pay_Employee_Attendance_Select_Row.Leave_Days")});
        this.xrTableCell29.Name = "xrTableCell29";
        this.xrTableCell29.StylePriority.UseTextAlignment = false;
        this.xrTableCell29.Text = "xrTableCell29";
        this.xrTableCell29.TextAlignment = DevExpress.XtraPrinting.TextAlignment.TopRight;
        this.xrTableCell29.Weight = 0.56948304659080873;
        // 
        // xrTableCell32
        // 
        this.xrTableCell32.DataBindings.AddRange(new DevExpress.XtraReports.UI.XRBinding[] {
            new DevExpress.XtraReports.UI.XRBinding("Text", null, "sp_Pay_Employee_Attendance_Select_Row.Absent_Days")});
        this.xrTableCell32.Name = "xrTableCell32";
        this.xrTableCell32.StylePriority.UseTextAlignment = false;
        this.xrTableCell32.Text = "xrTableCell32";
        this.xrTableCell32.TextAlignment = DevExpress.XtraPrinting.TextAlignment.TopRight;
        this.xrTableCell32.Weight = 0.69007754130733057;
        // 
        // xrTableCell16
        // 
        this.xrTableCell16.DataBindings.AddRange(new DevExpress.XtraReports.UI.XRBinding[] {
            new DevExpress.XtraReports.UI.XRBinding("Text", null, "sp_Pay_Employee_Attendance_Select_Row.Late_Min")});
        this.xrTableCell16.Name = "xrTableCell16";
        this.xrTableCell16.StylePriority.UseTextAlignment = false;
        this.xrTableCell16.Text = "xrTableCell16";
        this.xrTableCell16.TextAlignment = DevExpress.XtraPrinting.TextAlignment.TopCenter;
        this.xrTableCell16.Weight = 0.6082079590947902;
        this.xrTableCell16.BeforePrint += new System.Drawing.Printing.PrintEventHandler(this.xrTableCell16_BeforePrint);
        // 
        // xrTableCell13
        // 
        this.xrTableCell13.DataBindings.AddRange(new DevExpress.XtraReports.UI.XRBinding[] {
            new DevExpress.XtraReports.UI.XRBinding("Text", null, "sp_Pay_Employee_Attendance_Select_Row.Early_Min")});
        this.xrTableCell13.Name = "xrTableCell13";
        this.xrTableCell13.StylePriority.UseTextAlignment = false;
        this.xrTableCell13.Text = "xrTableCell13";
        this.xrTableCell13.TextAlignment = DevExpress.XtraPrinting.TextAlignment.TopCenter;
        this.xrTableCell13.Weight = 0.70012945430166407;
        this.xrTableCell13.BeforePrint += new System.Drawing.Printing.PrintEventHandler(this.xrTableCell13_BeforePrint);
        // 
        // xrTableCell15
        // 
        this.xrTableCell15.DataBindings.AddRange(new DevExpress.XtraReports.UI.XRBinding[] {
            new DevExpress.XtraReports.UI.XRBinding("Text", null, "sp_Pay_Employee_Attendance_Select_Row.Partial_Min")});
        this.xrTableCell15.Name = "xrTableCell15";
        this.xrTableCell15.StylePriority.UseTextAlignment = false;
        this.xrTableCell15.Text = "xrTableCell15";
        this.xrTableCell15.TextAlignment = DevExpress.XtraPrinting.TextAlignment.TopCenter;
        this.xrTableCell15.Weight = 0.72070004706815183;
        this.xrTableCell15.BeforePrint += new System.Drawing.Printing.PrintEventHandler(this.xrTableCell15_BeforePrint);
        // 
        // xrTableCell14
        // 
        this.xrTableCell14.DataBindings.AddRange(new DevExpress.XtraReports.UI.XRBinding[] {
            new DevExpress.XtraReports.UI.XRBinding("Text", null, "sp_Pay_Employee_Attendance_Select_Row.Assigned_Worked_Min")});
        this.xrTableCell14.Name = "xrTableCell14";
        this.xrTableCell14.StylePriority.UseTextAlignment = false;
        this.xrTableCell14.Text = "xrTableCell14";
        this.xrTableCell14.TextAlignment = DevExpress.XtraPrinting.TextAlignment.TopCenter;
        this.xrTableCell14.Weight = 0.60071659046157266;
        this.xrTableCell14.BeforePrint += new System.Drawing.Printing.PrintEventHandler(this.xrTableCell14_BeforePrint);
        // 
        // xrTableCell11
        // 
        this.xrTableCell11.DataBindings.AddRange(new DevExpress.XtraReports.UI.XRBinding[] {
            new DevExpress.XtraReports.UI.XRBinding("Text", null, "sp_Pay_Employee_Attendance_Select_Row.Total_Worked_Min")});
        this.xrTableCell11.Name = "xrTableCell11";
        this.xrTableCell11.StylePriority.UseTextAlignment = false;
        this.xrTableCell11.Text = "xrTableCell11";
        this.xrTableCell11.TextAlignment = DevExpress.XtraPrinting.TextAlignment.TopCenter;
        this.xrTableCell11.Weight = 0.69524593908076882;
        this.xrTableCell11.BeforePrint += new System.Drawing.Printing.PrintEventHandler(this.xrTableCell11_BeforePrint);
        // 
        // xrTableCell25
        // 
        this.xrTableCell25.DataBindings.AddRange(new DevExpress.XtraReports.UI.XRBinding[] {
            new DevExpress.XtraReports.UI.XRBinding("Text", null, "sp_Pay_Employee_Attendance_Select_Row.Normal_OT_Min")});
        this.xrTableCell25.Name = "xrTableCell25";
        this.xrTableCell25.StylePriority.UseTextAlignment = false;
        this.xrTableCell25.Text = "xrTableCell25";
        this.xrTableCell25.TextAlignment = DevExpress.XtraPrinting.TextAlignment.TopCenter;
        this.xrTableCell25.Weight = 0.726694193804055;
        this.xrTableCell25.BeforePrint += new System.Drawing.Printing.PrintEventHandler(this.xrTableCell25_BeforePrint);
        // 
        // xrTableCell30
        // 
        this.xrTableCell30.DataBindings.AddRange(new DevExpress.XtraReports.UI.XRBinding[] {
            new DevExpress.XtraReports.UI.XRBinding("Text", null, "sp_Pay_Employee_Attendance_Select_Row.Week_Off_OT_Min")});
        this.xrTableCell30.Name = "xrTableCell30";
        this.xrTableCell30.StylePriority.UseTextAlignment = false;
        this.xrTableCell30.Text = "xrTableCell30";
        this.xrTableCell30.TextAlignment = DevExpress.XtraPrinting.TextAlignment.TopCenter;
        this.xrTableCell30.Weight = 0.84290345347592366;
        this.xrTableCell30.BeforePrint += new System.Drawing.Printing.PrintEventHandler(this.xrTableCell30_BeforePrint);
        // 
        // xrTableCell31
        // 
        this.xrTableCell31.DataBindings.AddRange(new DevExpress.XtraReports.UI.XRBinding[] {
            new DevExpress.XtraReports.UI.XRBinding("Text", null, "sp_Pay_Employee_Attendance_Select_Row.Holiday_OT_Min")});
        this.xrTableCell31.Name = "xrTableCell31";
        this.xrTableCell31.StylePriority.UseTextAlignment = false;
        this.xrTableCell31.Text = "xrTableCell31";
        this.xrTableCell31.TextAlignment = DevExpress.XtraPrinting.TextAlignment.TopCenter;
        this.xrTableCell31.Weight = 0.76165686777133934;
        this.xrTableCell31.BeforePrint += new System.Drawing.Printing.PrintEventHandler(this.xrTableCell31_BeforePrint);
        // 
        // TopMargin
        // 
        this.TopMargin.HeightF = 0F;
        this.TopMargin.Name = "TopMargin";
        this.TopMargin.Padding = new DevExpress.XtraPrinting.PaddingInfo(0, 0, 0, 0, 100F);
        this.TopMargin.TextAlignment = DevExpress.XtraPrinting.TextAlignment.TopLeft;
        // 
        // BottomMargin
        // 
        this.BottomMargin.HeightF = 1.041667F;
        this.BottomMargin.Name = "BottomMargin";
        this.BottomMargin.Padding = new DevExpress.XtraPrinting.PaddingInfo(0, 0, 0, 0, 100F);
        this.BottomMargin.TextAlignment = DevExpress.XtraPrinting.TextAlignment.TopLeft;
        // 
        // PageFooter
        // 
        this.PageFooter.Controls.AddRange(new DevExpress.XtraReports.UI.XRControl[] {
            this.xrPageInfo2,
            this.xrPageInfo1});
        this.PageFooter.HeightF = 25F;
        this.PageFooter.Name = "PageFooter";
        // 
        // xrPageInfo2
        // 
        this.xrPageInfo2.Format = "Page{0}";
        this.xrPageInfo2.LocationFloat = new DevExpress.Utils.PointFloat(856.7918F, 0F);
        this.xrPageInfo2.Name = "xrPageInfo2";
        this.xrPageInfo2.Padding = new DevExpress.XtraPrinting.PaddingInfo(2, 2, 0, 0, 100F);
        this.xrPageInfo2.SizeF = new System.Drawing.SizeF(295.2082F, 23F);
        this.xrPageInfo2.StylePriority.UseTextAlignment = false;
        this.xrPageInfo2.TextAlignment = DevExpress.XtraPrinting.TextAlignment.TopRight;
        // 
        // xrPageInfo1
        // 
        this.xrPageInfo1.LocationFloat = new DevExpress.Utils.PointFloat(5F, 0F);
        this.xrPageInfo1.Name = "xrPageInfo1";
        this.xrPageInfo1.Padding = new DevExpress.XtraPrinting.PaddingInfo(2, 2, 0, 0, 100F);
        this.xrPageInfo1.PageInfo = DevExpress.XtraPrinting.PageInfo.DateTime;
        this.xrPageInfo1.SizeF = new System.Drawing.SizeF(344.7917F, 23F);
        // 
        // attendanceDataSet1
        // 
        this.attendanceDataSet1.DataSetName = "AttendanceDataSet";
        this.attendanceDataSet1.SchemaSerializationMode = System.Data.SchemaSerializationMode.IncludeSchema;
        // 
        // ReportHeader
        // 
        this.ReportHeader.Controls.AddRange(new DevExpress.XtraReports.UI.XRControl[] {
            this.xrPanel1});
        this.ReportHeader.HeightF = 81.12501F;
        this.ReportHeader.Name = "ReportHeader";
        this.ReportHeader.StylePriority.UseBorders = false;
        // 
        // xrPanel1
        // 
        this.xrPanel1.Borders = ((DevExpress.XtraPrinting.BorderSide)((((DevExpress.XtraPrinting.BorderSide.Left | DevExpress.XtraPrinting.BorderSide.Top)
                    | DevExpress.XtraPrinting.BorderSide.Right)
                    | DevExpress.XtraPrinting.BorderSide.Bottom)));
        this.xrPanel1.Controls.AddRange(new DevExpress.XtraReports.UI.XRControl[] {
            this.xrCompAddress,
            this.xrTitle,
            this.xrPictureBox1,
            this.xrCompName});
        this.xrPanel1.LocationFloat = new DevExpress.Utils.PointFloat(5.000003F, 1.999998F);
        this.xrPanel1.Name = "xrPanel1";
        this.xrPanel1.SizeF = new System.Drawing.SizeF(1147F, 79.12501F);
        this.xrPanel1.StylePriority.UseBorders = false;
        // 
        // xrCompAddress
        // 
        this.xrCompAddress.Borders = DevExpress.XtraPrinting.BorderSide.None;
        this.xrCompAddress.Font = new System.Drawing.Font("Verdana", 9.75F, System.Drawing.FontStyle.Bold);
        this.xrCompAddress.LocationFloat = new DevExpress.Utils.PointFloat(5F, 28F);
        this.xrCompAddress.Name = "xrCompAddress";
        this.xrCompAddress.Padding = new DevExpress.XtraPrinting.PaddingInfo(5, 2, 0, 0, 100F);
        this.xrCompAddress.SizeF = new System.Drawing.SizeF(353.125F, 22.04167F);
        this.xrCompAddress.StylePriority.UseBorders = false;
        this.xrCompAddress.StylePriority.UseFont = false;
        this.xrCompAddress.StylePriority.UsePadding = false;
        this.xrCompAddress.Text = "xrCompAddress";
        // 
        // xrTitle
        // 
        this.xrTitle.Borders = DevExpress.XtraPrinting.BorderSide.None;
        this.xrTitle.Font = new System.Drawing.Font("Verdana", 9.75F, System.Drawing.FontStyle.Bold);
        this.xrTitle.LocationFloat = new DevExpress.Utils.PointFloat(357.9375F, 53.12502F);
        this.xrTitle.Name = "xrTitle";
        this.xrTitle.Padding = new DevExpress.XtraPrinting.PaddingInfo(2, 2, 0, 0, 100F);
        this.xrTitle.SizeF = new System.Drawing.SizeF(597.0627F, 16F);
        this.xrTitle.StylePriority.UseBorders = false;
        this.xrTitle.StylePriority.UseFont = false;
        this.xrTitle.StylePriority.UseTextAlignment = false;
        this.xrTitle.Text = "xrTitle";
        this.xrTitle.TextAlignment = DevExpress.XtraPrinting.TextAlignment.TopCenter;
        // 
        // xrPictureBox1
        // 
        this.xrPictureBox1.Borders = DevExpress.XtraPrinting.BorderSide.None;
        this.xrPictureBox1.LocationFloat = new DevExpress.Utils.PointFloat(1037.5F, 3.000005F);
        this.xrPictureBox1.Name = "xrPictureBox1";
        this.xrPictureBox1.SizeF = new System.Drawing.SizeF(105.5F, 45.04167F);
        this.xrPictureBox1.Sizing = DevExpress.XtraPrinting.ImageSizeMode.StretchImage;
        this.xrPictureBox1.StylePriority.UseBorders = false;
        // 
        // xrCompName
        // 
        this.xrCompName.Borders = DevExpress.XtraPrinting.BorderSide.None;
        this.xrCompName.Font = new System.Drawing.Font("Verdana", 9.75F, System.Drawing.FontStyle.Bold);
        this.xrCompName.LocationFloat = new DevExpress.Utils.PointFloat(5F, 2.999998F);
        this.xrCompName.Name = "xrCompName";
        this.xrCompName.Padding = new DevExpress.XtraPrinting.PaddingInfo(5, 2, 0, 0, 100F);
        this.xrCompName.SizeF = new System.Drawing.SizeF(353.125F, 23F);
        this.xrCompName.StylePriority.UseBorders = false;
        this.xrCompName.StylePriority.UseFont = false;
        this.xrCompName.StylePriority.UsePadding = false;
        this.xrCompName.Text = "xrCompName";
        // 
        // xrControlStyle1
        // 
        this.xrControlStyle1.Name = "xrControlStyle1";
        this.xrControlStyle1.Padding = new DevExpress.XtraPrinting.PaddingInfo(0, 0, 0, 0, 100F);
        // 
        // PageHeader
        // 
        this.PageHeader.Controls.AddRange(new DevExpress.XtraReports.UI.XRControl[] {
            this.xrTable1});
        this.PageHeader.HeightF = 29.99998F;
        this.PageHeader.Name = "PageHeader";
        // 
        // xrTable1
        // 
        this.xrTable1.Borders = ((DevExpress.XtraPrinting.BorderSide)(((DevExpress.XtraPrinting.BorderSide.Left | DevExpress.XtraPrinting.BorderSide.Right)
                    | DevExpress.XtraPrinting.BorderSide.Bottom)));
        this.xrTable1.Font = new System.Drawing.Font("Verdana", 8F, System.Drawing.FontStyle.Bold);
        this.xrTable1.LocationFloat = new DevExpress.Utils.PointFloat(5F, 0F);
        this.xrTable1.Name = "xrTable1";
        this.xrTable1.Rows.AddRange(new DevExpress.XtraReports.UI.XRTableRow[] {
            this.xrTableRow3,
            this.xrTableRow1});
        this.xrTable1.SizeF = new System.Drawing.SizeF(1147F, 29.99998F);
        this.xrTable1.StylePriority.UseBorders = false;
        this.xrTable1.StylePriority.UseFont = false;
        // 
        // xrTableRow3
        // 
        this.xrTableRow3.Cells.AddRange(new DevExpress.XtraReports.UI.XRTableCell[] {
            this.xrTableCell24,
            this.xrTableCell28,
            this.xrTableCell33,
            this.xrTableCell36});
        this.xrTableRow3.Name = "xrTableRow3";
        this.xrTableRow3.Weight = 1;
        // 
        // xrTableCell24
        // 
        this.xrTableCell24.Name = "xrTableCell24";
        this.xrTableCell24.Weight = 2.0165216523660767;
        // 
        // xrTableCell28
        // 
        this.xrTableCell28.Name = "xrTableCell28";
        this.xrTableCell28.StylePriority.UseTextAlignment = false;
        this.xrTableCell28.Text = "Days";
        this.xrTableCell28.TextAlignment = DevExpress.XtraPrinting.TextAlignment.TopCenter;
        this.xrTableCell28.Weight = 3.0393622989707052;
        // 
        // xrTableCell33
        // 
        this.xrTableCell33.Name = "xrTableCell33";
        this.xrTableCell33.StylePriority.UseTextAlignment = false;
        this.xrTableCell33.Text = "Hours";
        this.xrTableCell33.TextAlignment = DevExpress.XtraPrinting.TextAlignment.TopCenter;
        this.xrTableCell33.Weight = 2.89156235185292;
        // 
        // xrTableCell36
        // 
        this.xrTableCell36.Name = "xrTableCell36";
        this.xrTableCell36.StylePriority.UseTextAlignment = false;
        this.xrTableCell36.Text = "Over Time (Hours)";
        this.xrTableCell36.TextAlignment = DevExpress.XtraPrinting.TextAlignment.TopCenter;
        this.xrTableCell36.Weight = 2.0273586297550685;
        // 
        // xrTableRow1
        // 
        this.xrTableRow1.Cells.AddRange(new DevExpress.XtraReports.UI.XRTableCell[] {
            this.xrTableCell2,
            this.xrTableCell1,
            this.xrTableCell3,
            this.xrTableCell4,
            this.xrTableCell9,
            this.xrTableCell10,
            this.xrTableCell7,
            this.xrTableCell17,
            this.xrTableCell18,
            this.xrTableCell19,
            this.xrTableCell23,
            this.xrTableCell6,
            this.xrTableCell20,
            this.xrTableCell21,
            this.xrTableCell22});
        this.xrTableRow1.Name = "xrTableRow1";
        this.xrTableRow1.Weight = 1;
        // 
        // xrTableCell2
        // 
        this.xrTableCell2.Name = "xrTableCell2";
        this.xrTableCell2.Padding = new DevExpress.XtraPrinting.PaddingInfo(5, 0, 0, 0, 100F);
        this.xrTableCell2.StylePriority.UsePadding = false;
        this.xrTableCell2.StylePriority.UseTextAlignment = false;
        this.xrTableCell2.Text = "ID";
        this.xrTableCell2.TextAlignment = DevExpress.XtraPrinting.TextAlignment.TopLeft;
        this.xrTableCell2.Weight = 0.43593381338027259;
        // 
        // xrTableCell1
        // 
        this.xrTableCell1.Name = "xrTableCell1";
        this.xrTableCell1.StylePriority.UseTextAlignment = false;
        this.xrTableCell1.Text = "Name";
        this.xrTableCell1.TextAlignment = DevExpress.XtraPrinting.TextAlignment.TopLeft;
        this.xrTableCell1.Weight = 1.5805878389858041;
        // 
        // xrTableCell3
        // 
        this.xrTableCell3.Name = "xrTableCell3";
        this.xrTableCell3.Text = "Total Days";
        this.xrTableCell3.Weight = 0.74567958677855306;
        // 
        // xrTableCell4
        // 
        this.xrTableCell4.Name = "xrTableCell4";
        this.xrTableCell4.Text = "Week Off";
        this.xrTableCell4.Weight = 0.69209038865863259;
        // 
        // xrTableCell9
        // 
        this.xrTableCell9.Name = "xrTableCell9";
        this.xrTableCell9.Text = "Holiday";
        this.xrTableCell9.Weight = 0.50622426294570788;
        // 
        // xrTableCell10
        // 
        this.xrTableCell10.Name = "xrTableCell10";
        this.xrTableCell10.Text = "Leave";
        this.xrTableCell10.Weight = 0.49524664412117136;
        // 
        // xrTableCell7
        // 
        this.xrTableCell7.Name = "xrTableCell7";
        this.xrTableCell7.StylePriority.UseTextAlignment = false;
        this.xrTableCell7.Text = "Absent";
        this.xrTableCell7.TextAlignment = DevExpress.XtraPrinting.TextAlignment.TopCenter;
        this.xrTableCell7.Weight = 0.60012142905502164;
        // 
        // xrTableCell17
        // 
        this.xrTableCell17.Name = "xrTableCell17";
        this.xrTableCell17.Text = "Late ";
        this.xrTableCell17.Weight = 0.52892383880186422;
        // 
        // xrTableCell18
        // 
        this.xrTableCell18.Name = "xrTableCell18";
        this.xrTableCell18.Text = "Early ";
        this.xrTableCell18.Weight = 0.60886208782637719;
        // 
        // xrTableCell19
        // 
        this.xrTableCell19.Name = "xrTableCell19";
        this.xrTableCell19.Text = "Partial ";
        this.xrTableCell19.Weight = 0.62675228929352178;
        // 
        // xrTableCell23
        // 
        this.xrTableCell23.Name = "xrTableCell23";
        this.xrTableCell23.Text = "Assign";
        this.xrTableCell23.Weight = 0.52240893161909041;
        // 
        // xrTableCell6
        // 
        this.xrTableCell6.Name = "xrTableCell6";
        this.xrTableCell6.Text = "Work";
        this.xrTableCell6.Weight = 0.60461577681561218;
        // 
        // xrTableCell20
        // 
        this.xrTableCell20.Name = "xrTableCell20";
        this.xrTableCell20.Text = "Normal ";
        this.xrTableCell20.Weight = 0.63196462695392275;
        // 
        // xrTableCell21
        // 
        this.xrTableCell21.Name = "xrTableCell21";
        this.xrTableCell21.Text = "Week Off";
        this.xrTableCell21.Weight = 0.73302509221790435;
        // 
        // xrTableCell22
        // 
        this.xrTableCell22.Name = "xrTableCell22";
        this.xrTableCell22.Text = "Holiday";
        this.xrTableCell22.Weight = 0.662368325491314;
        // 
        // sp_Pay_Employee_Attendance_Select_RowTableAdapter1
        // 
        this.sp_Pay_Employee_Attendance_Select_RowTableAdapter1.ClearBeforeFill = true;
        // 
        // Att_SummaryReport
        // 
        this.Bands.AddRange(new DevExpress.XtraReports.UI.Band[] {
            this.Detail,
            this.TopMargin,
            this.BottomMargin,
            this.PageFooter,
            this.ReportHeader,
            this.PageHeader});
        this.DataAdapter = this.sp_Pay_Employee_Attendance_Select_RowTableAdapter1;
        this.DataMember = "sp_Pay_Employee_Attendance_Select_Row";
        this.DataSource = this.attendanceDataSet1;
        this.Font = new System.Drawing.Font("Times New Roman", 9.75F);
        this.Margins = new System.Drawing.Printing.Margins(0, 7, 0, 1);
        this.PageHeight = 827;
        this.PageWidth = 1169;
        this.PaperKind = System.Drawing.Printing.PaperKind.A4Rotated;
        this.StyleSheet.AddRange(new DevExpress.XtraReports.UI.XRControlStyle[] {
            this.xrControlStyle1});
        this.Version = "10.2";
        ((System.ComponentModel.ISupportInitialize)(this.xrTable2)).EndInit();
        ((System.ComponentModel.ISupportInitialize)(this.attendanceDataSet1)).EndInit();
        ((System.ComponentModel.ISupportInitialize)(this.xrTable1)).EndInit();
        ((System.ComponentModel.ISupportInitialize)(this)).EndInit();

	}

	#endregion


    public void SetImage(string Url)
    {
        xrPictureBox1.ImageUrl = Url;
    }
    public void setTitleName(string Title)
    {
        xrTitle.Text = Title;
        
    }
    public void setaddress(string address)
    {
        xrCompAddress.Text = address;
    }
    public void setcompanyname(string companyname)
    {
        xrCompName.Text = companyname;
    }
    public string GetHours(object obj)
    {
        if (obj.ToString() == "")
        {
            return "";
        }
        string retval = string.Empty;
        retval = ((Convert.ToInt32(obj) / 60) < 10) ? "0" + (Convert.ToInt32(obj) / 60).ToString() : ((Convert.ToInt32(obj) / 60)).ToString();
        retval += ":" + (((Convert.ToInt32(obj) % 60) < 10) ? "0" + (Convert.ToInt32(obj) % 60) : (Convert.ToInt32(obj) % 60).ToString());

        return retval;
    }
    private void xrTableCell11_BeforePrint(object sender, System.Drawing.Printing.PrintEventArgs e)
    {
        if (xrTableCell11.Text != "")
        {
            try
            {
                xrTableCell11.Text = GetHours(xrTableCell11.Text);
            }
            catch
            {
            }


        }

    }

    private void xrTableCell12_BeforePrint(object sender, System.Drawing.Printing.PrintEventArgs e)
    {
      
    }

    

    private void xrTableCell14_BeforePrint(object sender, System.Drawing.Printing.PrintEventArgs e)
    {

        if (xrTableCell14.Text != "")
        {
            try
            {
                xrTableCell14.Text = GetHours(xrTableCell14.Text);
            }
            catch
            {
            }


        }
    }

    private void xrTableCell16_BeforePrint(object sender, System.Drawing.Printing.PrintEventArgs e)
    {
        if (xrTableCell16.Text!="")
        {
            try
            {
                xrTableCell16.Text = GetHours(xrTableCell16.Text);
            }
            catch
            {
            }


        }
    }

    private void xrTableCell13_BeforePrint(object sender, System.Drawing.Printing.PrintEventArgs e)
    {
        if (xrTableCell13.Text != "")
        {
            try
            {
                xrTableCell13.Text = GetHours(xrTableCell13.Text);
            }
            catch
            {
            }


        }
    }

    private void xrTableCell15_BeforePrint(object sender, System.Drawing.Printing.PrintEventArgs e)
    {
        if (xrTableCell15.Text != "")
        {
            try
            {
                xrTableCell15.Text = GetHours(xrTableCell15.Text);
            }
            catch
            {
            }


        }
    }

    private void xrTableCell25_BeforePrint(object sender, System.Drawing.Printing.PrintEventArgs e)
    {
        if (xrTableCell25.Text != "")
        {
            try
            {
                xrTableCell25.Text = GetHours(xrTableCell25.Text);
            }
            catch
            {
            }


        }
    }

    private void xrTableCell30_BeforePrint(object sender, System.Drawing.Printing.PrintEventArgs e)
    {
        if (xrTableCell30.Text != "")
        {
            try
            {
                xrTableCell30.Text = GetHours(xrTableCell30.Text);
            }
            catch
            {
            }


        }

    }

    private void xrTableCell31_BeforePrint(object sender, System.Drawing.Printing.PrintEventArgs e)
    {
        if (xrTableCell31.Text != "")
        {
            try
            {
                xrTableCell31.Text = GetHours(xrTableCell31.Text);
            }
            catch
            {
            }


        }
    }

   
}
