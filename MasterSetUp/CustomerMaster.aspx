<%@ Page Language="C#" MasterPageFile="~/ERPMaster.master" AutoEventWireup="true"
    CodeFile="CustomerMaster.aspx.cs" Inherits="MasterSetUp_CustomerMaster" Title="Untitled Page" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <link href="../App_Themes/AJAX.css" rel="stylesheet" type="text/css" />
    <asp:UpdatePanel ID="upallpage" runat="server">
        <Triggers>
            <asp:PostBackTrigger ControlID="btnList" />
            <asp:PostBackTrigger ControlID="btnNew" />
            <asp:PostBackTrigger ControlID="btnBin" />
            <asp:PostBackTrigger ControlID="GvCustomer" />
        </Triggers>
        <ContentTemplate>
            <table width="100%">
                <tr>
                    <td align='<%= Common.ChangeTDForDefaultLeft()%>'>
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
                                            <td style="padding-left: 5px">
                                                <asp:Label ID="lblHeader" runat="server" Text="<%$ Resources:Attendance,Customer Setup %>"
                                                    CssClass="LableHeaderTitle"></asp:Label>
                                            </td>
                                        </tr>
                                    </table>
                                </td>
                                <td align="right">
                                    <table cellpadding="0" cellspacing="0">
                                        <tr>
                                            <td>
                                                <asp:Panel ID="pnlMenuList" runat="server" CssClass="a">
                                                    <asp:Button ID="btnList" runat="server" Text="<%$ Resources:Attendance,List %>" Width="90px"
                                                        BorderStyle="none" BackColor="Transparent" OnClick="btnList_Click" Style="padding-left: 10px;
                                                        padding-top: 3px; background-image: url(  '../Images/List.png' ); background-repeat: no-repeat;
                                                        height: 49px; background-position: 5px 15px; font: bold 14px Trebuchet MS; color: #000000;" />
                                                </asp:Panel>
                                            </td>
                                            <td>
                                                <asp:Panel ID="pnlMenuNew" runat="server" CssClass="a">
                                                    <asp:Button ID="btnNew" runat="server" Text="<%$ Resources:Attendance,New %>" Width="90px"
                                                        BorderStyle="none" BackColor="Transparent" OnClick="btnNew_Click" Style="padding-left: 10px;
                                                        padding-top: 3px; background-image: url('../Images/New.png' ); background-repeat: no-repeat;
                                                        height: 49px; background-position: 5px 15px; font: bold 14px Trebuchet MS; color: #000000;" />
                                                </asp:Panel>
                                            </td>
                                            <td>
                                                <asp:Panel ID="pnlMenuBin" runat="server" CssClass="a" Width="100%">
                                                    <asp:Button ID="btnBin" runat="server" Text="<%$ Resources:Attendance,Bin %>" Width="90px"
                                                        BorderStyle="none" BackColor="Transparent" OnClick="btnBin_Click" Style="padding-left: 10px;
                                                        padding-top: 3px; background-image: url(  '../Images/Bin.png' ); background-repeat: no-repeat;
                                                        height: 49px; background-position: 5px 15px; font: bold 14px Trebuchet MS; color: #000000;" />
                                                </asp:Panel>
                                            </td>
                                        </tr>
                                    </table>
                                </td>
                            </tr>
                            <tr>
                                <td bgcolor="#ccddee" colspan="4" width="100%" height="500px" valign="top">
                                    <asp:Panel ID="PnlList" runat="server">
                                        <asp:Panel ID="pnlSearchRecords" runat="server" DefaultButton="btnbind">
                                            <div width="100%" style="background-image: url(../Images/bg_repeat.jpg); background-repeat: repeat;">
                                                <table width="100%" style="padding-left: 20px; height: 38px">
                                                    <tr>
                                                        <td width="90px">
                                                            <asp:Label ID="lblSelectField" runat="server" Text="<%$ Resources:Attendance,Select Field %>"
                                                                CssClass="labelComman"></asp:Label>
                                                        </td>
                                                        <td width="180px">
                                                            <asp:DropDownList ID="ddlFieldName" runat="server" CssClass="DropdownSearch" Height="25px"
                                                                Width="170px">
                                                                <asp:ListItem Text="<%$ Resources:Attendance, Customer Id %>" Value="Customer_Id"></asp:ListItem>
                                                                <asp:ListItem Text="<%$ Resources:Attendance, Customer Name %>" Value="Contact_Name"
                                                                    Selected="True"></asp:ListItem>
                                                                <asp:ListItem Text="<%$ Resources:Attendance, Customer Name(Local) %>" Value="Contact_Name_L"></asp:ListItem>
                                                            </asp:DropDownList>
                                                        </td>
                                                        <td width="135px">
                                                            <asp:DropDownList ID="ddlOption" runat="server" CssClass="DropdownSearch" Height="25px"
                                                                Width="120px">
                                                                <asp:ListItem Text="<%$ Resources:Attendance,--Select-- %>"></asp:ListItem>
                                                                <asp:ListItem Text="<%$ Resources:Attendance,Equal %>"></asp:ListItem>
                                                                <asp:ListItem Selected="True" Text="<%$ Resources:Attendance,Contains %>"></asp:ListItem>
                                                                <asp:ListItem Text="<%$ Resources:Attendance,Like %>"></asp:ListItem>
                                                            </asp:DropDownList>
                                                        </td>
                                                        <td width="270px">
                                                            <asp:TextBox ID="txtValue" runat="server" CssClass="textCommanSearch" Height="14px"
                                                                Width="250px"></asp:TextBox>
                                                        </td>
                                                        <td width="50px" align="center">
                                                            <asp:ImageButton ID="btnbind" runat="server" CausesValidation="False" Height="25px"
                                                                ImageUrl="~/Images/search.png" OnClick="btnbindrpt_Click" Width="25px" ToolTip="<%$ Resources:Attendance,Search %>">
                                                            </asp:ImageButton>
                                                        </td>
                                                        <td>
                                                            <asp:Panel ID="PnlRefresh" runat="server" DefaultButton="btnRefresh">
                                                                <asp:ImageButton ID="btnRefresh" runat="server" CausesValidation="False" Height="25px"
                                                                    ImageUrl="~/Images/refresh.png" OnClick="btnRefreshReport_Click" Width="25px"
                                                                    ToolTip="<%$ Resources:Attendance,Refresh %>"></asp:ImageButton>
                                                            </asp:Panel>
                                                        </td>
                                                        <td align="center">
                                                            <asp:Label ID="lblTotalRecords" runat="server" Text="<%$ Resources:Attendance,Total Records: 0 %>"
                                                                CssClass="labelComman"></asp:Label>
                                                        </td>
                                                    </tr>
                                                </table>
                                            </div>
                                            <asp:GridView ID="GvCustomer" PageSize="10" runat="server" AutoGenerateColumns="False"
                                                Width="100%" AllowPaging="True" OnPageIndexChanging="GvCustomer_PageIndexChanging"
                                                AllowSorting="True" OnSorting="GvCustomer_Sorting" CssClass="grid">
                                                <AlternatingRowStyle CssClass="InvgridAltRow" />
                                                <Columns>
                                                    <asp:TemplateField HeaderText="<%$ Resources:Attendance,Edit %>">
                                                        <ItemTemplate>
                                                            <asp:ImageButton ID="btnEdit" runat="server" CommandArgument='<%# Eval("Customer_Id") %>'
                                                                ImageUrl="~/Images/edit.png" OnCommand="btnEdit_Command" ToolTip="<%$ Resources:Attendance,Edit %>"
                                                                CausesValidation="False" Visible="false" /></ItemTemplate>
                                                        <ItemStyle CssClass="grid" HorizontalAlign="Center" />
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="<%$ Resources:Attendance,Delete %>">
                                                        <ItemTemplate>
                                                            <asp:ImageButton ID="IbtnDelete" runat="server" CausesValidation="False" CommandArgument='<%# Eval("Customer_Id") %>'
                                                                ImageUrl="~/Images/Erase.png" OnCommand="IbtnDelete_Command" ToolTip="<%$ Resources:Attendance,Delete %>"
                                                                Width="16px" Visible="false" />
                                                        </ItemTemplate>
                                                        <ItemStyle CssClass="grid" HorizontalAlign="Center" />
                                                    </asp:TemplateField>
                                                    <asp:BoundField DataField="Customer_Id" HeaderText="<%$ Resources:Attendance,Customer Id %>"
                                                        SortExpression="Customer_Id" ItemStyle-CssClass="grid" />
                                                    <asp:BoundField DataField="Contact_Name" HeaderText="<%$ Resources:Attendance,Customer Name %>"
                                                        SortExpression="Contact_Name" ItemStyle-CssClass="grid" />
                                                    <asp:BoundField DataField="Contact_Name_L" HeaderText="<%$ Resources:Attendance,Customer Name(Local) %>"
                                                        SortExpression="Contact_Name_L" ItemStyle-CssClass="grid" />
                                                    <asp:BoundField DataField="Account_No" HeaderText="<%$ Resources:Attendance,Account No %>"
                                                        SortExpression="Account_No" ItemStyle-CssClass="grid" />
                                                    <asp:BoundField DataField="Credit_Limit" HeaderText="<%$ Resources:Attendance,Credit Limit %>"
                                                        SortExpression="Credit_Limit" ItemStyle-CssClass="grid" />
                                                </Columns>
                                                <HeaderStyle CssClass="Invgridheader" />
                                                <PagerStyle CssClass="Invgridheader" />
                                                <RowStyle CssClass="Invgridrow" HorizontalAlign="Center" />
                                            </asp:GridView>
                                        </asp:Panel>
                                    </asp:Panel>
                                    <asp:Panel ID="PnlNewEdit" runat="server" DefaultButton="btnAddressSave">
                                        <table width="100%" style="padding-left: 43px">
                                            <tr>
                                                <td align='<%= Common.ChangeTDForDefaultLeft()%>'>
                                                    <asp:Label ID="lblCustomerName" runat="server" CssClass="labelComman" Text="<%$ Resources:Attendance,Customer Name %>" />
                                                </td>
                                                <td align="center">
                                                    :
                                                </td>
                                                <td align='<%= Common.ChangeTDForDefaultLeft()%>' colspan="4">
                                                    <asp:TextBox ID="txtCustomerName" runat="server" CssClass="textComman" Width="713px"
                                                        BackColor="#c3c3c3" />
                                                    <cc1:AutoCompleteExtender ID="txtCustomerName_AutoCompleteExtender" runat="server"
                                                        DelimiterCharacters="" Enabled="True" CompletionInterval="100" CompletionSetCount="1"
                                                        MinimumPrefixLength="1" ServiceMethod="GetCompletionList" ServicePath="" TargetControlID="txtCustomerName"
                                                        UseContextKey="True" CompletionListCssClass="completionList" CompletionListItemCssClass="listItem"
                                                        CompletionListHighlightedItemCssClass="itemHighlighted">
                                                    </cc1:AutoCompleteExtender>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td align='<%= Common.ChangeTDForDefaultLeft()%>'>
                                                    <asp:Label ID="lblLCustomerName" runat="server" CssClass="labelComman" Text="<%$ Resources:Attendance,Customer Name(Local) %>" />
                                                </td>
                                                <td align="center">
                                                    :
                                                </td>
                                                <td align='<%= Common.ChangeTDForDefaultLeft()%>' colspan="4">
                                                    <asp:TextBox ID="txtLCusomerName" runat="server" CssClass="textComman" Width="713px"
                                                        BackColor="#c3c3c3" />
                                                </td>
                                            </tr>
                                            <tr>
                                                <td align='<%= Common.ChangeTDForDefaultLeft()%>'>
                                                    <asp:Label ID="lblNickName" runat="server" CssClass="labelComman" Text="<%$ Resources:Attendance,Nick Name %>" />
                                                </td>
                                                <td align="center">
                                                    :
                                                </td>
                                                <td align='<%= Common.ChangeTDForDefaultLeft()%>' colspan="1">
                                                    <asp:TextBox ID="txtNickName" runat="server" CssClass="textComman" />
                                                </td>
                                                <td align='<%= Common.ChangeTDForDefaultLeft()%>'>
                                                    <asp:Label ID="lblCivilId" runat="server" CssClass="labelComman" Text="<%$ Resources:Attendance,Civil Id %>" />
                                                </td>
                                                <td align="center">
                                                    :
                                                </td>
                                                <td align='<%= Common.ChangeTDForDefaultLeft()%>' colspan="4">
                                                    <asp:TextBox ID="txtCivilId" runat="server" CssClass="textComman" />
                                                </td>
                                            </tr>
                                            <tr>
                                                <td align='<%= Common.ChangeTDForDefaultLeft()%>' width="150px">
                                                    <asp:Label ID="lblCompName" runat="server" CssClass="labelComman" Text="<%$ Resources:Attendance,Company Name %>"></asp:Label>
                                                </td>
                                                <td>
                                                    :
                                                </td>
                                                <td align='<%= Common.ChangeTDForDefaultLeft()%>' colspan="2">
                                                    <asp:Panel ID="pnlComp" runat="server" DefaultButton="ImgAddCompany">
                                                        <table>
                                                            <tr>
                                                                <td>
                                                                    <asp:TextBox ID="txtCname" runat="server" BackColor="#c3c3c3" CssClass="textComman" />
                                                                    <cc1:AutoCompleteExtender ID="txtCname_AutoCompleteExtender" runat="server" CompletionInterval="100"
                                                                        CompletionSetCount="1" DelimiterCharacters="" Enabled="True" MinimumPrefixLength="1"
                                                                        ServiceMethod="GetCompletionList2" ServicePath="" TargetControlID="txtCname"
                                                                        UseContextKey="True" CompletionListCssClass="completionList" CompletionListItemCssClass="listItem"
                                                                        CompletionListHighlightedItemCssClass="itemHighlighted">
                                                                    </cc1:AutoCompleteExtender>
                                                                </td>
                                                                <td align="left" valign="bottom">
                                                                    <asp:Button ID="ImgAddCompany" runat="server" Text="<%$ Resources:Attendance,New %>"
                                                                        BackColor="Transparent" BorderStyle="None" Font-Bold="true" CssClass="btnNewAdress"
                                                                        Font-Size="13px" Font-Names=" Arial" Width="92px" Height="32px" OnClick="ImgAddCompany_Click" />
                                                                </td>
                                                            </tr>
                                                        </table>
                                                    </asp:Panel>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td align='<%= Common.ChangeTDForDefaultLeft()%>'>
                                                    <asp:Label ID="lblAddressName" runat="server" CssClass="labelComman" Text="<%$ Resources:Attendance,Address Name %>" />
                                                </td>
                                                <td align="center">
                                                    :
                                                </td>
                                                <td align='<%= Common.ChangeTDForDefaultLeft()%>' colspan="4">
                                                    <asp:Panel ID="pnlAddress" runat="server" DefaultButton="imgAddAddressName">
                                                        <table>
                                                            <tr>
                                                                <td>
                                                                    <asp:TextBox ID="txtAddressName" runat="server" CssClass="textComman" AutoPostBack="true"
                                                                        OnTextChanged="txtAddressName_TextChanged" BackColor="#c3c3c3" />
                                                                    <cc1:AutoCompleteExtender ID="AutoCompleteExtender2" runat="server" DelimiterCharacters=""
                                                                        Enabled="True" ServiceMethod="GetCompletionListAddressName" ServicePath="" CompletionInterval="100"
                                                                        MinimumPrefixLength="1" CompletionSetCount="1" TargetControlID="txtAddressName"
                                                                        UseContextKey="True" CompletionListCssClass="completionList" CompletionListItemCssClass="listItem"
                                                                        CompletionListHighlightedItemCssClass="itemHighlighted">
                                                                    </cc1:AutoCompleteExtender>
                                                                </td>
                                                                <td valign="bottom">
                                                                    <asp:Button ID="imgAddAddressName" runat="server" Text="<%$ Resources:Attendance,Add %>"
                                                                        BackColor="Transparent" BorderStyle="None" Font-Bold="true" CssClass="btnAddAdress"
                                                                        Font-Size="13px" Font-Names=" Arial" Width="92px" Height="32px" OnClick="imgAddAddressName_Click" />
                                                                </td>
                                                                <td valign="bottom">
                                                                    <asp:Button ID="btnAddNewAddress" runat="server" Text="<%$ Resources:Attendance,New %>"
                                                                        BackColor="Transparent" BorderStyle="None" Font-Bold="true" CssClass="btnNewAdress"
                                                                        Font-Size="13px" Font-Names=" Arial" Width="92px" Height="32px" OnClick="btnAddNewAddress_Click" />
                                                                </td>
                                                            </tr>
                                                        </table>
                                                    </asp:Panel>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td colspan="6" align="center">
                                                    <asp:GridView ID="GvAddressName" runat="server" Width="60%" CssClass="grid" AutoGenerateColumns="False">
                                                        <AlternatingRowStyle CssClass="InvgridAltRow" />
                                                        <Columns>
                                                            <asp:TemplateField HeaderText="<%$ Resources:Attendance,Edit %>">
                                                                <ItemTemplate>
                                                                    <asp:ImageButton ID="imgBtnAddressEdit" runat="server" CommandArgument='<%# Eval("Trans_Id") %>'
                                                                        ImageUrl="~/Images/edit.png" Width="16px" OnCommand="btnAddressEdit_Command"
                                                                        Visible="false" ToolTip="<%$ Resources:Attendance,Edit %>" />
                                                                </ItemTemplate>
                                                                <ItemStyle CssClass="grid" HorizontalAlign="Center" />
                                                            </asp:TemplateField>
                                                            <asp:TemplateField HeaderText="<%$ Resources:Attendance,Delete %>">
                                                                <ItemTemplate>
                                                                    <asp:ImageButton ID="imgBtnDelete" runat="server" CommandArgument='<%# Eval("Trans_Id") %>'
                                                                        Height="16px" ImageUrl="~/Images/Erase.png" Width="16px" OnCommand="btnAddressDelete_Command"
                                                                        ToolTip="<%$ Resources:Attendance,Delete %>" Visible="false" />
                                                                </ItemTemplate>
                                                                <ItemStyle CssClass="grid" HorizontalAlign="Center" />
                                                            </asp:TemplateField>
                                                            <asp:TemplateField HeaderText="<%$ Resources:Attendance,S No. %>">
                                                                <ItemTemplate>
                                                                    <asp:Label ID="lblSNo" runat="server" Text='<%#Eval("Trans_Id") %>' />
                                                                </ItemTemplate>
                                                                <ItemStyle CssClass="grid" HorizontalAlign="Center" />
                                                            </asp:TemplateField>
                                                            <asp:TemplateField HeaderText="<%$ Resources:Attendance, Address Name %>">
                                                                <ItemTemplate>
                                                                    <asp:Label ID="lblgvAddressName" runat="server" Text='<%#Eval("Address_Name") %>' />
                                                                </ItemTemplate>
                                                                <ItemStyle CssClass="grid" HorizontalAlign="Center" />
                                                            </asp:TemplateField>
                                                        </Columns>
                                                        <HeaderStyle CssClass="Invgridheader" />
                                                        <PagerStyle CssClass="Invgridheader" />
                                                        <RowStyle CssClass="Invgridrow" HorizontalAlign="Center" />
                                                    </asp:GridView>
                                                    <asp:HiddenField ID="hdnAddressId" runat="server" />
                                                    <asp:HiddenField ID="hdnAddressName" runat="server" />
                                                </td>
                                            </tr>
                                            <tr>
                                                <td align='<%= Common.ChangeTDForDefaultLeft()%>'>
                                                    <asp:Label ID="lblAccountNo" runat="server" CssClass="labelComman" Text="<%$ Resources:Attendance,Account No. %>" />
                                                </td>
                                                <td>
                                                    :
                                                </td>
                                                <td align='<%= Common.ChangeTDForDefaultLeft()%>'>
                                                    <asp:TextBox ID="txtAccountNo" runat="server" CssClass="textComman" />
                                                </td>
                                                <td align='<%= Common.ChangeTDForDefaultLeft()%>'>
                                                    <asp:Label ID="lblCustomerType" runat="server" CssClass="labelComman" Text="<%$ Resources:Attendance,Customer Type %>" />
                                                </td>
                                                <td>
                                                    :
                                                </td>
                                                <td align='<%= Common.ChangeTDForDefaultLeft()%>'>
                                                    <asp:DropDownList ID="ddlCustomerType" runat="server" CssClass="textComman">
                                                        <asp:ListItem Text="--Select--" Value="0" />
                                                        <asp:ListItem Text="Reseller" Value="1" />
                                                        <asp:ListItem Text="Distributer" Value="2" />
                                                    </asp:DropDownList>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td align='<%= Common.ChangeTDForDefaultLeft()%>'>
                                                    <asp:Label ID="lblFoundEmployee" runat="server" CssClass="labelComman" Text="<%$ Resources:Attendance,Found Employee %>" />
                                                </td>
                                                <td>
                                                    :
                                                </td>
                                                <td align='<%= Common.ChangeTDForDefaultLeft()%>'>
                                                    <asp:TextBox ID="txtFoundEmployee" runat="server" CssClass="textComman"
                                                    OnTextChanged="txtFoundEmployee_TextChanged" AutoPostBack="true"
                                                    
                                                    
                                                     />
                                                    <cc1:AutoCompleteExtender ID="AutoCompleteExtender4" runat="server" DelimiterCharacters=""
                                                        Enabled="True" ServiceMethod="GetCompletionListEmployeeName" ServicePath="" CompletionInterval="100"
                                                        MinimumPrefixLength="1" CompletionSetCount="1" TargetControlID="txtFoundEmployee"
                                                        UseContextKey="True" CompletionListCssClass="completionList" CompletionListItemCssClass="listItem"
                                                        CompletionListHighlightedItemCssClass="itemHighlighted">
                                                    </cc1:AutoCompleteExtender>
                                                </td>
                                                <td align='<%= Common.ChangeTDForDefaultLeft()%>'>
                                                    <asp:Label ID="lblHandledEmployee" runat="server" CssClass="labelComman" Text="<%$ Resources:Attendance,Handled Employee %>" />
                                                </td>
                                                <td>
                                                    :
                                                </td>
                                                <td align='<%= Common.ChangeTDForDefaultLeft()%>'>
                                                    <asp:TextBox ID="txtHandledEmployee" runat="server" CssClass="textComman" 
                                                    OnTextChanged="txtHandledEmployee_TextChanged" AutoPostBack="true"
                                                    
                                                    
                                                    />
                                                    <cc1:AutoCompleteExtender ID="AutoCompleteExtender5" runat="server" DelimiterCharacters=""
                                                        Enabled="True" ServiceMethod="GetCompletionListEmployeeName" ServicePath="" CompletionInterval="100"
                                                        MinimumPrefixLength="1" CompletionSetCount="1" TargetControlID="txtHandledEmployee"
                                                        UseContextKey="True" CompletionListCssClass="completionList" CompletionListItemCssClass="listItem"
                                                        CompletionListHighlightedItemCssClass="itemHighlighted">
                                                    </cc1:AutoCompleteExtender>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td align='<%= Common.ChangeTDForDefaultLeft()%>'>
                                                    <asp:Label ID="lblDebitAmount" runat="server" CssClass="labelComman" Text="<%$ Resources:Attendance,Debit Amount %>" />
                                                </td>
                                                <td>
                                                    :
                                                </td>
                                                <td align='<%= Common.ChangeTDForDefaultLeft()%>'>
                                                    <asp:TextBox ID="txtDebitAmount" runat="server" CssClass="textComman" />
                                                </td>
                                                <td align='<%= Common.ChangeTDForDefaultLeft()%>'>
                                                    <asp:Label ID="lblCreditAmount" runat="server" CssClass="labelComman" Text="<%$ Resources:Attendance,Credit Amount %>" />
                                                </td>
                                                <td>
                                                    :
                                                </td>
                                                <td align='<%= Common.ChangeTDForDefaultLeft()%>'>
                                                    <asp:TextBox ID="txtCreditAmount" runat="server" CssClass="textComman" />
                                                </td>
                                            </tr>
                                            <tr>
                                                <td align='<%= Common.ChangeTDForDefaultLeft()%>'>
                                                    <asp:Label ID="lblODebitAmount" runat="server" CssClass="labelComman" Text="<%$ Resources:Attendance,Opening Debit Amount %>" />
                                                </td>
                                                <td>
                                                    :
                                                </td>
                                                <td align='<%= Common.ChangeTDForDefaultLeft()%>'>
                                                    <asp:TextBox ID="txtODebitAmount" runat="server" CssClass="textComman" />
                                                </td>
                                                <td align='<%= Common.ChangeTDForDefaultLeft()%>'>
                                                    <asp:Label ID="lblOCreditAmount" runat="server" CssClass="labelComman" Text="<%$ Resources:Attendance,Opening Credit Amount %>" />
                                                </td>
                                                <td>
                                                    :
                                                </td>
                                                <td align='<%= Common.ChangeTDForDefaultLeft()%>'>
                                                    <asp:TextBox ID="txtOCreditAmount" runat="server" CssClass="textComman" />
                                                </td>
                                            </tr>
                                            <tr>
                                                <td align='<%= Common.ChangeTDForDefaultLeft()%>'>
                                                    <asp:Label ID="lblSalesQuota" runat="server" CssClass="labelComman" Text="<%$ Resources:Attendance,Sales Quota %>" />
                                                </td>
                                                <td>
                                                    :
                                                </td>
                                                <td align='<%= Common.ChangeTDForDefaultLeft()%>'>
                                                    <asp:TextBox ID="txtSalesQuota" runat="server" CssClass="textComman" />
                                                </td>
                                                <td align='<%= Common.ChangeTDForDefaultLeft()%>'>
                                                    <asp:Label ID="lblCreditLimit" runat="server" CssClass="labelComman" Text="<%$ Resources:Attendance,Credit Limit %>" />
                                                </td>
                                                <td>
                                                    :
                                                </td>
                                                <td align='<%= Common.ChangeTDForDefaultLeft()%>'>
                                                    <asp:TextBox ID="txtCreditLimit" runat="server" CssClass="textComman" />
                                                </td>
                                            </tr>
                                            <tr>
                                                <td align='<%= Common.ChangeTDForDefaultLeft()%>'>
                                                    <asp:Label ID="lblCreditDays" runat="server" CssClass="labelComman" Text="<%$ Resources:Attendance,Credit Days %>" />
                                                </td>
                                                <td>
                                                    :
                                                </td>
                                                <td align='<%= Common.ChangeTDForDefaultLeft()%>'>
                                                    <asp:TextBox ID="txtCreditDays" runat="server" CssClass="textComman" />
                                                </td>
                                                <td align='<%= Common.ChangeTDForDefaultLeft()%>'>
                                                    <asp:Label ID="lblPriceLevel" runat="server" CssClass="labelComman" Text="<%$ Resources:Attendance,Price Level %>" />
                                                </td>
                                                <td>
                                                    :
                                                </td>
                                                <td align='<%= Common.ChangeTDForDefaultLeft()%>'>
                                                    <asp:TextBox ID="txtPriceLevel" runat="server" CssClass="textComman" />
                                                </td>
                                            </tr>
                                            <tr>
                                                <td align='<%= Common.ChangeTDForDefaultLeft()%>'>
                                                </td>
                                                <td>
                                                    :
                                                </td>
                                                <td align='<%= Common.ChangeTDForDefaultLeft()%>'>
                                                    <asp:CheckBox ID="chkIsTaxable" runat="server" Text="<%$ Resources:Attendance,Is Taxable %>" />
                                                </td>
                                            </tr>
                                            <tr>
                                                <td colspan="6" align="center" style="padding-left: 10px">
                                                    <table>
                                                        <tr>
                                                            <td width="90px">
                                                                <asp:Button ID="btnCustomerSave" runat="server" Text="<%$ Resources:Attendance,Save %>"
                                                                    CssClass="buttonCommman" Visible="false" OnClick="btnCustomerSave_Click" />
                                                            </td>
                                                            <td width="90px">
                                                                <asp:Button ID="BtnReset" runat="server" Text="<%$ Resources:Attendance,Reset %>"
                                                                    CssClass="buttonCommman" CausesValidation="False" OnClick="BtnReset_Click" />
                                                            </td>
                                                            <td width="90px">
                                                                <asp:Button ID="btnCustomerCancel" runat="server" CssClass="buttonCommman" Text="<%$ Resources:Attendance,Cancel %>"
                                                                    CausesValidation="False" OnClick="btnCustomerCancel_Click" />
                                                            </td>
                                                        </tr>
                                                    </table>
                                                    <asp:HiddenField ID="hdnCustomerId" runat="server" />
                                                </td>
                                            </tr>
                                        </table>
                                    </asp:Panel>
                                    <asp:Panel ID="PnlBin" runat="server" DefaultButton="btnbindBin">
                                        <div width="100%" style="background-image: url(../Images/bg_repeat.jpg); background-repeat: repeat;">
                                            <table width="100%" style="padding-left: 20px; height: 38px">
                                                <tr>
                                                    <td width="90px">
                                                        <asp:Label ID="Label1" runat="server" Text="<%$ Resources:Attendance,Select Field %>"
                                                            CssClass="labelComman"></asp:Label>
                                                    </td>
                                                    <td width="180px">
                                                        <asp:DropDownList ID="ddlFieldNameBin" runat="server" CssClass="DropdownSearch" Height="25px"
                                                            Width="170px">
                                                            <asp:ListItem Text="<%$ Resources:Attendance, Customer Id %>" Value="Customer_Id"></asp:ListItem>
                                                            <asp:ListItem Text="<%$ Resources:Attendance, Customer Name %>" Value="Contact_Name"
                                                                Selected="True"></asp:ListItem>
                                                            <asp:ListItem Text="<%$ Resources:Attendance, Customer Name(Local) %>" Value="Contact_Name_L"></asp:ListItem>
                                                        </asp:DropDownList>
                                                    </td>
                                                    <td width="135px">
                                                        <asp:DropDownList ID="ddlOptionBin" runat="server" CssClass="DropdownSearch" Height="25px"
                                                            Width="120px">
                                                            <asp:ListItem Text="<%$ Resources:Attendance,--Select-- %>"></asp:ListItem>
                                                            <asp:ListItem Text="<%$ Resources:Attendance,Equal %>"></asp:ListItem>
                                                            <asp:ListItem Selected="True" Text="<%$ Resources:Attendance,Contains %>"></asp:ListItem>
                                                            <asp:ListItem Text="<%$ Resources:Attendance,Like %>"></asp:ListItem>
                                                        </asp:DropDownList>
                                                    </td>
                                                    <td width="270px">
                                                        <asp:TextBox ID="txtValueBin" runat="server" CssClass="textCommanSearch" Height="14px"
                                                            Width="250px"></asp:TextBox>
                                                    </td>
                                                    <td width="50px" align="center">
                                                        <asp:ImageButton ID="btnbindBin" runat="server" CausesValidation="False" Height="25px"
                                                            ImageUrl="~/Images/search.png" OnClick="btnbindBin_Click" Width="25px" ToolTip="<%$ Resources:Attendance,Search %>">
                                                        </asp:ImageButton>
                                                    </td>
                                                    <td>
                                                        <asp:Panel ID="Panel1" runat="server" DefaultButton="btnRefreshBin">
                                                            <asp:ImageButton ID="btnRefreshBin" runat="server" CausesValidation="False" Height="25px"
                                                                ImageUrl="~/Images/refresh.png" OnClick="btnRefreshBin_Click" Width="25px" ToolTip="<%$ Resources:Attendance,Refresh %>">
                                                            </asp:ImageButton>
                                                        </asp:Panel>
                                                    </td>
                                                    <td width="30px" align="center">
                                                        <asp:Panel ID="pnlimgBtnRestore" runat="server" DefaultButton="imgBtnRestore">
                                                            <asp:ImageButton ID="imgBtnRestore" Height="25px" Width="25px" CausesValidation="False"
                                                                runat="server" ImageUrl="~/Images/active.png" OnClick="imgBtnRestore_Click" ToolTip="<%$ Resources:Attendance, Active %>" />
                                                        </asp:Panel>
                                                    </td>
                                                    <td>
                                                        <asp:Panel ID="pnlImgbtnSelectAll" runat="server" DefaultButton="ImgbtnSelectAll">
                                                            <asp:ImageButton ID="ImgbtnSelectAll" runat="server" OnClick="ImgbtnSelectAll_Click"
                                                                ToolTip="<%$ Resources:Attendance, Select All %>" AutoPostBack="true" ImageUrl="~/Images/selectAll.png" />
                                                        </asp:Panel>
                                                    </td>
                                                    <td align="center">
                                                        <asp:Label ID="lblTotalRecordsBin" runat="server" Text="<%$ Resources:Attendance,Total Records: 0 %>"
                                                            CssClass="labelComman"></asp:Label>
                                                    </td>
                                                </tr>
                                            </table>
                                        </div>
                                        <asp:Label ID="lblSelectedRecord" runat="server" Visible="false"></asp:Label>
                                        <asp:GridView ID="GvCustomerBin" PageSize="10" runat="server" AutoGenerateColumns="False"
                                            Width="100%" AllowPaging="True" OnPageIndexChanging="GvCustomerBin_PageIndexChanging"
                                            OnSorting="GvCustomerBin_OnSorting" AllowSorting="true" CssClass="grid">
                                            <Columns>
                                                <asp:TemplateField>
                                                    <HeaderTemplate>
                                                        <asp:CheckBox ID="chkCurrent" runat="server" OnCheckedChanged="chkCurrent_CheckedChanged"
                                                            AutoPostBack="true" />
                                                    </HeaderTemplate>
                                                    <ItemTemplate>
                                                        <asp:CheckBox ID="chkSelect" runat="server" AutoPostBack="true" OnCheckedChanged="chkSelect_CheckedChanged" />
                                                    </ItemTemplate>
                                                    <ItemStyle CssClass="grid" HorizontalAlign="Center" />
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="<%$ Resources:Attendance,Customer Id%>" SortExpression="Customer_Id">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblgvCustomerId" runat="server" Text='<%# Eval("Customer_Id") %>' />
                                                    </ItemTemplate>
                                                    <ItemStyle CssClass="grid" />
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="<%$ Resources:Attendance,Customer Name %>" SortExpression="Contact_Name">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblgvCustomerName" runat="server" Text='<%# Eval("Contact_Name") %>' />
                                                    </ItemTemplate>
                                                    <ItemStyle CssClass="grid" />
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="<%$ Resources:Attendance,Customer Name(Local) %>"
                                                    SortExpression="Contact_Name_L">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblgvLCustomerName" runat="server" Text='<%# Eval("Contact_Name_L") %>' />
                                                    </ItemTemplate>
                                                    <ItemStyle CssClass="grid" />
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="<%$ Resources:Attendance,Account No %>" SortExpression="Account_No">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblgvAccountNo" runat="server" Text='<%# Eval("Account_No") %>' />
                                                    </ItemTemplate>
                                                    <ItemStyle CssClass="grid" />
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="<%$ Resources:Attendance,Credit Limit %>" SortExpression="Credit_Limit">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblgvCreditLimit" runat="server" Text='<%# Eval("Credit_Limit") %>' />
                                                    </ItemTemplate>
                                                    <ItemStyle CssClass="grid" />
                                                </asp:TemplateField>
                                            </Columns>
                                            <AlternatingRowStyle CssClass="InvgridAltRow"></AlternatingRowStyle>
                                            <HeaderStyle CssClass="Invgridheader" />
                                            <PagerStyle CssClass="Invgridheader" />
                                            <RowStyle CssClass="Invgridrow" HorizontalAlign="Center"></RowStyle>
                                        </asp:GridView>
                                        <asp:HiddenField ID="HDFSortbin" runat="server" />
                                    </asp:Panel>
                                    <asp:Panel ID="pnlCompany1" runat="server" class="MsgOverlayAddress" Visible="False">
                                        <asp:Panel ID="pnlCompany2" runat="server" class="MsgPopUpPanelAddress" Visible="False">
                                            <asp:Panel ID="pnlCompany3" runat="server" Style="width: 100%; height: 100%; text-align: center;">
                                                <div width="100%" style="background-image: url(../Images/bg_repeat.jpg); background-repeat: repeat;">
                                                    <table width="100%">
                                                        <tr>
                                                            <td width="300px" align="left">
                                                                <asp:Label ID="Label6" runat="server" Text="<%$ Resources:Attendance,Add Company %>"
                                                                    CssClass="labelComman" Font-Bold="True" Font-Size="15px" />
                                                            </td>
                                                            <td align="right">
                                                                <asp:ImageButton ID="ImgClose" runat="server" ImageUrl="~/Images/close.png" OnClick="ImgClose_Click" />
                                                            </td>
                                                        </tr>
                                                    </table>
                                                </div>
                                                <table>
                                                    <tr>
                                                        <td align="left">
                                                            <table>
                                                                <tr>
                                                                    <td align="<%= Common.ChangeTDForDefaultLeft()%>">
                                                                        <asp:Label ID="lblCompanyName" runat="server" CssClass="labelComman" Text="<%$ Resources:Attendance,Company Name %>" />
                                                                    </td>
                                                                    <td>
                                                                        :
                                                                    </td>
                                                                    <td align="<%= Common.ChangeTDForDefaultLeft()%>">
                                                                        <asp:TextBox ID="txtCompanyName" runat="server" CssClass="textComman" />
                                                                    </td>
                                                                </tr>
                                                                <tr>
                                                                    <td align="<%= Common.ChangeTDForDefaultLeft()%>">
                                                                        <asp:Label ID="lblCompanyNameL" runat="server" CssClass="labelComman" Text="<%$ Resources:Attendance,LCompany Name %>" />
                                                                    </td>
                                                                    <td>
                                                                        :
                                                                    </td>
                                                                    <td align="<%= Common.ChangeTDForDefaultLeft()%>">
                                                                        <asp:TextBox ID="txtCompanyNameL" runat="server" CssClass="textComman" />
                                                                    </td>
                                                                </tr>
                                                                <tr>
                                                                    <td align='<%= Common.ChangeTDForDefaultLeft()%>'>
                                                                        <asp:Label ID="lblCompanyAddressName" runat="server" CssClass="labelComman" Text="<%$ Resources:Attendance,Address Name %>" />
                                                                    </td>
                                                                    <td align="center">
                                                                        :
                                                                    </td>
                                                                    <td align='<%= Common.ChangeTDForDefaultLeft()%>'>
                                                                        <asp:Panel ID="pnlCompanyAddress" runat="server" DefaultButton="imgAddCompanyAddressName">
                                                                            <table>
                                                                                <tr>
                                                                                    <td>
                                                                                        <asp:TextBox ID="txtCompanyAddress" runat="server" CssClass="textComman" AutoPostBack="true"
                                                                                            OnTextChanged="txtCompanyAddress_TextChanged" BackColor="#c3c3c3" />
                                                                                        <cc1:AutoCompleteExtender ID="AutoCompleteExtender1" runat="server" DelimiterCharacters=""
                                                                                            Enabled="True" ServiceMethod="GetCompletionListAddressName" ServicePath="" CompletionInterval="100"
                                                                                            MinimumPrefixLength="1" CompletionSetCount="1" TargetControlID="txtCompanyAddress"
                                                                                            UseContextKey="True" CompletionListCssClass="completionList" CompletionListItemCssClass="listItem"
                                                                                            CompletionListHighlightedItemCssClass="itemHighlighted">
                                                                                        </cc1:AutoCompleteExtender>
                                                                                    </td>
                                                                                    <td valign="bottom">
                                                                                        <asp:ImageButton ID="imgAddCompanyAddressName" runat="server" ImageUrl="~/Images/add.png"
                                                                                            OnClick="imgAddCompanyAddressName_Click" />
                                                                                    </td>
                                                                                </tr>
                                                                            </table>
                                                                        </asp:Panel>
                                                                    </td>
                                                                </tr>
                                                                <tr>
                                                                    <td colspan="3" align="center">
                                                                        <asp:GridView ID="GvCompanyAddressName" runat="server" Width="60%" CssClass="grid"
                                                                            AutoGenerateColumns="False">
                                                                            <AlternatingRowStyle CssClass="InvgridAltRow" />
                                                                            <Columns>
                                                                                <asp:TemplateField HeaderText="<%$ Resources:Attendance,Edit %>">
                                                                                    <ItemTemplate>
                                                                                        <asp:ImageButton ID="imgBtnCompanyAddressEdit" runat="server" CommandArgument='<%# Eval("Trans_Id") %>'
                                                                                            ImageUrl="~/Images/edit.png" Width="16px" ToolTip="<%$ Resources:Attendance,Edit %>"
                                                                                            OnCommand="imgBtnCompanyAddressEdit_Command" Visible="false" />
                                                                                    </ItemTemplate>
                                                                                    <ItemStyle CssClass="grid" HorizontalAlign="Center" />
                                                                                </asp:TemplateField>
                                                                                <asp:TemplateField HeaderText="<%$ Resources:Attendance,Delete %>">
                                                                                    <ItemTemplate>
                                                                                        <asp:ImageButton ID="imgBtnCompanyAddressDelete" runat="server" CommandArgument='<%# Eval("Trans_Id") %>'
                                                                                            Height="16px" ImageUrl="~/Images/Erase.png" Width="16px" ToolTip="<%$ Resources:Attendance,Delete %>"
                                                                                            OnCommand="imgBtnCompanyAddressDelete_Command" Visible="false" />
                                                                                    </ItemTemplate>
                                                                                    <ItemStyle CssClass="grid" HorizontalAlign="Center" />
                                                                                </asp:TemplateField>
                                                                                <asp:TemplateField HeaderText="<%$ Resources:Attendance,S No. %>">
                                                                                    <ItemTemplate>
                                                                                        <asp:Label ID="lblCSNo" runat="server" Text='<%#Eval("Trans_Id") %>' />
                                                                                    </ItemTemplate>
                                                                                    <ItemStyle CssClass="grid" HorizontalAlign="Center" />
                                                                                </asp:TemplateField>
                                                                                <asp:TemplateField HeaderText="<%$ Resources:Attendance, Address Name %>">
                                                                                    <ItemTemplate>
                                                                                        <asp:Label ID="lblgvCAddressName" runat="server" Text='<%#Eval("Address_Name") %>' />
                                                                                    </ItemTemplate>
                                                                                    <ItemStyle CssClass="grid" HorizontalAlign="Center" />
                                                                                </asp:TemplateField>
                                                                            </Columns>
                                                                            <HeaderStyle CssClass="Invgridheader" />
                                                                            <PagerStyle CssClass="Invgridheader" />
                                                                            <RowStyle CssClass="Invgridrow" HorizontalAlign="Center" />
                                                                        </asp:GridView>
                                                                        <asp:HiddenField ID="hdnCompanyAddressId" runat="server" />
                                                                        <asp:HiddenField ID="hdnCompanyAddressName" runat="server" />
                                                                    </td>
                                                                </tr>
                                                                <tr>
                                                                    <td align="<%= Common.ChangeTDForDefaultLeft()%>">
                                                                        <asp:Label ID="lblParentCompany" runat="server" CssClass="labelComman" Text="<%$ Resources:Attendance,Parent Company %>" />
                                                                    </td>
                                                                    <td>
                                                                        :
                                                                    </td>
                                                                    <td align="<%= Common.ChangeTDForDefaultLeft()%>">
                                                                        <asp:DropDownList ID="ddlParentCompany" runat="server" Width="260px" CssClass="textComman" />
                                                                    </td>
                                                                </tr>
                                                                <tr>
                                                                    <td align="<%= Common.ChangeTDForDefaultLeft()%>">
                                                                        <asp:Label ID="lblCurrency" runat="server" CssClass="labelComman" Text="<%$ Resources:Attendance,Currency %>" />
                                                                    </td>
                                                                    <td>
                                                                        :
                                                                    </td>
                                                                    <td align="<%= Common.ChangeTDForDefaultLeft()%>">
                                                                        <asp:DropDownList ID="ddlCurrency" runat="server" Width="260px" CssClass="textComman" />
                                                                    </td>
                                                                </tr>
                                                            </table>
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td align="center">
                                                            <asp:Button ID="btnCompanySave" runat="server" Text="<%$ Resources:Attendance,Save %>"
                                                                CssClass="buttonCommman" OnClick="btnCompanySave_Click" Visible="False" />
                                                        </td>
                                                    </tr>
                                                </table>
                                            </asp:Panel>
                                        </asp:Panel>
                                    </asp:Panel>
                                    <asp:Panel ID="pnlAddress1" runat="server" class="MsgOverlayAddress" Visible="False">
                                        <asp:Panel ID="pnlAddress2" runat="server" class="MsgPopUpPanelAddress" Visible="False">
                                            <asp:Panel ID="pnlAddress3" runat="server" Style="width: 100%; height: 100%; text-align: center;">
                                                <table width="100%" style="background-image: url(../Images/bg_repeat.jpg); background-repeat: repeat;">
                                                    <tr>
                                                        <td align="<%= Common.ChangeTDForDefaultLeft()%>">
                                                            <asp:Label ID="lblAddressHeader" runat="server" Font-Size="14px" Font-Bold="true"
                                                                CssClass="labelComman" Text="<%$ Resources:Attendance, Address Setup %>"></asp:Label>
                                                        </td>
                                                        <td align="right">
                                                            <asp:ImageButton ID="btnClosePanel" runat="server" ImageUrl="~/Images/close.png"
                                                                CausesValidation="False" OnClick="btnClosePanel_Click" Height="20px" Width="20px" />
                                                        </td>
                                                    </tr>
                                                </table>
                                                <table width="100%" style="padding-left: 43px">
                                                    <tr>
                                                        <td align='<%= Common.ChangeTDForDefaultLeft()%>'>
                                                            <asp:Label ID="lblAddressCategory" runat="server" CssClass="labelComman" Text="<%$ Resources:Attendance,Address Category %>"></asp:Label>
                                                        </td>
                                                        <td align="center">
                                                            :
                                                        </td>
                                                        <td align='<%= Common.ChangeTDForDefaultLeft()%>'>
                                                            <asp:DropDownList ID="ddlAddressCategory" runat="server" CssClass="textComman" />
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td align='<%= Common.ChangeTDForDefaultLeft()%>'>
                                                            <asp:Label ID="Label2" runat="server" CssClass="labelComman" Text="<%$ Resources:Attendance,Address Name %>" />
                                                        </td>
                                                        <td align="center">
                                                            :
                                                        </td>
                                                        <td align='<%= Common.ChangeTDForDefaultLeft()%>' colspan="4">
                                                            <asp:TextBox ID="txtAddressNameNew" runat="server" CssClass="textComman" AutoPostBack="true"
                                                                OnTextChanged="txtAddressNameNew_TextChanged" Width="610px" BackColor="#c3c3c3" />
                                                            <cc1:AutoCompleteExtender ID="AutoCompleteExtender3" runat="server" DelimiterCharacters=""
                                                                Enabled="True" ServiceMethod="GetCompletionListAddressName" ServicePath="" CompletionInterval="100"
                                                                MinimumPrefixLength="1" CompletionSetCount="1" TargetControlID="txtAddressNameNew"
                                                                UseContextKey="True" CompletionListCssClass="completionList" CompletionListItemCssClass="listItem"
                                                                CompletionListHighlightedItemCssClass="itemHighlighted">
                                                            </cc1:AutoCompleteExtender>
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td align='<%= Common.ChangeTDForDefaultLeft()%>'>
                                                            <asp:Label ID="lblAddress" runat="server" CssClass="labelComman" Text="<%$ Resources:Attendance,Address %>" />
                                                        </td>
                                                        <td align="center">
                                                            :
                                                        </td>
                                                        <td align='<%= Common.ChangeTDForDefaultLeft()%>' colspan="4">
                                                            <asp:TextBox ID="txtAddress" Width="610px" TextMode="MultiLine" runat="server" CssClass="textComman" />
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td align='<%= Common.ChangeTDForDefaultLeft()%>'>
                                                            <asp:Label ID="lblStreet" runat="server" CssClass="labelComman" Text="<%$ Resources:Attendance,Street %>" />
                                                        </td>
                                                        <td>
                                                            :
                                                        </td>
                                                        <td align='<%= Common.ChangeTDForDefaultLeft()%>'>
                                                            <asp:TextBox ID="txtStreet" runat="server" CssClass="textComman" />
                                                        </td>
                                                        <td align='<%= Common.ChangeTDForDefaultLeft()%>'>
                                                            <asp:Label ID="lblBlock" runat="server" CssClass="labelComman" Text="<%$ Resources:Attendance,Block %>" />
                                                        </td>
                                                        <td>
                                                            :
                                                        </td>
                                                        <td align='<%= Common.ChangeTDForDefaultLeft()%>'>
                                                            <asp:TextBox ID="txtBlock" runat="server" CssClass="textComman" />
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td align='<%= Common.ChangeTDForDefaultLeft()%>'>
                                                            <asp:Label ID="lblAvenue" runat="server" CssClass="labelComman" Text="<%$ Resources:Attendance,Avenue %>" />
                                                        </td>
                                                        <td>
                                                            :
                                                        </td>
                                                        <td align='<%= Common.ChangeTDForDefaultLeft()%>'>
                                                            <asp:TextBox ID="txtAvenue" runat="server" CssClass="textComman" />
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td align='<%= Common.ChangeTDForDefaultLeft()%>'>
                                                            <asp:Label ID="lblCountry" runat="server" CssClass="labelComman" Text="<%$ Resources:Attendance,Country %>" />
                                                        </td>
                                                        <td>
                                                            :
                                                        </td>
                                                        <td align='<%= Common.ChangeTDForDefaultLeft()%>'>
                                                            <asp:DropDownList ID="ddlCountry" runat="server" CssClass="textComman" />
                                                        </td>
                                                        <td align='<%= Common.ChangeTDForDefaultLeft()%>'>
                                                            <asp:Label ID="lblState" runat="server" CssClass="labelComman" Text="<%$ Resources:Attendance,State %>" />
                                                        </td>
                                                        <td>
                                                            :
                                                        </td>
                                                        <td align='<%= Common.ChangeTDForDefaultLeft()%>'>
                                                            <asp:TextBox ID="txtState" runat="server" CssClass="textComman" />
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td align='<%= Common.ChangeTDForDefaultLeft()%>'>
                                                            <asp:Label ID="lblCity" runat="server" CssClass="labelComman" Text="<%$ Resources:Attendance,City %>" />
                                                        </td>
                                                        <td>
                                                            :
                                                        </td>
                                                        <td align='<%= Common.ChangeTDForDefaultLeft()%>'>
                                                            <asp:TextBox ID="txtCity" runat="server" CssClass="textComman" />
                                                        </td>
                                                        <td align='<%= Common.ChangeTDForDefaultLeft()%>'>
                                                            <asp:Label ID="lblPinCode" runat="server" CssClass="labelComman" Text="<%$ Resources:Attendance,PinCode %>" />
                                                        </td>
                                                        <td>
                                                            :
                                                        </td>
                                                        <td align='<%= Common.ChangeTDForDefaultLeft()%>'>
                                                            <asp:TextBox ID="txtPinCode" runat="server" CssClass="textComman" />
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td align='<%= Common.ChangeTDForDefaultLeft()%>'>
                                                            <asp:Label ID="lblPhoneNo1" runat="server" CssClass="labelComman" Text="<%$ Resources:Attendance,Phone No 1 %>" />
                                                        </td>
                                                        <td>
                                                            :
                                                        </td>
                                                        <td align='<%= Common.ChangeTDForDefaultLeft()%>'>
                                                            <asp:TextBox ID="txtPhoneNo1" runat="server" CssClass="textComman" />
                                                        </td>
                                                        <td align='<%= Common.ChangeTDForDefaultLeft()%>'>
                                                            <asp:Label ID="lblPhoneNo2" runat="server" CssClass="labelComman" Text="<%$ Resources:Attendance,Phone No 2 %>" />
                                                        </td>
                                                        <td>
                                                            :
                                                        </td>
                                                        <td align='<%= Common.ChangeTDForDefaultLeft()%>'>
                                                            <asp:TextBox ID="txtPhoneNo2" runat="server" CssClass="textComman" />
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td align='<%= Common.ChangeTDForDefaultLeft()%>'>
                                                            <asp:Label ID="lblMobileNo1" runat="server" CssClass="labelComman" Text="<%$ Resources:Attendance,Mobile No 1 %>" />
                                                        </td>
                                                        <td>
                                                            :
                                                        </td>
                                                        <td align='<%= Common.ChangeTDForDefaultLeft()%>'>
                                                            <asp:TextBox ID="txtMobileNo1" runat="server" CssClass="textComman" />
                                                        </td>
                                                        <td align='<%= Common.ChangeTDForDefaultLeft()%>'>
                                                            <asp:Label ID="lblMobileNo2" runat="server" CssClass="labelComman" Text="<%$ Resources:Attendance,Mobile No 2 %>" />
                                                        </td>
                                                        <td>
                                                            :
                                                        </td>
                                                        <td align='<%= Common.ChangeTDForDefaultLeft()%>'>
                                                            <asp:TextBox ID="txtMobileNo2" runat="server" CssClass="textComman" />
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td align='<%= Common.ChangeTDForDefaultLeft()%>'>
                                                            <asp:Label ID="lblEmailId1" runat="server" CssClass="labelComman" Text="<%$ Resources:Attendance,Email Id 1 %>" />
                                                        </td>
                                                        <td>
                                                            :
                                                        </td>
                                                        <td align='<%= Common.ChangeTDForDefaultLeft()%>'>
                                                            <asp:TextBox ID="txtEmailId1" runat="server" CssClass="textComman" />
                                                        </td>
                                                        <td align='<%= Common.ChangeTDForDefaultLeft()%>'>
                                                            <asp:Label ID="lblEmailId2" runat="server" CssClass="labelComman" Text="<%$ Resources:Attendance,Email Id 2 %>" />
                                                        </td>
                                                        <td>
                                                            :
                                                        </td>
                                                        <td align='<%= Common.ChangeTDForDefaultLeft()%>'>
                                                            <asp:TextBox ID="txtEmailId2" runat="server" CssClass="textComman" />
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td align='<%= Common.ChangeTDForDefaultLeft()%>'>
                                                            <asp:Label ID="lblFaxNo" runat="server" CssClass="labelComman" Text="<%$ Resources:Attendance,Fax No %>" />
                                                        </td>
                                                        <td>
                                                            :
                                                        </td>
                                                        <td align='<%= Common.ChangeTDForDefaultLeft()%>'>
                                                            <asp:TextBox ID="txtFaxNo" runat="server" CssClass="textComman" />
                                                        </td>
                                                        <td align='<%= Common.ChangeTDForDefaultLeft()%>'>
                                                            <asp:Label ID="lblWebsite" runat="server" CssClass="labelComman" Text="<%$ Resources:Attendance,Website %>" />
                                                        </td>
                                                        <td>
                                                            :
                                                        </td>
                                                        <td align='<%= Common.ChangeTDForDefaultLeft()%>'>
                                                            <asp:TextBox ID="txtWebsite" runat="server" CssClass="textComman" />
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td align='<%= Common.ChangeTDForDefaultLeft()%>'>
                                                            <asp:Label ID="lblLongitude" runat="server" CssClass="labelComman" Text="<%$ Resources:Attendance,Longitude %>" />
                                                        </td>
                                                        <td>
                                                            :
                                                        </td>
                                                        <td align='<%= Common.ChangeTDForDefaultLeft()%>'>
                                                            <asp:TextBox ID="txtLongitude" runat="server" CssClass="textComman" />
                                                        </td>
                                                        <td align='<%= Common.ChangeTDForDefaultLeft()%>'>
                                                            <asp:Label ID="lblLatitude" runat="server" CssClass="labelComman" Text="<%$ Resources:Attendance,Latitude %>" />
                                                        </td>
                                                        <td>
                                                            :
                                                        </td>
                                                        <td align='<%= Common.ChangeTDForDefaultLeft()%>'>
                                                            <asp:TextBox ID="txtLatitude" runat="server" CssClass="textComman" />
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td colspan="6" align="center" style="padding-left: 10px">
                                                            <table>
                                                                <tr>
                                                                    <td width="90px">
                                                                        <asp:Button ID="btnAddressSave" runat="server" Text="<%$ Resources:Attendance,Save %>"
                                                                            CssClass="buttonCommman" Visible="false" OnClick="btnAddressSave_Click" />
                                                                    </td>
                                                                    <td width="90px">
                                                                        <asp:Button ID="btnAddressReset" runat="server" Text="<%$ Resources:Attendance,Reset %>"
                                                                            CssClass="buttonCommman" CausesValidation="False" OnClick="btnAddressReset_Click" />
                                                                    </td>
                                                                    <td width="90px">
                                                                        <asp:Button ID="btnAddressCancel" runat="server" CssClass="buttonCommman" Text="<%$ Resources:Attendance,Cancel %>"
                                                                            CausesValidation="False" OnClick="btnAddressCancel_Click" />
                                                                    </td>
                                                                </tr>
                                                            </table>
                                                            <asp:HiddenField ID="HiddenField3" runat="server" />
                                                        </td>
                                                    </tr>
                                                </table>
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
    <asp:UpdateProgress runat="server" AssociatedUpdatePanelID="upallpage" ID="updategvprogress">
        <ProgressTemplate>
            <div id="progressBackgroundFilter">
            </div>
            <div id="processMessage" class="progressBackgroundFilter">
                Loading…<br />
                <br />
                <img alt="Loading" src="../Images/ajax-loader2.gif" class="processMessage" /></div>
        </ProgressTemplate>
    </asp:UpdateProgress>
</asp:Content>
