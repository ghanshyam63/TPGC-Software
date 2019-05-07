<%@ Page Language="C#" MasterPageFile="~/ERPMaster.master" AutoEventWireup="true"
    CodeFile="PurchaseQuatation.aspx.cs" Inherits="Purchase_PurchaseQuatation" Title="Untitled Page" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <%--<link href="../App_Themes/AJAX.css" rel="stylesheet" type="text/css" />--%>
    <asp:UpdatePanel ID="upallpage" runat="server">
        <Triggers>
            <asp:PostBackTrigger ControlID="btnList" />
            <asp:PostBackTrigger ControlID="btnNew" />
            <asp:PostBackTrigger ControlID="btnBin" />
           <%-- <asp:PostBackTrigger ControlID="GvPurchaseQuote" />
            <asp:PostBackTrigger ControlID="GvSupplier" />--%>
            <asp:PostBackTrigger ControlID="btnProductSave" />
            <%--<asp:PostBackTrigger ControlID="GvPurchaseInquiry" />--%>
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
                                                <asp:Label ID="lblHeader" runat="server" Text="<%$ Resources:Attendance,Purchase Quotation %>"
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
                                                <asp:Panel ID="pnlMenuBin" runat="server" CssClass="a" Width="100%">
                                                    <asp:Button ID="btnBin" runat="server" Text="<%$ Resources:Attendance,Bin %>" Width="90px"
                                                        BorderStyle="none" BackColor="Transparent" OnClick="btnBin_Click" Style="padding-left: 10px;
                                                        padding-top: 3px; background-image: url('../Images/Bin.png' ); background-repeat: no-repeat;
                                                        height: 49px; background-position: 5px 15px; font: bold 14px Trebuchet MS; color: #000000;" />
                                                </asp:Panel>
                                            </td>
                                            <td>
                                                <asp:Panel ID="pnlMenuRequest" runat="server" CssClass="a" Width="100%">
                                                    <asp:Button ID="btnPRequest" runat="server" Text="<%$ Resources:Attendance,Inquiry %>"
                                                        Width="100px" BorderStyle="none" BackColor="Transparent" OnClick="btnPRequest_Click"
                                                        Style="padding-left: 40px; padding-top: 3px; background-image: url('../Images/Request.png' );
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
                                                                <asp:ListItem Text="<%$ Resources:Attendance, Quotation Id %>" Value="Trans_Id"></asp:ListItem>
                                                                <asp:ListItem Text="<%$ Resources:Attendance, Quotation No. %>" Value="RPQ_No" Selected="True"></asp:ListItem>
                                                                <asp:ListItem Text="<%$ Resources:Attendance, Quotation Date %>" Value="RPQ_Date"></asp:ListItem>
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
                                            <asp:GridView ID="GvPurchaseQuote" PageSize="<%# SystemParameter.GetPageSize() %>" runat="server" AutoGenerateColumns="False"
                                                Width="100%" AllowPaging="True" OnPageIndexChanging="GvPurchaseQuote_PageIndexChanging"
                                                AllowSorting="True" OnSorting="GvPurchaseQuote_Sorting" CssClass="grid">
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
                                                    <asp:TemplateField HeaderText="<%$ Resources:Attendance,Quotation No. %>" SortExpression="RPQ_No">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblgvRPQNo" runat="server" Text='<%#Eval("RPQ_No") %>' />
                                                        </ItemTemplate>
                                                        <ItemStyle HorizontalAlign="Center" CssClass="grid" />
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="<%$ Resources:Attendance,Quotation Date %>" SortExpression="RPQ_Date">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblgvRPQDate" runat="server" Text='<%#GetDate(Eval("RPQ_Date").ToString()) %>' />
                                                        </ItemTemplate>
                                                        <ItemStyle HorizontalAlign="Center" CssClass="grid" />
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="<%$ Resources:Attendance,Purchase Inquiry No. %>"
                                                        SortExpression="PI_No">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblgvPINo" runat="server" Text='<%#Eval("PI_No") %>' />
                                                        </ItemTemplate>
                                                        <ItemStyle HorizontalAlign="Center" CssClass="grid" />
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="<%$ Resources:Attendance,Total Amount %>" SortExpression="TotalAmount">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblgvTotalAmount" runat="server" Text='<%#Eval("TotalAmount") %>' />
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
                                    <asp:Panel ID="PnlNewEdit" runat="server" DefaultButton="btnQuoteSave">
                                        <table width="100%" style="padding-left: 43px">
                                            <tr>
                                                <td align='<%= Common.ChangeTDForDefaultLeft()%>'>
                                                    <asp:Label ID="lblRPQDate" runat="server" CssClass="labelComman" Text="<%$ Resources:Attendance,Quotation Date %>"></asp:Label>
                                                </td>
                                                <td align="center">
                                                    :
                                                </td>
                                                <td align='<%= Common.ChangeTDForDefaultLeft()%>'>
                                                    <asp:TextBox ID="txtRPQDate" runat="server" CssClass="textComman" />
                                                    <cc1:CalendarExtender ID="Calender" runat="server" TargetControlID="txtRPQDate" Format="dd/MMM/yyyy" />
                                                </td>
                                                <td align='<%= Common.ChangeTDForDefaultLeft()%>'>
                                                    <asp:Label ID="lblRPQNo" runat="server" CssClass="labelComman" Text="<%$ Resources:Attendance,Quotation No. %>"></asp:Label>
                                                </td>
                                                <td align="center">
                                                    :
                                                </td>
                                                <td align='<%= Common.ChangeTDForDefaultLeft()%>'>
                                                    <asp:TextBox ID="txtRPQNo" runat="server" CssClass="textComman" />
                                                </td>
                                            </tr>
                                            <tr>
                                                <td align='<%= Common.ChangeTDForDefaultLeft()%>'>
                                                    <asp:Label ID="lblInquiryNo" runat="server" CssClass="labelComman" Text="<%$ Resources:Attendance,Inquiry No. %>" />
                                                </td>
                                                <td align="center">
                                                    :
                                                </td>
                                                <td align='<%= Common.ChangeTDForDefaultLeft()%>'>
                                                    <asp:TextBox ID="txtInquiryNo" runat="server" CssClass="textComman" ReadOnly="True" />
                                                </td>
                                                <td align='<%= Common.ChangeTDForDefaultLeft()%>'>
                                                    <asp:Label ID="lblInquiryDate" runat="server" CssClass="labelComman" Text="<%$ Resources:Attendance,Inquiry Date%>" />
                                                </td>
                                                <td align="center">
                                                    :
                                                </td>
                                                <td align='<%= Common.ChangeTDForDefaultLeft()%>'>
                                                    <asp:TextBox ID="txtInquiryDate" runat="server" CssClass="textComman" ReadOnly="True" />
                                                </td>
                                            </tr>
                                            <tr>
                                                <td colspan="6" align="center">
                                                    <asp:Panel ID="pnlSupplier" runat="server">
                                                        <asp:GridView ID="GvSupplier" runat="server" Width="80%" CssClass="grid" AutoGenerateColumns="False"
                                                            DataKeyNames="Supplier_Id">
                                                            <AlternatingRowStyle CssClass="InvgridAltRow" />
                                                            <Columns>
                                                                <asp:TemplateField HeaderText="<%$ Resources:Attendance,S No. %>">
                                                                    <ItemTemplate>
                                                                        <%#Container.DataItemIndex+1 %>
                                                                    </ItemTemplate>
                                                                    <ItemStyle CssClass="grid" HorizontalAlign="Center" />
                                                                </asp:TemplateField>
                                                                <asp:TemplateField HeaderText="<%$ Resources:Attendance, Detail %>">
                                                                    <ItemTemplate>
                                                                        <asp:ImageButton ID="ImgSupplierDetail" runat="server" CommandArgument='<%# Eval("Supplier_Id") %>'
                                                                            ImageUrl="~/Images/Detail.png" OnCommand="ImgSupplierDetail_Command" CausesValidation="False" />
                                                                    </ItemTemplate>
                                                                    <ItemStyle CssClass="grid" HorizontalAlign="Center" />
                                                                </asp:TemplateField>
                                                                <asp:TemplateField HeaderText="<%$ Resources:Attendance, Supplier %>">
                                                                    <ItemTemplate>
                                                                        <asp:HiddenField ID="hdngvSupplierId" runat="server" Value='<%#Eval("Supplier_Id") %>' />
                                                                        <asp:Label ID="lblgvSupplierName" runat="server" Text='<%#GetSupplierName(Eval("Supplier_Id").ToString()) %>' />
                                                                    </ItemTemplate>
                                                                    <ItemStyle CssClass="grid" HorizontalAlign="Center" />
                                                                </asp:TemplateField>
                                                            </Columns>
                                                            <HeaderStyle CssClass="Invgridheader" />
                                                            <PagerStyle CssClass="Invgridheader" />
                                                            <RowStyle CssClass="Invgridrow" HorizontalAlign="Center" />
                                                        </asp:GridView>
                                                        <asp:HiddenField ID="hdnSupplierId" runat="server" Value="0" />
                                                    </asp:Panel>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td align='<%= Common.ChangeTDForDefaultLeft()%>'>
                                                    <asp:Label ID="lblTotalAmount" runat="server" CssClass="labelComman" Text="<%$ Resources:Attendance,Total Amount %>" />
                                                </td>
                                                <td>
                                                    :
                                                </td>
                                                <td align='<%= Common.ChangeTDForDefaultLeft()%>'>
                                                    <asp:TextBox ID="txtTotalAmount" runat="server" ReadOnly="true" CssClass="textComman" />
                                                </td>
                                            </tr>
                                            <tr>
                                                <td colspan="6" align="center" style="padding-left: 10px">
                                                    <table>
                                                        <tr>
                                                            <td width="90px">
                                                                <asp:Button ID="btnQuoteSave" runat="server" Text="<%$ Resources:Attendance,Save %>"
                                                                    CssClass="buttonCommman" OnClick="btnQuoteSave_Click" Visible="False" />
                                                            </td>
                                                            <td width="90px">
                                                                <asp:Button ID="BtnReset" runat="server" Text="<%$ Resources:Attendance,Reset %>"
                                                                    CssClass="buttonCommman" CausesValidation="False" OnClick="BtnReset_Click" />
                                                            </td>
                                                            <td width="90px">
                                                                <asp:Button ID="btnQuoteCancel" runat="server" CssClass="buttonCommman" Text="<%$ Resources:Attendance,Cancel %>"
                                                                    CausesValidation="False" OnClick="btnQuoteCancel_Click" />
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
                                                            <asp:ListItem Text="<%$ Resources:Attendance, Quotation Id %>" Value="Trans_Id"></asp:ListItem>
                                                            <asp:ListItem Text="<%$ Resources:Attendance, Quotation No. %>" Value="RPQ_No" Selected="True"></asp:ListItem>
                                                            <asp:ListItem Text="<%$ Resources:Attendance, Quotation Date %>" Value="RPQ_Date"></asp:ListItem>
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
                                        <asp:GridView ID="GvPurchaseQuoteBin" PageSize="10" runat="server" AutoGenerateColumns="False"
                                            Width="100%" AllowPaging="True" OnPageIndexChanging="GvPurchaseQuoteBin_PageIndexChanging"
                                            OnSorting="GvPurchaseQuoteBin_OnSorting" AllowSorting="true" CssClass="grid">
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
                                                <asp:TemplateField HeaderText="<%$ Resources:Attendance,Quotation No. %>" SortExpression="RPQ_No">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblgvRPQNo" runat="server" Text='<%#Eval("RPQ_No") %>' />
                                                        <asp:HiddenField ID="hdnTransId" runat="server" Value='<%#Eval("Trans_Id") %>' />
                                                    </ItemTemplate>
                                                    <ItemStyle HorizontalAlign="Center" CssClass="grid" />
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="<%$ Resources:Attendance,Quotation Date %>" SortExpression="RPQ_Date">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblgvRPQDate" runat="server" Text='<%#GetDate(Eval("RPQ_Date").ToString()) %>' />
                                                    </ItemTemplate>
                                                    <ItemStyle HorizontalAlign="Center" CssClass="grid" />
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="<%$ Resources:Attendance,Purchase Inquiry No. %>"
                                                    SortExpression="PI_No">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblgvPINo" runat="server" Text='<%#Eval("PI_No") %>' />
                                                    </ItemTemplate>
                                                    <ItemStyle HorizontalAlign="Center" CssClass="grid" />
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="<%$ Resources:Attendance,Total Amount %>" SortExpression="TotalAmount">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblgvTotalAmount" runat="server" Text='<%#Eval("TotalAmount") %>' />
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
                                        <asp:GridView ID="GvPurchaseInquiry" PageSize="<%# SystemParameter.GetPageSize() %>" runat="server" AutoGenerateColumns="False"
                                            Width="100%" AllowPaging="True" OnPageIndexChanging="GvPurchaseInquiry_PageIndexChanging"
                                            AllowSorting="True" OnSorting="GvPurchaseInquiry_Sorting" CssClass="grid">
                                            <AlternatingRowStyle CssClass="InvgridAltRow" />
                                            <Columns>
                                                <asp:TemplateField>
                                                    <ItemTemplate>
                                                        <asp:ImageButton ID="btnEdit" runat="server" CommandArgument='<%# Eval("Trans_Id") %>'
                                                            ImageUrl="~/Images/edit.png" OnCommand="btnPREdit_Command" CausesValidation="False" /></ItemTemplate>
                                                    <ItemStyle CssClass="grid" HorizontalAlign="Center" />
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="<%$ Resources:Attendance,Inquiry Id %>" SortExpression="Trans_Id">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblgvInquiryId" runat="server" Text='<%#Eval("Trans_Id") %>' />
                                                    </ItemTemplate>
                                                    <ItemStyle HorizontalAlign="Center" CssClass="grid" />
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="<%$ Resources:Attendance,Inquiry No. %>" SortExpression="PI_No">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblgvPINo" runat="server" Text='<%#Eval("PI_No") %>' />
                                                    </ItemTemplate>
                                                    <ItemStyle HorizontalAlign="Center" CssClass="grid" />
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="<%$ Resources:Attendance,Inquiry Date %>" SortExpression="PIDate">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblgvPIDate" runat="server" Text='<%#GetDate(Eval("PIDate").ToString()) %>' />
                                                    </ItemTemplate>
                                                    <ItemStyle HorizontalAlign="Center" CssClass="grid" />
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="<%$ Resources:Attendance,Transfer From %>" SortExpression="TransFrom">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblgvTransFrom" runat="server" Text='<%#Eval("TransFrom") %>' />
                                                    </ItemTemplate>
                                                    <ItemStyle HorizontalAlign="Center" CssClass="grid" />
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="<%$ Resources:Attendance,Transfer No.%>" SortExpression="TransNo">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblgvTransNo" runat="server" Text='<%#Eval("TransNo") %>' />
                                                    </ItemTemplate>
                                                    <ItemStyle HorizontalAlign="Center" CssClass="grid" />
                                                </asp:TemplateField>
                                            </Columns>
                                            <HeaderStyle CssClass="Invgridheader" />
                                            <PagerStyle CssClass="Invgridheader" />
                                            <RowStyle CssClass="Invgridrow" HorizontalAlign="Center" />
                                        </asp:GridView>
                                        <asp:HiddenField ID="hdnPIId" runat="server" Value="0" />
                                    </asp:Panel>
                                    <asp:Panel ID="pnlProduct" runat="server" Visible="false">
                                        <table width="100%">
                                            <tr>
                                                <td align="center">
                                                    <asp:DataList ID="dlProductDetail" runat="server">
                                                        <ItemTemplate>
                                                            <fieldset>
                                                                <%-- <legend>
                                                                    <asp:Label ID="lblLegend" runat="server" Text="<%$ Resources:Attendance,Product Detail %>" />
                                                                </legend>--%>
                                                                <table width="100%">
                                                                    <tr>
                                                                        <td align='<%= Common.ChangeTDForDefaultLeft()%>'>
                                                                            <asp:Label ID="lblProductName" runat="server" CssClass="labelComman" Text="<%$ Resources:Attendance,Product Name %>" />
                                                                        </td>
                                                                        <td align="center">
                                                                            :
                                                                        </td>
                                                                        <td colspan="4" align='<%= Common.ChangeTDForDefaultLeft()%>'>
                                                                            <asp:Label ID="txtProductName" runat="server" CssClass="labelComman" Width="550px"
                                                                                Text='<%#GetProductName(Eval("Product_Id").ToString()) %>' />
                                                                            <asp:HiddenField ID="hdngvProductId" runat="server" Value='<%#Eval("Product_Id") %>' />
                                                                        </td>
                                                                    </tr>
                                                                    <tr>
                                                                        <td align='<%= Common.ChangeTDForDefaultLeft()%>'>
                                                                            <asp:Label ID="lblProductDescription" runat="server" CssClass="labelComman" Text="<%$ Resources:Attendance,Product Description %>" />
                                                                        </td>
                                                                        <td align="center">
                                                                            :
                                                                        </td>
                                                                        <td colspan="4" align='<%= Common.ChangeTDForDefaultLeft()%>'>
                                                                            <asp:Label ID="txtProductDescription" CssClass="labelComman" runat="server" Width="550px"
                                                                                Text='<%#Eval("ProductDescription") %>' />
                                                                        </td>
                                                                    </tr>
                                                                    <tr>
                                                                        <td align='<%= Common.ChangeTDForDefaultLeft()%>'>
                                                                            <asp:Label ID="lblRefProductName" runat="server" CssClass="labelComman" Text="<%$ Resources:Attendance,Refrenced Product Name %>" />
                                                                        </td>
                                                                        <td align="center">
                                                                            :
                                                                        </td>
                                                                        <td align='<%= Common.ChangeTDForDefaultLeft()%>'>
                                                                            <asp:TextBox ID="txtRefProductName" runat="server" CssClass="textComman" />
                                                                        </td>
                                                                        <td align='<%= Common.ChangeTDForDefaultLeft()%>'>
                                                                            <asp:Label ID="lblRefPartNo" runat="server" CssClass="labelComman" Text="<%$ Resources:Attendance,Refrenced Part No. %>" />
                                                                        </td>
                                                                        <td align="center">
                                                                            :
                                                                        </td>
                                                                        <td align='<%= Common.ChangeTDForDefaultLeft()%>'>
                                                                            <asp:TextBox ID="txtRefPartNo" runat="server" CssClass="textComman" />
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
                                                                            <asp:Label ID="txtUnit" runat="server" CssClass="labelComman" Text='<%#GetUnitName(Eval("UnitId").ToString()) %>' />
                                                                            <asp:HiddenField ID="hdngvUnitId" runat="server" Value='<%#Eval("UnitId") %>' />
                                                                        </td>
                                                                        <td align='<%= Common.ChangeTDForDefaultLeft()%>'>
                                                                            <asp:Label ID="lblRequiredQty" runat="server" CssClass="labelComman" Text="<%$ Resources:Attendance,Required Quantity %>" />
                                                                        </td>
                                                                        <td align="center">
                                                                            :
                                                                        </td>
                                                                        <td align='<%= Common.ChangeTDForDefaultLeft()%>'>
                                                                            <asp:Label ID="txtRequiredQuantity" runat="server" CssClass="labelComman" Text='<%#Eval("ReqQty") %>' />
                                                                        </td>
                                                                    </tr>
                                                                    <tr>
                                                                        <td align='<%= Common.ChangeTDForDefaultLeft()%>'>
                                                                            <asp:Label ID="lblUnitPrice" runat="server" CssClass="labelComman" Text="<%$ Resources:Attendance,Unit Price %>" />
                                                                        </td>
                                                                        <td align="center">
                                                                            :
                                                                        </td>
                                                                        <td align='<%= Common.ChangeTDForDefaultLeft()%>'>
                                                                            <asp:TextBox ID="txtUnitPrice" runat="server" CssClass="textComman" OnTextChanged="txtUnitPrice_TextChanged"
                                                                                AutoPostBack="true" />
                                                                                  <cc1:FilteredTextBoxExtender ID="FilteredTextBoxExtender1" runat="server" Enabled="True"
                                                        TargetControlID="txtUnitPrice" ValidChars="1,2,3,4,5,6,7,8,9,0,.">
                                                    </cc1:FilteredTextBoxExtender>
                                                                        </td>
                                                                    </tr>
                                                                    <tr>
                                                                        <td align='<%= Common.ChangeTDForDefaultLeft()%>'>
                                                                            <asp:Label ID="lblTaxP" runat="server" CssClass="labelComman" Text="<%$ Resources:Attendance,Tax %>" />
                                                                        </td>
                                                                        <td align="center">
                                                                            :
                                                                        </td>
                                                                        <td align='<%= Common.ChangeTDForDefaultLeft()%>'>
                                                                            <asp:TextBox ID="txtTaxP" runat="server" Width="65px" CssClass="textComman" OnTextChanged="txtTaxP_TextChanged"
                                                                                AutoPostBack="true" />(%)
                                                                                  <cc1:FilteredTextBoxExtender ID="FilteredTextBoxExtender2" runat="server" Enabled="True"
                                                        TargetControlID="txtTaxP" ValidChars="1,2,3,4,5,6,7,8,9,0,.">
                                                    </cc1:FilteredTextBoxExtender>

                                                                            <asp:TextBox ID="txtTaxV" runat="server" Width="100px" CssClass="textComman" OnTextChanged="txtTaxV_TextChanged"
                                                                                AutoPostBack="true" />(Value)
                                                                                 <cc1:FilteredTextBoxExtender ID="FilteredTextBoxExtender3" runat="server" Enabled="True"
                                                        TargetControlID="txtTaxV" ValidChars="1,2,3,4,5,6,7,8,9,0,.">
                                                    </cc1:FilteredTextBoxExtender>
                                                                        </td>
                                                                        <td align='<%= Common.ChangeTDForDefaultLeft()%>'>
                                                                            <asp:Label ID="lblPriceAfterTax" runat="server" CssClass="labelComman" Text="<%$ Resources:Attendance,Price After Tax %>" />
                                                                        </td>
                                                                        <td align="center">
                                                                            :
                                                                        </td>
                                                                        <td align='<%= Common.ChangeTDForDefaultLeft()%>'>
                                                                            <asp:TextBox ID="txtPriceAfterTax" runat="server" CssClass="textComman" ReadOnly="True" />
                                                                               <cc1:FilteredTextBoxExtender ID="FilteredTextBoxExtender4" runat="server" Enabled="True"
                                                        TargetControlID="txtPriceAfterTax" ValidChars="1,2,3,4,5,6,7,8,9,0,.">
                                                    </cc1:FilteredTextBoxExtender>
                                                                        </td>
                                                                    </tr>
                                                                    <tr>
                                                                        <td align='<%= Common.ChangeTDForDefaultLeft()%>'>
                                                                            <asp:Label ID="lblDiscountP" runat="server" CssClass="labelComman" Text="<%$ Resources:Attendance,Discount %>" />
                                                                        </td>
                                                                        <td align="center">
                                                                            :
                                                                        </td>
                                                                        <td align='<%= Common.ChangeTDForDefaultLeft()%>'>
                                                                            <asp:TextBox ID="txtDiscountP" runat="server" Width="65px" CssClass="textComman"
                                                                                OnTextChanged="txtDiscountP_TextChanged" AutoPostBack="true" />(%)
                                                                                  <cc1:FilteredTextBoxExtender ID="FilteredTextBoxExtender5" runat="server" Enabled="True"
                                                        TargetControlID="txtDiscountP" ValidChars="1,2,3,4,5,6,7,8,9,0,.">
                                                    </cc1:FilteredTextBoxExtender>
                                                                            <asp:TextBox ID="txtDiscountV" runat="server" Width="100px" CssClass="textComman"
                                                                                OnTextChanged="txtDiscountV_TextChanged" AutoPostBack="true" />(Value)
                                                                                  <cc1:FilteredTextBoxExtender ID="FilteredTextBoxExtender6" runat="server" Enabled="True"
                                                        TargetControlID="txtDiscountV" ValidChars="1,2,3,4,5,6,7,8,9,0,.">
                                                    </cc1:FilteredTextBoxExtender>
                                                                        </td>
                                                                        <td align='<%= Common.ChangeTDForDefaultLeft()%>'>
                                                                            <asp:Label ID="lblAmount" runat="server" CssClass="labelComman" Text="<%$ Resources:Attendance,Amount %>" />
                                                                        </td>
                                                                        <td align="center">
                                                                            :
                                                                        </td>
                                                                        <td align='<%= Common.ChangeTDForDefaultLeft()%>'>
                                                                            <asp:TextBox ID="txtAmount" runat="server" CssClass="textComman" ReadOnly="True" />
                                                                               <cc1:FilteredTextBoxExtender ID="FilteredTextBoxExtender7" runat="server" Enabled="True"
                                                        TargetControlID="txtAmount" ValidChars="1,2,3,4,5,6,7,8,9,0,.">
                                                    </cc1:FilteredTextBoxExtender>
                                                                        </td>
                                                                    </tr>
                                                                    <tr>
                                                                        <td align='<%= Common.ChangeTDForDefaultLeft()%>'>
                                                                            <asp:Label ID="lblTermsCondition" runat="server" CssClass="labelComman" Text="<%$ Resources:Attendance,Terms & Conditions %>" />
                                                                        </td>
                                                                        <td align="center">
                                                                            :
                                                                        </td>
                                                                        <td colspan="4" align='<%= Common.ChangeTDForDefaultLeft()%>'>
                                                                            <asp:TextBox ID="txtTermsCondition" runat="server" CssClass="textComman" TextMode="MultiLine"
                                                                                Width="668px" />
                                                                        </td>
                                                                    </tr>
                                                                </table>
                                                            </fieldset>
                                                        </ItemTemplate>
                                                    </asp:DataList>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td colspan="6" align="center" style="padding-left: 10px">
                                                    <table>
                                                        <tr>
                                                            <td width="90px">
                                                                <asp:Button ID="btnProductSave" runat="server" Text="<%$ Resources:Attendance,Save %>"
                                                                    CssClass="buttonCommman" OnClick="btnProductSave_Click" Visible="False" />
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
