﻿<%@ Page Language="C#" MasterPageFile="~/ERPMaster.master" AutoEventWireup="true"
    CodeFile="GroupMaster.aspx.cs" Inherits="EMS_GroupMaster" Title="PegasusInventory" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <link href="../App_Themes/AJAX.css" rel="stylesheet" type="text/css" />
    <style>
        .Overlay
        {
            position: fixed;
            top: 0px;
            bottom: 0px;
            left: 0px;
            right: 0px;
            overflow: hidden;
            padding: 0;
            margin: 0;
            background-color: #c2c2c2;
            z-index: 1000;
            height: 100%;
        }
        .PopUpPanel
        {
            position: absolute;
            background-color: #ffffff;
            top: 15%;
            left: 35%;
            z-index: 2001;
            -moz-box-shadow: -0.5px 0px 9px #000000;
            -webkit-box-shadow: -0.5px 0px 9px #000000;
            box-shadow: 3.5px 4px 5px #000000;
            border-radius: 5px;
            -moz-border-radiux: 5px;
            -webkit-border-radiux: 5px;
            border: solid 1px #939191;
        }
    </style>
    <asp:UpdateProgress ID="UpdateProgress1" runat="server" AssociatedUpdatePanelID="UpdatePanel1">
        <ProgressTemplate>
            <div id="progressBackgroundFilter">
            </div>
            <div id="processMessage" class="progressBackgroundFilter">
                Loading…<br />
                <br />
                <img alt="Loading" src="../Images/ajax-loader2.gif" class="processMessage" />
            </div>
        </ProgressTemplate>
    </asp:UpdateProgress>
    <asp:UpdatePanel ID="UpdatePanel1" runat="server">
        <Triggers>
            <asp:PostBackTrigger ControlID="btnList" />
            <asp:PostBackTrigger ControlID="btnNew" />
            <asp:PostBackTrigger ControlID="btnBin" />
            <asp:PostBackTrigger ControlID="GvGroup" />
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
                                                <asp:Label ID="lblHeader" runat="server" Text="<%$ Resources:Attendance,Contact Group Setup %>"
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
                                                            <asp:Label ID="Label6" runat="server" Text="<%$ Resources:Attendance,Select Field %>"
                                                                CssClass="labelComman"></asp:Label>
                                                        </td>
                                                        <td width="180px">
                                                            <asp:DropDownList ID="ddlFieldName" runat="server" CssClass="DropdownSearch" Height="25px"
                                                                Width="170px">
                                                                <asp:ListItem Text="<%$ Resources:Attendance,Group Id %>" Value="Group_Id"></asp:ListItem>
                                                                <asp:ListItem Text="<%$ Resources:Attendance,Group Name %>" Value="Group_Name" Selected="True"></asp:ListItem>
                                                                <asp:ListItem Text="<%$ Resources:Attendance,Parent Group %>" Value="ParentGroupName"></asp:ListItem>
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
                                                            <asp:TextBox ID="txtValue" runat="server"  CssClass="textCommanSearch"
                                                                Height="14px" Width="250px"></asp:TextBox>
                                                        </td>
                                                        <td width="50px" align="center">
                                                            <asp:ImageButton ID="btnbind" runat="server" CausesValidation="False" Height="25px"
                                                                ImageUrl="~/Images/search.png" OnClick="btnbind_Click" Width="25px" ToolTip="<%$ Resources:Attendance,Search %>" />
                                                        </td>
                                                        <td>
                                                            <asp:Panel ID="PnlRefresh" runat="server" DefaultButton="btnRefresh">
                                                                <asp:ImageButton ID="btnRefresh" runat="server" CausesValidation="False" Height="25px"
                                                                    ImageUrl="~/Images/refresh.png" OnClick="btnRefreshReport_Click" Width="25px"
                                                                    ToolTip="<%$ Resources:Attendance,Refresh %>" />
                                                            </asp:Panel>
                                                        </td>
                                                        <td>
                                                            <asp:ImageButton ID="btnGridView" runat="server" CausesValidation="False" Height="25px"
                                                                Visible="true" ImageUrl="~/Images/NewTree.png" OnClick="btnGridView_Click" Width="25px"
                                                                ToolTip="<%$ Resources:Attendance, Tree View %>"></asp:ImageButton>
                                                        </td>
                                                        <td>
                                                            <asp:ImageButton ID="btnTreeView" runat="server" CausesValidation="False" Height="25px"
                                                                ImageUrl="~/Images/NewTree.png" OnClick="btnTreeView_Click" Width="25px" Visible="false">
                                                            </asp:ImageButton>
                                                        </td>
                                                        <td align="center">
                                                            <asp:Label ID="lblTotalRecords" runat="server" Text="<%$ Resources:Attendance,Total Records: 0 %>"
                                                                CssClass="labelComman" />
                                                        </td>
                                                    </tr>
                                                </table>
                                            </div>
                                            <asp:GridView ID="GvGroup" PageSize="10" runat="server" AutoGenerateColumns="False"
                                                Width="100%" AllowPaging="True" OnPageIndexChanging="GvGroup_PageIndexChanging"
                                                OnSorting="GvGroup_OnSorting" CssClass="grid" AllowSorting="true">
                                                <AlternatingRowStyle CssClass="InvgridAltRow" />
                                                <Columns>
                                                    <asp:TemplateField HeaderText="<%$ Resources:Attendance,Edit %>">
                                                        <ItemTemplate>
                                                            <asp:ImageButton ID="btnEdit" runat="server" CommandArgument='<%# Eval("Group_Id") %>'
                                                                ImageUrl="~/Images/edit.png" CausesValidation="False" Visible="false" ToolTip="<%$ Resources:Attendance,Edit %>"
                                                                OnCommand="btnEdit_Command" />
                                                        </ItemTemplate>
                                                        <ItemStyle CssClass="grid" HorizontalAlign="Center" />
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="<%$ Resources:Attendance,Delete %>">
                                                        <ItemTemplate>
                                                            <asp:ImageButton ID="IbtnDelete" runat="server" CausesValidation="False" CommandArgument='<%# Eval("Group_Id") %>'
                                                                ImageUrl="~/Images/Erase.png" OnCommand="IbtnDelete_Command" Visible="false"
                                                                ToolTip="<%$ Resources:Attendance,Delete %>" />
                                                        </ItemTemplate>
                                                        <ItemStyle CssClass="grid" HorizontalAlign="Center" />
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="<%$ Resources:Attendance,Group Id%>" SortExpression="Group_Id">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblGroupId" runat="server" Text='<%# Eval("Group_Id") %>'></asp:Label>
                                                        </ItemTemplate>
                                                        <ItemStyle CssClass="grid" HorizontalAlign="Center" />
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="<%$ Resources:Attendance,Group Name %>" SortExpression="Group_Name">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblGroupName" runat="server" Text='<%# Eval("Group_Name") %>'></asp:Label>
                                                        </ItemTemplate>
                                                        <ItemStyle CssClass="grid" HorizontalAlign="Center" />
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="<%$ Resources:Attendance,Parent Group %>" SortExpression="ParentGroupName">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblParentGName" runat="server" Text='<%# Eval("ParentGroupName") %>'></asp:Label>
                                                        </ItemTemplate>
                                                        <ItemStyle CssClass="grid" HorizontalAlign="Center" />
                                                    </asp:TemplateField>
                                                </Columns>
                                                <HeaderStyle CssClass="Invgridheader" />
                                                <PagerStyle CssClass="Invgridheader" />
                                                <RowStyle CssClass="Invgridrow" HorizontalAlign="Center" />
                                            </asp:GridView>
                                            <asp:HiddenField ID="HDFSort" runat="server" />
                                            <asp:TreeView ID="TreeViewCategory" runat="server" Visible="false" OnSelectedNodeChanged="TreeViewCategory_SelectedNodeChanged">
                                            </asp:TreeView>
                                            <asp:Panel ID="panelOverlay" runat="server" class="Overlay" Visible="false">
                                                <asp:Panel ID="panelPopUpPanel" runat="server" class="PopUpPanel" Visible="false">
                                                    <table width="100%" style="background-image: url(../Images/bg_repeat.jpg); background-repeat: repeat;">
                                                        <tr>
                                                            <td align="<%= Common.ChangeTDForDefaultLeft()%>">
                                                                <asp:Label ID="lblDeletePanelHeader" runat="server" Font-Size="14px" Font-Bold="true"
                                                                    CssClass="labelComman" Text="<%$ Resources:Attendance, Delete Group %>"></asp:Label>
                                                            </td>
                                                            <td align="right">
                                                                <asp:ImageButton ID="btnClosePanel" runat="server" ImageUrl="~/Images/close.png"
                                                                    CausesValidation="False" OnClick="btnClosePanel_Click" Height="20px" Width="20px" />
                                                            </td>
                                                        </tr>
                                                    </table>
                                                    <table border="0" width="400px" bgcolor="#ccddee">
                                                        <tr id="trgv" runat="server" visible="false">
                                                            <td>
                                                                <asp:Label ID="Label1" runat="server" CssClass="labelComman" Text="<%$ Resources:Attendance,Group Id%>"></asp:Label>
                                                            </td>
                                                            <td align="<%= Common.ChangeTDForDefaultLeft()%>">
                                                                :
                                                            </td>
                                                            <td align="<%= Common.ChangeTDForDefaultLeft()%>">
                                                                <asp:Label ID="lblDelGroupId" runat="server" CssClass="labelComman" />
                                                            </td>
                                                        </tr>
                                                        <tr>
                                                            <td>
                                                                <asp:Label ID="Label2" runat="server" CssClass="labelComman" Text="<%$ Resources:Attendance,Group Name%>"></asp:Label>
                                                            </td>
                                                            <td align="<%= Common.ChangeTDForDefaultLeft()%>">
                                                                :
                                                            </td>
                                                            <td align="<%= Common.ChangeTDForDefaultLeft()%>">
                                                                <asp:Label ID="lblDelGroupName" runat="server" CssClass="labelComman" />
                                                            </td>
                                                        </tr>
                                                        <tr id="rowDelParentCategory" runat="server">
                                                            <td>
                                                                <asp:Label ID="Label3" runat="server" CssClass="labelComman" Text="<%$ Resources:Attendance,Parent Group%>"></asp:Label>
                                                            </td>
                                                            <td align="<%= Common.ChangeTDForDefaultLeft()%>">
                                                                :
                                                            </td>
                                                            <td align="<%= Common.ChangeTDForDefaultLeft()%>">
                                                                <asp:Label ID="lblDelParentGroup" runat="server" CssClass="labelComman" />
                                                            </td>
                                                        </tr>
                                                        <tr id="trdel" runat="server">
                                                            <td id="Td2" runat="server" colspan="3" align="center">
                                                                <asp:RadioButton ID="rdbtndelchild" runat="server" AutoPostBack="True" CssClass="labelComman"
                                                                    OnCheckedChanged="rdbtndelchild_CheckedChanged" Text="<%$ Resources:Attendance,Delete Child Also%>"
                                                                    GroupName="a" Visible="false" />
                                                                <asp:RadioButton ID="rbtnmovechild" runat="server" AutoPostBack="True" CssClass="labelComman"
                                                                    OnCheckedChanged="rbtnmovechild_CheckedChanged" Text="<%$ Resources:Attendance,Move Child%>"
                                                                    GroupName="a" Visible="false" />
                                                            </td>
                                                        </tr>
                                                        <tr id="trdel2" runat="server">
                                                            <td id="Td3" runat="server" colspan="3" align="center">
                                                                <asp:DropDownList ID="ddlgroup0" runat="server" CssClass="textComman" Width="200px"
                                                                    Visible="false" />
                                                            </td>
                                                        </tr>
                                                        <tr>
                                                            <td colspan="3" align="center">
                                                                <table style="width: 178px">
                                                                    <tr>
                                                                        <td align="left">
                                                                            <asp:Button ID="btnDeleteNode" runat="server" Text="<%$ Resources:Attendance, Delete %>"
                                                                                Visible="true" CssClass="buttonCommman" OnClick="btnDeleteNode_Click" CausesValidation="false" />
                                                                        </td>
                                                                        <td align="left">
                                                                            <asp:Button ID="btnCancelDelete" runat="server" Text="<%$ Resources:Attendance, Cancel %>"
                                                                                CssClass="buttonCommman" OnClick="btnCancelDelete_Click" CausesValidation="False" />
                                                                        </td>
                                                                    </tr>
                                                                </table>
                                                            </td>
                                                        </tr>
                                                    </table>
                                                </asp:Panel>
                                            </asp:Panel>
                                        </asp:Panel>
                                    </asp:Panel>
                                    <asp:Panel ID="PnlNewEdit" runat="server" DefaultButton="btnSave">
                                        <table width="100%" style="padding-left: 43px">
                                            <tr>
                                                <td align='<%= Common.ChangeTDForDefaultLeft()%>'>
                                                    <asp:Label ID="lblGroupName" runat="server" Text="<%$ Resources:Attendance,Group Name %>"
                                                        CssClass="labelComman" />
                                                </td>
                                                <td align="center">
                                                    :
                                                </td>
                                                <td align='<%= Common.ChangeTDForDefaultLeft()%>'>
                                                    <asp:TextBox ID="txtGroupName" runat="server" Width="250px" Height="21px" BackColor="#c3c3c3"
                                                        CssClass="textComman" AutoPostBack="true" OnTextChanged="txtGroupName_TextChanged" />
                                                    <cc1:AutoCompleteExtender ID="txtGroupName_AutoCompleteExtender" runat="server" DelimiterCharacters=""
                                                        Enabled="True" ServiceMethod="GetCompletionList" ServicePath="" CompletionInterval="100"
                                                        MinimumPrefixLength="1" CompletionSetCount="1" TargetControlID="txtGroupName"
                                                        UseContextKey="True">
                                                    </cc1:AutoCompleteExtender>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td>
                                                    <asp:Label ID="lblLGroupName" runat="server" CssClass="labelComman" Text="<%$ Resources:Attendance,Group Name(Local) %>"></asp:Label>
                                                </td>
                                                <td align="<%= Common.ChangeTDForDefaultLeft()%>">
                                                    :
                                                </td>
                                                <td>
                                                    <asp:TextBox ID="txtLGroupName" runat="server" Width="250px" CssClass="textComman" />
                                                </td>
                                            </tr>
                                            <tr>
                                                <td>
                                                    <asp:Label ID="lblPerentGroup" runat="server" CssClass="labelComman" Text="<%$ Resources:Attendance,Parent Group %>" />
                                                </td>
                                                <td align="<%= Common.ChangeTDForDefaultLeft()%>">
                                                    :
                                                </td>
                                                <td align='<%= Common.ChangeTDForDefaultLeft()%>'>
                                                    <asp:DropDownList ID="ddlPerentGroup" runat="server" CssClass="textComman" Width="263px">
                                                    </asp:DropDownList>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td colspan="2">
                                                </td>
                                                <td align='<%= Common.ChangeTDForDefaultLeft()%>' style="padding-left: 10px">
                                                    <table>
                                                        <tr>
                                                            <td width="90px">
                                                                <asp:Button ID="btnSave" runat="server" Text="<%$ Resources:Attendance, Save %>"
                                                                    CssClass="buttonCommman" OnClick="btnSave_Click" Visible="false" />
                                                            </td>
                                                            <td width="90px">
                                                                <asp:Button ID="btnReset" runat="server" Text="<%$ Resources:Attendance, Reset %>"
                                                                    CssClass="buttonCommman" OnClick="btnReset_Click" CausesValidation="False" />
                                                            </td>
                                                            <td>
                                                                <asp:Button ID="btnCancel" runat="server" Text="<%$ Resources:Attendance, Cancel %>"
                                                                    CssClass="buttonCommman" OnClick="btnCancel_Click" CausesValidation="False" />
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
                                                        <asp:Label ID="Label8" runat="server" Text="<%$ Resources:Attendance,Select Field %>"
                                                            CssClass="labelComman"></asp:Label>
                                                    </td>
                                                    <td width="180px">
                                                        <asp:DropDownList ID="ddlFieldNameBin" runat="server" W CssClass="DropdownSearch"
                                                            Height="25px" Width="170px">
                                                            <asp:ListItem Text="<%$ Resources:Attendance,Group Id %>" Value="Group_Id"></asp:ListItem>
                                                            <asp:ListItem Text="<%$ Resources:Attendance,Group Name %>" Value="Group_Name" Selected="True"></asp:ListItem>
                                                            <asp:ListItem Text="<%$ Resources:Attendance,Parent Group %>" Value="ParentGroupName"></asp:ListItem>
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
                                                        <asp:TextBox ID="txtValueBin" runat="server" Height="14px" Width="250px" CssClass="textCommanSearch" />
                                                    </td>
                                                    <td width="50px" align="center">
                                                        <asp:ImageButton ID="btnbindBin" runat="server" CausesValidation="False" Height="25px"
                                                            ImageUrl="~/Images/search.png" OnClick="btnbindBin_Click" Width="25px" ToolTip="<%$ Resources:Attendance,Search %>" />
                                                    </td>
                                                    <td>
                                                        <asp:Panel ID="Panel1" runat="server" DefaultButton="btnRefreshBin">
                                                            <asp:ImageButton ID="btnRefreshBin" runat="server" CausesValidation="False" Height="25px"
                                                                ImageUrl="~/Images/refresh.png" OnClick="btnRefreshBin_Click" Width="25px" ToolTip="<%$ Resources:Attendance,Refresh %>" />
                                                        </asp:Panel>
                                                    </td>
                                                    <td width="30px" align="center">
                                                        <asp:Panel ID="pnlimgBtnRestore" runat="server" DefaultButton="btnRestoreSelected">
                                                            <asp:ImageButton ID="btnRestoreSelected" runat="server" CausesValidation="False"
                                                                Height="25px" ImageUrl="~/Images/active.png" OnCommand="btnRestoreSelected_Click"
                                                                Width="25px" ToolTip="<%$ Resources:Attendance, Active %>" Visible="false" />
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
                                                        <asp:Label ID="lblTotalRecordsBin" runat="server" Text="<%$ Resources:Attendance,Total Records: 0 %>"
                                                            CssClass="labelComman"></asp:Label>
                                                    </td>
                                                </tr>
                                            </table>
                                        </div>
                                        <asp:Label ID="lblSelectedRecord" runat="server" Visible="false"></asp:Label>
                                        <asp:GridView ID="GvGroupBin" PageSize="10" runat="server" AutoGenerateColumns="False"
                                            Width="100%" AllowPaging="True" OnPageIndexChanging="GvGroupBin_PageIndexChanging"
                                            OnSorting="GvGroupBin_OnSorting" AllowSorting="true" CssClass="grid">
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
                                                <asp:TemplateField HeaderText="<%$ Resources:Attendance,Group Id%>" SortExpression="Group_Id">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblgvBGroupId" runat="server" Text='<%# Eval("Group_Id") %>'></asp:Label>
                                                    </ItemTemplate>
                                                    <ItemStyle CssClass="grid" />
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="<%$ Resources:Attendance,Group Name %>" SortExpression="Group_Name">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblGroupName" runat="server" Text='<%# Eval("Group_Name") %>'></asp:Label>
                                                    </ItemTemplate>
                                                    <ItemStyle CssClass="grid" />
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="<%$ Resources:Attendance,Parent Group %>" SortExpression="ParentGroupName">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblParentGName" runat="server" Text='<%# Eval("ParentGroupName") %>'></asp:Label>
                                                    </ItemTemplate>
                                                    <ItemStyle CssClass="grid" HorizontalAlign="Center" />
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
</asp:Content>
