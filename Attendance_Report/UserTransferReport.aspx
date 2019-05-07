<%@ Page Language="C#" MasterPageFile="~/ERPMaster.master" AutoEventWireup="true" CodeFile="UserTransferReport.aspx.cs" Inherits="AttendanceReport_UserTransferReport" Title="Untitled Page" %>

<%@ Register Assembly="DevExpress.XtraReports.v10.2.Web, Version=10.2.6.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a"
    Namespace="DevExpress.XtraReports.Web" TagPrefix="dx" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
<link href="../CSS/InvStyle.css" rel="stylesheet" type="text/css" />
  <table width="100%">
                <tr >
                    <td>
                        <table width="100%" cellpadding="0" cellspacing="0" bordercolor="#F0F0F0">
                            <tr bgcolor="#90BDE9">
                                <td align='<%= Common.ChangeTDForDefaultLeft()%>'>
                                    <table>
                                        <tr>
                                            <td>
                                                <img src="../Images/product_icon.png" width="31" height="30" alt="D" />
                                            </td>
                                            <td>
                                                <img src="../Images/seperater.png" width="2" height="43" alt="SS" />
                                            </td>
                                            <td style="padding-left: 5px">
                                                <asp:Label ID="lblHeader" runat="server" Text="<%$ Resources:Attendance,User Transfer Report %>"
                                                    CssClass="LableHeaderTitle"></asp:Label>
                                            </td>
                                        </tr>
                                    </table>
                                </td>
                                
                            </tr>
                            <tr >
                            
                                <td bgcolor="#ccddee" align='<%= Common.ChangeTDForDefaultLeft()%>'>
                                  
                                   <table width="100%">
                                <tr>
                                <td> <table><tr>  <td width="50px" align='<%= Common.ChangeTDForDefaultLeft()%>'>
                                <asp:Label ID="lblFinger" CssClass="labelComman"   runat="server" Text="<%$ Resources:Attendance,Type %>"></asp:Label>
                            </td>
                            <td align='<%= Common.ChangeTDForDefaultLeft()%>' width="1px">
                                :
                            </td>
                            <td width="50px" align='<%= Common.ChangeTDForDefaultLeft()%>'>
                                <asp:DropDownList runat="server" CssClass="dropdownsearch" ID="ddlReport" AutoPostBack="True" OnSelectedIndexChanged="ddlReportTrOrFal_SelectedIndexChanged">
                                    
                                    <asp:ListItem Text="True" Value="True"></asp:ListItem>
                                    <asp:ListItem Text="False" Value="False"></asp:ListItem>
                                   
                                    <asp:ListItem Text="All" Value="All"></asp:ListItem>
                                    
                                </asp:DropDownList>
                            </td>
                            </tr>
                            </table>
                                  
                                  
                                  </td>
                                
                            </tr>
                            <tr>
                                <td bgcolor="#ccddee"  width="100%" height="500px" valign="top">
                             
                             
                                 <table width="100%">
                        <tr>
                            <td>
                                <dx:ReportToolbar ID="rptToolBar" runat="server" ShowDefaultButtons="False" ReportViewer="<%# rptViewer %>"
                                    Width="100%" AccessibilityCompliant="True">
                                    <Items>
                                        <dx:ReportToolbarButton ItemKind="Search" />
                                        <dx:ReportToolbarSeparator />
                                        <dx:ReportToolbarButton ItemKind="PrintReport" />
                                        <dx:ReportToolbarButton ItemKind="PrintPage" />
                                        <dx:ReportToolbarSeparator />
                                        <dx:ReportToolbarButton Enabled="False" ItemKind="FirstPage" />
                                        <dx:ReportToolbarButton Enabled="False" ItemKind="PreviousPage" />
                                        <dx:ReportToolbarLabel ItemKind="PageLabel" />
                                        <dx:ReportToolbarComboBox ItemKind="PageNumber" Width="65px">
                                        </dx:ReportToolbarComboBox>
                                        <dx:ReportToolbarLabel ItemKind="OfLabel" />
                                        <dx:ReportToolbarTextBox IsReadOnly="True" ItemKind="PageCount" />
                                        <dx:ReportToolbarButton ItemKind="NextPage" />
                                        <dx:ReportToolbarButton ItemKind="LastPage" />
                                        <dx:ReportToolbarSeparator />
                                        <dx:ReportToolbarButton ItemKind="SaveToDisk" />
                                        <dx:ReportToolbarButton ItemKind="SaveToWindow" />
                                        <dx:ReportToolbarComboBox ItemKind="SaveFormat" Width="70px">
                                            <Elements>
                                                <dx:ListElement Value="pdf" />
                                                <dx:ListElement Value="xls" />
                                                <dx:ListElement Value="xlsx" />
                                                <dx:ListElement Value="rtf" />
                                                <dx:ListElement Value="mht" />
                                                <dx:ListElement Value="html" />
                                                <dx:ListElement Value="txt" />
                                                <dx:ListElement Value="csv" />
                                                <dx:ListElement Value="png" />
                                            </Elements>
                                        </dx:ReportToolbarComboBox>
                                    </Items>
                                    <Styles>
                                        <LabelStyle>
                                            <Margins MarginLeft="3px" MarginRight="3px" />
                                        </LabelStyle>
                                    </Styles>
                                </dx:ReportToolbar>
                            </td>
                        </tr>
                        <tr>
                            <td>
                                <asp:Panel ID="pnlrptviewer" runat="server" Width="100%" Height="100%">
                                    <dx:ReportViewer ID="rptViewer" runat="server" AutoSize="False" Width="98%" Height="500px">
                                    </dx:ReportViewer>
                                </asp:Panel>
                            </td>
                        </tr>
                    </table>
                                
</td>
</tr>

</table>
</td>
</tr>
</table>
</asp:Content>

