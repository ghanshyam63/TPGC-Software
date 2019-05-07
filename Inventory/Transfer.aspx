<%@ Page Language="C#" MasterPageFile="~/ERPMaster.master" AutoEventWireup="true"
    CodeFile="Transfer.aspx.cs" Inherits="Inventory_Transfer" Title="Untitled Page" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
   
    <asp:UpdatePanel ID="upallpage" runat="server">
        <Triggers>
            <asp:PostBackTrigger ControlID="btnList" />
            <asp:PostBackTrigger ControlID="btnNew" />
            <asp:PostBackTrigger ControlID="btnBin" />
            <asp:PostBackTrigger ControlID="btnSave" />
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
                                                <asp:Label ID="lblHeader" runat="server" Text="<%$ Resources:Attendance,Transfer %>"
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
                                                    <asp:Button ID="btnTransRequest" runat="server" Text="<%$ Resources:Attendance,Request %>"
                                                        Width="100px" BorderStyle="none" BackColor="Transparent" OnClick="btnTransRequest_Click"
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
                                                                <asp:ListItem Text="<%$ Resources:Attendance, Voucher No. %>" Value="VoucherNo" Selected="True"></asp:ListItem>
                                                                <asp:ListItem Text="<%$ Resources:Attendance, Transfer Date %>" Value="TDate"></asp:ListItem>
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
                                        </asp:Panel>
                                        <asp:GridView ID="GvTransfer" PageSize="<%# SystemParameter.GetPageSize() %>" runat="server"
                                            AutoGenerateColumns="False" Width="100%" AllowPaging="True" AllowSorting="True"
                                            OnPageIndexChanging="GvTransfer_PageIndexChanging" OnSorting="GvTransfer_Sorting"
                                            CssClass="grid">
                                            <Columns>
                                                <asp:TemplateField HeaderText="<%$ Resources:Attendance,Edit %>">
                                                    <ItemTemplate>
                                                        <asp:ImageButton ID="btnEdit" runat="server" CommandArgument='<%# Eval("Trans_Id") %>'
                                                            ImageUrl="~/Images/edit.png" CausesValidation="False" OnCommand="btnEdit_Command"
                                                            ToolTip="<%$ Resources:Attendance,Edit %>" Visible="false" /></ItemTemplate>
                                                    <ItemStyle HorizontalAlign="Center" />
                                                    <ItemStyle CssClass="grid" />
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="<%$ Resources:Attendance,Delete %>">
                                                    <ItemTemplate>
                                                        <asp:ImageButton ID="IbtnDelete" runat="server" CausesValidation="False" CommandArgument='<%# Eval("Trans_Id") %>'
                                                            ImageUrl="~/Images/Erase.png" OnCommand="IbtnDelete_Command" ToolTip="<%$ Resources:Attendance,Delete %>"
                                                            Visible="false" />
                                                    </ItemTemplate>
                                                    <ItemStyle HorizontalAlign="Center" />
                                                    <ItemStyle CssClass="grid" />
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="<%$ Resources:Attendance,Voucher No. %>" SortExpression="VoucherNo">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblVocherNo" runat="server" Text='<%# Eval("VoucherNo") %>'></asp:Label>
                                                        <asp:Label ID="lblTransId" Visible="false" runat="server" Text='<%# Eval("Trans_Id") %>'></asp:Label>
                                                    </ItemTemplate>
                                                    <ItemStyle CssClass="grid" />
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="<%$ Resources:Attendance,Transfer Date %>" SortExpression="TDate">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblTransDate" runat="server" Text='<%# Convert.ToDateTime(Eval("TDate").ToString()).ToString("dd-MMM-yyyy") %>'></asp:Label>
                                                    </ItemTemplate>
                                                    <ItemStyle CssClass="grid" />
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="<%$ Resources:Attendance,To Location %>" SortExpression="Location_Name">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblLocation_Name" runat="server" Text='<%# Eval("Location_Name") %>'></asp:Label>
                                                        <asp:Label ID="lblLocationId" Visible="false" runat="server" Text='<%# Eval("ToLocationID") %>'></asp:Label></ItemTemplate>
                                                    <ItemStyle CssClass="grid" />
                                                </asp:TemplateField>
                                               
                                            </Columns>
                                            <AlternatingRowStyle CssClass="InvgridAltRow"></AlternatingRowStyle>
                                            <HeaderStyle CssClass="Invgridheader" />
                                            <PagerStyle CssClass="Invgridheader" />
                                            <RowStyle CssClass="Invgridrow" HorizontalAlign="Center"></RowStyle>
                                        </asp:GridView>
                                        <asp:HiddenField ID="hdnSort" runat="server" />
                                    </asp:Panel>
                                    <asp:Panel ID="PnlNewEdit" runat="server" DefaultButton="btnSave">
                                        <table width="100%" style="padding-left: 43px">
                                            <tr>
                                                <td align='<%= Common.ChangeTDForDefaultLeft()%>' width="150px">
                                                    <asp:Label ID="lblTransferDate" runat="server" CssClass="labelComman" Text="<%$ Resources:Attendance,Transfer Date %>"></asp:Label>
                                                </td>
                                                <td align="center">
                                                    :
                                                </td>
                                                <td align='<%= Common.ChangeTDForDefaultLeft()%>'>
                                                    <asp:TextBox ID="txtTransferDate" runat="server" CssClass="textComman" 
                                                        AutoPostBack="True" ontextchanged="txtTransferDate_TextChanged" />
                                                    <cc1:CalendarExtender ID="Calender" runat="server" TargetControlID="txtTransferDate"
                                                        Format="dd/MMM/yyyy" />
                                                </td>
                                                <td align='<%= Common.ChangeTDForDefaultLeft()%>' width="150px">
                                                    <asp:Label ID="lblVoucherNo" runat="server" CssClass="labelComman" Text="<%$ Resources:Attendance,Voucher No. %>"></asp:Label>
                                                </td>
                                                <td align="center">
                                                    :
                                                </td>
                                                <td align='<%= Common.ChangeTDForDefaultLeft()%>'>
                                                    <asp:TextBox ID="txtVoucherNo" runat="server" CssClass="textComman" />
                                                </td>
                                            </tr>
                                            <tr>
                                                <td colspan="6" align='<%= Common.ChangeTDForDefaultLeft()%>'>
                                                    <asp:Panel ID="pnlTrans" runat="server" Visible="false">
                                                        <table width="100%">
                                                            <tr>
                                                                <td align='<%= Common.ChangeTDForDefaultLeft()%>' width="145px">
                                                                    <asp:Label ID="lblTransReqDate" runat="server" CssClass="labelComman" Text="<%$ Resources:Attendance,Transfer Request Date %>" />
                                                                </td>
                                                                <td align="center">
                                                                    :
                                                                </td>
                                                                <td align='<%= Common.ChangeTDForDefaultLeft()%>'>
                                                                    <asp:TextBox ID="txtTransReqDate" runat="server" CssClass="textComman" ReadOnly="True" />
                                                                    <cc1:CalendarExtender ID="CalendarExtender1" runat="server" TargetControlID="txtTransReqDate"
                                                                        Format="dd/MMM/yyyy" />
                                                                </td>
                                                                <td align='<%= Common.ChangeTDForDefaultLeft()%>' width="150px">
                                                                    <asp:Label ID="lblTransReqNo" runat="server" CssClass="labelComman" Text="<%$ Resources:Attendance,Transfer Request No.%>" />
                                                                </td>
                                                                <td align="center">
                                                                    :
                                                                </td>
                                                                <td align='<%= Common.ChangeTDForDefaultLeft()%>'>
                                                                    <asp:TextBox ID="txtTransNo" runat="server" CssClass="textComman" ReadOnly="True" />
                                                                </td>
                                                            </tr>
                                                            <tr>
                                                                <td align='<%= Common.ChangeTDForDefaultLeft()%>'>
                                                                    <asp:Label ID="lblLocationName" runat="server" CssClass="labelComman" Text="<%$ Resources:Attendance,To Location %>"></asp:Label>
                                                                </td>
                                                                <td width="1px">
                                                                    :
                                                                </td>
                                                                <td align='<%= Common.ChangeTDForDefaultLeft()%>'>
                                                                    <asp:TextBox ID="txtLocationName" Width="250px" runat="server" CssClass="textComman"></asp:TextBox>
                                                                </td>
                                                                <td align='<%= Common.ChangeTDForDefaultLeft()%>'>
                                                                        </td>
                                                            </tr>
                                                        </table>
                                                    </asp:Panel>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td colspan="6" align="center">
                                                    <asp:Panel ID="pnlProduct" runat="server">
                                                        <asp:GridView ID="GvProduct" runat="server" Width="100%" CssClass="grid" AutoGenerateColumns="False">
                                                            <AlternatingRowStyle CssClass="InvgridAltRow" />
                                                            <Columns>
                                                                <asp:TemplateField HeaderText="<%$ Resources:Attendance,Serial No %>">
                                                                    <ItemTemplate>
                                                                        <asp:Label ID="lblSerialNO" runat="server" Text='<%# Eval("SerialNo") %>'></asp:Label>
                                                                    </ItemTemplate>
                                                                    <ItemStyle CssClass="grid" />
                                                                </asp:TemplateField>
                                                                <asp:TemplateField HeaderText="<%$ Resources:Attendance,Product Name %>">
                                                                    <ItemTemplate>
                                                                        <asp:Label ID="lblProductId" runat="server" Text='<%# ProductName(Eval("ProductId").ToString()) %>'></asp:Label>
                                                                        <asp:Label ID="lblPId" runat="server" Visible="false" Text='<%# Eval("ProductId").ToString() %>'></asp:Label>
                                                                    </ItemTemplate>
                                                                    <ItemStyle CssClass="grid" />
                                                                </asp:TemplateField>
                                                                <asp:TemplateField HeaderText="<%$ Resources:Attendance,Unit Name %>">
                                                                    <ItemTemplate>
                                                                        <asp:Label ID="lblUnit" runat="server" Text='<%# UnitName(Eval("UnitId").ToString()) %>'></asp:Label>
                                                                        <asp:Label ID="lblUnitId" runat="server" Visible="false" Text='<%# Eval("UnitId").ToString() %>'></asp:Label>
                                                                    </ItemTemplate>
                                                                    <ItemStyle CssClass="grid" />
                                                                </asp:TemplateField>
                                                                <asp:TemplateField HeaderText="<%$ Resources:Attendance,Unit Cost %>">
                                                                    <ItemTemplate>
                                                                        <asp:Label ID="lblunitcost" runat="server" Text='<%# Eval("UnitCost") %>'></asp:Label>
                                                                    </ItemTemplate>
                                                                    <ItemStyle CssClass="grid" />
                                                                </asp:TemplateField>
                                                                <asp:TemplateField HeaderText="<%$ Resources:Attendance,Request Quantity %>">
                                                                    <ItemTemplate>
                                                                        <asp:Label ID="lblReqQty" runat="server" Text='<%# Eval("Quantity") %>'></asp:Label>
                                                                    </ItemTemplate>
                                                                    <ItemStyle CssClass="grid" />
                                                                </asp:TemplateField>
                                                                <asp:TemplateField HeaderText="<%$ Resources:Attendance,Out Quantity %>">
                                                                    <ItemTemplate>
                                                                        <asp:TextBox ID="txtOutQty" runat="server" Width="80px" Height="10px"  OnTextChanged="txtOutQty_TextChanged" AutoPostBack="true" CssClass="textCommanSearch"></asp:TextBox>
                                                                        <asp:Label ID="lblTransId" runat="server" Visible="false" Text='<%# Eval("Trans_Id").ToString() %>'></asp:Label>
                                                                    </ItemTemplate>
                                                                    <ItemStyle CssClass="grid" />
                                                                </asp:TemplateField>
                                                            </Columns>
                                                            <HeaderStyle CssClass="Invgridheader" />
                                                            <PagerStyle CssClass="Invgridheader" />
                                                            <RowStyle CssClass="Invgridrow" HorizontalAlign="Center" />
                                                        </asp:GridView>
                                                        <asp:GridView ID="gvEditProduct" runat="server" Width="100%" CssClass="grid" AutoGenerateColumns="False">
                                                            <AlternatingRowStyle CssClass="InvgridAltRow" />
                                                            <Columns>
                                                                <asp:TemplateField HeaderText="<%$ Resources:Attendance,Serial No %>">
                                                                    <ItemTemplate>
                                                                        <asp:Label ID="lblSerialNO" runat="server" Text='<%# Eval("Serial_No") %>'></asp:Label>
                                                                    </ItemTemplate>
                                                                    <ItemStyle CssClass="grid" />
                                                                </asp:TemplateField>
                                                                <asp:TemplateField HeaderText="<%$ Resources:Attendance,Product Name %>">
                                                                    <ItemTemplate>
                                                                        <asp:Label ID="lblProductId" runat="server" Text='<%# ProductName(Eval("ProductId").ToString()) %>'></asp:Label>
                                                                        <asp:Label ID="lblPId" runat="server" Visible="false" Text='<%# Eval("ProductId").ToString() %>'></asp:Label>
                                                                    </ItemTemplate>
                                                                    <ItemStyle CssClass="grid" />
                                                                </asp:TemplateField>
                                                                <asp:TemplateField HeaderText="<%$ Resources:Attendance,Unit Name %>">
                                                                    <ItemTemplate>
                                                                        <asp:Label ID="lblUnit" runat="server" Text='<%# UnitName(Eval("Unit_Id").ToString()) %>'></asp:Label>
                                                                        <asp:Label ID="lblUnitId" runat="server" Visible="false" Text='<%# Eval("Unit_Id").ToString() %>'></asp:Label>
                                                                    </ItemTemplate>
                                                                    <ItemStyle CssClass="grid" />
                                                                </asp:TemplateField>
                                                                <asp:TemplateField HeaderText="<%$ Resources:Attendance,Unit Cost %>">
                                                                    <ItemTemplate>
                                                                        <asp:Label ID="lblunitcost" runat="server" Text='<%# Eval("UnitCost") %>'></asp:Label>
                                                                    </ItemTemplate>
                                                                    <ItemStyle CssClass="grid" />
                                                                </asp:TemplateField>
                                                                <asp:TemplateField HeaderText="<%$ Resources:Attendance,Request Quantity %>">
                                                                    <ItemTemplate>
                                                                        <asp:Label ID="lblReqQty" runat="server" Text='<%# Eval("RequestQty") %>'></asp:Label>
                                                                    </ItemTemplate>
                                                                    <ItemStyle CssClass="grid" />
                                                                </asp:TemplateField>
                                                                <asp:TemplateField HeaderText="<%$ Resources:Attendance,Out Quantity %>">
                                                                    <ItemTemplate>
                                                                        <asp:TextBox ID="txtOutQty" runat="server" AutoPostBack="true" Width="80px" Height="10px" CssClass="textCommanSearch"
                                                                            Text='<%# Eval("OutQty") %>' OnTextChanged="txtOutQty_TextChanged"></asp:TextBox>
                                                                    </ItemTemplate>
                                                                    <ItemStyle CssClass="grid" />
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
                                                                <asp:Button ID="btnSave" runat="server" Text="<%$ Resources:Attendance,Save %>" CssClass="buttonCommman"
                                                                    OnClick="btnSave_Click" />
                                                            </td>
                                                            <td width="90px">
                                                                <asp:Button ID="BtnReset" runat="server" Text="<%$ Resources:Attendance,Reset %>"
                                                                    CssClass="buttonCommman" CausesValidation="False" OnClick="BtnReset_Click" />
                                                            </td>
                                                            <td width="90px">
                                                                <asp:Button ID="btnPICancel" runat="server" CssClass="buttonCommman" Text="<%$ Resources:Attendance,Cancel %>"
                                                                    CausesValidation="False" OnClick="btnCancel_Click" />
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
                                                            <asp:ListItem Text="<%$ Resources:Attendance, Voucher No. %>" Value="VoucherNo" Selected="True"></asp:ListItem>
                                                            <asp:ListItem Text="<%$ Resources:Attendance, Transfer Date %>" Value="TDate"></asp:ListItem>
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
                                        <asp:GridView ID="gvTransferBin" PageSize="<%# SystemParameter.GetPageSize() %>"
                                            runat="server" AutoGenerateColumns="False" Width="100%" AllowPaging="True" AllowSorting="True"
                                            CssClass="grid" OnPageIndexChanging="gvTransferBin_PageIndexChanging" OnSorting="gvTransferBin_Sorting">
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
                                                <asp:TemplateField HeaderText="<%$ Resources:Attendance,Voucher No. %>" SortExpression="VoucherNo">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblVocherNo" runat="server" Text='<%# Eval("VoucherNo") %>'></asp:Label>
                                                        <asp:Label ID="lblTransId" Visible="false" runat="server" Text='<%# Eval("Trans_Id") %>'></asp:Label>
                                                    </ItemTemplate>
                                                    <ItemStyle CssClass="grid" />
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="<%$ Resources:Attendance,Transfer Date %>" SortExpression="TDate">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblTransDate" runat="server" Text='<%# Convert.ToDateTime(Eval("TDate").ToString()).ToString("dd-MMM-yyyy") %>'></asp:Label>
                                                    </ItemTemplate>
                                                    <ItemStyle CssClass="grid" />
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="<%$ Resources:Attendance,To Location %>" SortExpression="Location_Name">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblLocation_Name" runat="server" Text='<%# Eval("Location_Name") %>'></asp:Label>
                                                        <asp:Label ID="lblLocationId" Visible="false" runat="server" Text='<%# Eval("ToLocationID") %>'></asp:Label></ItemTemplate>
                                                    <ItemStyle CssClass="grid" />
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="<%$ Resources:Attendance,Post %>">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblPost" runat="server" Text='<%# GetPost(Eval("Post").ToString()) %>'></asp:Label>
                                                    </ItemTemplate>
                                                    <ItemStyle CssClass="grid" />
                                                </asp:TemplateField>
                                            </Columns>
                                            <AlternatingRowStyle CssClass="InvgridAltRow"></AlternatingRowStyle>
                                            <HeaderStyle CssClass="Invgridheader" />
                                            <PagerStyle CssClass="Invgridheader" />
                                            <RowStyle CssClass="Invgridrow" HorizontalAlign="Center"></RowStyle>
                                        </asp:GridView>
                                        <asp:Label ID="lblSelectedRecord" runat="server" Visible="false"></asp:Label>
                                        <asp:HiddenField ID="HDFSortbin" runat="server" />
                                    </asp:Panel>
                                    <asp:Panel ID="PnlTransferRequest" runat="server">
                                        <asp:HiddenField ID="hdnTransferRequest" runat="server" />
                                        <asp:GridView ID="gvTransferRequest" PageSize="<%# SystemParameter.GetPageSize() %>"
                                            runat="server" AutoGenerateColumns="False" Width="100%" AllowPaging="True" AllowSorting="True"
                                            CssClass="grid" OnPageIndexChanging="gvTransferRequest_PageIndexChanging" OnSorting="gvTransferRequest_Sorting">
                                            <Columns>
                                                <asp:TemplateField>
                                                    <ItemTemplate>
                                                        <asp:Button ID="btnTransferRequest" runat="server" BorderStyle="None" BackColor="Transparent"
                                                            CssClass="btnPull" CommandArgument='<%# Eval("Trans_Id") %>' OnCommand="btnTransferRequest_Command" />
                                                    </ItemTemplate>
                                                    <ItemStyle HorizontalAlign="Center" />
                                                    <ItemStyle CssClass="grid" />
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="<%$ Resources:Attendance,Request No %>" SortExpression="RequestNo">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblRequestNo" runat="server" Text='<%# Eval("RequestNo") %>'></asp:Label>
                                                        <asp:Label ID="lblReqId" Visible="false" runat="server" Text='<%# Eval("Trans_Id") %>'></asp:Label>
                                                    </ItemTemplate>
                                                    <ItemStyle CssClass="grid" />
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="<%$ Resources:Attendance,Transfer Request Date %>"
                                                    SortExpression="TDate">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblRequestDate" runat="server" Text='<%# Convert.ToDateTime(Eval("TDate").ToString()).ToString("dd-MMM-yyyy") %>'></asp:Label>
                                                    </ItemTemplate>
                                                    <ItemStyle CssClass="grid" />
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="<%$ Resources:Attendance,From Location %>" SortExpression="Location_Name">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblExpDelDate" runat="server" Text='<%# Eval("Location_Name") %>'></asp:Label>
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
