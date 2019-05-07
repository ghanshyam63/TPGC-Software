using System;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using DevExpress.XtraReports.UI;

/// <summary>
/// Summary description for Employee_Pay_Slip_Report
/// </summary>
public class Employee_Pay_Slip_Report : DevExpress.XtraReports.UI.XtraReport
{
	private DevExpress.XtraReports.UI.DetailBand Detail;
	private DevExpress.XtraReports.UI.TopMarginBand TopMargin;
    private DevExpress.XtraReports.UI.BottomMarginBand BottomMargin;
    private EmployeePaySlipDataSet employeePaySlipDataSet1;
    private GroupFooterBand GroupFooter1;
    private PageFooterBand PageFooter;
    private XRTable xrTable4;
    private XRTableRow xrTableRow9;
    private XRTableCell xrTableCell21;
    private XRTableCell xrTableCell22;
    private XRPanel xrPanel2;
    private XRLabel xrLabel3;
    private XRLabel xrLabel4;
    private XRLabel xrLabel2;
    private XRPictureBox xrPictureBox1;
    private XRLabel xrLabel1;
    private XRPanel xrPanel4;
    private XRTable xrTable2;
    private XRTableRow xrTableRow8;
    private XRTableCell xrTableCell23;
    private XRTableCell xrTableCell20;
    private GroupHeaderBand GroupHeader1;
    private XRPanel xrPanel3;
    private XRTable xrTable3;
    private XRTableRow xrTableRow7;
    private XRTableCell xrTableCell19;
    private GroupHeaderBand GroupHeader4;
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
    private GroupFooterBand GroupFooter2;
    private XRTable xrTable7;
    private XRTableRow xrTableRow18;
    private XRTableRow xrTableRow19;
    private XRTableCell xrTableCell52;
    private XRTableCell xrTableCell64;
    private XRTableRow xrTableRow20;
    private XRTableCell xrTableCell65;
    private GroupHeaderBand GroupHeader2;
    private XRLabel xrLabel8;
    private XRLabel xrLabel9;
    private XRPanel xrPanel5;
    private XRTable xrTable6;
    private XRTableRow xrTableRow11;
    private XRTableCell xrTableCell26;
    private XRTableCell xrTableCell25;
    private XRTableCell xrTableCell109;
    private XRTableRow xrTableRow12;
    private XRTableCell xrTableCell27;
    private XRTableCell xrTableCell29;
    private XRTableCell xrTableCell31;
    private XRTableCell xrTableCell28;
    private XRTableCell xrTableCell47;
    private XRTableCell xrTableCell85;
    private XRTableCell xrTableCell93;
    private XRTableCell xrTableCell110;
    private XRTableCell xrTableCell118;
    private XRTableRow xrTableRow13;
    private XRTableCell xrTableCell32;
    private XRTableCell xrTableCell33;
    private XRTableCell xrTableCell35;
    private XRTableCell xrTableCell30;
    private XRTableCell xrTableCell63;
    private XRTableCell xrTableCell89;
    private XRTableCell xrTableCell104;
    private XRTableCell xrTableCell111;
    private XRTableCell xrTableCell119;
    private XRTableRow xrTableRow14;
    private XRTableCell xrTableCell36;
    private XRTableCell xrTableCell37;
    private XRTableCell xrTableCell39;
    private XRTableCell xrTableCell34;
    private XRTableCell xrTableCell81;
    private XRTableCell xrTableCell67;
    private XRTableCell xrTableCell105;
    private XRTableCell xrTableCell112;
    private XRTableCell xrTableCell120;
    private XRTableRow xrTableRow15;
    private XRTableCell xrTableCell40;
    private XRTableCell xrTableCell41;
    private XRTableCell xrTableCell43;
    private XRTableCell xrTableCell38;
    private XRTableCell xrTableCell71;
    private XRTableCell xrTableCell97;
    private XRTableCell xrTableCell107;
    private XRTableCell xrTableCell113;
    private XRTableCell xrTableCell123;
    private XRTableRow xrTableRow16;
    private XRTableCell xrTableCell44;
    private XRTableCell xrTableCell45;
    private XRTableCell xrTableCell42;
    private XRTableCell xrTableCell102;
    private XRTableCell xrTableCell82;
    private XRTableCell xrTableCell124;
    private XRTableRow xrTableRow36;
    private XRTableCell xrTableCell121;
    private XRTableCell xrTableCell122;
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
    private XRTableRow xrTableRow26;
    private XRTableCell xrTableCell57;
    private XRTableCell xrTableCell75;
    private XRTableCell xrTableCell58;
    private XRTableRow xrTableRow27;
    private XRTableCell xrTableCell59;
    private XRTableCell xrTableCell76;
    private XRTableCell xrTableCell60;
    private XRTableRow xrTableRow28;
    private XRTableCell xrTableCell61;
    private XRTableCell xrTableCell77;
    private XRTableCell xrTableCell62;
    private XRTableRow xrTableRow35;
    private XRTableCell xrTableCell101;
    private XRTableCell xrTableCell103;
    private XRTableCell xrTableCell24;
    private XRTableCell xrTableCell46;
    private XRLabel xrLabel10;
    private XRPageBreak xrPageBreak1;
    private XRTableRow xrTableRow10;
    private XRTableCell xrTableCell48;
    private XRTableCell xrTableCell49;
	/// <summary>
	/// Required designer variable.
	/// </summary>
	private System.ComponentModel.IContainer components = null;

	public Employee_Pay_Slip_Report()
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
        xrLabel1.Text = companyname;
       // xrLabel13.Text = companyname;
    }
    public void setcompanyAddress(string Address)
    {
        xrLabel2.Text = Address;
       // xrLabel12.Text = Address;
    }
    public void setcontact(string Contact)
    {
        xrLabel4.Text = Contact;
       // xrLabel11.Text = Contact;
    }

    public void setwebsite(string web)
    {
        xrLabel8.Text = web;
       
    }
    public void setmailid(string mailid)
    {
         xrLabel9.Text = mailid;
        
    }
    
    public void setnetamount(string netamt)
    {
       
        xrLabel10.Text = netamt;
      
    }

    //public void setattendance(string dayspresent,string weekoffdays,string daysabsent, string holidays, string leavedays, string totaldays, string weekdayssal, string weekofsal, string holidayssal, string leavesal, string totalattendsal, string normalOTsal, string weekoffOTsal, string holidaysOTsal, string totalOTsal, string late, string early, string partial, string absentpean, string totalpenalty, string grossamt)
    //{

    //    lbldaypresent.Text = dayspresent;
    //    lblweekdays.Text = weekoffdays;
    //    xrLabel10.Text = daysabsent;
    //    xrLabel11.Text=holidays;
    //        xrLabel12.Text=leavedays;
    //        xrLabel13.Text=totaldays;
            
    //    xrLabel14.Text=weekdayssal;
    //        xrLabel15.Text=weekofsal;
    //        xrLabel16.Text=holidayssal;
    //        xrLabel17.Text=leavesal;
    //        xrLabel18.Text=totalattendsal;

    //        xrLabel19.Text=normalOTsal;
    //        xrLabel20.Text=weekoffOTsal;
    //        xrLabel21.Text=holidaysOTsal;
    //        xrLabel22.Text=totalOTsal;

    //        xrLabel23.Text=late;
    //        xrLabel24.Text=early;
    //        xrLabel25.Text=partial;
    //        xrLabel26.Text=absentpean;
    //        xrLabel27.Text= totalpenalty;

    //        xrLabel28.Text = grossamt;



    
    //}
  

    
   
   
	
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
        string resourceFileName = "Employee_Pay_Slip_Report.resx";
        DevExpress.XtraReports.UI.XRSummary xrSummary1 = new DevExpress.XtraReports.UI.XRSummary();
        DevExpress.XtraReports.UI.XRSummary xrSummary2 = new DevExpress.XtraReports.UI.XRSummary();
        this.Detail = new DevExpress.XtraReports.UI.DetailBand();
        this.xrPanel4 = new DevExpress.XtraReports.UI.XRPanel();
        this.xrTable2 = new DevExpress.XtraReports.UI.XRTable();
        this.xrTableRow8 = new DevExpress.XtraReports.UI.XRTableRow();
        this.xrTableCell20 = new DevExpress.XtraReports.UI.XRTableCell();
        this.xrTableCell23 = new DevExpress.XtraReports.UI.XRTableCell();
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
        this.employeePaySlipDataSet1 = new EmployeePaySlipDataSet();
        this.GroupFooter1 = new DevExpress.XtraReports.UI.GroupFooterBand();
        this.xrTable4 = new DevExpress.XtraReports.UI.XRTable();
        this.xrTableRow9 = new DevExpress.XtraReports.UI.XRTableRow();
        this.xrTableCell21 = new DevExpress.XtraReports.UI.XRTableCell();
        this.xrTableCell22 = new DevExpress.XtraReports.UI.XRTableCell();
        this.PageFooter = new DevExpress.XtraReports.UI.PageFooterBand();
        this.GroupHeader1 = new DevExpress.XtraReports.UI.GroupHeaderBand();
        this.xrPanel3 = new DevExpress.XtraReports.UI.XRPanel();
        this.xrTable3 = new DevExpress.XtraReports.UI.XRTable();
        this.xrTableRow7 = new DevExpress.XtraReports.UI.XRTableRow();
        this.xrTableCell19 = new DevExpress.XtraReports.UI.XRTableCell();
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
        this.GroupFooter2 = new DevExpress.XtraReports.UI.GroupFooterBand();
        this.xrPageBreak1 = new DevExpress.XtraReports.UI.XRPageBreak();
        this.xrTable7 = new DevExpress.XtraReports.UI.XRTable();
        this.xrTableRow18 = new DevExpress.XtraReports.UI.XRTableRow();
        this.xrTableCell46 = new DevExpress.XtraReports.UI.XRTableCell();
        this.xrTableCell24 = new DevExpress.XtraReports.UI.XRTableCell();
        this.xrLabel10 = new DevExpress.XtraReports.UI.XRLabel();
        this.xrTableRow10 = new DevExpress.XtraReports.UI.XRTableRow();
        this.xrTableCell48 = new DevExpress.XtraReports.UI.XRTableCell();
        this.xrTableCell49 = new DevExpress.XtraReports.UI.XRTableCell();
        this.xrTableRow19 = new DevExpress.XtraReports.UI.XRTableRow();
        this.xrTableCell52 = new DevExpress.XtraReports.UI.XRTableCell();
        this.xrTableCell64 = new DevExpress.XtraReports.UI.XRTableCell();
        this.xrTableRow20 = new DevExpress.XtraReports.UI.XRTableRow();
        this.xrTableCell65 = new DevExpress.XtraReports.UI.XRTableCell();
        this.GroupHeader2 = new DevExpress.XtraReports.UI.GroupHeaderBand();
        this.xrPanel5 = new DevExpress.XtraReports.UI.XRPanel();
        this.xrTable6 = new DevExpress.XtraReports.UI.XRTable();
        this.xrTableRow11 = new DevExpress.XtraReports.UI.XRTableRow();
        this.xrTableCell26 = new DevExpress.XtraReports.UI.XRTableCell();
        this.xrTableCell25 = new DevExpress.XtraReports.UI.XRTableCell();
        this.xrTableCell109 = new DevExpress.XtraReports.UI.XRTableCell();
        this.xrTableRow12 = new DevExpress.XtraReports.UI.XRTableRow();
        this.xrTableCell27 = new DevExpress.XtraReports.UI.XRTableCell();
        this.xrTableCell29 = new DevExpress.XtraReports.UI.XRTableCell();
        this.xrTableCell31 = new DevExpress.XtraReports.UI.XRTableCell();
        this.xrTableCell28 = new DevExpress.XtraReports.UI.XRTableCell();
        this.xrTableCell47 = new DevExpress.XtraReports.UI.XRTableCell();
        this.xrTableCell85 = new DevExpress.XtraReports.UI.XRTableCell();
        this.xrTableCell93 = new DevExpress.XtraReports.UI.XRTableCell();
        this.xrTableCell110 = new DevExpress.XtraReports.UI.XRTableCell();
        this.xrTableCell118 = new DevExpress.XtraReports.UI.XRTableCell();
        this.xrTableRow13 = new DevExpress.XtraReports.UI.XRTableRow();
        this.xrTableCell32 = new DevExpress.XtraReports.UI.XRTableCell();
        this.xrTableCell33 = new DevExpress.XtraReports.UI.XRTableCell();
        this.xrTableCell35 = new DevExpress.XtraReports.UI.XRTableCell();
        this.xrTableCell30 = new DevExpress.XtraReports.UI.XRTableCell();
        this.xrTableCell63 = new DevExpress.XtraReports.UI.XRTableCell();
        this.xrTableCell89 = new DevExpress.XtraReports.UI.XRTableCell();
        this.xrTableCell104 = new DevExpress.XtraReports.UI.XRTableCell();
        this.xrTableCell111 = new DevExpress.XtraReports.UI.XRTableCell();
        this.xrTableCell119 = new DevExpress.XtraReports.UI.XRTableCell();
        this.xrTableRow14 = new DevExpress.XtraReports.UI.XRTableRow();
        this.xrTableCell36 = new DevExpress.XtraReports.UI.XRTableCell();
        this.xrTableCell37 = new DevExpress.XtraReports.UI.XRTableCell();
        this.xrTableCell39 = new DevExpress.XtraReports.UI.XRTableCell();
        this.xrTableCell34 = new DevExpress.XtraReports.UI.XRTableCell();
        this.xrTableCell81 = new DevExpress.XtraReports.UI.XRTableCell();
        this.xrTableCell67 = new DevExpress.XtraReports.UI.XRTableCell();
        this.xrTableCell105 = new DevExpress.XtraReports.UI.XRTableCell();
        this.xrTableCell112 = new DevExpress.XtraReports.UI.XRTableCell();
        this.xrTableCell120 = new DevExpress.XtraReports.UI.XRTableCell();
        this.xrTableRow15 = new DevExpress.XtraReports.UI.XRTableRow();
        this.xrTableCell40 = new DevExpress.XtraReports.UI.XRTableCell();
        this.xrTableCell41 = new DevExpress.XtraReports.UI.XRTableCell();
        this.xrTableCell43 = new DevExpress.XtraReports.UI.XRTableCell();
        this.xrTableCell38 = new DevExpress.XtraReports.UI.XRTableCell();
        this.xrTableCell71 = new DevExpress.XtraReports.UI.XRTableCell();
        this.xrTableCell97 = new DevExpress.XtraReports.UI.XRTableCell();
        this.xrTableCell107 = new DevExpress.XtraReports.UI.XRTableCell();
        this.xrTableCell113 = new DevExpress.XtraReports.UI.XRTableCell();
        this.xrTableCell123 = new DevExpress.XtraReports.UI.XRTableCell();
        this.xrTableRow16 = new DevExpress.XtraReports.UI.XRTableRow();
        this.xrTableCell44 = new DevExpress.XtraReports.UI.XRTableCell();
        this.xrTableCell45 = new DevExpress.XtraReports.UI.XRTableCell();
        this.xrTableCell42 = new DevExpress.XtraReports.UI.XRTableCell();
        this.xrTableCell102 = new DevExpress.XtraReports.UI.XRTableCell();
        this.xrTableCell82 = new DevExpress.XtraReports.UI.XRTableCell();
        this.xrTableCell124 = new DevExpress.XtraReports.UI.XRTableCell();
        this.xrTableRow36 = new DevExpress.XtraReports.UI.XRTableRow();
        this.xrTableCell121 = new DevExpress.XtraReports.UI.XRTableCell();
        this.xrTableCell122 = new DevExpress.XtraReports.UI.XRTableCell();
        this.xrPanel9 = new DevExpress.XtraReports.UI.XRPanel();
        this.xrTable11 = new DevExpress.XtraReports.UI.XRTable();
        this.xrTableRow23 = new DevExpress.XtraReports.UI.XRTableRow();
        this.xrTableCell51 = new DevExpress.XtraReports.UI.XRTableCell();
        this.xrTableRow24 = new DevExpress.XtraReports.UI.XRTableRow();
        this.xrTableCell53 = new DevExpress.XtraReports.UI.XRTableCell();
        this.xrTableCell73 = new DevExpress.XtraReports.UI.XRTableCell();
        this.xrTableCell54 = new DevExpress.XtraReports.UI.XRTableCell();
        this.xrTableRow25 = new DevExpress.XtraReports.UI.XRTableRow();
        this.xrTableCell55 = new DevExpress.XtraReports.UI.XRTableCell();
        this.xrTableCell74 = new DevExpress.XtraReports.UI.XRTableCell();
        this.xrTableCell56 = new DevExpress.XtraReports.UI.XRTableCell();
        this.xrTableRow26 = new DevExpress.XtraReports.UI.XRTableRow();
        this.xrTableCell57 = new DevExpress.XtraReports.UI.XRTableCell();
        this.xrTableCell75 = new DevExpress.XtraReports.UI.XRTableCell();
        this.xrTableCell58 = new DevExpress.XtraReports.UI.XRTableCell();
        this.xrTableRow27 = new DevExpress.XtraReports.UI.XRTableRow();
        this.xrTableCell59 = new DevExpress.XtraReports.UI.XRTableCell();
        this.xrTableCell76 = new DevExpress.XtraReports.UI.XRTableCell();
        this.xrTableCell60 = new DevExpress.XtraReports.UI.XRTableCell();
        this.xrTableRow28 = new DevExpress.XtraReports.UI.XRTableRow();
        this.xrTableCell61 = new DevExpress.XtraReports.UI.XRTableCell();
        this.xrTableCell77 = new DevExpress.XtraReports.UI.XRTableCell();
        this.xrTableCell62 = new DevExpress.XtraReports.UI.XRTableCell();
        this.xrTableRow35 = new DevExpress.XtraReports.UI.XRTableRow();
        this.xrTableCell101 = new DevExpress.XtraReports.UI.XRTableCell();
        this.xrTableCell103 = new DevExpress.XtraReports.UI.XRTableCell();
        ((System.ComponentModel.ISupportInitialize)(this.xrTable2)).BeginInit();
        ((System.ComponentModel.ISupportInitialize)(this.employeePaySlipDataSet1)).BeginInit();
        ((System.ComponentModel.ISupportInitialize)(this.xrTable4)).BeginInit();
        ((System.ComponentModel.ISupportInitialize)(this.xrTable3)).BeginInit();
        ((System.ComponentModel.ISupportInitialize)(this.xrTable1)).BeginInit();
        ((System.ComponentModel.ISupportInitialize)(this.xrTable7)).BeginInit();
        ((System.ComponentModel.ISupportInitialize)(this.xrTable6)).BeginInit();
        ((System.ComponentModel.ISupportInitialize)(this.xrTable11)).BeginInit();
        ((System.ComponentModel.ISupportInitialize)(this)).BeginInit();
        // 
        // Detail
        // 
        this.Detail.Borders = ((DevExpress.XtraPrinting.BorderSide)((((DevExpress.XtraPrinting.BorderSide.Left | DevExpress.XtraPrinting.BorderSide.Top)
                    | DevExpress.XtraPrinting.BorderSide.Right)
                    | DevExpress.XtraPrinting.BorderSide.Bottom)));
        this.Detail.Controls.AddRange(new DevExpress.XtraReports.UI.XRControl[] {
            this.xrPanel4});
        this.Detail.HeightF = 16.66667F;
        this.Detail.Name = "Detail";
        this.Detail.Padding = new DevExpress.XtraPrinting.PaddingInfo(0, 0, 0, 0, 100F);
        this.Detail.StylePriority.UseBorders = false;
        this.Detail.TextAlignment = DevExpress.XtraPrinting.TextAlignment.TopLeft;
        // 
        // xrPanel4
        // 
        this.xrPanel4.Borders = ((DevExpress.XtraPrinting.BorderSide)((DevExpress.XtraPrinting.BorderSide.Left | DevExpress.XtraPrinting.BorderSide.Right)));
        this.xrPanel4.Controls.AddRange(new DevExpress.XtraReports.UI.XRControl[] {
            this.xrTable2});
        this.xrPanel4.LocationFloat = new DevExpress.Utils.PointFloat(0.7790883F, 0F);
        this.xrPanel4.Name = "xrPanel4";
        this.xrPanel4.SizeF = new System.Drawing.SizeF(543.635F, 16.66667F);
        this.xrPanel4.StylePriority.UseBorders = false;
        // 
        // xrTable2
        // 
        this.xrTable2.Borders = DevExpress.XtraPrinting.BorderSide.None;
        this.xrTable2.LocationFloat = new DevExpress.Utils.PointFloat(9.916721F, 0F);
        this.xrTable2.Name = "xrTable2";
        this.xrTable2.Rows.AddRange(new DevExpress.XtraReports.UI.XRTableRow[] {
            this.xrTableRow8});
        this.xrTable2.SizeF = new System.Drawing.SizeF(533.0226F, 16.66667F);
        this.xrTable2.StylePriority.UseBorders = false;
        // 
        // xrTableRow8
        // 
        this.xrTableRow8.Cells.AddRange(new DevExpress.XtraReports.UI.XRTableCell[] {
            this.xrTableCell20,
            this.xrTableCell23});
        this.xrTableRow8.Name = "xrTableRow8";
        this.xrTableRow8.Weight = 1;
        // 
        // xrTableCell20
        // 
        this.xrTableCell20.Borders = DevExpress.XtraPrinting.BorderSide.Right;
        this.xrTableCell20.DataBindings.AddRange(new DevExpress.XtraReports.UI.XRBinding[] {
            new DevExpress.XtraReports.UI.XRBinding("Text", null, "dtEmpPayslip.AllowanceName")});
        this.xrTableCell20.Font = new System.Drawing.Font("Arial", 8F);
        this.xrTableCell20.Name = "xrTableCell20";
        this.xrTableCell20.StylePriority.UseBorders = false;
        this.xrTableCell20.StylePriority.UseFont = false;
        this.xrTableCell20.StylePriority.UseTextAlignment = false;
        this.xrTableCell20.TextAlignment = DevExpress.XtraPrinting.TextAlignment.TopLeft;
        this.xrTableCell20.Weight = 1.6676364196160249;
        // 
        // xrTableCell23
        // 
        this.xrTableCell23.Borders = DevExpress.XtraPrinting.BorderSide.None;
        this.xrTableCell23.DataBindings.AddRange(new DevExpress.XtraReports.UI.XRBinding[] {
            new DevExpress.XtraReports.UI.XRBinding("Text", null, "dtEmpPayslip.AllowanceActAmt")});
        this.xrTableCell23.Font = new System.Drawing.Font("Arial", 8F);
        this.xrTableCell23.Name = "xrTableCell23";
        this.xrTableCell23.StylePriority.UseBorders = false;
        this.xrTableCell23.StylePriority.UseFont = false;
        this.xrTableCell23.StylePriority.UseTextAlignment = false;
        this.xrTableCell23.TextAlignment = DevExpress.XtraPrinting.TextAlignment.TopCenter;
        this.xrTableCell23.Weight = 1.1039361110732995;
        // 
        // TopMargin
        // 
        this.TopMargin.Borders = ((DevExpress.XtraPrinting.BorderSide)((((DevExpress.XtraPrinting.BorderSide.Left | DevExpress.XtraPrinting.BorderSide.Top)
                    | DevExpress.XtraPrinting.BorderSide.Right)
                    | DevExpress.XtraPrinting.BorderSide.Bottom)));
        this.TopMargin.Controls.AddRange(new DevExpress.XtraReports.UI.XRControl[] {
            this.xrPanel2});
        this.TopMargin.Font = new System.Drawing.Font("Times New Roman", 9.75F);
        this.TopMargin.HeightF = 120.4583F;
        this.TopMargin.Name = "TopMargin";
        this.TopMargin.Padding = new DevExpress.XtraPrinting.PaddingInfo(0, 0, 0, 0, 100F);
        this.TopMargin.StylePriority.UseBorders = false;
        this.TopMargin.StylePriority.UseFont = false;
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
        this.xrPanel2.LocationFloat = new DevExpress.Utils.PointFloat(0.08327166F, 20.00001F);
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
        this.BottomMargin.Borders = ((DevExpress.XtraPrinting.BorderSide)((((DevExpress.XtraPrinting.BorderSide.Left | DevExpress.XtraPrinting.BorderSide.Top)
                    | DevExpress.XtraPrinting.BorderSide.Right)
                    | DevExpress.XtraPrinting.BorderSide.Bottom)));
        this.BottomMargin.HeightF = 0F;
        this.BottomMargin.Name = "BottomMargin";
        this.BottomMargin.Padding = new DevExpress.XtraPrinting.PaddingInfo(0, 0, 0, 0, 100F);
        this.BottomMargin.StylePriority.UseBorders = false;
        this.BottomMargin.TextAlignment = DevExpress.XtraPrinting.TextAlignment.TopLeft;
        // 
        // employeePaySlipDataSet1
        // 
        this.employeePaySlipDataSet1.DataSetName = "EmployeePaySlipDataSet";
        this.employeePaySlipDataSet1.SchemaSerializationMode = System.Data.SchemaSerializationMode.IncludeSchema;
        // 
        // GroupFooter1
        // 
        this.GroupFooter1.Controls.AddRange(new DevExpress.XtraReports.UI.XRControl[] {
            this.xrTable4});
        this.GroupFooter1.Font = new System.Drawing.Font("Times New Roman", 9.75F);
        this.GroupFooter1.HeightF = 15.00003F;
        this.GroupFooter1.Name = "GroupFooter1";
        this.GroupFooter1.StylePriority.UseFont = false;
        // 
        // xrTable4
        // 
        this.xrTable4.Borders = ((DevExpress.XtraPrinting.BorderSide)((((DevExpress.XtraPrinting.BorderSide.Left | DevExpress.XtraPrinting.BorderSide.Top)
                    | DevExpress.XtraPrinting.BorderSide.Right)
                    | DevExpress.XtraPrinting.BorderSide.Bottom)));
        this.xrTable4.LocationFloat = new DevExpress.Utils.PointFloat(0.7790883F, 0F);
        this.xrTable4.Name = "xrTable4";
        this.xrTable4.Rows.AddRange(new DevExpress.XtraReports.UI.XRTableRow[] {
            this.xrTableRow9});
        this.xrTable4.SizeF = new System.Drawing.SizeF(543.635F, 14.99996F);
        this.xrTable4.StylePriority.UseBorders = false;
        // 
        // xrTableRow9
        // 
        this.xrTableRow9.Cells.AddRange(new DevExpress.XtraReports.UI.XRTableCell[] {
            this.xrTableCell21,
            this.xrTableCell22});
        this.xrTableRow9.Name = "xrTableRow9";
        this.xrTableRow9.Weight = 1;
        // 
        // xrTableCell21
        // 
        this.xrTableCell21.Font = new System.Drawing.Font("Verdana", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
        this.xrTableCell21.Name = "xrTableCell21";
        this.xrTableCell21.StylePriority.UseFont = false;
        this.xrTableCell21.StylePriority.UseTextAlignment = false;
        this.xrTableCell21.Text = "  Gross Pay";
        this.xrTableCell21.TextAlignment = DevExpress.XtraPrinting.TextAlignment.TopLeft;
        this.xrTableCell21.Weight = 1.8327612298490474;
        // 
        // xrTableCell22
        // 
        this.xrTableCell22.DataBindings.AddRange(new DevExpress.XtraReports.UI.XRBinding[] {
            new DevExpress.XtraReports.UI.XRBinding("Text", null, "dtEmpPayslip.AllowanceActAmt")});
        this.xrTableCell22.Font = new System.Drawing.Font("Arial", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
        this.xrTableCell22.Name = "xrTableCell22";
        this.xrTableCell22.StylePriority.UseFont = false;
        this.xrTableCell22.StylePriority.UseTextAlignment = false;
        xrSummary1.Running = DevExpress.XtraReports.UI.SummaryRunning.Group;
        this.xrTableCell22.Summary = xrSummary1;
        this.xrTableCell22.Text = "xrTableCell22";
        this.xrTableCell22.TextAlignment = DevExpress.XtraPrinting.TextAlignment.TopCenter;
        this.xrTableCell22.Weight = 1.1807123468097009;
        // 
        // PageFooter
        // 
        this.PageFooter.HeightF = 27.08333F;
        this.PageFooter.Name = "PageFooter";
        // 
        // GroupHeader1
        // 
        this.GroupHeader1.Controls.AddRange(new DevExpress.XtraReports.UI.XRControl[] {
            this.xrPanel3});
        this.GroupHeader1.GroupFields.AddRange(new DevExpress.XtraReports.UI.GroupField[] {
            new DevExpress.XtraReports.UI.GroupField("EmpCode", DevExpress.XtraReports.UI.XRColumnSortOrder.Ascending),
            new DevExpress.XtraReports.UI.GroupField("TypeAllow", DevExpress.XtraReports.UI.XRColumnSortOrder.Ascending)});
        this.GroupHeader1.GroupUnion = DevExpress.XtraReports.UI.GroupUnion.WithFirstDetail;
        this.GroupHeader1.HeightF = 12.5F;
        this.GroupHeader1.Name = "GroupHeader1";
        // 
        // xrPanel3
        // 
        this.xrPanel3.Borders = ((DevExpress.XtraPrinting.BorderSide)(((DevExpress.XtraPrinting.BorderSide.Left | DevExpress.XtraPrinting.BorderSide.Right)
                    | DevExpress.XtraPrinting.BorderSide.Bottom)));
        this.xrPanel3.Controls.AddRange(new DevExpress.XtraReports.UI.XRControl[] {
            this.xrTable3});
        this.xrPanel3.LocationFloat = new DevExpress.Utils.PointFloat(0.7790883F, 0F);
        this.xrPanel3.Name = "xrPanel3";
        this.xrPanel3.SizeF = new System.Drawing.SizeF(543.635F, 12.5F);
        this.xrPanel3.StylePriority.UseBorders = false;
        // 
        // xrTable3
        // 
        this.xrTable3.Borders = DevExpress.XtraPrinting.BorderSide.None;
        this.xrTable3.Font = new System.Drawing.Font("Arial", 9F);
        this.xrTable3.LocationFloat = new DevExpress.Utils.PointFloat(10.04148F, 0F);
        this.xrTable3.Name = "xrTable3";
        this.xrTable3.Rows.AddRange(new DevExpress.XtraReports.UI.XRTableRow[] {
            this.xrTableRow7});
        this.xrTable3.SizeF = new System.Drawing.SizeF(532.9808F, 8.124988F);
        this.xrTable3.StylePriority.UseBorders = false;
        this.xrTable3.StylePriority.UseFont = false;
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
        this.xrTableCell19.DataBindings.AddRange(new DevExpress.XtraReports.UI.XRBinding[] {
            new DevExpress.XtraReports.UI.XRBinding("Text", null, "dtEmpPayslip.TypeAllow")});
        this.xrTableCell19.Font = new System.Drawing.Font("Arial", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
        this.xrTableCell19.Name = "xrTableCell19";
        this.xrTableCell19.StylePriority.UseFont = false;
        this.xrTableCell19.StylePriority.UseTextAlignment = false;
        this.xrTableCell19.Text = " ";
        this.xrTableCell19.TextAlignment = DevExpress.XtraPrinting.TextAlignment.TopLeft;
        this.xrTableCell19.Weight = 1.0459957755295188;
        // 
        // GroupHeader4
        // 
        this.GroupHeader4.Controls.AddRange(new DevExpress.XtraReports.UI.XRControl[] {
            this.xrPanel1});
        this.GroupHeader4.GroupFields.AddRange(new DevExpress.XtraReports.UI.GroupField[] {
            new DevExpress.XtraReports.UI.GroupField("EmpCode", DevExpress.XtraReports.UI.XRColumnSortOrder.Ascending)});
        this.GroupHeader4.HeightF = 86.25005F;
        this.GroupHeader4.Level = 2;
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
        this.xrPanel1.LocationFloat = new DevExpress.Utils.PointFloat(0.08351008F, 0F);
        this.xrPanel1.Name = "xrPanel1";
        this.xrPanel1.SizeF = new System.Drawing.SizeF(544.3306F, 86.25005F);
        this.xrPanel1.StylePriority.UseBorders = false;
        // 
        // xrPictureBox2
        // 
        this.xrPictureBox2.Borders = DevExpress.XtraPrinting.BorderSide.None;
        this.xrPictureBox2.LocationFloat = new DevExpress.Utils.PointFloat(453.1237F, 22.1667F);
        this.xrPictureBox2.Name = "xrPictureBox2";
        this.xrPictureBox2.SizeF = new System.Drawing.SizeF(80.20834F, 39.94447F);
        this.xrPictureBox2.Sizing = DevExpress.XtraPrinting.ImageSizeMode.StretchImage;
        this.xrPictureBox2.StylePriority.UseBorders = false;
        // 
        // xrLabel6
        // 
        this.xrLabel6.Borders = DevExpress.XtraPrinting.BorderSide.None;
        this.xrLabel6.DataBindings.AddRange(new DevExpress.XtraReports.UI.XRBinding[] {
            new DevExpress.XtraReports.UI.XRBinding("Text", null, "dtEmpPayslip.Month")});
        this.xrLabel6.Font = new System.Drawing.Font("Arial", 10F, System.Drawing.FontStyle.Bold);
        this.xrLabel6.LocationFloat = new DevExpress.Utils.PointFloat(301.4985F, 9.566148F);
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
        this.xrLabel5.LocationFloat = new DevExpress.Utils.PointFloat(93.88426F, 9.569486F);
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
        this.xrLabel7.LocationFloat = new DevExpress.Utils.PointFloat(348.4753F, 10.00001F);
        this.xrLabel7.Name = "xrLabel7";
        this.xrLabel7.Padding = new DevExpress.XtraPrinting.PaddingInfo(2, 2, 0, 0, 100F);
        this.xrLabel7.SizeF = new System.Drawing.SizeF(44.16653F, 12.58334F);
        this.xrLabel7.StylePriority.UseBorders = false;
        this.xrLabel7.StylePriority.UseFont = false;
        this.xrLabel7.Text = "xrLabel7";
        // 
        // GroupFooter2
        // 
        this.GroupFooter2.Controls.AddRange(new DevExpress.XtraReports.UI.XRControl[] {
            this.xrPageBreak1,
            this.xrTable7});
        this.GroupFooter2.HeightF = 62.08324F;
        this.GroupFooter2.Level = 1;
        this.GroupFooter2.Name = "GroupFooter2";
        // 
        // xrPageBreak1
        // 
        this.xrPageBreak1.LocationFloat = new DevExpress.Utils.PointFloat(0F, 59.99991F);
        this.xrPageBreak1.Name = "xrPageBreak1";
        // 
        // xrTable7
        // 
        this.xrTable7.Borders = ((DevExpress.XtraPrinting.BorderSide)(((DevExpress.XtraPrinting.BorderSide.Left | DevExpress.XtraPrinting.BorderSide.Right)
                    | DevExpress.XtraPrinting.BorderSide.Bottom)));
        this.xrTable7.LocationFloat = new DevExpress.Utils.PointFloat(0.7790883F, 0F);
        this.xrTable7.Name = "xrTable7";
        this.xrTable7.Rows.AddRange(new DevExpress.XtraReports.UI.XRTableRow[] {
            this.xrTableRow18,
            this.xrTableRow10,
            this.xrTableRow19,
            this.xrTableRow20});
        this.xrTable7.SizeF = new System.Drawing.SizeF(543.635F, 59.99994F);
        this.xrTable7.StylePriority.UseBorders = false;
        // 
        // xrTableRow18
        // 
        this.xrTableRow18.Cells.AddRange(new DevExpress.XtraReports.UI.XRTableCell[] {
            this.xrTableCell46,
            this.xrTableCell24});
        this.xrTableRow18.Name = "xrTableRow18";
        this.xrTableRow18.Weight = 1;
        // 
        // xrTableCell46
        // 
        this.xrTableCell46.Borders = DevExpress.XtraPrinting.BorderSide.Left;
        this.xrTableCell46.Name = "xrTableCell46";
        this.xrTableCell46.StylePriority.UseBorders = false;
        this.xrTableCell46.StylePriority.UseTextAlignment = false;
        this.xrTableCell46.Text = "Net Salary:";
        this.xrTableCell46.TextAlignment = DevExpress.XtraPrinting.TextAlignment.TopRight;
        this.xrTableCell46.Weight = 1.8230605172313781;
        // 
        // xrTableCell24
        // 
        this.xrTableCell24.Borders = DevExpress.XtraPrinting.BorderSide.Right;
        this.xrTableCell24.Controls.AddRange(new DevExpress.XtraReports.UI.XRControl[] {
            this.xrLabel10});
        this.xrTableCell24.DataBindings.AddRange(new DevExpress.XtraReports.UI.XRBinding[] {
            new DevExpress.XtraReports.UI.XRBinding("Text", null, "dtEmpPayslip.NetSalary")});
        this.xrTableCell24.Font = new System.Drawing.Font("Times New Roman", 9.75F, ((System.Drawing.FontStyle)((System.Drawing.FontStyle.Bold | System.Drawing.FontStyle.Underline))));
        this.xrTableCell24.Name = "xrTableCell24";
        this.xrTableCell24.StylePriority.UseBorders = false;
        this.xrTableCell24.StylePriority.UseFont = false;
        this.xrTableCell24.Text = "                                                                           Net Sa" +
            "lary:";
        this.xrTableCell24.Weight = 1.1769394827686219;
        // 
        // xrLabel10
        // 
        this.xrLabel10.Borders = DevExpress.XtraPrinting.BorderSide.None;
        this.xrLabel10.DataBindings.AddRange(new DevExpress.XtraReports.UI.XRBinding[] {
            new DevExpress.XtraReports.UI.XRBinding("Text", null, "dtEmpPayslip.NetSalary")});
        this.xrLabel10.LocationFloat = new DevExpress.Utils.PointFloat(0F, 0F);
        this.xrLabel10.Name = "xrLabel10";
        this.xrLabel10.Padding = new DevExpress.XtraPrinting.PaddingInfo(2, 2, 0, 0, 100F);
        this.xrLabel10.SizeF = new System.Drawing.SizeF(100.4229F, 14.99996F);
        this.xrLabel10.StylePriority.UseBorders = false;
        xrSummary2.Func = DevExpress.XtraReports.UI.SummaryFunc.Custom;
        xrSummary2.Running = DevExpress.XtraReports.UI.SummaryRunning.Group;
        this.xrLabel10.Summary = xrSummary2;
        this.xrLabel10.Text = "xrLabel10";
        this.xrLabel10.SummaryReset += new System.EventHandler(this.xrLabel10_SummaryReset);
        this.xrLabel10.SummaryRowChanged += new System.EventHandler(this.xrLabel10_SummaryRowChanged);
        this.xrLabel10.SummaryGetResult += new DevExpress.XtraReports.UI.SummaryGetResultHandler(this.xrLabel10_SummaryGetResult);
        // 
        // xrTableRow10
        // 
        this.xrTableRow10.Cells.AddRange(new DevExpress.XtraReports.UI.XRTableCell[] {
            this.xrTableCell48,
            this.xrTableCell49});
        this.xrTableRow10.Name = "xrTableRow10";
        this.xrTableRow10.Weight = 1;
        // 
        // xrTableCell48
        // 
        this.xrTableCell48.Borders = DevExpress.XtraPrinting.BorderSide.Left;
        this.xrTableCell48.Name = "xrTableCell48";
        this.xrTableCell48.StylePriority.UseBorders = false;
        this.xrTableCell48.Weight = 1.8230605172313781;
        // 
        // xrTableCell49
        // 
        this.xrTableCell49.Borders = DevExpress.XtraPrinting.BorderSide.Right;
        this.xrTableCell49.Name = "xrTableCell49";
        this.xrTableCell49.StylePriority.UseBorders = false;
        this.xrTableCell49.Weight = 1.1769394827686219;
        // 
        // xrTableRow19
        // 
        this.xrTableRow19.Cells.AddRange(new DevExpress.XtraReports.UI.XRTableCell[] {
            this.xrTableCell52,
            this.xrTableCell64});
        this.xrTableRow19.Name = "xrTableRow19";
        this.xrTableRow19.Weight = 1;
        // 
        // xrTableCell52
        // 
        this.xrTableCell52.Borders = ((DevExpress.XtraPrinting.BorderSide)((DevExpress.XtraPrinting.BorderSide.Left | DevExpress.XtraPrinting.BorderSide.Bottom)));
        this.xrTableCell52.Font = new System.Drawing.Font("Arial", 9F);
        this.xrTableCell52.Name = "xrTableCell52";
        this.xrTableCell52.StylePriority.UseBorders = false;
        this.xrTableCell52.StylePriority.UseFont = false;
        this.xrTableCell52.Text = "  HR SIGNATURE";
        this.xrTableCell52.Weight = 1.2232252462926265;
        // 
        // xrTableCell64
        // 
        this.xrTableCell64.Borders = ((DevExpress.XtraPrinting.BorderSide)((DevExpress.XtraPrinting.BorderSide.Right | DevExpress.XtraPrinting.BorderSide.Bottom)));
        this.xrTableCell64.Font = new System.Drawing.Font("Arial", 9F);
        this.xrTableCell64.Name = "xrTableCell64";
        this.xrTableCell64.StylePriority.UseBorders = false;
        this.xrTableCell64.StylePriority.UseFont = false;
        this.xrTableCell64.StylePriority.UseTextAlignment = false;
        this.xrTableCell64.Text = "                                                   EMPLOYEE SIGNATUR";
        this.xrTableCell64.TextAlignment = DevExpress.XtraPrinting.TextAlignment.TopCenter;
        this.xrTableCell64.Weight = 1.7767747537073733;
        // 
        // xrTableRow20
        // 
        this.xrTableRow20.Cells.AddRange(new DevExpress.XtraReports.UI.XRTableCell[] {
            this.xrTableCell65});
        this.xrTableRow20.Name = "xrTableRow20";
        this.xrTableRow20.Weight = 1;
        // 
        // xrTableCell65
        // 
        this.xrTableCell65.Name = "xrTableCell65";
        this.xrTableCell65.Weight = 3;
        // 
        // GroupHeader2
        // 
        this.GroupHeader2.Controls.AddRange(new DevExpress.XtraReports.UI.XRControl[] {
            this.xrPanel5,
            this.xrPanel9});
        this.GroupHeader2.GroupFields.AddRange(new DevExpress.XtraReports.UI.GroupField[] {
            new DevExpress.XtraReports.UI.GroupField("EmpCode", DevExpress.XtraReports.UI.XRColumnSortOrder.Ascending)});
        this.GroupHeader2.HeightF = 177.3828F;
        this.GroupHeader2.Level = 1;
        this.GroupHeader2.Name = "GroupHeader2";
        // 
        // xrPanel5
        // 
        this.xrPanel5.Controls.AddRange(new DevExpress.XtraReports.UI.XRControl[] {
            this.xrTable6});
        this.xrPanel5.LocationFloat = new DevExpress.Utils.PointFloat(0.7790883F, 80.20841F);
        this.xrPanel5.Name = "xrPanel5";
        this.xrPanel5.SizeF = new System.Drawing.SizeF(543.1351F, 97.17442F);
        // 
        // xrTable6
        // 
        this.xrTable6.Borders = DevExpress.XtraPrinting.BorderSide.None;
        this.xrTable6.LocationFloat = new DevExpress.Utils.PointFloat(0F, 3.051758E-05F);
        this.xrTable6.Name = "xrTable6";
        this.xrTable6.Rows.AddRange(new DevExpress.XtraReports.UI.XRTableRow[] {
            this.xrTableRow11,
            this.xrTableRow12,
            this.xrTableRow13,
            this.xrTableRow14,
            this.xrTableRow15,
            this.xrTableRow16,
            this.xrTableRow36});
        this.xrTable6.SizeF = new System.Drawing.SizeF(542.9391F, 97.59106F);
        this.xrTable6.StylePriority.UseBorders = false;
        // 
        // xrTableRow11
        // 
        this.xrTableRow11.Cells.AddRange(new DevExpress.XtraReports.UI.XRTableCell[] {
            this.xrTableCell26,
            this.xrTableCell25,
            this.xrTableCell109});
        this.xrTableRow11.Name = "xrTableRow11";
        this.xrTableRow11.Weight = 1;
        // 
        // xrTableCell26
        // 
        this.xrTableCell26.Borders = DevExpress.XtraPrinting.BorderSide.Bottom;
        this.xrTableCell26.Font = new System.Drawing.Font("Arial", 9F, System.Drawing.FontStyle.Bold);
        this.xrTableCell26.Name = "xrTableCell26";
        this.xrTableCell26.StylePriority.UseBorders = false;
        this.xrTableCell26.StylePriority.UseFont = false;
        this.xrTableCell26.Text = "   ATTENDANCE SALARY";
        this.xrTableCell26.Weight = 0.4352880418175753;
        // 
        // xrTableCell25
        // 
        this.xrTableCell25.Borders = ((DevExpress.XtraPrinting.BorderSide)(((DevExpress.XtraPrinting.BorderSide.Left | DevExpress.XtraPrinting.BorderSide.Right)
                    | DevExpress.XtraPrinting.BorderSide.Bottom)));
        this.xrTableCell25.Font = new System.Drawing.Font("Arial", 9F, System.Drawing.FontStyle.Bold);
        this.xrTableCell25.Name = "xrTableCell25";
        this.xrTableCell25.StylePriority.UseBorders = false;
        this.xrTableCell25.StylePriority.UseFont = false;
        this.xrTableCell25.Text = " OT SALARY";
        this.xrTableCell25.Weight = 0.33177431296642662;
        // 
        // xrTableCell109
        // 
        this.xrTableCell109.Borders = DevExpress.XtraPrinting.BorderSide.Bottom;
        this.xrTableCell109.Font = new System.Drawing.Font("Arial", 9F, System.Drawing.FontStyle.Bold);
        this.xrTableCell109.Name = "xrTableCell109";
        this.xrTableCell109.StylePriority.UseBorders = false;
        this.xrTableCell109.StylePriority.UseFont = false;
        this.xrTableCell109.Text = "  PENALTIES ";
        this.xrTableCell109.Weight = 0.30049702183691784;
        // 
        // xrTableRow12
        // 
        this.xrTableRow12.Cells.AddRange(new DevExpress.XtraReports.UI.XRTableCell[] {
            this.xrTableCell27,
            this.xrTableCell29,
            this.xrTableCell31,
            this.xrTableCell28,
            this.xrTableCell47,
            this.xrTableCell85,
            this.xrTableCell93,
            this.xrTableCell110,
            this.xrTableCell118});
        this.xrTableRow12.Name = "xrTableRow12";
        this.xrTableRow12.Weight = 1;
        // 
        // xrTableCell27
        // 
        this.xrTableCell27.Font = new System.Drawing.Font("Arial", 8F);
        this.xrTableCell27.Name = "xrTableCell27";
        this.xrTableCell27.StylePriority.UseFont = false;
        this.xrTableCell27.Text = "   Worked Days";
        this.xrTableCell27.Weight = 0.18594919233470408;
        // 
        // xrTableCell29
        // 
        this.xrTableCell29.Name = "xrTableCell29";
        this.xrTableCell29.Text = ":";
        this.xrTableCell29.Weight = 0.025472856998046835;
        // 
        // xrTableCell31
        // 
        this.xrTableCell31.DataBindings.AddRange(new DevExpress.XtraReports.UI.XRBinding[] {
            new DevExpress.XtraReports.UI.XRBinding("Text", null, "dtEmpPayslip.WorkedSal")});
        this.xrTableCell31.Name = "xrTableCell31";
        this.xrTableCell31.StylePriority.UseTextAlignment = false;
        this.xrTableCell31.Text = "xrTableCell21";
        this.xrTableCell31.TextAlignment = DevExpress.XtraPrinting.TextAlignment.TopLeft;
        this.xrTableCell31.Weight = 0.22386613506172504;
        // 
        // xrTableCell28
        // 
        this.xrTableCell28.Borders = DevExpress.XtraPrinting.BorderSide.Left;
        this.xrTableCell28.Font = new System.Drawing.Font("Arial", 8F);
        this.xrTableCell28.Name = "xrTableCell28";
        this.xrTableCell28.StylePriority.UseBorders = false;
        this.xrTableCell28.StylePriority.UseFont = false;
        this.xrTableCell28.Text = " Normal OT";
        this.xrTableCell28.Weight = 0.15616849737224356;
        // 
        // xrTableCell47
        // 
        this.xrTableCell47.Name = "xrTableCell47";
        this.xrTableCell47.Text = ":";
        this.xrTableCell47.Weight = 0.024270301359368855;
        // 
        // xrTableCell85
        // 
        this.xrTableCell85.Borders = DevExpress.XtraPrinting.BorderSide.Right;
        this.xrTableCell85.DataBindings.AddRange(new DevExpress.XtraReports.UI.XRBinding[] {
            new DevExpress.XtraReports.UI.XRBinding("Text", null, "dtEmpPayslip.NormalOTSal")});
        this.xrTableCell85.Name = "xrTableCell85";
        this.xrTableCell85.StylePriority.UseBorders = false;
        this.xrTableCell85.Text = "xrTableCell85";
        this.xrTableCell85.Weight = 0.15110972333795877;
        // 
        // xrTableCell93
        // 
        this.xrTableCell93.Font = new System.Drawing.Font("Arial", 8F);
        this.xrTableCell93.Name = "xrTableCell93";
        this.xrTableCell93.StylePriority.UseFont = false;
        this.xrTableCell93.Text = " Late";
        this.xrTableCell93.Weight = 0.087603078519234559;
        // 
        // xrTableCell110
        // 
        this.xrTableCell110.Name = "xrTableCell110";
        this.xrTableCell110.Text = ":";
        this.xrTableCell110.Weight = 0.019662777829249956;
        // 
        // xrTableCell118
        // 
        this.xrTableCell118.DataBindings.AddRange(new DevExpress.XtraReports.UI.XRBinding[] {
            new DevExpress.XtraReports.UI.XRBinding("Text", null, "dtEmpPayslip.LatePenaSal")});
        this.xrTableCell118.Name = "xrTableCell118";
        this.xrTableCell118.Text = "xrTableCell118";
        this.xrTableCell118.Weight = 0.1934568138083887;
        // 
        // xrTableRow13
        // 
        this.xrTableRow13.Cells.AddRange(new DevExpress.XtraReports.UI.XRTableCell[] {
            this.xrTableCell32,
            this.xrTableCell33,
            this.xrTableCell35,
            this.xrTableCell30,
            this.xrTableCell63,
            this.xrTableCell89,
            this.xrTableCell104,
            this.xrTableCell111,
            this.xrTableCell119});
        this.xrTableRow13.Name = "xrTableRow13";
        this.xrTableRow13.Weight = 1;
        // 
        // xrTableCell32
        // 
        this.xrTableCell32.Font = new System.Drawing.Font("Arial", 8F);
        this.xrTableCell32.Name = "xrTableCell32";
        this.xrTableCell32.StylePriority.UseFont = false;
        this.xrTableCell32.Text = "   Week Off";
        this.xrTableCell32.Weight = 0.18688653667863625;
        // 
        // xrTableCell33
        // 
        this.xrTableCell33.Name = "xrTableCell33";
        this.xrTableCell33.Text = ":";
        this.xrTableCell33.Weight = 0.024535510044127087;
        // 
        // xrTableCell35
        // 
        this.xrTableCell35.DataBindings.AddRange(new DevExpress.XtraReports.UI.XRBinding[] {
            new DevExpress.XtraReports.UI.XRBinding("Text", null, "dtEmpPayslip.WeekOffSal")});
        this.xrTableCell35.Name = "xrTableCell35";
        this.xrTableCell35.StylePriority.UseTextAlignment = false;
        this.xrTableCell35.Text = "xrTableCell22";
        this.xrTableCell35.TextAlignment = DevExpress.XtraPrinting.TextAlignment.TopLeft;
        this.xrTableCell35.Weight = 0.22386596997871794;
        // 
        // xrTableCell30
        // 
        this.xrTableCell30.Borders = DevExpress.XtraPrinting.BorderSide.Left;
        this.xrTableCell30.Font = new System.Drawing.Font("Arial", 8F);
        this.xrTableCell30.Name = "xrTableCell30";
        this.xrTableCell30.StylePriority.UseBorders = false;
        this.xrTableCell30.StylePriority.UseFont = false;
        this.xrTableCell30.Text = " Week Off OT";
        this.xrTableCell30.Weight = 0.15616876266240365;
        // 
        // xrTableCell63
        // 
        this.xrTableCell63.Name = "xrTableCell63";
        this.xrTableCell63.Text = ":";
        this.xrTableCell63.Weight = 0.024270241353877536;
        // 
        // xrTableCell89
        // 
        this.xrTableCell89.Borders = DevExpress.XtraPrinting.BorderSide.Right;
        this.xrTableCell89.DataBindings.AddRange(new DevExpress.XtraReports.UI.XRBinding[] {
            new DevExpress.XtraReports.UI.XRBinding("Text", null, "dtEmpPayslip.WeekOffOTSal")});
        this.xrTableCell89.Name = "xrTableCell89";
        this.xrTableCell89.StylePriority.UseBorders = false;
        this.xrTableCell89.Text = "xrTableCell89";
        this.xrTableCell89.Weight = 0.15110972507237963;
        // 
        // xrTableCell104
        // 
        this.xrTableCell104.Font = new System.Drawing.Font("Arial", 8F);
        this.xrTableCell104.Name = "xrTableCell104";
        this.xrTableCell104.StylePriority.UseFont = false;
        this.xrTableCell104.Text = " Early";
        this.xrTableCell104.Weight = 0.087603056091744527;
        // 
        // xrTableCell111
        // 
        this.xrTableCell111.Name = "xrTableCell111";
        this.xrTableCell111.Text = ":";
        this.xrTableCell111.Weight = 0.019662841291488833;
        // 
        // xrTableCell119
        // 
        this.xrTableCell119.DataBindings.AddRange(new DevExpress.XtraReports.UI.XRBinding[] {
            new DevExpress.XtraReports.UI.XRBinding("Text", null, "dtEmpPayslip.EarlyPenaSal")});
        this.xrTableCell119.Name = "xrTableCell119";
        this.xrTableCell119.Text = "xrTableCell119";
        this.xrTableCell119.Weight = 0.19345673344754491;
        // 
        // xrTableRow14
        // 
        this.xrTableRow14.Cells.AddRange(new DevExpress.XtraReports.UI.XRTableCell[] {
            this.xrTableCell36,
            this.xrTableCell37,
            this.xrTableCell39,
            this.xrTableCell34,
            this.xrTableCell81,
            this.xrTableCell67,
            this.xrTableCell105,
            this.xrTableCell112,
            this.xrTableCell120});
        this.xrTableRow14.Name = "xrTableRow14";
        this.xrTableRow14.Weight = 1;
        // 
        // xrTableCell36
        // 
        this.xrTableCell36.Font = new System.Drawing.Font("Arial", 8F);
        this.xrTableCell36.Name = "xrTableCell36";
        this.xrTableCell36.StylePriority.UseFont = false;
        this.xrTableCell36.Text = "   Holidays";
        this.xrTableCell36.Weight = 0.18702090112624492;
        // 
        // xrTableCell37
        // 
        this.xrTableCell37.Name = "xrTableCell37";
        this.xrTableCell37.Text = ":";
        this.xrTableCell37.Weight = 0.024401141341759469;
        // 
        // xrTableCell39
        // 
        this.xrTableCell39.DataBindings.AddRange(new DevExpress.XtraReports.UI.XRBinding[] {
            new DevExpress.XtraReports.UI.XRBinding("Text", null, "dtEmpPayslip.HolidaysSal")});
        this.xrTableCell39.Name = "xrTableCell39";
        this.xrTableCell39.StylePriority.UseTextAlignment = false;
        this.xrTableCell39.Text = "xrTableCell24";
        this.xrTableCell39.TextAlignment = DevExpress.XtraPrinting.TextAlignment.TopLeft;
        this.xrTableCell39.Weight = 0.22386597423347693;
        // 
        // xrTableCell34
        // 
        this.xrTableCell34.Borders = DevExpress.XtraPrinting.BorderSide.Left;
        this.xrTableCell34.Font = new System.Drawing.Font("Arial", 8F);
        this.xrTableCell34.Name = "xrTableCell34";
        this.xrTableCell34.StylePriority.UseBorders = false;
        this.xrTableCell34.StylePriority.UseFont = false;
        this.xrTableCell34.Text = " Holidays OT";
        this.xrTableCell34.Weight = 0.15616876266240359;
        // 
        // xrTableCell81
        // 
        this.xrTableCell81.Name = "xrTableCell81";
        this.xrTableCell81.Text = ":";
        this.xrTableCell81.Weight = 0.024270239673919898;
        // 
        // xrTableCell67
        // 
        this.xrTableCell67.Borders = DevExpress.XtraPrinting.BorderSide.Right;
        this.xrTableCell67.DataBindings.AddRange(new DevExpress.XtraReports.UI.XRBinding[] {
            new DevExpress.XtraReports.UI.XRBinding("Text", null, "dtEmpPayslip.HolidaysOTSal")});
        this.xrTableCell67.Name = "xrTableCell67";
        this.xrTableCell67.StylePriority.UseBorders = false;
        this.xrTableCell67.Weight = 0.151109731150787;
        // 
        // xrTableCell105
        // 
        this.xrTableCell105.Font = new System.Drawing.Font("Arial", 8F);
        this.xrTableCell105.Name = "xrTableCell105";
        this.xrTableCell105.StylePriority.UseFont = false;
        this.xrTableCell105.Text = " Partial";
        this.xrTableCell105.Weight = 0.08760293950791892;
        // 
        // xrTableCell112
        // 
        this.xrTableCell112.Name = "xrTableCell112";
        this.xrTableCell112.Text = ":";
        this.xrTableCell112.Weight = 0.019662719120937176;
        // 
        // xrTableCell120
        // 
        this.xrTableCell120.DataBindings.AddRange(new DevExpress.XtraReports.UI.XRBinding[] {
            new DevExpress.XtraReports.UI.XRBinding("Text", null, "dtEmpPayslip.PartialPenaSal")});
        this.xrTableCell120.Name = "xrTableCell120";
        this.xrTableCell120.Text = "xrTableCell120";
        this.xrTableCell120.Weight = 0.19345696780347235;
        // 
        // xrTableRow15
        // 
        this.xrTableRow15.Cells.AddRange(new DevExpress.XtraReports.UI.XRTableCell[] {
            this.xrTableCell40,
            this.xrTableCell41,
            this.xrTableCell43,
            this.xrTableCell38,
            this.xrTableCell71,
            this.xrTableCell97,
            this.xrTableCell107,
            this.xrTableCell113,
            this.xrTableCell123});
        this.xrTableRow15.Name = "xrTableRow15";
        this.xrTableRow15.Weight = 1;
        // 
        // xrTableCell40
        // 
        this.xrTableCell40.Borders = DevExpress.XtraPrinting.BorderSide.None;
        this.xrTableCell40.Font = new System.Drawing.Font("Arial", 8F);
        this.xrTableCell40.Name = "xrTableCell40";
        this.xrTableCell40.StylePriority.UseBorders = false;
        this.xrTableCell40.StylePriority.UseFont = false;
        this.xrTableCell40.Text = "   Leaves";
        this.xrTableCell40.Weight = 0.18702090112624492;
        // 
        // xrTableCell41
        // 
        this.xrTableCell41.Borders = DevExpress.XtraPrinting.BorderSide.None;
        this.xrTableCell41.Name = "xrTableCell41";
        this.xrTableCell41.StylePriority.UseBorders = false;
        this.xrTableCell41.Text = ":";
        this.xrTableCell41.Weight = 0.024401141341759469;
        // 
        // xrTableCell43
        // 
        this.xrTableCell43.Borders = DevExpress.XtraPrinting.BorderSide.None;
        this.xrTableCell43.DataBindings.AddRange(new DevExpress.XtraReports.UI.XRBinding[] {
            new DevExpress.XtraReports.UI.XRBinding("Text", null, "dtEmpPayslip.LeavedaysSal")});
        this.xrTableCell43.Name = "xrTableCell43";
        this.xrTableCell43.StylePriority.UseBorders = false;
        this.xrTableCell43.StylePriority.UseTextAlignment = false;
        this.xrTableCell43.TextAlignment = DevExpress.XtraPrinting.TextAlignment.TopLeft;
        this.xrTableCell43.Weight = 0.22386597423347693;
        // 
        // xrTableCell38
        // 
        this.xrTableCell38.Borders = DevExpress.XtraPrinting.BorderSide.Left;
        this.xrTableCell38.Font = new System.Drawing.Font("Arial", 8F);
        this.xrTableCell38.Name = "xrTableCell38";
        this.xrTableCell38.StylePriority.UseBorders = false;
        this.xrTableCell38.StylePriority.UseFont = false;
        this.xrTableCell38.Weight = 0.20576258124998126;
        // 
        // xrTableCell71
        // 
        this.xrTableCell71.Name = "xrTableCell71";
        this.xrTableCell71.Weight = 0.025881163716585665;
        // 
        // xrTableCell97
        // 
        this.xrTableCell97.Borders = DevExpress.XtraPrinting.BorderSide.Right;
        this.xrTableCell97.Name = "xrTableCell97";
        this.xrTableCell97.StylePriority.UseBorders = false;
        this.xrTableCell97.Weight = 0.099905007144806365;
        // 
        // xrTableCell107
        // 
        this.xrTableCell107.Font = new System.Drawing.Font("Arial", 8F);
        this.xrTableCell107.Name = "xrTableCell107";
        this.xrTableCell107.StylePriority.UseFont = false;
        this.xrTableCell107.Text = " Absent";
        this.xrTableCell107.Weight = 0.087602800741235692;
        // 
        // xrTableCell113
        // 
        this.xrTableCell113.Name = "xrTableCell113";
        this.xrTableCell113.Text = ":";
        this.xrTableCell113.Weight = 0.019663080792461384;
        // 
        // xrTableCell123
        // 
        this.xrTableCell123.DataBindings.AddRange(new DevExpress.XtraReports.UI.XRBinding[] {
            new DevExpress.XtraReports.UI.XRBinding("Text", null, "dtEmpPayslip.AbsentPenaSal")});
        this.xrTableCell123.Name = "xrTableCell123";
        this.xrTableCell123.Text = "xrTableCell123";
        this.xrTableCell123.Weight = 0.19345672627436866;
        // 
        // xrTableRow16
        // 
        this.xrTableRow16.Cells.AddRange(new DevExpress.XtraReports.UI.XRTableCell[] {
            this.xrTableCell44,
            this.xrTableCell45,
            this.xrTableCell42,
            this.xrTableCell102,
            this.xrTableCell82,
            this.xrTableCell124});
        this.xrTableRow16.Name = "xrTableRow16";
        this.xrTableRow16.Weight = 1;
        // 
        // xrTableCell44
        // 
        this.xrTableCell44.Borders = DevExpress.XtraPrinting.BorderSide.Top;
        this.xrTableCell44.Font = new System.Drawing.Font("Times New Roman", 9.75F, System.Drawing.FontStyle.Bold);
        this.xrTableCell44.Name = "xrTableCell44";
        this.xrTableCell44.StylePriority.UseBorders = false;
        this.xrTableCell44.StylePriority.UseFont = false;
        this.xrTableCell44.StylePriority.UseTextAlignment = false;
        this.xrTableCell44.Text = "  TOTAL:";
        this.xrTableCell44.TextAlignment = DevExpress.XtraPrinting.TextAlignment.TopLeft;
        this.xrTableCell44.Weight = 0.21142203059914005;
        // 
        // xrTableCell45
        // 
        this.xrTableCell45.Borders = DevExpress.XtraPrinting.BorderSide.Top;
        this.xrTableCell45.DataBindings.AddRange(new DevExpress.XtraReports.UI.XRBinding[] {
            new DevExpress.XtraReports.UI.XRBinding("Text", null, "dtEmpPayslip.totalAttendSal")});
        this.xrTableCell45.Font = new System.Drawing.Font("Times New Roman", 9.75F);
        this.xrTableCell45.Name = "xrTableCell45";
        this.xrTableCell45.StylePriority.UseBorders = false;
        this.xrTableCell45.StylePriority.UseFont = false;
        this.xrTableCell45.StylePriority.UseTextAlignment = false;
        this.xrTableCell45.TextAlignment = DevExpress.XtraPrinting.TextAlignment.TopLeft;
        this.xrTableCell45.Weight = 0.22386615638754148;
        // 
        // xrTableCell42
        // 
        this.xrTableCell42.Borders = DevExpress.XtraPrinting.BorderSide.Top;
        this.xrTableCell42.Name = "xrTableCell42";
        this.xrTableCell42.StylePriority.UseBorders = false;
        this.xrTableCell42.Weight = 0.18043874002793445;
        // 
        // xrTableCell102
        // 
        this.xrTableCell102.Borders = DevExpress.XtraPrinting.BorderSide.Top;
        this.xrTableCell102.DataBindings.AddRange(new DevExpress.XtraReports.UI.XRBinding[] {
            new DevExpress.XtraReports.UI.XRBinding("Text", null, "dtEmpPayslip.TotalOTSal")});
        this.xrTableCell102.Font = new System.Drawing.Font("Times New Roman", 9.75F);
        this.xrTableCell102.Name = "xrTableCell102";
        this.xrTableCell102.StylePriority.UseBorders = false;
        this.xrTableCell102.StylePriority.UseFont = false;
        this.xrTableCell102.StylePriority.UseTextAlignment = false;
        this.xrTableCell102.Text = "xrTableCell102";
        this.xrTableCell102.TextAlignment = DevExpress.XtraPrinting.TextAlignment.TopLeft;
        this.xrTableCell102.Weight = 0.15133541117928304;
        // 
        // xrTableCell82
        // 
        this.xrTableCell82.Borders = DevExpress.XtraPrinting.BorderSide.Top;
        this.xrTableCell82.Name = "xrTableCell82";
        this.xrTableCell82.StylePriority.UseBorders = false;
        this.xrTableCell82.Weight = 0.087377838395882257;
        // 
        // xrTableCell124
        // 
        this.xrTableCell124.Borders = DevExpress.XtraPrinting.BorderSide.Top;
        this.xrTableCell124.DataBindings.AddRange(new DevExpress.XtraReports.UI.XRBinding[] {
            new DevExpress.XtraReports.UI.XRBinding("Text", null, "dtEmpPayslip.TotalPenaltySal")});
        this.xrTableCell124.Font = new System.Drawing.Font("Times New Roman", 9.75F);
        this.xrTableCell124.Name = "xrTableCell124";
        this.xrTableCell124.StylePriority.UseBorders = false;
        this.xrTableCell124.StylePriority.UseFont = false;
        this.xrTableCell124.StylePriority.UseTextAlignment = false;
        this.xrTableCell124.Text = "xrTableCell124";
        this.xrTableCell124.TextAlignment = DevExpress.XtraPrinting.TextAlignment.TopLeft;
        this.xrTableCell124.Weight = 0.21311920003113902;
        // 
        // xrTableRow36
        // 
        this.xrTableRow36.Cells.AddRange(new DevExpress.XtraReports.UI.XRTableCell[] {
            this.xrTableCell121,
            this.xrTableCell122});
        this.xrTableRow36.Name = "xrTableRow36";
        this.xrTableRow36.Weight = 1;
        // 
        // xrTableCell121
        // 
        this.xrTableCell121.Borders = DevExpress.XtraPrinting.BorderSide.Top;
        this.xrTableCell121.Font = new System.Drawing.Font("Times New Roman", 9F, System.Drawing.FontStyle.Bold);
        this.xrTableCell121.Name = "xrTableCell121";
        this.xrTableCell121.StylePriority.UseBorders = false;
        this.xrTableCell121.StylePriority.UseFont = false;
        this.xrTableCell121.StylePriority.UseTextAlignment = false;
        this.xrTableCell121.Text = "        GROSS PAY:";
        this.xrTableCell121.TextAlignment = DevExpress.XtraPrinting.TextAlignment.TopRight;
        this.xrTableCell121.Weight = 0.8581265584392519;
        // 
        // xrTableCell122
        // 
        this.xrTableCell122.Borders = DevExpress.XtraPrinting.BorderSide.Top;
        this.xrTableCell122.DataBindings.AddRange(new DevExpress.XtraReports.UI.XRBinding[] {
            new DevExpress.XtraReports.UI.XRBinding("Text", null, "dtEmpPayslip.GrossAttendanceSal")});
        this.xrTableCell122.Font = new System.Drawing.Font("Times New Roman", 9.75F, System.Drawing.FontStyle.Bold);
        this.xrTableCell122.Name = "xrTableCell122";
        this.xrTableCell122.StylePriority.UseBorders = false;
        this.xrTableCell122.StylePriority.UseFont = false;
        this.xrTableCell122.Text = "xrTableCell122";
        this.xrTableCell122.Weight = 0.20943281818166851;
        // 
        // xrPanel9
        // 
        this.xrPanel9.Borders = ((DevExpress.XtraPrinting.BorderSide)((DevExpress.XtraPrinting.BorderSide.Left | DevExpress.XtraPrinting.BorderSide.Right)));
        this.xrPanel9.Controls.AddRange(new DevExpress.XtraReports.UI.XRControl[] {
            this.xrTable11});
        this.xrPanel9.LocationFloat = new DevExpress.Utils.PointFloat(0.7790883F, 0F);
        this.xrPanel9.Name = "xrPanel9";
        this.xrPanel9.SizeF = new System.Drawing.SizeF(543.1351F, 80.20841F);
        this.xrPanel9.StylePriority.UseBorders = false;
        // 
        // xrTable11
        // 
        this.xrTable11.Borders = DevExpress.XtraPrinting.BorderSide.None;
        this.xrTable11.LocationFloat = new DevExpress.Utils.PointFloat(7.450581E-09F, 0F);
        this.xrTable11.Name = "xrTable11";
        this.xrTable11.Rows.AddRange(new DevExpress.XtraReports.UI.XRTableRow[] {
            this.xrTableRow23,
            this.xrTableRow24,
            this.xrTableRow25,
            this.xrTableRow26,
            this.xrTableRow27,
            this.xrTableRow28,
            this.xrTableRow35});
        this.xrTable11.SizeF = new System.Drawing.SizeF(543.635F, 80.62509F);
        this.xrTable11.StylePriority.UseBorders = false;
        // 
        // xrTableRow23
        // 
        this.xrTableRow23.Cells.AddRange(new DevExpress.XtraReports.UI.XRTableCell[] {
            this.xrTableCell51});
        this.xrTableRow23.Name = "xrTableRow23";
        this.xrTableRow23.Weight = 1;
        // 
        // xrTableCell51
        // 
        this.xrTableCell51.Borders = DevExpress.XtraPrinting.BorderSide.Bottom;
        this.xrTableCell51.Font = new System.Drawing.Font("Arial", 9F, System.Drawing.FontStyle.Bold);
        this.xrTableCell51.Name = "xrTableCell51";
        this.xrTableCell51.StylePriority.UseBorders = false;
        this.xrTableCell51.StylePriority.UseFont = false;
        this.xrTableCell51.Text = "  ATTENDANCE";
        this.xrTableCell51.Weight = 3.7279183959960935;
        // 
        // xrTableRow24
        // 
        this.xrTableRow24.Cells.AddRange(new DevExpress.XtraReports.UI.XRTableCell[] {
            this.xrTableCell53,
            this.xrTableCell73,
            this.xrTableCell54});
        this.xrTableRow24.Name = "xrTableRow24";
        this.xrTableRow24.Weight = 1;
        // 
        // xrTableCell53
        // 
        this.xrTableCell53.Font = new System.Drawing.Font("Arial", 8F);
        this.xrTableCell53.Name = "xrTableCell53";
        this.xrTableCell53.StylePriority.UseFont = false;
        this.xrTableCell53.Text = "   Days Present";
        this.xrTableCell53.Weight = 0.56859380161528073;
        // 
        // xrTableCell73
        // 
        this.xrTableCell73.Name = "xrTableCell73";
        this.xrTableCell73.Text = ":";
        this.xrTableCell73.Weight = 2.561295779769313;
        // 
        // xrTableCell54
        // 
        this.xrTableCell54.DataBindings.AddRange(new DevExpress.XtraReports.UI.XRBinding[] {
            new DevExpress.XtraReports.UI.XRBinding("Text", null, "dtEmpPayslip.DaysPresent")});
        this.xrTableCell54.Font = new System.Drawing.Font("Arial", 9F);
        this.xrTableCell54.Name = "xrTableCell54";
        this.xrTableCell54.StylePriority.UseFont = false;
        this.xrTableCell54.Weight = 0.59802881461149981;
        // 
        // xrTableRow25
        // 
        this.xrTableRow25.Cells.AddRange(new DevExpress.XtraReports.UI.XRTableCell[] {
            this.xrTableCell55,
            this.xrTableCell74,
            this.xrTableCell56});
        this.xrTableRow25.Name = "xrTableRow25";
        this.xrTableRow25.Weight = 1;
        // 
        // xrTableCell55
        // 
        this.xrTableCell55.Font = new System.Drawing.Font("Arial", 8F);
        this.xrTableCell55.Name = "xrTableCell55";
        this.xrTableCell55.StylePriority.UseFont = false;
        this.xrTableCell55.Text = "   Week Off";
        this.xrTableCell55.Weight = 0.56859380161528073;
        // 
        // xrTableCell74
        // 
        this.xrTableCell74.Name = "xrTableCell74";
        this.xrTableCell74.Text = ":";
        this.xrTableCell74.Weight = 2.5541362003950487;
        // 
        // xrTableCell56
        // 
        this.xrTableCell56.DataBindings.AddRange(new DevExpress.XtraReports.UI.XRBinding[] {
            new DevExpress.XtraReports.UI.XRBinding("Text", null, "dtEmpPayslip.WeekOff")});
        this.xrTableCell56.Font = new System.Drawing.Font("Arial", 9F);
        this.xrTableCell56.Name = "xrTableCell56";
        this.xrTableCell56.StylePriority.UseFont = false;
        this.xrTableCell56.Weight = 0.60518839398576485;
        // 
        // xrTableRow26
        // 
        this.xrTableRow26.Cells.AddRange(new DevExpress.XtraReports.UI.XRTableCell[] {
            this.xrTableCell57,
            this.xrTableCell75,
            this.xrTableCell58});
        this.xrTableRow26.Name = "xrTableRow26";
        this.xrTableRow26.Weight = 1;
        // 
        // xrTableCell57
        // 
        this.xrTableCell57.Font = new System.Drawing.Font("Arial", 8F);
        this.xrTableCell57.Name = "xrTableCell57";
        this.xrTableCell57.StylePriority.UseFont = false;
        this.xrTableCell57.Text = "   Days Absent";
        this.xrTableCell57.Weight = 0.56859380161528073;
        // 
        // xrTableCell75
        // 
        this.xrTableCell75.Name = "xrTableCell75";
        this.xrTableCell75.Text = ":";
        this.xrTableCell75.Weight = 2.561295779769313;
        // 
        // xrTableCell58
        // 
        this.xrTableCell58.DataBindings.AddRange(new DevExpress.XtraReports.UI.XRBinding[] {
            new DevExpress.XtraReports.UI.XRBinding("Text", null, "dtEmpPayslip.daysAbsent")});
        this.xrTableCell58.Font = new System.Drawing.Font("Arial", 9F);
        this.xrTableCell58.Name = "xrTableCell58";
        this.xrTableCell58.StylePriority.UseFont = false;
        this.xrTableCell58.Weight = 0.59802881461149981;
        // 
        // xrTableRow27
        // 
        this.xrTableRow27.Cells.AddRange(new DevExpress.XtraReports.UI.XRTableCell[] {
            this.xrTableCell59,
            this.xrTableCell76,
            this.xrTableCell60});
        this.xrTableRow27.Name = "xrTableRow27";
        this.xrTableRow27.Weight = 1;
        // 
        // xrTableCell59
        // 
        this.xrTableCell59.Font = new System.Drawing.Font("Arial", 8F);
        this.xrTableCell59.Name = "xrTableCell59";
        this.xrTableCell59.StylePriority.UseFont = false;
        this.xrTableCell59.Text = "   Holiday";
        this.xrTableCell59.Weight = 0.56859380161528073;
        // 
        // xrTableCell76
        // 
        this.xrTableCell76.Name = "xrTableCell76";
        this.xrTableCell76.Text = ":";
        this.xrTableCell76.Weight = 2.561295779769313;
        // 
        // xrTableCell60
        // 
        this.xrTableCell60.DataBindings.AddRange(new DevExpress.XtraReports.UI.XRBinding[] {
            new DevExpress.XtraReports.UI.XRBinding("Text", null, "dtEmpPayslip.Holiday")});
        this.xrTableCell60.Font = new System.Drawing.Font("Arial", 9F);
        this.xrTableCell60.Name = "xrTableCell60";
        this.xrTableCell60.StylePriority.UseFont = false;
        this.xrTableCell60.Weight = 0.59802881461149981;
        // 
        // xrTableRow28
        // 
        this.xrTableRow28.Cells.AddRange(new DevExpress.XtraReports.UI.XRTableCell[] {
            this.xrTableCell61,
            this.xrTableCell77,
            this.xrTableCell62});
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
        this.xrTableCell61.Text = "   Leaves";
        this.xrTableCell61.Weight = 0.56859380161528073;
        // 
        // xrTableCell77
        // 
        this.xrTableCell77.Borders = DevExpress.XtraPrinting.BorderSide.Bottom;
        this.xrTableCell77.Name = "xrTableCell77";
        this.xrTableCell77.StylePriority.UseBorders = false;
        this.xrTableCell77.Text = ":";
        this.xrTableCell77.Weight = 2.561295779769313;
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
        this.xrTableCell62.Weight = 0.59802881461149981;
        // 
        // xrTableRow35
        // 
        this.xrTableRow35.Cells.AddRange(new DevExpress.XtraReports.UI.XRTableCell[] {
            this.xrTableCell101,
            this.xrTableCell103});
        this.xrTableRow35.Name = "xrTableRow35";
        this.xrTableRow35.Weight = 1;
        // 
        // xrTableCell101
        // 
        this.xrTableCell101.Font = new System.Drawing.Font("Arial", 9F, System.Drawing.FontStyle.Bold);
        this.xrTableCell101.Name = "xrTableCell101";
        this.xrTableCell101.StylePriority.UseFont = false;
        this.xrTableCell101.StylePriority.UseTextAlignment = false;
        this.xrTableCell101.Text = "TOTAL DAYS:";
        this.xrTableCell101.TextAlignment = DevExpress.XtraPrinting.TextAlignment.TopRight;
        this.xrTableCell101.Weight = 3.1306815345342986;
        // 
        // xrTableCell103
        // 
        this.xrTableCell103.DataBindings.AddRange(new DevExpress.XtraReports.UI.XRBinding[] {
            new DevExpress.XtraReports.UI.XRBinding("Text", null, "dtEmpPayslip.Totaldays")});
        this.xrTableCell103.Font = new System.Drawing.Font("Times New Roman", 9.75F, System.Drawing.FontStyle.Bold);
        this.xrTableCell103.Name = "xrTableCell103";
        this.xrTableCell103.StylePriority.UseFont = false;
        this.xrTableCell103.Text = "xrTableCell103";
        this.xrTableCell103.Weight = 0.59723686146179611;
        // 
        // Employee_Pay_Slip_Report
        // 
        this.Bands.AddRange(new DevExpress.XtraReports.UI.Band[] {
            this.Detail,
            this.TopMargin,
            this.BottomMargin,
            this.GroupFooter1,
            this.PageFooter,
            this.GroupHeader1,
            this.GroupHeader4,
            this.GroupFooter2,
            this.GroupHeader2});
        this.Borders = ((DevExpress.XtraPrinting.BorderSide)((((DevExpress.XtraPrinting.BorderSide.Left | DevExpress.XtraPrinting.BorderSide.Top)
                    | DevExpress.XtraPrinting.BorderSide.Right)
                    | DevExpress.XtraPrinting.BorderSide.Bottom)));
        this.DataMember = "dtEmpPayslip";
        this.DataSource = this.employeePaySlipDataSet1;
        this.Font = new System.Drawing.Font("Times New Roman", 9.75F);
        this.Margins = new System.Drawing.Printing.Margins(122, 141, 120, 0);
        this.Version = "10.2";
        ((System.ComponentModel.ISupportInitialize)(this.xrTable2)).EndInit();
        ((System.ComponentModel.ISupportInitialize)(this.employeePaySlipDataSet1)).EndInit();
        ((System.ComponentModel.ISupportInitialize)(this.xrTable4)).EndInit();
        ((System.ComponentModel.ISupportInitialize)(this.xrTable3)).EndInit();
        ((System.ComponentModel.ISupportInitialize)(this.xrTable1)).EndInit();
        ((System.ComponentModel.ISupportInitialize)(this.xrTable7)).EndInit();
        ((System.ComponentModel.ISupportInitialize)(this.xrTable6)).EndInit();
        ((System.ComponentModel.ISupportInitialize)(this.xrTable11)).EndInit();
        ((System.ComponentModel.ISupportInitialize)(this)).EndInit();

	}

	#endregion

    double netsalary = 0;
    double totalallowclaim = 0;

    double total = 0;

    private void xrLabel10_SummaryReset(object sender, EventArgs e)
    {
        netsalary = 0;
        

    }

    private void xrLabel10_SummaryRowChanged(object sender, EventArgs e)
    {

        if (GetCurrentColumnValue("NetSalary").ToString() != null && GetCurrentColumnValue("NetSalary").ToString() != "")
        {
            netsalary = Convert.ToDouble(GetCurrentColumnValue("NetSalary"));
        }

    }
    private void xrLabel10_SummaryGetResult(object sender, SummaryGetResultEventArgs e)
    {

        e.Result = netsalary;
        e.Handled = true;
    }

   
   


}
