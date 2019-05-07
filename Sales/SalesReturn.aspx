<%@ Page Language="C#" MasterPageFile="~/ERPMaster.master" AutoEventWireup="true"
    CodeFile="SalesReturn.aspx.cs" Inherits="Sales_SalesReturn" Title="Untitled Page" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <link href="../CSS/AJAX.css" rel="stylesheet" type="text/css" />
    <asp:UpdatePanel ID="upallpage" runat="server">
        <Triggers>
            <asp:PostBackTrigger ControlID="btnList" />
            <asp:PostBackTrigger ControlID="btnNew" />
            <asp:PostBackTrigger ControlID="btnBin" />
            <asp:PostBackTrigger ControlID="GvSalesReturn" />
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
                                                <asp:Label ID="lblPHeader" runat="server" Text="<%$ Resources:Attendance,Sales Invoice %>"
                                                    CssClass="LableHeaderTitle" />
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
                                                <asp:Panel ID="pnlMenuBin" runat="server" CssClass="a" Width="100%">
                                                    <asp:Button ID="btnBin" runat="server" Text="<%$ Resources:Attendance,Bin %>" Width="90px"
                                                        BorderStyle="none" BackColor="Transparent" OnClick="btnBin_Click" Style="padding-left: 10px;
                                                        padding-top: 3px; background-image: url('../Images/Bin.png' ); background-repeat: no-repeat;
                                                        height: 49px; background-position: 5px 15px; font: bold 14px Trebuchet MS; color: #000000;" />
                                                </asp:Panel>
                                            </td>
                                            <td>
                                                <asp:Panel ID="pnlMenuRequest" runat="server" CssClass="a" Width="100%">
                                                    <asp:Button ID="btnPRequest" runat="server" Text="<%$ Resources:Attendance,Order %>"
                                                        Width="120px" BorderStyle="none" BackColor="Transparent" OnClick="btnPRequest_Click"
                                                        Style="padding-left: 30px; padding-top: 3px; background-image: url('../Images/Request.png' );
                                                        background-repeat: no-repeat; height: 49px; background-position: 7px 15px; font: bold 14px Trebuchet MS;
                                                        color: #000000;" />
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
                                                                <asp:ListItem Text="<%$ Resources:Attendance, Invoice Id %>" Value="Trans_Id"></asp:ListItem>
                                                                <asp:ListItem Text="<%$ Resources:Attendance, Invoice No %>" Value="Invoice_No" Selected="True"></asp:ListItem>
                                                                <asp:ListItem Text="<%$ Resources:Attendance, Invoice Date %>" Value="Invoice_Date"></asp:ListItem>
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
                                            <asp:GridView ID="GvSalesReturn" PageSize="10" runat="server" AutoGenerateColumns="False"
                                                Width="100%" AllowPaging="True" OnPageIndexChanging="GvSalesReturn_PageIndexChanging"
                                                AllowSorting="True" OnSorting="GvSalesReturn_Sorting" CssClass="grid">
                                                <AlternatingRowStyle CssClass="InvgridAltRow" />
                                                <Columns>
                                                    <asp:TemplateField>
                                                        <ItemTemplate>
                                                            <asp:ImageButton ID="btnEdit" runat="server" CommandArgument='<%# Eval("Trans_Id") %>'
                                                                ImageUrl="~/Images/edit.png" OnCommand="btnEdit_Command" CausesValidation="False" />
                                                        </ItemTemplate>
                                                        <ItemStyle CssClass="grid" HorizontalAlign="Center" />
                                                    </asp:TemplateField>
                                                    <asp:TemplateField>
                                                        <ItemTemplate>
                                                            <asp:ImageButton ID="IbtnDelete" runat="server" CausesValidation="False" CommandArgument='<%# Eval("Trans_Id") %>'
                                                                ImageUrl="~/Images/Erase.png" OnCommand="IbtnDelete_Command" Width="16px" />
                                                        </ItemTemplate>
                                                        <ItemStyle CssClass="grid" HorizontalAlign="Center" />
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="<%$ Resources:Attendance,Invoice No. %>" SortExpression="Invoice_No">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblgvSInvNo" runat="server" Text='<%#Eval("Invoice_No") %>' />
                                                        </ItemTemplate>
                                                        <ItemStyle HorizontalAlign="Center" CssClass="grid" />
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="<%$ Resources:Attendance,Invoice Date %>" SortExpression="Invoice_Date">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblgvSInvDate" runat="server" Text='<%#GetDate(Eval("Invoice_Date").ToString()) %>' />
                                                        </ItemTemplate>
                                                        <ItemStyle HorizontalAlign="Center" CssClass="grid" />
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="<%$ Resources:Attendance,Sales Person %>" SortExpression="SalesPerson_Id">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblgvSalesPerson" runat="server" Text='<%#Eval("SalesPerson_Id") %>' />
                                                        </ItemTemplate>
                                                        <ItemStyle HorizontalAlign="Center" CssClass="grid" />
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="<%$ Resources:Attendance,Transfer Type%>" SortExpression="SIFromTransType">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblgvTransType" runat="server" Text='<%#Eval("SIFromTransType") %>' />
                                                        </ItemTemplate>
                                                        <ItemStyle HorizontalAlign="Center" CssClass="grid" />
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="<%$ Resources:Attendance,Transfer No.%>" SortExpression="SIFromTransNo">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblgvTransNo" runat="server" Text='<%#Eval("SIFromTransNo") %>' />
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
                                    <asp:Panel ID="PnlNewEdit" runat="server" DefaultButton="btnSInvSave">
                                        <table width="100%" style="padding-left: 43px">
                                            <tr>
                                                <td align='<%= Common.ChangeTDForDefaultLeft()%>'>
                                                    <asp:Label ID="lblSInvNo" runat="server" CssClass="labelComman" Text="<%$ Resources:Attendance,Invoice No. %>"></asp:Label>
                                                </td>
                                                <td align="center">
                                                    :
                                                </td>
                                                <td align='<%= Common.ChangeTDForDefaultLeft()%>'>
                                                    <asp:TextBox ID="txtSInvNo" runat="server" CssClass="textComman" BackColor="#c3c3c3" />
                                                    <cc1:AutoCompleteExtender ID="txtSInvNo_AutoCompleteExtender" runat="server" DelimiterCharacters=""
                                                        Enabled="True" CompletionInterval="100" CompletionSetCount="1" MinimumPrefixLength="1"
                                                        ServiceMethod="GetCompletionListInvoiceNo" ServicePath="" TargetControlID="txtSInvNo"
                                                        UseContextKey="True" CompletionListCssClass="completionList" CompletionListItemCssClass="listItem"
                                                        CompletionListHighlightedItemCssClass="itemHighlighted">
                                                    </cc1:AutoCompleteExtender>
                                                </td>
                                                <td align='<%= Common.ChangeTDForDefaultLeft()%>'>
                                                    <asp:Label ID="lblSInvDate" runat="server" CssClass="labelComman" Text="<%$ Resources:Attendance,Invoice Date %>"></asp:Label>
                                                </td>
                                                <td align="center">
                                                    :
                                                </td>
                                                <td align='<%= Common.ChangeTDForDefaultLeft()%>'>
                                                    <asp:TextBox ID="txtSInvDate" runat="server" CssClass="textComman" ReadOnly="True" />
                                                    <cc1:CalendarExtender ID="Calender" runat="server" TargetControlID="txtSInvDate"
                                                        Format="dd/MMM/yyyy" />
                                                </td>
                                            </tr>
                                            <tr id="trTransfer" runat="server" visible="false">
                                                <td align='<%= Common.ChangeTDForDefaultLeft()%>'>
                                                    <asp:Label ID="lblSalesOrderNo" runat="server" CssClass="labelComman" Text="<%$ Resources:Attendance,Sales Order No. %>" />
                                                </td>
                                                <td align="center">
                                                    :
                                                </td>
                                                <td align='<%= Common.ChangeTDForDefaultLeft()%>'>
                                                    <asp:TextBox ID="txtSalesOrderNo" runat="server" CssClass="textComman" ReadOnly="True" />
                                                </td>
                                                <td align='<%= Common.ChangeTDForDefaultLeft()%>'>
                                                    <asp:Label ID="lblSalesOrderDate" runat="server" CssClass="labelComman" Text="<%$ Resources:Attendance,Sales Order Date %>" />
                                                </td>
                                                <td align="center">
                                                    :
                                                </td>
                                                <td align='<%= Common.ChangeTDForDefaultLeft()%>'>
                                                    <asp:TextBox ID="txtSalesOrderDate" runat="server" ReadOnly="true" CssClass="textComman" />
                                                </td>
                                            </tr>
                                            <tr>
                                                <td align='<%= Common.ChangeTDForDefaultLeft()%>'>
                                                    <asp:Label ID="lblCustomerName" runat="server" CssClass="labelComman" Text="<%$ Resources:Attendance,Customer Name %>" />
                                                </td>
                                                <td align="center">
                                                    :
                                                </td>
                                                <td align='<%= Common.ChangeTDForDefaultLeft()%>' colspan="4">
                                                    <asp:TextBox ID="txtCustomerName" runat="server" ReadOnly="true" CssClass="textComman"
                                                        Width="722px" />
                                                </td>
                                            </tr>
                                            <tr>
                                                <td align='<%= Common.ChangeTDForDefaultLeft()%>'>
                                                    <asp:Label ID="lblPaymentMode" runat="server" CssClass="labelComman" Text="<%$ Resources:Attendance,Payment Mode %>" />
                                                </td>
                                                <td align="center">
                                                    :
                                                </td>
                                                <td align='<%= Common.ChangeTDForDefaultLeft()%>'>
                                                    <asp:TextBox ID="txtPaymentMode" runat="server" ReadOnly="true" CssClass="textComman" />
                                                </td>
                                                <td align='<%= Common.ChangeTDForDefaultLeft()%>'>
                                                    <asp:Label ID="lblCurrency" runat="server" CssClass="labelComman" Text="<%$ Resources:Attendance,Currency %>" />
                                                </td>
                                                <td align="center">
                                                    :
                                                </td>
                                                <td align='<%= Common.ChangeTDForDefaultLeft()%>'>
                                                    <asp:TextBox ID="txtCurrency" runat="server" ReadOnly="true" CssClass="textComman"
                                                        Width="262px" />
                                                </td>
                                            </tr>
                                            <tr>
                                                <td align='<%= Common.ChangeTDForDefaultLeft()%>'>
                                                    <asp:Label ID="lblSalesPerson" runat="server" CssClass="labelComman" Text="<%$ Resources:Attendance,Sales Person%>" />
                                                </td>
                                                <td align="center">
                                                    :
                                                </td>
                                                <td align='<%= Common.ChangeTDForDefaultLeft()%>'>
                                                    <asp:TextBox ID="txtSalesPerson" runat="server" ReadOnly="true" CssClass="textComman" />
                                                </td>
                                                <td align='<%= Common.ChangeTDForDefaultLeft()%>'>
                                                    <asp:Label ID="lblPOSNo" runat="server" CssClass="labelComman" Text="<%$ Resources:Attendance,POS No.%>" />
                                                </td>
                                                <td align="center">
                                                    :
                                                </td>
                                                <td align='<%= Common.ChangeTDForDefaultLeft()%>'>
                                                    <asp:TextBox ID="txtPOSNo" runat="server" ReadOnly="true" CssClass="textComman" />
                                                </td>
                                            </tr>
                                            <tr>
                                                <td align='<%= Common.ChangeTDForDefaultLeft()%>'>
                                                    <asp:Label ID="lblAccountNo" runat="server" CssClass="labelComman" Text="<%$ Resources:Attendance,Account No%>" />
                                                </td>
                                                <td align="center">
                                                    :
                                                </td>
                                                <td align='<%= Common.ChangeTDForDefaultLeft()%>'>
                                                    <asp:TextBox ID="txtAccountNo" ReadOnly="true" runat="server" CssClass="textComman" />
                                                </td>
                                                <td align='<%= Common.ChangeTDForDefaultLeft()%>'>
                                                    <asp:Label ID="lblInvoiceCosting" runat="server" CssClass="labelComman" Text="<%$ Resources:Attendance,Invoice Costing%>" />
                                                </td>
                                                <td align="center">
                                                    :
                                                </td>
                                                <td align='<%= Common.ChangeTDForDefaultLeft()%>'>
                                                    <asp:TextBox ID="txtInvoiceCosting" ReadOnly="true" runat="server" CssClass="textComman" />
                                                </td>
                                            </tr>
                                            <tr>
                                                <td align='<%= Common.ChangeTDForDefaultLeft()%>'>
                                                    <asp:Label ID="lblShift" runat="server" CssClass="labelComman" Text="<%$ Resources:Attendance,Shift%>" />
                                                </td>
                                                <td align="center">
                                                    :
                                                </td>
                                                <td align='<%= Common.ChangeTDForDefaultLeft()%>'>
                                                    <asp:TextBox ID="txtShift" runat="server" CssClass="textComman" ReadOnly="true" />
                                                </td>
                                                <td align='<%= Common.ChangeTDForDefaultLeft()%>'>
                                                    <asp:Label ID="lblTender" runat="server" CssClass="labelComman" Text="<%$ Resources:Attendance,Tender%>" />
                                                </td>
                                                <td align="center">
                                                    :
                                                </td>
                                                <td align='<%= Common.ChangeTDForDefaultLeft()%>'>
                                                    <asp:TextBox ID="txtTender" runat="server" CssClass="textComman" ReadOnly="true" />
                                                </td>
                                            </tr>
                                            <tr>
                                                <td colspan="6">
                                                    <asp:Panel ID="pnlDetail" runat="server" Width="900px">
                                                        <asp:GridView ID="GvSalesOrderDetail" runat="server" AutoGenerateColumns="False"
                                                            Width="100%" CssClass="grid" OnRowCreated="GvSalesOrderDetail_RowCreated">
                                                            <AlternatingRowStyle CssClass="InvgridAltRow" />
                                                            <Columns>
                                                                <asp:TemplateField>
                                                                    <ItemTemplate>
                                                                        <asp:Label ID="lblgvSerialNo" runat="server" Text='<%#Eval("Serial_No") %>' />
                                                                    </ItemTemplate>
                                                                    <ItemStyle HorizontalAlign="Center" CssClass="grid" Width="30px" />
                                                                </asp:TemplateField>
                                                                <asp:TemplateField HeaderText="<%$ Resources:Attendance,Name %>">
                                                                    <ItemTemplate>
                                                                        <asp:HiddenField ID="hdngvProductId" runat="server" Value='<%#Eval("Product_Id") %>' />
                                                                        <asp:Label ID="lblgvProductName" runat="server" Text='<%#GetProductName(Eval("Product_Id").ToString()) %>' />
                                                                    </ItemTemplate>
                                                                    <ItemStyle HorizontalAlign="Center" CssClass="grid" />
                                                                </asp:TemplateField>
                                                                <asp:TemplateField HeaderText="<%$ Resources:Attendance,Description %>">
                                                                    <ItemTemplate>
                                                                        <asp:Label ID="lblgvProductDescription" runat="server" Text='<%#GetProductDescription(Eval("Product_Id").ToString()) %>' />
                                                                    </ItemTemplate>
                                                                    <ItemStyle HorizontalAlign="Center" CssClass="grid" />
                                                                </asp:TemplateField>
                                                                <asp:TemplateField HeaderText="<%$ Resources:Attendance,Unit %>">
                                                                    <ItemTemplate>
                                                                        <asp:Label ID="lblgvUnit" runat="server" Text='<%#GetUnitCode(Eval("UnitId").ToString()) %>' />
                                                                        <asp:HiddenField ID="hdngvUnitId" runat="server" Value='<%#Eval("UnitId") %>' />
                                                                    </ItemTemplate>
                                                                    <ItemStyle HorizontalAlign="Center" CssClass="grid" />
                                                                </asp:TemplateField>
                                                                <asp:TemplateField HeaderText="<%$ Resources:Attendance,Price %>">
                                                                    <ItemTemplate>
                                                                        <asp:Label ID="lblgvUnitPrice" runat="server" Text='<%#Eval("UnitPrice") %>' />
                                                                    </ItemTemplate>
                                                                    <ItemStyle HorizontalAlign="Center" CssClass="grid" />
                                                                </asp:TemplateField>
                                                                <asp:TemplateField HeaderText="<%$ Resources:Attendance,Cost %>">
                                                                    <ItemTemplate>
                                                                        <asp:TextBox ID="txtgvUnitCost" runat="server" Width="50px" />
                                                                    </ItemTemplate>
                                                                    <ItemStyle HorizontalAlign="Center" CssClass="grid" />
                                                                </asp:TemplateField>
                                                                <asp:TemplateField HeaderText="<%$ Resources:Attendance,Free %>">
                                                                    <ItemTemplate>
                                                                        <asp:Label ID="lblgvFreeQuantity" runat="server" Text='<%#Eval("FreeQty") %>' />
                                                                    </ItemTemplate>
                                                                    <ItemStyle HorizontalAlign="Center" CssClass="grid" />
                                                                </asp:TemplateField>
                                                                <asp:TemplateField HeaderText="<%$ Resources:Attendance,Quantity %>">
                                                                    <ItemTemplate>
                                                                        <asp:Label ID="lblgvQuantity" runat="server" Text='<%#Eval("Quantity") %>' />
                                                                    </ItemTemplate>
                                                                    <ItemStyle HorizontalAlign="Center" CssClass="grid" />
                                                                </asp:TemplateField>
                                                                <asp:TemplateField HeaderText="<%$ Resources:Attendance,% %>">
                                                                    <ItemTemplate>
                                                                        <asp:TextBox ID="txtgvTaxP" Width="30px" Text='<%#Eval("TaxP") %>' runat="server" />
                                                                    </ItemTemplate>
                                                                    <ItemStyle HorizontalAlign="Center" CssClass="grid" />
                                                                </asp:TemplateField>
                                                                <asp:TemplateField HeaderText="<%$ Resources:Attendance,Value %>">
                                                                    <ItemTemplate>
                                                                        <asp:TextBox ID="txtgvTaxV" Width="45px" runat="server" Text='<%#Eval("TaxV") %>' />
                                                                    </ItemTemplate>
                                                                    <ItemStyle HorizontalAlign="Center" CssClass="grid" />
                                                                </asp:TemplateField>
                                                                <asp:TemplateField HeaderText="<%$ Resources:Attendance,After Price %>">
                                                                    <ItemTemplate>
                                                                        <asp:TextBox ID="txtgvPriceAfterTax" Width="60px" ReadOnly="true" runat="server" />
                                                                    </ItemTemplate>
                                                                    <ItemStyle HorizontalAlign="Center" CssClass="grid" />
                                                                </asp:TemplateField>
                                                                <asp:TemplateField HeaderText="<%$ Resources:Attendance,% %>">
                                                                    <ItemTemplate>
                                                                        <asp:TextBox ID="txtgvDiscountP" Width="30px" Text='<%#Eval("DiscountP") %>' runat="server" />
                                                                    </ItemTemplate>
                                                                    <ItemStyle HorizontalAlign="Center" CssClass="grid" />
                                                                </asp:TemplateField>
                                                                <asp:TemplateField HeaderText="<%$ Resources:Attendance,Value %>">
                                                                    <ItemTemplate>
                                                                        <asp:TextBox ID="txtgvDiscountV" Width="45px" Text='<%#Eval("DiscountV") %>' runat="server" />
                                                                    </ItemTemplate>
                                                                    <ItemStyle HorizontalAlign="Center" CssClass="grid" />
                                                                </asp:TemplateField>
                                                                <asp:TemplateField HeaderText="<%$ Resources:Attendance,After Price %>">
                                                                    <ItemTemplate>
                                                                        <asp:TextBox ID="txtgvPriceAfterDiscount" Width="60px" ReadOnly="true" runat="server" />
                                                                    </ItemTemplate>
                                                                    <ItemStyle HorizontalAlign="Center" CssClass="grid" />
                                                                </asp:TemplateField>
                                                                <asp:TemplateField>
                                                                    <ItemTemplate>
                                                                        <asp:TextBox ID="txtgvTotal" Width="60px" ReadOnly="true" runat="server" />
                                                                    </ItemTemplate>
                                                                    <ItemStyle HorizontalAlign="Center" CssClass="grid" />
                                                                </asp:TemplateField>
                                                            </Columns>
                                                            <HeaderStyle CssClass="Invgridheader" />
                                                            <PagerStyle CssClass="Invgridheader" />
                                                            <RowStyle CssClass="Invgridrow" HorizontalAlign="Center" />
                                                        </asp:GridView>
                                                    </asp:Panel>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td align='<%= Common.ChangeTDForDefaultLeft()%>'>
                                                    <asp:Label ID="lblRemark" runat="server" CssClass="labelComman" Text="<%$ Resources:Attendance,Remark %>" />
                                                </td>
                                                <td align="center">
                                                    :
                                                </td>
                                                <td align='<%= Common.ChangeTDForDefaultLeft()%>' colspan="4">
                                                    <asp:TextBox ID="txtRemark" runat="server" CssClass="textComman" TextMode="MultiLine"
                                                        Width="722px" ReadOnly="true" />
                                                </td>
                                            </tr>
                                            <tr>
                                                <td colspan="6" align="center" style="padding-left: 10px">
                                                    <table>
                                                        <tr>
                                                            <td width="90px">
                                                                <asp:Button ID="btnSInvSave" runat="server" CssClass="buttonCommman" OnClick="btnSInvSave_Click"
                                                                    Text="<%$ Resources:Attendance,Save %>" />
                                                            </td>
                                                            <td width="90px">
                                                                <asp:Button ID="BtnReset" runat="server" Text="<%$ Resources:Attendance,Reset %>"
                                                                    CssClass="buttonCommman" CausesValidation="False" OnClick="BtnReset_Click" />
                                                            </td>
                                                            <td width="90px">
                                                                <asp:Button ID="btnSInvCancel" runat="server" CssClass="buttonCommman" Text="<%$ Resources:Attendance,Cancel %>"
                                                                    CausesValidation="False" OnClick="btnSInvCancel_Click" />
                                                            </td>
                                                        </tr>
                                                    </table>
                                                    <asp:HiddenField ID="editid" runat="server" />
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
                                                            <asp:ListItem Text="<%$ Resources:Attendance, Invoice Id %>" Value="Trans_Id"></asp:ListItem>
                                                            <asp:ListItem Text="<%$ Resources:Attendance, Invoice No %>" Value="Invoice_No" Selected="True"></asp:ListItem>
                                                            <asp:ListItem Text="<%$ Resources:Attendance, Invoice Date %>" Value="Invoice_Date"></asp:ListItem>
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
                                        <asp:GridView ID="GvSalesReturnBin" PageSize="10" runat="server" AutoGenerateColumns="False"
                                            Width="100%" AllowPaging="True" OnPageIndexChanging="GvSalesReturnBin_PageIndexChanging"
                                            OnSorting="GvSalesReturnBin_OnSorting" AllowSorting="true" CssClass="grid">
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
                                                <asp:TemplateField HeaderText="<%$ Resources:Attendance,Invoice No. %>" SortExpression="Invoice_No">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblgvSInvNo" runat="server" Text='<%#Eval("Invoice_No") %>' />
                                                        <asp:HiddenField ID="hdnTransId" runat="server" Value='<%#Eval("Trans_Id") %>' />
                                                    </ItemTemplate>
                                                    <ItemStyle HorizontalAlign="Center" CssClass="grid" />
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="<%$ Resources:Attendance,Invoice Date %>" SortExpression="Invoice_Date">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblgvSInvDate" runat="server" Text='<%#GetDate(Eval("Invoice_Date").ToString()) %>' />
                                                    </ItemTemplate>
                                                    <ItemStyle HorizontalAlign="Center" CssClass="grid" />
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="<%$ Resources:Attendance,Sales Person %>" SortExpression="SalesPerson_Id">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblgvSalesPerson" runat="server" Text='<%#Eval("SalesPerson_Id") %>' />
                                                    </ItemTemplate>
                                                    <ItemStyle HorizontalAlign="Center" CssClass="grid" />
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="<%$ Resources:Attendance,Transfer Type%>" SortExpression="SIFromTransType">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblgvTransType" runat="server" Text='<%#Eval("SIFromTransType") %>' />
                                                    </ItemTemplate>
                                                    <ItemStyle HorizontalAlign="Center" CssClass="grid" />
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="<%$ Resources:Attendance,Transfer No.%>" SortExpression="SIFromTransNo">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblgvTransNo" runat="server" Text='<%#Eval("SIFromTransNo") %>' />
                                                    </ItemTemplate>
                                                    <ItemStyle HorizontalAlign="Center" CssClass="grid" />
                                                </asp:TemplateField>
                                            </Columns>
                                            <AlternatingRowStyle CssClass="InvgridAltRow"></AlternatingRowStyle>
                                            <HeaderStyle CssClass="Invgridheader" />
                                            <PagerStyle CssClass="Invgridheader" />
                                            <RowStyle CssClass="Invgridrow" HorizontalAlign="Center"></RowStyle>
                                        </asp:GridView>
                                        <asp:HiddenField ID="HDFSortbin" runat="server" />
                                    </asp:Panel>
                                    <asp:Panel ID="pnlRequest" runat="server">
                                        <asp:GridView ID="GvSalesOrder" PageSize="10" runat="server" AutoGenerateColumns="False"
                                            Width="100%" AllowPaging="True" OnPageIndexChanging="GvSalesOrder_PageIndexChanging"
                                            AllowSorting="True" OnSorting="GvSalesOrder_Sorting" CssClass="grid">
                                            <AlternatingRowStyle CssClass="InvgridAltRow" />
                                            <Columns>
                                                <asp:TemplateField>
                                                    <ItemTemplate>
                                                        <asp:ImageButton ID="btnEdit" runat="server" CommandArgument='<%# Eval("Trans_Id") %>'
                                                            ImageUrl="~/Images/edit.png" OnCommand="btnSIEdit_Command" CausesValidation="False" />
                                                    </ItemTemplate>
                                                    <ItemStyle CssClass="grid" HorizontalAlign="Center" />
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="<%$ Resources:Attendance,Order Id %>" SortExpression="Trans_Id">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblgvSQuotationId" runat="server" Text='<%#Eval("Trans_Id") %>' />
                                                    </ItemTemplate>
                                                    <ItemStyle HorizontalAlign="Center" CssClass="grid" />
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="<%$ Resources:Attendance,Order No. %>" SortExpression="SalesOrderNo">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblgvSINo" runat="server" Text='<%#Eval("SalesOrderNo") %>' />
                                                    </ItemTemplate>
                                                    <ItemStyle HorizontalAlign="Center" CssClass="grid" />
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="<%$ Resources:Attendance,Order Date %>" SortExpression="SalesOrderDate">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblgvQuotationDate" runat="server" Text='<%#GetDate(Eval("SalesOrderDate").ToString()) %>' />
                                                    </ItemTemplate>
                                                    <ItemStyle HorizontalAlign="Center" CssClass="grid" />
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="<%$ Resources:Attendance, Customer Name %>" SortExpression="CustomerId">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblgvCustomerName" runat="server" Text='<%#GetCustomerName(Eval("CustomerId").ToString()) %>' />
                                                    </ItemTemplate>
                                                    <ItemStyle HorizontalAlign="Center" CssClass="grid" />
                                                </asp:TemplateField>
                                            </Columns>
                                            <HeaderStyle CssClass="Invgridheader" />
                                            <PagerStyle CssClass="Invgridheader" />
                                            <RowStyle CssClass="Invgridrow" HorizontalAlign="Center" />
                                        </asp:GridView>
                                        <asp:HiddenField ID="hdnSalesOrderId" runat="server" Value="0" />
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
