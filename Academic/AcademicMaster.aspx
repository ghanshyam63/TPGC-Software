<%@ Page Language="C#" MasterPageFile="~/ERPMaster.master" AutoEventWireup="true" CodeFile="AcademicMaster.aspx.cs" Inherits="Academic_AcademicMaster" Title="Academic Master" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
 <br />
        <br />
    <asp:HyperLink ID="Hln1" NavigateUrl="~/Academic/Masters.aspx" runat="server">Masters</asp:HyperLink>
    <br />
        <br />
    <asp:HyperLink ID="Hln2" NavigateUrl="~/Academic/AcademicFormsMasters.aspx" runat="server">Academic Forms Master</asp:HyperLink>
    <br />
        <br />
    <asp:HyperLink ID="Hln13" NavigateUrl="~/Academic/AcademicReportsMaster.aspx" runat="server">Academic Reports Master</asp:HyperLink>

</asp:Content>


