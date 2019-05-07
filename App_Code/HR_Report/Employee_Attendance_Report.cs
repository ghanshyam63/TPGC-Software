using System;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using DevExpress.XtraReports.UI;

/// <summary>
/// Summary description for Employee_Attendance_Report
/// </summary>
public class Employee_Attendance_Report : DevExpress.XtraReports.UI.XtraReport
{
	private DevExpress.XtraReports.UI.DetailBand Detail;
	private DevExpress.XtraReports.UI.TopMarginBand TopMargin;
    private DevExpress.XtraReports.UI.BottomMarginBand BottomMargin;
    private GroupHeaderBand GroupHeader1;
    private GroupHeaderBand GroupHeader2;
    private GroupHeaderBand GroupHeader3;
    private GroupHeaderBand GroupHeader4;
    private EmployeePaySlipDataSet employeePaySlipDataSet1;
    private XRPanel xrPanel9;
    private XRTable xrTable11;
    private XRTableRow xrTableRow23;
    private XRTableCell xrTableCell51;
    private XRTableRow xrTableRow24;
    private XRTableCell xrTableCell53;
    private XRTableCell xrTableCell73;
    private XRTableCell xrTableCell54;
    private XRTableRow xrTableRow25;
    private XRTableCell xrTableCell55;
    private XRTableCell xrTableCell74;
    private XRTableCell xrTableCell56;
    private XRTableRow xrTableRow27;
    private XRTableCell xrTableCell59;
    private XRTableCell xrTableCell76;
    private XRTableCell xrTableCell60;
    private XRTableRow xrTableRow28;
    private XRTableCell xrTableCell61;
    private XRTableCell xrTableCell77;
    private XRTableCell xrTableCell62;
    private XRTableRow xrTableRow7;
    private XRTableCell xrTableCell19;
    private XRTableCell xrTableCell20;
    private XRTableCell xrTableCell22;
    private XRTableCell xrTableCell24;
    private XRTableCell xrTableCell26;
    private XRTableCell xrTableCell27;
    private XRTableCell xrTableCell28;
    private XRPanel xrPanel3;
    private XRTable xrTable2;
    private XRTableRow xrTableRow8;
    private XRTableCell xrTableCell23;
    private XRTableCell xrTableCell29;
    private XRTableCell xrTableCell30;
    private XRTableRow xrTableRow9;
    private XRTableCell xrTableCell31;
    private XRTableCell xrTableCell32;
    private XRTableCell xrTableCell33;
    private XRTableCell xrTableCell34;
    private XRTableRow xrTableRow10;
    private XRTableCell xrTableCell35;
    private XRTableCell xrTableCell36;
    private XRTableCell xrTableCell37;
    private XRTableCell xrTableCell38;
    private XRTableRow xrTableRow11;
    private XRTableCell xrTableCell39;
    private XRTableCell xrTableCell40;
    private XRTableCell xrTableCell41;
    private XRTableCell xrTableCell42;
    private XRTableRow xrTableRow13;
    private XRTableCell xrTableCell47;
    private XRTableCell xrTableCell49;
    private XRPanel xrPanel4;
    private XRTable xrTable3;
    private XRTableRow xrTableRow12;
    private XRTableCell xrTableCell43;
    private XRTableCell xrTableCell44;
    private XRTableCell xrTableCell45;
    private XRTableRow xrTableRow14;
    private XRTableCell xrTableCell46;
    private XRTableCell xrTableCell48;
    private XRTableCell xrTableCell50;
    private XRTableCell xrTableCell52;
    private XRTableRow xrTableRow15;
    private XRTableCell xrTableCell57;
    private XRTableCell xrTableCell58;
    private XRTableCell xrTableCell63;
    private XRTableCell xrTableCell64;
    private XRTableRow xrTableRow16;
    private XRTableCell xrTableCell65;
    private XRTableCell xrTableCell66;
    private XRTableCell xrTableCell67;
    private XRTableCell xrTableCell68;
    private XRTableRow xrTableRow17;
    private XRTableCell xrTableCell69;
    private XRTableCell xrTableCell70;
    private XRTableCell xrTableCell71;
    private XRTableCell xrTableCell72;
    private XRTableRow xrTableRow18;
    private XRTableCell xrTableCell75;
    private XRTableCell xrTableCell79;
    private ReportFooterBand ReportFooter;
    private XRTable xrTable5;
    private XRTableRow xrTableRow19;
    private XRTableCell xrTableCell80;
    private XRLabel xrLabel9;
    private XRLabel xrLabel8;
    private XRTableRow xrTableRow22;
    private XRTableCell xrTableCell81;
    private XRLabel xrLabel21;
    private XRLabel xrLabel20;
    private XRTableRow xrTableRow35;
    private XRTableCell xrTableCell83;
    private XRTableCell xrTableCell21;
    private XRPanel xrPanel1;
    private XRPictureBox xrPictureBox2;
    private XRLabel xrLabel6;
    private XRLabel xrLabel5;
    private XRTable xrTable1;
    private XRTableRow xrTableRow1;
    private XRTableCell xrTableCell1;
    private XRTableCell xrTableCell2;
    private XRTableCell xrTableCell3;
    private XRTableRow xrTableRow2;
    private XRTableCell xrTableCell4;
    private XRTableCell xrTableCell5;
    private XRTableCell xrTableCell6;
    private XRTableRow xrTableRow4;
    private XRTableCell xrTableCell10;
    private XRTableCell xrTableCell11;
    private XRTableCell xrTableCell9;
    private XRTableRow xrTableRow3;
    private XRTableCell xrTableCell7;
    private XRTableCell xrTableCell8;
    private XRTableCell xrTableCell12;
    private XRTableRow xrTableRow5;
    private XRTableCell xrTableCell13;
    private XRTableCell xrTableCell14;
    private XRTableCell xrTableCell15;
    private XRTableRow xrTableRow6;
    private XRTableCell xrTableCell16;
    private XRTableCell xrTableCell17;
    private XRTableCell xrTableCell18;
    private XRLabel xrLabel7;
    private GroupFooterBand GroupFooter1;
    private XRTableRow xrTableRow20;
    private XRTableCell xrTableCell25;
    private XRPageBreak xrPageBreak1;
    private XRPanel xrPanel2;
    private XRLabel xrLabel1;
    private XRLabel xrLabel2;
    private XRLabel xrLabel3;
    private XRLabel xrLabel4;
    private XRLabel xrLabel10;
    private XRPictureBox xrPictureBox1;
    private XRLabel xrLabel11;
	/// <summary>
	/// Required designer variable.
	/// </summary>
	private System.ComponentModel.IContainer components = null;

	public Employee_Attendance_Report()
	{
		InitializeComponent();
		//
		// TODO: Add constructor logic here
		//
	}
    public void setimage(string url)
    {
        xrPictureBox1.ImageUrl = url;
        // xrPictureBox2.ImageUrl = url;
    }

    public void setEmpimage(string EmpImageurl)
    {
        xrPictureBox2.ImageUrl = EmpImageurl;
    
    }
    
    public void setempimage(string empurl)
    {
        xrPictureBox2.ImageUrl = empurl;
        // xrPictureBox2.ImageUrl = url;
    }

    public void setcompanyname(string companyname)
    {
        xrLabel11.Text = companyname;

    }
    public void setcompanyAddress(string Address)
    {
        xrLabel10.Text = Address;

    }
    public void setcontact(string Contact)
    {
        xrLabel4.Text = Contact;
        // xrLabel11.Text = Contact;
    }


    public void setwebsite(string web)
    {
        xrLabel2.Text = web;

    }
    public void setmailid(string mailid)
    {
        xrLabel1.Text = mailid;

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
        string resourceFileName = "Employee_Attendance_Report.resx";
        this.Detail = new DevExpress.XtraReports.UI.DetailBand();
        this.TopMargin = new DevExpress.XtraReports.UI.TopMarginBand();
        this.BottomMargin = new DevExpress.XtraReports.UI.BottomMarginBand();
        this.GroupHeader1 = new DevExpress.XtraReports.UI.GroupHeaderBand();
        this.xrPanel4 = new DevExpress.XtraReports.UI.XRPanel();
        this.xrTable3 = new DevExpress.XtraReports.UI.XRTable();
        this.xrTableRow12 = new DevExpress.XtraReports.UI.XRTableRow();
        this.xrTableCell43 = new DevExpress.XtraReports.UI.XRTableCell();
        this.xrTableCell44 = new DevExpress.XtraReports.UI.XRTableCell();
        this.xrTableCell45 = new DevExpress.XtraReports.UI.XRTableCell();
        this.xrTableRow14 = new DevExpress.XtraReports.UI.XRTableRow();
        this.xrTableCell46 = new DevExpress.XtraReports.UI.XRTableCell();
        this.xrTableCell48 = new DevExpress.XtraReports.UI.XRTableCell();
        this.xrTableCell50 = new DevExpress.XtraReports.UI.XRTableCell();
        this.xrTableCell52 = new DevExpress.XtraReports.UI.XRTableCell();
        this.xrTableRow15 = new DevExpress.XtraReports.UI.XRTableRow();
        this.xrTableCell57 = new DevExpress.XtraReports.UI.XRTableCell();
        this.xrTableCell58 = new DevExpress.XtraReports.UI.XRTableCell();
        this.xrTableCell63 = new DevExpress.XtraReports.UI.XRTableCell();
        this.xrTableCell64 = new DevExpress.XtraReports.UI.XRTableCell();
        this.xrTableRow16 = new DevExpress.XtraReports.UI.XRTableRow();
        this.xrTableCell65 = new DevExpress.XtraReports.UI.XRTableCell();
        this.xrTableCell66 = new DevExpress.XtraReports.UI.XRTableCell();
        this.xrTableCell67 = new DevExpress.XtraReports.UI.XRTableCell();
        this.xrTableCell68 = new DevExpress.XtraReports.UI.XRTableCell();
        this.xrTableRow17 = new DevExpress.XtraReports.UI.XRTableRow();
        this.xrTableCell69 = new DevExpress.XtraReports.UI.XRTableCell();
        this.xrTableCell70 = new DevExpress.XtraReports.UI.XRTableCell();
        this.xrTableCell71 = new DevExpress.XtraReports.UI.XRTableCell();
        this.xrTableCell72 = new DevExpress.XtraReports.UI.XRTableCell();
        this.xrTableRow18 = new DevExpress.XtraReports.UI.XRTableRow();
        this.xrTableCell75 = new DevExpress.XtraReports.UI.XRTableCell();
        this.xrTableCell79 = new DevExpress.XtraReports.UI.XRTableCell();
        this.GroupHeader2 = new DevExpress.XtraReports.UI.GroupHeaderBand();
        this.xrPanel3 = new DevExpress.XtraReports.UI.XRPanel();
        this.xrTable2 = new DevExpress.XtraReports.UI.XRTable();
        this.xrTableRow8 = new DevExpress.XtraReports.UI.XRTableRow();
        this.xrTableCell23 = new DevExpress.XtraReports.UI.XRTableCell();
        this.xrTableCell29 = new DevExpress.XtraReports.UI.XRTableCell();
        this.xrTableCell30 = new DevExpress.XtraReports.UI.XRTableCell();
        this.xrTableRow9 = new DevExpress.XtraReports.UI.XRTableRow();
        this.xrTableCell31 = new DevExpress.XtraReports.UI.XRTableCell();
        this.xrTableCell32 = new DevExpress.XtraReports.UI.XRTableCell();
        this.xrTableCell33 = new DevExpress.XtraReports.UI.XRTableCell();
        this.xrTableCell34 = new DevExpress.XtraReports.UI.XRTableCell();
        this.xrTableRow10 = new DevExpress.XtraReports.UI.XRTableRow();
        this.xrTableCell35 = new DevExpress.XtraReports.UI.XRTableCell();
        this.xrTableCell36 = new DevExpress.XtraReports.UI.XRTableCell();
        this.xrTableCell37 = new DevExpress.XtraReports.UI.XRTableCell();
        this.xrTableCell38 = new DevExpress.XtraReports.UI.XRTableCell();
        this.xrTableRow11 = new DevExpress.XtraReports.UI.XRTableRow();
        this.xrTableCell39 = new DevExpress.XtraReports.UI.XRTableCell();
        this.xrTableCell40 = new DevExpress.XtraReports.UI.XRTableCell();
        this.xrTableCell41 = new DevExpress.XtraReports.UI.XRTableCell();
        this.xrTableCell42 = new DevExpress.XtraReports.UI.XRTableCell();
        this.xrTableRow13 = new DevExpress.XtraReports.UI.XRTableRow();
        this.xrTableCell47 = new DevExpress.XtraReports.UI.XRTableCell();
        this.xrTableCell49 = new DevExpress.XtraReports.UI.XRTableCell();
        this.GroupHeader3 = new DevExpress.XtraReports.UI.GroupHeaderBand();
        this.xrPanel9 = new DevExpress.XtraReports.UI.XRPanel();
        this.xrTable11 = new DevExpress.XtraReports.UI.XRTable();
        this.xrTableRow23 = new DevExpress.XtraReports.UI.XRTableRow();
        this.xrTableCell51 = new DevExpress.XtraReports.UI.XRTableCell();
        this.xrTableCell28 = new DevExpress.XtraReports.UI.XRTableCell();
        this.xrTableCell20 = new DevExpress.XtraReports.UI.XRTableCell();
        this.xrTableRow24 = new DevExpress.XtraReports.UI.XRTableRow();
        this.xrTableCell53 = new DevExpress.XtraReports.UI.XRTableCell();
        this.xrTableCell73 = new DevExpress.XtraReports.UI.XRTableCell();
        this.xrTableCell54 = new DevExpress.XtraReports.UI.XRTableCell();
        this.xrTableCell21 = new DevExpress.XtraReports.UI.XRTableCell();
        this.xrTableRow25 = new DevExpress.XtraReports.UI.XRTableRow();
        this.xrTableCell55 = new DevExpress.XtraReports.UI.XRTableCell();
        this.xrTableCell74 = new DevExpress.XtraReports.UI.XRTableCell();
        this.xrTableCell56 = new DevExpress.XtraReports.UI.XRTableCell();
        this.xrTableCell22 = new DevExpress.XtraReports.UI.XRTableCell();
        this.xrTableRow27 = new DevExpress.XtraReports.UI.XRTableRow();
        this.xrTableCell59 = new DevExpress.XtraReports.UI.XRTableCell();
        this.xrTableCell76 = new DevExpress.XtraReports.UI.XRTableCell();
        this.xrTableCell60 = new DevExpress.XtraReports.UI.XRTableCell();
        this.xrTableCell24 = new DevExpress.XtraReports.UI.XRTableCell();
        this.xrTableRow28 = new DevExpress.XtraReports.UI.XRTableRow();
        this.xrTableCell61 = new DevExpress.XtraReports.UI.XRTableCell();
        this.xrTableCell77 = new DevExpress.XtraReports.UI.XRTableCell();
        this.xrTableCell62 = new DevExpress.XtraReports.UI.XRTableCell();
        this.xrTableCell26 = new DevExpress.XtraReports.UI.XRTableCell();
        this.xrTableRow7 = new DevExpress.XtraReports.UI.XRTableRow();
        this.xrTableCell19 = new DevExpress.XtraReports.UI.XRTableCell();
        this.xrTableCell27 = new DevExpress.XtraReports.UI.XRTableCell();
        this.GroupHeader4 = new DevExpress.XtraReports.UI.GroupHeaderBand();
        this.xrPanel1 = new DevExpress.XtraReports.UI.XRPanel();
        this.xrPictureBox2 = new DevExpress.XtraReports.UI.XRPictureBox();
        this.xrLabel6 = new DevExpress.XtraReports.UI.XRLabel();
        this.xrLabel5 = new DevExpress.XtraReports.UI.XRLabel();
        this.xrTable1 = new DevExpress.XtraReports.UI.XRTable();
        this.xrTableRow1 = new DevExpress.XtraReports.UI.XRTableRow();
        this.xrTableCell1 = new DevExpress.XtraReports.UI.XRTableCell();
        this.xrTableCell2 = new DevExpress.XtraReports.UI.XRTableCell();
        this.xrTableCell3 = new DevExpress.XtraReports.UI.XRTableCell();
        this.xrTableRow2 = new DevExpress.XtraReports.UI.XRTableRow();
        this.xrTableCell4 = new DevExpress.XtraReports.UI.XRTableCell();
        this.xrTableCell5 = new DevExpress.XtraReports.UI.XRTableCell();
        this.xrTableCell6 = new DevExpress.XtraReports.UI.XRTableCell();
        this.xrTableRow4 = new DevExpress.XtraReports.UI.XRTableRow();
        this.xrTableCell10 = new DevExpress.XtraReports.UI.XRTableCell();
        this.xrTableCell11 = new DevExpress.XtraReports.UI.XRTableCell();
        this.xrTableCell9 = new DevExpress.XtraReports.UI.XRTableCell();
        this.xrTableRow3 = new DevExpress.XtraReports.UI.XRTableRow();
        this.xrTableCell7 = new DevExpress.XtraReports.UI.XRTableCell();
        this.xrTableCell8 = new DevExpress.XtraReports.UI.XRTableCell();
        this.xrTableCell12 = new DevExpress.XtraReports.UI.XRTableCell();
        this.xrTableRow5 = new DevExpress.XtraReports.UI.XRTableRow();
        this.xrTableCell13 = new DevExpress.XtraReports.UI.XRTableCell();
        this.xrTableCell14 = new DevExpress.XtraReports.UI.XRTableCell();
        this.xrTableCell15 = new DevExpress.XtraReports.UI.XRTableCell();
        this.xrTableRow6 = new DevExpress.XtraReports.UI.XRTableRow();
        this.xrTableCell16 = new DevExpress.XtraReports.UI.XRTableCell();
        this.xrTableCell17 = new DevExpress.XtraReports.UI.XRTableCell();
        this.xrTableCell18 = new DevExpress.XtraReports.UI.XRTableCell();
        this.xrLabel7 = new DevExpress.XtraReports.UI.XRLabel();
        this.employeePaySlipDataSet1 = new EmployeePaySlipDataSet();
        this.ReportFooter = new DevExpress.XtraReports.UI.ReportFooterBand();
        this.xrTable5 = new DevExpress.XtraReports.UI.XRTable();
        this.xrTableRow19 = new DevExpress.XtraReports.UI.XRTableRow();
        this.xrTableCell80 = new DevExpress.XtraReports.UI.XRTableCell();
        this.xrLabel9 = new DevExpress.XtraReports.UI.XRLabel();
        this.xrLabel8 = new DevExpress.XtraReports.UI.XRLabel();
        this.xrTableRow22 = new DevExpress.XtraReports.UI.XRTableRow();
        this.xrTableCell81 = new DevExpress.XtraReports.UI.XRTableCell();
        this.xrLabel21 = new DevExpress.XtraReports.UI.XRLabel();
        this.xrLabel20 = new DevExpress.XtraReports.UI.XRLabel();
        this.xrTableRow35 = new DevExpress.XtraReports.UI.XRTableRow();
        this.xrTableCell83 = new DevExpress.XtraReports.UI.XRTableCell();
        this.GroupFooter1 = new DevExpress.XtraReports.UI.GroupFooterBand();
        this.xrTableRow20 = new DevExpress.XtraReports.UI.XRTableRow();
        this.xrTableCell25 = new DevExpress.XtraReports.UI.XRTableCell();
        this.xrPageBreak1 = new DevExpress.XtraReports.UI.XRPageBreak();
        this.xrPanel2 = new DevExpress.XtraReports.UI.XRPanel();
        this.xrLabel1 = new DevExpress.XtraReports.UI.XRLabel();
        this.xrLabel2 = new DevExpress.XtraReports.UI.XRLabel();
        this.xrLabel3 = new DevExpress.XtraReports.UI.XRLabel();
        this.xrLabel4 = new DevExpress.XtraReports.UI.XRLabel();
        this.xrLabel10 = new DevExpress.XtraReports.UI.XRLabel();
        this.xrPictureBox1 = new DevExpress.XtraReports.UI.XRPictureBox();
        this.xrLabel11 = new DevExpress.XtraReports.UI.XRLabel();
        ((System.ComponentModel.ISupportInitialize)(this.xrTable3)).BeginInit();
        ((System.ComponentModel.ISupportInitialize)(this.xrTable2)).BeginInit();
        ((System.ComponentModel.ISupportInitialize)(this.xrTable11)).BeginInit();
        ((System.ComponentModel.ISupportInitialize)(this.xrTable1)).BeginInit();
        ((System.ComponentModel.ISupportInitialize)(this.employeePaySlipDataSet1)).BeginInit();
        ((System.ComponentModel.ISupportInitialize)(this.xrTable5)).BeginInit();
        ((System.ComponentModel.ISupportInitialize)(this)).BeginInit();
        // 
        // Detail
        // 
        this.Detail.HeightF = 7.291667F;
        this.Detail.Name = "Detail";
        this.Detail.Padding = new DevExpress.XtraPrinting.PaddingInfo(0, 0, 0, 0, 100F);
        this.Detail.TextAlignment = DevExpress.XtraPrinting.TextAlignment.TopLeft;
        // 
        // TopMargin
        // 
        this.TopMargin.Controls.AddRange(new DevExpress.XtraReports.UI.XRControl[] {
            this.xrPanel2});
        this.TopMargin.HeightF = 121.125F;
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
            this.xrPanel4});
        this.GroupHeader1.HeightF = 73.75008F;
        this.GroupHeader1.Name = "GroupHeader1";
        // 
        // xrPanel4
        // 
        this.xrPanel4.Borders = ((DevExpress.XtraPrinting.BorderSide)(((DevExpress.XtraPrinting.BorderSide.Left | DevExpress.XtraPrinting.BorderSide.Right)
                    | DevExpress.XtraPrinting.BorderSide.Bottom)));
        this.xrPanel4.Controls.AddRange(new DevExpress.XtraReports.UI.XRControl[] {
            this.xrTable3});
        this.xrPanel4.LocationFloat = new DevExpress.Utils.PointFloat(1.589457E-05F, 0F);
        this.xrPanel4.Name = "xrPanel4";
        this.xrPanel4.SizeF = new System.Drawing.SizeF(473.625F, 73.75008F);
        this.xrPanel4.StylePriority.UseBorders = false;
        // 
        // xrTable3
        // 
        this.xrTable3.Borders = DevExpress.XtraPrinting.BorderSide.None;
        this.xrTable3.LocationFloat = new DevExpress.Utils.PointFloat(1.818989E-12F, 0F);
        this.xrTable3.Name = "xrTable3";
        this.xrTable3.Rows.AddRange(new DevExpress.XtraReports.UI.XRTableRow[] {
            this.xrTableRow12,
            this.xrTableRow14,
            this.xrTableRow15,
            this.xrTableRow16,
            this.xrTableRow17,
            this.xrTableRow18});
        this.xrTable3.SizeF = new System.Drawing.SizeF(473.625F, 73.75008F);
        this.xrTable3.StylePriority.UseBorders = false;
        // 
        // xrTableRow12
        // 
        this.xrTableRow12.Cells.AddRange(new DevExpress.XtraReports.UI.XRTableCell[] {
            this.xrTableCell43,
            this.xrTableCell44,
            this.xrTableCell45});
        this.xrTableRow12.Name = "xrTableRow12";
        this.xrTableRow12.Weight = 1;
        // 
        // xrTableCell43
        // 
        this.xrTableCell43.Borders = DevExpress.XtraPrinting.BorderSide.Bottom;
        this.xrTableCell43.Font = new System.Drawing.Font("Arial", 9.75F, System.Drawing.FontStyle.Bold);
        this.xrTableCell43.Name = "xrTableCell43";
        this.xrTableCell43.StylePriority.UseBorders = false;
        this.xrTableCell43.StylePriority.UseFont = false;
        this.xrTableCell43.Text = "  PENALTIES";
        this.xrTableCell43.Weight = 0.74014631001116538;
        // 
        // xrTableCell44
        // 
        this.xrTableCell44.Borders = DevExpress.XtraPrinting.BorderSide.Bottom;
        this.xrTableCell44.Name = "xrTableCell44";
        this.xrTableCell44.StylePriority.UseBorders = false;
        this.xrTableCell44.StylePriority.UseTextAlignment = false;
        this.xrTableCell44.Text = "MIN/DAYS";
        this.xrTableCell44.TextAlignment = DevExpress.XtraPrinting.TextAlignment.TopCenter;
        this.xrTableCell44.Weight = 0.61174075008503415;
        // 
        // xrTableCell45
        // 
        this.xrTableCell45.Borders = DevExpress.XtraPrinting.BorderSide.Bottom;
        this.xrTableCell45.Name = "xrTableCell45";
        this.xrTableCell45.StylePriority.UseBorders = false;
        this.xrTableCell45.StylePriority.UseTextAlignment = false;
        this.xrTableCell45.Text = "SALARY";
        this.xrTableCell45.TextAlignment = DevExpress.XtraPrinting.TextAlignment.TopCenter;
        this.xrTableCell45.Weight = 1.058741586289669;
        // 
        // xrTableRow14
        // 
        this.xrTableRow14.Cells.AddRange(new DevExpress.XtraReports.UI.XRTableCell[] {
            this.xrTableCell46,
            this.xrTableCell48,
            this.xrTableCell50,
            this.xrTableCell52});
        this.xrTableRow14.Name = "xrTableRow14";
        this.xrTableRow14.Weight = 1;
        // 
        // xrTableCell46
        // 
        this.xrTableCell46.Font = new System.Drawing.Font("Arial", 8F);
        this.xrTableCell46.Name = "xrTableCell46";
        this.xrTableCell46.StylePriority.UseFont = false;
        this.xrTableCell46.Text = "  Late";
        this.xrTableCell46.Weight = 0.56734756269597442;
        // 
        // xrTableCell48
        // 
        this.xrTableCell48.Name = "xrTableCell48";
        this.xrTableCell48.Text = ":";
        this.xrTableCell48.Weight = 0.066226089421506373;
        // 
        // xrTableCell50
        // 
        this.xrTableCell50.DataBindings.AddRange(new DevExpress.XtraReports.UI.XRBinding[] {
            new DevExpress.XtraReports.UI.XRBinding("Text", null, "dtEmpPayslip.LatePenalty")});
        this.xrTableCell50.Font = new System.Drawing.Font("Arial", 9F);
        this.xrTableCell50.Name = "xrTableCell50";
        this.xrTableCell50.StylePriority.UseFont = false;
        this.xrTableCell50.StylePriority.UseTextAlignment = false;
        this.xrTableCell50.TextAlignment = DevExpress.XtraPrinting.TextAlignment.TopCenter;
        this.xrTableCell50.Weight = 0.71831324726921686;
        // 
        // xrTableCell52
        // 
        this.xrTableCell52.DataBindings.AddRange(new DevExpress.XtraReports.UI.XRBinding[] {
            new DevExpress.XtraReports.UI.XRBinding("Text", null, "dtEmpPayslip.LatePenaSal")});
        this.xrTableCell52.Name = "xrTableCell52";
        this.xrTableCell52.StylePriority.UseTextAlignment = false;
        this.xrTableCell52.Text = "xrTableCell21";
        this.xrTableCell52.TextAlignment = DevExpress.XtraPrinting.TextAlignment.TopCenter;
        this.xrTableCell52.Weight = 1.058741746999172;
        // 
        // xrTableRow15
        // 
        this.xrTableRow15.Cells.AddRange(new DevExpress.XtraReports.UI.XRTableCell[] {
            this.xrTableCell57,
            this.xrTableCell58,
            this.xrTableCell63,
            this.xrTableCell64});
        this.xrTableRow15.Name = "xrTableRow15";
        this.xrTableRow15.Weight = 1;
        // 
        // xrTableCell57
        // 
        this.xrTableCell57.Font = new System.Drawing.Font("Arial", 8F);
        this.xrTableCell57.Name = "xrTableCell57";
        this.xrTableCell57.StylePriority.UseFont = false;
        this.xrTableCell57.Text = "  Early";
        this.xrTableCell57.Weight = 0.56734755442145768;
        // 
        // xrTableCell58
        // 
        this.xrTableCell58.Name = "xrTableCell58";
        this.xrTableCell58.Text = ":";
        this.xrTableCell58.Weight = 0.066226087988112317;
        // 
        // xrTableCell63
        // 
        this.xrTableCell63.DataBindings.AddRange(new DevExpress.XtraReports.UI.XRBinding[] {
            new DevExpress.XtraReports.UI.XRBinding("Text", null, "dtEmpPayslip.EarlyPenalty")});
        this.xrTableCell63.Font = new System.Drawing.Font("Arial", 9F);
        this.xrTableCell63.Name = "xrTableCell63";
        this.xrTableCell63.StylePriority.UseFont = false;
        this.xrTableCell63.StylePriority.UseTextAlignment = false;
        this.xrTableCell63.TextAlignment = DevExpress.XtraPrinting.TextAlignment.TopCenter;
        this.xrTableCell63.Weight = 0.71831325697712756;
        // 
        // xrTableCell64
        // 
        this.xrTableCell64.DataBindings.AddRange(new DevExpress.XtraReports.UI.XRBinding[] {
            new DevExpress.XtraReports.UI.XRBinding("Text", null, "dtEmpPayslip.EarlyPenaSal")});
        this.xrTableCell64.Name = "xrTableCell64";
        this.xrTableCell64.StylePriority.UseTextAlignment = false;
        this.xrTableCell64.Text = "xrTableCell22";
        this.xrTableCell64.TextAlignment = DevExpress.XtraPrinting.TextAlignment.TopCenter;
        this.xrTableCell64.Weight = 1.058741746999172;
        // 
        // xrTableRow16
        // 
        this.xrTableRow16.Cells.AddRange(new DevExpress.XtraReports.UI.XRTableCell[] {
            this.xrTableCell65,
            this.xrTableCell66,
            this.xrTableCell67,
            this.xrTableCell68});
        this.xrTableRow16.Name = "xrTableRow16";
        this.xrTableRow16.Weight = 1;
        // 
        // xrTableCell65
        // 
        this.xrTableCell65.Font = new System.Drawing.Font("Arial", 8F);
        this.xrTableCell65.Name = "xrTableCell65";
        this.xrTableCell65.StylePriority.UseFont = false;
        this.xrTableCell65.Text = "  Partial";
        this.xrTableCell65.Weight = 0.56734755442145768;
        // 
        // xrTableCell66
        // 
        this.xrTableCell66.Name = "xrTableCell66";
        this.xrTableCell66.Text = ":";
        this.xrTableCell66.Weight = 0.066226087988112317;
        // 
        // xrTableCell67
        // 
        this.xrTableCell67.DataBindings.AddRange(new DevExpress.XtraReports.UI.XRBinding[] {
            new DevExpress.XtraReports.UI.XRBinding("Text", null, "dtEmpPayslip.PartialPenalty")});
        this.xrTableCell67.Font = new System.Drawing.Font("Arial", 9F);
        this.xrTableCell67.Name = "xrTableCell67";
        this.xrTableCell67.StylePriority.UseFont = false;
        this.xrTableCell67.StylePriority.UseTextAlignment = false;
        this.xrTableCell67.TextAlignment = DevExpress.XtraPrinting.TextAlignment.TopCenter;
        this.xrTableCell67.Weight = 0.71831325697712756;
        // 
        // xrTableCell68
        // 
        this.xrTableCell68.DataBindings.AddRange(new DevExpress.XtraReports.UI.XRBinding[] {
            new DevExpress.XtraReports.UI.XRBinding("Text", null, "dtEmpPayslip.PartialPenaSal")});
        this.xrTableCell68.Name = "xrTableCell68";
        this.xrTableCell68.StylePriority.UseTextAlignment = false;
        this.xrTableCell68.Text = "xrTableCell24";
        this.xrTableCell68.TextAlignment = DevExpress.XtraPrinting.TextAlignment.TopCenter;
        this.xrTableCell68.Weight = 1.058741746999172;
        // 
        // xrTableRow17
        // 
        this.xrTableRow17.Cells.AddRange(new DevExpress.XtraReports.UI.XRTableCell[] {
            this.xrTableCell69,
            this.xrTableCell70,
            this.xrTableCell71,
            this.xrTableCell72});
        this.xrTableRow17.Name = "xrTableRow17";
        this.xrTableRow17.Weight = 1;
        // 
        // xrTableCell69
        // 
        this.xrTableCell69.Borders = DevExpress.XtraPrinting.BorderSide.Bottom;
        this.xrTableCell69.Font = new System.Drawing.Font("Arial", 8F);
        this.xrTableCell69.Name = "xrTableCell69";
        this.xrTableCell69.StylePriority.UseBorders = false;
        this.xrTableCell69.StylePriority.UseFont = false;
        this.xrTableCell69.Text = "  Absent";
        this.xrTableCell69.Weight = 0.56734755442145768;
        // 
        // xrTableCell70
        // 
        this.xrTableCell70.Borders = DevExpress.XtraPrinting.BorderSide.Bottom;
        this.xrTableCell70.Name = "xrTableCell70";
        this.xrTableCell70.StylePriority.UseBorders = false;
        this.xrTableCell70.Text = ":";
        this.xrTableCell70.Weight = 0.10813446906817072;
        // 
        // xrTableCell71
        // 
        this.xrTableCell71.Borders = DevExpress.XtraPrinting.BorderSide.Bottom;
        this.xrTableCell71.DataBindings.AddRange(new DevExpress.XtraReports.UI.XRBinding[] {
            new DevExpress.XtraReports.UI.XRBinding("Text", null, "dtEmpPayslip.AbsentPenalty")});
        this.xrTableCell71.Font = new System.Drawing.Font("Arial", 9F);
        this.xrTableCell71.Name = "xrTableCell71";
        this.xrTableCell71.StylePriority.UseBorders = false;
        this.xrTableCell71.StylePriority.UseFont = false;
        this.xrTableCell71.StylePriority.UseTextAlignment = false;
        this.xrTableCell71.TextAlignment = DevExpress.XtraPrinting.TextAlignment.TopCenter;
        this.xrTableCell71.Weight = 0.67640487589706921;
        // 
        // xrTableCell72
        // 
        this.xrTableCell72.Borders = DevExpress.XtraPrinting.BorderSide.Bottom;
        this.xrTableCell72.DataBindings.AddRange(new DevExpress.XtraReports.UI.XRBinding[] {
            new DevExpress.XtraReports.UI.XRBinding("Text", null, "dtEmpPayslip.AbsentPenaSal")});
        this.xrTableCell72.Name = "xrTableCell72";
        this.xrTableCell72.StylePriority.UseBorders = false;
        this.xrTableCell72.StylePriority.UseTextAlignment = false;
        this.xrTableCell72.TextAlignment = DevExpress.XtraPrinting.TextAlignment.TopCenter;
        this.xrTableCell72.Weight = 1.058741746999172;
        // 
        // xrTableRow18
        // 
        this.xrTableRow18.Cells.AddRange(new DevExpress.XtraReports.UI.XRTableCell[] {
            this.xrTableCell75,
            this.xrTableCell79});
        this.xrTableRow18.Name = "xrTableRow18";
        this.xrTableRow18.Weight = 1;
        // 
        // xrTableCell75
        // 
        this.xrTableCell75.Font = new System.Drawing.Font("Times New Roman", 9.75F);
        this.xrTableCell75.Name = "xrTableCell75";
        this.xrTableCell75.StylePriority.UseFont = false;
        this.xrTableCell75.StylePriority.UseTextAlignment = false;
        this.xrTableCell75.Text = "                                    GROSS PAY:";
        this.xrTableCell75.TextAlignment = DevExpress.XtraPrinting.TextAlignment.TopRight;
        this.xrTableCell75.Weight = 1.3268025738541549;
        // 
        // xrTableCell79
        // 
        this.xrTableCell79.DataBindings.AddRange(new DevExpress.XtraReports.UI.XRBinding[] {
            new DevExpress.XtraReports.UI.XRBinding("Text", null, "dtEmpPayslip.TotalPenaltySal")});
        this.xrTableCell79.Font = new System.Drawing.Font("Times New Roman", 9.75F, System.Drawing.FontStyle.Bold);
        this.xrTableCell79.Name = "xrTableCell79";
        this.xrTableCell79.StylePriority.UseFont = false;
        this.xrTableCell79.StylePriority.UseTextAlignment = false;
        this.xrTableCell79.TextAlignment = DevExpress.XtraPrinting.TextAlignment.TopCenter;
        this.xrTableCell79.Weight = 1.0838260725317146;
        // 
        // GroupHeader2
        // 
        this.GroupHeader2.Controls.AddRange(new DevExpress.XtraReports.UI.XRControl[] {
            this.xrPanel3});
        this.GroupHeader2.HeightF = 52.91665F;
        this.GroupHeader2.Level = 1;
        this.GroupHeader2.Name = "GroupHeader2";
        // 
        // xrPanel3
        // 
        this.xrPanel3.Borders = ((DevExpress.XtraPrinting.BorderSide)(((DevExpress.XtraPrinting.BorderSide.Left | DevExpress.XtraPrinting.BorderSide.Right)
                    | DevExpress.XtraPrinting.BorderSide.Bottom)));
        this.xrPanel3.Controls.AddRange(new DevExpress.XtraReports.UI.XRControl[] {
            this.xrTable2});
        this.xrPanel3.LocationFloat = new DevExpress.Utils.PointFloat(1.589457E-05F, 0F);
        this.xrPanel3.Name = "xrPanel3";
        this.xrPanel3.SizeF = new System.Drawing.SizeF(473.6252F, 52.91665F);
        this.xrPanel3.StylePriority.UseBorders = false;
        // 
        // xrTable2
        // 
        this.xrTable2.Borders = DevExpress.XtraPrinting.BorderSide.None;
        this.xrTable2.LocationFloat = new DevExpress.Utils.PointFloat(1.125129F, 0F);
        this.xrTable2.Name = "xrTable2";
        this.xrTable2.Rows.AddRange(new DevExpress.XtraReports.UI.XRTableRow[] {
            this.xrTableRow8,
            this.xrTableRow9,
            this.xrTableRow10,
            this.xrTableRow11,
            this.xrTableRow13});
        this.xrTable2.SizeF = new System.Drawing.SizeF(472.5F, 52.91665F);
        this.xrTable2.StylePriority.UseBorders = false;
        // 
        // xrTableRow8
        // 
        this.xrTableRow8.Cells.AddRange(new DevExpress.XtraReports.UI.XRTableCell[] {
            this.xrTableCell23,
            this.xrTableCell29,
            this.xrTableCell30});
        this.xrTableRow8.Name = "xrTableRow8";
        this.xrTableRow8.Weight = 1;
        // 
        // xrTableCell23
        // 
        this.xrTableCell23.Borders = DevExpress.XtraPrinting.BorderSide.Bottom;
        this.xrTableCell23.Font = new System.Drawing.Font("Arial", 9.75F, System.Drawing.FontStyle.Bold);
        this.xrTableCell23.Name = "xrTableCell23";
        this.xrTableCell23.StylePriority.UseBorders = false;
        this.xrTableCell23.StylePriority.UseFont = false;
        this.xrTableCell23.Text = " OVERTIME";
        this.xrTableCell23.Weight = 0.6760206431594572;
        // 
        // xrTableCell29
        // 
        this.xrTableCell29.Borders = DevExpress.XtraPrinting.BorderSide.Bottom;
        this.xrTableCell29.Name = "xrTableCell29";
        this.xrTableCell29.StylePriority.UseBorders = false;
        this.xrTableCell29.StylePriority.UseTextAlignment = false;
        this.xrTableCell29.Text = "MIN";
        this.xrTableCell29.TextAlignment = DevExpress.XtraPrinting.TextAlignment.TopCenter;
        this.xrTableCell29.Weight = 0.61174075008503415;
        // 
        // xrTableCell30
        // 
        this.xrTableCell30.Borders = DevExpress.XtraPrinting.BorderSide.Bottom;
        this.xrTableCell30.Name = "xrTableCell30";
        this.xrTableCell30.StylePriority.UseBorders = false;
        this.xrTableCell30.StylePriority.UseTextAlignment = false;
        this.xrTableCell30.Text = "SALARY";
        this.xrTableCell30.TextAlignment = DevExpress.XtraPrinting.TextAlignment.TopCenter;
        this.xrTableCell30.Weight = 1.058741586289669;
        // 
        // xrTableRow9
        // 
        this.xrTableRow9.Cells.AddRange(new DevExpress.XtraReports.UI.XRTableCell[] {
            this.xrTableCell31,
            this.xrTableCell32,
            this.xrTableCell33,
            this.xrTableCell34});
        this.xrTableRow9.Name = "xrTableRow9";
        this.xrTableRow9.Weight = 1;
        // 
        // xrTableCell31
        // 
        this.xrTableCell31.Font = new System.Drawing.Font("Arial", 8F);
        this.xrTableCell31.Name = "xrTableCell31";
        this.xrTableCell31.StylePriority.UseFont = false;
        this.xrTableCell31.Text = "  Normal OT";
        this.xrTableCell31.Weight = 0.54669208704730854;
        // 
        // xrTableCell32
        // 
        this.xrTableCell32.Name = "xrTableCell32";
        this.xrTableCell32.Text = ":";
        this.xrTableCell32.Weight = 0.064664269590611667;
        // 
        // xrTableCell33
        // 
        this.xrTableCell33.DataBindings.AddRange(new DevExpress.XtraReports.UI.XRBinding[] {
            new DevExpress.XtraReports.UI.XRBinding("Text", null, "dtEmpPayslip.NormalOT")});
        this.xrTableCell33.Font = new System.Drawing.Font("Arial", 9F);
        this.xrTableCell33.Name = "xrTableCell33";
        this.xrTableCell33.StylePriority.UseFont = false;
        this.xrTableCell33.StylePriority.UseTextAlignment = false;
        this.xrTableCell33.TextAlignment = DevExpress.XtraPrinting.TextAlignment.TopCenter;
        this.xrTableCell33.Weight = 0.67640487589706921;
        // 
        // xrTableCell34
        // 
        this.xrTableCell34.DataBindings.AddRange(new DevExpress.XtraReports.UI.XRBinding[] {
            new DevExpress.XtraReports.UI.XRBinding("Text", null, "dtEmpPayslip.NormalOTSal")});
        this.xrTableCell34.Name = "xrTableCell34";
        this.xrTableCell34.StylePriority.UseTextAlignment = false;
        this.xrTableCell34.Text = "xrTableCell21";
        this.xrTableCell34.TextAlignment = DevExpress.XtraPrinting.TextAlignment.TopCenter;
        this.xrTableCell34.Weight = 1.058741746999172;
        // 
        // xrTableRow10
        // 
        this.xrTableRow10.Cells.AddRange(new DevExpress.XtraReports.UI.XRTableCell[] {
            this.xrTableCell35,
            this.xrTableCell36,
            this.xrTableCell37,
            this.xrTableCell38});
        this.xrTableRow10.Name = "xrTableRow10";
        this.xrTableRow10.Weight = 1;
        // 
        // xrTableCell35
        // 
        this.xrTableCell35.Font = new System.Drawing.Font("Arial", 8F);
        this.xrTableCell35.Name = "xrTableCell35";
        this.xrTableCell35.StylePriority.UseFont = false;
        this.xrTableCell35.Text = "  Week Off OT";
        this.xrTableCell35.Weight = 0.54669208704730854;
        // 
        // xrTableCell36
        // 
        this.xrTableCell36.Name = "xrTableCell36";
        this.xrTableCell36.Text = ":";
        this.xrTableCell36.Weight = 0.064664269590611667;
        // 
        // xrTableCell37
        // 
        this.xrTableCell37.DataBindings.AddRange(new DevExpress.XtraReports.UI.XRBinding[] {
            new DevExpress.XtraReports.UI.XRBinding("Text", null, "dtEmpPayslip.WeekOT")});
        this.xrTableCell37.Font = new System.Drawing.Font("Arial", 9F);
        this.xrTableCell37.Name = "xrTableCell37";
        this.xrTableCell37.StylePriority.UseFont = false;
        this.xrTableCell37.StylePriority.UseTextAlignment = false;
        this.xrTableCell37.TextAlignment = DevExpress.XtraPrinting.TextAlignment.TopCenter;
        this.xrTableCell37.Weight = 0.67640487589706921;
        // 
        // xrTableCell38
        // 
        this.xrTableCell38.DataBindings.AddRange(new DevExpress.XtraReports.UI.XRBinding[] {
            new DevExpress.XtraReports.UI.XRBinding("Text", null, "dtEmpPayslip.WeekOffOTSal")});
        this.xrTableCell38.Name = "xrTableCell38";
        this.xrTableCell38.StylePriority.UseTextAlignment = false;
        this.xrTableCell38.Text = "xrTableCell22";
        this.xrTableCell38.TextAlignment = DevExpress.XtraPrinting.TextAlignment.TopCenter;
        this.xrTableCell38.Weight = 1.058741746999172;
        // 
        // xrTableRow11
        // 
        this.xrTableRow11.Cells.AddRange(new DevExpress.XtraReports.UI.XRTableCell[] {
            this.xrTableCell39,
            this.xrTableCell40,
            this.xrTableCell41,
            this.xrTableCell42});
        this.xrTableRow11.Name = "xrTableRow11";
        this.xrTableRow11.Weight = 1;
        // 
        // xrTableCell39
        // 
        this.xrTableCell39.Borders = DevExpress.XtraPrinting.BorderSide.Bottom;
        this.xrTableCell39.Font = new System.Drawing.Font("Arial", 8F);
        this.xrTableCell39.Name = "xrTableCell39";
        this.xrTableCell39.StylePriority.UseBorders = false;
        this.xrTableCell39.StylePriority.UseFont = false;
        this.xrTableCell39.Text = "  Holidays OT";
        this.xrTableCell39.Weight = 0.54669208704730854;
        // 
        // xrTableCell40
        // 
        this.xrTableCell40.Borders = DevExpress.XtraPrinting.BorderSide.Bottom;
        this.xrTableCell40.Name = "xrTableCell40";
        this.xrTableCell40.StylePriority.UseBorders = false;
        this.xrTableCell40.Text = ":";
        this.xrTableCell40.Weight = 0.064664269590611667;
        // 
        // xrTableCell41
        // 
        this.xrTableCell41.Borders = DevExpress.XtraPrinting.BorderSide.Bottom;
        this.xrTableCell41.DataBindings.AddRange(new DevExpress.XtraReports.UI.XRBinding[] {
            new DevExpress.XtraReports.UI.XRBinding("Text", null, "dtEmpPayslip.HolidayOT")});
        this.xrTableCell41.Font = new System.Drawing.Font("Arial", 9F);
        this.xrTableCell41.Name = "xrTableCell41";
        this.xrTableCell41.StylePriority.UseBorders = false;
        this.xrTableCell41.StylePriority.UseFont = false;
        this.xrTableCell41.StylePriority.UseTextAlignment = false;
        this.xrTableCell41.TextAlignment = DevExpress.XtraPrinting.TextAlignment.TopCenter;
        this.xrTableCell41.Weight = 0.67640487589706921;
        // 
        // xrTableCell42
        // 
        this.xrTableCell42.Borders = DevExpress.XtraPrinting.BorderSide.Bottom;
        this.xrTableCell42.DataBindings.AddRange(new DevExpress.XtraReports.UI.XRBinding[] {
            new DevExpress.XtraReports.UI.XRBinding("Text", null, "dtEmpPayslip.HolidaysOTSal")});
        this.xrTableCell42.Name = "xrTableCell42";
        this.xrTableCell42.StylePriority.UseBorders = false;
        this.xrTableCell42.StylePriority.UseTextAlignment = false;
        this.xrTableCell42.Text = "xrTableCell24";
        this.xrTableCell42.TextAlignment = DevExpress.XtraPrinting.TextAlignment.TopCenter;
        this.xrTableCell42.Weight = 1.058741746999172;
        // 
        // xrTableRow13
        // 
        this.xrTableRow13.Cells.AddRange(new DevExpress.XtraReports.UI.XRTableCell[] {
            this.xrTableCell47,
            this.xrTableCell49});
        this.xrTableRow13.Name = "xrTableRow13";
        this.xrTableRow13.Weight = 1;
        // 
        // xrTableCell47
        // 
        this.xrTableCell47.Font = new System.Drawing.Font("Times New Roman", 9.75F);
        this.xrTableCell47.Name = "xrTableCell47";
        this.xrTableCell47.StylePriority.UseFont = false;
        this.xrTableCell47.StylePriority.UseTextAlignment = false;
        this.xrTableCell47.Text = "                                   GROSS PAY:";
        this.xrTableCell47.TextAlignment = DevExpress.XtraPrinting.TextAlignment.TopRight;
        this.xrTableCell47.Weight = 1.2885204181865895;
        // 
        // xrTableCell49
        // 
        this.xrTableCell49.DataBindings.AddRange(new DevExpress.XtraReports.UI.XRBinding[] {
            new DevExpress.XtraReports.UI.XRBinding("Text", null, "dtEmpPayslip.TotalOTSal")});
        this.xrTableCell49.Font = new System.Drawing.Font("Times New Roman", 9.75F, System.Drawing.FontStyle.Bold);
        this.xrTableCell49.Name = "xrTableCell49";
        this.xrTableCell49.StylePriority.UseFont = false;
        this.xrTableCell49.StylePriority.UseTextAlignment = false;
        this.xrTableCell49.TextAlignment = DevExpress.XtraPrinting.TextAlignment.TopCenter;
        this.xrTableCell49.Weight = 1.0579825613475717;
        // 
        // GroupHeader3
        // 
        this.GroupHeader3.Controls.AddRange(new DevExpress.XtraReports.UI.XRControl[] {
            this.xrPanel9});
        this.GroupHeader3.HeightF = 71.04178F;
        this.GroupHeader3.Level = 2;
        this.GroupHeader3.Name = "GroupHeader3";
        // 
        // xrPanel9
        // 
        this.xrPanel9.Borders = ((DevExpress.XtraPrinting.BorderSide)(((DevExpress.XtraPrinting.BorderSide.Left | DevExpress.XtraPrinting.BorderSide.Right)
                    | DevExpress.XtraPrinting.BorderSide.Bottom)));
        this.xrPanel9.Controls.AddRange(new DevExpress.XtraReports.UI.XRControl[] {
            this.xrTable11});
        this.xrPanel9.LocationFloat = new DevExpress.Utils.PointFloat(0F, 0F);
        this.xrPanel9.Name = "xrPanel9";
        this.xrPanel9.SizeF = new System.Drawing.SizeF(473.6252F, 71.04178F);
        this.xrPanel9.StylePriority.UseBorders = false;
        // 
        // xrTable11
        // 
        this.xrTable11.Borders = DevExpress.XtraPrinting.BorderSide.None;
        this.xrTable11.LocationFloat = new DevExpress.Utils.PointFloat(0F, 0F);
        this.xrTable11.Name = "xrTable11";
        this.xrTable11.Rows.AddRange(new DevExpress.XtraReports.UI.XRTableRow[] {
            this.xrTableRow23,
            this.xrTableRow24,
            this.xrTableRow25,
            this.xrTableRow27,
            this.xrTableRow28,
            this.xrTableRow7});
        this.xrTable11.SizeF = new System.Drawing.SizeF(473.6251F, 71.04178F);
        this.xrTable11.StylePriority.UseBorders = false;
        // 
        // xrTableRow23
        // 
        this.xrTableRow23.Cells.AddRange(new DevExpress.XtraReports.UI.XRTableCell[] {
            this.xrTableCell51,
            this.xrTableCell28,
            this.xrTableCell20});
        this.xrTableRow23.Name = "xrTableRow23";
        this.xrTableRow23.Weight = 1;
        // 
        // xrTableCell51
        // 
        this.xrTableCell51.Borders = DevExpress.XtraPrinting.BorderSide.Bottom;
        this.xrTableCell51.Font = new System.Drawing.Font("Arial", 9.75F, System.Drawing.FontStyle.Bold);
        this.xrTableCell51.Name = "xrTableCell51";
        this.xrTableCell51.StylePriority.UseBorders = false;
        this.xrTableCell51.StylePriority.UseFont = false;
        this.xrTableCell51.Text = " ATTENDANCE";
        this.xrTableCell51.Weight = 0.68329625440029718;
        // 
        // xrTableCell28
        // 
        this.xrTableCell28.Borders = DevExpress.XtraPrinting.BorderSide.Bottom;
        this.xrTableCell28.Name = "xrTableCell28";
        this.xrTableCell28.StylePriority.UseBorders = false;
        this.xrTableCell28.StylePriority.UseTextAlignment = false;
        this.xrTableCell28.Text = "DAYS";
        this.xrTableCell28.TextAlignment = DevExpress.XtraPrinting.TextAlignment.TopCenter;
        this.xrTableCell28.Weight = 0.61174075008503415;
        // 
        // xrTableCell20
        // 
        this.xrTableCell20.Borders = DevExpress.XtraPrinting.BorderSide.Bottom;
        this.xrTableCell20.Name = "xrTableCell20";
        this.xrTableCell20.StylePriority.UseBorders = false;
        this.xrTableCell20.StylePriority.UseTextAlignment = false;
        this.xrTableCell20.Text = "SALARY";
        this.xrTableCell20.TextAlignment = DevExpress.XtraPrinting.TextAlignment.TopCenter;
        this.xrTableCell20.Weight = 1.058741586289669;
        // 
        // xrTableRow24
        // 
        this.xrTableRow24.Cells.AddRange(new DevExpress.XtraReports.UI.XRTableCell[] {
            this.xrTableCell53,
            this.xrTableCell73,
            this.xrTableCell54,
            this.xrTableCell21});
        this.xrTableRow24.Name = "xrTableRow24";
        this.xrTableRow24.Weight = 1;
        // 
        // xrTableCell53
        // 
        this.xrTableCell53.Font = new System.Drawing.Font("Arial", 8F);
        this.xrTableCell53.Name = "xrTableCell53";
        this.xrTableCell53.StylePriority.UseFont = false;
        this.xrTableCell53.Text = "  Woked Days";
        this.xrTableCell53.Weight = 0.55396769828814851;
        // 
        // xrTableCell73
        // 
        this.xrTableCell73.Name = "xrTableCell73";
        this.xrTableCell73.Text = ":";
        this.xrTableCell73.Weight = 0.064664269590611667;
        // 
        // xrTableCell54
        // 
        this.xrTableCell54.DataBindings.AddRange(new DevExpress.XtraReports.UI.XRBinding[] {
            new DevExpress.XtraReports.UI.XRBinding("Text", null, "dtEmpPayslip.DaysPresent")});
        this.xrTableCell54.Font = new System.Drawing.Font("Arial", 9F);
        this.xrTableCell54.Name = "xrTableCell54";
        this.xrTableCell54.StylePriority.UseFont = false;
        this.xrTableCell54.StylePriority.UseTextAlignment = false;
        this.xrTableCell54.TextAlignment = DevExpress.XtraPrinting.TextAlignment.TopCenter;
        this.xrTableCell54.Weight = 0.67640487589706921;
        // 
        // xrTableCell21
        // 
        this.xrTableCell21.DataBindings.AddRange(new DevExpress.XtraReports.UI.XRBinding[] {
            new DevExpress.XtraReports.UI.XRBinding("Text", null, "dtEmpPayslip.WorkedSal")});
        this.xrTableCell21.Name = "xrTableCell21";
        this.xrTableCell21.StylePriority.UseTextAlignment = false;
        this.xrTableCell21.Text = "xrTableCell21";
        this.xrTableCell21.TextAlignment = DevExpress.XtraPrinting.TextAlignment.TopCenter;
        this.xrTableCell21.Weight = 1.058741746999172;
        // 
        // xrTableRow25
        // 
        this.xrTableRow25.Cells.AddRange(new DevExpress.XtraReports.UI.XRTableCell[] {
            this.xrTableCell55,
            this.xrTableCell74,
            this.xrTableCell56,
            this.xrTableCell22});
        this.xrTableRow25.Name = "xrTableRow25";
        this.xrTableRow25.Weight = 1;
        // 
        // xrTableCell55
        // 
        this.xrTableCell55.Font = new System.Drawing.Font("Arial", 8F);
        this.xrTableCell55.Name = "xrTableCell55";
        this.xrTableCell55.StylePriority.UseFont = false;
        this.xrTableCell55.Text = "  Week Off";
        this.xrTableCell55.Weight = 0.55396769828814851;
        // 
        // xrTableCell74
        // 
        this.xrTableCell74.Name = "xrTableCell74";
        this.xrTableCell74.Text = ":";
        this.xrTableCell74.Weight = 0.064664269590611667;
        // 
        // xrTableCell56
        // 
        this.xrTableCell56.DataBindings.AddRange(new DevExpress.XtraReports.UI.XRBinding[] {
            new DevExpress.XtraReports.UI.XRBinding("Text", null, "dtEmpPayslip.WeekOff")});
        this.xrTableCell56.Font = new System.Drawing.Font("Arial", 9F);
        this.xrTableCell56.Name = "xrTableCell56";
        this.xrTableCell56.StylePriority.UseFont = false;
        this.xrTableCell56.StylePriority.UseTextAlignment = false;
        this.xrTableCell56.TextAlignment = DevExpress.XtraPrinting.TextAlignment.TopCenter;
        this.xrTableCell56.Weight = 0.67640487589706921;
        // 
        // xrTableCell22
        // 
        this.xrTableCell22.DataBindings.AddRange(new DevExpress.XtraReports.UI.XRBinding[] {
            new DevExpress.XtraReports.UI.XRBinding("Text", null, "dtEmpPayslip.WeekOffSal")});
        this.xrTableCell22.Name = "xrTableCell22";
        this.xrTableCell22.StylePriority.UseTextAlignment = false;
        this.xrTableCell22.Text = "xrTableCell22";
        this.xrTableCell22.TextAlignment = DevExpress.XtraPrinting.TextAlignment.TopCenter;
        this.xrTableCell22.Weight = 1.058741746999172;
        // 
        // xrTableRow27
        // 
        this.xrTableRow27.Cells.AddRange(new DevExpress.XtraReports.UI.XRTableCell[] {
            this.xrTableCell59,
            this.xrTableCell76,
            this.xrTableCell60,
            this.xrTableCell24});
        this.xrTableRow27.Name = "xrTableRow27";
        this.xrTableRow27.Weight = 1;
        // 
        // xrTableCell59
        // 
        this.xrTableCell59.Font = new System.Drawing.Font("Arial", 8F);
        this.xrTableCell59.Name = "xrTableCell59";
        this.xrTableCell59.StylePriority.UseFont = false;
        this.xrTableCell59.Text = "  Holidays";
        this.xrTableCell59.Weight = 0.55396769828814851;
        // 
        // xrTableCell76
        // 
        this.xrTableCell76.Name = "xrTableCell76";
        this.xrTableCell76.Text = ":";
        this.xrTableCell76.Weight = 0.064664269590611667;
        // 
        // xrTableCell60
        // 
        this.xrTableCell60.DataBindings.AddRange(new DevExpress.XtraReports.UI.XRBinding[] {
            new DevExpress.XtraReports.UI.XRBinding("Text", null, "dtEmpPayslip.Holiday")});
        this.xrTableCell60.Font = new System.Drawing.Font("Arial", 9F);
        this.xrTableCell60.Name = "xrTableCell60";
        this.xrTableCell60.StylePriority.UseFont = false;
        this.xrTableCell60.StylePriority.UseTextAlignment = false;
        this.xrTableCell60.TextAlignment = DevExpress.XtraPrinting.TextAlignment.TopCenter;
        this.xrTableCell60.Weight = 0.67640487589706921;
        // 
        // xrTableCell24
        // 
        this.xrTableCell24.DataBindings.AddRange(new DevExpress.XtraReports.UI.XRBinding[] {
            new DevExpress.XtraReports.UI.XRBinding("Text", null, "dtEmpPayslip.HolidaysSal")});
        this.xrTableCell24.Name = "xrTableCell24";
        this.xrTableCell24.StylePriority.UseTextAlignment = false;
        this.xrTableCell24.Text = "xrTableCell24";
        this.xrTableCell24.TextAlignment = DevExpress.XtraPrinting.TextAlignment.TopCenter;
        this.xrTableCell24.Weight = 1.058741746999172;
        // 
        // xrTableRow28
        // 
        this.xrTableRow28.Cells.AddRange(new DevExpress.XtraReports.UI.XRTableCell[] {
            this.xrTableCell61,
            this.xrTableCell77,
            this.xrTableCell62,
            this.xrTableCell26});
        this.xrTableRow28.Name = "xrTableRow28";
        this.xrTableRow28.Weight = 1;
        // 
        // xrTableCell61
        // 
        this.xrTableCell61.Borders = DevExpress.XtraPrinting.BorderSide.Bottom;
        this.xrTableCell61.Font = new System.Drawing.Font("Arial", 8F);
        this.xrTableCell61.Name = "xrTableCell61";
        this.xrTableCell61.StylePriority.UseBorders = false;
        this.xrTableCell61.StylePriority.UseFont = false;
        this.xrTableCell61.Text = "  Leaves";
        this.xrTableCell61.Weight = 0.55396769828814851;
        // 
        // xrTableCell77
        // 
        this.xrTableCell77.Borders = DevExpress.XtraPrinting.BorderSide.Bottom;
        this.xrTableCell77.Name = "xrTableCell77";
        this.xrTableCell77.StylePriority.UseBorders = false;
        this.xrTableCell77.Text = ":";
        this.xrTableCell77.Weight = 0.064664269590611667;
        // 
        // xrTableCell62
        // 
        this.xrTableCell62.Borders = DevExpress.XtraPrinting.BorderSide.Bottom;
        this.xrTableCell62.DataBindings.AddRange(new DevExpress.XtraReports.UI.XRBinding[] {
            new DevExpress.XtraReports.UI.XRBinding("Text", null, "dtEmpPayslip.Leaves")});
        this.xrTableCell62.Font = new System.Drawing.Font("Arial", 9F);
        this.xrTableCell62.Name = "xrTableCell62";
        this.xrTableCell62.StylePriority.UseBorders = false;
        this.xrTableCell62.StylePriority.UseFont = false;
        this.xrTableCell62.StylePriority.UseTextAlignment = false;
        this.xrTableCell62.TextAlignment = DevExpress.XtraPrinting.TextAlignment.TopCenter;
        this.xrTableCell62.Weight = 0.67640487589706921;
        // 
        // xrTableCell26
        // 
        this.xrTableCell26.Borders = DevExpress.XtraPrinting.BorderSide.Bottom;
        this.xrTableCell26.DataBindings.AddRange(new DevExpress.XtraReports.UI.XRBinding[] {
            new DevExpress.XtraReports.UI.XRBinding("Text", null, "dtEmpPayslip.LeavedaysSal")});
        this.xrTableCell26.Name = "xrTableCell26";
        this.xrTableCell26.StylePriority.UseBorders = false;
        this.xrTableCell26.StylePriority.UseTextAlignment = false;
        this.xrTableCell26.TextAlignment = DevExpress.XtraPrinting.TextAlignment.TopCenter;
        this.xrTableCell26.Weight = 1.058741746999172;
        // 
        // xrTableRow7
        // 
        this.xrTableRow7.Cells.AddRange(new DevExpress.XtraReports.UI.XRTableCell[] {
            this.xrTableCell19,
            this.xrTableCell27});
        this.xrTableRow7.Name = "xrTableRow7";
        this.xrTableRow7.Weight = 1;
        // 
        // xrTableCell19
        // 
        this.xrTableCell19.Font = new System.Drawing.Font("Times New Roman", 9.75F);
        this.xrTableCell19.Name = "xrTableCell19";
        this.xrTableCell19.StylePriority.UseFont = false;
        this.xrTableCell19.StylePriority.UseTextAlignment = false;
        this.xrTableCell19.Text = "                                  GROSS PAY:";
        this.xrTableCell19.TextAlignment = DevExpress.XtraPrinting.TextAlignment.TopRight;
        this.xrTableCell19.Weight = 1.2950370692385937;
        // 
        // xrTableCell27
        // 
        this.xrTableCell27.DataBindings.AddRange(new DevExpress.XtraReports.UI.XRBinding[] {
            new DevExpress.XtraReports.UI.XRBinding("Text", null, "dtEmpPayslip.totalAttendSal")});
        this.xrTableCell27.Font = new System.Drawing.Font("Times New Roman", 9.75F, System.Drawing.FontStyle.Bold);
        this.xrTableCell27.Name = "xrTableCell27";
        this.xrTableCell27.StylePriority.UseFont = false;
        this.xrTableCell27.StylePriority.UseTextAlignment = false;
        this.xrTableCell27.TextAlignment = DevExpress.XtraPrinting.TextAlignment.TopCenter;
        this.xrTableCell27.Weight = 1.0587415215364076;
        // 
        // GroupHeader4
        // 
        this.GroupHeader4.Controls.AddRange(new DevExpress.XtraReports.UI.XRControl[] {
            this.xrPanel1});
        this.GroupHeader4.GroupFields.AddRange(new DevExpress.XtraReports.UI.GroupField[] {
            new DevExpress.XtraReports.UI.GroupField("EmpCode", DevExpress.XtraReports.UI.XRColumnSortOrder.Ascending)});
        this.GroupHeader4.HeightF = 86.25005F;
        this.GroupHeader4.Level = 3;
        this.GroupHeader4.Name = "GroupHeader4";
        // 
        // xrPanel1
        // 
        this.xrPanel1.Borders = ((DevExpress.XtraPrinting.BorderSide)((((DevExpress.XtraPrinting.BorderSide.Left | DevExpress.XtraPrinting.BorderSide.Top)
                    | DevExpress.XtraPrinting.BorderSide.Right)
                    | DevExpress.XtraPrinting.BorderSide.Bottom)));
        this.xrPanel1.Controls.AddRange(new DevExpress.XtraReports.UI.XRControl[] {
            this.xrPictureBox2,
            this.xrLabel6,
            this.xrLabel5,
            this.xrTable1,
            this.xrLabel7});
        this.xrPanel1.LocationFloat = new DevExpress.Utils.PointFloat(0F, 0F);
        this.xrPanel1.Name = "xrPanel1";
        this.xrPanel1.SizeF = new System.Drawing.SizeF(473.6251F, 86.25005F);
        this.xrPanel1.StylePriority.UseBorders = false;
        // 
        // xrPictureBox2
        // 
        this.xrPictureBox2.Borders = DevExpress.XtraPrinting.BorderSide.None;
        this.xrPictureBox2.LocationFloat = new DevExpress.Utils.PointFloat(407.222F, 32.15281F);
        this.xrPictureBox2.Name = "xrPictureBox2";
        this.xrPictureBox2.SizeF = new System.Drawing.SizeF(60.41666F, 29.95836F);
        this.xrPictureBox2.Sizing = DevExpress.XtraPrinting.ImageSizeMode.StretchImage;
        this.xrPictureBox2.StylePriority.UseBorders = false;
        // 
        // xrLabel6
        // 
        this.xrLabel6.Borders = DevExpress.XtraPrinting.BorderSide.None;
        this.xrLabel6.DataBindings.AddRange(new DevExpress.XtraReports.UI.XRBinding[] {
            new DevExpress.XtraReports.UI.XRBinding("Text", null, "dtEmpPayslip.Month")});
        this.xrLabel6.Font = new System.Drawing.Font("Arial", 10F, System.Drawing.FontStyle.Bold);
        this.xrLabel6.LocationFloat = new DevExpress.Utils.PointFloat(318.8232F, 9.572817F);
        this.xrLabel6.Name = "xrLabel6";
        this.xrLabel6.Padding = new DevExpress.XtraPrinting.PaddingInfo(2, 2, 0, 0, 100F);
        this.xrLabel6.SizeF = new System.Drawing.SizeF(55.98291F, 12.58333F);
        this.xrLabel6.StylePriority.UseBorders = false;
        this.xrLabel6.StylePriority.UseFont = false;
        this.xrLabel6.Text = "xrLabel6";
        // 
        // xrLabel5
        // 
        this.xrLabel5.Borders = DevExpress.XtraPrinting.BorderSide.None;
        this.xrLabel5.Font = new System.Drawing.Font("Arial", 10F, System.Drawing.FontStyle.Bold);
        this.xrLabel5.LocationFloat = new DevExpress.Utils.PointFloat(111.2089F, 9.572817F);
        this.xrLabel5.Name = "xrLabel5";
        this.xrLabel5.Padding = new DevExpress.XtraPrinting.PaddingInfo(2, 2, 0, 0, 100F);
        this.xrLabel5.SizeF = new System.Drawing.SizeF(207.6142F, 12.58F);
        this.xrLabel5.StylePriority.UseBorders = false;
        this.xrLabel5.StylePriority.UseFont = false;
        this.xrLabel5.Text = "PAYSLIP FOR THE MONTH OF";
        // 
        // xrTable1
        // 
        this.xrTable1.Borders = DevExpress.XtraPrinting.BorderSide.None;
        this.xrTable1.LocationFloat = new DevExpress.Utils.PointFloat(9.916733F, 22.1667F);
        this.xrTable1.Name = "xrTable1";
        this.xrTable1.Rows.AddRange(new DevExpress.XtraReports.UI.XRTableRow[] {
            this.xrTableRow1,
            this.xrTableRow2,
            this.xrTableRow4,
            this.xrTableRow3,
            this.xrTableRow5,
            this.xrTableRow6});
        this.xrTable1.SizeF = new System.Drawing.SizeF(364.2723F, 59.9167F);
        this.xrTable1.StylePriority.UseBorders = false;
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
        this.xrTableCell1.Font = new System.Drawing.Font("Arial", 9F, System.Drawing.FontStyle.Bold);
        this.xrTableCell1.Name = "xrTableCell1";
        this.xrTableCell1.StylePriority.UseFont = false;
        this.xrTableCell1.Text = "EMPLOYEE CODE";
        this.xrTableCell1.Weight = 0.25692606735088941;
        // 
        // xrTableCell2
        // 
        this.xrTableCell2.Name = "xrTableCell2";
        this.xrTableCell2.Text = ":";
        this.xrTableCell2.Weight = 0.025509254301800344;
        // 
        // xrTableCell3
        // 
        this.xrTableCell3.DataBindings.AddRange(new DevExpress.XtraReports.UI.XRBinding[] {
            new DevExpress.XtraReports.UI.XRBinding("Text", null, "dtEmpPayslip.EmpCode")});
        this.xrTableCell3.Font = new System.Drawing.Font("Arial", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
        this.xrTableCell3.Name = "xrTableCell3";
        this.xrTableCell3.StylePriority.UseFont = false;
        this.xrTableCell3.Weight = 0.2452364633721415;
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
        this.xrTableCell4.Borders = DevExpress.XtraPrinting.BorderSide.None;
        this.xrTableCell4.Font = new System.Drawing.Font("Arial", 9F, System.Drawing.FontStyle.Bold);
        this.xrTableCell4.Name = "xrTableCell4";
        this.xrTableCell4.StylePriority.UseBorders = false;
        this.xrTableCell4.StylePriority.UseFont = false;
        this.xrTableCell4.Text = "EMPLOYEE NAME";
        this.xrTableCell4.Weight = 0.25692606532795614;
        // 
        // xrTableCell5
        // 
        this.xrTableCell5.Name = "xrTableCell5";
        this.xrTableCell5.Text = ":";
        this.xrTableCell5.Weight = 0.026470578361171504;
        // 
        // xrTableCell6
        // 
        this.xrTableCell6.DataBindings.AddRange(new DevExpress.XtraReports.UI.XRBinding[] {
            new DevExpress.XtraReports.UI.XRBinding("Text", null, "dtEmpPayslip.EmpName")});
        this.xrTableCell6.Font = new System.Drawing.Font("Arial", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
        this.xrTableCell6.Name = "xrTableCell6";
        this.xrTableCell6.StylePriority.UseFont = false;
        this.xrTableCell6.Weight = 0.24427514133570366;
        // 
        // xrTableRow4
        // 
        this.xrTableRow4.Cells.AddRange(new DevExpress.XtraReports.UI.XRTableCell[] {
            this.xrTableCell10,
            this.xrTableCell11,
            this.xrTableCell9});
        this.xrTableRow4.Name = "xrTableRow4";
        this.xrTableRow4.Weight = 1;
        // 
        // xrTableCell10
        // 
        this.xrTableCell10.Font = new System.Drawing.Font("Arial", 9F, System.Drawing.FontStyle.Bold);
        this.xrTableCell10.Name = "xrTableCell10";
        this.xrTableCell10.StylePriority.UseFont = false;
        this.xrTableCell10.Text = "DESIGNATION";
        this.xrTableCell10.Weight = 0.25692606532795614;
        // 
        // xrTableCell11
        // 
        this.xrTableCell11.Name = "xrTableCell11";
        this.xrTableCell11.Text = ":";
        this.xrTableCell11.Weight = 0.026470578361171504;
        // 
        // xrTableCell9
        // 
        this.xrTableCell9.DataBindings.AddRange(new DevExpress.XtraReports.UI.XRBinding[] {
            new DevExpress.XtraReports.UI.XRBinding("Text", null, "dtEmpPayslip.Designation")});
        this.xrTableCell9.Font = new System.Drawing.Font("Arial", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
        this.xrTableCell9.Name = "xrTableCell9";
        this.xrTableCell9.StylePriority.UseFont = false;
        this.xrTableCell9.Weight = 0.24427514133570366;
        // 
        // xrTableRow3
        // 
        this.xrTableRow3.Cells.AddRange(new DevExpress.XtraReports.UI.XRTableCell[] {
            this.xrTableCell7,
            this.xrTableCell8,
            this.xrTableCell12});
        this.xrTableRow3.Name = "xrTableRow3";
        this.xrTableRow3.Weight = 1;
        // 
        // xrTableCell7
        // 
        this.xrTableCell7.Font = new System.Drawing.Font("Arial", 9F, System.Drawing.FontStyle.Bold);
        this.xrTableCell7.Name = "xrTableCell7";
        this.xrTableCell7.StylePriority.UseFont = false;
        this.xrTableCell7.Text = "DEPARTMENT";
        this.xrTableCell7.Weight = 0.25692606532795614;
        // 
        // xrTableCell8
        // 
        this.xrTableCell8.Name = "xrTableCell8";
        this.xrTableCell8.Text = ":";
        this.xrTableCell8.Weight = 0.026470578361171504;
        // 
        // xrTableCell12
        // 
        this.xrTableCell12.DataBindings.AddRange(new DevExpress.XtraReports.UI.XRBinding[] {
            new DevExpress.XtraReports.UI.XRBinding("Text", null, "dtEmpPayslip.Department")});
        this.xrTableCell12.Font = new System.Drawing.Font("Arial", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
        this.xrTableCell12.Name = "xrTableCell12";
        this.xrTableCell12.StylePriority.UseFont = false;
        this.xrTableCell12.Weight = 0.24427514133570366;
        // 
        // xrTableRow5
        // 
        this.xrTableRow5.Cells.AddRange(new DevExpress.XtraReports.UI.XRTableCell[] {
            this.xrTableCell13,
            this.xrTableCell14,
            this.xrTableCell15});
        this.xrTableRow5.Name = "xrTableRow5";
        this.xrTableRow5.Weight = 1;
        // 
        // xrTableCell13
        // 
        this.xrTableCell13.Font = new System.Drawing.Font("Arial", 9F, System.Drawing.FontStyle.Bold);
        this.xrTableCell13.Name = "xrTableCell13";
        this.xrTableCell13.StylePriority.UseFont = false;
        this.xrTableCell13.Text = "DOJ";
        this.xrTableCell13.Weight = 0.25692606532795614;
        // 
        // xrTableCell14
        // 
        this.xrTableCell14.Name = "xrTableCell14";
        this.xrTableCell14.Text = ":";
        this.xrTableCell14.Weight = 0.026470578361171504;
        // 
        // xrTableCell15
        // 
        this.xrTableCell15.DataBindings.AddRange(new DevExpress.XtraReports.UI.XRBinding[] {
            new DevExpress.XtraReports.UI.XRBinding("Text", null, "dtEmpPayslip.DOJ")});
        this.xrTableCell15.Font = new System.Drawing.Font("Arial", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
        this.xrTableCell15.Name = "xrTableCell15";
        this.xrTableCell15.StylePriority.UseFont = false;
        this.xrTableCell15.Weight = 0.24427514133570366;
        // 
        // xrTableRow6
        // 
        this.xrTableRow6.Cells.AddRange(new DevExpress.XtraReports.UI.XRTableCell[] {
            this.xrTableCell16,
            this.xrTableCell17,
            this.xrTableCell18});
        this.xrTableRow6.Name = "xrTableRow6";
        this.xrTableRow6.Weight = 1;
        // 
        // xrTableCell16
        // 
        this.xrTableCell16.Font = new System.Drawing.Font("Arial", 9F, System.Drawing.FontStyle.Bold);
        this.xrTableCell16.Name = "xrTableCell16";
        this.xrTableCell16.StylePriority.UseFont = false;
        this.xrTableCell16.Text = "BANK A/C NO.";
        this.xrTableCell16.Weight = 0.25692606532795614;
        // 
        // xrTableCell17
        // 
        this.xrTableCell17.Name = "xrTableCell17";
        this.xrTableCell17.Text = ":";
        this.xrTableCell17.Weight = 0.026470578361171504;
        // 
        // xrTableCell18
        // 
        this.xrTableCell18.DataBindings.AddRange(new DevExpress.XtraReports.UI.XRBinding[] {
            new DevExpress.XtraReports.UI.XRBinding("Text", null, "dtEmpPayslip.BankAccountNo")});
        this.xrTableCell18.Font = new System.Drawing.Font("Arial", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
        this.xrTableCell18.Name = "xrTableCell18";
        this.xrTableCell18.StylePriority.UseFont = false;
        this.xrTableCell18.Weight = 0.24427514133570366;
        // 
        // xrLabel7
        // 
        this.xrLabel7.Borders = DevExpress.XtraPrinting.BorderSide.None;
        this.xrLabel7.DataBindings.AddRange(new DevExpress.XtraReports.UI.XRBinding[] {
            new DevExpress.XtraReports.UI.XRBinding("Text", null, "dtEmpPayslip.Year")});
        this.xrLabel7.Font = new System.Drawing.Font("Arial", 10F, System.Drawing.FontStyle.Bold);
        this.xrLabel7.LocationFloat = new DevExpress.Utils.PointFloat(365.8F, 10.00334F);
        this.xrLabel7.Name = "xrLabel7";
        this.xrLabel7.Padding = new DevExpress.XtraPrinting.PaddingInfo(2, 2, 0, 0, 100F);
        this.xrLabel7.SizeF = new System.Drawing.SizeF(44.16653F, 12.58334F);
        this.xrLabel7.StylePriority.UseBorders = false;
        this.xrLabel7.StylePriority.UseFont = false;
        this.xrLabel7.Text = "xrLabel7";
        // 
        // employeePaySlipDataSet1
        // 
        this.employeePaySlipDataSet1.DataSetName = "EmployeePaySlipDataSet";
        this.employeePaySlipDataSet1.SchemaSerializationMode = System.Data.SchemaSerializationMode.IncludeSchema;
        // 
        // ReportFooter
        // 
        this.ReportFooter.Controls.AddRange(new DevExpress.XtraReports.UI.XRControl[] {
            this.xrPageBreak1});
        this.ReportFooter.HeightF = 3.125F;
        this.ReportFooter.Name = "ReportFooter";
        // 
        // xrTable5
        // 
        this.xrTable5.Borders = ((DevExpress.XtraPrinting.BorderSide)(((DevExpress.XtraPrinting.BorderSide.Left | DevExpress.XtraPrinting.BorderSide.Right)
                    | DevExpress.XtraPrinting.BorderSide.Bottom)));
        this.xrTable5.LocationFloat = new DevExpress.Utils.PointFloat(1.589457E-05F, 0F);
        this.xrTable5.Name = "xrTable5";
        this.xrTable5.Rows.AddRange(new DevExpress.XtraReports.UI.XRTableRow[] {
            this.xrTableRow19,
            this.xrTableRow20,
            this.xrTableRow22,
            this.xrTableRow35});
        this.xrTable5.SizeF = new System.Drawing.SizeF(473.6252F, 68.33327F);
        this.xrTable5.StylePriority.UseBorders = false;
        // 
        // xrTableRow19
        // 
        this.xrTableRow19.Cells.AddRange(new DevExpress.XtraReports.UI.XRTableCell[] {
            this.xrTableCell80});
        this.xrTableRow19.Name = "xrTableRow19";
        this.xrTableRow19.Weight = 1;
        // 
        // xrTableCell80
        // 
        this.xrTableCell80.Borders = ((DevExpress.XtraPrinting.BorderSide)((DevExpress.XtraPrinting.BorderSide.Left | DevExpress.XtraPrinting.BorderSide.Right)));
        this.xrTableCell80.Controls.AddRange(new DevExpress.XtraReports.UI.XRControl[] {
            this.xrLabel9,
            this.xrLabel8});
        this.xrTableCell80.Name = "xrTableCell80";
        this.xrTableCell80.StylePriority.UseBorders = false;
        this.xrTableCell80.Weight = 3;
        // 
        // xrLabel9
        // 
        this.xrLabel9.Borders = DevExpress.XtraPrinting.BorderSide.None;
        this.xrLabel9.DataBindings.AddRange(new DevExpress.XtraReports.UI.XRBinding[] {
            new DevExpress.XtraReports.UI.XRBinding("Text", null, "dtEmpPayslip.NetSalary")});
        this.xrLabel9.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, ((System.Drawing.FontStyle)((System.Drawing.FontStyle.Bold | System.Drawing.FontStyle.Underline))));
        this.xrLabel9.LocationFloat = new DevExpress.Utils.PointFloat(363.3314F, 0F);
        this.xrLabel9.Name = "xrLabel9";
        this.xrLabel9.Padding = new DevExpress.XtraPrinting.PaddingInfo(2, 2, 0, 0, 100F);
        this.xrLabel9.SizeF = new System.Drawing.SizeF(97.92313F, 14.99999F);
        this.xrLabel9.StylePriority.UseBorders = false;
        this.xrLabel9.StylePriority.UseFont = false;
        this.xrLabel9.Text = "xrLabel9";
        // 
        // xrLabel8
        // 
        this.xrLabel8.Borders = DevExpress.XtraPrinting.BorderSide.None;
        this.xrLabel8.Font = new System.Drawing.Font("Arial", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
        this.xrLabel8.LocationFloat = new DevExpress.Utils.PointFloat(279.998F, 2.083333F);
        this.xrLabel8.Name = "xrLabel8";
        this.xrLabel8.Padding = new DevExpress.XtraPrinting.PaddingInfo(2, 2, 0, 0, 100F);
        this.xrLabel8.SizeF = new System.Drawing.SizeF(83.3334F, 14.99999F);
        this.xrLabel8.StylePriority.UseBorders = false;
        this.xrLabel8.StylePriority.UseFont = false;
        this.xrLabel8.Text = "Net Salary:";
        // 
        // xrTableRow22
        // 
        this.xrTableRow22.Cells.AddRange(new DevExpress.XtraReports.UI.XRTableCell[] {
            this.xrTableCell81});
        this.xrTableRow22.Name = "xrTableRow22";
        this.xrTableRow22.Weight = 1;
        // 
        // xrTableCell81
        // 
        this.xrTableCell81.Controls.AddRange(new DevExpress.XtraReports.UI.XRControl[] {
            this.xrLabel21,
            this.xrLabel20});
        this.xrTableCell81.Name = "xrTableCell81";
        this.xrTableCell81.Weight = 3;
        // 
        // xrLabel21
        // 
        this.xrLabel21.Borders = DevExpress.XtraPrinting.BorderSide.None;
        this.xrLabel21.Font = new System.Drawing.Font("Arial", 9F);
        this.xrLabel21.LocationFloat = new DevExpress.Utils.PointFloat(307.0688F, 4.196167E-05F);
        this.xrLabel21.Name = "xrLabel21";
        this.xrLabel21.Padding = new DevExpress.XtraPrinting.PaddingInfo(2, 2, 0, 0, 96F);
        this.xrLabel21.SizeF = new System.Drawing.SizeF(166.5562F, 17.49999F);
        this.xrLabel21.StylePriority.UseBorders = false;
        this.xrLabel21.StylePriority.UseFont = false;
        this.xrLabel21.Text = "EMPLOYEE SIGNATURE";
        // 
        // xrLabel20
        // 
        this.xrLabel20.Borders = DevExpress.XtraPrinting.BorderSide.None;
        this.xrLabel20.Font = new System.Drawing.Font("Arial", 9F);
        this.xrLabel20.LocationFloat = new DevExpress.Utils.PointFloat(1.125129F, 8.392334E-05F);
        this.xrLabel20.Name = "xrLabel20";
        this.xrLabel20.Padding = new DevExpress.XtraPrinting.PaddingInfo(2, 2, 0, 0, 96F);
        this.xrLabel20.SizeF = new System.Drawing.SizeF(173.3749F, 17.49999F);
        this.xrLabel20.StylePriority.UseBorders = false;
        this.xrLabel20.StylePriority.UseFont = false;
        this.xrLabel20.Text = "  HR SIGNATURE";
        // 
        // xrTableRow35
        // 
        this.xrTableRow35.Cells.AddRange(new DevExpress.XtraReports.UI.XRTableCell[] {
            this.xrTableCell83});
        this.xrTableRow35.Name = "xrTableRow35";
        this.xrTableRow35.Weight = 1;
        // 
        // xrTableCell83
        // 
        this.xrTableCell83.Borders = ((DevExpress.XtraPrinting.BorderSide)((((DevExpress.XtraPrinting.BorderSide.Left | DevExpress.XtraPrinting.BorderSide.Top)
                    | DevExpress.XtraPrinting.BorderSide.Right)
                    | DevExpress.XtraPrinting.BorderSide.Bottom)));
        this.xrTableCell83.Name = "xrTableCell83";
        this.xrTableCell83.StylePriority.UseBorders = false;
        this.xrTableCell83.Weight = 3;
        // 
        // GroupFooter1
        // 
        this.GroupFooter1.Controls.AddRange(new DevExpress.XtraReports.UI.XRControl[] {
            this.xrTable5});
        this.GroupFooter1.HeightF = 68.75F;
        this.GroupFooter1.Name = "GroupFooter1";
        // 
        // xrTableRow20
        // 
        this.xrTableRow20.Cells.AddRange(new DevExpress.XtraReports.UI.XRTableCell[] {
            this.xrTableCell25});
        this.xrTableRow20.Name = "xrTableRow20";
        this.xrTableRow20.Weight = 1;
        // 
        // xrTableCell25
        // 
        this.xrTableCell25.Borders = ((DevExpress.XtraPrinting.BorderSide)((DevExpress.XtraPrinting.BorderSide.Left | DevExpress.XtraPrinting.BorderSide.Right)));
        this.xrTableCell25.Name = "xrTableCell25";
        this.xrTableCell25.StylePriority.UseBorders = false;
        this.xrTableCell25.Weight = 3;
        // 
        // xrPageBreak1
        // 
        this.xrPageBreak1.LocationFloat = new DevExpress.Utils.PointFloat(0F, 0F);
        this.xrPageBreak1.Name = "xrPageBreak1";
        // 
        // xrPanel2
        // 
        this.xrPanel2.Borders = ((DevExpress.XtraPrinting.BorderSide)((((DevExpress.XtraPrinting.BorderSide.Left | DevExpress.XtraPrinting.BorderSide.Top)
                    | DevExpress.XtraPrinting.BorderSide.Right)
                    | DevExpress.XtraPrinting.BorderSide.Bottom)));
        this.xrPanel2.Controls.AddRange(new DevExpress.XtraReports.UI.XRControl[] {
            this.xrLabel1,
            this.xrLabel2,
            this.xrLabel3,
            this.xrLabel4,
            this.xrLabel10,
            this.xrPictureBox1,
            this.xrLabel11});
        this.xrPanel2.LocationFloat = new DevExpress.Utils.PointFloat(0F, 13.37504F);
        this.xrPanel2.Name = "xrPanel2";
        this.xrPanel2.SizeF = new System.Drawing.SizeF(473.6252F, 107.75F);
        this.xrPanel2.StylePriority.UseBorders = false;
        // 
        // xrLabel1
        // 
        this.xrLabel1.Borders = DevExpress.XtraPrinting.BorderSide.None;
        this.xrLabel1.Font = new System.Drawing.Font("Verdana", 6F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
        this.xrLabel1.LocationFloat = new DevExpress.Utils.PointFloat(9.916718F, 66.41667F);
        this.xrLabel1.Name = "xrLabel1";
        this.xrLabel1.Padding = new DevExpress.XtraPrinting.PaddingInfo(2, 2, 0, 0, 100F);
        this.xrLabel1.SizeF = new System.Drawing.SizeF(164.5833F, 10.49997F);
        this.xrLabel1.StylePriority.UseBorders = false;
        this.xrLabel1.StylePriority.UseFont = false;
        // 
        // xrLabel2
        // 
        this.xrLabel2.Borders = DevExpress.XtraPrinting.BorderSide.None;
        this.xrLabel2.Font = new System.Drawing.Font("Verdana", 6F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
        this.xrLabel2.LocationFloat = new DevExpress.Utils.PointFloat(282.1974F, 66.41664F);
        this.xrLabel2.Name = "xrLabel2";
        this.xrLabel2.Padding = new DevExpress.XtraPrinting.PaddingInfo(2, 2, 0, 0, 100F);
        this.xrLabel2.SizeF = new System.Drawing.SizeF(166.1884F, 10.49998F);
        this.xrLabel2.StylePriority.UseBorders = false;
        this.xrLabel2.StylePriority.UseFont = false;
        // 
        // xrLabel3
        // 
        this.xrLabel3.Borders = DevExpress.XtraPrinting.BorderSide.None;
        this.xrLabel3.Font = new System.Drawing.Font("Verdana", 6.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
        this.xrLabel3.LocationFloat = new DevExpress.Utils.PointFloat(281.1533F, 51.75002F);
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
        this.xrLabel4.LocationFloat = new DevExpress.Utils.PointFloat(338.8771F, 51.75001F);
        this.xrLabel4.Name = "xrLabel4";
        this.xrLabel4.Padding = new DevExpress.XtraPrinting.PaddingInfo(2, 2, 0, 0, 100F);
        this.xrLabel4.SizeF = new System.Drawing.SizeF(114.6494F, 14.66664F);
        this.xrLabel4.StylePriority.UseBorders = false;
        this.xrLabel4.StylePriority.UseFont = false;
        // 
        // xrLabel10
        // 
        this.xrLabel10.Borders = DevExpress.XtraPrinting.BorderSide.None;
        this.xrLabel10.Font = new System.Drawing.Font("Verdana", 6.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
        this.xrLabel10.LocationFloat = new DevExpress.Utils.PointFloat(281.1533F, 41.33334F);
        this.xrLabel10.Name = "xrLabel10";
        this.xrLabel10.Padding = new DevExpress.XtraPrinting.PaddingInfo(2, 2, 0, 0, 100F);
        this.xrLabel10.SizeF = new System.Drawing.SizeF(178.9761F, 10.41668F);
        this.xrLabel10.StylePriority.UseBorders = false;
        this.xrLabel10.StylePriority.UseFont = false;
        this.xrLabel10.StylePriority.UseTextAlignment = false;
        this.xrLabel10.TextAlignment = DevExpress.XtraPrinting.TextAlignment.TopLeft;
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
        // xrLabel11
        // 
        this.xrLabel11.Borders = DevExpress.XtraPrinting.BorderSide.None;
        this.xrLabel11.Font = new System.Drawing.Font("Verdana", 10F, ((System.Drawing.FontStyle)((System.Drawing.FontStyle.Bold | System.Drawing.FontStyle.Underline))));
        this.xrLabel11.LocationFloat = new DevExpress.Utils.PointFloat(82.91696F, 28.75001F);
        this.xrLabel11.Name = "xrLabel11";
        this.xrLabel11.Padding = new DevExpress.XtraPrinting.PaddingInfo(2, 2, 0, 0, 100F);
        this.xrLabel11.SizeF = new System.Drawing.SizeF(276.0157F, 12.58334F);
        this.xrLabel11.StylePriority.UseBorders = false;
        this.xrLabel11.StylePriority.UseFont = false;
        this.xrLabel11.StylePriority.UseTextAlignment = false;
        this.xrLabel11.TextAlignment = DevExpress.XtraPrinting.TextAlignment.TopCenter;
        // 
        // Employee_Attendance_Report
        // 
        this.Bands.AddRange(new DevExpress.XtraReports.UI.Band[] {
            this.Detail,
            this.TopMargin,
            this.BottomMargin,
            this.GroupHeader1,
            this.GroupHeader2,
            this.GroupHeader3,
            this.GroupHeader4,
            this.ReportFooter,
            this.GroupFooter1});
        this.DataMember = "dtEmpPayslip";
        this.DataSource = this.employeePaySlipDataSet1;
        this.Margins = new System.Drawing.Printing.Margins(161, 131, 121, 0);
        this.Version = "10.2";
        ((System.ComponentModel.ISupportInitialize)(this.xrTable3)).EndInit();
        ((System.ComponentModel.ISupportInitialize)(this.xrTable2)).EndInit();
        ((System.ComponentModel.ISupportInitialize)(this.xrTable11)).EndInit();
        ((System.ComponentModel.ISupportInitialize)(this.xrTable1)).EndInit();
        ((System.ComponentModel.ISupportInitialize)(this.employeePaySlipDataSet1)).EndInit();
        ((System.ComponentModel.ISupportInitialize)(this.xrTable5)).EndInit();
        ((System.ComponentModel.ISupportInitialize)(this)).EndInit();

	}

	#endregion
}
