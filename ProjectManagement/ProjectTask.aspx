<%@ Page Title="" Language="C#" MasterPageFile="~/ERPMaster.master" AutoEventWireup="true" CodeFile="ProjectTask.aspx.cs" Inherits="ProjectManagement_ProjectTask" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit.HTMLEditor"
    TagPrefix="cc2" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
 <link href="../CSS/AJAX.css" rel="stylesheet" type="text/css" />
<asp:UpdatePanel ID="UpdatePanel2" runat="server" UpdateMode="Conditional">
  <Triggers>
            <asp:PostBackTrigger ControlID="btnNew" />
          <asp:PostBackTrigger ControlID="btnList" />
        </Triggers>
  <ContentTemplate>
        <table width="100%">
                <tr >
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
                                                <asp:Label ID="lblHeader" runat="server" Text="<%$ Resources:Attendance,Project Task %>"
                                                    CssClass="LableHeaderTitle"></asp:Label>
                                            </td>
                                        </tr>
                                    </table>
                                </td>
                                <td align="right" style="width: 70%;">
                                    <table cellpadding="0" cellspacing="0">
                                        <tr>
                                         <td>
                                            
                                             <asp:Panel ID="PanelList" runat="server" CssClass="a" Width="100%">
                                                    <asp:Button ID="btnList" runat="server" Text="<%$ Resources:Attendance,List %>" Width="80px"
                                                        BorderStyle="none" BackColor="Transparent" Style="padding-top: 3px; padding-left: 10px;
                                                        background-image: url('../Images/List.png' ); background-repeat: no-repeat; height: 49px;
                                                        background-position: 5px 15px; font: bold 14px Trebuchet MS; color: #000000;"
                                                        OnClick="btList_Click" />
                                                </asp:Panel>
                                               
                                           </td>
                                            <td>
                                              <asp:Panel ID="pnlnew" runat="server" CssClass="a" Width="100%">
                                                    <asp:Button ID="btnnew" runat="server" Text="<%$ Resources:Attendance,New %>" Width="100px"
                                                        BorderStyle="none" BackColor="Transparent" Style="padding-top: 3px; padding-left: 15px;
                                                        background-image: url('../Images/Bin.png' ); background-repeat: no-repeat; height: 49px;
                                                        background-position: 5px 15px; font: bold 14px Trebuchet MS; color: #000000;"
                                                        OnClick="btnnew_Click" />
                                                </asp:Panel>
                                          </td>
                                        </tr>
                                    </table>
                                </td>
                            </tr>
                            <tr>
                                <td bgcolor="#ccddee" colspan="4" width="100%" height="500px" valign="top">
    <asp:Panel ID="pnllist" runat="server" >
                                            <div width="100%" style="background-image: url(../Images/bg_repeat.jpg); background-repeat: repeat;">
                                                <table width="100%" style="padding-left: 20px; height: 38px">
                                                    <tr>
                                                        <td width="90px">
                                                            <asp:Label ID="LblSelectField" runat="server" Text="<%$ Resources:Attendance,Select Field %>"
                                                                CssClass="labelComman"></asp:Label>
                                                        </td>
                                                        <td width="180px">
                                                            <asp:DropDownList ID="ddlFieldName" runat="server" CssClass="DropdownSearch" Height="25px"
                                                                Width="170px">
                                                                
                                                                <asp:ListItem Text="<%$ Resources:Attendance,Employee Name %>" Value="Emp_Name"
                                                                    Selected="True" />
                                                            </asp:DropDownList>
                                                        </td>
                                                        <td width="135px">
                                                            <asp:DropDownList ID="ddlOption" runat="server" CssClass="DropdownSearch" Height="25px"
                                                                Width="120px">
                                                                <asp:ListItem Text="<%$ Resources:Attendance,--Select-- %>"></asp:ListItem>
                                                                <asp:ListItem Text="<%$ Resources:Attendance,Equal %>"></asp:ListItem>
                                                                <asp:ListItem Selected="True" Text="<%$ Resources:Attendance,Contains %>"></asp:ListItem>
                                                                <asp:ListItem Text="<%$ Resources:Attendance,Like %>"></asp:ListItem>
                                                            </asp:DropDownList>
                                                        </td>
                                                        <td width="270px">
                                                            <asp:TextBox ID="txtValue" runat="server" CssClass="textCommanSearch" Height="14px"
                                                                Width="250px"></asp:TextBox>
                                                        </td>
                                                        <td width="50px" align="center">
                                                            <asp:ImageButton ID="btnbind" runat="server" CausesValidation="False" Height="25px"
                                                                ImageUrl="~/Images/search.png" OnClick="btnbindrpt_Click" Width="25px" ToolTip="<%$ Resources:Attendance,Search %>">
                                                            </asp:ImageButton>
                                                        </td>
                                                        <td>
                                                            <asp:Panel ID="PnlRefresh" runat="server">
                                                               <asp:ImageButton ID="btnRefresh" runat="server" CausesValidation="False" Height="25px"
                                                                    ImageUrl="~/Images/refresh.png"  Width="25px"
                                                                    ToolTip="<%$ Resources:Attendance,Refresh %>" onclick="btnRefresh_Click"></asp:ImageButton></asp:Panel>
                                                        </td>
                                                        <td align='<%= Common.ChangeTDForDefaultLeft()%>' colspan="9">
                                                            <asp:Label ID="lblTotalRecords" runat="server" Text="<%$ Resources:Attendance,Total Records: 0 %>"
                                                                CssClass="labelComman"></asp:Label>
                                                        </td>
                                                    </tr>
                                                </table>
                                            </div>
                                       
                                        <asp:GridView ID="GvrProjecttask" runat="server" AutoGenerateColumns="False"
                                            Width="100%" AllowPaging="True" OnPageIndexChanging="GvrProjecttask_PageIndexChanging"
                                            AllowSorting="True" CssClass="grid" 
                                            onsorting="GvrProjecttask_Sorting"
                                           
                                           >
                                            <AlternatingRowStyle CssClass="InvgridAltRow" />
                                            <Columns>
                                                <%--<asp:TemplateField HeaderText="<%$ Resources:Attendance,Edit %>">
                                                    <ItemTemplate>
                                                        <asp:ImageButton ID="btnEdit" runat="server" CommandArgument='<%# Eval("Project_Id") %>'
                                                            ImageUrl="~/Images/edit.png" CausesValidation="False" OnCommand="btnEdit_command"
                                                            ToolTip="<%$ Resources:Attendance,Edit %>" Width="16px" /></ItemTemplate>
                                                    <ItemStyle HorizontalAlign="Center" />
                                                    <ItemStyle CssClass="grid" />
                                                </asp:TemplateField>--%>
                                               
                                                <asp:TemplateField HeaderText="<%$ Resources:Attendance,Employee Name %>" SortExpression="Emp_Name">
                                                    <ItemTemplate>
                                                     <asp:HiddenField ID="HiddeniD" runat="server" />    
                                                        <asp:Label ID="lblprojectIdList" runat="server" Text='<%# Eval("Emp_Name") %>'></asp:Label>
                                                    </ItemTemplate>
                                                    <ItemStyle CssClass="grid" />
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="<%$ Resources:Attendance,Subject %>" SortExpression="">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblEmpnameList1" runat="server" Text='<%# Eval("Subject") %>'></asp:Label>
                                                    </ItemTemplate>
                                                    <ItemStyle CssClass="grid" />
                                                </asp:TemplateField>
                                                 <asp:TemplateField HeaderText="<%$ Resources:Attendance,Assign Date %>" SortExpression="">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblEmpnameList2" runat="server" DataFormatString="{0:MM/dd/yy}" Text='<%# Eval("Assign_Date", "{0:dd-MMM-yyyy}") %>'></asp:Label>
                                                    </ItemTemplate>
                                                    <ItemStyle CssClass="grid" />
                                                </asp:TemplateField>
                                                 <asp:TemplateField HeaderText="<%$ Resources:Attendance,Assign Time %>" SortExpression="">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblEmpnameList3" runat="server" Text='<%# FormatTime(Eval("Assign_Time")) %>'></asp:Label>
                                                    </ItemTemplate>
                                                    <ItemStyle CssClass="grid" />
                                                </asp:TemplateField>
                                                 <asp:TemplateField HeaderText="<%$ Resources:Attendance,Status %>" SortExpression="">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblEmpnameList4" runat="server" Text='<%# Eval("Status") %>'></asp:Label>
                                                    </ItemTemplate>
                                                    <ItemStyle CssClass="grid" />
                                                </asp:TemplateField>
                                            </Columns>
                                            <HeaderStyle CssClass="Invgridheader" />
                                            <PagerStyle CssClass="Invgridheader" />
                                            <RowStyle CssClass="Invgridrow" HorizontalAlign="Center" />
                                        </asp:GridView>
                                         <asp:HiddenField ID="HDFSort" runat="server" />
                                    <br />
                                    <br />
                                    <%-- <asp:Label ID="Lblemployeeid" runat="server" Font-Bold="true" CssClass="labelComman" Text="<%$ Resources:Attendance,Employee Id %>" Visible="false"></asp:Label>
                                     : <asp:Label ID="SetEmployeeId" runat="server" CssClass="labelComman" Visible="false"></asp:Label>
                                     &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                                     <asp:Label ID="Lblemployeename" runat="server" Font-Bold="true" CssClass="labelComman" Text="<%$ Resources:Attendance,Employee Name %>" Visible="false"></asp:Label>
                                     : <asp:Label ID="setemployeename" runat="server" CssClass="labelComman" Visible="false"></asp:Label>
                                     --%>
                                     
                                    <asp:HiddenField ID="HiddeniD" runat="server" />     </asp:Panel>                 
      <asp:Panel ID="pnlteamdetials" runat="server" Width="100%" Visible="false">
 <table bgcolor="#ccddee"  Width="100%">
    <tr>
    <td align='<%= Common.ChangeTDForDefaultLeft()%>'>
        <asp:Label ID="Label1" runat="server" Text="Project Name" 
            CssClass="labelComman"></asp:Label>
    
    </td>
    <td style="width: 292px" colspan="3" align='<%= Common.ChangeTDForDefaultLeft()%>'>
        <asp:DropDownList ID="ddlprojectname" runat="server" CssClass="DropdownSearch" 
            
            AutoPostBack="True">
        </asp:DropDownList>
    </td>
    </tr>
     <tr>
    <td align='<%= Common.ChangeTDForDefaultLeft()%>'>
        <asp:Label ID="Label12" runat="server" Text="Employee Name" 
            CssClass="labelComman"></asp:Label>
    
    </td>
    <td style="width: 741px" colspan="3" align='<%= Common.ChangeTDForDefaultLeft()%>'>
     <asp:TextBox ID="txtEmplname" runat="server" Width="741px"  BackColor="#e3e3e3"
             OnTextChanged="txtEmplname_TextChanged" CssClass="textComman"></asp:TextBox>
       <cc1:AutoCompleteExtender ID="AutoCompleteExtender5" runat="server" DelimiterCharacters=""
         Enabled="True" ServiceMethod="GetCompletionListEmployeeName" ServicePath="" CompletionInterval="100"
         MinimumPrefixLength="1" CompletionSetCount="1" TargetControlID="txtEmplname" UseContextKey="True"
           CompletionListCssClass="completionList" CompletionListItemCssClass="listItem"
            CompletionListHighlightedItemCssClass="itemHighlighted">
                                                    </cc1:AutoCompleteExtender>
    </td>
    </tr>
     <tr>
    <td align='<%= Common.ChangeTDForDefaultLeft()%>' style="height: 26px">
      <asp:Label ID="Label5" runat="server" Text="Assign Date" CssClass="labelComman"></asp:Label>
    </td>
     <td style="width: 292px; height: 26px;" 
             align='<%= Common.ChangeTDForDefaultLeft()%>'>
     <asp:TextBox ID="txtassigndate" runat="server" CssClass="textComman"></asp:TextBox>
      <cc1:CalendarExtender  ID="CalendarExtender1"  Format="dd-MMM-yyyy"  runat="server" Enabled="True" TargetControlID="txtassigndate">
           </cc1:CalendarExtender>
    </td>
    <td align='<%= Common.ChangeTDForDefaultLeft()%>' style="height: 26px">
      <asp:Label ID="Label6" runat="server" Text="Assign Time" CssClass="labelComman"></asp:Label>
    </td>
    <td align='<%= Common.ChangeTDForDefaultLeft()%>' style="height: 26px">
     <asp:TextBox ID="txtassigntime" runat="server" CssClass="textComman" Width="244px"></asp:TextBox>
        <cc1:MaskedEditExtender ID="MaskedEditExtender1" runat="server" CultureAMPMPlaceholder=""
                                                        CultureCurrencySymbolPlaceholder="" CultureDateFormat="" CultureDatePlaceholder=""
                                                        CultureDecimalPlaceholder="" CultureThousandsPlaceholder="" CultureTimePlaceholder=""
                                                        Enabled="True" Mask="99:99:99" MaskType="Time" TargetControlID="txtassigntime">
                                                    </cc1:MaskedEditExtender>
    </td>
    </tr>
     <tr>
    <td align='<%= Common.ChangeTDForDefaultLeft()%>' style="height: 26px">
      <asp:Label ID="Label2" runat="server" Text="Employee End Date" CssClass="labelComman"></asp:Label>
    </td>
     <td style="width: 292px; height: 26px;" 
             align='<%= Common.ChangeTDForDefaultLeft()%>'>
     <asp:TextBox ID="txtempenddate" runat="server" CssClass="textComman"></asp:TextBox>
      <cc1:CalendarExtender  ID="CalendarExtender3"  Format="dd-MMM-yyyy"  runat="server" Enabled="True" TargetControlID="txtempenddate">
           </cc1:CalendarExtender>
    </td>
    <td align='<%= Common.ChangeTDForDefaultLeft()%>' style="height: 26px">
      <asp:Label ID="Label3" runat="server" Text="Employee End Time" CssClass="labelComman"></asp:Label>
    </td>
    <td align='<%= Common.ChangeTDForDefaultLeft()%>' style="height: 26px">
     <asp:TextBox ID="txtempendtime" runat="server" CssClass="textComman" Width="244px"></asp:TextBox>
       <cc1:MaskedEditExtender ID="MaskedEditExtender6" runat="server" CultureAMPMPlaceholder=""
                                                        CultureCurrencySymbolPlaceholder="" CultureDateFormat="" CultureDatePlaceholder=""
                                                        CultureDecimalPlaceholder="" CultureThousandsPlaceholder="" CultureTimePlaceholder=""
                                                        Enabled="True" Mask="99:99:99" MaskType="Time" TargetControlID="txtempendtime">
                                                    </cc1:MaskedEditExtender>
    </td>
    </tr>
     <tr>
    <td align='<%= Common.ChangeTDForDefaultLeft()%>' style="height: 26px">
      <asp:Label ID="Label4" runat="server" Text="TL End Date" CssClass="labelComman"></asp:Label>
    </td>
     <td style="width: 292px; height: 26px;" 
             align='<%= Common.ChangeTDForDefaultLeft()%>'>
     <asp:TextBox ID="txttlenddate" runat="server" CssClass="textComman"></asp:TextBox>
      <cc1:CalendarExtender  ID="CalendarExtender5"  Format="dd-MMM-yyyy"  runat="server" Enabled="True" TargetControlID="txttlenddate">
           </cc1:CalendarExtender>
    </td>
    <td align='<%= Common.ChangeTDForDefaultLeft()%>' style="height: 26px">
      <asp:Label ID="Label7" runat="server" Text="TL End Time" CssClass="labelComman"></asp:Label>
    </td>
    <td align='<%= Common.ChangeTDForDefaultLeft()%>' style="height: 26px">
     <asp:TextBox ID="txttlendtime" runat="server" CssClass="textComman" Width="244px"></asp:TextBox>
    <cc1:MaskedEditExtender ID="MaskedEditExtender2" runat="server" CultureAMPMPlaceholder=""
                                                        CultureCurrencySymbolPlaceholder="" CultureDateFormat="" CultureDatePlaceholder=""
                                                        CultureDecimalPlaceholder="" CultureThousandsPlaceholder="" CultureTimePlaceholder=""
                                                        Enabled="True" Mask="99:99:99" MaskType="Time" TargetControlID="txttlendtime">
                                                    </cc1:MaskedEditExtender>
    </td>
    </tr>
    <tr>
    <td align='<%= Common.ChangeTDForDefaultLeft()%>' style="height: 26px">
      <asp:Label ID="Label8" runat="server" Text="Subject" CssClass="labelComman"></asp:Label>
    </td >
    <td style="width: 741px;" colspan="3" 
            align='<%= Common.ChangeTDForDefaultLeft()%>'>
     <asp:TextBox ID="txtsubject" runat="server" Width="741px" CssClass="textComman"></asp:TextBox>
    </td>
    </tr>
     <tr>
    <td align='<%= Common.ChangeTDForDefaultLeft()%>'>
      <asp:Label ID="Label9" runat="server" Text="Project Description" 
            CssClass="labelComman"></asp:Label>
    </td>
    <td style="width: 292px" colspan="3" align='<%= Common.ChangeTDForDefaultLeft()%>'>
        <cc2:Editor ID="Editor1" runat="server" Width="741px" Height="300px"/>
    </td>
    </tr>
    
    <tr>
    <td align='<%= Common.ChangeTDForDefaultLeft()%>'>
     <asp:Label ID="Label10" runat="server" Text="File Download" CssClass="labelComman"></asp:Label>
    </td>
    <td style="width: 292px" align='<%= Common.ChangeTDForDefaultLeft()%>'>
     <asp:TextBox ID="txtfilename" runat="server" CssClass="textComman"></asp:TextBox>&nbsp;&nbsp;
        </td>
    <td >
        <asp:Button ID="btnfiledwnload" runat="server" CssClass="buttonCommman" 
            OnClick="btnfiledwnload_Click" Text="Download" />
        </td>
    </tr>
     <tr>
    <td align='<%= Common.ChangeTDForDefaultLeft()%>' style="height: 50px">
        </td>
    <td style="width: 292px; height: 50px;" 
            align='<%= Common.ChangeTDForDefaultLeft()%>'>
        <asp:Button ID="btnsave" runat="server" CssClass="buttonCommman" 
            OnClick="btnsave_Click" Text="Save" />&nbsp;&nbsp; &nbsp;&nbsp;
        <asp:Button ID="btnreset" runat="server" CssClass="buttonCommman" 
            OnClick="btnreset_Click" Text="Reset" />&nbsp;&nbsp; &nbsp;&nbsp;
     <asp:Button ID="btncencel" runat="server" Text="Cancel" OnClick="btncencel_Click" 
            CssClass="buttonCommman"/>
    </td>
    <td style="height: 50px">
        </td>
    </tr>
   
    </table>
      
                                        
      </asp:Panel>
      
       <asp:Panel ID="pnlpopup" runat="server" class="MsgOverlayAddress" >
    
    
    <asp:Panel ID="pnl2" runat="server" class="MsgPopUpPanelAddress" Visible="false">
     <asp:Panel ID="pnl3" runat="server" Style="width: 100%; height: 100%; text-align: center;">
 
    <table width="100%" style="background-image: url(../Images/bg_repeat.jpg); background-repeat: repeat;">
                            <tr>
                                <asp:Label ID="lblSelect" runat="server" Visible="false"></asp:Label>
                                <td align="<%= Common.ChangeTDForDefaultLeft()%>">
                                    <asp:Label ID="Label14" runat="server" Font-Size="14px" Font-Bold="true" CssClass="labelComman"
                                        Text="<%$ Resources:Attendance,File Upload %>"></asp:Label>
                                </td>
                                <td align="right">
                                    <asp:ImageButton ID="ImageButton5" runat="server" ImageUrl="~/Images/close.png" CausesValidation="False"
                                        Height="20px" Width="20px" OnClick="Imgbtn_Click" />
                                </td>
                            </tr>
                        </table>
    <table bgcolor="#ccddee">
    <tr>
    <td align='<%= Common.ChangeTDForDefaultLeft()%>'>
      <asp:Label ID="Label11" runat="server" Text="Directory Name" 
            CssClass="labelComman"></asp:Label>
    </td>
    <td align='<%= Common.ChangeTDForDefaultLeft()%>'>
        <asp:DropDownList ID="ddlDirctoryname" runat="server" CssClass="DropdownSearch" 
            onselectedindexchanged="ddlDirctoryname_SelectedIndexChanged" 
            AutoPostBack="True">
        </asp:DropDownList>
    </td>
    </tr>
    
    <tr>
    <td align='<%= Common.ChangeTDForDefaultLeft()%>'>
     <asp:Label ID="Label13" runat="server" Text="File Name" CssClass="labelComman"></asp:Label>
    </td>
    <td align='<%= Common.ChangeTDForDefaultLeft()%>'>
    <asp:DropDownList ID="ddlFilename" runat="server" CssClass="DropdownSearch" 
            
            AutoPostBack="True">
        </asp:DropDownList>
    </td>
    </tr>
    <tr>
    <td align="center" colspan="4">
        <asp:Button ID="btnsubmitpopup" runat="server" Text="Submit" 
            OnClick="btnsubmitpopup_Click" CssClass="buttonCommman"/>
            &nbsp;&nbsp;
     <asp:Button ID="btncencelpnl" runat="server" Text="Cancel" OnClick="btncencelpnl_Click" 
            CssClass="buttonCommman"/>
        <asp:HiddenField ID="HidCustId" runat="server" />
        <asp:HiddenField ID="hdnfileid" runat="server" />
      
    </td>
    </tr>
    </table>
    </asp:Panel>
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
            <div id="processMessage" class="progressBackgroundFilter">
                Loading…<br />
                <br />
                <img alt="Loading" src="../Images/ajax-loader2.gif" class="processMessage" />
            </div>
        </ProgressTemplate>
    </asp:UpdateProgress>


</asp:Content>

