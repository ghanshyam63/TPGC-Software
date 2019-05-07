<%@ Page Title="" Language="C#" MasterPageFile="~/ERPMaster.master" AutoEventWireup="true" CodeFile="ProjectTeam.aspx.cs" Inherits="ProjectManagement_ProjectTeam" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
 <link href="../CSS/InvStyle.css" rel="stylesheet" type="text/css" />

<asp:UpdatePanel ID="UpdatePanel2" runat="server" UpdateMode="Conditional">
       <%-- <Triggers>
            <asp:PostBackTrigger ControlID="btnLeave" />
            <asp:PostBackTrigger ControlID="btnReset" />
            <asp:PostBackTrigger ControlID="btnCancel" />
            <asp:PostBackTrigger ControlID="btnSave" />
            <asp:PostBackTrigger ControlID="btnLeast" />
            <asp:PostBackTrigger ControlID="BtnBindList" />
            <asp:PostBackTrigger ControlID="BtnRefreshList" />
            <asp:PostBackTrigger ControlID="BtnUpdate" />
            <asp:PostBackTrigger ControlID="BtnResetPenalty" />
           <asp:PostBackTrigger ControlID="gvEmployee" />
            <asp:PostBackTrigger ControlID="gvEmpLeave" />
          
        </Triggers>--%>
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
                                                <asp:Label ID="lblHeader" runat="server" Text="<%$ Resources:Attendance,Project Team %>"
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
    <asp:Panel ID="pnllist" runat="server" DefaultButton="btnbind">
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
                                                                
                                                                <asp:ListItem Text="<%$ Resources:Attendance, Project Name %>" Value="Project_Name"
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
                                                            <asp:Panel ID="PnlRefresh" runat="server" DefaultButton="btnRefresh">
                                                                <asp:ImageButton ID="btnRefresh" runat="server" CausesValidation="False" Height="25px"
                                                                    ImageUrl="~/Images/refresh.png"  Width="25px"
                                                                    ToolTip="<%$ Resources:Attendance,Refresh %>" onclick="btnRefresh_Click"></asp:ImageButton>
                                                            </asp:Panel>
                                                        </td>
                                                        <td align='<%= Common.ChangeTDForDefaultLeft()%>' colspan="9">
                                                            <asp:Label ID="lblTotalRecords" runat="server" Text="<%$ Resources:Attendance,Total Records: 0 %>"
                                                                CssClass="labelComman"></asp:Label>
                                                        </td>
                                                    </tr>
                                                </table>
                                            </div>
                                        
                                        <asp:GridView ID="GvrProjectteam" runat="server" AutoGenerateColumns="False"
                                            Width="100%" AllowPaging="True" OnPageIndexChanging="GvrProjectteam_PageIndexChanging"
                                            AllowSorting="True" CssClass="grid" 
                                            onsorting="GvrProjectteam_Sorting">
                                            <AlternatingRowStyle CssClass="InvgridAltRow" />
                                            <Columns>
                                              <asp:TemplateField HeaderText="<%$ Resources:Attendance,Edit %>">
                                                    <ItemTemplate>
                                                         <asp:ImageButton ID="btnEdit" runat="server" CommandArgument='<%# Eval("Project_Id") %>'
                                                            ImageUrl="~/Images/edit.png" OnCommand="btnEdit_Command" Visible="True" CausesValidation="False"
                                                            ToolTip="<%$ Resources:Attendance,Edit %>" onclick="btnEdit_Click" 
                                                             style="width: 14px" /></ItemTemplate>
                                                    <ItemStyle HorizontalAlign="Center" />
                                                    <ItemStyle CssClass="grid" />
                                                </asp:TemplateField>
                                               
                                                <asp:TemplateField HeaderText="<%$ Resources:Attendance,Project Name %>" SortExpression="Project_Name">
                                                    <ItemTemplate>
                                                     <asp:HiddenField ID="HiddeniD" runat="server" />    
                                                      <asp:HiddenField ID="hdnproID" runat="server" value='<%# Eval("Project_Id") %>'/>   
                                                        <asp:Label ID="lblprojectIdList" runat="server" Text='<%# Eval("Project_Name") %>'></asp:Label>
                                                    </ItemTemplate>
                                                    <ItemStyle CssClass="grid" />
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="<%$ Resources:Attendance,Total Team Member %>" SortExpression="counters">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblEmpnameList" runat="server" Text='<%# Eval("counters") %>'></asp:Label>
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
                                     
                                    <asp:HiddenField ID="HiddeniD" runat="server" />       </asp:Panel>    
                                    
                                              
      <asp:Panel ID="pnlteamdetials" runat="server" Width="100%" Visible="false">
      
      <table bgcolor="#ccddee" Width="100%">
    <tr>
    <td align='<%= Common.ChangeTDForDefaultLeft()%>'>
      <asp:Label ID="Label10" runat="server" Text="Project Name" 
            CssClass="labelComman"></asp:Label>
    </td>
    <td align='<%= Common.ChangeTDForDefaultLeft()%>'>
        <asp:DropDownList ID="ddlprojectname" runat="server" CssClass="DropdownSearch" 
            onselectedindexchanged="ddlprojectname_SelectedIndexChanged" 
            AutoPostBack="True">
        </asp:DropDownList>
    </td>
    </tr>
    
    <tr>
    <td align='<%= Common.ChangeTDForDefaultLeft()%>' style="height: 26px">
     <asp:Label ID="Label11" runat="server" Text="Employee Name" CssClass="labelComman"></asp:Label>
    </td>
    <td align='<%= Common.ChangeTDForDefaultLeft()%>' style="height: 26px">
    <asp:DropDownList ID="ddlempname" runat="server" CssClass="DropdownSearch" 
            AutoPostBack="True">
        </asp:DropDownList>
    </td>
    </tr>
     <tr>
    <td align='<%= Common.ChangeTDForDefaultLeft()%>' style="height: 26px">
     <asp:Label ID="Label1" runat="server" Text="Task Visibility" CssClass="labelComman"></asp:Label>
    </td>
    <td>
        <asp:CheckBox ID="chktaskvisibility" runat="server" />
    </td>
    </tr>
    
    
    <tr>
    <td align="center"colspan="2">
        <asp:Button ID="btnsubmit" runat="server" Text="Save" 
            OnClick="btnsubmit_Click" CssClass="buttonCommman"/>
        &nbsp;
        &nbsp;
         <asp:Button ID="btndelete" runat="server" Text="Delete" 
            OnClick="btndelete_Click" CssClass="buttonCommman"/>
        
        <asp:HiddenField ID="HidCustId" runat="server" />
        <asp:HiddenField ID="hdnfileid" runat="server" />
      
    </td>
    </tr>
    </table>
    </asp:Panel>
       <asp:Panel ID="pnlgrid" runat="server" Width="100%" >
      
      
                                        <asp:GridView ID="grvteamlistDetailrecord" runat="server"  
                                            AllowSorting="true" AutoGenerateColumns="False"
                                            CssClass="grid" Width="100%" 
                                            PageSize="<%# SystemParameter.GetPageSize() %>" 
                                            OnPageIndexChanging="grvteamlistDetailrecord_PageIndexChanging"
                                            >
                                            
                                            <Columns>
                                              <asp:TemplateField HeaderText="<%$ Resources:Attendance,Edit %>">
                                                    <ItemTemplate>
                                                         <asp:ImageButton ID="btnEditGrid" runat="server" CommandArgument='<%# Eval("Trans_Id") %>'
                                                            ImageUrl="~/Images/edit.png" OnCommand="btnEditGrid_Command" Visible="True" CausesValidation="False"
                                                            ToolTip="<%$ Resources:Attendance,Edit %>" onclick="btnEditGrid_Click" 
                                                             style="width: 14px" /></ItemTemplate>
                                                    <ItemStyle HorizontalAlign="Center" />
                                                    <ItemStyle CssClass="grid" />
                                                </asp:TemplateField>
                                               <asp:TemplateField HeaderText="<%$ Resources:Attendance,Project Name %>"  >
                                                    <ItemTemplate>
                                                     <asp:HiddenField ID="hdnempid" runat="server" value='<%# Eval("Emp_Id") %>'/>
                                                     <asp:HiddenField ID="hdntrans" runat="server" value='<%# Eval("Trans_Id") %>'/>
                                                      <asp:HiddenField ID="hdnprojectid" runat="server" value='<%# Eval("Project_Id") %>'/>
                                                        <asp:Label ID="lblpojectname" runat="server" Text='<%# Eval("Project_Name") %>'></asp:Label>
                                                    </ItemTemplate>
                                                    <ItemStyle CssClass="grid" />
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="<%$ Resources:Attendance,Employee Name %>"  >
                                                    <ItemTemplate>
                                                    
                                                        <asp:Label ID="lblEmpIdList" runat="server" Text='<%# Eval("Emp_Name") %>'></asp:Label>
                                                    </ItemTemplate>
                                                    <ItemStyle CssClass="grid" />
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="<%$ Resources:Attendance,Visibility %>" >
                                                    <ItemTemplate>
                                                       
                                                        <asp:Label ID="lbltaskList" runat="server" Text='<%# Eval("Task_Visibility") %>'></asp:Label>
                                                    </ItemTemplate>
                                                    <ItemStyle CssClass="grid" />
                                                </asp:TemplateField>
                                        
                                                  
                                           
                                         
                                          
                                           
                                            </Columns>
                                            <AlternatingRowStyle CssClass="InvgridAltRow" />
                                            <HeaderStyle CssClass="Invgridheader" BorderStyle="Solid" BorderColor="#6E6E6E" BorderWidth="1px" />
                                            <PagerStyle CssClass="Invgridheader" />
                                            <RowStyle CssClass="Invgridrow" HorizontalAlign="Center" />
                                        </asp:GridView>
                                          <asp:HiddenField ID="HdfSortDetail" runat="server" />
                                          <asp:HiddenField ID="hidTransId" runat="server" />
                                           <asp:HiddenField ID="hidProId" runat="server" />
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

