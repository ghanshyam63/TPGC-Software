<%@ Page Language="C#" MasterPageFile="~/ERPMaster.master" AutoEventWireup="true" CodeFile="UploadUser.aspx.cs" Inherits="Device_UploadUser" Title="Untitled Page" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
 <asp:UpdatePanel ID="UpdatePanel1" runat="server">
        <ContentTemplate>
            <table width="100%">
                <tr>
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
                                                <asp:Label ID="lblHeader" runat="server" Text="<%$ Resources:Attendance,Upload User Setup %>"
                                                    CssClass="LableHeaderTitle"></asp:Label>
                                            </td>
                                        </tr>
                                    </table>
                                </td>
                                <td align="right">
                                    
                                </td>
                            </tr>
                            <tr>
                                <td bgcolor="#ccddee"  width="100%" height="500px" valign="top">
                                 <asp:HiddenField ID="HDFSort" runat="server" />
                                <asp:Panel  ID="pnlList" runat="server" DefaultButton="imgBtnBind">
                                <table width="100%" >
                                 <tr>

                                <td style="padding-left:10px;"align='<%= Common.ChangeTDForDefaultLeft()%>' >
                                <asp:Button ID="btnNext" runat="server" CssClass="buttonCommman" OnClick="btnNext_Click"
                                                            Text="<%$ Resources:Attendance,Next%>" />
                                
                                
                                </td>
                                </tr>
                                 <tr>

                                <td>
                                
                                
                                
                                
                                
                                
                                
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
                                                                        <asp:ImageButton ID="imgBtnBind" runat="server" CausesValidation="False" Height="25px"
                                                                            ImageUrl="~/Images/search.png" Width="25px" OnClick="btnbind_Click" ToolTip="<%$ Resources:Attendance,Search %>" />
                                                                    </td>
                                                                    <td width="30px" align="center">
                                                                        <asp:Panel ID="Panel5" runat="server" DefaultButton="ImageButton2">
                                                                            <asp:ImageButton ID="ImageButton2" runat="server" CausesValidation="False" Height="25px"
                                                                                ImageUrl="~/Images/refresh.png" Width="25px" OnClick="btnRefresh_Click"
                                                                                ToolTip="<%$ Resources:Attendance,Refresh %>"></asp:ImageButton>
                                                                        </asp:Panel>
                                                                    </td>
                                                                    <td width="30px" align="center">
                                                                    </td>
                                                                    <td width="30px" align="center">
                                                                    </td>
                                                                    <td>
                                                                        <asp:ImageButton ID="ImageButton6" runat="server" OnClick="ImgbtnSelectAll_Click"
                                                                            ToolTip="<%$ Resources:Attendance, Select All %>" AutoPostBack="true" ImageUrl="~/Images/selectAll.png" />
                                                                    </td>
                                                                    <td align="center">
                                                                        <asp:Label ID="lblTotalRecords" runat="server" Text="<%$ Resources:Attendance,Total Records: 0 %>"
                                                                            CssClass="labelComman"></asp:Label>
                                                                    </td>
                                                                </tr>
                                                            </table>
                                                        </div>
                                                        <asp:Label ID="lblSelectRecd" runat="server" Visible="false"></asp:Label>
                                                        <asp:GridView ID="gvEmp" runat="server" AllowPaging="True" AutoGenerateColumns="False"
                                                            OnPageIndexChanging="gvEmp_PageIndexChanging" CssClass="grid" Width="100%"
                                                            DataKeyNames="Emp_Id" PageSize="<%# SystemParameter.GetPageSize() %>" >
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
                                                               
                                                                <asp:TemplateField HeaderText="<%$ Resources:Attendance,Employee Code %>">
                                                                    <ItemTemplate>
                                                                        <asp:Label ID="lblEmpCode" runat="server" Text='<%# Eval("Emp_Code") %>'></asp:Label>
                                                                        <asp:Label ID="lblEmpId" Visible="false" runat="server" Text='<%# Eval("Emp_Code") %>'></asp:Label>
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
                                                 
                                                    
                                                
                                
                                
                                
                                
                                </td>
                                </tr>
                                
                                </table>
                                
                                                       
                                                    
                                                    
                                
                                </asp:Panel>
                                
                                <asp:Panel ID="pnlDestDevice" runat="server">
                                                        <table width="100%">
                                                            <tr>
                                                                <td align='<%= Common.ChangeTDForDefaultLeft()%>'  style="padding-left:10px;">
                                                                    <asp:Button ID="btnBack" runat="server" CssClass="buttonCommman" OnClick="btnBack_Click"
                                                                        Text="<%$ Resources:Attendance,Back%>" />
                                                                </td>
                                                                <td align='<%= Common.ChangeTDForDefaultLeft()%>' width="10%">
                                                                    &nbsp;
                                                                </td>
                                                                <td align='<%= Common.ChangeTDForDefaultLeft()%>'>
                                                                    <asp:CheckBox ID="chkUploadFP" CssClass="labelComman" runat="server" Text="<%$ Resources:Attendance,Upload Finger%>" />
                                                                </td>
                                                                <td>
                                                                    <asp:CheckBox ID="chkUploadFace"  CssClass="labelComman" runat="server" Text="<%$ Resources:Attendance,Upload Face%>" />
                                                                </td>
                                                                <td align='<%= Common.ChangeTDForDefaultRight()%>'>
                                                                    <asp:Button ID="btnUploadUser" runat="server" OnClick="btnUploadUser_Click" Text="<%$ Resources:Attendance,Upload User%>"
                                                                        CssClass="buttonCommman" />
                                                                </td>
                                                            </tr>
                                                            <tr>
                                                                <td align='<%= Common.ChangeTDForDefaultLeft()%>' colspan="5">
                                                                   
                                                                   <asp:GridView ID="gvDevice"  DataKeyNames="Device_Id,Port,IP_Address,Device_Name" PageSize="<%# SystemParameter.GetPageSize() %>" runat="server"
                                            AutoGenerateColumns="False" Width="100%" AllowSorting="True"
                                            CssClass="grid"  OnSorting="gvDevice_OnSorting">
                                            <Columns>
                                            
                                            
                                          
                                           <asp:TemplateField>
                                                                    <ItemTemplate>
                                                                        <asp:CheckBox ID="chkSelectDevice" runat="server" />
                                                                    </ItemTemplate>
                                                                    <HeaderTemplate>
                                                                        <asp:CheckBox ID="chkSelAll" runat="server" AutoPostBack="True" OnCheckedChanged="chkSelAll_CheckedChanged1" />
                                                                    </HeaderTemplate>
                                                                    <HeaderStyle HorizontalAlign="Left" />
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
                                                                </td>
                                                              
                                                            </tr>
                                                        </table>
                                                        
                                                    </asp:Panel>
                                 
                                 <asp:Panel ID="pnlFailedRec" runat="server">
                                                        <table width="100%">
                                                            <tr>
                                                                <td   style="padding-left:10px;" align='<%= Common.ChangeTDForDefaultLeft()%>' style="width: 310px">
                                                                   
                                                                    <asp:Button ID="btnBackToList" Visible="false"  runat="server" OnClick="btnBackToList_Click" Text="<%$ Resources:Attendance,Back To List%>"
                                                                        CssClass="buttonCommman" />
                                                                </td>
                                                                <td colspan="2" align="center">
                                                                    <asp:Label ID="lblFailedRec" Font-Bold="true" CssClass="labelComman" runat="server" Text="<%$ Resources:Attendance,Failed Record%>"></asp:Label>
                                                                </td>
                                                            </tr>
                                                            <tr>
                                                                <td align='<%= Common.ChangeTDForDefaultLeft()%>' colspan="2">
                                                                    <asp:GridView ID="gvFailedRecord" AutoGenerateColumns="false" CssClass="grid" runat="server" Width="100%">
                                                                    <Columns>
                                                                    
                                                                     <asp:TemplateField HeaderText="<%$ Resources:Attendance,Employee Code %>">
                                                                    <ItemTemplate>
                                                                        <asp:Label ID="lblEmpCode" runat="server" Text='<%# Eval("Emp_Code") %>'></asp:Label>
                                                                        <asp:Label ID="lblEmpId" Visible="false" runat="server" Text='<%# Eval("Emp_Code") %>'></asp:Label>
                                                                    </ItemTemplate>
                                                                    <ItemStyle CssClass="grid" />
                                                                </asp:TemplateField>
                                                                <asp:BoundField DataField="Emp_Name" HeaderText="<%$ Resources:Attendance,Employee Name %>"
                                                                    SortExpression="Emp_Name" ItemStyle-Width="40%" />
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
                                                                       <AlternatingRowStyle CssClass="InvgridAltRow"></AlternatingRowStyle>
                                            <HeaderStyle CssClass="Invgridheader" />
                                            <PagerStyle CssClass="Invgridheader" />
                                            <RowStyle CssClass="Invgridrow" HorizontalAlign="Center"></RowStyle>
                                                                    </asp:GridView>
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

