using System;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using DevExpress.XtraReports.UI;
using System.Data;

/// <summary>
/// Summary description for Att_ShiftReport
/// </summary>
public class Att_DailySalaryReport1 : DevExpress.XtraReports.UI.XtraReport
{
    Att_AttendanceRegister objAttReg = new Att_AttendanceRegister();
    Set_Employee_Holiday objEmpHoli = new Set_Employee_Holiday();
    SystemParameter objSys = new SystemParameter();
	private DevExpress.XtraReports.UI.DetailBand Detail;
	private DevExpress.XtraReports.UI.TopMarginBand TopMargin;
	private DevExpress.XtraReports.UI.BottomMarginBand BottomMargin;
    private GroupHeaderBand GroupHeader1;
    private PageFooterBand PageFooter;
    private AttendanceDataSet attendanceDataSet1;
    private AttendanceDataSetTableAdapters.sp_Att_ScheduleDescription_ReportTableAdapter sp_Att_ScheduleDescription_ReportTableAdapter1;
    private ReportHeaderBand ReportHeader;
    private GroupHeaderBand GroupHeader2;
    private XRPageInfo xrPageInfo2;
    private XRPageInfo xrPageInfo1;
    private AttendanceDataSetTableAdapters.sp_Att_AttendanceRegister_ReportTableAdapter sp_Att_AttendanceRegister_ReportTableAdapter1;
    private XRTable xrTable1;
    private XRTableRow xrTableRow1;
    private XRTableCell xrTableCell21;
    private XRTableCell xrTableCell7;
    private XRTableCell xrTableCell1;
    private XRTableCell xrTableCell22;
    private XRTableCell xrTableCell24;
    private XRTableCell xrTableCell25;
    private XRTableCell xrTableCell23;
    private XRTableCell xrTableCell26;
    private XRTableCell xrTableCell3;
    private XRTableCell xrTableCell2;
    private XRTableCell xrTableCell4;
    private XRTable xrTable2;
    private XRTableRow xrTableRow2;
    private XRTableCell xrTableCell5;
    private XRTableCell xrTableCell6;
    private XRTableCell xrTableCell8;
    private XRTableCell xrTableCell9;
    private XRTableCell xrTableCell10;
    private XRTableCell xrTableCell11;
    private XRTableCell xrTableCell12;
    private XRTableCell xrTableCell20;
    private XRTableCell xrTableCell27;
    private XRTableCell xrTableCell28;
    private XRTableCell xrTableCell29;
    private GroupFooterBand GroupFooter1;
    private XRTable xrTable4;
    private XRTableRow xrTableRow4;
    private XRTableCell xrTableCell19;
    private XRTableCell xrTableCell30;
    private XRTableCell xrTableCell31;
    private XRTableRow xrTableRow5;
    private XRTableCell xrTableCell32;
    private XRTableCell xrTableCell33;
    private XRTableCell xrTableCell34;
    private XRTableCell xrTableCell35;
    private XRTableCell xrTableCell36;
    private XRTableRow xrTableRow6;
    private XRTableCell xrTableCell37;
    private XRTableCell xrTableCell38;
    private XRTableCell xrTableCell39;
    private XRTableCell xrTableCell40;
    private XRTableRow xrTableRow7;
    private XRTableCell xrTableCell41;
    private XRTableCell xrTableCell42;
    private XRTableCell xrTableCell43;
    private XRTableCell xrTableCell44;
    private XRTableCell xrTableCell46;
    private XRTableCell xrTableCell47;
    private XRTableCell xrTableCell48;
    private XRTableCell xrTableCell50;
    private XRTableCell xrTableCell45;
    private XRTableCell xrTableCell49;
    private XRPanel xrPanel1;
    private XRLabel xrCompAddress;
    private XRLabel xrTitle;
    private XRPictureBox xrPictureBox1;
    private XRLabel xrCompName;
    private XRTable xrTable3;
    private XRTableRow xrTableRow3;
    private XRTableCell xrTableCell13;
    private XRTableCell xrTableCell14;
    private XRTableCell xrTableCell16;
    private XRTableCell xrTableCell17;
    private XRTableCell xrTableCell18;
    private XRTableCell xrTableCell15;
	/// <summary>
	/// Required designer variable.
	/// </summary>
	private System.ComponentModel.IContainer components = null;

    public Att_DailySalaryReport1()
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
        string resourceFileName = "Att_DailySalaryReport1.resx";
        DevExpress.XtraReports.UI.XRSummary xrSummary1 = new DevExpress.XtraReports.UI.XRSummary();
        DevExpress.XtraReports.UI.XRSummary xrSummary2 = new DevExpress.XtraReports.UI.XRSummary();
        this.Detail = new DevExpress.XtraReports.UI.DetailBand();
        this.xrTable2 = new DevExpress.XtraReports.UI.XRTable();
        this.xrTableRow2 = new DevExpress.XtraReports.UI.XRTableRow();
        this.xrTableCell5 = new DevExpress.XtraReports.UI.XRTableCell();
        this.xrTableCell6 = new DevExpress.XtraReports.UI.XRTableCell();
        this.xrTableCell29 = new DevExpress.XtraReports.UI.XRTableCell();
        this.xrTableCell28 = new DevExpress.XtraReports.UI.XRTableCell();
        this.xrTableCell20 = new DevExpress.XtraReports.UI.XRTableCell();
        this.xrTableCell27 = new DevExpress.XtraReports.UI.XRTableCell();
        this.xrTableCell8 = new DevExpress.XtraReports.UI.XRTableCell();
        this.xrTableCell9 = new DevExpress.XtraReports.UI.XRTableCell();
        this.xrTableCell10 = new DevExpress.XtraReports.UI.XRTableCell();
        this.xrTableCell11 = new DevExpress.XtraReports.UI.XRTableCell();
        this.xrTableCell12 = new DevExpress.XtraReports.UI.XRTableCell();
        this.TopMargin = new DevExpress.XtraReports.UI.TopMarginBand();
        this.BottomMargin = new DevExpress.XtraReports.UI.BottomMarginBand();
        this.GroupHeader1 = new DevExpress.XtraReports.UI.GroupHeaderBand();
        this.xrTable1 = new DevExpress.XtraReports.UI.XRTable();
        this.xrTableRow1 = new DevExpress.XtraReports.UI.XRTableRow();
        this.xrTableCell21 = new DevExpress.XtraReports.UI.XRTableCell();
        this.xrTableCell7 = new DevExpress.XtraReports.UI.XRTableCell();
        this.xrTableCell1 = new DevExpress.XtraReports.UI.XRTableCell();
        this.xrTableCell22 = new DevExpress.XtraReports.UI.XRTableCell();
        this.xrTableCell24 = new DevExpress.XtraReports.UI.XRTableCell();
        this.xrTableCell25 = new DevExpress.XtraReports.UI.XRTableCell();
        this.xrTableCell23 = new DevExpress.XtraReports.UI.XRTableCell();
        this.xrTableCell26 = new DevExpress.XtraReports.UI.XRTableCell();
        this.xrTableCell3 = new DevExpress.XtraReports.UI.XRTableCell();
        this.xrTableCell2 = new DevExpress.XtraReports.UI.XRTableCell();
        this.xrTableCell4 = new DevExpress.XtraReports.UI.XRTableCell();
        this.PageFooter = new DevExpress.XtraReports.UI.PageFooterBand();
        this.xrPageInfo2 = new DevExpress.XtraReports.UI.XRPageInfo();
        this.xrPageInfo1 = new DevExpress.XtraReports.UI.XRPageInfo();
        this.attendanceDataSet1 = new AttendanceDataSet();
        this.sp_Att_ScheduleDescription_ReportTableAdapter1 = new AttendanceDataSetTableAdapters.sp_Att_ScheduleDescription_ReportTableAdapter();
        this.ReportHeader = new DevExpress.XtraReports.UI.ReportHeaderBand();
        this.xrPanel1 = new DevExpress.XtraReports.UI.XRPanel();
        this.xrCompAddress = new DevExpress.XtraReports.UI.XRLabel();
        this.xrTitle = new DevExpress.XtraReports.UI.XRLabel();
        this.xrPictureBox1 = new DevExpress.XtraReports.UI.XRPictureBox();
        this.xrCompName = new DevExpress.XtraReports.UI.XRLabel();
        this.GroupHeader2 = new DevExpress.XtraReports.UI.GroupHeaderBand();
        this.xrTable3 = new DevExpress.XtraReports.UI.XRTable();
        this.xrTableRow3 = new DevExpress.XtraReports.UI.XRTableRow();
        this.xrTableCell13 = new DevExpress.XtraReports.UI.XRTableCell();
        this.xrTableCell14 = new DevExpress.XtraReports.UI.XRTableCell();
        this.xrTableCell16 = new DevExpress.XtraReports.UI.XRTableCell();
        this.xrTableCell17 = new DevExpress.XtraReports.UI.XRTableCell();
        this.xrTableCell18 = new DevExpress.XtraReports.UI.XRTableCell();
        this.xrTableCell15 = new DevExpress.XtraReports.UI.XRTableCell();
        this.sp_Att_AttendanceRegister_ReportTableAdapter1 = new AttendanceDataSetTableAdapters.sp_Att_AttendanceRegister_ReportTableAdapter();
        this.GroupFooter1 = new DevExpress.XtraReports.UI.GroupFooterBand();
        this.xrTable4 = new DevExpress.XtraReports.UI.XRTable();
        this.xrTableRow4 = new DevExpress.XtraReports.UI.XRTableRow();
        this.xrTableCell19 = new DevExpress.XtraReports.UI.XRTableCell();
        this.xrTableCell30 = new DevExpress.XtraReports.UI.XRTableCell();
        this.xrTableCell31 = new DevExpress.XtraReports.UI.XRTableCell();
        this.xrTableCell35 = new DevExpress.XtraReports.UI.XRTableCell();
        this.xrTableRow7 = new DevExpress.XtraReports.UI.XRTableRow();
        this.xrTableCell41 = new DevExpress.XtraReports.UI.XRTableCell();
        this.xrTableCell42 = new DevExpress.XtraReports.UI.XRTableCell();
        this.xrTableCell46 = new DevExpress.XtraReports.UI.XRTableCell();
        this.xrTableCell50 = new DevExpress.XtraReports.UI.XRTableCell();
        this.xrTableCell43 = new DevExpress.XtraReports.UI.XRTableCell();
        this.xrTableCell44 = new DevExpress.XtraReports.UI.XRTableCell();
        this.xrTableRow5 = new DevExpress.XtraReports.UI.XRTableRow();
        this.xrTableCell32 = new DevExpress.XtraReports.UI.XRTableCell();
        this.xrTableCell33 = new DevExpress.XtraReports.UI.XRTableCell();
        this.xrTableCell34 = new DevExpress.XtraReports.UI.XRTableCell();
        this.xrTableCell47 = new DevExpress.XtraReports.UI.XRTableCell();
        this.xrTableCell45 = new DevExpress.XtraReports.UI.XRTableCell();
        this.xrTableCell36 = new DevExpress.XtraReports.UI.XRTableCell();
        this.xrTableRow6 = new DevExpress.XtraReports.UI.XRTableRow();
        this.xrTableCell37 = new DevExpress.XtraReports.UI.XRTableCell();
        this.xrTableCell38 = new DevExpress.XtraReports.UI.XRTableCell();
        this.xrTableCell39 = new DevExpress.XtraReports.UI.XRTableCell();
        this.xrTableCell48 = new DevExpress.XtraReports.UI.XRTableCell();
        this.xrTableCell49 = new DevExpress.XtraReports.UI.XRTableCell();
        this.xrTableCell40 = new DevExpress.XtraReports.UI.XRTableCell();
        ((System.ComponentModel.ISupportInitialize)(this.xrTable2)).BeginInit();
        ((System.ComponentModel.ISupportInitialize)(this.xrTable1)).BeginInit();
        ((System.ComponentModel.ISupportInitialize)(this.attendanceDataSet1)).BeginInit();
        ((System.ComponentModel.ISupportInitialize)(this.xrTable3)).BeginInit();
        ((System.ComponentModel.ISupportInitialize)(this.xrTable4)).BeginInit();
        ((System.ComponentModel.ISupportInitialize)(this)).BeginInit();
        // 
        // Detail
        // 
        this.Detail.Controls.AddRange(new DevExpress.XtraReports.UI.XRControl[] {
            this.xrTable2});
        this.Detail.HeightF = 14.99999F;
        this.Detail.Name = "Detail";
        this.Detail.Padding = new DevExpress.XtraPrinting.PaddingInfo(0, 0, 0, 0, 100F);
        this.Detail.TextAlignment = DevExpress.XtraPrinting.TextAlignment.TopLeft;
        // 
        // xrTable2
        // 
        this.xrTable2.Borders = ((DevExpress.XtraPrinting.BorderSide)(((DevExpress.XtraPrinting.BorderSide.Left | DevExpress.XtraPrinting.BorderSide.Right)
                    | DevExpress.XtraPrinting.BorderSide.Bottom)));
        this.xrTable2.LocationFloat = new DevExpress.Utils.PointFloat(10.00001F, 0F);
        this.xrTable2.Name = "xrTable2";
        this.xrTable2.Rows.AddRange(new DevExpress.XtraReports.UI.XRTableRow[] {
            this.xrTableRow2});
        this.xrTable2.SizeF = new System.Drawing.SizeF(881.0001F, 14.99999F);
        this.xrTable2.StylePriority.UseBorders = false;
        // 
        // xrTableRow2
        // 
        this.xrTableRow2.Cells.AddRange(new DevExpress.XtraReports.UI.XRTableCell[] {
            this.xrTableCell5,
            this.xrTableCell6,
            this.xrTableCell29,
            this.xrTableCell28,
            this.xrTableCell20,
            this.xrTableCell27,
            this.xrTableCell8,
            this.xrTableCell9,
            this.xrTableCell10,
            this.xrTableCell11,
            this.xrTableCell12});
        this.xrTableRow2.Name = "xrTableRow2";
        this.xrTableRow2.Weight = 1;
        // 
        // xrTableCell5
        // 
        this.xrTableCell5.DataBindings.AddRange(new DevExpress.XtraReports.UI.XRBinding[] {
            new DevExpress.XtraReports.UI.XRBinding("Text", null, "sp_Att_AttendanceRegister_Report.Emp_Name")});
        this.xrTableCell5.Font = new System.Drawing.Font("Verdana", 8F);
        this.xrTableCell5.Name = "xrTableCell5";
        this.xrTableCell5.StylePriority.UseFont = false;
        this.xrTableCell5.Text = "xrTableCell5";
        this.xrTableCell5.Weight = 1.6399990844726564;
        // 
        // xrTableCell6
        // 
        this.xrTableCell6.DataBindings.AddRange(new DevExpress.XtraReports.UI.XRBinding[] {
            new DevExpress.XtraReports.UI.XRBinding("Text", null, "sp_Att_AttendanceRegister_Report.Att_Date")});
        this.xrTableCell6.Font = new System.Drawing.Font("Verdana", 8F);
        this.xrTableCell6.Name = "xrTableCell6";
        this.xrTableCell6.StylePriority.UseFont = false;
        this.xrTableCell6.StylePriority.UseTextAlignment = false;
        this.xrTableCell6.Text = "xrTableCell6";
        this.xrTableCell6.TextAlignment = DevExpress.XtraPrinting.TextAlignment.TopCenter;
        this.xrTableCell6.Weight = 0.88484619140625009;
        this.xrTableCell6.BeforePrint += new System.Drawing.Printing.PrintEventHandler(this.xrTableCell6_BeforePrint);
        // 
        // xrTableCell29
        // 
        this.xrTableCell29.DataBindings.AddRange(new DevExpress.XtraReports.UI.XRBinding[] {
            new DevExpress.XtraReports.UI.XRBinding("Text", null, "sp_Att_AttendanceRegister_Report.In_Time")});
        this.xrTableCell29.Font = new System.Drawing.Font("Verdana", 8F);
        this.xrTableCell29.Name = "xrTableCell29";
        this.xrTableCell29.StylePriority.UseFont = false;
        this.xrTableCell29.StylePriority.UseTextAlignment = false;
        this.xrTableCell29.Text = "xrTableCell29";
        this.xrTableCell29.TextAlignment = DevExpress.XtraPrinting.TextAlignment.TopCenter;
        this.xrTableCell29.Weight = 0.58103973388671881;
        // 
        // xrTableCell28
        // 
        this.xrTableCell28.DataBindings.AddRange(new DevExpress.XtraReports.UI.XRBinding[] {
            new DevExpress.XtraReports.UI.XRBinding("Text", null, "sp_Att_AttendanceRegister_Report.Out_Time")});
        this.xrTableCell28.Font = new System.Drawing.Font("Verdana", 8F);
        this.xrTableCell28.Name = "xrTableCell28";
        this.xrTableCell28.StylePriority.UseFont = false;
        this.xrTableCell28.StylePriority.UseTextAlignment = false;
        this.xrTableCell28.Text = "xrTableCell28";
        this.xrTableCell28.TextAlignment = DevExpress.XtraPrinting.TextAlignment.TopCenter;
        this.xrTableCell28.Weight = 0.64203094482421863;
        // 
        // xrTableCell20
        // 
        this.xrTableCell20.DataBindings.AddRange(new DevExpress.XtraReports.UI.XRBinding[] {
            new DevExpress.XtraReports.UI.XRBinding("Text", null, "sp_Att_AttendanceRegister_Report.EffectiveWork_Min")});
        this.xrTableCell20.Font = new System.Drawing.Font("Verdana", 8F);
        this.xrTableCell20.Name = "xrTableCell20";
        this.xrTableCell20.StylePriority.UseFont = false;
        this.xrTableCell20.StylePriority.UseTextAlignment = false;
        this.xrTableCell20.Text = "xrTableCell20";
        this.xrTableCell20.TextAlignment = DevExpress.XtraPrinting.TextAlignment.TopCenter;
        this.xrTableCell20.Weight = 0.7986181640625003;
        this.xrTableCell20.BeforePrint += new System.Drawing.Printing.PrintEventHandler(this.xrTableCell20_BeforePrint);
        // 
        // xrTableCell27
        // 
        this.xrTableCell27.DataBindings.AddRange(new DevExpress.XtraReports.UI.XRBinding[] {
            new DevExpress.XtraReports.UI.XRBinding("Text", null, "sp_Att_AttendanceRegister_Report.OverTime_Min")});
        this.xrTableCell27.Font = new System.Drawing.Font("Verdana", 8F);
        this.xrTableCell27.Name = "xrTableCell27";
        this.xrTableCell27.StylePriority.UseFont = false;
        this.xrTableCell27.StylePriority.UseTextAlignment = false;
        this.xrTableCell27.Text = "xrTableCell27";
        this.xrTableCell27.TextAlignment = DevExpress.XtraPrinting.TextAlignment.TopCenter;
        this.xrTableCell27.Weight = 0.68054901123046863;
        this.xrTableCell27.BeforePrint += new System.Drawing.Printing.PrintEventHandler(this.xrTableCell27_BeforePrint);
        // 
        // xrTableCell8
        // 
        this.xrTableCell8.DataBindings.AddRange(new DevExpress.XtraReports.UI.XRBinding[] {
            new DevExpress.XtraReports.UI.XRBinding("Text", null, "sp_Att_AttendanceRegister_Report.Field3")});
        this.xrTableCell8.Font = new System.Drawing.Font("Verdana", 8F);
        this.xrTableCell8.Name = "xrTableCell8";
        this.xrTableCell8.StylePriority.UseFont = false;
        this.xrTableCell8.StylePriority.UseTextAlignment = false;
        this.xrTableCell8.Text = "xrTableCell8";
        this.xrTableCell8.TextAlignment = DevExpress.XtraPrinting.TextAlignment.TopCenter;
        this.xrTableCell8.Weight = 0.71528350830078136;
        // 
        // xrTableCell9
        // 
        this.xrTableCell9.DataBindings.AddRange(new DevExpress.XtraReports.UI.XRBinding[] {
            new DevExpress.XtraReports.UI.XRBinding("Text", null, "sp_Att_AttendanceRegister_Report.Field4")});
        this.xrTableCell9.Font = new System.Drawing.Font("Verdana", 8F);
        this.xrTableCell9.Name = "xrTableCell9";
        this.xrTableCell9.StylePriority.UseFont = false;
        this.xrTableCell9.StylePriority.UseTextAlignment = false;
        this.xrTableCell9.Text = "xrTableCell9";
        this.xrTableCell9.TextAlignment = DevExpress.XtraPrinting.TextAlignment.TopCenter;
        this.xrTableCell9.Weight = 0.82825927734374982;
        // 
        // xrTableCell10
        // 
        this.xrTableCell10.DataBindings.AddRange(new DevExpress.XtraReports.UI.XRBinding[] {
            new DevExpress.XtraReports.UI.XRBinding("Text", null, "sp_Att_AttendanceRegister_Report.Is_Week_Off")});
        this.xrTableCell10.Font = new System.Drawing.Font("Verdana", 8F);
        this.xrTableCell10.Name = "xrTableCell10";
        this.xrTableCell10.StylePriority.UseFont = false;
        this.xrTableCell10.StylePriority.UseTextAlignment = false;
        this.xrTableCell10.Text = "xrTableCell10";
        this.xrTableCell10.TextAlignment = DevExpress.XtraPrinting.TextAlignment.TopCenter;
        this.xrTableCell10.Weight = 0.54603942871093736;
        // 
        // xrTableCell11
        // 
        this.xrTableCell11.DataBindings.AddRange(new DevExpress.XtraReports.UI.XRBinding[] {
            new DevExpress.XtraReports.UI.XRBinding("Text", null, "sp_Att_AttendanceRegister_Report.Is_Holiday")});
        this.xrTableCell11.Font = new System.Drawing.Font("Verdana", 8F);
        this.xrTableCell11.Name = "xrTableCell11";
        this.xrTableCell11.StylePriority.UseFont = false;
        this.xrTableCell11.StylePriority.UseTextAlignment = false;
        this.xrTableCell11.Text = "xrTableCell11";
        this.xrTableCell11.TextAlignment = DevExpress.XtraPrinting.TextAlignment.TopCenter;
        this.xrTableCell11.Weight = 0.79441345214843739;
        // 
        // xrTableCell12
        // 
        this.xrTableCell12.DataBindings.AddRange(new DevExpress.XtraReports.UI.XRBinding[] {
            new DevExpress.XtraReports.UI.XRBinding("Text", null, "sp_Att_AttendanceRegister_Report.Is_Leave")});
        this.xrTableCell12.Font = new System.Drawing.Font("Verdana", 8F);
        this.xrTableCell12.Name = "xrTableCell12";
        this.xrTableCell12.StylePriority.UseFont = false;
        this.xrTableCell12.StylePriority.UseTextAlignment = false;
        this.xrTableCell12.Text = "xrTableCell12";
        this.xrTableCell12.TextAlignment = DevExpress.XtraPrinting.TextAlignment.TopCenter;
        this.xrTableCell12.Weight = 0.69892211983505892;
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
        this.BottomMargin.HeightF = 0F;
        this.BottomMargin.Name = "BottomMargin";
        this.BottomMargin.Padding = new DevExpress.XtraPrinting.PaddingInfo(0, 0, 0, 0, 100F);
        this.BottomMargin.TextAlignment = DevExpress.XtraPrinting.TextAlignment.TopLeft;
        // 
        // GroupHeader1
        // 
        this.GroupHeader1.Controls.AddRange(new DevExpress.XtraReports.UI.XRControl[] {
            this.xrTable1});
        this.GroupHeader1.HeightF = 18.54165F;
        this.GroupHeader1.Name = "GroupHeader1";
        // 
        // xrTable1
        // 
        this.xrTable1.Borders = ((DevExpress.XtraPrinting.BorderSide)(((DevExpress.XtraPrinting.BorderSide.Left | DevExpress.XtraPrinting.BorderSide.Right)
                    | DevExpress.XtraPrinting.BorderSide.Bottom)));
        this.xrTable1.Font = new System.Drawing.Font("Verdana", 8F, System.Drawing.FontStyle.Bold);
        this.xrTable1.LocationFloat = new DevExpress.Utils.PointFloat(10F, 0F);
        this.xrTable1.Name = "xrTable1";
        this.xrTable1.Rows.AddRange(new DevExpress.XtraReports.UI.XRTableRow[] {
            this.xrTableRow1});
        this.xrTable1.SizeF = new System.Drawing.SizeF(881F, 18.54165F);
        this.xrTable1.StylePriority.UseBorders = false;
        this.xrTable1.StylePriority.UseFont = false;
        // 
        // xrTableRow1
        // 
        this.xrTableRow1.Cells.AddRange(new DevExpress.XtraReports.UI.XRTableCell[] {
            this.xrTableCell21,
            this.xrTableCell7,
            this.xrTableCell1,
            this.xrTableCell22,
            this.xrTableCell24,
            this.xrTableCell25,
            this.xrTableCell23,
            this.xrTableCell26,
            this.xrTableCell3,
            this.xrTableCell2,
            this.xrTableCell4});
        this.xrTableRow1.Name = "xrTableRow1";
        this.xrTableRow1.Weight = 1;
        // 
        // xrTableCell21
        // 
        this.xrTableCell21.Name = "xrTableCell21";
        this.xrTableCell21.Text = "Name";
        this.xrTableCell21.Weight = 1.4316496972943049;
        // 
        // xrTableCell7
        // 
        this.xrTableCell7.Name = "xrTableCell7";
        this.xrTableCell7.Text = "Date";
        this.xrTableCell7.Weight = 0.76950037203886257;
        // 
        // xrTableCell1
        // 
        this.xrTableCell1.Name = "xrTableCell1";
        this.xrTableCell1.Text = "In Time";
        this.xrTableCell1.Weight = 0.50529712870880239;
        // 
        // xrTableCell22
        // 
        this.xrTableCell22.Name = "xrTableCell22";
        this.xrTableCell22.Text = "Out Time";
        this.xrTableCell22.Weight = 0.55833738523356735;
        // 
        // xrTableCell24
        // 
        this.xrTableCell24.Name = "xrTableCell24";
        this.xrTableCell24.Text = "Work Hour";
        this.xrTableCell24.Weight = 0.69451261757313243;
        // 
        // xrTableCell25
        // 
        this.xrTableCell25.Name = "xrTableCell25";
        this.xrTableCell25.Text = "OT Hour";
        this.xrTableCell25.Weight = 0.5918340793384701;
        // 
        // xrTableCell23
        // 
        this.xrTableCell23.Name = "xrTableCell23";
        this.xrTableCell23.Text = "Salary";
        this.xrTableCell23.Weight = 0.62204175888056668;
        // 
        // xrTableCell26
        // 
        this.xrTableCell26.Name = "xrTableCell26";
        this.xrTableCell26.Text = "OT Salary";
        this.xrTableCell26.Weight = 0.720290384830808;
        // 
        // xrTableCell3
        // 
        this.xrTableCell3.Name = "xrTableCell3";
        this.xrTableCell3.Text = "Is Off";
        this.xrTableCell3.Weight = 0.47485877541759114;
        // 
        // xrTableCell2
        // 
        this.xrTableCell2.Name = "xrTableCell2";
        this.xrTableCell2.Text = "Is Holiday";
        this.xrTableCell2.Weight = 0.690856695969327;
        // 
        // xrTableCell4
        // 
        this.xrTableCell4.Name = "xrTableCell4";
        this.xrTableCell4.Text = "Is Leave";
        this.xrTableCell4.Weight = 0.60237534443706453;
        // 
        // PageFooter
        // 
        this.PageFooter.Controls.AddRange(new DevExpress.XtraReports.UI.XRControl[] {
            this.xrPageInfo2,
            this.xrPageInfo1});
        this.PageFooter.HeightF = 23.95833F;
        this.PageFooter.Name = "PageFooter";
        // 
        // xrPageInfo2
        // 
        this.xrPageInfo2.Format = "Page{0}";
        this.xrPageInfo2.LocationFloat = new DevExpress.Utils.PointFloat(595.7918F, 0F);
        this.xrPageInfo2.Name = "xrPageInfo2";
        this.xrPageInfo2.Padding = new DevExpress.XtraPrinting.PaddingInfo(2, 2, 0, 0, 100F);
        this.xrPageInfo2.SizeF = new System.Drawing.SizeF(295.2082F, 23F);
        this.xrPageInfo2.StylePriority.UseTextAlignment = false;
        this.xrPageInfo2.TextAlignment = DevExpress.XtraPrinting.TextAlignment.TopRight;
        // 
        // xrPageInfo1
        // 
        this.xrPageInfo1.LocationFloat = new DevExpress.Utils.PointFloat(10.00001F, 0F);
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
        // sp_Att_ScheduleDescription_ReportTableAdapter1
        // 
        this.sp_Att_ScheduleDescription_ReportTableAdapter1.ClearBeforeFill = true;
        // 
        // ReportHeader
        // 
        this.ReportHeader.Controls.AddRange(new DevExpress.XtraReports.UI.XRControl[] {
            this.xrPanel1});
        this.ReportHeader.HeightF = 75F;
        this.ReportHeader.Name = "ReportHeader";
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
        this.xrPanel1.LocationFloat = new DevExpress.Utils.PointFloat(9.999998F, 5.000003F);
        this.xrPanel1.Name = "xrPanel1";
        this.xrPanel1.SizeF = new System.Drawing.SizeF(881F, 70F);
        this.xrPanel1.StylePriority.UseBorders = false;
        // 
        // xrCompAddress
        // 
        this.xrCompAddress.Borders = DevExpress.XtraPrinting.BorderSide.None;
        this.xrCompAddress.Font = new System.Drawing.Font("Verdana", 9.75F, System.Drawing.FontStyle.Bold);
        this.xrCompAddress.LocationFloat = new DevExpress.Utils.PointFloat(7.000001F, 27F);
        this.xrCompAddress.Name = "xrCompAddress";
        this.xrCompAddress.Padding = new DevExpress.XtraPrinting.PaddingInfo(2, 2, 0, 0, 100F);
        this.xrCompAddress.SizeF = new System.Drawing.SizeF(353.125F, 22.04167F);
        this.xrCompAddress.StylePriority.UseBorders = false;
        this.xrCompAddress.StylePriority.UseFont = false;
        this.xrCompAddress.Text = "xrCompAddress";
        // 
        // xrTitle
        // 
        this.xrTitle.Borders = DevExpress.XtraPrinting.BorderSide.None;
        this.xrTitle.Font = new System.Drawing.Font("Verdana", 9.75F, System.Drawing.FontStyle.Bold);
        this.xrTitle.LocationFloat = new DevExpress.Utils.PointFloat(250.9532F, 51.00001F);
        this.xrTitle.Name = "xrTitle";
        this.xrTitle.Padding = new DevExpress.XtraPrinting.PaddingInfo(2, 2, 0, 0, 100F);
        this.xrTitle.SizeF = new System.Drawing.SizeF(450.1771F, 16F);
        this.xrTitle.StylePriority.UseBorders = false;
        this.xrTitle.StylePriority.UseFont = false;
        this.xrTitle.StylePriority.UseTextAlignment = false;
        this.xrTitle.Text = "xrTitle";
        this.xrTitle.TextAlignment = DevExpress.XtraPrinting.TextAlignment.TopCenter;
        // 
        // xrPictureBox1
        // 
        this.xrPictureBox1.Borders = DevExpress.XtraPrinting.BorderSide.None;
        this.xrPictureBox1.LocationFloat = new DevExpress.Utils.PointFloat(778.0001F, 1.999996F);
        this.xrPictureBox1.Name = "xrPictureBox1";
        this.xrPictureBox1.SizeF = new System.Drawing.SizeF(100F, 45.04167F);
        this.xrPictureBox1.Sizing = DevExpress.XtraPrinting.ImageSizeMode.StretchImage;
        this.xrPictureBox1.StylePriority.UseBorders = false;
        // 
        // xrCompName
        // 
        this.xrCompName.Borders = DevExpress.XtraPrinting.BorderSide.None;
        this.xrCompName.Font = new System.Drawing.Font("Verdana", 9.75F, System.Drawing.FontStyle.Bold);
        this.xrCompName.LocationFloat = new DevExpress.Utils.PointFloat(7.000001F, 2F);
        this.xrCompName.Name = "xrCompName";
        this.xrCompName.Padding = new DevExpress.XtraPrinting.PaddingInfo(2, 2, 0, 0, 100F);
        this.xrCompName.SizeF = new System.Drawing.SizeF(353.125F, 23F);
        this.xrCompName.StylePriority.UseBorders = false;
        this.xrCompName.StylePriority.UseFont = false;
        this.xrCompName.Text = "xrCompName";
        // 
        // GroupHeader2
        // 
        this.GroupHeader2.Controls.AddRange(new DevExpress.XtraReports.UI.XRControl[] {
            this.xrTable3});
        this.GroupHeader2.GroupFields.AddRange(new DevExpress.XtraReports.UI.GroupField[] {
            new DevExpress.XtraReports.UI.GroupField("Emp_Code", DevExpress.XtraReports.UI.XRColumnSortOrder.Ascending)});
        this.GroupHeader2.HeightF = 14.99999F;
        this.GroupHeader2.Level = 1;
        this.GroupHeader2.Name = "GroupHeader2";
        // 
        // xrTable3
        // 
        this.xrTable3.Borders = ((DevExpress.XtraPrinting.BorderSide)(((DevExpress.XtraPrinting.BorderSide.Left | DevExpress.XtraPrinting.BorderSide.Right)
                    | DevExpress.XtraPrinting.BorderSide.Bottom)));
        this.xrTable3.Font = new System.Drawing.Font("Verdana", 8F, System.Drawing.FontStyle.Bold);
        this.xrTable3.LocationFloat = new DevExpress.Utils.PointFloat(10.00001F, 0F);
        this.xrTable3.Name = "xrTable3";
        this.xrTable3.Rows.AddRange(new DevExpress.XtraReports.UI.XRTableRow[] {
            this.xrTableRow3});
        this.xrTable3.SizeF = new System.Drawing.SizeF(880.9999F, 14.99999F);
        this.xrTable3.StylePriority.UseBorders = false;
        this.xrTable3.StylePriority.UseFont = false;
        // 
        // xrTableRow3
        // 
        this.xrTableRow3.Cells.AddRange(new DevExpress.XtraReports.UI.XRTableCell[] {
            this.xrTableCell13,
            this.xrTableCell14,
            this.xrTableCell16,
            this.xrTableCell17,
            this.xrTableCell18,
            this.xrTableCell15});
        this.xrTableRow3.Name = "xrTableRow3";
        this.xrTableRow3.Weight = 1;
        // 
        // xrTableCell13
        // 
        this.xrTableCell13.Name = "xrTableCell13";
        this.xrTableCell13.Text = "ID";
        this.xrTableCell13.Weight = 0.3645834350585937;
        // 
        // xrTableCell14
        // 
        this.xrTableCell14.Font = new System.Drawing.Font("Times New Roman", 9.75F, System.Drawing.FontStyle.Bold);
        this.xrTableCell14.Name = "xrTableCell14";
        this.xrTableCell14.StylePriority.UseFont = false;
        this.xrTableCell14.StylePriority.UseTextAlignment = false;
        this.xrTableCell14.Text = ":";
        this.xrTableCell14.TextAlignment = DevExpress.XtraPrinting.TextAlignment.TopCenter;
        this.xrTableCell14.Weight = 0.13541648864746092;
        // 
        // xrTableCell16
        // 
        this.xrTableCell16.DataBindings.AddRange(new DevExpress.XtraReports.UI.XRBinding[] {
            new DevExpress.XtraReports.UI.XRBinding("Text", null, "sp_Att_AttendanceRegister_Report.Emp_Code")});
        this.xrTableCell16.Name = "xrTableCell16";
        this.xrTableCell16.Text = "xrTableCell16";
        this.xrTableCell16.Weight = 0.96875007629394538;
        // 
        // xrTableCell17
        // 
        this.xrTableCell17.Name = "xrTableCell17";
        this.xrTableCell17.Text = "Name";
        this.xrTableCell17.Weight = 0.49263153076171884;
        // 
        // xrTableCell18
        // 
        this.xrTableCell18.Font = new System.Drawing.Font("Times New Roman", 9.75F, System.Drawing.FontStyle.Bold);
        this.xrTableCell18.Name = "xrTableCell18";
        this.xrTableCell18.StylePriority.UseFont = false;
        this.xrTableCell18.StylePriority.UseTextAlignment = false;
        this.xrTableCell18.Text = ":";
        this.xrTableCell18.TextAlignment = DevExpress.XtraPrinting.TextAlignment.TopCenter;
        this.xrTableCell18.Weight = 0.15929840087890612;
        // 
        // xrTableCell15
        // 
        this.xrTableCell15.DataBindings.AddRange(new DevExpress.XtraReports.UI.XRBinding[] {
            new DevExpress.XtraReports.UI.XRBinding("Text", null, "sp_Att_AttendanceRegister_Report.Emp_Name")});
        this.xrTableCell15.Name = "xrTableCell15";
        this.xrTableCell15.Text = "xrTableCell15";
        this.xrTableCell15.Weight = 4.5933091257450656;
        // 
        // sp_Att_AttendanceRegister_ReportTableAdapter1
        // 
        this.sp_Att_AttendanceRegister_ReportTableAdapter1.ClearBeforeFill = true;
        // 
        // GroupFooter1
        // 
        this.GroupFooter1.Controls.AddRange(new DevExpress.XtraReports.UI.XRControl[] {
            this.xrTable4});
        this.GroupFooter1.HeightF = 70.00002F;
        this.GroupFooter1.Name = "GroupFooter1";
        // 
        // xrTable4
        // 
        this.xrTable4.Borders = ((DevExpress.XtraPrinting.BorderSide)(((DevExpress.XtraPrinting.BorderSide.Left | DevExpress.XtraPrinting.BorderSide.Right)
                    | DevExpress.XtraPrinting.BorderSide.Bottom)));
        this.xrTable4.Font = new System.Drawing.Font("Verdana", 8F, System.Drawing.FontStyle.Bold);
        this.xrTable4.LocationFloat = new DevExpress.Utils.PointFloat(9.999998F, 0F);
        this.xrTable4.Name = "xrTable4";
        this.xrTable4.Rows.AddRange(new DevExpress.XtraReports.UI.XRTableRow[] {
            this.xrTableRow4,
            this.xrTableRow7,
            this.xrTableRow5,
            this.xrTableRow6});
        this.xrTable4.SizeF = new System.Drawing.SizeF(881.0001F, 70.00002F);
        this.xrTable4.StylePriority.UseBorders = false;
        this.xrTable4.StylePriority.UseFont = false;
        // 
        // xrTableRow4
        // 
        this.xrTableRow4.Cells.AddRange(new DevExpress.XtraReports.UI.XRTableCell[] {
            this.xrTableCell19,
            this.xrTableCell30,
            this.xrTableCell31,
            this.xrTableCell35});
        this.xrTableRow4.Name = "xrTableRow4";
        this.xrTableRow4.Weight = 1;
        // 
        // xrTableCell19
        // 
        this.xrTableCell19.Name = "xrTableCell19";
        this.xrTableCell19.Text = "ID ";
        this.xrTableCell19.Weight = 0.46130226035871841;
        // 
        // xrTableCell30
        // 
        this.xrTableCell30.DataBindings.AddRange(new DevExpress.XtraReports.UI.XRBinding[] {
            new DevExpress.XtraReports.UI.XRBinding("Text", null, "sp_Att_AttendanceRegister_Report.Emp_Code")});
        this.xrTableCell30.Font = new System.Drawing.Font("Verdana", 8F);
        this.xrTableCell30.Name = "xrTableCell30";
        this.xrTableCell30.StylePriority.UseFont = false;
        this.xrTableCell30.Text = "xrTableCell30";
        this.xrTableCell30.Weight = 0.5082759017956533;
        // 
        // xrTableCell31
        // 
        this.xrTableCell31.Name = "xrTableCell31";
        this.xrTableCell31.Text = "Name ";
        this.xrTableCell31.Weight = 0.58291212169362827;
        // 
        // xrTableCell35
        // 
        this.xrTableCell35.DataBindings.AddRange(new DevExpress.XtraReports.UI.XRBinding[] {
            new DevExpress.XtraReports.UI.XRBinding("Text", null, "sp_Att_AttendanceRegister_Report.Emp_Name")});
        this.xrTableCell35.Font = new System.Drawing.Font("Verdana", 8F);
        this.xrTableCell35.Name = "xrTableCell35";
        this.xrTableCell35.StylePriority.UseFont = false;
        this.xrTableCell35.Text = "xrTableCell35";
        this.xrTableCell35.Weight = 1.5894516185475991;
        // 
        // xrTableRow7
        // 
        this.xrTableRow7.Cells.AddRange(new DevExpress.XtraReports.UI.XRTableCell[] {
            this.xrTableCell41,
            this.xrTableCell42,
            this.xrTableCell46,
            this.xrTableCell50,
            this.xrTableCell43,
            this.xrTableCell44});
        this.xrTableRow7.Name = "xrTableRow7";
        this.xrTableRow7.Weight = 1;
        // 
        // xrTableCell41
        // 
        this.xrTableCell41.Name = "xrTableCell41";
        this.xrTableCell41.Text = "Week Off Count";
        this.xrTableCell41.Weight = 0.46130231886021961;
        // 
        // xrTableCell42
        // 
        this.xrTableCell42.DataBindings.AddRange(new DevExpress.XtraReports.UI.XRBinding[] {
            new DevExpress.XtraReports.UI.XRBinding("Text", null, "sp_Att_AttendanceRegister_Report.Emp_Id")});
        this.xrTableCell42.Font = new System.Drawing.Font("Verdana", 8F);
        this.xrTableCell42.Name = "xrTableCell42";
        this.xrTableCell42.StylePriority.UseFont = false;
        this.xrTableCell42.Text = "xrTableCell42";
        this.xrTableCell42.Weight = 0.50827563624101046;
        this.xrTableCell42.BeforePrint += new System.Drawing.Printing.PrintEventHandler(this.xrTableCell42_BeforePrint);
        // 
        // xrTableCell46
        // 
        this.xrTableCell46.Name = "xrTableCell46";
        this.xrTableCell46.Text = "Holiday Count";
        this.xrTableCell46.Weight = 0.58291255322333224;
        // 
        // xrTableCell50
        // 
        this.xrTableCell50.DataBindings.AddRange(new DevExpress.XtraReports.UI.XRBinding[] {
            new DevExpress.XtraReports.UI.XRBinding("Text", null, "sp_Att_AttendanceRegister_Report.Emp_Id")});
        this.xrTableCell50.Font = new System.Drawing.Font("Verdana", 8F);
        this.xrTableCell50.Name = "xrTableCell50";
        this.xrTableCell50.StylePriority.UseFont = false;
        this.xrTableCell50.Text = "xrTableCell50";
        this.xrTableCell50.Weight = 0.53678187032889058;
        this.xrTableCell50.BeforePrint += new System.Drawing.Printing.PrintEventHandler(this.xrTableCell50_BeforePrint);
        // 
        // xrTableCell43
        // 
        this.xrTableCell43.Name = "xrTableCell43";
        this.xrTableCell43.Text = "Leave Count";
        this.xrTableCell43.Weight = 0.52898069775017953;
        // 
        // xrTableCell44
        // 
        this.xrTableCell44.DataBindings.AddRange(new DevExpress.XtraReports.UI.XRBinding[] {
            new DevExpress.XtraReports.UI.XRBinding("Text", null, "sp_Att_AttendanceRegister_Report.Emp_Id")});
        this.xrTableCell44.Font = new System.Drawing.Font("Verdana", 8F);
        this.xrTableCell44.Name = "xrTableCell44";
        this.xrTableCell44.StylePriority.UseFont = false;
        this.xrTableCell44.Text = "xrTableCell44";
        this.xrTableCell44.Weight = 0.52368882599196653;
        this.xrTableCell44.BeforePrint += new System.Drawing.Printing.PrintEventHandler(this.xrTableCell44_BeforePrint);
        // 
        // xrTableRow5
        // 
        this.xrTableRow5.Cells.AddRange(new DevExpress.XtraReports.UI.XRTableCell[] {
            this.xrTableCell32,
            this.xrTableCell33,
            this.xrTableCell34,
            this.xrTableCell47,
            this.xrTableCell45,
            this.xrTableCell36});
        this.xrTableRow5.Name = "xrTableRow5";
        this.xrTableRow5.Weight = 1;
        // 
        // xrTableCell32
        // 
        this.xrTableCell32.Name = "xrTableCell32";
        this.xrTableCell32.Text = "Work Hour";
        this.xrTableCell32.Weight = 0.46130236439919259;
        // 
        // xrTableCell33
        // 
        this.xrTableCell33.DataBindings.AddRange(new DevExpress.XtraReports.UI.XRBinding[] {
            new DevExpress.XtraReports.UI.XRBinding("Text", null, "sp_Att_AttendanceRegister_Report.Emp_Id")});
        this.xrTableCell33.Font = new System.Drawing.Font("Verdana", 8F);
        this.xrTableCell33.Name = "xrTableCell33";
        this.xrTableCell33.StylePriority.UseFont = false;
        this.xrTableCell33.Text = "xrTableCell33";
        this.xrTableCell33.Weight = 0.50827568949126456;
        this.xrTableCell33.BeforePrint += new System.Drawing.Printing.PrintEventHandler(this.xrTableCell33_BeforePrint);
        // 
        // xrTableCell34
        // 
        this.xrTableCell34.Name = "xrTableCell34";
        this.xrTableCell34.Text = "Assigned Hour";
        this.xrTableCell34.Weight = 0.58291216022996017;
        // 
        // xrTableCell47
        // 
        this.xrTableCell47.DataBindings.AddRange(new DevExpress.XtraReports.UI.XRBinding[] {
            new DevExpress.XtraReports.UI.XRBinding("Text", null, "sp_Att_AttendanceRegister_Report.Emp_Id")});
        this.xrTableCell47.Font = new System.Drawing.Font("Verdana", 8F);
        this.xrTableCell47.Name = "xrTableCell47";
        this.xrTableCell47.StylePriority.UseFont = false;
        this.xrTableCell47.Text = "xrTableCell47";
        this.xrTableCell47.Weight = 0.53678187447915626;
        this.xrTableCell47.BeforePrint += new System.Drawing.Printing.PrintEventHandler(this.xrTableCell47_BeforePrint);
        // 
        // xrTableCell45
        // 
        this.xrTableCell45.Name = "xrTableCell45";
        this.xrTableCell45.Text = "Over Time Hour";
        this.xrTableCell45.Weight = 0.52898098780405878;
        // 
        // xrTableCell36
        // 
        this.xrTableCell36.DataBindings.AddRange(new DevExpress.XtraReports.UI.XRBinding[] {
            new DevExpress.XtraReports.UI.XRBinding("Text", null, "sp_Att_AttendanceRegister_Report.Field2")});
        this.xrTableCell36.Font = new System.Drawing.Font("Verdana", 8F);
        this.xrTableCell36.Name = "xrTableCell36";
        this.xrTableCell36.StylePriority.UseFont = false;
        this.xrTableCell36.Text = "xrTableCell36";
        this.xrTableCell36.Weight = 0.52368882599196653;
        this.xrTableCell36.BeforePrint += new System.Drawing.Printing.PrintEventHandler(this.xrTableCell36_BeforePrint);
        // 
        // xrTableRow6
        // 
        this.xrTableRow6.Cells.AddRange(new DevExpress.XtraReports.UI.XRTableCell[] {
            this.xrTableCell37,
            this.xrTableCell38,
            this.xrTableCell39,
            this.xrTableCell48,
            this.xrTableCell49,
            this.xrTableCell40});
        this.xrTableRow6.Name = "xrTableRow6";
        this.xrTableRow6.Weight = 1;
        // 
        // xrTableCell37
        // 
        this.xrTableCell37.Name = "xrTableCell37";
        this.xrTableCell37.Text = "Work Salary";
        this.xrTableCell37.Weight = 0.46130241881715561;
        // 
        // xrTableCell38
        // 
        this.xrTableCell38.DataBindings.AddRange(new DevExpress.XtraReports.UI.XRBinding[] {
            new DevExpress.XtraReports.UI.XRBinding("Text", null, "sp_Att_AttendanceRegister_Report.Field3")});
        this.xrTableCell38.Font = new System.Drawing.Font("Verdana", 8F);
        this.xrTableCell38.Name = "xrTableCell38";
        this.xrTableCell38.StylePriority.UseFont = false;
        xrSummary1.Running = DevExpress.XtraReports.UI.SummaryRunning.Group;
        this.xrTableCell38.Summary = xrSummary1;
        this.xrTableCell38.Text = "xrTableCell38";
        this.xrTableCell38.Weight = 0.50827563507330153;
        // 
        // xrTableCell39
        // 
        this.xrTableCell39.Name = "xrTableCell39";
        this.xrTableCell39.Text = "Over Time Salary";
        this.xrTableCell39.Weight = 0.58291216022996017;
        // 
        // xrTableCell48
        // 
        this.xrTableCell48.DataBindings.AddRange(new DevExpress.XtraReports.UI.XRBinding[] {
            new DevExpress.XtraReports.UI.XRBinding("Text", null, "sp_Att_AttendanceRegister_Report.Field4")});
        this.xrTableCell48.Font = new System.Drawing.Font("Verdana", 8F);
        this.xrTableCell48.Name = "xrTableCell48";
        this.xrTableCell48.StylePriority.UseFont = false;
        xrSummary2.Running = DevExpress.XtraReports.UI.SummaryRunning.Group;
        this.xrTableCell48.Summary = xrSummary2;
        this.xrTableCell48.Text = "xrTableCell48";
        this.xrTableCell48.Weight = 0.53678187447915626;
        // 
        // xrTableCell49
        // 
        this.xrTableCell49.Name = "xrTableCell49";
        this.xrTableCell49.Text = "Gross Total";
        this.xrTableCell49.Weight = 0.52898098780405878;
        // 
        // xrTableCell40
        // 
        this.xrTableCell40.DataBindings.AddRange(new DevExpress.XtraReports.UI.XRBinding[] {
            new DevExpress.XtraReports.UI.XRBinding("Text", null, "sp_Att_AttendanceRegister_Report.Field5")});
        this.xrTableCell40.Font = new System.Drawing.Font("Verdana", 8F);
        this.xrTableCell40.Name = "xrTableCell40";
        this.xrTableCell40.StylePriority.UseFont = false;
        this.xrTableCell40.Text = "xrTableCell40";
        this.xrTableCell40.Weight = 0.52368882599196653;
        // 
        // Att_DailySalaryReport1
        // 
        this.Bands.AddRange(new DevExpress.XtraReports.UI.Band[] {
            this.Detail,
            this.TopMargin,
            this.BottomMargin,
            this.GroupHeader1,
            this.PageFooter,
            this.ReportHeader,
            this.GroupHeader2,
            this.GroupFooter1});
        this.DataAdapter = this.sp_Att_AttendanceRegister_ReportTableAdapter1;
        this.DataMember = "sp_Att_AttendanceRegister_Report";
        this.DataSource = this.attendanceDataSet1;
        this.Font = new System.Drawing.Font("Times New Roman", 9.75F);
        this.Margins = new System.Drawing.Printing.Margins(0, 1, 0, 0);
        this.PageWidth = 900;
        this.PaperKind = System.Drawing.Printing.PaperKind.Custom;
        this.Version = "10.2";
        ((System.ComponentModel.ISupportInitialize)(this.xrTable2)).EndInit();
        ((System.ComponentModel.ISupportInitialize)(this.xrTable1)).EndInit();
        ((System.ComponentModel.ISupportInitialize)(this.attendanceDataSet1)).EndInit();
        ((System.ComponentModel.ISupportInitialize)(this.xrTable3)).EndInit();
        ((System.ComponentModel.ISupportInitialize)(this.xrTable4)).EndInit();
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

    private void xrTableCell27_BeforePrint(object sender, System.Drawing.Printing.PrintEventArgs e)
    {
        xrTableCell27.Text = GetHours(xrTableCell27.Text);
    }

    private void xrTableCell20_BeforePrint(object sender, System.Drawing.Printing.PrintEventArgs e)
    {
        xrTableCell20.Text = GetHours(xrTableCell20.Text);
    }

    

    private void xrTableCell42_BeforePrint(object sender, System.Drawing.Printing.PrintEventArgs e)
    {
        //week

        int sum = 0;
        DateTime fromdate = objSys.getDateForInput(System.Web.HttpContext.Current.Session["FromDate"].ToString());
        DateTime ToDate = objSys.getDateForInput(System.Web.HttpContext.Current.Session["ToDate"].ToString());


        DataTable dt = objAttReg.GetAttendanceRegDataByEmpId(xrTableCell42.Text, fromdate.ToString(), ToDate.ToString());
        dt = new DataView(dt, "Is_Week_Off='True'", "", DataViewRowState.CurrentRows).ToTable();
        if (dt.Rows.Count > 0)
        {
            sum = dt.Rows.Count;
        }
        xrTableCell42.Text = (sum.ToString());
    }

    private void xrTableCell50_BeforePrint(object sender, System.Drawing.Printing.PrintEventArgs e)
    {
        //holi
        int sum = 0;
        DateTime fromdate = objSys.getDateForInput(System.Web.HttpContext.Current.Session["FromDate"].ToString());
        DateTime ToDate = objSys.getDateForInput(System.Web.HttpContext.Current.Session["ToDate"].ToString());


        DataTable dt = objAttReg.GetAttendanceRegDataByEmpId(xrTableCell50.Text, fromdate.ToString(), ToDate.ToString());
        dt = new DataView(dt, "Is_Holiday='True'", "", DataViewRowState.CurrentRows).ToTable();
        if (dt.Rows.Count > 0)
        {
            sum = dt.Rows.Count;
        }
        xrTableCell50.Text = (sum.ToString());
    }

    private void xrTableCell44_BeforePrint(object sender, System.Drawing.Printing.PrintEventArgs e)
    {
        //leave
        int sum = 0;
        DateTime fromdate = objSys.getDateForInput(System.Web.HttpContext.Current.Session["FromDate"].ToString());
        DateTime ToDate = objSys.getDateForInput(System.Web.HttpContext.Current.Session["ToDate"].ToString());


        DataTable dt = objAttReg.GetAttendanceRegDataByEmpId(xrTableCell44.Text, fromdate.ToString(), ToDate.ToString());
        dt = new DataView(dt, "Is_Leave='True'", "", DataViewRowState.CurrentRows).ToTable();
        if (dt.Rows.Count > 0)
        {
            sum = dt.Rows.Count;
        }
        xrTableCell44.Text = (sum.ToString());

    }

    private void xrTableCell33_BeforePrint(object sender, System.Drawing.Printing.PrintEventArgs e)
    {
        int sum = 0;
        DateTime fromdate = objSys.getDateForInput(System.Web.HttpContext.Current.Session["FromDate"].ToString());
        DateTime ToDate = objSys.getDateForInput(System.Web.HttpContext.Current.Session["ToDate"].ToString());


        DataTable dt = objAttReg.GetAttendanceRegDataByEmpId(xrTableCell33.Text, fromdate.ToString(), ToDate.ToString());
        dt = new DataView(dt, "EffectiveWork_Min<>0", "", DataViewRowState.CurrentRows).ToTable();
        if (dt.Rows.Count > 0)
        {
            for (int i = 0; i < dt.Rows.Count; i++)
            {

                sum += int.Parse(dt.Rows[i]["EffectiveWork_Min"].ToString());
            }

        }
        xrTableCell33.Text = GetHours(sum.ToString());

    }

    private void xrTableCell47_BeforePrint(object sender, System.Drawing.Printing.PrintEventArgs e)
    {
        int sum = 0;
        DateTime fromdate = objSys.getDateForInput(System.Web.HttpContext.Current.Session["FromDate"].ToString());
        DateTime ToDate = objSys.getDateForInput(System.Web.HttpContext.Current.Session["ToDate"].ToString());


        DataTable dt = objAttReg.GetAttendanceRegDataByEmpId(xrTableCell47.Text, fromdate.ToString(), ToDate.ToString());
        dt = new DataView(dt, "TotalAssign_Min<>0", "", DataViewRowState.CurrentRows).ToTable();
        if (dt.Rows.Count > 0)
        {
            for (int i = 0; i < dt.Rows.Count; i++)
            {

                sum += int.Parse(dt.Rows[i]["TotalAssign_Min"].ToString());
            }

        }
        xrTableCell47.Text = GetHours(sum.ToString());
    }

    private void xrTableCell36_BeforePrint(object sender, System.Drawing.Printing.PrintEventArgs e)
    {
        xrTableCell36.Text = GetHours(xrTableCell36.Text);
    }

    private void xrTableCell6_BeforePrint(object sender, System.Drawing.Printing.PrintEventArgs e)
    {
        if (xrTableCell6.Text != "")
        {
            xrTableCell6.Text = Convert.ToDateTime(xrTableCell6.Text).ToString(objSys.SetDateFormat());

        }
    }

   
    
}
