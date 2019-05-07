<%@ Page Language="C#" MasterPageFile="~/ERPMaster.master" AutoEventWireup="true"
    CodeFile="PurchaseInquiry.aspx.cs" Inherits="Purchase_PurchaseInquiry" Title="Untitled Page" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    
    <asp:UpdatePanel ID="upallpage" runat="server">
        <Triggers>
            <asp:PostBackTrigger ControlID="btnList" />
            <asp:PostBackTrigger ControlID="btnNew" />
            <asp:PostBackTrigger ControlID="btnBin" />
           
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
                                                <asp:Label ID="lblHeader" runat="server" Text="<%$ Resources:Attendance,Purchase Inquiry %>"
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
                                                    <asp:Button ID="btnPRequest" runat="server" Text="<%$ Resources:Attendance,Request %>"
                                                        Width="100px" BorderStyle="none" BackColor="Transparent" OnClick="btnPRequest_Click"
                                                        Style="padding-left: 30px; padding-top: 3px; background-image: url('../Images/Request.png' );
                                                        background-repeat: no-repeat; height: 49px; background-position: 1px 15px; font: bold 14px Trebuchet MS;
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
                                                                <asp:ListItem Text="<%$ Resources:Attendance, PI Id %>" Value="Trans_Id"></asp:ListItem>
                                                                <asp:ListItem Text="<%$ Resources:Attendance, Inquiry No. %>" Value="PI_No" Selected="True"></asp:ListItem>
                                                                <asp:ListItem Text="<%$ Resources:Attendance, Inquiry Date %>" Value="PIDate"></asp:ListItem>
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
                                            <asp:GridView ID="GvPurchaseInquiry" PageSize="<%# SystemParameter.GetPageSize() %>"
                                                runat="server" AutoGenerateColumns="False" Width="100%" AllowPaging="True" OnPageIndexChanging="GvPurchaseInquiry_PageIndexChanging"
                                                AllowSorting="True" OnSorting="GvPurchaseInquiry_Sorting" CssClass="grid">
                                                <AlternatingRowStyle CssClass="InvgridAltRow" />
                                                <Columns>
                                                    <asp:TemplateField>
                                                        <ItemTemplate>
                                                            <asp:ImageButton ID="btnEdit" runat="server" CommandArgument='<%# Eval("Trans_Id") %>'
                                                                ImageUrl="~/Images/edit.png" OnCommand="btnEdit_Command" CausesValidation="False"
                                                                Visible="false" /></ItemTemplate>
                                                        <ItemStyle CssClass="grid" HorizontalAlign="Center" />
                                                    </asp:TemplateField>
                                                    <asp:TemplateField>
                                                        <ItemTemplate>
                                                            <asp:ImageButton ID="IbtnDelete" runat="server" CausesValidation="False" CommandArgument='<%# Eval("Trans_Id") %>'
                                                                ImageUrl="~/Images/Erase.png" OnCommand="IbtnDelete_Command" Width="16px" Visible="false" />
                                                        </ItemTemplate>
                                                        <ItemStyle CssClass="grid" HorizontalAlign="Center" />
                                                    </asp:TemplateField>
                                                    <asp:BoundField DataField="PI_No" HeaderText="<%$ Resources:Attendance,Purchase Inquiry No. %>"
                                                        SortExpression="PI_No" ItemStyle-CssClass="grid" />
                                                    <asp:TemplateField SortExpression="PIDate" HeaderText="<%$ Resources:Attendance,Purchase Inquiry Date %>">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblgvDate" runat="server" Text='<%#GetDate(Eval("PIDate").ToString()) %>' />
                                                        </ItemTemplate>
                                                        <ItemStyle HorizontalAlign="Center" CssClass="grid" />
                                                    </asp:TemplateField>
                                                    <asp:BoundField DataField="Description" HeaderText="<%$ Resources:Attendance,Description %>"
                                                        SortExpression="Description" ItemStyle-CssClass="grid" />
                                                </Columns>
                                                <HeaderStyle CssClass="Invgridheader" />
                                                <PagerStyle CssClass="Invgridheader" />
                                                <RowStyle CssClass="Invgridrow" HorizontalAlign="Center" />
                                            </asp:GridView>
                                        </asp:Panel>
                                    </asp:Panel>
                                    <asp:Panel ID="PnlNewEdit" runat="server" DefaultButton="btnPISave">
                                        <table width="100%" style="padding-left: 43px;">
                                            <tr>
                                                <td align='<%= Common.ChangeTDForDefaultLeft()%>' width="130px">
                                                    <asp:Label ID="lblPIDate" runat="server" CssClass="labelComman" Text="<%$ Resources:Attendance,PI Date %>"></asp:Label>
                                                </td>
                                                <td align="center">
                                                    :
                                                </td>
                                                <td align='<%= Common.ChangeTDForDefaultLeft()%>'>
                                                    <asp:TextBox ID="txtPIDate" runat="server" CssClass="textComman" />
                                                    <cc1:CalendarExtender ID="Calender" runat="server" TargetControlID="txtPIDate" Format="dd/MMM/yyyy" />
                                                </td>
                                                <td align='<%= Common.ChangeTDForDefaultLeft()%>' width="145px">
                                                    <asp:Label ID="lblPINo" runat="server" CssClass="labelComman" Text="<%$ Resources:Attendance,Purchase Inquiry No. %>"></asp:Label>
                                                </td>
                                                <td align="center">
                                                    :
                                                </td>
                                                <td align='<%= Common.ChangeTDForDefaultLeft()%>'>
                                                    <asp:TextBox ID="txtPINo" runat="server" CssClass="textComman" />
                                                </td>
                                            </tr>
                                            <tr>
                                                <td colspan="6" align='<%= Common.ChangeTDForDefaultLeft()%>'>
                                                    <asp:Panel ID="pnlTrans" runat="server" Visible="false">
                                                        <table width="100%">
                                                            <tr>
                                                                <td align='<%= Common.ChangeTDForDefaultLeft()%>' width="135px">
                                                                    <asp:Label ID="lblTransFrom" runat="server" CssClass="labelComman" Text="<%$ Resources:Attendance,Transfer From %>" />
                                                                </td>
                                                                <td align="center">
                                                                    :
                                                                </td>
                                                                <td align='<%= Common.ChangeTDForDefaultLeft()%>'>
                                                                    <asp:TextBox ID="txtTransFrom" runat="server" CssClass="textComman" ReadOnly="True" />
                                                                </td>
                                                                <td align='<%= Common.ChangeTDForDefaultLeft()%>' width="160px">
                                                                    <asp:Label ID="lblTransNo" runat="server" CssClass="labelComman" Text="<%$ Resources:Attendance,Transfer No.%>" />
                                                                </td>
                                                                <td align="center">
                                                                    :
                                                                </td>
                                                                <td align='<%= Common.ChangeTDForDefaultLeft()%>'>
                                                                    <asp:TextBox ID="txtTransNo" runat="server" CssClass="textComman" ReadOnly="True" />
                                                                </td>
                                                            </tr>
                                                        </table>
                                                    </asp:Panel>
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
                                                        BackColor="Transparent" BorderStyle="None" Font-Bold="true" CssClass="btnLocation"
                                                        Width="171px" Font-Size="13px" Font-Names=" Arial" Height="32px" OnClick="btnAddNewProduct_Click"
                                                        Visible="False" />
                                                </td>
                                            </tr>
                                            <tr>
                                                <td colspan="6" align="center">
                                                    <asp:Panel ID="pnlProduct" runat="server">
                                                        <asp:GridView ID="GvProduct" runat="server" Width="100%" CssClass="grid" AutoGenerateColumns="False">
                                                            <AlternatingRowStyle CssClass="InvgridAltRow" />
                                                            <Columns>
                                                                <asp:TemplateField HeaderText="Edit">
                                                                    <ItemTemplate>
                                                                        <asp:ImageButton ID="imgBtnProductEdit" runat="server" CommandArgument='<%# Eval("Serial_No") %>'
                                                                            ImageUrl="~/Images/edit.png" Width="16px" Visible="false" OnCommand="imgBtnProductEdit_Command" />
                                                                    </ItemTemplate>
                                                                    <ItemStyle CssClass="grid" HorizontalAlign="Center" />
                                                                </asp:TemplateField>
                                                                <asp:TemplateField HeaderText="Delete">
                                                                    <ItemTemplate>
                                                                        <asp:ImageButton ID="imgBtnProductDelete" runat="server" CommandArgument='<%# Eval("Serial_No") %>'
                                                                            Height="16px" ImageUrl="~/Images/Erase.png" Width="16px" Visible="false" OnCommand="imgBtnProductDelete_Command" />
                                                                    </ItemTemplate>
                                                                    <ItemStyle CssClass="grid" HorizontalAlign="Center" />
                                                                </asp:TemplateField>
                                                                <asp:TemplateField HeaderText="<%$ Resources:Attendance,S No. %>">
                                                                    <ItemTemplate>
                                                                        <asp:Label ID="lblSNo" runat="server" Text='<%#Eval("Serial_No") %>' />
                                                                    </ItemTemplate>
                                                                    <ItemStyle CssClass="grid" HorizontalAlign="Center" Width="50px" />
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
                                                                <asp:TemplateField HeaderText="<%$ Resources:Attendance, Required Quantity %>">
                                                                    <ItemTemplate>
                                                                        <asp:Label ID="lblgvRequiredQty" runat="server" Text='<%#Eval("ReqQty") %>' />
                                                                    </ItemTemplate>
                                                                    <ItemStyle CssClass="grid" HorizontalAlign="Center" Width="120px" />
                                                                </asp:TemplateField>
                                                                <asp:TemplateField HeaderText="<%$ Resources:Attendance, Product Description %>">
                                                                    <ItemTemplate>
                                                                        <asp:Label ID="lblgvProductDescription" runat="server" Text='<%#Eval("ProductDescription") %>' />
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
                                                    </asp:Panel>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td align='<%= Common.ChangeTDForDefaultLeft()%>'>
                                                    <asp:Label ID="lblDesription" runat="server" CssClass="labelComman" Text="<%$ Resources:Attendance,Description %>" />
                                                </td>
                                                <td>
                                                    :
                                                </td>
                                                <td align='<%= Common.ChangeTDForDefaultLeft()%>' colspan="4">
                                                    <asp:TextBox ID="txtDescription" runat="server" Width="685px" TextMode="MultiLine"
                                                        CssClass="textComman" />
                                                </td>
                                            </tr>
                                            <tr>
                                                <td colspan="6" align="center" style="padding-left: 10px">
                                                    <table>
                                                        <tr>
                                                            <td width="90px">
                                                                <asp:Button ID="btnPISave" runat="server" Text="<%$ Resources:Attendance,Next %>"
                                                                    CssClass="buttonCommman" OnClick="btnPISave_Click" />
                                                            </td>
                                                            <td width="90px">
                                                                <asp:Button ID="BtnReset" runat="server" Text="<%$ Resources:Attendance,Reset %>"
                                                                    CssClass="buttonCommman" CausesValidation="False" OnClick="BtnReset_Click" />
                                                            </td>
                                                            <td width="90px">
                                                                <asp:Button ID="btnPICancel" runat="server" CssClass="buttonCommman" Text="<%$ Resources:Attendance,Cancel %>"
                                                                    CausesValidation="False" OnClick="btnPICancel_Click" />
                                                            </td>
                                                        </tr>
                                                    </table>
                                                    <asp:HiddenField ID="editid" runat="server" />
                                                </td>
                                            </tr>
                                        </table>
                                    </asp:Panel>
                                    <asp:Panel ID="pnlSupplier" runat="server" Visible="false" DefaultButton="btnSaveSupplier">
                                        <table width="100%" style="padding-left: 43px">
                                            <tr>
                                                <td align='<%= Common.ChangeTDForDefaultLeft()%>'>
                                                    <asp:Label ID="lblSInquiryDate" runat="server" CssClass="labelComman" Text="<%$ Resources:Attendance, Inquiry Date %>" />
                                                </td>
                                                <td align="center">
                                                    :
                                                </td>
                                                <td align='<%= Common.ChangeTDForDefaultLeft()%>' width="320px">
                                                    <asp:TextBox ID="txtSInquiryDate" runat="server" CssClass="textComman" ReadOnly="True" />
                                                </td>
                                                <td align='<%= Common.ChangeTDForDefaultLeft()%>'>
                                                    <asp:Label ID="lblSInquiryNo" runat="server" CssClass="labelComman" Text="<%$ Resources:Attendance,Inquiry No. %>" />
                                                </td>
                                                <td align="center">
                                                    :
                                                </td>
                                                <td align='<%= Common.ChangeTDForDefaultLeft()%>'>
                                                    <asp:TextBox ID="txtSInquiryNo" runat="server" CssClass="textComman" ReadOnly="True" />
                                                </td>
                                            </tr>
                                            <tr>
                                                <td align='<%= Common.ChangeTDForDefaultLeft()%>' valign="top">
                                                    <asp:Label ID="lblSupplierGroup" runat="server" CssClass="labelComman" Text="<%$ Resources:Attendance, Supplier Group %>" />
                                                </td>
                                                <td align="center" valign="top">
                                                    :
                                                </td>
                                                <td align='<%= Common.ChangeTDForDefaultLeft()%>' valign="top">
                                                    <asp:DropDownList ID="ddlSupplierGroup" runat="server" CssClass="textComman" Width="260px"
                                                        OnSelectedIndexChanged="ddlSupplierGroup_SelectedIndexChanged" AutoPostBack="true" />
                                                    <asp:GridView ID="GvPurchaseRequest" runat="server" AllowPaging="True" 
                                                        AllowSorting="True" AutoGenerateColumns="False" CssClass="grid" 
                                                        OnPageIndexChanging="GvPurchaseRequest_PageIndexChanging" 
                                                        OnSorting="GvPurchaseRequest_Sorting" 
                                                        PageSize="<%# SystemParameter.GetPageSize() %>" Width="100%">
                                                        <AlternatingRowStyle CssClass="InvgridAltRow" />
                                                        <Columns>
                                                            <asp:TemplateField>
                                                                <ItemTemplate>
                                                                    <asp:ImageButton ID="btnEdit" runat="server" CausesValidation="False" 
                                                                        CommandArgument='<%# Eval("Trans_Id") %>' ImageUrl="~/Images/edit.png" 
                                                                        OnCommand="btnPREdit_Command" />
                                                                </ItemTemplate>
                                                                <ItemStyle CssClass="grid" HorizontalAlign="Center" />
                                                            </asp:TemplateField>
                                                            <asp:TemplateField HeaderText="<%$ Resources:Attendance,Request Id %>" 
                                                                SortExpression="Trans_Id">
                                                                <ItemTemplate>
                                                                    <asp:Label ID="lblgvRequestId" runat="server" Text='<%#Eval("Trans_Id") %>' />
                                                                </ItemTemplate>
                                                                <ItemStyle CssClass="grid" HorizontalAlign="Center" />
                                                            </asp:TemplateField>
                                                            <asp:TemplateField HeaderText="<%$ Resources:Attendance,Request No %>" 
                                                                SortExpression="RequestNo">
                                                                <ItemTemplate>
                                                                    <asp:Label ID="lblgvRequestNo" runat="server" Text='<%#Eval("RequestNo") %>' />
                                                                </ItemTemplate>
                                                                <ItemStyle CssClass="grid" HorizontalAlign="Center" />
                                                            </asp:TemplateField>
                                                            <asp:TemplateField HeaderText="<%$ Resources:Attendance,Request Date %>" 
                                                                SortExpression="RequestDate">
                                                                <ItemTemplate>
                                                                    <asp:Label ID="lblgvRequestDate" runat="server" 
                                                                        Text='<%#GetDate(Eval("RequestDate").ToString()) %>' />
                                                                </ItemTemplate>
                                                                <ItemStyle CssClass="grid" HorizontalAlign="Center" />
                                                            </asp:TemplateField>
                                                            <asp:TemplateField HeaderText="Request Type">
                                                                <ItemTemplate>
                                                                    <asp:Label ID="lblgvRequestType" runat="server" 
                                                                        Text='<%#Eval("RequestType").ToString() %>' />
                                                                </ItemTemplate>
                                                                <ItemStyle CssClass="grid" HorizontalAlign="Center" />
                                                            </asp:TemplateField>
                                                        </Columns>
                                                        <HeaderStyle CssClass="Invgridheader" />
                                                        <PagerStyle CssClass="Invgridheader" />
                                                        <RowStyle CssClass="Invgridrow" HorizontalAlign="Center" />
                                                    </asp:GridView>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td colspan="6">
                                                    <asp:Panel ID="pnlChkSupplier" runat="server" Height="150px" Width="815px" BorderStyle="Solid"
                                                        BorderWidth="1px" BorderColor="#abadb3" BackColor="White" ScrollBars="Auto">
                                                        <asp:CheckBoxList ID="ChkSupplier" runat="server" RepeatColumns="5" RepeatDirection="Horizontal"
                                                            Font-Names="Trebuchet MS" Font-Size="Small" ForeColor="Gray" Width="100%" />
                                                    </asp:Panel>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td colspan="6" align="center" style="padding-left: 10px">
                                                    <table>
                                                        <tr>
                                                            <td width="90px">
                                                                <asp:Button ID="btnSaveSupplier" runat="server" Text="<%$ Resources:Attendance,Save %>"
                                                                    CssClass="buttonCommman" OnClick="btnSaveSupplier_Click" Visible="False" />
                                                            </td>
                                                            <td width="90px">
                                                                <asp:Button ID="btnResetSupplier" runat="server" Text="<%$ Resources:Attendance,Reset %>"
                                                                    CssClass="buttonCommman" CausesValidation="False" OnClick="btnResetSupplier_Click" />
                                                            </td>
                                                            <td width="90px">
                                                                <asp:Button ID="btnCancelSupplier" runat="server" CssClass="buttonCommman" Text="<%$ Resources:Attendance,Cancel %>"
                                                                    CausesValidation="False" OnClick="btnCancelSupplier_Click" />
                                                            </td>
                                                        </tr>
                                                    </table>
                                                    <asp:HiddenField ID="hdnSupplierId" runat="server" />
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
                                                            <asp:ListItem Text="<%$ Resources:Attendance, PI Id %>" Value="Trans_Id"></asp:ListItem>
                                                            <asp:ListItem Text="<%$ Resources:Attendance, Inquiry No. %>" Value="PI_No" Selected="True"></asp:ListItem>
                                                            <asp:ListItem Text="<%$ Resources:Attendance, Inquiry Date %>" Value="PIDate"></asp:ListItem>
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
                                                                Height="24px" Visible="False" />
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
                                        <asp:GridView ID="GvPurchaseInquiryBin" PageSize="<%# SystemParameter.GetPageSize() %>"
                                            runat="server" AutoGenerateColumns="False" Width="100%" AllowPaging="True" OnPageIndexChanging="GvPurchaseInquiryBin_PageIndexChanging"
                                            OnSorting="GvPurchaseInquiryBin_OnSorting" AllowSorting="true" CssClass="grid">
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
                                                <asp:TemplateField HeaderText="<%$ Resources:Attendance,Purchase Inquiry No.%>" SortExpression="PI_No">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblgvPINoId" runat="server" Text='<%# Eval("PI_No") %>' />
                                                        <asp:HiddenField ID="hdnTransId" runat="server" Value='<%#Eval("Trans_Id") %>' />
                                                    </ItemTemplate>
                                                    <ItemStyle CssClass="grid" />
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="<%$ Resources:Attendance,Purchase Inquiry Date %>"
                                                    SortExpression="PIDate">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblgvPIDate" runat="server" Text='<%#GetDate(Eval("PIDate").ToString()) %>' />
                                                    </ItemTemplate>
                                                    <ItemStyle CssClass="grid" />
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="<%$ Resources:Attendance,Description %>" SortExpression="Description">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblgvDescription" runat="server" Text='<%# Eval("Description") %>' />
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
                                                                OnTextChanged="txtProductName_TextChanged" BackColor="#c3c3c3" Width="615px" />
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
                                                            <asp:DropDownList ID="ddlUnit" runat="server" CssClass="textComman" />
                                                            <asp:TextBox ID="txtUnit" runat="server" CssClass="textComman" Visible="False" />
                                                            <asp:HiddenField ID="hdnNewUnitId" runat="server" Value="0" />
                                                        </td>
                                                        <td align='<%= Common.ChangeTDForDefaultLeft()%>'>
                                                            <asp:Label ID="lblRequestQty" runat="server" CssClass="labelComman" Text="<%$ Resources:Attendance,Request Quantity %>" />
                                                        </td>
                                                        <td align="center">
                                                            :
                                                        </td>
                                                        <td align='<%= Common.ChangeTDForDefaultLeft()%>'>
                                                            <asp:TextBox ID="txtRequestQty" runat="server" CssClass="textComman" />
                                                            <cc1:FilteredTextBoxExtender ID="FilteredTextBoxExtender1" runat="server" Enabled="True"
                                                                TargetControlID="txtRequestQty" ValidChars="1,2,3,4,5,6,7,8,9,0,.">
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
                                                            <asp:Panel ID="pnlPDescription" runat="server" Width="615px" Height="100px" CssClass="textComman"
                                                                BorderColor="#8ca7c1" BackColor="#ffffff" ScrollBars="Vertical" Visible="false">
                                                                <asp:Literal ID="txtPDescription" runat="server"></asp:Literal>
                                                            </asp:Panel>
                                                            <asp:TextBox ID="txtPDesc" runat="server" Width="615px"  Height="100px" TextMode="MultiLine" CssClass="textComman"></asp:TextBox>
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
                                        </asp:Panel>
                                    </asp:Panel>
                                    <asp:Panel ID="pnlRequest" runat="server">
                                        <asp:HiddenField ID="hdnTransNo" runat="server" Value="0" />
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
