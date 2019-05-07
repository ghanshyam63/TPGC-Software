using System;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using DevExpress.XtraReports.UI;
using System.Data;

/// <summary>
/// Summary description for Att_ShiftReport
/// </summary>
public class Att_InOutReport : DevExpress.XtraReports.UI.XtraReport
{
    SystemParameter objsys = new SystemParameter();
    Set_Employee_Holiday objEmpHoli = new Set_Employee_Holiday();
    Att_AttendanceRegister objAttReg = new Att_AttendanceRegister();
	private DevExpress.XtraReports.UI.DetailBand Detail;
	private DevExpress.XtraReports.UI.TopMarginBand TopMargin;
	private DevExpress.XtraReports.UI.BottomMarginBand BottomMargin;
    private GroupHeaderBand GroupHeader1;
    private PageFooterBand PageFooter;
    private XRTable xrTable1;
    private XRTableRow xrTableRow1;
    private XRTableCell xrTableCell2;
    private XRTableCell xrTableCell3;
    private XRTableCell xrTableCell4;
    private XRTableCell xrTableCell7;
    private AttendanceDataSet attendanceDataSet1;
    private AttendanceDataSetTableAdapters.sp_Att_ScheduleDescription_ReportTableAdapter sp_Att_ScheduleDescription_ReportTableAdapter1;
    private ReportHeaderBand ReportHeader;
    private GroupHeaderBand GroupHeader2;
    private XRTable xrTable3;
    private XRTableRow xrTableRow3;
    private XRTableCell xrTableCell13;
    private XRTableCell xrTableCell14;
    private XRTableCell xrTableCell15;
    private XRTableCell xrTableCell16;
    private XRTableCell xrTableCell17;
    private XRTableCell xrTableCell18;
    private XRPageInfo xrPageInfo2;
    private XRPageInfo xrPageInfo1;
    private XRTableCell xrTableCell21;
    private XRTableCell xrTableCell1;
    private XRTableCell xrTableCell22;
    private XRTableCell xrTableCell6;
    private XRTableCell xrTableCell23;
    private XRTableCell xrTableCell24;
    private XRTableCell xrTableCell25;
    private XRTableCell xrTableCell26;
    private XRTable xrTable2;
    private XRTableRow xrTableRow2;
    private XRTableCell xrTableCell5;
    private XRTableCell xrTableCell8;
    private XRTableCell xrTableCell9;
    private XRTableCell xrTableCell10;
    private XRTableCell xrTableCell11;
    private XRTableCell xrTableCell12;
    private XRTableCell xrTableCell19;
    private XRTableCell xrTableCell20;
    private XRTableCell xrTableCell27;
    private XRTableCell xrTableCell28;
    private XRTableCell xrTableCell29;
    private XRTableCell xrTableCell30;
    private AttendanceDataSetTableAdapters.sp_Att_AttendanceRegister_ReportTableAdapter sp_Att_AttendanceRegister_ReportTableAdapter1;
    private GroupFooterBand GroupFooter1;
    private XRTable xrTable5;
    private XRTableRow xrTableRow5;
    private XRTableCell xrTableCell33;
    private XRTableCell xrTableCell34;
    private XRTableCell xrTableCell35;
    private XRTableCell xrTableCell32;
    private XRTableCell xrTableCell36;
    private XRTableCell xrTableCell37;
    private XRPanel xrPanel1;
    private XRLabel xrCompAddress;
    private XRLabel xrTitle;
    private XRPictureBox xrPictureBox1;
    private XRLabel xrCompName;
	/// <summary>
	/// Required designer variable.
	/// </summary>
	private System.ComponentModel.IContainer components = null;

    public Att_InOutReport()
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
        string resourceFileName = "Att_InOutReport.resx";
        this.Detail = new DevExpress.XtraReports.UI.DetailBand();
        this.xrTable2 = new DevExpress.XtraReports.UI.XRTable();
        this.xrTableRow2 = new DevExpress.XtraReports.UI.XRTableRow();
        this.xrTableCell27 = new DevExpress.XtraReports.UI.XRTableCell();
        this.xrTableCell30 = new DevExpress.XtraReports.UI.XRTableCell();
        this.xrTableCell29 = new DevExpress.XtraReports.UI.XRTableCell();
        this.xrTableCell28 = new DevExpress.XtraReports.UI.XRTableCell();
        this.xrTableCell20 = new DevExpress.XtraReports.UI.XRTableCell();
        this.xrTableCell19 = new DevExpress.XtraReports.UI.XRTableCell();
        this.xrTableCell5 = new DevExpress.XtraReports.UI.XRTableCell();
        this.xrTableCell8 = new DevExpress.XtraReports.UI.XRTableCell();
        this.xrTableCell10 = new DevExpress.XtraReports.UI.XRTableCell();
        this.xrTableCell9 = new DevExpress.XtraReports.UI.XRTableCell();
        this.xrTableCell11 = new DevExpress.XtraReports.UI.XRTableCell();
        this.xrTableCell12 = new DevExpress.XtraReports.UI.XRTableCell();
        this.TopMargin = new DevExpress.XtraReports.UI.TopMarginBand();
        this.BottomMargin = new DevExpress.XtraReports.UI.BottomMarginBand();
        this.GroupHeader1 = new DevExpress.XtraReports.UI.GroupHeaderBand();
        this.xrTable1 = new DevExpress.XtraReports.UI.XRTable();
        this.xrTableRow1 = new DevExpress.XtraReports.UI.XRTableRow();
        this.xrTableCell21 = new DevExpress.XtraReports.UI.XRTableCell();
        this.xrTableCell7 = new DevExpress.XtraReports.UI.XRTableCell();
        this.xrTableCell6 = new DevExpress.XtraReports.UI.XRTableCell();
        this.xrTableCell2 = new DevExpress.XtraReports.UI.XRTableCell();
        this.xrTableCell4 = new DevExpress.XtraReports.UI.XRTableCell();
        this.xrTableCell1 = new DevExpress.XtraReports.UI.XRTableCell();
        this.xrTableCell22 = new DevExpress.XtraReports.UI.XRTableCell();
        this.xrTableCell24 = new DevExpress.XtraReports.UI.XRTableCell();
        this.xrTableCell25 = new DevExpress.XtraReports.UI.XRTableCell();
        this.xrTableCell23 = new DevExpress.XtraReports.UI.XRTableCell();
        this.xrTableCell26 = new DevExpress.XtraReports.UI.XRTableCell();
        this.xrTableCell3 = new DevExpress.XtraReports.UI.XRTableCell();
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
        this.xrTable5 = new DevExpress.XtraReports.UI.XRTable();
        this.xrTableRow5 = new DevExpress.XtraReports.UI.XRTableRow();
        this.xrTableCell36 = new DevExpress.XtraReports.UI.XRTableCell();
        this.xrTableCell32 = new DevExpress.XtraReports.UI.XRTableCell();
        this.xrTableCell33 = new DevExpress.XtraReports.UI.XRTableCell();
        this.xrTableCell34 = new DevExpress.XtraReports.UI.XRTableCell();
        this.xrTableCell35 = new DevExpress.XtraReports.UI.XRTableCell();
        this.xrTableCell37 = new DevExpress.XtraReports.UI.XRTableCell();
        ((System.ComponentModel.ISupportInitialize)(this.xrTable2)).BeginInit();
        ((System.ComponentModel.ISupportInitialize)(this.xrTable1)).BeginInit();
        ((System.ComponentModel.ISupportInitialize)(this.attendanceDataSet1)).BeginInit();
        ((System.ComponentModel.ISupportInitialize)(this.xrTable3)).BeginInit();
        ((System.ComponentModel.ISupportInitialize)(this.xrTable5)).BeginInit();
        ((System.ComponentModel.ISupportInitialize)(this)).BeginInit();
        // 
        // Detail
        // 
        this.Detail.Controls.AddRange(new DevExpress.XtraReports.UI.XRControl[] {
            this.xrTable2});
        this.Detail.HeightF = 15.41665F;
        this.Detail.Name = "Detail";
        this.Detail.Padding = new DevExpress.XtraPrinting.PaddingInfo(0, 0, 0, 0, 100F);
        this.Detail.TextAlignment = DevExpress.XtraPrinting.TextAlignment.TopLeft;
        // 
        // xrTable2
        // 
        this.xrTable2.Borders = ((DevExpress.XtraPrinting.BorderSide)(((DevExpress.XtraPrinting.BorderSide.Left | DevExpress.XtraPrinting.BorderSide.Right)
                    | DevExpress.XtraPrinting.BorderSide.Bottom)));
        this.xrTable2.Font = new System.Drawing.Font("Times New Roman", 9.75F);
        this.xrTable2.LocationFloat = new DevExpress.Utils.PointFloat(36.45825F, 0F);
        this.xrTable2.Name = "xrTable2";
        this.xrTable2.Rows.AddRange(new DevExpress.XtraReports.UI.XRTableRow[] {
            this.xrTableRow2});
        this.xrTable2.SizeF = new System.Drawing.SizeF(846.5417F, 15.41665F);
        this.xrTable2.StylePriority.UseBorders = false;
        this.xrTable2.StylePriority.UseFont = false;
        // 
        // xrTableRow2
        // 
        this.xrTableRow2.Cells.AddRange(new DevExpress.XtraReports.UI.XRTableCell[] {
            this.xrTableCell27,
            this.xrTableCell30,
            this.xrTableCell29,
            this.xrTableCell28,
            this.xrTableCell20,
            this.xrTableCell19,
            this.xrTableCell5,
            this.xrTableCell8,
            this.xrTableCell10,
            this.xrTableCell9,
            this.xrTableCell11,
            this.xrTableCell12});
        this.xrTableRow2.Name = "xrTableRow2";
        this.xrTableRow2.Weight = 1;
        // 
        // xrTableCell27
        // 
        this.xrTableCell27.DataBindings.AddRange(new DevExpress.XtraReports.UI.XRBinding[] {
            new DevExpress.XtraReports.UI.XRBinding("Text", null, "sp_Att_AttendanceRegister_Report.Emp_Code")});
        this.xrTableCell27.Font = new System.Drawing.Font("Verdana", 8F);
        this.xrTableCell27.Name = "xrTableCell27";
        this.xrTableCell27.StylePriority.UseFont = false;
        this.xrTableCell27.Text = "xrTableCell27";
        this.xrTableCell27.Weight = 0.4999997711181639;
        // 
        // xrTableCell30
        // 
        this.xrTableCell30.DataBindings.AddRange(new DevExpress.XtraReports.UI.XRBinding[] {
            new DevExpress.XtraReports.UI.XRBinding("Text", null, "sp_Att_AttendanceRegister_Report.Att_Date")});
        this.xrTableCell30.Font = new System.Drawing.Font("Verdana", 8F);
        this.xrTableCell30.Name = "xrTableCell30";
        this.xrTableCell30.StylePriority.UseFont = false;
        this.xrTableCell30.Text = "xrTableCell30";
        this.xrTableCell30.Weight = 0.96874988555908215;
        this.xrTableCell30.BeforePrint += new System.Drawing.Printing.PrintEventHandler(this.xrTableCell30_BeforePrint);
        // 
        // xrTableCell29
        // 
        this.xrTableCell29.DataBindings.AddRange(new DevExpress.XtraReports.UI.XRBinding[] {
            new DevExpress.XtraReports.UI.XRBinding("Text", null, "sp_Att_AttendanceRegister_Report.Shift_Name")});
        this.xrTableCell29.Font = new System.Drawing.Font("Verdana", 8F);
        this.xrTableCell29.Name = "xrTableCell29";
        this.xrTableCell29.Text = "xrTableCell29";
        this.xrTableCell29.Weight = 1.0208333206176756;
        // 
        // xrTableCell28
        // 
        this.xrTableCell28.DataBindings.AddRange(new DevExpress.XtraReports.UI.XRBinding[] {
            new DevExpress.XtraReports.UI.XRBinding("Text", null, "sp_Att_AttendanceRegister_Report.OnDuty_Time")});
        this.xrTableCell28.Font = new System.Drawing.Font("Verdana", 8F);
        this.xrTableCell28.Name = "xrTableCell28";
        this.xrTableCell28.Text = "xrTableCell28";
        this.xrTableCell28.TextAlignment = DevExpress.XtraPrinting.TextAlignment.TopCenter;
        this.xrTableCell28.Weight = 0.63541610717773434;
        // 
        // xrTableCell20
        // 
        this.xrTableCell20.DataBindings.AddRange(new DevExpress.XtraReports.UI.XRBinding[] {
            new DevExpress.XtraReports.UI.XRBinding("Text", null, "sp_Att_AttendanceRegister_Report.OffDuty_Time")});
        this.xrTableCell20.Font = new System.Drawing.Font("Verdana", 8F);
        this.xrTableCell20.Name = "xrTableCell20";
        this.xrTableCell20.Text = "xrTableCell20";
        this.xrTableCell20.TextAlignment = DevExpress.XtraPrinting.TextAlignment.TopCenter;
        this.xrTableCell20.Weight = 0.67456237792968743;
        // 
        // xrTableCell19
        // 
        this.xrTableCell19.DataBindings.AddRange(new DevExpress.XtraReports.UI.XRBinding[] {
            new DevExpress.XtraReports.UI.XRBinding("Text", null, "sp_Att_AttendanceRegister_Report.In_Time")});
        this.xrTableCell19.Font = new System.Drawing.Font("Verdana", 8F);
        this.xrTableCell19.Name = "xrTableCell19";
        this.xrTableCell19.Text = "xrTableCell19";
        this.xrTableCell19.TextAlignment = DevExpress.XtraPrinting.TextAlignment.TopCenter;
        this.xrTableCell19.Weight = 0.69382476806640625;
        // 
        // xrTableCell5
        // 
        this.xrTableCell5.DataBindings.AddRange(new DevExpress.XtraReports.UI.XRBinding[] {
            new DevExpress.XtraReports.UI.XRBinding("Text", null, "sp_Att_AttendanceRegister_Report.Out_Time")});
        this.xrTableCell5.Font = new System.Drawing.Font("Verdana", 8F);
        this.xrTableCell5.Name = "xrTableCell5";
        this.xrTableCell5.Text = "xrTableCell5";
        this.xrTableCell5.TextAlignment = DevExpress.XtraPrinting.TextAlignment.TopCenter;
        this.xrTableCell5.Weight = 0.62500030517578153;
        // 
        // xrTableCell8
        // 
        this.xrTableCell8.DataBindings.AddRange(new DevExpress.XtraReports.UI.XRBinding[] {
            new DevExpress.XtraReports.UI.XRBinding("Text", null, "sp_Att_AttendanceRegister_Report.LateMin")});
        this.xrTableCell8.Font = new System.Drawing.Font("Verdana", 8F);
        this.xrTableCell8.Name = "xrTableCell8";
        this.xrTableCell8.Text = "xrTableCell8";
        this.xrTableCell8.TextAlignment = DevExpress.XtraPrinting.TextAlignment.TopCenter;
        this.xrTableCell8.Weight = 0.52398162841796925;
        this.xrTableCell8.BeforePrint += new System.Drawing.Printing.PrintEventHandler(this.xrTableCell8_BeforePrint);
        // 
        // xrTableCell10
        // 
        this.xrTableCell10.DataBindings.AddRange(new DevExpress.XtraReports.UI.XRBinding[] {
            new DevExpress.XtraReports.UI.XRBinding("Text", null, "sp_Att_AttendanceRegister_Report.EarlyMin")});
        this.xrTableCell10.Font = new System.Drawing.Font("Verdana", 8F);
        this.xrTableCell10.Name = "xrTableCell10";
        this.xrTableCell10.Text = "xrTableCell10";
        this.xrTableCell10.TextAlignment = DevExpress.XtraPrinting.TextAlignment.TopCenter;
        this.xrTableCell10.Weight = 0.57291687011718739;
        this.xrTableCell10.BeforePrint += new System.Drawing.Printing.PrintEventHandler(this.xrTableCell10_BeforePrint);
        // 
        // xrTableCell9
        // 
        this.xrTableCell9.DataBindings.AddRange(new DevExpress.XtraReports.UI.XRBinding[] {
            new DevExpress.XtraReports.UI.XRBinding("Text", null, "sp_Att_AttendanceRegister_Report.EffectiveWork_Min")});
        this.xrTableCell9.Font = new System.Drawing.Font("Verdana", 8F);
        this.xrTableCell9.Name = "xrTableCell9";
        this.xrTableCell9.Text = "xrTableCell9";
        this.xrTableCell9.TextAlignment = DevExpress.XtraPrinting.TextAlignment.TopCenter;
        this.xrTableCell9.Weight = 0.5723724365234375;
        this.xrTableCell9.BeforePrint += new System.Drawing.Printing.PrintEventHandler(this.xrTableCell9_BeforePrint);
        // 
        // xrTableCell11
        // 
        this.xrTableCell11.DataBindings.AddRange(new DevExpress.XtraReports.UI.XRBinding[] {
            new DevExpress.XtraReports.UI.XRBinding("Text", null, "sp_Att_AttendanceRegister_Report.OverTime_Min")});
        this.xrTableCell11.Font = new System.Drawing.Font("Verdana", 8F);
        this.xrTableCell11.Name = "xrTableCell11";
        this.xrTableCell11.StylePriority.UseTextAlignment = false;
        this.xrTableCell11.Text = "xrTableCell11";
        this.xrTableCell11.TextAlignment = DevExpress.XtraPrinting.TextAlignment.TopCenter;
        this.xrTableCell11.Weight = 0.88281188964843693;
        this.xrTableCell11.BeforePrint += new System.Drawing.Printing.PrintEventHandler(this.xrTableCell11_BeforePrint);
        // 
        // xrTableCell12
        // 
        this.xrTableCell12.DataBindings.AddRange(new DevExpress.XtraReports.UI.XRBinding[] {
            new DevExpress.XtraReports.UI.XRBinding("Text", null, "sp_Att_AttendanceRegister_Report.Field1")});
        this.xrTableCell12.Font = new System.Drawing.Font("Verdana", 8F);
        this.xrTableCell12.Name = "xrTableCell12";
        this.xrTableCell12.StylePriority.UseTextAlignment = false;
        this.xrTableCell12.Text = "xrTableCell12";
        this.xrTableCell12.TextAlignment = DevExpress.XtraPrinting.TextAlignment.TopCenter;
        this.xrTableCell12.Weight = 0.794948733033147;
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
        this.xrTable1.LocationFloat = new DevExpress.Utils.PointFloat(36.45834F, 0F);
        this.xrTable1.Name = "xrTable1";
        this.xrTable1.Rows.AddRange(new DevExpress.XtraReports.UI.XRTableRow[] {
            this.xrTableRow1});
        this.xrTable1.SizeF = new System.Drawing.SizeF(846.5417F, 18.54165F);
        this.xrTable1.StylePriority.UseBorders = false;
        this.xrTable1.StylePriority.UseFont = false;
        // 
        // xrTableRow1
        // 
        this.xrTableRow1.Cells.AddRange(new DevExpress.XtraReports.UI.XRTableCell[] {
            this.xrTableCell21,
            this.xrTableCell7,
            this.xrTableCell6,
            this.xrTableCell2,
            this.xrTableCell4,
            this.xrTableCell1,
            this.xrTableCell22,
            this.xrTableCell24,
            this.xrTableCell25,
            this.xrTableCell23,
            this.xrTableCell26,
            this.xrTableCell3});
        this.xrTableRow1.Name = "xrTableRow1";
        this.xrTableRow1.Weight = 1;
        // 
        // xrTableCell21
        // 
        this.xrTableCell21.Name = "xrTableCell21";
        this.xrTableCell21.Text = "ID";
        this.xrTableCell21.Weight = 0.43482135264705046;
        // 
        // xrTableCell7
        // 
        this.xrTableCell7.Name = "xrTableCell7";
        this.xrTableCell7.Text = "Date";
        this.xrTableCell7.Weight = 0.84246639449167837;
        // 
        // xrTableCell6
        // 
        this.xrTableCell6.Name = "xrTableCell6";
        this.xrTableCell6.Text = "Shift";
        this.xrTableCell6.Weight = 0.88776053973238311;
        // 
        // xrTableCell2
        // 
        this.xrTableCell2.Name = "xrTableCell2";
        this.xrTableCell2.Text = "On Duty ";
        this.xrTableCell2.Weight = 0.55258535928846564;
        // 
        // xrTableCell4
        // 
        this.xrTableCell4.Name = "xrTableCell4";
        this.xrTableCell4.Text = "Off Duty ";
        this.xrTableCell4.Weight = 0.58662782725143692;
        // 
        // xrTableCell1
        // 
        this.xrTableCell1.Name = "xrTableCell1";
        this.xrTableCell1.Text = "In Time";
        this.xrTableCell1.Weight = 0.60338002583602923;
        // 
        // xrTableCell22
        // 
        this.xrTableCell22.Name = "xrTableCell22";
        this.xrTableCell22.Text = "Out Time";
        this.xrTableCell22.Weight = 0.54352680981884849;
        // 
        // xrTableCell24
        // 
        this.xrTableCell24.Name = "xrTableCell24";
        this.xrTableCell24.Text = "Late";
        this.xrTableCell24.Weight = 0.45567689520649723;
        // 
        // xrTableCell25
        // 
        this.xrTableCell25.Name = "xrTableCell25";
        this.xrTableCell25.Text = "Early";
        this.xrTableCell25.Weight = 0.49823307924366106;
        // 
        // xrTableCell23
        // 
        this.xrTableCell23.Name = "xrTableCell23";
        this.xrTableCell23.Text = "Work";
        this.xrTableCell23.Weight = 0.49775962447414274;
        // 
        // xrTableCell26
        // 
        this.xrTableCell26.Name = "xrTableCell26";
        this.xrTableCell26.Text = "Overtime";
        this.xrTableCell26.Weight = 0.76773174494671226;
        // 
        // xrTableCell3
        // 
        this.xrTableCell3.Name = "xrTableCell3";
        this.xrTableCell3.Text = "Status";
        this.xrTableCell3.Weight = 0.69132030305227632;
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
        this.xrPageInfo2.LocationFloat = new DevExpress.Utils.PointFloat(587.7919F, 0F);
        this.xrPageInfo2.Name = "xrPageInfo2";
        this.xrPageInfo2.Padding = new DevExpress.XtraPrinting.PaddingInfo(2, 2, 0, 0, 100F);
        this.xrPageInfo2.SizeF = new System.Drawing.SizeF(295.2082F, 23F);
        this.xrPageInfo2.StylePriority.UseTextAlignment = false;
        this.xrPageInfo2.TextAlignment = DevExpress.XtraPrinting.TextAlignment.TopRight;
        // 
        // xrPageInfo1
        // 
        this.xrPageInfo1.LocationFloat = new DevExpress.Utils.PointFloat(36.45825F, 0F);
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
        this.ReportHeader.HeightF = 77F;
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
        this.xrPanel1.LocationFloat = new DevExpress.Utils.PointFloat(36.45833F, 5F);
        this.xrPanel1.Name = "xrPanel1";
        this.xrPanel1.SizeF = new System.Drawing.SizeF(846.5417F, 72F);
        this.xrPanel1.StylePriority.UseBorders = false;
        // 
        // xrCompAddress
        // 
        this.xrCompAddress.Borders = DevExpress.XtraPrinting.BorderSide.None;
        this.xrCompAddress.Font = new System.Drawing.Font("Verdana", 9.75F, System.Drawing.FontStyle.Bold);
        this.xrCompAddress.LocationFloat = new DevExpress.Utils.PointFloat(4.041668F, 27F);
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
        this.xrTitle.LocationFloat = new DevExpress.Utils.PointFloat(208.7263F, 52F);
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
        this.xrPictureBox1.LocationFloat = new DevExpress.Utils.PointFloat(743.5417F, 2F);
        this.xrPictureBox1.Name = "xrPictureBox1";
        this.xrPictureBox1.SizeF = new System.Drawing.SizeF(100F, 47.04167F);
        this.xrPictureBox1.Sizing = DevExpress.XtraPrinting.ImageSizeMode.StretchImage;
        this.xrPictureBox1.StylePriority.UseBorders = false;
        // 
        // xrCompName
        // 
        this.xrCompName.Borders = DevExpress.XtraPrinting.BorderSide.None;
        this.xrCompName.Font = new System.Drawing.Font("Verdana", 9.75F, System.Drawing.FontStyle.Bold);
        this.xrCompName.LocationFloat = new DevExpress.Utils.PointFloat(4.041668F, 2F);
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
        this.GroupHeader2.HeightF = 15.625F;
        this.GroupHeader2.Level = 1;
        this.GroupHeader2.Name = "GroupHeader2";
        // 
        // xrTable3
        // 
        this.xrTable3.Borders = ((DevExpress.XtraPrinting.BorderSide)(((DevExpress.XtraPrinting.BorderSide.Left | DevExpress.XtraPrinting.BorderSide.Right)
                    | DevExpress.XtraPrinting.BorderSide.Bottom)));
        this.xrTable3.Font = new System.Drawing.Font("Verdana", 8F, System.Drawing.FontStyle.Bold);
        this.xrTable3.LocationFloat = new DevExpress.Utils.PointFloat(36.45825F, 0F);
        this.xrTable3.Name = "xrTable3";
        this.xrTable3.Rows.AddRange(new DevExpress.XtraReports.UI.XRTableRow[] {
            this.xrTableRow3});
        this.xrTable3.SizeF = new System.Drawing.SizeF(846.5418F, 14.99999F);
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
        this.xrTableCell16.Text = "[Emp_Code]";
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
        this.xrTableCell15.Borders = ((DevExpress.XtraPrinting.BorderSide)(((DevExpress.XtraPrinting.BorderSide.Left | DevExpress.XtraPrinting.BorderSide.Right)
                    | DevExpress.XtraPrinting.BorderSide.Bottom)));
        this.xrTableCell15.DataBindings.AddRange(new DevExpress.XtraReports.UI.XRBinding[] {
            new DevExpress.XtraReports.UI.XRBinding("Text", null, "sp_Att_AttendanceRegister_Report.Emp_Name")});
        this.xrTableCell15.Name = "xrTableCell15";
        this.xrTableCell15.StylePriority.UseBorders = false;
        this.xrTableCell15.Text = "xrTableCell15";
        this.xrTableCell15.Weight = 6.3447381591796876;
        // 
        // sp_Att_AttendanceRegister_ReportTableAdapter1
        // 
        this.sp_Att_AttendanceRegister_ReportTableAdapter1.ClearBeforeFill = true;
        // 
        // GroupFooter1
        // 
        this.GroupFooter1.Controls.AddRange(new DevExpress.XtraReports.UI.XRControl[] {
            this.xrTable5});
        this.GroupFooter1.HeightF = 20F;
        this.GroupFooter1.Name = "GroupFooter1";
        // 
        // xrTable5
        // 
        this.xrTable5.Borders = ((DevExpress.XtraPrinting.BorderSide)(((DevExpress.XtraPrinting.BorderSide.Left | DevExpress.XtraPrinting.BorderSide.Right)
                    | DevExpress.XtraPrinting.BorderSide.Bottom)));
        this.xrTable5.Font = new System.Drawing.Font("Verdana", 8F, System.Drawing.FontStyle.Bold);
        this.xrTable5.LocationFloat = new DevExpress.Utils.PointFloat(36.45831F, 0F);
        this.xrTable5.Name = "xrTable5";
        this.xrTable5.Rows.AddRange(new DevExpress.XtraReports.UI.XRTableRow[] {
            this.xrTableRow5});
        this.xrTable5.SizeF = new System.Drawing.SizeF(846.5417F, 20F);
        this.xrTable5.StylePriority.UseBorders = false;
        this.xrTable5.StylePriority.UseFont = false;
        // 
        // xrTableRow5
        // 
        this.xrTableRow5.Cells.AddRange(new DevExpress.XtraReports.UI.XRTableCell[] {
            this.xrTableCell36,
            this.xrTableCell32,
            this.xrTableCell33,
            this.xrTableCell34,
            this.xrTableCell35,
            this.xrTableCell37});
        this.xrTableRow5.Name = "xrTableRow5";
        this.xrTableRow5.Weight = 1;
        // 
        // xrTableCell36
        // 
        this.xrTableCell36.Name = "xrTableCell36";
        this.xrTableCell36.StylePriority.UseTextAlignment = false;
        this.xrTableCell36.Text = "Total ";
        this.xrTableCell36.TextAlignment = DevExpress.XtraPrinting.TextAlignment.TopCenter;
        this.xrTableCell36.Weight = 2.0018547344583935;
        // 
        // xrTableCell32
        // 
        this.xrTableCell32.DataBindings.AddRange(new DevExpress.XtraReports.UI.XRBinding[] {
            new DevExpress.XtraReports.UI.XRBinding("Text", null, "sp_Att_AttendanceRegister_Report.Emp_Id")});
        this.xrTableCell32.Name = "xrTableCell32";
        this.xrTableCell32.StylePriority.UseTextAlignment = false;
        this.xrTableCell32.Text = "xrTableCell32";
        this.xrTableCell32.TextAlignment = DevExpress.XtraPrinting.TextAlignment.TopCenter;
        this.xrTableCell32.Weight = 0.20493458428833339;
        this.xrTableCell32.BeforePrint += new System.Drawing.Printing.PrintEventHandler(this.xrTableCell32_BeforePrint);
        // 
        // xrTableCell33
        // 
        this.xrTableCell33.DataBindings.AddRange(new DevExpress.XtraReports.UI.XRBinding[] {
            new DevExpress.XtraReports.UI.XRBinding("Text", null, "sp_Att_AttendanceRegister_Report.Emp_Id")});
        this.xrTableCell33.Name = "xrTableCell33";
        this.xrTableCell33.StylePriority.UseTextAlignment = false;
        this.xrTableCell33.Text = "xrTableCell33";
        this.xrTableCell33.TextAlignment = DevExpress.XtraPrinting.TextAlignment.TopCenter;
        this.xrTableCell33.Weight = 0.22407351670249165;
        this.xrTableCell33.BeforePrint += new System.Drawing.Printing.PrintEventHandler(this.xrTableCell33_BeforePrint);
        // 
        // xrTableCell34
        // 
        this.xrTableCell34.DataBindings.AddRange(new DevExpress.XtraReports.UI.XRBinding[] {
            new DevExpress.XtraReports.UI.XRBinding("Text", null, "sp_Att_AttendanceRegister_Report.Emp_Id")});
        this.xrTableCell34.Name = "xrTableCell34";
        this.xrTableCell34.StylePriority.UseTextAlignment = false;
        this.xrTableCell34.Text = "xrTableCell34";
        this.xrTableCell34.TextAlignment = DevExpress.XtraPrinting.TextAlignment.TopCenter;
        this.xrTableCell34.Weight = 0.22386105546104113;
        this.xrTableCell34.BeforePrint += new System.Drawing.Printing.PrintEventHandler(this.xrTableCell34_BeforePrint);
        // 
        // xrTableCell35
        // 
        this.xrTableCell35.DataBindings.AddRange(new DevExpress.XtraReports.UI.XRBinding[] {
            new DevExpress.XtraReports.UI.XRBinding("Text", null, "sp_Att_AttendanceRegister_Report.Emp_Id")});
        this.xrTableCell35.Name = "xrTableCell35";
        this.xrTableCell35.StylePriority.UseTextAlignment = false;
        this.xrTableCell35.Text = "xrTableCell35";
        this.xrTableCell35.TextAlignment = DevExpress.XtraPrinting.TextAlignment.TopCenter;
        this.xrTableCell35.Weight = 0.345276499085476;
        this.xrTableCell35.BeforePrint += new System.Drawing.Printing.PrintEventHandler(this.xrTableCell35_BeforePrint);
        // 
        // xrTableCell37
        // 
        this.xrTableCell37.Name = "xrTableCell37";
        this.xrTableCell37.Weight = 0.3109128341579464;
        // 
        // Att_InOutReport
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
        this.Margins = new System.Drawing.Printing.Margins(7, 0, 0, 0);
        this.PageWidth = 900;
        this.PaperKind = System.Drawing.Printing.PaperKind.Custom;
        this.Version = "10.2";
        ((System.ComponentModel.ISupportInitialize)(this.xrTable2)).EndInit();
        ((System.ComponentModel.ISupportInitialize)(this.xrTable1)).EndInit();
        ((System.ComponentModel.ISupportInitialize)(this.attendanceDataSet1)).EndInit();
        ((System.ComponentModel.ISupportInitialize)(this.xrTable3)).EndInit();
        ((System.ComponentModel.ISupportInitialize)(this.xrTable5)).EndInit();
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
    private void xrTableCell8_BeforePrint(object sender, System.Drawing.Printing.PrintEventArgs e)
    {
        xrTableCell8.Text = GetHours(xrTableCell8.Text);
    }

    private void xrTableCell10_BeforePrint(object sender, System.Drawing.Printing.PrintEventArgs e)
    {
        xrTableCell10.Text = GetHours(xrTableCell10.Text);
    }

    private void xrTableCell9_BeforePrint(object sender, System.Drawing.Printing.PrintEventArgs e)
    {
        xrTableCell9.Text = GetHours(xrTableCell9.Text);
    }

    private void xrTableCell11_BeforePrint(object sender, System.Drawing.Printing.PrintEventArgs e)
    {
        xrTableCell11.Text = GetHours(xrTableCell11.Text);
    }

    private void xrTableCell32_BeforePrint(object sender, System.Drawing.Printing.PrintEventArgs e)
    {
        int sum = 0;
        DateTime fromdate = objsys.getDateForInput(System.Web.HttpContext.Current.Session["FromDate"].ToString());
        DateTime ToDate = objsys.getDateForInput(System.Web.HttpContext.Current.Session["ToDate"].ToString());


        DataTable dt = objAttReg.GetAttendanceRegDataByEmpId(xrTableCell32.Text, fromdate.ToString(), ToDate.ToString());
        dt = new DataView(dt,"LateMin<>0","",DataViewRowState.CurrentRows).ToTable();
        if(dt.Rows.Count > 0)
        {
            for (int i = 0; i < dt.Rows.Count;i++ )
            {

                sum += int.Parse(dt.Rows[i]["LateMin"].ToString());
            }

        }
        xrTableCell32.Text = GetHours(sum.ToString());

    }

    private void xrTableCell33_BeforePrint(object sender, System.Drawing.Printing.PrintEventArgs e)
    {
        int sum = 0;
        DateTime fromdate = objsys.getDateForInput(System.Web.HttpContext.Current.Session["FromDate"].ToString());
        DateTime ToDate = objsys.getDateForInput(System.Web.HttpContext.Current.Session["ToDate"].ToString());


        DataTable dt = objAttReg.GetAttendanceRegDataByEmpId(xrTableCell33.Text, fromdate.ToString(), ToDate.ToString());
        dt = new DataView(dt, "EarlyMin<>0", "", DataViewRowState.CurrentRows).ToTable();
        if (dt.Rows.Count > 0)
        {
            for (int i = 0; i < dt.Rows.Count; i++)
            {

                sum += int.Parse(dt.Rows[i]["EarlyMin"].ToString());
            }

        }
        xrTableCell33.Text = GetHours(sum.ToString());
    }

    private void xrTableCell34_BeforePrint(object sender, System.Drawing.Printing.PrintEventArgs e)
    {
        int sum = 0;
        DateTime fromdate = objsys.getDateForInput(System.Web.HttpContext.Current.Session["FromDate"].ToString());
        DateTime ToDate = objsys.getDateForInput(System.Web.HttpContext.Current.Session["ToDate"].ToString());


        DataTable dt = objAttReg.GetAttendanceRegDataByEmpId(xrTableCell34.Text, fromdate.ToString(), ToDate.ToString());
        dt = new DataView(dt, "EffectiveWork_Min<>0", "", DataViewRowState.CurrentRows).ToTable();
        if (dt.Rows.Count > 0)
        {
            for (int i = 0; i < dt.Rows.Count; i++)
            {

                sum += int.Parse(dt.Rows[i]["EffectiveWork_Min"].ToString());
            }

        }
        xrTableCell34.Text = GetHours(sum.ToString());

    }

    private void xrTableCell35_BeforePrint(object sender, System.Drawing.Printing.PrintEventArgs e)
    {
        int sum = 0;
        DateTime fromdate = objsys.getDateForInput(System.Web.HttpContext.Current.Session["FromDate"].ToString());
        DateTime ToDate = objsys.getDateForInput(System.Web.HttpContext.Current.Session["ToDate"].ToString());


        DataTable dt = objAttReg.GetAttendanceRegDataByEmpId(xrTableCell35.Text, fromdate.ToString(), ToDate.ToString());
        
        if (dt.Rows.Count > 0)
        {
            for (int i = 0; i < dt.Rows.Count; i++)
            {

                sum += int.Parse(dt.Rows[i]["OverTime_Min"].ToString()) + int.Parse(dt.Rows[i]["Week_Off_Min"].ToString()) + int.Parse(dt.Rows[i]["Holiday_Min"].ToString());
            }

        }
        xrTableCell35.Text = GetHours(sum.ToString());
    }

    private void xrTableCell30_BeforePrint(object sender, System.Drawing.Printing.PrintEventArgs e)
    {
        if (xrTableCell30.Text != "")
        {
            xrTableCell30.Text = Convert.ToDateTime(xrTableCell30.Text).ToString(objsys.SetDateFormat());

        }
    }


    

   

   

    
}
