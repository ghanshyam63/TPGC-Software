<%@ Page Language="C#" MasterPageFile="~/ERPMaster.master" AutoEventWireup="true" 
    CodeFile="ShiftAssignToEmp.aspx.cs" Inherits="Attendance_ShiftAssignToEmp" Title="Untitled Page" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <link href="../CSS/InvStyle.css" rel="stylesheet" type="text/css" />
    <asp:UpdatePanel ID="UpdatePanel1" runat="server">
        <Triggers>
            <asp:PostBackTrigger ControlID="btnList" />
            <asp:PostBackTrigger ControlID="btnNew" />
            
             <asp:PostBackTrigger ControlID="btnNext" />
            <asp:PostBackTrigger ControlID="gvEmp" />
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
                                                <asp:Label ID="lblHeader" runat="server" Text="<%$ Resources:Attendance,Shift Assign To Employee %>"
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
                                                            <asp:Label ID="lblSelectOption" runat="server" CssClass="labelComman" Text="<%$ Resources:Attendance,Select Option %>"></asp:Label>
                                                        </td>
                                                        <td width="180px">
                                                            <asp:DropDownList ID="ddlFieldName" runat="server" CssClass="DropdownSearch" Height="25px"
                                                                Width="170px">
                                                                <asp:ListItem Text="<%$ Resources:Attendance,Employee Code %>" Value="Emp_Code"></asp:ListItem>
                                                                <asp:ListItem Text="<%$ Resources:Attendance,Employee Name %>" Value="Emp_Name" Selected="True"></asp:ListItem>
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
                                                                Width="100%" />
                                                        </td>
                                                        <td width="50px" align="center">
                                                            <asp:ImageButton ID="btnbind" runat="server" CausesValidation="False" Height="25px"
                                                                ImageUrl="~/Images/search.png" Width="25px" OnClick="btnbind_Click" ToolTip="<%$ Resources:Attendance,Search %>" />
                                                        </td>
                                                        <td width="50px" align="center">
                                                            <asp:Panel ID="pnlRefresh" runat="server" DefaultButton="btnRefresh">
                                                                <asp:ImageButton ID="btnRefresh" runat="server" CausesValidation="False" Height="25px"
                                                                    ImageUrl="~/Images/refresh.png" Width="25px" OnClick="btnRefresh_Click" ToolTip="<%$ Resources:Attendance,Refresh %>">
                                                                </asp:ImageButton>
                                                            </asp:Panel>
                                                        </td>
                                                        <td width="50px" align="center">
                                                        </td>
                                                        <td>
                                                            <td align="center">
                                                                <asp:Label ID="lblTotalRecord" runat="server" Text="<%$ Resources:Attendance,Total Records: 0 %>"
                                                                    CssClass="labelComman"></asp:Label>
                                                            </td>
                                                        </td>
                                                    </tr>
                                                </table>
                                            </div>
                                            <asp:GridView ID="gvEmp" runat="server" AllowPaging="True" AutoGenerateColumns="False"
                                                OnPageIndexChanging="gvEmp_PageIndexChanging" CssClass="grid" Width="100%" DataKeyNames="Emp_Code"
                                                PageSize="<%# SystemParameter.GetPageSize() %>" Visible="true">
                                                <Columns>
                                                    <asp:TemplateField HeaderText="<%$ Resources:Attendance,Edit %>">
                                                        <ItemTemplate>
                                                            <asp:ImageButton ID="btnEdit" runat="server" CausesValidation="False" CommandArgument='<%# Eval("Emp_Code") %>'
                                                                ImageUrl="~/Images/edit.png" Visible="true" OnCommand="btnEdit_Command" Width="16px" />
                                                        </ItemTemplate>
                                                        <ItemStyle HorizontalAlign="Center" />
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="<%$ Resources:Attendance,Employee Code %>">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblEmpCode" runat="server" Text='<%# Eval("Emp_Code") %>'></asp:Label>
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
                                    </asp:Panel>
                                    <asp:Panel ID="PnlNewEdit" runat="server">
                                        <asp:HiddenField ID="editid" runat="server" />
                                        <asp:Panel ID="pnlMain" runat="server">
                                            <table width="100%">
                                                <tr>
                                                    <td runat="server" id="trGroup" align="left" style="width: 368px; height: 23px;">
                                                        <asp:RadioButton ID="rbtnGroup" CssClass="labelComman" OnCheckedChanged="EmpGroup_CheckedChanged"
                                                            runat="server" Text="<%$ Resources:Attendance,Group %>" Font-Bold="true" GroupName="EmpGroup"
                                                            AutoPostBack="true" />
                                                        <asp:RadioButton ID="rbtnEmp" runat="server" CssClass="labelComman" AutoPostBack="true"
                                                            Text="<%$ Resources:Attendance,Employee %>" GroupName="EmpGroup" Font-Bold="true"
                                                            OnCheckedChanged="EmpGroup_CheckedChanged" />
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td colspan="2">
                                                        <asp:UpdatePanel ID="updt1" runat="server">
                                                            <ContentTemplate>
                                                                <asp:Panel ID="pnlShifttoemp" runat="server">
                                                                    <table style="height: 188px" width="100%">
                                                                        <tr>
                                                                            <td align="<%= Common.ChangeTDForDefaultLeft()%>">
                                                                                <asp:Panel ID="pnlemployee" runat="server" Height="300px" ScrollBars="Both" Width="100%"
                                                                                    DefaultButton="btnEmp">
                                                                                    <asp:Panel ID="pnlfilter" runat="server" Style="background-image: url(../Images/bg_repeat.jpg);
                                                                                        background-repeat: repeat;" Width="100%">
                                                                                        <table style="padding-left: 20px;">
                                                                                            <tr>
                                                                                                <td align="<%= Common.ChangeTDForDefaultLeft()%>" width="90px">
                                                                                                    <asp:Label ID="Label2" runat="server" CssClass="labelComman" Text="<%$ Resources:Attendance,Select Option %>"></asp:Label>
                                                                                                </td>
                                                                                                <td width="50px" align="<%= Common.ChangeTDForDefaultLeft()%>">
                                                                                                    <asp:DropDownList ID="ddlSelectField" runat="server" CssClass="DropdownSearch" Height="25px"
                                                                                                        Width="80px">
                                                                                                        <asp:ListItem Text="<%$ Resources:Attendance,Id%>" Value="Emp_Code"></asp:ListItem>
                                                                                                        <asp:ListItem Text="<%$ Resources:Attendance,Name %>" Value="Emp_Name"></asp:ListItem>
                                                                                                    </asp:DropDownList>
                                                                                                </td>
                                                                                                <td width="20px" align="<%= Common.ChangeTDForDefaultLeft()%>">
                                                                                                    <asp:DropDownList ID="ddlSelectOption" runat="server" CssClass="DropdownSearch" Height="25px"
                                                                                                        Style="margin-left: 0px" Width="100px">
                                                                                                        <asp:ListItem Text="<%$ Resources:Attendance,--Select--%>" Value="--Select one--"></asp:ListItem>
                                                                                                        <asp:ListItem Text="<%$ Resources:Attendance,Equal %>" Value="Equal"></asp:ListItem>
                                                                                                        <asp:ListItem Text="<%$ Resources:Attendance,Contains %>" Selected="True" Value="Contains"></asp:ListItem>
                                                                                                        <asp:ListItem Text="<%$ Resources:Attendance,Like%>" Value="Like"></asp:ListItem>
                                                                                                    </asp:DropDownList>
                                                                                                </td>
                                                                                                <td align="<%= Common.ChangeTDForDefaultLeft()%>" width="50px">
                                                                                                    <asp:TextBox ID="txtval" runat="server" CssClass="textCommanSearch" Height="14px"
                                                                                                        Width="100px"></asp:TextBox>
                                                                                                </td>
                                                                                                <td align="<%= Common.ChangeTDForDefaultLeft()%>" width="30px">
                                                                                                    <asp:ImageButton ID="btnEmp" runat="server" CausesValidation="False" Height="25px"
                                                                                                        ImageUrl="~/Images/search.png" OnClick="btnEmp_Click" TabIndex="4" Width="25px" />
                                                                                                </td>
                                                                                                <td align="<%= Common.ChangeTDForDefaultLeft()%>" width="30px">
                                                                                                    <asp:ImageButton ID="btnrefresh2" runat="server" CausesValidation="False" Height="25px"
                                                                                                        ImageUrl="~/Images/refresh.png" OnClick="btnRefresh2Report_Click" Width="25px"
                                                                                                        TabIndex="5"></asp:ImageButton>
                                                                                                </td>
                                                                                            </tr>
                                                                                        </table>
                                                                                    </asp:Panel>
                                                                                    <asp:GridView ID="GvEmpList" runat="server" AutoGenerateColumns="False" DataKeyNames="Emp_Code"
                                                                                        Width="100%">
                                                                                        <Columns>
                                                                                            <asp:TemplateField>
                                                                                                <ItemTemplate>
                                                                                                    <asp:CheckBox ID="chkSelect" runat="server" AutoPostBack="True" OnCheckedChanged="chkSelect_CheckedChanged" /></ItemTemplate>
                                                                                                <HeaderTemplate>
                                                                                                    <asp:CheckBox ID="chkSelAll" runat="server" AutoPostBack="True" OnCheckedChanged="chkSelAll_CheckedChanged" /></HeaderTemplate>
                                                                                                <HeaderStyle HorizontalAlign="Left" />
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
                                                                                </asp:Panel>
                                                                            </td>
                                                                            <td align="<%= Common.ChangeTDForDefaultLeft()%>">
                                                                                <asp:Panel ID="pnlEmpSel" runat="server" Height="300px" ScrollBars="Both" Width="100%"
                                                                                    DefaultButton="btnEmp0">
                                                                                    <asp:Panel ID="Panel1" runat="server" Style="background-image: url(../Images/bg_repeat.jpg);
                                                                                        background-repeat: repeat;" Width="100%">
                                                                                        <table style="padding-left: 20px;">
                                                                                            <tr>
                                                                                                <td width="90px" align='<%= Common.ChangeTDForDefaultLeft()%>'>
                                                                                                    <asp:Label ID="Label1" runat="server" CssClass="labelComman" Text="<%$ Resources:Attendance,Select Option %>"></asp:Label>
                                                                                                </td>
                                                                                                <td width="50px" align='<%= Common.ChangeTDForDefaultLeft()%>'>
                                                                                                    <asp:DropDownList ID="ddlSelectField0" CssClass="DropdownSearch" runat="server" Height="25px"
                                                                                                        Width="80px">
                                                                                                        <asp:ListItem Text="<%$ Resources:Attendance,ID%>" Value="Emp_Code"></asp:ListItem>
                                                                                                        <asp:ListItem Text="<%$ Resources:Attendance,Name %>" Value="Emp_Name"></asp:ListItem>
                                                                                                    </asp:DropDownList>
                                                                                                </td>
                                                                                                <td width="50px" align='<%= Common.ChangeTDForDefaultLeft()%>'>
                                                                                                    <asp:DropDownList ID="ddlSelectOption0" CssClass="DropdownSearch" runat="server"
                                                                                                        Height="25px" Width="100px">
                                                                                                        <asp:ListItem Text="<%$ Resources:Attendance,--Select--%>"></asp:ListItem>
                                                                                                        <asp:ListItem Text="<%$ Resources:Attendance,Equal %>" Value="Equal"></asp:ListItem>
                                                                                                        <asp:ListItem Text="<%$ Resources:Attendance,Contains %>" Selected="True" Value="Contains"></asp:ListItem>
                                                                                                        <asp:ListItem Text="<%$ Resources:Attendance,Like%>" Value="Like"></asp:ListItem>
                                                                                                    </asp:DropDownList>
                                                                                                </td>
                                                                                                <td width="50px" align='<%= Common.ChangeTDForDefaultLeft()%>'>
                                                                                                    <asp:TextBox ID="txtval0" CssClass="textCommanSearch" Height="14px" runat="server"
                                                                                                        Width="100px"></asp:TextBox>
                                                                                                </td>
                                                                                                <td width="30px" align='<%= Common.ChangeTDForDefaultLeft()%>'>
                                                                                                    <asp:ImageButton ID="btnEmp0" runat="server" CausesValidation="False" Height="25px"
                                                                                                        ImageUrl="~/Images/search.png" OnClick="btnEmp0_Click" TabIndex="4" Width="25px" />
                                                                                                </td>
                                                                                                <td width="50px" align='<%= Common.ChangeTDForDefaultLeft()%>'>
                                                                                                    <asp:ImageButton ID="btnRefresh3" runat="server" CausesValidation="False" Height="25px"
                                                                                                        ImageUrl="~/Images/refresh.png" OnClick="btnRefresh3Report_Click" Width="25px"
                                                                                                        TabIndex="5"></asp:ImageButton>
                                                                                                </td>
                                                                                            </tr>
                                                                                        </table>
                                                                                    </asp:Panel>
                                                                                    <asp:GridView ID="GvEmpListSelected" runat="server" AutoGenerateColumns="False" DataKeyNames="Emp_Code"
                                                                                        Width="100%">
                                                                                        <AlternatingRowStyle CssClass="InvgridAltRow" />
                                                                                        <HeaderStyle CssClass="Invgridheader" BorderStyle="Solid" BorderColor="#6E6E6E" BorderWidth="1px" />
                                                                                        <PagerStyle CssClass="Invgridheader" />
                                                                                        <RowStyle CssClass="Invgridrow" HorizontalAlign="Center" />
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
                                                                                    </asp:GridView>
                                                                                </asp:Panel>
                                                                    </table>
                                                                </asp:Panel>
                                                                <asp:Panel ID="pnlEmpGroup" runat="server">
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
                                                            </ContentTemplate>
                                                        </asp:UpdatePanel>
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td colspan="2">
                                                        <asp:Panel ID="Pnl1" runat="server" DefaultButton="btnsave">
                                                            <table style="width: 100%">
                                                                <tr>
                                                                    <td align='<%= Common.ChangeTDForDefaultLeft()%>' style="width: 125px">
                                                                        <asp:Label ID="lblDateFrom" runat="server" Font-Names="Verdana" Font-Size="12px"
                                                                            Text="<%$ Resources:Attendance,From Date %>"></asp:Label>
                                                                    </td>
                                                                    <td align='<%= Common.ChangeTDForDefaultLeft()%>' style="width: 257px">
                                                                        <asp:TextBox ID="txtFrom" runat="server" Width="150px" CssClass="textComman"></asp:TextBox>
                                                                        <cc1:CalendarExtender ID="txtTermDate_CalendarExtender" runat="server" Enabled="True"
                                                                    TargetControlID="txtFrom" >
                                                                </cc1:CalendarExtender>
                                                                    </td>
                                                                    <td align="<%= Common.ChangeTDForDefaultLeft()%>">
                                                                        &#160;&#160;
                                                                    </td>
                                                                    <td align='<%= Common.ChangeTDForDefaultLeft()%>'>
                                                                        <asp:Label ID="lblTo" runat="server" Text="<%$ Resources:Attendance,To Date %>" Font-Names="Verdana"
                                                                            Font-Size="12px"></asp:Label>
                                                                    </td>
                                                                    <td align='<%= Common.ChangeTDForDefaultLeft()%>'>
                                                                        <asp:TextBox ID="txtTo" runat="server" Width="150px" CssClass="textComman"></asp:TextBox>
                                                                        <cc1:CalendarExtender ID="txtTo_CalendarExtender"  runat="server"
                                                                            Enabled="True" TargetControlID="txtTo">
                                                                        </cc1:CalendarExtender>
                                                                    </td>
                                                                    <td align="left">
                                                                        <asp:Button ID="btnIsTemprary" Visible="false" runat="server" CssClass="buttonCommman"
                                                                            OnClick="btnIsTemprary_Click" Text="<%$ Resources:Attendance,Temporary Shift %>"
                                                                            Height="23px" Width="130px" />
                                                                        &nbsp;&nbsp;
                                                                    </td>
                                                                </tr>
                                                                <tr>
                                                                    <td align='<%= Common.ChangeTDForDefaultLeft()%>' style="height: 23px; width: 125px;"
                                                                        valign="bottom">
                                                                        <asp:Label ID="lblShift" runat="server" Text="<%$ Resources:Attendance,Shift %>"
                                                                            Font-Names="Verdana" Font-Size="12px"></asp:Label>
                                                                    </td>
                                                                    <td align='<%= Common.ChangeTDForDefaultLeft()%>' colspan="4" style="height: 23px"
                                                                        valign="top">
                                                                        <asp:DropDownList ID="ddlShift" runat="server" CssClass="DropdownSearch" Height="25px"
                                                                            Width="162px">
                                                                        </asp:DropDownList>
                                                                        <asp:Button ID="btnshowpopup" runat="server" Style="display: none" />
                                                                    </td>
                                                                </tr>
                                                                <tr>
                                                                    <td align="center" colspan="5">
                                                                        <asp:Button ID="btnsave" runat="server" Text="<%$ Resources:Attendance,Save %>" OnClick="btnsave_Click"
                                                                            CssClass="buttonCommman" Width="77px" Visible="true" Height="23px" /><asp:HiddenField
                                                                                ID="HiddenField1" runat="server" />
                                                                        &nbsp;&nbsp;
                                                                        <asp:Button ID="btncancel1" runat="server" Text="<%$ Resources:Attendance,Cancel %>"
                                                                            OnClick="btnCancel1_Click" CssClass="buttonCommman" Width="77px" Visible="true"
                                                                            Height="23px" /><asp:HiddenField ID="HiddenField2" runat="server" />
                                                                        &nbsp;&nbsp;
                                                                        <asp:Button ID="Button1" runat="server" CssClass="buttonCommman" Height="23px" OnClick="BtnDelete_Click"
                                                                            Text="<%$ Resources:Attendance,Delete %>" />
                                                                    </td>
                                                                </tr>
                                                            </table>
                                                        </asp:Panel>
                                                        &nbsp;&nbsp; &nbsp;
                                                        <asp:Button ID="btnNext" runat="server" CausesValidation="False" CssClass="buttonCommman"
                                                            Height="25px" ImageUrl="~/Images/buttonCancel.png" OnClick="btnNext1_Click" TabIndex="4"
                                                            Text="<%$ Resources:Attendance,Next %>" />
                                                        &nbsp;&nbsp;
                                                        <asp:Button ID="btnCancel2" runat="server" CausesValidation="False" CssClass="buttonCommman"
                                                            Height="25px" ImageUrl="~/Images/buttonCancel.png" OnClick="btnCancel2_Click"
                                                            TabIndex="4" Text="<%$ Resources:Attendance,Cancel %>" />
                                                    </td>
                                                </tr>
                                            </table>
                                        </asp:Panel>
                                        <table width="100%" runat="server" id="tbl">
                                            <tr id="Tr1" runat="server">
                                                <td id="Td1" colspan="2" align="<%= Common.ChangeTDForDefaultLeft()%>" runat="server">
                                                    <table>
                                                        <tr>
                                                            <td>
                                                            </td>
                                                            <td align="right">
                                                                <asp:Button ID="btnOk0" runat="server" CausesValidation="False" CssClass="buttonCommman"
                                                                    ImageUrl="~/Images/buttonSave.png" OnClick="btnsaveTempShift_Click" TabIndex="2"
                                                                    Text="<%$ Resources:Attendance,Save %>" Visible="true" Height="25px" />
                                                                &nbsp;&nbsp;&nbsp;&nbsp;
                                                                <asp:Button ID="btnClearAll" runat="server" CausesValidation="False" CssClass="buttonCommman"
                                                                    Height="25px" ImageUrl="~/Images/buttonSave.png" OnClick="btnClearAll_Click"
                                                                    TabIndex="2" Text="Clear All" />
                                                                &nbsp;&nbsp;&nbsp;&nbsp;
                                                                <asp:Button ID="btnCancelPanel" runat="server" CausesValidation="False" CssClass="buttonCommman"
                                                                    Height="25px" ImageUrl="~/Images/buttonCancel.png" OnClick="btnCancelPanel_Click1"
                                                                    TabIndex="4" Text="<%$ Resources:Attendance,Cancel %>" />
                                                            </td>
                                                        </tr>
                                                    </table>
                                                </td>
                                            </tr>
                                            <tr id="Tr2" runat="server">
                                                <td id="Td2" valign="top" runat="server">
                                                    <table width="100%">
                                                        <tr>
                                                            <td>
                                                                <asp:Label ID="lblSelectShift" runat="server" CssClass="labelComman" Text="<%$ Resources:Attendance,selectshiftcat %>"
                                                                    Font-Bold="true"></asp:Label>
                                                            </td>
                                                        </tr>
                                                        <tr>
                                                            <td>
                                                                <asp:Panel ID="PnlTimeTableList" runat="server" Width="100%" Height="350px" ScrollBars="Vertical">
                                                                    <table>
                                                                        <tr>
                                                                            <td align="<%= Common.ChangeTDForDefaultLeft()%>" valign="top">
                                                                                <asp:CheckBoxList ID="chkTimeTableList" runat="server" AutoPostBack="True" OnSelectedIndexChanged="chkTimeTableList_SelectedIndexChanged"
                                                                                    RepeatColumns="1" TabIndex="1" Font-Names="Verdana" Font-Size="10pt">
                                                                                </asp:CheckBoxList>
                                                                            </td>
                                                                        </tr>
                                                                    </table>
                                                                </asp:Panel>
                                                            </td>
                                                        </tr>
                                                    </table>
                                                </td>
                                                <td id="Td3" valign="top" runat="server">
                                                    <table width="100%">
                                                        <tr>
                                                            <td>
                                                                <asp:Label ID="lblselectdate" runat="server" Font-Bold="true" CssClass="labelComman"
                                                                    Text="<%$ Resources:Attendance,selectdate %>"></asp:Label>
                                                            </td>
                                                        </tr>
                                                        <tr>
                                                            <td>
                                                                <asp:Panel ID="pnlAddDays" runat="server" Width="100%" Height="350px" ScrollBars="Vertical">
                                                                    <table>
                                                                        <tr>
                                                                            <td align="<%= Common.ChangeTDForDefaultLeft()%>" valign="top">
                                                                                <asp:CheckBoxList ID="chkDayUnderPeriod" runat="server" RepeatColumns="1" TabIndex="3"
                                                                                    Font-Names="Verdana" Font-Size="10pt">
                                                                                </asp:CheckBoxList>
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
                                        <asp:Panel ID="pnlView" runat="server">
                                            <table width="100%">
                                                <tr>
                                                    <td>
                                                        <asp:LinkButton ID="lnkBackToList" runat="server" CausesValidation="False" OnClick="lnkBackToList_Click"
                                                            CssClass="labelComman" Text="<%$ Resources:Attendance,Back To List %>"></asp:LinkButton>
                                                    </td>
                                                    <td colspan="4">
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td style="height: 23px; background-image: url('../../zczxc.gif');" align="center"
                                                        colspan="5">
                                                        <asp:Label ID="lblschheader" runat="server" Text="<%$ Resources:Attendance,Schedule For %>"
                                                            CssClass="labelComman"></asp:Label>
                                                        : &nbsp;&nbsp;<asp:Label ID="lblempname" CssClass="labelComman" Font-Bold="true"
                                                            runat="server"></asp:Label>
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td>
                                                        <asp:Label ID="lblFromDate" CssClass="labelComman" runat="server" Text="<%$ Resources:Attendance,From Date  %>"></asp:Label>
                                                        :
                                                    </td>
                                                    <td>
                                                        <asp:TextBox ID="txtFromDate" runat="server" CssClass="textComman" Width="150px"></asp:TextBox>
                                                        <cc1:CalendarExtender
                                                            ID="txtFromDate_CalendarExtender" runat="server" Enabled="True" 
                                                            TargetControlID="txtFromDate">
                                                        </cc1:CalendarExtender>
                                                    </td>
                                                    <td>
                                                        <asp:Label ID="lblToDate" CssClass="labelComman" runat="server" Text="<%$ Resources:Attendance,To Date  %>"></asp:Label>
                                                        :
                                                    </td>
                                                    <td>
                                                        <asp:TextBox ID="txtToDate" runat="server" CssClass="textComman" Width="150px"></asp:TextBox>
                                                        <cc1:CalendarExtender
                                                            ID="txtToDate_CalendarExtender" runat="server" Enabled="True" 
                                                            TargetControlID="txtToDate">
                                                        </cc1:CalendarExtender>
                                                    </td>
                                                    <td>
                                                        <asp:Button Width="50px" ID="btnsubmit" runat="server" Text="<%$ Resources:Attendance,Go %>"
                                                            OnClick="btnsubmit_Click" CssClass="buttonCommman" CausesValidation="False" />
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td style="padding-left: 35px;" colspan="5">
                                                        <asp:Panel ID="Panel2" runat="server" Height="300px" ScrollBars="Both">
                                                            <asp:GridView ID="GvShiftReport" runat="server" DataKeyNames="Att_Date,Emp_Id,OnDuty_Time,OffDuty_Time"
                                                                OnRowDataBound="GvShiftreport_RowDataBound" AutoGenerateColumns="False" Width="100%">
                                                                <Columns>
                                                                    <asp:TemplateField HeaderText="<%$ Resources:Attendance,ID %>">
                                                                        <ItemTemplate>
                                                                            <asp:Label ID="lblEmpID" runat="server" Text='<%# Eval("Emp_Id") %>'></asp:Label></ItemTemplate>
                                                                    </asp:TemplateField>
                                                                    <asp:BoundField DataField="EmpName" HeaderText="<%$ Resources:Attendance,Name %>" />
                                                                    <asp:TemplateField HeaderText="<%$ Resources:Attendance,Date %>">
                                                                        <ItemTemplate>
                                                                            <asp:Label ID="lblDate" runat="server" Text='<%# GetDate(Eval("Att_Date")) %>'></asp:Label></ItemTemplate>
                                                                    </asp:TemplateField>
                                                                    <asp:TemplateField HeaderText="<%$ Resources:Attendance,Shift %>">
                                                                        <ItemTemplate>
                                                                            <asp:Label ID="lblshift0" runat="server" Text='<%# Eval("Shift_Name") %>'> </asp:Label></ItemTemplate>
                                                                    </asp:TemplateField>
                                                                    <asp:TemplateField HeaderText="<%$ Resources:Attendance,On Duty Time %>">
                                                                        <ItemTemplate>
                                                                            <asp:Label ID="lblOnDuty" runat="server" Text='<%# Eval("OnDuty_Time") %>'></asp:Label></ItemTemplate>
                                                                    </asp:TemplateField>
                                                                    <asp:TemplateField HeaderText="<%$ Resources:Attendance,Off Duty Time %>">
                                                                        <ItemTemplate>
                                                                            <asp:Label ID="lbloffduty" runat="server" Text='<%# Eval("OffDuty_Time") %>'></asp:Label></ItemTemplate>
                                                                    </asp:TemplateField>
                                                                    <asp:CheckBoxField DataField="Is_Off" HeaderText="<%$ Resources:Attendance,Is Off %>" />
                                                                    <asp:CheckBoxField DataField="Is_Temp" HeaderText="<%$ Resources:Attendance,Is Temp %>" />
                                                                    <asp:TemplateField HeaderText="<%$ Resources:Attendance,Is Holiday %>">
                                                                        <ItemTemplate>
                                                                            <asp:CheckBox ID="chkHoliday" runat="server" Enabled="false" />
                                                                        </ItemTemplate>
                                                                    </asp:TemplateField>
                                                                </Columns>
                                                                <AlternatingRowStyle CssClass="InvgridAltRow" />
                                                                <HeaderStyle CssClass="Invgridheader" BorderStyle="Solid" BorderColor="#6E6E6E" BorderWidth="1px" />
                                                                <PagerStyle CssClass="Invgridheader" />
                                                                <RowStyle CssClass="Invgridrow" HorizontalAlign="Center" />
                                                            </asp:GridView>
                                                        </asp:Panel>
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
    <asp:UpdateProgress ID="UpdateProgress1" runat="server" AssociatedUpdatePanelID="UpdatePanel1">
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
