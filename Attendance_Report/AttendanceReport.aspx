<%@ Page Language="C#" MasterPageFile="~/ERPMaster.master" AutoEventWireup="true" CodeFile="AttendanceReport.aspx.cs" Inherits="AttendanceReport" Title="Untitled Page" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">

<link href="../CSS/InvStyle.css" rel="stylesheet" type="text/css" />
    <asp:UpdatePanel ID="UpdatePanel1" runat="server">
       <Triggers>
       <asp:PostBackTrigger ControlID="lnkChangeFilter" />
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
                                                <asp:Label ID="lblHeader" runat="server" Text="<%$ Resources:Attendance,Attendance Report Setup %>"
                                                    CssClass="LableHeaderTitle"></asp:Label>
                                            </td>
                                        </tr>
                                    </table>
                                </td>
                                
                            </tr>
                            <tr>
                                <td bgcolor="#ccddee" colspan="4" width="100%" height="500px" valign="top">



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
                                                          
                                            <asp:Panel ID="Panel1" runat="server" DefaultButton="btnLogProcess">

                                                        
                                                        <table width="100%">
                                                        
                                                        
                                                        <tr runat="server"  >
                                                        <td style="padding-left:10px;" align="<%= Common.ChangeTDForDefaultLeft()%>">
                                                                    <asp:Label ID="Label27" runat="server" CssClass="labelComman" Text="<%$ Resources:Attendance,From Date %>"></asp:Label>
                                                                </td>
                                                                <td align="<%= Common.ChangeTDForDefaultLeft()%>">
                                                                    :
                                                                </td>
                                                                <td align="<%= Common.ChangeTDForDefaultLeft()%>">
                                                                    <asp:TextBox  ID="txtFromDate" runat="server" Width="130px" CssClass="labelComman"></asp:TextBox>
                                   
                                                                   <cc1:CalendarExtender 
                                                                    ID="txtFrom_CalendarExtender"    runat="server" Enabled="True" TargetControlID="txtFromDate">
                                                                </cc1:CalendarExtender>
                                                                </td>
                                                                
                                                                
                                                                 <td style="padding-left:10px;" align="<%= Common.ChangeTDForDefaultLeft()%>">
                                                                    <asp:Label ID="Label3" runat="server" CssClass="labelComman" Text="<%$ Resources:Attendance,To Date %>"></asp:Label>
                                                                </td>
                                                                <td align="<%= Common.ChangeTDForDefaultLeft()%>">
                                                                    :
                                                                </td>
                                                                <td align="<%= Common.ChangeTDForDefaultLeft()%>">
                                                                    <asp:TextBox ID="txtToDate" runat="server" Width="130px" CssClass="labelComman"></asp:TextBox>
                                   
                                                                   <cc1:CalendarExtender 
                                                                    ID="CalendarExtender1"    runat="server" Enabled="True" TargetControlID="txtToDate">
                                                                </cc1:CalendarExtender>
                                                                </td>
                                                            </tr>
                                                         
                                                         
                                                         
                                                         
                                                            <tr>
                                                            <td colspan="6" align="center" >
                                                            <asp:Button ID="btnLogProcess"  runat="server" OnClick="btnGenerate_Click" Text="<%$ Resources:Attendance,Next %>"
                                                                    CssClass="buttonCommman" Visible="true" />
                                                                     &nbsp; &nbsp;
                                                                     <asp:Button ID="btnReset" runat="server" OnClick="btnReset_Click" Text="<%$ Resources:Attendance,Reset %>"
                                                                    CssClass="buttonCommman" Visible="true" />
                                                                    
                                                                     &nbsp; &nbsp;  
                                                           
                                                            </td>
                                                            
                                                            </tr>
                                                        </table>
                                                        </asp:Panel>
                                            
                                            
                                            </td>
                                            </tr>
                                            
                                            
                                            </table>
                                   
                                                        
                                                    </asp:Panel>
 
 <asp:Panel ID="pnlReport" runat="server" >
                                            <table width="100%">
                        <tr>
                            <td style="padding-left:10px;" align='<%= Common.ChangeTDForDefaultLeft()%>'>
                                <asp:LinkButton ID="lnkChangeFilter" runat="server"  Font-Size="18px" CssClass="acc" Text="<%$ Resources:Attendance,Change Filter Criteria%>"
                                    OnClick="lnkChangeFilter_Click"></asp:LinkButton>
                            </td>
                            </tr>
                            <tr>
                            <td>
                             <table width="100%">
                                <tr>
                                <td align='<%= Common.ChangeTDForDefaultLeft()%>'>
                                        <fieldset style="height: 180px">
                                            <legend>
                                                <asp:Label ID="Label6" Font-Bold="true"  CssClass="labelComman" runat="server" Text="<%$ Resources:Attendance,General Report%>"></asp:Label></legend>
                                            <table width="230px">
                                                <tr>
                                                    <td align='<%= Common.ChangeTDForDefaultLeft()%>'>
                                                        <asp:LinkButton ID="lnkShiftReport"  CssClass="acc" PostBackUrl="~/Attendance_Report/ShiftReport.aspx"
                                                            Text="<%$ Resources:Attendance,Shift Report%>" runat="server"></asp:LinkButton>
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td align='<%= Common.ChangeTDForDefaultLeft()%>'>
                                                        <asp:LinkButton ID="LinkButton17"   CssClass="acc" PostBackUrl="~/Attendance_Report/HolidayReport.aspx"
                                                            Text="<%$ Resources:Attendance,Holiday Report%>" runat="server"></asp:LinkButton>
                                                    </td>
                                                </tr>
                                                <tr>
                                                   <td align='<%= Common.ChangeTDForDefaultLeft()%>'>
                                                        <asp:LinkButton ID="lnkHolidayReport"  CssClass="acc"  PostBackUrl="~/Attendance_Report/WeekoffReport.aspx"
                                                            Text="<%$ Resources:Attendance,Week Off Report%>" runat="server"></asp:LinkButton>
                                                    </td>
                                                </tr>
                                                
                                            </table>
                                        </fieldset>
                                    </td>
                                 
                                 
                                 <td align='<%= Common.ChangeTDForDefaultLeft()%>'>
                                        <fieldset style="height: 180px">
                                            <legend>
                                                <asp:Label ID="Label1" Font-Bold="true" CssClass="labelComman" runat="server" Text="<%$ Resources:Attendance,Detail Report%>"></asp:Label></legend>
                                            <table>
                                                <tr>
                                                    <td width="200px" align='<%= Common.ChangeTDForDefaultLeft()%>'>
                                                        <asp:LinkButton ID="LinkButton1" CssClass="acc" PostBackUrl="~/Attendance_Report/InOutReport.aspx"
                                                            Text="<%$ Resources:Attendance,In Out Report%>" runat="server"></asp:LinkButton>
                                                    </td>
                                                    <td width="200px" align='<%= Common.ChangeTDForDefaultLeft()%>'>
                                                    
                                                    <asp:LinkButton ID="LinkButton16" CssClass="acc" PostBackUrl="~/Attendance_Report/LeaveReport.aspx"
                                                            Text="<%$ Resources:Attendance,Leave Report %>" runat="server"></asp:LinkButton>
                                                    </td>
                                                </tr>
                                                
                                                <tr>
                                                    <td align='<%= Common.ChangeTDForDefaultLeft()%>'>
                                                        <asp:LinkButton ID="LinkButton3" CssClass="acc" PostBackUrl="~/Attendance_Report/LateReport.aspx"
                                                            Text="<%$ Resources:Attendance,Late In Report%>" runat="server"></asp:LinkButton>
                                                    </td>
                                                   <td align='<%= Common.ChangeTDForDefaultLeft()%>'>
                                                   
                                                    <asp:LinkButton ID="LinkButton18" CssClass="acc" PostBackUrl="~/Attendance_Report/LeaveStatusReport.aspx"
                                                            Text="<%$ Resources:Attendance,Leave Status Report %>" runat="server"></asp:LinkButton>
                                             
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td align='<%= Common.ChangeTDForDefaultLeft()%>'>
                                                        <asp:LinkButton ID="LinkButton4" CssClass="acc" PostBackUrl="~/Attendance_Report/EarlyOutReport.aspx"
                                                            Text="<%$ Resources:Attendance,Early Out Report%>" runat="server"></asp:LinkButton>
                                                    </td>
                                                    
                                                      <td align='<%= Common.ChangeTDForDefaultLeft()%>'>
                                                          <asp:LinkButton ID="LinkButton9" CssClass="acc" PostBackUrl="~/Attendance_Report/LeaveRemainingReport.aspx"
                                                            Text="<%$ Resources:Attendance,Leave Remaining Report %>" runat="server"></asp:LinkButton>
                                               
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td align='<%= Common.ChangeTDForDefaultLeft()%>'> 
                                                        <asp:LinkButton ID="LinkButton5" CssClass="acc" PostBackUrl="~/Attendance_Report/AbsentReport.aspx"
                                                            Text="<%$ Resources:Attendance,Absent Report%>" runat="server"></asp:LinkButton>
                                                    </td>
                                                    
                                                      <td align='<%= Common.ChangeTDForDefaultLeft()%>'>
                                                            <asp:LinkButton ID="LinkButton7" CssClass="acc" PostBackUrl="~/Attendance_Report/PartialLeaveReport.aspx"
                                                            Text="<%$ Resources:Attendance,Partial Leave Report %>" runat="server"></asp:LinkButton>
                                                
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td align='<%= Common.ChangeTDForDefaultLeft()%>'>
                                                        <asp:LinkButton ID="LinkButton6" CssClass="acc" PostBackUrl="~/Attendance_Report/OverTimeReport.aspx"
                                                            Text="<%$ Resources:Attendance,Over Time Report%>" runat="server"></asp:LinkButton>
                                                    </td>
                                                    
                                                      <td align='<%= Common.ChangeTDForDefaultLeft()%>'>
                                                       <asp:LinkButton ID="LinkButton8" CssClass="acc" PostBackUrl="~/Attendance_Report/PartialViolaionReport.aspx"
                                                            Text="<%$ Resources:Attendance,Partial Violation Report %>" runat="server"></asp:LinkButton>
                                                
                                                    </td>
                                                </tr>
                                                <tr>
                                                     <td align='<%= Common.ChangeTDForDefaultLeft()%>'>
                                                        <asp:LinkButton ID="LinkButton2" CssClass="acc"  PostBackUrl="~/Attendance_Report/DailySalaryReport.aspx"
                                                            Text="<%$ Resources:Attendance,Daily Salary Report%>" runat="server"></asp:LinkButton>
                                                    </td>
                                                      <td align='<%= Common.ChangeTDForDefaultLeft()%>'>
                                                             <asp:LinkButton ID="LinkButton13" CssClass="acc"  PostBackUrl="~/Attendance_Report/UserTransferReport.aspx"
                                                            Text="<%$ Resources:Attendance,User Transfer Report%>" runat="server"></asp:LinkButton>
                                              
                                                      
                                                    </td>
                                                </tr>
                                                
                                                 <tr>
                                                     <td align='<%= Common.ChangeTDForDefaultLeft()%>'>
                                                        <asp:LinkButton ID="LinkButton14" CssClass="acc"  PostBackUrl="~/Attendance_Report/MailTransactionReport.aspx"
                                                            Text="<%$ Resources:Attendance,Mail Transaction Report%>" runat="server"></asp:LinkButton>
                                                    </td>
                                                      <td align='<%= Common.ChangeTDForDefaultLeft()%>'>
                                                             <asp:LinkButton ID="LinkButton19" CssClass="acc"  PostBackUrl="~/Attendance_Report/SMSTransactionReport.aspx"
                                                            Text="<%$ Resources:Attendance,SMS Transaction Report%>" runat="server"></asp:LinkButton>
                                              
                                                      
                                                    </td>
                                                </tr>
                                                
                                                 
                                            </table>
                                        </fieldset>
                                    </td>
                                </tr>
                                
                                 <tr>
                                    <td align='<%= Common.ChangeTDForDefaultLeft()%>'>
                                        <fieldset style="height: 90px">
                                            <legend>
                                                <asp:Label ID="Label2" Font-Bold="true" CssClass="labelComman" runat="server" Text="<%$ Resources:Attendance,Chart Report%>"></asp:Label></legend>
                                            <table>
                                                <tr>
                                                    <td align='<%= Common.ChangeTDForDefaultLeft()%>'>
                                                        <asp:LinkButton ID="LinkButton10" CssClass="acc" PostBackUrl="~/Attendance_Report/AttendanceRegister.aspx"
                                                            Text="<%$ Resources:Attendance,Attendance Register%>" runat="server"></asp:LinkButton>
                                                    </td>
                                                </tr>
                                            </table>
                                        </fieldset>
                                    </td>
                                    <td align='<%= Common.ChangeTDForDefaultLeft()%>'>
                                        <fieldset style="height: 90px">
                                            <legend>
                                                <asp:Label ID="Label82" Font-Bold="true" CssClass="labelComman" runat="server" Text="<%$ Resources:Attendance,Summary Report%>"></asp:Label></legend>
                                            <table>
                                            
                                             <tr>
                                                     <td align='<%= Common.ChangeTDForDefaultLeft()%>'>
                                                        <asp:LinkButton ID="LinkButton15" CssClass="acc" PostBackUrl="~/Attendance_Report/LogReport.aspx"
                                                            Text="<%$ Resources:Attendance,Log Report %>" runat="server"></asp:LinkButton>
                                                    </td>
                                                </tr>
                                            <tr>
                                                     <td align='<%= Common.ChangeTDForDefaultLeft()%>'>
                                                        <asp:LinkButton ID="LinkButton20" CssClass="acc" PostBackUrl="~/Attendance_Report/SummaryReport.aspx"
                                                            Text="<%$ Resources:Attendance,Attendance Summary Report %>" runat="server"></asp:LinkButton>
                                                    </td>
                                                </tr>
                                                <tr>
                                                     <td align='<%= Common.ChangeTDForDefaultLeft()%>'>
                                                        <asp:LinkButton ID="LinkButton21" CssClass="acc" PostBackUrl="~/Attendance_Report/AttendanceSalaryReport.aspx"
                                                            Text="<%$ Resources:Attendance,Attendance Salary Report %>" runat="server"></asp:LinkButton>
                                                    </td>
                                                </tr>
                                                
                                                
                                            </table>
                                        </fieldset>
                                    </td>
                                </tr>
                                
                                      <tr>
                                    <td align='<%= Common.ChangeTDForDefaultLeft()%>'>
                                        <fieldset style="height: 70px">
                                            <legend>
                                                <asp:Label ID="Label4" Font-Bold="true" CssClass="labelComman" runat="server" Text="<%$ Resources:Attendance,Dril Report %>"></asp:Label></legend>
                                            <table>
                                                <tr>
                                                    <td align='<%= Common.ChangeTDForDefaultLeft()%>'>
                                                        <asp:LinkButton ID="LinkButton11" CssClass="acc" PostBackUrl="~/Attendance_Report/EmployeeReport.aspx"
                                                            Text="<%$ Resources:Attendance,Employee Information Report%>" runat="server"></asp:LinkButton>
                                                    </td>
                                                </tr>
                                            </table>
                                        </fieldset>
                                    </td>
                                    <td align='<%= Common.ChangeTDForDefaultLeft()%>'>
                                        <fieldset style="height: 70px">
                                            <legend>
                                                <asp:Label ID="Label5" Font-Bold="true" CssClass="labelComman" runat="server" Text="<%$ Resources:Attendance,Exception Report %>"></asp:Label></legend>
                                            <table>
                                            
                                             <tr>
                                                     <td align='<%= Common.ChangeTDForDefaultLeft()%>'>
                                                        <asp:LinkButton ID="LinkButton12" CssClass="acc" PostBackUrl="~/Attendance_Report/InOutExceptionReport.aspx"
                                                            Text="<%$ Resources:Attendance,In Out Exception Report %>" runat="server"></asp:LinkButton>
                                                    </td>
                                                </tr>
                                            
                                                
                                                
                                            </table>
                                        </fieldset>
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

