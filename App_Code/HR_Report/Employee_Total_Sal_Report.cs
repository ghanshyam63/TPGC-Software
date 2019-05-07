using System;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using DevExpress.XtraReports.UI;

/// <summary>
/// Summary description for Employee_Total_Sal_Report
/// </summary>
public class Employee_Total_Sal_Report : DevExpress.XtraReports.UI.XtraReport
{
	private DevExpress.XtraReports.UI.DetailBand Detail;
	private DevExpress.XtraReports.UI.TopMarginBand TopMargin;
	private DevExpress.XtraReports.UI.BottomMarginBand BottomMargin;
    private GroupHeaderBand GroupHeader1;
    private EmployeePaySlipDataSet employeePaySlipDataSet1;
    private GroupHeaderBand GroupHeader2;
    private XRPanel xrPanel3;
    private XRTable xrTable2;
    private XRTableRow xrTableRow7;
    private XRTableCell xrTableCell19;
    private XRTableRow xrTableRow8;
    private XRTableCell xrTableCell22;
    private XRTableCell xrTableCell23;
    private XRTableCell xrTableCell24;
    private XRTableRow xrTableRow9;
    private XRTableCell xrTableCell25;
    private XRTableCell xrTableCell26;
    private XRTableCell xrTableCell27;
    private XRTableRow xrTableRow10;
    private XRTableCell xrTableCell28;
    private XRTableCell xrTableCell29;
    private XRTableCell xrTableCell30;
    private XRTableCell xrTableCell32;
    private XRTableCell xrTableCell33;
    private XRTableCell xrTableCell34;
    private XRTableRow xrTableRow11;
    private XRTableCell xrTableCell20;
    private XRTableCell xrTableCell21;
    private XRTableCell xrTableCell31;
    private XRTableRow xrTableRow12;
    private XRTableCell xrTableCell36;
    private XRTableCell xrTableCell39;
    private XRTableRow xrTableRow13;
    private XRTableCell xrTableCell41;
    private XRTableCell xrTableCell35;
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

    public Employee_Total_Sal_Report()
    {
        InitializeComponent();
        //
        // TODO: Add constructor logic here
        //



    }

    public void setimage(string url)
    {
        xrPictureBox1.ImageUrl = url;
        //xrPictureBox2.ImageUrl = url;
    }
    public void setempimage(string urlemp)
    {
        xrPictureBox2.ImageUrl = urlemp;
        //xrPictureBox2.ImageUrl = url;
    }


    public void setcompanyname(string companyname)
    {
        xrLabel11.Text = companyname;
        // xrLabel13.Text = companyname;
    }
    public void setcompanyAddress(string Address)
    {
        xrLabel10.Text = Address;
        // xrLabel12.Text = Address;
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
        string resourceFileName = "Employee_Total_Sal_Report.resx";
        this.Detail = new DevExpress.XtraReports.UI.DetailBand();
        this.TopMargin = new DevExpress.XtraReports.UI.TopMarginBand();
        this.BottomMargin = new DevExpress.XtraReports.UI.BottomMarginBand();
        this.GroupHeader1 = new DevExpress.XtraReports.UI.GroupHeaderBand();
        this.xrPanel3 = new DevExpress.XtraReports.UI.XRPanel();
        this.xrTable2 = new DevExpress.XtraReports.UI.XRTable();
        this.xrTableRow7 = new DevExpress.XtraReports.UI.XRTableRow();
        this.xrTableCell19 = new DevExpress.XtraReports.UI.XRTableCell();
        this.xrTableRow8 = new DevExpress.XtraReports.UI.XRTableRow();
        this.xrTableCell22 = new DevExpress.XtraReports.UI.XRTableCell();
        this.xrTableCell23 = new DevExpress.XtraReports.UI.XRTableCell();
        this.xrTableCell24 = new DevExpress.XtraReports.UI.XRTableCell();
        this.xrTableCell32 = new DevExpress.XtraReports.UI.XRTableCell();
        this.xrTableRow9 = new DevExpress.XtraReports.UI.XRTableRow();
        this.xrTableCell25 = new DevExpress.XtraReports.UI.XRTableCell();
        this.xrTableCell26 = new DevExpress.XtraReports.UI.XRTableCell();
        this.xrTableCell27 = new DevExpress.XtraReports.UI.XRTableCell();
        this.xrTableCell33 = new DevExpress.XtraReports.UI.XRTableCell();
        this.xrTableRow10 = new DevExpress.XtraReports.UI.XRTableRow();
        this.xrTableCell28 = new DevExpress.XtraReports.UI.XRTableCell();
        this.xrTableCell29 = new DevExpress.XtraReports.UI.XRTableCell();
        this.xrTableCell30 = new DevExpress.XtraReports.UI.XRTableCell();
        this.xrTableCell34 = new DevExpress.XtraReports.UI.XRTableCell();
        this.xrTableRow11 = new DevExpress.XtraReports.UI.XRTableRow();
        this.xrTableCell20 = new DevExpress.XtraReports.UI.XRTableCell();
        this.xrTableCell21 = new DevExpress.XtraReports.UI.XRTableCell();
        this.xrTableCell31 = new DevExpress.XtraReports.UI.XRTableCell();
        this.xrTableCell35 = new DevExpress.XtraReports.UI.XRTableCell();
        this.xrTableRow12 = new DevExpress.XtraReports.UI.XRTableRow();
        this.xrTableCell36 = new DevExpress.XtraReports.UI.XRTableCell();
        this.xrTableCell39 = new DevExpress.XtraReports.UI.XRTableCell();
        this.xrTableRow13 = new DevExpress.XtraReports.UI.XRTableRow();
        this.xrTableCell41 = new DevExpress.XtraReports.UI.XRTableCell();
        this.employeePaySlipDataSet1 = new EmployeePaySlipDataSet();
        this.GroupHeader2 = new DevExpress.XtraReports.UI.GroupHeaderBand();
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
        this.xrPanel2 = new DevExpress.XtraReports.UI.XRPanel();
        this.xrLabel1 = new DevExpress.XtraReports.UI.XRLabel();
        this.xrLabel2 = new DevExpress.XtraReports.UI.XRLabel();
        this.xrLabel3 = new DevExpress.XtraReports.UI.XRLabel();
        this.xrLabel4 = new DevExpress.XtraReports.UI.XRLabel();
        this.xrLabel10 = new DevExpress.XtraReports.UI.XRLabel();
        this.xrPictureBox1 = new DevExpress.XtraReports.UI.XRPictureBox();
        this.xrLabel11 = new DevExpress.XtraReports.UI.XRLabel();
        ((System.ComponentModel.ISupportInitialize)(this.xrTable2)).BeginInit();
        ((System.ComponentModel.ISupportInitialize)(this.employeePaySlipDataSet1)).BeginInit();
        ((System.ComponentModel.ISupportInitialize)(this.xrTable1)).BeginInit();
        ((System.ComponentModel.ISupportInitialize)(this)).BeginInit();
        // 
        // Detail
        // 
        this.Detail.HeightF = 3.125F;
        this.Detail.Name = "Detail";
        this.Detail.Padding = new DevExpress.XtraPrinting.PaddingInfo(0, 0, 0, 0, 100F);
        this.Detail.TextAlignment = DevExpress.XtraPrinting.TextAlignment.TopLeft;
        // 
        // TopMargin
        // 
        this.TopMargin.Controls.AddRange(new DevExpress.XtraReports.UI.XRControl[] {
            this.xrPanel2});
        this.TopMargin.Font = new System.Drawing.Font("Times New Roman", 9.75F);
        this.TopMargin.HeightF = 115.5833F;
        this.TopMargin.Name = "TopMargin";
        this.TopMargin.Padding = new DevExpress.XtraPrinting.PaddingInfo(0, 0, 0, 0, 100F);
        this.TopMargin.StylePriority.UseFont = false;
        this.TopMargin.TextAlignment = DevExpress.XtraPrinting.TextAlignment.TopLeft;
        // 
        // BottomMargin
        // 
        this.BottomMargin.HeightF = 8F;
        this.BottomMargin.Name = "BottomMargin";
        this.BottomMargin.Padding = new DevExpress.XtraPrinting.PaddingInfo(0, 0, 0, 0, 100F);
        this.BottomMargin.TextAlignment = DevExpress.XtraPrinting.TextAlignment.TopLeft;
        // 
        // GroupHeader1
        // 
        this.GroupHeader1.Controls.AddRange(new DevExpress.XtraReports.UI.XRControl[] {
            this.xrPanel3});
        this.GroupHeader1.GroupFields.AddRange(new DevExpress.XtraReports.UI.GroupField[] {
            new DevExpress.XtraReports.UI.GroupField("EmpCode", DevExpress.XtraReports.UI.XRColumnSortOrder.Ascending)});
        this.GroupHeader1.HeightF = 97.50001F;
        this.GroupHeader1.Name = "GroupHeader1";
        // 
        // xrPanel3
        // 
        this.xrPanel3.Borders = ((DevExpress.XtraPrinting.BorderSide)((((DevExpress.XtraPrinting.BorderSide.Left | DevExpress.XtraPrinting.BorderSide.Top)
                    | DevExpress.XtraPrinting.BorderSide.Right)
                    | DevExpress.XtraPrinting.BorderSide.Bottom)));
        this.xrPanel3.Controls.AddRange(new DevExpress.XtraReports.UI.XRControl[] {
            this.xrTable2});
        this.xrPanel3.LocationFloat = new DevExpress.Utils.PointFloat(88.5417F, 0F);
        this.xrPanel3.Name = "xrPanel3";
        this.xrPanel3.SizeF = new System.Drawing.SizeF(388.5161F, 97.50001F);
        this.xrPanel3.StylePriority.UseBorders = false;
        // 
        // xrTable2
        // 
        this.xrTable2.LocationFloat = new DevExpress.Utils.PointFloat(0F, 0F);
        this.xrTable2.Name = "xrTable2";
        this.xrTable2.Rows.AddRange(new DevExpress.XtraReports.UI.XRTableRow[] {
            this.xrTableRow7,
            this.xrTableRow8,
            this.xrTableRow9,
            this.xrTableRow10,
            this.xrTableRow11,
            this.xrTableRow12,
            this.xrTableRow13});
        this.xrTable2.SizeF = new System.Drawing.SizeF(388.516F, 96.45834F);
        // 
        // xrTableRow7
        // 
        this.xrTableRow7.Cells.AddRange(new DevExpress.XtraReports.UI.XRTableCell[] {
            this.xrTableCell19});
        this.xrTableRow7.Name = "xrTableRow7";
        this.xrTableRow7.Weight = 1;
        // 
        // xrTableCell19
        // 
        this.xrTableCell19.Font = new System.Drawing.Font("Verdana", 9.75F, System.Drawing.FontStyle.Bold);
        this.xrTableCell19.Name = "xrTableCell19";
        this.xrTableCell19.StylePriority.UseFont = false;
        this.xrTableCell19.Text = " SALARY";
        this.xrTableCell19.Weight = 3.7279183959960935;
        // 
        // xrTableRow8
        // 
        this.xrTableRow8.Cells.AddRange(new DevExpress.XtraReports.UI.XRTableCell[] {
            this.xrTableCell22,
            this.xrTableCell23,
            this.xrTableCell24,
            this.xrTableCell32});
        this.xrTableRow8.Name = "xrTableRow8";
        this.xrTableRow8.Weight = 1;
        // 
        // xrTableCell22
        // 
        this.xrTableCell22.Font = new System.Drawing.Font("Times New Roman", 9.75F);
        this.xrTableCell22.Name = "xrTableCell22";
        this.xrTableCell22.StylePriority.UseFont = false;
        this.xrTableCell22.Text = "  Attendance";
        this.xrTableCell22.Weight = 1;
        // 
        // xrTableCell23
        // 
        this.xrTableCell23.DataBindings.AddRange(new DevExpress.XtraReports.UI.XRBinding[] {
            new DevExpress.XtraReports.UI.XRBinding("Text", null, "dtEmpPayslip.totalAttendSal")});
        this.xrTableCell23.Font = new System.Drawing.Font("Arial", 9F);
        this.xrTableCell23.Name = "xrTableCell23";
        this.xrTableCell23.StylePriority.UseFont = false;
        this.xrTableCell23.Text = "xrTableCell23";
        this.xrTableCell23.Weight = 0.90817013649076006;
        // 
        // xrTableCell24
        // 
        this.xrTableCell24.Font = new System.Drawing.Font("Times New Roman", 9.75F);
        this.xrTableCell24.Name = "xrTableCell24";
        this.xrTableCell24.StylePriority.UseFont = false;
        this.xrTableCell24.Text = "  Deduction";
        this.xrTableCell24.Weight = 1.0476186427301384;
        // 
        // xrTableCell32
        // 
        this.xrTableCell32.DataBindings.AddRange(new DevExpress.XtraReports.UI.XRBinding[] {
            new DevExpress.XtraReports.UI.XRBinding("Text", null, "dtEmpPayslip.DeductionActAmt")});
        this.xrTableCell32.Font = new System.Drawing.Font("Arial", 9F);
        this.xrTableCell32.Name = "xrTableCell32";
        this.xrTableCell32.StylePriority.UseFont = false;
        this.xrTableCell32.Text = "xrTableCell32";
        this.xrTableCell32.Weight = 0.77212961677519532;
        // 
        // xrTableRow9
        // 
        this.xrTableRow9.Cells.AddRange(new DevExpress.XtraReports.UI.XRTableCell[] {
            this.xrTableCell25,
            this.xrTableCell26,
            this.xrTableCell27,
            this.xrTableCell33});
        this.xrTableRow9.Name = "xrTableRow9";
        this.xrTableRow9.Weight = 1;
        // 
        // xrTableCell25
        // 
        this.xrTableCell25.Font = new System.Drawing.Font("Times New Roman", 9.75F);
        this.xrTableCell25.Name = "xrTableCell25";
        this.xrTableCell25.StylePriority.UseFont = false;
        this.xrTableCell25.Text = "  Allowance";
        this.xrTableCell25.Weight = 1;
        // 
        // xrTableCell26
        // 
        this.xrTableCell26.DataBindings.AddRange(new DevExpress.XtraReports.UI.XRBinding[] {
            new DevExpress.XtraReports.UI.XRBinding("Text", null, "dtEmpPayslip.AllowanceActAmt")});
        this.xrTableCell26.Font = new System.Drawing.Font("Arial", 9F);
        this.xrTableCell26.Name = "xrTableCell26";
        this.xrTableCell26.StylePriority.UseFont = false;
        this.xrTableCell26.Text = "[AllowanceActAmt]";
        this.xrTableCell26.Weight = 0.90817013649076006;
        // 
        // xrTableCell27
        // 
        this.xrTableCell27.Font = new System.Drawing.Font("Times New Roman", 9.75F);
        this.xrTableCell27.Name = "xrTableCell27";
        this.xrTableCell27.StylePriority.UseFont = false;
        this.xrTableCell27.Text = "  Penalty";
        this.xrTableCell27.Weight = 1.0476186427301384;
        // 
        // xrTableCell33
        // 
        this.xrTableCell33.DataBindings.AddRange(new DevExpress.XtraReports.UI.XRBinding[] {
            new DevExpress.XtraReports.UI.XRBinding("Text", null, "dtEmpPayslip.TotalEmpPenalty")});
        this.xrTableCell33.Font = new System.Drawing.Font("Arial", 9F);
        this.xrTableCell33.Name = "xrTableCell33";
        this.xrTableCell33.StylePriority.UseFont = false;
        this.xrTableCell33.Text = "xrTableCell33";
        this.xrTableCell33.Weight = 0.77212961677519532;
        // 
        // xrTableRow10
        // 
        this.xrTableRow10.Cells.AddRange(new DevExpress.XtraReports.UI.XRTableCell[] {
            this.xrTableCell28,
            this.xrTableCell29,
            this.xrTableCell30,
            this.xrTableCell34});
        this.xrTableRow10.Name = "xrTableRow10";
        this.xrTableRow10.Weight = 1;
        // 
        // xrTableCell28
        // 
        this.xrTableCell28.Font = new System.Drawing.Font("Times New Roman", 9.75F);
        this.xrTableCell28.Name = "xrTableCell28";
        this.xrTableCell28.StylePriority.UseFont = false;
        this.xrTableCell28.Text = "  Claim";
        this.xrTableCell28.Weight = 1;
        // 
        // xrTableCell29
        // 
        this.xrTableCell29.DataBindings.AddRange(new DevExpress.XtraReports.UI.XRBinding[] {
            new DevExpress.XtraReports.UI.XRBinding("Text", null, "dtEmpPayslip.TotalEmpClaim")});
        this.xrTableCell29.Font = new System.Drawing.Font("Arial", 9F);
        this.xrTableCell29.Name = "xrTableCell29";
        this.xrTableCell29.StylePriority.UseFont = false;
        this.xrTableCell29.Text = "xrTableCell29";
        this.xrTableCell29.Weight = 0.90817013649076006;
        // 
        // xrTableCell30
        // 
        this.xrTableCell30.Font = new System.Drawing.Font("Times New Roman", 9.75F);
        this.xrTableCell30.Name = "xrTableCell30";
        this.xrTableCell30.StylePriority.UseFont = false;
        this.xrTableCell30.Text = "  Loan";
        this.xrTableCell30.Weight = 1.0476186427301384;
        // 
        // xrTableCell34
        // 
        this.xrTableCell34.DataBindings.AddRange(new DevExpress.XtraReports.UI.XRBinding[] {
            new DevExpress.XtraReports.UI.XRBinding("Text", null, "dtEmpPayslip.TotalEmpLoan")});
        this.xrTableCell34.Font = new System.Drawing.Font("Arial", 9F);
        this.xrTableCell34.Name = "xrTableCell34";
        this.xrTableCell34.StylePriority.UseFont = false;
        this.xrTableCell34.Text = "xrTableCell34";
        this.xrTableCell34.Weight = 0.77212961677519532;
        // 
        // xrTableRow11
        // 
        this.xrTableRow11.Cells.AddRange(new DevExpress.XtraReports.UI.XRTableCell[] {
            this.xrTableCell20,
            this.xrTableCell21,
            this.xrTableCell31,
            this.xrTableCell35});
        this.xrTableRow11.Name = "xrTableRow11";
        this.xrTableRow11.Weight = 1;
        // 
        // xrTableCell20
        // 
        this.xrTableCell20.Font = new System.Drawing.Font("Times New Roman", 9.75F);
        this.xrTableCell20.Name = "xrTableCell20";
        this.xrTableCell20.StylePriority.UseFont = false;
        this.xrTableCell20.Text = "  GROSS PAY";
        this.xrTableCell20.Weight = 1;
        // 
        // xrTableCell21
        // 
        this.xrTableCell21.DataBindings.AddRange(new DevExpress.XtraReports.UI.XRBinding[] {
            new DevExpress.XtraReports.UI.XRBinding("Text", null, "dtEmpPayslip.NetSalary")});
        this.xrTableCell21.Font = new System.Drawing.Font("Verdana", 9.75F, System.Drawing.FontStyle.Bold);
        this.xrTableCell21.Name = "xrTableCell21";
        this.xrTableCell21.StylePriority.UseFont = false;
        this.xrTableCell21.Text = "[NetSalary]";
        this.xrTableCell21.Weight = 0.90817013649076006;
        // 
        // xrTableCell31
        // 
        this.xrTableCell31.Font = new System.Drawing.Font("Times New Roman", 9.75F);
        this.xrTableCell31.Name = "xrTableCell31";
        this.xrTableCell31.StylePriority.UseFont = false;
        this.xrTableCell31.Text = "  GROSS PAY";
        this.xrTableCell31.Weight = 1.0476186427301384;
        // 
        // xrTableCell35
        // 
        this.xrTableCell35.DataBindings.AddRange(new DevExpress.XtraReports.UI.XRBinding[] {
            new DevExpress.XtraReports.UI.XRBinding("Text", null, "dtEmpPayslip.TotalDeducloanpenaty")});
        this.xrTableCell35.Font = new System.Drawing.Font("Verdana", 9F, System.Drawing.FontStyle.Bold);
        this.xrTableCell35.Name = "xrTableCell35";
        this.xrTableCell35.StylePriority.UseFont = false;
        this.xrTableCell35.Text = "[NetSalary]";
        this.xrTableCell35.Weight = 0.77212961677519532;
        // 
        // xrTableRow12
        // 
        this.xrTableRow12.Cells.AddRange(new DevExpress.XtraReports.UI.XRTableCell[] {
            this.xrTableCell36,
            this.xrTableCell39});
        this.xrTableRow12.Name = "xrTableRow12";
        this.xrTableRow12.Weight = 1;
        // 
        // xrTableCell36
        // 
        this.xrTableCell36.Borders = ((DevExpress.XtraPrinting.BorderSide)(((DevExpress.XtraPrinting.BorderSide.Left | DevExpress.XtraPrinting.BorderSide.Top)
                    | DevExpress.XtraPrinting.BorderSide.Bottom)));
        this.xrTableCell36.Font = new System.Drawing.Font("Arial", 8F);
        this.xrTableCell36.Name = "xrTableCell36";
        this.xrTableCell36.StylePriority.UseBorders = false;
        this.xrTableCell36.StylePriority.UseFont = false;
        this.xrTableCell36.Text = "PREVIOUS MONTH ADJUSTMENT:";
        this.xrTableCell36.Weight = 1.8005926483205781;
        // 
        // xrTableCell39
        // 
        this.xrTableCell39.Borders = ((DevExpress.XtraPrinting.BorderSide)(((DevExpress.XtraPrinting.BorderSide.Top | DevExpress.XtraPrinting.BorderSide.Right)
                    | DevExpress.XtraPrinting.BorderSide.Bottom)));
        this.xrTableCell39.DataBindings.AddRange(new DevExpress.XtraReports.UI.XRBinding[] {
            new DevExpress.XtraReports.UI.XRBinding("Text", null, "dtEmpPayslip.TotalAdjustmentAmt")});
        this.xrTableCell39.Name = "xrTableCell39";
        this.xrTableCell39.StylePriority.UseBorders = false;
        this.xrTableCell39.Text = "xrTableCell39";
        this.xrTableCell39.Weight = 1.9273257476755159;
        // 
        // xrTableRow13
        // 
        this.xrTableRow13.Cells.AddRange(new DevExpress.XtraReports.UI.XRTableCell[] {
            this.xrTableCell41});
        this.xrTableRow13.Name = "xrTableRow13";
        this.xrTableRow13.Weight = 1;
        // 
        // xrTableCell41
        // 
        this.xrTableCell41.Borders = DevExpress.XtraPrinting.BorderSide.Top;
        this.xrTableCell41.Name = "xrTableCell41";
        this.xrTableCell41.StylePriority.UseBorders = false;
        this.xrTableCell41.Weight = 3.727918395996094;
        // 
        // employeePaySlipDataSet1
        // 
        this.employeePaySlipDataSet1.DataSetName = "EmployeePaySlipDataSet";
        this.employeePaySlipDataSet1.SchemaSerializationMode = System.Data.SchemaSerializationMode.IncludeSchema;
        // 
        // GroupHeader2
        // 
        this.GroupHeader2.Controls.AddRange(new DevExpress.XtraReports.UI.XRControl[] {
            this.xrPanel1});
        this.GroupHeader2.GroupFields.AddRange(new DevExpress.XtraReports.UI.GroupField[] {
            new DevExpress.XtraReports.UI.GroupField("EmpCode", DevExpress.XtraReports.UI.XRColumnSortOrder.Ascending)});
        this.GroupHeader2.HeightF = 86.25005F;
        this.GroupHeader2.Level = 1;
        this.GroupHeader2.Name = "GroupHeader2";
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
        this.xrPanel1.LocationFloat = new DevExpress.Utils.PointFloat(88.54165F, 0F);
        this.xrPanel1.Name = "xrPanel1";
        this.xrPanel1.SizeF = new System.Drawing.SizeF(388.5161F, 86.25005F);
        this.xrPanel1.StylePriority.UseBorders = false;
        // 
        // xrPictureBox2
        // 
        this.xrPictureBox2.Borders = DevExpress.XtraPrinting.BorderSide.None;
        this.xrPictureBox2.LocationFloat = new DevExpress.Utils.PointFloat(324.9745F, 22.58669F);
        this.xrPictureBox2.Name = "xrPictureBox2";
        this.xrPictureBox2.SizeF = new System.Drawing.SizeF(53.54163F, 29.53835F);
        this.xrPictureBox2.Sizing = DevExpress.XtraPrinting.ImageSizeMode.StretchImage;
        this.xrPictureBox2.StylePriority.UseBorders = false;
        // 
        // xrLabel6
        // 
        this.xrLabel6.Borders = DevExpress.XtraPrinting.BorderSide.None;
        this.xrLabel6.DataBindings.AddRange(new DevExpress.XtraReports.UI.XRBinding[] {
            new DevExpress.XtraReports.UI.XRBinding("Text", null, "dtEmpPayslip.Month")});
        this.xrLabel6.Font = new System.Drawing.Font("Arial", 10F, System.Drawing.FontStyle.Bold);
        this.xrLabel6.LocationFloat = new DevExpress.Utils.PointFloat(252.581F, 9.569486F);
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
        this.xrLabel5.LocationFloat = new DevExpress.Utils.PointFloat(44.96678F, 9.572824F);
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
        this.xrTable1.LocationFloat = new DevExpress.Utils.PointFloat(9.91674F, 22.1667F);
        this.xrTable1.Name = "xrTable1";
        this.xrTable1.Rows.AddRange(new DevExpress.XtraReports.UI.XRTableRow[] {
            this.xrTableRow1,
            this.xrTableRow2,
            this.xrTableRow4,
            this.xrTableRow3,
            this.xrTableRow5,
            this.xrTableRow6});
        this.xrTable1.SizeF = new System.Drawing.SizeF(298.1296F, 54.08335F);
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
        this.xrLabel7.LocationFloat = new DevExpress.Utils.PointFloat(299.5579F, 10.00334F);
        this.xrLabel7.Name = "xrLabel7";
        this.xrLabel7.Padding = new DevExpress.XtraPrinting.PaddingInfo(2, 2, 0, 0, 100F);
        this.xrLabel7.SizeF = new System.Drawing.SizeF(44.16653F, 12.58334F);
        this.xrLabel7.StylePriority.UseBorders = false;
        this.xrLabel7.StylePriority.UseFont = false;
        this.xrLabel7.Text = "xrLabel7";
        // 
        // xrPanel2
        // 
        this.xrPanel2.Controls.AddRange(new DevExpress.XtraReports.UI.XRControl[] {
            this.xrLabel1,
            this.xrLabel2,
            this.xrLabel3,
            this.xrLabel4,
            this.xrLabel10,
            this.xrPictureBox1,
            this.xrLabel11});
        this.xrPanel2.LocationFloat = new DevExpress.Utils.PointFloat(88.54165F, 10.00001F);
        this.xrPanel2.Name = "xrPanel2";
        this.xrPanel2.SizeF = new System.Drawing.SizeF(388.5161F, 105.5833F);
        // 
        // xrLabel1
        // 
        this.xrLabel1.Borders = DevExpress.XtraPrinting.BorderSide.None;
        this.xrLabel1.Font = new System.Drawing.Font("Verdana", 6F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
        this.xrLabel1.LocationFloat = new DevExpress.Utils.PointFloat(9.99999F, 66.41664F);
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
        this.xrLabel2.LocationFloat = new DevExpress.Utils.PointFloat(199.9098F, 66.41664F);
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
        this.xrLabel3.LocationFloat = new DevExpress.Utils.PointFloat(198.8656F, 51.74999F);
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
        this.xrLabel4.LocationFloat = new DevExpress.Utils.PointFloat(256.5894F, 51.74999F);
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
        this.xrLabel10.LocationFloat = new DevExpress.Utils.PointFloat(198.8656F, 41.33331F);
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
        this.xrPictureBox1.LocationFloat = new DevExpress.Utils.PointFloat(9.916702F, 10.00001F);
        this.xrPictureBox1.Name = "xrPictureBox1";
        this.xrPictureBox1.SizeF = new System.Drawing.SizeF(60.41671F, 41.75001F);
        this.xrPictureBox1.Sizing = DevExpress.XtraPrinting.ImageSizeMode.StretchImage;
        this.xrPictureBox1.StylePriority.UseBorders = false;
        // 
        // xrLabel11
        // 
        this.xrLabel11.Borders = DevExpress.XtraPrinting.BorderSide.None;
        this.xrLabel11.Font = new System.Drawing.Font("Verdana", 10F, ((System.Drawing.FontStyle)((System.Drawing.FontStyle.Bold | System.Drawing.FontStyle.Underline))));
        this.xrLabel11.LocationFloat = new DevExpress.Utils.PointFloat(70.3334F, 28.74998F);
        this.xrLabel11.Name = "xrLabel11";
        this.xrLabel11.Padding = new DevExpress.XtraPrinting.PaddingInfo(2, 2, 0, 0, 100F);
        this.xrLabel11.SizeF = new System.Drawing.SizeF(276.0157F, 12.58334F);
        this.xrLabel11.StylePriority.UseBorders = false;
        this.xrLabel11.StylePriority.UseFont = false;
        this.xrLabel11.StylePriority.UseTextAlignment = false;
        this.xrLabel11.TextAlignment = DevExpress.XtraPrinting.TextAlignment.TopCenter;
        // 
        // Employee_Total_Sal_Report
        // 
        this.Bands.AddRange(new DevExpress.XtraReports.UI.Band[] {
            this.Detail,
            this.TopMargin,
            this.BottomMargin,
            this.GroupHeader1,
            this.GroupHeader2});
        this.Borders = ((DevExpress.XtraPrinting.BorderSide)((((DevExpress.XtraPrinting.BorderSide.Left | DevExpress.XtraPrinting.BorderSide.Top)
                    | DevExpress.XtraPrinting.BorderSide.Right)
                    | DevExpress.XtraPrinting.BorderSide.Bottom)));
        this.DataMember = "dtEmpPayslip";
        this.DataSource = this.employeePaySlipDataSet1;
        this.Font = new System.Drawing.Font("Times New Roman", 9.75F);
        this.Margins = new System.Drawing.Printing.Margins(100, 100, 116, 8);
        this.Version = "10.2";
        ((System.ComponentModel.ISupportInitialize)(this.xrTable2)).EndInit();
        ((System.ComponentModel.ISupportInitialize)(this.employeePaySlipDataSet1)).EndInit();
        ((System.ComponentModel.ISupportInitialize)(this.xrTable1)).EndInit();
        ((System.ComponentModel.ISupportInitialize)(this)).EndInit();

	}

	#endregion
}
