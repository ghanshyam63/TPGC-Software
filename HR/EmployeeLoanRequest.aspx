<%@ Page Language="C#" MasterPageFile="~/ERPMaster.master" AutoEventWireup="true" CodeFile="EmployeeLoanRequest.aspx.cs" Inherits="HR_EmployeeLoanRequest" Title="Untitled Page" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">

<link href="../CSS/InvStyle.css" rel="stylesheet" type="text/css" />
    <asp:UpdatePanel ID="UpdatePanel2" runat="server" UpdateMode="Conditional">
        <Triggers>
            <asp:PostBackTrigger ControlID="BtnSave" />
            <asp:PostBackTrigger ControlID="BtnReset" />
            <asp:PostBackTrigger ControlID="BtnCancel" />
            </Triggers>
        <ContentTemplate>
            <table width="100%">
                <tr align='<%= Common.ChangeTDForDefaultLeft()%>'>
                    <td>
                        <table width="100%" cellpadding="0" cellspacing="0" bordercolor="#F0F0F0">
                            <tr bgcolor="#90BDE9">
                                <td style="width: 30%;">
                                    <table>
                                        <tr>
                                            <td>
                                                <img src="../Images/product_icon.png" width="31" height="30" alt="D" />
                                            </td>
                                            <td>
                                                <img src="../Images/seperater.png" width="2" height="43" alt="SS" />
                                            </td>
                                            <td width="150px" style="padding-left: 5px">
                                                <asp:Label ID="lblHeader" runat="server" Text="<%$ Resources:Attendance,Loan %>"
                                                    CssClass="LableHeaderTitle"></asp:Label>
                                            </td>
                                        </tr>
                                    </table>
                                </td>
                                <td align="right" style="width: 70%;">
                                    <table cellpadding="0" cellspacing="0">
                                        <tr>
                                            <td>
                                                <%--<asp:Panel ID="pnlLeave" runat="server" CssClass="a" Width="100%">
                                                    <asp:Button ID="btnLoan" runat="server" Text="<%$ Resources:Attendance,Loan %>" Width="100px"
                                                        BorderStyle="none" BackColor="Transparent" Style="padding-top: 3px; padding-left: 15px;
                                                        background-image: url('../Images/Bin.png' ); background-repeat: no-repeat; height: 49px;
                                                        background-position: 5px 15px; font: bold 14px Trebuchet MS; color: #000000;"
                                                         />
                                                </asp:Panel>
                                           --%> </td>
                                            <td>
                                               <%-- <asp:Panel ID="PanelList" runat="server" CssClass="a" Width="100%">
                                                    <asp:Button ID="btnList" runat="server" Text="<%$ Resources:Attendance,List %>" Width="80px"
                                                        BorderStyle="none" BackColor="Transparent" Style="padding-top: 3px; padding-left: 10px;
                                                        background-image: url('../Images/List.png' ); background-repeat: no-repeat; height: 49px;
                                                        background-position: 5px 15px; font: bold 14px Trebuchet MS; color: #000000;"
                                                        OnClick="btList_Click" />
                                                </asp:Panel>
                                           --%> </td>
                                        </tr>
                                    </table>
                                </td>
                            </tr>
                            <tr>
                                <td bgcolor="#ccddee" colspan="4" width="100%" height="500px" valign="top">
                                    <asp:Panel ID="PanelSaveLoan" runat="server" Width="100%">
                                        <table width="100%">
                                            <tr>
                                                <td colspan="3" style="padding-top: 5px;">
                                                </td>
                                            </tr>
                                            <tr>
                                                <td align="<%= Common.ChangeTDForDefaultLeft()%>" width="104">
                                                    <asp:Label ID="LblEmpName" runat="server" CssClass="labelComman" Text="<%$ Resources:Attendance,Employee Name %>"></asp:Label>
                                                </td>
                                                <td align="<%= Common.ChangeTDForDefaultLeft()%>">
                                                    :
                                                </td>
                                                <td align="<%= Common.ChangeTDForDefaultLeft()%>">
                                                    <asp:TextBox ID="txtEmpName" runat="server" Width="200px" TabIndex="1" CssClass="textComman"
                                                        BackColor="#e3e3e3" OnTextChanged="TxtEmpName_TextChanged"></asp:TextBox>
                                                    <cc1:AutoCompleteExtender ID="AutoCompleteExtender5" runat="server" DelimiterCharacters=""
                                                        Enabled="True" ServiceMethod="GetCompletionListEmployeeName" ServicePath="" CompletionInterval="100"
                                                        MinimumPrefixLength="1" CompletionSetCount="1" TargetControlID="txtEmpName" UseContextKey="True"
                                                        CompletionListCssClass="completionList" CompletionListItemCssClass="listItem"
                                                        CompletionListHighlightedItemCssClass="itemHighlighted">
                                                    </cc1:AutoCompleteExtender>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td align="<%= Common.ChangeTDForDefaultLeft()%>">
                                                    <asp:Label ID="Lbllist" runat="server" CssClass="labelComman" Text="<%$ Resources:Attendance,Loan Name %>"></asp:Label>
                                                </td>
                                                <td align="<%= Common.ChangeTDForDefaultLeft()%>">
                                                    :
                                                </td>
                                                <td align='<%= Common.ChangeTDForDefaultLeft()%>'>
                                                    <asp:TextBox ID="txtLoanName" runat="server" Width="200px" BackColor="#e3e3e3" TabIndex="2"
                                                        CssClass="textComman"></asp:TextBox>
                                                    <cc1:AutoCompleteExtender ID="AutoCompleteExtender1" runat="server" DelimiterCharacters=""
                                                        Enabled="True" ServiceMethod="GetLoanName" ServicePath="" CompletionInterval="100"
                                                        MinimumPrefixLength="1" CompletionSetCount="1" TargetControlID="TxtLoanName"
                                                        UseContextKey="True" CompletionListCssClass="completionList" CompletionListItemCssClass="listItem"
                                                        CompletionListHighlightedItemCssClass="itemHighlighted">
                                                    </cc1:AutoCompleteExtender>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td align="<%= Common.ChangeTDForDefaultLeft()%>">
                                                    <asp:Label ID="LblLoanAmount" runat="server" CssClass="labelComman" Text="<%$ Resources:Attendance,Loan Amount %>"></asp:Label>
                                                </td>
                                                <td>
                                                    :
                                                </td>
                                                <td align="<%= Common.ChangeTDForDefaultLeft()%>">
                                                    <asp:TextBox ID="txtLoanAmount" runat="server" Width="200px" TabIndex="3" CssClass="textComman"></asp:TextBox>
                                                    <cc1:FilteredTextBoxExtender ID="FilteredTextBoxExtender1" runat="server" Enabled="True"
                                                        TargetControlID="TxtLoanAmount" ValidChars="1,2,3,4,5,6,7,8,9,0,.">
                                                    </cc1:FilteredTextBoxExtender>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td>
                                                    &nbsp;
                                                </td>
                                                <td>
                                                    &nbsp;
                                                </td>
                                                <td align="<%= Common.ChangeTDForDefaultLeft()%>">
                                                    &nbsp; &nbsp;<asp:Button ID="BtnSave" runat="server" CssClass="buttonCommman" TabIndex="5"
                                                        Text="<%$ Resources:Attendance,Save %>" ValidationGroup="leavesave" Width="65px"
                                                        OnClick="BtnSave_Click" />
                                                    &nbsp;&nbsp;
                                                    <asp:Button ID="BtnReset" runat="server" CausesValidation="False" CssClass="buttonCommman"
                                                        TabIndex="5" Text="<%$ Resources:Attendance,Reset %>" Width="65px" 
                                                        OnClick="BtnReset_Click" />
                                                    &nbsp;&nbsp;
                                                    <asp:Button ID="BtnCancel" runat="server" CausesValidation="False" CssClass="buttonCommman"
                                                        TabIndex="6" Text="<%$ Resources:Attendance,Cancel %>" Width="65px" 
                                                        OnClick="BtnCancel_Click" />
                                                    &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                                                    <asp:HiddenField ID="HiddeniD" runat="server" />
                                                    <asp:HiddenField ID="HidEmpId" runat="server" />
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
    <asp:UpdateProgress runat="server" AssociatedUpdatePanelID="UpdatePanel2" ID="updategvprogress">
        <ProgressTemplate>
            <div id="progressBackgroundFilter">
            </div>
            <div id="processMessainfoge" class="progressBackgroundFilter">
                Loading…<br />
                <br />
                <img alt="Loading" src="../Images/ajax-loader2.gif" class="processMessage" />
            </div>
        </ProgressTemplate>
    </asp:UpdateProgress>

    

</asp:Content>


