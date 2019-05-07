<%@ Page Language="C#" MasterPageFile="~/ERPMaster.master" AutoEventWireup="true"
    CodeFile="UnitMaster.aspx.cs" Inherits="Inventory_UnitMaster" Title="PegasusInventory" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    
    <asp:UpdatePanel ID="UpdatePanel1" runat="server">
        <Triggers>
          <asp:PostBackTrigger ControlID="btnList" />
            <asp:PostBackTrigger ControlID="btnNew" />
            <asp:PostBackTrigger ControlID="btnBin" />
           <%--<asp:PostBackTrigger ControlID="gvunitMaster" />
            <asp:PostBackTrigger ControlID="BtnSave" />
            <%--<asp:PostBackTrigger ControlID="gvUnitMasterBin" />--%>
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
                                                <asp:Label ID="lblHeader" runat="server" Text="<%$ Resources:Attendance,Unit Setup %>"
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
                                                        padding-top: 3px; background-image: url(  '../Images/New.png' ); background-repeat: no-repeat;
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
                                                                <asp:ListItem Selected="True" Text="<%$ Resources:Attendance,Unit Name %>" Value="Unit_Name"></asp:ListItem>
                                                                <asp:ListItem Text="<%$ Resources:Attendance,Unit Name(Local) %>" Value="Unit_Name_L"></asp:ListItem>
                                                                <asp:ListItem Text="<%$ Resources:Attendance,Conversion Quantity %>" Value="Coversion_Qty"></asp:ListItem>
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
                                        <asp:GridView ID="gvunitMaster" PageSize="<%# SystemParameter.GetPageSize() %>" runat="server"
                                            AutoGenerateColumns="False" Width="100%" AllowPaging="True" AllowSorting="True"
                                            CssClass="grid" OnPageIndexChanging="gvunitMaster_PageIndexChanging" OnSorting="gvUnitMaster_OnSorting">
                                            <Columns>
                                                <asp:TemplateField HeaderText="<%$ Resources:Attendance,Edit %>">
                                                    <ItemTemplate>
                                                        <asp:ImageButton ID="btnEdit" runat="server" CommandArgument='<%# Eval("Unit_Id") %>'
                                                            ImageUrl="~/Images/edit.png" CausesValidation="False" OnCommand="btnEdit_Command"
                                                            ToolTip="<%$ Resources:Attendance,Edit %>" Visible="false" /></ItemTemplate>
                                                    <ItemStyle HorizontalAlign="Center" />
                                                    <ItemStyle CssClass="grid" />
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="<%$ Resources:Attendance,Delete %>">
                                                    <ItemTemplate>
                                                        <asp:ImageButton ID="IbtnDelete" runat="server" CausesValidation="False" CommandArgument='<%# Eval("Unit_Id") %>'
                                                            ImageUrl="~/Images/Erase.png" OnCommand="IbtnDelete_Command" Visible="false"
                                                            ToolTip="<%$ Resources:Attendance,Delete %>" />
                                                    </ItemTemplate>
                                                    <ItemStyle HorizontalAlign="Center" />
                                                    <ItemStyle CssClass="grid" />
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="<%$ Resources:Attendance,Unit Name %>" SortExpression="Unit_Name">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lbEUnitName" runat="server" Text='<%# Eval("Unit_Name") %>'></asp:Label>
                                                    </ItemTemplate>
                                                    <ItemStyle CssClass="grid" />
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="<%$ Resources:Attendance,Unit Name(Local) %>" SortExpression="Unit_Name_L">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblUnitName" runat="server" Text='<%# Eval("Unit_Name_L") %>'></asp:Label>
                                                    </ItemTemplate>
                                                    <ItemStyle CssClass="grid" />
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="<%$ Resources:Attendance,Conversion Unit %>" SortExpression="Conversion_Unit">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblConversion_Unit" runat="server" Text='<%# GetUnitName(Eval("Conversion_Unit").ToString()) %>'></asp:Label>
                                                    </ItemTemplate>
                                                    <ItemStyle CssClass="grid" />
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="<%$ Resources:Attendance,Conversion Quantity %>" SortExpression="Coversion_Qty">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblConversion_Qty" runat="server" Text='<%# Eval("Coversion_Qty") %>'></asp:Label>
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
                                                    <asp:Label ID="lblEUnitName" runat="server" CssClass="labelComman" Text="<%$ Resources:Attendance,Unit Name %>"></asp:Label>
                                                </td>
                                                <td width="1px">
                                                    :
                                                </td>
                                                <td align='<%= Common.ChangeTDForDefaultLeft()%>'>
                                                    <asp:TextBox ID="txtEUnitName" runat="server" CssClass="textComman" BackColor="#e3e3e3"
                                                        AutoPostBack="True" OnTextChanged="txtEUnitName_TextChanged" />
                                                    <cc1:AutoCompleteExtender ID="txtEUnitName_AutoCompleteExtender" runat="server" DelimiterCharacters=""
                                                        Enabled="True" ServiceMethod="GetCompletionList" ServicePath="" CompletionInterval="100"
                                                        MinimumPrefixLength="1" CompletionSetCount="1" TargetControlID="txtEUnitName"
                                                        UseContextKey="True" CompletionListCssClass="completionList" CompletionListItemCssClass="listItem"
                                                        CompletionListHighlightedItemCssClass="itemHighlighted">
                                                    </cc1:AutoCompleteExtender>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td align='<%= Common.ChangeTDForDefaultLeft()%>'>
                                                    <asp:Label ID="lblLUnitName" runat="server" Text="<%$ Resources:Attendance,Unit Name(Local) %>"
                                                        CssClass="labelComman"></asp:Label>
                                                </td>
                                                <td>
                                                    :
                                                </td>
                                                <td align='<%= Common.ChangeTDForDefaultLeft()%>'>
                                                    <asp:TextBox ID="txtlUnitName" runat="server" CssClass="textComman" /></asp:TextBox>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td align='<%= Common.ChangeTDForDefaultLeft()%>'>
                                                    <asp:Label ID="lblConversion_Unit" runat="server" Text="<%$ Resources:Attendance,Conversion Unit %>"
                                                        CssClass="labelComman"></asp:Label>
                                                </td>
                                                <td>
                                                    :
                                                </td>
                                                <td align='<%= Common.ChangeTDForDefaultLeft()%>'>
                                                    <asp:DropDownList ID="ddlConversion_Unit" runat="server" Width="262px" CssClass="textComman" />
                                                </td>
                                            </tr>
                                            <tr>
                                                <td align='<%= Common.ChangeTDForDefaultLeft()%>'>
                                                    <asp:Label ID="lblConversion_Qty" runat="server" Text="<%$ Resources:Attendance,Conversion Quantity %>"
                                                        CssClass="labelComman"></asp:Label>
                                                </td>
                                                <td>
                                                    :
                                                </td>
                                                <td align='<%= Common.ChangeTDForDefaultLeft()%>'>
                                                    <asp:TextBox ID="txtConversion_Qty" runat="server" CssClass="textComman" />
                                                    <cc1:FilteredTextBoxExtender ID="txtConversion_Qty_FilteredTextBoxExtender" runat="server"
                                                        Enabled="True" TargetControlID="txtConversion_Qty" ValidChars="1,2,3,4,5,6,7,8,9,0,.">
                                                    </cc1:FilteredTextBoxExtender>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td>
                                                </td>
                                                <td>
                                                </td>
                                                <td align='<%= Common.ChangeTDForDefaultLeft()%>'>
                                                    <table style="padding-left: 8px">
                                                        <tr>
                                                            <td width="80px">
                                                                <asp:Button ID="btnSave" runat="server" Text="<%$ Resources:Attendance,Save %>" CssClass="buttonCommman"
                                                                    OnClick="btnSave_Click" Visible="false" ValidationGroup="a" />
                                                            </td>
                                                            <td width="80px">
                                                                <asp:Panel ID="pnlReset" runat="server" DefaultButton="btnReset">
                                                                    <asp:Button ID="btnReset" runat="server" Text="<%$ Resources:Attendance,Reset %>"
                                                                        CssClass="buttonCommman" OnClick="btnReset_Click" CausesValidation="False" />
                                                                </asp:Panel>
                                                            </td>
                                                            <td>
                                                                <asp:Panel ID="pnlCancel" runat="server" DefaultButton="btnCancel">
                                                                    <asp:Button ID="btnCancel" runat="server" Text="<%$ Resources:Attendance,Cancel %>"
                                                                        CssClass="buttonCommman" OnClick="btnCancel_Click" CausesValidation="False" />
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
                                                                <asp:ListItem Selected="True" Text="<%$ Resources:Attendance,Unit Name %>" Value="Unit_Name"></asp:ListItem>
                                                                <asp:ListItem Text="<%$ Resources:Attendance,Unit Name(Local) %>" Value="Unit_Name_L"></asp:ListItem>
                                                                <asp:ListItem Text="<%$ Resources:Attendance,Conversion Quantity %>" Value="Coversion_Qty"></asp:ListItem>
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
                                        <asp:GridView ID="gvUnitMasterBin" PageSize="<%# SystemParameter.GetPageSize() %>"
                                            runat="server" AutoGenerateColumns="False" Width="100%" AllowPaging="True" OnPageIndexChanging="gvUnitMasterBin_PageIndexChanging"
                                            OnSorting="gvUnitMasterBin_OnSorting" AllowSorting="true" CssClass="grid">
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
                                                <asp:TemplateField HeaderText="<%$ Resources:Attendance,Unit Name %>" SortExpression="Unit_Name">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lbEUnitName" runat="server" Text='<%# Eval("Unit_Name") %>'></asp:Label>
                                                        <asp:Label ID="lblUnitId" runat="server" Text='<%# Eval("Unit_Id") %>'></asp:Label>
                                                    </ItemTemplate>
                                                    <ItemStyle CssClass="grid" />
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="<%$ Resources:Attendance,Unit Name(Local) %>" SortExpression="Unit_Name_L">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblUnitName" runat="server" Text='<%# Eval("Unit_Name_L") %>'></asp:Label>
                                                    </ItemTemplate>
                                                    <ItemStyle CssClass="grid" />
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="<%$ Resources:Attendance,Conversion Unit %>" SortExpression="Conversion_Unit">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblConversion_Unit" runat="server" Text='<%# GetUnitName(Eval("Conversion_Unit").ToString()) %>'></asp:Label>
                                                    </ItemTemplate>
                                                    <ItemStyle CssClass="grid" />
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="<%$ Resources:Attendance,Conversion Quantity %>" SortExpression="Coversion_Qty">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblConversion_Qty" runat="server" Text='<%# Eval("Coversion_Qty") %>'></asp:Label>
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
