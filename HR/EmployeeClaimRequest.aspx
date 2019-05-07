<%@ Page Language="C#" MasterPageFile="~/ERPMaster.master" AutoEventWireup="true" CodeFile="EmployeeClaimRequest.aspx.cs" Inherits="HR_EmployeeClaimRequest" Title="Untitled Page" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
<link href="../CSS/InvStyle.css" rel="stylesheet" type="text/css" />
<asp:UpdatePanel ID="Upd" runat="server">
<Triggers>
<asp:PostBackTrigger ControlID="btnSave" />
<asp:PostBackTrigger ControlID="btnReset" />
<asp:PostBackTrigger ControlID="btnCancel" />

</Triggers>
<ContentTemplate>
<table width="100%">
                <tr align='<%= Common.ChangeTDForDefaultLeft()%>'>
                    <td>
                        <table width="100%" cellpadding="0" cellspacing="0" bordercolor="#F0F0F0">
                            <tr bgcolor="#90BDE9">
                                <td>
                                    <table>
                                        <tr>
                                            <td>
                                                <img src="../Images/product_icon.png" width="31" height="30" alt="D" />
                                            </td>
                                            <td>
                                                <img src="../Images/seperater.png" width="2" height="43" alt="SS" />
                                            </td>
                                            <td width="150px" style="padding-left: 5px">
                                                <asp:Label ID="lblHeader" runat="server" Text="<%$ Resources:Attendance,Claim %>"
                                                    CssClass="LableHeaderTitle"></asp:Label>
                                            </td>
                                        </tr>
                                    </table>
                                </td>
                                <td align="right">
                                    <table cellpadding="0" cellspacing="0">
                                        <tr>
                                            <td>
                                               <%-- <asp:Panel ID="pnlLeave" runat="server" CssClass="a" Width="100%">
                                                    <asp:Button ID="btnLeave" runat="server" Text="<%$ Resources:Attendance,Claim %>"
                                                        Width="90px" BorderStyle="none" BackColor="Transparent" 
                                                        Style="padding-left: 10px; padding-top: 3px; background-image: url('../Images/Bin.png' );
                                                        background-repeat: no-repeat; height: 49px; background-position: 5px 15px; font: bold 14px Trebuchet MS;
                                                        color: #000000;" onclick="btnLeave_Click" />
                                                </asp:Panel>
                                                <td>
                                                <asp:Panel ID="PanelList" runat="server" CssClass="a" Width="100%">
                                                    <asp:Button ID="btnLeast" runat="server" Text="<%$ Resources:Attendance,List %>"
                                                        Width="80px" BorderStyle="none" BackColor="Transparent" Style="padding-top: 3px;
                                                        padding-left: 10px; background-image: url('../Images/List.png' ); background-repeat: no-repeat;
                                                        height: 49px; background-position: 5px 15px; font: bold 14px Trebuchet MS; color: #000000;"
                                                        OnClick="btnLeast_Click" />
                                                </asp:Panel>--%>
                                            </td>
                                            </td>
                                        </tr>
                                    </table>
                                </td>
                            </tr>
                            <tr>
                                <td bgcolor="#ccddee" colspan="4" width="100%" height="500px" valign="top">
                                
                                 <asp:Panel ID="Panel3" runat="server" Width="100%">
                                                        <table width="100%">
                                                            <tr>
                                                                <td style="padding-top: 5px;">
                                                                
                                                                </td>
                                                            </tr>
                                                            <tr>
                                                            <td align="<%= Common.ChangeTDForDefaultLeft()%>">
                                                            <asp:Label ID="Label1" runat="server" CssClass="labelComman" Text="<%$ Resources:Attendance,Employee Name %>"></asp:Label>
                                                                
                                                            </td>
                                                            <td>
                                                            :
                                                            </td>
                                                            <td colspan="4">
                                                            <asp:TextBox ID="TxtEmployeeId" runat="server" Width="440px" TabIndex="1" 
                                                                    CssClass="textComman" BackColor="#e3e3e3" 
                                                                    ontextchanged="TxtEmployeeId_TextChanged"></asp:TextBox>
                                                             <cc1:AutoCompleteExtender ID="AutoCompleteExtender1" runat="server" DelimiterCharacters=""
                                                        Enabled="True" ServiceMethod="GetCompletionListEmployeeName" ServicePath="" CompletionInterval="100"
                                                        MinimumPrefixLength="1" CompletionSetCount="1" TargetControlID="TxtEmployeeId" UseContextKey="True"
                                                        CompletionListCssClass="completionList" CompletionListItemCssClass="listItem"
                                                        CompletionListHighlightedItemCssClass="itemHighlighted">
                                                    </cc1:AutoCompleteExtender>
                                                    <asp:HiddenField ID="HidEmpId" runat="server" />
                                                  
                                                            </td>
                                                            </tr>
                                                            
                                                            <tr>
                                                                <td align="<%= Common.ChangeTDForDefaultLeft()%>" width="104">
                                                                    <asp:Label ID="LblClaimName" runat="server" CssClass="labelComman" Text="<%$ Resources:Attendance,Claim Name %>"></asp:Label>
                                                                </td>
                                                                <td align="<%= Common.ChangeTDForDefaultLeft()%>">
                                                                    :
                                                                </td>
                                                                <td colspan="4" align="<%= Common.ChangeTDForDefaultLeft()%>">
                                                                    <asp:TextBox ID="TxtClaimName" runat="server" BackColor="#e3e3e3" Width="440px" TabIndex="1" CssClass="textComman"></asp:TextBox>
                                                                <cc1:AutoCompleteExtender ID="AutoCompleteExtender5" runat="server" DelimiterCharacters=""
                                                        Enabled="True" ServiceMethod="GetClaimName" ServicePath="" CompletionInterval="100"
                                                        MinimumPrefixLength="1" CompletionSetCount="1" TargetControlID="TxtClaimName" UseContextKey="True"
                                                        CompletionListCssClass="completionList" CompletionListItemCssClass="listItem"
                                                        CompletionListHighlightedItemCssClass="itemHighlighted">
                                                    </cc1:AutoCompleteExtender>
                                                                </td>
                                                            </tr>
                                                            <tr>
                                                                <td valign="top" align="<%= Common.ChangeTDForDefaultLeft()%>">
                                                                    <asp:Label ID="lblClaimDiscription" runat="server" CssClass="labelComman" Text="<%$ Resources:Attendance,Claim Description %>"></asp:Label>
                                                                </td>
                                                                <td  valign="top" align="<%= Common.ChangeTDForDefaultLeft()%>">
                                                                    :
                                                                </td>
                                                                <td colspan="4" align='<%= Common.ChangeTDForDefaultLeft()%>'>
                                                                    <asp:TextBox ID="TxtClaimDiscription" runat="server" Width="440px" Height="100px"
                                                                        TextMode="MultiLine" TabIndex="2" CssClass="textComman"></asp:TextBox>
                                                                </td>
                                                            </tr>
                                                            <tr>
                                                                <td align="<%= Common.ChangeTDForDefaultLeft()%>">
                                                                    <asp:Label ID="LabelValueType" runat="server" CssClass="labelComman" Text="<%$ Resources:Attendance,Value Type %>"></asp:Label>
                                                                </td>
                                                                <td>
                                                                    :
                                                                </td>
                                                                <td>
                                                                    <asp:DropDownList ID="DdlValueType" runat="server" CssClass="textComman" TabIndex="3"
                                                                        Width="158px" >
                                                                        <asp:ListItem Text="--Select--" Value="0"></asp:ListItem>
                                                                        <asp:ListItem Text="Fixed" Value="1" />
                                                                        <asp:ListItem Text="Percentage" Value="2" />
                                                                    </asp:DropDownList>
                                                                </td>
                                                                <td align="<%= Common.ChangeTDForDefaultLeft()%>">
                                                                    <asp:Label ID="LblValue" runat="server" CssClass="labelComman" Text="<%$ Resources:Attendance,Value%>"></asp:Label>
                                                                </td>
                                                                <td>
                                                                    :
                                                                </td>
                                                                <td align="<%= Common.ChangeTDForDefaultLeft()%>">
                                                                    <asp:TextBox ID="txtCalValue" runat="server" Width="150px" TabIndex="4" CssClass="textComman"></asp:TextBox>
                                                                    <cc1:FilteredTextBoxExtender ID="txtMobile_FilteredTextBoxExtender" runat="server"
                                                                        Enabled="True" TargetControlID="txtCalValue" ValidChars="1,2,3,4,5,6,7,8,9,0,.">
                                                                    </cc1:FilteredTextBoxExtender>
                                                                </td>
                                                            </tr>
                                                            <tr>
                                                                <td align="<%= Common.ChangeTDForDefaultLeft()%>">
                                                                    <asp:Label ID="lblMonth" runat="server" CssClass="labelComman" Text="<%$ Resources:Attendance,Month %>"></asp:Label>
                                                                </td>
                                                                <td>
                                                                    :
                                                                </td>
                                                                <td align="<%= Common.ChangeTDForDefaultLeft()%>">
                                                                    <asp:DropDownList ID="ddlMonth" runat="server" CssClass="textComman" TabIndex="5"
                                                                        Width="158px" >
                                                                        <asp:ListItem Text="--Select--" Value="0" />
                                                                        <asp:ListItem Text="January" Value="1" />
                                                                        <asp:ListItem Text="February" Value="2" />
                                                                        <asp:ListItem Text="March" Value="3" />
                                                                        <asp:ListItem Text="April" Value="4" />
                                                                        <asp:ListItem Text="May" Value="5" />
                                                                        <asp:ListItem Text="June" Value="6" />
                                                                        <asp:ListItem Text="July" Value="7" />
                                                                        <asp:ListItem Text="August" Value="8" />
                                                                        <asp:ListItem Text="September" Value="9" />
                                                                        <asp:ListItem Text="October" Value="10" />
                                                                        <asp:ListItem Text="November" Value="11" />
                                                                        <asp:ListItem Text="December" Value="12" />
                                                                    </asp:DropDownList>
                                                                </td>
                                                                <td align='<%= Common.ChangeTDForDefaultLeft()%>'>
                                                                    <asp:Label ID="LblYear" runat="server" CssClass="labelComman" Text="<%$ Resources:Attendance,Year %>"></asp:Label>
                                                                </td>
                                                                <td>
                                                                    :
                                                                </td>
                                                                <td align='<%= Common.ChangeTDForDefaultLeft()%>'>
                                                                    <asp:TextBox ID="TxtYear" runat="server" Width="150px" TabIndex="6" CssClass="textComman"></asp:TextBox>
                                                                </td>
                                                            </tr>
                                                            <tr>
                                                                <td colspan="6" align="center" style="padding-left: 12px;">
                                                                    <asp:Button ID="btnSave" runat="server" CssClass="buttonCommman" TabIndex="7" Text="<%$ Resources:Attendance,Save %>"
                                                                        OnClick="btnSaveLeave_Click" ValidationGroup="leavesave" Width="65px" />
                                                                    &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                                                                    <asp:Button ID="btnReset" runat="server" CausesValidation="False" CssClass="buttonCommman"
                                                                        TabIndex="8" Text="<%$ Resources:Attendance,Reset %>" Width="65px" 
                                                                        onclick="btnReset_Click" />
                                                                    &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                                                                    <asp:Button ID="btnCancel" runat="server" CausesValidation="False" CssClass="buttonCommman"
                                                                        TabIndex="9" Text="<%$ Resources:Attendance,Cancel %>" Width="65px" 
                                                                        onclick="btnCancel_Click" />
                                                                </td>
                                                            </tr>
                                                            <tr>
                                                                <td colspan="6">
                                                                    &nbsp;
                                                                </td>
                                                            </tr>
                                                        </table>
                                                    </asp:Panel>
                                </td>
                                </tr>
                                </table>
                                </td>
                                </tr>
                                </table>

</ContentTemplate>

</asp:UpdatePanel>
</asp:Content>

