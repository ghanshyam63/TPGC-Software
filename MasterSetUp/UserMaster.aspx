<%@ Page Language="C#" MasterPageFile="~/ERPMaster.master" AutoEventWireup="true" CodeFile="UserMaster.aspx.cs" Inherits="MasterSetUp_UserMaster" Title="User Master" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
   <link href="../CSS/InvStyle.css" rel="stylesheet" type="text/css" />
    <asp:UpdatePanel ID="UpdatePanel1" runat="server">
        <Triggers>
            <asp:PostBackTrigger ControlID="btnList" />
            <asp:PostBackTrigger ControlID="btnNew" />
            <asp:PostBackTrigger ControlID="btnBin" />
            <asp:PostBackTrigger ControlID="gvUserMaster" />
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
                                                <asp:Label ID="lblHeader" runat="server" Text="<%$ Resources:Attendance,User Setup %>"
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
                                                <asp:Panel ID="pnlMenuBin" runat="server" CssClass="a" Width="100%">
                                                    <asp:Button ID="btnBin" runat="server" Text="<%$ Resources:Attendance,Bin %>" Width="90px"
                                                        BorderStyle="none" BackColor="Transparent" OnClick="btnBin_Click" Style="padding-left: 10px;
                                                        padding-top: 3px; background-image: url(  '../Images/Bin.png' ); background-repeat: no-repeat;
                                                        height: 49px; background-position: 5px 15px; font: bold 14px Trebuchet MS; color: #000000;" />
                                                </asp:Panel>
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
                                                            <asp:Label ID="lblSelectField" runat="server" Text="<%$ Resources:Attendance,Select Field %>"
                                                                CssClass="labelComman"></asp:Label>
                                                        </td>
                                                        <td width="180px">
                                                            <asp:DropDownList ID="ddlFieldName" runat="server" CssClass="DropdownSearch" Height="25px"
                                                                Width="170px">
                                                                 <asp:ListItem Text="<%$ Resources:Attendance,User Id %>" Value="User_Id"></asp:ListItem>
                                                                <asp:ListItem Text="<%$ Resources:Attendance,Role Name %>" Value="Role_Name"></asp:ListItem>
                                                               
                                                               
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
                                                        <td width="24%">
                                                            <asp:TextBox ID="txtValue" runat="server" CssClass="textCommanSearch" Height="14px"
                                                                Width="100%"></asp:TextBox>
                                                        </td>
                                                        <td width="50px" align="center">
                                                            <asp:ImageButton ID="btnbind" runat="server" CausesValidation="False" Height="25px"
                                                                ImageUrl="~/Images/search.png" OnClick="btnbind_Click" Width="25px" ToolTip="<%$ Resources:Attendance,Search %>">
                                                            </asp:ImageButton>
                                                        </td>
                                                        <td>
                                                            <asp:Panel ID="PnlRefresh" runat="server" DefaultButton="btnRefresh">
                                                                <asp:ImageButton ID="btnRefresh" runat="server" CausesValidation="False" Height="25px"
                                                                    ImageUrl="~/Images/refresh.png" OnClick="btnRefresh_Click" Width="25px" ToolTip="<%$ Resources:Attendance,Refresh %>">
                                                                </asp:ImageButton>
                                                            </asp:Panel>
                                                        </td>
                                                        <td align="center">
                                                            <asp:Label ID="lblTotalRecords" runat="server" Text="<%$ Resources:Attendance,Total Records: 0 %>"
                                                                CssClass="labelComman"></asp:Label>
                                                            <asp:Label ID="lblSelectedRecord" runat="server" Visible="false"></asp:Label>
                                                        </td>
                                                    </tr>
                                                </table>
                                            </div>
                                            <asp:HiddenField ID="HDFSort" runat="server" />
                                        </asp:Panel>
                                       
                               <asp:GridView ID="gvUserMaster" PageSize="<%# SystemParameter.GetPageSize() %>" runat="server"
                                            AutoGenerateColumns="False" Width="100%" AllowPaging="True" AllowSorting="True"
                                            CssClass="grid" OnPageIndexChanging="gvUserMaster_PageIndexChanging" OnSorting="gvUserMaster_OnSorting">
                                            <Columns>
                                              <asp:TemplateField HeaderText="<%$ Resources:Attendance,Edit %>">
                                                    <ItemTemplate>
                                                        <asp:ImageButton ID="btnEdit" runat="server" CommandArgument='<%# Eval("User_Id") %>'
                                                            ImageUrl="~/Images/edit.png" CausesValidation="False" OnCommand="btnEdit_Command"
                                                            Visible="false" ToolTip="<%$ Resources:Attendance,Edit %>" /></ItemTemplate>
                                                    <ItemStyle HorizontalAlign="Center" />
                                                 <ItemStyle CssClass="grid" />
                                                </asp:TemplateField>
                                               <asp:TemplateField HeaderText="<%$ Resources:Attendance,Delete %>">
                                                    <ItemTemplate>
                                                        <asp:ImageButton ID="IbtnDelete" runat="server" CausesValidation="False" CommandArgument='<%# Eval("User_Id") %>'
                                                            ImageUrl="~/Images/Erase.png" OnCommand="IbtnDelete_Command" Visible="false"
                                                            ToolTip="<%$ Resources:Attendance,Delete %>" />
                                                    </ItemTemplate>
                                                    <ItemStyle HorizontalAlign="Center" />
                                                    <ItemStyle CssClass="grid" />
                                                </asp:TemplateField>
                                                 <asp:TemplateField HeaderText="<%$ Resources:Attendance,User Id %>" SortExpression="User_Id">
                                                    <ItemTemplate>
                                        
                                                        <asp:Label ID="lblUserId1"  runat="server" Text='<%# Eval("User_Id") %>'></asp:Label>
                                                        
                                                        
                                                    </ItemTemplate>
                                                    <ItemStyle CssClass="grid" />
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="<%$ Resources:Attendance,Role Name %>" SortExpression="Role_Name">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lbUserName" runat="server" Text='<%# Eval("Role_Name") %>'></asp:Label>
                                                        
                                                        <asp:Label ID="lblRoleId" Visible="false" runat="server" Text='<%# Eval("Role_Id") %>'></asp:Label>
                                                        
                                                        
                                                        
                                                    </ItemTemplate>
                                                    <ItemStyle CssClass="grid" />
                                                </asp:TemplateField>
                                               
                                                
                                            
                                            </Columns>
                                            <AlternatingRowStyle CssClass="InvgridAltRow"></AlternatingRowStyle>
                                            <HeaderStyle CssClass="Invgridheader" />
                                            <PagerStyle CssClass="Invgridheader" />
                                            <RowStyle CssClass="Invgridrow" HorizontalAlign="Center"></RowStyle>
                                        </asp:GridView>
                                       </asp:Panel>
                                    <asp:Panel ID="PnlNewEdit" runat="server" DefaultButton="btnSave">
                                    <asp:Panel ID="PnlUser" runat="server" >
                                    
                                    
                                    <table width="100%" style="padding-left: 43px">
                                    
                                    <tr>
                                    <td valign="top" >
                                    <table>
                                    
                                    <tr id="trSuperUser" runat="server" >
                                    <td colspan="3" align="center" >
                                    <asp:RadioButton ID="rbtnSuperAdmin" runat="server" GroupName="admin" Text="<%$ Resources:Attendance,Super Admin %>" CssClass="labelComman"  AutoPostBack="true" OnCheckedChanged="rbtnUserCheckedChanged" />
                                    <asp:RadioButton ID="rbtnUser" runat="server" GroupName="admin"  Text="<%$ Resources:Attendance,User %>" CssClass="labelComman"  AutoPostBack="true" OnCheckedChanged="rbtnUserCheckedChanged" />
                                    
                                    
                                    </td>
                                    </tr>
            <tr id="trEmp" runat="server" >
                <td width="170px" align="left" >
                    <asp:Label ID="Label1"  runat="server" CssClass="labelComman" Text="<%$ Resources:Attendance,Employee %>"></asp:Label>
                </td>
                <td width="1px">
                    :
                </td>
                <td align="left">
                    <asp:TextBox ID="txtEmp" OnTextChanged="ddlEmp_TextChanged" AutoPostBack="true" TabIndex ="104" BackColor="#e3e3e3" Width="250px" runat="server" CssClass="textComman"  />
                <cc1:AutoCompleteExtender ID="AutoCompleteExtender2" runat="server" DelimiterCharacters=""
                                                        Enabled="True" ServiceMethod="GetCompletionList" ServicePath="" CompletionInterval="100"
                                                        MinimumPrefixLength="1" CompletionSetCount="1" TargetControlID="txtEmp"
                                                        UseContextKey="True" CompletionListCssClass="completionList" CompletionListItemCssClass="listItem"
                                                        CompletionListHighlightedItemCssClass="itemHighlighted">
                                                    </cc1:AutoCompleteExtender>
                
                
                </td>
                 
            </tr>
           <tr>
                <td width="170px" align="left" >
                    <asp:Label ID="lblUserName"  runat="server" CssClass="labelComman" Text="<%$ Resources:Attendance,User Id %>"></asp:Label>
                </td>
                <td width="1px">
                    :
                </td>
                <td align="left">
                    <asp:TextBox ID="txtUserName" TabIndex ="104" BackColor="#e3e3e3" Width="250px" runat="server" CssClass="textComman"  />
                  <cc1:AutoCompleteExtender ID="AutoCompleteExtender1" runat="server" DelimiterCharacters=""
                                                        Enabled="True" ServiceMethod="GetCompletionListUser" ServicePath="" CompletionInterval="100"
                                                        MinimumPrefixLength="1" CompletionSetCount="1" TargetControlID="txtUserName"
                                                        UseContextKey="True" CompletionListCssClass="completionList" CompletionListItemCssClass="listItem"
                                                        CompletionListHighlightedItemCssClass="itemHighlighted">
                                                    </cc1:AutoCompleteExtender>
                </td>
                 
            </tr>
             <tr>
                <td width="170px" align="left" >
                    <asp:Label ID="lblUserNameL" runat="server" CssClass="labelComman" Text="<%$ Resources:Attendance,Password %>"></asp:Label>
                </td>
                <td width="1px">
                    :
                </td>
                <td align="left">
                    <asp:TextBox ID="txtPassword" TextMode="Password"  TabIndex ="105" Width="250px" runat="server" CssClass="textComman"  />
                 
                </td>
                
            </tr>
            <tr id="trRole" runat="server" >
                <td width="170px" align="left" >
                    <asp:Label ID="Label2" runat="server" CssClass="labelComman" Text="<%$ Resources:Attendance,Role Id %>"></asp:Label>
                </td>
                <td width="1px">
                    :
                </td>
                <td align="left">
                       <asp:DropDownList ID="ddlRole" TabIndex="106"  Width="262px" runat="server"  CssClass="textComman" AutoPostBack="true"  OnSelectedIndexChanged="ddlRole_SelectIndexChanged" />
                    
                    
                </td>
                
            </tr>
          
            <tr id="trEditRole" runat="server" >
                <td width="170px" align="left" >
                   
                </td>
                <td width="1px">
                    :
                </td>
                <td align="left">
                     
                     <asp:CheckBox ID="chkEditRole" TabIndex="106" Text="<%$ Resources:Attendance,Edit Role %>"  CssClass="labelComman"  runat="server" AutoPostBack="true" OnCheckedChanged="chkEditRoleCheckedChanged"  />
                </td>
                
            </tr>
           <tr>
                                               <td>
                                               </td>
                                               <td></td>
                                                <td align="left"    >
                                                    <table style="padding-left: 8px">
                                                        <tr>
                                                            <td width="80px">
                                                                <asp:Button ID="btnSave" runat="server" TabIndex ="107" 
                                                                    Text="<%$ Resources:Attendance,Save %>" Visible="false" CssClass="buttonCommman"
                                                                    ValidationGroup="a" onclick="btnSave_Click" />
                                                            </td>
                                                            <td width="80px">
                                                                <asp:Button ID="btnReset" runat="server" TabIndex ="108" 
                                                                    Text="<%$ Resources:Attendance,Reset %>" CssClass="buttonCommman"
                                                                    CausesValidation="False" onclick="btnReset_Click" />
                                                            </td>
                                                            <td width="80px">
                                                                <asp:Button ID="btnCancel" runat="server" TabIndex ="109" 
                                                                    Text="<%$ Resources:Attendance,Cancel %>" CssClass="buttonCommman"
                                                                     CausesValidation="False" onclick="btnCancel_Click" />
                                                            </td>
                                                        </tr>
                                                    </table>
                                                    <asp:HiddenField ID="editid" runat="server" />
                                                    <asp:HiddenField ID="hdnRoleId" runat="server" />
                                                </td>
                                                
                                            </tr>
           
           </table>
                                    
                                    
                                    </td>
                                    <td>
                                    
                                    <asp:Panel ID="PnlUserPerm" runat="server">
                                            <table width="100%" style="padding-left: 43px">
                                    <tr>
                                    <td align="center" >
                                    <asp:Label ID="Label3" Font-Bold="true"  runat="server" CssClass="labelComman" Text="<%$ Resources:Attendance,User Permission %>"></asp:Label>
                                    
                                    </td>
                                    </tr>
                                    <tr>
                                    <td>
                                    
                                      <asp:Panel  ID="pnlRoleP" runat="server">
                                            <table width="100%" style="padding-left: 43px">
           
           
             <tr>
               
       
            
          <td colspan="3" >
          
          
          <asp:Panel ID="pnlTreeView" runat="server" Width="100%" Height="500px" ScrollBars="Both"
                                                    HorizontalAlign="Left"> 
                                                    <asp:TreeView ID="navTree" runat="server"  Height="100%" CssClass="labelComman"
                                                        OnSelectedNodeChanged="navTree_SelectedNodeChanged1" OnTreeNodeCheckChanged="navTree_TreeNodeCheckChanged"
                                                        ShowCheckBoxes="All">
                                                    </asp:TreeView>
                                                </asp:Panel>
          
          
          
          
          
          
          
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
                                    
                                    
                                    </asp:Panel>
                                        
                                        
                                         
                                        
                                        
                                    </asp:Panel>
                                    <asp:Panel ID="PnlBin" runat="server">
                                        <asp:Panel ID="pnlbinsearch" runat="server" DefaultButton="btnbinbind">
                                            <div width="100%" style="background-image: url(../Images/bg_repeat.jpg); background-repeat: repeat;">
                                                <table width="100%" style="padding-left: 20px; height: 38px">
                                                    <tr>
                                                        <td width="90px">
                                                            <asp:Label ID="lblbinSelectField" runat="server" Text="<%$ Resources:Attendance,Select Field %>"
                                                                CssClass="labelComman"></asp:Label>
                                                        </td>
                                                        <td width="180px">
                                                            <asp:DropDownList ID="ddlbinFieldName" runat="server" CssClass="DropdownSearch" Height="25px"
                                                                Width="170px">
                                                                <asp:ListItem Text="<%$ Resources:Attendance,User Id %>" Value="User_Id"></asp:ListItem>
                                                                <asp:ListItem Text="<%$ Resources:Attendance,Role Name %>" Value="Role_Name"></asp:ListItem>
                                                               
                       </asp:DropDownList>
                                                        </td>
                                                        <td width="135px">
                                                            <asp:DropDownList ID="ddlbinOption" runat="server" CssClass="DropdownSearch" Height="25px"
                                                                Width="120px">
                                                                <asp:ListItem Text="<%$ Resources:Attendance,--Select-- %>"></asp:ListItem>
                                                                <asp:ListItem Text="<%$ Resources:Attendance,Equal %>"></asp:ListItem>
                                                                 <asp:ListItem Selected="True" Text="<%$ Resources:Attendance,Contains %>"></asp:ListItem>
                                                    
                                                                <asp:ListItem Text="<%$ Resources:Attendance,Like %>"></asp:ListItem>
                                                                       </asp:DropDownList>
                                                        </td>
                                                        <td width="24%">
                                                            <asp:TextBox ID="txtbinValue" runat="server" CssClass="textCommanSearch" Height="14px"
                                                                Width="100%"></asp:TextBox>
                                                        </td>
                                                        <td width="50px" align="center">
                                                            <asp:ImageButton ID="btnbinbind" runat="server" CausesValidation="False" Height="25px"
                                                                ImageUrl="~/Images/search.png" OnClick="btnbinbind_Click" Width="25px" ToolTip="<%$ Resources:Attendance,Search %>">
                                                            </asp:ImageButton>
                                                        </td>
                                                        <td>
                                                            <asp:Panel ID="Panel2" runat="server" DefaultButton="btnbinRefresh">
                                                                <asp:ImageButton ID="btnbinRefresh" runat="server" CausesValidation="False" Height="25px"
                                                                    ImageUrl="~/Images/refresh.png" OnClick="btnbinRefresh_Click" Width="25px" ToolTip="<%$ Resources:Attendance,Refresh %>">
                                                                </asp:ImageButton>
                                                            </asp:Panel>
                                                        </td>
                                                        <td width="30px" align="center">
                                                            <asp:Panel ID="pnlimgBtnRestore" runat="server" DefaultButton="imgBtnRestore">
                                                                <asp:ImageButton ID="imgBtnRestore" Height="25px" Width="25px" CausesValidation="False"
                                                                    Visible="false" runat="server" ImageUrl="~/Images/active.png" OnClick="imgBtnRestore_Click"
                                                                    ToolTip="<%$ Resources:Attendance, Active %>" />
                                                            </asp:Panel>
                                                        </td>
                                                        <td>
                                                            <asp:Panel ID="pnlImgbtnSelectAll" runat="server" DefaultButton="ImgbtnSelectAll">
                                                                <asp:ImageButton ID="ImgbtnSelectAll" runat="server" OnClick="ImgbtnSelectAll_Click"
                                                                    Visible="false" ToolTip="<%$ Resources:Attendance, Select All %>" AutoPostBack="true"
                                                                    ImageUrl="~/Images/selectAll.png" />
                                                            </asp:Panel>
                                                        </td>
                                                        <td align="center">
                                                            <asp:Label ID="lblbinTotalRecords" runat="server" Text="<%$ Resources:Attendance,Total Records: 0 %>"
                                                                CssClass="labelComman"></asp:Label>
                                                        </td>
                                                    </tr>
                                                </table>
                                            </div>
                                        </asp:Panel>
                                        
                                        <asp:GridView ID="gvUserMasterBin" PageSize="<%# SystemParameter.GetPageSize() %>"
                                            runat="server" AutoGenerateColumns="False" Width="100%" AllowPaging="True" OnPageIndexChanging="gvUserMasterBin_PageIndexChanging"
                                            OnSorting="gvUserMasterBin_OnSorting" AllowSorting="true" CssClass="grid">
                                            <Columns>
                                                <asp:TemplateField>
                                                    <ItemTemplate>
                                                        <asp:CheckBox ID="chkgvSelect" runat="server" OnCheckedChanged="chkgvSelect_CheckedChanged"
                                                            AutoPostBack="true" />
                                                    </ItemTemplate>
                                                    <HeaderTemplate>
                                                        <asp:CheckBox ID="chkgvSelectAll" runat="server" OnCheckedChanged="chkgvSelectAll_CheckedChanged"
                                                            AutoPostBack="true" />
                                                    </HeaderTemplate>
                                                    <ItemStyle HorizontalAlign="Center" />
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="<%$ Resources:Attendance,User Id %>" SortExpression="User_Id">
                                                    <ItemTemplate>
                                        
                                                        <asp:Label ID="lblUserId"  runat="server" Text='<%# Eval("User_Id") %>'></asp:Label>
                                                        
                                                        
                                                    </ItemTemplate>
                                                    <ItemStyle CssClass="grid" />
                                                </asp:TemplateField>
                                              <asp:TemplateField HeaderText="<%$ Resources:Attendance,Role Name %>" SortExpression="Role_Name">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lbUserName" runat="server" Text='<%# Eval("Role_Name") %>'></asp:Label>
                                                        
                                                        <asp:Label ID="lblRoleId" Visible="false" runat="server" Text='<%# Eval("Role_Id") %>'></asp:Label>
                                                        
                                                        
                                                        
                                                    </ItemTemplate>
                                                    <ItemStyle CssClass="grid" />
                                                </asp:TemplateField>
                                                
                                                
                                          
                                                 
                                            </Columns>
                                            <AlternatingRowStyle CssClass="InvgridAltRow"></AlternatingRowStyle>
                                            <HeaderStyle CssClass="Invgridheader" />
                                            <PagerStyle CssClass="Invgridheader" />
                                            <RowStyle CssClass="Invgridrow" HorizontalAlign="Center"></RowStyle>
                                        </asp:GridView>
                                        <asp:HiddenField ID="HDFSortbin" runat="server" />
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


