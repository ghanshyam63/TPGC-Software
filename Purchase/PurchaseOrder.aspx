<%@ Page Language="C#" MasterPageFile="~/ERPMaster.master" AutoEventWireup="true"
    CodeFile="PurchaseOrder.aspx.cs" Inherits="Purchase_PurchaseOrder" Title="Untitled Page" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <asp:UpdatePanel ID="UpdatePanel1" runat="server">
        <Triggers>
            <asp:PostBackTrigger ControlID="btnList" />
            <asp:PostBackTrigger ControlID="btnNew" />
            <asp:PostBackTrigger ControlID="btnBin" />
            <asp:PostBackTrigger ControlID="txtTotalWeight" />
            <asp:PostBackTrigger ControlID="txtUnitRate" />
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
                                                <asp:Label ID="lblHeader" runat="server" Text="<%$ Resources:Attendance,Purchase Order %>"
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
                                            <td>
                                                <asp:Panel ID="pnlMenuBin" runat="server" CssClass="a">
                                                    <asp:Button ID="btnBin" runat="server" Text="<%$ Resources:Attendance,Bin %>" Width="90px"
                                                        BorderStyle="none" BackColor="Transparent" OnClick="btnBin_Click" Style="padding-left: 10px;
                                                        padding-top: 3px; background-image: url(  '../Images/Bin.png' ); background-repeat: no-repeat;
                                                        height: 49px; background-position: 5px 15px; font: bold 14px Trebuchet MS; color: #000000;" />
                                                </asp:Panel>
                                            </td>
                                            <td>
                                                <asp:Panel ID="PnlMenuQuotation" runat="server" CssClass="a">
                                                    <asp:Button ID="BtnQuotation" runat="server" Text="<%$ Resources:Attendance,Quotation %>"
                                                        Width="120px" BorderStyle="none" BackColor="Transparent" Style="padding-left: 35px;
                                                        padding-top: 3px; background-image: url('../Images/Request.png' ); background-repeat: no-repeat;
                                                        height: 49px; background-position: 5px 15px; font: bold 14px Trebuchet MS; color: #000000;"
                                                        OnClick="BtnQuotation_Click" />
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
                                                                <asp:ListItem Selected="True" Text="<%$ Resources:Attendance,Purchase Order No %>"
                                                                    Value="PONo"></asp:ListItem>
                                                                <asp:ListItem Text="<%$ Resources:Attendance,Purchase Order Date %>" Value="PODate"></asp:ListItem>
                                                                <asp:ListItem Text="<%$ Resources:Attendance,Delivery Date %>" Value="DeliveryDate"></asp:ListItem>
                                                                <asp:ListItem Text="<%$ Resources:Attendance,Order Type %>" Value="OrderType"></asp:ListItem>
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
                                        <asp:GridView ID="gvPurchaseOrder" PageSize="<%# SystemParameter.GetPageSize() %>"
                                            runat="server" AutoGenerateColumns="False" Width="100%" AllowPaging="True" AllowSorting="True"
                                            CssClass="grid" OnPageIndexChanging="gvPurchaseOrder_PageIndexChanging" OnSorting="gvPurchaseOrder_OnSorting">
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
                                                <asp:TemplateField HeaderText="<%$ Resources:Attendance,Purchase Order No %>" SortExpression="PONo">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lbPONo" runat="server" Text='<%# Eval("PONo") %>'></asp:Label>
                                                    </ItemTemplate>
                                                    <ItemStyle CssClass="grid" />
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="<%$ Resources:Attendance,Purchase Order Date %>" SortExpression="PODate">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblPODate" runat="server" Text='<%# GetDateFromat(Eval("PODate").ToString()) %>'></asp:Label>
                                                    </ItemTemplate>
                                                    <ItemStyle CssClass="grid" />
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="<%$ Resources:Attendance,Delivery Date %>" SortExpression="DeliveryDate">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblDeliveryDate" runat="server" Text='<%# GetDateFromat(Eval("DeliveryDate").ToString()) %>'></asp:Label>
                                                    </ItemTemplate>
                                                    <ItemStyle CssClass="grid" />
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="<%$ Resources:Attendance,Order Type %>" SortExpression="OrderType">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblOrderType" runat="server" Text='<%# GetOderType(Eval("OrderType").ToString()) %>'></asp:Label>
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
                                                    <asp:Label ID="lblRequestdate" runat="server" CssClass="labelComman" Text="<%$ Resources:Attendance,Purchase Order Date %>"></asp:Label>
                                                </td>
                                                <td width="1px">
                                                    :
                                                </td>
                                                <td align='<%= Common.ChangeTDForDefaultLeft()%>' width="270px">
                                                    <asp:TextBox ID="txtPOdate" runat="server" CssClass="textComman" />
                                                    <cc1:CalendarExtender ID="txtCalenderExtender" runat="server" TargetControlID="txtPOdate"
                                                        Format="dd-MMM-yyyy">
                                                    </cc1:CalendarExtender>
                                                </td>
                                                <td width="150px" align='<%= Common.ChangeTDForDefaultLeft()%>'>
                                                    <asp:Label ID="Label22" runat="server" CssClass="labelComman" Text="<%$ Resources:Attendance,Purchase Order No %>"></asp:Label>
                                                </td>
                                                <td width="1px">
                                                    :
                                                </td>
                                                <td align='<%= Common.ChangeTDForDefaultLeft()%>'>
                                                    <asp:TextBox ID="txtPoNo" runat="server" CssClass="textComman" AutoPostBack="True"
                                                        OnTextChanged="txtPoNo_TextChanged" />
                                                </td>
                                            </tr>
                                            <tr>
                                                <td align='<%= Common.ChangeTDForDefaultLeft()%>' width="170px">
                                                    <asp:Label ID="lblOrderType" runat="server" Text="<%$ Resources:Attendance,Order Type %>"
                                                        CssClass="labelComman"></asp:Label>
                                                </td>
                                                <td>
                                                    :
                                                </td>
                                                <td align='<%= Common.ChangeTDForDefaultLeft()%>' width="270px">
                                                    <asp:DropDownList ID="ddlOrderType" runat="server" CssClass="textComman" Width="260px"
                                                        AutoPostBack="True" OnSelectedIndexChanged="ddlOrderType_SelectedIndexChanged">
                                                        <asp:ListItem Text="Direct" Value="D"></asp:ListItem>
                                                        <asp:ListItem Text="InDirect" Value="R"></asp:ListItem>
                                                    </asp:DropDownList>
                                                </td>
                                                <td align='<%= Common.ChangeTDForDefaultLeft()%>' width="150px">
                                                    <asp:Label ID="Label3" runat="server" CssClass="labelComman" Text="<%$ Resources:Attendance,Delivery Date %>"></asp:Label>
                                                </td>
                                                <td width="1px">
                                                    :
                                                </td>
                                                <td align='<%= Common.ChangeTDForDefaultLeft()%>'>
                                                    <asp:TextBox ID="txtDeliveryDate" runat="server" CssClass="textComman" />
                                                    <cc1:CalendarExtender ID="CalendarExtender1" runat="server" TargetControlID="txtDeliveryDate"
                                                        Format="dd-MMM-yyyy">
                                                    </cc1:CalendarExtender>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td colspan="6" align='<%= Common.ChangeTDForDefaultLeft()%>'>
                                                    <asp:Panel ID="PnlReference" runat="server">
                                                        <table width="100%">
                                                            <tr>
                                                                <td width="170px" align='<%= Common.ChangeTDForDefaultLeft()%>'>
                                                                    <asp:Label ID="Label1" runat="server" CssClass="labelComman" Text="<%$ Resources:Attendance,Reference Voucher Type %>"></asp:Label>
                                                                </td>
                                                                <td width="1px">
                                                                    :
                                                                </td>
                                                                <td align='<%= Common.ChangeTDForDefaultLeft()%>' width="270px">
                                                                    <asp:DropDownList ID="ddlReferenceVoucherType" runat="server" CssClass="textComman"
                                                                        Width="260px">
                                                                        <asp:ListItem Text="Purchase Quotation" Value="PQ"></asp:ListItem>
                                                                        <asp:ListItem Text="Sales Order" Value="SO"></asp:ListItem>
                                                                    </asp:DropDownList>
                                                                </td>
                                                                <td align='<%= Common.ChangeTDForDefaultLeft()%>' width="150px">
                                                                    <asp:Label ID="lblReferenceNo" runat="server" Text="<%$ Resources:Attendance,Reference No %>"
                                                                        CssClass="labelComman"></asp:Label>
                                                                </td>
                                                                <td>
                                                                    :
                                                                </td>
                                                                <td align='<%= Common.ChangeTDForDefaultLeft()%>'>
                                                                    <asp:TextBox ID="txtReferenceNo" runat="server" CssClass="textComman"></asp:TextBox>
                                                                </td>
                                                            </tr>
                                                        </table>
                                                    </asp:Panel>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td align='<%= Common.ChangeTDForDefaultLeft()%>'>
                                                    <asp:Label ID="Label2" runat="server" CssClass="labelComman" Text="<%$ Resources:Attendance,Supplier Name %>"></asp:Label>
                                                </td>
                                                <td width="1px">
                                                    :
                                                </td>
                                                <td align='<%= Common.ChangeTDForDefaultLeft()%>' colspan="4" width="684px">
                                                    <asp:TextBox ID="txtSupplierName" runat="server" CssClass="textComman" BackColor="#e3e3e3" Width="684px"
                                                        AutoPostBack="True" OnTextChanged="txtSupplierName_TextChanged"></asp:TextBox>
                                                    <cc1:AutoCompleteExtender ID="AutoCompleteExtender2" runat="server" CompletionInterval="100"
                                                        DelimiterCharacters="" Enabled="True" MinimumPrefixLength="1" ServiceMethod="GetCompletionList_Supplier"
                                                        ServicePath="" TargetControlID="txtSupplierName" UseContextKey="True" CompletionListCssClass="completionList"
                                                        CompletionListItemCssClass="listItem" CompletionListHighlightedItemCssClass="itemHighlighted">
                                                    </cc1:AutoCompleteExtender>
                                                </td>
                                                <td align='<%= Common.ChangeTDForDefaultLeft()%>'>
                                                    <asp:Panel ID="pnlGetQutation" runat="server" DefaultButton="btnCompareQuatation">
                                                        <asp:ImageButton ID="btnCompareQuatation" runat="server" ImageUrl="~/Images/compare.png"
                                                            Height="35px" ToolTip="<%$ Resources:Attendance,Quotation Comparison %>" OnClick="btnCompareQuatation_Click" />
                                                    </asp:Panel>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td colspan="5">
                                                </td>
                                                <td align='<%= Common.ChangeTDForDefaultLeft()%>'>
                                                    <asp:Panel ID="Panel5" runat="server" DefaultButton="btnAddProduct">
                                                        <asp:Button ID="btnAddProduct" runat="server" Text="<%$ Resources:Attendance,Add Product %>"
                                                            BackColor="Transparent" BorderStyle="None" Font-Bold="true" CssClass="btnLocation"
                                                            Font-Size="13px" Font-Names=" Arial" Width="172px" Height="32px" OnClick="btnAddProduct_Click" />
                                                    </asp:Panel>
                                                </td>
                                            </tr>
                                        </table>
                                        <table width="100%" style="padding-left: 43px">
                                            <tr>
                                                <td colspan="6">
                                                    <asp:GridView ID="gvProduct" PageSize="<%# SystemParameter.GetPageSize() %>" runat="server"
                                                        AutoGenerateColumns="False" Width="100%" CssClass="grid">
                                                        <Columns>
                                                            <asp:TemplateField HeaderText="<%$ Resources:Attendance,Edit %>">
                                                                <ItemTemplate>
                                                                    <asp:ImageButton ID="btnPDEdit" runat="server" CommandArgument='<%# Eval("Trans_Id") %>'
                                                                        ImageUrl="~/Images/edit.png" CausesValidation="False" OnCommand="btnPDEdit_Command"
                                                                        ToolTip="<%$ Resources:Attendance,Edit %>" Visible="false" /></ItemTemplate>
                                                                <ItemStyle HorizontalAlign="Center" />
                                                                <ItemStyle CssClass="grid" />
                                                            </asp:TemplateField>
                                                            <asp:TemplateField HeaderText="<%$ Resources:Attendance,Delete %>">
                                                                <ItemTemplate>
                                                                    <asp:ImageButton ID="IbtnPDDelete" runat="server" CausesValidation="False" CommandArgument='<%# Eval("Trans_Id") %>'
                                                                        ImageUrl="~/Images/Erase.png" OnCommand="IbtnPDDelete_Command" Visible="false"
                                                                        ToolTip="<%$ Resources:Attendance,Delete %>" />
                                                                </ItemTemplate>
                                                                <ItemStyle HorizontalAlign="Center" />
                                                                <ItemStyle CssClass="grid" />
                                                            </asp:TemplateField>
                                                            <asp:TemplateField HeaderText="<%$ Resources:Attendance,Serial No %>">
                                                                <ItemTemplate>
                                                                    <asp:Label ID="lblSerialNO" runat="server" Text='<%# Eval("Serial_No") %>'></asp:Label>
                                                                </ItemTemplate>
                                                                <ItemStyle CssClass="grid" />
                                                            </asp:TemplateField>
                                                            <asp:TemplateField HeaderText="<%$ Resources:Attendance,Product Name %>" ItemStyle-Width="30%">
                                                                <ItemTemplate>
                                                                    <asp:Label ID="lblProductId" runat="server" Text='<%# ProductName(Eval("Product_Id").ToString()) %>'></asp:Label>
                                                                </ItemTemplate>
                                                                <ItemStyle CssClass="grid" />
                                                            </asp:TemplateField>
                                                            <asp:TemplateField HeaderText="<%$ Resources:Attendance,Unit Name %>">
                                                                <ItemTemplate>
                                                                    <asp:Label ID="lblUnit" runat="server" Text='<%# UnitName(Eval("UnitId").ToString()) %>'></asp:Label>
                                                                </ItemTemplate>
                                                                <ItemStyle CssClass="grid" />
                                                            </asp:TemplateField>
                                                            <asp:TemplateField HeaderText="<%$ Resources:Attendance,Unit Cost %>">
                                                                <ItemTemplate>
                                                                    <asp:Label ID="lblUnitRate" runat="server" Text='<%# Eval("UnitCost") %>'></asp:Label>
                                                                </ItemTemplate>
                                                                <ItemStyle CssClass="grid" />
                                                            </asp:TemplateField>
                                                            <asp:TemplateField HeaderText="<%$ Resources:Attendance,Order Quantity %>">
                                                                <ItemTemplate>
                                                                    <asp:Label ID="lblReqQty" runat="server" Text='<%# Eval("OrderQty") %>'></asp:Label>
                                                                </ItemTemplate>
                                                                <ItemStyle CssClass="grid" />
                                                            </asp:TemplateField>
                                                            <asp:TemplateField HeaderText="<%$ Resources:Attendance,Free Quantity %>">
                                                                <ItemTemplate>
                                                                    <asp:Label ID="lblFreeQty" runat="server" Text='<%# Eval("FreeQty") %>'></asp:Label>
                                                                </ItemTemplate>
                                                                <ItemStyle CssClass="grid" />
                                                            </asp:TemplateField>
                                                        </Columns>
                                                        <AlternatingRowStyle CssClass="InvgridAltRow"></AlternatingRowStyle>
                                                        <HeaderStyle CssClass="Invgridheader" />
                                                        <PagerStyle CssClass="Invgridheader" />
                                                        <RowStyle CssClass="Invgridrow" HorizontalAlign="Center"></RowStyle>
                                                    </asp:GridView>
                                                    <asp:GridView ID="gvQuatationProduct" PageSize="<%# SystemParameter.GetPageSize() %>"
                                                        runat="server" AutoGenerateColumns="False" Width="100%" CssClass="grid">
                                                        <Columns>
                                                            <asp:TemplateField>
                                                                <ItemTemplate>
                                                                    <asp:CheckBox ID="chk" runat="server" />
                                                                </ItemTemplate>
                                                                <ItemStyle CssClass="grid" HorizontalAlign="Center" />
                                                            </asp:TemplateField>
                                                            <asp:TemplateField HeaderText="<%$ Resources:Attendance,S No. %>">
                                                                <ItemTemplate>
                                                                    <%#Container.DataItemIndex+1 %>
                                                                </ItemTemplate>
                                                                <ItemStyle CssClass="grid" HorizontalAlign="Center" />
                                                            </asp:TemplateField>
                                                            <asp:TemplateField HeaderText="<%$ Resources:Attendance, Product Name %>">
                                                                <ItemTemplate>
                                                                    <asp:Label ID="lblgvProductId" runat="server" Text='<%#Eval("Product_Id") %>' Visible="false" />
                                                                    <asp:Label ID="lblgvProductName" runat="server" Text='<%# ProductName(Eval("Product_Id").ToString()) %>' />
                                                                </ItemTemplate>
                                                                <ItemStyle CssClass="grid" HorizontalAlign="Center" />
                                                            </asp:TemplateField>
                                                            <asp:TemplateField HeaderText="<%$ Resources:Attendance, Unit %>">
                                                                <ItemTemplate>
                                                                    <asp:Label ID="lblgvUnitId" runat="server" Visible="false" Text='<%#Eval("UnitId") %>' />
                                                                    <asp:Label ID="lblgvUnit" runat="server" Text='<%# UnitName(Eval("UnitId").ToString()) %>' />
                                                                </ItemTemplate>
                                                                <ItemStyle CssClass="grid" HorizontalAlign="Center" />
                                                            </asp:TemplateField>
                                                            <asp:TemplateField HeaderText="<%$ Resources:Attendance, Order Quantity %>">
                                                                <ItemTemplate>
                                                                    <asp:Label ID="lblgvRequiredQty" runat="server" Text='<%#Eval("ReqQty") %>' />
                                                                </ItemTemplate>
                                                                <ItemStyle CssClass="grid" HorizontalAlign="Center" />
                                                            </asp:TemplateField>
                                                            <asp:TemplateField HeaderText="<%$ Resources:Attendance, Unit Cost %>">
                                                                <ItemTemplate>
                                                                    <asp:Label ID="lblUnitCost" runat="server" Text='<%# Eval("UnitPrice").ToString() %>' />
                                                                </ItemTemplate>
                                                                <ItemStyle CssClass="grid" HorizontalAlign="Center" />
                                                            </asp:TemplateField>
                                                            <asp:TemplateField HeaderText="<%$ Resources:Attendance, Tax %>">
                                                                <ItemTemplate>
                                                                    <asp:Label ID="lblTax" runat="server" Text='<%#Eval("TaxPercentage")+"% " +Eval("TaxValue")+"(Value)"+ "="+ Eval("PriceAfterTax") %>' />
                                                                </ItemTemplate>
                                                                <ItemStyle CssClass="grid" HorizontalAlign="Center" />
                                                            </asp:TemplateField>
                                                            <asp:TemplateField HeaderText="<%$ Resources:Attendance, Discount %>">
                                                                <ItemTemplate>
                                                                    <asp:Label ID="lblDiscount" runat="server" Text='<%#Eval("DisPercentage")+"% " +Eval("DiscountValue")+"(Value)" %>' />
                                                                </ItemTemplate>
                                                                <ItemStyle CssClass="grid" HorizontalAlign="Center" />
                                                            </asp:TemplateField>
                                                            <asp:TemplateField HeaderText="<%$ Resources:Attendance, Amount %>">
                                                                <ItemTemplate>
                                                                    <asp:Label ID="lblgvAmmount" runat="server" Text='<%#Eval("Amount") %>' />
                                                                </ItemTemplate>
                                                                <ItemStyle CssClass="grid" HorizontalAlign="Center" />
                                                            </asp:TemplateField>
                                                            <asp:TemplateField HeaderText="<%$ Resources:Attendance, Free Quantity %>">
                                                                <ItemTemplate>
                                                                    <asp:TextBox ID="txtFreeQty" runat="server" Width="70px" CssClass="textCommanSearch"
                                                                        Height="10px" Text="0" AutoPostBack="True" OnTextChanged="txtFreeQty_TextChanged"></asp:TextBox>
                                                                </ItemTemplate>
                                                                <ItemStyle CssClass="grid" HorizontalAlign="Center" />
                                                            </asp:TemplateField>
                                                        </Columns>
                                                        <AlternatingRowStyle CssClass="InvgridAltRow"></AlternatingRowStyle>
                                                        <HeaderStyle CssClass="Invgridheader" />
                                                        <PagerStyle CssClass="Invgridheader" />
                                                        <RowStyle CssClass="Invgridrow" HorizontalAlign="Center"></RowStyle>
                                                    </asp:GridView>
                                                </td>
                                            </tr>
                                        </table>
                                        <table width="100%" style="padding-left: 43px">
                                            <tr>
                                                <td align='<%= Common.ChangeTDForDefaultLeft()%>' width="170px">
                                                    <asp:Label ID="Label4" runat="server" Text="<%$ Resources:Attendance,Payment Mode %>"
                                                        CssClass="labelComman"></asp:Label>
                                                </td>
                                                <td>
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
                                                        AutoPostBack="true" OnSelectedIndexChanged="ddlCurrency_SelectedIndexChanged">
                                                    </asp:DropDownList>
                                                </td>
                                                <td align='<%= Common.ChangeTDForDefaultLeft()%>' width="150px">
                                                    <asp:Label ID="Label6" runat="server" Text="<%$ Resources:Attendance,Currency Rate %>"
                                                        CssClass="labelComman"></asp:Label>
                                                </td>
                                                <td>
                                                    :
                                                </td>
                                                <td align='<%= Common.ChangeTDForDefaultLeft()%>'>
                                                    <asp:TextBox ID="txtCurrencyRate" runat="server" CssClass="textComman" />
                                                   <cc1:FilteredTextBoxExtender ID="FilteredTextBoxExtender6" runat="server" Enabled="True"
                                                        TargetControlID="txtCurrencyRate" ValidChars="1,2,3,4,5,6,7,8,9,0,.">
                                                    </cc1:FilteredTextBoxExtender>
                               
                                                </td>
                                            </tr>
                                            <tr>
                                                <td align='<%= Common.ChangeTDForDefaultLeft()%>'>
                                                    <asp:Label ID="Label7" runat="server" Text="<%$ Resources:Attendance,Remark %>" CssClass="labelComman"></asp:Label>
                                                </td>
                                                <td>
                                                    :
                                                </td>
                                                <td align='<%= Common.ChangeTDForDefaultLeft()%>' colspan="4">
                                                    <asp:TextBox ID="txRemark" runat="server" CssClass="textComman" Width="680px" TextMode="MultiLine"
                                                        Height="50px" />
                                                </td>
                                            </tr>
                                            <tr>
                                                <td align='<%= Common.ChangeTDForDefaultLeft()%>'>
                                                    <asp:Label ID="Label8" runat="server" Text="<%$ Resources:Attendance,Shipping Line %>"
                                                        CssClass="labelComman"></asp:Label>
                                                </td>
                                                <td>
                                                    :
                                                </td>
                                                <td align='<%= Common.ChangeTDForDefaultLeft()%>' colspan="4">
                                                    <asp:TextBox ID="txtShippingLine" runat="server" BackColor="#e3e3e3" CssClass="textComman" Width="680px"
                                                        AutoPostBack="True" OnTextChanged="txtShippingLine_TextChanged" />
                                                    <cc1:AutoCompleteExtender ID="AutoCompleteExtender1" runat="server" CompletionInterval="100"
                                                        DelimiterCharacters="" Enabled="True" MinimumPrefixLength="1" ServiceMethod="GetCompletionList"
                                                        ServicePath="" TargetControlID="txtShippingLine" UseContextKey="True" CompletionListCssClass="completionList"
                                                        CompletionListItemCssClass="listItem" CompletionListHighlightedItemCssClass="itemHighlighted">
                                                    </cc1:AutoCompleteExtender>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td colspan="2">
                                                </td>
                                                <td colspan="4">
                                                    <table>
                                                        <tr>
                                                            <td width="200px" align='<%= Common.ChangeTDForDefaultLeft()%>'>
                                                                <asp:Label ID="lblshipingLineMobileNo" runat="server" CssClass="labelComman"></asp:Label>
                                                            </td>
                                                            <td align='<%= Common.ChangeTDForDefaultLeft()%>'>
                                                                <asp:Label ID="lblShipingEmailId" runat="server" CssClass="labelComman"></asp:Label>
                                                            </td>
                                                        </tr>
                                                    </table>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td align='<%= Common.ChangeTDForDefaultLeft()%>'>
                                                    <asp:Label ID="Label9" runat="server" Text="<%$ Resources:Attendance,Ship By %>"
                                                        CssClass="labelComman"></asp:Label>
                                                </td>
                                                <td>
                                                    :
                                                </td>
                                                <td align='<%= Common.ChangeTDForDefaultLeft()%>'>
                                                    <asp:DropDownList ID="ddlShipBy" runat="server" CssClass="textComman" Width="260px">
                                                        <asp:ListItem Text="By Track" Value="By Track"></asp:ListItem>
                                                        <asp:ListItem Text="By Ship" Value="By Ship"></asp:ListItem>
                                                        <asp:ListItem Text="By Train" Value="By Train"></asp:ListItem>
                                                        <asp:ListItem Text="By Air Fright" Value="By Air Fright"></asp:ListItem>
                                                        <asp:ListItem Text="By Courier Company" Value="By Courier Company"></asp:ListItem>
                                                    </asp:DropDownList>
                                                </td>
                                                <td align='<%= Common.ChangeTDForDefaultLeft()%>'>
                                                    <asp:Label ID="Label12" runat="server" Text="<%$ Resources:Attendance,Ship Unit %>"
                                                        CssClass="labelComman"></asp:Label>
                                                </td>
                                                <td>
                                                    :
                                                </td>
                                                <td align='<%= Common.ChangeTDForDefaultLeft()%>'>
                                                    <asp:DropDownList ID="ddlShipUnit" runat="server" CssClass="textComman" Width="260px">
                                                    </asp:DropDownList>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td align='<%= Common.ChangeTDForDefaultLeft()%>'>
                                                    <asp:Label ID="Label23" runat="server" Text="<%$ Resources:Attendance,Total Weight %>"
                                                        CssClass="labelComman"></asp:Label>
                                                </td>
                                                <td>
                                                    :
                                                </td>
                                                <td align='<%= Common.ChangeTDForDefaultLeft()%>'>
                                                    <asp:TextBox ID="txtTotalWeight" runat="server" CssClass="textComman" OnTextChanged="txtTotalWeight_TextChanged" AutoPostBack="true" />
                                                <cc1:FilteredTextBoxExtender ID="FilteredTextBoxExtender4" runat="server" Enabled="True"
                                                        TargetControlID="txtTotalWeight" ValidChars="1,2,3,4,5,6,7,8,9,0,.">
                                                    </cc1:FilteredTextBoxExtender>
                               
                                                </td>
                                                <td align='<%= Common.ChangeTDForDefaultLeft()%>'>
                                                    <asp:Label ID="Label13" runat="server" Text="<%$ Resources:Attendance,Unit Rate %>"
                                                        CssClass="labelComman"></asp:Label>
                                                </td>
                                                <td>
                                                    :
                                                </td>
                                                <td align='<%= Common.ChangeTDForDefaultLeft()%>'>
                                                    <asp:TextBox ID="txtUnitRate" runat="server" CssClass="textComman" OnTextChanged="txtUnitRate_TextChanged" AutoPostBack="true" />
                                                 <cc1:FilteredTextBoxExtender ID="FilteredTextBoxExtender5" runat="server" Enabled="True"
                                                        TargetControlID="txtUnitRate" ValidChars="1,2,3,4,5,6,7,8,9,0,.">
                                                    </cc1:FilteredTextBoxExtender>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td align='<%= Common.ChangeTDForDefaultLeft()%>'>
                                                    <asp:Label ID="Label10" runat="server" Text="<%$ Resources:Attendance,Shipment Type %>"
                                                        CssClass="labelComman"></asp:Label>
                                                </td>
                                                <td>
                                                    :
                                                </td>
                                                <td align='<%= Common.ChangeTDForDefaultLeft()%>'>
                                                    <asp:DropDownList ID="ddlShipmentType" runat="server" CssClass="textComman" Width="260px">
                                                        <asp:ListItem Text="FOB (Frieght On Board)" Value="FOB (Frieght On Board)"></asp:ListItem>
                                                        <asp:ListItem Text="EX-Work" Value="EX-Work"></asp:ListItem>
                                                        <asp:ListItem Text="C&F" Value="C&F"></asp:ListItem>
                                                    </asp:DropDownList>
                                                </td>
                                                <td align='<%= Common.ChangeTDForDefaultLeft()%>'>
                                                    <asp:Label ID="Label11" runat="server" Text="<%$ Resources:Attendance,Freight Status %>"
                                                        CssClass="labelComman"></asp:Label>
                                                </td>
                                                <td>
                                                    :
                                                </td>
                                                <td align='<%= Common.ChangeTDForDefaultLeft()%>'>
                                                    <asp:DropDownList ID="ddlFreightStatus" runat="server" CssClass="textComman" Width="260px">
                                                        <asp:ListItem Text="Paid" Value="Y"></asp:ListItem>
                                                        <asp:ListItem Text="UnPaid" Value="N"></asp:ListItem>
                                                    </asp:DropDownList>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td align='<%= Common.ChangeTDForDefaultLeft()%>'>
                                                    <asp:Label ID="Label14" runat="server" CssClass="labelComman" Text="<%$ Resources:Attendance,Shipping Date %>"></asp:Label>
                                                </td>
                                                <td width="1px">
                                                    :
                                                </td>
                                                <td align='<%= Common.ChangeTDForDefaultLeft()%>'>
                                                    <asp:TextBox ID="txtShippingDate" runat="server" CssClass="textComman" />
                                                    <cc1:CalendarExtender ID="CalendarExtender2" runat="server" TargetControlID="txtShippingDate"
                                                        Format="dd-MMM-yyyy">
                                                    </cc1:CalendarExtender>
                                                </td>
                                                <td align='<%= Common.ChangeTDForDefaultLeft()%>'>
                                                    <asp:Label ID="Label15" runat="server" CssClass="labelComman" Text="<%$ Resources:Attendance,Receiving Date %>"></asp:Label>
                                                </td>
                                                <td width="1px">
                                                    :
                                                </td>
                                                <td align='<%= Common.ChangeTDForDefaultLeft()%>'>
                                                    <asp:TextBox ID="txtReceivingDate" runat="server" CssClass="textComman" />
                                                    <cc1:CalendarExtender ID="CalendarExtender3" runat="server" TargetControlID="txtReceivingDate"
                                                        Format="dd-MMM-yyyy">
                                                    </cc1:CalendarExtender>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td align='<%= Common.ChangeTDForDefaultLeft()%>'>
                                                    <asp:Label ID="Label16" runat="server" Text="<%$ Resources:Attendance,Partial Shipment %>"
                                                        CssClass="labelComman"></asp:Label>
                                                </td>
                                                <td>
                                                    :
                                                </td>
                                                <td align='<%= Common.ChangeTDForDefaultLeft()%>'>
                                                    <asp:DropDownList ID="ddlPartialShipment" runat="server" CssClass="textComman" Width="260px">
                                                        <asp:ListItem Text="Yes" Value="Y"></asp:ListItem>
                                                        <asp:ListItem Text="No" Value="N"></asp:ListItem>
                                                    </asp:DropDownList>
                                                </td>
                                            </tr>
                                        </table>
                                        <asp:Panel ID="PnlCondition" runat="server">
                                            <table width="100%" style="padding-left: 43px">
                                                <tr>
                                                    <td width="170px" align='<%= Common.ChangeTDForDefaultLeft()%>'>
                                                        <asp:Label ID="Label17" runat="server" CssClass="labelComman" Text="<%$ Resources:Attendance,Condition 1 %>"></asp:Label>
                                                    </td>
                                                    <td width="1px">
                                                        :
                                                    </td>
                                                    <td align='<%= Common.ChangeTDForDefaultLeft()%>' colspan="4">
                                                        <asp:TextBox ID="txtCondition_1" runat="server" CssClass="textComman" Width="680px"
                                                            Height="50px" TextMode="MultiLine"></asp:TextBox>
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td width="170px" align='<%= Common.ChangeTDForDefaultLeft()%>'>
                                                        <asp:Label ID="Label18" runat="server" CssClass="labelComman" Text="<%$ Resources:Attendance,Condition 2 %>"></asp:Label>
                                                    </td>
                                                    <td width="1px">
                                                        :
                                                    </td>
                                                    <td align='<%= Common.ChangeTDForDefaultLeft()%>' colspan="4">
                                                        <asp:TextBox ID="txtCondition2" runat="server" CssClass="textComman" Width="680px"
                                                            Height="50px" TextMode="MultiLine"></asp:TextBox>
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td width="170px" align='<%= Common.ChangeTDForDefaultLeft()%>'>
                                                        <asp:Label ID="Label19" runat="server" CssClass="labelComman" Text="<%$ Resources:Attendance,Condition 3 %>"></asp:Label>
                                                    </td>
                                                    <td width="1px">
                                                        :
                                                    </td>
                                                    <td align='<%= Common.ChangeTDForDefaultLeft()%>' colspan="4">
                                                        <asp:TextBox ID="txtCondition3" runat="server" CssClass="textComman" Width="680px"
                                                            Height="50px" TextMode="MultiLine"></asp:TextBox>
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td width="170px" align='<%= Common.ChangeTDForDefaultLeft()%>'>
                                                        <asp:Label ID="Label20" runat="server" CssClass="labelComman" Text="<%$ Resources:Attendance,Condition 4 %>"></asp:Label>
                                                    </td>
                                                    <td width="1px">
                                                        :
                                                    </td>
                                                    <td align='<%= Common.ChangeTDForDefaultLeft()%>' colspan="4">
                                                        <asp:TextBox ID="txtCondition4" runat="server" CssClass="textComman" Width="680px"
                                                            Height="50px" TextMode="MultiLine"></asp:TextBox>
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td width="170px" align='<%= Common.ChangeTDForDefaultLeft()%>'>
                                                        <asp:Label ID="Label21" runat="server" CssClass="labelComman" Text="<%$ Resources:Attendance,Condition 5 %>"></asp:Label>
                                                    </td>
                                                    <td width="1px">
                                                        :
                                                    </td>
                                                    <td align='<%= Common.ChangeTDForDefaultLeft()%>' colspan="4">
                                                        <asp:TextBox ID="txtCondition5" runat="server" CssClass="textComman" Width="680px"
                                                            Height="50px" TextMode="MultiLine"></asp:TextBox>
                                                    </td>
                                                </tr>
                                            </table>
                                        </asp:Panel>
                                        <table width="100%" style="padding-left: 43px">
                                            <tr>
                                                <td colspan="6" align="center">
                                                    <table>
                                                        <tr>
                                                            <td width="90px">
                                                                <asp:Panel ID="Panel1" runat="server" DefaultButton="btnSave">
                                                                    <asp:Button ID="btnSave" runat="server" Text="<%$ Resources:Attendance,Save %>" CssClass="buttonCommman"
                                                                        OnClick="btnSave_Click" Visible="false" />
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
                                                                <asp:ListItem Selected="True" Text="<%$ Resources:Attendance,Purchase Order No %>"
                                                                    Value="PONo"></asp:ListItem>
                                                                <asp:ListItem Text="<%$ Resources:Attendance,Purchase Order Date %>" Value="PODate"></asp:ListItem>
                                                                <asp:ListItem Text="<%$ Resources:Attendance,Delivery Date %>" Value="DeliveryDate"></asp:ListItem>
                                                                <asp:ListItem Text="<%$ Resources:Attendance,Order Type %>" Value="OrderType"></asp:ListItem>
                                                            </asp:DropDownList>
                                                        </td>
                                                        <td width="135px">
                                                            <asp:DropDownList ID="ddlbinOption" runat="server" CssClass="DropdownSearch" Height="25px"
                                                                Width="120px">
                                                                <asp:ListItem Text="<%$ Resources:Attendance,--Select-- %>"></asp:ListItem>
                                                                <asp:ListItem Text="<%$ Resources:Attendance,Equal %>"></asp:ListItem>
                                                                <asp:ListItem Text="<%$ Resources:Attendance,Like %>"></asp:ListItem>
                                                                <asp:ListItem Selected="True" Text="<%$ Resources:Attendance,Contains %>"></asp:ListItem>
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
                                                                    runat="server" ImageUrl="~/Images/active.png" Visible="false" OnClick="imgBtnRestore_Click"
                                                                    ToolTip="<%$ Resources:Attendance, Active %>" />
                                                            </asp:Panel>
                                                        </td>
                                                        <td>
                                                            <asp:Panel ID="pnlImgbtnSelectAll" runat="server" DefaultButton="ImgbtnSelectAll">
                                                                <asp:ImageButton ID="ImgbtnSelectAll" runat="server" OnClick="ImgbtnSelectAll_Click"
                                                                    ToolTip="<%$ Resources:Attendance, Select All %>" Visible="false" AutoPostBack="true"
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
                                        <asp:GridView ID="GvBinPurchaseOrder" PageSize="<%# SystemParameter.GetPageSize() %>"
                                            runat="server" AutoGenerateColumns="False" Width="100%" AllowPaging="True" AllowSorting="True"
                                            CssClass="grid" OnPageIndexChanging="GvBinPurchaseOrder_PageIndexChanging" OnSorting="GvBinPurchaseOrder_Sorting">
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
                                                <asp:TemplateField HeaderText="<%$ Resources:Attendance,Purchase Order No %>" SortExpression="PONo">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lbPONo" runat="server" Text='<%# Eval("PONo") %>'></asp:Label>
                                                        <asp:Label ID="lblId" runat="server" Text='<%# Eval("TransId") %>' Visible="false"></asp:Label>
                                                    </ItemTemplate>
                                                    <ItemStyle CssClass="grid" />
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="<%$ Resources:Attendance,Purchase Order Date %>" SortExpression="PODate">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblPODate" runat="server" Text='<%# GetDateFromat(Eval("PODate").ToString()) %>'></asp:Label>
                                                    </ItemTemplate>
                                                    <ItemStyle CssClass="grid" />
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="<%$ Resources:Attendance,Delivery Date %>" SortExpression="DeliveryDate">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblDeliveryDate" runat="server" Text='<%# GetDateFromat(Eval("DeliveryDate").ToString()) %>'></asp:Label>
                                                    </ItemTemplate>
                                                    <ItemStyle CssClass="grid" />
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="<%$ Resources:Attendance,Order Type %>" SortExpression="OrderType">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblOrderType" runat="server" Text='<%# GetOderType(Eval("OrderType").ToString()) %>'></asp:Label>
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
                                    <asp:Panel ID="PnlQuotation" runat="server">
                                        <asp:Panel ID="PnlQuotationSearch" runat="server" DefaultButton="ImgBtnQBind">
                                            <div width="100%" style="background-image: url(../Images/bg_repeat.jpg); background-repeat: repeat;">
                                                <table width="100%" style="padding-left: 20px; height: 38px">
                                                    <tr>
                                                        <td width="90px">
                                                            <asp:Label ID="lblQSeleclField" runat="server" Text="<%$ Resources:Attendance,Select Field %>"
                                                                CssClass="labelComman"></asp:Label>
                                                        </td>
                                                        <td width="180px">
                                                            <asp:DropDownList ID="ddlQSeleclField" runat="server" CssClass="DropdownSearch" Height="25px"
                                                                Width="170px">
                                                                <asp:ListItem Text="<%$ Resources:Attendance, Quotation Id %>" Value="Trans_Id"></asp:ListItem>
                                                                <asp:ListItem Text="<%$ Resources:Attendance, Quotation No. %>" Value="RPQ_No" Selected="True"></asp:ListItem>
                                                                <asp:ListItem Text="<%$ Resources:Attendance, Quotation Date %>" Value="RPQ_Date"></asp:ListItem>
                                                            </asp:DropDownList>
                                                        </td>
                                                        <td width="135px">
                                                            <asp:DropDownList ID="ddlQOption" runat="server" CssClass="DropdownSearch" Height="25px"
                                                                Width="120px">
                                                                <asp:ListItem Text="<%$ Resources:Attendance,--Select-- %>"></asp:ListItem>
                                                                <asp:ListItem Text="<%$ Resources:Attendance,Equal %>"></asp:ListItem>
                                                                <asp:ListItem Selected="True" Text="<%$ Resources:Attendance,Contains %>"></asp:ListItem>
                                                                <asp:ListItem Text="<%$ Resources:Attendance,Like %>"></asp:ListItem>
                                                            </asp:DropDownList>
                                                        </td>
                                                        <td width="270px">
                                                            <asp:TextBox ID="txtQValue" runat="server" CssClass="textCommanSearch" Height="14px"
                                                                Width="250px"></asp:TextBox>
                                                        </td>
                                                        <td width="50px" align="center">
                                                            <asp:ImageButton ID="ImgBtnQBind" runat="server" CausesValidation="False" Height="25px"
                                                                ImageUrl="~/Images/search.png" OnClick="ImgBtnQBind_Click" Width="25px" ToolTip="<%$ Resources:Attendance,Search %>">
                                                            </asp:ImageButton>
                                                        </td>
                                                        <td>
                                                            <asp:Panel ID="pnl" runat="server" DefaultButton="ImgBtnQRefresh">
                                                                <asp:ImageButton ID="ImgBtnQRefresh" runat="server" CausesValidation="False" Height="25px"
                                                                    ImageUrl="~/Images/refresh.png" OnClick="ImgBtnQRefresh_Click" Width="25px" ToolTip="<%$ Resources:Attendance,Refresh %>">
                                                                </asp:ImageButton>
                                                            </asp:Panel>
                                                        </td>
                                                        <td align="center">
                                                            <asp:Label ID="lblQTotalRecords" runat="server" Text="<%$ Resources:Attendance,Total Records: 0 %>"
                                                                CssClass="labelComman"></asp:Label>
                                                        </td>
                                                    </tr>
                                                </table>
                                            </div>
                                            <asp:GridView ID="GvPurchaseQuote" PageSize="<%# SystemParameter.GetPageSize() %>"
                                                runat="server" AutoGenerateColumns="False" Width="100%" AllowPaging="True" OnPageIndexChanging="GvPurchaseQuote_PageIndexChanging"
                                                AllowSorting="True" OnSorting="GvPurchaseQuote_Sorting" CssClass="grid">
                                                <AlternatingRowStyle CssClass="InvgridAltRow" />
                                                <Columns>
                                                    <asp:TemplateField>
                                                        <ItemTemplate>
                                                            <asp:Button ID="btnpullBrand" runat="server" BorderStyle="None" BackColor="Transparent"
                                                                CssClass="btnPull" CommandArgument='<%# Eval("Trans_Id") %>' OnCommand="btnPurchaseQuote_Command" />
                                                        </ItemTemplate>
                                                        <ItemStyle CssClass="grid" HorizontalAlign="Center" />
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="<%$ Resources:Attendance,Quotation No. %>" SortExpression="RPQ_No">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblgvRPQNo" runat="server" Text='<%# Eval("RPQ_No") %>' />
                                                        </ItemTemplate>
                                                        <ItemStyle HorizontalAlign="Center" CssClass="grid" />
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="<%$ Resources:Attendance,Quotation Date %>" SortExpression="RPQ_Date">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblgvRPQDate" runat="server" Text='<%# GetDateFromat(Eval("RPQ_Date").ToString()) %>' />
                                                        </ItemTemplate>
                                                        <ItemStyle HorizontalAlign="Center" CssClass="grid" />
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="<%$ Resources:Attendance,Purchase Inquiry No. %>"
                                                        SortExpression="PI_No">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblgvPINo" runat="server" Text='<%# Eval("PI_No") %>' />
                                                        </ItemTemplate>
                                                        <ItemStyle HorizontalAlign="Center" CssClass="grid" />
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="<%$ Resources:Attendance,Total Amount %>" SortExpression="TotalAmount">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblgvTotalAmount" runat="server" Text='<%# Eval("TotalAmount") %>' />
                                                        </ItemTemplate>
                                                        <ItemStyle HorizontalAlign="Center" CssClass="grid" />
                                                    </asp:TemplateField>
                                                </Columns>
                                                <HeaderStyle CssClass="Invgridheader" />
                                                <PagerStyle CssClass="Invgridheader" />
                                                <RowStyle CssClass="Invgridrow" HorizontalAlign="Center" />
                                            </asp:GridView>
                                        </asp:Panel>
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
                                    <asp:TextBox ID="txtProductName" runat="server" CssClass="textComman"  BackColor="#e3e3e3" AutoPostBack="True"
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
                                    <asp:TextBox ID="txtUnitCost" runat="server" CssClass="textComman" />
                                    <cc1:FilteredTextBoxExtender ID="FilteredTextBoxExtender1" runat="server" Enabled="True"
                                                        TargetControlID="txtUnitCost" ValidChars="1,2,3,4,5,6,7,8,9,0,.">
                                                    </cc1:FilteredTextBoxExtender>
                               
                                </td>
                            </tr>
                            <tr>
                                <td align='<%= Common.ChangeTDForDefaultLeft()%>'>
                                    <asp:Label ID="Label24" runat="server" CssClass="labelComman" Text="<%$ Resources:Attendance,Order Quantity %>" />
                                </td>
                                <td align="center">
                                    :
                                </td>
                                <td align='<%= Common.ChangeTDForDefaultLeft()%>'>
                                    <asp:TextBox ID="txtOrderQty" runat="server" CssClass="textComman" />
                                      <cc1:FilteredTextBoxExtender ID="FilteredTextBoxExtender2" runat="server" Enabled="True"
                                                        TargetControlID="txtOrderQty" ValidChars="1,2,3,4,5,6,7,8,9,0,.">
                                                    </cc1:FilteredTextBoxExtender>
                               
                                </td>
                                <td align='<%= Common.ChangeTDForDefaultLeft()%>'>
                                    <asp:Label ID="Label25" runat="server" CssClass="labelComman" Text="<%$ Resources:Attendance,Free Quantity %>" />
                                </td>
                                <td align="center">
                                    :
                                </td>
                                <td align='<%= Common.ChangeTDForDefaultLeft()%>'>
                                    <asp:TextBox ID="txtfreeQty" runat="server" CssClass="textComman" />
                                      <cc1:FilteredTextBoxExtender ID="FilteredTextBoxExtender3" runat="server" Enabled="True"
                                                        TargetControlID="txtfreeQty" ValidChars="1,2,3,4,5,6,7,8,9,0,.">
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
                                    <%-- <asp:TextBox ID="txtPDescription" Width="660px" TextMode="MultiLine" runat="server"
                                        CssClass="textComman" />--%>
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
                                                        CssClass="buttonCommman" Visible="false" OnClick="btnProductSave_Click" />
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
