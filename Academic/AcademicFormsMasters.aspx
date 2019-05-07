<%@ Page Language="C#" MasterPageFile="~/ERPMaster.master" AutoEventWireup="true" CodeFile="AcademicFormsMasters.aspx.cs" Inherits="Academic_AcademicFormsMasters" Title="Academic Forms Masters" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
<br />
        <br />
       <asp:HyperLink ID="Hln2" NavigateUrl="~/Academic/StudentEnquiry.aspx" runat="server">Student Enquiry</asp:HyperLink>
   
    <br />
        <br />
      <asp:HyperLink ID="Hln1" NavigateUrl="~/Academic/StudentAdmissionMaster.aspx" runat="server">Admission Master</asp:HyperLink>
    <br />
        <br />
    <%--<asp:HyperLink ID="Hln13" NavigateUrl="~/Academic/AcademicReportsMaster.aspx" runat="server">Academic Reports Master</asp:HyperLink>--%>
</asp:Content>


