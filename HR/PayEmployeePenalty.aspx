<%@ Page Language="C#" MasterPageFile="~/ERPMaster.master" AutoEventWireup="true"
    CodeFile="PayEmployeePenalty.aspx.cs" Inherits="Arca_Wing_PayEmployeePenalty"
    Title="Untitled Page" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <link href="../CSS/InvStyle.css" rel="stylesheet" type="text/css" />
    <asp:UpdatePanel ID="UpdatePanel2" runat="server" UpdateMode="Conditional">
        <Triggers>
            <asp:PostBackTrigger ControlID="btnLeave" />
            <asp:PostBackTrigger ControlID="btnReset" />
            <asp:PostBackTrigger ControlID="btnCancel" />
            <asp:PostBackTrigger ControlID="btnSave" />
            <asp:PostBackTrigger ControlID="btnLeast" />
            <asp:PostBackTrigger ControlID="BtnBindList" />
            <asp:PostBackTrigger ControlID="BtnRefreshList" />
            <asp:PostBackTrigger ControlID="BtnUpdate" />
            <asp:PostBackTrigger ControlID="BtnResetPenalty" />
            <asp:PostBackTrigger ControlID="gvEmployee" />
            <asp:PostBackTrigger ControlID="gvEmpLeave" />
            
        </Triggers>
        <ContentTemplate>
            <table width="100%">
                <tr align='<%= Common.ChangeTDForDefaultLeft()%>'>
                    <td>
                        <table width="100%" cellpadding="0" cellspacing="0" bordercolor="#F0F0F0">
                            <tr bgcolor="#90BDE9">
                                <td style="width: 30%;">
                                    <table>
                                        <tr>
                                            <td>
                                                <img src="../Images/product_icon.png" width="31" height="30" alt="D" />
                                            </td>
                                            <td>
                                                <img src="../Images/seperater.png" width="2" height="43" alt="SS" />
                                            </td>
                                            <td width="150px" style="padding-left: 5px">
                                                <asp:Label ID="lblHeader" runat="server" Text="<%$ Resources:Attendance,Penalty %>"
                                                    CssClass="LableHeaderTitle"></asp:Label>
                                            </td>
                                        </tr>
                                    </table>
                                </td>
                                <td align="right" style="width: 70%;">
                                    <table cellpadding="0" cellspacing="0">
                                        <tr>
                                            <td>
                                                <asp:Panel ID="pnlLeave" runat="server" CssClass="a" Width="100%">
                                                    <asp:Button ID="btnLeave" runat="server" Text="<%$ Resources:Attendance,Penalty %>"
                                                        Width="100px" BorderStyle="none" BackColor="Transparent" OnClick="btnLeave_Click"
                                                        Style="padding-top: 3px; padding-left: 15px; background-image: url('../Images/Bin.png' );
                                                        background-repeat: no-repeat; height: 49px; background-position: 5px 15px; font: bold 14px Trebuchet MS;
                                                        color: #000000;" />
                                                </asp:Panel>
                                            </td>
                                            <td>
                                                <asp:Panel ID="PanelList" runat="server" CssClass="a" Width="100%">
                                                    <asp:Button ID="btnLeast" runat="server" Text="<%$ Resources:Attendance,List %>"
                                                        Width="80px" BorderStyle="none" BackColor="Transparent" Style="padding-top: 3px;
                                                        padding-left: 10px; background-image: url('../Images/List.png' ); background-repeat: no-repeat;
                                                        height: 49px; background-position: 5px 15px; font: bold 14px Trebuchet MS; color: #000000;"
                                                        OnClick="btnLeast_Click" />
                                                </asp:Panel>
                                            </td>
                                        </tr>
                                    </table>
                                </td>
                            </tr>
                            <tr>
                                <td bgcolor="#ccddee" colspan="4" width="100%" height="500px" valign="top">
                                    <asp:Panel ID="PnlEmployeeLeave" runat="server" Visible="false">
                                        <table width="100%">
                                            <tr>
                                                <td align='<%= Common.ChangeTDForDefaultLeft()%>'>
                                                    <asp:RadioButton ID="rbtnGroup" CssClass="labelComman" OnCheckedChanged="EmpGroup_CheckedChanged"
                                                        runat="server" Text="<%$ Resources:Attendance,Group %>" Font-Bold="true" GroupName="EmpGroup"
                                                        AutoPostBack="true" />
                                                    <asp:RadioButton ID="rbtnEmp" runat="server" CssClass="labelComman" AutoPostBack="true"
                                                        Text="<%$ Resources:Attendance,Employee %>" GroupName="EmpGroup" Font-Bold="true"
                                                        OnCheckedChanged="EmpGroup_CheckedChanged" />
                                                </td>
                                            </tr>
                                            <tr>
                                                <td>
                                                    <asp:Panel ID="pnlGroup" runat="server">
                                                        <table width="100%">
                                                            <tr>
                                                                <td>
                                                                    <table width="100%">
                                                                        <tr>
                                                                            <td valign="top" align='<%= Common.ChangeTDForDefaultLeft()%>' width="170px">
                                                                                <asp:ListBox ID="lbxGroup" runat="server" Height="211px" Width="171px" SelectionMode="Multiple"
                                                                                    AutoPostBack="true" OnSelectedIndexChanged="lbxGroup_SelectedIndexChanged" CssClass="list"
                                                                                    Font-Names="Trebuchet MS" Font-Size="Small" ForeColor="Gray"></asp:ListBox>
                                                                            </td>
                                                                            <td valign="top" align='<%= Common.ChangeTDForDefaultLeft()%>'>
                                                                                <asp:GridView ID="gvEmployee" runat="server" AllowPaging="True" AutoGenerateColumns="False"
                                                                                    OnPageIndexChanging="gvEmp1_PageIndexChanging" CssClass="grid" Width="100%" PageSize="<%# SystemParameter.GetPageSize() %>">
                                                                                    <Columns>
                                                                                        <asp:TemplateField HeaderText="<%$ Resources:Attendance,Employee Code %>">
                                                                                            <ItemTemplate>
                                                                                                <asp:Label ID="lblEmpCode" runat="server" Text='<%# Eval("Emp_Code") %>'></asp:Label>
                                                                                                <asp:Label ID="lblEmpId" Visible="false" runat="server" Text='<%# Eval("Emp_Id") %>'></asp:Label>
                                                                                            </ItemTemplate>
                                                                                            <ItemStyle CssClass="grid" />
                                                                                        </asp:TemplateField>
                                                                                        <asp:BoundField DataField="Emp_Name" HeaderText="<%$ Resources:Attendance,Employee Name %>"
                                                                                            SortExpression="Emp_Name" ItemStyle-CssClass="grid" ItemStyle-Width="40%" />
                                                                                        <asp:TemplateField HeaderText="<%$ Resources:Attendance,Email Id %>">
                                                                                            <ItemTemplate>
                                                                                                <asp:Label ID="lblProductCode" runat="server" Text='<%# Eval("Email_Id") %>'></asp:Label>
                                                                                            </ItemTemplate>
                                                                                            <ItemStyle CssClass="grid" />
                                                                                        </asp:TemplateField>
                                                                                        <asp:TemplateField HeaderText="<%$ Resources:Attendance,Designation Name %>">
                                                                                            <ItemTemplate>
                                                                                                <asp:Label ID="lblItemType" runat="server" Text='<%# Eval("Designation") %>'></asp:Label>
                                                                                            </ItemTemplate>
                                                                                            <ItemStyle CssClass="grid" />
                                                                                        </asp:TemplateField>
                                                                                    </Columns>
                                                                                    <AlternatingRowStyle CssClass="InvgridAltRow" />
                                                                                    <HeaderStyle CssClass="Invgridheader" BorderStyle="Solid" BorderColor="#6E6E6E" BorderWidth="1px" />
                                                                                    <PagerStyle CssClass="Invgridheader" />
                                                                                    <RowStyle CssClass="Invgridrow" HorizontalAlign="Center" />
                                                                                </asp:GridView>
                                                                            </td>
                                                                        </tr>
                                                                    </table>
                                                                    <asp:Label ID="lblEmp" runat="server" Visible="false"></asp:Label>
                                                                </td>
                                                            </tr>
                                                        </table>
                                                    </asp:Panel>
                                                    <asp:Panel ID="Panel4" runat="server" DefaultButton="imgBtnLeaveBind">
                                                        <div width="100%" style="background-image: url(../Images/bg_repeat.jpg); background-repeat: repeat;">
                                                            <table width="100%" style="padding-left: 20px; height: 38px;">
                                                                <tr>
                                                                    <td width="90px">
                                                                        <asp:Label ID="Label11" runat="server" CssClass="labelComman" Text="<%$ Resources:Attendance,Select Option %>"></asp:Label>
                                                                    </td>
                                                                    <td width="170px">
                                                                        <asp:DropDownList ID="ddlField1" runat="server" CssClass="DropdownSearch" Height="25px"
                                                                            Width="165px">
                                                                            <asp:ListItem Text="<%$ Resources:Attendance,Employee Code %>" Value="Emp_Code"></asp:ListItem>
                                                                            <asp:ListItem Text="<%$ Resources:Attendance,Employee Name %>" Value="Emp_Name" Selected="True"></asp:ListItem>
                                                                        </asp:DropDownList>
                                                                    </td>
                                                                    <td width="125px">
                                                                        <asp:DropDownList ID="ddlOption1" runat="server" CssClass="DropdownSearch" Height="25px"
                                                                            Width="120px">
                                                                            <asp:ListItem Text="<%$ Resources:Attendance,--Select-- %>"></asp:ListItem>
                                                                            <asp:ListItem Text="<%$ Resources:Attendance,Equal %>"></asp:ListItem>
                                                                            <asp:ListItem Selected="True" Text="<%$ Resources:Attendance,Contains %>"></asp:ListItem>
                                                                            <asp:ListItem Text="<%$ Resources:Attendance,Like %>"></asp:ListItem>
                                                                        </asp:DropDownList>
                                                                    </td>
                                                                    <td width="24%">
                                                                        <asp:TextBox ID="txtValue1" runat="server" CssClass="textCommanSearch" Height="14px"
                                                                            Width="100%" />
                                                                    </td>
                                                                    <td width="50px" align="center">
                                                                        <asp:ImageButton ID="imgBtnLeaveBind" runat="server" CausesValidation="False" Height="25px"
                                                                            ImageUrl="~/Images/search.png" Width="25px" OnClick="btnLeavebind_Click" ToolTip="<%$ Resources:Attendance,Search %>" />
                                                                    </td>
                                                                    <td width="30px" align="center">
                                                                        <asp:Panel ID="Panel5" runat="server" DefaultButton="ImageButton2">
                                                                            <asp:ImageButton ID="ImageButton2" runat="server" CausesValidation="False" Height="25px"
                                                                                ImageUrl="~/Images/refresh.png" Width="25px" OnClick="btnLeaveRefresh_Click"
                                                                                ToolTip="<%$ Resources:Attendance,Refresh %>"></asp:ImageButton>
                                                                        </asp:Panel>
                                                                    </td>
                                                                    <td width="30px" align="center">
                                                                    </td>
                                                                    <td width="30px" align="center">
                                                                    </td>
                                                                    <td>
                                                                        <asp:ImageButton ID="ImageButton6" runat="server" OnClick="ImgbtnSelectAll_ClickLeave"
                                                                            ToolTip="<%$ Resources:Attendance, Select All %>" AutoPostBack="true" ImageUrl="~/Images/selectAll.png" />
                                                                    </td>
                                                                    <td align="center">
                                                                        <asp:Label ID="lblTotalRecordsLeave" runat="server" Text="<%$ Resources:Attendance,Total Records: 0 %>"
                                                                            CssClass="labelComman"></asp:Label>
                                                                    </td>
                                                                </tr>
                                                            </table>
                                                        </div>
                                                        <asp:Label ID="lblSelectRecd" runat="server" Visible="false"></asp:Label>
                                                        <asp:GridView ID="gvEmpLeave" runat="server" AllowPaging="True" AutoGenerateColumns="False"
                                                            OnPageIndexChanging="gvEmpLeave_PageIndexChanging" CssClass="grid" Width="100%"
                                                            DataKeyNames="Emp_Id" PageSize="<%# SystemParameter.GetPageSize() %>" Visible="false">
                                                            <Columns>
                                                                <asp:TemplateField>
                                                                    <ItemTemplate>
                                                                        <asp:CheckBox ID="chkgvSelect" runat="server" AutoPostBack="true" OnCheckedChanged="chkgvSelect_CheckedChangedLeave" />
                                                                    </ItemTemplate>
                                                                    <HeaderTemplate>
                                                                        <asp:CheckBox ID="chkgvSelectAll" runat="server" OnCheckedChanged="chkgvSelectAll_CheckedChangedLeave"
                                                                            AutoPostBack="true" />
                                                                    </HeaderTemplate>
                                                                    <ItemStyle HorizontalAlign="Center" />
                                                                </asp:TemplateField>
                                                                <asp:TemplateField HeaderText="<%$ Resources:Attendance,Employee Code %>">
                                                                    <ItemTemplate>
                                                                        <asp:Label ID="lblEmpCode" runat="server" Text='<%# Eval("Emp_Code") %>'></asp:Label>
                                                                        <asp:Label ID="lblEmpId" Visible="false" runat="server" Text='<%# Eval("Emp_Id") %>'></asp:Label>
                                                                    </ItemTemplate>
                                                                    <ItemStyle CssClass="grid" />
                                                                </asp:TemplateField>
                                                                <asp:BoundField DataField="Emp_Name" HeaderText="<%$ Resources:Attendance,Employee Name %>"
                                                                    SortExpression="Emp_Name" ItemStyle-CssClass="grid" ItemStyle-Width="40%" />
                                                                <asp:TemplateField HeaderText="<%$ Resources:Attendance,Email Id %>">
                                                                    <ItemTemplate>
                                                                        <asp:Label ID="lblProductCode" runat="server" Text='<%# Eval("Email_Id") %>'></asp:Label>
                                                                    </ItemTemplate>
                                                                    <ItemStyle CssClass="grid" />
                                                                </asp:TemplateField>
                                                                <asp:TemplateField HeaderText="<%$ Resources:Attendance,Phone No. %>">
                                                                    <ItemTemplate>
                                                                        <asp:Label ID="lblItemType" runat="server" Text='<%# Eval("Phone_No") %>'></asp:Label>
                                                                    </ItemTemplate>
                                                                    <ItemStyle CssClass="grid" />
                                                                </asp:TemplateField>
                                                            </Columns>
                                                            <AlternatingRowStyle CssClass="InvgridAltRow" />
                                                            <HeaderStyle CssClass="Invgridheader" BorderStyle="Solid" BorderColor="#6E6E6E" BorderWidth="1px" />
                                                            <PagerStyle CssClass="Invgridheader" />
                                                            <RowStyle CssClass="Invgridrow" HorizontalAlign="Center" />
                                                        </asp:GridView>
                                                    </asp:Panel>
                                                    <hr />
                                                    <asp:Panel ID="Panel3" runat="server" Width="100%">
                                                        <table width="100%">
                                                            <tr>
                                                                <td style="padding-top: 5px;">
                                                                </td>
                                                            </tr>
                                                            <tr>
                                                                <td align="<%= Common.ChangeTDForDefaultLeft()%>" width="104">
                                                                    <asp:Label ID="lblPenaltyName" runat="server" CssClass="labelComman" Text="<%$ Resources:Attendance,Penalty Name %>"></asp:Label>
                                                                </td>
                                                                <td align="<%= Common.ChangeTDForDefaultLeft()%>">
                                                                    :
                                                                </td>
                                                                <td colspan="4" align="<%= Common.ChangeTDForDefaultLeft()%>">
                                                                    <asp:TextBox ID="TxtPenaltyName" runat="server" Width="440px" TabIndex="1" CssClass="textComman" BackColor="#e3e3e3"></asp:TextBox>
                                                                <cc1:AutoCompleteExtender ID="AutoCompleteExtender5" runat="server" DelimiterCharacters=""
                                                        Enabled="True" ServiceMethod="GetPenaltyName" ServicePath="" CompletionInterval="100"
                                                        MinimumPrefixLength="1" CompletionSetCount="1" TargetControlID="TxtPenaltyName" UseContextKey="True"
                                                        CompletionListCssClass="completionList" CompletionListItemCssClass="listItem"
                                                        CompletionListHighlightedItemCssClass="itemHighlighted">
                                                    </cc1:AutoCompleteExtender>
                                                                </td>
                                                            </tr>
                                                            <tr>
                                                                <td valign="top" align="<%= Common.ChangeTDForDefaultLeft()%>">
                                                                    <asp:Label ID="lblPenaltyDiscription" runat="server" CssClass="labelComman" Text="<%$ Resources:Attendance,Penalty Description %>"></asp:Label>
                                                                </td>
                                                                <td valign="top" align="<%= Common.ChangeTDForDefaultLeft()%>">
                                                                    :
                                                                </td>
                                                                <td colspan="4" align='<%= Common.ChangeTDForDefaultLeft()%>'>
                                                                    <asp:TextBox ID="TxtPenaltyDiscription" runat="server" Width="440px" Height="70px"
                                                                        TextMode="MultiLine" TabIndex="2" CssClass="textComman"></asp:TextBox>
                                                                </td>
                                                            </tr>
                                                            <tr>
                                                                <td align="<%= Common.ChangeTDForDefaultLeft()%>">
                                                                    <asp:Label ID="LabelValueType" runat="server" CssClass="labelComman" Text="<%$ Resources:Attendance,Value Type %>"></asp:Label>
                                                                </td>
                                                                <td>
                                                                    :
                                                                </td>
                                                                <td>
                                                                    <asp:DropDownList ID="DdlValueType" runat="server" CssClass="textComman" TabIndex="3"
                                                                        Width="158px">
                                                                        <asp:ListItem Text="--Select--" Value="0"></asp:ListItem>
                                                                        <asp:ListItem Text="Fixed" Value="1" />
                                                                        <asp:ListItem Text="Percentage" Value="2" />
                                                                    </asp:DropDownList>
                                                                </td>
                                                                <td align="<%= Common.ChangeTDForDefaultLeft()%>">
                                                                    <asp:Label ID="LblValue" runat="server" CssClass="labelComman" Text="<%$ Resources:Attendance,Value%>"></asp:Label>
                                                                </td>
                                                                <td>
                                                                    :
                                                                </td>
                                                                <td align="<%= Common.ChangeTDForDefaultLeft()%>">
                                                                    <asp:TextBox ID="txtCalValue" runat="server" Width="150px" TabIndex="4" CssClass="textComman"></asp:TextBox>
                                                                    <cc1:FilteredTextBoxExtender ID="txtMobile_FilteredTextBoxExtender" runat="server"
                                                                        Enabled="True" TargetControlID="txtCalValue" ValidChars="1,2,3,4,5,6,7,8,9,0,.">
                                                                    </cc1:FilteredTextBoxExtender>
                                                                </td>
                                                            </tr>
                                                            <tr>
                                                                <td align="<%= Common.ChangeTDForDefaultLeft()%>">
                                                                    <asp:Label ID="lblMonth" runat="server" CssClass="labelComman" Text="<%$ Resources:Attendance,Month %>"></asp:Label>
                                                                </td>
                                                                <td>
                                                                    :
                                                                </td>
                                                                <td align="<%= Common.ChangeTDForDefaultLeft()%>">
                                                                    <asp:DropDownList ID="ddlMonth" runat="server" CssClass="textComman" TabIndex="5"
                                                                        Width="158px">
                                                                        <asp:ListItem Text="--Select--" Value="0" />
                                                                        <asp:ListItem Text="January" Value="1" />
                                                                        <asp:ListItem Text="February" Value="2" />
                                                                        <asp:ListItem Text="March" Value="3" />
                                                                        <asp:ListItem Text="April" Value="4" />
                                                                        <asp:ListItem Text="May" Value="5" />
                                                                        <asp:ListItem Text="June" Value="6" />
                                                                        <asp:ListItem Text="July" Value="7" />
                                                                        <asp:ListItem Text="August" Value="8" />
                                                                        <asp:ListItem Text="September" Value="9" />
                                                                        <asp:ListItem Text="October" Value="10" />
                                                                        <asp:ListItem Text="November" Value="11" />
                                                                        <asp:ListItem Text="December" Value="12" />
                                                                    </asp:DropDownList>
                                                                </td>
                                                                <td align='<%= Common.ChangeTDForDefaultLeft()%>'>
                                                                    <asp:Label ID="LblYear" runat="server" CssClass="labelComman" Text="<%$ Resources:Attendance,Year %>"></asp:Label>
                                                                </td>
                                                                <td>
                                                                    :
                                                                </td>
                                                                <td align='<%= Common.ChangeTDForDefaultLeft()%>'>
                                                                    <asp:TextBox ID="TxtYear" runat="server" Width="150px" TabIndex="6" CssClass="textComman"></asp:TextBox>
                                                                </td>
                                                            </tr>
                                                            <tr>
                                                                <td colspan="6" align="center" style="padding-left: 12px;">
                                                                    <asp:Button ID="btnSave" runat="server" CssClass="buttonCommman" TabIndex="7" Text="<%$ Resources:Attendance,Save %>"
                                                                        OnClick="btnSaveLeave_Click" ValidationGroup="leavesave" Width="65px" />
                                                                    &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                                                                    <asp:Button ID="btnReset" runat="server" CausesValidation="False" CssClass="buttonCommman"
                                                                        TabIndex="8" Text="<%$ Resources:Attendance,Reset %>" Width="65px" OnClick="btnReset_Click" />
                                                                    &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                                                                    <asp:Button ID="btnCancel" runat="server" CausesValidation="False" CssClass="buttonCommman"
                                                                        TabIndex="9" Text="<%$ Resources:Attendance,Cancel %>" Width="65px" OnClick="btnCancel_Click" />
                                                                </td>
                                                            </tr>
                                                            <tr>
                                                                <td colspan="6">
                                                                    &nbsp;
                                                                </td>
                                                            </tr>
                                                        </table>
                                                    </asp:Panel>
                                                </td>
                                            </tr>
                                        </table>
                                    </asp:Panel>
                                    <asp:Panel ID="PanelSearchList" runat="server" Width="100%">
                                        <div width="100%" style="background-image: url(../Images/bg_repeat.jpg); background-repeat: repeat;">
                                            <table width="100%" style="padding-left: 20px; height: 38px;">
                                                <tr>
                                                    <td width="90px">
                                                        <asp:Label ID="Label1" runat="server" CssClass="labelComman" Text="<%$ Resources:Attendance,Month %>"></asp:Label>
                                                    </td>
                                                    <td width="170px">
                                                        <asp:DropDownList ID="DdlMonthList" runat="server" CssClass="DropdownSearch" Height="25px"
                                                            Width="200px">
                                                            <asp:ListItem Text="--Select--" Value="0" />
                                                            <asp:ListItem Text="January" Value="1" />
                                                            <asp:ListItem Text="February" Value="2" />
                                                            <asp:ListItem Text="March" Value="3" />
                                                            <asp:ListItem Text="April" Value="4" />
                                                            <asp:ListItem Text="May" Value="5" />
                                                            <asp:ListItem Text="June" Value="6" />
                                                            <asp:ListItem Text="July" Value="7" />
                                                            <asp:ListItem Text="August" Value="8" />
                                                            <asp:ListItem Text="September" Value="9" />
                                                            <asp:ListItem Text="October" Value="10" />
                                                            <asp:ListItem Text="November" Value="11" />
                                                            <asp:ListItem Text="December" Value="12" />
                                                        </asp:DropDownList>
                                                    </td>
                                                    <td width="125px">
                                                        <asp:Label ID="Label4" runat="server" CssClass="labelComman" Text="<%$ Resources:Attendance,Year %>"></asp:Label>
                                                    </td>
                                                    <td width="24%">
                                                        <asp:TextBox ID="TxtYearList" runat="server" CssClass="textComman" Height="14px"
                                                            Width="200px" />
                                                             <cc1:FilteredTextBoxExtender ID="FilteredTextBoxExtender1" runat="server"
                                                                        Enabled="True" TargetControlID="TxtYearList" ValidChars="1,2,3,4,5,6,7,8,9,0,.">
                                                                    </cc1:FilteredTextBoxExtender>
                                                    </td>
                                                    <td width="50px" align="center">
                                                        <asp:Panel ID="Panel2" runat="server" DefaultButton="BtnBindList">
                                                            <asp:ImageButton ID="BtnBindList" runat="server" CausesValidation="False" Height="25px"
                                                                ImageUrl="~/Images/search.png" Width="25px" ToolTip="<%$ Resources:Attendance,Search %>"
                                                                OnClick="BtnBindList_Click" />
                                                        </asp:Panel>
                                                    </td>
                                                    <td width="30px" align="center">
                                                        <asp:Panel ID="Panel1" runat="server" DefaultButton="BtnRefreshList">
                                                            <asp:ImageButton ID="BtnRefreshList" runat="server" CausesValidation="False" Height="25px"
                                                                ImageUrl="~/Images/refresh.png" Width="25px" ToolTip="<%$ Resources:Attendance,Refresh %>"
                                                                OnClick="BtnRefreshList_Click"></asp:ImageButton>
                                                        </asp:Panel>
                                                    </td>
                                                    <%-- <td width="30px" align="center">
                                                                    </td>
                                                                    <td width="30px" align="center">
                                                                    </td>
                                                                    <td>
                                                                        <asp:ImageButton ID="ImageButton4" runat="server" OnClick="ImgbtnSelectAll_ClickLeave"
                                                                            ToolTip="<%$ Resources:Attendance, Select All %>" AutoPostBack="true" ImageUrl="~/Images/selectAll.png" />
                                                                    </td>
                                                                    <td align="center">
                                                                        <asp:Label ID="Label2" runat="server" Text="<%$ Resources:Attendance,Total Records: 0 %>"
                                                                            CssClass="labelComman"></asp:Label>
                                                                    </td>--%>
                                                </tr>
                                            </table>
                                        </div>
                                        <asp:Label ID="Label3" runat="server" Visible="false"></asp:Label>
                                        <asp:GridView ID="GridViewPenaltyList" runat="server" AllowPaging="True" AutoGenerateColumns="False"
                                            CssClass="grid" Width="100%" DataKeyNames="Emp_Id" 
                                            PageSize="<%# SystemParameter.GetPageSize() %>" onpageindexchanging="GridViewPenaltyList_PageIndexChanging" 
                                            >
                                            <Columns>
                                            <asp:TemplateField HeaderText="<%$ Resources:Attendance,Edit %>">
                                                    <ItemTemplate>
                                                        <asp:ImageButton ID="btnEdit" runat="server" CommandArgument='<%# Eval("Penalty_Id") %>'
                                                            ImageUrl="~/Images/edit.png" CausesValidation="False" OnCommand="btnEdit_command" 
                                                             ToolTip="<%$ Resources:Attendance,Edit %>" /></ItemTemplate>
                                                    <ItemStyle HorizontalAlign="Center" />
                                                 <ItemStyle CssClass="grid" />
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="<%$ Resources:Attendance,Delete %>">
                                                    <ItemTemplate>
                                                        <asp:ImageButton ID="IbtnDelete" runat="server" CausesValidation="False" CommandArgument='<%# Eval("Penalty_Id") %>'
                                                            ImageUrl="~/Images/Erase.png" OnCommand="IbtnDelete_command"  
                                                            ToolTip="<%$ Resources:Attendance,Delete %>" />
                                                    </ItemTemplate>
                                                    <ItemStyle HorizontalAlign="Center" />
                                                    <ItemStyle CssClass="grid" />
                                                    </asp:TemplateField>
                                                <asp:TemplateField HeaderText="<%$ Resources:Attendance,Employee Id %>">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblEmpIdList" runat="server" Text='<%# Eval("Emp_Id") %>'></asp:Label>
                                                    </ItemTemplate>
                                                    <ItemStyle CssClass="grid" />
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="<%$ Resources:Attendance,Employee Name %>">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblEmpnameList" runat="server" Text='<%# Eval("Emp_Name") %>'></asp:Label>
                                                    </ItemTemplate>
                                                    <ItemStyle CssClass="grid" />
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="<%$ Resources:Attendance,Penalty name %>">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblPenaltynameList" runat="server" Text='<%# Eval("Penalty_Name") %>'></asp:Label>
                                                    </ItemTemplate>
                                                    <ItemStyle CssClass="grid" />
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="<%$ Resources:Attendance,Value Type %>">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblvaluetype" runat="server" Text='<%# GetType(Eval("Value_Type").ToString()) %>'></asp:Label>
                                                    </ItemTemplate>
                                                    <ItemStyle CssClass="grid" />
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="<%$ Resources:Attendance,Value %>">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblPenaltyvalue" runat="server" Text='<%# Eval("Value") %>'></asp:Label>
                                                    </ItemTemplate>
                                                    <ItemStyle CssClass="grid" />
                                                </asp:TemplateField>
                                            </Columns>
                                            <AlternatingRowStyle CssClass="InvgridAltRow" />
                                            <HeaderStyle CssClass="Invgridheader" BorderStyle="Solid" BorderColor="#6E6E6E" BorderWidth="1px" />
                                            <PagerStyle CssClass="Invgridheader" />
                                            <RowStyle CssClass="Invgridrow" HorizontalAlign="Center" />
                                        </asp:GridView>
                                         <asp:Panel ID="PanelUpdatePenalty" runat="server" Width="100%" Visible="false">
                                                        <table width="100%">
                                                            <tr>
                                                                <td style="padding-top: 5px;">
                                                                </td>
                                                            </tr>
                                                             <tr>
                                                                <td  align="<%= Common.ChangeTDForDefaultLeft()%>">
                                                                    <asp:Label ID="Label2" runat="server" CssClass="labelComman" Text="<%$ Resources:Attendance,Employee Id %>"></asp:Label>
                                                                </td>
                                                                <td valign="top" align="<%= Common.ChangeTDForDefaultLeft()%>">
                                                                    :
                                                                </td>
                                                                <td colspan="4" align='<%= Common.ChangeTDForDefaultLeft()%>'>
                                                                    <asp:TextBox ID="txtEmployeeId" ReadOnly="true" runat="server" Width="440px" 
                                                                         TabIndex="2" CssClass="textComman"></asp:TextBox>
                                                                </td>
                                                            </tr>
                                                           
                                                            <tr>
                                                                <td  align="<%= Common.ChangeTDForDefaultLeft()%>">
                                                                    <asp:Label ID="Label5" runat="server" CssClass="labelComman" Text="<%$ Resources:Attendance,Employee Name %>"></asp:Label>
                                                                </td>
                                                                <td  align="<%= Common.ChangeTDForDefaultLeft()%>">
                                                                    :
                                                                </td>
                                                                <td colspan="4" align='<%= Common.ChangeTDForDefaultLeft()%>'>
                                                                    <asp:TextBox ID="txtEmployeeName" ReadOnly="true" runat="server" Width="440px" 
                                                                         TabIndex="2" CssClass="textComman"></asp:TextBox>
                                                                </td>
                                                            </tr>
                                                           
                                                           
                                                            <tr>
                                                                <td align="<%= Common.ChangeTDForDefaultLeft()%>" width="104">
                                                                    <asp:Label ID="LblPenaltynameList" runat="server" CssClass="labelComman" Text="<%$ Resources:Attendance,Penalty Name %>"></asp:Label>
                                                                </td>
                                                                <td align="<%= Common.ChangeTDForDefaultLeft()%>">
                                                                    :
                                                                </td>
                                                                <td colspan="4" align="<%= Common.ChangeTDForDefaultLeft()%>">
                                                                    <asp:TextBox ID="TxtPenaltyNameList" runat="server" Width="440px" TabIndex="1" CssClass="textComman" BackColor="#e3e3e3"></asp:TextBox>
                                                                <cc1:AutoCompleteExtender ID="AutoCompleteExtender1" runat="server" DelimiterCharacters=""
                                                        Enabled="True" ServiceMethod="GetPenaltyName" ServicePath="" CompletionInterval="100"
                                                        MinimumPrefixLength="1" CompletionSetCount="1" TargetControlID="TxtPenaltyNameList" UseContextKey="True"
                                                        CompletionListCssClass="completionList" CompletionListItemCssClass="listItem"
                                                        CompletionListHighlightedItemCssClass="itemHighlighted">
                                                    </cc1:AutoCompleteExtender>
                                                                
                                                                </td>
                                                            </tr>
                                                            <tr>
                                                                <td valign="top" align="<%= Common.ChangeTDForDefaultLeft()%>">
                                                                    <asp:Label ID="Lbllist" runat="server" CssClass="labelComman" Text="<%$ Resources:Attendance,Penalty Description %>"></asp:Label>
                                                                </td>
                                                                <td valign="top" align="<%= Common.ChangeTDForDefaultLeft()%>">
                                                                    :
                                                                </td>
                                                                <td colspan="4" align='<%= Common.ChangeTDForDefaultLeft()%>'>
                                                                    <asp:TextBox ID="TxtPenaltyDiscList" runat="server" Width="440px" Height="70px"
                                                                        TextMode="MultiLine" TabIndex="2" CssClass="textComman"></asp:TextBox>
                                                                </td>
                                                            </tr>
                                                            <tr>
                                                                <td align="<%= Common.ChangeTDForDefaultLeft()%>">
                                                                    <asp:Label ID="Lbltypelist" runat="server" CssClass="labelComman" Text="<%$ Resources:Attendance,Value Type %>"></asp:Label>
                                                                </td>
                                                                <td>
                                                                    :
                                                                </td>
                                                                <td>
                                                                    <asp:DropDownList ID="DdlvalueTypelist" runat="server" CssClass="textComman" TabIndex="3"
                                                                        Width="158px">
                                                                        <asp:ListItem Text="--Select--" Value="0"></asp:ListItem>
                                                                        <asp:ListItem Text="Fixed" Value="1" />
                                                                        <asp:ListItem Text="Percentage" Value="2" />
                                                                    </asp:DropDownList>
                                                                </td>
                                                                <td align="<%= Common.ChangeTDForDefaultLeft()%>">
                                                                    <asp:Label ID="LblvalueList" runat="server" CssClass="labelComman" Text="<%$ Resources:Attendance,Value%>"></asp:Label>
                                                                </td>
                                                                <td>
                                                                    :
                                                                </td>
                                                                <td align="<%= Common.ChangeTDForDefaultLeft()%>">
                                                                    <asp:TextBox ID="Txtvaluelist" runat="server" Width="150px" TabIndex="4" CssClass="textComman"></asp:TextBox>
                                                                    <cc1:FilteredTextBoxExtender ID="FilteredTextBoxExtender2" runat="server"
                                                                        Enabled="True" TargetControlID="txtCalValue" ValidChars="1,2,3,4,5,6,7,8,9,0,.">
                                                                    </cc1:FilteredTextBoxExtender>
                                                                </td>
                                                            </tr>
                                                            <tr>
                                                                <td align="<%= Common.ChangeTDForDefaultLeft()%>">
                                                                    <asp:Label ID="Label8" runat="server" CssClass="labelComman" Text="<%$ Resources:Attendance,Month %>"></asp:Label>
                                                                </td>
                                                                <td>
                                                                    :
                                                                </td>
                                                                <td align="<%= Common.ChangeTDForDefaultLeft()%>">
                                                                    <asp:DropDownList ID="DdlMonthListPanel" runat="server" CssClass="textComman" TabIndex="5"
                                                                        Width="158px">
                                                                        <asp:ListItem Text="--Select--" Value="0" />
                                                                        <asp:ListItem Text="January" Value="1" />
                                                                        <asp:ListItem Text="February" Value="2" />
                                                                        <asp:ListItem Text="March" Value="3" />
                                                                        <asp:ListItem Text="April" Value="4" />
                                                                        <asp:ListItem Text="May" Value="5" />
                                                                        <asp:ListItem Text="June" Value="6" />
                                                                        <asp:ListItem Text="July" Value="7" />
                                                                        <asp:ListItem Text="August" Value="8" />
                                                                        <asp:ListItem Text="September" Value="9" />
                                                                        <asp:ListItem Text="October" Value="10" />
                                                                        <asp:ListItem Text="November" Value="11" />
                                                                        <asp:ListItem Text="December" Value="12" />
                                                                    </asp:DropDownList>
                                                                </td>
                                                                <td align='<%= Common.ChangeTDForDefaultLeft()%>'>
                                                                    <asp:Label ID="Label9" runat="server" CssClass="labelComman" Text="<%$ Resources:Attendance,Year %>"></asp:Label>
                                                                </td>
                                                                <td>
                                                                    :
                                                                </td>
                                                                <td align='<%= Common.ChangeTDForDefaultLeft()%>'>
                                                                    <asp:TextBox ID="TxtpanelYearList" runat="server" Width="150px" TabIndex="6" CssClass="textComman"></asp:TextBox>
                                                                </td>
                                                            </tr>
                                                            <tr>
                                                                <td colspan="6" align="center" style="padding-left: 12px;">
                                                                    <asp:Button ID="BtnUpdate" runat="server" CssClass="buttonCommman" TabIndex="7" Text="<%$ Resources:Attendance,Update %>"
                                                                         ValidationGroup="leavesave" Width="65px" onclick="BtnUpdate_Click" />
                                                                    &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                                                                    <asp:Button ID="BtnResetPenalty" runat="server" CausesValidation="False" CssClass="buttonCommman"
                                                                        TabIndex="8" Text="<%$ Resources:Attendance,Reset %>" Width="65px" 
                                                                        onclick="BtnResetPenalty_Click"  />
                                                                    &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                                                                    <asp:HiddenField ID="HiddeniD" runat="server" />
                                                                    </td>
                                                            </tr>
                                                            <tr>
                                                                <td colspan="6">
                                                                    &nbsp;
                                                                </td>
                                                            </tr>
                                                        </table>
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
    <asp:UpdateProgress runat="server" AssociatedUpdatePanelID="UpdatePanel2" ID="updategvprogress">
        <ProgressTemplate>
            <div id="progressBackgroundFilter">
            </div>
            <div id="processMessainfoge" class="progressBackgroundFilter">
                Loading…<br />
                <br />
                <img alt="Loading" src="../Images/ajax-loader2.gif" class="processMessage" />
            </div>
        </ProgressTemplate>
    </asp:UpdateProgress>
</asp:Content>
