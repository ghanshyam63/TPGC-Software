<%@ Page Language="C#" MasterPageFile="~/ERPMaster.master" AutoEventWireup="true" CodeFile="ManualAttendance.aspx.cs" Inherits="Attendance_ManualAttendance" Title="Untitled Page" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">

<link href="../CSS/InvStyle.css" rel="stylesheet" type="text/css" />
    <asp:UpdatePanel ID="UpdatePanel1" runat="server">
        <Triggers>
            <asp:PostBackTrigger ControlID="btnList" />
            <asp:PostBackTrigger ControlID="btnNew" />
          
          
            
        </Triggers>
        <ContentTemplate>
            <table width="100%">
                <tr >
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
                                                <asp:Label ID="lblHeader" runat="server" Text="<%$ Resources:Attendance,Manual Attendance Setup %>"
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
                                           
                                        </tr>
                                    </table>
                                </td>
                            </tr>
                            <tr>
                                <td bgcolor="#ccddee" colspan="4" width="100%" height="500px" valign="top">

<asp:Panel ID="pnlAttList" runat="server" >
<table >
<tr>
<td style="padding-left:30px;" width="100px"   align="<%= Common.ChangeTDForDefaultLeft()%>">
    <asp:Label ID="Label4" runat="server" CssClass="labelComman" Text="<%$ Resources:Attendance,From Date %>"></asp:Label>

</td>
<td width="1px">
:
</td>

<td width="100px"  align="<%= Common.ChangeTDForDefaultLeft()%>">
                                                                    <asp:TextBox ID="txtFromDate" runat="server" Width="130px" CssClass="labelComman"></asp:TextBox>
                                   
                                                                   <cc1:CalendarExtender 
                                                                    ID="CalendarExtender1"  Format="dd-MMM-yyyy"  runat="server" Enabled="True" TargetControlID="txtFromDate">
                                                                </cc1:CalendarExtender>
                                                                </td>
<td width="100px"  align="<%= Common.ChangeTDForDefaultLeft()%>">
    <asp:Label ID="Label5" runat="server" CssClass="labelComman" Text="<%$ Resources:Attendance,To Date %>"></asp:Label>
</td>
<td  width="1px">
:
</td>
<td>
<td width="200px" align="<%= Common.ChangeTDForDefaultLeft()%>">
                                                                    <asp:TextBox ID="txtToDate" runat="server" Width="130px" CssClass="labelComman"></asp:TextBox>
                                   
                                                                   <cc1:CalendarExtender 
                                                                    ID="CalendarExtender2"  Format="dd-MMM-yyyy"  runat="server" Enabled="True" TargetControlID="txtToDate">
                                                                </cc1:CalendarExtender>
                                                                </td>
<td  width="100px"  align="<%= Common.ChangeTDForDefaultLeft()%>">

<asp:Button ID="Button1" runat="server" OnClick="btnSearch_Click" Text="<%$ Resources:Attendance,Search %>"
                                                                    CssClass="buttonCommman" Visible="true" />
</td>


<td  width="100px"  align="<%= Common.ChangeTDForDefaultLeft()%>">

<asp:Button ID="Button2" runat="server" OnClick="btnResetLog_Click" Text="<%$ Resources:Attendance,Reset %>"
                                                                    CssClass="buttonCommman" Visible="true" />
</td>

</tr>

</table>

<asp:Panel ID="p1" runat="server" DefaultButton="ImgBtnBind1">
<div width="100%" style="background-image: url(../Images/bg_repeat.jpg); background-repeat: repeat;">
                                                            <table width="100%" style="padding-left: 20px; height: 38px;">
                                                                <tr>
                                                                    <td width="90px">
                                                                        <asp:Label ID="Label3" runat="server" CssClass="labelComman" Text="<%$ Resources:Attendance,Select Option %>"></asp:Label>
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
                                                                        <asp:TextBox ID="txtVal1" runat="server" CssClass="textCommanSearch" Height="14px"
                                                                            Width="100%" />
                                                                    </td>
                                                                    <td width="50px" align="center">
                                                                        <asp:ImageButton ID="ImgBtnBind1" runat="server" CausesValidation="False" Height="25px"
                                                                            ImageUrl="~/Images/search.png" Width="25px" OnClick="btnarybind_Click" ToolTip="<%$ Resources:Attendance,Search %>" />
                                                                    </td>
                                                                    <td width="30px" align="center">
                                                                        <asp:Panel ID="Panel1" runat="server" DefaultButton="ImageButton9">
                                                                            <asp:ImageButton ID="ImgBtnRefresh1" runat="server" CausesValidation="False" Height="25px"
                                                                                ImageUrl="~/Images/refresh.png" Width="25px" OnClick="btnaryRefresh_Click"
                                                                                ToolTip="<%$ Resources:Attendance,Refresh %>"></asp:ImageButton>
                                                                        </asp:Panel>
                                                                    </td>
                                                                    <td width="30px" align="center">
                                                                    </td>
                                                                    <td width="30px" align="center">
                                                                    </td>
                                                                    <td>
                                                                        <asp:ImageButton ID="ImageButton3" runat="server" OnClick="ImgbtnSelectAll_Click1"
                                                                            ToolTip="<%$ Resources:Attendance, Select All %>" AutoPostBack="true" ImageUrl="~/Images/selectAll.png" />
                                                                    </td>
                                                                    
                                                                     <td>
                                                                        <asp:ImageButton ID="ImageButton1" runat="server" OnClick="ImgbtnDeleteAll_Click1"
                                                                            ToolTip="<%$ Resources:Attendance, Delete %>" AutoPostBack="true" ImageUrl="~/Images/Erase.png" />
                                                                    </td>
                                                                    <td align="center">
                                                                        <asp:Label ID="lblTotalRecord1" runat="server" Text="<%$ Resources:Attendance,Total Records: 0 %>"
                                                                            CssClass="labelComman"></asp:Label>
                                                                    </td>
                                                                </tr>
                                                            </table>
                                                        </div>

</asp:Panel>
  
  
                                                        <asp:Label ID="lblSelectRecord1" runat="server" Visible="false"></asp:Label>
                                                        <asp:GridView ID="gvEmpLog" runat="server" AllowPaging="True" AutoGenerateColumns="False"
                                                            OnPageIndexChanging="gvEmpLog_PageIndexChanging" CssClass="grid" Width="100%"
                                                            DataKeyNames="Trans_Id" PageSize="<%# SystemParameter.GetPageSize() %>" Visible="true">
                                                            <Columns>
                                                                <asp:TemplateField>
                                                                    <ItemTemplate>
                                                                        <asp:CheckBox ID="chkgvSelect" runat="server" AutoPostBack="true" OnCheckedChanged="chkgvSelect_CheckedChanged1" />
                                                                    </ItemTemplate>
                                                                    <HeaderTemplate>
                                                                        <asp:CheckBox ID="chkgvSelectAll" runat="server" OnCheckedChanged="chkgvSelectAll_CheckedChanged1"
                                                                            AutoPostBack="true" />
                                                                    </HeaderTemplate>
                                                                    <ItemStyle HorizontalAlign="Center" />
                                                                </asp:TemplateField>
                                                                <%--<asp:TemplateField HeaderText="<%$ Resources:Attendance,Edit %>">
                                                                    <ItemTemplate>
                                                                        <asp:ImageButton ID="btnEdit" runat="server" CausesValidation="False" CommandArgument='<%# Eval("Emp_Id") %>'
                                                                            ImageUrl="~/Images/edit.png" Visible="true" OnCommand="btnEditary_Command"
                                                                            Width="16px" />
                                                                    </ItemTemplate>
                                                                    <ItemStyle HorizontalAlign="Center" />
                                                                </asp:TemplateField>--%>
                                                                <asp:TemplateField HeaderText="<%$ Resources:Attendance,Employee Code %>">
                                                                    <ItemTemplate>
                                                                        <asp:Label ID="lblEmpCode" runat="server" Text='<%# Eval("Emp_Code") %>'></asp:Label>
                                                                        <asp:Label ID="lblTransId" Visible="false" runat="server" Text='<%# Eval("Trans_Id") %>'></asp:Label>
                                                                    </ItemTemplate>
                                                                    <ItemStyle CssClass="grid" />
                                                                </asp:TemplateField>
                                                                <asp:BoundField DataField="Emp_Name" HeaderText="<%$ Resources:Attendance,Employee Name %>"
                                                                    SortExpression="Emp_Name" ItemStyle-CssClass="grid" ItemStyle-Width="40%" />
                                                                <asp:TemplateField HeaderText="<%$ Resources:Attendance,Department %>">
                                                                    <ItemTemplate>
                                                                        <asp:Label ID="lblProductCode" runat="server" Text='<%# Eval("Dep_Name") %>'></asp:Label>
                                                                    </ItemTemplate>
                                                                    <ItemStyle CssClass="grid" />
                                                                </asp:TemplateField>
                                                                <asp:TemplateField HeaderText="<%$ Resources:Attendance,Date %>">
                                                                    <ItemTemplate>
                                                                        <asp:Label ID="lblItemType" runat="server" Text='<%# GetDate(Eval("Event_Date")) %>'></asp:Label>
                                                                    </ItemTemplate>
                                                                    <ItemStyle CssClass="grid" />
                                                                </asp:TemplateField>
                                                                
                                                                 <asp:TemplateField HeaderText="<%$ Resources:Attendance,Time %>">
                                                                    <ItemTemplate>
                                                                        <asp:Label ID="lblItemType1" runat="server" Text='<%# Convert.ToDateTime(Eval("Event_Time")).ToString("HH:mm") %>'></asp:Label>
                                                                    </ItemTemplate>
                                                                    <ItemStyle CssClass="grid" />
                                                                </asp:TemplateField>
                                                                 <asp:TemplateField HeaderText="<%$ Resources:Attendance,Type %>">
                                                                    <ItemTemplate>
                                                                        <asp:Label ID="lblItemType2" runat="server" Text='<%# Eval("Type") %>'></asp:Label>
                                                                    </ItemTemplate>
                                                                    <ItemStyle CssClass="grid" />
                                                                </asp:TemplateField>
                                                                 <asp:TemplateField HeaderText="<%$ Resources:Attendance,Function Key %>">
                                                                    <ItemTemplate>
                                                                        <asp:Label ID="lblItemType3" runat="server" Text='<%# Eval("Func_Code") %>'></asp:Label>
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

<asp:Panel ID="pnlEmpAtt" runat="server" >

<table width="100%">
 <tr>
                                                <td align='<%= Common.ChangeTDForDefaultLeft()%>'>
                                                    <asp:RadioButton ID="rbtnGroupSal" CssClass="labelComman" OnCheckedChanged="EmpGroupSal_CheckedChanged"
                                                        runat="server" Text="<%$ Resources:Attendance,Group %>" Font-Bold="true" GroupName="EmpGroupSal"
                                                        AutoPostBack="true" />
                                                    <asp:RadioButton ID="rbtnEmpSal" runat="server" CssClass="labelComman" AutoPostBack="true"
                                                        Text="<%$ Resources:Attendance,Employee %>" GroupName="EmpGroupSal" Font-Bold="true"
                                                        OnCheckedChanged="EmpGroupSal_CheckedChanged" />
                                                        <asp:Label ID="lblEmp" runat="server" ></asp:Label>
                                                </td>
                                            </tr>
                                            <tr>
                                            <td>
                                            <asp:Panel ID="pnlGroupSal" runat="server">
                                                        <table width="100%">
                                                            <tr>
                                                                <td>
                                                                    <table width="100%">
                                                                        <tr>
                                                                            <td valign="top" align='<%= Common.ChangeTDForDefaultLeft()%>' width="170px">
                                                                                <asp:ListBox ID="lbxGroupSal" runat="server" Height="211px" Width="171px" SelectionMode="Multiple"
                                                                                    AutoPostBack="true" OnSelectedIndexChanged="lbxGroupSal_SelectedIndexChanged"
                                                                                    CssClass="list" Font-Names="Trebuchet MS" Font-Size="Small" ForeColor="Gray">
                                                                                </asp:ListBox>
                                                                            </td>
                                                                            <td valign="top" align='<%= Common.ChangeTDForDefaultLeft()%>'>
                                                                                <asp:GridView ID="gvEmployeeSal" runat="server" AllowPaging="True" AutoGenerateColumns="False"
                                                                                    OnPageIndexChanging="gvEmployeeSal_PageIndexChanging" CssClass="grid" Width="100%"
                                                                                    PageSize="<%# SystemParameter.GetPageSize() %>">
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
                                                                    <asp:Label ID="Label52" runat="server" Visible="false"></asp:Label>
                                                                </td>
                                                            </tr>
                                                        </table>
                                                    </asp:Panel>
       
                                            
                                            <asp:Panel ID="pnlEmp" runat="server" DefaultButton="ImageButton8">


                                                        <div width="100%" style="background-image: url(../Images/bg_repeat.jpg); background-repeat: repeat;">
                                                            <table width="100%" style="padding-left: 20px; height: 38px;">
                                                                <tr>
                                                                    <td width="90px">
                                                                        <asp:Label ID="Label24" runat="server" CssClass="labelComman" Text="<%$ Resources:Attendance,Select Option %>"></asp:Label>
                                                                    </td>
                                                                    <td width="170px">
                                                                        <asp:DropDownList ID="ddlField" runat="server" CssClass="DropdownSearch" Height="25px"
                                                                            Width="165px">
                                                                            <asp:ListItem Text="<%$ Resources:Attendance,Employee Code %>" Value="Emp_Code"></asp:ListItem>
                                                                            <asp:ListItem Text="<%$ Resources:Attendance,Employee Name %>" Value="Emp_Name" Selected="True"></asp:ListItem>
                                                                        </asp:DropDownList>
                                                                    </td>
                                                                    <td width="125px">
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
                                                                            Width="100%" />
                                                                    </td>
                                                                    <td width="50px" align="center">
                                                                        <asp:ImageButton ID="ImageButton8" runat="server" CausesValidation="False" Height="25px"
                                                                            ImageUrl="~/Images/search.png" Width="25px" OnClick="btnarybind_Click1" ToolTip="<%$ Resources:Attendance,Search %>" />
                                                                    </td>
                                                                    <td width="30px" align="center">
                                                                        <asp:Panel ID="Panel15" runat="server" DefaultButton="ImageButton9">
                                                                            <asp:ImageButton ID="ImageButton9" runat="server" CausesValidation="False" Height="25px"
                                                                                ImageUrl="~/Images/refresh.png" Width="25px" OnClick="btnaryRefresh_Click1"
                                                                                ToolTip="<%$ Resources:Attendance,Refresh %>"></asp:ImageButton>
                                                                        </asp:Panel>
                                                                    </td>
                                                                    <td width="30px" align="center">
                                                                    </td>
                                                                    <td width="30px" align="center">
                                                                    </td>
                                                                    <td>
                                                                        <asp:ImageButton ID="ImageButton10" runat="server" OnClick="ImgbtnSelectAll_Clickary"
                                                                            ToolTip="<%$ Resources:Attendance, Select All %>" AutoPostBack="true" ImageUrl="~/Images/selectAll.png" />
                                                                    </td>
                                                                    <td align="center">
                                                                        <asp:Label ID="lblTotalRecord" runat="server" Text="<%$ Resources:Attendance,Total Records: 0 %>"
                                                                            CssClass="labelComman"></asp:Label>
                                                                    </td>
                                                                </tr>
                                                            </table>
                                                        </div>
                                                        <asp:Label ID="lblSelectRecord" runat="server" Visible="false"></asp:Label>
                                                        <asp:GridView ID="gvEmployee" runat="server" AllowPaging="True" AutoGenerateColumns="False"
                                                            OnPageIndexChanging="gvEmp_PageIndexChanging" CssClass="grid" Width="100%"
                                                            DataKeyNames="Emp_Id" PageSize="<%# SystemParameter.GetPageSize() %>" Visible="true">
                                                            <Columns>
                                                                <asp:TemplateField>
                                                                    <ItemTemplate>
                                                                        <asp:CheckBox ID="chkgvSelect" runat="server" AutoPostBack="true" OnCheckedChanged="chkgvSelect_CheckedChanged" />
                                                                    </ItemTemplate>
                                                                    <HeaderTemplate>
                                                                        <asp:CheckBox ID="chkgvSelectAll" runat="server" OnCheckedChanged="chkgvSelectAll_CheckedChanged"
                                                                            AutoPostBack="true" />
                                                                    </HeaderTemplate>
                                                                    <ItemStyle HorizontalAlign="Center" />
                                                                </asp:TemplateField>
                                                                <%--<asp:TemplateField HeaderText="<%$ Resources:Attendance,Edit %>">
                                                                    <ItemTemplate>
                                                                        <asp:ImageButton ID="btnEdit" runat="server" CausesValidation="False" CommandArgument='<%# Eval("Emp_Id") %>'
                                                                            ImageUrl="~/Images/edit.png" Visible="true" OnCommand="btnEditary_Command"
                                                                            Width="16px" />
                                                                    </ItemTemplate>
                                                                    <ItemStyle HorizontalAlign="Center" />
                                                                </asp:TemplateField>--%>
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
                                                        <table width="100%">
                                                        <tr>
                                                        <td style="padding-left:10px;" align="<%= Common.ChangeTDForDefaultLeft()%>">
                                                                    <asp:Label ID="Label27" runat="server" CssClass="labelComman" Text="<%$ Resources:Attendance,Date %>"></asp:Label>
                                                                </td>
                                                                <td align="<%= Common.ChangeTDForDefaultLeft()%>">
                                                                    :
                                                                </td>
                                                                <td align="<%= Common.ChangeTDForDefaultLeft()%>">
                                                                    <asp:TextBox ID="txtDate" runat="server" Width="143px" CssClass="labelComman"></asp:TextBox>
                                   
                                                                   <cc1:CalendarExtender 
                                                                    ID="txtFrom_CalendarExtender"  Format="dd-MMM-yyyy"  runat="server" Enabled="True" TargetControlID="txtDate">
                                                                </cc1:CalendarExtender>
                                                                </td>
                                                                <td align="<%= Common.ChangeTDForDefaultLeft()%>">
                                                                    <asp:Label ID="Label28" runat="server" CssClass="labelComman" Text="<%$ Resources:Attendance,Time %>"></asp:Label>
                                                                </td>
                                                                <td align="<%= Common.ChangeTDForDefaultLeft()%>">
                                                                    :
                                                                </td>
                                                                <td align="<%= Common.ChangeTDForDefaultLeft()%>">
                                                                     <asp:TextBox ID="txtOnDuty" runat="server" Width="130px" Font-Names="Verdana" Font-Size="14px"></asp:TextBox>
                                                           
                                                                   
                                                                   
                                                                    <cc1:MaskedEditExtender ID="txtOnDuty_MaskedEditExtender" runat="server" CultureAMPMPlaceholder=""
                                                                    CultureCurrencySymbolPlaceholder="" CultureDateFormat="" CultureDatePlaceholder=""
                                                                    CultureDecimalPlaceholder="" CultureThousandsPlaceholder="" CultureTimePlaceholder=""
                                                                    Enabled="True" ErrorTooltipEnabled="True" Mask="99:99" MaskType="Time" TargetControlID="txtOnDuty"
                                                                    UserTimeFormat="TwentyFourHour">
                                                                </cc1:MaskedEditExtender>
                                                                </td>
                                                            </tr>
                                                         
                                                         
                                                         
                                                         <tr>
                                                        <td style="padding-left:10px;" align="<%= Common.ChangeTDForDefaultLeft()%>">
                                                                    <asp:Label ID="Label1" runat="server" CssClass="labelComman" Text="<%$ Resources:Attendance,Function Key %>"></asp:Label>
                                                                </td>
                                                                <td align="<%= Common.ChangeTDForDefaultLeft()%>">
                                                                    :
                                                                </td>
                                                                <td align="<%= Common.ChangeTDForDefaultLeft()%>">
                                                                
                                                                 <asp:DropDownList ID="ddlFunction" Width="147px" CssClass="DropdownSearch" runat="server" >
                                                                    <asp:ListItem Value="F1" Text="F1"></asp:ListItem>
                                                                    <asp:ListItem Value="F2" Text="F2"></asp:ListItem>
                                                                    <asp:ListItem Value="F3" Text="F3"></asp:ListItem>
                                                                    <asp:ListItem Value="F4" Text="F4"></asp:ListItem>
                                                                    <asp:ListItem Value="F5" Text="F5"></asp:ListItem>
                                                                    <asp:ListItem Value="F6" Text="F6"></asp:ListItem>
                                                                   
                                                                </asp:DropDownList>
                                                                    </td>
                                                                <td align="<%= Common.ChangeTDForDefaultLeft()%>">
                                                                    <asp:Label ID="Label2" runat="server" CssClass="labelComman" Text="<%$ Resources:Attendance,Type %>"></asp:Label>
                                                                </td>
                                                                <td align="<%= Common.ChangeTDForDefaultLeft()%>">
                                                                    :
                                                                </td>
                                                                <td align="<%= Common.ChangeTDForDefaultLeft()%>">
                                                                     <asp:DropDownList ID="ddlType" CssClass="DropdownSearch" runat="server"  Width="136px">
                                                                    <asp:ListItem Value="In" Text="In"></asp:ListItem>
                                                                    <asp:ListItem Value="Out" Text="Out"></asp:ListItem>
                                                                   
                                                                </asp:DropDownList>
                                                                </td>
                                                            </tr>
                                                            <tr>
                                                            <td  style="padding-left:10px;" align="<%= Common.ChangeTDForDefaultLeft()%>" >
                                                               <asp:Label ID="Label6" runat="server" CssClass="labelComman" Text="<%$ Resources:Attendance,Attendance Type %>"></asp:Label>
                                                               
                                                            </td>
                                                             <td align="<%= Common.ChangeTDForDefaultLeft()%>">
                                                            :
                                                            </td>
                                                                <td align="<%= Common.ChangeTDForDefaultLeft()%>" colspan="4" 
                                                                   ">
                                                                    
                                                                      <asp:RadioButton ID="rbtnByManual" Text="<%$ Resources:Attendance,By Manual %>"   CssClass="labelComman" runat="server" GroupName="AttType"   />
                                                                  <asp:RadioButton ID="rbtnByTour" Text="<%$ Resources:Attendance,By Tour %>" CssClass="labelComman" runat="server" GroupName="AttType"  />
                                                                  
                                                                    
                                                                    </td>
                                                            </tr>
                                                            <tr>
                                                            <td colspan="6" align="center" >
                                                            <asp:Button ID="btnSave" runat="server" OnClick="btnSave_Click" Text="<%$ Resources:Attendance,Save %>"
                                                                    CssClass="buttonCommman" Visible="true" />
                                                                     &nbsp; &nbsp;
                                                                     <asp:Button ID="btnReset" runat="server" OnClick="btnReset_Click" Text="<%$ Resources:Attendance,Reset %>"
                                                                    CssClass="buttonCommman" Visible="true" />
                                                            
                                                            </td>
                                                            
                                                            </tr>
                                                        </table>
                                            
                                            
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

