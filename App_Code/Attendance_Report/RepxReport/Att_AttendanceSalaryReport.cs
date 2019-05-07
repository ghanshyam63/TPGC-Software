using System;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using DevExpress.XtraReports.UI;
using System.Data;

/// <summary>
/// Summary description for EmpPayslipWithAtt
/// </summary>
public class Att_AttendanceSalaryReport : DevExpress.XtraReports.UI.XtraReport
{
    Att_AttendanceRegister objAttReg = new Att_AttendanceRegister();
   
    private DevExpress.XtraReports.UI.DetailBand Detail;
    private AttendanceDataSet reportData1;
   
    private XRControlStyle Title;
    private XRControlStyle FieldCaption;
    private XRControlStyle PageInfo;
    private XRControlStyle DataField;
    private PageFooterBand pageFooterBand1;
    private XRPageInfo xrPageInfo1;
    private XRPageInfo xrPageInfo2;
    private ReportHeaderBand reportHeaderBand1;
    private TopMarginBand topMarginBand1;
    private BottomMarginBand bottomMarginBand1;
    private XRTable xrTable1;
    private XRTableRow xrTableRow1;
    private XRTableCell xrTableCell1;
    private XRTableCell xrTableCell2;
    private XRTableCell xrTableCell4;
    private XRTableCell xrTableCell3;
    private XRTableRow xrTableRow7;
    private XRTableCell xrTableCell25;
    private XRTableRow xrTableRow9;
    private XRTableCell xrTableCell35;
    private XRTableRow xrTableRow11;
    private XRTableCell xrTableCell11;
  
    private PageHeaderBand PageHeader;
    private XRTableRow xrTableRow14;
    private XRTableCell xrTableCell6;
    private XRTableRow xrTableRow13;
    private XRTableCell xrTableCell60;
    private XRTableCell xrTableCell64;
    private XRTableRow xrTableRow8;
    private XRTableCell xrTableCell36;
    private XRTableRow xrTableRow12;
    private XRTableCell xrTableCell41;
    private XRTableCell xrTableCell19;
    private XRTableCell xrTableCell20;
    private XRTableCell xrTableCell24;
    private XRTableCell xrTableCell18;
    private XRTableCell xrTableCell43;
    private XRTableCell xrTableCell46;
    private XRTableCell xrTableCell49;
    private XRTableCell xrTableCell55;
    private XRTableCell xrTableCell56;
    private XRTableCell xrTableCell57;
    private XRTableCell xrTableCell59;
    private XRTableCell xrTableCell66;
    private XRTableCell xrTableCell44;
    private XRTableCell xrTableCell48;
    private XRTableRow xrTableRow6;
    private XRTableCell xrTableCell26;
    private XRTableCell xrTableCell34;
    private XRTableRow xrTableRow2;
    private XRTableCell xrTableCell5;
    private XRTableCell xrTableCell7;
    private XRTableCell xrTableCell8;
    private XRTableCell xrTableCell45;
    private XRTableRow xrTableRow21;
    private XRTableCell xrTableCell51;
    private XRTableCell xrTableCell53;
    private XRTableCell xrTableCell68;
    private XRTableRow xrTableRow22;
    private XRTableCell xrTableCell69;
    private XRTableCell xrTableCell71;
    private XRTableCell xrTableCell72;
    private XRTableRow xrTableRow4;
    private XRTableCell xrTableCell12;
    private XRTableCell xrTableCell15;
    private XRTableCell xrTableCell16;
    private XRTableCell xrTableCell17;
    private XRTableCell xrTableCell29;
    private XRTableCell xrTableCell37;
    private XRTableCell xrTableCell39;
    private XRTableCell xrTableCell47;
    private XRTableRow xrTableRow10;
    private XRTableCell xrTableCell50;
    private XRTableCell xrTableCell52;
    private XRTableCell xrTableCell58;
    private XRTableRow xrTableRow3;
    private XRTableCell xrTableCell9;
    private XRTableCell xrTableCell10;
    private XRTableCell xrTableCell27;
    private XRTableCell xrTableCell28;
    private XRTableRow xrTableRow5;
    private XRTableCell xrTableCell13;
    private XRTableCell xrTableCell14;
    private XRTableCell xrTableCell21;
    private XRTableCell xrTableCell22;
    private XRTableCell xrTableCell33;
    private XRTableCell xrTableCell23;
    private XRTableCell xrTableCell30;
    private XRTableCell xrTableCell31;
    private XRTableRow xrTableRow15;
    private XRTableCell xrTableCell32;
    private XRTableCell xrTableCell38;
    private XRTableCell xrTableCell40;
    private XRTableCell xrTableCell42;
    private XRTableRow xrTableRow16;
    private XRTableCell xrTableCell61;
    private XRTableCell xrTableCell62;
    private XRTableCell xrTableCell63;
    private XRTableCell xrTableCell65;
    private AttendanceDataSetTableAdapters.sp_Pay_Employee_Attendance_Select_RowTableAdapter sp_Pay_Employee_Attendance_Select_RowTableAdapter1;
    private XRTableCell xrTableCell54;
    private XRPanel xrPanel1;
    private XRLabel xrCompAddress;
    private XRLabel xrMonth;
    private XRLabel xrYear;
    private XRLabel xrCompName;
    private XRPictureBox xrPictureBox1;
    private XRLabel xrTitle;
	/// <summary>
	/// Required designer variable.
	/// </summary>
	private System.ComponentModel.IContainer components = null;

    public Att_AttendanceSalaryReport()
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
        string resourceFileName = "Att_AttendanceSalaryReport.resx";
        this.Detail = new DevExpress.XtraReports.UI.DetailBand();
        this.xrTable1 = new DevExpress.XtraReports.UI.XRTable();
        this.xrTableRow1 = new DevExpress.XtraReports.UI.XRTableRow();
        this.xrTableCell1 = new DevExpress.XtraReports.UI.XRTableCell();
        this.xrTableCell2 = new DevExpress.XtraReports.UI.XRTableCell();
        this.xrTableCell4 = new DevExpress.XtraReports.UI.XRTableCell();
        this.xrTableCell3 = new DevExpress.XtraReports.UI.XRTableCell();
        this.xrTableRow2 = new DevExpress.XtraReports.UI.XRTableRow();
        this.xrTableCell5 = new DevExpress.XtraReports.UI.XRTableCell();
        this.xrTableCell7 = new DevExpress.XtraReports.UI.XRTableCell();
        this.xrTableCell8 = new DevExpress.XtraReports.UI.XRTableCell();
        this.xrTableCell45 = new DevExpress.XtraReports.UI.XRTableCell();
        this.xrTableRow15 = new DevExpress.XtraReports.UI.XRTableRow();
        this.xrTableCell32 = new DevExpress.XtraReports.UI.XRTableCell();
        this.xrTableCell38 = new DevExpress.XtraReports.UI.XRTableCell();
        this.xrTableCell40 = new DevExpress.XtraReports.UI.XRTableCell();
        this.xrTableCell42 = new DevExpress.XtraReports.UI.XRTableCell();
        this.xrTableRow11 = new DevExpress.XtraReports.UI.XRTableRow();
        this.xrTableCell11 = new DevExpress.XtraReports.UI.XRTableCell();
        this.xrTableCell18 = new DevExpress.XtraReports.UI.XRTableCell();
        this.xrTableRow12 = new DevExpress.XtraReports.UI.XRTableRow();
        this.xrTableCell41 = new DevExpress.XtraReports.UI.XRTableCell();
        this.xrTableCell29 = new DevExpress.XtraReports.UI.XRTableCell();
        this.xrTableCell19 = new DevExpress.XtraReports.UI.XRTableCell();
        this.xrTableCell43 = new DevExpress.XtraReports.UI.XRTableCell();
        this.xrTableRow14 = new DevExpress.XtraReports.UI.XRTableRow();
        this.xrTableCell6 = new DevExpress.XtraReports.UI.XRTableCell();
        this.xrTableCell44 = new DevExpress.XtraReports.UI.XRTableCell();
        this.xrTableCell55 = new DevExpress.XtraReports.UI.XRTableCell();
        this.xrTableCell20 = new DevExpress.XtraReports.UI.XRTableCell();
        this.xrTableRow13 = new DevExpress.XtraReports.UI.XRTableRow();
        this.xrTableCell60 = new DevExpress.XtraReports.UI.XRTableCell();
        this.xrTableCell37 = new DevExpress.XtraReports.UI.XRTableCell();
        this.xrTableCell64 = new DevExpress.XtraReports.UI.XRTableCell();
        this.xrTableCell46 = new DevExpress.XtraReports.UI.XRTableCell();
        this.xrTableRow21 = new DevExpress.XtraReports.UI.XRTableRow();
        this.xrTableCell51 = new DevExpress.XtraReports.UI.XRTableCell();
        this.xrTableCell39 = new DevExpress.XtraReports.UI.XRTableCell();
        this.xrTableCell53 = new DevExpress.XtraReports.UI.XRTableCell();
        this.xrTableCell68 = new DevExpress.XtraReports.UI.XRTableCell();
        this.xrTableRow22 = new DevExpress.XtraReports.UI.XRTableRow();
        this.xrTableCell69 = new DevExpress.XtraReports.UI.XRTableCell();
        this.xrTableCell47 = new DevExpress.XtraReports.UI.XRTableCell();
        this.xrTableCell71 = new DevExpress.XtraReports.UI.XRTableCell();
        this.xrTableCell72 = new DevExpress.XtraReports.UI.XRTableCell();
        this.xrTableRow7 = new DevExpress.XtraReports.UI.XRTableRow();
        this.xrTableCell25 = new DevExpress.XtraReports.UI.XRTableCell();
        this.xrTableCell59 = new DevExpress.XtraReports.UI.XRTableCell();
        this.xrTableCell48 = new DevExpress.XtraReports.UI.XRTableCell();
        this.xrTableCell56 = new DevExpress.XtraReports.UI.XRTableCell();
        this.xrTableRow8 = new DevExpress.XtraReports.UI.XRTableRow();
        this.xrTableCell66 = new DevExpress.XtraReports.UI.XRTableCell();
        this.xrTableCell57 = new DevExpress.XtraReports.UI.XRTableCell();
        this.xrTableCell24 = new DevExpress.XtraReports.UI.XRTableCell();
        this.xrTableCell49 = new DevExpress.XtraReports.UI.XRTableCell();
        this.xrTableRow9 = new DevExpress.XtraReports.UI.XRTableRow();
        this.xrTableCell33 = new DevExpress.XtraReports.UI.XRTableCell();
        this.xrTableCell34 = new DevExpress.XtraReports.UI.XRTableCell();
        this.xrTableCell35 = new DevExpress.XtraReports.UI.XRTableCell();
        this.xrTableCell36 = new DevExpress.XtraReports.UI.XRTableCell();
        this.xrTableRow6 = new DevExpress.XtraReports.UI.XRTableRow();
        this.xrTableCell23 = new DevExpress.XtraReports.UI.XRTableCell();
        this.xrTableCell26 = new DevExpress.XtraReports.UI.XRTableCell();
        this.xrTableCell30 = new DevExpress.XtraReports.UI.XRTableCell();
        this.xrTableCell31 = new DevExpress.XtraReports.UI.XRTableCell();
        this.xrTableRow4 = new DevExpress.XtraReports.UI.XRTableRow();
        this.xrTableCell12 = new DevExpress.XtraReports.UI.XRTableCell();
        this.xrTableCell15 = new DevExpress.XtraReports.UI.XRTableCell();
        this.xrTableCell16 = new DevExpress.XtraReports.UI.XRTableCell();
        this.xrTableCell17 = new DevExpress.XtraReports.UI.XRTableCell();
        this.xrTableRow16 = new DevExpress.XtraReports.UI.XRTableRow();
        this.xrTableCell61 = new DevExpress.XtraReports.UI.XRTableCell();
        this.xrTableCell62 = new DevExpress.XtraReports.UI.XRTableCell();
        this.xrTableCell63 = new DevExpress.XtraReports.UI.XRTableCell();
        this.xrTableCell65 = new DevExpress.XtraReports.UI.XRTableCell();
        this.xrTableRow3 = new DevExpress.XtraReports.UI.XRTableRow();
        this.xrTableCell9 = new DevExpress.XtraReports.UI.XRTableCell();
        this.xrTableCell10 = new DevExpress.XtraReports.UI.XRTableCell();
        this.xrTableCell27 = new DevExpress.XtraReports.UI.XRTableCell();
        this.xrTableCell28 = new DevExpress.XtraReports.UI.XRTableCell();
        this.xrTableRow5 = new DevExpress.XtraReports.UI.XRTableRow();
        this.xrTableCell13 = new DevExpress.XtraReports.UI.XRTableCell();
        this.xrTableCell14 = new DevExpress.XtraReports.UI.XRTableCell();
        this.xrTableCell21 = new DevExpress.XtraReports.UI.XRTableCell();
        this.xrTableCell22 = new DevExpress.XtraReports.UI.XRTableCell();
        this.xrTableRow10 = new DevExpress.XtraReports.UI.XRTableRow();
        this.xrTableCell50 = new DevExpress.XtraReports.UI.XRTableCell();
        this.xrTableCell52 = new DevExpress.XtraReports.UI.XRTableCell();
        this.xrTableCell54 = new DevExpress.XtraReports.UI.XRTableCell();
        this.xrTableCell58 = new DevExpress.XtraReports.UI.XRTableCell();
        this.reportData1 = new AttendanceDataSet();
        this.Title = new DevExpress.XtraReports.UI.XRControlStyle();
        this.FieldCaption = new DevExpress.XtraReports.UI.XRControlStyle();
        this.PageInfo = new DevExpress.XtraReports.UI.XRControlStyle();
        this.DataField = new DevExpress.XtraReports.UI.XRControlStyle();
        this.pageFooterBand1 = new DevExpress.XtraReports.UI.PageFooterBand();
        this.xrPageInfo1 = new DevExpress.XtraReports.UI.XRPageInfo();
        this.xrPageInfo2 = new DevExpress.XtraReports.UI.XRPageInfo();
        this.reportHeaderBand1 = new DevExpress.XtraReports.UI.ReportHeaderBand();
        this.topMarginBand1 = new DevExpress.XtraReports.UI.TopMarginBand();
        this.bottomMarginBand1 = new DevExpress.XtraReports.UI.BottomMarginBand();
        this.PageHeader = new DevExpress.XtraReports.UI.PageHeaderBand();
        this.xrPanel1 = new DevExpress.XtraReports.UI.XRPanel();
        this.xrCompAddress = new DevExpress.XtraReports.UI.XRLabel();
        this.xrMonth = new DevExpress.XtraReports.UI.XRLabel();
        this.xrYear = new DevExpress.XtraReports.UI.XRLabel();
        this.xrCompName = new DevExpress.XtraReports.UI.XRLabel();
        this.xrPictureBox1 = new DevExpress.XtraReports.UI.XRPictureBox();
        this.xrTitle = new DevExpress.XtraReports.UI.XRLabel();
        this.sp_Pay_Employee_Attendance_Select_RowTableAdapter1 = new AttendanceDataSetTableAdapters.sp_Pay_Employee_Attendance_Select_RowTableAdapter();
        ((System.ComponentModel.ISupportInitialize)(this.xrTable1)).BeginInit();
        ((System.ComponentModel.ISupportInitialize)(this.reportData1)).BeginInit();
        ((System.ComponentModel.ISupportInitialize)(this)).BeginInit();
        // 
        // Detail
        // 
        this.Detail.Controls.AddRange(new DevExpress.XtraReports.UI.XRControl[] {
            this.xrTable1});
        this.Detail.HeightF = 609.3269F;
        this.Detail.Name = "Detail";
        this.Detail.Padding = new DevExpress.XtraPrinting.PaddingInfo(0, 0, 0, 0, 100F);
        this.Detail.TextAlignment = DevExpress.XtraPrinting.TextAlignment.TopLeft;
        this.Detail.BeforePrint += new System.Drawing.Printing.PrintEventHandler(this.Detail_BeforePrint_1);
        // 
        // xrTable1
        // 
        this.xrTable1.Borders = ((DevExpress.XtraPrinting.BorderSide)((((DevExpress.XtraPrinting.BorderSide.Left | DevExpress.XtraPrinting.BorderSide.Top)
                    | DevExpress.XtraPrinting.BorderSide.Right)
                    | DevExpress.XtraPrinting.BorderSide.Bottom)));
        this.xrTable1.Font = new System.Drawing.Font("Verdana", 8.25F);
        this.xrTable1.LocationFloat = new DevExpress.Utils.PointFloat(5.999994F, 0F);
        this.xrTable1.Name = "xrTable1";
        this.xrTable1.Padding = new DevExpress.XtraPrinting.PaddingInfo(5, 5, 5, 5, 100F);
        this.xrTable1.Rows.AddRange(new DevExpress.XtraReports.UI.XRTableRow[] {
            this.xrTableRow1,
            this.xrTableRow2,
            this.xrTableRow15,
            this.xrTableRow11,
            this.xrTableRow12,
            this.xrTableRow14,
            this.xrTableRow13,
            this.xrTableRow21,
            this.xrTableRow22,
            this.xrTableRow7,
            this.xrTableRow8,
            this.xrTableRow9,
            this.xrTableRow6,
            this.xrTableRow4,
            this.xrTableRow16,
            this.xrTableRow3,
            this.xrTableRow5,
            this.xrTableRow10});
        this.xrTable1.SizeF = new System.Drawing.SizeF(801.8333F, 608.6542F);
        this.xrTable1.StylePriority.UseBorders = false;
        this.xrTable1.StylePriority.UseFont = false;
        this.xrTable1.StylePriority.UsePadding = false;
        // 
        // xrTableRow1
        // 
        this.xrTableRow1.Cells.AddRange(new DevExpress.XtraReports.UI.XRTableCell[] {
            this.xrTableCell1,
            this.xrTableCell2,
            this.xrTableCell4,
            this.xrTableCell3});
        this.xrTableRow1.Name = "xrTableRow1";
        this.xrTableRow1.Weight = 1;
        // 
        // xrTableCell1
        // 
        this.xrTableCell1.Borders = ((DevExpress.XtraPrinting.BorderSide)((((DevExpress.XtraPrinting.BorderSide.Left | DevExpress.XtraPrinting.BorderSide.Top)
                    | DevExpress.XtraPrinting.BorderSide.Right)
                    | DevExpress.XtraPrinting.BorderSide.Bottom)));
        this.xrTableCell1.Font = new System.Drawing.Font("Verdana", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
        this.xrTableCell1.Name = "xrTableCell1";
        this.xrTableCell1.StylePriority.UseBorders = false;
        this.xrTableCell1.StylePriority.UseFont = false;
        this.xrTableCell1.Text = "ID";
        this.xrTableCell1.Weight = 1.1042819233971235;
        // 
        // xrTableCell2
        // 
        this.xrTableCell2.Borders = ((DevExpress.XtraPrinting.BorderSide)((((DevExpress.XtraPrinting.BorderSide.Left | DevExpress.XtraPrinting.BorderSide.Top)
                    | DevExpress.XtraPrinting.BorderSide.Right)
                    | DevExpress.XtraPrinting.BorderSide.Bottom)));
        this.xrTableCell2.DataBindings.AddRange(new DevExpress.XtraReports.UI.XRBinding[] {
            new DevExpress.XtraReports.UI.XRBinding("Text", null, "sp_Pay_Employee_Attendance_Select_Row.Emp_Code")});
        this.xrTableCell2.Font = new System.Drawing.Font("Verdana", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
        this.xrTableCell2.Name = "xrTableCell2";
        this.xrTableCell2.StylePriority.UseBorders = false;
        this.xrTableCell2.StylePriority.UseFont = false;
        this.xrTableCell2.Text = "xrTableCell2";
        this.xrTableCell2.Weight = 1.4570953587414408;
        // 
        // xrTableCell4
        // 
        this.xrTableCell4.Borders = ((DevExpress.XtraPrinting.BorderSide)((((DevExpress.XtraPrinting.BorderSide.Left | DevExpress.XtraPrinting.BorderSide.Top)
                    | DevExpress.XtraPrinting.BorderSide.Right)
                    | DevExpress.XtraPrinting.BorderSide.Bottom)));
        this.xrTableCell4.Font = new System.Drawing.Font("Verdana", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
        this.xrTableCell4.Name = "xrTableCell4";
        this.xrTableCell4.StylePriority.UseBorders = false;
        this.xrTableCell4.StylePriority.UseFont = false;
        this.xrTableCell4.Text = "Name";
        this.xrTableCell4.Weight = 1.6285158002342812;
        // 
        // xrTableCell3
        // 
        this.xrTableCell3.Borders = ((DevExpress.XtraPrinting.BorderSide)((((DevExpress.XtraPrinting.BorderSide.Left | DevExpress.XtraPrinting.BorderSide.Top)
                    | DevExpress.XtraPrinting.BorderSide.Right)
                    | DevExpress.XtraPrinting.BorderSide.Bottom)));
        this.xrTableCell3.DataBindings.AddRange(new DevExpress.XtraReports.UI.XRBinding[] {
            new DevExpress.XtraReports.UI.XRBinding("Text", null, "sp_Pay_Employee_Attendance_Select_Row.Emp_Name")});
        this.xrTableCell3.Font = new System.Drawing.Font("Verdana", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
        this.xrTableCell3.Name = "xrTableCell3";
        this.xrTableCell3.StylePriority.UseBorders = false;
        this.xrTableCell3.StylePriority.UseFont = false;
        this.xrTableCell3.Text = "xrTableCell3";
        this.xrTableCell3.Weight = 3.1501063072755908;
        // 
        // xrTableRow2
        // 
        this.xrTableRow2.Cells.AddRange(new DevExpress.XtraReports.UI.XRTableCell[] {
            this.xrTableCell5,
            this.xrTableCell7,
            this.xrTableCell8,
            this.xrTableCell45});
        this.xrTableRow2.Name = "xrTableRow2";
        this.xrTableRow2.Weight = 1;
        // 
        // xrTableCell5
        // 
        this.xrTableCell5.Name = "xrTableCell5";
        this.xrTableCell5.Text = "Designation";
        this.xrTableCell5.Weight = 1.1042819233971235;
        // 
        // xrTableCell7
        // 
        this.xrTableCell7.DataBindings.AddRange(new DevExpress.XtraReports.UI.XRBinding[] {
            new DevExpress.XtraReports.UI.XRBinding("Text", null, "sp_Pay_Employee_Attendance_Select_Row.Designation")});
        this.xrTableCell7.Name = "xrTableCell7";
        this.xrTableCell7.Text = "Designation";
        this.xrTableCell7.Weight = 1.4570953587414408;
        // 
        // xrTableCell8
        // 
        this.xrTableCell8.Name = "xrTableCell8";
        this.xrTableCell8.Text = "Department";
        this.xrTableCell8.Weight = 1.6285158002342812;
        // 
        // xrTableCell45
        // 
        this.xrTableCell45.DataBindings.AddRange(new DevExpress.XtraReports.UI.XRBinding[] {
            new DevExpress.XtraReports.UI.XRBinding("Text", null, "sp_Pay_Employee_Attendance_Select_Row.Dep_Name")});
        this.xrTableCell45.Name = "xrTableCell45";
        this.xrTableCell45.Text = "xrTableCell45";
        this.xrTableCell45.Weight = 3.1501063072755908;
        // 
        // xrTableRow15
        // 
        this.xrTableRow15.Cells.AddRange(new DevExpress.XtraReports.UI.XRTableCell[] {
            this.xrTableCell32,
            this.xrTableCell38,
            this.xrTableCell40,
            this.xrTableCell42});
        this.xrTableRow15.Name = "xrTableRow15";
        this.xrTableRow15.Weight = 1;
        // 
        // xrTableCell32
        // 
        this.xrTableCell32.Name = "xrTableCell32";
        this.xrTableCell32.Text = "Brand";
        this.xrTableCell32.Weight = 1.1042819233971235;
        // 
        // xrTableCell38
        // 
        this.xrTableCell38.DataBindings.AddRange(new DevExpress.XtraReports.UI.XRBinding[] {
            new DevExpress.XtraReports.UI.XRBinding("Text", null, "sp_Pay_Employee_Attendance_Select_Row.Brand_Name")});
        this.xrTableCell38.Name = "xrTableCell38";
        this.xrTableCell38.Text = "xrTableCell38";
        this.xrTableCell38.Weight = 1.4570953587414408;
        // 
        // xrTableCell40
        // 
        this.xrTableCell40.Name = "xrTableCell40";
        this.xrTableCell40.Text = "Location";
        this.xrTableCell40.Weight = 1.6285158002342812;
        // 
        // xrTableCell42
        // 
        this.xrTableCell42.DataBindings.AddRange(new DevExpress.XtraReports.UI.XRBinding[] {
            new DevExpress.XtraReports.UI.XRBinding("Text", null, "sp_Pay_Employee_Attendance_Select_Row.Location_Name")});
        this.xrTableCell42.Name = "xrTableCell42";
        this.xrTableCell42.Text = "xrTableCell42";
        this.xrTableCell42.Weight = 3.1501063072755908;
        // 
        // xrTableRow11
        // 
        this.xrTableRow11.Cells.AddRange(new DevExpress.XtraReports.UI.XRTableCell[] {
            this.xrTableCell11,
            this.xrTableCell18});
        this.xrTableRow11.Name = "xrTableRow11";
        this.xrTableRow11.Weight = 1;
        // 
        // xrTableCell11
        // 
        this.xrTableCell11.Borders = ((DevExpress.XtraPrinting.BorderSide)((((DevExpress.XtraPrinting.BorderSide.Left | DevExpress.XtraPrinting.BorderSide.Top)
                    | DevExpress.XtraPrinting.BorderSide.Right)
                    | DevExpress.XtraPrinting.BorderSide.Bottom)));
        this.xrTableCell11.Font = new System.Drawing.Font("Verdana", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
        this.xrTableCell11.Name = "xrTableCell11";
        this.xrTableCell11.StylePriority.UseBorders = false;
        this.xrTableCell11.StylePriority.UseFont = false;
        this.xrTableCell11.Text = "Employee Type";
        this.xrTableCell11.Weight = 1.104282370398983;
        // 
        // xrTableCell18
        // 
        this.xrTableCell18.Borders = ((DevExpress.XtraPrinting.BorderSide)((((DevExpress.XtraPrinting.BorderSide.Left | DevExpress.XtraPrinting.BorderSide.Top)
                    | DevExpress.XtraPrinting.BorderSide.Right)
                    | DevExpress.XtraPrinting.BorderSide.Bottom)));
        this.xrTableCell18.DataBindings.AddRange(new DevExpress.XtraReports.UI.XRBinding[] {
            new DevExpress.XtraReports.UI.XRBinding("Text", null, "sp_Pay_Employee_Attendance_Select_Row.Emp_Type")});
        this.xrTableCell18.Name = "xrTableCell18";
        this.xrTableCell18.StylePriority.UseBorders = false;
        this.xrTableCell18.Text = "xrTableCell18";
        this.xrTableCell18.Weight = 6.2357170192494547;
        // 
        // xrTableRow12
        // 
        this.xrTableRow12.Cells.AddRange(new DevExpress.XtraReports.UI.XRTableCell[] {
            this.xrTableCell41,
            this.xrTableCell29,
            this.xrTableCell19,
            this.xrTableCell43});
        this.xrTableRow12.Name = "xrTableRow12";
        this.xrTableRow12.Weight = 1;
        // 
        // xrTableCell41
        // 
        this.xrTableCell41.Borders = ((DevExpress.XtraPrinting.BorderSide)(((DevExpress.XtraPrinting.BorderSide.Left | DevExpress.XtraPrinting.BorderSide.Top)
                    | DevExpress.XtraPrinting.BorderSide.Bottom)));
        this.xrTableCell41.Font = new System.Drawing.Font("Verdana", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
        this.xrTableCell41.Name = "xrTableCell41";
        this.xrTableCell41.StylePriority.UseBorders = false;
        this.xrTableCell41.StylePriority.UseFont = false;
        this.xrTableCell41.Text = "Total Working Shift";
        this.xrTableCell41.Weight = 1.3433500755601178;
        // 
        // xrTableCell29
        // 
        this.xrTableCell29.DataBindings.AddRange(new DevExpress.XtraReports.UI.XRBinding[] {
            new DevExpress.XtraReports.UI.XRBinding("Text", null, "sp_Pay_Employee_Attendance_Select_Row.Total_Days")});
        this.xrTableCell29.Name = "xrTableCell29";
        this.xrTableCell29.Text = "xrTableCell29";
        this.xrTableCell29.Weight = 1.1630055470373462;
        // 
        // xrTableCell19
        // 
        this.xrTableCell19.Font = new System.Drawing.Font("Verdana", 8.25F);
        this.xrTableCell19.Name = "xrTableCell19";
        this.xrTableCell19.StylePriority.UseFont = false;
        this.xrTableCell19.Text = "Present Shift";
        this.xrTableCell19.Weight = 1.5065274122353061;
        // 
        // xrTableCell43
        // 
        this.xrTableCell43.DataBindings.AddRange(new DevExpress.XtraReports.UI.XRBinding[] {
            new DevExpress.XtraReports.UI.XRBinding("Text", null, "sp_Pay_Employee_Attendance_Select_Row.Worked_Days")});
        this.xrTableCell43.Font = new System.Drawing.Font("Verdana", 8.25F);
        this.xrTableCell43.Name = "xrTableCell43";
        this.xrTableCell43.StylePriority.UseFont = false;
        this.xrTableCell43.Text = "xrTableCell43";
        this.xrTableCell43.Weight = 1.8780283032233016;
        // 
        // xrTableRow14
        // 
        this.xrTableRow14.Cells.AddRange(new DevExpress.XtraReports.UI.XRTableCell[] {
            this.xrTableCell6,
            this.xrTableCell44,
            this.xrTableCell55,
            this.xrTableCell20});
        this.xrTableRow14.Name = "xrTableRow14";
        this.xrTableRow14.Weight = 1;
        // 
        // xrTableCell6
        // 
        this.xrTableCell6.Font = new System.Drawing.Font("Verdana", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
        this.xrTableCell6.Name = "xrTableCell6";
        this.xrTableCell6.StylePriority.UseFont = false;
        this.xrTableCell6.Text = "Absent";
        this.xrTableCell6.Weight = 1.6737970934779929;
        // 
        // xrTableCell44
        // 
        this.xrTableCell44.DataBindings.AddRange(new DevExpress.XtraReports.UI.XRBinding[] {
            new DevExpress.XtraReports.UI.XRBinding("Text", null, "sp_Pay_Employee_Attendance_Select_Row.Absent_Days")});
        this.xrTableCell44.Font = new System.Drawing.Font("Verdana", 8.25F);
        this.xrTableCell44.Name = "xrTableCell44";
        this.xrTableCell44.StylePriority.UseFont = false;
        this.xrTableCell44.Text = "xrTableCell44";
        this.xrTableCell44.Weight = 1.4490885906995557;
        // 
        // xrTableCell55
        // 
        this.xrTableCell55.Font = new System.Drawing.Font("Verdana", 8.25F);
        this.xrTableCell55.Name = "xrTableCell55";
        this.xrTableCell55.StylePriority.UseFont = false;
        this.xrTableCell55.Text = "Leave";
        this.xrTableCell55.Weight = 1.87711517069937;
        // 
        // xrTableCell20
        // 
        this.xrTableCell20.DataBindings.AddRange(new DevExpress.XtraReports.UI.XRBinding[] {
            new DevExpress.XtraReports.UI.XRBinding("Text", null, "sp_Pay_Employee_Attendance_Select_Row.Leave_Days")});
        this.xrTableCell20.Font = new System.Drawing.Font("Verdana", 8.25F);
        this.xrTableCell20.Name = "xrTableCell20";
        this.xrTableCell20.StylePriority.UseFont = false;
        this.xrTableCell20.Text = "xrTableCell20";
        this.xrTableCell20.Weight = 2.3399985347715182;
        // 
        // xrTableRow13
        // 
        this.xrTableRow13.Cells.AddRange(new DevExpress.XtraReports.UI.XRTableCell[] {
            this.xrTableCell60,
            this.xrTableCell37,
            this.xrTableCell64,
            this.xrTableCell46});
        this.xrTableRow13.Name = "xrTableRow13";
        this.xrTableRow13.Weight = 1;
        // 
        // xrTableCell60
        // 
        this.xrTableCell60.Borders = ((DevExpress.XtraPrinting.BorderSide)((((DevExpress.XtraPrinting.BorderSide.Left | DevExpress.XtraPrinting.BorderSide.Top)
                    | DevExpress.XtraPrinting.BorderSide.Right)
                    | DevExpress.XtraPrinting.BorderSide.Bottom)));
        this.xrTableCell60.Font = new System.Drawing.Font("Verdana", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
        this.xrTableCell60.Name = "xrTableCell60";
        this.xrTableCell60.StylePriority.UseBorders = false;
        this.xrTableCell60.StylePriority.UseFont = false;
        this.xrTableCell60.Text = "Holiday";
        this.xrTableCell60.Weight = 1.3433509718646219;
        // 
        // xrTableCell37
        // 
        this.xrTableCell37.DataBindings.AddRange(new DevExpress.XtraReports.UI.XRBinding[] {
            new DevExpress.XtraReports.UI.XRBinding("Text", null, "sp_Pay_Employee_Attendance_Select_Row.Holiday_Days")});
        this.xrTableCell37.Name = "xrTableCell37";
        this.xrTableCell37.Text = "xrTableCell37";
        this.xrTableCell37.Weight = 1.1630043753309787;
        // 
        // xrTableCell64
        // 
        this.xrTableCell64.Borders = ((DevExpress.XtraPrinting.BorderSide)((((DevExpress.XtraPrinting.BorderSide.Left | DevExpress.XtraPrinting.BorderSide.Top)
                    | DevExpress.XtraPrinting.BorderSide.Right)
                    | DevExpress.XtraPrinting.BorderSide.Bottom)));
        this.xrTableCell64.Font = new System.Drawing.Font("Verdana", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
        this.xrTableCell64.Name = "xrTableCell64";
        this.xrTableCell64.StylePriority.UseBorders = false;
        this.xrTableCell64.StylePriority.UseFont = false;
        this.xrTableCell64.Text = "WeekOff";
        this.xrTableCell64.Weight = 1.5065278958882362;
        // 
        // xrTableCell46
        // 
        this.xrTableCell46.DataBindings.AddRange(new DevExpress.XtraReports.UI.XRBinding[] {
            new DevExpress.XtraReports.UI.XRBinding("Text", null, "sp_Pay_Employee_Attendance_Select_Row.Week_Off_Days")});
        this.xrTableCell46.Font = new System.Drawing.Font("Verdana", 8.25F);
        this.xrTableCell46.Name = "xrTableCell46";
        this.xrTableCell46.StylePriority.UseFont = false;
        this.xrTableCell46.Text = "xrTableCell46";
        this.xrTableCell46.Weight = 1.8780279022366919;
        // 
        // xrTableRow21
        // 
        this.xrTableRow21.Cells.AddRange(new DevExpress.XtraReports.UI.XRTableCell[] {
            this.xrTableCell51,
            this.xrTableCell39,
            this.xrTableCell53,
            this.xrTableCell68});
        this.xrTableRow21.Name = "xrTableRow21";
        this.xrTableRow21.Weight = 1;
        // 
        // xrTableCell51
        // 
        this.xrTableCell51.Name = "xrTableCell51";
        this.xrTableCell51.Text = "Total Days";
        this.xrTableCell51.Weight = 1.3433510839679346;
        // 
        // xrTableCell39
        // 
        this.xrTableCell39.DataBindings.AddRange(new DevExpress.XtraReports.UI.XRBinding[] {
            new DevExpress.XtraReports.UI.XRBinding("Text", null, "sp_Pay_Employee_Attendance_Select_Row.Total_Days")});
        this.xrTableCell39.Name = "xrTableCell39";
        this.xrTableCell39.Text = "xrTableCell39";
        this.xrTableCell39.Weight = 1.1630046555892601;
        // 
        // xrTableCell53
        // 
        this.xrTableCell53.Name = "xrTableCell53";
        this.xrTableCell53.Text = "Paid Days";
        this.xrTableCell53.Weight = 1.5065276156299547;
        // 
        // xrTableCell68
        // 
        this.xrTableCell68.DataBindings.AddRange(new DevExpress.XtraReports.UI.XRBinding[] {
            new DevExpress.XtraReports.UI.XRBinding("Text", null, "sp_Pay_Employee_Attendance_Select_Row.Worked_Days")});
        this.xrTableCell68.Name = "xrTableCell68";
        this.xrTableCell68.Text = "xrTableCell68";
        this.xrTableCell68.Weight = 1.8780277901333793;
        // 
        // xrTableRow22
        // 
        this.xrTableRow22.Cells.AddRange(new DevExpress.XtraReports.UI.XRTableCell[] {
            this.xrTableCell69,
            this.xrTableCell47,
            this.xrTableCell71,
            this.xrTableCell72});
        this.xrTableRow22.Name = "xrTableRow22";
        this.xrTableRow22.Weight = 1;
        // 
        // xrTableCell69
        // 
        this.xrTableCell69.Name = "xrTableCell69";
        this.xrTableCell69.Text = "Total Assigned Hour";
        this.xrTableCell69.Weight = 1.343349906883152;
        // 
        // xrTableCell47
        // 
        this.xrTableCell47.DataBindings.AddRange(new DevExpress.XtraReports.UI.XRBinding[] {
            new DevExpress.XtraReports.UI.XRBinding("Text", null, "sp_Pay_Employee_Attendance_Select_Row.Assigned_Worked_Min")});
        this.xrTableCell47.Name = "xrTableCell47";
        this.xrTableCell47.Text = "xrTableCell47";
        this.xrTableCell47.Weight = 1.1630054963641046;
        this.xrTableCell47.BeforePrint += new System.Drawing.Printing.PrintEventHandler(this.xrTableCell47_BeforePrint);
        // 
        // xrTableCell71
        // 
        this.xrTableCell71.Name = "xrTableCell71";
        this.xrTableCell71.Text = "Worked Hour";
        this.xrTableCell71.Weight = 1.5065284003531434;
        // 
        // xrTableCell72
        // 
        this.xrTableCell72.DataBindings.AddRange(new DevExpress.XtraReports.UI.XRBinding[] {
            new DevExpress.XtraReports.UI.XRBinding("Text", null, "sp_Pay_Employee_Attendance_Select_Row.Total_Worked_Min")});
        this.xrTableCell72.Name = "xrTableCell72";
        this.xrTableCell72.Text = "xrTableCell72";
        this.xrTableCell72.Weight = 1.878027341720129;
        this.xrTableCell72.BeforePrint += new System.Drawing.Printing.PrintEventHandler(this.xrTableCell72_BeforePrint);
        // 
        // xrTableRow7
        // 
        this.xrTableRow7.Cells.AddRange(new DevExpress.XtraReports.UI.XRTableCell[] {
            this.xrTableCell25,
            this.xrTableCell59,
            this.xrTableCell48,
            this.xrTableCell56});
        this.xrTableRow7.Name = "xrTableRow7";
        this.xrTableRow7.Weight = 1;
        // 
        // xrTableCell25
        // 
        this.xrTableCell25.Borders = ((DevExpress.XtraPrinting.BorderSide)((((DevExpress.XtraPrinting.BorderSide.Left | DevExpress.XtraPrinting.BorderSide.Top)
                    | DevExpress.XtraPrinting.BorderSide.Right)
                    | DevExpress.XtraPrinting.BorderSide.Bottom)));
        this.xrTableCell25.Font = new System.Drawing.Font("Verdana", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
        this.xrTableCell25.Name = "xrTableCell25";
        this.xrTableCell25.StylePriority.UseBorders = false;
        this.xrTableCell25.StylePriority.UseFont = false;
        this.xrTableCell25.Text = "Late Count";
        this.xrTableCell25.Weight = 1.6737970934779929;
        // 
        // xrTableCell59
        // 
        this.xrTableCell59.DataBindings.AddRange(new DevExpress.XtraReports.UI.XRBinding[] {
            new DevExpress.XtraReports.UI.XRBinding("Text", null, "sp_Pay_Employee_Attendance_Select_Row.Emp_Id")});
        this.xrTableCell59.Font = new System.Drawing.Font("Verdana", 8.25F);
        this.xrTableCell59.Name = "xrTableCell59";
        this.xrTableCell59.StylePriority.UseFont = false;
        this.xrTableCell59.Text = "[LateCount]";
        this.xrTableCell59.Weight = 1.4490885233111572;
        this.xrTableCell59.BeforePrint += new System.Drawing.Printing.PrintEventHandler(this.xrTableCell59_BeforePrint);
        // 
        // xrTableCell48
        // 
        this.xrTableCell48.Font = new System.Drawing.Font("Verdana", 8.25F);
        this.xrTableCell48.Name = "xrTableCell48";
        this.xrTableCell48.StylePriority.UseFont = false;
        this.xrTableCell48.Text = "Late Hour";
        this.xrTableCell48.Weight = 1.8771157771949594;
        // 
        // xrTableCell56
        // 
        this.xrTableCell56.DataBindings.AddRange(new DevExpress.XtraReports.UI.XRBinding[] {
            new DevExpress.XtraReports.UI.XRBinding("Text", null, "sp_Pay_Employee_Attendance_Select_Row.Late_Min")});
        this.xrTableCell56.Font = new System.Drawing.Font("Verdana", 8.25F);
        this.xrTableCell56.Name = "xrTableCell56";
        this.xrTableCell56.StylePriority.UseFont = false;
        this.xrTableCell56.Text = "Late Hour";
        this.xrTableCell56.Weight = 2.3399979956643278;
        this.xrTableCell56.BeforePrint += new System.Drawing.Printing.PrintEventHandler(this.xrTableCell56_BeforePrint);
        // 
        // xrTableRow8
        // 
        this.xrTableRow8.Cells.AddRange(new DevExpress.XtraReports.UI.XRTableCell[] {
            this.xrTableCell66,
            this.xrTableCell57,
            this.xrTableCell24,
            this.xrTableCell49});
        this.xrTableRow8.Name = "xrTableRow8";
        this.xrTableRow8.Weight = 1;
        // 
        // xrTableCell66
        // 
        this.xrTableCell66.Font = new System.Drawing.Font("Verdana", 8.25F);
        this.xrTableCell66.Name = "xrTableCell66";
        this.xrTableCell66.StylePriority.UseFont = false;
        this.xrTableCell66.Text = "Early Leave Count";
        this.xrTableCell66.Weight = 1.6737976325851833;
        // 
        // xrTableCell57
        // 
        this.xrTableCell57.DataBindings.AddRange(new DevExpress.XtraReports.UI.XRBinding[] {
            new DevExpress.XtraReports.UI.XRBinding("Text", null, "sp_Pay_Employee_Attendance_Select_Row.Emp_Id")});
        this.xrTableCell57.Font = new System.Drawing.Font("Verdana", 8.25F);
        this.xrTableCell57.Name = "xrTableCell57";
        this.xrTableCell57.StylePriority.UseFont = false;
        this.xrTableCell57.Text = "xrTableCell57";
        this.xrTableCell57.Weight = 1.4490888602531511;
        this.xrTableCell57.BeforePrint += new System.Drawing.Printing.PrintEventHandler(this.xrTableCell57_BeforePrint);
        // 
        // xrTableCell24
        // 
        this.xrTableCell24.Font = new System.Drawing.Font("Verdana", 8.25F);
        this.xrTableCell24.Name = "xrTableCell24";
        this.xrTableCell24.StylePriority.UseFont = false;
        this.xrTableCell24.Text = "Early Leave Hour";
        this.xrTableCell24.Weight = 1.87711517069937;
        // 
        // xrTableCell49
        // 
        this.xrTableCell49.DataBindings.AddRange(new DevExpress.XtraReports.UI.XRBinding[] {
            new DevExpress.XtraReports.UI.XRBinding("Text", null, "sp_Pay_Employee_Attendance_Select_Row.Early_Min")});
        this.xrTableCell49.Font = new System.Drawing.Font("Verdana", 8.25F);
        this.xrTableCell49.Name = "xrTableCell49";
        this.xrTableCell49.StylePriority.UseFont = false;
        this.xrTableCell49.Text = "xrTableCell49";
        this.xrTableCell49.Weight = 2.3399977261107323;
        this.xrTableCell49.BeforePrint += new System.Drawing.Printing.PrintEventHandler(this.xrTableCell49_BeforePrint);
        // 
        // xrTableRow9
        // 
        this.xrTableRow9.Cells.AddRange(new DevExpress.XtraReports.UI.XRTableCell[] {
            this.xrTableCell33,
            this.xrTableCell34,
            this.xrTableCell35,
            this.xrTableCell36});
        this.xrTableRow9.Name = "xrTableRow9";
        this.xrTableRow9.Weight = 1;
        // 
        // xrTableCell33
        // 
        this.xrTableCell33.Borders = ((DevExpress.XtraPrinting.BorderSide)((((DevExpress.XtraPrinting.BorderSide.Left | DevExpress.XtraPrinting.BorderSide.Top)
                    | DevExpress.XtraPrinting.BorderSide.Right)
                    | DevExpress.XtraPrinting.BorderSide.Bottom)));
        this.xrTableCell33.Font = new System.Drawing.Font("Verdana", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
        this.xrTableCell33.Name = "xrTableCell33";
        this.xrTableCell33.StylePriority.UseBorders = false;
        this.xrTableCell33.StylePriority.UseFont = false;
        this.xrTableCell33.Text = "Normal OT Count";
        this.xrTableCell33.Weight = 1.6737969850819074;
        // 
        // xrTableCell34
        // 
        this.xrTableCell34.Borders = ((DevExpress.XtraPrinting.BorderSide)(((DevExpress.XtraPrinting.BorderSide.Top | DevExpress.XtraPrinting.BorderSide.Right)
                    | DevExpress.XtraPrinting.BorderSide.Bottom)));
        this.xrTableCell34.DataBindings.AddRange(new DevExpress.XtraReports.UI.XRBinding[] {
            new DevExpress.XtraReports.UI.XRBinding("Text", null, "sp_Pay_Employee_Attendance_Select_Row.Emp_Id")});
        this.xrTableCell34.Font = new System.Drawing.Font("Verdana", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
        this.xrTableCell34.Name = "xrTableCell34";
        this.xrTableCell34.StylePriority.UseBorders = false;
        this.xrTableCell34.StylePriority.UseFont = false;
        this.xrTableCell34.StylePriority.UseTextAlignment = false;
        this.xrTableCell34.Text = "xrTableCell34";
        this.xrTableCell34.TextAlignment = DevExpress.XtraPrinting.TextAlignment.TopLeft;
        this.xrTableCell34.Weight = 1.4490896717585249;
        this.xrTableCell34.BeforePrint += new System.Drawing.Printing.PrintEventHandler(this.xrTableCell34_BeforePrint);
        // 
        // xrTableCell35
        // 
        this.xrTableCell35.Borders = ((DevExpress.XtraPrinting.BorderSide)((((DevExpress.XtraPrinting.BorderSide.Left | DevExpress.XtraPrinting.BorderSide.Top)
                    | DevExpress.XtraPrinting.BorderSide.Right)
                    | DevExpress.XtraPrinting.BorderSide.Bottom)));
        this.xrTableCell35.Font = new System.Drawing.Font("Verdana", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
        this.xrTableCell35.Name = "xrTableCell35";
        this.xrTableCell35.StylePriority.UseBorders = false;
        this.xrTableCell35.StylePriority.UseFont = false;
        this.xrTableCell35.Text = "Normal OT Hour";
        this.xrTableCell35.Weight = 1.8771133431595668;
        // 
        // xrTableCell36
        // 
        this.xrTableCell36.Borders = ((DevExpress.XtraPrinting.BorderSide)((((DevExpress.XtraPrinting.BorderSide.Left | DevExpress.XtraPrinting.BorderSide.Top)
                    | DevExpress.XtraPrinting.BorderSide.Right)
                    | DevExpress.XtraPrinting.BorderSide.Bottom)));
        this.xrTableCell36.DataBindings.AddRange(new DevExpress.XtraReports.UI.XRBinding[] {
            new DevExpress.XtraReports.UI.XRBinding("Text", null, "sp_Pay_Employee_Attendance_Select_Row.Normal_OT_Min")});
        this.xrTableCell36.Name = "xrTableCell36";
        this.xrTableCell36.Weight = 0.712419873418503;
        this.xrTableCell36.BeforePrint += new System.Drawing.Printing.PrintEventHandler(this.xrTableCell36_BeforePrint);
        // 
        // xrTableRow6
        // 
        this.xrTableRow6.Cells.AddRange(new DevExpress.XtraReports.UI.XRTableCell[] {
            this.xrTableCell23,
            this.xrTableCell26,
            this.xrTableCell30,
            this.xrTableCell31});
        this.xrTableRow6.Name = "xrTableRow6";
        this.xrTableRow6.Weight = 1;
        // 
        // xrTableCell23
        // 
        this.xrTableCell23.Font = new System.Drawing.Font("Verdana", 8.25F);
        this.xrTableCell23.Name = "xrTableCell23";
        this.xrTableCell23.StylePriority.UseFont = false;
        this.xrTableCell23.Text = "Holiday OT Count";
        this.xrTableCell23.Weight = 1.6737969850819074;
        // 
        // xrTableCell26
        // 
        this.xrTableCell26.DataBindings.AddRange(new DevExpress.XtraReports.UI.XRBinding[] {
            new DevExpress.XtraReports.UI.XRBinding("Text", null, "sp_Pay_Employee_Attendance_Select_Row.Emp_Id")});
        this.xrTableCell26.Font = new System.Drawing.Font("Verdana", 8.25F);
        this.xrTableCell26.Name = "xrTableCell26";
        this.xrTableCell26.StylePriority.UseFont = false;
        this.xrTableCell26.Text = "xrTableCell26";
        this.xrTableCell26.Weight = 1.4490896717585249;
        this.xrTableCell26.BeforePrint += new System.Drawing.Printing.PrintEventHandler(this.xrTableCell26_BeforePrint);
        // 
        // xrTableCell30
        // 
        this.xrTableCell30.Font = new System.Drawing.Font("Verdana", 8.25F);
        this.xrTableCell30.Name = "xrTableCell30";
        this.xrTableCell30.StylePriority.UseFont = false;
        this.xrTableCell30.Text = "Hoiliday OT Hour";
        this.xrTableCell30.Weight = 1.8771133431595668;
        // 
        // xrTableCell31
        // 
        this.xrTableCell31.DataBindings.AddRange(new DevExpress.XtraReports.UI.XRBinding[] {
            new DevExpress.XtraReports.UI.XRBinding("Text", null, "sp_Pay_Employee_Attendance_Select_Row.Holiday_OT_Min")});
        this.xrTableCell31.Name = "xrTableCell31";
        this.xrTableCell31.Weight = 0.712419873418503;
        this.xrTableCell31.BeforePrint += new System.Drawing.Printing.PrintEventHandler(this.xrTableCell31_BeforePrint);
        // 
        // xrTableRow4
        // 
        this.xrTableRow4.Cells.AddRange(new DevExpress.XtraReports.UI.XRTableCell[] {
            this.xrTableCell12,
            this.xrTableCell15,
            this.xrTableCell16,
            this.xrTableCell17});
        this.xrTableRow4.Name = "xrTableRow4";
        this.xrTableRow4.Weight = 1;
        // 
        // xrTableCell12
        // 
        this.xrTableCell12.Name = "xrTableCell12";
        this.xrTableCell12.Text = "WeekOff OT Count";
        this.xrTableCell12.Weight = 1.6737969850819074;
        // 
        // xrTableCell15
        // 
        this.xrTableCell15.DataBindings.AddRange(new DevExpress.XtraReports.UI.XRBinding[] {
            new DevExpress.XtraReports.UI.XRBinding("Text", null, "sp_Pay_Employee_Attendance_Select_Row.Emp_Id")});
        this.xrTableCell15.Name = "xrTableCell15";
        this.xrTableCell15.Text = "xrTableCell15";
        this.xrTableCell15.Weight = 1.4490896717585249;
        this.xrTableCell15.BeforePrint += new System.Drawing.Printing.PrintEventHandler(this.xrTableCell15_BeforePrint);
        // 
        // xrTableCell16
        // 
        this.xrTableCell16.Name = "xrTableCell16";
        this.xrTableCell16.Text = "WeekOff OT Hour";
        this.xrTableCell16.Weight = 1.8771133431595668;
        // 
        // xrTableCell17
        // 
        this.xrTableCell17.DataBindings.AddRange(new DevExpress.XtraReports.UI.XRBinding[] {
            new DevExpress.XtraReports.UI.XRBinding("Text", null, "sp_Pay_Employee_Attendance_Select_Row.Week_Off_OT_Min")});
        this.xrTableCell17.Name = "xrTableCell17";
        this.xrTableCell17.Weight = 0.712419873418503;
        this.xrTableCell17.BeforePrint += new System.Drawing.Printing.PrintEventHandler(this.xrTableCell17_BeforePrint);
        // 
        // xrTableRow16
        // 
        this.xrTableRow16.Cells.AddRange(new DevExpress.XtraReports.UI.XRTableCell[] {
            this.xrTableCell61,
            this.xrTableCell62,
            this.xrTableCell63,
            this.xrTableCell65});
        this.xrTableRow16.Name = "xrTableRow16";
        this.xrTableRow16.Weight = 1;
        // 
        // xrTableCell61
        // 
        this.xrTableCell61.Name = "xrTableCell61";
        this.xrTableCell61.Text = "Basic Salary";
        this.xrTableCell61.Weight = 1.6737969850819074;
        // 
        // xrTableCell62
        // 
        this.xrTableCell62.DataBindings.AddRange(new DevExpress.XtraReports.UI.XRBinding[] {
            new DevExpress.XtraReports.UI.XRBinding("Text", null, "sp_Pay_Employee_Attendance_Select_Row.Basic_Salary")});
        this.xrTableCell62.Name = "xrTableCell62";
        this.xrTableCell62.Text = "[EmpId]";
        this.xrTableCell62.Weight = 1.4490896717585249;
        this.xrTableCell62.BeforePrint += new System.Drawing.Printing.PrintEventHandler(this.xrTableCell62_BeforePrint);
        // 
        // xrTableCell63
        // 
        this.xrTableCell63.Name = "xrTableCell63";
        this.xrTableCell63.Weight = 1.8771133431595668;
        // 
        // xrTableCell65
        // 
        this.xrTableCell65.Name = "xrTableCell65";
        this.xrTableCell65.Weight = 2.3399993896484372;
        // 
        // xrTableRow3
        // 
        this.xrTableRow3.Cells.AddRange(new DevExpress.XtraReports.UI.XRTableCell[] {
            this.xrTableCell9,
            this.xrTableCell10,
            this.xrTableCell27,
            this.xrTableCell28});
        this.xrTableRow3.Name = "xrTableRow3";
        this.xrTableRow3.Weight = 1;
        // 
        // xrTableCell9
        // 
        this.xrTableCell9.Name = "xrTableCell9";
        this.xrTableCell9.Text = "Worked Hour Salary";
        this.xrTableCell9.Weight = 1.6737969850819074;
        // 
        // xrTableCell10
        // 
        this.xrTableCell10.DataBindings.AddRange(new DevExpress.XtraReports.UI.XRBinding[] {
            new DevExpress.XtraReports.UI.XRBinding("Text", null, "sp_Pay_Employee_Attendance_Select_Row.Basic_Work_Salary")});
        this.xrTableCell10.Name = "xrTableCell10";
        this.xrTableCell10.Text = "xrTableCell10";
        this.xrTableCell10.Weight = 1.4490896717585249;
        // 
        // xrTableCell27
        // 
        this.xrTableCell27.Name = "xrTableCell27";
        this.xrTableCell27.Text = "OT Salary";
        this.xrTableCell27.Weight = 1.8771133431595668;
        // 
        // xrTableCell28
        // 
        this.xrTableCell28.DataBindings.AddRange(new DevExpress.XtraReports.UI.XRBinding[] {
            new DevExpress.XtraReports.UI.XRBinding("Text", null, "sp_Pay_Employee_Attendance_Select_Row.Normal_OT_Work_Salary")});
        this.xrTableCell28.Name = "xrTableCell28";
        this.xrTableCell28.Text = "xrTableCell28";
        this.xrTableCell28.Weight = 2.3399993896484372;
        // 
        // xrTableRow5
        // 
        this.xrTableRow5.Cells.AddRange(new DevExpress.XtraReports.UI.XRTableCell[] {
            this.xrTableCell13,
            this.xrTableCell14,
            this.xrTableCell21,
            this.xrTableCell22});
        this.xrTableRow5.Name = "xrTableRow5";
        this.xrTableRow5.Weight = 1;
        // 
        // xrTableCell13
        // 
        this.xrTableCell13.Name = "xrTableCell13";
        this.xrTableCell13.Text = "WeekOff OT Salary";
        this.xrTableCell13.Weight = 1.6737969850819074;
        // 
        // xrTableCell14
        // 
        this.xrTableCell14.DataBindings.AddRange(new DevExpress.XtraReports.UI.XRBinding[] {
            new DevExpress.XtraReports.UI.XRBinding("Text", null, "sp_Pay_Employee_Attendance_Select_Row.WeekOff_OT_Work_Salary")});
        this.xrTableCell14.Name = "xrTableCell14";
        this.xrTableCell14.Text = "xrTableCell14";
        this.xrTableCell14.Weight = 1.4490896717585249;
        // 
        // xrTableCell21
        // 
        this.xrTableCell21.Name = "xrTableCell21";
        this.xrTableCell21.Text = "Holiday OT Salary";
        this.xrTableCell21.Weight = 1.8771133431595668;
        // 
        // xrTableCell22
        // 
        this.xrTableCell22.DataBindings.AddRange(new DevExpress.XtraReports.UI.XRBinding[] {
            new DevExpress.XtraReports.UI.XRBinding("Text", null, "sp_Pay_Employee_Attendance_Select_Row.Holiday_OT_Work_Salary")});
        this.xrTableCell22.Name = "xrTableCell22";
        this.xrTableCell22.Text = "xrTableCell22";
        this.xrTableCell22.Weight = 2.3399993896484372;
        // 
        // xrTableRow10
        // 
        this.xrTableRow10.Cells.AddRange(new DevExpress.XtraReports.UI.XRTableCell[] {
            this.xrTableCell50,
            this.xrTableCell52,
            this.xrTableCell54,
            this.xrTableCell58});
        this.xrTableRow10.Name = "xrTableRow10";
        this.xrTableRow10.Weight = 1;
        // 
        // xrTableCell50
        // 
        this.xrTableCell50.Name = "xrTableCell50";
        this.xrTableCell50.Weight = 1.6737969850819074;
        // 
        // xrTableCell52
        // 
        this.xrTableCell52.Font = new System.Drawing.Font("Verdana", 8.25F, System.Drawing.FontStyle.Bold);
        this.xrTableCell52.Name = "xrTableCell52";
        this.xrTableCell52.StylePriority.UseFont = false;
        this.xrTableCell52.Text = "Gross Total";
        this.xrTableCell52.Weight = 1.4490896717585249;
        // 
        // xrTableCell54
        // 
        this.xrTableCell54.DataBindings.AddRange(new DevExpress.XtraReports.UI.XRBinding[] {
            new DevExpress.XtraReports.UI.XRBinding("Text", null, "sp_Pay_Employee_Attendance_Select_Row.Field4")});
        this.xrTableCell54.Font = new System.Drawing.Font("Verdana", 8.25F, System.Drawing.FontStyle.Bold);
        this.xrTableCell54.Name = "xrTableCell54";
        this.xrTableCell54.StylePriority.UseFont = false;
        this.xrTableCell54.Text = "0";
        this.xrTableCell54.Weight = 1.8771133431595668;
        this.xrTableCell54.BeforePrint += new System.Drawing.Printing.PrintEventHandler(this.xrTableCell54_BeforePrint);
        // 
        // xrTableCell58
        // 
        this.xrTableCell58.Name = "xrTableCell58";
        this.xrTableCell58.Weight = 2.3399993896484372;
        // 
        // reportData1
        // 
        this.reportData1.DataSetName = "ReportData";
        this.reportData1.SchemaSerializationMode = System.Data.SchemaSerializationMode.IncludeSchema;
        // 
        // Title
        // 
        this.Title.BackColor = System.Drawing.Color.White;
        this.Title.BorderColor = System.Drawing.SystemColors.ControlText;
        this.Title.Borders = DevExpress.XtraPrinting.BorderSide.None;
        this.Title.BorderWidth = 1;
        this.Title.Font = new System.Drawing.Font("Times New Roman", 24F);
        this.Title.ForeColor = System.Drawing.Color.Black;
        this.Title.Name = "Title";
        // 
        // FieldCaption
        // 
        this.FieldCaption.BackColor = System.Drawing.Color.White;
        this.FieldCaption.BorderColor = System.Drawing.SystemColors.ControlText;
        this.FieldCaption.Borders = DevExpress.XtraPrinting.BorderSide.None;
        this.FieldCaption.BorderWidth = 1;
        this.FieldCaption.Font = new System.Drawing.Font("Times New Roman", 10F, System.Drawing.FontStyle.Bold);
        this.FieldCaption.ForeColor = System.Drawing.Color.Black;
        this.FieldCaption.Name = "FieldCaption";
        // 
        // PageInfo
        // 
        this.PageInfo.BackColor = System.Drawing.Color.White;
        this.PageInfo.BorderColor = System.Drawing.SystemColors.ControlText;
        this.PageInfo.Borders = DevExpress.XtraPrinting.BorderSide.None;
        this.PageInfo.BorderWidth = 1;
        this.PageInfo.Font = new System.Drawing.Font("Times New Roman", 8F);
        this.PageInfo.ForeColor = System.Drawing.Color.Black;
        this.PageInfo.Name = "PageInfo";
        // 
        // DataField
        // 
        this.DataField.BackColor = System.Drawing.Color.White;
        this.DataField.BorderColor = System.Drawing.SystemColors.ControlText;
        this.DataField.Borders = DevExpress.XtraPrinting.BorderSide.None;
        this.DataField.BorderWidth = 1;
        this.DataField.Font = new System.Drawing.Font("Times New Roman", 8F);
        this.DataField.ForeColor = System.Drawing.SystemColors.ControlText;
        this.DataField.Name = "DataField";
        this.DataField.Padding = new DevExpress.XtraPrinting.PaddingInfo(2, 2, 0, 0, 100F);
        // 
        // pageFooterBand1
        // 
        this.pageFooterBand1.Controls.AddRange(new DevExpress.XtraReports.UI.XRControl[] {
            this.xrPageInfo1,
            this.xrPageInfo2});
        this.pageFooterBand1.HeightF = 24.8333F;
        this.pageFooterBand1.Name = "pageFooterBand1";
        // 
        // xrPageInfo1
        // 
        this.xrPageInfo1.LocationFloat = new DevExpress.Utils.PointFloat(6F, 0F);
        this.xrPageInfo1.Name = "xrPageInfo1";
        this.xrPageInfo1.Padding = new DevExpress.XtraPrinting.PaddingInfo(2, 2, 0, 0, 100F);
        this.xrPageInfo1.PageInfo = DevExpress.XtraPrinting.PageInfo.DateTime;
        this.xrPageInfo1.SizeF = new System.Drawing.SizeF(313F, 23F);
        this.xrPageInfo1.StyleName = "PageInfo";
        // 
        // xrPageInfo2
        // 
        this.xrPageInfo2.Format = "Page {0}";
        this.xrPageInfo2.LocationFloat = new DevExpress.Utils.PointFloat(494.8333F, 0F);
        this.xrPageInfo2.Name = "xrPageInfo2";
        this.xrPageInfo2.Padding = new DevExpress.XtraPrinting.PaddingInfo(2, 2, 0, 0, 100F);
        this.xrPageInfo2.SizeF = new System.Drawing.SizeF(313F, 23F);
        this.xrPageInfo2.StyleName = "PageInfo";
        this.xrPageInfo2.TextAlignment = DevExpress.XtraPrinting.TextAlignment.TopRight;
        // 
        // reportHeaderBand1
        // 
        this.reportHeaderBand1.HeightF = 0F;
        this.reportHeaderBand1.Name = "reportHeaderBand1";
        // 
        // topMarginBand1
        // 
        this.topMarginBand1.HeightF = 0F;
        this.topMarginBand1.Name = "topMarginBand1";
        // 
        // bottomMarginBand1
        // 
        this.bottomMarginBand1.HeightF = 1F;
        this.bottomMarginBand1.Name = "bottomMarginBand1";
        // 
        // PageHeader
        // 
        this.PageHeader.Controls.AddRange(new DevExpress.XtraReports.UI.XRControl[] {
            this.xrPanel1});
        this.PageHeader.HeightF = 74.99997F;
        this.PageHeader.Name = "PageHeader";
        // 
        // xrPanel1
        // 
        this.xrPanel1.Borders = ((DevExpress.XtraPrinting.BorderSide)((((DevExpress.XtraPrinting.BorderSide.Left | DevExpress.XtraPrinting.BorderSide.Top)
                    | DevExpress.XtraPrinting.BorderSide.Right)
                    | DevExpress.XtraPrinting.BorderSide.Bottom)));
        this.xrPanel1.Controls.AddRange(new DevExpress.XtraReports.UI.XRControl[] {
            this.xrCompAddress,
            this.xrMonth,
            this.xrYear,
            this.xrCompName,
            this.xrPictureBox1,
            this.xrTitle});
        this.xrPanel1.LocationFloat = new DevExpress.Utils.PointFloat(6.000002F, 3.999996F);
        this.xrPanel1.Name = "xrPanel1";
        this.xrPanel1.SizeF = new System.Drawing.SizeF(801.8333F, 70.99998F);
        this.xrPanel1.StylePriority.UseBorders = false;
        // 
        // xrCompAddress
        // 
        this.xrCompAddress.Borders = DevExpress.XtraPrinting.BorderSide.None;
        this.xrCompAddress.Font = new System.Drawing.Font("Verdana", 9.75F, System.Drawing.FontStyle.Bold);
        this.xrCompAddress.LocationFloat = new DevExpress.Utils.PointFloat(6.499998F, 27F);
        this.xrCompAddress.Name = "xrCompAddress";
        this.xrCompAddress.Padding = new DevExpress.XtraPrinting.PaddingInfo(2, 2, 0, 0, 100F);
        this.xrCompAddress.SizeF = new System.Drawing.SizeF(353.125F, 22.04167F);
        this.xrCompAddress.StylePriority.UseBorders = false;
        this.xrCompAddress.StylePriority.UseFont = false;
        this.xrCompAddress.Text = "xrCompAddress";
        // 
        // xrMonth
        // 
        this.xrMonth.Borders = DevExpress.XtraPrinting.BorderSide.None;
        this.xrMonth.LocationFloat = new DevExpress.Utils.PointFloat(406.5F, 14.5F);
        this.xrMonth.Name = "xrMonth";
        this.xrMonth.Padding = new DevExpress.XtraPrinting.PaddingInfo(2, 2, 0, 0, 100F);
        this.xrMonth.SizeF = new System.Drawing.SizeF(100F, 23F);
        this.xrMonth.StylePriority.UseBorders = false;
        this.xrMonth.Text = "xrMonth";
        this.xrMonth.Visible = false;
        // 
        // xrYear
        // 
        this.xrYear.Borders = DevExpress.XtraPrinting.BorderSide.None;
        this.xrYear.LocationFloat = new DevExpress.Utils.PointFloat(506.5F, 14.5F);
        this.xrYear.Name = "xrYear";
        this.xrYear.Padding = new DevExpress.XtraPrinting.PaddingInfo(2, 2, 0, 0, 100F);
        this.xrYear.SizeF = new System.Drawing.SizeF(100F, 23F);
        this.xrYear.StylePriority.UseBorders = false;
        this.xrYear.Text = "xrYear";
        this.xrYear.Visible = false;
        // 
        // xrCompName
        // 
        this.xrCompName.Borders = DevExpress.XtraPrinting.BorderSide.None;
        this.xrCompName.Font = new System.Drawing.Font("Verdana", 9.75F, System.Drawing.FontStyle.Bold);
        this.xrCompName.LocationFloat = new DevExpress.Utils.PointFloat(6.499998F, 2F);
        this.xrCompName.Name = "xrCompName";
        this.xrCompName.Padding = new DevExpress.XtraPrinting.PaddingInfo(2, 2, 0, 0, 100F);
        this.xrCompName.SizeF = new System.Drawing.SizeF(353.125F, 23F);
        this.xrCompName.StylePriority.UseBorders = false;
        this.xrCompName.StylePriority.UseFont = false;
        this.xrCompName.Text = "xrCompName";
        // 
        // xrPictureBox1
        // 
        this.xrPictureBox1.Borders = DevExpress.XtraPrinting.BorderSide.None;
        this.xrPictureBox1.LocationFloat = new DevExpress.Utils.PointFloat(701.8333F, 1.999998F);
        this.xrPictureBox1.Name = "xrPictureBox1";
        this.xrPictureBox1.SizeF = new System.Drawing.SizeF(100F, 45.04167F);
        this.xrPictureBox1.Sizing = DevExpress.XtraPrinting.ImageSizeMode.StretchImage;
        this.xrPictureBox1.StylePriority.UseBorders = false;
        // 
        // xrTitle
        // 
        this.xrTitle.Borders = DevExpress.XtraPrinting.BorderSide.None;
        this.xrTitle.Font = new System.Drawing.Font("Verdana", 9.75F, System.Drawing.FontStyle.Bold);
        this.xrTitle.LocationFloat = new DevExpress.Utils.PointFloat(131.5F, 52F);
        this.xrTitle.Name = "xrTitle";
        this.xrTitle.Padding = new DevExpress.XtraPrinting.PaddingInfo(2, 2, 0, 0, 100F);
        this.xrTitle.SizeF = new System.Drawing.SizeF(570.3333F, 16F);
        this.xrTitle.StylePriority.UseBorders = false;
        this.xrTitle.StylePriority.UseFont = false;
        this.xrTitle.StylePriority.UseTextAlignment = false;
        this.xrTitle.Text = "xrTitle";
        this.xrTitle.TextAlignment = DevExpress.XtraPrinting.TextAlignment.TopCenter;
        // 
        // sp_Pay_Employee_Attendance_Select_RowTableAdapter1
        // 
        this.sp_Pay_Employee_Attendance_Select_RowTableAdapter1.ClearBeforeFill = true;
        // 
        // Att_AttendanceSalaryReport
        // 
        this.Bands.AddRange(new DevExpress.XtraReports.UI.Band[] {
            this.Detail,
            this.pageFooterBand1,
            this.reportHeaderBand1,
            this.topMarginBand1,
            this.bottomMarginBand1,
            this.PageHeader});
        this.DataAdapter = this.sp_Pay_Employee_Attendance_Select_RowTableAdapter1;
        this.DataMember = "sp_Pay_Employee_Attendance_Select_Row";
        this.DataSource = this.reportData1;
        this.Font = new System.Drawing.Font("Times New Roman", 9.75F);
        this.Margins = new System.Drawing.Printing.Margins(0, 42, 0, 1);
        this.StyleSheet.AddRange(new DevExpress.XtraReports.UI.XRControlStyle[] {
            this.Title,
            this.FieldCaption,
            this.PageInfo,
            this.DataField});
        this.Version = "10.2";
        ((System.ComponentModel.ISupportInitialize)(this.xrTable1)).EndInit();
        ((System.ComponentModel.ISupportInitialize)(this.reportData1)).EndInit();
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

    private void Detail_BeforePrint_1(object sender, System.Drawing.Printing.PrintEventArgs e)
    {
        int RecordCount = 0;
        RecordCount += 1;

        if (RecordCount == 1)
        {
            Detail.PageBreak = PageBreak.AfterBand;
            RecordCount = 0;
        }
        else
        {
            Detail.PageBreak = PageBreak.None;
        }

    }

    private void xrTableCell62_BeforePrint(object sender, System.Drawing.Printing.PrintEventArgs e)
    {
       
    }

    private void xrTableCell59_BeforePrint(object sender, System.Drawing.Printing.PrintEventArgs e)
    {
        int sum = 0;

        try
        {
            DataTable dt = objAttReg.GetAttendanceRegDataByMonth_Year_EmpId(xrTableCell59.Text, xrMonth.Text, xrYear.Text);
            dt = new DataView(dt, "LateMin<>0", "", DataViewRowState.CurrentRows).ToTable();
            if (dt.Rows.Count > 0)
            {
                sum = dt.Rows.Count;

            }
        }
        catch
        {

        }
        xrTableCell59.Text = (sum.ToString());



    }

    private void xrTableCell57_BeforePrint(object sender, System.Drawing.Printing.PrintEventArgs e)
    {
        int sum = 0;

        try
        {
            DataTable dt = objAttReg.GetAttendanceRegDataByMonth_Year_EmpId(xrTableCell57.Text, xrMonth.Text, xrYear.Text);
            dt = new DataView(dt, "EarlyMin<>0", "", DataViewRowState.CurrentRows).ToTable();
            if (dt.Rows.Count > 0)
            {
                sum = dt.Rows.Count;

            }
        }
        catch
        {

        }
        xrTableCell57.Text = (sum.ToString());


    }

    private void xrTableCell34_BeforePrint(object sender, System.Drawing.Printing.PrintEventArgs e)
    {
        int sum = 0;

        try
        {
            DataTable dt = objAttReg.GetAttendanceRegDataByMonth_Year_EmpId(xrTableCell34.Text, xrMonth.Text, xrYear.Text);
            dt = new DataView(dt, "OverTime_Min<>0", "", DataViewRowState.CurrentRows).ToTable();
            if (dt.Rows.Count > 0)
            {
                sum = dt.Rows.Count;

            }
        }
        catch
        {
        }

        xrTableCell34.Text = (sum.ToString());

    }

    private void xrTableCell26_BeforePrint(object sender, System.Drawing.Printing.PrintEventArgs e)
    {
        int sum = 0;

        try
        {
            DataTable dt = objAttReg.GetAttendanceRegDataByMonth_Year_EmpId(xrTableCell26.Text, xrMonth.Text, xrYear.Text);
            dt = new DataView(dt, "Holiday_Min<>0", "", DataViewRowState.CurrentRows).ToTable();
            if (dt.Rows.Count > 0)
            {
                sum = dt.Rows.Count;

            }
        }
        catch
        {

        }
        xrTableCell26.Text = (sum.ToString());

    }
    public void setyearmonth(string year,string month)
    {
        xrYear.Text = year;
        xrMonth.Text = month;

    }

    private void xrTableCell15_BeforePrint(object sender, System.Drawing.Printing.PrintEventArgs e)
    {
        int sum = 0;

        try
        {
            DataTable dt = objAttReg.GetAttendanceRegDataByMonth_Year_EmpId(xrTableCell15.Text, xrMonth.Text, xrYear.Text);
            dt = new DataView(dt, "Week_Off_Min<>0", "", DataViewRowState.CurrentRows).ToTable();
            if (dt.Rows.Count > 0)
            {


                sum = dt.Rows.Count;


            }
        }
        catch
        {

        }
        xrTableCell15.Text = (sum.ToString());
    }

    private void xrTableCell47_BeforePrint(object sender, System.Drawing.Printing.PrintEventArgs e)
    {
        xrTableCell47.Text = GetHours(xrTableCell47.Text);
    }

    private void xrTableCell36_BeforePrint(object sender, System.Drawing.Printing.PrintEventArgs e)
    {
        xrTableCell36.Text = GetHours(xrTableCell36.Text);
    }

    private void xrTableCell56_BeforePrint(object sender, System.Drawing.Printing.PrintEventArgs e)
    {
        xrTableCell56.Text = GetHours(xrTableCell56.Text);
    }

    private void xrTableCell49_BeforePrint(object sender, System.Drawing.Printing.PrintEventArgs e)
    {
        xrTableCell49.Text = GetHours(xrTableCell49.Text);
    }

    private void xrTableCell31_BeforePrint(object sender, System.Drawing.Printing.PrintEventArgs e)
    {
        xrTableCell31.Text = GetHours(xrTableCell31.Text);
    }

    private void xrTableCell17_BeforePrint(object sender, System.Drawing.Printing.PrintEventArgs e)
    {
        xrTableCell17.Text = GetHours(xrTableCell17.Text);

    }

    private void xrTableCell72_BeforePrint(object sender, System.Drawing.Printing.PrintEventArgs e)
    {
        xrTableCell72.Text = GetHours(xrTableCell72.Text);

    }

    private void xrTableCell54_BeforePrint(object sender, System.Drawing.Printing.PrintEventArgs e)
    {
        double GrossSalary = 0;

        try
        {

            GrossSalary = double.Parse(xrTableCell10.Text) + double.Parse(xrTableCell28.Text) + double.Parse(xrTableCell22.Text) + double.Parse(xrTableCell14.Text);

        }
        catch
        {

        }
        xrTableCell54.Text = GrossSalary.ToString();
    }



    
}
