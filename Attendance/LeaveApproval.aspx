<%@ Page Language="C#" MasterPageFile="~/ERPMaster.master" AutoEventWireup="true" CodeFile="LeaveApproval.aspx.cs" Inherits="Attendance_LeaveApproval" Title="Untitled Page" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">

<link href="../CSS/InvStyle.css" rel="stylesheet" type="text/css" />
    <asp:UpdatePanel ID="UpdatePanel1" runat="server">
    <ContentTemplate>
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
                                                <asp:Label ID="lblHeader" runat="server" Text="<%$ Resources:Attendance,Leave Approval Setup %>"
                                                    CssClass="LableHeaderTitle"></asp:Label>
                                            </td>
                                        </tr>
                                    </table>
                                </td>
                                
                            </tr>
                            <tr>
                            <td>
                            
                            <asp:GridView ID="gvLeaveStatus" PageSize="<%# SystemParameter.GetPageSize() %>" runat="server"
                                            AutoGenerateColumns="False" Width="100%" OnPageIndexChanging="gvLeaveStatus_PageIndexChanging" AllowPaging="true" 
                                            CssClass="grid" >
                                            <Columns>
                                             
                                             <asp:TemplateField HeaderText="<%$ Resources:Attendance,ID %>" >
                                                    <ItemTemplate>
                                        
                                                        <asp:Label ID="lblEmpId"  runat="server" Text='<%# Eval("Emp_Code") %>'></asp:Label>
                                                        
                                                        
                                                    </ItemTemplate>
                                                    <ItemStyle CssClass="grid" />
                                                </asp:TemplateField>
                                             
                                             <asp:TemplateField HeaderText="<%$ Resources:Attendance,Employee Name  %>" >
                                                    <ItemTemplate>
                                        
                                                        <asp:Label ID="lblEmpName"  runat="server" Text='<%# Eval("Emp_Name") %>'></asp:Label>
                                                        
                                                        
                                                    </ItemTemplate>
                                                    <ItemStyle CssClass="grid" />
                                                </asp:TemplateField>
                                                 <asp:TemplateField HeaderText="<%$ Resources:Attendance,Leave Name %>" >
                                                    <ItemTemplate>
                                        
                                                        <asp:Label ID="lblLeaveName"  runat="server" Text='<%# Eval("Leave_Name") %>'></asp:Label>
                                                        
                                                        
                                                    </ItemTemplate>
                                                    <ItemStyle CssClass="grid" />
                                                </asp:TemplateField>
                                                
                                              <asp:TemplateField HeaderText="<%$ Resources:Attendance,Schedule Type %>" >
                                                    <ItemTemplate>
                                        
                                                        <asp:Label ID="lblScheduleType"  runat="server" Text='<%# GetScheduleType(Eval("From_Date"),Eval("Emp_Id"),Eval("Leave_Type_Id")) %>'></asp:Label>
                                                        
                                                        
                                                    </ItemTemplate>
                                                    <ItemStyle CssClass="grid" />
                                                </asp:TemplateField>
                                               <asp:TemplateField HeaderText="<%$ Resources:Attendance,Total Leave %>" >
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblTotaldays" runat="server" Text='<%# Eval("DaysCount") %>'></asp:Label>
                                                    </ItemTemplate>
                                                    <ItemStyle CssClass="grid" />
                                                </asp:TemplateField>
                                                
                                                  <asp:TemplateField HeaderText="<%$ Resources:Attendance,Apply Date %>">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblApplicatonDate" runat="server" Text='<%# GetDate(Eval("Application_Date")) %>'></asp:Label>
                                                    </ItemTemplate>
                                                    <ItemStyle CssClass="grid" />
                                                </asp:TemplateField>
                                                
                                                
                                                <asp:TemplateField HeaderText="<%$ Resources:Attendance,From Date %>">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblFromDate" runat="server" Text='<%# GetDate(Eval("From_Date")) %>'></asp:Label>
                                                    </ItemTemplate>
                                                    <ItemStyle CssClass="grid" />
                                                </asp:TemplateField>
                                                
                                                 <asp:TemplateField HeaderText="<%$ Resources:Attendance,To Date %>">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblTodays" runat="server" Text='<%# GetDate(Eval("To_Date")) %>'></asp:Label>
                                                    </ItemTemplate>
                                                    <ItemStyle CssClass="grid" />
                                                </asp:TemplateField>

                                                
                                                 <asp:TemplateField HeaderText="<%$ Resources:Attendance,Status %>" >
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblStatus" runat="server" Text='<%# GetLeaveStatus(Eval("Trans_Id"))%>' ></asp:Label>
                                                    </ItemTemplate>
                                                    <ItemStyle CssClass="grid" />
                                                </asp:TemplateField>
                                                
                                                
                                                
                                                <asp:TemplateField HeaderText="<%$ Resources:Attendance,Approve %>">
                                                    <ItemTemplate>
                                                        <asp:ImageButton ID="IbtnApprove" runat="server" CommandArgument='<%# Eval("Trans_Id") %>' CommandName='<%# Eval("Leave_Type_Id") %>'
                                                            ImageUrl="~/Images/approve.png" CausesValidation="False" OnCommand="btnApprove_Command"
                                                            Visible="true" ToolTip="<%$ Resources:Attendance,Approve %>" /></ItemTemplate>
                                                    <ItemStyle HorizontalAlign="Center" />
                                                 <ItemStyle CssClass="grid" />
                                                </asp:TemplateField>
                                                 <asp:TemplateField HeaderText="<%$ Resources:Attendance,Reject %>">
                                                    <ItemTemplate>
                                                        <asp:ImageButton ID="IbtnReject" runat="server" CausesValidation="False" CommandName='<%# Eval("Leave_Type_Id") %>'  CommandArgument='<%# Eval("Trans_Id") %>'
                                                            ImageUrl="~/Images/disapprove.png" OnCommand="IbtnReject_Command" Visible="true"
                                                            ToolTip="<%$ Resources:Attendance,Reject %>" />
                                                    </ItemTemplate>
                                                    <ItemStyle HorizontalAlign="Center" />
                                                    <ItemStyle CssClass="grid" />
                                                </asp:TemplateField>
                                                
                                                
                                                
                                            </Columns>
                                            <AlternatingRowStyle CssClass="InvgridAltRow"></AlternatingRowStyle>
                                            <HeaderStyle CssClass="Invgridheader" />
                                            <PagerStyle CssClass="Invgridheader" />
                                            <RowStyle CssClass="Invgridrow" HorizontalAlign="Center"></RowStyle>
                                        </asp:GridView>
                            
                            </td>
                            
                            </tr>
                            <tr>
                           <td>
                           <table>
                           <tr>
                            <td >
                            <asp:Label ID="Label3" runat="server" CssClass="labelComman" Text="<%$ Resources:Attendance,Employee Name %>"></asp:Label>
                            
                            
                            </td>
                           <td width="1px">
                    :
                </td>
                <td  align='<%= Common.ChangeTDForDefaultLeft()%>' colspan="6">
                <asp:TextBox ID="txtEmpName" TabIndex ="105" Width="282px" runat="server" CssClass="textComman" BackColor="#e3e3e3"  AutoPostBack="true" OnTextChanged="txtEmpName_textChanged"  />
                                 <cc1:AutoCompleteExtender ID="AutoCompleteExtender5" runat="server" DelimiterCharacters=""
                                                                        Enabled="True" ServiceMethod="GetCompletionListEmployeeName" ServicePath="" CompletionInterval="100"
                                                                        MinimumPrefixLength="1" CompletionSetCount="1" TargetControlID="txtEmpName"
                                                                        UseContextKey="True" CompletionListCssClass="completionList" CompletionListItemCssClass="listItem"
                                                                        CompletionListHighlightedItemCssClass="itemHighlighted">
                                                                    </cc1:AutoCompleteExtender>
                
                </td>
                           
                           </tr>
                           <tr>
                <td  align='<%= Common.ChangeTDForDefaultLeft()%>' >
                    <asp:Label ID="lblHolidayCode" runat="server" CssClass="labelComman" Text="<%$ Resources:Attendance,From Date %>"></asp:Label>
                </td>
                <td width="1px">
                    :
                </td>
                <td align='<%= Common.ChangeTDForDefaultLeft()%>'>
                 
                  <asp:TextBox ID="txtFromDate" TabIndex ="105" Width="100px" runat="server" CssClass="textComman" AutoPostBack="true" OnTextChanged="txtToDate_textChanged"  />
                  <cc1:CalendarExtender ID="CalendarExtender1" runat="server" Enabled="True" TargetControlID="txtFromDate"
                                                                   >
                                                                </cc1:CalendarExtender>
                </td>
                <td  align='<%= Common.ChangeTDForDefaultLeft()%>' >
                    <asp:Label ID="Label1" runat="server" CssClass="labelComman" Text="<%$ Resources:Attendance,To Date %>"></asp:Label>
                </td>
                <td width="1px">
                    :
                </td>
                <td align='<%= Common.ChangeTDForDefaultLeft()%>'>
                 
               <asp:TextBox ID="txtToDate"  TabIndex ="105" Width="100px" runat="server" CssClass="textComman"  AutoPostBack="true" OnTextChanged="txtToDate_textChanged"   />
                  <cc1:CalendarExtender ID="CalendarExtender2" runat="server" Enabled="True" TargetControlID="txtToDate"
                                                                    >
                                                                </cc1:CalendarExtender>
                </td>
                <td align='<%= Common.ChangeTDForDefaultLeft()%>'>
                
                <asp:Label ID="Label2" runat="server" CssClass="labelComman" Text="<%$ Resources:Attendance,Days %>"></asp:Label>
                
                </td>
                <td align='<%= Common.ChangeTDForDefaultLeft()%>'>
                 <asp:TextBox Enabled="false" Width="20px" ID="lblDays" runat="server" CssClass="labelComman" ></asp:TextBox>
               
                
                </td>
            </tr>
                     
                     <tr>
                     <td>
                     </td>
                     <td>
                     </td>
                     <td align='<%= Common.ChangeTDForDefaultLeft()%>' >
                     
                    <asp:RadioButton ID="rbtnYearly" runat="server" CssClass="labelComman" AutoPostBack="true" OnCheckedChanged="rbtnMonthlyYearly" GroupName="leave" Text="<%$ Resources:Attendance,Yearly %>" />
                 <asp:RadioButton ID="rbtnMonthly" runat="server" CssClass="labelComman" AutoPostBack="true" OnCheckedChanged="rbtnMonthlyYearly" GroupName="leave" Text="<%$ Resources:Attendance,Monthly %>" />
                     </td>
                    
                     </td>
                     <td > </td>
                     <td >
                    
                    
                     <td align='<%= Common.ChangeTDForDefaultLeft()%>'>
                       <asp:DropDownList ID="ddlLeaveType" Width="112px" runat="server" CssClass="labelComman"  />
               
                     <asp:HiddenField ID="hdnEmpId" runat="server" />
                     </td> 
                     </tr>  
                     
                     
                    
                     
                     
                    <tr>
                    <td > </td>
                    <td > </td>
                    <td align='<%= Common.ChangeTDForDefaultLeft()%>' > 
                     <asp:Button ID="btnApply" runat="server" TabIndex ="107"  Width="50px"
                                                                    Text="<%$ Resources:Attendance,Apply %>" Visible="true" CssClass="buttonCommman"
                                                                     onclick="btnApply_Click" />
                    
                    &nbsp;&nbsp;
                     <asp:Button ID="Button1" runat="server" TabIndex ="107"  Width="50px"
                                                                    Text="<%$ Resources:Attendance,Reset %>" Visible="true" CssClass="buttonCommman"
                                                                     onclick="btnReset_Click" />
                    </td>
                    </tr>   
                           
                           </table>
                           
                           </td>
                           
                           </tr>
                           <tr>
                           <td>
                           <asp:GridView ID="gvLeaveSummary" PageSize="<%# SystemParameter.GetPageSize() %>" runat="server"
                                            AutoGenerateColumns="False" Width="100%"
                                            CssClass="grid" >
                                            <Columns>
                                             
                                                 <asp:TemplateField HeaderText="<%$ Resources:Attendance,Leave Name %>" >
                                                    <ItemTemplate>
                                        
                                                        <asp:Label ID="lblLeaveName"  runat="server" Text='<%# Eval("Leave_Name") %>'></asp:Label>
                                                        
                                                        
                                                    </ItemTemplate>
                                                    <ItemStyle CssClass="grid" />
                                                </asp:TemplateField>
                                                
                                                 <asp:TemplateField HeaderText="<%$ Resources:Attendance,Schedule Type %>" >
                                                    <ItemTemplate>
                                        
                                                        <asp:Label ID="lblScheduleType"  runat="server" Text='<%# Eval("Shedule_Type") %>'></asp:Label>
                                                        
                                                        
                                                    </ItemTemplate>
                                                    <ItemStyle CssClass="grid" />
                                                </asp:TemplateField>
                                                 <asp:TemplateField HeaderText="Month" >
                                                    <ItemTemplate>
                                        
                                                        <asp:Label ID="lblMonthName"  runat="server" Text='<%# GetMonthName(Eval("Month"),Eval("MonthName")) %>'></asp:Label>
                                                        
                                                        
                                                    </ItemTemplate>
                                                    <ItemStyle CssClass="grid" />
                                                </asp:TemplateField>
                                                
                                                
                                                
                                                 <asp:TemplateField HeaderText="Year" >
                                                    <ItemTemplate>
                                        
                                                        <asp:Label ID="lblMonthName"  runat="server" Text='<%# Eval("Year") %>'></asp:Label>
                                                        
                                                        
                                                    </ItemTemplate>
                                                    <ItemStyle CssClass="grid" />
                                                </asp:TemplateField>
                                                
                                                
                                                <asp:TemplateField HeaderText="<%$ Resources:Attendance,Total Leave %>" >
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblTotalDays" runat="server" Text='<%# Eval("Total_Days") %>'></asp:Label>
                                                    </ItemTemplate>
                                                    <ItemStyle CssClass="grid" />
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="<%$ Resources:Attendance,Used Leave %>">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblUsedDays" runat="server" Text='<%# Eval("Used_Days") %>'></asp:Label>
                                                    </ItemTemplate>
                                                    <ItemStyle CssClass="grid" />
                                                </asp:TemplateField>
                                                
                                                

                                                
                                                 <asp:TemplateField HeaderText="<%$ Resources:Attendance,Remaining Leave %>" >
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblRemainingDays" runat="server" Text='<%# Eval("Remaining_Days") %>'></asp:Label>
                                                    </ItemTemplate>
                                                    <ItemStyle CssClass="grid" />
                                                </asp:TemplateField>
                                            </Columns>
                                            <AlternatingRowStyle CssClass="InvgridAltRow"></AlternatingRowStyle>
                                            <HeaderStyle CssClass="Invgridheader" />
                                            <PagerStyle CssClass="Invgridheader" />
                                            <RowStyle CssClass="Invgridrow" HorizontalAlign="Center"></RowStyle>
                                        </asp:GridView>
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

