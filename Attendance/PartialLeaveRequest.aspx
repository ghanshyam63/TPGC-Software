<%@ Page Language="C#" MasterPageFile="~/ERPMaster.master" AutoEventWireup="true" CodeFile="PartialLeaveRequest.aspx.cs" Inherits="Attendance_PartialLeaveRequest" Title="Untitled Page" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
<link href="../CSS/InvStyle.css" rel="stylesheet" type="text/css" />
  <asp:UpdatePanel ID="UpdatePanel1" runat="server">
        <Triggers>
            <asp:PostBackTrigger ControlID="btnList" />
            <asp:PostBackTrigger ControlID="btnNew" />
            <asp:PostBackTrigger ControlID="btnApply" />
            <asp:PostBackTrigger ControlID="btnReset" />
            <asp:PostBackTrigger ControlID="btnCancel" />
                
            
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
                                                <asp:Label ID="lblHeader" runat="server" Text="<%$ Resources:Attendance,Partial Leave Setup %>"
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
                                
                                <asp:Panel ID="pnlList" runat="server" >
                                
                                <asp:GridView ID="gvLeaveStatus" PageSize="<%# SystemParameter.GetPageSize() %>" runat="server"
                                            AutoGenerateColumns="False" Width="100%" OnPageIndexChanging="gvLeaveStatus_PageIndexChanging" AllowPaging="true" 
                                            CssClass="grid" >
                                            <Columns>
                                               <asp:TemplateField HeaderText="<%$ Resources:Attendance,Edit %>">
                                                    <ItemTemplate>
                                                        <asp:ImageButton ID="IbtnEdit" runat="server" CommandName='<%# Eval("Emp_Id") %>' CommandArgument='<%# Eval("Trans_Id") %>'
                                                            ImageUrl="~/Images/edit.png" CausesValidation="False" OnCommand="btnEdit_Command"
                                                            Visible="true" ToolTip="<%$ Resources:Attendance,Edit %>" /></ItemTemplate>
                                                    <ItemStyle HorizontalAlign="Center" />
                                                 <ItemStyle CssClass="grid" />
                                                </asp:TemplateField>
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
                                                 
                                                
                                              <asp:TemplateField HeaderText="<%$ Resources:Attendance,Type %>" >
                                                    <ItemTemplate>
                                        
                                                        <asp:Label ID="lblScheduleType"  runat="server" Text='<%# leavetype(Eval("Partial_Leave_Type").ToString()) %>'></asp:Label>
                                                        
                                                        
                                                    </ItemTemplate>
                                                    <ItemStyle CssClass="grid" />
                                                </asp:TemplateField>
                                             
                                                
                                                  <asp:TemplateField HeaderText="<%$ Resources:Attendance,Apply Date %>">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblApplicatonDate" runat="server" Text='<%# GetDate(Eval("Partial_Leave_Date")) %>'></asp:Label>
                                                    </ItemTemplate>
                                                    <ItemStyle CssClass="grid" />
                                                </asp:TemplateField>
                                                
                                                
                                                <asp:TemplateField HeaderText="<%$ Resources:Attendance,To Time %>">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblFromDate" runat="server" Text='<%# Eval("From_Time") %>'></asp:Label>
                                                    </ItemTemplate>
                                                    <ItemStyle CssClass="grid" />
                                                </asp:TemplateField>
                                                
                                                 <asp:TemplateField HeaderText="<%$ Resources:Attendance,From Time %>">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblTodays" runat="server" Text='<%# Eval("To_Time") %>'></asp:Label>
                                                    </ItemTemplate>
                                                    <ItemStyle CssClass="grid" />
                                                </asp:TemplateField>

                                                
                                                 <asp:TemplateField HeaderText="<%$ Resources:Attendance,Status %>" >
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblStatus" runat="server" Text='<%# Eval("Is_Confirmed")%>' ></asp:Label>
                                                    </ItemTemplate>
                                                    <ItemStyle CssClass="grid" />
                                                </asp:TemplateField>
                                                
                                                
                                                
                                                <asp:TemplateField HeaderText="<%$ Resources:Attendance,Approve %>">
                                                    <ItemTemplate>
                                                        <asp:ImageButton ID="IbtnApprove" runat="server" CommandName='<%# Eval("Emp_Id") %>' CommandArgument='<%# Eval("Trans_Id") %>'
                                                            ImageUrl="~/Images/approve.png" CausesValidation="False" OnCommand="btnApprove_Command"
                                                            Visible="true" ToolTip="<%$ Resources:Attendance,Approve %>" /></ItemTemplate>
                                                    <ItemStyle HorizontalAlign="Center" />
                                                 <ItemStyle CssClass="grid" />
                                                </asp:TemplateField>
                                                 <asp:TemplateField HeaderText="<%$ Resources:Attendance,Reject %>">
                                                    <ItemTemplate>
                                                        <asp:ImageButton ID="IbtnReject" runat="server" CausesValidation="False"   CommandArgument='<%# Eval("Trans_Id") %>'
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
                                
                                </asp:Panel>
                                
                                 <asp:Panel ID="pnlNew" runat="server" DefaultButton="btnApply" >
                                 <table style="padding-left:20px;">
                                 
                                 <tr>
                                 
                                 <td  colspan="6"   >
                                 <asp:RadioButton ID="rbtnPersonal" CssClass="labelComman" runat="server" GroupName="Partial" Text="<%$ Resources:Attendance,Personal%>" />
                                 <asp:RadioButton ID="rbtnOfficial" CssClass="labelComman" runat="server" GroupName="Partial" Text="<%$ Resources:Attendance,Offical %>" />
                                 
                                 </td>
                                 </tr>
                           <tr>
                            <td  align='<%= Common.ChangeTDForDefaultLeft()%>'>
                            <asp:HiddenField ID="hdnEmpId" runat="server" />
                            
                             <asp:HiddenField ID="hdnEdit" runat="server" />
                            <asp:Label ID="Label3" runat="server" CssClass="labelComman" Text="<%$ Resources:Attendance,Employee Name %>"></asp:Label>
                            
                            
                            </td>
                           <td width="1px">
                    :
                </td>
                <td  align='<%= Common.ChangeTDForDefaultLeft()%>' colspan="4">
                <asp:TextBox ID="txtEmpName"  Width="282px" runat="server" CssClass="textComman" BackColor="#e3e3e3"  AutoPostBack="true" OnTextChanged="txtEmpName_textChanged"  />
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
                            
                            <asp:Label ID="Label1" runat="server" CssClass="labelComman" Text="<%$ Resources:Attendance,Apply Date %>"></asp:Label>
                            
                            
                            </td>
                           <td width="1px">
                    :
                </td>
                <td  align='<%= Common.ChangeTDForDefaultLeft()%>' colspan="6">
                <asp:TextBox ID="txtApplyDate"  Width="100px" runat="server" CssClass="textComman" />
                                <cc1:CalendarExtender ID="CalendarExtender2" runat="server" Enabled="True" TargetControlID="txtApplyDate"
                                                                   >
                                                                </cc1:CalendarExtender>
                
                </td>
                           </tr>
                           <tr>
                           
                          <td  align='<%= Common.ChangeTDForDefaultLeft()%>'>
                            
                            <asp:Label ID="Label2" runat="server" CssClass="labelComman" Text="<%$ Resources:Attendance,From Time %>"></asp:Label>
                            
                            
                            </td>
                           <td width="1px">
                    :
                </td>
                <td  Width="100px" align='<%= Common.ChangeTDForDefaultLeft()%>' >
                <asp:TextBox ID="txtInTime"  Width="100px" runat="server" CssClass="textComman" />
                              
                               <cc1:MaskedEditExtender ID="MaskedEditExtender1" runat="server" CultureAMPMPlaceholder=""
                                CultureCurrencySymbolPlaceholder="" CultureDateFormat="" CultureDatePlaceholder=""
                                CultureDecimalPlaceholder="" CultureThousandsPlaceholder="" CultureTimePlaceholder=""
                                Enabled="True" ErrorTooltipEnabled="True" Mask="99:99" MaskType="Time" TargetControlID="txtInTime"
                                UserTimeFormat="TwentyFourHour">
                            </cc1:MaskedEditExtender>
                
                </td>
                <td  align='<%= Common.ChangeTDForDefaultLeft()%>' >
                            
                            <asp:Label ID="Label4" runat="server" CssClass="labelComman" Text="<%$ Resources:Attendance,To Time %>"></asp:Label>
                            
                            
                            </td>
                           <td width="1px">
                    :
                </td>
                <td  align='<%= Common.ChangeTDForDefaultLeft()%>' >
                <asp:TextBox ID="txtOuttime"  Width="100px" runat="server" CssClass="textComman" />
                               <cc1:MaskedEditExtender ID="txtOnDuty_MaskedEditExtender" runat="server" CultureAMPMPlaceholder=""
                                CultureCurrencySymbolPlaceholder="" CultureDateFormat="" CultureDatePlaceholder=""
                                CultureDecimalPlaceholder="" CultureThousandsPlaceholder="" CultureTimePlaceholder=""
                                Enabled="True" ErrorTooltipEnabled="True" Mask="99:99" MaskType="Time" TargetControlID="txtOuttime"
                                UserTimeFormat="TwentyFourHour">
                            </cc1:MaskedEditExtender>
                
                </td> 
                
                </tr>
                
                <tr>
                            <td  align='<%= Common.ChangeTDForDefaultLeft()%>'>
                            <asp:HiddenField ID="HiddenField1" runat="server" />
                            <asp:Label ID="Label5" runat="server" CssClass="labelComman" Text="<%$ Resources:Attendance,Description %>"></asp:Label>
                            
                            
                            </td>
                           <td width="1px">
                    :
                </td>
                <td  align='<%= Common.ChangeTDForDefaultLeft()%>' colspan="4">
                <asp:TextBox ID="txtDescription" TextMode= "MultiLine"   Width="282px" runat="server" CssClass="textComman" />                                
                
                </td>
                           
                           </tr>
                
                
                <tr>
                <td>
                
                </td>
                <td>
                
                </td>
                <td style="padding-left:12px;" align='<%= Common.ChangeTDForDefaultLeft()%>' colspan="4" >
                <asp:Button ID="btnApply" runat="server" 
                                                                    Text="<%$ Resources:Attendance,Apply %>" Visible="true" CssClass="buttonCommman"
                                                                     onclick="btnApply_Click" />
                    
                    &nbsp;&nbsp;
                   <asp:Button ID="btnReset" runat="server"   
                                                                    Text="<%$ Resources:Attendance,Reset %>" Visible="true" CssClass="buttonCommman"
                                                                     onclick="btnReset_Click" />
                    
                    &nbsp;&nbsp;
                     <asp:Button ID="btnCancel" runat="server"  
                                                                    Text="<%$ Resources:Attendance,Cancel %>" Visible="true" CssClass="buttonCommman"
                                                                     onclick="btnCancel_Click" />
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

