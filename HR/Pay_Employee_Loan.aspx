<%@ Page Language="C#" MasterPageFile="~/ERPMaster.master" AutoEventWireup="true"
    CodeFile="Pay_Employee_Loan.aspx.cs" Inherits="Arca_Wing_Pay_Employee_Loan" Title="Untitled Page" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <link href="../CSS/InvStyle.css" rel="stylesheet" type="text/css" />
    <asp:UpdatePanel ID="UpdatePanel2" runat="server" UpdateMode="Conditional">
        <Triggers>
            <asp:PostBackTrigger ControlID="BtnSave" />
            <asp:PostBackTrigger ControlID="BtnReset" />
            <asp:PostBackTrigger ControlID="BtnCancel" />
            <asp:PostBackTrigger ControlID="btnLoan" />
            <asp:PostBackTrigger ControlID="btnList" />
            <asp:PostBackTrigger ControlID="btn_PopUp_Save" />
            <asp:PostBackTrigger ControlID="btn_PopUp_Reset" />
            <asp:PostBackTrigger ControlID="RbtnApproved" />
            <asp:PostBackTrigger ControlID="RbtnCancelled" />
            <asp:PostBackTrigger ControlID="GridViewLoan" />
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
                                                <asp:Panel ID="pnlLeave" runat="server" CssClass="a" Width="100%">
                                                    <asp:Button ID="btnLoan" runat="server" Text="<%$ Resources:Attendance,Loan %>" Width="100px"
                                                        BorderStyle="none" BackColor="Transparent" Style="padding-top: 3px; padding-left: 15px;
                                                        background-image: url('../Images/Bin.png' ); background-repeat: no-repeat; height: 49px;
                                                        background-position: 5px 15px; font: bold 14px Trebuchet MS; color: #000000;"
                                                        OnClick="btnLoan_Click" />
                                                </asp:Panel>
                                            </td>
                                            <td>
                                                <asp:Panel ID="PanelList" runat="server" CssClass="a" Width="100%">
                                                    <asp:Button ID="btnList" runat="server" Text="<%$ Resources:Attendance,List %>" Width="80px"
                                                        BorderStyle="none" BackColor="Transparent" Style="padding-top: 3px; padding-left: 10px;
                                                        background-image: url('../Images/List.png' ); background-repeat: no-repeat; height: 49px;
                                                        background-position: 5px 15px; font: bold 14px Trebuchet MS; color: #000000;"
                                                        OnClick="btList_Click" />
                                                </asp:Panel>
                                            </td>
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
                                    <asp:Panel ID="PanelUpdateLoan" runat="server" Width="100%">
                                        <asp:GridView ID="GridViewLoan" runat="server" AllowPaging="True" 
                                            AllowSorting="true" AutoGenerateColumns="False"
                                            CssClass="grid" Width="100%" 
                                            PageSize="<%# SystemParameter.GetPageSize() %>" 
                                            onpageindexchanging="GridViewLoan_PageIndexChanging" 
                                            onsorting="GridViewLoan_Sorting">
                                            <Columns>
                                                <asp:TemplateField HeaderText="<%$ Resources:Attendance,Edit %>">
                                                    <ItemTemplate>
                                                        <asp:ImageButton ID="btnEdit" runat="server" CommandArgument='<%# Eval("Loan_Id") %>'
                                                            ImageUrl="~/Images/edit.png" CausesValidation="False" OnCommand="btnEdit_command"
                                                            ToolTip="<%$ Resources:Attendance,Edit %>" Width="16px" /></ItemTemplate>
                                                    <ItemStyle HorizontalAlign="Center" />
                                                    <ItemStyle CssClass="grid" />
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="<%$ Resources:Attendance,Delete %>">
                                                    <ItemTemplate>
                                                        <asp:ImageButton ID="IbtnDelete" runat="server" CausesValidation="False" CommandArgument='<%# Eval("Loan_Id") %>'
                                                            ImageUrl="~/Images/Erase.png" OnCommand="IbtnDelete_command" ToolTip="<%$ Resources:Attendance,Delete %>" />
                                                    </ItemTemplate>
                                                    <ItemStyle HorizontalAlign="Center" />
                                                    <ItemStyle CssClass="grid" />
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="<%$ Resources:Attendance,Loan Id %>" SortExpression="Loan_Id">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblEmpIdList" runat="server" Text='<%# Eval("Loan_Id") %>'></asp:Label>
                                                    </ItemTemplate>
                                                    <ItemStyle CssClass="grid" />
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="<%$ Resources:Attendance,Employee Name %>" SortExpression="Emp_Name">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblEmpnameList" runat="server" Text='<%# Eval("Emp_Name") %>'></asp:Label>
                                                    </ItemTemplate>
                                                    <ItemStyle CssClass="grid" />
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="<%$ Resources:Attendance,Loan Name %>" SortExpression="Loan_Name">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblPenaltynameList" runat="server" Text='<%# Eval("Loan_Name") %>'></asp:Label>
                                                    </ItemTemplate>
                                                    <ItemStyle CssClass="grid" />
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="<%$ Resources:Attendance,Loan Amount %>" SortExpression="Loan_Amount">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblvaluetype" runat="server" Text='<%# Eval("Loan_Amount") %>'></asp:Label>
                                                    </ItemTemplate>
                                                    <ItemStyle CssClass="grid" />
                                                </asp:TemplateField>
                                            </Columns>
                                            <AlternatingRowStyle CssClass="InvgridAltRow" />
                                            <HeaderStyle CssClass="Invgridheader" BorderStyle="Solid" BorderColor="#6E6E6E" BorderWidth="1px" />
                                            <PagerStyle CssClass="Invgridheader" />
                                            <RowStyle CssClass="Invgridrow" HorizontalAlign="Center" />
                                        </asp:GridView>
                                          <asp:HiddenField ID="HDFSort" runat="server" />
                                    </asp:Panel>
                                    <asp:Panel ID="pnlBackGroud" runat="server" class="MsgOverlayAddress" Visible="False">
                                        <asp:Panel ID="pnlLoanDetail" runat="server" class="MsgPopUpPanelAddress" Visible="False">
                                            <asp:Panel ID="pnlProduct3" runat="server" Style="width: 100%; height: 100%; text-align: center;">
                                                <table width="100%" style="background-image: url(../Images/bg_repeat.jpg); background-repeat: repeat;">
                                                    <tr>
                                                        <td align="<%= Common.ChangeTDForDefaultLeft()%>">
                                                            <asp:Label ID="lblProductHeader" runat="server" Font-Size="14px" Font-Bold="true"
                                                                CssClass="labelComman" Text="<%$ Resources:Attendance, Loan Setup %>"></asp:Label>
                                                        </td>
                                                        <td align="right">
                                                            <asp:ImageButton ID="btnClosePanel" runat="server" ImageUrl="~/Images/close.png"
                                                                CausesValidation="False" OnClick="btnClosePanel_Click" Height="20px" Width="20px" />
                                                        </td>
                                                    </tr>
                                                </table>
                                                <asp:Panel ID="pnlEditLoan" runat="server" Width="100%" DefaultButton="btn_PopUp_Save">
                                                    <table width="100%">
                                                        <tr>
                                                            <td align="<%= Common.ChangeTDForDefaultLeft()%>" width="104">
                                                                <asp:Label ID="Label1" runat="server" CssClass="labelComman" Text="<%$ Resources:Attendance,Employee Name %>"></asp:Label>
                                                            </td>
                                                            <td align="<%= Common.ChangeTDForDefaultLeft()%>">
                                                                :
                                                            </td>
                                                            <td align="<%= Common.ChangeTDForDefaultLeft()%>">
                                                                <asp:TextBox ID="txtPopUpEmployee" runat="server" Width="200px" TabIndex="1" CssClass="textComman"
                                                                    ReadOnly="true"></asp:TextBox>
                                                            </td>
                                                        </tr>
                                                        <tr>
                                                            <td align="<%= Common.ChangeTDForDefaultLeft()%>">
                                                                <asp:Label ID="Label3" runat="server" CssClass="labelComman" Text="<%$ Resources:Attendance,Loan Name %>"></asp:Label>
                                                            </td>
                                                            <td align="<%= Common.ChangeTDForDefaultLeft()%>">
                                                                :
                                                            </td>
                                                            <td align='<%= Common.ChangeTDForDefaultLeft()%>'>
                                                                <asp:TextBox ID="txtPopUpLoanName" runat="server" Width="200px" ReadOnly="true" TabIndex="2"
                                                                    CssClass="textComman"></asp:TextBox>
                                                            </td>
                                                        </tr>
                                                        <tr>
                                                            <td align="<%= Common.ChangeTDForDefaultLeft()%>">
                                                                <asp:Label ID="Label2" runat="server" CssClass="labelComman" Text="<%$ Resources:Attendance,Loan Amount %>"></asp:Label>
                                                            </td>
                                                            <td>
                                                                :
                                                            </td>
                                                            <td align="<%= Common.ChangeTDForDefaultLeft()%>">
                                                                <asp:TextBox ID="txtPopupLoanAmount" runat="server" Width="200px" TabIndex="3" CssClass="textComman"
                                                                    ReadOnly="true"></asp:TextBox>
                                                            </td>
                                                        </tr>
                                                        <tr>
                                                            <td colspan="3">
                                                                &nbsp;
                                                            </td>
                                                        </tr>
                                                        <tr>
                                                            <td align="<%= Common.ChangeTDForDefaultLeft()%>">
                                                                <asp:RadioButton ID="RbtnApproved" runat="server" CssClass="labelComman" Text="<%$ Resources:Attendance,Approved %>"
                                                                    TabIndex="4" GroupName="Loan" AutoPostBack="true" OnCheckedChanged="RbtnApproved_CheckedChanged" />
                                                            </td>
                                                            <td>
                                                                &nbsp;
                                                            </td>
                                                            <td align="<%= Common.ChangeTDForDefaultLeft()%>">
                                                                <asp:RadioButton ID="RbtnCancelled" runat="server" TabIndex="5" CssClass="labelComman"
                                                                    Text="<%$ Resources:Attendance,Cancelled %>" GroupName="Loan" AutoPostBack="true"
                                                                    OnCheckedChanged="RbtnCancelled_CheckedChanged" />
                                                            </td>
                                                        </tr>
                                                        <tr>
                                                            <td colspan="3">
                                                                &nbsp;
                                                            </td>
                                                            <tr>
                                                                <td align="<%= Common.ChangeTDForDefaultLeft()%>">
                                                                    <asp:Label ID="Label4" runat="server" CssClass="labelComman" Text="<%$ Resources:Attendance,Interest %>"></asp:Label>
                                                                </td>
                                                                <td>
                                                                    :
                                                                </td>
                                                                <td align="<%= Common.ChangeTDForDefaultLeft()%>">
                                                                    <asp:TextBox ID="txtInterest" runat="server" CssClass="textComman" TabIndex="6" Width="200px"></asp:TextBox>
                                                                    <cc1:FilteredTextBoxExtender ID="FilteredTextBoxExtender3" runat="server" Enabled="True"
                                                                        TargetControlID="txtInterest" ValidChars="1,2,3,4,5,6,7,8,9,0,.">
                                                                    </cc1:FilteredTextBoxExtender>
                                                                </td>
                                                            </tr>
                                                            <tr>
                                                                <td align="<%= Common.ChangeTDForDefaultLeft()%>">
                                                                    <asp:Label ID="Label5" runat="server" CssClass="labelComman" Text="<%$ Resources:Attendance,Duration %>"></asp:Label>
                                                                </td>
                                                                <td>
                                                                    :
                                                                </td>
                                                                <td align="<%= Common.ChangeTDForDefaultLeft()%>">
                                                                    <asp:TextBox ID="txtDuration" runat="server" AutoPostBack="true" CssClass="textComman"
                                                                        OnTextChanged="TxtDuration_TextChanged" TabIndex="7" Width="200px"></asp:TextBox>
                                                                    <cc1:FilteredTextBoxExtender ID="FilteredTextBoxExtender2" runat="server" Enabled="True"
                                                                        TargetControlID="txtDuration" ValidChars="1,2,3,4,5,6,7,8,9,0,.">
                                                                    </cc1:FilteredTextBoxExtender>
                                                                </td>
                                                            </tr>
                                                            <tr>
                                                                <td align="<%= Common.ChangeTDForDefaultLeft()%>">
                                                                    <asp:Label ID="Label6" runat="server" CssClass="labelComman" Text="<%$ Resources:Attendance,Gross Amount %>"></asp:Label>
                                                                </td>
                                                                <td>
                                                                    :
                                                                </td>
                                                                <td align="<%= Common.ChangeTDForDefaultLeft()%>">
                                                                    <asp:TextBox ID="txtGrossAmount" runat="server" CssClass="textComman" ReadOnly="true"
                                                                        TabIndex="8" Width="200px"></asp:TextBox>
                                                                </td>
                                                            </tr>
                                                            <tr>
                                                                <td align="<%= Common.ChangeTDForDefaultLeft()%>">
                                                                    <asp:Label ID="Label7" runat="server" CssClass="labelComman" 
                                                                        Text="<%$ Resources:Attendance,Monthly Installment %>"></asp:Label>
                                                                </td>
                                                                <td>
                                                                    :</td>
                                                                <td align="<%= Common.ChangeTDForDefaultLeft()%>">
                                                                    <asp:TextBox ID="txtMonthlyInstallment" runat="server" CssClass="textComman" 
                                                                        ReadOnly="true" TabIndex="9" Width="200px"></asp:TextBox>
                                                                </td>
                                                            </tr>
                                                            <tr>
                                                                <td colspan="3">
                                                                    &nbsp;
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
                                                                    &nbsp; &nbsp;<asp:Button ID="btn_PopUp_Save" runat="server" CssClass="buttonCommman" 
                                                                        OnClick="btn_PopUp_Save_Click" TabIndex="10" 
                                                                        Text="<%$ Resources:Attendance,Save %>" ValidationGroup="leavesave" 
                                                                        Width="65px" />
                                                                    &nbsp;&nbsp; &nbsp;&nbsp;
                                                                    <asp:Button ID="btn_PopUp_Reset" runat="server" CausesValidation="False" 
                                                                        CssClass="buttonCommman" OnClick="btn_PopUp_Reset_Click" TabIndex="11" 
                                                                        Text="<%$ Resources:Attendance,Cancel %>" Width="65px" />
                                                                    &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                                                                    <asp:HiddenField ID="HiddenField1" runat="server" />
                                                                </td>
                                                            </tr>
                                                        </tr>
                                                    </table>
                                                </asp:Panel>
                                            </asp:Panel>
                                        </asp:Panel>
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
