<%@ Page Language="C#" MasterPageFile="~/ERPMaster.master" AutoEventWireup="true"
    CodeFile="PurchaseReturn.aspx.cs" Inherits="Purchase_PurchaseReturn" Title="Untitled Page" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <%--<link href="../App_Themes/AJAX.css" rel="stylesheet" type="text/css" />
--%>    <asp:UpdatePanel ID="upallpage" runat="server">
        <Triggers>
            <asp:PostBackTrigger ControlID="btnList" />
            <asp:PostBackTrigger ControlID="btnNew" />
            <asp:PostBackTrigger ControlID="btnBin" />
            <asp:PostBackTrigger ControlID="btnPReturnSave" />
           <%-- <asp:PostBackTrigger ControlID="GvPReturn" />--%>
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
                                                <asp:Label ID="lblHeader" runat="server" Text="<%$ Resources:Attendance,Purchase Return%>"
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
                                                                <asp:ListItem Text="<%$ Resources:Attendance, Return Id %>" Value="Trans_Id"></asp:ListItem>
                                                                <asp:ListItem Text="<%$ Resources:Attendance, Return No. %>" Value="PReturn_No" Selected="True"></asp:ListItem>
                                                                <asp:ListItem Text="<%$ Resources:Attendance, Return Date %>" Value="PRDate"></asp:ListItem>
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
                                            <asp:GridView ID="GvPReturn" PageSize="10" runat="server" AutoGenerateColumns="False"
                                                Width="100%" AllowPaging="True" OnPageIndexChanging="GvPReturn_PageIndexChanging"
                                                AllowSorting="True" OnSorting="GvPReturn_Sorting" CssClass="grid">
                                                <AlternatingRowStyle CssClass="InvgridAltRow" />
                                                <Columns>
                                                    <asp:TemplateField>
                                                        <ItemTemplate>
                                                            <asp:ImageButton ID="btnEdit" runat="server" CommandArgument='<%# Eval("Trans_Id") %>'
                                                                ImageUrl="~/Images/edit.png" OnCommand="btnEdit_Command" CausesValidation="False"
                                                                Visible="False" /></ItemTemplate>
                                                        <ItemStyle CssClass="grid" HorizontalAlign="Center" />
                                                    </asp:TemplateField>
                                                    <asp:TemplateField>
                                                        <ItemTemplate>
                                                            <asp:ImageButton ID="IbtnDelete" runat="server" CausesValidation="False" CommandArgument='<%# Eval("Trans_Id") %>'
                                                                ImageUrl="~/Images/Erase.png" OnCommand="IbtnDelete_Command" Width="16px" Visible="False" />
                                                        </ItemTemplate>
                                                        <ItemStyle CssClass="grid" HorizontalAlign="Center" />
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="<%$ Resources:Attendance,Return Id %>" SortExpression="Trans_Id">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblgvReturnId" runat="server" Text='<%#Eval("Trans_Id") %>' />
                                                        </ItemTemplate>
                                                        <ItemStyle HorizontalAlign="Center" CssClass="grid" />
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="<%$ Resources:Attendance, Return No. %>" SortExpression="PReturn_No">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblgvReturnNo" runat="server" Text='<%#Eval("PReturn_No") %>' />
                                                        </ItemTemplate>
                                                        <ItemStyle HorizontalAlign="Center" CssClass="grid" />
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="<%$ Resources:Attendance, Return Date %>" SortExpression="PRDate">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblgvReturnDate" runat="server" Text='<%#Eval("PRDate") %>' />
                                                        </ItemTemplate>
                                                        <ItemStyle HorizontalAlign="Center" CssClass="grid" />
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="<%$ Resources:Attendance, Invoice No. %>" SortExpression="InvoiceNo">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblgvInvoiceNo" runat="server" Text='<%#Eval("InvoiceNo") %>' />
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
                                    <asp:Panel ID="PnlNewEdit" runat="server" DefaultButton="btnPReturnSave">
                                        <table width="100%" style="padding-left: 43px">
                                            <tr>
                                                <td align='<%= Common.ChangeTDForDefaultLeft()%>'>
                                                    <asp:Label ID="lblReturnDate" runat="server" CssClass="labelComman" Text="<%$ Resources:Attendance,Return Date %>"></asp:Label>
                                                </td>
                                                <td align="center">
                                                    :
                                                </td>
                                                <td align='<%= Common.ChangeTDForDefaultLeft()%>'>
                                                    <asp:TextBox ID="txtReturnDate" runat="server" CssClass="textComman" />
                                                    <cc1:CalendarExtender ID="Calender" runat="server" TargetControlID="txtReturnDate"
                                                        Format="dd/MMM/yyyy" />
                                                </td>
                                                <td align='<%= Common.ChangeTDForDefaultLeft()%>'>
                                                    <asp:Label ID="lblReturnNo" runat="server" CssClass="labelComman" Text="<%$ Resources:Attendance,Return No. %>"></asp:Label>
                                                </td>
                                                <td align="center">
                                                    :
                                                </td>
                                                <td align='<%= Common.ChangeTDForDefaultLeft()%>'>
                                                    <asp:TextBox ID="txtReturnNo" runat="server" CssClass="textComman" />
                                                </td>
                                            </tr>
                                            <tr>
                                                <td align='<%= Common.ChangeTDForDefaultLeft()%>'>
                                                    <asp:Label ID="lblSupplierName" runat="server" CssClass="labelComman" Text="<%$ Resources:Attendance,Supplier Name %>" />
                                                </td>
                                                <td align="center">
                                                    :
                                                </td>
                                                <td align='<%= Common.ChangeTDForDefaultLeft()%>' colspan="4">
                                                    <asp:TextBox ID="txtSupplierName" runat="server" CssClass="textComman" Width="728px"
                                                        BackColor="#c3c3c3" OnTextChanged="txtSupplierName_TextChanged" AutoPostBack="true" />
                                                    <cc1:AutoCompleteExtender ID="txtSupplierName_AutoCompleteExtender" runat="server"
                                                        DelimiterCharacters="" Enabled="True" CompletionInterval="100" CompletionSetCount="1"
                                                        MinimumPrefixLength="1" ServiceMethod="GetCompletionListSupplier" ServicePath=""
                                                        TargetControlID="txtSupplierName" UseContextKey="True" CompletionListCssClass="completionList"
                                                        CompletionListItemCssClass="listItem" CompletionListHighlightedItemCssClass="itemHighlighted">
                                                    </cc1:AutoCompleteExtender>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td align='<%= Common.ChangeTDForDefaultLeft()%>'>
                                                    <asp:Label ID="lblPInvoiceNo" runat="server" CssClass="labelComman" Text="<%$ Resources:Attendance,Purchase Invoice No. %>" />
                                                </td>
                                                <td align="center">
                                                    :
                                                </td>
                                                <td align='<%= Common.ChangeTDForDefaultLeft()%>'>
                                                    <asp:TextBox ID="txtPInvoiceNo" runat="server" CssClass="textComman" Visible="false"
                                                        OnTextChanged="txtPInvoiceNo_TextChanged" AutoPostBack="true" />
                                                    <cc1:AutoCompleteExtender ID="txtPInvoiceNo_AutoCompleteExtender" runat="server"
                                                        DelimiterCharacters="" Enabled="True" CompletionInterval="100" CompletionSetCount="1"
                                                        MinimumPrefixLength="1" ServiceMethod="GetCompletionListInvoiceNo" ServicePath=""
                                                        TargetControlID="txtPInvoiceNo" UseContextKey="True" CompletionListCssClass="completionList"
                                                        CompletionListItemCssClass="listItem" CompletionListHighlightedItemCssClass="itemHighlighted">
                                                    </cc1:AutoCompleteExtender>
                                                    <asp:DropDownList ID="ddlPIncoiceNo" runat="server" Width="262px" CssClass="textComman"
                                                        Visible="false" OnSelectedIndexChanged="ddlPIncoiceNo_SelectedIndexChanged" AutoPostBack="true" />
                                                </td>
                                                <td align='<%= Common.ChangeTDForDefaultLeft()%>'>
                                                    <asp:Label ID="lblPInvoiceDate" runat="server" CssClass="labelComman" Text="<%$ Resources:Attendance,Purchase Invoice Date %>" />
                                                </td>
                                                <td align="center">
                                                    :
                                                </td>
                                                <td align='<%= Common.ChangeTDForDefaultLeft()%>'>
                                                    <asp:TextBox ID="txtPInvoiceDate" runat="server" CssClass="textComman" ReadOnly="true" />
                                                </td>
                                            </tr>
                                            <tr>
                                                <td colspan="6" align="left" style="padding-right: 70px">
                                                    <asp:GridView ID="GvInvoiceDetail" runat="server" AutoGenerateColumns="False" Width="100%"
                                                        CssClass="grid">
                                                        <AlternatingRowStyle CssClass="InvgridAltRow" />
                                                        <Columns>
                                                            <asp:TemplateField HeaderText="<%$ Resources:Attendance,S No. %>">
                                                                <ItemTemplate>
                                                                    <asp:Label ID="lblSerialNo" runat="server" Text='<%#Container.DataItemIndex+1 %>' />
                                                                </ItemTemplate>
                                                                <ItemStyle HorizontalAlign="Center" CssClass="grid" />
                                                            </asp:TemplateField>
                                                            <asp:TemplateField HeaderText="<%$ Resources:Attendance,Product Name %>">
                                                                <ItemTemplate>
                                                                    <asp:Label ID="lblIProductName" runat="server" Text='<%#GetProductName(Eval("ProductId").ToString()) %>' />
                                                                    <asp:HiddenField ID="hdnIProductId" runat="server" Value='<%#Eval("ProductId") %>' />
                                                                </ItemTemplate>
                                                                <ItemStyle HorizontalAlign="Center" CssClass="grid" />
                                                            </asp:TemplateField>
                                                            <asp:TemplateField HeaderText="<%$ Resources:Attendance,Unit %>">
                                                                <ItemTemplate>
                                                                    <asp:Label ID="lblIUnit" runat="server" Text='<%#GetUnitName(Eval("UnitId").ToString()) %>' />
                                                                    <asp:HiddenField ID="hdnIUnitId" runat="server" Value='<%#Eval("UnitId") %>' />
                                                                </ItemTemplate>
                                                                <ItemStyle HorizontalAlign="Center" CssClass="grid" />
                                                            </asp:TemplateField>
                                                            <asp:TemplateField HeaderText="<%$ Resources:Attendance,Order Quantity %>">
                                                                <ItemTemplate>
                                                                    <asp:Label ID="lblIOrderQty" runat="server" Text='<%#Eval("OrderQty") %>' />
                                                                </ItemTemplate>
                                                                <ItemStyle HorizontalAlign="Center" CssClass="grid" />
                                                            </asp:TemplateField>
                                                            <asp:TemplateField HeaderText="<%$ Resources:Attendance,Received Quantity %>">
                                                                <ItemTemplate>
                                                                    <asp:Label ID="lblIReceiveQty" runat="server" Text='<%# Eval("RecQty") %>' />
                                                                </ItemTemplate>
                                                                <ItemStyle HorizontalAlign="Center" CssClass="grid" />
                                                            </asp:TemplateField>
                                                            <asp:TemplateField HeaderText="<%$ Resources:Attendance,Unit Cost %>">
                                                                <ItemTemplate>
                                                                    <asp:Label ID="lblIUnitCost" runat="server" Text='<%#Eval("UnitCost") %>' />
                                                                </ItemTemplate>
                                                                <ItemStyle HorizontalAlign="Center" CssClass="grid" />
                                                            </asp:TemplateField>
                                                            <asp:TemplateField HeaderText="<%$ Resources:Attendance,Tax %>">
                                                                <ItemTemplate>
                                                                    <asp:Label ID="lblITaxValue" runat="server" Text='<%#Eval("TaxValue") %>' />
                                                                </ItemTemplate>
                                                                <ItemStyle HorizontalAlign="Center" CssClass="grid" />
                                                            </asp:TemplateField>
                                                            <asp:TemplateField HeaderText="<%$ Resources:Attendance,Discount %>">
                                                                <ItemTemplate>
                                                                    <asp:Label ID="lblIDiscountValue" runat="server" Text='<%#Eval("DiscountValue") %>' />
                                                                </ItemTemplate>
                                                                <ItemStyle HorizontalAlign="Center" CssClass="grid" />
                                                            </asp:TemplateField>
                                                            <asp:TemplateField HeaderText="<%$ Resources:Attendance,Return Quantity %>">
                                                                <ItemTemplate>
                                                                    <asp:TextBox ID="txtReturnQty" runat="server" Width="60px" OnTextChanged="txtReturnQty_TextChanged"
                                                                        AutoPostBack="true" />
                                                                           <cc1:FilteredTextBoxExtender ID="FilteredTextBoxExtender59" runat="server" Enabled="True"
                                                        TargetControlID="txtReturnQty" ValidChars="1,2,3,4,5,6,7,8,9,0,.">
                                                    </cc1:FilteredTextBoxExtender>
                               
                                                                    <asp:HiddenField ID="hdnQty" runat="server" Value="0" />
                                                                </ItemTemplate>
                                                                <ItemStyle HorizontalAlign="Center" CssClass="grid" />
                                                            </asp:TemplateField>
                                                        </Columns>
                                                        <HeaderStyle CssClass="Invgridheader" />
                                                        <PagerStyle CssClass="Invgridheader" />
                                                        <RowStyle CssClass="Invgridrow" HorizontalAlign="Center" />
                                                    </asp:GridView>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td align='<%= Common.ChangeTDForDefaultLeft()%>'>
                                                    <asp:Label ID="lblInvoiceType" runat="server" CssClass="labelComman" Text="<%$ Resources:Attendance,Invoice Type %>" />
                                                </td>
                                                <td align="center">
                                                    :
                                                </td>
                                                <td align='<%= Common.ChangeTDForDefaultLeft()%>'>
                                                    <asp:DropDownList ID="ddlInvoiceType" runat="server" Width="262px" CssClass="textComman">
                                                        <asp:ListItem Text="--Select--" Value="--Select--"></asp:ListItem>
                                                        <asp:ListItem Text="Cash" Value="A"></asp:ListItem>
                                                        <asp:ListItem Text="Credit" Value="R"></asp:ListItem>
                                                    </asp:DropDownList>
                                                </td>
                                                <td align='<%= Common.ChangeTDForDefaultLeft()%>'>
                                                    <asp:Label ID="lblPaymentMode" runat="server" CssClass="labelComman" Text="<%$ Resources:Attendance,Payment Mode %>" />
                                                </td>
                                                <td align="center">
                                                    :
                                                </td>
                                                <td align='<%= Common.ChangeTDForDefaultLeft()%>'>
                                                    <asp:DropDownList ID="ddlPaymentMode" runat="server" Width="262px" CssClass="textComman" />
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
                                                    <asp:TextBox ID="txtRemark" runat="server" TextMode="MultiLine" Width="728px" CssClass="textComman" />
                                                </td>
                                            </tr>
                                            <tr>
                                                <td colspan="6" align="center" style="padding-left: 10px">
                                                    <table>
                                                        <tr>
                                                            <td width="90px">
                                                                <asp:Button ID="btnPReturnSave" runat="server" Text="<%$ Resources:Attendance,Save %>"
                                                                    CssClass="buttonCommman" OnClick="btnPReturnSave_Click" Visible="False" />
                                                            </td>
                                                            <td width="90px">
                                                                <asp:Button ID="BtnReset" runat="server" Text="<%$ Resources:Attendance,Reset %>"
                                                                    CssClass="buttonCommman" CausesValidation="False" OnClick="BtnReset_Click" />
                                                            </td>
                                                            <td width="90px">
                                                                <asp:Button ID="btnPReturnCancel" runat="server" CssClass="buttonCommman" Text="<%$ Resources:Attendance,Cancel %>"
                                                                    CausesValidation="False" OnClick="btnPReturnCancel_Click" />
                                                            </td>
                                                        </tr>
                                                    </table>
                                                    <asp:HiddenField ID="hdnPReturnId" runat="server" Value="0" />
                                                    <asp:HiddenField ID="hdnInvoiceId" runat="server" Value="0" />
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
                                                            <asp:ListItem Text="<%$ Resources:Attendance, Return Id %>" Value="Trans_Id"></asp:ListItem>
                                                            <asp:ListItem Text="<%$ Resources:Attendance, Return No. %>" Value="PReturn_No" Selected="True"></asp:ListItem>
                                                            <asp:ListItem Text="<%$ Resources:Attendance, Return Date %>" Value="PRDate"></asp:ListItem>
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
                                                                runat="server" ImageUrl="~/Images/active.png" OnClick="imgBtnRestore_Click" ToolTip="<%$ Resources:Attendance, Active %>"
                                                                Visible="False" />
                                                        </asp:Panel>
                                                    </td>
                                                    <td>
                                                        <asp:Panel ID="pnlImgbtnSelectAll" runat="server" DefaultButton="ImgbtnSelectAll">
                                                            <asp:ImageButton ID="ImgbtnSelectAll" runat="server" OnClick="ImgbtnSelectAll_Click"
                                                                ToolTip="<%$ Resources:Attendance, Select All %>" AutoPostBack="true" ImageUrl="~/Images/selectAll.png"
                                                                Visible="False" />
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
                                        <asp:GridView ID="GvPReturnBin" PageSize="10" runat="server" AutoGenerateColumns="False"
                                            Width="100%" AllowPaging="True" OnPageIndexChanging="GvPReturnBin_PageIndexChanging"
                                            OnSorting="GvPReturnBin_OnSorting" AllowSorting="true" CssClass="grid">
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
                                                <asp:TemplateField HeaderText="<%$ Resources:Attendance,Return Id %>" SortExpression="Trans_Id">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblgvReturnId" runat="server" Text='<%#Eval("Trans_Id") %>' />
                                                    </ItemTemplate>
                                                    <ItemStyle HorizontalAlign="Center" CssClass="grid" />
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="<%$ Resources:Attendance, Return No. %>" SortExpression="PReturn_No">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblgvReturnNo" runat="server" Text='<%#Eval("PReturn_No") %>' />
                                                    </ItemTemplate>
                                                    <ItemStyle HorizontalAlign="Center" CssClass="grid" />
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="<%$ Resources:Attendance, Return Date %>" SortExpression="PRDate">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblgvReturnDate" runat="server" Text='<%#Eval("PRDate") %>' />
                                                    </ItemTemplate>
                                                    <ItemStyle HorizontalAlign="Center" CssClass="grid" />
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="<%$ Resources:Attendance, Invoice No. %>" SortExpression="InvoiceNo">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblgvInvoiceNo" runat="server" Text='<%#Eval("InvoiceNo") %>' />
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
