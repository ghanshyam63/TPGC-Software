<%@ Page Language="C#" MasterPageFile="~/ERPMaster.master" AutoEventWireup="true"
    CodeFile="PurchaseInvoice.aspx.cs" Inherits="Purchase_PurchaseInvoice" Title="Untitled Page" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <asp:UpdatePanel ID="UpdatePanel1" runat="server">
        <Triggers>
            <asp:PostBackTrigger ControlID="btnList" />
            <asp:PostBackTrigger ControlID="btnNew" />
        </Triggers>
        <ContentTemplate>
            <table width="100%">
                <tr>
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
                                            <td style="padding-left: 5px">
                                                <asp:Label ID="lblHeader" runat="server" Text="<%$ Resources:Attendance,Purchase Invoice %>"
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
                                                        padding-top: 3px; background-image: url('../Images/List.png' ); background-repeat: no-repeat;
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
                                                                <asp:ListItem Selected="True" Text="<%$ Resources:Attendance,Invoice No %>" Value="InvoiceNo"></asp:ListItem>
                                                                <asp:ListItem Text="<%$ Resources:Attendance,Invoice Date %>" Value="InvoiceDate"></asp:ListItem>
                                                                <asp:ListItem Text="<%$ Resources:Attendance,Supplier Invoice No %>" Value="SupInvoiceNo"></asp:ListItem>
                                                                <asp:ListItem Text="<%$ Resources:Attendance,Supplier Invoice Date %>" Value="SupInvoiceDate"></asp:ListItem>
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
                                            <asp:HiddenField ID="HdnEdit" runat="server" />
                                        </asp:Panel>
                                        <asp:GridView ID="GvPurchaseInvocie" PageSize="<%# SystemParameter.GetPageSize() %>"
                                            runat="server" AutoGenerateColumns="False" Width="100%" AllowPaging="True" AllowSorting="True"
                                            CssClass="grid" OnSorting="GvPurchaseInvocie_OnSorting" OnPageIndexChanging="GvPurchaseInvocie_PageIndexChanging">
                                            <Columns>
                                                <asp:TemplateField HeaderText="<%$ Resources:Attendance,Edit %>">
                                                    <ItemTemplate>
                                                        <asp:ImageButton ID="btnEdit" runat="server" CommandArgument='<%# Eval("TransId") %>'
                                                            ImageUrl="~/Images/edit.png" CausesValidation="False" OnCommand="btnEdit_Command"
                                                            ToolTip="<%$ Resources:Attendance,Edit %>" Visible="false" /></ItemTemplate>
                                                    <ItemStyle HorizontalAlign="Center" />
                                                    <ItemStyle CssClass="grid" />
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="<%$ Resources:Attendance,Delete %>">
                                                    <ItemTemplate>
                                                        <asp:ImageButton ID="IbtnDelete" runat="server" CausesValidation="False" CommandArgument='<%# Eval("TransId") %>'
                                                            ImageUrl="~/Images/Erase.png" OnCommand="IbtnDelete_Command" Visible="false"
                                                            ToolTip="<%$ Resources:Attendance,Delete %>" />
                                                    </ItemTemplate>
                                                    <ItemStyle HorizontalAlign="Center" />
                                                    <ItemStyle CssClass="grid" />
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="<%$ Resources:Attendance,Invoice No %>" SortExpression="InvoiceNo">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lbInvoiceNo" runat="server" Text='<%# Eval("InvoiceNo") %>'></asp:Label>
                                                    </ItemTemplate>
                                                    <ItemStyle CssClass="grid" />
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="<%$ Resources:Attendance,Invoice Date %>" SortExpression="InvoiceDate">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblInvoiceDate" runat="server" Text='<%# GetDateFormat(Eval("InvoiceDate").ToString()) %>'></asp:Label>
                                                    </ItemTemplate>
                                                    <ItemStyle CssClass="grid" />
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="<%$ Resources:Attendance,Supplier Invoice No %>" SortExpression="SupInvoiceNo">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblSupInvoiceNo" runat="server" Text='<%# Eval("SupInvoiceNo") %>'></asp:Label>
                                                    </ItemTemplate>
                                                    <ItemStyle CssClass="grid" />
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="<%$ Resources:Attendance,Supplier Invoice Date %>"
                                                    SortExpression="SupInvoiceDate">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblSupInvoiceDate" runat="server" Text='<%# GetDateFormat(Eval("SupInvoiceDate").ToString()) %>'></asp:Label>
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
                                                <td width="170px" align='<%= Common.ChangeTDForDefaultLeft()%>'>
                                                    <asp:Label ID="lblInvoicedate" runat="server" CssClass="labelComman" Text="<%$ Resources:Attendance,Invoice Date %>"></asp:Label>
                                                </td>
                                                <td width="1px">
                                                    :
                                                </td>
                                                <td align='<%= Common.ChangeTDForDefaultLeft()%>' width="270px">
                                                    <asp:TextBox ID="txtInvoicedate" runat="server" CssClass="textComman" />
                                                    <cc1:CalendarExtender ID="txtCalenderExtender" runat="server" TargetControlID="txtInvoicedate"
                                                        Format="dd-MMM-yyyy">
                                                    </cc1:CalendarExtender>
                                                </td>
                                                <td width="157px" align='<%= Common.ChangeTDForDefaultLeft()%>'>
                                                    <asp:Label ID="lblInvoiceNo" runat="server" CssClass="labelComman" Text="<%$ Resources:Attendance,Invoice No %>"></asp:Label>
                                                </td>
                                                <td width="1px">
                                                    :
                                                </td>
                                                <td align='<%= Common.ChangeTDForDefaultLeft()%>'>
                                                    <asp:TextBox ID="txtInvoiceNo" runat="server" CssClass="textComman" AutoPostBack="True"
                                                        OnTextChanged="txtInvoiceNo_TextChanged" />
                                                </td>
                                            </tr>
                                            <tr>
                                                <td align='<%= Common.ChangeTDForDefaultLeft()%>' width="170px">
                                                    <asp:Label ID="lblOrderType" runat="server" Text="<%$ Resources:Attendance,Invoice Type %>"
                                                        CssClass="labelComman"></asp:Label>
                                                </td>
                                                <td>
                                                    :
                                                </td>
                                                <td align='<%= Common.ChangeTDForDefaultLeft()%>' width="270px">
                                                    <asp:DropDownList ID="ddlInvoiceType" runat="server" CssClass="textComman" Width="260px">
                                                        <asp:ListItem Text="Cash" Value="A"></asp:ListItem>
                                                        <asp:ListItem Text="Credit" Value="R"></asp:ListItem>
                                                    </asp:DropDownList>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td width="170px" align='<%= Common.ChangeTDForDefaultLeft()%>'>
                                                    <asp:Label ID="lblSupInvoiceDate" runat="server" CssClass="labelComman" Text="<%$ Resources:Attendance,Supplier Invoice Date %>"></asp:Label>
                                                </td>
                                                <td width="1px">
                                                    :
                                                </td>
                                                <td align='<%= Common.ChangeTDForDefaultLeft()%>' width="270px">
                                                    <asp:TextBox ID="txtSupInvoiceDate" runat="server" CssClass="textComman" />
                                                    <cc1:CalendarExtender ID="CalendarExtender4" runat="server" TargetControlID="txtSupInvoiceDate"
                                                        Format="dd-MMM-yyyy">
                                                    </cc1:CalendarExtender>
                                                </td>
                                                <td width="150px" align='<%= Common.ChangeTDForDefaultLeft()%>'>
                                                    <asp:Label ID="lblSupInvoiceNo" runat="server" CssClass="labelComman" Text="<%$ Resources:Attendance,Supplier Invoice No %>"></asp:Label>
                                                </td>
                                                <td width="1px">
                                                    :
                                                </td>
                                                <td align='<%= Common.ChangeTDForDefaultLeft()%>'>
                                                    <asp:TextBox ID="txtSupplierInvoiceNo" runat="server" CssClass="textComman" />
                                                </td>
                                            </tr>
                                            <tr>
                                                <td align='<%= Common.ChangeTDForDefaultLeft()%>'>
                                                    <asp:Label ID="Label22" runat="server" CssClass="labelComman" Text="<%$ Resources:Attendance,Supplier Name %>"></asp:Label>
                                                </td>
                                                <td width="1px">
                                                    :
                                                </td>
                                                <td align='<%= Common.ChangeTDForDefaultLeft()%>' colspan="4">
                                                    <asp:TextBox ID="txtSupplierName" runat="server" CssClass="textComman" BackColor="#e3e3e3" AutoPostBack="True"
                                                        OnTextChanged="txtSupplierName_TextChanged" Width="688px" />
                                                    <cc1:AutoCompleteExtender ID="txtSupplierName_AutoCompleteExtender" runat="server"
                                                        CompletionInterval="100" DelimiterCharacters="" Enabled="True" MinimumPrefixLength="1"
                                                        ServiceMethod="GetCompletionList" ServicePath="" TargetControlID="txtSupplierName"
                                                        UseContextKey="True" CompletionListCssClass="completionList" CompletionListItemCssClass="listItem"
                                                        CompletionListHighlightedItemCssClass="itemHighlighted">
                                                    </cc1:AutoCompleteExtender>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td colspan="2">
                                                </td>
                                                <td colspan="2" align='<%= Common.ChangeTDForDefaultLeft()%>'>
                                                    <asp:RadioButton ID="RdoPo" runat="server" CssClass="labelComman" Text="<%$ Resources:Attendance,Purchase Order %>"
                                                        OnCheckedChanged="RdoPo_CheckedChanged" AutoPostBack="true" GroupName="Po" />
                                                    <asp:RadioButton ID="RdoWithOutPo" runat="server" CssClass="labelComman" GroupName="Po"
                                                        Text="<%$ Resources:Attendance,Without Purchase Order %>" OnCheckedChanged="RdoPo_CheckedChanged"
                                                        AutoPostBack="true" />
                                                </td>
                                                <td>
                                                </td>
                                                <td align='<%= Common.ChangeTDForDefaultLeft()%>'>
                                                    <asp:Panel ID="pnlbtnAddProduct" runat="server" DefaultButton="btnAddProduct">
                                                        <asp:Button ID="btnAddProduct" runat="server" Text="<%$ Resources:Attendance,Add Product %>"
                                                            BackColor="Transparent" BorderStyle="None" Font-Bold="true" Visible="false" CssClass="btnLocation"
                                                            Font-Size="13px" Font-Names=" Arial" Width="172px" Height="32px" OnClick="btnAddProduct_Click" />
                                                    </asp:Panel>
                                                    <asp:Panel ID="pnlbtnAddOrderProduct" runat="server" DefaultButton="btnAddOrderProduct">
                                                        <asp:Button ID="btnAddOrderProduct" runat="server" Text="<%$ Resources:Attendance,Add Product %>"
                                                            BackColor="Transparent" BorderStyle="None" Font-Bold="true" CssClass="btnLocation"
                                                            Font-Size="13px" Font-Names=" Arial" Width="172px" Height="32px" Visible="false"
                                                            ToolTip="Add Product Based on Purchase Order" OnClick="btnAddOrderProduct_Click" />
                                                    </asp:Panel>
                                                </td>
                                            </tr>
                                        </table>
                                        <table width="100%" style="padding-left: 43px">
                                            <tr>
                                                <td colspan="6">
                                                    <asp:GridView ID="gvPurchaseOrder" PageSize="<%# SystemParameter.GetPageSize() %>"
                                                        runat="server" ShowHeader="false" AutoGenerateColumns="False" Width="100%" AllowPaging="True"
                                                        AllowSorting="True" CssClass="grid">
                                                        <Columns>
                                                            <asp:TemplateField HeaderText="<%$ Resources:Attendance,Purchase Order No %>" SortExpression="PONo">
                                                                <ItemTemplate>
                                                                    <table width="100%" cellpadding="0">
                                                                        <tr>
                                                                            <td width="1px" style="border-right: solid 1px #c4cc2">
                                                                                <asp:CheckBox ID="ChkPoId" runat="server" AutoPostBack="True" OnCheckedChanged="ChkPoId_CheckedChanged" />
                                                                                <asp:Label ID="lblPoId" runat="server" Text='<%# Eval("TransId") %>' Visible="false"></asp:Label>
                                                                            </td>
                                                                            <td>
                                                                                <asp:Label ID="lblPONo" runat="server" Font-Size="14px" Text="<%$ Resources:Attendance,Purchase Order No %>"></asp:Label>
                                                                            </td>
                                                                            <td>
                                                                                :
                                                                            </td>
                                                                            <td>
                                                                                <asp:Label ID="lbPONo" runat="server" Font-Size="14px" Font-Bold="true" Text='<%# Eval("PONo") %>'></asp:Label>
                                                                            </td>
                                                                            <td>
                                                                                <asp:Label ID="lbPODate" runat="server" Font-Size="14px" Text="<%$ Resources:Attendance,Purchase Order Date %>"></asp:Label>
                                                                            </td>
                                                                            <td>
                                                                                :
                                                                            </td>
                                                                            <td>
                                                                                <asp:Label ID="lblPODate" runat="server" Font-Size="14px" Font-Bold="true" Text='<%# GetDateFormat(Eval("PODate").ToString()) %>'></asp:Label>
                                                                            </td>
                                                                        </tr>
                                                                    </table>
                                                                    <asp:GridView ID="gvProduct" PageSize="<%# SystemParameter.GetPageSize() %>" runat="server"
                                                                        AutoGenerateColumns="False" Width="100%" ShowFooter="true" CssClass="grid" OnRowCreated="gvProduct_RowCreated">
                                                                        <Columns>
                                                                            <asp:TemplateField HeaderText="<%$ Resources:Attendance,Serial No %>">
                                                                                <ItemTemplate>
                                                                                    <%#Container.DataItemIndex+1 %>
                                                                                    <asp:Label ID="lblTransId" Visible="false" runat="server" Text='<%# Eval("Trans_Id") %>'></asp:Label>
                                                                                </ItemTemplate>
                                                                                <FooterTemplate>
                                                                                    <asp:Label ID="lblTotalQuantity" runat="server" Text="<%$ Resources:Attendance,Total %>"
                                                                                        CssClass="labelComman"></asp:Label>
                                                                                </FooterTemplate>
                                                                                <ItemStyle CssClass="grid" />
                                                                                <FooterStyle BorderStyle="None" />
                                                                            </asp:TemplateField>
                                                                            <asp:TemplateField HeaderText="<%$ Resources:Attendance,Product Name %>" ItemStyle-Width="35%">
                                                                                <ItemTemplate>
                                                                                    <asp:Label ID="lblProductId" Visible="false" runat="server" Text='<%# Eval("Product_Id") %>'></asp:Label>
                                                                                    <asp:Label ID="lblProductName" runat="server" Text='<%# ProductName(Eval("Product_Id").ToString()) %>'></asp:Label>
                                                                                </ItemTemplate>
                                                                                <ItemStyle CssClass="grid" />
                                                                                <FooterStyle BorderStyle="None" />
                                                                            </asp:TemplateField>
                                                                            <asp:TemplateField HeaderText="<%$ Resources:Attendance,Name %>">
                                                                                <ItemTemplate>
                                                                                    <asp:Label ID="lblUnitId" Visible="false" runat="server" Text='<%# Eval("UnitId") %>'></asp:Label>
                                                                                    <asp:Label ID="lblUnit" runat="server" Text='<%# UnitName(Eval("UnitId").ToString()) %>'></asp:Label>
                                                                                </ItemTemplate>
                                                                                <ItemStyle CssClass="grid" />
                                                                                <FooterStyle BorderStyle="None" />
                                                                            </asp:TemplateField>
                                                                            <asp:TemplateField HeaderText="<%$ Resources:Attendance,Cost %>">
                                                                                <ItemTemplate>
                                                                                    <asp:Label ID="lblUnitRate" runat="server" Text='<%# Eval("UnitCost") %>'></asp:Label>
                                                                                </ItemTemplate>
                                                                                <ItemStyle CssClass="grid" />
                                                                                <FooterStyle BorderStyle="None" />
                                                                            </asp:TemplateField>
                                                                            <asp:TemplateField HeaderText="<%$ Resources:Attendance,Order %>">
                                                                                <ItemTemplate>
                                                                                    <asp:Label ID="lblReqQty" runat="server" Text='<%# Eval("OrderQty") %>'></asp:Label>
                                                                                </ItemTemplate>
                                                                                <ItemStyle CssClass="grid" />
                                                                                <FooterStyle BorderStyle="None" />
                                                                            </asp:TemplateField>
                                                                            <asp:TemplateField HeaderText="<%$ Resources:Attendance,Free %>">
                                                                                <ItemTemplate>
                                                                                    <asp:Label ID="lblFreeQty" runat="server" Text='<%# Eval("FreeQty") %>'></asp:Label>
                                                                                </ItemTemplate>
                                                                                <ItemStyle CssClass="grid" />
                                                                                <FooterStyle BorderStyle="None" />
                                                                            </asp:TemplateField>
                                                                            <asp:TemplateField HeaderText="<%$ Resources:Attendance,Remain %>">
                                                                                <ItemTemplate>
                                                                                    <asp:Label ID="lblRemainQty" runat="server" Text='<%# Eval("RemainQty") %>'></asp:Label>
                                                                                </ItemTemplate>
                                                                                <ItemStyle CssClass="grid" />
                                                                                <FooterStyle BorderStyle="None" />
                                                                            </asp:TemplateField>
                                                                            <asp:TemplateField HeaderText="<%$ Resources:Attendance,Received %>">
                                                                                <ItemTemplate>
                                                                                    <asp:TextBox ID="txtRecQty" runat="server" CssClass="textCommanSearch" Width="50px"
                                                                                        Height="10px" Text="0" AutoPostBack="True" OnTextChanged="txtRecQty_TextChanged"></asp:TextBox>
                                                                                         <cc1:FilteredTextBoxExtender ID="FilteredTextBoxExtender59" runat="server" Enabled="True"
                                                        TargetControlID="txtRecQty" ValidChars="1,2,3,4,5,6,7,8,9,0,.">
                                                    </cc1:FilteredTextBoxExtender>
                               
                                                                                </ItemTemplate>
                                                                                <ItemStyle CssClass="grid" />
                                                                                <FooterStyle BorderStyle="None" HorizontalAlign="Left" />
                                                                                <FooterTemplate>
                                                                                    <asp:Label ID="txtTotalQuantity" runat="server" Text="0" CssClass="labelComman"></asp:Label>
                                                                                </FooterTemplate>
                                                                            </asp:TemplateField>
                                                                            <asp:TemplateField HeaderText="<%$ Resources:Attendance,Discount %>">
                                                                                <ItemTemplate>
                                                                                    <asp:TextBox ID="txtDiscount" runat="server" CssClass="textCommanSearch" Width="50px"
                                                                                        Height="10px" Text="0" AutoPostBack="True" OnTextChanged="txtRecQty_TextChanged"></asp:TextBox>
                                                                                             <cc1:FilteredTextBoxExtender ID="FilteredTextBoxExtender60" runat="server" Enabled="True"
                                                        TargetControlID="txtDiscount" ValidChars="1,2,3,4,5,6,7,8,9,0,.">
                                                    </cc1:FilteredTextBoxExtender>
                                                                                </ItemTemplate>
                                                                                <FooterStyle BorderStyle="None" />
                                                                                <ItemStyle CssClass="grid" />
                                                                            </asp:TemplateField>
                                                                            <asp:TemplateField HeaderText="<%$ Resources:Attendance,Tax %>">
                                                                                <ItemTemplate>
                                                                                    <asp:TextBox ID="txtTax" runat="server" CssClass="textCommanSearch" Width="50px"
                                                                                        Height="10px" Text="0" AutoPostBack="True" OnTextChanged="txtRecQty_TextChanged"></asp:TextBox>
                                                                                           <cc1:FilteredTextBoxExtender ID="FilteredTextBoxExtender61" runat="server" Enabled="True"
                                                        TargetControlID="txtTax" ValidChars="1,2,3,4,5,6,7,8,9,0,.">
                                                    </cc1:FilteredTextBoxExtender>
                                                                                </ItemTemplate>
                                                                                <ItemStyle CssClass="grid" />
                                                                                <FooterStyle BorderStyle="None" />
                                                                            </asp:TemplateField>
                                                                            <asp:TemplateField HeaderText="<%$ Resources:Attendance,Amount %>">
                                                                                <ItemTemplate>
                                                                                    <asp:TextBox ID="txtAmount" runat="server" CssClass="textCommanSearch" Width="80px"
                                                                                        Height="10px" Text="0" AutoPostBack="True" OnTextChanged="txtRecQty_TextChanged"></asp:TextBox>
                                                                                               <cc1:FilteredTextBoxExtender ID="FilteredTextBoxExtender62" runat="server" Enabled="True"
                                                        TargetControlID="txtAmount" ValidChars="1,2,3,4,5,6,7,8,9,0,.">
                                                    </cc1:FilteredTextBoxExtender>
                                                                                </ItemTemplate>
                                                                                <FooterTemplate>
                                                                                    <asp:Label ID="txtTotalAmount" runat="server" Font-Bold="true" Text="0" CssClass="labelComman"></asp:Label>
                                                                                </FooterTemplate>
                                                                                <ItemStyle CssClass="grid" />
                                                                                <FooterStyle BorderStyle="None" HorizontalAlign="Left" />
                                                                            </asp:TemplateField>
                                                                        </Columns>
                                                                        <AlternatingRowStyle CssClass="InvgridAltRow"></AlternatingRowStyle>
                                                                        <HeaderStyle CssClass="Invgridheader" />
                                                                        <PagerStyle CssClass="Invgridheader" />
                                                                        <FooterStyle CssClass="Invgridheader" />
                                                                        <RowStyle CssClass="Invgridrow" HorizontalAlign="Center"></RowStyle>
                                                                    </asp:GridView>
                                                                </ItemTemplate>
                                                                <FooterTemplate>
                                                                    <table>
                                                                        <tr>
                                                                            <td width="500px">
                                                                            </td>
                                                                            <td width="100px">
                                                                                <asp:Label ID="lblTotalQuantity" runat="server" CssClass="labelComman"></asp:Label>
                                                                            </td>
                                                                            <td width="1px">
                                                                                :
                                                                            </td>
                                                                            <td width="100px">
                                                                                <asp:Label ID="txtTotalQuantity" runat="server" Font-Bold="true" CssClass="labelComman"></asp:Label>
                                                                            </td>
                                                                            <td width="200px">
                                                                            </td>
                                                                            <td width="100px">
                                                                                <asp:Label ID="lblTotalAmount" runat="server" CssClass="labelComman"></asp:Label>
                                                                            </td>
                                                                            <td>
                                                                                :
                                                                            </td>
                                                                            <td width="100px">
                                                                                <asp:Label ID="txtTotalAmount" runat="server" Font-Bold="true" CssClass="labelComman"></asp:Label>
                                                                            </td>
                                                                        </tr>
                                                                    </table>
                                                                </FooterTemplate>
                                                                <FooterStyle HorizontalAlign="Center" />
                                                                <ItemStyle CssClass="grid" />
                                                            </asp:TemplateField>
                                                        </Columns>
                                                        <AlternatingRowStyle CssClass="Invgridrow"></AlternatingRowStyle>
                                                        <HeaderStyle CssClass="Invgridheader" />
                                                        <PagerStyle CssClass="Invgridheader" />
                                                        <FooterStyle CssClass="Invgridheader" />
                                                        <RowStyle CssClass="Invgridrow" HorizontalAlign="Center"></RowStyle>
                                                    </asp:GridView>
                                                    <asp:GridView ID="gvProduct" PageSize="<%# SystemParameter.GetPageSize() %>" runat="server"
                                                        AutoGenerateColumns="False" Width="100%" CssClass="grid" ShowFooter="true">
                                                        <Columns>
                                                            <asp:TemplateField HeaderText="<%$ Resources:Attendance,Edit %>">
                                                                <ItemTemplate>
                                                                    <asp:ImageButton ID="btnPDEdit" runat="server" CommandArgument='<%# Eval("TransId") %>'
                                                                        ImageUrl="~/Images/edit.png" CausesValidation="False" OnCommand="btnPDEdit_Command"
                                                                        ToolTip="<%$ Resources:Attendance,Edit %>" Visible="false" /></ItemTemplate>
                                                                <ItemStyle HorizontalAlign="Center" />
                                                                <ItemStyle CssClass="grid" />
                                                                <FooterTemplate>
                                                                    <asp:Label ID="lblTotalQuantity" runat="server" Text="<%$ Resources:Attendance,Total %>"
                                                                        CssClass="labelComman"></asp:Label>
                                                                </FooterTemplate>
                                                                <FooterStyle BorderStyle="None" />
                                                            </asp:TemplateField>
                                                            <asp:TemplateField HeaderText="<%$ Resources:Attendance,Delete %>">
                                                                <ItemTemplate>
                                                                    <asp:ImageButton ID="IbtnPDDelete" runat="server" CausesValidation="False" CommandArgument='<%# Eval("TransId") %>'
                                                                        ImageUrl="~/Images/Erase.png" OnCommand="IbtnPDDelete_Command" ToolTip="<%$ Resources:Attendance,Delete %>" />
                                                                </ItemTemplate>
                                                                <ItemStyle HorizontalAlign="Center" />
                                                                <ItemStyle CssClass="grid" />
                                                                <FooterStyle BorderStyle="None" />
                                                            </asp:TemplateField>
                                                            <asp:TemplateField HeaderText="<%$ Resources:Attendance,Serial No %>">
                                                                <ItemTemplate>
                                                                    <asp:Label ID="lblSerialNO" runat="server" Text='<%# Eval("SerialNo") %>'></asp:Label>
                                                                </ItemTemplate>
                                                                <ItemStyle CssClass="grid" />
                                                                <FooterStyle BorderStyle="None" />
                                                            </asp:TemplateField>
                                                            <asp:TemplateField HeaderText="<%$ Resources:Attendance,Product Name %>">
                                                                <ItemTemplate>
                                                                    <asp:Label ID="lblProductId" runat="server" Text='<%# ProductName(Eval("ProductId").ToString()) %>'></asp:Label>
                                                                </ItemTemplate>
                                                                <FooterStyle BorderStyle="None" />
                                                                <ItemStyle CssClass="grid" />
                                                            </asp:TemplateField>
                                                            <asp:TemplateField HeaderText="<%$ Resources:Attendance,Unit Name %>">
                                                                <ItemTemplate>
                                                                    <asp:Label ID="lblUnit" runat="server" Text='<%# UnitName(Eval("UnitId").ToString()) %>'></asp:Label>
                                                                </ItemTemplate>
                                                                <FooterStyle BorderStyle="None" />
                                                                <ItemStyle CssClass="grid" />
                                                            </asp:TemplateField>
                                                            <asp:TemplateField HeaderText="<%$ Resources:Attendance,Unit Cost %>">
                                                                <ItemTemplate>
                                                                    <asp:Label ID="lblUnitRate" runat="server" Text='<%# Eval("UnitCost") %>'></asp:Label>
                                                                </ItemTemplate>
                                                                <FooterStyle BorderStyle="None" />
                                                                <ItemStyle CssClass="grid" />
                                                            </asp:TemplateField>
                                                            <asp:TemplateField HeaderText="<%$ Resources:Attendance,Order %>">
                                                                <ItemTemplate>
                                                                    <asp:Label ID="OrderQty" runat="server" Text='<%# Eval("OrderQty") %>'></asp:Label>
                                                                </ItemTemplate>
                                                                <FooterStyle BorderStyle="None" />
                                                                <ItemStyle CssClass="grid" />
                                                            </asp:TemplateField>
                                                            <asp:TemplateField HeaderText="<%$ Resources:Attendance,Free %>">
                                                                <ItemTemplate>
                                                                    <asp:Label ID="lblFreeQty" runat="server" Text='<%# Eval("FreeQty") %>'></asp:Label>
                                                                </ItemTemplate>
                                                                <FooterStyle BorderStyle="None" />
                                                                <ItemStyle CssClass="grid" />
                                                            </asp:TemplateField>
                                                            <asp:TemplateField HeaderText="<%$ Resources:Attendance,Received %>">
                                                                <ItemTemplate>
                                                                    <asp:Label ID="QtyReceived" runat="server" Text='<%# Eval("InvoiceQty") %>'></asp:Label>
                                                                </ItemTemplate>
                                                                <FooterTemplate>
                                                                    <asp:Label ID="txtTotalQuantity" runat="server" Text="0" CssClass="labelComman"></asp:Label>
                                                                </FooterTemplate>
                                                                <FooterStyle BorderStyle="None" />
                                                                <ItemStyle CssClass="grid" />
                                                            </asp:TemplateField>
                                                            <asp:TemplateField HeaderText="<%$ Resources:Attendance,Discount %>">
                                                                <ItemTemplate>
                                                                    <asp:Label ID="lblDiscount" runat="server" Text='<%# Eval("DiscountValue") %>'></asp:Label>
                                                                </ItemTemplate>
                                                                <FooterStyle BorderStyle="None" />
                                                                <ItemStyle CssClass="grid" />
                                                            </asp:TemplateField>
                                                            <asp:TemplateField HeaderText="<%$ Resources:Attendance,Tax %>">
                                                                <ItemTemplate>
                                                                    <asp:Label ID="lblTax" runat="server" Text='<%# Eval("TaxValue") %>'></asp:Label>
                                                                </ItemTemplate>
                                                                <FooterStyle BorderStyle="None" />
                                                                <ItemStyle CssClass="grid" />
                                                            </asp:TemplateField>
                                                            <asp:TemplateField HeaderText="<%$ Resources:Attendance,Amount %>">
                                                                <ItemTemplate>
                                                                    <asp:Label ID="lblAmount" runat="server" Text='<%# (Convert.ToDouble(Eval("UnitCost").ToString())*Convert.ToDouble(Eval("OrderQty").ToString())+Convert.ToDouble(Eval("TaxValue").ToString())-Convert.ToDouble(Eval("DiscountValue").ToString())).ToString() %>'></asp:Label>
                                                                </ItemTemplate>
                                                                <FooterTemplate>
                                                                    <asp:Label ID="txtTotalAmount" runat="server" Font-Bold="true" Text="0" CssClass="labelComman"></asp:Label>
                                                                </FooterTemplate>
                                                                <FooterStyle BorderStyle="None" />
                                                                <ItemStyle CssClass="grid" />
                                                            </asp:TemplateField>
                                                        </Columns>
                                                        <AlternatingRowStyle CssClass="InvgridAltRow"></AlternatingRowStyle>
                                                        <HeaderStyle CssClass="Invgridheader" />
                                                        <FooterStyle CssClass="Invgridheader" />
                                                        <PagerStyle CssClass="Invgridheader" />
                                                        <RowStyle CssClass="Invgridrow" HorizontalAlign="Center"></RowStyle>
                                                    </asp:GridView>
                                                </td>
                                            </tr>
                                        </table>
                                        <table width="100%" style="padding-left: 43px">
                                            <tr>
                                                <td align='<%= Common.ChangeTDForDefaultLeft()%>' width="170px">
                                                    <asp:Label ID="lblNetDisPer" runat="server" Text="<%$ Resources:Attendance,Net Discount(%)%>"
                                                        CssClass="labelComman"></asp:Label>
                                                </td>
                                                <td width="1px">
                                                    :
                                                </td>
                                                <td align='<%= Common.ChangeTDForDefaultLeft()%>' width="271px">
                                                    <table>
                                                        <tr>
                                                            <td align='<%= Common.ChangeTDForDefaultLeft()%>' width="70px">
                                                                <asp:TextBox ID="txtNetDisPer" runat="server" CssClass="textComman" Width="70px"
                                                                    Text="0" AutoPostBack="True" OnTextChanged="txtNetDisPer_TextChanged" />
                                                                      <cc1:FilteredTextBoxExtender ID="FilteredTextBoxExtender34" runat="server" Enabled="True"
                                                        TargetControlID="txtNetDisPer" ValidChars="1,2,3,4,5,6,7,8,9,0,.">
                                                    </cc1:FilteredTextBoxExtender>
                                                            </td>
                                                            <td align='<%= Common.ChangeTDForDefaultLeft()%>' width="1px">
                                                                <asp:Label ID="lblNetDisVal" runat="server" Text="<%$ Resources:Attendance,Value %>"
                                                                    CssClass="labelComman"></asp:Label>
                                                            </td>
                                                            <td width="150px">
                                                                <asp:TextBox ID="txtNetDisVal" runat="server" CssClass="textComman" Width="128px"
                                                                    Text="0" AutoPostBack="True" OnTextChanged="txtNetDisVal_TextChanged" />
                                                                     <cc1:FilteredTextBoxExtender ID="FilteredTextBoxExtender35" runat="server" Enabled="True"
                                                        TargetControlID="txtNetDisVal" ValidChars="1,2,3,4,5,6,7,8,9,0,.">
                                                    </cc1:FilteredTextBoxExtender>
                                                            
                                                            </td>
                                                        </tr>
                                                    </table>
                                                </td>
                                                <td align='<%= Common.ChangeTDForDefaultLeft()%>'>
                                                    <asp:Label ID="lblNetTaxPar" runat="server" Text="<%$ Resources:Attendance,Net Tax(%)%>"
                                                        CssClass="labelComman"></asp:Label>
                                                </td>
                                                <td>
                                                    :
                                                </td>
                                                <td align='<%= Common.ChangeTDForDefaultLeft()%>'>
                                                    <table>
                                                        <tr>
                                                            <td align='<%= Common.ChangeTDForDefaultLeft()%>' width="1px">
                                                                <asp:TextBox ID="txtNetTaxPar" runat="server" CssClass="textComman" Width="70px"
                                                                    Text="0" AutoPostBack="True" OnTextChanged="txtNetTaxPar_TextChanged" />
                                                                        <cc1:FilteredTextBoxExtender ID="FilteredTextBoxExtender36" runat="server" Enabled="True"
                                                        TargetControlID="txtNetTaxPar" ValidChars="1,2,3,4,5,6,7,8,9,0,.">
                                                    </cc1:FilteredTextBoxExtender>
                                                            
                                                            </td>
                                                            <td align='<%= Common.ChangeTDForDefaultLeft()%>' width="1px">
                                                                <asp:Label ID="lblNetTaxVal" runat="server" Text="<%$ Resources:Attendance,Value %>"
                                                                    CssClass="labelComman"></asp:Label>
                                                            </td>
                                                            <td align='<%= Common.ChangeTDForDefaultLeft()%>' width="1px">
                                                                <asp:TextBox ID="txtNetTaxVal" runat="server" CssClass="textComman" Width="130px"
                                                                    Text="0" AutoPostBack="True" OnTextChanged="txtNetTaxVal_TextChanged" />
                                                                     <cc1:FilteredTextBoxExtender ID="FilteredTextBoxExtender37" runat="server" Enabled="True"
                                                        TargetControlID="txtNetTaxVal" ValidChars="1,2,3,4,5,6,7,8,9,0,.">
                                                    </cc1:FilteredTextBoxExtender>
                                                            </td>
                                                        </tr>
                                                    </table>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td align='<%= Common.ChangeTDForDefaultLeft()%>' width="150px">
                                                    <asp:Label ID="lblNetAmount" runat="server" Text="<%$ Resources:Attendance,Net Amount %>"
                                                        CssClass="labelComman"></asp:Label><asp:TextBox ID="txtNetAmount" runat="server"
                                                            Visible="false" CssClass="textComman" Text="0" />
                                                </td>
                                                <td>
                                                    :
                                                </td>
                                                <td align='<%= Common.ChangeTDForDefaultLeft()%>'>
                                                    <asp:TextBox ID="txtGrandTotal" runat="server" CssClass="textComman">0</asp:TextBox>
                                                     <cc1:FilteredTextBoxExtender ID="FilteredTextBoxExtender38" runat="server" Enabled="True"
                                                        TargetControlID="txtGrandTotal" ValidChars="1,2,3,4,5,6,7,8,9,0,.">
                                                    </cc1:FilteredTextBoxExtender>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td align='<%= Common.ChangeTDForDefaultLeft()%>'>
                                                    <asp:Label ID="Label5" runat="server" Text="<%$ Resources:Attendance,Currency %>"
                                                        CssClass="labelComman"></asp:Label>
                                                </td>
                                                <td>
                                                    :
                                                </td>
                                                <td align='<%= Common.ChangeTDForDefaultLeft()%>'>
                                                    <asp:DropDownList ID="ddlCurrency" runat="server" CssClass="textComman" Width="260px"
                                                        AutoPostBack="True" OnSelectedIndexChanged="ddlCurrency_SelectedIndexChanged">
                                                    </asp:DropDownList>
                                                </td>
                                                <td align='<%= Common.ChangeTDForDefaultLeft()%>' width="150px">
                                                    <asp:Label ID="Label6" runat="server" Text="<%$ Resources:Attendance,Exchange Rate %>"
                                                        CssClass="labelComman"></asp:Label>
                                                </td>
                                                <td>
                                                    :
                                                </td>
                                                <td align='<%= Common.ChangeTDForDefaultLeft()%>'>
                                                    <asp:TextBox ID="txtExchangeRate" runat="server" CssClass="textComman" Text="0" />
                                                     <cc1:FilteredTextBoxExtender ID="FilteredTextBoxExtender39" runat="server" Enabled="True"
                                                        TargetControlID="txtExchangeRate" ValidChars="1,2,3,4,5,6,7,8,9,0,.">
                                                    </cc1:FilteredTextBoxExtender>
                                                </td>
                                            </tr>
                                        </table>
                                        <table width="100%" style="padding-left: 43px">
                                            <tr>
                                                <td colspan="6" align='<%= Common.ChangeTDForDefaultLeft()%>'>
                                                    <cc1:TabContainer ID="TabContainer1" runat="server" ActiveTabIndex="0" Width="97%"
                                                        CssClass="Tab">
                                                        <cc1:TabPanel ID="TabProductSupplier" runat="server" HeaderText="<%$ Resources:Attendance,Expenses %>">
                                                            <ContentTemplate>
                                                                <table style="background-image: url(../Images/bgs.png); height: 100%" width="100%">
                                                                    <tr>
                                                                        <td valign="top">
                                                                            <asp:Panel ID="panPS" runat="server">
                                                                                <table width="100%" style="padding-left: 20px; padding-top: 5px; padding-bottom: 10px">
                                                                                    <tr>
                                                                                        <td align='<%= Common.ChangeTDForDefaultLeft()%>' width="100px">
                                                                                            <asp:Label ID="lblSelectExp" runat="server" CssClass="labelComman" Text="<%$ Resources:Attendance,Select Expenses %>" />
                                                                                        </td>
                                                                                        <td width="1px">
                                                                                            :
                                                                                        </td>
                                                                                        <td align='<%= Common.ChangeTDForDefaultLeft()%>' width="270PX">
                                                                                            <asp:DropDownList ID="ddlExpense" runat="server" CssClass="textComman">
                                                                                            </asp:DropDownList>
                                                                                        </td>
                                                                                        <td align='<%= Common.ChangeTDForDefaultLeft()%>' width="150px">
                                                                                            <asp:Label ID="lblFCExpAmount" runat="server" Text="FCExpAmount" CssClass="labelComman"></asp:Label>
                                                                                        </td>
                                                                                        <td>
                                                                                            :
                                                                                        </td>
                                                                                        <td align='<%= Common.ChangeTDForDefaultLeft()%>'>
                                                                                            <asp:TextBox ID="txtFCExpAmount" runat="server" CssClass="textComman" Width="180px">0</asp:TextBox>
                                                                                                <cc1:FilteredTextBoxExtender ID="FilteredTextBoxExtender40" runat="server" Enabled="True"
                                                        TargetControlID="txtFCExpAmount" ValidChars="1,2,3,4,5,6,7,8,9,0,.">
                                                    </cc1:FilteredTextBoxExtender>
                                                                                        </td>
                                                                                    </tr>
                                                                                    <tr>
                                                                                        <td align='<%= Common.ChangeTDForDefaultLeft()%>'>
                                                                                            <asp:Label ID="Label3" runat="server" Text="<%$ Resources:Attendance,Currency %>"
                                                                                                CssClass="labelComman"></asp:Label>
                                                                                        </td>
                                                                                        <td>
                                                                                            :
                                                                                        </td>
                                                                                        <td align='<%= Common.ChangeTDForDefaultLeft()%>' width="270PX">
                                                                                            <asp:DropDownList ID="ddlExpCurrency" runat="server" CssClass="textComman" AutoPostBack="True"
                                                                                                OnSelectedIndexChanged="ddlExpCurrency_SelectedIndexChanged">
                                                                                            </asp:DropDownList>
                                                                                        </td>
                                                                                        <td align='<%= Common.ChangeTDForDefaultLeft()%>' width="150px">
                                                                                            <asp:Label ID="Label13" runat="server" Text="<%$ Resources:Attendance,Exchange Rate %>"
                                                                                                CssClass="labelComman"></asp:Label>
                                                                                        </td>
                                                                                        <td>
                                                                                            :
                                                                                        </td>
                                                                                        <td align='<%= Common.ChangeTDForDefaultLeft()%>'>
                                                                                            <asp:TextBox ID="txtExpExchangeRate" runat="server" CssClass="textComman" Width="180px">0</asp:TextBox>
                                                                                               <cc1:FilteredTextBoxExtender ID="FilteredTextBoxExtender41" runat="server" Enabled="True"
                                                        TargetControlID="txtExpExchangeRate" ValidChars="1,2,3,4,5,6,7,8,9,0,.">
                                                    </cc1:FilteredTextBoxExtender>
                                                                                        </td>
                                                                                    </tr>
                                                                                    <tr>
                                                                                        <td align='<%= Common.ChangeTDForDefaultLeft()%>' width="150px">
                                                                                            <asp:Label ID="lblExpCharges" runat="server" CssClass="labelComman" Text="<%$ Resources:Attendance,Expenses Charges %>" />
                                                                                        </td>
                                                                                        <td width="1px">
                                                                                            :
                                                                                        </td>
                                                                                        <td align='<%= Common.ChangeTDForDefaultLeft()%>'>
                                                                                            <asp:TextBox ID="txtExpCharges" runat="server" Width="240px" CssClass="textComman">0</asp:TextBox>
                                                                                                <cc1:FilteredTextBoxExtender ID="FilteredTextBoxExtender42" runat="server" Enabled="True"
                                                        TargetControlID="txtExpCharges" ValidChars="1,2,3,4,5,6,7,8,9,0,.">
                                                    </cc1:FilteredTextBoxExtender>
                                                                                        </td>
                                                                                        <td>
                                                                                            <asp:ImageButton runat="server" CausesValidation="False" ImageUrl="~/Images/add.png"
                                                                                                Height="29px" ToolTip="<%$ Resources:Attendance,Add %>" Width="35px" ID="IbtnAddExpenses"
                                                                                                OnClick="IbtnAddExpenses_Click"></asp:ImageButton>
                                                                                            <asp:HiddenField ID="hdnProductExpenses" runat="server" Value="0" />
                                                                                        </td>
                                                                                    </tr>
                                                                                    <tr>
                                                                                        <td colspan="6" align='<%= Common.ChangeTDForDefaultLeft()%>' style="padding-top: 20PX">
                                                                                            <asp:GridView ID="GridExpenses" runat="server" AllowPaging="True" AutoGenerateColumns="False"
                                                                                                ShowFooter="True" BorderStyle="Solid" Width="100%" CssClass="grid" PageSize="5"
                                                                                                OnPageIndexChanging="GridExpenses_PageIndexChanging">
                                                                                                <AlternatingRowStyle CssClass="InvgridAltRow" />
                                                                                                <Columns>
                                                                                                    <asp:TemplateField HeaderText="<%$ Resources:Attendance,Delete %>">
                                                                                                        <ItemTemplate>
                                                                                                            <asp:ImageButton ID="IbtnDeleteExp" runat="server" CausesValidation="False" CommandArgument='<%# Eval("Expense_Id") %>'
                                                                                                                ImageUrl="~/Images/Erase.png" Width="16px" ToolTip="<%$ Resources:Attendance,Delete %>"
                                                                                                                OnCommand="IbtnDeleteExp_Command" Visible="false" />
                                                                                                        </ItemTemplate>
                                                                                                        <FooterStyle BorderStyle="None" />
                                                                                                        <ItemStyle HorizontalAlign="Center" CssClass="grid" />
                                                                                                    </asp:TemplateField>
                                                                                                    <asp:TemplateField HeaderText="<%$ Resources:Attendance,Expenses %>" SortExpression="Expense_Id">
                                                                                                        <ItemTemplate>
                                                                                                            <asp:Label ID="lblgvExpense_Id" runat="server" Text='<%# GetExpName(Eval("Expense_Id").ToString()) %>' />
                                                                                                        </ItemTemplate>
                                                                                                        <FooterStyle BorderStyle="None" />
                                                                                                        <ItemStyle CssClass="grid" />
                                                                                                    </asp:TemplateField>
                                                                                                    <asp:TemplateField HeaderText="<%$ Resources:Attendance,Currency %>" SortExpression="ProductSupplierCode">
                                                                                                        <ItemTemplate>
                                                                                                            <asp:Label ID="lblgvExpCurrencyID" runat="server" Text='<%# CurrencyName(Eval("ExpCurrencyID").ToString()) %>' /></ItemTemplate>
                                                                                                        <FooterStyle BorderStyle="None" />
                                                                                                        <ItemStyle CssClass="grid" />
                                                                                                    </asp:TemplateField>
                                                                                                    <asp:TemplateField HeaderText="FC Exchange Amount" SortExpression="ProductSupplierCode">
                                                                                                        <ItemTemplate>
                                                                                                            <asp:Label ID="lblgvFCExchangeAmount" runat="server" Text='<%# Eval("FCExpAmount") %>' /></ItemTemplate>
                                                                                                        <FooterStyle BorderStyle="None" />
                                                                                                        <ItemStyle CssClass="grid" />
                                                                                                    </asp:TemplateField>
                                                                                                    <asp:TemplateField HeaderText="<%$ Resources:Attendance,Exchange Rate %>" SortExpression="ProductSupplierCode">
                                                                                                        <ItemTemplate>
                                                                                                            <asp:Label ID="lblgvExpExchangeRate" runat="server" Text='<%# Eval("ExpExchangeRate") %>' /></ItemTemplate>
                                                                                                        <FooterTemplate>
                                                                                                            <asp:Label ID="lbltotExp" runat="server" Font-Bold="true" Text="Total Expenses " /><b>:</b></ItemTemplate>
                                                                                                        </FooterTemplate>
                                                                                                        <FooterStyle BorderStyle="None" HorizontalAlign="Right" />
                                                                                                        <ItemStyle CssClass="grid" />
                                                                                                    </asp:TemplateField>
                                                                                                    <asp:TemplateField HeaderText="<%$ Resources:Attendance,Expenses Charges %>" SortExpression="Exp_Charges">
                                                                                                        <ItemTemplate>
                                                                                                            <asp:Label ID="lblgvExp_Charges" runat="server" Text='<%#Eval("Exp_Charges") %>' /></ItemTemplate>
                                                                                                        <FooterTemplate>
                                                                                                            <asp:Label ID="txttotExp" runat="server" Font-Bold="true" Text="0" /></ItemTemplate>
                                                                                                        </FooterTemplate>
                                                                                                        <FooterStyle BorderStyle="None" />
                                                                                                        <ItemStyle CssClass="grid" />
                                                                                                    </asp:TemplateField>
                                                                                                </Columns>
                                                                                                <FooterStyle CssClass="Invgridheader" />
                                                                                                <HeaderStyle CssClass="Invgridheader" BorderStyle="Solid" BorderColor="#6E6E6E" BorderWidth="1px" />
                                                                                                <PagerStyle CssClass="Invgridheader" />
                                                                                                <RowStyle CssClass="Invgridrow" HorizontalAlign="Center" />
                                                                                            </asp:GridView>
                                                                                            <asp:HiddenField ID="hdnfPSC" runat="server" />
                                                                                        </td>
                                                                                    </tr>
                                                                                </table>
                                                                            </asp:Panel>
                                                                        </td>
                                                                    </tr>
                                                                </table>
                                                            </ContentTemplate>
                                                        </cc1:TabPanel>
                                                        <cc1:TabPanel ID="TabProductPaymentMode" runat="server" HeaderText="<%$ Resources:Attendance,Payment Mode %>">
                                                            <ContentTemplate>
                                                                <table style="background-image: url(../Images/bgs.png); height: 100%" width="100%">
                                                                    <tr>
                                                                        <td valign="top">
                                                                            <asp:Panel ID="Panel2" runat="server">
                                                                                <table style="padding-left: 20px; padding-top: 5px; padding-bottom: 10px">
                                                                                    <tr>
                                                                                        <td align='<%= Common.ChangeTDForDefaultLeft()%>' width="170px">
                                                                                            <asp:Label ID="Label4" runat="server" Text="<%$ Resources:Attendance,Payment Mode %>"
                                                                                                CssClass="labelComman"></asp:Label>
                                                                                        </td>
                                                                                        <td width="1px">
                                                                                            :
                                                                                        </td>
                                                                                        <td align='<%= Common.ChangeTDForDefaultLeft()%>' width="270px">
                                                                                            <asp:DropDownList ID="ddlPaymentMode" runat="server" CssClass="textComman" Width="260px">
                                                                                                <asp:ListItem Text="Cash" Value="1"></asp:ListItem>
                                                                                                <asp:ListItem Text="Credit" Value="2"></asp:ListItem>
                                                                                                <asp:ListItem Text="Card" Value="2"></asp:ListItem>
                                                                                            </asp:DropDownList>
                                                                                        </td>
                                                                                    </tr>
                                                                                </table>
                                                                            </asp:Panel>
                                                                        </td>
                                                                    </tr>
                                                                </table>
                                                            </ContentTemplate>
                                                        </cc1:TabPanel>
                                                    </cc1:TabContainer>
                                                </td>
                                            </tr>
                                        </table>
                                        <table width="100%" style="padding-left: 43px">
                                            <tr>
                                                <td align='<%= Common.ChangeTDForDefaultLeft()%>' width="170px">
                                                    <asp:Label ID="lblCostingRate" runat="server" Text="<%$ Resources:Attendance,Costing Rate%>"
                                                        CssClass="labelComman"></asp:Label>
                                                </td>
                                                <td width="1px">
                                                    :
                                                </td>
                                                <td align='<%= Common.ChangeTDForDefaultLeft()%>' width="270px">
                                                    <asp:TextBox ID="txtCostingRate" runat="server" CssClass="textComman">0</asp:TextBox>
                                                        <cc1:FilteredTextBoxExtender ID="FilteredTextBoxExtender43" runat="server" Enabled="True"
                                                        TargetControlID="txtCostingRate" ValidChars="1,2,3,4,5,6,7,8,9,0,.">
                                                    </cc1:FilteredTextBoxExtender>
                                                   
                                                </td>
                                                <td align='<%= Common.ChangeTDForDefaultLeft()%>'>
                                                    <asp:TextBox ID="txtOtherCharges" runat="server" CssClass="textComman" Visible="False">0</asp:TextBox>
                                                     <cc1:FilteredTextBoxExtender ID="FilteredTextBoxExtender44" runat="server" Enabled="True"
                                                        TargetControlID="txtOtherCharges" ValidChars="1,2,3,4,5,6,7,8,9,0,.">
                                                    </cc1:FilteredTextBoxExtender>
                                                </td>
                                            </tr>
                                        </table>
                                        <table width="100%" style="padding-left: 43px">
                                            <tr>
                                                <td align='<%= Common.ChangeTDForDefaultLeft()%>' width="170px">
                                                    <asp:Label ID="Label7" runat="server" Text="<%$ Resources:Attendance,Remark %>" CssClass="labelComman"></asp:Label>
                                                </td>
                                                <td width="1px">
                                                    :
                                                </td>
                                                <td align='<%= Common.ChangeTDForDefaultLeft()%>' colspan="4">
                                                    <asp:TextBox ID="txRemark" runat="server" CssClass="textComman" Width="687px" TextMode="MultiLine"
                                                        Height="50px" />
                                                </td>
                                            </tr>
                                            <tr>
                                                <td align='<%= Common.ChangeTDForDefaultLeft()%>'>
                                                    <asp:Label ID="Label1" runat="server" Text="<%$ Resources:Attendance,Post %>" CssClass="labelComman"></asp:Label>
                                                </td>
                                                <td>
                                                    :
                                                </td>
                                                <td align='<%= Common.ChangeTDForDefaultLeft()%>' colspan="4">
                                                    <asp:RadioButton ID="RdoOpen" runat="server" CssClass="labelComman" GroupName="a"
                                                        Text="<%$ Resources:Attendance,Open %>" />
                                                    <asp:RadioButton ID="RdoClose" runat="server" CssClass="labelComman" GroupName="a"
                                                        Text="<%$ Resources:Attendance,Close %>" />
                                                </td>
                                            </tr>
                                            <tr>
                                                <td colspan="6" align="center">
                                                    <table>
                                                        <tr>
                                                            <td width="90px">
                                                                <asp:Panel ID="Panel1" runat="server" DefaultButton="btnSave">
                                                                    <asp:Button ID="btnSave" runat="server" Text="<%$ Resources:Attendance,Save %>" CssClass="buttonCommman"
                                                                        OnClick="btnSave_Click" />
                                                                </asp:Panel>
                                                            </td>
                                                            <td width="90px">
                                                                <asp:Panel ID="Panel3" runat="server" DefaultButton="btnReset">
                                                                    <asp:Button ID="btnReset" runat="server" Text="<%$ Resources:Attendance,Reset %>"
                                                                        CssClass="buttonCommman" OnClick="btnReset_Click" />
                                                                </asp:Panel>
                                                            </td>
                                                            <td width="90px">
                                                                <asp:Panel ID="Panel4" runat="server" DefaultButton="btnCancel">
                                                                    <asp:Button ID="btnCancel" runat="server" Text="<%$ Resources:Attendance,Cancel %>"
                                                                        CssClass="buttonCommman" OnClick="btnCancel_Click" />
                                                                </asp:Panel>
                                                            </td>
                                                        </tr>
                                                    </table>
                                                </td>
                                            </tr>
                                        </table>
                                    </asp:Panel>
                                    <asp:Panel ID="PnlBin" runat="server">
                                    </asp:Panel>
                                </td>
                            </tr>
                        </table>
                    </td>
                </tr>
            </table>
            <asp:Panel ID="pnlProduct1" runat="server" class="MsgOverlayAddress" Visible="False">
                <asp:Panel ID="pnlProduct2" runat="server" class="MsgPopUpPanelAddress" Visible="False">
                    <asp:Panel ID="pnlProduct3" runat="server" Style="width: 100%; height: 100%; text-align: center;">
                        <table width="100%" style="background-image: url(../Images/bg_repeat.jpg); background-repeat: repeat;">
                            <tr>
                                <td align="<%= Common.ChangeTDForDefaultLeft()%>">
                                    <asp:Label ID="lblProductHeader" runat="server" Font-Size="14px" Font-Bold="true"
                                        CssClass="labelComman" Text="<%$ Resources:Attendance, Product Setup %>"></asp:Label>
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
                                    <asp:Label ID="lblProductName" runat="server" CssClass="labelComman" Text="<%$ Resources:Attendance,Product Name %>" />
                                </td>
                                <td align="center">
                                    :
                                </td>
                                <td align='<%= Common.ChangeTDForDefaultLeft()%>' colspan="4">
                                    <asp:TextBox ID="txtProductName" runat="server" BackColor="#e3e3e3" CssClass="textComman" AutoPostBack="True"
                                        OnTextChanged="txtProductName_TextChanged" Width="660PX" />
                                    <cc1:AutoCompleteExtender ID="AutoCompleteExtender3" runat="server" CompletionInterval="100"
                                        DelimiterCharacters="" Enabled="True" MinimumPrefixLength="1" ServiceMethod="GetCompletionListProductName"
                                        ServicePath="" TargetControlID="txtProductName" UseContextKey="True" CompletionListCssClass="completionList"
                                        CompletionListItemCssClass="listItem" CompletionListHighlightedItemCssClass="itemHighlighted">
                                    </cc1:AutoCompleteExtender>
                                </td>
                            </tr>
                            <tr>
                                <td align='<%= Common.ChangeTDForDefaultLeft()%>'>
                                    <asp:Label ID="lblUnit" runat="server" CssClass="labelComman" Text="<%$ Resources:Attendance,Unit %>" />
                                </td>
                                <td align="center">
                                    :
                                </td>
                                <td align='<%= Common.ChangeTDForDefaultLeft()%>'>
                                    <asp:DropDownList ID="ddlUnit" runat="server" CssClass="textComman" Width="260PX" />
                                </td>
                                <td align='<%= Common.ChangeTDForDefaultLeft()%>'>
                                    <asp:Label ID="lblUnitCost" runat="server" CssClass="labelComman" Text="<%$ Resources:Attendance,Unit Cost %>" />
                                </td>
                                <td align="center">
                                    :
                                </td>
                                <td align='<%= Common.ChangeTDForDefaultLeft()%>'>
                                    <asp:TextBox ID="txtUnitCost" runat="server" CssClass="textComman" AutoPostBack="True"
                                        OnTextChanged="txtUnitCost_TextChanged">0</asp:TextBox>
                                          <cc1:FilteredTextBoxExtender ID="FilteredTextBoxExtender45" runat="server" Enabled="True"
                                                        TargetControlID="txtUnitCost" ValidChars="1,2,3,4,5,6,7,8,9,0,.">
                                                    </cc1:FilteredTextBoxExtender>
                                </td>
                            </tr>
                            <tr>
                                <td align='<%= Common.ChangeTDForDefaultLeft()%>'>
                                    <asp:Label ID="Label2" runat="server" CssClass="labelComman" Text="<%$ Resources:Attendance,Received Quantity %>" />
                                </td>
                                <td align="center">
                                    :
                                </td>
                                <td align='<%= Common.ChangeTDForDefaultLeft()%>'>
                                    <asp:TextBox ID="txtReceivedQty" runat="server" CssClass="textComman" AutoPostBack="True"
                                        OnTextChanged="txtReceivedQty_TextChanged">0</asp:TextBox>
                                          <cc1:FilteredTextBoxExtender ID="FilteredTextBoxExtender46" runat="server" Enabled="True"
                                                        TargetControlID="txtReceivedQty" ValidChars="1,2,3,4,5,6,7,8,9,0,.">
                                                    </cc1:FilteredTextBoxExtender>
                                </td>
                                <td align='<%= Common.ChangeTDForDefaultLeft()%>'>
                                    <asp:Label ID="Label25" runat="server" CssClass="labelComman" Text="<%$ Resources:Attendance,Free Quantity %>" />
                                </td>
                                <td align="center">
                                    :
                                </td>
                                <td align='<%= Common.ChangeTDForDefaultLeft()%>'>
                                    <asp:TextBox ID="txtfreeQty" runat="server" CssClass="textComman" AutoPostBack="True"
                                        OnTextChanged="txtfreeQty_TextChanged">0</asp:TextBox>
                                        <cc1:FilteredTextBoxExtender ID="FilteredTextBoxExtender47" runat="server" Enabled="True"
                                                        TargetControlID="txtfreeQty" ValidChars="1,2,3,4,5,6,7,8,9,0,.">
                                                    </cc1:FilteredTextBoxExtender>
                                </td>
                            </tr>
                            <tr>
                                <td align='<%= Common.ChangeTDForDefaultLeft()%>'>
                                    <asp:Label ID="lblAmmount" runat="server" CssClass="labelComman" Text="<%$ Resources:Attendance,Amount %>" />
                                </td>
                                <td align="center">
                                    :
                                </td>
                                <td align='<%= Common.ChangeTDForDefaultLeft()%>'>
                                    <asp:TextBox ID="txtAmount" runat="server" CssClass="textComman" AutoPostBack="True"
                                        ReadOnly="True">0</asp:TextBox>
                                          <cc1:FilteredTextBoxExtender ID="FilteredTextBoxExtender48" runat="server" Enabled="True"
                                                        TargetControlID="txtAmount" ValidChars="1,2,3,4,5,6,7,8,9,0,.">
                                                    </cc1:FilteredTextBoxExtender>
                                </td>
                            </tr>
                            <tr>
                                <td align='<%= Common.ChangeTDForDefaultLeft()%>'>
                                    <asp:Label ID="lblPDis" runat="server" CssClass="labelComman" Text="<%$ Resources:Attendance,Discount %>" />
                                    <asp:Label ID="Label10" runat="server" Text="(%)" CssClass="labelComman"></asp:Label>
                                </td>
                                <td align="center">
                                    :
                                </td>
                                <td align='<%= Common.ChangeTDForDefaultLeft()%>'>
                                    <table>
                                        <tr>
                                            <td align='<%= Common.ChangeTDForDefaultLeft()%>'>
                                                <asp:TextBox ID="txtPDisPer" runat="server" CssClass="textComman" Width="70px" AutoPostBack="True"
                                                    OnTextChanged="txtPDisPer_TextChanged">0</asp:TextBox>
                                                      <cc1:FilteredTextBoxExtender ID="FilteredTextBoxExtender49" runat="server" Enabled="True"
                                                        TargetControlID="txtPDisPer" ValidChars="1,2,3,4,5,6,7,8,9,0,.">
                                                    </cc1:FilteredTextBoxExtender>
                                            </td>
                                            <td align='<%= Common.ChangeTDForDefaultLeft()%>' width="1px">
                                                <asp:Label ID="Label11" runat="server" Text="<%$ Resources:Attendance,Value %>" CssClass="labelComman"></asp:Label>
                                            </td>
                                            <td width="150px">
                                                <asp:TextBox ID="txtPDisValue" runat="server" CssClass="textComman" Width="130px"
                                                    Text="0" AutoPostBack="True" OnTextChanged="txtPDisValue_TextChanged" />
                                                      <cc1:FilteredTextBoxExtender ID="FilteredTextBoxExtender50" runat="server" Enabled="True"
                                                        TargetControlID="txtPDisValue" ValidChars="1,2,3,4,5,6,7,8,9,0,.">
                                                    </cc1:FilteredTextBoxExtender>
                                           
                                                   
                                            </td>
                                        </tr>
                                    </table>
                                </td>
                                <td align='<%= Common.ChangeTDForDefaultLeft()%>'>
                                    <asp:Label ID="Label8" runat="server" CssClass="labelComman" Text="<%$ Resources:Attendance,Tax %>" />
                                    <asp:Label ID="Label12" runat="server" Text="(%)" CssClass="labelComman"></asp:Label>
                                </td>
                                <td align="center">
                                    :
                                </td>
                                <td align='<%= Common.ChangeTDForDefaultLeft()%>'>
                                    <table>
                                        <tr>
                                            <td align='<%= Common.ChangeTDForDefaultLeft()%>'>
                                                <asp:TextBox ID="txtPTaxPer" runat="server" CssClass="textComman" Width="70px" AutoPostBack="True"
                                                    OnTextChanged="txtPTaxPer_TextChanged">0</asp:TextBox>
                                                       <cc1:FilteredTextBoxExtender ID="FilteredTextBoxExtender51" runat="server" Enabled="True"
                                                        TargetControlID="txtPTaxPer" ValidChars="1,2,3,4,5,6,7,8,9,0,.">
                                                    </cc1:FilteredTextBoxExtender>
                                           
                                            </td>
                                            <td align='<%= Common.ChangeTDForDefaultLeft()%>' width="1px">
                                                <asp:Label ID="Label9" runat="server" Text="<%$ Resources:Attendance,Value %>" CssClass="labelComman"></asp:Label>
                                            </td>
                                            <td width="150px">
                                                <asp:TextBox ID="txtPTaxVal" runat="server" CssClass="textComman" Width="130px" Text="0"
                                                    AutoPostBack="True" OnTextChanged="txtPTaxVal_TextChanged" />
                                                     <cc1:FilteredTextBoxExtender ID="FilteredTextBoxExtender52" runat="server" Enabled="True"
                                                        TargetControlID="txtPTaxVal" ValidChars="1,2,3,4,5,6,7,8,9,0,.">
                                                    </cc1:FilteredTextBoxExtender>
                                                    
                                            </td>
                                        </tr>
                                    </table>
                                </td>
                            </tr>
                            <tr visible="false">
                                <td align='<%= Common.ChangeTDForDefaultLeft()%>'>
                                    <asp:TextBox ID="txtPAfterTaxAmount" runat="server" CssClass="textComman" AutoPostBack="True"
                                        Visible="false" ReadOnly="True">0</asp:TextBox>
                                         <cc1:FilteredTextBoxExtender ID="FilteredTextBoxExtender53" runat="server" Enabled="True"
                                                        TargetControlID="txtPAfterTaxAmount" ValidChars="1,2,3,4,5,6,7,8,9,0,.">
                                                    </cc1:FilteredTextBoxExtender>
                                </td>
                            </tr>
                            <tr>
                                <td align='<%= Common.ChangeTDForDefaultLeft()%>'>
                                    <asp:Label ID="lblPTotalAmount" runat="server" CssClass="labelComman" Text="<%$ Resources:Attendance,Total Amount %>" />
                                </td>
                                <td align="center">
                                    :
                                </td>
                                <td align='<%= Common.ChangeTDForDefaultLeft()%>'>
                                    <asp:TextBox ID="txtTotalAmount" runat="server" CssClass="textComman" ReadOnly="True" />
                                     <cc1:FilteredTextBoxExtender ID="FilteredTextBoxExtender54" runat="server" Enabled="True"
                                                        TargetControlID="txtTotalAmount" ValidChars="1,2,3,4,5,6,7,8,9,0,.">
                                                    </cc1:FilteredTextBoxExtender>
                                </td>
                            </tr>
                            <tr>
                                <td align='<%= Common.ChangeTDForDefaultLeft()%>'>
                                    <asp:Label ID="lblPDescription" runat="server" CssClass="labelComman" Text="<%$ Resources:Attendance,Product Description %>" />
                                </td>
                                <td align="center">
                                    :
                                </td>
                                <td align='<%= Common.ChangeTDForDefaultLeft()%>' colspan="4">
                                    <%--<asp:TextBox ID="txtPDescription" Width="660px" TextMode="MultiLine" runat="server" CssClass="textComman" />--%> 
                                     <asp:Panel ID="pnlPDescription" runat="server" Width="615px" Height="100px" CssClass="textComman"
                                                                BorderColor="#8ca7c1" BackColor="#ffffff" ScrollBars="Vertical" >
                                                                <asp:Literal ID="txtPDescription" runat="server"></asp:Literal>
                                                            </asp:Panel>
                                                          
                               </td>
                            </tr>
                            <tr>
                                <td colspan="6" align="center" style="padding-left: 10px">
                                    <table>
                                        <tr>
                                            <td width="90px">
                                                <asp:Panel ID="Panel6" runat="server" DefaultButton="btnProductSave">
                                                    <asp:Button ID="btnProductSave" runat="server" Text="<%$ Resources:Attendance,Save %>"
                                                        CssClass="buttonCommman" OnClick="btnProductSave_Click" />
                                                </asp:Panel>
                                            </td>
                                            <td width="90px">
                                                <asp:Panel ID="Panel7" runat="server" DefaultButton="btnProductCancel">
                                                    <asp:Button ID="btnProductCancel" runat="server" CssClass="buttonCommman" Text="<%$ Resources:Attendance,Cancel %>"
                                                        CausesValidation="False" OnClick="btnProductCancel_Click" />
                                                </asp:Panel>
                                            </td>
                                        </tr>
                                    </table>
                                    <asp:HiddenField ID="hidProduct" runat="server" />
                                </td>
                            </tr>
                        </table>
                    </asp:Panel>
                </asp:Panel>
            </asp:Panel>
            <asp:Panel ID="pnlAddOrderProduct" runat="server" class="MsgOverlayAddress" Visible="False">
                <center>
                    <asp:Panel ID="pnlAddOrderProduct1" runat="server" class="MsgPopUpPanelAddress" Visible="False">
                        <asp:Panel ID="Panel9" runat="server" Style="width: 100%; height: 100%; text-align: center;">
                            <table width="100%" style="background-image: url(../Images/bg_repeat.jpg); background-repeat: repeat;">
                                <tr>
                                    <td align="<%= Common.ChangeTDForDefaultLeft()%>">
                                        <asp:Label ID="Label14" runat="server" Font-Size="14px" Font-Bold="true" CssClass="labelComman"
                                            Text="<%$ Resources:Attendance, Product Setup %>"></asp:Label>
                                    </td>
                                    <td align="right">
                                        <asp:ImageButton ID="ImageButton1" runat="server" ImageUrl="~/Images/close.png" CausesValidation="False"
                                            OnClick="btnClosePanel_Click" Height="20px" Width="20px" />
                                    </td>
                                </tr>
                            </table>
                            <asp:GridView ID="gvaddOrderProduct" PageSize="<%# SystemParameter.GetPageSize() %>"
                                runat="server" ShowHeader="false" AutoGenerateColumns="False" Width="100%" AllowPaging="True"
                                AllowSorting="True" CssClass="grid">
                                <Columns>
                                    <asp:TemplateField HeaderText="<%$ Resources:Attendance,Purchase Order No %>" SortExpression="PONo">
                                        <ItemTemplate>
                                            <table width="100%" cellpadding="0">
                                                <tr>
                                                    <td width="1px" style="border-right: solid 1px #c4cc2">
                                                        <asp:CheckBox ID="ChkgvaddOrderProductPoId" runat="server" AutoPostBack="True" OnCheckedChanged="ChkgvaddOrderProductPoId_CheckedChanged" />
                                                        <asp:Label ID="lblPoId" runat="server" Text='<%# Eval("TransId") %>' Visible="false"></asp:Label>
                                                    </td>
                                                    <td width="150px">
                                                        <asp:Label ID="lblPONo" runat="server" Font-Size="14px" Text="<%$ Resources:Attendance,Purchase Order No %>"></asp:Label>
                                                    </td>
                                                    <td>
                                                        :
                                                    </td>
                                                    <td width="180px">
                                                        <asp:Label ID="lbPONo" runat="server" Font-Size="14px" Font-Bold="true" Text='<%# Eval("PONo") %>'></asp:Label>
                                                    </td>
                                                    <td width="150px">
                                                        <asp:Label ID="lbPODate" runat="server" Font-Size="14px" Text="<%$ Resources:Attendance,Purchase Order Date %>"></asp:Label>
                                                    </td>
                                                    <td>
                                                        :
                                                    </td>
                                                    <td>
                                                        <asp:Label ID="lblPODate" runat="server" Font-Size="14px" Font-Bold="true" Text='<%# GetDateFormat(Eval("PODate").ToString()) %>'></asp:Label>
                                                    </td>
                                                </tr>
                                            </table>
                                            <asp:GridView ID="gvProduct" PageSize="<%# SystemParameter.GetPageSize() %>" runat="server"
                                                AutoGenerateColumns="False" Width="100%" ShowFooter="true" CssClass="grid" OnRowCreated="gvProduct_RowCreated">
                                                <Columns>
                                                    <asp:TemplateField HeaderText="<%$ Resources:Attendance,Serial No %>">
                                                        <ItemTemplate>
                                                            <%#Container.DataItemIndex+1 %>
                                                            <asp:Label ID="lblTransId" Visible="false" runat="server" Text='<%# Eval("Trans_Id") %>'></asp:Label>
                                                        </ItemTemplate>
                                                        <FooterTemplate>
                                                            <asp:Label ID="lblTotalQuantity" runat="server" Text="<%$ Resources:Attendance,Total %>"
                                                                CssClass="labelComman"></asp:Label>
                                                        </FooterTemplate>
                                                        <ItemStyle CssClass="grid" />
                                                        <FooterStyle BorderStyle="None" />
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="<%$ Resources:Attendance,Product Name %>" ItemStyle-Width="35%">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblProductId" Visible="false" runat="server" Text='<%# Eval("Product_Id") %>'></asp:Label>
                                                            <asp:Label ID="lblProductName" runat="server" Text='<%# ProductName(Eval("Product_Id").ToString()) %>'></asp:Label>
                                                        </ItemTemplate>
                                                        <ItemStyle CssClass="grid" />
                                                        <FooterStyle BorderStyle="None" />
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="<%$ Resources:Attendance,Name %>">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblUnitId" Visible="false" runat="server" Text='<%# Eval("UnitId") %>'></asp:Label>
                                                            <asp:Label ID="lblUnit" runat="server" Text='<%# UnitName(Eval("UnitId").ToString()) %>'></asp:Label>
                                                        </ItemTemplate>
                                                        <ItemStyle CssClass="grid" />
                                                        <FooterStyle BorderStyle="None" />
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="<%$ Resources:Attendance,Cost %>">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblUnitRate" runat="server" Text='<%# Eval("UnitCost") %>'></asp:Label>
                                                        </ItemTemplate>
                                                        <ItemStyle CssClass="grid" />
                                                        <FooterStyle BorderStyle="None" />
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="<%$ Resources:Attendance,Order %>">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblReqQty" runat="server" Text='<%# Eval("OrderQty") %>'></asp:Label>
                                                        </ItemTemplate>
                                                        <ItemStyle CssClass="grid" />
                                                        <FooterStyle BorderStyle="None" />
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="<%$ Resources:Attendance,Free %>">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblFreeQty" runat="server" Text='<%# Eval("FreeQty") %>'></asp:Label>
                                                        </ItemTemplate>
                                                        <ItemStyle CssClass="grid" />
                                                        <FooterStyle BorderStyle="None" />
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="<%$ Resources:Attendance,Remain %>">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblRemainQty" runat="server" Text='<%# Eval("RemainQty") %>'></asp:Label>
                                                        </ItemTemplate>
                                                        <ItemStyle CssClass="grid" />
                                                        <FooterStyle BorderStyle="None" />
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="<%$ Resources:Attendance,Received %>">
                                                        <ItemTemplate>
                                                            <asp:TextBox ID="txtRecQty" runat="server" CssClass="textCommanSearch" Width="50px"
                                                                Height="10px" Text="0" AutoPostBack="True" OnTextChanged="txtRecQty_TextChanged"></asp:TextBox>
                                                                 <cc1:FilteredTextBoxExtender ID="FilteredTextBoxExtender55" runat="server" Enabled="True"
                                                        TargetControlID="txtRecQty" ValidChars="1,2,3,4,5,6,7,8,9,0,.">
                                                    </cc1:FilteredTextBoxExtender>
                                                        </ItemTemplate>
                                                        <ItemStyle CssClass="grid" />
                                                        <FooterStyle BorderStyle="None" HorizontalAlign="Left" />
                                                        <FooterTemplate>
                                                            <asp:Label ID="txtTotalQuantity" runat="server" Text="0" CssClass="labelComman"></asp:Label>
                                                        </FooterTemplate>
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="<%$ Resources:Attendance,Discount %>">
                                                        <ItemTemplate>
                                                            <asp:TextBox ID="txtDiscount" runat="server" CssClass="textCommanSearch" Width="50px"
                                                                Height="10px" Text="0" AutoPostBack="True" OnTextChanged="txtRecQty_TextChanged"></asp:TextBox>
                                                                 <cc1:FilteredTextBoxExtender ID="FilteredTextBoxExtender56" runat="server" Enabled="True"
                                                        TargetControlID="txtDiscount" ValidChars="1,2,3,4,5,6,7,8,9,0,.">
                                                    </cc1:FilteredTextBoxExtender>
                                                       
                                                        </ItemTemplate>
                                                        <FooterStyle BorderStyle="None" />
                                                        <ItemStyle CssClass="grid" />
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="<%$ Resources:Attendance,Tax %>">
                                                        <ItemTemplate>
                                                            <asp:TextBox ID="txtTax" runat="server" CssClass="textCommanSearch" Width="50px"
                                                                Height="10px" Text="0" AutoPostBack="True" OnTextChanged="txtRecQty_TextChanged"></asp:TextBox>
                                                                  <cc1:FilteredTextBoxExtender ID="FilteredTextBoxExtender57" runat="server" Enabled="True"
                                                        TargetControlID="txtTax" ValidChars="1,2,3,4,5,6,7,8,9,0,.">
                                                    </cc1:FilteredTextBoxExtender>
                                                       
                                                        </ItemTemplate>
                                                        <ItemStyle CssClass="grid" />
                                                        <FooterStyle BorderStyle="None" />
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="<%$ Resources:Attendance,Amount %>">
                                                        <ItemTemplate>
                                                            <asp:TextBox ID="txtAmount" runat="server" CssClass="textCommanSearch" Width="80px"
                                                                Height="10px" Text="0" AutoPostBack="True" OnTextChanged="txtRecQty_TextChanged"></asp:TextBox>
                                                                 <cc1:FilteredTextBoxExtender ID="FilteredTextBoxExtender58" runat="server" Enabled="True"
                                                        TargetControlID="txtAmount" ValidChars="1,2,3,4,5,6,7,8,9,0,.">
                                                    </cc1:FilteredTextBoxExtender>
                                                        </ItemTemplate>
                                                        <FooterTemplate>
                                                            <asp:Label ID="txtTotalAmount" runat="server" Font-Bold="true" Text="0" CssClass="labelComman"></asp:Label>
                                                        </FooterTemplate>
                                                        <ItemStyle CssClass="grid" />
                                                        <FooterStyle BorderStyle="None" HorizontalAlign="Left" />
                                                    </asp:TemplateField>
                                                </Columns>
                                                <AlternatingRowStyle CssClass="InvgridAltRow"></AlternatingRowStyle>
                                                <HeaderStyle CssClass="Invgridheader" />
                                                <PagerStyle CssClass="Invgridheader" />
                                                <FooterStyle CssClass="Invgridheader" />
                                                <RowStyle CssClass="Invgridrow" HorizontalAlign="Center"></RowStyle>
                                            </asp:GridView>
                                        </ItemTemplate>
                                        <FooterStyle HorizontalAlign="Center" />
                                        <ItemStyle CssClass="grid" />
                                    </asp:TemplateField>
                                </Columns>
                                <AlternatingRowStyle CssClass="Invgridrow"></AlternatingRowStyle>
                                <HeaderStyle CssClass="Invgridheader" />
                                <PagerStyle CssClass="Invgridheader" />
                                <FooterStyle CssClass="Invgridheader" />
                                <RowStyle CssClass="Invgridrow" HorizontalAlign="Center"></RowStyle>
                            </asp:GridView>
                            <center>
                                <table>
                                    <asp:Panel ID="Panel5" runat="server" DefaultButton="btnAddToInvoice">
                                        <asp:Button ID="btnAddToInvoice" runat="server" CssClass="buttonCommman" Text="<%$ Resources:Attendance,Add To Invoice %>"
                                            CausesValidation="False" OnClick="btnAddToInvoice_Click" Width="200px" />
                                    </asp:Panel>
                                </table>
                            </center>
                        </asp:Panel>
                    </asp:Panel>
                </center>
            </asp:Panel>
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
