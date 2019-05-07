<%@ Page Language="C#" MasterPageFile="~/ERPMaster.master" AutoEventWireup="true"
    CodeFile="PurchaseRequest.aspx.cs" Inherits="Purchase_PurchaseRequest" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <asp:UpdatePanel ID="UpdatePanel1" runat="server">
        <Triggers>
            <asp:PostBackTrigger ControlID="btnList" />
            <asp:PostBackTrigger ControlID="btnNew" />
            <asp:PostBackTrigger ControlID="btnBin" />
           <%-- <asp:PostBackTrigger ControlID="gvPurchaseRequest" />--%>
            <asp:PostBackTrigger ControlID="BtnSave" />
           
  
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
                                                <asp:Label ID="lblHeader" runat="server" Text="<%$ Resources:Attendance,Purchase Request %>"
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
                                                                <asp:ListItem Selected="True" Text="<%$ Resources:Attendance,Request No %>" Value="RequestNo"></asp:ListItem>
                                                                <asp:ListItem Text="<%$ Resources:Attendance,Request Date %>" Value="RequestDate"></asp:ListItem>
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
                                        </asp:Panel>
                                        <asp:GridView ID="gvPurchaseRequest" PageSize="<%# SystemParameter.GetPageSize() %>"
                                            runat="server" AutoGenerateColumns="False" Width="100%" AllowPaging="True" AllowSorting="True"
                                            CssClass="grid" OnPageIndexChanging="gvPurchaseRequest_PageIndexChanging" OnSorting="gvPurchaseRequest_Sorting">
                                            <Columns>
                                                <asp:TemplateField HeaderText="<%$ Resources:Attendance,Print %>">
                                                    <ItemTemplate>
                                                        <asp:ImageButton ID="IbtnPrint" runat="server" CausesValidation="False" CommandArgument='<%# Eval("RequestNo") %>'
                                                            ImageUrl="~/Images/print.png" OnCommand="IbtnPrint_Command" ToolTip="<%$ Resources:Attendance,Print %>"
                                                            Width="16px" Visible="false" />
                                                    </ItemTemplate>
                                                    <ItemStyle CssClass="grid" HorizontalAlign="Center" />
                                                </asp:TemplateField>
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
                                                <asp:TemplateField HeaderText="<%$ Resources:Attendance,Request No %>" SortExpression="RequestNo">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblRequestNo" runat="server" Text='<%# Eval("RequestNo") %>'></asp:Label>
                                                    </ItemTemplate>
                                                    <ItemStyle CssClass="grid" />
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="<%$ Resources:Attendance,Request Date %>" SortExpression="RequestDate">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblRequestDate" runat="server" Text='<%# Convert.ToDateTime(Eval("RequestDate").ToString()).ToString("dd-MMM-yyyy") %>'></asp:Label>
                                                    </ItemTemplate>
                                                    <ItemStyle CssClass="grid" />
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="<%$ Resources:Attendance,Expected Delivery Date %>"
                                                    SortExpression="ExpDelDate">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblExpDelDate" runat="server" Text='<%# Convert.ToDateTime(Eval("ExpDelDate").ToString()).ToString("dd-MMM-yyyy") %>'></asp:Label>
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
                                                <td align='<%= Common.ChangeTDForDefaultLeft()%>'>
                                                    <asp:Label ID="lblRequestdate" runat="server" CssClass="labelComman" Text="<%$ Resources:Attendance,Request Date %>"></asp:Label>
                                                </td>
                                                <td width="1px">
                                                    :
                                                </td>
                                                <td align='<%= Common.ChangeTDForDefaultLeft()%>'>
                                                    <asp:TextBox ID="txtRequestdate" runat="server" CssClass="textComman" />
                                                    <cc1:CalendarExtender ID="txtCalenderExtender" runat="server" TargetControlID="txtRequestdate"
                                                        Format="dd-MMM-yyyy">
                                                    </cc1:CalendarExtender>
                                                </td>
                                                <td align='<%= Common.ChangeTDForDefaultLeft()%>'>
                                                    <asp:Label ID="lblRequestNo" runat="server" Text="<%$ Resources:Attendance,Request No %>"
                                                        CssClass="labelComman"></asp:Label>
                                                </td>
                                                <td>
                                                    :
                                                </td>
                                                <td align='<%= Common.ChangeTDForDefaultLeft()%>'>
                                                    <asp:TextBox ID="txtlRequestNo" runat="server" CssClass="textComman" AutoPostBack="True"
                                                        OnTextChanged="txtlRequestNo_TextChanged" /></asp:TextBox>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td align='<%= Common.ChangeTDForDefaultLeft()%>'>
                                                    <asp:Label ID="lblExpDelDate" runat="server" CssClass="labelComman" Text="<%$ Resources:Attendance,Expected Delivery Date %>"></asp:Label>
                                                </td>
                                                <td width="1px">
                                                    :
                                                </td>
                                                <td align='<%= Common.ChangeTDForDefaultLeft()%>'>
                                                    <asp:TextBox ID="txtExpDelDate" runat="server" CssClass="textComman" />
                                                    <cc1:CalendarExtender ID="CalendarExtender1" runat="server" TargetControlID="txtExpDelDate"
                                                        Format="dd-MMM-yyyy">
                                                    </cc1:CalendarExtender>
                                                </td>
                                                <td colspan="2">
                                                </td>
                                                <td align='<%= Common.ChangeTDForDefaultLeft()%>'>
                                                    <asp:Panel ID="Panel1" runat="server" DefaultButton="btnAddProduct">
                                                        <asp:Button ID="btnAddProduct" runat="server" Text="<%$ Resources:Attendance,Add Product %>"
                                                            BackColor="Transparent" BorderStyle="None" Font-Bold="true" CssClass="btnLocation"
                                                            Font-Size="13px" Font-Names=" Arial" Width="172px" Height="32px" OnClick="btnAddProduct_Click" />
                                                    </asp:Panel>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td colspan="6">
                                                    <asp:GridView ID="gvProductRequest" PageSize="<%# SystemParameter.GetPageSize() %>"
                                                        runat="server" AutoGenerateColumns="False" Width="100%" AllowPaging="True" AllowSorting="True"
                                                        CssClass="grid">
                                                        <Columns>
                                                            <asp:TemplateField HeaderText="<%$ Resources:Attendance,Edit %>">
                                                                <ItemTemplate>
                                                                    <asp:ImageButton ID="btnEdit" runat="server" CommandArgument='<%# Eval("Trans_Id") %>'
                                                                        ImageUrl="~/Images/edit.png" CausesValidation="False" ToolTip="<%$ Resources:Attendance,Edit %>"
                                                                        OnCommand="btnEdit_Command1" Visible="false" /></ItemTemplate>
                                                                <ItemStyle HorizontalAlign="Center" />
                                                                <ItemStyle CssClass="grid" />
                                                            </asp:TemplateField>
                                                            <asp:TemplateField HeaderText="<%$ Resources:Attendance,Delete %>">
                                                                <ItemTemplate>
                                                                    <asp:ImageButton ID="IbtnDelete" runat="server" CausesValidation="False" CommandArgument='<%# Eval("Trans_Id") %>'
                                                                        ImageUrl="~/Images/Erase.png" ToolTip="<%$ Resources:Attendance,Delete %>" Style="height: 14px"
                                                                        OnCommand="IbtnDelete_Command1" Visible="false" />
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
                                                            <asp:TemplateField HeaderText="<%$ Resources:Attendance,Product Name %>">
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
                                                            <asp:TemplateField HeaderText="<%$ Resources:Attendance,Quantity %>">
                                                                <ItemTemplate>
                                                                    <asp:Label ID="lblReqQty" runat="server" Text='<%# Eval("ReqQty") %>'></asp:Label>
                                                                </ItemTemplate>
                                                                <ItemStyle CssClass="grid" />
                                                            </asp:TemplateField>
                                                            <asp:TemplateField HeaderText="<%$ Resources:Attendance,Description %>" ItemStyle-Width="50%">
                                                                <ItemTemplate>
                                                                    <asp:Label ID="lblDescription" runat="server" Text='<%# Eval("ProductDescription") %>'></asp:Label>
                                                                </ItemTemplate>
                                                                <ItemStyle CssClass="grid" />
                                                            </asp:TemplateField>
                                                        </Columns>
                                                        <AlternatingRowStyle CssClass="InvgridAltRow"></AlternatingRowStyle>
                                                        <HeaderStyle CssClass="Invgridheader" />
                                                        <PagerStyle CssClass="Invgridheader" />
                                                        <RowStyle CssClass="Invgridrow" HorizontalAlign="Center"></RowStyle>
                                                    </asp:GridView>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td align='<%= Common.ChangeTDForDefaultLeft()%>'>
                                                    <asp:Label ID="lblTermCondition" runat="server" Text="<%$ Resources:Attendance,Description %>"
                                                        CssClass="labelComman"></asp:Label>
                                                </td>
                                                <td>
                                                    :
                                                </td>
                                                <td colspan="4" align='<%= Common.ChangeTDForDefaultLeft()%>'>
                                                    <asp:TextBox ID="txtTermCondition" runat="server" CssClass="textComman" Width="90%"
                                                        Height="80px" TextMode="MultiLine" /></asp:TextBox>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td>
                                                </td>
                                                <td>
                                                </td>
                                                <td align="center" colspan="3">
                                                    <table>
                                                        <tr>
                                                            <td width="90px">
                                                                <asp:Button ID="btnSave" runat="server" Text="<%$ Resources:Attendance,Save %>" CssClass="buttonCommman"
                                                                    OnClick="btnSave_Click" ValidationGroup="a" Visible="false" />
                                                            </td>
                                                            <td width="90px">
                                                                <asp:Panel ID="pnlReset" runat="server" DefaultButton="btnReset">
                                                                    <asp:Button ID="btnReset" runat="server" Text="<%$ Resources:Attendance,Reset %>"
                                                                        CssClass="buttonCommman" OnClick="btnReset_Click" CausesValidation="False" />
                                                                </asp:Panel>
                                                            </td>
                                                            <td width="90px">
                                                                <asp:Panel ID="pnlCancel" runat="server" DefaultButton="btnCancel">
                                                                    <asp:Button ID="btnCancel" runat="server" Text="<%$ Resources:Attendance,Cancel %>"
                                                                        CssClass="buttonCommman" OnClick="btnCancel_Click" CausesValidation="False" />
                                                                </asp:Panel>
                                                            </td>
                                                            <td width="130px">
                                                                <asp:Panel ID="pnlSavePrint" runat="server" DefaultButton="btnSavePrint">
                                                                    <asp:Button ID="btnSavePrint" runat="server" Text="<%$ Resources:Attendance,Save & Print %>"
                                                                        CssClass="buttonCommman" CausesValidation="False" OnClick="btnSavePrint_Click"
                                                                        Width="120px" />
                                                                </asp:Panel>
                                                            </td>
                                                        </tr>
                                                    </table>
                                                    <asp:HiddenField ID="editid" runat="server" />
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
                                                                <asp:ListItem Selected="True" Text="<%$ Resources:Attendance,Request No %>" Value="RequestNo"></asp:ListItem>
                                                                <asp:ListItem Text="<%$ Resources:Attendance,Request Date %>" Value="RequestDate"></asp:ListItem>
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
                                        <asp:GridView ID="gvBinPurchaseRequest" PageSize="<%# SystemParameter.GetPageSize() %>"
                                            runat="server" AutoGenerateColumns="False" Width="100%" AllowPaging="True" AllowSorting="True"
                                            CssClass="grid" OnPageIndexChanging="gvBinPurchaseRequest_PageIndexChanging"
                                            OnSorting="gvBinPurchaseRequest_Sorting">
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
                                                <asp:TemplateField HeaderText="<%$ Resources:Attendance,Request No %>" SortExpression="RequestNo">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblRequestNo" runat="server" Text='<%# Eval("RequestNo") %>'></asp:Label>
                                                        <asp:Label ID="lblReqId" Visible="false" runat="server" Text='<%# Eval("Trans_Id") %>'></asp:Label>
                                                    </ItemTemplate>
                                                    <ItemStyle CssClass="grid" />
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="<%$ Resources:Attendance,Request Date %>" SortExpression="RequestDate">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblRequestDate" runat="server" Text='<%# Convert.ToDateTime(Eval("RequestDate").ToString()).ToString("dd-MMM-yyyy") %>'></asp:Label>
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
                                    <asp:Panel ID="Panel5" runat="server" DefaultButton="btnClosePanel">
                                        <asp:ImageButton ID="btnClosePanel" runat="server" ImageUrl="~/Images/close.png"
                                            CausesValidation="False" OnClick="btnClosePanel_Click" Height="20px" Width="20px" />
                                    </asp:Panel>
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
                                    <asp:TextBox ID="txtProductName" runat="server" CssClass="textComman" AutoPostBack="True"
                                        OnTextChanged="txtProductName_TextChanged" BackColor="#c3c3c3" Width="615px" />
                                    <cc1:AutoCompleteExtender ID="AutoCompleteExtender1" runat="server" CompletionInterval="100"
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
                                    <asp:DropDownList ID="ddlUnit" runat="server" CssClass="textComman" AutoPostBack="True" />
                                    <asp:TextBox ID="txtUnit" runat="server" CssClass="textComman" Visible="False" />
                                </td>
                                <td align='<%= Common.ChangeTDForDefaultLeft()%>'>
                                    <asp:Label ID="lblRequestQty" runat="server" CssClass="labelComman" Text="<%$ Resources:Attendance,Request Quantity %>" />
                                </td>
                                <td align="center">
                                    :
                                </td>
                                <td align='<%= Common.ChangeTDForDefaultLeft()%>'>
                                    <asp:TextBox ID="txtRequestQty" runat="server" CssClass="textComman" Text="1" />
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
                                                <asp:Panel ID="Panel4" runat="server" DefaultButton="btnProductSave">
                                                    <asp:Button ID="btnProductSave" runat="server" Text="<%$ Resources:Attendance,Save %>"
                                                        CssClass="buttonCommman" Visible="false" OnClick="btnProductSave_Click" />
                                                </asp:Panel>
                                            </td>
                                            <td width="90px">
                                                <asp:Panel ID="Panel3" runat="server" DefaultButton="btnProductCancel">
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
