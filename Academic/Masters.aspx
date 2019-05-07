<%@ Page Language="C#" MasterPageFile="~/ERPMaster.master" AutoEventWireup="true" CodeFile="Masters.aspx.cs" Inherits="Academic_Masters" Title="Masters" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
<br />
        <br />
    <asp:HyperLink ID="Hln1" NavigateUrl="~/Academic/CourseMaster.aspx" runat="server">Course Master</asp:HyperLink>
    <br />
        <br />
    <asp:HyperLink ID="Hln2" NavigateUrl="~/Academic/ClassMaster.aspx" runat="server">Class Master</asp:HyperLink>
    <br />
        <br />
    <asp:HyperLink ID="Hln13" NavigateUrl="~/Academic/FeesHeadMaster.aspx" runat="server">Fees Head Master</asp:HyperLink>
    <br />
       <asp:HyperLink ID="HyperLink1" NavigateUrl="~/Academic/StudentMaster.aspx" runat="server">Student Master</asp:HyperLink>
</asp:Content>


