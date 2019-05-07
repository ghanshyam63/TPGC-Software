<%@ Page Language="C#" MasterPageFile="~/ERPMaster.master" AutoEventWireup="true"
    CodeFile="SalesInquiry.aspx.cs" Inherits="Sales_SalesInquiry" Title="Untitled Page" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
   <%-- <link href="../App_Themes/AJAX.css" rel="stylesheet" type="text/css" />
--%>    <asp:UpdatePanel ID="upallpage" runat="server">
        <Triggers>
            <asp:PostBackTrigger ControlID="btnList" />
            <asp:PostBackTrigger ControlID="btnNew" />
            <asp:PostBackTrigger ControlID="btnBin" />
            <asp:PostBackTrigger ControlID="btnSInquirySave" />
            
          <%--  <asp:PostBackTrigger ControlID="GvSalesInquiry" />--%>
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
                                                <asp:Label ID="lblHeader" runat="server" Text="<%$ Resources:Attendance,Sales Inquiry%>"
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
                                                                <asp:ListItem Text="<%$ Resources:Attendance, Inquiry Id %>" Value="SInquiryID"></asp:ListItem>
                                                                <asp:ListItem Text="<%$ Resources:Attendance, Inquiry No. %>" Value="SInquiryNo"
                                                                    Selected="True"></asp:ListItem>
                                                                <asp:ListItem Text="<%$ Resources:Attendance, Inquiry Date %>" Value="IDate"></asp:ListItem>
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
                                            <asp:GridView ID="GvSalesInquiry" PageSize="10" runat="server" AutoGenerateColumns="False"
                                                Width="100%" AllowPaging="True" OnPageIndexChanging="GvSalesInquiry_PageIndexChanging"
                                                AllowSorting="True" OnSorting="GvSalesInquiry_Sorting" CssClass="grid">
                                                <AlternatingRowStyle CssClass="InvgridAltRow" />
                                                <Columns>
                                                    <asp:TemplateField>
                                                        <ItemTemplate>
                                                            <asp:ImageButton ID="btnEdit" runat="server" CommandArgument='<%# Eval("SInquiryID") %>'
                                                                ImageUrl="~/Images/edit.png" OnCommand="btnEdit_Command" CausesValidation="False" />
                                                        </ItemTemplate>
                                                        <ItemStyle CssClass="grid" HorizontalAlign="Center" />
                                                    </asp:TemplateField>
                                                    <asp:TemplateField>
                                                        <ItemTemplate>
                                                            <asp:ImageButton ID="IbtnDelete" runat="server" CausesValidation="False" CommandArgument='<%# Eval("SInquiryID") %>'
                                                                ImageUrl="~/Images/Erase.png" OnCommand="IbtnDelete_Command" Width="16px" />
                                                        </ItemTemplate>
                                                        <ItemStyle CssClass="grid" HorizontalAlign="Center" />
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="<%$ Resources:Attendance,Inquiry Id %>" SortExpression="SInquiryID">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblgvInquiryId" runat="server" Text='<%#Eval("SInquiryID") %>' />
                                                        </ItemTemplate>
                                                        <ItemStyle HorizontalAlign="Center" CssClass="grid" />
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="<%$ Resources:Attendance, Inquiry No. %>" SortExpression="SInquiryNo">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblgvInquiryNo" runat="server" Text='<%#Eval("SInquiryNo") %>' />
                                                        </ItemTemplate>
                                                        <ItemStyle HorizontalAlign="Center" CssClass="grid" />
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="<%$ Resources:Attendance, Inquiry Date %>" SortExpression="IDate">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblgvInquiryDate" runat="server" Text='<%#GetDate(Eval("IDate").ToString()) %>' />
                                                        </ItemTemplate>
                                                        <ItemStyle HorizontalAlign="Center" CssClass="grid" />
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="<%$ Resources:Attendance, Customer Name %>" SortExpression="Contact_Name">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblgvCustomerName" runat="server" Text='<%#Eval("Contact_Name") %>' />
                                                        </ItemTemplate>
                                                        <ItemStyle HorizontalAlign="Center" CssClass="grid" />
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="<%$ Resources:Attendance, Received Employee %>" SortExpression="ReceivedEmployee">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblgvReceivedEmployee" runat="server" Text='<%#Eval("ReceivedEmployee") %>' />
                                                        </ItemTemplate>
                                                        <ItemStyle HorizontalAlign="Center" CssClass="grid" />
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="<%$ Resources:Attendance, Handled Employee %>" SortExpression="HandledEmployee">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblgvHandledEmployee" runat="server" Text='<%#Eval("HandledEmployee") %>' />
                                                        </ItemTemplate>
                                                        <ItemStyle HorizontalAlign="Center" CssClass="grid" />
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="<%$ Resources:Attendance, Status %>" SortExpression="HandledEmployee">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblgvStstus" runat="server" Text='<%# getStatus(Eval("Field1").ToString()) %>' />
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
                                    <asp:Panel ID="PnlNewEdit" runat="server" DefaultButton="btnSInquirySave">
                                        <asp:Panel ID="PnlNewContant" runat="server">
                                            <table width="100%" style="padding-left: 43px">
                                                <tr>
                                                    <td align='<%= Common.ChangeTDForDefaultLeft()%>'>
                                                        <asp:Label ID="lblInquiryDate" runat="server" CssClass="labelComman" Text="<%$ Resources:Attendance,Inquiry Date %>" />
                                                    </td>
                                                    <td align="center">
                                                        :
                                                    </td>
                                                    <td align='<%= Common.ChangeTDForDefaultLeft()%>'>
                                                        <asp:TextBox ID="txtInquiryDate" runat="server" CssClass="textComman" />
                                                        <cc1:CalendarExtender ID="Calender" runat="server" TargetControlID="txtInquiryDate"
                                                            Format="dd/MMM/yyyy" />
                                                    </td>
                                                    <td align='<%= Common.ChangeTDForDefaultLeft()%>'>
                                                        <asp:Label ID="lblInquiryNo" runat="server" CssClass="labelComman" Text="<%$ Resources:Attendance,Inquiry No. %>" />
                                                    </td>
                                                    <td align="center">
                                                        :
                                                    </td>
                                                    <td align='<%= Common.ChangeTDForDefaultLeft()%>'>
                                                        <asp:TextBox ID="txtInquiryNo" runat="server" CssClass="textComman" />
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td align='<%= Common.ChangeTDForDefaultLeft()%>'>
                                                        <asp:Label ID="lblTenderNo" runat="server" CssClass="labelComman" Text="<%$ Resources:Attendance,Tender No %>" />
                                                    </td>
                                                    <td align="center">
                                                        :
                                                    </td>
                                                    <td align='<%= Common.ChangeTDForDefaultLeft()%>'>
                                                        <asp:TextBox ID="txtTenderNo" runat="server" CssClass="textComman" />
                                                    </td>
                                                    <td align='<%= Common.ChangeTDForDefaultLeft()%>'>
                                                        <asp:Label ID="lblTenderDate" runat="server" CssClass="labelComman" Text="<%$ Resources:Attendance,Tender Date %>" />
                                                    </td>
                                                    <td align="center">
                                                        :
                                                    </td>
                                                    <td align='<%= Common.ChangeTDForDefaultLeft()%>'>
                                                        <asp:TextBox ID="txtTenderDate" runat="server" CssClass="textComman" />
                                                        <cc1:CalendarExtender ID="CalendarExtender1" runat="server" TargetControlID="txtTenderDate"
                                                            Format="dd/MMM/yyyy" />
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
                                                        <asp:TextBox ID="txtCustomerName" runat="server" CssClass="textComman" Width="708px"
                                                            BackColor="#c3c3c3" OnTextChanged="txtCustomerName_TextChanged" AutoPostBack="true" />
                                                        <cc1:AutoCompleteExtender ID="txtCustomerName_AutoCompleteExtender" runat="server"
                                                            DelimiterCharacters="" Enabled="True" CompletionInterval="100" CompletionSetCount="1"
                                                            MinimumPrefixLength="1" ServiceMethod="GetCompletionListCustomer" ServicePath=""
                                                            TargetControlID="txtCustomerName" UseContextKey="True" CompletionListCssClass="completionList"
                                                            CompletionListItemCssClass="listItem" CompletionListHighlightedItemCssClass="itemHighlighted">
                                                        </cc1:AutoCompleteExtender>
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td align='<%= Common.ChangeTDForDefaultLeft()%>'>
                                                        <asp:Label ID="lblCurrency" runat="server" CssClass="labelComman" Text="<%$ Resources:Attendance,Currency %>" />
                                                    </td>
                                                    <td align="center">
                                                        :
                                                    </td>
                                                    <td align='<%= Common.ChangeTDForDefaultLeft()%>'>
                                                        <asp:DropDownList ID="ddlCurrency" runat="server" Width="260px" CssClass="textComman" />
                                                    </td>
                                                    <td align='<%= Common.ChangeTDForDefaultLeft()%>'>
                                                        <asp:Label ID="lblInquiryType" runat="server" CssClass="labelComman" Text="<%$ Resources:Attendance,Inquiry Type %>" />
                                                    </td>
                                                    <td align="center">
                                                        :
                                                    </td>
                                                    <td align='<%= Common.ChangeTDForDefaultLeft()%>'>
                                                        <asp:DropDownList ID="ddlInquiryType" runat="server" Width="260px" CssClass="textComman">
                                                            <asp:ListItem Value="--Select--" Text="--Select--"></asp:ListItem>
                                                            <asp:ListItem Value="Email" Text="Email"></asp:ListItem>
                                                            <asp:ListItem Value="Direct" Text="Direct"></asp:ListItem>
                                                        </asp:DropDownList>
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td align='<%= Common.ChangeTDForDefaultLeft()%>'>
                                                        <asp:Label ID="lblOrderCloseDate" runat="server" CssClass="labelComman" Text="<%$ Resources:Attendance,Order Close Date %>" />
                                                    </td>
                                                    <td align="center">
                                                        :
                                                    </td>
                                                    <td align='<%= Common.ChangeTDForDefaultLeft()%>'>
                                                        <asp:TextBox ID="txtOrderCompletionDate" runat="server" CssClass="textComman" />
                                                        <cc1:CalendarExtender ID="CalendarExtender_txtOrderCompletionDate" runat="server"
                                                            TargetControlID="txtOrderCompletionDate" Format="dd/MMM/yyyy" />
                                                    </td>
                                                    <td>
                                                    </td>
                                                    <td>
                                                    </td>
                                                    <td>
                                                        <asp:Button ID="btnAddNewProduct" runat="server" Text="<%$ Resources:Attendance,Add Product %>"
                                                            BackColor="Transparent" BorderStyle="None" Font-Bold="true" CssClass="btnLocation"
                                                            Width="171px" Font-Size="13px" Font-Names=" Arial" Height="32px" OnClick="btnAddNewProduct_Click" />
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td colspan="6" align="center">
                                                        <asp:GridView ID="GvProduct" runat="server" Width="100%" CssClass="grid" AutoGenerateColumns="False">
                                                            <AlternatingRowStyle CssClass="InvgridAltRow" />
                                                            <Columns>
                                                                <asp:TemplateField HeaderText="Edit">
                                                                    <ItemTemplate>
                                                                        <asp:ImageButton ID="imgBtnProductEdit" runat="server" CommandArgument='<%# Eval("Serial_No") %>'
                                                                            ImageUrl="~/Images/edit.png" Width="16px" OnCommand="imgBtnProductEdit_Command" />
                                                                    </ItemTemplate>
                                                                    <ItemStyle CssClass="grid" HorizontalAlign="Center" />
                                                                </asp:TemplateField>
                                                                <asp:TemplateField HeaderText="Delete">
                                                                    <ItemTemplate>
                                                                        <asp:ImageButton ID="imgBtnProductDelete" runat="server" CommandArgument='<%# Eval("Serial_No") %>'
                                                                            Height="16px" ImageUrl="~/Images/Erase.png" Width="16px" OnCommand="imgBtnProductDelete_Command" />
                                                                    </ItemTemplate>
                                                                    <ItemStyle CssClass="grid" HorizontalAlign="Center" />
                                                                </asp:TemplateField>
                                                                <asp:TemplateField HeaderText="<%$ Resources:Attendance,S No. %>">
                                                                    <ItemTemplate>
                                                                        <asp:Label ID="lblSNo" runat="server" Text='<%#Eval("Serial_No") %>' />
                                                                    </ItemTemplate>
                                                                    <ItemStyle CssClass="grid" HorizontalAlign="Center" />
                                                                </asp:TemplateField>
                                                                <asp:TemplateField HeaderText="<%$ Resources:Attendance, Product Name %>">
                                                                    <ItemTemplate>
                                                                        <asp:Label ID="lblgvProductId" runat="server" Text='<%#Eval("Product_Id") %>' Visible="false" />
                                                                        <asp:Label ID="lblgvProductName" runat="server" Text='<%#GetProductName(Eval("Product_Id").ToString()) %>' />
                                                                    </ItemTemplate>
                                                                    <ItemStyle CssClass="grid" HorizontalAlign="Center" />
                                                                </asp:TemplateField>
                                                                <asp:TemplateField HeaderText="<%$ Resources:Attendance, Unit %>">
                                                                    <ItemTemplate>
                                                                        <asp:Label ID="lblgvUnitId" runat="server" Visible="false" Text='<%#Eval("UnitId") %>' />
                                                                        <asp:Label ID="lblgvUnit" runat="server" Text='<%#GetUnitName(Eval("UnitId").ToString()) %>' />
                                                                    </ItemTemplate>
                                                                    <ItemStyle CssClass="grid" HorizontalAlign="Center" />
                                                                </asp:TemplateField>
                                                                <asp:TemplateField HeaderText="<%$ Resources:Attendance, Product Description %>">
                                                                    <ItemTemplate>
                                                                        <asp:Label ID="lblgvProductDescription" runat="server" Text='<%#Eval("ProductDescription") %>' />
                                                                    </ItemTemplate>
                                                                    <ItemStyle CssClass="grid" HorizontalAlign="Center" />
                                                                </asp:TemplateField>
                                                                <asp:TemplateField HeaderText="<%$ Resources:Attendance, Required Quantity %>">
                                                                    <ItemTemplate>
                                                                        <asp:Label ID="lblgvRequiredQty" runat="server" Text='<%#Eval("Quantity") %>' />
                                                                    </ItemTemplate>
                                                                    <ItemStyle CssClass="grid" HorizontalAlign="Center" />
                                                                </asp:TemplateField>
                                                                <asp:TemplateField HeaderText="<%$ Resources:Attendance, Currency %>">
                                                                    <ItemTemplate>
                                                                        <asp:Label ID="lblgvCurrencyId" runat="server" Visible="false" Text='<%#Eval("Currency_Id") %>' />
                                                                        <asp:Label ID="lblgvCurrency" runat="server" Text='<%#GetCurrencyName(Eval("Currency_Id").ToString()) %>' />
                                                                    </ItemTemplate>
                                                                    <ItemStyle CssClass="grid" HorizontalAlign="Center" />
                                                                </asp:TemplateField>
                                                                <asp:TemplateField HeaderText="<%$ Resources:Attendance, Estimated Unit Price %>">
                                                                    <ItemTemplate>
                                                                        <asp:Label ID="lblgvEstimatedUnitPrice" runat="server" Text='<%#Eval("EstimatedUnitPrice") %>' />
                                                                    </ItemTemplate>
                                                                    <ItemStyle CssClass="grid" HorizontalAlign="Center" />
                                                                </asp:TemplateField>
                                                            </Columns>
                                                            <HeaderStyle CssClass="Invgridheader" />
                                                            <PagerStyle CssClass="Invgridheader" />
                                                            <RowStyle CssClass="Invgridrow" HorizontalAlign="Center" />
                                                        </asp:GridView>
                                                        <asp:HiddenField ID="hdnProductId" runat="server" />
                                                        <asp:HiddenField ID="hdnProductName" runat="server" />
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
                                                        <asp:TextBox ID="txtRemark" runat="server" CssClass="textComman" Width="708px" TextMode="MultiLine" />
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td align='<%= Common.ChangeTDForDefaultLeft()%>'>
                                                        <asp:Label ID="lblReceivedEmp" runat="server" CssClass="labelComman" Text="<%$ Resources:Attendance,Received Employee %>" />
                                                    </td>
                                                    <td align="center">
                                                        :
                                                    </td>
                                                    <td align='<%= Common.ChangeTDForDefaultLeft()%>'>
                                                        <asp:TextBox ID="txtReceivedEmp" runat="server" CssClass="textComman" BackColor="#c3c3c3"
                                                            OnTextChanged="txtReceivedEmp_TextChanged" AutoPostBack="true" />
                                                        <cc1:AutoCompleteExtender ID="txtReceivedEmp_AutoCompleteExtender" runat="server"
                                                            DelimiterCharacters="" Enabled="True" CompletionInterval="100" CompletionSetCount="1"
                                                            MinimumPrefixLength="1" ServiceMethod="GetCompletionListEmployeeName" ServicePath=""
                                                            TargetControlID="txtReceivedEmp" UseContextKey="True" CompletionListCssClass="completionList"
                                                            CompletionListItemCssClass="listItem" CompletionListHighlightedItemCssClass="itemHighlighted">
                                                        </cc1:AutoCompleteExtender>
                                                    </td>
                                                    <td align='<%= Common.ChangeTDForDefaultLeft()%>'>
                                                        <asp:Label ID="lblHandledEmp" runat="server" CssClass="labelComman" Text="<%$ Resources:Attendance,Handled Employee %>" />
                                                    </td>
                                                    <td align="center">
                                                        :
                                                    </td>
                                                    <td align='<%= Common.ChangeTDForDefaultLeft()%>'>
                                                        <asp:TextBox ID="txtHandledEmp" runat="server" CssClass="textComman" BackColor="#c3c3c3"
                                                            OnTextChanged="txtHandledEmp_TextChanged" AutoPostBack="true" />
                                                        <cc1:AutoCompleteExtender ID="txtHandledEmp_AutoCompleteExtender" runat="server"
                                                            DelimiterCharacters="" Enabled="True" CompletionInterval="100" CompletionSetCount="1"
                                                            MinimumPrefixLength="1" ServiceMethod="GetCompletionListEmployeeName" ServicePath=""
                                                            TargetControlID="txtHandledEmp" UseContextKey="True" CompletionListCssClass="completionList"
                                                            CompletionListItemCssClass="listItem" CompletionListHighlightedItemCssClass="itemHighlighted">
                                                        </cc1:AutoCompleteExtender>
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td align="center" colspan="6">
                                                        <table width="100%">
                                                            <tr>
                                                                <td align="right">
                                                                    <asp:CheckBox ID="chkBuyingPriority" runat="server" CssClass="labelComman" Text="<%$ Resources:Attendance,Buying Priority %>" />
                                                                </td>
                                                                <td align="center">
                                                                    <asp:CheckBox ID="chkSendMail" runat="server" CssClass="labelComman" Text="<%$ Resources:Attendance,Send Email %>" />
                                                                </td>
                                                                <td align="left">
                                                                    <asp:CheckBox ID="chkPost" runat="server" CssClass="labelComman" Text="<%$ Resources:Attendance,Post %>" />
                                                                </td>
                                                            </tr>
                                                        </table>
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td align='<%= Common.ChangeTDForDefaultLeft()%>'>
                                                        <asp:Label ID="lblCondition1" runat="server" CssClass="labelComman" Text="<%$ Resources:Attendance,Condition1 %>" />
                                                    </td>
                                                    <td align="center">
                                                        :
                                                    </td>
                                                    <td align='<%= Common.ChangeTDForDefaultLeft()%>' colspan="4">
                                                        <asp:TextBox ID="txtCondition1" runat="server" CssClass="textComman" Width="708px"
                                                            TextMode="MultiLine" />
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td align='<%= Common.ChangeTDForDefaultLeft()%>'>
                                                        <asp:Label ID="lblCondition2" runat="server" CssClass="labelComman" Text="<%$ Resources:Attendance,Condition2 %>" />
                                                    </td>
                                                    <td align="center">
                                                        :
                                                    </td>
                                                    <td align='<%= Common.ChangeTDForDefaultLeft()%>' colspan="4">
                                                        <asp:TextBox ID="txtCondition2" runat="server" CssClass="textComman" Width="708px"
                                                            TextMode="MultiLine" />
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td align='<%= Common.ChangeTDForDefaultLeft()%>'>
                                                        <asp:Label ID="lblCondition3" runat="server" CssClass="labelComman" Text="<%$ Resources:Attendance,Condition3 %>" />
                                                    </td>
                                                    <td align="center">
                                                        :
                                                    </td>
                                                    <td align='<%= Common.ChangeTDForDefaultLeft()%>' colspan="4">
                                                        <asp:TextBox ID="txtCondition3" runat="server" CssClass="textComman" Width="708px"
                                                            TextMode="MultiLine" />
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td align='<%= Common.ChangeTDForDefaultLeft()%>'>
                                                        <asp:Label ID="lblCondition4" runat="server" CssClass="labelComman" Text="<%$ Resources:Attendance,Condition4 %>" />
                                                    </td>
                                                    <td align="center">
                                                        :
                                                    </td>
                                                    <td align='<%= Common.ChangeTDForDefaultLeft()%>' colspan="4">
                                                        <asp:TextBox ID="txtCondition4" runat="server" CssClass="textComman" Width="708px"
                                                            TextMode="MultiLine" />
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td align='<%= Common.ChangeTDForDefaultLeft()%>'>
                                                        <asp:Label ID="lblCondition5" runat="server" CssClass="labelComman" Text="<%$ Resources:Attendance,Condition5 %>" />
                                                    </td>
                                                    <td align="center">
                                                        :
                                                    </td>
                                                    <td align='<%= Common.ChangeTDForDefaultLeft()%>' colspan="4">
                                                        <asp:TextBox ID="txtCondition5" runat="server" CssClass="textComman" Width="708px"
                                                            TextMode="MultiLine" />
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td align='<%= Common.ChangeTDForDefaultLeft()%>'>
                                                        <asp:Label ID="Label2" runat="server" CssClass="labelComman" Text="Send In Purchase" />
                                                    </td>
                                                    <td align="center">
                                                        :
                                                    </td>
                                                    <td align='<%= Common.ChangeTDForDefaultLeft()%>' colspan="4">
                                                        <asp:CheckBox ID="ChkSendInPurchase" runat="server" Text="Yes" CssClass="labelComman" />
                                                    </td>
                                                </tr>
                                            </table>
                                        </asp:Panel>
                                        <asp:Panel ID="PnlOperationButton" runat="server">
                                            <table width="100%" style="padding-left: 43px">
                                                <tr>
                                                    <td colspan="2">
                                                    </td>
                                                    <td colspan="4" align="center" style="padding-left: 10px">
                                                        <table>
                                                            <tr>
                                                                <td width="90px">
                                                                    <asp:Button ID="btnSInquirySave" runat="server" Text="<%$ Resources:Attendance,Save %>"
                                                                        CssClass="buttonCommman" OnClick="btnSInquirySave_Click" />
                                                                </td>
                                                                <td width="90px">
                                                                    <asp:Button ID="BtnReset" runat="server" Text="<%$ Resources:Attendance,Reset %>"
                                                                        CssClass="buttonCommman" CausesValidation="False" OnClick="BtnReset_Click" />
                                                                </td>
                                                                <td width="90px">
                                                                    <asp:Button ID="btnSInquiryCancel" runat="server" CssClass="buttonCommman" Text="<%$ Resources:Attendance,Cancel %>"
                                                                        CausesValidation="False" OnClick="btnSInquiryCancel_Click" />
                                                                </td>
                                                            </tr>
                                                        </table>
                                                        <asp:HiddenField ID="hdnSInquiryId" runat="server" Value="0" />
                                                    </td>
                                                </tr>
                                            </table>
                                        </asp:Panel>
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
                                                            <asp:ListItem Text="<%$ Resources:Attendance, Inquiry Id %>" Value="SInquiryID"></asp:ListItem>
                                                            <asp:ListItem Text="<%$ Resources:Attendance, Inquiry No. %>" Value="SInquiryNo"
                                                                Selected="True"></asp:ListItem>
                                                            <asp:ListItem Text="<%$ Resources:Attendance, Inquiry Date %>" Value="IDate"></asp:ListItem>
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
                                        <asp:GridView ID="GvSalesInquiryBin" PageSize="10" runat="server" AutoGenerateColumns="False"
                                            Width="100%" AllowPaging="True" OnPageIndexChanging="GvSalesInquiryBin_PageIndexChanging"
                                            OnSorting="GvSalesInquiryBin_OnSorting" AllowSorting="true" CssClass="grid">
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
                                                <asp:TemplateField HeaderText="<%$ Resources:Attendance,Inquiry Id %>" SortExpression="SInquiryID">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblgvInquiryId" runat="server" Text='<%#Eval("SInquiryID") %>' />
                                                    </ItemTemplate>
                                                    <ItemStyle HorizontalAlign="Center" CssClass="grid" />
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="<%$ Resources:Attendance, Inquiry No. %>" SortExpression="SInquiryNo">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblgvInquiryNo" runat="server" Text='<%#Eval("SInquiryNo") %>' />
                                                    </ItemTemplate>
                                                    <ItemStyle HorizontalAlign="Center" CssClass="grid" />
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="<%$ Resources:Attendance, Inquiry Date %>" SortExpression="IDate">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblgvInquiryDate" runat="server" Text='<%#GetDate(Eval("IDate").ToString()) %>' />
                                                    </ItemTemplate>
                                                    <ItemStyle HorizontalAlign="Center" CssClass="grid" />
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="<%$ Resources:Attendance, Customer Name %>" SortExpression="Contact_Name">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblgvCustomerName" runat="server" Text='<%#Eval("Contact_Name") %>' />
                                                    </ItemTemplate>
                                                    <ItemStyle HorizontalAlign="Center" CssClass="grid" />
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="<%$ Resources:Attendance, Received Employee %>" SortExpression="ReceivedEmployee">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblgvReceivedEmployee" runat="server" Text='<%#Eval("ReceivedEmployee") %>' />
                                                    </ItemTemplate>
                                                    <ItemStyle HorizontalAlign="Center" CssClass="grid" />
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="<%$ Resources:Attendance, Handled Employee %>" SortExpression="HandledEmployee">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblgvHandledEmployee" runat="server" Text='<%#Eval("HandledEmployee") %>' />
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
                                                    </tr>
                                                    <tr>
                                                        <td align='<%= Common.ChangeTDForDefaultLeft()%>'>
                                                            <asp:Label ID="lblPDescription" runat="server" CssClass="labelComman" Text="<%$ Resources:Attendance,Product Description %>" />
                                                        </td>
                                                        <td align="center">
                                                            :
                                                        </td>
                                                        <td align='<%= Common.ChangeTDForDefaultLeft()%>' colspan="4">
                                                           <%-- <asp:TextBox ID="txtPDescription" Width="610px" TextMode="MultiLine" runat="server"
                                                                CssClass="textComman" />--%> 
                                                              <asp:Panel ID="pnlPDescription" runat="server" Width="615px" Height="100px" CssClass="textComman"
                                                                BorderColor="#8ca7c1" BackColor="#ffffff" ScrollBars="Vertical" Visible="false">
                                                                <asp:Literal ID="txtPDescription" runat="server"></asp:Literal>
                                                            </asp:Panel>
                                                            <asp:TextBox ID="txtPDesc" runat="server" Width="615px"  Height="100px" TextMode="MultiLine" CssClass="textComman"></asp:TextBox>

                                                                
                                                       </td>
                                                    </tr>
                                                    <tr>
                                                        <td align='<%= Common.ChangeTDForDefaultLeft()%>'>
                                                            <asp:Label ID="lblRequiredQty" runat="server" CssClass="labelComman" Text="<%$ Resources:Attendance,Required Quantity %>" />
                                                        </td>
                                                        <td align="center">
                                                            :
                                                        </td>
                                                        <td align='<%= Common.ChangeTDForDefaultLeft()%>'>
                                                            <asp:TextBox ID="txtRequiredQty" runat="server" CssClass="textComman" />
                                                             <cc1:FilteredTextBoxExtender ID="FilteredTextBoxExtender2" runat="server" Enabled="True"
                                                        TargetControlID="txtRequiredQty" ValidChars="1,2,3,4,5,6,7,8,9,0,.">
                                                    </cc1:FilteredTextBoxExtender>
                                                        </td>
                                                        <td align='<%= Common.ChangeTDForDefaultLeft()%>'>
                                                            <asp:Label ID="lblPCurrency" runat="server" CssClass="labelComman" Text="<%$ Resources:Attendance,Currency %>" />
                                                        </td>
                                                        <td align="center">
                                                            :
                                                        </td>
                                                        <td align='<%= Common.ChangeTDForDefaultLeft()%>'>
                                                            <asp:DropDownList ID="ddlPCurrency" runat="server" CssClass="textComman" Width="260px" />
                                                            <asp:HiddenField ID="hdnCurrencyId" runat="server" Value="0" />
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td align='<%= Common.ChangeTDForDefaultLeft()%>'>
                                                            <asp:Label ID="lblEstimatedUnitPrice" runat="server" CssClass="labelComman" Text="<%$ Resources:Attendance,Estimated Unit Price %>" />
                                                        </td>
                                                        <td align="center">
                                                            :
                                                        </td>
                                                        <td align='<%= Common.ChangeTDForDefaultLeft()%>'>
                                                            <asp:TextBox ID="txtEstimatedUnitPrice" runat="server" CssClass="textComman" />
                                                              <cc1:FilteredTextBoxExtender ID="FilteredTextBoxExtender1" runat="server" Enabled="True"
                                                        TargetControlID="txtEstimatedUnitPrice" ValidChars="1,2,3,4,5,6,7,8,9,0,.">
                                                    </cc1:FilteredTextBoxExtender>
                                                  
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
