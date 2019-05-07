<%@ Page Language="C#" MasterPageFile="~/ERPMaster.master" AutoEventWireup="true" CodeFile="DeviceMaster.aspx.cs" Inherits="Attendance_DeviceMaster" Title="Device Setup" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
   <link href="../CSS/InvStyle.css" rel="stylesheet" type="text/css" />
<link href="../CSS/AJAX.css" rel="stylesheet" type="text/css" />
    <asp:UpdatePanel ID="UpdatePanel1" runat="server">
        <Triggers>
            <asp:PostBackTrigger ControlID="btnList" />
            <asp:PostBackTrigger ControlID="btnNew" />
            <asp:PostBackTrigger ControlID="btnBin" />
            <asp:PostBackTrigger ControlID="btnSave" />
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
                                                <asp:Label ID="lblHeader" runat="server" Text="<%$ Resources:Attendance,Device Setup %>"
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
                                                                <asp:ListItem Selected="True" Text="<%$ Resources:Attendance,Device Name %>" Value="Device_Name"></asp:ListItem>
                                                                <asp:ListItem Text="<%$ Resources:Attendance,Device Name(Local) %>" Value="Device_Name_L"></asp:ListItem>
                                                                                            
                                                                
                                                                <asp:ListItem Text="<%$ Resources:Attendance,Device Id %>" Value="Device_Id"></asp:ListItem>
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
                                       
                               <asp:GridView ID="gvDevice"  DataKeyNames="Device_Id,Port,IP_Address" PageSize="<%# SystemParameter.GetPageSize() %>" runat="server"
                                            AutoGenerateColumns="False" Width="100%" AllowPaging="True" AllowSorting="True"
                                            CssClass="grid" OnPageIndexChanging="gvDevice_PageIndexChanging" OnSorting="gvDevice_OnSorting">
                                            <Columns>
                                            
                                            
                                             <asp:TemplateField HeaderText="<%$ Resources:Attendance,Test Connect/Disconnect %>">
                                                                <HeaderStyle HorizontalAlign="Left" />
                                                               
                                                                <ItemTemplate>
                                                                    <asp:ImageButton ID="lnkConnect" runat="server"  ImageUrl="~/Images/connect.png"  CommandArgument='<%# Eval("Device_Id") %>'
                                                                        OnClick="lnkConnect_Click" ToolTip="<%$ Resources:Attendance,Connect%>"/>
                                                                </ItemTemplate>
                                                            </asp:TemplateField>
                                            
                                            
                                               <asp:TemplateField HeaderText="<%$ Resources:Attendance,Edit %>">
                                                    <ItemTemplate>
                                                        <asp:ImageButton ID="btnEdit" runat="server" CommandArgument='<%# Eval("Device_Id") %>'
                                                            ImageUrl="~/Images/edit.png" CausesValidation="False" OnCommand="btnEdit_Command"
                                                            Visible="false" ToolTip="<%$ Resources:Attendance,Edit %>" /></ItemTemplate>
                                                    <ItemStyle HorizontalAlign="Center" />
                                                 <ItemStyle CssClass="grid" />
                                                </asp:TemplateField>
                                                 <asp:TemplateField HeaderText="<%$ Resources:Attendance,Delete %>">
                                                    <ItemTemplate>
                                                        <asp:ImageButton ID="IbtnDelete" runat="server" CausesValidation="False" CommandArgument='<%# Eval("Device_Id") %>'
                                                            ImageUrl="~/Images/Erase.png" OnCommand="IbtnDelete_Command" Visible="false"
                                                            ToolTip="<%$ Resources:Attendance,Delete %>" />
                                                    </ItemTemplate>
                                                    <ItemStyle HorizontalAlign="Center" />
                                                    <ItemStyle CssClass="grid" />
                                                </asp:TemplateField>
                                                 <asp:TemplateField HeaderText="<%$ Resources:Attendance,Device Id %>" SortExpression="Device_Id">
                                                    <ItemTemplate>
                                        
                                                        <asp:Label ID="lblDeviceId1"  runat="server" Text='<%# Eval("Device_Id") %>'></asp:Label>
                                                        
                                                        
                                                    </ItemTemplate>
                                                    <ItemStyle CssClass="grid" />
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="<%$ Resources:Attendance,Device Name %>" SortExpression="Device_Name">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lbDeviceName" runat="server" Text='<%# Eval("Device_Name") %>'></asp:Label>
                                                    </ItemTemplate>
                                                    <ItemStyle CssClass="grid" />
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="<%$ Resources:Attendance,Device Name(Local) %>" SortExpression="Device_Name_L">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblDeviceNameL" runat="server" Text='<%# Eval("Device_Name_L") %>'></asp:Label>
                                                    </ItemTemplate>
                                                    <ItemStyle CssClass="grid" />
                                                </asp:TemplateField>
                                                
                                                
                                              <asp:TemplateField HeaderText="<%$ Resources:Attendance,IP Address %>" SortExpression="IP_Address">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblFromDate" runat="server" Text='<%# Eval("IP_Address") %>'></asp:Label>
                                                    </ItemTemplate>
                                                    <ItemStyle CssClass="grid" />
                                                </asp:TemplateField>
                                                
                                                 <asp:TemplateField HeaderText="<%$ Resources:Attendance,Port Number %>" SortExpression="Port">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lbltoDate" runat="server" Text='<%# Eval("Port") %>'></asp:Label>
                                                    </ItemTemplate>
                                                    <ItemStyle CssClass="grid" />
                                                </asp:TemplateField>
                                                
                                                
                                                
                                                <asp:TemplateField HeaderText="<%$ Resources:Attendance,Brand %>" SortExpression="Brand_Name">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblBrand2" runat="server" Text='<%# Eval("Brand_Name") %>'></asp:Label>
                                                    </ItemTemplate>
                                                    <ItemStyle CssClass="grid" />
                                                </asp:TemplateField>
                                                
                                                
                                                
                                                <asp:TemplateField HeaderText="<%$ Resources:Attendance,Location %>" SortExpression="Location_Name">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblLoc2" runat="server" Text='<%# Eval("Location_Name") %>'></asp:Label>
                                                    </ItemTemplate>
                                                    <ItemStyle CssClass="grid" />
                                                </asp:TemplateField>
                                                
                                                <asp:TemplateField HeaderText="<%$ Resources:Attendance,Communication Type %>" SortExpression="Communication_Type">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lbltoDate1" runat="server" Text='<%# Eval("Communication_Type") %>'></asp:Label>
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
                                        <table width="100%" style="padding-left: 43px">
       
             <tr>
                <td width="170px" align='<%= Common.ChangeTDForDefaultLeft()%>' >
                    <asp:Label ID="lblDeviceName"  runat="server" CssClass="labelComman" Text="<%$ Resources:Attendance,Device Name %>"></asp:Label>
                </td>
                <td width="1px">
                    :
                </td>
                <td align='<%= Common.ChangeTDForDefaultLeft()%>'>
                    <asp:TextBox ID="txtDeviceName"  BackColor="#e3e3e3" Width="250px" runat="server" CssClass="textComman" AutoPostBack="true" OnTextChanged="txtDeviceName_OnTextChanged" />
                  <cc1:AutoCompleteExtender ID="AutoCompleteExtender1" runat="server" DelimiterCharacters=""
                                                        Enabled="True" ServiceMethod="GetCompletionListDeviceName" ServicePath="" CompletionInterval="100"
                                                        MinimumPrefixLength="1" CompletionSetCount="1" TargetControlID="txtDeviceName"
                                                        UseContextKey="True" CompletionListCssClass="completionList" CompletionListItemCssClass="listItem"
                                                        CompletionListHighlightedItemCssClass="itemHighlighted">
                                                    </cc1:AutoCompleteExtender>
                </td>
                
                 <td width="170px" align='<%= Common.ChangeTDForDefaultLeft()%>' >
                    <asp:Label ID="lblDeviceNameL" runat="server" CssClass="labelComman" Text="<%$ Resources:Attendance,Device Name(Local) %>"></asp:Label>
                </td>
                <td width="1px">
                    :
                </td>
                <td align='<%= Common.ChangeTDForDefaultLeft()%>'>
                    <asp:TextBox ID="txtDeviceNameL" Width="250px" runat="server" CssClass="textComman"  />
                 
                </td>
                
                
                
                
                
                
                
            </tr>
             
            
          
                  <tr>
                <td width="170px" align='<%= Common.ChangeTDForDefaultLeft()%>' >
                    <asp:Label ID="lblDeviceCode" runat="server" CssClass="labelComman" Text="<%$ Resources:Attendance,IP Address %>"></asp:Label>
                </td>
                <td width="1px">
                    :
                </td>
                <td align='<%= Common.ChangeTDForDefaultLeft()%>'>
                 
                  <asp:TextBox ID="txtIPAddress"  Width="250px" runat="server" CssClass="textComman"   />

   <cc1:FilteredTextBoxExtender ID="FilteredTextBoxExtender5" runat="server" Enabled="True"
                                                                        TargetControlID="txtIPAddress" ValidChars="1,2,3,4,5,6,7,8,9,0,.">
                                                                    </cc1:FilteredTextBoxExtender>
                                                  <cc1:AutoCompleteExtender ID="AutoCompleteExtender2" runat="server" DelimiterCharacters=""
                                                        Enabled="True" ServiceMethod="GetCompletionListIpAddress" ServicePath="" CompletionInterval="100"
                                                        MinimumPrefixLength="1" CompletionSetCount="1" TargetControlID="txtIPAddress"
                                                        UseContextKey="True" CompletionListCssClass="completionList" CompletionListItemCssClass="listItem"
                                                        CompletionListHighlightedItemCssClass="itemHighlighted">
                                                    </cc1:AutoCompleteExtender>
                </td>
                <td width="170px" align='<%= Common.ChangeTDForDefaultLeft()%>' >
                    <asp:Label ID="Label1" runat="server" CssClass="labelComman" Text="<%$ Resources:Attendance,Port Number %>"></asp:Label>
                </td>
                <td width="1px">
                    :
                </td>
                <td align='<%= Common.ChangeTDForDefaultLeft()%>'>
                 
               <asp:TextBox ID="txtPortNumber"  Width="250px" runat="server" CssClass="textComman" />
                 <cc1:FilteredTextBoxExtender ID="FilteredTextBoxExtender1" runat="server" Enabled="True"
                                                                        TargetControlID="txtPortNumber" ValidChars="1,2,3,4,5,6,7,8,9,0">
                                                                    </cc1:FilteredTextBoxExtender>
                </td>
                
            </tr>
          
              
            <tr>
                <td width="170px" align='<%= Common.ChangeTDForDefaultLeft()%>' >
                    <asp:Label ID="Label2" runat="server" CssClass="labelComman" Text="<%$ Resources:Attendance,Communication Type %>"></asp:Label>
                </td>
                <td width="1px">
                    :
                </td>
                <td align='<%= Common.ChangeTDForDefaultLeft()%>'>
                 
                <asp:DropDownList ID="ddlCommType" runat="server" Height="30px"  CssClass="DropdownSearch"  Width="262px">
                                                        <asp:ListItem Value="Serial Port/RS485" Text="Serial Port/RS485" ></asp:ListItem>
                                                        <asp:ListItem Value="Ethernet" Text="Ethernet">  </asp:ListItem>
                                                        <asp:ListItem Value="USB"  Text="USB"></asp:ListItem>
                                                    </asp:DropDownList>
                                                 
                </td>
                
                
            </tr>
             
            <tr>
                                           
                                                <td colspan="6"  align="center"    >
                                                    <table style="padding-left: 8px">
                                                        <tr>
                                                            <td width="80px">
                                                                <asp:Button ID="btnSave" runat="server"
                                                                    Text="<%$ Resources:Attendance,Save %>" Visible="false" CssClass="buttonCommman"
                                                                    ValidationGroup="a" onclick="btnSave_Click" />
                                                            </td>
                                                            <td width="80px">
                                                                <asp:Button ID="btnReset" runat="server"  
                                                                    Text="<%$ Resources:Attendance,Reset %>" CssClass="buttonCommman"
                                                                    CausesValidation="False" onclick="btnReset_Click" />
                                                            </td>
                                                            <td width="80px">
                                                                <asp:Button ID="btnCancel" runat="server" 
                                                                    Text="<%$ Resources:Attendance,Cancel %>" CssClass="buttonCommman"
                                                                     CausesValidation="False" onclick="btnCancel_Click" />
                                                            </td>
                                                        </tr>
                                                    </table>
                                                    <asp:HiddenField ID="editid" runat="server" />
                                                </td>
                                               
                                            </tr>
            
        </table>
                                        
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
                                                               <asp:ListItem Selected="True" Text="<%$ Resources:Attendance,Device Name %>" Value="Device_Name"></asp:ListItem>
                                                                <asp:ListItem Text="<%$ Resources:Attendance,Device Name(Local) %>" Value="Device_Name_L"></asp:ListItem>
                                                                
                                                                 <asp:ListItem Text="<%$ Resources:Attendance,Device Id %>" Value="Device_Id"></asp:ListItem>
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
                                        
                                        <asp:GridView ID="gvDeviceBin" PageSize="<%# SystemParameter.GetPageSize() %>"
                                            runat="server" AutoGenerateColumns="False" Width="100%" AllowPaging="True" OnPageIndexChanging="gvDeviceBin_PageIndexChanging"
                                            OnSorting="gvDeviceBin_OnSorting" AllowSorting="true" CssClass="grid">
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
                                                <asp:TemplateField HeaderText="<%$ Resources:Attendance,Device Id %>" SortExpression="Device_Id">
                                                    <ItemTemplate>
                                        
                                                        <asp:Label ID="lblDeviceId"  runat="server" Text='<%# Eval("Device_Id") %>'></asp:Label>
                                                        
                                                        
                                                    </ItemTemplate>
                                                    <ItemStyle CssClass="grid" />
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="<%$ Resources:Attendance,Device Name %>" SortExpression="Device_Name">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lbDeviceName" runat="server" Text='<%# Eval("Device_Name") %>'></asp:Label>
                                                    </ItemTemplate>
                                                    <ItemStyle CssClass="grid" />
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="<%$ Resources:Attendance,Device Name(Local) %>" SortExpression="Device_Name_L">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblDeviceNameL" runat="server" Text='<%# Eval("Device_Name_L") %>'></asp:Label>
                                                    </ItemTemplate>
                                                    <ItemStyle CssClass="grid" />
                                                </asp:TemplateField>
                                                
                                                
                                              <asp:TemplateField HeaderText="<%$ Resources:Attendance,IP Address %>" SortExpression="IP_Address">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblFromDate" runat="server" Text='<%# Eval("IP_Address") %>'></asp:Label>
                                                    </ItemTemplate>
                                                    <ItemStyle CssClass="grid" />
                                                </asp:TemplateField>
                                                
                                                 <asp:TemplateField HeaderText="<%$ Resources:Attendance,Port Number %>" SortExpression="Port">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lbltoDate" runat="server" Text='<%# Eval("Port") %>'></asp:Label>
                                                    </ItemTemplate>
                                                    <ItemStyle CssClass="grid" />
                                                    </asp:TemplateField>
                                                    
                                                    
                                                     <asp:TemplateField HeaderText="<%$ Resources:Attendance,Brand %>" SortExpression="Brand_Name">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblBrand2" runat="server" Text='<%# Eval("Brand_Name") %>'></asp:Label>
                                                    </ItemTemplate>
                                                    <ItemStyle CssClass="grid" />
                                                </asp:TemplateField>
                                                
                                                
                                                
                                                <asp:TemplateField HeaderText="<%$ Resources:Attendance,Location %>" SortExpression="Location_Name">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblLoc2" runat="server" Text='<%# Eval("Location_Name") %>'></asp:Label>
                                                    </ItemTemplate>
                                                    <ItemStyle CssClass="grid" />
                                                </asp:TemplateField>
                                                
                                                
                                                <asp:TemplateField HeaderText="<%$ Resources:Attendance,Communication Type %>" SortExpression="Communication_Type">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lbltoDate1" runat="server" Text='<%# Eval("Communication_Type") %>'></asp:Label>
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


