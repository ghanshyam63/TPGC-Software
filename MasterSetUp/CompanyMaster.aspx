<%@ Page Language="C#" MasterPageFile="~/ERPMaster.master" AutoEventWireup="true"
    CodeFile="CompanyMaster.aspx.cs" Inherits="MasterSetUp_CompanyMaster" Title="Company Master" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <link href="../CSS/InvStyle.css" rel="stylesheet" type="text/css" />
    <asp:UpdatePanel ID="UpdatePanel1" runat="server">
        <Triggers>
            <asp:PostBackTrigger ControlID="btnList" />
            <asp:PostBackTrigger ControlID="btnNew" />
            <asp:PostBackTrigger ControlID="btnBin" />
            <asp:PostBackTrigger ControlID="gvCompanyMaster" />
        </Triggers>
        <ContentTemplate>
            <table width="100%">
                <tr>
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
                                                <asp:Label ID="lblHeader" runat="server" Text="College Setup"
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
                                                                <asp:ListItem Selected="True" Text="College Name" Value="Company_Name"></asp:ListItem>
                                                                <asp:ListItem Text="College Name(Local)" Value="Company_Name_L"></asp:ListItem>
                                                                <asp:ListItem Text="Parent College Name" Value="ParentCompany"></asp:ListItem>
                                                                <asp:ListItem Text="College Code" Value="Company_Code"></asp:ListItem>
                                                                <asp:ListItem Text="College Id " Value="Company_Id"></asp:ListItem>
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
                                                        <td width="24%">
                                                            <asp:TextBox ID="txtValue" runat="server" CssClass="textCommanSearch" Height="14px"
                                                                Width="100%"></asp:TextBox>
                                                        </td>
                                                        <td width="50px" align="center">
                                                            <asp:ImageButton ID="btnbind" runat="server" CausesValidation="False" Height="25px"
                                                                ImageUrl="~/Images/search.png" OnClick="btnbind_Click" Width="25px" ToolTip="<%$ Resources:Attendance,Search %>">
                                                            </asp:ImageButton>
                                                        </td>
                                                        <td>
                                                            <asp:Panel ID="PnlRefresh" runat="server" DefaultButton="btnRefresh">
                                                                <asp:ImageButton ID="btnRefresh" runat="server" CausesValidation="False" Height="25px"
                                                                    ImageUrl="~/Images/refresh.png" OnClick="btnRefresh_Click" Width="25px" ToolTip="<%$ Resources:Attendance,Refresh %>">
                                                                </asp:ImageButton>
                                                            </asp:Panel>
                                                        </td>
                                                        <td align="center">
                                                            <asp:Label ID="lblTotalRecords" runat="server" Text="<%$ Resources:Attendance,Total Records: 0 %>"
                                                                CssClass="labelComman"></asp:Label>
                                                            <asp:Label ID="lblSelectedRecord" runat="server" Visible="false"></asp:Label>
                                                        </td>
                                                    </tr>
                                                </table>
                                            </div>
                                            <asp:HiddenField ID="HDFSort" runat="server" />
                                        </asp:Panel>
                                        <asp:GridView ID="gvCompanyMaster" PageSize="<%# SystemParameter.GetPageSize() %>"
                                            runat="server" AutoGenerateColumns="False" Width="100%" AllowPaging="True" AllowSorting="True"
                                            CssClass="grid" OnPageIndexChanging="gvCompanyMaster_PageIndexChanging" OnSorting="gvCompanyMaster_OnSorting">
                                            <Columns>
                                                <asp:TemplateField HeaderText="<%$ Resources:Attendance,Edit %>">
                                                    <ItemTemplate>
                                                        <asp:ImageButton ID="btnEdit" runat="server" CommandArgument='<%# Eval("Company_Id") %>'
                                                            ImageUrl="~/Images/edit.png" CausesValidation="False" OnCommand="btnEdit_Command"
                                                            Visible="false" ToolTip="<%$ Resources:Attendance,Edit %>" /></ItemTemplate>
                                                    <ItemStyle HorizontalAlign="Center" />
                                                    <ItemStyle CssClass="grid" />
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="<%$ Resources:Attendance,Delete %>">
                                                    <ItemTemplate>
                                                        <asp:ImageButton ID="IbtnDelete" runat="server" CausesValidation="False" CommandArgument='<%# Eval("Company_Id") %>'
                                                            ImageUrl="~/Images/Erase.png" OnCommand="IbtnDelete_Command" Visible="false"
                                                            ToolTip="<%$ Resources:Attendance,Delete %>" />
                                                    </ItemTemplate>
                                                    <ItemStyle HorizontalAlign="Center" />
                                                    <ItemStyle CssClass="grid" />
                                                </asp:TemplateField>
                                                 <asp:TemplateField HeaderText="<%$ Resources:Attendance,Parameter %>">
                                                    <ItemTemplate>
                                                        <asp:ImageButton ID="IbtnParam" runat="server" CausesValidation="False" CommandArgument='<%# Eval("Company_Id") %>'
                                                            ImageUrl="~/Images/parameter.png" OnCommand="IbtnParam_Command" Visible="true"
                                                            ToolTip="<%$ Resources:Attendance,Parameter %>" />
                                                    </ItemTemplate>
                                                    <ItemStyle HorizontalAlign="Center" />
                                                    <ItemStyle CssClass="grid" />
                                                </asp:TemplateField>
                                                
                                                
                                                
                                                <asp:TemplateField HeaderText="College Id " SortExpression="Company_Id">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblCompanyId1" runat="server" Text='<%# Eval("Company_Id") %>'></asp:Label>
                                                    </ItemTemplate>
                                                    <ItemStyle CssClass="grid" />
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="College Name" SortExpression="Company_Name">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lbCompanyName" runat="server" Text='<%# Eval("Company_Name") %>'></asp:Label>
                                                    </ItemTemplate>
                                                    <ItemStyle CssClass="grid" />
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="College Name(Local)" SortExpression="Company_Name_L">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblCompanyNameL" runat="server" Text='<%# Eval("Company_Name_L") %>'></asp:Label>
                                                    </ItemTemplate>
                                                    <ItemStyle CssClass="grid" />
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="Parent College Name" SortExpression="ParentCompany">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblCompanyParentName" runat="server" Text='<%# Eval("ParentCompany") %>'></asp:Label>
                                                    </ItemTemplate>
                                                    <ItemStyle CssClass="grid" />
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="College Code" SortExpression="Company_Code">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblCompanycode" runat="server" Text='<%# Eval("Company_Code") %>'></asp:Label>
                                                    </ItemTemplate>
                                                    <ItemStyle CssClass="grid" />
                                                </asp:TemplateField>
                                            </Columns>
                                            <AlternatingRowStyle CssClass="InvgridAltRow"></AlternatingRowStyle>
                                            <HeaderStyle CssClass="Invgridheader" />
                                            <PagerStyle CssClass="Invgridheader" />
                                            <RowStyle CssClass="Invgridrow" HorizontalAlign="Center"></RowStyle>
                                        </asp:GridView>
                                    </asp:Panel>
                                    <asp:Panel ID="PnlNewEdit" runat="server" DefaultButton="btnSave">
                                        <table width="100%" style="padding-left: 43px">
                                            <tr>
                                                <td align='<%= Common.ChangeTDForDefaultLeft()%>'>
                                                    <asp:Label ID="lblCompanyCode" runat="server" CssClass="labelComman" Text="<%$ Resources:Attendance,Company Code %>"></asp:Label>
                                                </td>
                                                <td width="1px">
                                                    :
                                                </td>
                                                <td align='<%= Common.ChangeTDForDefaultLeft()%>'>
                                                    <asp:TextBox ID="txtCompanyCode" TabIndex="101" Width="250px" runat="server" CssClass="textComman"
                                                        BackColor="#e3e3e3" />
                                                    <cc1:AutoCompleteExtender ID="txtCompCode_AutoCompleteExtender" runat="server" DelimiterCharacters=""
                                                        Enabled="True" ServiceMethod="GetCompletionListCompanyCode" ServicePath="" CompletionInterval="100"
                                                        MinimumPrefixLength="1" CompletionSetCount="1" TargetControlID="txtCompanyCode"
                                                        UseContextKey="True" CompletionListCssClass="completionList" CompletionListItemCssClass="listItem"
                                                        CompletionListHighlightedItemCssClass="itemHighlighted">
                                                    </cc1:AutoCompleteExtender>
                                                </td>
                                                <td>
                                                </td>
                                                <td rowspan="4" colspan="3">
                                                    <table width="100%">
                                                        <tr>
                                                            <td width="160px" valign="top" align='<%= Common.ChangeTDForDefaultLeft()%>'>
                                                                <asp:Label ID="lblCompanyLogo" runat="server" CssClass="labelComman" Text="<%$ Resources:Attendance,Company Logo %>"></asp:Label>
                                                            </td>
                                                            <td width="1px" valign="top">
                                                                :
                                                            </td>
                                                            <td align='<%= Common.ChangeTDForDefaultLeft()%>'>
                                                                <cc1:AsyncFileUpload ID="FULogoPath" TabIndex="102" Width="250px" runat="server"
                                                                    OnUploadedComplete="FULogoPath_UploadedComplete" FailedValidation="False" />
                                                            </td>
                                                        </tr>
                                                        <tr>
                                                            <td colspan="2">
                                                            </td>
                                                            <td align='<%= Common.ChangeTDForDefaultLeft()%>'>
                                                                <asp:Image ID="imgLogo" runat="server" Width="90px" Height="90px" />
                                                                <br />
                                                                &nbsp;&nbsp;
                                                                <asp:Button ID="btnUpload" TabIndex="103" runat="server" Text="<%$ Resources:Attendance,Load %>"
                                                                    CssClass="buttonCommman" Height="15px" OnClick="btnUpload_Click" />
                                                            </td>
                                                        </tr>
                                                    </table>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td width="160px" align='<%= Common.ChangeTDForDefaultLeft()%>'>
                                                    <asp:Label ID="lblCompanyName" runat="server" CssClass="labelComman" Text="<%$ Resources:Attendance,Company Name %>"></asp:Label>
                                                </td>
                                                <td width="1px">
                                                    :
                                                </td>
                                                <td align='<%= Common.ChangeTDForDefaultLeft()%>'>
                                                    <asp:TextBox ID="txtCompanyName" TabIndex="104" AutoPostBack="true" OnTextChanged="txtCompanyName_OnTextChanged"  BackColor="#e3e3e3" Width="250px"
                                                        runat="server" CssClass="textComman" />
                                                    <cc1:AutoCompleteExtender ID="AutoCompleteExtender1" runat="server" DelimiterCharacters=""
                                                        Enabled="True" ServiceMethod="GetCompletionListCompanyName" ServicePath="" CompletionInterval="100"
                                                        MinimumPrefixLength="1" CompletionSetCount="1" TargetControlID="txtCompanyName"
                                                        UseContextKey="True" CompletionListCssClass="completionList" CompletionListItemCssClass="listItem"
                                                        CompletionListHighlightedItemCssClass="itemHighlighted">
                                                    </cc1:AutoCompleteExtender>
                                                </td>
                                                <td colspan="3">
                                                </td>
                                            </tr>
                                            <tr>
                                                <td width="160px" align='<%= Common.ChangeTDForDefaultLeft()%>'>
                                                    <asp:Label ID="lblCompanyNameL" runat="server" CssClass="labelComman" Text="<%$ Resources:Attendance,Company Name(Local) %>"></asp:Label>
                                                </td>
                                                <td width="1px">
                                                    :
                                                </td>
                                                <td align='<%= Common.ChangeTDForDefaultLeft()%>'>
                                                    <asp:TextBox ID="txtCompanyNameL" TabIndex="105" Width="250px" runat="server" CssClass="textComman" />
                                                </td>
                                                <td colspan="3">
                                                </td>
                                            </tr>
                                            <tr>
                                                <td align='<%= Common.ChangeTDForDefaultLeft()%>' width="140px">
                                                    <asp:Label ID="lblParentCompany" runat="server" CssClass="labelComman" Text="<%$ Resources:Attendance,Parent Company Name %>" />
                                                </td>
                                                <td width="1px">
                                                    :
                                                </td>
                                                <td align='<%= Common.ChangeTDForDefaultLeft()%>'>
                                                    <asp:DropDownList ID="ddlParentCompany" runat="server" CssClass="textComman" TabIndex="106"
                                                        Width="262px" />
                                                </td>
                                                <tr>
                                                    <td align="<%= Common.ChangeTDForDefaultLeft()%>">
                                                        <asp:Label ID="lblAddressName" runat="server" CssClass="labelComman" Text="<%$ Resources:Attendance,Address Name %>" />
                                                    </td>
                                                    <td align="center">
                                                        :
                                                    </td>
                                                    <td align="<%= Common.ChangeTDForDefaultLeft()%>" colspan="4">
                                                        <asp:Panel ID="pnlAddress" runat="server" DefaultButton="imgAddAddressName">
                                                            <table>
                                                                <tr>
                                                                    <td>
                                                                        <asp:TextBox ID="txtAddressName" runat="server" AutoPostBack="true" BackColor="#e3e3e3"
                                                                            CssClass="textComman" OnTextChanged="txtAddressName_TextChanged" TabIndex="107" />
                                                                        <cc1:AutoCompleteExtender ID="AutoCompleteExtender2" runat="server" CompletionInterval="100"
                                                                            CompletionListCssClass="completionList" CompletionListHighlightedItemCssClass="itemHighlighted"
                                                                            CompletionListItemCssClass="listItem" CompletionSetCount="1" DelimiterCharacters=""
                                                                            Enabled="True" MinimumPrefixLength="1" ServiceMethod="GetCompletionListAddressName"
                                                                            ServicePath="" TargetControlID="txtAddressName" UseContextKey="True">
                                                                        </cc1:AutoCompleteExtender>
                                                                    </td>
                                                                    <td valign="bottom">
                                                                        <asp:Button ID="imgAddAddressName" runat="server" Text="<%$ Resources:Attendance,Add %>"
                                                                            BackColor="Transparent" BorderStyle="None" Font-Bold="true" CssClass="btnNewAdress"
                                                                            Font-Size="13px" Font-Names=" Arial" Width="92px" Height="32px" OnClick="imgAddAddressName_Click" />
                                                                    </td>
                                                                    <td valign="bottom">
                                                                        <asp:Button ID="btnAddNewAddress" runat="server" Text="<%$ Resources:Attendance,New %>"
                                                                            BackColor="Transparent" BorderStyle="None" Font-Bold="true" CssClass="btnAddAdress"
                                                                            Font-Size="13px" Font-Names=" Arial" Width="92px" Height="32px" OnClick="btnAddNewAddress_Click" />
                                                                    </td>
                                                                </tr>
                                                            </table>
                                                        </asp:Panel>
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td align="center" colspan="6">
                                                        <asp:GridView ID="GvAddressName" runat="server" AutoGenerateColumns="False" CssClass="grid"
                                                            TabIndex="110" Width="60%">
                                                            <AlternatingRowStyle CssClass="InvgridAltRow" />
                                                            <Columns>
                                                                <asp:TemplateField HeaderText="Edit">
                                                                    <ItemTemplate>
                                                                        <asp:ImageButton ID="imgBtnAddressEdit" runat="server" CommandArgument='<%# Eval("Trans_Id") %>'
                                                                            ImageUrl="~/Images/edit.png" OnCommand="btnAddressEdit_Command" Width="16px" />
                                                                    </ItemTemplate>
                                                                    <ItemStyle CssClass="grid" HorizontalAlign="Center" />
                                                                </asp:TemplateField>
                                                                <asp:TemplateField HeaderText="Delete">
                                                                    <ItemTemplate>
                                                                        <asp:ImageButton ID="imgBtnDelete" runat="server" CommandArgument='<%# Eval("Trans_Id") %>'
                                                                            Height="16px" ImageUrl="~/Images/Erase.png" OnCommand="btnAddressDelete_Command"
                                                                            Width="16px" />
                                                                        <%--<cc1:ConfirmButtonExtender ID="ConfirmButtonExtender1" runat="server" ConfirmText="<%$ Resources:Attendance,Are You Sure You Want to Delete? %>"
                                                                        TargetControlID="imgBtnDelete">
                                                                    </cc1:ConfirmButtonExtender>--%>
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
                                                    <td align="<%= Common.ChangeTDForDefaultLeft()%>">
                                                        <asp:Label ID="lblCountry" runat="server" CssClass="labelComman" Text="<%$ Resources:Attendance,Country Name %>"></asp:Label>
                                                    </td>
                                                    <td width="1px">
                                                        :
                                                    </td>
                                                    <td align="<%= Common.ChangeTDForDefaultLeft()%>">
                                                        <asp:DropDownList ID="ddlCountry1" runat="server" CssClass="textComman" TabIndex="111"
                                                            Width="262px" />
                                                    </td>
                                                    <td>
                                                    </td>
                                                    <td colspan="3">
                                                        <table>
                                                            <tr>
                                                                <td align="<%= Common.ChangeTDForDefaultLeft()%>" width="140px">
                                                                    <asp:Label ID="lblCurrency" runat="server" CssClass="labelComman" Text="<%$ Resources:Attendance,Currency Name %>"></asp:Label>
                                                                </td>
                                                                <td width="1px">
                                                                    :
                                                                </td>
                                                                <td align="<%= Common.ChangeTDForDefaultLeft()%>">
                                                                    <asp:DropDownList ID="ddlCurrency" runat="server" CssClass="textComman" TabIndex="112"
                                                                        Width="262px" />
                                                                </td>
                                                                <td>
                                                                </td>
                                                            </tr>
                                                        </table>
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td align="<%= Common.ChangeTDForDefaultLeft()%>">
                                                        <asp:Label ID="lblManager" runat="server" CssClass="labelComman" Text="<%$ Resources:Attendance,Manager %>"></asp:Label>
                                                    </td>
                                                    <td width="1px">
                                                        :
                                                    </td>
                                                    <td align="<%= Common.ChangeTDForDefaultLeft()%>">
                                                        <asp:TextBox ID="txtManagerName" runat="server" BackColor="#e3e3e3" CssClass="textComman"
                                                            TabIndex="113" Width="250px" />
                                                        <cc1:AutoCompleteExtender ID="AutoCompleteExtender5" runat="server" CompletionInterval="100"
                                                            CompletionListCssClass="completionList" CompletionListHighlightedItemCssClass="itemHighlighted"
                                                            CompletionListItemCssClass="listItem" CompletionSetCount="1" DelimiterCharacters=""
                                                            Enabled="True" MinimumPrefixLength="1" ServiceMethod="GetCompletionListEmployeeName"
                                                            ServicePath="" TargetControlID="txtManagerName" UseContextKey="True">
                                                        </cc1:AutoCompleteExtender>
                                                    </td>
                                                    <td>
                                                    </td>
                                                    <td colspan="3">
                                                        <table>
                                                            <tr>
                                                                <td align="<%= Common.ChangeTDForDefaultLeft()%>" width="140px">
                                                                    <asp:Label ID="lblLicenseNo" runat="server" CssClass="labelComman" Text="<%$ Resources:Attendance,License No. %>"></asp:Label>
                                                                </td>
                                                                <td width="1px">
                                                                    :
                                                                </td>
                                                                <td align="<%= Common.ChangeTDForDefaultLeft()%>">
                                                                    <asp:TextBox ID="txtLicenceNo" runat="server" CssClass="textComman" TabIndex="114"
                                                                        Width="250px" />
                                                                </td>
                                                            </tr>
                                                        </table>
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td align="center" colspan="6">
                                                        <table style="padding-left: 8px">
                                                            <tr>
                                                                <td width="80px">
                                                                    <asp:Button ID="btnSave" runat="server" CssClass="buttonCommman" OnClick="btnSave_Click"
                                                                        TabIndex="115" Text="<%$ Resources:Attendance,Save %>" ValidationGroup="a" Visible="false" />
                                                                </td>
                                                                <td width="80px">
                                                                    <asp:Button ID="btnReset" runat="server" CausesValidation="False" CssClass="buttonCommman"
                                                                        OnClick="btnReset_Click" TabIndex="116" Text="<%$ Resources:Attendance,Reset %>" />
                                                                </td>
                                                                <td width="80px">
                                                                    <asp:Button ID="btnCancel" runat="server" CausesValidation="False" CssClass="buttonCommman"
                                                                        OnClick="btnCancel_Click" TabIndex="117" Text="<%$ Resources:Attendance,Cancel %>" />
                                                                </td>
                                                            </tr>
                                                        </table>
                                                        <asp:HiddenField ID="editid" runat="server" />
                                                    </td>
                                                </tr>
                                            </tr>
                                        </table>
                                    </asp:Panel>
                                    <asp:Panel ID="PnlBin" runat="server">
                                        <asp:Panel ID="pnlbinsearch" runat="server" DefaultButton="btnbinbind">
                                            <div width="100%" style="background-image: url(../Images/bg_repeat.jpg); background-repeat: repeat;">
                                                <table width="100%" style="padding-left: 20px; height: 38px">
                                                    <tr>
                                                        <td width="90px">
                                                            <asp:Label ID="lblbinSelectField" runat="server" Text="<%$ Resources:Attendance,Select Field %>"
                                                                CssClass="labelComman"></asp:Label>
                                                        </td>
                                                        <td width="180px">
                                                            <asp:DropDownList ID="ddlbinFieldName" runat="server" CssClass="DropdownSearch" Height="25px"
                                                                Width="170px">
                                                                <asp:ListItem Selected="True" Text="<%$ Resources:Attendance,Company Name %>" Value="Company_Name"></asp:ListItem>
                                                                <asp:ListItem Text="<%$ Resources:Attendance,Company Name(Local) %>" Value="Company_Name_L"></asp:ListItem>
                                                                <asp:ListItem Text="<%$ Resources:Attendance,Parent Company Name %>" Value="ParentCompany"></asp:ListItem>
                                                                <asp:ListItem Text="<%$ Resources:Attendance,Company Code %>" Value="Company_Code"></asp:ListItem>
                                                                <asp:ListItem Text="<%$ Resources:Attendance,Company Id %>" Value="Company_Id"></asp:ListItem>
                                                            </asp:DropDownList>
                                                        </td>
                                                        <td width="135px">
                                                            <asp:DropDownList ID="ddlbinOption" runat="server" CssClass="DropdownSearch" Height="25px"
                                                                Width="120px">
                                                                <asp:ListItem Text="<%$ Resources:Attendance,--Select-- %>"></asp:ListItem>
                                                                <asp:ListItem Text="<%$ Resources:Attendance,Equal %>"></asp:ListItem>
                                                                <asp:ListItem Selected="True" Text="<%$ Resources:Attendance,Contains %>"></asp:ListItem>
                                                                <asp:ListItem Text="<%$ Resources:Attendance,Like %>"></asp:ListItem>
                                                            </asp:DropDownList>
                                                        </td>
                                                        <td width="24%">
                                                            <asp:TextBox ID="txtbinValue" runat="server" CssClass="textCommanSearch" Height="14px"
                                                                Width="100%"></asp:TextBox>
                                                        </td>
                                                        <td width="50px" align="center">
                                                            <asp:ImageButton ID="btnbinbind" runat="server" CausesValidation="False" Height="25px"
                                                                ImageUrl="~/Images/search.png" OnClick="btnbinbind_Click" Width="25px" ToolTip="<%$ Resources:Attendance,Search %>">
                                                            </asp:ImageButton>
                                                        </td>
                                                        <td>
                                                            <asp:Panel ID="Panel2" runat="server" DefaultButton="btnbinRefresh">
                                                                <asp:ImageButton ID="btnbinRefresh" runat="server" CausesValidation="False" Height="25px"
                                                                    ImageUrl="~/Images/refresh.png" OnClick="btnbinRefresh_Click" Width="25px" ToolTip="<%$ Resources:Attendance,Refresh %>">
                                                                </asp:ImageButton>
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
                                                            <asp:Label ID="lblbinTotalRecords" runat="server" Text="<%$ Resources:Attendance,Total Records: 0 %>"
                                                                CssClass="labelComman"></asp:Label>
                                                        </td>
                                                    </tr>
                                                </table>
                                            </div>
                                        </asp:Panel>
                                        <asp:GridView ID="gvCompanyMasterBin" PageSize="<%# SystemParameter.GetPageSize() %>"
                                            runat="server" AutoGenerateColumns="False" Width="100%" AllowPaging="True" OnPageIndexChanging="gvCompanyMasterBin_PageIndexChanging"
                                            OnSorting="gvCompanyMasterBin_OnSorting" AllowSorting="true" CssClass="grid">
                                            <Columns>
                                                <asp:TemplateField>
                                                    <ItemTemplate>
                                                        <asp:CheckBox ID="chkgvSelect" runat="server" OnCheckedChanged="chkgvSelect_CheckedChanged"
                                                            AutoPostBack="true" />
                                                    </ItemTemplate>
                                                    <HeaderTemplate>
                                                        <asp:CheckBox ID="chkgvSelectAll" runat="server" OnCheckedChanged="chkgvSelectAll_CheckedChanged"
                                                            AutoPostBack="true" />
                                                    </HeaderTemplate>
                                                    <ItemStyle HorizontalAlign="Center" />
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="<%$ Resources:Attendance,Company Id %>" SortExpression="Company_Id">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblCompanyId1" runat="server" Text='<%# Eval("Company_Id") %>'></asp:Label>
                                                    </ItemTemplate>
                                                    <ItemStyle CssClass="grid" />
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="<%$ Resources:Attendance,Company Name %>" SortExpression="Company_Name">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lbCompanyName" runat="server" Text='<%# Eval("Company_Name") %>'></asp:Label>
                                                        <asp:Label ID="lblCompanyId" Visible="false" runat="server" Text='<%# Eval("Company_Id") %>'></asp:Label>
                                                    </ItemTemplate>
                                                    <ItemStyle CssClass="grid" />
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="<%$ Resources:Attendance,Company Name(Local) %>" SortExpression="Company_Name_L">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblCompanyNameL" runat="server" Text='<%# Eval("Company_Name_L") %>'></asp:Label>
                                                    </ItemTemplate>
                                                    <ItemStyle CssClass="grid" />
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="<%$ Resources:Attendance,Parent Company Name %>" SortExpression="ParentCompany">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblCompanyParentName" runat="server" Text='<%# Eval("ParentCompany") %>'></asp:Label>
                                                    </ItemTemplate>
                                                    <ItemStyle CssClass="grid" />
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="<%$ Resources:Attendance,Company Code %>" SortExpression="Company_Code">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblCompanycode" runat="server" Text='<%# Eval("Company_Code") %>'></asp:Label>
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
                                    <asp:Panel ID="pnlAddress1" runat="server" class="MsgOverlayAddress" Visible="False">
                                        <asp:Panel ID="pnlAddress2" runat="server" class="MsgPopUpPanelAddress" Visible="False">
                                            <asp:Panel ID="pnlAddress3" runat="server" DefaultButton="btnAddressSave" Style="width: 100%;
                                                height: 100%; text-align: center;">
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
                                                <table width="100%" style="padding-left: 43px;border-top: solid 2px #c2c2c2">
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
                                                                Enabled="True" ServiceMethod="GetCompletionListNewAddress" ServicePath="" CompletionInterval="100"
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
                                                            <asp:Label ID="Label1" runat="server" CssClass="labelComman" Text="<%$ Resources:Attendance,Country %>" />
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
                                                            <asp:HiddenField ID="HiddenField1" runat="server" />
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
    <asp:UpdateProgress ID="UpdateProgress1" runat="server">
      <ProgressTemplate>
            <div id="Background">
            </div>
            <div id="Progress">
                <center>
                    <img src="../Images/ajax-loader2.gif" style="vertical-align: middle" />
                </center>
            </div>
        </ProgressTemplate>
    </asp:UpdateProgress>
</asp:Content>
