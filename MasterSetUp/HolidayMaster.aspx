<%@ Page Language="C#" MasterPageFile="~/ERPMaster.master" AutoEventWireup="true" CodeFile="HolidayMaster.aspx.cs" Inherits="MasterSetUp_HolidayMaster" Title="Holiday Setup" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
   <link href="../CSS/InvStyle.css" rel="stylesheet" type="text/css" />
    <asp:UpdatePanel ID="UpdatePanel1" runat="server">
        <Triggers>
            <asp:PostBackTrigger ControlID="btnList" />
            <asp:PostBackTrigger ControlID="btnNew" />
            <asp:PostBackTrigger ControlID="btnBin" />
            <asp:PostBackTrigger ControlID="gvHolidayMaster" />
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
                                                <asp:Label ID="lblHeader" runat="server" Text="<%$ Resources:Attendance,Holiday Setup %>"
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
                                                                <asp:ListItem Selected="True" Text="<%$ Resources:Attendance,Holiday Name %>" Value="Holiday_Name"></asp:ListItem>
                                                                <asp:ListItem Text="<%$ Resources:Attendance,Holiday Name(Local) %>" Value="Holiday_Name_L"></asp:ListItem>
                                                                                            <asp:ListItem Text="<%$ Resources:Attendance,From Date %>" Value="From_Date"></asp:ListItem>
                                                               <asp:ListItem Text="<%$ Resources:Attendance,To Date %>" Value="To_Date"></asp:ListItem>
                                                           
                                                                
                                                                <asp:ListItem Text="<%$ Resources:Attendance,Holiday Id %>" Value="Holiday_Id"></asp:ListItem>
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
                                       
                               <asp:GridView ID="gvHolidayMaster" PageSize="<%# SystemParameter.GetPageSize() %>" runat="server"
                                            AutoGenerateColumns="False" Width="100%" AllowPaging="True" AllowSorting="True"
                                            CssClass="grid" OnPageIndexChanging="gvHolidayMaster_PageIndexChanging" OnSorting="gvHolidayMaster_OnSorting">
                                            <Columns>
                                               <asp:TemplateField HeaderText="<%$ Resources:Attendance,Edit %>">
                                                    <ItemTemplate>
                                                        <asp:ImageButton ID="btnEdit" runat="server" CommandArgument='<%# Eval("Holiday_Id") %>'
                                                            ImageUrl="~/Images/edit.png" CausesValidation="False" OnCommand="btnEdit_Command"
                                                            Visible="false" ToolTip="<%$ Resources:Attendance,Edit %>" /></ItemTemplate>
                                                    <ItemStyle HorizontalAlign="Center" />
                                                 <ItemStyle CssClass="grid" />
                                                </asp:TemplateField>
                                                 <asp:TemplateField HeaderText="<%$ Resources:Attendance,Delete %>">
                                                    <ItemTemplate>
                                                        <asp:ImageButton ID="IbtnDelete" runat="server" CausesValidation="False" CommandArgument='<%# Eval("Holiday_Id") %>'
                                                            ImageUrl="~/Images/Erase.png" OnCommand="IbtnDelete_Command" Visible="false"
                                                            ToolTip="<%$ Resources:Attendance,Delete %>" />
                                                    </ItemTemplate>
                                                    <ItemStyle HorizontalAlign="Center" />
                                                    <ItemStyle CssClass="grid" />
                                                </asp:TemplateField>
                                                 <asp:TemplateField HeaderText="<%$ Resources:Attendance,Holiday Id %>" SortExpression="Holiday_Id">
                                                    <ItemTemplate>
                                        
                                                        <asp:Label ID="lblHolidayId1"  runat="server" Text='<%# Eval("Holiday_Id") %>'></asp:Label>
                                                        
                                                        
                                                    </ItemTemplate>
                                                    <ItemStyle CssClass="grid" />
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="<%$ Resources:Attendance,Holiday Name %>" SortExpression="Holiday_Name">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lbHolidayName" runat="server" Text='<%# Eval("Holiday_Name") %>'></asp:Label>
                                                    </ItemTemplate>
                                                    <ItemStyle CssClass="grid" />
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="<%$ Resources:Attendance,Holiday Name(Local) %>" SortExpression="Holiday_Name_L">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblHolidayNameL" runat="server" Text='<%# Eval("Holiday_Name_L") %>'></asp:Label>
                                                    </ItemTemplate>
                                                    <ItemStyle CssClass="grid" />
                                                </asp:TemplateField>
                                                
                                                
                                              <asp:TemplateField HeaderText="<%$ Resources:Attendance,From Date %>" SortExpression="From_Date">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblFromDate" runat="server" Text='<%# GetDate(Eval("From_Date")) %>'></asp:Label>
                                                    </ItemTemplate>
                                                    <ItemStyle CssClass="grid" />
                                                </asp:TemplateField>
                                                
                                                 <asp:TemplateField HeaderText="<%$ Resources:Attendance,To Date %>" SortExpression="To_Date">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lbltoDate" runat="server" Text='<%# GetDate(Eval("To_Date")) %>'></asp:Label>
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
                                    <asp:Panel ID="PnlNewEdit" runat="server" >
                                      
                                      <asp:Panel ID="pnlHoliday" runat="server" DefaultButton="btnSave" >
                                      <table width="100%" style="padding-left: 43px">
       
             <tr>
                <td width="170px" align='<%= Common.ChangeTDForDefaultLeft()%>' >
                    <asp:Label ID="lblHolidayName"  runat="server" CssClass="labelComman" Text="<%$ Resources:Attendance,Holiday Name %>"></asp:Label>
                </td>
                <td width="1px">
                    :
                </td>
                <td align='<%= Common.ChangeTDForDefaultLeft()%>'>
                    <asp:TextBox ID="txtHolidayName" TabIndex ="104" BackColor="#e3e3e3" Width="250px" runat="server" CssClass="textComman" AutoPostBack="true" OnTextChanged="txtHolidayName_OnTextChanged" />
                  <cc1:AutoCompleteExtender ID="AutoCompleteExtender1" runat="server" DelimiterCharacters=""
                                                        Enabled="True" ServiceMethod="GetCompletionListHolidayName" ServicePath="" CompletionInterval="100"
                                                        MinimumPrefixLength="1" CompletionSetCount="1" TargetControlID="txtHolidayName"
                                                        UseContextKey="True" CompletionListCssClass="completionList" CompletionListItemCssClass="listItem"
                                                        CompletionListHighlightedItemCssClass="itemHighlighted">
                                                    </cc1:AutoCompleteExtender>
                </td>
                
                 <td width="170px" align='<%= Common.ChangeTDForDefaultLeft()%>' >
                    <asp:Label ID="lblHolidayNameL" runat="server" CssClass="labelComman" Text="<%$ Resources:Attendance,Holiday Name(Local) %>"></asp:Label>
                </td>
                <td width="1px">
                    :
                </td>
                <td align='<%= Common.ChangeTDForDefaultLeft()%>'>
                    <asp:TextBox ID="txtHolidayNameL" TabIndex ="105" Width="250px" runat="server" CssClass="textComman"  />
                 
                </td>
                
                
                
                
                
                
                
            </tr>
             
            
          
                  <tr>
                <td width="170px" align='<%= Common.ChangeTDForDefaultLeft()%>' >
                    <asp:Label ID="lblHolidayCode" runat="server" CssClass="labelComman" Text="<%$ Resources:Attendance,From Date %>"></asp:Label>
                </td>
                <td width="1px">
                    :
                </td>
                <td align='<%= Common.ChangeTDForDefaultLeft()%>'>
                 
                  <asp:TextBox ID="txtFromDate" TabIndex ="105" Width="250px" runat="server" CssClass="textComman"  />
                  <cc1:CalendarExtender ID="CalendarExtender1" runat="server" Enabled="True" TargetControlID="txtFromDate"
                                                                    >
                                                                </cc1:CalendarExtender>
                </td>
                <td width="170px" align='<%= Common.ChangeTDForDefaultLeft()%>' >
                    <asp:Label ID="Label1" runat="server" CssClass="labelComman" Text="<%$ Resources:Attendance,To Date %>"></asp:Label>
                </td>
                <td width="1px">
                    :
                </td>
                <td align='<%= Common.ChangeTDForDefaultLeft()%>'>
                 
               <asp:TextBox ID="txtToDate" TabIndex ="105" Width="250px" runat="server" CssClass="textComman"  />
                  <cc1:CalendarExtender ID="CalendarExtender2" runat="server" Enabled="True" TargetControlID="txtToDate"
                                                                    >
                                                                </cc1:CalendarExtender>
                </td>
                
            </tr>
          
            
            
             
            <tr>
                                           
                                                <td colspan="6"  align="center"    >
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
                                                </td>
                                               
                                            </tr>
            
        </table>
                                      </asp:Panel>
                                      
                                      <asp:Panel ID="pnlHolidayGroup" runat="server"  >
                                      <table width="100%">
                                      <tr>
                                      <td align='<%= Common.ChangeTDForDefaultLeft()%>'>
                                        <asp:RadioButton ID="rbtnGroup" CssClass="labelComman" OnCheckedChanged="EmpGroup_CheckedChanged" runat="server"  Text="<%$ Resources:Attendance,Group %>"
                                                        Font-Bold="true"         GroupName="EmpGroup" AutoPostBack="true" />
                                                                
   <asp:RadioButton ID="rbtnEmp" runat="server" CssClass="labelComman" AutoPostBack="true" Text="<%$ Resources:Attendance,Employee %>"
                                                                  GroupName="EmpGroup" Font-Bold="true"  OnCheckedChanged="EmpGroup_CheckedChanged" />
                               
                                      </td>
                                      </tr>
                                      <tr>
                                      <td>
                                      
                                       <asp:Panel ID="pnlGroup" runat="server" >
                                       <table width="100%">
                                      
                                       <tr>
                                       <td>
                                       <table width="100%">
                                       <tr>
                                       <td valign="top"  align='<%= Common.ChangeTDForDefaultLeft()%>' width="170px">
                                     <asp:ListBox ID="lbxGroup" runat="server" Height="211px" Width="171px" SelectionMode="Multiple" AutoPostBack="true"   OnSelectedIndexChanged="lbxGroup_SelectedIndexChanged"
                                                                                            CssClass="list" Font-Names="Trebuchet MS" Font-Size="Small" ForeColor="Gray">
                                                                                        </asp:ListBox>
                                                                 
                                       
                                       </td>
                                       <td valign="top"  align='<%= Common.ChangeTDForDefaultLeft()%>'>
                                       
                                       <asp:GridView ID="gvEmp" runat="server" AllowPaging="True" AutoGenerateColumns="False"
                                                OnPageIndexChanging="gvEmp_PageIndexChanging" CssClass="grid" Width="100%"
                                                 PageSize="<%# SystemParameter.GetPageSize() %>" >
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
                                       
                                       </td>
                                       
                                       </tr>
                                       </table>
                                       
                                       </asp:Panel>
                                       <asp:Panel ID="pnlEmp" runat="server" >
                                       <table width="100%">
                                       <tr>
                                       <td>
                                       
                                       </td>
                                       </tr>
                                       <tr>
                                       <td>
                                        <asp:Label ID="lblEmp" runat="server" Visible="false"></asp:Label>
                                                 
                                                 <div width="100%" style="background-image: url(../Images/bg_repeat.jpg); background-repeat: repeat;">
                                               
                                               <asp:Panel  ID="pnlEmpSearch" runat="server" DefaultButton="imgBtnBindEmp" >
                                                <table width="100%" style="padding-left: 20px; height: 38px;">
                                                    <tr>
                                                        <td align='<%= Common.ChangeTDForDefaultLeft()%>' width="120px">
                                                            <asp:Label ID="lblbinSelectOption" runat="server" CssClass="labelComman" Text="<%$ Resources:Attendance,Select Option %>"></asp:Label>
                                                        </td>
                                                        <td align='<%= Common.ChangeTDForDefaultLeft()%>' width="170px">
                                                            <asp:DropDownList ID="ddlFieldName1" runat="server" CssClass="DropdownSearch" Height="25px"
                                                                Width="165px">
                                                                <asp:ListItem Text="<%$ Resources:Attendance,Employee Code %>" Value="Emp_Code"></asp:ListItem>
                                                                <asp:ListItem Text="<%$ Resources:Attendance,Employee Name %>" Value="Emp_Name" Selected="True"></asp:ListItem>
                                                            </asp:DropDownList>
                                                        </td>
                                                        <td align='<%= Common.ChangeTDForDefaultLeft()%>' width="125px">
                                                            <asp:DropDownList ID="ddlOption1" runat="server" CssClass="DropdownSearch" Height="25px"
                                                                Width="120px">
                                                                <asp:ListItem Text="<%$ Resources:Attendance,--Select-- %>"></asp:ListItem>
                                                                <asp:ListItem Text="<%$ Resources:Attendance,Equal %>"></asp:ListItem>
                                                              
                                                                <asp:ListItem Selected="True" Text="<%$ Resources:Attendance,Contains %>"></asp:ListItem>
                                                                  <asp:ListItem Text="<%$ Resources:Attendance,Like %>"></asp:ListItem>
                                                            </asp:DropDownList>
                                                        </td>
                                                        <td align='<%= Common.ChangeTDForDefaultLeft()%>' width="24%">
                                                            <asp:TextBox ID="txtVal1" runat="server" CssClass="textCommanSearch" Height="14px"
                                                                Width="90%" Visible="true" />
                                                        </td>
                                                        <td width="40px" align="center">
                                                            <asp:ImageButton ID="imgBtnBindEmp" runat="server" CausesValidation="False" Height="25px"
                                                                ImageUrl="~/Images/search.png" Width="25px" OnClick="btnbindEmp_Click" ToolTip="<%$ Resources:Attendance,Search %>" />
                                                        </td>
                                                        <td width="30px" align="center">
                                                            <asp:Panel ID="Panel7" runat="server" DefaultButton="btnRefresh">
                                                                <asp:ImageButton ID="ImageButton2" runat="server" CausesValidation="False" Height="25px"
                                                                    ImageUrl="~/Images/refresh.png" Width="25px" OnClick="btnEmpRefresh_Click" ToolTip="<%$ Resources:Attendance,Refresh %>">
                                                                </asp:ImageButton>
                                                            </asp:Panel>
                                                        </td>
                                                      
                                                       
                                                        <td >
                                                            <asp:ImageButton ID="imgbtnSelectAll1" runat="server" OnClick="ImgbtnSelectAll_Click1"
                                                                ToolTip="<%$ Resources:Attendance, Select All %>" AutoPostBack="true" ImageUrl="~/Images/selectAll.png" />
                                                        </td>
                                                        <td align="center" width="180px">
                                                            <asp:Label ID="lblTotalRecordEmp" runat="server" Text="<%$ Resources:Attendance,Total Records: 0 %>"
                                                                CssClass="labelComman" ></asp:Label>
                                                        </td>
                                                    </tr>
                                                </table>
                                               </asp:Panel>
                                               
                                            </div>
                                                 
                                                 
                                                 
                                            <asp:GridView ID="gvEmployee" runat="server" AllowPaging="True" AutoGenerateColumns="False"
                                                OnPageIndexChanging="gvEmployee_PageIndexChanging" CssClass="grid" Width="100%"
                                                DataKeyNames="Emp_Id" PageSize="<%# SystemParameter.GetPageSize() %>" >
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
                                       
                                       </asp:Panel>
                                     
                                      
                                      </td>
                                      </tr>
                                      <tr>
                                      <td align="center" >
                                       <table style="padding-left: 8px">
                                                        <tr>
                                                            <td width="80px">
                                                                <asp:Button ID="btnSaveHoliday" runat="server" TabIndex ="107" 
                                                                    Text="<%$ Resources:Attendance,Save %>" Visible="true" CssClass="buttonCommman"
                                                                    ValidationGroup="a" onclick="btnSaveHoliday_Click" />
                                                            </td>
                                                            
                                                            <td width="80px">
                                                                <asp:Button ID="btnCancelHoliday" runat="server" TabIndex ="109" 
                                                                    Text="<%$ Resources:Attendance,Cancel %>" CssClass="buttonCommman"
                                                                     CausesValidation="False" onclick="btnCancelHoliday_Click" />
                                                            </td>
                                                            
                                                            
                                                             <td width="80px">
                                                                <asp:Button ID="btnDelete" runat="server" TabIndex ="109" 
                                                                    Text="<%$ Resources:Attendance,Delete %>" CssClass="buttonCommman"
                                                                     CausesValidation="False" onclick="btnDeleteHoliday_Click" />
                                                            </td>
                                                        </tr>
                                                    </table>
                                      
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
                                                               <asp:ListItem Selected="True" Text="<%$ Resources:Attendance,Holiday Name %>" Value="Holiday_Name"></asp:ListItem>
                                                                <asp:ListItem Text="<%$ Resources:Attendance,Holiday Name(Local) %>" Value="Holiday_Name_L"></asp:ListItem>
                                                                                           <asp:ListItem Text="<%$ Resources:Attendance,From Date %>" Value="From_Date"></asp:ListItem>
                                                               <asp:ListItem Text="<%$ Resources:Attendance,To Date %>" Value="To_Date"></asp:ListItem>
                                                            
                                                                 <asp:ListItem Text="<%$ Resources:Attendance,Holiday Id %>" Value="Holiday_Id"></asp:ListItem>
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
                                        
                                        <asp:GridView ID="gvHolidayMasterBin" PageSize="<%# SystemParameter.GetPageSize() %>"
                                            runat="server" AutoGenerateColumns="False" Width="100%" AllowPaging="True" OnPageIndexChanging="gvHolidayMasterBin_PageIndexChanging"
                                            OnSorting="gvHolidayMasterBin_OnSorting" AllowSorting="true" CssClass="grid">
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
                                                <asp:TemplateField HeaderText="<%$ Resources:Attendance,Holiday Id %>" SortExpression="Holiday_Id">
                                                    <ItemTemplate>
                                        
                                                        <asp:Label ID="lblHolidayId1"  runat="server" Text='<%# Eval("Holiday_Id") %>'></asp:Label>
                                                        
                                                        
                                                    </ItemTemplate>
                                                    <ItemStyle CssClass="grid" />
                                                </asp:TemplateField>
                                               <asp:TemplateField HeaderText="<%$ Resources:Attendance,Holiday Name %>" SortExpression="Holiday_Name">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lbHolidayName" runat="server" Text='<%# Eval("Holiday_Name") %>'></asp:Label>
                                                        
                                                         <asp:Label ID="lblHolidayId" Visible="false"  runat="server" Text='<%# Eval("Holiday_Id") %>'></asp:Label>
                                                    </ItemTemplate>
                                                    <ItemStyle CssClass="grid" />
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="<%$ Resources:Attendance,Holiday Name(Local) %>" SortExpression="Holiday_Name_L">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblHolidayNameL" runat="server" Text='<%# Eval("Holiday_Name_L") %>'></asp:Label>
                                                    </ItemTemplate>
                                                    <ItemStyle CssClass="grid" />
                                                </asp:TemplateField>
                                                
                                                
                                             <asp:TemplateField HeaderText="<%$ Resources:Attendance,From Date %>" SortExpression="From_Date">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblFromDate" runat="server" Text='<%# GetDate(Eval("From_Date")) %>'></asp:Label>
                                                    </ItemTemplate>
                                                    <ItemStyle CssClass="grid" />
                                                </asp:TemplateField>
                                                
                                                 <asp:TemplateField HeaderText="<%$ Resources:Attendance,To Date %>" SortExpression="To_Date">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lbltoDate" runat="server" Text='<%# GetDate(Eval("To_Date")) %>'></asp:Label>
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
    <asp:UpdateProgress ID="UpdateProgress1" runat="server">
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


