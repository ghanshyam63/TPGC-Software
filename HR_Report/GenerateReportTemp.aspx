<%@ Page Language="C#" MasterPageFile="~/ERPMaster.master" AutoEventWireup="true" CodeFile="GenerateReportTemp.aspx.cs" Inherits="Temp_Report_GenerateReportTemp" Title="Untitled Page" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <link href="../CSS/InvStyle.css" rel="stylesheet" type="text/css" /><asp:UpdatePanel ID="UpdatePanel2" runat="server" UpdateMode="Conditional">
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
                                            <td width="150px" style="padding-left: 5px">
                                                <asp:Label ID="lblHeader" runat="server" Text="<%$ Resources:Attendance,Generate Report %>"
                                                    CssClass="LableHeaderTitle"></asp:Label>
                                            </td>
                                        </tr>
                                    </table>
                                </td>
                                <td align="right">
                                    <table cellpadding="0" cellspacing="0">
                                        <tr>
                                            <td>
                                                <asp:Panel ID="pnlLeave" runat="server" CssClass="a" Width="100%">
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
                                                        <table width="100%" style="padding-left: 20px; height: 38px;">
                                                           
                                                            <tr>
                                                                <td align='<%= Common.ChangeTDForDefaultLeft()%>' width="100px">
                                                                    <asp:Label ID="lblmonth" runat="server" Font-Size="14px" Font-Bold="true" CssClass="labelComman"
                                                                        Text="<%$ Resources:Attendance,Month %>"></asp:Label>
                                                                </td>
                                                                <td align='<%= Common.ChangeTDForDefaultLeft()%>' width="100px">
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
                                                                <td>
                                                                </td>
                                                                <td align='<%= Common.ChangeTDForDefaultLeft()%>' style="width: 165px">
                                                                    <asp:Label ID="lblyeay" runat="server" Font-Size="14px" Font-Bold="true" CssClass="labelComman"
                                                                        Text="<%$ Resources:Attendance,Year %>"></asp:Label>
                                                                </td>
                                                                <td align='<%= Common.ChangeTDForDefaultLeft()%>' width="100px">
                                                                    <asp:TextBox ID="TxtYear" runat="server" Width="150px" TabIndex="6" CssClass="textComman">2013</asp:TextBox>
                                                                </td>
                                                            </tr>
                                                             <tr>
                                                                <td align='<%= Common.ChangeTDForDefaultLeft()%>' colspan="5">
                                                                    <asp:Button ID="btnreports" runat="server" CssClass="buttonCommman"
                                                                        TabIndex="8" Text="<%$ Resources:Attendance,Generate Report %>" ValidationGroup="leavesave"
                                                                        Width="130px" OnClick="btnreports_Click" />
                                                                    <asp:Button ID="btnnonsettlereport" runat="server" CssClass="buttonCommman" 
                                                                        OnClick="btnnonsettlereport_Click" TabIndex="8" 
                                                                        Text="<%$ Resources:Attendance,Non Adjust Report %>" 
                                                                        ValidationGroup="leavesave" Width="130px" />
                                                                    <asp:Button ID="btnallowancereport" runat="server" CssClass="buttonCommman" 
                                                                        OnClick="btnallowancereport_Click" TabIndex="5" 
                                                                        Text="<%$ Resources:Attendance,Allowance Report %>" ValidationGroup="leavesave" 
                                                                        Width="130px" />
                                                                    <asp:Button ID="btnByAllowance" runat="server" CssClass="buttonCommman" 
                                                                        OnClick="btnByAllowance_Click" TabIndex="6" 
                                                                        Text="<%$ Resources:Attendance,ByAllowance Report %>" 
                                                                        ValidationGroup="leavesave" Width="130px" />
                                                                    <asp:Button ID="btndeductionreport" runat="server" CssClass="buttonCommman" 
                                                                        OnClick="btndeductionreport_Click" TabIndex="5" 
                                                                        Text="<%$ Resources:Attendance,Deduction Report %>" ValidationGroup="leavesave" 
                                                                        Width="130px" />
                                                                    <asp:Button ID="btndeuctionbydeduction" runat="server" CssClass="buttonCommman" 
                                                                        OnClick="btndeuctionbydeduction_Click" TabIndex="5" 
                                                                        Text="<%$ Resources:Attendance,ByDeduction Report %>" 
                                                                        ValidationGroup="leavesave" Width="130px" />
                                                                         <asp:Button ID="btnemppayslip" runat="server" CssClass="buttonCommman" 
                                                                        OnClick="btnemppayslip_Click" TabIndex="5" 
                                                                        Text="<%$ Resources:Attendance,Pay Slip Report %>" 
                                                                        ValidationGroup="leavesave" Width="130px" />
                                                                </td>
                                                            </tr>
                                                            <tr>
                                                            <td colspan="4">
                                                                <asp:Button ID="btnAttendReport" runat="server" CssClass="buttonCommman" 
                                                                        OnClick="btnAttendReport_Click" TabIndex="8" 
                                                                        Text="<%$ Resources:Attendance,Employee Attendance Report %>" 
                                                                        ValidationGroup="leavesave" Width="190px" />
                                                                       
                                                                        <asp:Button ID="btntotalsalreport" runat="server" CssClass="buttonCommman" 
                                                                        OnClick="btntotalsalreport_Click" TabIndex="8" 
                                                                        Text="<%$ Resources:Attendance,Employee Salary Report %>" 
                                                                        ValidationGroup="leavesave" Width="150px" />
                                                                        
                                                            
                                                            </td>
                                                            </tr>
                                                            
                                                            <tr>
                                                                <td align='<%= Common.ChangeTDForDefaultLeft()%>' width="100px">
                                                                    <asp:Label ID="lblsumallow1" runat="server" Text="" CssClass="labelComman" Visible="false"></asp:Label>
                                                                </td>
                                                                <td align='<%= Common.ChangeTDForDefaultLeft()%>' width="100px">
                                                                    <asp:Label ID="lblsumdeduc1" runat="server" Text="" CssClass="labelComman" Visible="false"></asp:Label>
                                                                </td>
                                                                <td align='<%= Common.ChangeTDForDefaultLeft()%>' width="100px">
                                                                    <asp:Label ID="lblsum" runat="server" Text="" CssClass="labelComman" Visible="False"></asp:Label>
                                                                </td>
                                                                <td align='<%= Common.ChangeTDForDefaultLeft()%>' style="width: 165px">
                                                                    <asp:Label ID="lblpenaltyshow" runat="server" Text="" CssClass="labelComman" Visible="False"></asp:Label>
                                                                </td>
                                                            </tr>
                                                            <tr>
                                                                <td colspan="5">
                                                                    </div>
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
            <asp:Panel ID="pnlpayroll" runat="server" Visible="true">
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
