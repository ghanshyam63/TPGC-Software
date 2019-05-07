<%@ Page Language="C#" MasterPageFile="~/ERPMaster.master" AutoEventWireup="true"
    CodeFile="BillOfMaterial.aspx.cs" Inherits="Inventory_BillOfMaterial" Title="PegasusInventory" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <asp:UpdatePanel ID="upallpage" runat="server">
        <Triggers>
            <asp:PostBackTrigger ControlID="btnList" />
            <asp:PostBackTrigger ControlID="btnNew" />
            <%--<asp:PostBackTrigger ControlID="GridProduct" />--%>
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
                                                <asp:Label ID="lblHeader" runat="server" Text="<%$ Resources:Attendance,Bill Of Material %>"
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
                                                        padding-top: 3px; background-image: url('../Images/List.png'); background-repeat: no-repeat;
                                                        height: 49px; background-position: 5px 15px; font: bold 14px Trebuchet MS; color: #000000;" />
                                                </asp:Panel>
                                            </td>
                                            <td>
                                                <asp:Panel ID="pnlMenuNew" runat="server" CssClass="a">
                                                    <asp:Button ID="btnNew" runat="server" Text="<%$ Resources:Attendance,New %>" Width="90px"
                                                        BorderStyle="none" BackColor="Transparent" OnClick="btnNew_Click" Style="padding-left: 10px;
                                                        padding-top: 3px; background-image: url('../Images/New.png'); background-repeat: no-repeat;
                                                        height: 49px; background-position: 5px 15px; font: bold 14px Trebuchet MS; color: #000000;" />
                                                </asp:Panel>
                                            </td>
                                        </tr>
                                    </table>
                                </td>
                            </tr>
                            <tr>
                                <td bgcolor="#ccddee" colspan="4" width="100%" height="500px" valign="top">
                                    <asp:Panel ID="pnlList" runat="server">
                                        <div width="100%" style="background-image: url(../Images/bg_repeat.jpg); background-repeat: repeat;">
                                            <asp:Panel ID="pnlFilter" runat="server" DefaultButton="btnbind">
                                                <table width="100%" style="padding-left: 20px; height: 38px">
                                                    <tr>
                                                        <td width="90px">
                                                            <asp:Label ID="lblSelectOption" runat="server" CssClass="labelComman" Text="<%$ Resources:Attendance,Select Option %>"></asp:Label>
                                                        </td>
                                                        <td width="180px">
                                                            <asp:DropDownList ID="ddlFieldName" runat="server" CssClass="DropdownSearch" Height="25px"
                                                                Width="170px">
                                                                <asp:ListItem Text="<%$ Resources:Attendance,Product Id %>" Value="ProductCode"></asp:ListItem>
                                                                <asp:ListItem Text="<%$ Resources:Attendance,Product Name %>" Value="EProductName"
                                                                    Selected="True"></asp:ListItem>
                                                                <asp:ListItem Text="<%$ Resources:Attendance,Model No. %>" Value="ModelNo"></asp:ListItem>
                                                                <asp:ListItem Text="<%$ Resources:Attendance,Item Type %>" Value="ItemType"></asp:ListItem>
                                                            </asp:DropDownList>
                                                        </td>
                                                        <td width="135px">
                                                            <asp:DropDownList ID="ddlOption" runat="server" CssClass="DropdownSearch" Height="25px"
                                                                Width="120px">
                                                                <asp:ListItem Text="<%$ Resources:Attendance,--Select-- %>"></asp:ListItem>
                                                                <asp:ListItem Text="<%$ Resources:Attendance,Equal %>"></asp:ListItem>
                                                                <asp:ListItem Text="<%$ Resources:Attendance,Like %>"></asp:ListItem>
                                                                <asp:ListItem Selected="True" Text="<%$ Resources:Attendance,Contains %>"></asp:ListItem>
                                                            </asp:DropDownList>
                                                        </td>
                                                        <td width="24%">
                                                            <asp:TextBox ID="txtValue" runat="server" CssClass="textCommanSearch" Height="14px"
                                                                Width="100%" />
                                                        </td>
                                                        <td width="50px" align="center">
                                                            <asp:ImageButton ID="btnbind" runat="server" CausesValidation="False" Height="25px"
                                                                ImageUrl="~/Images/search.png" Width="25px" OnClick="btnbind_Click" ToolTip="<%$ Resources:Attendance,Search %>" />
                                                        </td>
                                                        <td width="50px" align="center">
                                                            <asp:Panel ID="pnlRefresh" runat="server" DefaultButton="btnRefresh">
                                                                <asp:ImageButton ID="btnRefresh" runat="server" CausesValidation="False" Height="25px"
                                                                    ImageUrl="~/Images/refresh.png" Width="25px" OnClick="btnRefresh_Click" ToolTip="<%$ Resources:Attendance,Refresh %>">
                                                                </asp:ImageButton>
                                                            </asp:Panel>
                                                        </td>
                                                        <td>
                                                            <td align="center">
                                                                <asp:Label ID="lblTotalRecord" runat="server" Text="<%$ Resources:Attendance,Total Records: 0 %>"
                                                                    CssClass="labelComman"></asp:Label>
                                                            </td>
                                                        </td>
                                                    </tr>
                                                </table>
                                            </asp:Panel>
                                        </div>
                                        <asp:GridView ID="GridProduct" runat="server" AllowPaging="True" AutoGenerateColumns="False"
                                            Width="100%" CssClass="grid" DataKeyNames="ProductId" PageSize="<%# SystemParameter.GetPageSize() %>" OnPageIndexChanging="GridProduct_PageIndexChanging">
                                            <Columns>
                                                <asp:TemplateField HeaderText="<%$ Resources:Attendance,Edit %>">
                                                    <ItemTemplate>
                                                        <asp:ImageButton ID="btnEdit" runat="server" CausesValidation="False" CommandArgument='<%# Eval("ProductId") %>'
                                                            ImageUrl="~/Images/edit.png" OnCommand="btnEdit_Command" Width="16px" Visible="False"
                                                            ToolTip="<%$ Resources:Attendance,Edit %>" />
                                                    </ItemTemplate>
                                                    <ItemStyle HorizontalAlign="Center" />
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="<%$ Resources:Attendance,Product Id %>" ItemStyle-Width="200px">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblProductId" runat="server" Text='<%# Eval("Productcode") %>'></asp:Label>
                                                    </ItemTemplate>
                                                    <ItemStyle CssClass="grid" />
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="<%$ Resources:Attendance,Product Name %>">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblProductName" runat="server" Text='<%# Eval("EProductName") %>'></asp:Label>
                                                    </ItemTemplate>
                                                    <ItemStyle CssClass="grid" />
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="<%$ Resources:Attendance, Model No %>" ItemStyle-Width="100px">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblModelNo" runat="server" Text='<%# Eval("ModelNo") %>'></asp:Label>
                                                    </ItemTemplate>
                                                    <ItemStyle CssClass="grid" />
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="<%$ Resources:Attendance,Item Type %>" ItemStyle-Width="200px">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblItemType" runat="server" Text='<%#GetItemType(Eval("ItemType").ToString()) %>'></asp:Label>
                                                    </ItemTemplate>
                                                    <ItemStyle CssClass="grid" />
                                                </asp:TemplateField>
                                            </Columns>
                                            <AlternatingRowStyle CssClass="InvgridAltRow" />
                                            <HeaderStyle CssClass="Invgridheader" />
                                            <PagerStyle CssClass="Invgridheader" />
                                            <RowStyle CssClass="Invgridrow" HorizontalAlign="Center" />
                                        </asp:GridView>
                                    </asp:Panel>
                                    <asp:Panel ID="pnlNew" runat="server">
                                        <table style="width: 100%; padding-left: 43px">
                                            <tr>
                                                <td align='<%= Common.ChangeTDForDefaultLeft()%>' width="170px">
                                                    <asp:Label ID="lblTransType" runat="server" Text="<%$ Resources:Attendance,Type %>"
                                                        CssClass="labelComman"></asp:Label>
                                                </td>
                                                <td>
                                                    :
                                                </td>
                                                <td align='<%= Common.ChangeTDForDefaultLeft()%>' width="270px">
                                                    <asp:DropDownList ID="ddlTransType" runat="server" Width="260px" AutoPostBack="true"
                                                        OnSelectedIndexChanged="ddlTransType_SelectedIndexChanged" CssClass="textComman">
                                                        <asp:ListItem Text="Assemble" Selected="True" Value="A"></asp:ListItem>
                                                        <asp:ListItem Text="Kit" Value="K"></asp:ListItem>
                                                    </asp:DropDownList>
                                                </td>
                                                <td align='<%= Common.ChangeTDForDefaultLeft()%>' width="150px">
                                                    <asp:Label ID="lblDate" runat="server" Text="<%$ Resources:Attendance,Date %>" CssClass="labelComman"></asp:Label>
                                                </td>
                                                <td>
                                                    :
                                                </td>
                                                <td align='<%= Common.ChangeTDForDefaultLeft()%>'>
                                                    <asp:TextBox ID="txtDate" runat="server" CssClass="textComman"></asp:TextBox>
                                                    <cc1:CalendarExtender ID="CalendarExtender_date" runat="server" TargetControlID="txtDate"
                                                        Format="dd/MM/yyyy" Enabled="True">
                                                    </cc1:CalendarExtender>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td align='<%= Common.ChangeTDForDefaultLeft()%>'>
                                                    <asp:Label ID="lblProductId" runat="server" Text="<%$ Resources:Attendance,Product Name %>"
                                                        CssClass="labelComman"></asp:Label>
                                                </td>
                                                <td>
                                                    :
                                                </td>
                                                <td align='<%= Common.ChangeTDForDefaultLeft()%>'>
                                                    <asp:TextBox ID="txtProductId" runat="server" BackColor="#e3e3e3" CssClass="textComman"
                                                        AutoPostBack="true" OnTextChanged="txtProductId_TextChanged"></asp:TextBox>
                                                    <cc1:AutoCompleteExtender ID="AutoCompleteExtender1" runat="server" CompletionInterval="100"
                                                        DelimiterCharacters="" Enabled="True" MinimumPrefixLength="1" ServiceMethod="GetCompletionListProductName"
                                                        ServicePath="" TargetControlID="txtProductId" UseContextKey="True" CompletionListCssClass="completionList"
                                                        CompletionListItemCssClass="listItem" CompletionListHighlightedItemCssClass="itemHighlighted">
                                                    </cc1:AutoCompleteExtender>
                                                </td>
                                                <td align='<%= Common.ChangeTDForDefaultLeft()%>'>
                                                    <asp:Label ID="lblModelNo" runat="server" Text="<%$ Resources:Attendance,Model No %>"
                                                        CssClass="labelComman"> </asp:Label>
                                                </td>
                                                <td>
                                                    :
                                                </td>
                                                <td align='<%= Common.ChangeTDForDefaultLeft()%>'>
                                                    <asp:TextBox ID="txtModelNo" runat="server" CssClass="textComman" ReadOnly="true"></asp:TextBox>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td colspan="2">
                                                </td>
                                                <td colspan="2">
                                                    <asp:RadioButton ID="rdoOption" runat="server" Text="<%$ Resources:Attendance,Assemble Item %>"
                                                        GroupName="a" AutoPostBack="True" OnCheckedChanged="rdoStockOption_CheckedChanged"
                                                        CssClass="labelComman" />
                                                    <asp:RadioButton ID="rdoStock" runat="server" Text="<%$ Resources:Attendance,Inventory Item %>"
                                                        AutoPostBack="True" GroupName="a" OnCheckedChanged="rdoStockOption_CheckedChanged"
                                                        CssClass="labelComman" />
                                                </td>
                                            </tr>
                                        </table>
                                        <asp:Panel ID="pnlOptStock" runat="server" DefaultButton="btnSave">
                                            <table style="width: 100%; padding-left: 43px">
                                                <tr id="trStock" runat="server">
                                                    <td id="Td1" align="left" runat="server">
                                                        <asp:Label ID="lblSupProductId" runat="server" Text="<%$ Resources:Attendance,Sub Product Name %>"
                                                            CssClass="labelComman"></asp:Label>
                                                    </td>
                                                    <td id="Td2" runat="server">
                                                        :
                                                    </td>
                                                    <td id="Td3" align="left" colspan="4" runat="server">
                                                        <asp:TextBox ID="txtSubProduct" runat="server" Width="87%" BackColor="#e3e3e3" CssClass="textComman"></asp:TextBox>
                                                        <cc1:AutoCompleteExtender ID="AutoCompleteExtender2" runat="server" CompletionInterval="100"
                                                            DelimiterCharacters="" Enabled="True" MinimumPrefixLength="1" ServiceMethod="GetCompletionListSubProductName"
                                                            ServicePath="" TargetControlID="txtSubProduct" UseContextKey="True" CompletionListCssClass="completionList"
                                                            CompletionListItemCssClass="listItem" CompletionListHighlightedItemCssClass="itemHighlighted">
                                                        </cc1:AutoCompleteExtender>
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td align='<%= Common.ChangeTDForDefaultLeft()%>' width="170px">
                                                        <asp:Label ID="lblOption" runat="server" Text="<%$ Resources:Attendance,Product Option %>"
                                                            CssClass="labelComman"></asp:Label>
                                                    </td>
                                                    <td>
                                                        :
                                                    </td>
                                                    <td align='<%= Common.ChangeTDForDefaultLeft()%>' width="270px">
                                                        <asp:TextBox ID="txtOption" runat="server" MaxLength="3" CssClass="textComman"></asp:TextBox>
                                                    </td>
                                                    <td align='<%= Common.ChangeTDForDefaultLeft()%>' width="150px">
                                                        <asp:Label ID="lblOptCatId" runat="server" Text="<%$ Resources:Attendance,Option Category Name %>"
                                                            CssClass="labelComman"></asp:Label>
                                                    </td>
                                                    <td>
                                                        :
                                                    </td>
                                                    <td align='<%= Common.ChangeTDForDefaultLeft()%>'>
                                                        <asp:TextBox ID="txtOptCatId" BackColor="#e3e3e3" runat="server" CssClass="textComman"
                                                            AutoPostBack="true" OnTextChanged="txtOptCatId_TextChanged"></asp:TextBox>
                                                        <cc1:AutoCompleteExtender ID="txtOpCateName_AutoCompleteExtender" runat="server"
                                                            DelimiterCharacters="" Enabled="True" ServiceMethod="GetCompletionList" ServicePath=""
                                                            CompletionInterval="100" MinimumPrefixLength="1" CompletionSetCount="1" TargetControlID="txtOptCatId"
                                                            UseContextKey="True" CompletionListCssClass="completionList" CompletionListItemCssClass="listItem"
                                                            CompletionListHighlightedItemCssClass="itemHighlighted">
                                                        </cc1:AutoCompleteExtender>
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td align='<%= Common.ChangeTDForDefaultLeft()%>'>
                                                        <asp:Label ID="lblShortOptionDesc" runat="server" Text="<%$ Resources:Attendance,Option Short Description %>"
                                                            CssClass="labelComman"></asp:Label>
                                                    </td>
                                                    <td>
                                                        :
                                                    </td>
                                                    <td colspan="4" align='<%= Common.ChangeTDForDefaultLeft()%>'>
                                                        <asp:TextBox ID="txtShortDesc" runat="server" TextMode="MultiLine" Width="87%" CssClass="textComman"></asp:TextBox>
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td align='<%= Common.ChangeTDForDefaultLeft()%>'>
                                                        <asp:Label ID="lblOptionDesc" runat="server" Text="<%$ Resources:Attendance,Option Description %>"
                                                            CssClass="labelComman"></asp:Label>
                                                    </td>
                                                    <td>
                                                        :
                                                    </td>
                                                    <td colspan="4" align='<%= Common.ChangeTDForDefaultLeft()%>'>
                                                        <asp:TextBox ID="txtOptionDesc" runat="server" TextMode="MultiLine" Width="87%" CssClass="textComman"></asp:TextBox>
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td align='<%= Common.ChangeTDForDefaultLeft()%>'>
                                                        <asp:Label ID="lblUnitPrice" runat="server" Text="<%$ Resources:Attendance,Unit Price %>"
                                                            CssClass="labelComman"></asp:Label>
                                                    </td>
                                                    <td>
                                                        :
                                                    </td>
                                                    <td align='<%= Common.ChangeTDForDefaultLeft()%>'>
                                                        <asp:TextBox ID="txtUnitPrice" runat="server" CssClass="textComman"></asp:TextBox>
                                                        <cc1:FilteredTextBoxExtender ID="FilteredtxtUnitPrice" runat="server" Enabled="True"
                                                            TargetControlID="txtUnitPrice" ValidChars="1,2,3,4,5,6,7,8,9,0,.">
                                                        </cc1:FilteredTextBoxExtender>
                                                    </td>
                                                    <td align='<%= Common.ChangeTDForDefaultLeft()%>'>
                                                        <asp:Label ID="lblQty" runat="server" Text="<%$ Resources:Attendance,Quantity %>"
                                                            CssClass="labelComman"></asp:Label>
                                                    </td>
                                                    <td>
                                                        :
                                                    </td>
                                                    <td align='<%= Common.ChangeTDForDefaultLeft()%>'>
                                                        <asp:TextBox ID="txtQty" runat="server" CssClass="textComman"></asp:TextBox>
                                                        <cc1:FilteredTextBoxExtender ID="FilteredtxtQty" runat="server" Enabled="True" TargetControlID="txtQty"
                                                            ValidChars="1,2,3,4,5,6,7,8,9,0,.">
                                                        </cc1:FilteredTextBoxExtender>
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td colspan="6">
                                                        <center>
                                                            <table>
                                                                <tr>
                                                                    <td width="100px">
                                                                        <asp:Button ID="btnSave" runat="server" Text="<%$ Resources:Attendance,Save %>" CssClass="buttonCommman"
                                                                            OnClick="btnSave_Click" Visible="False" />
                                                                    </td>
                                                                    <td>
                                                                        <asp:Button ID="btnReset" runat="server" Text="<%$ Resources:Attendance,Reset %>"
                                                                            CssClass="buttonCommman" OnClick="btnReset_Click" />
                                                                    </td>
                                                                </tr>
                                                            </table>
                                                        </center>
                                                        <asp:HiddenField ID="hdnDetailEdit" runat="server" />
                                                    </td>
                                                </tr>
                                            </table>
                                        </asp:Panel>
                                        <asp:Panel ID="pnlChlidGrid" runat="server" Visible="false">
                                            <table style="width: 100%; padding-left: 43px">
                                                <tr>
                                                    <td align='<%= Common.ChangeTDForDefaultLeft()%>'>
                                                        <asp:GridView ID="gvProductSpecsChild" runat="server" CellPadding="4" ForeColor="#333333"
                                                            GridLines="None" AutoGenerateColumns="False" Width="100%" PageSize="<%# SystemParameter.GetPageSize() %>" AllowPaging="True"
                                                            OnPageIndexChanging="gvProductSpecsChild_PageIndexChanging" CssClass="grid">
                                                            <Columns>
                                                                <asp:TemplateField HeaderText="<%$ Resources:Attendance,Edit %>">
                                                                    <ItemTemplate>
                                                                        <asp:ImageButton ID="btnDetailEdit" runat="server" ImageUrl="~/Images/edit.png" CommandArgument='<%# Eval("TransId") %>'
                                                                            Height="16px" OnCommand="btnDetailEdit_Command" ToolTip="<%$ Resources:Attendance,Edit %>"
                                                                            Visible="false" />
                                                                    </ItemTemplate>
                                                                    <ItemStyle CssClass="grid" HorizontalAlign="Center" />
                                                                </asp:TemplateField>
                                                                <asp:TemplateField HeaderText="<%$ Resources:Attendance,Delete %>">
                                                                    <ItemTemplate>
                                                                        <asp:ImageButton ID="IbtnDelete" runat="server" Height="16px" ImageUrl="~/Images/Erase.png"
                                                                            Width="16px" CommandArgument='<%# Eval("TransId") %>' ToolTip="<%$ Resources:Attendance,Delete %>"
                                                                            OnCommand="IbtnDelete_Command" Visible="false" />
                                                                    </ItemTemplate>
                                                                    <ItemStyle CssClass="grid" HorizontalAlign="Center" />
                                                                </asp:TemplateField>
                                                                <asp:TemplateField HeaderText="<%$ Resources:Attendance,Id %>">
                                                                    <ItemTemplate>
                                                                        <asp:Label ID="lblOptionId" runat="server" Text='<%# Eval("OptionId") %>'></asp:Label>
                                                                    </ItemTemplate>
                                                                </asp:TemplateField>
                                                                <asp:TemplateField HeaderText="<%$ Resources:Attendance,Option Category %>">
                                                                    <ItemTemplate>
                                                                        <asp:Label ID="lblOptionCatId" runat="server" Text='<%# GetOpCateName(Eval("OptionCategoryId").ToString()) %>'></asp:Label>
                                                                    </ItemTemplate>
                                                                    <ItemStyle CssClass="grid" />
                                                                </asp:TemplateField>
                                                                <asp:TemplateField HeaderText="<%$ Resources:Attendance,Unit Price %>">
                                                                    <ItemTemplate>
                                                                        <asp:Label ID="lblUnitPrice" runat="server" Text='<%# Eval("UnitPrice") %>'></asp:Label>
                                                                    </ItemTemplate>
                                                                    <ItemStyle CssClass="grid" />
                                                                </asp:TemplateField>
                                                                <asp:TemplateField HeaderText="<%$ Resources:Attendance,Quantity %>">
                                                                    <ItemTemplate>
                                                                        <asp:Label ID="lblQty" runat="server" Text='<%# Eval("Quantity") %>'></asp:Label>
                                                                    </ItemTemplate>
                                                                    <ItemStyle CssClass="grid" />
                                                                </asp:TemplateField>
                                                                <asp:TemplateField HeaderText="<%$ Resources:Attendance,Sub Product %>">
                                                                    <ItemTemplate>
                                                                        <asp:Label ID="lblSubProdId" runat="server" Text='<%# getProductName(Eval("SubProductId").ToString()) %>'></asp:Label>
                                                                    </ItemTemplate>
                                                                    <ItemStyle CssClass="grid" />
                                                                </asp:TemplateField>
                                                                <asp:TemplateField HeaderText="<%$ Resources:Attendance,Short Description %>">
                                                                    <ItemTemplate>
                                                                        <asp:Label ID="lblShortDesc" runat="server" Text='<%# Eval("ShortDescription") %>'></asp:Label>
                                                                    </ItemTemplate>
                                                                    <ItemStyle CssClass="grid" />
                                                                </asp:TemplateField>
                                                            </Columns>
                                                            <AlternatingRowStyle CssClass="InvgridAltRow" />
                                                            <HeaderStyle CssClass="Invgridheader" />
                                                            <PagerStyle CssClass="Invgridheader" />
                                                            <RowStyle CssClass="Invgridrow" />
                                                        </asp:GridView>
                                                    </td>
                                                    <td>
                                                    </td>
                                                </tr>
                                            </table>
                                            <table width="100%">
                                                <tr>
                                                    <td align="right" style="padding-right: 90px">
                                                        <asp:Button ID="btnFinalSave" runat="server" Text="<%$ Resources:Attendance,Final Save %>"
                                                            CssClass="buttonCommman" OnClick="btnFinalSave_Click" />
                                                    </td>
                                                </tr>
                                            </table>
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
