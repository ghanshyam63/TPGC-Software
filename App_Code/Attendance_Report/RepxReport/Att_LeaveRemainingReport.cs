using System;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using DevExpress.XtraReports.UI;

/// <summary>
/// Summary description for Att_ShiftReport
/// </summary>
public class Att_LeaveRemainingReport : DevExpress.XtraReports.UI.XtraReport
{
    Set_Employee_Holiday objEmpHoli = new Set_Employee_Holiday();
    SystemParameter objSys = new SystemParameter();
	private DevExpress.XtraReports.UI.DetailBand Detail;
	private DevExpress.XtraReports.UI.TopMarginBand TopMargin;
	private DevExpress.XtraReports.UI.BottomMarginBand BottomMargin;
    private GroupHeaderBand GroupHeader1;
    private PageFooterBand PageFooter;
    private XRTable xrTable1;
    private XRTableRow xrTableRow1;
    private XRTableCell xrTableCell7;
    private AttendanceDataSet attendanceDataSet1;
    private AttendanceDataSetTableAdapters.sp_Att_ScheduleDescription_ReportTableAdapter sp_Att_ScheduleDescription_ReportTableAdapter1;
    private ReportHeaderBand ReportHeader;
    private GroupHeaderBand GroupHeader2;
    private XRPageInfo xrPageInfo2;
    private XRPageInfo xrPageInfo1;
    private AttendanceDataSetTableAdapters.sp_Set_Employee_Holiday_ReportTableAdapter sp_Set_Employee_Holiday_ReportTableAdapter1;
    private XRTableCell xrTableCell3;
    private XRTableCell xrTableCell4;
    private XRTable xrTable2;
    private XRTableRow xrTableRow2;
    private XRTableCell xrTableCell5;
    private XRTableCell xrTableCell9;
    private XRTableCell xrTableCell10;
    private AttendanceDataSetTableAdapters.sp_Set_Att_Employee_Leave_ReportTableAdapter sp_Set_Att_Employee_Leave_ReportTableAdapter1;
    private XRTableCell xrTableCell8;
    private XRTableCell xrTableCell6;
    private XRTableCell xrTableCell1;
    private XRTableCell xrTableCell2;
    private XRTableCell xrTableCell11;
    private XRTableCell xrTableCell12;
    private XRTableCell xrTableCell19;
    private XRTableCell xrTableCell20;
    private AttendanceDataSetTableAdapters.sp_Att_Employee_Leave_Trans_ReportTableAdapter sp_Att_Employee_Leave_Trans_ReportTableAdapter1;
    private XRControlStyle xrControlStyle1;
    private CalculatedField calculatedField1;
    private CalculatedField calculatedField2;
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
    private XRTableCell xrTableCell21;
    private XRTableCell xrTableCell22;
	/// <summary>
	/// Required designer variable.
	/// </summary>
	private System.ComponentModel.IContainer components = null;

    public Att_LeaveRemainingReport()
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
        string resourceFileName = "Att_LeaveRemainingReport.resx";
        this.Detail = new DevExpress.XtraReports.UI.DetailBand();
        this.xrTable2 = new DevExpress.XtraReports.UI.XRTable();
        this.xrTableRow2 = new DevExpress.XtraReports.UI.XRTableRow();
        this.xrTableCell5 = new DevExpress.XtraReports.UI.XRTableCell();
        this.xrTableCell6 = new DevExpress.XtraReports.UI.XRTableCell();
        this.xrTableCell8 = new DevExpress.XtraReports.UI.XRTableCell();
        this.xrTableCell9 = new DevExpress.XtraReports.UI.XRTableCell();
        this.xrTableCell10 = new DevExpress.XtraReports.UI.XRTableCell();
        this.xrTableCell19 = new DevExpress.XtraReports.UI.XRTableCell();
        this.xrTableCell20 = new DevExpress.XtraReports.UI.XRTableCell();
        this.TopMargin = new DevExpress.XtraReports.UI.TopMarginBand();
        this.BottomMargin = new DevExpress.XtraReports.UI.BottomMarginBand();
        this.GroupHeader1 = new DevExpress.XtraReports.UI.GroupHeaderBand();
        this.xrTable1 = new DevExpress.XtraReports.UI.XRTable();
        this.xrTableRow1 = new DevExpress.XtraReports.UI.XRTableRow();
        this.xrTableCell7 = new DevExpress.XtraReports.UI.XRTableCell();
        this.xrTableCell1 = new DevExpress.XtraReports.UI.XRTableCell();
        this.xrTableCell2 = new DevExpress.XtraReports.UI.XRTableCell();
        this.xrTableCell3 = new DevExpress.XtraReports.UI.XRTableCell();
        this.xrTableCell11 = new DevExpress.XtraReports.UI.XRTableCell();
        this.xrTableCell12 = new DevExpress.XtraReports.UI.XRTableCell();
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
        this.xrTableCell21 = new DevExpress.XtraReports.UI.XRTableCell();
        this.xrTableCell22 = new DevExpress.XtraReports.UI.XRTableCell();
        this.sp_Set_Employee_Holiday_ReportTableAdapter1 = new AttendanceDataSetTableAdapters.sp_Set_Employee_Holiday_ReportTableAdapter();
        this.sp_Set_Att_Employee_Leave_ReportTableAdapter1 = new AttendanceDataSetTableAdapters.sp_Set_Att_Employee_Leave_ReportTableAdapter();
        this.sp_Att_Employee_Leave_Trans_ReportTableAdapter1 = new AttendanceDataSetTableAdapters.sp_Att_Employee_Leave_Trans_ReportTableAdapter();
        this.xrControlStyle1 = new DevExpress.XtraReports.UI.XRControlStyle();
        this.calculatedField1 = new DevExpress.XtraReports.UI.CalculatedField();
        this.calculatedField2 = new DevExpress.XtraReports.UI.CalculatedField();
        ((System.ComponentModel.ISupportInitialize)(this.xrTable2)).BeginInit();
        ((System.ComponentModel.ISupportInitialize)(this.xrTable1)).BeginInit();
        ((System.ComponentModel.ISupportInitialize)(this.attendanceDataSet1)).BeginInit();
        ((System.ComponentModel.ISupportInitialize)(this.xrTable3)).BeginInit();
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
        this.xrTable2.Font = new System.Drawing.Font("Verdana", 8F);
        this.xrTable2.LocationFloat = new DevExpress.Utils.PointFloat(0F, 0F);
        this.xrTable2.Name = "xrTable2";
        this.xrTable2.Rows.AddRange(new DevExpress.XtraReports.UI.XRTableRow[] {
            this.xrTableRow2});
        this.xrTable2.SizeF = new System.Drawing.SizeF(712F, 14.99999F);
        this.xrTable2.StylePriority.UseBorders = false;
        this.xrTable2.StylePriority.UseFont = false;
        // 
        // xrTableRow2
        // 
        this.xrTableRow2.Cells.AddRange(new DevExpress.XtraReports.UI.XRTableCell[] {
            this.xrTableCell5,
            this.xrTableCell6,
            this.xrTableCell8,
            this.xrTableCell9,
            this.xrTableCell10,
            this.xrTableCell19,
            this.xrTableCell20});
        this.xrTableRow2.Name = "xrTableRow2";
        this.xrTableRow2.Weight = 1;
        // 
        // xrTableCell5
        // 
        this.xrTableCell5.DataBindings.AddRange(new DevExpress.XtraReports.UI.XRBinding[] {
            new DevExpress.XtraReports.UI.XRBinding("Text", null, "Att_Employee_Leave_Trans_Report.Leave_Name")});
        this.xrTableCell5.Name = "xrTableCell5";
        this.xrTableCell5.StylePriority.UseTextAlignment = false;
        this.xrTableCell5.Text = "Leave Name";
        this.xrTableCell5.TextAlignment = DevExpress.XtraPrinting.TextAlignment.TopLeft;
        this.xrTableCell5.Weight = 1.4312874415578869;
        // 
        // xrTableCell6
        // 
        this.xrTableCell6.DataBindings.AddRange(new DevExpress.XtraReports.UI.XRBinding[] {
            new DevExpress.XtraReports.UI.XRBinding("Text", null, "sp_Att_Employee_Leave_Trans_Report.Shedule_Type")});
        this.xrTableCell6.Name = "xrTableCell6";
        this.xrTableCell6.StylePriority.UseTextAlignment = false;
        this.xrTableCell6.Text = "Total Leave";
        this.xrTableCell6.TextAlignment = DevExpress.XtraPrinting.TextAlignment.TopCenter;
        this.xrTableCell6.Weight = 0.95604458552715421;
        // 
        // xrTableCell8
        // 
        this.xrTableCell8.DataBindings.AddRange(new DevExpress.XtraReports.UI.XRBinding[] {
            new DevExpress.XtraReports.UI.XRBinding("Text", null, "Att_Employee_Leave_Trans_Report.Month")});
        this.xrTableCell8.Name = "xrTableCell8";
        this.xrTableCell8.StylePriority.UseTextAlignment = false;
        this.xrTableCell8.Text = "Paid Leave";
        this.xrTableCell8.TextAlignment = DevExpress.XtraPrinting.TextAlignment.TopCenter;
        this.xrTableCell8.Weight = 0.61112460280127046;
        this.xrTableCell8.BeforePrint += new System.Drawing.Printing.PrintEventHandler(this.xrTableCell8_BeforePrint);
        // 
        // xrTableCell9
        // 
        this.xrTableCell9.DataBindings.AddRange(new DevExpress.XtraReports.UI.XRBinding[] {
            new DevExpress.XtraReports.UI.XRBinding("Text", null, "Att_Employee_Leave_Trans_Report.Year")});
        this.xrTableCell9.Name = "xrTableCell9";
        this.xrTableCell9.StylePriority.UseTextAlignment = false;
        this.xrTableCell9.Text = "Schedule Type";
        this.xrTableCell9.TextAlignment = DevExpress.XtraPrinting.TextAlignment.TopCenter;
        this.xrTableCell9.Weight = 0.4735676583462341;
        // 
        // xrTableCell10
        // 
        this.xrTableCell10.DataBindings.AddRange(new DevExpress.XtraReports.UI.XRBinding[] {
            new DevExpress.XtraReports.UI.XRBinding("Text", null, "Att_Employee_Leave_Trans_Report.Assign_Days")});
        this.xrTableCell10.Name = "xrTableCell10";
        this.xrTableCell10.StylePriority.UseTextAlignment = false;
        this.xrTableCell10.Text = "Is Year Carry";
        this.xrTableCell10.TextAlignment = DevExpress.XtraPrinting.TextAlignment.TopCenter;
        this.xrTableCell10.Weight = 0.85621222280662235;
        // 
        // xrTableCell19
        // 
        this.xrTableCell19.DataBindings.AddRange(new DevExpress.XtraReports.UI.XRBinding[] {
            new DevExpress.XtraReports.UI.XRBinding("Text", null, "sp_Att_Employee_Leave_Trans_Report.calculatedField1")});
        this.xrTableCell19.Name = "xrTableCell19";
        this.xrTableCell19.StylePriority.UseTextAlignment = false;
        this.xrTableCell19.Text = "xrTableCell19";
        this.xrTableCell19.TextAlignment = DevExpress.XtraPrinting.TextAlignment.TopCenter;
        this.xrTableCell19.Weight = 0.81463703895329687;
        // 
        // xrTableCell20
        // 
        this.xrTableCell20.DataBindings.AddRange(new DevExpress.XtraReports.UI.XRBinding[] {
            new DevExpress.XtraReports.UI.XRBinding("Text", null, "sp_Att_Employee_Leave_Trans_Report.calculatedField2")});
        this.xrTableCell20.Name = "xrTableCell20";
        this.xrTableCell20.StylePriority.UseTextAlignment = false;
        this.xrTableCell20.Text = "xrTableCell20";
        this.xrTableCell20.TextAlignment = DevExpress.XtraPrinting.TextAlignment.TopCenter;
        this.xrTableCell20.Weight = 1.0489849125898805;
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
        this.GroupHeader1.HeightF = 14.99999F;
        this.GroupHeader1.Name = "GroupHeader1";
        this.GroupHeader1.StylePriority.UseTextAlignment = false;
        this.GroupHeader1.TextAlignment = DevExpress.XtraPrinting.TextAlignment.TopLeft;
        // 
        // xrTable1
        // 
        this.xrTable1.Borders = ((DevExpress.XtraPrinting.BorderSide)(((DevExpress.XtraPrinting.BorderSide.Left | DevExpress.XtraPrinting.BorderSide.Right)
                    | DevExpress.XtraPrinting.BorderSide.Bottom)));
        this.xrTable1.Font = new System.Drawing.Font("Verdana", 8F, System.Drawing.FontStyle.Bold);
        this.xrTable1.LocationFloat = new DevExpress.Utils.PointFloat(0F, 0F);
        this.xrTable1.Name = "xrTable1";
        this.xrTable1.Rows.AddRange(new DevExpress.XtraReports.UI.XRTableRow[] {
            this.xrTableRow1});
        this.xrTable1.SizeF = new System.Drawing.SizeF(712F, 14.99999F);
        this.xrTable1.StylePriority.UseBorders = false;
        this.xrTable1.StylePriority.UseFont = false;
        // 
        // xrTableRow1
        // 
        this.xrTableRow1.Cells.AddRange(new DevExpress.XtraReports.UI.XRTableCell[] {
            this.xrTableCell7,
            this.xrTableCell1,
            this.xrTableCell2,
            this.xrTableCell3,
            this.xrTableCell11,
            this.xrTableCell12,
            this.xrTableCell4});
        this.xrTableRow1.Name = "xrTableRow1";
        this.xrTableRow1.Weight = 1;
        // 
        // xrTableCell7
        // 
        this.xrTableCell7.Name = "xrTableCell7";
        this.xrTableCell7.StylePriority.UseTextAlignment = false;
        this.xrTableCell7.Text = "Leave Name";
        this.xrTableCell7.TextAlignment = DevExpress.XtraPrinting.TextAlignment.TopLeft;
        this.xrTableCell7.Weight = 1.3669449759005865;
        // 
        // xrTableCell1
        // 
        this.xrTableCell1.Name = "xrTableCell1";
        this.xrTableCell1.Text = "Schedule Type";
        this.xrTableCell1.Weight = 0.91306646222635934;
        // 
        // xrTableCell2
        // 
        this.xrTableCell2.Name = "xrTableCell2";
        this.xrTableCell2.Text = "Month";
        this.xrTableCell2.Weight = 0.58365168371187348;
        // 
        // xrTableCell3
        // 
        this.xrTableCell3.Name = "xrTableCell3";
        this.xrTableCell3.Text = "Year";
        this.xrTableCell3.Weight = 0.452278572679668;
        // 
        // xrTableCell11
        // 
        this.xrTableCell11.Name = "xrTableCell11";
        this.xrTableCell11.Text = "Assign Leave";
        this.xrTableCell11.Weight = 0.81772210380704968;
        // 
        // xrTableCell12
        // 
        this.xrTableCell12.Name = "xrTableCell12";
        this.xrTableCell12.Text = "Used Leave";
        this.xrTableCell12.Weight = 0.77801579472853466;
        // 
        // xrTableCell4
        // 
        this.xrTableCell4.Name = "xrTableCell4";
        this.xrTableCell4.Text = "Remaining Leave";
        this.xrTableCell4.Weight = 1.0018283776456325;
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
        this.xrPageInfo2.LocationFloat = new DevExpress.Utils.PointFloat(399.247F, 0F);
        this.xrPageInfo2.Name = "xrPageInfo2";
        this.xrPageInfo2.Padding = new DevExpress.XtraPrinting.PaddingInfo(2, 2, 0, 0, 100F);
        this.xrPageInfo2.SizeF = new System.Drawing.SizeF(295.2082F, 23F);
        this.xrPageInfo2.StylePriority.UseTextAlignment = false;
        this.xrPageInfo2.TextAlignment = DevExpress.XtraPrinting.TextAlignment.TopRight;
        // 
        // xrPageInfo1
        // 
        this.xrPageInfo1.LocationFloat = new DevExpress.Utils.PointFloat(0F, 0F);
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
        this.ReportHeader.HeightF = 76.00001F;
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
        this.xrPanel1.LocationFloat = new DevExpress.Utils.PointFloat(0F, 1F);
        this.xrPanel1.Name = "xrPanel1";
        this.xrPanel1.SizeF = new System.Drawing.SizeF(712.0001F, 75.00001F);
        this.xrPanel1.StylePriority.UseBorders = false;
        // 
        // xrCompAddress
        // 
        this.xrCompAddress.Borders = DevExpress.XtraPrinting.BorderSide.None;
        this.xrCompAddress.Font = new System.Drawing.Font("Verdana", 9.75F, System.Drawing.FontStyle.Bold);
        this.xrCompAddress.LocationFloat = new DevExpress.Utils.PointFloat(2F, 27F);
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
        this.xrTitle.LocationFloat = new DevExpress.Utils.PointFloat(200.2917F, 52.04168F);
        this.xrTitle.Name = "xrTitle";
        this.xrTitle.Padding = new DevExpress.XtraPrinting.PaddingInfo(2, 2, 0, 0, 100F);
        this.xrTitle.SizeF = new System.Drawing.SizeF(356.8776F, 16F);
        this.xrTitle.StylePriority.UseBorders = false;
        this.xrTitle.StylePriority.UseFont = false;
        this.xrTitle.StylePriority.UseTextAlignment = false;
        this.xrTitle.Text = "xrTitle";
        this.xrTitle.TextAlignment = DevExpress.XtraPrinting.TextAlignment.TopCenter;
        // 
        // xrPictureBox1
        // 
        this.xrPictureBox1.Borders = DevExpress.XtraPrinting.BorderSide.None;
        this.xrPictureBox1.LocationFloat = new DevExpress.Utils.PointFloat(607.5F, 3F);
        this.xrPictureBox1.Name = "xrPictureBox1";
        this.xrPictureBox1.SizeF = new System.Drawing.SizeF(100F, 45.04167F);
        this.xrPictureBox1.Sizing = DevExpress.XtraPrinting.ImageSizeMode.StretchImage;
        this.xrPictureBox1.StylePriority.UseBorders = false;
        // 
        // xrCompName
        // 
        this.xrCompName.Borders = DevExpress.XtraPrinting.BorderSide.None;
        this.xrCompName.Font = new System.Drawing.Font("Verdana", 9.75F, System.Drawing.FontStyle.Bold);
        this.xrCompName.LocationFloat = new DevExpress.Utils.PointFloat(2F, 2F);
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
        this.xrTable3.LocationFloat = new DevExpress.Utils.PointFloat(0F, 0F);
        this.xrTable3.Name = "xrTable3";
        this.xrTable3.Rows.AddRange(new DevExpress.XtraReports.UI.XRTableRow[] {
            this.xrTableRow3});
        this.xrTable3.SizeF = new System.Drawing.SizeF(712.0834F, 14.99999F);
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
            this.xrTableCell15,
            this.xrTableCell21,
            this.xrTableCell22});
        this.xrTableRow3.Name = "xrTableRow3";
        this.xrTableRow3.Weight = 1;
        // 
        // xrTableCell13
        // 
        this.xrTableCell13.Name = "xrTableCell13";
        this.xrTableCell13.Text = "ID";
        this.xrTableCell13.Weight = 0.336781647507343;
        // 
        // xrTableCell14
        // 
        this.xrTableCell14.Font = new System.Drawing.Font("Times New Roman", 9.75F, System.Drawing.FontStyle.Bold);
        this.xrTableCell14.Name = "xrTableCell14";
        this.xrTableCell14.StylePriority.UseFont = false;
        this.xrTableCell14.StylePriority.UseTextAlignment = false;
        this.xrTableCell14.Text = ":";
        this.xrTableCell14.TextAlignment = DevExpress.XtraPrinting.TextAlignment.TopCenter;
        this.xrTableCell14.Weight = 0.18302226053248696;
        // 
        // xrTableCell16
        // 
        this.xrTableCell16.DataBindings.AddRange(new DevExpress.XtraReports.UI.XRBinding[] {
            new DevExpress.XtraReports.UI.XRBinding("Text", null, "Att_Employee_Leave_Trans_Report.Emp_Code")});
        this.xrTableCell16.Name = "xrTableCell16";
        this.xrTableCell16.Text = "xrTableCell16";
        this.xrTableCell16.Weight = 1.1172698197541959;
        // 
        // xrTableCell17
        // 
        this.xrTableCell17.Name = "xrTableCell17";
        this.xrTableCell17.Text = "Name";
        this.xrTableCell17.Weight = 0.55118778390858836;
        // 
        // xrTableCell18
        // 
        this.xrTableCell18.Font = new System.Drawing.Font("Times New Roman", 9.75F, System.Drawing.FontStyle.Bold);
        this.xrTableCell18.Name = "xrTableCell18";
        this.xrTableCell18.StylePriority.UseFont = false;
        this.xrTableCell18.StylePriority.UseTextAlignment = false;
        this.xrTableCell18.Text = ":";
        this.xrTableCell18.TextAlignment = DevExpress.XtraPrinting.TextAlignment.TopCenter;
        this.xrTableCell18.Weight = 0.18838647620110136;
        // 
        // xrTableCell15
        // 
        this.xrTableCell15.DataBindings.AddRange(new DevExpress.XtraReports.UI.XRBinding[] {
            new DevExpress.XtraReports.UI.XRBinding("Text", null, "Att_Employee_Leave_Trans_Report.Emp_Name")});
        this.xrTableCell15.Name = "xrTableCell15";
        this.xrTableCell15.Text = "xrTableCell15";
        this.xrTableCell15.Weight = 1.8506503231162608;
        // 
        // xrTableCell21
        // 
        this.xrTableCell21.Name = "xrTableCell21";
        this.xrTableCell21.Text = "Date Of Joining :";
        this.xrTableCell21.Weight = 1.1292532658857057;
        // 
        // xrTableCell22
        // 
        this.xrTableCell22.DataBindings.AddRange(new DevExpress.XtraReports.UI.XRBinding[] {
            new DevExpress.XtraReports.UI.XRBinding("Text", null, "sp_Att_Employee_Leave_Trans_Report.Field1")});
        this.xrTableCell22.Name = "xrTableCell22";
        this.xrTableCell22.Text = "xrTableCell22";
        this.xrTableCell22.Weight = 1.1521051326240552;
        this.xrTableCell22.BeforePrint += new System.Drawing.Printing.PrintEventHandler(this.xrTableCell22_BeforePrint);
        // 
        // sp_Set_Employee_Holiday_ReportTableAdapter1
        // 
        this.sp_Set_Employee_Holiday_ReportTableAdapter1.ClearBeforeFill = true;
        // 
        // sp_Set_Att_Employee_Leave_ReportTableAdapter1
        // 
        this.sp_Set_Att_Employee_Leave_ReportTableAdapter1.ClearBeforeFill = true;
        // 
        // sp_Att_Employee_Leave_Trans_ReportTableAdapter1
        // 
        this.sp_Att_Employee_Leave_Trans_ReportTableAdapter1.ClearBeforeFill = true;
        // 
        // xrControlStyle1
        // 
        this.xrControlStyle1.Name = "xrControlStyle1";
        this.xrControlStyle1.Padding = new DevExpress.XtraPrinting.PaddingInfo(5, 0, 0, 0, 100F);
        // 
        // calculatedField1
        // 
        this.calculatedField1.DataMember = "sp_Att_Employee_Leave_Trans_Report";
        this.calculatedField1.DataSource = this.attendanceDataSet1;
        this.calculatedField1.Expression = "[Used_Days] - [Pending_Days]";
        this.calculatedField1.Name = "calculatedField1";
        // 
        // calculatedField2
        // 
        this.calculatedField2.DataMember = "sp_Att_Employee_Leave_Trans_Report";
        this.calculatedField2.DataSource = this.attendanceDataSet1;
        this.calculatedField2.Expression = "[Remaining_Days] + [Pending_Days]";
        this.calculatedField2.Name = "calculatedField2";
        // 
        // Att_LeaveRemainingReport
        // 
        this.Bands.AddRange(new DevExpress.XtraReports.UI.Band[] {
            this.Detail,
            this.TopMargin,
            this.BottomMargin,
            this.GroupHeader1,
            this.PageFooter,
            this.ReportHeader,
            this.GroupHeader2});
        this.CalculatedFields.AddRange(new DevExpress.XtraReports.UI.CalculatedField[] {
            this.calculatedField1,
            this.calculatedField2});
        this.DataAdapter = this.sp_Att_Employee_Leave_Trans_ReportTableAdapter1;
        this.DataMember = "sp_Att_Employee_Leave_Trans_Report";
        this.DataSource = this.attendanceDataSet1;
        this.Font = new System.Drawing.Font("Times New Roman", 9.75F);
        this.Margins = new System.Drawing.Printing.Margins(66, 66, 0, 0);
        this.StyleSheet.AddRange(new DevExpress.XtraReports.UI.XRControlStyle[] {
            this.xrControlStyle1});
        this.Version = "10.2";
        ((System.ComponentModel.ISupportInitialize)(this.xrTable2)).EndInit();
        ((System.ComponentModel.ISupportInitialize)(this.xrTable1)).EndInit();
        ((System.ComponentModel.ISupportInitialize)(this.attendanceDataSet1)).EndInit();
        ((System.ComponentModel.ISupportInitialize)(this.xrTable3)).EndInit();
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

    private void xrTableCell8_BeforePrint(object sender, System.Drawing.Printing.PrintEventArgs e)
    {
        if (xrTableCell8.Text == "0")
        {
            xrTableCell8.Text = "-";

        }
        else
        {
            try
            {
                xrTableCell8.Text = new DateTime(DateTime.Now.Year, int.Parse(xrTableCell8.Text), DateTime.Now.Day).ToString("MMM");
            }
            catch
            {
            }

        }

    }

    private void xrTableCell22_BeforePrint(object sender, System.Drawing.Printing.PrintEventArgs e)
    {
        if(xrTableCell22.Text!="")
        {
            try
            {
                xrTableCell22.Text = Convert.ToDateTime(xrTableCell22.Text).ToString(objSys.SetDateFormat());

            }
            catch
            {
            }
        }
    }

   
}
