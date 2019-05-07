<%@ Page Language="C#" MasterPageFile="~/ERPMaster.master" AutoEventWireup="true"
    CodeFile="ProductBuilder.aspx.cs" Inherits="Inventory_ProductBuilder" Title="Untitled Page" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <asp:UpdatePanel ID="upallpage" runat="server">
        <Triggers>
            <asp:PostBackTrigger ControlID="btnList" />
            <asp:PostBackTrigger ControlID="btnNew" />
            <%--<asp:PostBackTrigger ControlID="GridProduct" />
--%>        </Triggers>
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
                                                <asp:Label ID="lblHeader" runat="server" Text="<%$ Resources:Attendance,Product Builder %>"
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
                                                <asp:TemplateField ItemStyle-Width="1px">
                                                    <ItemTemplate>
                                                        <asp:ImageButton ID="btnEdit" runat="server" CausesValidation="False" CommandArgument='<%# Eval("ProductId") %>'
                                                            ImageUrl="~/Images/edit.png" OnCommand="btnEdit_Command" Width="16px" />
                                                    </ItemTemplate>
                                                    <ItemStyle HorizontalAlign="Center" />
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="<%$ Resources:Attendance,Product Id %>" ItemStyle-Width="150px">
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
                                                <td align='<%= Common.ChangeTDForDefaultLeft()%>'>
                                                    <asp:Label ID="lblPartNo" runat="server" Text="<%$ Resources:Attendance,Product Part No .%>"
                                                        CssClass="labelComman"> </asp:Label>
                                                </td>
                                                <td>
                                                    :
                                                </td>
                                                <td colspan="4">
                                                    <table>
                                                        <tr>
                                                            <td align='<%= Common.ChangeTDForDefaultLeft()%>'>
                                                                <asp:TextBox ID="txtProductPartNo" runat="server" CssClass="textComman"></asp:TextBox>-
                                                            </td>
                                                            <td align='<%= Common.ChangeTDForDefaultLeft()%>'>
                                                                <asp:TextBox ID="txtOptionPartNo" runat="server" CssClass="textComman" Width="424px"
                                                                    AutoPostBack="True" OnTextChanged="txtOptionPartNo_TextChanged"></asp:TextBox>
                                                            </td>
                                                        </tr>
                                                    </table>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td colspan="5">
                                                </td>
                                                <td style="padding-left:93px">
                                                    <table>
                                                        <tr>
                                                            <td width="90px">
                                                                <asp:Button ID="btnReset" runat="server" Text="<%$ Resources:Attendance,Reset %>"
                                                                    CssClass="buttonCommman" OnClick="btnReset_Click" />
                                                            </td>
                                                            <td>
                                                                <asp:Button ID="btnCancel" runat="server" Text="<%$ Resources:Attendance,Cancel %>"
                                                                    CssClass="buttonCommman" OnClick="btnCancel_Click" />
                                                            </td>
                                                        </tr>
                                                    </table>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td colspan="2">
                                                </td>
                                                <td colspan="4">
                                                    <asp:GridView ID="gvOptionCategory" runat="server" AllowPaging="True" AutoGenerateColumns="False"
                                                        CssClass="grid" PageSize="<%# SystemParameter.GetPageSize() %>" OnDataBound="gvOptionCategory_DataBound" Width="87%">
                                                        <Columns>
                                                            <asp:TemplateField HeaderText="<%$ Resources:Attendance,Option Category %>" ItemStyle-Width="150px">
                                                                <ItemTemplate>
                                                                    <asp:Label ID="lblOptionCategoryName" Font-Bold="true" runat="server" Text='<%# GetOpCateName(Eval("OptionCategoryId").ToString()) %>'></asp:Label>
                                                                    <br />
                                                                    <asp:Label ID="lblOptionCategoryId" runat="server" Visible="false" Text='<%# Eval("OptionCategoryId") %>'></asp:Label>
                                                                    <asp:RadioButtonList ID="rdoOption" runat="server" Width="100%" CssClass="labelComman" AutoPostBack="True"
                                                                        OnSelectedIndexChanged="rdoOption_SelectedIndexChanged">
                                                                    </asp:RadioButtonList>
                                                                </ItemTemplate>
                                                                <ItemStyle CssClass="grid" />
                                                            </asp:TemplateField>
                                                        </Columns>
                                                        <AlternatingRowStyle CssClass="InvgridAltRow" />
                                                        <HeaderStyle CssClass="Invgridheader" />
                                                        <PagerStyle CssClass="Invgridheader" />
                                                        <RowStyle CssClass="Invgridrow" HorizontalAlign="Center" />
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
