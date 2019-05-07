<%@ Page Language="C#" MasterPageFile="~/ERPMaster.master" AutoEventWireup="true"
    EnableEventValidation="false" CodeFile="ContactMaster.aspx.cs" Inherits="EmailMarkettingSystem_ContactMaster"
    Title=" PegasusInventory" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <link href="../CSS/AJAX.css" rel="stylesheet" type="text/css" />
    <style>
        .Overlay
        {
            position: fixed;
            top: 0px;
            bottom: 0px;
            left: 0px;
            right: 0px;
            overflow: hidden;
            padding: 0;
            margin: 0;
            background-color: #c2c2c2;
            z-index: 1000;
            height: 100%;
        }
        .PopUpPanel
        {
            position: absolute;
            background-color: #ffffff;
            top: 15%;
            left: 35%;
            z-index: 2001;
            -moz-box-shadow: -0.5px 0px 9px #000000;
            -webkit-box-shadow: -0.5px 0px 9px #000000;
            box-shadow: 3.5px 4px 5px #000000;
            border-radius: 5px;
            -moz-border-radiux: 5px;
            -webkit-border-radiux: 5px;
            border: solid 1px #939191;
        }
        .style1
        {
            width: 242px;
        }
    </style>
    <asp:UpdateProgress runat="server" AssociatedUpdatePanelID="UpdatePanel1" ID="updategvprogress">
        <ProgressTemplate>
            <div id="progressBackgroundFilter">
            </div>
            <div id="processMessage" class="progressBackgroundFilter">
                Loading…<br />
                <br />
                <img alt="Loading" src="../Images/ajax-loader2.gif" class="processMessage" />
            </div>
        </ProgressTemplate>
    </asp:UpdateProgress>
    <asp:UpdatePanel ID="UpdatePanel1" runat="server">
        <Triggers>
            <asp:PostBackTrigger ControlID="btnList" />
            <asp:PostBackTrigger ControlID="btnNew" />
            <asp:PostBackTrigger ControlID="btnBin" />
            <asp:PostBackTrigger ControlID="navTree" />
            <asp:PostBackTrigger ControlID="GvCustomerBrand" />
            <asp:PostBackTrigger ControlID="GvSupplierBrand" />
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
                                                <asp:Label ID="lblHeader" runat="server" Text="<%$ Resources:Attendance,Contact Setup %>"
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
                                                        padding-top: 3px; background-image: url(  '../Images/New.png' ); background-repeat: no-repeat;
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
                                        <asp:Panel ID="PnlSearch" runat="server" DefaultButton="btnbind">
                                            <div width="100%" style="background-image: url(../Images/bg_repeat.jpg); background-repeat: repeat;">
                                                <table width="100%" style="padding-left: 20px; height: 38px">
                                                    <tr>
                                                        <td width="90px">
                                                            <asp:Label ID="lblSelectField" runat="server" Text="<%$ Resources:Attendance,Select Field %>"
                                                                CssClass="labelComman"></asp:Label>
                                                        </td>
                                                        <td width="180px">
                                                            <asp:DropDownList ID="ddlFieldName" runat="server" Width="170px" Height="25px" CssClass="DropdownSearch">
                                                                <asp:ListItem Text="<%$ Resources:Attendance,Company Name %>" Value="Company_Name"></asp:ListItem>
                                                                <asp:ListItem Selected="True" Text="<%$ Resources:Attendance,Contact Name %>" Value="Contact_Name"></asp:ListItem>
                                                                <asp:ListItem Text="<%$ Resources:Attendance,Department %>" Value="Dep_Name"></asp:ListItem>
                                                                <asp:ListItem Text="<%$ Resources:Attendance,Designation %>" Value="Designation"></asp:ListItem>
                                                            </asp:DropDownList>
                                                        </td>
                                                        <td width="135px">
                                                            <asp:DropDownList ID="ddlOption" runat="server" Height="25px" Width="120px" CssClass="DropdownSearch">
                                                                <asp:ListItem Text="<%$ Resources:Attendance,--Select-- %>"></asp:ListItem>
                                                                <asp:ListItem Text="<%$ Resources:Attendance,Equal %>"></asp:ListItem>
                                                                <asp:ListItem Selected="True" Text="<%$ Resources:Attendance,Contains %>"></asp:ListItem>
                                                                <asp:ListItem Text="<%$ Resources:Attendance,Like %>"></asp:ListItem>
                                                            </asp:DropDownList>
                                                        </td>
                                                        <td width="24%">
                                                            <asp:TextBox ID="txtValue" runat="server" CssClass="textCommanSearch" Width="100%" />
                                                        </td>
                                                        <td width="50px" align="center">
                                                            <asp:ImageButton ID="btnbind" runat="server" CausesValidation="False" Height="25px"
                                                                ImageUrl="~/Images/search.png" Width="25px" OnClick="btnbind_Click" ToolTip="<%$ Resources:Attendance,Search %>">
                                                            </asp:ImageButton>
                                                        </td>
                                                        <td>
                                                            <asp:Panel ID="pnlRefresh" runat="server" DefaultButton="btnRefresh">
                                                                <asp:ImageButton ID="btnRefresh" runat="server" CausesValidation="False" Height="25px"
                                                                    ImageUrl="~/Images/refresh.png" Width="25px" OnClick="btnRefresh_Click" ToolTip="<%$ Resources:Attendance,Refresh %>" />
                                                            </asp:Panel>
                                                        </td>
                                                        <td align="center">
                                                            <asp:Label ID="lblTotalRecords" runat="server" CssClass="labelComman" Text="<%$ Resources:Attendance,Total Records : 0 %>"></asp:Label>
                                                        </td>
                                                    </tr>
                                                </table>
                                            </div>
                                        </asp:Panel>
                                        <asp:GridView ID="GridContact" runat="server" AllowPaging="True" PageSize="10" AutoGenerateColumns="False"
                                            Width="100%" AllowSorting="true" OnSorting="GridContact_Sorting" OnPageIndexChanging="GridContact_PageIndexChanging"
                                            CssClass="grid">
                                            <AlternatingRowStyle CssClass="InvgridAltRow" />
                                            <Columns>
                                                <asp:TemplateField HeaderText="<%$ Resources:Attendance,Edit %>">
                                                    <ItemTemplate>
                                                        <asp:ImageButton ID="imgBtnEdit" runat="server" CommandArgument='<%# Eval("Contact_Id") %>'
                                                            ImageUrl="~/Images/edit.png" Visible="false" Width="16px" OnCommand="BtnEdit"
                                                            ToolTip="<%$ Resources:Attendance,Edit %>" />
                                                    </ItemTemplate>
                                                    <ItemStyle CssClass="grid" HorizontalAlign="Center" />
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="<%$ Resources:Attendance,Delete %>">
                                                    <ItemTemplate>
                                                        <asp:ImageButton ID="imgBtnDelete" runat="server" CommandArgument='<%# Eval("Contact_Id") %>'
                                                            Height="16px" ImageUrl="~/Images/Erase.png" Visible="false" Width="16px" OnCommand="BtnDelete"
                                                            ToolTip="<%$ Resources:Attendance,Delete %>" />
                                                    </ItemTemplate>
                                                    <ItemStyle CssClass="grid" HorizontalAlign="Center" />
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="<%$ Resources:Attendance,Contact Code %>" SortExpression="Contact_Code">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblgvContactCode" runat="server" Text='<%#Eval("Contact_Code") %>' />
                                                    </ItemTemplate>
                                                    <ItemStyle CssClass="grid" HorizontalAlign="Center" />
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="<%$ Resources:Attendance,Contact Name %>" SortExpression="Contact_Name">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblgvName" runat="server" Text='<%# Eval("Contact_Name") %>'></asp:Label>
                                                        <asp:Label ID="Label5" runat="server" Visible="false" Text='<%# Eval("Contact_Id") %>'></asp:Label>
                                                    </ItemTemplate>
                                                    <ItemStyle CssClass="grid" HorizontalAlign="Center" />
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="<%$ Resources:Attendance,Contact Name(Local) %>" SortExpression="Contact_Name_L">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblgvCNameL" runat="server" Text='<%#Eval("Contact_Name_L") %>' />
                                                    </ItemTemplate>
                                                    <ItemStyle CssClass="grid" HorizontalAlign="Center" />
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="<%$ Resources:Attendance,Company Name %>" SortExpression="Company_Name">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblgvCName" runat="server" Text='<%#Eval("Company_Name") %>'></asp:Label>
                                                    </ItemTemplate>
                                                    <ItemStyle CssClass="grid" HorizontalAlign="Center" />
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="<%$ Resources:Attendance,Department %>" SortExpression="Dep_Name">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblgvDepartment" runat="server" Text='<%#Eval("Dep_Name") %>'></asp:Label>
                                                    </ItemTemplate>
                                                    <ItemStyle CssClass="grid" HorizontalAlign="Center" />
                                                </asp:TemplateField>
                                            </Columns>
                                            <HeaderStyle CssClass="Invgridheader" />
                                            <PagerStyle CssClass="Invgridheader" />
                                            <RowStyle CssClass="Invgridrow" HorizontalAlign="Center" />
                                        </asp:GridView>
                                    </asp:Panel>
                                    <asp:Panel ID="PnlNew" runat="server">
                                        <asp:HiddenField ID="hdnContactId" runat="server" />
                                        <asp:HiddenField ID="hdnCompId" runat="server" />
                                        <asp:Panel ID="Panel6" runat="server">
                                            <table width="100%" style="padding-left: 43px">
                                                <tr>
                                                    <td align='<%= Common.ChangeTDForDefaultLeft()%>'>
                                                        <asp:Panel ID="panNewEdit" runat="server">
                                                            <table width="100%">
                                                                <tr>
                                                                    <td align='<%= Common.ChangeTDForDefaultLeft()%>'>
                                                                        <asp:Label ID="lblContactCode" runat="server" CssClass="labelComman" Text="<%$ Resources:Attendance,Contact Code %>"></asp:Label>
                                                                    </td>
                                                                    <td>
                                                                        :
                                                                    </td>
                                                                    <td align='<%= Common.ChangeTDForDefaultLeft()%>' width="280px">
                                                                        <asp:TextBox ID="txtContactCode" runat="server" CssClass="textComman" />
                                                                    </td>
                                                                </tr>
                                                                <tr>
                                                                    <td align='<%= Common.ChangeTDForDefaultLeft()%>' width="150px">
                                                                        <asp:Label ID="lblContactName" runat="server" CssClass="labelComman" Text="<%$ Resources:Attendance,Contact Name %>"></asp:Label>
                                                                    </td>
                                                                    <td>
                                                                        :
                                                                    </td>
                                                                    <td align='<%= Common.ChangeTDForDefaultLeft()%>' colspan="4">
                                                                        <asp:TextBox ID="txtContactName" runat="server" CssClass="textComman" Width="713px"
                                                                            BackColor="#c3c3c3" />
                                                                        <cc1:AutoCompleteExtender ID="txtContactName_AutoCompleteExtender" runat="server"
                                                                            DelimiterCharacters="" Enabled="True" CompletionInterval="100" CompletionSetCount="1"
                                                                            MinimumPrefixLength="1" ServiceMethod="GetCompletionList" ServicePath="" TargetControlID="txtContactName"
                                                                            UseContextKey="True" CompletionListCssClass="completionList" CompletionListItemCssClass="listItem"
                                                                            CompletionListHighlightedItemCssClass="itemHighlighted">
                                                                        </cc1:AutoCompleteExtender>
                                                                    </td>
                                                                </tr>
                                                                <tr>
                                                                    <td align='<%= Common.ChangeTDForDefaultLeft()%>' width="150px">
                                                                        <asp:Label ID="lblLContactName" runat="server" CssClass="labelComman" Text="<%$ Resources:Attendance,Contact Name(Local) %>"></asp:Label>
                                                                    </td>
                                                                    <td>
                                                                        :
                                                                    </td>
                                                                    <td align='<%= Common.ChangeTDForDefaultLeft()%>' colspan="4">
                                                                        <asp:TextBox ID="txtLContactName" runat="server" CssClass="textComman" Width="713px"
                                                                            BackColor="#c3c3c3" />
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
                                                                                            ImageUrl="~/Images/edit.png" Visible="false" Width="16px" OnCommand="btnAddressEdit_Command" />
                                                                                    </ItemTemplate>
                                                                                    <ItemStyle CssClass="grid" HorizontalAlign="Center" />
                                                                                </asp:TemplateField>
                                                                                <asp:TemplateField HeaderText="<%$ Resources:Attendance,Delete %>">
                                                                                    <ItemTemplate>
                                                                                        <asp:ImageButton ID="imgBtnDelete" runat="server" CommandArgument='<%# Eval("Trans_Id") %>'
                                                                                            Height="16px" ImageUrl="~/Images/Erase.png" Visible="false" Width="16px" OnCommand="btnAddressDelete_Command" />
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
                                                                        <asp:Label ID="lblNickName" runat="server" CssClass="labelComman" Text="<%$ Resources:Attendance,Nick Name %>"></asp:Label>
                                                                    </td>
                                                                    <td>
                                                                        :
                                                                    </td>
                                                                    <td align='<%= Common.ChangeTDForDefaultLeft()%>' width="280px">
                                                                        <asp:TextBox ID="txtNickName" runat="server" CssClass="textComman"></asp:TextBox>
                                                                    </td>
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
                                                                        <asp:Label ID="lblDepartment" runat="server" CssClass="labelComman" Text="<%$ Resources:Attendance,Department %>"></asp:Label>
                                                                    </td>
                                                                    <td>
                                                                        :
                                                                    </td>
                                                                    <td align='<%= Common.ChangeTDForDefaultLeft()%>'>
                                                                        <asp:DropDownList ID="ddlDepartment" runat="server" Width="260px" CssClass="textComman" />
                                                                    </td>
                                                                    <td align='<%= Common.ChangeTDForDefaultLeft()%>'>
                                                                        <asp:Label ID="lblDesignation" runat="server" CssClass="labelComman" Text="<%$ Resources:Attendance,Designation %>" />
                                                                    </td>
                                                                    <td>
                                                                        :
                                                                    </td>
                                                                    <td align='<%= Common.ChangeTDForDefaultLeft()%>'>
                                                                        <asp:DropDownList ID="ddlDesignation" runat="server" Width="260px" CssClass="textComman" />
                                                                    </td>
                                                                </tr>
                                                                <tr>
                                                                    <td align='<%= Common.ChangeTDForDefaultLeft()%>'>
                                                                        <asp:Label ID="lblReligion" runat="server" CssClass="labelComman" Text="<%$ Resources:Attendance,Religion %>" />
                                                                    </td>
                                                                    <td>
                                                                        :
                                                                    </td>
                                                                    <td align='<%= Common.ChangeTDForDefaultLeft()%>'>
                                                                        <asp:DropDownList ID="ddlReligion" runat="server" CssClass="textComman" Width="260px" />
                                                                    </td>
                                                                    <td align='<%= Common.ChangeTDForDefaultLeft()%>'>
                                                                        <asp:Label ID="lblCivilId" runat="server" CssClass="labelComman" Text="<%$ Resources:Attendance,Civil Id %>" />
                                                                    </td>
                                                                    <td>
                                                                        :
                                                                    </td>
                                                                    <td align='<%= Common.ChangeTDForDefaultLeft()%>' width="280px">
                                                                        <asp:TextBox ID="txtCivilId" runat="server" CssClass="textComman" />
                                                                    </td>
                                                                </tr>
                                                                <tr>
                                                                    <td colspan="2">
                                                                    </td>
                                                                    <td align='<%= Common.ChangeTDForDefaultLeft()%>'>
                                                                        <br />
                                                                        <asp:CheckBox ID="chkIsEmail" CssClass="labelComman" runat="server" Text="<%$ Resources:Attendance,Send Email %>" />
                                                                        <asp:CheckBox ID="chkIsSMS" runat="server" CssClass="labelComman" Text="<%$ Resources:Attendance,Send SMS %>" />
                                                                    </td>
                                                                </tr>
                                                            </table>
                                                            <table width="100%">
                                                                <tr>
                                                                    <td align="center">
                                                                        <br />
                                                                        <table>
                                                                            <tr>
                                                                                <td width="90px">
                                                                                    <asp:Button ID="btnsave" runat="server" Text="<%$ Resources:Attendance,Save %>" CssClass="buttonCommman"
                                                                                        OnClick="btnsave_Click" Visible="false" />
                                                                                </td>
                                                                                <td width="90px">
                                                                                    <asp:Button ID="btnReset" runat="server" Text="<%$ Resources:Attendance,Reset %>"
                                                                                        CssClass="buttonCommman" OnClick="btnReset_Click" />
                                                                                </td>
                                                                                <td width="90px">
                                                                                    <asp:Button ID="btnCancel" runat="server" CausesValidation="False" CssClass="buttonCommman"
                                                                                        Text="<%$ Resources:Attendance,Cancel %>" OnClick="btnCancel_Click" />
                                                                                </td>
                                                                            </tr>
                                                                        </table>
                                                                    </td>
                                                                </tr>
                                                            </table>
                                                            &nbsp;
                                                            <asp:HiddenField ID="HiddenField1" runat="server" />
                                                            <asp:HiddenField ID="HiddenField2" runat="server" />
                                                        </asp:Panel>
                                                        <asp:Panel ID="PnlGroup" Visible="false" runat="server" DefaultButton="btnGSave">
                                                            <fieldset>
                                                                <legend>
                                                                    <asp:Label ID="lblAddGroup" runat="server" Font-Bold="True" CssClass="labelComman"
                                                                        Text="<%$ Resources:Attendance,Contact Add To Group %>"></asp:Label>
                                                                </legend>
                                                                <table width="100%">
                                                                    <tr>
                                                                        <td align="left">
                                                                            <table>
                                                                                <tr>
                                                                                    <td align='<%= Common.ChangeTDForDefaultLeft()%>' width="150px">
                                                                                        <asp:Label ID="lblConId" runat="server" CssClass="labelComman" Text="<%$ Resources:Attendance,Contact Id %>" />
                                                                                    </td>
                                                                                    <td>
                                                                                        :
                                                                                    </td>
                                                                                    <td align='<%= Common.ChangeTDForDefaultLeft()%>' width="250px">
                                                                                        <asp:Label ID="txtConId" runat="server" CssClass="labelComman" Font-Bold="true" />
                                                                                    </td>
                                                                                    <td align="<%= Common.ChangeTDForDefaultLeft()%>" width="150px">
                                                                                        <asp:Label ID="Label3" runat="server" CssClass="labelComman" Text="<%$ Resources:Attendance,Contact Name %>" />
                                                                                    </td>
                                                                                    <td>
                                                                                        :
                                                                                    </td>
                                                                                    <td align="<%= Common.ChangeTDForDefaultLeft()%>">
                                                                                        <asp:Label ID="txtgroupContactName" runat="server" CssClass="labelComman" Font-Bold="true" />
                                                                                    </td>
                                                                                </tr>
                                                                            </table>
                                                                        </td>
                                                                    </tr>
                                                                    <tr>
                                                                        <td align="left">
                                                                            <table width="100%">
                                                                                <tr>
                                                                                    <td valign="top" align="left" class="style1">
                                                                                        <asp:Panel ID="pnlTreeView" runat="server" Width="250px" Height="400px" ScrollBars="Auto"
                                                                                            HorizontalAlign="Left">
                                                                                            <asp:TreeView ID="navTree" runat="server" Height="100%" OnSelectedNodeChanged="navTree_SelectedNodeChanged1"
                                                                                                OnTreeNodeCheckChanged="navTree_TreeNodeCheckChanged" ShowCheckBoxes="All">
                                                                                            </asp:TreeView>
                                                                                        </asp:Panel>
                                                                                    </td>
                                                                                    <td align="left" valign="top">
                                                                                        <asp:Panel ID="pnlCustomerBrand" Visible="false" runat="server">
                                                                                            <table width="100%">
                                                                                                <tr>
                                                                                                    <td>
                                                                                                        <asp:Label ID="Label4" runat="server" CssClass="labelComman" Text="<%$ Resources:Attendance,Brand For Customer %>" />
                                                                                                    </td>
                                                                                                </tr>
                                                                                                <tr>
                                                                                                    <td>
                                                                                                        <asp:GridView ID="GvCustomerBrand" runat="server" AllowPaging="True" PageSize="10"
                                                                                                            AutoGenerateColumns="False" Width="100%" AllowSorting="true" OnSorting="GvCustomerBrand_Sorting"
                                                                                                            OnPageIndexChanging="GvCustomerBrand_PageIndexChanging" DataKeyNames="Brand_Id"
                                                                                                            CssClass="grid">
                                                                                                            <RowStyle CssClass="Invgridrow" />
                                                                                                            <Columns>
                                                                                                                <asp:TemplateField>
                                                                                                                    <HeaderTemplate>
                                                                                                                        <asp:CheckBox ID="chkCBActiveAll" runat="server" OnCheckedChanged="chkCBActiveAll_CheckedChanged"
                                                                                                                            AutoPostBack="true" />
                                                                                                                    </HeaderTemplate>
                                                                                                                    <ItemTemplate>
                                                                                                                        <asp:CheckBox ID="chkCBActive" runat="server" OnCheckedChanged="chkCBActive_CheckedChanged"
                                                                                                                            AutoPostBack="true" />
                                                                                                                    </ItemTemplate>
                                                                                                                    <ItemStyle CssClass="grid" HorizontalAlign="Center" />
                                                                                                                </asp:TemplateField>
                                                                                                                <asp:TemplateField HeaderText="<%$ Resources:Attendance,Brand Name %>" SortExpression="Brand_Name">
                                                                                                                    <ItemTemplate>
                                                                                                                        <asp:HiddenField ID="hdnCBBrandId" runat="server" Value='<%# Eval("Brand_Id") %>' />
                                                                                                                        <asp:Label ID="lblCBBrandName" runat="server" Text='<%# Eval("Brand_Name") %>' />
                                                                                                                    </ItemTemplate>
                                                                                                                    <ItemStyle CssClass="grid" />
                                                                                                                </asp:TemplateField>
                                                                                                            </Columns>
                                                                                                            <PagerStyle CssClass="Invgridheader" />
                                                                                                            <HeaderStyle CssClass="Invgridheader" />
                                                                                                            <AlternatingRowStyle CssClass="InvgridAltRow" />
                                                                                                        </asp:GridView>
                                                                                                    </td>
                                                                                                </tr>
                                                                                            </table>
                                                                                        </asp:Panel>
                                                                                    </td>
                                                                                    <td valign="top">
                                                                                        <asp:Panel ID="pnlSupplierBrand" Visible="false" runat="server">
                                                                                            <table width="100%">
                                                                                                <tr>
                                                                                                    <td>
                                                                                                        <asp:Label ID="Label7" runat="server" CssClass="labelComman" Text="<%$ Resources:Attendance,Brand For Supplier %>" />
                                                                                                    </td>
                                                                                                </tr>
                                                                                                <tr>
                                                                                                    <td>
                                                                                                        <asp:GridView ID="GvSupplierBrand" runat="server" AllowPaging="True" PageSize="10"
                                                                                                            AutoGenerateColumns="False" Width="100%" AllowSorting="true" OnSorting="GvSupplierBrand_Sorting"
                                                                                                            OnPageIndexChanging="GvSupplierBrand_PageIndexChanging" DataKeyNames="Brand_Id"
                                                                                                            CssClass="grid">
                                                                                                            <RowStyle CssClass="Invgridrow" />
                                                                                                            <Columns>
                                                                                                                <asp:TemplateField>
                                                                                                                    <HeaderTemplate>
                                                                                                                        <asp:CheckBox ID="chkSBActiveAll" runat="server" OnCheckedChanged="chkSBActiveAll_CheckedChanged"
                                                                                                                            AutoPostBack="true" />
                                                                                                                    </HeaderTemplate>
                                                                                                                    <ItemTemplate>
                                                                                                                        <asp:CheckBox ID="chkSBActive" runat="server" OnCheckedChanged="chkSBActive_CheckedChanged"
                                                                                                                            AutoPostBack="true" />
                                                                                                                    </ItemTemplate>
                                                                                                                    <ItemStyle CssClass="grid" HorizontalAlign="Center" />
                                                                                                                </asp:TemplateField>
                                                                                                                <asp:TemplateField HeaderText="<%$ Resources:Attendance,Brand Name %>" SortExpression="Brand_Name">
                                                                                                                    <ItemTemplate>
                                                                                                                        <asp:HiddenField ID="hdnSBBrandId" runat="server" Value='<%# Eval("Brand_Id") %>' />
                                                                                                                        <asp:Label ID="lblSBBrandName" runat="server" Text='<%# Eval("Brand_Name") %>' />
                                                                                                                    </ItemTemplate>
                                                                                                                    <ItemStyle CssClass="grid" />
                                                                                                                </asp:TemplateField>
                                                                                                            </Columns>
                                                                                                            <PagerStyle CssClass="Invgridheader" />
                                                                                                            <HeaderStyle CssClass="Invgridheader" />
                                                                                                            <AlternatingRowStyle CssClass="InvgridAltRow" />
                                                                                                        </asp:GridView>
                                                                                                    </td>
                                                                                                </tr>
                                                                                            </table>
                                                                                        </asp:Panel>
                                                                                    </td>
                                                                                </tr>
                                                                            </table>
                                                                        </td>
                                                                    </tr>
                                                                    <tr>
                                                                        <td align="center">
                                                                            <table>
                                                                                <tr>
                                                                                    <td width="100px">
                                                                                        <asp:Panel ID="Panel4" runat="server" DefaultButton="btnGSave">
                                                                                            <asp:Button ID="btnGSave" runat="server" CssClass="buttonCommman" OnClick="btnGSave_Click"
                                                                                                Text="<%$ Resources:Attendance,Save %>" Visible="false" />
                                                                                        </asp:Panel>
                                                                                    </td>
                                                                                    <td>
                                                                                        <asp:Panel ID="Panel5" runat="server" DefaultButton="btnGCancel">
                                                                                            <asp:Button ID="btnGCancel" runat="server" CausesValidation="False" CssClass="buttonCommman"
                                                                                                OnClick="btnGCancel_Click" Text="<%$ Resources:Attendance,Cancel %>" />
                                                                                        </asp:Panel>
                                                                                    </td>
                                                                                </tr>
                                                                            </table>
                                                                        </td>
                                                                    </tr>
                                                                </table>
                                                            </fieldset>
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
                                                                                                                ImageUrl="~/Images/edit.png" Width="16px" OnCommand="imgBtnCompanyAddressEdit_Command" />
                                                                                                        </ItemTemplate>
                                                                                                        <ItemStyle CssClass="grid" HorizontalAlign="Center" />
                                                                                                    </asp:TemplateField>
                                                                                                    <asp:TemplateField HeaderText="<%$ Resources:Attendance,Delete %>">
                                                                                                        <ItemTemplate>
                                                                                                            <asp:ImageButton ID="imgBtnCompanyAddressDelete" runat="server" CommandArgument='<%# Eval("Trans_Id") %>'
                                                                                                                Height="16px" ImageUrl="~/Images/Erase.png" Width="16px" OnCommand="imgBtnCompanyAddressDelete_Command" />
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
                                                    </td>
                                                </tr>
                                            </table>
                                        </asp:Panel>
                                    </asp:Panel>
                                    <asp:Panel ID="PnlBin" runat="server">
                                        <asp:Panel ID="PNL3" runat="server" DefaultButton="btnbindBin">
                                            <div width="100%" style="background-image: url(../Images/bg_repeat.jpg); background-repeat: repeat;">
                                                <table width="100%" style="padding-left: 20px; height: 38px">
                                                    <tr>
                                                        <td width="90px">
                                                            <asp:Label ID="Label2" runat="server" Text="<%$ Resources:Attendance,Select Field %>"
                                                                CssClass="labelComman"></asp:Label>
                                                        </td>
                                                        <td width="180px">
                                                            <asp:DropDownList ID="ddlFieldNameBin" runat="server" Width="170px" Height="25px"
                                                                CssClass="DropdownSearch">
                                                                <asp:ListItem Text="<%$ Resources:Attendance,Company Name %>" Value="Company_Name"></asp:ListItem>
                                                                <asp:ListItem Selected="True" Text="<%$ Resources:Attendance,Contact Name %>" Value="Contact_Name"></asp:ListItem>
                                                                <asp:ListItem Text="<%$ Resources:Attendance,Department %>" Value="Dep_Name"></asp:ListItem>
                                                                <asp:ListItem Text="<%$ Resources:Attendance,Designation %>" Value="Designation"></asp:ListItem>
                                                            </asp:DropDownList>
                                                        </td>
                                                        <td width="135px">
                                                            <asp:DropDownList ID="ddlOptionBin" runat="server" Height="25px" Width="120px" CssClass="DropdownSearch">
                                                                <asp:ListItem Text="<%$ Resources:Attendance,--Select-- %>"></asp:ListItem>
                                                                <asp:ListItem Text="<%$ Resources:Attendance,Equal %>"></asp:ListItem>
                                                                <asp:ListItem Selected="True" Text="<%$ Resources:Attendance,Contains %>"></asp:ListItem>
                                                                <asp:ListItem Text="<%$ Resources:Attendance,Like %>"></asp:ListItem>
                                                            </asp:DropDownList>
                                                        </td>
                                                        <td width="24%">
                                                            <asp:TextBox ID="txtValueBin" runat="server" CssClass="textCommanSearch" Width="100%"></asp:TextBox>
                                                        </td>
                                                        <td width="50px" align="center">
                                                            <asp:ImageButton ID="btnbindBin" runat="server" CausesValidation="False" Height="25px"
                                                                ImageUrl="~/Images/search.png" OnClick="btnbindBin_Click" Width="25px" ToolTip="<%$ Resources:Attendance,Search %>">
                                                            </asp:ImageButton>
                                                        </td>
                                                        <td>
                                                            <asp:Panel ID="Panel1" runat="server" DefaultButton="btnRefreshBin">
                                                                <asp:ImageButton ID="btnRefreshBin" runat="server" CausesValidation="False" Height="25px"
                                                                    ImageUrl="~/Images/refresh.png" OnClick="btnRefreshBin_Click" ToolTip="<%$ Resources:Attendance,Refresh %>"
                                                                    Width="25px" />
                                                            </asp:Panel>
                                                        </td>
                                                        <td width="30px" align="center">
                                                            <asp:Panel ID="pnlimgBtnRestore" runat="server" DefaultButton="imgBtnRestore">
                                                                <asp:ImageButton ID="imgBtnRestore" Height="25px" Width="25px" CausesValidation="False"
                                                                    Visible="false" runat="server" ImageUrl="~/Images/active.png" OnClick="imgBtnRestore_Click"
                                                                    ToolTip="<%$ Resources:Attendance, Active %>" />
                                                            </asp:Panel>
                                                        </td>
                                                        <td>
                                                            <asp:Panel ID="pnlImgbtnSelectAll" runat="server" DefaultButton="ImgbtnSelectAll">
                                                                <asp:ImageButton ID="ImgbtnSelectAll" runat="server" OnClick="ImgbtnSelectAll_Click"
                                                                    Visible="false" ToolTip="<%$ Resources:Attendance, Select All %>" AutoPostBack="true"
                                                                    ImageUrl="~/Images/selectAll.png" />
                                                            </asp:Panel>
                                                        </td>
                                                        <td align="center">
                                                            <asp:Label ID="lblTotalRecordsBin" runat="server" CssClass="labelComman" Text="<%$ Resources:Attendance,Total Records : 0 %>"></asp:Label>
                                                            <asp:Label ID="lblSelectedRecord" runat="server" Visible="false"></asp:Label>
                                                        </td>
                                                    </tr>
                                                </table>
                                            </div>
                                        </asp:Panel>
                                        <asp:HiddenField ID="HdnSortBin" runat="server" />
                                        <asp:GridView ID="GridContactBin" runat="server" AllowPaging="True" PageSize="10"
                                            AutoGenerateColumns="False" Width="100%" AllowSorting="true" OnSorting="GridContactBin_Sorting"
                                            OnPageIndexChanging="GridContactBin_PageIndexChanging" DataKeyNames="Contact_Id"
                                            CssClass="grid">
                                            <RowStyle CssClass="Invgridrow" />
                                            <Columns>
                                                <asp:TemplateField>
                                                    <HeaderTemplate>
                                                        <asp:CheckBox ID="chkActiveAll" runat="server" OnCheckedChanged="chkActiveAll_CheckedChanged"
                                                            AutoPostBack="true" />
                                                    </HeaderTemplate>
                                                    <ItemTemplate>
                                                        <asp:CheckBox ID="chkActive" runat="server" OnCheckedChanged="chkActive_CheckedChanged"
                                                            AutoPostBack="true" />
                                                    </ItemTemplate>
                                                    <ItemStyle CssClass="grid" HorizontalAlign="Center" />
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="<%$ Resources:Attendance,Contact Code %>" SortExpression="Contact_Code">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblgvBContactCode" runat="server" Text='<%#Eval("Contact_Code") %>' />
                                                    </ItemTemplate>
                                                    <ItemStyle CssClass="grid" HorizontalAlign="Center" />
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="<%$ Resources:Attendance,Contact Name %>" SortExpression="Contact_Name">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblBgvName" runat="server" Text='<%# Eval("Contact_Name") %>'></asp:Label>
                                                        <asp:Label ID="Label5" runat="server" Visible="false" Text='<%# Eval("Contact_Id") %>'></asp:Label>
                                                    </ItemTemplate>
                                                    <ItemStyle CssClass="grid" HorizontalAlign="Center" />
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="<%$ Resources:Attendance,Contact Name(Local) %>" SortExpression="Contact_Name_L">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblBgvCNameL" runat="server" Text='<%#Eval("Contact_Name_L") %>' />
                                                    </ItemTemplate>
                                                    <ItemStyle CssClass="grid" HorizontalAlign="Center" />
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="<%$ Resources:Attendance,Company Name %>" SortExpression="Company_Name">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblBgvCName" runat="server" Text='<%#Eval("Company_Name") %>'></asp:Label>
                                                    </ItemTemplate>
                                                    <ItemStyle CssClass="grid" HorizontalAlign="Center" />
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="<%$ Resources:Attendance,Department %>" SortExpression="Dep_Name">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblgvBDepartment" runat="server" Text='<%#Eval("Dep_Name") %>'></asp:Label>
                                                    </ItemTemplate>
                                                    <ItemStyle CssClass="grid" HorizontalAlign="Center" />
                                                </asp:TemplateField>
                                            </Columns>
                                            <PagerStyle CssClass="Invgridheader" />
                                            <HeaderStyle CssClass="Invgridheader" />
                                            <AlternatingRowStyle CssClass="InvgridAltRow" />
                                        </asp:GridView>
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
                                                            <asp:Label ID="Label1" runat="server" CssClass="labelComman" Text="<%$ Resources:Attendance,Address Name %>" />
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
                                                                            CssClass="buttonCommman" OnClick="btnAddressSave_Click" />
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
</asp:Content>
