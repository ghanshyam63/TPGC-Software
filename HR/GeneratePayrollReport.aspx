<%@ Page Language="C#" MasterPageFile="~/ERPMaster.master" AutoEventWireup="true" CodeFile="GeneratePayrollReport.aspx.cs" Inherits="HR_GeneratePayrollReport" Title="Untitled Page" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">

<link href="../CSS/InvStyle.css" rel="stylesheet" type="text/css" />
    <asp:UpdatePanel ID="UpdatePanel2" runat="server" UpdateMode="Conditional">
        <Triggers>
            <asp:PostBackTrigger ControlID="btnLeave" />
             <asp:PostBackTrigger ControlID="btnSave" />
            <asp:PostBackTrigger ControlID="btnLeast" />
           <asp:PostBackTrigger ControlID="gvEmployee" />
            <asp:PostBackTrigger ControlID="gvEmpLeave" />
          
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
                                            <td width="150px" style="padding-left: 5px">
                                                <asp:Label ID="lblHeader" runat="server" Text="<%$ Resources:Attendance,Report %>"
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
                                                    <asp:Button ID="btnLeave" runat="server" Text="<%$ Resources:Attendance,Report %>"
                                                        Width="100px" BorderStyle="none" BackColor="Transparent" 
                                                        Style="padding-left: 10px; padding-top: 3px; background-image: url('../Images/appIcon-fileAClaim.png' );
                                                        background-repeat: no-repeat; height: 49px; background-position: 5px 15px; font: bold 14px Trebuchet MS;
                                                        color: #000000;" onclick="btnLeave_Click" />
                                                </asp:Panel>
                                                <td>
                                                <asp:Panel ID="PanelList" runat="server" CssClass="a" Width="100%">
                                                    <asp:Button ID="btnLeast" runat="server" Text="<%$ Resources:Attendance,List %>"
                                                        Width="80px" BorderStyle="none" BackColor="Transparent" Style="padding-top: 3px;
                                                        padding-left: 10px; background-image: url('../Images/List.png' ); background-repeat: no-repeat;
                                                        height: 49px; background-position: 5px 15px; font: bold 14px Trebuchet MS; color: #000000;" OnClick="btnLeast_Click"
                                                         />
                                                </asp:Panel>
                                            </td>
                                            </td>
                                        </tr>
                                    </table>
                                </td>
                            </tr>
                            <tr>
                                <td bgcolor="#ccddee" colspan="4" width="100%" height="500px" valign="top">
                                    <asp:Panel ID="PnlEmployeeLeave" runat="server" >
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
                                                    <asp:Panel ID="PanelInsertClaim" runat="server" Width="100%">
                                                        <table width="100%">
                                                            <tr>
                                                                <td style="padding-top: 5px;">
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
                                                                        Width="158px" >
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
                                                            <td align='<%= Common.ChangeTDForDefaultLeft()%>'>
                                                             <asp:Label ID="Label1" runat="server" CssClass="labelComman" Text="<%$ Resources:Attendance,Claim %>"></asp:Label>
                                                                
                                                            </td>
                                                            <td>
                                                            :
                                                            </td>
                                                            <td  align='<%= Common.ChangeTDForDefaultLeft()%>'>
                                                            <asp:DropDownList ID="ddlClaimType" runat="server" CssClass="textComman" TabIndex="5"
                                                                        Width="158px" >
                                                                        <asp:ListItem Text="--Select--" Value="0" />
                                                                        <asp:ListItem Text="Approved" Value="1" />
                                                                        <asp:ListItem Text="Cancelled" Value="2" />
                                                                        <asp:ListItem Text="Pending" Value="3" />
                                                                    
                                                                    </asp:DropDownList>
                                                                    </td>
                                                                    <td align='<%= Common.ChangeTDForDefaultLeft()%>'>
                                                                    <asp:Label ID="Label2" runat="server" CssClass="labelComman" Text="<%$ Resources:Attendance,Loan %>"></asp:Label>
                                                             
                                                                    
                                                                    </td>
                                                          
                                                          <td>
                                                          :
                                                          </td>
                                                          <td  align='<%= Common.ChangeTDForDefaultLeft()%>'>
                                                            <asp:DropDownList ID="Ddlloantype" runat="server" CssClass="textComman" TabIndex="5"
                                                                        Width="158px" >
                                                                        <asp:ListItem Text="--Select--" Value="0" />
                                                                        <asp:ListItem Text="Loan Detail Report" Value="1" />
                                                                        <asp:ListItem Text="Loan Installment Report" Value="2" />
                                                                        
                                                                    
                                                                    </asp:DropDownList>
                                                                    </td>
                                                            
                                                          
                                                            </tr>
                                                            <tr>
                                                             <td>
                                                           <asp:RadioButton ID="rbtnClaimType" runat="server" Text="Claim Report" GroupName="aa" />
                                                           </td>
                                                           <td>
                                                           &nbsp;
                                                           </td>
                                                           <td>
                                                            <asp:RadioButton ID="rbtnLoan" runat="server" Text="Loan Report" GroupName="aa" />
                                                                   
                                                           </td>
                                                           <td>
                                                          <asp:RadioButton ID="RbtnloanDetail" runat="server" Text="LoanDetail Report"  GroupName="aa"/>
                                                           
                                                           </td>
                                                           <td>
                                                           &nbsp;
                                                           </td>
                                                           
                                                           <td>
                                                         <asp:RadioButton ID="rbtnPenalty" runat="server" Text="Penalty Report" GroupName="aa" />
                                                         &nbsp;&nbsp;&nbsp;
                                                         <asp:RadioButton ID="RbtnDirectory" runat="server" Text="Employee Directory Report" GroupName="aa" />
                                                         
                                                           </td>
                                                            
                                                            </tr>
                                                            <tr>
                                                            <td colspan="6" align='<%= Common.ChangeTDForDefaultLeft()%>'>
                                                            <asp:RadioButton ID="Rbtndocument" runat="server" Text="Document Expiry Report" GroupName="aa" />
                                                            </td>
                                                            </tr>
                                                            <tr>
                                                                <td colspan="6" align="center" style="padding-left: 12px;">
                                                                    <asp:Button ID="btnSave" runat="server" CssClass="buttonCommman" TabIndex="7" Text="<%$ Resources:Attendance, Generate Report %>"
                                                                         ValidationGroup="leavesave" Width="150px" onclick="btnSave_Click" />
                                                                    &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                                                                    
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
                                     <asp:Panel ID="PanelSearchList" runat="server" >
                                                    <table style="width:100%; >
                                                    
                                                    <tr>
                                                    <td  style="width:200px; height;200px; border-color:White;">
                                                    <b><span style="font-size:20px; font-family:Verdana;">Claim Report</span></b>
                                                    <br />
                                                    <br />
                                                    <asp:LinkButton ID="lnkClaimApproved" runat="server" Text="Claim Approved Report" CssClass="labelComman">
                                                    </asp:LinkButton><br />
                                                    <asp:LinkButton ID="lnkClaimPending" runat="server" Text="Claim Pending Report" CssClass="labelComman"></asp:LinkButton>
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

