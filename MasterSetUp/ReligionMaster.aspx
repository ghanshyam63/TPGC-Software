﻿<%@ Page Language="C#" MasterPageFile="~/ERPMaster.master" AutoEventWireup="true"
    CodeFile="ReligionMaster.aspx.cs" Inherits="Master_ReligionMaster" Title="Religion Master" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
   
    <asp:UpdatePanel ID="upallpage" runat="server">
        <Triggers>
            <asp:PostBackTrigger ControlID="btnNew" />
            <asp:PostBackTrigger ControlID="btnBin" />
        </Triggers>
        <ContentTemplate>
            <table width="100%">
                <tr align='<%= Common.ChangeTDForDefaultLeft()%>'>
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
                                                <asp:Label ID="lblHeader" runat="server" Text="<%$ Resources:Attendance,Religion Setup %>"
                                                    CssClass="LableHeaderTitle"></asp:Label>
                                            </td>
                                        </tr>
                                    </table>
                                </td>
                                <td align="right">
                                    <table cellpadding="0" cellspacing="0">
                                        <tr>
                                            <td>
                                                <asp:Panel ID="pnlMenuNew" runat="server" CssClass="a">
                                                    <asp:Button ID="btnNew" runat="server" Text="<%$ Resources:Attendance,New %>" Width="90px"
                                                        BorderStyle="none" BackColor="Transparent" OnClick="btnNew_Click" Style="padding-left: 10px;
                                                        padding-top: 3px; background-image: url('../Images/New.png'); background-repeat: no-repeat;
                                                        height: 49px; background-position: 5px 15px; font: bold 14px Trebuchet MS; color: #000000;" />
                                                </asp:Panel>
                                            </td>
                                            <td>
                                                <asp:Panel ID="pnlMenuBin" runat="server" CssClass="a" Width="100%">
                                                    <asp:Button ID="btnBin" runat="server" Text="<%$ Resources:Attendance,Bin %>" Width="90px"
                                                        BorderStyle="none" BackColor="Transparent" OnClick="btnBin_Click" Style="padding-left: 10px;
                                                        padding-top: 3px; background-image: url('../Images/Bin.png'); background-repeat: no-repeat;
                                                        height: 49px; background-position: 5px 15px; font: bold 14px Trebuchet MS; color: #000000;" />
                                                </asp:Panel>
                                            </td>
                                        </tr>
                                    </table>
                                </td>
                            </tr>
                            <tr>
                                <td bgcolor="#ccddee" colspan="4" width="100%" height="500px" valign="top">
                                    <asp:Panel ID="pnlList" runat="server">
                                        <asp:Panel ID="panNewEdit" runat="server" DefaultButton="btnCSave">
                                            <table width="100%" style="padding-left: 43px; padding-top: 10px; padding-bottom: 20px">
                                                <tr>
                                                    <td align='<%= Common.ChangeTDForDefaultLeft()%>' width="120px">
                                                        <asp:Label ID="lblReligionName" runat="server" CssClass="labelComman" Text="<%$ Resources:Attendance,Religion Name %>" />
                                                    </td>
                                                    <td>
                                                        :
                                                    </td>
                                                    <td align='<%= Common.ChangeTDForDefaultLeft()%>' width="270px">
                                                        <asp:TextBox ID="txtReligionName" runat="server" Font-Names="Verdana" AutoPostBack="true"
                                                            OnTextChanged="txtReligionName_TextChanged" CssClass="textComman" BackColor="#e3e3e3"></asp:TextBox>
                                                        <cc1:AutoCompleteExtender ID="autoComplete1" runat="server"
                                                            DelimiterCharacters="" Enabled="True" ServiceMethod="GetCompletionListReligion" ServicePath=""
                                                            CompletionInterval="100" MinimumPrefixLength="1" CompletionSetCount="1" TargetControlID="txtReligionName"
                                                            UseContextKey="True" CompletionListCssClass="completionList" CompletionListItemCssClass="listItem"
                                                            CompletionListHighlightedItemCssClass="itemHighlighted">
                                                        </cc1:AutoCompleteExtender>
                                                    </td>
                                                    <td align='<%= Common.ChangeTDForDefaultLeft()%>'>
                                                        <asp:Label ID="lblReligionNameL" runat="server" CssClass="labelComman" Text="<%$ Resources:Attendance,Religion Name(Local) %>" />
                                                    </td>
                                                    <td>
                                                        :
                                                    </td>
                                                    <td align='<%= Common.ChangeTDForDefaultLeft()%>'>
                                                        <asp:TextBox ID="txtReligionNameL" runat="server" CssClass="textComman"></asp:TextBox>
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td colspan="6" align="center">
                                                        <table>
                                                            <tr>
                                                                <td width="90px">
                                                                    <asp:Button ID="btnCSave" runat="server" Text="<%$ Resources:Attendance,Save %>"
                                                                        CssClass="buttonCommman" OnClick="btnCSave_Click" Visible="false"  />
                                                                </td>
                                                                <td width="90px">
                                                                    <asp:Button ID="BtnReset" runat="server" Text="<%$ Resources:Attendance,Reset %>"
                                                                        CssClass="buttonCommman" CausesValidation="False" OnClick="BtnReset_Click" />
                                                                    <asp:HiddenField ID="editid" runat="server" />
                                                                </td>
                                                            </tr>
                                                        </table>
                                                    </td>
                                                </tr>
                                            </table>
                                        </asp:Panel>
                                        <asp:Panel ID="pnlSearchRecords" runat="server" DefaultButton="btnbind">
                                            <div width="100%" style="background-image: url(../Images/bg_repeat.jpg); background-repeat: repeat;">
                                                <table width="100%" style="padding-left: 20px; height: 38px">
                                                    <tr>
                                                        <td width="90px">
                                                            <asp:Label ID="LblSelectField" runat="server" Text="<%$ Resources:Attendance,Select Field %>"
                                                                CssClass="labelComman"></asp:Label>
                                                        </td>
                                                        <td width="180px">
                                                            <asp:DropDownList ID="ddlFieldName" runat="server" CssClass="DropdownSearch" Height="25px"
                                                                Width="170px">
                                                                <asp:ListItem Text="<%$ Resources:Attendance, Religion Name %>" Value="Religion" />
                                                                <asp:ListItem Text="<%$ Resources:Attendance, Religion Name(Local) %>" Value="Religion_L"
                                                                   />
                                                                   <asp:ListItem Text="<%$ Resources:Attendance,Religion Id %>" Value="Religion_Id"></asp:ListItem>
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
                                                        <td align='<%= Common.ChangeTDForDefaultLeft()%>' colspan="9">
                                                            <asp:Label ID="lblTotalRecords" runat="server" Text="<%$ Resources:Attendance,Total Records: 0 %>"
                                                                CssClass="labelComman"></asp:Label>
                                                        </td>
                                                    </tr>
                                                </table>
                                            </div>
                                        </asp:Panel>
                                        <asp:GridView ID="GvReligion" PageSize="<%# SystemParameter.GetPageSize() %>" runat="server" AutoGenerateColumns="False"
                                            Width="100%" AllowPaging="True" OnPageIndexChanging="GvReligion_PageIndexChanging"
                                            AllowSorting="True" OnSorting="GvReligion_Sorting" CssClass="grid">
                                            <AlternatingRowStyle CssClass="InvgridAltRow" />
                                            <Columns>
                                                <asp:TemplateField HeaderText="<%$ Resources:Attendance,Edit %>">
                                                    <ItemTemplate>
                                                        <asp:ImageButton ID="btnEdit" runat="server" CommandArgument='<%# Eval("Religion_Id") %>'
                                                            ImageUrl="~/Images/edit.png" OnCommand="btnEdit_Command" Visible="false"  CausesValidation="False"
                                                            ToolTip="<%$ Resources:Attendance,Edit %>" />
                                                    </ItemTemplate>
                                                    <ItemStyle HorizontalAlign="Center" />
                                                    <ItemStyle CssClass="grid" />
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="<%$ Resources:Attendance,Delete %>">
                                                    <ItemTemplate>
                                                        <asp:ImageButton ID="IbtnDelete" runat="server" CausesValidation="False" CommandArgument='<%# Eval("Religion_ID") %>'
                                                            ImageUrl="~/Images/Erase.png" OnCommand="IbtnDelete_Command" Visible="false"  Width="16px" ToolTip="<%$ Resources:Attendance,Delete %>" />
                                                    </ItemTemplate>
                                                    <ItemStyle HorizontalAlign="Center" />
                                                    <ItemStyle CssClass="grid" />
                                                </asp:TemplateField>
                                                 <asp:TemplateField HeaderText="<%$ Resources:Attendance,Religion Id %>" SortExpression="Religion_Id">
                                                    <ItemTemplate>
                                        
                                                        <asp:Label ID="lblDesigId1"  runat="server" Text='<%# Eval("Religion_Id") %>'></asp:Label>
                                                        
                                                        
                                                    </ItemTemplate>
                                                    <ItemStyle CssClass="grid" />
                                                </asp:TemplateField>
                                                <asp:BoundField DataField="Religion" HeaderText="<%$ Resources:Attendance,Religion Name %>"
                                                    SortExpression="Religion" ItemStyle-CssClass="grid" />
                                                <asp:BoundField DataField="Religion_L" HeaderText="<%$ Resources:Attendance,Religion Name(Local) %>"
                                                    SortExpression="Religion_L" ItemStyle-CssClass="grid" />
                                            </Columns>
                                            <HeaderStyle CssClass="Invgridheader" />
                                            <PagerStyle CssClass="Invgridheader" />
                                            <RowStyle CssClass="Invgridrow" HorizontalAlign="Center" />
                                        </asp:GridView>
                                    </asp:Panel>
                                    <asp:Panel ID="pnlBin" runat="server">
                                        <asp:Panel ID="pnlbinSearch" runat="server" DefaultButton="btnbindBin">
                                            <div width="100%" style="background-image: url(../Images/bg_repeat.jpg); background-repeat: repeat;">
                                                <table width="100%" style="padding-left: 20px; height: 38px">
                                                    <tr>
                                                        <td width="90px">
                                                            <asp:Label ID="lblbinSelectField" runat="server" Text="<%$ Resources:Attendance,Select Field %>"
                                                                CssClass="labelComman"></asp:Label>
                                                        </td>
                                                        <td width="180px">
                                                            <asp:DropDownList ID="ddlFieldNameBin" runat="server" CssClass="DropdownSearch" Height="25px"
                                                                Width="170px">
                                                                <asp:ListItem Text="<%$ Resources:Attendance, Religion Name%>" Value="Religion" />
                                                                <asp:ListItem Text="<%$ Resources:Attendance, Religion Name(Local) %>" Value="Religion_L"
                                                                     />
                                                                                       <asp:ListItem Text="<%$ Resources:Attendance,Religion Id %>" Value="Religion_Id"></asp:ListItem>
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
                                                            <asp:Panel ID="pnlbinRefresh" runat="server" DefaultButton="btnRefreshBin">
                                                                <asp:ImageButton ID="btnRefreshBin" runat="server" CausesValidation="False" Height="25px"
                                                                    ImageUrl="~/Images/refresh.png" OnClick="btnRefreshBin_Click" Width="25px" ToolTip="<%$ Resources:Attendance,Refresh %>">
                                                                </asp:ImageButton>
                                                            </asp:Panel>
                                                        </td>
                                                        <td width="30px" align="center">
                                                            <asp:Panel ID="pnlimgBtnRestore" runat="server" DefaultButton="imgBtnRestore">
                                                                <asp:ImageButton ID="imgBtnRestore" Height="25px" Width="25px" CausesValidation="False"
                                                                    runat="server" ImageUrl="~/Images/active.png"  Visible="false"   OnClick="btnRestoreSelected_Click"
                                                                    ToolTip="<%$ Resources:Attendance, Active %>" />
                                                            </asp:Panel>
                                                        </td>
                                                        <td>
                                                            <asp:Panel ID="pnlImgbtnSelectAll" runat="server" DefaultButton="ImgbtnSelectAll">
                                                                <asp:ImageButton ID="ImgbtnSelectAll" runat="server" OnClick="ImgbtnSelectAll_Click"
                                                                    ToolTip="<%$ Resources:Attendance, Select All %>" AutoPostBack="true" ImageUrl="~/Images/selectAll.png"   Visible="false"   />
                                                            </asp:Panel>
                                                        </td>
                                                        <td align='<%= Common.ChangeTDForDefaultLeft()%>'>
                                                            <asp:Label ID="lblTotalRecordsBin" runat="server" Text="<%$ Resources:Attendance,Total Records: 0 %>"
                                                                CssClass="labelComman"></asp:Label>
                                                        </td>
                                                    </tr>
                                                </table>
                                            </div>
                                        </asp:Panel>
                                        <asp:Label ID="lblSelectedRecord" runat="server" Visible="false"></asp:Label>
                                        <asp:GridView ID="GvReligionBin" PageSize="<%# SystemParameter.GetPageSize() %>" runat="server" AutoGenerateColumns="False"
                                            Width="100%" AllowPaging="True" OnPageIndexChanging="GvReligionBin_PageIndexChanging"
                                            OnSorting="GvReligionBin_OnSorting" AllowSorting="true" CssClass="grid">
                                            <Columns>
                                                <asp:TemplateField>
                                                    <HeaderTemplate>
                                                        <asp:CheckBox ID="chkgvSelectAll" runat="server" OnCheckedChanged="chkgvSelectAll_CheckedChanged"
                                                            AutoPostBack="true" />
                                                    </HeaderTemplate>
                                                    <ItemTemplate>
                                                        <asp:CheckBox ID="chkgvSelect" runat="server" AutoPostBack="true" OnCheckedChanged="chkgvSelect_CheckedChanged" />
                                                    </ItemTemplate>
                                                    <ItemStyle HorizontalAlign="Center" />
                                                </asp:TemplateField>
                                               <asp:TemplateField HeaderText="<%$ Resources:Attendance,Religion Id %>" SortExpression="Religion_Id">
                                                    <ItemTemplate>
                                        
                                                        <asp:Label ID="lblDesigId1"  runat="server" Text='<%# Eval("Religion_Id") %>'></asp:Label>
                                                        
                                                        
                                                    </ItemTemplate>
                                                    <ItemStyle CssClass="grid" />
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="<%$ Resources:Attendance,Religion Name %>" SortExpression="Religion">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblgvReligionName" runat="server" Text='<%# Eval("Religion") %>'></asp:Label>
                                                        <asp:Label ID="lblReligionId" Visible="false"  runat="server" Text='<%# Eval("Religion_Id") %>'></asp:Label>
                                                        
                                                        
                                                    </ItemTemplate>
                                                    <ItemStyle CssClass="grid" />
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="<%$ Resources:Attendance,Religion Name(Local) %>" SortExpression="Religion_L">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblgvReligionNameL" runat="server" Text='<%# Eval("Religion_L") %>'></asp:Label>
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
    <asp:UpdateProgress ID="uppro1" runat="server" AssociatedUpdatePanelID="upallpage">
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
