<%@ Page Language="C#" MasterPageFile="~/ERPMaster.master" AutoEventWireup="true"
    CodeFile="DocumentNumber.aspx.cs" Inherits="SystemSetUp_DocumentNumber" Title="Untitled Page" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <link href="../CSS/InvStyle.css" rel="stylesheet" type="text/css" />
    <asp:UpdatePanel ID="UpdatePanel1" runat="server">
        <Triggers>
            <asp:PostBackTrigger ControlID="btnList" />
            <asp:PostBackTrigger ControlID="btnNew" />
            <asp:PostBackTrigger ControlID="ddlModuleName" />
            <asp:PostBackTrigger ControlID="gvDocMaster" />
        </Triggers>
        <ContentTemplate>
            <table width="100%">
                <tr>
                    <td>
                        <table width="100%" cellpadding="0" cellspacing="0" bordercolor="#F0F0F0">
                            <tr bgcolor="#90BDE9">
                                <td align='<%= Common.ChangeTDForDefaultLeft()%>'>
                                    <table>
                                        <tr>
                                            <td>
                                                <img src="../Images/product_icon.png" width="31" height="30" alt="D" />
                                            </td>
                                            <td>
                                                <img src="../Images/seperater.png" width="2" height="43" alt="SS" />
                                            </td>
                                            <td style="padding-left: 5px">
                                                <asp:Label ID="lblHeader" runat="server" Text="<%$ Resources:Attendance,Document Number %>"
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
                                                <%--   <asp:Panel ID="pnlMenuBin" runat="server" CssClass="a" Width="100%">
                                                    <asp:Button ID="btnBin" runat="server" Text="<%$ Resources:Attendance,Bin %>" Width="90px"
                                                        BorderStyle="none" BackColor="Transparent" OnClick="btnBin_Click" Style="padding-left: 10px;
                                                        padding-top: 3px; background-image: url(  '../Images/Bin.png' ); background-repeat: no-repeat;
                                                        height: 49px; background-position: 5px 15px; font: bold 14px Trebuchet MS; color: #000000;" />
                                                </asp:Panel> --%>
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
                                                                <asp:ListItem Text="<%$ Resources:Attendance,Module Name %>" Value="Module_Name"></asp:ListItem>
                                                                <asp:ListItem Text="<%$ Resources:Attendance,Object Name %>" Value="Object_Name"></asp:ListItem>
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
                                        <asp:GridView ID="gvDocMaster" PageSize="<%# SystemParameter.GetPageSize() %>" runat="server"
                                            AutoGenerateColumns="False" Width="100%" AllowPaging="True" AllowSorting="True"
                                            CssClass="grid" OnPageIndexChanging="gvDepMaster_PageIndexChanging" OnSorting="gvDepMaster_OnSorting">
                                            <Columns>
                                                <asp:TemplateField HeaderText="<%$ Resources:Attendance,Edit %>">
                                                    <ItemTemplate>
                                                        <asp:ImageButton ID="btnEdit" runat="server" CommandArgument='<%# Eval("Trans_Id") %>'
                                                            ImageUrl="~/Images/edit.png" CausesValidation="False" OnCommand="btnEdit_Command"
                                                            Visible="false" ToolTip="<%$ Resources:Attendance,Edit %>" /></ItemTemplate>
                                                    <ItemStyle HorizontalAlign="Center" />
                                                    <ItemStyle CssClass="grid" />
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="<%$ Resources:Attendance,Delete %>">
                                                    <ItemTemplate>
                                                        <asp:ImageButton ID="IbtnDelete" runat="server" CausesValidation="False" CommandArgument='<%# Eval("Trans_Id") %>'
                                                            ImageUrl="~/Images/Erase.png" OnCommand="IbtnDelete_Command" Visible="false"
                                                            ToolTip="<%$ Resources:Attendance,Delete %>" />
                                                    </ItemTemplate>
                                                    <ItemStyle HorizontalAlign="Center" />
                                                    <ItemStyle CssClass="grid" />
                                                </asp:TemplateField>
                                                <asp:TemplateField Visible="false">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblDocumentId1" runat="server" Text='<%# Eval("Trans_Id") %>'></asp:Label>
                                                    </ItemTemplate>
                                                    <ItemStyle CssClass="grid" />
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="<%$ Resources:Attendance,Module Name %>" SortExpression="Module_Name">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblDocName" runat="server" Text='<%# Eval("Module_Name") %>'></asp:Label>
                                                    </ItemTemplate>
                                                    <ItemStyle CssClass="grid" />
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="<%$ Resources:Attendance,Object Name %>" SortExpression="Object_Name">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblDocNameLocal" runat="server" Text='<%# Eval("Object_Name") %>'></asp:Label>
                                                    </ItemTemplate>
                                                    <ItemStyle CssClass="grid" />
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="<%$ Resources:Attendance,Prefix Name %>" SortExpression="Prefix">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblprefix" runat="server" Text='<%# Eval("Prefix") %>'></asp:Label>
                                                    </ItemTemplate>
                                                    <ItemStyle CssClass="grid" />
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="<%$ Resources:Attendance,Suffix Name %>" SortExpression="Suffix">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblsuffix" runat="server" Text='<%# Eval("Suffix") %>'></asp:Label>
                                                    </ItemTemplate>
                                                    <ItemStyle CssClass="grid" />
                                                </asp:TemplateField>
                                                <asp:TemplateField ItemStyle-HorizontalAlign="Center" HeaderText="<%$ Resources:Attendance,Company Id %>"
                                                    SortExpression="CompId">
                                                    <ItemTemplate>
                                                        <asp:Image ID="Image1" runat="server" ImageUrl="~/Images/Allow.png" Visible='<%#Eval("CompId") %>' />
                                                    </ItemTemplate>
                                                    <ItemStyle CssClass="grid" />
                                                </asp:TemplateField>
                                                <asp:TemplateField ItemStyle-HorizontalAlign="Center" HeaderText="<%$ Resources:Attendance,Brand Id %>"
                                                    SortExpression="BrandId">
                                                    <ItemTemplate>
                                                        <asp:Image ID="Image2" runat="server" ImageUrl="~/Images/Allow.png" Visible='<%#Eval("BrandId") %>' />
                                                    </ItemTemplate>
                                                    <ItemStyle CssClass="grid" />
                                                </asp:TemplateField>
                                                <asp:TemplateField ItemStyle-HorizontalAlign="Center" HeaderText="<%$ Resources:Attendance,Location Id %>"
                                                    SortExpression="LocationId">
                                                    <ItemTemplate>
                                                        <asp:Image ID="Image3" runat="server" ImageUrl="~/Images/Allow.png" Visible='<%#Eval("LocationId") %>' />
                                                    </ItemTemplate>
                                                    <ItemStyle CssClass="grid" />
                                                </asp:TemplateField>
                                                <asp:TemplateField ItemStyle-HorizontalAlign="Center" HeaderText="<%$ Resources:Attendance,Department Id %>"
                                                    SortExpression="DeptId">
                                                    <ItemTemplate>
                                                        <asp:Image ID="Image4" runat="server" ImageUrl="~/Images/Allow.png" Visible='<%#Eval("DeptId") %>'/>
                                                    </ItemTemplate>
                                                    <ItemStyle CssClass="grid" />
                                                </asp:TemplateField>
                                                <asp:TemplateField ItemStyle-HorizontalAlign="Center" HeaderText="<%$ Resources:Attendance,Employee Id %>"
                                                    SortExpression="EmpId">
                                                    <ItemTemplate>
                                                        <asp:Image ID="Image5" runat="server" ImageUrl="~/Images/Allow.png" Visible='<%#Eval("EmpId")%>' />
                                                    </ItemTemplate>
                                                    <ItemStyle CssClass="grid" />
                                                </asp:TemplateField>
                                                <asp:TemplateField ItemStyle-HorizontalAlign="Center" HeaderText="<%$ Resources:Attendance,Day %>"
                                                    SortExpression="Day">
                                                    <ItemTemplate>
                                                        <asp:Image ID="Image6" runat="server" ImageUrl="~/Images/Allow.png" Visible='<%#Eval("Day")%>' />
                                                    </ItemTemplate>
                                                    <ItemStyle CssClass="grid" />
                                                </asp:TemplateField>
                                                <asp:TemplateField ItemStyle-HorizontalAlign="Center" HeaderText="<%$ Resources:Attendance,Month %>"
                                                    SortExpression="Month">
                                                    <ItemTemplate>
                                                        <asp:Image ID="Image7" runat="server" ImageUrl="~/Images/Allow.png" Visible='<%#Eval("Month")%>' />
                                                    </ItemTemplate>
                                                    <ItemStyle CssClass="grid" />
                                                </asp:TemplateField>
                                                <asp:TemplateField ItemStyle-HorizontalAlign="Center" HeaderText="<%$ Resources:Attendance,Year %>"
                                                    SortExpression="Year">
                                                    <ItemTemplate>
                                                        <asp:Image ID="Image8" runat="server" ImageUrl="~/Images/Allow.png" Visible='<%#Eval("Year")%>' />
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
                                                <td align='<%= Common.ChangeTDForDefaultLeft()%>' width="150px">
                                                    <asp:Label ID="lblModuleName" runat="server" CssClass="labelComman" Text="<%$ Resources:Attendance,Module Name %>"></asp:Label>
                                                </td>
                                                <td width="1px">
                                                    :
                                                </td>
                                                <td align='<%= Common.ChangeTDForDefaultLeft()%>' width="270px">
                                                    <asp:DropDownList ID="ddlModuleName" Width="262px" runat="server" CssClass="textComman"
                                                        AutoPostBack="true" OnSelectedIndexChanged="ddlModuleName_SelectedIndexChanged1" />
                                                </td>
                                                <td align='<%= Common.ChangeTDForDefaultLeft()%>' width="170px">
                                                    <asp:Label ID="LblObjectName" runat="server" CssClass="labelComman" Text="<%$ Resources:Attendance,Object Name %>"></asp:Label>
                                                </td>
                                                <td style="width: 1px;">
                                                    :
                                                </td>
                                                <td align='<%= Common.ChangeTDForDefaultLeft()%>'>
                                                    <asp:DropDownList ID="ddlObjectName" Width="262px" runat="server" CssClass="textComman" />
                                                </td>
                                            </tr>
                                            <tr>
                                                <td align='<%= Common.ChangeTDForDefaultLeft()%>'>
                                                    <asp:Label ID="lblPrefix" runat="server" CssClass="labelComman" Text="<%$ Resources:Attendance,Prefix Name %>"></asp:Label>
                                                </td>
                                                <td style="width: 1px;">
                                                    :
                                                </td>
                                                <td align='<%= Common.ChangeTDForDefaultLeft()%>'>
                                                    <asp:TextBox ID="txtPrefixName" runat="server" CssClass="textComman"></asp:TextBox>
                                                </td>
                                                <td align='<%= Common.ChangeTDForDefaultLeft()%>'>
                                                    <asp:Label ID="lblSuffix" runat="server" CssClass="labelComman" Text="<%$ Resources:Attendance,Suffix Name %>"></asp:Label>
                                                </td>
                                                <td style="width: 1px;">
                                                    :
                                                </td>
                                                <td align='<%= Common.ChangeTDForDefaultLeft()%>'>
                                                    <asp:TextBox ID="txtSuffixName" runat="server" CssClass="textComman"></asp:TextBox>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td colspan="2">
                                                </td>
                                                <td align='<%= Common.ChangeTDForDefaultLeft()%>' colspan="4">
                                                    <table>
                                                        <tr>
                                                            <td width="150px" valign="top" align='<%= Common.ChangeTDForDefaultLeft()%>'>
                                                                <asp:CheckBox ID="chkDay" runat="server" CssClass="labelComman" Text="<%$ Resources:Attendance,Day %>" /><br />
                                                                <asp:CheckBox ID="chkMonth" runat="server" CssClass="labelComman" Text="<%$ Resources:Attendance,Month %>" /><br />
                                                                <asp:CheckBox ID="chkYear" runat="server" CssClass="labelComman" Text="<%$ Resources:Attendance,Year %>" />
                                                            </td>
                                                            <td width="150px" valign="top" align='<%= Common.ChangeTDForDefaultLeft()%>'>
                                                                <asp:CheckBox ID="chkCompanyId" runat="server" CssClass="labelComman" Text="<%$ Resources:Attendance,Company Id %>" /><br />
                                                                <asp:CheckBox ID="chkBrandId" runat="server" CssClass="labelComman" Text="<%$ Resources:Attendance,Brand Id %>" /><br />
                                                                <asp:CheckBox ID="chkLocationId" runat="server" CssClass="labelComman" Text="<%$ Resources:Attendance,Location Id %>" />
                                                            </td>
                                                            <td width="150px" valign="top" align='<%= Common.ChangeTDForDefaultLeft()%>'>
                                                                <asp:CheckBox ID="chkDepartmentId" runat="server" CssClass="labelComman" Text="<%$ Resources:Attendance,Department Id %>" /><br />
                                                                <asp:CheckBox ID="chkEmpId" runat="server" CssClass="labelComman" Text="<%$ Resources:Attendance,Employee Id %>" />
                                                            </td>
                                                        </tr>
                                                    </table>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td colspan="6" align="center">
                                                    <table style="width: 30%;">
                                                        <tr>
                                                            <td>
                                                                <asp:Button ID="btnSave" runat="server" Text="<%$ Resources:Attendance,Save %>" OnClick="btnSave_Click"
                                                                    Visible="false" CssClass="buttonCommman" ValidationGroup="a" />
                                                            </td>
                                                            <td>
                                                                <asp:Panel ID="PanelReset" runat="server" DefaultButton="btnReset">
                                                                    <asp:Button ID="btnReset" runat="server" Text="<%$ Resources:Attendance,Reset %>"
                                                                        OnClick="btnReset_Click" CssClass="buttonCommman" CausesValidation="False" />
                                                                </asp:Panel>
                                                            </td>
                                                            <td>
                                                                <asp:Panel ID="PanelCancel" runat="server" DefaultButton="btnCancel">
                                                                    <asp:Button ID="btnCancel" runat="server" Text="<%$ Resources:Attendance,Cancel %>"
                                                                        OnClick="btnCancel_Click" CssClass="buttonCommman" CausesValidation="False" />
                                                                </asp:Panel>
                                                            </td>
                                                        </tr>
                                                    </table>
                                                    <asp:HiddenField ID="editid" runat="server" />
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
            <div id="progressBackgroundFilter">
            </div>
            <div id="processMessage" class="progressBackgroundFilter">
                Loading…<br />
                <br />
                <img alt="Loading" src="../Images/ajax-loader2.gif" class="processMessage" /></div>
        </ProgressTemplate>
    </asp:UpdateProgress>
</asp:Content>
