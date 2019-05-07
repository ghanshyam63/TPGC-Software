using System;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using DevExpress.XtraReports.UI;

/// <summary>
/// Summary description for EmployeeClaimreport
/// </summary>
public class EmployeeClaimreport : DevExpress.XtraReports.UI.XtraReport
{
	private DevExpress.XtraReports.UI.DetailBand Detail;
	private DevExpress.XtraReports.UI.TopMarginBand TopMargin;
	private DevExpress.XtraReports.UI.BottomMarginBand BottomMargin;
    private EmployeeClaimApproved employeeClaimApproved1;
    private XRPictureBox xrPictureBox1;
    private XRTable xrTable1;
    private XRTableRow xrTableRow1;
    private XRTableCell xrTableCell1;
    private XRTableCell xrTableCell2;
    private XRTableCell xrTableCell3;
    private XRTableCell xrTableCell4;
    private XRTable xrTable2;
    private XRTableRow xrTableRow2;
    private XRTableCell xrTableCell5;
    private XRTableCell xrTableCell6;
    private XRTableCell xrTableCell7;
    private XRTableCell xrTableCell8;
    private XRLabel xrLabel3;
    private XRLabel xrLabel4;
    private XRLabel xrLabel2;
    private XRLabel xrLabel5;
    private XRLabel xrLabel1;
    private FormattingRule formattingRule1;
    private GroupFooterBand GroupFooter1;
    private XRLabel xrLabel6;
    private XRLabel xrLabel7;
    private XRLabel xrLabel8;
    private PageFooterBand PageFooter;
    private XRPageInfo xrPageInfo1;
    private XRPageInfo xrPageInfo2;
	/// <summary>
	/// Required designer variable.
	/// </summary>
	private System.ComponentModel.IContainer components = null;

	public EmployeeClaimreport()
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
    public void SetCompanyName(string Name)
    {
        xrLabel1.Text = Name;
    }
    public void SetAddress(string Address)
    {
        xrLabel5.Text = Address;

    }
	#region Designer generated code

	/// <summary>
	/// Required method for Designer support - do not modify
	/// the contents of this method with the code editor.
	/// </summary>
	private void InitializeComponent() {
        string resourceFileName = "EmployeeClaimreport.resx";
        System.Resources.ResourceManager resources = global::Resources.EmployeeClaimreport.ResourceManager;
        DevExpress.XtraReports.UI.XRSummary xrSummary1 = new DevExpress.XtraReports.UI.XRSummary();
        this.Detail = new DevExpress.XtraReports.UI.DetailBand();
        this.xrTable2 = new DevExpress.XtraReports.UI.XRTable();
        this.xrTableRow2 = new DevExpress.XtraReports.UI.XRTableRow();
        this.xrTableCell5 = new DevExpress.XtraReports.UI.XRTableCell();
        this.xrTableCell6 = new DevExpress.XtraReports.UI.XRTableCell();
        this.xrTableCell7 = new DevExpress.XtraReports.UI.XRTableCell();
        this.xrTableCell8 = new DevExpress.XtraReports.UI.XRTableCell();
        this.TopMargin = new DevExpress.XtraReports.UI.TopMarginBand();
        this.xrLabel1 = new DevExpress.XtraReports.UI.XRLabel();
        this.xrLabel5 = new DevExpress.XtraReports.UI.XRLabel();
        this.xrLabel2 = new DevExpress.XtraReports.UI.XRLabel();
        this.xrLabel4 = new DevExpress.XtraReports.UI.XRLabel();
        this.xrLabel3 = new DevExpress.XtraReports.UI.XRLabel();
        this.xrTable1 = new DevExpress.XtraReports.UI.XRTable();
        this.xrTableRow1 = new DevExpress.XtraReports.UI.XRTableRow();
        this.xrTableCell1 = new DevExpress.XtraReports.UI.XRTableCell();
        this.xrTableCell2 = new DevExpress.XtraReports.UI.XRTableCell();
        this.xrTableCell3 = new DevExpress.XtraReports.UI.XRTableCell();
        this.xrTableCell4 = new DevExpress.XtraReports.UI.XRTableCell();
        this.xrPictureBox1 = new DevExpress.XtraReports.UI.XRPictureBox();
        this.BottomMargin = new DevExpress.XtraReports.UI.BottomMarginBand();
        this.employeeClaimApproved1 = new EmployeeClaimApproved();
        this.formattingRule1 = new DevExpress.XtraReports.UI.FormattingRule();
        this.GroupFooter1 = new DevExpress.XtraReports.UI.GroupFooterBand();
        this.xrLabel7 = new DevExpress.XtraReports.UI.XRLabel();
        this.xrLabel6 = new DevExpress.XtraReports.UI.XRLabel();
        this.xrLabel8 = new DevExpress.XtraReports.UI.XRLabel();
        this.PageFooter = new DevExpress.XtraReports.UI.PageFooterBand();
        this.xrPageInfo1 = new DevExpress.XtraReports.UI.XRPageInfo();
        this.xrPageInfo2 = new DevExpress.XtraReports.UI.XRPageInfo();
        ((System.ComponentModel.ISupportInitialize)(this.xrTable2)).BeginInit();
        ((System.ComponentModel.ISupportInitialize)(this.xrTable1)).BeginInit();
        ((System.ComponentModel.ISupportInitialize)(this.employeeClaimApproved1)).BeginInit();
        ((System.ComponentModel.ISupportInitialize)(this)).BeginInit();
        // 
        // Detail
        // 
        this.Detail.Controls.AddRange(new DevExpress.XtraReports.UI.XRControl[] {
            this.xrTable2});
        this.Detail.HeightF = 16.04166F;
        this.Detail.Name = "Detail";
        this.Detail.Padding = new DevExpress.XtraPrinting.PaddingInfo(0, 0, 0, 0, 100F);
        this.Detail.SortFields.AddRange(new DevExpress.XtraReports.UI.GroupField[] {
            new DevExpress.XtraReports.UI.GroupField("Emp_Name", DevExpress.XtraReports.UI.XRColumnSortOrder.Ascending),
            new DevExpress.XtraReports.UI.GroupField("Sum_Claim", DevExpress.XtraReports.UI.XRColumnSortOrder.Descending)});
        this.Detail.TextAlignment = DevExpress.XtraPrinting.TextAlignment.TopLeft;
        // 
        // xrTable2
        // 
        this.xrTable2.Borders = ((DevExpress.XtraPrinting.BorderSide)(((DevExpress.XtraPrinting.BorderSide.Left | DevExpress.XtraPrinting.BorderSide.Right)
                    | DevExpress.XtraPrinting.BorderSide.Bottom)));
        this.xrTable2.Font = new System.Drawing.Font("Verdana", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
        this.xrTable2.LocationFloat = new DevExpress.Utils.PointFloat(0F, 0F);
        this.xrTable2.Name = "xrTable2";
        this.xrTable2.Rows.AddRange(new DevExpress.XtraReports.UI.XRTableRow[] {
            this.xrTableRow2});
        this.xrTable2.SizeF = new System.Drawing.SizeF(639.9999F, 14.99999F);
        this.xrTable2.StylePriority.UseBorders = false;
        this.xrTable2.StylePriority.UseFont = false;
        // 
        // xrTableRow2
        // 
        this.xrTableRow2.Cells.AddRange(new DevExpress.XtraReports.UI.XRTableCell[] {
            this.xrTableCell5,
            this.xrTableCell6,
            this.xrTableCell7,
            this.xrTableCell8});
        this.xrTableRow2.Name = "xrTableRow2";
        this.xrTableRow2.Weight = 1;
        // 
        // xrTableCell5
        // 
        this.xrTableCell5.DataBindings.AddRange(new DevExpress.XtraReports.UI.XRBinding[] {
            new DevExpress.XtraReports.UI.XRBinding("Text", null, "EmployeeClaim.Emp_Id")});
        this.xrTableCell5.Name = "xrTableCell5";
        this.xrTableCell5.StylePriority.UseTextAlignment = false;
        this.xrTableCell5.Text = "xrTableCell5";
        this.xrTableCell5.TextAlignment = DevExpress.XtraPrinting.TextAlignment.TopCenter;
        this.xrTableCell5.Weight = 1;
        // 
        // xrTableCell6
        // 
        this.xrTableCell6.DataBindings.AddRange(new DevExpress.XtraReports.UI.XRBinding[] {
            new DevExpress.XtraReports.UI.XRBinding("Text", null, "EmployeeClaim.Emp_Name")});
        this.xrTableCell6.Name = "xrTableCell6";
        this.xrTableCell6.Text = "xrTableCell6";
        this.xrTableCell6.Weight = 1.9062503051757813;
        // 
        // xrTableCell7
        // 
        this.xrTableCell7.DataBindings.AddRange(new DevExpress.XtraReports.UI.XRBinding[] {
            new DevExpress.XtraReports.UI.XRBinding("Text", null, "EmployeeClaim.Claim_Name")});
        this.xrTableCell7.Name = "xrTableCell7";
        this.xrTableCell7.Text = "xrTableCell7";
        this.xrTableCell7.Weight = 1.9583331298828124;
        // 
        // xrTableCell8
        // 
        this.xrTableCell8.DataBindings.AddRange(new DevExpress.XtraReports.UI.XRBinding[] {
            new DevExpress.XtraReports.UI.XRBinding("Text", null, "EmployeeClaim.Claim_Amount")});
        this.xrTableCell8.Name = "xrTableCell8";
        this.xrTableCell8.StylePriority.UseTextAlignment = false;
        this.xrTableCell8.Text = "xrTableCell8";
        this.xrTableCell8.TextAlignment = DevExpress.XtraPrinting.TextAlignment.TopCenter;
        this.xrTableCell8.Weight = 1.5354159545898436;
        // 
        // TopMargin
        // 
        this.TopMargin.Controls.AddRange(new DevExpress.XtraReports.UI.XRControl[] {
            this.xrLabel1,
            this.xrLabel5,
            this.xrLabel2,
            this.xrLabel4,
            this.xrLabel3,
            this.xrTable1,
            this.xrPictureBox1});
        this.TopMargin.HeightF = 94.58336F;
        this.TopMargin.Name = "TopMargin";
        this.TopMargin.Padding = new DevExpress.XtraPrinting.PaddingInfo(0, 0, 0, 0, 100F);
        this.TopMargin.TextAlignment = DevExpress.XtraPrinting.TextAlignment.TopLeft;
        // 
        // xrLabel1
        // 
        this.xrLabel1.Font = new System.Drawing.Font("Verdana", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
        this.xrLabel1.LocationFloat = new DevExpress.Utils.PointFloat(0F, 13.33332F);
        this.xrLabel1.Name = "xrLabel1";
        this.xrLabel1.Padding = new DevExpress.XtraPrinting.PaddingInfo(2, 2, 0, 0, 100F);
        this.xrLabel1.SizeF = new System.Drawing.SizeF(446.875F, 13.625F);
        this.xrLabel1.StylePriority.UseFont = false;
        this.xrLabel1.Text = "xrLabel1";
        // 
        // xrLabel5
        // 
        this.xrLabel5.Font = new System.Drawing.Font("Verdana", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
        this.xrLabel5.LocationFloat = new DevExpress.Utils.PointFloat(4.768372E-05F, 26.95832F);
        this.xrLabel5.Name = "xrLabel5";
        this.xrLabel5.Padding = new DevExpress.XtraPrinting.PaddingInfo(2, 2, 0, 0, 100F);
        this.xrLabel5.SizeF = new System.Drawing.SizeF(446.8749F, 14.66667F);
        this.xrLabel5.StylePriority.UseFont = false;
        this.xrLabel5.Text = "xrLabel5";
        // 
        // xrLabel2
        // 
        this.xrLabel2.Font = new System.Drawing.Font("Verdana", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
        this.xrLabel2.LocationFloat = new DevExpress.Utils.PointFloat(232.2917F, 41.62499F);
        this.xrLabel2.Name = "xrLabel2";
        this.xrLabel2.Padding = new DevExpress.XtraPrinting.PaddingInfo(2, 2, 0, 0, 100F);
        this.xrLabel2.SizeF = new System.Drawing.SizeF(214.5833F, 12.58334F);
        this.xrLabel2.StylePriority.UseFont = false;
        this.xrLabel2.StylePriority.UseTextAlignment = false;
        this.xrLabel2.Text = "xrLabel2";
        this.xrLabel2.TextAlignment = DevExpress.XtraPrinting.TextAlignment.TopCenter;
        // 
        // xrLabel4
        // 
        this.xrLabel4.DataBindings.AddRange(new DevExpress.XtraReports.UI.XRBinding[] {
            new DevExpress.XtraReports.UI.XRBinding("Text", null, "EmployeeClaim.Year")});
        this.xrLabel4.Font = new System.Drawing.Font("Verdana", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
        this.xrLabel4.LocationFloat = new DevExpress.Utils.PointFloat(99.99998F, 41.62499F);
        this.xrLabel4.Name = "xrLabel4";
        this.xrLabel4.Padding = new DevExpress.XtraPrinting.PaddingInfo(2, 2, 0, 0, 100F);
        this.xrLabel4.SizeF = new System.Drawing.SizeF(100F, 12.58333F);
        this.xrLabel4.StylePriority.UseFont = false;
        this.xrLabel4.Text = "xrLabel4";
        // 
        // xrLabel3
        // 
        this.xrLabel3.DataBindings.AddRange(new DevExpress.XtraReports.UI.XRBinding[] {
            new DevExpress.XtraReports.UI.XRBinding("Text", null, "EmployeeClaim.Month")});
        this.xrLabel3.Font = new System.Drawing.Font("Verdana", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
        this.xrLabel3.LocationFloat = new DevExpress.Utils.PointFloat(0F, 41.62499F);
        this.xrLabel3.Name = "xrLabel3";
        this.xrLabel3.Padding = new DevExpress.XtraPrinting.PaddingInfo(2, 2, 0, 0, 100F);
        this.xrLabel3.SizeF = new System.Drawing.SizeF(100F, 12.58332F);
        this.xrLabel3.StylePriority.UseFont = false;
        this.xrLabel3.Text = "xrLabel3";
        // 
        // xrTable1
        // 
        this.xrTable1.Font = new System.Drawing.Font("Verdana", 8.25F, System.Drawing.FontStyle.Bold);
        this.xrTable1.LocationFloat = new DevExpress.Utils.PointFloat(4.768372E-05F, 74.74995F);
        this.xrTable1.Name = "xrTable1";
        this.xrTable1.Rows.AddRange(new DevExpress.XtraReports.UI.XRTableRow[] {
            this.xrTableRow1});
        this.xrTable1.SizeF = new System.Drawing.SizeF(639.9999F, 15.20837F);
        this.xrTable1.StylePriority.UseBorders = false;
        this.xrTable1.StylePriority.UseFont = false;
        // 
        // xrTableRow1
        // 
        this.xrTableRow1.Borders = ((DevExpress.XtraPrinting.BorderSide)((((DevExpress.XtraPrinting.BorderSide.Left | DevExpress.XtraPrinting.BorderSide.Top)
                    | DevExpress.XtraPrinting.BorderSide.Right)
                    | DevExpress.XtraPrinting.BorderSide.Bottom)));
        this.xrTableRow1.Cells.AddRange(new DevExpress.XtraReports.UI.XRTableCell[] {
            this.xrTableCell1,
            this.xrTableCell2,
            this.xrTableCell3,
            this.xrTableCell4});
        this.xrTableRow1.Name = "xrTableRow1";
        this.xrTableRow1.StylePriority.UseBorders = false;
        this.xrTableRow1.Weight = 1;
        // 
        // xrTableCell1
        // 
        this.xrTableCell1.Name = "xrTableCell1";
        this.xrTableCell1.StylePriority.UseTextAlignment = false;
        this.xrTableCell1.Text = "Employee Id";
        this.xrTableCell1.TextAlignment = DevExpress.XtraPrinting.TextAlignment.TopCenter;
        this.xrTableCell1.Weight = 1;
        // 
        // xrTableCell2
        // 
        this.xrTableCell2.Name = "xrTableCell2";
        this.xrTableCell2.Text = "Employee Name";
        this.xrTableCell2.Weight = 1.90625;
        // 
        // xrTableCell3
        // 
        this.xrTableCell3.Font = new System.Drawing.Font("Verdana", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
        this.xrTableCell3.Name = "xrTableCell3";
        this.xrTableCell3.StylePriority.UseFont = false;
        this.xrTableCell3.Text = "Claim Name";
        this.xrTableCell3.Weight = 1.9583330674988213;
        // 
        // xrTableCell4
        // 
        this.xrTableCell4.Name = "xrTableCell4";
        this.xrTableCell4.StylePriority.UseTextAlignment = false;
        this.xrTableCell4.Text = "Claim Amount";
        this.xrTableCell4.TextAlignment = DevExpress.XtraPrinting.TextAlignment.TopCenter;
        this.xrTableCell4.Weight = 1.535415976845782;
        // 
        // xrPictureBox1
        // 
        this.xrPictureBox1.Image = ((System.Drawing.Image)(resources.GetObject("xrPictureBox1.Image")));
        this.xrPictureBox1.LocationFloat = new DevExpress.Utils.PointFloat(521.875F, 10F);
        this.xrPictureBox1.Name = "xrPictureBox1";
        this.xrPictureBox1.SizeF = new System.Drawing.SizeF(102.4999F, 60.37499F);
        this.xrPictureBox1.Sizing = DevExpress.XtraPrinting.ImageSizeMode.StretchImage;
        // 
        // BottomMargin
        // 
        this.BottomMargin.HeightF = 14.79168F;
        this.BottomMargin.Name = "BottomMargin";
        this.BottomMargin.Padding = new DevExpress.XtraPrinting.PaddingInfo(0, 0, 0, 0, 100F);
        this.BottomMargin.TextAlignment = DevExpress.XtraPrinting.TextAlignment.TopLeft;
        // 
        // employeeClaimApproved1
        // 
        this.employeeClaimApproved1.DataSetName = "EmployeeClaimApproved";
        this.employeeClaimApproved1.SchemaSerializationMode = System.Data.SchemaSerializationMode.IncludeSchema;
        // 
        // formattingRule1
        // 
        this.formattingRule1.DataMember = "EmployeeClaim";
        this.formattingRule1.DataSource = this.employeeClaimApproved1;
        this.formattingRule1.Name = "formattingRule1";
        // 
        // GroupFooter1
        // 
        this.GroupFooter1.Controls.AddRange(new DevExpress.XtraReports.UI.XRControl[] {
            this.xrLabel8,
            this.xrLabel7,
            this.xrLabel6});
        this.GroupFooter1.HeightF = 15.16666F;
        this.GroupFooter1.KeepTogether = true;
        this.GroupFooter1.Name = "GroupFooter1";
        // 
        // xrLabel7
        // 
        this.xrLabel7.Font = new System.Drawing.Font("Verdana", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
        this.xrLabel7.LocationFloat = new DevExpress.Utils.PointFloat(145.4168F, 0F);
        this.xrLabel7.Name = "xrLabel7";
        this.xrLabel7.Padding = new DevExpress.XtraPrinting.PaddingInfo(2, 2, 0, 0, 100F);
        this.xrLabel7.SizeF = new System.Drawing.SizeF(147.9167F, 13F);
        this.xrLabel7.StylePriority.UseFont = false;
        this.xrLabel7.Text = "Total Claim Amount :";
        this.xrLabel7.Visible = false;
        // 
        // xrLabel6
        // 
        this.xrLabel6.Font = new System.Drawing.Font("Verdana", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
        this.xrLabel6.LocationFloat = new DevExpress.Utils.PointFloat(293.3335F, 0F);
        this.xrLabel6.Name = "xrLabel6";
        this.xrLabel6.Padding = new DevExpress.XtraPrinting.PaddingInfo(2, 2, 0, 0, 100F);
        this.xrLabel6.SizeF = new System.Drawing.SizeF(153.5415F, 13F);
        this.xrLabel6.StylePriority.UseFont = false;
        this.xrLabel6.Text = "xrLabel6";
        this.xrLabel6.Visible = false;
        this.xrLabel6.BeforePrint += new System.Drawing.Printing.PrintEventHandler(this.xrLabel6_BeforePrint);
        // 
        // xrLabel8
        // 
        this.xrLabel8.DataBindings.AddRange(new DevExpress.XtraReports.UI.XRBinding[] {
            new DevExpress.XtraReports.UI.XRBinding("Text", null, "EmployeeClaim.Claim_Amount")});
        this.xrLabel8.LocationFloat = new DevExpress.Utils.PointFloat(486.4583F, 0F);
        this.xrLabel8.Name = "xrLabel8";
        this.xrLabel8.Padding = new DevExpress.XtraPrinting.PaddingInfo(2, 2, 0, 0, 96F);
        this.xrLabel8.SizeF = new System.Drawing.SizeF(153.5415F, 13F);
        this.xrLabel8.StylePriority.UseTextAlignment = false;
        xrSummary1.Running = DevExpress.XtraReports.UI.SummaryRunning.Report;
        this.xrLabel8.Summary = xrSummary1;
        this.xrLabel8.Text = "xrLabel8";
        this.xrLabel8.TextAlignment = DevExpress.XtraPrinting.TextAlignment.TopCenter;
        // 
        // PageFooter
        // 
        this.PageFooter.Controls.AddRange(new DevExpress.XtraReports.UI.XRControl[] {
            this.xrPageInfo2,
            this.xrPageInfo1});
        this.PageFooter.HeightF = 22.99998F;
        this.PageFooter.Name = "PageFooter";
        // 
        // xrPageInfo1
        // 
        this.xrPageInfo1.LocationFloat = new DevExpress.Utils.PointFloat(0F, 0F);
        this.xrPageInfo1.Name = "xrPageInfo1";
        this.xrPageInfo1.Padding = new DevExpress.XtraPrinting.PaddingInfo(2, 2, 0, 0, 96F);
        this.xrPageInfo1.PageInfo = DevExpress.XtraPrinting.PageInfo.DateTime;
        this.xrPageInfo1.SizeF = new System.Drawing.SizeF(321.875F, 22.99998F);
        // 
        // xrPageInfo2
        // 
        this.xrPageInfo2.Format = "Page{0}Of {1}";
        this.xrPageInfo2.LocationFloat = new DevExpress.Utils.PointFloat(321.875F, 0F);
        this.xrPageInfo2.Name = "xrPageInfo2";
        this.xrPageInfo2.Padding = new DevExpress.XtraPrinting.PaddingInfo(2, 2, 0, 0, 96F);
        this.xrPageInfo2.SizeF = new System.Drawing.SizeF(318.1248F, 22.99998F);
        this.xrPageInfo2.StylePriority.UseTextAlignment = false;
        this.xrPageInfo2.TextAlignment = DevExpress.XtraPrinting.TextAlignment.TopRight;
        // 
        // EmployeeClaimreport
        // 
        this.Bands.AddRange(new DevExpress.XtraReports.UI.Band[] {
            this.Detail,
            this.TopMargin,
            this.BottomMargin,
            this.GroupFooter1,
            this.PageFooter});
        this.DataMember = "EmployeeClaim";
        this.DataSource = this.employeeClaimApproved1;
        this.FormattingRuleSheet.AddRange(new DevExpress.XtraReports.UI.FormattingRule[] {
            this.formattingRule1});
        this.Margins = new System.Drawing.Printing.Margins(100, 100, 95, 15);
        this.Version = "10.2";
        ((System.ComponentModel.ISupportInitialize)(this.xrTable2)).EndInit();
        ((System.ComponentModel.ISupportInitialize)(this.xrTable1)).EndInit();
        ((System.ComponentModel.ISupportInitialize)(this.employeeClaimApproved1)).EndInit();
        ((System.ComponentModel.ISupportInitialize)(this)).EndInit();

	}

	#endregion


    public void setSumClaimAmount(double sum)
    {
        xrLabel6.Text = sum.ToString("0.000");
    }


    private void xrLabel6_BeforePrint(object sender, System.Drawing.Printing.PrintEventArgs e)
    {

    }

}
