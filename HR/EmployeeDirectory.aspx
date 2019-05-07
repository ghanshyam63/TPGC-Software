<%@ Page Language="C#" MasterPageFile="~/ERPMaster.master" AutoEventWireup="true" CodeFile="EmployeeDirectory.aspx.cs" Inherits="HR_EmployeeDirectory" Title="Untitled Page" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">

<link href="../CSS/InvStyle.css" rel="stylesheet" type="text/css" />
    
<asp:UpdatePanel ID="UPd" runat="server">
<Triggers>
<asp:PostBackTrigger ControlID="btnSearch" />
<asp:PostBackTrigger ControlID="btnAddDocument" />
<asp:PostBackTrigger ControlID="btnSave" />
<asp:PostBackTrigger ControlID="btnReset" />
<asp:PostBackTrigger ControlID="btnCancel" />
<asp:PostBackTrigger ControlID="BtnEmployeeReset" />

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
                                                <asp:Label ID="lblHeader" runat="server" Text="<%$ Resources:Attendance,Directory Setup %>"
                                                    CssClass="LableHeaderTitle"></asp:Label>
                                            </td>
                                        </tr>
                                    </table>
                                </td>
                                <td align="right" style="width: 70%;">
                                    <table cellpadding="0" cellspacing="0">
                                        <tr>
                                            <td>
                                                <%--<asp:Panel ID="pnlLeave" runat="server" CssClass="a" Width="100%">
                                                    <asp:Button ID="btnLeave" runat="server" Text="<%$ Resources:Attendance,Penalty %>"
                                                        Width="100px" BorderStyle="none" BackColor="Transparent" 
                                                        Style="padding-top: 3px; padding-left: 15px; background-image: url('../Images/Bin.png' );
                                                        background-repeat: no-repeat; height: 49px; background-position: 5px 15px; font: bold 14px Trebuchet MS;
                                                        color: #000000;" />
                                                </asp:Panel>--%>
                                            </td>
                                            <td>
                                               <%-- <asp:Panel ID="PanelList" runat="server" CssClass="a" Width="100%">
                                                    <asp:Button ID="btnLeast" runat="server" Text="<%$ Resources:Attendance,List %>"
                                                        Width="80px" BorderStyle="none" BackColor="Transparent" Style="padding-top: 3px;
                                                        padding-left: 10px; background-image: url('../Images/List.png' ); background-repeat: no-repeat;
                                                        height: 49px; background-position: 5px 15px; font: bold 14px Trebuchet MS; color: #000000;"
                                                         />
                                                </asp:Panel>--%>
                                            </td>
                                        </tr>
                                    </table>
                                </td>
                            </tr>
                         
                            <tr>
                            <td valign="top" bgcolor="#ccddee" colspan="2" style="width:100%; height:500px;">
                            <table>
                               <tr>
                            <td colspan="2" style="width:100%; ">
                            <table>
                            <tr>
                            <td align="<%= Common.ChangeTDForDefaultLeft()%>">
                                                            <asp:Label ID="Label1" runat="server" CssClass="labelComman" 
                                                                Text="<%$ Resources:Attendance,Employee Name %>"></asp:Label>
                                                                
                                                            </td>
                                                            <td style="width:1px;">
                                                            :
                                                            </td>
                                                            <td align="<%= Common.ChangeTDForDefaultLeft()%>" >
                                                            <asp:TextBox ID="TxtEmployeeId" runat="server" Width="200px" TabIndex="1" 
                                                                    CssClass="textComman" ontextchanged="TxtEmployeeId_TextChanged" BackColor="#e3e3e3"></asp:TextBox>
                                                                <cc1:AutoCompleteExtender ID="AutoCompleteExtender5" runat="server" DelimiterCharacters=""
                                                        Enabled="True" ServiceMethod="GetCompletionListEmployeeName" ServicePath="" CompletionInterval="100"
                                                        MinimumPrefixLength="1" CompletionSetCount="1" TargetControlID="TxtEmployeeId" UseContextKey="True"
                                                        CompletionListCssClass="completionList" CompletionListItemCssClass="listItem"
                                                        CompletionListHighlightedItemCssClass="itemHighlighted">
                                                    </cc1:AutoCompleteExtender>
                                                
                                                            </td>
                                                            <td style="padding-left:30px;"  align="<%= Common.ChangeTDForDefaultLeft()%>">
                                                            <asp:Button ID="btnSearch" runat="server" CssClass="buttonCommman" TabIndex="7" Text="<%$ Resources:Attendance,Search %>"
                                                                        OnClick="btnSaveLeave_Click" Width="64px" ValidationGroup="leavesave"  />
                                                                    &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                                                                    <asp:Button ID="btnAddDocument" runat="server" CausesValidation="False" CssClass="buttonCommman"
                                                                        TabIndex="8" Text="<%$ Resources:Attendance,Add Document %>" 
                                                                     onclick="btnAddDocument_Click" 
                                                                         />
                                                                         &nbsp; &nbsp; &nbsp; 
                                                                          <asp:Button ID="BtnEmployeeReset" Width="64px" runat="server" 
                                                                    CausesValidation="False" CssClass="buttonCommman"
                                                                        TabIndex="8" Text="<%$ Resources:Attendance,Reset %>" onclick="BtnEmployeeReset_Click"   />
                                                                     <asp:HiddenField id="HidEmpId" runat="server" />
                                                                       
                                                                    </td>
                            </tr>
                            </table>
                            </td>
                            </tr>
                            <tr>
                            <td colspan="2" style="width:100%;">
                            <asp:GridView ID="gvFileMaster" PageSize="<%# SystemParameter.GetPageSize() %>" runat="server"
                                            AutoGenerateColumns="False" Width="100%" AllowPaging="True" AllowSorting="True"
                                            CssClass="grid"  >
                                            <Columns>
                                                
                                                
                                                  <asp:TemplateField HeaderText="<%$ Resources:Attendance,FileTransaction Id %>" >
                                                    <ItemTemplate>
                                        
                                                        <asp:Label ID="lblfileId1"  runat="server" Text='<%# Eval("Trans_Id") %>'></asp:Label>
                                                        
                                                        
                                                    </ItemTemplate>
                                                    <ItemStyle CssClass="grid" />
                                                </asp:TemplateField>
                                               <asp:TemplateField HeaderText="<%$ Resources:Attendance,Document Name %>" >
                                                    <ItemTemplate>
                                        
                                                        <asp:Label ID="lblfileId3"  runat="server" Text='<%# Eval("Document_Name") %>'></asp:Label>
                                                        
                                                        
                                                    </ItemTemplate>
                                                    <ItemStyle CssClass="grid" />
                                                </asp:TemplateField>
                                               
                                                <asp:TemplateField HeaderText="<%$ Resources:Attendance,File Name %>" >
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblFileName" runat="server" Text='<%# Eval("File_Name") %>'></asp:Label>
                                                    </ItemTemplate>
                                                    <ItemStyle CssClass="grid" />
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="<%$ Resources:Attendance,File Upload Date  %>" >
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblFileuploaddate" runat="server" Text='<%# Eval("File_Upload_Date") %>'></asp:Label>
                                                    </ItemTemplate>
                                                    <ItemStyle CssClass="grid" />
                                                </asp:TemplateField>
                                               
                                               
                                            
                                            </Columns>
                                            <AlternatingRowStyle CssClass="InvgridAltRow"></AlternatingRowStyle>
                                            <HeaderStyle CssClass="Invgridheader" />
                                            <PagerStyle CssClass="Invgridheader" />
                                            <RowStyle CssClass="Invgridrow" HorizontalAlign="Center"></RowStyle>
                                        </asp:GridView>
                                  
                                                  <asp:Panel ID="Paneladddocument" runat="server" Visible="false" >
                                 <table width="100%" style="padding-left: 43px">
        
             <tr>
                <td width="170px"align='<%= Common.ChangeTDForDefaultLeft()%>' >
                    <asp:Label ID="lblDirectName"  runat="server" CssClass="labelComman" Text="<%$ Resources:Attendance,Directory Name %>"></asp:Label>
                    
                </td>
                <td width="1px">
                    :
                </td>
                <td align='<%= Common.ChangeTDForDefaultLeft()%>'>
                     <asp:DropDownList ID="ddlDirectory" Width="262px" runat="server"  CssClass="textComman" />
                  </td>
                  <td>
                  &nbsp;
                  </td>
                  <td>
                  &nbsp;
                  </td>
                  <td>
                  &nbsp;
                  </td>
            </tr>
            <tr>
            <td width="170px" align='<%= Common.ChangeTDForDefaultLeft()%>'>
                    <asp:Label ID="lblDocName" runat="server" CssClass="labelComman" Text="<%$ Resources:Attendance,Document Name %>"></asp:Label>
                </td>
                <td width="1px">
                    :
                </td>
                <td align='<%= Common.ChangeTDForDefaultLeft()%>'>
                    <asp:DropDownList ID="ddlDocumentName" Width="262px" runat="server"  CssClass="textComman" />
                 
                </td>
                <td>
                &nbsp;
                </td>
            <td>
                &nbsp;
                </td>
            <td>
                &nbsp;
                </td>
            
            
            
            </tr>
           
            <tr>
             
                 <td width="170px"align='<%= Common.ChangeTDForDefaultLeft()%>' >
                    <asp:Label ID="lblfileName1" runat="server" CssClass="labelComman" Text="<%$ Resources:Attendance,File Name %>"></asp:Label>
                </td>
                <td width="1px">
                    :
                </td>
                <td align='<%= Common.ChangeTDForDefaultLeft()%>'>
                    <asp:TextBox ID="txtFileName" Width="254px" runat="server" CssClass="textComman" />
                         <%--<cc1:AutoCompleteExtender ID="AutoCompleteExtender2" runat="server" DelimiterCharacters=""
                                                        Enabled="True" ServiceMethod="GetCompletionListDepCode" ServicePath="" CompletionInterval="100"
                                                        MinimumPrefixLength="1" CompletionSetCount="1" TargetControlID="txtFileName"
                                                        UseContextKey="True" CompletionListCssClass="completionList" CompletionListItemCssClass="listItem"
                                                        CompletionListHighlightedItemCssClass="itemHighlighted">
                                                    </cc1:AutoCompleteExtender>
               --%> </td>
                  <td width="170px" align='<%= Common.ChangeTDForDefaultLeft()%>' >
                    <asp:Label ID="lblfiletype" runat="server" CssClass="labelComman" Text="<%$ Resources:Attendance,File Type %>" Visible="False"></asp:Label>
                </td>
                <td width="1px">
                    
                </td>
                <td align='<%= Common.ChangeTDForDefaultLeft()%>'>
                     <asp:DropDownList ID="ddlFiletype" Width="262px" runat="server"  CssClass="textComman" Visible="False" />
                 
                </td>
            </tr>
                
          
            
             <tr>
                <td width="170px"align='<%= Common.ChangeTDForDefaultLeft()%>' >
                    <asp:Label ID="lblUploadfile" runat="server" CssClass="labelComman" Text="<%$ Resources:Attendance,File Upload %>"></asp:Label>
                </td>
                <td width="1px">
                    :
                </td>
                <td align='<%= Common.ChangeTDForDefaultLeft()%>'>
                   <asp:FileUpload ID="UploadFile" runat="server" CssClass="textComman" Width="250px" /> 
                </td>
                   <td width="170px"align='<%= Common.ChangeTDForDefaultLeft()%>' >
                    
                    &nbsp; </td>
                <td width="1px">
                    &nbsp;
                </td>
                <td align='<%= Common.ChangeTDForDefaultLeft()%>'>
                   &nbsp;
                </td>
            </tr>
            <tr>
                <td width="170px"align='<%= Common.ChangeTDForDefaultLeft()%>' >
                    <asp:Label ID="Label2" runat="server" CssClass="labelComman" Text="<%$ Resources:Attendance,Expiry Date %>"></asp:Label>
                </td>
                <td width="1px">
                    :
                </td>
                <td align='<%= Common.ChangeTDForDefaultLeft()%>'>
                  <asp:TextBox ID="txtExpiryDate" runat="server" CssClass="textComman" Width="254px" />
                                                    <cc1:CalendarExtender ID="txtCalenderExtender" runat="server" TargetControlID="txtExpiryDate"
                                                        Format="dd-MMM-yyyy">
                                                    </cc1:CalendarExtender>
                </td>
                   <td width="170px"align='<%= Common.ChangeTDForDefaultLeft()%>' >
                    
                    &nbsp; </td>
                <td width="1px">
                    &nbsp;
                </td>
                <td align='<%= Common.ChangeTDForDefaultLeft()%>'>
                   &nbsp;
                </td>
            </tr>
            
           
            
             
            <tr>
                                              
                                                <td align="center" colspan="6" >
                                                    <table style="padding-left: 8px">
                                                        <tr>
                                                           
                                                                         <td width="80px">
                                                                <asp:Button ID="btnSave" runat="server" Width="64px" Text="<%$ Resources:Attendance,Save %>" OnClick="btnSave_Click"  CssClass="buttonCommman"
                                                                    ValidationGroup="a" />
                                                            </td>
                                                            <td width="80px">
                                                                <asp:Button ID="btnReset" runat="server" Width="64px" Text="<%$ Resources:Attendance,Reset %>" OnClick="btnReset_Click"  CssClass="buttonCommman"
                                                                    CausesValidation="False" />
                                                            </td>
                                                            <td width="80px">
                                                                <asp:Button ID="btnCancel" runat="server" Width="64px" Text="<%$ Resources:Attendance,Cancel %>" OnClick="btnCancel_Click" CssClass="buttonCommman"
                                                                     CausesValidation="False" />
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
                    </td>
                </tr>
            </table>
</ContentTemplate>
</asp:UpdatePanel>


            
</asp:Content>

