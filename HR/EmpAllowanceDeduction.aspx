<%@ Page Language="C#" MasterPageFile="~/ERPMaster.master" AutoEventWireup="true"
    CodeFile="EmpAllowanceDeduction.aspx.cs" Inherits="HR_EmpAllowanceDeduction"
    Title="Untitled Page" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <link href="../CSS/InvStyle.css" rel="stylesheet" type="text/css" />
    <asp:UpdatePanel ID="UpdatePanel2" runat="server" UpdateMode="Conditional">
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
                                            <td width="350px" style="padding-left: 5px">
                                                <asp:Label ID="lblHeader" runat="server" Text="<%$ Resources:Attendance,Employee Allowance Deduction Setup %>"
                                                    CssClass="LableHeaderTitle"></asp:Label>
                                            </td>
                                        </tr>
                                    </table>
                                </td>
                                <td align="right">
                                    <table cellpadding="0" cellspacing="0">
                                        <tr>
                                            <td>
                                                <asp:Panel ID="pnlLeave" runat="server" CssClass="a" Width="100%" >
                                                    <asp:Button ID="btnLeave" runat="server" Text="<%$ Resources:Attendance,Leave %>" Visible="false"
                                                        Width="90px" BorderStyle="none" BackColor="Transparent" OnClick="btnLeave_Click"
                                                        Style="padding-left: 10px; padding-top: 3px; background-image: url('../Images/Bin.png' );
                                                        background-repeat: no-repeat; height: 49px; background-position: 5px 15px; font: bold 14px Trebuchet MS;
                                                        color: #000000;" />
                                                </asp:Panel>
                                            </td>
                                        </tr>
                                    </table>
                                </td>
                            </tr>
                            <tr>
                                <td bgcolor="#ccddee" colspan="4" width="100%" height="500px" valign="top">
                                    <asp:Panel ID="PnlEmployeeLeave" runat="server" Visible="true">
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
                                                    <asp:Panel ID="pnlGroup" runat="server" Visible="true">
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
                                                    <asp:Panel ID="Panel4" runat="server" DefaultButton="imgBtnLeaveBind" Visible="true">
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
                                                            DataKeyNames="Emp_Id" PageSize="<%# SystemParameter.GetPageSize() %>">
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
                                                                <asp:TemplateField>
                                                                    <ItemTemplate>
                                                                        <asp:ImageButton ID="imgBtnEmpEdit" runat="server" ImageUrl="~/Images/edit.png" Width="16px"
                                                                            OnCommand="btnEmpEdit_Command" CommandArgument='<%# Eval("Emp_Id") %>' />
                                                                    </ItemTemplate>
                                                                    <ItemStyle CssClass="grid" HorizontalAlign="Center" />
                                                                </asp:TemplateField>
                                                                <asp:TemplateField HeaderText="<%$ Resources:Attendance,Employee Code %>">
                                                                    <ItemTemplate>
                                                                        <asp:Label ID="lblEmpCode" runat="server" Text='<%# Eval("Emp_Id") %>'></asp:Label>
                                                                        <asp:Label ID="lblEmpId" Visible="false" runat="server" Text='<%# Eval("Emp_Id") %>'></asp:Label>
                                                                    </ItemTemplate>
                                                                    <ItemStyle CssClass="grid" />
                                                                </asp:TemplateField>
                                                                <asp:BoundField DataField="Emp_Name" HeaderText="<%$ Resources:Attendance,Employee Name %>"
                                                                    SortExpression="Emp_Name" ItemStyle-CssClass="grid" ItemStyle-Width="40%">
                                                                    <ItemStyle CssClass="grid" Width="40%" />
                                                                </asp:BoundField>
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
                                                                    <asp:Label ID="lblLeaveType" runat="server" CssClass="labelComman" Text="<%$ Resources:Attendance,Type %>"></asp:Label>
                                                                </td>
                                                                <td align="<%= Common.ChangeTDForDefaultLeft()%>">
                                                                    :
                                                                </td>
                                                                <td align="<%= Common.ChangeTDForDefaultLeft()%>">
                                                                    <asp:DropDownList ID="ddlType" runat="server" CssClass="labelComman" TabIndex="1"
                                                                        Width="200px" AutoPostBack="True" OnSelectedIndexChanged="ddlType_SelectedIndexChanged1">
                                                                        <asp:ListItem Value="0">---Select---</asp:ListItem>
                                                                        <asp:ListItem Value="1">Allowance</asp:ListItem>
                                                                        <asp:ListItem Value="2">Deduction</asp:ListItem>
                                                                    </asp:DropDownList>
                                                                </td>
                                                                <td align="<%= Common.ChangeTDForDefaultLeft()%>">
                                                                    <asp:Label ID="lblAllowDeduc" runat="server" CssClass="labelComman" Text="<%$ Resources:Attendance,Allowance/Deduction %>"></asp:Label>
                                                                </td>
                                                                <td align="<%= Common.ChangeTDForDefaultLeft()%>">
                                                                    :
                                                                </td>
                                                                <td align="<%= Common.ChangeTDForDefaultLeft()%>">
                                                                    <asp:DropDownList ID="ddlAllDeduc" runat="server" CssClass="labelComman" TabIndex="1"
                                                                        Width="200px" AutoPostBack="True">
                                                                    </asp:DropDownList>
                                                                </td>
                                                            </tr>
                                                            <tr>
                                                                <td align="<%= Common.ChangeTDForDefaultLeft()%>">
                                                                    <asp:Label ID="lblPaidLeave" runat="server" CssClass="labelComman" Text="<%$ Resources:Attendance,Value Type %>"></asp:Label>
                                                                </td>
                                                                <td align="<%= Common.ChangeTDForDefaultLeft()%>">
                                                                    :
                                                                </td>
                                                                <td align='<%= Common.ChangeTDForDefaultLeft()%>'>
                                                                    <asp:DropDownList ID="ddlValueType" runat="server" AutoPostBack="True" CssClass="labelComman"
                                                                        OnSelectedIndexChanged="ddlValueType_SelectedIndexChanged" TabIndex="5" Width="200px">
                                                                        <asp:ListItem Value="1">Fixed</asp:ListItem>
                                                                        <asp:ListItem Value="2">Percentage</asp:ListItem>
                                                                    </asp:DropDownList>
                                                                </td>
                                                                <td align="<%= Common.ChangeTDForDefaultLeft()%>">
                                                                    <asp:Label ID="lblPerOfSal" runat="server" CssClass="labelComman" Text="<%$ Resources:Attendance,Value %>"></asp:Label>
                                                                </td>
                                                                <td align="<%= Common.ChangeTDForDefaultLeft()%>">
                                                                    :
                                                                </td>
                                                                <td align="<%= Common.ChangeTDForDefaultLeft()%>">
                                                                    <asp:TextBox ID="txtCalValue" runat="server" Width="195px" TabIndex="4" CssClass="labelComman"></asp:TextBox>
                                                                    <cc1:FilteredTextBoxExtender ID="txtMobile_FilteredTextBoxExtender" runat="server"
                                                                        Enabled="True" TargetControlID="txtCalValue" ValidChars="1,2,3,4,5,6,7,8,9,0,.">
                                                                    </cc1:FilteredTextBoxExtender>
                                                                </td>
                                                            </tr>
                                                            <tr>
                                                                <td align="<%= Common.ChangeTDForDefaultLeft()%>">
                                                                    <asp:Label ID="lblSchType" runat="server" CssClass="labelComman" Text="<%$ Resources:Attendance,Calculation %>"></asp:Label>
                                                                </td>
                                                                <td>
                                                                    :
                                                                </td>
                                                                <td align="<%= Common.ChangeTDForDefaultLeft()%>">
                                                                    <asp:DropDownList ID="ddlCalculation" runat="server" CssClass="labelComman" TabIndex="1"
                                                                        Width="200px" AutoPostBack="True">
                                                                        <asp:ListItem Text="Daily" Value="Daily" />
                                                                        <asp:ListItem Text="Monthly" Value="Monthly" />
                                                                    </asp:DropDownList>
                                                                </td>
                                                                <td align='<%= Common.ChangeTDForDefaultLeft()%>'>
                                                                    <asp:CheckBox ID="chkMonthCarry" Visible="false" runat="server" CssClass="labelComman"
                                                                        TabIndex="6" Text="<%$ Resources:Attendance,IsMonthCarry %>" />
                                                                </td>
                                                                <td align="left">
                                                                    &#160;&#160;
                                                                </td>
                                                                <td align='<%= Common.ChangeTDForDefaultLeft()%>'>
                                                                    <asp:CheckBox ID="chkYearCarry" CssClass="labelComman" runat="server" TabIndex="7"
                                                                        Text="<%$ Resources:Attendance,IsYearCarry %>" />
                                                                </td>
                                                            </tr>
                                                            <tr>
                                                                <td colspan="2">
                                                                </td>
                                                                <td colspan="4" align="<%= Common.ChangeTDForDefaultLeft()%>" style="padding-left: 12px;">
                                                                    <asp:Button ID="btnSaveLeave" runat="server" CssClass="buttonCommman" TabIndex="8"
                                                                        Text="<%$ Resources:Attendance,Save %>" ValidationGroup="leavesave" Width="65px"
                                                                        OnClick="btnSaveLeave_Click" />
                                                                    &nbsp;
                                                                    <asp:Button ID="btnResetLeave" runat="server" CausesValidation="False" CssClass="buttonCommman"
                                                                        TabIndex="9" Text="<%$ Resources:Attendance,Reset %>" Width="65px" 
                                                                        onclick="btnResetLeave_Click" />
                                                                    &nbsp;
                                                                    <asp:Button ID="btnCancelLeave" runat="server" CausesValidation="False" CssClass="buttonCommman"
                                                                        TabIndex="10" Text="<%$ Resources:Attendance,Cancel %>" Width="65px" />
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
                                </td>
                            </tr>
                        </table>
                    </td>
                </tr>
            </table>
            
            <asp:Panel ID="pnl1" runat="server" class="MsgOverlayAddress" Visible="False">
                <asp:Panel ID="pnl2" runat="server" class="MsgPopUpPanelAddress" Visible="false">
                    <asp:Panel ID="pnl3" runat="server" Style="width: 100%; height: 100%; text-align: center;">
                        <table width="100%" style="background-image: url(../Images/bg_repeat.jpg); background-repeat: repeat;">
                            <tr>
                                <asp:Label ID="lblSelect" runat="server" Visible="false"></asp:Label>
                                <td align="<%= Common.ChangeTDForDefaultLeft()%>">
                                    <asp:Label ID="Label12" runat="server" Font-Size="14px" Font-Bold="true" CssClass="labelComman"
                                        Text="<%$ Resources:Attendance,Edit %>"></asp:Label>
                                </td>
                                <td align="right">
                                    <asp:ImageButton ID="ImageButton5" runat="server" ImageUrl="~/Images/close.png" CausesValidation="False"
                                        Height="20px" Width="20px" OnClick="Imgbtn_Click" />
                                </td>
                            </tr>
                        </table>
                        <table width="100%" style="padding-left: 43px">
                            <tr>
                                <td width="100px" align="<%= Common.ChangeTDForDefaultLeft()%>">
                                    <asp:Label ID="Label14" runat="server" Font-Size="14px" Font-Bold="true" CssClass="labelComman"
                                        Text="<%$ Resources:Attendance,Employee Code %>"></asp:Label>
                                    <asp:HiddenField ID="hdnEmpId" Visible="false" runat="server" Value='<%# Eval("Emp_Id") %>' />
                                </td>
                                <td width="1px">
                                    :
                                </td>
                                <td width="50px" align="<%= Common.ChangeTDForDefaultLeft()%>">
                                    <asp:Label ID="lblEmpCodeLeave" runat="server" Font-Size="14px" Font-Bold="true"
                                        CssClass="labelComman"></asp:Label>
                                </td>
                                <td width="150px" align="<%= Common.ChangeTDForDefaultLeft()%>">
                                    <asp:Label ID="Label13" runat="server" Font-Size="14px" Font-Bold="true" CssClass="labelComman"
                                        Text="<%$ Resources:Attendance,Employee Name %>"></asp:Label>
                                </td>
                                <td width="1px">
                                    :
                                </td>
                                <td width="300px" align="<%= Common.ChangeTDForDefaultLeft()%>">
                                    <asp:Label ID="lblEmpNameLeave" runat="server" Font-Size="14px" Font-Bold="true"
                                        CssClass="labelComman"></asp:Label>
                                </td>
                            </tr>
                            <tr>
                                <td width="100px">
                                    <asp:RadioButton ID="RbtAllowance" runat="server" Text="Allowance" GroupName="A"
                                        OnCheckedChanged="RbtAllowance_CheckedChanged" AutoPostBack="True" />
                                </td>
                                <td width="100px">
                                    <asp:RadioButton ID="RbtDeduction" runat="server" Text="Deduction" GroupName="A"
                                        OnCheckedChanged="RbtDeduction_CheckedChanged" AutoPostBack="True" />
                                </td>
                                <td width="100px">
                                    <asp:RadioButton ID="RbtBoth" runat="server" Text="Both" GroupName="A" OnCheckedChanged="RbtBoth_CheckedChanged"
                                        AutoPostBack="True" Checked="True" />
                                </td>
                            </tr>
                            <tr>
                                <td colspan="6">
                                    <asp:GridView ID="gvLeaveEmp" runat="server" AutoGenerateColumns="False" DataKeyNames="Emp_Id,Value"
                                        PageSize="<%# SystemParameter.GetPageSize() %>" TabIndex="10" Width="100%" AllowPaging="True"
                                        OnPageIndexChanging="gvLeaveEmp_PageIndexChanging"> 
                                        
                                        
                                        <Columns>
                                            <asp:TemplateField>
                                                <ItemTemplate>
                                                    <asp:CheckBox ID="ChkEmpCheck" runat="server" />
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="<%$ Resources:Attendance,Type %>">
                                                <ItemTemplate>
                                                    
                                                    <asp:Label ID="lblType" runat="server" Text='<%# GetType(Eval("Type").ToString()) %>'></asp:Label>
                                                    <asp:HiddenField ID="hdntransId" runat="server" Value='<%# Eval("Trans_Id") %>' />
                                                    <asp:HiddenField ID="hdngvType" runat="server" Value='<%#Eval("Type") %>' />
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="<%$ Resources:Attendance,Allowance/Deduction %>">
                                                <ItemTemplate>
                                                    <asp:HiddenField ID="hdnRefId" runat="server" Value='<%# Eval("Ref_Id") %>' />
                                                    <asp:Label ID="lblgvRefValue" runat="server" />
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="<%$ Resources:Attendance,Value Type %>">
                                                <ItemTemplate>
                                                    <asp:DropDownList ID="ddlSchType0" runat="server" SelectedValue='<%#Eval("Value_Type") %>'
                                                        Visible="true" Width="110px" AutoPostBack="true">
                                                        <asp:ListItem Value="1">Fixed</asp:ListItem>
                                                        <asp:ListItem Value="2">Percentage</asp:ListItem>
                                                    </asp:DropDownList>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="<%$ Resources:Attendance,Value %>">
                                                <ItemTemplate>
                                                    <asp:Label ID="lblValue" Visible="false" runat="server" Text='<%# Eval("Value") %>'></asp:Label>
                                                    <asp:TextBox ID="txtValue" runat="server" Visible="true" Text='<%# Eval("Value") %>'
                                                        Width="110px"></asp:TextBox>
                                                    <cc1:FilteredTextBoxExtender ID="FilteredTextBoxExtenderPAidLeave" runat="server"
                                                        TargetControlID="txtValue" ValidChars="0,1,2,3,4,5,6,7,8,9,.">
                                                    </cc1:FilteredTextBoxExtender>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="<%$ Resources:Attendance,Calculation %>">
                                                <ItemTemplate>
                                                    <asp:DropDownList ID="ddlCalcuationGrid" runat="server" SelectedValue='<%# Eval("Calculation_Method") %>'
                                                        Visible="True" Width="110px" AutoPostBack="true">
                                                        <asp:ListItem Value="Daily">Daily</asp:ListItem>
                                                        <asp:ListItem Value="Monthly">Monthly</asp:ListItem>
                                                    </asp:DropDownList>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                        </Columns>
                                        <AlternatingRowStyle CssClass="InvgridAltRow" />
                                        <HeaderStyle CssClass="Invgridheader" BorderStyle="Solid" BorderColor="#6E6E6E" BorderWidth="1px" />
                                        <PagerStyle CssClass="Invgridheader" />
                                        <RowStyle CssClass="Invgridrow" HorizontalAlign="Center" />
                                    </asp:GridView>
                                </td>
                            </tr>
                            <tr>
                                <td colspan="3">
                                    <asp:Button ID="btnSave" runat="server" CausesValidation="False" CssClass="buttonCommman"
                                        OnClick="BtnSave_Click" TabIndex="10" Text="<%$ Resources:Attendance,Save %>"
                                        Width="60px" />
                                    &nbsp;
                                    <asp:Button ID="Button2" runat="server" CausesValidation="False" CssClass="buttonCommman"
                                        TabIndex="9" Text="<%$ Resources:Attendance,Cancel %>" Width="60px" OnClick="Button2_Click" />
                                </td>
                            </tr>
                        </table>
                    </asp:Panel>
                </asp:Panel>
            </asp:Panel>
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
