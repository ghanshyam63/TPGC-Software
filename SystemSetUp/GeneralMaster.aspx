<%@ Page Language="C#" MasterPageFile="~/ERPMaster.master" AutoEventWireup="true" CodeFile="GeneralMaster.aspx.cs" Inherits="MasterSetUp_GeneralMaster" Title="General Master" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
 
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
    <link href="../CSS/InvStyle.css" rel="stylesheet" type="text/css" />
<div style="background-color: #33CCFF; ">
    <asp:Button ID="btnCertificate" runat="server" Text="Certificate" 
         Height="25px" 
        Width="131px" CssClass="buttonclass" 
        PostBackUrl="~/SystemSetUp/CertificateMaster.aspx" />
    <asp:Button ID="btnUnivercity" runat="server" Text="Univercity Name" Height="25px" 
        Width="141px" CssClass="buttonclass" 
        PostBackUrl="~/SystemSetUp/UnivercityMaster.aspx" />
    <asp:Button ID="btnExamination" runat="server" Text="Examination" Height="25px" 
        Width="136px" CssClass="buttonclass" 
        PostBackUrl="~/SystemSetUp/ExaminationMaster.aspx" />
    <asp:Button ID="btnStudentType" runat="server" Text="StudentType" Height="25px" 
        Width="135px" CssClass="buttonclass" 
        PostBackUrl="~/SystemSetUp/StudentTypeMaster.aspx" />
    <asp:Button ID="btnConcessionName" runat="server" Text="Concession Name"  Height="25px" 
        Width="142px" CssClass="buttonclass" 
        PostBackUrl="~/SystemSetUp/ConcessionMaster.aspx" />
    <asp:Button ID="btnState" runat="server" Text="State" Height="25px" 
        Width="131px" CssClass="buttonclass" 
        PostBackUrl="~/SystemSetUp/StateMaster.aspx" />
    <asp:Button ID="btnCity" runat="server" Text="City" Height="25px" 
        Width="135px" CssClass="buttonclass" 
        PostBackUrl="~/SystemSetUp/CityMaster.aspx" />
    <asp:Button ID="btnDistrict" runat="server" Text="District" Height="25px" 
        Width="135px" CssClass="buttonclass" 
        PostBackUrl="~/SystemSetUp/DistrictMaster.aspx" />
    <asp:Button ID="btnGrampanchayat" runat="server" Text="Grampanchayat" Height="25px" 
        Width="141px" CssClass="buttonclass" 
        PostBackUrl="~/SystemSetUp/GrampacyatMaster.aspx"/>
    <asp:Button ID="btnPost" runat="server" Text="Post" Height="25px" 
        Width="121px" CssClass="buttonclass" 
        PostBackUrl="~/SystemSetUp/PostMaster.aspx"/>
    <asp:Button ID="btnVillage" runat="server" Text="Village" Height="25px" 
        Width="135px" CssClass="buttonclass" 
        PostBackUrl="~/SystemSetUp/VillageMaster.aspx"/>
 </div>
</asp:Content>


