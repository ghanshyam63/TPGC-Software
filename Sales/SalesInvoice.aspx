<%@ Page Language="C#" MasterPageFile="~/ERPMaster.master" AutoEventWireup="true"
    CodeFile="SalesInvoice.aspx.cs" Inherits="Sales_SalesInvoice" Title="Untitled Page" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <link href="../CSS/AJAX.css" rel="stylesheet" type="text/css" />
    <asp:UpdatePanel ID="upallpage" runat="server">
        <Triggers>
            <asp:PostBackTrigger ControlID="btnList" />
            <asp:PostBackTrigger ControlID="btnNew" />
            <asp:PostBackTrigger ControlID="btnBin" />
            <asp:PostBackTrigger ControlID="btnSInvSave" />
          
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
                                            <asp:GridView ID="GvSalesInvoice" PageSize="10" runat="server" AutoGenerateColumns="False"
                                                Width="100%" AllowPaging="True" OnPageIndexChanging="GvSalesInvoice_PageIndexChanging"
                                                AllowSorting="True" OnSorting="GvSalesInvoice_Sorting" CssClass="grid">
                                                <AlternatingRowStyle CssClass="InvgridAltRow" />
                                                <Columns>
                                                    <asp:TemplateField>
                                                        <ItemTemplate>
                                                            <asp:ImageButton ID="btnEdit" runat="server" CommandArgument='<%# Eval("Trans_Id") %>'
                                                                ImageUrl="~/Images/edit.png" OnCommand="btnEdit_Command" CausesValidation="False" /></ItemTemplate>
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
                                                    <asp:Label ID="lblSInvDate" runat="server" CssClass="labelComman" Text="<%$ Resources:Attendance,Invoice Date %>"></asp:Label>
                                                </td>
                                                <td align="center">
                                                    :
                                                </td>
                                                <td align='<%= Common.ChangeTDForDefaultLeft()%>'>
                                                    <asp:TextBox ID="txtSInvDate" runat="server" CssClass="textComman" />
                                                    <cc1:CalendarExtender ID="Calender" runat="server" TargetControlID="txtSInvDate"
                                                        Format="dd/MMM/yyyy" />
                                                </td>
                                                <td align='<%= Common.ChangeTDForDefaultLeft()%>'>
                                                    <asp:Label ID="lblSInvNo" runat="server" CssClass="labelComman" Text="<%$ Resources:Attendance,Invoice No. %>"></asp:Label>
                                                </td>
                                                <td align="center">
                                                    :
                                                </td>
                                                <td align='<%= Common.ChangeTDForDefaultLeft()%>'>
                                                    <asp:TextBox ID="txtSInvNo" runat="server" CssClass="textComman" />
                                                </td>
                                            </tr>
                                            <tr>
                                                <td align='<%= Common.ChangeTDForDefaultLeft()%>'>
                                                    <asp:Label ID="lblOrderType" runat="server" CssClass="labelComman" Text="<%$ Resources:Attendance,Order Type %>" />
                                                </td>
                                                <td align="center">
                                                    :
                                                </td>
                                                <td align='<%= Common.ChangeTDForDefaultLeft()%>'>
                                                    <asp:DropDownList ID="ddlOrderType" runat="server" Width="262px" CssClass="textComman"
                                                        OnSelectedIndexChanged="ddlOrderType_SelectedIndexChanged" AutoPostBack="true">
                                                        <asp:ListItem Text="--Select--" Value="--Select--"></asp:ListItem>
                                                        <asp:ListItem Text="Direct" Value="D"></asp:ListItem>
                                                        <asp:ListItem Text="By Sales Order" Value="Q"></asp:ListItem>
                                                    </asp:DropDownList>
                                                </td>
                                                  <td align='<%= Common.ChangeTDForDefaultLeft()%>'>
                                                    <asp:CheckBox ID="chkPost" runat="server" Text="Post" CssClass="labelComman" />
                                                </td>
                                            </tr>
                                            <tr id="trTransfer" runat="server" visible="false">
                                                <td align='<%= Common.ChangeTDForDefaultLeft()%>'>
                                                    <asp:Label ID="lblTransFrom" runat="server" CssClass="labelComman" Text="<%$ Resources:Attendance,Transfer From %>" />
                                                </td>
                                                <td align="center">
                                                    :
                                                </td>
                                                <td align='<%= Common.ChangeTDForDefaultLeft()%>'>
                                                    <asp:TextBox ID="txtTransFrom" runat="server" CssClass="textComman" ReadOnly="True" />
                                                </td>
                                                <td align='<%= Common.ChangeTDForDefaultLeft()%>'>
                                                    <asp:Label ID="lblSalesOrderNo" runat="server" CssClass="labelComman" Text="<%$ Resources:Attendance,Sales Order No. %>" />
                                                </td>
                                                <td align="center">
                                                    :
                                                </td>
                                                <td align='<%= Common.ChangeTDForDefaultLeft()%>'>
                                                    <asp:DropDownList ID="ddlSalesOrderNo" Width="262px" runat="server" CssClass="textComman"
                                                        Visible="false" OnSelectedIndexChanged="ddlSalesOrderNo_SelectedIndexChanged"
                                                        AutoPostBack="true" />
                                                    <asp:TextBox ID="txtSalesOrderNo" runat="server" ReadOnly="true" CssClass="textComman"
                                                        Visible="false" />
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
                                                    <asp:TextBox ID="txtCustomer" runat="server" CssClass="textComman" Width="722px"
                                                        BackColor="#c3c3c3" />
                                                    <cc1:AutoCompleteExtender ID="txtCustomerName_AutoCompleteExtender" runat="server"
                                                        DelimiterCharacters="" Enabled="True" CompletionInterval="100" CompletionSetCount="1"
                                                        MinimumPrefixLength="1" ServiceMethod="GetCompletionListCustomer" ServicePath=""
                                                        TargetControlID="txtCustomer" UseContextKey="True" CompletionListCssClass="completionList"
                                                        CompletionListItemCssClass="listItem" CompletionListHighlightedItemCssClass="itemHighlighted">
                                                    </cc1:AutoCompleteExtender>
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
                                                    <asp:DropDownList ID="ddlPaymentMode" Width="262px" runat="server" CssClass="textComman" />
                                                </td>
                                                <td align='<%= Common.ChangeTDForDefaultLeft()%>'>
                                                    <asp:Label ID="lblCurrency" runat="server" CssClass="labelComman" Text="<%$ Resources:Attendance,Currency %>" />
                                                </td>
                                                <td align="center">
                                                    :
                                                </td>
                                                <td align='<%= Common.ChangeTDForDefaultLeft()%>'>
                                                    <asp:DropDownList ID="ddlCurrency" runat="server" CssClass="textComman" Width="262px" />
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
                                                    <asp:TextBox ID="txtSalesPerson" runat="server" CssClass="textComman" BackColor="#c3c3c3"
                                                        OnTextChanged="txtSalesPerson_TextChanged" AutoPostBack="true" />
                                                    <cc1:AutoCompleteExtender ID="txtSalesPerson_AutoCompleteExtender" runat="server"
                                                        DelimiterCharacters="" Enabled="True" CompletionInterval="100" CompletionSetCount="1"
                                                        MinimumPrefixLength="1" ServiceMethod="GetCompletionListEmployeeName" ServicePath=""
                                                        TargetControlID="txtSalesPerson" UseContextKey="True" CompletionListCssClass="completionList"
                                                        CompletionListItemCssClass="listItem" CompletionListHighlightedItemCssClass="itemHighlighted">
                                                    </cc1:AutoCompleteExtender>
                                                </td>
                                                <td align='<%= Common.ChangeTDForDefaultLeft()%>'>
                                                    <asp:Label ID="lblPOSNo" runat="server" CssClass="labelComman" Text="<%$ Resources:Attendance,POS No.%>" />
                                                </td>
                                                <td align="center">
                                                    :
                                                </td>
                                                <td align='<%= Common.ChangeTDForDefaultLeft()%>'>
                                                    <asp:TextBox ID="txtPOSNo" runat="server" CssClass="textComman" />
                                                    <cc1:FilteredTextBoxExtender ID="FilteredTextBoxExtender5" runat="server" Enabled="True"
                                                        TargetControlID="txtPOSNo" ValidChars="1,2,3,4,5,6,7,8,9,0,.">
                                                    </cc1:FilteredTextBoxExtender>
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
                                                    <asp:TextBox ID="txtAccountNo" runat="server" CssClass="textComman" />
                                                    <cc1:FilteredTextBoxExtender ID="FilteredTextBoxExtender1" runat="server" Enabled="True"
                                                        TargetControlID="txtAccountNo" ValidChars="1,2,3,4,5,6,7,8,9,0,.">
                                                    </cc1:FilteredTextBoxExtender>
                                                </td>
                                                <td align='<%= Common.ChangeTDForDefaultLeft()%>'>
                                                    <asp:Label ID="lblInvoiceCosting" runat="server" CssClass="labelComman" Text="<%$ Resources:Attendance,Invoice Costing%>" />
                                                </td>
                                                <td align="center">
                                                    :
                                                </td>
                                                <td align='<%= Common.ChangeTDForDefaultLeft()%>'>
                                                    <asp:TextBox ID="txtInvoiceCosting" runat="server" CssClass="textComman" />
                                                    <cc1:FilteredTextBoxExtender ID="FilteredTextBoxExtender2" runat="server" Enabled="True"
                                                        TargetControlID="txtInvoiceCosting" ValidChars="1,2,3,4,5,6,7,8,9,0,.">
                                                    </cc1:FilteredTextBoxExtender>
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
                                                    <asp:TextBox ID="txtShift" runat="server" CssClass="textComman" />
                                                </td>
                                                <td align='<%= Common.ChangeTDForDefaultLeft()%>'>
                                                    <asp:Label ID="lblTender" runat="server" CssClass="labelComman" Text="<%$ Resources:Attendance,Tender%>" />
                                                </td>
                                                <td align="center">
                                                    :
                                                </td>
                                                <td align='<%= Common.ChangeTDForDefaultLeft()%>'>
                                                    <asp:TextBox ID="txtTender" runat="server" CssClass="textComman" />
                                                    <cc1:FilteredTextBoxExtender ID="FilteredTextBoxExtender3" runat="server" Enabled="True"
                                                        TargetControlID="txtTender" ValidChars="1,2,3,4,5,6,7,8,9,0,.">
                                                    </cc1:FilteredTextBoxExtender>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td>
                                                </td>
                                                <td>
                                                </td>
                                                <td>
                                                </td>
                                                <td>
                                                </td>
                                                <td>
                                                </td>
                                                <td>
                                                    <asp:Button ID="btnAddNewProduct" runat="server" Text="<%$ Resources:Attendance,Add Product %>"
                                                        BackColor="Transparent" Visible="false" BorderStyle="None" Font-Bold="true" CssClass="btnLocation"
                                                        Width="171px" Font-Size="13px" Font-Names=" Arial" Height="32px" OnClick="btnAddNewProduct_Click" />
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
                                                                <asp:TemplateField HeaderText="<%$ Resources:Attendance,Name %>">
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
                                                                        <asp:TextBox ID="txtgvUnitCost" runat="server" Width="50px" CssClass="textCommanSearch" Height="10px" />
                                                                        <cc1:FilteredTextBoxExtender ID="FilteredTextBoxExtender5" runat="server" Enabled="True"
                                                                            TargetControlID="txtgvUnitCost" ValidChars="1,2,3,4,5,6,7,8,9,0,.">
                                                                        </cc1:FilteredTextBoxExtender>
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
                                                                        <asp:TextBox ID="txtgvTaxP" Width="30px" Text='<%#Eval("TaxP") %>' runat="server" Height="10px"
                                                                            OnTextChanged="txtgvTaxP_TextChanged" AutoPostBack="true" CssClass="textCommanSearch" />
                                                                        <cc1:FilteredTextBoxExtender ID="FilterTaxP" runat="server" Enabled="True" TargetControlID="txtgvTaxP"
                                                                            ValidChars="1,2,3,4,5,6,7,8,9,0,.">
                                                                        </cc1:FilteredTextBoxExtender>
                                                                    </ItemTemplate>
                                                                    <ItemStyle HorizontalAlign="Center" CssClass="grid" />
                                                                </asp:TemplateField>
                                                                <asp:TemplateField HeaderText="<%$ Resources:Attendance,Value %>">
                                                                    <ItemTemplate>
                                                                        <asp:TextBox ID="txtgvTaxV" Width="45px" runat="server" OnTextChanged="txtgvTaxV_TextChanged" Height="10px"
                                                                            AutoPostBack="true" Text='<%#Eval("TaxV") %>' CssClass="textCommanSearch" />
                                                                        <cc1:FilteredTextBoxExtender ID="FilterTaxV" runat="server" Enabled="True" TargetControlID="txtgvTaxV"
                                                                            ValidChars="1,2,3,4,5,6,7,8,9,0,.">
                                                                        </cc1:FilteredTextBoxExtender>
                                                                    </ItemTemplate>
                                                                    <ItemStyle HorizontalAlign="Center" CssClass="grid" />
                                                                </asp:TemplateField>
                                                                <asp:TemplateField HeaderText="<%$ Resources:Attendance,After Price %>">
                                                                    <ItemTemplate>
                                                                        <asp:TextBox ID="txtgvPriceAfterTax" Width="60px" ReadOnly="true" runat="server" CssClass="textCommanSearch" Height="10px" />
                                                                        <cc1:FilteredTextBoxExtender ID="FilterPriceAfterTax" runat="server" Enabled="True"
                                                                            TargetControlID="txtgvPriceAfterTax" ValidChars="1,2,3,4,5,6,7,8,9,0,.">
                                                                        </cc1:FilteredTextBoxExtender>
                                                                    </ItemTemplate>
                                                                    <ItemStyle HorizontalAlign="Center" CssClass="grid" />
                                                                </asp:TemplateField>
                                                                <asp:TemplateField HeaderText="<%$ Resources:Attendance,% %>">
                                                                    <ItemTemplate>
                                                                        <asp:TextBox ID="txtgvDiscountP" Width="30px" Text='<%#Eval("DiscountP") %>' runat="server" Height="10px"
                                                                            OnTextChanged="txtgvDiscountP_TextChanged" AutoPostBack="true" CssClass="textCommanSearch" />
                                                                        <cc1:FilteredTextBoxExtender ID="FilterDiscountP" runat="server" Enabled="True" TargetControlID="txtgvDiscountP"
                                                                            ValidChars="1,2,3,4,5,6,7,8,9,0,.">
                                                                        </cc1:FilteredTextBoxExtender>
                                                                    </ItemTemplate>
                                                                    <ItemStyle HorizontalAlign="Center" CssClass="grid" />
                                                                </asp:TemplateField>
                                                                <asp:TemplateField HeaderText="<%$ Resources:Attendance,Value %>">
                                                                    <ItemTemplate>
                                                                        <asp:TextBox ID="txtgvDiscountV" Width="45px" Text='<%#Eval("DiscountV") %>' runat="server" Height="10px"
                                                                            OnTextChanged="txtgvDiscountV_TextChanged" AutoPostBack="true" CssClass="textCommanSearch" />
                                                                        <cc1:FilteredTextBoxExtender ID="FilterDiscountV" runat="server" Enabled="True" TargetControlID="txtgvDiscountV"
                                                                            ValidChars="1,2,3,4,5,6,7,8,9,0,.">
                                                                        </cc1:FilteredTextBoxExtender>
                                                                    </ItemTemplate>
                                                                    <ItemStyle HorizontalAlign="Center" CssClass="grid" />
                                                                </asp:TemplateField>
                                                                <asp:TemplateField HeaderText="<%$ Resources:Attendance,After Price %>">
                                                                    <ItemTemplate>
                                                                        <asp:TextBox ID="txtgvPriceAfterDiscount" Width="60px" ReadOnly="true" runat="server" CssClass="textCommanSearch" Height="10px" />
                                                                        <cc1:FilteredTextBoxExtender ID="FilterPriceAfterDis" runat="server" Enabled="True"
                                                                            TargetControlID="txtgvPriceAfterDiscount" ValidChars="1,2,3,4,5,6,7,8,9,0,.">
                                                                        </cc1:FilteredTextBoxExtender>
                                                                    </ItemTemplate>
                                                                    <ItemStyle HorizontalAlign="Center" CssClass="grid" />
                                                                </asp:TemplateField>
                                                                <asp:TemplateField>
                                                                    <ItemTemplate>
                                                                        <asp:TextBox ID="txtgvTotal" Width="60px" ReadOnly="true" runat="server" CssClass="textCommanSearch" Height="10px" />
                                                                        <cc1:FilteredTextBoxExtender ID="FilterTotal" runat="server" Enabled="True" TargetControlID="txtgvTotal"
                                                                            ValidChars="1,2,3,4,5,6,7,8,9,0,.">
                                                                        </cc1:FilteredTextBoxExtender>
                                                                    </ItemTemplate>
                                                                    <ItemStyle HorizontalAlign="Center" CssClass="grid" />
                                                                </asp:TemplateField>
                                                            </Columns>
                                                            <HeaderStyle CssClass="Invgridheader" />
                                                            <PagerStyle CssClass="Invgridheader" />
                                                            <RowStyle CssClass="Invgridrow" HorizontalAlign="Center" />
                                                        </asp:GridView>
                                                        <asp:GridView ID="GvProductDetail" runat="server" AutoGenerateColumns="False" Width="100%"
                                                            CssClass="grid" OnRowCreated="GvProductDetail_RowCreated">
                                                            <AlternatingRowStyle CssClass="InvgridAltRow" />
                                                            <Columns>
                                                                <asp:TemplateField>
                                                                    <ItemTemplate>
                                                                        <asp:ImageButton ID="imgBtnProductEdit" runat="server" CommandArgument='<%# Eval("Serial_No") %>'
                                                                            ImageUrl="~/Images/edit.png" Width="16px" OnCommand="imgBtnProductEdit_Command" />
                                                                    </ItemTemplate>
                                                                    <ItemStyle CssClass="grid" HorizontalAlign="Center" />
                                                                </asp:TemplateField>
                                                                <asp:TemplateField>
                                                                    <ItemTemplate>
                                                                        <asp:ImageButton ID="imgBtnProductDelete" runat="server" CommandArgument='<%# Eval("Serial_No") %>'
                                                                            Height="16px" ImageUrl="~/Images/Erase.png" Width="16px" OnCommand="imgBtnProductDelete_Command" />
                                                                    </ItemTemplate>
                                                                    <ItemStyle CssClass="grid" HorizontalAlign="Center" />
                                                                </asp:TemplateField>
                                                                <asp:TemplateField>
                                                                    <ItemTemplate>
                                                                        <asp:Label ID="lblgvSerialNo" runat="server" Text='<%#Eval("Serial_No") %>' />
                                                                    </ItemTemplate>
                                                                    <ItemStyle HorizontalAlign="Center" CssClass="grid" Width="30px" />
                                                                </asp:TemplateField>
                                                                <asp:TemplateField HeaderText="<%$ Resources:Attendance,Product Name %>">
                                                                    <ItemTemplate>
                                                                        <asp:HiddenField ID="hdngvProductId" runat="server" Value='<%#Eval("Product_Id") %>' />
                                                                        <asp:Label ID="lblgvProductName" runat="server" Text='<%#GetProductName(Eval("Product_Id").ToString()) %>' />
                                                                    </ItemTemplate>
                                                                    <ItemStyle HorizontalAlign="Center" CssClass="grid" />
                                                                </asp:TemplateField>
                                                                <asp:TemplateField HeaderText="<%$ Resources:Attendance,Description %>">
                                                                    <ItemTemplate>
                                                                        <asp:Label ID="lblgvProductDescription" runat="server" Text='<%#Eval("ProductDescription") %>' />
                                                                    </ItemTemplate>
                                                                    <ItemStyle HorizontalAlign="Center" CssClass="grid" />
                                                                </asp:TemplateField>
                                                                <asp:TemplateField HeaderText="<%$ Resources:Attendance,Name %>">
                                                                    <ItemTemplate>
                                                                        <asp:HiddenField ID="hdngvUnitId" runat="server" Value='<%#Eval("Unit_Id") %>' />
                                                                        <asp:Label ID="lblgvUnit" runat="server" Text='<%#GetUnitCode(Eval("Unit_Id").ToString()) %>' />
                                                                    </ItemTemplate>
                                                                    <ItemStyle HorizontalAlign="Center" CssClass="grid" />
                                                                </asp:TemplateField>
                                                                <asp:TemplateField HeaderText="<%$ Resources:Attendance,Unit Price %>">
                                                                    <ItemTemplate>
                                                                        <asp:Label ID="lblgvUnitPrice" runat="server" Text='<%#Eval("UnitPrice") %>' />
                                                                    </ItemTemplate>
                                                                    <ItemStyle HorizontalAlign="Center" CssClass="grid" />
                                                                </asp:TemplateField>
                                                                <asp:TemplateField HeaderText="<%$ Resources:Attendance,Unit Cost %>">
                                                                    <ItemTemplate>
                                                                        <asp:Label ID="lblgvUnitCost" runat="server" Text='<%#Eval("UnitCost") %>' />
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
                                                                        <asp:Label ID="lblgvTaxP" runat="server" Text='<%#Eval("TaxP") %>' />
                                                                    </ItemTemplate>
                                                                    <ItemStyle HorizontalAlign="Center" CssClass="grid" />
                                                                </asp:TemplateField>
                                                                <asp:TemplateField HeaderText="<%$ Resources:Attendance,Value %>">
                                                                    <ItemTemplate>
                                                                        <asp:Label ID="lblgvTaxV" runat="server" Text='<%#Eval("TaxV") %>' />
                                                                    </ItemTemplate>
                                                                    <ItemStyle HorizontalAlign="Center" CssClass="grid" />
                                                                </asp:TemplateField>
                                                                <asp:TemplateField HeaderText="<%$ Resources:Attendance,After Price %>">
                                                                    <ItemTemplate>
                                                                        <asp:Label ID="lblgvPriceAfterTax" runat="server" />
                                                                    </ItemTemplate>
                                                                    <ItemStyle HorizontalAlign="Center" CssClass="grid" />
                                                                </asp:TemplateField>
                                                                <asp:TemplateField HeaderText="<%$ Resources:Attendance,% %>">
                                                                    <ItemTemplate>
                                                                        <asp:Label ID="lblgvDiscountP" Width="30px" runat="server" Text='<%#Eval("DiscountP") %>' />
                                                                    </ItemTemplate>
                                                                    <ItemStyle HorizontalAlign="Center" CssClass="grid" />
                                                                </asp:TemplateField>
                                                                <asp:TemplateField HeaderText="<%$ Resources:Attendance,Value %>">
                                                                    <ItemTemplate>
                                                                        <asp:Label ID="lblgvDiscountV" runat="server" Text='<%#Eval("DiscountV") %>' />
                                                                    </ItemTemplate>
                                                                    <ItemStyle HorizontalAlign="Center" CssClass="grid" />
                                                                </asp:TemplateField>
                                                                <asp:TemplateField HeaderText="<%$ Resources:Attendance,After Price %>">
                                                                    <ItemTemplate>
                                                                        <asp:Label ID="lblgvPriceAfterDiscount" runat="server" />
                                                                    </ItemTemplate>
                                                                    <ItemStyle HorizontalAlign="Center" CssClass="grid" />
                                                                </asp:TemplateField>
                                                                <asp:TemplateField>
                                                                    <ItemTemplate>
                                                                        <asp:Label ID="lblgvTotal" runat="server" />
                                                                    </ItemTemplate>
                                                                    <ItemStyle HorizontalAlign="Center" CssClass="grid" />
                                                                </asp:TemplateField>
                                                            </Columns>
                                                            <HeaderStyle CssClass="Invgridheader" />
                                                            <PagerStyle CssClass="Invgridheader" />
                                                            <RowStyle CssClass="Invgridrow" HorizontalAlign="Center" />
                                                        </asp:GridView>
                                                        <asp:HiddenField ID="hdnProductId" runat="server" />
                                                        <asp:HiddenField ID="hdnProductName" runat="server" />
                                                    </asp:Panel>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td align='<%= Common.ChangeTDForDefaultLeft()%>'>
                                                    <asp:Label ID="lblAmount" runat="server" CssClass="labelComman" Text="<%$ Resources:Attendance, Amount %>" />
                                                </td>
                                                <td>
                                                    :
                                                </td>
                                                <td align='<%= Common.ChangeTDForDefaultLeft()%>'>
                                                    <asp:TextBox ID="txtAmount" runat="server" ReadOnly="true" CssClass="textComman"
                                                        OnTextChanged="txtAmount_TextChanged" AutoPostBack="true" />
                                                </td>
                                                <td align='<%= Common.ChangeTDForDefaultLeft()%>'>
                                                    <asp:Label ID="lblTotalQuantity" runat="server" CssClass="labelComman" Text="<%$ Resources:Attendance, Total Quantity %>" />
                                                </td>
                                                <td>
                                                    :
                                                </td>
                                                <td align='<%= Common.ChangeTDForDefaultLeft()%>'>
                                                    <asp:TextBox ID="txtTotalQuantity" runat="server" CssClass="textComman" ReadOnly="true" />
                                                    <cc1:FilteredTextBoxExtender ID="FilteredTextBoxExtender4" runat="server" Enabled="True"
                                                        TargetControlID="txtTotalQuantity" ValidChars="1,2,3,4,5,6,7,8,9,0,.">
                                                    </cc1:FilteredTextBoxExtender>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td align='<%= Common.ChangeTDForDefaultLeft()%>'>
                                                    <asp:Label ID="lblTaxP" runat="server" CssClass="labelComman" Text="<%$ Resources:Attendance,Tax(%) %>" />
                                                </td>
                                                <td align="center">
                                                    :
                                                </td>
                                                <td align='<%= Common.ChangeTDForDefaultLeft()%>'>
                                                    <asp:TextBox ID="txtTaxP" runat="server" Width="100px" CssClass="textComman" OnTextChanged="txtTaxP_TextChanged"
                                                        AutoPostBack="true" />
                                                    <cc1:FilteredTextBoxExtender ID="FilteredTextBoxExtender6" runat="server" Enabled="True"
                                                        TargetControlID="txtTaxP" ValidChars="1,2,3,4,5,6,7,8,9,0,.">
                                                    </cc1:FilteredTextBoxExtender>
                                                      <asp:Label ID="Label2" runat="server" CssClass="labelComman" Text="<%$ Resources:Attendance,Value %>" />
                                              
                                                    <asp:TextBox ID="txtTaxV" runat="server" Width="100px" CssClass="textComman" OnTextChanged="txtTaxV_TextChanged"
                                                        AutoPostBack="true" />
                                                    <cc1:FilteredTextBoxExtender ID="FilteredTextBoxExtender7" runat="server" Enabled="True"
                                                        TargetControlID="txtTaxV" ValidChars="1,2,3,4,5,6,7,8,9,0,.">
                                                    </cc1:FilteredTextBoxExtender>
                                                </td>
                                                <td align='<%= Common.ChangeTDForDefaultLeft()%>'>
                                                    <asp:Label ID="lblNetAmount" runat="server" CssClass="labelComman" Text="<%$ Resources:Attendance,Net Amount %>" />
                                                </td>
                                                <td align="center">
                                                    :
                                                </td>
                                                <td align='<%= Common.ChangeTDForDefaultLeft()%>'>
                                                    <asp:TextBox ID="txtNetAmount" runat="server" CssClass="textComman" ReadOnly="True" />
                                                </td>
                                            </tr>
                                            <tr>
                                                <td align='<%= Common.ChangeTDForDefaultLeft()%>'>
                                                    <asp:Label ID="lblDiscountP" runat="server" CssClass="labelComman" Text="<%$ Resources:Attendance,Discount(%) %>" />
                                                </td>
                                                <td align="center">
                                                    :
                                                </td>
                                                <td align='<%= Common.ChangeTDForDefaultLeft()%>'>
                                                    <asp:TextBox ID="txtDiscountP" runat="server" Width="100px" CssClass="textComman"
                                                        OnTextChanged="txtDiscountP_TextChanged" AutoPostBack="true" />
                                                    <cc1:FilteredTextBoxExtender ID="FilteredTextBoxExtender8" runat="server" Enabled="True"
                                                        TargetControlID="txtDiscountP" ValidChars="1,2,3,4,5,6,7,8,9,0,.">
                                                    </cc1:FilteredTextBoxExtender>
                                                     <asp:Label ID="Label3" runat="server" CssClass="labelComman" Text="<%$ Resources:Attendance,Value %>" />
                                              
                                                    <asp:TextBox ID="txtDiscountV" runat="server" Width="100px" CssClass="textComman"
                                                        OnTextChanged="txtDiscountV_TextChanged" AutoPostBack="true" />
                                                    <cc1:FilteredTextBoxExtender ID="FilteredTextBoxExtender9" runat="server" Enabled="True"
                                                        TargetControlID="txtDiscountV" ValidChars="1,2,3,4,5,6,7,8,9,0,.">
                                                    </cc1:FilteredTextBoxExtender>
                                                </td>
                                                <td align='<%= Common.ChangeTDForDefaultLeft()%>'>
                                                    <asp:Label ID="lblGrandTotal" runat="server" CssClass="labelComman" Text="<%$ Resources:Attendance, Grand Total %>" />
                                                </td>
                                                <td align="center">
                                                    :
                                                </td>
                                                <td align='<%= Common.ChangeTDForDefaultLeft()%>'>
                                                    <asp:TextBox ID="txtGrandTotal" runat="server" CssClass="textComman" ReadOnly="True" />
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
                                                        Width="722px" />
                                                </td>
                                            </tr>
                                            <tr>
                                                <td align='<%= Common.ChangeTDForDefaultLeft()%>'>
                                                    <asp:Label ID="lblCondition1" runat="server" CssClass="labelComman" Text="<%$ Resources:Attendance,Condition 1 %>" />
                                                </td>
                                                <td align="center">
                                                    :
                                                </td>
                                                <td align='<%= Common.ChangeTDForDefaultLeft()%>' colspan="4">
                                                    <asp:TextBox ID="txtCondition1" runat="server" TextMode="MultiLine" CssClass="textComman"
                                                        Width="722px" />
                                                </td>
                                            </tr>
                                            <tr>
                                                <td align='<%= Common.ChangeTDForDefaultLeft()%>'>
                                                    <asp:Label ID="lblCondition2" runat="server" CssClass="labelComman" Text="<%$ Resources:Attendance,Condition 2 %>" />
                                                </td>
                                                <td align="center">
                                                    :
                                                </td>
                                                <td align='<%= Common.ChangeTDForDefaultLeft()%>' colspan="4">
                                                    <asp:TextBox ID="txtCondition2" runat="server" TextMode="MultiLine" CssClass="textComman"
                                                        Width="722px" />
                                                </td>
                                            </tr>
                                            <tr>
                                                <td align='<%= Common.ChangeTDForDefaultLeft()%>'>
                                                    <asp:Label ID="lblCondition3" runat="server" CssClass="labelComman" Text="<%$ Resources:Attendance,Condition 3 %>" />
                                                </td>
                                                <td align="center">
                                                    :
                                                </td>
                                                <td align='<%= Common.ChangeTDForDefaultLeft()%>' colspan="4">
                                                    <asp:TextBox ID="txtCondition3" runat="server" CssClass="textComman" TextMode="MultiLine"
                                                        Width="722px" />
                                                </td>
                                            </tr>
                                            <tr>
                                                <td align='<%= Common.ChangeTDForDefaultLeft()%>'>
                                                    <asp:Label ID="lblCondition4" runat="server" CssClass="labelComman" Text="<%$ Resources:Attendance,Condition 4 %>" />
                                                </td>
                                                <td align="center">
                                                    :
                                                </td>
                                                <td align='<%= Common.ChangeTDForDefaultLeft()%>' colspan="4">
                                                    <asp:TextBox ID="txtCondition4" runat="server" TextMode="MultiLine" CssClass="textComman"
                                                        Width="722px" />
                                                </td>
                                            </tr>
                                            <tr>
                                                <td align='<%= Common.ChangeTDForDefaultLeft()%>'>
                                                    <asp:Label ID="lblCondition5" runat="server" CssClass="labelComman" Text="<%$ Resources:Attendance,Condition 5 %>" />
                                                </td>
                                                <td align="center">
                                                    :
                                                </td>
                                                <td align='<%= Common.ChangeTDForDefaultLeft()%>' colspan="4">
                                                    <asp:TextBox ID="txtCondition5" runat="server" TextMode="MultiLine" CssClass="textComman"
                                                        Width="722px" />
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
                                        <asp:GridView ID="GvSalesInvoiceBin" PageSize="10" runat="server" AutoGenerateColumns="False"
                                            Width="100%" AllowPaging="True" OnPageIndexChanging="GvSalesInvoiceBin_PageIndexChanging"
                                            OnSorting="GvSalesInvoiceBin_OnSorting" AllowSorting="true" CssClass="grid">
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
                                                            <asp:TextBox ID="txtProductName" runat="server" CssClass="textComman" AutoPostBack="true"
                                                                OnTextChanged="txtProductName_TextChanged" BackColor="#c3c3c3" Width="610px" />
                                                            <cc1:AutoCompleteExtender ID="AutoCompleteExtender2" runat="server" DelimiterCharacters=""
                                                                Enabled="True" ServiceMethod="GetCompletionListProductName" ServicePath="" CompletionInterval="100"
                                                                MinimumPrefixLength="1" CompletionSetCount="1" TargetControlID="txtProductName"
                                                                UseContextKey="True" CompletionListCssClass="completionList" CompletionListItemCssClass="listItem"
                                                                CompletionListHighlightedItemCssClass="itemHighlighted">
                                                            </cc1:AutoCompleteExtender>
                                                            <asp:HiddenField ID="hdnNewProductId" runat="server" Value="0" />
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
                                                            <asp:DropDownList ID="ddlUnit" runat="server" CssClass="textComman" Width="260px" />
                                                            <asp:HiddenField ID="hdnUnitId" runat="server" Value="0" />
                                                        </td>
                                                        <td align='<%= Common.ChangeTDForDefaultLeft()%>'>
                                                            <asp:Label ID="lblPUnitPrice" runat="server" CssClass="labelComman" Text="<%$ Resources:Attendance,Unit Price %>" />
                                                        </td>
                                                        <td align="center">
                                                            :
                                                        </td>
                                                        <td align='<%= Common.ChangeTDForDefaultLeft()%>'>
                                                            <asp:TextBox ID="txtPUnitPrice" runat="server" CssClass="textComman" Width="150px"
                                                                OnTextChanged="txtPUnitPrice_TextChanged" AutoPostBack="true" />
                                                            <cc1:FilteredTextBoxExtender ID="FilteredTextBoxExtender17" runat="server" Enabled="True"
                                                                TargetControlID="txtPUnitPrice" ValidChars="1,2,3,4,5,6,7,8,9,0,.">
                                                            </cc1:FilteredTextBoxExtender>
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td align='<%= Common.ChangeTDForDefaultLeft()%>'>
                                                            <asp:Label ID="lblPUnitCost" runat="server" CssClass="labelComman" Text="<%$ Resources:Attendance,Unit Cost %>" />
                                                        </td>
                                                        <td align="center">
                                                            :
                                                        </td>
                                                        <td align='<%= Common.ChangeTDForDefaultLeft()%>'>
                                                            <asp:TextBox ID="txtPUnitCost" runat="server" CssClass="textComman" OnTextChanged="txtPUnitCost_TextChanged"
                                                                AutoPostBack="true" />
                                                            <cc1:FilteredTextBoxExtender ID="FilteredTextBoxExtender18" runat="server" Enabled="True"
                                                                TargetControlID="txtPUnitCost" ValidChars="1,2,3,4,5,6,7,8,9,0,.">
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
                                                            <%--<asp:TextBox ID="txtPDescription" Width="610px" TextMode="MultiLine" runat="server"
                                                                CssClass="textComman" ReadOnly="True" />--%>
                                                                   <asp:Panel ID="pnlPDescription" runat="server" Width="615px" Height="100px" CssClass="textComman"
                                                                BorderColor="#8ca7c1" BackColor="#ffffff" ScrollBars="Vertical" >
                                                                <asp:Literal ID="txtPDescription" runat="server"></asp:Literal>
                                                            </asp:Panel>
                                      
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td align='<%= Common.ChangeTDForDefaultLeft()%>'>
                                                            <asp:Label ID="lblPQuantity" runat="server" CssClass="labelComman" Text="<%$ Resources:Attendance,Quantity %>" />
                                                        </td>
                                                        <td align="center">
                                                            :
                                                        </td>
                                                        <td align='<%= Common.ChangeTDForDefaultLeft()%>'>
                                                            <asp:TextBox ID="txtPQuantity" runat="server" CssClass="textComman" OnTextChanged="txtPQuantity_TextChanged"
                                                                AutoPostBack="true" />
                                                            <cc1:FilteredTextBoxExtender ID="FilteredTextBoxExtender10" runat="server" Enabled="True"
                                                                TargetControlID="txtPQuantity" ValidChars="1,2,3,4,5,6,7,8,9,0,.">
                                                            </cc1:FilteredTextBoxExtender>
                                                        </td>
                                                        <td align='<%= Common.ChangeTDForDefaultLeft()%>'>
                                                            <asp:Label ID="lblPQuantityPrice" runat="server" CssClass="labelComman" Text="<%$ Resources:Attendance,Quantity Price %>" />
                                                        </td>
                                                        <td align="center">
                                                            :
                                                        </td>
                                                        <td align='<%= Common.ChangeTDForDefaultLeft()%>'>
                                                            <asp:TextBox ID="txtPQuantityPrice" runat="server" Width="150px" CssClass="textComman"
                                                                ReadOnly="true" />
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td align='<%= Common.ChangeTDForDefaultLeft()%>'>
                                                            <asp:Label ID="lblPTax" runat="server" CssClass="labelComman" Text="<%$ Resources:Attendance,Tax %>" />
                                                        </td>
                                                        <td align="center">
                                                            :
                                                        </td>
                                                        <td align='<%= Common.ChangeTDForDefaultLeft()%>'>
                                                            <asp:TextBox ID="txtPTaxP" runat="server" Width="65px" CssClass="textComman" OnTextChanged="txtPTaxP_TextChanged"
                                                                AutoPostBack="true" />(%)
                                                            <cc1:FilteredTextBoxExtender ID="FilteredTextBoxExtender11" runat="server" Enabled="True"
                                                                TargetControlID="txtPTaxP" ValidChars="1,2,3,4,5,6,7,8,9,0,.">
                                                            </cc1:FilteredTextBoxExtender>
                                                            <asp:TextBox ID="txtPTaxV" runat="server" Width="100px" CssClass="textComman" OnTextChanged="txtPTaxV_TextChanged"
                                                                AutoPostBack="true" />(Value)
                                                            <cc1:FilteredTextBoxExtender ID="FilteredTextBoxExtender12" runat="server" Enabled="True"
                                                                TargetControlID="txtPTaxV" ValidChars="1,2,3,4,5,6,7,8,9,0,.">
                                                            </cc1:FilteredTextBoxExtender>
                                                        </td>
                                                        <td align='<%= Common.ChangeTDForDefaultLeft()%>'>
                                                            <asp:Label ID="lblPPriceAfterTax" runat="server" CssClass="labelComman" Text="<%$ Resources:Attendance,Price After Tax %>" />
                                                        </td>
                                                        <td align="center">
                                                            :
                                                        </td>
                                                        <td align='<%= Common.ChangeTDForDefaultLeft()%>'>
                                                            <asp:TextBox ID="txtPPriceAfterTax" runat="server" CssClass="textComman" ReadOnly="True"
                                                                Width="150px" />
                                                            <cc1:FilteredTextBoxExtender ID="FilteredTextBoxExtender13" runat="server" Enabled="True"
                                                                TargetControlID="txtPPriceAfterTax" ValidChars="1,2,3,4,5,6,7,8,9,0,.">
                                                            </cc1:FilteredTextBoxExtender>
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td align='<%= Common.ChangeTDForDefaultLeft()%>'>
                                                            <asp:Label ID="lblPDiscount" runat="server" CssClass="labelComman" Text="<%$ Resources:Attendance,Discount %>" />
                                                        </td>
                                                        <td align="center">
                                                            :
                                                        </td>
                                                        <td align='<%= Common.ChangeTDForDefaultLeft()%>'>
                                                            <asp:TextBox ID="txtPDiscountP" runat="server" Width="65px" CssClass="textComman"
                                                                OnTextChanged="txtPDiscountP_TextChanged" AutoPostBack="true" />(%)
                                                            <cc1:FilteredTextBoxExtender ID="FilteredTextBoxExtender14" runat="server" Enabled="True"
                                                                TargetControlID="txtPDiscountP" ValidChars="1,2,3,4,5,6,7,8,9,0,.">
                                                            </cc1:FilteredTextBoxExtender>
                                                            <asp:TextBox ID="txtPDiscountV" runat="server" Width="100px" CssClass="textComman"
                                                                OnTextChanged="txtPDiscountV_TextChanged" AutoPostBack="true" />(Value)
                                                            <cc1:FilteredTextBoxExtender ID="FilteredTextBoxExtender16" runat="server" Enabled="True"
                                                                TargetControlID="txtPDiscountV" ValidChars="1,2,3,4,5,6,7,8,9,0,.">
                                                            </cc1:FilteredTextBoxExtender>
                                                        </td>
                                                        <td align='<%= Common.ChangeTDForDefaultLeft()%>'>
                                                            <asp:Label ID="lblPriceAfterDiscount" runat="server" CssClass="labelComman" Text="<%$ Resources:Attendance, Price After Discount %>" />
                                                        </td>
                                                        <td align="center">
                                                            :
                                                        </td>
                                                        <td align='<%= Common.ChangeTDForDefaultLeft()%>'>
                                                            <asp:TextBox ID="txtPPriceAfterDiscount" runat="server" CssClass="textComman" ReadOnly="True"
                                                                Width="150px" />
                                                            <cc1:FilteredTextBoxExtender ID="FilteredTextBoxExtender15" runat="server" Enabled="True"
                                                                TargetControlID="txtPPriceAfterDiscount" ValidChars="1,2,3,4,5,6,7,8,9,0,.">
                                                            </cc1:FilteredTextBoxExtender>
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td align='<%= Common.ChangeTDForDefaultLeft()%>'>
                                                            <asp:Label ID="lblPTotalAmount" runat="server" CssClass="labelComman" Text="<%$ Resources:Attendance, Total Amount %>" />
                                                        </td>
                                                        <td align="center">
                                                            :
                                                        </td>
                                                        <td align='<%= Common.ChangeTDForDefaultLeft()%>'>
                                                            <asp:TextBox ID="txtPTotalAmount" runat="server" CssClass="textComman" ReadOnly="True" />
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td colspan="6" align="center" style="padding-left: 10px">
                                                            <table>
                                                                <tr>
                                                                    <td width="90px">
                                                                        <asp:Button ID="btnProductSave" runat="server" Text="<%$ Resources:Attendance,Save %>"
                                                                            CssClass="buttonCommman" OnClick="btnProductSave_Click" />
                                                                    </td>
                                                                    <td width="90px">
                                                                        <asp:Button ID="btnProductCancel" runat="server" CssClass="buttonCommman" Text="<%$ Resources:Attendance,Cancel %>"
                                                                            CausesValidation="False" OnClick="btnProductCancel_Click" />
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
